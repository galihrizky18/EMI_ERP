<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Display_Barcode
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.LblInquiry_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.LblInquiry_Customer = New System.Windows.Forms.Label()
        Me.TxtInquiry_Cari = New System.Windows.Forms.TextBox()
        Me.BtnInquiry_Cari = New System.Windows.Forms.Button()
        Me.DgvInquiry_DataInquiry = New System.Windows.Forms.DataGridView()
        Me.LblInquiry_Lokasi = New System.Windows.Forms.Label()
        Me.CmbInquiry_Lokasi = New System.Windows.Forms.ComboBox()
        Me.BtnInquiry_Refresh = New System.Windows.Forms.Button()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.DgvInquiry_DataInquiry, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(771, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(771, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'LblInquiry_Judul
        '
        Me.LblInquiry_Judul.AutoSize = True
        Me.LblInquiry_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInquiry_Judul.Location = New System.Drawing.Point(15, 11)
        Me.LblInquiry_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LblInquiry_Judul.Name = "LblInquiry_Judul"
        Me.LblInquiry_Judul.Size = New System.Drawing.Size(195, 30)
        Me.LblInquiry_Judul.TabIndex = 0
        Me.LblInquiry_Judul.Text = "Display - Barcode"
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
        Me.Panel3.Size = New System.Drawing.Size(19, 416)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(750, 85)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 416)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(-7, 583)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1436, 15)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'LblInquiry_Customer
        '
        Me.LblInquiry_Customer.AutoSize = True
        Me.LblInquiry_Customer.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblInquiry_Customer.Location = New System.Drawing.Point(27, 105)
        Me.LblInquiry_Customer.Name = "LblInquiry_Customer"
        Me.LblInquiry_Customer.Size = New System.Drawing.Size(83, 20)
        Me.LblInquiry_Customer.TabIndex = 227
        Me.LblInquiry_Customer.Text = "CustomerX"
        '
        'TxtInquiry_Cari
        '
        Me.TxtInquiry_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtInquiry_Cari.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtInquiry_Cari.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtInquiry_Cari.Location = New System.Drawing.Point(131, 104)
        Me.TxtInquiry_Cari.MaxLength = 50
        Me.TxtInquiry_Cari.Name = "TxtInquiry_Cari"
        Me.TxtInquiry_Cari.Size = New System.Drawing.Size(228, 22)
        Me.TxtInquiry_Cari.TabIndex = 228
        '
        'BtnInquiry_Cari
        '
        Me.BtnInquiry_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnInquiry_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnInquiry_Cari.ForeColor = System.Drawing.Color.White
        Me.BtnInquiry_Cari.Location = New System.Drawing.Point(365, 100)
        Me.BtnInquiry_Cari.Name = "BtnInquiry_Cari"
        Me.BtnInquiry_Cari.Size = New System.Drawing.Size(80, 28)
        Me.BtnInquiry_Cari.TabIndex = 338
        Me.BtnInquiry_Cari.Text = "Cari"
        Me.BtnInquiry_Cari.UseVisualStyleBackColor = False
        '
        'DgvInquiry_DataInquiry
        '
        Me.DgvInquiry_DataInquiry.AllowUserToAddRows = False
        Me.DgvInquiry_DataInquiry.AllowUserToDeleteRows = False
        Me.DgvInquiry_DataInquiry.AllowUserToResizeColumns = False
        Me.DgvInquiry_DataInquiry.AllowUserToResizeRows = False
        Me.DgvInquiry_DataInquiry.BackgroundColor = System.Drawing.Color.White
        Me.DgvInquiry_DataInquiry.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Work Sans", 8.0!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvInquiry_DataInquiry.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvInquiry_DataInquiry.ColumnHeadersHeight = 45
        Me.DgvInquiry_DataInquiry.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column8, Me.Column1, Me.Column10, Me.Column2})
        Me.DgvInquiry_DataInquiry.Location = New System.Drawing.Point(20, 143)
        Me.DgvInquiry_DataInquiry.Name = "DgvInquiry_DataInquiry"
        Me.DgvInquiry_DataInquiry.ReadOnly = True
        Me.DgvInquiry_DataInquiry.RowHeadersWidth = 21
        Me.DgvInquiry_DataInquiry.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvInquiry_DataInquiry.Size = New System.Drawing.Size(726, 443)
        Me.DgvInquiry_DataInquiry.TabIndex = 382
        '
        'LblInquiry_Lokasi
        '
        Me.LblInquiry_Lokasi.AutoSize = True
        Me.LblInquiry_Lokasi.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblInquiry_Lokasi.Location = New System.Drawing.Point(27, 72)
        Me.LblInquiry_Lokasi.Name = "LblInquiry_Lokasi"
        Me.LblInquiry_Lokasi.Size = New System.Drawing.Size(60, 20)
        Me.LblInquiry_Lokasi.TabIndex = 383
        Me.LblInquiry_Lokasi.Text = "LokasiX"
        '
        'CmbInquiry_Lokasi
        '
        Me.CmbInquiry_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbInquiry_Lokasi.FormattingEnabled = True
        Me.CmbInquiry_Lokasi.Location = New System.Drawing.Point(131, 71)
        Me.CmbInquiry_Lokasi.Name = "CmbInquiry_Lokasi"
        Me.CmbInquiry_Lokasi.Size = New System.Drawing.Size(228, 24)
        Me.CmbInquiry_Lokasi.TabIndex = 388
        '
        'BtnInquiry_Refresh
        '
        Me.BtnInquiry_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnInquiry_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnInquiry_Refresh.ForeColor = System.Drawing.Color.White
        Me.BtnInquiry_Refresh.Location = New System.Drawing.Point(365, 67)
        Me.BtnInquiry_Refresh.Name = "BtnInquiry_Refresh"
        Me.BtnInquiry_Refresh.Size = New System.Drawing.Size(80, 28)
        Me.BtnInquiry_Refresh.TabIndex = 389
        Me.BtnInquiry_Refresh.Text = "Refresh"
        Me.BtnInquiry_Refresh.UseVisualStyleBackColor = False
        '
        'Column8
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.Column8.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column8.HeaderText = "No Inquiry"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Width = 250
        '
        'Column1
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column1.HeaderText = "Lokasi"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 200
        '
        'Column10
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.Column10.DefaultCellStyle = DataGridViewCellStyle4
        Me.Column10.HeaderText = "Kode Barang"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        Me.Column10.Width = 250
        '
        'Column2
        '
        Me.Column2.HeaderText = "Urut"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Visible = False
        Me.Column2.Width = 5
        '
        'EMI_Display_Barcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(771, 598)
        Me.Controls.Add(Me.BtnInquiry_Refresh)
        Me.Controls.Add(Me.CmbInquiry_Lokasi)
        Me.Controls.Add(Me.LblInquiry_Lokasi)
        Me.Controls.Add(Me.DgvInquiry_DataInquiry)
        Me.Controls.Add(Me.BtnInquiry_Cari)
        Me.Controls.Add(Me.TxtInquiry_Cari)
        Me.Controls.Add(Me.LblInquiry_Customer)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Display_Barcode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DgvInquiry_DataInquiry, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents LblInquiry_Customer As Label
    Friend WithEvents TxtInquiry_Cari As TextBox
    Friend WithEvents BtnInquiry_Cari As Button
    Friend WithEvents DgvInquiry_DataInquiry As DataGridView
    Friend WithEvents LblInquiry_Lokasi As Label
    Friend WithEvents CmbInquiry_Lokasi As ComboBox
    Friend WithEvents BtnInquiry_Refresh As Button
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column10 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
End Class
