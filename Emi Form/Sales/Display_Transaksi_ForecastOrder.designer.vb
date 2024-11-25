<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Display_Transaksi_ForecastOrder
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
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.LblInquiry_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblNoFaktur = New System.Windows.Forms.Label()
        Me.Txt_Value = New System.Windows.Forms.TextBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.LblInquiry_Lokasi = New System.Windows.Forms.Label()
        Me.Cmb_Kolom = New System.Windows.Forms.ComboBox()
        Me.Lv_Barang = New System.Windows.Forms.ListView()
        Me.Btn_New = New System.Windows.Forms.Button()
        Me.Cb_Referensi = New System.Windows.Forms.CheckBox()
        Me.Lv_Barang_Detail = New System.Windows.Forms.ListView()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
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
        Me.LblInquiry_Judul.Size = New System.Drawing.Size(271, 30)
        Me.LblInquiry_Judul.TabIndex = 0
        Me.LblInquiry_Judul.Text = "Display - Forecast Order "
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
        Me.Panel4.Location = New System.Drawing.Point(25, 404)
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
        Me.lblNoFaktur.Location = New System.Drawing.Point(25, 94)
        Me.lblNoFaktur.Name = "lblNoFaktur"
        Me.lblNoFaktur.Size = New System.Drawing.Size(46, 20)
        Me.lblNoFaktur.TabIndex = 227
        Me.lblNoFaktur.Text = "Value"
        '
        'Txt_Value
        '
        Me.Txt_Value.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Value.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Value.Location = New System.Drawing.Point(83, 93)
        Me.Txt_Value.MaxLength = 50
        Me.Txt_Value.Name = "Txt_Value"
        Me.Txt_Value.Size = New System.Drawing.Size(228, 22)
        Me.Txt_Value.TabIndex = 228
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(327, 89)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(80, 28)
        Me.Btn_Cari.TabIndex = 338
        Me.Btn_Cari.Text = "Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(327, 61)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(80, 28)
        Me.Btn_Refresh.TabIndex = 389
        Me.Btn_Refresh.Text = "Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'LblInquiry_Lokasi
        '
        Me.LblInquiry_Lokasi.AutoSize = True
        Me.LblInquiry_Lokasi.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblInquiry_Lokasi.Location = New System.Drawing.Point(27, 65)
        Me.LblInquiry_Lokasi.Name = "LblInquiry_Lokasi"
        Me.LblInquiry_Lokasi.Size = New System.Drawing.Size(50, 20)
        Me.LblInquiry_Lokasi.TabIndex = 383
        Me.LblInquiry_Lokasi.Text = "Kolom"
        '
        'Cmb_Kolom
        '
        Me.Cmb_Kolom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Kolom.FormattingEnabled = True
        Me.Cmb_Kolom.Location = New System.Drawing.Point(83, 65)
        Me.Cmb_Kolom.Name = "Cmb_Kolom"
        Me.Cmb_Kolom.Size = New System.Drawing.Size(228, 24)
        Me.Cmb_Kolom.TabIndex = 388
        '
        'Lv_Barang
        '
        Me.Lv_Barang.FullRowSelect = True
        Me.Lv_Barang.GridLines = True
        Me.Lv_Barang.HideSelection = False
        Me.Lv_Barang.Location = New System.Drawing.Point(26, 147)
        Me.Lv_Barang.Name = "Lv_Barang"
        Me.Lv_Barang.Size = New System.Drawing.Size(767, 255)
        Me.Lv_Barang.TabIndex = 390
        Me.Lv_Barang.UseCompatibleStateImageBehavior = False
        Me.Lv_Barang.View = System.Windows.Forms.View.Details
        '
        'Btn_New
        '
        Me.Btn_New.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_New.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_New.ForeColor = System.Drawing.Color.White
        Me.Btn_New.Location = New System.Drawing.Point(423, 60)
        Me.Btn_New.Name = "Btn_New"
        Me.Btn_New.Size = New System.Drawing.Size(157, 28)
        Me.Btn_New.TabIndex = 391
        Me.Btn_New.Text = "Tambah data baru"
        Me.Btn_New.UseVisualStyleBackColor = False
        '
        'Cb_Referensi
        '
        Me.Cb_Referensi.AutoSize = True
        Me.Cb_Referensi.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Cb_Referensi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Cb_Referensi.Location = New System.Drawing.Point(26, 119)
        Me.Cb_Referensi.Name = "Cb_Referensi"
        Me.Cb_Referensi.Size = New System.Drawing.Size(93, 24)
        Me.Cb_Referensi.TabIndex = 392
        Me.Cb_Referensi.Text = "Referensi"
        Me.Cb_Referensi.UseVisualStyleBackColor = True
        '
        'Lv_Barang_Detail
        '
        Me.Lv_Barang_Detail.FullRowSelect = True
        Me.Lv_Barang_Detail.GridLines = True
        Me.Lv_Barang_Detail.HideSelection = False
        Me.Lv_Barang_Detail.Location = New System.Drawing.Point(25, 421)
        Me.Lv_Barang_Detail.Name = "Lv_Barang_Detail"
        Me.Lv_Barang_Detail.Size = New System.Drawing.Size(767, 255)
        Me.Lv_Barang_Detail.TabIndex = 393
        Me.Lv_Barang_Detail.UseCompatibleStateImageBehavior = False
        Me.Lv_Barang_Detail.View = System.Windows.Forms.View.Details
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(-308, 678)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1436, 15)
        Me.Panel6.TabIndex = 394
        Me.Panel6.Visible = False
        '
        'Display_Transaksi_ForecastOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(821, 728)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Lv_Barang_Detail)
        Me.Controls.Add(Me.Cb_Referensi)
        Me.Controls.Add(Me.Btn_New)
        Me.Controls.Add(Me.Lv_Barang)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Cmb_Kolom)
        Me.Controls.Add(Me.LblInquiry_Lokasi)
        Me.Controls.Add(Me.Btn_Cari)
        Me.Controls.Add(Me.Txt_Value)
        Me.Controls.Add(Me.lblNoFaktur)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Display_Transaksi_ForecastOrder"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
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
    Friend WithEvents Txt_Value As TextBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents LblInquiry_Lokasi As Label
    Friend WithEvents Cmb_Kolom As ComboBox
    Friend WithEvents Lv_Barang As ListView
    Friend WithEvents Btn_New As Button
    Friend WithEvents Cb_Referensi As CheckBox
    Friend WithEvents Lv_Barang_Detail As ListView
    Friend WithEvents Panel6 As Panel
End Class
