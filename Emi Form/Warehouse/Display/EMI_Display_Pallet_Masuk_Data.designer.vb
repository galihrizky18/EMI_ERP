<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Display_Pallet_Masuk_Data

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
        Me.Lbl_Title = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Lv_BM_PerPallet = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CetakToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalinNoFakturToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Lv_BMPerPalletDetail = New System.Windows.Forms.ListView()
        Me.GroupBoxFilterData = New System.Windows.Forms.GroupBox()
        Me.Dtp_Awal = New System.Windows.Forms.DateTimePicker()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Cmb_Lokasi = New System.Windows.Forms.ComboBox()
        Me.Cb_TransaksiHrIni = New System.Windows.Forms.CheckBox()
        Me.Txt_ParamLain = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Cmb_ParamLain = New System.Windows.Forms.ComboBox()
        Me.Cb_ParamLain = New System.Windows.Forms.CheckBox()
        Me.Dtp_Akhir = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Cb_ParamTgl = New System.Windows.Forms.CheckBox()
        Me.Cmb_ParamTgl = New System.Windows.Forms.ComboBox()
        Me.PictureBoxTracking = New System.Windows.Forms.PictureBox()
        Me.PictureBoxKdBrg = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BatalkanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBoxFilterData.SuspendLayout()
        CType(Me.PictureBoxTracking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxKdBrg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Lbl_Title)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1278, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1278, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Title
        '
        Me.Lbl_Title.AutoSize = True
        Me.Lbl_Title.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Title.Location = New System.Drawing.Point(5, 9)
        Me.Lbl_Title.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Title.Name = "Lbl_Title"
        Me.Lbl_Title.Size = New System.Drawing.Size(364, 30)
        Me.Lbl_Title.TabIndex = 0
        Me.Lbl_Title.Text = "Display - Barang Masuk Per Pallet"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1280, 12)
        Me.Panel2.TabIndex = 34
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 63)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 669)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1261, 60)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 655)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(13, 518)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1444, 15)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'Lv_BM_PerPallet
        '
        Me.Lv_BM_PerPallet.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Lv_BM_PerPallet.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_BM_PerPallet.FullRowSelect = True
        Me.Lv_BM_PerPallet.GridLines = True
        Me.Lv_BM_PerPallet.HideSelection = False
        Me.Lv_BM_PerPallet.Location = New System.Drawing.Point(21, 92)
        Me.Lv_BM_PerPallet.MultiSelect = False
        Me.Lv_BM_PerPallet.Name = "Lv_BM_PerPallet"
        Me.Lv_BM_PerPallet.Size = New System.Drawing.Size(1237, 424)
        Me.Lv_BM_PerPallet.TabIndex = 234
        Me.Lv_BM_PerPallet.UseCompatibleStateImageBehavior = False
        Me.Lv_BM_PerPallet.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CetakToolStripMenuItem, Me.SalinNoFakturToolStripMenuItem, Me.BatalkanToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(181, 92)
        '
        'CetakToolStripMenuItem
        '
        Me.CetakToolStripMenuItem.Name = "CetakToolStripMenuItem"
        Me.CetakToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.CetakToolStripMenuItem.Text = "Cetak"
        '
        'SalinNoFakturToolStripMenuItem
        '
        Me.SalinNoFakturToolStripMenuItem.Name = "SalinNoFakturToolStripMenuItem"
        Me.SalinNoFakturToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.SalinNoFakturToolStripMenuItem.Text = "Salin No Faktur"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(20, 666)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1436, 15)
        Me.Panel6.TabIndex = 39
        Me.Panel6.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(1261, 323)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1436, 24)
        Me.Panel7.TabIndex = 39
        Me.Panel7.Visible = False
        '
        'Lv_BMPerPalletDetail
        '
        Me.Lv_BMPerPalletDetail.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_BMPerPalletDetail.FullRowSelect = True
        Me.Lv_BMPerPalletDetail.GridLines = True
        Me.Lv_BMPerPalletDetail.HideSelection = False
        Me.Lv_BMPerPalletDetail.Location = New System.Drawing.Point(1073, 535)
        Me.Lv_BMPerPalletDetail.Name = "Lv_BMPerPalletDetail"
        Me.Lv_BMPerPalletDetail.Size = New System.Drawing.Size(185, 126)
        Me.Lv_BMPerPalletDetail.TabIndex = 341
        Me.Lv_BMPerPalletDetail.UseCompatibleStateImageBehavior = False
        Me.Lv_BMPerPalletDetail.View = System.Windows.Forms.View.Details
        Me.Lv_BMPerPalletDetail.Visible = False
        '
        'GroupBoxFilterData
        '
        Me.GroupBoxFilterData.Controls.Add(Me.Dtp_Awal)
        Me.GroupBoxFilterData.Controls.Add(Me.Btn_Refresh)
        Me.GroupBoxFilterData.Controls.Add(Me.Btn_Cari)
        Me.GroupBoxFilterData.Controls.Add(Me.Cmb_Lokasi)
        Me.GroupBoxFilterData.Controls.Add(Me.Cb_TransaksiHrIni)
        Me.GroupBoxFilterData.Controls.Add(Me.Txt_ParamLain)
        Me.GroupBoxFilterData.Controls.Add(Me.Label6)
        Me.GroupBoxFilterData.Controls.Add(Me.Cmb_ParamLain)
        Me.GroupBoxFilterData.Controls.Add(Me.Cb_ParamLain)
        Me.GroupBoxFilterData.Controls.Add(Me.Dtp_Akhir)
        Me.GroupBoxFilterData.Controls.Add(Me.Label7)
        Me.GroupBoxFilterData.Controls.Add(Me.Cb_ParamTgl)
        Me.GroupBoxFilterData.Controls.Add(Me.Cmb_ParamTgl)
        Me.GroupBoxFilterData.Location = New System.Drawing.Point(20, 534)
        Me.GroupBoxFilterData.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBoxFilterData.Name = "GroupBoxFilterData"
        Me.GroupBoxFilterData.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBoxFilterData.Size = New System.Drawing.Size(757, 130)
        Me.GroupBoxFilterData.TabIndex = 342
        Me.GroupBoxFilterData.TabStop = False
        Me.GroupBoxFilterData.Text = "Filter Data"
        '
        'Dtp_Awal
        '
        Me.Dtp_Awal.CustomFormat = "dd MMMM yyyy"
        Me.Dtp_Awal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Dtp_Awal.Location = New System.Drawing.Point(301, 74)
        Me.Dtp_Awal.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Dtp_Awal.Name = "Dtp_Awal"
        Me.Dtp_Awal.Size = New System.Drawing.Size(158, 20)
        Me.Dtp_Awal.TabIndex = 345
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(658, 98)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(88, 27)
        Me.Btn_Refresh.TabIndex = 344
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(568, 98)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(88, 27)
        Me.Btn_Cari.TabIndex = 343
        Me.Btn_Cari.Text = "&Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Cmb_Lokasi
        '
        Me.Cmb_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Lokasi.Enabled = False
        Me.Cmb_Lokasi.FormattingEnabled = True
        Me.Cmb_Lokasi.Location = New System.Drawing.Point(8, 16)
        Me.Cmb_Lokasi.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_Lokasi.Name = "Cmb_Lokasi"
        Me.Cmb_Lokasi.Size = New System.Drawing.Size(209, 24)
        Me.Cmb_Lokasi.TabIndex = 342
        '
        'Cb_TransaksiHrIni
        '
        Me.Cb_TransaksiHrIni.AutoSize = True
        Me.Cb_TransaksiHrIni.Location = New System.Drawing.Point(8, 46)
        Me.Cb_TransaksiHrIni.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cb_TransaksiHrIni.Name = "Cb_TransaksiHrIni"
        Me.Cb_TransaksiHrIni.Size = New System.Drawing.Size(118, 20)
        Me.Cb_TransaksiHrIni.TabIndex = 9
        Me.Cb_TransaksiHrIni.Text = "Transaksi Hari Ini"
        Me.Cb_TransaksiHrIni.UseVisualStyleBackColor = True
        '
        'Txt_ParamLain
        '
        Me.Txt_ParamLain.Location = New System.Drawing.Point(337, 102)
        Me.Txt_ParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_ParamLain.Name = "Txt_ParamLain"
        Me.Txt_ParamLain.Size = New System.Drawing.Size(226, 20)
        Me.Txt_ParamLain.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(298, 103)
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
        Me.Cmb_ParamLain.Location = New System.Drawing.Point(143, 99)
        Me.Cmb_ParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_ParamLain.Name = "Cmb_ParamLain"
        Me.Cmb_ParamLain.Size = New System.Drawing.Size(147, 24)
        Me.Cmb_ParamLain.TabIndex = 6
        '
        'Cb_ParamLain
        '
        Me.Cb_ParamLain.AutoSize = True
        Me.Cb_ParamLain.Location = New System.Drawing.Point(8, 101)
        Me.Cb_ParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cb_ParamLain.Name = "Cb_ParamLain"
        Me.Cb_ParamLain.Size = New System.Drawing.Size(107, 20)
        Me.Cb_ParamLain.TabIndex = 5
        Me.Cb_ParamLain.Text = "Parameter Lain"
        Me.Cb_ParamLain.UseVisualStyleBackColor = True
        '
        'Dtp_Akhir
        '
        Me.Dtp_Akhir.CustomFormat = "dd MMMM yyyy"
        Me.Dtp_Akhir.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Dtp_Akhir.Location = New System.Drawing.Point(501, 74)
        Me.Dtp_Akhir.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Dtp_Akhir.Name = "Dtp_Akhir"
        Me.Dtp_Akhir.Size = New System.Drawing.Size(158, 20)
        Me.Dtp_Akhir.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(468, 76)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(25, 16)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "s/d"
        '
        'Cb_ParamTgl
        '
        Me.Cb_ParamTgl.AutoSize = True
        Me.Cb_ParamTgl.Location = New System.Drawing.Point(8, 72)
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
        Me.Cmb_ParamTgl.Location = New System.Drawing.Point(143, 70)
        Me.Cmb_ParamTgl.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Cmb_ParamTgl.Name = "Cmb_ParamTgl"
        Me.Cmb_ParamTgl.Size = New System.Drawing.Size(147, 24)
        Me.Cmb_ParamTgl.TabIndex = 2
        '
        'PictureBoxTracking
        '
        Me.PictureBoxTracking.Location = New System.Drawing.Point(987, 535)
        Me.PictureBoxTracking.Name = "PictureBoxTracking"
        Me.PictureBoxTracking.Size = New System.Drawing.Size(80, 72)
        Me.PictureBoxTracking.TabIndex = 345
        Me.PictureBoxTracking.TabStop = False
        Me.PictureBoxTracking.Visible = False
        '
        'PictureBoxKdBrg
        '
        Me.PictureBoxKdBrg.Location = New System.Drawing.Point(901, 535)
        Me.PictureBoxKdBrg.Name = "PictureBoxKdBrg"
        Me.PictureBoxKdBrg.Size = New System.Drawing.Size(80, 72)
        Me.PictureBoxKdBrg.TabIndex = 344
        Me.PictureBoxKdBrg.TabStop = False
        Me.PictureBoxKdBrg.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(1159, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 16)
        Me.Label2.TabIndex = 347
        Me.Label2.Text = "Pallet Dibatalkan"
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.DarkRed
        Me.Panel9.Location = New System.Drawing.Point(1138, 70)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(15, 15)
        Me.Panel9.TabIndex = 346
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Yellow
        Me.Panel8.Location = New System.Drawing.Point(1023, 70)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(15, 15)
        Me.Panel8.TabIndex = 346
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1044, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 16)
        Me.Label1.TabIndex = 347
        Me.Label1.Text = "Sudah Cetak"
        '
        'BatalkanToolStripMenuItem
        '
        Me.BatalkanToolStripMenuItem.Name = "BatalkanToolStripMenuItem"
        Me.BatalkanToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.BatalkanToolStripMenuItem.Text = "Batalkan"
        '
        'EMI_Display_Pallet_Masuk_Data
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1278, 681)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel9)
        Me.Controls.Add(Me.PictureBoxTracking)
        Me.Controls.Add(Me.PictureBoxKdBrg)
        Me.Controls.Add(Me.GroupBoxFilterData)
        Me.Controls.Add(Me.Lv_BMPerPalletDetail)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Lv_BM_PerPallet)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Display_Pallet_Masuk_Data"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBoxFilterData.ResumeLayout(False)
        Me.GroupBoxFilterData.PerformLayout()
        CType(Me.PictureBoxTracking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxKdBrg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Title As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Lv_BM_PerPallet As ListView
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Lv_BMPerPalletDetail As ListView
    Friend WithEvents GroupBoxFilterData As GroupBox
    Friend WithEvents Cmb_Lokasi As ComboBox
    Friend WithEvents Cb_TransaksiHrIni As CheckBox
    Friend WithEvents Txt_ParamLain As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Cmb_ParamLain As ComboBox
    Friend WithEvents Cb_ParamLain As CheckBox
    Friend WithEvents Dtp_Akhir As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents Cb_ParamTgl As CheckBox
    Friend WithEvents Cmb_ParamTgl As ComboBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents CetakToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PictureBoxKdBrg As PictureBox
    Friend WithEvents PictureBoxTracking As PictureBox
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents SalinNoFakturToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Dtp_Awal As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents BatalkanToolStripMenuItem As ToolStripMenuItem
End Class
