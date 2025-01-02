<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SD_Convert_Request_Material
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbWarna = New System.Windows.Forms.ComboBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.TxtNama = New System.Windows.Forms.TextBox()
        Me.txtSatuan = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtJumlah = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtJmlhRequest = New System.Windows.Forms.TextBox()
        Me.txtLokasi = New System.Windows.Forms.TextBox()
        Me.txtKodeBarang = New System.Windows.Forms.TextBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtUrut = New System.Windows.Forms.TextBox()
        Me.txtNoFaktur = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(8, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(121, 20)
        Me.Label3.TabIndex = 359
        Me.Label3.Text = "Jumlah Request"
        '
        'cmbWarna
        '
        Me.cmbWarna.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbWarna.FormattingEnabled = True
        Me.cmbWarna.Location = New System.Drawing.Point(135, 69)
        Me.cmbWarna.Name = "cmbWarna"
        Me.cmbWarna.Size = New System.Drawing.Size(165, 24)
        Me.cmbWarna.TabIndex = 360
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 34
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 63)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 470)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(767, 63)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 456)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(19, 482)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1436, 15)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(276, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Convert Request Material"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(785, 51)
        Me.Panel1.TabIndex = 22
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
        Me.PanelGradient1.Size = New System.Drawing.Size(785, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'ListView1
        '
        Me.ListView1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(3, 16)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(739, 166)
        Me.ListView1.TabIndex = 361
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Button3)
        Me.Panel6.Controls.Add(Me.TxtNama)
        Me.Panel6.Controls.Add(Me.txtSatuan)
        Me.Panel6.Controls.Add(Me.Label5)
        Me.Panel6.Controls.Add(Me.txtJumlah)
        Me.Panel6.Controls.Add(Me.Label4)
        Me.Panel6.Controls.Add(Me.Label2)
        Me.Panel6.Controls.Add(Me.TxtJmlhRequest)
        Me.Panel6.Controls.Add(Me.cmbWarna)
        Me.Panel6.Controls.Add(Me.Label3)
        Me.Panel6.Location = New System.Drawing.Point(19, 63)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(745, 168)
        Me.Panel6.TabIndex = 364
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Button3.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Location = New System.Drawing.Point(135, 127)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(165, 33)
        Me.Button3.TabIndex = 378
        Me.Button3.Text = "Convert"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'TxtNama
        '
        Me.TxtNama.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtNama.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtNama.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtNama.Location = New System.Drawing.Point(135, 13)
        Me.TxtNama.MaxLength = 50
        Me.TxtNama.Name = "TxtNama"
        Me.TxtNama.ReadOnly = True
        Me.TxtNama.Size = New System.Drawing.Size(244, 22)
        Me.TxtNama.TabIndex = 366
        '
        'txtSatuan
        '
        Me.txtSatuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtSatuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSatuan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.txtSatuan.Location = New System.Drawing.Point(259, 41)
        Me.txtSatuan.MaxLength = 50
        Me.txtSatuan.Name = "txtSatuan"
        Me.txtSatuan.ReadOnly = True
        Me.txtSatuan.Size = New System.Drawing.Size(118, 22)
        Me.txtSatuan.TabIndex = 367
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(8, 14)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 20)
        Me.Label5.TabIndex = 365
        Me.Label5.Text = "Nama"
        '
        'txtJumlah
        '
        Me.txtJumlah.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJumlah.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.txtJumlah.Location = New System.Drawing.Point(135, 99)
        Me.txtJumlah.MaxLength = 50
        Me.txtJumlah.Name = "txtJumlah"
        Me.txtJumlah.Size = New System.Drawing.Size(165, 22)
        Me.txtJumlah.TabIndex = 364
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(8, 100)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 20)
        Me.Label4.TabIndex = 363
        Me.Label4.Text = "Jumlah"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(8, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 20)
        Me.Label2.TabIndex = 362
        Me.Label2.Text = "Warna"
        '
        'TxtJmlhRequest
        '
        Me.TxtJmlhRequest.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtJmlhRequest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtJmlhRequest.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtJmlhRequest.Location = New System.Drawing.Point(135, 41)
        Me.TxtJmlhRequest.MaxLength = 50
        Me.TxtJmlhRequest.Name = "TxtJmlhRequest"
        Me.TxtJmlhRequest.ReadOnly = True
        Me.TxtJmlhRequest.Size = New System.Drawing.Size(118, 22)
        Me.TxtJmlhRequest.TabIndex = 361
        '
        'txtLokasi
        '
        Me.txtLokasi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtLokasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLokasi.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.txtLokasi.Location = New System.Drawing.Point(868, 103)
        Me.txtLokasi.MaxLength = 50
        Me.txtLokasi.Name = "txtLokasi"
        Me.txtLokasi.ReadOnly = True
        Me.txtLokasi.Size = New System.Drawing.Size(54, 22)
        Me.txtLokasi.TabIndex = 369
        Me.txtLokasi.Visible = False
        '
        'txtKodeBarang
        '
        Me.txtKodeBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtKodeBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKodeBarang.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.txtKodeBarang.Location = New System.Drawing.Point(804, 103)
        Me.txtKodeBarang.MaxLength = 50
        Me.txtKodeBarang.Name = "txtKodeBarang"
        Me.txtKodeBarang.ReadOnly = True
        Me.txtKodeBarang.Size = New System.Drawing.Size(58, 22)
        Me.txtKodeBarang.TabIndex = 368
        Me.txtKodeBarang.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(21, 233)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(942, 12)
        Me.Panel7.TabIndex = 35
        Me.Panel7.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ListView1)
        Me.GroupBox1.Location = New System.Drawing.Point(21, 247)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(744, 188)
        Me.GroupBox1.TabIndex = 365
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detail Requestion"
        '
        'txtUrut
        '
        Me.txtUrut.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtUrut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUrut.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.txtUrut.Location = New System.Drawing.Point(808, 131)
        Me.txtUrut.MaxLength = 50
        Me.txtUrut.Name = "txtUrut"
        Me.txtUrut.ReadOnly = True
        Me.txtUrut.Size = New System.Drawing.Size(54, 22)
        Me.txtUrut.TabIndex = 370
        Me.txtUrut.Visible = False
        '
        'txtNoFaktur
        '
        Me.txtNoFaktur.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtNoFaktur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoFaktur.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.txtNoFaktur.Location = New System.Drawing.Point(868, 131)
        Me.txtNoFaktur.MaxLength = 50
        Me.txtNoFaktur.Name = "txtNoFaktur"
        Me.txtNoFaktur.ReadOnly = True
        Me.txtNoFaktur.Size = New System.Drawing.Size(54, 22)
        Me.txtNoFaktur.TabIndex = 371
        Me.txtNoFaktur.Visible = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Button2.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(123, 442)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(93, 33)
        Me.Button2.TabIndex = 377
        Me.Button2.Text = "Refresh"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(24, 442)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(93, 33)
        Me.Button1.TabIndex = 376
        Me.Button1.Text = "Simpan"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'SD_Convert_Request_Material
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(785, 496)
        Me.Controls.Add(Me.txtNoFaktur)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.txtUrut)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.txtLokasi)
        Me.Controls.Add(Me.txtKodeBarang)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "SD_Convert_Request_Material"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbWarna As ComboBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ListView1 As ListView
    Friend WithEvents Panel6 As Panel
    Friend WithEvents TxtJmlhRequest As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtNama As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtJumlah As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents txtSatuan As TextBox
    Friend WithEvents txtKodeBarang As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtLokasi As TextBox
    Friend WithEvents txtUrut As TextBox
    Friend WithEvents txtNoFaktur As TextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
End Class
