<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Pengeluaran_Barang
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
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.TxtNoFaktur = New System.Windows.Forms.TextBox()
        Me.CmbSO = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TxtKodeCost = New System.Windows.Forms.TextBox()
        Me.TxtIDCost = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtKetPB = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TxtSatuan = New System.Windows.Forms.TextBox()
        Me.TxtKet = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtTotal = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.LvBarang = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader13 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.BtnClear = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TxtJlh = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TxtNamaBarang = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtKodeBarang = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CmbSOBrg = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.BtnSimpan = New System.Windows.Forms.Button()
        Me.BtnRefresh = New System.Windows.Forms.Button()
        Me.LvCost = New System.Windows.Forms.ListView()
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.LvBrg = New System.Windows.Forms.ListView()
        Me.ColumnHeader12 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader11 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader14 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Lbl_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1142, 49)
        Me.Panel1.TabIndex = 233
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 46)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(1142, 3)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(7, 7)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(342, 30)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Transaksi - Pengeluaran Barang"
        '
        'TxtNoFaktur
        '
        Me.TxtNoFaktur.Location = New System.Drawing.Point(12, 58)
        Me.TxtNoFaktur.Name = "TxtNoFaktur"
        Me.TxtNoFaktur.ReadOnly = True
        Me.TxtNoFaktur.Size = New System.Drawing.Size(169, 23)
        Me.TxtNoFaktur.TabIndex = 234
        Me.TxtNoFaktur.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'CmbSO
        '
        Me.CmbSO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSO.Enabled = False
        Me.CmbSO.FormattingEnabled = True
        Me.CmbSO.Location = New System.Drawing.Point(185, 57)
        Me.CmbSO.Name = "CmbSO"
        Me.CmbSO.Size = New System.Drawing.Size(272, 26)
        Me.CmbSO.TabIndex = 235
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TxtKodeCost)
        Me.GroupBox1.Controls.Add(Me.TxtIDCost)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.TxtKetPB)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Tgl1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 88)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1118, 110)
        Me.GroupBox1.TabIndex = 236
        Me.GroupBox1.TabStop = False
        '
        'TxtKodeCost
        '
        Me.TxtKodeCost.Location = New System.Drawing.Point(107, 73)
        Me.TxtKodeCost.Name = "TxtKodeCost"
        Me.TxtKodeCost.Size = New System.Drawing.Size(416, 23)
        Me.TxtKodeCost.TabIndex = 4
        '
        'TxtIDCost
        '
        Me.TxtIDCost.Location = New System.Drawing.Point(529, 73)
        Me.TxtIDCost.Name = "TxtIDCost"
        Me.TxtIDCost.ReadOnly = True
        Me.TxtIDCost.Size = New System.Drawing.Size(62, 23)
        Me.TxtIDCost.TabIndex = 3
        Me.TxtIDCost.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 18)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Cost Center"
        '
        'TxtKetPB
        '
        Me.TxtKetPB.Location = New System.Drawing.Point(107, 46)
        Me.TxtKetPB.MaxLength = 50
        Me.TxtKetPB.Name = "TxtKetPB"
        Me.TxtKetPB.Size = New System.Drawing.Size(416, 23)
        Me.TxtKetPB.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 18)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Keterangan"
        '
        'Tgl1
        '
        Me.Tgl1.CustomFormat = "dd MMMM yyyy"
        Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl1.Location = New System.Drawing.Point(107, 19)
        Me.Tgl1.Name = "Tgl1"
        Me.Tgl1.Size = New System.Drawing.Size(193, 23)
        Me.Tgl1.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Tanggal"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TxtSatuan)
        Me.GroupBox2.Controls.Add(Me.TxtKet)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.TxtTotal)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.LvBarang)
        Me.GroupBox2.Controls.Add(Me.BtnOK)
        Me.GroupBox2.Controls.Add(Me.BtnClear)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.TxtJlh)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.TxtNamaBarang)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.TxtKodeBarang)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 200)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1118, 383)
        Me.GroupBox2.TabIndex = 237
        Me.GroupBox2.TabStop = False
        '
        'TxtSatuan
        '
        Me.TxtSatuan.Location = New System.Drawing.Point(655, 37)
        Me.TxtSatuan.MaxLength = 8
        Me.TxtSatuan.Name = "TxtSatuan"
        Me.TxtSatuan.ReadOnly = True
        Me.TxtSatuan.Size = New System.Drawing.Size(94, 23)
        Me.TxtSatuan.TabIndex = 435
        Me.TxtSatuan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TxtKet
        '
        Me.TxtKet.Location = New System.Drawing.Point(750, 37)
        Me.TxtKet.MaxLength = 50
        Me.TxtKet.Name = "TxtKet"
        Me.TxtKet.Size = New System.Drawing.Size(290, 23)
        Me.TxtKet.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Location = New System.Drawing.Point(750, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(290, 22)
        Me.Label2.TabIndex = 434
        Me.Label2.Text = "Keterangan"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtTotal
        '
        Me.TxtTotal.Location = New System.Drawing.Point(673, 356)
        Me.TxtTotal.Name = "TxtTotal"
        Me.TxtTotal.ReadOnly = True
        Me.TxtTotal.Size = New System.Drawing.Size(66, 23)
        Me.TxtTotal.TabIndex = 433
        Me.TxtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(627, 359)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(39, 18)
        Me.Label18.TabIndex = 432
        Me.Label18.Text = "Total"
        '
        'LvBarang
        '
        Me.LvBarang.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader7, Me.ColumnHeader13})
        Me.LvBarang.FullRowSelect = True
        Me.LvBarang.GridLines = True
        Me.LvBarang.HideSelection = False
        Me.LvBarang.Location = New System.Drawing.Point(5, 63)
        Me.LvBarang.Name = "LvBarang"
        Me.LvBarang.Size = New System.Drawing.Size(1107, 293)
        Me.LvBarang.TabIndex = 421
        Me.LvBarang.UseCompatibleStateImageBehavior = False
        Me.LvBarang.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Lokasi"
        Me.ColumnHeader1.Width = 160
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Kode Barang"
        Me.ColumnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader2.Width = 146
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Nama Barang"
        Me.ColumnHeader3.Width = 359
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Jumlah"
        Me.ColumnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader4.Width = 66
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Satuan"
        Me.ColumnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader5.Width = 95
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Keterangan"
        Me.ColumnHeader7.Width = 254
        '
        'ColumnHeader13
        '
        Me.ColumnHeader13.Text = "Id Group Jenis"
        Me.ColumnHeader13.Width = 0
        '
        'BtnOK
        '
        Me.BtnOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnOK.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnOK.ForeColor = System.Drawing.Color.White
        Me.BtnOK.Location = New System.Drawing.Point(1041, 35)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(73, 26)
        Me.BtnOK.TabIndex = 11
        Me.BtnOK.Text = "&OK"
        Me.BtnOK.UseVisualStyleBackColor = False
        '
        'BtnClear
        '
        Me.BtnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnClear.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnClear.ForeColor = System.Drawing.Color.White
        Me.BtnClear.Location = New System.Drawing.Point(1041, 12)
        Me.BtnClear.Name = "BtnClear"
        Me.BtnClear.Size = New System.Drawing.Size(73, 26)
        Me.BtnClear.TabIndex = 10
        Me.BtnClear.Text = "&Clear"
        Me.BtnClear.UseVisualStyleBackColor = False
        '
        'Label13
        '
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Location = New System.Drawing.Point(655, 13)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(94, 22)
        Me.Label13.TabIndex = 19
        Me.Label13.Text = "Satuan"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtJlh
        '
        Me.TxtJlh.Location = New System.Drawing.Point(589, 37)
        Me.TxtJlh.MaxLength = 8
        Me.TxtJlh.Name = "TxtJlh"
        Me.TxtJlh.Size = New System.Drawing.Size(65, 23)
        Me.TxtJlh.TabIndex = 7
        Me.TxtJlh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Location = New System.Drawing.Point(589, 13)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(65, 22)
        Me.Label12.TabIndex = 17
        Me.Label12.Text = "Jumlah"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtNamaBarang
        '
        Me.TxtNamaBarang.Location = New System.Drawing.Point(218, 37)
        Me.TxtNamaBarang.Name = "TxtNamaBarang"
        Me.TxtNamaBarang.Size = New System.Drawing.Size(370, 23)
        Me.TxtNamaBarang.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Location = New System.Drawing.Point(218, 13)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(370, 22)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Nama Barang"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtKodeBarang
        '
        Me.TxtKodeBarang.Location = New System.Drawing.Point(5, 37)
        Me.TxtKodeBarang.Name = "TxtKodeBarang"
        Me.TxtKodeBarang.Size = New System.Drawing.Size(212, 23)
        Me.TxtKodeBarang.TabIndex = 5
        Me.TxtKodeBarang.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Location = New System.Drawing.Point(5, 13)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(212, 22)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Kode Barang"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CmbSOBrg
        '
        Me.CmbSOBrg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSOBrg.FormattingEnabled = True
        Me.CmbSOBrg.Location = New System.Drawing.Point(541, 57)
        Me.CmbSOBrg.Name = "CmbSOBrg"
        Me.CmbSOBrg.Size = New System.Drawing.Size(248, 26)
        Me.CmbSOBrg.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(484, 58)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 22)
        Me.Label5.TabIndex = 436
        Me.Label5.Text = "Lokasi"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BtnSimpan
        '
        Me.BtnSimpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnSimpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnSimpan.ForeColor = System.Drawing.Color.White
        Me.BtnSimpan.Location = New System.Drawing.Point(11, 584)
        Me.BtnSimpan.Name = "BtnSimpan"
        Me.BtnSimpan.Size = New System.Drawing.Size(85, 31)
        Me.BtnSimpan.TabIndex = 12
        Me.BtnSimpan.Text = "&Simpan"
        Me.BtnSimpan.UseVisualStyleBackColor = False
        '
        'BtnRefresh
        '
        Me.BtnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnRefresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnRefresh.ForeColor = System.Drawing.Color.White
        Me.BtnRefresh.Location = New System.Drawing.Point(95, 584)
        Me.BtnRefresh.Name = "BtnRefresh"
        Me.BtnRefresh.Size = New System.Drawing.Size(85, 31)
        Me.BtnRefresh.TabIndex = 13
        Me.BtnRefresh.Text = "Re&fresh"
        Me.BtnRefresh.UseVisualStyleBackColor = False
        '
        'LvCost
        '
        Me.LvCost.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader6, Me.ColumnHeader8})
        Me.LvCost.FullRowSelect = True
        Me.LvCost.GridLines = True
        Me.LvCost.HideSelection = False
        Me.LvCost.Location = New System.Drawing.Point(1265, 109)
        Me.LvCost.Name = "LvCost"
        Me.LvCost.Size = New System.Drawing.Size(445, 161)
        Me.LvCost.TabIndex = 238
        Me.LvCost.UseCompatibleStateImageBehavior = False
        Me.LvCost.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "ID"
        Me.ColumnHeader6.Width = 0
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Kode Cost Center"
        Me.ColumnHeader8.Width = 413
        '
        'LvBrg
        '
        Me.LvBrg.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader12, Me.ColumnHeader9, Me.ColumnHeader10, Me.ColumnHeader11, Me.ColumnHeader14})
        Me.LvBrg.FullRowSelect = True
        Me.LvBrg.GridLines = True
        Me.LvBrg.HideSelection = False
        Me.LvBrg.Location = New System.Drawing.Point(1265, 328)
        Me.LvBrg.Name = "LvBrg"
        Me.LvBrg.Size = New System.Drawing.Size(769, 228)
        Me.LvBrg.TabIndex = 239
        Me.LvBrg.UseCompatibleStateImageBehavior = False
        Me.LvBrg.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader12
        '
        Me.ColumnHeader12.Text = "Lokasi"
        Me.ColumnHeader12.Width = 160
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Kode Barang"
        Me.ColumnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader9.Width = 145
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "Nama Barang"
        Me.ColumnHeader10.Width = 358
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "Satuan"
        Me.ColumnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader11.Width = 66
        '
        'ColumnHeader14
        '
        Me.ColumnHeader14.Text = "Id Group Jenis"
        Me.ColumnHeader14.Width = 0
        '
        'Pengeluaran_Barang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1142, 627)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.CmbSOBrg)
        Me.Controls.Add(Me.LvBrg)
        Me.Controls.Add(Me.LvCost)
        Me.Controls.Add(Me.BtnRefresh)
        Me.Controls.Add(Me.BtnSimpan)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CmbSO)
        Me.Controls.Add(Me.TxtNoFaktur)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Pengeluaran_Barang"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents TxtNoFaktur As TextBox
    Friend WithEvents CmbSO As ComboBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TxtIDCost As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtKetPB As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents TxtJlh As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents TxtNamaBarang As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TxtKodeBarang As TextBox
    Friend WithEvents LvBarang As ListView
    Friend WithEvents BtnOK As Button
    Friend WithEvents BtnClear As Button
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents TxtTotal As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents BtnSimpan As Button
    Friend WithEvents BtnRefresh As Button
    Friend WithEvents TxtKet As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents LvCost As ListView
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents TxtKodeCost As TextBox
    Friend WithEvents TxtSatuan As TextBox
    Friend WithEvents ColumnHeader8 As ColumnHeader
    Friend WithEvents LvBrg As ListView
    Friend WithEvents ColumnHeader9 As ColumnHeader
    Friend WithEvents ColumnHeader10 As ColumnHeader
    Friend WithEvents ColumnHeader11 As ColumnHeader
    Friend WithEvents CmbSOBrg As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents ColumnHeader12 As ColumnHeader
    Friend WithEvents ColumnHeader13 As ColumnHeader
    Friend WithEvents ColumnHeader14 As ColumnHeader
End Class
