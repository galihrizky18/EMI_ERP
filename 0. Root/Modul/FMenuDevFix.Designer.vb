<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FMenuDevFix
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
		Me.components = New System.ComponentModel.Container()
		Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
		Me.MasterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ProductionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.TransaksiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.DisplayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ProductionResultToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.BarangLainToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.PRDeptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
		Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.ToolStripStatusLabel4 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
		Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
		Me.GIToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.MenuStrip1.SuspendLayout()
		Me.StatusStrip1.SuspendLayout()
		Me.SuspendLayout()
		'
		'MenuStrip1
		'
		Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MasterToolStripMenuItem, Me.ProductionToolStripMenuItem, Me.BarangLainToolStripMenuItem})
		Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
		Me.MenuStrip1.Name = "MenuStrip1"
		Me.MenuStrip1.Size = New System.Drawing.Size(1277, 24)
		Me.MenuStrip1.TabIndex = 0
		Me.MenuStrip1.Text = "MenuStrip1"
		'
		'MasterToolStripMenuItem
		'
		Me.MasterToolStripMenuItem.Name = "MasterToolStripMenuItem"
		Me.MasterToolStripMenuItem.Size = New System.Drawing.Size(55, 20)
		Me.MasterToolStripMenuItem.Text = "Master"
		'
		'ProductionToolStripMenuItem
		'
		Me.ProductionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TransaksiToolStripMenuItem, Me.DisplayToolStripMenuItem, Me.ReportToolStripMenuItem})
		Me.ProductionToolStripMenuItem.Name = "ProductionToolStripMenuItem"
		Me.ProductionToolStripMenuItem.Size = New System.Drawing.Size(78, 20)
		Me.ProductionToolStripMenuItem.Text = "Production"
		'
		'TransaksiToolStripMenuItem
		'
		Me.TransaksiToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GIToolStripMenuItem})
		Me.TransaksiToolStripMenuItem.Name = "TransaksiToolStripMenuItem"
		Me.TransaksiToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
		Me.TransaksiToolStripMenuItem.Text = "Transaksi"
		'
		'DisplayToolStripMenuItem
		'
		Me.DisplayToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProductionResultToolStripMenuItem})
		Me.DisplayToolStripMenuItem.Name = "DisplayToolStripMenuItem"
		Me.DisplayToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
		Me.DisplayToolStripMenuItem.Text = "Display"
		'
		'ProductionResultToolStripMenuItem
		'
		Me.ProductionResultToolStripMenuItem.Name = "ProductionResultToolStripMenuItem"
		Me.ProductionResultToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
		Me.ProductionResultToolStripMenuItem.Text = "Production Result"
		'
		'ReportToolStripMenuItem
		'
		Me.ReportToolStripMenuItem.Name = "ReportToolStripMenuItem"
		Me.ReportToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
		Me.ReportToolStripMenuItem.Text = "Report"
		'
		'BarangLainToolStripMenuItem
		'
		Me.BarangLainToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PRDeptToolStripMenuItem})
		Me.BarangLainToolStripMenuItem.Name = "BarangLainToolStripMenuItem"
		Me.BarangLainToolStripMenuItem.Size = New System.Drawing.Size(81, 20)
		Me.BarangLainToolStripMenuItem.Text = "Barang Lain"
		'
		'PRDeptToolStripMenuItem
		'
		Me.PRDeptToolStripMenuItem.Name = "PRDeptToolStripMenuItem"
		Me.PRDeptToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
		Me.PRDeptToolStripMenuItem.Text = "PR Dept"
		'
		'StatusStrip1
		'
		Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel4, Me.ToolStripStatusLabel2, Me.ToolStripStatusLabel3})
		Me.StatusStrip1.Location = New System.Drawing.Point(0, 570)
		Me.StatusStrip1.Name = "StatusStrip1"
		Me.StatusStrip1.Size = New System.Drawing.Size(1277, 22)
		Me.StatusStrip1.TabIndex = 5
		Me.StatusStrip1.Text = "StatusStrip1"
		'
		'ToolStripStatusLabel1
		'
		Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
		Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(120, 17)
		Me.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1"
		'
		'ToolStripStatusLabel4
		'
		Me.ToolStripStatusLabel4.Name = "ToolStripStatusLabel4"
		Me.ToolStripStatusLabel4.Size = New System.Drawing.Size(120, 17)
		Me.ToolStripStatusLabel4.Text = "ToolStripStatusLabel4"
		'
		'ToolStripStatusLabel2
		'
		Me.ToolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
		Me.ToolStripStatusLabel2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
		Me.ToolStripStatusLabel2.ForeColor = System.Drawing.Color.DarkBlue
		Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
		Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(208, 17)
		Me.ToolStripStatusLabel2.Text = "Last Update : 24 Agustus 2024 11:50"
		'
		'ToolStripStatusLabel3
		'
		Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
		Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(120, 17)
		Me.ToolStripStatusLabel3.Text = "ToolStripStatusLabel3"
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
		'GIToolStripMenuItem
		'
		Me.GIToolStripMenuItem.Name = "GIToolStripMenuItem"
		Me.GIToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
		Me.GIToolStripMenuItem.Text = "GI"
		'
		'FMenuDevFix
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(1277, 592)
		Me.Controls.Add(Me.StatusStrip1)
		Me.Controls.Add(Me.MenuStrip1)
		Me.IsMdiContainer = True
		Me.MainMenuStrip = Me.MenuStrip1
		Me.Name = "FMenuDevFix"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
		Me.MenuStrip1.ResumeLayout(False)
		Me.MenuStrip1.PerformLayout()
		Me.StatusStrip1.ResumeLayout(False)
		Me.StatusStrip1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents MenuStrip1 As MenuStrip
	Friend WithEvents StatusStrip1 As StatusStrip
	Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
	Friend WithEvents ToolStripStatusLabel4 As ToolStripStatusLabel
	Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
	Friend WithEvents ToolStripStatusLabel3 As ToolStripStatusLabel
	Friend WithEvents Timer1 As Timer
	Friend WithEvents Timer2 As Timer
	Friend WithEvents MasterToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents ProductionToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents TransaksiToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents DisplayToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents ReportToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents ProductionResultToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents BarangLainToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents PRDeptToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents GIToolStripMenuItem As ToolStripMenuItem
End Class
