<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class N_EMI_SD_Transaksi_Transfer_Stock_Timbang
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
        Me.TxtOriginal_Data_FloorScale = New System.Windows.Forms.TextBox()
        Me.TxtSatuan_FloorScale = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txt_Jumlah_Timbang = New System.Windows.Forms.TextBox()
        Me.Txt_Sisa_Bags = New System.Windows.Forms.TextBox()
        Me.TxtJumlahBagsDetail = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblJumlahTimbang = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Txt_Sisa_Request = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Txt_Sisa_Jumlah = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TxtBeratBersih = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CmbSatuan = New System.Windows.Forms.ComboBox()
        Me.LblSatuan = New System.Windows.Forms.Label()
        Me.TxtJumlahBags = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtBeratBags = New System.Windows.Forms.TextBox()
        Me.CmbJenisAlas = New System.Windows.Forms.ComboBox()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtBeratAlas = New System.Windows.Forms.TextBox()
        Me.CmbJenisTimbang = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtKodeTransfer = New System.Windows.Forms.TextBox()
        Me.txt_Jml_Estimasi = New System.Windows.Forms.TextBox()
        Me.lblLokasi = New System.Windows.Forms.Label()
        Me.Txt_Timbangan = New System.Windows.Forms.TextBox()
        Me.txt_lokasi = New System.Windows.Forms.TextBox()
        Me.lblBarang = New System.Windows.Forms.Label()
        Me.txt_barang = New System.Windows.Forms.TextBox()
        Me.lblJumlahEstimasi = New System.Windows.Forms.Label()
        Me.TxtBarcode = New System.Windows.Forms.TextBox()
        Me.txtUrutOto = New System.Windows.Forms.TextBox()
        Me.txt_Barang_SN = New System.Windows.Forms.TextBox()
        Me.TxtKdBarang = New System.Windows.Forms.TextBox()
        Me.Txt_SatuanKecil = New System.Windows.Forms.TextBox()
        Me.UNIX = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Barcode = New System.Windows.Forms.PictureBox()
        Me.Txt_Berat_Bags_Bersih = New System.Windows.Forms.TextBox()
        Me.TxtBeratAlas_Bersih = New System.Windows.Forms.TextBox()
        Me.Txt_Jumlah_Sisa_Bersih = New System.Windows.Forms.TextBox()
        Me.Txt_Bags_Sisa_Bersih = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Cmb_No_Faktur = New System.Windows.Forms.ComboBox()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        GroupBox1 = New System.Windows.Forms.GroupBox()
        GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.Barcode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        GroupBox1.Controls.Add(Me.TxtOriginal_Data_FloorScale)
        GroupBox1.Controls.Add(Me.TxtSatuan_FloorScale)
        GroupBox1.Controls.Add(Me.GroupBox2)
        GroupBox1.Controls.Add(Me.TxtJumlahBags)
        GroupBox1.Controls.Add(Me.Label7)
        GroupBox1.Controls.Add(Me.Label6)
        GroupBox1.Controls.Add(Me.TxtBeratBags)
        GroupBox1.Controls.Add(Me.CmbJenisAlas)
        GroupBox1.Controls.Add(Me.Btn_Refresh)
        GroupBox1.Controls.Add(Me.Btn_Simpan)
        GroupBox1.Controls.Add(Me.Label3)
        GroupBox1.Controls.Add(Me.Label4)
        GroupBox1.Controls.Add(Me.TxtBeratAlas)
        GroupBox1.Controls.Add(Me.Cmb_No_Faktur)
        GroupBox1.Controls.Add(Me.Label12)
        GroupBox1.Controls.Add(Me.CmbJenisTimbang)
        GroupBox1.Controls.Add(Me.Label2)
        GroupBox1.Controls.Add(Me.Label1)
        GroupBox1.Controls.Add(Me.txtKodeTransfer)
        GroupBox1.Controls.Add(Me.txt_Jml_Estimasi)
        GroupBox1.Controls.Add(Me.lblLokasi)
        GroupBox1.Controls.Add(Me.Txt_Timbangan)
        GroupBox1.Controls.Add(Me.txt_lokasi)
        GroupBox1.Controls.Add(Me.lblBarang)
        GroupBox1.Controls.Add(Me.txt_barang)
        GroupBox1.Controls.Add(Me.lblJumlahEstimasi)
        GroupBox1.Location = New System.Drawing.Point(20, 66)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New System.Drawing.Size(789, 396)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "Data Scales/Timbang"
        '
        'TxtOriginal_Data_FloorScale
        '
        Me.TxtOriginal_Data_FloorScale.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtOriginal_Data_FloorScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtOriginal_Data_FloorScale.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtOriginal_Data_FloorScale.Location = New System.Drawing.Point(452, 244)
        Me.TxtOriginal_Data_FloorScale.MaxLength = 50
        Me.TxtOriginal_Data_FloorScale.Name = "TxtOriginal_Data_FloorScale"
        Me.TxtOriginal_Data_FloorScale.Size = New System.Drawing.Size(324, 21)
        Me.TxtOriginal_Data_FloorScale.TabIndex = 498
        Me.TxtOriginal_Data_FloorScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtOriginal_Data_FloorScale.Visible = False
        '
        'TxtSatuan_FloorScale
        '
        Me.TxtSatuan_FloorScale.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtSatuan_FloorScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSatuan_FloorScale.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtSatuan_FloorScale.Location = New System.Drawing.Point(452, 271)
        Me.TxtSatuan_FloorScale.MaxLength = 50
        Me.TxtSatuan_FloorScale.Name = "TxtSatuan_FloorScale"
        Me.TxtSatuan_FloorScale.Size = New System.Drawing.Size(133, 21)
        Me.TxtSatuan_FloorScale.TabIndex = 497
        Me.TxtSatuan_FloorScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtSatuan_FloorScale.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txt_Jumlah_Timbang)
        Me.GroupBox2.Controls.Add(Me.Txt_Sisa_Bags)
        Me.GroupBox2.Controls.Add(Me.TxtJumlahBagsDetail)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.lblJumlahTimbang)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Txt_Sisa_Request)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Txt_Sisa_Jumlah)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.TxtBeratBersih)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.CmbSatuan)
        Me.GroupBox2.Controls.Add(Me.LblSatuan)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 222)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(439, 132)
        Me.GroupBox2.TabIndex = 497
        Me.GroupBox2.TabStop = False
        '
        'txt_Jumlah_Timbang
        '
        Me.txt_Jumlah_Timbang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txt_Jumlah_Timbang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Jumlah_Timbang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.txt_Jumlah_Timbang.Location = New System.Drawing.Point(115, 69)
        Me.txt_Jumlah_Timbang.MaxLength = 50
        Me.txt_Jumlah_Timbang.Name = "txt_Jumlah_Timbang"
        Me.txt_Jumlah_Timbang.Size = New System.Drawing.Size(124, 21)
        Me.txt_Jumlah_Timbang.TabIndex = 7
        Me.txt_Jumlah_Timbang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Txt_Sisa_Bags
        '
        Me.Txt_Sisa_Bags.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Sisa_Bags.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Sisa_Bags.Enabled = False
        Me.Txt_Sisa_Bags.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_Sisa_Bags.Location = New System.Drawing.Point(341, 42)
        Me.Txt_Sisa_Bags.MaxLength = 50
        Me.Txt_Sisa_Bags.Name = "Txt_Sisa_Bags"
        Me.Txt_Sisa_Bags.ReadOnly = True
        Me.Txt_Sisa_Bags.Size = New System.Drawing.Size(92, 21)
        Me.Txt_Sisa_Bags.TabIndex = 495
        Me.Txt_Sisa_Bags.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtJumlahBagsDetail
        '
        Me.TxtJumlahBagsDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtJumlahBagsDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtJumlahBagsDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtJumlahBagsDetail.Location = New System.Drawing.Point(341, 69)
        Me.TxtJumlahBagsDetail.MaxLength = 50
        Me.TxtJumlahBagsDetail.Name = "TxtJumlahBagsDetail"
        Me.TxtJumlahBagsDetail.ReadOnly = True
        Me.TxtJumlahBagsDetail.Size = New System.Drawing.Size(92, 21)
        Me.TxtJumlahBagsDetail.TabIndex = 495
        Me.TxtJumlahBagsDetail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label10.Location = New System.Drawing.Point(245, 44)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(71, 17)
        Me.Label10.TabIndex = 496
        Me.Label10.Text = "Sisa Bags"
        '
        'lblJumlahTimbang
        '
        Me.lblJumlahTimbang.AutoSize = True
        Me.lblJumlahTimbang.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.lblJumlahTimbang.Location = New System.Drawing.Point(8, 71)
        Me.lblJumlahTimbang.Name = "lblJumlahTimbang"
        Me.lblJumlahTimbang.Size = New System.Drawing.Size(101, 17)
        Me.lblJumlahTimbang.TabIndex = 441
        Me.lblJumlahTimbang.Text = "Berat Timbang"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label8.Location = New System.Drawing.Point(245, 71)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(91, 17)
        Me.Label8.TabIndex = 496
        Me.Label8.Text = "Bags Simpan"
        '
        'Txt_Sisa_Request
        '
        Me.Txt_Sisa_Request.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Sisa_Request.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Sisa_Request.Enabled = False
        Me.Txt_Sisa_Request.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_Sisa_Request.Location = New System.Drawing.Point(115, 15)
        Me.Txt_Sisa_Request.MaxLength = 50
        Me.Txt_Sisa_Request.Name = "Txt_Sisa_Request"
        Me.Txt_Sisa_Request.Size = New System.Drawing.Size(124, 21)
        Me.Txt_Sisa_Request.TabIndex = 9
        Me.Txt_Sisa_Request.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label11.Location = New System.Drawing.Point(8, 17)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(92, 17)
        Me.Label11.TabIndex = 488
        Me.Label11.Text = "Sisa Request"
        '
        'Txt_Sisa_Jumlah
        '
        Me.Txt_Sisa_Jumlah.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Sisa_Jumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Sisa_Jumlah.Enabled = False
        Me.Txt_Sisa_Jumlah.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_Sisa_Jumlah.Location = New System.Drawing.Point(115, 42)
        Me.Txt_Sisa_Jumlah.MaxLength = 50
        Me.Txt_Sisa_Jumlah.Name = "Txt_Sisa_Jumlah"
        Me.Txt_Sisa_Jumlah.Size = New System.Drawing.Size(124, 21)
        Me.Txt_Sisa_Jumlah.TabIndex = 9
        Me.Txt_Sisa_Jumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label9.Location = New System.Drawing.Point(8, 44)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(84, 17)
        Me.Label9.TabIndex = 488
        Me.Label9.Text = "Sisa Jumlah"
        '
        'TxtBeratBersih
        '
        Me.TxtBeratBersih.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtBeratBersih.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtBeratBersih.Enabled = False
        Me.TxtBeratBersih.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtBeratBersih.Location = New System.Drawing.Point(115, 96)
        Me.TxtBeratBersih.MaxLength = 50
        Me.TxtBeratBersih.Name = "TxtBeratBersih"
        Me.TxtBeratBersih.Size = New System.Drawing.Size(124, 21)
        Me.TxtBeratBersih.TabIndex = 9
        Me.TxtBeratBersih.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(8, 98)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 17)
        Me.Label5.TabIndex = 488
        Me.Label5.Text = "Berat Bersih"
        '
        'CmbSatuan
        '
        Me.CmbSatuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSatuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbSatuan.FormattingEnabled = True
        Me.CmbSatuan.Location = New System.Drawing.Point(341, 98)
        Me.CmbSatuan.Name = "CmbSatuan"
        Me.CmbSatuan.Size = New System.Drawing.Size(92, 23)
        Me.CmbSatuan.TabIndex = 6
        '
        'LblSatuan
        '
        Me.LblSatuan.AutoSize = True
        Me.LblSatuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.LblSatuan.Location = New System.Drawing.Point(245, 101)
        Me.LblSatuan.Name = "LblSatuan"
        Me.LblSatuan.Size = New System.Drawing.Size(53, 17)
        Me.LblSatuan.TabIndex = 480
        Me.LblSatuan.Text = "Satuan"
        '
        'TxtJumlahBags
        '
        Me.TxtJumlahBags.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtJumlahBags.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtJumlahBags.Enabled = False
        Me.TxtJumlahBags.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtJumlahBags.Location = New System.Drawing.Point(348, 168)
        Me.TxtJumlahBags.MaxLength = 50
        Me.TxtJumlahBags.Name = "TxtJumlahBags"
        Me.TxtJumlahBags.ReadOnly = True
        Me.TxtJumlahBags.Size = New System.Drawing.Size(92, 21)
        Me.TxtJumlahBags.TabIndex = 492
        Me.TxtJumlahBags.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label7.Location = New System.Drawing.Point(252, 170)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(89, 17)
        Me.Label7.TabIndex = 493
        Me.Label7.Text = "Jumlah Bags"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(252, 197)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 17)
        Me.Label6.TabIndex = 491
        Me.Label6.Text = "Berat Bags"
        '
        'TxtBeratBags
        '
        Me.TxtBeratBags.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtBeratBags.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtBeratBags.Enabled = False
        Me.TxtBeratBags.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtBeratBags.Location = New System.Drawing.Point(348, 195)
        Me.TxtBeratBags.MaxLength = 50
        Me.TxtBeratBags.Name = "TxtBeratBags"
        Me.TxtBeratBags.Size = New System.Drawing.Size(92, 21)
        Me.TxtBeratBags.TabIndex = 490
        Me.TxtBeratBags.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CmbJenisAlas
        '
        Me.CmbJenisAlas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbJenisAlas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbJenisAlas.FormattingEnabled = True
        Me.CmbJenisAlas.Location = New System.Drawing.Point(122, 139)
        Me.CmbJenisAlas.Name = "CmbJenisAlas"
        Me.CmbJenisAlas.Size = New System.Drawing.Size(317, 23)
        Me.CmbJenisAlas.TabIndex = 4
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(100, 360)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(94, 32)
        Me.Btn_Refresh.TabIndex = 2
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(6, 360)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(94, 32)
        Me.Btn_Simpan.TabIndex = 1
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
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
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(9, 197)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 17)
        Me.Label4.TabIndex = 487
        Me.Label4.Text = "Berat Alas"
        '
        'TxtBeratAlas
        '
        Me.TxtBeratAlas.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtBeratAlas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtBeratAlas.Enabled = False
        Me.TxtBeratAlas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtBeratAlas.Location = New System.Drawing.Point(122, 195)
        Me.TxtBeratAlas.MaxLength = 50
        Me.TxtBeratAlas.Name = "TxtBeratAlas"
        Me.TxtBeratAlas.Size = New System.Drawing.Size(124, 21)
        Me.TxtBeratAlas.TabIndex = 8
        Me.TxtBeratAlas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.Label1.Location = New System.Drawing.Point(7, 85)
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
        Me.txtKodeTransfer.Location = New System.Drawing.Point(122, 83)
        Me.txtKodeTransfer.MaxLength = 50
        Me.txtKodeTransfer.Name = "txtKodeTransfer"
        Me.txtKodeTransfer.ReadOnly = True
        Me.txtKodeTransfer.Size = New System.Drawing.Size(141, 21)
        Me.txtKodeTransfer.TabIndex = 1
        '
        'txt_Jml_Estimasi
        '
        Me.txt_Jml_Estimasi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txt_Jml_Estimasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Jml_Estimasi.Enabled = False
        Me.txt_Jml_Estimasi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.txt_Jml_Estimasi.Location = New System.Drawing.Point(122, 168)
        Me.txt_Jml_Estimasi.MaxLength = 50
        Me.txt_Jml_Estimasi.Name = "txt_Jml_Estimasi"
        Me.txt_Jml_Estimasi.ReadOnly = True
        Me.txt_Jml_Estimasi.Size = New System.Drawing.Size(124, 21)
        Me.txt_Jml_Estimasi.TabIndex = 5
        Me.txt_Jml_Estimasi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblLokasi
        '
        Me.lblLokasi.AutoSize = True
        Me.lblLokasi.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.lblLokasi.Location = New System.Drawing.Point(273, 85)
        Me.lblLokasi.Name = "lblLokasi"
        Me.lblLokasi.Size = New System.Drawing.Size(49, 17)
        Me.lblLokasi.TabIndex = 421
        Me.lblLokasi.Text = "Lokasi"
        '
        'Txt_Timbangan
        '
        Me.Txt_Timbangan.BackColor = System.Drawing.SystemColors.WindowText
        Me.Txt_Timbangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Timbangan.Font = New System.Drawing.Font("Microsoft Sans Serif", 40.0!, System.Drawing.FontStyle.Bold)
        Me.Txt_Timbangan.ForeColor = System.Drawing.Color.White
        Me.Txt_Timbangan.Location = New System.Drawing.Point(445, 24)
        Me.Txt_Timbangan.Name = "Txt_Timbangan"
        Me.Txt_Timbangan.ReadOnly = True
        Me.Txt_Timbangan.Size = New System.Drawing.Size(325, 68)
        Me.Txt_Timbangan.TabIndex = 433
        Me.Txt_Timbangan.TabStop = False
        Me.Txt_Timbangan.Text = "0"
        Me.Txt_Timbangan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_lokasi
        '
        Me.txt_lokasi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txt_lokasi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_lokasi.Enabled = False
        Me.txt_lokasi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.txt_lokasi.Location = New System.Drawing.Point(324, 83)
        Me.txt_lokasi.MaxLength = 50
        Me.txt_lokasi.Name = "txt_lokasi"
        Me.txt_lokasi.ReadOnly = True
        Me.txt_lokasi.Size = New System.Drawing.Size(115, 21)
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
        'lblJumlahEstimasi
        '
        Me.lblJumlahEstimasi.AutoSize = True
        Me.lblJumlahEstimasi.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.lblJumlahEstimasi.Location = New System.Drawing.Point(9, 170)
        Me.lblJumlahEstimasi.Name = "lblJumlahEstimasi"
        Me.lblJumlahEstimasi.Size = New System.Drawing.Size(98, 17)
        Me.lblJumlahEstimasi.TabIndex = 450
        Me.lblJumlahEstimasi.Text = "Berat Estimasi"
        '
        'TxtBarcode
        '
        Me.TxtBarcode.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtBarcode.Enabled = False
        Me.TxtBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtBarcode.Location = New System.Drawing.Point(935, 235)
        Me.TxtBarcode.MaxLength = 50
        Me.TxtBarcode.Name = "TxtBarcode"
        Me.TxtBarcode.ReadOnly = True
        Me.TxtBarcode.Size = New System.Drawing.Size(249, 21)
        Me.TxtBarcode.TabIndex = 494
        Me.TxtBarcode.Visible = False
        '
        'txtUrutOto
        '
        Me.txtUrutOto.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtUrutOto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUrutOto.Enabled = False
        Me.txtUrutOto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.txtUrutOto.Location = New System.Drawing.Point(944, 68)
        Me.txtUrutOto.MaxLength = 50
        Me.txtUrutOto.Name = "txtUrutOto"
        Me.txtUrutOto.ReadOnly = True
        Me.txtUrutOto.Size = New System.Drawing.Size(62, 21)
        Me.txtUrutOto.TabIndex = 482
        '
        'txt_Barang_SN
        '
        Me.txt_Barang_SN.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txt_Barang_SN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Barang_SN.Enabled = False
        Me.txt_Barang_SN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.txt_Barang_SN.Location = New System.Drawing.Point(935, 180)
        Me.txt_Barang_SN.MaxLength = 50
        Me.txt_Barang_SN.Name = "txt_Barang_SN"
        Me.txt_Barang_SN.ReadOnly = True
        Me.txt_Barang_SN.Size = New System.Drawing.Size(216, 21)
        Me.txt_Barang_SN.TabIndex = 481
        Me.txt_Barang_SN.Visible = False
        '
        'TxtKdBarang
        '
        Me.TxtKdBarang.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtKdBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtKdBarang.Enabled = False
        Me.TxtKdBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtKdBarang.Location = New System.Drawing.Point(935, 208)
        Me.TxtKdBarang.MaxLength = 50
        Me.TxtKdBarang.Name = "TxtKdBarang"
        Me.TxtKdBarang.ReadOnly = True
        Me.TxtKdBarang.Size = New System.Drawing.Size(249, 21)
        Me.TxtKdBarang.TabIndex = 481
        Me.TxtKdBarang.Visible = False
        '
        'Txt_SatuanKecil
        '
        Me.Txt_SatuanKecil.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_SatuanKecil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_SatuanKecil.Enabled = False
        Me.Txt_SatuanKecil.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_SatuanKecil.Location = New System.Drawing.Point(944, 96)
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
        Me.UNIX.Location = New System.Drawing.Point(935, 123)
        Me.UNIX.MaxLength = 50
        Me.UNIX.Name = "UNIX"
        Me.UNIX.ReadOnly = True
        Me.UNIX.Size = New System.Drawing.Size(249, 21)
        Me.UNIX.TabIndex = 477
        Me.UNIX.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Lbl_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(830, 51)
        Me.Panel1.TabIndex = 22
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
        Me.Panel7.Location = New System.Drawing.Point(20, 459)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(676, 15)
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
        'Barcode
        '
        Me.Barcode.Location = New System.Drawing.Point(833, 52)
        Me.Barcode.Name = "Barcode"
        Me.Barcode.Size = New System.Drawing.Size(80, 72)
        Me.Barcode.TabIndex = 463
        Me.Barcode.TabStop = False
        Me.Barcode.Visible = False
        '
        'Txt_Berat_Bags_Bersih
        '
        Me.Txt_Berat_Bags_Bersih.Location = New System.Drawing.Point(836, 180)
        Me.Txt_Berat_Bags_Bersih.Name = "Txt_Berat_Bags_Bersih"
        Me.Txt_Berat_Bags_Bersih.Size = New System.Drawing.Size(68, 20)
        Me.Txt_Berat_Bags_Bersih.TabIndex = 497
        Me.Txt_Berat_Bags_Bersih.Visible = False
        '
        'TxtBeratAlas_Bersih
        '
        Me.TxtBeratAlas_Bersih.Location = New System.Drawing.Point(836, 207)
        Me.TxtBeratAlas_Bersih.Name = "TxtBeratAlas_Bersih"
        Me.TxtBeratAlas_Bersih.Size = New System.Drawing.Size(68, 20)
        Me.TxtBeratAlas_Bersih.TabIndex = 498
        Me.TxtBeratAlas_Bersih.Visible = False
        '
        'Txt_Jumlah_Sisa_Bersih
        '
        Me.Txt_Jumlah_Sisa_Bersih.Location = New System.Drawing.Point(836, 250)
        Me.Txt_Jumlah_Sisa_Bersih.Name = "Txt_Jumlah_Sisa_Bersih"
        Me.Txt_Jumlah_Sisa_Bersih.Size = New System.Drawing.Size(68, 20)
        Me.Txt_Jumlah_Sisa_Bersih.TabIndex = 498
        Me.Txt_Jumlah_Sisa_Bersih.Visible = False
        '
        'Txt_Bags_Sisa_Bersih
        '
        Me.Txt_Bags_Sisa_Bersih.Location = New System.Drawing.Point(836, 276)
        Me.Txt_Bags_Sisa_Bersih.Name = "Txt_Bags_Sisa_Bersih"
        Me.Txt_Bags_Sisa_Bersih.Size = New System.Drawing.Size(68, 20)
        Me.Txt_Bags_Sisa_Bersih.TabIndex = 498
        Me.Txt_Bags_Sisa_Bersih.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label12.Location = New System.Drawing.Point(7, 57)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(70, 17)
        Me.Label12.TabIndex = 475
        Me.Label12.Text = "No Faktur"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Cmb_No_Faktur
        '
        Me.Cmb_No_Faktur.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cmb_No_Faktur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_No_Faktur.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_No_Faktur.FormattingEnabled = True
        Me.Cmb_No_Faktur.Location = New System.Drawing.Point(122, 54)
        Me.Cmb_No_Faktur.Name = "Cmb_No_Faktur"
        Me.Cmb_No_Faktur.Size = New System.Drawing.Size(317, 23)
        Me.Cmb_No_Faktur.TabIndex = 0
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
        Me.PanelGradient1.Size = New System.Drawing.Size(830, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'N_EMI_SD_Transaksi_Transfer_Stock_Timbang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(830, 474)
        Me.Controls.Add(Me.Txt_Bags_Sisa_Bersih)
        Me.Controls.Add(Me.Txt_Jumlah_Sisa_Bersih)
        Me.Controls.Add(Me.TxtBeratAlas_Bersih)
        Me.Controls.Add(Me.TxtBarcode)
        Me.Controls.Add(Me.Txt_Berat_Bags_Bersih)
        Me.Controls.Add(Me.Barcode)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(GroupBox1)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.txtUrutOto)
        Me.Controls.Add(Me.UNIX)
        Me.Controls.Add(Me.Txt_SatuanKecil)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TxtKdBarang)
        Me.Controls.Add(Me.txt_Barang_SN)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_SD_Transaksi_Transfer_Stock_Timbang"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.Barcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Friend WithEvents TxtBeratBersih As TextBox
    Friend WithEvents TxtBeratAlas As TextBox
    Friend WithEvents CmbJenisAlas As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtBeratBags As TextBox
    Friend WithEvents TxtJumlahBags As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtBarcode As TextBox
    Friend WithEvents TxtJumlahBagsDetail As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Txt_Berat_Bags_Bersih As TextBox
    Friend WithEvents TxtBeratAlas_Bersih As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Txt_Sisa_Bags As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Txt_Sisa_Jumlah As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Txt_Jumlah_Sisa_Bersih As TextBox
    Friend WithEvents Txt_Bags_Sisa_Bersih As TextBox
    Friend WithEvents TxtSatuan_FloorScale As TextBox
    Friend WithEvents TxtOriginal_Data_FloorScale As TextBox
    Friend WithEvents Txt_Sisa_Request As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Cmb_No_Faktur As ComboBox
    Friend WithEvents Label12 As Label
    '''Friend WithEvents StreamPlayerControl1 As WebEye.Controls.WinForms.StreamPlayerControl.StreamPlayerControl
    '''Friend WithEvents StreamPlayerControl2 As WebEye.Controls.WinForms.StreamPlayerControl.StreamPlayerControl
End Class
