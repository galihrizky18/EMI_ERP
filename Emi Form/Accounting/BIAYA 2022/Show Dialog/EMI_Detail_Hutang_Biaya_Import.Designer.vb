<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EMI_Detail_Hutang_Biaya_Import
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Txt_NoPO = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txt_TanggalPO = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Txt_Kategori = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Txt_Perusahaan = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
        Me.Lv_DetailHutang = New System.Windows.Forms.ListView()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Txt_TanggalJatuhTempo = New System.Windows.Forms.TextBox()
        Me.Txt_KdPerusahaanBiayaImport = New System.Windows.Forms.TextBox()
        Me.Txt_KdMasterKategori = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Txt_TotalHutang = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Txt_TotalBayar = New System.Windows.Forms.TextBox()
        Me.Txt_Sisa = New System.Windows.Forms.TextBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Txt_MataUang = New System.Windows.Forms.TextBox()
        Me.Txt_MataUang2 = New System.Windows.Forms.TextBox()
        Me.Txt_MataUang3 = New System.Windows.Forms.TextBox()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(987, 57)
        Me.Panel1.TabIndex = 83
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 18)
        Me.Label2.Margin = New System.Windows.Forms.Padding(7, 0, 7, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(155, 25)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Detail Hutang"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 57)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1581, 17)
        Me.Panel2.TabIndex = 84
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 75)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(20, 839)
        Me.Panel3.TabIndex = 85
        Me.Panel3.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(26, 78)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 18)
        Me.Label1.TabIndex = 86
        Me.Label1.Text = "No PO"
        '
        'Txt_NoPO
        '
        Me.Txt_NoPO.Enabled = False
        Me.Txt_NoPO.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_NoPO.Location = New System.Drawing.Point(175, 77)
        Me.Txt_NoPO.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_NoPO.Name = "Txt_NoPO"
        Me.Txt_NoPO.Size = New System.Drawing.Size(266, 23)
        Me.Txt_NoPO.TabIndex = 87
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(26, 109)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 18)
        Me.Label3.TabIndex = 86
        Me.Label3.Text = "Tanggal PO"
        '
        'Txt_TanggalPO
        '
        Me.Txt_TanggalPO.Enabled = False
        Me.Txt_TanggalPO.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_TanggalPO.Location = New System.Drawing.Point(175, 108)
        Me.Txt_TanggalPO.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_TanggalPO.Name = "Txt_TanggalPO"
        Me.Txt_TanggalPO.Size = New System.Drawing.Size(266, 23)
        Me.Txt_TanggalPO.TabIndex = 87
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(524, 78)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 18)
        Me.Label4.TabIndex = 86
        Me.Label4.Text = "Kategori"
        '
        'Txt_Kategori
        '
        Me.Txt_Kategori.Enabled = False
        Me.Txt_Kategori.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Kategori.Location = New System.Drawing.Point(652, 75)
        Me.Txt_Kategori.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_Kategori.Name = "Txt_Kategori"
        Me.Txt_Kategori.Size = New System.Drawing.Size(266, 23)
        Me.Txt_Kategori.TabIndex = 87
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(970, 78)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(20, 839)
        Me.Panel4.TabIndex = 85
        Me.Panel4.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(524, 109)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 18)
        Me.Label5.TabIndex = 86
        Me.Label5.Text = "Perusahaan"
        '
        'Txt_Perusahaan
        '
        Me.Txt_Perusahaan.Enabled = False
        Me.Txt_Perusahaan.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Perusahaan.Location = New System.Drawing.Point(652, 106)
        Me.Txt_Perusahaan.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_Perusahaan.Name = "Txt_Perusahaan"
        Me.Txt_Perusahaan.Size = New System.Drawing.Size(266, 23)
        Me.Txt_Perusahaan.TabIndex = 87
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(524, 140)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(79, 18)
        Me.Label6.TabIndex = 86
        Me.Label6.Text = "Keterangan"
        '
        'Txt_Keterangan
        '
        Me.Txt_Keterangan.Enabled = False
        Me.Txt_Keterangan.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Keterangan.Location = New System.Drawing.Point(652, 137)
        Me.Txt_Keterangan.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_Keterangan.Name = "Txt_Keterangan"
        Me.Txt_Keterangan.Size = New System.Drawing.Size(266, 23)
        Me.Txt_Keterangan.TabIndex = 87
        '
        'Lv_DetailHutang
        '
        Me.Lv_DetailHutang.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_DetailHutang.FullRowSelect = True
        Me.Lv_DetailHutang.GridLines = True
        Me.Lv_DetailHutang.HideSelection = False
        Me.Lv_DetailHutang.Location = New System.Drawing.Point(6, 22)
        Me.Lv_DetailHutang.Name = "Lv_DetailHutang"
        Me.Lv_DetailHutang.Size = New System.Drawing.Size(940, 275)
        Me.Lv_DetailHutang.TabIndex = 88
        Me.Lv_DetailHutang.UseCompatibleStateImageBehavior = False
        Me.Lv_DetailHutang.View = System.Windows.Forms.View.Details
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(26, 138)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(141, 18)
        Me.Label7.TabIndex = 86
        Me.Label7.Text = "Tanggal Jatuh Tempo"
        '
        'Txt_TanggalJatuhTempo
        '
        Me.Txt_TanggalJatuhTempo.Enabled = False
        Me.Txt_TanggalJatuhTempo.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_TanggalJatuhTempo.Location = New System.Drawing.Point(175, 137)
        Me.Txt_TanggalJatuhTempo.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_TanggalJatuhTempo.Name = "Txt_TanggalJatuhTempo"
        Me.Txt_TanggalJatuhTempo.Size = New System.Drawing.Size(266, 23)
        Me.Txt_TanggalJatuhTempo.TabIndex = 87
        '
        'Txt_KdPerusahaanBiayaImport
        '
        Me.Txt_KdPerusahaanBiayaImport.Enabled = False
        Me.Txt_KdPerusahaanBiayaImport.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_KdPerusahaanBiayaImport.Location = New System.Drawing.Point(999, 95)
        Me.Txt_KdPerusahaanBiayaImport.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_KdPerusahaanBiayaImport.Name = "Txt_KdPerusahaanBiayaImport"
        Me.Txt_KdPerusahaanBiayaImport.Size = New System.Drawing.Size(64, 23)
        Me.Txt_KdPerusahaanBiayaImport.TabIndex = 87
        '
        'Txt_KdMasterKategori
        '
        Me.Txt_KdMasterKategori.Enabled = False
        Me.Txt_KdMasterKategori.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_KdMasterKategori.Location = New System.Drawing.Point(999, 126)
        Me.Txt_KdMasterKategori.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_KdMasterKategori.Name = "Txt_KdMasterKategori"
        Me.Txt_KdMasterKategori.Size = New System.Drawing.Size(64, 23)
        Me.Txt_KdMasterKategori.TabIndex = 87
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Lv_DetailHutang)
        Me.GroupBox1.Location = New System.Drawing.Point(23, 167)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(952, 309)
        Me.GroupBox1.TabIndex = 89
        Me.GroupBox1.TabStop = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(574, 511)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(83, 18)
        Me.Label8.TabIndex = 86
        Me.Label8.Text = "Total Bayar"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(574, 479)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(92, 18)
        Me.Label9.TabIndex = 86
        Me.Label9.Text = "Total Hutang"
        '
        'Txt_TotalHutang
        '
        Me.Txt_TotalHutang.Enabled = False
        Me.Txt_TotalHutang.Location = New System.Drawing.Point(707, 479)
        Me.Txt_TotalHutang.Name = "Txt_TotalHutang"
        Me.Txt_TotalHutang.ReadOnly = True
        Me.Txt_TotalHutang.Size = New System.Drawing.Size(198, 23)
        Me.Txt_TotalHutang.TabIndex = 90
        Me.Txt_TotalHutang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(574, 540)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(35, 18)
        Me.Label10.TabIndex = 86
        Me.Label10.Text = "Sisa"
        '
        'Txt_TotalBayar
        '
        Me.Txt_TotalBayar.Enabled = False
        Me.Txt_TotalBayar.Location = New System.Drawing.Point(707, 508)
        Me.Txt_TotalBayar.Name = "Txt_TotalBayar"
        Me.Txt_TotalBayar.ReadOnly = True
        Me.Txt_TotalBayar.Size = New System.Drawing.Size(198, 23)
        Me.Txt_TotalBayar.TabIndex = 90
        Me.Txt_TotalBayar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Txt_Sisa
        '
        Me.Txt_Sisa.Enabled = False
        Me.Txt_Sisa.Location = New System.Drawing.Point(707, 537)
        Me.Txt_Sisa.Name = "Txt_Sisa"
        Me.Txt_Sisa.ReadOnly = True
        Me.Txt_Sisa.Size = New System.Drawing.Size(198, 23)
        Me.Txt_Sisa.TabIndex = 90
        Me.Txt_Sisa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(29, 564)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1581, 17)
        Me.Panel5.TabIndex = 84
        Me.Panel5.Visible = False
        '
        'Txt_MataUang
        '
        Me.Txt_MataUang.Enabled = False
        Me.Txt_MataUang.Location = New System.Drawing.Point(911, 479)
        Me.Txt_MataUang.Name = "Txt_MataUang"
        Me.Txt_MataUang.ReadOnly = True
        Me.Txt_MataUang.Size = New System.Drawing.Size(58, 23)
        Me.Txt_MataUang.TabIndex = 90
        '
        'Txt_MataUang2
        '
        Me.Txt_MataUang2.Enabled = False
        Me.Txt_MataUang2.Location = New System.Drawing.Point(911, 507)
        Me.Txt_MataUang2.Name = "Txt_MataUang2"
        Me.Txt_MataUang2.ReadOnly = True
        Me.Txt_MataUang2.Size = New System.Drawing.Size(58, 23)
        Me.Txt_MataUang2.TabIndex = 90
        '
        'Txt_MataUang3
        '
        Me.Txt_MataUang3.Enabled = False
        Me.Txt_MataUang3.Location = New System.Drawing.Point(911, 536)
        Me.Txt_MataUang3.Name = "Txt_MataUang3"
        Me.Txt_MataUang3.ReadOnly = True
        Me.Txt_MataUang3.Size = New System.Drawing.Size(58, 23)
        Me.Txt_MataUang3.TabIndex = 90
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 54)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(987, 3)
        Me.PanelGradient1.TabIndex = 22
        '
        'EMI_Detail_Hutang_Biaya_Import
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(987, 581)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Txt_MataUang3)
        Me.Controls.Add(Me.Txt_MataUang2)
        Me.Controls.Add(Me.Txt_MataUang)
        Me.Controls.Add(Me.Txt_Sisa)
        Me.Controls.Add(Me.Txt_TotalBayar)
        Me.Controls.Add(Me.Txt_TotalHutang)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Txt_Keterangan)
        Me.Controls.Add(Me.Txt_TanggalJatuhTempo)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Txt_TanggalPO)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Txt_KdMasterKategori)
        Me.Controls.Add(Me.Txt_KdPerusahaanBiayaImport)
        Me.Controls.Add(Me.Txt_Perusahaan)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Txt_Kategori)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Txt_NoPO)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Detail_Hutang_Biaya_Import"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_NoPO As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Txt_TanggalPO As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Txt_Kategori As TextBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Txt_Perusahaan As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Txt_Keterangan As TextBox
    Friend WithEvents Lv_DetailHutang As ListView
    Friend WithEvents Label7 As Label
    Friend WithEvents Txt_TanggalJatuhTempo As TextBox
    Friend WithEvents Txt_KdPerusahaanBiayaImport As TextBox
    Friend WithEvents Txt_KdMasterKategori As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Txt_TotalHutang As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Txt_TotalBayar As TextBox
    Friend WithEvents Txt_Sisa As TextBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Txt_MataUang As TextBox
    Friend WithEvents Txt_MataUang2 As TextBox
    Friend WithEvents Txt_MataUang3 As TextBox
End Class
