<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Transaksi_Trial_Good_Received
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
        Me.LblPilihBarang_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Txt_FilterValue = New System.Windows.Forms.TextBox()
        Me.Cmb_Filter = New System.Windows.Forms.ComboBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Lv_GR = New System.Windows.Forms.ListView()
        Me.Txt_Pages_1 = New System.Windows.Forms.TextBox()
        Me.BtnFirst_GI = New System.Windows.Forms.Button()
        Me.BtnNext_GI = New System.Windows.Forms.Button()
        Me.BtnPrev_GI = New System.Windows.Forms.Button()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1184, 45)
        Me.Panel1.TabIndex = 24
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1184, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'LblPilihBarang_Judul
        '
        Me.LblPilihBarang_Judul.AutoSize = True
        Me.LblPilihBarang_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPilihBarang_Judul.Location = New System.Drawing.Point(15, 7)
        Me.LblPilihBarang_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LblPilihBarang_Judul.Name = "LblPilihBarang_Judul"
        Me.LblPilihBarang_Judul.Size = New System.Drawing.Size(339, 30)
        Me.LblPilihBarang_Judul.TabIndex = 0
        Me.LblPilihBarang_Judul.Text = "Transaksi - Good Received Trial"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 44)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 36
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 64)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 582)
        Me.Panel3.TabIndex = 37
        Me.Panel3.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(20, 595)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(942, 15)
        Me.Panel4.TabIndex = 36
        Me.Panel4.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(20, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 17)
        Me.Label3.TabIndex = 418
        Me.Label3.Text = "Filter"
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(610, 55)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(102, 30)
        Me.Btn_Refresh.TabIndex = 4
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(526, 55)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(80, 30)
        Me.Btn_Cari.TabIndex = 3
        Me.Btn_Cari.Text = "&Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Txt_FilterValue
        '
        Me.Txt_FilterValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_FilterValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_FilterValue.Enabled = False
        Me.Txt_FilterValue.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Txt_FilterValue.Location = New System.Drawing.Point(219, 59)
        Me.Txt_FilterValue.MaxLength = 50
        Me.Txt_FilterValue.Name = "Txt_FilterValue"
        Me.Txt_FilterValue.Size = New System.Drawing.Size(304, 22)
        Me.Txt_FilterValue.TabIndex = 2
        '
        'Cmb_Filter
        '
        Me.Cmb_Filter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cmb_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Filter.FormattingEnabled = True
        Me.Cmb_Filter.Location = New System.Drawing.Point(89, 58)
        Me.Cmb_Filter.Name = "Cmb_Filter"
        Me.Cmb_Filter.Size = New System.Drawing.Size(127, 24)
        Me.Cmb_Filter.TabIndex = 1
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(21, 82)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(942, 12)
        Me.Panel5.TabIndex = 8
        Me.Panel5.Visible = False
        '
        'Lv_GR
        '
        Me.Lv_GR.Cursor = System.Windows.Forms.Cursors.Default
        Me.Lv_GR.FullRowSelect = True
        Me.Lv_GR.GridLines = True
        Me.Lv_GR.HideSelection = False
        Me.Lv_GR.Location = New System.Drawing.Point(20, 94)
        Me.Lv_GR.Name = "Lv_GR"
        Me.Lv_GR.Size = New System.Drawing.Size(1145, 458)
        Me.Lv_GR.TabIndex = 0
        Me.Lv_GR.UseCompatibleStateImageBehavior = False
        Me.Lv_GR.View = System.Windows.Forms.View.Details
        '
        'Txt_Pages_1
        '
        Me.Txt_Pages_1.BackColor = System.Drawing.Color.White
        Me.Txt_Pages_1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Txt_Pages_1.Enabled = False
        Me.Txt_Pages_1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Txt_Pages_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Txt_Pages_1.Location = New System.Drawing.Point(991, 570)
        Me.Txt_Pages_1.Name = "Txt_Pages_1"
        Me.Txt_Pages_1.Size = New System.Drawing.Size(73, 15)
        Me.Txt_Pages_1.TabIndex = 508
        Me.Txt_Pages_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BtnFirst_GI
        '
        Me.BtnFirst_GI.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnFirst_GI.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnFirst_GI.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnFirst_GI.ForeColor = System.Drawing.Color.White
        Me.BtnFirst_GI.Location = New System.Drawing.Point(761, 563)
        Me.BtnFirst_GI.Name = "BtnFirst_GI"
        Me.BtnFirst_GI.Size = New System.Drawing.Size(102, 30)
        Me.BtnFirst_GI.TabIndex = 5
        Me.BtnFirst_GI.Text = "&First"
        Me.BtnFirst_GI.UseVisualStyleBackColor = False
        '
        'BtnNext_GI
        '
        Me.BtnNext_GI.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnNext_GI.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnNext_GI.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnNext_GI.ForeColor = System.Drawing.Color.White
        Me.BtnNext_GI.Location = New System.Drawing.Point(1063, 563)
        Me.BtnNext_GI.Name = "BtnNext_GI"
        Me.BtnNext_GI.Size = New System.Drawing.Size(102, 30)
        Me.BtnNext_GI.TabIndex = 7
        Me.BtnNext_GI.Text = "&Next"
        Me.BtnNext_GI.UseVisualStyleBackColor = False
        '
        'BtnPrev_GI
        '
        Me.BtnPrev_GI.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnPrev_GI.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnPrev_GI.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnPrev_GI.ForeColor = System.Drawing.Color.White
        Me.BtnPrev_GI.Location = New System.Drawing.Point(886, 563)
        Me.BtnPrev_GI.Name = "BtnPrev_GI"
        Me.BtnPrev_GI.Size = New System.Drawing.Size(102, 30)
        Me.BtnPrev_GI.TabIndex = 6
        Me.BtnPrev_GI.Text = "&Prev"
        Me.BtnPrev_GI.UseVisualStyleBackColor = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(1165, 59)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(19, 582)
        Me.Panel6.TabIndex = 37
        Me.Panel6.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(24, 552)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(942, 12)
        Me.Panel7.TabIndex = 36
        Me.Panel7.Visible = False
        '
        'N_EMI_Transaksi_Trial_Good_Received
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1184, 611)
        Me.Controls.Add(Me.Txt_Pages_1)
        Me.Controls.Add(Me.BtnFirst_GI)
        Me.Controls.Add(Me.BtnNext_GI)
        Me.Controls.Add(Me.BtnPrev_GI)
        Me.Controls.Add(Me.Lv_GR)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Cari)
        Me.Controls.Add(Me.Txt_FilterValue)
        Me.Controls.Add(Me.Cmb_Filter)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_Transaksi_Trial_Good_Received"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents LblPilihBarang_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Txt_FilterValue As TextBox
    Friend WithEvents Cmb_Filter As ComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Lv_GR As ListView
    Friend WithEvents Txt_Pages_1 As TextBox
    Friend WithEvents BtnFirst_GI As Button
    Friend WithEvents BtnNext_GI As Button
    Friend WithEvents BtnPrev_GI As Button
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
End Class
