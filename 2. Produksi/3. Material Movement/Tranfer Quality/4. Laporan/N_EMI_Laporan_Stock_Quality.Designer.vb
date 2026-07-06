<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class N_EMI_Laporan_Stock_Quality
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
		Me.Label11 = New System.Windows.Forms.Label()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.BtnCetak = New System.Windows.Forms.Button()
		Me.BtnExit = New System.Windows.Forms.Button()
		Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
		Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Cmb_Kualitas = New System.Windows.Forms.ComboBox()
		Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
		Me.Txt_Jenis = New System.Windows.Forms.TextBox()
		Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
		Me.Cmb_Tgl = New System.Windows.Forms.ComboBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Lv_Jenis = New System.Windows.Forms.ListView()
		Me.Lv_Barang = New System.Windows.Forms.ListView()
		Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
		Me.txt_IdJenis = New System.Windows.Forms.TextBox()
		Me.Panel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.PanelGradient1)
		Me.Panel1.Controls.Add(Me.Label11)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(772, 37)
		Me.Panel1.TabIndex = 91
		'
		'PanelGradient1
		'
		Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
		Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
		Me.PanelGradient1.cuteTransparent1 = 100
		Me.PanelGradient1.cuteTransparent2 = 64
		Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.PanelGradient1.Location = New System.Drawing.Point(0, 35)
		Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
		Me.PanelGradient1.Name = "PanelGradient1"
		Me.PanelGradient1.Size = New System.Drawing.Size(772, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Label11
		'
		Me.Label11.AutoSize = True
		Me.Label11.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label11.Location = New System.Drawing.Point(15, 6)
		Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label11.Name = "Label11"
		Me.Label11.Size = New System.Drawing.Size(247, 58)
		Me.Label11.TabIndex = 0
		Me.Label11.Text = "Laporan - Stock Quality" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Location = New System.Drawing.Point(-53, 37)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(706, 10)
		Me.Panel5.TabIndex = 92
		Me.Panel5.Visible = False
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Location = New System.Drawing.Point(0, 43)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(14, 488)
		Me.Panel3.TabIndex = 93
		Me.Panel3.Visible = False
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(594, 38)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(14, 488)
		Me.Panel2.TabIndex = 94
		Me.Panel2.Visible = False
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Location = New System.Drawing.Point(0, 257)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(706, 12)
		Me.Panel4.TabIndex = 95
		Me.Panel4.Visible = False
		'
		'BtnCetak
		'
		Me.BtnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.BtnCetak.Cursor = System.Windows.Forms.Cursors.Hand
		Me.BtnCetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.BtnCetak.ForeColor = System.Drawing.Color.White
		Me.BtnCetak.Location = New System.Drawing.Point(312, 224)
		Me.BtnCetak.Margin = New System.Windows.Forms.Padding(2)
		Me.BtnCetak.Name = "BtnCetak"
		Me.BtnCetak.Size = New System.Drawing.Size(130, 30)
		Me.BtnCetak.TabIndex = 95
		Me.BtnCetak.Text = "&Cetak"
		Me.BtnCetak.UseVisualStyleBackColor = False
		'
		'BtnExit
		'
		Me.BtnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand
		Me.BtnExit.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.BtnExit.ForeColor = System.Drawing.Color.White
		Me.BtnExit.Location = New System.Drawing.Point(459, 225)
		Me.BtnExit.Margin = New System.Windows.Forms.Padding(2)
		Me.BtnExit.Name = "BtnExit"
		Me.BtnExit.Size = New System.Drawing.Size(130, 30)
		Me.BtnExit.TabIndex = 96
		Me.BtnExit.Text = "&Keluar"
		Me.BtnExit.UseVisualStyleBackColor = False
		'
		'Tgl1
		'
		Me.Tgl1.CustomFormat = "dd MMMM yyyy"
		Me.Tgl1.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
		Me.Tgl1.Location = New System.Drawing.Point(213, 68)
		Me.Tgl1.Margin = New System.Windows.Forms.Padding(2)
		Me.Tgl1.Name = "Tgl1"
		Me.Tgl1.Size = New System.Drawing.Size(174, 22)
		Me.Tgl1.TabIndex = 97
		'
		'Tgl2
		'
		Me.Tgl2.CustomFormat = "dd MMMM yyyy"
		Me.Tgl2.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
		Me.Tgl2.Location = New System.Drawing.Point(415, 68)
		Me.Tgl2.Margin = New System.Windows.Forms.Padding(2)
		Me.Tgl2.Name = "Tgl2"
		Me.Tgl2.Size = New System.Drawing.Size(174, 22)
		Me.Tgl2.TabIndex = 98
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(388, 72)
		Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(23, 13)
		Me.Label3.TabIndex = 99
		Me.Label3.Text = "s/d"
		'
		'Cmb_Kualitas
		'
		Me.Cmb_Kualitas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Kualitas.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Cmb_Kualitas.FormattingEnabled = True
		Me.Cmb_Kualitas.Location = New System.Drawing.Point(88, 129)
		Me.Cmb_Kualitas.Margin = New System.Windows.Forms.Padding(2)
		Me.Cmb_Kualitas.Name = "Cmb_Kualitas"
		Me.Cmb_Kualitas.Size = New System.Drawing.Size(501, 24)
		Me.Cmb_Kualitas.TabIndex = 100
		'
		'Cmb_Lokasi
		'
		Me.Cmb_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Lokasi.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Cmb_Lokasi.FormattingEnabled = True
		Me.Cmb_Lokasi.Location = New System.Drawing.Point(88, 98)
		Me.Cmb_Lokasi.Margin = New System.Windows.Forms.Padding(2)
		Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
		Me.Cmb_Lokasi.Size = New System.Drawing.Size(501, 24)
		Me.Cmb_Lokasi.TabIndex = 102
		'
		'Txt_Jenis
		'
		Me.Txt_Jenis.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Jenis.Location = New System.Drawing.Point(88, 158)
		Me.Txt_Jenis.Margin = New System.Windows.Forms.Padding(2)
		Me.Txt_Jenis.Multiline = True
		Me.Txt_Jenis.Name = "Txt_Jenis"
		Me.Txt_Jenis.Size = New System.Drawing.Size(501, 25)
		Me.Txt_Jenis.TabIndex = 104
		'
		'Txt_NmBarang
		'
		Me.Txt_NmBarang.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_NmBarang.Location = New System.Drawing.Point(311, 191)
		Me.Txt_NmBarang.Margin = New System.Windows.Forms.Padding(2)
		Me.Txt_NmBarang.Multiline = True
		Me.Txt_NmBarang.Name = "Txt_NmBarang"
		Me.Txt_NmBarang.Size = New System.Drawing.Size(278, 25)
		Me.Txt_NmBarang.TabIndex = 107
		'
		'Cmb_Tgl
		'
		Me.Cmb_Tgl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Tgl.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Cmb_Tgl.FormattingEnabled = True
		Me.Cmb_Tgl.Location = New System.Drawing.Point(88, 67)
		Me.Cmb_Tgl.Margin = New System.Windows.Forms.Padding(2)
		Me.Cmb_Tgl.Name = "Cmb_Tgl"
		Me.Cmb_Tgl.Size = New System.Drawing.Size(123, 24)
		Me.Cmb_Tgl.TabIndex = 108
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(32, 162)
		Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(38, 17)
		Me.Label1.TabIndex = 110
		Me.Label1.Text = "Jenis"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label5.Location = New System.Drawing.Point(32, 132)
		Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(55, 17)
		Me.Label5.TabIndex = 111
		Me.Label5.Text = "Kualitas"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Location = New System.Drawing.Point(32, 101)
		Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(45, 17)
		Me.Label4.TabIndex = 112
		Me.Label4.Text = "Lokasi"
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label6.Location = New System.Drawing.Point(32, 195)
		Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(48, 17)
		Me.Label6.TabIndex = 113
		Me.Label6.Text = "Barang"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Location = New System.Drawing.Point(32, 68)
		Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(52, 17)
		Me.Label2.TabIndex = 109
		Me.Label2.Text = "Periode"
		'
		'Lv_Jenis
		'
		Me.Lv_Jenis.FullRowSelect = True
		Me.Lv_Jenis.HideSelection = False
		Me.Lv_Jenis.Location = New System.Drawing.Point(35, 299)
		Me.Lv_Jenis.Margin = New System.Windows.Forms.Padding(2)
		Me.Lv_Jenis.Name = "Lv_Jenis"
		Me.Lv_Jenis.Size = New System.Drawing.Size(501, 110)
		Me.Lv_Jenis.TabIndex = 114
		Me.Lv_Jenis.UseCompatibleStateImageBehavior = False
		Me.Lv_Jenis.Visible = False
		'
		'Lv_Barang
		'
		Me.Lv_Barang.FullRowSelect = True
		Me.Lv_Barang.HideSelection = False
		Me.Lv_Barang.Location = New System.Drawing.Point(96, 274)
		Me.Lv_Barang.Margin = New System.Windows.Forms.Padding(2)
		Me.Lv_Barang.Name = "Lv_Barang"
		Me.Lv_Barang.Size = New System.Drawing.Size(502, 115)
		Me.Lv_Barang.TabIndex = 115
		Me.Lv_Barang.UseCompatibleStateImageBehavior = False
		Me.Lv_Barang.Visible = False
		'
		'Txt_KdBarang
		'
		Me.Txt_KdBarang.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_KdBarang.Location = New System.Drawing.Point(88, 190)
		Me.Txt_KdBarang.Margin = New System.Windows.Forms.Padding(2)
		Me.Txt_KdBarang.Multiline = True
		Me.Txt_KdBarang.Name = "Txt_KdBarang"
		Me.Txt_KdBarang.Size = New System.Drawing.Size(220, 25)
		Me.Txt_KdBarang.TabIndex = 105
		'
		'txt_IdJenis
		'
		Me.txt_IdJenis.Location = New System.Drawing.Point(626, 278)
		Me.txt_IdJenis.Margin = New System.Windows.Forms.Padding(2)
		Me.txt_IdJenis.Name = "txt_IdJenis"
		Me.txt_IdJenis.Size = New System.Drawing.Size(122, 20)
		Me.txt_IdJenis.TabIndex = 116
		Me.txt_IdJenis.Visible = False
		'
		'N_EMI_Laporan_Stock_Quality
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(772, 391)
		Me.Controls.Add(Me.Lv_Barang)
		Me.Controls.Add(Me.txt_IdJenis)
		Me.Controls.Add(Me.Lv_Jenis)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.Label6)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Cmb_Tgl)
		Me.Controls.Add(Me.Cmb_Kualitas)
		Me.Controls.Add(Me.Cmb_Lokasi)
		Me.Controls.Add(Me.Txt_Jenis)
		Me.Controls.Add(Me.Txt_KdBarang)
		Me.Controls.Add(Me.Txt_NmBarang)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Tgl2)
		Me.Controls.Add(Me.Tgl1)
		Me.Controls.Add(Me.BtnExit)
		Me.Controls.Add(Me.BtnCetak)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel5)
		Me.Controls.Add(Me.Panel1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(2)
		Me.Name = "N_EMI_Laporan_Stock_Quality"
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label11 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents BtnCetak As Button
    Friend WithEvents BtnExit As Button
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents Tgl2 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Cmb_Kualitas As ComboBox
    Friend WithEvents Cmb_Lokasi As ComboBox
    Friend WithEvents Txt_Jenis As TextBox
    Friend WithEvents Txt_NmBarang As TextBox
    Friend WithEvents Cmb_Tgl As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Lv_Jenis As ListView
    Friend WithEvents Lv_Barang As ListView
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents txt_IdJenis As TextBox
End Class
