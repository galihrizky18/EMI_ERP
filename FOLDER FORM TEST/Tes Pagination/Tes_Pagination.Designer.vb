<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Tes_Pagination
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
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Pagination1 = New ERP_EMI.Pagination()
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(43, 56)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(708, 247)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'Pagination1
        '
        Me.Pagination1.BackColor = System.Drawing.Color.Transparent
        Me.Pagination1.Location = New System.Drawing.Point(433, 309)
        Me.Pagination1.Name = "Pagination1"
        Me.Pagination1.Size = New System.Drawing.Size(318, 41)
        Me.Pagination1.TabIndex = 1
        '
        'Tes_Pagination
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Pagination1)
        Me.Controls.Add(Me.ListView1)
        Me.Name = "Tes_Pagination"
        Me.Text = "Tes_Pagination"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ListView1 As ListView
    Friend WithEvents Pagination1 As Pagination
End Class
