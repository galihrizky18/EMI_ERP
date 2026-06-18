Public Class N_EMI_SD_Transaksi_Request_Material_Tambahan

	Public kd_so As String

	Dim Dgv_NoFak, Dgv_KdSo, Dgv_KdBarang, Dgv_JmlhKebutuhan, Dgv_JmlhDiProduksi, Dgv_Sisa, Dgv_SatuanBesar, Dgv_JmlhInput, Dgv_SatuanKecil, Dgv_Tipe, Dgv_Warna, Dgv_JenisBahan, Dgv_StockProduksi, Dgv_TotalTF, Dgv_Lokasi_Tujuan, Dgv_Nama_Barang As String

	Dim cell_NoFak As Integer = 0
	Dim cell_Kd_SO As Integer = 1
	Dim cell_Kd_Barang As Integer = 2
	Dim cell_JumlahKebutuhan As Integer = 3
	Dim cell_JumlahDiproduksi As Integer = 4
	Dim cell_Sisa As Integer = 5
	Dim cell_SatuanBesar As Integer = 6
	Dim cell_JumlahInput As Integer = 7
	Dim cell_SatuanKecil As Integer = 8
	Dim cell_Tipe As Integer = 9
	Dim cell_warna As Integer = 10
	Dim cell_JenisBahan As Integer = 11
	Dim cell_StockProduksi As Integer = 12
	Dim cell_TotalTransfer As Integer = 13
	Dim cell_LokasiTujuan As Integer = 14
	Dim cell_Nama_Barang As Integer = 15

	Private Sub N_EMI_SD_Transaksi_Request_Material_Tambahan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Dgv_Data.Columns(cell_Nama_Barang).DisplayIndex = 3
		Dgv_Data.Columns(cell_StockProduksi).DisplayIndex = 4
		Dgv_Data.Columns(cell_TotalTransfer).DisplayIndex = 6
		Dgv_Data.Columns(cell_LokasiTujuan).DisplayIndex = 8
		Dgv_Data.Columns(cell_JumlahInput).DisplayIndex = 9

		Try
			OpenConn()

			SQL = "select Kode_Stock_Owner from Stock_Owner where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & Ket_Lokasi_HO & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Txt_Lokasi.Text = Dr("Kode_Stock_Owner")
				End If
			End Using

			Dim Jumlah_Batch As Integer
			SQL = "select Jumlah_Batch from Emi_Split_Production_Order "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null and No_Transaksi = '" & Txt_No_Split.Text & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Jumlah_Batch = Dr("Jumlah_Batch")
				Else
					CloseConn()
					MessageBox.Show("Jumlah Batch Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			Cmb_Batch.Items.Clear()

			For i As Integer = 1 To Jumlah_Batch
				Cmb_Batch.Items.Add(i)
			Next

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong()

	End Sub

	Private Sub Kosong()

		TxtKeterangan.Text = ""

		Load_Dgv()
	End Sub

	Private Sub get_no_faktur()
		Txt_NoFaktur_ReqMaterial.Text = fRequestMaterial & Format(tgl_skg, "MMyy") & "-" &
							 General_Class.Get_Last_Number2("Emi_Material_Requisition", "No_Faktur", 5,
							 "Kode_perusahaan", KodePerusahaan,
							 "And", "substring(No_Faktur, 1, " & Len(fRequestMaterial) + 4 & ")", fRequestMaterial & Format(tgl_skg, "MMyy"))
	End Sub

	Private Sub Get_DGV_Items(ByVal index As Integer)
		'TODO : GetDataLV
		Dgv_NoFak = Dgv_Data.Rows(index).Cells(cell_NoFak).Value
		Dgv_KdSo = Dgv_Data.Rows(index).Cells(cell_Kd_SO).Value
		Dgv_KdBarang = Dgv_Data.Rows(index).Cells(cell_Kd_Barang).Value
		Dgv_JmlhKebutuhan = Dgv_Data.Rows(index).Cells(cell_JumlahKebutuhan).Value
		Dgv_JmlhDiProduksi = Dgv_Data.Rows(index).Cells(cell_JumlahDiproduksi).Value
		Dgv_Sisa = Dgv_Data.Rows(index).Cells(cell_Sisa).Value
		Dgv_SatuanBesar = Dgv_Data.Rows(index).Cells(cell_SatuanBesar).Value
		Dgv_JmlhInput = Dgv_Data.Rows(index).Cells(cell_JumlahInput).Value
		Dgv_SatuanKecil = Dgv_Data.Rows(index).Cells(cell_SatuanKecil).Value
		Dgv_Tipe = Dgv_Data.Rows(index).Cells(cell_Tipe).Value
		Dgv_Warna = Dgv_Data.Rows(index).Cells(cell_warna).Value
		Dgv_JenisBahan = Dgv_Data.Rows(index).Cells(cell_JenisBahan).Value
		Dgv_StockProduksi = Dgv_Data.Rows(index).Cells(cell_StockProduksi).Value
		Dgv_TotalTF = Dgv_Data.Rows(index).Cells(cell_TotalTransfer).Value
		Dgv_Lokasi_Tujuan = Dgv_Data.Rows(index).Cells(cell_LokasiTujuan).Value
		Dgv_Nama_Barang = Dgv_Data.Rows(index).Cells(cell_Nama_Barang).Value

	End Sub

	Private Sub Load_Dgv()

		If Txt_No_Split.Text.Trim.Length = 0 Then
			MessageBox.Show("No Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Try
			OpenConn()

			Dgv_Data.Rows.Clear()
			SQL = "Select a.No_Faktur, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama, a.Jumlah, a.Satuan, a.Nilai_Barang, a.Satuan_Barang, 'Bahan' as tipe, c.lokasi_gudang, "
			SQL = SQL & "(ISNULL( "
			SQL = SQL & "(select sum(z.jumlah) "
			SQL = SQL & "from Emi_Material_Requisition_det z, Emi_Material_Requisition x "
			SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan and a.Kode_Stock_Owner =z.Kode_Stock_Owner and a.Kode_Barang = z.Kode_Barang "
			SQL = SQL & "and z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur and a.No_Faktur = x.No_Faktur_Order and x.Status is null ), 0)) as Jumlah_Diproduksi, " 'JUMLAH DI PRODUKSI

			SQL = SQL & "(a.Jumlah - ISNULL( "
			SQL = SQL & "(select sum(z.jumlah) "
			SQL = SQL & "from Emi_Material_Requisition_det z, Emi_Material_Requisition x "
			SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan and a.Kode_Stock_Owner =z.Kode_Stock_Owner and a.Kode_Barang = z.Kode_Barang "
			SQL = SQL & "and z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur and a.No_Faktur = x.No_Faktur_Order and x.status is null ), 0)) as sisa, " 'SISA
			SQL = SQL & "'BAHAN' as Jenis_Bahan, " ' JENIS BAHAN

			SQL = SQL & "ISNULL(( select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang, a.Satuan_Barang, a.Satuan, sum(z.Jumlah)) "
			SQL = SQL & "from Barang_SN z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_Stock_Owner = a.Kode_Stock_Owner and z.Kode_Barang = a.Kode_Barang "
			SQL = SQL & "), 0) as Stock_Gudang_Produksi, " ' Stock_Gudang_Produksi

			SQL = SQL & "ISNULL((select sum(w.jumlah) from tf_stock_parent x, Tf_Stock y, Tf_Stock_det z, Tf_Stock_det2 w, "
			SQL = SQL & "emi_material_requisition_det_convert m, emi_material_requisition n where "
			SQL = SQL & "x.kode_Perusahaan = y.kode_perusahaan And x.no_faktur = y.no_faktur And x.status Is null And "
			SQL = SQL & "y.kode_Perusahaan = z.kode_perusahaan And y.no_faktur = z.no_faktur And y.urut_oto = z.urut_tf And (z.selesai Is null Or z.selesai ='Y') and "
			SQL = SQL & "z.kode_Perusahaan = w.kode_perusahaan And z.no_faktur = w.no_faktur And z.urut_oto = w.Urut_Det And "
			SQL = SQL & "m.Kode_Perusahaan = y.Kode_Perusahaan And m.Urut_Oto = y.urut_material_requisition_convert and "
			SQL = SQL & "y.Flag_Jenis_Request = 'PRODUKSI' and "
			SQL = SQL & "m.kode_Perusahaan = n.kode_perusahaan And m.no_faktur = n.no_faktur and n.status is null and "
			SQL = SQL & "a.Kode_Perusahaan = m.Kode_Perusahaan and a.Kode_Stock_Owner = m.Kode_Stock_Owner and a.Kode_Barang = m.Kode_Barang "
			SQL = SQL & "and a.Kode_Perusahaan = n.Kode_Perusahaan and a.No_Faktur = n.No_Faktur_Order and n.Status is null "
			SQL = SQL & "), '0') as Total_TF "

			SQL = SQL & "from Emi_Split_Production_Order_Detail_Bahan a, Barang b, EMI_Kategori_Gudang_PerLokasi c, Stock_Owner_Gudang d "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
			SQL = SQL & "and b.Kode_Perusahaan = c.kode_perusahaan and b.ID_Kategori_Gudang = c.Id_Kategori_Gudang "
			SQL = SQL & "and c.kode_perusahaan = d.kode_Perusahaan and c.lokasi_gudang = d.kode_Stock_owner "
			'SQL = SQL & "and d.Flag_QC is null "
			SQL = SQL & "and a.kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Faktur='" & Txt_No_Split.Text & "' "

			SQL = SQL & "union all "

			SQL = SQL & "select a.No_Faktur, a.Kode_Stock_Owner, a.kode_barang, b.Nama, a.Jumlah, a.Satuan, a.Nilai_Barang, a.Satuan_Barang, 'Packaging' as tipe, c.lokasi_gudang, "
			SQL = SQL & "(ISNULL( "
			SQL = SQL & "(select sum(z.jumlah) "
			SQL = SQL & "from Emi_Material_Requisition_det z, Emi_Material_Requisition x "
			SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan and a.Kode_Stock_Owner =z.Kode_Stock_Owner and a.Kode_Barang = z.Kode_Barang "
			SQL = SQL & "and z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur and a.No_Faktur = x.No_Faktur_Order and x.status is null), 0)) as Jumlah_Diproduksi, " 'JUMLAH DI PRODUKSI

			SQL = SQL & "(a.Jumlah - ISNULL( "
			SQL = SQL & "(select sum(z.jumlah) "
			SQL = SQL & "from Emi_Material_Requisition_det z, Emi_Material_Requisition x "
			SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan and a.Kode_Stock_Owner =z.Kode_Stock_Owner and a.Kode_Barang = z.Kode_Barang "
			SQL = SQL & "and z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur and a.No_Faktur = x.No_Faktur_Order and x.status is null), 0)) as sisa, " 'SISA

			SQL = SQL & "'PACKAGING' as Jenis_Bahan, " ' JENIS BAHAN

			SQL = SQL & "ISNULL(( select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang, a.Satuan_Barang, a.Satuan, sum(z.Jumlah)) "
			SQL = SQL & "from Barang_SN z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_Stock_Owner = a.Kode_Stock_Owner and z.Kode_Barang = a.Kode_Barang "
			SQL = SQL & "), 0) as Stock_Gudang_Produksi, " ' Stock_Gudang_Produksi

			SQL = SQL & "ISNULL((select sum(w.jumlah) from tf_stock_parent x, Tf_Stock y, Tf_Stock_det z, Tf_Stock_det2 w, "
			SQL = SQL & "emi_material_requisition_det_convert m, emi_material_requisition n where "
			SQL = SQL & "x.kode_Perusahaan = y.kode_perusahaan And x.no_faktur = y.no_faktur And x.status Is null And "
			SQL = SQL & "y.kode_Perusahaan = z.kode_perusahaan And y.no_faktur = z.no_faktur And y.urut_oto = z.urut_tf And (z.selesai Is null Or z.selesai ='Y') and "
			SQL = SQL & "z.kode_Perusahaan = w.kode_perusahaan And z.no_faktur = w.no_faktur And z.urut_oto = w.Urut_Det And "
			SQL = SQL & "m.Kode_Perusahaan = y.Kode_Perusahaan And m.Urut_Oto = y.urut_material_requisition_convert and "
			SQL = SQL & "y.Flag_Jenis_Request = 'PRODUKSI' and "
			SQL = SQL & "m.kode_Perusahaan = n.kode_perusahaan And m.no_faktur = n.no_faktur and n.status is null and "
			SQL = SQL & "a.Kode_Perusahaan = m.Kode_Perusahaan and a.Kode_Stock_Owner = m.Kode_Stock_Owner and a.Kode_Barang = m.Kode_Barang "
			SQL = SQL & "and a.Kode_Perusahaan = n.Kode_Perusahaan and a.No_Faktur = n.No_Faktur_Order and n.status is null "
			SQL = SQL & "), '0') as Total_TF "

			SQL = SQL & "from Emi_Split_Production_Order_Detail_Packaging a, Barang b, EMI_Kategori_Gudang_PerLokasi c, Stock_Owner_Gudang d "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
			SQL = SQL & "and b.Kode_Perusahaan = c.kode_perusahaan and b.ID_Kategori_Gudang = c.Id_Kategori_Gudang "
			SQL = SQL & "and c.kode_perusahaan = d.kode_Perusahaan and c.lokasi_gudang = d.kode_Stock_owner "
			'SQL = SQL & "and d.Flag_QC is null "
			SQL = SQL & "and a.kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Faktur='" & Txt_No_Split.Text & "' "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							Dgv_Data.Rows.Add(1)
							Dgv_Data.Rows(i).Cells(cell_NoFak).Value = .Rows(i).Item("No_Faktur") '0
							Dgv_Data.Rows(i).Cells(cell_Kd_SO).Value = .Rows(i).Item("Kode_Stock_Owner") '1
							Dgv_Data.Rows(i).Cells(cell_Kd_Barang).Value = .Rows(i).Item("Kode_Barang") '2
							Dgv_Data.Rows(i).Cells(cell_JumlahKebutuhan).Value = Format(Val(.Rows(i).Item("Jumlah")), "N4") '3
							Dgv_Data.Rows(i).Cells(cell_JumlahDiproduksi).Value = Format(Val(.Rows(i).Item("Jumlah_Diproduksi")), "N4") '4
							Dgv_Data.Rows(i).Cells(cell_Sisa).Value = Format(Val(.Rows(i).Item("Sisa")), "N4") '5
							Dgv_Data.Rows(i).Cells(cell_SatuanBesar).Value = .Rows(i).Item("Satuan") '6
							Dgv_Data.Rows(i).Cells(cell_SatuanKecil).Value = .Rows(i).Item("Satuan_Barang") '7
							Dgv_Data.Rows(i).Cells(cell_Tipe).Value = .Rows(i).Item("tipe") '8
							Dgv_Data.Rows(i).Cells(cell_warna).Value = "Hijau" '8
							Dgv_Data.Rows(i).Cells(cell_JenisBahan).Value = .Rows(i).Item("Jenis_Bahan") '9

							Dgv_Data.Rows(i).Cells(cell_StockProduksi).Value = Format(Val(.Rows(i).Item("Stock_Gudang_Produksi")), "N4") '10
							Dgv_Data.Rows(i).Cells(cell_TotalTransfer).Value = Format(Val(.Rows(i).Item("Total_TF")), "N4") '11
							Dgv_Data.Rows(i).Cells(cell_LokasiTujuan).Value = .Rows(i).Item("lokasi_gudang") '12
							Dgv_Data.Rows(i).Cells(cell_Nama_Barang).Value = .Rows(i).Item("Nama") '13

							Dgv_Data.Rows(i).Cells(cell_JumlahInput).Style.BackColor = Color.LightGray
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

	Private Sub Get_Total_Request()
		If Dgv_Data.RowCount = 0 Then Exit Sub

		Dim total As Double = 0
		For i As Integer = 0 To Dgv_Data.RowCount - 1

			Get_DGV_Items(i)

			If Dgv_JmlhInput = "" Then
				total = total + 0
			Else
				total = total + Dgv_JmlhInput
			End If

		Next

		TxtTotalRequest.Text = Format(total, "N4")
	End Sub

	Private Sub Dgv_Data_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data.CellEndEdit

		If Dgv_Data.RowCount = 0 Then Exit Sub

		If Not IsNumeric(Dgv_Data.CurrentRow.Cells(cell_JumlahInput).Value) Then
			Dgv_Data.CurrentRow.Cells(cell_JumlahInput).Value = ""
			Get_Total_Request()
			Exit Sub
		End If

		'If Val(HilangkanTanda(Dgv_Data.CurrentRow.Cells(cell_JumlahInput).Value)) > Val(HilangkanTanda(Dgv_Data.CurrentRow.Cells(cell_Sisa).Value)) Then
		'    MessageBox.Show("Jumlah Tidak Boleh Lebih Dari Sisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'    Dgv_Data.CurrentRow.Cells(cell_JumlahInput).Value = ""
		'    Get_Total_Request()
		'    Exit Sub
		'End If

		'======================
		'=     SET FORMAT     =
		'======================

		If Dgv_Data.CurrentCell.ColumnIndex = cell_JumlahInput Then

			Dim cellKuantity As String = Dgv_Data.CurrentCell.Value

			If cellKuantity.Contains(",") Then
				MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Dgv_Data.CurrentRow.Cells(cell_JumlahInput).Value = Format(0, "N4")
				Get_Total_Request()
				Exit Sub
			End If

			Dim nilai As Decimal = Decimal.Parse(cellKuantity)
			Dim formattedValue As String = nilai.ToString("N4", Globalization.CultureInfo.GetCultureInfo("en-us"))

			Dgv_Data.CurrentRow.Cells(cell_JumlahInput).Value = formattedValue
		End If

		Get_Total_Request()
	End Sub

	Private Sub Dgv_Data_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data.CellEnter
		'======================
		'=     SET FORMAT     =
		'======================

		If Dgv_Data.CurrentCell.ColumnIndex = cell_JumlahInput Then
			Dim cellKuantity As String = Dgv_Data.CurrentCell.Value

			If cellKuantity = "" Then
				Exit Sub
			End If

			Dim cleanedStr As String = HilangkanTanda(cellKuantity) ' Menghapus titik
			Dim nilai As Decimal = Decimal.Parse(cleanedStr)

			Dgv_Data.CurrentCell.Value = nilai
		End If
	End Sub

	Private Sub Dgv_Data_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data.CellLeave
		'======================
		'=     SET FORMAT     =
		'======================

		If Dgv_Data.CurrentCell.ColumnIndex = cell_JumlahInput Then
			Dim cellKuantity As String = Dgv_Data.CurrentCell.Value

			If cellKuantity = "" Then
				Exit Sub
			End If

			Dim nilai As Decimal = Decimal.Parse(cellKuantity)
			Dim formattedValue As String = nilai.ToString("N4", Globalization.CultureInfo.GetCultureInfo("en-us"))

			Dgv_Data.CurrentCell.Value = formattedValue

		End If
	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		Kosong()
	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If Dgv_Data.Rows.Count = 0 Then
			MessageBox.Show("Tidak Ada Data yang Dapat Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		ElseIf Cmb_Batch.SelectedIndex = -1 Then
			MessageBox.Show("Batch Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Batch.Focus() : Cmb_Batch.DroppedDown = True : Exit Sub
		ElseIf TxtKeterangan.Text.Trim.Length = 0 Then
			MessageBox.Show("Keterangan Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TxtKeterangan.Focus() : Exit Sub
		End If

		'============================
		'=     CEK DATAGRIDVIEW     =
		'============================
		Dim hasData As Boolean = False
		For i As Integer = 0 To Dgv_Data.RowCount - 1
			Get_DGV_Items(i)
			If String.IsNullOrWhiteSpace(Dgv_JmlhInput) Then
				Continue For
			Else
				hasData = True
				Exit For
			End If
		Next

		If Not hasData Then
			MessageBox.Show("Tidak Ada Jumlah yang Diinput", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		get_jam()
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			get_no_faktur()

			'=============================
			'=     GET ID GROUP JENIS    =
			'=============================
			Dim Id_Group_Jenis As String
			SQL = "select Id_Group_Jenis from barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & kd_so & "' and Kode_Barang='" & Txt_Kd_Barang.Text & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Id_Group_Jenis = Dr("Id_Group_Jenis")
				Else
					CloseTrans()
					CloseConn()
					MessageBox.Show("Jenis Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

			End Using

			'=========================
			'=     INSERT PARENT     =
			'=========================
			SQL = "insert into Emi_Material_Requisition (Kode_Perusahaan, No_Faktur, No_Faktur_Order, Kode_Stock_Owner, Kode_Barang, Id_Group_Jenis, Tanggal, Jam, Flag_Process, UserId, Status, Keterangan, Lokasi, "
			SQL = SQL & "flag_tambah, Batch) values "
			SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial.Text & "', '" & Txt_No_Split.Text & "', "
			SQL = SQL & "'" & kd_so & "', '" & Txt_Kd_Barang.Text & "', '" & Id_Group_Jenis & "', "
			SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', 'Y', '" & UserID & "', NULL, '" & TxtKeterangan.Text & "', '" & Txt_Lokasi.Text & "', "
			SQL = SQL & "'Y', " & Cmb_Batch.Text & ")"
			ExecuteTrans(SQL)

			For i As Integer = 0 To Dgv_Data.RowCount - 1
				Get_DGV_Items(i)

				If Dgv_JmlhInput = "" Or Val(Dgv_JmlhInput) = 0 Then
					Continue For
				End If
				'================================
				'=     CONVERT SATUAN KECIL     =
				'================================
				Dim nilai_kecil As Double = 0
				SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & Dgv_KdBarang & "', '" & Dgv_SatuanBesar & "',"
				SQL = SQL & "'" & Dgv_SatuanKecil & "', '" & HilangkanTanda(Dgv_JmlhInput) & "' ) as hasil"
				Using Dr1 = OpenTrans(SQL)
					If Dr1.Read Then
						If General_Class.CekNULL(Dr1("hasil")) = "" Then
							Dr1.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("data konversi satuan kirim tidak ada ")
							Exit Sub
						End If

						nilai_kecil = Val(HilangkanTanda(Format(Dr1("hasil"), "N4")))
					Else
						Dr1.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("data konversi satuan kirim tidak ada ")
						Exit Sub
					End If
				End Using

				'==============================
				'=     INSERT TABEL DET     =
				'==============================

				SQL = "insert into Emi_Material_Requisition_det (Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Stock_Owner_Tujuan, Kode_Barang, Kebutuhan, Jumlah, Satuan, Jumlah_Barang, Satuan_Barang, Jenis_Material) values "
				SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial.Text & "', '" & Dgv_KdSo & "', '" & Dgv_Lokasi_Tujuan & "', '" & Dgv_KdBarang & "', '" & HilangkanTanda(Dgv_JmlhKebutuhan) & "',  "

				If Dgv_JmlhInput = "" Then
					SQL = SQL & "'0', "
				Else
					SQL = SQL & "'" & HilangkanTanda(Dgv_JmlhInput) & "', "
				End If

				SQL = SQL & "'" & Dgv_SatuanBesar & "', '" & nilai_kecil & "', '" & Dgv_SatuanKecil & "', '" & Dgv_Tipe & "')"
				ExecuteTrans(SQL)

				Dim x_ident_currentPackaging As Integer = 0
				SQL = "select IDENT_CURRENT('Emi_Material_Requisition_det') as urutan"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						x_ident_currentPackaging = Dr("urutan")
					End If
				End Using

				SQL = "insert into Emi_Material_Requisition_det_convert(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Jumlah_Barang,Satuan_Barang,Warna,No_Urut_Det)"
				SQL = SQL & "values("
				SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial.Text & "', '" & Dgv_KdSo & "', '" & Dgv_KdBarang & "', "
				If Dgv_JmlhInput = "" Then
					SQL = SQL & "'0', "
				Else
					SQL = SQL & "'" & HilangkanTanda(Dgv_JmlhInput) & "', "
				End If
				SQL = SQL & "'" & Dgv_SatuanBesar & "', '" & nilai_kecil & "', '" & Dgv_SatuanKecil & "', '" & Dgv_Warna & "', '" & x_ident_currentPackaging & "')"
				ExecuteTrans(SQL)

				'======================================
				'=     CEK APAKAH BAHAN TERPENUHI     =
				'======================================
				If Dgv_JenisBahan = "BAHAN" Then

					SQL = "select "
					SQL = SQL & "(a.jumlah - ISNULL(( "
					SQL = SQL & "select sum(x.Jumlah) "
					SQL = SQL & "from Emi_Material_Requisition z, Emi_Material_Requisition_det x "
					SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
					SQL = SQL & "and z.No_Faktur = x.No_Faktur "
					SQL = SQL & "and a.No_Faktur = z.No_Faktur_Order "
					SQL = SQL & "and a.Kode_Stock_Owner = x.Kode_Stock_Owner and a.Kode_Barang = x.Kode_Barang "
					SQL = SQL & "), 0)) as Sisa "
					SQL = SQL & "from Emi_Split_Production_Order_Detail_Bahan a "
					SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
					SQL = SQL & "and a.No_Faktur = '" & Txt_No_Split.Text & "' "
					SQL = SQL & "and a.Kode_Barang = '" & Dgv_KdBarang & "' "
					Using Ds = BindingTrans(SQL)
						With Ds.Tables("MyTable")
							If .Rows.Count <> 0 Then

								Dim cekDataDouble As Integer = 0
								For j As Integer = 0 To .Rows.Count - 1
									cekDataDouble = cekDataDouble + 1

									If cekDataDouble > 1 Then
										CloseTrans()
										CloseConn()
										MessageBox.Show("Terjadi Kesalahan Saat Cek Sisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If

									If Val(.Rows(j).Item("Sisa")) = 0 Then

										SQL = "update Emi_Split_Production_Order_Detail_Bahan set Flag_Terpenuhi =  'Y' where kode_perusahaan = '" & KodePerusahaan & "' "
										SQL = SQL & "and No_Faktur = '" & Txt_No_Split.Text & "' and Kode_Stock_Owner = '" & kd_so & "' and Kode_Barang = '" & Dgv_KdBarang & "'"
										ExecuteTrans(SQL)

									End If
								Next
							End If
						End With
					End Using

				ElseIf Dgv_JenisBahan = "PACKAGING" Then

					SQL = "select "
					SQL = SQL & "(a.jumlah - ISNULL(( "
					SQL = SQL & "select sum(x.Jumlah) "
					SQL = SQL & "from Emi_Material_Requisition z, Emi_Material_Requisition_det x "
					SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
					SQL = SQL & "and z.No_Faktur = x.No_Faktur "
					SQL = SQL & "and a.No_Faktur = z.No_Faktur_Order "
					SQL = SQL & "and a.Kode_Stock_Owner = x.Kode_Stock_Owner and a.Kode_Barang = x.Kode_Barang "
					SQL = SQL & "), 0)) as Sisa "
					SQL = SQL & "from Emi_Split_Production_Order_Detail_Packaging a "
					SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
					SQL = SQL & "and a.No_Faktur = '" & Txt_No_Split.Text & "' "
					SQL = SQL & "and a.Kode_Barang = '" & Dgv_KdBarang & "' "
					Using Ds = BindingTrans(SQL)
						With Ds.Tables("MyTable")
							If .Rows.Count <> 0 Then

								Dim cekDataDouble As Integer = 0
								For j As Integer = 0 To .Rows.Count - 1
									cekDataDouble = cekDataDouble + 1

									If cekDataDouble > 1 Then
										CloseTrans()
										CloseConn()
										MessageBox.Show("Terjadi Kesalahan Saat Cek Sisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If

									If Val(.Rows(j).Item("Sisa")) = 0 Then

										SQL = "update Emi_Split_Production_Order_Detail_Packaging set Flag_Terpenuhi =  'Y' where kode_perusahaan = '" & KodePerusahaan & "' "
										SQL = SQL & "and No_Faktur = '" & Txt_No_Split.Text & "' and Kode_Stock_Owner = '" & Dgv_KdSo & "' and Kode_Barang = '" & Dgv_KdBarang & "'"
										ExecuteTrans(SQL)

									End If
								Next
							End If
						End With
					End Using

				End If

			Next

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Request Tambahan Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Me.Close()

	End Sub

End Class