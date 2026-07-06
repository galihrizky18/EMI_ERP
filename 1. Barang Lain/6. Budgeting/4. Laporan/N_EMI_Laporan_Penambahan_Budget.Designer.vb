<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_Laporan_Penambahan_Budget
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
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Txt_Sub = New System.Windows.Forms.TextBox()
        Me.Txt_Jenis = New System.Windows.Forms.TextBox()
        Me.Cmb_Binding = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
        Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.BtnCetak = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Cmb_Tgl = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Txt_lain = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
        Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
        Me.Lv_Jenis = New System.Windows.Forms.ListView()
        Me.Lv_Sub = New System.Windows.Forms.ListView()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Lv_Barang = New System.Windows.Forms.ListView()
        Me.Lv_lain = New System.Windows.Forms.ListView()
        Me.Cmb_Lain = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(29, 139)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(92, 17)
        Me.Label6.TabIndex = 162
        Me.Label6.Text = "Sub Kategori 1"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(29, 113)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 17)
        Me.Label5.TabIndex = 161
        Me.Label5.Text = "Kategori Jenis"
        '
        'Txt_Sub
        '
        Me.Txt_Sub.Location = New System.Drawing.Point(157, 136)
        Me.Txt_Sub.Name = "Txt_Sub"
        Me.Txt_Sub.Size = New System.Drawing.Size(456, 20)
        Me.Txt_Sub.TabIndex = 160
        '
        'Txt_Jenis
        '
        Me.Txt_Jenis.Location = New System.Drawing.Point(157, 110)
        Me.Txt_Jenis.Name = "Txt_Jenis"
        Me.Txt_Jenis.Size = New System.Drawing.Size(456, 20)
        Me.Txt_Jenis.TabIndex = 159
        '
        'Cmb_Binding
        '
        Me.Cmb_Binding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Binding.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Cmb_Binding.FormattingEnabled = True
        Me.Cmb_Binding.Location = New System.Drawing.Point(157, 81)
        Me.Cmb_Binding.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmb_Binding.Name = "Cmb_Binding"
        Me.Cmb_Binding.Size = New System.Drawing.Size(456, 24)
        Me.Cmb_Binding.TabIndex = 158
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(29, 83)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 17)
        Me.Label1.TabIndex = 157
        Me.Label1.Text = "Department"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(29, 59)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 17)
        Me.Label2.TabIndex = 156
        Me.Label2.Text = "Periode"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(432, 58)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 16)
        Me.Label3.TabIndex = 155
        Me.Label3.Text = "s/d"
        '
        'Tgl2
        '
        Me.Tgl2.CustomFormat = "dd MMMM yyyy"
        Me.Tgl2.Enabled = False
        Me.Tgl2.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl2.Location = New System.Drawing.Point(461, 55)
        Me.Tgl2.Margin = New System.Windows.Forms.Padding(2)
        Me.Tgl2.Name = "Tgl2"
        Me.Tgl2.Size = New System.Drawing.Size(152, 22)
        Me.Tgl2.TabIndex = 154
        '
        'Tgl1
        '
        Me.Tgl1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Tgl1.CustomFormat = "dd MMMM yyyy"
        Me.Tgl1.Enabled = False
        Me.Tgl1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl1.Location = New System.Drawing.Point(274, 55)
        Me.Tgl1.Margin = New System.Windows.Forms.Padding(2)
        Me.Tgl1.Name = "Tgl1"
        Me.Tgl1.Size = New System.Drawing.Size(154, 22)
        Me.Tgl1.TabIndex = 147
        '
        'BtnExit
        '
        Me.BtnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnExit.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnExit.ForeColor = System.Drawing.Color.White
        Me.BtnExit.Location = New System.Drawing.Point(483, 220)
        Me.BtnExit.Margin = New System.Windows.Forms.Padding(2)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(130, 30)
        Me.BtnExit.TabIndex = 153
        Me.BtnExit.Text = "&Keluar"
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'BtnCetak
        '
        Me.BtnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnCetak.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnCetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCetak.ForeColor = System.Drawing.Color.White
        Me.BtnCetak.Location = New System.Drawing.Point(336, 219)
        Me.BtnCetak.Margin = New System.Windows.Forms.Padding(2)
        Me.BtnCetak.Name = "BtnCetak"
        Me.BtnCetak.Size = New System.Drawing.Size(130, 30)
        Me.BtnCetak.TabIndex = 152
        Me.BtnCetak.Text = "&Cetak"
        Me.BtnCetak.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(20, 255)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(706, 12)
        Me.Panel4.TabIndex = 151
        Me.Panel4.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(627, 60)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(14, 488)
        Me.Panel2.TabIndex = 150
        Me.Panel2.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(0, 36)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(706, 10)
        Me.Panel5.TabIndex = 149
        Me.Panel5.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(641, 37)
        Me.Panel1.TabIndex = 148
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 35)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(641, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(15, 6)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(300, 29)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Laporan Penambahan Budget"
        '
        'Cmb_Tgl
        '
        Me.Cmb_Tgl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Tgl.FormattingEnabled = True
        Me.Cmb_Tgl.Location = New System.Drawing.Point(157, 52)
        Me.Cmb_Tgl.Name = "Cmb_Tgl"
        Me.Cmb_Tgl.Size = New System.Drawing.Size(112, 24)
        Me.Cmb_Tgl.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(29, 194)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 17)
        Me.Label4.TabIndex = 169
        Me.Label4.Text = "Lainnya"
        '
        'Txt_lain
        '
        Me.Txt_lain.Enabled = False
        Me.Txt_lain.Location = New System.Drawing.Point(390, 194)
        Me.Txt_lain.Name = "Txt_lain"
        Me.Txt_lain.Size = New System.Drawing.Size(223, 20)
        Me.Txt_lain.TabIndex = 170
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(29, 162)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(48, 17)
        Me.Label7.TabIndex = 171
        Me.Label7.Text = "Barang"
        '
        'Txt_KdBarang
        '
        Me.Txt_KdBarang.Location = New System.Drawing.Point(157, 162)
        Me.Txt_KdBarang.Name = "Txt_KdBarang"
        Me.Txt_KdBarang.Size = New System.Drawing.Size(223, 20)
        Me.Txt_KdBarang.TabIndex = 172
        '
        'Txt_NmBarang
        '
        Me.Txt_NmBarang.Location = New System.Drawing.Point(390, 162)
        Me.Txt_NmBarang.Name = "Txt_NmBarang"
        Me.Txt_NmBarang.Size = New System.Drawing.Size(223, 20)
        Me.Txt_NmBarang.TabIndex = 173
        '
        'Lv_Jenis
        '
        Me.Lv_Jenis.HideSelection = False
        Me.Lv_Jenis.Location = New System.Drawing.Point(157, 303)
        Me.Lv_Jenis.Name = "Lv_Jenis"
        Me.Lv_Jenis.Size = New System.Drawing.Size(456, 106)
        Me.Lv_Jenis.TabIndex = 174
        Me.Lv_Jenis.UseCompatibleStateImageBehavior = False
        Me.Lv_Jenis.Visible = False
        '
        'Lv_Sub
        '
        Me.Lv_Sub.HideSelection = False
        Me.Lv_Sub.Location = New System.Drawing.Point(110, 360)
        Me.Lv_Sub.Name = "Lv_Sub"
        Me.Lv_Sub.Size = New System.Drawing.Size(456, 106)
        Me.Lv_Sub.TabIndex = 175
        Me.Lv_Sub.UseCompatibleStateImageBehavior = False
        Me.Lv_Sub.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 60)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(14, 488)
        Me.Panel3.TabIndex = 151
        Me.Panel3.Visible = False
        '
        'Lv_Barang
        '
        Me.Lv_Barang.HideSelection = False
        Me.Lv_Barang.Location = New System.Drawing.Point(68, 388)
        Me.Lv_Barang.Name = "Lv_Barang"
        Me.Lv_Barang.Size = New System.Drawing.Size(456, 106)
        Me.Lv_Barang.TabIndex = 176
        Me.Lv_Barang.UseCompatibleStateImageBehavior = False
        Me.Lv_Barang.Visible = False
        '
        'Lv_lain
        '
        Me.Lv_lain.HideSelection = False
        Me.Lv_lain.Location = New System.Drawing.Point(49, 415)
        Me.Lv_lain.Name = "Lv_lain"
        Me.Lv_lain.Size = New System.Drawing.Size(456, 106)
        Me.Lv_lain.TabIndex = 177
        Me.Lv_lain.UseCompatibleStateImageBehavior = False
        Me.Lv_lain.Visible = False
        '
        'Cmb_Lain
        '
        Me.Cmb_Lain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lain.FormattingEnabled = True
        Me.Cmb_Lain.Location = New System.Drawing.Point(157, 192)
        Me.Cmb_Lain.Name = "Cmb_Lain"
        Me.Cmb_Lain.Size = New System.Drawing.Size(223, 24)
        Me.Cmb_Lain.TabIndex = 178
        '
        'N_EMI_Laporan_Penambahan_Budget
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(641, 265)
        Me.Controls.Add(Me.Lv_lain)
        Me.Controls.Add(Me.Lv_Barang)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Lv_Sub)
        Me.Controls.Add(Me.Lv_Jenis)
        Me.Controls.Add(Me.Cmb_Lain)
        Me.Controls.Add(Me.Txt_NmBarang)
        Me.Controls.Add(Me.Txt_KdBarang)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Txt_lain)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Cmb_Tgl)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Txt_Sub)
        Me.Controls.Add(Me.Txt_Jenis)
        Me.Controls.Add(Me.Cmb_Binding)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Tgl2)
        Me.Controls.Add(Me.Tgl1)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnCetak)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "N_EMI_Laporan_Penambahan_Budget"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Txt_Sub As TextBox
    Friend WithEvents Txt_Jenis As TextBox
    Friend WithEvents Cmb_Binding As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Tgl2 As DateTimePicker
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents BtnExit As Button
    Friend WithEvents BtnCetak As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label11 As Label
    Friend WithEvents Cmb_Tgl As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Txt_lain As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents Txt_NmBarang As TextBox
    Friend WithEvents Lv_Jenis As ListView
    Friend WithEvents Lv_Sub As ListView
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Lv_Barang As ListView
    Friend WithEvents Lv_lain As ListView
    Friend WithEvents Cmb_Lain As ComboBox
End Class
