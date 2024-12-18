<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Barang_Masuk_Summary_Data
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.LV_PembelianLoading = New System.Windows.Forms.ListView()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Lv_PODetail = New System.Windows.Forms.ListView()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.BtnBarangMasuk_Cari = New System.Windows.Forms.Button()
        Me.ComboBox6 = New System.Windows.Forms.ComboBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Panel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1005, 51)
        Me.Panel1.TabIndex = 22
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1005, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(422, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Summary Data - Loading Barang Masuk"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 34
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 63)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 491)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(986, 63)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 491)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(21, 568)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1436, 15)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'LV_PembelianLoading
        '
        Me.LV_PembelianLoading.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.LV_PembelianLoading.FullRowSelect = True
        Me.LV_PembelianLoading.GridLines = True
        Me.LV_PembelianLoading.HideSelection = False
        Me.LV_PembelianLoading.Location = New System.Drawing.Point(21, 64)
        Me.LV_PembelianLoading.Name = "LV_PembelianLoading"
        Me.LV_PembelianLoading.Size = New System.Drawing.Size(963, 257)
        Me.LV_PembelianLoading.TabIndex = 234
        Me.LV_PembelianLoading.UseCompatibleStateImageBehavior = False
        Me.LV_PembelianLoading.View = System.Windows.Forms.View.Details
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(20, 721)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1436, 15)
        Me.Panel6.TabIndex = 39
        Me.Panel6.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(21, 322)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1436, 24)
        Me.Panel7.TabIndex = 39
        Me.Panel7.Visible = False
        '
        'Lv_PODetail
        '
        Me.Lv_PODetail.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_PODetail.FullRowSelect = True
        Me.Lv_PODetail.GridLines = True
        Me.Lv_PODetail.HideSelection = False
        Me.Lv_PODetail.Location = New System.Drawing.Point(0, 0)
        Me.Lv_PODetail.Name = "Lv_PODetail"
        Me.Lv_PODetail.Size = New System.Drawing.Size(955, 211)
        Me.Lv_PODetail.TabIndex = 341
        Me.Lv_PODetail.UseCompatibleStateImageBehavior = False
        Me.Lv_PODetail.View = System.Windows.Forms.View.Details
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.BtnBarangMasuk_Cari)
        Me.GroupBox3.Controls.Add(Me.ComboBox6)
        Me.GroupBox3.Controls.Add(Me.CheckBox3)
        Me.GroupBox3.Controls.Add(Me.TextBox4)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.ComboBox2)
        Me.GroupBox3.Controls.Add(Me.CheckBox2)
        Me.GroupBox3.Controls.Add(Me.DateTimePicker2)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox3.Controls.Add(Me.CheckBox1)
        Me.GroupBox3.Controls.Add(Me.ComboBox3)
        Me.GroupBox3.Location = New System.Drawing.Point(20, 588)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox3.Size = New System.Drawing.Size(636, 130)
        Me.GroupBox3.TabIndex = 342
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Filter Data"
        '
        'BtnBarangMasuk_Cari
        '
        Me.BtnBarangMasuk_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnBarangMasuk_Cari.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnBarangMasuk_Cari.ForeColor = System.Drawing.Color.White
        Me.BtnBarangMasuk_Cari.Location = New System.Drawing.Point(496, 92)
        Me.BtnBarangMasuk_Cari.Name = "BtnBarangMasuk_Cari"
        Me.BtnBarangMasuk_Cari.Size = New System.Drawing.Size(81, 27)
        Me.BtnBarangMasuk_Cari.TabIndex = 343
        Me.BtnBarangMasuk_Cari.Text = "&Cari"
        Me.BtnBarangMasuk_Cari.UseVisualStyleBackColor = False
        '
        'ComboBox6
        '
        Me.ComboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox6.FormattingEnabled = True
        Me.ComboBox6.Location = New System.Drawing.Point(8, 23)
        Me.ComboBox6.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ComboBox6.Name = "ComboBox6"
        Me.ComboBox6.Size = New System.Drawing.Size(209, 24)
        Me.ComboBox6.TabIndex = 342
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(8, 48)
        Me.CheckBox3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(118, 20)
        Me.CheckBox3.TabIndex = 9
        Me.CheckBox3.Text = "Transaksi Hari Ini"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(283, 95)
        Me.TextBox4.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(208, 20)
        Me.TextBox4.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(244, 96)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 16)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Value"
        '
        'ComboBox2
        '
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(143, 92)
        Me.ComboBox2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(97, 24)
        Me.ComboBox2.TabIndex = 6
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(8, 97)
        Me.CheckBox2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(107, 20)
        Me.CheckBox2.TabIndex = 5
        Me.CheckBox2.Text = "Parameter Lain"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(419, 66)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(158, 20)
        Me.DateTimePicker2.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(390, 67)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(25, 16)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "s/d"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(228, 66)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(158, 20)
        Me.DateTimePicker1.TabIndex = 3
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(8, 72)
        Me.CheckBox1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(124, 20)
        Me.CheckBox1.TabIndex = 1
        Me.CheckBox1.Text = "Parameter Tanggal"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'ComboBox3
        '
        Me.ComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Location = New System.Drawing.Point(143, 64)
        Me.ComboBox3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(81, 24)
        Me.ComboBox3.TabIndex = 2
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(21, 331)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(963, 240)
        Me.TabControl1.TabIndex = 343
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Lv_PODetail)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(955, 211)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.ListView1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(955, 211)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(955, 211)
        Me.ListView1.TabIndex = 342
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'EMI_Barang_Masuk_Summary_Data
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1005, 737)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.LV_PembelianLoading)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Barang_Masuk_Summary_Data"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents LV_PembelianLoading As ListView
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Lv_PODetail As ListView
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents ComboBox6 As ComboBox
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents ComboBox3 As ComboBox
    Friend WithEvents BtnBarangMasuk_Cari As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents ListView1 As ListView
End Class
