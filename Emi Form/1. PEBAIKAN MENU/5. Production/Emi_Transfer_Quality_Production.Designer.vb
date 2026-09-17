<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Emi_Transfer_Quality_Production
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
		Me.Lv_So = New System.Windows.Forms.ListView()
		Me.Txt_NoSplit = New System.Windows.Forms.TextBox()
		Me.Txt_NoPO = New System.Windows.Forms.TextBox()
		Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
		Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
		Me.Txt_Stock = New System.Windows.Forms.TextBox()
		Me.Txt_Satuan = New System.Windows.Forms.TextBox()
		Me.SuspendLayout()
		'
		'Lv_So
		'
		Me.Lv_So.HideSelection = False
		Me.Lv_So.Location = New System.Drawing.Point(48, 292)
		Me.Lv_So.Name = "Lv_So"
		Me.Lv_So.Size = New System.Drawing.Size(121, 97)
		Me.Lv_So.TabIndex = 0
		Me.Lv_So.UseCompatibleStateImageBehavior = False
		'
		'Txt_NoSplit
		'
		Me.Txt_NoSplit.Location = New System.Drawing.Point(178, 73)
		Me.Txt_NoSplit.Name = "Txt_NoSplit"
		Me.Txt_NoSplit.Size = New System.Drawing.Size(100, 20)
		Me.Txt_NoSplit.TabIndex = 1
		'
		'Txt_NoPO
		'
		Me.Txt_NoPO.Location = New System.Drawing.Point(320, 73)
		Me.Txt_NoPO.Name = "Txt_NoPO"
		Me.Txt_NoPO.Size = New System.Drawing.Size(100, 20)
		Me.Txt_NoPO.TabIndex = 2
		'
		'Txt_KdBarang
		'
		Me.Txt_KdBarang.Location = New System.Drawing.Point(178, 99)
		Me.Txt_KdBarang.Name = "Txt_KdBarang"
		Me.Txt_KdBarang.Size = New System.Drawing.Size(100, 20)
		Me.Txt_KdBarang.TabIndex = 3
		'
		'Txt_NmBarang
		'
		Me.Txt_NmBarang.Location = New System.Drawing.Point(320, 99)
		Me.Txt_NmBarang.Name = "Txt_NmBarang"
		Me.Txt_NmBarang.Size = New System.Drawing.Size(100, 20)
		Me.Txt_NmBarang.TabIndex = 4
		'
		'Txt_Stock
		'
		Me.Txt_Stock.Location = New System.Drawing.Point(178, 125)
		Me.Txt_Stock.Name = "Txt_Stock"
		Me.Txt_Stock.Size = New System.Drawing.Size(100, 20)
		Me.Txt_Stock.TabIndex = 5
		'
		'Txt_Satuan
		'
		Me.Txt_Satuan.Location = New System.Drawing.Point(320, 125)
		Me.Txt_Satuan.Name = "Txt_Satuan"
		Me.Txt_Satuan.Size = New System.Drawing.Size(100, 20)
		Me.Txt_Satuan.TabIndex = 6
		'
		'Emi_Transfer_Quality_Production
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(800, 450)
		Me.Controls.Add(Me.Txt_Satuan)
		Me.Controls.Add(Me.Txt_Stock)
		Me.Controls.Add(Me.Txt_NmBarang)
		Me.Controls.Add(Me.Txt_KdBarang)
		Me.Controls.Add(Me.Txt_NoPO)
		Me.Controls.Add(Me.Txt_NoSplit)
		Me.Controls.Add(Me.Lv_So)
		Me.Name = "Emi_Transfer_Quality_Production"
		Me.Text = "Emi_Transfer_Quality_Production"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Lv_So As ListView
	Friend WithEvents Txt_NoSplit As TextBox
	Friend WithEvents Txt_NoPO As TextBox
	Friend WithEvents Txt_KdBarang As TextBox
	Friend WithEvents Txt_NmBarang As TextBox
	Friend WithEvents Txt_Stock As TextBox
	Friend WithEvents Txt_Satuan As TextBox
End Class
