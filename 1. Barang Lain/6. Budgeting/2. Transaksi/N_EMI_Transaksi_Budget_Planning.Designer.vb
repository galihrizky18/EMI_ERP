<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class N_EMI_Transaksi_Budget_Planning
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
		Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
		Me.Lbl_Judul = New System.Windows.Forms.Label()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Cmb_Department = New System.Windows.Forms.ComboBox()
		Me.Btn_Get_Data = New System.Windows.Forms.Button()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.Dgv_Data = New System.Windows.Forms.DataGridView()
		Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Panel8 = New System.Windows.Forms.Panel()
		Me.Panel9 = New System.Windows.Forms.Panel()
		Me.Panel10 = New System.Windows.Forms.Panel()
		Me.Panel11 = New System.Windows.Forms.Panel()
		Me.Btn_Refresh = New System.Windows.Forms.Button()
		Me.Btn_Simpan = New System.Windows.Forms.Button()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.Cmb_Kategori = New System.Windows.Forms.ComboBox()
		Me.Btn_Cetak_Laporan = New System.Windows.Forms.Button()
		Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Dtp_PeriodeAwal = New System.Windows.Forms.DateTimePicker()
		Me.Dtp_PeriodeAkhir = New System.Windows.Forms.DateTimePicker()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Panel1.SuspendLayout()
		Me.Panel5.SuspendLayout()
		Me.Panel2.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		CType(Me.Dgv_Data, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Panel8.SuspendLayout()
		Me.Panel10.SuspendLayout()
		Me.SuspendLayout()
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.PanelGradient1)
		Me.Panel1.Controls.Add(Me.Lbl_Judul)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(1184, 45)
		Me.Panel1.TabIndex = 32
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
		Me.PanelGradient1.Size = New System.Drawing.Size(1184, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Lbl_Judul
		'
		Me.Lbl_Judul.AutoSize = True
		Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Lbl_Judul.Location = New System.Drawing.Point(20, 7)
		Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.Lbl_Judul.Name = "Lbl_Judul"
		Me.Lbl_Judul.Size = New System.Drawing.Size(182, 30)
		Me.Lbl_Judul.TabIndex = 0
		Me.Lbl_Judul.Text = "Budget Planning"
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Location = New System.Drawing.Point(0, 53)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 637)
		Me.Panel3.TabIndex = 505
		Me.Panel3.Visible = False
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Controls.Add(Me.Panel6)
		Me.Panel5.Location = New System.Drawing.Point(-4, 45)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(1348, 12)
		Me.Panel5.TabIndex = 506
		Me.Panel5.Visible = False
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.Color.Red
		Me.Panel6.Location = New System.Drawing.Point(1167, 8)
		Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(19, 637)
		Me.Panel6.TabIndex = 495
		Me.Panel6.Visible = False
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Controls.Add(Me.Panel4)
		Me.Panel2.Location = New System.Drawing.Point(20, 596)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(1348, 15)
		Me.Panel2.TabIndex = 506
		Me.Panel2.Visible = False
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Location = New System.Drawing.Point(1167, 8)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(19, 637)
		Me.Panel4.TabIndex = 495
		Me.Panel4.Visible = False
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.Color.Red
		Me.Panel7.Location = New System.Drawing.Point(1165, 65)
		Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(19, 637)
		Me.Panel7.TabIndex = 505
		Me.Panel7.Visible = False
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label2.Location = New System.Drawing.Point(20, 59)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(79, 17)
		Me.Label2.TabIndex = 507
		Me.Label2.Text = "Department"
		'
		'Cmb_Department
		'
		Me.Cmb_Department.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Department.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Department.FormattingEnabled = True
		Me.Cmb_Department.Location = New System.Drawing.Point(105, 57)
		Me.Cmb_Department.Name = "Cmb_Department"
		Me.Cmb_Department.Size = New System.Drawing.Size(219, 24)
		Me.Cmb_Department.TabIndex = 0
		'
		'Btn_Get_Data
		'
		Me.Btn_Get_Data.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Get_Data.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Get_Data.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Get_Data.ForeColor = System.Drawing.Color.White
		Me.Btn_Get_Data.Location = New System.Drawing.Point(617, 84)
		Me.Btn_Get_Data.Name = "Btn_Get_Data"
		Me.Btn_Get_Data.Size = New System.Drawing.Size(104, 27)
		Me.Btn_Get_Data.TabIndex = 5
		Me.Btn_Get_Data.Text = "&Get Data"
		Me.Btn_Get_Data.UseVisualStyleBackColor = False
		'
		'GroupBox1
		'
		Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GroupBox1.Controls.Add(Me.Dgv_Data)
		Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.GroupBox1.Location = New System.Drawing.Point(21, 126)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(1142, 416)
		Me.GroupBox1.TabIndex = 6
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Input Budget"
		'
		'Dgv_Data
		'
		Me.Dgv_Data.AllowUserToAddRows = False
		Me.Dgv_Data.AllowUserToDeleteRows = False
		Me.Dgv_Data.BackgroundColor = System.Drawing.Color.White
		DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle1.Font = New System.Drawing.Font("Work Sans", 9.0!)
		DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.Dgv_Data.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
		Me.Dgv_Data.ColumnHeadersHeight = 45
		Me.Dgv_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
		Me.Dgv_Data.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column2, Me.Column1})
		Me.Dgv_Data.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Dgv_Data.Location = New System.Drawing.Point(3, 18)
		Me.Dgv_Data.Name = "Dgv_Data"
		Me.Dgv_Data.RowHeadersWidth = 30
		Me.Dgv_Data.Size = New System.Drawing.Size(1136, 395)
		Me.Dgv_Data.TabIndex = 0
		'
		'Column2
		'
		Me.Column2.HeaderText = "ID Kategori_Layer3"
		Me.Column2.Name = "Column2"
		Me.Column2.ReadOnly = True
		Me.Column2.Visible = False
		'
		'Column1
		'
		Me.Column1.HeaderText = "Kategori Layer 3"
		Me.Column1.Name = "Column1"
		Me.Column1.ReadOnly = True
		Me.Column1.Width = 150
		'
		'Panel8
		'
		Me.Panel8.BackColor = System.Drawing.Color.Red
		Me.Panel8.Controls.Add(Me.Panel9)
		Me.Panel8.Location = New System.Drawing.Point(22, 112)
		Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel8.Name = "Panel8"
		Me.Panel8.Size = New System.Drawing.Size(1348, 12)
		Me.Panel8.TabIndex = 506
		Me.Panel8.Visible = False
		'
		'Panel9
		'
		Me.Panel9.BackColor = System.Drawing.Color.Red
		Me.Panel9.Location = New System.Drawing.Point(1167, 8)
		Me.Panel9.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel9.Name = "Panel9"
		Me.Panel9.Size = New System.Drawing.Size(19, 637)
		Me.Panel9.TabIndex = 495
		Me.Panel9.Visible = False
		'
		'Panel10
		'
		Me.Panel10.BackColor = System.Drawing.Color.Red
		Me.Panel10.Controls.Add(Me.Panel11)
		Me.Panel10.Location = New System.Drawing.Point(13, 542)
		Me.Panel10.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel10.Name = "Panel10"
		Me.Panel10.Size = New System.Drawing.Size(1348, 12)
		Me.Panel10.TabIndex = 506
		Me.Panel10.Visible = False
		'
		'Panel11
		'
		Me.Panel11.BackColor = System.Drawing.Color.Red
		Me.Panel11.Location = New System.Drawing.Point(1167, 8)
		Me.Panel11.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel11.Name = "Panel11"
		Me.Panel11.Size = New System.Drawing.Size(19, 637)
		Me.Panel11.TabIndex = 495
		Me.Panel11.Visible = False
		'
		'Btn_Refresh
		'
		Me.Btn_Refresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Refresh.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 10.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
		Me.Btn_Refresh.Location = New System.Drawing.Point(195, 556)
		Me.Btn_Refresh.Margin = New System.Windows.Forms.Padding(2)
		Me.Btn_Refresh.Name = "Btn_Refresh"
		Me.Btn_Refresh.Size = New System.Drawing.Size(170, 38)
		Me.Btn_Refresh.TabIndex = 8
		Me.Btn_Refresh.Text = "&Refresh"
		Me.Btn_Refresh.UseVisualStyleBackColor = False
		'
		'Btn_Simpan
		'
		Me.Btn_Simpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Simpan.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 10.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
		Me.Btn_Simpan.Location = New System.Drawing.Point(22, 556)
		Me.Btn_Simpan.Margin = New System.Windows.Forms.Padding(2)
		Me.Btn_Simpan.Name = "Btn_Simpan"
		Me.Btn_Simpan.Size = New System.Drawing.Size(169, 38)
		Me.Btn_Simpan.TabIndex = 7
		Me.Btn_Simpan.Text = "&Simpan"
		Me.Btn_Simpan.UseVisualStyleBackColor = False
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label6.Location = New System.Drawing.Point(330, 59)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(56, 17)
		Me.Label6.TabIndex = 507
		Me.Label6.Text = "Kategori"
		'
		'Cmb_Kategori
		'
		Me.Cmb_Kategori.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Kategori.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Kategori.Enabled = False
		Me.Cmb_Kategori.FormattingEnabled = True
		Me.Cmb_Kategori.Location = New System.Drawing.Point(392, 57)
		Me.Cmb_Kategori.Name = "Cmb_Kategori"
		Me.Cmb_Kategori.Size = New System.Drawing.Size(219, 24)
		Me.Cmb_Kategori.TabIndex = 1
		'
		'Btn_Cetak_Laporan
		'
		Me.Btn_Cetak_Laporan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Cetak_Laporan.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Cetak_Laporan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Cetak_Laporan.ForeColor = System.Drawing.Color.White
		Me.Btn_Cetak_Laporan.Location = New System.Drawing.Point(727, 84)
		Me.Btn_Cetak_Laporan.Name = "Btn_Cetak_Laporan"
		Me.Btn_Cetak_Laporan.Size = New System.Drawing.Size(131, 27)
		Me.Btn_Cetak_Laporan.TabIndex = 508
		Me.Btn_Cetak_Laporan.Text = "&Cetak Laporan"
		Me.Btn_Cetak_Laporan.UseVisualStyleBackColor = False
		'
		'DataGridViewTextBoxColumn1
		'
		Me.DataGridViewTextBoxColumn1.HeaderText = "Kategori Layer 3"
		Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
		Me.DataGridViewTextBoxColumn1.ReadOnly = True
		Me.DataGridViewTextBoxColumn1.Visible = False
		Me.DataGridViewTextBoxColumn1.Width = 150
		'
		'DataGridViewTextBoxColumn2
		'
		Me.DataGridViewTextBoxColumn2.HeaderText = "Kategori Layer 3"
		Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
		Me.DataGridViewTextBoxColumn2.ReadOnly = True
		Me.DataGridViewTextBoxColumn2.Width = 150
		'
		'Dtp_PeriodeAwal
		'
		Me.Dtp_PeriodeAwal.CustomFormat = "MMMM yyyy"
		Me.Dtp_PeriodeAwal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
		Me.Dtp_PeriodeAwal.Location = New System.Drawing.Point(105, 87)
		Me.Dtp_PeriodeAwal.Name = "Dtp_PeriodeAwal"
		Me.Dtp_PeriodeAwal.Size = New System.Drawing.Size(219, 20)
		Me.Dtp_PeriodeAwal.TabIndex = 510
		'
		'Dtp_PeriodeAkhir
		'
		Me.Dtp_PeriodeAkhir.CustomFormat = "MMMM yyyy"
		Me.Dtp_PeriodeAkhir.Format = System.Windows.Forms.DateTimePickerFormat.Custom
		Me.Dtp_PeriodeAkhir.Location = New System.Drawing.Point(392, 87)
		Me.Dtp_PeriodeAkhir.Name = "Dtp_PeriodeAkhir"
		Me.Dtp_PeriodeAkhir.Size = New System.Drawing.Size(219, 20)
		Me.Dtp_PeriodeAkhir.TabIndex = 511
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label1.Location = New System.Drawing.Point(22, 90)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(52, 17)
		Me.Label1.TabIndex = 507
		Me.Label1.Text = "Periode"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label3.Location = New System.Drawing.Point(350, 89)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(26, 17)
		Me.Label3.TabIndex = 507
		Me.Label3.Text = "s/d"
		'
		'N_EMI_Transaksi_Budget_Planning
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(1184, 611)
		Me.Controls.Add(Me.Dtp_PeriodeAkhir)
		Me.Controls.Add(Me.Dtp_PeriodeAwal)
		Me.Controls.Add(Me.Btn_Cetak_Laporan)
		Me.Controls.Add(Me.Btn_Refresh)
		Me.Controls.Add(Me.Btn_Simpan)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.Btn_Get_Data)
		Me.Controls.Add(Me.Cmb_Kategori)
		Me.Controls.Add(Me.Cmb_Department)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Label6)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel10)
		Me.Controls.Add(Me.Panel8)
		Me.Controls.Add(Me.Panel5)
		Me.Controls.Add(Me.Panel7)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel1)
		Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.MaximizeBox = False
		Me.Name = "N_EMI_Transaksi_Budget_Planning"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.Panel5.ResumeLayout(False)
		Me.Panel2.ResumeLayout(False)
		Me.GroupBox1.ResumeLayout(False)
		CType(Me.Dgv_Data, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Panel8.ResumeLayout(False)
		Me.Panel10.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Panel1 As Panel
	Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
	Friend WithEvents Lbl_Judul As Label
	Friend WithEvents Panel3 As Panel
	Friend WithEvents Panel5 As Panel
	Friend WithEvents Panel6 As Panel
	Friend WithEvents Panel2 As Panel
	Friend WithEvents Panel4 As Panel
	Friend WithEvents Panel7 As Panel
	Friend WithEvents Label2 As Label
	Friend WithEvents Cmb_Department As ComboBox
	Friend WithEvents Btn_Get_Data As Button
	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents Panel8 As Panel
	Friend WithEvents Panel9 As Panel
	Friend WithEvents Panel10 As Panel
	Friend WithEvents Panel11 As Panel
	Friend WithEvents Btn_Refresh As Button
	Friend WithEvents Btn_Simpan As Button
	Friend WithEvents Dgv_Data As DataGridView
	Friend WithEvents Label6 As Label
	Friend WithEvents Cmb_Kategori As ComboBox
	Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
	Friend WithEvents Column2 As DataGridViewTextBoxColumn
	Friend WithEvents Column1 As DataGridViewTextBoxColumn
	Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
	Friend WithEvents Btn_Cetak_Laporan As Button
	Friend WithEvents Dtp_PeriodeAwal As DateTimePicker
	Friend WithEvents Dtp_PeriodeAkhir As DateTimePicker
	Friend WithEvents Label1 As Label
	Friend WithEvents Label3 As Label
End Class
