<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Master_Menu_x
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
		Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.GroupBox2 = New System.Windows.Forms.GroupBox()
		Me.Tb_MenuForm = New System.Windows.Forms.TextBox()
		Me.Tb_Var3 = New System.Windows.Forms.TextBox()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.Tb_ImagePath = New System.Windows.Forms.TextBox()
		Me.Tb_MenuOrder = New System.Windows.Forms.TextBox()
		Me.Tb_MenuName = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label16 = New System.Windows.Forms.Label()
		Me.Tb_Var2 = New System.Windows.Forms.TextBox()
		Me.Label13 = New System.Windows.Forms.Label()
		Me.Label9 = New System.Windows.Forms.Label()
		Me.Tb_IsiVariabel3 = New System.Windows.Forms.TextBox()
		Me.Label11 = New System.Windows.Forms.Label()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.Tb_IsiVariabel2 = New System.Windows.Forms.TextBox()
		Me.Label12 = New System.Windows.Forms.Label()
		Me.Label14 = New System.Windows.Forms.Label()
		Me.Tb_IsiVariabel1 = New System.Windows.Forms.TextBox()
		Me.Tb_Var1 = New System.Windows.Forms.TextBox()
		Me.Label15 = New System.Windows.Forms.Label()
		Me.Btn_Delete = New System.Windows.Forms.Button()
		Me.Cb_SubMenuLv3 = New System.Windows.Forms.ComboBox()
		Me.Btn_Refresh = New System.Windows.Forms.Button()
		Me.Btn_Simpan = New System.Windows.Forms.Button()
		Me.Cb_SubMenu = New System.Windows.Forms.ComboBox()
		Me.Cb_SubMenuLv2 = New System.Windows.Forms.ComboBox()
		Me.Cb_MainMenu = New System.Windows.Forms.ComboBox()
		Me.Cb_Menu = New System.Windows.Forms.ComboBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Cb_SubMenuLv1 = New System.Windows.Forms.ComboBox()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
		Me.Lbl_Judul = New System.Windows.Forms.Label()
		Me.Lv_hierarki = New System.Windows.Forms.ListView()
		Me.TabControl1 = New System.Windows.Forms.TabControl()
		Me.view = New System.Windows.Forms.TabPage()
		Me.Btn_Cari = New System.Windows.Forms.Button()
		Me.Txt_Filter = New System.Windows.Forms.TextBox()
		Me.Cmb_Filter = New System.Windows.Forms.ComboBox()
		Me.Label17 = New System.Windows.Forms.Label()
		Me.order = New System.Windows.Forms.TabPage()
		Me.Btn_Simpan_Order = New System.Windows.Forms.Button()
		Me.Dgv_Order = New System.Windows.Forms.DataGridView()
		Me.Menu_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.mainmenu = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.current_order = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.new_order = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Txt_SelectedMenu = New System.Windows.Forms.TextBox()
		Me.GroupBox1.SuspendLayout()
		Me.GroupBox2.SuspendLayout()
		Me.Panel1.SuspendLayout()
		Me.TabControl1.SuspendLayout()
		Me.view.SuspendLayout()
		Me.order.SuspendLayout()
		CType(Me.Dgv_Order, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.GroupBox2)
		Me.GroupBox1.Controls.Add(Me.Btn_Delete)
		Me.GroupBox1.Controls.Add(Me.Cb_SubMenuLv3)
		Me.GroupBox1.Controls.Add(Me.Btn_Refresh)
		Me.GroupBox1.Controls.Add(Me.Btn_Simpan)
		Me.GroupBox1.Controls.Add(Me.Cb_SubMenu)
		Me.GroupBox1.Controls.Add(Me.Cb_SubMenuLv2)
		Me.GroupBox1.Controls.Add(Me.Cb_MainMenu)
		Me.GroupBox1.Controls.Add(Me.Cb_Menu)
		Me.GroupBox1.Controls.Add(Me.Label2)
		Me.GroupBox1.Controls.Add(Me.Cb_SubMenuLv1)
		Me.GroupBox1.Controls.Add(Me.Label7)
		Me.GroupBox1.Controls.Add(Me.Label6)
		Me.GroupBox1.Controls.Add(Me.Label5)
		Me.GroupBox1.Controls.Add(Me.Label4)
		Me.GroupBox1.Controls.Add(Me.Label3)
		Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.GroupBox1.Location = New System.Drawing.Point(14, 64)
		Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
		Me.GroupBox1.Size = New System.Drawing.Size(727, 431)
		Me.GroupBox1.TabIndex = 1
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Menus"
		'
		'GroupBox2
		'
		Me.GroupBox2.Controls.Add(Me.Tb_MenuForm)
		Me.GroupBox2.Controls.Add(Me.Tb_Var3)
		Me.GroupBox2.Controls.Add(Me.Label8)
		Me.GroupBox2.Controls.Add(Me.Tb_ImagePath)
		Me.GroupBox2.Controls.Add(Me.Tb_MenuOrder)
		Me.GroupBox2.Controls.Add(Me.Tb_MenuName)
		Me.GroupBox2.Controls.Add(Me.Label1)
		Me.GroupBox2.Controls.Add(Me.Label16)
		Me.GroupBox2.Controls.Add(Me.Tb_Var2)
		Me.GroupBox2.Controls.Add(Me.Label13)
		Me.GroupBox2.Controls.Add(Me.Label9)
		Me.GroupBox2.Controls.Add(Me.Tb_IsiVariabel3)
		Me.GroupBox2.Controls.Add(Me.Label11)
		Me.GroupBox2.Controls.Add(Me.Label10)
		Me.GroupBox2.Controls.Add(Me.Tb_IsiVariabel2)
		Me.GroupBox2.Controls.Add(Me.Label12)
		Me.GroupBox2.Controls.Add(Me.Label14)
		Me.GroupBox2.Controls.Add(Me.Tb_IsiVariabel1)
		Me.GroupBox2.Controls.Add(Me.Tb_Var1)
		Me.GroupBox2.Controls.Add(Me.Label15)
		Me.GroupBox2.Location = New System.Drawing.Point(13, 130)
		Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
		Me.GroupBox2.Size = New System.Drawing.Size(705, 249)
		Me.GroupBox2.TabIndex = 5
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "Detail Menu"
		'
		'Tb_MenuForm
		'
		Me.Tb_MenuForm.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Tb_MenuForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Tb_MenuForm.Enabled = False
		Me.Tb_MenuForm.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Tb_MenuForm.Location = New System.Drawing.Point(119, 119)
		Me.Tb_MenuForm.Margin = New System.Windows.Forms.Padding(4)
		Me.Tb_MenuForm.Name = "Tb_MenuForm"
		Me.Tb_MenuForm.Size = New System.Drawing.Size(204, 23)
		Me.Tb_MenuForm.TabIndex = 3
		'
		'Tb_Var3
		'
		Me.Tb_Var3.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Tb_Var3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Tb_Var3.Enabled = False
		Me.Tb_Var3.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Tb_Var3.Location = New System.Drawing.Point(119, 212)
		Me.Tb_Var3.Margin = New System.Windows.Forms.Padding(4)
		Me.Tb_Var3.Name = "Tb_Var3"
		Me.Tb_Var3.Size = New System.Drawing.Size(204, 23)
		Me.Tb_Var3.TabIndex = 8
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label8.Location = New System.Drawing.Point(14, 121)
		Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(40, 18)
		Me.Label8.TabIndex = 1
		Me.Label8.Text = "Form"
		'
		'Tb_ImagePath
		'
		Me.Tb_ImagePath.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Tb_ImagePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Tb_ImagePath.Enabled = False
		Me.Tb_ImagePath.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Tb_ImagePath.Location = New System.Drawing.Point(119, 26)
		Me.Tb_ImagePath.Margin = New System.Windows.Forms.Padding(4)
		Me.Tb_ImagePath.Name = "Tb_ImagePath"
		Me.Tb_ImagePath.Size = New System.Drawing.Size(204, 23)
		Me.Tb_ImagePath.TabIndex = 0
		'
		'Tb_MenuOrder
		'
		Me.Tb_MenuOrder.BackColor = System.Drawing.Color.White
		Me.Tb_MenuOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Tb_MenuOrder.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Tb_MenuOrder.Location = New System.Drawing.Point(118, 88)
		Me.Tb_MenuOrder.Margin = New System.Windows.Forms.Padding(4)
		Me.Tb_MenuOrder.Name = "Tb_MenuOrder"
		Me.Tb_MenuOrder.Size = New System.Drawing.Size(92, 23)
		Me.Tb_MenuOrder.TabIndex = 2
		'
		'Tb_MenuName
		'
		Me.Tb_MenuName.BackColor = System.Drawing.Color.White
		Me.Tb_MenuName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Tb_MenuName.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Tb_MenuName.Location = New System.Drawing.Point(119, 57)
		Me.Tb_MenuName.Margin = New System.Windows.Forms.Padding(4)
		Me.Tb_MenuName.Name = "Tb_MenuName"
		Me.Tb_MenuName.Size = New System.Drawing.Size(204, 23)
		Me.Tb_MenuName.TabIndex = 1
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(14, 28)
		Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(78, 18)
		Me.Label1.TabIndex = 1
		Me.Label1.Text = "Image Path"
		'
		'Label16
		'
		Me.Label16.AutoSize = True
		Me.Label16.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label16.Location = New System.Drawing.Point(15, 90)
		Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label16.Name = "Label16"
		Me.Label16.Size = New System.Drawing.Size(83, 18)
		Me.Label16.TabIndex = 1
		Me.Label16.Text = "Menu Order"
		'
		'Tb_Var2
		'
		Me.Tb_Var2.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Tb_Var2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Tb_Var2.Enabled = False
		Me.Tb_Var2.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Tb_Var2.Location = New System.Drawing.Point(119, 181)
		Me.Tb_Var2.Margin = New System.Windows.Forms.Padding(4)
		Me.Tb_Var2.Name = "Tb_Var2"
		Me.Tb_Var2.Size = New System.Drawing.Size(204, 23)
		Me.Tb_Var2.TabIndex = 6
		'
		'Label13
		'
		Me.Label13.AutoSize = True
		Me.Label13.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label13.Location = New System.Drawing.Point(14, 59)
		Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label13.Name = "Label13"
		Me.Label13.Size = New System.Drawing.Size(84, 18)
		Me.Label13.TabIndex = 1
		Me.Label13.Text = "Menu Name"
		'
		'Label9
		'
		Me.Label9.AutoSize = True
		Me.Label9.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label9.Location = New System.Drawing.Point(14, 151)
		Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label9.Name = "Label9"
		Me.Label9.Size = New System.Drawing.Size(66, 18)
		Me.Label9.TabIndex = 1
		Me.Label9.Text = "Variabel 1"
		'
		'Tb_IsiVariabel3
		'
		Me.Tb_IsiVariabel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Tb_IsiVariabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Tb_IsiVariabel3.Enabled = False
		Me.Tb_IsiVariabel3.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Tb_IsiVariabel3.Location = New System.Drawing.Point(481, 211)
		Me.Tb_IsiVariabel3.Margin = New System.Windows.Forms.Padding(4)
		Me.Tb_IsiVariabel3.Name = "Tb_IsiVariabel3"
		Me.Tb_IsiVariabel3.Size = New System.Drawing.Size(204, 23)
		Me.Tb_IsiVariabel3.TabIndex = 9
		'
		'Label11
		'
		Me.Label11.AutoSize = True
		Me.Label11.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label11.Location = New System.Drawing.Point(354, 153)
		Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label11.Name = "Label11"
		Me.Label11.Size = New System.Drawing.Size(84, 18)
		Me.Label11.TabIndex = 1
		Me.Label11.Text = "Isi Variabel 1"
		'
		'Label10
		'
		Me.Label10.AutoSize = True
		Me.Label10.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label10.Location = New System.Drawing.Point(14, 182)
		Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(69, 18)
		Me.Label10.TabIndex = 1
		Me.Label10.Text = "Variabel 2"
		'
		'Tb_IsiVariabel2
		'
		Me.Tb_IsiVariabel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Tb_IsiVariabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Tb_IsiVariabel2.Enabled = False
		Me.Tb_IsiVariabel2.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Tb_IsiVariabel2.Location = New System.Drawing.Point(481, 180)
		Me.Tb_IsiVariabel2.Margin = New System.Windows.Forms.Padding(4)
		Me.Tb_IsiVariabel2.Name = "Tb_IsiVariabel2"
		Me.Tb_IsiVariabel2.Size = New System.Drawing.Size(204, 23)
		Me.Tb_IsiVariabel2.TabIndex = 7
		'
		'Label12
		'
		Me.Label12.AutoSize = True
		Me.Label12.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label12.Location = New System.Drawing.Point(354, 184)
		Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label12.Name = "Label12"
		Me.Label12.Size = New System.Drawing.Size(87, 18)
		Me.Label12.TabIndex = 1
		Me.Label12.Text = "Isi Variabel 2"
		'
		'Label14
		'
		Me.Label14.AutoSize = True
		Me.Label14.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label14.Location = New System.Drawing.Point(14, 213)
		Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label14.Name = "Label14"
		Me.Label14.Size = New System.Drawing.Size(69, 18)
		Me.Label14.TabIndex = 1
		Me.Label14.Text = "Variabel 3"
		'
		'Tb_IsiVariabel1
		'
		Me.Tb_IsiVariabel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Tb_IsiVariabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Tb_IsiVariabel1.Enabled = False
		Me.Tb_IsiVariabel1.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Tb_IsiVariabel1.Location = New System.Drawing.Point(481, 150)
		Me.Tb_IsiVariabel1.Margin = New System.Windows.Forms.Padding(4)
		Me.Tb_IsiVariabel1.Name = "Tb_IsiVariabel1"
		Me.Tb_IsiVariabel1.Size = New System.Drawing.Size(204, 23)
		Me.Tb_IsiVariabel1.TabIndex = 5
		'
		'Tb_Var1
		'
		Me.Tb_Var1.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Tb_Var1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Tb_Var1.Enabled = False
		Me.Tb_Var1.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Tb_Var1.Location = New System.Drawing.Point(119, 150)
		Me.Tb_Var1.Margin = New System.Windows.Forms.Padding(4)
		Me.Tb_Var1.Name = "Tb_Var1"
		Me.Tb_Var1.Size = New System.Drawing.Size(204, 23)
		Me.Tb_Var1.TabIndex = 4
		'
		'Label15
		'
		Me.Label15.AutoSize = True
		Me.Label15.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label15.Location = New System.Drawing.Point(354, 215)
		Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label15.Name = "Label15"
		Me.Label15.Size = New System.Drawing.Size(87, 18)
		Me.Label15.TabIndex = 1
		Me.Label15.Text = "Isi Variabel 3"
		'
		'Btn_Delete
		'
		Me.Btn_Delete.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Delete.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Btn_Delete.ForeColor = System.Drawing.Color.White
		Me.Btn_Delete.Location = New System.Drawing.Point(426, 387)
		Me.Btn_Delete.Margin = New System.Windows.Forms.Padding(4)
		Me.Btn_Delete.Name = "Btn_Delete"
		Me.Btn_Delete.Size = New System.Drawing.Size(140, 35)
		Me.Btn_Delete.TabIndex = 3
		Me.Btn_Delete.Text = "Delete"
		Me.Btn_Delete.UseVisualStyleBackColor = False
		'
		'Cb_SubMenuLv3
		'
		Me.Cb_SubMenuLv3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cb_SubMenuLv3.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Cb_SubMenuLv3.FormattingEnabled = True
		Me.Cb_SubMenuLv3.Location = New System.Drawing.Point(494, 89)
		Me.Cb_SubMenuLv3.Margin = New System.Windows.Forms.Padding(4)
		Me.Cb_SubMenuLv3.Name = "Cb_SubMenuLv3"
		Me.Cb_SubMenuLv3.Size = New System.Drawing.Size(164, 26)
		Me.Cb_SubMenuLv3.TabIndex = 5
		'
		'Btn_Refresh
		'
		Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
		Me.Btn_Refresh.Location = New System.Drawing.Point(285, 387)
		Me.Btn_Refresh.Margin = New System.Windows.Forms.Padding(4)
		Me.Btn_Refresh.Name = "Btn_Refresh"
		Me.Btn_Refresh.Size = New System.Drawing.Size(140, 35)
		Me.Btn_Refresh.TabIndex = 3
		Me.Btn_Refresh.Text = "Refresh"
		Me.Btn_Refresh.UseVisualStyleBackColor = False
		'
		'Btn_Simpan
		'
		Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
		Me.Btn_Simpan.Location = New System.Drawing.Point(146, 387)
		Me.Btn_Simpan.Margin = New System.Windows.Forms.Padding(4)
		Me.Btn_Simpan.Name = "Btn_Simpan"
		Me.Btn_Simpan.Size = New System.Drawing.Size(140, 35)
		Me.Btn_Simpan.TabIndex = 6
		Me.Btn_Simpan.Text = "&Save"
		Me.Btn_Simpan.UseVisualStyleBackColor = False
		'
		'Cb_SubMenu
		'
		Me.Cb_SubMenu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cb_SubMenu.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Cb_SubMenu.FormattingEnabled = True
		Me.Cb_SubMenu.Location = New System.Drawing.Point(131, 89)
		Me.Cb_SubMenu.Margin = New System.Windows.Forms.Padding(4)
		Me.Cb_SubMenu.Name = "Cb_SubMenu"
		Me.Cb_SubMenu.Size = New System.Drawing.Size(164, 26)
		Me.Cb_SubMenu.TabIndex = 2
		'
		'Cb_SubMenuLv2
		'
		Me.Cb_SubMenuLv2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cb_SubMenuLv2.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Cb_SubMenuLv2.FormattingEnabled = True
		Me.Cb_SubMenuLv2.Location = New System.Drawing.Point(494, 58)
		Me.Cb_SubMenuLv2.Margin = New System.Windows.Forms.Padding(4)
		Me.Cb_SubMenuLv2.Name = "Cb_SubMenuLv2"
		Me.Cb_SubMenuLv2.Size = New System.Drawing.Size(164, 26)
		Me.Cb_SubMenuLv2.TabIndex = 4
		'
		'Cb_MainMenu
		'
		Me.Cb_MainMenu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cb_MainMenu.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Cb_MainMenu.FormattingEnabled = True
		Me.Cb_MainMenu.Location = New System.Drawing.Point(131, 27)
		Me.Cb_MainMenu.Margin = New System.Windows.Forms.Padding(4)
		Me.Cb_MainMenu.Name = "Cb_MainMenu"
		Me.Cb_MainMenu.Size = New System.Drawing.Size(164, 26)
		Me.Cb_MainMenu.TabIndex = 0
		'
		'Cb_Menu
		'
		Me.Cb_Menu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cb_Menu.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Cb_Menu.FormattingEnabled = True
		Me.Cb_Menu.Location = New System.Drawing.Point(131, 59)
		Me.Cb_Menu.Margin = New System.Windows.Forms.Padding(4)
		Me.Cb_Menu.Name = "Cb_Menu"
		Me.Cb_Menu.Size = New System.Drawing.Size(164, 26)
		Me.Cb_Menu.TabIndex = 1
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Location = New System.Drawing.Point(12, 30)
		Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(79, 18)
		Me.Label2.TabIndex = 1
		Me.Label2.Text = "MainMenus"
		'
		'Cb_SubMenuLv1
		'
		Me.Cb_SubMenuLv1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cb_SubMenuLv1.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Cb_SubMenuLv1.FormattingEnabled = True
		Me.Cb_SubMenuLv1.Location = New System.Drawing.Point(494, 27)
		Me.Cb_SubMenuLv1.Margin = New System.Windows.Forms.Padding(4)
		Me.Cb_SubMenuLv1.Name = "Cb_SubMenuLv1"
		Me.Cb_SubMenuLv1.Size = New System.Drawing.Size(164, 26)
		Me.Cb_SubMenuLv1.TabIndex = 3
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label7.Location = New System.Drawing.Point(367, 92)
		Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(104, 18)
		Me.Label7.TabIndex = 1
		Me.Label7.Text = "SubMenus Lv 3"
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label6.Location = New System.Drawing.Point(367, 60)
		Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(104, 18)
		Me.Label6.TabIndex = 1
		Me.Label6.Text = "SubMenus Lv 2"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label5.Location = New System.Drawing.Point(367, 27)
		Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(101, 18)
		Me.Label5.TabIndex = 1
		Me.Label5.Text = "SubMenus Lv 1"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Location = New System.Drawing.Point(12, 97)
		Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(74, 18)
		Me.Label4.TabIndex = 1
		Me.Label4.Text = "SubMenus"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Location = New System.Drawing.Point(12, 63)
		Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(50, 18)
		Me.Label3.TabIndex = 1
		Me.Label3.Text = "Menus"
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.PanelGradient1)
		Me.Panel1.Controls.Add(Me.Lbl_Judul)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(1305, 54)
		Me.Panel1.TabIndex = 23
		'
		'PanelGradient1
		'
		Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
		Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
		Me.PanelGradient1.cuteTransparent1 = 100
		Me.PanelGradient1.cuteTransparent2 = 64
		Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.PanelGradient1.Location = New System.Drawing.Point(0, 52)
		Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
		Me.PanelGradient1.Name = "PanelGradient1"
		Me.PanelGradient1.Size = New System.Drawing.Size(1305, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Lbl_Judul
		'
		Me.Lbl_Judul.AutoSize = True
		Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Lbl_Judul.Location = New System.Drawing.Point(18, 11)
		Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
		Me.Lbl_Judul.Name = "Lbl_Judul"
		Me.Lbl_Judul.Size = New System.Drawing.Size(219, 30)
		Me.Lbl_Judul.TabIndex = 0
		Me.Lbl_Judul.Text = "Master Data - Menu"
		'
		'Lv_hierarki
		'
		Me.Lv_hierarki.FullRowSelect = True
		Me.Lv_hierarki.GridLines = True
		Me.Lv_hierarki.HideSelection = False
		Me.Lv_hierarki.Location = New System.Drawing.Point(3, 0)
		Me.Lv_hierarki.Name = "Lv_hierarki"
		Me.Lv_hierarki.Size = New System.Drawing.Size(539, 354)
		Me.Lv_hierarki.TabIndex = 24
		Me.Lv_hierarki.UseCompatibleStateImageBehavior = False
		Me.Lv_hierarki.View = System.Windows.Forms.View.Details
		'
		'TabControl1
		'
		Me.TabControl1.Controls.Add(Me.view)
		Me.TabControl1.Controls.Add(Me.order)
		Me.TabControl1.Location = New System.Drawing.Point(746, 64)
		Me.TabControl1.Name = "TabControl1"
		Me.TabControl1.SelectedIndex = 0
		Me.TabControl1.Size = New System.Drawing.Size(553, 431)
		Me.TabControl1.TabIndex = 25
		'
		'view
		'
		Me.view.Controls.Add(Me.Btn_Cari)
		Me.view.Controls.Add(Me.Txt_Filter)
		Me.view.Controls.Add(Me.Cmb_Filter)
		Me.view.Controls.Add(Me.Lv_hierarki)
		Me.view.Controls.Add(Me.Label17)
		Me.view.Location = New System.Drawing.Point(4, 25)
		Me.view.Name = "view"
		Me.view.Padding = New System.Windows.Forms.Padding(3)
		Me.view.Size = New System.Drawing.Size(545, 402)
		Me.view.TabIndex = 0
		Me.view.Text = "View"
		Me.view.UseVisualStyleBackColor = True
		'
		'Btn_Cari
		'
		Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Btn_Cari.ForeColor = System.Drawing.Color.White
		Me.Btn_Cari.Location = New System.Drawing.Point(464, 363)
		Me.Btn_Cari.Margin = New System.Windows.Forms.Padding(4)
		Me.Btn_Cari.Name = "Btn_Cari"
		Me.Btn_Cari.Size = New System.Drawing.Size(78, 31)
		Me.Btn_Cari.TabIndex = 6
		Me.Btn_Cari.Text = "&Cari"
		Me.Btn_Cari.UseVisualStyleBackColor = False
		'
		'Txt_Filter
		'
		Me.Txt_Filter.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Txt_Filter.Location = New System.Drawing.Point(193, 368)
		Me.Txt_Filter.Name = "Txt_Filter"
		Me.Txt_Filter.Size = New System.Drawing.Size(272, 23)
		Me.Txt_Filter.TabIndex = 26
		'
		'Cmb_Filter
		'
		Me.Cmb_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Filter.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Cmb_Filter.FormattingEnabled = True
		Me.Cmb_Filter.Location = New System.Drawing.Point(68, 367)
		Me.Cmb_Filter.Name = "Cmb_Filter"
		Me.Cmb_Filter.Size = New System.Drawing.Size(119, 25)
		Me.Cmb_Filter.TabIndex = 25
		'
		'Label17
		'
		Me.Label17.AutoSize = True
		Me.Label17.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label17.Location = New System.Drawing.Point(7, 368)
		Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label17.Name = "Label17"
		Me.Label17.Size = New System.Drawing.Size(44, 18)
		Me.Label17.TabIndex = 1
		Me.Label17.Text = "Filter"
		'
		'order
		'
		Me.order.Controls.Add(Me.Btn_Simpan_Order)
		Me.order.Controls.Add(Me.Dgv_Order)
		Me.order.Location = New System.Drawing.Point(4, 25)
		Me.order.Name = "order"
		Me.order.Padding = New System.Windows.Forms.Padding(3)
		Me.order.Size = New System.Drawing.Size(545, 402)
		Me.order.TabIndex = 1
		Me.order.Text = "Order"
		Me.order.UseVisualStyleBackColor = True
		'
		'Btn_Simpan_Order
		'
		Me.Btn_Simpan_Order.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Simpan_Order.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Btn_Simpan_Order.ForeColor = System.Drawing.Color.White
		Me.Btn_Simpan_Order.Location = New System.Drawing.Point(15, 361)
		Me.Btn_Simpan_Order.Margin = New System.Windows.Forms.Padding(4)
		Me.Btn_Simpan_Order.Name = "Btn_Simpan_Order"
		Me.Btn_Simpan_Order.Size = New System.Drawing.Size(140, 35)
		Me.Btn_Simpan_Order.TabIndex = 8
		Me.Btn_Simpan_Order.Text = "&Save"
		Me.Btn_Simpan_Order.UseVisualStyleBackColor = False
		'
		'Dgv_Order
		'
		Me.Dgv_Order.AllowUserToAddRows = False
		Me.Dgv_Order.BackgroundColor = System.Drawing.Color.White
		Me.Dgv_Order.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.Dgv_Order.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Menu_id, Me.mainmenu, Me.current_order, Me.new_order})
		Me.Dgv_Order.Location = New System.Drawing.Point(3, 3)
		Me.Dgv_Order.Name = "Dgv_Order"
		Me.Dgv_Order.RowHeadersWidth = 20
		Me.Dgv_Order.Size = New System.Drawing.Size(542, 351)
		Me.Dgv_Order.TabIndex = 0
		'
		'Menu_id
		'
		Me.Menu_id.HeaderText = "Menu Id"
		Me.Menu_id.Name = "Menu_id"
		Me.Menu_id.ReadOnly = True
		Me.Menu_id.Visible = False
		'
		'mainmenu
		'
		DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		Me.mainmenu.DefaultCellStyle = DataGridViewCellStyle1
		Me.mainmenu.HeaderText = "Menu Name"
		Me.mainmenu.Name = "mainmenu"
		Me.mainmenu.ReadOnly = True
		Me.mainmenu.Width = 290
		'
		'current_order
		'
		DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		Me.current_order.DefaultCellStyle = DataGridViewCellStyle2
		Me.current_order.HeaderText = "Current Order"
		Me.current_order.Name = "current_order"
		Me.current_order.ReadOnly = True
		Me.current_order.Width = 110
		'
		'new_order
		'
		DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		Me.new_order.DefaultCellStyle = DataGridViewCellStyle3
		Me.new_order.HeaderText = "New Order"
		Me.new_order.Name = "new_order"
		Me.new_order.Width = 110
		'
		'Txt_SelectedMenu
		'
		Me.Txt_SelectedMenu.Location = New System.Drawing.Point(1376, 122)
		Me.Txt_SelectedMenu.Name = "Txt_SelectedMenu"
		Me.Txt_SelectedMenu.Size = New System.Drawing.Size(100, 20)
		Me.Txt_SelectedMenu.TabIndex = 26
		Me.Txt_SelectedMenu.Visible = False
		'
		'Master_Menu_x
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(1305, 504)
		Me.Controls.Add(Me.Txt_SelectedMenu)
		Me.Controls.Add(Me.TabControl1)
		Me.Controls.Add(Me.Panel1)
		Me.Controls.Add(Me.GroupBox1)
		Me.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.MaximizeBox = False
		Me.Name = "Master_Menu_x"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "MasterMenu"
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.GroupBox2.ResumeLayout(False)
		Me.GroupBox2.PerformLayout()
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.TabControl1.ResumeLayout(False)
		Me.view.ResumeLayout(False)
		Me.view.PerformLayout()
		Me.order.ResumeLayout(False)
		CType(Me.Dgv_Order, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Cb_SubMenu As ComboBox
    Friend WithEvents Cb_Menu As ComboBox
    Friend WithEvents Cb_SubMenuLv1 As ComboBox
    Friend WithEvents Cb_SubMenuLv3 As ComboBox
    Friend WithEvents Cb_SubMenuLv2 As ComboBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Tb_MenuName As TextBox
    Friend WithEvents Tb_Var2 As TextBox
    Friend WithEvents Tb_Var1 As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Tb_MenuForm As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Tb_IsiVariabel1 As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Tb_Var3 As TextBox
    Friend WithEvents Tb_IsiVariabel3 As TextBox
    Friend WithEvents Tb_IsiVariabel2 As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Tb_ImagePath As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Delete As Button
    Friend WithEvents Lv_hierarki As ListView
    Friend WithEvents Cb_MainMenu As ComboBox
    Friend WithEvents Tb_MenuOrder As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents view As TabPage
    Friend WithEvents order As TabPage
    Friend WithEvents Dgv_Order As DataGridView
    Friend WithEvents Menu_id As DataGridViewTextBoxColumn
    Friend WithEvents mainmenu As DataGridViewTextBoxColumn
    Friend WithEvents current_order As DataGridViewTextBoxColumn
    Friend WithEvents new_order As DataGridViewTextBoxColumn
    Friend WithEvents Btn_Simpan_Order As Button
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Txt_Filter As TextBox
    Friend WithEvents Cmb_Filter As ComboBox
    Friend WithEvents Label17 As Label
    Friend WithEvents Txt_SelectedMenu As TextBox
End Class
