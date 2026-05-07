<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Laporan_Transaksi_Waste
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
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
		Me.Label11 = New System.Windows.Forms.Label()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.Rd_Status_Belum_Validasi = New System.Windows.Forms.RadioButton()
		Me.Rd_Status_Validasi = New System.Windows.Forms.RadioButton()
		Me.Rd_Status_Seluruh = New System.Windows.Forms.RadioButton()
		Me.Cmb_Jenis_Barang = New System.Windows.Forms.ComboBox()
		Me.Cmb_Jenis_Transaksi = New System.Windows.Forms.ComboBox()
		Me.Cmb_Jenis_Laporan = New System.Windows.Forms.ComboBox()
		Me.Cmb_Jenis = New System.Windows.Forms.ComboBox()
		Me.Txt_No_Transaksi = New System.Windows.Forms.TextBox()
		Me.Txt_No_Split = New System.Windows.Forms.TextBox()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Txt_Kd_Barang = New System.Windows.Forms.TextBox()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Txt_Keterangan_Split = New System.Windows.Forms.TextBox()
		Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Txt_Nm_Barang = New System.Windows.Forms.TextBox()
		Me.BtnCetak = New System.Windows.Forms.Button()
		Me.BtnExit = New System.Windows.Forms.Button()
		Me.Lv_Transaksi = New System.Windows.Forms.ListView()
		Me.Lv_Split = New System.Windows.Forms.ListView()
		Me.Lv_Barang = New System.Windows.Forms.ListView()
		Me.Panel1.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		Me.SuspendLayout()
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.PanelGradient1)
		Me.Panel1.Controls.Add(Me.Label11)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(792, 45)
		Me.Panel1.TabIndex = 91
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
		Me.PanelGradient1.Size = New System.Drawing.Size(792, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Label11
		'
		Me.Label11.AutoSize = True
		Me.Label11.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label11.Location = New System.Drawing.Point(20, 7)
		Me.Label11.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.Label11.Name = "Label11"
		Me.Label11.Size = New System.Drawing.Size(307, 29)
		Me.Label11.TabIndex = 0
		Me.Label11.Text = "Laporan - Pemusnhaan Waste"
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Location = New System.Drawing.Point(0, 53)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 601)
		Me.Panel3.TabIndex = 93
		Me.Panel3.Visible = False
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(773, 43)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(19, 601)
		Me.Panel2.TabIndex = 93
		Me.Panel2.Visible = False
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Location = New System.Drawing.Point(-3, 45)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(942, 12)
		Me.Panel5.TabIndex = 94
		Me.Panel5.Visible = False
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Location = New System.Drawing.Point(28, 329)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(942, 15)
		Me.Panel4.TabIndex = 94
		Me.Panel4.Visible = False
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.Rd_Status_Belum_Validasi)
		Me.GroupBox1.Controls.Add(Me.Rd_Status_Validasi)
		Me.GroupBox1.Controls.Add(Me.Rd_Status_Seluruh)
		Me.GroupBox1.Controls.Add(Me.Cmb_Jenis_Barang)
		Me.GroupBox1.Controls.Add(Me.Cmb_Jenis_Transaksi)
		Me.GroupBox1.Controls.Add(Me.Cmb_Jenis_Laporan)
		Me.GroupBox1.Controls.Add(Me.Cmb_Jenis)
		Me.GroupBox1.Controls.Add(Me.Txt_No_Transaksi)
		Me.GroupBox1.Controls.Add(Me.Txt_No_Split)
		Me.GroupBox1.Controls.Add(Me.Label5)
		Me.GroupBox1.Controls.Add(Me.Txt_Kd_Barang)
		Me.GroupBox1.Controls.Add(Me.Label7)
		Me.GroupBox1.Controls.Add(Me.Label1)
		Me.GroupBox1.Controls.Add(Me.Label8)
		Me.GroupBox1.Controls.Add(Me.Label4)
		Me.GroupBox1.Controls.Add(Me.Label6)
		Me.GroupBox1.Controls.Add(Me.Tgl2)
		Me.GroupBox1.Controls.Add(Me.Label3)
		Me.GroupBox1.Controls.Add(Me.Txt_Keterangan_Split)
		Me.GroupBox1.Controls.Add(Me.Tgl1)
		Me.GroupBox1.Controls.Add(Me.Label2)
		Me.GroupBox1.Controls.Add(Me.Txt_Nm_Barang)
		Me.GroupBox1.Location = New System.Drawing.Point(19, 55)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(752, 235)
		Me.GroupBox1.TabIndex = 0
		Me.GroupBox1.TabStop = False
		'
		'Rd_Status_Belum_Validasi
		'
		Me.Rd_Status_Belum_Validasi.AutoSize = True
		Me.Rd_Status_Belum_Validasi.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Rd_Status_Belum_Validasi.Location = New System.Drawing.Point(294, 83)
		Me.Rd_Status_Belum_Validasi.Name = "Rd_Status_Belum_Validasi"
		Me.Rd_Status_Belum_Validasi.Size = New System.Drawing.Size(102, 20)
		Me.Rd_Status_Belum_Validasi.TabIndex = 6
		Me.Rd_Status_Belum_Validasi.TabStop = True
		Me.Rd_Status_Belum_Validasi.Tag = "Belum Validasi"
		Me.Rd_Status_Belum_Validasi.Text = "Belum Validasi"
		Me.Rd_Status_Belum_Validasi.UseVisualStyleBackColor = True
		'
		'Rd_Status_Validasi
		'
		Me.Rd_Status_Validasi.AutoSize = True
		Me.Rd_Status_Validasi.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Rd_Status_Validasi.Location = New System.Drawing.Point(212, 83)
		Me.Rd_Status_Validasi.Name = "Rd_Status_Validasi"
		Me.Rd_Status_Validasi.Size = New System.Drawing.Size(65, 20)
		Me.Rd_Status_Validasi.TabIndex = 5
		Me.Rd_Status_Validasi.TabStop = True
		Me.Rd_Status_Validasi.Tag = "Sudah Validasi"
		Me.Rd_Status_Validasi.Text = "Validasi"
		Me.Rd_Status_Validasi.UseVisualStyleBackColor = True
		'
		'Rd_Status_Seluruh
		'
		Me.Rd_Status_Seluruh.AutoSize = True
		Me.Rd_Status_Seluruh.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Rd_Status_Seluruh.Location = New System.Drawing.Point(130, 83)
		Me.Rd_Status_Seluruh.Name = "Rd_Status_Seluruh"
		Me.Rd_Status_Seluruh.Size = New System.Drawing.Size(66, 20)
		Me.Rd_Status_Seluruh.TabIndex = 4
		Me.Rd_Status_Seluruh.TabStop = True
		Me.Rd_Status_Seluruh.Tag = "Seluruh"
		Me.Rd_Status_Seluruh.Text = "Seluruh"
		Me.Rd_Status_Seluruh.UseVisualStyleBackColor = True
		'
		'Cmb_Jenis_Barang
		'
		Me.Cmb_Jenis_Barang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Jenis_Barang.Enabled = False
		Me.Cmb_Jenis_Barang.FormattingEnabled = True
		Me.Cmb_Jenis_Barang.Location = New System.Drawing.Point(130, 199)
		Me.Cmb_Jenis_Barang.Name = "Cmb_Jenis_Barang"
		Me.Cmb_Jenis_Barang.Size = New System.Drawing.Size(163, 24)
		Me.Cmb_Jenis_Barang.TabIndex = 11
		'
		'Cmb_Jenis_Transaksi
		'
		Me.Cmb_Jenis_Transaksi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Jenis_Transaksi.Enabled = False
		Me.Cmb_Jenis_Transaksi.FormattingEnabled = True
		Me.Cmb_Jenis_Transaksi.Location = New System.Drawing.Point(130, 142)
		Me.Cmb_Jenis_Transaksi.Name = "Cmb_Jenis_Transaksi"
		Me.Cmb_Jenis_Transaksi.Size = New System.Drawing.Size(163, 24)
		Me.Cmb_Jenis_Transaksi.TabIndex = 8
		'
		'Cmb_Jenis_Laporan
		'
		Me.Cmb_Jenis_Laporan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Jenis_Laporan.FormattingEnabled = True
		Me.Cmb_Jenis_Laporan.Location = New System.Drawing.Point(130, 48)
		Me.Cmb_Jenis_Laporan.Name = "Cmb_Jenis_Laporan"
		Me.Cmb_Jenis_Laporan.Size = New System.Drawing.Size(374, 24)
		Me.Cmb_Jenis_Laporan.TabIndex = 3
		'
		'Cmb_Jenis
		'
		Me.Cmb_Jenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Jenis.Enabled = False
		Me.Cmb_Jenis.FormattingEnabled = True
		Me.Cmb_Jenis.Location = New System.Drawing.Point(130, 112)
		Me.Cmb_Jenis.Name = "Cmb_Jenis"
		Me.Cmb_Jenis.Size = New System.Drawing.Size(163, 24)
		Me.Cmb_Jenis.TabIndex = 7
		'
		'Txt_No_Transaksi
		'
		Me.Txt_No_Transaksi.Enabled = False
		Me.Txt_No_Transaksi.Location = New System.Drawing.Point(297, 146)
		Me.Txt_No_Transaksi.Name = "Txt_No_Transaksi"
		Me.Txt_No_Transaksi.Size = New System.Drawing.Size(207, 20)
		Me.Txt_No_Transaksi.TabIndex = 9
		'
		'Txt_No_Split
		'
		Me.Txt_No_Split.Enabled = False
		Me.Txt_No_Split.Location = New System.Drawing.Point(130, 173)
		Me.Txt_No_Split.Name = "Txt_No_Split"
		Me.Txt_No_Split.Size = New System.Drawing.Size(163, 20)
		Me.Txt_No_Split.TabIndex = 10
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(8, 145)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(76, 16)
		Me.Label5.TabIndex = 4
		Me.Label5.Text = "No Transaksi"
		'
		'Txt_Kd_Barang
		'
		Me.Txt_Kd_Barang.Enabled = False
		Me.Txt_Kd_Barang.Location = New System.Drawing.Point(297, 202)
		Me.Txt_Kd_Barang.Name = "Txt_Kd_Barang"
		Me.Txt_Kd_Barang.Size = New System.Drawing.Size(116, 20)
		Me.Txt_Kd_Barang.TabIndex = 12
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Location = New System.Drawing.Point(8, 51)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(83, 16)
		Me.Label7.TabIndex = 4
		Me.Label7.Text = "Jenis Laporan"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(8, 171)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(51, 16)
		Me.Label1.TabIndex = 4
		Me.Label1.Text = "No Split"
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Location = New System.Drawing.Point(8, 85)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(97, 16)
		Me.Label8.TabIndex = 4
		Me.Label8.Text = "Status Transaksi"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(8, 115)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(35, 16)
		Me.Label4.TabIndex = 4
		Me.Label4.Text = "Jenis"
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Location = New System.Drawing.Point(8, 197)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(43, 16)
		Me.Label6.TabIndex = 4
		Me.Label6.Text = "Barang"
		'
		'Tgl2
		'
		Me.Tgl2.CustomFormat = "dd MMMM yyyy"
		Me.Tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
		Me.Tgl2.Location = New System.Drawing.Point(341, 22)
		Me.Tgl2.Name = "Tgl2"
		Me.Tgl2.Size = New System.Drawing.Size(163, 20)
		Me.Tgl2.TabIndex = 2
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(303, 24)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(25, 16)
		Me.Label3.TabIndex = 2
		Me.Label3.Text = "s/d"
		'
		'Txt_Keterangan_Split
		'
		Me.Txt_Keterangan_Split.Enabled = False
		Me.Txt_Keterangan_Split.Location = New System.Drawing.Point(297, 173)
		Me.Txt_Keterangan_Split.Name = "Txt_Keterangan_Split"
		Me.Txt_Keterangan_Split.Size = New System.Drawing.Size(441, 20)
		Me.Txt_Keterangan_Split.TabIndex = 6
		'
		'Tgl1
		'
		Me.Tgl1.CustomFormat = "dd MMMM yyyy"
		Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
		Me.Tgl1.Location = New System.Drawing.Point(130, 22)
		Me.Tgl1.Name = "Tgl1"
		Me.Tgl1.Size = New System.Drawing.Size(163, 20)
		Me.Tgl1.TabIndex = 1
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(8, 23)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(47, 16)
		Me.Label2.TabIndex = 0
		Me.Label2.Text = "Periode"
		'
		'Txt_Nm_Barang
		'
		Me.Txt_Nm_Barang.Enabled = False
		Me.Txt_Nm_Barang.Location = New System.Drawing.Point(417, 202)
		Me.Txt_Nm_Barang.Name = "Txt_Nm_Barang"
		Me.Txt_Nm_Barang.Size = New System.Drawing.Size(321, 20)
		Me.Txt_Nm_Barang.TabIndex = 13
		'
		'BtnCetak
		'
		Me.BtnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.BtnCetak.Cursor = System.Windows.Forms.Cursors.Hand
		Me.BtnCetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.BtnCetak.ForeColor = System.Drawing.Color.White
		Me.BtnCetak.Location = New System.Drawing.Point(589, 296)
		Me.BtnCetak.Name = "BtnCetak"
		Me.BtnCetak.Size = New System.Drawing.Size(84, 33)
		Me.BtnCetak.TabIndex = 1
		Me.BtnCetak.Text = "&Cetak"
		Me.BtnCetak.UseVisualStyleBackColor = False
		'
		'BtnExit
		'
		Me.BtnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand
		Me.BtnExit.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.BtnExit.ForeColor = System.Drawing.Color.White
		Me.BtnExit.Location = New System.Drawing.Point(678, 296)
		Me.BtnExit.Name = "BtnExit"
		Me.BtnExit.Size = New System.Drawing.Size(84, 33)
		Me.BtnExit.TabIndex = 2
		Me.BtnExit.Text = "&Keluar"
		Me.BtnExit.UseVisualStyleBackColor = False
		'
		'Lv_Transaksi
		'
		Me.Lv_Transaksi.BackColor = System.Drawing.Color.White
		Me.Lv_Transaksi.FullRowSelect = True
		Me.Lv_Transaksi.GridLines = True
		Me.Lv_Transaksi.HideSelection = False
		Me.Lv_Transaksi.Location = New System.Drawing.Point(800, 224)
		Me.Lv_Transaksi.Name = "Lv_Transaksi"
		Me.Lv_Transaksi.Size = New System.Drawing.Size(608, 211)
		Me.Lv_Transaksi.TabIndex = 98
		Me.Lv_Transaksi.UseCompatibleStateImageBehavior = False
		Me.Lv_Transaksi.View = System.Windows.Forms.View.Details
		Me.Lv_Transaksi.Visible = False
		'
		'Lv_Split
		'
		Me.Lv_Split.BackColor = System.Drawing.Color.White
		Me.Lv_Split.FullRowSelect = True
		Me.Lv_Split.GridLines = True
		Me.Lv_Split.HideSelection = False
		Me.Lv_Split.Location = New System.Drawing.Point(800, 250)
		Me.Lv_Split.Name = "Lv_Split"
		Me.Lv_Split.Size = New System.Drawing.Size(608, 211)
		Me.Lv_Split.TabIndex = 98
		Me.Lv_Split.UseCompatibleStateImageBehavior = False
		Me.Lv_Split.View = System.Windows.Forms.View.Details
		Me.Lv_Split.Visible = False
		'
		'Lv_Barang
		'
		Me.Lv_Barang.BackColor = System.Drawing.Color.White
		Me.Lv_Barang.FullRowSelect = True
		Me.Lv_Barang.GridLines = True
		Me.Lv_Barang.HideSelection = False
		Me.Lv_Barang.Location = New System.Drawing.Point(800, 280)
		Me.Lv_Barang.Name = "Lv_Barang"
		Me.Lv_Barang.Size = New System.Drawing.Size(608, 211)
		Me.Lv_Barang.TabIndex = 98
		Me.Lv_Barang.UseCompatibleStateImageBehavior = False
		Me.Lv_Barang.View = System.Windows.Forms.View.Details
		Me.Lv_Barang.Visible = False
		'
		'N_EMI_Laporan_Transaksi_Waste
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(792, 344)
		Me.Controls.Add(Me.Lv_Barang)
		Me.Controls.Add(Me.Lv_Split)
		Me.Controls.Add(Me.Lv_Transaksi)
		Me.Controls.Add(Me.BtnCetak)
		Me.Controls.Add(Me.BtnExit)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Panel5)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel1)
		Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.Name = "N_EMI_Laporan_Transaksi_Waste"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label11 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Cmb_Jenis_Barang As ComboBox
    Friend WithEvents Cmb_Jenis As ComboBox
    Friend WithEvents Txt_No_Transaksi As TextBox
    Friend WithEvents Txt_No_Split As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Txt_Kd_Barang As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Tgl2 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_Nm_Barang As TextBox
    Friend WithEvents BtnCetak As Button
    Friend WithEvents BtnExit As Button
    Friend WithEvents Txt_Keterangan_Split As TextBox
    Friend WithEvents Lv_Transaksi As ListView
    Friend WithEvents Lv_Split As ListView
    Friend WithEvents Lv_Barang As ListView
    Friend WithEvents Cmb_Jenis_Transaksi As ComboBox
    Friend WithEvents Cmb_Jenis_Laporan As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Rd_Status_Seluruh As RadioButton
    Friend WithEvents Label8 As Label
    Friend WithEvents Rd_Status_Belum_Validasi As RadioButton
    Friend WithEvents Rd_Status_Validasi As RadioButton
End Class
