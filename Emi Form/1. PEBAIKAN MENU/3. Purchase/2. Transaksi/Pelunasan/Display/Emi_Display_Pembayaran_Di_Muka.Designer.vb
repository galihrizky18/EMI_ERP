<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Emi_Display_Pembayaran_Di_Muka
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
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Lv_DataDP = New System.Windows.Forms.ListView()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.CbTransaksi_HrIni = New System.Windows.Forms.CheckBox()
        Me.btnCari = New System.Windows.Forms.Button()
        Me.TxtValue = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbParamLain = New System.Windows.Forms.ComboBox()
        Me.CbParamLain = New System.Windows.Forms.CheckBox()
        Me.DtpAkhir = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DtpAwal = New System.Windows.Forms.DateTimePicker()
        Me.CbParamTgl = New System.Windows.Forms.CheckBox()
        Me.cmbTgl = New System.Windows.Forms.ComboBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Lv_DetailDP = New System.Windows.Forms.ListView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Lv_Pajak = New System.Windows.Forms.ListView()
        Me.Panel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1184, 51)
        Me.Panel1.TabIndex = 23
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1184, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(265, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Display - Down Payment"
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Location = New System.Drawing.Point(0, 56)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(12, 489)
        Me.Panel8.TabIndex = 36
        Me.Panel8.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(19, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1073, 12)
        Me.Panel2.TabIndex = 37
        Me.Panel2.Visible = False
        '
        'Lv_DataDP
        '
        Me.Lv_DataDP.FullRowSelect = True
        Me.Lv_DataDP.GridLines = True
        Me.Lv_DataDP.HideSelection = False
        Me.Lv_DataDP.Location = New System.Drawing.Point(12, 59)
        Me.Lv_DataDP.Name = "Lv_DataDP"
        Me.Lv_DataDP.Size = New System.Drawing.Size(1160, 245)
        Me.Lv_DataDP.TabIndex = 38
        Me.Lv_DataDP.UseCompatibleStateImageBehavior = False
        Me.Lv_DataDP.View = System.Windows.Forms.View.Details
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1172, 94)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(12, 489)
        Me.Panel3.TabIndex = 36
        Me.Panel3.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(19, 689)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1073, 12)
        Me.Panel4.TabIndex = 37
        Me.Panel4.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.CbTransaksi_HrIni)
        Me.GroupBox3.Controls.Add(Me.btnCari)
        Me.GroupBox3.Controls.Add(Me.TxtValue)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.cmbParamLain)
        Me.GroupBox3.Controls.Add(Me.CbParamLain)
        Me.GroupBox3.Controls.Add(Me.DtpAkhir)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.DtpAwal)
        Me.GroupBox3.Controls.Add(Me.CbParamTgl)
        Me.GroupBox3.Controls.Add(Me.cmbTgl)
        Me.GroupBox3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 571)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(783, 117)
        Me.GroupBox3.TabIndex = 78
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Filter Data"
        '
        'CbTransaksi_HrIni
        '
        Me.CbTransaksi_HrIni.AutoSize = True
        Me.CbTransaksi_HrIni.Location = New System.Drawing.Point(12, 25)
        Me.CbTransaksi_HrIni.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.CbTransaksi_HrIni.Name = "CbTransaksi_HrIni"
        Me.CbTransaksi_HrIni.Size = New System.Drawing.Size(147, 24)
        Me.CbTransaksi_HrIni.TabIndex = 9
        Me.CbTransaksi_HrIni.Text = "Transaksi Hari Ini"
        Me.CbTransaksi_HrIni.UseVisualStyleBackColor = True
        '
        'btnCari
        '
        Me.btnCari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.btnCari.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnCari.ForeColor = System.Drawing.Color.White
        Me.btnCari.Location = New System.Drawing.Point(612, 79)
        Me.btnCari.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.btnCari.Name = "btnCari"
        Me.btnCari.Size = New System.Drawing.Size(118, 32)
        Me.btnCari.TabIndex = 8
        Me.btnCari.Text = "&Cari"
        Me.btnCari.UseVisualStyleBackColor = False
        '
        'TxtValue
        '
        Me.TxtValue.Location = New System.Drawing.Point(410, 84)
        Me.TxtValue.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.TxtValue.Name = "TxtValue"
        Me.TxtValue.Size = New System.Drawing.Size(198, 23)
        Me.TxtValue.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(366, 88)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 20)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Value"
        '
        'cmbParamLain
        '
        Me.cmbParamLain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbParamLain.FormattingEnabled = True
        Me.cmbParamLain.Location = New System.Drawing.Point(163, 84)
        Me.cmbParamLain.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.cmbParamLain.Name = "cmbParamLain"
        Me.cmbParamLain.Size = New System.Drawing.Size(196, 26)
        Me.cmbParamLain.TabIndex = 6
        '
        'CbParamLain
        '
        Me.CbParamLain.AutoSize = True
        Me.CbParamLain.Location = New System.Drawing.Point(12, 84)
        Me.CbParamLain.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.CbParamLain.Name = "CbParamLain"
        Me.CbParamLain.Size = New System.Drawing.Size(134, 24)
        Me.CbParamLain.TabIndex = 5
        Me.CbParamLain.Text = "Parameter Lain"
        Me.CbParamLain.UseVisualStyleBackColor = True
        '
        'DtpAkhir
        '
        Me.DtpAkhir.CustomFormat = "dd MMMM yyyy"
        Me.DtpAkhir.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpAkhir.Location = New System.Drawing.Point(568, 51)
        Me.DtpAkhir.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.DtpAkhir.Name = "DtpAkhir"
        Me.DtpAkhir.Size = New System.Drawing.Size(162, 23)
        Me.DtpAkhir.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(537, 55)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "s/d"
        '
        'DtpAwal
        '
        Me.DtpAwal.CustomFormat = "dd MMMM yyyy"
        Me.DtpAwal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpAwal.Location = New System.Drawing.Point(365, 51)
        Me.DtpAwal.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.DtpAwal.Name = "DtpAwal"
        Me.DtpAwal.Size = New System.Drawing.Size(162, 23)
        Me.DtpAwal.TabIndex = 3
        '
        'CbParamTgl
        '
        Me.CbParamTgl.AutoSize = True
        Me.CbParamTgl.Location = New System.Drawing.Point(12, 53)
        Me.CbParamTgl.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.CbParamTgl.Name = "CbParamTgl"
        Me.CbParamTgl.Size = New System.Drawing.Size(78, 24)
        Me.CbParamTgl.TabIndex = 1
        Me.CbParamTgl.Text = "Tanggal"
        Me.CbParamTgl.UseVisualStyleBackColor = True
        '
        'cmbTgl
        '
        Me.cmbTgl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTgl.FormattingEnabled = True
        Me.cmbTgl.Location = New System.Drawing.Point(163, 51)
        Me.cmbTgl.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.cmbTgl.Name = "cmbTgl"
        Me.cmbTgl.Size = New System.Drawing.Size(196, 26)
        Me.cmbTgl.TabIndex = 2
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(17, 557)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1073, 12)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Lv_DetailDP)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 309)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(670, 245)
        Me.GroupBox1.TabIndex = 79
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detail"
        '
        'Lv_DetailDP
        '
        Me.Lv_DetailDP.FullRowSelect = True
        Me.Lv_DetailDP.GridLines = True
        Me.Lv_DetailDP.HideSelection = False
        Me.Lv_DetailDP.Location = New System.Drawing.Point(6, 22)
        Me.Lv_DetailDP.Name = "Lv_DetailDP"
        Me.Lv_DetailDP.Size = New System.Drawing.Size(658, 214)
        Me.Lv_DetailDP.TabIndex = 38
        Me.Lv_DetailDP.UseCompatibleStateImageBehavior = False
        Me.Lv_DetailDP.View = System.Windows.Forms.View.Details
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Lv_Pajak)
        Me.GroupBox2.Location = New System.Drawing.Point(690, 310)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(484, 245)
        Me.GroupBox2.TabIndex = 79
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Pajak"
        '
        'Lv_Pajak
        '
        Me.Lv_Pajak.FullRowSelect = True
        Me.Lv_Pajak.GridLines = True
        Me.Lv_Pajak.HideSelection = False
        Me.Lv_Pajak.Location = New System.Drawing.Point(6, 21)
        Me.Lv_Pajak.Name = "Lv_Pajak"
        Me.Lv_Pajak.Size = New System.Drawing.Size(472, 214)
        Me.Lv_Pajak.TabIndex = 38
        Me.Lv_Pajak.UseCompatibleStateImageBehavior = False
        Me.Lv_Pajak.View = System.Windows.Forms.View.Details
        '
        'Emi_Display_Down_Payment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1184, 701)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Lv_DataDP)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Emi_Display_Down_Payment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Lv_DataDP As ListView
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents CbTransaksi_HrIni As CheckBox
    Friend WithEvents btnCari As Button
    Friend WithEvents TxtValue As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbParamLain As ComboBox
    Friend WithEvents CbParamLain As CheckBox
    Friend WithEvents DtpAkhir As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents DtpAwal As DateTimePicker
    Friend WithEvents CbParamTgl As CheckBox
    Friend WithEvents cmbTgl As ComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Lv_DetailDP As ListView
    Friend WithEvents Lv_Pajak As ListView
End Class
