<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SD_Detail_PajakPO_Barang_Lain
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.TxtPO_GrandTotal = New System.Windows.Forms.TextBox()
        Me.LblPO_GrandTotal = New System.Windows.Forms.Label()
        Me.Lv_Data = New System.Windows.Forms.ListView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_TotalPPH = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(560, 44)
        Me.Panel1.TabIndex = 24
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 42)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(560, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 6)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(321, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Detail - Pajak Purchase Order"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(0, 41)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1214, 12)
        Me.Panel5.TabIndex = 304
        Me.Panel5.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 55)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(12, 649)
        Me.Panel3.TabIndex = 305
        Me.Panel3.Visible = False
        '
        'TxtPO_GrandTotal
        '
        Me.TxtPO_GrandTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TxtPO_GrandTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPO_GrandTotal.Enabled = False
        Me.TxtPO_GrandTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.TxtPO_GrandTotal.Location = New System.Drawing.Point(375, 326)
        Me.TxtPO_GrandTotal.Name = "TxtPO_GrandTotal"
        Me.TxtPO_GrandTotal.Size = New System.Drawing.Size(173, 21)
        Me.TxtPO_GrandTotal.TabIndex = 323
        Me.TxtPO_GrandTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblPO_GrandTotal
        '
        Me.LblPO_GrandTotal.AutoSize = True
        Me.LblPO_GrandTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.LblPO_GrandTotal.Location = New System.Drawing.Point(256, 328)
        Me.LblPO_GrandTotal.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LblPO_GrandTotal.Name = "LblPO_GrandTotal"
        Me.LblPO_GrandTotal.Size = New System.Drawing.Size(64, 17)
        Me.LblPO_GrandTotal.TabIndex = 322
        Me.LblPO_GrandTotal.Text = "Total PO"
        '
        'Lv_Data
        '
        Me.Lv_Data.FullRowSelect = True
        Me.Lv_Data.GridLines = True
        Me.Lv_Data.HideSelection = False
        Me.Lv_Data.Location = New System.Drawing.Point(12, 50)
        Me.Lv_Data.Name = "Lv_Data"
        Me.Lv_Data.Size = New System.Drawing.Size(536, 259)
        Me.Lv_Data.TabIndex = 324
        Me.Lv_Data.UseCompatibleStateImageBehavior = False
        Me.Lv_Data.View = System.Windows.Forms.View.Details
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(548, 55)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(12, 649)
        Me.Panel2.TabIndex = 305
        Me.Panel2.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(256, 355)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 17)
        Me.Label2.TabIndex = 322
        Me.Label2.Text = "Total PPH"
        '
        'Txt_TotalPPH
        '
        Me.Txt_TotalPPH.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_TotalPPH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_TotalPPH.Enabled = False
        Me.Txt_TotalPPH.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.999999!)
        Me.Txt_TotalPPH.Location = New System.Drawing.Point(375, 353)
        Me.Txt_TotalPPH.Name = "Txt_TotalPPH"
        Me.Txt_TotalPPH.Size = New System.Drawing.Size(173, 21)
        Me.Txt_TotalPPH.TabIndex = 323
        Me.Txt_TotalPPH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(12, 310)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1214, 12)
        Me.Panel4.TabIndex = 304
        Me.Panel4.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(18, 375)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1214, 12)
        Me.Panel6.TabIndex = 304
        Me.Panel6.Visible = False
        '
        'SD_Detail_PajakPO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(560, 388)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Lv_Data)
        Me.Controls.Add(Me.Txt_TotalPPH)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtPO_GrandTotal)
        Me.Controls.Add(Me.LblPO_GrandTotal)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "SD_Detail_PajakPO"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents TxtPO_GrandTotal As TextBox
    Friend WithEvents LblPO_GrandTotal As Label
    Friend WithEvents Lv_Data As ListView
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_TotalPPH As TextBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel6 As Panel
End Class
