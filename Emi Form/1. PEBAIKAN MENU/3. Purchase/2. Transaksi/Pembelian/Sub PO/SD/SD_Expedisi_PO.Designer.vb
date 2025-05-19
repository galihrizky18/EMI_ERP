<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SD_Expedisi_PO
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
        Me.LblPO_Judul = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Lv_BiayaLokal = New System.Windows.Forms.ListView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Lv_DataInput = New System.Windows.Forms.ListView()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.BtnPO_Simpan = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.LblPO_TotalMUA = New System.Windows.Forms.Label()
        Me.Txt_TotTarif = New System.Windows.Forms.TextBox()
        Me.Txt_TotBiayaLain = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Txt_GrandTotal = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CmbFilter_KodeBiaya = New System.Windows.Forms.ComboBox()
        Me.CmbFilter_Lokasi = New System.Windows.Forms.ComboBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Txt_ValueKeterangan = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.LblPO_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1184, 51)
        Me.Panel1.TabIndex = 2
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1184, 2)
        Me.PanelGradient1.TabIndex = 0
        '
        'LblPO_Judul
        '
        Me.LblPO_Judul.AutoSize = True
        Me.LblPO_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPO_Judul.Location = New System.Drawing.Point(21, 12)
        Me.LblPO_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LblPO_Judul.Name = "LblPO_Judul"
        Me.LblPO_Judul.Size = New System.Drawing.Size(141, 25)
        Me.LblPO_Judul.TabIndex = 0
        Me.LblPO_Judul.Text = "Expedisi PO"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(0, 52)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1387, 14)
        Me.Panel5.TabIndex = 304
        Me.Panel5.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 65)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(14, 730)
        Me.Panel3.TabIndex = 305
        Me.Panel3.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Lv_BiayaLokal)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 104)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1158, 230)
        Me.GroupBox1.TabIndex = 306
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Biaya Lokal"
        '
        'Lv_BiayaLokal
        '
        Me.Lv_BiayaLokal.BackColor = System.Drawing.Color.White
        Me.Lv_BiayaLokal.FullRowSelect = True
        Me.Lv_BiayaLokal.GridLines = True
        Me.Lv_BiayaLokal.HideSelection = False
        Me.Lv_BiayaLokal.Location = New System.Drawing.Point(7, 22)
        Me.Lv_BiayaLokal.Name = "Lv_BiayaLokal"
        Me.Lv_BiayaLokal.Size = New System.Drawing.Size(1144, 200)
        Me.Lv_BiayaLokal.TabIndex = 0
        Me.Lv_BiayaLokal.UseCompatibleStateImageBehavior = False
        Me.Lv_BiayaLokal.View = System.Windows.Forms.View.Details
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(1170, 72)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(14, 730)
        Me.Panel2.TabIndex = 305
        Me.Panel2.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(20, 688)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1387, 14)
        Me.Panel4.TabIndex = 304
        Me.Panel4.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(15, 340)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1158, 230)
        Me.GroupBox2.TabIndex = 306
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Data Terinput"
        '
        'Lv_DataInput
        '
        Me.Lv_DataInput.BackColor = System.Drawing.Color.White
        Me.Lv_DataInput.FullRowSelect = True
        Me.Lv_DataInput.GridLines = True
        Me.Lv_DataInput.HideSelection = False
        Me.Lv_DataInput.Location = New System.Drawing.Point(27, 364)
        Me.Lv_DataInput.Name = "Lv_DataInput"
        Me.Lv_DataInput.Size = New System.Drawing.Size(1144, 200)
        Me.Lv_DataInput.TabIndex = 1
        Me.Lv_DataInput.UseCompatibleStateImageBehavior = False
        Me.Lv_DataInput.View = System.Windows.Forms.View.Details
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(20, 630)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1387, 14)
        Me.Panel6.TabIndex = 304
        Me.Panel6.Visible = False
        '
        'BtnPO_Simpan
        '
        Me.BtnPO_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnPO_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnPO_Simpan.ForeColor = System.Drawing.Color.White
        Me.BtnPO_Simpan.Location = New System.Drawing.Point(20, 646)
        Me.BtnPO_Simpan.Name = "BtnPO_Simpan"
        Me.BtnPO_Simpan.Size = New System.Drawing.Size(108, 43)
        Me.BtnPO_Simpan.TabIndex = 348
        Me.BtnPO_Simpan.Text = "&Simpan"
        Me.BtnPO_Simpan.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(131, 646)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(108, 43)
        Me.Button1.TabIndex = 348
        Me.Button1.Text = "&Refresh"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'LblPO_TotalMUA
        '
        Me.LblPO_TotalMUA.AutoSize = True
        Me.LblPO_TotalMUA.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblPO_TotalMUA.Location = New System.Drawing.Point(828, 578)
        Me.LblPO_TotalMUA.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LblPO_TotalMUA.Name = "LblPO_TotalMUA"
        Me.LblPO_TotalMUA.Size = New System.Drawing.Size(77, 20)
        Me.LblPO_TotalMUA.TabIndex = 350
        Me.LblPO_TotalMUA.Text = "Total Tarif"
        '
        'Txt_TotTarif
        '
        Me.Txt_TotTarif.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_TotTarif.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_TotTarif.Enabled = False
        Me.Txt_TotTarif.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Txt_TotTarif.Location = New System.Drawing.Point(936, 576)
        Me.Txt_TotTarif.Name = "Txt_TotTarif"
        Me.Txt_TotTarif.Size = New System.Drawing.Size(228, 23)
        Me.Txt_TotTarif.TabIndex = 349
        Me.Txt_TotTarif.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Txt_TotBiayaLain
        '
        Me.Txt_TotBiayaLain.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_TotBiayaLain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_TotBiayaLain.Enabled = False
        Me.Txt_TotBiayaLain.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Txt_TotBiayaLain.Location = New System.Drawing.Point(585, 576)
        Me.Txt_TotBiayaLain.Name = "Txt_TotBiayaLain"
        Me.Txt_TotBiayaLain.Size = New System.Drawing.Size(228, 23)
        Me.Txt_TotBiayaLain.TabIndex = 349
        Me.Txt_TotBiayaLain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(461, 578)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(116, 20)
        Me.Label1.TabIndex = 350
        Me.Label1.Text = "Total Biaya Lain"
        '
        'Txt_GrandTotal
        '
        Me.Txt_GrandTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_GrandTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_GrandTotal.Enabled = False
        Me.Txt_GrandTotal.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Txt_GrandTotal.Location = New System.Drawing.Point(936, 603)
        Me.Txt_GrandTotal.Name = "Txt_GrandTotal"
        Me.Txt_GrandTotal.Size = New System.Drawing.Size(228, 23)
        Me.Txt_GrandTotal.TabIndex = 349
        Me.Txt_GrandTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(828, 605)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 20)
        Me.Label2.TabIndex = 350
        Me.Label2.Text = "Grand Total"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(24, 76)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 17)
        Me.Label3.TabIndex = 350
        Me.Label3.Text = "Filter"
        '
        'CmbFilter_KodeBiaya
        '
        Me.CmbFilter_KodeBiaya.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbFilter_KodeBiaya.DropDownWidth = 300
        Me.CmbFilter_KodeBiaya.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbFilter_KodeBiaya.FormattingEnabled = True
        Me.CmbFilter_KodeBiaya.Location = New System.Drawing.Point(81, 72)
        Me.CmbFilter_KodeBiaya.Name = "CmbFilter_KodeBiaya"
        Me.CmbFilter_KodeBiaya.Size = New System.Drawing.Size(189, 26)
        Me.CmbFilter_KodeBiaya.TabIndex = 351
        '
        'CmbFilter_Lokasi
        '
        Me.CmbFilter_Lokasi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbFilter_Lokasi.DropDownWidth = 300
        Me.CmbFilter_Lokasi.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbFilter_Lokasi.FormattingEnabled = True
        Me.CmbFilter_Lokasi.Location = New System.Drawing.Point(276, 72)
        Me.CmbFilter_Lokasi.Name = "CmbFilter_Lokasi"
        Me.CmbFilter_Lokasi.Size = New System.Drawing.Size(154, 26)
        Me.CmbFilter_Lokasi.TabIndex = 351
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(696, 68)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(95, 33)
        Me.Btn_Cari.TabIndex = 348
        Me.Btn_Cari.Text = "&Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Txt_ValueKeterangan
        '
        Me.Txt_ValueKeterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_ValueKeterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_ValueKeterangan.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Txt_ValueKeterangan.Location = New System.Drawing.Point(436, 74)
        Me.Txt_ValueKeterangan.Name = "Txt_ValueKeterangan"
        Me.Txt_ValueKeterangan.Size = New System.Drawing.Size(254, 23)
        Me.Txt_ValueKeterangan.TabIndex = 349
        '
        'SD_Expedisi_PO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1184, 701)
        Me.Controls.Add(Me.CmbFilter_Lokasi)
        Me.Controls.Add(Me.CmbFilter_KodeBiaya)
        Me.Controls.Add(Me.Lv_DataInput)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblPO_TotalMUA)
        Me.Controls.Add(Me.Txt_GrandTotal)
        Me.Controls.Add(Me.Txt_ValueKeterangan)
        Me.Controls.Add(Me.Txt_TotBiayaLain)
        Me.Controls.Add(Me.Txt_TotTarif)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Btn_Cari)
        Me.Controls.Add(Me.BtnPO_Simpan)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Name = "SD_Expedisi_PO"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents LblPO_Judul As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents BtnPO_Simpan As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Lv_BiayaLokal As ListView
    Friend WithEvents Lv_DataInput As ListView
    Friend WithEvents LblPO_TotalMUA As Label
    Friend WithEvents Txt_TotTarif As TextBox
    Friend WithEvents Txt_TotBiayaLain As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_GrandTotal As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents CmbFilter_KodeBiaya As ComboBox
    Friend WithEvents CmbFilter_Lokasi As ComboBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Txt_ValueKeterangan As TextBox
End Class
