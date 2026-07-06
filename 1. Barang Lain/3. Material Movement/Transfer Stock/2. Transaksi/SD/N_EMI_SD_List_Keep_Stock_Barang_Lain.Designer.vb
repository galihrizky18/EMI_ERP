<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class N_EMI_SD_List_Keep_Stock_Barang_Lain
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
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelGradient1 = New ERP_EMI.CustomControl.PanelGradient()
        Me.Lbl_Title = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Lv_Data = New System.Windows.Forms.ListView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.BtnFirst = New System.Windows.Forms.Button()
        Me.BtnPrev = New System.Windows.Forms.Button()
        Me.BtnNext = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Chk_Belum_Selesai = New System.Windows.Forms.CheckBox()
        Me.Cmb_Filter = New System.Windows.Forms.ComboBox()
        Me.Btn_cari = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_Value_Filter = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.cmbFilter_Pr_Dept = New System.Windows.Forms.ComboBox()
        Me.btn_pr_dept = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_value_pr_dept = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Lv_PRDEPT = New System.Windows.Forms.ListView()
        Me.btn_first_pr_dept = New System.Windows.Forms.Button()
        Me.btn_prev_pr_dept = New System.Windows.Forms.Button()
        Me.btn_next_pr_dept = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelGradient1)
        Me.Panel1.Controls.Add(Me.Lbl_Title)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1130, 51)
        Me.Panel1.TabIndex = 23
        Me.ToolTip1.SetToolTip(Me.Panel1, "Limit Data berdasarkan No Split Terbaru")
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
        Me.PanelGradient1.Size = New System.Drawing.Size(1130, 2)
        Me.PanelGradient1.TabIndex = 22
        '
        'Lbl_Title
        '
        Me.Lbl_Title.AutoSize = True
        Me.Lbl_Title.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Title.Location = New System.Drawing.Point(5, 9)
        Me.Lbl_Title.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Lbl_Title.Name = "Lbl_Title"
        Me.Lbl_Title.Size = New System.Drawing.Size(379, 25)
        Me.Lbl_Title.TabIndex = 0
        Me.Lbl_Title.Text = "Display - List Transfer Barang Lain"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(6, 51)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1280, 12)
        Me.Panel2.TabIndex = 35
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Red
        Me.Panel3.Location = New System.Drawing.Point(0, 55)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(19, 669)
        Me.Panel3.TabIndex = 36
        Me.Panel3.Visible = False
        '
        'Lv_Data
        '
        Me.Lv_Data.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Lv_Data.FullRowSelect = True
        Me.Lv_Data.GridLines = True
        Me.Lv_Data.HideSelection = False
        Me.Lv_Data.Location = New System.Drawing.Point(2, 27)
        Me.Lv_Data.MultiSelect = False
        Me.Lv_Data.Name = "Lv_Data"
        Me.Lv_Data.Size = New System.Drawing.Size(1076, 470)
        Me.Lv_Data.TabIndex = 235
        Me.Lv_Data.UseCompatibleStateImageBehavior = False
        Me.Lv_Data.View = System.Windows.Forms.View.Details
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Red
        Me.Panel4.Location = New System.Drawing.Point(1111, 57)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(19, 669)
        Me.Panel4.TabIndex = 36
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(27, 629)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1280, 12)
        Me.Panel5.TabIndex = 35
        Me.Panel5.Visible = False
        '
        'BtnFirst
        '
        Me.BtnFirst.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnFirst.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnFirst.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFirst.ForeColor = System.Drawing.Color.White
        Me.BtnFirst.Location = New System.Drawing.Point(0, 503)
        Me.BtnFirst.Name = "BtnFirst"
        Me.BtnFirst.Size = New System.Drawing.Size(86, 32)
        Me.BtnFirst.TabIndex = 497
        Me.BtnFirst.Text = "&First"
        Me.BtnFirst.UseVisualStyleBackColor = False
        '
        'BtnPrev
        '
        Me.BtnPrev.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnPrev.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnPrev.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrev.ForeColor = System.Drawing.Color.White
        Me.BtnPrev.Location = New System.Drawing.Point(168, 503)
        Me.BtnPrev.Name = "BtnPrev"
        Me.BtnPrev.Size = New System.Drawing.Size(86, 32)
        Me.BtnPrev.TabIndex = 498
        Me.BtnPrev.Text = "&Prev"
        Me.BtnPrev.UseVisualStyleBackColor = False
        '
        'BtnNext
        '
        Me.BtnNext.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnNext.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnNext.ForeColor = System.Drawing.Color.White
        Me.BtnNext.Location = New System.Drawing.Point(260, 503)
        Me.BtnNext.Name = "BtnNext"
        Me.BtnNext.Size = New System.Drawing.Size(86, 32)
        Me.BtnNext.TabIndex = 499
        Me.BtnNext.Text = "&Next"
        Me.BtnNext.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(22, 67)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1090, 562)
        Me.TabControl1.TabIndex = 502
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Chk_Belum_Selesai)
        Me.TabPage1.Controls.Add(Me.Cmb_Filter)
        Me.TabPage1.Controls.Add(Me.Btn_cari)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Txt_Value_Filter)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.Panel6)
        Me.TabPage1.Controls.Add(Me.Panel9)
        Me.TabPage1.Controls.Add(Me.Lv_Data)
        Me.TabPage1.Controls.Add(Me.BtnFirst)
        Me.TabPage1.Controls.Add(Me.BtnPrev)
        Me.TabPage1.Controls.Add(Me.BtnNext)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1082, 536)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Keep Stock"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Chk_Belum_Selesai
        '
        Me.Chk_Belum_Selesai.AutoSize = True
        Me.Chk_Belum_Selesai.Location = New System.Drawing.Point(629, 6)
        Me.Chk_Belum_Selesai.Name = "Chk_Belum_Selesai"
        Me.Chk_Belum_Selesai.Size = New System.Drawing.Size(92, 17)
        Me.Chk_Belum_Selesai.TabIndex = 502
        Me.Chk_Belum_Selesai.Text = "Belum Selesai"
        Me.Chk_Belum_Selesai.UseVisualStyleBackColor = True
        '
        'Cmb_Filter
        '
        Me.Cmb_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cmb_Filter.FormattingEnabled = True
        Me.Cmb_Filter.Location = New System.Drawing.Point(131, 2)
        Me.Cmb_Filter.Name = "Cmb_Filter"
        Me.Cmb_Filter.Size = New System.Drawing.Size(156, 21)
        Me.Cmb_Filter.TabIndex = 513
        '
        'Btn_cari
        '
        Me.Btn_cari.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.Btn_cari.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_cari.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_cari.ForeColor = System.Drawing.Color.White
        Me.Btn_cari.Location = New System.Drawing.Point(540, 0)
        Me.Btn_cari.Name = "Btn_cari"
        Me.Btn_cari.Size = New System.Drawing.Size(83, 27)
        Me.Btn_cari.TabIndex = 511
        Me.Btn_cari.Text = "&Search"
        Me.Btn_cari.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(4, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 17)
        Me.Label2.TabIndex = 509
        Me.Label2.Text = "Filter"
        '
        'Txt_Value_Filter
        '
        Me.Txt_Value_Filter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Value_Filter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Txt_Value_Filter.Location = New System.Drawing.Point(292, 3)
        Me.Txt_Value_Filter.Name = "Txt_Value_Filter"
        Me.Txt_Value_Filter.Size = New System.Drawing.Size(244, 21)
        Me.Txt_Value_Filter.TabIndex = 507
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(925, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 505
        Me.Label1.Text = "Dalam Proses"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(1027, 11)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 13)
        Me.Label8.TabIndex = 506
        Me.Label8.Text = "Terpenuhi"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.LightYellow
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Location = New System.Drawing.Point(909, 13)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(12, 12)
        Me.Panel6.TabIndex = 503
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.LightGreen
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Location = New System.Drawing.Point(1011, 13)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(12, 12)
        Me.Panel9.TabIndex = 504
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.cmbFilter_Pr_Dept)
        Me.TabPage2.Controls.Add(Me.btn_pr_dept)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.txt_value_pr_dept)
        Me.TabPage2.Controls.Add(Me.Label9)
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Controls.Add(Me.Panel7)
        Me.TabPage2.Controls.Add(Me.Panel8)
        Me.TabPage2.Controls.Add(Me.Lv_PRDEPT)
        Me.TabPage2.Controls.Add(Me.btn_first_pr_dept)
        Me.TabPage2.Controls.Add(Me.btn_prev_pr_dept)
        Me.TabPage2.Controls.Add(Me.btn_next_pr_dept)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1082, 536)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "PR Departement"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'cmbFilter_Pr_Dept
        '
        Me.cmbFilter_Pr_Dept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFilter_Pr_Dept.FormattingEnabled = True
        Me.cmbFilter_Pr_Dept.Location = New System.Drawing.Point(131, 2)
        Me.cmbFilter_Pr_Dept.Name = "cmbFilter_Pr_Dept"
        Me.cmbFilter_Pr_Dept.Size = New System.Drawing.Size(156, 21)
        Me.cmbFilter_Pr_Dept.TabIndex = 531
        '
        'btn_pr_dept
        '
        Me.btn_pr_dept.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.btn_pr_dept.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_pr_dept.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btn_pr_dept.ForeColor = System.Drawing.Color.White
        Me.btn_pr_dept.Location = New System.Drawing.Point(540, 0)
        Me.btn_pr_dept.Name = "btn_pr_dept"
        Me.btn_pr_dept.Size = New System.Drawing.Size(83, 27)
        Me.btn_pr_dept.TabIndex = 529
        Me.btn_pr_dept.Text = "&Search"
        Me.btn_pr_dept.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(4, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 17)
        Me.Label6.TabIndex = 527
        Me.Label6.Text = "Filter"
        '
        'txt_value_pr_dept
        '
        Me.txt_value_pr_dept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_value_pr_dept.Enabled = False
        Me.txt_value_pr_dept.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.txt_value_pr_dept.Location = New System.Drawing.Point(292, 3)
        Me.txt_value_pr_dept.Name = "txt_value_pr_dept"
        Me.txt_value_pr_dept.Size = New System.Drawing.Size(244, 21)
        Me.txt_value_pr_dept.TabIndex = 525
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(925, 11)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 13)
        Me.Label9.TabIndex = 523
        Me.Label9.Text = "Dalam Proses"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(1027, 11)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(55, 13)
        Me.Label10.TabIndex = 524
        Me.Label10.Text = "Terpenuhi"
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.LightYellow
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Location = New System.Drawing.Point(909, 13)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(12, 12)
        Me.Panel7.TabIndex = 521
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.LightGreen
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Location = New System.Drawing.Point(1011, 13)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(12, 12)
        Me.Panel8.TabIndex = 522
        '
        'Lv_PRDEPT
        '
        Me.Lv_PRDEPT.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Lv_PRDEPT.FullRowSelect = True
        Me.Lv_PRDEPT.GridLines = True
        Me.Lv_PRDEPT.HideSelection = False
        Me.Lv_PRDEPT.Location = New System.Drawing.Point(6, 27)
        Me.Lv_PRDEPT.MultiSelect = False
        Me.Lv_PRDEPT.Name = "Lv_PRDEPT"
        Me.Lv_PRDEPT.Size = New System.Drawing.Size(1076, 470)
        Me.Lv_PRDEPT.TabIndex = 516
        Me.Lv_PRDEPT.UseCompatibleStateImageBehavior = False
        Me.Lv_PRDEPT.View = System.Windows.Forms.View.Details
        '
        'btn_first_pr_dept
        '
        Me.btn_first_pr_dept.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.btn_first_pr_dept.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_first_pr_dept.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_first_pr_dept.ForeColor = System.Drawing.Color.White
        Me.btn_first_pr_dept.Location = New System.Drawing.Point(0, 503)
        Me.btn_first_pr_dept.Name = "btn_first_pr_dept"
        Me.btn_first_pr_dept.Size = New System.Drawing.Size(86, 32)
        Me.btn_first_pr_dept.TabIndex = 517
        Me.btn_first_pr_dept.Text = "&First"
        Me.btn_first_pr_dept.UseVisualStyleBackColor = False
        '
        'btn_prev_pr_dept
        '
        Me.btn_prev_pr_dept.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.btn_prev_pr_dept.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_prev_pr_dept.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_prev_pr_dept.ForeColor = System.Drawing.Color.White
        Me.btn_prev_pr_dept.Location = New System.Drawing.Point(168, 503)
        Me.btn_prev_pr_dept.Name = "btn_prev_pr_dept"
        Me.btn_prev_pr_dept.Size = New System.Drawing.Size(86, 32)
        Me.btn_prev_pr_dept.TabIndex = 518
        Me.btn_prev_pr_dept.Text = "&Prev"
        Me.btn_prev_pr_dept.UseVisualStyleBackColor = False
        '
        'btn_next_pr_dept
        '
        Me.btn_next_pr_dept.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.btn_next_pr_dept.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_next_pr_dept.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_next_pr_dept.ForeColor = System.Drawing.Color.White
        Me.btn_next_pr_dept.Location = New System.Drawing.Point(260, 503)
        Me.btn_next_pr_dept.Name = "btn_next_pr_dept"
        Me.btn_next_pr_dept.Size = New System.Drawing.Size(86, 32)
        Me.btn_next_pr_dept.TabIndex = 519
        Me.btn_next_pr_dept.Text = "&Next"
        Me.btn_next_pr_dept.UseVisualStyleBackColor = False
        '
        'N_EMI_SD_List_Keep_Stock_Barang_Lain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1130, 641)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "N_EMI_SD_List_Keep_Stock_Barang_Lain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PanelGradient1 As CustomControl.PanelGradient
    Friend WithEvents Lbl_Title As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Lv_Data As ListView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents BtnFirst As Button
    Friend WithEvents BtnPrev As Button
    Friend WithEvents BtnNext As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Chk_Belum_Selesai As CheckBox
    Friend WithEvents Cmb_Filter As ComboBox
    Friend WithEvents Btn_cari As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_Value_Filter As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel9 As Panel
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents cmbFilter_Pr_Dept As ComboBox
    Friend WithEvents btn_pr_dept As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents txt_value_pr_dept As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Lv_PRDEPT As ListView
    Friend WithEvents btn_first_pr_dept As Button
    Friend WithEvents btn_prev_pr_dept As Button
    Friend WithEvents btn_next_pr_dept As Button
End Class
