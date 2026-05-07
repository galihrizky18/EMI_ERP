<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Laporan_Adjustment_Stock_Barang_Lain
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Rd_Status_Belum_Validasi = New System.Windows.Forms.RadioButton()
        Me.Rd_Status_Validasi = New System.Windows.Forms.RadioButton()
        Me.Rd_Status_Seluruh = New System.Windows.Forms.RadioButton()
        Me.Cmb_Lain = New System.Windows.Forms.ComboBox()
        Me.Cmb_KdSO = New System.Windows.Forms.ComboBox()
        Me.Txt_No_Faktur = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Txt_Ket_Faktur = New System.Windows.Forms.TextBox()
        Me.Txt_Lain = New System.Windows.Forms.TextBox()
        Me.BtnCetak = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Lv_Faktur = New System.Windows.Forms.ListView()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(670, 45)
        Me.Panel1.TabIndex = 346
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(19, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(418, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Laporan Adjustment Stock Barang Lain"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 56)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 714)
        Me.Panel3.TabIndex = 348
        Me.Panel3.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 44)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1284, 12)
        Me.Panel2.TabIndex = 349
        Me.Panel2.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Rd_Status_Belum_Validasi)
        Me.GroupBox1.Controls.Add(Me.Rd_Status_Validasi)
        Me.GroupBox1.Controls.Add(Me.Rd_Status_Seluruh)
        Me.GroupBox1.Controls.Add(Me.Cmb_Lain)
        Me.GroupBox1.Controls.Add(Me.Cmb_KdSO)
        Me.GroupBox1.Controls.Add(Me.Txt_No_Faktur)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Tgl2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Tgl1)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Txt_Ket_Faktur)
        Me.GroupBox1.Controls.Add(Me.Txt_Lain)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 56)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(630, 176)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Rd_Status_Belum_Validasi
        '
        Me.Rd_Status_Belum_Validasi.AutoSize = True
        Me.Rd_Status_Belum_Validasi.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Rd_Status_Belum_Validasi.Location = New System.Drawing.Point(294, 50)
        Me.Rd_Status_Belum_Validasi.Name = "Rd_Status_Belum_Validasi"
        Me.Rd_Status_Belum_Validasi.Size = New System.Drawing.Size(102, 20)
        Me.Rd_Status_Belum_Validasi.TabIndex = 4
        Me.Rd_Status_Belum_Validasi.TabStop = True
        Me.Rd_Status_Belum_Validasi.Tag = "Belum Validasi"
        Me.Rd_Status_Belum_Validasi.Text = "Belum Validasi"
        Me.Rd_Status_Belum_Validasi.UseVisualStyleBackColor = True
        '
        'Rd_Status_Validasi
        '
        Me.Rd_Status_Validasi.AutoSize = True
        Me.Rd_Status_Validasi.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Rd_Status_Validasi.Location = New System.Drawing.Point(212, 50)
        Me.Rd_Status_Validasi.Name = "Rd_Status_Validasi"
        Me.Rd_Status_Validasi.Size = New System.Drawing.Size(65, 20)
        Me.Rd_Status_Validasi.TabIndex = 3
        Me.Rd_Status_Validasi.TabStop = True
        Me.Rd_Status_Validasi.Tag = "Sudah Validasi"
        Me.Rd_Status_Validasi.Text = "Validasi"
        Me.Rd_Status_Validasi.UseVisualStyleBackColor = True
        '
        'Rd_Status_Seluruh
        '
        Me.Rd_Status_Seluruh.AutoSize = True
        Me.Rd_Status_Seluruh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Rd_Status_Seluruh.Location = New System.Drawing.Point(130, 50)
        Me.Rd_Status_Seluruh.Name = "Rd_Status_Seluruh"
        Me.Rd_Status_Seluruh.Size = New System.Drawing.Size(66, 20)
        Me.Rd_Status_Seluruh.TabIndex = 2
        Me.Rd_Status_Seluruh.TabStop = True
        Me.Rd_Status_Seluruh.Tag = "Seluruh"
        Me.Rd_Status_Seluruh.Text = "Seluruh"
        Me.Rd_Status_Seluruh.UseVisualStyleBackColor = True
        '
        'Cmb_Lain
        '
        Me.Cmb_Lain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lain.FormattingEnabled = True
        Me.Cmb_Lain.Location = New System.Drawing.Point(130, 139)
        Me.Cmb_Lain.Name = "Cmb_Lain"
        Me.Cmb_Lain.Size = New System.Drawing.Size(163, 24)
        Me.Cmb_Lain.TabIndex = 7
        '
        'Cmb_KdSO
        '
        Me.Cmb_KdSO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_KdSO.FormattingEnabled = True
        Me.Cmb_KdSO.Location = New System.Drawing.Point(130, 79)
        Me.Cmb_KdSO.Name = "Cmb_KdSO"
        Me.Cmb_KdSO.Size = New System.Drawing.Size(163, 24)
        Me.Cmb_KdSO.TabIndex = 5
        '
        'Txt_No_Faktur
        '
        Me.Txt_No_Faktur.Location = New System.Drawing.Point(130, 109)
        Me.Txt_No_Faktur.Name = "Txt_No_Faktur"
        Me.Txt_No_Faktur.Size = New System.Drawing.Size(163, 20)
        Me.Txt_No_Faktur.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(8, 111)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 17)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "No Faktur"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(8, 81)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(58, 17)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Kode SO"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(8, 51)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(104, 17)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Status Transaksi"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 142)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 16)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Lainnya"
        '
        'Tgl2
        '
        Me.Tgl2.CustomFormat = "dd MMMM yyyy"
        Me.Tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl2.Location = New System.Drawing.Point(341, 22)
        Me.Tgl2.Name = "Tgl2"
        Me.Tgl2.Size = New System.Drawing.Size(163, 20)
        Me.Tgl2.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(303, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "s/d"
        '
        'Tgl1
        '
        Me.Tgl1.CustomFormat = "dd MMMM yyyy"
        Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl1.Location = New System.Drawing.Point(130, 22)
        Me.Tgl1.Name = "Tgl1"
        Me.Tgl1.Size = New System.Drawing.Size(163, 20)
        Me.Tgl1.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(8, 23)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(52, 17)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Periode"
        '
        'Txt_Ket_Faktur
        '
        Me.Txt_Ket_Faktur.Enabled = False
        Me.Txt_Ket_Faktur.Location = New System.Drawing.Point(299, 109)
        Me.Txt_Ket_Faktur.Name = "Txt_Ket_Faktur"
        Me.Txt_Ket_Faktur.Size = New System.Drawing.Size(321, 20)
        Me.Txt_Ket_Faktur.TabIndex = 13
        '
        'Txt_Lain
        '
        Me.Txt_Lain.Enabled = False
        Me.Txt_Lain.Location = New System.Drawing.Point(299, 142)
        Me.Txt_Lain.Name = "Txt_Lain"
        Me.Txt_Lain.Size = New System.Drawing.Size(321, 20)
        Me.Txt_Lain.TabIndex = 8
        '
        'BtnCetak
        '
        Me.BtnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnCetak.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnCetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnCetak.ForeColor = System.Drawing.Color.White
        Me.BtnCetak.Location = New System.Drawing.Point(467, 238)
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
        Me.BtnExit.Location = New System.Drawing.Point(556, 238)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(84, 33)
        Me.BtnExit.TabIndex = 2
        Me.BtnExit.Text = "&Keluar"
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(651, 61)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 714)
        Me.Panel4.TabIndex = 348
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(23, 271)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1284, 15)
        Me.Panel5.TabIndex = 349
        Me.Panel5.Visible = False
        '
        'Lv_Faktur
        '
        Me.Lv_Faktur.BackColor = System.Drawing.Color.White
        Me.Lv_Faktur.FullRowSelect = True
        Me.Lv_Faktur.GridLines = True
        Me.Lv_Faktur.HideSelection = False
        Me.Lv_Faktur.Location = New System.Drawing.Point(676, 187)
        Me.Lv_Faktur.Name = "Lv_Faktur"
        Me.Lv_Faktur.Size = New System.Drawing.Size(490, 158)
        Me.Lv_Faktur.TabIndex = 353
        Me.Lv_Faktur.UseCompatibleStateImageBehavior = False
        Me.Lv_Faktur.View = System.Windows.Forms.View.Details
        Me.Lv_Faktur.Visible = False
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
        Me.PanelGradient1.Size = New System.Drawing.Size(670, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'N_EMI_Laporan_Adjustment_Stock_Barang_Lain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(670, 285)
        Me.Controls.Add(Me.Lv_Faktur)
        Me.Controls.Add(Me.BtnCetak)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_Laporan_Adjustment_Stock_Barang_Lain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
	Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
	Friend WithEvents Label1 As Label
	Friend WithEvents Panel3 As Panel
	Friend WithEvents Panel2 As Panel
	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents Rd_Status_Belum_Validasi As RadioButton
	Friend WithEvents Rd_Status_Validasi As RadioButton
	Friend WithEvents Rd_Status_Seluruh As RadioButton
	Friend WithEvents Cmb_Lain As ComboBox
	Friend WithEvents Txt_No_Faktur As TextBox
	Friend WithEvents Label5 As Label
	Friend WithEvents Label8 As Label
	Friend WithEvents Label6 As Label
	Friend WithEvents Tgl2 As DateTimePicker
	Friend WithEvents Label3 As Label
	Friend WithEvents Tgl1 As DateTimePicker
	Friend WithEvents Label9 As Label
	Friend WithEvents Txt_Lain As TextBox
	Friend WithEvents Cmb_KdSO As ComboBox
	Friend WithEvents Label7 As Label
	Friend WithEvents BtnCetak As Button
	Friend WithEvents BtnExit As Button
	Friend WithEvents Panel4 As Panel
	Friend WithEvents Panel5 As Panel
	Friend WithEvents Lv_Faktur As ListView
	Friend WithEvents Txt_Ket_Faktur As TextBox
End Class
