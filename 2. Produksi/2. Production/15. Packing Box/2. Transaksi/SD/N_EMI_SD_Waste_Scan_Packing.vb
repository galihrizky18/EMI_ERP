Public Class N_EMI_SD_Waste_Scan_Packing
	Public asal As String
	Public kolom As String

	Private Sub N_EMI_SD_Waste_Scan_Packing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		TextBox1.Text = ""
		TextBox1.Focus()
	End Sub

	Private Sub N_EMI_SD_Waste_Scan_Packing_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub ButtonAngka_Click(sender As Object, e As EventArgs) Handles btn0.Click, btn1.Click, btn2.Click, btn3.Click, btn4.Click, btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click
		' Ambil tombol yang diklik
		Dim tombol As System.Windows.Forms.Button = CType(sender, System.Windows.Forms.Button)

		TextBox1.Text &= tombol.Text
	End Sub

	Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
		If asal = "N_EMI_Transaksi_Scan_Packing - Waste" Then
			If Val(TextBox1.Text) > Val(Label4.Text) Then
				MessageBox.Show("jumlah yang diinput 0!", Module1.Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			ElseIf TextBox1.Text = "" Then
				MessageBox.Show("jumlah belum diinput!", Module1.Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			ElseIf Val(TextBox1.Text) = 0 Then
				MessageBox.Show("jumlah tidak boleh diinput 0 !", Module1.Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			N_EMI_SD_Waste_Scan_Packing_Pin.Label3.Text = Me.Label3.Text
			N_EMI_SD_Waste_Scan_Packing_Pin.Label5.Text = Me.TextBox1.Text
			N_EMI_SD_Waste_Scan_Packing_Pin.asal = "N_EMI_Transaksi_Scan_Packing - Waste"
			N_EMI_SD_Waste_Scan_Packing_Pin.ShowDialog()
			Me.Close()
		ElseIf Me.asal = "N_Emi_Transaksi_Repacking - Waste" Then
			If Val(Me.TextBox1.Text) > Val(Me.Label4.Text) Then
				MessageBox.Show("jumlah yang diinput lebih besar dari jumlah box !", Module1.Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
				'ElseIf Me.TextBox1.Text = "0" Then
				'    MessageBox.Show("jumlah yang diinput 0!", Module1.Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'    Exit Sub
			ElseIf Me.TextBox1.Text = "" Then
				MessageBox.Show("jumlah belum diinput!", Module1.Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			N_Emi_Transaksi_Repacking.DataGridView1.Rows(CInt(Label3.Text)).Cells(CInt(kolom)).Value = TextBox1.Text.Trim()
			Me.Close()
		ElseIf Me.asal = "N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru - Cetak Pallet" Then
			If Val(Me.TextBox1.Text) > Val(Me.Label3.Text) Then
				MessageBox.Show("jumlah yang diinput lebih besar dari jumlah max pallet !", Module1.Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			ElseIf Me.TextBox1.Text = "0" Then
				MessageBox.Show("jumlah yang diinput 0!", Module1.Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			ElseIf Me.TextBox1.Text = "" Then
				MessageBox.Show("jumlah belum diinput!", Module1.Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If
			If Val(Me.TextBox1.Text) < Val(Me.Label3.Text) Then
				Dim Hapus As String = MessageBox.Show("Barang belum memenuhi kapasitas 1 pallet, apakah mau cetak barcode pallet?", Module1.Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
				If Hapus = vbNo Then Exit Sub
			End If

			N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru.Label11.Text = TextBox1.Text
			N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru.xFlag_OK = "Y"
			N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru.xprint_pallet_new()
			Me.Close()
		ElseIf Me.asal = "N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru - Sementara" Then
			If Val(Me.TextBox1.Text) > Val(Me.Label3.Text) Then
				MessageBox.Show("jumlah yang diinput lebih besar dari jumlah max pallet !", Module1.Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			ElseIf Me.TextBox1.Text = "0" Then
				MessageBox.Show("jumlah yang diinput 0!", Module1.Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			ElseIf Me.TextBox1.Text = "" Then
				MessageBox.Show("jumlah belum diinput!", Module1.Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			Dim Hapus As String = MessageBox.Show("apakah mau melakukan simpan sementara??", Module1.Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			If Hapus = vbNo Then Exit Sub

			N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru.Label11.Text = Val(TextBox1.Text)
			If Val(Me.TextBox1.Text) = Val(Me.Label3.Text) Then
				N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru.xFlag_OK = "T"
				N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru.xprint_pallet_new()
			Else
				N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru.xsimpan_sementara()
			End If
			Me.Close()
		Else
			MessageBox.Show("Asal tidak ditemukan.....", Module1.Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If
	End Sub

	Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
		TextBox1.Text = ""
	End Sub

	Public Sub kosong()
		If Me.asal = "N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru - Cetak Pallet" Then
			Button12.Size = New Size(130, 47)
			Button1.Enabled = True
		Else
			Button12.Size = New Size(264, 47)
			Button1.Enabled = False
		End If
	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		If Me.asal = "N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru - Cetak Pallet" Then
			If Val(Me.TextBox1.Text) > Val(Me.Label3.Text) Then
				MessageBox.Show("jumlah yang diinput lebih besar dari jumlah max pallet !", Module1.Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			ElseIf Me.TextBox1.Text = "0" Then
				MessageBox.Show("jumlah yang diinput 0!", Module1.Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			ElseIf Me.TextBox1.Text = "" Then
				MessageBox.Show("jumlah belum diinput!", Module1.Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If
			If Val(Me.TextBox1.Text) < Val(Me.Label3.Text) Then
				Dim Hapus As String = MessageBox.Show("Barang belum memenuhi kapasitas 1 pallet, apakah mau cetak barcode pallet?", Module1.Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
				If Hapus = vbNo Then Exit Sub
			End If

			N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru.Label11.Text = TextBox1.Text
			N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru.xFlag_OK = "T"
			N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru.xprint_pallet_new()
			Me.Close()
		End If
	End Sub

End Class