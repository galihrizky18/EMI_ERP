Imports System.Text

Public Class EMI_Pengeluaran_Stock_Barang_Lain
	Public Property asal As String
	Public Property MenuAsal As String

	Dim arrSO, arrInisialFaktur, arrIdWMSWarehouse, WarehosePosition, arrJenisPengeluaran, arr_id_keterangan, arr_kode_akun As New ArrayList
	Private random As New Random()

	Dim arr2RakTujuan As New List(Of List(Of String))

	Dim lv_DetKodeSO, lv_DetKodeBarang, lv_DetNamaBarang, lv_DetGoodStock, lv_DetSatuan, lv_DetSatuanDIsplay, lv_DetJmlhBags, lv_DetSatuanBags, lv_DetMetPotStock, lv_DetJenisBags As String

	Dim dgv_Lokasi, dgv_KodeBarang, dgv_SerialNumber, dgv_Nama, dgv_IDWareHouse, dgv_KodeRak As String
	Dim dgv_IDPallet, dgv_GoodStock, dgv_Satuan, dgv_Jumlah, dgv_RakTujuan, dgv_IDWarehouseTujuan, dgv_JmlhBags, dgv_Warna, dgv_JenisKemasan As String
	Dim dgv_IsiPerBags, dgv_SatuanIsiBags, dgv_TglProd, dgv_TglExp, dgv_KetWarna, dgv_Barcode, dgv_FlagBlokSN As String
	Dim dgv_CheckBox As Boolean

	Dim dgv_Rekap_lokasi, dgv_Rekap_KdBarang, dgv_Rekap_NmBarang, dgv_Rekap_Jumlah, dgv_Rekap_JumlahBags, dgv_Rekap_JumlahBersih, dgv_Rekap_Satuan, dgv_Rekap_SatuanKecil, dgv_Rekap_Oto As String

	Dim dgv_detail_lokasi, dgv_detail_KdBarang, dgv_detail_SN, dgv_detail_NmBarang, dgv_detail_IDWarehouseAwal, dgv_detail_KodeRakAwal As String
	Dim dgv_detail_IDPalletAwal, dgv_detail_Jumlah, dgv_detail_JumlahBags, dgv_detail_KodeRakTujuan, dgv_detail_IDWarehouseTujuan As String
	Dim dgv_detail_Warna, dgv_detail_JenisKemasan, dgv_detail_TglProduksi, dgv_detail_TglExpired, dgv_detail_JenisKualitas, dgv_detail_Barcode As String

	Dim kd_barang As String

	Dim TotalTransfer As Double = 0

	Dim itemDetKodeSO As Integer = 0
	Dim itemDetKodeBarang As Integer = 1
	Dim itemDetNamaBarang As Integer = 2
	Dim itemDetGoodStock As Integer = 3
	Dim itemDetSatuan As Integer = 4
	Dim itemDetSatuandisplay As Integer = 5
	Dim itemDetJmlhBags As Integer = 6
	Dim itemDetSatuanBags As Integer = 7
	Dim itemDetMetPotStock As Integer = 8
	Dim itemDetJenisBags As Integer = 9

	Dim itemDgvLokasi As Integer = 0
	Dim itemDgvKodeBarang As Integer = 1
	Dim itemDgvSerialNumber As Integer = 2
	Dim itemDgvNama As Integer = 3
	Dim itemDgvIDWareHose As Integer = 4
	Dim itemDgvKodeRak As Integer = 5
	Dim itemDgvIDPallet As Integer = 6
	Dim itemDgvGoodStock As Integer = 7
	Dim itemDgvSatuan As Integer = 8
	Dim itemDgvStockBags As Integer = 9
	Dim itemDgvCheckBox As Integer = 10
	Dim itemDgvJumlah As Integer = 11
	Dim itemDgvBags As Integer = 12
	Dim itemDgvRakTujuan As Integer = 13
	Dim itemIDWarehouseTujuan As Integer = 14
	Dim itemDgvWarna As Integer = 15
	Dim itemJenisKemasan As Integer = 16
	Dim itemDGVIsiPerBags As Integer = 17
	Dim itemDGVSatuanIsiBags As Integer = 18
	Dim itemDGVTglProd As Integer = 19
	Dim itemDGVTglExp As Integer = 20
	Dim itemDGVKetWarna As Integer = 21
	Dim itemDGVBarcode As Integer = 22
	Dim itemDGVFlagBlokSN As Integer = 23

	'Tab 2
	Dim itemDgvRekap_lokasi As Integer = 0

	Dim itemDgvRekap_KdBarang As Integer = 1
	Dim itemDgvRekap_NmBarang As Integer = 2
	Dim itemDgvRekap_Jumlah As Integer = 3
	Dim itemDgvRekap_JumlahBags As Integer = 4
	Dim itemDgvRekap_JumlahBersih As Integer = 5
	Dim itemDgvRekap_Satuan As Integer = 6
	Dim itemDgvRekap_SatuanKecil As Integer = 7

	Private Sub TxtKeterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKeterangan.KeyPress
		If e.KeyChar = Chr(13) Then
			CmbSO_Asal.Focus()
		End If
	End Sub

	Private Sub CmbSO_Asal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbSO_Asal.KeyPress
		If e.KeyChar = Chr(13) Then
			TxtKodeCost.Focus()
		End If
	End Sub

	Private Sub TxtKd_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtKd_Barang.KeyDown
		If e.KeyCode = Keys.Down Then Lv_DetBarang.Focus()
	End Sub

	Private Sub TxtKd_Barang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKd_Barang.KeyPress
		If e.KeyChar = Chr(13) Then
			If TxtKd_Barang.Text.Trim.Length = 0 Then
				Lv_DetBarang.Visible = False : TxtKd_Barang.Focus() : Exit Sub
			End If
			' TxtKd_Barang_Leave(TxtKd_Barang, e)
		End If
	End Sub

	Private Sub Lv_DetBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_DetBarang.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_DetBarang_DoubleClick(Lv_DetBarang, e)
		End If
	End Sub

	Private Sub Btn_Scan_Click(sender As Object, e As EventArgs) Handles Btn_Scan.Click
		If Txt_QR.Text.Trim.Length = 0 Then
			MessageBox.Show("Harap isi dahulu QR Codenya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_QR.Focus()
			Exit Sub
		End If

		If DGV_Data_TF.Rows.Count = 0 Then Exit Sub

		Dim foundMatch As Boolean = False
		Dim keyword As String = Txt_QR.Text.Trim().ToUpper()

		For i As Integer = 0 To DGV_Data_TF.Rows.Count - 1
			get_grid_view(i)
			If dgv_FlagBlokSN <> "Y" Then
				DGV_Data_TF.Rows(i).DefaultCellStyle.BackColor = Color.White
				DGV_Data_TF.Rows(i).DefaultCellStyle.ForeColor = Color.Black
			End If
		Next

		For i As Integer = 0 To DGV_Data_TF.Rows.Count - 1
			get_grid_view(i)

			If dgv_Barcode.Trim().ToUpper().Contains(keyword) Then

				'DGV_Data_TF.Rows(i).Cells(itemDgvCheckBox).Value = "True"
				DGV_Data_TF.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
				DGV_Data_TF.Rows(i).DefaultCellStyle.ForeColor = Color.Black

				If Not foundMatch Then
					DGV_Data_TF.FirstDisplayedScrollingRowIndex = i ' Scroll ke hasil pertama
				End If

				foundMatch = True

			End If

		Next

		If Not foundMatch Then

			Try
				OpenConn()

				Dim rows As Integer = DGV_Data_TF.Rows.Count

				SQL = "Select top(1) a.Kode_Stock_Owner, a.Kode_Barang, a.Serial_Number, b.Nama, "
				SQL = SQL & " a.Id_Warehouse, c.Keterangan As kode_rak, a.Id_Nametag_pallet, "

				SQL = SQL & " dbo.ubah_satuan_lain(a.kode_Perusahaan, 'masa', a.kode_barang, b.satuan, '" & TxtSatuan.Text & "', a.jumlah) as jumlah, "

				SQL = SQL & " b.satuan, a.nomor_pallet, isNull(a.Jumlah_Bags, 0) As stock_bags, a.warna, b.Metode_Pengeluaran_Stok, "
				SQL = SQL & " b.Jenis_Kemasan, 1 as Isi_Per_Bags, '-' as Satuan_Isi_Bags, a.Tgl_Expired, a.Tgl_Produksi "

				SQL = SQL & ",isNull((select x.keterangan from emi_master_warna x where "
				SQL = SQL & "x.kode_Perusahaan = a.kode_Perusahaan And x.kode_warna = a.warna),NULL) As Ket_Warna, "

				SQL = SQL & "(a.Qr_Code + '-' + a.Kode_Unik_Berjalan) as Barcode, a.Blok_SN "

				SQL = SQL & " From Barang_Lain_SN a, barang_lain b, View_Warehouse_Position_Barang_Lain c "
				SQL = SQL & " Where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Barang = b.Kode_Barang And "
				SQL = SQL & " a.Kode_Stock_Owner = b.Kode_Stock_Owner And a.Kode_Perusahaan = c.Kode_Perusahaan And "
				SQL = SQL & " a.Id_Warehouse = c.Id_WMS_Warehouse_Position And "

				SQL = SQL & " a.Kode_Perusahaan ='" & KodePerusahaan & "' "
				SQL = SQL & " And b.Kode_Stock_Owner ='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
				SQL = SQL & " And b.Kode_Barang='" & TxtKd_Barang.Text & "' "

				SQL = SQL & "And round(a.Jumlah,4) <> 0 and (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) like '%" & Txt_QR.Text.Trim().ToUpper() & "%'"

				SQL = SQL & " order by case "
				SQL = SQL & " when Metode_Pengeluaran_Stok='FIFO' then a.Tgl_Masuk "
				SQL = SQL & " Else a.Tgl_Expired End "

				Using Dr = OpenTrans(SQL)
					Do While Dr.Read

						Dim subArr As New List(Of String)

						DGV_Data_TF.Rows.Add(1)
						DGV_Data_TF.Rows(rows).Cells(itemDgvLokasi).Value = General_Class.CekNULL(Dr("Kode_Stock_Owner"))
						DGV_Data_TF.Rows(rows).Cells(itemDgvKodeBarang).Value = General_Class.CekNULL(Dr("Kode_Barang"))
						DGV_Data_TF.Rows(rows).Cells(itemDgvSerialNumber).Value = General_Class.CekNULL(Dr("Serial_Number"))
						DGV_Data_TF.Rows(rows).Cells(itemDgvNama).Value = General_Class.CekNULL(Dr("Nama"))
						DGV_Data_TF.Rows(rows).Cells(itemDgvIDWareHose).Value = General_Class.CekNULL(Dr("Id_Warehouse"))
						DGV_Data_TF.Rows(rows).Cells(itemDgvKodeRak).Value = General_Class.CekNULL(Dr("kode_rak"))
						DGV_Data_TF.Rows(rows).Cells(itemDgvIDPallet).Value = General_Class.CekNULL(Dr("nomor_pallet"))
						DGV_Data_TF.Rows(rows).Cells(itemDgvGoodStock).Value = If(General_Class.CekNULL(Dr("jumlah")) = "", "", Format(Dr("jumlah"), "N2"))
						DGV_Data_TF.Rows(rows).Cells(itemDgvStockBags).Value = If(General_Class.CekNULL(Dr("stock_bags")) = "", "", Format(Dr("stock_bags"), "N0"))
						DGV_Data_TF.Rows(rows).Cells(itemDgvWarna).Value = General_Class.CekNULL(Dr("warna"))
						DGV_Data_TF.Rows(rows).Cells(itemJenisKemasan).Value = General_Class.CekNULL(Dr("Jenis_Kemasan"))
						DGV_Data_TF.Rows(rows).Cells(itemDGVIsiPerBags).Value = General_Class.CekNULL(Dr("Isi_Per_Bags"))
						DGV_Data_TF.Rows(rows).Cells(itemDGVSatuanIsiBags).Value = General_Class.CekNULL(Dr("Satuan_Isi_Bags"))
						DGV_Data_TF.Rows(rows).Cells(itemDGVKetWarna).Value = General_Class.CekNULL(Dr("Ket_Warna"))
						DGV_Data_TF.Rows(rows).Cells(itemDGVTglProd).Value = If(General_Class.CekNULL(Dr("Tgl_Produksi")) = "", "", Format(Dr("Tgl_Produksi"), "dd MMM yyyy"))
						DGV_Data_TF.Rows(rows).Cells(itemDgvSatuan).Value = TxtSatuan.Text

						Dim dgvCmbValueRak As DataGridViewComboBoxCell
						dgvCmbValueRak = DGV_Data_TF.Rows(rows).Cells(itemDgvRakTujuan)
						dgvCmbValueRak.Items.Clear()

						'dgvCmbValueRak.Items.Add("-- Tidak Berubah --") : subArr.Add(Dr("Id_Warehouse"))
						For i As Integer = 0 To WarehosePosition.Count - 1
							dgvCmbValueRak.Items.Add(WarehosePosition(i)) : subArr.Add(arrIdWMSWarehouse(i))
						Next

						arr2RakTujuan.Add(subArr)

						DGV_Data_TF.Rows(rows).Cells(itemDgvJumlah).ReadOnly = True
						DGV_Data_TF.Rows(rows).Cells(itemDgvBags).ReadOnly = True
						DGV_Data_TF.Rows(rows).Cells(itemDgvRakTujuan).ReadOnly = True

						If Dr("Jenis_Kemasan").ToString.ToUpper = "ORIGINAL BAGS" Then
							DGV_Data_TF.Rows(rows).Cells(itemDgvJumlah).ReadOnly = True
						End If

						If Dr("Metode_Pengeluaran_Stok").ToString.ToUpper = "FIFO" Then
							DGV_Data_TF.Rows(rows).Cells(itemDGVTglExp).Value = "-"
						Else
							DGV_Data_TF.Rows(rows).Cells(itemDGVTglExp).Value = If(General_Class.CekNULL(Dr("Tgl_Expired")) = "", "", Format(Dr("Tgl_Expired"), "dd MMM yyyy"))
						End If

						DGV_Data_TF.Rows(rows).Cells(itemDGVBarcode).Value = Dr("Barcode")
						DGV_Data_TF.Rows(rows).Cells(itemDGVFlagBlokSN).Value = General_Class.CekNULL(Dr("Blok_SN"))

						rows = rows + 1

					Loop
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try

		End If

		Txt_QR.Text = ""

	End Sub

	Private Sub Txt_QR_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_QR.KeyPress
		If e.KeyChar = Chr(13) Then
			Btn_Scan_Click(e, New EventArgs)
			DGV_Data_TF.Focus()
		End If
	End Sub

	'Private Sub TxtKd_Barang_Leave(sender As Object, e As EventArgs) Handles TxtKd_Barang.Leave

	'    ''

	'    If asal <> "Emi_Display_Request_Material" Then
	'        'If TxtKd_Barang.Text.Trim.Length = 0 Then Exit Sub
	'        If CmbJnsTransfer.SelectedIndex = -1 Then Exit Sub
	'        If CmbJnsTransfer.SelectedIndex = 0 Then
	'            If CmbSO_Asal.SelectedIndex = -1 Then Exit Sub

	'        ElseIf CmbJnsTransfer.SelectedIndex = 1 Then
	'            If CmbSO_Asal.SelectedIndex = -1 Then Exit Sub
	'            If CmbSo_Tujuan.SelectedIndex = -1 Then Exit Sub
	'        End If

	'        If Not TxtKd_Barang.Text.Trim.Count = 0 Then
	'            Lv_DetBarang.Location = New Point(37, 277)
	'            Lv_DetBarang.Visible = True
	'        Else
	'            Lv_DetBarang.Location = New Point(1278, 277)
	'            Lv_DetBarang.Visible = False
	'        End If

	'        Try
	'            OpenConn()
	'            get_no_faktur()

	'            Lv_DetBarang.Items.Clear()

	'            SQL = "select a.kode_stock_owner, a.kode_barang, a.nama, dbo.ubah_satuan_lain(a.kode_Perusahaan, 'masa', a.kode_barang, a.satuan, "
	'            SQL = SQL & "b.satuan, a.good_stock) as Good_Stock, a.Satuan, b.satuan as satuan_display, ISNULL(a.Jumlah_Bags, 0) as Jumlah_Bags, "
	'            SQL = SQL & "a.Metode_Pengeluaran_Stok, a.Jenis_Kemasan from barang_lain a, Barang_Detail_Satuan_Lain b "
	'            SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' and a.Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
	'            SQL = SQL & "and a.kode_barang like '" & TxtKd_Barang.Text & "' and a.Kode_Barang=b.kode_barang "
	'            SQL = SQL & "And a.kode_Perusahaan = b.kode_Perusahaan And b.flag_tampil_display ='Y'  "
	'            SQL = SQL & "order by a.Kode_Barang "
	'            Using Dr = OpenTrans(SQL)
	'                Do While Dr.Read
	'                    Dim Lv As New ListViewItem

	'                    Lv = Lv_DetBarang.Items.Add(General_Class.CekNULL(Dr("kode_stock_owner")))
	'                    Lv.SubItems.Add(General_Class.CekNULL(Dr("kode_barang")))
	'                    Lv.SubItems.Add("X")
	'                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Good_Stock")) = "", "", Format(Dr("Good_Stock"), "N2")))
	'                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Satuan")))
	'                    Lv.SubItems.Add(General_Class.CekNULL(Dr("satuan_display")))
	'                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jumlah_Bags")) = "", "", Format(Dr("Jumlah_Bags"), "N0")))
	'                    Lv.SubItems.Add("-") 'satuan perbags tidak dipakai lagi
	'                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Metode_Pengeluaran_Stok")))
	'                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Jenis_Kemasan")))

	'                    '================================================
	'                    '=     CEK APAKAH DATA MASTER SUDAH LENGKAP     =
	'                    '================================================
	'                    Dim TempArray As New ArrayList

	'                    TempArray.Clear()
	'                    For i As Integer = 0 To Lv_DetBarang.Columns.Count - 1
	'                        Dim indexx As Integer = Lv_DetBarang.FocusedItem.Index
	'                        Dim data As String = Lv_DetBarang.Items(indexx).SubItems(i).Text

	'                        TempArray.Add(data)
	'                    Next

	'                    If TempArray.Contains("") Then
	'                        MessageBox.Show("Data Master Terhadap Barang Masih Belum Lengkap", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'                        Exit Sub
	'                    End If

	'                    CmbJnsTransfer.Enabled = False
	'                    CmbSO_Asal.Enabled = False
	'                    CmbSo_Tujuan.Enabled = False

	'                    TxtKd_Barang.Text = String.Empty
	'                    Txt_SO.Text = String.Empty
	'                    TxtNm_Barang.Text = String.Empty

	'                    TxtKd_Barang.Text = lv_DetKodeBarang
	'                    Txt_SO.Text = lv_DetKodeSO
	'                    TxtNm_Barang.Text = lv_DetNamaBarang
	'                    TxtSatuan.Text = lv_DetSatuanDIsplay
	'                    TxtSatuanKecil.Text = lv_DetSatuan
	'                    TxtStock.Text = lv_DetGoodStock
	'                    TxtBags.Text = lv_DetJmlhBags
	'                    TxtMetPotStok.Text = lv_DetMetPotStock
	'                    TxtJenisBags.Text = lv_DetJenisBags

	'                    TxtStockDisplay.Text = Format(Val(HilangkanTanda(lv_DetGoodStock)), "N2") + " " + lv_DetSatuanDIsplay
	'                    'TxtSatuanBags.Text = lv_DetSatuanBags

	'                    Lv_DetBarang.Visible = False
	'                    DGV_Data_TF.Rows.Clear()

	'                    Btn_GetData.Focus()

	'                Loop
	'            End Using

	'            CloseConn()
	'        Catch ex As Exception
	'            CloseConn()
	'            MessageBox.Show(ex.Message)
	'            Exit Sub
	'        End Try
	'    End If
	'End Sub

	Dim itemDgvRekap_Oto As Integer = 8

	'Tab 3
	Dim itemDgvDetail_lokasi As Integer = 0

	Dim itemDgvDetail_KdBarang As Integer = 1
	Dim itemDgvDetail_SN As Integer = 2
	Dim itemDgvDetail_NmBarang As Integer = 3
	Dim itemDgvDetail_IDWarehouseAwal As Integer = 4
	Dim itemDgvDetail_KodeRakAwal As Integer = 5
	Dim itemDgvDetail_IDPalletAwal As Integer = 6
	Dim itemDgvDetail_Jumlah As Integer = 7
	Dim itemDgvDetail_JumlahBags As Integer = 8
	Dim itemDgvDetail_KodeRakTujuan As Integer = 9
	Dim itemDgvDetail_IDWarehouseTujuan As Integer = 10
	Dim itemDgvDetail_Warna As Integer = 11
	Dim itemDgvDetail_JenisKemasan As Integer = 12
	Dim itemDgvDetail_TglProduksi As Integer = 13
	Dim itemDgvDetail_TglExpired As Integer = 14
	Dim itemDgvDetail_JenisKualitas As Integer = 15
	Dim itemDgvDetail_Barcode As Integer = 16

	'Dim itemDgvIDWareHouseTujuan As Integer = 10

	Private Sub TxtKodeCost_TextChanged(sender As Object, e As EventArgs) Handles TxtKodeCost.TextChanged

		If Not TxtKodeCost.Text.Trim.Length = 0 Then
			LvCost.Location = New Point(129, 165)
			LvCost.Visible = True
		Else
			LvCost.Location = New Point(1278, 165)
			LvCost.Visible = False
		End If

		Try
			OpenConn()

			LvCost.Items.Clear()
			Dim lv As New ListViewItem
			SQL = "Select id_cost_center,kode_cost_center From EMI_Master_Cost_Center "
			SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' AND Kode_Cost_Center LIKE '" & TxtKodeCost.Text & "%' "
			SQL = SQL & "ORDER BY Kode_Cost_Center"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					lv = LvCost.Items.Add(Dr("id_cost_center"))
					lv.SubItems.Add(Dr("kode_cost_center"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub LvCost_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LvCost.SelectedIndexChanged

	End Sub

	Private Sub Transfer_Stock_3_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

		Try
			OpenConn()

			'===============================
			'=     GET GUDANG PRODUKSI     =
			'===============================
			Dim Gudang As String = ""
			SQL = "select Kode_Stock_Owner, Keterangan from Stock_Owner_Gudang_Lain "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_produksi = 'Y'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Gudang = Dr("Kode_Stock_Owner")
				Else
					CloseConn()
					MessageBox.Show("Gudang Tidak Ditemukan", "Transfer Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			If CmbJnsTransfer.SelectedIndex = 0 Then
				MessageBox.Show("Lokasi Tujuan Harus Ke Lokasi Produksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			Else
				If CmbSo_Tujuan.Items.Count = 0 Or CmbSo_Tujuan.SelectedIndex = -1 Then Exit Sub
				If Not arrSO(CmbSo_Tujuan.SelectedIndex) = Gudang Then
					MessageBox.Show("List Request Hanya Tersedia Untuk Lokasi Produksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End If

			get_no_faktur()
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
		Emi_Display_Request_Material.lokasi_kirim = arrSO.Item(CmbSO_Asal.SelectedIndex)
		Emi_Display_Request_Material.ShowDialog()
	End Sub

	Private Sub Transfer_Stock_3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		kosong()
		Initial_List_View()

	End Sub

	Private Sub get_det_barang(ByVal index As Integer)
		lv_DetKodeSO = Lv_DetBarang.Items(index).SubItems(itemDetKodeSO).Text
		lv_DetKodeBarang = Lv_DetBarang.Items(index).SubItems(itemDetKodeBarang).Text
		lv_DetNamaBarang = Lv_DetBarang.Items(index).SubItems(itemDetNamaBarang).Text
		lv_DetGoodStock = Lv_DetBarang.Items(index).SubItems(itemDetGoodStock).Text
		lv_DetSatuan = Lv_DetBarang.Items(index).SubItems(itemDetSatuan).Text
		lv_DetSatuanDIsplay = Lv_DetBarang.Items(index).SubItems(itemDetSatuandisplay).Text
		lv_DetJmlhBags = Lv_DetBarang.Items(index).SubItems(itemDetJmlhBags).Text
		lv_DetSatuanBags = Lv_DetBarang.Items(index).SubItems(itemDetSatuanBags).Text
		lv_DetMetPotStock = Lv_DetBarang.Items(index).SubItems(itemDetMetPotStock).Text
		lv_DetJenisBags = Lv_DetBarang.Items(index).SubItems(itemDetJenisBags).Text
	End Sub

	Private Sub get_grid_view(ByVal index As Integer)
		dgv_Lokasi = DGV_Data_TF.Rows(index).Cells(itemDgvLokasi).Value
		dgv_KodeBarang = DGV_Data_TF.Rows(index).Cells(itemDgvKodeBarang).Value
		dgv_SerialNumber = DGV_Data_TF.Rows(index).Cells(itemDgvSerialNumber).Value
		dgv_Nama = DGV_Data_TF.Rows(index).Cells(itemDgvNama).Value
		dgv_IDWareHouse = DGV_Data_TF.Rows(index).Cells(itemDgvIDWareHose).Value
		dgv_KodeRak = DGV_Data_TF.Rows(index).Cells(itemDgvKodeRak).Value
		dgv_IDPallet = DGV_Data_TF.Rows(index).Cells(itemDgvIDPallet).Value
		dgv_GoodStock = DGV_Data_TF.Rows(index).Cells(itemDgvGoodStock).Value
		dgv_Satuan = DGV_Data_TF.Rows(index).Cells(itemDgvSatuan).Value
		dgv_Jumlah = If(General_Class.CekNULL(DGV_Data_TF.Rows(index).Cells(itemDgvJumlah).Value) = "", "0", DGV_Data_TF.Rows(index).Cells(itemDgvJumlah).Value)
		dgv_RakTujuan = DGV_Data_TF.Rows(index).Cells(itemDgvRakTujuan).Value
		dgv_JmlhBags = DGV_Data_TF.Rows(index).Cells(itemDgvBags).Value
		dgv_Warna = DGV_Data_TF.Rows(index).Cells(itemDgvWarna).Value
		dgv_JenisKemasan = DGV_Data_TF.Rows(index).Cells(itemJenisKemasan).Value
		dgv_IsiPerBags = If(General_Class.CekNULL(DGV_Data_TF.Rows(index).Cells(itemDGVIsiPerBags).Value) = "", "0", DGV_Data_TF.Rows(index).Cells(itemDGVIsiPerBags).Value)
		dgv_SatuanIsiBags = DGV_Data_TF.Rows(index).Cells(itemDGVSatuanIsiBags).Value

		dgv_CheckBox = Convert.ToBoolean(DGV_Data_TF.Rows(index).Cells(itemDgvCheckBox).Value)

		dgv_TglProd = DGV_Data_TF.Rows(index).Cells(itemDGVTglProd).Value
		dgv_TglExp = DGV_Data_TF.Rows(index).Cells(itemDGVTglExp).Value
		dgv_KetWarna = DGV_Data_TF.Rows(index).Cells(itemDGVKetWarna).Value
		dgv_Barcode = DGV_Data_TF.Rows(index).Cells(itemDGVBarcode).Value
		dgv_FlagBlokSN = DGV_Data_TF.Rows(index).Cells(itemDGVFlagBlokSN).Value

	End Sub

	Private Sub get_grid_view_Rekap(ByVal index As Integer)
		dgv_Rekap_lokasi = Dgv_DataRekap.Rows(index).Cells(itemDgvRekap_lokasi).Value
		dgv_Rekap_KdBarang = Dgv_DataRekap.Rows(index).Cells(itemDgvRekap_KdBarang).Value
		dgv_Rekap_NmBarang = Dgv_DataRekap.Rows(index).Cells(itemDgvRekap_NmBarang).Value
		dgv_Rekap_Jumlah = Dgv_DataRekap.Rows(index).Cells(itemDgvRekap_Jumlah).Value
		dgv_Rekap_JumlahBags = Dgv_DataRekap.Rows(index).Cells(itemDgvRekap_JumlahBags).Value
		dgv_Rekap_JumlahBersih = Dgv_DataRekap.Rows(index).Cells(itemDgvRekap_JumlahBersih).Value
		dgv_Rekap_Satuan = Dgv_DataRekap.Rows(index).Cells(itemDgvRekap_Satuan).Value
		dgv_Rekap_SatuanKecil = Dgv_DataRekap.Rows(index).Cells(itemDgvRekap_SatuanKecil).Value
		dgv_Rekap_Oto = Dgv_DataRekap.Rows(index).Cells(itemDgvRekap_Oto).Value
	End Sub

	Private Sub get_grid_view_Detail(ByVal index As Integer)
		dgv_detail_lokasi = Dgv_DataDetail.Rows(index).Cells(itemDgvDetail_lokasi).Value
		dgv_detail_KdBarang = Dgv_DataDetail.Rows(index).Cells(itemDgvDetail_KdBarang).Value
		dgv_detail_SN = Dgv_DataDetail.Rows(index).Cells(itemDgvDetail_SN).Value
		dgv_detail_NmBarang = Dgv_DataDetail.Rows(index).Cells(itemDgvDetail_NmBarang).Value
		dgv_detail_IDWarehouseAwal = Dgv_DataDetail.Rows(index).Cells(itemDgvDetail_IDWarehouseAwal).Value
		dgv_detail_KodeRakAwal = Dgv_DataDetail.Rows(index).Cells(itemDgvDetail_KodeRakAwal).Value
		dgv_detail_IDPalletAwal = Dgv_DataDetail.Rows(index).Cells(itemDgvDetail_IDPalletAwal).Value
		dgv_detail_Jumlah = Dgv_DataDetail.Rows(index).Cells(itemDgvDetail_Jumlah).Value
		dgv_detail_JumlahBags = Dgv_DataDetail.Rows(index).Cells(itemDgvDetail_JumlahBags).Value
		dgv_detail_KodeRakTujuan = Dgv_DataDetail.Rows(index).Cells(itemDgvDetail_KodeRakTujuan).Value
		dgv_detail_IDWarehouseTujuan = Dgv_DataDetail.Rows(index).Cells(itemDgvDetail_IDWarehouseTujuan).Value
		dgv_detail_Warna = Dgv_DataDetail.Rows(index).Cells(itemDgvDetail_Warna).Value
		dgv_detail_JenisKemasan = Dgv_DataDetail.Rows(index).Cells(itemDgvDetail_JenisKemasan).Value
		dgv_detail_TglProduksi = Dgv_DataDetail.Rows(index).Cells(itemDgvDetail_TglProduksi).Value
		dgv_detail_TglExpired = Dgv_DataDetail.Rows(index).Cells(itemDgvDetail_TglExpired).Value
		dgv_detail_JenisKualitas = Dgv_DataDetail.Rows(index).Cells(itemDgvDetail_JenisKualitas).Value
		dgv_detail_Barcode = Dgv_DataDetail.Rows(index).Cells(itemDgvDetail_Barcode).Value

	End Sub

	Private Sub Initial_List_View()

		Lv_DetBarang.Columns.Add("Kode SO", 140, HorizontalAlignment.Center)
		Lv_DetBarang.Columns.Add("Kode Barang", 130, HorizontalAlignment.Center)
		Lv_DetBarang.Columns.Add("Nama", 200, HorizontalAlignment.Center)
		Lv_DetBarang.Columns.Add("Stock", 90, HorizontalAlignment.Center)
		Lv_DetBarang.Columns.Add("Satuan", 0, HorizontalAlignment.Center)
		Lv_DetBarang.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
		Lv_DetBarang.Columns.Add("Jumlah Bags", 90, HorizontalAlignment.Center)
		Lv_DetBarang.Columns.Add("Satuan", 0, HorizontalAlignment.Center)

		Lv_DetBarang.View = View.Details

	End Sub

	'FUNCTION UTILITY
	Public Sub kosong()
		get_jam()
		Dim x As New Point(129, 165)
		LvCost.Location = x
		LvCost.Visible = False
		Try
			OpenConn()
			'get_no_faktur()
			TxtKd_Barang.Enabled = True
			Btn_GetData.Enabled = True
			asal = "Transfer_Stock_3"

			DGV_Data_TF.Columns(itemDgvWarna).DisplayIndex = 6

			DGV_Data_TF.Columns(itemDGVKetWarna).DisplayIndex = 4
			DGV_Data_TF.Columns(itemDGVTglExp).DisplayIndex = 4
			DGV_Data_TF.Columns(itemDGVTglProd).DisplayIndex = 4

			DGV_Data_TF.Columns(itemDGVBarcode).DisplayIndex = 6
			Dgv_DataDetail.Columns(itemDgvDetail_Barcode).DisplayIndex = 6

			Cmb_Warna.Items.Clear() : Cmb_Warna.SelectedIndex = -1
			Cmb_Warna.Items.Add("MERAH") : Cmb_Warna.Items.Add("KUNING") : Cmb_Warna.Items.Add("HIJAU")
			Cmb_Warna.SelectedIndex = 2

			CmbSo_Tujuan.Items.Clear() : CmbSo_Tujuan.SelectedIndex = -1
			CmbSO_Asal.Items.Clear() : CmbSO_Asal.SelectedIndex = -1
			SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From Stock_Owner_Gudang_Lain where "
			SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and (flag_produksi='Y' or Flag_Penyimpanan='Y') "
			SQL = SQL & "order by kode_stock_owner"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbSo_Tujuan.Items.Add(dr("Keterangan")) : arrSO.Add(dr("kode_stock_owner"))
					CmbSO_Asal.Items.Add(dr("Keterangan")) : arrInisialFaktur.Add(dr("inisial_faktur"))
				Loop
			End Using

			Cmb_Lokasi.Items.Clear()
			SQL = "select kode_stock_owner from Stock_Owner"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Lokasi.Items.Add(Dr("kode_stock_owner"))
				Loop
			End Using
			Cmb_Lokasi.SelectedIndex = 0

			ComboBox1.Items.Clear() : arr_id_keterangan.Clear() : arr_kode_akun.Clear()
			SQL = "select ID, Keterangan, Kode_Account from N_EMI_Master_Account_Pengeluaran_Stock_Barang_Lain "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Aktif = 'Y' order by Keterangan "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					ComboBox1.Items.Add(Dr("Keterangan"))
					arr_id_keterangan.Add(Dr("ID"))
					arr_kode_akun.Add(Dr("Kode_Account"))
				Loop
			End Using
			ComboBox1.SelectedIndex = -1

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Lv_DetBarang.Items.Clear()

		CmbJnsTransfer.Items.Clear()
		CmbJnsTransfer.Items.Add("Antar Rak")
		CmbJnsTransfer.Items.Add("Antar Gudang")
		CmbJnsTransfer.SelectedIndex = 0

		TxtKodeCost.Text = ""
		TxtIDCost.Text = ""
		TxtJenisBags.Text = ""
		Txt_OtoMaterial_req.Text = ""
		Txt_JumlahPermintaan.Text = ""
		Txt_SatuanPermintaan.Text = ""
		TxtSatuanKecil.Text = ""
		Txt_Warna.Text = ""
		TxtSatuan.Text = ""
		TxtStock.Text = ""
		TxtKeterangan.Text = ""
		TxtKd_Barang.Text = ""
		Txt_SO.Text = ""
		TxtNm_Barang.Text = ""
		TxtBags.Text = ""
		TxtTotalTransferBags.Text = ""

		TxtjmlPermintaanDisplay.Text = ""
		TxtStockDisplay.Text = ""
		TxtMetPotStok.Text = ""

		TxtjmlPermintaanBersih.Text = ""
		TxtsisaRequest.Text = ""

		CmbJnsTransfer.Enabled = True

		Cmb_JenisPengeluaran.Items.Clear() : arrJenisPengeluaran.Clear()
		Cmb_JenisPengeluaran.Items.Add("Pengeluaran Stock") : arrJenisPengeluaran.Add("Pengeluaran_Stock")
		Cmb_JenisPengeluaran.Items.Add("Pengeluaran Barang Reject") : arrJenisPengeluaran.Add("Pengeluaran_Reject")

		If MenuAsal.Trim.Length = 0 Then
			MessageBox.Show("Menu Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.Close()
		Else
			If MenuAsal.ToUpper = "PENGELUARAN_STOCK" Then
				Cmb_JenisPengeluaran.SelectedIndex = 0
			ElseIf MenuAsal.ToUpper = "PENGELUARAN_STOCK_REJECTED" Then
				Cmb_JenisPengeluaran.SelectedIndex = 1
			Else
				MessageBox.Show("Menu Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Me.Close()
			End If
		End If

		DGV_Data_TF.Rows.Clear()
		Dgv_DataRekap.Rows.Clear()
		Dgv_DataDetail.Rows.Clear()
	End Sub

	Private Sub KosongTab1()

		TxtjmlPermintaanDisplay.Text = ""
		TxtKd_Barang.Text = ""
		TxtNm_Barang.Text = ""
		TxtStockDisplay.Text = ""
		TxtBags.Text = ""
		TxtMetPotStok.Text = ""
		TxtJenisBags.Text = ""
		TxtTotalTransfer.Text = ""
		TxtTotalTransferBags.Text = ""
		TxtsisaRequest.Text = ""
		Txt_QR.Text = ""

		DGV_Data_TF.Rows.Clear()

	End Sub

	Private Sub DGV_Data_TF_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Data_TF.CellClick
		'cek apakah yang di klik adalah header
		If e.RowIndex = -1 Then Exit Sub

		Dim currentCell As Integer = DGV_Data_TF.CurrentCell.ColumnIndex
		Dim currentRow As Integer = DGV_Data_TF.CurrentRow.Index

		Dim cellValue As Object = HilangkanTanda(DGV_Data_TF.Rows(currentRow).Cells(currentCell).Value)

		If currentCell = rak_tujuan.Index Then
			If Not DGV_Data_TF.CurrentCell.Value = "" Then
				DGV_Data_TF.CurrentCell = DGV_Data_TF.Rows(currentRow).Cells(currentCell)
				DGV_Data_TF.BeginEdit(True)
			End If
		ElseIf currentCell = itemDgvJumlah Then
			Dim cellKuantity As String = HilangkanTanda(DGV_Data_TF.CurrentCell.Value)

			If cellKuantity = "" Then
				Exit Sub
			End If

			Dim cleanedStr As String = HilangkanTanda(cellKuantity)
			Dim nilai As Decimal = Decimal.Parse(cleanedStr)

			DGV_Data_TF.Rows(currentRow).Cells(currentCell).Value = nilai
		ElseIf currentCell = itemDgvBags Then
			Dim cellKuantity As String = HilangkanTanda(DGV_Data_TF.CurrentCell.Value)

			If cellKuantity = "" Then
				Exit Sub
			End If

			Dim cleanedStr As String = HilangkanTanda(cellKuantity)
			Dim nilai As Decimal = Decimal.Parse(cleanedStr)

			DGV_Data_TF.Rows(currentRow).Cells(currentCell).Value = nilai
		End If

	End Sub

	Private Sub DGV_Data_TF_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Data_TF.CellEndEdit
		If DGV_Data_TF.Rows.Count = 0 Then Exit Sub

		Dim indexRow As Integer = DGV_Data_TF.CurrentRow.Index
		Dim TempArray As New ArrayList

		If DGV_Data_TF.CurrentRow.Cells(itemDgvCheckBox).Value = "True" Then

			TempArray.Clear()
			For i As Integer = 0 To DGV_Data_TF.Columns.Count - 1

				If i <> Val(itemDgvJumlah) AndAlso i <> Val(itemDgvBags) AndAlso i <> Val(itemDgvRakTujuan) AndAlso i <> Val(itemDGVTglExp) AndAlso i <> Val(itemDGVFlagBlokSN) Then

					TempArray.Add(DGV_Data_TF.Rows(indexRow).Cells(i).Value)
				End If

			Next
			If TempArray.Contains("") Then
				DGV_Data_TF.CurrentRow.Cells(itemDgvCheckBox).Value = False
				Exit Sub
			End If

			If DGV_Data_TF.CurrentRow.Cells(itemDGVFlagBlokSN).Value.ToString.ToUpper = "Y" Then
				DGV_Data_TF.CurrentRow.Cells(itemDgvCheckBox).Value = False
				MessageBox.Show("Data Tidak Bisa Dikirim Karena SN telah di Blok", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			If DGV_Data_TF.CurrentRow.Cells(itemDgvWarna).Value.ToString.ToUpper <> "HIJAU" Then
				DGV_Data_TF.CurrentRow.Cells(itemDgvCheckBox).Value = False
				MessageBox.Show("Data Tidak bisa di kirim . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			Dim currentColumn As Integer = DGV_Data_TF.CurrentCell.ColumnIndex
			Dim cellValue As Object = DGV_Data_TF.CurrentRow.Cells(currentColumn).Value

			If currentColumn = itemDgvBags OrElse currentColumn = itemDgvJumlah Then
				If Not IsNumeric(cellValue) Then
					DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = ""
					DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
					Exit Sub
				End If
			End If

			If DGV_Data_TF.CurrentRow.Cells(itemJenisKemasan).Value.ToString.ToUpper = "ORIGINAL BAGS" Then
				DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).ReadOnly = True

				If Not DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = "" Then

					Dim stockBags As Double = Val(HilangkanTanda(DGV_Data_TF.CurrentRow.Cells(itemDgvStockBags).Value))
					Dim jumlahInputBags As Double = Val(HilangkanTanda(DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value))
					Dim isiPerbags As Double = Val(HilangkanTanda(DGV_Data_TF.CurrentRow.Cells(itemDGVIsiPerBags).Value))
					Dim jumlahStock As Double = Val(HilangkanTanda(DGV_Data_TF.CurrentRow.Cells(itemDgvGoodStock).Value))

					'cek apakah input melebihi
					If jumlahInputBags > stockBags Then
						MessageBox.Show("Jumlah Input Tidak Boleh Melebihi Stock", "Transfer Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
						DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = ""
						Exit Sub
					End If

					Dim valueJumlah As Double = isiPerbags * jumlahInputBags

					DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = Format(valueJumlah, "N2")
					If valueJumlah > jumlahStock Then
						DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = Format(jumlahStock, "N2")
						'DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = Math.Floor(jumlahStock / isiPerbags)
					Else
						DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = Format(valueJumlah, "N2")
					End If

				End If
			Else

				DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).ReadOnly = False

				Dim jumlahStock As Double = Val(HilangkanTanda(DGV_Data_TF.CurrentRow.Cells(itemDgvGoodStock).Value))
				Dim jumlahInput As Double = Val(HilangkanTanda(DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value))
				Dim stockBags As Double = Val(HilangkanTanda(DGV_Data_TF.CurrentRow.Cells(itemDgvStockBags).Value))
				Dim jumlahInputBags As Double = Val(HilangkanTanda(DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value))

				If jumlahInput > jumlahStock Then
					MessageBox.Show("Jumlah Tidak Boleh Melebihi Stock ", "Transfer Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
					DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = ""
					Exit Sub
				End If
				'cek apakah input melebihi
				If jumlahInputBags > stockBags Then
					MessageBox.Show("Jumlah Input Tidak Boleh Melebihi Stock", "Transfer Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = ""
					Exit Sub
				End If

			End If

			DGV_Data_TF.CurrentRow.Cells(itemDgvBags).ReadOnly = False
			DGV_Data_TF.CurrentRow.Cells(itemDgvRakTujuan).ReadOnly = False

			DGV_Data_TF.CurrentRow.DefaultCellStyle.BackColor = Color.White
			DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Style.BackColor = Color.LightGray
		Else

			DGV_Data_TF.CurrentRow.DefaultCellStyle.BackColor = Color.White
			DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Style.BackColor = Color.White

			DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
			DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = ""
			DGV_Data_TF.CurrentRow.Cells(itemDgvRakTujuan).Value = ""

			DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).ReadOnly = True
			DGV_Data_TF.CurrentRow.Cells(itemDgvBags).ReadOnly = True
			DGV_Data_TF.CurrentRow.Cells(itemDgvRakTujuan).ReadOnly = True
		End If

		Dim currentColumnIndex As Integer = DGV_Data_TF.CurrentCell.ColumnIndex

		If currentColumnIndex = itemDgvBags OrElse currentColumnIndex = itemDgvJumlah Then

			Dim jumlahValue As Object = HilangkanTanda(DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value)
			If jumlahValue IsNot Nothing AndAlso IsNumeric(jumlahValue) Then
				DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = Convert.ToDecimal(jumlahValue).ToString("N2")
			Else
				DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
			End If

			Dim bagsValue As Object = HilangkanTanda(DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value)
			If bagsValue IsNot Nothing AndAlso IsNumeric(bagsValue) Then
				DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = Convert.ToDecimal(bagsValue).ToString("N2")
			Else
				DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = ""
			End If
		End If

		GetGrandTotal()

	End Sub

	Private Sub DGV_Data_TF_MouseLeave(sender As Object, e As EventArgs) Handles DGV_Data_TF.MouseLeave
		If DGV_Data_TF.RowCount = 0 Then Exit Sub

		Dim cellValue As String = DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value

		If Not IsNumeric(cellValue) Then
			DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
		End If

		'PINDAH FOKUS
		'DGV_Data_TF.CurrentCell = DGV_Data_TF.CurrentRow.Cells(itemDgvSatuan)
		'DGV_Data_TF.BeginEdit(True)
	End Sub

	Private Sub get_jam()
		Try
			OpenConn()

			SQL = "declare @ab int; select @ab = Selisih_Jam from Init; "
			SQL = SQL & " Select FORMAT(DATEADD(hh, @ab, getdate()), 'yyyy-MM-dd HH:mm:ss') as Tanggal_Sekarang "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					tgl_skg = dr("Tanggal_Sekarang")
				Loop
			End Using
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub get_no_faktur()
		Dim FPro_Results As String = "PA-"
		TxtNo_Transaksi.Text = FPro_Results & arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy") & "-" &
									  General_Class.Get_Last_Number2("EMI_Pengeluaran_Stock_parent_barang_lain", "no_faktur", JumlahDigit,
									  "Kode_perusahaan", KodePerusahaan,
									  "And", "substring(no_faktur,1," & Len(FPro_Results) + Len(arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex)) + 6 & ")", FPro_Results & arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))

	End Sub

	'FUNCTION HANDLE
	Private Sub TxtKd_Barang_TextChanged(sender As Object, e As EventArgs) Handles TxtKd_Barang.TextChanged, Txt_SO.TextChanged, Txt_SatuanPermintaan.TextChanged, Txt_JumlahPermintaan.TextChanged, Txt_OtoMaterial_req.TextChanged
		If asal <> "Emi_Display_Request_Material" Then
			'If TxtKd_Barang.Text.Trim.Length = 0 Then Exit Sub
			If CmbJnsTransfer.SelectedIndex = -1 Then Exit Sub
			If CmbJnsTransfer.SelectedIndex = 0 Then
				If CmbSO_Asal.SelectedIndex = -1 Then Exit Sub

			ElseIf CmbJnsTransfer.SelectedIndex = 1 Then
				If CmbSO_Asal.SelectedIndex = -1 Then Exit Sub
				If CmbSo_Tujuan.SelectedIndex = -1 Then Exit Sub
			End If

			If Not TxtKd_Barang.Text.Trim.Count = 0 Then
				Lv_DetBarang.Location = New Point(37, 318)
				Lv_DetBarang.Visible = True
			Else
				Lv_DetBarang.Location = New Point(1278, 277)
				Lv_DetBarang.Visible = False
			End If

			Try
				OpenConn()
				get_no_faktur()

				Lv_DetBarang.BeginUpdate()
				Lv_DetBarang.Items.Clear()
				SQL = "select top(20) a.kode_stock_owner, a.kode_barang, a.nama, dbo.ubah_satuan_lain(a.kode_Perusahaan, 'masa', a.kode_barang, a.satuan, "
				SQL = SQL & "b.satuan, a.good_stock) as Good_Stock, a.Satuan, b.satuan as satuan_display, ISNULL(a.Jumlah_Bags, 0) as Jumlah_Bags, "
				SQL = SQL & "a.Metode_Pengeluaran_Stok, a.Jenis_Kemasan from barang_lain a, Barang_Detail_Satuan_Lain b "
				SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' and a.Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
				SQL = SQL & "and a.nama like '%" & TxtKd_Barang.Text & "%' and a.Kode_Barang=b.kode_barang "
				SQL = SQL & "And a.kode_Perusahaan = b.kode_Perusahaan And b.flag_tampil_display ='Y'  "
				SQL = SQL & "order by a.Kode_Barang "
				Using Dr = OpenTrans(SQL)
					Do While Dr.Read
						Dim Lv As New ListViewItem
						Lv = Lv_DetBarang.Items.Add(General_Class.CekNULL(Dr("kode_stock_owner")))
						Lv.SubItems.Add(General_Class.CekNULL(Dr("kode_barang")))
						Lv.SubItems.Add(General_Class.CekNULL(Dr("nama")))
						Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Good_Stock")) = "", "", Format(Dr("Good_Stock"), "N2")))
						Lv.SubItems.Add(General_Class.CekNULL(Dr("Satuan")))
						Lv.SubItems.Add(General_Class.CekNULL(Dr("satuan_display")))
						Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jumlah_Bags")) = "", "", Format(Dr("Jumlah_Bags"), "N0")))
						Lv.SubItems.Add("-") 'satuan perbags tidak dipakai lagi
						Lv.SubItems.Add(General_Class.CekNULL(Dr("Metode_Pengeluaran_Stok")))
						Lv.SubItems.Add(General_Class.CekNULL(Dr("Jenis_Kemasan")))
					Loop
				End Using
				Lv_DetBarang.EndUpdate()

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try
		End If

	End Sub

	Private Sub Lv_DetBarang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DetBarang.DoubleClick

		If Lv_DetBarang.Items.Count = 0 Or TxtKd_Barang.Text.Trim = "" Then Exit Sub

		get_det_barang(Lv_DetBarang.FocusedItem.Index)

		'================================================
		'=     CEK APAKAH DATA MASTER SUDAH LENGKAP     =
		'================================================
		Dim TempArray As New ArrayList

		TempArray.Clear()
		For i As Integer = 0 To Lv_DetBarang.Columns.Count - 1
			Dim indexx As Integer = Lv_DetBarang.FocusedItem.Index
			Dim data As String = Lv_DetBarang.Items(indexx).SubItems(i).Text

			TempArray.Add(data)
		Next

		If TempArray.Contains("") Then
			MessageBox.Show("Data Master Terhadap Barang Masih Belum Lengkap", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		CmbJnsTransfer.Enabled = False
		CmbSO_Asal.Enabled = False
		CmbSo_Tujuan.Enabled = False

		TxtKd_Barang.Text = String.Empty
		Txt_SO.Text = String.Empty
		TxtNm_Barang.Text = String.Empty

		TxtKd_Barang.Text = lv_DetKodeBarang
		Txt_SO.Text = lv_DetKodeSO
		TxtNm_Barang.Text = lv_DetNamaBarang
		TxtSatuan.Text = lv_DetSatuanDIsplay
		TxtSatuanKecil.Text = lv_DetSatuan
		TxtStock.Text = lv_DetGoodStock
		TxtBags.Text = lv_DetJmlhBags
		TxtMetPotStok.Text = lv_DetMetPotStock
		TxtJenisBags.Text = lv_DetJenisBags

		TxtStockDisplay.Text = Format(Val(HilangkanTanda(lv_DetGoodStock)), "N2") + " " + lv_DetSatuanDIsplay
		'TxtSatuanBags.Text = lv_DetSatuanBags

		Lv_DetBarang.Location = New Point(803, 258)
		Lv_DetBarang.Visible = False
		DGV_Data_TF.Rows.Clear()

		Btn_GetData.Focus()
	End Sub

	Private Sub CmbJnsTransfer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbJnsTransfer.SelectedIndexChanged

		If CmbJnsTransfer.Items.Count = 0 Then Exit Sub

		If CmbJnsTransfer.SelectedIndex = 0 Then
			CmbSO_Asal.Enabled = True
			CmbSo_Tujuan.SelectedIndex = -1
			CmbSo_Tujuan.Enabled = False

		ElseIf CmbJnsTransfer.SelectedIndex = 1 Then
			CmbSO_Asal.Enabled = True
			CmbSo_Tujuan.Enabled = True
		End If

		Lv_DetBarang.Visible = False
		TxtKd_Barang.Text = ""
		DGV_Data_TF.Rows.Clear()
		TxtTotalTransfer.Text = String.Empty

	End Sub

	Public Sub Btn_Insert_Click(sender As Object, e As EventArgs) Handles Btn_GetData.Click

		If CmbJnsTransfer.SelectedIndex = 0 Then
			If CmbSO_Asal.SelectedIndex = -1 Then
				MessageBox.Show("Isi SO Awal terlebih dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				CmbSO_Asal.Focus() : Exit Sub
			End If
		ElseIf CmbJnsTransfer.SelectedIndex = 1 Then
			If CmbSo_Tujuan.SelectedIndex = -1 Then
				MessageBox.Show("Isi SO Awal terlebih dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				CmbSO_Asal.Focus() : Exit Sub
			End If
		End If

		If TxtKd_Barang.Text.Trim.Length = 0 Or TxtKd_Barang.Text.Trim = "" Or TxtNm_Barang.Text.Trim.Length = 0 Then Exit Sub

		If Cmb_Warna.SelectedIndex = -1 Then
			MessageBox.Show("Isi Warna terlebih dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Warna.Focus() : Exit Sub
		End If

		Try
			OpenConn()

			Dim rows As Integer = 0
			DGV_Data_TF.Rows.Clear()

			kd_barang = String.Empty
			kd_barang = TxtKd_Barang.Text

			arrIdWMSWarehouse.Clear()
			WarehosePosition.Clear()

			'SQL = "Select a.Id_WMS_Warehouse_Position, a.Keterangan from "
			'SQL = SQL & "View_Warehouse_Position_Barang_Lain a, View_Warehouse_Position_Detail_Barang_Lain b "
			'SQL = SQL & "where a.id_wms_warehouse_position = b.id_wms_warehouse_position "
			'SQL = SQL & "And a.KOde_Perusahaan = b.KOde_Perusahaan "
			'SQL = SQL & " And b.kode_Barang Is null "

			SQL = "Select a.Id_WMS_Warehouse_Position, a.Keterangan "
			SQL = SQL & "from View_Warehouse_Position_Barang_Lain a "
			SQL = SQL & "where a.KOde_Perusahaan = '" & KodePerusahaan & "' "

			If CmbJnsTransfer.SelectedIndex = 0 Then
				SQL = SQL & "and a.Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
			ElseIf CmbJnsTransfer.SelectedIndex = 1 Then
				SQL = SQL & "and a.Kode_Stock_Owner='" & arrSO(CmbSo_Tujuan.SelectedIndex) & "' "
			End If

			SQL = SQL & "group by a.Id_WMS_Warehouse_Position, a.Keterangan "
			SQL = SQL & "order by a.Keterangan "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					WarehosePosition.Add(Dr("Keterangan")) : arrIdWMSWarehouse.Add(Dr("Id_WMS_Warehouse_Position"))
				Loop
			End Using

			For index = 0 To Dgv_DataRekap.Rows.Count - 1
				get_grid_view_Rekap(index)

				If TxtKd_Barang.Text = dgv_Rekap_KdBarang Then
					MessageBox.Show("barang sudah Pernah di input ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					TxtKd_Barang.Focus() : Exit Sub
				End If

			Next
			'SQL = "select a.Kode_Stock_Owner, a.Kode_Barang, a.Serial_Number, b.Nama, a.Id_Warehouse, c.Keterangan as kode_rak, "
			'SQL = SQL & " a.Id_Nametag_pallet, dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, b.satuan, "
			'SQL = SQL & "'" & TxtSatuan.Text & "', a.jumlah) as jumlah, b.satuan, a.nomor_pallet, ISNULL(a.Jumlah_Bags, 0) as stock_bags, a.warna, b.Jenis_Kemasan, "
			'SQL = SQL & "b.Isi_Per_Bags, b.Satuan_Isi_Bags, a.Tgl_Expired, a.Tgl_Produksi "
			'SQL = SQL & "from barang_sn a, barang b, View_Warehouse_Position c, View_Warehouse_Position_Detail d "
			'SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Barang=b.Kode_Barang and a.Kode_Stock_Owner=b.Kode_Stock_Owner "
			'SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner = d.Kode_Stock_Owner and a.Nomor_Pallet = d.nomor_urut "
			'SQL = SQL & "and a.Id_Warehouse=c.Id_WMS_Warehouse_Position and c.Id_WMS_Warehouse_Position=d.Id_WMS_Warehouse_Position "
			'SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
			'SQL = SQL & "and b.Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' and b.Kode_Barang='" & TxtKd_Barang.Text & "' "
			'' SQL = SQL & "and a.warna = '" & Cmb_Warna.SelectedItem.ToString & "' "
			'SQL = SQL & "and a.Jumlah <> 0 "
			'SQL = SQL & "order by a.Kode_Barang "

			SQL = "Select top(40) a.Kode_Stock_Owner, a.Kode_Barang, a.Serial_Number, b.Nama, "
			SQL = SQL & " a.Id_Warehouse, c.Keterangan As kode_rak, a.Id_Nametag_pallet, "
			SQL = SQL & " dbo.ubah_satuan_lain(a.kode_Perusahaan, 'masa', a.kode_barang, b.satuan, '" & TxtSatuan.Text & "', a.jumlah) as jumlah, "
			SQL = SQL & " b.satuan, a.nomor_pallet, isNull(a.Jumlah_Bags, 0) As stock_bags, a.warna, b.Metode_Pengeluaran_Stok, "
			SQL = SQL & " b.Jenis_Kemasan, 1 as Isi_Per_Bags, '-' as Satuan_Isi_Bags, a.Tgl_Expired, a.Tgl_Produksi "
			SQL = SQL & ",isNull((select x.keterangan from emi_master_warna x where "
			SQL = SQL & "x.kode_Perusahaan = a.kode_Perusahaan And x.kode_warna = a.warna),NULL) As Ket_Warna, "
			SQL = SQL & "(a.Qr_Code + '-' + a.Kode_Unik_Berjalan) as Barcode, a.Blok_SN "
			SQL = SQL & " From Barang_Lain_SN a, barang_lain b, View_Warehouse_Position_Barang_Lain c "
			SQL = SQL & " Where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Barang = b.Kode_Barang And "
			SQL = SQL & " a.Kode_Stock_Owner = b.Kode_Stock_Owner And a.Kode_Perusahaan = c.Kode_Perusahaan And "
			SQL = SQL & " a.Id_Warehouse = c.Id_WMS_Warehouse_Position And "
			SQL = SQL & " a.Kode_Perusahaan ='" & KodePerusahaan & "' "
			SQL = SQL & " And b.Kode_Stock_Owner ='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
			SQL = SQL & " And b.Kode_Barang='" & TxtKd_Barang.Text & "' "
			SQL = SQL & " And a.Jumlah <> 0 "
			SQL = SQL & " order by case "
			SQL = SQL & " when Metode_Pengeluaran_Stok='FIFO' then a.Tgl_Masuk "
			SQL = SQL & " Else a.Tgl_Expired End "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					Dim subArr As New List(Of String)

					DGV_Data_TF.Rows.Add(1)
					DGV_Data_TF.Rows(rows).Cells(itemDgvLokasi).Value = General_Class.CekNULL(Dr("Kode_Stock_Owner"))
					DGV_Data_TF.Rows(rows).Cells(itemDgvKodeBarang).Value = General_Class.CekNULL(Dr("Kode_Barang"))
					DGV_Data_TF.Rows(rows).Cells(itemDgvSerialNumber).Value = General_Class.CekNULL(Dr("Serial_Number"))
					DGV_Data_TF.Rows(rows).Cells(itemDgvNama).Value = General_Class.CekNULL(Dr("Nama"))
					DGV_Data_TF.Rows(rows).Cells(itemDgvIDWareHose).Value = General_Class.CekNULL(Dr("Id_Warehouse"))
					DGV_Data_TF.Rows(rows).Cells(itemDgvKodeRak).Value = General_Class.CekNULL(Dr("kode_rak"))
					DGV_Data_TF.Rows(rows).Cells(itemDgvIDPallet).Value = General_Class.CekNULL(Dr("nomor_pallet"))
					DGV_Data_TF.Rows(rows).Cells(itemDgvGoodStock).Value = If(General_Class.CekNULL(Dr("jumlah")) = "", "", Format(Dr("jumlah"), "N2"))
					DGV_Data_TF.Rows(rows).Cells(itemDgvStockBags).Value = If(General_Class.CekNULL(Dr("stock_bags")) = "", "", Format(Dr("stock_bags"), "N0"))
					DGV_Data_TF.Rows(rows).Cells(itemDgvWarna).Value = General_Class.CekNULL(Dr("warna"))
					DGV_Data_TF.Rows(rows).Cells(itemJenisKemasan).Value = General_Class.CekNULL(Dr("Jenis_Kemasan"))
					DGV_Data_TF.Rows(rows).Cells(itemDGVIsiPerBags).Value = General_Class.CekNULL(Dr("Isi_Per_Bags"))
					DGV_Data_TF.Rows(rows).Cells(itemDGVSatuanIsiBags).Value = General_Class.CekNULL(Dr("Satuan_Isi_Bags"))
					DGV_Data_TF.Rows(rows).Cells(itemDGVKetWarna).Value = General_Class.CekNULL(Dr("Ket_Warna"))
					DGV_Data_TF.Rows(rows).Cells(itemDGVTglProd).Value = If(General_Class.CekNULL(Dr("Tgl_Produksi")) = "", "", Format(Dr("Tgl_Produksi"), "dd MMM yyyy"))
					DGV_Data_TF.Rows(rows).Cells(itemDgvSatuan).Value = TxtSatuan.Text

					Dim dgvCmbValueRak As DataGridViewComboBoxCell
					dgvCmbValueRak = DGV_Data_TF.Rows(rows).Cells(itemDgvRakTujuan)
					dgvCmbValueRak.Items.Clear()

					'dgvCmbValueRak.Items.Add("-- Tidak Berubah --") : subArr.Add(Dr("Id_Warehouse"))
					For i As Integer = 0 To WarehosePosition.Count - 1
						dgvCmbValueRak.Items.Add(WarehosePosition(i)) : subArr.Add(arrIdWMSWarehouse(i))
					Next

					arr2RakTujuan.Add(subArr)

					DGV_Data_TF.Rows(rows).Cells(itemDgvJumlah).ReadOnly = True
					DGV_Data_TF.Rows(rows).Cells(itemDgvBags).ReadOnly = True
					DGV_Data_TF.Rows(rows).Cells(itemDgvRakTujuan).ReadOnly = True

					If Dr("Jenis_Kemasan").ToString.ToUpper = "ORIGINAL BAGS" Then
						DGV_Data_TF.Rows(rows).Cells(itemDgvJumlah).ReadOnly = True
					End If

					If Dr("Metode_Pengeluaran_Stok").ToString.ToUpper = "FIFO" Then
						DGV_Data_TF.Rows(rows).Cells(itemDGVTglExp).Value = "-"
					Else
						DGV_Data_TF.Rows(rows).Cells(itemDGVTglExp).Value = If(General_Class.CekNULL(Dr("Tgl_Expired")) = "", "", Format(Dr("Tgl_Expired"), "dd MMM yyyy"))
					End If

					DGV_Data_TF.Rows(rows).Cells(itemDGVBarcode).Value = Dr("Barcode")
					DGV_Data_TF.Rows(rows).Cells(itemDGVFlagBlokSN).Value = General_Class.CekNULL(Dr("Blok_SN"))

					rows = rows + 1

				Loop
			End Using

			TxtTotalTransfer.Text = 0
			TxtTotalTransferBags.Text = 0
			TxtsisaRequest.Text = 0

			If Not TxtjmlPermintaanDisplay.Text.Trim.Length = 0 Then
				TxtsisaRequest.Text = TxtjmlPermintaanDisplay.Text
			Else
				TxtsisaRequest.Text = 0
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		kosong()
	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

		If Dgv_DataRekap.RowCount = 0 Then
			MessageBox.Show("Belum ada barang yang mau di transfer!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TxtKd_Barang.Focus() : Exit Sub
		ElseIf TxtKeterangan.Text.Trim.Length = 0 Then
			MessageBox.Show("Keterangan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TxtKeterangan.Focus() : Exit Sub
		ElseIf TxtKodeCost.Text.Trim.Length = 0 Or TxtIDCost.Text.Trim.Length = 0 Then
			MessageBox.Show("Cost Center harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TxtKeterangan.Focus() : Exit Sub
		ElseIf CmbSO_Asal.Text = CmbSo_Tujuan.Text Then
			MessageBox.Show("SO asal dan so tujuan sama", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			CmbSO_Asal.Focus() : Exit Sub
		End If

		If CmbJnsTransfer.SelectedIndex = 0 Then
			If CmbSO_Asal.SelectedIndex = -1 Then
				MessageBox.Show("SO asal harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				CmbSO_Asal.Focus() : Exit Sub
			End If
		Else
			If CmbSo_Tujuan.SelectedIndex = -1 Then
				MessageBox.Show("SO tujuan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				CmbSo_Tujuan.Focus() : Exit Sub
			End If

		End If

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim hasData As Boolean = False

			get_no_faktur()

			Dim lokasi_tujuan As String = ""

			If Not CmbSo_Tujuan.SelectedIndex = -1 Then
				lokasi_tujuan = arrSO(CmbSo_Tujuan.SelectedIndex)
			Else
				lokasi_tujuan = arrSO(CmbSO_Asal.SelectedIndex)
			End If

			Dim Flag_Stock_Rejected As String = ""
			If arrJenisPengeluaran(Cmb_JenisPengeluaran.SelectedIndex).ToString.ToUpper = "PENGELUARAN_STOCK" Then
				Flag_Stock_Rejected = "NULL"
			ElseIf arrJenisPengeluaran(Cmb_JenisPengeluaran.SelectedIndex).ToString.ToUpper = "PENGELUARAN_REJECT" Then

				Flag_Stock_Rejected = "'Y'"
			Else
				CloseTrans()
				CloseConn()
				MessageBox.Show("Terjadi Kesalaham pada Form", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			'==================================
			'=     INSERT PARENT TF STOCK     =
			'==================================
			SQL = "insert into EMI_Pengeluaran_Stock_parent_barang_lain (kode_perusahaan, No_faktur, Kode_Stock_Owner, Tanggal, Jam, "
			SQL = SQL & "UserID, Lokasi, Keterangan, Id_Cost_Center, Flag_Stock_Rejected, Kode_Account, ID_Account_Pengeluaran_Stock_Barang_Lain) "
			SQL = SQL & "Values ('" & KodePerusahaan & "', '" & Trim(TxtNo_Transaksi.Text) & "', '" & arrSO(CmbSO_Asal.SelectedIndex) & "', "
			SQL = SQL & "'" & tgl_skg & "', '" & tgl_skg.ToString("HH:mm:ss") & "', '" & UserID & "', "
			SQL = SQL & "'" & Cmb_Lokasi.Text & "', '" & TxtKeterangan.Text & "', '" & TxtIDCost.Text & "', " & Flag_Stock_Rejected & ","
			SQL = SQL & "'" & arr_kode_akun.Item(ComboBox1.SelectedIndex) & "', '" & arr_id_keterangan.Item(ComboBox1.SelectedIndex) & "' )"
			ExecuteTrans(SQL)

			For i As Integer = 0 To Dgv_DataRekap.Rows.Count - 1

				get_grid_view_Rekap(i)

				hasData = True

				Dim nilai_kecil As Double = 0
				SQL = "select dbo.ubah_satuan_lain('" & KodePerusahaan & "', 'masa','" & dgv_Rekap_KdBarang & "', '" & dgv_Rekap_Satuan & "',"
				SQL = SQL & "'" & dgv_Rekap_SatuanKecil & "', '" & HilangkanTanda(dgv_Rekap_JumlahBersih) & "' ) as hasil"
				Using Dr1 = OpenTrans(SQL)
					If Dr1.Read Then
						If General_Class.CekNULL(Dr1("hasil")) = "" Then
							Dr1.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("data konversi satuan kirim tidak ada ")
							Exit Sub
						End If

						nilai_kecil = Dr1("hasil")
					Else
						Dr1.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("data konversi satuan kirim tidak ada ")
						Exit Sub
					End If
				End Using

				Dim Jenis_Berat As String = ""
				SQL = "Select isnull(flag_tampil_berat,'T') as flag_tampil_berat from emi_satuan where "
				SQL = SQL & "satuan='" & dgv_Rekap_Satuan & "' and kode_perusahaan='" & KodePerusahaan & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						Jenis_Berat = dr("flag_tampil_berat")
					Else
						dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("data Satuan Tidak ada . . ! ! ")
						Exit Sub
					End If
				End Using

				Dim Jenis_kemasan As String = ""
				SQL = "Select Jenis_Kemasan from Barang_Lain where "
				SQL = SQL & "Kode_Barang='" & dgv_Rekap_KdBarang & "' and kode_perusahaan='" & KodePerusahaan & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						Jenis_kemasan = dr("Jenis_Kemasan")
					Else
						dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("data Satuan Tidak ada . . ! ! ")
						Exit Sub
					End If
				End Using

				Dim Flag_Timbang As String = ""
				If Jenis_kemasan.ToUpper = "ORIGINAL BAGS" Or Jenis_Berat = "T" Then
					Flag_Timbang = "T"
				Else
					Flag_Timbang = "Y"
				End If

				SQL = "insert into EMI_Pengeluaran_Stock_barang_lain (Kode_Perusahaan, No_faktur, Kode_Barang, Total, Satuan, "
				SQL = SQL & "Total_Barang, Satuan_Barang, Total_Bags, Flag_Timbang) values "
				SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(TxtNo_Transaksi.Text) & "', '" & dgv_Rekap_KdBarang & "', "
				SQL = SQL & "'" & HilangkanTanda(dgv_Rekap_JumlahBersih) & "', '" & dgv_Rekap_Satuan & "', "
				SQL = SQL & "'" & nilai_kecil & "', '" & dgv_Rekap_SatuanKecil & "', "
				SQL = SQL & "'" & HilangkanTanda(dgv_Rekap_JumlahBags) & "', '" & Flag_Timbang & "')"
				ExecuteTrans(SQL)

				Dim x_ident_current As Integer = 0
				SQL = "select IDENT_CURRENT('EMI_Pengeluaran_Stock_barang_lain') as urutan"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						x_ident_current = Dr("urutan")
					End If
				End Using

				SQL = "select kode_Perusahaan from EMI_Pengeluaran_Stock_barang_lain where "
				SQL = SQL & "kode_Perusahaan='" & KodePerusahaan & "' and "
				SQL = SQL & "No_Faktur='" & TxtNo_Transaksi.Text & "' and "
				SQL = SQL & "Kode_barang='" & dgv_Rekap_KdBarang & "' and "
				SQL = SQL & "urut_oto='" & x_ident_current & "' "
				Using Dr = OpenTrans(SQL)
					If Not Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Terjadi Kesalahan, Silahkan Ulangi Transaksi  . . ! ! ")
						Exit Sub
					End If
				End Using

				'tab 3 / pallet

				For j As Integer = 0 To Dgv_DataDetail.Rows.Count - 1

					get_grid_view_Detail(j)

					If Not dgv_Rekap_KdBarang = dgv_detail_KdBarang Then
						Continue For
					End If

					If dgv_detail_lokasi = dgv_Rekap_lokasi And dgv_detail_KdBarang = dgv_Rekap_KdBarang Then

						'======================================
						'=       CEK APAKAH SUDAH CETAK       =
						'======================================
						SQL = "select a.Kode_Perusahaan from EMI_Pengeluaran_Stock_parent_barang_lain a, EMI_Pengeluaran_Stock_Det_barang_lain b  where "
						SQL = SQL & "a.kode_Perusahaan=b.kode_Perusahaan And a.No_Faktur=b.no_faktur and "
						SQL = SQL & "b.Serial_Number_Awal = '" & dgv_detail_SN & "' and "
						SQL = SQL & "selesai is null and a.status is null "
						Using Dr = OpenTrans(SQL)
							If Dr.Read Then
								Dr.Close()
								CloseTrans()
								CloseConn()
								MessageBox.Show("Barang " & dgv_detail_Barcode & " belum melalui proses pencetakan barcode.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							Else
								Dr.Close()
							End If
						End Using

						Dim nilai_kecildetail As Double = 0
						SQL = "select dbo.ubah_satuan_lain('" & KodePerusahaan & "', 'masa','" & dgv_detail_KdBarang & "', '" & dgv_Rekap_Satuan & "',"
						SQL = SQL & "'" & dgv_Rekap_SatuanKecil & "', '" & HilangkanTanda(dgv_detail_Jumlah.ToString) & "' ) as hasil"
						Using Dr1 = OpenTrans(SQL)
							If Dr1.Read Then
								If General_Class.CekNULL(Dr1("hasil")) = "" Then
									Dr1.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("data konversi satuan kirim tidak ada ")
									Exit Sub
								End If

								nilai_kecildetail = Dr1("hasil")
							Else
								Dr1.Close()
								CloseTrans()
								CloseConn()
								MessageBox.Show("data konversi satuan kirim tidak ada ")
								Exit Sub
							End If
						End Using

						SQL = "insert into EMI_Pengeluaran_Stock_Det_barang_lain(Kode_Perusahaan, No_faktur, Id_Wms_Awal, No_Pallet_Awal, "
						SQL = SQL & "Serial_Number_Awal, Jumlah, Jumlah_Barang, Jumlah_Bags, Warna, Urut_TF) values( "
						SQL = SQL & "'" & KodePerusahaan & "', '" & Trim(TxtNo_Transaksi.Text) & "', '" & dgv_detail_IDWarehouseAwal & "', "
						SQL = SQL & "'" & dgv_detail_IDPalletAwal & "', '" & dgv_detail_SN & "', "
						SQL = SQL & "'" & HilangkanTanda(dgv_detail_Jumlah.ToString) & "', '" & nilai_kecildetail & "', "
						SQL = SQL & "'" & HilangkanTanda(dgv_detail_JumlahBags) & "', "
						SQL = SQL & "'" & dgv_detail_Warna & "', '" & x_ident_current & "')"
						ExecuteTrans(SQL)
						'end tab 3 / pallet

					End If

				Next

			Next

#Region "Potong Stock dan Jurnal"

			'            Dim Jenis_Berat As String = ""
			'            SQL = "Select isnull(flag_tampil_berat,'T') as flag_tampil_berat from emi_satuan where "
			'            SQL = SQL & "satuan='" & TxtSatuan.Text & "' and kode_perusahaan='" & KodePerusahaan & "' "
			'            Using dr = OpenTrans(SQL)
			'                If dr.Read Then
			'                    Jenis_Berat = dr("flag_tampil_berat")
			'                Else
			'                    dr.Close()
			'                    CloseTrans()
			'                    CloseConn()
			'                    MessageBox.Show("data Satuan Tidak ada . . ! ! ")
			'                    Exit Sub
			'                End If
			'            End Using

			'            'Dim isCheck As Boolean = False
			'            Dim nilai_persediaan_min As Double = 0

			'            For row As Integer = 0 To DGV_Data_TF.RowCount - 1

			'                get_grid_view(row)

			'                If dgv_CheckBox = False Then
			'                    Continue For
			'                End If

			'                If dgv_Jumlah = "" Or dgv_JmlhBags = "" Then
			'                    CloseTrans()
			'                    CloseConn()
			'                    MessageBox.Show("Jumlah harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                    Exit Sub
			'                ElseIf CType(DGV_Data_TF.Rows(row).Cells(itemDgvRakTujuan), DataGridViewComboBoxCell).Value = "" Then
			'                    CloseTrans()
			'                    CloseConn()
			'                    MessageBox.Show("Rak Tujuan harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                    Exit Sub
			'                End If

			'                isCheck = True

			'                Dim comboBoxCell As DataGridViewComboBoxCell = CType(DGV_Data_TF.Rows(row).Cells(itemDgvRakTujuan), DataGridViewComboBoxCell)
			'                Dim selectedIndex As Integer = comboBoxCell.Items.IndexOf(comboBoxCell.Value)

			'                Dim nilai_kecildetail As Double = 0
			'                SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & TxtKd_Barang.Text & "', '" & TxtSatuan.Text & "',"
			'                SQL = SQL & "'" & TxtSatuanKecil.Text & "', '" & HilangkanTanda(dgv_Jumlah.ToString) & "' ) as hasil"
			'                Using Dr1 = OpenTrans(SQL)
			'                    If Dr1.Read Then
			'                        If General_Class.CekNULL(Dr1("hasil")) = "" Then
			'                            Dr1.Close()
			'                            CloseTrans()
			'                            CloseConn()
			'                            MessageBox.Show("data konversi satuan kirim tidak ada ")
			'                            Exit Sub
			'                        End If

			'                        nilai_kecildetail = Dr1("hasil")
			'                    Else
			'                        Dr1.Close()
			'                        CloseTrans()
			'                        CloseConn()
			'                        MessageBox.Show("data konversi satuan kirim tidak ada ")
			'                        Exit Sub
			'                    End If
			'                End Using

			'                Dim palletTujuan As Double = 0
			'                SQL = "Select Top(1) nomor_urut from view_warehouse_position_detail where "
			'                SQL = SQL & "kode_Perusahaan ='" & KodePerusahaan & "' and kode_barang is null and "
			'                SQL = SQL & "id_wms_warehouse_position = '" & arr2RakTujuan(row)(selectedIndex) & "' "
			'                SQL = SQL & "order by nomor_urut "
			'                Using dr = OpenTrans(SQL)
			'                    If dr.Read Then
			'                        palletTujuan = dr("nomor_urut")
			'                    Else
			'                        dr.Close()
			'                        CloseTrans()
			'                        CloseConn()
			'                        MessageBox.Show("data Rak Sudah Penuh . . ! ! ")
			'                        Exit Sub
			'                    End If
			'                End Using

			'                Dim flag_pot_stock As String = "NULL"
			'                Dim jumlah_pot_stock As String = "0"
			'                Dim sn As String = "NULL"

			'                If Jenis_Berat = "T" Then
			'                    flag_pot_stock = "'T'"
			'                    jumlah_pot_stock = nilai_kecildetail

			'                    SQL = "update barang_sn set jumlah = jumlah-'" & nilai_kecildetail & "', Jumlah_Bags = Jumlah_Bags-" & dgv_JmlhBags & " "
			'                    SQL = SQL & "where Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' and Kode_Barang='" & dgv_KodeBarang & "' "
			'                    SQL = SQL & "and Serial_Number='" & dgv_SerialNumber & "'"
			'                    ExecuteTrans(SQL)

			'                    SQL = "update barang set Good_Stock= Good_Stock-" & nilai_kecildetail & ", Jumlah_Bags = Jumlah_Bags-" & dgv_JmlhBags & " "
			'                    SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
			'                    SQL = SQL & " and Kode_Barang='" & kd_barang & "'"
			'                    ExecuteTrans(SQL)

			'                    Dim nilai_Per_Row As Double = 0
			'                    SQL = "select round(dbo.get_hpp(serial_number) * " & nilai_kecildetail & ", 2) as rp_persediaan_min from barang_sn where "
			'                    SQL = SQL & "Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' and Kode_Barang='" & dgv_KodeBarang & "' "
			'                    SQL = SQL & "and Serial_Number='" & dgv_SerialNumber & "'"
			'                    Using dr = OpenTrans(SQL)
			'                        If dr.Read Then
			'                            nilai_Per_Row = dr("rp_persediaan_min")
			'                        Else
			'                            dr.Close()
			'                            CloseTrans()
			'                            CloseConn()
			'                            MessageBox.Show("Data SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                            Exit Sub
			'                        End If
			'                    End Using

			'                    nilai_persediaan_min += nilai_Per_Row

			'                    Dim hargaIsn As String = ""
			'                    Dim QrLama As String = ""
			'                    Dim batchLama As String = ""
			'                    Dim namaBarang As String = ""
			'                    Dim expDate As String = ""

			'                    'Ambil Data Lama
			'                    SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired "
			'                    SQL = SQL & "from barang_sn a, barang b "
			'                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
			'                    SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
			'                    SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
			'                    SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
			'                    SQL = SQL & "and a.Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
			'                    SQL = SQL & "and a.Kode_Barang ='" & dgv_KodeBarang & "' "
			'                    SQL = SQL & "and a.Serial_Number='" & dgv_SerialNumber & "' "
			'                    Using Dr = OpenTrans(SQL)
			'                        If Dr.Read Then
			'                            hargaIsn = Get_Harga_SN(Dr("Serial_Number"))
			'                            QrLama = General_Class.CekNULL(Dr("Qr_Code"))
			'                            batchLama = General_Class.CekNULL(Dr("Batch_Number"))
			'                            namaBarang = General_Class.CekNULL(Dr("Nama"))
			'                            expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
			'                        Else
			'                            Dr.Close()
			'                            CloseTrans()
			'                            CloseConn()
			'                            MessageBox.Show("Data Tidak ada")
			'                            Exit Sub
			'                        End If
			'                    End Using

			'                    'GENERATE SN BARU
			'                    Dim Random As New Random()
			'                    Dim str As String = Format(Random.Next(0, 999), "000") & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HHmmss")
			'                    Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
			'                    Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(DateTime.Now, "yyyy-MM-dd")

			'                    Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)
			'                    sn = "'" & SN_Baru & "'"

			'                    'INSERT BARANG SN BARU
			'                    SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, "
			'                    SQL = SQL & "Jumlah, Jumlah_Bags, Tgl_Expired, Tgl_Produksi, "
			'                    SQL = SQL & "Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, "
			'                    SQL = SQL & "Kode_Unik_Asal, Nomor_Pallet, batch_number, warna) "
			'                    SQL = SQL & "select Kode_Perusahaan, '" & lokasi_tujuan & "', Kode_Barang, '" & SN_Baru & "', "
			'                    'SQL = SQL & "'" & nilai_kecildetail & "', Warning_Stock, Bad_Stock, " & dgv_JmlhBags & ", "
			'                    SQL = SQL & "'" & nilai_kecildetail & "', " & dgv_JmlhBags & ", "
			'                    SQL = SQL & "Tgl_Expired, Tgl_Produksi, '" & arr2RakTujuan(row)(selectedIndex) & "', "
			'                    SQL = SQL & "id_Susunan, Qr_Code, '" & newKodeUnikBerjalan & "', Kode_Unik_Asal, '" & palletTujuan & "', batch_number, '" & dgv_Warna & "' "
			'                    SQL = SQL & "from Barang_SN "
			'                    SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
			'                    SQL = SQL & "and Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
			'                    SQL = SQL & "and Kode_Barang='" & dgv_KodeBarang & "' "
			'                    SQL = SQL & "and Serial_Number='" & dgv_SerialNumber & "' "
			'                    ExecuteTrans(SQL)

			'                    '============================
			'                    '=       TAMBAH STOCK       =
			'                    '============================

			'                    SQL = "update barang set Good_Stock= Good_Stock + " & nilai_kecildetail & ", Jumlah_Bags = Jumlah_Bags + " & dgv_JmlhBags & " "
			'                    SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & lokasi_tujuan & "' "
			'                    SQL = SQL & " and Kode_Barang='" & dgv_KodeBarang & "'"
			'                    ExecuteTrans(SQL)

			'                    'CEK KESESUAIAN STOCK
			'                    SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
			'                    SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
			'                    SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
			'                    SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
			'                    SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
			'                    SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
			'                    SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & lokasi_tujuan & "' "
			'                    SQL = SQL & "AND a.Kode_Barang = '" & dgv_KodeBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
			'                    SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
			'                    Using Ds = BindingTrans(SQL)
			'                        With Ds.Tables("MyTable")
			'                            If .Rows.Count <> 0 Then
			'                                If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
			'                                    CloseTrans()
			'                                    CloseConn()
			'                                    MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                                    Exit Sub
			'                                End If
			'                            Else
			'                                CloseTrans()
			'                                CloseConn()
			'                                MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                                Exit Sub
			'                            End If
			'                        End With
			'                    End Using

			'                End If

			'                SQL = "insert into EMI_Pengeluaran_Stock_det (Kode_Perusahaan, No_Faktur, Serial_Number_Awal, Serial_Number_Akhir, "
			'                SQL = SQL & "Id_Wms_Awal, Id_Wms_Tujuan, Jumlah, Jumlah_Bags, Satuan ,No_Pallet_Awal, No_Pallet_Tujuan, "
			'                SQL = SQL & "Flag_Pot_Stock, jumlah_pot_Stock, warna) values "
			'                SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(TxtNo_Transaksi.Text) & "', "
			'                SQL = SQL & "'" & dgv_SerialNumber & "', " & sn & ", " & dgv_IDWareHouse & ", " & arr2RakTujuan(row)(selectedIndex) & ", '" & nilai_kecildetail & "', " & dgv_JmlhBags & ", "
			'                SQL = SQL & "'" & TxtSatuanKecil.Text & "','" & dgv_IDPallet & "', " & palletTujuan & ", "
			'                SQL = SQL & "" & flag_pot_stock & "," & jumlah_pot_stock & ", "
			'                SQL = SQL & "'" & dgv_Warna & "')"
			'                ExecuteTrans(SQL)

			'                '============================
			'                '=       UPDATE STOCK       =
			'                '============================

			'                'Dim jumlahAkhir As Double = Val(dgv_GoodStock) - Val(dgv_Jumlah)

			'                'SQL = "update barang_sn set jumlah = jumlah-'" & nilai_kecildetail & "', Jumlah_Bags = Jumlah_Bags-" & dgv_JmlhBags & " "
			'                'SQL = SQL & "where Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' and Kode_Barang='" & dgv_KodeBarang & "' "
			'                'SQL = SQL & "and Serial_Number='" & dgv_SerialNumber & "'"
			'                'ExecuteTrans(SQL)

			'                'SQL = "update barang set Good_Stock= Good_Stock-" & nilai_kecildetail & ", Jumlah_Bags = Jumlah_Bags-" & dgv_JmlhBags & " "
			'                'SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
			'                'SQL = SQL & " and Kode_Barang='" & kd_barang & "'"
			'                'ExecuteTrans(SQL)

			'            Next

			'            'Cek STOCK BARANG
			'            SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
			'            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
			'            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
			'            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
			'            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
			'            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
			'            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
			'            SQL = SQL & "AND a.Kode_Barang = '" & kd_barang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
			'            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
			'            Using Ds = BindingTrans(SQL)
			'                With Ds.Tables("MyTable")
			'                    If .Rows.Count <> 0 Then
			'                        If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
			'                            CloseTrans()
			'                            CloseConn()
			'                            MessageBox.Show("Terjadi Kesalahan Pada SN . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                            Exit Sub
			'                        End If
			'                    Else
			'                        CloseTrans()
			'                        CloseConn()
			'                        MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                        Exit Sub
			'                    End If
			'                End With
			'            End Using

			'            SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
			'            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
			'            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
			'            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
			'            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
			'            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
			'            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner =  "
			'            SQL = SQL & "'" & lokasi_tujuan & "' "
			'            SQL = SQL & "AND a.Kode_Barang = '" & kd_barang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
			'            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
			'            Using Ds = BindingTrans(SQL)
			'                With Ds.Tables("MyTable")
			'                    If .Rows.Count <> 0 Then
			'                        If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
			'                            CloseTrans()
			'                            CloseConn()
			'                            MessageBox.Show("Terjadi Kesalahan Pada SN . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                            Exit Sub
			'                        End If
			'                    Else
			'                        CloseTrans()
			'                        CloseConn()
			'                        MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                        Exit Sub
			'                    End If
			'                End With
			'            End Using

			'            'dari
			'            'JURNAL
			'            If Jenis_Berat = "T" Then
			'                Dim fRaw_Material_dari As String = ""
			'                Dim fFinished_Good_dari As String = ""
			'                Dim fSemi_FG_dari As String = ""
			'                Dim fScrap_dari As String = ""
			'                Dim fPackaging_dari As String = ""
			'                Dim akun_persediaan_dari As String = ""

			'                Dim fRaw_Material_tujuan As String = ""
			'                Dim fFinished_Good_tujuan As String = ""
			'                Dim fSemi_FG_tujuan As String = ""
			'                Dim fScrap_tujuan As String = ""
			'                Dim akun_persediaan_tujuan As String = ""
			'                Dim fPackaging_tujuan As String = ""
			'                Dim inisial_faktur_dari As String = ""

			'                SQL = "select a.Flag_Raw_Material,a.Flag_Finished_Good,a.Flag_Semi_FG,a.Flag_Scrap, a.Flag_Packaging "
			'                SQL = SQL & "from Barang b,EMI_Group_Jenis a where a.Kode_Perusahaan = b.Kode_Perusahaan "
			'                SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'                SQL = SQL & "and b.kode_stock_owner = '" & arrSO(CmbSO_Asal.SelectedIndex) & "' and b.Kode_Barang='" & TxtKd_Barang.Text & "' "
			'                Using Dr = OpenTrans(SQL)
			'                    If Dr.Read Then
			'                        fRaw_Material_dari = Dr("Flag_Raw_Material")
			'                        fFinished_Good_dari = Dr("Flag_Finished_Good")
			'                        fSemi_FG_dari = Dr("Flag_Semi_FG")
			'                        fScrap_dari = Dr("Flag_Scrap")
			'                        fPackaging_dari = Dr("Flag_Packaging")
			'                    Else
			'                        Dr.Close()
			'                        CloseTrans()
			'                        CloseConn()
			'                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                        Exit Sub
			'                    End If
			'                End Using

			'                SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
			'                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
			'                Using Dr = OpenTrans(SQL)
			'                    If Dr.Read Then
			'                        'akun_persediaan_dari = Dr("persediaan")
			'                        inisial_faktur_dari = Dr("inisial_faktur")
			'                        If fRaw_Material_dari = "Y" Then
			'                            akun_persediaan_dari = Dr("Persediaan_Bahan_Baku")
			'                        ElseIf fFinished_Good_dari = "Y" Then
			'                            akun_persediaan_dari = Dr("Persediaan")
			'                        ElseIf fSemi_FG_dari = "Y" Then
			'                            akun_persediaan_dari = Dr("Persediaan_Bahan_Setengah_Jadi")
			'                        ElseIf fScrap_dari = "Y" Then
			'                            akun_persediaan_dari = Dr("Persediaan_Scrap")
			'                        ElseIf fPackaging_dari = "Y" Then
			'                            akun_persediaan_dari = Dr("Persediaan_Packaging")
			'                        Else
			'                            Dr.Close()
			'                            CloseTrans()
			'                            CloseConn()
			'                            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                            Exit Sub
			'                        End If
			'                    Else
			'                        Dr.Close()
			'                        CloseTrans()
			'                        CloseConn()
			'                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                        Exit Sub
			'                    End If
			'                End Using

			'                SQL = "select a.Flag_Raw_Material,a.Flag_Finished_Good,a.Flag_Semi_FG,a.Flag_Scrap, a.Flag_Packaging "
			'                SQL = SQL & "from Barang b,EMI_Group_Jenis a where a.Kode_Perusahaan = b.Kode_Perusahaan "
			'                SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'                SQL = SQL & "and b.kode_stock_owner = '" & lokasi_tujuan & "' and b.Kode_Barang='" & TxtKd_Barang.Text & "' "
			'                Using Dr = OpenTrans(SQL)
			'                    If Dr.Read Then
			'                        fRaw_Material_tujuan = Dr("Flag_Raw_Material")
			'                        fFinished_Good_tujuan = Dr("Flag_Finished_Good")
			'                        fSemi_FG_tujuan = Dr("Flag_Semi_FG")
			'                        fScrap_tujuan = Dr("Flag_Scrap")
			'                        fPackaging_tujuan = Dr("Flag_Packaging")
			'                    Else
			'                        Dr.Close()
			'                        CloseTrans()
			'                        CloseConn()
			'                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                        Exit Sub
			'                    End If
			'                End Using

			'                SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
			'                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & lokasi_tujuan & "' "
			'                Using Dr = OpenTrans(SQL)
			'                    If Dr.Read Then
			'                        'akun_persediaan_dari = Dr("persediaan")
			'                        If fRaw_Material_tujuan = "Y" Then
			'                            akun_persediaan_tujuan = Dr("Persediaan_Bahan_Baku")
			'                        ElseIf fFinished_Good_tujuan = "Y" Then
			'                            akun_persediaan_tujuan = Dr("Persediaan")
			'                        ElseIf fSemi_FG_tujuan = "Y" Then
			'                            akun_persediaan_tujuan = Dr("Persediaan_Bahan_Setengah_Jadi")
			'                        ElseIf fScrap_tujuan = "Y" Then
			'                            akun_persediaan_tujuan = Dr("Persediaan_Scrap")
			'                        ElseIf fPackaging_tujuan = "Y" Then
			'                            akun_persediaan_tujuan = Dr("Persediaan_Packaging")
			'                        Else
			'                            Dr.Close()
			'                            CloseTrans()
			'                            CloseConn()
			'                            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                            Exit Sub
			'                        End If
			'                    Else
			'                        Dr.Close()
			'                        CloseTrans()
			'                        CloseConn()
			'                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                        Exit Sub
			'                    End If
			'                End Using

			'                Dim Kode_voucher As String = ""
			'                Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
			'                Dim pagenumber As Integer = 1

			'                SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
			'                SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
			'                SQL = SQL & "'" & Kode_voucher & "', "
			'                SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
			'                SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
			'                SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock " & Trim(TxtNo_Transaksi.Text) & "', '', "
			'                SQL = SQL & "'-', '" & UserID & "')"
			'                ExecuteTrans(SQL)

			'                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
			'                          Strings.Mid(akun_persediaan_dari, 2, 1),
			'                          Strings.Mid(Ganti(akun_persediaan_dari), 3),
			'                          KodePerusahaan, KodeProyek, "Persedian " & Trim(TxtNo_Transaksi.Text), "0", nilai_persediaan_min, pagenumber, "TSSS")
			'                ExecuteTrans(SQL)
			'                pagenumber = pagenumber + 1

			'                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
			'                         Strings.Mid(akun_persediaan_tujuan, 2, 1),
			'                         Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
			'                         KodePerusahaan, KodeProyek, "Persedian " & Trim(TxtNo_Transaksi.Text), nilai_persediaan_min, "0", pagenumber, "TSSS")
			'                ExecuteTrans(SQL)
			'                pagenumber = pagenumber + 1

			'                SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
			'                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
			'                SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
			'                Using Dr = OpenTrans(SQL)
			'                    If Dr.Read Then
			'                        If Dr("debit") <> Dr("kredit") Then
			'                            Dr.Close()
			'                            CloseTrans()
			'                            CloseConn()
			'                            MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                            Exit Sub
			'                        End If
			'                    Else
			'                        Dr.Close()
			'                        CloseTrans()
			'                        CloseConn()
			'                        MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                        Exit Sub
			'                    End If
			'                End Using

			'                SQL = "update EMI_Pengeluaran_Stock set kode_voucher = '" & Kode_voucher & "' "
			'                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
			'                SQL = SQL & "and Kode_Transfer = '" & Trim(TxtNo_Transaksi.Text) & "' "
			'                ExecuteTrans(SQL)
			'            End If

#End Region

			'==============================
			'=     UPDATE FLAG TAMPIL     =
			''=============================
			'SQL = "update Emi_Material_Requisition_Det_Convert set Flag_Transfer = 'Y' where Urut_Oto = '" & Txt_OtoMaterial_req.Text & "' "
			'ExecuteTrans(SQL)

			If hasData <> True Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Tidak Ada Data yang Dikirim . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			Cmd.Transaction.Commit()
			MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'=================================
		'=     CETAK FAKTUR TF STOCK     =
		'=================================
		Try
			OpenConn()

			Dim CrDoc As New Object
			Dim kertas As String = ""

			SQL = "select a.Kode_Perusahaan "
			SQL = SQL & "from Vw_Pengeluaran_Stock a where "
			SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Faktur = '" & Trim(TxtNo_Transaksi.Text) & "' "
			Using Ds = BindingTrans(SQL)
				If Ds.Tables("MyTable").Rows.Count <> 0 Then

					CrDoc = New Rpt_EMI_Faktur_Pengeluaran_Stock_Lain
					kertas = "Faktur"

					'With A_Place_For_Printing2
					'    CrDoc.SetDataSource(Ds)
					'    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
					'    CrDoc.PrintOptions.PrinterName = ""
					'    CrDoc.RecordSelectionFormula = "{Vw_EMI_Pengeluaran_Stock.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_EMI_Pengeluaran_Stock.No_Faktur}='" & Trim(TxtNo_Transaksi.Text) & "' "
					'    CrDoc.SummaryInfo.ReportTitle = "TF"
					'    .Text = "TF"
					'    .CrystalReportViewer1.ReportSource = CrDoc
					'    .Refresh()
					'    .Show()
					'End With

					'============================================================================================================================================
					'============================================================================================================================================
					CrDoc.SetDataSource(Ds)
					CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
					CrDoc.PrintOptions.PrinterName = PrinterNameTS
					CrDoc.RecordSelectionFormula = "{Vw_Pengeluaran_Stock.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_Pengeluaran_Stock.No_Faktur}='" & Trim(TxtNo_Transaksi.Text) & "' "
					'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

					Dim doctoprint As New System.Drawing.Printing.PrintDocument()
					doctoprint.PrinterSettings.PrinterName = PrinterNameTS
					'doctoprint.DefaultPageSettings.Landscape = True
					Dim rawKind As Integer
					CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
					For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
						If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
							rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
							CrDoc.PrintOptions.PaperSize = rawKind
							Exit For
						End If
					Next

					CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
					CrDoc.PrintToPrinter(1, False, 1, 99)

					MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Dim TanyaInput As String = MessageBox.Show("Lanjut Input . . ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
		If TanyaInput = vbYes Then
			CmbJnsTransfer.Enabled = False
			CmbSO_Asal.Enabled = False
			CmbSo_Tujuan.Enabled = False

			TxtKd_Barang.Enabled = True
			Btn_GetData.Enabled = True

			Txt_OtoMaterial_req.Text = ""
			Txt_JumlahPermintaan.Text = ""
			Txt_SatuanPermintaan.Text = ""
			TxtSatuanKecil.Text = ""
			Txt_Warna.Text = ""
			TxtSatuan.Text = ""
			TxtStock.Text = ""
			TxtKeterangan.Text = ""
			TxtKd_Barang.Text = ""
			Txt_SO.Text = ""
			TxtNm_Barang.Text = ""
			TxtBags.Text = ""
			TxtTotalTransferBags.Text = ""

			TxtjmlPermintaanDisplay.Text = ""
			TxtStockDisplay.Text = ""
			TxtMetPotStok.Text = ""
			TxtJenisBags.Text = ""
			asal = "Transfer_Stock_3"
			DGV_Data_TF.Rows.Clear()
			Dgv_DataRekap.Rows.Clear()
			Dgv_DataDetail.Rows.Clear()

			Try
				OpenConn()
				get_no_faktur()
				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try
		Else
			kosong()
		End If

	End Sub

	Private Sub CmbSO_Asal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSO_Asal.SelectedIndexChanged, Cmb_JenisPengeluaran.SelectedIndexChanged

		If CmbSO_Asal.SelectedIndex = -1 Then Exit Sub

		If CmbJnsTransfer.SelectedIndex = -1 Then
			MessageBox.Show("Pilih Jenis Transfer Dahulu", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			CmbSO_Asal.SelectedIndex = -1
			Exit Sub
		End If

		Try
			OpenConn()

			SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From  "
			SQL = SQL & "Stock_Owner_Gudang_Lain a  where a.kode_perusahaan = '" & KodePerusahaan & "'  "
			SQL = SQL & "and aktif = 'Y' and (flag_produksi='Y' or Flag_Penyimpanan='Y')  "
			'   SQL = SQL & "and a.kode_perusahaan = b.kode_perusahaan and a.kode_stock_owner = b.kode_stock_owner_gudang "
			'  SQL = SQL & "and user_id = '" & UserID & "' "
			SQL = SQL & "and a.kode_stock_owner = '" & CmbSO_Asal.Text & "' "
			SQL = SQL & "group by kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan "
			SQL = SQL & "order by kode_stock_owner"
			Using dr = OpenTrans(SQL)
				If Not dr.Read Then
					MessageBox.Show("Anda tidak memiliki akses untuk gudang ini", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					CmbSO_Asal.SelectedIndex = -1
					Exit Sub
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Lv_DetBarang.Items.Clear()
		Lv_DetBarang.Location = New Point(1278, 277)
		Lv_DetBarang.Visible = False

		DGV_Data_TF.Rows.Clear()
		TxtKd_Barang.Text = ""
		Txt_SO.Text = ""
		TxtNm_Barang.Text = ""
		CmbSo_Tujuan.SelectedIndex = -1
	End Sub

	Private Sub CmbSo_Tujuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSo_Tujuan.SelectedIndexChanged

		If CmbSo_Tujuan.SelectedIndex = -1 Then Exit Sub

		If arrSO(CmbSo_Tujuan.SelectedIndex) = arrSO(CmbSO_Asal.SelectedIndex) Then
			MessageBox.Show("Lokasi Tujuan Tidak Boleh Sama Dengan Lokasi Awal", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			CmbSo_Tujuan.SelectedIndex = -1
			Exit Sub
		End If

		DGV_Data_TF.Rows.Clear()
		TxtKd_Barang.Text = ""
		Txt_SO.Text = ""
		TxtNm_Barang.Text = ""
	End Sub

	Private Sub GetGrandTotal()
		If DGV_Data_TF.Rows.Count = 0 Then Exit Sub

		Dim total As Double = 0
		Dim totalBags As Double = 0
		Dim request As Double = 0

		For i As Integer = 0 To DGV_Data_TF.Rows.Count - 1
			get_grid_view(i)
			If DGV_Data_TF.Rows(i).Cells(itemDgvCheckBox).Value = "False" Then
				Continue For
			End If

			total = total + Val(HilangkanTanda(dgv_Jumlah))
			totalBags = totalBags + Val(HilangkanTanda(dgv_JmlhBags))

		Next

		TxtTotalTransfer.Text = Format(total, "N2")
		TxtTotalTransferBags.Text = Format(totalBags, "N2")

		If Not TxtjmlPermintaanDisplay.Text.Trim.Length = 0 Then
			request = Val(HilangkanTanda(TxtjmlPermintaanBersih.Text))
			TxtsisaRequest.Text = Format((request - total), "N2") & " KG"
		Else
			TxtsisaRequest.Text = 0
		End If

	End Sub

	Public Shared Function isNull(ByVal xNullString As String) As String
		Try
			If String.IsNullOrWhiteSpace(xNullString) Then
				Return "0"
			Else
				Return xNullString
			End If
		Catch ex As Exception
			MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return "0"
		End Try
	End Function

	Private Function Generate_Random_Kode(ByVal length As Integer) As String
		Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
		Dim result As New StringBuilder()

		For i As Integer = 1 To length
			Dim index As Integer = random.Next(0, chars.Length)
			result.Append(chars(index))
		Next

		Return result.ToString()
	End Function

	Private Sub DGV_Data_TF_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Data_TF.CellLeave
		If DGV_Data_TF.CurrentCell.ColumnIndex = itemDgvBags Then
			Dim cellKuantity As String = DGV_Data_TF.CurrentCell.Value

			If Not String.IsNullOrEmpty(cellKuantity) Then

				Dim nilai As Decimal = Decimal.Parse(cellKuantity)
				Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

				DGV_Data_TF.CurrentCell.Value = formattedValue
			End If
		ElseIf DGV_Data_TF.CurrentCell.ColumnIndex = itemDgvJumlah Then
			Dim cellKuantity As String = DGV_Data_TF.CurrentCell.Value

			If Not String.IsNullOrEmpty(cellKuantity) Then

				Dim nilai As Decimal = Decimal.Parse(cellKuantity)
				Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

				DGV_Data_TF.CurrentCell.Value = formattedValue
			End If
		End If

	End Sub

	Private Sub Btn_SimpanSementara_Click(sender As Object, e As EventArgs) Handles Btn_SimpanSementara.Click
		If DGV_Data_TF.Rows.Count = 0 Then Exit Sub

		Dim DgvTab2_Rows As Integer = Dgv_DataRekap.Rows.Count
		Dim DgvTab3_Rows As Integer = Dgv_DataDetail.Rows.Count

		Dim hasDataSelected As Boolean = False

		For row As Integer = 0 To DGV_Data_TF.RowCount - 1

			get_grid_view(row)
			If dgv_CheckBox = False Then
				Continue For
			End If

			If String.IsNullOrWhiteSpace(dgv_Jumlah) Or String.IsNullOrWhiteSpace(dgv_JmlhBags) Then
				MessageBox.Show("Jumlah atau Bags Harus Di Input", "Transfer Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			If dgv_FlagBlokSN = "Y" Then
				MessageBox.Show("SN pada Pallet Di Block, Pallet Tidak Bisa di Transfer " & vbNewLine & " Barcode : " & dgv_Barcode, "Transfer Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If
		Next

		For row As Integer = 0 To DGV_Data_TF.RowCount - 1

			get_grid_view(row)
			If dgv_CheckBox = False Then
				Continue For
			End If

			hasDataSelected = True

			If String.IsNullOrWhiteSpace(dgv_Jumlah) Or String.IsNullOrWhiteSpace(dgv_JmlhBags) Then
				MessageBox.Show("Jumlah atau Bags Harus Di Input", "Transfer Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			If dgv_FlagBlokSN = "Y" Then
				MessageBox.Show("SN pada Pallet Di Block, Pallet Tidak Bisa di Transfer " & vbNewLine & " Barcode : " & dgv_Barcode, "Transfer Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			'tab 3
			'If DGV_Data_TF.Rows(row).Cells(itemDgvRakTujuan).Value = "" Then
			'    MessageBox.Show("Rak Tujuan Harus Diisi", "Transfer Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    For i As Integer = Dgv_DataRekap.Rows.Count - 1 To 0 Step -1
			'        If Dgv_DataRekap.Rows(i).Cells(itemDgvRekap_KdBarang).Value IsNot Nothing AndAlso Dgv_DataRekap.Rows(i).Cells(itemDgvRekap_KdBarang).Value.ToString() = dgv_KodeBarang Then
			'            Dgv_DataRekap.Rows.RemoveAt(i)
			'        End If
			'    Next
			'    For i As Integer = Dgv_DataDetail.Rows.Count - 1 To 0 Step -1
			'        If Dgv_DataDetail.Rows(i).Cells(itemDgvDetail_KdBarang).Value IsNot Nothing AndAlso Dgv_DataDetail.Rows(i).Cells(itemDgvDetail_KdBarang).Value.ToString() = dgv_KodeBarang Then
			'            Dgv_DataDetail.Rows.RemoveAt(i)
			'        End If
			'    Next
			'    Exit Sub
			'End If

			'Dim comboBoxCell As DataGridViewComboBoxCell = CType(DGV_Data_TF.Rows(row).Cells(itemDgvRakTujuan), DataGridViewComboBoxCell)
			'Dim selectedIndex As Integer = comboBoxCell.Items.IndexOf(comboBoxCell.Value)

			Dgv_DataDetail.Rows.Add(1)
			Dgv_DataDetail.Rows(DgvTab3_Rows).Cells(itemDgvDetail_lokasi).Value = dgv_Lokasi
			Dgv_DataDetail.Rows(DgvTab3_Rows).Cells(itemDgvDetail_KdBarang).Value = dgv_KodeBarang
			Dgv_DataDetail.Rows(DgvTab3_Rows).Cells(itemDgvDetail_SN).Value = dgv_SerialNumber
			Dgv_DataDetail.Rows(DgvTab3_Rows).Cells(itemDgvDetail_NmBarang).Value = dgv_Nama
			Dgv_DataDetail.Rows(DgvTab3_Rows).Cells(itemDgvDetail_IDWarehouseAwal).Value = dgv_IDWareHouse
			Dgv_DataDetail.Rows(DgvTab3_Rows).Cells(itemDgvDetail_KodeRakAwal).Value = dgv_KodeRak
			Dgv_DataDetail.Rows(DgvTab3_Rows).Cells(itemDgvDetail_IDPalletAwal).Value = dgv_IDPallet
			Dgv_DataDetail.Rows(DgvTab3_Rows).Cells(itemDgvDetail_Jumlah).Value = dgv_Jumlah
			Dgv_DataDetail.Rows(DgvTab3_Rows).Cells(itemDgvDetail_JumlahBags).Value = dgv_JmlhBags
			Dgv_DataDetail.Rows(DgvTab3_Rows).Cells(itemDgvDetail_KodeRakTujuan).Value = "" 'dgv_RakTujuan
			Dgv_DataDetail.Rows(DgvTab3_Rows).Cells(itemDgvDetail_IDWarehouseTujuan).Value = "" 'arr2RakTujuan(row)(selectedIndex)
			Dgv_DataDetail.Rows(DgvTab3_Rows).Cells(itemDgvDetail_Warna).Value = dgv_Warna
			Dgv_DataDetail.Rows(DgvTab3_Rows).Cells(itemDgvDetail_JenisKemasan).Value = dgv_JenisKemasan
			Dgv_DataDetail.Rows(DgvTab3_Rows).Cells(itemDgvDetail_TglProduksi).Value = dgv_TglProd
			Dgv_DataDetail.Rows(DgvTab3_Rows).Cells(itemDgvDetail_TglExpired).Value = dgv_TglExp
			Dgv_DataDetail.Rows(DgvTab3_Rows).Cells(itemDgvDetail_JenisKualitas).Value = dgv_KetWarna
			Dgv_DataDetail.Rows(DgvTab3_Rows).Cells(itemDgvDetail_Barcode).Value = dgv_Barcode

			DgvTab3_Rows += 1

		Next

		If hasDataSelected Then

			'tab 2
			Dgv_DataRekap.Rows.Add(1)
			Dgv_DataRekap.Rows(DgvTab2_Rows).Cells(itemDgvRekap_lokasi).Value = arrSO(CmbSO_Asal.SelectedIndex)
			Dgv_DataRekap.Rows(DgvTab2_Rows).Cells(itemDgvRekap_KdBarang).Value = TxtKd_Barang.Text
			Dgv_DataRekap.Rows(DgvTab2_Rows).Cells(itemDgvRekap_NmBarang).Value = TxtNm_Barang.Text
			Dgv_DataRekap.Rows(DgvTab2_Rows).Cells(itemDgvRekap_Jumlah).Value = TxtTotalTransfer.Text & " " & TxtSatuan.Text
			Dgv_DataRekap.Rows(DgvTab2_Rows).Cells(itemDgvRekap_JumlahBags).Value = TxtTotalTransferBags.Text
			Dgv_DataRekap.Rows(DgvTab2_Rows).Cells(itemDgvRekap_JumlahBersih).Value = TxtTotalTransfer.Text
			Dgv_DataRekap.Rows(DgvTab2_Rows).Cells(itemDgvRekap_Satuan).Value = TxtSatuan.Text
			Dgv_DataRekap.Rows(DgvTab2_Rows).Cells(itemDgvRekap_SatuanKecil).Value = TxtSatuanKecil.Text
			Dgv_DataRekap.Rows(DgvTab2_Rows).Cells(itemDgvRekap_Oto).Value = Txt_OtoMaterial_req.Text
		Else

			MessageBox.Show("Tidak Ada Data Yang Di Pilih", "Transfer Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub

		End If

		KosongTab1()

	End Sub

	Private Sub Dgv_DataRekap_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgv_DataRekap.KeyDown
		If Dgv_DataRekap.Rows.Count = 0 Or Dgv_DataRekap.SelectedCells.Count = 0 Then
			Exit Sub
		End If

		Dim currentRow = Dgv_DataRekap.CurrentRow.Index
		Dim currentCell = Dgv_DataRekap.CurrentCellAddress.X

		If e.KeyCode = Keys.Delete Then

			Dim Hapus1 As String = MessageBox.Show("Apakah Anda Yakin, Ingin Menghapus Data ?", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			If Hapus1 = vbNo Then Exit Sub

			get_grid_view_Rekap(currentRow)

			For index = Dgv_DataDetail.Rows.Count - 1 To 0 Step -1
				get_grid_view_Detail(index)

				If dgv_Rekap_KdBarang = dgv_detail_KdBarang Then
					Dgv_DataDetail.Rows.RemoveAt(index)
				End If

			Next

			Dgv_DataRekap.Rows.RemoveAt(currentRow)

		End If
	End Sub

	Private Sub TxtKodeCost_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtKodeCost.KeyDown
		If e.KeyCode = Keys.Down Then LvCost.Focus()
	End Sub

	Private Sub TxtKodeCost_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKodeCost.KeyPress
		If e.KeyChar = Chr(13) Then
			If TxtKodeCost.Text.Trim.Length = 0 Then TxtKd_Barang.Focus()
			TxtKodeCost_Leave(TxtKodeCost, e)
			LvCost.Visible = False
		End If
	End Sub

	Private Sub TxtKodeCost_Leave(sender As Object, e As EventArgs) Handles TxtKodeCost.Leave
		If TxtKodeCost.Text.Trim.Length = 0 Then Exit Sub
		If LvCost.Focused = True Then Exit Sub

		Try
			OpenConn()

			SQL = "Select id_cost_center,kode_cost_center From EMI_Master_Cost_Center "
			SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' AND id_cost_center = '" & TxtIDCost.Text & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					TxtIDCost.Text = Dr("id_cost_center")
					TxtKodeCost.Text = Dr("kode_cost_center")
					TxtKd_Barang.Focus()
				Else
					MessageBox.Show("Kode cost center tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					TxtIDCost.Text = "" : TxtKodeCost.Text = ""
					TxtKodeCost.Focus()
				End If
				LvCost.Visible = False
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub LvCost_DoubleClick(sender As Object, e As EventArgs) Handles LvCost.DoubleClick
		Dim Kode As String = LvCost.FocusedItem.Text

		TxtIDCost.Text = Kode

		LvCost.Visible = False
		TxtKodeCost_Leave(LvCost, e)
		TxtKd_Barang.Focus()
	End Sub

	Private Sub LvCost_KeyDown(sender As Object, e As KeyEventArgs) Handles LvCost.KeyDown
		If e.KeyCode = Keys.Enter Then
			LvCost_DoubleClick(LvCost, e)
		End If
	End Sub

End Class