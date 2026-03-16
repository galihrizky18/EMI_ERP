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
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TlpDetailFormulator = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.BtnSimpanSudahBinding = New System.Windows.Forms.Button()
        Me.LvDetailBahan = New System.Windows.Forms.ListView()
        Me.LvDetailKandungan = New System.Windows.Forms.ListView()
        Me.LvDetailStep = New System.Windows.Forms.ListView()
        Me.LbEstHPP = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
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
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(22, 10)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(176, 25)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Detail Formulator"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
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
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.White
        Me.TabPage2.Controls.Add(Me.LvDetailKandungan)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1035, 272)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Kandungan"
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.White
        Me.TabPage3.Controls.Add(Me.LvDetailStep)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(1035, 272)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Step"
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
        'LvDetailKandungan
        '
        Me.LvDetailKandungan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LvDetailKandungan.HideSelection = False
        Me.LvDetailKandungan.Location = New System.Drawing.Point(3, 3)
        Me.LvDetailKandungan.Name = "LvDetailKandungan"
        Me.LvDetailKandungan.Size = New System.Drawing.Size(1029, 266)
        Me.LvDetailKandungan.TabIndex = 0
        Me.LvDetailKandungan.UseCompatibleStateImageBehavior = False
        '
        'LvDetailStep
        '
        Me.LvDetailStep.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LvDetailStep.HideSelection = False
        Me.LvDetailStep.Location = New System.Drawing.Point(0, 0)
        Me.LvDetailStep.Name = "LvDetailStep"
        Me.LvDetailStep.Size = New System.Drawing.Size(1035, 272)
        Me.LvDetailStep.TabIndex = 0
        Me.LvDetailStep.UseCompatibleStateImageBehavior = False
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
        Me.MaximizeBox = False
        Me.Name = "N_EMI_SD_Detail_Formulator"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TlpDetailFormulator As TableLayoutPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents BtnSimpanSudahBinding As Button
    Friend WithEvents LvDetailBahan As ListView
    Friend WithEvents LvDetailKandungan As ListView
    Friend WithEvents LvDetailStep As ListView
    Friend WithEvents LbEstHPP As Label
End Class
