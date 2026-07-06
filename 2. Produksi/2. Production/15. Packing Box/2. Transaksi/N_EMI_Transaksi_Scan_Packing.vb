Public Class N_EMI_Transaksi_Scan_Packing
	Dim Lvno As String
	Dim Lvsplit As String
	Dim Lvbatch As String
	Dim Lvnama As String
	Dim Lvkode As String
	Dim Lvpcs As String
	Dim Lvsdh_packing As String
	Dim Lvjmlh_waste As String
	Dim Lvwaktu As String
	Dim Lvbarcode As String
	Dim Lvqr As String
	Dim Lvkode_unik As String
	Dim Lvtrans As String
	Dim Lvaksi As String
	Dim Lvwaste As String

	Dim Cellno As Integer = 0
	Dim Cellsplit As Integer = 1
	Dim Cellbatch As Integer = 2
	Dim Cellnama As Integer = 3
	Dim Cellkode As Integer = 4
	Dim Cellbarcode As Integer = 5
	Dim Cellpcs As Integer = 6
	Dim Cellsdh_packing As Integer = 7
	Dim Celljmlh_waste As Integer = 8
	Dim Cellwaktu As Integer = 9
	Dim Cellqr As Integer = 10
	Dim Cellkode_unik As Integer = 11
	Dim Celltrans As Integer = 12
	Dim Cellaksi As Integer = 13
	Dim Cellwaste As Integer = 14

	Private Sub get_grid_view(ByVal index As Integer)
		Lvno = DataGridView1.Rows(index).Cells(Cellno).Value
		Lvsplit = DataGridView1.Rows(index).Cells(Cellsplit).Value
		Lvbatch = DataGridView1.Rows(index).Cells(Cellbatch).Value
		Lvnama = DataGridView1.Rows(index).Cells(Cellnama).Value
		Lvkode = DataGridView1.Rows(index).Cells(Cellkode).Value
		Lvbarcode = DataGridView1.Rows(index).Cells(Cellbarcode).Value
		Lvpcs = DataGridView1.Rows(index).Cells(Cellpcs).Value
		Lvsdh_packing = DataGridView1.Rows(index).Cells(Cellsdh_packing).Value
		Lvjmlh_waste = DataGridView1.Rows(index).Cells(Celljmlh_waste).Value
		Lvwaktu = DataGridView1.Rows(index).Cells(Cellwaktu).Value
		Lvqr = DataGridView1.Rows(index).Cells(Cellqr).Value
		Lvkode_unik = DataGridView1.Rows(index).Cells(Cellkode_unik).Value
		Lvtrans = DataGridView1.Rows(index).Cells(Celltrans).Value
	End Sub

	Private Sub N_EMI_Transaksi_Scan_Packing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		Txt_QR.Text = "Scan QR Code"
		Txt_QR.ForeColor = Color.Gray

		kosong()
	End Sub

	Public Sub kosong()
		DataGridView1.Rows.Clear()
		Label5.Text = "0"
		Label7.Text = "0"

		Label2.Text = ""
		Label12.Text = ""
		Label13.Text = ""
		Label14.Text = ""
		Button4.Text = "Shift In"

		'ComboBox1.Items.Clear()
		'ComboBox1.Items.Add("Line 1")
		'ComboBox1.Items.Add("Line 2")
		'ComboBox1.Items.Add("Line 3")
		'ComboBox1.SelectedIndex = -1

		'ComboBox1.Enabled = True
		'ComboBox1.Focus()
	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		If Label14.Text.Trim.Length = 0 Then
			MessageBox.Show("line belum dipilih ...", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		N_EMI_SD_Pilih_Scan_Packing.kosong()
		If DataGridView1.RowCount <> 0 Then
			N_EMI_SD_Pilih_Scan_Packing.filtertambah = "and d.Kode_Barang = '" & DataGridView1.Rows(0).Cells(4).Value & "' "
		Else
			N_EMI_SD_Pilih_Scan_Packing.filtertambah = " "
		End If
		N_EMI_SD_Pilih_Scan_Packing.Cari("Y")
		N_EMI_SD_Pilih_Scan_Packing.ShowDialog()
	End Sub

	Private Sub N_EMI_Transaksi_Scan_Packing_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub Btn_Scan_Click(sender As Object, e As EventArgs) Handles Btn_Scan.Click
		If Label14.Text.Trim.Length = 0 And Txt_QR.Text <> "Scan QR Code" Then
			MessageBox.Show("line belum dipilih ...", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf Txt_QR.Text.Trim.Length = 0 Then
			'Txt_QR.Focus()
			Exit Sub
		End If

		xscan(Txt_QR.Text)
	End Sub

	Public Sub xscan(ByVal xbarcode As String)
		get_jam()
		If Txt_QR.Text = "Scan QR Code" Then
			Exit Sub
		ElseIf Label14.Text.Trim.Length = 0 Then
			Exit Sub
		End If

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			SQL = "select a.No_Transaksi, a.Tanggal, a.Jam, a.Qr_Code + '-' + a.Kode_Unik_Berjalan as Barcode "
			SQL = SQL & "from N_EMI_Transaksi_Packing a "
			SQL = SQL & "where a.status is null and a.Flag_Selesai is null and a.Flag_Hold is null "
			SQL = SQL & "and Qr_Code + '-' + Kode_Unik_Berjalan = '" & xbarcode & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Barcode sudah pernah discan.....", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Txt_QR.Text = ""
					Txt_QR.Focus()
					Exit Sub
				End If
			End Using

			Dim xNo_Transaksi As String = ""
			Dim FPro_Results As String = "SC"
			xNo_Transaksi = FPro_Results & "-" & Format(tgl_skg, "MM/yy") & "-" &
														  General_Class.Get_Last_Number2("N_EMI_Transaksi_Packing", "No_Transaksi", 5,
														  "Kode_perusahaan", KodePerusahaan,
														  "And", "substring(No_Transaksi,1," & Len(FPro_Results) + 6 & ")", FPro_Results & "-" & Format(tgl_skg, "MM/yy"))

			SQL = "with cte as( "
			SQL = SQL & "select a.No_Production_Order, d.Nama as Nama_Barang, d.Kode_Barang, "
			SQL = SQL & "c.Jumlah as Jumlah_Pcs, b.Satuan, b.Qr_Code + '-' + b.Kode_Unik_Berjalan as Barcode, b.Qr_Code, "
			SQL = SQL & "b.Kode_Unik_Berjalan, e.Proses, isnull(b.Jumlah_Waste, 0) as Jumlah_Waste, isnull(b.Jumlah_Sdh_Packing, 0) as Jumlah_Sdh_Packing, "
			SQL = SQL & "isnull(b.Jumlah_Input_GR_2_Pallet, 0) as Jumlah_Input_GR_2_Pallet, isnull(b.Jumlah_Input_GR_2_Waste, 0) as Jumlah_Input_GR_2_Waste "
			SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b, Barang_SN c, Barang d, Emi_Production_Results_HPP e "
			SQL = SQL & "where a.Status is null and a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
			SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.SN_Baru = c.Serial_Number and b.Flag_Scan is null  "
			SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
			SQL = SQL & "and b.Kode_Perusahaan = e.Kode_Perusahaan and b.Urut_HPP = e.Urut and b.No_Transaksi = e.No_Transaksi "
			SQL = SQL & "and c.Jumlah - (isnull(b.Jumlah_Waste, 0) - isnull(b.Jumlah_Input_GR_2_Waste, 0)) - (isnull(b.Jumlah_Sdh_Packing, 0) - isnull(b.Jumlah_Input_GR_2_Pallet, 0) ) > 0 "
			SQL = SQL & "and (b.Qr_Code + '-' + b.Kode_Unik_Berjalan) like '%" & xbarcode & "%' "
			SQL = SQL & ") "
			SQL = SQL & "select No_Production_Order, Nama_Barang, Kode_Barang, sum(Jumlah_Pcs) as Jumlah_Pcs, "
			SQL = SQL & "sum(Jumlah_Waste) as Jumlah_Waste, sum(Jumlah_Sdh_Packing) as Jumlah_Sdh_Packing, sum(Jumlah_Input_GR_2_Pallet) as Jumlah_Input_GR_2_Pallet, "
			SQL = SQL & "Satuan, Barcode, Qr_Code, Kode_Unik_Berjalan, STRING_AGG(Proses, ', ') as proses, sum(Jumlah_Input_GR_2_Waste) as Jumlah_Input_GR_2_Waste "
			SQL = SQL & "from cte group by No_Production_Order, Nama_Barang, Kode_Barang, Satuan, Barcode, Qr_Code, Kode_Unik_Berjalan "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							If DataGridView1.Rows.Count <> 0 Then
								For z As Integer = 0 To DataGridView1.Rows.Count - 1
									get_grid_view(z)
									If .Rows(i).Item("Kode_Barang") <> Lvkode Then
										CloseTrans()
										CloseConn()
										MessageBox.Show("kode Barang yang discan berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									ElseIf .Rows(i).Item("Barcode") = Lvbarcode Then
										CloseTrans()
										CloseConn()
										MessageBox.Show("kode Barang sudah discan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If
								Next
							End If

							Dim xjum As Integer = .Rows(i).Item("Jumlah_Pcs") - (.Rows(i).Item("Jumlah_Sdh_Packing") - .Rows(i).Item("Jumlah_Input_GR_2_Pallet")) - (.Rows(i).Item("Jumlah_Waste") - .Rows(i).Item("Jumlah_Input_GR_2_Waste"))
							SQL = "insert into N_EMI_Transaksi_Packing(Kode_Perusahaan, No_Transaksi, Line, Tanggal, Jam, Qr_Code, Kode_Unik_Berjalan, Jumlah, Satuan) "
							SQL = SQL & "values('" & KodePerusahaan & "', '" & xNo_Transaksi & "', '" & Label14.Text & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
							SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & .Rows(i).Item("Qr_Code") & "', '" & .Rows(i).Item("Kode_Unik_Berjalan") & "', "
							SQL = SQL & "'" & xjum & "', '" & .Rows(i).Item("Satuan") & "') "
							ExecuteTrans(SQL)

						Next
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show("Barcode tidak ditemukan.....", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_QR.Text = ""
						Txt_QR.Focus()
						Exit Sub
					End If
				End With
			End Using

			SQL = "select a.No_Production_Order, d.Nama as Nama_Barang, d.Kode_Barang, b.Urut_Oto, b.SN_Baru, c.Tgl_Produksi, c.Tgl_Expired, "
			SQL = SQL & "c.Jumlah as Jumlah_Pcs, isnull(b.Jumlah_Waste, 0) as Jumlah_Waste, isnull(b.Jumlah_Sdh_Packing, 0) as Jumlah_Sdh_Packing, "
			SQL = SQL & "isnull(b.Jumlah_Input_GR_2_Pallet, 0) as Jumlah_Input_GR_2_Pallet, isnull(b.Jumlah_Input_GR_2_Waste, 0) as Jumlah_Input_GR_2_Waste, "
			SQL = SQL & "b.Satuan, b.Qr_Code + '-' + b.Kode_Unik_Berjalan as Barcode, b.Qr_Code, b.Kode_Unik_Berjalan, e.Proses "
			SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b, Barang_SN c, Barang d, Emi_Production_Results_HPP e "
			SQL = SQL & "where a.Status is null and a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
			SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.SN_Baru = c.Serial_Number and c.Jumlah - b.Jumlah_Waste - b.Jumlah_Sdh_Packing > 0 "
			SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
			SQL = SQL & "and b.Kode_Perusahaan = e.Kode_Perusahaan and b.Urut_HPP = e.Urut and b.No_Transaksi = e.No_Transaksi "
			SQL = SQL & "and (b.Qr_Code + '-' + b.Kode_Unik_Berjalan) like '%" & xbarcode & "%' and b.Flag_Scan is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							Dim xjum1 As Integer = .Rows(i).Item("Jumlah_Pcs") - (.Rows(i).Item("Jumlah_Sdh_Packing") - .Rows(i).Item("Jumlah_Input_GR_2_Pallet")) - (.Rows(i).Item("Jumlah_Waste") - .Rows(i).Item("Jumlah_Input_GR_2_Waste"))

							SQL = "insert into N_EMI_Transaksi_Packing_Detail(Kode_Perusahaan, No_Transaksi, SN_Baru, Urut_Results_Detail_Pallet, Jumlah, Jumlah_Sdh_Packing, Tgl_Produksi, Tgl_Expired, Jumlah_Waste) "
							SQL = SQL & "values('" & KodePerusahaan & "', '" & xNo_Transaksi & "','" & .Rows(i).Item("SN_Baru") & "',"
							SQL = SQL & "'" & .Rows(i).Item("Urut_Oto") & "', '" & xjum1 & "', '0', '" & .Rows(i).Item("Tgl_Produksi") & "', '" & .Rows(i).Item("Tgl_Expired") & "','0') "
							ExecuteTrans(SQL)

							SQL = "update Emi_Production_Results_Detail_Pallet set Flag_Scan = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "and Urut_Oto = '" & .Rows(i).Item("Urut_Oto") & "' "
							ExecuteTrans(SQL)
						Next
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show("Barcode sn tidak ditemukan.....", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_QR.Text = ""
						Txt_QR.Focus()
						Exit Sub
					End If
				End With
			End Using

			Cmd.Transaction.Commit()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'ComboBox1.Enabled = False
		get_data()
		Txt_QR.Text = ""
		Txt_QR.Focus()
	End Sub

	Private Sub N_EMI_Transaksi_Scan_Packing_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
		Button3.Anchor = AnchorStyles.Top Or AnchorStyles.Right

		DataGridView1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
		'DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
		Dim totalLebar As Integer = DataGridView1.Width - 20
		DataGridView1.Columns(Cellno).Width = CInt(totalLebar * 0.03)
		DataGridView1.Columns(Cellsplit).Width = CInt(totalLebar * 0.09)
		DataGridView1.Columns(Cellbatch).Width = CInt(totalLebar * 0.09)
		DataGridView1.Columns(Cellnama).Width = CInt(totalLebar * 0.31)
		DataGridView1.Columns(Cellbarcode).Width = CInt(totalLebar * 0.2)
		DataGridView1.Columns(Cellpcs).Width = CInt(totalLebar * 0.04)
		DataGridView1.Columns(Cellsdh_packing).Width = CInt(totalLebar * 0.04)
		DataGridView1.Columns(Celljmlh_waste).Width = CInt(totalLebar * 0.04)
		DataGridView1.Columns(Cellwaktu).Width = CInt(totalLebar * 0.06)
		DataGridView1.Columns(Cellaksi).Width = CInt(totalLebar * 0.04)
		DataGridView1.Columns(Cellwaste).Width = CInt(totalLebar * 0.04)
	End Sub

	Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
		If e.RowIndex >= 0 Then
			get_jam()
			If e.ColumnIndex = Cellaksi Then

				N_EMI_SD_Waste_Scan_Packing_Pin.Label3.Text = DataGridView1.Rows(e.RowIndex).Cells(Celltrans).Value
				N_EMI_SD_Waste_Scan_Packing_Pin.Label5.Text = DataGridView1.Rows(e.RowIndex).Cells(Cellbarcode).Value
				N_EMI_SD_Waste_Scan_Packing_Pin.asal = "N_EMI_Transaksi_Scan_Packing - Hapus"
				N_EMI_SD_Waste_Scan_Packing_Pin.Label4.Text = e.RowIndex
				N_EMI_SD_Waste_Scan_Packing_Pin.TextBox2.Focus()
				N_EMI_SD_Waste_Scan_Packing_Pin.ShowDialog()

			ElseIf e.ColumnIndex = Cellwaste Then

				N_EMI_SD_Waste_Scan_Packing.Label1.Text = "Waste"
				N_EMI_SD_Waste_Scan_Packing.Label6.Text = DataGridView1.Rows(e.RowIndex).Cells(Cellbarcode).Value
				N_EMI_SD_Waste_Scan_Packing.Label3.Text = DataGridView1.Rows(e.RowIndex).Cells(Celltrans).Value
				N_EMI_SD_Waste_Scan_Packing.Label4.Text = HilangkanTanda(DataGridView1.Rows(e.RowIndex).Cells(Cellpcs).Value) - HilangkanTanda(DataGridView1.Rows(e.RowIndex).Cells(Cellsdh_packing).Value) - HilangkanTanda(DataGridView1.Rows(e.RowIndex).Cells(Celljmlh_waste).Value)
				N_EMI_SD_Waste_Scan_Packing.kosong()
				N_EMI_SD_Waste_Scan_Packing.asal = "N_EMI_Transaksi_Scan_Packing - Waste"
				N_EMI_SD_Waste_Scan_Packing.ShowDialog()
			End If
		End If
	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
		If Label12.Text.Trim.Length <> 0 Then
			get_jam()
			Try
				OpenConn()
				Cmd.Transaction = Cn.BeginTransaction

				SQL = "update N_EMI_Transaksi_Packing_Check_In_Out set "
				SQL = SQL & "Tanggal_Out = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
				SQL = SQL & "Jam_Out = '" & Format(tgl_skg, "HH:mm:ss") & "' "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & Label12.Text.Trim & "' "
				ExecuteTrans(SQL)

				Cmd.Transaction.Commit()
				CloseTrans()
				CloseConn()
			Catch ex As Exception
				CloseTrans()
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try
		End If
		Me.Close()
	End Sub

	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		If Button4.Text = "Shift In" Then
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

	Public Sub get_data()
		If Label14.Text.Trim.Length = 0 Then Exit Sub

		Try
			OpenConn()

			Dim a As Integer = 0
			Dim xpcs As Integer = 0
			DataGridView1.Rows.Clear()
			SQL = "with cte as( "
			SQL = SQL & "select d.No_Production_Order, e.Proses, g.Nama as Nama_Barang, g.Kode_Barang, "
			SQL = SQL & "a.Qr_Code + '-' + a.Kode_Unik_Berjalan as Barcode, b.Jumlah as Jumlah_Pcs, b.Tgl_Produksi, "
			SQL = SQL & "b.Jumlah_Waste, b.Jumlah_Sdh_Packing, a.Tanggal, a.Jam, a.Qr_Code, a.Kode_Unik_Berjalan, a.No_Transaksi "
			SQL = SQL & "from N_EMI_Transaksi_Packing a, N_EMI_Transaksi_Packing_Detail b, "
			SQL = SQL & "Emi_Production_Results_Detail_Pallet c, Emi_Production_Results d, "
			SQL = SQL & "Emi_Production_Results_HPP e, Barang_SN f, Barang g "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
			SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Urut_Results_Detail_Pallet = c.Urut_Oto "
			SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Transaksi = d.No_Transaksi and d.Status is null "
			SQL = SQL & "and c.Kode_Perusahaan = e.Kode_Perusahaan and c.Urut_HPP = e.Urut and c.No_Transaksi = e.No_Transaksi "
			SQL = SQL & "and b.SN_Baru = f.Serial_Number and b.Kode_Perusahaan = f.Kode_Perusahaan and f.Kode_Perusahaan = g.Kode_Perusahaan "
			SQL = SQL & "and f.Kode_Stock_Owner = g.Kode_Stock_Owner and f.Kode_Barang = g.Kode_Barang "
			SQL = SQL & "and a.Status is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Line = '" & Label14.Text & "' "
			SQL = SQL & "and b.Jumlah - b.Jumlah_Waste - b.Jumlah_Sdh_Packing > 0 and a.Flag_hold is null and a.Flag_Selesai is null ) "
			SQL = SQL & "select top(50)No_Production_Order, STRING_AGG(Proses, ', ') as Proses, Nama_Barang, Kode_Barang, Barcode, "
			SQL = SQL & "sum(Jumlah_Pcs) as Jumlah_Pcs, sum(Jumlah_Waste) as Jumlah_Waste, sum(Jumlah_Sdh_Packing) as Jumlah_Sdh_Packing, "
			SQL = SQL & "Tanggal, Jam, Qr_Code, Kode_Unik_Berjalan, No_Transaksi, Tgl_Produksi "
			SQL = SQL & "from cte group by No_Production_Order, Nama_Barang, Kode_Barang, Barcode, "
			SQL = SQL & "Tanggal, Jam, Qr_Code, Kode_Unik_Berjalan, No_Transaksi, Tgl_Produksi "
			SQL = SQL & "order by Tanggal + Jam desc, Tgl_Produksi desc, No_Transaksi desc "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							Dim sisa As Integer = .Rows(i).Item("Jumlah_Pcs") - .Rows(i).Item("Jumlah_Sdh_Packing") - .Rows(i).Item("Jumlah_Waste")

							xpcs = xpcs + .Rows(i).Item("Jumlah_Pcs")
							a = a + 1
							DataGridView1.Rows.Add(1)
							DataGridView1.Rows(i).Cells(Cellno).Value = a
							DataGridView1.Rows(i).Cells(Cellsplit).Value = .Rows(i).Item("No_Production_Order")
							DataGridView1.Rows(i).Cells(Cellbatch).Value = .Rows(i).Item("Proses")
							DataGridView1.Rows(i).Cells(Cellnama).Value = .Rows(i).Item("Nama_Barang")
							DataGridView1.Rows(i).Cells(Cellkode).Value = .Rows(i).Item("Kode_Barang")
							DataGridView1.Rows(i).Cells(Cellbarcode).Value = .Rows(i).Item("Barcode")
							DataGridView1.Rows(i).Cells(Cellpcs).Value = Format(.Rows(i).Item("Jumlah_Pcs"), "N0")
							DataGridView1.Rows(i).Cells(Cellsdh_packing).Value = Format(.Rows(i).Item("Jumlah_Sdh_Packing"), "N0")
							DataGridView1.Rows(i).Cells(Celljmlh_waste).Value = Format(.Rows(i).Item("Jumlah_Waste"), "N0")
							DataGridView1.Rows(i).Cells(Cellwaktu).Value = Format(.Rows(i).Item("Tanggal"), "dd-MMM-yyyy") & " " & .Rows(i).Item("Jam")
							DataGridView1.Rows(i).Cells(Cellqr).Value = .Rows(i).Item("Qr_Code")
							DataGridView1.Rows(i).Cells(Cellkode_unik).Value = .Rows(i).Item("Kode_Unik_Berjalan")
							DataGridView1.Rows(i).Cells(Celltrans).Value = .Rows(i).Item("No_Transaksi")

						Next
						'Else
						'    CloseTrans()
						'    CloseConn()
						'    MessageBox.Show("Barcode sn tidak ditemukan.....", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'    Exit Sub
					End If
				End With
			End Using

			'Dim a As Integer = DataGridView1.Rows.Count + 1

			Dim xbox As Integer = DataGridView1.Rows.Count

			Label5.Text = Format(xbox, "N0")
			Label7.Text = Format(xpcs, "N0")

			'' Hitung jumlah baris
			'Dim rows As Integer = DataGridView1.Rows.Count

			'' Tambah baris di posisi 0 (paling atas)
			'DataGridView1.Rows.Insert(0, 1)

			'' Isi data ke baris paling atas
			'DataGridView1.Rows(0).Cells(Cellno).Value = a
			'DataGridView1.Rows(0).Cells(Cellsplit).Value = .Rows(i).Item("No_Production_Order")
			'DataGridView1.Rows(0).Cells(Cellbatch).Value = .Rows(i).Item("Proses")
			'DataGridView1.Rows(0).Cells(Cellnama).Value = .Rows(i).Item("Nama_Barang")
			'DataGridView1.Rows(0).Cells(Cellkode).Value = .Rows(i).Item("Kode_Barang")
			'DataGridView1.Rows(0).Cells(Cellbarcode).Value = .Rows(i).Item("Barcode")
			'DataGridView1.Rows(0).Cells(Cellpcs).Value = Format(xjum, "N0")
			'DataGridView1.Rows(0).Cells(Cellwaktu).Value = Format(tgl_skg, "dd-MMM-yyyy HH:mm:ss")
			'DataGridView1.Rows(0).Cells(Cellqr).Value = .Rows(i).Item("Qr_Code")
			'DataGridView1.Rows(0).Cells(Cellkode_unik).Value = .Rows(i).Item("Kode_Unik_Berjalan")
			'DataGridView1.Rows(0).Cells(Celltrans).Value = xNo_Transaksi

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		get_data()
	End Sub

	Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
		N_Emi_Transaksi_Repacking.Label12.Text = Label12.Text
		N_Emi_Transaksi_Repacking.Label13.Text = Label13.Text
		N_Emi_Transaksi_Repacking.Label14.Text = Label14.Text
		N_Emi_Transaksi_Repacking.Label2.Text = Label2.Text
		If DataGridView1.RowCount <> 0 Then
			N_Emi_Transaksi_Repacking.filtertambah = "and d.Kode_Barang = '" & DataGridView1.Rows(0).Cells(4).Value & "' "
			N_Emi_Transaksi_Repacking.Label3.Text = DataGridView1.Rows(0).Cells(4).Value
		Else
			N_Emi_Transaksi_Repacking.filtertambah = ""
			N_Emi_Transaksi_Repacking.Label3.Text = ""
		End If
		N_Emi_Transaksi_Repacking.ShowDialog()
	End Sub

	Private Sub Txt_QR_TextChanged(sender As Object, e As EventArgs) Handles Txt_QR.TextChanged
		' Abaikan jika placeholder
		If Txt_QR.Text = "Scan QR Code" OrElse Txt_QR.Text.Trim = "" Then
			Exit Sub
		End If

		' Jalankan scan
		'xscan(Txt_QR.Text)
	End Sub

	Private Sub Txt_QR_Enter(sender As Object, e As EventArgs) Handles Txt_QR.Enter
		If Txt_QR.Text = "Scan QR Code" Then
			Txt_QR.Text = ""
			Txt_QR.ForeColor = Color.Black
		End If
	End Sub

	Private Sub Txt_QR_Leave(sender As Object, e As EventArgs) Handles Txt_QR.Leave
		' Jangan ubah kembali ke "Scan QR Code" kalau sudah ada hasil scan
		If Txt_QR.Text = "" Then
			Txt_QR.Text = "Scan QR Code"
			Txt_QR.ForeColor = Color.Gray
			'Else
			'    Button1.Focus()
		End If
	End Sub

	Public Sub Txt_QR_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_QR.KeyDown
		If e.KeyCode = Keys.Enter Then
			' Abaikan placeholder
			If Txt_QR.Text.Trim <> "" AndAlso Txt_QR.Text <> "Scan QR Code" Then
				xscan(Txt_QR.Text)
			End If
		End If
	End Sub

	Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
		get_data()
	End Sub

End Class