<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class N_EMI_Display_Validasi_Inkubasi_Produksi
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.FLPanel_Data = New System.Windows.Forms.FlowLayoutPanel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel_Isi = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.FPNL_Batch = New System.Windows.Forms.FlowLayoutPanel()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.DTP_Tanggal_Produksi = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
        Me.Txt_SOTujuan = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Txt_SoAwal = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Lv_DataDestail = New System.Windows.Forms.ListView()
        Me.Txt_NoFaktur = New System.Windows.Forms.TextBox()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Cmb_Filter = New System.Windows.Forms.ComboBox()
        Me.Txt_FilterValue = New System.Windows.Forms.TextBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Btn_Refresh_Cari = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel_Isi.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1184, 42)
        Me.Panel1.TabIndex = 25
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 40)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(1184, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 10)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(369, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Validasi - Masa Inkubasi Produksi"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(3, 42)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1278, 10)
        Me.Panel2.TabIndex = 37
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Controls.Add(Me.Panel6)
        Me.Panel3.Location = New System.Drawing.Point(3, 50)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 566)
        Me.Panel3.TabIndex = 38
        Me.Panel3.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(18, 457)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(824, 11)
        Me.Panel6.TabIndex = 35
        Me.Panel6.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Location = New System.Drawing.Point(1165, 47)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 566)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(18, 457)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(824, 11)
        Me.Panel5.TabIndex = 35
        Me.Panel5.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(21, 596)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1278, 15)
        Me.Panel7.TabIndex = 37
        Me.Panel7.Visible = False
        '
        'FLPanel_Data
        '
        Me.FLPanel_Data.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.FLPanel_Data.AutoScroll = True
        Me.FLPanel_Data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FLPanel_Data.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FLPanel_Data.Location = New System.Drawing.Point(22, 91)
        Me.FLPanel_Data.Name = "FLPanel_Data"
        Me.FLPanel_Data.Size = New System.Drawing.Size(240, 508)
        Me.FLPanel_Data.TabIndex = 4
        Me.FLPanel_Data.WrapContents = False
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Controls.Add(Me.Panel9)
        Me.Panel8.Location = New System.Drawing.Point(261, 91)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(12, 526)
        Me.Panel8.TabIndex = 38
        Me.Panel8.Visible = False
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.Red
        Me.Panel9.Location = New System.Drawing.Point(18, 457)
        Me.Panel9.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(824, 11)
        Me.Panel9.TabIndex = 35
        Me.Panel9.Visible = False
        '
        'Panel_Isi
        '
        Me.Panel_Isi.Controls.Add(Me.Label15)
        Me.Panel_Isi.Controls.Add(Me.Label14)
        Me.Panel_Isi.Controls.Add(Me.Label13)
        Me.Panel_Isi.Controls.Add(Me.Panel15)
        Me.Panel_Isi.Controls.Add(Me.Panel16)
        Me.Panel_Isi.Controls.Add(Me.Panel17)
        Me.Panel_Isi.Controls.Add(Me.GroupBox1)
        Me.Panel_Isi.Controls.Add(Me.Panel14)
        Me.Panel_Isi.Controls.Add(Me.DTP_Tanggal_Produksi)
        Me.Panel_Isi.Controls.Add(Me.Label6)
        Me.Panel_Isi.Controls.Add(Me.Txt_Keterangan)
        Me.Panel_Isi.Controls.Add(Me.Txt_SOTujuan)
        Me.Panel_Isi.Controls.Add(Me.Label4)
        Me.Panel_Isi.Controls.Add(Me.Label5)
        Me.Panel_Isi.Controls.Add(Me.Txt_SoAwal)
        Me.Panel_Isi.Controls.Add(Me.Label3)
        Me.Panel_Isi.Controls.Add(Me.Btn_Refresh)
        Me.Panel_Isi.Controls.Add(Me.Btn_Simpan)
        Me.Panel_Isi.Controls.Add(Me.Lv_DataDestail)
        Me.Panel_Isi.Controls.Add(Me.Txt_NoFaktur)
        Me.Panel_Isi.Controls.Add(Me.Panel13)
        Me.Panel_Isi.Controls.Add(Me.Panel12)
        Me.Panel_Isi.Controls.Add(Me.Panel11)
        Me.Panel_Isi.Location = New System.Drawing.Point(274, 91)
        Me.Panel_Isi.Name = "Panel_Isi"
        Me.Panel_Isi.Size = New System.Drawing.Size(890, 509)
        Me.Panel_Isi.TabIndex = 5
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.FPNL_Batch)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 34)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(878, 84)
        Me.GroupBox1.TabIndex = 506
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Batch"
        '
        'FPNL_Batch
        '
        Me.FPNL_Batch.AutoScroll = True
        Me.FPNL_Batch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FPNL_Batch.Location = New System.Drawing.Point(3, 16)
        Me.FPNL_Batch.Name = "FPNL_Batch"
        Me.FPNL_Batch.Size = New System.Drawing.Size(872, 65)
        Me.FPNL_Batch.TabIndex = 505
        '
        'Panel14
        '
        Me.Panel14.BackColor = System.Drawing.Color.Red
        Me.Panel14.Location = New System.Drawing.Point(4, 117)
        Me.Panel14.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(1278, 10)
        Me.Panel14.TabIndex = 37
        Me.Panel14.Visible = False
        '
        'DTP_Tanggal_Produksi
        '
        Me.DTP_Tanggal_Produksi.Enabled = False
        Me.DTP_Tanggal_Produksi.Location = New System.Drawing.Point(656, 3)
        Me.DTP_Tanggal_Produksi.Name = "DTP_Tanggal_Produksi"
        Me.DTP_Tanggal_Produksi.Size = New System.Drawing.Size(234, 20)
        Me.DTP_Tanggal_Produksi.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 444)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(67, 16)
        Me.Label6.TabIndex = 504
        Me.Label6.Text = "Keterangan"
        '
        'Txt_Keterangan
        '
        Me.Txt_Keterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Keterangan.Location = New System.Drawing.Point(83, 440)
        Me.Txt_Keterangan.MaxLength = 225
        Me.Txt_Keterangan.Name = "Txt_Keterangan"
        Me.Txt_Keterangan.Size = New System.Drawing.Size(468, 20)
        Me.Txt_Keterangan.TabIndex = 4
        '
        'Txt_SOTujuan
        '
        Me.Txt_SOTujuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_SOTujuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_SOTujuan.Enabled = False
        Me.Txt_SOTujuan.Location = New System.Drawing.Point(99, 153)
        Me.Txt_SOTujuan.Name = "Txt_SOTujuan"
        Me.Txt_SOTujuan.Size = New System.Drawing.Size(220, 20)
        Me.Txt_SOTujuan.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 156)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 16)
        Me.Label4.TabIndex = 504
        Me.Label4.Text = "Lokasi Tujuan"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(549, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 16)
        Me.Label5.TabIndex = 504
        Me.Label5.Text = "Tanggal Produksi"
        '
        'Txt_SoAwal
        '
        Me.Txt_SoAwal.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_SoAwal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_SoAwal.Enabled = False
        Me.Txt_SoAwal.Location = New System.Drawing.Point(99, 127)
        Me.Txt_SoAwal.Name = "Txt_SoAwal"
        Me.Txt_SoAwal.Size = New System.Drawing.Size(220, 20)
        Me.Txt_SoAwal.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 130)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 16)
        Me.Label3.TabIndex = 504
        Me.Label3.Text = "Lokasi Awal"
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(111, 471)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(102, 35)
        Me.Btn_Refresh.TabIndex = 6
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(6, 471)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(102, 35)
        Me.Btn_Simpan.TabIndex = 5
        Me.Btn_Simpan.Text = "&Validasi"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Lv_DataDestail
        '
        Me.Lv_DataDestail.FullRowSelect = True
        Me.Lv_DataDestail.GridLines = True
        Me.Lv_DataDestail.HideSelection = False
        Me.Lv_DataDestail.Location = New System.Drawing.Point(6, 181)
        Me.Lv_DataDestail.Name = "Lv_DataDestail"
        Me.Lv_DataDestail.Size = New System.Drawing.Size(881, 248)
        Me.Lv_DataDestail.TabIndex = 3
        Me.Lv_DataDestail.UseCompatibleStateImageBehavior = False
        Me.Lv_DataDestail.View = System.Windows.Forms.View.Details
        '
        'Txt_NoFaktur
        '
        Me.Txt_NoFaktur.BackColor = System.Drawing.Color.Goldenrod
        Me.Txt_NoFaktur.Enabled = False
        Me.Txt_NoFaktur.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_NoFaktur.ForeColor = System.Drawing.SystemColors.Window
        Me.Txt_NoFaktur.Location = New System.Drawing.Point(6, 7)
        Me.Txt_NoFaktur.MaxLength = 10
        Me.Txt_NoFaktur.Name = "Txt_NoFaktur"
        Me.Txt_NoFaktur.Size = New System.Drawing.Size(228, 22)
        Me.Txt_NoFaktur.TabIndex = 501
        '
        'Panel13
        '
        Me.Panel13.BackColor = System.Drawing.Color.Red
        Me.Panel13.Location = New System.Drawing.Point(4, 429)
        Me.Panel13.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(1278, 10)
        Me.Panel13.TabIndex = 37
        Me.Panel13.Visible = False
        '
        'Panel12
        '
        Me.Panel12.BackColor = System.Drawing.Color.Red
        Me.Panel12.Location = New System.Drawing.Point(3, 461)
        Me.Panel12.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(1278, 10)
        Me.Panel12.TabIndex = 37
        Me.Panel12.Visible = False
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.Color.Red
        Me.Panel11.Location = New System.Drawing.Point(6, 173)
        Me.Panel11.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(1278, 10)
        Me.Panel11.TabIndex = 37
        Me.Panel11.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(25, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 17)
        Me.Label2.TabIndex = 476
        Me.Label2.Text = "Filter"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Cmb_Filter
        '
        Me.Cmb_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Filter.FormattingEnabled = True
        Me.Cmb_Filter.Location = New System.Drawing.Point(70, 55)
        Me.Cmb_Filter.Name = "Cmb_Filter"
        Me.Cmb_Filter.Size = New System.Drawing.Size(101, 24)
        Me.Cmb_Filter.TabIndex = 0
        '
        'Txt_FilterValue
        '
        Me.Txt_FilterValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_FilterValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_FilterValue.Enabled = False
        Me.Txt_FilterValue.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_FilterValue.Location = New System.Drawing.Point(177, 57)
        Me.Txt_FilterValue.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_FilterValue.Name = "Txt_FilterValue"
        Me.Txt_FilterValue.Size = New System.Drawing.Size(245, 20)
        Me.Txt_FilterValue.TabIndex = 1
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(425, 52)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(81, 29)
        Me.Btn_Cari.TabIndex = 2
        Me.Btn_Cari.Text = "&Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.Red
        Me.Panel10.Location = New System.Drawing.Point(22, 80)
        Me.Panel10.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(1278, 10)
        Me.Panel10.TabIndex = 37
        Me.Panel10.Visible = False
        '
        'Btn_Refresh_Cari
        '
        Me.Btn_Refresh_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh_Cari.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh_Cari.Location = New System.Drawing.Point(509, 52)
        Me.Btn_Refresh_Cari.Name = "Btn_Refresh_Cari"
        Me.Btn_Refresh_Cari.Size = New System.Drawing.Size(81, 29)
        Me.Btn_Refresh_Cari.TabIndex = 3
        Me.Btn_Refresh_Cari.Text = "&Refresh"
        Me.Btn_Refresh_Cari.UseVisualStyleBackColor = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(580, 155)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(105, 16)
        Me.Label15.TabIndex = 510
        Me.Label15.Text = "Ready For Packing"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(706, 155)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(32, 16)
        Me.Label14.TabIndex = 511
        Me.Label14.Text = "Hold"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(758, 155)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(127, 16)
        Me.Label13.TabIndex = 512
        Me.Label13.Text = "Quality Control Reject"
        '
        'Panel15
        '
        Me.Panel15.BackColor = System.Drawing.Color.LightGreen
        Me.Panel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel15.Location = New System.Drawing.Point(564, 156)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(12, 12)
        Me.Panel15.TabIndex = 507
        '
        'Panel16
        '
        Me.Panel16.BackColor = System.Drawing.Color.LightYellow
        Me.Panel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel16.Location = New System.Drawing.Point(690, 156)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(12, 12)
        Me.Panel16.TabIndex = 508
        '
        'Panel17
        '
        Me.Panel17.BackColor = System.Drawing.Color.DarkRed
        Me.Panel17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel17.Location = New System.Drawing.Point(742, 156)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(12, 12)
        Me.Panel17.TabIndex = 509
        '
        'N_EMI_Display_Validasi_Inkubasi_Produksi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1184, 611)
        Me.Controls.Add(Me.Btn_Refresh_Cari)
        Me.Controls.Add(Me.Btn_Cari)
        Me.Controls.Add(Me.Txt_FilterValue)
        Me.Controls.Add(Me.Cmb_Filter)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel_Isi)
        Me.Controls.Add(Me.FLPanel_Data)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel10)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_Display_Validasi_Inkubasi_Produksi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel_Isi.ResumeLayout(False)
        Me.Panel_Isi.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents FLPanel_Data As FlowLayoutPanel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Panel_Isi As Panel
    Friend WithEvents Txt_NoFaktur As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Cmb_Filter As ComboBox
    Friend WithEvents Txt_FilterValue As TextBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Lv_DataDestail As ListView
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Txt_SOTujuan As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Txt_SoAwal As TextBox
    Friend WithEvents Panel11 As Panel
    Friend WithEvents DTP_Tanggal_Produksi As DateTimePicker
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents Panel12 As Panel
    Friend WithEvents Txt_Keterangan As TextBox
    Friend WithEvents Panel13 As Panel
    Friend WithEvents Btn_Refresh_Cari As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents FPNL_Batch As FlowLayoutPanel
    Friend WithEvents Panel14 As Panel
    Friend WithEvents Label15 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Panel15 As Panel
    Friend WithEvents Panel16 As Panel
    Friend WithEvents Panel17 As Panel
End Class
