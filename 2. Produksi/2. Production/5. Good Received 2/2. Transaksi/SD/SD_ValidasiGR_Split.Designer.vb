<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SD_ValidasiGR_Split
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
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Cmb_Lain = New System.Windows.Forms.ComboBox()
		Me.Tgl2 = New System.Windows.Forms.DateTimePicker()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Tgl1 = New System.Windows.Forms.DateTimePicker()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Cmb_Periode = New System.Windows.Forms.ComboBox()
		Me.Txt_ValueLain = New System.Windows.Forms.TextBox()
		Me.Btn_Cari = New System.Windows.Forms.Button()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.Panel9 = New System.Windows.Forms.Panel()
		Me.Lv_Data = New System.Windows.Forms.ListView()
		Me.Btn_Tambah = New System.Windows.Forms.Button()
		Me.Panel10 = New System.Windows.Forms.Panel()
		Me.Btn_Close = New System.Windows.Forms.Button()
		Me.Label15 = New System.Windows.Forms.Label()
		Me.Panel13 = New System.Windows.Forms.Panel()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Panel8 = New System.Windows.Forms.Panel()
		Me.TabControl1 = New System.Windows.Forms.TabControl()
		Me.TabPage1 = New System.Windows.Forms.TabPage()
		Me.TabPage2 = New System.Windows.Forms.TabPage()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.Panel11 = New System.Windows.Forms.Panel()
		Me.Panel12 = New System.Windows.Forms.Panel()
		Me.Lv_Packing_Pallet = New System.Windows.Forms.ListView()
		Me.TabPage3 = New System.Windows.Forms.TabPage()
		Me.Lv_Packing_Waste = New System.Windows.Forms.ListView()
		Me.Panel1.SuspendLayout()
		Me.Panel3.SuspendLayout()
		Me.Panel4.SuspendLayout()
		Me.TabControl1.SuspendLayout()
		Me.TabPage1.SuspendLayout()
		Me.TabPage2.SuspendLayout()
		Me.TabPage3.SuspendLayout()
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
		Me.Panel1.Size = New System.Drawing.Size(998, 45)
		Me.Panel1.TabIndex = 25
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
		Me.PanelGradient1.Size = New System.Drawing.Size(998, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(20, 10)
		Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(511, 25)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Validasi Penerimaan Barang - Production Order"
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(0, 45)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(1113, 12)
		Me.Panel2.TabIndex = 37
		Me.Panel2.Visible = False
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Controls.Add(Me.Panel6)
		Me.Panel3.Location = New System.Drawing.Point(0, 64)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 601)
		Me.Panel3.TabIndex = 38
		Me.Panel3.Visible = False
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.Color.Red
		Me.Panel6.Location = New System.Drawing.Point(20, 485)
		Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(942, 12)
		Me.Panel6.TabIndex = 35
		Me.Panel6.Visible = False
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Controls.Add(Me.Panel5)
		Me.Panel4.Location = New System.Drawing.Point(979, 67)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(19, 601)
		Me.Panel4.TabIndex = 38
		Me.Panel4.Visible = False
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Location = New System.Drawing.Point(20, 485)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(942, 12)
		Me.Panel5.TabIndex = 35
		Me.Panel5.Visible = False
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
		Me.Label3.Location = New System.Drawing.Point(19, 89)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(71, 15)
		Me.Label3.TabIndex = 41
		Me.Label3.Text = "Param Lain"
		'
		'Cmb_Lain
		'
		Me.Cmb_Lain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Lain.FormattingEnabled = True
		Me.Cmb_Lain.Location = New System.Drawing.Point(104, 88)
		Me.Cmb_Lain.Name = "Cmb_Lain"
		Me.Cmb_Lain.Size = New System.Drawing.Size(113, 21)
		Me.Cmb_Lain.TabIndex = 1
		'
		'Tgl2
		'
		Me.Tgl2.CustomFormat = "dd MMMM yyyy"
		Me.Tgl2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
		Me.Tgl2.Location = New System.Drawing.Point(425, 62)
		Me.Tgl2.Name = "Tgl2"
		Me.Tgl2.Size = New System.Drawing.Size(163, 20)
		Me.Tgl2.TabIndex = 45
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
		Me.Label4.Location = New System.Drawing.Point(392, 64)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(23, 15)
		Me.Label4.TabIndex = 46
		Me.Label4.Text = "s/d"
		'
		'Tgl1
		'
		Me.Tgl1.CustomFormat = "dd MMMM yyyy"
		Me.Tgl1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
		Me.Tgl1.Location = New System.Drawing.Point(223, 62)
		Me.Tgl1.Name = "Tgl1"
		Me.Tgl1.Size = New System.Drawing.Size(163, 20)
		Me.Tgl1.TabIndex = 44
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
		Me.Label5.Location = New System.Drawing.Point(19, 60)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(50, 15)
		Me.Label5.TabIndex = 43
		Me.Label5.Text = "Periode"
		'
		'Cmb_Periode
		'
		Me.Cmb_Periode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Periode.FormattingEnabled = True
		Me.Cmb_Periode.Location = New System.Drawing.Point(104, 60)
		Me.Cmb_Periode.Name = "Cmb_Periode"
		Me.Cmb_Periode.Size = New System.Drawing.Size(113, 21)
		Me.Cmb_Periode.TabIndex = 0
		'
		'Txt_ValueLain
		'
		Me.Txt_ValueLain.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_ValueLain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_ValueLain.Enabled = False
		Me.Txt_ValueLain.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
		Me.Txt_ValueLain.Location = New System.Drawing.Point(223, 87)
		Me.Txt_ValueLain.MaxLength = 50
		Me.Txt_ValueLain.Name = "Txt_ValueLain"
		Me.Txt_ValueLain.Size = New System.Drawing.Size(300, 21)
		Me.Txt_ValueLain.TabIndex = 2
		'
		'Btn_Cari
		'
		Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Cari.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Cari.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Btn_Cari.ForeColor = System.Drawing.Color.White
		Me.Btn_Cari.Location = New System.Drawing.Point(524, 82)
		Me.Btn_Cari.Name = "Btn_Cari"
		Me.Btn_Cari.Size = New System.Drawing.Size(68, 30)
		Me.Btn_Cari.TabIndex = 3
		Me.Btn_Cari.Text = "&Cari"
		Me.Btn_Cari.UseVisualStyleBackColor = False
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.Color.Red
		Me.Panel7.Location = New System.Drawing.Point(22, 109)
		Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(1113, 12)
		Me.Panel7.TabIndex = 37
		Me.Panel7.Visible = False
		'
		'Panel9
		'
		Me.Panel9.BackColor = System.Drawing.Color.Red
		Me.Panel9.Location = New System.Drawing.Point(19, 466)
		Me.Panel9.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel9.Name = "Panel9"
		Me.Panel9.Size = New System.Drawing.Size(1113, 12)
		Me.Panel9.TabIndex = 37
		Me.Panel9.Visible = False
		'
		'Lv_Data
		'
		Me.Lv_Data.CheckBoxes = True
		Me.Lv_Data.FullRowSelect = True
		Me.Lv_Data.GridLines = True
		Me.Lv_Data.HideSelection = False
		Me.Lv_Data.Location = New System.Drawing.Point(3, 22)
		Me.Lv_Data.Name = "Lv_Data"
		Me.Lv_Data.Size = New System.Drawing.Size(944, 292)
		Me.Lv_Data.TabIndex = 4
		Me.Lv_Data.UseCompatibleStateImageBehavior = False
		Me.Lv_Data.View = System.Windows.Forms.View.Details
		'
		'Btn_Tambah
		'
		Me.Btn_Tambah.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Tambah.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Tambah.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Tambah.ForeColor = System.Drawing.Color.White
		Me.Btn_Tambah.Location = New System.Drawing.Point(19, 481)
		Me.Btn_Tambah.Name = "Btn_Tambah"
		Me.Btn_Tambah.Size = New System.Drawing.Size(110, 32)
		Me.Btn_Tambah.TabIndex = 5
		Me.Btn_Tambah.Text = "&Tambah"
		Me.Btn_Tambah.UseVisualStyleBackColor = False
		'
		'Panel10
		'
		Me.Panel10.BackColor = System.Drawing.Color.Red
		Me.Panel10.Location = New System.Drawing.Point(22, 513)
		Me.Panel10.Margin = New System.Windows.Forms.Padding(4)
		Me.Panel10.Name = "Panel10"
		Me.Panel10.Size = New System.Drawing.Size(1113, 15)
		Me.Panel10.TabIndex = 37
		Me.Panel10.Visible = False
		'
		'Btn_Close
		'
		Me.Btn_Close.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Close.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Close.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Close.ForeColor = System.Drawing.Color.White
		Me.Btn_Close.Location = New System.Drawing.Point(132, 481)
		Me.Btn_Close.Name = "Btn_Close"
		Me.Btn_Close.Size = New System.Drawing.Size(85, 32)
		Me.Btn_Close.TabIndex = 5
		Me.Btn_Close.Text = "Close"
		Me.Btn_Close.UseVisualStyleBackColor = False
		'
		'Label15
		'
		Me.Label15.AutoSize = True
		Me.Label15.Location = New System.Drawing.Point(839, 3)
		Me.Label15.Name = "Label15"
		Me.Label15.Size = New System.Drawing.Size(105, 13)
		Me.Label15.TabIndex = 492
		Me.Label15.Text = "Ready For Validation"
		'
		'Panel13
		'
		Me.Panel13.BackColor = System.Drawing.Color.LightBlue
		Me.Panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Panel13.Location = New System.Drawing.Point(823, 4)
		Me.Panel13.Name = "Panel13"
		Me.Panel13.Size = New System.Drawing.Size(12, 12)
		Me.Panel13.TabIndex = 491
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(772, 3)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(45, 13)
		Me.Label2.TabIndex = 493
		Me.Label2.Text = "PO Trial"
		'
		'Panel8
		'
		Me.Panel8.BackColor = System.Drawing.Color.Tan
		Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Panel8.Location = New System.Drawing.Point(754, 4)
		Me.Panel8.Name = "Panel8"
		Me.Panel8.Size = New System.Drawing.Size(12, 12)
		Me.Panel8.TabIndex = 492
		'
		'TabControl1
		'
		Me.TabControl1.Controls.Add(Me.TabPage1)
		Me.TabControl1.Controls.Add(Me.TabPage2)
		Me.TabControl1.Controls.Add(Me.TabPage3)
		Me.TabControl1.Location = New System.Drawing.Point(21, 122)
		Me.TabControl1.Name = "TabControl1"
		Me.TabControl1.SelectedIndex = 0
		Me.TabControl1.Size = New System.Drawing.Size(958, 343)
		Me.TabControl1.TabIndex = 494
		'
		'TabPage1
		'
		Me.TabPage1.BackColor = System.Drawing.Color.White
		Me.TabPage1.Controls.Add(Me.Lv_Data)
		Me.TabPage1.Controls.Add(Me.Panel8)
		Me.TabPage1.Controls.Add(Me.Label15)
		Me.TabPage1.Controls.Add(Me.Label2)
		Me.TabPage1.Controls.Add(Me.Panel13)
		Me.TabPage1.Location = New System.Drawing.Point(4, 22)
		Me.TabPage1.Name = "TabPage1"
		Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage1.Size = New System.Drawing.Size(950, 317)
		Me.TabPage1.TabIndex = 0
		Me.TabPage1.Text = "GR 1"
		'
		'TabPage2
		'
		Me.TabPage2.BackColor = System.Drawing.Color.White
		Me.TabPage2.Controls.Add(Me.Label7)
		Me.TabPage2.Controls.Add(Me.Label6)
		Me.TabPage2.Controls.Add(Me.Panel11)
		Me.TabPage2.Controls.Add(Me.Panel12)
		Me.TabPage2.Controls.Add(Me.Lv_Packing_Pallet)
		Me.TabPage2.Location = New System.Drawing.Point(4, 22)
		Me.TabPage2.Name = "TabPage2"
		Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage2.Size = New System.Drawing.Size(950, 317)
		Me.TabPage2.TabIndex = 1
		Me.TabPage2.Text = "Pallet Packing"
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Location = New System.Drawing.Point(778, 3)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(59, 13)
		Me.Label7.TabIndex = 496
		Me.Label7.Text = "Repacking"
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Location = New System.Drawing.Point(862, 3)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(82, 13)
		Me.Label6.TabIndex = 496
		Me.Label6.Text = "Validasi Android"
		'
		'Panel11
		'
		Me.Panel11.BackColor = System.Drawing.Color.Tan
		Me.Panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Panel11.Location = New System.Drawing.Point(762, 4)
		Me.Panel11.Name = "Panel11"
		Me.Panel11.Size = New System.Drawing.Size(12, 12)
		Me.Panel11.TabIndex = 494
		'
		'Panel12
		'
		Me.Panel12.BackColor = System.Drawing.Color.LightBlue
		Me.Panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Panel12.Location = New System.Drawing.Point(846, 4)
		Me.Panel12.Name = "Panel12"
		Me.Panel12.Size = New System.Drawing.Size(12, 12)
		Me.Panel12.TabIndex = 494
		'
		'Lv_Packing_Pallet
		'
		Me.Lv_Packing_Pallet.CheckBoxes = True
		Me.Lv_Packing_Pallet.FullRowSelect = True
		Me.Lv_Packing_Pallet.GridLines = True
		Me.Lv_Packing_Pallet.HideSelection = False
		Me.Lv_Packing_Pallet.Location = New System.Drawing.Point(3, 22)
		Me.Lv_Packing_Pallet.Name = "Lv_Packing_Pallet"
		Me.Lv_Packing_Pallet.Size = New System.Drawing.Size(944, 292)
		Me.Lv_Packing_Pallet.TabIndex = 5
		Me.Lv_Packing_Pallet.UseCompatibleStateImageBehavior = False
		Me.Lv_Packing_Pallet.View = System.Windows.Forms.View.Details
		'
		'TabPage3
		'
		Me.TabPage3.Controls.Add(Me.Lv_Packing_Waste)
		Me.TabPage3.Location = New System.Drawing.Point(4, 22)
		Me.TabPage3.Name = "TabPage3"
		Me.TabPage3.Size = New System.Drawing.Size(950, 317)
		Me.TabPage3.TabIndex = 2
		Me.TabPage3.Text = "Waste Packing"
		Me.TabPage3.UseVisualStyleBackColor = True
		'
		'Lv_Packing_Waste
		'
		Me.Lv_Packing_Waste.CheckBoxes = True
		Me.Lv_Packing_Waste.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Lv_Packing_Waste.FullRowSelect = True
		Me.Lv_Packing_Waste.GridLines = True
		Me.Lv_Packing_Waste.HideSelection = False
		Me.Lv_Packing_Waste.Location = New System.Drawing.Point(0, 0)
		Me.Lv_Packing_Waste.Name = "Lv_Packing_Waste"
		Me.Lv_Packing_Waste.Size = New System.Drawing.Size(950, 317)
		Me.Lv_Packing_Waste.TabIndex = 5
		Me.Lv_Packing_Waste.UseCompatibleStateImageBehavior = False
		Me.Lv_Packing_Waste.View = System.Windows.Forms.View.Details
		'
		'SD_ValidasiGR_Split
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(998, 528)
		Me.Controls.Add(Me.TabControl1)
		Me.Controls.Add(Me.Btn_Close)
		Me.Controls.Add(Me.Btn_Tambah)
		Me.Controls.Add(Me.Btn_Cari)
		Me.Controls.Add(Me.Panel10)
		Me.Controls.Add(Me.Panel9)
		Me.Controls.Add(Me.Panel7)
		Me.Controls.Add(Me.Txt_ValueLain)
		Me.Controls.Add(Me.Tgl2)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.Tgl1)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.Cmb_Periode)
		Me.Controls.Add(Me.Cmb_Lain)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.MaximizeBox = False
		Me.Name = "SD_ValidasiGR_Split"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.Panel3.ResumeLayout(False)
		Me.Panel4.ResumeLayout(False)
		Me.TabControl1.ResumeLayout(False)
		Me.TabPage1.ResumeLayout(False)
		Me.TabPage1.PerformLayout()
		Me.TabPage2.ResumeLayout(False)
		Me.TabPage2.PerformLayout()
		Me.TabPage3.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents Cmb_Lain As ComboBox
    Friend WithEvents Tgl2 As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents Tgl1 As DateTimePicker
    Friend WithEvents Label5 As Label
    Friend WithEvents Cmb_Periode As ComboBox
    Friend WithEvents Txt_ValueLain As TextBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Lv_Data As ListView
    Friend WithEvents Btn_Tambah As Button
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Btn_Close As Button
    Friend WithEvents Label15 As Label
    Friend WithEvents Panel13 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel8 As Panel
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
	Friend WithEvents TabPage3 As TabPage
	Friend WithEvents Lv_Packing_Pallet As ListView
	Friend WithEvents Lv_Packing_Waste As ListView
	Friend WithEvents Label6 As Label
	Friend WithEvents Panel12 As Panel
	Friend WithEvents Label7 As Label
	Friend WithEvents Panel11 As Panel
End Class
