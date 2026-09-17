<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main_Menu
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main_Menu))
		Me.FlowLayoutMenu_Main = New System.Windows.Forms.FlowLayoutPanel()
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
		Me.Lbl_UserGreeting = New System.Windows.Forms.Label()
		Me.Lbl_HeaderTitle = New System.Windows.Forms.Label()
		Me.PictureBox1 = New System.Windows.Forms.PictureBox()
		Me.PanelSubHeader = New System.Windows.Forms.Panel()
		Me.Lbl_ModuleCountBadge = New System.Windows.Forms.Label()
		Me.Lbl_WorkspaceSubtitle = New System.Windows.Forms.Label()
		Me.Lbl_WorkspaceTitle = New System.Windows.Forms.Label()
		Me.Panel1.SuspendLayout()
		CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.PanelSubHeader.SuspendLayout()
		Me.SuspendLayout()
		'
		'FlowLayoutMenu_Main
		'
		Me.FlowLayoutMenu_Main.AutoScroll = True
		Me.FlowLayoutMenu_Main.BackColor = System.Drawing.Color.Transparent
		Me.FlowLayoutMenu_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
		Me.FlowLayoutMenu_Main.Location = New System.Drawing.Point(140, 130)
		Me.FlowLayoutMenu_Main.Margin = New System.Windows.Forms.Padding(4)
		Me.FlowLayoutMenu_Main.Name = "FlowLayoutMenu_Main"
		Me.FlowLayoutMenu_Main.Padding = New System.Windows.Forms.Padding(12)
		Me.FlowLayoutMenu_Main.Size = New System.Drawing.Size(920, 500)
		Me.FlowLayoutMenu_Main.TabIndex = 0
		'
		'Panel1
		'
		Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
		Me.Panel1.Controls.Add(Me.PanelGradient1)
		Me.Panel1.Controls.Add(Me.Lbl_UserGreeting)
		Me.Panel1.Controls.Add(Me.Lbl_HeaderTitle)
		Me.Panel1.Controls.Add(Me.PictureBox1)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(1200, 65)
		Me.Panel1.TabIndex = 1
		'
		'PanelGradient1
		'
		Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(199, Byte), Integer))
		Me.PanelGradient1.cuteColor2 = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
		Me.PanelGradient1.cuteTransparent1 = 100
		Me.PanelGradient1.cuteTransparent2 = 64
		Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.PanelGradient1.Location = New System.Drawing.Point(0, 62)
		Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
		Me.PanelGradient1.Name = "PanelGradient1"
		Me.PanelGradient1.Size = New System.Drawing.Size(1200, 3)
		Me.PanelGradient1.TabIndex = 23
		'
		'Lbl_UserGreeting
		'
		Me.Lbl_UserGreeting.AutoSize = True
		Me.Lbl_UserGreeting.Font = New System.Drawing.Font("Work Sans", 8.5!)
		Me.Lbl_UserGreeting.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
		Me.Lbl_UserGreeting.Location = New System.Drawing.Point(180, 36)
		Me.Lbl_UserGreeting.Name = "Lbl_UserGreeting"
		Me.Lbl_UserGreeting.Size = New System.Drawing.Size(225, 17)
		Me.Lbl_UserGreeting.TabIndex = 2
		Me.Lbl_UserGreeting.Text = "👤 ERP Main Dashboard | Executive Control"
		'
		'Lbl_HeaderTitle
		'
		Me.Lbl_HeaderTitle.AutoSize = True
		Me.Lbl_HeaderTitle.Font = New System.Drawing.Font("Work Sans SemiBold", 12.0!, System.Drawing.FontStyle.Bold)
		Me.Lbl_HeaderTitle.ForeColor = System.Drawing.Color.White
		Me.Lbl_HeaderTitle.Location = New System.Drawing.Point(180, 11)
		Me.Lbl_HeaderTitle.Name = "Lbl_HeaderTitle"
		Me.Lbl_HeaderTitle.Size = New System.Drawing.Size(296, 23)
		Me.Lbl_HeaderTitle.TabIndex = 1
		Me.Lbl_HeaderTitle.Text = "PT. EVO MANUFACTURING INDONESIA"
		'
		'PictureBox1
		'
		Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
		Me.PictureBox1.InitialImage = CType(resources.GetObject("PictureBox1.InitialImage"), System.Drawing.Image)
		Me.PictureBox1.Location = New System.Drawing.Point(18, 6)
		Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
		Me.PictureBox1.Name = "PictureBox1"
		Me.PictureBox1.Size = New System.Drawing.Size(150, 50)
		Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
		Me.PictureBox1.TabIndex = 0
		Me.PictureBox1.TabStop = False
		'
		'PanelSubHeader
		'
		Me.PanelSubHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
		Me.PanelSubHeader.Controls.Add(Me.Lbl_ModuleCountBadge)
		Me.PanelSubHeader.Controls.Add(Me.Lbl_WorkspaceSubtitle)
		Me.PanelSubHeader.Controls.Add(Me.Lbl_WorkspaceTitle)
		Me.PanelSubHeader.Dock = System.Windows.Forms.DockStyle.Top
		Me.PanelSubHeader.Location = New System.Drawing.Point(0, 65)
		Me.PanelSubHeader.Name = "PanelSubHeader"
		Me.PanelSubHeader.Size = New System.Drawing.Size(1200, 52)
		Me.PanelSubHeader.TabIndex = 2
		'
		'Lbl_ModuleCountBadge
		'
		Me.Lbl_ModuleCountBadge.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Lbl_ModuleCountBadge.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(254, Byte), Integer))
		Me.Lbl_ModuleCountBadge.Font = New System.Drawing.Font("Work Sans", 8.5!, System.Drawing.FontStyle.Bold)
		Me.Lbl_ModuleCountBadge.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(199, Byte), Integer))
		Me.Lbl_ModuleCountBadge.Location = New System.Drawing.Point(1050, 13)
		Me.Lbl_ModuleCountBadge.Name = "Lbl_ModuleCountBadge"
		Me.Lbl_ModuleCountBadge.Size = New System.Drawing.Size(130, 26)
		Me.Lbl_ModuleCountBadge.TabIndex = 2
		Me.Lbl_ModuleCountBadge.Text = "✨ Modul ERP"
		Me.Lbl_ModuleCountBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'Lbl_WorkspaceSubtitle
		'
		Me.Lbl_WorkspaceSubtitle.AutoSize = True
		Me.Lbl_WorkspaceSubtitle.Font = New System.Drawing.Font("Work Sans", 8.5!)
		Me.Lbl_WorkspaceSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
		Me.Lbl_WorkspaceSubtitle.Location = New System.Drawing.Point(20, 28)
		Me.Lbl_WorkspaceSubtitle.Name = "Lbl_WorkspaceSubtitle"
		Me.Lbl_WorkspaceSubtitle.Size = New System.Drawing.Size(362, 17)
		Me.Lbl_WorkspaceSubtitle.TabIndex = 1
		Me.Lbl_WorkspaceSubtitle.Text = "Pilih modul operasional di bawah ini untuk mengakses fitur sistem."
		'
		'Lbl_WorkspaceTitle
		'
		Me.Lbl_WorkspaceTitle.AutoSize = True
		Me.Lbl_WorkspaceTitle.Font = New System.Drawing.Font("Work Sans", 10.0!, System.Drawing.FontStyle.Bold)
		Me.Lbl_WorkspaceTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
		Me.Lbl_WorkspaceTitle.Location = New System.Drawing.Point(20, 8)
		Me.Lbl_WorkspaceTitle.Name = "Lbl_WorkspaceTitle"
		Me.Lbl_WorkspaceTitle.Size = New System.Drawing.Size(225, 20)
		Me.Lbl_WorkspaceTitle.TabIndex = 0
		Me.Lbl_WorkspaceTitle.Text = "🚀 MODUL UTAMA SISTEM ERP"
		'
		'Main_Menu
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
		Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
		Me.ClientSize = New System.Drawing.Size(1200, 680)
		Me.Controls.Add(Me.FlowLayoutMenu_Main)
		Me.Controls.Add(Me.PanelSubHeader)
		Me.Controls.Add(Me.Panel1)
		Me.DoubleBuffered = True
		Me.Font = New System.Drawing.Font("Work Sans", 8.25!)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.Name = "Main_Menu"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = ".:: Menu Utama ::."
		Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.PanelSubHeader.ResumeLayout(False)
		Me.PanelSubHeader.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents FlowLayoutMenu_Main As FlowLayoutPanel
	Friend WithEvents Panel1 As Panel
	Friend WithEvents PictureBox1 As PictureBox
	Friend WithEvents PanelGradient1 As ERP_EMI.CustomControl.PanelGradient
	Friend WithEvents Lbl_UserGreeting As Label
	Friend WithEvents Lbl_HeaderTitle As Label
	Friend WithEvents PanelSubHeader As Panel
	Friend WithEvents Lbl_ModuleCountBadge As Label
	Friend WithEvents Lbl_WorkspaceSubtitle As Label
	Friend WithEvents Lbl_WorkspaceTitle As Label
End Class
