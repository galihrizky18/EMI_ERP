<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class N_EMI_Laporan_Pelunasan_Cut_Off_Asset
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
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Txt_Kd_Kategori = New System.Windows.Forms.TextBox()
        Me.Txt_Kd_Perusahaan_Import = New System.Windows.Forms.TextBox()
        Me.Txt_PO = New System.Windows.Forms.TextBox()
        Me.Txt_Ket_PO = New System.Windows.Forms.TextBox()
        Me.Txt_Faktur = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_Nm_Perusahaan_Import = New System.Windows.Forms.TextBox()
        Me.Txt_Nm_Kategori = New System.Windows.Forms.TextBox()
        Me.BtnCetak = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.Lv_Faktur = New System.Windows.Forms.ListView()
        Me.Lv_PO = New System.Windows.Forms.ListView()
        Me.LV_Perusahaan_Import = New System.Windows.Forms.ListView()
        Me.Lv_Kategori = New System.Windows.Forms.ListView()
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
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(738, 44)
        Me.Panel1.TabIndex = 27
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
        Me.PanelGradient1.Size = New System.Drawing.Size(738, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(15, 7)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(353, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Laporan - Pelunasan Cut Off Asset"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 46)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 601)
        Me.Panel3.TabIndex = 42
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(20, 46)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(942, 12)
        Me.Panel5.TabIndex = 43
        Me.Panel5.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(26, 256)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 15)
        Me.Panel2.TabIndex = 43
        Me.Panel2.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(721, 60)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 601)
        Me.Panel4.TabIndex = 42
        Me.Panel4.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Txt_Kd_Kategori)
        Me.GroupBox1.Controls.Add(Me.Txt_Kd_Perusahaan_Import)
        Me.GroupBox1.Controls.Add(Me.Txt_PO)
        Me.GroupBox1.Controls.Add(Me.Txt_Ket_PO)
        Me.GroupBox1.Controls.Add(Me.Txt_Faktur)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Tgl2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Tgl1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Txt_Nm_Perusahaan_Import)
        Me.GroupBox1.Controls.Add(Me.Txt_Nm_Kategori)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 55)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(700, 161)
        Me.GroupBox1.TabIndex = 44
        Me.GroupBox1.TabStop = False
        '
        'Txt_Kd_Kategori
        '
        Me.Txt_Kd_Kategori.Location = New System.Drawing.Point(177, 127)
        Me.Txt_Kd_Kategori.Name = "Txt_Kd_Kategori"
        Me.Txt_Kd_Kategori.Size = New System.Drawing.Size(163, 20)
        Me.Txt_Kd_Kategori.TabIndex = 6
        '
        'Txt_Kd_Perusahaan_Import
        '
        Me.Txt_Kd_Perusahaan_Import.Location = New System.Drawing.Point(177, 101)
        Me.Txt_Kd_Perusahaan_Import.Name = "Txt_Kd_Perusahaan_Import"
        Me.Txt_Kd_Perusahaan_Import.Size = New System.Drawing.Size(163, 20)
        Me.Txt_Kd_Perusahaan_Import.TabIndex = 4
        '
        'Txt_PO
        '
        Me.Txt_PO.Location = New System.Drawing.Point(177, 76)
        Me.Txt_PO.Name = "Txt_PO"
        Me.Txt_PO.Size = New System.Drawing.Size(163, 20)
        Me.Txt_PO.TabIndex = 3
        '
        'Txt_Ket_PO
        '
        Me.Txt_Ket_PO.Location = New System.Drawing.Point(346, 76)
        Me.Txt_Ket_PO.Name = "Txt_Ket_PO"
        Me.Txt_Ket_PO.Size = New System.Drawing.Size(346, 20)
        Me.Txt_Ket_PO.TabIndex = 2
        '
        'Txt_Faktur
        '
        Me.Txt_Faktur.Location = New System.Drawing.Point(177, 49)
        Me.Txt_Faktur.Name = "Txt_Faktur"
        Me.Txt_Faktur.Size = New System.Drawing.Size(163, 20)
        Me.Txt_Faktur.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 101)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(141, 16)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Perusahaan Biaya Import"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 76)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 16)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "No PO"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 127)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(123, 16)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Kategori Biaya Import"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 49)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 16)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "No Faktur"
        '
        'Tgl2
        '
        Me.Tgl2.CustomFormat = "dd MMMM yyyy"
        Me.Tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl2.Location = New System.Drawing.Point(388, 20)
        Me.Tgl2.Name = "Tgl2"
        Me.Tgl2.Size = New System.Drawing.Size(163, 20)
        Me.Tgl2.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(351, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "s/d"
        '
        'Tgl1
        '
        Me.Tgl1.CustomFormat = "dd MMMM yyyy"
        Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl1.Location = New System.Drawing.Point(177, 20)
        Me.Tgl1.Name = "Tgl1"
        Me.Tgl1.Size = New System.Drawing.Size(163, 20)
        Me.Tgl1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Periode"
        '
        'Txt_Nm_Perusahaan_Import
        '
        Me.Txt_Nm_Perusahaan_Import.Location = New System.Drawing.Point(346, 101)
        Me.Txt_Nm_Perusahaan_Import.Name = "Txt_Nm_Perusahaan_Import"
        Me.Txt_Nm_Perusahaan_Import.Size = New System.Drawing.Size(346, 20)
        Me.Txt_Nm_Perusahaan_Import.TabIndex = 5
        '
        'Txt_Nm_Kategori
        '
        Me.Txt_Nm_Kategori.Location = New System.Drawing.Point(346, 127)
        Me.Txt_Nm_Kategori.Name = "Txt_Nm_Kategori"
        Me.Txt_Nm_Kategori.Size = New System.Drawing.Size(346, 20)
        Me.Txt_Nm_Kategori.TabIndex = 7
        '
        'BtnCetak
        '
        Me.BtnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnCetak.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnCetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnCetak.ForeColor = System.Drawing.Color.White
        Me.BtnCetak.Location = New System.Drawing.Point(553, 222)
        Me.BtnCetak.Name = "BtnCetak"
        Me.BtnCetak.Size = New System.Drawing.Size(84, 33)
        Me.BtnCetak.TabIndex = 45
        Me.BtnCetak.Text = "&Cetak"
        Me.BtnCetak.UseVisualStyleBackColor = False
        '
        'BtnExit
        '
        Me.BtnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnExit.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnExit.ForeColor = System.Drawing.Color.White
        Me.BtnExit.Location = New System.Drawing.Point(636, 222)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(84, 33)
        Me.BtnExit.TabIndex = 46
        Me.BtnExit.Text = "&Keluar"
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'Lv_Faktur
        '
        Me.Lv_Faktur.BackColor = System.Drawing.Color.White
        Me.Lv_Faktur.FullRowSelect = True
        Me.Lv_Faktur.GridLines = True
        Me.Lv_Faktur.HideSelection = False
        Me.Lv_Faktur.Location = New System.Drawing.Point(750, 126)
        Me.Lv_Faktur.Name = "Lv_Faktur"
        Me.Lv_Faktur.Size = New System.Drawing.Size(515, 200)
        Me.Lv_Faktur.TabIndex = 47
        Me.Lv_Faktur.UseCompatibleStateImageBehavior = False
        Me.Lv_Faktur.View = System.Windows.Forms.View.Details
        Me.Lv_Faktur.Visible = False
        '
        'Lv_PO
        '
        Me.Lv_PO.BackColor = System.Drawing.Color.White
        Me.Lv_PO.FullRowSelect = True
        Me.Lv_PO.GridLines = True
        Me.Lv_PO.HideSelection = False
        Me.Lv_PO.Location = New System.Drawing.Point(750, 155)
        Me.Lv_PO.Name = "Lv_PO"
        Me.Lv_PO.Size = New System.Drawing.Size(515, 200)
        Me.Lv_PO.TabIndex = 47
        Me.Lv_PO.UseCompatibleStateImageBehavior = False
        Me.Lv_PO.View = System.Windows.Forms.View.Details
        Me.Lv_PO.Visible = False
        '
        'LV_Perusahaan_Import
        '
        Me.LV_Perusahaan_Import.BackColor = System.Drawing.Color.White
        Me.LV_Perusahaan_Import.FullRowSelect = True
        Me.LV_Perusahaan_Import.GridLines = True
        Me.LV_Perusahaan_Import.HideSelection = False
        Me.LV_Perusahaan_Import.Location = New System.Drawing.Point(750, 180)
        Me.LV_Perusahaan_Import.Name = "LV_Perusahaan_Import"
        Me.LV_Perusahaan_Import.Size = New System.Drawing.Size(515, 200)
        Me.LV_Perusahaan_Import.TabIndex = 47
        Me.LV_Perusahaan_Import.UseCompatibleStateImageBehavior = False
        Me.LV_Perusahaan_Import.View = System.Windows.Forms.View.Details
        Me.LV_Perusahaan_Import.Visible = False
        '
        'Lv_Kategori
        '
        Me.Lv_Kategori.BackColor = System.Drawing.Color.White
        Me.Lv_Kategori.FullRowSelect = True
        Me.Lv_Kategori.GridLines = True
        Me.Lv_Kategori.HideSelection = False
        Me.Lv_Kategori.Location = New System.Drawing.Point(750, 205)
        Me.Lv_Kategori.Name = "Lv_Kategori"
        Me.Lv_Kategori.Size = New System.Drawing.Size(515, 200)
        Me.Lv_Kategori.TabIndex = 47
        Me.Lv_Kategori.UseCompatibleStateImageBehavior = False
        Me.Lv_Kategori.View = System.Windows.Forms.View.Details
        Me.Lv_Kategori.Visible = False
        '
        'N_EMI_Laporan_Pelunasan_Cut_Off_Asset
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(738, 271)
        Me.Controls.Add(Me.Lv_Kategori)
        Me.Controls.Add(Me.LV_Perusahaan_Import)
        Me.Controls.Add(Me.Lv_PO)
        Me.Controls.Add(Me.Lv_Faktur)
        Me.Controls.Add(Me.BtnCetak)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_Laporan_Pelunasan_Cut_Off_Asset"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Txt_Kd_Kategori As TextBox
    Friend WithEvents Txt_Kd_Perusahaan_Import As TextBox
    Friend WithEvents Txt_PO As TextBox
    Friend WithEvents Txt_Faktur As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Tgl2 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_Nm_Perusahaan_Import As TextBox
    Friend WithEvents Txt_Nm_Kategori As TextBox
    Friend WithEvents Txt_Ket_PO As TextBox
    Friend WithEvents BtnCetak As Button
    Friend WithEvents BtnExit As Button
    Friend WithEvents Lv_Faktur As ListView
    Friend WithEvents Lv_PO As ListView
    Friend WithEvents LV_Perusahaan_Import As ListView
    Friend WithEvents Lv_Kategori As ListView
End Class
