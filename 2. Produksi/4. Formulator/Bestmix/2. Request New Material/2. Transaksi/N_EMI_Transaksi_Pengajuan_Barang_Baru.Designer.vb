<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Transaksi_Pengajuan_Barang_Baru
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_Kd_Barang = New System.Windows.Forms.TextBox()
        Me.TxtNo_Transaksi = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Cmb_Group_Jenis = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Cmb_Flag_Preservative = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Cmb_Klasifikasi_Bahan_3 = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Cmb_Klasifikasi_Bahan_2 = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Cmb_Klasifikasi_Bahan = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Txt_Nm_Barang = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Lv_Data = New System.Windows.Forms.ListView()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel5.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(884, 42)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(884, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 8)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(262, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Pengajuan Barang Baru"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Controls.Add(Me.Panel6)
        Me.Panel3.Location = New System.Drawing.Point(0, 48)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 566)
        Me.Panel3.TabIndex = 39
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
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 40)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(974, 12)
        Me.Panel2.TabIndex = 38
        Me.Panel2.Visible = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(159, 310)
        Me.Btn_Simpan.Margin = New System.Windows.Forms.Padding(2)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(138, 31)
        Me.Btn_Simpan.TabIndex = 487
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(12, 138)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 17)
        Me.Label2.TabIndex = 488
        Me.Label2.Text = "Kode Barang"
        '
        'Txt_Kd_Barang
        '
        Me.Txt_Kd_Barang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Kd_Barang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Kd_Barang.Enabled = False
        Me.Txt_Kd_Barang.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Txt_Kd_Barang.Location = New System.Drawing.Point(138, 136)
        Me.Txt_Kd_Barang.MaxLength = 40
        Me.Txt_Kd_Barang.Name = "Txt_Kd_Barang"
        Me.Txt_Kd_Barang.Size = New System.Drawing.Size(272, 22)
        Me.Txt_Kd_Barang.TabIndex = 489
        '
        'TxtNo_Transaksi
        '
        Me.TxtNo_Transaksi.BackColor = System.Drawing.Color.Goldenrod
        Me.TxtNo_Transaksi.Enabled = False
        Me.TxtNo_Transaksi.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNo_Transaksi.Location = New System.Drawing.Point(19, 50)
        Me.TxtNo_Transaksi.Margin = New System.Windows.Forms.Padding(2)
        Me.TxtNo_Transaksi.MaxLength = 50
        Me.TxtNo_Transaksi.Name = "TxtNo_Transaksi"
        Me.TxtNo_Transaksi.Size = New System.Drawing.Size(202, 21)
        Me.TxtNo_Transaksi.TabIndex = 490
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(680, 13)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(68, 16)
        Me.Label15.TabIndex = 492
        Me.Label15.Text = "On Process"
        '
        'Panel13
        '
        Me.Panel13.BackColor = System.Drawing.Color.Goldenrod
        Me.Panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel13.Location = New System.Drawing.Point(664, 15)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(12, 12)
        Me.Panel13.TabIndex = 491
        '
        'Cmb_Group_Jenis
        '
        Me.Cmb_Group_Jenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Group_Jenis.FormattingEnabled = True
        Me.Cmb_Group_Jenis.Location = New System.Drawing.Point(138, 16)
        Me.Cmb_Group_Jenis.Name = "Cmb_Group_Jenis"
        Me.Cmb_Group_Jenis.Size = New System.Drawing.Size(272, 24)
        Me.Cmb_Group_Jenis.TabIndex = 493
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(12, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 17)
        Me.Label3.TabIndex = 488
        Me.Label3.Text = "Group Jenis"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Cmb_Flag_Preservative)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Cmb_Klasifikasi_Bahan_3)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Cmb_Klasifikasi_Bahan_2)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Cmb_Klasifikasi_Bahan)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Txt_Nm_Barang)
        Me.GroupBox1.Controls.Add(Me.Txt_Kd_Barang)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Cmb_Group_Jenis)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(21, 72)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(526, 233)
        Me.GroupBox1.TabIndex = 494
        Me.GroupBox1.TabStop = False
        '
        'Cmb_Flag_Preservative
        '
        Me.Cmb_Flag_Preservative.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Flag_Preservative.Enabled = False
        Me.Cmb_Flag_Preservative.FormattingEnabled = True
        Me.Cmb_Flag_Preservative.Location = New System.Drawing.Point(138, 192)
        Me.Cmb_Flag_Preservative.Name = "Cmb_Flag_Preservative"
        Me.Cmb_Flag_Preservative.Size = New System.Drawing.Size(74, 24)
        Me.Cmb_Flag_Preservative.TabIndex = 495
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(12, 194)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(107, 17)
        Me.Label10.TabIndex = 494
        Me.Label10.Text = "Flag Preservative"
        '
        'Cmb_Klasifikasi_Bahan_3
        '
        Me.Cmb_Klasifikasi_Bahan_3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Klasifikasi_Bahan_3.Enabled = False
        Me.Cmb_Klasifikasi_Bahan_3.FormattingEnabled = True
        Me.Cmb_Klasifikasi_Bahan_3.Location = New System.Drawing.Point(138, 106)
        Me.Cmb_Klasifikasi_Bahan_3.Name = "Cmb_Klasifikasi_Bahan_3"
        Me.Cmb_Klasifikasi_Bahan_3.Size = New System.Drawing.Size(272, 24)
        Me.Cmb_Klasifikasi_Bahan_3.TabIndex = 495
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(12, 108)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(118, 17)
        Me.Label9.TabIndex = 494
        Me.Label9.Text = "Klasifikasi Bahan 3"
        '
        'Cmb_Klasifikasi_Bahan_2
        '
        Me.Cmb_Klasifikasi_Bahan_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Klasifikasi_Bahan_2.Enabled = False
        Me.Cmb_Klasifikasi_Bahan_2.FormattingEnabled = True
        Me.Cmb_Klasifikasi_Bahan_2.Location = New System.Drawing.Point(138, 76)
        Me.Cmb_Klasifikasi_Bahan_2.Name = "Cmb_Klasifikasi_Bahan_2"
        Me.Cmb_Klasifikasi_Bahan_2.Size = New System.Drawing.Size(272, 24)
        Me.Cmb_Klasifikasi_Bahan_2.TabIndex = 493
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(12, 78)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(118, 17)
        Me.Label5.TabIndex = 488
        Me.Label5.Text = "Klasifikasi Bahan 2"
        '
        'Cmb_Klasifikasi_Bahan
        '
        Me.Cmb_Klasifikasi_Bahan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Klasifikasi_Bahan.Enabled = False
        Me.Cmb_Klasifikasi_Bahan.FormattingEnabled = True
        Me.Cmb_Klasifikasi_Bahan.Location = New System.Drawing.Point(138, 46)
        Me.Cmb_Klasifikasi_Bahan.Name = "Cmb_Klasifikasi_Bahan"
        Me.Cmb_Klasifikasi_Bahan.Size = New System.Drawing.Size(272, 24)
        Me.Cmb_Klasifikasi_Bahan.TabIndex = 493
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(12, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(107, 17)
        Me.Label4.TabIndex = 488
        Me.Label4.Text = "Klasifikasi Bahan"
        '
        'Txt_Nm_Barang
        '
        Me.Txt_Nm_Barang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Nm_Barang.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Txt_Nm_Barang.Location = New System.Drawing.Point(138, 164)
        Me.Txt_Nm_Barang.MaxLength = 100
        Me.Txt_Nm_Barang.Name = "Txt_Nm_Barang"
        Me.Txt_Nm_Barang.Size = New System.Drawing.Size(376, 22)
        Me.Txt_Nm_Barang.TabIndex = 489
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(12, 164)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 17)
        Me.Label6.TabIndex = 488
        Me.Label6.Text = "Nama Barang"
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(301, 310)
        Me.Btn_Refresh.Margin = New System.Windows.Forms.Padding(2)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(130, 31)
        Me.Btn_Refresh.TabIndex = 487
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(18, 596)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(974, 15)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Lv_Data)
        Me.GroupBox2.Controls.Add(Me.Panel9)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Panel8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Panel13)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Location = New System.Drawing.Point(21, 346)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(842, 249)
        Me.GroupBox2.TabIndex = 495
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Display"
        '
        'Lv_Data
        '
        Me.Lv_Data.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Lv_Data.FullRowSelect = True
        Me.Lv_Data.GridLines = True
        Me.Lv_Data.HideSelection = False
        Me.Lv_Data.Location = New System.Drawing.Point(3, 33)
        Me.Lv_Data.Name = "Lv_Data"
        Me.Lv_Data.Size = New System.Drawing.Size(836, 213)
        Me.Lv_Data.TabIndex = 0
        Me.Lv_Data.UseCompatibleStateImageBehavior = False
        Me.Lv_Data.View = System.Windows.Forms.View.Details
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.DarkRed
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Location = New System.Drawing.Point(582, 15)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(12, 12)
        Me.Panel9.TabIndex = 491
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(598, 13)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 16)
        Me.Label8.TabIndex = 492
        Me.Label8.Text = "Rejected"
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.LightGreen
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Location = New System.Drawing.Point(757, 15)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(12, 12)
        Me.Panel8.TabIndex = 491
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(773, 13)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 16)
        Me.Label7.TabIndex = 492
        Me.Label7.Text = "Validated"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Controls.Add(Me.Panel7)
        Me.Panel5.Location = New System.Drawing.Point(865, 60)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 566)
        Me.Panel5.TabIndex = 39
        Me.Panel5.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(18, 457)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(824, 11)
        Me.Panel7.TabIndex = 35
        Me.Panel7.Visible = False
        '
        'N_EMI_Transaksi_Pengajuan_Barang_Baru
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(884, 611)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TxtNo_Transaksi)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_Transaksi_Pengajuan_Barang_Baru"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_Kd_Barang As TextBox
    Friend WithEvents TxtNo_Transaksi As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Panel13 As Panel
    Friend WithEvents Cmb_Group_Jenis As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Cmb_Klasifikasi_Bahan As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Cmb_Klasifikasi_Bahan_2 As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Txt_Nm_Barang As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Lv_Data As ListView
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label7 As Label
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents Cmb_Klasifikasi_Bahan_3 As ComboBox
    Friend WithEvents Label9 As Label
	Friend WithEvents Cmb_Flag_Preservative As ComboBox
	Friend WithEvents Label10 As Label
End Class
