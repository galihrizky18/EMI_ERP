<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class N_EMI_Laporan_Pelunasan_Proyek_Cut_Off
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(N_EMI_Laporan_Pelunasan_Proyek_Cut_Off))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_Faktur = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Txt_Kd_Perusahaan_Import = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Txt_Nm_Perusahaan_Import = New System.Windows.Forms.TextBox()
        Me.BtnCetak = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.Lv_Faktur = New System.Windows.Forms.ListView()
        Me.LV_Perusahaan_Import = New System.Windows.Forms.ListView()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ComboBox4 = New System.Windows.Forms.ComboBox()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Cmb_Jenis_Laporan = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(569, 45)
        Me.Panel1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(18, 8)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(242, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Laporan Hutang Proyek"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 52)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1312, 12)
        Me.Panel2.TabIndex = 34
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 63)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 619)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(551, 62)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 619)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(20, 247)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1231, 15)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'Panel17
        '
        Me.Panel17.BackColor = System.Drawing.Color.Red
        Me.Panel17.Location = New System.Drawing.Point(25, 201)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(1264, 12)
        Me.Panel17.TabIndex = 35
        Me.Panel17.Visible = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "3.png")
        Me.ImageList1.Images.SetKeyName(1, "1.png")
        Me.ImageList1.Images.SetKeyName(2, "6.png")
        Me.ImageList1.Images.SetKeyName(3, "5.png")
        Me.ImageList1.Images.SetKeyName(4, "4.png")
        Me.ImageList1.Images.SetKeyName(5, "2.png")
        '
        'Tgl2
        '
        Me.Tgl2.CustomFormat = "dd MMMM yyyy"
        Me.Tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl2.Location = New System.Drawing.Point(305, 68)
        Me.Tgl2.Name = "Tgl2"
        Me.Tgl2.Size = New System.Drawing.Size(163, 20)
        Me.Tgl2.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(268, 68)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(23, 13)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "s/d"
        '
        'Tgl1
        '
        Me.Tgl1.CustomFormat = "dd MMMM yyyy"
        Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl1.Location = New System.Drawing.Point(94, 68)
        Me.Tgl1.Name = "Tgl1"
        Me.Tgl1.Size = New System.Drawing.Size(163, 20)
        Me.Tgl1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "Periode"
        '
        'Txt_Faktur
        '
        Me.Txt_Faktur.Location = New System.Drawing.Point(94, 152)
        Me.Txt_Faktur.Name = "Txt_Faktur"
        Me.Txt_Faktur.Size = New System.Drawing.Size(163, 20)
        Me.Txt_Faktur.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(22, 155)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 13)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "No Faktur"
        '
        'Txt_Kd_Perusahaan_Import
        '
        Me.Txt_Kd_Perusahaan_Import.Location = New System.Drawing.Point(94, 178)
        Me.Txt_Kd_Perusahaan_Import.Name = "Txt_Kd_Perusahaan_Import"
        Me.Txt_Kd_Perusahaan_Import.Size = New System.Drawing.Size(163, 20)
        Me.Txt_Kd_Perusahaan_Import.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 181)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 13)
        Me.Label4.TabIndex = 46
        Me.Label4.Text = "Supplier"
        '
        'Txt_Nm_Perusahaan_Import
        '
        Me.Txt_Nm_Perusahaan_Import.Enabled = False
        Me.Txt_Nm_Perusahaan_Import.Location = New System.Drawing.Point(263, 178)
        Me.Txt_Nm_Perusahaan_Import.Name = "Txt_Nm_Perusahaan_Import"
        Me.Txt_Nm_Perusahaan_Import.Size = New System.Drawing.Size(285, 20)
        Me.Txt_Nm_Perusahaan_Import.TabIndex = 6
        '
        'BtnCetak
        '
        Me.BtnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnCetak.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnCetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnCetak.ForeColor = System.Drawing.Color.White
        Me.BtnCetak.Location = New System.Drawing.Point(377, 212)
        Me.BtnCetak.Name = "BtnCetak"
        Me.BtnCetak.Size = New System.Drawing.Size(84, 33)
        Me.BtnCetak.TabIndex = 7
        Me.BtnCetak.Text = "&Cetak"
        Me.BtnCetak.UseVisualStyleBackColor = False
        '
        'BtnExit
        '
        Me.BtnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnExit.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnExit.ForeColor = System.Drawing.Color.White
        Me.BtnExit.Location = New System.Drawing.Point(464, 212)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(84, 33)
        Me.BtnExit.TabIndex = 8
        Me.BtnExit.Text = "&Keluar"
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'Lv_Faktur
        '
        Me.Lv_Faktur.BackColor = System.Drawing.Color.White
        Me.Lv_Faktur.FullRowSelect = True
        Me.Lv_Faktur.GridLines = True
        Me.Lv_Faktur.HideSelection = False
        Me.Lv_Faktur.Location = New System.Drawing.Point(570, 174)
        Me.Lv_Faktur.Name = "Lv_Faktur"
        Me.Lv_Faktur.Size = New System.Drawing.Size(454, 200)
        Me.Lv_Faktur.TabIndex = 50
        Me.Lv_Faktur.UseCompatibleStateImageBehavior = False
        Me.Lv_Faktur.View = System.Windows.Forms.View.Details
        Me.Lv_Faktur.Visible = False
        '
        'LV_Perusahaan_Import
        '
        Me.LV_Perusahaan_Import.BackColor = System.Drawing.Color.White
        Me.LV_Perusahaan_Import.FullRowSelect = True
        Me.LV_Perusahaan_Import.GridLines = True
        Me.LV_Perusahaan_Import.HideSelection = False
        Me.LV_Perusahaan_Import.Location = New System.Drawing.Point(570, 200)
        Me.LV_Perusahaan_Import.Name = "LV_Perusahaan_Import"
        Me.LV_Perusahaan_Import.Size = New System.Drawing.Size(454, 200)
        Me.LV_Perusahaan_Import.TabIndex = 51
        Me.LV_Perusahaan_Import.UseCompatibleStateImageBehavior = False
        Me.LV_Perusahaan_Import.View = System.Windows.Forms.View.Details
        Me.LV_Perusahaan_Import.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(22, 127)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 52
        Me.Label6.Text = "Lokasi"
        '
        'ComboBox4
        '
        Me.ComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox4.DropDownWidth = 150
        Me.ComboBox4.Font = New System.Drawing.Font("Work Sans", 8.9!)
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Location = New System.Drawing.Point(94, 122)
        Me.ComboBox4.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(163, 25)
        Me.ComboBox4.TabIndex = 3
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
        Me.PanelGradient1.Size = New System.Drawing.Size(569, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(22, 98)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(46, 13)
        Me.Label7.TabIndex = 52
        Me.Label7.Text = "Laporan"
        '
        'Cmb_Jenis_Laporan
        '
        Me.Cmb_Jenis_Laporan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Jenis_Laporan.DropDownWidth = 150
        Me.Cmb_Jenis_Laporan.Font = New System.Drawing.Font("Work Sans", 8.9!)
        Me.Cmb_Jenis_Laporan.FormattingEnabled = True
        Me.Cmb_Jenis_Laporan.Location = New System.Drawing.Point(94, 93)
        Me.Cmb_Jenis_Laporan.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmb_Jenis_Laporan.Name = "Cmb_Jenis_Laporan"
        Me.Cmb_Jenis_Laporan.Size = New System.Drawing.Size(374, 25)
        Me.Cmb_Jenis_Laporan.TabIndex = 2
        '
        'N_EMI_Laporan_Pelunasan_Proyek_Cut_Off
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(569, 261)
        Me.Controls.Add(Me.Cmb_Jenis_Laporan)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.ComboBox4)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.LV_Perusahaan_Import)
        Me.Controls.Add(Me.Lv_Faktur)
        Me.Controls.Add(Me.BtnCetak)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Txt_Kd_Perusahaan_Import)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Txt_Nm_Perusahaan_Import)
        Me.Controls.Add(Me.Txt_Faktur)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Tgl2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Tgl1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel17)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "N_EMI_Laporan_Pelunasan_Proyek_Cut_Off"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel17 As Panel
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents Tgl2 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_Faktur As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Txt_Kd_Perusahaan_Import As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Txt_Nm_Perusahaan_Import As TextBox
    Friend WithEvents BtnCetak As Button
    Friend WithEvents BtnExit As Button
    Friend WithEvents Lv_Faktur As ListView
    Friend WithEvents LV_Perusahaan_Import As ListView
    Friend WithEvents Label6 As Label
    Friend WithEvents ComboBox4 As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Cmb_Jenis_Laporan As ComboBox
End Class
