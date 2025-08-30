<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Display_Data_Penawaran_Barang_Lain
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
        Me.components = New System.ComponentModel.Container()
        Me.TabControl = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.BtnPenawaranTampil = New System.Windows.Forms.Button()
        Me.CmbpenawaranAktif = New System.Windows.Forms.ComboBox()
        Me.LblPenawaranAktif = New System.Windows.Forms.Label()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Lv_Penawaran_Detail = New System.Windows.Forms.ListView()
        Me.Lbl_SatuanBrg = New System.Windows.Forms.Label()
        Me.Lbl_NmBrg = New System.Windows.Forms.Label()
        Me.Lbl_GetKdBrg = New System.Windows.Forms.Label()
        Me.Lbl_BindingLokasiGudang = New System.Windows.Forms.Label()
        Me.LvAutoCompleteSupplier = New System.Windows.Forms.ListView()
        Me.Lbl_NmSupplier = New System.Windows.Forms.Label()
        Me.Lbl_KdSupplier = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Lbl_Kolom = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Lv_Penawaran = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AkhiriPenawaranToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.BtnOngkirTampil = New System.Windows.Forms.Button()
        Me.CmbOngkirAktif = New System.Windows.Forms.ComboBox()
        Me.LblOngkirAktif = New System.Windows.Forms.Label()
        Me.BtnOngkir_Refresh = New System.Windows.Forms.Button()
        Me.Lbl_NmEkspedisi = New System.Windows.Forms.Label()
        Me.Lbl_KdEkspedisi = New System.Windows.Forms.Label()
        Me.Lbl_IdEkspedisi = New System.Windows.Forms.Label()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Lv_AutoCompleteNmEkspedisi = New System.Windows.Forms.ListView()
        Me.CmbOngkir_Kolom = New System.Windows.Forms.ComboBox()
        Me.BtnOngkir_Cari = New System.Windows.Forms.Button()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.LblOngkir_Kolom = New System.Windows.Forms.Label()
        Me.TxtOngkir_Value = New System.Windows.Forms.TextBox()
        Me.Lv_Ongkir = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AkhiriPenawaranToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabControl.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.TabPage1)
        Me.TabControl.Controls.Add(Me.TabPage2)
        Me.TabControl.Location = New System.Drawing.Point(1, 50)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(1374, 777)
        Me.TabControl.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.BtnPenawaranTampil)
        Me.TabPage1.Controls.Add(Me.CmbpenawaranAktif)
        Me.TabPage1.Controls.Add(Me.LblPenawaranAktif)
        Me.TabPage1.Controls.Add(Me.Btn_Refresh)
        Me.TabPage1.Controls.Add(Me.Panel6)
        Me.TabPage1.Controls.Add(Me.Panel5)
        Me.TabPage1.Controls.Add(Me.Lv_Penawaran_Detail)
        Me.TabPage1.Controls.Add(Me.Lbl_SatuanBrg)
        Me.TabPage1.Controls.Add(Me.Lbl_NmBrg)
        Me.TabPage1.Controls.Add(Me.Lbl_GetKdBrg)
        Me.TabPage1.Controls.Add(Me.Lbl_BindingLokasiGudang)
        Me.TabPage1.Controls.Add(Me.LvAutoCompleteSupplier)
        Me.TabPage1.Controls.Add(Me.Lbl_NmSupplier)
        Me.TabPage1.Controls.Add(Me.Lbl_KdSupplier)
        Me.TabPage1.Controls.Add(Me.Panel4)
        Me.TabPage1.Controls.Add(Me.ComboBox1)
        Me.TabPage1.Controls.Add(Me.Btn_Cari)
        Me.TabPage1.Controls.Add(Me.Panel3)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Panel2)
        Me.TabPage1.Controls.Add(Me.Lbl_Kolom)
        Me.TabPage1.Controls.Add(Me.TextBox3)
        Me.TabPage1.Controls.Add(Me.Lv_Penawaran)
        Me.TabPage1.Location = New System.Drawing.Point(4, 27)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1366, 746)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Display Penawaran"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'BtnPenawaranTampil
        '
        Me.BtnPenawaranTampil.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnPenawaranTampil.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
        Me.BtnPenawaranTampil.ForeColor = System.Drawing.Color.White
        Me.BtnPenawaranTampil.Location = New System.Drawing.Point(779, 15)
        Me.BtnPenawaranTampil.Name = "BtnPenawaranTampil"
        Me.BtnPenawaranTampil.Size = New System.Drawing.Size(91, 28)
        Me.BtnPenawaranTampil.TabIndex = 421
        Me.BtnPenawaranTampil.Text = "Tampil Semua"
        Me.BtnPenawaranTampil.UseVisualStyleBackColor = False
        '
        'CmbpenawaranAktif
        '
        Me.CmbpenawaranAktif.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbpenawaranAktif.DropDownWidth = 150
        Me.CmbpenawaranAktif.Font = New System.Drawing.Font("Work Sans", 8.9!)
        Me.CmbpenawaranAktif.FormattingEnabled = True
        Me.CmbpenawaranAktif.Location = New System.Drawing.Point(591, 17)
        Me.CmbpenawaranAktif.Margin = New System.Windows.Forms.Padding(2)
        Me.CmbpenawaranAktif.Name = "CmbpenawaranAktif"
        Me.CmbpenawaranAktif.Size = New System.Drawing.Size(103, 30)
        Me.CmbpenawaranAktif.TabIndex = 420
        '
        'LblPenawaranAktif
        '
        Me.LblPenawaranAktif.AutoSize = True
        Me.LblPenawaranAktif.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblPenawaranAktif.Location = New System.Drawing.Point(536, 18)
        Me.LblPenawaranAktif.Name = "LblPenawaranAktif"
        Me.LblPenawaranAktif.Size = New System.Drawing.Size(63, 25)
        Me.LblPenawaranAktif.TabIndex = 419
        Me.LblPenawaranAktif.Text = "Kolom"
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(922, 15)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(80, 28)
        Me.Btn_Refresh.TabIndex = 418
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(19, 326)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1436, 15)
        Me.Panel6.TabIndex = 399
        Me.Panel6.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1003, 15)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 715)
        Me.Panel5.TabIndex = 397
        Me.Panel5.Visible = False
        '
        'Lv_Penawaran_Detail
        '
        Me.Lv_Penawaran_Detail.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Lv_Penawaran_Detail.FullRowSelect = True
        Me.Lv_Penawaran_Detail.GridLines = True
        Me.Lv_Penawaran_Detail.HideSelection = False
        Me.Lv_Penawaran_Detail.Location = New System.Drawing.Point(21, 342)
        Me.Lv_Penawaran_Detail.Name = "Lv_Penawaran_Detail"
        Me.Lv_Penawaran_Detail.Size = New System.Drawing.Size(981, 388)
        Me.Lv_Penawaran_Detail.TabIndex = 417
        Me.Lv_Penawaran_Detail.UseCompatibleStateImageBehavior = False
        Me.Lv_Penawaran_Detail.View = System.Windows.Forms.View.Details
        '
        'Lbl_SatuanBrg
        '
        Me.Lbl_SatuanBrg.AutoSize = True
        Me.Lbl_SatuanBrg.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_SatuanBrg.Location = New System.Drawing.Point(1032, 474)
        Me.Lbl_SatuanBrg.Name = "Lbl_SatuanBrg"
        Me.Lbl_SatuanBrg.Size = New System.Drawing.Size(176, 25)
        Me.Lbl_SatuanBrg.TabIndex = 416
        Me.Lbl_SatuanBrg.Text = "Get_Satuan_Barang"
        Me.Lbl_SatuanBrg.Visible = False
        '
        'Lbl_NmBrg
        '
        Me.Lbl_NmBrg.AutoSize = True
        Me.Lbl_NmBrg.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_NmBrg.Location = New System.Drawing.Point(1032, 433)
        Me.Lbl_NmBrg.Name = "Lbl_NmBrg"
        Me.Lbl_NmBrg.Size = New System.Drawing.Size(165, 25)
        Me.Lbl_NmBrg.TabIndex = 415
        Me.Lbl_NmBrg.Text = "Get_Nama_Barang"
        Me.Lbl_NmBrg.Visible = False
        '
        'Lbl_GetKdBrg
        '
        Me.Lbl_GetKdBrg.AutoSize = True
        Me.Lbl_GetKdBrg.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_GetKdBrg.Location = New System.Drawing.Point(1032, 395)
        Me.Lbl_GetKdBrg.Name = "Lbl_GetKdBrg"
        Me.Lbl_GetKdBrg.Size = New System.Drawing.Size(158, 25)
        Me.Lbl_GetKdBrg.TabIndex = 414
        Me.Lbl_GetKdBrg.Text = "Get_Kode_Barang"
        Me.Lbl_GetKdBrg.Visible = False
        '
        'Lbl_BindingLokasiGudang
        '
        Me.Lbl_BindingLokasiGudang.AutoSize = True
        Me.Lbl_BindingLokasiGudang.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_BindingLokasiGudang.Location = New System.Drawing.Point(1032, 353)
        Me.Lbl_BindingLokasiGudang.Name = "Lbl_BindingLokasiGudang"
        Me.Lbl_BindingLokasiGudang.Size = New System.Drawing.Size(240, 25)
        Me.Lbl_BindingLokasiGudang.TabIndex = 413
        Me.Lbl_BindingLokasiGudang.Text = "lbl_Binding_Lokasi_Gudang"
        Me.Lbl_BindingLokasiGudang.Visible = False
        '
        'LvAutoCompleteSupplier
        '
        Me.LvAutoCompleteSupplier.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.LvAutoCompleteSupplier.FullRowSelect = True
        Me.LvAutoCompleteSupplier.GridLines = True
        Me.LvAutoCompleteSupplier.HideSelection = False
        Me.LvAutoCompleteSupplier.Location = New System.Drawing.Point(1036, 90)
        Me.LvAutoCompleteSupplier.Name = "LvAutoCompleteSupplier"
        Me.LvAutoCompleteSupplier.Size = New System.Drawing.Size(314, 259)
        Me.LvAutoCompleteSupplier.TabIndex = 410
        Me.LvAutoCompleteSupplier.UseCompatibleStateImageBehavior = False
        Me.LvAutoCompleteSupplier.View = System.Windows.Forms.View.Details
        '
        'Lbl_NmSupplier
        '
        Me.Lbl_NmSupplier.AutoSize = True
        Me.Lbl_NmSupplier.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_NmSupplier.Location = New System.Drawing.Point(1106, 39)
        Me.Lbl_NmSupplier.Name = "Lbl_NmSupplier"
        Me.Lbl_NmSupplier.Size = New System.Drawing.Size(136, 25)
        Me.Lbl_NmSupplier.TabIndex = 412
        Me.Lbl_NmSupplier.Text = "Nama Supplier"
        Me.Lbl_NmSupplier.Visible = False
        '
        'Lbl_KdSupplier
        '
        Me.Lbl_KdSupplier.AutoSize = True
        Me.Lbl_KdSupplier.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_KdSupplier.Location = New System.Drawing.Point(1106, 6)
        Me.Lbl_KdSupplier.Name = "Lbl_KdSupplier"
        Me.Lbl_KdSupplier.Size = New System.Drawing.Size(129, 25)
        Me.Lbl_KdSupplier.TabIndex = 411
        Me.Lbl_KdSupplier.Text = "Kode Supplier"
        Me.Lbl_KdSupplier.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(21, 731)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1436, 15)
        Me.Panel4.TabIndex = 398
        Me.Panel4.Visible = False
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.DropDownWidth = 150
        Me.ComboBox1.Font = New System.Drawing.Font("Work Sans", 8.9!)
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(92, 18)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(195, 30)
        Me.ComboBox1.TabIndex = 408
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(699, 15)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(80, 28)
        Me.Btn_Cari.TabIndex = 407
        Me.Btn_Cari.Text = "Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(4, 15)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 739)
        Me.Panel3.TabIndex = 395
        Me.Panel3.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(291, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 25)
        Me.Label5.TabIndex = 406
        Me.Label5.Text = "Value"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1015, 12)
        Me.Panel2.TabIndex = 393
        Me.Panel2.Visible = False
        '
        'Lbl_Kolom
        '
        Me.Lbl_Kolom.AutoSize = True
        Me.Lbl_Kolom.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Kolom.Location = New System.Drawing.Point(25, 19)
        Me.Lbl_Kolom.Name = "Lbl_Kolom"
        Me.Lbl_Kolom.Size = New System.Drawing.Size(63, 25)
        Me.Lbl_Kolom.TabIndex = 405
        Me.Lbl_Kolom.Text = "Kolom"
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Enabled = False
        Me.TextBox3.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TextBox3.Location = New System.Drawing.Point(345, 18)
        Me.TextBox3.MaxLength = 50
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(189, 25)
        Me.TextBox3.TabIndex = 404
        '
        'Lv_Penawaran
        '
        Me.Lv_Penawaran.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Lv_Penawaran.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Lv_Penawaran.FullRowSelect = True
        Me.Lv_Penawaran.GridLines = True
        Me.Lv_Penawaran.HideSelection = False
        Me.Lv_Penawaran.Location = New System.Drawing.Point(21, 49)
        Me.Lv_Penawaran.Name = "Lv_Penawaran"
        Me.Lv_Penawaran.Size = New System.Drawing.Size(981, 276)
        Me.Lv_Penawaran.TabIndex = 403
        Me.Lv_Penawaran.UseCompatibleStateImageBehavior = False
        Me.Lv_Penawaran.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AkhiriPenawaranToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(206, 30)
        '
        'AkhiriPenawaranToolStripMenuItem
        '
        Me.AkhiriPenawaranToolStripMenuItem.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.AkhiriPenawaranToolStripMenuItem.Name = "AkhiriPenawaranToolStripMenuItem"
        Me.AkhiriPenawaranToolStripMenuItem.Size = New System.Drawing.Size(205, 26)
        Me.AkhiriPenawaranToolStripMenuItem.Text = "Akhiri Penawaran"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.BtnOngkirTampil)
        Me.TabPage2.Controls.Add(Me.CmbOngkirAktif)
        Me.TabPage2.Controls.Add(Me.LblOngkirAktif)
        Me.TabPage2.Controls.Add(Me.BtnOngkir_Refresh)
        Me.TabPage2.Controls.Add(Me.Lbl_NmEkspedisi)
        Me.TabPage2.Controls.Add(Me.Lbl_KdEkspedisi)
        Me.TabPage2.Controls.Add(Me.Lbl_IdEkspedisi)
        Me.TabPage2.Controls.Add(Me.Panel15)
        Me.TabPage2.Controls.Add(Me.Lv_AutoCompleteNmEkspedisi)
        Me.TabPage2.Controls.Add(Me.CmbOngkir_Kolom)
        Me.TabPage2.Controls.Add(Me.BtnOngkir_Cari)
        Me.TabPage2.Controls.Add(Me.Panel12)
        Me.TabPage2.Controls.Add(Me.Panel13)
        Me.TabPage2.Controls.Add(Me.Label17)
        Me.TabPage2.Controls.Add(Me.Panel14)
        Me.TabPage2.Controls.Add(Me.LblOngkir_Kolom)
        Me.TabPage2.Controls.Add(Me.TxtOngkir_Value)
        Me.TabPage2.Controls.Add(Me.Lv_Ongkir)
        Me.TabPage2.Location = New System.Drawing.Point(4, 27)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1366, 746)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Display Ekspedisi"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'BtnOngkirTampil
        '
        Me.BtnOngkirTampil.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnOngkirTampil.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
        Me.BtnOngkirTampil.ForeColor = System.Drawing.Color.White
        Me.BtnOngkirTampil.Location = New System.Drawing.Point(777, 14)
        Me.BtnOngkirTampil.Name = "BtnOngkirTampil"
        Me.BtnOngkirTampil.Size = New System.Drawing.Size(91, 28)
        Me.BtnOngkirTampil.TabIndex = 422
        Me.BtnOngkirTampil.Text = "Cari"
        Me.BtnOngkirTampil.UseVisualStyleBackColor = False
        '
        'CmbOngkirAktif
        '
        Me.CmbOngkirAktif.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbOngkirAktif.DropDownWidth = 150
        Me.CmbOngkirAktif.Font = New System.Drawing.Font("Work Sans", 8.9!)
        Me.CmbOngkirAktif.FormattingEnabled = True
        Me.CmbOngkirAktif.Location = New System.Drawing.Point(589, 17)
        Me.CmbOngkirAktif.Margin = New System.Windows.Forms.Padding(2)
        Me.CmbOngkirAktif.Name = "CmbOngkirAktif"
        Me.CmbOngkirAktif.Size = New System.Drawing.Size(103, 30)
        Me.CmbOngkirAktif.TabIndex = 417
        '
        'LblOngkirAktif
        '
        Me.LblOngkirAktif.AutoSize = True
        Me.LblOngkirAktif.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblOngkirAktif.Location = New System.Drawing.Point(534, 18)
        Me.LblOngkirAktif.Name = "LblOngkirAktif"
        Me.LblOngkirAktif.Size = New System.Drawing.Size(63, 25)
        Me.LblOngkirAktif.TabIndex = 416
        Me.LblOngkirAktif.Text = "Kolom"
        '
        'BtnOngkir_Refresh
        '
        Me.BtnOngkir_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnOngkir_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnOngkir_Refresh.ForeColor = System.Drawing.Color.White
        Me.BtnOngkir_Refresh.Location = New System.Drawing.Point(913, 15)
        Me.BtnOngkir_Refresh.Name = "BtnOngkir_Refresh"
        Me.BtnOngkir_Refresh.Size = New System.Drawing.Size(84, 28)
        Me.BtnOngkir_Refresh.TabIndex = 415
        Me.BtnOngkir_Refresh.Text = "&Refresh"
        Me.BtnOngkir_Refresh.UseVisualStyleBackColor = False
        '
        'Lbl_NmEkspedisi
        '
        Me.Lbl_NmEkspedisi.AutoSize = True
        Me.Lbl_NmEkspedisi.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_NmEkspedisi.Location = New System.Drawing.Point(1062, 77)
        Me.Lbl_NmEkspedisi.Name = "Lbl_NmEkspedisi"
        Me.Lbl_NmEkspedisi.Size = New System.Drawing.Size(147, 25)
        Me.Lbl_NmEkspedisi.TabIndex = 414
        Me.Lbl_NmEkspedisi.Text = "Nama Ekspedisi"
        '
        'Lbl_KdEkspedisi
        '
        Me.Lbl_KdEkspedisi.AutoSize = True
        Me.Lbl_KdEkspedisi.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_KdEkspedisi.Location = New System.Drawing.Point(1062, 46)
        Me.Lbl_KdEkspedisi.Name = "Lbl_KdEkspedisi"
        Me.Lbl_KdEkspedisi.Size = New System.Drawing.Size(140, 25)
        Me.Lbl_KdEkspedisi.TabIndex = 413
        Me.Lbl_KdEkspedisi.Text = "Kode Ekspedisi"
        '
        'Lbl_IdEkspedisi
        '
        Me.Lbl_IdEkspedisi.AutoSize = True
        Me.Lbl_IdEkspedisi.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_IdEkspedisi.Location = New System.Drawing.Point(1062, 17)
        Me.Lbl_IdEkspedisi.Name = "Lbl_IdEkspedisi"
        Me.Lbl_IdEkspedisi.Size = New System.Drawing.Size(115, 25)
        Me.Lbl_IdEkspedisi.TabIndex = 412
        Me.Lbl_IdEkspedisi.Text = "Id Ekspedisi"
        '
        'Panel15
        '
        Me.Panel15.BackColor = System.Drawing.Color.Red
        Me.Panel15.Location = New System.Drawing.Point(2, 723)
        Me.Panel15.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(1017, 19)
        Me.Panel15.TabIndex = 348
        Me.Panel15.Visible = False
        '
        'Lv_AutoCompleteNmEkspedisi
        '
        Me.Lv_AutoCompleteNmEkspedisi.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_AutoCompleteNmEkspedisi.FullRowSelect = True
        Me.Lv_AutoCompleteNmEkspedisi.GridLines = True
        Me.Lv_AutoCompleteNmEkspedisi.HideSelection = False
        Me.Lv_AutoCompleteNmEkspedisi.Location = New System.Drawing.Point(1026, 229)
        Me.Lv_AutoCompleteNmEkspedisi.Name = "Lv_AutoCompleteNmEkspedisi"
        Me.Lv_AutoCompleteNmEkspedisi.Size = New System.Drawing.Size(399, 259)
        Me.Lv_AutoCompleteNmEkspedisi.TabIndex = 411
        Me.Lv_AutoCompleteNmEkspedisi.UseCompatibleStateImageBehavior = False
        Me.Lv_AutoCompleteNmEkspedisi.View = System.Windows.Forms.View.Details
        '
        'CmbOngkir_Kolom
        '
        Me.CmbOngkir_Kolom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbOngkir_Kolom.DropDownWidth = 150
        Me.CmbOngkir_Kolom.Font = New System.Drawing.Font("Work Sans", 8.9!)
        Me.CmbOngkir_Kolom.FormattingEnabled = True
        Me.CmbOngkir_Kolom.Location = New System.Drawing.Point(86, 17)
        Me.CmbOngkir_Kolom.Margin = New System.Windows.Forms.Padding(2)
        Me.CmbOngkir_Kolom.Name = "CmbOngkir_Kolom"
        Me.CmbOngkir_Kolom.Size = New System.Drawing.Size(195, 30)
        Me.CmbOngkir_Kolom.TabIndex = 356
        '
        'BtnOngkir_Cari
        '
        Me.BtnOngkir_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnOngkir_Cari.Font = New System.Drawing.Font("Work Sans", 8.0!, System.Drawing.FontStyle.Bold)
        Me.BtnOngkir_Cari.ForeColor = System.Drawing.Color.White
        Me.BtnOngkir_Cari.Location = New System.Drawing.Point(697, 14)
        Me.BtnOngkir_Cari.Name = "BtnOngkir_Cari"
        Me.BtnOngkir_Cari.Size = New System.Drawing.Size(80, 28)
        Me.BtnOngkir_Cari.TabIndex = 355
        Me.BtnOngkir_Cari.Text = "Cari"
        Me.BtnOngkir_Cari.UseVisualStyleBackColor = False
        '
        'Panel12
        '
        Me.Panel12.BackColor = System.Drawing.Color.Red
        Me.Panel12.Location = New System.Drawing.Point(1000, 14)
        Me.Panel12.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(19, 706)
        Me.Panel12.TabIndex = 345
        Me.Panel12.Visible = False
        '
        'Panel13
        '
        Me.Panel13.BackColor = System.Drawing.Color.Red
        Me.Panel13.Location = New System.Drawing.Point(2, 14)
        Me.Panel13.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(19, 706)
        Me.Panel13.TabIndex = 344
        Me.Panel13.Visible = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label17.Location = New System.Drawing.Point(287, 18)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(57, 25)
        Me.Label17.TabIndex = 354
        Me.Label17.Text = "Value"
        '
        'Panel14
        '
        Me.Panel14.BackColor = System.Drawing.Color.Red
        Me.Panel14.Location = New System.Drawing.Point(1, 2)
        Me.Panel14.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(1021, 12)
        Me.Panel14.TabIndex = 342
        Me.Panel14.Visible = False
        '
        'LblOngkir_Kolom
        '
        Me.LblOngkir_Kolom.AutoSize = True
        Me.LblOngkir_Kolom.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblOngkir_Kolom.Location = New System.Drawing.Point(21, 18)
        Me.LblOngkir_Kolom.Name = "LblOngkir_Kolom"
        Me.LblOngkir_Kolom.Size = New System.Drawing.Size(63, 25)
        Me.LblOngkir_Kolom.TabIndex = 353
        Me.LblOngkir_Kolom.Text = "Kolom"
        '
        'TxtOngkir_Value
        '
        Me.TxtOngkir_Value.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtOngkir_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtOngkir_Value.Enabled = False
        Me.TxtOngkir_Value.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtOngkir_Value.Location = New System.Drawing.Point(341, 17)
        Me.TxtOngkir_Value.MaxLength = 50
        Me.TxtOngkir_Value.Name = "TxtOngkir_Value"
        Me.TxtOngkir_Value.Size = New System.Drawing.Size(189, 25)
        Me.TxtOngkir_Value.TabIndex = 352
        '
        'Lv_Ongkir
        '
        Me.Lv_Ongkir.ContextMenuStrip = Me.ContextMenuStrip2
        Me.Lv_Ongkir.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.Lv_Ongkir.FullRowSelect = True
        Me.Lv_Ongkir.GridLines = True
        Me.Lv_Ongkir.HideSelection = False
        Me.Lv_Ongkir.Location = New System.Drawing.Point(25, 45)
        Me.Lv_Ongkir.Name = "Lv_Ongkir"
        Me.Lv_Ongkir.Size = New System.Drawing.Size(972, 675)
        Me.Lv_Ongkir.TabIndex = 351
        Me.Lv_Ongkir.UseCompatibleStateImageBehavior = False
        Me.Lv_Ongkir.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.ContextMenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AkhiriPenawaranToolStripMenuItem1})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(209, 30)
        '
        'AkhiriPenawaranToolStripMenuItem1
        '
        Me.AkhiriPenawaranToolStripMenuItem1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.AkhiriPenawaranToolStripMenuItem1.Name = "AkhiriPenawaranToolStripMenuItem1"
        Me.AkhiriPenawaranToolStripMenuItem1.Size = New System.Drawing.Size(208, 26)
        Me.AkhiriPenawaranToolStripMenuItem1.Text = "Akhiri Penawaran"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1026, 49)
        Me.Panel1.TabIndex = 392
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 47)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(1026, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(504, 32)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Master Data - Penawaran Barang Lain"
        '
        'Display_Data_Penawaran
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1026, 826)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Display_Data_Penawaran"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TabControl.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl As TabControl
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents CmbOngkir_Kolom As ComboBox
    Friend WithEvents BtnOngkir_Cari As Button
    Friend WithEvents Panel12 As Panel
    Friend WithEvents Panel13 As Panel
    Friend WithEvents Label17 As Label
    Friend WithEvents Panel14 As Panel
    Friend WithEvents LblOngkir_Kolom As Label
    Friend WithEvents TxtOngkir_Value As TextBox
    Friend WithEvents Lv_Ongkir As ListView
    Friend WithEvents Lv_AutoCompleteNmEkspedisi As ListView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel15 As Panel
    Friend WithEvents Lbl_NmEkspedisi As Label
    Friend WithEvents Lbl_KdEkspedisi As Label
    Friend WithEvents Lbl_IdEkspedisi As Label
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Lv_Penawaran_Detail As ListView
    Friend WithEvents Lbl_SatuanBrg As Label
    Friend WithEvents Lbl_NmBrg As Label
    Friend WithEvents Lbl_GetKdBrg As Label
    Friend WithEvents Lbl_BindingLokasiGudang As Label
    Friend WithEvents LvAutoCompleteSupplier As ListView
    Friend WithEvents Lbl_NmSupplier As Label
    Friend WithEvents Lbl_KdSupplier As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Lbl_Kolom As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Lv_Penawaran As ListView
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents BtnOngkir_Refresh As Button
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents AkhiriPenawaranToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents AkhiriPenawaranToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents CmbpenawaranAktif As ComboBox
    Friend WithEvents LblPenawaranAktif As Label
    Friend WithEvents CmbOngkirAktif As ComboBox
    Friend WithEvents LblOngkirAktif As Label
    Friend WithEvents BtnPenawaranTampil As Button
    Friend WithEvents BtnOngkirTampil As Button
End Class
