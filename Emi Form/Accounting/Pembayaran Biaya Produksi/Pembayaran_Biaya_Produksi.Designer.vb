<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Pembayaran_Biaya_Produksi
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
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"", "10 Feb 2025", "Listrik", "Hydraulic Press", "1,500,00.00"}, -1)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Txt_NoFak = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
        Me.Dtp_Tgl = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Cmb_Mesin = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txt_NoTagihan = New System.Windows.Forms.TextBox()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Txt_Biaya = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Cmb_JenisBiaya = New System.Windows.Forms.ComboBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Txt_FilterValue = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Lv_data = New System.Windows.Forms.ListView()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Cmb_Filter = New System.Windows.Forms.ComboBox()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Lbl_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(826, 51)
        Me.Panel1.TabIndex = 23
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 11)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(308, 25)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Pembayaran Biaya Produksi"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1208, 10)
        Me.Panel2.TabIndex = 35
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 71)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(15, 687)
        Me.Panel3.TabIndex = 36
        Me.Panel3.Visible = False
        '
        'Txt_NoFak
        '
        Me.Txt_NoFak.BackColor = System.Drawing.Color.Goldenrod
        Me.Txt_NoFak.Enabled = False
        Me.Txt_NoFak.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_NoFak.ForeColor = System.Drawing.SystemColors.Window
        Me.Txt_NoFak.Location = New System.Drawing.Point(13, 59)
        Me.Txt_NoFak.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_NoFak.MaxLength = 20
        Me.Txt_NoFak.Name = "Txt_NoFak"
        Me.Txt_NoFak.Size = New System.Drawing.Size(201, 21)
        Me.Txt_NoFak.TabIndex = 408
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(16, 94)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 20)
        Me.Label4.TabIndex = 410
        Me.Label4.Text = "Tanggal"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(20, 81)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1208, 10)
        Me.Panel4.TabIndex = 35
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(813, 71)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(15, 687)
        Me.Panel5.TabIndex = 36
        Me.Panel5.Visible = False
        '
        'Txt_Keterangan
        '
        Me.Txt_Keterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Keterangan.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Txt_Keterangan.Location = New System.Drawing.Point(153, 123)
        Me.Txt_Keterangan.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_Keterangan.MaxLength = 40
        Me.Txt_Keterangan.Name = "Txt_Keterangan"
        Me.Txt_Keterangan.Size = New System.Drawing.Size(318, 23)
        Me.Txt_Keterangan.TabIndex = 411
        '
        'Dtp_Tgl
        '
        Me.Dtp_Tgl.CustomFormat = "dd MMMM yyyy"
        Me.Dtp_Tgl.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Dtp_Tgl.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Dtp_Tgl.Location = New System.Drawing.Point(153, 94)
        Me.Dtp_Tgl.Margin = New System.Windows.Forms.Padding(2)
        Me.Dtp_Tgl.Name = "Dtp_Tgl"
        Me.Dtp_Tgl.Size = New System.Drawing.Size(212, 23)
        Me.Dtp_Tgl.TabIndex = 412
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(16, 123)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 20)
        Me.Label1.TabIndex = 410
        Me.Label1.Text = "Keterangan"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(16, 188)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 20)
        Me.Label2.TabIndex = 410
        Me.Label2.Text = "Mesin"
        '
        'Cmb_Mesin
        '
        Me.Cmb_Mesin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Mesin.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Cmb_Mesin.FormattingEnabled = True
        Me.Cmb_Mesin.Location = New System.Drawing.Point(153, 185)
        Me.Cmb_Mesin.Name = "Cmb_Mesin"
        Me.Cmb_Mesin.Size = New System.Drawing.Size(318, 26)
        Me.Cmb_Mesin.TabIndex = 413
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(16, 218)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 20)
        Me.Label3.TabIndex = 410
        Me.Label3.Text = "No Tagihan"
        '
        'Txt_NoTagihan
        '
        Me.Txt_NoTagihan.BackColor = System.Drawing.Color.White
        Me.Txt_NoTagihan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NoTagihan.Enabled = False
        Me.Txt_NoTagihan.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Txt_NoTagihan.Location = New System.Drawing.Point(153, 218)
        Me.Txt_NoTagihan.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_NoTagihan.MaxLength = 40
        Me.Txt_NoTagihan.Name = "Txt_NoTagihan"
        Me.Txt_NoTagihan.Size = New System.Drawing.Size(318, 23)
        Me.Txt_NoTagihan.TabIndex = 411
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(150, 280)
        Me.Btn_Simpan.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(121, 38)
        Me.Btn_Simpan.TabIndex = 414
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(271, 280)
        Me.Btn_Refresh.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(121, 38)
        Me.Btn_Refresh.TabIndex = 414
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(16, 249)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 20)
        Me.Label5.TabIndex = 410
        Me.Label5.Text = "Biaya"
        '
        'Txt_Biaya
        '
        Me.Txt_Biaya.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Biaya.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Biaya.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Txt_Biaya.Location = New System.Drawing.Point(153, 249)
        Me.Txt_Biaya.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_Biaya.MaxLength = 40
        Me.Txt_Biaya.Name = "Txt_Biaya"
        Me.Txt_Biaya.Size = New System.Drawing.Size(318, 23)
        Me.Txt_Biaya.TabIndex = 411
        Me.Txt_Biaya.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(16, 156)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 20)
        Me.Label6.TabIndex = 410
        Me.Label6.Text = "Biaya"
        '
        'Cmb_JenisBiaya
        '
        Me.Cmb_JenisBiaya.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_JenisBiaya.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Cmb_JenisBiaya.FormattingEnabled = True
        Me.Cmb_JenisBiaya.Location = New System.Drawing.Point(153, 153)
        Me.Cmb_JenisBiaya.Name = "Cmb_JenisBiaya"
        Me.Cmb_JenisBiaya.Size = New System.Drawing.Size(318, 26)
        Me.Cmb_JenisBiaya.TabIndex = 413
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(15, 320)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1208, 10)
        Me.Panel6.TabIndex = 35
        Me.Panel6.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label7.Location = New System.Drawing.Point(19, 334)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(46, 20)
        Me.Label7.TabIndex = 410
        Me.Label7.Text = "Filter"
        '
        'Txt_FilterValue
        '
        Me.Txt_FilterValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_FilterValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_FilterValue.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Txt_FilterValue.Location = New System.Drawing.Point(273, 332)
        Me.Txt_FilterValue.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_FilterValue.MaxLength = 40
        Me.Txt_FilterValue.Name = "Txt_FilterValue"
        Me.Txt_FilterValue.Size = New System.Drawing.Size(272, 23)
        Me.Txt_FilterValue.TabIndex = 411
        Me.Txt_FilterValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Button2.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(547, 329)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(88, 31)
        Me.Button2.TabIndex = 414
        Me.Button2.Text = "&Cari"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Lv_data
        '
        Me.Lv_data.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
        Me.Lv_data.HideSelection = False
        Me.Lv_data.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem2})
        Me.Lv_data.Location = New System.Drawing.Point(15, 367)
        Me.Lv_data.Name = "Lv_data"
        Me.Lv_data.Size = New System.Drawing.Size(800, 326)
        Me.Lv_data.TabIndex = 415
        Me.Lv_data.UseCompatibleStateImageBehavior = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(20, 358)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1208, 10)
        Me.Panel7.TabIndex = 35
        Me.Panel7.Visible = False
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Location = New System.Drawing.Point(15, 695)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(1208, 10)
        Me.Panel8.TabIndex = 35
        Me.Panel8.Visible = False
        '
        'Cmb_Filter
        '
        Me.Cmb_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Filter.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Cmb_Filter.FormattingEnabled = True
        Me.Cmb_Filter.Location = New System.Drawing.Point(153, 331)
        Me.Cmb_Filter.Name = "Cmb_Filter"
        Me.Cmb_Filter.Size = New System.Drawing.Size(118, 26)
        Me.Cmb_Filter.TabIndex = 413
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Tanggal"
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Jenis Biaya"
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Mesin"
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Biaya"
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
        Me.PanelGradient1.Size = New System.Drawing.Size(826, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Pembayaran_Biaya_Produksi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(826, 705)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Lv_data)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Cmb_Filter)
        Me.Controls.Add(Me.Cmb_JenisBiaya)
        Me.Controls.Add(Me.Cmb_Mesin)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Dtp_Tgl)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Txt_FilterValue)
        Me.Controls.Add(Me.Txt_Biaya)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Txt_NoTagihan)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Txt_Keterangan)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Txt_NoFak)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Pembayaran_Biaya_Produksi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Txt_NoFak As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Txt_Keterangan As TextBox
    Friend WithEvents Dtp_Tgl As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Cmb_Mesin As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Txt_NoTagihan As TextBox
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Txt_Biaya As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Cmb_JenisBiaya As ComboBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label7 As Label
    Friend WithEvents Txt_FilterValue As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Lv_data As ListView
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Cmb_Filter As ComboBox
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
End Class
