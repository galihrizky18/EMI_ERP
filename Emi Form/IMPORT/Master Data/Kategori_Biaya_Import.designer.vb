<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Kategori_Biaya_Import
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Kategori_Biaya_Import))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.CbKode_Master = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbUrutan = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtKeterangan = New System.Windows.Forms.TextBox()
        Me.txtKode_Kategori = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.BtRefresh = New System.Windows.Forms.Button()
        Me.btHapus = New System.Windows.Forms.Button()
        Me.btSimpan = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lvKategori_Biaya = New System.Windows.Forms.ListView()
        Me.Cmb_MasukHPP = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Maroon
        Me.Label1.Location = New System.Drawing.Point(-1, -1)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.Label1.Size = New System.Drawing.Size(709, 33)
        Me.Label1.TabIndex = 72
        Me.Label1.Text = "Kategori Import"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Cmb_MasukHPP)
        Me.GroupBox1.Controls.Add(Me.ComboBox1)
        Me.GroupBox1.Controls.Add(Me.CbKode_Master)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cbUrutan)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtKeterangan)
        Me.GroupBox1.Controls.Add(Me.txtKode_Kategori)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 41)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(506, 173)
        Me.GroupBox1.TabIndex = 73
        Me.GroupBox1.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 119)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Flag Average"
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(172, 116)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(229, 21)
        Me.ComboBox1.TabIndex = 4
        '
        'CbKode_Master
        '
        Me.CbKode_Master.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbKode_Master.FormattingEnabled = True
        Me.CbKode_Master.Location = New System.Drawing.Point(172, 89)
        Me.CbKode_Master.Name = "CbKode_Master"
        Me.CbKode_Master.Size = New System.Drawing.Size(229, 21)
        Me.CbKode_Master.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 92)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(145, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Kode Master Kategori Import"
        '
        'cbUrutan
        '
        Me.cbUrutan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbUrutan.FormattingEnabled = True
        Me.cbUrutan.Location = New System.Drawing.Point(172, 63)
        Me.cbUrutan.Name = "cbUrutan"
        Me.cbUrutan.Size = New System.Drawing.Size(229, 21)
        Me.cbUrutan.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 66)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Urutan"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Keterangan"
        '
        'txtKeterangan
        '
        Me.txtKeterangan.Location = New System.Drawing.Point(172, 39)
        Me.txtKeterangan.Name = "txtKeterangan"
        Me.txtKeterangan.Size = New System.Drawing.Size(229, 21)
        Me.txtKeterangan.TabIndex = 1
        '
        'txtKode_Kategori
        '
        Me.txtKode_Kategori.Location = New System.Drawing.Point(172, 15)
        Me.txtKode_Kategori.Name = "txtKode_Kategori"
        Me.txtKode_Kategori.Size = New System.Drawing.Size(229, 21)
        Me.txtKode_Kategori.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Kode Kategori"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.BtRefresh)
        Me.GroupBox2.Controls.Add(Me.btHapus)
        Me.GroupBox2.Controls.Add(Me.btSimpan)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 214)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(506, 51)
        Me.GroupBox2.TabIndex = 74
        Me.GroupBox2.TabStop = False
        '
        'BtRefresh
        '
        Me.BtRefresh.Location = New System.Drawing.Point(349, 19)
        Me.BtRefresh.Name = "BtRefresh"
        Me.BtRefresh.Size = New System.Drawing.Size(150, 22)
        Me.BtRefresh.TabIndex = 3
        Me.BtRefresh.Text = "&Refresh"
        Me.BtRefresh.UseVisualStyleBackColor = True
        '
        'btHapus
        '
        Me.btHapus.Enabled = False
        Me.btHapus.Location = New System.Drawing.Point(185, 19)
        Me.btHapus.Name = "btHapus"
        Me.btHapus.Size = New System.Drawing.Size(150, 22)
        Me.btHapus.TabIndex = 1
        Me.btHapus.Text = "&Hapus"
        Me.btHapus.UseVisualStyleBackColor = True
        '
        'btSimpan
        '
        Me.btSimpan.Location = New System.Drawing.Point(19, 19)
        Me.btSimpan.Name = "btSimpan"
        Me.btSimpan.Size = New System.Drawing.Size(150, 22)
        Me.btSimpan.TabIndex = 0
        Me.btSimpan.Text = "&Simpan"
        Me.btSimpan.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lvKategori_Biaya)
        Me.GroupBox3.Location = New System.Drawing.Point(3, 268)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(688, 200)
        Me.GroupBox3.TabIndex = 75
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Display Kategori Biaya Import"
        '
        'lvKategori_Biaya
        '
        Me.lvKategori_Biaya.FullRowSelect = True
        Me.lvKategori_Biaya.GridLines = True
        Me.lvKategori_Biaya.HideSelection = False
        Me.lvKategori_Biaya.Location = New System.Drawing.Point(6, 16)
        Me.lvKategori_Biaya.Name = "lvKategori_Biaya"
        Me.lvKategori_Biaya.Size = New System.Drawing.Size(672, 178)
        Me.lvKategori_Biaya.TabIndex = 0
        Me.lvKategori_Biaya.UseCompatibleStateImageBehavior = False
        Me.lvKategori_Biaya.View = System.Windows.Forms.View.Details
        '
        'Cmb_MasukHPP
        '
        Me.Cmb_MasukHPP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_MasukHPP.FormattingEnabled = True
        Me.Cmb_MasukHPP.Location = New System.Drawing.Point(172, 143)
        Me.Cmb_MasukHPP.Name = "Cmb_MasukHPP"
        Me.Cmb_MasukHPP.Size = New System.Drawing.Size(229, 21)
        Me.Cmb_MasukHPP.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 146)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Flag Masuk HPP"
        '
        'Kategori_Biaya_Import
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.ClientSize = New System.Drawing.Size(691, 468)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Kategori_Biaya_Import"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ".:: Kategori Import ::."
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtKode_Kategori As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btHapus As System.Windows.Forms.Button
    Friend WithEvents btSimpan As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lvKategori_Biaya As System.Windows.Forms.ListView
    Friend WithEvents BtRefresh As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtKeterangan As System.Windows.Forms.TextBox
    Friend WithEvents cbUrutan As System.Windows.Forms.ComboBox
    Friend WithEvents CbKode_Master As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Cmb_MasukHPP As ComboBox
End Class
