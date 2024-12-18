<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EMI_Transfer_Stock
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
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Lv_Barang_TS = New System.Windows.Forms.ListView()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.TxtNo_Transaksi = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtKd_Barang = New System.Windows.Forms.TextBox()
        Me.TxtNm_Barang = New System.Windows.Forms.TextBox()
        Me.TxtStock = New System.Windows.Forms.TextBox()
        Me.TxtJumlah = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtKeterangan = New System.Windows.Forms.TextBox()
        Me.CmbSO_Asal = New System.Windows.Forms.ComboBox()
        Me.CmbSo_Tujuan = New System.Windows.Forms.ComboBox()
        Me.ListView2 = New System.Windows.Forms.ListView()
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
        Me.Panel1.Size = New System.Drawing.Size(678, 51)
        Me.Panel1.TabIndex = 23
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
        Me.PanelGradient1.Size = New System.Drawing.Size(678, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 11)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(167, 30)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Transfer Stock"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 35
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 63)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 498)
        Me.Panel3.TabIndex = 36
        Me.Panel3.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(5, 142)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(924, 12)
        Me.Panel6.TabIndex = 232
        Me.Panel6.Visible = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(111, 547)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Refresh.TabIndex = 235
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(21, 547)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Simpan.TabIndex = 233
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(4, 524)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(938, 19)
        Me.Panel7.TabIndex = 236
        Me.Panel7.Visible = False
        '
        'Lv_Barang_TS
        '
        Me.Lv_Barang_TS.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_Barang_TS.FullRowSelect = True
        Me.Lv_Barang_TS.GridLines = True
        Me.Lv_Barang_TS.HideSelection = False
        Me.Lv_Barang_TS.Location = New System.Drawing.Point(21, 217)
        Me.Lv_Barang_TS.Name = "Lv_Barang_TS"
        Me.Lv_Barang_TS.Size = New System.Drawing.Size(633, 304)
        Me.Lv_Barang_TS.TabIndex = 343
        Me.Lv_Barang_TS.UseCompatibleStateImageBehavior = False
        Me.Lv_Barang_TS.View = System.Windows.Forms.View.Details
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(659, 56)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 515)
        Me.Panel5.TabIndex = 344
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(-247, 586)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1436, 15)
        Me.Panel4.TabIndex = 345
        Me.Panel4.Visible = False
        '
        'TxtNo_Transaksi
        '
        Me.TxtNo_Transaksi.BackColor = System.Drawing.Color.Goldenrod
        Me.TxtNo_Transaksi.Enabled = False
        Me.TxtNo_Transaksi.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNo_Transaksi.Location = New System.Drawing.Point(20, 63)
        Me.TxtNo_Transaksi.MaxLength = 50
        Me.TxtNo_Transaksi.Name = "TxtNo_Transaksi"
        Me.TxtNo_Transaksi.Size = New System.Drawing.Size(210, 21)
        Me.TxtNo_Transaksi.TabIndex = 347
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Location = New System.Drawing.Point(513, 157)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 21)
        Me.Label2.TabIndex = 348
        Me.Label2.Text = "Stock"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Location = New System.Drawing.Point(23, 157)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(102, 21)
        Me.Label6.TabIndex = 349
        Me.Label6.Text = "Kode Barang"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Location = New System.Drawing.Point(583, 157)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(71, 21)
        Me.Label10.TabIndex = 351
        Me.Label10.Text = "Jumlah"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Location = New System.Drawing.Point(128, 157)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(380, 21)
        Me.Label7.TabIndex = 350
        Me.Label7.Text = "Nama Barang"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtKd_Barang
        '
        Me.TxtKd_Barang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKd_Barang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKd_Barang.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtKd_Barang.Location = New System.Drawing.Point(23, 183)
        Me.TxtKd_Barang.MaxLength = 50
        Me.TxtKd_Barang.Name = "TxtKd_Barang"
        Me.TxtKd_Barang.Size = New System.Drawing.Size(102, 22)
        Me.TxtKd_Barang.TabIndex = 352
        '
        'TxtNm_Barang
        '
        Me.TxtNm_Barang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtNm_Barang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtNm_Barang.Enabled = False
        Me.TxtNm_Barang.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtNm_Barang.Location = New System.Drawing.Point(128, 183)
        Me.TxtNm_Barang.MaxLength = 50
        Me.TxtNm_Barang.Name = "TxtNm_Barang"
        Me.TxtNm_Barang.Size = New System.Drawing.Size(380, 22)
        Me.TxtNm_Barang.TabIndex = 353
        '
        'TxtStock
        '
        Me.TxtStock.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtStock.Enabled = False
        Me.TxtStock.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtStock.Location = New System.Drawing.Point(513, 183)
        Me.TxtStock.MaxLength = 50
        Me.TxtStock.Name = "TxtStock"
        Me.TxtStock.Size = New System.Drawing.Size(67, 22)
        Me.TxtStock.TabIndex = 354
        '
        'TxtJumlah
        '
        Me.TxtJumlah.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtJumlah.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtJumlah.Location = New System.Drawing.Point(583, 183)
        Me.TxtJumlah.MaxLength = 50
        Me.TxtJumlah.Name = "TxtJumlah"
        Me.TxtJumlah.Size = New System.Drawing.Size(71, 22)
        Me.TxtJumlah.TabIndex = 355
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Location = New System.Drawing.Point(244, 92)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(410, 21)
        Me.Label3.TabIndex = 358
        Me.Label3.Text = "Keterangan"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Location = New System.Drawing.Point(131, 92)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(110, 21)
        Me.Label12.TabIndex = 357
        Me.Label12.Text = "SO Tujuan"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Location = New System.Drawing.Point(23, 92)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(105, 21)
        Me.Label8.TabIndex = 356
        Me.Label8.Text = "SO Awal"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtKeterangan
        '
        Me.TxtKeterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKeterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKeterangan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtKeterangan.Location = New System.Drawing.Point(244, 116)
        Me.TxtKeterangan.MaxLength = 50
        Me.TxtKeterangan.Name = "TxtKeterangan"
        Me.TxtKeterangan.Size = New System.Drawing.Size(410, 22)
        Me.TxtKeterangan.TabIndex = 361
        '
        'CmbSO_Asal
        '
        Me.CmbSO_Asal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSO_Asal.FormattingEnabled = True
        Me.CmbSO_Asal.Location = New System.Drawing.Point(23, 117)
        Me.CmbSO_Asal.Name = "CmbSO_Asal"
        Me.CmbSO_Asal.Size = New System.Drawing.Size(105, 21)
        Me.CmbSO_Asal.TabIndex = 392
        '
        'CmbSo_Tujuan
        '
        Me.CmbSo_Tujuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSo_Tujuan.FormattingEnabled = True
        Me.CmbSo_Tujuan.Location = New System.Drawing.Point(131, 117)
        Me.CmbSo_Tujuan.Name = "CmbSo_Tujuan"
        Me.CmbSo_Tujuan.Size = New System.Drawing.Size(110, 21)
        Me.CmbSo_Tujuan.TabIndex = 393
        '
        'ListView2
        '
        Me.ListView2.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.ListView2.FullRowSelect = True
        Me.ListView2.GridLines = True
        Me.ListView2.HideSelection = False
        Me.ListView2.Location = New System.Drawing.Point(669, 206)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(633, 146)
        Me.ListView2.TabIndex = 394
        Me.ListView2.UseCompatibleStateImageBehavior = False
        Me.ListView2.View = System.Windows.Forms.View.Details
        '
        'EMI_Transfer_Stock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(678, 602)
        Me.Controls.Add(Me.ListView2)
        Me.Controls.Add(Me.CmbSo_Tujuan)
        Me.Controls.Add(Me.CmbSO_Asal)
        Me.Controls.Add(Me.TxtKeterangan)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtJumlah)
        Me.Controls.Add(Me.TxtStock)
        Me.Controls.Add(Me.TxtNm_Barang)
        Me.Controls.Add(Me.TxtKd_Barang)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtNo_Transaksi)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Lv_Barang_TS)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "EMI_Transfer_Stock"
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
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Lv_Barang_TS As ListView
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents TxtNo_Transaksi As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtKd_Barang As TextBox
    Friend WithEvents TxtNm_Barang As TextBox
    Friend WithEvents TxtStock As TextBox
    Friend WithEvents TxtJumlah As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents TxtKeterangan As TextBox
    Friend WithEvents CmbSO_Asal As ComboBox
    Friend WithEvents CmbSo_Tujuan As ComboBox
    Friend WithEvents ListView2 As ListView
End Class
