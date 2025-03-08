<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EMI_Binding_Meteran
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
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Txt_Keterangan = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Txt_Kode = New System.Windows.Forms.TextBox()
        Me.Lbl_Kd = New System.Windows.Forms.Label()
        Me.Cmb_Meteran = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Lv_WorkCenter = New System.Windows.Forms.ListView()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Btn_Simpan = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.id_bindingUpdate = New System.Windows.Forms.TextBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
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
        Me.Panel1.Size = New System.Drawing.Size(652, 51)
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
        Me.PanelGradient1.Size = New System.Drawing.Size(652, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Judul
        '
        Me.Lbl_Judul.AutoSize = True
        Me.Lbl_Judul.Font = New System.Drawing.Font("Work Sans SemiBold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Judul.Location = New System.Drawing.Point(11, 9)
        Me.Lbl_Judul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Judul.Name = "Lbl_Judul"
        Me.Lbl_Judul.Size = New System.Drawing.Size(277, 30)
        Me.Lbl_Judul.TabIndex = 0
        Me.Lbl_Judul.Text = "Master Binding - Meteran"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 56)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(12, 598)
        Me.Panel3.TabIndex = 38
        Me.Panel3.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(16, 53)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1205, 12)
        Me.Panel2.TabIndex = 39
        Me.Panel2.Visible = False
        '
        'Txt_Keterangan
        '
        Me.Txt_Keterangan.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Keterangan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Keterangan.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Keterangan.Location = New System.Drawing.Point(135, 96)
        Me.Txt_Keterangan.MaxLength = 200
        Me.Txt_Keterangan.Name = "Txt_Keterangan"
        Me.Txt_Keterangan.Size = New System.Drawing.Size(399, 22)
        Me.Txt_Keterangan.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(12, 97)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 20)
        Me.Label1.TabIndex = 234
        Me.Label1.Text = "Keterangan"
        '
        'Txt_Kode
        '
        Me.Txt_Kode.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Txt_Kode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Kode.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.Txt_Kode.Location = New System.Drawing.Point(135, 68)
        Me.Txt_Kode.MaxLength = 30
        Me.Txt_Kode.Name = "Txt_Kode"
        Me.Txt_Kode.Size = New System.Drawing.Size(399, 22)
        Me.Txt_Kode.TabIndex = 0
        '
        'Lbl_Kd
        '
        Me.Lbl_Kd.AutoSize = True
        Me.Lbl_Kd.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Lbl_Kd.Location = New System.Drawing.Point(12, 69)
        Me.Lbl_Kd.Name = "Lbl_Kd"
        Me.Lbl_Kd.Size = New System.Drawing.Size(42, 20)
        Me.Lbl_Kd.TabIndex = 235
        Me.Lbl_Kd.Text = "Kode"
        '
        'Cmb_Meteran
        '
        Me.Cmb_Meteran.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Meteran.FormattingEnabled = True
        Me.Cmb_Meteran.Location = New System.Drawing.Point(135, 124)
        Me.Cmb_Meteran.Name = "Cmb_Meteran"
        Me.Cmb_Meteran.Size = New System.Drawing.Size(399, 26)
        Me.Cmb_Meteran.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Work Sans", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(12, 127)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 20)
        Me.Label2.TabIndex = 237
        Me.Label2.Text = "No Meteran"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Lv_WorkCenter)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 165)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(631, 373)
        Me.GroupBox1.TabIndex = 238
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Work Center"
        '
        'Lv_WorkCenter
        '
        Me.Lv_WorkCenter.CheckBoxes = True
        Me.Lv_WorkCenter.FullRowSelect = True
        Me.Lv_WorkCenter.GridLines = True
        Me.Lv_WorkCenter.HideSelection = False
        Me.Lv_WorkCenter.Location = New System.Drawing.Point(6, 22)
        Me.Lv_WorkCenter.Name = "Lv_WorkCenter"
        Me.Lv_WorkCenter.Size = New System.Drawing.Size(616, 345)
        Me.Lv_WorkCenter.TabIndex = 0
        Me.Lv_WorkCenter.UseCompatibleStateImageBehavior = False
        Me.Lv_WorkCenter.View = System.Windows.Forms.View.Details
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Refresh.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Refresh.ForeColor = System.Drawing.Color.White
        Me.Btn_Refresh.Location = New System.Drawing.Point(117, 544)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(101, 36)
        Me.Btn_Refresh.TabIndex = 234
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Btn_Simpan
        '
        Me.Btn_Simpan.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_Simpan.Font = New System.Drawing.Font("Work Sans", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Simpan.ForeColor = System.Drawing.Color.White
        Me.Btn_Simpan.Location = New System.Drawing.Point(16, 544)
        Me.Btn_Simpan.Name = "Btn_Simpan"
        Me.Btn_Simpan.Size = New System.Drawing.Size(101, 36)
        Me.Btn_Simpan.TabIndex = 3
        Me.Btn_Simpan.Text = "&Simpan"
        Me.Btn_Simpan.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(641, 97)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(12, 598)
        Me.Panel4.TabIndex = 38
        Me.Panel4.Visible = False
        '
        'id_bindingUpdate
        '
        Me.id_bindingUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.id_bindingUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.id_bindingUpdate.Font = New System.Drawing.Font("Work Sans", 8.999999!)
        Me.id_bindingUpdate.Location = New System.Drawing.Point(603, 67)
        Me.id_bindingUpdate.MaxLength = 200
        Me.id_bindingUpdate.Name = "id_bindingUpdate"
        Me.id_bindingUpdate.Size = New System.Drawing.Size(37, 22)
        Me.id_bindingUpdate.TabIndex = 1
        Me.id_bindingUpdate.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(12, 581)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1205, 12)
        Me.Panel5.TabIndex = 39
        Me.Panel5.Visible = False
        '
        'EMI_Binding_Meteran
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(652, 594)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Btn_Simpan)
        Me.Controls.Add(Me.Cmb_Meteran)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.id_bindingUpdate)
        Me.Controls.Add(Me.Txt_Keterangan)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Txt_Kode)
        Me.Controls.Add(Me.Lbl_Kd)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Work Sans", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EMI_Binding_Meteran"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Judul As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Txt_Keterangan As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_Kode As TextBox
    Friend WithEvents Lbl_Kd As Label
    Friend WithEvents Cmb_Meteran As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Lv_WorkCenter As ListView
    Friend WithEvents Btn_Refresh As Button
    Friend WithEvents Btn_Simpan As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents id_bindingUpdate As TextBox
    Friend WithEvents Panel5 As Panel
End Class
