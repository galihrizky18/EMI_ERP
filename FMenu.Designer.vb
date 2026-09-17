<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FMenu
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
	Public Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FMenu))
		Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
		Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.ToolStripStatusLabel4 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
		Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
		Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
		Me.StatusStrip1.SuspendLayout()
		Me.SuspendLayout()
		'
		'StatusStrip1
		'
		Me.StatusStrip1.BackColor = System.Drawing.Color.White
		Me.StatusStrip1.Font = New System.Drawing.Font("Work Sans", 8.5!)
		Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
		Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel4, Me.ToolStripStatusLabel2, Me.ToolStripStatusLabel3})
		Me.StatusStrip1.Location = New System.Drawing.Point(0, 652)
		Me.StatusStrip1.Name = "StatusStrip1"
		Me.StatusStrip1.Size = New System.Drawing.Size(1325, 25)
		Me.StatusStrip1.TabIndex = 3
		Me.StatusStrip1.Text = "StatusStrip1"
		'
		'ToolStripStatusLabel1
		'
		Me.ToolStripStatusLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
		Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
		Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(86, 20)
		Me.ToolStripStatusLabel1.Text = "Login : Admin"
		'
		'ToolStripStatusLabel4
		'
		Me.ToolStripStatusLabel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
		Me.ToolStripStatusLabel4.Margin = New System.Windows.Forms.Padding(15, 3, 0, 2)
		Me.ToolStripStatusLabel4.Name = "ToolStripStatusLabel4"
		Me.ToolStripStatusLabel4.Size = New System.Drawing.Size(78, 20)
		Me.ToolStripStatusLabel4.Text = "Lokasi : Main"
		'
		'ToolStripStatusLabel2
		'
		Me.ToolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
		Me.ToolStripStatusLabel2.Font = New System.Drawing.Font("Work Sans Medium", 8.5!, System.Drawing.FontStyle.Bold)
		Me.ToolStripStatusLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(199, Byte), Integer))
		Me.ToolStripStatusLabel2.Margin = New System.Windows.Forms.Padding(15, 3, 0, 2)
		Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
		Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(232, 20)
		Me.ToolStripStatusLabel2.Text = "System Version : ERP System v2.0"
		'
		'ToolStripStatusLabel3
		'
		Me.ToolStripStatusLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
		Me.ToolStripStatusLabel3.Margin = New System.Windows.Forms.Padding(15, 3, 0, 2)
		Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
		Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(125, 20)
		Me.ToolStripStatusLabel3.Text = "2026-08-10 16:00:00"
		'
		'Timer1
		'
		Me.Timer1.Enabled = True
		Me.Timer1.Interval = 1000
		'
		'Timer2
		'
		Me.Timer2.Enabled = True
		Me.Timer2.Interval = 1800000
		'
		'MenuStrip1
		'
		Me.MenuStrip1.BackColor = System.Drawing.Color.White
		Me.MenuStrip1.Font = New System.Drawing.Font("Work Sans Medium", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
		Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
		Me.MenuStrip1.Name = "MenuStrip1"
		Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(6, 4, 0, 4)
		Me.MenuStrip1.Size = New System.Drawing.Size(1325, 28)
		Me.MenuStrip1.TabIndex = 5
		Me.MenuStrip1.Text = "MenuStrip1"
		'
		'FMenu
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
		Me.ClientSize = New System.Drawing.Size(1325, 677)
		Me.Controls.Add(Me.MenuStrip1)
		Me.Controls.Add(Me.StatusStrip1)
		Me.DoubleBuffered = True
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.IsMdiContainer = True
		Me.MainMenuStrip = Me.MenuStrip1
		Me.Name = "FMenu"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "ERP System - Modul Menu"
		Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
		Me.StatusStrip1.ResumeLayout(False)
		Me.StatusStrip1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents StatusStrip1 As StatusStrip
	Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
	Friend WithEvents ToolStripStatusLabel4 As ToolStripStatusLabel
	Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
	Friend WithEvents ToolStripStatusLabel3 As ToolStripStatusLabel
	Friend WithEvents Timer1 As Timer
	Friend WithEvents Timer2 As Timer
	Friend WithEvents MenuStrip1 As MenuStrip
End Class
