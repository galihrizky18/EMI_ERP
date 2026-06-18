<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Server_Sinkronasi_B2B
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
		Me.Button1 = New System.Windows.Forms.Button()
		Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Button22 = New System.Windows.Forms.Button()
		Me.Button23 = New System.Windows.Forms.Button()
		Me.ListView1 = New System.Windows.Forms.ListView()
		Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.Button2 = New System.Windows.Forms.Button()
		Me.Button3 = New System.Windows.Forms.Button()
		Me.Button4 = New System.Windows.Forms.Button()
		Me.btnSupplierInsert = New System.Windows.Forms.Button()
		Me.btnPnwrBahanBaku = New System.Windows.Forms.Button()
		Me.btnPenawaranPackaging = New System.Windows.Forms.Button()
		Me.Button5 = New System.Windows.Forms.Button()
		Me.Btn_BiayaLokal = New System.Windows.Forms.Button()
		Me.Btn_InsExpedisi = New System.Windows.Forms.Button()
		Me.Btn_Update_BiayaLokal = New System.Windows.Forms.Button()
		Me.Btn_InsKendaraan = New System.Windows.Forms.Button()
		Me.Btn_UpdateKendaraan = New System.Windows.Forms.Button()
		Me.btnPerusahaanBIaya = New System.Windows.Forms.Button()
		Me.btn_Master_Satuan = New System.Windows.Forms.Button()
		Me.Button6 = New System.Windows.Forms.Button()
		Me.Button7 = New System.Windows.Forms.Button()
		Me.Button8 = New System.Windows.Forms.Button()
		Me.Button9 = New System.Windows.Forms.Button()
		Me.ContextMenuStrip1.SuspendLayout()
		Me.SuspendLayout()
		'
		'Button1
		'
		Me.Button1.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.Button1.Location = New System.Drawing.Point(12, 52)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(173, 34)
		Me.Button1.TabIndex = 0
		Me.Button1.Text = "INSERT | Purchase Order (Supplier)"
		Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.Button1.UseVisualStyleBackColor = False
		'
		'Timer1
		'
		Me.Timer1.Enabled = True
		Me.Timer1.Interval = 180000
		'
		'Label4
		'
		Me.Label4.BackColor = System.Drawing.Color.White
		Me.Label4.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.ForeColor = System.Drawing.Color.Maroon
		Me.Label4.Location = New System.Drawing.Point(12, 9)
		Me.Label4.Name = "Label4"
		Me.Label4.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
		Me.Label4.Size = New System.Drawing.Size(553, 33)
		Me.Label4.TabIndex = 77
		Me.Label4.Text = "Dari SQL (B2B) Ke SqlServer (EMI)"
		Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'Label1
		'
		Me.Label1.BackColor = System.Drawing.Color.White
		Me.Label1.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.ForeColor = System.Drawing.Color.Maroon
		Me.Label1.Location = New System.Drawing.Point(600, 9)
		Me.Label1.Name = "Label1"
		Me.Label1.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
		Me.Label1.Size = New System.Drawing.Size(406, 33)
		Me.Label1.TabIndex = 78
		Me.Label1.Text = "Dari SQL (EMI) Ke SqlServer (B2B)"
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'Button22
		'
		Me.Button22.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.Button22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Button22.ForeColor = System.Drawing.Color.Maroon
		Me.Button22.Location = New System.Drawing.Point(17, 418)
		Me.Button22.Name = "Button22"
		Me.Button22.Size = New System.Drawing.Size(553, 34)
		Me.Button22.TabIndex = 79
		Me.Button22.Text = "BUTTON 'Dari MySQL to SqlServer'"
		Me.Button22.UseVisualStyleBackColor = False
		'
		'Button23
		'
		Me.Button23.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.Button23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
		Me.Button23.ForeColor = System.Drawing.Color.Maroon
		Me.Button23.Location = New System.Drawing.Point(609, 417)
		Me.Button23.Name = "Button23"
		Me.Button23.Size = New System.Drawing.Size(402, 34)
		Me.Button23.TabIndex = 80
		Me.Button23.Text = "BUTTON 'Dari SqlServer Ke MySql'"
		Me.Button23.UseVisualStyleBackColor = False
		'
		'ListView1
		'
		Me.ListView1.ContextMenuStrip = Me.ContextMenuStrip1
		Me.ListView1.FullRowSelect = True
		Me.ListView1.GridLines = True
		Me.ListView1.HideSelection = False
		Me.ListView1.Location = New System.Drawing.Point(17, 457)
		Me.ListView1.Name = "ListView1"
		Me.ListView1.Size = New System.Drawing.Size(989, 116)
		Me.ListView1.TabIndex = 87
		Me.ListView1.UseCompatibleStateImageBehavior = False
		'
		'ContextMenuStrip1
		'
		Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyToolStripMenuItem})
		Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
		Me.ContextMenuStrip1.Size = New System.Drawing.Size(103, 26)
		'
		'CopyToolStripMenuItem
		'
		Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
		Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(102, 22)
		Me.CopyToolStripMenuItem.Text = "Copy"
		'
		'Button2
		'
		Me.Button2.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.Button2.Location = New System.Drawing.Point(604, 52)
		Me.Button2.Name = "Button2"
		Me.Button2.Size = New System.Drawing.Size(173, 34)
		Me.Button2.TabIndex = 88
		Me.Button2.Text = "INSERT | Purchase Order (Supplier)"
		Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.Button2.UseVisualStyleBackColor = False
		'
		'Button3
		'
		Me.Button3.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.Button3.Location = New System.Drawing.Point(783, 52)
		Me.Button3.Name = "Button3"
		Me.Button3.Size = New System.Drawing.Size(173, 34)
		Me.Button3.TabIndex = 89
		Me.Button3.Text = "UPDATE | Jumlah Masuk Purchase Order"
		Me.Button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.Button3.UseVisualStyleBackColor = False
		'
		'Button4
		'
		Me.Button4.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.Button4.Location = New System.Drawing.Point(604, 92)
		Me.Button4.Name = "Button4"
		Me.Button4.Size = New System.Drawing.Size(173, 34)
		Me.Button4.TabIndex = 90
		Me.Button4.Text = "INSERT | Barang"
		Me.Button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.Button4.UseVisualStyleBackColor = False
		'
		'btnSupplierInsert
		'
		Me.btnSupplierInsert.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.btnSupplierInsert.Location = New System.Drawing.Point(604, 132)
		Me.btnSupplierInsert.Name = "btnSupplierInsert"
		Me.btnSupplierInsert.Size = New System.Drawing.Size(173, 34)
		Me.btnSupplierInsert.TabIndex = 91
		Me.btnSupplierInsert.Text = "INSERT | Supplier"
		Me.btnSupplierInsert.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.btnSupplierInsert.UseVisualStyleBackColor = False
		'
		'btnPnwrBahanBaku
		'
		Me.btnPnwrBahanBaku.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.btnPnwrBahanBaku.Location = New System.Drawing.Point(12, 92)
		Me.btnPnwrBahanBaku.Name = "btnPnwrBahanBaku"
		Me.btnPnwrBahanBaku.Size = New System.Drawing.Size(173, 34)
		Me.btnPnwrBahanBaku.TabIndex = 92
		Me.btnPnwrBahanBaku.Text = "INSERT | Penawaran Bahan Baku"
		Me.btnPnwrBahanBaku.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.btnPnwrBahanBaku.UseVisualStyleBackColor = False
		'
		'btnPenawaranPackaging
		'
		Me.btnPenawaranPackaging.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.btnPenawaranPackaging.Location = New System.Drawing.Point(12, 132)
		Me.btnPenawaranPackaging.Name = "btnPenawaranPackaging"
		Me.btnPenawaranPackaging.Size = New System.Drawing.Size(173, 34)
		Me.btnPenawaranPackaging.TabIndex = 93
		Me.btnPenawaranPackaging.Text = "INSERT | Penawaran Packaging"
		Me.btnPenawaranPackaging.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.btnPenawaranPackaging.UseVisualStyleBackColor = False
		'
		'Button5
		'
		Me.Button5.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.Button5.Location = New System.Drawing.Point(191, 52)
		Me.Button5.Name = "Button5"
		Me.Button5.Size = New System.Drawing.Size(173, 34)
		Me.Button5.TabIndex = 94
		Me.Button5.Text = "UPDATE | PO PEMBELIAN"
		Me.Button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.Button5.UseVisualStyleBackColor = False
		'
		'Btn_BiayaLokal
		'
		Me.Btn_BiayaLokal.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.Btn_BiayaLokal.Location = New System.Drawing.Point(604, 172)
		Me.Btn_BiayaLokal.Name = "Btn_BiayaLokal"
		Me.Btn_BiayaLokal.Size = New System.Drawing.Size(173, 34)
		Me.Btn_BiayaLokal.TabIndex = 91
		Me.Btn_BiayaLokal.Text = "INSERT | Biaya Lokal EMI"
		Me.Btn_BiayaLokal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.Btn_BiayaLokal.UseVisualStyleBackColor = False
		'
		'Btn_InsExpedisi
		'
		Me.Btn_InsExpedisi.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.Btn_InsExpedisi.Location = New System.Drawing.Point(12, 172)
		Me.Btn_InsExpedisi.Name = "Btn_InsExpedisi"
		Me.Btn_InsExpedisi.Size = New System.Drawing.Size(173, 34)
		Me.Btn_InsExpedisi.TabIndex = 94
		Me.Btn_InsExpedisi.Text = "INSERT | Expedisi"
		Me.Btn_InsExpedisi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.Btn_InsExpedisi.UseVisualStyleBackColor = False
		'
		'Btn_Update_BiayaLokal
		'
		Me.Btn_Update_BiayaLokal.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.Btn_Update_BiayaLokal.Location = New System.Drawing.Point(783, 172)
		Me.Btn_Update_BiayaLokal.Name = "Btn_Update_BiayaLokal"
		Me.Btn_Update_BiayaLokal.Size = New System.Drawing.Size(173, 34)
		Me.Btn_Update_BiayaLokal.TabIndex = 91
		Me.Btn_Update_BiayaLokal.Text = "UPDATE | Biaya Lokal EMI"
		Me.Btn_Update_BiayaLokal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.Btn_Update_BiayaLokal.UseVisualStyleBackColor = False
		'
		'Btn_InsKendaraan
		'
		Me.Btn_InsKendaraan.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.Btn_InsKendaraan.Location = New System.Drawing.Point(604, 212)
		Me.Btn_InsKendaraan.Name = "Btn_InsKendaraan"
		Me.Btn_InsKendaraan.Size = New System.Drawing.Size(173, 34)
		Me.Btn_InsKendaraan.TabIndex = 91
		Me.Btn_InsKendaraan.Text = "INSERT | Kendaraan"
		Me.Btn_InsKendaraan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.Btn_InsKendaraan.UseVisualStyleBackColor = False
		'
		'Btn_UpdateKendaraan
		'
		Me.Btn_UpdateKendaraan.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.Btn_UpdateKendaraan.Location = New System.Drawing.Point(783, 212)
		Me.Btn_UpdateKendaraan.Name = "Btn_UpdateKendaraan"
		Me.Btn_UpdateKendaraan.Size = New System.Drawing.Size(173, 34)
		Me.Btn_UpdateKendaraan.TabIndex = 91
		Me.Btn_UpdateKendaraan.Text = "UPDATE | Kendaraan"
		Me.Btn_UpdateKendaraan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.Btn_UpdateKendaraan.UseVisualStyleBackColor = False
		'
		'btnPerusahaanBIaya
		'
		Me.btnPerusahaanBIaya.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.btnPerusahaanBIaya.Location = New System.Drawing.Point(604, 252)
		Me.btnPerusahaanBIaya.Name = "btnPerusahaanBIaya"
		Me.btnPerusahaanBIaya.Size = New System.Drawing.Size(173, 34)
		Me.btnPerusahaanBIaya.TabIndex = 95
		Me.btnPerusahaanBIaya.Text = "INSERT | Perusahaan Biaya"
		Me.btnPerusahaanBIaya.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.btnPerusahaanBIaya.UseVisualStyleBackColor = False
		'
		'btn_Master_Satuan
		'
		Me.btn_Master_Satuan.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.btn_Master_Satuan.Location = New System.Drawing.Point(604, 292)
		Me.btn_Master_Satuan.Name = "btn_Master_Satuan"
		Me.btn_Master_Satuan.Size = New System.Drawing.Size(173, 34)
		Me.btn_Master_Satuan.TabIndex = 96
		Me.btn_Master_Satuan.Text = "INSERT | Master Satuan Barang"
		Me.btn_Master_Satuan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.btn_Master_Satuan.UseVisualStyleBackColor = False
		'
		'Button6
		'
		Me.Button6.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.Button6.Location = New System.Drawing.Point(604, 332)
		Me.Button6.Name = "Button6"
		Me.Button6.Size = New System.Drawing.Size(173, 34)
		Me.Button6.TabIndex = 96
		Me.Button6.Text = "SYNC | BA Waste Process"
		Me.Button6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.Button6.UseVisualStyleBackColor = False
		'
		'Button7
		'
		Me.Button7.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.Button7.Location = New System.Drawing.Point(604, 372)
		Me.Button7.Name = "Button7"
		Me.Button7.Size = New System.Drawing.Size(173, 34)
		Me.Button7.TabIndex = 96
		Me.Button7.Text = "SYNC | BA Waste Product"
		Me.Button7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.Button7.UseVisualStyleBackColor = False
		'
		'Button8
		'
		Me.Button8.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.Button8.Location = New System.Drawing.Point(792, 332)
		Me.Button8.Name = "Button8"
		Me.Button8.Size = New System.Drawing.Size(173, 34)
		Me.Button8.TabIndex = 97
		Me.Button8.Text = "INSERT | BA Waste Process"
		Me.Button8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.Button8.UseVisualStyleBackColor = False
		'
		'Button9
		'
		Me.Button9.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.Button9.Location = New System.Drawing.Point(792, 372)
		Me.Button9.Name = "Button9"
		Me.Button9.Size = New System.Drawing.Size(173, 34)
		Me.Button9.TabIndex = 98
		Me.Button9.Text = "INSERT | BA Waste Product"
		Me.Button9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.Button9.UseVisualStyleBackColor = False
		'
		'Server_Sinkronasi_B2B
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(1023, 585)
		Me.Controls.Add(Me.Button9)
		Me.Controls.Add(Me.Button8)
		Me.Controls.Add(Me.Button7)
		Me.Controls.Add(Me.Button6)
		Me.Controls.Add(Me.btn_Master_Satuan)
		Me.Controls.Add(Me.btnPerusahaanBIaya)
		Me.Controls.Add(Me.Btn_InsExpedisi)
		Me.Controls.Add(Me.Button5)
		Me.Controls.Add(Me.btnPenawaranPackaging)
		Me.Controls.Add(Me.btnPnwrBahanBaku)
		Me.Controls.Add(Me.Btn_Update_BiayaLokal)
		Me.Controls.Add(Me.Btn_UpdateKendaraan)
		Me.Controls.Add(Me.Btn_InsKendaraan)
		Me.Controls.Add(Me.Btn_BiayaLokal)
		Me.Controls.Add(Me.btnSupplierInsert)
		Me.Controls.Add(Me.Button4)
		Me.Controls.Add(Me.Button3)
		Me.Controls.Add(Me.Button2)
		Me.Controls.Add(Me.ListView1)
		Me.Controls.Add(Me.Button23)
		Me.Controls.Add(Me.Button22)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.Button1)
		Me.Name = "Server_Sinkronasi_B2B"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Sinkronisasi B2B"
		Me.ContextMenuStrip1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents Button1 As System.Windows.Forms.Button
	Friend WithEvents Timer1 As System.Windows.Forms.Timer
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Button22 As System.Windows.Forms.Button
	Friend WithEvents Button23 As System.Windows.Forms.Button
	Friend WithEvents ListView1 As System.Windows.Forms.ListView
	Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
	Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents Button2 As Button
	Friend WithEvents Button3 As Button
	Friend WithEvents Button4 As Button
	Friend WithEvents btnSupplierInsert As Button
	Friend WithEvents btnPnwrBahanBaku As Button
	Friend WithEvents btnPenawaranPackaging As Button
	Friend WithEvents Button5 As Button
	Friend WithEvents Btn_BiayaLokal As Button
	Friend WithEvents Btn_InsExpedisi As Button
	Friend WithEvents Btn_Update_BiayaLokal As Button
	Friend WithEvents Btn_InsKendaraan As Button
	Friend WithEvents Btn_UpdateKendaraan As Button
	Friend WithEvents btnPerusahaanBIaya As Button
	Friend WithEvents btn_Master_Satuan As Button
	Friend WithEvents Button6 As Button
	Friend WithEvents Button7 As Button
	Friend WithEvents Button8 As Button
	Friend WithEvents Button9 As Button
End Class
