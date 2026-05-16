<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_SD_Transaksi_Reformulasi_Formula_Trial
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
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Lv_Parent = New System.Windows.Forms.ListView()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.Txt_Tot_Persen = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Txt_Tot_HPP = New System.Windows.Forms.TextBox()
		Me.LblFormulator_TotalPersen = New System.Windows.Forms.Label()
		Me.Lv_Detail_Bahan = New System.Windows.Forms.ListView()
		Me.GroupBox2 = New System.Windows.Forms.GroupBox()
		Me.Lv_Detail_MoistureContent = New System.Windows.Forms.ListView()
		Me.Panel8 = New System.Windows.Forms.Panel()
		Me.Btn_Reset_Filter = New System.Windows.Forms.Button()
		Me.Cmb_Filter = New System.Windows.Forms.ComboBox()
		Me.Txt_Filter = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Panel1.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		Me.GroupBox2.SuspendLayout()
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
		Me.Panel1.Size = New System.Drawing.Size(1184, 51)
		Me.Panel1.TabIndex = 24
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
		Me.PanelGradient1.Size = New System.Drawing.Size(1184, 2)
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
		Me.LblFormulator_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LblFormulator_Judul.Location = New System.Drawing.Point(20, 11)
		Me.LblFormulator_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.LblFormulator_Judul.Name = "LblFormulator_Judul"
		Me.LblFormulator_Judul.Size = New System.Drawing.Size(151, 30)
		Me.LblFormulator_Judul.TabIndex = 0
		Me.LblFormulator_Judul.Text = "Formula Trial"
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(2, 51)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(1142, 12)
		Me.Panel2.TabIndex = 36
		Me.Panel2.Visible = False
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Location = New System.Drawing.Point(0, 59)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 574)
		Me.Panel3.TabIndex = 37
		Me.Panel3.Visible = False
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Location = New System.Drawing.Point(17, 546)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(1142, 15)
		Me.Panel4.TabIndex = 36
		Me.Panel4.Visible = False
		'
		'Lv_Parent
		'
		Me.Lv_Parent.FullRowSelect = True
		Me.Lv_Parent.GridLines = True
		Me.Lv_Parent.HideSelection = False
		Me.Lv_Parent.Location = New System.Drawing.Point(20, 91)
		Me.Lv_Parent.Name = "Lv_Parent"
		Me.Lv_Parent.Size = New System.Drawing.Size(1147, 182)
		Me.Lv_Parent.TabIndex = 0
		Me.Lv_Parent.UseCompatibleStateImageBehavior = False
		Me.Lv_Parent.View = System.Windows.Forms.View.Details
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.Color.Red
		Me.Panel6.Location = New System.Drawing.Point(1167, 63)
		Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(19, 574)
		Me.Panel6.TabIndex = 37
		Me.Panel6.Visible = False
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.Color.Red
		Me.Panel7.Location = New System.Drawing.Point(27, 273)
		Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(1142, 12)
		Me.Panel7.TabIndex = 36
		Me.Panel7.Visible = False
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.Txt_Tot_Persen)
		Me.GroupBox1.Controls.Add(Me.Label1)
		Me.GroupBox1.Controls.Add(Me.Txt_Tot_HPP)
		Me.GroupBox1.Controls.Add(Me.LblFormulator_TotalPersen)
		Me.GroupBox1.Controls.Add(Me.Lv_Detail_Bahan)
		Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.GroupBox1.Location = New System.Drawing.Point(21, 287)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(568, 258)
		Me.GroupBox1.TabIndex = 39
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Detail Bahan"
		'
		'Txt_Tot_Persen
		'
		Me.Txt_Tot_Persen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Tot_Persen.Enabled = False
		Me.Txt_Tot_Persen.Font = New System.Drawing.Font("Work Sans SemiBold", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Txt_Tot_Persen.Location = New System.Drawing.Point(263, 232)
		Me.Txt_Tot_Persen.Name = "Txt_Tot_Persen"
		Me.Txt_Tot_Persen.ReadOnly = True
		Me.Txt_Tot_Persen.Size = New System.Drawing.Size(100, 20)
		Me.Txt_Tot_Persen.TabIndex = 399
		Me.Txt_Tot_Persen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label1.Location = New System.Drawing.Point(152, 233)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(105, 17)
		Me.Label1.TabIndex = 400
		Me.Label1.Text = "Total Persentase"
		'
		'Txt_Tot_HPP
		'
		Me.Txt_Tot_HPP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Tot_HPP.Enabled = False
		Me.Txt_Tot_HPP.Font = New System.Drawing.Font("Work Sans SemiBold", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Txt_Tot_HPP.Location = New System.Drawing.Point(465, 232)
		Me.Txt_Tot_HPP.Name = "Txt_Tot_HPP"
		Me.Txt_Tot_HPP.ReadOnly = True
		Me.Txt_Tot_HPP.Size = New System.Drawing.Size(100, 20)
		Me.Txt_Tot_HPP.TabIndex = 397
		Me.Txt_Tot_HPP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		'
		'LblFormulator_TotalPersen
		'
		Me.LblFormulator_TotalPersen.AutoSize = True
		Me.LblFormulator_TotalPersen.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.LblFormulator_TotalPersen.Location = New System.Drawing.Point(369, 233)
		Me.LblFormulator_TotalPersen.Name = "LblFormulator_TotalPersen"
		Me.LblFormulator_TotalPersen.Size = New System.Drawing.Size(91, 17)
		Me.LblFormulator_TotalPersen.TabIndex = 398
		Me.LblFormulator_TotalPersen.Text = "Total HPP PCS"
		'
		'Lv_Detail_Bahan
		'
		Me.Lv_Detail_Bahan.Dock = System.Windows.Forms.DockStyle.Top
		Me.Lv_Detail_Bahan.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv_Detail_Bahan.FullRowSelect = True
		Me.Lv_Detail_Bahan.GridLines = True
		Me.Lv_Detail_Bahan.HideSelection = False
		Me.Lv_Detail_Bahan.Location = New System.Drawing.Point(3, 18)
		Me.Lv_Detail_Bahan.Name = "Lv_Detail_Bahan"
		Me.Lv_Detail_Bahan.Size = New System.Drawing.Size(562, 208)
		Me.Lv_Detail_Bahan.TabIndex = 0
		Me.Lv_Detail_Bahan.UseCompatibleStateImageBehavior = False
		Me.Lv_Detail_Bahan.View = System.Windows.Forms.View.Details
		'
		'GroupBox2
		'
		Me.GroupBox2.Controls.Add(Me.Lv_Detail_MoistureContent)
		Me.GroupBox2.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.GroupBox2.Location = New System.Drawing.Point(597, 287)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(568, 258)
		Me.GroupBox2.TabIndex = 40
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "Moisture Content"
		'
		'Lv_Detail_MoistureContent
		'
		Me.Lv_Detail_MoistureContent.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Lv_Detail_MoistureContent.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv_Detail_MoistureContent.FullRowSelect = True
		Me.Lv_Detail_MoistureContent.GridLines = True
		Me.Lv_Detail_MoistureContent.HideSelection = False
		Me.Lv_Detail_MoistureContent.Location = New System.Drawing.Point(3, 18)
		Me.Lv_Detail_MoistureContent.Name = "Lv_Detail_MoistureContent"
		Me.Lv_Detail_MoistureContent.Size = New System.Drawing.Size(562, 237)
		Me.Lv_Detail_MoistureContent.TabIndex = 0
		Me.Lv_Detail_MoistureContent.UseCompatibleStateImageBehavior = False
		Me.Lv_Detail_MoistureContent.View = System.Windows.Forms.View.Details
		'
		'Panel8
		'
		Me.Panel8.BackColor = System.Drawing.Color.Red
		Me.Panel8.Location = New System.Drawing.Point(588, 287)
		Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel8.Name = "Panel8"
		Me.Panel8.Size = New System.Drawing.Size(12, 574)
		Me.Panel8.TabIndex = 37
		Me.Panel8.Visible = False
		'
		'Btn_Reset_Filter
		'
		Me.Btn_Reset_Filter.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Reset_Filter.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Reset_Filter.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Reset_Filter.ForeColor = System.Drawing.Color.White
		Me.Btn_Reset_Filter.Location = New System.Drawing.Point(585, 56)
		Me.Btn_Reset_Filter.Name = "Btn_Reset_Filter"
		Me.Btn_Reset_Filter.Size = New System.Drawing.Size(109, 30)
		Me.Btn_Reset_Filter.TabIndex = 3
		Me.Btn_Reset_Filter.Text = "&Reset"
		Me.Btn_Reset_Filter.UseVisualStyleBackColor = False
		'
		'Cmb_Filter
		'
		Me.Cmb_Filter.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Filter.FormattingEnabled = True
		Me.Cmb_Filter.Items.AddRange(New Object() {"Ali"})
		Me.Cmb_Filter.Location = New System.Drawing.Point(67, 60)
		Me.Cmb_Filter.Name = "Cmb_Filter"
		Me.Cmb_Filter.Size = New System.Drawing.Size(133, 24)
		Me.Cmb_Filter.TabIndex = 1
		'
		'Txt_Filter
		'
		Me.Txt_Filter.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Filter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Filter.Enabled = False
		Me.Txt_Filter.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Filter.Location = New System.Drawing.Point(206, 62)
		Me.Txt_Filter.MaxLength = 50
		Me.Txt_Filter.Name = "Txt_Filter"
		Me.Txt_Filter.Size = New System.Drawing.Size(375, 20)
		Me.Txt_Filter.TabIndex = 2
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label2.Location = New System.Drawing.Point(22, 63)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(39, 17)
		Me.Label2.TabIndex = 454
		Me.Label2.Text = "Filter"
		'
		'N_EMI_SD_Transaksi_Reformulasi_Formula_Trial
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(1184, 561)
		Me.Controls.Add(Me.Btn_Reset_Filter)
		Me.Controls.Add(Me.Cmb_Filter)
		Me.Controls.Add(Me.Txt_Filter)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.GroupBox2)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.Lv_Parent)
		Me.Controls.Add(Me.Panel6)
		Me.Controls.Add(Me.Panel8)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Panel7)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel1)
		Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.Name = "N_EMI_SD_Transaksi_Reformulasi_Formula_Trial"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.GroupBox2.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Panel1 As Panel
	Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
	Friend WithEvents Panel5 As Panel
	Friend WithEvents LblFormulator_Judul As Label
	Friend WithEvents Panel2 As Panel
	Friend WithEvents Panel3 As Panel
	Friend WithEvents Panel4 As Panel
	Friend WithEvents Lv_Parent As ListView
	Friend WithEvents Panel6 As Panel
	Friend WithEvents Panel7 As Panel
	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents GroupBox2 As GroupBox
	Friend WithEvents Lv_Detail_Bahan As ListView
	Friend WithEvents Lv_Detail_MoistureContent As ListView
	Friend WithEvents Panel8 As Panel
	Friend WithEvents Txt_Tot_HPP As TextBox
	Friend WithEvents LblFormulator_TotalPersen As Label
	Friend WithEvents Txt_Tot_Persen As TextBox
	Friend WithEvents Label1 As Label
	Friend WithEvents Btn_Reset_Filter As Button
	Friend WithEvents Cmb_Filter As ComboBox
	Friend WithEvents Txt_Filter As TextBox
	Friend WithEvents Label2 As Label
End Class
