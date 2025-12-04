<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CutOFF_PremixSN
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
        Me.Lv_Data = New System.Windows.Forms.ListView()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.cr = New System.Windows.Forms.Button()
        Me.Txt_NoReservasi = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Lv_Data
        '
        Me.Lv_Data.BackColor = System.Drawing.Color.White
        Me.Lv_Data.FullRowSelect = True
        Me.Lv_Data.GridLines = True
        Me.Lv_Data.HideSelection = False
        Me.Lv_Data.Location = New System.Drawing.Point(13, 47)
        Me.Lv_Data.Margin = New System.Windows.Forms.Padding(4)
        Me.Lv_Data.Name = "Lv_Data"
        Me.Lv_Data.Size = New System.Drawing.Size(1109, 413)
        Me.Lv_Data.TabIndex = 0
        Me.Lv_Data.UseCompatibleStateImageBehavior = False
        Me.Lv_Data.View = System.Windows.Forms.View.Details
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Simpan.Enabled = False
        Me.Btn_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(14, 476)
        Me.Btn_Simpan.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(121, 50)
        Me.Btn_Simpan.TabIndex = 422
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'cr
        '
        Me.cr.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.cr.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cr.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.cr.ForeColor = System.Drawing.Color.White
        Me.cr.Location = New System.Drawing.Point(341, 9)
        Me.cr.Margin = New System.Windows.Forms.Padding(4)
        Me.cr.Name = "cr"
        Me.cr.Size = New System.Drawing.Size(99, 30)
        Me.cr.TabIndex = 425
        Me.cr.Text = "&Cari"
        Me.cr.UseVisualStyleBackColor = False
        '
        'Txt_NoReservasi
        '
        Me.Txt_NoReservasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NoReservasi.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Txt_NoReservasi.Location = New System.Drawing.Point(113, 13)
        Me.Txt_NoReservasi.Margin = New System.Windows.Forms.Padding(4)
        Me.Txt_NoReservasi.Name = "Txt_NoReservasi"
        Me.Txt_NoReservasi.Size = New System.Drawing.Size(220, 22)
        Me.Txt_NoReservasi.TabIndex = 426
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(11, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 17)
        Me.Label1.TabIndex = 427
        Me.Label1.Text = "No Reservasi"
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(445, 9)
        Me.Btn_Refresh.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(99, 30)
        Me.Btn_Refresh.TabIndex = 425
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'CutOFF_PremixSN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1138, 533)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Txt_NoReservasi)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.cr)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Lv_Data)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "CutOFF_PremixSN"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Lv_Data As ListView
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents cr As Button
    Friend WithEvents Txt_NoReservasi As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Btn_Refresh As Button
End Class
