<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Produksi
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
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Txt_NoTransaksi = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Cmb_Operator = New System.Windows.Forms.ComboBox()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Txt_BatchNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Txt_Qty = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Cmb_Routing = New System.Windows.Forms.ComboBox()
        Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Txt_NoFaktur = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TxtQtyPO = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TxtQtyProduksi = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Cmb_Satuan = New System.Windows.Forms.ComboBox()
        Me.Txt_DisplayQtyProd = New System.Windows.Forms.TextBox()
        Me.Txt_DisplayQtyPO = New System.Windows.Forms.TextBox()
        Me.TxtQtyPO_Satuan = New System.Windows.Forms.TextBox()
        Me.TxtQtyProduksi_Satuan = New System.Windows.Forms.TextBox()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Txt_JumlahBatch = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Txt_QtyBatch = New System.Windows.Forms.TextBox()
        Me.Cmb_SatuanBatch = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(607, 51)
        Me.Panel1.TabIndex = 22
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Location = New System.Drawing.Point(787, 0)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(607, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(227, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Transaksi - Produksi"
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
        Me.Panel3.Size = New System.Drawing.Size(19, 489)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(587, 63)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 471)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(38, 73)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 20)
        Me.Label6.TabIndex = 349
        Me.Label6.Text = "No. Transaksi"
        '
        'Txt_NoTransaksi
        '
        Me.Txt_NoTransaksi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NoTransaksi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NoTransaksi.Enabled = False
        Me.Txt_NoTransaksi.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_NoTransaksi.Location = New System.Drawing.Point(218, 72)
        Me.Txt_NoTransaksi.MaxLength = 50
        Me.Txt_NoTransaksi.Name = "Txt_NoTransaksi"
        Me.Txt_NoTransaksi.Size = New System.Drawing.Size(353, 22)
        Me.Txt_NoTransaksi.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label7.Location = New System.Drawing.Point(38, 129)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(124, 20)
        Me.Label7.TabIndex = 351
        Me.Label7.Text = "Tanggal Produksi"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(218, 130)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(170, 20)
        Me.DateTimePicker1.TabIndex = 4
        '
        'Cmb_Operator
        '
        Me.Cmb_Operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Operator.FormattingEnabled = True
        Me.Cmb_Operator.Items.AddRange(New Object() {"ZCM-948X", "JBC-0293L"})
        Me.Cmb_Operator.Location = New System.Drawing.Point(214, 436)
        Me.Cmb_Operator.Name = "Cmb_Operator"
        Me.Cmb_Operator.Size = New System.Drawing.Size(354, 24)
        Me.Cmb_Operator.TabIndex = 11
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(41, 477)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Simpan.TabIndex = 12
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(19, 465)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1072, 12)
        Me.Panel7.TabIndex = 37
        Me.Panel7.Visible = False
        '
        'Txt_BatchNo
        '
        Me.Txt_BatchNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_BatchNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_BatchNo.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_BatchNo.Location = New System.Drawing.Point(214, 408)
        Me.Txt_BatchNo.MaxLength = 50
        Me.Txt_BatchNo.Name = "Txt_BatchNo"
        Me.Txt_BatchNo.Size = New System.Drawing.Size(354, 22)
        Me.Txt_BatchNo.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(36, 408)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 20)
        Me.Label4.TabIndex = 385
        Me.Label4.Text = "Keterangan"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(36, 437)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 20)
        Me.Label5.TabIndex = 387
        Me.Label5.Text = "Operator"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(362, 552)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1072, 12)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'Txt_KdBarang
        '
        Me.Txt_KdBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KdBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_KdBarang.Enabled = False
        Me.Txt_KdBarang.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_KdBarang.Location = New System.Drawing.Point(218, 182)
        Me.Txt_KdBarang.MaxLength = 50
        Me.Txt_KdBarang.Name = "Txt_KdBarang"
        Me.Txt_KdBarang.Size = New System.Drawing.Size(353, 22)
        Me.Txt_KdBarang.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label8.Location = New System.Drawing.Point(39, 182)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(94, 20)
        Me.Label8.TabIndex = 391
        Me.Label8.Text = "Kode Barang"
        '
        'Txt_Qty
        '
        Me.Txt_Qty.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Qty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Qty.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Qty.Location = New System.Drawing.Point(217, 292)
        Me.Txt_Qty.MaxLength = 50
        Me.Txt_Qty.Name = "Txt_Qty"
        Me.Txt_Qty.Size = New System.Drawing.Size(256, 22)
        Me.Txt_Qty.TabIndex = 8
        Me.Txt_Qty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label10.Location = New System.Drawing.Point(38, 292)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(74, 20)
        Me.Label10.TabIndex = 395
        Me.Label10.Text = "Qty input"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label11.Location = New System.Drawing.Point(36, 378)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(61, 20)
        Me.Label11.TabIndex = 422
        Me.Label11.Text = "Routing"
        '
        'Cmb_Routing
        '
        Me.Cmb_Routing.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Cmb_Routing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Routing.Enabled = False
        Me.Cmb_Routing.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Cmb_Routing.FormattingEnabled = True
        Me.Cmb_Routing.Items.AddRange(New Object() {"ROUTING 1 (Mixer, Hammer)", "ROUTING 2 (Mixer, Pellet)"})
        Me.Cmb_Routing.Location = New System.Drawing.Point(215, 376)
        Me.Cmb_Routing.Name = "Cmb_Routing"
        Me.Cmb_Routing.Size = New System.Drawing.Size(353, 25)
        Me.Cmb_Routing.TabIndex = 9
        '
        'Cmb_Lokasi
        '
        Me.Cmb_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lokasi.Enabled = False
        Me.Cmb_Lokasi.FormattingEnabled = True
        Me.Cmb_Lokasi.Items.AddRange(New Object() {"Ali"})
        Me.Cmb_Lokasi.Location = New System.Drawing.Point(218, 100)
        Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
        Me.Cmb_Lokasi.Size = New System.Drawing.Size(353, 24)
        Me.Cmb_Lokasi.TabIndex = 2
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = "HH:mm:ss"
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(218, 156)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(170, 20)
        Me.DateTimePicker2.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(39, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 20)
        Me.Label3.TabIndex = 423
        Me.Label3.Text = "Lokasi"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(22, 516)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1072, 12)
        Me.Panel6.TabIndex = 424
        Me.Panel6.Visible = False
        '
        'Txt_NoFaktur
        '
        Me.Txt_NoFaktur.BackColor = System.Drawing.Color.Goldenrod
        Me.Txt_NoFaktur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NoFaktur.Enabled = False
        Me.Txt_NoFaktur.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_NoFaktur.Location = New System.Drawing.Point(696, 73)
        Me.Txt_NoFaktur.MaxLength = 50
        Me.Txt_NoFaktur.Name = "Txt_NoFaktur"
        Me.Txt_NoFaktur.Size = New System.Drawing.Size(214, 22)
        Me.Txt_NoFaktur.TabIndex = 425
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label12.Location = New System.Drawing.Point(609, 73)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(81, 20)
        Me.Label12.TabIndex = 426
        Me.Label12.Text = "No. Faktur"
        '
        'TxtQtyPO
        '
        Me.TxtQtyPO.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtQtyPO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtQtyPO.Enabled = False
        Me.TxtQtyPO.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtQtyPO.Location = New System.Drawing.Point(408, 235)
        Me.TxtQtyPO.MaxLength = 50
        Me.TxtQtyPO.Name = "TxtQtyPO"
        Me.TxtQtyPO.Size = New System.Drawing.Size(107, 22)
        Me.TxtQtyPO.TabIndex = 427
        Me.TxtQtyPO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtQtyPO.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label13.Location = New System.Drawing.Point(39, 234)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(56, 20)
        Me.Label13.TabIndex = 428
        Me.Label13.Text = "Qty PO"
        '
        'TxtQtyProduksi
        '
        Me.TxtQtyProduksi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtQtyProduksi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtQtyProduksi.Enabled = False
        Me.TxtQtyProduksi.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtQtyProduksi.Location = New System.Drawing.Point(408, 263)
        Me.TxtQtyProduksi.MaxLength = 50
        Me.TxtQtyProduksi.Name = "TxtQtyProduksi"
        Me.TxtQtyProduksi.Size = New System.Drawing.Size(107, 22)
        Me.TxtQtyProduksi.TabIndex = 429
        Me.TxtQtyProduksi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtQtyProduksi.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(38, 264)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 20)
        Me.Label2.TabIndex = 430
        Me.Label2.Text = "Qty Produksi"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label9.Location = New System.Drawing.Point(39, 210)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(100, 20)
        Me.Label9.TabIndex = 431
        Me.Label9.Text = "Nama Barang"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Enabled = False
        Me.TextBox1.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TextBox1.Location = New System.Drawing.Point(218, 209)
        Me.TextBox1.MaxLength = 50
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(353, 22)
        Me.TextBox1.TabIndex = 432
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label14.Location = New System.Drawing.Point(38, 156)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(103, 20)
        Me.Label14.TabIndex = 433
        Me.Label14.Text = "Jam Produksi"
        '
        'Cmb_Satuan
        '
        Me.Cmb_Satuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Cmb_Satuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Satuan.Enabled = False
        Me.Cmb_Satuan.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Cmb_Satuan.FormattingEnabled = True
        Me.Cmb_Satuan.Items.AddRange(New Object() {"ROUTING 1 (Mixer, Hammer)", "ROUTING 2 (Mixer, Pellet)"})
        Me.Cmb_Satuan.Location = New System.Drawing.Point(479, 290)
        Me.Cmb_Satuan.Name = "Cmb_Satuan"
        Me.Cmb_Satuan.Size = New System.Drawing.Size(92, 25)
        Me.Cmb_Satuan.TabIndex = 434
        '
        'Txt_DisplayQtyProd
        '
        Me.Txt_DisplayQtyProd.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_DisplayQtyProd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_DisplayQtyProd.Enabled = False
        Me.Txt_DisplayQtyProd.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_DisplayQtyProd.Location = New System.Drawing.Point(217, 263)
        Me.Txt_DisplayQtyProd.MaxLength = 50
        Me.Txt_DisplayQtyProd.Name = "Txt_DisplayQtyProd"
        Me.Txt_DisplayQtyProd.Size = New System.Drawing.Size(171, 22)
        Me.Txt_DisplayQtyProd.TabIndex = 436
        Me.Txt_DisplayQtyProd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Txt_DisplayQtyPO
        '
        Me.Txt_DisplayQtyPO.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_DisplayQtyPO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_DisplayQtyPO.Enabled = False
        Me.Txt_DisplayQtyPO.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_DisplayQtyPO.Location = New System.Drawing.Point(217, 235)
        Me.Txt_DisplayQtyPO.MaxLength = 50
        Me.Txt_DisplayQtyPO.Name = "Txt_DisplayQtyPO"
        Me.Txt_DisplayQtyPO.Size = New System.Drawing.Size(171, 22)
        Me.Txt_DisplayQtyPO.TabIndex = 435
        Me.Txt_DisplayQtyPO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtQtyPO_Satuan
        '
        Me.TxtQtyPO_Satuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtQtyPO_Satuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtQtyPO_Satuan.Enabled = False
        Me.TxtQtyPO_Satuan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtQtyPO_Satuan.Location = New System.Drawing.Point(516, 235)
        Me.TxtQtyPO_Satuan.MaxLength = 50
        Me.TxtQtyPO_Satuan.Name = "TxtQtyPO_Satuan"
        Me.TxtQtyPO_Satuan.Size = New System.Drawing.Size(55, 22)
        Me.TxtQtyPO_Satuan.TabIndex = 437
        Me.TxtQtyPO_Satuan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtQtyPO_Satuan.Visible = False
        '
        'TxtQtyProduksi_Satuan
        '
        Me.TxtQtyProduksi_Satuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtQtyProduksi_Satuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtQtyProduksi_Satuan.Enabled = False
        Me.TxtQtyProduksi_Satuan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtQtyProduksi_Satuan.Location = New System.Drawing.Point(516, 263)
        Me.TxtQtyProduksi_Satuan.MaxLength = 50
        Me.TxtQtyProduksi_Satuan.Name = "TxtQtyProduksi_Satuan"
        Me.TxtQtyProduksi_Satuan.Size = New System.Drawing.Size(55, 22)
        Me.TxtQtyProduksi_Satuan.TabIndex = 438
        Me.TxtQtyProduksi_Satuan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtQtyProduksi_Satuan.Visible = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(131, 477)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Refresh.TabIndex = 439
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label15.Location = New System.Drawing.Point(37, 320)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(105, 20)
        Me.Label15.TabIndex = 395
        Me.Label15.Text = "Jumlah Batch"
        '
        'Txt_JumlahBatch
        '
        Me.Txt_JumlahBatch.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_JumlahBatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_JumlahBatch.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_JumlahBatch.Location = New System.Drawing.Point(216, 320)
        Me.Txt_JumlahBatch.MaxLength = 50
        Me.Txt_JumlahBatch.Name = "Txt_JumlahBatch"
        Me.Txt_JumlahBatch.Size = New System.Drawing.Size(256, 22)
        Me.Txt_JumlahBatch.TabIndex = 8
        Me.Txt_JumlahBatch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label16.Location = New System.Drawing.Point(37, 348)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(77, 20)
        Me.Label16.TabIndex = 395
        Me.Label16.Text = "Qty Batch"
        '
        'Txt_QtyBatch
        '
        Me.Txt_QtyBatch.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_QtyBatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_QtyBatch.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_QtyBatch.Location = New System.Drawing.Point(216, 348)
        Me.Txt_QtyBatch.MaxLength = 50
        Me.Txt_QtyBatch.Name = "Txt_QtyBatch"
        Me.Txt_QtyBatch.Size = New System.Drawing.Size(256, 22)
        Me.Txt_QtyBatch.TabIndex = 8
        Me.Txt_QtyBatch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Cmb_SatuanBatch
        '
        Me.Cmb_SatuanBatch.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Cmb_SatuanBatch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_SatuanBatch.Enabled = False
        Me.Cmb_SatuanBatch.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Cmb_SatuanBatch.FormattingEnabled = True
        Me.Cmb_SatuanBatch.Items.AddRange(New Object() {"ROUTING 1 (Mixer, Hammer)", "ROUTING 2 (Mixer, Pellet)"})
        Me.Cmb_SatuanBatch.Location = New System.Drawing.Point(479, 346)
        Me.Cmb_SatuanBatch.Name = "Cmb_SatuanBatch"
        Me.Cmb_SatuanBatch.Size = New System.Drawing.Size(92, 25)
        Me.Cmb_SatuanBatch.TabIndex = 434
        '
        'EMI_Produksi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(607, 529)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.TxtQtyProduksi_Satuan)
        Me.Controls.Add(Me.TxtQtyPO_Satuan)
        Me.Controls.Add(Me.Txt_DisplayQtyProd)
        Me.Controls.Add(Me.Txt_DisplayQtyPO)
        Me.Controls.Add(Me.Cmb_SatuanBatch)
        Me.Controls.Add(Me.Cmb_Satuan)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TxtQtyProduksi)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtQtyPO)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Txt_NoFaktur)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.Cmb_Lokasi)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Cmb_Routing)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Txt_QtyBatch)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Txt_JumlahBatch)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Txt_Qty)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Txt_KdBarang)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Txt_BatchNo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Cmb_Operator)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Txt_NoTransaksi)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Produksi"
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
    Friend WithEvents Label6 As Label
    Friend WithEvents Txt_NoTransaksi As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Cmb_Operator As ComboBox
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Txt_BatchNo As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Txt_Qty As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Cmb_Routing As ComboBox
    Friend WithEvents Cmb_Lokasi As ComboBox
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Txt_NoFaktur As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents TxtQtyPO As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents TxtQtyProduksi As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Cmb_Satuan As ComboBox
    Friend WithEvents Txt_DisplayQtyProd As TextBox
    Friend WithEvents Txt_DisplayQtyPO As TextBox
    Friend WithEvents TxtQtyPO_Satuan As TextBox
    Friend WithEvents TxtQtyProduksi_Satuan As TextBox
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Label15 As Label
    Friend WithEvents Txt_JumlahBatch As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Txt_QtyBatch As TextBox
    Friend WithEvents Cmb_SatuanBatch As ComboBox
End Class
