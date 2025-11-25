<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Laporan_Pemakaian_Stock_Barang_Lain
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
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Cmb_JenisPengguna = New System.Windows.Forms.ComboBox()
        Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
        Me.Cmb_ParamLain = New System.Windows.Forms.ComboBox()
        Me.Txt_IdPengguna = New System.Windows.Forms.TextBox()
        Me.Txt_IdCostCenter = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
        Me.Txt_Pengguna = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_NmCostCenter = New System.Windows.Forms.TextBox()
        Me.Txt_ParamLain = New System.Windows.Forms.TextBox()
        Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.BtnCetak = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.Lv_Barang = New System.Windows.Forms.ListView()
        Me.Lv_CostCenter = New System.Windows.Forms.ListView()
        Me.Lv_Pengguna = New System.Windows.Forms.ListView()
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
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(644, 43)
        Me.Panel1.TabIndex = 29
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 41)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(644, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(13, 7)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(328, 28)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Laporan - Pemakaian Stock Asset"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(0, 42)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(942, 12)
        Me.Panel5.TabIndex = 44
        Me.Panel5.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 54)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 601)
        Me.Panel3.TabIndex = 45
        Me.Panel3.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(21, 313)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(942, 15)
        Me.Panel4.TabIndex = 46
        Me.Panel4.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Cmb_JenisPengguna)
        Me.GroupBox1.Controls.Add(Me.Cmb_Lokasi)
        Me.GroupBox1.Controls.Add(Me.Cmb_ParamLain)
        Me.GroupBox1.Controls.Add(Me.Txt_IdPengguna)
        Me.GroupBox1.Controls.Add(Me.Txt_IdCostCenter)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Txt_KdBarang)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Tgl2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Tgl1)
        Me.GroupBox1.Controls.Add(Me.Txt_Pengguna)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Txt_NmCostCenter)
        Me.GroupBox1.Controls.Add(Me.Txt_ParamLain)
        Me.GroupBox1.Controls.Add(Me.Txt_NmBarang)
        Me.GroupBox1.Location = New System.Drawing.Point(19, 48)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(606, 227)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Cmb_JenisPengguna
        '
        Me.Cmb_JenisPengguna.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_JenisPengguna.FormattingEnabled = True
        Me.Cmb_JenisPengguna.Location = New System.Drawing.Point(105, 104)
        Me.Cmb_JenisPengguna.Name = "Cmb_JenisPengguna"
        Me.Cmb_JenisPengguna.Size = New System.Drawing.Size(163, 24)
        Me.Cmb_JenisPengguna.TabIndex = 2
        '
        'Cmb_Lokasi
        '
        Me.Cmb_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lokasi.FormattingEnabled = True
        Me.Cmb_Lokasi.Location = New System.Drawing.Point(105, 48)
        Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
        Me.Cmb_Lokasi.Size = New System.Drawing.Size(163, 24)
        Me.Cmb_Lokasi.TabIndex = 2
        '
        'Cmb_ParamLain
        '
        Me.Cmb_ParamLain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_ParamLain.FormattingEnabled = True
        Me.Cmb_ParamLain.Location = New System.Drawing.Point(105, 186)
        Me.Cmb_ParamLain.Name = "Cmb_ParamLain"
        Me.Cmb_ParamLain.Size = New System.Drawing.Size(163, 24)
        Me.Cmb_ParamLain.TabIndex = 7
        '
        'Txt_IdPengguna
        '
        Me.Txt_IdPengguna.Enabled = False
        Me.Txt_IdPengguna.Location = New System.Drawing.Point(105, 134)
        Me.Txt_IdPengguna.Name = "Txt_IdPengguna"
        Me.Txt_IdPengguna.Size = New System.Drawing.Size(55, 20)
        Me.Txt_IdPengguna.TabIndex = 3
        '
        'Txt_IdCostCenter
        '
        Me.Txt_IdCostCenter.Location = New System.Drawing.Point(105, 78)
        Me.Txt_IdCostCenter.Name = "Txt_IdCostCenter"
        Me.Txt_IdCostCenter.Size = New System.Drawing.Size(55, 20)
        Me.Txt_IdCostCenter.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 107)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(91, 16)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Jenis Pengguna"
        '
        'Txt_KdBarang
        '
        Me.Txt_KdBarang.Location = New System.Drawing.Point(105, 160)
        Me.Txt_KdBarang.Name = "Txt_KdBarang"
        Me.Txt_KdBarang.Size = New System.Drawing.Size(163, 20)
        Me.Txt_KdBarang.TabIndex = 5
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 137)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(59, 16)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Pengguna"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 16)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Lokasi"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 81)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(73, 16)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Cost Center"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 189)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 16)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Lainnya"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 163)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 16)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Barang"
        '
        'Tgl2
        '
        Me.Tgl2.CustomFormat = "dd MMMM yyyy"
        Me.Tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl2.Location = New System.Drawing.Point(316, 22)
        Me.Tgl2.Name = "Tgl2"
        Me.Tgl2.Size = New System.Drawing.Size(163, 20)
        Me.Tgl2.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(278, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "s/d"
        '
        'Tgl1
        '
        Me.Tgl1.CustomFormat = "dd MMMM yyyy"
        Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl1.Location = New System.Drawing.Point(105, 22)
        Me.Tgl1.Name = "Tgl1"
        Me.Tgl1.Size = New System.Drawing.Size(163, 20)
        Me.Tgl1.TabIndex = 0
        '
        'Txt_Pengguna
        '
        Me.Txt_Pengguna.Enabled = False
        Me.Txt_Pengguna.Location = New System.Drawing.Point(163, 134)
        Me.Txt_Pengguna.Name = "Txt_Pengguna"
        Me.Txt_Pengguna.Size = New System.Drawing.Size(428, 20)
        Me.Txt_Pengguna.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Periode"
        '
        'Txt_NmCostCenter
        '
        Me.Txt_NmCostCenter.Location = New System.Drawing.Point(163, 78)
        Me.Txt_NmCostCenter.Name = "Txt_NmCostCenter"
        Me.Txt_NmCostCenter.Size = New System.Drawing.Size(428, 20)
        Me.Txt_NmCostCenter.TabIndex = 4
        '
        'Txt_ParamLain
        '
        Me.Txt_ParamLain.Enabled = False
        Me.Txt_ParamLain.Location = New System.Drawing.Point(272, 189)
        Me.Txt_ParamLain.Name = "Txt_ParamLain"
        Me.Txt_ParamLain.Size = New System.Drawing.Size(319, 20)
        Me.Txt_ParamLain.TabIndex = 8
        '
        'Txt_NmBarang
        '
        Me.Txt_NmBarang.Location = New System.Drawing.Point(272, 160)
        Me.Txt_NmBarang.Name = "Txt_NmBarang"
        Me.Txt_NmBarang.Size = New System.Drawing.Size(319, 20)
        Me.Txt_NmBarang.TabIndex = 6
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(626, 98)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(19, 601)
        Me.Panel2.TabIndex = 45
        Me.Panel2.Visible = False
        '
        'BtnCetak
        '
        Me.BtnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnCetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnCetak.ForeColor = System.Drawing.Color.White
        Me.BtnCetak.Location = New System.Drawing.Point(443, 281)
        Me.BtnCetak.Name = "BtnCetak"
        Me.BtnCetak.Size = New System.Drawing.Size(84, 33)
        Me.BtnCetak.TabIndex = 1
        Me.BtnCetak.Text = "&Cetak"
        Me.BtnCetak.UseVisualStyleBackColor = False
        '
        'BtnExit
        '
        Me.BtnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnExit.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnExit.ForeColor = System.Drawing.Color.White
        Me.BtnExit.Location = New System.Drawing.Point(526, 281)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(84, 33)
        Me.BtnExit.TabIndex = 2
        Me.BtnExit.Text = "&Keluar"
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'Lv_Barang
        '
        Me.Lv_Barang.BackColor = System.Drawing.Color.White
        Me.Lv_Barang.FullRowSelect = True
        Me.Lv_Barang.GridLines = True
        Me.Lv_Barang.HideSelection = False
        Me.Lv_Barang.Location = New System.Drawing.Point(650, 230)
        Me.Lv_Barang.Name = "Lv_Barang"
        Me.Lv_Barang.Size = New System.Drawing.Size(485, 200)
        Me.Lv_Barang.TabIndex = 50
        Me.Lv_Barang.UseCompatibleStateImageBehavior = False
        Me.Lv_Barang.View = System.Windows.Forms.View.Details
        Me.Lv_Barang.Visible = False
        '
        'Lv_CostCenter
        '
        Me.Lv_CostCenter.BackColor = System.Drawing.Color.White
        Me.Lv_CostCenter.FullRowSelect = True
        Me.Lv_CostCenter.GridLines = True
        Me.Lv_CostCenter.HideSelection = False
        Me.Lv_CostCenter.Location = New System.Drawing.Point(650, 148)
        Me.Lv_CostCenter.Name = "Lv_CostCenter"
        Me.Lv_CostCenter.Size = New System.Drawing.Size(485, 200)
        Me.Lv_CostCenter.TabIndex = 50
        Me.Lv_CostCenter.UseCompatibleStateImageBehavior = False
        Me.Lv_CostCenter.View = System.Windows.Forms.View.Details
        Me.Lv_CostCenter.Visible = False
        '
        'Lv_Pengguna
        '
        Me.Lv_Pengguna.BackColor = System.Drawing.Color.White
        Me.Lv_Pengguna.FullRowSelect = True
        Me.Lv_Pengguna.GridLines = True
        Me.Lv_Pengguna.HideSelection = False
        Me.Lv_Pengguna.Location = New System.Drawing.Point(650, 205)
        Me.Lv_Pengguna.Name = "Lv_Pengguna"
        Me.Lv_Pengguna.Size = New System.Drawing.Size(485, 200)
        Me.Lv_Pengguna.TabIndex = 50
        Me.Lv_Pengguna.UseCompatibleStateImageBehavior = False
        Me.Lv_Pengguna.View = System.Windows.Forms.View.Details
        Me.Lv_Pengguna.Visible = False
        '
        'N_EMI_Laporan_Pemakaian_Stock_Barang_Lain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(644, 328)
        Me.Controls.Add(Me.Lv_Barang)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Lv_Pengguna)
        Me.Controls.Add(Me.Lv_CostCenter)
        Me.Controls.Add(Me.BtnCetak)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_Laporan_Pemakaian_Stock_Barang_Lain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Cmb_ParamLain As ComboBox
    Friend WithEvents Txt_IdCostCenter As TextBox
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Tgl2 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_NmCostCenter As TextBox
    Friend WithEvents Txt_ParamLain As TextBox
    Friend WithEvents Txt_NmBarang As TextBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents BtnCetak As Button
    Friend WithEvents BtnExit As Button
    Friend WithEvents Cmb_Lokasi As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Lv_Barang As ListView
    Friend WithEvents Lv_CostCenter As ListView
    Friend WithEvents Cmb_JenisPengguna As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Txt_IdPengguna As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Txt_Pengguna As TextBox
    Friend WithEvents Lv_Pengguna As ListView
End Class
