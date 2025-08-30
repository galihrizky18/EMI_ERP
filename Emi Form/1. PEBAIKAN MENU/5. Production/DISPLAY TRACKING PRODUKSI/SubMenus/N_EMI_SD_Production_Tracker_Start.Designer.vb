<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class N_EMI_SD_Production_Tracker_Start
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
        Me.FlPanel_Split = New System.Windows.Forms.FlowLayoutPanel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel_Isi = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Lv_Data = New System.Windows.Forms.ListView()
        Me.Cmb_Satuan = New System.Windows.Forms.ComboBox()
        Me.Dtp_Tgl = New System.Windows.Forms.DateTimePicker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Txt_NmBarang = New System.Windows.Forms.TextBox()
        Me.Txt_Jumlah = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Txt_Operator = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Txt_KdBarang = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_NoSplit = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FlPanel_Split.SuspendLayout()
        Me.Panel_Isi.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'FlPanel_Split
        '
        Me.FlPanel_Split.AutoScroll = True
        Me.FlPanel_Split.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.FlPanel_Split.Controls.Add(Me.Panel5)
        Me.FlPanel_Split.Dock = System.Windows.Forms.DockStyle.Top
        Me.FlPanel_Split.Location = New System.Drawing.Point(0, 0)
        Me.FlPanel_Split.Name = "FlPanel_Split"
        Me.FlPanel_Split.Size = New System.Drawing.Size(1184, 72)
        Me.FlPanel_Split.TabIndex = 0
        Me.FlPanel_Split.WrapContents = False
        '
        'Panel5
        '
        Me.Panel5.Location = New System.Drawing.Point(3, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(16, 60)
        Me.Panel5.TabIndex = 0
        '
        'Panel_Isi
        '
        Me.Panel_Isi.Controls.Add(Me.GroupBox1)
        Me.Panel_Isi.Controls.Add(Me.Cmb_Satuan)
        Me.Panel_Isi.Controls.Add(Me.Dtp_Tgl)
        Me.Panel_Isi.Controls.Add(Me.Panel1)
        Me.Panel_Isi.Controls.Add(Me.Panel4)
        Me.Panel_Isi.Controls.Add(Me.Panel6)
        Me.Panel_Isi.Controls.Add(Me.Panel2)
        Me.Panel_Isi.Controls.Add(Me.Panel3)
        Me.Panel_Isi.Controls.Add(Me.Txt_NmBarang)
        Me.Panel_Isi.Controls.Add(Me.Txt_Jumlah)
        Me.Panel_Isi.Controls.Add(Me.Label4)
        Me.Panel_Isi.Controls.Add(Me.Txt_Operator)
        Me.Panel_Isi.Controls.Add(Me.Label5)
        Me.Panel_Isi.Controls.Add(Me.Txt_KdBarang)
        Me.Panel_Isi.Controls.Add(Me.Label3)
        Me.Panel_Isi.Controls.Add(Me.Label2)
        Me.Panel_Isi.Controls.Add(Me.Txt_NoSplit)
        Me.Panel_Isi.Controls.Add(Me.Label1)
        Me.Panel_Isi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Isi.Location = New System.Drawing.Point(0, 72)
        Me.Panel_Isi.Name = "Panel_Isi"
        Me.Panel_Isi.Size = New System.Drawing.Size(1184, 539)
        Me.Panel_Isi.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Lv_Data)
        Me.GroupBox1.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 150)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1145, 377)
        Me.GroupBox1.TabIndex = 50
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detail Bahan dan Packaging"
        '
        'Lv_Data
        '
        Me.Lv_Data.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Lv_Data.Font = New System.Drawing.Font("Work Sans", 9.0!)
        Me.Lv_Data.FullRowSelect = True
        Me.Lv_Data.GridLines = True
        Me.Lv_Data.HideSelection = False
        Me.Lv_Data.Location = New System.Drawing.Point(6, 21)
        Me.Lv_Data.Name = "Lv_Data"
        Me.Lv_Data.Size = New System.Drawing.Size(1132, 345)
        Me.Lv_Data.TabIndex = 0
        Me.Lv_Data.UseCompatibleStateImageBehavior = False
        Me.Lv_Data.View = System.Windows.Forms.View.Details
        '
        'Cmb_Satuan
        '
        Me.Cmb_Satuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Satuan.Enabled = False
        Me.Cmb_Satuan.FormattingEnabled = True
        Me.Cmb_Satuan.Location = New System.Drawing.Point(224, 89)
        Me.Cmb_Satuan.Name = "Cmb_Satuan"
        Me.Cmb_Satuan.Size = New System.Drawing.Size(95, 24)
        Me.Cmb_Satuan.TabIndex = 49
        '
        'Dtp_Tgl
        '
        Me.Dtp_Tgl.Enabled = False
        Me.Dtp_Tgl.Location = New System.Drawing.Point(124, 39)
        Me.Dtp_Tgl.Name = "Dtp_Tgl"
        Me.Dtp_Tgl.Size = New System.Drawing.Size(302, 20)
        Me.Dtp_Tgl.TabIndex = 47
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Red
        Me.Panel1.Location = New System.Drawing.Point(18, 137)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1573, 12)
        Me.Panel1.TabIndex = 46
        Me.Panel1.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(20, 523)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1573, 15)
        Me.Panel4.TabIndex = 46
        Me.Panel4.Visible = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Red
        Me.Panel6.Location = New System.Drawing.Point(20, 0)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1573, 12)
        Me.Panel6.TabIndex = 46
        Me.Panel6.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(1165, 15)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(19, 709)
        Me.Panel2.TabIndex = 36
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(1, -1)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 709)
        Me.Panel3.TabIndex = 36
        Me.Panel3.Visible = False
        '
        'Txt_NmBarang
        '
        Me.Txt_NmBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NmBarang.Enabled = False
        Me.Txt_NmBarang.Location = New System.Drawing.Point(224, 65)
        Me.Txt_NmBarang.Name = "Txt_NmBarang"
        Me.Txt_NmBarang.Size = New System.Drawing.Size(202, 20)
        Me.Txt_NmBarang.TabIndex = 1
        '
        'Txt_Jumlah
        '
        Me.Txt_Jumlah.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Jumlah.Enabled = False
        Me.Txt_Jumlah.Location = New System.Drawing.Point(125, 91)
        Me.Txt_Jumlah.Name = "Txt_Jumlah"
        Me.Txt_Jumlah.Size = New System.Drawing.Size(97, 20)
        Me.Txt_Jumlah.TabIndex = 1
        Me.Txt_Jumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(23, 94)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 16)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Jumlah"
        '
        'Txt_Operator
        '
        Me.Txt_Operator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Operator.Enabled = False
        Me.Txt_Operator.Location = New System.Drawing.Point(124, 117)
        Me.Txt_Operator.Name = "Txt_Operator"
        Me.Txt_Operator.Size = New System.Drawing.Size(302, 20)
        Me.Txt_Operator.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(22, 120)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 16)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Operator"
        '
        'Txt_KdBarang
        '
        Me.Txt_KdBarang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_KdBarang.Enabled = False
        Me.Txt_KdBarang.Location = New System.Drawing.Point(125, 65)
        Me.Txt_KdBarang.Name = "Txt_KdBarang"
        Me.Txt_KdBarang.Size = New System.Drawing.Size(97, 20)
        Me.Txt_KdBarang.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(23, 68)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Barang"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Tanggal"
        '
        'Txt_NoSplit
        '
        Me.Txt_NoSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_NoSplit.Enabled = False
        Me.Txt_NoSplit.Location = New System.Drawing.Point(124, 13)
        Me.Txt_NoSplit.Name = "Txt_NoSplit"
        Me.Txt_NoSplit.Size = New System.Drawing.Size(302, 20)
        Me.Txt_NoSplit.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "No Transaksi"
        '
        'N_EMI_SD_Production_Tracker_Start
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1184, 611)
        Me.Controls.Add(Me.Panel_Isi)
        Me.Controls.Add(Me.FlPanel_Split)
        Me.Font = New System.Drawing.Font("Work Sans", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_SD_Production_Tracker_Start"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.FlPanel_Split.ResumeLayout(False)
        Me.Panel_Isi.ResumeLayout(False)
        Me.Panel_Isi.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FlPanel_Split As FlowLayoutPanel
    Friend WithEvents Panel_Isi As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_NoSplit As TextBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_KdBarang As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Txt_NmBarang As TextBox
    Friend WithEvents Dtp_Tgl As DateTimePicker
    Friend WithEvents Cmb_Satuan As ComboBox
    Friend WithEvents Txt_Jumlah As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Txt_Operator As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Lv_Data As ListView
    Friend WithEvents Panel5 As Panel
End Class
