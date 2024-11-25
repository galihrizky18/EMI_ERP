<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Master_CAM
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
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Cb_CamNumber = New System.Windows.Forms.ComboBox()
        Me.Btn_Delete = New System.Windows.Forms.Button()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Txt_IPPort = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txt_PasswordIP = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_UserIP = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Lv_Cam = New System.Windows.Forms.ListView()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Lbl_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(933, 63)
        Me.Panel1.TabIndex = 23
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(18, 14)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(155, 30)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Master - CAM"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Cb_CamNumber)
        Me.GroupBox1.Controls.Add(Me.Btn_Delete)
        Me.GroupBox1.Controls.Add(Me.Btn_Refresh)
        Me.GroupBox1.Controls.Add(Me.Btn_Simpan)
        Me.GroupBox1.Controls.Add(Me.Txt_IPPort)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Txt_PasswordIP)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Txt_UserIP)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 71)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(478, 244)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Input"
        '
        'Cb_CamNumber
        '
        Me.Cb_CamNumber.FormattingEnabled = True
        Me.Cb_CamNumber.Location = New System.Drawing.Point(184, 116)
        Me.Cb_CamNumber.Name = "Cb_CamNumber"
        Me.Cb_CamNumber.Size = New System.Drawing.Size(121, 26)
        Me.Cb_CamNumber.TabIndex = 422
        '
        'Btn_Delete
        '
        Me.Btn_Delete.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Delete.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Delete.ForeColor = System.Drawing.Color.White
        Me.Btn_Delete.Location = New System.Drawing.Point(370, 160)
        Me.Btn_Delete.Name = "Btn_Delete"
        Me.Btn_Delete.Size = New System.Drawing.Size(94, 32)
        Me.Btn_Delete.TabIndex = 6
        Me.Btn_Delete.Text = "&Delete"
        Me.Btn_Delete.UseVisualStyleBackColor = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(275, 160)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(94, 32)
        Me.Btn_Refresh.TabIndex = 5
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(180, 160)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(94, 32)
        Me.Btn_Simpan.TabIndex = 4
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Txt_IPPort
        '
        Me.Txt_IPPort.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_IPPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_IPPort.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_IPPort.Location = New System.Drawing.Point(184, 88)
        Me.Txt_IPPort.MaxLength = 50
        Me.Txt_IPPort.Name = "Txt_IPPort"
        Me.Txt_IPPort.Size = New System.Drawing.Size(280, 22)
        Me.Txt_IPPort.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(18, 89)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 20)
        Me.Label3.TabIndex = 421
        Me.Label3.Text = "IP Address"
        '
        'Txt_PasswordIP
        '
        Me.Txt_PasswordIP.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_PasswordIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_PasswordIP.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_PasswordIP.Location = New System.Drawing.Point(184, 60)
        Me.Txt_PasswordIP.MaxLength = 50
        Me.Txt_PasswordIP.Name = "Txt_PasswordIP"
        Me.Txt_PasswordIP.Size = New System.Drawing.Size(280, 22)
        Me.Txt_PasswordIP.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(18, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 20)
        Me.Label2.TabIndex = 421
        Me.Label2.Text = "Password"
        '
        'Txt_UserIP
        '
        Me.Txt_UserIP.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_UserIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_UserIP.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_UserIP.Location = New System.Drawing.Point(184, 32)
        Me.Txt_UserIP.MaxLength = 50
        Me.Txt_UserIP.Name = "Txt_UserIP"
        Me.Txt_UserIP.Size = New System.Drawing.Size(280, 22)
        Me.Txt_UserIP.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(18, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 20)
        Me.Label1.TabIndex = 421
        Me.Label1.Text = "Username"
        '
        'Lv_Cam
        '
        Me.Lv_Cam.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lv_Cam.FullRowSelect = True
        Me.Lv_Cam.GridLines = True
        Me.Lv_Cam.HideSelection = False
        Me.Lv_Cam.Location = New System.Drawing.Point(496, 79)
        Me.Lv_Cam.Name = "Lv_Cam"
        Me.Lv_Cam.Size = New System.Drawing.Size(425, 236)
        Me.Lv_Cam.TabIndex = 0
        Me.Lv_Cam.UseCompatibleStateImageBehavior = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(18, 119)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 20)
        Me.Label4.TabIndex = 421
        Me.Label4.Text = "Cam Number"
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 61)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(933, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Master_CAM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(933, 331)
        Me.Controls.Add(Me.Lv_Cam)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Master_CAM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Master_CAM"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Lv_Cam As ListView
    Friend WithEvents Txt_UserIP As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_IPPort As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Txt_PasswordIP As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Delete As Button
    Friend WithEvents Cb_CamNumber As ComboBox
    Friend WithEvents Label4 As Label
End Class
