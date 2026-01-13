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
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.btnFirst = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnLast = New System.Windows.Forms.Button()
        Me.lblInfo = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btnPrev
        '
        Me.btnPrev.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPrev.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnPrev.ForeColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(203, Byte), Integer))
        Me.btnPrev.Location = New System.Drawing.Point(45, 6)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(30, 30)
        Me.btnPrev.TabIndex = 2
        Me.btnPrev.Text = "<"
        Me.btnPrev.UseVisualStyleBackColor = True
        '
        'btnFirst
        '
        Me.btnFirst.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnFirst.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnFirst.ForeColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(203, Byte), Integer))
        Me.btnFirst.Location = New System.Drawing.Point(5, 6)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(38, 30)
        Me.btnFirst.TabIndex = 2
        Me.btnFirst.Text = "<<"
        Me.btnFirst.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNext.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnNext.ForeColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(203, Byte), Integer))
        Me.btnNext.Location = New System.Drawing.Point(203, 6)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(30, 30)
        Me.btnNext.TabIndex = 2
        Me.btnNext.Text = ">"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnLast
        '
        Me.btnLast.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLast.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnLast.ForeColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(203, Byte), Integer))
        Me.btnLast.Location = New System.Drawing.Point(235, 6)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(38, 30)
        Me.btnLast.TabIndex = 2
        Me.btnLast.Text = ">>"
        Me.btnLast.UseVisualStyleBackColor = True
        '
        'lblInfo
        '
        Me.lblInfo.BackColor = System.Drawing.Color.White
        Me.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblInfo.Font = New System.Drawing.Font("Work Sans SemiBold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblInfo.Location = New System.Drawing.Point(79, 14)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.ReadOnly = True
        Me.lblInfo.Size = New System.Drawing.Size(120, 15)
        Me.lblInfo.TabIndex = 3
        Me.lblInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Pagination
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.btnFirst)
        Me.Controls.Add(Me.btnLast)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPrev)
        Me.Name = "Pagination"
        Me.Size = New System.Drawing.Size(281, 41)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnPrev As Button
    Friend WithEvents btnFirst As Button
    Friend WithEvents btnNext As Button
    Friend WithEvents btnLast As Button
    Friend WithEvents lblInfo As TextBox
End Class
