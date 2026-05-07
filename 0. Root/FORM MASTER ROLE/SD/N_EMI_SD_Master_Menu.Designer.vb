<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_SD_Master_Menu
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
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Txt_CurrentForm = New System.Windows.Forms.TextBox()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Txt_Kategori = New System.Windows.Forms.TextBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Txt_NamaRole = New System.Windows.Forms.TextBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.Lv_Data_Role = New System.Windows.Forms.ListView()
		Me.Btn_Delete = New System.Windows.Forms.Button()
		Me.Btn_Refresh = New System.Windows.Forms.Button()
		Me.Btn_Simpan = New System.Windows.Forms.Button()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.Panel7 = New System.Windows.Forms.Panel()
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
		Me.Panel1.Margin = New System.Windows.Forms.Padding(6)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(634, 45)
		Me.Panel1.TabIndex = 27
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
		Me.PanelGradient1.Size = New System.Drawing.Size(634, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold)
		Me.Label1.Location = New System.Drawing.Point(20, 7)
		Me.Label1.Margin = New System.Windows.Forms.Padding(7, 0, 7, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(194, 30)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Show Dialog Role"
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Location = New System.Drawing.Point(0, 57)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 602)
		Me.Panel3.TabIndex = 40
		Me.Panel3.Visible = False
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(0, 45)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(1287, 12)
		Me.Panel2.TabIndex = 41
		Me.Panel2.Visible = False
		'
		'Txt_CurrentForm
		'
		Me.Txt_CurrentForm.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_CurrentForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_CurrentForm.Enabled = False
		Me.Txt_CurrentForm.Font = New System.Drawing.Font("Work Sans SemiBold", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Txt_CurrentForm.Location = New System.Drawing.Point(121, 57)
		Me.Txt_CurrentForm.Margin = New System.Windows.Forms.Padding(4)
		Me.Txt_CurrentForm.Name = "Txt_CurrentForm"
		Me.Txt_CurrentForm.Size = New System.Drawing.Size(377, 20)
		Me.Txt_CurrentForm.TabIndex = 42
		Me.Txt_CurrentForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'Label10
		'
		Me.Label10.AutoSize = True
		Me.Label10.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label10.Location = New System.Drawing.Point(20, 58)
		Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(37, 17)
		Me.Label10.TabIndex = 43
		Me.Label10.Text = "Form"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label2.Location = New System.Drawing.Point(20, 86)
		Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(56, 17)
		Me.Label2.TabIndex = 43
		Me.Label2.Text = "Kategori"
		'
		'Txt_Kategori
		'
		Me.Txt_Kategori.BackColor = System.Drawing.Color.White
		Me.Txt_Kategori.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Kategori.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Kategori.Location = New System.Drawing.Point(121, 85)
		Me.Txt_Kategori.Margin = New System.Windows.Forms.Padding(4)
		Me.Txt_Kategori.Name = "Txt_Kategori"
		Me.Txt_Kategori.Size = New System.Drawing.Size(377, 20)
		Me.Txt_Kategori.TabIndex = 0
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label3.Location = New System.Drawing.Point(20, 114)
		Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(72, 17)
		Me.Label3.TabIndex = 43
		Me.Label3.Text = "Nama Role"
		'
		'Txt_NamaRole
		'
		Me.Txt_NamaRole.BackColor = System.Drawing.Color.White
		Me.Txt_NamaRole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_NamaRole.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_NamaRole.Location = New System.Drawing.Point(121, 113)
		Me.Txt_NamaRole.Margin = New System.Windows.Forms.Padding(4)
		Me.Txt_NamaRole.Name = "Txt_NamaRole"
		Me.Txt_NamaRole.Size = New System.Drawing.Size(377, 20)
		Me.Txt_NamaRole.TabIndex = 1
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label4.Location = New System.Drawing.Point(20, 142)
		Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(73, 17)
		Me.Label4.TabIndex = 43
		Me.Label4.Text = "Keterangan"
		'
		'Txt_Keterangan
		'
		Me.Txt_Keterangan.BackColor = System.Drawing.Color.White
		Me.Txt_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Keterangan.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Keterangan.Location = New System.Drawing.Point(121, 141)
		Me.Txt_Keterangan.Margin = New System.Windows.Forms.Padding(4)
		Me.Txt_Keterangan.MaxLength = 100
		Me.Txt_Keterangan.Name = "Txt_Keterangan"
		Me.Txt_Keterangan.Size = New System.Drawing.Size(377, 20)
		Me.Txt_Keterangan.TabIndex = 2
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.Lv_Data_Role)
		Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.GroupBox1.Location = New System.Drawing.Point(20, 211)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(594, 325)
		Me.GroupBox1.TabIndex = 6
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Data Role"
		'
		'Lv_Data_Role
		'
		Me.Lv_Data_Role.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv_Data_Role.FullRowSelect = True
		Me.Lv_Data_Role.GridLines = True
		Me.Lv_Data_Role.HideSelection = False
		Me.Lv_Data_Role.Location = New System.Drawing.Point(7, 21)
		Me.Lv_Data_Role.Name = "Lv_Data_Role"
		Me.Lv_Data_Role.Size = New System.Drawing.Size(580, 296)
		Me.Lv_Data_Role.TabIndex = 0
		Me.Lv_Data_Role.UseCompatibleStateImageBehavior = False
		Me.Lv_Data_Role.View = System.Windows.Forms.View.Details
		'
		'Btn_Delete
		'
		Me.Btn_Delete.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Delete.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Delete.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Delete.ForeColor = System.Drawing.Color.White
		Me.Btn_Delete.Location = New System.Drawing.Point(375, 173)
		Me.Btn_Delete.Name = "Btn_Delete"
		Me.Btn_Delete.Size = New System.Drawing.Size(123, 28)
		Me.Btn_Delete.TabIndex = 5
		Me.Btn_Delete.Text = "&Delete"
		Me.Btn_Delete.UseVisualStyleBackColor = False
		'
		'Btn_Refresh
		'
		Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Refresh.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
		Me.Btn_Refresh.Location = New System.Drawing.Point(248, 173)
		Me.Btn_Refresh.Name = "Btn_Refresh"
		Me.Btn_Refresh.Size = New System.Drawing.Size(123, 28)
		Me.Btn_Refresh.TabIndex = 4
		Me.Btn_Refresh.Text = "&Refresh"
		Me.Btn_Refresh.UseVisualStyleBackColor = False
		'
		'Btn_Simpan
		'
		Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Simpan.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
		Me.Btn_Simpan.Location = New System.Drawing.Point(121, 173)
		Me.Btn_Simpan.Name = "Btn_Simpan"
		Me.Btn_Simpan.Size = New System.Drawing.Size(123, 28)
		Me.Btn_Simpan.TabIndex = 3
		Me.Btn_Simpan.Text = "&Simpan"
		Me.Btn_Simpan.UseVisualStyleBackColor = False
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Location = New System.Drawing.Point(11, 201)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(1287, 12)
		Me.Panel4.TabIndex = 41
		Me.Panel4.Visible = False
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Location = New System.Drawing.Point(15, 161)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(1287, 12)
		Me.Panel5.TabIndex = 41
		Me.Panel5.Visible = False
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.Color.Red
		Me.Panel6.Location = New System.Drawing.Point(18, 536)
		Me.Panel6.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(1287, 15)
		Me.Panel6.TabIndex = 41
		Me.Panel6.Visible = False
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.Color.Red
		Me.Panel7.Location = New System.Drawing.Point(615, 58)
		Me.Panel7.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(19, 602)
		Me.Panel7.TabIndex = 40
		Me.Panel7.Visible = False
		'
		'N_EMI_SD_Master_Menu
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(634, 551)
		Me.Controls.Add(Me.Btn_Delete)
		Me.Controls.Add(Me.Btn_Refresh)
		Me.Controls.Add(Me.Btn_Simpan)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.Txt_Keterangan)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.Txt_NamaRole)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Txt_Kategori)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Txt_CurrentForm)
		Me.Controls.Add(Me.Label10)
		Me.Controls.Add(Me.Panel5)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Panel6)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel7)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel1)
		Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.Name = "N_EMI_SD_Master_Menu"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.GroupBox1.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Panel1 As Panel
	Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
	Friend WithEvents Label1 As Label
	Friend WithEvents Panel3 As Panel
	Friend WithEvents Panel2 As Panel
	Friend WithEvents Txt_CurrentForm As TextBox
	Friend WithEvents Label10 As Label
	Friend WithEvents Label2 As Label
	Friend WithEvents Txt_Kategori As TextBox
	Friend WithEvents Label3 As Label
	Friend WithEvents Txt_NamaRole As TextBox
	Friend WithEvents Label4 As Label
	Friend WithEvents Txt_Keterangan As TextBox
	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents Btn_Delete As Button
	Friend WithEvents Btn_Refresh As Button
	Friend WithEvents Btn_Simpan As Button
	Friend WithEvents Panel4 As Panel
	Friend WithEvents Panel5 As Panel
	Friend WithEvents Panel6 As Panel
	Friend WithEvents Lv_Data_Role As ListView
	Friend WithEvents Panel7 As Panel
End Class
