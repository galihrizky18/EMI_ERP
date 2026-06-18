Public Class N_EMI_Transaksi_Pemusnahan_Barang_Validasi

	Dim judulForm As String = "Validasi Pemusnahan Barang"

	Private Sub N_EMI_Transaksi_Pemusnahan_Barang_Validasi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Lv_DataPengajuan.Columns.Clear()
		Lv_DataPengajuan.Columns.Add("No Faktur", 180, HorizontalAlignment.Left)
		Lv_DataPengajuan.Columns.Add("Lokasi", 150, HorizontalAlignment.Left)
		Lv_DataPengajuan.Columns.Add("Tanggal", 130, HorizontalAlignment.Center)
		Lv_DataPengajuan.Columns.Add("Keterangan", 340, HorizontalAlignment.Left)
		Lv_DataPengajuan.Columns.Add("User ID", 120, HorizontalAlignment.Center)
		Lv_DataPengajuan.View = View.Details

		Lv_DetailPallet.Columns.Clear()
		Lv_DetailPallet.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
		Lv_DetailPallet.Columns.Add("Nama Barang", 200, HorizontalAlignment.Left)
		Lv_DetailPallet.Columns.Add("Barode", 280, HorizontalAlignment.Left)
		Lv_DetailPallet.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
		Lv_DetailPallet.Columns.Add("Jumlah Bags", 100, HorizontalAlignment.Center)
		Lv_DetailPallet.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
		Lv_DetailPallet.View = View.Details

		Kosong()

	End Sub

	Private Sub Kosong()

		Txt_NoFaktur.Text = ""

		LoadData()

	End Sub

	Private Sub LoadData()

		Try
			OpenConn()

			Lv_DataPengajuan.Items.Clear() : Lv_DetailPallet.Items.Clear()

			SQL = "SELECT DISTINCT a.Kode_Perusahaan, a.No_Faktur, a.Kode_Stock_Owner, a.Tanggal, a.UserID, "
			SQL = SQL & "a.Keterangan "
			SQL = SQL & "FROM N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Detail b, N_EMI_Transaksi_Transfer_Waste_Det c "
			SQL = SQL & "WHERE a.kode_perusahaan = b.kode_perusahaan AND b.Kode_Perusahaan = c.Kode_Perusahaan "
			SQL = SQL & "AND a.No_Faktur = b.no_Faktur "
			SQL = SQL & "AND b.No_Faktur = c.No_Faktur  "
			SQL = SQL & "AND b.Urut_Oto = c.Urut_TF "
			SQL = SQL & "AND NOT EXISTS ( "
			SQL = SQL & "SELECT 1 FROM N_EMI_Transaksi_Transfer_Waste_Det c2 "
			SQL = SQL & "WHERE c2.Kode_Perusahaan = a.Kode_Perusahaan "
			SQL = SQL & "AND c2.No_Faktur = a.No_Faktur "
			SQL = SQL & "AND (c2.Selesai IS NULL OR c2.Selesai <> 'Y')) "
			SQL = SQL & "AND a.status IS NULL "
			SQL = SQL & "AND a.Flag_Validasi IS NULL "
			SQL = SQL & "AND a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "ORDER BY a.no_Faktur "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_DataPengajuan.Items.Add(Dr("No_Faktur"))
					Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
					Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
					Lv.SubItems.Add(Dr("Keterangan"))
					Lv.SubItems.Add(Dr("UserID"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
		If Txt_NoFaktur.Text.Trim.Length = 0 Then
			MessageBox.Show("No Faktur Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_NoFaktur.Focus() : Exit Sub
		End If

		Try
			OpenConn()

			Lv_DataPengajuan.Items.Clear() : Lv_DetailPallet.Items.Clear()

			SQL = "SELECT DISTINCT a.Kode_Perusahaan, a.No_Faktur, a.Kode_Stock_Owner, a.Tanggal, a.UserID, "
			SQL = SQL & "a.Keterangan, b.Kode_Barang, d.Nama AS Nama_Barang, b.Total, b.Total_Bags, b.Satuan "
			SQL = SQL & "FROM N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Detail b, N_EMI_Transaksi_Transfer_Waste_Det c, barang d "
			SQL = SQL & "WHERE a.kode_perusahaan = b.kode_perusahaan AND b.Kode_Perusahaan = c.Kode_Perusahaan  AND b.Kode_Perusahaan = d.Kode_Perusahaan "
			SQL = SQL & "AND a.No_Faktur = b.no_Faktur "
			SQL = SQL & "AND b.No_Faktur = c.No_Faktur  "
			SQL = SQL & "AND b.Urut_Oto = c.Urut_TF "
			SQL = SQL & "AND a.Kode_Stock_Owner = d.Kode_Stock_Owner  "
			SQL = SQL & "AND b.Kode_Barang = d.Kode_Barang "
			SQL = SQL & "AND NOT EXISTS ( "
			SQL = SQL & "SELECT 1 FROM N_EMI_Transaksi_Transfer_Waste_Det c2 "
			SQL = SQL & "WHERE c2.Kode_Perusahaan = a.Kode_Perusahaan "
			SQL = SQL & "AND c2.No_Faktur = a.No_Faktur "
			SQL = SQL & "AND (c2.Selesai IS NULL OR c2.Selesai <> 'Y')) "
			SQL = SQL & "AND a.status IS NULL "
			SQL = SQL & "AND a.Flag_Validasi IS NULL "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Faktur like '%" & Txt_NoFaktur.Text & "%' "
			SQL = SQL & "ORDER BY a.no_Faktur "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_DataPengajuan.Items.Add(Dr("No_Faktur"))
					Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
					Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
					Lv.SubItems.Add(Dr("Keterangan"))
					Lv.SubItems.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Dr("Nama_Barang"))
					Lv.SubItems.Add(Format(Dr("Total"), "N2"))
					Lv.SubItems.Add(Dr("Total_Bags"))
					Lv.SubItems.Add(Dr("Satuan"))
					Lv.SubItems.Add(Dr("UserID"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Lv_DataPengajuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_DataPengajuan.SelectedIndexChanged
		If Lv_DataPengajuan.Items.Count = 0 Or Lv_DataPengajuan.FocusedItem Is Nothing Then Exit Sub

		Try
			OpenConn()

			Dim SelectedFaktur As String = Lv_DataPengajuan.FocusedItem.Text

			Lv_DetailPallet.Items.Clear()
			SQL = "select a.kode_perusahaan, a.no_faktur, c.serial_number_awal, b.Kode_Barang, e.Nama as Nama_Barang, (d.Qr_Code +'-' + d.Kode_Unik_Berjalan) as Barcode, c.Jumlah, c.Jumlah_Bags, b.Satuan "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Detail b, N_EMI_Transaksi_Transfer_Waste_Det c, Barang_SN d, barang e "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.kode_perusahaan = d.kode_perusahaan "
			SQL = SQL & "and b.Kode_Perusahaan = e.Kode_Perusahaan "
			SQL = SQL & "and a.No_Faktur = b.No_Faktur "
			SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
			SQL = SQL & "and a.Kode_Stock_Owner = e.Kode_Stock_Owner and b.Kode_Barang = e.Kode_Barang "
			SQL = SQL & "and a.kode_Stock_owner = d.kode_stock_owner and b.kode_barang = d.kode_barang and c.serial_number_awal= d.serial_number "
			SQL = SQL & "and a.Status is null and a.Flag_Validasi is null "
			SQL = SQL & "and c.Selesai = 'Y' "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Faktur = '" & SelectedFaktur & "' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_DetailPallet.Items.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Dr("Nama_Barang"))
					Lv.SubItems.Add(Dr("Barcode"))
					Lv.SubItems.Add(Format(Dr("Jumlah"), "N2"))
					Lv.SubItems.Add(Dr("Jumlah_Bags"))
					Lv.SubItems.Add(Dr("Satuan"))

				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub ValidasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiToolStripMenuItem.Click
		If Lv_DataPengajuan.Items.Count = 0 Then Exit Sub
		If Lv_DataPengajuan.FocusedItem Is Nothing Then
			MessageBox.Show("Pilih Data yang akan divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Lv_DataPengajuan.Focus() : Exit Sub
		End If

		If MessageBox.Show("Apakah anda yakin akan memvalidasi data ini ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'====================
			'=     CEK ROLE     =
			'====================
			If CekButtonRole("Validasi_Pemusnahan_Barang") = "T" Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Validasi Pemusnahan Barang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			Dim SelectedFaktur As String = Lv_DataPengajuan.FocusedItem.Text

			'===================================================
			'=     CEK APAKAH SEMUA DATA SUDAH DI VALIDASI     =
			'===================================================
			SQL = "select a.Kode_Perusahaan from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Detail b, N_EMI_Transaksi_Transfer_Waste_Det c "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
			SQL = SQL & "and a.No_Faktur = b.No_Faktur and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
			SQL = SQL & "and a.status is null and c.Selesai is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Faktur = '" & SelectedFaktur & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Barang Belum di Validasi Sepenuhnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'==============================
			'=     GET DATA PENGAJUAN     =
			'==============================
			SQL = "select a.No_Faktur, a.Kode_Stock_Owner, b.Kode_Barang, c.Serial_Number_Awal, d.Serial_Number, c.Jumlah, d.Jumlah as Jumlah_Barang, d.Jumlah_Bags, b.Satuan, b.Satuan_Barang, d.urut_oto "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Detail b, N_EMI_Transaksi_Transfer_Waste_Det c, N_EMI_Transaksi_Transfer_Waste_Det2 d "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
			SQL = SQL & "and a.No_Faktur = b.No_Faktur "
			SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
			SQL = SQL & "and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
			SQL = SQL & "and a.status is null and a.Flag_Validasi is null and c.Selesai = 'Y' "
			SQL = SQL & "and d.Flag_Validasi is null " 'tuk memastikan data di det2 belum divalidasi
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Faktur = '" & SelectedFaktur & "' "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							Dim Kode_Transfer As String = .Rows(i).Item("No_Faktur")
							Dim KdSo As String = .Rows(i).Item("Kode_Stock_Owner")
							Dim KdBarang As String = .Rows(i).Item("Kode_Barang")
							Dim SN_Awal As String = .Rows(i).Item("Serial_Number_Awal")
							Dim SN_Baru As String = .Rows(i).Item("Serial_Number")
							Dim Jumlah As String = .Rows(i).Item("Jumlah")
							Dim Jumlah_Kecil As String = .Rows(i).Item("Jumlah_Barang")
							Dim Jumlah_Bags As String = .Rows(i).Item("Jumlah_Bags")
							Dim Satuan As String = .Rows(i).Item("Satuan")
							Dim Satuan_kecil As String = .Rows(i).Item("Satuan_Barang")
							Dim Urut_Det2 As String = .Rows(i).Item("urut_oto")

							'===============================
							'=     CEK KESESUAIAN DATA     =
							'===============================
							SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
							SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
							SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
							SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
							SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
							SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
							SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & KdSo & "' "
							SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
							SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
							Using Ds1 = BindingTrans(SQL)
								If Ds1.Tables("MyTable").Rows.Count <> 0 Then
									If Ds1.Tables("MyTable").Rows(0).Item("good_stock") <> Ds1.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
										CloseTrans()
										CloseConn()
										MessageBox.Show("Jumlah Stock pada Kode Barang " & KdBarang & " Tidak Sesuai Sebelum Potong Stock ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If
								Else
									CloseTrans()
									CloseConn()
									MessageBox.Show("Data Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

							'==================================
							'=     POTONG STOCK BARANG_SN     =
							'==================================
							SQL = "select Jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & KdSo & "' "
							SQL = SQL & "and Kode_Barang = '" & KdBarang & "' and Serial_Number = '" & SN_Baru & "' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then

									If Val(HilangkanTanda(Dr("Jumlah"))) < Jumlah_Kecil Or Val(HilangkanTanda(Dr("Jumlah_Bags"))) < Jumlah_Bags Then

										Dr.Close()
										CloseTrans()
										CloseConn()
										MessageBox.Show("Jumlah Stock " & KdBarang & " Lebih Kecil dari Jumlah Potong", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									Else

										Dr.Close()
										SQL = "update Barang_SN set Jumlah = Jumlah - Round(" & Jumlah_Kecil & ",4), Jumlah_Bags = Jumlah_Bags - " & Jumlah_Bags & " "
										SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & KdSo & "' "
										SQL = SQL & "and Kode_Barang = '" & KdBarang & "' and Serial_Number = '" & SN_Baru & "' "
										ExecuteTrans(SQL)

									End If
								Else
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Kode Barang " & KdBarang & " Tidak Ditemukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

							'===============================
							'=     POTONG STOCK BARANG     =
							'===============================
							SQL = "select Good_Stock, Jumlah_Bags from Barang where kode_perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "and Kode_Stock_Owner = '" & KdSo & "' and Kode_Barang = '" & KdBarang & "' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then

									If Val(HilangkanTanda(Dr("Good_Stock"))) < Jumlah_Kecil Or Val(HilangkanTanda(Dr("Jumlah_Bags"))) < Jumlah_Bags Then

										Dr.Close()
										CloseTrans()
										CloseConn()
										MessageBox.Show("Jumlah Stock " & KdBarang & " Lebih Kecil dari Jumlah Potong", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									Else

										Dr.Close()
										SQL = "update Barang set Good_Stock = Good_Stock - Round(" & Jumlah_Kecil & ",4), Jumlah_Bags = Jumlah_Bags - " & Jumlah_Bags & " "
										SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & KdSo & "' "
										SQL = SQL & "and Kode_Barang = '" & KdBarang & "' "
										ExecuteTrans(SQL)

									End If
								Else
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Kode Barang " & KdBarang & " Tidak Ditemukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

							'=================================
							'=     CEK BARANG SN HARUS 0     =
							'=================================
							SQL = "select Jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & KdSo & "' "
							SQL = SQL & "and Kode_Barang = '" & KdBarang & "' and Serial_Number = '" & SN_Baru & "' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									If Val(HilangkanTanda(Dr("Jumlah"))) <> 0 Or Val(HilangkanTanda(Dr("Jumlah_Bags"))) <> 0 Then
										Dr.Close()
										CloseTrans()
										CloseConn()
										MessageBox.Show("Terjadi Kesalahan Saat Potong Stock", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If
								End If
							End Using

							'===============================
							'=     CEK KESESUAIAN DATA     =
							'===============================
							SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
							SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
							SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
							SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
							SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
							SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
							SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & KdSo & "' "
							SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
							SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
							Using Ds1 = BindingTrans(SQL)
								If Ds1.Tables("MyTable").Rows.Count <> 0 Then
									If Ds1.Tables("MyTable").Rows(0).Item("good_stock") <> Ds1.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
										CloseTrans()
										CloseConn()
										MessageBox.Show("Jumlah Stock pada Kode Barang " & KdBarang & " Tidak Sesuai Setelah Potong Stock ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If
								Else
									CloseTrans()
									CloseConn()
									MessageBox.Show("Data Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

							'==================
							'=     JURNAL     =
							'==================
							'dari
							Dim inisial_faktur_dari As String = ""
							Dim akun_persediaan_dari As String = ""
							Dim akun_persediaan_tujuan As String = ""

							Dim nilai_persediaan_min As Double = 0
							SQL = "select round(dbo.get_hpp(serial_number) * " & Jumlah_Kecil & ", 2) as rp_persediaan_min from barang_sn where "
							SQL = SQL & "Kode_Stock_Owner='" & KdSo & "' and Kode_Barang='" & KdBarang & "' "
							SQL = SQL & "and Serial_Number='" & SN_Baru & "'"
							Using dr = OpenTrans(SQL)
								If dr.Read Then
									nilai_persediaan_min = dr("rp_persediaan_min")
								Else
									dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Data SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

							SQL = "select inisial_faktur from stock_owner_gudang "
							SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & KdSo & "' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									'akun_persediaan_dari = Dr("persediaan")
									inisial_faktur_dari = Dr("inisial_faktur")
								Else
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

							SQL = "select c.akun_Persediaan "
							SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
							SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
							SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
							SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "and b.kode_stock_owner = '" & KdSo & "' and b.Kode_Barang='" & KdBarang & "' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									akun_persediaan_dari = Dr("akun_Persediaan")
								Else
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

							SQL = "select c.akun_Persediaan "
							SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
							SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
							SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
							SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "and b.kode_stock_owner = '" & KdSo & "' and b.Kode_Barang='" & KdBarang & "' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									akun_persediaan_tujuan = Dr("akun_Persediaan")
								Else
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

							Dim Kode_voucher As String = ""
							Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
							Dim pagenumber As Integer = 1

							SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
							SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
							SQL = SQL & "'" & Kode_voucher & "', "
							SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
							SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
							SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock " & Kode_Transfer & "', '', "
							SQL = SQL & "'-', '" & UserID & "')"
							ExecuteTrans(SQL)

							SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
									  Strings.Mid(akun_persediaan_dari, 2, 1),
									  Strings.Mid(Ganti(akun_persediaan_dari), 3),
									  KodePerusahaan, KodeProyek, "Persedian " & Kode_Transfer, "0", nilai_persediaan_min, pagenumber, KdSo, Bahasa_Pilihan, Ket_Cost_Center_HO)
							ExecuteTrans(SQL)
							pagenumber = pagenumber + 1

							SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
									 Strings.Mid(akun_persediaan_tujuan, 2, 1),
									 Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
									 KodePerusahaan, KodeProyek, "Persedian " & Kode_Transfer, nilai_persediaan_min, "0", pagenumber, KdSo, Bahasa_Pilihan, Ket_Cost_Center_HO)
							ExecuteTrans(SQL)
							pagenumber = pagenumber + 1

							'======================
							'=     CEK JURNAL     =
							'======================
							SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
							SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
							SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									If Dr("debit") <> Dr("kredit") Then
										Dr.Close()
										CloseTrans()
										CloseConn()
										MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If
								Else
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

							'============================================
							'=     UPDATE N_EMI_Transaksi_Transfer_Waste_Det2     =
							'============================================
							SQL = "select Kode_Perusahaan from N_EMI_Transaksi_Transfer_Waste_Det2 where Kode_Perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "and No_Faktur = '" & Kode_Transfer & "' and Urut_Oto = '" & Urut_Det2 & "' "
							Using Ds1 = BindingTrans(SQL)
								If Ds1.Tables("MyTable").Rows.Count <> 0 Then

									SQL = "update N_EMI_Transaksi_Transfer_Waste_Det2 set Flag_Validasi = 'Y', "
									SQL = SQL & "Kode_Voucher_Validasi = '" & Kode_voucher & "' "
									SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
									SQL = SQL & "and No_Faktur = '" & Kode_Transfer & "' and Urut_Oto = '" & Urut_Det2 & "' "
									ExecuteTrans(SQL)
								Else
									CloseTrans()
									CloseConn()
									MessageBox.Show("Data Barang Tidak Ditemukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

						Next
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show("Faktur Pengajuan Tidak Ditemukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			'============================================
			'=     UPDATE N_EMI_Transaksi_Transfer_Waste_Det2     =
			'============================================

			SQL = "select distinct a.Kode_Perusahaan, a.Status from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Detail b, N_EMI_Transaksi_Transfer_Waste_Det c, N_EMI_Transaksi_Transfer_Waste_Det2 d "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
			SQL = SQL & "and a.No_Faktur = b.No_Faktur "
			SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
			SQL = SQL & "and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
			SQL = SQL & "and d.Flag_Validasi is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Faktur = '" & SelectedFaktur & "' and a.status is null "
			Using Ds = BindingTrans(SQL)
				If Ds.Tables("MyTable").Rows.Count = 0 Then

					SQL = "update N_EMI_Transaksi_Transfer_Waste set Flag_Validasi = 'Y', "
					SQL = SQL & "UserID_Validasi = '" & UserID & "', "
					SQL = SQL & "Tanggal_Validasi = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Validasi = '" & Format(tgl_skg, "HH:mm:ss") & "' "
					SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
					SQL = SQL & "and No_Faktur = '" & SelectedFaktur & "' and status is null "
					ExecuteTrans(SQL)
				Else
					CloseTrans()
					CloseConn()
					MessageBox.Show("Terjadi Kesalahan, Barang belum sepeunuhnya tervalidasi", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Data Berhasil Divalidasi", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		LoadData()

	End Sub

	Private Sub Txt_NoFaktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NoFaktur.KeyPress
		If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
	End Sub

End Class