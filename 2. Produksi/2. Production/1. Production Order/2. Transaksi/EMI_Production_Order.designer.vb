<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Production_Order


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
		Me.LvData = New System.Windows.Forms.ListView()
		Me.ContextMenuStrip3 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.BatalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.UbahAntrianToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.cb_seluruh = New System.Windows.Forms.CheckBox()
		Me.Button2 = New System.Windows.Forms.Button()
		Me.Button1 = New System.Windows.Forms.Button()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.ComboBox2 = New System.Windows.Forms.ComboBox()
		Me.GroupBox2 = New System.Windows.Forms.GroupBox()
		Me.txtNmBrgPO = New System.Windows.Forms.TextBox()
		Me.txtKdBrgPO = New System.Windows.Forms.TextBox()
		Me.Label9 = New System.Windows.Forms.Label()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.LvOrder = New System.Windows.Forms.ListView()
		Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.HapusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.btnClear = New System.Windows.Forms.Button()
		Me.btnOk = New System.Windows.Forms.Button()
		Me.txtJumlah = New System.Windows.Forms.TextBox()
		Me.lblTxtJumlah = New System.Windows.Forms.Label()
		Me.txtNoPo = New System.Windows.Forms.TextBox()
		Me.txtNamaCustomer = New System.Windows.Forms.TextBox()
		Me.lbltxtNoPo = New System.Windows.Forms.Label()
		Me.lblTxtNmCust = New System.Windows.Forms.Label()
		Me.txtKodeCustomer = New System.Windows.Forms.TextBox()
		Me.lblTxtKodeCustomer = New System.Windows.Forms.Label()
		Me.txtKodeBarang = New System.Windows.Forms.TextBox()
		Me.lblTxtKdBrng = New System.Windows.Forms.Label()
		Me.txtNamaBarang = New System.Windows.Forms.TextBox()
		Me.lblTxtNmBrng = New System.Windows.Forms.Label()
		Me.txtSatuan = New System.Windows.Forms.TextBox()
		Me.lblTxtSatuan = New System.Windows.Forms.Label()
		Me.Btn_Simpan = New System.Windows.Forms.Button()
		Me.btnRefresh = New System.Windows.Forms.Button()
		Me.lblLine = New System.Windows.Forms.Label()
		Me.cmbLine = New System.Windows.Forms.ComboBox()
		Me.txtNoFaktur = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.CmbLokasi = New System.Windows.Forms.ComboBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.TxtCatatan = New System.Windows.Forms.TextBox()
		Me.DateTimePicker3 = New System.Windows.Forms.DateTimePicker()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.LvBahan = New System.Windows.Forms.ListView()
		Me.Txt_No_Formula = New System.Windows.Forms.TextBox()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.cmb_routing = New System.Windows.Forms.ComboBox()
		Me.txt_IdJenisProduk = New System.Windows.Forms.TextBox()
		Me.Button3 = New System.Windows.Forms.Button()
		Me.TabControl1 = New System.Windows.Forms.TabControl()
		Me.TabPage1 = New System.Windows.Forms.TabPage()
		Me.LvBahanNew = New System.Windows.Forms.ListView()
		Me.TabPage2 = New System.Windows.Forms.TabPage()
		Me.LvPackagingNew = New System.Windows.Forms.ListView()
		Me.LvPackaging = New System.Windows.Forms.ListView()
		Me.txt_faktur_bayangan = New System.Windows.Forms.TextBox()
		Me.Btn_UnRelease = New System.Windows.Forms.Button()
		Me.Cmb_Jenis = New System.Windows.Forms.ComboBox()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.Btn_Get_Detail_Formula = New System.Windows.Forms.Button()
		Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
		Me.Panel1.SuspendLayout()
		Me.ContextMenuStrip3.SuspendLayout()
		Me.ContextMenuStrip1.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		Me.GroupBox2.SuspendLayout()
		Me.ContextMenuStrip2.SuspendLayout()
		Me.TabControl1.SuspendLayout()
		Me.TabPage1.SuspendLayout()
		Me.TabPage2.SuspendLayout()
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
		Me.Panel1.Size = New System.Drawing.Size(1160, 45)
		Me.Panel1.TabIndex = 22
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold)
		Me.Label1.Location = New System.Drawing.Point(20, 7)
		Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(299, 29)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Transaksi - Production Order"
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(0, 44)
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
		Me.Panel5.Location = New System.Drawing.Point(1141, 56)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(19, 692)
		Me.Panel5.TabIndex = 37
		Me.Panel5.Visible = False
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Location = New System.Drawing.Point(-2, 731)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(1205, 12)
		Me.Panel4.TabIndex = 35
		Me.Panel4.Visible = False
		'
		'LvData
		'
		Me.LvData.CheckBoxes = True
		Me.LvData.ContextMenuStrip = Me.ContextMenuStrip3
		Me.LvData.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.LvData.FullRowSelect = True
		Me.LvData.GridLines = True
		Me.LvData.HideSelection = False
		Me.LvData.Location = New System.Drawing.Point(6, 49)
		Me.LvData.Name = "LvData"
		Me.LvData.Size = New System.Drawing.Size(1095, 258)
		Me.LvData.TabIndex = 381
		Me.LvData.UseCompatibleStateImageBehavior = False
		Me.LvData.View = System.Windows.Forms.View.Details
		'
		'ContextMenuStrip3
		'
		Me.ContextMenuStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BatalToolStripMenuItem})
		Me.ContextMenuStrip3.Name = "ContextMenuStrip3"
		Me.ContextMenuStrip3.Size = New System.Drawing.Size(101, 26)
		'
		'BatalToolStripMenuItem
		'
		Me.BatalToolStripMenuItem.Name = "BatalToolStripMenuItem"
		Me.BatalToolStripMenuItem.Size = New System.Drawing.Size(100, 22)
		Me.BatalToolStripMenuItem.Text = "Batal"
		'
		'ContextMenuStrip1
		'
		Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
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
		Me.GroupBox1.Controls.Add(Me.cb_seluruh)
		Me.GroupBox1.Controls.Add(Me.Button2)
		Me.GroupBox1.Controls.Add(Me.Button1)
		Me.GroupBox1.Controls.Add(Me.Label6)
		Me.GroupBox1.Controls.Add(Me.ComboBox2)
		Me.GroupBox1.Controls.Add(Me.LvData)
		Me.GroupBox1.Location = New System.Drawing.Point(27, 82)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(1110, 313)
		Me.GroupBox1.TabIndex = 382
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Data Order"
		'
		'cb_seluruh
		'
		Me.cb_seluruh.AutoSize = True
		Me.cb_seluruh.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.cb_seluruh.Location = New System.Drawing.Point(11, 22)
		Me.cb_seluruh.Name = "cb_seluruh"
		Me.cb_seluruh.Size = New System.Drawing.Size(95, 21)
		Me.cb_seluruh.TabIndex = 424
		Me.cb_seluruh.Text = "Pilih Semua"
		Me.cb_seluruh.UseVisualStyleBackColor = True
		'
		'Button2
		'
		Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Button2.Font = New System.Drawing.Font("Work Sans Medium", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Button2.ForeColor = System.Drawing.Color.White
		Me.Button2.Location = New System.Drawing.Point(881, 14)
		Me.Button2.Name = "Button2"
		Me.Button2.Size = New System.Drawing.Size(226, 29)
		Me.Button2.TabIndex = 423
		Me.Button2.Text = "Add Independent Order"
		Me.Button2.UseVisualStyleBackColor = False
		'
		'Button1
		'
		Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Button1.Font = New System.Drawing.Font("Work Sans Medium", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Button1.ForeColor = System.Drawing.Color.White
		Me.Button1.Location = New System.Drawing.Point(799, 14)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(81, 29)
		Me.Button1.TabIndex = 421
		Me.Button1.Text = "&Cari"
		Me.Button1.UseVisualStyleBackColor = False
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label6.Location = New System.Drawing.Point(515, 19)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(60, 17)
		Me.Label6.TabIndex = 422
		Me.Label6.Text = "Order By"
		'
		'ComboBox2
		'
		Me.ComboBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.ComboBox2.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.ComboBox2.FormattingEnabled = True
		Me.ComboBox2.Items.AddRange(New Object() {"Nama Barang", "Tanggal"})
		Me.ComboBox2.Location = New System.Drawing.Point(614, 16)
		Me.ComboBox2.Name = "ComboBox2"
		Me.ComboBox2.Size = New System.Drawing.Size(179, 24)
		Me.ComboBox2.TabIndex = 421
		'
		'GroupBox2
		'
		Me.GroupBox2.Controls.Add(Me.txtNmBrgPO)
		Me.GroupBox2.Controls.Add(Me.txtKdBrgPO)
		Me.GroupBox2.Controls.Add(Me.Label9)
		Me.GroupBox2.Controls.Add(Me.Label8)
		Me.GroupBox2.Controls.Add(Me.LvOrder)
		Me.GroupBox2.Location = New System.Drawing.Point(27, 401)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(346, 256)
		Me.GroupBox2.TabIndex = 383
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "Data Production Order"
		'
		'txtNmBrgPO
		'
		Me.txtNmBrgPO.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.txtNmBrgPO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtNmBrgPO.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.txtNmBrgPO.Location = New System.Drawing.Point(151, 227)
		Me.txtNmBrgPO.Name = "txtNmBrgPO"
		Me.txtNmBrgPO.ReadOnly = True
		Me.txtNmBrgPO.Size = New System.Drawing.Size(189, 20)
		Me.txtNmBrgPO.TabIndex = 422
		'
		'txtKdBrgPO
		'
		Me.txtKdBrgPO.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.txtKdBrgPO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtKdBrgPO.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.txtKdBrgPO.Location = New System.Drawing.Point(151, 202)
		Me.txtKdBrgPO.Name = "txtKdBrgPO"
		Me.txtKdBrgPO.ReadOnly = True
		Me.txtKdBrgPO.Size = New System.Drawing.Size(189, 20)
		Me.txtKdBrgPO.TabIndex = 422
		'
		'Label9
		'
		Me.Label9.AutoSize = True
		Me.Label9.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label9.Location = New System.Drawing.Point(8, 228)
		Me.Label9.Name = "Label9"
		Me.Label9.Size = New System.Drawing.Size(42, 17)
		Me.Label9.TabIndex = 427
		Me.Label9.Text = "Nama"
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label8.Location = New System.Drawing.Point(8, 205)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(81, 17)
		Me.Label8.TabIndex = 425
		Me.Label8.Text = "Kode Barang"
		'
		'LvOrder
		'
		Me.LvOrder.ContextMenuStrip = Me.ContextMenuStrip2
		Me.LvOrder.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.LvOrder.FullRowSelect = True
		Me.LvOrder.GridLines = True
		Me.LvOrder.HideSelection = False
		Me.LvOrder.Location = New System.Drawing.Point(10, 19)
		Me.LvOrder.Name = "LvOrder"
		Me.LvOrder.Size = New System.Drawing.Size(330, 180)
		Me.LvOrder.TabIndex = 381
		Me.LvOrder.UseCompatibleStateImageBehavior = False
		Me.LvOrder.View = System.Windows.Forms.View.Details
		'
		'ContextMenuStrip2
		'
		Me.ContextMenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
		Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HapusToolStripMenuItem})
		Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
		Me.ContextMenuStrip2.Size = New System.Drawing.Size(109, 26)
		'
		'HapusToolStripMenuItem
		'
		Me.HapusToolStripMenuItem.Name = "HapusToolStripMenuItem"
		Me.HapusToolStripMenuItem.Size = New System.Drawing.Size(108, 22)
		Me.HapusToolStripMenuItem.Text = "Hapus"
		'
		'btnClear
		'
		Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.btnClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
		Me.btnClear.ForeColor = System.Drawing.Color.White
		Me.btnClear.Location = New System.Drawing.Point(2323, 452)
		Me.btnClear.Name = "btnClear"
		Me.btnClear.Size = New System.Drawing.Size(91, 28)
		Me.btnClear.TabIndex = 392
		Me.btnClear.Text = "Clear"
		Me.btnClear.UseVisualStyleBackColor = False
		'
		'btnOk
		'
		Me.btnOk.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.btnOk.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
		Me.btnOk.ForeColor = System.Drawing.Color.White
		Me.btnOk.Location = New System.Drawing.Point(2323, 479)
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
		Me.txtJumlah.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
		Me.txtJumlah.Location = New System.Drawing.Point(2133, 481)
		Me.txtJumlah.Name = "txtJumlah"
		Me.txtJumlah.Size = New System.Drawing.Size(109, 21)
		Me.txtJumlah.TabIndex = 391
		Me.txtJumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		'
		'lblTxtJumlah
		'
		Me.lblTxtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lblTxtJumlah.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTxtJumlah.Location = New System.Drawing.Point(2133, 454)
		Me.lblTxtJumlah.Name = "lblTxtJumlah"
		Me.lblTxtJumlah.Size = New System.Drawing.Size(109, 25)
		Me.lblTxtJumlah.TabIndex = 390
		Me.lblTxtJumlah.Text = "Jumlah"
		Me.lblTxtJumlah.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'txtNoPo
		'
		Me.txtNoPo.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.txtNoPo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtNoPo.Enabled = False
		Me.txtNoPo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
		Me.txtNoPo.Location = New System.Drawing.Point(1372, 480)
		Me.txtNoPo.Name = "txtNoPo"
		Me.txtNoPo.Size = New System.Drawing.Size(120, 21)
		Me.txtNoPo.TabIndex = 388
		'
		'txtNamaCustomer
		'
		Me.txtNamaCustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.txtNamaCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtNamaCustomer.Enabled = False
		Me.txtNamaCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
		Me.txtNamaCustomer.Location = New System.Drawing.Point(1629, 480)
		Me.txtNamaCustomer.Name = "txtNamaCustomer"
		Me.txtNamaCustomer.Size = New System.Drawing.Size(229, 21)
		Me.txtNamaCustomer.TabIndex = 387
		'
		'lbltxtNoPo
		'
		Me.lbltxtNoPo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lbltxtNoPo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lbltxtNoPo.Location = New System.Drawing.Point(1372, 454)
		Me.lbltxtNoPo.Name = "lbltxtNoPo"
		Me.lbltxtNoPo.Size = New System.Drawing.Size(120, 25)
		Me.lbltxtNoPo.TabIndex = 385
		Me.lbltxtNoPo.Text = "No PO"
		Me.lbltxtNoPo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'lblTxtNmCust
		'
		Me.lblTxtNmCust.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lblTxtNmCust.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTxtNmCust.Location = New System.Drawing.Point(1629, 454)
		Me.lblTxtNmCust.Name = "lblTxtNmCust"
		Me.lblTxtNmCust.Size = New System.Drawing.Size(229, 25)
		Me.lblTxtNmCust.TabIndex = 386
		Me.lblTxtNmCust.Text = "Nama Customer"
		Me.lblTxtNmCust.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'txtKodeCustomer
		'
		Me.txtKodeCustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.txtKodeCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtKodeCustomer.Enabled = False
		Me.txtKodeCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
		Me.txtKodeCustomer.Location = New System.Drawing.Point(1493, 480)
		Me.txtKodeCustomer.Name = "txtKodeCustomer"
		Me.txtKodeCustomer.Size = New System.Drawing.Size(135, 21)
		Me.txtKodeCustomer.TabIndex = 397
		'
		'lblTxtKodeCustomer
		'
		Me.lblTxtKodeCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lblTxtKodeCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTxtKodeCustomer.Location = New System.Drawing.Point(1493, 454)
		Me.lblTxtKodeCustomer.Name = "lblTxtKodeCustomer"
		Me.lblTxtKodeCustomer.Size = New System.Drawing.Size(135, 25)
		Me.lblTxtKodeCustomer.TabIndex = 396
		Me.lblTxtKodeCustomer.Text = "Kode Customer"
		Me.lblTxtKodeCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'txtKodeBarang
		'
		Me.txtKodeBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.txtKodeBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtKodeBarang.Enabled = False
		Me.txtKodeBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
		Me.txtKodeBarang.Location = New System.Drawing.Point(1859, 480)
		Me.txtKodeBarang.Name = "txtKodeBarang"
		Me.txtKodeBarang.Size = New System.Drawing.Size(168, 21)
		Me.txtKodeBarang.TabIndex = 401
		'
		'lblTxtKdBrng
		'
		Me.lblTxtKdBrng.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lblTxtKdBrng.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTxtKdBrng.Location = New System.Drawing.Point(1859, 454)
		Me.lblTxtKdBrng.Name = "lblTxtKdBrng"
		Me.lblTxtKdBrng.Size = New System.Drawing.Size(168, 25)
		Me.lblTxtKdBrng.TabIndex = 400
		Me.lblTxtKdBrng.Text = "Kode Barang"
		Me.lblTxtKdBrng.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'txtNamaBarang
		'
		Me.txtNamaBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.txtNamaBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtNamaBarang.Enabled = False
		Me.txtNamaBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
		Me.txtNamaBarang.Location = New System.Drawing.Point(2029, 480)
		Me.txtNamaBarang.Name = "txtNamaBarang"
		Me.txtNamaBarang.Size = New System.Drawing.Size(229, 21)
		Me.txtNamaBarang.TabIndex = 399
		'
		'lblTxtNmBrng
		'
		Me.lblTxtNmBrng.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lblTxtNmBrng.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTxtNmBrng.Location = New System.Drawing.Point(2029, 454)
		Me.lblTxtNmBrng.Name = "lblTxtNmBrng"
		Me.lblTxtNmBrng.Size = New System.Drawing.Size(229, 25)
		Me.lblTxtNmBrng.TabIndex = 398
		Me.lblTxtNmBrng.Text = "Nama Barang"
		Me.lblTxtNmBrng.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'txtSatuan
		'
		Me.txtSatuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.txtSatuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtSatuan.Enabled = False
		Me.txtSatuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
		Me.txtSatuan.Location = New System.Drawing.Point(2244, 481)
		Me.txtSatuan.Name = "txtSatuan"
		Me.txtSatuan.Size = New System.Drawing.Size(77, 21)
		Me.txtSatuan.TabIndex = 403
		'
		'lblTxtSatuan
		'
		Me.lblTxtSatuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lblTxtSatuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTxtSatuan.Location = New System.Drawing.Point(2244, 454)
		Me.lblTxtSatuan.Name = "lblTxtSatuan"
		Me.lblTxtSatuan.Size = New System.Drawing.Size(77, 25)
		Me.lblTxtSatuan.TabIndex = 402
		Me.lblTxtSatuan.Text = "Satuan"
		Me.lblTxtSatuan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'Btn_Simpan
		'
		Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Simpan.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans Medium", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
		Me.Btn_Simpan.Location = New System.Drawing.Point(29, 694)
		Me.Btn_Simpan.Name = "Btn_Simpan"
		Me.Btn_Simpan.Size = New System.Drawing.Size(84, 36)
		Me.Btn_Simpan.TabIndex = 362
		Me.Btn_Simpan.Text = "&Simpan"
		Me.Btn_Simpan.UseVisualStyleBackColor = False
		'
		'btnRefresh
		'
		Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
		Me.btnRefresh.Font = New System.Drawing.Font("Work Sans Medium", 8.0!, System.Drawing.FontStyle.Bold)
		Me.btnRefresh.ForeColor = System.Drawing.Color.White
		Me.btnRefresh.Location = New System.Drawing.Point(117, 694)
		Me.btnRefresh.Name = "btnRefresh"
		Me.btnRefresh.Size = New System.Drawing.Size(84, 36)
		Me.btnRefresh.TabIndex = 380
		Me.btnRefresh.Text = "&Refresh"
		Me.btnRefresh.UseVisualStyleBackColor = False
		'
		'lblLine
		'
		Me.lblLine.AutoSize = True
		Me.lblLine.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.lblLine.Location = New System.Drawing.Point(915, 769)
		Me.lblLine.Name = "lblLine"
		Me.lblLine.Size = New System.Drawing.Size(35, 17)
		Me.lblLine.TabIndex = 409
		Me.lblLine.Text = "Line"
		Me.lblLine.Visible = False
		'
		'cmbLine
		'
		Me.cmbLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.cmbLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmbLine.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
		Me.cmbLine.FormattingEnabled = True
		Me.cmbLine.Location = New System.Drawing.Point(959, 768)
		Me.cmbLine.Name = "cmbLine"
		Me.cmbLine.Size = New System.Drawing.Size(147, 23)
		Me.cmbLine.TabIndex = 407
		Me.cmbLine.Visible = False
		'
		'txtNoFaktur
		'
		Me.txtNoFaktur.BackColor = System.Drawing.Color.Goldenrod
		Me.txtNoFaktur.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtNoFaktur.ForeColor = System.Drawing.SystemColors.Window
		Me.txtNoFaktur.Location = New System.Drawing.Point(27, 55)
		Me.txtNoFaktur.MaxLength = 30
		Me.txtNoFaktur.Name = "txtNoFaktur"
		Me.txtNoFaktur.Size = New System.Drawing.Size(211, 21)
		Me.txtNoFaktur.TabIndex = 414
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label2.Location = New System.Drawing.Point(878, 58)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(45, 17)
		Me.Label2.TabIndex = 416
		Me.Label2.Text = "Lokasi"
		'
		'CmbLokasi
		'
		Me.CmbLokasi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.CmbLokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.CmbLokasi.Enabled = False
		Me.CmbLokasi.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.CmbLokasi.FormattingEnabled = True
		Me.CmbLokasi.Location = New System.Drawing.Point(949, 56)
		Me.CmbLokasi.Name = "CmbLokasi"
		Me.CmbLokasi.Size = New System.Drawing.Size(179, 24)
		Me.CmbLokasi.TabIndex = 415
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label3.Location = New System.Drawing.Point(25, 667)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(54, 17)
		Me.Label3.TabIndex = 417
		Me.Label3.Text = "Catatan"
		'
		'TxtCatatan
		'
		Me.TxtCatatan.BackColor = System.Drawing.Color.White
		Me.TxtCatatan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtCatatan.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.TxtCatatan.Location = New System.Drawing.Point(98, 667)
		Me.TxtCatatan.Name = "TxtCatatan"
		Me.TxtCatatan.Size = New System.Drawing.Size(400, 20)
		Me.TxtCatatan.TabIndex = 418
		'
		'DateTimePicker3
		'
		Me.DateTimePicker3.CustomFormat = "dd MMMM yyyy"
		Me.DateTimePicker3.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right
		Me.DateTimePicker3.Enabled = False
		Me.DateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Custom
		Me.DateTimePicker3.Location = New System.Drawing.Point(392, 165)
		Me.DateTimePicker3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
		Me.DateTimePicker3.Name = "DateTimePicker3"
		Me.DateTimePicker3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.DateTimePicker3.Size = New System.Drawing.Size(206, 20)
		Me.DateTimePicker3.TabIndex = 421
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.Label7.Location = New System.Drawing.Point(300, 163)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(83, 17)
		Me.Label7.TabIndex = 420
		Me.Label7.Text = "Tgl Formula"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
		Me.Label4.Location = New System.Drawing.Point(11, 165)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(81, 17)
		Me.Label4.TabIndex = 419
		Me.Label4.Text = "No Formula"
		'
		'LvBahan
		'
		Me.LvBahan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
		Me.LvBahan.FullRowSelect = True
		Me.LvBahan.GridLines = True
		Me.LvBahan.HideSelection = False
		Me.LvBahan.Location = New System.Drawing.Point(392, 750)
		Me.LvBahan.Name = "LvBahan"
		Me.LvBahan.Size = New System.Drawing.Size(723, 194)
		Me.LvBahan.TabIndex = 381
		Me.LvBahan.UseCompatibleStateImageBehavior = False
		Me.LvBahan.View = System.Windows.Forms.View.Details
		'
		'Txt_No_Formula
		'
		Me.Txt_No_Formula.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_No_Formula.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_No_Formula.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
		Me.Txt_No_Formula.Location = New System.Drawing.Point(105, 164)
		Me.Txt_No_Formula.Name = "Txt_No_Formula"
		Me.Txt_No_Formula.ReadOnly = True
		Me.Txt_No_Formula.Size = New System.Drawing.Size(189, 21)
		Me.Txt_No_Formula.TabIndex = 419
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label5.Location = New System.Drawing.Point(504, 669)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(51, 17)
		Me.Label5.TabIndex = 420
		Me.Label5.Text = "Routing"
		'
		'cmb_routing
		'
		Me.cmb_routing.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.cmb_routing.Cursor = System.Windows.Forms.Cursors.Hand
		Me.cmb_routing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmb_routing.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.cmb_routing.FormattingEnabled = True
		Me.cmb_routing.Items.AddRange(New Object() {"ROUTING 1 (Mixer, Hammer)", "ROUTING 2 (Mixer, Pellet)"})
		Me.cmb_routing.Location = New System.Drawing.Point(567, 667)
		Me.cmb_routing.Name = "cmb_routing"
		Me.cmb_routing.Size = New System.Drawing.Size(271, 24)
		Me.cmb_routing.TabIndex = 419
		'
		'txt_IdJenisProduk
		'
		Me.txt_IdJenisProduk.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.txt_IdJenisProduk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txt_IdJenisProduk.Enabled = False
		Me.txt_IdJenisProduk.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.txt_IdJenisProduk.Location = New System.Drawing.Point(452, 703)
		Me.txt_IdJenisProduk.Name = "txt_IdJenisProduk"
		Me.txt_IdJenisProduk.Size = New System.Drawing.Size(150, 20)
		Me.txt_IdJenisProduk.TabIndex = 421
		Me.txt_IdJenisProduk.Visible = False
		'
		'Button3
		'
		Me.Button3.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Button3.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Button3.Font = New System.Drawing.Font("Work Sans Medium", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Button3.ForeColor = System.Drawing.Color.White
		Me.Button3.Location = New System.Drawing.Point(205, 694)
		Me.Button3.Name = "Button3"
		Me.Button3.Size = New System.Drawing.Size(84, 36)
		Me.Button3.TabIndex = 422
		Me.Button3.Text = "Release"
		Me.Button3.UseVisualStyleBackColor = False
		Me.Button3.Visible = False
		'
		'TabControl1
		'
		Me.TabControl1.Controls.Add(Me.TabPage1)
		Me.TabControl1.Controls.Add(Me.TabPage2)
		Me.TabControl1.Location = New System.Drawing.Point(398, 438)
		Me.TabControl1.Name = "TabControl1"
		Me.TabControl1.SelectedIndex = 0
		Me.TabControl1.Size = New System.Drawing.Size(739, 219)
		Me.TabControl1.TabIndex = 423
		'
		'TabPage1
		'
		Me.TabPage1.Controls.Add(Me.LvBahanNew)
		Me.TabPage1.Controls.Add(Me.DateTimePicker3)
		Me.TabPage1.Controls.Add(Me.Label7)
		Me.TabPage1.Controls.Add(Me.Label4)
		Me.TabPage1.Controls.Add(Me.Txt_No_Formula)
		Me.TabPage1.Location = New System.Drawing.Point(4, 22)
		Me.TabPage1.Name = "TabPage1"
		Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage1.Size = New System.Drawing.Size(731, 193)
		Me.TabPage1.TabIndex = 0
		Me.TabPage1.Text = "Material"
		Me.TabPage1.UseVisualStyleBackColor = True
		'
		'LvBahanNew
		'
		Me.LvBahanNew.Dock = System.Windows.Forms.DockStyle.Fill
		Me.LvBahanNew.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.LvBahanNew.FullRowSelect = True
		Me.LvBahanNew.GridLines = True
		Me.LvBahanNew.HideSelection = False
		Me.LvBahanNew.Location = New System.Drawing.Point(3, 3)
		Me.LvBahanNew.Name = "LvBahanNew"
		Me.LvBahanNew.Size = New System.Drawing.Size(725, 187)
		Me.LvBahanNew.TabIndex = 422
		Me.LvBahanNew.UseCompatibleStateImageBehavior = False
		Me.LvBahanNew.View = System.Windows.Forms.View.Details
		'
		'TabPage2
		'
		Me.TabPage2.Controls.Add(Me.LvPackagingNew)
		Me.TabPage2.Location = New System.Drawing.Point(4, 22)
		Me.TabPage2.Name = "TabPage2"
		Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage2.Size = New System.Drawing.Size(731, 193)
		Me.TabPage2.TabIndex = 1
		Me.TabPage2.Text = "Packaging"
		Me.TabPage2.UseVisualStyleBackColor = True
		'
		'LvPackagingNew
		'
		Me.LvPackagingNew.Dock = System.Windows.Forms.DockStyle.Fill
		Me.LvPackagingNew.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.LvPackagingNew.FullRowSelect = True
		Me.LvPackagingNew.GridLines = True
		Me.LvPackagingNew.HideSelection = False
		Me.LvPackagingNew.Location = New System.Drawing.Point(3, 3)
		Me.LvPackagingNew.Name = "LvPackagingNew"
		Me.LvPackagingNew.Size = New System.Drawing.Size(725, 187)
		Me.LvPackagingNew.TabIndex = 383
		Me.LvPackagingNew.UseCompatibleStateImageBehavior = False
		Me.LvPackagingNew.View = System.Windows.Forms.View.Details
		'
		'LvPackaging
		'
		Me.LvPackaging.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
		Me.LvPackaging.FullRowSelect = True
		Me.LvPackaging.GridLines = True
		Me.LvPackaging.HideSelection = False
		Me.LvPackaging.Location = New System.Drawing.Point(1175, 768)
		Me.LvPackaging.Name = "LvPackaging"
		Me.LvPackaging.Size = New System.Drawing.Size(723, 194)
		Me.LvPackaging.TabIndex = 382
		Me.LvPackaging.UseCompatibleStateImageBehavior = False
		Me.LvPackaging.View = System.Windows.Forms.View.Details
		'
		'txt_faktur_bayangan
		'
		Me.txt_faktur_bayangan.BackColor = System.Drawing.Color.Goldenrod
		Me.txt_faktur_bayangan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txt_faktur_bayangan.ForeColor = System.Drawing.SystemColors.Window
		Me.txt_faktur_bayangan.Location = New System.Drawing.Point(244, 55)
		Me.txt_faktur_bayangan.MaxLength = 30
		Me.txt_faktur_bayangan.Name = "txt_faktur_bayangan"
		Me.txt_faktur_bayangan.ReadOnly = True
		Me.txt_faktur_bayangan.Size = New System.Drawing.Size(211, 21)
		Me.txt_faktur_bayangan.TabIndex = 424
		Me.txt_faktur_bayangan.Visible = False
		'
		'Btn_UnRelease
		'
		Me.Btn_UnRelease.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_UnRelease.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_UnRelease.Font = New System.Drawing.Font("Work Sans Medium", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_UnRelease.ForeColor = System.Drawing.Color.White
		Me.Btn_UnRelease.Location = New System.Drawing.Point(293, 694)
		Me.Btn_UnRelease.Name = "Btn_UnRelease"
		Me.Btn_UnRelease.Size = New System.Drawing.Size(93, 36)
		Me.Btn_UnRelease.TabIndex = 422
		Me.Btn_UnRelease.Text = "UnRelease"
		Me.Btn_UnRelease.UseVisualStyleBackColor = False
		Me.Btn_UnRelease.Visible = False
		'
		'Cmb_Jenis
		'
		Me.Cmb_Jenis.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Cmb_Jenis.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Jenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Jenis.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Cmb_Jenis.FormattingEnabled = True
		Me.Cmb_Jenis.Items.AddRange(New Object() {"ROUTING 1 (Mixer, Hammer)", "ROUTING 2 (Mixer, Pellet)"})
		Me.Cmb_Jenis.Location = New System.Drawing.Point(899, 666)
		Me.Cmb_Jenis.Name = "Cmb_Jenis"
		Me.Cmb_Jenis.Size = New System.Drawing.Size(234, 24)
		Me.Cmb_Jenis.TabIndex = 419
		'
		'Label10
		'
		Me.Label10.AutoSize = True
		Me.Label10.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label10.Location = New System.Drawing.Point(852, 669)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(38, 17)
		Me.Label10.TabIndex = 420
		Me.Label10.Text = "Jenis"
		'
		'Btn_Get_Detail_Formula
		'
		Me.Btn_Get_Detail_Formula.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Get_Detail_Formula.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Get_Detail_Formula.Font = New System.Drawing.Font("Work Sans Medium", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Get_Detail_Formula.ForeColor = System.Drawing.Color.White
		Me.Btn_Get_Detail_Formula.Location = New System.Drawing.Point(398, 401)
		Me.Btn_Get_Detail_Formula.Name = "Btn_Get_Detail_Formula"
		Me.Btn_Get_Detail_Formula.Size = New System.Drawing.Size(163, 29)
		Me.Btn_Get_Detail_Formula.TabIndex = 425
		Me.Btn_Get_Detail_Formula.Text = "&Get Detail Bahan"
		Me.Btn_Get_Detail_Formula.UseVisualStyleBackColor = False
		'
		'PanelGradient1
		'
		Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
		Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
		Me.PanelGradient1.cuteTransparent1 = 100
		Me.PanelGradient1.cuteTransparent2 = 64
		Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.PanelGradient1.Location = New System.Drawing.Point(0, 43)
		Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
		Me.PanelGradient1.Name = "PanelGradient1"
		Me.PanelGradient1.Size = New System.Drawing.Size(1160, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'EMI_Production_Order
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(1160, 743)
		Me.Controls.Add(Me.Btn_Get_Detail_Formula)
		Me.Controls.Add(Me.LvPackaging)
		Me.Controls.Add(Me.LvBahan)
		Me.Controls.Add(Me.txt_faktur_bayangan)
		Me.Controls.Add(Me.TabControl1)
		Me.Controls.Add(Me.Btn_UnRelease)
		Me.Controls.Add(Me.Button3)
		Me.Controls.Add(Me.txt_IdJenisProduk)
		Me.Controls.Add(Me.Label10)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.Cmb_Jenis)
		Me.Controls.Add(Me.cmb_routing)
		Me.Controls.Add(Me.TxtCatatan)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.CmbLokasi)
		Me.Controls.Add(Me.txtNoFaktur)
		Me.Controls.Add(Me.lblLine)
		Me.Controls.Add(Me.cmbLine)
		Me.Controls.Add(Me.txtSatuan)
		Me.Controls.Add(Me.lblTxtSatuan)
		Me.Controls.Add(Me.txtKodeBarang)
		Me.Controls.Add(Me.lblTxtKdBrng)
		Me.Controls.Add(Me.txtNamaBarang)
		Me.Controls.Add(Me.lblTxtNmBrng)
		Me.Controls.Add(Me.txtKodeCustomer)
		Me.Controls.Add(Me.lblTxtKodeCustomer)
		Me.Controls.Add(Me.btnClear)
		Me.Controls.Add(Me.btnOk)
		Me.Controls.Add(Me.txtJumlah)
		Me.Controls.Add(Me.lblTxtJumlah)
		Me.Controls.Add(Me.txtNoPo)
		Me.Controls.Add(Me.txtNamaCustomer)
		Me.Controls.Add(Me.lbltxtNoPo)
		Me.Controls.Add(Me.lblTxtNmCust)
		Me.Controls.Add(Me.GroupBox2)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.btnRefresh)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Panel5)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel1)
		Me.Controls.Add(Me.Btn_Simpan)
		Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.Name = "EMI_Production_Order"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.ContextMenuStrip3.ResumeLayout(False)
		Me.ContextMenuStrip1.ResumeLayout(False)
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.GroupBox2.ResumeLayout(False)
		Me.GroupBox2.PerformLayout()
		Me.ContextMenuStrip2.ResumeLayout(False)
		Me.TabControl1.ResumeLayout(False)
		Me.TabPage1.ResumeLayout(False)
		Me.TabPage1.PerformLayout()
		Me.TabPage2.ResumeLayout(False)
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
    Friend WithEvents LvData As ListView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents LvOrder As ListView
    Friend WithEvents btnClear As Button
    Friend WithEvents btnOk As Button
    Friend WithEvents txtJumlah As TextBox
    Friend WithEvents lblTxtJumlah As Label
    Friend WithEvents txtNoPo As TextBox
    Friend WithEvents txtNamaCustomer As TextBox
    Friend WithEvents lbltxtNoPo As Label
    Friend WithEvents lblTxtNmCust As Label
    Friend WithEvents txtKodeCustomer As TextBox
    Friend WithEvents lblTxtKodeCustomer As Label
    Friend WithEvents txtKodeBarang As TextBox
    Friend WithEvents lblTxtKdBrng As Label
    Friend WithEvents txtNamaBarang As TextBox
    Friend WithEvents lblTxtNmBrng As Label
    Friend WithEvents txtSatuan As TextBox
    Friend WithEvents lblTxtSatuan As Label
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents lblLine As Label
    Friend WithEvents cmbLine As ComboBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents UbahAntrianToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents txtNoFaktur As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents CmbLokasi As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtCatatan As TextBox
    Friend WithEvents LvBahan As ListView
    Friend WithEvents Label4 As Label
    Friend WithEvents Txt_No_Formula As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cmb_routing As ComboBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents DateTimePicker3 As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents cb_seluruh As CheckBox
    Friend WithEvents txt_IdJenisProduk As TextBox
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents HapusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Button3 As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents LvPackaging As ListView
    Friend WithEvents txt_faktur_bayangan As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents txtKdBrgPO As TextBox
    Friend WithEvents txtNmBrgPO As TextBox
    Friend WithEvents ContextMenuStrip3 As ContextMenuStrip
    Friend WithEvents BatalToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Btn_UnRelease As Button
    Friend WithEvents Cmb_Jenis As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents LvBahanNew As ListView
    Friend WithEvents LvPackagingNew As ListView
	Friend WithEvents Btn_Get_Detail_Formula As Button
End Class
