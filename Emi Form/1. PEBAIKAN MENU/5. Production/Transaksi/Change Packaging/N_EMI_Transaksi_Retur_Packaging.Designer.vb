<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class N_EMI_Transaksi_Retur_Packaging
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Lv_Data_Split = New System.Windows.Forms.ListView()
        Me.Lbl_Supplier = New System.Windows.Forms.Label()
        Me.Txt_NoSplit = New System.Windows.Forms.TextBox()
        Me.Txt_Kd_Barang = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txt_Jumlah_Produksi = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Cmb_Satuan_Produksi = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Cmb_Lokasi_Retur = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Cmb_Barang_Retur = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Cmb_Barang_Scrap = New System.Windows.Forms.ComboBox()
        Me.Txt_Jumlah_Retur = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Cmb_Satuan_Retur = New System.Windows.Forms.ComboBox()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Txt_Nm_Barang = New System.Windows.Forms.TextBox()
        Me.Txt_Jumlah_Tot_Retur = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Cmb_Satuan_Tot_Retur = New System.Windows.Forms.ComboBox()
        Me.Txt_Jumlah_Retur_Satuan_Scrap = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TxtJumlahPakai = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TxtBerat = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1184, 43)
        Me.Panel1.TabIndex = 24
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 41)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(1184, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(311, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Transaksi - Retur Packaging"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 43)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1221, 12)
        Me.Panel2.TabIndex = 36
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Controls.Add(Me.Panel6)
        Me.Panel3.Location = New System.Drawing.Point(0, 63)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 601)
        Me.Panel3.TabIndex = 37
        Me.Panel3.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(20, 485)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(942, 12)
        Me.Panel6.TabIndex = 35
        Me.Panel6.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Location = New System.Drawing.Point(1165, 63)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 601)
        Me.Panel4.TabIndex = 37
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(20, 485)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(942, 12)
        Me.Panel5.TabIndex = 35
        Me.Panel5.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(14, 596)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1221, 15)
        Me.Panel7.TabIndex = 36
        Me.Panel7.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Panel9)
        Me.GroupBox1.Controls.Add(Me.Lv_Data_Split)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GroupBox1.Location = New System.Drawing.Point(26, 56)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(602, 533)
        Me.GroupBox1.TabIndex = 38
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Split Production"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(541, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 15)
        Me.Label2.TabIndex = 349
        Me.Label2.Text = "Belum GI"
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.LightYellow
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Location = New System.Drawing.Point(520, 15)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(15, 15)
        Me.Panel9.TabIndex = 348
        '
        'Lv_Data_Split
        '
        Me.Lv_Data_Split.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Lv_Data_Split.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Lv_Data_Split.FullRowSelect = True
        Me.Lv_Data_Split.GridLines = True
        Me.Lv_Data_Split.HideSelection = False
        Me.Lv_Data_Split.Location = New System.Drawing.Point(6, 34)
        Me.Lv_Data_Split.Name = "Lv_Data_Split"
        Me.Lv_Data_Split.Size = New System.Drawing.Size(590, 493)
        Me.Lv_Data_Split.TabIndex = 0
        Me.Lv_Data_Split.UseCompatibleStateImageBehavior = False
        Me.Lv_Data_Split.View = System.Windows.Forms.View.Details
        '
        'Lbl_Supplier
        '
        Me.Lbl_Supplier.AutoSize = True
        Me.Lbl_Supplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Lbl_Supplier.Location = New System.Drawing.Point(633, 64)
        Me.Lbl_Supplier.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Lbl_Supplier.Name = "Lbl_Supplier"
        Me.Lbl_Supplier.Size = New System.Drawing.Size(50, 15)
        Me.Lbl_Supplier.TabIndex = 435
        Me.Lbl_Supplier.Text = "No Split"
        '
        'Txt_NoSplit
        '
        Me.Txt_NoSplit.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NoSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NoSplit.Enabled = False
        Me.Txt_NoSplit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Txt_NoSplit.Location = New System.Drawing.Point(771, 64)
        Me.Txt_NoSplit.Margin = New System.Windows.Forms.Padding(2)
        Me.Txt_NoSplit.MaxLength = 50
        Me.Txt_NoSplit.Name = "Txt_NoSplit"
        Me.Txt_NoSplit.Size = New System.Drawing.Size(182, 20)
        Me.Txt_NoSplit.TabIndex = 434
        '
        'Txt_Kd_Barang
        '
        Me.Txt_Kd_Barang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Kd_Barang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Kd_Barang.Enabled = False
        Me.Txt_Kd_Barang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Txt_Kd_Barang.Location = New System.Drawing.Point(771, 90)
        Me.Txt_Kd_Barang.Margin = New System.Windows.Forms.Padding(2)
        Me.Txt_Kd_Barang.MaxLength = 50
        Me.Txt_Kd_Barang.Name = "Txt_Kd_Barang"
        Me.Txt_Kd_Barang.Size = New System.Drawing.Size(130, 20)
        Me.Txt_Kd_Barang.TabIndex = 434
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(633, 90)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 15)
        Me.Label3.TabIndex = 435
        Me.Label3.Text = "Barang"
        '
        'Txt_Jumlah_Produksi
        '
        Me.Txt_Jumlah_Produksi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Jumlah_Produksi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Jumlah_Produksi.Enabled = False
        Me.Txt_Jumlah_Produksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Txt_Jumlah_Produksi.Location = New System.Drawing.Point(771, 114)
        Me.Txt_Jumlah_Produksi.Margin = New System.Windows.Forms.Padding(2)
        Me.Txt_Jumlah_Produksi.MaxLength = 50
        Me.Txt_Jumlah_Produksi.Name = "Txt_Jumlah_Produksi"
        Me.Txt_Jumlah_Produksi.Size = New System.Drawing.Size(185, 20)
        Me.Txt_Jumlah_Produksi.TabIndex = 434
        Me.Txt_Jumlah_Produksi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(633, 114)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 15)
        Me.Label4.TabIndex = 435
        Me.Label4.Text = "Jumlah Produksi"
        '
        'Cmb_Satuan_Produksi
        '
        Me.Cmb_Satuan_Produksi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Satuan_Produksi.Enabled = False
        Me.Cmb_Satuan_Produksi.FormattingEnabled = True
        Me.Cmb_Satuan_Produksi.Location = New System.Drawing.Point(959, 113)
        Me.Cmb_Satuan_Produksi.Name = "Cmb_Satuan_Produksi"
        Me.Cmb_Satuan_Produksi.Size = New System.Drawing.Size(99, 21)
        Me.Cmb_Satuan_Produksi.TabIndex = 436
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(633, 141)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(91, 15)
        Me.Label5.TabIndex = 435
        Me.Label5.Text = "Lokasi Transfer"
        '
        'Cmb_Lokasi_Retur
        '
        Me.Cmb_Lokasi_Retur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lokasi_Retur.Enabled = False
        Me.Cmb_Lokasi_Retur.FormattingEnabled = True
        Me.Cmb_Lokasi_Retur.Location = New System.Drawing.Point(771, 140)
        Me.Cmb_Lokasi_Retur.Name = "Cmb_Lokasi_Retur"
        Me.Cmb_Lokasi_Retur.Size = New System.Drawing.Size(195, 21)
        Me.Cmb_Lokasi_Retur.TabIndex = 436
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(633, 167)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(108, 15)
        Me.Label6.TabIndex = 435
        Me.Label6.Text = "Barang Packaging"
        '
        'Cmb_Barang_Retur
        '
        Me.Cmb_Barang_Retur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Barang_Retur.Enabled = False
        Me.Cmb_Barang_Retur.FormattingEnabled = True
        Me.Cmb_Barang_Retur.Location = New System.Drawing.Point(771, 167)
        Me.Cmb_Barang_Retur.Name = "Cmb_Barang_Retur"
        Me.Cmb_Barang_Retur.Size = New System.Drawing.Size(388, 21)
        Me.Cmb_Barang_Retur.TabIndex = 436
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(634, 242)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 15)
        Me.Label7.TabIndex = 435
        Me.Label7.Text = "Barang Scrap"
        '
        'Cmb_Barang_Scrap
        '
        Me.Cmb_Barang_Scrap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Barang_Scrap.Enabled = False
        Me.Cmb_Barang_Scrap.FormattingEnabled = True
        Me.Cmb_Barang_Scrap.Location = New System.Drawing.Point(771, 241)
        Me.Cmb_Barang_Scrap.Name = "Cmb_Barang_Scrap"
        Me.Cmb_Barang_Scrap.Size = New System.Drawing.Size(387, 21)
        Me.Cmb_Barang_Scrap.TabIndex = 436
        '
        'Txt_Jumlah_Retur
        '
        Me.Txt_Jumlah_Retur.BackColor = System.Drawing.Color.White
        Me.Txt_Jumlah_Retur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Jumlah_Retur.Enabled = False
        Me.Txt_Jumlah_Retur.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Txt_Jumlah_Retur.Location = New System.Drawing.Point(771, 267)
        Me.Txt_Jumlah_Retur.Margin = New System.Windows.Forms.Padding(2)
        Me.Txt_Jumlah_Retur.MaxLength = 50
        Me.Txt_Jumlah_Retur.Name = "Txt_Jumlah_Retur"
        Me.Txt_Jumlah_Retur.Size = New System.Drawing.Size(195, 20)
        Me.Txt_Jumlah_Retur.TabIndex = 434
        Me.Txt_Jumlah_Retur.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(635, 267)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(96, 15)
        Me.Label8.TabIndex = 435
        Me.Label8.Text = "Jumlah Transfer"
        '
        'Cmb_Satuan_Retur
        '
        Me.Cmb_Satuan_Retur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Satuan_Retur.Enabled = False
        Me.Cmb_Satuan_Retur.FormattingEnabled = True
        Me.Cmb_Satuan_Retur.Location = New System.Drawing.Point(971, 266)
        Me.Cmb_Satuan_Retur.Name = "Cmb_Satuan_Retur"
        Me.Cmb_Satuan_Retur.Size = New System.Drawing.Size(102, 21)
        Me.Cmb_Satuan_Retur.TabIndex = 436
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(637, 331)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(133, 35)
        Me.Btn_Simpan.TabIndex = 437
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(776, 331)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(133, 35)
        Me.Btn_Refresh.TabIndex = 437
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Txt_Nm_Barang
        '
        Me.Txt_Nm_Barang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Nm_Barang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Nm_Barang.Enabled = False
        Me.Txt_Nm_Barang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Txt_Nm_Barang.Location = New System.Drawing.Point(903, 90)
        Me.Txt_Nm_Barang.Margin = New System.Windows.Forms.Padding(2)
        Me.Txt_Nm_Barang.MaxLength = 50
        Me.Txt_Nm_Barang.Name = "Txt_Nm_Barang"
        Me.Txt_Nm_Barang.Size = New System.Drawing.Size(256, 20)
        Me.Txt_Nm_Barang.TabIndex = 434
        '
        'Txt_Jumlah_Tot_Retur
        '
        Me.Txt_Jumlah_Tot_Retur.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Jumlah_Tot_Retur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Jumlah_Tot_Retur.Enabled = False
        Me.Txt_Jumlah_Tot_Retur.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Txt_Jumlah_Tot_Retur.Location = New System.Drawing.Point(990, 216)
        Me.Txt_Jumlah_Tot_Retur.Margin = New System.Windows.Forms.Padding(2)
        Me.Txt_Jumlah_Tot_Retur.MaxLength = 50
        Me.Txt_Jumlah_Tot_Retur.Name = "Txt_Jumlah_Tot_Retur"
        Me.Txt_Jumlah_Tot_Retur.Size = New System.Drawing.Size(109, 20)
        Me.Txt_Jumlah_Tot_Retur.TabIndex = 434
        Me.Txt_Jumlah_Tot_Retur.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(890, 216)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(96, 15)
        Me.Label9.TabIndex = 435
        Me.Label9.Text = "Jumlah Transfer"
        '
        'Cmb_Satuan_Tot_Retur
        '
        Me.Cmb_Satuan_Tot_Retur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Satuan_Tot_Retur.Enabled = False
        Me.Cmb_Satuan_Tot_Retur.FormattingEnabled = True
        Me.Cmb_Satuan_Tot_Retur.Location = New System.Drawing.Point(1104, 216)
        Me.Cmb_Satuan_Tot_Retur.Name = "Cmb_Satuan_Tot_Retur"
        Me.Cmb_Satuan_Tot_Retur.Size = New System.Drawing.Size(54, 21)
        Me.Cmb_Satuan_Tot_Retur.TabIndex = 436
        '
        'Txt_Jumlah_Retur_Satuan_Scrap
        '
        Me.Txt_Jumlah_Retur_Satuan_Scrap.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Jumlah_Retur_Satuan_Scrap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Jumlah_Retur_Satuan_Scrap.Enabled = False
        Me.Txt_Jumlah_Retur_Satuan_Scrap.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Txt_Jumlah_Retur_Satuan_Scrap.Location = New System.Drawing.Point(771, 291)
        Me.Txt_Jumlah_Retur_Satuan_Scrap.Margin = New System.Windows.Forms.Padding(2)
        Me.Txt_Jumlah_Retur_Satuan_Scrap.MaxLength = 50
        Me.Txt_Jumlah_Retur_Satuan_Scrap.Name = "Txt_Jumlah_Retur_Satuan_Scrap"
        Me.Txt_Jumlah_Retur_Satuan_Scrap.Size = New System.Drawing.Size(195, 20)
        Me.Txt_Jumlah_Retur_Satuan_Scrap.TabIndex = 434
        Me.Txt_Jumlah_Retur_Satuan_Scrap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(634, 216)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(114, 15)
        Me.Label10.TabIndex = 438
        Me.Label10.Text = "Jumlah Pemakaian"
        '
        'TxtJumlahPakai
        '
        Me.TxtJumlahPakai.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtJumlahPakai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtJumlahPakai.Enabled = False
        Me.TxtJumlahPakai.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.TxtJumlahPakai.Location = New System.Drawing.Point(771, 216)
        Me.TxtJumlahPakai.Margin = New System.Windows.Forms.Padding(2)
        Me.TxtJumlahPakai.MaxLength = 50
        Me.TxtJumlahPakai.Name = "TxtJumlahPakai"
        Me.TxtJumlahPakai.Size = New System.Drawing.Size(115, 20)
        Me.TxtJumlahPakai.TabIndex = 439
        Me.TxtJumlahPakai.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(635, 291)
        Me.Label11.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(78, 15)
        Me.Label11.TabIndex = 440
        Me.Label11.Text = "Jumlah Akhir"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(633, 192)
        Me.Label12.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(97, 15)
        Me.Label12.TabIndex = 441
        Me.Label12.Text = "Berat Packaging"
        '
        'TxtBerat
        '
        Me.TxtBerat.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtBerat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtBerat.Enabled = False
        Me.TxtBerat.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.TxtBerat.Location = New System.Drawing.Point(771, 192)
        Me.TxtBerat.Margin = New System.Windows.Forms.Padding(2)
        Me.TxtBerat.MaxLength = 50
        Me.TxtBerat.Name = "TxtBerat"
        Me.TxtBerat.Size = New System.Drawing.Size(115, 20)
        Me.TxtBerat.TabIndex = 442
        Me.TxtBerat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(890, 192)
        Me.Label13.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(24, 15)
        Me.Label13.TabIndex = 443
        Me.Label13.Text = "KG"
        '
        'N_EMI_Transaksi_Retur_Packaging
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1184, 611)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.TxtBerat)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TxtJumlahPakai)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Cmb_Barang_Scrap)
        Me.Controls.Add(Me.Cmb_Barang_Retur)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Cmb_Lokasi_Retur)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Cmb_Satuan_Retur)
        Me.Controls.Add(Me.Cmb_Satuan_Tot_Retur)
        Me.Controls.Add(Me.Cmb_Satuan_Produksi)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Lbl_Supplier)
        Me.Controls.Add(Me.Txt_Jumlah_Retur)
        Me.Controls.Add(Me.Txt_Jumlah_Retur_Satuan_Scrap)
        Me.Controls.Add(Me.Txt_Jumlah_Tot_Retur)
        Me.Controls.Add(Me.Txt_Jumlah_Produksi)
        Me.Controls.Add(Me.Txt_Nm_Barang)
        Me.Controls.Add(Me.Txt_Kd_Barang)
        Me.Controls.Add(Me.Txt_NoSplit)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_Transaksi_Retur_Packaging"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
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
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Lv_Data_Split As ListView
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Lbl_Supplier As Label
    Friend WithEvents Txt_NoSplit As TextBox
    Friend WithEvents Txt_Kd_Barang As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Txt_Jumlah_Produksi As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Cmb_Satuan_Produksi As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Cmb_Lokasi_Retur As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Cmb_Barang_Retur As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Cmb_Barang_Scrap As ComboBox
    Friend WithEvents Txt_Jumlah_Retur As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Cmb_Satuan_Retur As ComboBox
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Txt_Nm_Barang As TextBox
    Friend WithEvents Txt_Jumlah_Tot_Retur As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Cmb_Satuan_Tot_Retur As ComboBox
    Friend WithEvents Txt_Jumlah_Retur_Satuan_Scrap As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents TxtJumlahPakai As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents TxtBerat As TextBox
    Friend WithEvents Label13 As Label
End Class
