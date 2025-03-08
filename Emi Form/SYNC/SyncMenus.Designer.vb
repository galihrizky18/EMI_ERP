<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SyncMenus
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
        Me.Label_DatabaseAwal = New System.Windows.Forms.Label()
        Me.Cmb_Database_Awal = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Cmb_DatabaseTujuan = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Btn_Set = New System.Windows.Forms.Button()
        Me.Cmb_Menus = New System.Windows.Forms.ComboBox()
        Me.Btn_Transfer = New System.Windows.Forms.Button()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
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
        Me.Panel1.Size = New System.Drawing.Size(568, 48)
        Me.Panel1.TabIndex = 25
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 45)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(568, 3)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(206, 9)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(126, 30)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Sync Menu"
        '
        'Label_DatabaseAwal
        '
        Me.Label_DatabaseAwal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_DatabaseAwal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label_DatabaseAwal.Location = New System.Drawing.Point(70, 78)
        Me.Label_DatabaseAwal.Name = "Label_DatabaseAwal"
        Me.Label_DatabaseAwal.Size = New System.Drawing.Size(149, 25)
        Me.Label_DatabaseAwal.TabIndex = 315
        Me.Label_DatabaseAwal.Text = "Database Awal"
        Me.Label_DatabaseAwal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Cmb_Database_Awal
        '
        Me.Cmb_Database_Awal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Database_Awal.FormattingEnabled = True
        Me.Cmb_Database_Awal.Location = New System.Drawing.Point(70, 106)
        Me.Cmb_Database_Awal.Name = "Cmb_Database_Awal"
        Me.Cmb_Database_Awal.Size = New System.Drawing.Size(149, 24)
        Me.Cmb_Database_Awal.TabIndex = 316
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(261, 86)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 30)
        Me.Label2.TabIndex = 317
        Me.Label2.Text = "-->"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(0, 56)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(70, 640)
        Me.Panel4.TabIndex = 318
        Me.Panel4.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(498, 56)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(70, 640)
        Me.Panel2.TabIndex = 318
        Me.Panel2.Visible = False
        '
        'Cmb_DatabaseTujuan
        '
        Me.Cmb_DatabaseTujuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_DatabaseTujuan.FormattingEnabled = True
        Me.Cmb_DatabaseTujuan.Location = New System.Drawing.Point(347, 106)
        Me.Cmb_DatabaseTujuan.Name = "Cmb_DatabaseTujuan"
        Me.Cmb_DatabaseTujuan.Size = New System.Drawing.Size(149, 24)
        Me.Cmb_DatabaseTujuan.TabIndex = 316
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(347, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(149, 25)
        Me.Label1.TabIndex = 315
        Me.Label1.Text = "Database Akhir"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(173, 197)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(224, 25)
        Me.Label3.TabIndex = 315
        Me.Label3.Text = "Tables"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Btn_Set
        '
        Me.Btn_Set.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Set.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Set.ForeColor = System.Drawing.Color.White
        Me.Btn_Set.Location = New System.Drawing.Point(204, 148)
        Me.Btn_Set.Name = "Btn_Set"
        Me.Btn_Set.Size = New System.Drawing.Size(156, 35)
        Me.Btn_Set.TabIndex = 319
        Me.Btn_Set.Text = "&Set"
        Me.Btn_Set.UseVisualStyleBackColor = False
        '
        'Cmb_Menus
        '
        Me.Cmb_Menus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Menus.Enabled = False
        Me.Cmb_Menus.FormattingEnabled = True
        Me.Cmb_Menus.Location = New System.Drawing.Point(173, 225)
        Me.Cmb_Menus.Name = "Cmb_Menus"
        Me.Cmb_Menus.Size = New System.Drawing.Size(224, 24)
        Me.Cmb_Menus.TabIndex = 316
        '
        'Btn_Transfer
        '
        Me.Btn_Transfer.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Transfer.Enabled = False
        Me.Btn_Transfer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Transfer.ForeColor = System.Drawing.Color.White
        Me.Btn_Transfer.Location = New System.Drawing.Point(204, 274)
        Me.Btn_Transfer.Name = "Btn_Transfer"
        Me.Btn_Transfer.Size = New System.Drawing.Size(156, 35)
        Me.Btn_Transfer.TabIndex = 319
        Me.Btn_Transfer.Text = "&Sync"
        Me.Btn_Transfer.UseVisualStyleBackColor = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(430, 274)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(76, 35)
        Me.Btn_Refresh.TabIndex = 319
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'SyncMenus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(568, 349)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Transfer)
        Me.Controls.Add(Me.Btn_Set)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Cmb_Menus)
        Me.Controls.Add(Me.Cmb_DatabaseTujuan)
        Me.Controls.Add(Me.Cmb_Database_Awal)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label_DatabaseAwal)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "SyncMenus"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Label_DatabaseAwal As Label
    Friend WithEvents Cmb_Database_Awal As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Cmb_DatabaseTujuan As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Btn_Set As Button
    Friend WithEvents Cmb_Menus As ComboBox
    Friend WithEvents Btn_Transfer As Button
    Friend WithEvents Btn_Refresh As Button
End Class
