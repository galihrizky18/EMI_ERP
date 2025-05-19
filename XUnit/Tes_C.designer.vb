<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Tes_C
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
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.StreamPlayerControl1 = New WebEye.Controls.WinForms.StreamPlayerControl.StreamPlayerControl()
        Me.StreamPlayerControl2 = New WebEye.Controls.WinForms.StreamPlayerControl.StreamPlayerControl()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Panel1.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(984, 51)
        Me.Panel1.TabIndex = 22
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
        Me.PanelGradient1.Size = New System.Drawing.Size(984, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 11)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(159, 25)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "(づ｡◕‿‿◕｡)づ "
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1208, 10)
        Me.Panel2.TabIndex = 34
        Me.Panel2.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(20, 908)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1225, 12)
        Me.Panel4.TabIndex = 35
        Me.Panel4.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 63)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 687)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'StreamPlayerControl1
        '
        Me.StreamPlayerControl1.BackColor = System.Drawing.Color.LightGray
        Me.StreamPlayerControl1.Location = New System.Drawing.Point(5, 20)
        Me.StreamPlayerControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.StreamPlayerControl1.Name = "StreamPlayerControl1"
        Me.StreamPlayerControl1.Size = New System.Drawing.Size(942, 390)
        Me.StreamPlayerControl1.TabIndex = 463
        '
        'StreamPlayerControl2
        '
        Me.StreamPlayerControl2.BackColor = System.Drawing.Color.LightGray
        Me.StreamPlayerControl2.Location = New System.Drawing.Point(5, 419)
        Me.StreamPlayerControl2.Margin = New System.Windows.Forms.Padding(5)
        Me.StreamPlayerControl2.Name = "StreamPlayerControl2"
        Me.StreamPlayerControl2.Size = New System.Drawing.Size(942, 426)
        Me.StreamPlayerControl2.TabIndex = 464
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.StreamPlayerControl1)
        Me.GroupBox2.Controls.Add(Me.StreamPlayerControl2)
        Me.GroupBox2.Location = New System.Drawing.Point(20, 63)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(955, 845)
        Me.GroupBox2.TabIndex = 465
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Foto Kendaraan"
        '
        'Tes_C
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(984, 920)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Tes_C"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents StreamPlayerControl1 As WebEye.Controls.WinForms.StreamPlayerControl.StreamPlayerControl
    Friend WithEvents StreamPlayerControl2 As WebEye.Controls.WinForms.StreamPlayerControl.StreamPlayerControl
    Friend WithEvents GroupBox2 As GroupBox
    '''Friend WithEvents StreamPlayerControl1 As WebEye.Controls.WinForms.StreamPlayerControl.StreamPlayerControl
    '''Friend WithEvents StreamPlayerControl2 As WebEye.Controls.WinForms.StreamPlayerControl.StreamPlayerControl
End Class
