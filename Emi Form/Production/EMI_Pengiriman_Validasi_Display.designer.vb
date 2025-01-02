<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Pengiriman_Validasi_Display
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
        Me.Panel2T = New System.Windows.Forms.Panel()
        Me.Panel3T = New System.Windows.Forms.Panel()
        Me.Panel5T = New System.Windows.Forms.Panel()
        Me.Panel4T = New System.Windows.Forms.Panel()
        Me.ListView1T = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SetEkspedisiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnCari = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
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
        Me.Panel1.Size = New System.Drawing.Size(796, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(796, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(229, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Cek Data Pengiriman"
        '
        'Panel2T
        '
        Me.Panel2T.BackColor = System.Drawing.Color.Red
        Me.Panel2T.Location = New System.Drawing.Point(0, 51)
        Me.Panel2T.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2T.Name = "Panel2T"
        Me.Panel2T.Size = New System.Drawing.Size(942, 12)
        Me.Panel2T.TabIndex = 34
        Me.Panel2T.Visible = False
        '
        'Panel3T
        '
        Me.Panel3T.BackColor = System.Drawing.Color.Red
        Me.Panel3T.Location = New System.Drawing.Point(1, 63)
        Me.Panel3T.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3T.Name = "Panel3T"
        Me.Panel3T.Size = New System.Drawing.Size(19, 450)
        Me.Panel3T.TabIndex = 35
        Me.Panel3T.Visible = False
        '
        'Panel5T
        '
        Me.Panel5T.BackColor = System.Drawing.Color.Red
        Me.Panel5T.Location = New System.Drawing.Point(777, 63)
        Me.Panel5T.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5T.Name = "Panel5T"
        Me.Panel5T.Size = New System.Drawing.Size(19, 446)
        Me.Panel5T.TabIndex = 37
        Me.Panel5T.Visible = False
        '
        'Panel4T
        '
        Me.Panel4T.BackColor = System.Drawing.Color.Red
        Me.Panel4T.Location = New System.Drawing.Point(-3, 498)
        Me.Panel4T.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4T.Name = "Panel4T"
        Me.Panel4T.Size = New System.Drawing.Size(1436, 15)
        Me.Panel4T.TabIndex = 38
        Me.Panel4T.Visible = False
        '
        'ListView1T
        '
        Me.ListView1T.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ListView1T.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.ListView1T.FullRowSelect = True
        Me.ListView1T.GridLines = True
        Me.ListView1T.HideSelection = False
        Me.ListView1T.Location = New System.Drawing.Point(20, 111)
        Me.ListView1T.Name = "ListView1T"
        Me.ListView1T.Size = New System.Drawing.Size(754, 380)
        Me.ListView1T.TabIndex = 234
        Me.ListView1T.UseCompatibleStateImageBehavior = False
        Me.ListView1T.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SetEkspedisiToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(142, 26)
        '
        'SetEkspedisiToolStripMenuItem
        '
        Me.SetEkspedisiToolStripMenuItem.Name = "SetEkspedisiToolStripMenuItem"
        Me.SetEkspedisiToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.SetEkspedisiToolStripMenuItem.Text = "Set Ekspedisi"
        '
        'btnCari
        '
        Me.btnCari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.btnCari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnCari.ForeColor = System.Drawing.Color.White
        Me.btnCari.Location = New System.Drawing.Point(534, 74)
        Me.btnCari.Name = "btnCari"
        Me.btnCari.Size = New System.Drawing.Size(74, 29)
        Me.btnCari.TabIndex = 359
        Me.btnCari.Text = "Cari"
        Me.btnCari.UseVisualStyleBackColor = False
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.DropDownWidth = 150
        Me.ComboBox1.Font = New System.Drawing.Font("Work Sans", 8.9!)
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(81, 77)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(195, 25)
        Me.ComboBox1.TabIndex = 363
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(287, 78)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 20)
        Me.Label5.TabIndex = 362
        Me.Label5.Text = "Value"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(21, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 20)
        Me.Label4.TabIndex = 361
        Me.Label4.Text = "Kolom"
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TextBox3.Location = New System.Drawing.Point(339, 78)
        Me.TextBox3.MaxLength = 50
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(189, 22)
        Me.TextBox3.TabIndex = 360
        '
        'Master_Cek_Data_Pengiriman
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(796, 514)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.btnCari)
        Me.Controls.Add(Me.ListView1T)
        Me.Controls.Add(Me.Panel4T)
        Me.Controls.Add(Me.Panel5T)
        Me.Controls.Add(Me.Panel3T)
        Me.Controls.Add(Me.Panel2T)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Master_Cek_Data_Pengiriman"
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
    Friend WithEvents Panel2T As Panel
    Friend WithEvents Panel3T As Panel
    Friend WithEvents Panel5T As Panel
    Friend WithEvents Panel4T As Panel
    Friend WithEvents ListView1T As ListView
    Friend WithEvents btnCari As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents SetEkspedisiToolStripMenuItem As ToolStripMenuItem
End Class
