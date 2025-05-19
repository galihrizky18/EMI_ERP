<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SD_Detail_Batch
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Lv_Data = New System.Windows.Forms.ListView()
        Me.Txt_TotNilaiFormula = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Txt_TotNilaiPRoduksi = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel_GI = New System.Windows.Forms.Panel()
        Me.Btn_Cari_Pn1 = New System.Windows.Forms.Button()
        Me.Cmb_KdBarang_Pn1 = New System.Windows.Forms.ComboBox()
        Me.Cmb_Filter_Batch_Pn1 = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel_GR = New System.Windows.Forms.Panel()
        Me.Lv_DetailGr = New System.Windows.Forms.ListView()
        Me.Lv_DataGr = New System.Windows.Forms.ListView()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.Panel_GI.SuspendLayout()
        Me.Panel_GR.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Lbl_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1014, 54)
        Me.Panel1.TabIndex = 26
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(285, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(211, 18)
        Me.Label2.TabIndex = 419
        Me.Label2.Text = "Catatan : Ada 2 Panel di Form ini"
        Me.Label2.Visible = False
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 14)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(140, 25)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Detail Batch"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(14, 55)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1179, 12)
        Me.Panel2.TabIndex = 41
        Me.Panel2.Visible = False
        '
        'Lv_Data
        '
        Me.Lv_Data.Font = New System.Drawing.Font("Work Sans", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_Data.FullRowSelect = True
        Me.Lv_Data.GridLines = True
        Me.Lv_Data.HideSelection = False
        Me.Lv_Data.Location = New System.Drawing.Point(0, 37)
        Me.Lv_Data.Name = "Lv_Data"
        Me.Lv_Data.Size = New System.Drawing.Size(985, 435)
        Me.Lv_Data.TabIndex = 414
        Me.Lv_Data.UseCompatibleStateImageBehavior = False
        Me.Lv_Data.View = System.Windows.Forms.View.Details
        '
        'Txt_TotNilaiFormula
        '
        Me.Txt_TotNilaiFormula.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_TotNilaiFormula.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_TotNilaiFormula.Enabled = False
        Me.Txt_TotNilaiFormula.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Txt_TotNilaiFormula.Location = New System.Drawing.Point(1163, 366)
        Me.Txt_TotNilaiFormula.MaxLength = 50
        Me.Txt_TotNilaiFormula.Name = "Txt_TotNilaiFormula"
        Me.Txt_TotNilaiFormula.Size = New System.Drawing.Size(169, 23)
        Me.Txt_TotNilaiFormula.TabIndex = 416
        Me.Txt_TotNilaiFormula.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(1017, 368)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(126, 18)
        Me.Label3.TabIndex = 415
        Me.Label3.Text = "Total NIlai Formula"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1017, 397)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(128, 18)
        Me.Label1.TabIndex = 415
        Me.Label1.Text = "Total Nilai Produksi"
        '
        'Txt_TotNilaiPRoduksi
        '
        Me.Txt_TotNilaiPRoduksi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_TotNilaiPRoduksi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_TotNilaiPRoduksi.Enabled = False
        Me.Txt_TotNilaiPRoduksi.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Txt_TotNilaiPRoduksi.Location = New System.Drawing.Point(1163, 395)
        Me.Txt_TotNilaiPRoduksi.MaxLength = 50
        Me.Txt_TotNilaiPRoduksi.Name = "Txt_TotNilaiPRoduksi"
        Me.Txt_TotNilaiPRoduksi.Size = New System.Drawing.Size(169, 23)
        Me.Txt_TotNilaiPRoduksi.TabIndex = 416
        Me.Txt_TotNilaiPRoduksi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 77)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(12, 515)
        Me.Panel3.TabIndex = 413
        Me.Panel3.Visible = False
        '
        'Panel_GI
        '
        Me.Panel_GI.Controls.Add(Me.Btn_Cari_Pn1)
        Me.Panel_GI.Controls.Add(Me.Cmb_KdBarang_Pn1)
        Me.Panel_GI.Controls.Add(Me.Cmb_Filter_Batch_Pn1)
        Me.Panel_GI.Controls.Add(Me.Label5)
        Me.Panel_GI.Controls.Add(Me.Lv_Data)
        Me.Panel_GI.Controls.Add(Me.Label4)
        Me.Panel_GI.Location = New System.Drawing.Point(13, 67)
        Me.Panel_GI.Name = "Panel_GI"
        Me.Panel_GI.Size = New System.Drawing.Size(1000, 490)
        Me.Panel_GI.TabIndex = 0
        '
        'Btn_Cari_Pn1
        '
        Me.Btn_Cari_Pn1.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari_Pn1.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Cari_Pn1.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari_Pn1.Location = New System.Drawing.Point(464, -2)
        Me.Btn_Cari_Pn1.Name = "Btn_Cari_Pn1"
        Me.Btn_Cari_Pn1.Size = New System.Drawing.Size(80, 33)
        Me.Btn_Cari_Pn1.TabIndex = 2
        Me.Btn_Cari_Pn1.Text = "&Cari"
        Me.Btn_Cari_Pn1.UseVisualStyleBackColor = False
        '
        'Cmb_KdBarang_Pn1
        '
        Me.Cmb_KdBarang_Pn1.BackColor = System.Drawing.Color.White
        Me.Cmb_KdBarang_Pn1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_KdBarang_Pn1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Cmb_KdBarang_Pn1.FormattingEnabled = True
        Me.Cmb_KdBarang_Pn1.Location = New System.Drawing.Point(318, 1)
        Me.Cmb_KdBarang_Pn1.Name = "Cmb_KdBarang_Pn1"
        Me.Cmb_KdBarang_Pn1.Size = New System.Drawing.Size(140, 25)
        Me.Cmb_KdBarang_Pn1.TabIndex = 1
        '
        'Cmb_Filter_Batch_Pn1
        '
        Me.Cmb_Filter_Batch_Pn1.BackColor = System.Drawing.Color.White
        Me.Cmb_Filter_Batch_Pn1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Filter_Batch_Pn1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Cmb_Filter_Batch_Pn1.FormattingEnabled = True
        Me.Cmb_Filter_Batch_Pn1.Location = New System.Drawing.Point(73, 1)
        Me.Cmb_Filter_Batch_Pn1.Name = "Cmb_Filter_Batch_Pn1"
        Me.Cmb_Filter_Batch_Pn1.Size = New System.Drawing.Size(140, 25)
        Me.Cmb_Filter_Batch_Pn1.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(226, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 18)
        Me.Label5.TabIndex = 415
        Me.Label5.Text = "Kode Barang"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 2)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 18)
        Me.Label4.TabIndex = 415
        Me.Label4.Text = "Batch"
        '
        'Panel_GR
        '
        Me.Panel_GR.Controls.Add(Me.Lv_DetailGr)
        Me.Panel_GR.Controls.Add(Me.Lv_DataGr)
        Me.Panel_GR.Location = New System.Drawing.Point(13, 566)
        Me.Panel_GR.Name = "Panel_GR"
        Me.Panel_GR.Size = New System.Drawing.Size(1000, 490)
        Me.Panel_GR.TabIndex = 0
        '
        'Lv_DetailGr
        '
        Me.Lv_DetailGr.Font = New System.Drawing.Font("Work Sans", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_DetailGr.FullRowSelect = True
        Me.Lv_DetailGr.GridLines = True
        Me.Lv_DetailGr.HideSelection = False
        Me.Lv_DetailGr.Location = New System.Drawing.Point(2, 247)
        Me.Lv_DetailGr.Name = "Lv_DetailGr"
        Me.Lv_DetailGr.Size = New System.Drawing.Size(985, 225)
        Me.Lv_DetailGr.TabIndex = 416
        Me.Lv_DetailGr.UseCompatibleStateImageBehavior = False
        Me.Lv_DetailGr.View = System.Windows.Forms.View.Details
        '
        'Lv_DataGr
        '
        Me.Lv_DataGr.Font = New System.Drawing.Font("Work Sans", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_DataGr.FullRowSelect = True
        Me.Lv_DataGr.GridLines = True
        Me.Lv_DataGr.HideSelection = False
        Me.Lv_DataGr.Location = New System.Drawing.Point(2, 3)
        Me.Lv_DataGr.Name = "Lv_DataGr"
        Me.Lv_DataGr.Size = New System.Drawing.Size(985, 225)
        Me.Lv_DataGr.TabIndex = 416
        Me.Lv_DataGr.UseCompatibleStateImageBehavior = False
        Me.Lv_DataGr.View = System.Windows.Forms.View.Details
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 52)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(1014, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'SD_Detail_Batch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1014, 557)
        Me.Controls.Add(Me.Panel_GR)
        Me.Controls.Add(Me.Panel_GI)
        Me.Controls.Add(Me.Txt_TotNilaiPRoduksi)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Txt_TotNilaiFormula)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Name = "SD_Detail_Batch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel_GI.ResumeLayout(False)
        Me.Panel_GI.PerformLayout()
        Me.Panel_GR.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Lv_Data As ListView
    Friend WithEvents Txt_TotNilaiFormula As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_TotNilaiPRoduksi As TextBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel_GI As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Cmb_Filter_Batch_Pn1 As ComboBox
    Friend WithEvents Cmb_KdBarang_Pn1 As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Btn_Cari_Pn1 As Button
    Friend WithEvents Panel_GR As Panel
    Friend WithEvents Lv_DataGr As ListView
    Friend WithEvents Lv_DetailGr As ListView
End Class
