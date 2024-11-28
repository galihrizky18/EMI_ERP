<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Timbang_Floor_Scale
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
        Dim GroupBox1 As System.Windows.Forms.GroupBox
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.txtUrutOto = New System.Windows.Forms.TextBox()
        Me.txt_Barang_SN = New System.Windows.Forms.TextBox()
        Me.TxtKdBarang = New System.Windows.Forms.TextBox()
        Me.LblSatuan = New System.Windows.Forms.Label()
        Me.CmbSatuan = New System.Windows.Forms.ComboBox()
        Me.CmbJenisTimbang = New System.Windows.Forms.ComboBox()
        Me.Txt_SatuanKecil = New System.Windows.Forms.TextBox()
        Me.UNIX = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtKodeTransfer = New System.Windows.Forms.TextBox()
        Me.txt_Jumlah_Timbang = New System.Windows.Forms.TextBox()
        Me.txt_Jml_Estimasi = New System.Windows.Forms.TextBox()
        Me.lblLokasi = New System.Windows.Forms.Label()
        Me.Txt_Timbangan = New System.Windows.Forms.TextBox()
        Me.txt_lokasi = New System.Windows.Forms.TextBox()
        Me.lblBarang = New System.Windows.Forms.Label()
        Me.txt_barang = New System.Windows.Forms.TextBox()
        Me.lblJumlahTimbang = New System.Windows.Forms.Label()
        Me.lblJumlahEstimasi = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Barcode = New System.Windows.Forms.PictureBox()
        GroupBox1 = New System.Windows.Forms.GroupBox()
        GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.Barcode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        GroupBox1.Controls.Add(Me.ComboBox1)
        GroupBox1.Controls.Add(Me.Label3)
        GroupBox1.Controls.Add(Me.Label5)
        GroupBox1.Controls.Add(Me.Label4)
        GroupBox1.Controls.Add(Me.TextBox3)
        GroupBox1.Controls.Add(Me.TextBox2)
        GroupBox1.Controls.Add(Me.txtUrutOto)
        GroupBox1.Controls.Add(Me.txt_Barang_SN)
        GroupBox1.Controls.Add(Me.TxtKdBarang)
        GroupBox1.Controls.Add(Me.LblSatuan)
        GroupBox1.Controls.Add(Me.CmbSatuan)
        GroupBox1.Controls.Add(Me.CmbJenisTimbang)
        GroupBox1.Controls.Add(Me.Txt_SatuanKecil)
        GroupBox1.Controls.Add(Me.UNIX)
        GroupBox1.Controls.Add(Me.Label2)
        GroupBox1.Controls.Add(Me.Label1)
        GroupBox1.Controls.Add(Me.txtKodeTransfer)
        GroupBox1.Controls.Add(Me.txt_Jumlah_Timbang)
        GroupBox1.Controls.Add(Me.txt_Jml_Estimasi)
        GroupBox1.Controls.Add(Me.lblLokasi)
        GroupBox1.Controls.Add(Me.Txt_Timbangan)
        GroupBox1.Controls.Add(Me.txt_lokasi)
        GroupBox1.Controls.Add(Me.lblBarang)
        GroupBox1.Controls.Add(Me.txt_barang)
        GroupBox1.Controls.Add(Me.lblJumlahTimbang)
        GroupBox1.Controls.Add(Me.lblJumlahEstimasi)
        GroupBox1.Location = New System.Drawing.Point(20, 66)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New System.Drawing.Size(789, 280)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "Data Scales/Timbang"
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(122, 139)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(317, 23)
        Me.ComboBox1.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(7, 142)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 17)
        Me.Label3.TabIndex = 489
        Me.Label3.Text = "Jenis Alas"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(7, 251)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(97, 17)
        Me.Label5.TabIndex = 488
        Me.Label5.Text = "Jumlah Bersih"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(7, 224)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 17)
        Me.Label4.TabIndex = 487
        Me.Label4.Text = "Jumlah Alas"
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Enabled = False
        Me.TextBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TextBox3.Location = New System.Drawing.Point(122, 249)
        Me.TextBox3.MaxLength = 50
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(154, 21)
        Me.TextBox3.TabIndex = 9
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox2.Enabled = False
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TextBox2.Location = New System.Drawing.Point(122, 222)
        Me.TextBox2.MaxLength = 50
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(154, 21)
        Me.TextBox2.TabIndex = 8
        '
        'txtUrutOto
        '
        Me.txtUrutOto.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtUrutOto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUrutOto.Enabled = False
        Me.txtUrutOto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.txtUrutOto.Location = New System.Drawing.Point(700, 5)
        Me.txtUrutOto.MaxLength = 50
        Me.txtUrutOto.Name = "txtUrutOto"
        Me.txtUrutOto.ReadOnly = True
        Me.txtUrutOto.Size = New System.Drawing.Size(62, 21)
        Me.txtUrutOto.TabIndex = 482
        Me.txtUrutOto.Visible = False
        '
        'txt_Barang_SN
        '
        Me.txt_Barang_SN.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txt_Barang_SN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Barang_SN.Enabled = False
        Me.txt_Barang_SN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.txt_Barang_SN.Location = New System.Drawing.Point(445, 165)
        Me.txt_Barang_SN.MaxLength = 50
        Me.txt_Barang_SN.Name = "txt_Barang_SN"
        Me.txt_Barang_SN.ReadOnly = True
        Me.txt_Barang_SN.Size = New System.Drawing.Size(317, 21)
        Me.txt_Barang_SN.TabIndex = 481
        Me.txt_Barang_SN.Visible = False
        '
        'TxtKdBarang
        '
        Me.TxtKdBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKdBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKdBarang.Enabled = False
        Me.TxtKdBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtKdBarang.Location = New System.Drawing.Point(445, 192)
        Me.TxtKdBarang.MaxLength = 50
        Me.TxtKdBarang.Name = "TxtKdBarang"
        Me.TxtKdBarang.ReadOnly = True
        Me.TxtKdBarang.Size = New System.Drawing.Size(317, 21)
        Me.TxtKdBarang.TabIndex = 481
        Me.TxtKdBarang.Visible = False
        '
        'LblSatuan
        '
        Me.LblSatuan.AutoSize = True
        Me.LblSatuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.LblSatuan.Location = New System.Drawing.Point(283, 168)
        Me.LblSatuan.Name = "LblSatuan"
        Me.LblSatuan.Size = New System.Drawing.Size(53, 17)
        Me.LblSatuan.TabIndex = 480
        Me.LblSatuan.Text = "Satuan"
        '
        'CmbSatuan
        '
        Me.CmbSatuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSatuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbSatuan.FormattingEnabled = True
        Me.CmbSatuan.Location = New System.Drawing.Point(342, 165)
        Me.CmbSatuan.Name = "CmbSatuan"
        Me.CmbSatuan.Size = New System.Drawing.Size(97, 23)
        Me.CmbSatuan.TabIndex = 6
        '
        'CmbJenisTimbang
        '
        Me.CmbJenisTimbang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbJenisTimbang.Enabled = False
        Me.CmbJenisTimbang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbJenisTimbang.FormattingEnabled = True
        Me.CmbJenisTimbang.Location = New System.Drawing.Point(122, 25)
        Me.CmbJenisTimbang.Name = "CmbJenisTimbang"
        Me.CmbJenisTimbang.Size = New System.Drawing.Size(317, 23)
        Me.CmbJenisTimbang.TabIndex = 0
        '
        'Txt_SatuanKecil
        '
        Me.Txt_SatuanKecil.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_SatuanKecil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_SatuanKecil.Enabled = False
        Me.Txt_SatuanKecil.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_SatuanKecil.Location = New System.Drawing.Point(700, 28)
        Me.Txt_SatuanKecil.MaxLength = 50
        Me.Txt_SatuanKecil.Name = "Txt_SatuanKecil"
        Me.Txt_SatuanKecil.ReadOnly = True
        Me.Txt_SatuanKecil.Size = New System.Drawing.Size(62, 21)
        Me.Txt_SatuanKecil.TabIndex = 477
        Me.Txt_SatuanKecil.Visible = False
        '
        'UNIX
        '
        Me.UNIX.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.UNIX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UNIX.Enabled = False
        Me.UNIX.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.UNIX.Location = New System.Drawing.Point(445, 28)
        Me.UNIX.MaxLength = 50
        Me.UNIX.Name = "UNIX"
        Me.UNIX.ReadOnly = True
        Me.UNIX.Size = New System.Drawing.Size(249, 21)
        Me.UNIX.TabIndex = 477
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(7, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 17)
        Me.Label2.TabIndex = 475
        Me.Label2.Text = "Jenis Timbang"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(7, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 17)
        Me.Label1.TabIndex = 473
        Me.Label1.Text = "No Faktur"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtKodeTransfer
        '
        Me.txtKodeTransfer.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtKodeTransfer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKodeTransfer.Enabled = False
        Me.txtKodeTransfer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.txtKodeTransfer.Location = New System.Drawing.Point(122, 55)
        Me.txtKodeTransfer.MaxLength = 50
        Me.txtKodeTransfer.Name = "txtKodeTransfer"
        Me.txtKodeTransfer.ReadOnly = True
        Me.txtKodeTransfer.Size = New System.Drawing.Size(317, 21)
        Me.txtKodeTransfer.TabIndex = 1
        '
        'txt_Jumlah_Timbang
        '
        Me.txt_Jumlah_Timbang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txt_Jumlah_Timbang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Jumlah_Timbang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.txt_Jumlah_Timbang.Location = New System.Drawing.Point(122, 196)
        Me.txt_Jumlah_Timbang.MaxLength = 50
        Me.txt_Jumlah_Timbang.Name = "txt_Jumlah_Timbang"
        Me.txt_Jumlah_Timbang.Size = New System.Drawing.Size(154, 21)
        Me.txt_Jumlah_Timbang.TabIndex = 7
        '
        'txt_Jml_Estimasi
        '
        Me.txt_Jml_Estimasi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txt_Jml_Estimasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Jml_Estimasi.Enabled = False
        Me.txt_Jml_Estimasi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.txt_Jml_Estimasi.Location = New System.Drawing.Point(122, 166)
        Me.txt_Jml_Estimasi.MaxLength = 50
        Me.txt_Jml_Estimasi.Name = "txt_Jml_Estimasi"
        Me.txt_Jml_Estimasi.ReadOnly = True
        Me.txt_Jml_Estimasi.Size = New System.Drawing.Size(154, 21)
        Me.txt_Jml_Estimasi.TabIndex = 5
        '
        'lblLokasi
        '
        Me.lblLokasi.AutoSize = True
        Me.lblLokasi.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.lblLokasi.Location = New System.Drawing.Point(7, 83)
        Me.lblLokasi.Name = "lblLokasi"
        Me.lblLokasi.Size = New System.Drawing.Size(49, 17)
        Me.lblLokasi.TabIndex = 421
        Me.lblLokasi.Text = "Lokasi"
        '
        'Txt_Timbangan
        '
        Me.Txt_Timbangan.BackColor = System.Drawing.SystemColors.WindowText
        Me.Txt_Timbangan.Enabled = False
        Me.Txt_Timbangan.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Timbangan.ForeColor = System.Drawing.SystemColors.Info
        Me.Txt_Timbangan.Location = New System.Drawing.Point(445, 55)
        Me.Txt_Timbangan.Multiline = True
        Me.Txt_Timbangan.Name = "Txt_Timbangan"
        Me.Txt_Timbangan.Size = New System.Drawing.Size(325, 79)
        Me.Txt_Timbangan.TabIndex = 433
        Me.Txt_Timbangan.Text = "000000"
        Me.Txt_Timbangan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_lokasi
        '
        Me.txt_lokasi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txt_lokasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_lokasi.Enabled = False
        Me.txt_lokasi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.txt_lokasi.Location = New System.Drawing.Point(122, 82)
        Me.txt_lokasi.MaxLength = 50
        Me.txt_lokasi.Name = "txt_lokasi"
        Me.txt_lokasi.ReadOnly = True
        Me.txt_lokasi.Size = New System.Drawing.Size(317, 21)
        Me.txt_lokasi.TabIndex = 2
        '
        'lblBarang
        '
        Me.lblBarang.AutoSize = True
        Me.lblBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.lblBarang.Location = New System.Drawing.Point(7, 111)
        Me.lblBarang.Name = "lblBarang"
        Me.lblBarang.Size = New System.Drawing.Size(54, 17)
        Me.lblBarang.TabIndex = 423
        Me.lblBarang.Text = "Barang"
        '
        'txt_barang
        '
        Me.txt_barang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txt_barang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_barang.Enabled = False
        Me.txt_barang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.txt_barang.Location = New System.Drawing.Point(122, 110)
        Me.txt_barang.MaxLength = 50
        Me.txt_barang.Name = "txt_barang"
        Me.txt_barang.ReadOnly = True
        Me.txt_barang.Size = New System.Drawing.Size(317, 21)
        Me.txt_barang.TabIndex = 3
        '
        'lblJumlahTimbang
        '
        Me.lblJumlahTimbang.AutoSize = True
        Me.lblJumlahTimbang.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.lblJumlahTimbang.Location = New System.Drawing.Point(7, 197)
        Me.lblJumlahTimbang.Name = "lblJumlahTimbang"
        Me.lblJumlahTimbang.Size = New System.Drawing.Size(112, 17)
        Me.lblJumlahTimbang.TabIndex = 441
        Me.lblJumlahTimbang.Text = "Jumlah Timbang"
        '
        'lblJumlahEstimasi
        '
        Me.lblJumlahEstimasi.AutoSize = True
        Me.lblJumlahEstimasi.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.lblJumlahEstimasi.Location = New System.Drawing.Point(7, 168)
        Me.lblJumlahEstimasi.Name = "lblJumlahEstimasi"
        Me.lblJumlahEstimasi.Size = New System.Drawing.Size(109, 17)
        Me.lblJumlahEstimasi.TabIndex = 450
        Me.lblJumlahEstimasi.Text = "Jumlah Estimasi"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Lbl_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1012, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1012, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 11)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(340, 25)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Transaksi - Timbang Unloading"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1208, 10)
        Me.Panel2.TabIndex = 34
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 63)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 687)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(20, 386)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(676, 12)
        Me.Panel7.TabIndex = 37
        Me.Panel7.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(810, 63)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 687)
        Me.Panel5.TabIndex = 36
        Me.Panel5.Visible = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(23, 352)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(94, 32)
        Me.Btn_Simpan.TabIndex = 1
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(117, 352)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(94, 32)
        Me.Btn_Refresh.TabIndex = 2
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Barcode
        '
        Me.Barcode.Location = New System.Drawing.Point(815, 52)
        Me.Barcode.Name = "Barcode"
        Me.Barcode.Size = New System.Drawing.Size(80, 72)
        Me.Barcode.TabIndex = 463
        Me.Barcode.TabStop = False
        Me.Barcode.Visible = False
        '
        'EMI_Timbang_Floor_Scale
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1012, 405)
        Me.Controls.Add(Me.Barcode)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(GroupBox1)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Timbang_Floor_Scale"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.Barcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents lblJumlahEstimasi As Label
    Friend WithEvents txt_barang As TextBox
    Friend WithEvents lblBarang As Label
    Friend WithEvents txt_lokasi As TextBox
    Friend WithEvents Txt_Timbangan As TextBox
    Friend WithEvents lblLokasi As Label
    Friend WithEvents txt_Jumlah_Timbang As TextBox
    Friend WithEvents txt_Jml_Estimasi As TextBox
    Friend WithEvents lblJumlahTimbang As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtKodeTransfer As TextBox
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents UNIX As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents CmbJenisTimbang As ComboBox
    Friend WithEvents LblSatuan As Label
    Friend WithEvents CmbSatuan As ComboBox
    Friend WithEvents TxtKdBarang As TextBox
    Friend WithEvents txt_Barang_SN As TextBox
    Friend WithEvents Txt_SatuanKecil As TextBox
    Friend WithEvents Barcode As PictureBox
    Friend WithEvents txtUrutOto As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label3 As Label
    '''Friend WithEvents StreamPlayerControl1 As WebEye.Controls.WinForms.StreamPlayerControl.StreamPlayerControl
    '''Friend WithEvents StreamPlayerControl2 As WebEye.Controls.WinForms.StreamPlayerControl.StreamPlayerControl
End Class
