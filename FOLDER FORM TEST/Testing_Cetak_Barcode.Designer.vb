<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Testing_Cetak_Barcode
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
        Me.Btn_Cetak = New System.Windows.Forms.Button()
        Me.Barcode = New System.Windows.Forms.PictureBox()
        CType(Me.Barcode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Btn_Cetak
        '
        Me.Btn_Cetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cetak.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Cetak.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cetak.ForeColor = System.Drawing.Color.White
        Me.Btn_Cetak.Location = New System.Drawing.Point(216, 134)
        Me.Btn_Cetak.Name = "Btn_Cetak"
        Me.Btn_Cetak.Size = New System.Drawing.Size(280, 124)
        Me.Btn_Cetak.TabIndex = 3
        Me.Btn_Cetak.Text = "&CETAK"
        Me.Btn_Cetak.UseVisualStyleBackColor = False
        '
        'Barcode
        '
        Me.Barcode.Location = New System.Drawing.Point(708, 40)
        Me.Barcode.Name = "Barcode"
        Me.Barcode.Size = New System.Drawing.Size(80, 72)
        Me.Barcode.TabIndex = 464
        Me.Barcode.TabStop = False
        Me.Barcode.Visible = False
        '
        'Testing_Cetak_Barcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Barcode)
        Me.Controls.Add(Me.Btn_Cetak)
        Me.Name = "Testing_Cetak_Barcode"
        Me.Text = "Testing_Cetak_Barcode"
        CType(Me.Barcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Btn_Cetak As Button
    Friend WithEvents Barcode As PictureBox
End Class
