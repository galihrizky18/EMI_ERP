<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Emi_Laporan_Barang_Masuk
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.BtnCetak = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Lv_Supplier = New System.Windows.Forms.ListView()
        Me.Lv_Barang = New System.Windows.Forms.ListView()
        Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Txt_KdSupplier = New System.Windows.Forms.TextBox()
        Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
        Me.Txt_NmSupplier = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Txt_NoSJ = New System.Windows.Forms.TextBox()
        Me.Txt_ValueFilter = New System.Windows.Forms.TextBox()
        Me.Cmb_Filter_Mobil = New System.Windows.Forms.ComboBox()
        Me.Cmb_FlagValidasiAcc = New System.Windows.Forms.ComboBox()
        Me.Cmb_FlagValidasiWarehouse = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Cmb_FilterBy = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label_Status = New System.Windows.Forms.Label()
        Me.Txt_NmSJ = New System.Windows.Forms.TextBox()
        Me.Lv_SJ = New System.Windows.Forms.ListView()
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
        Me.Panel1.Size = New System.Drawing.Size(584, 51)
        Me.Panel1.TabIndex = 24
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
        Me.PanelGradient1.Size = New System.Drawing.Size(584, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(267, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Laporan - Barang Masuk"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(3, 50)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 38
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 70)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(12, 601)
        Me.Panel3.TabIndex = 39
        Me.Panel3.Visible = False
        '
        'BtnExit
        '
        Me.BtnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnExit.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnExit.ForeColor = System.Drawing.Color.White
        Me.BtnExit.Location = New System.Drawing.Point(483, 340)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(84, 33)
        Me.BtnExit.TabIndex = 41
        Me.BtnExit.Text = "&Keluar"
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'BtnCetak
        '
        Me.BtnCetak.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnCetak.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnCetak.ForeColor = System.Drawing.Color.White
        Me.BtnCetak.Location = New System.Drawing.Point(400, 340)
        Me.BtnCetak.Name = "BtnCetak"
        Me.BtnCetak.Size = New System.Drawing.Size(84, 33)
        Me.BtnCetak.TabIndex = 1
        Me.BtnCetak.Text = "&Cetak"
        Me.BtnCetak.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(574, 70)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(12, 601)
        Me.Panel4.TabIndex = 39
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(13, 370)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(942, 12)
        Me.Panel5.TabIndex = 38
        Me.Panel5.Visible = False
        '
        'Lv_Supplier
        '
        Me.Lv_Supplier.BackColor = System.Drawing.Color.White
        Me.Lv_Supplier.FullRowSelect = True
        Me.Lv_Supplier.GridLines = True
        Me.Lv_Supplier.HideSelection = False
        Me.Lv_Supplier.Location = New System.Drawing.Point(600, 170)
        Me.Lv_Supplier.Name = "Lv_Supplier"
        Me.Lv_Supplier.Size = New System.Drawing.Size(408, 200)
        Me.Lv_Supplier.TabIndex = 42
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
        Me.Lv_Barang.Location = New System.Drawing.Point(600, 200)
        Me.Lv_Barang.Name = "Lv_Barang"
        Me.Lv_Barang.Size = New System.Drawing.Size(408, 200)
        Me.Lv_Barang.TabIndex = 42
        Me.Lv_Barang.UseCompatibleStateImageBehavior = False
        Me.Lv_Barang.View = System.Windows.Forms.View.Details
        Me.Lv_Barang.Visible = False
        '
        'Txt_NmBarang
        '
        Me.Txt_NmBarang.Location = New System.Drawing.Point(308, 112)
        Me.Txt_NmBarang.Name = "Txt_NmBarang"
        Me.Txt_NmBarang.Size = New System.Drawing.Size(239, 23)
        Me.Txt_NmBarang.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 18)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Periode"
        '
        'Tgl1
        '
        Me.Tgl1.CustomFormat = "dd MMMM yyyy"
        Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl1.Location = New System.Drawing.Point(139, 22)
        Me.Tgl1.Name = "Tgl1"
        Me.Tgl1.Size = New System.Drawing.Size(163, 23)
        Me.Tgl1.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(313, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 18)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "s/d"
        '
        'Tgl2
        '
        Me.Tgl2.CustomFormat = "dd MMMM yyyy"
        Me.Tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tgl2.Location = New System.Drawing.Point(350, 22)
        Me.Tgl2.Name = "Tgl2"
        Me.Tgl2.Size = New System.Drawing.Size(163, 23)
        Me.Tgl2.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 18)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Filter By"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 86)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 18)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Supplier"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 115)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(50, 18)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Barang"
        '
        'Txt_KdSupplier
        '
        Me.Txt_KdSupplier.Location = New System.Drawing.Point(139, 83)
        Me.Txt_KdSupplier.Name = "Txt_KdSupplier"
        Me.Txt_KdSupplier.Size = New System.Drawing.Size(163, 23)
        Me.Txt_KdSupplier.TabIndex = 3
        '
        'Txt_KdBarang
        '
        Me.Txt_KdBarang.Location = New System.Drawing.Point(139, 112)
        Me.Txt_KdBarang.Name = "Txt_KdBarang"
        Me.Txt_KdBarang.Size = New System.Drawing.Size(163, 23)
        Me.Txt_KdBarang.TabIndex = 4
        '
        'Txt_NmSupplier
        '
        Me.Txt_NmSupplier.Location = New System.Drawing.Point(308, 83)
        Me.Txt_NmSupplier.Name = "Txt_NmSupplier"
        Me.Txt_NmSupplier.Size = New System.Drawing.Size(239, 23)
        Me.Txt_NmSupplier.TabIndex = 4
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Txt_NmSupplier)
        Me.GroupBox1.Controls.Add(Me.Txt_NoSJ)
        Me.GroupBox1.Controls.Add(Me.Txt_KdBarang)
        Me.GroupBox1.Controls.Add(Me.Txt_KdSupplier)
        Me.GroupBox1.Controls.Add(Me.Txt_ValueFilter)
        Me.GroupBox1.Controls.Add(Me.Cmb_Filter_Mobil)
        Me.GroupBox1.Controls.Add(Me.Cmb_FlagValidasiAcc)
        Me.GroupBox1.Controls.Add(Me.Cmb_FlagValidasiWarehouse)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Cmb_FilterBy)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label_Status)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Tgl2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Tgl1)
        Me.GroupBox1.Controls.Add(Me.Txt_NmSJ)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Txt_NmBarang)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 63)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(561, 270)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Txt_NoSJ
        '
        Me.Txt_NoSJ.Location = New System.Drawing.Point(139, 141)
        Me.Txt_NoSJ.Name = "Txt_NoSJ"
        Me.Txt_NoSJ.Size = New System.Drawing.Size(163, 23)
        Me.Txt_NoSJ.TabIndex = 5
        '
        'Txt_ValueFilter
        '
        Me.Txt_ValueFilter.Enabled = False
        Me.Txt_ValueFilter.Location = New System.Drawing.Point(308, 237)
        Me.Txt_ValueFilter.Name = "Txt_ValueFilter"
        Me.Txt_ValueFilter.Size = New System.Drawing.Size(239, 23)
        Me.Txt_ValueFilter.TabIndex = 8
        '
        'Cmb_Filter_Mobil
        '
        Me.Cmb_Filter_Mobil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Filter_Mobil.FormattingEnabled = True
        Me.Cmb_Filter_Mobil.Location = New System.Drawing.Point(139, 234)
        Me.Cmb_Filter_Mobil.Name = "Cmb_Filter_Mobil"
        Me.Cmb_Filter_Mobil.Size = New System.Drawing.Size(163, 26)
        Me.Cmb_Filter_Mobil.TabIndex = 6
        '
        'Cmb_FlagValidasiAcc
        '
        Me.Cmb_FlagValidasiAcc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_FlagValidasiAcc.FormattingEnabled = True
        Me.Cmb_FlagValidasiAcc.Location = New System.Drawing.Point(139, 202)
        Me.Cmb_FlagValidasiAcc.Name = "Cmb_FlagValidasiAcc"
        Me.Cmb_FlagValidasiAcc.Size = New System.Drawing.Size(163, 26)
        Me.Cmb_FlagValidasiAcc.TabIndex = 5
        '
        'Cmb_FlagValidasiWarehouse
        '
        Me.Cmb_FlagValidasiWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_FlagValidasiWarehouse.FormattingEnabled = True
        Me.Cmb_FlagValidasiWarehouse.Location = New System.Drawing.Point(139, 170)
        Me.Cmb_FlagValidasiWarehouse.Name = "Cmb_FlagValidasiWarehouse"
        Me.Cmb_FlagValidasiWarehouse.Size = New System.Drawing.Size(163, 26)
        Me.Cmb_FlagValidasiWarehouse.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 144)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 18)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "No SJ"
        '
        'Cmb_FilterBy
        '
        Me.Cmb_FilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_FilterBy.FormattingEnabled = True
        Me.Cmb_FilterBy.Location = New System.Drawing.Point(139, 51)
        Me.Cmb_FilterBy.Name = "Cmb_FilterBy"
        Me.Cmb_FilterBy.Size = New System.Drawing.Size(163, 26)
        Me.Cmb_FilterBy.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(7, 205)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(127, 18)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "Validasi Accounting"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(7, 237)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(41, 18)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Filter"
        '
        'Label_Status
        '
        Me.Label_Status.AutoSize = True
        Me.Label_Status.Location = New System.Drawing.Point(7, 173)
        Me.Label_Status.Name = "Label_Status"
        Me.Label_Status.Size = New System.Drawing.Size(128, 18)
        Me.Label_Status.TabIndex = 6
        Me.Label_Status.Text = "Validasi Warehouse"
        '
        'Txt_NmSJ
        '
        Me.Txt_NmSJ.Location = New System.Drawing.Point(308, 141)
        Me.Txt_NmSJ.Name = "Txt_NmSJ"
        Me.Txt_NmSJ.Size = New System.Drawing.Size(239, 23)
        Me.Txt_NmSJ.TabIndex = 6
        '
        'Lv_SJ
        '
        Me.Lv_SJ.BackColor = System.Drawing.Color.White
        Me.Lv_SJ.FullRowSelect = True
        Me.Lv_SJ.GridLines = True
        Me.Lv_SJ.HideSelection = False
        Me.Lv_SJ.Location = New System.Drawing.Point(600, 230)
        Me.Lv_SJ.Name = "Lv_SJ"
        Me.Lv_SJ.Size = New System.Drawing.Size(408, 200)
        Me.Lv_SJ.TabIndex = 42
        Me.Lv_SJ.UseCompatibleStateImageBehavior = False
        Me.Lv_SJ.View = System.Windows.Forms.View.Details
        Me.Lv_SJ.Visible = False
        '
        'Emi_Laporan_Barang_Masuk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(584, 381)
        Me.Controls.Add(Me.Lv_SJ)
        Me.Controls.Add(Me.Lv_Barang)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Lv_Supplier)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.BtnCetak)
        Me.Controls.Add(Me.BtnExit)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Emi_Laporan_Barang_Masuk"
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
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents BtnExit As Button
    Friend WithEvents BtnCetak As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Lv_Supplier As ListView
    Friend WithEvents Lv_Barang As ListView
    Friend WithEvents Txt_NmBarang As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Tgl2 As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Txt_KdSupplier As TextBox
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents Txt_NmSupplier As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Txt_ValueFilter As TextBox
    Friend WithEvents Cmb_Filter_Mobil As ComboBox
    Friend WithEvents Cmb_FlagValidasiWarehouse As ComboBox
    Friend WithEvents Cmb_FilterBy As ComboBox
    Friend WithEvents Label_Status As Label
    Friend WithEvents Txt_NoSJ As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Txt_NmSJ As TextBox
    Friend WithEvents Lv_SJ As ListView
    Friend WithEvents Cmb_FlagValidasiAcc As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
End Class
