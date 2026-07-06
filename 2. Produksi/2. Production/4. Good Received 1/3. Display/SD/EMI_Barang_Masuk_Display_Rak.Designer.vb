<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EMI_Barang_Masuk_Display_Rak
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
        Me.TxtNoBM = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'TxtNoBM
        '
        Me.TxtNoBM.Location = New System.Drawing.Point(215, 92)
        Me.TxtNoBM.Name = "TxtNoBM"
        Me.TxtNoBM.Size = New System.Drawing.Size(100, 20)
        Me.TxtNoBM.TabIndex = 0
        '
        'EMI_Barang_Masuk_Display_Rak
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.TxtNoBM)
        Me.Name = "EMI_Barang_Masuk_Display_Rak"
        Me.Text = "EMI_Barang_Masuk_Display_Rak"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtNoBM As TextBox
End Class
