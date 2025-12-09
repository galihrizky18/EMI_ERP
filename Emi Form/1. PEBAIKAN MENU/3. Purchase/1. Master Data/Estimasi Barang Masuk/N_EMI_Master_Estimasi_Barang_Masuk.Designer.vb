<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Master_Estimasi_Barang_Masuk
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
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Lbl_Kd = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Txt_Kd_Barang = New System.Windows.Forms.TextBox()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Hapus = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Txt_Nm_Barang = New System.Windows.Forms.TextBox()
        Me.Cmb_Jenis_Supplier = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_Waktu_Pabrikasi = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txt_Waktu_Pengiriman = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Lv_Data = New System.Windows.Forms.ListView()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Lv_Barang = New System.Windows.Forms.ListView()
        Me.Panel1.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(884, 45)
        Me.Panel1.TabIndex = 24
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
        Me.PanelGradient1.Size = New System.Drawing.Size(884, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Lbl_Judul.Location = New System.Drawing.Point(20, 11)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(324, 25)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Master - Estimasi Barang Masuk"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(2, 46)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 36
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 73)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 498)
        Me.Panel3.TabIndex = 37
        Me.Panel3.Visible = False
        '
        'Lbl_Kd
        '
        Me.Lbl_Kd.AutoSize = True
        Me.Lbl_Kd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Lbl_Kd.Location = New System.Drawing.Point(21, 62)
        Me.Lbl_Kd.Name = "Lbl_Kd"
        Me.Lbl_Kd.Size = New System.Drawing.Size(85, 15)
        Me.Lbl_Kd.TabIndex = 371
        Me.Lbl_Kd.Text = "Jenis Supplier"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(21, 89)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 15)
        Me.Label1.TabIndex = 371
        Me.Label1.Text = "Kode Barang"
        '
        'Txt_Kd_Barang
        '
        Me.Txt_Kd_Barang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Kd_Barang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Kd_Barang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_Kd_Barang.Location = New System.Drawing.Point(140, 86)
        Me.Txt_Kd_Barang.MaxLength = 50
        Me.Txt_Kd_Barang.Name = "Txt_Kd_Barang"
        Me.Txt_Kd_Barang.Size = New System.Drawing.Size(163, 21)
        Me.Txt_Kd_Barang.TabIndex = 1
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(228, 176)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(85, 30)
        Me.Btn_Refresh.TabIndex = 7
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Hapus
        '
        Me.Btn_Hapus.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Hapus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Hapus.ForeColor = System.Drawing.Color.White
        Me.Btn_Hapus.Location = New System.Drawing.Point(316, 176)
        Me.Btn_Hapus.Name = "Btn_Hapus"
        Me.Btn_Hapus.Size = New System.Drawing.Size(85, 30)
        Me.Btn_Hapus.TabIndex = 6
        Me.Btn_Hapus.Text = "&Hapus"
        Me.Btn_Hapus.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(140, 176)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(85, 30)
        Me.Btn_Simpan.TabIndex = 5
        Me.Btn_Simpan.Tag = "SIMPAN"
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Txt_Nm_Barang
        '
        Me.Txt_Nm_Barang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Nm_Barang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Nm_Barang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_Nm_Barang.Location = New System.Drawing.Point(309, 86)
        Me.Txt_Nm_Barang.MaxLength = 50
        Me.Txt_Nm_Barang.Name = "Txt_Nm_Barang"
        Me.Txt_Nm_Barang.Size = New System.Drawing.Size(412, 21)
        Me.Txt_Nm_Barang.TabIndex = 2
        '
        'Cmb_Jenis_Supplier
        '
        Me.Cmb_Jenis_Supplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Jenis_Supplier.FormattingEnabled = True
        Me.Cmb_Jenis_Supplier.Location = New System.Drawing.Point(140, 61)
        Me.Cmb_Jenis_Supplier.Name = "Cmb_Jenis_Supplier"
        Me.Cmb_Jenis_Supplier.Size = New System.Drawing.Size(163, 21)
        Me.Cmb_Jenis_Supplier.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(21, 116)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 15)
        Me.Label2.TabIndex = 371
        Me.Label2.Text = "Waktu Pabrikasi"
        '
        'Txt_Waktu_Pabrikasi
        '
        Me.Txt_Waktu_Pabrikasi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Waktu_Pabrikasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Waktu_Pabrikasi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_Waktu_Pabrikasi.Location = New System.Drawing.Point(140, 113)
        Me.Txt_Waktu_Pabrikasi.MaxLength = 50
        Me.Txt_Waktu_Pabrikasi.Name = "Txt_Waktu_Pabrikasi"
        Me.Txt_Waktu_Pabrikasi.Size = New System.Drawing.Size(163, 21)
        Me.Txt_Waktu_Pabrikasi.TabIndex = 3
        Me.Txt_Waktu_Pabrikasi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(21, 143)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(108, 15)
        Me.Label3.TabIndex = 371
        Me.Label3.Text = "Waktu Pengiriman"
        '
        'Txt_Waktu_Pengiriman
        '
        Me.Txt_Waktu_Pengiriman.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Waktu_Pengiriman.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Waktu_Pengiriman.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_Waktu_Pengiriman.Location = New System.Drawing.Point(140, 140)
        Me.Txt_Waktu_Pengiriman.MaxLength = 50
        Me.Txt_Waktu_Pengiriman.Name = "Txt_Waktu_Pengiriman"
        Me.Txt_Waktu_Pengiriman.Size = New System.Drawing.Size(163, 21)
        Me.Txt_Waktu_Pengiriman.TabIndex = 4
        Me.Txt_Waktu_Pengiriman.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox5
        '
        Me.TextBox5.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox5.Enabled = False
        Me.TextBox5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TextBox5.Location = New System.Drawing.Point(309, 113)
        Me.TextBox5.MaxLength = 50
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(89, 21)
        Me.TextBox5.TabIndex = 372
        Me.TextBox5.Text = "Hari"
        Me.TextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox6
        '
        Me.TextBox6.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox6.Enabled = False
        Me.TextBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TextBox6.Location = New System.Drawing.Point(309, 140)
        Me.TextBox6.MaxLength = 50
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(89, 21)
        Me.TextBox6.TabIndex = 372
        Me.TextBox6.Text = "Hari"
        Me.TextBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(20, 163)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(942, 12)
        Me.Panel4.TabIndex = 36
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(23, 207)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(942, 12)
        Me.Panel5.TabIndex = 36
        Me.Panel5.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(25, 596)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(942, 15)
        Me.Panel6.TabIndex = 36
        Me.Panel6.Visible = False
        '
        'Lv_Data
        '
        Me.Lv_Data.FullRowSelect = True
        Me.Lv_Data.GridLines = True
        Me.Lv_Data.HideSelection = False
        Me.Lv_Data.Location = New System.Drawing.Point(20, 221)
        Me.Lv_Data.Name = "Lv_Data"
        Me.Lv_Data.Size = New System.Drawing.Size(845, 378)
        Me.Lv_Data.TabIndex = 8
        Me.Lv_Data.UseCompatibleStateImageBehavior = False
        Me.Lv_Data.View = System.Windows.Forms.View.Details
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(865, 66)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(19, 498)
        Me.Panel7.TabIndex = 37
        Me.Panel7.Visible = False
        '
        'Lv_Barang
        '
        Me.Lv_Barang.FullRowSelect = True
        Me.Lv_Barang.GridLines = True
        Me.Lv_Barang.HideSelection = False
        Me.Lv_Barang.Location = New System.Drawing.Point(890, 110)
        Me.Lv_Barang.Name = "Lv_Barang"
        Me.Lv_Barang.Size = New System.Drawing.Size(581, 227)
        Me.Lv_Barang.TabIndex = 377
        Me.Lv_Barang.UseCompatibleStateImageBehavior = False
        Me.Lv_Barang.View = System.Windows.Forms.View.Details
        '
        'N_EMI_Master_Estimasi_Barang_Masuk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(884, 611)
        Me.Controls.Add(Me.Lv_Barang)
        Me.Controls.Add(Me.Lv_Data)
        Me.Controls.Add(Me.Cmb_Jenis_Supplier)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Hapus)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Txt_Nm_Barang)
        Me.Controls.Add(Me.Txt_Waktu_Pengiriman)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBox6)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.Txt_Waktu_Pabrikasi)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Txt_Kd_Barang)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Lbl_Kd)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "N_EMI_Master_Estimasi_Barang_Masuk"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Lbl_Kd As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_Kd_Barang As TextBox
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Hapus As Button
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Txt_Nm_Barang As TextBox
    Friend WithEvents Cmb_Jenis_Supplier As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_Waktu_Pabrikasi As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Txt_Waktu_Pengiriman As TextBox
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Lv_Data As ListView
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Lv_Barang As ListView
End Class
