<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Transaksi_Actual_Biaya_Produksi
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
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.TxtFaktur = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtJumlah = New System.Windows.Forms.TextBox()
        Me.txtSatuan = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DtpTanggal = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbJenisBiaya = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbMesin = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnKosong = New System.Windows.Forms.Button()
        Me.cmbStockOwner = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Panel5)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(594, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(594, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1279, 49)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 416)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(359, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Transaksi - Actual Biaya Produksi"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1157, 12)
        Me.Panel2.TabIndex = 34
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Controls.Add(Me.Panel7)
        Me.Panel3.Location = New System.Drawing.Point(1, 63)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 574)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(18, 297)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1779, 15)
        Me.Panel7.TabIndex = 39
        Me.Panel7.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Controls.Add(Me.Panel8)
        Me.Panel4.Location = New System.Drawing.Point(20, 302)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1779, 15)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Red
        Me.Panel8.Location = New System.Drawing.Point(0, -64)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(1779, 15)
        Me.Panel8.TabIndex = 39
        Me.Panel8.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(575, 63)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(19, 644)
        Me.Panel6.TabIndex = 36
        Me.Panel6.Visible = False
        '
        'TxtFaktur
        '
        Me.TxtFaktur.BackColor = System.Drawing.Color.Goldenrod
        Me.TxtFaktur.Enabled = False
        Me.TxtFaktur.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFaktur.ForeColor = System.Drawing.SystemColors.Window
        Me.TxtFaktur.Location = New System.Drawing.Point(175, 70)
        Me.TxtFaktur.MaxLength = 30
        Me.TxtFaktur.Name = "TxtFaktur"
        Me.TxtFaktur.Size = New System.Drawing.Size(200, 22)
        Me.TxtFaktur.TabIndex = 402
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(34, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 20)
        Me.Label2.TabIndex = 398
        Me.Label2.Text = "No. Transaksi"
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(175, 259)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(101, 36)
        Me.Btn_Simpan.TabIndex = 416
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(34, 198)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 20)
        Me.Label4.TabIndex = 417
        Me.Label4.Text = "Jumlah"
        '
        'txtJumlah
        '
        Me.txtJumlah.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtJumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJumlah.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.txtJumlah.Location = New System.Drawing.Point(175, 196)
        Me.txtJumlah.MaxLength = 50
        Me.txtJumlah.Name = "txtJumlah"
        Me.txtJumlah.Size = New System.Drawing.Size(240, 22)
        Me.txtJumlah.TabIndex = 418
        Me.txtJumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSatuan
        '
        Me.txtSatuan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtSatuan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSatuan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.txtSatuan.Location = New System.Drawing.Point(175, 228)
        Me.txtSatuan.MaxLength = 50
        Me.txtSatuan.Name = "txtSatuan"
        Me.txtSatuan.ReadOnly = True
        Me.txtSatuan.Size = New System.Drawing.Size(240, 22)
        Me.txtSatuan.TabIndex = 420
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(34, 229)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 20)
        Me.Label5.TabIndex = 419
        Me.Label5.Text = "Satuan"
        '
        'DtpTanggal
        '
        Me.DtpTanggal.Location = New System.Drawing.Point(175, 101)
        Me.DtpTanggal.Name = "DtpTanggal"
        Me.DtpTanggal.Size = New System.Drawing.Size(200, 20)
        Me.DtpTanggal.TabIndex = 425
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(34, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 20)
        Me.Label3.TabIndex = 424
        Me.Label3.Text = "Tanggal"
        '
        'cmbJenisBiaya
        '
        Me.cmbJenisBiaya.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbJenisBiaya.FormattingEnabled = True
        Me.cmbJenisBiaya.Location = New System.Drawing.Point(175, 131)
        Me.cmbJenisBiaya.Name = "cmbJenisBiaya"
        Me.cmbJenisBiaya.Size = New System.Drawing.Size(240, 24)
        Me.cmbJenisBiaya.TabIndex = 427
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label8.Location = New System.Drawing.Point(34, 132)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(86, 20)
        Me.Label8.TabIndex = 426
        Me.Label8.Text = "Jenis Biaya"
        '
        'cmbMesin
        '
        Me.cmbMesin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMesin.FormattingEnabled = True
        Me.cmbMesin.Location = New System.Drawing.Point(175, 163)
        Me.cmbMesin.Name = "cmbMesin"
        Me.cmbMesin.Size = New System.Drawing.Size(240, 24)
        Me.cmbMesin.TabIndex = 429
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(34, 164)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 20)
        Me.Label6.TabIndex = 428
        Me.Label6.Text = "Work Center"
        '
        'btnKosong
        '
        Me.btnKosong.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.btnKosong.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnKosong.ForeColor = System.Drawing.Color.White
        Me.btnKosong.Location = New System.Drawing.Point(282, 259)
        Me.btnKosong.Name = "btnKosong"
        Me.btnKosong.Size = New System.Drawing.Size(101, 36)
        Me.btnKosong.TabIndex = 430
        Me.btnKosong.Text = "Kosong"
        Me.btnKosong.UseVisualStyleBackColor = False
        '
        'cmbStockOwner
        '
        Me.cmbStockOwner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStockOwner.Enabled = False
        Me.cmbStockOwner.FormattingEnabled = True
        Me.cmbStockOwner.Location = New System.Drawing.Point(381, 70)
        Me.cmbStockOwner.Name = "cmbStockOwner"
        Me.cmbStockOwner.Size = New System.Drawing.Size(161, 24)
        Me.cmbStockOwner.TabIndex = 431
        Me.cmbStockOwner.Visible = False
        '
        'EMI_Transaksi_Actual_Biaya_Produksi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(594, 317)
        Me.Controls.Add(Me.cmbStockOwner)
        Me.Controls.Add(Me.btnKosong)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.cmbMesin)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmbJenisBiaya)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.DtpTanggal)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.txtSatuan)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtJumlah)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.TxtFaktur)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Transaksi_Actual_Biaya_Produksi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents TxtFaktur As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents txtJumlah As TextBox
    Friend WithEvents txtSatuan As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents DtpTanggal As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbJenisBiaya As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents cmbMesin As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents btnKosong As Button
    Friend WithEvents cmbStockOwner As ComboBox
End Class
