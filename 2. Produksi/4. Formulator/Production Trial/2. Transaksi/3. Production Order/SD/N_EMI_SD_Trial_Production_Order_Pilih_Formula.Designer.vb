<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class N_EMI_SD_Trial_Production_Order_Pilih_Formula
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
        Me.LblPilihBarang_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
        Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
        Me.Btn_Get_Formula = New System.Windows.Forms.Button()
        Me.Lv_Data_Formula_Pending = New System.Windows.Forms.ListView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Lv_Detail_Bahan = New System.Windows.Forms.ListView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Txt_Jumlah = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Cmb_Satuan = New System.Windows.Forms.ComboBox()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Lv_Barang = New System.Windows.Forms.ListView()
        Me.cmb_Lokasi_Init_Faktur = New System.Windows.Forms.ComboBox()
        Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Txt_TanggaFormula = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Lv_Data_Formula_Completed = New System.Windows.Forms.ListView()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1004, 45)
        Me.Panel1.TabIndex = 23
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 43)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(1004, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'LblPilihBarang_Judul
        '
        Me.LblPilihBarang_Judul.AutoSize = True
        Me.LblPilihBarang_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPilihBarang_Judul.Location = New System.Drawing.Point(15, 7)
        Me.LblPilihBarang_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LblPilihBarang_Judul.Name = "LblPilihBarang_Judul"
        Me.LblPilihBarang_Judul.Size = New System.Drawing.Size(303, 30)
        Me.LblPilihBarang_Judul.TabIndex = 0
        Me.LblPilihBarang_Judul.Text = "Show Dialog - Pilih Formula"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(-3, 43)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 35
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 58)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 582)
        Me.Panel3.TabIndex = 36
        Me.Panel3.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(21, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 17)
        Me.Label1.TabIndex = 402
        Me.Label1.Text = "Kode Barang"
        '
        'Txt_KdBarang
        '
        Me.Txt_KdBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KdBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_KdBarang.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_KdBarang.Location = New System.Drawing.Point(146, 57)
        Me.Txt_KdBarang.MaxLength = 50
        Me.Txt_KdBarang.Name = "Txt_KdBarang"
        Me.Txt_KdBarang.Size = New System.Drawing.Size(186, 20)
        Me.Txt_KdBarang.TabIndex = 0
        '
        'Txt_NmBarang
        '
        Me.Txt_NmBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NmBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NmBarang.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_NmBarang.Location = New System.Drawing.Point(336, 57)
        Me.Txt_NmBarang.MaxLength = 50
        Me.Txt_NmBarang.Name = "Txt_NmBarang"
        Me.Txt_NmBarang.Size = New System.Drawing.Size(332, 20)
        Me.Txt_NmBarang.TabIndex = 1
        '
        'Btn_Get_Formula
        '
        Me.Btn_Get_Formula.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Get_Formula.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Get_Formula.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Get_Formula.ForeColor = System.Drawing.Color.White
        Me.Btn_Get_Formula.Location = New System.Drawing.Point(146, 80)
        Me.Btn_Get_Formula.Name = "Btn_Get_Formula"
        Me.Btn_Get_Formula.Size = New System.Drawing.Size(122, 28)
        Me.Btn_Get_Formula.TabIndex = 2
        Me.Btn_Get_Formula.Text = "Get Formula"
        Me.Btn_Get_Formula.UseVisualStyleBackColor = False
        '
        'Lv_Data_Formula_Pending
        '
        Me.Lv_Data_Formula_Pending.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lv_Data_Formula_Pending.FullRowSelect = True
        Me.Lv_Data_Formula_Pending.GridLines = True
        Me.Lv_Data_Formula_Pending.HideSelection = False
        Me.Lv_Data_Formula_Pending.Location = New System.Drawing.Point(3, 3)
        Me.Lv_Data_Formula_Pending.Name = "Lv_Data_Formula_Pending"
        Me.Lv_Data_Formula_Pending.Size = New System.Drawing.Size(291, 435)
        Me.Lv_Data_Formula_Pending.TabIndex = 0
        Me.Lv_Data_Formula_Pending.UseCompatibleStateImageBehavior = False
        Me.Lv_Data_Formula_Pending.View = System.Windows.Forms.View.Details
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(18, 596)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(942, 15)
        Me.Panel4.TabIndex = 35
        Me.Panel4.Visible = False
        '
        'Lv_Detail_Bahan
        '
        Me.Lv_Detail_Bahan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lv_Detail_Bahan.FullRowSelect = True
        Me.Lv_Detail_Bahan.GridLines = True
        Me.Lv_Detail_Bahan.HideSelection = False
        Me.Lv_Detail_Bahan.Location = New System.Drawing.Point(3, 16)
        Me.Lv_Detail_Bahan.Name = "Lv_Detail_Bahan"
        Me.Lv_Detail_Bahan.Size = New System.Drawing.Size(626, 321)
        Me.Lv_Detail_Bahan.TabIndex = 0
        Me.Lv_Detail_Bahan.UseCompatibleStateImageBehavior = False
        Me.Lv_Detail_Bahan.View = System.Windows.Forms.View.Details
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Lv_Detail_Bahan)
        Me.GroupBox2.Location = New System.Drawing.Point(349, 148)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(632, 340)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Detail Bahan"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(336, 123)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(12, 582)
        Me.Panel5.TabIndex = 36
        Me.Panel5.Visible = False
        '
        'Txt_Jumlah
        '
        Me.Txt_Jumlah.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Jumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Jumlah.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_Jumlah.Location = New System.Drawing.Point(447, 504)
        Me.Txt_Jumlah.MaxLength = 50
        Me.Txt_Jumlah.Name = "Txt_Jumlah"
        Me.Txt_Jumlah.Size = New System.Drawing.Size(186, 20)
        Me.Txt_Jumlah.TabIndex = 5
        Me.Txt_Jumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(349, 505)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 17)
        Me.Label2.TabIndex = 402
        Me.Label2.Text = "Jumlah PO"
        '
        'Cmb_Satuan
        '
        Me.Cmb_Satuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Satuan.Enabled = False
        Me.Cmb_Satuan.FormattingEnabled = True
        Me.Cmb_Satuan.Location = New System.Drawing.Point(639, 502)
        Me.Cmb_Satuan.Name = "Cmb_Satuan"
        Me.Cmb_Satuan.Size = New System.Drawing.Size(121, 24)
        Me.Cmb_Satuan.TabIndex = 6
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(447, 561)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(138, 32)
        Me.Btn_Simpan.TabIndex = 8
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(591, 561)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(138, 32)
        Me.Btn_Refresh.TabIndex = 9
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(245, 490)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(942, 12)
        Me.Panel6.TabIndex = 35
        Me.Panel6.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(237, 550)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(942, 12)
        Me.Panel7.TabIndex = 35
        Me.Panel7.Visible = False
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Location = New System.Drawing.Point(985, 62)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(19, 582)
        Me.Panel8.TabIndex = 36
        Me.Panel8.Visible = False
        '
        'Lv_Barang
        '
        Me.Lv_Barang.FullRowSelect = True
        Me.Lv_Barang.GridLines = True
        Me.Lv_Barang.HideSelection = False
        Me.Lv_Barang.Location = New System.Drawing.Point(1090, 81)
        Me.Lv_Barang.Name = "Lv_Barang"
        Me.Lv_Barang.Size = New System.Drawing.Size(522, 231)
        Me.Lv_Barang.TabIndex = 409
        Me.Lv_Barang.UseCompatibleStateImageBehavior = False
        Me.Lv_Barang.View = System.Windows.Forms.View.Details
        '
        'cmb_Lokasi_Init_Faktur
        '
        Me.cmb_Lokasi_Init_Faktur.FormattingEnabled = True
        Me.cmb_Lokasi_Init_Faktur.Location = New System.Drawing.Point(857, 62)
        Me.cmb_Lokasi_Init_Faktur.Name = "cmb_Lokasi_Init_Faktur"
        Me.cmb_Lokasi_Init_Faktur.Size = New System.Drawing.Size(121, 24)
        Me.cmb_Lokasi_Init_Faktur.TabIndex = 410
        Me.cmb_Lokasi_Init_Faktur.Visible = False
        '
        'Txt_Keterangan
        '
        Me.Txt_Keterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Keterangan.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_Keterangan.Location = New System.Drawing.Point(447, 530)
        Me.Txt_Keterangan.MaxLength = 100
        Me.Txt_Keterangan.Name = "Txt_Keterangan"
        Me.Txt_Keterangan.Size = New System.Drawing.Size(313, 20)
        Me.Txt_Keterangan.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(349, 531)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 17)
        Me.Label3.TabIndex = 402
        Me.Label3.Text = "Keterangan"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(355, 123)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(102, 17)
        Me.Label4.TabIndex = 402
        Me.Label4.Text = "Tanggal Formula"
        '
        'Txt_TanggaFormula
        '
        Me.Txt_TanggaFormula.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_TanggaFormula.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_TanggaFormula.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_TanggaFormula.Location = New System.Drawing.Point(485, 122)
        Me.Txt_TanggaFormula.MaxLength = 50
        Me.Txt_TanggaFormula.Name = "Txt_TanggaFormula"
        Me.Txt_TanggaFormula.Size = New System.Drawing.Size(183, 20)
        Me.Txt_TanggaFormula.TabIndex = 1
        Me.Txt_TanggaFormula.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(24, 123)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(305, 470)
        Me.TabControl1.TabIndex = 411
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.White
        Me.TabPage1.Controls.Add(Me.Lv_Data_Formula_Pending)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(297, 441)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Pending Trial"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Lv_Data_Formula_Completed)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(297, 441)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Completed Trial"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Lv_Data_Formula_Completed
        '
        Me.Lv_Data_Formula_Completed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lv_Data_Formula_Completed.FullRowSelect = True
        Me.Lv_Data_Formula_Completed.GridLines = True
        Me.Lv_Data_Formula_Completed.HideSelection = False
        Me.Lv_Data_Formula_Completed.Location = New System.Drawing.Point(3, 3)
        Me.Lv_Data_Formula_Completed.Name = "Lv_Data_Formula_Completed"
        Me.Lv_Data_Formula_Completed.Size = New System.Drawing.Size(291, 435)
        Me.Lv_Data_Formula_Completed.TabIndex = 1
        Me.Lv_Data_Formula_Completed.UseCompatibleStateImageBehavior = False
        Me.Lv_Data_Formula_Completed.View = System.Windows.Forms.View.Details
        '
        'ListView1
        '
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(1011, 80)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(522, 231)
        Me.ListView1.TabIndex = 409
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'N_EMI_SD_Trial_Production_Order_Pilih_Formula
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1004, 611)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.Lv_Barang)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.cmb_Lokasi_Init_Faktur)
        Me.Controls.Add(Me.Cmb_Satuan)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Btn_Get_Formula)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Txt_TanggaFormula)
        Me.Controls.Add(Me.Txt_NmBarang)
        Me.Controls.Add(Me.Txt_Keterangan)
        Me.Controls.Add(Me.Txt_Jumlah)
        Me.Controls.Add(Me.Txt_KdBarang)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_SD_Trial_Production_Order_Pilih_Formula"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents LblPilihBarang_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents Txt_NmBarang As TextBox
    Friend WithEvents Btn_Get_Formula As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Lv_Data_Formula_Pending As ListView
    Friend WithEvents Lv_Detail_Bahan As ListView
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Txt_Jumlah As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Cmb_Satuan As ComboBox
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Lv_Barang As ListView
    Friend WithEvents cmb_Lokasi_Init_Faktur As ComboBox
    Friend WithEvents Txt_Keterangan As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Txt_TanggaFormula As TextBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Lv_Data_Formula_Completed As ListView
    Friend WithEvents ListView1 As ListView
End Class
