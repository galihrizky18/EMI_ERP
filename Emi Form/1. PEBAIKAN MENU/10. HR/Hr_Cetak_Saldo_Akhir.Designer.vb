<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Hr_Cetak_Saldo_Akhir
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Hr_Cetak_Saldo_Akhir))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Txt_IdGroupJenis = New System.Windows.Forms.TextBox()
        Me.Txt_NmGroupJenis = New System.Windows.Forms.TextBox()
        Me.Cmb_FlagInspection = New System.Windows.Forms.ComboBox()
        Me.CmbJenis = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CmbTahun = New System.Windows.Forms.ComboBox()
        Me.CmbBulan = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CmbLokasi = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Btn_Cetak = New System.Windows.Forms.Button()
        Me.Btn_Exit = New System.Windows.Forms.Button()
        Me.Lv_GroupJenis = New System.Windows.Forms.ListView()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Txt_IdGroupJenis)
        Me.GroupBox1.Controls.Add(Me.Txt_NmGroupJenis)
        Me.GroupBox1.Controls.Add(Me.Cmb_FlagInspection)
        Me.GroupBox1.Controls.Add(Me.CmbJenis)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.CmbTahun)
        Me.GroupBox1.Controls.Add(Me.CmbBulan)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.CmbLokasi)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 56)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(450, 194)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Pilihan Pencetakan"
        '
        'Txt_IdGroupJenis
        '
        Me.Txt_IdGroupJenis.Location = New System.Drawing.Point(114, 156)
        Me.Txt_IdGroupJenis.Name = "Txt_IdGroupJenis"
        Me.Txt_IdGroupJenis.Size = New System.Drawing.Size(126, 23)
        Me.Txt_IdGroupJenis.TabIndex = 4
        '
        'Txt_NmGroupJenis
        '
        Me.Txt_NmGroupJenis.Location = New System.Drawing.Point(246, 156)
        Me.Txt_NmGroupJenis.Name = "Txt_NmGroupJenis"
        Me.Txt_NmGroupJenis.Size = New System.Drawing.Size(186, 23)
        Me.Txt_NmGroupJenis.TabIndex = 5
        '
        'Cmb_FlagInspection
        '
        Me.Cmb_FlagInspection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_FlagInspection.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.Cmb_FlagInspection.FormattingEnabled = True
        Me.Cmb_FlagInspection.Location = New System.Drawing.Point(114, 123)
        Me.Cmb_FlagInspection.Margin = New System.Windows.Forms.Padding(4)
        Me.Cmb_FlagInspection.Name = "Cmb_FlagInspection"
        Me.Cmb_FlagInspection.Size = New System.Drawing.Size(126, 26)
        Me.Cmb_FlagInspection.TabIndex = 3
        '
        'CmbJenis
        '
        Me.CmbJenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbJenis.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.CmbJenis.FormattingEnabled = True
        Me.CmbJenis.Location = New System.Drawing.Point(114, 89)
        Me.CmbJenis.Margin = New System.Windows.Forms.Padding(4)
        Me.CmbJenis.Name = "CmbJenis"
        Me.CmbJenis.Size = New System.Drawing.Size(126, 26)
        Me.CmbJenis.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.Label5.Location = New System.Drawing.Point(9, 127)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 18)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Inspection"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.Label4.Location = New System.Drawing.Point(9, 159)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 18)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Group Jenis"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.Label2.Location = New System.Drawing.Point(9, 93)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 18)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Jenis Laporan"
        '
        'CmbTahun
        '
        Me.CmbTahun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbTahun.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.CmbTahun.FormattingEnabled = True
        Me.CmbTahun.Location = New System.Drawing.Point(248, 55)
        Me.CmbTahun.Margin = New System.Windows.Forms.Padding(4)
        Me.CmbTahun.Name = "CmbTahun"
        Me.CmbTahun.Size = New System.Drawing.Size(89, 26)
        Me.CmbTahun.TabIndex = 2
        '
        'CmbBulan
        '
        Me.CmbBulan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbBulan.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.CmbBulan.FormattingEnabled = True
        Me.CmbBulan.Location = New System.Drawing.Point(114, 55)
        Me.CmbBulan.Margin = New System.Windows.Forms.Padding(4)
        Me.CmbBulan.Name = "CmbBulan"
        Me.CmbBulan.Size = New System.Drawing.Size(126, 26)
        Me.CmbBulan.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.Label1.Location = New System.Drawing.Point(9, 59)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 18)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Periode"
        '
        'CmbLokasi
        '
        Me.CmbLokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbLokasi.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.CmbLokasi.FormattingEnabled = True
        Me.CmbLokasi.Location = New System.Drawing.Point(114, 21)
        Me.CmbLokasi.Margin = New System.Windows.Forms.Padding(4)
        Me.CmbLokasi.Name = "CmbLokasi"
        Me.CmbLokasi.Size = New System.Drawing.Size(223, 26)
        Me.CmbLokasi.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.Label3.Location = New System.Drawing.Point(9, 27)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 18)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Lokasi"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Lbl_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(489, 43)
        Me.Panel1.TabIndex = 79
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
        Me.PanelGradient1.Size = New System.Drawing.Size(489, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(16, 7)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(188, 29)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Cetak Saldo Akhir"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 42)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1099, 12)
        Me.Panel2.TabIndex = 80
        Me.Panel2.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(0, 57)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 740)
        Me.Panel4.TabIndex = 81
        Me.Panel4.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(470, 62)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 740)
        Me.Panel3.TabIndex = 81
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(21, 289)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1099, 15)
        Me.Panel5.TabIndex = 80
        Me.Panel5.Visible = False
        '
        'Btn_Cetak
        '
        Me.Btn_Cetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cetak.ForeColor = System.Drawing.Color.White
        Me.Btn_Cetak.Location = New System.Drawing.Point(296, 257)
        Me.Btn_Cetak.Name = "Btn_Cetak"
        Me.Btn_Cetak.Size = New System.Drawing.Size(84, 33)
        Me.Btn_Cetak.TabIndex = 1
        Me.Btn_Cetak.Text = "&Cetak"
        Me.Btn_Cetak.UseVisualStyleBackColor = False
        '
        'Btn_Exit
        '
        Me.Btn_Exit.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Exit.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Exit.ForeColor = System.Drawing.Color.White
        Me.Btn_Exit.Location = New System.Drawing.Point(386, 257)
        Me.Btn_Exit.Name = "Btn_Exit"
        Me.Btn_Exit.Size = New System.Drawing.Size(84, 33)
        Me.Btn_Exit.TabIndex = 2
        Me.Btn_Exit.Text = "&Keluar"
        Me.Btn_Exit.UseVisualStyleBackColor = False
        '
        'Lv_GroupJenis
        '
        Me.Lv_GroupJenis.BackColor = System.Drawing.Color.White
        Me.Lv_GroupJenis.FullRowSelect = True
        Me.Lv_GroupJenis.GridLines = True
        Me.Lv_GroupJenis.HideSelection = False
        Me.Lv_GroupJenis.Location = New System.Drawing.Point(500, 237)
        Me.Lv_GroupJenis.Name = "Lv_GroupJenis"
        Me.Lv_GroupJenis.Size = New System.Drawing.Size(318, 171)
        Me.Lv_GroupJenis.TabIndex = 84
        Me.Lv_GroupJenis.UseCompatibleStateImageBehavior = False
        Me.Lv_GroupJenis.View = System.Windows.Forms.View.Details
        Me.Lv_GroupJenis.Visible = False
        '
        'Hr_Cetak_Saldo_Akhir
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(489, 303)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Lv_GroupJenis)
        Me.Controls.Add(Me.Btn_Exit)
        Me.Controls.Add(Me.Btn_Cetak)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Hr_Cetak_Saldo_Akhir"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CmbBulan As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CmbLokasi As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CmbTahun As System.Windows.Forms.ComboBox
    Friend WithEvents CmbJenis As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents Txt_IdGroupJenis As TextBox
    Friend WithEvents Txt_NmGroupJenis As TextBox
    Friend WithEvents Btn_Cetak As Button
    Friend WithEvents Btn_Exit As Button
    Friend WithEvents Lv_GroupJenis As ListView
    Friend WithEvents Cmb_FlagInspection As ComboBox
    Friend WithEvents Label5 As Label
End Class
