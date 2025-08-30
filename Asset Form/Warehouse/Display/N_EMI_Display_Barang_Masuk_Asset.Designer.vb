<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Display_Barang_Masuk_Asset
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
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Lv_PembelianLoading = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SaliinNoFakturToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BatalBarangMasukToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Lv_PODetail = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BatalBarangMasukToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.BatalValidasiBarangMasukToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Txt_JumlahMasuk = New System.Windows.Forms.TextBox()
        Me.Txt_JumlahBlmMasuk = New System.Windows.Forms.TextBox()
        Me.Lv_DetailPallet = New System.Windows.Forms.ListView()
        Me.Txt_PalletBlmMasuk = New System.Windows.Forms.TextBox()
        Me.Txt_PalletMasuk = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Cmb_Tanggal = New System.Windows.Forms.ComboBox()
        Me.Chk_Tanggal = New System.Windows.Forms.CheckBox()
        Me.Tgl_1 = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Tgl_2 = New System.Windows.Forms.DateTimePicker()
        Me.Chk_Lain = New System.Windows.Forms.CheckBox()
        Me.Cmb_Lain = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Txt_ValueLain = New System.Windows.Forms.TextBox()
        Me.Chk_HariIni = New System.Windows.Forms.CheckBox()
        Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
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
        Me.Panel1.TabIndex = 23
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
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 8)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(362, 28)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Summary Data - Barang Masuk Asset"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 43)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1274, 12)
        Me.Panel2.TabIndex = 35
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 48)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 694)
        Me.Panel3.TabIndex = 36
        Me.Panel3.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(1165, 52)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 694)
        Me.Panel4.TabIndex = 36
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(11, 596)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1274, 15)
        Me.Panel5.TabIndex = 35
        Me.Panel5.Visible = False
        '
        'Lv_PembelianLoading
        '
        Me.Lv_PembelianLoading.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Lv_PembelianLoading.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Lv_PembelianLoading.FullRowSelect = True
        Me.Lv_PembelianLoading.GridLines = True
        Me.Lv_PembelianLoading.HideSelection = False
        Me.Lv_PembelianLoading.Location = New System.Drawing.Point(19, 71)
        Me.Lv_PembelianLoading.Name = "Lv_PembelianLoading"
        Me.Lv_PembelianLoading.Size = New System.Drawing.Size(1147, 153)
        Me.Lv_PembelianLoading.TabIndex = 352
        Me.Lv_PembelianLoading.UseCompatibleStateImageBehavior = False
        Me.Lv_PembelianLoading.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaliinNoFakturToolStripMenuItem, Me.BatalBarangMasukToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(179, 48)
        '
        'SaliinNoFakturToolStripMenuItem
        '
        Me.SaliinNoFakturToolStripMenuItem.Name = "SaliinNoFakturToolStripMenuItem"
        Me.SaliinNoFakturToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.SaliinNoFakturToolStripMenuItem.Text = "Saliin No Faktur"
        '
        'BatalBarangMasukToolStripMenuItem
        '
        Me.BatalBarangMasukToolStripMenuItem.Name = "BatalBarangMasukToolStripMenuItem"
        Me.BatalBarangMasukToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.BatalBarangMasukToolStripMenuItem.Text = "Batal Barang Masuk"
        Me.BatalBarangMasukToolStripMenuItem.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(19, 224)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1436, 12)
        Me.Panel7.TabIndex = 353
        Me.Panel7.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Lv_PODetail)
        Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 236)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(600, 210)
        Me.GroupBox1.TabIndex = 354
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Barang"
        '
        'Lv_PODetail
        '
        Me.Lv_PODetail.ContextMenuStrip = Me.ContextMenuStrip2
        Me.Lv_PODetail.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Lv_PODetail.FullRowSelect = True
        Me.Lv_PODetail.GridLines = True
        Me.Lv_PODetail.HideSelection = False
        Me.Lv_PODetail.Location = New System.Drawing.Point(6, 21)
        Me.Lv_PODetail.Name = "Lv_PODetail"
        Me.Lv_PODetail.Size = New System.Drawing.Size(588, 183)
        Me.Lv_PODetail.TabIndex = 0
        Me.Lv_PODetail.UseCompatibleStateImageBehavior = False
        Me.Lv_PODetail.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BatalBarangMasukToolStripMenuItem1, Me.BatalValidasiBarangMasukToolStripMenuItem})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(221, 70)
        '
        'BatalBarangMasukToolStripMenuItem1
        '
        Me.BatalBarangMasukToolStripMenuItem1.Name = "BatalBarangMasukToolStripMenuItem1"
        Me.BatalBarangMasukToolStripMenuItem1.Size = New System.Drawing.Size(220, 22)
        Me.BatalBarangMasukToolStripMenuItem1.Text = "Batal Barang Masuk"
        '
        'BatalValidasiBarangMasukToolStripMenuItem
        '
        Me.BatalValidasiBarangMasukToolStripMenuItem.Name = "BatalValidasiBarangMasukToolStripMenuItem"
        Me.BatalValidasiBarangMasukToolStripMenuItem.Size = New System.Drawing.Size(220, 22)
        Me.BatalValidasiBarangMasukToolStripMenuItem.Text = "Batal Validasi Barang Masuk"
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.Red
        Me.Panel10.Location = New System.Drawing.Point(622, 245)
        Me.Panel10.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(12, 491)
        Me.Panel10.TabIndex = 356
        Me.Panel10.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(18, 445)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1436, 12)
        Me.Panel6.TabIndex = 353
        Me.Panel6.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Label2.Location = New System.Drawing.Point(6, 157)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 16)
        Me.Label2.TabIndex = 343
        Me.Label2.Text = "Jumlah Masuk"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Label3.Location = New System.Drawing.Point(252, 158)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(118, 16)
        Me.Label3.TabIndex = 343
        Me.Label3.Text = "Jumlah Pallet Masuk"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Label5.Location = New System.Drawing.Point(6, 185)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(121, 16)
        Me.Label5.TabIndex = 343
        Me.Label5.Text = "Jumlah Belum Masuk"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Label4.Location = New System.Drawing.Point(253, 186)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(155, 16)
        Me.Label4.TabIndex = 343
        Me.Label4.Text = "Jumlah Pallet Belum Masuk"
        '
        'Txt_JumlahMasuk
        '
        Me.Txt_JumlahMasuk.Enabled = False
        Me.Txt_JumlahMasuk.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_JumlahMasuk.Location = New System.Drawing.Point(133, 155)
        Me.Txt_JumlahMasuk.Name = "Txt_JumlahMasuk"
        Me.Txt_JumlahMasuk.Size = New System.Drawing.Size(114, 20)
        Me.Txt_JumlahMasuk.TabIndex = 1
        Me.Txt_JumlahMasuk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Txt_JumlahBlmMasuk
        '
        Me.Txt_JumlahBlmMasuk.Enabled = False
        Me.Txt_JumlahBlmMasuk.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_JumlahBlmMasuk.Location = New System.Drawing.Point(133, 183)
        Me.Txt_JumlahBlmMasuk.Name = "Txt_JumlahBlmMasuk"
        Me.Txt_JumlahBlmMasuk.Size = New System.Drawing.Size(114, 20)
        Me.Txt_JumlahBlmMasuk.TabIndex = 3
        Me.Txt_JumlahBlmMasuk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Lv_DetailPallet
        '
        Me.Lv_DetailPallet.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Lv_DetailPallet.FullRowSelect = True
        Me.Lv_DetailPallet.GridLines = True
        Me.Lv_DetailPallet.HideSelection = False
        Me.Lv_DetailPallet.Location = New System.Drawing.Point(6, 21)
        Me.Lv_DetailPallet.Name = "Lv_DetailPallet"
        Me.Lv_DetailPallet.Size = New System.Drawing.Size(518, 128)
        Me.Lv_DetailPallet.TabIndex = 0
        Me.Lv_DetailPallet.UseCompatibleStateImageBehavior = False
        Me.Lv_DetailPallet.View = System.Windows.Forms.View.Details
        '
        'Txt_PalletBlmMasuk
        '
        Me.Txt_PalletBlmMasuk.Enabled = False
        Me.Txt_PalletBlmMasuk.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_PalletBlmMasuk.Location = New System.Drawing.Point(410, 183)
        Me.Txt_PalletBlmMasuk.Name = "Txt_PalletBlmMasuk"
        Me.Txt_PalletBlmMasuk.Size = New System.Drawing.Size(114, 20)
        Me.Txt_PalletBlmMasuk.TabIndex = 4
        Me.Txt_PalletBlmMasuk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Txt_PalletMasuk
        '
        Me.Txt_PalletMasuk.Enabled = False
        Me.Txt_PalletMasuk.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_PalletMasuk.Location = New System.Drawing.Point(410, 155)
        Me.Txt_PalletMasuk.Name = "Txt_PalletMasuk"
        Me.Txt_PalletMasuk.Size = New System.Drawing.Size(114, 20)
        Me.Txt_PalletMasuk.TabIndex = 2
        Me.Txt_PalletMasuk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Txt_PalletMasuk)
        Me.GroupBox2.Controls.Add(Me.Txt_PalletBlmMasuk)
        Me.GroupBox2.Controls.Add(Me.Lv_DetailPallet)
        Me.GroupBox2.Controls.Add(Me.Txt_JumlahBlmMasuk)
        Me.GroupBox2.Controls.Add(Me.Txt_JumlahMasuk)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.GroupBox2.Location = New System.Drawing.Point(635, 236)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(532, 210)
        Me.GroupBox2.TabIndex = 355
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Pallet"
        '
        'Cmb_Tanggal
        '
        Me.Cmb_Tanggal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Tanggal.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Cmb_Tanggal.FormattingEnabled = True
        Me.Cmb_Tanggal.Location = New System.Drawing.Point(138, 79)
        Me.Cmb_Tanggal.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Tanggal.Name = "Cmb_Tanggal"
        Me.Cmb_Tanggal.Size = New System.Drawing.Size(123, 24)
        Me.Cmb_Tanggal.TabIndex = 3
        '
        'Chk_Tanggal
        '
        Me.Chk_Tanggal.AutoSize = True
        Me.Chk_Tanggal.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Chk_Tanggal.Location = New System.Drawing.Point(10, 81)
        Me.Chk_Tanggal.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Chk_Tanggal.Name = "Chk_Tanggal"
        Me.Chk_Tanggal.Size = New System.Drawing.Size(124, 20)
        Me.Chk_Tanggal.TabIndex = 2
        Me.Chk_Tanggal.Text = "Parameter Tanggal"
        Me.Chk_Tanggal.UseVisualStyleBackColor = True
        '
        'Tgl_1
        '
        Me.Tgl_1.CustomFormat = "dd MMMM yyyy"
        Me.Tgl_1.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Tgl_1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl_1.Location = New System.Drawing.Point(265, 82)
        Me.Tgl_1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Tgl_1.Name = "Tgl_1"
        Me.Tgl_1.Size = New System.Drawing.Size(158, 20)
        Me.Tgl_1.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Label7.Location = New System.Drawing.Point(431, 83)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(25, 16)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "s/d"
        '
        'Tgl_2
        '
        Me.Tgl_2.CustomFormat = "dd MMMM yyyy"
        Me.Tgl_2.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Tgl_2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl_2.Location = New System.Drawing.Point(465, 82)
        Me.Tgl_2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Tgl_2.Name = "Tgl_2"
        Me.Tgl_2.Size = New System.Drawing.Size(158, 20)
        Me.Tgl_2.TabIndex = 5
        '
        'Chk_Lain
        '
        Me.Chk_Lain.AutoSize = True
        Me.Chk_Lain.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Chk_Lain.Location = New System.Drawing.Point(10, 108)
        Me.Chk_Lain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Chk_Lain.Name = "Chk_Lain"
        Me.Chk_Lain.Size = New System.Drawing.Size(107, 20)
        Me.Chk_Lain.TabIndex = 6
        Me.Chk_Lain.Text = "Parameter Lain"
        Me.Chk_Lain.UseVisualStyleBackColor = True
        '
        'Cmb_Lain
        '
        Me.Cmb_Lain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lain.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Cmb_Lain.FormattingEnabled = True
        Me.Cmb_Lain.Location = New System.Drawing.Point(138, 109)
        Me.Cmb_Lain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Lain.Name = "Cmb_Lain"
        Me.Cmb_Lain.Size = New System.Drawing.Size(123, 24)
        Me.Cmb_Lain.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Label6.Location = New System.Drawing.Point(270, 112)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 16)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Value"
        '
        'Txt_ValueLain
        '
        Me.Txt_ValueLain.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_ValueLain.Location = New System.Drawing.Point(320, 109)
        Me.Txt_ValueLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_ValueLain.Name = "Txt_ValueLain"
        Me.Txt_ValueLain.Size = New System.Drawing.Size(217, 20)
        Me.Txt_ValueLain.TabIndex = 8
        '
        'Chk_HariIni
        '
        Me.Chk_HariIni.AutoSize = True
        Me.Chk_HariIni.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Chk_HariIni.Location = New System.Drawing.Point(11, 55)
        Me.Chk_HariIni.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Chk_HariIni.Name = "Chk_HariIni"
        Me.Chk_HariIni.Size = New System.Drawing.Size(118, 20)
        Me.Chk_HariIni.TabIndex = 1
        Me.Chk_HariIni.Text = "Transaksi Hari Ini"
        Me.Chk_HariIni.UseVisualStyleBackColor = True
        '
        'Cmb_Lokasi
        '
        Me.Cmb_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lokasi.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Cmb_Lokasi.FormattingEnabled = True
        Me.Cmb_Lokasi.Location = New System.Drawing.Point(11, 21)
        Me.Cmb_Lokasi.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
        Me.Cmb_Lokasi.Size = New System.Drawing.Size(209, 24)
        Me.Cmb_Lokasi.TabIndex = 0
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(542, 102)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(81, 29)
        Me.Btn_Cari.TabIndex = 9
        Me.Btn_Cari.Text = "&Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Btn_Cari)
        Me.GroupBox3.Controls.Add(Me.Cmb_Lokasi)
        Me.GroupBox3.Controls.Add(Me.Chk_HariIni)
        Me.GroupBox3.Controls.Add(Me.Txt_ValueLain)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Cmb_Lain)
        Me.GroupBox3.Controls.Add(Me.Chk_Lain)
        Me.GroupBox3.Controls.Add(Me.Tgl_2)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Tgl_1)
        Me.GroupBox3.Controls.Add(Me.Chk_Tanggal)
        Me.GroupBox3.Controls.Add(Me.Cmb_Tanggal)
        Me.GroupBox3.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.GroupBox3.Location = New System.Drawing.Point(20, 456)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox3.Size = New System.Drawing.Size(632, 141)
        Me.GroupBox3.TabIndex = 351
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Filter Data"
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.LightYellow
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Location = New System.Drawing.Point(1030, 54)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(12, 12)
        Me.Panel8.TabIndex = 349
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(1046, 52)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(118, 16)
        Me.Label9.TabIndex = 350
        Me.Label9.Text = "Belum Barang Masuk"
        '
        'N_EMI_Display_Barang_Masuk_Asset
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1184, 611)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel10)
        Me.Controls.Add(Me.Lv_PembelianLoading)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_Display_Barang_Masuk_Asset"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Lv_PembelianLoading As ListView
    Friend WithEvents Panel7 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Lv_PODetail As ListView
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Txt_JumlahMasuk As TextBox
    Friend WithEvents Txt_JumlahBlmMasuk As TextBox
    Friend WithEvents Lv_DetailPallet As ListView
    Friend WithEvents Txt_PalletBlmMasuk As TextBox
    Friend WithEvents Txt_PalletMasuk As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Cmb_Tanggal As ComboBox
    Friend WithEvents Chk_Tanggal As CheckBox
    Friend WithEvents Tgl_1 As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents Tgl_2 As DateTimePicker
    Friend WithEvents Chk_Lain As CheckBox
    Friend WithEvents Cmb_Lain As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Txt_ValueLain As TextBox
    Friend WithEvents Chk_HariIni As CheckBox
    Friend WithEvents Cmb_Lokasi As ComboBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label9 As Label
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents SaliinNoFakturToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BatalBarangMasukToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents BatalBarangMasukToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents BatalValidasiBarangMasukToolStripMenuItem As ToolStripMenuItem
End Class
