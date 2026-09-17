<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_SD_Transaksi_Production_Barcode_Waste_Sample
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
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Lbl_Supplier = New System.Windows.Forms.Label()
		Me.Txt_NoSplit = New System.Windows.Forms.TextBox()
		Me.Txt_Kd_Barang = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Txt_Nm_Barang = New System.Windows.Forms.TextBox()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.Chk_All = New System.Windows.Forms.CheckBox()
		Me.Lv_Data_Sampel = New System.Windows.Forms.ListView()
		Me.Txt_Total_Final = New System.Windows.Forms.TextBox()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Txt_Total_KG = New System.Windows.Forms.TextBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Txt_Total_Pcs = New System.Windows.Forms.TextBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Btn_Simpan = New System.Windows.Forms.Button()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Btn_Keluar = New System.Windows.Forms.Button()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.Panel1.SuspendLayout()
		Me.Panel3.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		Me.Panel4.SuspendLayout()
		Me.SuspendLayout()
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.PanelGradient1)
		Me.Panel1.Controls.Add(Me.Label1)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(860, 45)
		Me.Panel1.TabIndex = 26
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
		Me.PanelGradient1.Size = New System.Drawing.Size(860, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold)
		Me.Label1.Location = New System.Drawing.Point(18, 7)
		Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(280, 30)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Show Dialog Data Sampel"
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Controls.Add(Me.Panel6)
		Me.Panel3.Location = New System.Drawing.Point(0, 54)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 601)
		Me.Panel3.TabIndex = 37
		Me.Panel3.Visible = False
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.Color.Red
		Me.Panel6.Location = New System.Drawing.Point(20, 485)
		Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(942, 12)
		Me.Panel6.TabIndex = 35
		Me.Panel6.Visible = False
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(-4, 44)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(942, 12)
		Me.Panel2.TabIndex = 38
		Me.Panel2.Visible = False
		'
		'Lbl_Supplier
		'
		Me.Lbl_Supplier.AutoSize = True
		Me.Lbl_Supplier.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Lbl_Supplier.Location = New System.Drawing.Point(20, 60)
		Me.Lbl_Supplier.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Lbl_Supplier.Name = "Lbl_Supplier"
		Me.Lbl_Supplier.Size = New System.Drawing.Size(56, 17)
		Me.Lbl_Supplier.TabIndex = 439
		Me.Lbl_Supplier.Text = "No Split"
		'
		'Txt_NoSplit
		'
		Me.Txt_NoSplit.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_NoSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_NoSplit.Enabled = False
		Me.Txt_NoSplit.Font = New System.Drawing.Font("Work Sans SemiBold", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Txt_NoSplit.Location = New System.Drawing.Point(106, 60)
		Me.Txt_NoSplit.Margin = New System.Windows.Forms.Padding(2)
		Me.Txt_NoSplit.MaxLength = 50
		Me.Txt_NoSplit.Name = "Txt_NoSplit"
		Me.Txt_NoSplit.Size = New System.Drawing.Size(258, 20)
		Me.Txt_NoSplit.TabIndex = 438
		Me.Txt_NoSplit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'Txt_Kd_Barang
		'
		Me.Txt_Kd_Barang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Kd_Barang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Kd_Barang.Enabled = False
		Me.Txt_Kd_Barang.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Kd_Barang.Location = New System.Drawing.Point(106, 84)
		Me.Txt_Kd_Barang.Margin = New System.Windows.Forms.Padding(2)
		Me.Txt_Kd_Barang.MaxLength = 50
		Me.Txt_Kd_Barang.Name = "Txt_Kd_Barang"
		Me.Txt_Kd_Barang.Size = New System.Drawing.Size(106, 20)
		Me.Txt_Kd_Barang.TabIndex = 438
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label2.Location = New System.Drawing.Point(20, 84)
		Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(48, 17)
		Me.Label2.TabIndex = 439
		Me.Label2.Text = "Barang"
		'
		'Txt_Nm_Barang
		'
		Me.Txt_Nm_Barang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Nm_Barang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Nm_Barang.Enabled = False
		Me.Txt_Nm_Barang.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Nm_Barang.Location = New System.Drawing.Point(216, 84)
		Me.Txt_Nm_Barang.Margin = New System.Windows.Forms.Padding(2)
		Me.Txt_Nm_Barang.MaxLength = 50
		Me.Txt_Nm_Barang.Name = "Txt_Nm_Barang"
		Me.Txt_Nm_Barang.Size = New System.Drawing.Size(399, 20)
		Me.Txt_Nm_Barang.TabIndex = 438
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.Chk_All)
		Me.GroupBox1.Controls.Add(Me.Lv_Data_Sampel)
		Me.GroupBox1.Controls.Add(Me.Txt_Total_Final)
		Me.GroupBox1.Controls.Add(Me.Label5)
		Me.GroupBox1.Controls.Add(Me.Txt_Total_KG)
		Me.GroupBox1.Controls.Add(Me.Label4)
		Me.GroupBox1.Controls.Add(Me.Txt_Total_Pcs)
		Me.GroupBox1.Controls.Add(Me.Label3)
		Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.GroupBox1.Location = New System.Drawing.Point(20, 114)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(821, 246)
		Me.GroupBox1.TabIndex = 440
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Data Sampel"
		'
		'Chk_All
		'
		Me.Chk_All.AutoSize = True
		Me.Chk_All.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Chk_All.Location = New System.Drawing.Point(6, 23)
		Me.Chk_All.Name = "Chk_All"
		Me.Chk_All.Size = New System.Drawing.Size(76, 20)
		Me.Chk_All.TabIndex = 1
		Me.Chk_All.Text = "Check All"
		Me.Chk_All.UseVisualStyleBackColor = True
		'
		'Lv_Data_Sampel
		'
		Me.Lv_Data_Sampel.CheckBoxes = True
		Me.Lv_Data_Sampel.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv_Data_Sampel.FullRowSelect = True
		Me.Lv_Data_Sampel.GridLines = True
		Me.Lv_Data_Sampel.HideSelection = False
		Me.Lv_Data_Sampel.Location = New System.Drawing.Point(6, 44)
		Me.Lv_Data_Sampel.Name = "Lv_Data_Sampel"
		Me.Lv_Data_Sampel.Size = New System.Drawing.Size(809, 169)
		Me.Lv_Data_Sampel.TabIndex = 0
		Me.Lv_Data_Sampel.UseCompatibleStateImageBehavior = False
		Me.Lv_Data_Sampel.View = System.Windows.Forms.View.Details
		'
		'Txt_Total_Final
		'
		Me.Txt_Total_Final.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Total_Final.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Total_Final.Enabled = False
		Me.Txt_Total_Final.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Total_Final.Location = New System.Drawing.Point(698, 218)
		Me.Txt_Total_Final.Margin = New System.Windows.Forms.Padding(2)
		Me.Txt_Total_Final.MaxLength = 50
		Me.Txt_Total_Final.Name = "Txt_Total_Final"
		Me.Txt_Total_Final.Size = New System.Drawing.Size(117, 20)
		Me.Txt_Total_Final.TabIndex = 438
		Me.Txt_Total_Final.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label5.Location = New System.Drawing.Point(596, 219)
		Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(98, 17)
		Me.Label5.TabIndex = 439
		Me.Label5.Text = "Total Final (KG)"
		'
		'Txt_Total_KG
		'
		Me.Txt_Total_KG.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Total_KG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Total_KG.Enabled = False
		Me.Txt_Total_KG.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Total_KG.Location = New System.Drawing.Point(475, 218)
		Me.Txt_Total_KG.Margin = New System.Windows.Forms.Padding(2)
		Me.Txt_Total_KG.MaxLength = 50
		Me.Txt_Total_KG.Name = "Txt_Total_KG"
		Me.Txt_Total_KG.Size = New System.Drawing.Size(117, 20)
		Me.Txt_Total_KG.TabIndex = 438
		Me.Txt_Total_KG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label4.Location = New System.Drawing.Point(413, 219)
		Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(58, 17)
		Me.Label4.TabIndex = 439
		Me.Label4.Text = "Total KG"
		'
		'Txt_Total_Pcs
		'
		Me.Txt_Total_Pcs.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Total_Pcs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Total_Pcs.Enabled = False
		Me.Txt_Total_Pcs.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Total_Pcs.Location = New System.Drawing.Point(288, 218)
		Me.Txt_Total_Pcs.Margin = New System.Windows.Forms.Padding(2)
		Me.Txt_Total_Pcs.MaxLength = 50
		Me.Txt_Total_Pcs.Name = "Txt_Total_Pcs"
		Me.Txt_Total_Pcs.Size = New System.Drawing.Size(117, 20)
		Me.Txt_Total_Pcs.TabIndex = 438
		Me.Txt_Total_Pcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label3.Location = New System.Drawing.Point(222, 219)
		Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(61, 17)
		Me.Label3.TabIndex = 439
		Me.Label3.Text = "Total Pcs"
		'
		'Btn_Simpan
		'
		Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Simpan.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
		Me.Btn_Simpan.Location = New System.Drawing.Point(26, 366)
		Me.Btn_Simpan.Name = "Btn_Simpan"
		Me.Btn_Simpan.Size = New System.Drawing.Size(103, 30)
		Me.Btn_Simpan.TabIndex = 441
		Me.Btn_Simpan.Text = "&Simpan"
		Me.Btn_Simpan.UseVisualStyleBackColor = False
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Controls.Add(Me.Panel5)
		Me.Panel4.Location = New System.Drawing.Point(841, 64)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(19, 601)
		Me.Panel4.TabIndex = 37
		Me.Panel4.Visible = False
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Location = New System.Drawing.Point(20, 485)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(942, 12)
		Me.Panel5.TabIndex = 35
		Me.Panel5.Visible = False
		'
		'Btn_Keluar
		'
		Me.Btn_Keluar.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Keluar.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Keluar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Keluar.ForeColor = System.Drawing.Color.White
		Me.Btn_Keluar.Location = New System.Drawing.Point(135, 366)
		Me.Btn_Keluar.Name = "Btn_Keluar"
		Me.Btn_Keluar.Size = New System.Drawing.Size(103, 30)
		Me.Btn_Keluar.TabIndex = 441
		Me.Btn_Keluar.Text = "&Keluar"
		Me.Btn_Keluar.UseVisualStyleBackColor = False
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.Color.Red
		Me.Panel7.Location = New System.Drawing.Point(16, 396)
		Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(942, 15)
		Me.Panel7.TabIndex = 38
		Me.Panel7.Visible = False
		'
		'N_EMI_SD_Transaksi_Production_Barcode_Waste_Sample
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(860, 410)
		Me.Controls.Add(Me.Btn_Keluar)
		Me.Controls.Add(Me.Btn_Simpan)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Lbl_Supplier)
		Me.Controls.Add(Me.Txt_Nm_Barang)
		Me.Controls.Add(Me.Txt_Kd_Barang)
		Me.Controls.Add(Me.Txt_NoSplit)
		Me.Controls.Add(Me.Panel7)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel1)
		Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.Name = "N_EMI_SD_Transaksi_Production_Barcode_Waste_Sample"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.Panel3.ResumeLayout(False)
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.Panel4.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Panel1 As Panel
	Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
	Friend WithEvents Label1 As Label
	Friend WithEvents Panel3 As Panel
	Friend WithEvents Panel6 As Panel
	Friend WithEvents Panel2 As Panel
	Friend WithEvents Lbl_Supplier As Label
	Friend WithEvents Txt_NoSplit As TextBox
	Friend WithEvents Txt_Kd_Barang As TextBox
	Friend WithEvents Label2 As Label
	Friend WithEvents Txt_Nm_Barang As TextBox
	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents Lv_Data_Sampel As ListView
	Friend WithEvents Btn_Simpan As Button
	Friend WithEvents Panel4 As Panel
	Friend WithEvents Panel5 As Panel
	Friend WithEvents Btn_Keluar As Button
	Friend WithEvents Panel7 As Panel
	Friend WithEvents Chk_All As CheckBox
	Friend WithEvents Txt_Total_Pcs As TextBox
	Friend WithEvents Label3 As Label
	Friend WithEvents Txt_Total_Final As TextBox
	Friend WithEvents Label5 As Label
	Friend WithEvents Txt_Total_KG As TextBox
	Friend WithEvents Label4 As Label
End Class
