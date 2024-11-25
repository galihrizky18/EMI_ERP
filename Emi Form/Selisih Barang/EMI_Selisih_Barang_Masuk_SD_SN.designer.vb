<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Selisih_Barang_Masuk_SD_SN
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EMI_Selisih_Barang_Masuk_SD_SN))
        Me.ComboBox4 = New System.Windows.Forms.ComboBox()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ListView3 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Simpan = New System.Windows.Forms.Button()
        Me.TanggalProduksi = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxkdBrg = New System.Windows.Forms.TextBox()
        Me.TanggalExpired = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxNmBrg = New System.Windows.Forms.TextBox()
        Me.TextBoxQty = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxNoKonte = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TextBoxLokasi = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.txtSatuan = New System.Windows.Forms.TextBox()
        Me.ComboBoxTgl = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ComboBox4
        '
        Me.ComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Location = New System.Drawing.Point(976, 194)
        Me.ComboBox4.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(68, 19)
        Me.ComboBox4.TabIndex = 10
        Me.ComboBox4.Visible = False
        '
        'ComboBox3
        '
        Me.ComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Location = New System.Drawing.Point(980, 157)
        Me.ComboBox3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(129, 19)
        Me.ComboBox3.TabIndex = 9
        Me.ComboBox3.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(978, 179)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 12)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Tipe Pengurutan"
        Me.Label4.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(978, 144)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 12)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Kolom Pengurutan"
        Me.Label5.Visible = False
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(1046, 411)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(62, 19)
        Me.Button2.TabIndex = 11
        Me.Button2.Text = "E&xit"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'ListView3
        '
        Me.ListView3.BackColor = System.Drawing.Color.Snow
        Me.ListView3.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader4})
        Me.ListView3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ListView3.FullRowSelect = True
        Me.ListView3.GridLines = True
        Me.ListView3.HideSelection = False
        Me.ListView3.Location = New System.Drawing.Point(980, 12)
        Me.ListView3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ListView3.Name = "ListView3"
        Me.ListView3.Size = New System.Drawing.Size(224, 129)
        Me.ListView3.TabIndex = 65
        Me.ListView3.UseCompatibleStateImageBehavior = False
        Me.ListView3.View = System.Windows.Forms.View.Details
        Me.ListView3.Visible = False
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "#"
        Me.ColumnHeader1.Width = 0
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Kode Barang"
        Me.ColumnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader2.Width = 196
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Jumlah"
        Me.ColumnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader4.Width = 49
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.Label1.Location = New System.Drawing.Point(19, 172)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 18)
        Me.Label1.TabIndex = 67
        Me.Label1.Text = "Kode Barang"
        '
        'Simpan
        '
        Me.Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Simpan.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Simpan.ForeColor = System.Drawing.Color.White
        Me.Simpan.Location = New System.Drawing.Point(19, 298)
        Me.Simpan.Name = "Simpan"
        Me.Simpan.Size = New System.Drawing.Size(93, 31)
        Me.Simpan.TabIndex = 2
        Me.Simpan.Text = "Simpan"
        Me.Simpan.UseVisualStyleBackColor = False
        '
        'TanggalProduksi
        '
        Me.TanggalProduksi.CustomFormat = "dd MMM yyyy"
        Me.TanggalProduksi.Enabled = False
        Me.TanggalProduksi.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.TanggalProduksi.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TanggalProduksi.Location = New System.Drawing.Point(149, 242)
        Me.TanggalProduksi.Name = "TanggalProduksi"
        Me.TanggalProduksi.Size = New System.Drawing.Size(317, 23)
        Me.TanggalProduksi.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.Label2.Location = New System.Drawing.Point(19, 123)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 18)
        Me.Label2.TabIndex = 70
        Me.Label2.Text = "Jumlah Selisih"
        '
        'TextBoxkdBrg
        '
        Me.TextBoxkdBrg.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBoxkdBrg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxkdBrg.Enabled = False
        Me.TextBoxkdBrg.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.TextBoxkdBrg.Location = New System.Drawing.Point(149, 173)
        Me.TextBoxkdBrg.Name = "TextBoxkdBrg"
        Me.TextBoxkdBrg.Size = New System.Drawing.Size(125, 20)
        Me.TextBoxkdBrg.TabIndex = 1
        '
        'TanggalExpired
        '
        Me.TanggalExpired.CustomFormat = "dd MMM yyyy"
        Me.TanggalExpired.Enabled = False
        Me.TanggalExpired.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.TanggalExpired.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TanggalExpired.Location = New System.Drawing.Point(149, 269)
        Me.TanggalExpired.Name = "TanggalExpired"
        Me.TanggalExpired.Size = New System.Drawing.Size(317, 23)
        Me.TanggalExpired.TabIndex = 71
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.Label3.Location = New System.Drawing.Point(19, 246)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 18)
        Me.Label3.TabIndex = 72
        Me.Label3.Text = "Tgl Produksi"
        '
        'TextBoxNmBrg
        '
        Me.TextBoxNmBrg.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBoxNmBrg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxNmBrg.Enabled = False
        Me.TextBoxNmBrg.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.TextBoxNmBrg.Location = New System.Drawing.Point(277, 173)
        Me.TextBoxNmBrg.Name = "TextBoxNmBrg"
        Me.TextBoxNmBrg.Size = New System.Drawing.Size(189, 20)
        Me.TextBoxNmBrg.TabIndex = 73
        '
        'TextBoxQty
        '
        Me.TextBoxQty.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBoxQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxQty.Enabled = False
        Me.TextBoxQty.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.TextBoxQty.Location = New System.Drawing.Point(149, 121)
        Me.TextBoxQty.Name = "TextBoxQty"
        Me.TextBoxQty.Size = New System.Drawing.Size(111, 20)
        Me.TextBoxQty.TabIndex = 74
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.Label6.Location = New System.Drawing.Point(19, 273)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 18)
        Me.Label6.TabIndex = 75
        Me.Label6.Text = "Tgl Expired"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.Label7.Location = New System.Drawing.Point(19, 199)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(31, 18)
        Me.Label7.TabIndex = 77
        Me.Label7.Text = "Rak"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.Label9.Location = New System.Drawing.Point(20, 63)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 18)
        Me.Label9.TabIndex = 80
        Me.Label9.Text = "No PO"
        '
        'TextBoxNoKonte
        '
        Me.TextBoxNoKonte.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBoxNoKonte.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxNoKonte.Enabled = False
        Me.TextBoxNoKonte.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.TextBoxNoKonte.Location = New System.Drawing.Point(91, 64)
        Me.TextBoxNoKonte.Name = "TextBoxNoKonte"
        Me.TextBoxNoKonte.Size = New System.Drawing.Size(210, 20)
        Me.TextBoxNoKonte.TabIndex = 81
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.Label10.Location = New System.Drawing.Point(19, 146)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(48, 18)
        Me.Label10.TabIndex = 82
        Me.Label10.Text = "Lokasi"
        '
        'TextBoxLokasi
        '
        Me.TextBoxLokasi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBoxLokasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxLokasi.Enabled = False
        Me.TextBoxLokasi.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.TextBoxLokasi.Location = New System.Drawing.Point(149, 147)
        Me.TextBoxLokasi.Name = "TextBoxLokasi"
        Me.TextBoxLokasi.Size = New System.Drawing.Size(317, 20)
        Me.TextBoxLokasi.TabIndex = 83
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Work Sans", 9.75!)
        Me.Label8.Location = New System.Drawing.Point(358, 123)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(12, 18)
        Me.Label8.TabIndex = 79
        Me.Label8.Text = " "
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(0, 331)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(513, 15)
        Me.Panel4.TabIndex = 88
        Me.Panel4.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(2, 50)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 85
        Me.Panel2.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(488, 51)
        Me.Panel1.TabIndex = 84
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
        Me.PanelGradient1.Size = New System.Drawing.Size(488, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(14, 9)
        Me.Label11.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(153, 30)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Set Waktu Uji"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 59)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 315)
        Me.Panel3.TabIndex = 89
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(467, 63)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 315)
        Me.Panel5.TabIndex = 90
        Me.Panel5.Visible = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(22, 227)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(65, 16)
        Me.CheckBox1.TabIndex = 91
        Me.CheckBox1.Text = "Rak Baru"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'txtSatuan
        '
        Me.txtSatuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtSatuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSatuan.Enabled = False
        Me.txtSatuan.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.txtSatuan.Location = New System.Drawing.Point(263, 121)
        Me.txtSatuan.Name = "txtSatuan"
        Me.txtSatuan.Size = New System.Drawing.Size(89, 20)
        Me.txtSatuan.TabIndex = 92
        '
        'ComboBoxTgl
        '
        Me.ComboBoxTgl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxTgl.Enabled = False
        Me.ComboBoxTgl.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxTgl.FormattingEnabled = True
        Me.ComboBoxTgl.Location = New System.Drawing.Point(149, 197)
        Me.ComboBoxTgl.Name = "ComboBoxTgl"
        Me.ComboBoxTgl.Size = New System.Drawing.Size(317, 25)
        Me.ComboBoxTgl.TabIndex = 93
        '
        'EMI_Selisih_Barang_Masuk_SD_SN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(5.0!, 11.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(488, 347)
        Me.Controls.Add(Me.ComboBoxTgl)
        Me.Controls.Add(Me.txtSatuan)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TextBoxLokasi)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.TextBoxNoKonte)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxQty)
        Me.Controls.Add(Me.TextBoxNmBrg)
        Me.Controls.Add(Me.TanggalExpired)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxkdBrg)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TanggalProduksi)
        Me.Controls.Add(Me.Simpan)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.ListView3)
        Me.Controls.Add(Me.ComboBox4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ComboBox3)
        Me.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Name = "EMI_Selisih_Barang_Masuk_SD_SN"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents ComboBox4 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox3 As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ListView3 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Simpan As System.Windows.Forms.Button
    Friend WithEvents TanggalProduksi As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxkdBrg As System.Windows.Forms.TextBox
    Friend WithEvents TanggalExpired As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNmBrg As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxQty As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNoKonte As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBoxLokasi As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label11 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents txtSatuan As TextBox
    Friend WithEvents ComboBoxTgl As ComboBox
End Class
