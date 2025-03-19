<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EMI_Display_Split_Stock
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
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Lv_Parent = New System.Windows.Forms.ListView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Cmb_1 = New System.Windows.Forms.ComboBox()
        Me.Chk_1 = New System.Windows.Forms.CheckBox()
        Me.Txt_ParamLainValue = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Cmb_3 = New System.Windows.Forms.ComboBox()
        Me.Chk_3 = New System.Windows.Forms.CheckBox()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Chk_2 = New System.Windows.Forms.CheckBox()
        Me.Cmb_2 = New System.Windows.Forms.ComboBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Lv_Child = New System.Windows.Forms.ListView()
        Me.BtnMasuk_Cari = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Lv_DetailPallet = New System.Windows.Forms.ListView()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1004, 51)
        Me.Panel1.TabIndex = 27
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 11)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(225, 25)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Display - Split Stock"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 37
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 65)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(12, 601)
        Me.Panel3.TabIndex = 38
        Me.Panel3.Visible = False
        '
        'Lv_Parent
        '
        Me.Lv_Parent.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_Parent.FullRowSelect = True
        Me.Lv_Parent.GridLines = True
        Me.Lv_Parent.HideSelection = False
        Me.Lv_Parent.Location = New System.Drawing.Point(12, 65)
        Me.Lv_Parent.Name = "Lv_Parent"
        Me.Lv_Parent.Size = New System.Drawing.Size(980, 231)
        Me.Lv_Parent.TabIndex = 235
        Me.Lv_Parent.UseCompatibleStateImageBehavior = False
        Me.Lv_Parent.View = System.Windows.Forms.View.Details
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(992, 87)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(12, 601)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.BtnMasuk_Cari)
        Me.GroupBox1.Controls.Add(Me.Cmb_1)
        Me.GroupBox1.Controls.Add(Me.Chk_1)
        Me.GroupBox1.Controls.Add(Me.Txt_ParamLainValue)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Cmb_3)
        Me.GroupBox1.Controls.Add(Me.Chk_3)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker2)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox1.Controls.Add(Me.Chk_2)
        Me.GroupBox1.Controls.Add(Me.Cmb_2)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 558)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox1.Size = New System.Drawing.Size(750, 131)
        Me.GroupBox1.TabIndex = 344
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filter Data"
        '
        'Cmb_1
        '
        Me.Cmb_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_1.FormattingEnabled = True
        Me.Cmb_1.Location = New System.Drawing.Point(8, 21)
        Me.Cmb_1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_1.Name = "Cmb_1"
        Me.Cmb_1.Size = New System.Drawing.Size(209, 26)
        Me.Cmb_1.TabIndex = 342
        '
        'Chk_1
        '
        Me.Chk_1.AutoSize = True
        Me.Chk_1.Location = New System.Drawing.Point(8, 46)
        Me.Chk_1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Chk_1.Name = "Chk_1"
        Me.Chk_1.Size = New System.Drawing.Size(133, 22)
        Me.Chk_1.TabIndex = 9
        Me.Chk_1.Text = "Transaksi Hari Ini"
        Me.Chk_1.UseVisualStyleBackColor = True
        '
        'Txt_ParamLainValue
        '
        Me.Txt_ParamLainValue.Enabled = False
        Me.Txt_ParamLainValue.Location = New System.Drawing.Point(383, 99)
        Me.Txt_ParamLainValue.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_ParamLainValue.Name = "Txt_ParamLainValue"
        Me.Txt_ParamLainValue.Size = New System.Drawing.Size(271, 23)
        Me.Txt_ParamLainValue.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(337, 100)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 18)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Value"
        '
        'Cmb_3
        '
        Me.Cmb_3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_3.Enabled = False
        Me.Cmb_3.FormattingEnabled = True
        Me.Cmb_3.Location = New System.Drawing.Point(155, 96)
        Me.Cmb_3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_3.Name = "Cmb_3"
        Me.Cmb_3.Size = New System.Drawing.Size(171, 26)
        Me.Cmb_3.TabIndex = 6
        '
        'Chk_3
        '
        Me.Chk_3.AutoSize = True
        Me.Chk_3.Location = New System.Drawing.Point(8, 96)
        Me.Chk_3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Chk_3.Name = "Chk_3"
        Me.Chk_3.Size = New System.Drawing.Size(122, 22)
        Me.Chk_3.TabIndex = 5
        Me.Chk_3.Text = "Parameter Lain"
        Me.Chk_3.UseVisualStyleBackColor = True
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker2.Enabled = False
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(561, 69)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(177, 23)
        Me.DateTimePicker2.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(526, 72)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(28, 18)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "s/d"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker1.Enabled = False
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(340, 69)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(177, 23)
        Me.DateTimePicker1.TabIndex = 3
        '
        'Chk_2
        '
        Me.Chk_2.AutoSize = True
        Me.Chk_2.Location = New System.Drawing.Point(8, 70)
        Me.Chk_2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Chk_2.Name = "Chk_2"
        Me.Chk_2.Size = New System.Drawing.Size(143, 22)
        Me.Chk_2.TabIndex = 1
        Me.Chk_2.Text = "Parameter Tanggal"
        Me.Chk_2.UseVisualStyleBackColor = True
        '
        'Cmb_2
        '
        Me.Cmb_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_2.Enabled = False
        Me.Cmb_2.FormattingEnabled = True
        Me.Cmb_2.Location = New System.Drawing.Point(155, 68)
        Me.Cmb_2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_2.Name = "Cmb_2"
        Me.Cmb_2.Size = New System.Drawing.Size(171, 26)
        Me.Cmb_2.TabIndex = 2
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(20, 691)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(942, 12)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Lv_Child
        '
        Me.Lv_Child.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_Child.FullRowSelect = True
        Me.Lv_Child.GridLines = True
        Me.Lv_Child.HideSelection = False
        Me.Lv_Child.Location = New System.Drawing.Point(7, 22)
        Me.Lv_Child.Name = "Lv_Child"
        Me.Lv_Child.Size = New System.Drawing.Size(536, 222)
        Me.Lv_Child.TabIndex = 346
        Me.Lv_Child.UseCompatibleStateImageBehavior = False
        Me.Lv_Child.View = System.Windows.Forms.View.Details
        '
        'BtnMasuk_Cari
        '
        Me.BtnMasuk_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnMasuk_Cari.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnMasuk_Cari.ForeColor = System.Drawing.Color.White
        Me.BtnMasuk_Cari.Location = New System.Drawing.Point(659, 96)
        Me.BtnMasuk_Cari.Name = "BtnMasuk_Cari"
        Me.BtnMasuk_Cari.Size = New System.Drawing.Size(81, 27)
        Me.BtnMasuk_Cari.TabIndex = 343
        Me.BtnMasuk_Cari.Text = "&Cari"
        Me.BtnMasuk_Cari.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Lv_Child)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 302)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(549, 250)
        Me.GroupBox2.TabIndex = 347
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Detail"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Lv_DetailPallet)
        Me.GroupBox3.Location = New System.Drawing.Point(567, 302)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(425, 250)
        Me.GroupBox3.TabIndex = 347
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Detail Pallet"
        '
        'Lv_DetailPallet
        '
        Me.Lv_DetailPallet.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_DetailPallet.FullRowSelect = True
        Me.Lv_DetailPallet.GridLines = True
        Me.Lv_DetailPallet.HideSelection = False
        Me.Lv_DetailPallet.Location = New System.Drawing.Point(6, 22)
        Me.Lv_DetailPallet.Name = "Lv_DetailPallet"
        Me.Lv_DetailPallet.Size = New System.Drawing.Size(416, 222)
        Me.Lv_DetailPallet.TabIndex = 346
        Me.Lv_DetailPallet.UseCompatibleStateImageBehavior = False
        Me.Lv_DetailPallet.View = System.Windows.Forms.View.Details
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1004, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'EMI_Display_Split_Stock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1004, 701)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Lv_Parent)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Name = "EMI_Display_Split_Stock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Lv_Parent As ListView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Cmb_1 As ComboBox
    Friend WithEvents Chk_1 As CheckBox
    Friend WithEvents Txt_ParamLainValue As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Cmb_3 As ComboBox
    Friend WithEvents Chk_3 As CheckBox
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Chk_2 As CheckBox
    Friend WithEvents Cmb_2 As ComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Lv_Child As ListView
    Friend WithEvents BtnMasuk_Cari As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Lv_DetailPallet As ListView
End Class
