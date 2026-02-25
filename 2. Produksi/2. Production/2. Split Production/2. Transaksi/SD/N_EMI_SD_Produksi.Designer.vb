<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_SD_Produksi
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Txt_NoPO = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
        Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
        Me.Cmb_Lokasi_Awal = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Cmb_Lokasi_Tujuan = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
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
        Me.Panel1.Size = New System.Drawing.Size(622, 42)
        Me.Panel1.TabIndex = 24
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 40)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(622, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(15, 6)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(204, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Ubah Lokasi Tujuan"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 50)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 552)
        Me.Panel3.TabIndex = 37
        Me.Panel3.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(27, 41)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 38
        Me.Panel2.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(603, 50)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 552)
        Me.Panel4.TabIndex = 37
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(20, 195)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(942, 15)
        Me.Panel5.TabIndex = 38
        Me.Panel5.Visible = False
        '
        'Txt_NoPO
        '
        Me.Txt_NoPO.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NoPO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NoPO.Enabled = False
        Me.Txt_NoPO.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_NoPO.Location = New System.Drawing.Point(90, 14)
        Me.Txt_NoPO.Name = "Txt_NoPO"
        Me.Txt_NoPO.Size = New System.Drawing.Size(282, 20)
        Me.Txt_NoPO.TabIndex = 40
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Label2.Location = New System.Drawing.Point(4, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 16)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "No PO"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Label3.Location = New System.Drawing.Point(4, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 16)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "Barang"
        '
        'Txt_KdBarang
        '
        Me.Txt_KdBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_KdBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_KdBarang.Enabled = False
        Me.Txt_KdBarang.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_KdBarang.Location = New System.Drawing.Point(90, 40)
        Me.Txt_KdBarang.Name = "Txt_KdBarang"
        Me.Txt_KdBarang.Size = New System.Drawing.Size(121, 20)
        Me.Txt_KdBarang.TabIndex = 40
        '
        'Txt_NmBarang
        '
        Me.Txt_NmBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NmBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NmBarang.Enabled = False
        Me.Txt_NmBarang.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Txt_NmBarang.Location = New System.Drawing.Point(217, 40)
        Me.Txt_NmBarang.Name = "Txt_NmBarang"
        Me.Txt_NmBarang.Size = New System.Drawing.Size(311, 20)
        Me.Txt_NmBarang.TabIndex = 40
        '
        'Cmb_Lokasi_Awal
        '
        Me.Cmb_Lokasi_Awal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lokasi_Awal.Enabled = False
        Me.Cmb_Lokasi_Awal.FormattingEnabled = True
        Me.Cmb_Lokasi_Awal.Location = New System.Drawing.Point(90, 66)
        Me.Cmb_Lokasi_Awal.Name = "Cmb_Lokasi_Awal"
        Me.Cmb_Lokasi_Awal.Size = New System.Drawing.Size(121, 24)
        Me.Cmb_Lokasi_Awal.TabIndex = 41
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Txt_NoPO)
        Me.GroupBox1.Controls.Add(Me.Cmb_Lokasi_Tujuan)
        Me.GroupBox1.Controls.Add(Me.Cmb_Lokasi_Awal)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Txt_NmBarang)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Txt_KdBarang)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 48)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(580, 106)
        Me.GroupBox1.TabIndex = 42
        Me.GroupBox1.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(218, 70)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(28, 16)
        Me.Label5.TabIndex = 42
        Me.Label5.Text = "==>"
        '
        'Cmb_Lokasi_Tujuan
        '
        Me.Cmb_Lokasi_Tujuan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cmb_Lokasi_Tujuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lokasi_Tujuan.FormattingEnabled = True
        Me.Cmb_Lokasi_Tujuan.Location = New System.Drawing.Point(251, 66)
        Me.Cmb_Lokasi_Tujuan.Name = "Cmb_Lokasi_Tujuan"
        Me.Cmb_Lokasi_Tujuan.Size = New System.Drawing.Size(121, 24)
        Me.Cmb_Lokasi_Tujuan.TabIndex = 41
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Label4.Location = New System.Drawing.Point(6, 70)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 16)
        Me.Label4.TabIndex = 39
        Me.Label4.Text = "Lokasi"
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(110, 160)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(100, 35)
        Me.Btn_Simpan.TabIndex = 43
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(214, 160)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(100, 35)
        Me.Button1.TabIndex = 43
        Me.Button1.Text = "&Keluar"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'N_EMI_SD_Produksi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(622, 210)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_SD_Produksi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Txt_NoPO As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents Txt_NmBarang As TextBox
    Friend WithEvents Cmb_Lokasi_Awal As ComboBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Cmb_Lokasi_Tujuan As ComboBox
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Button1 As Button
End Class
