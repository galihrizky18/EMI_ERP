<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Master_Kategori_Harga_Detail
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
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Lv_Data_MasterJenisMemberPerbarang = New System.Windows.Forms.ListView()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Lbl_JnsKategoriHrg = New System.Windows.Forms.Label()
        Me.Cmb_JnsKategoriHrg = New System.Windows.Forms.ComboBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Hapus = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Txt_RangeHrgMin = New System.Windows.Forms.TextBox()
        Me.Lbl_RangeHrg = New System.Windows.Forms.Label()
        Me.Txt_PersenMarkUp = New System.Windows.Forms.TextBox()
        Me.Lbl_PersenMarkUp = New System.Windows.Forms.Label()
        Me.Cmb_KategoriKemasan = New System.Windows.Forms.ComboBox()
        Me.Cmb_KategoriBerat = New System.Windows.Forms.ComboBox()
        Me.Lbl_KategoriKemasan = New System.Windows.Forms.Label()
        Me.Lbl_KategoriBerat = New System.Windows.Forms.Label()
        Me.Txt_RangeHrgMax = New System.Windows.Forms.TextBox()
        Me.Lbl_SampaiDengan = New System.Windows.Forms.Label()
        Me.Lbl_ID = New System.Windows.Forms.Label()
        Me.Cmb_KategoriProduk = New System.Windows.Forms.ComboBox()
        Me.Lbl_KategoriProduk = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Cmb_Biaya = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Txt_InputBiaya = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Lv_BiayaDetail = New System.Windows.Forms.ListView()
        Me.Btn_Tambah = New System.Windows.Forms.Button()
        Me.Lbl_IdJnsKategoriHrg = New System.Windows.Forms.Label()
        Me.Lbl_IdJnsProduk = New System.Windows.Forms.Label()
        Me.Lbl_IdJnsKemasanUtama = New System.Windows.Forms.Label()
        Me.Lbl_IdKapasitasKemasanUtama = New System.Windows.Forms.Label()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
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
        Me.Panel1.Size = New System.Drawing.Size(942, 51)
        Me.Panel1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 11)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(420, 30)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Master Data - Jenis Member Perbarang"
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
        Me.Panel3.Size = New System.Drawing.Size(19, 751)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(922, 63)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 751)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(20, 616)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1436, 15)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'Lv_Data_MasterJenisMemberPerbarang
        '
        Me.Lv_Data_MasterJenisMemberPerbarang.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_Data_MasterJenisMemberPerbarang.FullRowSelect = True
        Me.Lv_Data_MasterJenisMemberPerbarang.GridLines = True
        Me.Lv_Data_MasterJenisMemberPerbarang.HideSelection = False
        Me.Lv_Data_MasterJenisMemberPerbarang.Location = New System.Drawing.Point(21, 381)
        Me.Lv_Data_MasterJenisMemberPerbarang.Name = "Lv_Data_MasterJenisMemberPerbarang"
        Me.Lv_Data_MasterJenisMemberPerbarang.Size = New System.Drawing.Size(900, 228)
        Me.Lv_Data_MasterJenisMemberPerbarang.TabIndex = 234
        Me.Lv_Data_MasterJenisMemberPerbarang.UseCompatibleStateImageBehavior = False
        Me.Lv_Data_MasterJenisMemberPerbarang.View = System.Windows.Forms.View.Details
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Enabled = False
        Me.TextBox3.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TextBox3.Location = New System.Drawing.Point(347, 353)
        Me.TextBox3.MaxLength = 50
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(189, 22)
        Me.TextBox3.TabIndex = 12
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(22, 354)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 20)
        Me.Label4.TabIndex = 236
        Me.Label4.Text = "Kolom"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(293, 354)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 20)
        Me.Label5.TabIndex = 337
        Me.Label5.Text = "Value"
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(539, 350)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(80, 28)
        Me.Btn_Cari.TabIndex = 13
        Me.Btn_Cari.Text = "Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.DropDownWidth = 150
        Me.ComboBox1.Font = New System.Drawing.Font("Work Sans", 8.9!)
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(90, 353)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(195, 25)
        Me.ComboBox1.TabIndex = 11
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(4, 329)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(938, 19)
        Me.Panel7.TabIndex = 39
        Me.Panel7.Visible = False
        '
        'Lbl_JnsKategoriHrg
        '
        Me.Lbl_JnsKategoriHrg.AutoSize = True
        Me.Lbl_JnsKategoriHrg.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_JnsKategoriHrg.Location = New System.Drawing.Point(27, 71)
        Me.Lbl_JnsKategoriHrg.Name = "Lbl_JnsKategoriHrg"
        Me.Lbl_JnsKategoriHrg.Size = New System.Drawing.Size(150, 20)
        Me.Lbl_JnsKategoriHrg.TabIndex = 359
        Me.Lbl_JnsKategoriHrg.Text = "Jenis Kategori Harga"
        '
        'Cmb_JnsKategoriHrg
        '
        Me.Cmb_JnsKategoriHrg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_JnsKategoriHrg.FormattingEnabled = True
        Me.Cmb_JnsKategoriHrg.Items.AddRange(New Object() {"Pengolahan", "Pengisian", "Pengemasan", "Packing Akhir"})
        Me.Cmb_JnsKategoriHrg.Location = New System.Drawing.Point(305, 70)
        Me.Cmb_JnsKategoriHrg.Name = "Cmb_JnsKategoriHrg"
        Me.Cmb_JnsKategoriHrg.Size = New System.Drawing.Size(228, 24)
        Me.Cmb_JnsKategoriHrg.TabIndex = 0
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(4, 275)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(933, 12)
        Me.Panel6.TabIndex = 348
        Me.Panel6.Visible = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(391, 289)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Refresh.TabIndex = 10
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Hapus
        '
        Me.Btn_Hapus.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Hapus.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Hapus.ForeColor = System.Drawing.Color.White
        Me.Btn_Hapus.Location = New System.Drawing.Point(301, 289)
        Me.Btn_Hapus.Name = "Btn_Hapus"
        Me.Btn_Hapus.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Hapus.TabIndex = 9
        Me.Btn_Hapus.Text = "&Hapus"
        Me.Btn_Hapus.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(211, 289)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.Btn_Simpan.TabIndex = 8
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Txt_RangeHrgMin
        '
        Me.Txt_RangeHrgMin.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_RangeHrgMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_RangeHrgMin.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_RangeHrgMin.Location = New System.Drawing.Point(305, 190)
        Me.Txt_RangeHrgMin.MaxLength = 50
        Me.Txt_RangeHrgMin.Name = "Txt_RangeHrgMin"
        Me.Txt_RangeHrgMin.Size = New System.Drawing.Size(228, 22)
        Me.Txt_RangeHrgMin.TabIndex = 4
        '
        'Lbl_RangeHrg
        '
        Me.Lbl_RangeHrg.AutoSize = True
        Me.Lbl_RangeHrg.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_RangeHrg.Location = New System.Drawing.Point(27, 191)
        Me.Lbl_RangeHrg.Name = "Lbl_RangeHrg"
        Me.Lbl_RangeHrg.Size = New System.Drawing.Size(94, 20)
        Me.Lbl_RangeHrg.TabIndex = 349
        Me.Lbl_RangeHrg.Text = "Range Harga"
        '
        'Txt_PersenMarkUp
        '
        Me.Txt_PersenMarkUp.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_PersenMarkUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_PersenMarkUp.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_PersenMarkUp.Location = New System.Drawing.Point(305, 218)
        Me.Txt_PersenMarkUp.MaxLength = 50
        Me.Txt_PersenMarkUp.Name = "Txt_PersenMarkUp"
        Me.Txt_PersenMarkUp.Size = New System.Drawing.Size(228, 22)
        Me.Txt_PersenMarkUp.TabIndex = 6
        '
        'Lbl_PersenMarkUp
        '
        Me.Lbl_PersenMarkUp.AutoSize = True
        Me.Lbl_PersenMarkUp.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_PersenMarkUp.Location = New System.Drawing.Point(27, 219)
        Me.Lbl_PersenMarkUp.Name = "Lbl_PersenMarkUp"
        Me.Lbl_PersenMarkUp.Size = New System.Drawing.Size(114, 20)
        Me.Lbl_PersenMarkUp.TabIndex = 360
        Me.Lbl_PersenMarkUp.Text = "Persen MarkUp"
        '
        'Cmb_KategoriKemasan
        '
        Me.Cmb_KategoriKemasan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_KategoriKemasan.FormattingEnabled = True
        Me.Cmb_KategoriKemasan.Items.AddRange(New Object() {"Pengolahan", "Pengisian", "Pengemasan", "Packing Akhir"})
        Me.Cmb_KategoriKemasan.Location = New System.Drawing.Point(305, 130)
        Me.Cmb_KategoriKemasan.Name = "Cmb_KategoriKemasan"
        Me.Cmb_KategoriKemasan.Size = New System.Drawing.Size(228, 24)
        Me.Cmb_KategoriKemasan.TabIndex = 2
        '
        'Cmb_KategoriBerat
        '
        Me.Cmb_KategoriBerat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_KategoriBerat.FormattingEnabled = True
        Me.Cmb_KategoriBerat.Items.AddRange(New Object() {"Pengolahan", "Pengisian", "Pengemasan", "Packing Akhir"})
        Me.Cmb_KategoriBerat.Location = New System.Drawing.Point(305, 160)
        Me.Cmb_KategoriBerat.Name = "Cmb_KategoriBerat"
        Me.Cmb_KategoriBerat.Size = New System.Drawing.Size(228, 24)
        Me.Cmb_KategoriBerat.TabIndex = 3
        '
        'Lbl_KategoriKemasan
        '
        Me.Lbl_KategoriKemasan.AutoSize = True
        Me.Lbl_KategoriKemasan.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_KategoriKemasan.Location = New System.Drawing.Point(27, 131)
        Me.Lbl_KategoriKemasan.Name = "Lbl_KategoriKemasan"
        Me.Lbl_KategoriKemasan.Size = New System.Drawing.Size(131, 20)
        Me.Lbl_KategoriKemasan.TabIndex = 364
        Me.Lbl_KategoriKemasan.Text = "Kategori Kemasan"
        '
        'Lbl_KategoriBerat
        '
        Me.Lbl_KategoriBerat.AutoSize = True
        Me.Lbl_KategoriBerat.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_KategoriBerat.Location = New System.Drawing.Point(27, 161)
        Me.Lbl_KategoriBerat.Name = "Lbl_KategoriBerat"
        Me.Lbl_KategoriBerat.Size = New System.Drawing.Size(107, 20)
        Me.Lbl_KategoriBerat.TabIndex = 365
        Me.Lbl_KategoriBerat.Text = "Kategori Berat"
        '
        'Txt_RangeHrgMax
        '
        Me.Txt_RangeHrgMax.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_RangeHrgMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_RangeHrgMax.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_RangeHrgMax.Location = New System.Drawing.Point(595, 190)
        Me.Txt_RangeHrgMax.MaxLength = 50
        Me.Txt_RangeHrgMax.Name = "Txt_RangeHrgMax"
        Me.Txt_RangeHrgMax.Size = New System.Drawing.Size(228, 22)
        Me.Txt_RangeHrgMax.TabIndex = 5
        '
        'Lbl_SampaiDengan
        '
        Me.Lbl_SampaiDengan.AutoSize = True
        Me.Lbl_SampaiDengan.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_SampaiDengan.Location = New System.Drawing.Point(549, 191)
        Me.Lbl_SampaiDengan.Name = "Lbl_SampaiDengan"
        Me.Lbl_SampaiDengan.Size = New System.Drawing.Size(30, 20)
        Me.Lbl_SampaiDengan.TabIndex = 367
        Me.Lbl_SampaiDengan.Text = "s/d"
        '
        'Lbl_ID
        '
        Me.Lbl_ID.AutoSize = True
        Me.Lbl_ID.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_ID.Location = New System.Drawing.Point(722, 71)
        Me.Lbl_ID.Name = "Lbl_ID"
        Me.Lbl_ID.Size = New System.Drawing.Size(199, 20)
        Me.Lbl_ID.TabIndex = 368
        Me.Lbl_ID.Text = "Id Jenis Member Perbarang"
        Me.Lbl_ID.Visible = False
        '
        'Cmb_KategoriProduk
        '
        Me.Cmb_KategoriProduk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_KategoriProduk.FormattingEnabled = True
        Me.Cmb_KategoriProduk.Items.AddRange(New Object() {"Pengolahan", "Pengisian", "Pengemasan", "Packing Akhir"})
        Me.Cmb_KategoriProduk.Location = New System.Drawing.Point(305, 100)
        Me.Cmb_KategoriProduk.Name = "Cmb_KategoriProduk"
        Me.Cmb_KategoriProduk.Size = New System.Drawing.Size(228, 24)
        Me.Cmb_KategoriProduk.TabIndex = 1
        '
        'Lbl_KategoriProduk
        '
        Me.Lbl_KategoriProduk.AutoSize = True
        Me.Lbl_KategoriProduk.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_KategoriProduk.Location = New System.Drawing.Point(27, 101)
        Me.Lbl_KategoriProduk.Name = "Lbl_KategoriProduk"
        Me.Lbl_KategoriProduk.Size = New System.Drawing.Size(119, 20)
        Me.Lbl_KategoriProduk.TabIndex = 370
        Me.Lbl_KategoriProduk.Text = "Kategori Produk"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(27, 249)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 20)
        Me.Label1.TabIndex = 371
        Me.Label1.Text = "Persen MarkUp"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TextBox1.Location = New System.Drawing.Point(305, 248)
        Me.TextBox1.MaxLength = 50
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(228, 22)
        Me.TextBox1.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(534, 220)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(21, 20)
        Me.Label2.TabIndex = 372
        Me.Label2.Text = "%"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(535, 250)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(21, 20)
        Me.Label3.TabIndex = 373
        Me.Label3.Text = "%"
        '
        'Cmb_Biaya
        '
        Me.Cmb_Biaya.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Biaya.FormattingEnabled = True
        Me.Cmb_Biaya.Items.AddRange(New Object() {"Pengolahan", "Pengisian", "Pengemasan", "Packing Akhir"})
        Me.Cmb_Biaya.Location = New System.Drawing.Point(65, 19)
        Me.Cmb_Biaya.Name = "Cmb_Biaya"
        Me.Cmb_Biaya.Size = New System.Drawing.Size(149, 24)
        Me.Cmb_Biaya.TabIndex = 374
        Me.Cmb_Biaya.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(14, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 20)
        Me.Label6.TabIndex = 375
        Me.Label6.Text = "Biaya"
        Me.Label6.Visible = False
        '
        'Txt_InputBiaya
        '
        Me.Txt_InputBiaya.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_InputBiaya.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_InputBiaya.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_InputBiaya.Location = New System.Drawing.Point(306, 19)
        Me.Txt_InputBiaya.MaxLength = 50
        Me.Txt_InputBiaya.Name = "Txt_InputBiaya"
        Me.Txt_InputBiaya.Size = New System.Drawing.Size(131, 22)
        Me.Txt_InputBiaya.TabIndex = 376
        Me.Txt_InputBiaya.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Txt_InputBiaya.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label7.Location = New System.Drawing.Point(220, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 20)
        Me.Label7.TabIndex = 377
        Me.Label7.Text = "Nilai Biaya"
        Me.Label7.Visible = False
        '
        'Lv_BiayaDetail
        '
        Me.Lv_BiayaDetail.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_BiayaDetail.FullRowSelect = True
        Me.Lv_BiayaDetail.GridLines = True
        Me.Lv_BiayaDetail.HideSelection = False
        Me.Lv_BiayaDetail.Location = New System.Drawing.Point(18, 48)
        Me.Lv_BiayaDetail.Name = "Lv_BiayaDetail"
        Me.Lv_BiayaDetail.Size = New System.Drawing.Size(502, 155)
        Me.Lv_BiayaDetail.TabIndex = 378
        Me.Lv_BiayaDetail.UseCompatibleStateImageBehavior = False
        Me.Lv_BiayaDetail.View = System.Windows.Forms.View.Details
        Me.Lv_BiayaDetail.Visible = False
        '
        'Btn_Tambah
        '
        Me.Btn_Tambah.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Tambah.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Tambah.ForeColor = System.Drawing.Color.White
        Me.Btn_Tambah.Location = New System.Drawing.Point(443, 16)
        Me.Btn_Tambah.Name = "Btn_Tambah"
        Me.Btn_Tambah.Size = New System.Drawing.Size(77, 28)
        Me.Btn_Tambah.TabIndex = 379
        Me.Btn_Tambah.Text = "Tambah"
        Me.Btn_Tambah.UseVisualStyleBackColor = False
        Me.Btn_Tambah.Visible = False
        '
        'Lbl_IdJnsKategoriHrg
        '
        Me.Lbl_IdJnsKategoriHrg.AutoSize = True
        Me.Lbl_IdJnsKategoriHrg.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_IdJnsKategoriHrg.Location = New System.Drawing.Point(719, 94)
        Me.Lbl_IdJnsKategoriHrg.Name = "Lbl_IdJnsKategoriHrg"
        Me.Lbl_IdJnsKategoriHrg.Size = New System.Drawing.Size(177, 20)
        Me.Lbl_IdJnsKategoriHrg.TabIndex = 380
        Me.Lbl_IdJnsKategoriHrg.Text = "Id_Jenis_Kategori_Harga"
        Me.Lbl_IdJnsKategoriHrg.Visible = False
        '
        'Lbl_IdJnsProduk
        '
        Me.Lbl_IdJnsProduk.AutoSize = True
        Me.Lbl_IdJnsProduk.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_IdJnsProduk.Location = New System.Drawing.Point(719, 114)
        Me.Lbl_IdJnsProduk.Name = "Lbl_IdJnsProduk"
        Me.Lbl_IdJnsProduk.Size = New System.Drawing.Size(123, 20)
        Me.Lbl_IdJnsProduk.TabIndex = 381
        Me.Lbl_IdJnsProduk.Text = "Id_Jenis_Produk"
        Me.Lbl_IdJnsProduk.Visible = False
        '
        'Lbl_IdJnsKemasanUtama
        '
        Me.Lbl_IdJnsKemasanUtama.AutoSize = True
        Me.Lbl_IdJnsKemasanUtama.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_IdJnsKemasanUtama.Location = New System.Drawing.Point(719, 134)
        Me.Lbl_IdJnsKemasanUtama.Name = "Lbl_IdJnsKemasanUtama"
        Me.Lbl_IdJnsKemasanUtama.Size = New System.Drawing.Size(188, 20)
        Me.Lbl_IdJnsKemasanUtama.TabIndex = 382
        Me.Lbl_IdJnsKemasanUtama.Text = "Id_Jenis_Kemasan_Utama"
        Me.Lbl_IdJnsKemasanUtama.Visible = False
        '
        'Lbl_IdKapasitasKemasanUtama
        '
        Me.Lbl_IdKapasitasKemasanUtama.AutoSize = True
        Me.Lbl_IdKapasitasKemasanUtama.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_IdKapasitasKemasanUtama.Location = New System.Drawing.Point(700, 154)
        Me.Lbl_IdKapasitasKemasanUtama.Name = "Lbl_IdKapasitasKemasanUtama"
        Me.Lbl_IdKapasitasKemasanUtama.Size = New System.Drawing.Size(218, 20)
        Me.Lbl_IdKapasitasKemasanUtama.TabIndex = 383
        Me.Lbl_IdKapasitasKemasanUtama.Text = "Id_Kapasitas_Kemasan_Utama"
        Me.Lbl_IdKapasitasKemasanUtama.Visible = False
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
        Me.PanelGradient1.Size = New System.Drawing.Size(942, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Cmb_Biaya)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Txt_InputBiaya)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Lv_BiayaDetail)
        Me.GroupBox1.Controls.Add(Me.Btn_Tambah)
        Me.GroupBox1.Location = New System.Drawing.Point(942, 297)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(563, 136)
        Me.GroupBox1.TabIndex = 384
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "GroupBox1"
        Me.GroupBox1.Visible = False
        '
        'Master_Kategori_Harga_Detail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(942, 632)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Lbl_IdKapasitasKemasanUtama)
        Me.Controls.Add(Me.Lbl_IdJnsKemasanUtama)
        Me.Controls.Add(Me.Lbl_IdJnsProduk)
        Me.Controls.Add(Me.Lbl_IdJnsKategoriHrg)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Lbl_KategoriProduk)
        Me.Controls.Add(Me.Cmb_KategoriProduk)
        Me.Controls.Add(Me.Lbl_ID)
        Me.Controls.Add(Me.Lbl_SampaiDengan)
        Me.Controls.Add(Me.Txt_RangeHrgMax)
        Me.Controls.Add(Me.Lbl_KategoriBerat)
        Me.Controls.Add(Me.Lbl_KategoriKemasan)
        Me.Controls.Add(Me.Cmb_KategoriBerat)
        Me.Controls.Add(Me.Cmb_KategoriKemasan)
        Me.Controls.Add(Me.Txt_PersenMarkUp)
        Me.Controls.Add(Me.Lbl_PersenMarkUp)
        Me.Controls.Add(Me.Lbl_JnsKategoriHrg)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Cmb_JnsKategoriHrg)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Btn_Cari)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Btn_Hapus)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Lv_Data_MasterJenisMemberPerbarang)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Txt_RangeHrgMin)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Lbl_RangeHrg)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Master_Kategori_Harga_Detail"
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
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Lv_Data_MasterJenisMemberPerbarang As ListView
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Lbl_JnsKategoriHrg As Label
    Friend WithEvents Cmb_JnsKategoriHrg As ComboBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Hapus As Button
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Txt_RangeHrgMin As TextBox
    Friend WithEvents Lbl_RangeHrg As Label
    Friend WithEvents Txt_PersenMarkUp As TextBox
    Friend WithEvents Lbl_PersenMarkUp As Label
    Friend WithEvents Cmb_KategoriKemasan As ComboBox
    Friend WithEvents Cmb_KategoriBerat As ComboBox
    Friend WithEvents Lbl_KategoriKemasan As Label
    Friend WithEvents Lbl_KategoriBerat As Label
    Friend WithEvents Txt_RangeHrgMax As TextBox
    Friend WithEvents Lbl_SampaiDengan As Label
    Friend WithEvents Lbl_ID As Label
    Friend WithEvents Cmb_KategoriProduk As ComboBox
    Friend WithEvents Lbl_KategoriProduk As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Cmb_Biaya As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Txt_InputBiaya As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Lv_BiayaDetail As ListView
    Friend WithEvents Btn_Tambah As Button
    Friend WithEvents Lbl_IdJnsKategoriHrg As Label
    Friend WithEvents Lbl_IdJnsProduk As Label
    Friend WithEvents Lbl_IdJnsKemasanUtama As Label
    Friend WithEvents Lbl_IdKapasitasKemasanUtama As Label
    Friend WithEvents GroupBox1 As GroupBox
End Class
