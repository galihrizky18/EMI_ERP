<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Display_Log_ForecastOrder
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
        Me.Lv_PR = New System.Windows.Forms.ListView()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Lv_PRDetail = New System.Windows.Forms.ListView()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cmbParamBarang = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BtnBarangMasuk_Cari = New System.Windows.Forms.Button()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Cmb_KategoriBesar = New System.Windows.Forms.ComboBox()
        Me.Cmb_KategoriKecil = New System.Windows.Forms.ComboBox()
        Me.Txt_KdBrg = New System.Windows.Forms.TextBox()
        Me.Cb_ParamBarang = New System.Windows.Forms.CheckBox()
        Me.Cmb_Tahun = New System.Windows.Forms.ComboBox()
        Me.Cmb_Bulan = New System.Windows.Forms.ComboBox()
        Me.Txt_ParamLain = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Cmb_ParamLain = New System.Windows.Forms.ComboBox()
        Me.Cb_ParamLain = New System.Windows.Forms.CheckBox()
        Me.Cb_ParamTgl = New System.Windows.Forms.CheckBox()
        Me.Lv_DataBarang = New System.Windows.Forms.ListView()
        Me.ComboBox6 = New System.Windows.Forms.ComboBox()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
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
        Me.Panel1.Size = New System.Drawing.Size(1191, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1191, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(316, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Display - Log Forecast Order"
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
        Me.Panel3.Size = New System.Drawing.Size(19, 673)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1169, 64)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 491)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(1235, 566)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1436, 15)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'Lv_PR
        '
        Me.Lv_PR.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Lv_PR.FullRowSelect = True
        Me.Lv_PR.GridLines = True
        Me.Lv_PR.HideSelection = False
        Me.Lv_PR.Location = New System.Drawing.Point(21, 64)
        Me.Lv_PR.Name = "Lv_PR"
        Me.Lv_PR.Size = New System.Drawing.Size(1146, 464)
        Me.Lv_PR.TabIndex = 234
        Me.Lv_PR.UseCompatibleStateImageBehavior = False
        Me.Lv_PR.View = System.Windows.Forms.View.Details
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(21, 703)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1436, 15)
        Me.Panel6.TabIndex = 39
        Me.Panel6.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(21, 530)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1436, 18)
        Me.Panel7.TabIndex = 39
        Me.Panel7.Visible = False
        '
        'Lv_PRDetail
        '
        Me.Lv_PRDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Lv_PRDetail.FullRowSelect = True
        Me.Lv_PRDetail.GridLines = True
        Me.Lv_PRDetail.HideSelection = False
        Me.Lv_PRDetail.Location = New System.Drawing.Point(1213, 596)
        Me.Lv_PRDetail.Name = "Lv_PRDetail"
        Me.Lv_PRDetail.Size = New System.Drawing.Size(900, 220)
        Me.Lv_PRDetail.TabIndex = 341
        Me.Lv_PRDetail.UseCompatibleStateImageBehavior = False
        Me.Lv_PRDetail.View = System.Windows.Forms.View.Details
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmbParamBarang)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.BtnBarangMasuk_Cari)
        Me.GroupBox3.Controls.Add(Me.Btn_Refresh)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Cmb_KategoriBesar)
        Me.GroupBox3.Controls.Add(Me.Cmb_KategoriKecil)
        Me.GroupBox3.Controls.Add(Me.Txt_KdBrg)
        Me.GroupBox3.Controls.Add(Me.Cb_ParamBarang)
        Me.GroupBox3.Controls.Add(Me.Cmb_Tahun)
        Me.GroupBox3.Controls.Add(Me.Cmb_Bulan)
        Me.GroupBox3.Controls.Add(Me.Txt_ParamLain)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Cmb_ParamLain)
        Me.GroupBox3.Controls.Add(Me.Cb_ParamLain)
        Me.GroupBox3.Controls.Add(Me.Cb_ParamTgl)
        Me.GroupBox3.Location = New System.Drawing.Point(21, 549)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox3.Size = New System.Drawing.Size(783, 154)
        Me.GroupBox3.TabIndex = 342
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Filter Data"
        '
        'cmbParamBarang
        '
        Me.cmbParamBarang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbParamBarang.FormattingEnabled = True
        Me.cmbParamBarang.Location = New System.Drawing.Point(141, 68)
        Me.cmbParamBarang.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.cmbParamBarang.Name = "cmbParamBarang"
        Me.cmbParamBarang.Size = New System.Drawing.Size(113, 21)
        Me.cmbParamBarang.TabIndex = 353
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(138, 97)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 13)
        Me.Label3.TabIndex = 352
        Me.Label3.Text = "Bulan"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(322, 97)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 351
        Me.Label2.Text = "Tahun"
        '
        'BtnBarangMasuk_Cari
        '
        Me.BtnBarangMasuk_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnBarangMasuk_Cari.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnBarangMasuk_Cari.ForeColor = System.Drawing.Color.White
        Me.BtnBarangMasuk_Cari.Location = New System.Drawing.Point(580, 120)
        Me.BtnBarangMasuk_Cari.Name = "BtnBarangMasuk_Cari"
        Me.BtnBarangMasuk_Cari.Size = New System.Drawing.Size(90, 27)
        Me.BtnBarangMasuk_Cari.TabIndex = 343
        Me.BtnBarangMasuk_Cari.Text = "&Cari"
        Me.BtnBarangMasuk_Cari.UseVisualStyleBackColor = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(676, 120)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(90, 27)
        Me.Btn_Refresh.TabIndex = 350
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(26, 70)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 13)
        Me.Label5.TabIndex = 349
        Me.Label5.Text = "Parameter"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(26, 45)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 13)
        Me.Label4.TabIndex = 345
        Me.Label4.Text = "Kategori Kecil"
        '
        'Cmb_KategoriBesar
        '
        Me.Cmb_KategoriBesar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_KategoriBesar.FormattingEnabled = True
        Me.Cmb_KategoriBesar.Location = New System.Drawing.Point(141, 41)
        Me.Cmb_KategoriBesar.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_KategoriBesar.Name = "Cmb_KategoriBesar"
        Me.Cmb_KategoriBesar.Size = New System.Drawing.Size(174, 21)
        Me.Cmb_KategoriBesar.TabIndex = 347
        '
        'Cmb_KategoriKecil
        '
        Me.Cmb_KategoriKecil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_KategoriKecil.FormattingEnabled = True
        Me.Cmb_KategoriKecil.Location = New System.Drawing.Point(319, 41)
        Me.Cmb_KategoriKecil.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_KategoriKecil.Name = "Cmb_KategoriKecil"
        Me.Cmb_KategoriKecil.Size = New System.Drawing.Size(256, 21)
        Me.Cmb_KategoriKecil.TabIndex = 346
        '
        'Txt_KdBrg
        '
        Me.Txt_KdBrg.Location = New System.Drawing.Point(258, 68)
        Me.Txt_KdBrg.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_KdBrg.Name = "Txt_KdBrg"
        Me.Txt_KdBrg.Size = New System.Drawing.Size(317, 20)
        Me.Txt_KdBrg.TabIndex = 347
        '
        'Cb_ParamBarang
        '
        Me.Cb_ParamBarang.AutoSize = True
        Me.Cb_ParamBarang.Location = New System.Drawing.Point(8, 24)
        Me.Cb_ParamBarang.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cb_ParamBarang.Name = "Cb_ParamBarang"
        Me.Cb_ParamBarang.Size = New System.Drawing.Size(111, 17)
        Me.Cb_ParamBarang.TabIndex = 343
        Me.Cb_ParamBarang.Text = "Parameter Barang"
        Me.Cb_ParamBarang.UseVisualStyleBackColor = True
        '
        'Cmb_Tahun
        '
        Me.Cmb_Tahun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Tahun.FormattingEnabled = True
        Me.Cmb_Tahun.Location = New System.Drawing.Point(364, 94)
        Me.Cmb_Tahun.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Tahun.Name = "Cmb_Tahun"
        Me.Cmb_Tahun.Size = New System.Drawing.Size(115, 21)
        Me.Cmb_Tahun.TabIndex = 345
        '
        'Cmb_Bulan
        '
        Me.Cmb_Bulan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Bulan.FormattingEnabled = True
        Me.Cmb_Bulan.Location = New System.Drawing.Point(176, 94)
        Me.Cmb_Bulan.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Bulan.Name = "Cmb_Bulan"
        Me.Cmb_Bulan.Size = New System.Drawing.Size(139, 21)
        Me.Cmb_Bulan.TabIndex = 343
        '
        'Txt_ParamLain
        '
        Me.Txt_ParamLain.Location = New System.Drawing.Point(316, 123)
        Me.Txt_ParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_ParamLain.Name = "Txt_ParamLain"
        Me.Txt_ParamLain.Size = New System.Drawing.Size(259, 20)
        Me.Txt_ParamLain.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(278, 127)
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
        Me.Cmb_ParamLain.Location = New System.Drawing.Point(141, 122)
        Me.Cmb_ParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_ParamLain.Name = "Cmb_ParamLain"
        Me.Cmb_ParamLain.Size = New System.Drawing.Size(132, 21)
        Me.Cmb_ParamLain.TabIndex = 6
        '
        'Cb_ParamLain
        '
        Me.Cb_ParamLain.AutoSize = True
        Me.Cb_ParamLain.Location = New System.Drawing.Point(7, 125)
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
        Me.Cb_ParamTgl.Location = New System.Drawing.Point(7, 97)
        Me.Cb_ParamTgl.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cb_ParamTgl.Name = "Cb_ParamTgl"
        Me.Cb_ParamTgl.Size = New System.Drawing.Size(116, 17)
        Me.Cb_ParamTgl.TabIndex = 1
        Me.Cb_ParamTgl.Text = "Parameter Tanggal"
        Me.Cb_ParamTgl.UseVisualStyleBackColor = True
        '
        'Lv_DataBarang
        '
        Me.Lv_DataBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Lv_DataBarang.FullRowSelect = True
        Me.Lv_DataBarang.GridLines = True
        Me.Lv_DataBarang.HideSelection = False
        Me.Lv_DataBarang.Location = New System.Drawing.Point(1041, 663)
        Me.Lv_DataBarang.Name = "Lv_DataBarang"
        Me.Lv_DataBarang.Size = New System.Drawing.Size(434, 100)
        Me.Lv_DataBarang.TabIndex = 343
        Me.Lv_DataBarang.UseCompatibleStateImageBehavior = False
        Me.Lv_DataBarang.View = System.Windows.Forms.View.Details
        '
        'ComboBox6
        '
        Me.ComboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox6.FormattingEnabled = True
        Me.ComboBox6.Location = New System.Drawing.Point(965, 557)
        Me.ComboBox6.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ComboBox6.Name = "ComboBox6"
        Me.ComboBox6.Size = New System.Drawing.Size(135, 21)
        Me.ComboBox6.TabIndex = 342
        Me.ComboBox6.Visible = False
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(1104, 632)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(81, 20)
        Me.DateTimePicker2.TabIndex = 4
        Me.DateTimePicker2.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(1107, 611)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(23, 13)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "s/d"
        Me.Label7.Visible = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(1104, 587)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(81, 20)
        Me.DateTimePicker1.TabIndex = 3
        Me.DateTimePicker1.Visible = False
        '
        'ComboBox3
        '
        Me.ComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Location = New System.Drawing.Point(1104, 557)
        Me.ComboBox3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(81, 21)
        Me.ComboBox3.TabIndex = 2
        Me.ComboBox3.Visible = False
        '
        'EMI_Display_Log_ForecastOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1191, 718)
        Me.Controls.Add(Me.Lv_DataBarang)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Lv_PRDetail)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Lv_PR)
        Me.Controls.Add(Me.ComboBox6)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ComboBox3)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Display_Log_ForecastOrder"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Lv_PR As ListView
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Lv_PRDetail As ListView
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents ComboBox6 As ComboBox
    Friend WithEvents Txt_ParamLain As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Cmb_ParamLain As ComboBox
    Friend WithEvents Cb_ParamLain As CheckBox
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Cb_ParamTgl As CheckBox
    Friend WithEvents ComboBox3 As ComboBox
    Friend WithEvents BtnBarangMasuk_Cari As Button
    Friend WithEvents Cmb_Bulan As ComboBox
    Friend WithEvents Cmb_Tahun As ComboBox
    Friend WithEvents Cb_ParamBarang As CheckBox
    Friend WithEvents Txt_KdBrg As TextBox
    Friend WithEvents Lv_DataBarang As ListView
    Friend WithEvents Cmb_KategoriBesar As ComboBox
    Friend WithEvents Cmb_KategoriKecil As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbParamBarang As ComboBox
End Class
