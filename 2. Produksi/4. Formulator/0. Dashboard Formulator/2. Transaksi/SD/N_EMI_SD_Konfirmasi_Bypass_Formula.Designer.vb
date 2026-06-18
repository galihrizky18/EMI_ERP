<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_SD_Konfirmasi_Bypass_Formula
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
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.LblFormulator_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.CMB_SatuanHasil = New System.Windows.Forms.ComboBox()
        Me.TXT_NamaBarang = New System.Windows.Forms.TextBox()
        Me.TXT_QtyHasil = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TXT_KodeBarang = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TXT_NoFormula = New System.Windows.Forms.TextBox()
        Me.LblFormulator_KodeBarang = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.TXT_Keterangan = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.BTN_Simpan = New System.Windows.Forms.Button()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Panel5)
        Me.Panel1.Controls.Add(Me.LblFormulator_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(629, 51)
        Me.Panel1.TabIndex = 24
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
        Me.PanelGradient1.Size = New System.Drawing.Size(629, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1279, 49)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 416)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'LblFormulator_Judul
        '
        Me.LblFormulator_Judul.AutoSize = True
        Me.LblFormulator_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold)
        Me.LblFormulator_Judul.Location = New System.Drawing.Point(15, 10)
        Me.LblFormulator_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LblFormulator_Judul.Name = "LblFormulator_Judul"
        Me.LblFormulator_Judul.Size = New System.Drawing.Size(280, 29)
        Me.LblFormulator_Judul.TabIndex = 0
        Me.LblFormulator_Judul.Text = "Konfirmasi Bypass Formula"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(653, 12)
        Me.Panel2.TabIndex = 36
        Me.Panel2.Visible = False
        '
        'CMB_SatuanHasil
        '
        Me.CMB_SatuanHasil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMB_SatuanHasil.Enabled = False
        Me.CMB_SatuanHasil.FormattingEnabled = True
        Me.CMB_SatuanHasil.Items.AddRange(New Object() {"Ali"})
        Me.CMB_SatuanHasil.Location = New System.Drawing.Point(227, 117)
        Me.CMB_SatuanHasil.Name = "CMB_SatuanHasil"
        Me.CMB_SatuanHasil.Size = New System.Drawing.Size(65, 21)
        Me.CMB_SatuanHasil.TabIndex = 388
        '
        'TXT_NamaBarang
        '
        Me.TXT_NamaBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TXT_NamaBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TXT_NamaBarang.Enabled = False
        Me.TXT_NamaBarang.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.TXT_NamaBarang.Location = New System.Drawing.Point(227, 92)
        Me.TXT_NamaBarang.MaxLength = 50
        Me.TXT_NamaBarang.Name = "TXT_NamaBarang"
        Me.TXT_NamaBarang.Size = New System.Drawing.Size(376, 20)
        Me.TXT_NamaBarang.TabIndex = 387
        '
        'TXT_QtyHasil
        '
        Me.TXT_QtyHasil.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TXT_QtyHasil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TXT_QtyHasil.Enabled = False
        Me.TXT_QtyHasil.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.TXT_QtyHasil.Location = New System.Drawing.Point(113, 118)
        Me.TXT_QtyHasil.MaxLength = 50
        Me.TXT_QtyHasil.Name = "TXT_QtyHasil"
        Me.TXT_QtyHasil.Size = New System.Drawing.Size(108, 20)
        Me.TXT_QtyHasil.TabIndex = 381
        Me.TXT_QtyHasil.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(21, 119)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 17)
        Me.Label2.TabIndex = 384
        Me.Label2.Text = "Hasil"
        '
        'TXT_KodeBarang
        '
        Me.TXT_KodeBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TXT_KodeBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TXT_KodeBarang.Enabled = False
        Me.TXT_KodeBarang.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.TXT_KodeBarang.Location = New System.Drawing.Point(113, 92)
        Me.TXT_KodeBarang.MaxLength = 50
        Me.TXT_KodeBarang.Name = "TXT_KodeBarang"
        Me.TXT_KodeBarang.Size = New System.Drawing.Size(108, 20)
        Me.TXT_KodeBarang.TabIndex = 382
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(21, 93)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 17)
        Me.Label1.TabIndex = 385
        Me.Label1.Text = "Barang"
        '
        'TXT_NoFormula
        '
        Me.TXT_NoFormula.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TXT_NoFormula.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TXT_NoFormula.Enabled = False
        Me.TXT_NoFormula.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.TXT_NoFormula.Location = New System.Drawing.Point(113, 66)
        Me.TXT_NoFormula.MaxLength = 50
        Me.TXT_NoFormula.Name = "TXT_NoFormula"
        Me.TXT_NoFormula.Size = New System.Drawing.Size(490, 20)
        Me.TXT_NoFormula.TabIndex = 383
        Me.TXT_NoFormula.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblFormulator_KodeBarang
        '
        Me.LblFormulator_KodeBarang.AutoSize = True
        Me.LblFormulator_KodeBarang.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.LblFormulator_KodeBarang.Location = New System.Drawing.Point(21, 67)
        Me.LblFormulator_KodeBarang.Name = "LblFormulator_KodeBarang"
        Me.LblFormulator_KodeBarang.Size = New System.Drawing.Size(71, 17)
        Me.LblFormulator_KodeBarang.TabIndex = 386
        Me.LblFormulator_KodeBarang.Text = "No Fornula"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 63)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 401)
        Me.Panel3.TabIndex = 389
        Me.Panel3.Visible = False
        '
        'TXT_Keterangan
        '
        Me.TXT_Keterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TXT_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TXT_Keterangan.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.TXT_Keterangan.Location = New System.Drawing.Point(113, 144)
        Me.TXT_Keterangan.MaxLength = 250
        Me.TXT_Keterangan.Multiline = True
        Me.TXT_Keterangan.Name = "TXT_Keterangan"
        Me.TXT_Keterangan.Size = New System.Drawing.Size(490, 83)
        Me.TXT_Keterangan.TabIndex = 378
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(610, 58)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 401)
        Me.Panel4.TabIndex = 390
        Me.Panel4.Visible = False
        '
        'BTN_Simpan
        '
        Me.BTN_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BTN_Simpan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BTN_Simpan.ForeColor = System.Drawing.Color.White
        Me.BTN_Simpan.Location = New System.Drawing.Point(113, 233)
        Me.BTN_Simpan.Name = "BTN_Simpan"
        Me.BTN_Simpan.Size = New System.Drawing.Size(170, 36)
        Me.BTN_Simpan.TabIndex = 403
        Me.BTN_Simpan.Text = "&Simpan"
        Me.BTN_Simpan.UseVisualStyleBackColor = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(16, 269)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(609, 15)
        Me.Panel6.TabIndex = 405
        Me.Panel6.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(21, 144)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 17)
        Me.Label3.TabIndex = 406
        Me.Label3.Text = "Keterangan"
        '
        'N_EMI_SD_Konfirmasi_Bypass_Formula
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(629, 284)
        Me.Controls.Add(Me.TXT_Keterangan)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.BTN_Simpan)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.CMB_SatuanHasil)
        Me.Controls.Add(Me.TXT_NamaBarang)
        Me.Controls.Add(Me.TXT_QtyHasil)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TXT_KodeBarang)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TXT_NoFormula)
        Me.Controls.Add(Me.LblFormulator_KodeBarang)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.Name = "N_EMI_SD_Konfirmasi_Bypass_Formula"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Panel5 As Panel
    Friend WithEvents LblFormulator_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents CMB_SatuanHasil As ComboBox
    Friend WithEvents TXT_NamaBarang As TextBox
    Friend WithEvents TXT_QtyHasil As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TXT_KodeBarang As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TXT_NoFormula As TextBox
    Friend WithEvents LblFormulator_KodeBarang As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents TXT_Keterangan As TextBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents BTN_Simpan As Button
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label3 As Label
End Class
