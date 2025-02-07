<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Display_Val_Pel_Pelunasan_Biaya_Import_By_Perusahaan_lokal
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Display_Val_Pel_Pelunasan_Biaya_Import_By_Perusahaan_lokal))
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lvDetailValPelPelunasanBiayaImportByPerusahaan = New System.Windows.Forms.ListView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lvValPelPelunasanBiayaImportByPerusahaan = New System.Windows.Forms.ListView
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CopyNoValToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CetakToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.CbTransaksi_HrIni = New System.Windows.Forms.CheckBox
        Me.btnCari = New System.Windows.Forms.Button
        Me.TxtValue = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbParamLain = New System.Windows.Forms.ComboBox
        Me.CbParamLain = New System.Windows.Forms.CheckBox
        Me.DtpAkhir = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.DtpAwal = New System.Windows.Forms.DateTimePicker
        Me.CbParamTgl = New System.Windows.Forms.CheckBox
        Me.cmbTgl = New System.Windows.Forms.ComboBox
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Maroon
        Me.Label1.Location = New System.Drawing.Point(-1, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.Label1.Size = New System.Drawing.Size(1083, 36)
        Me.Label1.TabIndex = 68
        Me.Label1.Text = "Display Pelunasan Biaya Import"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lvDetailValPelPelunasanBiayaImportByPerusahaan)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 258)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(996, 263)
        Me.GroupBox2.TabIndex = 70
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Detail Pelunasan Biaya Import"
        '
        'lvDetailValPelPelunasanBiayaImportByPerusahaan
        '
        Me.lvDetailValPelPelunasanBiayaImportByPerusahaan.FullRowSelect = True
        Me.lvDetailValPelPelunasanBiayaImportByPerusahaan.GridLines = True
        Me.lvDetailValPelPelunasanBiayaImportByPerusahaan.Location = New System.Drawing.Point(5, 18)
        Me.lvDetailValPelPelunasanBiayaImportByPerusahaan.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.lvDetailValPelPelunasanBiayaImportByPerusahaan.Name = "lvDetailValPelPelunasanBiayaImportByPerusahaan"
        Me.lvDetailValPelPelunasanBiayaImportByPerusahaan.Size = New System.Drawing.Size(986, 239)
        Me.lvDetailValPelPelunasanBiayaImportByPerusahaan.TabIndex = 13
        Me.lvDetailValPelPelunasanBiayaImportByPerusahaan.UseCompatibleStateImageBehavior = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lvValPelPelunasanBiayaImportByPerusahaan)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 39)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox1.Size = New System.Drawing.Size(996, 213)
        Me.GroupBox1.TabIndex = 69
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Pelunasan Biaya Import"
        '
        'lvValPelPelunasanBiayaImportByPerusahaan
        '
        Me.lvValPelPelunasanBiayaImportByPerusahaan.AllowDrop = True
        Me.lvValPelPelunasanBiayaImportByPerusahaan.ContextMenuStrip = Me.ContextMenuStrip1
        Me.lvValPelPelunasanBiayaImportByPerusahaan.FullRowSelect = True
        Me.lvValPelPelunasanBiayaImportByPerusahaan.GridLines = True
        Me.lvValPelPelunasanBiayaImportByPerusahaan.HideSelection = False
        Me.lvValPelPelunasanBiayaImportByPerusahaan.Location = New System.Drawing.Point(4, 19)
        Me.lvValPelPelunasanBiayaImportByPerusahaan.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.lvValPelPelunasanBiayaImportByPerusahaan.Name = "lvValPelPelunasanBiayaImportByPerusahaan"
        Me.lvValPelPelunasanBiayaImportByPerusahaan.Size = New System.Drawing.Size(987, 188)
        Me.lvValPelPelunasanBiayaImportByPerusahaan.TabIndex = 12
        Me.lvValPelPelunasanBiayaImportByPerusahaan.UseCompatibleStateImageBehavior = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyNoValToolStripMenuItem, Me.CetakToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(140, 48)
        '
        'CopyNoValToolStripMenuItem
        '
        Me.CopyNoValToolStripMenuItem.Name = "CopyNoValToolStripMenuItem"
        Me.CopyNoValToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.CopyNoValToolStripMenuItem.Text = "Copy No Val"
        '
        'CetakToolStripMenuItem
        '
        Me.CetakToolStripMenuItem.Name = "CetakToolStripMenuItem"
        Me.CetakToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.CetakToolStripMenuItem.Text = "Cetak"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.CbTransaksi_HrIni)
        Me.GroupBox3.Controls.Add(Me.btnCari)
        Me.GroupBox3.Controls.Add(Me.TxtValue)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.cmbParamLain)
        Me.GroupBox3.Controls.Add(Me.CbParamLain)
        Me.GroupBox3.Controls.Add(Me.DtpAkhir)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.DtpAwal)
        Me.GroupBox3.Controls.Add(Me.CbParamTgl)
        Me.GroupBox3.Controls.Add(Me.cmbTgl)
        Me.GroupBox3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(8, 530)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox3.Size = New System.Drawing.Size(617, 95)
        Me.GroupBox3.TabIndex = 77
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Filter Data"
        '
        'CbTransaksi_HrIni
        '
        Me.CbTransaksi_HrIni.AutoSize = True
        Me.CbTransaksi_HrIni.Location = New System.Drawing.Point(4, 20)
        Me.CbTransaksi_HrIni.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.CbTransaksi_HrIni.Name = "CbTransaksi_HrIni"
        Me.CbTransaksi_HrIni.Size = New System.Drawing.Size(108, 17)
        Me.CbTransaksi_HrIni.TabIndex = 9
        Me.CbTransaksi_HrIni.Text = "Transaksi Hari Ini"
        Me.CbTransaksi_HrIni.UseVisualStyleBackColor = True
        '
        'btnCari
        '
        Me.btnCari.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCari.Location = New System.Drawing.Point(511, 64)
        Me.btnCari.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnCari.Name = "btnCari"
        Me.btnCari.Size = New System.Drawing.Size(100, 23)
        Me.btnCari.TabIndex = 8
        Me.btnCari.Text = "&Cari"
        Me.btnCari.UseVisualStyleBackColor = True
        '
        'TxtValue
        '
        Me.TxtValue.Location = New System.Drawing.Point(337, 66)
        Me.TxtValue.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TxtValue.Name = "TxtValue"
        Me.TxtValue.Size = New System.Drawing.Size(170, 21)
        Me.TxtValue.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(299, 69)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Value"
        '
        'cmbParamLain
        '
        Me.cmbParamLain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbParamLain.FormattingEnabled = True
        Me.cmbParamLain.Location = New System.Drawing.Point(125, 66)
        Me.cmbParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.cmbParamLain.Name = "cmbParamLain"
        Me.cmbParamLain.Size = New System.Drawing.Size(169, 21)
        Me.cmbParamLain.TabIndex = 6
        '
        'CbParamLain
        '
        Me.CbParamLain.AutoSize = True
        Me.CbParamLain.Location = New System.Drawing.Point(4, 68)
        Me.CbParamLain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.CbParamLain.Name = "CbParamLain"
        Me.CbParamLain.Size = New System.Drawing.Size(98, 17)
        Me.CbParamLain.TabIndex = 5
        Me.CbParamLain.Text = "Parameter Lain"
        Me.CbParamLain.UseVisualStyleBackColor = True
        '
        'DtpAkhir
        '
        Me.DtpAkhir.CustomFormat = "dd MMMM yyyy"
        Me.DtpAkhir.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpAkhir.Location = New System.Drawing.Point(472, 39)
        Me.DtpAkhir.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DtpAkhir.Name = "DtpAkhir"
        Me.DtpAkhir.Size = New System.Drawing.Size(139, 21)
        Me.DtpAkhir.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(446, 42)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(22, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "s/d"
        '
        'DtpAwal
        '
        Me.DtpAwal.CustomFormat = "dd MMMM yyyy"
        Me.DtpAwal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpAwal.Location = New System.Drawing.Point(298, 39)
        Me.DtpAwal.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DtpAwal.Name = "DtpAwal"
        Me.DtpAwal.Size = New System.Drawing.Size(139, 21)
        Me.DtpAwal.TabIndex = 3
        '
        'CbParamTgl
        '
        Me.CbParamTgl.AutoSize = True
        Me.CbParamTgl.Location = New System.Drawing.Point(4, 43)
        Me.CbParamTgl.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.CbParamTgl.Name = "CbParamTgl"
        Me.CbParamTgl.Size = New System.Drawing.Size(117, 17)
        Me.CbParamTgl.TabIndex = 1
        Me.CbParamTgl.Text = "Parameter Tanggal"
        Me.CbParamTgl.UseVisualStyleBackColor = True
        '
        'cmbTgl
        '
        Me.cmbTgl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTgl.FormattingEnabled = True
        Me.cmbTgl.Location = New System.Drawing.Point(125, 39)
        Me.cmbTgl.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.cmbTgl.Name = "cmbTgl"
        Me.cmbTgl.Size = New System.Drawing.Size(169, 21)
        Me.cmbTgl.TabIndex = 2
        '
        'Display_Val_Pel_Pelunasan_Biaya_Import_By_Perusahaan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.ClientSize = New System.Drawing.Size(1015, 637)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Display_Val_Pel_Pelunasan_Biaya_Import_By_Perusahaan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ".:: Display Pelunasan Biaya Import ::."
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lvDetailValPelPelunasanBiayaImportByPerusahaan As System.Windows.Forms.ListView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lvValPelPelunasanBiayaImportByPerusahaan As System.Windows.Forms.ListView
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents CbTransaksi_HrIni As System.Windows.Forms.CheckBox
    Friend WithEvents btnCari As System.Windows.Forms.Button
    Friend WithEvents TxtValue As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbParamLain As System.Windows.Forms.ComboBox
    Friend WithEvents CbParamLain As System.Windows.Forms.CheckBox
    Friend WithEvents DtpAkhir As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DtpAwal As System.Windows.Forms.DateTimePicker
    Friend WithEvents CbParamTgl As System.Windows.Forms.CheckBox
    Friend WithEvents cmbTgl As System.Windows.Forms.ComboBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CopyNoValToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CetakToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
