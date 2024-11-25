<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Emi_Selisih_Barang_Masuk_SD_DN2
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
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Txt_NoPO = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Cmb_Rak = New System.Windows.Forms.ComboBox()
        Me.Txt_Satuan = New System.Windows.Forms.TextBox()
        Me.Txt_Lokasi = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Txt_SelisihQty = New System.Windows.Forms.TextBox()
        Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
        Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Simpan = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(490, 51)
        Me.Panel1.TabIndex = 85
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
        Me.PanelGradient1.Size = New System.Drawing.Size(490, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(14, 9)
        Me.Label11.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(166, 25)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Set Posisi Rak"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(7, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 86
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 71)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 315)
        Me.Panel3.TabIndex = 90
        Me.Panel3.Visible = False
        '
        'Txt_NoPO
        '
        Me.Txt_NoPO.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NoPO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NoPO.Enabled = False
        Me.Txt_NoPO.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Txt_NoPO.Location = New System.Drawing.Point(92, 66)
        Me.Txt_NoPO.Name = "Txt_NoPO"
        Me.Txt_NoPO.Size = New System.Drawing.Size(227, 20)
        Me.Txt_NoPO.TabIndex = 92
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label9.Location = New System.Drawing.Point(21, 65)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 16)
        Me.Label9.TabIndex = 91
        Me.Label9.Text = "No PO"
        '
        'Cmb_Rak
        '
        Me.Cmb_Rak.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Rak.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_Rak.FormattingEnabled = True
        Me.Cmb_Rak.Location = New System.Drawing.Point(151, 177)
        Me.Cmb_Rak.Name = "Cmb_Rak"
        Me.Cmb_Rak.Size = New System.Drawing.Size(317, 23)
        Me.Cmb_Rak.TabIndex = 109
        '
        'Txt_Satuan
        '
        Me.Txt_Satuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Satuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Satuan.Enabled = False
        Me.Txt_Satuan.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Txt_Satuan.Location = New System.Drawing.Point(279, 103)
        Me.Txt_Satuan.Name = "Txt_Satuan"
        Me.Txt_Satuan.Size = New System.Drawing.Size(40, 20)
        Me.Txt_Satuan.TabIndex = 108
        Me.Txt_Satuan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Txt_Lokasi
        '
        Me.Txt_Lokasi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Lokasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Lokasi.Enabled = False
        Me.Txt_Lokasi.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Txt_Lokasi.Location = New System.Drawing.Point(151, 127)
        Me.Txt_Lokasi.Name = "Txt_Lokasi"
        Me.Txt_Lokasi.Size = New System.Drawing.Size(317, 20)
        Me.Txt_Lokasi.TabIndex = 106
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label10.Location = New System.Drawing.Point(21, 126)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(47, 16)
        Me.Label10.TabIndex = 105
        Me.Label10.Text = "Lokasi"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label8.Location = New System.Drawing.Point(360, 103)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(10, 16)
        Me.Label8.TabIndex = 104
        Me.Label8.Text = " "
        '
        'Txt_SelisihQty
        '
        Me.Txt_SelisihQty.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_SelisihQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_SelisihQty.Enabled = False
        Me.Txt_SelisihQty.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Txt_SelisihQty.Location = New System.Drawing.Point(151, 101)
        Me.Txt_SelisihQty.Name = "Txt_SelisihQty"
        Me.Txt_SelisihQty.Size = New System.Drawing.Size(125, 20)
        Me.Txt_SelisihQty.TabIndex = 102
        '
        'Txt_NmBarang
        '
        Me.Txt_NmBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NmBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NmBarang.Enabled = False
        Me.Txt_NmBarang.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Txt_NmBarang.Location = New System.Drawing.Point(279, 153)
        Me.Txt_NmBarang.Name = "Txt_NmBarang"
        Me.Txt_NmBarang.Size = New System.Drawing.Size(189, 20)
        Me.Txt_NmBarang.TabIndex = 101
        '
        'Txt_KdBarang
        '
        Me.Txt_KdBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KdBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_KdBarang.Enabled = False
        Me.Txt_KdBarang.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Txt_KdBarang.Location = New System.Drawing.Point(151, 153)
        Me.Txt_KdBarang.Name = "Txt_KdBarang"
        Me.Txt_KdBarang.Size = New System.Drawing.Size(125, 20)
        Me.Txt_KdBarang.TabIndex = 95
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label2.Location = New System.Drawing.Point(21, 103)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 16)
        Me.Label2.TabIndex = 98
        Me.Label2.Text = "Jumlah Selisih"
        '
        'Simpan
        '
        Me.Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Simpan.ForeColor = System.Drawing.Color.White
        Me.Simpan.Location = New System.Drawing.Point(148, 218)
        Me.Simpan.Name = "Simpan"
        Me.Simpan.Size = New System.Drawing.Size(126, 31)
        Me.Simpan.TabIndex = 96
        Me.Simpan.Text = "Simpan"
        Me.Simpan.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label1.Location = New System.Drawing.Point(21, 152)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 16)
        Me.Label1.TabIndex = 97
        Me.Label1.Text = "Kode Barang"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(469, 71)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 315)
        Me.Panel4.TabIndex = 90
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(24, 248)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(942, 12)
        Me.Panel5.TabIndex = 86
        Me.Panel5.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label7.Location = New System.Drawing.Point(21, 179)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(32, 16)
        Me.Label7.TabIndex = 110
        Me.Label7.Text = "Rak"
        '
        'Emi_Selisih_Barang_Masuk_SD_DN2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(490, 261)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Cmb_Rak)
        Me.Controls.Add(Me.Txt_Satuan)
        Me.Controls.Add(Me.Txt_Lokasi)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Txt_SelisihQty)
        Me.Controls.Add(Me.Txt_NmBarang)
        Me.Controls.Add(Me.Txt_KdBarang)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Simpan)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Txt_NoPO)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Emi_Selisih_Barang_Masuk_SD_DN2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label11 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Txt_NoPO As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Cmb_Rak As ComboBox
    Friend WithEvents Txt_Satuan As TextBox
    Friend WithEvents Txt_Lokasi As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Txt_SelisihQty As TextBox
    Friend WithEvents Txt_NmBarang As TextBox
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Simpan As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label7 As Label
End Class
