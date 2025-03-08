<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EMI_Master_Meteran
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Txt_Kode = New System.Windows.Forms.TextBox()
        Me.Lbl_Kd = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txt_NoMeteran = New System.Windows.Forms.TextBox()
        Me.Cmb_JenisBiaya = New System.Windows.Forms.ComboBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Cmb_Satuan = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Lv_Data = New System.Windows.Forms.ListView()
        Me.Cmb_Filter = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Txt_Filter = New System.Windows.Forms.TextBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Id_meteranUpdate = New System.Windows.Forms.TextBox()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(707, 51)
        Me.Panel1.TabIndex = 24
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(11, 9)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(248, 30)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Master Data - Meteran"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 53)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(12, 598)
        Me.Panel3.TabIndex = 37
        Me.Panel3.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(13, 53)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1205, 12)
        Me.Panel2.TabIndex = 38
        Me.Panel2.Visible = False
        '
        'Txt_Kode
        '
        Me.Txt_Kode.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Kode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Kode.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Kode.Location = New System.Drawing.Point(135, 68)
        Me.Txt_Kode.MaxLength = 50
        Me.Txt_Kode.Name = "Txt_Kode"
        Me.Txt_Kode.Size = New System.Drawing.Size(399, 22)
        Me.Txt_Kode.TabIndex = 0
        '
        'Lbl_Kd
        '
        Me.Lbl_Kd.AutoSize = True
        Me.Lbl_Kd.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Kd.Location = New System.Drawing.Point(12, 69)
        Me.Lbl_Kd.Name = "Lbl_Kd"
        Me.Lbl_Kd.Size = New System.Drawing.Size(42, 20)
        Me.Lbl_Kd.TabIndex = 231
        Me.Lbl_Kd.Text = "Kode"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(12, 97)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 20)
        Me.Label1.TabIndex = 231
        Me.Label1.Text = "Keterangan"
        '
        'Txt_Keterangan
        '
        Me.Txt_Keterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Keterangan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Keterangan.Location = New System.Drawing.Point(135, 96)
        Me.Txt_Keterangan.MaxLength = 50
        Me.Txt_Keterangan.Name = "Txt_Keterangan"
        Me.Txt_Keterangan.Size = New System.Drawing.Size(399, 22)
        Me.Txt_Keterangan.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(12, 127)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 20)
        Me.Label2.TabIndex = 231
        Me.Label2.Text = "Jenis Biaya"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(12, 157)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 20)
        Me.Label3.TabIndex = 231
        Me.Label3.Text = "No Meteran"
        '
        'Txt_NoMeteran
        '
        Me.Txt_NoMeteran.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_NoMeteran.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NoMeteran.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_NoMeteran.Location = New System.Drawing.Point(135, 156)
        Me.Txt_NoMeteran.MaxLength = 50
        Me.Txt_NoMeteran.Name = "Txt_NoMeteran"
        Me.Txt_NoMeteran.Size = New System.Drawing.Size(300, 22)
        Me.Txt_NoMeteran.TabIndex = 3
        Me.Txt_NoMeteran.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Cmb_JenisBiaya
        '
        Me.Cmb_JenisBiaya.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_JenisBiaya.FormattingEnabled = True
        Me.Cmb_JenisBiaya.Location = New System.Drawing.Point(135, 124)
        Me.Cmb_JenisBiaya.Name = "Cmb_JenisBiaya"
        Me.Cmb_JenisBiaya.Size = New System.Drawing.Size(399, 26)
        Me.Cmb_JenisBiaya.TabIndex = 2
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(696, 73)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(12, 598)
        Me.Panel4.TabIndex = 37
        Me.Panel4.Visible = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(135, 184)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(101, 36)
        Me.Btn_Simpan.TabIndex = 5
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(236, 184)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(101, 36)
        Me.Btn_Refresh.TabIndex = 6
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(16, 591)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1205, 12)
        Me.Panel5.TabIndex = 38
        Me.Panel5.Visible = False
        '
        'Cmb_Satuan
        '
        Me.Cmb_Satuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Satuan.FormattingEnabled = True
        Me.Cmb_Satuan.Location = New System.Drawing.Point(439, 154)
        Me.Cmb_Satuan.Name = "Cmb_Satuan"
        Me.Cmb_Satuan.Size = New System.Drawing.Size(95, 26)
        Me.Cmb_Satuan.TabIndex = 4
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Lv_Data)
        Me.GroupBox1.Controls.Add(Me.Cmb_Filter)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Txt_Filter)
        Me.GroupBox1.Controls.Add(Me.Btn_Cari)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 226)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(676, 363)
        Me.GroupBox1.TabIndex = 232
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Data"
        '
        'Lv_Data
        '
        Me.Lv_Data.FullRowSelect = True
        Me.Lv_Data.GridLines = True
        Me.Lv_Data.HideSelection = False
        Me.Lv_Data.Location = New System.Drawing.Point(6, 54)
        Me.Lv_Data.Name = "Lv_Data"
        Me.Lv_Data.Size = New System.Drawing.Size(667, 304)
        Me.Lv_Data.TabIndex = 232
        Me.Lv_Data.UseCompatibleStateImageBehavior = False
        Me.Lv_Data.View = System.Windows.Forms.View.Details
        '
        'Cmb_Filter
        '
        Me.Cmb_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Filter.FormattingEnabled = True
        Me.Cmb_Filter.Location = New System.Drawing.Point(119, 22)
        Me.Cmb_Filter.Name = "Cmb_Filter"
        Me.Cmb_Filter.Size = New System.Drawing.Size(110, 26)
        Me.Cmb_Filter.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(6, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 20)
        Me.Label4.TabIndex = 231
        Me.Label4.Text = "Filter"
        '
        'Txt_Filter
        '
        Me.Txt_Filter.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Filter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Filter.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Filter.Location = New System.Drawing.Point(232, 25)
        Me.Txt_Filter.MaxLength = 50
        Me.Txt_Filter.Name = "Txt_Filter"
        Me.Txt_Filter.Size = New System.Drawing.Size(286, 22)
        Me.Txt_Filter.TabIndex = 3
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(524, 18)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(71, 30)
        Me.Btn_Cari.TabIndex = 5
        Me.Btn_Cari.Text = "&Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Id_meteranUpdate
        '
        Me.Id_meteranUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Id_meteranUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Id_meteranUpdate.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Id_meteranUpdate.Location = New System.Drawing.Point(648, 72)
        Me.Id_meteranUpdate.MaxLength = 50
        Me.Id_meteranUpdate.Name = "Id_meteranUpdate"
        Me.Id_meteranUpdate.Size = New System.Drawing.Size(47, 22)
        Me.Id_meteranUpdate.TabIndex = 3
        Me.Id_meteranUpdate.Visible = False
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
        Me.PanelGradient1.Size = New System.Drawing.Size(707, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'EMI_Master_Meteran
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(707, 601)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Cmb_Satuan)
        Me.Controls.Add(Me.Cmb_JenisBiaya)
        Me.Controls.Add(Me.Id_meteranUpdate)
        Me.Controls.Add(Me.Txt_NoMeteran)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Txt_Keterangan)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Txt_Kode)
        Me.Controls.Add(Me.Lbl_Kd)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Master_Meteran"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Txt_Kode As TextBox
    Friend WithEvents Lbl_Kd As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_Keterangan As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Txt_NoMeteran As TextBox
    Friend WithEvents Cmb_JenisBiaya As ComboBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Cmb_Satuan As ComboBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Cmb_Filter As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Txt_Filter As TextBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Lv_Data As ListView
    Friend WithEvents Id_meteranUpdate As TextBox
End Class
