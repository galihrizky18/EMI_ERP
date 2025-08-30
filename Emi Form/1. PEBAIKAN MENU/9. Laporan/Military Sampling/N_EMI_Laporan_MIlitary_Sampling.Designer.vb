<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Laporan_MIlitary_Sampling
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
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Cmb_Jenis_Laporan = New System.Windows.Forms.ComboBox()
        Me.Cmb_Status = New System.Windows.Forms.ComboBox()
        Me.Cmb_Filter_Lain = New System.Windows.Forms.ComboBox()
        Me.Cmb_Jenis_MIlitary = New System.Windows.Forms.ComboBox()
        Me.Txt_Value_Lain = New System.Windows.Forms.TextBox()
        Me.Txt_No_Military = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Txt_No_GR = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Txt_No_Split = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.BtnCetak = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.Lv_Split = New System.Windows.Forms.ListView()
        Me.Lv_GR = New System.Windows.Forms.ListView()
        Me.Lv_Military = New System.Windows.Forms.ListView()
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
        Me.Panel1.Size = New System.Drawing.Size(580, 51)
        Me.Panel1.TabIndex = 27
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
        Me.PanelGradient1.Size = New System.Drawing.Size(580, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(303, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Laporan - Military Sampling"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(-2, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 41
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 71)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 601)
        Me.Panel3.TabIndex = 42
        Me.Panel3.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(20, 353)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(942, 15)
        Me.Panel4.TabIndex = 41
        Me.Panel4.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Cmb_Jenis_Laporan)
        Me.GroupBox1.Controls.Add(Me.Cmb_Status)
        Me.GroupBox1.Controls.Add(Me.Cmb_Filter_Lain)
        Me.GroupBox1.Controls.Add(Me.Cmb_Jenis_MIlitary)
        Me.GroupBox1.Controls.Add(Me.Txt_Value_Lain)
        Me.GroupBox1.Controls.Add(Me.Txt_No_Military)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Txt_No_GR)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Txt_No_Split)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Tgl2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Tgl1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 63)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(540, 249)
        Me.GroupBox1.TabIndex = 45
        Me.GroupBox1.TabStop = False
        '
        'Cmb_Jenis_Laporan
        '
        Me.Cmb_Jenis_Laporan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cmb_Jenis_Laporan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Jenis_Laporan.FormattingEnabled = True
        Me.Cmb_Jenis_Laporan.Location = New System.Drawing.Point(152, 46)
        Me.Cmb_Jenis_Laporan.Name = "Cmb_Jenis_Laporan"
        Me.Cmb_Jenis_Laporan.Size = New System.Drawing.Size(163, 24)
        Me.Cmb_Jenis_Laporan.TabIndex = 5
        '
        'Cmb_Status
        '
        Me.Cmb_Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Status.FormattingEnabled = True
        Me.Cmb_Status.Location = New System.Drawing.Point(152, 154)
        Me.Cmb_Status.Name = "Cmb_Status"
        Me.Cmb_Status.Size = New System.Drawing.Size(163, 24)
        Me.Cmb_Status.TabIndex = 5
        '
        'Cmb_Filter_Lain
        '
        Me.Cmb_Filter_Lain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Filter_Lain.FormattingEnabled = True
        Me.Cmb_Filter_Lain.Location = New System.Drawing.Point(152, 214)
        Me.Cmb_Filter_Lain.Name = "Cmb_Filter_Lain"
        Me.Cmb_Filter_Lain.Size = New System.Drawing.Size(101, 24)
        Me.Cmb_Filter_Lain.TabIndex = 5
        '
        'Cmb_Jenis_MIlitary
        '
        Me.Cmb_Jenis_MIlitary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Jenis_MIlitary.FormattingEnabled = True
        Me.Cmb_Jenis_MIlitary.Location = New System.Drawing.Point(152, 184)
        Me.Cmb_Jenis_MIlitary.Name = "Cmb_Jenis_MIlitary"
        Me.Cmb_Jenis_MIlitary.Size = New System.Drawing.Size(163, 24)
        Me.Cmb_Jenis_MIlitary.TabIndex = 5
        '
        'Txt_Value_Lain
        '
        Me.Txt_Value_Lain.Enabled = False
        Me.Txt_Value_Lain.Location = New System.Drawing.Point(256, 218)
        Me.Txt_Value_Lain.Name = "Txt_Value_Lain"
        Me.Txt_Value_Lain.Size = New System.Drawing.Size(270, 20)
        Me.Txt_Value_Lain.TabIndex = 4
        '
        'Txt_No_Military
        '
        Me.Txt_No_Military.Location = New System.Drawing.Point(152, 128)
        Me.Txt_No_Military.Name = "Txt_No_Military"
        Me.Txt_No_Military.Size = New System.Drawing.Size(163, 20)
        Me.Txt_No_Military.TabIndex = 4
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(8, 49)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(83, 16)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "Jenis Laporan"
        '
        'Txt_No_GR
        '
        Me.Txt_No_GR.Location = New System.Drawing.Point(152, 102)
        Me.Txt_No_GR.Name = "Txt_No_GR"
        Me.Txt_No_GR.Size = New System.Drawing.Size(163, 20)
        Me.Txt_No_GR.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 157)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 16)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Status"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 217)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(62, 16)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Filter Lain"
        '
        'Txt_No_Split
        '
        Me.Txt_No_Split.Location = New System.Drawing.Point(152, 76)
        Me.Txt_No_Split.Name = "Txt_No_Split"
        Me.Txt_No_Split.Size = New System.Drawing.Size(163, 20)
        Me.Txt_No_Split.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 187)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(131, 16)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Jenis Military Sampling"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 127)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(118, 16)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "No Military Sampling"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 102)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(128, 16)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "No Penerimaan Barang"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 76)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 16)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "No Split"
        '
        'Tgl2
        '
        Me.Tgl2.CustomFormat = "dd MMMM yyyy"
        Me.Tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl2.Location = New System.Drawing.Point(363, 20)
        Me.Tgl2.Name = "Tgl2"
        Me.Tgl2.Size = New System.Drawing.Size(163, 20)
        Me.Tgl2.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(326, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "s/d"
        '
        'Tgl1
        '
        Me.Tgl1.CustomFormat = "dd MMMM yyyy"
        Me.Tgl1.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl1.Location = New System.Drawing.Point(152, 20)
        Me.Tgl1.Name = "Tgl1"
        Me.Tgl1.Size = New System.Drawing.Size(163, 20)
        Me.Tgl1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Periode"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(561, 64)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 601)
        Me.Panel5.TabIndex = 42
        Me.Panel5.Visible = False
        '
        'BtnCetak
        '
        Me.BtnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnCetak.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnCetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnCetak.ForeColor = System.Drawing.Color.White
        Me.BtnCetak.Location = New System.Drawing.Point(393, 321)
        Me.BtnCetak.Name = "BtnCetak"
        Me.BtnCetak.Size = New System.Drawing.Size(84, 33)
        Me.BtnCetak.TabIndex = 47
        Me.BtnCetak.Text = "&Cetak"
        Me.BtnCetak.UseVisualStyleBackColor = False
        '
        'BtnExit
        '
        Me.BtnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnExit.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnExit.ForeColor = System.Drawing.Color.White
        Me.BtnExit.Location = New System.Drawing.Point(476, 321)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(84, 33)
        Me.BtnExit.TabIndex = 48
        Me.BtnExit.Text = "&Keluar"
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'Lv_Split
        '
        Me.Lv_Split.BackColor = System.Drawing.Color.White
        Me.Lv_Split.FullRowSelect = True
        Me.Lv_Split.GridLines = True
        Me.Lv_Split.HideSelection = False
        Me.Lv_Split.Location = New System.Drawing.Point(600, 161)
        Me.Lv_Split.Name = "Lv_Split"
        Me.Lv_Split.Size = New System.Drawing.Size(374, 166)
        Me.Lv_Split.TabIndex = 49
        Me.Lv_Split.UseCompatibleStateImageBehavior = False
        Me.Lv_Split.View = System.Windows.Forms.View.Details
        Me.Lv_Split.Visible = False
        '
        'Lv_GR
        '
        Me.Lv_GR.BackColor = System.Drawing.Color.White
        Me.Lv_GR.FullRowSelect = True
        Me.Lv_GR.GridLines = True
        Me.Lv_GR.HideSelection = False
        Me.Lv_GR.Location = New System.Drawing.Point(600, 187)
        Me.Lv_GR.Name = "Lv_GR"
        Me.Lv_GR.Size = New System.Drawing.Size(374, 166)
        Me.Lv_GR.TabIndex = 49
        Me.Lv_GR.UseCompatibleStateImageBehavior = False
        Me.Lv_GR.View = System.Windows.Forms.View.Details
        Me.Lv_GR.Visible = False
        '
        'Lv_Military
        '
        Me.Lv_Military.BackColor = System.Drawing.Color.White
        Me.Lv_Military.FullRowSelect = True
        Me.Lv_Military.GridLines = True
        Me.Lv_Military.HideSelection = False
        Me.Lv_Military.Location = New System.Drawing.Point(600, 213)
        Me.Lv_Military.Name = "Lv_Military"
        Me.Lv_Military.Size = New System.Drawing.Size(374, 166)
        Me.Lv_Military.TabIndex = 49
        Me.Lv_Military.UseCompatibleStateImageBehavior = False
        Me.Lv_Military.View = System.Windows.Forms.View.Details
        Me.Lv_Military.Visible = False
        '
        'N_EMI_Laporan_MIlitary_Sampling
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(580, 368)
        Me.Controls.Add(Me.Lv_Military)
        Me.Controls.Add(Me.Lv_GR)
        Me.Controls.Add(Me.Lv_Split)
        Me.Controls.Add(Me.BtnCetak)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_Laporan_MIlitary_Sampling"
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
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Txt_No_Military As TextBox
    Friend WithEvents Txt_No_GR As TextBox
    Friend WithEvents Txt_No_Split As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Tgl2 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Cmb_Jenis_MIlitary As ComboBox
    Friend WithEvents Cmb_Status As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Cmb_Filter_Lain As ComboBox
    Friend WithEvents Txt_Value_Lain As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents BtnCetak As Button
    Friend WithEvents BtnExit As Button
    Friend WithEvents Lv_Split As ListView
    Friend WithEvents Lv_GR As ListView
    Friend WithEvents Lv_Military As ListView
    Friend WithEvents Cmb_Jenis_Laporan As ComboBox
    Friend WithEvents Label10 As Label
End Class
