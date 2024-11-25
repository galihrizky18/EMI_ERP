<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Display_Transaksi_MaterialRequsition
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
        Me.LblInquiry_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblNoFaktur = New System.Windows.Forms.Label()
        Me.txtvalue = New System.Windows.Forms.TextBox()
        Me.BtnInquiry_Cari = New System.Windows.Forms.Button()
        Me.BtnInquiry_Refresh = New System.Windows.Forms.Button()
        Me.LblInquiry_Lokasi = New System.Windows.Forms.Label()
        Me.cmbCari = New System.Windows.Forms.ComboBox()
        Me.Lv_Barang = New System.Windows.Forms.ListView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.LblInquiry_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(862, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(862, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'LblInquiry_Judul
        '
        Me.LblInquiry_Judul.AutoSize = True
        Me.LblInquiry_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInquiry_Judul.Location = New System.Drawing.Point(15, 11)
        Me.LblInquiry_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LblInquiry_Judul.Name = "LblInquiry_Judul"
        Me.LblInquiry_Judul.Size = New System.Drawing.Size(318, 30)
        Me.LblInquiry_Judul.TabIndex = 0
        Me.LblInquiry_Judul.Text = "Display - Material Requisition"
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
        Me.Panel3.Size = New System.Drawing.Size(19, 567)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(840, 63)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 556)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(18, 615)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1436, 15)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'lblNoFaktur
        '
        Me.lblNoFaktur.AutoSize = True
        Me.lblNoFaktur.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.lblNoFaktur.Location = New System.Drawing.Point(25, 94)
        Me.lblNoFaktur.Name = "lblNoFaktur"
        Me.lblNoFaktur.Size = New System.Drawing.Size(46, 20)
        Me.lblNoFaktur.TabIndex = 227
        Me.lblNoFaktur.Text = "Value"
        '
        'txtvalue
        '
        Me.txtvalue.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtvalue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtvalue.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.txtvalue.Location = New System.Drawing.Point(83, 93)
        Me.txtvalue.MaxLength = 50
        Me.txtvalue.Name = "txtvalue"
        Me.txtvalue.Size = New System.Drawing.Size(228, 22)
        Me.txtvalue.TabIndex = 228
        '
        'BtnInquiry_Cari
        '
        Me.BtnInquiry_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnInquiry_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnInquiry_Cari.ForeColor = System.Drawing.Color.White
        Me.BtnInquiry_Cari.Location = New System.Drawing.Point(317, 89)
        Me.BtnInquiry_Cari.Name = "BtnInquiry_Cari"
        Me.BtnInquiry_Cari.Size = New System.Drawing.Size(80, 28)
        Me.BtnInquiry_Cari.TabIndex = 338
        Me.BtnInquiry_Cari.Text = "Cari"
        Me.BtnInquiry_Cari.UseVisualStyleBackColor = False
        '
        'BtnInquiry_Refresh
        '
        Me.BtnInquiry_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnInquiry_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnInquiry_Refresh.ForeColor = System.Drawing.Color.White
        Me.BtnInquiry_Refresh.Location = New System.Drawing.Point(317, 61)
        Me.BtnInquiry_Refresh.Name = "BtnInquiry_Refresh"
        Me.BtnInquiry_Refresh.Size = New System.Drawing.Size(80, 28)
        Me.BtnInquiry_Refresh.TabIndex = 389
        Me.BtnInquiry_Refresh.Text = "Refresh"
        Me.BtnInquiry_Refresh.UseVisualStyleBackColor = False
        '
        'LblInquiry_Lokasi
        '
        Me.LblInquiry_Lokasi.AutoSize = True
        Me.LblInquiry_Lokasi.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblInquiry_Lokasi.Location = New System.Drawing.Point(27, 65)
        Me.LblInquiry_Lokasi.Name = "LblInquiry_Lokasi"
        Me.LblInquiry_Lokasi.Size = New System.Drawing.Size(50, 20)
        Me.LblInquiry_Lokasi.TabIndex = 383
        Me.LblInquiry_Lokasi.Text = "Kolom"
        '
        'cmbCari
        '
        Me.cmbCari.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCari.FormattingEnabled = True
        Me.cmbCari.Location = New System.Drawing.Point(83, 65)
        Me.cmbCari.Name = "cmbCari"
        Me.cmbCari.Size = New System.Drawing.Size(228, 24)
        Me.cmbCari.TabIndex = 388
        '
        'Lv_Barang
        '
        Me.Lv_Barang.FullRowSelect = True
        Me.Lv_Barang.GridLines = True
        Me.Lv_Barang.HideSelection = False
        Me.Lv_Barang.Location = New System.Drawing.Point(26, 147)
        Me.Lv_Barang.Name = "Lv_Barang"
        Me.Lv_Barang.Size = New System.Drawing.Size(810, 212)
        Me.Lv_Barang.TabIndex = 390
        Me.Lv_Barang.UseCompatibleStateImageBehavior = False
        Me.Lv_Barang.View = System.Windows.Forms.View.Details
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(403, 61)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 28)
        Me.Button1.TabIndex = 391
        Me.Button1.Text = "New"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBox1.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.CheckBox1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CheckBox1.Location = New System.Drawing.Point(27, 120)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(93, 24)
        Me.CheckBox1.TabIndex = 392
        Me.CheckBox1.Text = "Referensi"
        Me.CheckBox1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(26, 386)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(810, 222)
        Me.ListView1.TabIndex = 393
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(6, 360)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(853, 24)
        Me.Panel7.TabIndex = 394
        Me.Panel7.Visible = False
        '
        'Display_Transaksi_MaterialRequsition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(862, 632)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Lv_Barang)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.BtnInquiry_Refresh)
        Me.Controls.Add(Me.cmbCari)
        Me.Controls.Add(Me.LblInquiry_Lokasi)
        Me.Controls.Add(Me.BtnInquiry_Cari)
        Me.Controls.Add(Me.txtvalue)
        Me.Controls.Add(Me.lblNoFaktur)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Display_Transaksi_MaterialRequsition"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents LblInquiry_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents lblNoFaktur As Label
    Friend WithEvents txtvalue As TextBox
    Friend WithEvents BtnInquiry_Cari As Button
    Friend WithEvents BtnInquiry_Refresh As Button
    Friend WithEvents LblInquiry_Lokasi As Label
    Friend WithEvents cmbCari As ComboBox
    Friend WithEvents Lv_Barang As ListView
    Friend WithEvents Button1 As Button
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents ListView1 As ListView
    Friend WithEvents Panel7 As Panel
End Class
