<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Import_Product_XML
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.CmbProductXML_Kolom = New System.Windows.Forms.ComboBox()
        Me.LblKlasifikasiBahan_Value = New System.Windows.Forms.Label()
        Me.LblKlasifikasiBahan_Kolom = New System.Windows.Forms.Label()
        Me.TxtProductXML_Value = New System.Windows.Forms.TextBox()
        Me.LvwProductXML_Data = New System.Windows.Forms.ListView()
        Me.Tanggal = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Jam = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Kode_Barang = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Filename = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.User = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.BtnProductXML_Refresh = New System.Windows.Forms.Button()
        Me.BtnProductXML_Cari = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.NoFaktur = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(998, 51)
        Me.Panel1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(278, 32)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Import Product XML"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(992, 12)
        Me.Panel2.TabIndex = 34
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 60)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 416)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(979, 51)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 416)
        Me.Panel5.TabIndex = 37
        Me.Panel5.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(1, 538)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1436, 15)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(132, 104)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(107, 37)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Upload"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(808, 70)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(57, 24)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Cari"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(132, 71)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(668, 25)
        Me.TextBox1.TabIndex = 362
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(20, 75)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 22)
        Me.Label2.TabIndex = 361
        Me.Label2.Text = "Lokasi File "
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'CmbProductXML_Kolom
        '
        Me.CmbProductXML_Kolom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbProductXML_Kolom.DropDownWidth = 150
        Me.CmbProductXML_Kolom.Font = New System.Drawing.Font("Work Sans", 7.5!)
        Me.CmbProductXML_Kolom.FormattingEnabled = True
        Me.CmbProductXML_Kolom.Location = New System.Drawing.Point(79, 159)
        Me.CmbProductXML_Kolom.Margin = New System.Windows.Forms.Padding(2)
        Me.CmbProductXML_Kolom.Name = "CmbProductXML_Kolom"
        Me.CmbProductXML_Kolom.Size = New System.Drawing.Size(195, 25)
        Me.CmbProductXML_Kolom.TabIndex = 373
        '
        'LblKlasifikasiBahan_Value
        '
        Me.LblKlasifikasiBahan_Value.AutoSize = True
        Me.LblKlasifikasiBahan_Value.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblKlasifikasiBahan_Value.Location = New System.Drawing.Point(287, 160)
        Me.LblKlasifikasiBahan_Value.Name = "LblKlasifikasiBahan_Value"
        Me.LblKlasifikasiBahan_Value.Size = New System.Drawing.Size(57, 25)
        Me.LblKlasifikasiBahan_Value.TabIndex = 378
        Me.LblKlasifikasiBahan_Value.Text = "Value"
        '
        'LblKlasifikasiBahan_Kolom
        '
        Me.LblKlasifikasiBahan_Kolom.AutoSize = True
        Me.LblKlasifikasiBahan_Kolom.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.LblKlasifikasiBahan_Kolom.Location = New System.Drawing.Point(22, 160)
        Me.LblKlasifikasiBahan_Kolom.Name = "LblKlasifikasiBahan_Kolom"
        Me.LblKlasifikasiBahan_Kolom.Size = New System.Drawing.Size(63, 25)
        Me.LblKlasifikasiBahan_Kolom.TabIndex = 377
        Me.LblKlasifikasiBahan_Kolom.Text = "Kolom"
        '
        'TxtProductXML_Value
        '
        Me.TxtProductXML_Value.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtProductXML_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtProductXML_Value.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.TxtProductXML_Value.Location = New System.Drawing.Point(341, 159)
        Me.TxtProductXML_Value.MaxLength = 50
        Me.TxtProductXML_Value.Name = "TxtProductXML_Value"
        Me.TxtProductXML_Value.Size = New System.Drawing.Size(189, 25)
        Me.TxtProductXML_Value.TabIndex = 374
        '
        'LvwProductXML_Data
        '
        Me.LvwProductXML_Data.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Tanggal, Me.Jam, Me.Kode_Barang, Me.Filename, Me.User, Me.NoFaktur})
        Me.LvwProductXML_Data.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.LvwProductXML_Data.FullRowSelect = True
        Me.LvwProductXML_Data.GridLines = True
        Me.LvwProductXML_Data.HideSelection = False
        Me.LvwProductXML_Data.Location = New System.Drawing.Point(21, 192)
        Me.LvwProductXML_Data.Name = "LvwProductXML_Data"
        Me.LvwProductXML_Data.Size = New System.Drawing.Size(957, 343)
        Me.LvwProductXML_Data.TabIndex = 376
        Me.LvwProductXML_Data.UseCompatibleStateImageBehavior = False
        Me.LvwProductXML_Data.View = System.Windows.Forms.View.Details
        '
        'Tanggal
        '
        Me.Tanggal.Text = "Tanggal"
        Me.Tanggal.Width = 120
        '
        'Jam
        '
        Me.Jam.Text = "Jam"
        Me.Jam.Width = 70
        '
        'Kode_Barang
        '
        Me.Kode_Barang.Text = "Kode Barang"
        Me.Kode_Barang.Width = 140
        '
        'Filename
        '
        Me.Filename.Text = "Filename"
        Me.Filename.Width = 395
        '
        'User
        '
        Me.User.Text = "User"
        Me.User.Width = 90
        '
        'BtnProductXML_Refresh
        '
        Me.BtnProductXML_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnProductXML_Refresh.Location = New System.Drawing.Point(247, 104)
        Me.BtnProductXML_Refresh.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnProductXML_Refresh.Name = "BtnProductXML_Refresh"
        Me.BtnProductXML_Refresh.Size = New System.Drawing.Size(107, 37)
        Me.BtnProductXML_Refresh.TabIndex = 3
        Me.BtnProductXML_Refresh.Text = "Refresh"
        Me.BtnProductXML_Refresh.UseVisualStyleBackColor = True
        '
        'BtnProductXML_Cari
        '
        Me.BtnProductXML_Cari.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnProductXML_Cari.Location = New System.Drawing.Point(648, 159)
        Me.BtnProductXML_Cari.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnProductXML_Cari.Name = "BtnProductXML_Cari"
        Me.BtnProductXML_Cari.Size = New System.Drawing.Size(85, 26)
        Me.BtnProductXML_Cari.TabIndex = 380
        Me.BtnProductXML_Cari.Text = "Cari"
        Me.BtnProductXML_Cari.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.DropDownWidth = 150
        Me.ComboBox1.Font = New System.Drawing.Font("Work Sans", 7.5!)
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(535, 160)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(107, 25)
        Me.ComboBox1.TabIndex = 1
        '
        'NoFaktur
        '
        Me.NoFaktur.Text = "No Faktur"
        Me.NoFaktur.Width = 90
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
        Me.PanelGradient1.Size = New System.Drawing.Size(998, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Import_Product_XML
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(998, 553)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.BtnProductXML_Cari)
        Me.Controls.Add(Me.BtnProductXML_Refresh)
        Me.Controls.Add(Me.CmbProductXML_Kolom)
        Me.Controls.Add(Me.LblKlasifikasiBahan_Value)
        Me.Controls.Add(Me.LblKlasifikasiBahan_Kolom)
        Me.Controls.Add(Me.TxtProductXML_Value)
        Me.Controls.Add(Me.LvwProductXML_Data)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Import_Product_XML"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
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
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents CmbProductXML_Kolom As ComboBox
    Friend WithEvents LblKlasifikasiBahan_Value As Label
    Friend WithEvents LblKlasifikasiBahan_Kolom As Label
    Friend WithEvents TxtProductXML_Value As TextBox
    Friend WithEvents LvwProductXML_Data As ListView
    Friend WithEvents BtnProductXML_Refresh As Button
    Friend WithEvents BtnProductXML_Cari As Button
    Friend WithEvents Tanggal As ColumnHeader
    Friend WithEvents Jam As ColumnHeader
    Friend WithEvents Kode_Barang As ColumnHeader
    Friend WithEvents Filename As ColumnHeader
    Friend WithEvents User As ColumnHeader
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents NoFaktur As ColumnHeader
End Class
