<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_SD_Detail_Formulator
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
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.LvDetailBahan = New System.Windows.Forms.ListView()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Lv_Moisture_Content = New System.Windows.Forms.ListView()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.LV_AnalisaLabTrialKitchen = New System.Windows.Forms.ListView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.LV_AnalisaLabTrialProduksi = New System.Windows.Forms.ListView()
        Me.TlpDetailFormulator = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.BtnSimpanSudahBinding = New System.Windows.Forms.Button()
        Me.LbEstHPP = New System.Windows.Forms.Label()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage2.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1086, 45)
        Me.Panel1.TabIndex = 29
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(22, 10)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(151, 25)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Detail Formula"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(22, 162)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1043, 298)
        Me.TabControl1.TabIndex = 30
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.White
        Me.TabPage1.Controls.Add(Me.LvDetailBahan)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1035, 272)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Bahan"
        '
        'LvDetailBahan
        '
        Me.LvDetailBahan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LvDetailBahan.HideSelection = False
        Me.LvDetailBahan.Location = New System.Drawing.Point(3, 3)
        Me.LvDetailBahan.Name = "LvDetailBahan"
        Me.LvDetailBahan.Size = New System.Drawing.Size(1029, 266)
        Me.LvDetailBahan.TabIndex = 0
        Me.LvDetailBahan.UseCompatibleStateImageBehavior = False
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Lv_Moisture_Content)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(1035, 272)
        Me.TabPage4.TabIndex = 1
        Me.TabPage4.Text = "Moisture Content"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Lv_Moisture_Content
        '
        Me.Lv_Moisture_Content.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lv_Moisture_Content.FullRowSelect = True
        Me.Lv_Moisture_Content.GridLines = True
        Me.Lv_Moisture_Content.HideSelection = False
        Me.Lv_Moisture_Content.Location = New System.Drawing.Point(0, 0)
        Me.Lv_Moisture_Content.Name = "Lv_Moisture_Content"
        Me.Lv_Moisture_Content.Size = New System.Drawing.Size(1035, 272)
        Me.Lv_Moisture_Content.TabIndex = 2
        Me.Lv_Moisture_Content.UseCompatibleStateImageBehavior = False
        Me.Lv_Moisture_Content.View = System.Windows.Forms.View.Details
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.LV_AnalisaLabTrialKitchen)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(1035, 272)
        Me.TabPage3.TabIndex = 3
        Me.TabPage3.Text = "Analisa Lab (Trial Kitchen)"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'LV_AnalisaLabTrialKitchen
        '
        Me.LV_AnalisaLabTrialKitchen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LV_AnalisaLabTrialKitchen.FullRowSelect = True
        Me.LV_AnalisaLabTrialKitchen.GridLines = True
        Me.LV_AnalisaLabTrialKitchen.HideSelection = False
        Me.LV_AnalisaLabTrialKitchen.Location = New System.Drawing.Point(0, 0)
        Me.LV_AnalisaLabTrialKitchen.Name = "LV_AnalisaLabTrialKitchen"
        Me.LV_AnalisaLabTrialKitchen.Size = New System.Drawing.Size(1035, 272)
        Me.LV_AnalisaLabTrialKitchen.TabIndex = 4
        Me.LV_AnalisaLabTrialKitchen.UseCompatibleStateImageBehavior = False
        Me.LV_AnalisaLabTrialKitchen.View = System.Windows.Forms.View.Details
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.LV_AnalisaLabTrialProduksi)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1035, 272)
        Me.TabPage2.TabIndex = 2
        Me.TabPage2.Text = "Analisa Lab (Trial Produksi)"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'LV_AnalisaLabTrialProduksi
        '
        Me.LV_AnalisaLabTrialProduksi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LV_AnalisaLabTrialProduksi.FullRowSelect = True
        Me.LV_AnalisaLabTrialProduksi.GridLines = True
        Me.LV_AnalisaLabTrialProduksi.HideSelection = False
        Me.LV_AnalisaLabTrialProduksi.Location = New System.Drawing.Point(3, 3)
        Me.LV_AnalisaLabTrialProduksi.Name = "LV_AnalisaLabTrialProduksi"
        Me.LV_AnalisaLabTrialProduksi.Size = New System.Drawing.Size(1029, 266)
        Me.LV_AnalisaLabTrialProduksi.TabIndex = 3
        Me.LV_AnalisaLabTrialProduksi.UseCompatibleStateImageBehavior = False
        Me.LV_AnalisaLabTrialProduksi.View = System.Windows.Forms.View.Details
        '
        'TlpDetailFormulator
        '
        Me.TlpDetailFormulator.ColumnCount = 11
        Me.TlpDetailFormulator.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpDetailFormulator.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpDetailFormulator.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpDetailFormulator.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpDetailFormulator.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpDetailFormulator.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpDetailFormulator.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpDetailFormulator.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpDetailFormulator.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpDetailFormulator.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpDetailFormulator.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpDetailFormulator.Location = New System.Drawing.Point(20, 59)
        Me.TlpDetailFormulator.Name = "TlpDetailFormulator"
        Me.TlpDetailFormulator.RowCount = 4
        Me.TlpDetailFormulator.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TlpDetailFormulator.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TlpDetailFormulator.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19.0!))
        Me.TlpDetailFormulator.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TlpDetailFormulator.Size = New System.Drawing.Size(1045, 80)
        Me.TlpDetailFormulator.TabIndex = 31
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 45)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1086, 12)
        Me.Panel2.TabIndex = 493
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 53)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 477)
        Me.Panel3.TabIndex = 494
        Me.Panel3.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(1067, 54)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 477)
        Me.Panel4.TabIndex = 495
        Me.Panel4.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(13, 512)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1060, 15)
        Me.Panel7.TabIndex = 496
        Me.Panel7.Visible = False
        '
        'BtnSimpanSudahBinding
        '
        Me.BtnSimpanSudahBinding.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnSimpanSudahBinding.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnSimpanSudahBinding.ForeColor = System.Drawing.Color.White
        Me.BtnSimpanSudahBinding.Location = New System.Drawing.Point(983, 476)
        Me.BtnSimpanSudahBinding.Name = "BtnSimpanSudahBinding"
        Me.BtnSimpanSudahBinding.Size = New System.Drawing.Size(84, 36)
        Me.BtnSimpanSudahBinding.TabIndex = 501
        Me.BtnSimpanSudahBinding.Text = "&Tutup"
        Me.BtnSimpanSudahBinding.UseVisualStyleBackColor = False
        '
        'LbEstHPP
        '
        Me.LbEstHPP.AutoSize = True
        Me.LbEstHPP.Location = New System.Drawing.Point(17, 464)
        Me.LbEstHPP.Name = "LbEstHPP"
        Me.LbEstHPP.Size = New System.Drawing.Size(39, 13)
        Me.LbEstHPP.TabIndex = 502
        Me.LbEstHPP.Text = "Label1"
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1086, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'N_EMI_SD_Detail_Formulator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1086, 527)
        Me.Controls.Add(Me.LbEstHPP)
        Me.Controls.Add(Me.BtnSimpanSudahBinding)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.TlpDetailFormulator)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "N_EMI_SD_Detail_Formulator"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TlpDetailFormulator As TableLayoutPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents BtnSimpanSudahBinding As Button
    Friend WithEvents LvDetailBahan As ListView
    Friend WithEvents LbEstHPP As Label
    Friend WithEvents TabPage4 As TabPage
	Friend WithEvents Lv_Moisture_Content As ListView
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents LV_AnalisaLabTrialProduksi As ListView
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents LV_AnalisaLabTrialKitchen As ListView
End Class
