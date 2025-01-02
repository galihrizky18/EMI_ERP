<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Emi_Display_QI

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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Cmb_Filter_Jenis = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Txt_Filter_Value = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Lv_Data = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ReleaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Lv_QI_detail = New System.Windows.Forms.ListView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1056, 51)
        Me.Panel1.TabIndex = 27
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 49)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(1056, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 11)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(301, 25)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Display - Quality Inspection"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 64)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 601)
        Me.Panel3.TabIndex = 40
        Me.Panel3.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 39
        Me.Panel2.Visible = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(338, 63)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(80, 28)
        Me.Btn_Refresh.TabIndex = 387
        Me.Btn_Refresh.Text = "Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(20, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 20)
        Me.Label3.TabIndex = 386
        Me.Label3.Text = "Jenis"
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(338, 92)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(80, 28)
        Me.Btn_Cari.TabIndex = 385
        Me.Btn_Cari.Text = "Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Cmb_Filter_Jenis
        '
        Me.Cmb_Filter_Jenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Filter_Jenis.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_Filter_Jenis.FormattingEnabled = True
        Me.Cmb_Filter_Jenis.Location = New System.Drawing.Point(100, 63)
        Me.Cmb_Filter_Jenis.Name = "Cmb_Filter_Jenis"
        Me.Cmb_Filter_Jenis.Size = New System.Drawing.Size(232, 26)
        Me.Cmb_Filter_Jenis.TabIndex = 384
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(20, 95)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 20)
        Me.Label5.TabIndex = 383
        Me.Label5.Text = "Value"
        '
        'Txt_Filter_Value
        '
        Me.Txt_Filter_Value.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Filter_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Filter_Value.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Filter_Value.Location = New System.Drawing.Point(100, 95)
        Me.Txt_Filter_Value.MaxLength = 50
        Me.Txt_Filter_Value.Name = "Txt_Filter_Value"
        Me.Txt_Filter_Value.Size = New System.Drawing.Size(232, 23)
        Me.Txt_Filter_Value.TabIndex = 382
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(27, 120)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(942, 12)
        Me.Panel4.TabIndex = 39
        Me.Panel4.Visible = False
        '
        'Lv_Data
        '
        Me.Lv_Data.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Lv_Data.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_Data.FullRowSelect = True
        Me.Lv_Data.GridLines = True
        Me.Lv_Data.HideSelection = False
        Me.Lv_Data.Location = New System.Drawing.Point(2, 15)
        Me.Lv_Data.Name = "Lv_Data"
        Me.Lv_Data.Size = New System.Drawing.Size(1008, 210)
        Me.Lv_Data.TabIndex = 0
        Me.Lv_Data.UseCompatibleStateImageBehavior = False
        Me.Lv_Data.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReleaseToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(114, 26)
        '
        'ReleaseToolStripMenuItem
        '
        Me.ReleaseToolStripMenuItem.Name = "ReleaseToolStripMenuItem"
        Me.ReleaseToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
        Me.ReleaseToolStripMenuItem.Text = "Release"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1035, 85)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 601)
        Me.Panel5.TabIndex = 40
        Me.Panel5.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(20, 628)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(942, 12)
        Me.Panel6.TabIndex = 39
        Me.Panel6.Visible = False
        '
        'Lv_QI_detail
        '
        Me.Lv_QI_detail.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Lv_QI_detail.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_QI_detail.FullRowSelect = True
        Me.Lv_QI_detail.GridLines = True
        Me.Lv_QI_detail.HideSelection = False
        Me.Lv_QI_detail.Location = New System.Drawing.Point(1, 17)
        Me.Lv_QI_detail.Name = "Lv_QI_detail"
        Me.Lv_QI_detail.Size = New System.Drawing.Size(1012, 249)
        Me.Lv_QI_detail.TabIndex = 388
        Me.Lv_QI_detail.UseCompatibleStateImageBehavior = False
        Me.Lv_QI_detail.View = System.Windows.Forms.View.Details
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Lv_QI_detail)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 361)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1013, 267)
        Me.GroupBox1.TabIndex = 389
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detail Data QI"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Lv_Data)
        Me.GroupBox2.Location = New System.Drawing.Point(21, 130)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1012, 228)
        Me.GroupBox2.TabIndex = 390
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Data QI"
        '
        'Emi_Display_QI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1056, 642)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Btn_Cari)
        Me.Controls.Add(Me.Cmb_Filter_Jenis)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Txt_Filter_Value)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Emi_Display_QI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Cmb_Filter_Jenis As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Txt_Filter_Value As TextBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Lv_Data As ListView
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ReleaseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Lv_QI_detail As ListView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
End Class
