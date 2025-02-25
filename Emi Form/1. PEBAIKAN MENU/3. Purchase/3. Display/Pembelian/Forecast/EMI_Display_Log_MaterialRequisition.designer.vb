<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Display_Log_MaterialRequisition
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Lv_PR = New System.Windows.Forms.ListView()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbParameterBarang = New System.Windows.Forms.ComboBox()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Cmb_KategoriBesar = New System.Windows.Forms.ComboBox()
        Me.Cmb_KategoriKecil = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.Cb_ParamBrg = New System.Windows.Forms.CheckBox()
        Me.Txt_KdBrg = New System.Windows.Forms.TextBox()
        Me.Cmb_Tahun = New System.Windows.Forms.ComboBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Txt_ParamLain = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Cmb_ParamLain = New System.Windows.Forms.ComboBox()
        Me.Cb_ParamLain = New System.Windows.Forms.CheckBox()
        Me.Cb_ParamTgl = New System.Windows.Forms.CheckBox()
        Me.Cmb_Bulan = New System.Windows.Forms.ComboBox()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1268, 51)
        Me.Panel1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(479, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Display -  Log Transaksi Material Requsition"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1383, 12)
        Me.Panel2.TabIndex = 34
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 63)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 458)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1245, 64)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 491)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Lv_PR
        '
        Me.Lv_PR.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Lv_PR.FullRowSelect = True
        Me.Lv_PR.GridLines = True
        Me.Lv_PR.HideSelection = False
        Me.Lv_PR.Location = New System.Drawing.Point(21, 65)
        Me.Lv_PR.Name = "Lv_PR"
        Me.Lv_PR.Size = New System.Drawing.Size(1220, 464)
        Me.Lv_PR.TabIndex = 234
        Me.Lv_PR.UseCompatibleStateImageBehavior = False
        Me.Lv_PR.View = System.Windows.Forms.View.Details
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(15, 692)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1436, 15)
        Me.Panel6.TabIndex = 39
        Me.Panel6.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(1, 529)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1436, 17)
        Me.Panel7.TabIndex = 39
        Me.Panel7.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.cmbParameterBarang)
        Me.GroupBox3.Controls.Add(Me.Btn_Refresh)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Cmb_KategoriBesar)
        Me.GroupBox3.Controls.Add(Me.Cmb_KategoriKecil)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.ListView2)
        Me.GroupBox3.Controls.Add(Me.Cb_ParamBrg)
        Me.GroupBox3.Controls.Add(Me.Txt_KdBrg)
        Me.GroupBox3.Controls.Add(Me.Cmb_Tahun)
        Me.GroupBox3.Controls.Add(Me.Btn_Cari)
        Me.GroupBox3.Controls.Add(Me.Txt_ParamLain)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Cmb_ParamLain)
        Me.GroupBox3.Controls.Add(Me.Cb_ParamLain)
        Me.GroupBox3.Controls.Add(Me.Cb_ParamTgl)
        Me.GroupBox3.Controls.Add(Me.Cmb_Bulan)
        Me.GroupBox3.Location = New System.Drawing.Point(3, 548)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox3.Size = New System.Drawing.Size(680, 143)
        Me.GroupBox3.TabIndex = 342
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Filter Data"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(137, 90)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 13)
        Me.Label3.TabIndex = 359
        Me.Label3.Text = "Bulan"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(283, 90)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 358
        Me.Label2.Text = "Tahun"
        '
        'cmbParameterBarang
        '
        Me.cmbParameterBarang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbParameterBarang.FormattingEnabled = True
        Me.cmbParameterBarang.Location = New System.Drawing.Point(140, 57)
        Me.cmbParameterBarang.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.cmbParameterBarang.Name = "cmbParameterBarang"
        Me.cmbParameterBarang.Size = New System.Drawing.Size(112, 21)
        Me.cmbParameterBarang.TabIndex = 357
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(597, 110)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(81, 27)
        Me.Btn_Refresh.TabIndex = 356
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(26, 61)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(55, 13)
        Me.Label7.TabIndex = 355
        Me.Label7.Text = "Parameter"
        '
        'Cmb_KategoriBesar
        '
        Me.Cmb_KategoriBesar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_KategoriBesar.FormattingEnabled = True
        Me.Cmb_KategoriBesar.Location = New System.Drawing.Point(140, 31)
        Me.Cmb_KategoriBesar.Name = "Cmb_KategoriBesar"
        Me.Cmb_KategoriBesar.Size = New System.Drawing.Size(151, 21)
        Me.Cmb_KategoriBesar.TabIndex = 352
        '
        'Cmb_KategoriKecil
        '
        Me.Cmb_KategoriKecil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_KategoriKecil.FormattingEnabled = True
        Me.Cmb_KategoriKecil.Location = New System.Drawing.Point(297, 31)
        Me.Cmb_KategoriKecil.Name = "Cmb_KategoriKecil"
        Me.Cmb_KategoriKecil.Size = New System.Drawing.Size(202, 21)
        Me.Cmb_KategoriKecil.TabIndex = 353
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(26, 34)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 13)
        Me.Label9.TabIndex = 354
        Me.Label9.Text = "Kategori Kecil"
        '
        'ListView2
        '
        Me.ListView2.FullRowSelect = True
        Me.ListView2.GridLines = True
        Me.ListView2.HideSelection = False
        Me.ListView2.Location = New System.Drawing.Point(773, 84)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(359, 89)
        Me.ListView2.TabIndex = 351
        Me.ListView2.UseCompatibleStateImageBehavior = False
        Me.ListView2.View = System.Windows.Forms.View.Details
        '
        'Cb_ParamBrg
        '
        Me.Cb_ParamBrg.AutoSize = True
        Me.Cb_ParamBrg.Location = New System.Drawing.Point(8, 15)
        Me.Cb_ParamBrg.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cb_ParamBrg.Name = "Cb_ParamBrg"
        Me.Cb_ParamBrg.Size = New System.Drawing.Size(111, 17)
        Me.Cb_ParamBrg.TabIndex = 350
        Me.Cb_ParamBrg.Text = "Parameter Barang"
        Me.Cb_ParamBrg.UseVisualStyleBackColor = True
        '
        'Txt_KdBrg
        '
        Me.Txt_KdBrg.Location = New System.Drawing.Point(259, 57)
        Me.Txt_KdBrg.Name = "Txt_KdBrg"
        Me.Txt_KdBrg.Size = New System.Drawing.Size(240, 20)
        Me.Txt_KdBrg.TabIndex = 347
        '
        'Cmb_Tahun
        '
        Me.Cmb_Tahun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Tahun.FormattingEnabled = True
        Me.Cmb_Tahun.Location = New System.Drawing.Point(325, 87)
        Me.Cmb_Tahun.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Tahun.Name = "Cmb_Tahun"
        Me.Cmb_Tahun.Size = New System.Drawing.Size(93, 21)
        Me.Cmb_Tahun.TabIndex = 344
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(510, 110)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(81, 27)
        Me.Btn_Cari.TabIndex = 343
        Me.Btn_Cari.Text = "&Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Txt_ParamLain
        '
        Me.Txt_ParamLain.Location = New System.Drawing.Point(294, 114)
        Me.Txt_ParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_ParamLain.Name = "Txt_ParamLain"
        Me.Txt_ParamLain.Size = New System.Drawing.Size(205, 20)
        Me.Txt_ParamLain.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(256, 119)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(34, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Value"
        '
        'Cmb_ParamLain
        '
        Me.Cmb_ParamLain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_ParamLain.FormattingEnabled = True
        Me.Cmb_ParamLain.Location = New System.Drawing.Point(140, 114)
        Me.Cmb_ParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_ParamLain.Name = "Cmb_ParamLain"
        Me.Cmb_ParamLain.Size = New System.Drawing.Size(112, 21)
        Me.Cmb_ParamLain.TabIndex = 6
        '
        'Cb_ParamLain
        '
        Me.Cb_ParamLain.AutoSize = True
        Me.Cb_ParamLain.Location = New System.Drawing.Point(8, 116)
        Me.Cb_ParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cb_ParamLain.Name = "Cb_ParamLain"
        Me.Cb_ParamLain.Size = New System.Drawing.Size(97, 17)
        Me.Cb_ParamLain.TabIndex = 5
        Me.Cb_ParamLain.Text = "Parameter Lain"
        Me.Cb_ParamLain.UseVisualStyleBackColor = True
        '
        'Cb_ParamTgl
        '
        Me.Cb_ParamTgl.AutoSize = True
        Me.Cb_ParamTgl.Location = New System.Drawing.Point(8, 87)
        Me.Cb_ParamTgl.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cb_ParamTgl.Name = "Cb_ParamTgl"
        Me.Cb_ParamTgl.Size = New System.Drawing.Size(116, 17)
        Me.Cb_ParamTgl.TabIndex = 1
        Me.Cb_ParamTgl.Text = "Parameter Tanggal"
        Me.Cb_ParamTgl.UseVisualStyleBackColor = True
        '
        'Cmb_Bulan
        '
        Me.Cmb_Bulan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Bulan.FormattingEnabled = True
        Me.Cmb_Bulan.Location = New System.Drawing.Point(175, 86)
        Me.Cmb_Bulan.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Bulan.Name = "Cmb_Bulan"
        Me.Cmb_Bulan.Size = New System.Drawing.Size(97, 21)
        Me.Cmb_Bulan.TabIndex = 2
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1268, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'EMI_Display_Log_MaterialRequsition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1268, 705)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Lv_PR)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Display_Log_MaterialRequsition"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Lv_PR As ListView
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Txt_ParamLain As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Cmb_ParamLain As ComboBox
    Friend WithEvents Cb_ParamLain As CheckBox
    Friend WithEvents Cb_ParamTgl As CheckBox
    Friend WithEvents Cmb_Bulan As ComboBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Cmb_Tahun As ComboBox
    Friend WithEvents Cb_ParamBrg As CheckBox
    Friend WithEvents ListView2 As ListView
    Friend WithEvents Cmb_KategoriBesar As ComboBox
    Friend WithEvents Cmb_KategoriKecil As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbParameterBarang As ComboBox
    Friend WithEvents Txt_KdBrg As TextBox
End Class
