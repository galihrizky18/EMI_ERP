<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Budgeting_Per_CostCenter
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Dgv_Data = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Txt_Filter_Value = New System.Windows.Forms.TextBox()
        Me.Cmb_Filter = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Btn_Release = New System.Windows.Forms.Button()
        Me.Txt_NoFaktur = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
        Me.Cmb_Bulan = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Cmb_Tahun = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.kode_account = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.formula = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.kode_detailAcc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.flag_budgeting = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.Dgv_Data, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1191, 51)
        Me.Panel1.TabIndex = 24
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 12)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(340, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Budgeting Akun Per Cost Cnter"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(1, 51)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1312, 10)
        Me.Panel6.TabIndex = 38
        Me.Panel6.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 62)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(14, 619)
        Me.Panel3.TabIndex = 39
        Me.Panel3.Visible = False
        '
        'Dgv_Data
        '
        Me.Dgv_Data.AllowUserToAddRows = False
        Me.Dgv_Data.BackgroundColor = System.Drawing.Color.White
        Me.Dgv_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Dgv_Data.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.kode_account, Me.formula, Me.kode_detailAcc, Me.flag_budgeting})
        Me.Dgv_Data.Location = New System.Drawing.Point(16, 180)
        Me.Dgv_Data.Name = "Dgv_Data"
        Me.Dgv_Data.Size = New System.Drawing.Size(1163, 367)
        Me.Dgv_Data.TabIndex = 423
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(1177, 67)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(14, 619)
        Me.Panel2.TabIndex = 40
        Me.Panel2.Visible = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(136, 558)
        Me.Btn_Refresh.Margin = New System.Windows.Forms.Padding(2)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(115, 35)
        Me.Btn_Refresh.TabIndex = 458
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(17, 557)
        Me.Btn_Simpan.Margin = New System.Windows.Forms.Padding(2)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(115, 35)
        Me.Btn_Simpan.TabIndex = 459
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(18, 594)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1312, 10)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(18, 547)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1312, 10)
        Me.Panel5.TabIndex = 38
        Me.Panel5.Visible = False
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(532, 151)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(79, 30)
        Me.Btn_Cari.TabIndex = 464
        Me.Btn_Cari.Text = "Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Txt_Filter_Value
        '
        Me.Txt_Filter_Value.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Filter_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Filter_Value.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Filter_Value.Location = New System.Drawing.Point(294, 156)
        Me.Txt_Filter_Value.MaxLength = 50
        Me.Txt_Filter_Value.Name = "Txt_Filter_Value"
        Me.Txt_Filter_Value.Size = New System.Drawing.Size(232, 20)
        Me.Txt_Filter_Value.TabIndex = 463
        '
        'Cmb_Filter
        '
        Me.Cmb_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Filter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_Filter.FormattingEnabled = True
        Me.Cmb_Filter.Location = New System.Drawing.Point(124, 155)
        Me.Cmb_Filter.Name = "Cmb_Filter"
        Me.Cmb_Filter.Size = New System.Drawing.Size(164, 21)
        Me.Cmb_Filter.TabIndex = 462
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(18, 158)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 16)
        Me.Label2.TabIndex = 461
        Me.Label2.Text = "Filter"
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(18, 141)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1312, 10)
        Me.Panel7.TabIndex = 38
        Me.Panel7.Visible = False
        '
        'Btn_Release
        '
        Me.Btn_Release.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Release.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Release.ForeColor = System.Drawing.Color.White
        Me.Btn_Release.Location = New System.Drawing.Point(255, 558)
        Me.Btn_Release.Margin = New System.Windows.Forms.Padding(2)
        Me.Btn_Release.Name = "Btn_Release"
        Me.Btn_Release.Size = New System.Drawing.Size(115, 35)
        Me.Btn_Release.TabIndex = 458
        Me.Btn_Release.Text = "&Release"
        Me.Btn_Release.UseVisualStyleBackColor = False
        Me.Btn_Release.Visible = False
        '
        'Txt_NoFaktur
        '
        Me.Txt_NoFaktur.BackColor = System.Drawing.Color.Goldenrod
        Me.Txt_NoFaktur.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_NoFaktur.ForeColor = System.Drawing.SystemColors.Window
        Me.Txt_NoFaktur.Location = New System.Drawing.Point(124, 60)
        Me.Txt_NoFaktur.MaxLength = 20
        Me.Txt_NoFaktur.Name = "Txt_NoFaktur"
        Me.Txt_NoFaktur.ReadOnly = True
        Me.Txt_NoFaktur.Size = New System.Drawing.Size(216, 21)
        Me.Txt_NoFaktur.TabIndex = 467
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(18, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 17)
        Me.Label3.TabIndex = 466
        Me.Label3.Text = "No. Transaksi"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label7.Location = New System.Drawing.Point(18, 87)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 17)
        Me.Label7.TabIndex = 472
        Me.Label7.Text = "Lokasi"
        '
        'Cmb_Lokasi
        '
        Me.Cmb_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lokasi.FormattingEnabled = True
        Me.Cmb_Lokasi.Location = New System.Drawing.Point(124, 87)
        Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
        Me.Cmb_Lokasi.Size = New System.Drawing.Size(335, 21)
        Me.Cmb_Lokasi.TabIndex = 473
        '
        'Cmb_Bulan
        '
        Me.Cmb_Bulan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Bulan.FormattingEnabled = True
        Me.Cmb_Bulan.Location = New System.Drawing.Point(124, 114)
        Me.Cmb_Bulan.Name = "Cmb_Bulan"
        Me.Cmb_Bulan.Size = New System.Drawing.Size(127, 21)
        Me.Cmb_Bulan.TabIndex = 473
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(18, 115)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 17)
        Me.Label5.TabIndex = 472
        Me.Label5.Text = "Bulan"
        '
        'Cmb_Tahun
        '
        Me.Cmb_Tahun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Tahun.FormattingEnabled = True
        Me.Cmb_Tahun.Location = New System.Drawing.Point(332, 114)
        Me.Cmb_Tahun.Name = "Cmb_Tahun"
        Me.Cmb_Tahun.Size = New System.Drawing.Size(127, 21)
        Me.Cmb_Tahun.TabIndex = 473
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(270, 115)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 17)
        Me.Label6.TabIndex = 472
        Me.Label6.Text = "Tahun"
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
        'kode_account
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.kode_account.DefaultCellStyle = DataGridViewCellStyle3
        Me.kode_account.Frozen = True
        Me.kode_account.HeaderText = "Kode Account"
        Me.kode_account.Name = "kode_account"
        Me.kode_account.ReadOnly = True
        Me.kode_account.Width = 160
        '
        'formula
        '
        Me.formula.Frozen = True
        Me.formula.HeaderText = "Formula"
        Me.formula.Name = "formula"
        Me.formula.ReadOnly = True
        Me.formula.Visible = False
        '
        'kode_detailAcc
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.kode_detailAcc.DefaultCellStyle = DataGridViewCellStyle4
        Me.kode_detailAcc.Frozen = True
        Me.kode_detailAcc.HeaderText = "Account"
        Me.kode_detailAcc.Name = "kode_detailAcc"
        Me.kode_detailAcc.ReadOnly = True
        Me.kode_detailAcc.Width = 200
        '
        'flag_budgeting
        '
        Me.flag_budgeting.HeaderText = "Flag_Budgeting"
        Me.flag_budgeting.Name = "flag_budgeting"
        Me.flag_budgeting.Visible = False
        '
        'Budgeting_Per_CostCenter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1191, 603)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Cmb_Tahun)
        Me.Controls.Add(Me.Cmb_Bulan)
        Me.Controls.Add(Me.Cmb_Lokasi)
        Me.Controls.Add(Me.Txt_NoFaktur)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Btn_Cari)
        Me.Controls.Add(Me.Txt_Filter_Value)
        Me.Controls.Add(Me.Cmb_Filter)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Btn_Release)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Dgv_Data)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Budgeting_Per_CostCenter"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.Dgv_Data, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Dgv_Data As DataGridView
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Txt_Filter_Value As TextBox
    Friend WithEvents Cmb_Filter As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Btn_Release As Button
    Friend WithEvents Txt_NoFaktur As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Cmb_Lokasi As ComboBox
    Friend WithEvents Cmb_Bulan As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Cmb_Tahun As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents kode_account As DataGridViewTextBoxColumn
    Friend WithEvents formula As DataGridViewTextBoxColumn
    Friend WithEvents kode_detailAcc As DataGridViewTextBoxColumn
    Friend WithEvents flag_budgeting As DataGridViewTextBoxColumn
End Class
