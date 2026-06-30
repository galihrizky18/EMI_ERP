<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_SD_List_Purchase_Requisition_Department_Barang_Lain
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
		Me.Lbl_Judul = New System.Windows.Forms.Label()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Cmb_Filter = New System.Windows.Forms.ComboBox()
		Me.Btn_cari = New System.Windows.Forms.Button()
		Me.Txt_Value_Filter = New System.Windows.Forms.TextBox()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.Txt_Pages = New System.Windows.Forms.TextBox()
		Me.Btn_First = New System.Windows.Forms.Button()
		Me.Btn_Next = New System.Windows.Forms.Button()
		Me.Btn_Prev = New System.Windows.Forms.Button()
		Me.Panel8 = New System.Windows.Forms.Panel()
		Me.Lv_Data_PR = New System.Windows.Forms.ListView()
		Me.Panel1.SuspendLayout()
		Me.Panel4.SuspendLayout()
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
		Me.Panel1.TabIndex = 25
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
		Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Lbl_Judul.Location = New System.Drawing.Point(20, 7)
		Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.Lbl_Judul.Name = "Lbl_Judul"
		Me.Lbl_Judul.Size = New System.Drawing.Size(391, 29)
		Me.Lbl_Judul.TabIndex = 0
		Me.Lbl_Judul.Text = "List Purchase Requisition Department"
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(5, 45)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(1087, 12)
		Me.Panel2.TabIndex = 39
		Me.Panel2.Visible = False
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Location = New System.Drawing.Point(0, 65)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 649)
		Me.Panel3.TabIndex = 40
		Me.Panel3.Visible = False
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Controls.Add(Me.Panel5)
		Me.Panel4.Location = New System.Drawing.Point(1165, 65)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(19, 649)
		Me.Panel4.TabIndex = 40
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
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label2.Location = New System.Drawing.Point(22, 65)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(39, 17)
		Me.Label2.TabIndex = 434
		Me.Label2.Text = "Filter"
		'
		'Cmb_Filter
		'
		Me.Cmb_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Filter.FormattingEnabled = True
		Me.Cmb_Filter.Location = New System.Drawing.Point(86, 60)
		Me.Cmb_Filter.Name = "Cmb_Filter"
		Me.Cmb_Filter.Size = New System.Drawing.Size(156, 24)
		Me.Cmb_Filter.TabIndex = 497
		'
		'Btn_cari
		'
		Me.Btn_cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_cari.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_cari.ForeColor = System.Drawing.Color.White
		Me.Btn_cari.Location = New System.Drawing.Point(567, 58)
		Me.Btn_cari.Name = "Btn_cari"
		Me.Btn_cari.Size = New System.Drawing.Size(94, 27)
		Me.Btn_cari.TabIndex = 499
		Me.Btn_cari.Text = "&Search"
		Me.Btn_cari.UseVisualStyleBackColor = False
		'
		'Txt_Value_Filter
		'
		Me.Txt_Value_Filter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Value_Filter.Enabled = False
		Me.Txt_Value_Filter.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Value_Filter.Location = New System.Drawing.Point(248, 62)
		Me.Txt_Value_Filter.Name = "Txt_Value_Filter"
		Me.Txt_Value_Filter.Size = New System.Drawing.Size(313, 20)
		Me.Txt_Value_Filter.TabIndex = 498
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.Color.Red
		Me.Panel6.Location = New System.Drawing.Point(25, 86)
		Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(1087, 12)
		Me.Panel6.TabIndex = 39
		Me.Panel6.Visible = False
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.Color.Red
		Me.Panel7.Location = New System.Drawing.Point(21, 596)
		Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(1087, 15)
		Me.Panel7.TabIndex = 39
		Me.Panel7.Visible = False
		'
		'Txt_Pages
		'
		Me.Txt_Pages.BackColor = System.Drawing.Color.White
		Me.Txt_Pages.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Txt_Pages.Enabled = False
		Me.Txt_Pages.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Txt_Pages.ForeColor = System.Drawing.SystemColors.WindowText
		Me.Txt_Pages.Location = New System.Drawing.Point(984, 573)
		Me.Txt_Pages.Name = "Txt_Pages"
		Me.Txt_Pages.Size = New System.Drawing.Size(73, 15)
		Me.Txt_Pages.TabIndex = 508
		Me.Txt_Pages.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'Btn_First
		'
		Me.Btn_First.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_First.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_First.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_First.ForeColor = System.Drawing.Color.White
		Me.Btn_First.Location = New System.Drawing.Point(754, 566)
		Me.Btn_First.Name = "Btn_First"
		Me.Btn_First.Size = New System.Drawing.Size(102, 30)
		Me.Btn_First.TabIndex = 507
		Me.Btn_First.Text = "&First"
		Me.Btn_First.UseVisualStyleBackColor = False
		'
		'Btn_Next
		'
		Me.Btn_Next.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Next.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Next.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Next.ForeColor = System.Drawing.Color.White
		Me.Btn_Next.Location = New System.Drawing.Point(1056, 566)
		Me.Btn_Next.Name = "Btn_Next"
		Me.Btn_Next.Size = New System.Drawing.Size(102, 30)
		Me.Btn_Next.TabIndex = 505
		Me.Btn_Next.Text = "&Next"
		Me.Btn_Next.UseVisualStyleBackColor = False
		'
		'Btn_Prev
		'
		Me.Btn_Prev.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Prev.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Prev.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Prev.ForeColor = System.Drawing.Color.White
		Me.Btn_Prev.Location = New System.Drawing.Point(879, 566)
		Me.Btn_Prev.Name = "Btn_Prev"
		Me.Btn_Prev.Size = New System.Drawing.Size(102, 30)
		Me.Btn_Prev.TabIndex = 506
		Me.Btn_Prev.Text = "&Prev"
		Me.Btn_Prev.UseVisualStyleBackColor = False
		'
		'Panel8
		'
		Me.Panel8.BackColor = System.Drawing.Color.Red
		Me.Panel8.Location = New System.Drawing.Point(20, 552)
		Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel8.Name = "Panel8"
		Me.Panel8.Size = New System.Drawing.Size(1153, 12)
		Me.Panel8.TabIndex = 39
		Me.Panel8.Visible = False
		'
		'Lv_Data_PR
		'
		Me.Lv_Data_PR.BackColor = System.Drawing.Color.White
		Me.Lv_Data_PR.FullRowSelect = True
		Me.Lv_Data_PR.GridLines = True
		Me.Lv_Data_PR.HideSelection = False
		Me.Lv_Data_PR.Location = New System.Drawing.Point(20, 99)
		Me.Lv_Data_PR.Name = "Lv_Data_PR"
		Me.Lv_Data_PR.Size = New System.Drawing.Size(1144, 451)
		Me.Lv_Data_PR.TabIndex = 509
		Me.Lv_Data_PR.UseCompatibleStateImageBehavior = False
		Me.Lv_Data_PR.View = System.Windows.Forms.View.Details
		'
		'N_EMI_SD_List_Purchase_Requisition_Department_Barang_Lain
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(1184, 611)
		Me.Controls.Add(Me.Lv_Data_PR)
		Me.Controls.Add(Me.Txt_Pages)
		Me.Controls.Add(Me.Btn_First)
		Me.Controls.Add(Me.Btn_Next)
		Me.Controls.Add(Me.Btn_Prev)
		Me.Controls.Add(Me.Btn_cari)
		Me.Controls.Add(Me.Txt_Value_Filter)
		Me.Controls.Add(Me.Cmb_Filter)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel7)
		Me.Controls.Add(Me.Panel8)
		Me.Controls.Add(Me.Panel6)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel1)
		Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.MaximizeBox = False
		Me.Name = "N_EMI_SD_List_Purchase_Requisition_Department_Barang_Lain"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.Panel4.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Panel1 As Panel
	Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
	Friend WithEvents Lbl_Judul As Label
	Friend WithEvents Panel2 As Panel
	Friend WithEvents Panel3 As Panel
	Friend WithEvents Panel4 As Panel
	Friend WithEvents Panel5 As Panel
	Friend WithEvents Label2 As Label
	Friend WithEvents Cmb_Filter As ComboBox
	Friend WithEvents Btn_cari As Button
	Friend WithEvents Txt_Value_Filter As TextBox
	Friend WithEvents Panel6 As Panel
	Friend WithEvents Panel7 As Panel
	Friend WithEvents Txt_Pages As TextBox
	Friend WithEvents Btn_First As Button
	Friend WithEvents Btn_Next As Button
	Friend WithEvents Btn_Prev As Button
	Friend WithEvents Panel8 As Panel
	Friend WithEvents Lv_Data_PR As ListView
End Class
