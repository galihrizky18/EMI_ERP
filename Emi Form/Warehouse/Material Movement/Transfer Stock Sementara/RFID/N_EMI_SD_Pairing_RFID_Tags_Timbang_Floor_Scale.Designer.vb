<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_SD_Pairing_RFID_Tags_Timbang_Floor_Scale
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel_Margin_Top = New System.Windows.Forms.Panel()
        Me.Panel_Margin_Left = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel_Margin_Bottom = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Lv_RFID_Tags = New System.Windows.Forms.ListView()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(661, 51)
        Me.Panel1.TabIndex = 25
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(192, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Pairing RFID Tags"
        '
        'Panel_Margin_Top
        '
        Me.Panel_Margin_Top.BackColor = System.Drawing.Color.Red
        Me.Panel_Margin_Top.Location = New System.Drawing.Point(0, 51)
        Me.Panel_Margin_Top.Name = "Panel_Margin_Top"
        Me.Panel_Margin_Top.Size = New System.Drawing.Size(661, 12)
        Me.Panel_Margin_Top.TabIndex = 38
        Me.Panel_Margin_Top.Visible = False
        '
        'Panel_Margin_Left
        '
        Me.Panel_Margin_Left.BackColor = System.Drawing.Color.Red
        Me.Panel_Margin_Left.Location = New System.Drawing.Point(0, 63)
        Me.Panel_Margin_Left.Name = "Panel_Margin_Left"
        Me.Panel_Margin_Left.Size = New System.Drawing.Size(19, 428)
        Me.Panel_Margin_Left.TabIndex = 39
        Me.Panel_Margin_Left.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(642, 63)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(19, 428)
        Me.Panel2.TabIndex = 40
        Me.Panel2.Visible = False
        '
        'Panel_Margin_Bottom
        '
        Me.Panel_Margin_Bottom.BackColor = System.Drawing.Color.Red
        Me.Panel_Margin_Bottom.Location = New System.Drawing.Point(19, 478)
        Me.Panel_Margin_Bottom.Name = "Panel_Margin_Bottom"
        Me.Panel_Margin_Bottom.Size = New System.Drawing.Size(630, 12)
        Me.Panel_Margin_Bottom.TabIndex = 41
        Me.Panel_Margin_Bottom.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Lv_RFID_Tags)
        Me.GroupBox1.Location = New System.Drawing.Point(19, 65)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(10, 8, 10, 12)
        Me.GroupBox1.Size = New System.Drawing.Size(623, 371)
        Me.GroupBox1.TabIndex = 389
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "RFID Tags"
        '
        'Lv_RFID_Tags
        '
        Me.Lv_RFID_Tags.CheckBoxes = True
        Me.Lv_RFID_Tags.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lv_RFID_Tags.FullRowSelect = True
        Me.Lv_RFID_Tags.GridLines = True
        Me.Lv_RFID_Tags.HideSelection = False
        Me.Lv_RFID_Tags.Location = New System.Drawing.Point(10, 21)
        Me.Lv_RFID_Tags.Margin = New System.Windows.Forms.Padding(4)
        Me.Lv_RFID_Tags.Name = "Lv_RFID_Tags"
        Me.Lv_RFID_Tags.Size = New System.Drawing.Size(603, 338)
        Me.Lv_RFID_Tags.TabIndex = 383
        Me.Lv_RFID_Tags.UseCompatibleStateImageBehavior = False
        Me.Lv_RFID_Tags.View = System.Windows.Forms.View.Details
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(19, 442)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(113, 36)
        Me.Btn_Simpan.TabIndex = 390
        Me.Btn_Simpan.Text = "Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
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
        Me.PanelGradient1.Size = New System.Drawing.Size(661, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'N_EMI_SD_Pairing_RFID_Tags_Timbang_Floor_Scale
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(661, 490)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel_Margin_Bottom)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel_Margin_Left)
        Me.Controls.Add(Me.Panel_Margin_Top)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.Name = "N_EMI_SD_Pairing_RFID_Tags_Timbang_Floor_Scale"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel_Margin_Top As Panel
    Friend WithEvents Panel_Margin_Left As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel_Margin_Bottom As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Lv_RFID_Tags As ListView
    Friend WithEvents Btn_Simpan As Button
End Class
