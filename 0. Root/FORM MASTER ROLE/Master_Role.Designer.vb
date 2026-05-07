<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Master_Role
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
		Me.GroupBox2 = New System.Windows.Forms.GroupBox()
		Me.Btn_Search = New System.Windows.Forms.Button()
		Me.Tb_UserName = New System.Windows.Forms.TextBox()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.Cb_Users = New System.Windows.Forms.ComboBox()
		Me.Lv_Role = New System.Windows.Forms.ListView()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.Btn_Save = New System.Windows.Forms.Button()
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.Lbl_Judul = New System.Windows.Forms.Label()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.GroupBox3 = New System.Windows.Forms.GroupBox()
		Me.Btn_GetReference = New System.Windows.Forms.Button()
		Me.Cmb_Reference = New System.Windows.Forms.ComboBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.Panel8 = New System.Windows.Forms.Panel()
		Me.Panel9 = New System.Windows.Forms.Panel()
		Me.Panel10 = New System.Windows.Forms.Panel()
		Me.Lv_RoleButton = New System.Windows.Forms.ListView()
		Me.GroupBox4 = New System.Windows.Forms.GroupBox()
		Me.Btn_SaveRoleButton = New System.Windows.Forms.Button()
		Me.Chk_All_RoleButton = New System.Windows.Forms.CheckBox()
		Me.Btn_Cari_RoleButton = New System.Windows.Forms.Button()
		Me.Txt_Filter_RoleButton = New System.Windows.Forms.TextBox()
		Me.Cmb_Filter_RoleButton = New System.Windows.Forms.ComboBox()
		Me.Panel11 = New System.Windows.Forms.Panel()
		Me.Panel12 = New System.Windows.Forms.Panel()
		Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
		Me.GroupBox2.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		Me.Panel1.SuspendLayout()
		Me.Panel3.SuspendLayout()
		Me.GroupBox3.SuspendLayout()
		Me.Panel4.SuspendLayout()
		Me.Panel9.SuspendLayout()
		Me.GroupBox4.SuspendLayout()
		Me.Panel11.SuspendLayout()
		Me.SuspendLayout()
		'
		'GroupBox2
		'
		Me.GroupBox2.Controls.Add(Me.Btn_Search)
		Me.GroupBox2.Controls.Add(Me.Tb_UserName)
		Me.GroupBox2.Controls.Add(Me.Label8)
		Me.GroupBox2.Controls.Add(Me.Cb_Users)
		Me.GroupBox2.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.GroupBox2.Location = New System.Drawing.Point(20, 58)
		Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
		Me.GroupBox2.Size = New System.Drawing.Size(471, 88)
		Me.GroupBox2.TabIndex = 2
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "Users"
		'
		'Btn_Search
		'
		Me.Btn_Search.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Search.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Search.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Search.ForeColor = System.Drawing.Color.White
		Me.Btn_Search.Location = New System.Drawing.Point(56, 51)
		Me.Btn_Search.Margin = New System.Windows.Forms.Padding(4)
		Me.Btn_Search.Name = "Btn_Search"
		Me.Btn_Search.Size = New System.Drawing.Size(119, 29)
		Me.Btn_Search.TabIndex = 1
		Me.Btn_Search.Text = "Search"
		Me.Btn_Search.UseVisualStyleBackColor = False
		'
		'Tb_UserName
		'
		Me.Tb_UserName.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Tb_UserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Tb_UserName.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Tb_UserName.ForeColor = System.Drawing.SystemColors.MenuText
		Me.Tb_UserName.Location = New System.Drawing.Point(216, 22)
		Me.Tb_UserName.Margin = New System.Windows.Forms.Padding(4)
		Me.Tb_UserName.Name = "Tb_UserName"
		Me.Tb_UserName.Size = New System.Drawing.Size(241, 20)
		Me.Tb_UserName.TabIndex = 1
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label8.Location = New System.Drawing.Point(8, 21)
		Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(40, 17)
		Me.Label8.TabIndex = 0
		Me.Label8.Text = "Users"
		'
		'Cb_Users
		'
		Me.Cb_Users.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
		Me.Cb_Users.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
		Me.Cb_Users.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cb_Users.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cb_Users.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Cb_Users.FormattingEnabled = True
		Me.Cb_Users.Location = New System.Drawing.Point(56, 19)
		Me.Cb_Users.Margin = New System.Windows.Forms.Padding(4)
		Me.Cb_Users.Name = "Cb_Users"
		Me.Cb_Users.Size = New System.Drawing.Size(152, 24)
		Me.Cb_Users.TabIndex = 0
		'
		'Lv_Role
		'
		Me.Lv_Role.CheckBoxes = True
		Me.Lv_Role.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv_Role.FullRowSelect = True
		Me.Lv_Role.GridLines = True
		Me.Lv_Role.HideSelection = False
		Me.Lv_Role.Location = New System.Drawing.Point(5, 22)
		Me.Lv_Role.Margin = New System.Windows.Forms.Padding(4)
		Me.Lv_Role.Name = "Lv_Role"
		Me.Lv_Role.OwnerDraw = True
		Me.Lv_Role.Size = New System.Drawing.Size(763, 377)
		Me.Lv_Role.TabIndex = 0
		Me.Lv_Role.UseCompatibleStateImageBehavior = False
		Me.Lv_Role.View = System.Windows.Forms.View.Details
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.Lv_Role)
		Me.GroupBox1.Controls.Add(Me.Btn_Save)
		Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.GroupBox1.Location = New System.Drawing.Point(20, 157)
		Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
		Me.GroupBox1.Size = New System.Drawing.Size(773, 439)
		Me.GroupBox1.TabIndex = 4
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Menus"
		'
		'Btn_Save
		'
		Me.Btn_Save.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Save.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Save.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Save.ForeColor = System.Drawing.Color.White
		Me.Btn_Save.Location = New System.Drawing.Point(5, 403)
		Me.Btn_Save.Margin = New System.Windows.Forms.Padding(4)
		Me.Btn_Save.Name = "Btn_Save"
		Me.Btn_Save.Size = New System.Drawing.Size(156, 29)
		Me.Btn_Save.TabIndex = 1
		Me.Btn_Save.Text = "Save"
		Me.Btn_Save.UseVisualStyleBackColor = False
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.PanelGradient1)
		Me.Panel1.Controls.Add(Me.Lbl_Judul)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.Panel1.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(1184, 45)
		Me.Panel1.TabIndex = 23
		'
		'Lbl_Judul
		'
		Me.Lbl_Judul.AutoSize = True
		Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Lbl_Judul.Location = New System.Drawing.Point(20, 7)
		Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
		Me.Lbl_Judul.Name = "Lbl_Judul"
		Me.Lbl_Judul.Size = New System.Drawing.Size(254, 30)
		Me.Lbl_Judul.TabIndex = 0
		Me.Lbl_Judul.Text = "Master Data Role Menu"
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Controls.Add(Me.Panel6)
		Me.Panel3.Location = New System.Drawing.Point(0, 55)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 613)
		Me.Panel3.TabIndex = 41
		Me.Panel3.Visible = False
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.Color.Red
		Me.Panel6.Location = New System.Drawing.Point(14, 169)
		Me.Panel6.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(1078, 15)
		Me.Panel6.TabIndex = 232
		Me.Panel6.Visible = False
		'
		'GroupBox3
		'
		Me.GroupBox3.Controls.Add(Me.Btn_GetReference)
		Me.GroupBox3.Controls.Add(Me.Cmb_Reference)
		Me.GroupBox3.Controls.Add(Me.Label1)
		Me.GroupBox3.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.GroupBox3.Location = New System.Drawing.Point(506, 58)
		Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
		Me.GroupBox3.Name = "GroupBox3"
		Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
		Me.GroupBox3.Size = New System.Drawing.Size(353, 88)
		Me.GroupBox3.TabIndex = 42
		Me.GroupBox3.TabStop = False
		Me.GroupBox3.Text = "Reference"
		'
		'Btn_GetReference
		'
		Me.Btn_GetReference.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_GetReference.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_GetReference.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_GetReference.ForeColor = System.Drawing.Color.White
		Me.Btn_GetReference.Location = New System.Drawing.Point(90, 51)
		Me.Btn_GetReference.Margin = New System.Windows.Forms.Padding(4)
		Me.Btn_GetReference.Name = "Btn_GetReference"
		Me.Btn_GetReference.Size = New System.Drawing.Size(119, 29)
		Me.Btn_GetReference.TabIndex = 1
		Me.Btn_GetReference.Text = "Get"
		Me.Btn_GetReference.UseVisualStyleBackColor = False
		'
		'Cmb_Reference
		'
		Me.Cmb_Reference.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Reference.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Reference.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Cmb_Reference.FormattingEnabled = True
		Me.Cmb_Reference.Location = New System.Drawing.Point(90, 19)
		Me.Cmb_Reference.Margin = New System.Windows.Forms.Padding(4)
		Me.Cmb_Reference.Name = "Cmb_Reference"
		Me.Cmb_Reference.Size = New System.Drawing.Size(243, 24)
		Me.Cmb_Reference.TabIndex = 1
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label1.Location = New System.Drawing.Point(14, 23)
		Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(68, 17)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Reference"
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(-5, 45)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(6)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(1502, 12)
		Me.Panel2.TabIndex = 43
		Me.Panel2.Visible = False
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Controls.Add(Me.Panel5)
		Me.Panel4.Location = New System.Drawing.Point(492, 58)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(12, 613)
		Me.Panel4.TabIndex = 41
		Me.Panel4.Visible = False
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Location = New System.Drawing.Point(14, 169)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(1078, 15)
		Me.Panel5.TabIndex = 232
		Me.Panel5.Visible = False
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.Color.Red
		Me.Panel7.Location = New System.Drawing.Point(40, 145)
		Me.Panel7.Margin = New System.Windows.Forms.Padding(6)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(1502, 12)
		Me.Panel7.TabIndex = 43
		Me.Panel7.Visible = False
		'
		'Panel8
		'
		Me.Panel8.BackColor = System.Drawing.Color.Red
		Me.Panel8.Location = New System.Drawing.Point(18, 596)
		Me.Panel8.Margin = New System.Windows.Forms.Padding(6)
		Me.Panel8.Name = "Panel8"
		Me.Panel8.Size = New System.Drawing.Size(1502, 15)
		Me.Panel8.TabIndex = 43
		Me.Panel8.Visible = False
		'
		'Panel9
		'
		Me.Panel9.BackColor = System.Drawing.Color.Red
		Me.Panel9.Controls.Add(Me.Panel10)
		Me.Panel9.Location = New System.Drawing.Point(1165, 58)
		Me.Panel9.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel9.Name = "Panel9"
		Me.Panel9.Size = New System.Drawing.Size(19, 613)
		Me.Panel9.TabIndex = 41
		Me.Panel9.Visible = False
		'
		'Panel10
		'
		Me.Panel10.BackColor = System.Drawing.Color.Red
		Me.Panel10.Location = New System.Drawing.Point(14, 169)
		Me.Panel10.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel10.Name = "Panel10"
		Me.Panel10.Size = New System.Drawing.Size(1078, 15)
		Me.Panel10.TabIndex = 232
		Me.Panel10.Visible = False
		'
		'Lv_RoleButton
		'
		Me.Lv_RoleButton.CheckBoxes = True
		Me.Lv_RoleButton.FullRowSelect = True
		Me.Lv_RoleButton.GridLines = True
		Me.Lv_RoleButton.HideSelection = False
		Me.Lv_RoleButton.Location = New System.Drawing.Point(6, 77)
		Me.Lv_RoleButton.Name = "Lv_RoleButton"
		Me.Lv_RoleButton.Size = New System.Drawing.Size(348, 322)
		Me.Lv_RoleButton.TabIndex = 44
		Me.Lv_RoleButton.UseCompatibleStateImageBehavior = False
		Me.Lv_RoleButton.View = System.Windows.Forms.View.Details
		'
		'GroupBox4
		'
		Me.GroupBox4.Controls.Add(Me.Btn_SaveRoleButton)
		Me.GroupBox4.Controls.Add(Me.Chk_All_RoleButton)
		Me.GroupBox4.Controls.Add(Me.Btn_Cari_RoleButton)
		Me.GroupBox4.Controls.Add(Me.Txt_Filter_RoleButton)
		Me.GroupBox4.Controls.Add(Me.Cmb_Filter_RoleButton)
		Me.GroupBox4.Controls.Add(Me.Lv_RoleButton)
		Me.GroupBox4.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.GroupBox4.Location = New System.Drawing.Point(806, 157)
		Me.GroupBox4.Name = "GroupBox4"
		Me.GroupBox4.Size = New System.Drawing.Size(359, 436)
		Me.GroupBox4.TabIndex = 45
		Me.GroupBox4.TabStop = False
		Me.GroupBox4.Text = "Role Button"
		'
		'Btn_SaveRoleButton
		'
		Me.Btn_SaveRoleButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_SaveRoleButton.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_SaveRoleButton.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_SaveRoleButton.ForeColor = System.Drawing.Color.White
		Me.Btn_SaveRoleButton.Location = New System.Drawing.Point(6, 403)
		Me.Btn_SaveRoleButton.Margin = New System.Windows.Forms.Padding(4)
		Me.Btn_SaveRoleButton.Name = "Btn_SaveRoleButton"
		Me.Btn_SaveRoleButton.Size = New System.Drawing.Size(156, 29)
		Me.Btn_SaveRoleButton.TabIndex = 75
		Me.Btn_SaveRoleButton.Text = "Save Role Button"
		Me.Btn_SaveRoleButton.UseVisualStyleBackColor = False
		'
		'Chk_All_RoleButton
		'
		Me.Chk_All_RoleButton.AutoSize = True
		Me.Chk_All_RoleButton.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Chk_All_RoleButton.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Chk_All_RoleButton.Location = New System.Drawing.Point(6, 53)
		Me.Chk_All_RoleButton.Name = "Chk_All_RoleButton"
		Me.Chk_All_RoleButton.Size = New System.Drawing.Size(76, 20)
		Me.Chk_All_RoleButton.TabIndex = 74
		Me.Chk_All_RoleButton.Text = "Check All"
		Me.Chk_All_RoleButton.UseVisualStyleBackColor = True
		'
		'Btn_Cari_RoleButton
		'
		Me.Btn_Cari_RoleButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Cari_RoleButton.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Cari_RoleButton.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Cari_RoleButton.ForeColor = System.Drawing.Color.White
		Me.Btn_Cari_RoleButton.Location = New System.Drawing.Point(299, 18)
		Me.Btn_Cari_RoleButton.Margin = New System.Windows.Forms.Padding(4)
		Me.Btn_Cari_RoleButton.Name = "Btn_Cari_RoleButton"
		Me.Btn_Cari_RoleButton.Size = New System.Drawing.Size(55, 26)
		Me.Btn_Cari_RoleButton.TabIndex = 47
		Me.Btn_Cari_RoleButton.Text = "Cari"
		Me.Btn_Cari_RoleButton.UseVisualStyleBackColor = False
		'
		'Txt_Filter_RoleButton
		'
		Me.Txt_Filter_RoleButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Filter_RoleButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Filter_RoleButton.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Filter_RoleButton.ForeColor = System.Drawing.SystemColors.MenuText
		Me.Txt_Filter_RoleButton.Location = New System.Drawing.Point(111, 22)
		Me.Txt_Filter_RoleButton.Margin = New System.Windows.Forms.Padding(4)
		Me.Txt_Filter_RoleButton.Name = "Txt_Filter_RoleButton"
		Me.Txt_Filter_RoleButton.Size = New System.Drawing.Size(186, 20)
		Me.Txt_Filter_RoleButton.TabIndex = 46
		'
		'Cmb_Filter_RoleButton
		'
		Me.Cmb_Filter_RoleButton.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Filter_RoleButton.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Filter_RoleButton.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Cmb_Filter_RoleButton.FormattingEnabled = True
		Me.Cmb_Filter_RoleButton.Location = New System.Drawing.Point(6, 21)
		Me.Cmb_Filter_RoleButton.Name = "Cmb_Filter_RoleButton"
		Me.Cmb_Filter_RoleButton.Size = New System.Drawing.Size(98, 24)
		Me.Cmb_Filter_RoleButton.TabIndex = 45
		'
		'Panel11
		'
		Me.Panel11.BackColor = System.Drawing.Color.Red
		Me.Panel11.Controls.Add(Me.Panel12)
		Me.Panel11.Location = New System.Drawing.Point(794, 152)
		Me.Panel11.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel11.Name = "Panel11"
		Me.Panel11.Size = New System.Drawing.Size(12, 613)
		Me.Panel11.TabIndex = 41
		Me.Panel11.Visible = False
		'
		'Panel12
		'
		Me.Panel12.BackColor = System.Drawing.Color.Red
		Me.Panel12.Location = New System.Drawing.Point(14, 169)
		Me.Panel12.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel12.Name = "Panel12"
		Me.Panel12.Size = New System.Drawing.Size(1078, 15)
		Me.Panel12.TabIndex = 232
		Me.Panel12.Visible = False
		'
		'PanelGradient1
		'
		Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
		Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
		Me.PanelGradient1.cuteTransparent1 = 100
		Me.PanelGradient1.cuteTransparent2 = 64
		Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.PanelGradient1.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.PanelGradient1.Location = New System.Drawing.Point(0, 43)
		Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
		Me.PanelGradient1.Name = "PanelGradient1"
		Me.PanelGradient1.Size = New System.Drawing.Size(1184, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Master_Role
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(1184, 611)
		Me.Controls.Add(Me.GroupBox4)
		Me.Controls.Add(Me.Panel8)
		Me.Controls.Add(Me.Panel7)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.GroupBox3)
		Me.Controls.Add(Me.Panel9)
		Me.Controls.Add(Me.Panel11)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel1)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.GroupBox2)
		Me.Controls.Add(Me.Panel4)
		Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.MaximizeBox = False
		Me.Name = "Master_Role"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.GroupBox2.ResumeLayout(False)
		Me.GroupBox2.PerformLayout()
		Me.GroupBox1.ResumeLayout(False)
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.Panel3.ResumeLayout(False)
		Me.GroupBox3.ResumeLayout(False)
		Me.GroupBox3.PerformLayout()
		Me.Panel4.ResumeLayout(False)
		Me.Panel9.ResumeLayout(False)
		Me.GroupBox4.ResumeLayout(False)
		Me.GroupBox4.PerformLayout()
		Me.Panel11.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Cb_Users As ComboBox
    Friend WithEvents Btn_Search As Button
    Friend WithEvents Tb_UserName As TextBox
    Friend WithEvents Lv_Role As ListView
	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Btn_Save As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Cmb_Reference As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Btn_GetReference As Button
	Friend WithEvents Panel2 As Panel
	Friend WithEvents Panel4 As Panel
	Friend WithEvents Panel5 As Panel
	Friend WithEvents Panel7 As Panel
	Friend WithEvents Panel8 As Panel
	Friend WithEvents Panel9 As Panel
	Friend WithEvents Panel10 As Panel
	Friend WithEvents Lv_RoleButton As ListView
	Friend WithEvents GroupBox4 As GroupBox
	Friend WithEvents Btn_Cari_RoleButton As Button
	Friend WithEvents Txt_Filter_RoleButton As TextBox
	Friend WithEvents Cmb_Filter_RoleButton As ComboBox
	Friend WithEvents Chk_All_RoleButton As CheckBox
	Friend WithEvents Panel11 As Panel
	Friend WithEvents Panel12 As Panel
	Friend WithEvents Btn_SaveRoleButton As Button
End Class
