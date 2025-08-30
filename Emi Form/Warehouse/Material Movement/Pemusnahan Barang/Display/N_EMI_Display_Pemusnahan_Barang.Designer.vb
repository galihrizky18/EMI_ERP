<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Display_Pemusnahan_Barang
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
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Lv_Data = New System.Windows.Forms.ListView()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
        Me.Chk_HariIni = New System.Windows.Forms.CheckBox()
        Me.Txt_ValueLain = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Cmb_Lain = New System.Windows.Forms.ComboBox()
        Me.Chk_Lain = New System.Windows.Forms.CheckBox()
        Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
        Me.Chk_Tanggal = New System.Windows.Forms.CheckBox()
        Me.Cmb_Tanggal = New System.Windows.Forms.ComboBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Lv_Detail = New System.Windows.Forms.ListView()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SalinToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(822, 43)
        Me.Panel1.TabIndex = 23
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 8)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(293, 28)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Display - Pemusnahan Barang"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(6, 42)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1240, 12)
        Me.Panel2.TabIndex = 35
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 43)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 780)
        Me.Panel3.TabIndex = 36
        Me.Panel3.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(803, 53)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 780)
        Me.Panel4.TabIndex = 36
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(17, 596)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1240, 15)
        Me.Panel5.TabIndex = 35
        Me.Panel5.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(737, 52)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 16)
        Me.Label8.TabIndex = 350
        Me.Label8.Text = "Dibatalkan"
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.DarkRed
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Location = New System.Drawing.Point(721, 54)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(12, 12)
        Me.Panel9.TabIndex = 349
        '
        'Lv_Data
        '
        Me.Lv_Data.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Lv_Data.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Lv_Data.FullRowSelect = True
        Me.Lv_Data.GridLines = True
        Me.Lv_Data.HideSelection = False
        Me.Lv_Data.Location = New System.Drawing.Point(18, 72)
        Me.Lv_Data.Name = "Lv_Data"
        Me.Lv_Data.Size = New System.Drawing.Size(785, 175)
        Me.Lv_Data.TabIndex = 1
        Me.Lv_Data.UseCompatibleStateImageBehavior = False
        Me.Lv_Data.View = System.Windows.Forms.View.Details
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Btn_Cari)
        Me.GroupBox3.Controls.Add(Me.Cmb_Lokasi)
        Me.GroupBox3.Controls.Add(Me.Chk_HariIni)
        Me.GroupBox3.Controls.Add(Me.Txt_ValueLain)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Cmb_Lain)
        Me.GroupBox3.Controls.Add(Me.Chk_Lain)
        Me.GroupBox3.Controls.Add(Me.Tgl2)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Tgl1)
        Me.GroupBox3.Controls.Add(Me.Chk_Tanggal)
        Me.GroupBox3.Controls.Add(Me.Cmb_Tanggal)
        Me.GroupBox3.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.GroupBox3.Location = New System.Drawing.Point(19, 457)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox3.Size = New System.Drawing.Size(632, 136)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Filter Data"
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(542, 102)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(81, 29)
        Me.Btn_Cari.TabIndex = 9
        Me.Btn_Cari.Text = "&Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Cmb_Lokasi
        '
        Me.Cmb_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lokasi.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Cmb_Lokasi.FormattingEnabled = True
        Me.Cmb_Lokasi.Location = New System.Drawing.Point(11, 21)
        Me.Cmb_Lokasi.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
        Me.Cmb_Lokasi.Size = New System.Drawing.Size(209, 24)
        Me.Cmb_Lokasi.TabIndex = 0
        '
        'Chk_HariIni
        '
        Me.Chk_HariIni.AutoSize = True
        Me.Chk_HariIni.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Chk_HariIni.Location = New System.Drawing.Point(11, 51)
        Me.Chk_HariIni.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Chk_HariIni.Name = "Chk_HariIni"
        Me.Chk_HariIni.Size = New System.Drawing.Size(118, 20)
        Me.Chk_HariIni.TabIndex = 1
        Me.Chk_HariIni.Text = "Transaksi Hari Ini"
        Me.Chk_HariIni.UseVisualStyleBackColor = True
        '
        'Txt_ValueLain
        '
        Me.Txt_ValueLain.Enabled = False
        Me.Txt_ValueLain.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_ValueLain.Location = New System.Drawing.Point(320, 108)
        Me.Txt_ValueLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_ValueLain.Name = "Txt_ValueLain"
        Me.Txt_ValueLain.Size = New System.Drawing.Size(217, 20)
        Me.Txt_ValueLain.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Label6.Location = New System.Drawing.Point(270, 110)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 16)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Value"
        '
        'Cmb_Lain
        '
        Me.Cmb_Lain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lain.Enabled = False
        Me.Cmb_Lain.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Cmb_Lain.FormattingEnabled = True
        Me.Cmb_Lain.Location = New System.Drawing.Point(138, 105)
        Me.Cmb_Lain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Lain.Name = "Cmb_Lain"
        Me.Cmb_Lain.Size = New System.Drawing.Size(123, 24)
        Me.Cmb_Lain.TabIndex = 7
        '
        'Chk_Lain
        '
        Me.Chk_Lain.AutoSize = True
        Me.Chk_Lain.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Chk_Lain.Location = New System.Drawing.Point(10, 104)
        Me.Chk_Lain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Chk_Lain.Name = "Chk_Lain"
        Me.Chk_Lain.Size = New System.Drawing.Size(107, 20)
        Me.Chk_Lain.TabIndex = 6
        Me.Chk_Lain.Text = "Parameter Lain"
        Me.Chk_Lain.UseVisualStyleBackColor = True
        '
        'Tgl2
        '
        Me.Tgl2.CustomFormat = "dd MMMM yyyy"
        Me.Tgl2.Enabled = False
        Me.Tgl2.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl2.Location = New System.Drawing.Point(465, 78)
        Me.Tgl2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Tgl2.Name = "Tgl2"
        Me.Tgl2.Size = New System.Drawing.Size(158, 20)
        Me.Tgl2.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Label7.Location = New System.Drawing.Point(431, 79)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(25, 16)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "s/d"
        '
        'Tgl1
        '
        Me.Tgl1.CustomFormat = "dd MMMM yyyy"
        Me.Tgl1.Enabled = False
        Me.Tgl1.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl1.Location = New System.Drawing.Point(265, 78)
        Me.Tgl1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Tgl1.Name = "Tgl1"
        Me.Tgl1.Size = New System.Drawing.Size(158, 20)
        Me.Tgl1.TabIndex = 4
        '
        'Chk_Tanggal
        '
        Me.Chk_Tanggal.AutoSize = True
        Me.Chk_Tanggal.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Chk_Tanggal.Location = New System.Drawing.Point(10, 77)
        Me.Chk_Tanggal.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Chk_Tanggal.Name = "Chk_Tanggal"
        Me.Chk_Tanggal.Size = New System.Drawing.Size(124, 20)
        Me.Chk_Tanggal.TabIndex = 2
        Me.Chk_Tanggal.Text = "Parameter Tanggal"
        Me.Chk_Tanggal.UseVisualStyleBackColor = True
        '
        'Cmb_Tanggal
        '
        Me.Cmb_Tanggal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Tanggal.Enabled = False
        Me.Cmb_Tanggal.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Cmb_Tanggal.FormattingEnabled = True
        Me.Cmb_Tanggal.Location = New System.Drawing.Point(138, 75)
        Me.Cmb_Tanggal.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Tanggal.Name = "Cmb_Tanggal"
        Me.Cmb_Tanggal.Size = New System.Drawing.Size(123, 24)
        Me.Cmb_Tanggal.TabIndex = 3
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(21, 450)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1240, 12)
        Me.Panel6.TabIndex = 35
        Me.Panel6.Visible = False
        '
        'Lv_Detail
        '
        Me.Lv_Detail.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Lv_Detail.FullRowSelect = True
        Me.Lv_Detail.GridLines = True
        Me.Lv_Detail.HideSelection = False
        Me.Lv_Detail.Location = New System.Drawing.Point(5, 21)
        Me.Lv_Detail.Name = "Lv_Detail"
        Me.Lv_Detail.Size = New System.Drawing.Size(772, 165)
        Me.Lv_Detail.TabIndex = 0
        Me.Lv_Detail.UseCompatibleStateImageBehavior = False
        Me.Lv_Detail.View = System.Windows.Forms.View.Details
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(18, 246)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1240, 12)
        Me.Panel7.TabIndex = 35
        Me.Panel7.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Lv_Detail)
        Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 258)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(783, 192)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detail"
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.LightGreen
        Me.Panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel10.Location = New System.Drawing.Point(640, 54)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(12, 12)
        Me.Panel10.TabIndex = 349
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(656, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 16)
        Me.Label3.TabIndex = 350
        Me.Label3.Text = "Divalidasi"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SalinToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(155, 26)
        '
        'SalinToolStripMenuItem
        '
        Me.SalinToolStripMenuItem.Name = "SalinToolStripMenuItem"
        Me.SalinToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.SalinToolStripMenuItem.Text = "Salin No Faktur"
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
        Me.PanelGradient1.Size = New System.Drawing.Size(822, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'N_EMI_Display_Pemusnahan_Barang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(822, 611)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Lv_Data)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel10)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Panel9)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_Display_Pemusnahan_Barang"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Lv_Data As ListView
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Cmb_Lokasi As ComboBox
    Friend WithEvents Chk_HariIni As CheckBox
    Friend WithEvents Txt_ValueLain As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Cmb_Lain As ComboBox
    Friend WithEvents Chk_Lain As CheckBox
    Friend WithEvents Tgl2 As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents Chk_Tanggal As CheckBox
    Friend WithEvents Cmb_Tanggal As ComboBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Lv_Detail As ListView
    Friend WithEvents Panel7 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents SalinToolStripMenuItem As ToolStripMenuItem
End Class
