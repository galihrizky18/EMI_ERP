<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Laporan_Validasi_GR_3
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Cmb_Lokasi_Tujuan = New System.Windows.Forms.ComboBox()
        Me.Cmb_Lokasi_Awal = New System.Windows.Forms.ComboBox()
        Me.Cmb_ParamLain = New System.Windows.Forms.ComboBox()
        Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_NoSplit = New System.Windows.Forms.TextBox()
        Me.Txt_NoTransaksi = New System.Windows.Forms.TextBox()
        Me.Txt_ParamLain = New System.Windows.Forms.TextBox()
        Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
        Me.BtnCetak = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
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
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1148, 43)
        Me.Panel1.TabIndex = 30
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1148, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(13, 7)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(435, 28)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Laporan - Validasi Good Received Warehouse"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 52)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 601)
        Me.Panel3.TabIndex = 46
        Me.Panel3.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(612, 49)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(19, 601)
        Me.Panel2.TabIndex = 46
        Me.Panel2.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(0, 42)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(942, 12)
        Me.Panel4.TabIndex = 47
        Me.Panel4.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Cmb_Lokasi_Tujuan)
        Me.GroupBox1.Controls.Add(Me.Cmb_Lokasi_Awal)
        Me.GroupBox1.Controls.Add(Me.Cmb_ParamLain)
        Me.GroupBox1.Controls.Add(Me.Txt_KdBarang)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Tgl2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Tgl1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Txt_NoSplit)
        Me.GroupBox1.Controls.Add(Me.Txt_NoTransaksi)
        Me.GroupBox1.Controls.Add(Me.Txt_ParamLain)
        Me.GroupBox1.Controls.Add(Me.Txt_NmBarang)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 55)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(592, 192)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Cmb_Lokasi_Tujuan
        '
        Me.Cmb_Lokasi_Tujuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lokasi_Tujuan.FormattingEnabled = True
        Me.Cmb_Lokasi_Tujuan.Location = New System.Drawing.Point(361, 100)
        Me.Cmb_Lokasi_Tujuan.Name = "Cmb_Lokasi_Tujuan"
        Me.Cmb_Lokasi_Tujuan.Size = New System.Drawing.Size(163, 24)
        Me.Cmb_Lokasi_Tujuan.TabIndex = 5
        '
        'Cmb_Lokasi_Awal
        '
        Me.Cmb_Lokasi_Awal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lokasi_Awal.FormattingEnabled = True
        Me.Cmb_Lokasi_Awal.Location = New System.Drawing.Point(94, 100)
        Me.Cmb_Lokasi_Awal.Name = "Cmb_Lokasi_Awal"
        Me.Cmb_Lokasi_Awal.Size = New System.Drawing.Size(163, 24)
        Me.Cmb_Lokasi_Awal.TabIndex = 4
        '
        'Cmb_ParamLain
        '
        Me.Cmb_ParamLain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_ParamLain.FormattingEnabled = True
        Me.Cmb_ParamLain.Location = New System.Drawing.Point(94, 156)
        Me.Cmb_ParamLain.Name = "Cmb_ParamLain"
        Me.Cmb_ParamLain.Size = New System.Drawing.Size(163, 24)
        Me.Cmb_ParamLain.TabIndex = 8
        '
        'Txt_KdBarang
        '
        Me.Txt_KdBarang.Location = New System.Drawing.Point(94, 130)
        Me.Txt_KdBarang.Name = "Txt_KdBarang"
        Me.Txt_KdBarang.Size = New System.Drawing.Size(163, 20)
        Me.Txt_KdBarang.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 77)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 16)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "No Split"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(275, 103)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(82, 16)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "Lokasi Tujuan"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 16)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "No Transaksi"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 103)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(71, 16)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Lokasi Awal"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 162)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 16)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Lainnya"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 133)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 16)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Barang"
        '
        'Tgl2
        '
        Me.Tgl2.CustomFormat = "dd MMMM yyyy"
        Me.Tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl2.Location = New System.Drawing.Point(305, 22)
        Me.Tgl2.Name = "Tgl2"
        Me.Tgl2.Size = New System.Drawing.Size(163, 20)
        Me.Tgl2.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(267, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "s/d"
        '
        'Tgl1
        '
        Me.Tgl1.CustomFormat = "dd MMMM yyyy"
        Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl1.Location = New System.Drawing.Point(94, 22)
        Me.Tgl1.Name = "Tgl1"
        Me.Tgl1.Size = New System.Drawing.Size(163, 20)
        Me.Tgl1.TabIndex = 0
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
        'Txt_NoSplit
        '
        Me.Txt_NoSplit.Location = New System.Drawing.Point(94, 74)
        Me.Txt_NoSplit.Name = "Txt_NoSplit"
        Me.Txt_NoSplit.Size = New System.Drawing.Size(163, 20)
        Me.Txt_NoSplit.TabIndex = 3
        '
        'Txt_NoTransaksi
        '
        Me.Txt_NoTransaksi.Location = New System.Drawing.Point(94, 48)
        Me.Txt_NoTransaksi.Name = "Txt_NoTransaksi"
        Me.Txt_NoTransaksi.Size = New System.Drawing.Size(163, 20)
        Me.Txt_NoTransaksi.TabIndex = 2
        '
        'Txt_ParamLain
        '
        Me.Txt_ParamLain.Enabled = False
        Me.Txt_ParamLain.Location = New System.Drawing.Point(263, 159)
        Me.Txt_ParamLain.Name = "Txt_ParamLain"
        Me.Txt_ParamLain.Size = New System.Drawing.Size(319, 20)
        Me.Txt_ParamLain.TabIndex = 9
        '
        'Txt_NmBarang
        '
        Me.Txt_NmBarang.Location = New System.Drawing.Point(263, 130)
        Me.Txt_NmBarang.Name = "Txt_NmBarang"
        Me.Txt_NmBarang.Size = New System.Drawing.Size(319, 20)
        Me.Txt_NmBarang.TabIndex = 7
        '
        'BtnCetak
        '
        Me.BtnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnCetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnCetak.ForeColor = System.Drawing.Color.White
        Me.BtnCetak.Location = New System.Drawing.Point(445, 253)
        Me.BtnCetak.Name = "BtnCetak"
        Me.BtnCetak.Size = New System.Drawing.Size(84, 33)
        Me.BtnCetak.TabIndex = 1
        Me.BtnCetak.Text = "&Cetak"
        Me.BtnCetak.UseVisualStyleBackColor = False
        '
        'BtnExit
        '
        Me.BtnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnExit.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnExit.ForeColor = System.Drawing.Color.White
        Me.BtnExit.Location = New System.Drawing.Point(528, 253)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(84, 33)
        Me.BtnExit.TabIndex = 50
        Me.BtnExit.Text = "&Keluar"
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(21, 286)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(942, 15)
        Me.Panel5.TabIndex = 47
        Me.Panel5.Visible = False
        '
        'Lv_Transaksi
        '
        Me.Lv_Transaksi.BackColor = System.Drawing.Color.White
        Me.Lv_Transaksi.FullRowSelect = True
        Me.Lv_Transaksi.GridLines = True
        Me.Lv_Transaksi.HideSelection = False
        Me.Lv_Transaksi.Location = New System.Drawing.Point(650, 125)
        Me.Lv_Transaksi.Name = "Lv_Transaksi"
        Me.Lv_Transaksi.Size = New System.Drawing.Size(485, 200)
        Me.Lv_Transaksi.TabIndex = 51
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
        Me.Lv_Split.Location = New System.Drawing.Point(650, 150)
        Me.Lv_Split.Name = "Lv_Split"
        Me.Lv_Split.Size = New System.Drawing.Size(485, 200)
        Me.Lv_Split.TabIndex = 51
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
        Me.Lv_Barang.Location = New System.Drawing.Point(650, 207)
        Me.Lv_Barang.Name = "Lv_Barang"
        Me.Lv_Barang.Size = New System.Drawing.Size(485, 200)
        Me.Lv_Barang.TabIndex = 51
        Me.Lv_Barang.UseCompatibleStateImageBehavior = False
        Me.Lv_Barang.View = System.Windows.Forms.View.Details
        Me.Lv_Barang.Visible = False
        '
        'N_EMI_Laporan_Validasi_GR_3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1148, 431)
        Me.Controls.Add(Me.Lv_Barang)
        Me.Controls.Add(Me.Lv_Split)
        Me.Controls.Add(Me.Lv_Transaksi)
        Me.Controls.Add(Me.BtnCetak)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_Laporan_Validasi_GR_3"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Cmb_ParamLain As ComboBox
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Tgl2 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_ParamLain As TextBox
    Friend WithEvents Txt_NmBarang As TextBox
    Friend WithEvents BtnCetak As Button
    Friend WithEvents BtnExit As Button
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Txt_NoTransaksi As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Txt_NoSplit As TextBox
    Friend WithEvents Cmb_Lokasi_Tujuan As ComboBox
    Friend WithEvents Cmb_Lokasi_Awal As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Lv_Transaksi As ListView
    Friend WithEvents Lv_Split As ListView
    Friend WithEvents Lv_Barang As ListView
End Class
