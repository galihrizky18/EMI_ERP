<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EMI_Transfer_Quality_QC
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
        Me.Txt_ScanBarcode = New System.Windows.Forms.TextBox()
        Me.Btn_Insert = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Lv_Data = New System.Windows.Forms.ListView()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TxtNo_Transaksi = New System.Windows.Forms.TextBox()
        Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
        Me.Cmb_KualitasAkhir = New System.Windows.Forms.ComboBox()
        Me.Cmb_KualitasAwal = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Barcode = New System.Windows.Forms.PictureBox()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Lbl_Supplier = New System.Windows.Forms.Label()
        Me.TxtKeterangan = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.Barcode, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(939, 51)
        Me.Panel1.TabIndex = 25
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
        Me.PanelGradient1.Size = New System.Drawing.Size(939, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 11)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(181, 25)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Transfer Quality"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(19, 50)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1208, 10)
        Me.Panel2.TabIndex = 35
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 51)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(17, 695)
        Me.Panel3.TabIndex = 36
        Me.Panel3.Visible = False
        '
        'Txt_ScanBarcode
        '
        Me.Txt_ScanBarcode.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_ScanBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_ScanBarcode.Enabled = False
        Me.Txt_ScanBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_ScanBarcode.Location = New System.Drawing.Point(19, 184)
        Me.Txt_ScanBarcode.MaxLength = 50
        Me.Txt_ScanBarcode.Name = "Txt_ScanBarcode"
        Me.Txt_ScanBarcode.Size = New System.Drawing.Size(389, 26)
        Me.Txt_ScanBarcode.TabIndex = 481
        '
        'Btn_Insert
        '
        Me.Btn_Insert.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Insert.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Insert.ForeColor = System.Drawing.Color.White
        Me.Btn_Insert.Location = New System.Drawing.Point(816, 181)
        Me.Btn_Insert.Name = "Btn_Insert"
        Me.Btn_Insert.Size = New System.Drawing.Size(105, 32)
        Me.Btn_Insert.TabIndex = 483
        Me.Btn_Insert.Text = "&Insert"
        Me.Btn_Insert.UseVisualStyleBackColor = False
        Me.Btn_Insert.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(20, 210)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1208, 10)
        Me.Panel4.TabIndex = 36
        Me.Panel4.Visible = False
        '
        'Lv_Data
        '
        Me.Lv_Data.FullRowSelect = True
        Me.Lv_Data.GridLines = True
        Me.Lv_Data.HideSelection = False
        Me.Lv_Data.Location = New System.Drawing.Point(17, 219)
        Me.Lv_Data.Name = "Lv_Data"
        Me.Lv_Data.Size = New System.Drawing.Size(903, 355)
        Me.Lv_Data.TabIndex = 484
        Me.Lv_Data.UseCompatibleStateImageBehavior = False
        Me.Lv_Data.View = System.Windows.Forms.View.Details
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(19, 651)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1208, 10)
        Me.Panel5.TabIndex = 36
        Me.Panel5.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(921, 53)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(17, 695)
        Me.Panel6.TabIndex = 36
        Me.Panel6.Visible = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(20, 618)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(119, 32)
        Me.Button1.TabIndex = 483
        Me.Button1.Text = "&Simpan"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(145, 618)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(119, 32)
        Me.Button2.TabIndex = 483
        Me.Button2.Text = "&Refresh"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(18, 576)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1208, 10)
        Me.Panel7.TabIndex = 36
        Me.Panel7.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TxtNo_Transaksi)
        Me.GroupBox1.Controls.Add(Me.Cmb_Lokasi)
        Me.GroupBox1.Controls.Add(Me.Cmb_KualitasAkhir)
        Me.GroupBox1.Controls.Add(Me.Cmb_KualitasAwal)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 60)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(901, 113)
        Me.GroupBox1.TabIndex = 485
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Quality"
        '
        'TxtNo_Transaksi
        '
        Me.TxtNo_Transaksi.BackColor = System.Drawing.Color.Goldenrod
        Me.TxtNo_Transaksi.Enabled = False
        Me.TxtNo_Transaksi.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNo_Transaksi.Location = New System.Drawing.Point(18, 19)
        Me.TxtNo_Transaksi.MaxLength = 50
        Me.TxtNo_Transaksi.Name = "TxtNo_Transaksi"
        Me.TxtNo_Transaksi.Size = New System.Drawing.Size(253, 21)
        Me.TxtNo_Transaksi.TabIndex = 485
        '
        'Cmb_Lokasi
        '
        Me.Cmb_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lokasi.Enabled = False
        Me.Cmb_Lokasi.FormattingEnabled = True
        Me.Cmb_Lokasi.Location = New System.Drawing.Point(124, 47)
        Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
        Me.Cmb_Lokasi.Size = New System.Drawing.Size(147, 24)
        Me.Cmb_Lokasi.TabIndex = 484
        '
        'Cmb_KualitasAkhir
        '
        Me.Cmb_KualitasAkhir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_KualitasAkhir.FormattingEnabled = True
        Me.Cmb_KualitasAkhir.Location = New System.Drawing.Point(396, 77)
        Me.Cmb_KualitasAkhir.Name = "Cmb_KualitasAkhir"
        Me.Cmb_KualitasAkhir.Size = New System.Drawing.Size(147, 24)
        Me.Cmb_KualitasAkhir.TabIndex = 484
        '
        'Cmb_KualitasAwal
        '
        Me.Cmb_KualitasAwal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_KualitasAwal.FormattingEnabled = True
        Me.Cmb_KualitasAwal.Location = New System.Drawing.Point(124, 77)
        Me.Cmb_KualitasAwal.Name = "Cmb_KualitasAwal"
        Me.Cmb_KualitasAwal.Size = New System.Drawing.Size(147, 24)
        Me.Cmb_KualitasAwal.TabIndex = 484
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans SemiBold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(285, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(105, 20)
        Me.Label3.TabIndex = 482
        Me.Label3.Text = "Kualitas Akhir"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans SemiBold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(14, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 20)
        Me.Label2.TabIndex = 482
        Me.Label2.Text = "Lokasi"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(14, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 20)
        Me.Label1.TabIndex = 482
        Me.Label1.Text = "Kualitas Awal"
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Location = New System.Drawing.Point(547, 73)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(59, 32)
        Me.Button3.TabIndex = 483
        Me.Button3.Text = "&Set"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Barcode
        '
        Me.Barcode.Location = New System.Drawing.Point(945, 81)
        Me.Barcode.Name = "Barcode"
        Me.Barcode.Size = New System.Drawing.Size(100, 50)
        Me.Barcode.TabIndex = 486
        Me.Barcode.TabStop = False
        Me.Barcode.Visible = False
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Location = New System.Drawing.Point(18, 174)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(1208, 10)
        Me.Panel8.TabIndex = 36
        Me.Panel8.Visible = False
        '
        'Lbl_Supplier
        '
        Me.Lbl_Supplier.AutoSize = True
        Me.Lbl_Supplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Lbl_Supplier.Location = New System.Drawing.Point(17, 591)
        Me.Lbl_Supplier.Name = "Lbl_Supplier"
        Me.Lbl_Supplier.Size = New System.Drawing.Size(82, 17)
        Me.Lbl_Supplier.TabIndex = 487
        Me.Lbl_Supplier.Text = "Keterangan"
        '
        'TxtKeterangan
        '
        Me.TxtKeterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKeterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKeterangan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtKeterangan.Location = New System.Drawing.Point(129, 589)
        Me.TxtKeterangan.MaxLength = 50
        Me.TxtKeterangan.Name = "TxtKeterangan"
        Me.TxtKeterangan.Size = New System.Drawing.Size(340, 21)
        Me.TxtKeterangan.TabIndex = 486
        '
        'EMI_Transfer_Quality_QC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(939, 658)
        Me.Controls.Add(Me.Barcode)
        Me.Controls.Add(Me.Lbl_Supplier)
        Me.Controls.Add(Me.TxtKeterangan)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Lv_Data)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Btn_Insert)
        Me.Controls.Add(Me.Txt_ScanBarcode)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Transfer_Quality_QC"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.Barcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Txt_ScanBarcode As TextBox
    Friend WithEvents Btn_Insert As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Lv_Data As ListView
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents Cmb_KualitasAwal As ComboBox
    Friend WithEvents Cmb_KualitasAkhir As ComboBox
    Friend WithEvents Panel8 As Panel
    Friend WithEvents TxtNo_Transaksi As TextBox
    Friend WithEvents Cmb_Lokasi As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Lbl_Supplier As Label
    Friend WithEvents TxtKeterangan As TextBox
    Friend WithEvents Barcode As PictureBox
End Class
