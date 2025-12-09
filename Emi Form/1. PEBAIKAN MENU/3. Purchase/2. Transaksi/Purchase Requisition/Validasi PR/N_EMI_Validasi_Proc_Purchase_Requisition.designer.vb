<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class N_EMI_Validasi_Proc_Purchase_Requisition
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Lv_PR = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ValidasiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Lv_PRDetail = New System.Windows.Forms.ListView()
        Me.BtnBarangMasuk_Cari = New System.Windows.Forms.Button()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TolakToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(942, 43)
        Me.Panel1.TabIndex = 22
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 41)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(942, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(20, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(312, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Validasi - Purchase Requisition"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 43)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 34
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 55)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 491)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(922, 55)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 491)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Lv_PR
        '
        Me.Lv_PR.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Lv_PR.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Lv_PR.FullRowSelect = True
        Me.Lv_PR.GridLines = True
        Me.Lv_PR.HideSelection = False
        Me.Lv_PR.Location = New System.Drawing.Point(21, 91)
        Me.Lv_PR.Name = "Lv_PR"
        Me.Lv_PR.Size = New System.Drawing.Size(900, 181)
        Me.Lv_PR.TabIndex = 234
        Me.Lv_PR.UseCompatibleStateImageBehavior = False
        Me.Lv_PR.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ValidasiToolStripMenuItem, Me.TolakToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(181, 70)
        '
        'ValidasiToolStripMenuItem
        '
        Me.ValidasiToolStripMenuItem.Name = "ValidasiToolStripMenuItem"
        Me.ValidasiToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ValidasiToolStripMenuItem.Text = "Validasi"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(1, 596)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1436, 15)
        Me.Panel6.TabIndex = 39
        Me.Panel6.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(21, 273)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1436, 12)
        Me.Panel7.TabIndex = 39
        Me.Panel7.Visible = False
        '
        'Lv_PRDetail
        '
        Me.Lv_PRDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Lv_PRDetail.FullRowSelect = True
        Me.Lv_PRDetail.GridLines = True
        Me.Lv_PRDetail.HideSelection = False
        Me.Lv_PRDetail.Location = New System.Drawing.Point(21, 286)
        Me.Lv_PRDetail.Name = "Lv_PRDetail"
        Me.Lv_PRDetail.Size = New System.Drawing.Size(899, 313)
        Me.Lv_PRDetail.TabIndex = 341
        Me.Lv_PRDetail.UseCompatibleStateImageBehavior = False
        Me.Lv_PRDetail.View = System.Windows.Forms.View.Details
        '
        'BtnBarangMasuk_Cari
        '
        Me.BtnBarangMasuk_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnBarangMasuk_Cari.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnBarangMasuk_Cari.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnBarangMasuk_Cari.ForeColor = System.Drawing.Color.White
        Me.BtnBarangMasuk_Cari.Location = New System.Drawing.Point(21, 52)
        Me.BtnBarangMasuk_Cari.Name = "BtnBarangMasuk_Cari"
        Me.BtnBarangMasuk_Cari.Size = New System.Drawing.Size(116, 34)
        Me.BtnBarangMasuk_Cari.TabIndex = 343
        Me.BtnBarangMasuk_Cari.Text = "&Refresh"
        Me.BtnBarangMasuk_Cari.UseVisualStyleBackColor = False
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Location = New System.Drawing.Point(26, 682)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(1436, 12)
        Me.Panel8.TabIndex = 38
        Me.Panel8.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(738, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(189, 13)
        Me.Label2.TabIndex = 344
        Me.Label2.Text = "* Klik Kanan untuk melakukan Validasi"
        '
        'TolakToolStripMenuItem
        '
        Me.TolakToolStripMenuItem.Name = "TolakToolStripMenuItem"
        Me.TolakToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.TolakToolStripMenuItem.Text = "Tolak"
        '
        'N_EMI_Validasi_Proc_Purchase_Requisition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(942, 611)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Lv_PRDetail)
        Me.Controls.Add(Me.BtnBarangMasuk_Cari)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Lv_PR)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_Validasi_Proc_Purchase_Requisition"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Lv_PR As ListView
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Lv_PRDetail As ListView
    Friend WithEvents BtnBarangMasuk_Cari As Button
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents ValidasiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TolakToolStripMenuItem As ToolStripMenuItem
End Class
