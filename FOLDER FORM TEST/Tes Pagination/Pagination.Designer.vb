<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Pagination
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Btn_Prev = New System.Windows.Forms.Panel()
        Me.Lbl_Prev = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Btn_Next = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Btn_Prev.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Btn_Prev
        '
        Me.Btn_Prev.Controls.Add(Me.Lbl_Prev)
        Me.Btn_Prev.Location = New System.Drawing.Point(95, 57)
        Me.Btn_Prev.Name = "Btn_Prev"
        Me.Btn_Prev.Size = New System.Drawing.Size(49, 30)
        Me.Btn_Prev.TabIndex = 0
        '
        'Lbl_Prev
        '
        Me.Lbl_Prev.AutoSize = True
        Me.Lbl_Prev.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Prev.ForeColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(203, Byte), Integer))
        Me.Lbl_Prev.Location = New System.Drawing.Point(5, 5)
        Me.Lbl_Prev.Name = "Lbl_Prev"
        Me.Lbl_Prev.Size = New System.Drawing.Size(39, 20)
        Me.Lbl_Prev.TabIndex = 0
        Me.Lbl_Prev.Text = "Prev"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Btn_Next)
        Me.Panel1.Location = New System.Drawing.Point(352, 57)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(49, 30)
        Me.Panel1.TabIndex = 0
        '
        'Btn_Next
        '
        Me.Btn_Next.AutoSize = True
        Me.Btn_Next.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Btn_Next.ForeColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(203, Byte), Integer))
        Me.Btn_Next.Location = New System.Drawing.Point(5, 5)
        Me.Btn_Next.Name = "Btn_Next"
        Me.Btn_Next.Size = New System.Drawing.Size(40, 20)
        Me.Btn_Next.TabIndex = 0
        Me.Btn_Next.Text = "Next"
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(145, 57)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(206, 30)
        Me.FlowLayoutPanel1.TabIndex = 1
        '
        'Pagination
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Btn_Prev)
        Me.Name = "Pagination"
        Me.Size = New System.Drawing.Size(503, 143)
        Me.Btn_Prev.ResumeLayout(False)
        Me.Btn_Prev.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Btn_Prev As Panel
    Friend WithEvents Lbl_Prev As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Btn_Next As Label
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
End Class
