<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UPDATE_API_LAPORAN_HASIL_PRODUKSI_TRIAL_KITCHEN
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
		Me.TextBox1 = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Button1 = New System.Windows.Forms.Button()
		Me.TextBox2 = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.TextBox3 = New System.Windows.Forms.TextBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.TextBox4 = New System.Windows.Forms.TextBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.SuspendLayout()
		'
		'TextBox1
		'
		Me.TextBox1.Location = New System.Drawing.Point(260, 83)
		Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
		Me.TextBox1.Name = "TextBox1"
		Me.TextBox1.Size = New System.Drawing.Size(371, 20)
		Me.TextBox1.TabIndex = 0
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Work Sans", 10.0!)
		Me.Label1.Location = New System.Drawing.Point(22, 84)
		Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(125, 20)
		Me.Label1.TabIndex = 1
		Me.Label1.Text = "Link API Formula"
		'
		'Button1
		'
		Me.Button1.Location = New System.Drawing.Point(260, 203)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(134, 40)
		Me.Button1.TabIndex = 2
		Me.Button1.Text = "Simpan"
		Me.Button1.UseVisualStyleBackColor = True
		'
		'TextBox2
		'
		Me.TextBox2.Location = New System.Drawing.Point(260, 111)
		Me.TextBox2.Margin = New System.Windows.Forms.Padding(4)
		Me.TextBox2.Name = "TextBox2"
		Me.TextBox2.Size = New System.Drawing.Size(371, 20)
		Me.TextBox2.TabIndex = 0
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
		Me.Label2.Location = New System.Drawing.Point(22, 112)
		Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(163, 20)
		Me.Label2.TabIndex = 1
		Me.Label2.Text = "Link API Trial Produksi"
		'
		'TextBox3
		'
		Me.TextBox3.Location = New System.Drawing.Point(260, 139)
		Me.TextBox3.Margin = New System.Windows.Forms.Padding(4)
		Me.TextBox3.Name = "TextBox3"
		Me.TextBox3.Size = New System.Drawing.Size(371, 20)
		Me.TextBox3.TabIndex = 0
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
		Me.Label3.Location = New System.Drawing.Point(22, 168)
		Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(230, 20)
		Me.Label3.TabIndex = 1
		Me.Label3.Text = "Link API Trial Produksi Compare"
		'
		'TextBox4
		'
		Me.TextBox4.Location = New System.Drawing.Point(260, 167)
		Me.TextBox4.Margin = New System.Windows.Forms.Padding(4)
		Me.TextBox4.Name = "TextBox4"
		Me.TextBox4.Size = New System.Drawing.Size(371, 20)
		Me.TextBox4.TabIndex = 0
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Work Sans", 10.0!)
		Me.Label4.Location = New System.Drawing.Point(22, 140)
		Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(131, 20)
		Me.Label4.TabIndex = 1
		Me.Label4.Text = "Link API Compare"
		'
		'UPDATE_API_LAPORAN_HASIL_PRODUKSI_TRIAL_KITCHEN
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(778, 332)
		Me.Controls.Add(Me.Button1)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.TextBox4)
		Me.Controls.Add(Me.TextBox3)
		Me.Controls.Add(Me.TextBox2)
		Me.Controls.Add(Me.TextBox1)
		Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.Name = "UPDATE_API_LAPORAN_HASIL_PRODUKSI_TRIAL_KITCHEN"
		Me.Text = "UPDATE_API_LAPORAN_HASIL_PRODUKSI_TRIAL_KITCHEN"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents TextBox1 As TextBox
	Friend WithEvents Label1 As Label
	Friend WithEvents Button1 As Button
	Friend WithEvents TextBox2 As TextBox
	Friend WithEvents Label2 As Label
	Friend WithEvents TextBox3 As TextBox
	Friend WithEvents Label3 As Label
	Friend WithEvents TextBox4 As TextBox
	Friend WithEvents Label4 As Label
End Class
