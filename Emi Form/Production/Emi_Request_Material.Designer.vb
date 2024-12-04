<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Emi_Request_Material
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Txt_NoFaktur_ReqMaterial = New System.Windows.Forms.TextBox()
        Me.Txt_NoFaktur = New System.Windows.Forms.TextBox()
        Me.Txt_So = New System.Windows.Forms.TextBox()
        Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Dgv_Data = New System.Windows.Forms.DataGridView()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtTotalRequest = New System.Windows.Forms.TextBox()
        Me.noFak = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.kode_so = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.kode_barang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_Order = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sisa = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.satuan_order = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.jumlah = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.satuan_barang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tipe = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.Dgv_Data, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(985, 51)
        Me.Panel1.TabIndex = 23
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(189, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Request Material"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 56)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 552)
        Me.Panel3.TabIndex = 36
        Me.Panel3.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(18, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 37
        Me.Panel2.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Txt_NoFaktur_ReqMaterial)
        Me.GroupBox1.Controls.Add(Me.Txt_NoFaktur)
        Me.GroupBox1.Controls.Add(Me.Txt_So)
        Me.GroupBox1.Controls.Add(Me.Txt_KdBarang)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(21, 62)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(939, 124)
        Me.GroupBox1.TabIndex = 38
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Request Material"
        '
        'Txt_NoFaktur_ReqMaterial
        '
        Me.Txt_NoFaktur_ReqMaterial.BackColor = System.Drawing.Color.Goldenrod
        Me.Txt_NoFaktur_ReqMaterial.Enabled = False
        Me.Txt_NoFaktur_ReqMaterial.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_NoFaktur_ReqMaterial.ForeColor = System.Drawing.SystemColors.Window
        Me.Txt_NoFaktur_ReqMaterial.Location = New System.Drawing.Point(11, 24)
        Me.Txt_NoFaktur_ReqMaterial.MaxLength = 10
        Me.Txt_NoFaktur_ReqMaterial.Name = "Txt_NoFaktur_ReqMaterial"
        Me.Txt_NoFaktur_ReqMaterial.Size = New System.Drawing.Size(211, 22)
        Me.Txt_NoFaktur_ReqMaterial.TabIndex = 270
        '
        'Txt_NoFaktur
        '
        Me.Txt_NoFaktur.BackColor = System.Drawing.Color.Goldenrod
        Me.Txt_NoFaktur.Enabled = False
        Me.Txt_NoFaktur.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_NoFaktur.ForeColor = System.Drawing.SystemColors.Window
        Me.Txt_NoFaktur.Location = New System.Drawing.Point(228, 24)
        Me.Txt_NoFaktur.MaxLength = 10
        Me.Txt_NoFaktur.Name = "Txt_NoFaktur"
        Me.Txt_NoFaktur.Size = New System.Drawing.Size(211, 22)
        Me.Txt_NoFaktur.TabIndex = 270
        Me.Txt_NoFaktur.Visible = False
        '
        'Txt_So
        '
        Me.Txt_So.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_So.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_So.Enabled = False
        Me.Txt_So.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_So.Location = New System.Drawing.Point(115, 58)
        Me.Txt_So.Name = "Txt_So"
        Me.Txt_So.Size = New System.Drawing.Size(169, 23)
        Me.Txt_So.TabIndex = 7
        '
        'Txt_KdBarang
        '
        Me.Txt_KdBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KdBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_KdBarang.Enabled = False
        Me.Txt_KdBarang.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_KdBarang.Location = New System.Drawing.Point(115, 89)
        Me.Txt_KdBarang.Name = "Txt_KdBarang"
        Me.Txt_KdBarang.Size = New System.Drawing.Size(169, 23)
        Me.Txt_KdBarang.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 92)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 18)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Kode Barang"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 18)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Stock Owner"
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(21, 576)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(135, 35)
        Me.Btn_Simpan.TabIndex = 1
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(22, 193)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(942, 12)
        Me.Panel4.TabIndex = 37
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(22, 565)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(942, 12)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(964, 56)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(19, 563)
        Me.Panel6.TabIndex = 36
        Me.Panel6.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(21, 607)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(942, 12)
        Me.Panel7.TabIndex = 37
        Me.Panel7.Visible = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(158, 576)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(135, 35)
        Me.Btn_Refresh.TabIndex = 2
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Dgv_Data
        '
        Me.Dgv_Data.AllowUserToAddRows = False
        Me.Dgv_Data.AllowUserToDeleteRows = False
        Me.Dgv_Data.AllowUserToResizeColumns = False
        Me.Dgv_Data.AllowUserToResizeRows = False
        Me.Dgv_Data.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_Data.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.Dgv_Data.ColumnHeadersHeight = 45
        Me.Dgv_Data.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.noFak, Me.kode_so, Me.kode_barang, Me.Jumlah_Order, Me.sisa, Me.satuan_order, Me.jumlah, Me.satuan_barang, Me.tipe, Me.Column1})
        Me.Dgv_Data.Location = New System.Drawing.Point(20, 207)
        Me.Dgv_Data.MultiSelect = False
        Me.Dgv_Data.Name = "Dgv_Data"
        Me.Dgv_Data.RowHeadersWidth = 21
        Me.Dgv_Data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.Dgv_Data.Size = New System.Drawing.Size(940, 359)
        Me.Dgv_Data.TabIndex = 285
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(772, 581)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 17)
        Me.Label5.TabIndex = 467
        Me.Label5.Text = "Total"
        '
        'TxtTotalRequest
        '
        Me.TxtTotalRequest.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtTotalRequest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtTotalRequest.Enabled = False
        Me.TxtTotalRequest.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtTotalRequest.Location = New System.Drawing.Point(818, 579)
        Me.TxtTotalRequest.MaxLength = 50
        Me.TxtTotalRequest.Name = "TxtTotalRequest"
        Me.TxtTotalRequest.Size = New System.Drawing.Size(140, 21)
        Me.TxtTotalRequest.TabIndex = 466
        '
        'noFak
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.noFak.DefaultCellStyle = DataGridViewCellStyle2
        Me.noFak.HeaderText = "No Faktur"
        Me.noFak.Name = "noFak"
        Me.noFak.ReadOnly = True
        Me.noFak.Visible = False
        '
        'kode_so
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.kode_so.DefaultCellStyle = DataGridViewCellStyle3
        Me.kode_so.HeaderText = "Kode Stock Owner"
        Me.kode_so.Name = "kode_so"
        Me.kode_so.ReadOnly = True
        Me.kode_so.Width = 200
        '
        'kode_barang
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.kode_barang.DefaultCellStyle = DataGridViewCellStyle4
        Me.kode_barang.HeaderText = "Kode Barang"
        Me.kode_barang.Name = "kode_barang"
        Me.kode_barang.ReadOnly = True
        Me.kode_barang.Width = 210
        '
        'Jumlah_Order
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Jumlah_Order.DefaultCellStyle = DataGridViewCellStyle5
        Me.Jumlah_Order.HeaderText = "Jumlah Kebutuhan"
        Me.Jumlah_Order.Name = "Jumlah_Order"
        Me.Jumlah_Order.ReadOnly = True
        Me.Jumlah_Order.Width = 150
        '
        'sisa
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.sisa.DefaultCellStyle = DataGridViewCellStyle6
        Me.sisa.HeaderText = "Sisa"
        Me.sisa.Name = "sisa"
        Me.sisa.ReadOnly = True
        Me.sisa.Width = 120
        '
        'satuan_order
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.satuan_order.DefaultCellStyle = DataGridViewCellStyle7
        Me.satuan_order.HeaderText = "Satuan"
        Me.satuan_order.Name = "satuan_order"
        Me.satuan_order.ReadOnly = True
        '
        'jumlah
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.jumlah.DefaultCellStyle = DataGridViewCellStyle8
        Me.jumlah.HeaderText = "Jumlah"
        Me.jumlah.Name = "jumlah"
        Me.jumlah.Width = 125
        '
        'satuan_barang
        '
        Me.satuan_barang.HeaderText = "Satuan barang"
        Me.satuan_barang.Name = "satuan_barang"
        Me.satuan_barang.ReadOnly = True
        Me.satuan_barang.Visible = False
        Me.satuan_barang.Width = 80
        '
        'tipe
        '
        Me.tipe.HeaderText = "Tipe"
        Me.tipe.Name = "tipe"
        Me.tipe.ReadOnly = True
        Me.tipe.Visible = False
        '
        'Column1
        '
        Me.Column1.HeaderText = "Warna"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Visible = False
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
        Me.PanelGradient1.Size = New System.Drawing.Size(985, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Emi_Request_Material
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(985, 621)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtTotalRequest)
        Me.Controls.Add(Me.Dgv_Data)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Emi_Request_Material"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.Dgv_Data, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_So As TextBox
    Friend WithEvents Dgv_Data As DataGridView
    Friend WithEvents Txt_NoFaktur As TextBox
    Friend WithEvents Txt_NoFaktur_ReqMaterial As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TxtTotalRequest As TextBox
    Friend WithEvents noFak As DataGridViewTextBoxColumn
    Friend WithEvents kode_so As DataGridViewTextBoxColumn
    Friend WithEvents kode_barang As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Order As DataGridViewTextBoxColumn
    Friend WithEvents sisa As DataGridViewTextBoxColumn
    Friend WithEvents satuan_order As DataGridViewTextBoxColumn
    Friend WithEvents jumlah As DataGridViewTextBoxColumn
    Friend WithEvents satuan_barang As DataGridViewTextBoxColumn
    Friend WithEvents tipe As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
End Class
