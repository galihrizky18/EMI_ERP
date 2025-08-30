<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SD_Pengajuan_Selesai_PR_Barang_Lain
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_NoPR = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txt_KdBrang = New System.Windows.Forms.TextBox()
        Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Txt_SisaPR = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DTP_TglEstimasi = New System.Windows.Forms.DateTimePicker()
        Me.Cmd_SatuanSisa = New System.Windows.Forms.ComboBox()
        Me.Txt_KdSo = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.BtnPO_Simpan = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.DTP_TglDelivery = New System.Windows.Forms.DateTimePicker()
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
        Me.Panel1.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(622, 55)
        Me.Panel1.TabIndex = 23
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 53)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(622, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 14)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(311, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Pengajuan Penyelesaian PR"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(2, 55)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1256, 15)
        Me.Panel2.TabIndex = 35
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(2, 65)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(14, 639)
        Me.Panel3.TabIndex = 36
        Me.Panel3.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(8, 20)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 17)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "No PR"
        '
        'Txt_NoPR
        '
        Me.Txt_NoPR.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NoPR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NoPR.Enabled = False
        Me.Txt_NoPR.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Txt_NoPR.Location = New System.Drawing.Point(137, 17)
        Me.Txt_NoPR.MaxLength = 100
        Me.Txt_NoPR.Name = "Txt_NoPR"
        Me.Txt_NoPR.Size = New System.Drawing.Size(436, 24)
        Me.Txt_NoPR.TabIndex = 38
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(8, 80)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 17)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "Kode Barang"
        '
        'Txt_KdBrang
        '
        Me.Txt_KdBrang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KdBrang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_KdBrang.Enabled = False
        Me.Txt_KdBrang.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Txt_KdBrang.Location = New System.Drawing.Point(137, 77)
        Me.Txt_KdBrang.MaxLength = 100
        Me.Txt_KdBrang.Name = "Txt_KdBrang"
        Me.Txt_KdBrang.Size = New System.Drawing.Size(125, 24)
        Me.Txt_KdBrang.TabIndex = 38
        '
        'Txt_NmBarang
        '
        Me.Txt_NmBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NmBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NmBarang.Enabled = False
        Me.Txt_NmBarang.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Txt_NmBarang.Location = New System.Drawing.Point(265, 77)
        Me.Txt_NmBarang.MaxLength = 100
        Me.Txt_NmBarang.Name = "Txt_NmBarang"
        Me.Txt_NmBarang.Size = New System.Drawing.Size(308, 24)
        Me.Txt_NmBarang.TabIndex = 38
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(8, 109)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 17)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "Sisa PR"
        '
        'Txt_SisaPR
        '
        Me.Txt_SisaPR.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_SisaPR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_SisaPR.Enabled = False
        Me.Txt_SisaPR.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Txt_SisaPR.Location = New System.Drawing.Point(137, 106)
        Me.Txt_SisaPR.MaxLength = 100
        Me.Txt_SisaPR.Name = "Txt_SisaPR"
        Me.Txt_SisaPR.Size = New System.Drawing.Size(205, 24)
        Me.Txt_SisaPR.TabIndex = 38
        Me.Txt_SisaPR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(8, 138)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(109, 17)
        Me.Label5.TabIndex = 37
        Me.Label5.Text = "Tanggal Delivery"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DTP_TglDelivery)
        Me.GroupBox1.Controls.Add(Me.DTP_TglEstimasi)
        Me.GroupBox1.Controls.Add(Me.Cmd_SatuanSisa)
        Me.GroupBox1.Controls.Add(Me.Txt_KdSo)
        Me.GroupBox1.Controls.Add(Me.Txt_NoPR)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.Txt_NmBarang)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Txt_Keterangan)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Txt_KdBrang)
        Me.GroupBox1.Controls.Add(Me.Txt_SisaPR)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 62)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(590, 230)
        Me.GroupBox1.TabIndex = 39
        Me.GroupBox1.TabStop = False
        '
        'DTP_TglEstimasi
        '
        Me.DTP_TglEstimasi.AccessibleDescription = ""
        Me.DTP_TglEstimasi.Enabled = False
        Me.DTP_TglEstimasi.Location = New System.Drawing.Point(137, 166)
        Me.DTP_TglEstimasi.Name = "DTP_TglEstimasi"
        Me.DTP_TglEstimasi.Size = New System.Drawing.Size(283, 23)
        Me.DTP_TglEstimasi.TabIndex = 42
        '
        'Cmd_SatuanSisa
        '
        Me.Cmd_SatuanSisa.Enabled = False
        Me.Cmd_SatuanSisa.FormattingEnabled = True
        Me.Cmd_SatuanSisa.Location = New System.Drawing.Point(346, 106)
        Me.Cmd_SatuanSisa.Name = "Cmd_SatuanSisa"
        Me.Cmd_SatuanSisa.Size = New System.Drawing.Size(74, 24)
        Me.Cmd_SatuanSisa.TabIndex = 40
        '
        'Txt_KdSo
        '
        Me.Txt_KdSo.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KdSo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_KdSo.Enabled = False
        Me.Txt_KdSo.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Txt_KdSo.Location = New System.Drawing.Point(137, 47)
        Me.Txt_KdSo.MaxLength = 100
        Me.Txt_KdSo.Name = "Txt_KdSo"
        Me.Txt_KdSo.Size = New System.Drawing.Size(436, 24)
        Me.Txt_KdSo.TabIndex = 38
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Label21.Location = New System.Drawing.Point(8, 50)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(45, 17)
        Me.Label21.TabIndex = 37
        Me.Label21.Text = "Lokasi"
        '
        'Txt_Keterangan
        '
        Me.Txt_Keterangan.BackColor = System.Drawing.Color.White
        Me.Txt_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Keterangan.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Txt_Keterangan.Location = New System.Drawing.Point(137, 195)
        Me.Txt_Keterangan.MaxLength = 255
        Me.Txt_Keterangan.Name = "Txt_Keterangan"
        Me.Txt_Keterangan.Size = New System.Drawing.Size(436, 24)
        Me.Txt_Keterangan.TabIndex = 38
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Label7.Location = New System.Drawing.Point(8, 198)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(78, 17)
        Me.Label7.TabIndex = 37
        Me.Label7.Text = "Keterangan"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(8, 168)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(108, 17)
        Me.Label6.TabIndex = 37
        Me.Label6.Text = "Tanggal Estimasi"
        '
        'BtnPO_Simpan
        '
        Me.BtnPO_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnPO_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnPO_Simpan.ForeColor = System.Drawing.Color.White
        Me.BtnPO_Simpan.Location = New System.Drawing.Point(29, 298)
        Me.BtnPO_Simpan.Name = "BtnPO_Simpan"
        Me.BtnPO_Simpan.Size = New System.Drawing.Size(94, 36)
        Me.BtnPO_Simpan.TabIndex = 348
        Me.BtnPO_Simpan.Text = "&Simpan"
        Me.BtnPO_Simpan.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(126, 298)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(94, 36)
        Me.Button1.TabIndex = 348
        Me.Button1.Text = "&Keluar"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(20, 335)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1256, 15)
        Me.Panel4.TabIndex = 35
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(610, 79)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(14, 639)
        Me.Panel5.TabIndex = 36
        Me.Panel5.Visible = False
        '
        'DTP_TglDelivery
        '
        Me.DTP_TglDelivery.AccessibleDescription = ""
        Me.DTP_TglDelivery.Enabled = False
        Me.DTP_TglDelivery.Location = New System.Drawing.Point(137, 136)
        Me.DTP_TglDelivery.Name = "DTP_TglDelivery"
        Me.DTP_TglDelivery.Size = New System.Drawing.Size(283, 23)
        Me.DTP_TglDelivery.TabIndex = 42
        '
        'SD_Pengajuan_Selesai_PR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(622, 346)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.BtnPO_Simpan)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "SD_Pengajuan_Selesai_PR"
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
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_NoPR As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Txt_KdBrang As TextBox
    Friend WithEvents Txt_NmBarang As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Txt_SisaPR As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Cmd_SatuanSisa As ComboBox
    Friend WithEvents Txt_Keterangan As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents BtnPO_Simpan As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Txt_KdSo As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents DTP_TglEstimasi As DateTimePicker
    Friend WithEvents DTP_TglDelivery As DateTimePicker
End Class
