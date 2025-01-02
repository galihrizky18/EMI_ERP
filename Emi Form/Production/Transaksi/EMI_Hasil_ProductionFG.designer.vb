<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Hasil_ProductionFG
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Txt_NoFak_TF = New System.Windows.Forms.TextBox()
        Me.TxtFormulator_NoFaktur = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.TxtJam = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtNoSplit = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
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
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtHasilProduksi = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.BtnFormulator_Refresh = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TxtGoodStock = New System.Windows.Forms.TextBox()
        Me.TxtBadStock = New System.Windows.Forms.TextBox()
        Me.TxtJmlScrap = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtSatuanScrap = New System.Windows.Forms.TextBox()
        Me.txtSatuanQty = New System.Windows.Forms.TextBox()
        Me.TxtKodeBarang = New System.Windows.Forms.TextBox()
        Me.TxtSatKecilProduksi = New System.Windows.Forms.TextBox()
        Me.TxtSatKecilScrap = New System.Windows.Forms.TextBox()
        Me.Cmb_LokasiSimpan = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TxtNama = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel8)
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Txt_NoFak_TF)
        Me.Panel1.Controls.Add(Me.TxtFormulator_NoFaktur)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(898, 51)
        Me.Panel1.TabIndex = 22
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Location = New System.Drawing.Point(876, 4)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(19, 489)
        Me.Panel8.TabIndex = 36
        Me.Panel8.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(285, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Transaksi - Hasil Produksi"
        '
        'Txt_NoFak_TF
        '
        Me.Txt_NoFak_TF.BackColor = System.Drawing.Color.Goldenrod
        Me.Txt_NoFak_TF.Enabled = False
        Me.Txt_NoFak_TF.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_NoFak_TF.ForeColor = System.Drawing.SystemColors.Window
        Me.Txt_NoFak_TF.Location = New System.Drawing.Point(435, 26)
        Me.Txt_NoFak_TF.MaxLength = 30
        Me.Txt_NoFak_TF.Name = "Txt_NoFak_TF"
        Me.Txt_NoFak_TF.ReadOnly = True
        Me.Txt_NoFak_TF.Size = New System.Drawing.Size(227, 22)
        Me.Txt_NoFak_TF.TabIndex = 0
        Me.Txt_NoFak_TF.Visible = False
        '
        'TxtFormulator_NoFaktur
        '
        Me.TxtFormulator_NoFaktur.BackColor = System.Drawing.Color.Goldenrod
        Me.TxtFormulator_NoFaktur.Enabled = False
        Me.TxtFormulator_NoFaktur.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFormulator_NoFaktur.ForeColor = System.Drawing.SystemColors.Window
        Me.TxtFormulator_NoFaktur.Location = New System.Drawing.Point(435, 4)
        Me.TxtFormulator_NoFaktur.MaxLength = 30
        Me.TxtFormulator_NoFaktur.Name = "TxtFormulator_NoFaktur"
        Me.TxtFormulator_NoFaktur.ReadOnly = True
        Me.TxtFormulator_NoFaktur.Size = New System.Drawing.Size(227, 22)
        Me.TxtFormulator_NoFaktur.TabIndex = 0
        Me.TxtFormulator_NoFaktur.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1073, 10)
        Me.Panel2.TabIndex = 34
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 64)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 489)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(877, 57)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 471)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'TxtJam
        '
        Me.TxtJam.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtJam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtJam.Enabled = False
        Me.TxtJam.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtJam.Location = New System.Drawing.Point(192, 119)
        Me.TxtJam.MaxLength = 50
        Me.TxtJam.Name = "TxtJam"
        Me.TxtJam.Size = New System.Drawing.Size(165, 22)
        Me.TxtJam.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(23, 65)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 20)
        Me.Label6.TabIndex = 349
        Me.Label6.Text = "No. Transaksi"
        '
        'TxtNoSplit
        '
        Me.TxtNoSplit.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtNoSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtNoSplit.Enabled = False
        Me.TxtNoSplit.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtNoSplit.Location = New System.Drawing.Point(192, 63)
        Me.TxtNoSplit.MaxLength = 50
        Me.TxtNoSplit.Name = "TxtNoSplit"
        Me.TxtNoSplit.Size = New System.Drawing.Size(165, 22)
        Me.TxtNoSplit.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label7.Location = New System.Drawing.Point(23, 91)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 20)
        Me.Label7.TabIndex = 351
        Me.Label7.Text = "Tanggal"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd MMM yyyy"
        Me.DateTimePicker1.Enabled = False
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(192, 92)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(165, 20)
        Me.DateTimePicker1.TabIndex = 2
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
        Me.Btn_Simpan.Location = New System.Drawing.Point(22, 561)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Simpan.TabIndex = 10
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(20, 598)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1072, 16)
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
        Me.Panel4.Location = New System.Drawing.Point(20, 378)
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
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox2.Enabled = False
        Me.TextBox2.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TextBox2.Location = New System.Drawing.Point(731, 70)
        Me.TextBox2.MaxLength = 50
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(165, 22)
        Me.TextBox2.TabIndex = 5
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TextBox2.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label8.Location = New System.Drawing.Point(387, 66)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(97, 20)
        Me.Label8.TabIndex = 391
        Me.Label8.Text = "Qty Produksi"
        Me.Label8.Visible = False
        '
        'TxtHasilProduksi
        '
        Me.TxtHasilProduksi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtHasilProduksi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtHasilProduksi.Enabled = False
        Me.TxtHasilProduksi.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtHasilProduksi.Location = New System.Drawing.Point(191, 236)
        Me.TxtHasilProduksi.MaxLength = 50
        Me.TxtHasilProduksi.Name = "TxtHasilProduksi"
        Me.TxtHasilProduksi.Size = New System.Drawing.Size(208, 22)
        Me.TxtHasilProduksi.TabIndex = 6
        Me.TxtHasilProduksi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label9.Location = New System.Drawing.Point(22, 237)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(135, 20)
        Me.Label9.TabIndex = 393
        Me.Label9.Text = "Qty Hasil Produksi"
        '
        'BtnFormulator_Refresh
        '
        Me.BtnFormulator_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnFormulator_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnFormulator_Refresh.ForeColor = System.Drawing.Color.White
        Me.BtnFormulator_Refresh.Location = New System.Drawing.Point(112, 561)
        Me.BtnFormulator_Refresh.Name = "BtnFormulator_Refresh"
        Me.BtnFormulator_Refresh.Size = New System.Drawing.Size(84, 36)
        Me.BtnFormulator_Refresh.TabIndex = 11
        Me.BtnFormulator_Refresh.Text = "&Refresh"
        Me.BtnFormulator_Refresh.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label10.Location = New System.Drawing.Point(22, 149)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(94, 20)
        Me.Label10.TabIndex = 397
        Me.Label10.Text = "Kode Barang"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label11.Location = New System.Drawing.Point(23, 265)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 20)
        Me.Label11.TabIndex = 399
        Me.Label11.Text = "Good Stock"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label12.Location = New System.Drawing.Point(305, 266)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(79, 20)
        Me.Label12.TabIndex = 400
        Me.Label12.Text = "Bad Stock"
        '
        'TxtGoodStock
        '
        Me.TxtGoodStock.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtGoodStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtGoodStock.Enabled = False
        Me.TxtGoodStock.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtGoodStock.Location = New System.Drawing.Point(191, 264)
        Me.TxtGoodStock.MaxLength = 50
        Me.TxtGoodStock.Name = "TxtGoodStock"
        Me.TxtGoodStock.Size = New System.Drawing.Size(108, 22)
        Me.TxtGoodStock.TabIndex = 8
        Me.TxtGoodStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtBadStock
        '
        Me.TxtBadStock.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtBadStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtBadStock.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtBadStock.Location = New System.Drawing.Point(391, 264)
        Me.TxtBadStock.MaxLength = 50
        Me.TxtBadStock.Name = "TxtBadStock"
        Me.TxtBadStock.Size = New System.Drawing.Size(114, 22)
        Me.TxtBadStock.TabIndex = 7
        Me.TxtBadStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtJmlScrap
        '
        Me.TxtJmlScrap.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtJmlScrap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtJmlScrap.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtJmlScrap.Location = New System.Drawing.Point(191, 292)
        Me.TxtJmlScrap.MaxLength = 50
        Me.TxtJmlScrap.Name = "TxtJmlScrap"
        Me.TxtJmlScrap.Size = New System.Drawing.Size(166, 22)
        Me.TxtJmlScrap.TabIndex = 401
        Me.TxtJmlScrap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label13.Location = New System.Drawing.Point(23, 293)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(49, 20)
        Me.Label13.TabIndex = 402
        Me.Label13.Text = "Scrap"
        '
        'txtSatuanScrap
        '
        Me.txtSatuanScrap.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtSatuanScrap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSatuanScrap.Enabled = False
        Me.txtSatuanScrap.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.txtSatuanScrap.Location = New System.Drawing.Point(362, 292)
        Me.txtSatuanScrap.MaxLength = 50
        Me.txtSatuanScrap.Name = "txtSatuanScrap"
        Me.txtSatuanScrap.Size = New System.Drawing.Size(143, 22)
        Me.txtSatuanScrap.TabIndex = 403
        '
        'txtSatuanQty
        '
        Me.txtSatuanQty.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtSatuanQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSatuanQty.Enabled = False
        Me.txtSatuanQty.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.txtSatuanQty.Location = New System.Drawing.Point(405, 236)
        Me.txtSatuanQty.MaxLength = 50
        Me.txtSatuanQty.Name = "txtSatuanQty"
        Me.txtSatuanQty.Size = New System.Drawing.Size(100, 22)
        Me.txtSatuanQty.TabIndex = 404
        '
        'TxtKodeBarang
        '
        Me.TxtKodeBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKodeBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKodeBarang.Enabled = False
        Me.TxtKodeBarang.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtKodeBarang.Location = New System.Drawing.Point(191, 147)
        Me.TxtKodeBarang.MaxLength = 50
        Me.TxtKodeBarang.Name = "TxtKodeBarang"
        Me.TxtKodeBarang.Size = New System.Drawing.Size(166, 22)
        Me.TxtKodeBarang.TabIndex = 405
        '
        'TxtSatKecilProduksi
        '
        Me.TxtSatKecilProduksi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtSatKecilProduksi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSatKecilProduksi.Enabled = False
        Me.TxtSatKecilProduksi.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtSatKecilProduksi.Location = New System.Drawing.Point(544, 120)
        Me.TxtSatKecilProduksi.MaxLength = 50
        Me.TxtSatKecilProduksi.Name = "TxtSatKecilProduksi"
        Me.TxtSatKecilProduksi.Size = New System.Drawing.Size(100, 22)
        Me.TxtSatKecilProduksi.TabIndex = 406
        Me.TxtSatKecilProduksi.Visible = False
        '
        'TxtSatKecilScrap
        '
        Me.TxtSatKecilScrap.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtSatKecilScrap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSatKecilScrap.Enabled = False
        Me.TxtSatKecilScrap.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtSatKecilScrap.Location = New System.Drawing.Point(448, 92)
        Me.TxtSatKecilScrap.MaxLength = 50
        Me.TxtSatKecilScrap.Name = "TxtSatKecilScrap"
        Me.TxtSatKecilScrap.Size = New System.Drawing.Size(100, 22)
        Me.TxtSatKecilScrap.TabIndex = 407
        Me.TxtSatKecilScrap.Visible = False
        '
        'Cmb_LokasiSimpan
        '
        Me.Cmb_LokasiSimpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Cmb_LokasiSimpan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_LokasiSimpan.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Cmb_LokasiSimpan.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_LokasiSimpan.FormattingEnabled = True
        Me.Cmb_LokasiSimpan.Location = New System.Drawing.Point(191, 320)
        Me.Cmb_LokasiSimpan.Name = "Cmb_LokasiSimpan"
        Me.Cmb_LokasiSimpan.Size = New System.Drawing.Size(314, 25)
        Me.Cmb_LokasiSimpan.TabIndex = 408
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(22, 322)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 20)
        Me.Label2.TabIndex = 402
        Me.Label2.Text = "Lokasi"
        '
        'Txt_Keterangan
        '
        Me.Txt_Keterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Txt_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Keterangan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Keterangan.Location = New System.Drawing.Point(192, 351)
        Me.Txt_Keterangan.MaxLength = 50
        Me.Txt_Keterangan.Name = "Txt_Keterangan"
        Me.Txt_Keterangan.Size = New System.Drawing.Size(313, 22)
        Me.Txt_Keterangan.TabIndex = 401
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label14.Location = New System.Drawing.Point(23, 352)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(86, 20)
        Me.Label14.TabIndex = 402
        Me.Label14.Text = "Keterangan"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label15.Location = New System.Drawing.Point(23, 177)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(100, 20)
        Me.Label15.TabIndex = 409
        Me.Label15.Text = "Nama Barang"
        '
        'TxtNama
        '
        Me.TxtNama.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtNama.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtNama.Enabled = False
        Me.TxtNama.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtNama.Location = New System.Drawing.Point(191, 176)
        Me.TxtNama.MaxLength = 50
        Me.TxtNama.Name = "TxtNama"
        Me.TxtNama.Size = New System.Drawing.Size(314, 22)
        Me.TxtNama.TabIndex = 410
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label16.Location = New System.Drawing.Point(23, 120)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(38, 20)
        Me.Label16.TabIndex = 411
        Me.Label16.Text = "Jam"
        '
        'ListView1
        '
        Me.ListView1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(23, 391)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(851, 156)
        Me.ListView1.TabIndex = 412
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
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
        Me.PanelGradient1.Size = New System.Drawing.Size(898, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(22, 547)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1072, 12)
        Me.Panel6.TabIndex = 39
        Me.Panel6.Visible = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label17.Location = New System.Drawing.Point(22, 206)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(104, 20)
        Me.Label17.TabIndex = 413
        Me.Label17.Text = "Jumlah Pallet"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Enabled = False
        Me.TextBox1.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TextBox1.Location = New System.Drawing.Point(191, 205)
        Me.TextBox1.MaxLength = 50
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(108, 22)
        Me.TextBox1.TabIndex = 414
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'EMI_Hasil_ProductionFG
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(898, 615)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.TxtNama)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Cmb_LokasiSimpan)
        Me.Controls.Add(Me.TxtSatKecilScrap)
        Me.Controls.Add(Me.TxtSatKecilProduksi)
        Me.Controls.Add(Me.TxtKodeBarang)
        Me.Controls.Add(Me.txtSatuanQty)
        Me.Controls.Add(Me.txtSatuanScrap)
        Me.Controls.Add(Me.Txt_Keterangan)
        Me.Controls.Add(Me.TxtJmlScrap)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.TxtBadStock)
        Me.Controls.Add(Me.TxtGoodStock)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.BtnFormulator_Refresh)
        Me.Controls.Add(Me.TxtHasilProduksi)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.ListView2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.ComboBox3)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtNoSplit)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.TxtJam)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Hasil_ProductionFG"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents TxtJam As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtNoSplit As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents ComboBox3 As ComboBox
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents TxtFormulator_NoFaktur As TextBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents ListView2 As ListView
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TxtHasilProduksi As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents BtnFormulator_Refresh As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents TxtGoodStock As TextBox
    Friend WithEvents TxtBadStock As TextBox
    Friend WithEvents TxtJmlScrap As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents txtSatuanScrap As TextBox
    Friend WithEvents txtSatuanQty As TextBox
    Friend WithEvents TxtKodeBarang As TextBox
    Friend WithEvents TxtSatKecilProduksi As TextBox
    Friend WithEvents TxtSatKecilScrap As TextBox
    Friend WithEvents Cmb_LokasiSimpan As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_NoFak_TF As TextBox
    Friend WithEvents Txt_Keterangan As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents TxtNama As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents ListView1 As ListView
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label17 As Label
    Friend WithEvents TextBox1 As TextBox
End Class
