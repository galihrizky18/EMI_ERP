<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_SD_Tambah_Master_Data_Hierarki_Binding_Formula
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
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
		Me.Lbl_Judul = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.TxtUrutan = New System.Windows.Forms.TextBox()
		Me.BtnSimpan = New System.Windows.Forms.Button()
		Me.TxtKodeHierarki = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.Panel1.SuspendLayout()
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
		Me.Panel1.Size = New System.Drawing.Size(439, 44)
		Me.Panel1.TabIndex = 31
		'
		'PanelGradient1
		'
		Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
		Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
		Me.PanelGradient1.cuteTransparent1 = 100
		Me.PanelGradient1.cuteTransparent2 = 64
		Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.PanelGradient1.Location = New System.Drawing.Point(0, 42)
		Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
		Me.PanelGradient1.Name = "PanelGradient1"
		Me.PanelGradient1.Size = New System.Drawing.Size(439, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Lbl_Judul
		'
		Me.Lbl_Judul.AutoSize = True
		Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Lbl_Judul.Location = New System.Drawing.Point(21, 10)
		Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.Lbl_Judul.Name = "Lbl_Judul"
		Me.Lbl_Judul.Size = New System.Drawing.Size(164, 24)
		Me.Lbl_Judul.TabIndex = 0
		Me.Lbl_Judul.Text = "Tambah Hierarki"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(22, 100)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(39, 13)
		Me.Label4.TabIndex = 490
		Me.Label4.Text = "Urutan"
		'
		'TxtUrutan
		'
		Me.TxtUrutan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.TxtUrutan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtUrutan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.TxtUrutan.Location = New System.Drawing.Point(112, 96)
		Me.TxtUrutan.MaxLength = 50
		Me.TxtUrutan.Name = "TxtUrutan"
		Me.TxtUrutan.Size = New System.Drawing.Size(73, 21)
		Me.TxtUrutan.TabIndex = 489
		'
		'BtnSimpan
		'
		Me.BtnSimpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.BtnSimpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
		Me.BtnSimpan.ForeColor = System.Drawing.Color.White
		Me.BtnSimpan.Location = New System.Drawing.Point(112, 130)
		Me.BtnSimpan.Name = "BtnSimpan"
		Me.BtnSimpan.Size = New System.Drawing.Size(156, 28)
		Me.BtnSimpan.TabIndex = 488
		Me.BtnSimpan.Text = "&Simpan"
		Me.BtnSimpan.UseVisualStyleBackColor = False
		'
		'TxtKodeHierarki
		'
		Me.TxtKodeHierarki.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.TxtKodeHierarki.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtKodeHierarki.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.TxtKodeHierarki.Location = New System.Drawing.Point(112, 67)
		Me.TxtKodeHierarki.MaxLength = 50
		Me.TxtKodeHierarki.Name = "TxtKodeHierarki"
		Me.TxtKodeHierarki.Size = New System.Drawing.Size(302, 21)
		Me.TxtKodeHierarki.TabIndex = 487
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(21, 71)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(71, 13)
		Me.Label2.TabIndex = 486
		Me.Label2.Text = "Kode Hierarki"
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(-38, 44)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(515, 10)
		Me.Panel2.TabIndex = 491
		Me.Panel2.Visible = False
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Location = New System.Drawing.Point(0, 52)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(16, 186)
		Me.Panel3.TabIndex = 467
		Me.Panel3.Visible = False
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Location = New System.Drawing.Point(423, 53)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(16, 186)
		Me.Panel4.TabIndex = 468
		Me.Panel4.Visible = False
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.Color.Red
		Me.Panel6.Location = New System.Drawing.Point(6, 191)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(509, 10)
		Me.Panel6.TabIndex = 468
		Me.Panel6.Visible = False
		'
		'N_EMI_SD_Tambah_Master_Data_Hierarki_Binding_Formula
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(439, 201)
		Me.Controls.Add(Me.Panel6)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.TxtUrutan)
		Me.Controls.Add(Me.BtnSimpan)
		Me.Controls.Add(Me.TxtKodeHierarki)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Panel1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.MaximizeBox = False
		Me.Name = "N_EMI_SD_Tambah_Master_Data_Hierarki_Binding_Formula"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtUrutan As TextBox
    Friend WithEvents BtnSimpan As Button
    Friend WithEvents TxtKodeHierarki As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel6 As Panel
End Class
