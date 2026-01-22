<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Laporan_Retur_Packaging
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
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Cmb_Jenis_Barang = New System.Windows.Forms.ComboBox()
        Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
        Me.Txt_No_RM = New System.Windows.Forms.TextBox()
        Me.Txt_No_Transaksi = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Txt_No_Split = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Txt_Kd_Barang = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txt_Ket_RM = New System.Windows.Forms.TextBox()
        Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_Nm_Barang = New System.Windows.Forms.TextBox()
        Me.BtnCetak = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Lv_Barang = New System.Windows.Forms.ListView()
        Me.Lv_No_RM = New System.Windows.Forms.ListView()
        Me.Lv_No_Transaksi = New System.Windows.Forms.ListView()
        Me.Lv_No_Split = New System.Windows.Forms.ListView()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
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
        Me.Panel1.Size = New System.Drawing.Size(784, 45)
        Me.Panel1.TabIndex = 90
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(20, 7)
        Me.Label11.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(280, 29)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Laporan - Packaging Waste"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 53)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 601)
        Me.Panel3.TabIndex = 92
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1, 45)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(942, 12)
        Me.Panel5.TabIndex = 93
        Me.Panel5.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Cmb_Jenis_Barang)
        Me.GroupBox1.Controls.Add(Me.Cmb_Lokasi)
        Me.GroupBox1.Controls.Add(Me.Txt_No_RM)
        Me.GroupBox1.Controls.Add(Me.Txt_No_Transaksi)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Txt_No_Split)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Txt_Kd_Barang)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Tgl2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Txt_Ket_RM)
        Me.GroupBox1.Controls.Add(Me.Tgl1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Txt_Nm_Barang)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 55)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(744, 195)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Cmb_Jenis_Barang
        '
        Me.Cmb_Jenis_Barang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Jenis_Barang.FormattingEnabled = True
        Me.Cmb_Jenis_Barang.Location = New System.Drawing.Point(130, 156)
        Me.Cmb_Jenis_Barang.Name = "Cmb_Jenis_Barang"
        Me.Cmb_Jenis_Barang.Size = New System.Drawing.Size(163, 24)
        Me.Cmb_Jenis_Barang.TabIndex = 6
        '
        'Cmb_Lokasi
        '
        Me.Cmb_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lokasi.FormattingEnabled = True
        Me.Cmb_Lokasi.Location = New System.Drawing.Point(130, 48)
        Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
        Me.Cmb_Lokasi.Size = New System.Drawing.Size(163, 24)
        Me.Cmb_Lokasi.TabIndex = 2
        '
        'Txt_No_RM
        '
        Me.Txt_No_RM.Location = New System.Drawing.Point(130, 130)
        Me.Txt_No_RM.Name = "Txt_No_RM"
        Me.Txt_No_RM.Size = New System.Drawing.Size(163, 20)
        Me.Txt_No_RM.TabIndex = 5
        '
        'Txt_No_Transaksi
        '
        Me.Txt_No_Transaksi.Location = New System.Drawing.Point(129, 104)
        Me.Txt_No_Transaksi.Name = "Txt_No_Transaksi"
        Me.Txt_No_Transaksi.Size = New System.Drawing.Size(163, 20)
        Me.Txt_No_Transaksi.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 133)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(116, 16)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "No Request Material"
        '
        'Txt_No_Split
        '
        Me.Txt_No_Split.Location = New System.Drawing.Point(130, 78)
        Me.Txt_No_Split.Name = "Txt_No_Split"
        Me.Txt_No_Split.Size = New System.Drawing.Size(163, 20)
        Me.Txt_No_Split.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 107)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 16)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "No Transaksi"
        '
        'Txt_Kd_Barang
        '
        Me.Txt_Kd_Barang.Enabled = False
        Me.Txt_Kd_Barang.Location = New System.Drawing.Point(297, 159)
        Me.Txt_Kd_Barang.Name = "Txt_Kd_Barang"
        Me.Txt_Kd_Barang.Size = New System.Drawing.Size(116, 20)
        Me.Txt_Kd_Barang.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 81)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 16)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "No Split"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 16)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Lokasi"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 159)
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
        Me.Tgl2.TabIndex = 1
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
        'Txt_Ket_RM
        '
        Me.Txt_Ket_RM.Enabled = False
        Me.Txt_Ket_RM.Location = New System.Drawing.Point(297, 130)
        Me.Txt_Ket_RM.Name = "Txt_Ket_RM"
        Me.Txt_Ket_RM.Size = New System.Drawing.Size(441, 20)
        Me.Txt_Ket_RM.TabIndex = 6
        '
        'Tgl1
        '
        Me.Tgl1.CustomFormat = "dd MMMM yyyy"
        Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl1.Location = New System.Drawing.Point(130, 22)
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
        'Txt_Nm_Barang
        '
        Me.Txt_Nm_Barang.Enabled = False
        Me.Txt_Nm_Barang.Location = New System.Drawing.Point(417, 159)
        Me.Txt_Nm_Barang.Name = "Txt_Nm_Barang"
        Me.Txt_Nm_Barang.Size = New System.Drawing.Size(321, 20)
        Me.Txt_Nm_Barang.TabIndex = 8
        '
        'BtnCetak
        '
        Me.BtnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnCetak.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnCetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnCetak.ForeColor = System.Drawing.Color.White
        Me.BtnCetak.Location = New System.Drawing.Point(585, 256)
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
        Me.BtnExit.Location = New System.Drawing.Point(674, 256)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(84, 33)
        Me.BtnExit.TabIndex = 2
        Me.BtnExit.Text = "&Keluar"
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(765, 65)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(19, 601)
        Me.Panel2.TabIndex = 92
        Me.Panel2.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(13, 292)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(942, 15)
        Me.Panel4.TabIndex = 93
        Me.Panel4.Visible = False
        '
        'Lv_Barang
        '
        Me.Lv_Barang.BackColor = System.Drawing.Color.White
        Me.Lv_Barang.FullRowSelect = True
        Me.Lv_Barang.GridLines = True
        Me.Lv_Barang.HideSelection = False
        Me.Lv_Barang.Location = New System.Drawing.Point(790, 235)
        Me.Lv_Barang.Name = "Lv_Barang"
        Me.Lv_Barang.Size = New System.Drawing.Size(608, 211)
        Me.Lv_Barang.TabIndex = 97
        Me.Lv_Barang.UseCompatibleStateImageBehavior = False
        Me.Lv_Barang.View = System.Windows.Forms.View.Details
        Me.Lv_Barang.Visible = False
        '
        'Lv_No_RM
        '
        Me.Lv_No_RM.BackColor = System.Drawing.Color.White
        Me.Lv_No_RM.FullRowSelect = True
        Me.Lv_No_RM.GridLines = True
        Me.Lv_No_RM.HideSelection = False
        Me.Lv_No_RM.Location = New System.Drawing.Point(790, 207)
        Me.Lv_No_RM.Name = "Lv_No_RM"
        Me.Lv_No_RM.Size = New System.Drawing.Size(608, 211)
        Me.Lv_No_RM.TabIndex = 97
        Me.Lv_No_RM.UseCompatibleStateImageBehavior = False
        Me.Lv_No_RM.View = System.Windows.Forms.View.Details
        Me.Lv_No_RM.Visible = False
        '
        'Lv_No_Transaksi
        '
        Me.Lv_No_Transaksi.BackColor = System.Drawing.Color.White
        Me.Lv_No_Transaksi.FullRowSelect = True
        Me.Lv_No_Transaksi.GridLines = True
        Me.Lv_No_Transaksi.HideSelection = False
        Me.Lv_No_Transaksi.Location = New System.Drawing.Point(790, 180)
        Me.Lv_No_Transaksi.Name = "Lv_No_Transaksi"
        Me.Lv_No_Transaksi.Size = New System.Drawing.Size(608, 211)
        Me.Lv_No_Transaksi.TabIndex = 97
        Me.Lv_No_Transaksi.UseCompatibleStateImageBehavior = False
        Me.Lv_No_Transaksi.View = System.Windows.Forms.View.Details
        Me.Lv_No_Transaksi.Visible = False
        '
        'Lv_No_Split
        '
        Me.Lv_No_Split.BackColor = System.Drawing.Color.White
        Me.Lv_No_Split.FullRowSelect = True
        Me.Lv_No_Split.GridLines = True
        Me.Lv_No_Split.HideSelection = False
        Me.Lv_No_Split.Location = New System.Drawing.Point(790, 155)
        Me.Lv_No_Split.Name = "Lv_No_Split"
        Me.Lv_No_Split.Size = New System.Drawing.Size(608, 211)
        Me.Lv_No_Split.TabIndex = 97
        Me.Lv_No_Split.UseCompatibleStateImageBehavior = False
        Me.Lv_No_Split.View = System.Windows.Forms.View.Details
        Me.Lv_No_Split.Visible = False
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
        Me.PanelGradient1.Size = New System.Drawing.Size(784, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'N_EMI_Laporan_Retur_Packaging
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(784, 306)
        Me.Controls.Add(Me.Lv_No_Split)
        Me.Controls.Add(Me.Lv_No_Transaksi)
        Me.Controls.Add(Me.Lv_No_RM)
        Me.Controls.Add(Me.Lv_Barang)
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
        Me.Name = "N_EMI_Laporan_Retur_Packaging"
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
    Friend WithEvents Panel5 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Cmb_Lokasi As ComboBox
    Friend WithEvents Txt_Kd_Barang As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Tgl2 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_Nm_Barang As TextBox
    Friend WithEvents BtnCetak As Button
    Friend WithEvents BtnExit As Button
    Friend WithEvents Txt_No_RM As TextBox
    Friend WithEvents Txt_No_Transaksi As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Txt_No_Split As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_Ket_RM As TextBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Cmb_Jenis_Barang As ComboBox
    Friend WithEvents Lv_Barang As ListView
    Friend WithEvents Lv_No_RM As ListView
    Friend WithEvents Lv_No_Transaksi As ListView
    Friend WithEvents Lv_No_Split As ListView
End Class
