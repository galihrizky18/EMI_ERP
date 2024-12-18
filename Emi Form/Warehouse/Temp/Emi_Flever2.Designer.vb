<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Emi_Flever2
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
        Dim DataGridViewCellStyle41 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle42 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle49 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle50 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle43 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle44 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle45 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle46 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle47 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle48 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Dgv_BarangSn = New System.Windows.Forms.DataGridView()
        Me.kode_barang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.idWarehouse = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.barang_sn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nama = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.position = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.good_stock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.satuan_display = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.checkbox = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.jumlah_flever = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Txt_Lock_RV = New System.Windows.Forms.TextBox()
        Me.Txt_Earned = New System.Windows.Forms.TextBox()
        Me.Txt_Required = New System.Windows.Forms.TextBox()
        Me.Txt_BarangAkhir = New System.Windows.Forms.TextBox()
        Me.Txt_BarangAwal = New System.Windows.Forms.TextBox()
        Me.Txt_SoAkhir = New System.Windows.Forms.TextBox()
        Me.Txt_SoAwal = New System.Windows.Forms.TextBox()
        Me.Txt_NamaBarang = New System.Windows.Forms.TextBox()
        Me.txt_Keterangan = New System.Windows.Forms.TextBox()
        Me.TxtKd_Barang = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.TxtFaktur = New System.Windows.Forms.TextBox()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Lv_Cari = New System.Windows.Forms.ListView()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Lbl_Total = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.Dgv_BarangSn, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(1037, 51)
        Me.Panel1.TabIndex = 318
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1037, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(202, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Flever - Per Pallet"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(20, 569)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1334, 12)
        Me.Panel2.TabIndex = 319
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 51)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 489)
        Me.Panel3.TabIndex = 320
        Me.Panel3.Visible = False
        '
        'Dgv_BarangSn
        '
        Me.Dgv_BarangSn.AllowUserToAddRows = False
        Me.Dgv_BarangSn.AllowUserToDeleteRows = False
        Me.Dgv_BarangSn.AllowUserToResizeColumns = False
        Me.Dgv_BarangSn.AllowUserToResizeRows = False
        DataGridViewCellStyle41.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_BarangSn.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle41
        Me.Dgv_BarangSn.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.Dgv_BarangSn.BackgroundColor = System.Drawing.Color.White
        Me.Dgv_BarangSn.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle42.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle42.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle42.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle42.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle42.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle42.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_BarangSn.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle42
        Me.Dgv_BarangSn.ColumnHeadersHeight = 30
        Me.Dgv_BarangSn.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.kode_barang, Me.idWarehouse, Me.barang_sn, Me.nama, Me.position, Me.good_stock, Me.satuan_display, Me.checkbox, Me.jumlah_flever})
        DataGridViewCellStyle49.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle49.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle49.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle49.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle49.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle49.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle49.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_BarangSn.DefaultCellStyle = DataGridViewCellStyle49
        Me.Dgv_BarangSn.Location = New System.Drawing.Point(23, 253)
        Me.Dgv_BarangSn.MultiSelect = False
        Me.Dgv_BarangSn.Name = "Dgv_BarangSn"
        Me.Dgv_BarangSn.RowHeadersWidth = 21
        DataGridViewCellStyle50.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_BarangSn.RowsDefaultCellStyle = DataGridViewCellStyle50
        Me.Dgv_BarangSn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.Dgv_BarangSn.Size = New System.Drawing.Size(991, 309)
        Me.Dgv_BarangSn.TabIndex = 463
        '
        'kode_barang
        '
        Me.kode_barang.HeaderText = "Kode Barang"
        Me.kode_barang.Name = "kode_barang"
        Me.kode_barang.Visible = False
        '
        'idWarehouse
        '
        Me.idWarehouse.HeaderText = "Id WareHouse"
        Me.idWarehouse.Name = "idWarehouse"
        Me.idWarehouse.Visible = False
        '
        'barang_sn
        '
        DataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.barang_sn.DefaultCellStyle = DataGridViewCellStyle43
        Me.barang_sn.HeaderText = "Serial Number"
        Me.barang_sn.Name = "barang_sn"
        Me.barang_sn.ReadOnly = True
        Me.barang_sn.Width = 230
        '
        'nama
        '
        DataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.nama.DefaultCellStyle = DataGridViewCellStyle44
        Me.nama.HeaderText = "Nama Barang"
        Me.nama.Name = "nama"
        Me.nama.ReadOnly = True
        Me.nama.Width = 250
        '
        'position
        '
        DataGridViewCellStyle45.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.position.DefaultCellStyle = DataGridViewCellStyle45
        Me.position.HeaderText = "Posisi"
        Me.position.Name = "position"
        Me.position.ReadOnly = True
        Me.position.Width = 150
        '
        'good_stock
        '
        DataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.good_stock.DefaultCellStyle = DataGridViewCellStyle46
        Me.good_stock.HeaderText = "Good Stock"
        Me.good_stock.Name = "good_stock"
        Me.good_stock.ReadOnly = True
        Me.good_stock.Width = 105
        '
        'satuan_display
        '
        DataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.satuan_display.DefaultCellStyle = DataGridViewCellStyle47
        Me.satuan_display.HeaderText = "Satuan"
        Me.satuan_display.Name = "satuan_display"
        Me.satuan_display.ReadOnly = True
        Me.satuan_display.Width = 70
        '
        'checkbox
        '
        Me.checkbox.HeaderText = ""
        Me.checkbox.Name = "checkbox"
        Me.checkbox.Width = 40
        '
        'jumlah_flever
        '
        DataGridViewCellStyle48.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.jumlah_flever.DefaultCellStyle = DataGridViewCellStyle48
        Me.jumlah_flever.HeaderText = "Jumlah Flever"
        Me.jumlah_flever.Name = "jumlah_flever"
        Me.jumlah_flever.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.jumlah_flever.Width = 120
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Txt_Lock_RV)
        Me.GroupBox1.Controls.Add(Me.Txt_Earned)
        Me.GroupBox1.Controls.Add(Me.Txt_Required)
        Me.GroupBox1.Controls.Add(Me.Txt_BarangAkhir)
        Me.GroupBox1.Controls.Add(Me.Txt_BarangAwal)
        Me.GroupBox1.Controls.Add(Me.Txt_SoAkhir)
        Me.GroupBox1.Controls.Add(Me.Txt_SoAwal)
        Me.GroupBox1.Controls.Add(Me.Txt_NamaBarang)
        Me.GroupBox1.Controls.Add(Me.txt_Keterangan)
        Me.GroupBox1.Controls.Add(Me.TxtKd_Barang)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Cmb_Lokasi)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker2)
        Me.GroupBox1.Controls.Add(Me.TxtFaktur)
        Me.GroupBox1.Location = New System.Drawing.Point(23, 63)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(991, 184)
        Me.GroupBox1.TabIndex = 464
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detail"
        '
        'Txt_Lock_RV
        '
        Me.Txt_Lock_RV.Location = New System.Drawing.Point(601, 49)
        Me.Txt_Lock_RV.Name = "Txt_Lock_RV"
        Me.Txt_Lock_RV.Size = New System.Drawing.Size(136, 23)
        Me.Txt_Lock_RV.TabIndex = 458
        Me.Txt_Lock_RV.Visible = False
        '
        'Txt_Earned
        '
        Me.Txt_Earned.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Earned.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Earned.Enabled = False
        Me.Txt_Earned.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_Earned.Location = New System.Drawing.Point(879, 150)
        Me.Txt_Earned.MaxLength = 50
        Me.Txt_Earned.Name = "Txt_Earned"
        Me.Txt_Earned.Size = New System.Drawing.Size(91, 21)
        Me.Txt_Earned.TabIndex = 452
        Me.Txt_Earned.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Txt_Required
        '
        Me.Txt_Required.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Required.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Required.Enabled = False
        Me.Txt_Required.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_Required.Location = New System.Drawing.Point(782, 150)
        Me.Txt_Required.MaxLength = 50
        Me.Txt_Required.Name = "Txt_Required"
        Me.Txt_Required.Size = New System.Drawing.Size(91, 21)
        Me.Txt_Required.TabIndex = 452
        Me.Txt_Required.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Txt_BarangAkhir
        '
        Me.Txt_BarangAkhir.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_BarangAkhir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_BarangAkhir.Enabled = False
        Me.Txt_BarangAkhir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_BarangAkhir.Location = New System.Drawing.Point(590, 150)
        Me.Txt_BarangAkhir.MaxLength = 50
        Me.Txt_BarangAkhir.Name = "Txt_BarangAkhir"
        Me.Txt_BarangAkhir.Size = New System.Drawing.Size(186, 21)
        Me.Txt_BarangAkhir.TabIndex = 452
        Me.Txt_BarangAkhir.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Txt_BarangAwal
        '
        Me.Txt_BarangAwal.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_BarangAwal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_BarangAwal.Enabled = False
        Me.Txt_BarangAwal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_BarangAwal.Location = New System.Drawing.Point(206, 150)
        Me.Txt_BarangAwal.MaxLength = 50
        Me.Txt_BarangAwal.Name = "Txt_BarangAwal"
        Me.Txt_BarangAwal.Size = New System.Drawing.Size(186, 21)
        Me.Txt_BarangAwal.TabIndex = 453
        Me.Txt_BarangAwal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Txt_SoAkhir
        '
        Me.Txt_SoAkhir.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_SoAkhir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_SoAkhir.Enabled = False
        Me.Txt_SoAkhir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_SoAkhir.Location = New System.Drawing.Point(398, 150)
        Me.Txt_SoAkhir.MaxLength = 50
        Me.Txt_SoAkhir.Name = "Txt_SoAkhir"
        Me.Txt_SoAkhir.Size = New System.Drawing.Size(186, 21)
        Me.Txt_SoAkhir.TabIndex = 454
        Me.Txt_SoAkhir.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Txt_SoAwal
        '
        Me.Txt_SoAwal.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_SoAwal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_SoAwal.Enabled = False
        Me.Txt_SoAwal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_SoAwal.Location = New System.Drawing.Point(14, 150)
        Me.Txt_SoAwal.MaxLength = 50
        Me.Txt_SoAwal.Name = "Txt_SoAwal"
        Me.Txt_SoAwal.Size = New System.Drawing.Size(186, 21)
        Me.Txt_SoAwal.TabIndex = 455
        Me.Txt_SoAwal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Txt_NamaBarang
        '
        Me.Txt_NamaBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NamaBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NamaBarang.Enabled = False
        Me.Txt_NamaBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_NamaBarang.Location = New System.Drawing.Point(206, 94)
        Me.Txt_NamaBarang.MaxLength = 50
        Me.Txt_NamaBarang.Name = "Txt_NamaBarang"
        Me.Txt_NamaBarang.Size = New System.Drawing.Size(186, 22)
        Me.Txt_NamaBarang.TabIndex = 456
        '
        'txt_Keterangan
        '
        Me.txt_Keterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txt_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Keterangan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Keterangan.Location = New System.Drawing.Point(102, 67)
        Me.txt_Keterangan.MaxLength = 50
        Me.txt_Keterangan.Name = "txt_Keterangan"
        Me.txt_Keterangan.Size = New System.Drawing.Size(290, 22)
        Me.txt_Keterangan.TabIndex = 457
        '
        'TxtKd_Barang
        '
        Me.TxtKd_Barang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKd_Barang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKd_Barang.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtKd_Barang.Location = New System.Drawing.Point(101, 95)
        Me.TxtKd_Barang.MaxLength = 50
        Me.TxtKd_Barang.Name = "TxtKd_Barang"
        Me.TxtKd_Barang.Size = New System.Drawing.Size(99, 22)
        Me.TxtKd_Barang.TabIndex = 457
        '
        'Label9
        '
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Location = New System.Drawing.Point(879, 126)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(91, 21)
        Me.Label9.TabIndex = 448
        Me.Label9.Text = "Earned"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Location = New System.Drawing.Point(782, 126)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(91, 21)
        Me.Label8.TabIndex = 448
        Me.Label8.Text = "Required"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Location = New System.Drawing.Point(590, 124)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(186, 21)
        Me.Label7.TabIndex = 448
        Me.Label7.Text = "Barang Akhir"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Location = New System.Drawing.Point(206, 124)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(186, 21)
        Me.Label5.TabIndex = 449
        Me.Label5.Text = "Barang Awal"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Location = New System.Drawing.Point(398, 124)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(186, 21)
        Me.Label4.TabIndex = 450
        Me.Label4.Text = "Gudang Akhir"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label10.Location = New System.Drawing.Point(10, 69)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(86, 20)
        Me.Label10.TabIndex = 446
        Me.Label10.Text = "Keterangan"
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Location = New System.Drawing.Point(14, 124)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(186, 21)
        Me.Label2.TabIndex = 451
        Me.Label2.Text = "Gudang Awal"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(9, 97)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 20)
        Me.Label6.TabIndex = 446
        Me.Label6.Text = "Cari "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(10, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 20)
        Me.Label3.TabIndex = 447
        Me.Label3.Text = "Lokasi"
        '
        'Cmb_Lokasi
        '
        Me.Cmb_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lokasi.Enabled = False
        Me.Cmb_Lokasi.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_Lokasi.FormattingEnabled = True
        Me.Cmb_Lokasi.Location = New System.Drawing.Point(101, 25)
        Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
        Me.Cmb_Lokasi.Size = New System.Drawing.Size(167, 24)
        Me.Cmb_Lokasi.TabIndex = 445
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CalendarFont = New System.Drawing.Font("Work Sans", 8.0!)
        Me.DateTimePicker2.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker2.Enabled = False
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(601, 21)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(2)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(168, 23)
        Me.DateTimePicker2.TabIndex = 444
        Me.DateTimePicker2.TabStop = False
        Me.DateTimePicker2.Visible = False
        '
        'TxtFaktur
        '
        Me.TxtFaktur.BackColor = System.Drawing.Color.Goldenrod
        Me.TxtFaktur.Enabled = False
        Me.TxtFaktur.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold)
        Me.TxtFaktur.Location = New System.Drawing.Point(369, 19)
        Me.TxtFaktur.MaxLength = 30
        Me.TxtFaktur.Name = "TxtFaktur"
        Me.TxtFaktur.Size = New System.Drawing.Size(227, 22)
        Me.TxtFaktur.TabIndex = 443
        Me.TxtFaktur.Visible = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(140, 583)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(118, 36)
        Me.Btn_Refresh.TabIndex = 466
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(20, 583)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(118, 36)
        Me.Btn_Simpan.TabIndex = 465
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(20, 620)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1334, 12)
        Me.Panel4.TabIndex = 319
        Me.Panel4.Visible = False
        '
        'Lv_Cari
        '
        Me.Lv_Cari.FullRowSelect = True
        Me.Lv_Cari.GridLines = True
        Me.Lv_Cari.HideSelection = False
        Me.Lv_Cari.Location = New System.Drawing.Point(1024, 157)
        Me.Lv_Cari.Name = "Lv_Cari"
        Me.Lv_Cari.Size = New System.Drawing.Size(689, 220)
        Me.Lv_Cari.TabIndex = 467
        Me.Lv_Cari.UseCompatibleStateImageBehavior = False
        Me.Lv_Cari.View = System.Windows.Forms.View.Details
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1016, 63)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 489)
        Me.Panel5.TabIndex = 320
        Me.Panel5.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(21, 52)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1334, 12)
        Me.Panel6.TabIndex = 319
        Me.Panel6.Visible = False
        '
        'Lbl_Total
        '
        Me.Lbl_Total.Font = New System.Drawing.Font("Work Sans", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Total.ForeColor = System.Drawing.Color.Red
        Me.Lbl_Total.Location = New System.Drawing.Point(924, 588)
        Me.Lbl_Total.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Lbl_Total.Name = "Lbl_Total"
        Me.Lbl_Total.Size = New System.Drawing.Size(79, 20)
        Me.Lbl_Total.TabIndex = 469
        Me.Lbl_Total.Text = "0"
        Me.Lbl_Total.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Work Sans", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.Red
        Me.Label35.Location = New System.Drawing.Point(840, 588)
        Me.Label35.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(83, 23)
        Me.Label35.TabIndex = 468
        Me.Label35.Text = "Total Qty"
        '
        'Emi_Flever2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1037, 634)
        Me.Controls.Add(Me.Lbl_Total)
        Me.Controls.Add(Me.Label35)
        Me.Controls.Add(Me.Lv_Cari)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Dgv_BarangSn)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Emi_Flever2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.Dgv_BarangSn, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Dgv_BarangSn As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Txt_BarangAkhir As TextBox
    Friend WithEvents Txt_BarangAwal As TextBox
    Friend WithEvents Txt_SoAkhir As TextBox
    Friend WithEvents Txt_SoAwal As TextBox
    Friend WithEvents Txt_NamaBarang As TextBox
    Friend WithEvents TxtKd_Barang As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Cmb_Lokasi As ComboBox
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents TxtFaktur As TextBox
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Lv_Cari As ListView
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Txt_Earned As TextBox
    Friend WithEvents Txt_Required As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Txt_Lock_RV As TextBox
    Friend WithEvents kode_barang As DataGridViewTextBoxColumn
    Friend WithEvents idWarehouse As DataGridViewTextBoxColumn
    Friend WithEvents barang_sn As DataGridViewTextBoxColumn
    Friend WithEvents nama As DataGridViewTextBoxColumn
    Friend WithEvents position As DataGridViewTextBoxColumn
    Friend WithEvents good_stock As DataGridViewTextBoxColumn
    Friend WithEvents satuan_display As DataGridViewTextBoxColumn
    Friend WithEvents checkbox As DataGridViewCheckBoxColumn
    Friend WithEvents jumlah_flever As DataGridViewTextBoxColumn
    Friend WithEvents txt_Keterangan As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Lbl_Total As Label
    Friend WithEvents Label35 As Label
End Class
