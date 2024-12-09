<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Display_PO_Loading_Barang
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Display_PO_Loading_Barang))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.LvRencanaOrder = New System.Windows.Forms.ListView
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.LvDetailRencanaOrder = New System.Windows.Forms.ListView
        Me.ComboBox4 = New System.Windows.Forms.ComboBox
        Me.ComboBox3 = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.ListView3 = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.LvRencanaOrder)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 33)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox1.Size = New System.Drawing.Size(829, 240)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Data Rencana Order"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.LightBlue
        Me.Label12.ForeColor = System.Drawing.Color.Blue
        Me.Label12.Location = New System.Drawing.Point(318, 221)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(31, 12)
        Me.Label12.TabIndex = 44
        Me.Label12.Text = "Finish"
        '
        'Label13
        '
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(134, 221)
        Me.Label13.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(126, 14)
        Me.Label13.TabIndex = 16
        Me.Label13.Text = "Transaksi Batal"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label8
        '
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.ForeColor = System.Drawing.Color.Blue
        Me.Label8.Location = New System.Drawing.Point(4, 221)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(126, 14)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Transaksi Tdk Batal"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LvRencanaOrder
        '
        Me.LvRencanaOrder.AllowDrop = True
        Me.LvRencanaOrder.FullRowSelect = True
        Me.LvRencanaOrder.GridLines = True
        Me.LvRencanaOrder.HideSelection = False
        Me.LvRencanaOrder.Location = New System.Drawing.Point(4, 13)
        Me.LvRencanaOrder.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.LvRencanaOrder.Name = "LvRencanaOrder"
        Me.LvRencanaOrder.Size = New System.Drawing.Size(821, 206)
        Me.LvRencanaOrder.TabIndex = 12
        Me.LvRencanaOrder.UseCompatibleStateImageBehavior = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.LvDetailRencanaOrder)
        Me.GroupBox2.Location = New System.Drawing.Point(5, 275)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GroupBox2.Size = New System.Drawing.Size(829, 193)
        Me.GroupBox2.TabIndex = 24
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Detail Rencana Order"
        '
        'LvDetailRencanaOrder
        '
        Me.LvDetailRencanaOrder.FullRowSelect = True
        Me.LvDetailRencanaOrder.GridLines = True
        Me.LvDetailRencanaOrder.Location = New System.Drawing.Point(4, 12)
        Me.LvDetailRencanaOrder.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.LvDetailRencanaOrder.Name = "LvDetailRencanaOrder"
        Me.LvDetailRencanaOrder.Size = New System.Drawing.Size(821, 171)
        Me.LvDetailRencanaOrder.TabIndex = 13
        Me.LvDetailRencanaOrder.UseCompatibleStateImageBehavior = False
        '
        'ComboBox4
        '
        Me.ComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Location = New System.Drawing.Point(976, 194)
        Me.ComboBox4.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(68, 19)
        Me.ComboBox4.TabIndex = 10
        Me.ComboBox4.Visible = False
        '
        'ComboBox3
        '
        Me.ComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Location = New System.Drawing.Point(980, 157)
        Me.ComboBox3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(129, 19)
        Me.ComboBox3.TabIndex = 9
        Me.ComboBox3.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(978, 179)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 12)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Tipe Pengurutan"
        Me.Label4.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(978, 144)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 12)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Kolom Pengurutan"
        Me.Label5.Visible = False
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(1046, 411)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(62, 19)
        Me.Button2.TabIndex = 11
        Me.Button2.Text = "E&xit"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'ListView3
        '
        Me.ListView3.BackColor = System.Drawing.Color.Snow
        Me.ListView3.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader4})
        Me.ListView3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ListView3.FullRowSelect = True
        Me.ListView3.GridLines = True
        Me.ListView3.Location = New System.Drawing.Point(980, 12)
        Me.ListView3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ListView3.Name = "ListView3"
        Me.ListView3.Size = New System.Drawing.Size(224, 129)
        Me.ListView3.TabIndex = 65
        Me.ListView3.UseCompatibleStateImageBehavior = False
        Me.ListView3.View = System.Windows.Forms.View.Details
        Me.ListView3.Visible = False
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "#"
        Me.ColumnHeader1.Width = 0
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Kode Barang"
        Me.ColumnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader2.Width = 196
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Jumlah"
        Me.ColumnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader4.Width = 49
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Maroon
        Me.Label1.Location = New System.Drawing.Point(5, -3)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.Label1.Size = New System.Drawing.Size(829, 33)
        Me.Label1.TabIndex = 69
        Me.Label1.Text = "Display Submit PO Import"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Display_PO_Loading_Barang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(5.0!, 11.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.ClientSize = New System.Drawing.Size(841, 469)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.ListView3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBox4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ComboBox3)
        Me.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Name = "Display_PO_Loading_Barang"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ".:: Display Submit PO Import ::."
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents LvRencanaOrder As System.Windows.Forms.ListView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents LvDetailRencanaOrder As System.Windows.Forms.ListView
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents ComboBox4 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox3 As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ListView3 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
End Class
