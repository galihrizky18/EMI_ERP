<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Laporan_Purchase_Order_Induk_Barang_Lain
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Cmb_Periode = New System.Windows.Forms.ComboBox()
        Me.Cmb_ParamLain = New System.Windows.Forms.ComboBox()
        Me.Cmb_Status = New System.Windows.Forms.ComboBox()
        Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
        Me.Txt_KdSupplier = New System.Windows.Forms.TextBox()
        Me.Txt_Faktur = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_NmSupplier = New System.Windows.Forms.TextBox()
        Me.Txt_ParamLain = New System.Windows.Forms.TextBox()
        Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
        Me.BtnCetak = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Lv_Faktur = New System.Windows.Forms.ListView()
        Me.Lv_Supplier = New System.Windows.Forms.ListView()
        Me.Lv_Barang = New System.Windows.Forms.ListView()
        Me.CmbJenis = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
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
        Me.Panel1.Size = New System.Drawing.Size(678, 51)
        Me.Panel1.TabIndex = 26
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
        Me.PanelGradient1.Size = New System.Drawing.Size(678, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(344, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Laporan - Purchase Order Asset"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(0, 52)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(942, 12)
        Me.Panel5.TabIndex = 41
        Me.Panel5.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 66)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(12, 601)
        Me.Panel3.TabIndex = 42
        Me.Panel3.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CmbJenis)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Cmb_Periode)
        Me.GroupBox1.Controls.Add(Me.Cmb_ParamLain)
        Me.GroupBox1.Controls.Add(Me.Cmb_Status)
        Me.GroupBox1.Controls.Add(Me.Txt_KdBarang)
        Me.GroupBox1.Controls.Add(Me.Txt_KdSupplier)
        Me.GroupBox1.Controls.Add(Me.Txt_Faktur)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Tgl2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Tgl1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Txt_NmSupplier)
        Me.GroupBox1.Controls.Add(Me.Txt_ParamLain)
        Me.GroupBox1.Controls.Add(Me.Txt_NmBarang)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 66)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(653, 209)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Cmb_Periode
        '
        Me.Cmb_Periode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Periode.FormattingEnabled = True
        Me.Cmb_Periode.Location = New System.Drawing.Point(112, 19)
        Me.Cmb_Periode.Name = "Cmb_Periode"
        Me.Cmb_Periode.Size = New System.Drawing.Size(151, 26)
        Me.Cmb_Periode.TabIndex = 0
        '
        'Cmb_ParamLain
        '
        Me.Cmb_ParamLain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_ParamLain.FormattingEnabled = True
        Me.Cmb_ParamLain.Location = New System.Drawing.Point(112, 170)
        Me.Cmb_ParamLain.Name = "Cmb_ParamLain"
        Me.Cmb_ParamLain.Size = New System.Drawing.Size(151, 26)
        Me.Cmb_ParamLain.TabIndex = 7
        '
        'Cmb_Status
        '
        Me.Cmb_Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Status.FormattingEnabled = True
        Me.Cmb_Status.Location = New System.Drawing.Point(112, 51)
        Me.Cmb_Status.Name = "Cmb_Status"
        Me.Cmb_Status.Size = New System.Drawing.Size(151, 26)
        Me.Cmb_Status.TabIndex = 3
        '
        'Txt_KdBarang
        '
        Me.Txt_KdBarang.Location = New System.Drawing.Point(112, 141)
        Me.Txt_KdBarang.Name = "Txt_KdBarang"
        Me.Txt_KdBarang.Size = New System.Drawing.Size(208, 23)
        Me.Txt_KdBarang.TabIndex = 6
        '
        'Txt_KdSupplier
        '
        Me.Txt_KdSupplier.Location = New System.Drawing.Point(112, 112)
        Me.Txt_KdSupplier.Name = "Txt_KdSupplier"
        Me.Txt_KdSupplier.Size = New System.Drawing.Size(208, 23)
        Me.Txt_KdSupplier.TabIndex = 5
        '
        'Txt_Faktur
        '
        Me.Txt_Faktur.Location = New System.Drawing.Point(112, 83)
        Me.Txt_Faktur.Name = "Txt_Faktur"
        Me.Txt_Faktur.Size = New System.Drawing.Size(208, 23)
        Me.Txt_Faktur.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 115)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 18)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Supplier"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 173)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 18)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Lainnya"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 144)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(50, 18)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Barang"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 54)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(48, 18)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Status"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 86)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 18)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "No Faktur"
        '
        'Tgl2
        '
        Me.Tgl2.CustomFormat = "dd MMMM yyyy"
        Me.Tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl2.Location = New System.Drawing.Point(480, 19)
        Me.Tgl2.Name = "Tgl2"
        Me.Tgl2.Size = New System.Drawing.Size(163, 23)
        Me.Tgl2.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(442, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 18)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "s/d"
        '
        'Tgl1
        '
        Me.Tgl1.CustomFormat = "dd MMMM yyyy"
        Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl1.Location = New System.Drawing.Point(269, 19)
        Me.Tgl1.Name = "Tgl1"
        Me.Tgl1.Size = New System.Drawing.Size(163, 23)
        Me.Tgl1.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 18)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Periode"
        '
        'Txt_NmSupplier
        '
        Me.Txt_NmSupplier.Location = New System.Drawing.Point(323, 109)
        Me.Txt_NmSupplier.Name = "Txt_NmSupplier"
        Me.Txt_NmSupplier.Size = New System.Drawing.Size(320, 23)
        Me.Txt_NmSupplier.TabIndex = 4
        '
        'Txt_ParamLain
        '
        Me.Txt_ParamLain.Enabled = False
        Me.Txt_ParamLain.Location = New System.Drawing.Point(269, 170)
        Me.Txt_ParamLain.Name = "Txt_ParamLain"
        Me.Txt_ParamLain.Size = New System.Drawing.Size(374, 23)
        Me.Txt_ParamLain.TabIndex = 6
        '
        'Txt_NmBarang
        '
        Me.Txt_NmBarang.Location = New System.Drawing.Point(323, 141)
        Me.Txt_NmBarang.Name = "Txt_NmBarang"
        Me.Txt_NmBarang.Size = New System.Drawing.Size(320, 23)
        Me.Txt_NmBarang.TabIndex = 6
        '
        'BtnCetak
        '
        Me.BtnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnCetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnCetak.ForeColor = System.Drawing.Color.White
        Me.BtnCetak.Location = New System.Drawing.Point(498, 281)
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
        Me.BtnExit.Location = New System.Drawing.Point(581, 281)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(84, 33)
        Me.BtnExit.TabIndex = 45
        Me.BtnExit.Text = "&Keluar"
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(666, 66)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(12, 601)
        Me.Panel2.TabIndex = 42
        Me.Panel2.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(12, 311)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(942, 12)
        Me.Panel4.TabIndex = 41
        Me.Panel4.Visible = False
        '
        'Lv_Faktur
        '
        Me.Lv_Faktur.BackColor = System.Drawing.Color.White
        Me.Lv_Faktur.FullRowSelect = True
        Me.Lv_Faktur.GridLines = True
        Me.Lv_Faktur.HideSelection = False
        Me.Lv_Faktur.Location = New System.Drawing.Point(700, 173)
        Me.Lv_Faktur.Name = "Lv_Faktur"
        Me.Lv_Faktur.Size = New System.Drawing.Size(531, 200)
        Me.Lv_Faktur.TabIndex = 46
        Me.Lv_Faktur.UseCompatibleStateImageBehavior = False
        Me.Lv_Faktur.View = System.Windows.Forms.View.Details
        Me.Lv_Faktur.Visible = False
        '
        'Lv_Supplier
        '
        Me.Lv_Supplier.BackColor = System.Drawing.Color.White
        Me.Lv_Supplier.FullRowSelect = True
        Me.Lv_Supplier.GridLines = True
        Me.Lv_Supplier.HideSelection = False
        Me.Lv_Supplier.Location = New System.Drawing.Point(700, 200)
        Me.Lv_Supplier.Name = "Lv_Supplier"
        Me.Lv_Supplier.Size = New System.Drawing.Size(531, 200)
        Me.Lv_Supplier.TabIndex = 46
        Me.Lv_Supplier.UseCompatibleStateImageBehavior = False
        Me.Lv_Supplier.View = System.Windows.Forms.View.Details
        Me.Lv_Supplier.Visible = False
        '
        'Lv_Barang
        '
        Me.Lv_Barang.BackColor = System.Drawing.Color.White
        Me.Lv_Barang.FullRowSelect = True
        Me.Lv_Barang.GridLines = True
        Me.Lv_Barang.HideSelection = False
        Me.Lv_Barang.Location = New System.Drawing.Point(700, 232)
        Me.Lv_Barang.Name = "Lv_Barang"
        Me.Lv_Barang.Size = New System.Drawing.Size(531, 200)
        Me.Lv_Barang.TabIndex = 46
        Me.Lv_Barang.UseCompatibleStateImageBehavior = False
        Me.Lv_Barang.View = System.Windows.Forms.View.Details
        Me.Lv_Barang.Visible = False
        '
        'CmbJenis
        '
        Me.CmbJenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbJenis.FormattingEnabled = True
        Me.CmbJenis.Location = New System.Drawing.Point(323, 51)
        Me.CmbJenis.Name = "CmbJenis"
        Me.CmbJenis.Size = New System.Drawing.Size(208, 26)
        Me.CmbJenis.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(272, 54)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 18)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Jenis"
        '
        'Laporan_Purchase_Order_Induk_Barang_Lain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(678, 324)
        Me.Controls.Add(Me.Lv_Barang)
        Me.Controls.Add(Me.Lv_Supplier)
        Me.Controls.Add(Me.Lv_Faktur)
        Me.Controls.Add(Me.BtnCetak)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Name = "Laporan_Purchase_Order_Induk_Barang_Lain"
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
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents Txt_KdSupplier As TextBox
    Friend WithEvents Txt_Faktur As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Tgl2 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_NmSupplier As TextBox
    Friend WithEvents Txt_NmBarang As TextBox
    Friend WithEvents BtnCetak As Button
    Friend WithEvents BtnExit As Button
    Friend WithEvents Cmb_Status As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Cmb_Periode As ComboBox
    Friend WithEvents Cmb_ParamLain As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Txt_ParamLain As TextBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Lv_Faktur As ListView
    Friend WithEvents Lv_Supplier As ListView
    Friend WithEvents Lv_Barang As ListView
    Friend WithEvents CmbJenis As ComboBox
    Friend WithEvents Label9 As Label
End Class
