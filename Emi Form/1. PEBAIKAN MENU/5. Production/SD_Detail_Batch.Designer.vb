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
		Me.components = New System.ComponentModel.Container()
		Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
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
		Me.Label25 = New System.Windows.Forms.Label()
		Me.Txt_GR_Rekap_Total_Scrap = New System.Windows.Forms.TextBox()
		Me.Label24 = New System.Windows.Forms.Label()
		Me.Lv_GR_Detail_Scrap = New System.Windows.Forms.ListView()
		Me.Label22 = New System.Windows.Forms.Label()
		Me.Txt_GR_Rekap_Total_GR = New System.Windows.Forms.TextBox()
		Me.Label21 = New System.Windows.Forms.Label()
		Me.Txt_GR_Rekap_Total = New System.Windows.Forms.TextBox()
		Me.Label17 = New System.Windows.Forms.Label()
		Me.Panel10 = New System.Windows.Forms.Panel()
		Me.Panel8 = New System.Windows.Forms.Panel()
		Me.Label23 = New System.Windows.Forms.Label()
		Me.Label16 = New System.Windows.Forms.Label()
		Me.Lv_GR_Detail_Pallet = New System.Windows.Forms.ListView()
		Me.PnlWeek = New System.Windows.Forms.Panel()
		Me.LblSatuan = New System.Windows.Forms.Label()
		Me.Dgv_GR_Batch = New System.Windows.Forms.DataGridView()
		Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.ValidasiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.Txt_GR_Rekap_Selisih = New System.Windows.Forms.TextBox()
		Me.Txt_GR_Rekap_Result = New System.Windows.Forms.TextBox()
		Me.Label20 = New System.Windows.Forms.Label()
		Me.Label15 = New System.Windows.Forms.Label()
		Me.Txt_GR_Rekap_Jumlah_Selesai = New System.Windows.Forms.TextBox()
		Me.Txt_GR_Rekap_Split = New System.Windows.Forms.TextBox()
		Me.Label19 = New System.Windows.Forms.Label()
		Me.Label14 = New System.Windows.Forms.Label()
		Me.Txt_GR_Rekap_Jumlah_Dosing = New System.Windows.Forms.TextBox()
		Me.Txt_GR_Rekap_PO = New System.Windows.Forms.TextBox()
		Me.Label18 = New System.Windows.Forms.Label()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.Label13 = New System.Windows.Forms.Label()
		Me.Panel11 = New System.Windows.Forms.Panel()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.TabPage4 = New System.Windows.Forms.TabPage()
		Me.Btn_Cari_GR = New System.Windows.Forms.Button()
		Me.Txt_Filter_GR = New System.Windows.Forms.TextBox()
		Me.Label12 = New System.Windows.Forms.Label()
		Me.Cmb_Filter_GR = New System.Windows.Forms.ComboBox()
		Me.Btn_Cetak_GR = New System.Windows.Forms.Button()
		Me.Batch = New System.Windows.Forms.ListView()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Btn_Batch_Blm_Validasi = New ERP_EMI.RoundedButton()
		Me.Btn_Batch_Sdh_Validasi = New ERP_EMI.RoundedButton()
		Me.Btn_Batch_All = New ERP_EMI.RoundedButton()
		Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
		CType(Me.Dgv_GR_Batch, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.ContextMenuStrip1.SuspendLayout()
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
		Me.Panel1.Size = New System.Drawing.Size(1184, 54)
		Me.Panel1.TabIndex = 26
		'
		'TxtJumlahBatch
		'
		Me.TxtJumlahBatch.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.TxtJumlahBatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtJumlahBatch.Enabled = False
		Me.TxtJumlahBatch.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold)
		Me.TxtJumlahBatch.Location = New System.Drawing.Point(1060, 16)
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
		Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Lbl_Judul.Location = New System.Drawing.Point(20, 14)
		Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
		Me.Lbl_Judul.Name = "Lbl_Judul"
		Me.Lbl_Judul.Size = New System.Drawing.Size(140, 25)
		Me.Lbl_Judul.TabIndex = 0
		Me.Lbl_Judul.Text = "Detail Batch"
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.Color.Red
		Me.Panel2.Location = New System.Drawing.Point(14, 55)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(1179, 12)
		Me.Panel2.TabIndex = 41
		Me.Panel2.Visible = False
		'
		'Lv_DataDetail
		'
		Me.Lv_DataDetail.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv_DataDetail.FullRowSelect = True
		Me.Lv_DataDetail.GridLines = True
		Me.Lv_DataDetail.HideSelection = False
		Me.Lv_DataDetail.Location = New System.Drawing.Point(0, 45)
		Me.Lv_DataDetail.Name = "Lv_DataDetail"
		Me.Lv_DataDetail.Size = New System.Drawing.Size(1131, 416)
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
		Me.Panel_GI.Location = New System.Drawing.Point(21, 619)
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
		Me.TxtBatch.Location = New System.Drawing.Point(306, 6)
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
		Me.Label11.Location = New System.Drawing.Point(207, 7)
		Me.Label11.Name = "Label11"
		Me.Label11.Size = New System.Drawing.Size(89, 17)
		Me.Label11.TabIndex = 425
		Me.Label11.Text = "Jumlah Batch"
		'
		'Label9
		'
		Me.Label9.AutoSize = True
		Me.Label9.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label9.Location = New System.Drawing.Point(1072, 7)
		Me.Label9.Name = "Label9"
		Me.Label9.Size = New System.Drawing.Size(50, 17)
		Me.Label9.TabIndex = 424
		Me.Label9.Text = "Selesai"
		'
		'Panel9
		'
		Me.Panel9.BackColor = System.Drawing.Color.LightGreen
		Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Panel9.Controls.Add(Me.Label10)
		Me.Panel9.Enabled = False
		Me.Panel9.Location = New System.Drawing.Point(1122, 6)
		Me.Panel9.Name = "Panel9"
		Me.Panel9.Size = New System.Drawing.Size(20, 20)
		Me.Panel9.TabIndex = 423
		'
		'Label10
		'
		Me.Label10.AutoSize = True
		Me.Label10.Location = New System.Drawing.Point(0, 0)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(0, 18)
		Me.Label10.TabIndex = 0
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label8.Location = New System.Drawing.Point(7, 7)
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
		Me.TxtJumlahBatchVw.Location = New System.Drawing.Point(112, 6)
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
		Me.TxtNamaBarang.Location = New System.Drawing.Point(766, 6)
		Me.TxtNamaBarang.MaxLength = 50
		Me.TxtNamaBarang.Name = "TxtNamaBarang"
		Me.TxtNamaBarang.Size = New System.Drawing.Size(300, 20)
		Me.TxtNamaBarang.TabIndex = 420
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label7.Location = New System.Drawing.Point(670, 7)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(86, 17)
		Me.Label7.TabIndex = 419
		Me.Label7.Text = "Nama Barang"
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label6.Location = New System.Drawing.Point(431, 7)
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
		Me.TxtNoSplit.Location = New System.Drawing.Point(495, 6)
		Me.TxtNoSplit.MaxLength = 50
		Me.TxtNoSplit.Name = "TxtNoSplit"
		Me.TxtNoSplit.Size = New System.Drawing.Size(169, 20)
		Me.TxtNoSplit.TabIndex = 417
		'
		'TabControl1
		'
		Me.TabControl1.Controls.Add(Me.TabPage1)
		Me.TabControl1.Controls.Add(Me.TabPage2)
		Me.TabControl1.Location = New System.Drawing.Point(3, 32)
		Me.TabControl1.Name = "TabControl1"
		Me.TabControl1.SelectedIndex = 0
		Me.TabControl1.Size = New System.Drawing.Size(1139, 492)
		Me.TabControl1.TabIndex = 416
		'
		'TabPage1
		'
		Me.TabPage1.Controls.Add(Me.LvDataRekap)
		Me.TabPage1.Location = New System.Drawing.Point(4, 27)
		Me.TabPage1.Name = "TabPage1"
		Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage1.Size = New System.Drawing.Size(1131, 461)
		Me.TabPage1.TabIndex = 0
		Me.TabPage1.Text = "Rekap"
		Me.TabPage1.UseVisualStyleBackColor = True
		'
		'LvDataRekap
		'
		Me.LvDataRekap.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.LvDataRekap.FullRowSelect = True
		Me.LvDataRekap.GridLines = True
		Me.LvDataRekap.HideSelection = False
		Me.LvDataRekap.Location = New System.Drawing.Point(0, 0)
		Me.LvDataRekap.Name = "LvDataRekap"
		Me.LvDataRekap.Size = New System.Drawing.Size(1131, 461)
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
		Me.TabPage2.Location = New System.Drawing.Point(4, 27)
		Me.TabPage2.Name = "TabPage2"
		Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage2.Size = New System.Drawing.Size(1131, 461)
		Me.TabPage2.TabIndex = 1
		Me.TabPage2.Text = "Detail"
		Me.TabPage2.UseVisualStyleBackColor = True
		'
		'Btn_Cari_Pn1
		'
		Me.Btn_Cari_Pn1.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Cari_Pn1.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Cari_Pn1.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Cari_Pn1.ForeColor = System.Drawing.Color.White
		Me.Btn_Cari_Pn1.Location = New System.Drawing.Point(443, 6)
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
		Me.Label4.Location = New System.Drawing.Point(4, 11)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(42, 17)
		Me.Label4.TabIndex = 415
		Me.Label4.Text = "Batch"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label5.Location = New System.Drawing.Point(205, 11)
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
		Me.Cmb_Filter_Batch_Pn1.Location = New System.Drawing.Point(52, 9)
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
		Me.Cmb_KdBarang_Pn1.Location = New System.Drawing.Point(297, 9)
		Me.Cmb_KdBarang_Pn1.Name = "Cmb_KdBarang_Pn1"
		Me.Cmb_KdBarang_Pn1.Size = New System.Drawing.Size(140, 24)
		Me.Cmb_KdBarang_Pn1.TabIndex = 1
		'
		'Panel_GR
		'
		Me.Panel_GR.Controls.Add(Me.TabControl2)
		Me.Panel_GR.Location = New System.Drawing.Point(20, 69)
		Me.Panel_GR.Name = "Panel_GR"
		Me.Panel_GR.Size = New System.Drawing.Size(1146, 530)
		Me.Panel_GR.TabIndex = 0
		'
		'TabControl2
		'
		Me.TabControl2.Controls.Add(Me.TabPage3)
		Me.TabControl2.Controls.Add(Me.TabPage4)
		Me.TabControl2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TabControl2.Location = New System.Drawing.Point(0, 0)
		Me.TabControl2.Name = "TabControl2"
		Me.TabControl2.SelectedIndex = 0
		Me.TabControl2.Size = New System.Drawing.Size(1146, 530)
		Me.TabControl2.TabIndex = 0
		'
		'TabPage3
		'
		Me.TabPage3.BackColor = System.Drawing.Color.White
		Me.TabPage3.Controls.Add(Me.Btn_Batch_Blm_Validasi)
		Me.TabPage3.Controls.Add(Me.Btn_Batch_Sdh_Validasi)
		Me.TabPage3.Controls.Add(Me.Btn_Batch_All)
		Me.TabPage3.Controls.Add(Me.Label25)
		Me.TabPage3.Controls.Add(Me.Txt_GR_Rekap_Total_Scrap)
		Me.TabPage3.Controls.Add(Me.Label24)
		Me.TabPage3.Controls.Add(Me.Lv_GR_Detail_Scrap)
		Me.TabPage3.Controls.Add(Me.Label22)
		Me.TabPage3.Controls.Add(Me.Txt_GR_Rekap_Total_GR)
		Me.TabPage3.Controls.Add(Me.Label21)
		Me.TabPage3.Controls.Add(Me.Txt_GR_Rekap_Total)
		Me.TabPage3.Controls.Add(Me.Label17)
		Me.TabPage3.Controls.Add(Me.Panel10)
		Me.TabPage3.Controls.Add(Me.Panel8)
		Me.TabPage3.Controls.Add(Me.Label23)
		Me.TabPage3.Controls.Add(Me.Label16)
		Me.TabPage3.Controls.Add(Me.Lv_GR_Detail_Pallet)
		Me.TabPage3.Controls.Add(Me.PnlWeek)
		Me.TabPage3.Controls.Add(Me.LblSatuan)
		Me.TabPage3.Controls.Add(Me.Dgv_GR_Batch)
		Me.TabPage3.Controls.Add(Me.Txt_GR_Rekap_Selisih)
		Me.TabPage3.Controls.Add(Me.Txt_GR_Rekap_Result)
		Me.TabPage3.Controls.Add(Me.Label20)
		Me.TabPage3.Controls.Add(Me.Label15)
		Me.TabPage3.Controls.Add(Me.Txt_GR_Rekap_Jumlah_Selesai)
		Me.TabPage3.Controls.Add(Me.Txt_GR_Rekap_Split)
		Me.TabPage3.Controls.Add(Me.Label19)
		Me.TabPage3.Controls.Add(Me.Label14)
		Me.TabPage3.Controls.Add(Me.Txt_GR_Rekap_Jumlah_Dosing)
		Me.TabPage3.Controls.Add(Me.Txt_GR_Rekap_PO)
		Me.TabPage3.Controls.Add(Me.Label18)
		Me.TabPage3.Controls.Add(Me.Panel7)
		Me.TabPage3.Controls.Add(Me.Label13)
		Me.TabPage3.Controls.Add(Me.Panel11)
		Me.TabPage3.Controls.Add(Me.Panel6)
		Me.TabPage3.Location = New System.Drawing.Point(4, 27)
		Me.TabPage3.Name = "TabPage3"
		Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage3.Size = New System.Drawing.Size(1138, 499)
		Me.TabPage3.TabIndex = 0
		Me.TabPage3.Text = "Rekap"
		'
		'Label25
		'
		Me.Label25.AutoSize = True
		Me.Label25.Font = New System.Drawing.Font("Work Sans SemiBold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label25.Location = New System.Drawing.Point(10, 76)
		Me.Label25.Name = "Label25"
		Me.Label25.Size = New System.Drawing.Size(104, 23)
		Me.Label25.TabIndex = 495
		Me.Label25.Text = "Filter Batch"
		'
		'Txt_GR_Rekap_Total_Scrap
		'
		Me.Txt_GR_Rekap_Total_Scrap.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_GR_Rekap_Total_Scrap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_GR_Rekap_Total_Scrap.Enabled = False
		Me.Txt_GR_Rekap_Total_Scrap.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_GR_Rekap_Total_Scrap.Location = New System.Drawing.Point(734, 470)
		Me.Txt_GR_Rekap_Total_Scrap.Name = "Txt_GR_Rekap_Total_Scrap"
		Me.Txt_GR_Rekap_Total_Scrap.Size = New System.Drawing.Size(148, 20)
		Me.Txt_GR_Rekap_Total_Scrap.TabIndex = 494
		Me.Txt_GR_Rekap_Total_Scrap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		'
		'Label24
		'
		Me.Label24.AutoSize = True
		Me.Label24.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label24.Location = New System.Drawing.Point(651, 472)
		Me.Label24.Name = "Label24"
		Me.Label24.Size = New System.Drawing.Size(76, 17)
		Me.Label24.TabIndex = 493
		Me.Label24.Text = "Total Scrap"
		'
		'Lv_GR_Detail_Scrap
		'
		Me.Lv_GR_Detail_Scrap.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv_GR_Detail_Scrap.FullRowSelect = True
		Me.Lv_GR_Detail_Scrap.GridLines = True
		Me.Lv_GR_Detail_Scrap.HideSelection = False
		Me.Lv_GR_Detail_Scrap.Location = New System.Drawing.Point(97, 324)
		Me.Lv_GR_Detail_Scrap.Name = "Lv_GR_Detail_Scrap"
		Me.Lv_GR_Detail_Scrap.Size = New System.Drawing.Size(1035, 140)
		Me.Lv_GR_Detail_Scrap.TabIndex = 492
		Me.Lv_GR_Detail_Scrap.UseCompatibleStateImageBehavior = False
		Me.Lv_GR_Detail_Scrap.View = System.Windows.Forms.View.Details
		'
		'Label22
		'
		Me.Label22.AutoSize = True
		Me.Label22.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Label22.Location = New System.Drawing.Point(777, 128)
		Me.Label22.Name = "Label22"
		Me.Label22.Size = New System.Drawing.Size(355, 16)
		Me.Label22.TabIndex = 491
		Me.Label22.Text = "* Klik kanan pada list batch untuk melakukan validasi per batch"
		'
		'Txt_GR_Rekap_Total_GR
		'
		Me.Txt_GR_Rekap_Total_GR.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_GR_Rekap_Total_GR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_GR_Rekap_Total_GR.Enabled = False
		Me.Txt_GR_Rekap_Total_GR.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_GR_Rekap_Total_GR.Location = New System.Drawing.Point(498, 470)
		Me.Txt_GR_Rekap_Total_GR.Name = "Txt_GR_Rekap_Total_GR"
		Me.Txt_GR_Rekap_Total_GR.Size = New System.Drawing.Size(148, 20)
		Me.Txt_GR_Rekap_Total_GR.TabIndex = 490
		Me.Txt_GR_Rekap_Total_GR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		'
		'Label21
		'
		Me.Label21.AutoSize = True
		Me.Label21.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label21.Location = New System.Drawing.Point(432, 472)
		Me.Label21.Name = "Label21"
		Me.Label21.Size = New System.Drawing.Size(58, 17)
		Me.Label21.TabIndex = 489
		Me.Label21.Text = "Total GR"
		'
		'Txt_GR_Rekap_Total
		'
		Me.Txt_GR_Rekap_Total.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_GR_Rekap_Total.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_GR_Rekap_Total.Enabled = False
		Me.Txt_GR_Rekap_Total.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_GR_Rekap_Total.Location = New System.Drawing.Point(984, 470)
		Me.Txt_GR_Rekap_Total.Name = "Txt_GR_Rekap_Total"
		Me.Txt_GR_Rekap_Total.Size = New System.Drawing.Size(148, 20)
		Me.Txt_GR_Rekap_Total.TabIndex = 490
		Me.Txt_GR_Rekap_Total.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		'
		'Label17
		'
		Me.Label17.AutoSize = True
		Me.Label17.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label17.Location = New System.Drawing.Point(888, 472)
		Me.Label17.Name = "Label17"
		Me.Label17.Size = New System.Drawing.Size(91, 17)
		Me.Label17.TabIndex = 489
		Me.Label17.Text = "Total Terpakai"
		'
		'Panel10
		'
		Me.Panel10.BackColor = System.Drawing.Color.DarkGray
		Me.Panel10.Location = New System.Drawing.Point(101, 319)
		Me.Panel10.Name = "Panel10"
		Me.Panel10.Size = New System.Drawing.Size(111, 2)
		Me.Panel10.TabIndex = 488
		'
		'Panel8
		'
		Me.Panel8.BackColor = System.Drawing.Color.DarkGray
		Me.Panel8.Location = New System.Drawing.Point(100, 145)
		Me.Panel8.Name = "Panel8"
		Me.Panel8.Size = New System.Drawing.Size(111, 2)
		Me.Panel8.TabIndex = 488
		'
		'Label23
		'
		Me.Label23.AutoSize = True
		Me.Label23.Font = New System.Drawing.Font("Work Sans SemiBold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label23.Location = New System.Drawing.Point(103, 295)
		Me.Label23.Name = "Label23"
		Me.Label23.Size = New System.Drawing.Size(107, 23)
		Me.Label23.TabIndex = 487
		Me.Label23.Text = "Detail Scrap"
		'
		'Label16
		'
		Me.Label16.AutoSize = True
		Me.Label16.Font = New System.Drawing.Font("Work Sans SemiBold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label16.Location = New System.Drawing.Point(102, 121)
		Me.Label16.Name = "Label16"
		Me.Label16.Size = New System.Drawing.Size(107, 23)
		Me.Label16.TabIndex = 487
		Me.Label16.Text = "Detail Pallet"
		'
		'Lv_GR_Detail_Pallet
		'
		Me.Lv_GR_Detail_Pallet.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Lv_GR_Detail_Pallet.FullRowSelect = True
		Me.Lv_GR_Detail_Pallet.GridLines = True
		Me.Lv_GR_Detail_Pallet.HideSelection = False
		Me.Lv_GR_Detail_Pallet.Location = New System.Drawing.Point(97, 150)
		Me.Lv_GR_Detail_Pallet.Name = "Lv_GR_Detail_Pallet"
		Me.Lv_GR_Detail_Pallet.Size = New System.Drawing.Size(1035, 140)
		Me.Lv_GR_Detail_Pallet.TabIndex = 486
		Me.Lv_GR_Detail_Pallet.UseCompatibleStateImageBehavior = False
		Me.Lv_GR_Detail_Pallet.View = System.Windows.Forms.View.Details
		'
		'PnlWeek
		'
		Me.PnlWeek.BackColor = System.Drawing.Color.DarkGray
		Me.PnlWeek.Location = New System.Drawing.Point(13, 145)
		Me.PnlWeek.Name = "PnlWeek"
		Me.PnlWeek.Size = New System.Drawing.Size(60, 2)
		Me.PnlWeek.TabIndex = 485
		'
		'LblSatuan
		'
		Me.LblSatuan.AutoSize = True
		Me.LblSatuan.Font = New System.Drawing.Font("Work Sans SemiBold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LblSatuan.Location = New System.Drawing.Point(15, 121)
		Me.LblSatuan.Name = "LblSatuan"
		Me.LblSatuan.Size = New System.Drawing.Size(56, 23)
		Me.LblSatuan.TabIndex = 484
		Me.LblSatuan.Text = "Batch"
		'
		'Dgv_GR_Batch
		'
		Me.Dgv_GR_Batch.AllowUserToAddRows = False
		Me.Dgv_GR_Batch.AllowUserToResizeColumns = False
		Me.Dgv_GR_Batch.AllowUserToResizeRows = False
		Me.Dgv_GR_Batch.BackgroundColor = System.Drawing.Color.White
		Me.Dgv_GR_Batch.BorderStyle = System.Windows.Forms.BorderStyle.None
		DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle1.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.Dgv_GR_Batch.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
		Me.Dgv_GR_Batch.ColumnHeadersHeight = 30
		Me.Dgv_GR_Batch.ColumnHeadersVisible = False
		Me.Dgv_GR_Batch.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn2})
		Me.Dgv_GR_Batch.ContextMenuStrip = Me.ContextMenuStrip1
		DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
		DataGridViewCellStyle3.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
		DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight
		DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
		DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
		Me.Dgv_GR_Batch.DefaultCellStyle = DataGridViewCellStyle3
		Me.Dgv_GR_Batch.GridColor = System.Drawing.Color.White
		Me.Dgv_GR_Batch.Location = New System.Drawing.Point(7, 150)
		Me.Dgv_GR_Batch.Margin = New System.Windows.Forms.Padding(0)
		Me.Dgv_GR_Batch.Name = "Dgv_GR_Batch"
		Me.Dgv_GR_Batch.RowHeadersVisible = False
		Me.Dgv_GR_Batch.RowHeadersWidth = 4
		DataGridViewCellStyle4.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Dgv_GR_Batch.RowsDefaultCellStyle = DataGridViewCellStyle4
		Me.Dgv_GR_Batch.RowTemplate.Height = 35
		Me.Dgv_GR_Batch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
		Me.Dgv_GR_Batch.Size = New System.Drawing.Size(72, 338)
		Me.Dgv_GR_Batch.TabIndex = 483
		'
		'ContextMenuStrip1
		'
		Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ValidasiToolStripMenuItem})
		Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
		Me.ContextMenuStrip1.Size = New System.Drawing.Size(114, 26)
		'
		'ValidasiToolStripMenuItem
		'
		Me.ValidasiToolStripMenuItem.Name = "ValidasiToolStripMenuItem"
		Me.ValidasiToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
		Me.ValidasiToolStripMenuItem.Text = "Validasi"
		'
		'Txt_GR_Rekap_Selisih
		'
		Me.Txt_GR_Rekap_Selisih.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_GR_Rekap_Selisih.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_GR_Rekap_Selisih.Enabled = False
		Me.Txt_GR_Rekap_Selisih.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_GR_Rekap_Selisih.Location = New System.Drawing.Point(717, 35)
		Me.Txt_GR_Rekap_Selisih.Name = "Txt_GR_Rekap_Selisih"
		Me.Txt_GR_Rekap_Selisih.Size = New System.Drawing.Size(198, 20)
		Me.Txt_GR_Rekap_Selisih.TabIndex = 422
		Me.Txt_GR_Rekap_Selisih.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		'
		'Txt_GR_Rekap_Result
		'
		Me.Txt_GR_Rekap_Result.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_GR_Rekap_Result.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_GR_Rekap_Result.Enabled = False
		Me.Txt_GR_Rekap_Result.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_GR_Rekap_Result.Location = New System.Drawing.Point(717, 9)
		Me.Txt_GR_Rekap_Result.Name = "Txt_GR_Rekap_Result"
		Me.Txt_GR_Rekap_Result.Size = New System.Drawing.Size(198, 20)
		Me.Txt_GR_Rekap_Result.TabIndex = 422
		'
		'Label20
		'
		Me.Label20.AutoSize = True
		Me.Label20.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label20.Location = New System.Drawing.Point(620, 37)
		Me.Label20.Name = "Label20"
		Me.Label20.Size = New System.Drawing.Size(93, 17)
		Me.Label20.TabIndex = 421
		Me.Label20.Text = "Jumlah Selisih"
		'
		'Label15
		'
		Me.Label15.AutoSize = True
		Me.Label15.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label15.Location = New System.Drawing.Point(620, 11)
		Me.Label15.Name = "Label15"
		Me.Label15.Size = New System.Drawing.Size(65, 17)
		Me.Label15.TabIndex = 421
		Me.Label15.Text = "No Result"
		'
		'Txt_GR_Rekap_Jumlah_Selesai
		'
		Me.Txt_GR_Rekap_Jumlah_Selesai.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_GR_Rekap_Jumlah_Selesai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_GR_Rekap_Jumlah_Selesai.Enabled = False
		Me.Txt_GR_Rekap_Jumlah_Selesai.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_GR_Rekap_Jumlah_Selesai.Location = New System.Drawing.Point(416, 35)
		Me.Txt_GR_Rekap_Jumlah_Selesai.Name = "Txt_GR_Rekap_Jumlah_Selesai"
		Me.Txt_GR_Rekap_Jumlah_Selesai.Size = New System.Drawing.Size(198, 20)
		Me.Txt_GR_Rekap_Jumlah_Selesai.TabIndex = 422
		Me.Txt_GR_Rekap_Jumlah_Selesai.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		'
		'Txt_GR_Rekap_Split
		'
		Me.Txt_GR_Rekap_Split.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_GR_Rekap_Split.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_GR_Rekap_Split.Enabled = False
		Me.Txt_GR_Rekap_Split.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_GR_Rekap_Split.Location = New System.Drawing.Point(416, 9)
		Me.Txt_GR_Rekap_Split.Name = "Txt_GR_Rekap_Split"
		Me.Txt_GR_Rekap_Split.Size = New System.Drawing.Size(198, 20)
		Me.Txt_GR_Rekap_Split.TabIndex = 422
		'
		'Label19
		'
		Me.Label19.AutoSize = True
		Me.Label19.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label19.Location = New System.Drawing.Point(314, 37)
		Me.Label19.Name = "Label19"
		Me.Label19.Size = New System.Drawing.Size(97, 17)
		Me.Label19.TabIndex = 421
		Me.Label19.Text = "Jumlah Selesai"
		'
		'Label14
		'
		Me.Label14.AutoSize = True
		Me.Label14.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label14.Location = New System.Drawing.Point(314, 11)
		Me.Label14.Name = "Label14"
		Me.Label14.Size = New System.Drawing.Size(56, 17)
		Me.Label14.TabIndex = 421
		Me.Label14.Text = "No Split"
		'
		'Txt_GR_Rekap_Jumlah_Dosing
		'
		Me.Txt_GR_Rekap_Jumlah_Dosing.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_GR_Rekap_Jumlah_Dosing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_GR_Rekap_Jumlah_Dosing.Enabled = False
		Me.Txt_GR_Rekap_Jumlah_Dosing.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_GR_Rekap_Jumlah_Dosing.Location = New System.Drawing.Point(109, 35)
		Me.Txt_GR_Rekap_Jumlah_Dosing.Name = "Txt_GR_Rekap_Jumlah_Dosing"
		Me.Txt_GR_Rekap_Jumlah_Dosing.Size = New System.Drawing.Size(198, 20)
		Me.Txt_GR_Rekap_Jumlah_Dosing.TabIndex = 422
		Me.Txt_GR_Rekap_Jumlah_Dosing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		'
		'Txt_GR_Rekap_PO
		'
		Me.Txt_GR_Rekap_PO.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_GR_Rekap_PO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_GR_Rekap_PO.Enabled = False
		Me.Txt_GR_Rekap_PO.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_GR_Rekap_PO.Location = New System.Drawing.Point(109, 9)
		Me.Txt_GR_Rekap_PO.Name = "Txt_GR_Rekap_PO"
		Me.Txt_GR_Rekap_PO.Size = New System.Drawing.Size(198, 20)
		Me.Txt_GR_Rekap_PO.TabIndex = 422
		'
		'Label18
		'
		Me.Label18.AutoSize = True
		Me.Label18.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label18.Location = New System.Drawing.Point(11, 37)
		Me.Label18.Name = "Label18"
		Me.Label18.Size = New System.Drawing.Size(93, 17)
		Me.Label18.TabIndex = 421
		Me.Label18.Text = "Jumlah Dosing"
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.Color.Red
		Me.Panel7.Location = New System.Drawing.Point(84, 121)
		Me.Panel7.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(12, 446)
		Me.Panel7.TabIndex = 413
		Me.Panel7.Visible = False
		'
		'Label13
		'
		Me.Label13.AutoSize = True
		Me.Label13.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label13.Location = New System.Drawing.Point(11, 11)
		Me.Label13.Name = "Label13"
		Me.Label13.Size = New System.Drawing.Size(44, 17)
		Me.Label13.TabIndex = 421
		Me.Label13.Text = "No PO"
		'
		'Panel11
		'
		Me.Panel11.BackColor = System.Drawing.Color.Red
		Me.Panel11.Location = New System.Drawing.Point(6, 108)
		Me.Panel11.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel11.Name = "Panel11"
		Me.Panel11.Size = New System.Drawing.Size(1179, 12)
		Me.Panel11.TabIndex = 41
		Me.Panel11.Visible = False
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.Color.Red
		Me.Panel6.Location = New System.Drawing.Point(8, 56)
		Me.Panel6.Margin = New System.Windows.Forms.Padding(5)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(1179, 12)
		Me.Panel6.TabIndex = 41
		Me.Panel6.Visible = False
		'
		'TabPage4
		'
		Me.TabPage4.BackColor = System.Drawing.Color.White
		Me.TabPage4.Controls.Add(Me.Btn_Cari_GR)
		Me.TabPage4.Controls.Add(Me.Txt_Filter_GR)
		Me.TabPage4.Controls.Add(Me.Label12)
		Me.TabPage4.Controls.Add(Me.Cmb_Filter_GR)
		Me.TabPage4.Controls.Add(Me.Btn_Cetak_GR)
		Me.TabPage4.Controls.Add(Me.Batch)
		Me.TabPage4.Location = New System.Drawing.Point(4, 27)
		Me.TabPage4.Name = "TabPage4"
		Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage4.Size = New System.Drawing.Size(1138, 499)
		Me.TabPage4.TabIndex = 1
		Me.TabPage4.Text = "Detail"
		'
		'Btn_Cari_GR
		'
		Me.Btn_Cari_GR.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Cari_GR.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Cari_GR.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Cari_GR.ForeColor = System.Drawing.Color.White
		Me.Btn_Cari_GR.Location = New System.Drawing.Point(543, 10)
		Me.Btn_Cari_GR.Name = "Btn_Cari_GR"
		Me.Btn_Cari_GR.Size = New System.Drawing.Size(80, 27)
		Me.Btn_Cari_GR.TabIndex = 421
		Me.Btn_Cari_GR.Text = "&Cari"
		Me.Btn_Cari_GR.UseVisualStyleBackColor = False
		'
		'Txt_Filter_GR
		'
		Me.Txt_Filter_GR.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Txt_Filter_GR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Txt_Filter_GR.Enabled = False
		Me.Txt_Filter_GR.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Txt_Filter_GR.Location = New System.Drawing.Point(202, 13)
		Me.Txt_Filter_GR.Name = "Txt_Filter_GR"
		Me.Txt_Filter_GR.Size = New System.Drawing.Size(336, 20)
		Me.Txt_Filter_GR.TabIndex = 420
		'
		'Label12
		'
		Me.Label12.AutoSize = True
		Me.Label12.Font = New System.Drawing.Font("Work Sans", 9.0!)
		Me.Label12.Location = New System.Drawing.Point(8, 15)
		Me.Label12.Name = "Label12"
		Me.Label12.Size = New System.Drawing.Size(39, 17)
		Me.Label12.TabIndex = 419
		Me.Label12.Text = "Filter"
		'
		'Cmb_Filter_GR
		'
		Me.Cmb_Filter_GR.BackColor = System.Drawing.Color.White
		Me.Cmb_Filter_GR.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Cmb_Filter_GR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Cmb_Filter_GR.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Cmb_Filter_GR.FormattingEnabled = True
		Me.Cmb_Filter_GR.Location = New System.Drawing.Point(56, 11)
		Me.Cmb_Filter_GR.Name = "Cmb_Filter_GR"
		Me.Cmb_Filter_GR.Size = New System.Drawing.Size(140, 24)
		Me.Cmb_Filter_GR.TabIndex = 418
		'
		'Btn_Cetak_GR
		'
		Me.Btn_Cetak_GR.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Cetak_GR.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Cetak_GR.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Cetak_GR.ForeColor = System.Drawing.Color.White
		Me.Btn_Cetak_GR.Location = New System.Drawing.Point(627, 10)
		Me.Btn_Cetak_GR.Name = "Btn_Cetak_GR"
		Me.Btn_Cetak_GR.Size = New System.Drawing.Size(85, 27)
		Me.Btn_Cetak_GR.TabIndex = 417
		Me.Btn_Cetak_GR.Text = "&Cetak"
		Me.Btn_Cetak_GR.UseVisualStyleBackColor = False
		'
		'Batch
		'
		Me.Batch.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.Batch.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.Batch.FullRowSelect = True
		Me.Batch.GridLines = True
		Me.Batch.HideSelection = False
		Me.Batch.Location = New System.Drawing.Point(3, 50)
		Me.Batch.Name = "Batch"
		Me.Batch.Size = New System.Drawing.Size(1132, 451)
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
		'DataGridViewTextBoxColumn1
		'
		DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle5
		Me.DataGridViewTextBoxColumn1.HeaderText = "Batch"
		Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
		Me.DataGridViewTextBoxColumn1.ReadOnly = True
		Me.DataGridViewTextBoxColumn1.Width = 70
		'
		'Btn_Batch_Blm_Validasi
		'
		Me.Btn_Batch_Blm_Validasi.BackColor = System.Drawing.Color.White
		Me.Btn_Batch_Blm_Validasi.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer))
		Me.Btn_Batch_Blm_Validasi.BorderRadius = 6
		Me.Btn_Batch_Blm_Validasi.BorderSize = 2
		Me.Btn_Batch_Blm_Validasi.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Batch_Blm_Validasi.DisableBackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Btn_Batch_Blm_Validasi.DisableBorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer))
		Me.Btn_Batch_Blm_Validasi.DisableForeColor = System.Drawing.Color.DarkGray
		Me.Btn_Batch_Blm_Validasi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer))
		Me.Btn_Batch_Blm_Validasi.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Batch_Blm_Validasi.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		Me.Btn_Batch_Blm_Validasi.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Batch_Blm_Validasi.ForeColor = System.Drawing.Color.Black
		Me.Btn_Batch_Blm_Validasi.HoverBackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Batch_Blm_Validasi.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Batch_Blm_Validasi.HoverForeColor = System.Drawing.Color.White
		Me.Btn_Batch_Blm_Validasi.Location = New System.Drawing.Point(382, 73)
		Me.Btn_Batch_Blm_Validasi.Name = "Btn_Batch_Blm_Validasi"
		Me.Btn_Batch_Blm_Validasi.PressedBackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
		Me.Btn_Batch_Blm_Validasi.PressedBorderColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
		Me.Btn_Batch_Blm_Validasi.PressedForeColor = System.Drawing.Color.Empty
		Me.Btn_Batch_Blm_Validasi.Size = New System.Drawing.Size(125, 29)
		Me.Btn_Batch_Blm_Validasi.TabIndex = 508
		Me.Btn_Batch_Blm_Validasi.Text = "Belum Validasi"
		Me.Btn_Batch_Blm_Validasi.UseVisualStyleBackColor = False
		'
		'Btn_Batch_Sdh_Validasi
		'
		Me.Btn_Batch_Sdh_Validasi.BackColor = System.Drawing.Color.White
		Me.Btn_Batch_Sdh_Validasi.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer))
		Me.Btn_Batch_Sdh_Validasi.BorderRadius = 6
		Me.Btn_Batch_Sdh_Validasi.BorderSize = 2
		Me.Btn_Batch_Sdh_Validasi.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Batch_Sdh_Validasi.DisableBackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Btn_Batch_Sdh_Validasi.DisableBorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer))
		Me.Btn_Batch_Sdh_Validasi.DisableForeColor = System.Drawing.Color.DarkGray
		Me.Btn_Batch_Sdh_Validasi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer))
		Me.Btn_Batch_Sdh_Validasi.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Batch_Sdh_Validasi.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		Me.Btn_Batch_Sdh_Validasi.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Batch_Sdh_Validasi.ForeColor = System.Drawing.Color.Black
		Me.Btn_Batch_Sdh_Validasi.HoverBackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Batch_Sdh_Validasi.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Batch_Sdh_Validasi.HoverForeColor = System.Drawing.Color.White
		Me.Btn_Batch_Sdh_Validasi.Location = New System.Drawing.Point(251, 73)
		Me.Btn_Batch_Sdh_Validasi.Name = "Btn_Batch_Sdh_Validasi"
		Me.Btn_Batch_Sdh_Validasi.PressedBackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
		Me.Btn_Batch_Sdh_Validasi.PressedBorderColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
		Me.Btn_Batch_Sdh_Validasi.PressedForeColor = System.Drawing.Color.Empty
		Me.Btn_Batch_Sdh_Validasi.Size = New System.Drawing.Size(125, 29)
		Me.Btn_Batch_Sdh_Validasi.TabIndex = 507
		Me.Btn_Batch_Sdh_Validasi.Text = "Sudah Validasi"
		Me.Btn_Batch_Sdh_Validasi.UseVisualStyleBackColor = False
		'
		'Btn_Batch_All
		'
		Me.Btn_Batch_All.BackColor = System.Drawing.Color.White
		Me.Btn_Batch_All.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer))
		Me.Btn_Batch_All.BorderRadius = 6
		Me.Btn_Batch_All.BorderSize = 2
		Me.Btn_Batch_All.Cursor = System.Windows.Forms.Cursors.Hand
		Me.Btn_Batch_All.DisableBackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
		Me.Btn_Batch_All.DisableBorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer))
		Me.Btn_Batch_All.DisableForeColor = System.Drawing.Color.DarkGray
		Me.Btn_Batch_All.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer))
		Me.Btn_Batch_All.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Batch_All.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		Me.Btn_Batch_All.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
		Me.Btn_Batch_All.ForeColor = System.Drawing.Color.Black
		Me.Btn_Batch_All.HoverBackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Batch_All.HoverBorderColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.Btn_Batch_All.HoverForeColor = System.Drawing.Color.White
		Me.Btn_Batch_All.Location = New System.Drawing.Point(120, 73)
		Me.Btn_Batch_All.Name = "Btn_Batch_All"
		Me.Btn_Batch_All.PressedBackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
		Me.Btn_Batch_All.PressedBorderColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
		Me.Btn_Batch_All.PressedForeColor = System.Drawing.Color.Empty
		Me.Btn_Batch_All.Size = New System.Drawing.Size(125, 29)
		Me.Btn_Batch_All.TabIndex = 506
		Me.Btn_Batch_All.Text = "All"
		Me.Btn_Batch_All.UseVisualStyleBackColor = False
		'
		'DataGridViewTextBoxColumn2
		'
		DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle2
		Me.DataGridViewTextBoxColumn2.HeaderText = "Batch"
		Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
		Me.DataGridViewTextBoxColumn2.ReadOnly = True
		Me.DataGridViewTextBoxColumn2.Width = 70
		'
		'PanelGradient1
		'
		Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
		Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
		Me.PanelGradient1.cuteTransparent1 = 100
		Me.PanelGradient1.cuteTransparent2 = 64
		Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.PanelGradient1.Location = New System.Drawing.Point(0, 52)
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
		Me.TabPage3.PerformLayout()
		CType(Me.Dgv_GR_Batch, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ContextMenuStrip1.ResumeLayout(False)
		Me.TabPage4.ResumeLayout(False)
		Me.TabPage4.PerformLayout()
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
	Friend WithEvents Btn_Cetak_GR As Button
	Friend WithEvents Label12 As Label
	Friend WithEvents Cmb_Filter_GR As ComboBox
	Friend WithEvents Txt_Filter_GR As TextBox
	Friend WithEvents Btn_Cari_GR As Button
	Friend WithEvents Txt_GR_Rekap_Result As TextBox
	Friend WithEvents Label15 As Label
	Friend WithEvents Txt_GR_Rekap_Split As TextBox
	Friend WithEvents Label14 As Label
	Friend WithEvents Txt_GR_Rekap_PO As TextBox
	Friend WithEvents Label13 As Label
	Friend WithEvents Panel6 As Panel
	Friend WithEvents PnlWeek As Panel
	Friend WithEvents LblSatuan As Label
	Friend WithEvents Dgv_GR_Batch As DataGridView
	Friend WithEvents Panel7 As Panel
	Friend WithEvents Lv_GR_Detail_Pallet As ListView
	Friend WithEvents Panel8 As Panel
	Friend WithEvents Label16 As Label
	Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
	Friend WithEvents Txt_GR_Rekap_Total As TextBox
	Friend WithEvents Label17 As Label
	Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
	Friend WithEvents Txt_GR_Rekap_Selisih As TextBox
	Friend WithEvents Label20 As Label
	Friend WithEvents Txt_GR_Rekap_Jumlah_Selesai As TextBox
	Friend WithEvents Label19 As Label
	Friend WithEvents Txt_GR_Rekap_Jumlah_Dosing As TextBox
	Friend WithEvents Label18 As Label
	Friend WithEvents Txt_GR_Rekap_Total_GR As TextBox
	Friend WithEvents Label21 As Label
	Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
	Friend WithEvents ValidasiToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents Label22 As Label
	Friend WithEvents Lv_GR_Detail_Scrap As ListView
	Friend WithEvents Panel10 As Panel
	Friend WithEvents Label23 As Label
	Friend WithEvents Txt_GR_Rekap_Total_Scrap As TextBox
	Friend WithEvents Label24 As Label
	Friend WithEvents Panel11 As Panel
	Friend WithEvents Label25 As Label
	Friend WithEvents Btn_Batch_Blm_Validasi As RoundedButton
	Friend WithEvents Btn_Batch_Sdh_Validasi As RoundedButton
	Friend WithEvents Btn_Batch_All As RoundedButton
End Class
