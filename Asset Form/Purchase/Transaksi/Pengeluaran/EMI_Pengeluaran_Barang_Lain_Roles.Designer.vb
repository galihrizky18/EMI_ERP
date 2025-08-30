<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EMI_Pengeluaran_Barang_Lain_Roles
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LvRoles = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TxtUserID = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.BtnRefresh = New System.Windows.Forms.Button()
        Me.BtnHapus = New System.Windows.Forms.Button()
        Me.BtnSimpan = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.BtnCari = New System.Windows.Forms.Button()
        Me.TxtValue = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CmbKolom = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LvDisplay = New System.Windows.Forms.ListView()
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.LvUser = New System.Windows.Forms.ListView()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Lbl_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(498, 47)
        Me.Panel1.TabIndex = 234
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 44)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(498, 3)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(7, 7)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(345, 30)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Roles Pengeluaran Barang Asset"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.LvRoles)
        Me.GroupBox1.Controls.Add(Me.TxtUserID)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Location = New System.Drawing.Point(17, 56)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(468, 197)
        Me.GroupBox1.TabIndex = 235
        Me.GroupBox1.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 18)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Roles"
        '
        'LvRoles
        '
        Me.LvRoles.CheckBoxes = True
        Me.LvRoles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.LvRoles.FullRowSelect = True
        Me.LvRoles.GridLines = True
        Me.LvRoles.HideSelection = False
        Me.LvRoles.Location = New System.Drawing.Point(85, 47)
        Me.LvRoles.Name = "LvRoles"
        Me.LvRoles.Size = New System.Drawing.Size(306, 136)
        Me.LvRoles.TabIndex = 2
        Me.LvRoles.UseCompatibleStateImageBehavior = False
        Me.LvRoles.View = System.Windows.Forms.View.List
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Roles"
        Me.ColumnHeader1.Width = 139
        '
        'TxtUserID
        '
        Me.TxtUserID.Location = New System.Drawing.Point(84, 21)
        Me.TxtUserID.MaxLength = 15
        Me.TxtUserID.Name = "TxtUserID"
        Me.TxtUserID.Size = New System.Drawing.Size(167, 23)
        Me.TxtUserID.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 18)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "User ID"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.BtnExit)
        Me.GroupBox2.Controls.Add(Me.BtnRefresh)
        Me.GroupBox2.Controls.Add(Me.BtnHapus)
        Me.GroupBox2.Controls.Add(Me.BtnSimpan)
        Me.GroupBox2.Location = New System.Drawing.Point(17, 255)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(468, 59)
        Me.GroupBox2.TabIndex = 236
        Me.GroupBox2.TabStop = False
        '
        'BtnExit
        '
        Me.BtnExit.Location = New System.Drawing.Point(347, 17)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(113, 31)
        Me.BtnExit.TabIndex = 6
        Me.BtnExit.Text = "E&xit"
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'BtnRefresh
        '
        Me.BtnRefresh.Location = New System.Drawing.Point(234, 17)
        Me.BtnRefresh.Name = "BtnRefresh"
        Me.BtnRefresh.Size = New System.Drawing.Size(113, 31)
        Me.BtnRefresh.TabIndex = 5
        Me.BtnRefresh.Text = "&Refresh"
        Me.BtnRefresh.UseVisualStyleBackColor = True
        '
        'BtnHapus
        '
        Me.BtnHapus.Location = New System.Drawing.Point(121, 17)
        Me.BtnHapus.Name = "BtnHapus"
        Me.BtnHapus.Size = New System.Drawing.Size(113, 31)
        Me.BtnHapus.TabIndex = 4
        Me.BtnHapus.Text = "&Hapus"
        Me.BtnHapus.UseVisualStyleBackColor = True
        '
        'BtnSimpan
        '
        Me.BtnSimpan.Location = New System.Drawing.Point(8, 17)
        Me.BtnSimpan.Name = "BtnSimpan"
        Me.BtnSimpan.Size = New System.Drawing.Size(113, 31)
        Me.BtnSimpan.TabIndex = 3
        Me.BtnSimpan.Text = "&Simpan"
        Me.BtnSimpan.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.BtnCari)
        Me.GroupBox3.Controls.Add(Me.TxtValue)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.CmbKolom)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.LvDisplay)
        Me.GroupBox3.Location = New System.Drawing.Point(17, 316)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(468, 316)
        Me.GroupBox3.TabIndex = 237
        Me.GroupBox3.TabStop = False
        '
        'BtnCari
        '
        Me.BtnCari.Location = New System.Drawing.Point(377, 286)
        Me.BtnCari.Name = "BtnCari"
        Me.BtnCari.Size = New System.Drawing.Size(87, 26)
        Me.BtnCari.TabIndex = 10
        Me.BtnCari.Text = "&Cari"
        Me.BtnCari.UseVisualStyleBackColor = True
        '
        'TxtValue
        '
        Me.TxtValue.Location = New System.Drawing.Point(247, 287)
        Me.TxtValue.Name = "TxtValue"
        Me.TxtValue.Size = New System.Drawing.Size(127, 23)
        Me.TxtValue.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(199, 289)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 18)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Value"
        '
        'CmbKolom
        '
        Me.CmbKolom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbKolom.FormattingEnabled = True
        Me.CmbKolom.Location = New System.Drawing.Point(59, 286)
        Me.CmbKolom.Name = "CmbKolom"
        Me.CmbKolom.Size = New System.Drawing.Size(134, 26)
        Me.CmbKolom.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 289)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 18)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Kolom"
        '
        'LvDisplay
        '
        Me.LvDisplay.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2, Me.ColumnHeader4})
        Me.LvDisplay.FullRowSelect = True
        Me.LvDisplay.GridLines = True
        Me.LvDisplay.HideSelection = False
        Me.LvDisplay.Location = New System.Drawing.Point(6, 14)
        Me.LvDisplay.Name = "LvDisplay"
        Me.LvDisplay.Size = New System.Drawing.Size(456, 270)
        Me.LvDisplay.TabIndex = 7
        Me.LvDisplay.UseCompatibleStateImageBehavior = False
        Me.LvDisplay.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "User ID"
        Me.ColumnHeader2.Width = 210
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Nama Role"
        Me.ColumnHeader4.Width = 210
        '
        'LvUser
        '
        Me.LvUser.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3})
        Me.LvUser.FullRowSelect = True
        Me.LvUser.GridLines = True
        Me.LvUser.HideSelection = False
        Me.LvUser.Location = New System.Drawing.Point(611, 103)
        Me.LvUser.Name = "LvUser"
        Me.LvUser.Size = New System.Drawing.Size(190, 182)
        Me.LvUser.TabIndex = 238
        Me.LvUser.UseCompatibleStateImageBehavior = False
        Me.LvUser.View = System.Windows.Forms.View.Details
        Me.LvUser.Visible = False
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "User ID"
        Me.ColumnHeader3.Width = 163
        '
        'Pengeluaran_Barang_Roles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(498, 647)
        Me.Controls.Add(Me.LvUser)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Pengeluaran_Barang_Roles"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents LvRoles As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents TxtUserID As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents BtnExit As Button
    Friend WithEvents BtnRefresh As Button
    Friend WithEvents BtnHapus As Button
    Friend WithEvents BtnSimpan As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents TxtValue As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents CmbKolom As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents LvDisplay As ListView
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents BtnCari As Button
    Friend WithEvents LvUser As ListView
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents Label3 As Label
End Class
