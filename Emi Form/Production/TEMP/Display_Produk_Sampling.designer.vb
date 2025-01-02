<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Display_Produk_Sampling
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LblPilihBarang_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Lv_Sampling = New System.Windows.Forms.ListView()
        Me.ColumnKdBrg = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnNmBrg = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnTgl = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnJmlhProd = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnSatuan = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Cmb_KategoriBesar = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Cmb_KategoriKecil = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Dtp_tgl = New System.Windows.Forms.DateTimePicker()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.LblPilihBarang_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(774, 51)
        Me.Panel1.TabIndex = 22
        '
        'LblPilihBarang_Judul
        '
        Me.LblPilihBarang_Judul.AutoSize = True
        Me.LblPilihBarang_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPilihBarang_Judul.Location = New System.Drawing.Point(15, 11)
        Me.LblPilihBarang_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LblPilihBarang_Judul.Name = "LblPilihBarang_Judul"
        Me.LblPilihBarang_Judul.Size = New System.Drawing.Size(288, 30)
        Me.LblPilihBarang_Judul.TabIndex = 0
        Me.LblPilihBarang_Judul.Text = "Display - Produk Sampling"
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
        Me.Panel3.Size = New System.Drawing.Size(19, 544)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(753, 63)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 544)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(1, 596)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1028, 15)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'Lv_Sampling
        '
        Me.Lv_Sampling.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnKdBrg, Me.ColumnNmBrg, Me.ColumnTgl, Me.ColumnJmlhProd, Me.ColumnSatuan})
        Me.Lv_Sampling.FullRowSelect = True
        Me.Lv_Sampling.GridLines = True
        Me.Lv_Sampling.HideSelection = False
        Me.Lv_Sampling.Location = New System.Drawing.Point(5, 17)
        Me.Lv_Sampling.MultiSelect = False
        Me.Lv_Sampling.Name = "Lv_Sampling"
        Me.Lv_Sampling.Size = New System.Drawing.Size(721, 381)
        Me.Lv_Sampling.TabIndex = 358
        Me.Lv_Sampling.UseCompatibleStateImageBehavior = False
        Me.Lv_Sampling.View = System.Windows.Forms.View.Details
        '
        'ColumnKdBrg
        '
        Me.ColumnKdBrg.Text = "Kode Barang"
        Me.ColumnKdBrg.Width = 150
        '
        'ColumnNmBrg
        '
        Me.ColumnNmBrg.Text = "Nama"
        Me.ColumnNmBrg.Width = 250
        '
        'ColumnTgl
        '
        Me.ColumnTgl.Text = "Tanggal"
        Me.ColumnTgl.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnTgl.Width = 100
        '
        'ColumnJmlhProd
        '
        Me.ColumnJmlhProd.Text = "Jumlah Produksi"
        Me.ColumnJmlhProd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnJmlhProd.Width = 130
        '
        'ColumnSatuan
        '
        Me.ColumnSatuan.Text = "Satuan"
        Me.ColumnSatuan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnSatuan.Width = 83
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(24, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(108, 20)
        Me.Label1.TabIndex = 363
        Me.Label1.Text = "Kategori Besar"
        '
        'Cmb_KategoriBesar
        '
        Me.Cmb_KategoriBesar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_KategoriBesar.FormattingEnabled = True
        Me.Cmb_KategoriBesar.Location = New System.Drawing.Point(163, 71)
        Me.Cmb_KategoriBesar.Name = "Cmb_KategoriBesar"
        Me.Cmb_KategoriBesar.Size = New System.Drawing.Size(361, 24)
        Me.Cmb_KategoriBesar.TabIndex = 364
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(24, 102)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 20)
        Me.Label2.TabIndex = 365
        Me.Label2.Text = "Kategori Kecil"
        '
        'Cmb_KategoriKecil
        '
        Me.Cmb_KategoriKecil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_KategoriKecil.FormattingEnabled = True
        Me.Cmb_KategoriKecil.Location = New System.Drawing.Point(163, 101)
        Me.Cmb_KategoriKecil.Name = "Cmb_KategoriKecil"
        Me.Cmb_KategoriKecil.Size = New System.Drawing.Size(361, 24)
        Me.Cmb_KategoriKecil.TabIndex = 366
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(904, 380)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 367
        Me.Button1.Text = "Pilih"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(24, 129)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 20)
        Me.Label3.TabIndex = 368
        Me.Label3.Text = "Tanggal"
        '
        'Dtp_tgl
        '
        Me.Dtp_tgl.CustomFormat = "dd MMMM yyyy"
        Me.Dtp_tgl.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Dtp_tgl.Location = New System.Drawing.Point(163, 131)
        Me.Dtp_tgl.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Dtp_tgl.Name = "Dtp_tgl"
        Me.Dtp_tgl.Size = New System.Drawing.Size(165, 20)
        Me.Dtp_tgl.TabIndex = 369
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(163, 163)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(170, 28)
        Me.Btn_Refresh.TabIndex = 377
        Me.Btn_Refresh.Text = "Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(354, 163)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(170, 28)
        Me.Btn_Cari.TabIndex = 376
        Me.Btn_Cari.Text = "Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Lv_Sampling)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 191)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(730, 404)
        Me.GroupBox1.TabIndex = 380
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Data Produk Sampling"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(330, 133)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(25, 16)
        Me.Label7.TabIndex = 381
        Me.Label7.Text = "s/d"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(359, 131)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(165, 20)
        Me.DateTimePicker1.TabIndex = 382
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
        Me.PanelGradient1.Size = New System.Drawing.Size(774, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Display_Produk_Sampling
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(774, 613)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Cari)
        Me.Controls.Add(Me.Dtp_tgl)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Cmb_KategoriKecil)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Cmb_KategoriBesar)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Display_Produk_Sampling"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents LblPilihBarang_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Lv_Sampling As ListView
    Friend WithEvents ColumnKdBrg As ColumnHeader
    Friend WithEvents ColumnNmBrg As ColumnHeader
    Friend WithEvents Label1 As Label
    Friend WithEvents Cmb_KategoriBesar As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Cmb_KategoriKecil As ComboBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Dtp_tgl As DateTimePicker
    Friend WithEvents ColumnTgl As ColumnHeader
    Friend WithEvents ColumnJmlhProd As ColumnHeader
    Friend WithEvents ColumnSatuan As ColumnHeader
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label7 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
End Class
