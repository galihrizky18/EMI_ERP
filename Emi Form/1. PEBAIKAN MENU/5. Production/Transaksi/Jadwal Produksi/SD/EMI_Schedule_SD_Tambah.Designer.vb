<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EMI_Schedule_SD_Tambah
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
        Me.TxtSchedule_NoFaktur = New System.Windows.Forms.TextBox()
        Me.BtnPilihBarang_Simpan = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'TxtSchedule_NoFaktur
        '
        Me.TxtSchedule_NoFaktur.Location = New System.Drawing.Point(230, 67)
        Me.TxtSchedule_NoFaktur.Name = "TxtSchedule_NoFaktur"
        Me.TxtSchedule_NoFaktur.Size = New System.Drawing.Size(100, 20)
        Me.TxtSchedule_NoFaktur.TabIndex = 0
        '
        'BtnPilihBarang_Simpan
        '
        Me.BtnPilihBarang_Simpan.Location = New System.Drawing.Point(350, 215)
        Me.BtnPilihBarang_Simpan.Name = "BtnPilihBarang_Simpan"
        Me.BtnPilihBarang_Simpan.Size = New System.Drawing.Size(100, 20)
        Me.BtnPilihBarang_Simpan.TabIndex = 1
        '
        'EMI_Schedule_SD_Tambah
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.BtnPilihBarang_Simpan)
        Me.Controls.Add(Me.TxtSchedule_NoFaktur)
        Me.Name = "EMI_Schedule_SD_Tambah"
        Me.Text = "EMI_Schedule_SD_Tambah"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtSchedule_NoFaktur As TextBox
    Friend WithEvents BtnPilihBarang_Simpan As TextBox
End Class
