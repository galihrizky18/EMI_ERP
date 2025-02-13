<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_DownPayment
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
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.BtnFormulator_Refresh = New System.Windows.Forms.Button()
        Me.TxtKodeSupplier = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtNamaSupplier = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TxtKeterangan = New System.Windows.Forms.TextBox()
        Me.LvSupplier = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TxtTotalIDR = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TxtKurs = New System.Windows.Forms.TextBox()
        Me.CmbMUA = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CbNoFaktur = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtNilai = New System.Windows.Forms.TextBox()
        Me.CmbNoPO = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CmbRekening = New System.Windows.Forms.ComboBox()
        Me.Dtp1 = New System.Windows.Forms.DateTimePicker()
        Me.CmbLokasi = New System.Windows.Forms.ComboBox()
        Me.TxtFakturPembayaran = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel8)
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(683, 51)
        Me.Panel1.TabIndex = 22
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Location = New System.Drawing.Point(846, 4)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(19, 489)
        Me.Panel8.TabIndex = 36
        Me.Panel8.Visible = False
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
        Me.PanelGradient1.Size = New System.Drawing.Size(683, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(352, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Transaksi - Pembayaran Di Muka"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 49)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1073, 12)
        Me.Panel2.TabIndex = 34
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 61)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 489)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(664, 62)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 471)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'ComboBox3
        '
        Me.ComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox3.Enabled = False
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Items.AddRange(New Object() {"ZCM-948X", "JBC-0293L"})
        Me.ComboBox3.Location = New System.Drawing.Point(1188, 326)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(277, 24)
        Me.ComboBox3.TabIndex = 355
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(35, 346)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Simpan.TabIndex = 10
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(20, 382)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1072, 12)
        Me.Panel7.TabIndex = 37
        Me.Panel7.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(1082, 272)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 20)
        Me.Label3.TabIndex = 383
        Me.Label3.Text = "Line"
        Me.Label3.Visible = False
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Enabled = False
        Me.TextBox3.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TextBox3.Location = New System.Drawing.Point(1188, 298)
        Me.TextBox3.MaxLength = 50
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(277, 22)
        Me.TextBox3.TabIndex = 386
        Me.TextBox3.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(1082, 299)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 20)
        Me.Label4.TabIndex = 385
        Me.Label4.Text = "Batch No."
        Me.Label4.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Enabled = False
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(1082, 326)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 20)
        Me.Label5.TabIndex = 387
        Me.Label5.Text = "Operator"
        Me.Label5.Visible = False
        '
        'ComboBox2
        '
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.Enabled = False
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Items.AddRange(New Object() {"RUDI"})
        Me.ComboBox2.Location = New System.Drawing.Point(1188, 271)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(277, 24)
        Me.ComboBox2.TabIndex = 348
        Me.ComboBox2.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(20, 336)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1072, 12)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'ListView2
        '
        Me.ListView2.Enabled = False
        Me.ListView2.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.ListView2.FullRowSelect = True
        Me.ListView2.GridLines = True
        Me.ListView2.HideSelection = False
        Me.ListView2.Location = New System.Drawing.Point(1086, 365)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(1030, 332)
        Me.ListView2.TabIndex = 390
        Me.ListView2.UseCompatibleStateImageBehavior = False
        Me.ListView2.View = System.Windows.Forms.View.Details
        Me.ListView2.Visible = False
        '
        'BtnFormulator_Refresh
        '
        Me.BtnFormulator_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnFormulator_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnFormulator_Refresh.ForeColor = System.Drawing.Color.White
        Me.BtnFormulator_Refresh.Location = New System.Drawing.Point(125, 346)
        Me.BtnFormulator_Refresh.Name = "BtnFormulator_Refresh"
        Me.BtnFormulator_Refresh.Size = New System.Drawing.Size(84, 36)
        Me.BtnFormulator_Refresh.TabIndex = 11
        Me.BtnFormulator_Refresh.Text = "&Refresh"
        Me.BtnFormulator_Refresh.UseVisualStyleBackColor = False
        '
        'TxtKodeSupplier
        '
        Me.TxtKodeSupplier.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKodeSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKodeSupplier.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtKodeSupplier.Location = New System.Drawing.Point(121, 29)
        Me.TxtKodeSupplier.MaxLength = 50
        Me.TxtKodeSupplier.Name = "TxtKodeSupplier"
        Me.TxtKodeSupplier.Size = New System.Drawing.Size(148, 22)
        Me.TxtKodeSupplier.TabIndex = 391
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(18, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 20)
        Me.Label2.TabIndex = 392
        Me.Label2.Text = "Supplier"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtNamaSupplier
        '
        Me.TxtNamaSupplier.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtNamaSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtNamaSupplier.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtNamaSupplier.Location = New System.Drawing.Point(275, 29)
        Me.TxtNamaSupplier.MaxLength = 50
        Me.TxtNamaSupplier.Name = "TxtNamaSupplier"
        Me.TxtNamaSupplier.Size = New System.Drawing.Size(197, 22)
        Me.TxtNamaSupplier.TabIndex = 393
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LvSupplier)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.TxtKeterangan)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.TxtTotalIDR)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.TxtKurs)
        Me.GroupBox1.Controls.Add(Me.CmbMUA)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.CbNoFaktur)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.TxtNilai)
        Me.GroupBox1.Controls.Add(Me.CmbNoPO)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.TxtNamaSupplier)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TxtKodeSupplier)
        Me.GroupBox1.Controls.Add(Me.CmbRekening)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 91)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(637, 240)
        Me.GroupBox1.TabIndex = 394
        Me.GroupBox1.TabStop = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label13.Location = New System.Drawing.Point(18, 184)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(86, 20)
        Me.Label13.TabIndex = 428
        Me.Label13.Text = "Keterangan"
        '
        'TxtKeterangan
        '
        Me.TxtKeterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKeterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKeterangan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtKeterangan.Location = New System.Drawing.Point(121, 183)
        Me.TxtKeterangan.MaxLength = 50
        Me.TxtKeterangan.Name = "TxtKeterangan"
        Me.TxtKeterangan.Size = New System.Drawing.Size(351, 22)
        Me.TxtKeterangan.TabIndex = 427
        Me.TxtKeterangan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LvSupplier
        '
        Me.LvSupplier.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.LvSupplier.FullRowSelect = True
        Me.LvSupplier.GridLines = True
        Me.LvSupplier.HideSelection = False
        Me.LvSupplier.Location = New System.Drawing.Point(494, 24)
        Me.LvSupplier.Name = "LvSupplier"
        Me.LvSupplier.Size = New System.Drawing.Size(393, 150)
        Me.LvSupplier.TabIndex = 418
        Me.LvSupplier.UseCompatibleStateImageBehavior = False
        Me.LvSupplier.View = System.Windows.Forms.View.Details
        Me.LvSupplier.Visible = False
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Kode Supplier"
        Me.ColumnHeader1.Width = 102
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Nama"
        Me.ColumnHeader2.Width = 280
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label12.Location = New System.Drawing.Point(247, 125)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(69, 20)
        Me.Label12.TabIndex = 426
        Me.Label12.Text = "Total IDR"
        '
        'TxtTotalIDR
        '
        Me.TxtTotalIDR.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtTotalIDR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtTotalIDR.Enabled = False
        Me.TxtTotalIDR.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtTotalIDR.Location = New System.Drawing.Point(322, 124)
        Me.TxtTotalIDR.MaxLength = 50
        Me.TxtTotalIDR.Name = "TxtTotalIDR"
        Me.TxtTotalIDR.Size = New System.Drawing.Size(150, 22)
        Me.TxtTotalIDR.TabIndex = 425
        Me.TxtTotalIDR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label11.Location = New System.Drawing.Point(18, 125)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(40, 20)
        Me.Label11.TabIndex = 424
        Me.Label11.Text = "Kurs"
        '
        'TxtKurs
        '
        Me.TxtKurs.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKurs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKurs.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtKurs.Location = New System.Drawing.Point(121, 124)
        Me.TxtKurs.MaxLength = 50
        Me.TxtKurs.Name = "TxtKurs"
        Me.TxtKurs.Size = New System.Drawing.Size(120, 22)
        Me.TxtKurs.TabIndex = 423
        Me.TxtKurs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CmbMUA
        '
        Me.CmbMUA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbMUA.Enabled = False
        Me.CmbMUA.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbMUA.FormattingEnabled = True
        Me.CmbMUA.Location = New System.Drawing.Point(369, 94)
        Me.CmbMUA.Name = "CmbMUA"
        Me.CmbMUA.Size = New System.Drawing.Size(103, 25)
        Me.CmbMUA.TabIndex = 422
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label9.Location = New System.Drawing.Point(18, 154)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(85, 20)
        Me.Label9.TabIndex = 421
        Me.Label9.Text = "Rek Tujuan"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(10, 130)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(0, 20)
        Me.Label6.TabIndex = 419
        '
        'CbNoFaktur
        '
        Me.CbNoFaktur.AutoSize = True
        Me.CbNoFaktur.Location = New System.Drawing.Point(121, 66)
        Me.CbNoFaktur.Name = "CbNoFaktur"
        Me.CbNoFaktur.Size = New System.Drawing.Size(15, 14)
        Me.CbNoFaktur.TabIndex = 417
        Me.CbNoFaktur.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label8.Location = New System.Drawing.Point(18, 97)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 20)
        Me.Label8.TabIndex = 411
        Me.Label8.Text = "Nilai"
        '
        'TxtNilai
        '
        Me.TxtNilai.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtNilai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtNilai.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtNilai.Location = New System.Drawing.Point(121, 96)
        Me.TxtNilai.MaxLength = 50
        Me.TxtNilai.Name = "TxtNilai"
        Me.TxtNilai.Size = New System.Drawing.Size(242, 22)
        Me.TxtNilai.TabIndex = 410
        Me.TxtNilai.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CmbNoPO
        '
        Me.CmbNoPO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbNoPO.Enabled = False
        Me.CmbNoPO.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbNoPO.FormattingEnabled = True
        Me.CmbNoPO.Location = New System.Drawing.Point(142, 60)
        Me.CmbNoPO.Name = "CmbNoPO"
        Me.CmbNoPO.Size = New System.Drawing.Size(330, 25)
        Me.CmbNoPO.TabIndex = 409
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label7.Location = New System.Drawing.Point(18, 62)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(51, 20)
        Me.Label7.TabIndex = 395
        Me.Label7.Text = "No PO"
        '
        'CmbRekening
        '
        Me.CmbRekening.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbRekening.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CmbRekening.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbRekening.FormattingEnabled = True
        Me.CmbRekening.Location = New System.Drawing.Point(121, 152)
        Me.CmbRekening.Name = "CmbRekening"
        Me.CmbRekening.Size = New System.Drawing.Size(351, 25)
        Me.CmbRekening.TabIndex = 420
        '
        'Dtp1
        '
        Me.Dtp1.CustomFormat = "dd MMM yyyy"
        Me.Dtp1.Enabled = False
        Me.Dtp1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Dtp1.Location = New System.Drawing.Point(230, 65)
        Me.Dtp1.Name = "Dtp1"
        Me.Dtp1.Size = New System.Drawing.Size(162, 20)
        Me.Dtp1.TabIndex = 418
        '
        'CmbLokasi
        '
        Me.CmbLokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbLokasi.Enabled = False
        Me.CmbLokasi.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbLokasi.FormattingEnabled = True
        Me.CmbLokasi.Location = New System.Drawing.Point(483, 63)
        Me.CmbLokasi.Name = "CmbLokasi"
        Me.CmbLokasi.Size = New System.Drawing.Size(179, 25)
        Me.CmbLokasi.TabIndex = 419
        '
        'TxtFakturPembayaran
        '
        Me.TxtFakturPembayaran.BackColor = System.Drawing.Color.Goldenrod
        Me.TxtFakturPembayaran.Enabled = False
        Me.TxtFakturPembayaran.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFakturPembayaran.ForeColor = System.Drawing.SystemColors.Window
        Me.TxtFakturPembayaran.Location = New System.Drawing.Point(19, 62)
        Me.TxtFakturPembayaran.MaxLength = 30
        Me.TxtFakturPembayaran.Name = "TxtFakturPembayaran"
        Me.TxtFakturPembayaran.ReadOnly = True
        Me.TxtFakturPembayaran.Size = New System.Drawing.Size(205, 22)
        Me.TxtFakturPembayaran.TabIndex = 417
        '
        'EMI_Transaksi_Pembayaran_DiMuka
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(683, 394)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Dtp1)
        Me.Controls.Add(Me.CmbLokasi)
        Me.Controls.Add(Me.TxtFakturPembayaran)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ListView2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.ComboBox3)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.BtnFormulator_Refresh)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Transaksi_Pembayaran_DiMuka"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents ComboBox3 As ComboBox
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents ListView2 As ListView
    Friend WithEvents BtnFormulator_Refresh As Button
    Friend WithEvents TxtKodeSupplier As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtNamaSupplier As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents TxtNilai As TextBox
    Friend WithEvents CmbNoPO As ComboBox
    Friend WithEvents CbNoFaktur As CheckBox
    Friend WithEvents LvSupplier As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents Dtp1 As DateTimePicker
    Friend WithEvents CmbLokasi As ComboBox
    Friend WithEvents TxtFakturPembayaran As TextBox
    Friend WithEvents CmbRekening As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents CmbMUA As ComboBox
    Friend WithEvents TxtTotalIDR As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents TxtKurs As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents TxtKeterangan As TextBox
End Class
