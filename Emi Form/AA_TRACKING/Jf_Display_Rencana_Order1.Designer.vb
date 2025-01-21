<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Jf_Display_Rencana_Order1
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Jf_Display_Rencana_Order1))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LvRencanaOrder = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.UpdateTrackingPengirimanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PembatalanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RencanaOrderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusOTWToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TrackingDokumenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DraftToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FinalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KirimToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DokDiterimaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KapalTibaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PenjaluranToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SPPBToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TarikKontainerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BongkarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RencanaOrderGabunganToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.CmbSelesai = New System.Windows.Forms.ComboBox()
        Me.ChkTransaksiHariIni = New System.Windows.Forms.CheckBox()
        Me.BtCari = New System.Windows.Forms.Button()
        Me.TxtValue = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CmbParameterLain = New System.Windows.Forms.ComboBox()
        Me.ChkParameterLain = New System.Windows.Forms.CheckBox()
        Me.DtpTgl2 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DtpTgl1 = New System.Windows.Forms.DateTimePicker()
        Me.ChkTgl = New System.Windows.Forms.CheckBox()
        Me.CmbTgl = New System.Windows.Forms.ComboBox()
        Me.CmbTracking = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CmbLokasi = New System.Windows.Forms.ComboBox()
        Me.ComboBox4 = New System.Windows.Forms.ComboBox()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ListView3 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ListView1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.LvDetailRencanaOrder = New System.Windows.Forms.ListView()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage9 = New System.Windows.Forms.TabPage()
        Me.LvGabungan = New System.Windows.Forms.ListView()
        Me.UpdateTidakPakaiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.ListView1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage9.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LvRencanaOrder)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 58)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox1.Size = New System.Drawing.Size(829, 245)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Data Rencana Order"
        '
        'LvRencanaOrder
        '
        Me.LvRencanaOrder.AllowDrop = True
        Me.LvRencanaOrder.ContextMenuStrip = Me.ContextMenuStrip1
        Me.LvRencanaOrder.FullRowSelect = True
        Me.LvRencanaOrder.GridLines = True
        Me.LvRencanaOrder.HideSelection = False
        Me.LvRencanaOrder.Location = New System.Drawing.Point(4, 13)
        Me.LvRencanaOrder.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.LvRencanaOrder.Name = "LvRencanaOrder"
        Me.LvRencanaOrder.Size = New System.Drawing.Size(821, 226)
        Me.LvRencanaOrder.TabIndex = 12
        Me.LvRencanaOrder.UseCompatibleStateImageBehavior = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UpdateTrackingPengirimanToolStripMenuItem, Me.PembatalanToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(225, 48)
        '
        'UpdateTrackingPengirimanToolStripMenuItem
        '
        Me.UpdateTrackingPengirimanToolStripMenuItem.Name = "UpdateTrackingPengirimanToolStripMenuItem"
        Me.UpdateTrackingPengirimanToolStripMenuItem.Size = New System.Drawing.Size(224, 22)
        Me.UpdateTrackingPengirimanToolStripMenuItem.Text = "Update Tracking Pengiriman"
        '
        'PembatalanToolStripMenuItem
        '
        Me.PembatalanToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RencanaOrderToolStripMenuItem, Me.StatusOTWToolStripMenuItem, Me.TrackingDokumenToolStripMenuItem, Me.KapalTibaToolStripMenuItem, Me.PenjaluranToolStripMenuItem, Me.SPPBToolStripMenuItem, Me.TarikKontainerToolStripMenuItem, Me.BongkarToolStripMenuItem, Me.RencanaOrderGabunganToolStripMenuItem})
        Me.PembatalanToolStripMenuItem.Name = "PembatalanToolStripMenuItem"
        Me.PembatalanToolStripMenuItem.Size = New System.Drawing.Size(224, 22)
        Me.PembatalanToolStripMenuItem.Text = "Pembatalan"
        '
        'RencanaOrderToolStripMenuItem
        '
        Me.RencanaOrderToolStripMenuItem.Name = "RencanaOrderToolStripMenuItem"
        Me.RencanaOrderToolStripMenuItem.Size = New System.Drawing.Size(210, 22)
        Me.RencanaOrderToolStripMenuItem.Text = "Rencana Order"
        '
        'StatusOTWToolStripMenuItem
        '
        Me.StatusOTWToolStripMenuItem.Name = "StatusOTWToolStripMenuItem"
        Me.StatusOTWToolStripMenuItem.Size = New System.Drawing.Size(210, 22)
        Me.StatusOTWToolStripMenuItem.Text = "Status OTW"
        '
        'TrackingDokumenToolStripMenuItem
        '
        Me.TrackingDokumenToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DraftToolStripMenuItem, Me.FinalToolStripMenuItem, Me.KirimToolStripMenuItem, Me.DokDiterimaToolStripMenuItem})
        Me.TrackingDokumenToolStripMenuItem.Name = "TrackingDokumenToolStripMenuItem"
        Me.TrackingDokumenToolStripMenuItem.Size = New System.Drawing.Size(210, 22)
        Me.TrackingDokumenToolStripMenuItem.Text = "Tracking Dokumen"
        '
        'DraftToolStripMenuItem
        '
        Me.DraftToolStripMenuItem.Name = "DraftToolStripMenuItem"
        Me.DraftToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.DraftToolStripMenuItem.Text = "Draft"
        '
        'FinalToolStripMenuItem
        '
        Me.FinalToolStripMenuItem.Name = "FinalToolStripMenuItem"
        Me.FinalToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.FinalToolStripMenuItem.Text = "Final"
        '
        'KirimToolStripMenuItem
        '
        Me.KirimToolStripMenuItem.Name = "KirimToolStripMenuItem"
        Me.KirimToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.KirimToolStripMenuItem.Text = "Kirim"
        '
        'DokDiterimaToolStripMenuItem
        '
        Me.DokDiterimaToolStripMenuItem.Name = "DokDiterimaToolStripMenuItem"
        Me.DokDiterimaToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.DokDiterimaToolStripMenuItem.Text = "Dok Diterima"
        '
        'KapalTibaToolStripMenuItem
        '
        Me.KapalTibaToolStripMenuItem.Name = "KapalTibaToolStripMenuItem"
        Me.KapalTibaToolStripMenuItem.Size = New System.Drawing.Size(210, 22)
        Me.KapalTibaToolStripMenuItem.Text = "Kapal Tiba"
        '
        'PenjaluranToolStripMenuItem
        '
        Me.PenjaluranToolStripMenuItem.Name = "PenjaluranToolStripMenuItem"
        Me.PenjaluranToolStripMenuItem.Size = New System.Drawing.Size(210, 22)
        Me.PenjaluranToolStripMenuItem.Text = "Penjaluran"
        '
        'SPPBToolStripMenuItem
        '
        Me.SPPBToolStripMenuItem.Name = "SPPBToolStripMenuItem"
        Me.SPPBToolStripMenuItem.Size = New System.Drawing.Size(210, 22)
        Me.SPPBToolStripMenuItem.Text = "SPPB"
        '
        'TarikKontainerToolStripMenuItem
        '
        Me.TarikKontainerToolStripMenuItem.Name = "TarikKontainerToolStripMenuItem"
        Me.TarikKontainerToolStripMenuItem.Size = New System.Drawing.Size(210, 22)
        Me.TarikKontainerToolStripMenuItem.Text = "Tarik Kontainer"
        '
        'BongkarToolStripMenuItem
        '
        Me.BongkarToolStripMenuItem.Name = "BongkarToolStripMenuItem"
        Me.BongkarToolStripMenuItem.Size = New System.Drawing.Size(210, 22)
        Me.BongkarToolStripMenuItem.Text = "Bongkar"
        '
        'RencanaOrderGabunganToolStripMenuItem
        '
        Me.RencanaOrderGabunganToolStripMenuItem.Name = "RencanaOrderGabunganToolStripMenuItem"
        Me.RencanaOrderGabunganToolStripMenuItem.Size = New System.Drawing.Size(210, 22)
        Me.RencanaOrderGabunganToolStripMenuItem.Text = "Rencana Order Gabungan"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.CmbSelesai)
        Me.GroupBox3.Controls.Add(Me.ChkTransaksiHariIni)
        Me.GroupBox3.Controls.Add(Me.BtCari)
        Me.GroupBox3.Controls.Add(Me.TxtValue)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.CmbParameterLain)
        Me.GroupBox3.Controls.Add(Me.ChkParameterLain)
        Me.GroupBox3.Controls.Add(Me.DtpTgl2)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.DtpTgl1)
        Me.GroupBox3.Controls.Add(Me.ChkTgl)
        Me.GroupBox3.Controls.Add(Me.CmbTgl)
        Me.GroupBox3.Location = New System.Drawing.Point(9, 567)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox3.Size = New System.Drawing.Size(553, 105)
        Me.GroupBox3.TabIndex = 25
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Filter Data"
        Me.GroupBox3.Visible = False
        '
        'CmbSelesai
        '
        Me.CmbSelesai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSelesai.FormattingEnabled = True
        Me.CmbSelesai.Location = New System.Drawing.Point(392, 82)
        Me.CmbSelesai.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.CmbSelesai.Name = "CmbSelesai"
        Me.CmbSelesai.Size = New System.Drawing.Size(87, 19)
        Me.CmbSelesai.TabIndex = 14
        '
        'ChkTransaksiHariIni
        '
        Me.ChkTransaksiHariIni.AutoSize = True
        Me.ChkTransaksiHariIni.Location = New System.Drawing.Point(5, 35)
        Me.ChkTransaksiHariIni.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ChkTransaksiHariIni.Name = "ChkTransaksiHariIni"
        Me.ChkTransaksiHariIni.Size = New System.Drawing.Size(102, 16)
        Me.ChkTransaksiHariIni.TabIndex = 9
        Me.ChkTransaksiHariIni.Text = "Transaksi Hari Ini"
        Me.ChkTransaksiHariIni.UseVisualStyleBackColor = True
        '
        'BtCari
        '
        Me.BtCari.Location = New System.Drawing.Point(482, 81)
        Me.BtCari.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.BtCari.Name = "BtCari"
        Me.BtCari.Size = New System.Drawing.Size(64, 21)
        Me.BtCari.TabIndex = 8
        Me.BtCari.Text = "&Cari"
        Me.BtCari.UseVisualStyleBackColor = True
        '
        'TxtValue
        '
        Me.TxtValue.Location = New System.Drawing.Point(253, 82)
        Me.TxtValue.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TxtValue.Name = "TxtValue"
        Me.TxtValue.Size = New System.Drawing.Size(135, 19)
        Me.TxtValue.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(219, 84)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Value"
        '
        'CmbParameterLain
        '
        Me.CmbParameterLain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbParameterLain.FormattingEnabled = True
        Me.CmbParameterLain.Location = New System.Drawing.Point(117, 82)
        Me.CmbParameterLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.CmbParameterLain.Name = "CmbParameterLain"
        Me.CmbParameterLain.Size = New System.Drawing.Size(97, 19)
        Me.CmbParameterLain.TabIndex = 6
        '
        'ChkParameterLain
        '
        Me.ChkParameterLain.AutoSize = True
        Me.ChkParameterLain.Location = New System.Drawing.Point(5, 84)
        Me.ChkParameterLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ChkParameterLain.Name = "ChkParameterLain"
        Me.ChkParameterLain.Size = New System.Drawing.Size(90, 16)
        Me.ChkParameterLain.TabIndex = 5
        Me.ChkParameterLain.Text = "Parameter Lain"
        Me.ChkParameterLain.UseVisualStyleBackColor = True
        '
        'DtpTgl2
        '
        Me.DtpTgl2.CustomFormat = "dd MMMM yyyy"
        Me.DtpTgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpTgl2.Location = New System.Drawing.Point(387, 58)
        Me.DtpTgl2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DtpTgl2.Name = "DtpTgl2"
        Me.DtpTgl2.Size = New System.Drawing.Size(158, 19)
        Me.DtpTgl2.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(365, 61)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(19, 12)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "s/d"
        '
        'DtpTgl1
        '
        Me.DtpTgl1.CustomFormat = "dd MMMM yyyy"
        Me.DtpTgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpTgl1.Location = New System.Drawing.Point(202, 58)
        Me.DtpTgl1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DtpTgl1.Name = "DtpTgl1"
        Me.DtpTgl1.Size = New System.Drawing.Size(158, 19)
        Me.DtpTgl1.TabIndex = 3
        '
        'ChkTgl
        '
        Me.ChkTgl.AutoSize = True
        Me.ChkTgl.Location = New System.Drawing.Point(5, 59)
        Me.ChkTgl.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ChkTgl.Name = "ChkTgl"
        Me.ChkTgl.Size = New System.Drawing.Size(108, 16)
        Me.ChkTgl.TabIndex = 1
        Me.ChkTgl.Text = "Parameter Tanggal"
        Me.ChkTgl.UseVisualStyleBackColor = True
        '
        'CmbTgl
        '
        Me.CmbTgl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbTgl.FormattingEnabled = True
        Me.CmbTgl.Location = New System.Drawing.Point(117, 58)
        Me.CmbTgl.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.CmbTgl.Name = "CmbTgl"
        Me.CmbTgl.Size = New System.Drawing.Size(81, 19)
        Me.CmbTgl.TabIndex = 2
        '
        'CmbTracking
        '
        Me.CmbTracking.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbTracking.FormattingEnabled = True
        Me.CmbTracking.Location = New System.Drawing.Point(849, 340)
        Me.CmbTracking.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.CmbTracking.Name = "CmbTracking"
        Me.CmbTracking.Size = New System.Drawing.Size(141, 19)
        Me.CmbTracking.TabIndex = 17
        Me.CmbTracking.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(847, 325)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(98, 12)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Tracking Pengiriman"
        Me.Label6.Visible = False
        '
        'CmbLokasi
        '
        Me.CmbLokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbLokasi.FormattingEnabled = True
        Me.CmbLokasi.Location = New System.Drawing.Point(12, 35)
        Me.CmbLokasi.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.CmbLokasi.Name = "CmbLokasi"
        Me.CmbLokasi.Size = New System.Drawing.Size(232, 19)
        Me.CmbLokasi.TabIndex = 15
        '
        'ComboBox4
        '
        Me.ComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Location = New System.Drawing.Point(976, 194)
        Me.ComboBox4.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(68, 19)
        Me.ComboBox4.TabIndex = 10
        Me.ComboBox4.Visible = False
        '
        'ComboBox3
        '
        Me.ComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Location = New System.Drawing.Point(980, 157)
        Me.ComboBox3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(129, 19)
        Me.ComboBox3.TabIndex = 9
        Me.ComboBox3.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(978, 179)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 12)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Tipe Pengurutan"
        Me.Label4.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(978, 144)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 12)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Kolom Pengurutan"
        Me.Label5.Visible = False
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(1046, 411)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(62, 19)
        Me.Button2.TabIndex = 11
        Me.Button2.Text = "E&xit"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'ListView3
        '
        Me.ListView3.BackColor = System.Drawing.Color.Snow
        Me.ListView3.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader4})
        Me.ListView3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ListView3.FullRowSelect = True
        Me.ListView3.GridLines = True
        Me.ListView3.HideSelection = False
        Me.ListView3.Location = New System.Drawing.Point(980, 12)
        Me.ListView3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ListView3.Name = "ListView3"
        Me.ListView3.Size = New System.Drawing.Size(224, 129)
        Me.ListView3.TabIndex = 65
        Me.ListView3.UseCompatibleStateImageBehavior = False
        Me.ListView3.View = System.Windows.Forms.View.Details
        Me.ListView3.Visible = False
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "#"
        Me.ColumnHeader1.Width = 0
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Kode Barang"
        Me.ColumnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader2.Width = 196
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Jumlah"
        Me.ColumnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader4.Width = 49
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Maroon
        Me.Label1.Location = New System.Drawing.Point(5, -3)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.Label1.Size = New System.Drawing.Size(829, 33)
        Me.Label1.TabIndex = 69
        Me.Label1.Text = "PO Dalam Perjalanan"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ListView1
        '
        Me.ListView1.Controls.Add(Me.TabPage1)
        Me.ListView1.Controls.Add(Me.TabPage5)
        Me.ListView1.Controls.Add(Me.TabPage9)
        Me.ListView1.Location = New System.Drawing.Point(5, 307)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.SelectedIndex = 0
        Me.ListView1.Size = New System.Drawing.Size(829, 261)
        Me.ListView1.TabIndex = 70
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.LvDetailRencanaOrder)
        Me.TabPage1.Location = New System.Drawing.Point(4, 20)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(821, 341)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Detail Barang"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'LvDetailRencanaOrder
        '
        Me.LvDetailRencanaOrder.FullRowSelect = True
        Me.LvDetailRencanaOrder.GridLines = True
        Me.LvDetailRencanaOrder.HideSelection = False
        Me.LvDetailRencanaOrder.Location = New System.Drawing.Point(2, 2)
        Me.LvDetailRencanaOrder.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.LvDetailRencanaOrder.Name = "LvDetailRencanaOrder"
        Me.LvDetailRencanaOrder.Size = New System.Drawing.Size(817, 232)
        Me.LvDetailRencanaOrder.TabIndex = 14
        Me.LvDetailRencanaOrder.UseCompatibleStateImageBehavior = False
        Me.LvDetailRencanaOrder.View = System.Windows.Forms.View.Details
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.DataGridView1)
        Me.TabPage5.Location = New System.Drawing.Point(4, 20)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(821, 237)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Tracking Pengiriman"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.HighlightText
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.ColumnHeadersVisible = False
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2})
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DataGridView1.GridColor = System.Drawing.Color.Gainsboro
        Me.DataGridView1.Location = New System.Drawing.Point(4, 1)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridView1.RowTemplate.Height = 40
        Me.DataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DataGridView1.ShowCellErrors = False
        Me.DataGridView1.ShowCellToolTips = False
        Me.DataGridView1.ShowEditingIcon = False
        Me.DataGridView1.ShowRowErrors = False
        Me.DataGridView1.Size = New System.Drawing.Size(817, 232)
        Me.DataGridView1.TabIndex = 0
        '
        'Column1
        '
        Me.Column1.HeaderText = "Keterangan"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 768
        '
        'Column2
        '
        Me.Column2.HeaderText = "Status"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Visible = False
        Me.Column2.Width = 120
        '
        'TabPage9
        '
        Me.TabPage9.Controls.Add(Me.LvGabungan)
        Me.TabPage9.Location = New System.Drawing.Point(4, 20)
        Me.TabPage9.Name = "TabPage9"
        Me.TabPage9.Size = New System.Drawing.Size(821, 362)
        Me.TabPage9.TabIndex = 8
        Me.TabPage9.Text = "Rencana Order Gabungan"
        Me.TabPage9.UseVisualStyleBackColor = True
        '
        'LvGabungan
        '
        Me.LvGabungan.FullRowSelect = True
        Me.LvGabungan.GridLines = True
        Me.LvGabungan.HideSelection = False
        Me.LvGabungan.Location = New System.Drawing.Point(2, 2)
        Me.LvGabungan.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.LvGabungan.Name = "LvGabungan"
        Me.LvGabungan.Size = New System.Drawing.Size(817, 232)
        Me.LvGabungan.TabIndex = 15
        Me.LvGabungan.UseCompatibleStateImageBehavior = False
        Me.LvGabungan.View = System.Windows.Forms.View.Details
        '
        'UpdateTidakPakaiToolStripMenuItem
        '
        Me.UpdateTidakPakaiToolStripMenuItem.Name = "UpdateTidakPakaiToolStripMenuItem"
        Me.UpdateTidakPakaiToolStripMenuItem.Size = New System.Drawing.Size(258, 22)
        Me.UpdateTidakPakaiToolStripMenuItem.Text = "Update Tidak Pakai"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.LightGray
        Me.Button1.Location = New System.Drawing.Point(249, 33)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 71
        Me.Button1.Text = "Seluruh"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.Tan
        Me.Button3.Location = New System.Drawing.Point(328, 33)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(89, 23)
        Me.Button3.TabIndex = 72
        Me.Button3.Text = "Loading Barang"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.Tan
        Me.Button4.Location = New System.Drawing.Point(420, 33)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(53, 23)
        Me.Button4.TabIndex = 73
        Me.Button4.Text = "OTW"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(849, 290)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(104, 23)
        Me.Button5.TabIndex = 74
        Me.Button5.Text = "Tracking Dokumen"
        Me.Button5.UseVisualStyleBackColor = True
        Me.Button5.Visible = False
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.Tan
        Me.Button6.Location = New System.Drawing.Point(476, 33)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(65, 23)
        Me.Button6.TabIndex = 75
        Me.Button6.Text = "Kapal Tiba"
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.Color.Tan
        Me.Button7.Location = New System.Drawing.Point(544, 33)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(65, 23)
        Me.Button7.TabIndex = 76
        Me.Button7.Text = "Penjaluran"
        Me.Button7.UseVisualStyleBackColor = False
        '
        'Button8
        '
        Me.Button8.BackColor = System.Drawing.Color.Tan
        Me.Button8.Location = New System.Drawing.Point(611, 33)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(65, 23)
        Me.Button8.TabIndex = 77
        Me.Button8.Text = "SPPB"
        Me.Button8.UseVisualStyleBackColor = False
        '
        'Button9
        '
        Me.Button9.BackColor = System.Drawing.Color.Tan
        Me.Button9.Location = New System.Drawing.Point(678, 33)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(83, 23)
        Me.Button9.TabIndex = 78
        Me.Button9.Text = "Tarik Kontainer"
        Me.Button9.UseVisualStyleBackColor = False
        '
        'Button10
        '
        Me.Button10.BackColor = System.Drawing.Color.Tan
        Me.Button10.Location = New System.Drawing.Point(762, 33)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(72, 23)
        Me.Button10.TabIndex = 79
        Me.Button10.Text = "Bongkar"
        Me.Button10.UseVisualStyleBackColor = False
        '
        'Jf_Display_Rencana_Order1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(5.0!, 11.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.ClientSize = New System.Drawing.Size(832, 571)
        Me.Controls.Add(Me.Button10)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CmbTracking)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.CmbLokasi)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.ListView3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBox4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ComboBox3)
        Me.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Name = "Jf_Display_Rencana_Order1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ".:: PO Dalam Perjalanan ::."
        Me.GroupBox1.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ListView1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage9.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents LvRencanaOrder As System.Windows.Forms.ListView
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents BtCari As System.Windows.Forms.Button
    Friend WithEvents TxtValue As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CmbParameterLain As System.Windows.Forms.ComboBox
    Friend WithEvents ChkParameterLain As System.Windows.Forms.CheckBox
    Friend WithEvents DtpTgl2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DtpTgl1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents ChkTgl As System.Windows.Forms.CheckBox
    Friend WithEvents CmbTgl As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox4 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox3 As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ChkTransaksiHariIni As System.Windows.Forms.CheckBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ListView3 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents CmbSelesai As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CmbLokasi As System.Windows.Forms.ComboBox
    Friend WithEvents ListView1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents LvDetailRencanaOrder As System.Windows.Forms.ListView
    Friend WithEvents TabPage9 As System.Windows.Forms.TabPage
    Friend WithEvents LvGabungan As System.Windows.Forms.ListView
    Friend WithEvents UpdateTidakPakaiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UpdateTrackingPengirimanToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PembatalanToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RencanaOrderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusOTWToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TrackingDokumenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KapalTibaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PenjaluranToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SPPBToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TarikKontainerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BongkarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RencanaOrderGabunganToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DraftToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FinalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KirimToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DokDiterimaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CmbTracking As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button10 As System.Windows.Forms.Button
End Class
