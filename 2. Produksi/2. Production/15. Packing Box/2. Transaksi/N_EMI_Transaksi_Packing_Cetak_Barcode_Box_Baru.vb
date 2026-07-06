Imports System.Drawing.Printing
Imports System.IO

Public Class N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru
	Dim kode_unik As String
	Dim kode_unik_pallet As String
	Private FileSize1 As UInt32
	Private rawData1() As Byte
	Private fs1 As FileStream
	Dim cetak_pallet As Boolean
	Dim xno_transaksi_pallet As String
	Dim arrno_repack As New ArrayList
	Dim ajumlah As Integer = 0
	Public xFlag_OK As String = ""

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		If Button1.Text = "Shift In" Then
			get_jam()
			N_EMI_Check_In_Out_Shift_Packing.Button1.Text = "Shift In"
			N_EMI_Check_In_Out_Shift_Packing.kosong()
			N_EMI_Check_In_Out_Shift_Packing.ShowDialog()
		Else
			N_EMI_Check_In_Out_Shift_Packing.Button1.Text = "Shift Out"
			N_EMI_Check_In_Out_Shift_Packing.TextBox1.Text = Label12.Text
			N_EMI_Check_In_Out_Shift_Packing.kosong()
			N_EMI_Check_In_Out_Shift_Packing.ComboBox2.Text = Label13.Text
			N_EMI_Check_In_Out_Shift_Packing.TextBox2.Text = Label14.Text
			N_EMI_Check_In_Out_Shift_Packing.ShowDialog()
		End If
	End Sub

	Public Sub kosong()
		DataGridView1.Rows.Clear()
		DataGridView2.Rows.Clear()
		DataGridView3.Rows.Clear()

		Label7.Text = "0"
		Label9.Text = "0"
		Label10.Text = "0"

		Label2.Text = ""
		Label12.Text = ""
		Label13.Text = ""
		Label14.Text = ""
		Label11.Text = ""

		Button1.Text = "Shift In"

		Try
			OpenConn()

			ComboBox1.Items.Clear()
			SQL = "select Line from N_EMI_Transaksi_Packing_Check_In_Out where Status is null and Kode_Perusahaan = '" & KodePerusahaan & "' and Tanggal_Out is null "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					ComboBox1.Items.Add(dr("Line"))
				Loop
			End Using
			ComboBox1.SelectedIndex = -1

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		kosong()
	End Sub

	Public Sub get_data_repacking()
		If Label14.Text.Trim.Length = 0 Then Exit Sub
		Try
			OpenConn()

			DataGridView3.Rows.Clear()
			SQL = "select top(50)a.No_Transaksi, f.Line, a.Qr_Code + '-' + a.Kode_Unik_Berjalan as Barcode, b.Kode_Barang, c.Nama as Nama_Barang, "
			SQL = SQL & "sum(e.Jumlah) as Jumlah, sum(e.Jumlah_Waste) as Jumlah_Waste, sum(e.Jumlah_Good) as Jumlah_Good, a.Qr_Code, a.Kode_Unik_Berjalan, f.Jumlah as isi_satuan_besar "
			SQL = SQL & "from N_EMI_Transaksi_Packing a, N_EMI_Transaksi_Packing_Detail d, Barang_SN b, Barang c, "
			SQL = SQL & "N_EMI_Transaksi_Packing_Repacking_Detail e, N_EMI_Transaksi_Packing_Repacking f "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
			SQL = SQL & "and d.SN_Baru = b.Serial_Number and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Transaksi = d.No_Transaksi "
			SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and a.Status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Line = '" & Label14.Text & "' and b.Kode_Barang = '" & Label4.Text & "' "
			SQL = SQL & "and e.Jumlah_Good <> 0 "
			SQL = SQL & "and e.Kode_Perusahaan = d.Kode_Perusahaan and e.Urut_Detail_Transaksi_Packing = d.Urut_Oto "
			SQL = SQL & "and e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Transaksi = f.No_Transaksi and f.Status is null and f.Flag_Selesai is null "
			SQL = SQL & "group by a.No_Transaksi, f.Line, a.Qr_Code + '-' + a.Kode_Unik_Berjalan, b.Kode_Barang, c.Nama, "
			SQL = SQL & "a.Qr_Code, a.Kode_Unik_Berjalan, f.Jumlah, a.Tanggal+a.Jam "
			SQL = SQL & "order by a.Tanggal+a.Jam "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							DataGridView3.Rows.Add(1)
							DataGridView3.Rows(i).Cells(0).Value = .Rows(i).Item("Barcode")
							DataGridView3.Rows(i).Cells(1).Value = .Rows(i).Item("Kode_Barang")
							DataGridView3.Rows(i).Cells(2).Value = .Rows(i).Item("Nama_Barang")
							DataGridView3.Rows(i).Cells(3).Value = Format(.Rows(i).Item("Jumlah"), "N0")
							DataGridView3.Rows(i).Cells(4).Value = Format(.Rows(i).Item("Jumlah_Good"), "N0")
							DataGridView3.Rows(i).Cells(5).Value = .Rows(i).Item("No_Transaksi")
							DataGridView3.Rows(i).Cells(6).Value = .Rows(i).Item("Qr_Code")
							DataGridView3.Rows(i).Cells(7).Value = .Rows(i).Item("Kode_Unik_Berjalan")

						Next
					End If
				End With
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Public Sub get_data()
		If Label14.Text.Trim.Length = 0 Then Exit Sub

		Try
			OpenConn()
			SQL = "select top(1)b.Kode_Barang "
			SQL = SQL & "from N_EMI_Transaksi_Packing a, N_EMI_Transaksi_Packing_Detail d, Barang_SN b, Barang c "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
			SQL = SQL & "and d.SN_Baru = b.Serial_Number and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Transaksi = d.No_Transaksi "
			SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and a.Status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Line = '" & Label14.Text & "' "
			SQL = SQL & "and d.Jumlah - d.Jumlah_Sdh_Packing - d.Jumlah_Waste <> 0 and a.Flag_Hold is null and a.Flag_Selesai is null "
			SQL = SQL & "group by b.Kode_Barang, a.Tanggal+a.Jam "
			SQL = SQL & "order by a.Tanggal+a.Jam "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					Label4.Text = dr("Kode_Barang")
				Else
					'dr.Close()
					'CloseConn()
					'MessageBox.Show("tidak ada barang yang akan discan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					'Exit Sub
					Label4.Text = ""
				End If
			End Using

			Dim totalscan As Integer = 0
			Dim totalpack As Integer = 0
			Dim isisatuan As Integer = 0
			DataGridView1.Rows.Clear()
			SQL = "select top(50)a.No_Transaksi, a.Line, a.Qr_Code + '-' + a.Kode_Unik_Berjalan as Barcode, b.Kode_Barang, c.Nama as Nama_Barang, "
			SQL = SQL & "sum(d.Jumlah) as Jumlah, sum(d.Jumlah_Sdh_Packing) as Jumlah_Sdh_Packing, sum(d.Jumlah_Waste) as Jumlah_Waste, a.Qr_Code, a.Kode_Unik_Berjalan, c.isi_satuan_besar  "
			SQL = SQL & "from N_EMI_Transaksi_Packing a, N_EMI_Transaksi_Packing_Detail d, Barang_SN b, Barang c "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
			SQL = SQL & "and d.SN_Baru = b.Serial_Number and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Transaksi = d.No_Transaksi "
			SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and a.Status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Line = '" & Label14.Text & "' and b.Kode_Barang = '" & Label4.Text & "' "
			SQL = SQL & "and d.Jumlah - d.Jumlah_Sdh_Packing - d.Jumlah_Waste <> 0 and a.Flag_Hold is null and a.Flag_Selesai is null "
			SQL = SQL & "group by a.No_Transaksi, a.Line, a.Qr_Code + '-' + a.Kode_Unik_Berjalan, b.Kode_Barang, c.Nama, "
			SQL = SQL & "a.Qr_Code, a.Kode_Unik_Berjalan, c.isi_satuan_besar, a.Tanggal+a.Jam, d.Tgl_Produksi "
			SQL = SQL & "order by a.Tanggal+a.Jam, d.Tgl_Produksi, a.No_Transaksi "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							Dim sisa As Integer = .Rows(i).Item("Jumlah") - .Rows(i).Item("Jumlah_Sdh_Packing") - .Rows(i).Item("Jumlah_Waste")

							DataGridView1.Rows.Add(1)
							DataGridView1.Rows(i).Cells(0).Value = .Rows(i).Item("Barcode")
							DataGridView1.Rows(i).Cells(1).Value = .Rows(i).Item("Kode_Barang")
							DataGridView1.Rows(i).Cells(2).Value = .Rows(i).Item("Nama_Barang")
							DataGridView1.Rows(i).Cells(3).Value = Format(.Rows(i).Item("Jumlah"), "N0")
							DataGridView1.Rows(i).Cells(4).Value = Format(sisa, "N0")
							DataGridView1.Rows(i).Cells(5).Value = .Rows(i).Item("No_Transaksi")
							DataGridView1.Rows(i).Cells(6).Value = .Rows(i).Item("Qr_Code")
							DataGridView1.Rows(i).Cells(7).Value = .Rows(i).Item("Kode_Unik_Berjalan")

							totalscan = totalscan + .Rows(i).Item("Jumlah")
							totalpack = totalpack + .Rows(i).Item("Jumlah_Sdh_Packing")
							isisatuan = .Rows(i).Item("isi_satuan_besar")
						Next
					End If
				End With
			End Using

			If Label4.Text <> "" Then
				Dim total_box As Integer = totalpack / isisatuan
				Dim sisa1 As Integer = totalscan - totalpack
				Label7.Text = Format(totalscan, "N0")
				'Label10.Text = Format(total_box, "N0")
				Label9.Text = Format(sisa1, "N0")
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Public Sub get_log_scan()
		If Label14.Text.Trim.Length = 0 Then Exit Sub

		Try
			OpenConn()

			DataGridView2.Rows.Clear()
			SQL = "select a.Kode_Unik_Print, a.Tgl_Cetak, a.Jam_Cetak from N_EMI_Transaksi_Packing_Box a, N_EMI_Transaksi_Packing_Check_In_Out b "
			SQL = SQL & "where a.Status is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' and b.Line = '" & Label14.Text & "' "
			SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi and a.Flag_Pallet is null "
			SQL = SQL & "order by a.Tgl_Cetak + a.Jam_Cetak desc "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							DataGridView2.Rows.Add(1)
							DataGridView2.Rows(i).Cells(0).Value = .Rows(i).Item("Kode_Unik_Print")
							DataGridView2.Rows(i).Cells(1).Value = Format(.Rows(i).Item("Tgl_Cetak"), "dd-MMM-yyyy") & " " & .Rows(i).Item("Jam_Cetak")
						Next
					End If
				End With
			End Using

			'SQL = "select count(distinct a.Urut_Pallet) as Jumlah from N_EMI_Transaksi_Packing_Box a, N_EMI_Transaksi_Packing_Check_In_Out b "
			'SQL = SQL & "where a.Status is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' and b.Line = '" & Label14.Text & "' and b.No_Transaksi = '" & Label12.Text & "' "
			'SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi and a.Flag_Pallet = 'Y' "
			'Using Ds = BindingTrans(SQL)
			'    With Ds.Tables("MyTable")
			'        If .Rows.Count <> 0 Then
			'            For i As Integer = 0 To .Rows.Count - 1
			'                Label10.Text = Format(.Rows(i).Item("Jumlah"), "N0")
			'            Next
			'        End If
			'    End With
			'End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
		'GroupBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
		'DataGridView1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom

		'GroupBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Bottom
		'DataGridView1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Bottom
		Button5.Anchor = AnchorStyles.Top Or AnchorStyles.Right

		Dim totalLebar As Integer = Me.ClientSize.Width
		Dim totalTinggi As Integer = Me.ClientSize.Height

		' Hitung margin 1% dari lebar dan tinggi form
		Dim marginX As Integer = CInt(totalLebar * 0.01)
		Dim marginY As Integer = CInt(totalTinggi * 0.01)

		' Lebar efektif setelah dikurangi margin kiri, kanan, dan jarak antar groupbox
		Dim lebarEfektif As Integer = totalLebar - (marginX * 3)

		' Tinggi efektif setelah dikurangi margin atas dan bawah
		Dim tinggiEfektif As Integer = totalTinggi - (TabControl1.Top + marginY)

		' Hitung lebar masing-masing groupbox
		Dim lebarGB1 As Integer = CInt(lebarEfektif * 0.65)
		Dim lebarGB2 As Integer = CInt(lebarEfektif * 0.35)

		' --- GroupBox kiri ---
		TabControl1.Left = marginX
		' Top biarkan sesuai desain awal
		TabControl1.Width = lebarGB1
		TabControl1.Height = tinggiEfektif

		DataGridView1.Dock = DockStyle.Fill
		DataGridView3.Dock = DockStyle.Fill

		' --- GroupBox kanan ---
		GroupBox2.Left = TabControl1.Right + marginX
		' Top biarkan sesuai desain awal
		GroupBox2.Width = lebarGB2
		GroupBox2.Height = tinggiEfektif

		DataGridView2.Dock = DockStyle.Fill

		Dim totalLebar1 As Integer = DataGridView1.Width - 20
		DataGridView1.Columns(0).Width = CInt(totalLebar1 * 0.2)
		DataGridView1.Columns(2).Width = CInt(totalLebar1 * 0.48)
		DataGridView1.Columns(3).Width = CInt(totalLebar1 * 0.1)
		DataGridView1.Columns(4).Width = CInt(totalLebar1 * 0.1)
		DataGridView1.Columns(8).Width = CInt(totalLebar1 * 0.1)

		Dim totalLebar3 As Integer = DataGridView3.Width - 20
		DataGridView3.Columns(0).Width = CInt(totalLebar3 * 0.2)
		DataGridView3.Columns(2).Width = CInt(totalLebar3 * 0.48)
		DataGridView3.Columns(3).Width = CInt(totalLebar3 * 0.15)
		DataGridView3.Columns(4).Width = CInt(totalLebar3 * 0.15)

		'GroupBox2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Bottom
		'DataGridView2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Bottom
		Dim totalLebar2 As Integer = DataGridView2.Width - 20
		DataGridView2.Columns(0).Width = CInt(totalLebar2 * 0.55)
		DataGridView2.Columns(1).Width = CInt(totalLebar2 * 0.4)
	End Sub

	Private Sub N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		If Label14.Text.Trim.Length = 0 Then Exit Sub

		get_data()

		get_jam()
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
			'Dim rnd As New Random()

			'' Ambil bagian tanggal
			'Dim yy As String = tgl_skg.ToString("yy")
			'Dim MM As String = tgl_skg.ToString("MM")
			'Dim dd As String = tgl_skg.ToString("dd")
			'Dim HHmm As String = tgl_skg.ToString("HHmmss")

			'' Huruf random
			'Dim r1 As String = chars(rnd.Next(chars.Length))
			'Dim r2 As String = chars(rnd.Next(chars.Length))
			'Dim r3 As String = chars(rnd.Next(chars.Length))

			'' 3 huruf random di akhir
			'Dim rAkhir As String = ""
			'For i As Integer = 1 To 3
			'    rAkhir &= chars(rnd.Next(chars.Length))
			'Next

			' Gabungkan sesuai pola
			'kode_unik = yy & r1 & MM & r2 & dd & r3 & HHmm & rAkhir

			'Dim kode_unik1 As String = yy & r1 & MM & r2 & dd & r3 & HHmm & rAkhir
			'Dim md5 As MD5 = MD5.Create()
			'Dim inputBytes As Byte() = Encoding.UTF8.GetBytes(kode_unik1)
			'Dim hashBytes As Byte() = md5.ComputeHash(inputBytes)

			'Dim sb As New StringBuilder()
			'For Each b As Byte In hashBytes
			'    sb.Append(b.ToString("x2"))
			'Next

			'kode_unik = sb.ToString()

			Dim Kode_Berjalan As String = ""
			Dim isUnique As Boolean = False

			Do While isUnique = False
				Kode_Berjalan = "B" & Generate_Random_Kode(15)

				SQL = "SELECT Kode_Unik_Print FROM N_EMI_Transaksi_Packing_Box WHERE Kode_Unik_Print = '" & Kode_Berjalan & "' "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count = 0 Then
							isUnique = True
						End If
					End With
				End Using
			Loop

			kode_unik = Kode_Berjalan

			SQL = "select top(1)Isi_Satuan_Besar from Barang where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang = '" & Label4.Text & "' "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						SQL = "insert into N_EMI_Transaksi_Packing_Box(Kode_Perusahaan, No_Transaksi, Kode_Unik_Print, Kode_Barang, Line, Jumlah, Tgl_Cetak, Jam_Cetak) "
						SQL = SQL & "values('" & KodePerusahaan & "', '" & Label12.Text.Trim & "', '" & kode_unik & "', '" & Label4.Text & "', '" & Label14.Text & "', "
						SQL = SQL & "'" & .Rows(0).Item("isi_satuan_besar") & "','" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "') "
						ExecuteTrans(SQL)
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show("Barang tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			Dim x_no_urut_packing_box As Integer = 0
			SQL = "select IDENT_CURRENT('N_EMI_Transaksi_Packing_Box') as urutan"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					x_no_urut_packing_box = Dr("urutan")
				End If
			End Using

			Dim isi_satuan As Integer = 0
			Dim sisa As Integer = 0
			SQL = "select a.No_Transaksi, a.Line, a.Qr_Code + '-' + a.Kode_Unik_Berjalan as Barcode, b.Kode_Barang, c.Nama as Nama_Barang, "
			SQL = SQL & "d.Jumlah, d.Jumlah_Sdh_Packing, a.Qr_Code, a.Kode_Unik_Berjalan, c.isi_satuan_besar, d.SN_Baru, "
			SQL = SQL & "d.Jumlah - d.Jumlah_Sdh_Packing - d.Jumlah_Waste as Jmlh_Sisa, d.Urut_Oto, d.Urut_Results_Detail_Pallet "
			SQL = SQL & "from N_EMI_Transaksi_Packing a, N_EMI_Transaksi_Packing_Detail d, Barang_SN b, Barang c "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
			SQL = SQL & "and d.SN_Baru = b.Serial_Number and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Transaksi = d.No_Transaksi "
			SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and a.Status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Line = '" & Label14.Text & "' and b.Kode_Barang = '" & Label4.Text & "' "
			SQL = SQL & "and d.Jumlah - d.Jumlah_Sdh_Packing - d.Jumlah_Waste <> 0 and a.Flag_Hold is null and a.Flag_Selesai is null "
			SQL = SQL & "order by a.Tanggal+a.Jam, b.Tgl_Produksi "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						isi_satuan = .Rows(0).Item("isi_satuan_besar")
						sisa = .Rows(0).Item("isi_satuan_besar")
						For i As Integer = 0 To .Rows.Count - 1

							If sisa = 0 Then
								Exit For
							ElseIf sisa < 0 Then
								CloseTrans()
								CloseConn()
								MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If

							If sisa < .Rows(i).Item("Jmlh_Sisa") Or sisa = .Rows(i).Item("Jmlh_Sisa") Then
								SQL = "insert into N_EMI_Transaksi_Packing_Det(Kode_Perusahaan, Urut_Detail_Transaksi_Packing, Urut_Transaksi_Box, Jumlah) "
								SQL = SQL & "values('" & KodePerusahaan & "', '" & .Rows(i).Item("Urut_Oto") & "', '" & x_no_urut_packing_box & "', "
								SQL = SQL & "'" & sisa & "' ) "
								ExecuteTrans(SQL)

								SQL = "update N_EMI_Transaksi_Packing_Detail set Jumlah_Sdh_Packing = Jumlah_Sdh_Packing + '" & sisa & "' where "
								SQL = SQL & "Urut_Oto = '" & .Rows(i).Item("Urut_Oto") & "' "
								ExecuteTrans(SQL)

								SQL = "update Emi_Production_Results_Detail_Pallet set Jumlah_Sdh_Packing = Jumlah_Sdh_Packing + '" & sisa & "' where "
								SQL = SQL & "Urut_Oto = '" & .Rows(i).Item("Urut_Results_Detail_Pallet") & "' "
								ExecuteTrans(SQL)

								sisa = 0
							ElseIf sisa > .Rows(i).Item("Jmlh_Sisa") Then
								SQL = "insert into N_EMI_Transaksi_Packing_Det(Kode_Perusahaan, Urut_Detail_Transaksi_Packing, Urut_Transaksi_Box, Jumlah) "
								SQL = SQL & "values('" & KodePerusahaan & "', '" & .Rows(i).Item("Urut_Oto") & "', '" & x_no_urut_packing_box & "', "
								SQL = SQL & "'" & .Rows(i).Item("Jmlh_Sisa") & "' ) "
								ExecuteTrans(SQL)

								SQL = "update N_EMI_Transaksi_Packing_Detail set Jumlah_Sdh_Packing = Jumlah_Sdh_Packing + '" & .Rows(i).Item("Jmlh_Sisa") & "' where "
								SQL = SQL & "Urut_Oto = '" & .Rows(i).Item("Urut_Oto") & "' "
								ExecuteTrans(SQL)

								SQL = "update Emi_Production_Results_Detail_Pallet set Jumlah_Sdh_Packing = Jumlah_Sdh_Packing + '" & .Rows(i).Item("Jmlh_Sisa") & "' where "
								SQL = SQL & "Urut_Oto = '" & .Rows(i).Item("Urut_Results_Detail_Pallet") & "' "
								ExecuteTrans(SQL)

								sisa = sisa - .Rows(i).Item("Jmlh_Sisa")
							Else
								CloseTrans()
								CloseConn()
								MessageBox.Show("Barang tidak cukup harap scan barang terlebih dahulu !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If

							SQL = "select sum(a.Jumlah) as Jumlah, sum(a.Jumlah_Sdh_Packing) as Jumlah_Sdh_Packing, sum(a.Jumlah_Waste) as Jumlah_Waste "
							SQL = SQL & "from N_EMI_Transaksi_Packing_Detail a, N_EMI_Transaksi_Packing b "
							SQL = SQL & "where a.No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
							Using dr = OpenTrans(SQL)
								If dr.Read Then
									If dr("Jumlah") = dr("Jumlah_Sdh_Packing") + dr("Jumlah_Waste") Then
										dr.Close()
										SQL = "update N_EMI_Transaksi_Packing set Flag_Selesai = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' "
										SQL = SQL & "and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
										ExecuteTrans(SQL)
									End If
								End If
							End Using

						Next
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show("Barang tidak ditemukan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			cetak_pallet = False
			SQL = "select count(distinct a.Kode_Unik_Print) as Jumlah, min(b.Isi_Satuan_Pallet) as Isi_Satuan_Pallet from N_EMI_Transaksi_Packing_Box a, Barang b "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang and a.Status is null and a.Kode_Barang = '" & Label4.Text & "' "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Line = '" & Label14.Text & "' and a.Flag_Pallet is null "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					If dr("Jumlah") = dr("Isi_Satuan_Pallet") Then
						cetak_pallet = True
					Else
						cetak_pallet = False
					End If
				End If
			End Using

			Cmd.Transaction.Commit()
			'MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
		xprint(kode_unik)

		If cetak_pallet = True Then
			xprint_pallet()
		End If

		get_data()
		get_log_scan()
		get_data_repacking()
	End Sub

	Private Sub xprint_pallet()
		get_jam()
		get_no_transaksi_pallet()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
			'Dim rnd As New Random()

			'' Ambil bagian tanggal
			'Dim yy As String = tgl_skg.ToString("yy")
			'Dim MM As String = tgl_skg.ToString("MM")
			'Dim dd As String = tgl_skg.ToString("dd")
			'Dim HHmm As String = tgl_skg.ToString("HHmmss")

			'' Huruf random
			'Dim r1 As String = chars(rnd.Next(chars.Length))
			'Dim r2 As String = chars(rnd.Next(chars.Length))
			'Dim r3 As String = chars(rnd.Next(chars.Length))

			'' 3 huruf random di akhir
			'Dim rAkhir As String = ""
			'For i As Integer = 1 To 3
			'    rAkhir &= chars(rnd.Next(chars.Length))
			'Next

			'' Gabungkan sesuai pola
			''kode_unik_pallet = "P" & yy & r1 & MM & r2 & dd & r3 & HHmm & rAkhir

			'Dim kode_unik_pallet1 As String = yy & r1 & MM & r2 & dd & r3 & HHmm & rAkhir
			'Dim md5 As MD5 = MD5.Create()
			'Dim inputBytes As Byte() = Encoding.UTF8.GetBytes(kode_unik_pallet1)
			'Dim hashBytes As Byte() = md5.ComputeHash(inputBytes)

			'Dim sb As New StringBuilder()
			'For Each b As Byte In hashBytes
			'    sb.Append(b.ToString("x2"))
			'Next

			'kode_unik_pallet = sb.ToString()

			Dim Kode_Berjalan As String = ""
			Dim isUnique As Boolean = False

			Do While isUnique = False
				Kode_Berjalan = "P" & Generate_Random_Kode(15)

				SQL = "SELECT Kode_Unik_Print FROM N_EMI_Transaksi_Packing_Pallet WHERE Kode_Unik_Print = '" & Kode_Berjalan & "' "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count = 0 Then
							isUnique = True
						End If
					End With
				End Using
			Loop

			kode_unik_pallet = Kode_Berjalan

			SQL = "select count(distinct a.Kode_Unik_Print) as Jumlah, min(b.Isi_Satuan_Pallet) as Isi_Satuan_Pallet from N_EMI_Transaksi_Packing_Box a, Barang b "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang and a.Status is null and a.Kode_Barang = '" & Label4.Text & "' "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Line = '" & Label14.Text & "' and a.Flag_Pallet is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							SQL = "insert into N_EMI_Transaksi_Packing_Pallet(Kode_Perusahaan, No_Transaksi, Kode_Unik_Print, Jumlah, Tgl_Cetak, Jam_Cetak) "
							SQL = SQL & "values('" & KodePerusahaan & "', '" & xno_transaksi_pallet & "', '" & kode_unik_pallet & "', '" & .Rows(i).Item("Jumlah") & "', "
							SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "') "
							ExecuteTrans(SQL)
						Next
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show("Barcode sn tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			Dim x_no_urut_packing_pallet As Integer = 0
			SQL = "select IDENT_CURRENT('N_EMI_Transaksi_Packing_Pallet') as urutan"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					x_no_urut_packing_pallet = Dr("urutan")
				End If
			End Using

			SQL = "select a.Urut_Oto from N_EMI_Transaksi_Packing_Box a where a.Status is null and a.Kode_Barang = '" & Label4.Text & "' "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Line = '" & Label14.Text & "' and a.Flag_Pallet is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							SQL = "update N_EMI_Transaksi_Packing_Box set Flag_Pallet = 'Y', Urut_Pallet = '" & x_no_urut_packing_pallet & "' "
							SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Urut_Oto = '" & .Rows(i).Item("Urut_Oto") & "' "
							ExecuteTrans(SQL)
						Next
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show("Barcode sn tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			'HAPUS TABEL SEMENTARA
			SQL = "delete from N_EMI_Cetak_Label_Packing_Pallet where Line = '" & Label14.Text & "' "
			ExecuteTrans(SQL)

			Barcode.Image = Nothing
			Barcode.Image = Generate_QR_NoPadding(kode_unik_pallet)
			Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_pallet & ".jpg")
			Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
			'End If

			fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
			FileSize1 = fs1.Length
			rawData1 = New Byte(FileSize1) {}
			fs1.Read(rawData1, 0, FileSize1)
			fs1.Close()
			Cmd.Parameters.Add("@newBarcode" & kode_unik_pallet, SqlDbType.Image).Value = rawData1

			'SQL = "select a.Kode_Unik_Print, b.Kode_Barang, f.Nama as Nama_Barang, min(d.Tgl_Produksi) as Tgl_Produksi, "
			'SQL = SQL & "min(d.Tgl_Expired) as Tgl_Expired ,a.Jumlah, b.Line "
			'SQL = SQL & "from N_EMI_Transaksi_Packing_Pallet a, N_EMI_Transaksi_Packing_Box b, "
			'SQL = SQL & "N_EMI_Transaksi_Packing_Det c, N_EMI_Transaksi_Packing_Detail d, Barang_SN e, Barang f "
			'SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Status is null and a.Kode_Unik_Print = '" & kode_unik_pallet & "' "
			'SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Urut_Oto = b.Urut_Pallet and b.Status is null "
			'SQL = SQL & "and b.Line = '" & Label14.Text & "' and b.Urut_Oto = c.Urut_Transaksi_Box and c.Urut_Detail_Transaksi_Packing = d.Urut_Oto and c.Status is null "
			'SQL = SQL & "and d.SN_Baru = e.Serial_Number and e.Kode_Perusahaan = f.Kode_Perusahaan "
			'SQL = SQL & "and e.Kode_Stock_Owner = f.Kode_Stock_Owner and e.Kode_Barang = f.Kode_Barang "
			'SQL = SQL & "group by a.Kode_Unik_Print, b.Kode_Barang, f.Nama, a.Jumlah, b.Line "

			SQL = "WITH Prod AS ( SELECT y.No_Production_Order, x.Kode_Perusahaan, x.Urut_Oto, "
			SQL = SQL & "ROW_NUMBER() OVER (PARTITION BY x.Kode_Perusahaan, x.Urut_Oto ORDER BY DATEADD(SECOND, DATEDIFF(SECOND, 0, y.Jam), y.Tanggal) "
			SQL = SQL & ") AS rn "
			SQL = SQL & "FROM Emi_Production_Results y "
			SQL = SQL & "INNER JOIN Emi_ProductionS_ResulSSts_Detail_Pallet x "
			SQL = SQL & "ON x.Kode_Perusahaan = y.Kode_Perusahaan AND x.No_Transaksi = y.No_Transaksi "
			SQL = SQL & "WHERE y.Status IS NULL) "

			SQL = SQL & "SELECT p.No_Production_Order, a.Kode_Unik_Print, b.Kode_Barang, f.Nama AS Nama_Barang, "
			SQL = SQL & "MIN(d.Tgl_Produksi) AS Tgl_Produksi, MIN(d.Tgl_Expired) AS Tgl_Expired, a.Jumlah, b.Line "
			SQL = SQL & "FROM N_EMI_Transaksi_Packing_Pallet a INNER JOIN N_EMI_Transaksi_Packing_Box b "
			SQL = SQL & "ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.Urut_Oto = b.Urut_Pallet "
			SQL = SQL & "INNER JOIN N_EMI_Transaksi_Packing_Det c "
			SQL = SQL & "ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.Urut_Oto = c.Urut_Transaksi_Box "
			SQL = SQL & "INNER JOIN N_EMI_Transaksi_Packing_Detail d "
			SQL = SQL & "ON c.Urut_Detail_Transaksi_Packing = d.Urut_Oto and c.Kode_Perusahaan = d.Kode_Perusahaan "
			SQL = SQL & "INNER JOIN Barang_SN e "
			SQL = SQL & "ON d.SN_Baru = e.Serial_Number and d.Kode_Perusahaan = e.Kode_Perusahaan "
			SQL = SQL & "INNER JOIN Barang f "
			SQL = SQL & "ON e.Kode_Perusahaan = f.Kode_Perusahaan AND e.Kode_Stock_Owner = f.Kode_Stock_Owner "
			SQL = SQL & "AND e.Kode_Barang = f.Kode_Barang "
			SQL = SQL & "LEFT JOIN Prod p "
			SQL = SQL & "ON d.Kode_Perusahaan = p.Kode_Perusahaan AND d.Urut_Results_Detail_Pallet = p.Urut_Oto "
			SQL = SQL & "AND p.rn = 1 "
			SQL = SQL & "WHERE a.Kode_Perusahaan = '" & KodePerusahaan & "' AND a.Status IS NULL "
			SQL = SQL & "AND a.Kode_Unik_Print = '" & kode_unik_pallet & "' AND b.Status IS NULL "
			SQL = SQL & "AND b.Line = '" & Label14.Text & "' AND c.Status IS NULL "
			SQL = SQL & "GROUP BY p.No_Production_Order, a.Kode_Unik_Print, b.Kode_Barang, f.Nama, a.Jumlah, b.Line "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							SQL = "insert into N_EMI_Cetak_Label_Packing_Pallet (Kode_Perusahaan, Kode_Unik_Print, Barcode, Kode_Barang, Nama_Barang, Tgl_Produksi, Tgl_Expired, Jumlah, Line, No_Production_Order) "
							SQL = SQL & "values('" & KodePerusahaan & "', '" & .Rows(i).Item("Kode_Unik_Print") & "', @newBarcode" & kode_unik_pallet & ", "
							SQL = SQL & "'" & .Rows(i).Item("Kode_Barang") & "', '" & .Rows(i).Item("Nama_Barang") & "', "
							SQL = SQL & "'" & .Rows(i).Item("Tgl_Produksi") & "', '" & .Rows(i).Item("Tgl_Expired") & "', "
							SQL = SQL & "'" & .Rows(i).Item("Jumlah") & "', '" & Label14.Text & "', '" & .Rows(i).Item("No_Production_Order") & "') "
							ExecuteTrans(SQL)
						Next
					End If
				End With
			End Using

			Cmd.Transaction.Commit()
			'MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()

			Dim CrDoc As New Object
			Dim KertasKecil As String = "BarcodeQC"

			SQL = "select kode_perusahaan from N_EMI_Cetak_Label_Packing_Pallet where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Unik_Print = '" & kode_unik_pallet & "'"
			Using Ds = BindingTrans(SQL)
				If Ds.Tables("MyTable").Rows.Count <> 0 Then

					'==========================
					'=     BARCODEE BESAR     =
					'==========================
					Dim printerDitemukan As Boolean = False
					For Each printer As String In PrinterSettings.InstalledPrinters
						If printer.ToLower() = PrinterBarcode.ToLower() Then
							printerDitemukan = True
							Exit For
						End If
					Next

					If printerDitemukan Then

						CrDoc = New N_EMI_Label_Barcode_Box_Pallet

						'    With A_Place_For_Printing
						'    CrDoc.SetDataSource(Ds)
						'    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						'    CrDoc.PrintOptions.PrinterName = ""
						'    CrDoc.RecordSelectionFormula = "{N_EMI_Cetak_Label_Packing_Pallet.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Cetak_Label_Packing_Pallet.Kode_Unik_Print} = '" & kode_unik_pallet & "' "
						'    CrDoc.SummaryInfo.ReportTitle = "Label Good Received 2"
						'    .Text = "Label Good Received 2"
						'    .CrystalReportViewer1.ReportSource = CrDoc
						'    .Refresh()
						'    .Show()
						'End With

						'=====================================================

						Dim doctoprint As New System.Drawing.Printing.PrintDocument()
						CrDoc.SetDataSource(Ds)
						CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						CrDoc.RecordSelectionFormula = "{N_EMI_Cetak_Label_Packing_Pallet.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Cetak_Label_Packing_Pallet.Kode_Unik_Print} = '" & kode_unik_pallet & "' "
						CrDoc.PrintOptions.PrinterName = PrinterBarcode

						doctoprint.PrinterSettings.PrinterName = PrinterBarcode

						Dim rawKind As Integer
						Dim foundPaper As Boolean = False
						CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
						For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
							If doctoprint.PrinterSettings.PaperSizes(j).PaperName = KertasKecil Then
								rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
								CrDoc.PrintOptions.PaperSize = rawKind
								foundPaper = True
								Exit For
							End If
						Next

						If Not foundPaper Then
							CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
							MessageBox.Show("Kertas Tidak Ditemukan, Menggunakan Kertas Default", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

						End If

						CrDoc.PrintToPrinter(1, False, 1, 2500)
					Else
						MessageBox.Show("Printer FG Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					End If
					printerDitemukan = False
				Else
					MessageBox.Show("Printer Q Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				End If

			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub xprint(ByVal xkdunik As String)
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'HAPUS TABEL SEMENTARA
			SQL = "delete from N_EMI_Cetak_Label_Packing_Box where Line = '" & Label14.Text & "' "
			ExecuteTrans(SQL)

			Barcode.Image = Nothing
			Barcode.Image = Generate_QR_NoPadding(xkdunik)
			Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & xkdunik & ".jpg")
			Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
			'End If

			fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
			FileSize1 = fs1.Length
			rawData1 = New Byte(FileSize1) {}
			fs1.Read(rawData1, 0, FileSize1)
			fs1.Close()
			Cmd.Parameters.Add("@newBarcode" & xkdunik, SqlDbType.Image).Value = rawData1

			SQL = "select a.Kode_Perusahaan, d.Kode_Unik_Print, min(b.Tgl_Produksi) as Tgl_Produksi, "
			SQL = SQL & "min(b.Tgl_Expired) as Tgl_Expired, d.No_Transaksi, e.Shift, e.Line, g.Kode_Barang, g.Nama as Nama_Barang "
			SQL = SQL & "from N_EMI_Transaksi_Packing_Det a, N_EMI_Transaksi_Packing_Detail b, N_EMI_Transaksi_Packing c, "
			SQL = SQL & "N_EMI_Transaksi_Packing_Box d, N_EMI_Transaksi_Packing_Check_In_Out e, Barang_SN f, Barang g "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Urut_Detail_Transaksi_Packing = b.Urut_Oto and a.Status is null "
			SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Transaksi = c.No_Transaksi and c.Status is null "
			SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Urut_Transaksi_Box = d.Urut_oto and d.Status is null "
			SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and d.No_Transaksi = e.No_Transaksi and e.Status is null "
			SQL = SQL & "and b.SN_Baru = f.Serial_Number and f.Kode_Perusahaan = g.Kode_Perusahaan "
			SQL = SQL & "and f.Kode_Stock_Owner = g.Kode_Stock_Owner and f.Kode_Barang = g.Kode_Barang "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and d.Kode_Unik_Print = '" & xkdunik & "' and d.Kode_Barang = '" & Label4.Text & "' "
			SQL = SQL & "group by a.Kode_Perusahaan, d.Kode_Unik_Print, d.No_Transaksi, e.Shift, e.Line, g.Kode_Barang, g.Nama "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							SQL = "insert into N_EMI_Cetak_Label_Packing_Box(Kode_Perusahaan, Kode_Unik_Print, Barcode, Tgl_Produksi, Tgl_Expired, "
							SQL = SQL & "No_Transaksi_Box, Line, Shift, Kode_Barang, Nama_Barang) "
							SQL = SQL & "values('" & KodePerusahaan & "', '" & .Rows(i).Item("Kode_Unik_Print") & "', @newBarcode" & xkdunik & ", "
							SQL = SQL & "'" & .Rows(i).Item("Tgl_Produksi") & "', '" & .Rows(i).Item("Tgl_Expired") & "', '" & .Rows(i).Item("No_Transaksi") & "', "
							SQL = SQL & "'" & .Rows(i).Item("Line") & "', '" & .Rows(i).Item("Shift") & "', '" & .Rows(i).Item("Kode_Barang") & "', '" & .Rows(i).Item("Nama_Barang") & "') "
							ExecuteTrans(SQL)
						Next
					End If
				End With
			End Using

			Cmd.Transaction.Commit()
			'MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()

			Dim CrDoc As New Object
			Dim KertasKecil As String = "BarcodeQC"

			SQL = "select kode_perusahaan from N_EMI_Cetak_Label_Packing_Box where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Unik_Print = '" & xkdunik & "'"
			Using Ds = BindingTrans(SQL)
				If Ds.Tables("MyTable").Rows.Count <> 0 Then

					'==========================
					'=     BARCODEE BESAR     =
					'==========================
					Dim printerDitemukan As Boolean = False
					For Each printer As String In PrinterSettings.InstalledPrinters
						If printer.ToLower() = PrinterBarcode.ToLower() Then
							printerDitemukan = True
							Exit For
						End If
					Next

					If printerDitemukan Then

						CrDoc = New N_EMI_Label_Barcode_Box_Kecil

						'    With A_Place_For_Printing2
						'    CrDoc.SetDataSource(Ds)
						'    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						'    CrDoc.PrintOptions.PrinterName = ""
						'    CrDoc.RecordSelectionFormula = "{N_EMI_Cetak_Label_Packing_Box.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Cetak_Label_Packing_Box.Kode_Unik_Print} = '" & xkdunik & "' "
						'    CrDoc.SummaryInfo.ReportTitle = "Label Good Received 2"
						'    .Text = "Label Good Received 2"
						'    .CrystalReportViewer1.ReportSource = CrDoc
						'    .Refresh()
						'    .Show()
						'End With

						'=====================================================

						Dim doctoprint As New System.Drawing.Printing.PrintDocument()
						CrDoc.SetDataSource(Ds)
						CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						CrDoc.RecordSelectionFormula = "{N_EMI_Cetak_Label_Packing_Box.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Cetak_Label_Packing_Box.Kode_Unik_Print} = '" & xkdunik & "' "
						CrDoc.PrintOptions.PrinterName = PrinterBarcode

						doctoprint.PrinterSettings.PrinterName = PrinterBarcode

						Dim rawKind As Integer
						Dim foundPaper As Boolean = False
						CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
						For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
							If doctoprint.PrinterSettings.PaperSizes(j).PaperName = KertasKecil Then
								rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
								CrDoc.PrintOptions.PaperSize = rawKind
								foundPaper = True
								Exit For
							End If
						Next

						If Not foundPaper Then
							CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
							MessageBox.Show("Kertas Tidak Ditemukan, Menggunakan Kertas Default", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

						End If

						CrDoc.PrintToPrinter(1, False, 1, 2500)
					Else
						MessageBox.Show("Printer FG Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					End If
					printerDitemukan = False
				Else
					MessageBox.Show("Printer Q Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				End If

			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Function Generate_QR_NoPadding(ByVal isi As String)

		Dim options As New ZXing.QrCode.QrCodeEncodingOptions()

		options.DisableECI = True
		options.CharacterSet = "UTF-8"
		options.Width = 80
		options.Height = 80
		options.Margin = 0

		Dim qr As New ZXing.BarcodeWriter()
		qr.Format = ZXing.BarcodeFormat.QR_CODE
		qr.Options = options

		Dim result As New Bitmap(qr.Write(isi))
		Return result
	End Function

	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		If Label14.Text.Trim.Length = 0 Then Exit Sub

		Dim Hapus As String = MessageBox.Show("Barang belum memenuhi kapasitas 1 pallet, apakah mau cetak barcode pallet?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
		If Hapus = vbYes Then
			xprint_pallet()
		End If
		get_data()
		get_log_scan()
		get_data_repacking()
	End Sub

	Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
		'If e.RowIndex >= 0 Then
		'    If e.ColumnIndex = 2 Then
		'        xprint(DataGridView2.Rows(e.RowIndex).Cells(0).Value)
		'    End If
		'End If
	End Sub

	Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
		Me.Close()
	End Sub

	Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
		If e.RowIndex >= 0 Then
			If e.ColumnIndex = 8 Then
				N_EMI_SD_Waste_Scan_Packing_Pin.Label3.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value
				N_EMI_SD_Waste_Scan_Packing_Pin.asal = "N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru - Hold"
				N_EMI_SD_Waste_Scan_Packing_Pin.TextBox2.Focus()
				N_EMI_SD_Waste_Scan_Packing_Pin.ShowDialog()
			End If
		End If
	End Sub

	Private Sub get_no_transaksi_pallet()
		Dim FPro_Results As String = "TP"
		xno_transaksi_pallet = FPro_Results & "-" & Format(tgl_skg, "MM/yy") & "-" &
													  General_Class.Get_Last_Number2("N_EMI_Transaksi_Packing_Pallet", "No_Transaksi", 5,
													  "Kode_perusahaan", KodePerusahaan,
													  "And", "substring(No_Transaksi,1," & Len(FPro_Results) + 6 & ")", FPro_Results & "-" & Format(tgl_skg, "MM/yy"))
	End Sub

	Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
		If ComboBox1.SelectedIndex = -1 Then Exit Sub

		Try
			OpenConn()

			SQL = "select No_Transaksi, Line, Shift from N_EMI_Transaksi_Packing_Check_In_Out where Status is null "
			SQL = SQL & "and Kode_Perusahaan = '" & KodePerusahaan & "' and Tanggal_Out is null and Line = '" & ComboBox1.Text & "' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					Label12.Text = dr("No_Transaksi")
					Label13.Text = dr("Shift")
					Label14.Text = dr("Line")
				Else
					dr.Close()
					CloseConn()
					MessageBox.Show("Line belum melakukan check in", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		get_data()
		get_log_scan()
		get_data_repacking()
	End Sub

	Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
		get_data()
		get_data_repacking()
		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			SQL = "select No_Transaksi, Line, Shift from N_EMI_Transaksi_Packing_Check_In_Out where Status is null "
			SQL = SQL & "and Kode_Perusahaan = '" & KodePerusahaan & "' and Tanggal_Out is null and Line = '" & ComboBox1.Text & "' "
			Using dr = OpenTrans(SQL)
				If Not dr.Read Then
					dr.Close()
					CloseConn()
					MessageBox.Show("Line belum melakukan check in", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					kosong()
					Exit Sub
				End If
			End Using

			SQL = "select e.Kode_Barang "
			SQL = SQL & "from N_EMI_Transaksi_Packing_Repacking a, N_EMI_Transaksi_Packing_Repacking_Detail b, "
			SQL = SQL & "N_EMI_Transaksi_Packing_Detail c, Barang_SN d, Barang e "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi and a.Status is null and a.Flag_Selesai is null "
			SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Urut_Detail_Transaksi_Packing = c.Urut_Oto "
			SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.SN_Baru = d.Serial_Number and d.Kode_Perusahaan = e.Kode_Perusahaan "
			SQL = SQL & "and d.Kode_Stock_Owner = e.Kode_Stock_Owner and d.Kode_Barang = e.Kode_Barang "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Line = '" & Label14.Text & "' "
			SQL = SQL & "group by e.Kode_Barang "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					If dr("Kode_Barang") <> Label4.Text Then
						dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("data repacking berbeda dengan barang yang ada diline", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End If
			End Using

			Dim xjumlah_repacking As Integer = 0
			SQL = "select isnull(sum(a.Jumlah), 0) as Jumlah from N_EMI_Transaksi_Packing_Repacking a "
			SQL = SQL & "where a.Flag_Selesai is null and a.Status is null and a.Line = '" & Label14.Text & "' "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					xjumlah_repacking = dr("Jumlah")
				End If
			End Using

			SQL = "WITH cte AS ( "
			SQL = SQL & "select c.No_Transaksi, a.Line, e.Kode_Barang, e.Nama as Nama_Barang, c.SN_Baru, "
			SQL = SQL & "c.Jumlah - c.Jumlah_Sdh_Packing - c.Jumlah_Waste as Jmlh_Sisa, "
			SQL = SQL & "b.Urut_Detail_Transaksi_Packing, b.Urut_Results_Detail_Pallet, "
			SQL = SQL & "'REPACKING' as Jenis, d.Tgl_Produksi, f.Tanggal, f.Jam "
			SQL = SQL & "from N_EMI_Transaksi_Packing_Repacking a "
			SQL = SQL & "join N_EMI_Transaksi_Packing_Repacking_Detail b "
			SQL = SQL & "on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
			SQL = SQL & "join N_EMI_Transaksi_Packing_Detail c "
			SQL = SQL & "on b.Kode_Perusahaan = c.Kode_Perusahaan "
			SQL = SQL & "and b.Urut_Detail_Transaksi_Packing = c.Urut_Oto "
			SQL = SQL & "join Barang_SN d "
			SQL = SQL & "on c.Kode_Perusahaan = d.Kode_Perusahaan and c.SN_Baru = d.Serial_Number "
			SQL = SQL & "join Barang e "
			SQL = SQL & "on d.Kode_Perusahaan = e.Kode_Perusahaan "
			SQL = SQL & "and d.Kode_Stock_Owner = e.Kode_Stock_Owner "
			SQL = SQL & "and d.Kode_Barang = e.Kode_Barang "
			SQL = SQL & "join N_EMI_Transaksi_Packing f "
			SQL = SQL & "on c.Kode_Perusahaan = f.Kode_Perusahaan and c.No_Transaksi = f.No_Transaksi "
			SQL = SQL & "where a.Status is null and f.Status is null and a.Flag_Selesai is null  "
			SQL = SQL & "and c.Jumlah - c.Jumlah_Sdh_Packing - c.Jumlah_Waste <> 0 "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Line = '" & Label14.Text & "' "
			SQL = SQL & "and d.Kode_Barang = '" & Label4.Text & "' "

			SQL = SQL & "UNION ALL "

			SQL = SQL & "select d.No_Transaksi, a.Line, b.Kode_Barang, c.Nama as Nama_Barang, d.SN_Baru, "
			SQL = SQL & "d.Jumlah - d.Jumlah_Sdh_Packing - d.Jumlah_Waste as Jmlh_Sisa, d.Urut_Oto, d.Urut_Results_Detail_Pallet, "
			SQL = SQL & "'PACKING' as Jenis, b.Tgl_Produksi, a.Tanggal, a.Jam "
			SQL = SQL & "from N_EMI_Transaksi_Packing a "
			SQL = SQL & "join N_EMI_Transaksi_Packing_Detail d "
			SQL = SQL & "on a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Transaksi = d.No_Transaksi "
			SQL = SQL & "join Barang_SN b "
			SQL = SQL & "on d.SN_Baru = b.Serial_Number and a.Kode_Perusahaan = b.Kode_Perusahaan "
			SQL = SQL & "join Barang c "
			SQL = SQL & "on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
			SQL = SQL & "and b.Kode_Barang = c.Kode_Barang "
			SQL = SQL & "where a.Status is null and a.Flag_Hold is null and a.Flag_Selesai is null "
			SQL = SQL & "and d.Jumlah - d.Jumlah_Sdh_Packing - d.Jumlah_Waste <> 0 "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Line = '" & Label14.Text & "' "
			SQL = SQL & "and b.Kode_Barang = '" & Label4.Text & "' ), "
			SQL = SQL & "final AS ( "
			SQL = SQL & "SELECT *, ROW_NUMBER() OVER ("
			SQL = SQL & "PARTITION BY No_Transaksi, Line, Kode_Barang, Nama_Barang, SN_Baru, "
			SQL = SQL & "Jmlh_Sisa, Urut_Detail_Transaksi_Packing, Urut_Results_Detail_Pallet, "
			SQL = SQL & "Tgl_Produksi, Tanggal, Jam ORDER BY "
			SQL = SQL & "CASE WHEN Jenis = 'REPACKING' THEN 1 ELSE 2 END "
			SQL = SQL & ") AS rn FROM cte) "

			SQL = SQL & "SELECT sum(Jmlh_Sisa) as Jmlh_Sisa "
			SQL = SQL & "FROM final WHERE rn = 1  "
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					If dr("Jmlh_Sisa") < xjumlah_repacking Then
						dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("stock tidak mencukupi untuk repacking", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End If
			End Using

			arrno_repack.Clear()
			SQL = "select a.No_Transaksi, a.Urut_Pallet, a.Jumlah from N_EMI_Transaksi_Packing_Repacking a "
			SQL = SQL & "where a.Flag_Selesai is null and a.Status is null and a.Line = '" & Label14.Text & "' order by a.No_Transaksi "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							Dim Kode_Berjalan As String = ""
							Dim isUnique As Boolean = False

							Do While isUnique = False
								Kode_Berjalan = "B" & Generate_Random_Kode(15)

								SQL = "SELECT Kode_Unik_Print FROM N_EMI_Transaksi_Packing_Box WHERE Kode_Unik_Print = '" & Kode_Berjalan & "' "
								Using Ds2 = BindingTrans(SQL)
									If Ds2.Tables("MyTable").Rows.Count = 0 Then
										isUnique = True
									End If
								End Using
							Loop

							kode_unik = Kode_Berjalan
							arrno_repack.Add(kode_unik)

							SQL = "insert into N_EMI_Transaksi_Packing_Box(Kode_Perusahaan, No_Transaksi, Kode_Unik_Print, Kode_Barang, Line, Jumlah, Tgl_Cetak, Jam_Cetak, Flag_Pallet, Urut_Pallet, Flag_Repacking) "
							SQL = SQL & "values('" & KodePerusahaan & "', '" & Label12.Text.Trim & "', '" & kode_unik & "', '" & Label4.Text & "', '" & Label14.Text & "', "
							SQL = SQL & "'" & .Rows(i).Item("Jumlah") & "','" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', 'Y', '" & .Rows(i).Item("Urut_Pallet") & "', 'Y') "
							ExecuteTrans(SQL)

							Dim x_no_urut_packing_box As Integer = 0
							SQL = "select IDENT_CURRENT('N_EMI_Transaksi_Packing_Box') as urutan"
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									x_no_urut_packing_box = Dr("urutan")
								End If
							End Using

							SQL = "update N_EMI_Transaksi_Packing_Pallet set Jumlah = Jumlah + 1 where Urut_Oto = '" & .Rows(i).Item("Urut_Pallet") & "' "
							ExecuteTrans(SQL)

							'SQL = "with cte as( "
							'SQL = SQL & "select c.No_Transaksi, a.Line, e.Kode_Barang, e.Nama as Nama_Barang, c.SN_Baru, sum(b.Jumlah_Good) as Jmlh_Sisa, "
							'SQL = SQL & "b.Urut_Detail_Transaksi_Packing, b.Urut_Results_Detail_Pallet, 'REPACKING' as Jenis, min(d.Tgl_Produksi) as Tgl_Produksi, f.Tanggal, f.Jam "
							'SQL = SQL & "from N_EMI_Transaksi_Packing_Repacking a, N_EMI_Transaksi_Packing_Repacking_Detail b, "
							'SQL = SQL & "N_EMI_Transaksi_Packing_Detail c, Barang_SN d, Barang e, N_EMI_Transaksi_Packing f  "
							'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi and a.Status is null and a.Flag_Selesai is null "
							'SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Urut_Detail_Transaksi_Packing = c.Urut_Oto "
							'SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.SN_Baru = d.Serial_Number and d.Kode_Perusahaan = e.Kode_Perusahaan "
							'SQL = SQL & "and d.Kode_Stock_Owner = e.Kode_Stock_Owner and d.Kode_Barang = e.Kode_Barang "
							'SQL = SQL & "and c.Kode_Perusahaan = f.Kode_Perusahaan and c.No_Transaksi = f.No_Transaksi and f.Status is null "
							'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Line = '" & Label14.Text & "' and d.Kode_Barang = '" & Label4.Text & "' "
							''SQL = SQL & "and a.No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
							'SQL = SQL & "group by c.No_Transaksi, a.Line, e.Kode_Barang, e.Nama , c.SN_Baru, b.Urut_Detail_Transaksi_Packing, b.Urut_Results_Detail_Pallet, f.Tanggal, f.Jam ), "

							'SQL = SQL & "cta as( "
							'SQL = SQL & "select d.No_Transaksi, a.Line, b.Kode_Barang, c.Nama as Nama_Barang, d.SN_Baru, d.Jumlah - d.Jumlah_Sdh_Packing - d.Jumlah_Waste as Jmlh_Sisa, "
							'SQL = SQL & "d.Urut_Oto as Urut_Detail_Transaksi_Packing, d.Urut_Results_Detail_Pallet,'PACKING' as Jenis, b.Tgl_Produksi, a.Tanggal, a.Jam "
							'SQL = SQL & "from N_EMI_Transaksi_Packing a, N_EMI_Transaksi_Packing_Detail d, Barang_SN b, Barang c "
							'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
							'SQL = SQL & "and d.SN_Baru = b.Serial_Number and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Transaksi = d.No_Transaksi "
							'SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and a.Status is null "
							'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Line = '" & Label14.Text & "' and b.Kode_Barang = '" & Label4.Text & "' "
							'SQL = SQL & "and d.Jumlah - d.Jumlah_Sdh_Packing - d.Jumlah_Waste <> 0 and a.Flag_Hold is null and a.Flag_Selesai is null ) "

							'SQL = SQL & "select No_Transaksi, Line, Kode_Barang, Nama_Barang, SN_Baru, Jmlh_Sisa, Urut_Detail_Transaksi_Packing, Urut_Results_Detail_Pallet, Jenis, Tgl_Produksi, Tanggal, Jam "
							'SQL = SQL & "from cte where Jmlh_Sisa <> 0 "
							'SQL = SQL & "union all "
							'SQL = SQL & "select No_Transaksi, Line, Kode_Barang, Nama_Barang, SN_Baru, Jmlh_Sisa, Urut_Detail_Transaksi_Packing, Urut_Results_Detail_Pallet, Jenis, Tgl_Produksi, Tanggal, Jam "
							'SQL = SQL & "from cta "
							'SQL = SQL & "order by Jenis desc, Tanggal, Jam, Tgl_Produksi "

							Dim isi_satuan As Integer = 0
							Dim sisa As Integer = 0
							SQL = "WITH cte AS ( "
							SQL = SQL & "select c.No_Transaksi, a.Line, e.Kode_Barang, e.Nama as Nama_Barang, c.SN_Baru, "
							SQL = SQL & "c.Jumlah - c.Jumlah_Sdh_Packing - c.Jumlah_Waste as Jmlh_Sisa, "
							SQL = SQL & "b.Urut_Detail_Transaksi_Packing, b.Urut_Results_Detail_Pallet, "
							SQL = SQL & "'REPACKING' as Jenis, d.Tgl_Produksi, f.Tanggal, f.Jam "
							SQL = SQL & "from N_EMI_Transaksi_Packing_Repacking a "
							SQL = SQL & "join N_EMI_Transaksi_Packing_Repacking_Detail b "
							SQL = SQL & "on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
							SQL = SQL & "join N_EMI_Transaksi_Packing_Detail c "
							SQL = SQL & "on b.Kode_Perusahaan = c.Kode_Perusahaan "
							SQL = SQL & "and b.Urut_Detail_Transaksi_Packing = c.Urut_Oto "
							SQL = SQL & "join Barang_SN d "
							SQL = SQL & "on c.Kode_Perusahaan = d.Kode_Perusahaan and c.SN_Baru = d.Serial_Number "
							SQL = SQL & "join Barang e "
							SQL = SQL & "on d.Kode_Perusahaan = e.Kode_Perusahaan "
							SQL = SQL & "and d.Kode_Stock_Owner = e.Kode_Stock_Owner "
							SQL = SQL & "and d.Kode_Barang = e.Kode_Barang "
							SQL = SQL & "join N_EMI_Transaksi_Packing f "
							SQL = SQL & "on c.Kode_Perusahaan = f.Kode_Perusahaan and c.No_Transaksi = f.No_Transaksi "
							SQL = SQL & "where a.Status is null and f.Status is null and a.Flag_Selesai is null  "
							SQL = SQL & "and c.Jumlah - c.Jumlah_Sdh_Packing - c.Jumlah_Waste <> 0 "
							SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "and a.Line = '" & Label14.Text & "' "
							SQL = SQL & "and d.Kode_Barang = '" & Label4.Text & "' "

							SQL = SQL & "UNION ALL "

							SQL = SQL & "select d.No_Transaksi, a.Line, b.Kode_Barang, c.Nama as Nama_Barang, d.SN_Baru, "
							SQL = SQL & "d.Jumlah - d.Jumlah_Sdh_Packing - d.Jumlah_Waste as Jmlh_Sisa, d.Urut_Oto, d.Urut_Results_Detail_Pallet, "
							SQL = SQL & "'PACKING' as Jenis, b.Tgl_Produksi, a.Tanggal, a.Jam "
							SQL = SQL & "from N_EMI_Transaksi_Packing a "
							SQL = SQL & "join N_EMI_Transaksi_Packing_Detail d "
							SQL = SQL & "on a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Transaksi = d.No_Transaksi "
							SQL = SQL & "join Barang_SN b "
							SQL = SQL & "on d.SN_Baru = b.Serial_Number and a.Kode_Perusahaan = b.Kode_Perusahaan "
							SQL = SQL & "join Barang c "
							SQL = SQL & "on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
							SQL = SQL & "and b.Kode_Barang = c.Kode_Barang "
							SQL = SQL & "where a.Status is null and a.Flag_Hold is null and a.Flag_Selesai is null "
							SQL = SQL & "and d.Jumlah - d.Jumlah_Sdh_Packing - d.Jumlah_Waste <> 0 "
							SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "and a.Line = '" & Label14.Text & "' "
							SQL = SQL & "and b.Kode_Barang = '" & Label4.Text & "' ), "
							SQL = SQL & "final AS ( "
							SQL = SQL & "SELECT *, ROW_NUMBER() OVER ("
							SQL = SQL & "PARTITION BY No_Transaksi, Line, Kode_Barang, Nama_Barang, SN_Baru, "
							SQL = SQL & "Jmlh_Sisa, Urut_Detail_Transaksi_Packing, Urut_Results_Detail_Pallet, "
							SQL = SQL & "Tgl_Produksi, Tanggal, Jam ORDER BY "
							SQL = SQL & "CASE WHEN Jenis = 'REPACKING' THEN 1 ELSE 2 END "
							SQL = SQL & ") AS rn FROM cte) "

							SQL = SQL & "SELECT No_Transaksi, Line, Kode_Barang, Nama_Barang, SN_Baru, Jmlh_Sisa, "
							SQL = SQL & "Urut_Detail_Transaksi_Packing, Urut_Results_Detail_Pallet, Jenis, Tgl_Produksi, Tanggal, Jam "
							SQL = SQL & "FROM final WHERE rn = 1 ORDER BY Jenis DESC, Tanggal, Jam, Tgl_Produksi, No_Transaksi "
							Using Ds2 = BindingTrans(SQL)

								If Ds2.Tables("MyTable").Rows.Count <> 0 Then
									isi_satuan = .Rows(i).Item("Jumlah")
									sisa = .Rows(i).Item("Jumlah")
									For z As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

										If sisa = 0 Then
											Exit For
										ElseIf sisa < 0 Then
											CloseTrans()
											CloseConn()
											MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If

										If sisa < Ds2.Tables("MyTable").Rows(z).Item("Jmlh_Sisa") Or sisa = Ds2.Tables("MyTable").Rows(z).Item("Jmlh_Sisa") Then
											SQL = "insert into N_EMI_Transaksi_Packing_Det(Kode_Perusahaan, Urut_Detail_Transaksi_Packing, Urut_Transaksi_Box, Jumlah) "
											SQL = SQL & "values('" & KodePerusahaan & "', '" & Ds2.Tables("MyTable").Rows(z).Item("Urut_Detail_Transaksi_Packing") & "', '" & x_no_urut_packing_box & "', "
											SQL = SQL & "'" & sisa & "' ) "
											ExecuteTrans(SQL)

											SQL = "update N_EMI_Transaksi_Packing_Detail set Jumlah_Sdh_Packing = Jumlah_Sdh_Packing + '" & sisa & "' where "
											SQL = SQL & "Urut_Oto = '" & Ds2.Tables("MyTable").Rows(z).Item("Urut_Detail_Transaksi_Packing") & "' "
											ExecuteTrans(SQL)

											SQL = "update Emi_Production_Results_Detail_Pallet set Jumlah_Sdh_Packing = Jumlah_Sdh_Packing + '" & sisa & "' where "
											SQL = SQL & "Urut_Oto = '" & Ds2.Tables("MyTable").Rows(z).Item("Urut_Results_Detail_Pallet") & "' "
											ExecuteTrans(SQL)

											sisa = 0
										ElseIf sisa > Ds2.Tables("MyTable").Rows(z).Item("Jmlh_Sisa") Then
											SQL = "insert into N_EMI_Transaksi_Packing_Det(Kode_Perusahaan, Urut_Detail_Transaksi_Packing, Urut_Transaksi_Box, Jumlah) "
											SQL = SQL & "values('" & KodePerusahaan & "', '" & Ds2.Tables("MyTable").Rows(z).Item("Urut_Detail_Transaksi_Packing") & "', '" & x_no_urut_packing_box & "', "
											SQL = SQL & "'" & Ds2.Tables("MyTable").Rows(z).Item("Jmlh_Sisa") & "' ) "
											ExecuteTrans(SQL)

											SQL = "update N_EMI_Transaksi_Packing_Detail set Jumlah_Sdh_Packing = Jumlah_Sdh_Packing + '" & Ds2.Tables("MyTable").Rows(z).Item("Jmlh_Sisa") & "' where "
											SQL = SQL & "Urut_Oto = '" & Ds2.Tables("MyTable").Rows(z).Item("Urut_Detail_Transaksi_Packing") & "' "
											ExecuteTrans(SQL)

											SQL = "update Emi_Production_Results_Detail_Pallet set Jumlah_Sdh_Packing = Jumlah_Sdh_Packing + '" & Ds2.Tables("MyTable").Rows(z).Item("Jmlh_Sisa") & "' where "
											SQL = SQL & "Urut_Oto = '" & Ds2.Tables("MyTable").Rows(z).Item("Urut_Results_Detail_Pallet") & "' "
											ExecuteTrans(SQL)

											sisa = sisa - Ds2.Tables("MyTable").Rows(z).Item("Jmlh_Sisa")
										Else
											CloseTrans()
											CloseConn()
											MessageBox.Show("Barang tidak cukup harap scan barang terlebih dahulu !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If

										If Ds2.Tables("MyTable").Rows(z).Item("Jenis") = "PACKING" Then
											SQL = "select sum(a.Jumlah) as Jumlah, sum(a.Jumlah_Sdh_Packing) as Jumlah_Sdh_Packing, sum(a.Jumlah_Waste) as Jumlah_Waste "
											SQL = SQL & "from N_EMI_Transaksi_Packing_Detail a, N_EMI_Transaksi_Packing b "
											SQL = SQL & "where a.No_Transaksi = '" & Ds2.Tables("MyTable").Rows(z).Item("No_Transaksi") & "' and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
											SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
											Using dr = OpenTrans(SQL)
												If dr.Read Then
													If dr("Jumlah") = dr("Jumlah_Sdh_Packing") + dr("Jumlah_Waste") Then
														dr.Close()
														SQL = "update N_EMI_Transaksi_Packing set Flag_Selesai = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' "
														SQL = SQL & "and No_Transaksi = '" & Ds2.Tables("MyTable").Rows(z).Item("No_Transaksi") & "' "
														ExecuteTrans(SQL)
													End If
												End If
											End Using
										End If
									Next
								Else
									CloseTrans()
									CloseConn()
									MessageBox.Show("Barang tidak ditemukan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If

							End Using

							SQL = "update N_EMI_Transaksi_Packing_Repacking set Flag_Selesai = 'Y', Urut_Box_Baru = '" & x_no_urut_packing_box & "' where Kode_Perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
							ExecuteTrans(SQL)

						Next
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show("data repacking tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			Cmd.Transaction.Commit()
			MessageBox.Show("Repacking Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'For a As Integer = 0 To arrno_repack.Count - 1
		'    Dim kode_unik As String = CStr(arrno_repack(a))
		'    xprint(kode_unik)
		'Next

		get_data()
		get_log_scan()
		get_data_repacking()
	End Sub

	Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
		If Label14.Text.Trim.Length = 0 Then Exit Sub
		Dim ajumlah As Integer = 0
		xFlag_OK = ""
		Try
			OpenConn()

			SQL = "select No_Transaksi, Line, Shift from N_EMI_Transaksi_Packing_Check_In_Out where Status is null "
			SQL = SQL & "and Kode_Perusahaan = '" & KodePerusahaan & "' and Tanggal_Out is null and Line = '" & ComboBox1.Text & "' "
			Using dr = OpenTrans(SQL)
				If Not dr.Read Then
					dr.Close()
					CloseConn()
					MessageBox.Show("Line belum melakukan check in", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					kosong()
					Exit Sub
				End If
			End Using

			SQL = "select top(1)b.Isi_Satuan_Pallet from Barang b where b.Kode_Barang = '" & Label4.Text & "' and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					ajumlah = Dr("Isi_Satuan_Pallet")
				Else
					Dr.Close()
					CloseConn()
					MessageBox.Show("Barang tidak diemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
		N_EMI_SD_Waste_Scan_Packing.Label1.Text = "Pallet"
		N_EMI_SD_Waste_Scan_Packing.asal = "N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru - Cetak Pallet"
		N_EMI_SD_Waste_Scan_Packing.Label6.Text = "Masukan Jumlah Box"
		N_EMI_SD_Waste_Scan_Packing.Label3.Text = ajumlah
		N_EMI_SD_Waste_Scan_Packing.kosong()
		N_EMI_SD_Waste_Scan_Packing.ShowDialog()
	End Sub

	Public Sub xprint_pallet_new()
		If Label14.Text.Trim.Length = 0 Then Exit Sub

		get_jam()
		get_no_transaksi_pallet()
		get_data()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			SQL = "select No_Transaksi, Line, Shift from N_EMI_Transaksi_Packing_Check_In_Out where Status is null "
			SQL = SQL & "and Kode_Perusahaan = '" & KodePerusahaan & "' and Tanggal_Out is null and Line = '" & ComboBox1.Text & "' "
			Using dr = OpenTrans(SQL)
				If Not dr.Read Then
					dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Line belum melakukan check in", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					kosong()
					Exit Sub
				End If
			End Using

			'generate kode unik pallet
			Dim Kode_Berjalan As String = ""
			Dim isUnique As Boolean = False

			Do While isUnique = False
				Kode_Berjalan = "P" & Generate_Random_Kode(15)

				SQL = "SELECT Kode_Unik_Print FROM N_EMI_Transaksi_Packing_Pallet WHERE Kode_Unik_Print = '" & Kode_Berjalan & "' "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count = 0 Then
							isUnique = True
						End If
					End With
				End Using
			Loop

			kode_unik_pallet = Kode_Berjalan

			'insert ke tabel N_EMI_Transaksi_Packing_Pallet
			SQL = "insert into N_EMI_Transaksi_Packing_Pallet(Kode_Perusahaan, No_Transaksi, Kode_Unik_Print, Jumlah, Tgl_Cetak, Jam_Cetak, Flag_OK_QA) "
			SQL = SQL & "values('" & KodePerusahaan & "', '" & xno_transaksi_pallet & "', '" & kode_unik_pallet & "', '" & Val(Label11.Text) & "', "
			SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & xFlag_OK & "' ) "
			ExecuteTrans(SQL)

			Dim x_no_urut_packing_pallet As Integer = 0
			SQL = "select IDENT_CURRENT('N_EMI_Transaksi_Packing_Pallet') as urutan"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					x_no_urut_packing_pallet = Dr("urutan")
				End If
			End Using

			'update flag_pallet shift sebelumnya
			Dim sisa_box As Integer = Val(Label11.Text)
			SQL = "select a.Urut_Oto from N_EMI_Transaksi_Packing_Box a where a.Status is null and a.Kode_Barang = '" & Label4.Text & "' "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Line = '" & Label14.Text & "' and a.Flag_Pallet is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							SQL = "update N_EMI_Transaksi_Packing_Box set Flag_Pallet = 'Y', Urut_Pallet = '" & x_no_urut_packing_pallet & "' "
							SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Urut_Oto = '" & .Rows(i).Item("Urut_Oto") & "' "
							ExecuteTrans(SQL)

							sisa_box = sisa_box - 1
						Next
					End If
				End With
			End Using

			'========================
			'=     CARA OPTIMAL     =
			'========================

#Region "Cara Optimasi"

			'SQL = "
			'	;with cte as (
			'		select a.Kode_Perusahaan, a.Urut_Oto from N_EMI_Transaksi_Packing_Box a where a.Status is null and a.Kode_Barang = '" & Label4.Text & "'
			'		and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Line = '" & Label14.Text & "' and a.Flag_Pallet is null
			'	)
			'	update a
			'		set a.Flag_Pallet = 'Y', Urut_Pallet = '" & x_no_urut_packing_pallet & "'
			'	from N_EMI_Transaksi_Packing_Box a
			'		inner join cte b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Urut_Oto = b.Urut_Oto
			'	where a.Kode_Perusahaan = '" & KodePerusahaan & "'

			'	SELECT @@ROWCOUNT AS TotalData;
			'"
			'Using Dr = OpenTrans(SQL)
			'	If Dr.Read Then
			'		sisa_box -= Val(HilangkanTanda(Dr("TotalData")))
			'	Else
			'		Dr.Close()
			'		CloseTrans()
			'		CloseConn()
			'		MessageBox.Show("Terjadi Kesalahan Saat Update Urut Pallet Packing Box", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'		Exit Sub
			'	End If
			'End Using

#End Region

			Dim zjmlh_pcs As Integer = 0
			Dim xisi_satuan_besar As Integer = 0
			SQL = "select top(1) Isi_Satuan_Besar from Barang where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang = '" & Label4.Text & "' "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						zjmlh_pcs = Val(Label11.Text) * .Rows(0).Item("isi_satuan_besar")
						xisi_satuan_besar = Dr("Isi_Satuan_Besar")
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show("Barang tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			SQL = "select sum(d.Jumlah - d.Jumlah_Sdh_Packing - d.Jumlah_Waste) as Jmlh_Sisa "
			SQL = SQL & "from N_EMI_Transaksi_Packing a, N_EMI_Transaksi_Packing_Detail d, Barang_SN b, Barang c "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
			SQL = SQL & "and d.SN_Baru = b.Serial_Number and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Transaksi = d.No_Transaksi "
			SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and a.Status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Line = '" & Label14.Text & "' and b.Kode_Barang = '" & Label4.Text & "' "
			SQL = SQL & "and d.Jumlah - d.Jumlah_Sdh_Packing - d.Jumlah_Waste <> 0 and a.Flag_Hold is null and a.Flag_Selesai is null "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If zjmlh_pcs > Dr("Jmlh_Sisa") Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Stock barang yang di packing tidak mencukupi, silahkan melakukan scan barang tambahan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Barang yang mau dipacking tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			For a As Integer = 0 To sisa_box - 1
				'generate kode unik box
				Dim Kode_Berjalan_Box As String = ""
				Dim isUniqueBox As Boolean = False

				Do While isUniqueBox = False
					Kode_Berjalan_Box = "B" & Generate_Random_Kode(15)

					SQL = "SELECT Kode_Unik_Print FROM N_EMI_Transaksi_Packing_Box WHERE Kode_Unik_Print = '" & Kode_Berjalan_Box & "' "
					Using Ds = BindingTrans(SQL)
						With Ds.Tables("MyTable")
							If .Rows.Count = 0 Then
								isUniqueBox = True
							End If
						End With
					End Using
				Loop

				kode_unik = Kode_Berjalan_Box
				SQL = "insert into N_EMI_Transaksi_Packing_Box(Kode_Perusahaan, No_Transaksi, Kode_Unik_Print, Kode_Barang, Line, Jumlah, Tgl_Cetak, Jam_Cetak, Flag_Pallet, Urut_Pallet) "
				SQL = SQL & "values('" & KodePerusahaan & "', '" & Label12.Text.Trim & "', '" & kode_unik & "', '" & Label4.Text & "', '" & Label14.Text & "', "
				SQL = SQL & "'" & xisi_satuan_besar & "','" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', 'Y', '" & x_no_urut_packing_pallet & "') "
				ExecuteTrans(SQL)

				Dim x_no_urut_packing_box As Integer = 0
				SQL = "select IDENT_CURRENT('N_EMI_Transaksi_Packing_Box') as urutan"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						x_no_urut_packing_box = Dr("urutan")
					End If
				End Using

				'cek urutan

				Dim isi_satuan As Integer = 0
				Dim sisa As Integer = 0
				SQL = "select a.No_Transaksi, a.Line, a.Qr_Code + '-' + a.Kode_Unik_Berjalan as Barcode, b.Kode_Barang, c.Nama as Nama_Barang, "
				SQL = SQL & "d.Jumlah, d.Jumlah_Sdh_Packing, a.Qr_Code, a.Kode_Unik_Berjalan, c.isi_satuan_besar, d.SN_Baru, "
				SQL = SQL & "d.Jumlah - d.Jumlah_Sdh_Packing - d.Jumlah_Waste as Jmlh_Sisa, d.Urut_Oto, d.Urut_Results_Detail_Pallet "
				SQL = SQL & "from N_EMI_Transaksi_Packing a, N_EMI_Transaksi_Packing_Detail d, Barang_SN b, Barang c "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
				SQL = SQL & "and d.SN_Baru = b.Serial_Number and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Transaksi = d.No_Transaksi "
				SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and a.Status is null "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Line = '" & Label14.Text & "' and b.Kode_Barang = '" & Label4.Text & "' "
				SQL = SQL & "and d.Jumlah - d.Jumlah_Sdh_Packing - d.Jumlah_Waste <> 0 and a.Flag_Hold is null and a.Flag_Selesai is null "
				SQL = SQL & "order by a.Tanggal+a.Jam, b.Tgl_Produksi, a.No_Transaksi "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							isi_satuan = .Rows(0).Item("isi_satuan_besar")
							sisa = .Rows(0).Item("isi_satuan_besar")
							For i As Integer = 0 To .Rows.Count - 1

								If sisa = 0 Then
									Exit For
								ElseIf sisa < 0 Then
									CloseTrans()
									CloseConn()
									MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If

								If sisa < .Rows(i).Item("Jmlh_Sisa") Or sisa = .Rows(i).Item("Jmlh_Sisa") Then
									SQL = "insert into N_EMI_Transaksi_Packing_Det(Kode_Perusahaan, Urut_Detail_Transaksi_Packing, Urut_Transaksi_Box, Jumlah) "
									SQL = SQL & "values('" & KodePerusahaan & "', '" & .Rows(i).Item("Urut_Oto") & "', '" & x_no_urut_packing_box & "', "
									SQL = SQL & "'" & sisa & "' ) "
									ExecuteTrans(SQL)

									SQL = "update N_EMI_Transaksi_Packing_Detail set Jumlah_Sdh_Packing = Jumlah_Sdh_Packing + '" & sisa & "' where "
									SQL = SQL & "Urut_Oto = '" & .Rows(i).Item("Urut_Oto") & "' "
									ExecuteTrans(SQL)

									SQL = "update Emi_Production_Results_Detail_Pallet set Jumlah_Sdh_Packing = Jumlah_Sdh_Packing + '" & sisa & "' where "
									SQL = SQL & "Urut_Oto = '" & .Rows(i).Item("Urut_Results_Detail_Pallet") & "' "
									ExecuteTrans(SQL)

									sisa = 0
								ElseIf sisa > .Rows(i).Item("Jmlh_Sisa") Then
									SQL = "insert into N_EMI_Transaksi_Packing_Det(Kode_Perusahaan, Urut_Detail_Transaksi_Packing, Urut_Transaksi_Box, Jumlah) "
									SQL = SQL & "values('" & KodePerusahaan & "', '" & .Rows(i).Item("Urut_Oto") & "', '" & x_no_urut_packing_box & "', "
									SQL = SQL & "'" & .Rows(i).Item("Jmlh_Sisa") & "' ) "
									ExecuteTrans(SQL)

									SQL = "update N_EMI_Transaksi_Packing_Detail set Jumlah_Sdh_Packing = Jumlah_Sdh_Packing + '" & .Rows(i).Item("Jmlh_Sisa") & "' where "
									SQL = SQL & "Urut_Oto = '" & .Rows(i).Item("Urut_Oto") & "' "
									ExecuteTrans(SQL)

									SQL = "update Emi_Production_Results_Detail_Pallet set Jumlah_Sdh_Packing = Jumlah_Sdh_Packing + '" & .Rows(i).Item("Jmlh_Sisa") & "' where "
									SQL = SQL & "Urut_Oto = '" & .Rows(i).Item("Urut_Results_Detail_Pallet") & "' "
									ExecuteTrans(SQL)

									sisa = sisa - .Rows(i).Item("Jmlh_Sisa")
								Else
									CloseTrans()
									CloseConn()
									MessageBox.Show("Barang tidak cukup harap scan barang terlebih dahulu !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If

								SQL = "select sum(a.Jumlah) as Jumlah, sum(a.Jumlah_Sdh_Packing) as Jumlah_Sdh_Packing, sum(a.Jumlah_Waste) as Jumlah_Waste "
								SQL = SQL & "from N_EMI_Transaksi_Packing_Detail a, N_EMI_Transaksi_Packing b "
								SQL = SQL & "where a.No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
								SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
								Using dr = OpenTrans(SQL)
									If dr.Read Then
										If dr("Jumlah") = dr("Jumlah_Sdh_Packing") + dr("Jumlah_Waste") Then
											dr.Close()
											SQL = "update N_EMI_Transaksi_Packing set Flag_Selesai = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' "
											SQL = SQL & "and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
											ExecuteTrans(SQL)
										End If
									End If
								End Using

							Next
						Else
							CloseTrans()
							CloseConn()
							MessageBox.Show("Barang tidak ditemukan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End With
				End Using
			Next

			'HAPUS TABEL SEMENTARA
			SQL = "delete from N_EMI_Cetak_Label_Packing_Pallet where Line = '" & Label14.Text & "' "
			ExecuteTrans(SQL)

			Barcode.Image = Nothing
			Barcode.Image = Generate_QR_NoPadding(kode_unik_pallet)
			Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_pallet & ".jpg")
			Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
			'End If

			fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
			FileSize1 = fs1.Length
			rawData1 = New Byte(FileSize1) {}
			fs1.Read(rawData1, 0, FileSize1)
			fs1.Close()
			Cmd.Parameters.Add("@newBarcode" & kode_unik_pallet, SqlDbType.Image).Value = rawData1

			SQL = "WITH Prod AS ( SELECT y.No_Production_Order, y.Tanggal, x.Kode_Perusahaan, x.Urut_Oto, "
			SQL = SQL & "ROW_NUMBER() OVER (PARTITION BY x.Kode_Perusahaan, x.Urut_Oto ORDER BY DATEADD(SECOND, DATEDIFF(SECOND, 0, y.Jam), y.Tanggal) "
			SQL = SQL & ") AS rn "
			SQL = SQL & "FROM Emi_Production_Results y "
			SQL = SQL & "INNER JOIN Emi_Production_Results_Detail_Pallet x "
			SQL = SQL & "ON x.Kode_Perusahaan = y.Kode_Perusahaan AND x.No_Transaksi = y.No_Transaksi "
			SQL = SQL & "WHERE y.Status IS NULL) "

			SQL = SQL & "SELECT top(1)p.No_Production_Order, a.Kode_Unik_Print, b.Kode_Barang, f.Nama AS Nama_Barang, "
			SQL = SQL & "MIN(d.Tgl_Produksi) AS Tgl_Produksi, MIN(d.Tgl_Expired) AS Tgl_Expired, a.Jumlah, b.Line, a.Flag_OK_QA "
			SQL = SQL & "FROM N_EMI_Transaksi_Packing_Pallet a INNER JOIN N_EMI_Transaksi_Packing_Box b "
			SQL = SQL & "ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.Urut_Oto = b.Urut_Pallet "
			SQL = SQL & "INNER JOIN N_EMI_Transaksi_Packing_Det c "
			SQL = SQL & "ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.Urut_Oto = c.Urut_Transaksi_Box "
			SQL = SQL & "INNER JOIN N_EMI_Transaksi_Packing_Detail d "
			SQL = SQL & "ON c.Urut_Detail_Transaksi_Packing = d.Urut_Oto and c.Kode_Perusahaan = d.Kode_Perusahaan "
			SQL = SQL & "INNER JOIN Barang_SN e "
			SQL = SQL & "ON d.SN_Baru = e.Serial_Number and d.Kode_Perusahaan = e.Kode_Perusahaan "
			SQL = SQL & "INNER JOIN Barang f "
			SQL = SQL & "ON e.Kode_Perusahaan = f.Kode_Perusahaan AND e.Kode_Stock_Owner = f.Kode_Stock_Owner "
			SQL = SQL & "AND e.Kode_Barang = f.Kode_Barang "
			SQL = SQL & "INNER JOIN Prod p "
			SQL = SQL & "ON d.Kode_Perusahaan = p.Kode_Perusahaan AND d.Urut_Results_Detail_Pallet = p.Urut_Oto "
			SQL = SQL & "WHERE a.Kode_Perusahaan = '" & KodePerusahaan & "' AND a.Status IS NULL "
			SQL = SQL & "AND a.Kode_Unik_Print = '" & kode_unik_pallet & "' AND b.Status IS NULL "
			SQL = SQL & "AND b.Line = '" & Label14.Text & "' AND c.Status IS NULL AND p.rn = 1 "

			SQL = SQL & "GROUP BY p.No_Production_Order, p.Tanggal, a.Kode_Unik_Print, b.Kode_Barang, f.Nama, a.Jumlah, b.Line, a.Flag_OK_QA "
			SQL = SQL & "order by a.Kode_Unik_Print, p.Tanggal desc"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							SQL = "insert into N_EMI_Cetak_Label_Packing_Pallet (Kode_Perusahaan, Kode_Unik_Print, Barcode, Kode_Barang, Nama_Barang, Tgl_Produksi, Tgl_Expired, Jumlah, Line, No_Production_Order, Flag_OK_QA) "
							SQL = SQL & "values('" & KodePerusahaan & "', '" & .Rows(i).Item("Kode_Unik_Print") & "', @newBarcode" & kode_unik_pallet & ", "
							SQL = SQL & "'" & .Rows(i).Item("Kode_Barang") & "', '" & .Rows(i).Item("Nama_Barang") & "', "
							SQL = SQL & "'" & .Rows(i).Item("Tgl_Produksi") & "', '" & .Rows(i).Item("Tgl_Expired") & "', "
							SQL = SQL & "'" & .Rows(i).Item("Jumlah") & "', '" & Label14.Text & "', '" & .Rows(i).Item("No_Production_Order") & "', '" & .Rows(i).Item("Flag_OK_QA") & "') "
							ExecuteTrans(SQL)
						Next
					End If
				End With
			End Using

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()

			Dim CrDoc As New Object
			Dim KertasKecil As String = "BarcodeQC"

			SQL = "select kode_perusahaan from N_EMI_Cetak_Label_Packing_Pallet where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Unik_Print = '" & kode_unik_pallet & "'"
			Using Ds = BindingTrans(SQL)
				If Ds.Tables("MyTable").Rows.Count <> 0 Then

					'==========================
					'=     BARCODEE BESAR     =
					'==========================
					Dim printerDitemukan As Boolean = False
					For Each printer As String In PrinterSettings.InstalledPrinters
						If printer.ToLower() = PrinterBarcode.ToLower() Then
							printerDitemukan = True
							Exit For
						End If
					Next

					If printerDitemukan Then

						CrDoc = New N_EMI_Label_Barcode_Box_Pallet

						'    With A_Place_For_Printing
						'    CrDoc.SetDataSource(Ds)
						'    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						'    CrDoc.PrintOptions.PrinterName = ""
						'    CrDoc.RecordSelectionFormula = "{N_EMI_Cetak_Label_Packing_Pallet.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Cetak_Label_Packing_Pallet.Kode_Unik_Print} = '" & kode_unik_pallet & "' "
						'    CrDoc.SummaryInfo.ReportTitle = "Label Good Received 2"
						'    .Text = "Label Good Received 2"
						'    .CrystalReportViewer1.ReportSource = CrDoc
						'    .Refresh()
						'    .Show()
						'End With

						'=====================================================

						Dim doctoprint As New System.Drawing.Printing.PrintDocument()
						CrDoc.SetDataSource(Ds)
						CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						CrDoc.RecordSelectionFormula = "{N_EMI_Cetak_Label_Packing_Pallet.Kode_Perusahaan} = '" & KodePerusahaan & "'and {N_EMI_Cetak_Label_Packing_Pallet.Kode_Unik_Print} = '" & kode_unik_pallet & "' "
						CrDoc.PrintOptions.PrinterName = PrinterBarcode

						doctoprint.PrinterSettings.PrinterName = PrinterBarcode

						Dim rawKind As Integer
						Dim foundPaper As Boolean = False
						CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
						For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
							If doctoprint.PrinterSettings.PaperSizes(j).PaperName = KertasKecil Then
								rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
								CrDoc.PrintOptions.PaperSize = rawKind
								foundPaper = True
								Exit For
							End If
						Next

						If Not foundPaper Then
							CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
							MessageBox.Show("Kertas Tidak Ditemukan, Menggunakan Kertas Default", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

						End If

						CrDoc.PrintToPrinter(1, False, 1, 2500)
					Else
						MessageBox.Show("Printer FG Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					End If
					printerDitemukan = False
				Else
					MessageBox.Show("Printer Q Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					CloseConn()
					Exit Sub
				End If

			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Dim ttl_jmlh_pallet As Integer = Val(Label10.Text) + 1
		Label10.Text = Format(ttl_jmlh_pallet, "N0")

		get_data()
		get_log_scan()
		get_data_repacking()
	End Sub

	Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
		If Label14.Text.Trim.Length = 0 Then Exit Sub
		Dim sisa_box As Integer = 0
		Try
			OpenConn()

			SQL = "select No_Transaksi, Line, Shift from N_EMI_Transaksi_Packing_Check_In_Out where Status is null "
			SQL = SQL & "and Kode_Perusahaan = '" & KodePerusahaan & "' and Tanggal_Out is null and Line = '" & ComboBox1.Text & "' "
			Using dr = OpenTrans(SQL)
				If Not dr.Read Then
					dr.Close()
					CloseConn()
					MessageBox.Show("Line belum melakukan check in", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					kosong()
					Exit Sub
				End If
			End Using

			SQL = "select top(1)b.Isi_Satuan_Pallet from Barang b where b.Kode_Barang = '" & Label4.Text & "' and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					ajumlah = Dr("Isi_Satuan_Pallet")
				Else
					Dr.Close()
					CloseConn()
					MessageBox.Show("Barang tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
		N_EMI_SD_Waste_Scan_Packing.Label1.Text = "Pallet"
		N_EMI_SD_Waste_Scan_Packing.asal = "N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru - Sementara"
		N_EMI_SD_Waste_Scan_Packing.Label6.Text = "Masukan Jumlah Pallet"
		N_EMI_SD_Waste_Scan_Packing.Label3.Text = ajumlah
		N_EMI_SD_Waste_Scan_Packing.kosong()
		'N_EMI_SD_Waste_Scan_Packing.Label4.Text = sisa_box
		N_EMI_SD_Waste_Scan_Packing.ShowDialog()
	End Sub

	Public Sub xsimpan_sementara()
		If Label14.Text.Trim.Length = 0 Then Exit Sub

		get_data()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			SQL = "select No_Transaksi, Line, Shift from N_EMI_Transaksi_Packing_Check_In_Out where Status is null "
			SQL = SQL & "and Kode_Perusahaan = '" & KodePerusahaan & "' and Tanggal_Out is null and Line = '" & ComboBox1.Text & "' "
			Using dr = OpenTrans(SQL)
				If Not dr.Read Then
					dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Line belum melakukan check in", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					kosong()
					Exit Sub
				End If
			End Using

			Dim sisa_box As Integer = 0
			SQL = "select isnull(count(a.Urut_Oto),0) as Jumlah from N_EMI_Transaksi_Packing_Box a where a.Status is null and a.Kode_Barang = '" & Label4.Text & "' "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Line = '" & Label14.Text & "' and a.Flag_Pallet is null "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					sisa_box = Val(Label11.Text) - Dr("Jumlah")
				Else
					Dr.Close()
					CloseConn()
					MessageBox.Show("Barang tidak diemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'Dim sisa_box As Integer = ajumlah - sisa_box1
			Dim zjmlh_pcs As Integer = 0
			SQL = "select top(1)Isi_Satuan_Besar from Barang where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang = '" & Label4.Text & "' "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						zjmlh_pcs = sisa_box * .Rows(0).Item("isi_satuan_besar")
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show("Barang tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			SQL = "select sum(d.Jumlah - d.Jumlah_Sdh_Packing - d.Jumlah_Waste) as Jmlh_Sisa "
			SQL = SQL & "from N_EMI_Transaksi_Packing a, N_EMI_Transaksi_Packing_Detail d, Barang_SN b, Barang c "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
			SQL = SQL & "and d.SN_Baru = b.Serial_Number and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Transaksi = d.No_Transaksi "
			SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and a.Status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Line = '" & Label14.Text & "' and b.Kode_Barang = '" & Label4.Text & "' "
			SQL = SQL & "and d.Jumlah - d.Jumlah_Sdh_Packing - d.Jumlah_Waste <> 0 and a.Flag_Hold is null and a.Flag_Selesai is null "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If zjmlh_pcs > Dr("Jmlh_Sisa") Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("stock barang yang di paccking tidak mencukupi , silahkan melakukan scan barang tambahan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Barang yang mau dipacking tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			For a As Integer = 0 To sisa_box - 1

				SQL = "declare @ab int; declare @ac int; select @ab = Selisih_Jam, @ac= expired_proforma from Init; "
				SQL = SQL & " Select FORMAT(DATEADD(hh, @ab, getdate()), 'yyyy-MM-dd HH:mm:ss')  as Tanggal_Sekarang , @ac as expired"
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						tgl_skg = dr("Tanggal_Sekarang")
					End If
				End Using

				'generate kode unik box
				Dim Kode_Berjalan_Box As String = ""
				Dim isUniqueBox As Boolean = False

				Do While isUniqueBox = False
					Kode_Berjalan_Box = "B" & Generate_Random_Kode(15)

					SQL = "SELECT Kode_Unik_Print FROM N_EMI_Transaksi_Packing_Box WHERE Kode_Unik_Print = '" & Kode_Berjalan_Box & "' "
					Using Ds = BindingTrans(SQL)
						With Ds.Tables("MyTable")
							If .Rows.Count = 0 Then
								isUniqueBox = True
							End If
						End With
					End Using
				Loop

				kode_unik = Kode_Berjalan_Box

				SQL = "select top(1)Isi_Satuan_Besar from Barang where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang = '" & Label4.Text & "' "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							SQL = "insert into N_EMI_Transaksi_Packing_Box(Kode_Perusahaan, No_Transaksi, Kode_Unik_Print, Kode_Barang, Line, Jumlah, Tgl_Cetak, Jam_Cetak) "
							SQL = SQL & "values('" & KodePerusahaan & "', '" & Label12.Text.Trim & "', '" & kode_unik & "', '" & Label4.Text & "', '" & Label14.Text & "', "
							SQL = SQL & "'" & .Rows(0).Item("isi_satuan_besar") & "','" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "') "
							ExecuteTrans(SQL)
						Else
							CloseTrans()
							CloseConn()
							MessageBox.Show("Barang tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End With
				End Using

				Dim x_no_urut_packing_box As Integer = 0
				SQL = "select IDENT_CURRENT('N_EMI_Transaksi_Packing_Box') as urutan"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						x_no_urut_packing_box = Dr("urutan")
					End If
				End Using

				Dim isi_satuan As Integer = 0
				Dim sisa As Integer = 0
				SQL = "select a.No_Transaksi, a.Line, a.Qr_Code + '-' + a.Kode_Unik_Berjalan as Barcode, b.Kode_Barang, c.Nama as Nama_Barang, "
				SQL = SQL & "d.Jumlah, d.Jumlah_Sdh_Packing, a.Qr_Code, a.Kode_Unik_Berjalan, c.isi_satuan_besar, d.SN_Baru, "
				SQL = SQL & "d.Jumlah - d.Jumlah_Sdh_Packing - d.Jumlah_Waste as Jmlh_Sisa, d.Urut_Oto, d.Urut_Results_Detail_Pallet "
				SQL = SQL & "from N_EMI_Transaksi_Packing a, N_EMI_Transaksi_Packing_Detail d, Barang_SN b, Barang c "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
				SQL = SQL & "and d.SN_Baru = b.Serial_Number and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Transaksi = d.No_Transaksi "
				SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and a.Status is null "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Line = '" & Label14.Text & "' and b.Kode_Barang = '" & Label4.Text & "' "
				SQL = SQL & "and d.Jumlah - d.Jumlah_Sdh_Packing - d.Jumlah_Waste <> 0 and a.Flag_Hold is null and a.Flag_Selesai is null "
				SQL = SQL & "order by a.Tanggal+a.Jam, b.Tgl_Produksi, a.No_Transaksi "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							isi_satuan = .Rows(0).Item("isi_satuan_besar")
							sisa = .Rows(0).Item("isi_satuan_besar")
							For i As Integer = 0 To .Rows.Count - 1

								If sisa = 0 Then
									Exit For
								ElseIf sisa < 0 Then
									CloseTrans()
									CloseConn()
									MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If

								If sisa < .Rows(i).Item("Jmlh_Sisa") Or sisa = .Rows(i).Item("Jmlh_Sisa") Then
									SQL = "insert into N_EMI_Transaksi_Packing_Det(Kode_Perusahaan, Urut_Detail_Transaksi_Packing, Urut_Transaksi_Box, Jumlah) "
									SQL = SQL & "values('" & KodePerusahaan & "', '" & .Rows(i).Item("Urut_Oto") & "', '" & x_no_urut_packing_box & "', "
									SQL = SQL & "'" & sisa & "' ) "
									ExecuteTrans(SQL)

									SQL = "update N_EMI_Transaksi_Packing_Detail set Jumlah_Sdh_Packing = Jumlah_Sdh_Packing + '" & sisa & "' where "
									SQL = SQL & "Urut_Oto = '" & .Rows(i).Item("Urut_Oto") & "' "
									ExecuteTrans(SQL)

									SQL = "update Emi_Production_Results_Detail_Pallet set Jumlah_Sdh_Packing = Jumlah_Sdh_Packing + '" & sisa & "' where "
									SQL = SQL & "Urut_Oto = '" & .Rows(i).Item("Urut_Results_Detail_Pallet") & "' "
									ExecuteTrans(SQL)

									sisa = 0
								ElseIf sisa > .Rows(i).Item("Jmlh_Sisa") Then
									SQL = "insert into N_EMI_Transaksi_Packing_Det(Kode_Perusahaan, Urut_Detail_Transaksi_Packing, Urut_Transaksi_Box, Jumlah) "
									SQL = SQL & "values('" & KodePerusahaan & "', '" & .Rows(i).Item("Urut_Oto") & "', '" & x_no_urut_packing_box & "', "
									SQL = SQL & "'" & .Rows(i).Item("Jmlh_Sisa") & "' ) "
									ExecuteTrans(SQL)

									SQL = "update N_EMI_Transaksi_Packing_Detail set Jumlah_Sdh_Packing = Jumlah_Sdh_Packing + '" & .Rows(i).Item("Jmlh_Sisa") & "' where "
									SQL = SQL & "Urut_Oto = '" & .Rows(i).Item("Urut_Oto") & "' "
									ExecuteTrans(SQL)

									SQL = "update Emi_Production_Results_Detail_Pallet set Jumlah_Sdh_Packing = Jumlah_Sdh_Packing + '" & .Rows(i).Item("Jmlh_Sisa") & "' where "
									SQL = SQL & "Urut_Oto = '" & .Rows(i).Item("Urut_Results_Detail_Pallet") & "' "
									ExecuteTrans(SQL)

									sisa = sisa - .Rows(i).Item("Jmlh_Sisa")
								Else
									CloseTrans()
									CloseConn()
									MessageBox.Show("Barang tidak cukup harap scan barang terlebih dahulu !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If

								SQL = "select sum(a.Jumlah) as Jumlah, sum(a.Jumlah_Sdh_Packing) as Jumlah_Sdh_Packing, sum(a.Jumlah_Waste) as Jumlah_Waste "
								SQL = SQL & "from N_EMI_Transaksi_Packing_Detail a, N_EMI_Transaksi_Packing b "
								SQL = SQL & "where a.No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
								SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
								Using dr = OpenTrans(SQL)
									If dr.Read Then
										If dr("Jumlah") = dr("Jumlah_Sdh_Packing") + dr("Jumlah_Waste") Then
											dr.Close()
											SQL = "update N_EMI_Transaksi_Packing set Flag_Selesai = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' "
											SQL = SQL & "and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
											ExecuteTrans(SQL)
										End If
									End If
								End Using

							Next
						Else
							CloseTrans()
							CloseConn()
							MessageBox.Show("Barang tidak ditemukan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End With
				End Using
			Next

			Cmd.Transaction.Commit()
			MessageBox.Show("Berhasil Disimpan Sementara", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		get_data()
		get_log_scan()
		get_data_repacking()
	End Sub

	Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
		get_data()
		get_log_scan()
		get_data_repacking()
	End Sub

End Class