<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class N_EMI_Validasi_LIMS_Formula
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Lv_List_Barang = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ValidasiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Btn_TimbangFloorScale = New System.Windows.Forms.Button()
        Me.Barcode = New System.Windows.Forms.PictureBox()
        Me.Btn_Scan = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmb_Formulator = New System.Windows.Forms.ComboBox()
        Me.TxtValPencarian = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.Barcode, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(1045, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1045, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(233, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Validasi - Formulator"
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
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(1, 618)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1225, 12)
        Me.Panel4.TabIndex = 35
        Me.Panel4.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, 55)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 695)
        Me.Panel3.TabIndex = 35
        Me.Panel3.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1025, 53)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 703)
        Me.Panel5.TabIndex = 36
        Me.Panel5.Visible = False
        '
        'Lv_List_Barang
        '
        Me.Lv_List_Barang.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Lv_List_Barang.FullRowSelect = True
        Me.Lv_List_Barang.GridLines = True
        Me.Lv_List_Barang.HideSelection = False
        Me.Lv_List_Barang.Location = New System.Drawing.Point(20, 98)
        Me.Lv_List_Barang.Name = "Lv_List_Barang"
        Me.Lv_List_Barang.Size = New System.Drawing.Size(1005, 520)
        Me.Lv_List_Barang.TabIndex = 408
        Me.Lv_List_Barang.UseCompatibleStateImageBehavior = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ValidasiToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(114, 26)
        '
        'ValidasiToolStripMenuItem
        '
        Me.ValidasiToolStripMenuItem.Name = "ValidasiToolStripMenuItem"
        Me.ValidasiToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
        Me.ValidasiToolStripMenuItem.Text = "Validasi"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(1400, 53)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(19, 687)
        Me.Panel6.TabIndex = 409
        Me.Panel6.Visible = False
        '
        'Btn_TimbangFloorScale
        '
        Me.Btn_TimbangFloorScale.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_TimbangFloorScale.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_TimbangFloorScale.ForeColor = System.Drawing.Color.White
        Me.Btn_TimbangFloorScale.Location = New System.Drawing.Point(1449, 174)
        Me.Btn_TimbangFloorScale.Name = "Btn_TimbangFloorScale"
        Me.Btn_TimbangFloorScale.Size = New System.Drawing.Size(154, 32)
        Me.Btn_TimbangFloorScale.TabIndex = 476
        Me.Btn_TimbangFloorScale.Text = "&Timbang Floor Scale"
        Me.Btn_TimbangFloorScale.UseVisualStyleBackColor = False
        Me.Btn_TimbangFloorScale.Visible = False
        '
        'Barcode
        '
        Me.Barcode.Location = New System.Drawing.Point(1609, 174)
        Me.Barcode.Name = "Barcode"
        Me.Barcode.Size = New System.Drawing.Size(79, 32)
        Me.Barcode.TabIndex = 479
        Me.Barcode.TabStop = False
        Me.Barcode.Visible = False
        '
        'Btn_Scan
        '
        Me.Btn_Scan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Scan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Scan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Scan.ForeColor = System.Drawing.Color.White
        Me.Btn_Scan.Location = New System.Drawing.Point(420, 63)
        Me.Btn_Scan.Margin = New System.Windows.Forms.Padding(2)
        Me.Btn_Scan.Name = "Btn_Scan"
        Me.Btn_Scan.Size = New System.Drawing.Size(96, 30)
        Me.Btn_Scan.TabIndex = 483
        Me.Btn_Scan.Text = "Cari"
        Me.Btn_Scan.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(26, 68)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 17)
        Me.Label3.TabIndex = 484
        Me.Label3.Text = "Parameter"
        '
        'cmb_Formulator
        '
        Me.cmb_Formulator.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmb_Formulator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Formulator.FormattingEnabled = True
        Me.cmb_Formulator.Location = New System.Drawing.Point(104, 66)
        Me.cmb_Formulator.Margin = New System.Windows.Forms.Padding(2)
        Me.cmb_Formulator.Name = "cmb_Formulator"
        Me.cmb_Formulator.Size = New System.Drawing.Size(115, 24)
        Me.cmb_Formulator.TabIndex = 481
        '
        'TxtValPencarian
        '
        Me.TxtValPencarian.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtValPencarian.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtValPencarian.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtValPencarian.Location = New System.Drawing.Point(223, 67)
        Me.TxtValPencarian.Margin = New System.Windows.Forms.Padding(2)
        Me.TxtValPencarian.MaxLength = 50
        Me.TxtValPencarian.Name = "TxtValPencarian"
        Me.TxtValPencarian.Size = New System.Drawing.Size(193, 21)
        Me.TxtValPencarian.TabIndex = 482
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(520, 63)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(96, 30)
        Me.Button1.TabIndex = 486
        Me.Button1.Text = "Refresh"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'N_EMI_Validasi_LIMS_Formula
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1045, 630)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Btn_Scan)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmb_Formulator)
        Me.Controls.Add(Me.TxtValPencarian)
        Me.Controls.Add(Me.Barcode)
        Me.Controls.Add(Me.Btn_TimbangFloorScale)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Lv_List_Barang)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_Validasi_LIMS_Formula"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.Barcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Lv_List_Barang As ListView
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Btn_TimbangFloorScale As Button
    Friend WithEvents Barcode As PictureBox
    Friend WithEvents Btn_Scan As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents cmb_Formulator As ComboBox
    Friend WithEvents TxtValPencarian As TextBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ValidasiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Button1 As Button
End Class
