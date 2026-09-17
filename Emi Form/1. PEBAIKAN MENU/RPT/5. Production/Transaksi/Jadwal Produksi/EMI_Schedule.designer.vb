<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Schedule
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
        Dim HourglassOptions1 As Hourglass.HourglassOptions = New Hourglass.HourglassOptions()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EMI_Schedule))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.hg = New Hourglass.HgScheduler()
        Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar()
        Me.lblLine = New System.Windows.Forms.Label()
        Me.CmbSchedule_JenisProduksi = New System.Windows.Forms.ComboBox()
        Me.Init = New System.Windows.Forms.Timer(Me.components)
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PnlDay = New System.Windows.Forms.Panel()
        Me.LblDay = New System.Windows.Forms.Label()
        Me.PnlWeek = New System.Windows.Forms.Panel()
        Me.PnlMonth = New System.Windows.Forms.Panel()
        Me.LblMonth = New System.Windows.Forms.Label()
        Me.LblWeek = New System.Windows.Forms.Label()
        Me.DgvSimulasi_DataHPP = New System.Windows.Forms.DataGridView()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvSimulasi_DataHPP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel8)
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1437, 51)
        Me.Panel1.TabIndex = 22
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Location = New System.Drawing.Point(787, 0)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(19, 489)
        Me.Panel8.TabIndex = 36
        Me.Panel8.Visible = False
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1437, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(285, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Transaksi - Hasil Produksi"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 12)
        Me.Panel2.TabIndex = 34
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 63)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 489)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1418, 136)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 471)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(299, 111)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Refresh.TabIndex = 381
        Me.Btn_Refresh.Text = "&Simpan"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        Me.Btn_Refresh.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(1, 821)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1072, 12)
        Me.Panel7.TabIndex = 37
        Me.Panel7.Visible = False
        '
        'hg
        '
        Me.hg.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.hg.BackColor = System.Drawing.Color.White
        Me.hg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.hg.Location = New System.Drawing.Point(377, 94)
        Me.hg.Margin = New System.Windows.Forms.Padding(4)
        Me.hg.Name = "hg"
        HourglassOptions1.EventArrangement = Nothing
        HourglassOptions1.EventClickHandling = Nothing
        HourglassOptions1.EventDeleteHandling = Nothing
        HourglassOptions1.EventDoubleClickHandling = Hourglass.EventClickHandlings.Enabled
        HourglassOptions1.HeaderDateFormat = "ddd, dd MMM"
        HourglassOptions1.StartDate = New Date(2022, 2, 23, 17, 26, 5, 59)
        HourglassOptions1.ViewType = Hourglass.ViewTypes.Week
        Me.hg.Options = HourglassOptions1
        Me.hg.Size = New System.Drawing.Size(1040, 726)
        Me.hg.TabIndex = 382
        Me.hg.Theme = resources.GetString("hg.Theme")
        '
        'MonthCalendar1
        '
        Me.MonthCalendar1.BackColor = System.Drawing.SystemColors.Window
        Me.MonthCalendar1.Font = New System.Drawing.Font("Work Sans ExtraBold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MonthCalendar1.ForeColor = System.Drawing.SystemColors.MenuText
        Me.MonthCalendar1.Location = New System.Drawing.Point(72, 70)
        Me.MonthCalendar1.Name = "MonthCalendar1"
        Me.MonthCalendar1.TabIndex = 383
        Me.MonthCalendar1.TitleBackColor = System.Drawing.Color.Yellow
        Me.MonthCalendar1.TitleForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.MonthCalendar1.TrailingForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        '
        'lblLine
        '
        Me.lblLine.AutoSize = True
        Me.lblLine.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.lblLine.Location = New System.Drawing.Point(22, 241)
        Me.lblLine.Name = "lblLine"
        Me.lblLine.Size = New System.Drawing.Size(110, 20)
        Me.lblLine.TabIndex = 411
        Me.lblLine.Text = "Jenis Produksi"
        '
        'CmbSchedule_JenisProduksi
        '
        Me.CmbSchedule_JenisProduksi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CmbSchedule_JenisProduksi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSchedule_JenisProduksi.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.CmbSchedule_JenisProduksi.FormattingEnabled = True
        Me.CmbSchedule_JenisProduksi.Location = New System.Drawing.Point(145, 239)
        Me.CmbSchedule_JenisProduksi.Name = "CmbSchedule_JenisProduksi"
        Me.CmbSchedule_JenisProduksi.Size = New System.Drawing.Size(225, 25)
        Me.CmbSchedule_JenisProduksi.TabIndex = 410
        '
        'Init
        '
        Me.Init.Interval = 500
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Work Sans", 8.0!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeight = 4
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3})
        Me.DataGridView1.Location = New System.Drawing.Point(20, 270)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Work Sans", 8.0!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowHeadersWidth = 21
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DataGridView1.Size = New System.Drawing.Size(350, 100)
        Me.DataGridView1.TabIndex = 413
        '
        'Column1
        '
        Me.Column1.HeaderText = ""
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 35
        '
        'Column2
        '
        Me.Column2.HeaderText = ""
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 257
        '
        'Column3
        '
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(20)
        Me.Column3.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column3.HeaderText = ""
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 57
        '
        'PnlDay
        '
        Me.PnlDay.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.PnlDay.Location = New System.Drawing.Point(386, 90)
        Me.PnlDay.Name = "PnlDay"
        Me.PnlDay.Size = New System.Drawing.Size(60, 2)
        Me.PnlDay.TabIndex = 415
        '
        'LblDay
        '
        Me.LblDay.AutoSize = True
        Me.LblDay.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LblDay.Font = New System.Drawing.Font("Work Sans", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDay.ForeColor = System.Drawing.Color.Gray
        Me.LblDay.Location = New System.Drawing.Point(397, 64)
        Me.LblDay.Name = "LblDay"
        Me.LblDay.Size = New System.Drawing.Size(39, 23)
        Me.LblDay.TabIndex = 414
        Me.LblDay.Text = "Day"
        Me.LblDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PnlWeek
        '
        Me.PnlWeek.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.PnlWeek.Location = New System.Drawing.Point(448, 90)
        Me.PnlWeek.Name = "PnlWeek"
        Me.PnlWeek.Size = New System.Drawing.Size(60, 2)
        Me.PnlWeek.TabIndex = 416
        '
        'PnlMonth
        '
        Me.PnlMonth.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.PnlMonth.Location = New System.Drawing.Point(512, 90)
        Me.PnlMonth.Name = "PnlMonth"
        Me.PnlMonth.Size = New System.Drawing.Size(60, 2)
        Me.PnlMonth.TabIndex = 417
        '
        'LblMonth
        '
        Me.LblMonth.AutoSize = True
        Me.LblMonth.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LblMonth.Font = New System.Drawing.Font("Work Sans", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMonth.ForeColor = System.Drawing.Color.Gray
        Me.LblMonth.Location = New System.Drawing.Point(512, 64)
        Me.LblMonth.Name = "LblMonth"
        Me.LblMonth.Size = New System.Drawing.Size(61, 23)
        Me.LblMonth.TabIndex = 418
        Me.LblMonth.Text = "Month"
        Me.LblMonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblWeek
        '
        Me.LblWeek.AutoSize = True
        Me.LblWeek.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LblWeek.Font = New System.Drawing.Font("Work Sans", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblWeek.ForeColor = System.Drawing.Color.Gray
        Me.LblWeek.Location = New System.Drawing.Point(453, 64)
        Me.LblWeek.Name = "LblWeek"
        Me.LblWeek.Size = New System.Drawing.Size(51, 23)
        Me.LblWeek.TabIndex = 419
        Me.LblWeek.Text = "Week"
        Me.LblWeek.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DgvSimulasi_DataHPP
        '
        Me.DgvSimulasi_DataHPP.AllowUserToAddRows = False
        Me.DgvSimulasi_DataHPP.AllowUserToDeleteRows = False
        Me.DgvSimulasi_DataHPP.AllowUserToResizeColumns = False
        Me.DgvSimulasi_DataHPP.AllowUserToResizeRows = False
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvSimulasi_DataHPP.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DgvSimulasi_DataHPP.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DgvSimulasi_DataHPP.BackgroundColor = System.Drawing.Color.White
        Me.DgvSimulasi_DataHPP.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Work Sans", 8.0!)
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvSimulasi_DataHPP.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DgvSimulasi_DataHPP.ColumnHeadersHeight = 35
        Me.DgvSimulasi_DataHPP.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column4, Me.Column5})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Work Sans", 8.0!)
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvSimulasi_DataHPP.DefaultCellStyle = DataGridViewCellStyle8
        Me.DgvSimulasi_DataHPP.Location = New System.Drawing.Point(20, 376)
        Me.DgvSimulasi_DataHPP.MultiSelect = False
        Me.DgvSimulasi_DataHPP.Name = "DgvSimulasi_DataHPP"
        Me.DgvSimulasi_DataHPP.RowHeadersWidth = 21
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvSimulasi_DataHPP.RowsDefaultCellStyle = DataGridViewCellStyle9
        Me.DgvSimulasi_DataHPP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvSimulasi_DataHPP.Size = New System.Drawing.Size(350, 438)
        Me.DgvSimulasi_DataHPP.TabIndex = 454
        '
        'Column4
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle6
        Me.Column4.HeaderText = "No Rencana"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 120
        '
        'Column5
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column5.DefaultCellStyle = DataGridViewCellStyle7
        Me.Column5.HeaderText = "Keterangan"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 208
        '
        'EMI_Schedule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1437, 833)
        Me.Controls.Add(Me.DgvSimulasi_DataHPP)
        Me.Controls.Add(Me.LblWeek)
        Me.Controls.Add(Me.LblMonth)
        Me.Controls.Add(Me.PnlMonth)
        Me.Controls.Add(Me.PnlWeek)
        Me.Controls.Add(Me.PnlDay)
        Me.Controls.Add(Me.LblDay)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.lblLine)
        Me.Controls.Add(Me.CmbSchedule_JenisProduksi)
        Me.Controls.Add(Me.MonthCalendar1)
        Me.Controls.Add(Me.hg)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Schedule"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvSimulasi_DataHPP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel8 As Panel
    Private WithEvents hg As Hourglass.HgScheduler
    Friend WithEvents MonthCalendar1 As MonthCalendar
    Friend WithEvents lblLine As Label
    Friend WithEvents CmbSchedule_JenisProduksi As ComboBox
    Private WithEvents Init As Timer
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents PnlDay As Panel
    Friend WithEvents LblDay As Label
    Friend WithEvents PnlWeek As Panel
    Friend WithEvents PnlMonth As Panel
    Friend WithEvents LblMonth As Label
    Friend WithEvents LblWeek As Label
    Friend WithEvents Column1 As DataGridViewCheckBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents DgvSimulasi_DataHPP As DataGridView
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
End Class
