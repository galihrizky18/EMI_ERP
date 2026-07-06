<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Laporan_Budget
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
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.BtnCetak = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
        Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Cmb_Department = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Cmb_Binding = New System.Windows.Forms.ComboBox()
        Me.Txt_Jenis = New System.Windows.Forms.TextBox()
        Me.Txt_Sub = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Lv_Jenis = New System.Windows.Forms.ListView()
        Me.Lv_Sub = New System.Windows.Forms.ListView()
        Me.CmbJenis2 = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Cmb_Tahun = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.BlnAwal = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.BlnAkhir = New System.Windows.Forms.ComboBox()
        Me.Chk_Periode = New System.Windows.Forms.CheckBox()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(587, 37)
        Me.Panel1.TabIndex = 92
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(15, 6)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(168, 29)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Laporan Budget"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(0, 38)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(706, 10)
        Me.Panel5.TabIndex = 93
        Me.Panel5.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 41)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(14, 311)
        Me.Panel3.TabIndex = 94
        Me.Panel3.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(573, 54)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(14, 488)
        Me.Panel2.TabIndex = 94
        Me.Panel2.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(20, 233)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(706, 12)
        Me.Panel4.TabIndex = 96
        Me.Panel4.Visible = False
        '
        'BtnExit
        '
        Me.BtnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnExit.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnExit.ForeColor = System.Drawing.Color.White
        Me.BtnExit.Location = New System.Drawing.Point(434, 202)
        Me.BtnExit.Margin = New System.Windows.Forms.Padding(2)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(130, 30)
        Me.BtnExit.TabIndex = 98
        Me.BtnExit.Text = "&Keluar"
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'BtnCetak
        '
        Me.BtnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnCetak.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnCetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCetak.ForeColor = System.Drawing.Color.White
        Me.BtnCetak.Location = New System.Drawing.Point(287, 201)
        Me.BtnCetak.Margin = New System.Windows.Forms.Padding(2)
        Me.BtnCetak.Name = "BtnCetak"
        Me.BtnCetak.Size = New System.Drawing.Size(130, 30)
        Me.BtnCetak.TabIndex = 97
        Me.BtnCetak.Text = "&Cetak"
        Me.BtnCetak.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(30, 67)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 17)
        Me.Label2.TabIndex = 114
        Me.Label2.Text = "Periode"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(357, 64)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 16)
        Me.Label3.TabIndex = 112
        Me.Label3.Text = "s/d"
        '
        'Tgl2
        '
        Me.Tgl2.CustomFormat = "dd MMMM yyyy"
        Me.Tgl2.Enabled = False
        Me.Tgl2.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl2.Location = New System.Drawing.Point(389, 62)
        Me.Tgl2.Margin = New System.Windows.Forms.Padding(2)
        Me.Tgl2.Name = "Tgl2"
        Me.Tgl2.Size = New System.Drawing.Size(173, 22)
        Me.Tgl2.TabIndex = 111
        '
        'Tgl1
        '
        Me.Tgl1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Tgl1.CustomFormat = "dd MMMM yyyy"
        Me.Tgl1.Enabled = False
        Me.Tgl1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl1.Location = New System.Drawing.Point(182, 62)
        Me.Tgl1.Margin = New System.Windows.Forms.Padding(2)
        Me.Tgl1.Name = "Tgl1"
        Me.Tgl1.Size = New System.Drawing.Size(171, 22)
        Me.Tgl1.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(30, 340)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 17)
        Me.Label4.TabIndex = 130
        Me.Label4.Text = "Department"
        Me.Label4.Visible = False
        '
        'Cmb_Department
        '
        Me.Cmb_Department.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Department.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Cmb_Department.FormattingEnabled = True
        Me.Cmb_Department.Location = New System.Drawing.Point(172, 338)
        Me.Cmb_Department.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmb_Department.Name = "Cmb_Department"
        Me.Cmb_Department.Size = New System.Drawing.Size(380, 24)
        Me.Cmb_Department.TabIndex = 129
        Me.Cmb_Department.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(30, 121)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 17)
        Me.Label1.TabIndex = 131
        Me.Label1.Text = "Department"
        '
        'Cmb_Binding
        '
        Me.Cmb_Binding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Binding.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Cmb_Binding.FormattingEnabled = True
        Me.Cmb_Binding.Location = New System.Drawing.Point(158, 119)
        Me.Cmb_Binding.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmb_Binding.Name = "Cmb_Binding"
        Me.Cmb_Binding.Size = New System.Drawing.Size(404, 24)
        Me.Cmb_Binding.TabIndex = 132
        '
        'Txt_Jenis
        '
        Me.Txt_Jenis.Location = New System.Drawing.Point(158, 150)
        Me.Txt_Jenis.Name = "Txt_Jenis"
        Me.Txt_Jenis.Size = New System.Drawing.Size(404, 20)
        Me.Txt_Jenis.TabIndex = 133
        '
        'Txt_Sub
        '
        Me.Txt_Sub.Location = New System.Drawing.Point(158, 176)
        Me.Txt_Sub.Name = "Txt_Sub"
        Me.Txt_Sub.Size = New System.Drawing.Size(404, 20)
        Me.Txt_Sub.TabIndex = 134
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(30, 153)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 17)
        Me.Label5.TabIndex = 135
        Me.Label5.Text = "Kategori Jenis"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(30, 179)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(92, 17)
        Me.Label6.TabIndex = 136
        Me.Label6.Text = "Sub Kategori 1"
        '
        'Lv_Jenis
        '
        Me.Lv_Jenis.HideSelection = False
        Me.Lv_Jenis.Location = New System.Drawing.Point(148, 423)
        Me.Lv_Jenis.Name = "Lv_Jenis"
        Me.Lv_Jenis.Size = New System.Drawing.Size(404, 102)
        Me.Lv_Jenis.TabIndex = 137
        Me.Lv_Jenis.UseCompatibleStateImageBehavior = False
        Me.Lv_Jenis.Visible = False
        '
        'Lv_Sub
        '
        Me.Lv_Sub.HideSelection = False
        Me.Lv_Sub.Location = New System.Drawing.Point(46, 461)
        Me.Lv_Sub.Name = "Lv_Sub"
        Me.Lv_Sub.Size = New System.Drawing.Size(404, 108)
        Me.Lv_Sub.TabIndex = 138
        Me.Lv_Sub.UseCompatibleStateImageBehavior = False
        Me.Lv_Sub.Visible = False
        '
        'CmbJenis2
        '
        Me.CmbJenis2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbJenis2.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.CmbJenis2.FormattingEnabled = True
        Me.CmbJenis2.Location = New System.Drawing.Point(172, 368)
        Me.CmbJenis2.Margin = New System.Windows.Forms.Padding(2)
        Me.CmbJenis2.Name = "CmbJenis2"
        Me.CmbJenis2.Size = New System.Drawing.Size(380, 24)
        Me.CmbJenis2.TabIndex = 139
        Me.CmbJenis2.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(30, 370)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(38, 17)
        Me.Label7.TabIndex = 140
        Me.Label7.Text = "Jenis"
        Me.Label7.Visible = False
        '
        'Cmb_Tahun
        '
        Me.Cmb_Tahun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Tahun.FormattingEnabled = True
        Me.Cmb_Tahun.Location = New System.Drawing.Point(158, 90)
        Me.Cmb_Tahun.Name = "Cmb_Tahun"
        Me.Cmb_Tahun.Size = New System.Drawing.Size(90, 24)
        Me.Cmb_Tahun.TabIndex = 141
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(30, 92)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(97, 17)
        Me.Label8.TabIndex = 142
        Me.Label8.Text = "Periode Budget"
        '
        'BlnAwal
        '
        Me.BlnAwal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.BlnAwal.FormattingEnabled = True
        Me.BlnAwal.Location = New System.Drawing.Point(258, 90)
        Me.BlnAwal.Name = "BlnAwal"
        Me.BlnAwal.Size = New System.Drawing.Size(134, 24)
        Me.BlnAwal.TabIndex = 143
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(395, 94)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(25, 16)
        Me.Label9.TabIndex = 144
        Me.Label9.Text = "s/d"
        '
        'BlnAkhir
        '
        Me.BlnAkhir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.BlnAkhir.FormattingEnabled = True
        Me.BlnAkhir.Location = New System.Drawing.Point(423, 90)
        Me.BlnAkhir.Name = "BlnAkhir"
        Me.BlnAkhir.Size = New System.Drawing.Size(139, 24)
        Me.BlnAkhir.TabIndex = 145
        '
        'Chk_Periode
        '
        Me.Chk_Periode.AutoSize = True
        Me.Chk_Periode.Location = New System.Drawing.Point(158, 66)
        Me.Chk_Periode.Name = "Chk_Periode"
        Me.Chk_Periode.Size = New System.Drawing.Size(15, 14)
        Me.Chk_Periode.TabIndex = 146
        Me.Chk_Periode.UseVisualStyleBackColor = True
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 35)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(587, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'N_EMI_Laporan_Budget
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(587, 244)
        Me.Controls.Add(Me.BlnAkhir)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.BlnAwal)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Cmb_Tahun)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Lv_Jenis)
        Me.Controls.Add(Me.Lv_Sub)
        Me.Controls.Add(Me.CmbJenis2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Txt_Sub)
        Me.Controls.Add(Me.Txt_Jenis)
        Me.Controls.Add(Me.Cmb_Binding)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Cmb_Department)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Tgl2)
        Me.Controls.Add(Me.Tgl1)
        Me.Controls.Add(Me.Chk_Periode)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnCetak)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_Laporan_Budget"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label11 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents BtnExit As Button
    Friend WithEvents BtnCetak As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Tgl2 As DateTimePicker
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents Cmb_Department As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Cmb_Binding As ComboBox
    Friend WithEvents Txt_Jenis As TextBox
    Friend WithEvents Txt_Sub As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Lv_Jenis As ListView
    Friend WithEvents Lv_Sub As ListView
    Friend WithEvents CmbJenis2 As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Cmb_Tahun As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents BlnAwal As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents BlnAkhir As ComboBox
    Friend WithEvents Chk_Periode As CheckBox
End Class
