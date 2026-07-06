<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Master_Kategori_Gudang_Barang_Lain
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
		Me.Btn_Simpan = New System.Windows.Forms.Button()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Label18 = New System.Windows.Forms.Label()
		Me.Txt_Kd_Kategori = New System.Windows.Forms.TextBox()
		Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Cmb_Gudang = New System.Windows.Forms.ComboBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.Btn_Refresh = New System.Windows.Forms.Button()
		Me.Btn_Delete = New System.Windows.Forms.Button()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.Lv_Data = New System.Windows.Forms.ListView()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Cmb_Jenis_Gudang = New System.Windows.Forms.ComboBox()
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
		Me.Panel1.Size = New System.Drawing.Size(784, 45)
		Me.Panel1.TabIndex = 23
		'
		'PanelGradient1
		'
		Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
		Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
		Me.PanelGradient1.cuteTransparent1 = 100
		Me.PanelGradient1.cuteTransparent2 = 64
		Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.PanelGradient1.Location = New System.Drawing.Point(0, 43)
		Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
		Me.PanelGradient1.Name = "PanelGradient1"
		Me.PanelGradient1.Size = New System.Drawing.Size(784, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold)
		Me.Label1.Location = New System.Drawing.Point(20, 7)
		Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(385, 29)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Master - Kategori Gudang Barang Lain"
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Location = New System.Drawing.Point(0, 55)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 687)
		Me.Panel3.TabIndex = 36
		Me.Panel3.Visible = False
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(1, 44)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(1208, 12)
		Me.Panel2.TabIndex = 37
		Me.Panel2.Visible = False
		'
		'Btn_Simpan
		'
		Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Simpan.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
		Me.Btn_Simpan.Location = New System.Drawing.Point(173, 185)
		Me.Btn_Simpan.Name = "Btn_Simpan"
		Me.Btn_Simpan.Size = New System.Drawing.Size(116, 33)
		Me.Btn_Simpan.TabIndex = 3
		Me.Btn_Simpan.Tag = "SIMPAN"
		Me.Btn_Simpan.Text = "&Simpan"
		Me.Btn_Simpan.UseVisualStyleBackColor = False
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Location = New System.Drawing.Point(765, 64)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(19, 687)
		Me.Panel4.TabIndex = 36
		Me.Panel4.Visible = False
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Location = New System.Drawing.Point(21, 596)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(1208, 15)
		Me.Panel5.TabIndex = 37
		Me.Panel5.Visible = False
		'
		'Label18
		'
		Me.Label18.AutoSize = True
		Me.Label18.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label18.Location = New System.Drawing.Point(26, 60)
		Me.Label18.Name = "Label18"
		Me.Label18.Size = New System.Drawing.Size(137, 17)
		Me.Label18.TabIndex = 409
		Me.Label18.Text = "Kode Kategori Gudang"
		'
		'Txt_Kd_Kategori
		'
		Me.Txt_Kd_Kategori.BackColor = System.Drawing.Color.White
		Me.Txt_Kd_Kategori.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Kd_Kategori.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Txt_Kd_Kategori.Location = New System.Drawing.Point(172, 59)
		Me.Txt_Kd_Kategori.MaxLength = 50
		Me.Txt_Kd_Kategori.Name = "Txt_Kd_Kategori"
		Me.Txt_Kd_Kategori.Size = New System.Drawing.Size(398, 22)
		Me.Txt_Kd_Kategori.TabIndex = 0
		'
		'Txt_Keterangan
		'
		Me.Txt_Keterangan.BackColor = System.Drawing.Color.White
		Me.Txt_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Keterangan.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Txt_Keterangan.Location = New System.Drawing.Point(172, 118)
		Me.Txt_Keterangan.MaxLength = 255
		Me.Txt_Keterangan.Name = "Txt_Keterangan"
		Me.Txt_Keterangan.Size = New System.Drawing.Size(398, 22)
		Me.Txt_Keterangan.TabIndex = 2
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label2.Location = New System.Drawing.Point(26, 119)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(73, 17)
		Me.Label2.TabIndex = 409
		Me.Label2.Text = "Keterangan"
		'
		'Cmb_Gudang
		'
		Me.Cmb_Gudang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Gudang.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Cmb_Gudang.FormattingEnabled = True
		Me.Cmb_Gudang.Location = New System.Drawing.Point(172, 87)
		Me.Cmb_Gudang.Name = "Cmb_Gudang"
		Me.Cmb_Gudang.Size = New System.Drawing.Size(261, 25)
		Me.Cmb_Gudang.TabIndex = 1
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label3.Location = New System.Drawing.Point(26, 89)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(52, 17)
		Me.Label3.TabIndex = 409
		Me.Label3.Text = "Gudang"
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.Color.Red
		Me.Panel6.Location = New System.Drawing.Point(26, 171)
		Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(1208, 12)
		Me.Panel6.TabIndex = 37
		Me.Panel6.Visible = False
		'
		'Btn_Refresh
		'
		Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Refresh.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
		Me.Btn_Refresh.Location = New System.Drawing.Point(292, 185)
		Me.Btn_Refresh.Name = "Btn_Refresh"
		Me.Btn_Refresh.Size = New System.Drawing.Size(116, 33)
		Me.Btn_Refresh.TabIndex = 4
		Me.Btn_Refresh.Text = "&Refresh"
		Me.Btn_Refresh.UseVisualStyleBackColor = False
		'
		'Btn_Delete
		'
		Me.Btn_Delete.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Delete.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Delete.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Delete.ForeColor = System.Drawing.Color.White
		Me.Btn_Delete.Location = New System.Drawing.Point(412, 185)
		Me.Btn_Delete.Name = "Btn_Delete"
		Me.Btn_Delete.Size = New System.Drawing.Size(116, 33)
		Me.Btn_Delete.TabIndex = 6
		Me.Btn_Delete.Text = "&Delete"
		Me.Btn_Delete.UseVisualStyleBackColor = False
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.Color.Red
		Me.Panel7.Location = New System.Drawing.Point(20, 218)
		Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(1208, 12)
		Me.Panel7.TabIndex = 37
		Me.Panel7.Visible = False
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.Lv_Data)
		Me.GroupBox1.Location = New System.Drawing.Point(20, 247)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(743, 348)
		Me.GroupBox1.TabIndex = 5
		Me.GroupBox1.TabStop = False
		'
		'Lv_Data
		'
		Me.Lv_Data.FullRowSelect = True
		Me.Lv_Data.GridLines = True
		Me.Lv_Data.HideSelection = False
		Me.Lv_Data.Location = New System.Drawing.Point(0, -14)
		Me.Lv_Data.Name = "Lv_Data"
		Me.Lv_Data.Size = New System.Drawing.Size(744, 363)
		Me.Lv_Data.TabIndex = 0
		Me.Lv_Data.UseCompatibleStateImageBehavior = False
		Me.Lv_Data.View = System.Windows.Forms.View.Details
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label4.Location = New System.Drawing.Point(27, 148)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(86, 17)
		Me.Label4.TabIndex = 409
		Me.Label4.Text = "Jenis Gudang"
		'
		'Cmb_Jenis_Gudang
		'
		Me.Cmb_Jenis_Gudang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Jenis_Gudang.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Cmb_Jenis_Gudang.FormattingEnabled = True
		Me.Cmb_Jenis_Gudang.Location = New System.Drawing.Point(173, 146)
		Me.Cmb_Jenis_Gudang.Name = "Cmb_Jenis_Gudang"
		Me.Cmb_Jenis_Gudang.Size = New System.Drawing.Size(261, 25)
		Me.Cmb_Jenis_Gudang.TabIndex = 1
		'
		'N_EMI_Master_Kategori_Gudang_Barang_Lain
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(784, 611)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.Cmb_Jenis_Gudang)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.Cmb_Gudang)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label18)
		Me.Controls.Add(Me.Txt_Keterangan)
		Me.Controls.Add(Me.Txt_Kd_Kategori)
		Me.Controls.Add(Me.Btn_Delete)
		Me.Controls.Add(Me.Btn_Refresh)
		Me.Controls.Add(Me.Btn_Simpan)
		Me.Controls.Add(Me.Panel5)
		Me.Controls.Add(Me.Panel7)
		Me.Controls.Add(Me.Panel6)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel1)
		Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.Name = "N_EMI_Master_Kategori_Gudang_Barang_Lain"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.GroupBox1.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label18 As Label
    Friend WithEvents Txt_Kd_Kategori As TextBox
    Friend WithEvents Txt_Keterangan As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Cmb_Gudang As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Delete As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Lv_Data As ListView
    Friend WithEvents Label4 As Label
    Friend WithEvents Cmb_Jenis_Gudang As ComboBox
End Class
