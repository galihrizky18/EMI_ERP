Public Class N_Emi_Transaksi_Repacking
	Public filtertambah As String
	Dim Lvbarcode As String
	Dim LvKode As String
	Dim Lvnama As String
	Dim Lvjmlh As String
	Dim LvJumlahwaste As String
	Dim LvurutBox As String
	Dim Lvurutpallet As String
	Dim Lvwaste As String

	Dim Cellbarcode As Integer = 0
	Dim CellKode As Integer = 1
	Dim Cellnama As Integer = 2
	Dim Celljmlh As Integer = 3
	Dim CellJumlahwaste As Integer = 4
	Dim CellurutBox As Integer = 5
	Dim Cellurutpallet As Integer = 6
	Dim Cellwaste As Integer = 7

	Private Sub get_grid_view(ByVal index As Integer)

		Lvbarcode = DataGridView1.Rows(index).Cells(Cellbarcode).Value
		LvKode = DataGridView1.Rows(index).Cells(CellKode).Value
		Lvnama = DataGridView1.Rows(index).Cells(Cellnama).Value
		Lvjmlh = DataGridView1.Rows(index).Cells(Celljmlh).Value
		LvJumlahwaste = DataGridView1.Rows(index).Cells(CellJumlahwaste).Value
		LvurutBox = DataGridView1.Rows(index).Cells(CellurutBox).Value
		Lvurutpallet = DataGridView1.Rows(index).Cells(Cellurutpallet).Value
		Lvwaste = DataGridView1.Rows(index).Cells(Cellwaste).Value

	End Sub

	Private Sub N_Emi_Transaksi_Repacking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		kosong()

	End Sub

	Public Sub kosong()
		DataGridView1.Rows.Clear()
		Txt_QR.Text = "Scan QR Code"
		Txt_QR.ForeColor = Color.Gray

	End Sub

	Private Sub N_Emi_Transaksi_Repacking_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
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

			If DataGridView1.Rows.Count <> 0 Then
				For z As Integer = 0 To DataGridView1.Rows.Count - 1
					get_grid_view(z)
					If Txt_QR.Text = Lvbarcode Then
						CloseConn()
						MessageBox.Show("barcode sudah ada dilist.....", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_QR.Text = ""
						Txt_QR.Focus()
						Exit Sub
					End If
				Next
			End If

			SQL = "select b.Kode_Unik_Print "
			SQL = SQL & "from N_EMI_Transaksi_Packing_Repacking a, N_EMI_Transaksi_Packing_Box b "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Urut_Box_Lama = b.Urut_Oto "
			SQL = SQL & "and b.Kode_Unik_Print = '" & xbarcode & "' and a.Status is null "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("box sudah pernah di repacking !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Txt_QR.Text = ""
					Txt_QR.Focus()
					Exit Sub
				End If
			End Using

			Dim a As Integer = DataGridView1.Rows.Count
			SQL = "select a.Kode_Unik_Print as Barcode_Box, a.Urut_Oto as Urut_Box, a.Kode_Barang, e.Nama as Nama_Barang, sum(b.Jumlah) as Jumlah, "
			SQL = SQL & "ISNULL((select z.Kode_Unik_Print from N_EMI_Transaksi_Packing_Pallet z where a.Kode_Perusahaan = z.Kode_Perusahaan "
			SQL = SQL & "and a.Urut_Pallet = z.Urut_Oto),'-') Barcode_Pallet, ISNULL(a.Urut_Pallet, '0') AS Urut_Pallet "
			SQL = SQL & "from N_EMI_Transaksi_Packing_Box a, N_EMI_Transaksi_Packing_Det b, N_EMI_Transaksi_Packing_Detail c, Barang_SN d, Barang e "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Urut_Oto = b.Urut_Transaksi_Box "
			SQL = SQL & "and a.Status is null and b.Status is null "
			SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Urut_Detail_Transaksi_Packing = c.Urut_Oto "
			SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.SN_Baru = d.Serial_Number and d.Kode_Perusahaan = e.Kode_Perusahaan "
			SQL = SQL & "and d.Kode_Stock_Owner = e.Kode_Stock_Owner and d.Kode_Barang = e.Kode_Barang "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Unik_Print = '" & xbarcode & "' "
			SQL = SQL & "group by a.Kode_Perusahaan, a.Urut_Pallet, a.Kode_Unik_Print, a.Urut_Oto, a.Kode_Barang, e.Nama "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							If DataGridView1.Rows.Count <> 0 Then
								For z As Integer = 0 To DataGridView1.Rows.Count - 1
									get_grid_view(z)
									If .Rows(i).Item("Kode_Barang") <> LvKode Then
										CloseConn()
										MessageBox.Show("barang yang mau repacking harus sama.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Txt_QR.Text = ""
										Txt_QR.Focus()
										Exit Sub
									End If
								Next
							End If

							DataGridView1.Rows.Add(1)
							DataGridView1.Rows(a).Cells(Cellbarcode).Value = .Rows(i).Item("Barcode_Box")
							DataGridView1.Rows(a).Cells(CellKode).Value = .Rows(i).Item("Kode_Barang")
							DataGridView1.Rows(a).Cells(Cellnama).Value = .Rows(i).Item("Nama_Barang")
							DataGridView1.Rows(a).Cells(Celljmlh).Value = Format(.Rows(i).Item("Jumlah"), "N0")
							DataGridView1.Rows(a).Cells(CellJumlahwaste).Value = "0"
							DataGridView1.Rows(a).Cells(CellurutBox).Value = .Rows(i).Item("Urut_Box")
							DataGridView1.Rows(a).Cells(Cellurutpallet).Value = .Rows(i).Item("Urut_Pallet")

						Next
					Else
						CloseConn()
						MessageBox.Show("Box tidak ditemukan.....", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_QR.Text = ""
						Txt_QR.Focus()
						Exit Sub
					End If
				End With
			End Using

			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
		Txt_QR.Text = ""
		Txt_QR.Focus()
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

	Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
		If e.RowIndex >= 0 Then
			get_jam()
			If e.ColumnIndex = Cellwaste Then
				N_EMI_SD_Waste_Scan_Packing.Label3.Text = e.RowIndex
				N_EMI_SD_Waste_Scan_Packing.Label4.Text = DataGridView1.Rows(e.RowIndex).Cells(Celljmlh).Value
				N_EMI_SD_Waste_Scan_Packing.asal = "N_Emi_Transaksi_Repacking - Waste"
				N_EMI_SD_Waste_Scan_Packing.kolom = CellJumlahwaste
				N_EMI_SD_Waste_Scan_Packing.kosong()
				N_EMI_SD_Waste_Scan_Packing.ShowDialog()
			End If
		End If
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		If DataGridView1.Rows.Count = 0 Then
			MessageBox.Show("Box repacking belum di scan.....", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_QR.Text = ""
			Txt_QR.Focus()
			Exit Sub
		End If

		For a As Integer = 0 To DataGridView1.Rows.Count - 1
			get_grid_view(a)
			If LvJumlahwaste = "" Then
				MessageBox.Show("Jumlah waste belum di input.....", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If
		Next

		'Dim xjmlh_waste As Integer = 0
		'For a As Integer = 0 To DataGridView1.Rows.Count - 1
		'    get_grid_view(a)
		'    xjmlh_waste = xjmlh_waste + Val(LvJumlahwaste)
		'Next

		'If xjmlh_waste = 0 Then
		'    MessageBox.Show("Jumlah waste belum di input.....", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'    Exit Sub
		'End If

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			For a As Integer = 0 To DataGridView1.Rows.Count - 1
				get_grid_view(a)

				SQL = "select Flag_Input_GR2, Status from N_EMI_Transaksi_Packing_Pallet "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Urut_Oto = '" & Lvurutpallet & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						If General_Class.CekNULL(dr("Status")) <> "" Then
							dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("Pallet sudah pernah dibatalin....", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						ElseIf General_Class.CekNULL(dr("Flag_Input_GR2")) <> "" Then
							dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("pallet sudah input gr2, jadi tidak bisa repacking", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						End If
					Else
						dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Terjadi kesalahan, data pallet tidak ditemukan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				Dim FPro_Results As String = "TR"
				Dim xno_trans As String = ""
				xno_trans = FPro_Results & "-" & Format(tgl_skg, "MM/yy") & "-" &
													  General_Class.Get_Last_Number2("N_EMI_Transaksi_Packing_Repacking", "No_Transaksi", 5,
													  "Kode_perusahaan", KodePerusahaan,
													  "And", "substring(No_Transaksi,1," & Len(FPro_Results) + 6 & ")", FPro_Results & "-" & Format(tgl_skg, "MM/yy"))

				SQL = "Select a.Kode_Unik_Print As Barcode_Box, a.Urut_Oto As Urut_Box, a.Kode_Barang, e.Nama As Nama_Barang, sum(b.Jumlah) As Jumlah, "
				SQL = SQL & "ISNULL((Select z.Kode_Unik_Print from N_EMI_Transaksi_Packing_Pallet z where a.Kode_Perusahaan = z.Kode_Perusahaan "
				SQL = SQL & "And a.Urut_Pallet = z.Urut_Oto),'-') Barcode_Pallet, ISNULL(a.Urut_Pallet, '0') AS Urut_Pallet "
				SQL = SQL & "from N_EMI_Transaksi_Packing_Box a, N_EMI_Transaksi_Packing_Det b, N_EMI_Transaksi_Packing_Detail c, Barang_SN d, Barang e "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Urut_Oto = b.Urut_Transaksi_Box "
				SQL = SQL & "and a.Status is null and b.Status is null "
				SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Urut_Detail_Transaksi_Packing = c.Urut_Oto "
				SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.SN_Baru = d.Serial_Number and d.Kode_Perusahaan = e.Kode_Perusahaan "
				SQL = SQL & "and d.Kode_Stock_Owner = e.Kode_Stock_Owner and d.Kode_Barang = e.Kode_Barang "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Unik_Print = '" & Lvbarcode & "' "
				SQL = SQL & "group by a.Kode_Perusahaan, a.Urut_Pallet, a.Kode_Unik_Print, a.Urut_Oto, a.Kode_Barang, a.Tgl_Cetak, a.Jam_Cetak, e.Nama "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For i As Integer = 0 To .Rows.Count - 1

								SQL = "insert into N_EMI_Transaksi_Packing_Repacking(Kode_Perusahaan, No_Transaksi, Urut_Box_Lama, Urut_Pallet, Tanggal, Jam, Jumlah, Line) "
								SQL = SQL & "values('" & KodePerusahaan & "', '" & xno_trans & "', '" & .Rows(i).Item("Urut_Box") & "', '" & .Rows(i).Item("Urut_Pallet") & "', "
								SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & Val(HilangkanTanda(Lvjmlh)) & "', '" & Label14.Text & "')"
								ExecuteTrans(SQL)

								SQL = "select a.Kode_Unik_Print as Barcode_Box, a.Urut_Oto as Urut_Box, a.Kode_Barang, e.Nama as Nama_Barang, b.Jumlah, b.Urut_Detail_Transaksi_Packing, "
								SQL = SQL & "ISNULL((select z.Kode_Unik_Print from N_EMI_Transaksi_Packing_Pallet z where a.Kode_Perusahaan = z.Kode_Perusahaan "
								SQL = SQL & "and a.Urut_Pallet = z.Urut_Oto),'-') Barcode_Pallet, ISNULL(a.Urut_Pallet, '0') AS Urut_Pallet, b.Urut_Oto as Urut_Det, c.Urut_Results_Detail_Pallet "
								SQL = SQL & "from N_EMI_Transaksi_Packing_Box a, N_EMI_Transaksi_Packing_Det b, N_EMI_Transaksi_Packing_Detail c, Barang_SN d, Barang e "
								SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Urut_Oto = b.Urut_Transaksi_Box "
								SQL = SQL & "and a.Status is null and b.Status is null "
								SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Urut_Detail_Transaksi_Packing = c.Urut_Oto "
								SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.SN_Baru = d.Serial_Number and d.Kode_Perusahaan = e.Kode_Perusahaan "
								SQL = SQL & "and d.Kode_Stock_Owner = e.Kode_Stock_Owner and d.Kode_Barang = e.Kode_Barang "
								SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Unik_Print = '" & Lvbarcode & "' order by b.Urut_Detail_Transaksi_Packing "
								Using Ds2 = BindingTrans(SQL)
									If Ds2.Tables("MyTable").Rows.Count <> 0 Then
										For z As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1
											SQL = "insert into N_EMI_Transaksi_Packing_Repacking_Detail(Kode_Perusahaan, No_Transaksi, Urut_Detail_Transaksi_Packing, Urut_Results_Detail_Pallet, Jumlah, Jumlah_Waste, Jumlah_Good) "
											SQL = SQL & "values('" & KodePerusahaan & "', '" & xno_trans & "', '" & Ds2.Tables("MyTable").Rows(z).Item("Urut_Detail_Transaksi_Packing") & "', "
											SQL = SQL & "'" & Ds2.Tables("MyTable").Rows(z).Item("Urut_Results_Detail_Pallet") & "', '" & Ds2.Tables("MyTable").Rows(z).Item("Jumlah") & "', 0, 0) "
											ExecuteTrans(SQL)

											SQL = "update N_EMI_Transaksi_Packing_Detail set Jumlah_Sdh_Packing = Jumlah_Sdh_Packing - '" & Ds2.Tables("MyTable").Rows(z).Item("Jumlah") & "' where "
											SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Urut_Oto = '" & Ds2.Tables("MyTable").Rows(z).Item("Urut_Detail_Transaksi_Packing") & "' "
											ExecuteTrans(SQL)

											SQL = "update Emi_Production_Results_Detail_Pallet set Jumlah_Sdh_Packing = Jumlah_Sdh_Packing - '" & Ds2.Tables("MyTable").Rows(z).Item("Jumlah") & "' where "
											SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Urut_Oto = '" & Ds2.Tables("MyTable").Rows(z).Item("Urut_Results_Detail_Pallet") & "' "
											ExecuteTrans(SQL)

											SQL = "update N_EMI_Transaksi_Packing_Det set Status = 'Y' where "
											SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Urut_Oto = '" & Ds2.Tables("MyTable").Rows(z).Item("Urut_Det") & "' "
											ExecuteTrans(SQL)
										Next
									Else
										CloseTrans()
										CloseConn()
										MessageBox.Show("sn tidak ditemukan.....", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If
								End Using

								Dim xjumlahwaste As Integer = Val(HilangkanTanda(LvJumlahwaste))
								SQL = "select No_Transaksi, Urut_Oto, Urut_Detail_Transaksi_Packing, Urut_Results_Detail_Pallet, Jumlah from N_EMI_Transaksi_Packing_Repacking_Detail "
								SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi =  '" & xno_trans & "' "
								Using Ds2 = BindingTrans(SQL)
									If Ds2.Tables("MyTable").Rows.Count <> 0 Then
										For z As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1
											If xjumlahwaste = 0 Then
												Exit For
											ElseIf xjumlahwaste < 0 Then
												CloseTrans()
												CloseConn()
												MessageBox.Show("Sisa waste < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
												Exit Sub
											End If

											If xjumlahwaste < Ds2.Tables("MyTable").Rows(z).Item("Jumlah") Or xjumlahwaste = Ds2.Tables("MyTable").Rows(z).Item("Jumlah") Then
												SQL = "update N_EMI_Transaksi_Packing_Detail set Jumlah_Waste = Jumlah_Waste + '" & xjumlahwaste & "' where "
												SQL = SQL & "Urut_Oto = '" & Ds2.Tables("MyTable").Rows(z).Item("Urut_Detail_Transaksi_Packing") & "' "
												ExecuteTrans(SQL)

												SQL = "update Emi_Production_Results_Detail_Pallet set Jumlah_Waste = Jumlah_Waste + '" & xjumlahwaste & "' where "
												SQL = SQL & "Urut_Oto = '" & Ds2.Tables("MyTable").Rows(z).Item("Urut_Results_Detail_Pallet") & "' "
												ExecuteTrans(SQL)

												SQL = "update N_EMI_Transaksi_Packing_Repacking_Detail set Jumlah_Waste = Jumlah_Waste + '" & xjumlahwaste & "' "
												SQL = SQL & "where Urut_Oto = '" & Ds2.Tables("MyTable").Rows(z).Item("Urut_Oto") & "' "
												ExecuteTrans(SQL)

												xjumlahwaste = 0
											ElseIf xjumlahwaste > Ds2.Tables("MyTable").Rows(z).Item("Jumlah") Then
												SQL = "update N_EMI_Transaksi_Packing_Detail set Jumlah_Waste = Jumlah_Waste + '" & Ds2.Tables("MyTable").Rows(z).Item("Jumlah") & "' where "
												SQL = SQL & "Urut_Oto = '" & Ds2.Tables("MyTable").Rows(z).Item("Urut_Detail_Transaksi_Packing") & "' "
												ExecuteTrans(SQL)

												SQL = "update Emi_Production_Results_Detail_Pallet set Jumlah_Waste = Jumlah_Waste + '" & Ds2.Tables("MyTable").Rows(z).Item("Jumlah") & "' where "
												SQL = SQL & "Urut_Oto = '" & Ds2.Tables("MyTable").Rows(z).Item("Urut_Results_Detail_Pallet") & "' "
												ExecuteTrans(SQL)

												SQL = "update N_EMI_Transaksi_Packing_Repacking_Detail set Jumlah_Waste = Jumlah_Waste + '" & Ds2.Tables("MyTable").Rows(z).Item("Jumlah") & "' "
												SQL = SQL & "where Urut_Oto = '" & Ds2.Tables("MyTable").Rows(z).Item("Urut_Oto") & "' "
												ExecuteTrans(SQL)

												xjumlahwaste = xjumlahwaste - Ds2.Tables("MyTable").Rows(z).Item("Jumlah")
											Else
												CloseTrans()
												CloseConn()
												MessageBox.Show("Barang tidak cukup harap scan barang terlebih dahulu !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
												Exit Sub
											End If

											SQL = "update N_EMI_Transaksi_Packing_Pallet set Jumlah = Jumlah - 1 where Urut_Oto = '" & .Rows(i).Item("Urut_Pallet") & "' "
											ExecuteTrans(SQL)

											SQL = "update N_EMI_Transaksi_Packing_Box set Status = 'Y' where Urut_Oto = '" & .Rows(i).Item("Urut_Box") & "' "
											ExecuteTrans(SQL)
										Next
									End If
								End Using

								SQL = "select No_Transaksi, Urut_Oto, Urut_Detail_Transaksi_Packing, Jumlah_Waste, Jumlah from N_EMI_Transaksi_Packing_Repacking_Detail "
								SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi =  '" & xno_trans & "' "
								Using Ds2 = BindingTrans(SQL)
									If Ds2.Tables("MyTable").Rows.Count <> 0 Then
										For z As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

											Dim xjumlahgood As Integer = Ds2.Tables("MyTable").Rows(z).Item("Jumlah") - Ds2.Tables("MyTable").Rows(z).Item("Jumlah_Waste")
											SQL = "update N_EMI_Transaksi_Packing_Repacking_Detail set Jumlah_Good = Jumlah_Good + '" & xjumlahgood & "' "
											SQL = SQL & "where Urut_Oto = '" & Ds2.Tables("MyTable").Rows(z).Item("Urut_Oto") & "' "
											ExecuteTrans(SQL)

										Next
									End If
								End Using
							Next
						Else
							CloseTrans()
							CloseConn()
							MessageBox.Show("Box tidak ditemukan.....", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End With
				End Using
			Next

			Cmd.Transaction.Commit()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		kosong()
		Me.Close()
	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		If Label14.Text.Trim.Length = 0 Then
			MessageBox.Show("line belum dipilih ...", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		N_EMI_SD_Pilih_Scan_Repacking.kosong()
		If Label3.Text <> "" Then
			N_EMI_SD_Pilih_Scan_Repacking.filtertambah = "and d.Kode_Barang = '" & Label3.Text & "' "
		Else
			N_EMI_SD_Pilih_Scan_Repacking.filtertambah = " "
		End If
		N_EMI_SD_Pilih_Scan_Repacking.ShowDialog()
	End Sub

End Class