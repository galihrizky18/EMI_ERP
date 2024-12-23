<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Master_Work_Center
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
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Txt_Kd = New System.Windows.Forms.TextBox()
        Me.Lbl_Kd = New System.Windows.Forms.Label()
        Me.Lbl_Keterangan = New System.Windows.Forms.Label()
        Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Hapus = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Cmb_Kolom = New System.Windows.Forms.ComboBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Lbl_Value = New System.Windows.Forms.Label()
        Me.Lbl_Kolom = New System.Windows.Forms.Label()
        Me.Txt_Value = New System.Windows.Forms.TextBox()
        Me.Lv_WorkCenter = New System.Windows.Forms.ListView()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Lbl_IDWorkCenter = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Lbl_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(699, 51)
        Me.Panel1.TabIndex = 23
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 11)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(292, 30)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Master Data - Work Center"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 35
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 63)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 498)
        Me.Panel3.TabIndex = 36
        Me.Panel3.Visible = False
        '
        'Txt_Kd
        '
        Me.Txt_Kd.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Kd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Kd.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Kd.Location = New System.Drawing.Point(141, 83)
        Me.Txt_Kd.MaxLength = 50
        Me.Txt_Kd.Name = "Txt_Kd"
        Me.Txt_Kd.Size = New System.Drawing.Size(298, 22)
        Me.Txt_Kd.TabIndex = 228
        '
        'Lbl_Kd
        '
        Me.Lbl_Kd.AutoSize = True
        Me.Lbl_Kd.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Kd.Location = New System.Drawing.Point(16, 83)
        Me.Lbl_Kd.Name = "Lbl_Kd"
        Me.Lbl_Kd.Size = New System.Drawing.Size(42, 20)
        Me.Lbl_Kd.TabIndex = 229
        Me.Lbl_Kd.Text = "Kode"
        '
        'Lbl_Keterangan
        '
        Me.Lbl_Keterangan.AutoSize = True
        Me.Lbl_Keterangan.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Keterangan.Location = New System.Drawing.Point(16, 116)
        Me.Lbl_Keterangan.Name = "Lbl_Keterangan"
        Me.Lbl_Keterangan.Size = New System.Drawing.Size(86, 20)
        Me.Lbl_Keterangan.TabIndex = 230
        Me.Lbl_Keterangan.Text = "Keterangan"
        '
        'Txt_Keterangan
        '
        Me.Txt_Keterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Keterangan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Keterangan.Location = New System.Drawing.Point(141, 115)
        Me.Txt_Keterangan.MaxLength = 50
        Me.Txt_Keterangan.Name = "Txt_Keterangan"
        Me.Txt_Keterangan.Size = New System.Drawing.Size(298, 22)
        Me.Txt_Keterangan.TabIndex = 231
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(5, 144)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(924, 12)
        Me.Panel6.TabIndex = 232
        Me.Panel6.Visible = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(200, 155)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Refresh.TabIndex = 235
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Hapus
        '
        Me.Btn_Hapus.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Hapus.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Hapus.ForeColor = System.Drawing.Color.White
        Me.Btn_Hapus.Location = New System.Drawing.Point(110, 155)
        Me.Btn_Hapus.Name = "Btn_Hapus"
        Me.Btn_Hapus.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Hapus.TabIndex = 234
        Me.Btn_Hapus.Text = "&Hapus"
        Me.Btn_Hapus.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(20, 155)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Simpan.TabIndex = 233
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(4, 196)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(938, 19)
        Me.Panel7.TabIndex = 236
        Me.Panel7.Visible = False
        '
        'Cmb_Kolom
        '
        Me.Cmb_Kolom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Kolom.DropDownWidth = 150
        Me.Cmb_Kolom.Font = New System.Drawing.Font("Work Sans", 8.9!)
        Me.Cmb_Kolom.FormattingEnabled = True
        Me.Cmb_Kolom.Location = New System.Drawing.Point(65, 14)
        Me.Cmb_Kolom.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmb_Kolom.Name = "Cmb_Kolom"
        Me.Cmb_Kolom.Size = New System.Drawing.Size(195, 25)
        Me.Cmb_Kolom.TabIndex = 338
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(512, 11)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(80, 28)
        Me.Btn_Cari.TabIndex = 340
        Me.Btn_Cari.Text = "Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Lbl_Value
        '
        Me.Lbl_Value.AutoSize = True
        Me.Lbl_Value.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Value.Location = New System.Drawing.Point(265, 16)
        Me.Lbl_Value.Name = "Lbl_Value"
        Me.Lbl_Value.Size = New System.Drawing.Size(46, 20)
        Me.Lbl_Value.TabIndex = 342
        Me.Lbl_Value.Text = "Value"
        '
        'Lbl_Kolom
        '
        Me.Lbl_Kolom.AutoSize = True
        Me.Lbl_Kolom.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Kolom.Location = New System.Drawing.Point(6, 16)
        Me.Lbl_Kolom.Name = "Lbl_Kolom"
        Me.Lbl_Kolom.Size = New System.Drawing.Size(50, 20)
        Me.Lbl_Kolom.TabIndex = 341
        Me.Lbl_Kolom.Text = "Kolom"
        '
        'Txt_Value
        '
        Me.Txt_Value.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Value.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Value.Location = New System.Drawing.Point(317, 15)
        Me.Txt_Value.MaxLength = 50
        Me.Txt_Value.Name = "Txt_Value"
        Me.Txt_Value.Size = New System.Drawing.Size(189, 22)
        Me.Txt_Value.TabIndex = 339
        '
        'Lv_WorkCenter
        '
        Me.Lv_WorkCenter.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_WorkCenter.FullRowSelect = True
        Me.Lv_WorkCenter.GridLines = True
        Me.Lv_WorkCenter.HideSelection = False
        Me.Lv_WorkCenter.Location = New System.Drawing.Point(7, 43)
        Me.Lv_WorkCenter.Name = "Lv_WorkCenter"
        Me.Lv_WorkCenter.Size = New System.Drawing.Size(639, 259)
        Me.Lv_WorkCenter.TabIndex = 343
        Me.Lv_WorkCenter.UseCompatibleStateImageBehavior = False
        Me.Lv_WorkCenter.View = System.Windows.Forms.View.Details
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(679, 56)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 515)
        Me.Panel5.TabIndex = 344
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(-247, 537)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1436, 15)
        Me.Panel4.TabIndex = 345
        Me.Panel4.Visible = False
        '
        'Lbl_IDWorkCenter
        '
        Me.Lbl_IDWorkCenter.AutoSize = True
        Me.Lbl_IDWorkCenter.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_IDWorkCenter.Location = New System.Drawing.Point(458, 84)
        Me.Lbl_IDWorkCenter.Name = "Lbl_IDWorkCenter"
        Me.Lbl_IDWorkCenter.Size = New System.Drawing.Size(114, 20)
        Me.Lbl_IDWorkCenter.TabIndex = 346
        Me.Lbl_IDWorkCenter.Text = "Id Work Center"
        Me.Lbl_IDWorkCenter.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Lbl_Kolom)
        Me.GroupBox1.Controls.Add(Me.Txt_Value)
        Me.GroupBox1.Controls.Add(Me.Lbl_Value)
        Me.GroupBox1.Controls.Add(Me.Btn_Cari)
        Me.GroupBox1.Controls.Add(Me.Lv_WorkCenter)
        Me.GroupBox1.Controls.Add(Me.Cmb_Kolom)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 217)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(652, 318)
        Me.GroupBox1.TabIndex = 347
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Display"
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
        Me.PanelGradient1.Size = New System.Drawing.Size(699, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Master_Work_Center
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(699, 554)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Lbl_IDWorkCenter)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Hapus)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Txt_Keterangan)
        Me.Controls.Add(Me.Lbl_Keterangan)
        Me.Controls.Add(Me.Txt_Kd)
        Me.Controls.Add(Me.Lbl_Kd)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Master_Work_Center"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Txt_Kd As TextBox
    Friend WithEvents Lbl_Kd As Label
    Friend WithEvents Lbl_Keterangan As Label
    Friend WithEvents Txt_Keterangan As TextBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Hapus As Button
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Cmb_Kolom As ComboBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Lbl_Value As Label
    Friend WithEvents Lbl_Kolom As Label
    Friend WithEvents Txt_Value As TextBox
    Friend WithEvents Lv_WorkCenter As ListView
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Lbl_IDWorkCenter As Label
    Friend WithEvents GroupBox1 As GroupBox
End Class
