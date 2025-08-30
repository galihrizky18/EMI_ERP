<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EMI_Display_Transfer

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
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Lv_List_Barang = New System.Windows.Forms.ListView()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Txt_ScanBarcode = New System.Windows.Forms.TextBox()
        Me.Btn_TimbangFloorScale = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Lbl_GetSerialNumber = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
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
        Me.Panel1.Size = New System.Drawing.Size(1132, 51)
        Me.Panel1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(212, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "List Transfer Stock"
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
        Me.Panel4.Location = New System.Drawing.Point(1, 584)
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
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(22, 546)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(155, 36)
        Me.Btn_Refresh.TabIndex = 406
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1112, 53)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 703)
        Me.Panel5.TabIndex = 36
        Me.Panel5.Visible = False
        '
        'Lv_List_Barang
        '
        Me.Lv_List_Barang.FullRowSelect = True
        Me.Lv_List_Barang.GridLines = True
        Me.Lv_List_Barang.HideSelection = False
        Me.Lv_List_Barang.Location = New System.Drawing.Point(20, 110)
        Me.Lv_List_Barang.Name = "Lv_List_Barang"
        Me.Lv_List_Barang.Size = New System.Drawing.Size(1091, 433)
        Me.Lv_List_Barang.TabIndex = 408
        Me.Lv_List_Barang.UseCompatibleStateImageBehavior = False
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
        'Txt_ScanBarcode
        '
        Me.Txt_ScanBarcode.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_ScanBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_ScanBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_ScanBarcode.Location = New System.Drawing.Point(125, 67)
        Me.Txt_ScanBarcode.MaxLength = 50
        Me.Txt_ScanBarcode.Name = "Txt_ScanBarcode"
        Me.Txt_ScanBarcode.Size = New System.Drawing.Size(387, 26)
        Me.Txt_ScanBarcode.TabIndex = 475
        '
        'Btn_TimbangFloorScale
        '
        Me.Btn_TimbangFloorScale.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_TimbangFloorScale.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_TimbangFloorScale.ForeColor = System.Drawing.Color.White
        Me.Btn_TimbangFloorScale.Location = New System.Drawing.Point(957, 63)
        Me.Btn_TimbangFloorScale.Name = "Btn_TimbangFloorScale"
        Me.Btn_TimbangFloorScale.Size = New System.Drawing.Size(154, 32)
        Me.Btn_TimbangFloorScale.TabIndex = 476
        Me.Btn_TimbangFloorScale.Text = "&Timbang Floor Scale"
        Me.Btn_TimbangFloorScale.UseVisualStyleBackColor = False
        Me.Btn_TimbangFloorScale.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Red
        Me.Panel7.Location = New System.Drawing.Point(-38, 97)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1208, 10)
        Me.Panel7.TabIndex = 477
        Me.Panel7.Visible = False
        '
        'Lbl_GetSerialNumber
        '
        Me.Lbl_GetSerialNumber.AutoSize = True
        Me.Lbl_GetSerialNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Lbl_GetSerialNumber.Location = New System.Drawing.Point(1138, 73)
        Me.Lbl_GetSerialNumber.Name = "Lbl_GetSerialNumber"
        Me.Lbl_GetSerialNumber.Size = New System.Drawing.Size(125, 17)
        Me.Lbl_GetSerialNumber.TabIndex = 478
        Me.Lbl_GetSerialNumber.Text = "Get Serial Number"
        Me.Lbl_GetSerialNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Lbl_GetSerialNumber.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans SemiBold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(27, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 23)
        Me.Label2.TabIndex = 481
        Me.Label2.Text = "QR Code"
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1132, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Emi_Display_Transfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1132, 597)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Lbl_GetSerialNumber)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Btn_TimbangFloorScale)
        Me.Controls.Add(Me.Txt_ScanBarcode)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Lv_List_Barang)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Emi_Display_Transfer"
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
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Lv_List_Barang As ListView
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Txt_ScanBarcode As TextBox
    Friend WithEvents Btn_TimbangFloorScale As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Lbl_GetSerialNumber As Label
    Friend WithEvents Label2 As Label
End Class
