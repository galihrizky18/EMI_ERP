<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Display_HPP
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Lv_PembelianPO = New System.Windows.Forms.ListView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Data_Kendaraan = New System.Windows.Forms.TabPage()
        Me.Lv_DataKendaraan = New System.Windows.Forms.ListView()
        Me.Data_Barang = New System.Windows.Forms.TabPage()
        Me.Lv_DataBarang = New System.Windows.Forms.ListView()
        Me.Tot_HPP = New System.Windows.Forms.TabPage()
        Me.Lv_TotHpp = New System.Windows.Forms.ListView()
        Me.Biaya_Storage = New System.Windows.Forms.TabPage()
        Me.Lv_BiayaStorage = New System.Windows.Forms.ListView()
        Me.Kurs = New System.Windows.Forms.TabPage()
        Me.Lv_Kurs = New System.Windows.Forms.ListView()
        Me.Biaya_Import = New System.Windows.Forms.TabPage()
        Me.Txt_TotFreight = New System.Windows.Forms.TextBox()
        Me.Txt_TotTdkMskHPP = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Txt_TotMskHPP = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Lv_BiayaImport = New System.Windows.Forms.ListView()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ComboBox6 = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.Data_Kendaraan.SuspendLayout()
        Me.Data_Barang.SuspendLayout()
        Me.Tot_HPP.SuspendLayout()
        Me.Biaya_Storage.SuspendLayout()
        Me.Kurs.SuspendLayout()
        Me.Biaya_Import.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1157, 51)
        Me.Panel1.TabIndex = 26
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1157, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Display HPP"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 55)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(15, 798)
        Me.Panel3.TabIndex = 37
        Me.Panel3.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(15, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1904, 12)
        Me.Panel2.TabIndex = 38
        Me.Panel2.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Lv_PembelianPO)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 55)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1129, 256)
        Me.GroupBox1.TabIndex = 39
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Data HPP"
        '
        'Lv_PembelianPO
        '
        Me.Lv_PembelianPO.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_PembelianPO.FullRowSelect = True
        Me.Lv_PembelianPO.GridLines = True
        Me.Lv_PembelianPO.HideSelection = False
        Me.Lv_PembelianPO.Location = New System.Drawing.Point(7, 22)
        Me.Lv_PembelianPO.Name = "Lv_PembelianPO"
        Me.Lv_PembelianPO.Size = New System.Drawing.Size(1116, 228)
        Me.Lv_PembelianPO.TabIndex = 0
        Me.Lv_PembelianPO.UseCompatibleStateImageBehavior = False
        Me.Lv_PembelianPO.View = System.Windows.Forms.View.Details
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TabControl1)
        Me.GroupBox2.Location = New System.Drawing.Point(15, 317)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1129, 289)
        Me.GroupBox2.TabIndex = 39
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Detail"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.Data_Kendaraan)
        Me.TabControl1.Controls.Add(Me.Data_Barang)
        Me.TabControl1.Controls.Add(Me.Tot_HPP)
        Me.TabControl1.Controls.Add(Me.Biaya_Storage)
        Me.TabControl1.Controls.Add(Me.Kurs)
        Me.TabControl1.Controls.Add(Me.Biaya_Import)
        Me.TabControl1.Font = New System.Drawing.Font("Work Sans", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(5, 18)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1118, 269)
        Me.TabControl1.TabIndex = 0
        '
        'Data_Kendaraan
        '
        Me.Data_Kendaraan.Controls.Add(Me.Lv_DataKendaraan)
        Me.Data_Kendaraan.Location = New System.Drawing.Point(4, 26)
        Me.Data_Kendaraan.Name = "Data_Kendaraan"
        Me.Data_Kendaraan.Padding = New System.Windows.Forms.Padding(3)
        Me.Data_Kendaraan.Size = New System.Drawing.Size(1110, 239)
        Me.Data_Kendaraan.TabIndex = 0
        Me.Data_Kendaraan.Text = "Data Kendaraan"
        Me.Data_Kendaraan.UseVisualStyleBackColor = True
        '
        'Lv_DataKendaraan
        '
        Me.Lv_DataKendaraan.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_DataKendaraan.FullRowSelect = True
        Me.Lv_DataKendaraan.GridLines = True
        Me.Lv_DataKendaraan.HideSelection = False
        Me.Lv_DataKendaraan.Location = New System.Drawing.Point(4, 6)
        Me.Lv_DataKendaraan.Name = "Lv_DataKendaraan"
        Me.Lv_DataKendaraan.Size = New System.Drawing.Size(1100, 228)
        Me.Lv_DataKendaraan.TabIndex = 1
        Me.Lv_DataKendaraan.UseCompatibleStateImageBehavior = False
        Me.Lv_DataKendaraan.View = System.Windows.Forms.View.Details
        '
        'Data_Barang
        '
        Me.Data_Barang.Controls.Add(Me.Lv_DataBarang)
        Me.Data_Barang.Location = New System.Drawing.Point(4, 26)
        Me.Data_Barang.Name = "Data_Barang"
        Me.Data_Barang.Padding = New System.Windows.Forms.Padding(3)
        Me.Data_Barang.Size = New System.Drawing.Size(1110, 239)
        Me.Data_Barang.TabIndex = 1
        Me.Data_Barang.Text = "Data Barang"
        Me.Data_Barang.UseVisualStyleBackColor = True
        '
        'Lv_DataBarang
        '
        Me.Lv_DataBarang.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_DataBarang.FullRowSelect = True
        Me.Lv_DataBarang.GridLines = True
        Me.Lv_DataBarang.HideSelection = False
        Me.Lv_DataBarang.Location = New System.Drawing.Point(5, 5)
        Me.Lv_DataBarang.Name = "Lv_DataBarang"
        Me.Lv_DataBarang.Size = New System.Drawing.Size(1100, 228)
        Me.Lv_DataBarang.TabIndex = 2
        Me.Lv_DataBarang.UseCompatibleStateImageBehavior = False
        Me.Lv_DataBarang.View = System.Windows.Forms.View.Details
        '
        'Tot_HPP
        '
        Me.Tot_HPP.Controls.Add(Me.Lv_TotHpp)
        Me.Tot_HPP.Location = New System.Drawing.Point(4, 26)
        Me.Tot_HPP.Name = "Tot_HPP"
        Me.Tot_HPP.Size = New System.Drawing.Size(1110, 239)
        Me.Tot_HPP.TabIndex = 2
        Me.Tot_HPP.Text = "Total HPP"
        Me.Tot_HPP.UseVisualStyleBackColor = True
        '
        'Lv_TotHpp
        '
        Me.Lv_TotHpp.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_TotHpp.FullRowSelect = True
        Me.Lv_TotHpp.GridLines = True
        Me.Lv_TotHpp.HideSelection = False
        Me.Lv_TotHpp.Location = New System.Drawing.Point(5, 5)
        Me.Lv_TotHpp.Name = "Lv_TotHpp"
        Me.Lv_TotHpp.Size = New System.Drawing.Size(1100, 228)
        Me.Lv_TotHpp.TabIndex = 3
        Me.Lv_TotHpp.UseCompatibleStateImageBehavior = False
        Me.Lv_TotHpp.View = System.Windows.Forms.View.Details
        '
        'Biaya_Storage
        '
        Me.Biaya_Storage.Controls.Add(Me.Lv_BiayaStorage)
        Me.Biaya_Storage.Location = New System.Drawing.Point(4, 26)
        Me.Biaya_Storage.Name = "Biaya_Storage"
        Me.Biaya_Storage.Size = New System.Drawing.Size(1110, 239)
        Me.Biaya_Storage.TabIndex = 4
        Me.Biaya_Storage.Text = "Biaya Storage"
        Me.Biaya_Storage.UseVisualStyleBackColor = True
        '
        'Lv_BiayaStorage
        '
        Me.Lv_BiayaStorage.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_BiayaStorage.FullRowSelect = True
        Me.Lv_BiayaStorage.GridLines = True
        Me.Lv_BiayaStorage.HideSelection = False
        Me.Lv_BiayaStorage.Location = New System.Drawing.Point(5, 5)
        Me.Lv_BiayaStorage.Name = "Lv_BiayaStorage"
        Me.Lv_BiayaStorage.Size = New System.Drawing.Size(1100, 228)
        Me.Lv_BiayaStorage.TabIndex = 5
        Me.Lv_BiayaStorage.UseCompatibleStateImageBehavior = False
        Me.Lv_BiayaStorage.View = System.Windows.Forms.View.Details
        '
        'Kurs
        '
        Me.Kurs.Controls.Add(Me.Lv_Kurs)
        Me.Kurs.Location = New System.Drawing.Point(4, 26)
        Me.Kurs.Name = "Kurs"
        Me.Kurs.Size = New System.Drawing.Size(1110, 239)
        Me.Kurs.TabIndex = 5
        Me.Kurs.Text = "Kurs"
        Me.Kurs.UseVisualStyleBackColor = True
        '
        'Lv_Kurs
        '
        Me.Lv_Kurs.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_Kurs.FullRowSelect = True
        Me.Lv_Kurs.GridLines = True
        Me.Lv_Kurs.HideSelection = False
        Me.Lv_Kurs.Location = New System.Drawing.Point(5, 5)
        Me.Lv_Kurs.Name = "Lv_Kurs"
        Me.Lv_Kurs.Size = New System.Drawing.Size(1100, 228)
        Me.Lv_Kurs.TabIndex = 6
        Me.Lv_Kurs.UseCompatibleStateImageBehavior = False
        Me.Lv_Kurs.View = System.Windows.Forms.View.Details
        '
        'Biaya_Import
        '
        Me.Biaya_Import.Controls.Add(Me.Txt_TotFreight)
        Me.Biaya_Import.Controls.Add(Me.Txt_TotTdkMskHPP)
        Me.Biaya_Import.Controls.Add(Me.Label5)
        Me.Biaya_Import.Controls.Add(Me.Txt_TotMskHPP)
        Me.Biaya_Import.Controls.Add(Me.Label4)
        Me.Biaya_Import.Controls.Add(Me.Label6)
        Me.Biaya_Import.Controls.Add(Me.Lv_BiayaImport)
        Me.Biaya_Import.Location = New System.Drawing.Point(4, 26)
        Me.Biaya_Import.Name = "Biaya_Import"
        Me.Biaya_Import.Size = New System.Drawing.Size(1110, 239)
        Me.Biaya_Import.TabIndex = 6
        Me.Biaya_Import.Text = "Biaya Import"
        Me.Biaya_Import.UseVisualStyleBackColor = True
        '
        'Txt_TotFreight
        '
        Me.Txt_TotFreight.Enabled = False
        Me.Txt_TotFreight.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Txt_TotFreight.Location = New System.Drawing.Point(908, 75)
        Me.Txt_TotFreight.Name = "Txt_TotFreight"
        Me.Txt_TotFreight.ReadOnly = True
        Me.Txt_TotFreight.Size = New System.Drawing.Size(182, 20)
        Me.Txt_TotFreight.TabIndex = 19
        Me.Txt_TotFreight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Txt_TotTdkMskHPP
        '
        Me.Txt_TotTdkMskHPP.Enabled = False
        Me.Txt_TotTdkMskHPP.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Txt_TotTdkMskHPP.Location = New System.Drawing.Point(908, 47)
        Me.Txt_TotTdkMskHPP.Name = "Txt_TotTdkMskHPP"
        Me.Txt_TotTdkMskHPP.ReadOnly = True
        Me.Txt_TotTdkMskHPP.Size = New System.Drawing.Size(182, 20)
        Me.Txt_TotTdkMskHPP.TabIndex = 20
        Me.Txt_TotTdkMskHPP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(765, 78)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 16)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Total Freight"
        '
        'Txt_TotMskHPP
        '
        Me.Txt_TotMskHPP.Enabled = False
        Me.Txt_TotMskHPP.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Txt_TotMskHPP.Location = New System.Drawing.Point(908, 19)
        Me.Txt_TotMskHPP.Name = "Txt_TotMskHPP"
        Me.Txt_TotMskHPP.ReadOnly = True
        Me.Txt_TotMskHPP.Size = New System.Drawing.Size(182, 20)
        Me.Txt_TotMskHPP.TabIndex = 21
        Me.Txt_TotMskHPP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(765, 50)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(132, 16)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "Total Tidak Masuk HPP"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(765, 22)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(99, 16)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "Total Masuk HPP"
        '
        'Lv_BiayaImport
        '
        Me.Lv_BiayaImport.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_BiayaImport.FullRowSelect = True
        Me.Lv_BiayaImport.GridLines = True
        Me.Lv_BiayaImport.HideSelection = False
        Me.Lv_BiayaImport.Location = New System.Drawing.Point(5, 5)
        Me.Lv_BiayaImport.Name = "Lv_BiayaImport"
        Me.Lv_BiayaImport.Size = New System.Drawing.Size(755, 228)
        Me.Lv_BiayaImport.TabIndex = 7
        Me.Lv_BiayaImport.UseCompatibleStateImageBehavior = False
        Me.Lv_BiayaImport.View = System.Windows.Forms.View.Details
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.TextBox1)
        Me.GroupBox3.Controls.Add(Me.ComboBox6)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.CheckBox3)
        Me.GroupBox3.Controls.Add(Me.Btn_Cari)
        Me.GroupBox3.Controls.Add(Me.DateTimePicker2)
        Me.GroupBox3.Controls.Add(Me.ComboBox2)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.CheckBox2)
        Me.GroupBox3.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox3.Controls.Add(Me.CheckBox1)
        Me.GroupBox3.Controls.Add(Me.ComboBox1)
        Me.GroupBox3.Location = New System.Drawing.Point(20, 605)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(755, 116)
        Me.GroupBox3.TabIndex = 40
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Filter Data"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(355, 83)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(295, 23)
        Me.TextBox1.TabIndex = 84
        '
        'ComboBox6
        '
        Me.ComboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox6.FormattingEnabled = True
        Me.ComboBox6.Location = New System.Drawing.Point(458, 16)
        Me.ComboBox6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ComboBox6.Name = "ComboBox6"
        Me.ComboBox6.Size = New System.Drawing.Size(291, 26)
        Me.ComboBox6.TabIndex = 30
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(308, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 18)
        Me.Label3.TabIndex = 86
        Me.Label3.Text = "Value"
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(8, 24)
        Me.CheckBox3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(133, 22)
        Me.CheckBox3.TabIndex = 9
        Me.CheckBox3.Text = "Transaksi Hari Ini"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(651, 78)
        Me.Btn_Cari.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(90, 32)
        Me.Btn_Cari.TabIndex = 8
        Me.Btn_Cari.Text = "&Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(546, 50)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(195, 23)
        Me.DateTimePicker2.TabIndex = 4
        '
        'ComboBox2
        '
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(165, 83)
        Me.ComboBox2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(134, 26)
        Me.ComboBox2.TabIndex = 83
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(512, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 18)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "s/d"
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(8, 84)
        Me.CheckBox2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(122, 22)
        Me.CheckBox2.TabIndex = 82
        Me.CheckBox2.Text = "Parameter Lain"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd MMMM yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(311, 51)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(195, 23)
        Me.DateTimePicker1.TabIndex = 3
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(8, 52)
        Me.CheckBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(143, 22)
        Me.CheckBox1.TabIndex = 1
        Me.CheckBox1.Text = "Parameter Tanggal"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(165, 49)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(134, 26)
        Me.ComboBox1.TabIndex = 2
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(22, 723)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1904, 12)
        Me.Panel4.TabIndex = 39
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1142, 77)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(15, 798)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'EMI_Display_HPP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1157, 736)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Display_HPP"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.Data_Kendaraan.ResumeLayout(False)
        Me.Data_Barang.ResumeLayout(False)
        Me.Tot_HPP.ResumeLayout(False)
        Me.Biaya_Storage.ResumeLayout(False)
        Me.Kurs.ResumeLayout(False)
        Me.Biaya_Import.ResumeLayout(False)
        Me.Biaya_Import.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents Data_Kendaraan As TabPage
    Friend WithEvents Data_Barang As TabPage
    Friend WithEvents Lv_PembelianPO As ListView
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents ComboBox6 As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Tot_HPP As TabPage
    Friend WithEvents Biaya_Storage As TabPage
    Friend WithEvents Kurs As TabPage
    Friend WithEvents Biaya_Import As TabPage
    Friend WithEvents Lv_DataKendaraan As ListView
    Friend WithEvents Lv_DataBarang As ListView
    Friend WithEvents Lv_TotHpp As ListView
    Friend WithEvents Lv_BiayaStorage As ListView
    Friend WithEvents Lv_Kurs As ListView
    Friend WithEvents Lv_BiayaImport As ListView
    Friend WithEvents Txt_TotFreight As TextBox
    Friend WithEvents Txt_TotTdkMskHPP As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Txt_TotMskHPP As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
End Class
