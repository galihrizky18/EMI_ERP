<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Persentase_Budgeting_WorkCenter
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Txt_NoTransaksi = New System.Windows.Forms.TextBox()
        Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
        Me.Lbl_Kd = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Dgv_Work_Center = New System.Windows.Forms.DataGridView()
        Me.id_WorkCenter = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.work_center = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.persentase = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Btn_Release = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_TotPersen = New System.Windows.Forms.TextBox()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.Dgv_Work_Center, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Lbl_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(654, 51)
        Me.Panel1.TabIndex = 25
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(11, 9)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(523, 30)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Master Data - Persentase Budgeting Work Center"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(2, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1205, 12)
        Me.Panel2.TabIndex = 39
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(2, 59)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(12, 598)
        Me.Panel3.TabIndex = 40
        Me.Panel3.Visible = False
        '
        'Txt_NoTransaksi
        '
        Me.Txt_NoTransaksi.BackColor = System.Drawing.Color.Goldenrod
        Me.Txt_NoTransaksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_NoTransaksi.ForeColor = System.Drawing.SystemColors.Window
        Me.Txt_NoTransaksi.Location = New System.Drawing.Point(13, 59)
        Me.Txt_NoTransaksi.MaxLength = 30
        Me.Txt_NoTransaksi.Name = "Txt_NoTransaksi"
        Me.Txt_NoTransaksi.Size = New System.Drawing.Size(245, 21)
        Me.Txt_NoTransaksi.TabIndex = 391
        '
        'Txt_Keterangan
        '
        Me.Txt_Keterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Keterangan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Keterangan.Location = New System.Drawing.Point(135, 126)
        Me.Txt_Keterangan.MaxLength = 50
        Me.Txt_Keterangan.Name = "Txt_Keterangan"
        Me.Txt_Keterangan.Size = New System.Drawing.Size(399, 22)
        Me.Txt_Keterangan.TabIndex = 1
        '
        'Lbl_Kd
        '
        Me.Lbl_Kd.AutoSize = True
        Me.Lbl_Kd.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Kd.Location = New System.Drawing.Point(12, 127)
        Me.Lbl_Kd.Name = "Lbl_Kd"
        Me.Lbl_Kd.Size = New System.Drawing.Size(86, 20)
        Me.Lbl_Kd.TabIndex = 393
        Me.Lbl_Kd.Text = "Keterangan"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(135, 97)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(399, 23)
        Me.DateTimePicker1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(12, 101)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 20)
        Me.Label1.TabIndex = 393
        Me.Label1.Text = "Tanggal"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Dgv_Work_Center)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 154)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(629, 314)
        Me.GroupBox1.TabIndex = 395
        Me.GroupBox1.TabStop = False
        '
        'Dgv_Work_Center
        '
        Me.Dgv_Work_Center.AllowUserToAddRows = False
        Me.Dgv_Work_Center.AllowUserToResizeColumns = False
        Me.Dgv_Work_Center.AllowUserToResizeRows = False
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_Work_Center.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.Dgv_Work_Center.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.Dgv_Work_Center.BackgroundColor = System.Drawing.Color.White
        Me.Dgv_Work_Center.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_Work_Center.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.Dgv_Work_Center.ColumnHeadersHeight = 30
        Me.Dgv_Work_Center.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_WorkCenter, Me.work_center, Me.persentase})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_Work_Center.DefaultCellStyle = DataGridViewCellStyle5
        Me.Dgv_Work_Center.Location = New System.Drawing.Point(6, 13)
        Me.Dgv_Work_Center.Name = "Dgv_Work_Center"
        Me.Dgv_Work_Center.RowHeadersWidth = 21
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Dgv_Work_Center.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.Dgv_Work_Center.RowTemplate.Height = 30
        Me.Dgv_Work_Center.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.Dgv_Work_Center.Size = New System.Drawing.Size(616, 295)
        Me.Dgv_Work_Center.TabIndex = 394
        '
        'id_WorkCenter
        '
        Me.id_WorkCenter.HeaderText = "Id_WorkCenter"
        Me.id_WorkCenter.Name = "id_WorkCenter"
        Me.id_WorkCenter.ReadOnly = True
        Me.id_WorkCenter.Visible = False
        '
        'work_center
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.work_center.DefaultCellStyle = DataGridViewCellStyle3
        Me.work_center.HeaderText = "Work Center"
        Me.work_center.Name = "work_center"
        Me.work_center.ReadOnly = True
        Me.work_center.Width = 390
        '
        'persentase
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.persentase.DefaultCellStyle = DataGridViewCellStyle4
        Me.persentase.HeaderText = "Persentase (%)"
        Me.persentase.Name = "persentase"
        Me.persentase.Width = 200
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(642, 71)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(12, 598)
        Me.Panel4.TabIndex = 40
        Me.Panel4.Visible = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(13, 525)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(111, 33)
        Me.Btn_Simpan.TabIndex = 2
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(125, 525)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(111, 33)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "&Refresh"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Btn_Release
        '
        Me.Btn_Release.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Release.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Release.ForeColor = System.Drawing.Color.White
        Me.Btn_Release.Location = New System.Drawing.Point(238, 525)
        Me.Btn_Release.Name = "Btn_Release"
        Me.Btn_Release.Size = New System.Drawing.Size(111, 33)
        Me.Btn_Release.TabIndex = 4
        Me.Btn_Release.Text = "&Release"
        Me.Btn_Release.UseVisualStyleBackColor = False
        Me.Btn_Release.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(14, 559)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1205, 12)
        Me.Panel5.TabIndex = 39
        Me.Panel5.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(418, 475)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 20)
        Me.Label2.TabIndex = 393
        Me.Label2.Text = "Total ( % )"
        '
        'Txt_TotPersen
        '
        Me.Txt_TotPersen.BackColor = System.Drawing.Color.White
        Me.Txt_TotPersen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_TotPersen.Enabled = False
        Me.Txt_TotPersen.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_TotPersen.Location = New System.Drawing.Point(504, 474)
        Me.Txt_TotPersen.MaxLength = 50
        Me.Txt_TotPersen.Name = "Txt_TotPersen"
        Me.Txt_TotPersen.Size = New System.Drawing.Size(133, 22)
        Me.Txt_TotPersen.TabIndex = 392
        Me.Txt_TotPersen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.PanelGradient1.Size = New System.Drawing.Size(654, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'EMI_Persentase_Budgeting_WorkCenter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(654, 571)
        Me.Controls.Add(Me.Btn_Release)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Txt_TotPersen)
        Me.Controls.Add(Me.Txt_Keterangan)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Lbl_Kd)
        Me.Controls.Add(Me.Txt_NoTransaksi)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Name = "EMI_Persentase_Budgeting_WorkCenter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.Dgv_Work_Center, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Txt_NoTransaksi As TextBox
    Friend WithEvents Txt_Keterangan As TextBox
    Friend WithEvents Lbl_Kd As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Btn_Release As Button
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_TotPersen As TextBox
    Public WithEvents Dgv_Work_Center As DataGridView
    Friend WithEvents id_WorkCenter As DataGridViewTextBoxColumn
    Friend WithEvents work_center As DataGridViewTextBoxColumn
    Friend WithEvents persentase As DataGridViewTextBoxColumn
End Class
