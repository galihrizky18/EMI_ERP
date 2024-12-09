<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Input_Data_kontainer_Loading_Barang
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
        Me.Kontainer = New System.Windows.Forms.TextBox()
        Me.Barang = New System.Windows.Forms.TextBox()
        Me.Tanggal = New System.Windows.Forms.DateTimePicker()
        Me.Qty = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Seal = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.kode = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.faktur = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Lokasi = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.DTP_TglProduksi = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DTP_TglExpired = New System.Windows.Forms.DateTimePicker()
        Me.TxtSupplier = New System.Windows.Forms.TextBox()
        Me.Lokasi_utama = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Kontainer
        '
        Me.Kontainer.Enabled = False
        Me.Kontainer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Kontainer.Location = New System.Drawing.Point(131, 30)
        Me.Kontainer.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Kontainer.Name = "Kontainer"
        Me.Kontainer.Size = New System.Drawing.Size(203, 21)
        Me.Kontainer.TabIndex = 212
        '
        'Barang
        '
        Me.Barang.Enabled = False
        Me.Barang.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Barang.Location = New System.Drawing.Point(131, 141)
        Me.Barang.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Barang.Name = "Barang"
        Me.Barang.Size = New System.Drawing.Size(314, 21)
        Me.Barang.TabIndex = 214
        '
        'Tanggal
        '
        Me.Tanggal.CustomFormat = "dd MMMM yyyy"
        Me.Tanggal.Enabled = False
        Me.Tanggal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tanggal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Tanggal.Location = New System.Drawing.Point(131, 75)
        Me.Tanggal.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Tanggal.Name = "Tanggal"
        Me.Tanggal.Size = New System.Drawing.Size(203, 20)
        Me.Tanggal.TabIndex = 215
        '
        'Qty
        '
        Me.Qty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Qty.Location = New System.Drawing.Point(131, 164)
        Me.Qty.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Qty.Name = "Qty"
        Me.Qty.Size = New System.Drawing.Size(89, 21)
        Me.Qty.TabIndex = 216
        Me.Qty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 217
        Me.Label1.Text = "No Kontainer"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 218
        Me.Label2.Text = "No Seal"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(23, 81)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 219
        Me.Label3.Text = "Tanggal Muat"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(23, 146)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 13)
        Me.Label4.TabIndex = 220
        Me.Label4.Text = "Nama Barang"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(25, 236)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(401, 27)
        Me.Button1.TabIndex = 221
        Me.Button1.Text = "Simpan"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Seal
        '
        Me.Seal.Enabled = False
        Me.Seal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Seal.Location = New System.Drawing.Point(131, 53)
        Me.Seal.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Seal.Name = "Seal"
        Me.Seal.Size = New System.Drawing.Size(203, 21)
        Me.Seal.TabIndex = 222
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(23, 167)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 13)
        Me.Label5.TabIndex = 223
        Me.Label5.Text = "Quantity"
        '
        'kode
        '
        Me.kode.Enabled = False
        Me.kode.Location = New System.Drawing.Point(131, 119)
        Me.kode.Name = "kode"
        Me.kode.Size = New System.Drawing.Size(203, 20)
        Me.kode.TabIndex = 224
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(23, 122)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 13)
        Me.Label6.TabIndex = 225
        Me.Label6.Text = "Kode Barang"
        '
        'faktur
        '
        Me.faktur.Enabled = False
        Me.faktur.Location = New System.Drawing.Point(131, 8)
        Me.faktur.Name = "faktur"
        Me.faktur.Size = New System.Drawing.Size(203, 20)
        Me.faktur.TabIndex = 226
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(23, 11)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 13)
        Me.Label7.TabIndex = 227
        Me.Label7.Text = "No Faktur"
        '
        'Lokasi
        '
        Me.Lokasi.Enabled = False
        Me.Lokasi.Location = New System.Drawing.Point(131, 97)
        Me.Lokasi.Name = "Lokasi"
        Me.Lokasi.Size = New System.Drawing.Size(203, 20)
        Me.Lokasi.TabIndex = 228
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(23, 100)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 13)
        Me.Label8.TabIndex = 229
        Me.Label8.Text = "Lokasi"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(23, 188)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(90, 13)
        Me.Label9.TabIndex = 231
        Me.Label9.Text = "Tanggal Produksi"
        '
        'DTP_TglProduksi
        '
        Me.DTP_TglProduksi.CustomFormat = "dd MMMM yyyy"
        Me.DTP_TglProduksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTP_TglProduksi.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTP_TglProduksi.Location = New System.Drawing.Point(131, 187)
        Me.DTP_TglProduksi.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DTP_TglProduksi.Name = "DTP_TglProduksi"
        Me.DTP_TglProduksi.Size = New System.Drawing.Size(203, 20)
        Me.DTP_TglProduksi.TabIndex = 230
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(23, 210)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(84, 13)
        Me.Label10.TabIndex = 233
        Me.Label10.Text = "Tanggal Expired"
        '
        'DTP_TglExpired
        '
        Me.DTP_TglExpired.CustomFormat = "dd MMMM yyyy"
        Me.DTP_TglExpired.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTP_TglExpired.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTP_TglExpired.Location = New System.Drawing.Point(131, 209)
        Me.DTP_TglExpired.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DTP_TglExpired.Name = "DTP_TglExpired"
        Me.DTP_TglExpired.Size = New System.Drawing.Size(203, 20)
        Me.DTP_TglExpired.TabIndex = 232
        '
        'TxtSupplier
        '
        Me.TxtSupplier.Enabled = False
        Me.TxtSupplier.Location = New System.Drawing.Point(339, 11)
        Me.TxtSupplier.Name = "TxtSupplier"
        Me.TxtSupplier.Size = New System.Drawing.Size(110, 20)
        Me.TxtSupplier.TabIndex = 234
        '
        'Lokasi_utama
        '
        Me.Lokasi_utama.Enabled = False
        Me.Lokasi_utama.Location = New System.Drawing.Point(339, 37)
        Me.Lokasi_utama.Name = "Lokasi_utama"
        Me.Lokasi_utama.Size = New System.Drawing.Size(110, 20)
        Me.Lokasi_utama.TabIndex = 235
        Me.Lokasi_utama.Visible = False
        '
        'Input_Data_kontainer_Loading_Barang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.ClientSize = New System.Drawing.Size(457, 268)
        Me.Controls.Add(Me.Lokasi_utama)
        Me.Controls.Add(Me.TxtSupplier)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.DTP_TglExpired)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.DTP_TglProduksi)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Lokasi)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.faktur)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.kode)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Seal)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Qty)
        Me.Controls.Add(Me.Tanggal)
        Me.Controls.Add(Me.Barang)
        Me.Controls.Add(Me.Kontainer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Input_Data_kontainer_Loading_Barang"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ".: Input Data Kontainer :."
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Kontainer As System.Windows.Forms.TextBox
    Friend WithEvents Barang As System.Windows.Forms.TextBox
    Friend WithEvents Tanggal As System.Windows.Forms.DateTimePicker
    Friend WithEvents Qty As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Seal As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents kode As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents faktur As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Lokasi As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As Label
    Friend WithEvents DTP_TglProduksi As DateTimePicker
    Friend WithEvents Label10 As Label
    Friend WithEvents DTP_TglExpired As DateTimePicker
    Friend WithEvents TxtSupplier As TextBox
    Friend WithEvents Lokasi_utama As TextBox
End Class
