<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EMI_Display_Restock
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
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Lv_Data = New System.Windows.Forms.ListView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Cmb_Filter = New System.Windows.Forms.ComboBox()
        Me.Txt_Filter_Value = New System.Windows.Forms.TextBox()
        Me.Btn_Cari = New System.Windows.Forms.Button()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1138, 51)
        Me.Panel1.TabIndex = 25
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1138, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 11)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(197, 25)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Display - Restock"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(18, 52)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1334, 12)
        Me.Panel2.TabIndex = 320
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 52)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 489)
        Me.Panel3.TabIndex = 321
        Me.Panel3.Visible = False
        '
        'Lv_Data
        '
        Me.Lv_Data.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lv_Data.FullRowSelect = True
        Me.Lv_Data.GridLines = True
        Me.Lv_Data.HideSelection = False
        Me.Lv_Data.Location = New System.Drawing.Point(20, 105)
        Me.Lv_Data.Name = "Lv_Data"
        Me.Lv_Data.Size = New System.Drawing.Size(1098, 480)
        Me.Lv_Data.TabIndex = 322
        Me.Lv_Data.UseCompatibleStateImageBehavior = False
        Me.Lv_Data.View = System.Windows.Forms.View.Details
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(22, 68)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 20)
        Me.Label3.TabIndex = 381
        Me.Label3.Text = "Filter"
        '
        'Cmb_Filter
        '
        Me.Cmb_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Filter.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmb_Filter.FormattingEnabled = True
        Me.Cmb_Filter.Location = New System.Drawing.Point(73, 65)
        Me.Cmb_Filter.Name = "Cmb_Filter"
        Me.Cmb_Filter.Size = New System.Drawing.Size(155, 26)
        Me.Cmb_Filter.TabIndex = 382
        '
        'Txt_Filter_Value
        '
        Me.Txt_Filter_Value.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Filter_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Filter_Value.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Filter_Value.Location = New System.Drawing.Point(233, 65)
        Me.Txt_Filter_Value.MaxLength = 50
        Me.Txt_Filter_Value.Name = "Txt_Filter_Value"
        Me.Txt_Filter_Value.Size = New System.Drawing.Size(272, 23)
        Me.Txt_Filter_Value.TabIndex = 383
        '
        'Btn_Cari
        '
        Me.Btn_Cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Cari.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Cari.ForeColor = System.Drawing.Color.White
        Me.Btn_Cari.Location = New System.Drawing.Point(511, 63)
        Me.Btn_Cari.Name = "Btn_Cari"
        Me.Btn_Cari.Size = New System.Drawing.Size(102, 28)
        Me.Btn_Cari.TabIndex = 384
        Me.Btn_Cari.Text = "Cari"
        Me.Btn_Cari.UseVisualStyleBackColor = False
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(617, 63)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(102, 28)
        Me.Btn_Refresh.TabIndex = 384
        Me.Btn_Refresh.Text = "Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(22, 91)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1334, 12)
        Me.Panel4.TabIndex = 321
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(1118, 105)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(19, 489)
        Me.Panel5.TabIndex = 321
        Me.Panel5.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(22, 586)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1334, 12)
        Me.Panel6.TabIndex = 320
        Me.Panel6.Visible = False
        '
        'EMI_Display_Restock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1138, 600)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.Btn_Cari)
        Me.Controls.Add(Me.Txt_Filter_Value)
        Me.Controls.Add(Me.Cmb_Filter)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Lv_Data)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "EMI_Display_Restock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Lv_Data As ListView
    Friend WithEvents Label3 As Label
    Friend WithEvents Cmb_Filter As ComboBox
    Friend WithEvents Txt_Filter_Value As TextBox
    Friend WithEvents Btn_Cari As Button
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
End Class
