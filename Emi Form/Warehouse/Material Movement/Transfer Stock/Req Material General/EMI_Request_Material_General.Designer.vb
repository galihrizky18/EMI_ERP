<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Request_Material_General
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
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.TxtNoFaktur = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CmbSo_Sup = New System.Windows.Forms.ComboBox()
        Me.CmbSO_Req = New System.Windows.Forms.ComboBox()
        Me.Txt_Ket = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TxtJlh = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtSatuan = New System.Windows.Forms.TextBox()
        Me.TxtKet = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtTotal = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.LvBarang = New System.Windows.Forms.ListView()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.BtnClear = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Txt_Stock = New System.Windows.Forms.TextBox()
        Me.Stock = New System.Windows.Forms.Label()
        Me.TxtNamaBarang = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtKodeBarang = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.BtnSimpan = New System.Windows.Forms.Button()
        Me.BtnRefresh = New System.Windows.Forms.Button()
        Me.LvBrg = New System.Windows.Forms.ListView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1212, 49)
        Me.Panel1.TabIndex = 233
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(7, 7)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(290, 30)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Request Material (General)"
        '
        'TxtNoFaktur
        '
        Me.TxtNoFaktur.BackColor = System.Drawing.Color.Goldenrod
        Me.TxtNoFaktur.Location = New System.Drawing.Point(9, 60)
        Me.TxtNoFaktur.Name = "TxtNoFaktur"
        Me.TxtNoFaktur.ReadOnly = True
        Me.TxtNoFaktur.Size = New System.Drawing.Size(263, 23)
        Me.TxtNoFaktur.TabIndex = 234
        Me.TxtNoFaktur.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.CmbSo_Sup)
        Me.GroupBox1.Controls.Add(Me.CmbSO_Req)
        Me.GroupBox1.Controls.Add(Me.Txt_Ket)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 84)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1180, 90)
        Me.GroupBox1.TabIndex = 236
        Me.GroupBox1.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(312, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 20)
        Me.Label4.TabIndex = 439
        Me.Label4.Text = "=>"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(6, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(107, 17)
        Me.Label1.TabIndex = 438
        Me.Label1.Text = "Lokasi Transfer"
        '
        'CmbSo_Sup
        '
        Me.CmbSo_Sup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSo_Sup.Enabled = False
        Me.CmbSo_Sup.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbSo_Sup.FormattingEnabled = True
        Me.CmbSo_Sup.Location = New System.Drawing.Point(349, 19)
        Me.CmbSo_Sup.Name = "CmbSo_Sup"
        Me.CmbSo_Sup.Size = New System.Drawing.Size(163, 25)
        Me.CmbSo_Sup.TabIndex = 437
        '
        'CmbSO_Req
        '
        Me.CmbSO_Req.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSO_Req.Enabled = False
        Me.CmbSO_Req.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbSO_Req.FormattingEnabled = True
        Me.CmbSO_Req.Location = New System.Drawing.Point(140, 18)
        Me.CmbSO_Req.Name = "CmbSO_Req"
        Me.CmbSO_Req.Size = New System.Drawing.Size(163, 25)
        Me.CmbSO_Req.TabIndex = 436
        '
        'Txt_Ket
        '
        Me.Txt_Ket.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Ket.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Ket.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Ket.Location = New System.Drawing.Point(140, 50)
        Me.Txt_Ket.Name = "Txt_Ket"
        Me.Txt_Ket.Size = New System.Drawing.Size(372, 23)
        Me.Txt_Ket.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 18)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Keterangan"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TxtJlh)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.TxtSatuan)
        Me.GroupBox2.Controls.Add(Me.TxtKet)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.TxtTotal)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.LvBarang)
        Me.GroupBox2.Controls.Add(Me.BtnOK)
        Me.GroupBox2.Controls.Add(Me.BtnClear)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Txt_Stock)
        Me.GroupBox2.Controls.Add(Me.Stock)
        Me.GroupBox2.Controls.Add(Me.TxtKodeBarang)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 180)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1186, 403)
        Me.GroupBox2.TabIndex = 237
        Me.GroupBox2.TabStop = False
        '
        'TxtJlh
        '
        Me.TxtJlh.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtJlh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtJlh.Enabled = False
        Me.TxtJlh.Location = New System.Drawing.Point(404, 38)
        Me.TxtJlh.MaxLength = 8
        Me.TxtJlh.Name = "TxtJlh"
        Me.TxtJlh.Size = New System.Drawing.Size(181, 23)
        Me.TxtJlh.TabIndex = 436
        Me.TxtJlh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Location = New System.Drawing.Point(404, 14)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(181, 23)
        Me.Label5.TabIndex = 437
        Me.Label5.Text = "Jumlah"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtSatuan
        '
        Me.TxtSatuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtSatuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSatuan.Enabled = False
        Me.TxtSatuan.Location = New System.Drawing.Point(588, 38)
        Me.TxtSatuan.MaxLength = 8
        Me.TxtSatuan.Name = "TxtSatuan"
        Me.TxtSatuan.ReadOnly = True
        Me.TxtSatuan.Size = New System.Drawing.Size(103, 23)
        Me.TxtSatuan.TabIndex = 435
        Me.TxtSatuan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TxtKet
        '
        Me.TxtKet.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKet.Enabled = False
        Me.TxtKet.Location = New System.Drawing.Point(694, 37)
        Me.TxtKet.MaxLength = 50
        Me.TxtKet.Name = "TxtKet"
        Me.TxtKet.Size = New System.Drawing.Size(410, 23)
        Me.TxtKet.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Location = New System.Drawing.Point(694, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(410, 22)
        Me.Label2.TabIndex = 434
        Me.Label2.Text = "Keterangan"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtTotal
        '
        Me.TxtTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtTotal.Location = New System.Drawing.Point(1066, 374)
        Me.TxtTotal.Name = "TxtTotal"
        Me.TxtTotal.ReadOnly = True
        Me.TxtTotal.Size = New System.Drawing.Size(114, 23)
        Me.TxtTotal.TabIndex = 433
        Me.TxtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(1012, 376)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(39, 18)
        Me.Label18.TabIndex = 432
        Me.Label18.Text = "Total"
        '
        'LvBarang
        '
        Me.LvBarang.FullRowSelect = True
        Me.LvBarang.GridLines = True
        Me.LvBarang.HideSelection = False
        Me.LvBarang.Location = New System.Drawing.Point(5, 63)
        Me.LvBarang.Name = "LvBarang"
        Me.LvBarang.Size = New System.Drawing.Size(1175, 305)
        Me.LvBarang.TabIndex = 421
        Me.LvBarang.UseCompatibleStateImageBehavior = False
        Me.LvBarang.View = System.Windows.Forms.View.Details
        '
        'BtnOK
        '
        Me.BtnOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnOK.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnOK.ForeColor = System.Drawing.Color.White
        Me.BtnOK.Location = New System.Drawing.Point(1105, 35)
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
        Me.BtnClear.Location = New System.Drawing.Point(1105, 12)
        Me.BtnClear.Name = "BtnClear"
        Me.BtnClear.Size = New System.Drawing.Size(73, 26)
        Me.BtnClear.TabIndex = 10
        Me.BtnClear.Text = "&Clear"
        Me.BtnClear.UseVisualStyleBackColor = False
        '
        'Label13
        '
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Location = New System.Drawing.Point(588, 14)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(103, 22)
        Me.Label13.TabIndex = 19
        Me.Label13.Text = "Satuan"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Txt_Stock
        '
        Me.Txt_Stock.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Stock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Stock.Enabled = False
        Me.Txt_Stock.Location = New System.Drawing.Point(220, 38)
        Me.Txt_Stock.MaxLength = 8
        Me.Txt_Stock.Name = "Txt_Stock"
        Me.Txt_Stock.Size = New System.Drawing.Size(181, 23)
        Me.Txt_Stock.TabIndex = 7
        Me.Txt_Stock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Stock
        '
        Me.Stock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Stock.Location = New System.Drawing.Point(220, 14)
        Me.Stock.Name = "Stock"
        Me.Stock.Size = New System.Drawing.Size(181, 22)
        Me.Stock.TabIndex = 17
        Me.Stock.Text = "Stock"
        Me.Stock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtNamaBarang
        '
        Me.TxtNamaBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtNamaBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtNamaBarang.Location = New System.Drawing.Point(1212, 108)
        Me.TxtNamaBarang.Name = "TxtNamaBarang"
        Me.TxtNamaBarang.Size = New System.Drawing.Size(123, 23)
        Me.TxtNamaBarang.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Location = New System.Drawing.Point(1212, 84)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(123, 22)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Nama Barang"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtKodeBarang
        '
        Me.TxtKodeBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKodeBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKodeBarang.Location = New System.Drawing.Point(5, 37)
        Me.TxtKodeBarang.Name = "TxtKodeBarang"
        Me.TxtKodeBarang.Size = New System.Drawing.Size(212, 23)
        Me.TxtKodeBarang.TabIndex = 5
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
        'LvBrg
        '
        Me.LvBrg.FullRowSelect = True
        Me.LvBrg.GridLines = True
        Me.LvBrg.HideSelection = False
        Me.LvBrg.Location = New System.Drawing.Point(1265, 243)
        Me.LvBrg.Name = "LvBrg"
        Me.LvBrg.Size = New System.Drawing.Size(580, 228)
        Me.LvBrg.TabIndex = 239
        Me.LvBrg.UseCompatibleStateImageBehavior = False
        Me.LvBrg.View = System.Windows.Forms.View.Details
        Me.LvBrg.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(9, 49)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1087, 12)
        Me.Panel2.TabIndex = 240
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Controls.Add(Me.Panel9)
        Me.Panel3.Location = New System.Drawing.Point(0, 84)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(12, 649)
        Me.Panel3.TabIndex = 241
        Me.Panel3.Visible = False
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.Red
        Me.Panel9.Location = New System.Drawing.Point(13, 146)
        Me.Panel9.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(1091, 12)
        Me.Panel9.TabIndex = 38
        Me.Panel9.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Location = New System.Drawing.Point(1199, 84)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(12, 649)
        Me.Panel4.TabIndex = 241
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(13, 146)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1091, 12)
        Me.Panel5.TabIndex = 38
        Me.Panel5.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(21, 613)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1087, 12)
        Me.Panel6.TabIndex = 240
        Me.Panel6.Visible = False
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1212, 3)
        Me.PanelGradient1.TabIndex = 22
        '
        'EMI_Request_Material_General
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1212, 623)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.LvBrg)
        Me.Controls.Add(Me.BtnRefresh)
        Me.Controls.Add(Me.BtnSimpan)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TxtNoFaktur)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtNamaBarang)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Request_Material_General"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents TxtNoFaktur As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Txt_Stock As TextBox
    Friend WithEvents Stock As Label
    Friend WithEvents TxtNamaBarang As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TxtKodeBarang As TextBox
    Friend WithEvents LvBarang As ListView
    Friend WithEvents BtnOK As Button
    Friend WithEvents BtnClear As Button
    Friend WithEvents TxtTotal As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents BtnSimpan As Button
    Friend WithEvents BtnRefresh As Button
    Friend WithEvents TxtKet As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtSatuan As TextBox
    Friend WithEvents LvBrg As ListView
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Txt_Ket As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents CmbSo_Sup As ComboBox
    Friend WithEvents CmbSO_Req As ComboBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents TxtJlh As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel6 As Panel
End Class
