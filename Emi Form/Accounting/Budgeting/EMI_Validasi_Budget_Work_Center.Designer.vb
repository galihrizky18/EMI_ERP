<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EMI_Validasi_Budget_Work_Center
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
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Lv_Aktual_WorkCenter = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ValidasiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TolakToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Lv_Detail = New System.Windows.Forms.ListView()
        Me.Panel4 = New System.Windows.Forms.Panel()
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
        Me.Panel1.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1084, 54)
        Me.Panel1.TabIndex = 27
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1084, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(19, 15)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(334, 25)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Validasi - Budgeting VS Aktual"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 57)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1179, 10)
        Me.Panel2.TabIndex = 41
        Me.Panel2.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(0, 77)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(10, 659)
        Me.Panel5.TabIndex = 413
        Me.Panel5.Visible = False
        '
        'Lv_Aktual_WorkCenter
        '
        Me.Lv_Aktual_WorkCenter.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Lv_Aktual_WorkCenter.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_Aktual_WorkCenter.FullRowSelect = True
        Me.Lv_Aktual_WorkCenter.GridLines = True
        Me.Lv_Aktual_WorkCenter.HideSelection = False
        Me.Lv_Aktual_WorkCenter.Location = New System.Drawing.Point(5, 22)
        Me.Lv_Aktual_WorkCenter.Name = "Lv_Aktual_WorkCenter"
        Me.Lv_Aktual_WorkCenter.Size = New System.Drawing.Size(1050, 282)
        Me.Lv_Aktual_WorkCenter.TabIndex = 414
        Me.Lv_Aktual_WorkCenter.UseCompatibleStateImageBehavior = False
        Me.Lv_Aktual_WorkCenter.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ValidasiToolStripMenuItem, Me.TolakToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(110, 48)
        '
        'ValidasiToolStripMenuItem
        '
        Me.ValidasiToolStripMenuItem.Name = "ValidasiToolStripMenuItem"
        Me.ValidasiToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.ValidasiToolStripMenuItem.Text = "Terima"
        '
        'TolakToolStripMenuItem
        '
        Me.TolakToolStripMenuItem.Name = "TolakToolStripMenuItem"
        Me.TolakToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.TolakToolStripMenuItem.Text = "Tolak"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Lv_Aktual_WorkCenter)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 62)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1060, 310)
        Me.GroupBox1.TabIndex = 415
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Aktualisasi Budget"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(12, 690)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1179, 10)
        Me.Panel3.TabIndex = 41
        Me.Panel3.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Lv_Detail)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 378)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1060, 310)
        Me.GroupBox2.TabIndex = 415
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Detail"
        '
        'Lv_Detail
        '
        Me.Lv_Detail.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_Detail.FullRowSelect = True
        Me.Lv_Detail.GridLines = True
        Me.Lv_Detail.HideSelection = False
        Me.Lv_Detail.Location = New System.Drawing.Point(3, 22)
        Me.Lv_Detail.Name = "Lv_Detail"
        Me.Lv_Detail.Size = New System.Drawing.Size(1051, 282)
        Me.Lv_Detail.TabIndex = 414
        Me.Lv_Detail.UseCompatibleStateImageBehavior = False
        Me.Lv_Detail.View = System.Windows.Forms.View.Details
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(1074, 77)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(10, 659)
        Me.Panel4.TabIndex = 413
        Me.Panel4.Visible = False
        '
        'EMI_Validasi_Budget_Work_Center
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1084, 701)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Validasi_Budget_Work_Center"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Lv_Aktual_WorkCenter As ListView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Lv_Detail As ListView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ValidasiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TolakToolStripMenuItem As ToolStripMenuItem
End Class
