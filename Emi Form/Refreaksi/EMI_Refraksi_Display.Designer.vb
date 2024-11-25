<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Refraksi_Display
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Cmb_Filter = New System.Windows.Forms.ComboBox()
        Me.Txt_ValueFilter = New System.Windows.Forms.TextBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Lv_PO = New System.Windows.Forms.ListView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Txt_HargaRefraksi = New System.Windows.Forms.TextBox()
        Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
        Me.Txt_NoPO = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Txt_HargaPO = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Txt_JmlhPO = New System.Windows.Forms.TextBox()
        Me.Cmb_Satuan = New System.Windows.Forms.ComboBox()
        Me.Cmb_MataUang = New System.Windows.Forms.ComboBox()
        Me.Txt_NoUrut = New System.Windows.Forms.TextBox()
        Me.Txt_SatuanKecil = New System.Windows.Forms.TextBox()
        Me.label9 = New System.Windows.Forms.Label()
        Me.Txt_KdSo = New System.Windows.Forms.TextBox()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1234, 57)
        Me.Panel1.TabIndex = 23
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(17, 13)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(179, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Display Refraksi"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 67)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 597)
        Me.Panel3.TabIndex = 37
        Me.Panel3.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(-1, 53)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1302, 12)
        Me.Panel2.TabIndex = 36
        Me.Panel2.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(22, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 18)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "FIlter"
        '
        'Cmb_Filter
        '
        Me.Cmb_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Filter.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_Filter.FormattingEnabled = True
        Me.Cmb_Filter.Location = New System.Drawing.Point(93, 68)
        Me.Cmb_Filter.Name = "Cmb_Filter"
        Me.Cmb_Filter.Size = New System.Drawing.Size(182, 25)
        Me.Cmb_Filter.TabIndex = 39
        '
        'Txt_ValueFilter
        '
        Me.Txt_ValueFilter.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_ValueFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_ValueFilter.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_ValueFilter.Location = New System.Drawing.Point(281, 68)
        Me.Txt_ValueFilter.MaxLength = 50
        Me.Txt_ValueFilter.Name = "Txt_ValueFilter"
        Me.Txt_ValueFilter.Size = New System.Drawing.Size(231, 23)
        Me.Txt_ValueFilter.TabIndex = 236
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(518, 66)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(88, 28)
        Me.Btn_Cari.TabIndex = 373
        Me.Btn_Cari.Text = "Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Lv_PO
        '
        Me.Lv_PO.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_PO.FullRowSelect = True
        Me.Lv_PO.GridLines = True
        Me.Lv_PO.HideSelection = False
        Me.Lv_PO.Location = New System.Drawing.Point(19, 103)
        Me.Lv_PO.Name = "Lv_PO"
        Me.Lv_PO.Size = New System.Drawing.Size(739, 447)
        Me.Lv_PO.TabIndex = 374
        Me.Lv_PO.UseCompatibleStateImageBehavior = False
        Me.Lv_PO.View = System.Windows.Forms.View.Details
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(19, 94)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1302, 12)
        Me.Panel4.TabIndex = 36
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(759, 114)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(15, 597)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(1216, 114)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(19, 597)
        Me.Panel6.TabIndex = 37
        Me.Panel6.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Cmb_MataUang)
        Me.GroupBox1.Controls.Add(Me.Cmb_Satuan)
        Me.GroupBox1.Controls.Add(Me.Btn_Simpan)
        Me.GroupBox1.Controls.Add(Me.Txt_HargaRefraksi)
        Me.GroupBox1.Controls.Add(Me.Txt_JmlhPO)
        Me.GroupBox1.Controls.Add(Me.Txt_NmBarang)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Txt_KdBarang)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Txt_SatuanKecil)
        Me.GroupBox1.Controls.Add(Me.Txt_NoUrut)
        Me.GroupBox1.Controls.Add(Me.Txt_KdSo)
        Me.GroupBox1.Controls.Add(Me.Txt_NoPO)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.label9)
        Me.GroupBox1.Controls.Add(Me.Txt_HargaPO)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(775, 94)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(440, 452)
        Me.GroupBox1.TabIndex = 375
        Me.GroupBox1.TabStop = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(126, 221)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(162, 34)
        Me.Btn_Simpan.TabIndex = 486
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Txt_HargaRefraksi
        '
        Me.Txt_HargaRefraksi.BackColor = System.Drawing.Color.White
        Me.Txt_HargaRefraksi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_HargaRefraksi.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_HargaRefraksi.Location = New System.Drawing.Point(129, 193)
        Me.Txt_HargaRefraksi.MaxLength = 100
        Me.Txt_HargaRefraksi.Name = "Txt_HargaRefraksi"
        Me.Txt_HargaRefraksi.Size = New System.Drawing.Size(289, 22)
        Me.Txt_HargaRefraksi.TabIndex = 485
        '
        'Txt_KdBarang
        '
        Me.Txt_KdBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KdBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_KdBarang.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_KdBarang.Location = New System.Drawing.Point(129, 78)
        Me.Txt_KdBarang.MaxLength = 100
        Me.Txt_KdBarang.Name = "Txt_KdBarang"
        Me.Txt_KdBarang.ReadOnly = True
        Me.Txt_KdBarang.Size = New System.Drawing.Size(289, 22)
        Me.Txt_KdBarang.TabIndex = 485
        '
        'Txt_NoPO
        '
        Me.Txt_NoPO.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NoPO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NoPO.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_NoPO.Location = New System.Drawing.Point(129, 22)
        Me.Txt_NoPO.MaxLength = 100
        Me.Txt_NoPO.Name = "Txt_NoPO"
        Me.Txt_NoPO.ReadOnly = True
        Me.Txt_NoPO.Size = New System.Drawing.Size(289, 22)
        Me.Txt_NoPO.TabIndex = 485
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 79)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 18)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Kode Barang"
        '
        'Txt_HargaPO
        '
        Me.Txt_HargaPO.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_HargaPO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_HargaPO.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_HargaPO.Location = New System.Drawing.Point(207, 165)
        Me.Txt_HargaPO.MaxLength = 100
        Me.Txt_HargaPO.Name = "Txt_HargaPO"
        Me.Txt_HargaPO.ReadOnly = True
        Me.Txt_HargaPO.Size = New System.Drawing.Size(211, 22)
        Me.Txt_HargaPO.TabIndex = 485
        Me.Txt_HargaPO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 18)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "No PO"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 194)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 18)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Harga Refraksi"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 166)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 18)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Harga PO"
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(22, 551)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1302, 12)
        Me.Panel7.TabIndex = 36
        Me.Panel7.Visible = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(612, 66)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(88, 28)
        Me.Btn_Refresh.TabIndex = 373
        Me.Btn_Refresh.Text = "Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(15, 107)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 18)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Nama Barang"
        '
        'Txt_NmBarang
        '
        Me.Txt_NmBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NmBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NmBarang.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_NmBarang.Location = New System.Drawing.Point(129, 106)
        Me.Txt_NmBarang.MaxLength = 100
        Me.Txt_NmBarang.Name = "Txt_NmBarang"
        Me.Txt_NmBarang.ReadOnly = True
        Me.Txt_NmBarang.Size = New System.Drawing.Size(289, 22)
        Me.Txt_NmBarang.TabIndex = 485
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(15, 138)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(54, 18)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Jumlah"
        '
        'Txt_JmlhPO
        '
        Me.Txt_JmlhPO.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_JmlhPO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_JmlhPO.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_JmlhPO.Location = New System.Drawing.Point(129, 134)
        Me.Txt_JmlhPO.MaxLength = 100
        Me.Txt_JmlhPO.Name = "Txt_JmlhPO"
        Me.Txt_JmlhPO.ReadOnly = True
        Me.Txt_JmlhPO.Size = New System.Drawing.Size(213, 22)
        Me.Txt_JmlhPO.TabIndex = 485
        Me.Txt_JmlhPO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Cmb_Satuan
        '
        Me.Cmb_Satuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Satuan.Enabled = False
        Me.Cmb_Satuan.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_Satuan.FormattingEnabled = True
        Me.Cmb_Satuan.Location = New System.Drawing.Point(348, 133)
        Me.Cmb_Satuan.Name = "Cmb_Satuan"
        Me.Cmb_Satuan.Size = New System.Drawing.Size(70, 25)
        Me.Cmb_Satuan.TabIndex = 487
        '
        'Cmb_MataUang
        '
        Me.Cmb_MataUang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_MataUang.Enabled = False
        Me.Cmb_MataUang.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_MataUang.FormattingEnabled = True
        Me.Cmb_MataUang.Location = New System.Drawing.Point(129, 162)
        Me.Cmb_MataUang.Name = "Cmb_MataUang"
        Me.Cmb_MataUang.Size = New System.Drawing.Size(72, 25)
        Me.Cmb_MataUang.TabIndex = 487
        '
        'Txt_NoUrut
        '
        Me.Txt_NoUrut.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NoUrut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NoUrut.Enabled = False
        Me.Txt_NoUrut.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_NoUrut.Location = New System.Drawing.Point(398, 424)
        Me.Txt_NoUrut.MaxLength = 100
        Me.Txt_NoUrut.Name = "Txt_NoUrut"
        Me.Txt_NoUrut.Size = New System.Drawing.Size(35, 22)
        Me.Txt_NoUrut.TabIndex = 485
        Me.Txt_NoUrut.Visible = False
        '
        'Txt_SatuanKecil
        '
        Me.Txt_SatuanKecil.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_SatuanKecil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_SatuanKecil.Enabled = False
        Me.Txt_SatuanKecil.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_SatuanKecil.Location = New System.Drawing.Point(357, 424)
        Me.Txt_SatuanKecil.MaxLength = 100
        Me.Txt_SatuanKecil.Name = "Txt_SatuanKecil"
        Me.Txt_SatuanKecil.Size = New System.Drawing.Size(35, 22)
        Me.Txt_SatuanKecil.TabIndex = 485
        Me.Txt_SatuanKecil.Visible = False
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.Location = New System.Drawing.Point(15, 51)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(48, 18)
        Me.label9.TabIndex = 0
        Me.label9.Text = "Lokasi"
        '
        'Txt_KdSo
        '
        Me.Txt_KdSo.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KdSo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_KdSo.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_KdSo.Location = New System.Drawing.Point(129, 50)
        Me.Txt_KdSo.MaxLength = 100
        Me.Txt_KdSo.Name = "Txt_KdSo"
        Me.Txt_KdSo.ReadOnly = True
        Me.Txt_KdSo.Size = New System.Drawing.Size(289, 22)
        Me.Txt_KdSo.TabIndex = 485
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 55)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(1234, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'EMI_Refraksi_Display
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1234, 562)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Lv_PO)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Cari)
        Me.Controls.Add(Me.Txt_ValueFilter)
        Me.Controls.Add(Me.Cmb_Filter)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Name = "EMI_Refraksi_Display"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Cmb_Filter As ComboBox
    Friend WithEvents Txt_ValueFilter As TextBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Lv_PO As ListView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Txt_HargaPO As TextBox
    Friend WithEvents Txt_HargaRefraksi As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Txt_NoPO As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Txt_NmBarang As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Txt_JmlhPO As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Cmb_Satuan As ComboBox
    Friend WithEvents Cmb_MataUang As ComboBox
    Friend WithEvents Txt_NoUrut As TextBox
    Friend WithEvents Txt_SatuanKecil As TextBox
    Friend WithEvents Txt_KdSo As TextBox
    Friend WithEvents label9 As Label
End Class
