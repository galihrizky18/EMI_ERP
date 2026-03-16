<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class N_EMI_SD_Trial_Request_Material
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
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Lv_Data_Formula_Pending = New System.Windows.Forms.ListView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Lv_Detail_Bahan = New System.Windows.Forms.ListView()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Btn_Get_Formula = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
        Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
        Me.Lv_Barang = New System.Windows.Forms.ListView()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_Total_Bahan = New System.Windows.Forms.TextBox()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Cmb_Satuan = New System.Windows.Forms.ComboBox()
        Me.Txt_Kode_Formula = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txt_Jumlah_Kebutuhan = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Cmb_Satuan_Kebutuhan = New System.Windows.Forms.ComboBox()
        Me.Btn_Get_Data = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Lv_Data_Formula_Completed = New System.Windows.Forms.ListView()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Lbl_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(966, 45)
        Me.Panel1.TabIndex = 234
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 42)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(966, 3)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(20, 7)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(7, 0, 7, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(251, 30)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Show Dialog - Formula"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Controls.Add(Me.Panel9)
        Me.Panel3.Location = New System.Drawing.Point(0, 54)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 577)
        Me.Panel3.TabIndex = 242
        Me.Panel3.Visible = False
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.Red
        Me.Panel9.Location = New System.Drawing.Point(11, 130)
        Me.Panel9.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(955, 11)
        Me.Panel9.TabIndex = 38
        Me.Panel9.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(1, 45)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(951, 12)
        Me.Panel2.TabIndex = 243
        Me.Panel2.Visible = False
        '
        'Lv_Data_Formula_Pending
        '
        Me.Lv_Data_Formula_Pending.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lv_Data_Formula_Pending.FullRowSelect = True
        Me.Lv_Data_Formula_Pending.GridLines = True
        Me.Lv_Data_Formula_Pending.HideSelection = False
        Me.Lv_Data_Formula_Pending.Location = New System.Drawing.Point(3, 3)
        Me.Lv_Data_Formula_Pending.Name = "Lv_Data_Formula_Pending"
        Me.Lv_Data_Formula_Pending.Size = New System.Drawing.Size(284, 435)
        Me.Lv_Data_Formula_Pending.TabIndex = 0
        Me.Lv_Data_Formula_Pending.UseCompatibleStateImageBehavior = False
        Me.Lv_Data_Formula_Pending.View = System.Windows.Forms.View.Details
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Location = New System.Drawing.Point(312, 138)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(12, 577)
        Me.Panel4.TabIndex = 242
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(11, 130)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(955, 11)
        Me.Panel5.TabIndex = 0
        Me.Panel5.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Lv_Detail_Bahan)
        Me.GroupBox2.Location = New System.Drawing.Point(326, 214)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(617, 298)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Detail Bahan"
        '
        'Lv_Detail_Bahan
        '
        Me.Lv_Detail_Bahan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lv_Detail_Bahan.FullRowSelect = True
        Me.Lv_Detail_Bahan.GridLines = True
        Me.Lv_Detail_Bahan.HideSelection = False
        Me.Lv_Detail_Bahan.Location = New System.Drawing.Point(3, 16)
        Me.Lv_Detail_Bahan.Name = "Lv_Detail_Bahan"
        Me.Lv_Detail_Bahan.Size = New System.Drawing.Size(611, 279)
        Me.Lv_Detail_Bahan.TabIndex = 0
        Me.Lv_Detail_Bahan.UseCompatibleStateImageBehavior = False
        Me.Lv_Detail_Bahan.View = System.Windows.Forms.View.Details
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(15, 596)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(951, 15)
        Me.Panel6.TabIndex = 243
        Me.Panel6.Visible = False
        '
        'Btn_Get_Formula
        '
        Me.Btn_Get_Formula.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Get_Formula.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Get_Formula.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Get_Formula.ForeColor = System.Drawing.Color.White
        Me.Btn_Get_Formula.Location = New System.Drawing.Point(146, 82)
        Me.Btn_Get_Formula.Name = "Btn_Get_Formula"
        Me.Btn_Get_Formula.Size = New System.Drawing.Size(122, 28)
        Me.Btn_Get_Formula.TabIndex = 2
        Me.Btn_Get_Formula.Text = "Get Formula"
        Me.Btn_Get_Formula.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(21, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 17)
        Me.Label1.TabIndex = 406
        Me.Label1.Text = "Kode Barang"
        '
        'Txt_NmBarang
        '
        Me.Txt_NmBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NmBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NmBarang.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_NmBarang.Location = New System.Drawing.Point(336, 59)
        Me.Txt_NmBarang.MaxLength = 50
        Me.Txt_NmBarang.Name = "Txt_NmBarang"
        Me.Txt_NmBarang.Size = New System.Drawing.Size(332, 20)
        Me.Txt_NmBarang.TabIndex = 1
        '
        'Txt_KdBarang
        '
        Me.Txt_KdBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KdBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_KdBarang.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_KdBarang.Location = New System.Drawing.Point(146, 59)
        Me.Txt_KdBarang.MaxLength = 50
        Me.Txt_KdBarang.Name = "Txt_KdBarang"
        Me.Txt_KdBarang.Size = New System.Drawing.Size(186, 20)
        Me.Txt_KdBarang.TabIndex = 0
        '
        'Lv_Barang
        '
        Me.Lv_Barang.FullRowSelect = True
        Me.Lv_Barang.GridLines = True
        Me.Lv_Barang.HideSelection = False
        Me.Lv_Barang.Location = New System.Drawing.Point(973, 128)
        Me.Lv_Barang.Name = "Lv_Barang"
        Me.Lv_Barang.Size = New System.Drawing.Size(522, 231)
        Me.Lv_Barang.TabIndex = 410
        Me.Lv_Barang.UseCompatibleStateImageBehavior = False
        Me.Lv_Barang.View = System.Windows.Forms.View.Details
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(19, 109)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(951, 12)
        Me.Panel7.TabIndex = 243
        Me.Panel7.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(628, 529)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 17)
        Me.Label2.TabIndex = 412
        Me.Label2.Text = "Total Bahan"
        '
        'Txt_Total_Bahan
        '
        Me.Txt_Total_Bahan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Total_Bahan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Total_Bahan.Enabled = False
        Me.Txt_Total_Bahan.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_Total_Bahan.Location = New System.Drawing.Point(711, 528)
        Me.Txt_Total_Bahan.MaxLength = 50
        Me.Txt_Total_Bahan.Name = "Txt_Total_Bahan"
        Me.Txt_Total_Bahan.Size = New System.Drawing.Size(150, 20)
        Me.Txt_Total_Bahan.TabIndex = 5
        Me.Txt_Total_Bahan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(470, 562)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(138, 32)
        Me.Btn_Refresh.TabIndex = 9
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(326, 562)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(138, 32)
        Me.Btn_Simpan.TabIndex = 8
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Controls.Add(Me.Panel10)
        Me.Panel8.Location = New System.Drawing.Point(947, 65)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(19, 577)
        Me.Panel8.TabIndex = 242
        Me.Panel8.Visible = False
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.Red
        Me.Panel10.Location = New System.Drawing.Point(11, 130)
        Me.Panel10.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(955, 11)
        Me.Panel10.TabIndex = 38
        Me.Panel10.Visible = False
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.Color.Red
        Me.Panel11.Location = New System.Drawing.Point(322, 549)
        Me.Panel11.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(951, 12)
        Me.Panel11.TabIndex = 243
        Me.Panel11.Visible = False
        '
        'Panel12
        '
        Me.Panel12.BackColor = System.Drawing.Color.Red
        Me.Panel12.Location = New System.Drawing.Point(327, 514)
        Me.Panel12.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(951, 12)
        Me.Panel12.TabIndex = 243
        Me.Panel12.Visible = False
        '
        'Cmb_Satuan
        '
        Me.Cmb_Satuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Satuan.Enabled = False
        Me.Cmb_Satuan.FormattingEnabled = True
        Me.Cmb_Satuan.Location = New System.Drawing.Point(867, 525)
        Me.Cmb_Satuan.Name = "Cmb_Satuan"
        Me.Cmb_Satuan.Size = New System.Drawing.Size(73, 24)
        Me.Cmb_Satuan.TabIndex = 6
        '
        'Txt_Kode_Formula
        '
        Me.Txt_Kode_Formula.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Kode_Formula.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Kode_Formula.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_Kode_Formula.Location = New System.Drawing.Point(461, 128)
        Me.Txt_Kode_Formula.MaxLength = 50
        Me.Txt_Kode_Formula.Name = "Txt_Kode_Formula"
        Me.Txt_Kode_Formula.Size = New System.Drawing.Size(295, 20)
        Me.Txt_Kode_Formula.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(336, 130)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 17)
        Me.Label3.TabIndex = 406
        Me.Label3.Text = "Kode Formula"
        '
        'Txt_Jumlah_Kebutuhan
        '
        Me.Txt_Jumlah_Kebutuhan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Jumlah_Kebutuhan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Jumlah_Kebutuhan.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_Jumlah_Kebutuhan.Location = New System.Drawing.Point(461, 154)
        Me.Txt_Jumlah_Kebutuhan.MaxLength = 50
        Me.Txt_Jumlah_Kebutuhan.Name = "Txt_Jumlah_Kebutuhan"
        Me.Txt_Jumlah_Kebutuhan.Size = New System.Drawing.Size(191, 20)
        Me.Txt_Jumlah_Kebutuhan.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(336, 156)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 17)
        Me.Label4.TabIndex = 406
        Me.Label4.Text = "Kebutuhan"
        '
        'Cmb_Satuan_Kebutuhan
        '
        Me.Cmb_Satuan_Kebutuhan.Enabled = False
        Me.Cmb_Satuan_Kebutuhan.FormattingEnabled = True
        Me.Cmb_Satuan_Kebutuhan.Location = New System.Drawing.Point(658, 153)
        Me.Cmb_Satuan_Kebutuhan.Name = "Cmb_Satuan_Kebutuhan"
        Me.Cmb_Satuan_Kebutuhan.Size = New System.Drawing.Size(98, 24)
        Me.Cmb_Satuan_Kebutuhan.TabIndex = 5
        '
        'Btn_Get_Data
        '
        Me.Btn_Get_Data.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Get_Data.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Get_Data.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Get_Data.ForeColor = System.Drawing.Color.White
        Me.Btn_Get_Data.Location = New System.Drawing.Point(461, 180)
        Me.Btn_Get_Data.Name = "Btn_Get_Data"
        Me.Btn_Get_Data.Size = New System.Drawing.Size(122, 28)
        Me.Btn_Get_Data.TabIndex = 6
        Me.Btn_Get_Data.Text = "Get Data"
        Me.Btn_Get_Data.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(17, 123)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(298, 470)
        Me.TabControl1.TabIndex = 3
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.White
        Me.TabPage1.Controls.Add(Me.Lv_Data_Formula_Pending)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(290, 441)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Pending Trial"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Lv_Data_Formula_Completed)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(290, 441)
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
        Me.Lv_Data_Formula_Completed.Size = New System.Drawing.Size(284, 435)
        Me.Lv_Data_Formula_Completed.TabIndex = 1
        Me.Lv_Data_Formula_Completed.UseCompatibleStateImageBehavior = False
        Me.Lv_Data_Formula_Completed.View = System.Windows.Forms.View.Details
        '
        'N_EMI_SD_Trial_Request_Material
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(966, 611)
        Me.Controls.Add(Me.Lv_Barang)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Cmb_Satuan_Kebutuhan)
        Me.Controls.Add(Me.Cmb_Satuan)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Txt_Total_Bahan)
        Me.Controls.Add(Me.Btn_Get_Data)
        Me.Controls.Add(Me.Btn_Get_Formula)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Txt_NmBarang)
        Me.Controls.Add(Me.Txt_Jumlah_Kebutuhan)
        Me.Controls.Add(Me.Txt_Kode_Formula)
        Me.Controls.Add(Me.Txt_KdBarang)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel11)
        Me.Controls.Add(Me.Panel12)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_SD_Trial_Request_Material"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Lv_Data_Formula_Pending As ListView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Lv_Detail_Bahan As ListView
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Btn_Get_Formula As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_NmBarang As TextBox
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents Lv_Barang As ListView
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_Total_Bahan As TextBox
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Panel11 As Panel
    Friend WithEvents Panel12 As Panel
    Friend WithEvents Cmb_Satuan As ComboBox
    Friend WithEvents Txt_Kode_Formula As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Txt_Jumlah_Kebutuhan As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Cmb_Satuan_Kebutuhan As ComboBox
    Friend WithEvents Btn_Get_Data As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Lv_Data_Formula_Completed As ListView
End Class
