<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Emi_Display_Retur_Pembelian
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Cmb1 = New System.Windows.Forms.ComboBox()
        Me.Chk_HariIni = New System.Windows.Forms.CheckBox()
        Me.Txt_ParamLain = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Cmb_ParamLain = New System.Windows.Forms.ComboBox()
        Me.Chk_ParamLain = New System.Windows.Forms.CheckBox()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Chk_Tanggal = New System.Windows.Forms.CheckBox()
        Me.Cmb_Tanggal = New System.Windows.Forms.ComboBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Lv_Retur = New System.Windows.Forms.ListView()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Lv_Retur_Detail = New System.Windows.Forms.ListView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Lv_Retur_Mobil = New System.Windows.Forms.ListView()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1384, 51)
        Me.Panel1.TabIndex = 344
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1384, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(17, 10)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(282, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Display - Retur Pembelian"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 50)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 345
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 66)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(12, 491)
        Me.Panel3.TabIndex = 346
        Me.Panel3.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Btn_Cari)
        Me.GroupBox1.Controls.Add(Me.Cmb1)
        Me.GroupBox1.Controls.Add(Me.Chk_HariIni)
        Me.GroupBox1.Controls.Add(Me.Txt_ParamLain)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Cmb_ParamLain)
        Me.GroupBox1.Controls.Add(Me.Chk_ParamLain)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker2)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox1.Controls.Add(Me.Chk_Tanggal)
        Me.GroupBox1.Controls.Add(Me.Cmb_Tanggal)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 558)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox1.Size = New System.Drawing.Size(670, 131)
        Me.GroupBox1.TabIndex = 347
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filter Data"
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(562, 96)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(81, 27)
        Me.Btn_Cari.TabIndex = 343
        Me.Btn_Cari.Text = "&Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Cmb1
        '
        Me.Cmb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb1.FormattingEnabled = True
        Me.Cmb1.Location = New System.Drawing.Point(8, 21)
        Me.Cmb1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb1.Name = "Cmb1"
        Me.Cmb1.Size = New System.Drawing.Size(209, 21)
        Me.Cmb1.TabIndex = 342
        '
        'Chk_HariIni
        '
        Me.Chk_HariIni.AutoSize = True
        Me.Chk_HariIni.Location = New System.Drawing.Point(8, 46)
        Me.Chk_HariIni.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Chk_HariIni.Name = "Chk_HariIni"
        Me.Chk_HariIni.Size = New System.Drawing.Size(108, 17)
        Me.Chk_HariIni.TabIndex = 9
        Me.Chk_HariIni.Text = "Transaksi Hari Ini"
        Me.Chk_HariIni.UseVisualStyleBackColor = True
        '
        'Txt_ParamLain
        '
        Me.Txt_ParamLain.BackColor = System.Drawing.Color.White
        Me.Txt_ParamLain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_ParamLain.Enabled = False
        Me.Txt_ParamLain.Location = New System.Drawing.Point(349, 99)
        Me.Txt_ParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_ParamLain.Name = "Txt_ParamLain"
        Me.Txt_ParamLain.Size = New System.Drawing.Size(208, 20)
        Me.Txt_ParamLain.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(310, 100)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(34, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Value"
        '
        'Cmb_ParamLain
        '
        Me.Cmb_ParamLain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_ParamLain.Enabled = False
        Me.Cmb_ParamLain.FormattingEnabled = True
        Me.Cmb_ParamLain.Location = New System.Drawing.Point(143, 96)
        Me.Cmb_ParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_ParamLain.Name = "Cmb_ParamLain"
        Me.Cmb_ParamLain.Size = New System.Drawing.Size(159, 21)
        Me.Cmb_ParamLain.TabIndex = 6
        '
        'Chk_ParamLain
        '
        Me.Chk_ParamLain.AutoSize = True
        Me.Chk_ParamLain.Location = New System.Drawing.Point(8, 96)
        Me.Chk_ParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Chk_ParamLain.Name = "Chk_ParamLain"
        Me.Chk_ParamLain.Size = New System.Drawing.Size(97, 17)
        Me.Chk_ParamLain.TabIndex = 5
        Me.Chk_ParamLain.Text = "Parameter Lain"
        Me.Chk_ParamLain.UseVisualStyleBackColor = True
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker2.Enabled = False
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(496, 72)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(143, 20)
        Me.DateTimePicker2.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(464, 74)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(23, 13)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "s/d"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker1.Enabled = False
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(313, 72)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(143, 20)
        Me.DateTimePicker1.TabIndex = 3
        '
        'Chk_Tanggal
        '
        Me.Chk_Tanggal.AutoSize = True
        Me.Chk_Tanggal.Location = New System.Drawing.Point(8, 70)
        Me.Chk_Tanggal.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Chk_Tanggal.Name = "Chk_Tanggal"
        Me.Chk_Tanggal.Size = New System.Drawing.Size(116, 17)
        Me.Chk_Tanggal.TabIndex = 1
        Me.Chk_Tanggal.Text = "Parameter Tanggal"
        Me.Chk_Tanggal.UseVisualStyleBackColor = True
        '
        'Cmb_Tanggal
        '
        Me.Cmb_Tanggal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Tanggal.Enabled = False
        Me.Cmb_Tanggal.FormattingEnabled = True
        Me.Cmb_Tanggal.Location = New System.Drawing.Point(143, 68)
        Me.Cmb_Tanggal.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Tanggal.Name = "Cmb_Tanggal"
        Me.Cmb_Tanggal.Size = New System.Drawing.Size(159, 21)
        Me.Cmb_Tanggal.TabIndex = 2
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(18, 689)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(942, 12)
        Me.Panel4.TabIndex = 345
        Me.Panel4.Visible = False
        '
        'Lv_Retur
        '
        Me.Lv_Retur.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lv_Retur.FullRowSelect = True
        Me.Lv_Retur.GridLines = True
        Me.Lv_Retur.HideSelection = False
        Me.Lv_Retur.Location = New System.Drawing.Point(12, 64)
        Me.Lv_Retur.Name = "Lv_Retur"
        Me.Lv_Retur.Size = New System.Drawing.Size(1358, 235)
        Me.Lv_Retur.TabIndex = 348
        Me.Lv_Retur.UseCompatibleStateImageBehavior = False
        Me.Lv_Retur.View = System.Windows.Forms.View.Details
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1372, 82)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(12, 491)
        Me.Panel5.TabIndex = 346
        Me.Panel5.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(13, 300)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(942, 12)
        Me.Panel6.TabIndex = 345
        Me.Panel6.Visible = False
        '
        'Lv_Retur_Detail
        '
        Me.Lv_Retur_Detail.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lv_Retur_Detail.FullRowSelect = True
        Me.Lv_Retur_Detail.GridLines = True
        Me.Lv_Retur_Detail.HideSelection = False
        Me.Lv_Retur_Detail.Location = New System.Drawing.Point(5, 19)
        Me.Lv_Retur_Detail.Name = "Lv_Retur_Detail"
        Me.Lv_Retur_Detail.Size = New System.Drawing.Size(663, 224)
        Me.Lv_Retur_Detail.TabIndex = 349
        Me.Lv_Retur_Detail.UseCompatibleStateImageBehavior = False
        Me.Lv_Retur_Detail.View = System.Windows.Forms.View.Details
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Lv_Retur_Detail)
        Me.GroupBox2.Location = New System.Drawing.Point(13, 304)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(673, 248)
        Me.GroupBox2.TabIndex = 350
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Detail Retur"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Lv_Retur_Mobil)
        Me.GroupBox3.Location = New System.Drawing.Point(697, 306)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(673, 248)
        Me.GroupBox3.TabIndex = 350
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Detail Mobil"
        '
        'Lv_Retur_Mobil
        '
        Me.Lv_Retur_Mobil.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lv_Retur_Mobil.FullRowSelect = True
        Me.Lv_Retur_Mobil.GridLines = True
        Me.Lv_Retur_Mobil.HideSelection = False
        Me.Lv_Retur_Mobil.Location = New System.Drawing.Point(5, 19)
        Me.Lv_Retur_Mobil.Name = "Lv_Retur_Mobil"
        Me.Lv_Retur_Mobil.Size = New System.Drawing.Size(663, 224)
        Me.Lv_Retur_Mobil.TabIndex = 349
        Me.Lv_Retur_Mobil.UseCompatibleStateImageBehavior = False
        Me.Lv_Retur_Mobil.View = System.Windows.Forms.View.Details
        '
        'Emi_Display_Retur_Pembelian
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1384, 701)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Lv_Retur)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Emi_Display_Retur_Pembelian"
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
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Cmb1 As ComboBox
    Friend WithEvents Chk_HariIni As CheckBox
    Friend WithEvents Txt_ParamLain As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Cmb_ParamLain As ComboBox
    Friend WithEvents Chk_ParamLain As CheckBox
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Chk_Tanggal As CheckBox
    Friend WithEvents Cmb_Tanggal As ComboBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Lv_Retur As ListView
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Lv_Retur_Detail As ListView
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Lv_Retur_Mobil As ListView
End Class
