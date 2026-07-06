<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_SD_Compare_Formulator
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
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Tc = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.LvF1DetailBahan = New System.Windows.Forms.ListView()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.Lv_Moisture_Content = New System.Windows.Forms.ListView()
        Me.TbPosisiSekarang = New System.Windows.Forms.TextBox()
        Me.LblPO_NoNota = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TbPosisiTujuan = New System.Windows.Forms.TextBox()
        Me.BtnSimpan = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TbKeterangan = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TlpF1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TlpF2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.LvF2DetailBahan = New System.Windows.Forms.ListView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Lv_Moisture_Content_2 = New System.Windows.Forms.ListView()
        Me.LbF1EstHpp = New System.Windows.Forms.Label()
        Me.LbF2EstHpp = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Tc.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPage4.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1348, 45)
        Me.Panel1.TabIndex = 28
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1348, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(22, 10)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(184, 25)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Compare Formula"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 45)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1348, 12)
        Me.Panel2.TabIndex = 492
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 57)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 637)
        Me.Panel3.TabIndex = 493
        Me.Panel3.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Controls.Add(Me.RichTextBox1)
        Me.Panel4.Location = New System.Drawing.Point(1329, 57)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 637)
        Me.Panel4.TabIndex = 494
        Me.Panel4.Visible = False
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(20, -1)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(181, 140)
        Me.RichTextBox1.TabIndex = 461
        Me.RichTextBox1.Text = ""
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(19, 676)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1316, 15)
        Me.Panel7.TabIndex = 495
        Me.Panel7.Visible = False
        '
        'Tc
        '
        Me.Tc.Controls.Add(Me.TabPage1)
        Me.Tc.Controls.Add(Me.TabPage7)
        Me.Tc.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Tc.Location = New System.Drawing.Point(3, 120)
        Me.Tc.Name = "Tc"
        Me.Tc.SelectedIndex = 0
        Me.Tc.Size = New System.Drawing.Size(640, 386)
        Me.Tc.TabIndex = 496
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.White
        Me.TabPage1.Controls.Add(Me.LvF1DetailBahan)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(632, 360)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Bahan"
        '
        'LvF1DetailBahan
        '
        Me.LvF1DetailBahan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LvF1DetailBahan.HideSelection = False
        Me.LvF1DetailBahan.Location = New System.Drawing.Point(3, 3)
        Me.LvF1DetailBahan.Name = "LvF1DetailBahan"
        Me.LvF1DetailBahan.Size = New System.Drawing.Size(626, 354)
        Me.LvF1DetailBahan.TabIndex = 0
        Me.LvF1DetailBahan.UseCompatibleStateImageBehavior = False
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.Lv_Moisture_Content)
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(632, 360)
        Me.TabPage7.TabIndex = 1
        Me.TabPage7.Text = "Moisture Content"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'Lv_Moisture_Content
        '
        Me.Lv_Moisture_Content.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lv_Moisture_Content.FullRowSelect = True
        Me.Lv_Moisture_Content.GridLines = True
        Me.Lv_Moisture_Content.HideSelection = False
        Me.Lv_Moisture_Content.Location = New System.Drawing.Point(0, 0)
        Me.Lv_Moisture_Content.Name = "Lv_Moisture_Content"
        Me.Lv_Moisture_Content.Size = New System.Drawing.Size(632, 360)
        Me.Lv_Moisture_Content.TabIndex = 3
        Me.Lv_Moisture_Content.UseCompatibleStateImageBehavior = False
        Me.Lv_Moisture_Content.View = System.Windows.Forms.View.Details
        '
        'TbPosisiSekarang
        '
        Me.TbPosisiSekarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TbPosisiSekarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TbPosisiSekarang.Enabled = False
        Me.TbPosisiSekarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbPosisiSekarang.Location = New System.Drawing.Point(129, 66)
        Me.TbPosisiSekarang.MaxLength = 100
        Me.TbPosisiSekarang.Name = "TbPosisiSekarang"
        Me.TbPosisiSekarang.Size = New System.Drawing.Size(45, 21)
        Me.TbPosisiSekarang.TabIndex = 497
        '
        'LblPO_NoNota
        '
        Me.LblPO_NoNota.AutoSize = True
        Me.LblPO_NoNota.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.LblPO_NoNota.Location = New System.Drawing.Point(17, 68)
        Me.LblPO_NoNota.Name = "LblPO_NoNota"
        Me.LblPO_NoNota.Size = New System.Drawing.Size(110, 17)
        Me.LblPO_NoNota.TabIndex = 498
        Me.LblPO_NoNota.Text = "Posisi Sekarang"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(197, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 17)
        Me.Label1.TabIndex = 500
        Me.Label1.Text = "Posisi Tujuan"
        '
        'TbPosisiTujuan
        '
        Me.TbPosisiTujuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TbPosisiTujuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TbPosisiTujuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbPosisiTujuan.Location = New System.Drawing.Point(292, 66)
        Me.TbPosisiTujuan.MaxLength = 100
        Me.TbPosisiTujuan.Name = "TbPosisiTujuan"
        Me.TbPosisiTujuan.Size = New System.Drawing.Size(45, 21)
        Me.TbPosisiTujuan.TabIndex = 499
        '
        'BtnSimpan
        '
        Me.BtnSimpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnSimpan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnSimpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnSimpan.ForeColor = System.Drawing.Color.White
        Me.BtnSimpan.Location = New System.Drawing.Point(1245, 641)
        Me.BtnSimpan.Name = "BtnSimpan"
        Me.BtnSimpan.Size = New System.Drawing.Size(84, 36)
        Me.BtnSimpan.TabIndex = 501
        Me.BtnSimpan.Text = "&Simpan"
        Me.BtnSimpan.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(365, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 17)
        Me.Label2.TabIndex = 503
        Me.Label2.Text = "Keterangan"
        '
        'TbKeterangan
        '
        Me.TbKeterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TbKeterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TbKeterangan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbKeterangan.Location = New System.Drawing.Point(449, 66)
        Me.TbKeterangan.MaxLength = 100
        Me.TbKeterangan.Name = "TbKeterangan"
        Me.TbKeterangan.Size = New System.Drawing.Size(264, 21)
        Me.TbKeterangan.TabIndex = 502
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TlpF1)
        Me.GroupBox1.Controls.Add(Me.Tc)
        Me.GroupBox1.Location = New System.Drawing.Point(19, 119)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(646, 509)
        Me.GroupBox1.TabIndex = 504
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "FORMULA 1"
        '
        'TlpF1
        '
        Me.TlpF1.ColumnCount = 3
        Me.TlpF1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpF1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpF1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpF1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TlpF1.Location = New System.Drawing.Point(3, 16)
        Me.TlpF1.Name = "TlpF1"
        Me.TlpF1.RowCount = 4
        Me.TlpF1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TlpF1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TlpF1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.80952!))
        Me.TlpF1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.19048!))
        Me.TlpF1.Size = New System.Drawing.Size(640, 84)
        Me.TlpF1.TabIndex = 497
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TlpF2)
        Me.GroupBox2.Controls.Add(Me.TabControl2)
        Me.GroupBox2.Location = New System.Drawing.Point(690, 119)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(639, 509)
        Me.GroupBox2.TabIndex = 505
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "FORMULA 2"
        '
        'TlpF2
        '
        Me.TlpF2.ColumnCount = 3
        Me.TlpF2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpF2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpF2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TlpF2.Dock = System.Windows.Forms.DockStyle.Top
        Me.TlpF2.Location = New System.Drawing.Point(3, 16)
        Me.TlpF2.Name = "TlpF2"
        Me.TlpF2.RowCount = 4
        Me.TlpF2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TlpF2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TlpF2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TlpF2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TlpF2.Size = New System.Drawing.Size(633, 84)
        Me.TlpF2.TabIndex = 498
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.TabPage4)
        Me.TabControl2.Controls.Add(Me.TabPage2)
        Me.TabControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TabControl2.Location = New System.Drawing.Point(3, 120)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(633, 386)
        Me.TabControl2.TabIndex = 496
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.Color.White
        Me.TabPage4.Controls.Add(Me.LvF2DetailBahan)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(625, 360)
        Me.TabPage4.TabIndex = 0
        Me.TabPage4.Text = "Bahan"
        '
        'LvF2DetailBahan
        '
        Me.LvF2DetailBahan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LvF2DetailBahan.HideSelection = False
        Me.LvF2DetailBahan.Location = New System.Drawing.Point(3, 3)
        Me.LvF2DetailBahan.Name = "LvF2DetailBahan"
        Me.LvF2DetailBahan.Size = New System.Drawing.Size(619, 354)
        Me.LvF2DetailBahan.TabIndex = 1
        Me.LvF2DetailBahan.UseCompatibleStateImageBehavior = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Lv_Moisture_Content_2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(625, 360)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Moisture Content"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Lv_Moisture_Content_2
        '
        Me.Lv_Moisture_Content_2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lv_Moisture_Content_2.FullRowSelect = True
        Me.Lv_Moisture_Content_2.GridLines = True
        Me.Lv_Moisture_Content_2.HideSelection = False
        Me.Lv_Moisture_Content_2.Location = New System.Drawing.Point(0, 0)
        Me.Lv_Moisture_Content_2.Name = "Lv_Moisture_Content_2"
        Me.Lv_Moisture_Content_2.Size = New System.Drawing.Size(625, 360)
        Me.Lv_Moisture_Content_2.TabIndex = 5
        Me.Lv_Moisture_Content_2.UseCompatibleStateImageBehavior = False
        Me.Lv_Moisture_Content_2.View = System.Windows.Forms.View.Details
        '
        'LbF1EstHpp
        '
        Me.LbF1EstHpp.AutoSize = True
        Me.LbF1EstHpp.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.LbF1EstHpp.Location = New System.Drawing.Point(19, 634)
        Me.LbF1EstHpp.Name = "LbF1EstHpp"
        Me.LbF1EstHpp.Size = New System.Drawing.Size(93, 17)
        Me.LbF1EstHpp.TabIndex = 506
        Me.LbF1EstHpp.Text = "Posisi Tujuan"
        '
        'LbF2EstHpp
        '
        Me.LbF2EstHpp.AutoSize = True
        Me.LbF2EstHpp.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.LbF2EstHpp.Location = New System.Drawing.Point(687, 634)
        Me.LbF2EstHpp.Name = "LbF2EstHpp"
        Me.LbF2EstHpp.Size = New System.Drawing.Size(93, 17)
        Me.LbF2EstHpp.TabIndex = 507
        Me.LbF2EstHpp.Text = "Posisi Tujuan"
        '
        'N_EMI_SD_Compare_Formulator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1348, 691)
        Me.Controls.Add(Me.LbF2EstHpp)
        Me.Controls.Add(Me.LbF1EstHpp)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TbKeterangan)
        Me.Controls.Add(Me.BtnSimpan)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TbPosisiTujuan)
        Me.Controls.Add(Me.LblPO_NoNota)
        Me.Controls.Add(Me.TbPosisiSekarang)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.Name = "N_EMI_SD_Compare_Formulator"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Tc.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Tc As TabControl
    Friend WithEvents TabPage1 As TabPage
	Friend WithEvents TbPosisiSekarang As TextBox
	Friend WithEvents LblPO_NoNota As Label
	Friend WithEvents Label1 As Label
	Friend WithEvents TbPosisiTujuan As TextBox
	Friend WithEvents BtnSimpan As Button
	Friend WithEvents Label2 As Label
	Friend WithEvents TbKeterangan As TextBox
	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents GroupBox2 As GroupBox
	Friend WithEvents TabControl2 As TabControl
	Friend WithEvents TabPage4 As TabPage
	Friend WithEvents TlpF1 As TableLayoutPanel
	Friend WithEvents LvF1DetailBahan As ListView
	Friend WithEvents LvF2DetailBahan As ListView
	Friend WithEvents TlpF2 As TableLayoutPanel
	Friend WithEvents LbF1EstHpp As Label
	Friend WithEvents LbF2EstHpp As Label
	Friend WithEvents TabPage7 As TabPage
	Friend WithEvents Lv_Moisture_Content As ListView
	Friend WithEvents TabPage2 As TabPage
	Friend WithEvents Lv_Moisture_Content_2 As ListView
End Class
