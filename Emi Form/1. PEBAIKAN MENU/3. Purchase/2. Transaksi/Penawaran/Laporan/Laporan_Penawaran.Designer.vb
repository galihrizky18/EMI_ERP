<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Laporan_Penawaran
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Cmb_Periode = New System.Windows.Forms.ComboBox()
        Me.Cmb_StatusPenawaran = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Cmb_Release = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
        Me.Txt_NmSupplier = New System.Windows.Forms.TextBox()
        Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Txt_KdSupplier = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BtnCetak = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.Lv_Supplier = New System.Windows.Forms.ListView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Lv_Barang = New System.Windows.Forms.ListView()
        Me.Chk_Periode = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(219, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Laporan Penawaran"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(694, 51)
        Me.Panel1.TabIndex = 23
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
        Me.PanelGradient1.Size = New System.Drawing.Size(694, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Chk_Periode)
        Me.GroupBox1.Controls.Add(Me.Cmb_Periode)
        Me.GroupBox1.Controls.Add(Me.Cmb_StatusPenawaran)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Cmb_Release)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Txt_NmBarang)
        Me.GroupBox1.Controls.Add(Me.Txt_NmSupplier)
        Me.GroupBox1.Controls.Add(Me.Txt_KdBarang)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Txt_KdSupplier)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Tgl2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Tgl1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(21, 65)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(657, 184)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Cmb_Periode
        '
        Me.Cmb_Periode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Periode.FormattingEnabled = True
        Me.Cmb_Periode.Location = New System.Drawing.Point(163, 19)
        Me.Cmb_Periode.Name = "Cmb_Periode"
        Me.Cmb_Periode.Size = New System.Drawing.Size(111, 26)
        Me.Cmb_Periode.TabIndex = 1
        '
        'Cmb_StatusPenawaran
        '
        Me.Cmb_StatusPenawaran.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_StatusPenawaran.FormattingEnabled = True
        Me.Cmb_StatusPenawaran.Location = New System.Drawing.Point(139, 141)
        Me.Cmb_StatusPenawaran.Name = "Cmb_StatusPenawaran"
        Me.Cmb_StatusPenawaran.Size = New System.Drawing.Size(135, 26)
        Me.Cmb_StatusPenawaran.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 144)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(121, 18)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Status Penawaran"
        '
        'Cmb_Release
        '
        Me.Cmb_Release.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Release.FormattingEnabled = True
        Me.Cmb_Release.Location = New System.Drawing.Point(139, 109)
        Me.Cmb_Release.Name = "Cmb_Release"
        Me.Cmb_Release.Size = New System.Drawing.Size(135, 26)
        Me.Cmb_Release.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 18)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Release"
        '
        'Txt_NmBarang
        '
        Me.Txt_NmBarang.Location = New System.Drawing.Point(279, 80)
        Me.Txt_NmBarang.Name = "Txt_NmBarang"
        Me.Txt_NmBarang.Size = New System.Drawing.Size(365, 23)
        Me.Txt_NmBarang.TabIndex = 4
        '
        'Txt_NmSupplier
        '
        Me.Txt_NmSupplier.Location = New System.Drawing.Point(279, 51)
        Me.Txt_NmSupplier.Name = "Txt_NmSupplier"
        Me.Txt_NmSupplier.Size = New System.Drawing.Size(365, 23)
        Me.Txt_NmSupplier.TabIndex = 4
        '
        'Txt_KdBarang
        '
        Me.Txt_KdBarang.Location = New System.Drawing.Point(139, 80)
        Me.Txt_KdBarang.Name = "Txt_KdBarang"
        Me.Txt_KdBarang.Size = New System.Drawing.Size(135, 23)
        Me.Txt_KdBarang.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 81)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(50, 18)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Barang"
        '
        'Txt_KdSupplier
        '
        Me.Txt_KdSupplier.Location = New System.Drawing.Point(139, 51)
        Me.Txt_KdSupplier.Name = "Txt_KdSupplier"
        Me.Txt_KdSupplier.Size = New System.Drawing.Size(135, 23)
        Me.Txt_KdSupplier.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 52)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 18)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Supplier"
        '
        'Tgl2
        '
        Me.Tgl2.CustomFormat = "dd MMMM yyyy"
        Me.Tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl2.Location = New System.Drawing.Point(481, 20)
        Me.Tgl2.Name = "Tgl2"
        Me.Tgl2.Size = New System.Drawing.Size(163, 23)
        Me.Tgl2.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(448, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 18)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "s/d"
        '
        'Tgl1
        '
        Me.Tgl1.CustomFormat = "dd MMMM yyyy"
        Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl1.Location = New System.Drawing.Point(279, 20)
        Me.Tgl1.Name = "Tgl1"
        Me.Tgl1.Size = New System.Drawing.Size(163, 23)
        Me.Tgl1.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 18)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Periode"
        '
        'BtnCetak
        '
        Me.BtnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnCetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnCetak.ForeColor = System.Drawing.Color.White
        Me.BtnCetak.Location = New System.Drawing.Point(498, 255)
        Me.BtnCetak.Name = "BtnCetak"
        Me.BtnCetak.Size = New System.Drawing.Size(84, 33)
        Me.BtnCetak.TabIndex = 1
        Me.BtnCetak.Text = "&Cetak"
        Me.BtnCetak.UseVisualStyleBackColor = False
        '
        'BtnExit
        '
        Me.BtnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnExit.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnExit.ForeColor = System.Drawing.Color.White
        Me.BtnExit.Location = New System.Drawing.Point(584, 255)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(84, 33)
        Me.BtnExit.TabIndex = 6
        Me.BtnExit.Text = "&Keluar"
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'Lv_Supplier
        '
        Me.Lv_Supplier.FullRowSelect = True
        Me.Lv_Supplier.GridLines = True
        Me.Lv_Supplier.HideSelection = False
        Me.Lv_Supplier.Location = New System.Drawing.Point(700, 141)
        Me.Lv_Supplier.Name = "Lv_Supplier"
        Me.Lv_Supplier.Size = New System.Drawing.Size(505, 145)
        Me.Lv_Supplier.TabIndex = 354
        Me.Lv_Supplier.UseCompatibleStateImageBehavior = False
        Me.Lv_Supplier.View = System.Windows.Forms.View.Details
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(12, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(644, 12)
        Me.Panel2.TabIndex = 36
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 50)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 187)
        Me.Panel3.TabIndex = 355
        Me.Panel3.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(675, 84)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 178)
        Me.Panel4.TabIndex = 356
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(19, 287)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(644, 12)
        Me.Panel5.TabIndex = 357
        Me.Panel5.Visible = False
        '
        'Lv_Barang
        '
        Me.Lv_Barang.FullRowSelect = True
        Me.Lv_Barang.GridLines = True
        Me.Lv_Barang.HideSelection = False
        Me.Lv_Barang.Location = New System.Drawing.Point(700, 170)
        Me.Lv_Barang.Name = "Lv_Barang"
        Me.Lv_Barang.Size = New System.Drawing.Size(505, 145)
        Me.Lv_Barang.TabIndex = 354
        Me.Lv_Barang.UseCompatibleStateImageBehavior = False
        Me.Lv_Barang.View = System.Windows.Forms.View.Details
        '
        'Chk_Periode
        '
        Me.Chk_Periode.AutoSize = True
        Me.Chk_Periode.Location = New System.Drawing.Point(142, 26)
        Me.Chk_Periode.Name = "Chk_Periode"
        Me.Chk_Periode.Size = New System.Drawing.Size(15, 14)
        Me.Chk_Periode.TabIndex = 0
        Me.Chk_Periode.UseVisualStyleBackColor = True
        '
        'Laporan_Penawaran
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(694, 298)
        Me.Controls.Add(Me.Lv_Barang)
        Me.Controls.Add(Me.Lv_Supplier)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnCetak)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Laporan_Penawaran"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Txt_NmSupplier As TextBox
    Friend WithEvents Txt_KdSupplier As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Tgl2 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents BtnCetak As Button
    Friend WithEvents BtnExit As Button
    Friend WithEvents Lv_Supplier As ListView
    Friend WithEvents Cmb_Release As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Cmb_Periode As ComboBox
    Friend WithEvents Cmb_StatusPenawaran As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Txt_NmBarang As TextBox
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Lv_Barang As ListView
    Friend WithEvents Chk_Periode As CheckBox
End Class
