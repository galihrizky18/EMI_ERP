<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Display_Compare_Formula
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.LV_Data = New System.Windows.Forms.ListView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BTN_Refresh = New System.Windows.Forms.Button()
        Me.BTN_Cari = New System.Windows.Forms.Button()
        Me.TB_Value = New System.Windows.Forms.TextBox()
        Me.CMB_Filter = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BTN_Cetak = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
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
        Me.Panel1.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(934, 45)
        Me.Panel1.TabIndex = 27
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(15, 7)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(278, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Display Compare Formula"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 45)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(934, 12)
        Me.Panel2.TabIndex = 41
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 55)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 556)
        Me.Panel3.TabIndex = 42
        Me.Panel3.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(915, 56)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 556)
        Me.Panel4.TabIndex = 43
        Me.Panel4.Visible = False
        '
        'Panel14
        '
        Me.Panel14.BackColor = System.Drawing.Color.Red
        Me.Panel14.Location = New System.Drawing.Point(0, 596)
        Me.Panel14.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(929, 15)
        Me.Panel14.TabIndex = 544
        Me.Panel14.Visible = False
        '
        'LV_Data
        '
        Me.LV_Data.FullRowSelect = True
        Me.LV_Data.GridLines = True
        Me.LV_Data.HideSelection = False
        Me.LV_Data.Location = New System.Drawing.Point(19, 105)
        Me.LV_Data.Name = "LV_Data"
        Me.LV_Data.Size = New System.Drawing.Size(895, 431)
        Me.LV_Data.TabIndex = 546
        Me.LV_Data.UseCompatibleStateImageBehavior = False
        Me.LV_Data.View = System.Windows.Forms.View.Details
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.BTN_Refresh)
        Me.GroupBox1.Controls.Add(Me.BTN_Cari)
        Me.GroupBox1.Controls.Add(Me.TB_Value)
        Me.GroupBox1.Controls.Add(Me.CMB_Filter)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(19, 51)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(895, 48)
        Me.GroupBox1.TabIndex = 548
        Me.GroupBox1.TabStop = False
        '
        'BTN_Refresh
        '
        Me.BTN_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BTN_Refresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BTN_Refresh.ForeColor = System.Drawing.Color.White
        Me.BTN_Refresh.Location = New System.Drawing.Point(571, 12)
        Me.BTN_Refresh.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Refresh.Name = "BTN_Refresh"
        Me.BTN_Refresh.Size = New System.Drawing.Size(98, 28)
        Me.BTN_Refresh.TabIndex = 38
        Me.BTN_Refresh.Text = "&Refresh"
        Me.BTN_Refresh.UseVisualStyleBackColor = False
        '
        'BTN_Cari
        '
        Me.BTN_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BTN_Cari.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BTN_Cari.ForeColor = System.Drawing.Color.White
        Me.BTN_Cari.Location = New System.Drawing.Point(469, 12)
        Me.BTN_Cari.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Cari.Name = "BTN_Cari"
        Me.BTN_Cari.Size = New System.Drawing.Size(98, 28)
        Me.BTN_Cari.TabIndex = 38
        Me.BTN_Cari.Text = "&Cari"
        Me.BTN_Cari.UseVisualStyleBackColor = False
        '
        'TB_Value
        '
        Me.TB_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TB_Value.Location = New System.Drawing.Point(193, 16)
        Me.TB_Value.Name = "TB_Value"
        Me.TB_Value.Size = New System.Drawing.Size(270, 20)
        Me.TB_Value.TabIndex = 2
        '
        'CMB_Filter
        '
        Me.CMB_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CMB_Filter.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.CMB_Filter.FormattingEnabled = True
        Me.CMB_Filter.Location = New System.Drawing.Point(63, 14)
        Me.CMB_Filter.Name = "CMB_Filter"
        Me.CMB_Filter.Size = New System.Drawing.Size(124, 24)
        Me.CMB_Filter.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(7, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 17)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Filter"
        '
        'BTN_Cetak
        '
        Me.BTN_Cetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BTN_Cetak.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_Cetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BTN_Cetak.ForeColor = System.Drawing.Color.White
        Me.BTN_Cetak.Location = New System.Drawing.Point(20, 568)
        Me.BTN_Cetak.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Cetak.Name = "BTN_Cetak"
        Me.BTN_Cetak.Size = New System.Drawing.Size(146, 28)
        Me.BTN_Cetak.TabIndex = 39
        Me.BTN_Cetak.Text = "&Cetak Laporan"
        Me.BTN_Cetak.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(16, 539)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(455, 16)
        Me.Label5.TabIndex = 549
        Me.Label5.Text = "*Pilih dua formula dengan jenis trial yang sama untuk dibandingkan dalam laporan"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopCenter
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
        Me.PanelGradient1.Size = New System.Drawing.Size(934, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'N_EMI_Display_Compare_Formula
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(934, 611)
        Me.Controls.Add(Me.BTN_Cetak)
        Me.Controls.Add(Me.LV_Data)
        Me.Controls.Add(Me.Panel14)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label5)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.Name = "N_EMI_Display_Compare_Formula"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel14 As Panel
    Friend WithEvents LV_Data As ListView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents BTN_Refresh As Button
    Friend WithEvents BTN_Cari As Button
    Friend WithEvents TB_Value As TextBox
    Friend WithEvents CMB_Filter As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents BTN_Cetak As Button
    Friend WithEvents Label5 As Label
End Class
