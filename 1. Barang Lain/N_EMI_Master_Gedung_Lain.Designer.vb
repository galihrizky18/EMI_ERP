<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Master_Gedung_Lain
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
		Me.Labeljudul = New System.Windows.Forms.Label()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Txt_NmGedung = New System.Windows.Forms.TextBox()
		Me.Lbl_Kd = New System.Windows.Forms.Label()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.Btn_Refresh = New System.Windows.Forms.Button()
		Me.Btn_Hapus = New System.Windows.Forms.Button()
		Me.Btn_Simpan = New System.Windows.Forms.Button()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Txt_SelectedID = New System.Windows.Forms.TextBox()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Lv_DataGedung = New System.Windows.Forms.ListView()
		Me.Cmb_Filter = New System.Windows.Forms.ComboBox()
		Me.Btn_Cari = New System.Windows.Forms.Button()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Txt_ValueFilter = New System.Windows.Forms.TextBox()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.Panel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.PanelGradient1)
		Me.Panel1.Controls.Add(Me.Labeljudul)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(679, 45)
		Me.Panel1.TabIndex = 25
		'
		'PanelGradient1
		'
		Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
		Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
		Me.PanelGradient1.cuteTransparent1 = 100
		Me.PanelGradient1.cuteTransparent2 = 64
		Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.PanelGradient1.Location = New System.Drawing.Point(0, 43)
		Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
		Me.PanelGradient1.Name = "PanelGradient1"
		Me.PanelGradient1.Size = New System.Drawing.Size(679, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Labeljudul
		'
		Me.Labeljudul.AutoSize = True
		Me.Labeljudul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold)
		Me.Labeljudul.Location = New System.Drawing.Point(20, 6)
		Me.Labeljudul.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Labeljudul.Name = "Labeljudul"
		Me.Labeljudul.Size = New System.Drawing.Size(290, 29)
		Me.Labeljudul.TabIndex = 0
		Me.Labeljudul.Text = "Master Data - Gedung Asset"
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(1, 40)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(1227, 12)
		Me.Panel2.TabIndex = 37
		Me.Panel2.Visible = False
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Location = New System.Drawing.Point(1, 50)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 877)
		Me.Panel3.TabIndex = 38
		Me.Panel3.Visible = False
		'
		'Txt_Keterangan
		'
		Me.Txt_Keterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Keterangan.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Keterangan.Location = New System.Drawing.Point(121, 77)
		Me.Txt_Keterangan.MaxLength = 100
		Me.Txt_Keterangan.Name = "Txt_Keterangan"
		Me.Txt_Keterangan.Size = New System.Drawing.Size(259, 20)
		Me.Txt_Keterangan.TabIndex = 353
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label1.Location = New System.Drawing.Point(21, 79)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(73, 17)
		Me.Label1.TabIndex = 354
		Me.Label1.Text = "Keterangan"
		'
		'Txt_NmGedung
		'
		Me.Txt_NmGedung.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_NmGedung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_NmGedung.Enabled = False
		Me.Txt_NmGedung.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_NmGedung.Location = New System.Drawing.Point(121, 51)
		Me.Txt_NmGedung.MaxLength = 50
		Me.Txt_NmGedung.Name = "Txt_NmGedung"
		Me.Txt_NmGedung.Size = New System.Drawing.Size(259, 20)
		Me.Txt_NmGedung.TabIndex = 352
		'
		'Lbl_Kd
		'
		Me.Lbl_Kd.AutoSize = True
		Me.Lbl_Kd.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Lbl_Kd.Location = New System.Drawing.Point(21, 53)
		Me.Lbl_Kd.Name = "Lbl_Kd"
		Me.Lbl_Kd.Size = New System.Drawing.Size(85, 17)
		Me.Lbl_Kd.TabIndex = 355
		Me.Lbl_Kd.Text = "Kode Gedung"
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.Color.Red
		Me.Panel6.Location = New System.Drawing.Point(19, 98)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(1227, 12)
		Me.Panel6.TabIndex = 356
		Me.Panel6.Visible = False
		'
		'Btn_Refresh
		'
		Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
		Me.Btn_Refresh.Location = New System.Drawing.Point(297, 111)
		Me.Btn_Refresh.Name = "Btn_Refresh"
		Me.Btn_Refresh.Size = New System.Drawing.Size(85, 30)
		Me.Btn_Refresh.TabIndex = 359
		Me.Btn_Refresh.Text = "&Refresh"
		Me.Btn_Refresh.UseVisualStyleBackColor = False
		'
		'Btn_Hapus
		'
		Me.Btn_Hapus.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Hapus.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Hapus.ForeColor = System.Drawing.Color.White
		Me.Btn_Hapus.Location = New System.Drawing.Point(209, 111)
		Me.Btn_Hapus.Name = "Btn_Hapus"
		Me.Btn_Hapus.Size = New System.Drawing.Size(85, 30)
		Me.Btn_Hapus.TabIndex = 358
		Me.Btn_Hapus.Text = "&Hapus"
		Me.Btn_Hapus.UseVisualStyleBackColor = False
		'
		'Btn_Simpan
		'
		Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
		Me.Btn_Simpan.Location = New System.Drawing.Point(121, 111)
		Me.Btn_Simpan.Name = "Btn_Simpan"
		Me.Btn_Simpan.Size = New System.Drawing.Size(85, 30)
		Me.Btn_Simpan.TabIndex = 357
		Me.Btn_Simpan.Tag = "SIMPAN"
		Me.Btn_Simpan.Text = "&Simpan"
		Me.Btn_Simpan.UseVisualStyleBackColor = False
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Location = New System.Drawing.Point(19, 143)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(1227, 12)
		Me.Panel4.TabIndex = 356
		Me.Panel4.Visible = False
		'
		'Txt_SelectedID
		'
		Me.Txt_SelectedID.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_SelectedID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_SelectedID.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_SelectedID.Location = New System.Drawing.Point(619, 53)
		Me.Txt_SelectedID.MaxLength = 50
		Me.Txt_SelectedID.Name = "Txt_SelectedID"
		Me.Txt_SelectedID.Size = New System.Drawing.Size(34, 20)
		Me.Txt_SelectedID.TabIndex = 360
		Me.Txt_SelectedID.Visible = False
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Location = New System.Drawing.Point(660, 51)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(19, 877)
		Me.Panel5.TabIndex = 38
		Me.Panel5.Visible = False
		'
		'Lv_DataGedung
		'
		Me.Lv_DataGedung.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv_DataGedung.FullRowSelect = True
		Me.Lv_DataGedung.GridLines = True
		Me.Lv_DataGedung.HideSelection = False
		Me.Lv_DataGedung.Location = New System.Drawing.Point(20, 185)
		Me.Lv_DataGedung.Name = "Lv_DataGedung"
		Me.Lv_DataGedung.Size = New System.Drawing.Size(640, 307)
		Me.Lv_DataGedung.TabIndex = 364
		Me.Lv_DataGedung.UseCompatibleStateImageBehavior = False
		Me.Lv_DataGedung.View = System.Windows.Forms.View.Details
		'
		'Cmb_Filter
		'
		Me.Cmb_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Filter.DropDownWidth = 150
		Me.Cmb_Filter.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Cmb_Filter.FormattingEnabled = True
		Me.Cmb_Filter.Location = New System.Drawing.Point(74, 154)
		Me.Cmb_Filter.Margin = New System.Windows.Forms.Padding(2)
		Me.Cmb_Filter.Name = "Cmb_Filter"
		Me.Cmb_Filter.Size = New System.Drawing.Size(160, 24)
		Me.Cmb_Filter.TabIndex = 369
		'
		'Btn_Cari
		'
		Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Cari.ForeColor = System.Drawing.Color.White
		Me.Btn_Cari.Location = New System.Drawing.Point(483, 151)
		Me.Btn_Cari.Name = "Btn_Cari"
		Me.Btn_Cari.Size = New System.Drawing.Size(80, 28)
		Me.Btn_Cari.TabIndex = 368
		Me.Btn_Cari.Text = "Cari"
		Me.Btn_Cari.UseVisualStyleBackColor = False
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label5.Location = New System.Drawing.Point(245, 157)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(40, 17)
		Me.Label5.TabIndex = 367
		Me.Label5.Text = "Value"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label4.Location = New System.Drawing.Point(25, 157)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(44, 17)
		Me.Label4.TabIndex = 366
		Me.Label4.Text = "Kolom"
		'
		'Txt_ValueFilter
		'
		Me.Txt_ValueFilter.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_ValueFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_ValueFilter.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_ValueFilter.Location = New System.Drawing.Point(288, 155)
		Me.Txt_ValueFilter.MaxLength = 50
		Me.Txt_ValueFilter.Name = "Txt_ValueFilter"
		Me.Txt_ValueFilter.Size = New System.Drawing.Size(189, 20)
		Me.Txt_ValueFilter.TabIndex = 365
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.Color.Red
		Me.Panel7.Location = New System.Drawing.Point(19, 493)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(1227, 15)
		Me.Panel7.TabIndex = 356
		Me.Panel7.Visible = False
		'
		'N_EMI_Master_Gedung_Lain
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(679, 509)
		Me.Controls.Add(Me.Lv_DataGedung)
		Me.Controls.Add(Me.Cmb_Filter)
		Me.Controls.Add(Me.Btn_Cari)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.Txt_ValueFilter)
		Me.Controls.Add(Me.Txt_SelectedID)
		Me.Controls.Add(Me.Panel7)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Btn_Refresh)
		Me.Controls.Add(Me.Btn_Hapus)
		Me.Controls.Add(Me.Btn_Simpan)
		Me.Controls.Add(Me.Panel6)
		Me.Controls.Add(Me.Txt_Keterangan)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.Txt_NmGedung)
		Me.Controls.Add(Me.Lbl_Kd)
		Me.Controls.Add(Me.Panel5)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel1)
		Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.Name = "N_EMI_Master_Gedung_Lain"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Labeljudul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Txt_Keterangan As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_NmGedung As TextBox
    Friend WithEvents Lbl_Kd As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Hapus As Button
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Txt_SelectedID As TextBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Lv_DataGedung As ListView
    Friend WithEvents Cmb_Filter As ComboBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Txt_ValueFilter As TextBox
    Friend WithEvents Panel7 As Panel
End Class
