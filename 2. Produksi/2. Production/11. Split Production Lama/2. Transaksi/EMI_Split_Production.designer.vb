<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Split_Production

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
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.UbahAntrianToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.txtJenisProduk = New System.Windows.Forms.TextBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.txtJumlah = New System.Windows.Forms.TextBox()
        Me.lblTxtJumlah = New System.Windows.Forms.Label()
        Me.txtNmBarang = New System.Windows.Forms.TextBox()
        Me.lblTxtNmCust = New System.Windows.Forms.Label()
        Me.txtLokasi = New System.Windows.Forms.TextBox()
        Me.lblTxtLokasi = New System.Windows.Forms.Label()
        Me.txtKodeBarang = New System.Windows.Forms.TextBox()
        Me.lblTxtKdBrng = New System.Windows.Forms.Label()
        Me.txtRouting = New System.Windows.Forms.TextBox()
        Me.lblRouting = New System.Windows.Forms.Label()
        Me.txtSatuan = New System.Windows.Forms.TextBox()
        Me.lblTxtSatuan = New System.Windows.Forms.Label()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.lblLine = New System.Windows.Forms.Label()
        Me.cmbLine = New System.Windows.Forms.ComboBox()
        Me.lblJam = New System.Windows.Forms.Label()
        Me.lblTanggal = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.txtIdJenisProduk = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CmbLokasi = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtCatatan = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1353, 51)
        Me.Panel1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(370, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Transaksi - Split Production Order"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1209, 12)
        Me.Panel2.TabIndex = 34
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 54)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 692)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1331, 51)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 692)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(-2, 801)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1205, 12)
        Me.Panel4.TabIndex = 35
        Me.Panel4.Visible = False
        '
        'ListView1
        '
        Me.ListView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ListView1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(10, 19)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(1281, 228)
        Me.ListView1.TabIndex = 381
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UbahAntrianToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(145, 26)
        '
        'UbahAntrianToolStripMenuItem
        '
        Me.UbahAntrianToolStripMenuItem.Name = "UbahAntrianToolStripMenuItem"
        Me.UbahAntrianToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.UbahAntrianToolStripMenuItem.Text = "Ubah Antrian"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ListView1)
        Me.GroupBox1.Location = New System.Drawing.Point(27, 94)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1297, 256)
        Me.GroupBox1.TabIndex = 382
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Data Antrian"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ListView2)
        Me.GroupBox2.Location = New System.Drawing.Point(27, 399)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1297, 256)
        Me.GroupBox2.TabIndex = 383
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Data Produksi"
        '
        'ListView2
        '
        Me.ListView2.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.ListView2.FullRowSelect = True
        Me.ListView2.GridLines = True
        Me.ListView2.HideSelection = False
        Me.ListView2.Location = New System.Drawing.Point(10, 19)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(1281, 228)
        Me.ListView2.TabIndex = 381
        Me.ListView2.UseCompatibleStateImageBehavior = False
        Me.ListView2.View = System.Windows.Forms.View.Details
        '
        'txtJenisProduk
        '
        Me.txtJenisProduk.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtJenisProduk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJenisProduk.Enabled = False
        Me.txtJenisProduk.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.txtJenisProduk.Location = New System.Drawing.Point(1090, 699)
        Me.txtJenisProduk.Name = "txtJenisProduk"
        Me.txtJenisProduk.Size = New System.Drawing.Size(100, 22)
        Me.txtJenisProduk.TabIndex = 413
        Me.txtJenisProduk.Visible = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.btnClear.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Location = New System.Drawing.Point(1235, 349)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(91, 28)
        Me.btnClear.TabIndex = 392
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnOk
        '
        Me.btnOk.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.btnOk.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnOk.ForeColor = System.Drawing.Color.White
        Me.btnOk.Location = New System.Drawing.Point(1235, 376)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(91, 28)
        Me.btnOk.TabIndex = 393
        Me.btnOk.Text = "OK"
        Me.btnOk.UseVisualStyleBackColor = False
        '
        'txtJumlah
        '
        Me.txtJumlah.BackColor = System.Drawing.Color.White
        Me.txtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJumlah.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.txtJumlah.Location = New System.Drawing.Point(1028, 377)
        Me.txtJumlah.Name = "txtJumlah"
        Me.txtJumlah.Size = New System.Drawing.Size(121, 22)
        Me.txtJumlah.TabIndex = 391
        Me.txtJumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTxtJumlah
        '
        Me.lblTxtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTxtJumlah.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTxtJumlah.Location = New System.Drawing.Point(1028, 351)
        Me.lblTxtJumlah.Name = "lblTxtJumlah"
        Me.lblTxtJumlah.Size = New System.Drawing.Size(121, 25)
        Me.lblTxtJumlah.TabIndex = 390
        Me.lblTxtJumlah.Text = "Jumlah"
        Me.lblTxtJumlah.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtNmBarang
        '
        Me.txtNmBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtNmBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNmBarang.Enabled = False
        Me.txtNmBarang.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.txtNmBarang.Location = New System.Drawing.Point(327, 377)
        Me.txtNmBarang.Name = "txtNmBarang"
        Me.txtNmBarang.Size = New System.Drawing.Size(470, 22)
        Me.txtNmBarang.TabIndex = 387
        '
        'lblTxtNmCust
        '
        Me.lblTxtNmCust.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTxtNmCust.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTxtNmCust.Location = New System.Drawing.Point(327, 351)
        Me.lblTxtNmCust.Name = "lblTxtNmCust"
        Me.lblTxtNmCust.Size = New System.Drawing.Size(470, 25)
        Me.lblTxtNmCust.TabIndex = 386
        Me.lblTxtNmCust.Text = "Nama Barang"
        Me.lblTxtNmCust.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtLokasi
        '
        Me.txtLokasi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtLokasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLokasi.Enabled = False
        Me.txtLokasi.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.txtLokasi.Location = New System.Drawing.Point(917, 708)
        Me.txtLokasi.Name = "txtLokasi"
        Me.txtLokasi.Size = New System.Drawing.Size(150, 22)
        Me.txtLokasi.TabIndex = 395
        Me.txtLokasi.Visible = False
        '
        'lblTxtLokasi
        '
        Me.lblTxtLokasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTxtLokasi.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTxtLokasi.Location = New System.Drawing.Point(917, 682)
        Me.lblTxtLokasi.Name = "lblTxtLokasi"
        Me.lblTxtLokasi.Size = New System.Drawing.Size(150, 25)
        Me.lblTxtLokasi.TabIndex = 394
        Me.lblTxtLokasi.Text = "Lokasi"
        Me.lblTxtLokasi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTxtLokasi.Visible = False
        '
        'txtKodeBarang
        '
        Me.txtKodeBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtKodeBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKodeBarang.Enabled = False
        Me.txtKodeBarang.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.txtKodeBarang.Location = New System.Drawing.Point(158, 377)
        Me.txtKodeBarang.Name = "txtKodeBarang"
        Me.txtKodeBarang.Size = New System.Drawing.Size(168, 22)
        Me.txtKodeBarang.TabIndex = 401
        '
        'lblTxtKdBrng
        '
        Me.lblTxtKdBrng.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTxtKdBrng.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTxtKdBrng.Location = New System.Drawing.Point(158, 351)
        Me.lblTxtKdBrng.Name = "lblTxtKdBrng"
        Me.lblTxtKdBrng.Size = New System.Drawing.Size(168, 25)
        Me.lblTxtKdBrng.TabIndex = 400
        Me.lblTxtKdBrng.Text = "Kode Barang"
        Me.lblTxtKdBrng.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtRouting
        '
        Me.txtRouting.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtRouting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRouting.Enabled = False
        Me.txtRouting.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.txtRouting.Location = New System.Drawing.Point(798, 377)
        Me.txtRouting.Name = "txtRouting"
        Me.txtRouting.Size = New System.Drawing.Size(229, 22)
        Me.txtRouting.TabIndex = 399
        '
        'lblRouting
        '
        Me.lblRouting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRouting.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRouting.Location = New System.Drawing.Point(798, 351)
        Me.lblRouting.Name = "lblRouting"
        Me.lblRouting.Size = New System.Drawing.Size(229, 25)
        Me.lblRouting.TabIndex = 398
        Me.lblRouting.Text = "Routing"
        Me.lblRouting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSatuan
        '
        Me.txtSatuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtSatuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSatuan.Enabled = False
        Me.txtSatuan.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.txtSatuan.Location = New System.Drawing.Point(1150, 377)
        Me.txtSatuan.Name = "txtSatuan"
        Me.txtSatuan.Size = New System.Drawing.Size(80, 22)
        Me.txtSatuan.TabIndex = 403
        '
        'lblTxtSatuan
        '
        Me.lblTxtSatuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTxtSatuan.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTxtSatuan.Location = New System.Drawing.Point(1150, 351)
        Me.lblTxtSatuan.Name = "lblTxtSatuan"
        Me.lblTxtSatuan.Size = New System.Drawing.Size(80, 25)
        Me.lblTxtSatuan.TabIndex = 402
        Me.lblTxtSatuan.Text = "Satuan"
        Me.lblTxtSatuan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(29, 765)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Simpan.TabIndex = 362
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.btnRefresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(119, 765)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(84, 36)
        Me.btnRefresh.TabIndex = 380
        Me.btnRefresh.Text = "&Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'lblLine
        '
        Me.lblLine.AutoSize = True
        Me.lblLine.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.lblLine.Location = New System.Drawing.Point(984, 770)
        Me.lblLine.Name = "lblLine"
        Me.lblLine.Size = New System.Drawing.Size(38, 20)
        Me.lblLine.TabIndex = 409
        Me.lblLine.Text = "Line"
        Me.lblLine.Visible = False
        '
        'cmbLine
        '
        Me.cmbLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.cmbLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLine.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.cmbLine.FormattingEnabled = True
        Me.cmbLine.Location = New System.Drawing.Point(1028, 769)
        Me.cmbLine.Name = "cmbLine"
        Me.cmbLine.Size = New System.Drawing.Size(147, 25)
        Me.cmbLine.TabIndex = 407
        Me.cmbLine.Visible = False
        '
        'lblJam
        '
        Me.lblJam.AutoSize = True
        Me.lblJam.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.lblJam.Location = New System.Drawing.Point(813, 771)
        Me.lblJam.Name = "lblJam"
        Me.lblJam.Size = New System.Drawing.Size(38, 20)
        Me.lblJam.TabIndex = 406
        Me.lblJam.Text = "Jam"
        Me.lblJam.Visible = False
        '
        'lblTanggal
        '
        Me.lblTanggal.AutoSize = True
        Me.lblTanggal.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.lblTanggal.Location = New System.Drawing.Point(586, 770)
        Me.lblTanggal.Name = "lblTanggal"
        Me.lblTanggal.Size = New System.Drawing.Size(59, 20)
        Me.lblTanggal.TabIndex = 405
        Me.lblTanggal.Text = "Tanggal"
        Me.lblTanggal.Visible = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(661, 771)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(147, 20)
        Me.DateTimePicker1.TabIndex = 404
        Me.DateTimePicker1.Visible = False
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Checked = False
        Me.DateTimePicker2.CustomFormat = ""
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DateTimePicker2.Location = New System.Drawing.Point(856, 771)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DateTimePicker2.MinDate = New Date(2024, 4, 2, 0, 0, 0, 0)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(119, 20)
        Me.DateTimePicker2.TabIndex = 412
        Me.DateTimePicker2.Visible = False
        '
        'txtIdJenisProduk
        '
        Me.txtIdJenisProduk.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtIdJenisProduk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdJenisProduk.Enabled = False
        Me.txtIdJenisProduk.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.txtIdJenisProduk.Location = New System.Drawing.Point(1090, 667)
        Me.txtIdJenisProduk.Name = "txtIdJenisProduk"
        Me.txtIdJenisProduk.Size = New System.Drawing.Size(100, 22)
        Me.txtIdJenisProduk.TabIndex = 413
        Me.txtIdJenisProduk.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(39, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 20)
        Me.Label2.TabIndex = 416
        Me.Label2.Text = "Lokasi"
        '
        'CmbLokasi
        '
        Me.CmbLokasi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CmbLokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbLokasi.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.CmbLokasi.FormattingEnabled = True
        Me.CmbLokasi.Location = New System.Drawing.Point(98, 65)
        Me.CmbLokasi.Name = "CmbLokasi"
        Me.CmbLokasi.Size = New System.Drawing.Size(179, 25)
        Me.CmbLokasi.TabIndex = 415
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(25, 667)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 20)
        Me.Label3.TabIndex = 417
        Me.Label3.Text = "Catatan"
        '
        'TxtCatatan
        '
        Me.TxtCatatan.BackColor = System.Drawing.Color.White
        Me.TxtCatatan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtCatatan.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.TxtCatatan.Location = New System.Drawing.Point(98, 667)
        Me.TxtCatatan.Multiline = True
        Me.TxtCatatan.Name = "TxtCatatan"
        Me.TxtCatatan.Size = New System.Drawing.Size(400, 63)
        Me.TxtCatatan.TabIndex = 418
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Enabled = False
        Me.TextBox1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.TextBox1.Location = New System.Drawing.Point(27, 377)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(129, 22)
        Me.TextBox1.TabIndex = 420
        '
        'Label4
        '
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(27, 351)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(129, 25)
        Me.Label4.TabIndex = 419
        Me.Label4.Text = "No PR"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1353, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'EMI_Penentu_Production_Order
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1353, 813)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtCatatan)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CmbLokasi)
        Me.Controls.Add(Me.txtIdJenisProduk)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.lblLine)
        Me.Controls.Add(Me.cmbLine)
        Me.Controls.Add(Me.lblJam)
        Me.Controls.Add(Me.lblTanggal)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.txtSatuan)
        Me.Controls.Add(Me.lblTxtSatuan)
        Me.Controls.Add(Me.txtKodeBarang)
        Me.Controls.Add(Me.lblTxtKdBrng)
        Me.Controls.Add(Me.txtRouting)
        Me.Controls.Add(Me.lblRouting)
        Me.Controls.Add(Me.txtLokasi)
        Me.Controls.Add(Me.lblTxtLokasi)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.txtJumlah)
        Me.Controls.Add(Me.lblTxtJumlah)
        Me.Controls.Add(Me.txtNmBarang)
        Me.Controls.Add(Me.lblTxtNmCust)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.txtJenisProduk)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Penentu_Production_Order"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents ListView1 As ListView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents ListView2 As ListView
    Friend WithEvents btnClear As Button
    Friend WithEvents btnOk As Button
    Friend WithEvents txtJumlah As TextBox
    Friend WithEvents lblTxtJumlah As Label
    Friend WithEvents txtNmBarang As TextBox
    Friend WithEvents lblTxtNmCust As Label
    Friend WithEvents txtLokasi As TextBox
    Friend WithEvents lblTxtLokasi As Label
    Friend WithEvents txtKodeBarang As TextBox
    Friend WithEvents lblTxtKdBrng As Label
    Friend WithEvents txtRouting As TextBox
    Friend WithEvents lblRouting As Label
    Friend WithEvents txtSatuan As TextBox
    Friend WithEvents lblTxtSatuan As Label
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents lblLine As Label
    Friend WithEvents cmbLine As ComboBox
    Friend WithEvents lblJam As Label
    Friend WithEvents lblTanggal As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents txtJenisProduk As TextBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents UbahAntrianToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents txtIdJenisProduk As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents CmbLokasi As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtCatatan As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label4 As Label
End Class
