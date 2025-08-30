<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SD_Pilih_BM
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.LblInquiry_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblNoFaktur = New System.Windows.Forms.Label()
        Me.txtFaktur = New System.Windows.Forms.TextBox()
        Me.BtnInquiry_Cari = New System.Windows.Forms.Button()
        Me.dgvBarangMasuk = New System.Windows.Forms.DataGridView()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnInquiry_Refresh = New System.Windows.Forms.Button()
        Me.LblInquiry_Lokasi = New System.Windows.Forms.Label()
        Me.cmbLokasi = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvBarangMasuk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.LblInquiry_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(821, 51)
        Me.Panel1.TabIndex = 22
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
        Me.PanelGradient1.Size = New System.Drawing.Size(821, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'LblInquiry_Judul
        '
        Me.LblInquiry_Judul.AutoSize = True
        Me.LblInquiry_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInquiry_Judul.Location = New System.Drawing.Point(15, 11)
        Me.LblInquiry_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LblInquiry_Judul.Name = "LblInquiry_Judul"
        Me.LblInquiry_Judul.Size = New System.Drawing.Size(291, 30)
        Me.LblInquiry_Judul.TabIndex = 0
        Me.LblInquiry_Judul.Text = "Pembelian - Barang Masuk"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 34
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 63)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 460)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(800, 63)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 447)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(18, 508)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1436, 15)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'lblNoFaktur
        '
        Me.lblNoFaktur.AutoSize = True
        Me.lblNoFaktur.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.lblNoFaktur.Location = New System.Drawing.Point(25, 104)
        Me.lblNoFaktur.Name = "lblNoFaktur"
        Me.lblNoFaktur.Size = New System.Drawing.Size(77, 20)
        Me.lblNoFaktur.TabIndex = 227
        Me.lblNoFaktur.Text = "No Faktur"
        '
        'txtFaktur
        '
        Me.txtFaktur.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtFaktur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFaktur.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.txtFaktur.Location = New System.Drawing.Point(151, 101)
        Me.txtFaktur.MaxLength = 50
        Me.txtFaktur.Name = "txtFaktur"
        Me.txtFaktur.Size = New System.Drawing.Size(228, 22)
        Me.txtFaktur.TabIndex = 228
        '
        'BtnInquiry_Cari
        '
        Me.BtnInquiry_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnInquiry_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnInquiry_Cari.ForeColor = System.Drawing.Color.White
        Me.BtnInquiry_Cari.Location = New System.Drawing.Point(399, 99)
        Me.BtnInquiry_Cari.Name = "BtnInquiry_Cari"
        Me.BtnInquiry_Cari.Size = New System.Drawing.Size(80, 28)
        Me.BtnInquiry_Cari.TabIndex = 338
        Me.BtnInquiry_Cari.Text = "Cari"
        Me.BtnInquiry_Cari.UseVisualStyleBackColor = False
        '
        'dgvBarangMasuk
        '
        Me.dgvBarangMasuk.AllowUserToAddRows = False
        Me.dgvBarangMasuk.AllowUserToDeleteRows = False
        Me.dgvBarangMasuk.AllowUserToResizeColumns = False
        Me.dgvBarangMasuk.AllowUserToResizeRows = False
        Me.dgvBarangMasuk.BackgroundColor = System.Drawing.Color.White
        Me.dgvBarangMasuk.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Work Sans", 8.0!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvBarangMasuk.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvBarangMasuk.ColumnHeadersHeight = 45
        Me.dgvBarangMasuk.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column8, Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6})
        Me.dgvBarangMasuk.Location = New System.Drawing.Point(21, 141)
        Me.dgvBarangMasuk.Name = "dgvBarangMasuk"
        Me.dgvBarangMasuk.ReadOnly = True
        Me.dgvBarangMasuk.RowHeadersWidth = 21
        Me.dgvBarangMasuk.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvBarangMasuk.Size = New System.Drawing.Size(772, 328)
        Me.dgvBarangMasuk.TabIndex = 382
        '
        'Column8
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.Column8.DefaultCellStyle = DataGridViewCellStyle4
        Me.Column8.HeaderText = "No Faktur"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Width = 130
        '
        'Column1
        '
        Me.Column1.HeaderText = "Lokasi Gudang"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 150
        '
        'Column2
        '
        Me.Column2.HeaderText = "No Po"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 120
        '
        'Column3
        '
        Me.Column3.HeaderText = "No Nota"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "Kode Supplier"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Visible = False
        '
        'Column5
        '
        Me.Column5.HeaderText = "Supplier"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 150
        '
        'Column6
        '
        Me.Column6.HeaderText = "Tanggal PO"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'BtnInquiry_Refresh
        '
        Me.BtnInquiry_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnInquiry_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnInquiry_Refresh.ForeColor = System.Drawing.Color.White
        Me.BtnInquiry_Refresh.Location = New System.Drawing.Point(26, 475)
        Me.BtnInquiry_Refresh.Name = "BtnInquiry_Refresh"
        Me.BtnInquiry_Refresh.Size = New System.Drawing.Size(80, 28)
        Me.BtnInquiry_Refresh.TabIndex = 389
        Me.BtnInquiry_Refresh.Text = "Refresh"
        Me.BtnInquiry_Refresh.UseVisualStyleBackColor = False
        '
        'LblInquiry_Lokasi
        '
        Me.LblInquiry_Lokasi.AutoSize = True
        Me.LblInquiry_Lokasi.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblInquiry_Lokasi.Location = New System.Drawing.Point(27, 75)
        Me.LblInquiry_Lokasi.Name = "LblInquiry_Lokasi"
        Me.LblInquiry_Lokasi.Size = New System.Drawing.Size(52, 20)
        Me.LblInquiry_Lokasi.TabIndex = 383
        Me.LblInquiry_Lokasi.Text = "Lokasi"
        '
        'cmbLokasi
        '
        Me.cmbLokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLokasi.FormattingEnabled = True
        Me.cmbLokasi.Location = New System.Drawing.Point(150, 71)
        Me.cmbLokasi.Name = "cmbLokasi"
        Me.cmbLokasi.Size = New System.Drawing.Size(228, 24)
        Me.cmbLokasi.TabIndex = 388
        '
        'SD_Pilih_BM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(821, 523)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.BtnInquiry_Refresh)
        Me.Controls.Add(Me.cmbLokasi)
        Me.Controls.Add(Me.LblInquiry_Lokasi)
        Me.Controls.Add(Me.dgvBarangMasuk)
        Me.Controls.Add(Me.BtnInquiry_Cari)
        Me.Controls.Add(Me.txtFaktur)
        Me.Controls.Add(Me.lblNoFaktur)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "SD_Pilih_BM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgvBarangMasuk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents LblInquiry_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents lblNoFaktur As Label
    Friend WithEvents txtFaktur As TextBox
    Friend WithEvents BtnInquiry_Cari As Button
    Friend WithEvents dgvBarangMasuk As DataGridView
    Friend WithEvents BtnInquiry_Refresh As Button
    Friend WithEvents LblInquiry_Lokasi As Label
    Friend WithEvents cmbLokasi As ComboBox
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
End Class
