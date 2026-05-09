<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class N_EMI_Production_Schedule
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(N_EMI_Production_Schedule))
		Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.Panel8 = New System.Windows.Forms.Panel()
		Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.hg = New Hourglass.HgScheduler()
		Me.Init = New System.Windows.Forms.Timer(Me.components)
		Me.PnlDay = New System.Windows.Forms.Panel()
		Me.LblDay = New System.Windows.Forms.Label()
		Me.PnlWeek = New System.Windows.Forms.Panel()
		Me.PnlMonth = New System.Windows.Forms.Panel()
		Me.LblMonth = New System.Windows.Forms.Label()
		Me.LblWeek = New System.Windows.Forms.Label()
		Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar()
		Me.DataGridView1 = New System.Windows.Forms.DataGridView()
		Me.Column13 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
		Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Warna = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Panel1.SuspendLayout()
		CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Panel4.SuspendLayout()
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
		Me.Panel1.Size = New System.Drawing.Size(1440, 51)
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
		Me.PanelGradient1.Size = New System.Drawing.Size(1440, 2)
		Me.PanelGradient1.TabIndex = 22
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(15, 11)
		Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(404, 30)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Transaksi - Schedule Production Plan"
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
		Me.hg.AutoValidate = System.Windows.Forms.AutoValidate.Disable
		Me.hg.BackColor = System.Drawing.Color.White
		Me.hg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
		Me.hg.Cursor = System.Windows.Forms.Cursors.Arrow
		Me.hg.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.hg.Location = New System.Drawing.Point(424, 107)
		Me.hg.Margin = New System.Windows.Forms.Padding(4)
		Me.hg.Name = "hg"
		HourglassOptions1.EventArrangement = Nothing
		HourglassOptions1.EventClickHandling = Hourglass.EventClickHandlings.Enabled
		HourglassOptions1.EventDeleteHandling = Nothing
		HourglassOptions1.EventDoubleClickHandling = Hourglass.EventClickHandlings.Enabled
		HourglassOptions1.HeaderDateFormat = "ddd, dd MMM"
		HourglassOptions1.StartDate = New Date(2026, 4, 8, 0, 0, 0, 0)
		HourglassOptions1.ViewType = Hourglass.ViewTypes.Month
		Me.hg.Options = HourglassOptions1
		Me.hg.Size = New System.Drawing.Size(986, 629)
		Me.hg.TabIndex = 382
		Me.hg.Theme = resources.GetString("hg.Theme")
		'
		'Init
		'
		Me.Init.Interval = 500
		'
		'PnlDay
		'
		Me.PnlDay.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.PnlDay.Location = New System.Drawing.Point(438, 98)
		Me.PnlDay.Name = "PnlDay"
		Me.PnlDay.Size = New System.Drawing.Size(60, 2)
		Me.PnlDay.TabIndex = 415
		Me.PnlDay.Visible = False
		'
		'LblDay
		'
		Me.LblDay.AutoSize = True
		Me.LblDay.Cursor = System.Windows.Forms.Cursors.WaitCursor
		Me.LblDay.Font = New System.Drawing.Font("Work Sans", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LblDay.ForeColor = System.Drawing.Color.Gray
		Me.LblDay.Location = New System.Drawing.Point(449, 67)
		Me.LblDay.Name = "LblDay"
		Me.LblDay.Size = New System.Drawing.Size(39, 23)
		Me.LblDay.TabIndex = 414
		Me.LblDay.Text = "Day"
		Me.LblDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'PnlWeek
		'
		Me.PnlWeek.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.PnlWeek.Location = New System.Drawing.Point(504, 98)
		Me.PnlWeek.Name = "PnlWeek"
		Me.PnlWeek.Size = New System.Drawing.Size(60, 2)
		Me.PnlWeek.TabIndex = 416
		Me.PnlWeek.Visible = False
		'
		'PnlMonth
		'
		Me.PnlMonth.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
		Me.PnlMonth.Location = New System.Drawing.Point(570, 98)
		Me.PnlMonth.Name = "PnlMonth"
		Me.PnlMonth.Size = New System.Drawing.Size(60, 2)
		Me.PnlMonth.TabIndex = 417
		Me.PnlMonth.Visible = False
		'
		'LblMonth
		'
		Me.LblMonth.AutoSize = True
		Me.LblMonth.Cursor = System.Windows.Forms.Cursors.WaitCursor
		Me.LblMonth.Font = New System.Drawing.Font("Work Sans", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LblMonth.ForeColor = System.Drawing.Color.Gray
		Me.LblMonth.Location = New System.Drawing.Point(564, 67)
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
		Me.LblWeek.Location = New System.Drawing.Point(505, 67)
		Me.LblWeek.Name = "LblWeek"
		Me.LblWeek.Size = New System.Drawing.Size(51, 23)
		Me.LblWeek.TabIndex = 419
		Me.LblWeek.Text = "Week"
		Me.LblWeek.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'MonthCalendar1
		'
		Me.MonthCalendar1.Location = New System.Drawing.Point(102, 76)
		Me.MonthCalendar1.Name = "MonthCalendar1"
		Me.MonthCalendar1.TabIndex = 420
		'
		'DataGridView1
		'
		Me.DataGridView1.AllowUserToAddRows = False
		Me.DataGridView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
		DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle1.Font = New System.Drawing.Font("Work Sans", 8.0!)
		DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
		Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column13, Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Warna})
		Me.DataGridView1.Cursor = System.Windows.Forms.Cursors.Default
		Me.DataGridView1.Location = New System.Drawing.Point(27, 250)
		Me.DataGridView1.Name = "DataGridView1"
		Me.DataGridView1.RowHeadersWidth = 15
		Me.DataGridView1.Size = New System.Drawing.Size(392, 479)
		Me.DataGridView1.TabIndex = 421
		'
		'Column13
		'
		DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 3.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle2.NullValue = False
		Me.Column13.DefaultCellStyle = DataGridViewCellStyle2
		Me.Column13.Frozen = True
		Me.Column13.HeaderText = "#"
		Me.Column13.MinimumWidth = 6
		Me.Column13.Name = "Column13"
		Me.Column13.Width = 20
		'
		'Column1
		'
		DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		Me.Column1.DefaultCellStyle = DataGridViewCellStyle3
		Me.Column1.Frozen = True
		Me.Column1.HeaderText = "Kode Barang"
		Me.Column1.MinimumWidth = 6
		Me.Column1.Name = "Column1"
		Me.Column1.ReadOnly = True
		Me.Column1.Visible = False
		Me.Column1.Width = 110
		'
		'Column2
		'
		Me.Column2.HeaderText = "Nama"
		Me.Column2.Name = "Column2"
		Me.Column2.ReadOnly = True
		'
		'Column3
		'
		Me.Column3.HeaderText = "Jumlah Perbulan"
		Me.Column3.Name = "Column3"
		Me.Column3.ReadOnly = True
		'
		'Column4
		'
		Me.Column4.HeaderText = "Terpakai"
		Me.Column4.Name = "Column4"
		Me.Column4.ReadOnly = True
		'
		'Warna
		'
		Me.Warna.HeaderText = "Warna"
		Me.Warna.Name = "Warna"
		Me.Warna.ReadOnly = True
		Me.Warna.Width = 50
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Location = New System.Drawing.Point(5, 5)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(247, 14)
		Me.Label3.TabIndex = 429
		Me.Label3.Text = "* Double Klik pada item untuk melihat detail"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Location = New System.Drawing.Point(261, 5)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(336, 14)
		Me.Label2.TabIndex = 430
		Me.Label2.Text = "* Klik Pada Kolom Kosong Kalender Untuk Tambah Schedule"
		'
		'Panel4
		'
		Me.Panel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Panel4.Controls.Add(Me.Label3)
		Me.Panel4.Controls.Add(Me.Label2)
		Me.Panel4.Location = New System.Drawing.Point(806, 67)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(604, 23)
		Me.Panel4.TabIndex = 431
		'
		'DataGridViewTextBoxColumn1
		'
		DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle4
		Me.DataGridViewTextBoxColumn1.Frozen = True
		Me.DataGridViewTextBoxColumn1.HeaderText = "Kode Barang"
		Me.DataGridViewTextBoxColumn1.MinimumWidth = 6
		Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
		Me.DataGridViewTextBoxColumn1.ReadOnly = True
		Me.DataGridViewTextBoxColumn1.Visible = False
		Me.DataGridViewTextBoxColumn1.Width = 110
		'
		'DataGridViewTextBoxColumn2
		'
		Me.DataGridViewTextBoxColumn2.HeaderText = "Nama"
		Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
		Me.DataGridViewTextBoxColumn2.ReadOnly = True
		Me.DataGridViewTextBoxColumn2.Width = 150
		'
		'DataGridViewTextBoxColumn3
		'
		Me.DataGridViewTextBoxColumn3.HeaderText = "Jumlah Perbulan"
		Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
		Me.DataGridViewTextBoxColumn3.ReadOnly = True
		'
		'DataGridViewTextBoxColumn4
		'
		Me.DataGridViewTextBoxColumn4.HeaderText = "Terpakai"
		Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
		Me.DataGridViewTextBoxColumn4.ReadOnly = True
		'
		'DataGridViewTextBoxColumn5
		'
		Me.DataGridViewTextBoxColumn5.HeaderText = "Warna"
		Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
		Me.DataGridViewTextBoxColumn5.ReadOnly = True
		Me.DataGridViewTextBoxColumn5.Width = 50
		'
		'N_EMI_Production_Schedule
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(1440, 833)
		Me.Controls.Add(Me.Panel4)
		Me.Controls.Add(Me.DataGridView1)
		Me.Controls.Add(Me.MonthCalendar1)
		Me.Controls.Add(Me.LblWeek)
		Me.Controls.Add(Me.LblMonth)
		Me.Controls.Add(Me.PnlMonth)
		Me.Controls.Add(Me.PnlWeek)
		Me.Controls.Add(Me.PnlDay)
		Me.Controls.Add(Me.LblDay)
		Me.Controls.Add(Me.hg)
		Me.Controls.Add(Me.Panel7)
		Me.Controls.Add(Me.Panel5)
		Me.Controls.Add(Me.Panel3)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.Panel1)
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.Name = "N_EMI_Production_Schedule"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Panel4.ResumeLayout(False)
		Me.Panel4.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel8 As Panel
    Private WithEvents hg As Hourglass.HgScheduler
    Private WithEvents Init As Timer
    Friend WithEvents PnlDay As Panel
    Friend WithEvents LblDay As Label
    Friend WithEvents PnlWeek As Panel
    Friend WithEvents PnlMonth As Panel
    Friend WithEvents LblMonth As Label
    Friend WithEvents LblWeek As Label
    Friend WithEvents MonthCalendar1 As MonthCalendar
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents Column13 As DataGridViewCheckBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Warna As DataGridViewTextBoxColumn
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel4 As Panel
End Class
