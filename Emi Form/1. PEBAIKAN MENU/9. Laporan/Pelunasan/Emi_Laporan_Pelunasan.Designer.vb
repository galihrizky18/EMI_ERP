<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Emi_Laporan_Pelunasan
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Txt_KdKategori = New System.Windows.Forms.TextBox()
        Me.Txt_KdPerusahaan = New System.Windows.Forms.TextBox()
        Me.Txt_UserValidasi = New System.Windows.Forms.TextBox()
        Me.Txt_Faktur = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_NmPerusahaan = New System.Windows.Forms.TextBox()
        Me.Txt_NmKategori = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.BtnCetak = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Lv_Faktur = New System.Windows.Forms.ListView()
        Me.Lv_User = New System.Windows.Forms.ListView()
        Me.Lv_Perusahaan = New System.Windows.Forms.ListView()
        Me.Lv_Kategori = New System.Windows.Forms.ListView()
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
        Me.Panel1.Size = New System.Drawing.Size(615, 51)
        Me.Panel1.TabIndex = 26
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
        Me.PanelGradient1.Size = New System.Drawing.Size(615, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(228, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Laporan - Pelunasan"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(6, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 40
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 71)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(12, 601)
        Me.Panel3.TabIndex = 41
        Me.Panel3.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Txt_KdKategori)
        Me.GroupBox1.Controls.Add(Me.Txt_KdPerusahaan)
        Me.GroupBox1.Controls.Add(Me.Txt_UserValidasi)
        Me.GroupBox1.Controls.Add(Me.Txt_Faktur)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Tgl2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Tgl1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Txt_NmPerusahaan)
        Me.GroupBox1.Controls.Add(Me.Txt_NmKategori)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 58)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(590, 175)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Txt_KdKategori
        '
        Me.Txt_KdKategori.Location = New System.Drawing.Point(177, 138)
        Me.Txt_KdKategori.Name = "Txt_KdKategori"
        Me.Txt_KdKategori.Size = New System.Drawing.Size(163, 23)
        Me.Txt_KdKategori.TabIndex = 6
        '
        'Txt_KdPerusahaan
        '
        Me.Txt_KdPerusahaan.Location = New System.Drawing.Point(177, 109)
        Me.Txt_KdPerusahaan.Name = "Txt_KdPerusahaan"
        Me.Txt_KdPerusahaan.Size = New System.Drawing.Size(163, 23)
        Me.Txt_KdPerusahaan.TabIndex = 4
        '
        'Txt_UserValidasi
        '
        Me.Txt_UserValidasi.Location = New System.Drawing.Point(177, 80)
        Me.Txt_UserValidasi.Name = "Txt_UserValidasi"
        Me.Txt_UserValidasi.Size = New System.Drawing.Size(163, 23)
        Me.Txt_UserValidasi.TabIndex = 3
        '
        'Txt_Faktur
        '
        Me.Txt_Faktur.Location = New System.Drawing.Point(177, 51)
        Me.Txt_Faktur.Name = "Txt_Faktur"
        Me.Txt_Faktur.Size = New System.Drawing.Size(163, 23)
        Me.Txt_Faktur.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(163, 18)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Perusahaan Biaya Import"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 83)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(87, 18)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "User Validasi"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 141)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(142, 18)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Kategori Biaya Import"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 54)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 18)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "No Faktur"
        '
        'Tgl2
        '
        Me.Tgl2.CustomFormat = "dd MMMM yyyy"
        Me.Tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl2.Location = New System.Drawing.Point(388, 20)
        Me.Tgl2.Name = "Tgl2"
        Me.Tgl2.Size = New System.Drawing.Size(163, 23)
        Me.Tgl2.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(351, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 18)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "s/d"
        '
        'Tgl1
        '
        Me.Tgl1.CustomFormat = "dd MMMM yyyy"
        Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl1.Location = New System.Drawing.Point(177, 20)
        Me.Tgl1.Name = "Tgl1"
        Me.Tgl1.Size = New System.Drawing.Size(163, 23)
        Me.Tgl1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 18)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Periode"
        '
        'Txt_NmPerusahaan
        '
        Me.Txt_NmPerusahaan.Location = New System.Drawing.Point(346, 109)
        Me.Txt_NmPerusahaan.Name = "Txt_NmPerusahaan"
        Me.Txt_NmPerusahaan.Size = New System.Drawing.Size(236, 23)
        Me.Txt_NmPerusahaan.TabIndex = 5
        '
        'Txt_NmKategori
        '
        Me.Txt_NmKategori.Location = New System.Drawing.Point(346, 138)
        Me.Txt_NmKategori.Name = "Txt_NmKategori"
        Me.Txt_NmKategori.Size = New System.Drawing.Size(236, 23)
        Me.Txt_NmKategori.TabIndex = 7
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(603, 71)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(12, 601)
        Me.Panel4.TabIndex = 41
        Me.Panel4.Visible = False
        '
        'BtnCetak
        '
        Me.BtnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnCetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnCetak.ForeColor = System.Drawing.Color.White
        Me.BtnCetak.Location = New System.Drawing.Point(420, 239)
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
        Me.BtnExit.Location = New System.Drawing.Point(503, 239)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(84, 33)
        Me.BtnExit.TabIndex = 44
        Me.BtnExit.Text = "&Keluar"
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(18, 272)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(942, 12)
        Me.Panel5.TabIndex = 40
        Me.Panel5.Visible = False
        '
        'Lv_Faktur
        '
        Me.Lv_Faktur.BackColor = System.Drawing.Color.White
        Me.Lv_Faktur.FullRowSelect = True
        Me.Lv_Faktur.GridLines = True
        Me.Lv_Faktur.HideSelection = False
        Me.Lv_Faktur.Location = New System.Drawing.Point(640, 134)
        Me.Lv_Faktur.Name = "Lv_Faktur"
        Me.Lv_Faktur.Size = New System.Drawing.Size(389, 200)
        Me.Lv_Faktur.TabIndex = 46
        Me.Lv_Faktur.UseCompatibleStateImageBehavior = False
        Me.Lv_Faktur.View = System.Windows.Forms.View.Details
        Me.Lv_Faktur.Visible = False
        '
        'Lv_User
        '
        Me.Lv_User.BackColor = System.Drawing.Color.White
        Me.Lv_User.FullRowSelect = True
        Me.Lv_User.GridLines = True
        Me.Lv_User.HideSelection = False
        Me.Lv_User.Location = New System.Drawing.Point(640, 163)
        Me.Lv_User.Name = "Lv_User"
        Me.Lv_User.Size = New System.Drawing.Size(389, 200)
        Me.Lv_User.TabIndex = 46
        Me.Lv_User.UseCompatibleStateImageBehavior = False
        Me.Lv_User.View = System.Windows.Forms.View.Details
        Me.Lv_User.Visible = False
        '
        'Lv_Perusahaan
        '
        Me.Lv_Perusahaan.BackColor = System.Drawing.Color.White
        Me.Lv_Perusahaan.FullRowSelect = True
        Me.Lv_Perusahaan.GridLines = True
        Me.Lv_Perusahaan.HideSelection = False
        Me.Lv_Perusahaan.Location = New System.Drawing.Point(640, 192)
        Me.Lv_Perusahaan.Name = "Lv_Perusahaan"
        Me.Lv_Perusahaan.Size = New System.Drawing.Size(389, 200)
        Me.Lv_Perusahaan.TabIndex = 46
        Me.Lv_Perusahaan.UseCompatibleStateImageBehavior = False
        Me.Lv_Perusahaan.View = System.Windows.Forms.View.Details
        Me.Lv_Perusahaan.Visible = False
        '
        'Lv_Kategori
        '
        Me.Lv_Kategori.BackColor = System.Drawing.Color.White
        Me.Lv_Kategori.FullRowSelect = True
        Me.Lv_Kategori.GridLines = True
        Me.Lv_Kategori.HideSelection = False
        Me.Lv_Kategori.Location = New System.Drawing.Point(640, 220)
        Me.Lv_Kategori.Name = "Lv_Kategori"
        Me.Lv_Kategori.Size = New System.Drawing.Size(389, 200)
        Me.Lv_Kategori.TabIndex = 46
        Me.Lv_Kategori.UseCompatibleStateImageBehavior = False
        Me.Lv_Kategori.View = System.Windows.Forms.View.Details
        Me.Lv_Kategori.Visible = False
        '
        'Emi_Laporan_Pelunasan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(615, 286)
        Me.Controls.Add(Me.Lv_Kategori)
        Me.Controls.Add(Me.Lv_Perusahaan)
        Me.Controls.Add(Me.Lv_User)
        Me.Controls.Add(Me.Lv_Faktur)
        Me.Controls.Add(Me.BtnCetak)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Emi_Laporan_Pelunasan"
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
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Txt_KdKategori As TextBox
    Friend WithEvents Txt_KdPerusahaan As TextBox
    Friend WithEvents Txt_Faktur As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Tgl2 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_NmPerusahaan As TextBox
    Friend WithEvents Txt_NmKategori As TextBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents BtnCetak As Button
    Friend WithEvents BtnExit As Button
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Txt_UserValidasi As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Lv_Faktur As ListView
    Friend WithEvents Lv_User As ListView
    Friend WithEvents Lv_Perusahaan As ListView
    Friend WithEvents Lv_Kategori As ListView
End Class
