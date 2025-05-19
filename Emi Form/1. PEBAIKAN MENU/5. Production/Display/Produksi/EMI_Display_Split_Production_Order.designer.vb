<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Display_Split_Production_Order
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Lv_SplitProdOrder = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CopyNoTransaksiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BatalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Lv_PRDetail = New System.Windows.Forms.ListView()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Cmb_ValueParamLain = New System.Windows.Forms.ComboBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
        Me.Cb_TransaksiHrIni = New System.Windows.Forms.CheckBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Cmb_ParamLain = New System.Windows.Forms.ComboBox()
        Me.Cb_ParamLain = New System.Windows.Forms.CheckBox()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Cb_ParamTgl = New System.Windows.Forms.CheckBox()
        Me.Cmb_ParamTgl = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1208, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1208, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(345, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Display - Split Production Order"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1206, 12)
        Me.Panel2.TabIndex = 34
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 63)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 673)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1186, 63)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 669)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(21, 553)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1436, 15)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'Lv_SplitProdOrder
        '
        Me.Lv_SplitProdOrder.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Lv_SplitProdOrder.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_SplitProdOrder.FullRowSelect = True
        Me.Lv_SplitProdOrder.GridLines = True
        Me.Lv_SplitProdOrder.HideSelection = False
        Me.Lv_SplitProdOrder.Location = New System.Drawing.Point(21, 64)
        Me.Lv_SplitProdOrder.Name = "Lv_SplitProdOrder"
        Me.Lv_SplitProdOrder.Size = New System.Drawing.Size(1163, 486)
        Me.Lv_SplitProdOrder.TabIndex = 234
        Me.Lv_SplitProdOrder.UseCompatibleStateImageBehavior = False
        Me.Lv_SplitProdOrder.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyNoTransaksiToolStripMenuItem, Me.BatalToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(181, 70)
        '
        'CopyNoTransaksiToolStripMenuItem
        '
        Me.CopyNoTransaksiToolStripMenuItem.Name = "CopyNoTransaksiToolStripMenuItem"
        Me.CopyNoTransaksiToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.CopyNoTransaksiToolStripMenuItem.Text = "Copy No Transaksi"
        '
        'BatalToolStripMenuItem
        '
        Me.BatalToolStripMenuItem.Name = "BatalToolStripMenuItem"
        Me.BatalToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.BatalToolStripMenuItem.Text = "Batal"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(20, 716)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1436, 15)
        Me.Panel6.TabIndex = 39
        Me.Panel6.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(1212, 322)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1436, 24)
        Me.Panel7.TabIndex = 39
        Me.Panel7.Visible = False
        '
        'Lv_PRDetail
        '
        Me.Lv_PRDetail.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_PRDetail.FullRowSelect = True
        Me.Lv_PRDetail.GridLines = True
        Me.Lv_PRDetail.HideSelection = False
        Me.Lv_PRDetail.Location = New System.Drawing.Point(1217, 347)
        Me.Lv_PRDetail.Name = "Lv_PRDetail"
        Me.Lv_PRDetail.Size = New System.Drawing.Size(900, 220)
        Me.Lv_PRDetail.TabIndex = 341
        Me.Lv_PRDetail.UseCompatibleStateImageBehavior = False
        Me.Lv_PRDetail.View = System.Windows.Forms.View.Details
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Cmb_ValueParamLain)
        Me.GroupBox3.Controls.Add(Me.Btn_Cari)
        Me.GroupBox3.Controls.Add(Me.Cmb_Lokasi)
        Me.GroupBox3.Controls.Add(Me.Cb_TransaksiHrIni)
        Me.GroupBox3.Controls.Add(Me.TextBox4)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Cmb_ParamLain)
        Me.GroupBox3.Controls.Add(Me.Cb_ParamLain)
        Me.GroupBox3.Controls.Add(Me.DateTimePicker2)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox3.Controls.Add(Me.Cb_ParamTgl)
        Me.GroupBox3.Controls.Add(Me.Cmb_ParamTgl)
        Me.GroupBox3.Location = New System.Drawing.Point(20, 571)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox3.Size = New System.Drawing.Size(640, 143)
        Me.GroupBox3.TabIndex = 342
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Filter Data"
        '
        'Cmb_ValueParamLain
        '
        Me.Cmb_ValueParamLain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_ValueParamLain.FormattingEnabled = True
        Me.Cmb_ValueParamLain.Location = New System.Drawing.Point(318, 105)
        Me.Cmb_ValueParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_ValueParamLain.Name = "Cmb_ValueParamLain"
        Me.Cmb_ValueParamLain.Size = New System.Drawing.Size(220, 24)
        Me.Cmb_ValueParamLain.TabIndex = 344
        Me.Cmb_ValueParamLain.Visible = False
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(543, 104)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(84, 27)
        Me.Btn_Cari.TabIndex = 343
        Me.Btn_Cari.Text = "&Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Cmb_Lokasi
        '
        Me.Cmb_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lokasi.Enabled = False
        Me.Cmb_Lokasi.FormattingEnabled = True
        Me.Cmb_Lokasi.Location = New System.Drawing.Point(6, 19)
        Me.Cmb_Lokasi.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
        Me.Cmb_Lokasi.Size = New System.Drawing.Size(209, 24)
        Me.Cmb_Lokasi.TabIndex = 342
        '
        'Cb_TransaksiHrIni
        '
        Me.Cb_TransaksiHrIni.AutoSize = True
        Me.Cb_TransaksiHrIni.Location = New System.Drawing.Point(6, 49)
        Me.Cb_TransaksiHrIni.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cb_TransaksiHrIni.Name = "Cb_TransaksiHrIni"
        Me.Cb_TransaksiHrIni.Size = New System.Drawing.Size(118, 20)
        Me.Cb_TransaksiHrIni.TabIndex = 9
        Me.Cb_TransaksiHrIni.Text = "Transaksi Hari Ini"
        Me.Cb_TransaksiHrIni.UseVisualStyleBackColor = True
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(318, 107)
        Me.TextBox4.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(220, 20)
        Me.TextBox4.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(279, 110)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 16)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Value"
        '
        'Cmb_ParamLain
        '
        Me.Cmb_ParamLain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_ParamLain.FormattingEnabled = True
        Me.Cmb_ParamLain.Location = New System.Drawing.Point(141, 105)
        Me.Cmb_ParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_ParamLain.Name = "Cmb_ParamLain"
        Me.Cmb_ParamLain.Size = New System.Drawing.Size(134, 24)
        Me.Cmb_ParamLain.TabIndex = 6
        '
        'Cb_ParamLain
        '
        Me.Cb_ParamLain.AutoSize = True
        Me.Cb_ParamLain.Location = New System.Drawing.Point(6, 107)
        Me.Cb_ParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cb_ParamLain.Name = "Cb_ParamLain"
        Me.Cb_ParamLain.Size = New System.Drawing.Size(107, 20)
        Me.Cb_ParamLain.TabIndex = 5
        Me.Cb_ParamLain.Text = "Parameter Lain"
        Me.Cb_ParamLain.UseVisualStyleBackColor = True
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(469, 77)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(158, 20)
        Me.DateTimePicker2.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(441, 78)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(25, 16)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "s/d"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(279, 77)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(158, 20)
        Me.DateTimePicker1.TabIndex = 3
        '
        'Cb_ParamTgl
        '
        Me.Cb_ParamTgl.AutoSize = True
        Me.Cb_ParamTgl.Location = New System.Drawing.Point(6, 77)
        Me.Cb_ParamTgl.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cb_ParamTgl.Name = "Cb_ParamTgl"
        Me.Cb_ParamTgl.Size = New System.Drawing.Size(124, 20)
        Me.Cb_ParamTgl.TabIndex = 1
        Me.Cb_ParamTgl.Text = "Parameter Tanggal"
        Me.Cb_ParamTgl.UseVisualStyleBackColor = True
        '
        'Cmb_ParamTgl
        '
        Me.Cmb_ParamTgl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_ParamTgl.FormattingEnabled = True
        Me.Cmb_ParamTgl.Location = New System.Drawing.Point(141, 75)
        Me.Cmb_ParamTgl.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_ParamTgl.Name = "Cmb_ParamTgl"
        Me.Cmb_ParamTgl.Size = New System.Drawing.Size(134, 24)
        Me.Cmb_ParamTgl.TabIndex = 2
        '
        'EMI_Display_Split_Production_Order
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1208, 732)
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Lv_PRDetail)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Lv_SplitProdOrder)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Display_Split_Production_Order"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Lv_SplitProdOrder As ListView
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Lv_PRDetail As ListView
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Cmb_Lokasi As ComboBox
    Friend WithEvents Cb_TransaksiHrIni As CheckBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Cmb_ParamLain As ComboBox
    Friend WithEvents Cb_ParamLain As CheckBox
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Cb_ParamTgl As CheckBox
    Friend WithEvents Cmb_ParamTgl As ComboBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Cmb_ValueParamLain As ComboBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents CopyNoTransaksiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BatalToolStripMenuItem As ToolStripMenuItem
End Class
