<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_SD_Master_Kendaraan_DO
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Cmb_Jenis_Kepemilikan_STNK = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Cmb_Jenis = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txt_Kapasitas_Muatan = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_Plat_Kendaraan = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Btn_Hapus = New System.Windows.Forms.Button()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Exit = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Lv_Display_Kendaraan = New System.Windows.Forms.ListView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(646, 45)
        Me.Panel1.TabIndex = 315
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
        Me.PanelGradient1.Size = New System.Drawing.Size(646, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 9)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(344, 25)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Show Dialog - Tambah Kendaraan"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(-3, 44)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1410, 12)
        Me.Panel2.TabIndex = 316
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 55)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 649)
        Me.Panel3.TabIndex = 317
        Me.Panel3.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Cmb_Jenis_Kepemilikan_STNK)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Cmb_Jenis)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Txt_Kapasitas_Muatan)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Txt_Plat_Kendaraan)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 50)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(448, 140)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Work Sans", 7.0!)
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(282, 108)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(81, 14)
        Me.Label10.TabIndex = 321
        Me.Label10.Text = "Y :Ya   T : Tidak"
        '
        'Cmb_Jenis_Kepemilikan_STNK
        '
        Me.Cmb_Jenis_Kepemilikan_STNK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Jenis_Kepemilikan_STNK.FormattingEnabled = True
        Me.Cmb_Jenis_Kepemilikan_STNK.Location = New System.Drawing.Point(119, 101)
        Me.Cmb_Jenis_Kepemilikan_STNK.Name = "Cmb_Jenis_Kepemilikan_STNK"
        Me.Cmb_Jenis_Kepemilikan_STNK.Size = New System.Drawing.Size(156, 24)
        Me.Cmb_Jenis_Kepemilikan_STNK.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 104)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 16)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "STNK Sendiri"
        '
        'Cmb_Jenis
        '
        Me.Cmb_Jenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Jenis.FormattingEnabled = True
        Me.Cmb_Jenis.ItemHeight = 16
        Me.Cmb_Jenis.Location = New System.Drawing.Point(119, 71)
        Me.Cmb_Jenis.Name = "Cmb_Jenis"
        Me.Cmb_Jenis.Size = New System.Drawing.Size(156, 24)
        Me.Cmb_Jenis.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Jenis Kendaraan"
        '
        'Txt_Kapasitas_Muatan
        '
        Me.Txt_Kapasitas_Muatan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Kapasitas_Muatan.Location = New System.Drawing.Point(119, 44)
        Me.Txt_Kapasitas_Muatan.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Txt_Kapasitas_Muatan.Name = "Txt_Kapasitas_Muatan"
        Me.Txt_Kapasitas_Muatan.Size = New System.Drawing.Size(156, 20)
        Me.Txt_Kapasitas_Muatan.TabIndex = 1
        Me.Txt_Kapasitas_Muatan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(103, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Kapasitas Muatan"
        '
        'Txt_Plat_Kendaraan
        '
        Me.Txt_Plat_Kendaraan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Plat_Kendaraan.Location = New System.Drawing.Point(119, 16)
        Me.Txt_Plat_Kendaraan.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Txt_Plat_Kendaraan.MaxLength = 20
        Me.Txt_Plat_Kendaraan.Name = "Txt_Plat_Kendaraan"
        Me.Txt_Plat_Kendaraan.Size = New System.Drawing.Size(313, 20)
        Me.Txt_Plat_Kendaraan.TabIndex = 0
        Me.Txt_Plat_Kendaraan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Plat Kendaraan"
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(20, 197)
        Me.Btn_Simpan.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(109, 30)
        Me.Btn_Simpan.TabIndex = 1
        Me.Btn_Simpan.Tag = "SIMPAN"
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Btn_Hapus
        '
        Me.Btn_Hapus.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Hapus.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Hapus.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Hapus.ForeColor = System.Drawing.Color.White
        Me.Btn_Hapus.Location = New System.Drawing.Point(133, 197)
        Me.Btn_Hapus.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Btn_Hapus.Name = "Btn_Hapus"
        Me.Btn_Hapus.Size = New System.Drawing.Size(109, 30)
        Me.Btn_Hapus.TabIndex = 2
        Me.Btn_Hapus.Text = "&Hapus"
        Me.Btn_Hapus.UseVisualStyleBackColor = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(246, 197)
        Me.Btn_Refresh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(109, 30)
        Me.Btn_Refresh.TabIndex = 3
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Exit
        '
        Me.Btn_Exit.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Exit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Exit.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Exit.ForeColor = System.Drawing.Color.White
        Me.Btn_Exit.Location = New System.Drawing.Point(359, 197)
        Me.Btn_Exit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Btn_Exit.Name = "Btn_Exit"
        Me.Btn_Exit.Size = New System.Drawing.Size(109, 30)
        Me.Btn_Exit.TabIndex = 4
        Me.Btn_Exit.Text = "&Exit"
        Me.Btn_Exit.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Lv_Display_Kendaraan)
        Me.GroupBox2.Location = New System.Drawing.Point(20, 234)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(607, 362)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Display Data Kendaraan"
        '
        'Lv_Display_Kendaraan
        '
        Me.Lv_Display_Kendaraan.FullRowSelect = True
        Me.Lv_Display_Kendaraan.GridLines = True
        Me.Lv_Display_Kendaraan.HideSelection = False
        Me.Lv_Display_Kendaraan.Location = New System.Drawing.Point(9, 21)
        Me.Lv_Display_Kendaraan.Name = "Lv_Display_Kendaraan"
        Me.Lv_Display_Kendaraan.OwnerDraw = True
        Me.Lv_Display_Kendaraan.Size = New System.Drawing.Size(590, 332)
        Me.Lv_Display_Kendaraan.TabIndex = 0
        Me.Lv_Display_Kendaraan.UseCompatibleStateImageBehavior = False
        Me.Lv_Display_Kendaraan.View = System.Windows.Forms.View.Details
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(20, 596)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1410, 15)
        Me.Panel4.TabIndex = 316
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(627, 96)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 649)
        Me.Panel5.TabIndex = 317
        Me.Panel5.Visible = False
        '
        'N_EMI_SD_Master_Kendaraan_DO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(646, 611)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Btn_Exit)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Hapus)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_SD_Master_Kendaraan_DO"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_Plat_Kendaraan As TextBox
    Friend WithEvents Txt_Kapasitas_Muatan As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Cmb_Jenis_Kepemilikan_STNK As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Cmb_Jenis As ComboBox
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Btn_Hapus As Button
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Exit As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Lv_Display_Kendaraan As ListView
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label10 As Label
End Class
