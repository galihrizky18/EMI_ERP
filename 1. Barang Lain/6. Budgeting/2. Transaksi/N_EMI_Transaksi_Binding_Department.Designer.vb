<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Transaksi_Binding_Department
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
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Btn_Simpan = New System.Windows.Forms.Button()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Txt_KodeBinding = New System.Windows.Forms.TextBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
		Me.Lv_Department = New System.Windows.Forms.ListView()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.Panel8 = New System.Windows.Forms.Panel()
		Me.Panel9 = New System.Windows.Forms.Panel()
		Me.Panel10 = New System.Windows.Forms.Panel()
		Me.Panel11 = New System.Windows.Forms.Panel()
		Me.Btn_Refresh = New System.Windows.Forms.Button()
		Me.Btn_Delete = New System.Windows.Forms.Button()
		Me.Btn_Get_Data = New System.Windows.Forms.Button()
		Me.Panel12 = New System.Windows.Forms.Panel()
		Me.Panel13 = New System.Windows.Forms.Panel()
		Me.TabControl1 = New System.Windows.Forms.TabControl()
		Me.TabPage1 = New System.Windows.Forms.TabPage()
		Me.TabPage2 = New System.Windows.Forms.TabPage()
		Me.Lv_Display_Detail = New System.Windows.Forms.ListView()
		Me.Btn_Cari = New System.Windows.Forms.Button()
		Me.Txt_filter = New System.Windows.Forms.TextBox()
		Me.Cmb_Filter = New System.Windows.Forms.ComboBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Lv_Display_Parent = New System.Windows.Forms.ListView()
		Me.Panel14 = New System.Windows.Forms.Panel()
		Me.Panel1.SuspendLayout()
		Me.Panel5.SuspendLayout()
		Me.Panel4.SuspendLayout()
		Me.Panel8.SuspendLayout()
		Me.Panel10.SuspendLayout()
		Me.Panel12.SuspendLayout()
		Me.TabControl1.SuspendLayout()
		Me.TabPage1.SuspendLayout()
		Me.TabPage2.SuspendLayout()
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
		Me.Panel1.Size = New System.Drawing.Size(934, 45)
		Me.Panel1.TabIndex = 31
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
		Me.PanelGradient1.Size = New System.Drawing.Size(934, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Lbl_Judul
		'
		Me.Lbl_Judul.AutoSize = True
		Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Lbl_Judul.Location = New System.Drawing.Point(20, 7)
		Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.Lbl_Judul.Name = "Lbl_Judul"
		Me.Lbl_Judul.Size = New System.Drawing.Size(220, 30)
		Me.Lbl_Judul.TabIndex = 0
		Me.Lbl_Judul.Text = "Binding Department"
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Controls.Add(Me.Panel6)
		Me.Panel5.Location = New System.Drawing.Point(-4, 45)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(1348, 12)
		Me.Panel5.TabIndex = 503
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
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Location = New System.Drawing.Point(0, 60)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 637)
		Me.Panel3.TabIndex = 504
		Me.Panel3.Visible = False
		'
		'Btn_Simpan
		'
		Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Simpan.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
		Me.Btn_Simpan.Location = New System.Drawing.Point(21, 514)
		Me.Btn_Simpan.Margin = New System.Windows.Forms.Padding(2)
		Me.Btn_Simpan.Name = "Btn_Simpan"
		Me.Btn_Simpan.Size = New System.Drawing.Size(114, 31)
		Me.Btn_Simpan.TabIndex = 4
		Me.Btn_Simpan.Text = "&Simpan"
		Me.Btn_Simpan.UseVisualStyleBackColor = False
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label2.Location = New System.Drawing.Point(20, 61)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(83, 17)
		Me.Label2.TabIndex = 506
		Me.Label2.Text = "Kode Binding"
		'
		'Txt_KodeBinding
		'
		Me.Txt_KodeBinding.BackColor = System.Drawing.Color.White
		Me.Txt_KodeBinding.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_KodeBinding.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_KodeBinding.Location = New System.Drawing.Point(140, 58)
		Me.Txt_KodeBinding.MaxLength = 255
		Me.Txt_KodeBinding.Name = "Txt_KodeBinding"
		Me.Txt_KodeBinding.Size = New System.Drawing.Size(395, 20)
		Me.Txt_KodeBinding.TabIndex = 0
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label3.Location = New System.Drawing.Point(20, 87)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(73, 17)
		Me.Label3.TabIndex = 506
		Me.Label3.Text = "Keterangan"
		'
		'Txt_Keterangan
		'
		Me.Txt_Keterangan.BackColor = System.Drawing.Color.White
		Me.Txt_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Keterangan.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Keterangan.Location = New System.Drawing.Point(140, 84)
		Me.Txt_Keterangan.MaxLength = 255
		Me.Txt_Keterangan.Name = "Txt_Keterangan"
		Me.Txt_Keterangan.Size = New System.Drawing.Size(395, 20)
		Me.Txt_Keterangan.TabIndex = 1
		'
		'Lv_Department
		'
		Me.Lv_Department.BackColor = System.Drawing.Color.White
		Me.Lv_Department.CheckBoxes = True
		Me.Lv_Department.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Lv_Department.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv_Department.FullRowSelect = True
		Me.Lv_Department.GridLines = True
		Me.Lv_Department.HideSelection = False
		Me.Lv_Department.Location = New System.Drawing.Point(3, 3)
		Me.Lv_Department.Name = "Lv_Department"
		Me.Lv_Department.Size = New System.Drawing.Size(877, 301)
		Me.Lv_Department.TabIndex = 0
		Me.Lv_Department.UseCompatibleStateImageBehavior = False
		Me.Lv_Department.View = System.Windows.Forms.View.Details
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(915, 65)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(19, 637)
		Me.Panel2.TabIndex = 504
		Me.Panel2.Visible = False
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Controls.Add(Me.Panel7)
		Me.Panel4.Location = New System.Drawing.Point(16, 546)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(1348, 15)
		Me.Panel4.TabIndex = 503
		Me.Panel4.Visible = False
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.Color.Red
		Me.Panel7.Location = New System.Drawing.Point(1167, 8)
		Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(19, 637)
		Me.Panel7.TabIndex = 495
		Me.Panel7.Visible = False
		'
		'Panel8
		'
		Me.Panel8.BackColor = System.Drawing.Color.Red
		Me.Panel8.Controls.Add(Me.Panel9)
		Me.Panel8.Location = New System.Drawing.Point(22, 105)
		Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel8.Name = "Panel8"
		Me.Panel8.Size = New System.Drawing.Size(1348, 12)
		Me.Panel8.TabIndex = 503
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
		Me.Panel10.Location = New System.Drawing.Point(27, 501)
		Me.Panel10.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel10.Name = "Panel10"
		Me.Panel10.Size = New System.Drawing.Size(1348, 12)
		Me.Panel10.TabIndex = 503
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
		Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Refresh.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
		Me.Btn_Refresh.Location = New System.Drawing.Point(138, 514)
		Me.Btn_Refresh.Margin = New System.Windows.Forms.Padding(2)
		Me.Btn_Refresh.Name = "Btn_Refresh"
		Me.Btn_Refresh.Size = New System.Drawing.Size(114, 31)
		Me.Btn_Refresh.TabIndex = 5
		Me.Btn_Refresh.Text = "&Refresh"
		Me.Btn_Refresh.UseVisualStyleBackColor = False
		'
		'Btn_Delete
		'
		Me.Btn_Delete.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Delete.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Delete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Delete.ForeColor = System.Drawing.Color.White
		Me.Btn_Delete.Location = New System.Drawing.Point(256, 514)
		Me.Btn_Delete.Margin = New System.Windows.Forms.Padding(2)
		Me.Btn_Delete.Name = "Btn_Delete"
		Me.Btn_Delete.Size = New System.Drawing.Size(114, 31)
		Me.Btn_Delete.TabIndex = 6
		Me.Btn_Delete.Text = "&Delete"
		Me.Btn_Delete.UseVisualStyleBackColor = False
		'
		'Btn_Get_Data
		'
		Me.Btn_Get_Data.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Get_Data.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Get_Data.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Get_Data.ForeColor = System.Drawing.Color.White
		Me.Btn_Get_Data.Location = New System.Drawing.Point(137, 116)
		Me.Btn_Get_Data.Margin = New System.Windows.Forms.Padding(2)
		Me.Btn_Get_Data.Name = "Btn_Get_Data"
		Me.Btn_Get_Data.Size = New System.Drawing.Size(114, 31)
		Me.Btn_Get_Data.TabIndex = 2
		Me.Btn_Get_Data.Text = "&Get Data"
		Me.Btn_Get_Data.UseVisualStyleBackColor = False
		'
		'Panel12
		'
		Me.Panel12.BackColor = System.Drawing.Color.Red
		Me.Panel12.Controls.Add(Me.Panel13)
		Me.Panel12.Location = New System.Drawing.Point(20, 149)
		Me.Panel12.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel12.Name = "Panel12"
		Me.Panel12.Size = New System.Drawing.Size(1348, 12)
		Me.Panel12.TabIndex = 503
		Me.Panel12.Visible = False
		'
		'Panel13
		'
		Me.Panel13.BackColor = System.Drawing.Color.Red
		Me.Panel13.Location = New System.Drawing.Point(1167, 8)
		Me.Panel13.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel13.Name = "Panel13"
		Me.Panel13.Size = New System.Drawing.Size(19, 637)
		Me.Panel13.TabIndex = 495
		Me.Panel13.Visible = False
		'
		'TabControl1
		'
		Me.TabControl1.Controls.Add(Me.TabPage1)
		Me.TabControl1.Controls.Add(Me.TabPage2)
		Me.TabControl1.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.TabControl1.Location = New System.Drawing.Point(22, 161)
		Me.TabControl1.Name = "TabControl1"
		Me.TabControl1.SelectedIndex = 0
		Me.TabControl1.Size = New System.Drawing.Size(891, 337)
		Me.TabControl1.TabIndex = 3
		'
		'TabPage1
		'
		Me.TabPage1.BackColor = System.Drawing.Color.White
		Me.TabPage1.Controls.Add(Me.Lv_Department)
		Me.TabPage1.Location = New System.Drawing.Point(4, 26)
		Me.TabPage1.Name = "TabPage1"
		Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage1.Size = New System.Drawing.Size(883, 307)
		Me.TabPage1.TabIndex = 0
		Me.TabPage1.Text = "Department"
		'
		'TabPage2
		'
		Me.TabPage2.BackColor = System.Drawing.Color.White
		Me.TabPage2.Controls.Add(Me.Lv_Display_Detail)
		Me.TabPage2.Controls.Add(Me.Btn_Cari)
		Me.TabPage2.Controls.Add(Me.Txt_filter)
		Me.TabPage2.Controls.Add(Me.Cmb_Filter)
		Me.TabPage2.Controls.Add(Me.Label1)
		Me.TabPage2.Controls.Add(Me.Lv_Display_Parent)
		Me.TabPage2.Controls.Add(Me.Panel14)
		Me.TabPage2.Location = New System.Drawing.Point(4, 26)
		Me.TabPage2.Name = "TabPage2"
		Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage2.Size = New System.Drawing.Size(883, 307)
		Me.TabPage2.TabIndex = 1
		Me.TabPage2.Text = "Display"
		'
		'Lv_Display_Detail
		'
		Me.Lv_Display_Detail.BackColor = System.Drawing.Color.White
		Me.Lv_Display_Detail.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv_Display_Detail.FullRowSelect = True
		Me.Lv_Display_Detail.GridLines = True
		Me.Lv_Display_Detail.HideSelection = False
		Me.Lv_Display_Detail.Location = New System.Drawing.Point(525, 35)
		Me.Lv_Display_Detail.Name = "Lv_Display_Detail"
		Me.Lv_Display_Detail.Size = New System.Drawing.Size(352, 269)
		Me.Lv_Display_Detail.TabIndex = 511
		Me.Lv_Display_Detail.UseCompatibleStateImageBehavior = False
		Me.Lv_Display_Detail.View = System.Windows.Forms.View.Details
		'
		'Btn_Cari
		'
		Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Cari.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Cari.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Cari.ForeColor = System.Drawing.Color.White
		Me.Btn_Cari.Location = New System.Drawing.Point(535, 4)
		Me.Btn_Cari.Margin = New System.Windows.Forms.Padding(2)
		Me.Btn_Cari.Name = "Btn_Cari"
		Me.Btn_Cari.Size = New System.Drawing.Size(72, 26)
		Me.Btn_Cari.TabIndex = 510
		Me.Btn_Cari.Text = "&Cari"
		Me.Btn_Cari.UseVisualStyleBackColor = False
		'
		'Txt_filter
		'
		Me.Txt_filter.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_filter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_filter.Enabled = False
		Me.Txt_filter.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_filter.Location = New System.Drawing.Point(207, 7)
		Me.Txt_filter.MaxLength = 255
		Me.Txt_filter.Name = "Txt_filter"
		Me.Txt_filter.Size = New System.Drawing.Size(325, 20)
		Me.Txt_filter.TabIndex = 509
		'
		'Cmb_Filter
		'
		Me.Cmb_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Filter.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Cmb_Filter.FormattingEnabled = True
		Me.Cmb_Filter.Location = New System.Drawing.Point(53, 5)
		Me.Cmb_Filter.Name = "Cmb_Filter"
		Me.Cmb_Filter.Size = New System.Drawing.Size(150, 24)
		Me.Cmb_Filter.TabIndex = 508
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label1.Location = New System.Drawing.Point(8, 7)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(39, 17)
		Me.Label1.TabIndex = 507
		Me.Label1.Text = "Filter"
		'
		'Lv_Display_Parent
		'
		Me.Lv_Display_Parent.BackColor = System.Drawing.Color.White
		Me.Lv_Display_Parent.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv_Display_Parent.FullRowSelect = True
		Me.Lv_Display_Parent.GridLines = True
		Me.Lv_Display_Parent.HideSelection = False
		Me.Lv_Display_Parent.Location = New System.Drawing.Point(3, 35)
		Me.Lv_Display_Parent.Name = "Lv_Display_Parent"
		Me.Lv_Display_Parent.Size = New System.Drawing.Size(510, 269)
		Me.Lv_Display_Parent.TabIndex = 1
		Me.Lv_Display_Parent.UseCompatibleStateImageBehavior = False
		Me.Lv_Display_Parent.View = System.Windows.Forms.View.Details
		'
		'Panel14
		'
		Me.Panel14.BackColor = System.Drawing.Color.Red
		Me.Panel14.Location = New System.Drawing.Point(513, 34)
		Me.Panel14.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel14.Name = "Panel14"
		Me.Panel14.Size = New System.Drawing.Size(12, 637)
		Me.Panel14.TabIndex = 504
		Me.Panel14.Visible = False
		'
		'N_EMI_Transaksi_Binding_Department
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(934, 561)
		Me.Controls.Add(Me.TabControl1)
		Me.Controls.Add(Me.Btn_Get_Data)
		Me.Controls.Add(Me.Txt_Keterangan)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Txt_KodeBinding)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Btn_Delete)
		Me.Controls.Add(Me.Btn_Refresh)
		Me.Controls.Add(Me.Btn_Simpan)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Panel10)
		Me.Controls.Add(Me.Panel12)
		Me.Controls.Add(Me.Panel8)
		Me.Controls.Add(Me.Panel5)
		Me.Controls.Add(Me.Panel1)
		Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.MaximizeBox = False
		Me.Name = "N_EMI_Transaksi_Binding_Department"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.Panel5.ResumeLayout(False)
		Me.Panel4.ResumeLayout(False)
		Me.Panel8.ResumeLayout(False)
		Me.Panel10.ResumeLayout(False)
		Me.Panel12.ResumeLayout(False)
		Me.TabControl1.ResumeLayout(False)
		Me.TabPage1.ResumeLayout(False)
		Me.TabPage2.ResumeLayout(False)
		Me.TabPage2.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Panel1 As Panel
	Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
	Friend WithEvents Lbl_Judul As Label
	Friend WithEvents Panel5 As Panel
	Friend WithEvents Panel6 As Panel
	Friend WithEvents Panel3 As Panel
	Friend WithEvents Btn_Simpan As Button
	Friend WithEvents Label2 As Label
	Friend WithEvents Txt_KodeBinding As TextBox
	Friend WithEvents Label3 As Label
	Friend WithEvents Txt_Keterangan As TextBox
	Friend WithEvents Panel2 As Panel
	Friend WithEvents Panel4 As Panel
	Friend WithEvents Panel7 As Panel
	Friend WithEvents Panel8 As Panel
	Friend WithEvents Panel9 As Panel
	Friend WithEvents Panel10 As Panel
	Friend WithEvents Panel11 As Panel
	Friend WithEvents Btn_Refresh As Button
	Friend WithEvents Btn_Delete As Button
	Friend WithEvents Lv_Department As ListView
	Friend WithEvents Btn_Get_Data As Button
	Friend WithEvents Panel12 As Panel
	Friend WithEvents Panel13 As Panel
	Friend WithEvents TabControl1 As TabControl
	Friend WithEvents TabPage1 As TabPage
	Friend WithEvents TabPage2 As TabPage
	Friend WithEvents Lv_Display_Parent As ListView
	Friend WithEvents Btn_Cari As Button
	Friend WithEvents Txt_filter As TextBox
	Friend WithEvents Cmb_Filter As ComboBox
	Friend WithEvents Label1 As Label
	Friend WithEvents Lv_Display_Detail As ListView
	Friend WithEvents Panel14 As Panel
End Class
