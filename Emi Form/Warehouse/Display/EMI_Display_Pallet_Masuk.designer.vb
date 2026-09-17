<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Display_Pallet_Masuk
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
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
		Me.Lbl_Title = New System.Windows.Forms.Label()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Lv_BM_PerPallet = New System.Windows.Forms.ListView()
		Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.CetakToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.SalinNoFakturToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.Lv_BMPerPalletDetail = New System.Windows.Forms.ListView()
		Me.Btn_Refresh = New System.Windows.Forms.Button()
		Me.PictureBoxTracking = New System.Windows.Forms.PictureBox()
		Me.PictureBoxKdBrg = New System.Windows.Forms.PictureBox()
		Me.Panel1.SuspendLayout()
		Me.ContextMenuStrip1.SuspendLayout()
		CType(Me.PictureBoxTracking, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.PictureBoxKdBrg, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.PanelGradient1)
		Me.Panel1.Controls.Add(Me.Lbl_Title)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(1281, 51)
		Me.Panel1.TabIndex = 22
		'
		'PanelGradient1
		'
		Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
		Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
		Me.PanelGradient1.cuteTransparent1 = 100
		Me.PanelGradient1.cuteTransparent2 = 64
		Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.PanelGradient1.Location = New System.Drawing.Point(0, 49)
		Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
		Me.PanelGradient1.Name = "PanelGradient1"
		Me.PanelGradient1.Size = New System.Drawing.Size(1281, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Lbl_Title
		'
		Me.Lbl_Title.AutoSize = True
		Me.Lbl_Title.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Lbl_Title.Location = New System.Drawing.Point(5, 9)
		Me.Lbl_Title.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.Lbl_Title.Name = "Lbl_Title"
		Me.Lbl_Title.Size = New System.Drawing.Size(364, 30)
		Me.Lbl_Title.TabIndex = 0
		Me.Lbl_Title.Text = "Display - Barang Masuk Per Pallet"
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(0, 51)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(1280, 12)
		Me.Panel2.TabIndex = 34
		Me.Panel2.Visible = False
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Location = New System.Drawing.Point(1, 63)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 669)
		Me.Panel3.TabIndex = 35
		Me.Panel3.Visible = False
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Location = New System.Drawing.Point(1261, 60)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(19, 655)
		Me.Panel5.TabIndex = 37
		Me.Panel5.Visible = False
		'
		'Lv_BM_PerPallet
		'
		Me.Lv_BM_PerPallet.ContextMenuStrip = Me.ContextMenuStrip1
		Me.Lv_BM_PerPallet.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Lv_BM_PerPallet.FullRowSelect = True
		Me.Lv_BM_PerPallet.GridLines = True
		Me.Lv_BM_PerPallet.HideSelection = False
		Me.Lv_BM_PerPallet.Location = New System.Drawing.Point(21, 64)
		Me.Lv_BM_PerPallet.MultiSelect = False
		Me.Lv_BM_PerPallet.Name = "Lv_BM_PerPallet"
		Me.Lv_BM_PerPallet.Size = New System.Drawing.Size(1237, 561)
		Me.Lv_BM_PerPallet.TabIndex = 234
		Me.Lv_BM_PerPallet.UseCompatibleStateImageBehavior = False
		Me.Lv_BM_PerPallet.View = System.Windows.Forms.View.Details
		'
		'ContextMenuStrip1
		'
		Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CetakToolStripMenuItem, Me.SalinNoFakturToolStripMenuItem})
		Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
		Me.ContextMenuStrip1.Size = New System.Drawing.Size(155, 48)
		'
		'CetakToolStripMenuItem
		'
		Me.CetakToolStripMenuItem.Name = "CetakToolStripMenuItem"
		Me.CetakToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
		Me.CetakToolStripMenuItem.Text = "Cetak"
		'
		'SalinNoFakturToolStripMenuItem
		'
		Me.SalinNoFakturToolStripMenuItem.Name = "SalinNoFakturToolStripMenuItem"
		Me.SalinNoFakturToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
		Me.SalinNoFakturToolStripMenuItem.Text = "Salin No Faktur"
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.Color.Red
		Me.Panel6.Location = New System.Drawing.Point(20, 666)
		Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(1436, 15)
		Me.Panel6.TabIndex = 39
		Me.Panel6.Visible = False
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.Color.Red
		Me.Panel7.Location = New System.Drawing.Point(1261, 323)
		Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(1436, 24)
		Me.Panel7.TabIndex = 39
		Me.Panel7.Visible = False
		'
		'Lv_BMPerPalletDetail
		'
		Me.Lv_BMPerPalletDetail.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Lv_BMPerPalletDetail.FullRowSelect = True
		Me.Lv_BMPerPalletDetail.GridLines = True
		Me.Lv_BMPerPalletDetail.HideSelection = False
		Me.Lv_BMPerPalletDetail.Location = New System.Drawing.Point(1459, 64)
		Me.Lv_BMPerPalletDetail.Name = "Lv_BMPerPalletDetail"
		Me.Lv_BMPerPalletDetail.Size = New System.Drawing.Size(185, 126)
		Me.Lv_BMPerPalletDetail.TabIndex = 341
		Me.Lv_BMPerPalletDetail.UseCompatibleStateImageBehavior = False
		Me.Lv_BMPerPalletDetail.View = System.Windows.Forms.View.Details
		Me.Lv_BMPerPalletDetail.Visible = False
		'
		'Btn_Refresh
		'
		Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
		Me.Btn_Refresh.Location = New System.Drawing.Point(21, 631)
		Me.Btn_Refresh.Name = "Btn_Refresh"
		Me.Btn_Refresh.Size = New System.Drawing.Size(120, 32)
		Me.Btn_Refresh.TabIndex = 407
		Me.Btn_Refresh.Text = "&Refresh"
		Me.Btn_Refresh.UseVisualStyleBackColor = False
		'
		'PictureBoxTracking
		'
		Me.PictureBoxTracking.Location = New System.Drawing.Point(1373, 64)
		Me.PictureBoxTracking.Name = "PictureBoxTracking"
		Me.PictureBoxTracking.Size = New System.Drawing.Size(80, 72)
		Me.PictureBoxTracking.TabIndex = 345
		Me.PictureBoxTracking.TabStop = False
		Me.PictureBoxTracking.Visible = False
		'
		'PictureBoxKdBrg
		'
		Me.PictureBoxKdBrg.Location = New System.Drawing.Point(1287, 64)
		Me.PictureBoxKdBrg.Name = "PictureBoxKdBrg"
		Me.PictureBoxKdBrg.Size = New System.Drawing.Size(80, 72)
		Me.PictureBoxKdBrg.TabIndex = 344
		Me.PictureBoxKdBrg.TabStop = False
		Me.PictureBoxKdBrg.Visible = False
		'
		'EMI_Display_Pallet_Masuk
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(1281, 682)
		Me.Controls.Add(Me.Btn_Refresh)
		Me.Controls.Add(Me.PictureBoxTracking)
		Me.Controls.Add(Me.PictureBoxKdBrg)
		Me.Controls.Add(Me.Lv_BMPerPalletDetail)
		Me.Controls.Add(Me.Panel7)
		Me.Controls.Add(Me.Panel6)
		Me.Controls.Add(Me.Lv_BM_PerPallet)
		Me.Controls.Add(Me.Panel5)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel1)
		Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.Name = "EMI_Display_Pallet_Masuk"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.ContextMenuStrip1.ResumeLayout(False)
		CType(Me.PictureBoxTracking, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.PictureBoxKdBrg, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Title As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Lv_BM_PerPallet As ListView
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Lv_BMPerPalletDetail As ListView
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents CetakToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PictureBoxKdBrg As PictureBox
    Friend WithEvents PictureBoxTracking As PictureBox
    Friend WithEvents SalinNoFakturToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Btn_Refresh As Button
End Class
