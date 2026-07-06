<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SD_Detail_Batch
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
		Me.TxtJumlahBatch = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Lbl_Judul = New System.Windows.Forms.Label()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Lv_DataDetail = New System.Windows.Forms.ListView()
		Me.Txt_TotNilaiFormula = New System.Windows.Forms.TextBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Txt_TotNilaiPRoduksi = New System.Windows.Forms.TextBox()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Panel_GI = New System.Windows.Forms.Panel()
		Me.TxtBatch = New System.Windows.Forms.TextBox()
		Me.Label11 = New System.Windows.Forms.Label()
		Me.Label9 = New System.Windows.Forms.Label()
		Me.Panel9 = New System.Windows.Forms.Panel()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.TxtJumlahBatchVw = New System.Windows.Forms.TextBox()
		Me.TxtNamaBarang = New System.Windows.Forms.TextBox()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.TxtNoSplit = New System.Windows.Forms.TextBox()
		Me.TabControl1 = New System.Windows.Forms.TabControl()
		Me.TabPage1 = New System.Windows.Forms.TabPage()
		Me.LvDataRekap = New System.Windows.Forms.ListView()
		Me.TabPage2 = New System.Windows.Forms.TabPage()
		Me.Btn_Cari_Pn1 = New System.Windows.Forms.Button()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Cmb_Filter_Batch_Pn1 = New System.Windows.Forms.ComboBox()
		Me.Cmb_KdBarang_Pn1 = New System.Windows.Forms.ComboBox()
		Me.Panel_GR = New System.Windows.Forms.Panel()
		Me.TabControl2 = New System.Windows.Forms.TabControl()
		Me.TabPage3 = New System.Windows.Forms.TabPage()
		Me.Lv_DataGr = New System.Windows.Forms.ListView()
		Me.TabPage4 = New System.Windows.Forms.TabPage()
		Me.Batch = New System.Windows.Forms.ListView()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
		Me.Panel1.SuspendLayout()
		Me.Panel_GI.SuspendLayout()
		Me.Panel9.SuspendLayout()
		Me.TabControl1.SuspendLayout()
		Me.TabPage1.SuspendLayout()
		Me.TabPage2.SuspendLayout()
		Me.Panel_GR.SuspendLayout()
		Me.TabControl2.SuspendLayout()
		Me.TabPage3.SuspendLayout()
		Me.TabPage4.SuspendLayout()
		Me.SuspendLayout()
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.TxtJumlahBatch)
		Me.Panel1.Controls.Add(Me.Label2)
		Me.Panel1.Controls.Add(Me.PanelGradient1)
		Me.Panel1.Controls.Add(Me.Lbl_Judul)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(1184, 45)
		Me.Panel1.TabIndex = 26
		'
		'TxtJumlahBatch
		'
		Me.TxtJumlahBatch.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.TxtJumlahBatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtJumlahBatch.Enabled = False
		Me.TxtJumlahBatch.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold)
		Me.TxtJumlahBatch.Location = New System.Drawing.Point(545, 12)
		Me.TxtJumlahBatch.MaxLength = 50
		Me.TxtJumlahBatch.Name = "TxtJumlahBatch"
		Me.TxtJumlahBatch.Size = New System.Drawing.Size(112, 23)
		Me.TxtJumlahBatch.TabIndex = 423
		Me.TxtJumlahBatch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		Me.TxtJumlahBatch.Visible = False
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(285, 21)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(211, 18)
		Me.Label2.TabIndex = 419
		Me.Label2.Text = "Catatan : Ada 2 Panel di Form ini"
		Me.Label2.Visible = False
		'
		'Lbl_Judul
		'
		Me.Lbl_Judul.AutoSize = True
		Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.0!, System.Drawing.FontStyle.Bold)
		Me.Lbl_Judul.Location = New System.Drawing.Point(20, 7)
		Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
		Me.Lbl_Judul.Name = "Lbl_Judul"
		Me.Lbl_Judul.Size = New System.Drawing.Size(133, 29)
		Me.Lbl_Judul.TabIndex = 0
		Me.Lbl_Judul.Text = "Detail Batch"
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(14, 45)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(1179, 12)
		Me.Panel2.TabIndex = 41
		Me.Panel2.Visible = False
		'
		'Lv_DataDetail
		'
		Me.Lv_DataDetail.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.Lv_DataDetail.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Lv_DataDetail.FullRowSelect = True
		Me.Lv_DataDetail.GridLines = True
		Me.Lv_DataDetail.HideSelection = False
		Me.Lv_DataDetail.Location = New System.Drawing.Point(3, 38)
		Me.Lv_DataDetail.Name = "Lv_DataDetail"
		Me.Lv_DataDetail.Size = New System.Drawing.Size(1125, 421)
		Me.Lv_DataDetail.TabIndex = 414
		Me.Lv_DataDetail.UseCompatibleStateImageBehavior = False
		Me.Lv_DataDetail.View = System.Windows.Forms.View.Details
		'
		'Txt_TotNilaiFormula
		'
		Me.Txt_TotNilaiFormula.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_TotNilaiFormula.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_TotNilaiFormula.Enabled = False
		Me.Txt_TotNilaiFormula.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold)
		Me.Txt_TotNilaiFormula.Location = New System.Drawing.Point(1209, 256)
		Me.Txt_TotNilaiFormula.MaxLength = 50
		Me.Txt_TotNilaiFormula.Name = "Txt_TotNilaiFormula"
		Me.Txt_TotNilaiFormula.Size = New System.Drawing.Size(169, 23)
		Me.Txt_TotNilaiFormula.TabIndex = 416
		Me.Txt_TotNilaiFormula.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(1017, 368)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(126, 18)
		Me.Label3.TabIndex = 415
		Me.Label3.Text = "Total NIlai Formula"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(1017, 397)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(128, 18)
		Me.Label1.TabIndex = 415
		Me.Label1.Text = "Total Nilai Produksi"
		'
		'Txt_TotNilaiPRoduksi
		'
		Me.Txt_TotNilaiPRoduksi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_TotNilaiPRoduksi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_TotNilaiPRoduksi.Enabled = False
		Me.Txt_TotNilaiPRoduksi.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold)
		Me.Txt_TotNilaiPRoduksi.Location = New System.Drawing.Point(1163, 395)
		Me.Txt_TotNilaiPRoduksi.MaxLength = 50
		Me.Txt_TotNilaiPRoduksi.Name = "Txt_TotNilaiPRoduksi"
		Me.Txt_TotNilaiPRoduksi.Size = New System.Drawing.Size(169, 23)
		Me.Txt_TotNilaiPRoduksi.TabIndex = 416
		Me.Txt_TotNilaiPRoduksi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		Me.Txt_TotNilaiPRoduksi.Visible = False
		'
		'Panel3
		'
		Me.Panel3.BackColor = System.Drawing.Color.Red
		Me.Panel3.Location = New System.Drawing.Point(0, 77)
		Me.Panel3.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(19, 515)
		Me.Panel3.TabIndex = 413
		Me.Panel3.Visible = False
		'
		'Panel_GI
		'
		Me.Panel_GI.Controls.Add(Me.TxtBatch)
		Me.Panel_GI.Controls.Add(Me.Label11)
		Me.Panel_GI.Controls.Add(Me.Label9)
		Me.Panel_GI.Controls.Add(Me.Panel9)
		Me.Panel_GI.Controls.Add(Me.Label8)
		Me.Panel_GI.Controls.Add(Me.TxtJumlahBatchVw)
		Me.Panel_GI.Controls.Add(Me.TxtNamaBarang)
		Me.Panel_GI.Controls.Add(Me.Label7)
		Me.Panel_GI.Controls.Add(Me.Label6)
		Me.Panel_GI.Controls.Add(Me.TxtNoSplit)
		Me.Panel_GI.Controls.Add(Me.TabControl1)
		Me.Panel_GI.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Panel_GI.Location = New System.Drawing.Point(24, 619)
		Me.Panel_GI.Name = "Panel_GI"
		Me.Panel_GI.Size = New System.Drawing.Size(1145, 527)
		Me.Panel_GI.TabIndex = 0
		'
		'TxtBatch
		'
		Me.TxtBatch.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.TxtBatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtBatch.Enabled = False
		Me.TxtBatch.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
		Me.TxtBatch.Location = New System.Drawing.Point(306, 7)
		Me.TxtBatch.MaxLength = 50
		Me.TxtBatch.Name = "TxtBatch"
		Me.TxtBatch.Size = New System.Drawing.Size(69, 20)
		Me.TxtBatch.TabIndex = 426
		Me.TxtBatch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		'
		'Label11
		'
		Me.Label11.AutoSize = True
		Me.Label11.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label11.Location = New System.Drawing.Point(207, 8)
		Me.Label11.Name = "Label11"
		Me.Label11.Size = New System.Drawing.Size(89, 17)
		Me.Label11.TabIndex = 425
		Me.Label11.Text = "Jumlah Batch"
		'
		'Label9
		'
		Me.Label9.AutoSize = True
		Me.Label9.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label9.Location = New System.Drawing.Point(1066, 32)
		Me.Label9.Name = "Label9"
		Me.Label9.Size = New System.Drawing.Size(44, 16)
		Me.Label9.TabIndex = 424
		Me.Label9.Text = "Selesai"
		'
		'Panel9
		'
		Me.Panel9.BackColor = System.Drawing.Color.LightGreen
		Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Panel9.Controls.Add(Me.Label10)
		Me.Panel9.Enabled = False
		Me.Panel9.Location = New System.Drawing.Point(1116, 29)
		Me.Panel9.Name = "Panel9"
		Me.Panel9.Size = New System.Drawing.Size(20, 20)
		Me.Panel9.TabIndex = 423
		'
		'Label10
		'
		Me.Label10.AutoSize = True
		Me.Label10.Location = New System.Drawing.Point(0, 0)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(0, 16)
		Me.Label10.TabIndex = 0
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label8.Location = New System.Drawing.Point(7, 8)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(95, 17)
		Me.Label8.TabIndex = 422
		Me.Label8.Text = "Nilai Per Batch"
		'
		'TxtJumlahBatchVw
		'
		Me.TxtJumlahBatchVw.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.TxtJumlahBatchVw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtJumlahBatchVw.Enabled = False
		Me.TxtJumlahBatchVw.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
		Me.TxtJumlahBatchVw.Location = New System.Drawing.Point(112, 7)
		Me.TxtJumlahBatchVw.MaxLength = 50
		Me.TxtJumlahBatchVw.Name = "TxtJumlahBatchVw"
		Me.TxtJumlahBatchVw.Size = New System.Drawing.Size(89, 20)
		Me.TxtJumlahBatchVw.TabIndex = 421
		Me.TxtJumlahBatchVw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		'
		'TxtNamaBarang
		'
		Me.TxtNamaBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.TxtNamaBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtNamaBarang.Enabled = False
		Me.TxtNamaBarang.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
		Me.TxtNamaBarang.Location = New System.Drawing.Point(716, 7)
		Me.TxtNamaBarang.MaxLength = 50
		Me.TxtNamaBarang.Name = "TxtNamaBarang"
		Me.TxtNamaBarang.Size = New System.Drawing.Size(300, 20)
		Me.TxtNamaBarang.TabIndex = 420
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label7.Location = New System.Drawing.Point(620, 8)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(86, 17)
		Me.Label7.TabIndex = 419
		Me.Label7.Text = "Nama Barang"
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label6.Location = New System.Drawing.Point(381, 8)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(56, 17)
		Me.Label6.TabIndex = 418
		Me.Label6.Text = "No Split"
		'
		'TxtNoSplit
		'
		Me.TxtNoSplit.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.TxtNoSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtNoSplit.Enabled = False
		Me.TxtNoSplit.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
		Me.TxtNoSplit.Location = New System.Drawing.Point(445, 7)
		Me.TxtNoSplit.MaxLength = 50
		Me.TxtNoSplit.Name = "TxtNoSplit"
		Me.TxtNoSplit.Size = New System.Drawing.Size(169, 20)
		Me.TxtNoSplit.TabIndex = 417
		'
		'TabControl1
		'
		Me.TabControl1.Controls.Add(Me.TabPage1)
		Me.TabControl1.Controls.Add(Me.TabPage2)
		Me.TabControl1.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.TabControl1.Location = New System.Drawing.Point(3, 32)
		Me.TabControl1.Name = "TabControl1"
		Me.TabControl1.SelectedIndex = 0
		Me.TabControl1.Size = New System.Drawing.Size(1139, 492)
		Me.TabControl1.TabIndex = 416
		'
		'TabPage1
		'
		Me.TabPage1.Controls.Add(Me.LvDataRekap)
		Me.TabPage1.Location = New System.Drawing.Point(4, 26)
		Me.TabPage1.Name = "TabPage1"
		Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage1.Size = New System.Drawing.Size(1131, 462)
		Me.TabPage1.TabIndex = 0
		Me.TabPage1.Text = "Rekap"
		Me.TabPage1.UseVisualStyleBackColor = True
		'
		'LvDataRekap
		'
		Me.LvDataRekap.Dock = System.Windows.Forms.DockStyle.Fill
		Me.LvDataRekap.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.LvDataRekap.FullRowSelect = True
		Me.LvDataRekap.GridLines = True
		Me.LvDataRekap.HideSelection = False
		Me.LvDataRekap.Location = New System.Drawing.Point(3, 3)
		Me.LvDataRekap.Name = "LvDataRekap"
		Me.LvDataRekap.Size = New System.Drawing.Size(1125, 456)
		Me.LvDataRekap.TabIndex = 415
		Me.LvDataRekap.UseCompatibleStateImageBehavior = False
		Me.LvDataRekap.View = System.Windows.Forms.View.Details
		'
		'TabPage2
		'
		Me.TabPage2.Controls.Add(Me.Lv_DataDetail)
		Me.TabPage2.Controls.Add(Me.Btn_Cari_Pn1)
		Me.TabPage2.Controls.Add(Me.Label4)
		Me.TabPage2.Controls.Add(Me.Label5)
		Me.TabPage2.Controls.Add(Me.Cmb_Filter_Batch_Pn1)
		Me.TabPage2.Controls.Add(Me.Cmb_KdBarang_Pn1)
		Me.TabPage2.Location = New System.Drawing.Point(4, 26)
		Me.TabPage2.Name = "TabPage2"
		Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage2.Size = New System.Drawing.Size(1131, 462)
		Me.TabPage2.TabIndex = 1
		Me.TabPage2.Text = "Detail"
		Me.TabPage2.UseVisualStyleBackColor = True
		'
		'Btn_Cari_Pn1
		'
		Me.Btn_Cari_Pn1.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Cari_Pn1.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Cari_Pn1.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Btn_Cari_Pn1.ForeColor = System.Drawing.Color.White
		Me.Btn_Cari_Pn1.Location = New System.Drawing.Point(465, 6)
		Me.Btn_Cari_Pn1.Name = "Btn_Cari_Pn1"
		Me.Btn_Cari_Pn1.Size = New System.Drawing.Size(80, 33)
		Me.Btn_Cari_Pn1.TabIndex = 2
		Me.Btn_Cari_Pn1.Text = "&Cari"
		Me.Btn_Cari_Pn1.UseVisualStyleBackColor = False
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label4.Location = New System.Drawing.Point(4, 10)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(42, 17)
		Me.Label4.TabIndex = 415
		Me.Label4.Text = "Batch"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label5.Location = New System.Drawing.Point(227, 11)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(77, 17)
		Me.Label5.TabIndex = 415
		Me.Label5.Text = "Kode Bahan"
		'
		'Cmb_Filter_Batch_Pn1
		'
		Me.Cmb_Filter_Batch_Pn1.BackColor = System.Drawing.Color.White
		Me.Cmb_Filter_Batch_Pn1.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Filter_Batch_Pn1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Filter_Batch_Pn1.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Cmb_Filter_Batch_Pn1.FormattingEnabled = True
		Me.Cmb_Filter_Batch_Pn1.Location = New System.Drawing.Point(74, 9)
		Me.Cmb_Filter_Batch_Pn1.Name = "Cmb_Filter_Batch_Pn1"
		Me.Cmb_Filter_Batch_Pn1.Size = New System.Drawing.Size(140, 24)
		Me.Cmb_Filter_Batch_Pn1.TabIndex = 0
		'
		'Cmb_KdBarang_Pn1
		'
		Me.Cmb_KdBarang_Pn1.BackColor = System.Drawing.Color.White
		Me.Cmb_KdBarang_Pn1.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_KdBarang_Pn1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_KdBarang_Pn1.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Cmb_KdBarang_Pn1.FormattingEnabled = True
		Me.Cmb_KdBarang_Pn1.Location = New System.Drawing.Point(319, 9)
		Me.Cmb_KdBarang_Pn1.Name = "Cmb_KdBarang_Pn1"
		Me.Cmb_KdBarang_Pn1.Size = New System.Drawing.Size(140, 24)
		Me.Cmb_KdBarang_Pn1.TabIndex = 1
		'
		'Panel_GR
		'
		Me.Panel_GR.Controls.Add(Me.TabControl2)
		Me.Panel_GR.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Panel_GR.Location = New System.Drawing.Point(20, 57)
		Me.Panel_GR.Name = "Panel_GR"
		Me.Panel_GR.Size = New System.Drawing.Size(1146, 530)
		Me.Panel_GR.TabIndex = 0
		'
		'TabControl2
		'
		Me.TabControl2.Controls.Add(Me.TabPage3)
		Me.TabControl2.Controls.Add(Me.TabPage4)
		Me.TabControl2.Font = New System.Drawing.Font("Work Sans", 9.75!)
		Me.TabControl2.Location = New System.Drawing.Point(0, 3)
		Me.TabControl2.Name = "TabControl2"
		Me.TabControl2.SelectedIndex = 0
		Me.TabControl2.Size = New System.Drawing.Size(1146, 527)
		Me.TabControl2.TabIndex = 0
		'
		'TabPage3
		'
		Me.TabPage3.Controls.Add(Me.Lv_DataGr)
		Me.TabPage3.Location = New System.Drawing.Point(4, 27)
		Me.TabPage3.Name = "TabPage3"
		Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage3.Size = New System.Drawing.Size(1138, 496)
		Me.TabPage3.TabIndex = 0
		Me.TabPage3.Text = "Rekap"
		Me.TabPage3.UseVisualStyleBackColor = True
		'
		'Lv_DataGr
		'
		Me.Lv_DataGr.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Lv_DataGr.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Lv_DataGr.FullRowSelect = True
		Me.Lv_DataGr.GridLines = True
		Me.Lv_DataGr.HideSelection = False
		Me.Lv_DataGr.Location = New System.Drawing.Point(3, 3)
		Me.Lv_DataGr.Name = "Lv_DataGr"
		Me.Lv_DataGr.Size = New System.Drawing.Size(1132, 490)
		Me.Lv_DataGr.TabIndex = 416
		Me.Lv_DataGr.UseCompatibleStateImageBehavior = False
		Me.Lv_DataGr.View = System.Windows.Forms.View.Details
		'
		'TabPage4
		'
		Me.TabPage4.Controls.Add(Me.Batch)
		Me.TabPage4.Location = New System.Drawing.Point(4, 27)
		Me.TabPage4.Name = "TabPage4"
		Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage4.Size = New System.Drawing.Size(1138, 496)
		Me.TabPage4.TabIndex = 1
		Me.TabPage4.Text = "Detail"
		Me.TabPage4.UseVisualStyleBackColor = True
		'
		'Batch
		'
		Me.Batch.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Batch.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Batch.FullRowSelect = True
		Me.Batch.GridLines = True
		Me.Batch.HideSelection = False
		Me.Batch.Location = New System.Drawing.Point(3, 3)
		Me.Batch.Name = "Batch"
		Me.Batch.Size = New System.Drawing.Size(1132, 490)
		Me.Batch.TabIndex = 416
		Me.Batch.UseCompatibleStateImageBehavior = False
		Me.Batch.View = System.Windows.Forms.View.Details
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.Color.Red
		Me.Panel4.Location = New System.Drawing.Point(1165, 69)
		Me.Panel4.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(19, 515)
		Me.Panel4.TabIndex = 414
		Me.Panel4.Visible = False
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.Color.Red
		Me.Panel5.Location = New System.Drawing.Point(20, 596)
		Me.Panel5.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(1179, 15)
		Me.Panel5.TabIndex = 42
		Me.Panel5.Visible = False
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
		Me.PanelGradient1.Size = New System.Drawing.Size(1184, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'SD_Detail_Batch
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(1184, 611)
		Me.Controls.Add(Me.Panel_GR)
		Me.Controls.Add(Me.Panel_GI)
		Me.Controls.Add(Me.Panel5)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.Txt_TotNilaiPRoduksi)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.Txt_TotNilaiFormula)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel1)
		Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "SD_Detail_Batch"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.Panel_GI.ResumeLayout(False)
		Me.Panel_GI.PerformLayout()
		Me.Panel9.ResumeLayout(False)
		Me.Panel9.PerformLayout()
		Me.TabControl1.ResumeLayout(False)
		Me.TabPage1.ResumeLayout(False)
		Me.TabPage2.ResumeLayout(False)
		Me.TabPage2.PerformLayout()
		Me.Panel_GR.ResumeLayout(False)
		Me.TabControl2.ResumeLayout(False)
		Me.TabPage3.ResumeLayout(False)
		Me.TabPage4.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Lv_DataDetail As ListView
    Friend WithEvents Txt_TotNilaiFormula As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_TotNilaiPRoduksi As TextBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel_GI As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Cmb_Filter_Batch_Pn1 As ComboBox
    Friend WithEvents Cmb_KdBarang_Pn1 As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Btn_Cari_Pn1 As Button
    Friend WithEvents Panel_GR As Panel
    Friend WithEvents Lv_DataGr As ListView
    Friend WithEvents Batch As ListView
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents TxtNoSplit As TextBox
    Friend WithEvents TxtNamaBarang As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents LvDataRekap As ListView
    Friend WithEvents TxtJumlahBatch As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TxtJumlahBatchVw As TextBox
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Label9 As Label
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Label10 As Label
    Friend WithEvents TxtBatch As TextBox
    Friend WithEvents Label11 As Label
End Class
