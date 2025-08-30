<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Laporan_Down_Payment
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
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Txt_Pengajuan = New System.Windows.Forms.TextBox()
        Me.Txt_Kd_Supplier = New System.Windows.Forms.TextBox()
        Me.Txt_No_PO = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
        Me.Txt_Supplier = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_Keterangan_PO = New System.Windows.Forms.TextBox()
        Me.BtnCetak = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Lv_NoPengajuan = New System.Windows.Forms.ListView()
        Me.Lv_NoPO = New System.Windows.Forms.ListView()
        Me.Lv_Supplier = New System.Windows.Forms.ListView()
        Me.Cmb_Status_DP = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
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
        Me.Panel1.TabIndex = 30
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
        Me.Label1.Size = New System.Drawing.Size(251, 28)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Laporan - Down Payment"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 51)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 601)
        Me.Panel3.TabIndex = 46
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(-6, 42)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(942, 12)
        Me.Panel5.TabIndex = 47
        Me.Panel5.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(29, 260)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 15)
        Me.Panel2.TabIndex = 47
        Me.Panel2.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Cmb_Status_DP)
        Me.GroupBox1.Controls.Add(Me.Txt_Pengajuan)
        Me.GroupBox1.Controls.Add(Me.Txt_Kd_Supplier)
        Me.GroupBox1.Controls.Add(Me.Txt_No_PO)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Tgl2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Tgl1)
        Me.GroupBox1.Controls.Add(Me.Txt_Supplier)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Txt_Keterangan_PO)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 54)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(606, 167)
        Me.GroupBox1.TabIndex = 48
        Me.GroupBox1.TabStop = False
        '
        'Txt_Pengajuan
        '
        Me.Txt_Pengajuan.Location = New System.Drawing.Point(105, 48)
        Me.Txt_Pengajuan.Name = "Txt_Pengajuan"
        Me.Txt_Pengajuan.Size = New System.Drawing.Size(163, 20)
        Me.Txt_Pengajuan.TabIndex = 3
        '
        'Txt_Kd_Supplier
        '
        Me.Txt_Kd_Supplier.Location = New System.Drawing.Point(105, 100)
        Me.Txt_Kd_Supplier.Name = "Txt_Kd_Supplier"
        Me.Txt_Kd_Supplier.Size = New System.Drawing.Size(163, 20)
        Me.Txt_Kd_Supplier.TabIndex = 3
        '
        'Txt_No_PO
        '
        Me.Txt_No_PO.Location = New System.Drawing.Point(105, 74)
        Me.Txt_No_PO.Name = "Txt_No_PO"
        Me.Txt_No_PO.Size = New System.Drawing.Size(163, 20)
        Me.Txt_No_PO.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 16)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "No Pengajuan"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 103)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 16)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Supplier"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 77)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 16)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "No PO"
        '
        'Tgl2
        '
        Me.Tgl2.Cursor = System.Windows.Forms.Cursors.Hand
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
        Me.Tgl1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Tgl1.CustomFormat = "dd MMMM yyyy"
        Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl1.Location = New System.Drawing.Point(105, 22)
        Me.Tgl1.Name = "Tgl1"
        Me.Tgl1.Size = New System.Drawing.Size(163, 20)
        Me.Tgl1.TabIndex = 0
        '
        'Txt_Supplier
        '
        Me.Txt_Supplier.Location = New System.Drawing.Point(272, 100)
        Me.Txt_Supplier.Name = "Txt_Supplier"
        Me.Txt_Supplier.Size = New System.Drawing.Size(319, 20)
        Me.Txt_Supplier.TabIndex = 4
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
        'Txt_Keterangan_PO
        '
        Me.Txt_Keterangan_PO.Location = New System.Drawing.Point(272, 74)
        Me.Txt_Keterangan_PO.Name = "Txt_Keterangan_PO"
        Me.Txt_Keterangan_PO.Size = New System.Drawing.Size(319, 20)
        Me.Txt_Keterangan_PO.TabIndex = 4
        '
        'BtnCetak
        '
        Me.BtnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnCetak.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnCetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnCetak.ForeColor = System.Drawing.Color.White
        Me.BtnCetak.Location = New System.Drawing.Point(457, 227)
        Me.BtnCetak.Name = "BtnCetak"
        Me.BtnCetak.Size = New System.Drawing.Size(84, 33)
        Me.BtnCetak.TabIndex = 49
        Me.BtnCetak.Text = "&Cetak"
        Me.BtnCetak.UseVisualStyleBackColor = False
        '
        'BtnExit
        '
        Me.BtnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnExit.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnExit.ForeColor = System.Drawing.Color.White
        Me.BtnExit.Location = New System.Drawing.Point(540, 227)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(84, 33)
        Me.BtnExit.TabIndex = 50
        Me.BtnExit.Text = "&Keluar"
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(625, 62)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 601)
        Me.Panel4.TabIndex = 46
        Me.Panel4.Visible = False
        '
        'Lv_NoPengajuan
        '
        Me.Lv_NoPengajuan.BackColor = System.Drawing.Color.White
        Me.Lv_NoPengajuan.FullRowSelect = True
        Me.Lv_NoPengajuan.GridLines = True
        Me.Lv_NoPengajuan.HideSelection = False
        Me.Lv_NoPengajuan.Location = New System.Drawing.Point(650, 125)
        Me.Lv_NoPengajuan.Name = "Lv_NoPengajuan"
        Me.Lv_NoPengajuan.Size = New System.Drawing.Size(486, 200)
        Me.Lv_NoPengajuan.TabIndex = 51
        Me.Lv_NoPengajuan.UseCompatibleStateImageBehavior = False
        Me.Lv_NoPengajuan.View = System.Windows.Forms.View.Details
        Me.Lv_NoPengajuan.Visible = False
        '
        'Lv_NoPO
        '
        Me.Lv_NoPO.BackColor = System.Drawing.Color.White
        Me.Lv_NoPO.FullRowSelect = True
        Me.Lv_NoPO.GridLines = True
        Me.Lv_NoPO.HideSelection = False
        Me.Lv_NoPO.Location = New System.Drawing.Point(650, 150)
        Me.Lv_NoPO.Name = "Lv_NoPO"
        Me.Lv_NoPO.Size = New System.Drawing.Size(486, 200)
        Me.Lv_NoPO.TabIndex = 51
        Me.Lv_NoPO.UseCompatibleStateImageBehavior = False
        Me.Lv_NoPO.View = System.Windows.Forms.View.Details
        Me.Lv_NoPO.Visible = False
        '
        'Lv_Supplier
        '
        Me.Lv_Supplier.BackColor = System.Drawing.Color.White
        Me.Lv_Supplier.FullRowSelect = True
        Me.Lv_Supplier.GridLines = True
        Me.Lv_Supplier.HideSelection = False
        Me.Lv_Supplier.Location = New System.Drawing.Point(650, 176)
        Me.Lv_Supplier.Name = "Lv_Supplier"
        Me.Lv_Supplier.Size = New System.Drawing.Size(486, 200)
        Me.Lv_Supplier.TabIndex = 51
        Me.Lv_Supplier.UseCompatibleStateImageBehavior = False
        Me.Lv_Supplier.View = System.Windows.Forms.View.Details
        Me.Lv_Supplier.Visible = False
        '
        'Cmb_Status_DP
        '
        Me.Cmb_Status_DP.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cmb_Status_DP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Status_DP.FormattingEnabled = True
        Me.Cmb_Status_DP.Location = New System.Drawing.Point(105, 126)
        Me.Cmb_Status_DP.Name = "Cmb_Status_DP"
        Me.Cmb_Status_DP.Size = New System.Drawing.Size(163, 24)
        Me.Cmb_Status_DP.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 130)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 16)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Status DP"
        '
        'N_EMI_Laporan_Down_Payment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(644, 274)
        Me.Controls.Add(Me.Lv_Supplier)
        Me.Controls.Add(Me.Lv_NoPO)
        Me.Controls.Add(Me.Lv_NoPengajuan)
        Me.Controls.Add(Me.BtnCetak)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_Laporan_Down_Payment"
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
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Txt_No_PO As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Tgl2 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_Keterangan_PO As TextBox
    Friend WithEvents Txt_Pengajuan As TextBox
    Friend WithEvents Txt_Kd_Supplier As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Txt_Supplier As TextBox
    Friend WithEvents BtnCetak As Button
    Friend WithEvents BtnExit As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Lv_NoPengajuan As ListView
    Friend WithEvents Lv_NoPO As ListView
    Friend WithEvents Lv_Supplier As ListView
    Friend WithEvents Cmb_Status_DP As ComboBox
    Friend WithEvents Label6 As Label
End Class
