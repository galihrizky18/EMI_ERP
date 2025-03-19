<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SD_Detail_Validasi_HPP
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
        Me.Lbl_Judul = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Lv_BahanBaku = New System.Windows.Forms.ListView()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Lv_Packaging = New System.Windows.Forms.ListView()
        Me.Lv_Produksi = New System.Windows.Forms.ListView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Txt_TotalBahanBaku = New System.Windows.Forms.TextBox()
        Me.Txt_TotalPackaging = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_TotalProduksi = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Lbl_Judul)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1041, 54)
        Me.Panel1.TabIndex = 26
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(15, 14)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(298, 25)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Detail Biaya - Validasi HPP"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(0, 55)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1179, 12)
        Me.Panel2.TabIndex = 40
        Me.Panel2.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(0, 77)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(12, 515)
        Me.Panel5.TabIndex = 412
        Me.Panel5.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(12, 65)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1014, 580)
        Me.TabControl1.TabIndex = 413
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.White
        Me.TabPage1.Controls.Add(Me.Txt_TotalBahanBaku)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Lv_BahanBaku)
        Me.TabPage1.Location = New System.Drawing.Point(4, 27)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1006, 549)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Bahan Baku"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.White
        Me.TabPage2.Controls.Add(Me.Txt_TotalPackaging)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.Lv_Packaging)
        Me.TabPage2.Location = New System.Drawing.Point(4, 27)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1006, 549)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Packaging"
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.White
        Me.TabPage3.Controls.Add(Me.Txt_TotalProduksi)
        Me.TabPage3.Controls.Add(Me.Label3)
        Me.TabPage3.Controls.Add(Me.Lv_Produksi)
        Me.TabPage3.Location = New System.Drawing.Point(4, 27)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(1006, 549)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Produksi"
        '
        'Lv_BahanBaku
        '
        Me.Lv_BahanBaku.FullRowSelect = True
        Me.Lv_BahanBaku.GridLines = True
        Me.Lv_BahanBaku.HideSelection = False
        Me.Lv_BahanBaku.Location = New System.Drawing.Point(6, 6)
        Me.Lv_BahanBaku.Name = "Lv_BahanBaku"
        Me.Lv_BahanBaku.Size = New System.Drawing.Size(994, 460)
        Me.Lv_BahanBaku.TabIndex = 0
        Me.Lv_BahanBaku.UseCompatibleStateImageBehavior = False
        Me.Lv_BahanBaku.View = System.Windows.Forms.View.Details
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(16, 641)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1179, 12)
        Me.Panel3.TabIndex = 40
        Me.Panel3.Visible = False
        '
        'Lv_Packaging
        '
        Me.Lv_Packaging.FullRowSelect = True
        Me.Lv_Packaging.GridLines = True
        Me.Lv_Packaging.HideSelection = False
        Me.Lv_Packaging.Location = New System.Drawing.Point(6, 7)
        Me.Lv_Packaging.Name = "Lv_Packaging"
        Me.Lv_Packaging.Size = New System.Drawing.Size(994, 460)
        Me.Lv_Packaging.TabIndex = 1
        Me.Lv_Packaging.UseCompatibleStateImageBehavior = False
        Me.Lv_Packaging.View = System.Windows.Forms.View.Details
        '
        'Lv_Produksi
        '
        Me.Lv_Produksi.FullRowSelect = True
        Me.Lv_Produksi.GridLines = True
        Me.Lv_Produksi.HideSelection = False
        Me.Lv_Produksi.Location = New System.Drawing.Point(9, 6)
        Me.Lv_Produksi.Name = "Lv_Produksi"
        Me.Lv_Produksi.Size = New System.Drawing.Size(994, 460)
        Me.Lv_Produksi.TabIndex = 1
        Me.Lv_Produksi.UseCompatibleStateImageBehavior = False
        Me.Lv_Produksi.View = System.Windows.Forms.View.Details
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(777, 499)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 18)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Total"
        '
        'Txt_TotalBahanBaku
        '
        Me.Txt_TotalBahanBaku.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_TotalBahanBaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_TotalBahanBaku.Enabled = False
        Me.Txt_TotalBahanBaku.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_TotalBahanBaku.Location = New System.Drawing.Point(831, 498)
        Me.Txt_TotalBahanBaku.MaxLength = 50
        Me.Txt_TotalBahanBaku.Name = "Txt_TotalBahanBaku"
        Me.Txt_TotalBahanBaku.Size = New System.Drawing.Size(169, 23)
        Me.Txt_TotalBahanBaku.TabIndex = 382
        Me.Txt_TotalBahanBaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Txt_TotalPackaging
        '
        Me.Txt_TotalPackaging.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_TotalPackaging.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_TotalPackaging.Enabled = False
        Me.Txt_TotalPackaging.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Txt_TotalPackaging.Location = New System.Drawing.Point(831, 499)
        Me.Txt_TotalPackaging.MaxLength = 50
        Me.Txt_TotalPackaging.Name = "Txt_TotalPackaging"
        Me.Txt_TotalPackaging.Size = New System.Drawing.Size(169, 23)
        Me.Txt_TotalPackaging.TabIndex = 384
        Me.Txt_TotalPackaging.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(777, 500)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 18)
        Me.Label2.TabIndex = 383
        Me.Label2.Text = "Total"
        '
        'Txt_TotalProduksi
        '
        Me.Txt_TotalProduksi.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_TotalProduksi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_TotalProduksi.Enabled = False
        Me.Txt_TotalProduksi.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Txt_TotalProduksi.Location = New System.Drawing.Point(825, 495)
        Me.Txt_TotalProduksi.MaxLength = 50
        Me.Txt_TotalProduksi.Name = "Txt_TotalProduksi"
        Me.Txt_TotalProduksi.Size = New System.Drawing.Size(169, 23)
        Me.Txt_TotalProduksi.TabIndex = 384
        Me.Txt_TotalProduksi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(771, 496)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 18)
        Me.Label3.TabIndex = 383
        Me.Label3.Text = "Total"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(1027, 98)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(12, 515)
        Me.Panel4.TabIndex = 412
        Me.Panel4.Visible = False
        '
        'PanelGradient1
        '
        Me.PanelGradient1.cuteColor1 = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.PanelGradient1.cuteColor2 = System.Drawing.Color.LightGreen
        Me.PanelGradient1.cuteTransparent1 = 100
        Me.PanelGradient1.cuteTransparent2 = 64
        Me.PanelGradient1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelGradient1.Location = New System.Drawing.Point(0, 52)
        Me.PanelGradient1.Margin = New System.Windows.Forms.Padding(1)
        Me.PanelGradient1.Name = "PanelGradient1"
        Me.PanelGradient1.Size = New System.Drawing.Size(1041, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'SD_Detail_Validasi_HPP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1041, 654)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "SD_Detail_Validasi_HPP"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Lv_BahanBaku As ListView
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Lv_Packaging As ListView
    Friend WithEvents Lv_Produksi As ListView
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_TotalBahanBaku As TextBox
    Friend WithEvents Txt_TotalPackaging As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_TotalProduksi As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel4 As Panel
End Class
