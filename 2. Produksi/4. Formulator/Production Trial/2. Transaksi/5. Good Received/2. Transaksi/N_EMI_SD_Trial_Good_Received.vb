Imports System.IO

Public Class N_EMI_SD_Trial_Good_Received

	Dim Arr_Detail_Biaya As New List(Of (akun As String, keterangan As String, nilai As Double, kd_so As String, kd_barang As String))
	Dim ArrDataJenis As New List(Of (keterangan As String, Kode_Warna As String, Keterangan_Quality As String))

	Dim fno_po As String = ""
	Dim Prefix As String = ""
	Dim id_routing As String = ""

	'Dim kategoriQuality As New ArrayList({"HIJAU", "MERAH"})
	'Dim kategoriQualityKet As New ArrayList({"Good Stock", "Bad Stock"})
	Dim arrInisialFaktur, arrSO, arrSoGudangSisa As New ArrayList

	Dim arrKdBarangSrap, arrNamaScrap, arrSatuanScrap, arrSatuanKecilScrap As New ArrayList
	Private random As New Random()
	Private imageBytes1 As Byte = Nothing
	Private FileSize1 As UInt32
	Private rawData1() As Byte
	Private fs1 As FileStream

	Dim Lv_NoSplit, Lv_NoPO, Lv_Lokasi, Lv_Tanggal, Lv_Jam, Lv_KdSo, Lv_KdBarang, Lv_NmBarang, Lv_Jmlh, Lv_Satuan, Lv_Catatan, Lv_Routing, Lv_TglSelesaiProduksi, Lv_TglExpired, Lv_PrefixCode As String
	Dim Lv_Life_Time As String
	Dim Tahun_MulaiProduksi As String

	Dim item_NoSplit As Integer = 0
	Dim item_NoPO As Integer = 1
	Dim item_Lokasi As Integer = 2
	Dim item_Tanggal As Integer = 3
	Dim item_Jam As Integer = 4
	Dim item_KdSo As Integer = 5
	Dim item_KdBarang As Integer = 6
	Dim item_NamaBarang As Integer = 7
	Dim item_Catatan As Integer = 8
	Dim item_Routing As Integer = 9
	Dim item_TglSelesaiProduksi As Integer = 10
	Dim item_TglExpired As Integer = 11
	Dim item_PrefixCode As Integer = 12
	Dim item_lf As Integer = 13
	Dim item_Jmlh As Integer = 14
	Dim item_Satuan As Integer = 15

	Private Sub DtpProduksi_ValueChanged(sender As Object, e As EventArgs) Handles DtpProduksi.ValueChanged

		Dim tanggal_produksi As Date = DtpProduksi.Value

		Dim tglExp As Date = tanggal_produksi.AddDays(Val(TxtLifeTime.Text))

		DtpExpired.Value = tglExp

	End Sub

	Private Sub Emi_Barcode_FinishGood_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		kosong()

	End Sub

	Private Sub GetNoFakturGI()
		Dim FPro_Results As String = "PRST"
		TxtFormulator_NoFaktur.Text = FPro_Results & Format(tgl_skg, "MMyy") & "-" &
							 General_Class.Get_Last_Number2("N_EMI_Transaksi_Trial_Production_Results", "No_Transaksi", 5,
							 "Kode_perusahaan", KodePerusahaan,
							 "And", "substring(No_Transaksi, 1, " & Len(FPro_Results) + 4 & ")", FPro_Results & Format(tgl_skg, "MMyy"))
	End Sub

	Private Sub kosong()

		'Txt_NoSplit.Text = String.Empty
		Txt_KdBarang.Text = String.Empty
		Txt_NamaBarang.Text = String.Empty
		Txt_HasilProduksi.Text = String.Empty
		TxtLifeTime.Text = ""
		TxtJmlScrap.Text = ""
		DtpProduksi.ResetText()
		DtpExpired.ResetText()

		TxtJmlScrap.BackColor = Color.FromArgb(235, 235, 235)

		Dgv_Batch.Columns(2).ReadOnly = True

		'GET TAHUN MULAI PRODUKSI

		get_jam()

		Try
			OpenConn()

			GetNoFakturGI()

			SQL = "select Tahun_Mulai_Produksi from Init"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					Tahun_MulaiProduksi = If(General_Class.CekNULL(dr("Tahun_Mulai_Produksi")) = "", "0", dr("Tahun_Mulai_Produksi"))
				Loop
			End Using

			Cmb_Lokasi.Items.Clear()
			SQL = "select Kode_Stock_Owner from Stock_Owner where Kode_Perusahaan = '" & KodePerusahaan & "' order by Kode_Stock_Owner"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					Cmb_Lokasi.Items.Add(dr("Kode_Stock_Owner"))
				Loop
				Cmb_Lokasi.SelectedIndex = 0
			End Using

			ArrDataJenis.Clear()
			SQL = "select Kode_warna, Keterangan from emi_master_warna where kode_warna<>'HIJAU' and "
			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' order by Kode_warna"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					ArrDataJenis.Add((dr("Keterangan"), dr("Kode_warna"), dr("Keterangan")))
				Loop
			End Using

			SQL = "select a.No_Transaksi, a.no_po, a.lokasi, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama, a.jumlah, a.satuan,a.Tgl_Produksi, a.jam_Produksi "
			SQL = SQL & "from N_EMI_Transaksi_Trial_Split_Production_Order a, barang b where "
			SQL = SQL & "a.kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Barang=b.Kode_Barang "
			SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner And a.kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.no_transaksi = '" & Txt_NoSplit.Text & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					DateTimePicker1.Value = Dr("Tgl_Produksi")
					TxtJam.Text = Dr("jam_Produksi")
					Txt_HasilProduksi.Text = Dr("jumlah")
					Txt_NamaBarang.Text = Dr("Nama")
					fno_po = Dr("no_po")
					Txt_KdBarang.Text = Dr("Kode_Barang")
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("data Tidak ditemukan . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			SQL = "Select b.prefix_code, a.Id_Routing from N_EMI_Transaksi_Trial_Order_Produksi a, EMI_Master_Routing b where "
			SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.Id_Routing = b.Id_Routing "
			SQL = SQL & "And a.status Is null and a.Kode_Perusahaan='" & KodePerusahaan & "' and a.no_faktur='" & fno_po & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Prefix = Dr("prefix_code")
					id_routing = Dr("Id_Routing")
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("data Tidak ditemukan . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'SQL = "select no_transaksi from Emi_Production_Results where "
			'SQL = SQL & "No_Production_Order = '" & Txt_NoSplit.Text & "' and Kode_Perusahaan='" & KodePerusahaan & "' and status is null "
			'Using Dr = OpenTrans(SQL)
			'    If Dr.Read Then
			'        TxtFormulator_NoFaktur.Text = Dr("no_transaksi")
			'    Else
			'        Dr.Close()
			'        CloseTrans()
			'        CloseConn()
			'        MessageBox.Show("data Tidak ditemukan . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        Exit Sub
			'    End If
			'End Using

			'SQL = "select distinct top(1) Nomor from Emi_Production_Results_detail_pallet where "
			'SQL = SQL & "no_transaksi = '" & TxtFormulator_NoFaktur.Text & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
			'SQL = SQL & "order by Nomor Desc "
			'Using Dr = OpenTrans(SQL)
			'    If Dr.Read Then
			'        If General_Class.CekNULL(Dr("Nomor")) = "" Then
			'            TxtJumlahKeranjang.Text = 0
			'        Else
			'            TxtJumlahKeranjang.Text = Dr("Nomor")
			'        End If
			'    Else
			'        TxtJumlahKeranjang.Text = 0
			'    End If
			'End Using

			Dim Lokasi_Produksi As String = ""
			SQL = "select Kode_Stock_Owner From Stock_Owner_Gudang "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Produksi = 'Y' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Lokasi_Produksi = Dr("kode_stock_owner")
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Lokasi Produksi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'Cmb_Satuan.Items.Clear()
			'Cmb_SatuanProduksi.Items.Clear()
			'CmbSatScrap.Items.Clear()
			'SQL = "select satuan from EMI_Satuan where "
			'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
			'Using Dr = OpenTrans(SQL)
			'    Do While Dr.Read
			'        Cmb_Satuan.Items.Add(Dr("satuan"))
			'        Cmb_SatuanProduksi.Items.Add(Dr("satuan"))
			'        CmbSatScrap.Items.Add(Dr("satuan"))
			'    Loop
			'End Using

			Dim kode_barang_scrap As String = ""
			Dim kode_barang As String = Txt_KdBarang.Text

			CmbSisaProduksi.Items.Clear()
			arrKdBarangSrap.Clear() : arrNamaScrap.Clear() : arrSatuanScrap.Clear() : arrSatuanKecilScrap.Clear()
			SQL = "select a.kode_barang_Scrap, b.nama, b.satuan as satuan_kecil, c.satuan from "
			SQL = SQL & "emi_binding_scrap a, barang b, barang_detail_satuan c "
			SQL = SQL & "where a.kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Barang_scrap=B.Kode_Barang "
			SQL = SQL & "and b.kode_Perusahaan=c.Kode_Perusahaan and b.Kode_Barang=c.Kode_Barang "
			SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and b.kode_stock_Owner='" & Lokasi_Produksi & "' "
			SQL = SQL & "and c.flag_tampil_display = 'Y' and a.Flag_GR1='Y' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					CmbSisaProduksi.Items.Add(Dr("nama")) : arrKdBarangSrap.Add(Dr("kode_barang_Scrap"))
					arrNamaScrap.Add(Dr("nama")) : arrSatuanScrap.Add(Dr("satuan"))
					arrSatuanKecilScrap.Add(Dr("satuan_kecil"))
				Loop
			End Using

			Dim satuanKodeBarang As String = ""
			Dim id_group_jenis As String = ""
			SQL = "select top(1) a.satuan, b.satuan as Satuan_Kecil, isnull(Life_Time,0) as Life_Time, id_group_jenis from barang_detail_satuan a, barang b where a.kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.kode_barang = '" & kode_barang & "' and a.kode_Barang=b.kode_barang "
			SQL = SQL & "and a.flag_tampil_display = 'Y' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Cmb_SatuanProduksi.Text = Dr("satuan")
					TxtSatuanKecil.Text = Dr("Satuan_Kecil")
					TxtLifeTime.Text = Dr("Life_Time")
					id_group_jenis = Dr("id_group_jenis")
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Barang detail satuan tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			Dim flag_FG As String = ""
			SQL = "select kode_group_jenis, isnull(flag_finished_good,'T') as flag_finished_good "
			SQL = SQL & "from emi_group_jenis where kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and id_group_jenis = '" & id_group_jenis & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					TxtGroupJenis.Text = Dr("kode_group_jenis")
					flag_FG = Dr("flag_finished_good")
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Barang detail satuan tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			Cmb_LokasiSimpan.Items.Clear() : arrSO.Clear()
			SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From Stock_Owner_Gudang where "
			SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' "

			If flag_FG = "Y" Then
				SQL = SQL & "And Flag_QI ='Y' "
			Else
				SQL = SQL & "And Flag_Penyimpanan ='Y' And Flag_GR1  ='Y' "
			End If

			SQL = SQL & "order by kode_stock_owner"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					Cmb_LokasiSimpan.Items.Add(dr("Keterangan")) : arrSO.Add(dr("kode_stock_owner"))
					arrInisialFaktur.Add(dr("inisial_faktur"))

				Loop
			End Using

			Cmb_Lokasi_Gudang_Sisa.Items.Clear() : arrSoGudangSisa.Clear()
			SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From Stock_Owner_Gudang where "
			SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' "
			SQL = SQL & "And Flag_GR1  ='Y' "
			SQL = SQL & "order by kode_stock_owner"
			Using dr = OpenTrans(SQL)
				Do While dr.Read

					Cmb_Lokasi_Gudang_Sisa.Items.Add(dr("Keterangan")) : arrSoGudangSisa.Add(dr("kode_stock_owner"))
				Loop
			End Using

			'=============================
			'=     GET BATCH SELESAI     =
			'=============================
			Dim Finished_Batch As New ArrayList
			SQL = "select distinct a.Proses "
			SQL &= $"from N_EMI_Transaksi_Trial_Production_Results_HPP a "
			SQL &= $"inner join N_EMI_Transaksi_Trial_Production_Results b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
			SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and b.Status is null "
			SQL &= $"and b.No_Production_Order = '{Txt_NoSplit.Text}' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Finished_Batch.Add(Dr("Proses"))
				Loop
			End Using

			'======================
			'=     LOAD BATCH     =
			'======================
			'Lv_Batch.Items.Clear()
			'SQL = "select Jumlah_Batch from N_EMI_Transaksi_Trial_Split_Production_Order "
			'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
			'SQL = SQL & "and No_Transaksi = '" & Txt_NoSplit.Text & "' "
			'SQL = SQL & "and status is null "
			'Using Dr = OpenTrans(SQL)
			'    If Dr.Read Then
			'        Dim JumlahBatch As Double = Dr("Jumlah_Batch")
			'        For i As Integer = 1 To JumlahBatch
			'            Dim Lv As ListViewItem
			'            Lv = Lv_Batch.Items.Add(i)
			'        Next
			'    End If
			'End Using

			Dgv_Batch.Rows.Clear()
			SQL = "select Jumlah_Batch from N_EMI_Transaksi_Trial_Split_Production_Order "
			SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and No_Transaksi = '" & Txt_NoSplit.Text & "' "
			SQL = SQL & "and status is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then

						Dim JumlahBatch As Double = .Rows(0).Item("Jumlah_Batch")
						Dim currentRow As Integer = 0
						For i As Integer = 0 To JumlahBatch - 1
							If Finished_Batch.Contains(i + 1) Then Continue For

							Dim n As Integer = Dgv_Batch.Rows.Add()

							Dgv_Batch.Rows(n).Cells(0).Value = i + 1
							Dgv_Batch.Rows(n).Cells(1).Value = False
							Dgv_Batch.Rows(n).Cells(2).Value = ""

							'=============================
							'=      ISI DATA COMBOBOX      =
							'=============================
							Dim cmbCell As DataGridViewComboBoxCell = DirectCast(Dgv_Batch.Rows(n).Cells(3), DataGridViewComboBoxCell)
							cmbCell.Items.Clear()
							cmbCell.Items.AddRange(ArrDataJenis.Select(Function(x) x.keterangan).ToArray())

							Dgv_Batch.Rows(n).Cells(4).Value = ""
						Next
					End If
				End With
			End Using

			If flag_FG = "Y" Then
				If Cmb_LokasiSimpan.Items.Count = 0 Then
					CloseConn()
					MessageBox.Show("Lokasi Belum di set . !!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

				Cmb_LokasiSimpan.SelectedIndex = 0
				Cmb_LokasiSimpan.Enabled = False
			Else
				Cmb_LokasiSimpan.SelectedIndex = -1
				Cmb_LokasiSimpan.Enabled = True
			End If

			Dim tanggal_produksi As Date = DtpProduksi.Value

			Dim tglExp As Date = tanggal_produksi.AddDays(Val(TxtLifeTime.Text))

			DtpExpired.Value = tglExp

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		get_packaging()
	End Sub

	Private Sub get_packaging()

		Try
			OpenConn()

			Dim TotJumlah As Double = 0

			For i As Integer = 0 To Dgv_Batch.Rows.Count - 1
				If Dgv_Batch.Rows(i).Cells(1).Value = True Then
					TotJumlah += Val(HilangkanTanda(Dgv_Batch.Rows(i).Cells(2).Value))
				End If
			Next

			Dgv_Packaging.Rows.Clear()
			'GET DETAIL DATA PACKAGING BY NO SPLIT

			Dim kd_inq As String = ""
			SQL = "select top(1) kode_barang_inq from barang a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.kode_barang = '" & Txt_KdBarang.Text & "'  "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					kd_inq = Dr("kode_barang_inq")
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Data Tidak di temukan . !!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			SQL = "select a.No_Transaksi, b.jumlah_barang, b.jumlah_bahan, "
			SQL = SQL & "isnull(( select z.kode_barang_inq from barang z where a.kode_perusahaan = z.kode_perusahaan "
			SQL = SQL & "and a.kode_stock_owner = z.kode_stock_owner and a.kode_barang = z.kode_barang "
			SQL = SQL & "), '-') as Kode_Barang, "
			SQL = SQL & "b.Kode_Stock_Owner, b.Kode_Barang as Kode_Bahan, c.Nama ,b.Jumlah, b.Satuan, c.flag_potong_stok,isnull(c.standar_price,0) as standar_price, isnull(c.Flag_Pembulatan_Produksi,'T') as Flag_Pembulatan_Produksi "
			SQL = SQL & "from N_EMI_Transaksi_Trial_Split_Production_Order a, N_EMI_Transaksi_Trial_Split_Production_Order_Detail_Packaging b, barang c "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
			SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
			SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & Txt_NoSplit.Text & "' "
			SQL = SQL & "and  b.jenis = 'KEMASAN UTAMA'"
			SQL = SQL & "order by c.nama "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							'================================================================
							'=     GET JUMLAH KEBUTUHAN PACKAGING TERHADAP KD_BARANG PO     =
							'================================================================
							Dim jumlahKebutuhanPackaging As Double = 0
							Dim jumlahBarangPerBahan As Double = 0

							jumlahKebutuhanPackaging = Val(HilangkanTanda(Format(.Rows(i).Item("jumlah_bahan"), "N4")))
							jumlahBarangPerBahan = .Rows(i).Item("jumlah_barang")

							'SQL = "select jumlah_barang, jumlah_bahan  "
							'SQL = SQL & "from Barang_Detail_Bahan_Penolong  "
							'SQL = SQL & "where kode_barang='" & .Rows(i).Item("Kode_Barang") & "' "
							'SQL = SQL & "and kode_Bahan = '" & .Rows(i).Item("Kode_Bahan") & "' "
							'SQL = SQL & "and Kode_Perusahaan = '" & KodePerusahaan & "'"
							'Using Dr = OpenTrans(SQL)
							'    If Dr.Read Then
							'        jumlahKebutuhanPackaging = Val(HilangkanTanda(Format(Dr("jumlah_bahan"), "N4")))
							'        jumlahBarangPerBahan = Dr("jumlah_barang")
							'    End If
							'End Using

							Dim NilaiProduksiPck As Double = Val(HilangkanTanda(Format((Val(HilangkanTanda(TotJumlah)) / jumlahBarangPerBahan) * jumlahKebutuhanPackaging, "N4")))

							If .Rows(i).Item("Flag_Pembulatan_Produksi") = "Y" Then
								NilaiProduksiPck = Math.Ceiling(NilaiProduksiPck)
							End If

							Dgv_Packaging.Rows.Add(1)
							Dgv_Packaging.Rows.Item(i).Cells(0).Value = .Rows(i).Item("Kode_Bahan")
							Dgv_Packaging.Rows.Item(i).Cells(1).Value = .Rows(i).Item("Nama")
							Dgv_Packaging.Rows.Item(i).Cells(2).Value = NilaiProduksiPck

						Next
					Else
						'CloseTrans()
						'CloseConn()
						'MessageBox.Show("Data Packaging Terhadap No Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'Exit Sub
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

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		kosong()
	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If Txt_NoSplit.Text.Trim.Length = 0 Then Exit Sub

		If TxtJmlScrap.Text.Trim.Length <> 0 And Val(TxtJmlScrap.Text) <> 0 Then
			If CmbSisaProduksi.SelectedIndex = -1 Then
				MessageBox.Show("Sisa Produksi harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				CmbSisaProduksi.Focus()
				Exit Sub
			End If
		End If

		Dim hasData As Boolean = False
		For i As Integer = 0 To Dgv_Batch.Rows.Count - 1
			If Dgv_Batch.Rows(i).Cells(1).Value = True Then
				hasData = True
				Exit For
			End If
		Next

		If Not hasData Then
			MessageBox.Show("Tidak Ada Batch yang akan Disimpan Harap Pilih Batch Dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		'If CmbJenis.SelectedIndex = -1 Then
		'    MessageBox.Show("Jenis Quality Harus diPilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'    CmbJenis.Focus()
		'    Exit Sub
		'End If

		Simpan_Data(True)

	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		If Txt_NoSplit.Text.Trim.Length = 0 Then Exit Sub

		If TxtJmlScrap.Text.Trim.Length <> 0 And Val(TxtJmlScrap.Text) <> 0 Then
			If CmbSisaProduksi.SelectedIndex = -1 Then
				MessageBox.Show("Sisa Produksi harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				CmbSisaProduksi.Focus()
				Exit Sub
			End If
		End If

		Dim hasData As Boolean = False
		For i As Integer = 0 To Dgv_Batch.Rows.Count - 1
			If Dgv_Batch.Rows(i).Cells(1).Value = True Then
				hasData = True
				Exit For
			End If
		Next

		If Not hasData Then
			MessageBox.Show("Tidak Ada Batch yang akan Disimpan Harap Pilih Batch Dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If CmbSisaProduksi.SelectedIndex <> -1 Then
			If Cmb_Lokasi_Gudang_Sisa.SelectedIndex = -1 Then
				MessageBox.Show("Lokasi Gudang Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Cmb_Lokasi_Gudang_Sisa.DroppedDown = True : Cmb_Lokasi_Gudang_Sisa.Focus()
				Exit Sub
			End If
		End If

		get_jam()
		Try
			OpenConn()

			GetNoFakturGI()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Simpan_Data(False)

	End Sub

	Private Function Ubah_Angka_Kecil(ByVal kodeBarang As String, ByVal satuanBesar As String, ByVal satuanKecil As String, ByVal jumlahConvert As String) As Double

		Dim total_kecil As Double = jumlahConvert
		SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & kodeBarang & "', '" & satuanBesar & "',"
		SQL = SQL & "'" & satuanKecil & "', '" & HilangkanTanda(jumlahConvert) & "' ) as hasil"
		Using Dr1 = OpenTrans(SQL)
			If Dr1.Read Then
				If General_Class.CekNULL(Dr1("hasil")) = "" Then
					Dr1.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("data konversi satuan kirim tidak ada ")
				End If

				total_kecil = Dr1("hasil")
			Else
				Dr1.Close()
				CloseTrans()
				CloseConn()
				MessageBox.Show("data konversi satuan kirim tidak ada ")
			End If
		End Using

		Return total_kecil
	End Function

	Private Sub CmbSisaProduksi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSisaProduksi.SelectedIndexChanged
		If CmbSisaProduksi.SelectedIndex = -1 Then
			Exit Sub
		End If
		If CmbSisaProduksi.SelectedIndex = -1 Then
			Cmb_Lokasi_Gudang_Sisa.SelectedIndex = -1
			Cmb_Lokasi_Gudang_Sisa.Enabled = False
			TxtJmlScrap.Enabled = False
			TxtJmlScrap.BackColor = Color.FromArgb(235, 235, 235)
		Else
			Cmb_Lokasi_Gudang_Sisa.SelectedIndex = -1
			Cmb_Lokasi_Gudang_Sisa.Enabled = True
			TxtJmlScrap.Enabled = True
			TxtJmlScrap.BackColor = Color.White
		End If

		TxtJmlScrap.Text = ""

		CmbSatScrap.Text = arrSatuanScrap.Item(CmbSisaProduksi.SelectedIndex)
		TxtSatScrapKecil.Text = arrSatuanKecilScrap.Item(CmbSisaProduksi.SelectedIndex)
	End Sub

	Private Sub Txt_Jumlah_KeyPress(sender As Object, e As KeyPressEventArgs)
		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
	End Sub

	'Private Sub CmbJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbJenis.SelectedIndexChanged
	'    If CmbJenis.SelectedIndex = -1 Then Exit Sub
	'    If CmbJenis.SelectedItem.ToString.ToUpper = "QUALITY INSPECTION" Then
	'        Txt_Troli.Text = ""
	'        Cmb_Tahapan.Text = "" : Cmb_Tahapan.SelectedIndex = -1
	'        Cmb_Tahapan.Enabled = True
	'        'Txt_Troli.Enabled = True

	'        Txt_Troli.BackColor = Color.White
	'    Else
	'        Txt_Troli.Text = ""
	'        Cmb_Tahapan.Text = "" : Cmb_Tahapan.SelectedIndex = -1
	'        Cmb_Tahapan.Enabled = False
	'        Txt_Troli.Enabled = False
	'        Txt_Troli.BackColor = Color.FromArgb(235, 235, 235)
	'    End If
	'End Sub

	Private Function Simpan_Data_GI(ByVal CurrentBatch As Integer, ByVal CurrentCheckBox As Boolean, ByVal CurrentJmlhInput As String, ByVal CurrentJenisQuality As String, ByVal CurrentTrolly As String) As Boolean

		Dim NoFakturGI As String = TxtFormulator_NoFaktur.Text.Trim

		'Dim ToleransiFormulaMin As Double = 0
		'Dim ToleransiFormulaMax As Double = 0
		''=====================================
		''=    GET NILAI TOLERANSI FORMULA    =
		''=====================================
		'SQL = "select Toleransi_Formula_GI_Min, Toleransi_Formula_GI_Max from init "
		'SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
		'Using Dr = OpenTrans(SQL)
		'    If Dr.Read Then

		'        If General_Class.CekNULL(Dr("Toleransi_Formula_GI_Min")) = "" Then
		'            Dr.Close()
		'
		'
		'            MessageBox.Show("Terjadi Kesalahan Pada Tabel Init, Nilai Toleransi Formula GI Min Belum Diset", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'            return false
		'        ElseIf General_Class.CekNULL(Dr("Toleransi_Formula_GI_Max")) = "" Then
		'            Dr.Close()
		'
		'
		'            MessageBox.Show("Terjadi Kesalahan Pada Tabel Init, Nilai Toleransi Formula GI Max Belum Diset", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'            return false
		'        End If

		'        ToleransiFormulaMin = Math.Abs(Val(HilangkanTanda(Dr("Toleransi_Formula_GI_Min"))))
		'        ToleransiFormulaMax = Math.Abs(Val(HilangkanTanda(Dr("Toleransi_Formula_GI_Max"))))

		'    Else
		'        Dr.Close()
		'
		'
		'        MessageBox.Show("Terjadi Kesalahan Pada Tabel Init", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'        return false
		'    End If
		'End Using

		Dim Kd_So As String = ""
		Dim Kd_Brg As String = ""
		SQL = "Select b.Status, b.Selesai, b.Kode_Stock_Owner, b.Kode_Barang "
		SQL = SQL & "from N_EMI_Transaksi_Trial_Split_Production_Order a,N_EMI_Transaksi_Trial_Order_Produksi b "
		SQL = SQL & "where a.No_PO = b.No_Faktur "
		SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "' "
		SQL = SQL & "and a.No_Transaksi = '" & Txt_NoSplit.Text & "'"
		Using dr = OpenTrans(SQL)
			If dr.Read Then
				Kd_So = dr("Kode_Stock_Owner")
				Kd_Brg = dr("Kode_Barang")
				If General_Class.CekNULL(dr("Status")) <> "" Then
					dr.Close()
					MessageBox.Show(Base_Language.Lang_Global_NoFaktur & " " & Base_Language.Lang_Global_DataSudahBatal, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Return False
				End If
			End If
		End Using

		Dim IsBatchComplete As Boolean = False
		Dim IsAutomaticValidation As Boolean = True

		Dim Nilai_Bahan As Double = 0
		Dim Nilai_Packaging As Double = 0
		Dim Nilai_loss_production As Double = 0
		Dim arr_biaya_Produksi, arrID_Work_Center, arrJenis_Biaya As New ArrayList
		Dim Hpp_Work_Center_total As Double = 0

		Arr_Detail_Biaya.Clear()
		SQL = "select a.Qty_Batch, a.No_Transaksi, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama, b.Jumlah ,b.Satuan, c.flag_potong_stok, isnull(c.standar_price,0) as standar_price, Flag_Non_Barcode, "
		SQL &= $"isnull(f.Selesai, 'T') as Status, "
		SQL &= $"isnull(f.Nilai_Produksi, 0) as JumlahInput, "
		SQL &= $"isnull(( select ((x.Jumlah) / (select dbo.ubah_satuan(z.Kode_Perusahaan, 'masa', z.Kode_Barang, r.Satuan_Hasil, x.satuan, r.Hasil) "
		SQL &= $"from Emi_Transaksi_Formulator r "
		SQL &= $"where r.Kode_Perusahaan = x.Kode_Perusahaan And r.No_Faktur = x.No_Faktur)) * a.Qty_Batch "
		SQL &= $"from N_EMI_Transaksi_Trial_Order_Produksi z, EMI_Transaksi_Formulator_Detail_Bahan x "
		SQL &= $"where z.Kode_Perusahaan = x.Kode_Perusahaan "
		SQL &= $"and z.Kode_Formula = x.No_Faktur "
		SQL &= $"and z.Status is null "
		SQL &= $"and a.Kode_Perusahaan = z.Kode_Perusahaan "
		SQL &= $"and a.No_PO = z.No_Faktur "
		SQL &= $"and x.Kode_Barang = b.Kode_Barang "
		SQL &= $"), 0) as JumlahKebutuhan "
		SQL &= $"from N_EMI_Transaksi_Trial_Split_Production_Order a "
		SQL &= $"inner join N_EMI_Transaksi_Trial_Split_Production_Order_Detail_Bahan b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
		SQL &= $"inner join barang c on  b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
		SQL &= $"left join N_EMI_Transaksi_Trial_Production_Results d on a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Transaksi = d.No_Production_Order and d.Status is null "
		SQL &= $"left join N_EMI_Transaksi_Trial_Production_Results_HPP e on d.Kode_Perusahaan = e.Kode_Perusahaan and d.No_Transaksi = e.No_Transaksi and e.Tanggal is not null "
		SQL &= $"and e.Proses = {CurrentBatch} "
		SQL &= $"left join N_EMI_Transaksi_Trial_Production_Results_Detail f on d.kode_perusahaan = f.kode_perusahaan and d.No_Transaksi = f.No_Transaksi and f.Kode_Barang = b.Kode_Barang "
		SQL &= $"and f.Proses = {CurrentBatch} "
		SQL &= $"where a.Status is null "
		SQL &= $"and a.kode_perusahaan = '{KodePerusahaan}' "
		SQL &= $"and a.no_transaksi = '{Txt_NoSplit.Text.Trim}' "
		SQL &= $"order by c.nama "
		Using Ds = BindingTrans(SQL)
			With Ds.Tables("Mytable")
				If .Rows.Count <> 0 Then
					For i As Integer = 0 To .Rows.Count - 1

						Dim convertKeSatuanAsli_bhn As String = ""
						Dim jumlahConvertBhn As Double = 0

						Dim KdBahan As String = .Rows(i).Item("Kode_Barang")
						Dim KdSO As String = .Rows(i).Item("Kode_Stock_Owner")
						Dim JumlahKebutuhan As Double = Val(HilangkanTanda(Format(.Rows(i).Item("JumlahKebutuhan"), "N4")))
						Dim Satuan As String = .Rows(i).Item("Satuan")
						Dim NilaiProduksi As Double = Val(HilangkanTanda(.Rows(i).Item("Satuan")))

						SQL = "select satuan From barang where Kode_barang = '" & KdBahan & "' "
						SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & KdSO & "' "
						Using Dr3 = OpenTrans(SQL)
							If Dr3.Read Then

								convertKeSatuanAsli_bhn = Dr3("satuan")
								SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & KdBahan & "',"
								SQL = SQL & "'" & Satuan & "','" & Dr3("satuan") & "',"
								SQL = SQL & HilangkanTanda(JumlahKebutuhan) & ") as Hasil "
								Dr3.Close()

								Using dr4 = OpenTrans(SQL)
									If dr4.Read Then
										If General_Class.CekNULL(dr4("Hasil")) <> "" Then
											'If dr4("Hasil") = 0 Then
											'    dr4.Close()
											'
											'
											'    MessageBox.Show("Satuan " & LvSatuan & " Ke " & convertKeSatuanAsli_bhn & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											'    return false
											'Else
											jumlahConvertBhn = Val(HilangkanTanda(Format(dr4("hasil"), "N4")))

											'End If
										Else
											dr4.Close()
											MessageBox.Show("Satuan " & Satuan & " Ke " & convertKeSatuanAsli_bhn & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Return False
										End If
									End If
								End Using
							Else
								Dr3.Close()

								MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Return False
							End If
						End Using

						'Dim Nilai_Formula As Double = 0
						'SQL = "Select c.Kode_Barang, "
						'SQL = SQL & "isnull(( "
						'SQL = SQL & "round( "
						'SQL = SQL & "(c.Jumlah / (select z.Hasil from Emi_Transaksi_Formulator z "
						'SQL = SQL & "where z.Kode_Perusahaan = c.Kode_Perusahaan And z.No_Faktur = c.No_Faktur) "
						'SQL = SQL & ") "
						'SQL = SQL & "* "
						'SQL = SQL & "(ISNULL((a.Qty_Batch), 0)),4)), 0) as Nilai_Formula "
						'SQL = SQL & "From N_EMI_Transaksi_Trial_Split_Production_Order a, N_EMI_Transaksi_Trial_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c "
						'SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And b.Kode_Perusahaan = c.Kode_Perusahaan "
						'SQL = SQL & "And a.No_PO = b.No_Faktur "
						'SQL = SQL & "And b.Kode_Formula = c.No_Faktur "
						'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
						'SQL = SQL & "And a.No_Transaksi = '" & Txt_NoSplit.Text.Trim & "' "
						'SQL = SQL & "And c.Kode_Barang = '" & KdBahan & "' "
						'Using dr = OpenTrans(SQL)
						'    If dr.Read Then
						'        Nilai_Formula = Val(HilangkanTanda(Format(dr("Nilai_Formula"), "N4")))
						'    Else
						'        dr.Close()
						'
						'
						'        MessageBox.Show("Nilai Formula Tidak di temukan . . ! !")
						'        return false
						'    End If
						'End Using

						'=====================
						'=     CEK STOCK     =
						'=====================
						SQL = "select a.Kode_Stock_Owner, a.Kode_Barang, a.Good_Stock, isnull(sum(b.Jumlah), 0) as Jumlah_SN "
						SQL &= $"from Barang a "
						SQL &= $"left join Barang_SN b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang and b.Blok_SN is NULL "
						SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
						SQL &= $"and a.Kode_Stock_Owner = '{KdSO}' "
						SQL &= $"and a.Kode_Barang = '{KdBahan}' "
						SQL &= $"group by a.Kode_Stock_Owner, a.Kode_Barang, a.Good_Stock "
						Using Dr = OpenTrans(SQL)
							If Dr.Read Then

								If Val(HilangkanTanda(Dr("Good_Stock"))) <> Val(HilangkanTanda(Dr("Jumlah_SN"))) Then
									Dr.Close()
									MessageBox.Show($"Jumlah Barang dan Barang SN Kode Barang {KdBahan} Tidak Sesuai", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Return False
								End If

								If JumlahKebutuhan > Val(HilangkanTanda(Dr("Jumlah_SN"))) Then
									Dr.Close()

									MessageBox.Show($"Jumlah Stock Kode Barang {KdBahan} Tidak Mencukupi Untuk Memenuhi Kebutuhan Batch {CurrentBatch}", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Return False
								End If
							Else
								Dr.Close()
								MessageBox.Show($"Kode Barang {KdBahan} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Return False
							End If
						End Using

						SQL = "INSERT INTO N_EMI_Transaksi_Trial_Production_Results_Detail(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,Nilai_Formula,Nilai_Produksi,Satuan,proses,"
						SQL = SQL & "nilai_barang,satuan_barang,userid,tanggal,jam, Selesai ) "
						SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "','" & KdSO & "','" & KdBahan & "',"
						SQL = SQL & "'" & JumlahKebutuhan & "','" & HilangkanTanda(JumlahKebutuhan) & "','" & Satuan & "' , '" & CurrentBatch & "', "
						SQL = SQL & "'" & jumlahConvertBhn & "', '" & convertKeSatuanAsli_bhn & "', '" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
						SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', 'Y' "
						SQL = SQL & ")"
						ExecuteTrans(SQL)

						Dim x_ident_currentBahan As Integer = 0
						SQL = "select IDENT_CURRENT('N_EMI_Transaksi_Trial_Production_Results_Detail') as urutan"
						Using Dr = OpenTrans(SQL)
							If Dr.Read Then
								x_ident_currentBahan = Dr("urutan")
							End If
						End Using

						SQL = "select round(good_stock,4) as good_stock, flag_ppn from barang where "
						SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
						SQL = SQL & "kode_stock_owner = '" & KdSO & "' and "
						SQL = SQL & "kode_barang = '" & KdBahan & "'"
						Using Ds1 = BindingTrans(SQL)
							If Ds1.Tables("MyTable").Rows.Count <> 0 Then
								'If LvPotStokBhn = "Y" Then
								If Val(HilangkanTanda(Format(Ds1.Tables("MyTable").Rows(0).Item("good_stock"), "N4"))) - Val(jumlahConvertBhn) < BolehNegatif Then
									MessageBox.Show("Proses membuat stock menjadi negatif untuk kode barang " & KdBahan & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Return False
								Else
									SQL = "Update barang set good_stock = good_stock - " & jumlahConvertBhn & " where "
									SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
									SQL = SQL & "kode_stock_owner = '" & KdSO & "' and "
									SQL = SQL & "kode_barang = '" & KdBahan & "'"
									ExecuteTrans(SQL)
								End If
								'End If
							Else
								MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
								Return False
							End If
						End Using

						Dim lewatin As String = "T"
						SQL = "select isnull(round(sum(jumlah),4), 0) as stock from barang_sn where "
						SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
						SQL = SQL & "kode_stock_owner = '" & KdSO & "' and "
						SQL = SQL & "kode_barang = '" & KdBahan & "' and jumlah <> 0 "
						Using Dr = OpenTrans(SQL)
							If Dr.Read Then
								If Val(HilangkanTanda(Format(Dr("stock"), "N4"))) < Val(jumlahConvertBhn) Then
									'lewatin = "Y"
									Dr.Close()

									MessageBox.Show($"Stock Barang SN {KdBahan} Kurang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Return False
								Else
									lewatin = "T"
								End If
							Else
								Dr.Close()

								MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & KdBahan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Return False
							End If
						End Using

						'==================================================================================
						'======================  CHECK APAKAH FLAG POTONG STOK NYA Y atau T ================
						'==================================================================================
						'If LvPotStokBhn = "Y" Then
						If lewatin = "T" Then
							Dim sisa As Double = 0

							SQL = "select kode_stock_owner, kode_barang, serial_number, dbo.get_hpp(Serial_Number) as HPP, round(jumlah,4) as jumlah from barang_sn where "
							SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
							SQL = SQL & "kode_stock_owner = '" & KdSO & "' and "
							SQL = SQL & "kode_barang = '" & KdBahan & "' and jumlah <> 0 "
							SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
							Using Ds1 = BindingTrans(SQL)
								If Ds1.Tables("MyTable").Rows.Count <> 0 Then
									sisa = Val(jumlahConvertBhn)
									For h As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1
										If sisa = 0 Then
											Exit For
										ElseIf sisa < 0 Then

											MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Return False
										End If

										Dim hpp As Double = Ds1.Tables("MyTable").Rows(h).Item("HPP")
										Dim JumlahInsert As Double = 0

										'===========================
										'=     GET DETAIL AKUN     =
										'===========================
										Dim Kd_Akun_Biaya As String = ""
										Dim Ket_Group_Jenis As String = ""
										SQL = "select a.id_group_jenis, c.Akun_Persediaan, a.Kode_Group_Jenis "
										SQL = SQL & "from emi_group_jenis a, barang b, emi_group_jenis_akun c "
										SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
										SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
										SQL = SQL & "and a.id_group_jenis = b.id_group_jenis and b.kode_barang='" & Ds1.Tables("MyTable").Rows(h).Item("kode_barang") & "' "
										SQL = SQL & "and a.id_group_jenis = c.id_group_jenis and b.kode_stock_owner='" & Ds1.Tables("MyTable").Rows(h).Item("kode_stock_owner") & "' "
										Using Dr = OpenTrans(SQL)
											If Dr.Read Then
												Kd_Akun_Biaya = Dr("Akun_Persediaan")
												Ket_Group_Jenis = Dr("Kode_Group_Jenis")
											Else
												Dr.Close()

												MessageBox.Show("Barang detail jenis tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
												Return False
											End If
										End Using

										If sisa < Ds1.Tables("MyTable").Rows(h).Item("jumlah") Or sisa = Ds1.Tables("MyTable").Rows(h).Item("jumlah") Then
											SQL = "Update barang_sn set jumlah = jumlah - " & sisa & " where "
											SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
											SQL = SQL & "kode_stock_owner = '" & Ds1.Tables("MyTable").Rows(h).Item("kode_stock_owner") & "' and "
											SQL = SQL & "kode_barang = '" & Ds1.Tables("MyTable").Rows(h).Item("kode_barang") & "' and "
											SQL = SQL & "serial_number = '" & Ds1.Tables("MyTable").Rows(h).Item("serial_number") & "'"
											ExecuteTrans(SQL)

											SQL = "INSERT INTO N_EMI_Transaksi_Trial_Production_Results_det(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,"
											SQL = SQL & "Nilai,Serial_Number,no_urut_detail) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
											SQL = SQL & "'" & Ds1.Tables("MyTable").Rows(h).Item("kode_stock_owner") & "','" & Ds1.Tables("MyTable").Rows(h).Item("kode_barang") & "',"
											SQL = SQL & "" & sisa & ",'" & Ds1.Tables("MyTable").Rows(h).Item("serial_number") & "', '" & x_ident_currentBahan & "')"
											ExecuteTrans(SQL)

											JumlahInsert = sisa

											Nilai_Bahan = Nilai_Bahan + (Math.Round(hpp * sisa, 0))
											sisa = 0
										ElseIf sisa > .Rows(h).Item("jumlah") Then
											SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
											SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
											SQL = SQL & "kode_stock_owner = '" & Ds1.Tables("MyTable").Rows(h).Item("kode_stock_owner") & "' and "
											SQL = SQL & "kode_barang = '" & Ds1.Tables("MyTable").Rows(h).Item("kode_barang") & "' and "
											SQL = SQL & "serial_number = '" & Ds1.Tables("MyTable").Rows(h).Item("serial_number") & "'"
											ExecuteTrans(SQL)

											SQL = "INSERT INTO N_EMI_Transaksi_Trial_Production_Results_det(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,"
											SQL = SQL & "Nilai,Serial_Number,no_urut_detail) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
											SQL = SQL & "'" & Ds1.Tables("MyTable").Rows(h).Item("kode_stock_owner") & "','" & Ds1.Tables("MyTable").Rows(h).Item("kode_barang") & "',"
											SQL = SQL & "" & .Rows(h).Item("jumlah") & ",'" & Ds1.Tables("MyTable").Rows(h).Item("serial_number") & "', '" & x_ident_currentBahan & "')"
											ExecuteTrans(SQL)

											JumlahInsert = Ds1.Tables("MyTable").Rows(h).Item("jumlah")

											Nilai_Bahan = Nilai_Bahan + (Math.Round(hpp * Ds1.Tables("MyTable").Rows(h).Item("jumlah"), 0))
											sisa = sisa - Ds1.Tables("MyTable").Rows(h).Item("jumlah")
										Else

											MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & KdBahan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Return False
										End If

										'TODO : CEK GROUP BY KODE BARANG, TAMBAH ARRAY DETAIL

										Dim kdBarang As String = Ds1.Tables("MyTable").Rows(h).Item("kode_barang")

										Dim existingItemIndex As Integer = Arr_Detail_Biaya.FindIndex(Function(x) x.akun = Kd_Akun_Biaya)

										If existingItemIndex >= 0 Then
											Dim currentItem = Arr_Detail_Biaya(existingItemIndex)

											Arr_Detail_Biaya(existingItemIndex) = (
													akun:=currentItem.akun,
													keterangan:=currentItem.keterangan,
													nilai:=currentItem.nilai + Math.Round((hpp * JumlahInsert), 0),
													kd_so:=currentItem.kd_so,
													kd_barang:=currentItem.kd_barang
												)
										Else
											Arr_Detail_Biaya.Add((
													akun:=Kd_Akun_Biaya,
													keterangan:=Ket_Group_Jenis,
													nilai:=Math.Round((hpp * JumlahInsert), 0),
													kd_so:=KdSO,
													kd_barang:=kdBarang
												))
										End If

										If Math.Round(sisa, 4) <> 0 And h = Ds1.Tables("MyTable").Rows.Count - 1 Then

											MessageBox.Show("Jumlah stock tidak mencukupi untuk kode barang " & KdBahan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Return False
										End If

									Next ' for barang sn
								End If 'count <> 0

							End Using
						Else

							MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & KdBahan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Return False
						End If

						'==================================================
						'=     Cek Apakah ada Proses yg sudah selesai     =
						'==================================================
						SQL = "Select Proses from "
						SQL = SQL & "N_EMI_Transaksi_Trial_Production_Results_HPP a where "
						SQL = SQL & "a.Kode_Perusahaan ='" & KodePerusahaan & "' and "
						SQL = SQL & "a.No_Transaksi='" & TxtFormulator_NoFaktur.Text & "' and "
						SQL = SQL & "a.tanggal is null "
						Using Ds1 = BindingTrans(SQL)
							If Ds1.Tables("MyTable").Rows.Count <> 0 Then
								For index = 0 To Ds1.Tables("MyTable").Rows.Count - 1

									Dim proses_temp As Integer = Ds1.Tables("MyTable").Rows(index).Item("Proses")
									Dim selesai As Boolean = True

									'cek apakah bahan per formula sudah selesai
									SQL = "Select b.Kode_Barang, "
									SQL = SQL & "isnull((select 'Y' from N_EMI_Transaksi_Trial_Production_Results x, N_EMI_Transaksi_Trial_Production_Results_Detail y "
									SQL = SQL & "where x.kode_Perusahaan = y.kode_perusahaan And x.No_Transaksi = y.No_Transaksi And x.status Is null "
									SQL = SQL & "And x.Kode_Perusahaan = a.Kode_Perusahaan And x.No_Production_Order = a.No_Transaksi And "
									SQL = SQL & "y.Kode_Barang = b.Kode_Barang And y.Proses = '" & proses_temp & "' and y.status is null and y.selesai='Y'),'T') as Terpenuhi "
									SQL = SQL & "From N_EMI_Transaksi_Trial_Split_Production_Order a, N_EMI_Transaksi_Trial_Split_Production_Order_Detail_Bahan b Where "
									SQL = SQL & "a.kode_Perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Faktur And a.status Is null "
									SQL = SQL & " And a.no_transaksi ='" & Txt_NoSplit.Text & "' and a.kode_Perusahaan ='" & KodePerusahaan & "' "
									Using dr = OpenTrans(SQL)
										Do While dr.Read
											If dr("Terpenuhi") = "T" Then
												selesai = False
											End If
										Loop
									End Using

									'kalo sudah insert, insert packaging dan tentukan hpp
									If selesai = True Then
										Dim Jumlah_Dosing As Double = 0
										Dim Jumlah_Dosing_Pcs As Double = 0
										Dim satuan_dosing As String = ""

										SQL = "Select sum(nilai_Produksi) As Total, b.satuan from "
										SQL = SQL & "N_EMI_Transaksi_Trial_Production_Results a, N_EMI_Transaksi_Trial_Production_Results_Detail b where "
										SQL = SQL & "a.kode_perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi And a.status Is null "
										SQL = SQL & "And a.Kode_Perusahaan='" & KodePerusahaan & "' "
										SQL = SQL & "and a.No_Transaksi='" & TxtFormulator_NoFaktur.Text & "' "
										SQL = SQL & "and b.proses='" & proses_temp & "' "
										SQL = SQL & "and b.status is null "
										SQL = SQL & "group by b.satuan "
										Using dr = OpenTrans(SQL)
											If dr.Read Then
												Jumlah_Dosing = Val(HilangkanTanda(Format(dr("Total"), "N4")))
												satuan_dosing = dr("satuan")
											End If
										End Using

										Dim Kd_barang As String = ""
										Dim Kd_barang_inq As String = ""
										Dim satuan_barang As String = ""

										Dim No_Production_Order As String = ""
										SQL = "Select a.Kode_Barang, b.kode_barang_inq, b.satuan, a.No_PO "
										SQL = SQL & "From N_EMI_Transaksi_Trial_Split_Production_Order a, barang b Where "
										SQL = SQL & "a.kode_Perusahaan = b.Kode_Perusahaan And "
										SQL = SQL & "a.Kode_Barang = b.Kode_barang And "
										SQL = SQL & "a.kode_stock_owner=b.kode_stock_owner "
										SQL = SQL & "and a.no_transaksi ='" & Txt_NoSplit.Text & "' and a.kode_Perusahaan ='" & KodePerusahaan & "' "
										Using dr = OpenTrans(SQL)
											If dr.Read Then
												Kd_barang = dr("Kode_Barang")
												Kd_barang_inq = dr("kode_barang_inq")
												satuan_barang = dr("satuan")
												No_Production_Order = dr("No_PO")
											End If
										End Using

										Dim ID_Routing As String = ""
										SQL = "Select Id_Routing "
										SQL = SQL & "From N_EMI_Transaksi_Trial_Order_Produksi a Where "
										SQL = SQL & "a.no_faktur ='" & No_Production_Order & "' and a.kode_Perusahaan ='" & KodePerusahaan & "' "
										Using dr = OpenTrans(SQL)
											If dr.Read Then
												ID_Routing = dr("Id_Routing")
											End If
										End Using

										SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Kd_barang & "',"
										SQL = SQL & "'" & satuan_dosing & "','" & satuan_barang & "',"
										SQL = SQL & "" & Jumlah_Dosing & ") as Hasil "
										Using dr4 = OpenTrans(SQL)
											If dr4.Read Then
												If General_Class.CekNULL(dr4("Hasil")) <> "" Then

													Jumlah_Dosing_Pcs = Math.Floor(dr4("hasil"))
												Else
													dr4.Close()

													MessageBox.Show("Satuan " & satuan_dosing & " Ke " & satuan_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Return False
												End If
											End If
										End Using

										Dim satuan_bahan As String = ""

										Dim Hpp_Packaging_Total As Double = 0

										Dim Hpp_Bahan_baku_Total As Double = 0

										Dim Persen_loss_production As Double = 0

										SQL = "Select isnull(sum(nilai * dbo.get_hpp(c.Serial_Number)),0) As Total, b.satuan_barang  "
										SQL = SQL & "from N_EMI_Transaksi_Trial_Production_Results a, N_EMI_Transaksi_Trial_Production_Results_Detail b, N_EMI_Transaksi_Trial_Production_Results_det c where "
										SQL = SQL & "a.kode_perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi And a.status Is null And "
										SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.No_Transaksi = c.No_Transaksi And b.Urut = c.No_Urut_detail "
										SQL = SQL & "and b.status is null "
										SQL = SQL & "And a.Kode_Perusahaan='" & KodePerusahaan & "' and a.No_Transaksi='" & TxtFormulator_NoFaktur.Text & "' and b.proses='" & proses_temp & "' "
										SQL = SQL & "group by satuan_barang "
										Using dr = OpenTrans(SQL)
											If dr.Read Then
												satuan_bahan = dr("satuan_barang")
												Hpp_Bahan_baku_Total = Val(HilangkanTanda(Format(dr("Total"), "N0")))
											End If
										End Using

										SQL = "select Nilai_Persen from "
										SQL = SQL & "Emi_Budgeting_Loss_Production where "
										SQL = SQL & "Kode_Perusahaan='" & KodePerusahaan & "' "
										SQL = SQL & "order by Urut desc "
										Using dr = OpenTrans(SQL)
											If dr.Read Then
												Persen_loss_production = dr("Nilai_Persen")

											End If
										End Using

										SQL = "Update N_EMI_Transaksi_Trial_Production_Results_HPP set "
										SQL = SQL & "tanggal ='" & Format(tgl_skg, "yyyy-MM-dd") & "', jam='" & Format(tgl_skg, "HH:mm:ss") & "', "
										SQL = SQL & "Jumlah_Formula='" & 1 & "', jumlah_dosing='" & Jumlah_Dosing & "', "
										SQL = SQL & "satuan='" & satuan_dosing & "', Jumlah_Dosing_Pcs='" & Jumlah_Dosing_Pcs & "', "
										SQL = SQL & "Total_Bahan_Baku='" & Hpp_Bahan_baku_Total & "', Total_Packaging='" & Hpp_Packaging_Total & "', "
										SQL = SQL & "Total_Biaya_Produksi='" & Hpp_Work_Center_total & "', Nilai_Loss_Production='" & Nilai_loss_production & "', "
										SQL = SQL & "Persen_Loss_Production='" & Persen_loss_production & "', jumlah_terpakai=0 "
										SQL = SQL & "where kode_Perusahaan='" & KodePerusahaan & "' and "
										SQL = SQL & "No_Transaksi='" & TxtFormulator_NoFaktur.Text & "' and Proses='" & proses_temp & "' "
										ExecuteTrans(SQL)

									End If
								Next

							End If
						End Using

						'===================================================
						'=     CEEK APAKAH SEMUA BATCH SUDAH TERPENUHI     =
						'===================================================

#Region "Cek Semua Batch Sudah Terpenuhi"

						Dim jumlah_batch As Integer = 0
						Dim jumlah_batch_selesai As Integer = 0
						SQL = "select jumlah_batch from N_EMI_Transaksi_Trial_Split_Production_Order a "
						SQL = SQL & "where a.kode_Perusahaan='" & KodePerusahaan & "' and a.No_Transaksi='" & Txt_NoSplit.Text.Trim & "' "
						Using dr = OpenTrans(SQL)
							If dr.Read Then
								jumlah_batch = dr("jumlah_batch")
							End If
						End Using

						SQL = "Select count(b.Kode_Perusahaan) as Jumlah_selesai from N_EMI_Transaksi_Trial_Production_Results a, N_EMI_Transaksi_Trial_Production_Results_HPP b  "
						SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi And a.status Is null "
						SQL = SQL & "And a.kode_Perusahaan='" & KodePerusahaan & "' and a.No_Production_Order='" & Txt_NoSplit.Text.Trim & "' and b.Tanggal is not null "
						Using dr = OpenTrans(SQL)
							If dr.Read Then
								jumlah_batch_selesai = dr("Jumlah_selesai")
							End If
						End Using

						If jumlah_batch_selesai <> jumlah_batch Then
							IsBatchComplete = False
						Else
							IsBatchComplete = True
						End If

#End Region

						'=================================
						'=     VALIDASI GI OTOMASTIS     =
						'=================================
						If IsBatchComplete Then
							If IsAutomaticValidation Then

								'=======================================
								'=     CEK APAKAH PO SUDAH SELESAI     =
								'=======================================
								SQL = "select Flag_Hasil_Produksi_GI from N_EMI_Transaksi_Trial_Split_Production_Order "
								SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and no_transaksi = '" & Txt_NoSplit.Text.Trim & "' "
								Using Dr = OpenTrans(SQL)
									If Dr.Read Then
										If General_Class.CekNULL(Dr("Flag_Hasil_Produksi_GI")) = "Y" Then
											Dr.Close()

											MessageBox.Show("GI Sudah Selesai, Tidak Bisa Diselesaikan Lagi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Return False
										End If
									Else
										Dr.Close()

										MessageBox.Show("Data Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Return False
									End If
								End Using

								SQL = "update N_EMI_Transaksi_Trial_Split_Production_Order set Flag_Hasil_Produksi_GI = 'Y', UserID_Selesai_GI = '" & UserID & "', "
								SQL = SQL & "Tgl_Hasil_Produksi_GI = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Hasil_Produksi_GI = '" & Format(tgl_skg, "HH:mm:ss") & "'  "
								SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and no_transaksi = '" & Txt_NoSplit.Text.Trim & "' "
								ExecuteTrans(SQL)

							End If
						End If

					Next

					'==================
					'=     JURNAL     =
					'==================

#Region "JURNAL"

					Dim inisial_faktur_dari As String = ""
					Dim fso As String = ""
					SQL = "Select b.Inisial_Faktur,a.Kode_Stock_Owner from N_EMI_Transaksi_Trial_Split_Production_Order a,Stock_Owner_Gudang b "
					SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Stock_Owner = b.Kode_Stock_Owner "
					SQL = SQL & "And a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & Txt_NoSplit.Text & "' "
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							inisial_faktur_dari = Dr("inisial_faktur")
							fso = Dr("Kode_Stock_Owner")
						Else
							Dr.Close()

							MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Return False
						End If
					End Using

					Dim akun_Loss_production As String = ""
					Dim akun_persedian_brg_dlm_proses As String = ""

					Dim ket_loss_production As String = ""
					Dim ket_persedian_brg_dlm_proses As String = ""

					'awal persediaan barang dalam proses
					SQL = "select Persediaan_Barang_Dalam_Proses, Penyusutan_Barang_Dalam_Proses from stock_owner_gudang "
					SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & fso & "' "
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							akun_persedian_brg_dlm_proses = Dr("Persediaan_Barang_Dalam_Proses")
							ket_persedian_brg_dlm_proses = "Persediaan Barang Dalam Proses "

							akun_Loss_production = Dr("Penyusutan_Barang_Dalam_Proses")
							ket_loss_production = "Penyusutan Barang Dalam Proses "
						Else
							Dr.Close()

							MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Return False
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
					SQL = SQL & "'" & KodeProyek & "', 'Pengeluaran Bahan Baku " & TxtFormulator_NoFaktur.Text & "', '', "
					SQL = SQL & "'-', '" & UserID & "')"
					ExecuteTrans(SQL)

					Dim ftotal_barang_Dalam_Proses As Double = 0

					Dim ket_packaging As String = ""
					Dim akun_kredit_packaging As String = ""
					Dim lok_packaging As String = ""

					Nilai_Bahan = Math.Round(Nilai_Bahan, 0)
					ftotal_barang_Dalam_Proses = Nilai_Bahan + Nilai_Packaging + Nilai_loss_production + Hpp_Work_Center_total

					If ftotal_barang_Dalam_Proses = 0 Then
						MessageBox.Show("tidak ada data yang di jurnal...!!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Return False
					End If

					SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persedian_brg_dlm_proses, 1),
							 Strings.Mid(akun_persedian_brg_dlm_proses, 2, 1),
							 Strings.Mid(Ganti(akun_persedian_brg_dlm_proses), 3),
							 KodePerusahaan, KodeProyek, ket_persedian_brg_dlm_proses & TxtFormulator_NoFaktur.Text, ftotal_barang_Dalam_Proses, "0", pagenumber, fso, Bahasa_Pilihan, Ket_Cost_Center_HO)
					ExecuteTrans(SQL)
					pagenumber = pagenumber + 1

					Dim Temp_Nilai_Bahan As Double = 0
					If Nilai_Bahan <> 0 Then

						Dim ket_material As String = ""
						Dim akun_kredit_material As String = ""
						Dim lok_material As String = ""

						SQL = "select top(1) "
						SQL = SQL & "b.Id_Group_Jenis, b.kode_stock_owner, c.akun_persediaan, Kode_Group_Jenis "
						SQL = SQL & "from N_EMI_Transaksi_Trial_Production_Results_det a, Barang b, EMI_Group_Jenis_Akun c, "
						SQL = SQL & "N_EMI_Transaksi_Trial_Production_Results_Detail e, EMI_Group_Jenis f  "
						SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
						SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
						SQL = SQL & "and b.Kode_Perusahaan = f.Kode_Perusahaan and b.Id_Group_Jenis = f.Id_Group_Jenis "
						SQL = SQL & "and f.Kode_Perusahaan = c.Kode_Perusahaan and f.Id_Group_Jenis = c.Id_Group_Jenis "
						SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
						SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.No_Transaksi = e.No_Transaksi "
						SQL = SQL & "and a.Kode_Stock_Owner = e.Kode_Stock_Owner and a.Kode_Barang = e.Kode_Barang "
						SQL = SQL & "and a.No_Urut_Detail = e.Urut "
						SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
						SQL = SQL & "and a.No_Transaksi = '" & TxtFormulator_NoFaktur.Text & "' "
						SQL = SQL & "and e.status is null "
						Using Ds1 = BindingTrans(SQL)
							If Ds1.Tables("MyTable").Rows.Count <> 0 Then
								For h As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1

									lok_material = Ds1.Tables("MyTable").Rows(h).Item("kode_stock_owner")
									akun_kredit_material = Ds1.Tables("MyTable").Rows(h).Item("akun_persediaan")
									ket_material = "Persediaan " + Ds1.Tables("MyTable").Rows(h).Item("Kode_Group_Jenis")

								Next
							End If
						End Using

						'TODO : Insert dengan cara loop array detail
						For p As Integer = 0 To Arr_Detail_Biaya.Count - 1
							SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Arr_Detail_Biaya(p).akun, 1),
				Strings.Mid(Arr_Detail_Biaya(p).akun, 2, 1),
				Strings.Mid(Ganti(Arr_Detail_Biaya(p).akun), 3),
				KodePerusahaan, KodeProyek, $"Persediaan {Arr_Detail_Biaya(p).keterangan}" & " " & TxtFormulator_NoFaktur.Text, "0", Math.Round(Arr_Detail_Biaya(p).nilai, 0), pagenumber, Arr_Detail_Biaya(p).kd_so, Bahasa_Pilihan, Ket_Cost_Center_HO)
							ExecuteTrans(SQL)

							Temp_Nilai_Bahan += Math.Round(Arr_Detail_Biaya(p).nilai, 0)
						Next

						'SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_kredit_material, 1),
						'  Strings.Mid(akun_kredit_material, 2, 1),
						'  Strings.Mid(Ganti(akun_kredit_material), 3),
						'  KodePerusahaan, KodeProyek, ket_material & TxtFormulator_NoFaktur.Text, "0", Nilai_Bahan, pagenumber, lok_material, Bahasa_Pilihan, Ket_Cost_Center_HO)
						'ExecuteTrans(SQL)
						pagenumber = pagenumber + 1
					End If

					If Nilai_Bahan <> Math.Round(Temp_Nilai_Bahan, 0) Then

						MessageBox.Show("Terjadi Kesalahan, Nilai Bahan Tidak Sama !!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Return False
					End If

					If Nilai_Packaging <> 0 Then
						SQL = "select top(1) "
						SQL = SQL & "b.Id_Group_Jenis, b.kode_stock_owner, c.akun_persediaan, Kode_Group_Jenis "
						SQL = SQL & "from N_EMI_Transaksi_Trial_Production_Results_Packaging_Det a, Barang b, EMI_Group_Jenis_Akun c, "
						SQL = SQL & "N_EMI_Transaksi_Trial_Production_Results_Packaging_Detail e, EMI_Group_Jenis f where "
						SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
						SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
						SQL = SQL & "and b.Kode_Perusahaan = f.Kode_Perusahaan and b.Id_Group_Jenis = f.Id_Group_Jenis "
						SQL = SQL & "and f.Kode_Perusahaan = c.Kode_Perusahaan and f.Id_Group_Jenis = c.Id_Group_Jenis "
						SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
						SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.No_Transaksi = e.No_Transaksi "
						SQL = SQL & "and a.Kode_Stock_Owner = e.Kode_Stock_Owner and a.Kode_Barang = e.Kode_Barang "
						SQL = SQL & "and a.No_Urut_Detail = e.Urut "
						SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
						SQL = SQL & "and a.No_Transaksi = '" & TxtFormulator_NoFaktur.Text & "' "
						SQL = SQL & "and e.status is null "
						Using Ds1 = BindingTrans(SQL)
							If Ds1.Tables("MyTable").Rows.Count <> 0 Then
								For h As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1

									lok_packaging = Ds1.Tables("MyTable").Rows(h).Item("kode_stock_owner")
									akun_kredit_packaging = Ds1.Tables("MyTable").Rows(h).Item("akun_persediaan")
									ket_packaging = "Persediaan " + Ds1.Tables("MyTable").Rows(h).Item("Kode_Group_Jenis")

								Next
							End If
						End Using

						SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_kredit_packaging, 1),
		   Strings.Mid(akun_kredit_packaging, 2, 1),
		   Strings.Mid(Ganti(akun_kredit_packaging), 3),
		   KodePerusahaan, KodeProyek, ket_packaging & TxtFormulator_NoFaktur.Text, "0", Nilai_Packaging, pagenumber, lok_packaging, Bahasa_Pilihan, Ket_Cost_Center_HO)
						ExecuteTrans(SQL)
						pagenumber = pagenumber + 1

					End If

					If Nilai_loss_production <> 0 Then

						SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_Loss_production, 1),
				Strings.Mid(akun_Loss_production, 2, 1),
				Strings.Mid(Ganti(akun_Loss_production), 3),
				KodePerusahaan, KodeProyek, ket_loss_production & TxtFormulator_NoFaktur.Text, "0", Nilai_loss_production, pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
						ExecuteTrans(SQL)
						pagenumber = pagenumber + 1

					End If

					For index = 0 To arrID_Work_Center.Count - 1
						SQL = "Select Kode_Akun_Biaya, Kode_Akun_Budget, a.keterangan "
						SQL = SQL & "From Emi_Jenis_Biaya_Produksi a where "
						SQL = SQL & " a.Kode_Perusahaan = '" & KodePerusahaan & "' "
						SQL = SQL & "and kode_jenis_biaya_Produksi = '" & arrJenis_Biaya.Item(index) & "' "
						Using Ds1 = BindingTrans(SQL)
							If Ds1.Tables("MyTable").Rows.Count <> 0 Then
								For h As Integer = 0 To .Rows.Count - 1

									Dim akun As String = Ds1.Tables("MyTable").Rows(h).Item("Kode_Akun_Budget")
									Dim ket As String = Ds1.Tables("MyTable").Rows(h).Item("keterangan")

									SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun, 1),
						Strings.Mid(akun, 2, 1),
						Strings.Mid(Ganti(akun), 3),
						KodePerusahaan, KodeProyek, ket & " " & TxtFormulator_NoFaktur.Text, "0", arr_biaya_Produksi.Item(index), pagenumber, Lokasi, Bahasa_Pilihan, arrID_Work_Center.Item(index))
									ExecuteTrans(SQL)
									pagenumber = pagenumber + 1

								Next
							End If
						End Using
					Next

					SQL = "select round(sum(debit),2) as debit, round(sum(kredit),2) as kredit from detail_jurnal where "
					SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
					SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							If Dr("debit") <> Dr("kredit") Then
								Dr.Close()

								MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Return False
							End If
						Else
							Dr.Close()

							MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Return False
						End If
					End Using

					SQL = "insert into N_EMI_Transaksi_Trial_Production_Results_Jurnal (Kode_Perusahaan,No_Transaksi,Kode_Voucher,Proses, Jenis) values ("
					SQL = SQL & "'" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "','" & Kode_voucher & "',"
					SQL = SQL & "'" & CurrentBatch & "', 'GI') "
					ExecuteTrans(SQL)

#End Region

				End If
			End With
		End Using

		'If True Then
		'
		'
		'    MessageBox.Show("Stop Tahan...")
		'    Return False
		'End If

		Return True

	End Function

	Private Sub Simpan_Data(ByVal isNormalSave As Boolean)
		get_jam()

		'Dim kode_unik_print, kode_unik_print_scrap As String

		Dim arr_kode_unik_print As New ArrayList

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'==========================
			'=     GET DATA BATCH     =
			'==========================
			Dim arrBatchPersediaan As New ArrayList
			SQL = "select distinct Batch from N_EMI_Transaksi_Trial_Penyediaan_Bahan_Baku "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and No_Split = '{Txt_NoSplit.Text}' "
			SQL &= $"and Status is null "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					arrBatchPersediaan.Add(Dr("Batch"))
				Loop
			End Using

			'HAPUS TABEL SEMENTARA
			'SQL = "truncate table Cetak_Finish_Good "
			SQL = "delete N_EMI_Transaksi_Trial_Barcode_Label_Barcode_GR_1 "
			ExecuteTrans(SQL)

			SQL = "select no_transaksi, "
			SQL = SQL & "isnull((select top(1) proses from ( "
			SQL = SQL & "Select proses from N_EMI_Transaksi_Trial_Production_Results_Detail x where "
			SQL = SQL & "a.Kode_Perusahaan = x.Kode_Perusahaan And a.No_Transaksi = x.No_Transaksi "
			SQL = SQL & "union all "
			SQL = SQL & "Select proses from N_EMI_Transaksi_Trial_Production_Results_Packaging_Detail x where "
			SQL = SQL & "a.Kode_Perusahaan = x.Kode_Perusahaan And a.No_Transaksi = x.No_Transaksi "
			SQL = SQL & ") as Data order by proses desc ),0) as proses "
			SQL = SQL & "from N_EMI_Transaksi_Trial_Production_Results a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Production_Order = '" & Txt_NoSplit.Text & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					TxtFormulator_NoFaktur.Text = Dr("no_transaksi")
				Else
					Dr.Close()

					GetNoFakturGI()

					SQL = "INSERT INTO N_EMI_Transaksi_Trial_Production_Results(Kode_Perusahaan,No_Transaksi,No_Production_Order,Tanggal,Jam,UserID"
					SQL = SQL & ") VALUES('" & KodePerusahaan & "',"
					SQL = SQL & "'" & TxtFormulator_NoFaktur.Text & "','" & Txt_NoSplit.Text.Trim & "','" & Format(tgl_skg, "yyyy-MM-dd") & "',"
					SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & UserID & "')"
					ExecuteTrans(SQL)

					'proses = 1
				End If
			End Using

			For zzz As Integer = 0 To Dgv_Batch.Rows.Count - 1

				Dim CurrentBatch As Integer = Dgv_Batch.Rows(zzz).Cells(0).Value
				Dim CurrentCheckBox As Boolean = Dgv_Batch.Rows(zzz).Cells(1).Value
				Dim CurrentJmlhInput As String = Dgv_Batch.Rows(zzz).Cells(2).Value
				Dim CurrentJenisQuality As String = Dgv_Batch.Rows(zzz).Cells(3).Value
				Dim CurrentComboBoxJenisQuality As DataGridViewComboBoxCell = DirectCast(Dgv_Batch.Rows(zzz).Cells(3), DataGridViewComboBoxCell)
				Dim CurrentTrolly As String = Dgv_Batch.Rows(zzz).Cells(4).Value

				Dim CurrentIndexComboBoxJenisQuality As Integer = -1
				If CurrentJenisQuality IsNot Nothing Then
					CurrentIndexComboBoxJenisQuality = CurrentComboBoxJenisQuality.Items.IndexOf(CurrentJenisQuality)
				End If

				If Not CurrentCheckBox Then Continue For

				If Not arrBatchPersediaan.Contains(CurrentBatch) Then
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan Batch {CurrentBatch} Belum Melakukan Persediaan Bahan Baku", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

				'===============================
				'=     INPUT GR DETAIL HPP     =
				'===============================
				SQL = "Select a.Kode_Perusahaan, a.tanggal from "
				SQL = SQL & "N_EMI_Transaksi_Trial_Production_Results_HPP a where "
				SQL = SQL & "a.Kode_Perusahaan ='" & KodePerusahaan & "' and "
				SQL = SQL & "No_Transaksi='" & TxtFormulator_NoFaktur.Text & "' and "
				SQL = SQL & "proses='" & CurrentBatch & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then

						If General_Class.CekNULL(dr("tanggal")) <> "" Then
							Continue For
						Else
							dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show($"Terjadi Kesalahan Batch {CurrentBatch} Sudah Diinput Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					Else

						dr.Close()
						SQL = "insert into N_EMI_Transaksi_Trial_Production_Results_HPP "
						SQL = SQL & "(Kode_Perusahaan, No_Transaksi, Proses) "
						SQL = SQL & "Values('" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & CurrentBatch & "')"
						ExecuteTrans(SQL)
					End If
				End Using

				If Not Simpan_Data_GI(CurrentBatch, CurrentCheckBox, CurrentJmlhInput, CurrentJenisQuality, CurrentTrolly) Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("Terjadi Kesalahan pada Step GI", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

				'=====================
				'=     PROSES GR     =
				'=====================
				Dim Nilai_Packaging As Double = 0
				Dim nilai_packaging_Pcs As Double = 0

				Dim nilai_Produksi As Double = 0
				Dim Hpp_Work_Center_total As Double = 0
				Dim Hpp_Work_Center_Pcs As Double = 0

				Dim Hpp_Work_Center_totalSCP As Double = 0
				Dim Hpp_Work_Center_PcsSCP As Double = 0

				Dim Nilai_loss_production_Total As Double = 0
				Dim Nilai_loss_production_TotalSCP As Double = 0

				Dim Nilai_Bahan_Baku_Total As Double = 0
				Dim Nilai_Bahan_Baku_TotalSCP As Double = 0

				Dim TotalTf As Double = 0
				Dim TotalHPP As Double = 0
				Dim TotalHPPScrap As Double = 0

				Dim Kode_unik_print As String = ""

				Dim proses As Integer
				SQL = "select TOP(1) no_transaksi, "
				SQL = SQL & "isnull((select top(1) proses from N_EMI_Transaksi_Trial_Production_Results_Detail_Barang x where a.Kode_Perusahaan = x.Kode_Perusahaan "
				SQL = SQL & "and a.No_Transaksi = x.No_Transaksi order by proses desc "
				SQL = SQL & "),0) as proses "
				SQL = SQL & "from N_EMI_Transaksi_Trial_Production_Results a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Production_Order = '" & Txt_NoSplit.Text & "' order by proses desc "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						TxtFormulator_NoFaktur.Text = Dr("no_transaksi")
						proses = Dr("proses") + 1
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Data Tidak di temukan . !!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				Dim kd_inq As String = ""
				Dim Satuan_Barang_Produksi As String = ""
				SQL = "select top(1) kode_barang_inq, Satuan from barang a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.kode_barang = '" & Txt_KdBarang.Text & "'  "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						kd_inq = Dr("kode_barang_inq")
						Satuan_Barang_Produksi = Dr("Satuan")
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Data Tidak di temukan . !!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				Dim SoProduction As String
				Dim berat As Double = 0
				SQL = "select Kode_Stock_Owner From Stock_Owner_Gudang "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Produksi = 'Y' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						SoProduction = Dr("kode_stock_owner")
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Lokasi Produksi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				SQL = "select top(1) berat From barang "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_barang='" & Txt_KdBarang.Text & "' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						berat = Dr("berat")
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Lokasi Produksi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				If TxtJmlScrap.Text = "" Then TxtJmlScrap.Text = "0"

				Dim badstock As Double = 0
				Dim goodstock As Double = 0

				If Not CurrentIndexComboBoxJenisQuality = -1 Then
					If ArrDataJenis(CurrentIndexComboBoxJenisQuality).Kode_Warna = "KUNING" Then
						goodstock = Val(HilangkanTanda(CurrentJmlhInput))
						badstock = 0
					Else
						goodstock = 0
						badstock = Val(HilangkanTanda(CurrentJmlhInput))
					End If
				Else
					goodstock = 0
					badstock = 0
				End If

				Dim kd_barang_scrap As String = ""

				If Val(TxtJmlScrap.Text) = 0 Then
					kd_barang_scrap = "NULL"
				Else
					kd_barang_scrap = "'" & arrKdBarangSrap.Item(CmbSisaProduksi.SelectedIndex) & "'"
				End If

				'Get data Barang berdasarkan NoSplit
				Dim Kd_So As String = ""
				Dim Kd_Brg As String = ""
				Dim No_Production_Order As String = ""
				SQL = "Select b.Status,b.Selesai,b.Kode_Stock_Owner,b.Kode_Barang, a.No_PO "
				SQL = SQL & "from N_EMI_Transaksi_Trial_Split_Production_Order a,N_EMI_Transaksi_Trial_Order_Produksi b "
				SQL = SQL & "where a.No_PO = b.No_Faktur "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and a.No_Transaksi = '" & Txt_NoSplit.Text & "'"
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						Kd_So = dr("Kode_Stock_Owner")
						Kd_Brg = dr("Kode_Barang")
						No_Production_Order = dr("No_PO")

						If General_Class.CekNULL(dr("Status")) <> "" Then
							dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show(Base_Language.Lang_Global_NoFaktur & " " & Base_Language.Lang_Global_DataSudahBatal, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End If
				End Using

				Dim fbulan As String = Format(tgl_skg, "MM")
				Dim ftahun As String = Format(tgl_skg, "yyyy")

				'Semua Berat Dalam Gram, gimna pastiin Tidak ada Berat Yang Salah ??
				Dim Nilai_berat_FG As Double = Math.Round(Val(HilangkanTanda(CurrentJmlhInput)) * berat / 1000, 4)

#Region "Ambil Packaging"

				'==================================
				'=     POTONG STOCK PACKAGING     =
				'==================================
				'GET DETAIL DATA PACKAGING BY NO SPLIT
				SQL = "select a.No_Transaksi, b.jumlah_barang, b.jumlah_bahan, "
				SQL = SQL & "isnull(( select z.kode_barang_inq from barang z where a.kode_perusahaan = z.kode_perusahaan "
				SQL = SQL & "and a.kode_stock_owner = z.kode_stock_owner and a.kode_barang = z.kode_barang "
				SQL = SQL & "), '-') as Kode_Barang, "
				SQL = SQL & "b.Kode_Stock_Owner, b.Kode_Barang as Kode_Bahan, c.Nama ,b.Jumlah, b.Satuan, c.flag_potong_stok,isnull(c.standar_price,0) as standar_price, isnull(c.Flag_Pembulatan_Produksi,'T') as Flag_Pembulatan_Produksi "
				SQL = SQL & "from N_EMI_Transaksi_Trial_Split_Production_Order a, N_EMI_Transaksi_Trial_Split_Production_Order_Detail_Packaging b, barang c "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
				SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
				SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & Txt_NoSplit.Text & "'  "
				If Not isNormalSave Then
					SQL = SQL & ""
					SQL = SQL & "and b.jenis = 'KEMASAN UTAMA' "
				End If
				SQL = SQL & "order by c.nama "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For i As Integer = 0 To .Rows.Count - 1

								'================================================================
								'=     GET JUMLAH KEBUTUHAN PACKAGING TERHADAP KD_BARANG PO     =
								'================================================================
								Dim jumlahKebutuhanPackaging As Double = 0
								Dim jumlahBarangPerBahan As Double = 0

								jumlahKebutuhanPackaging = Val(HilangkanTanda(Format(.Rows(i).Item("jumlah_bahan"), "N4")))
								jumlahBarangPerBahan = .Rows(i).Item("jumlah_barang")
								'SQL = "select c  "
								'SQL = SQL & "from Barang_Detail_Bahan_Penolong  "
								'SQL = SQL & "where kode_barang='" & .Rows(i).Item("Kode_Barang") & "' "
								'SQL = SQL & "and kode_Bahan = '" & .Rows(i).Item("Kode_Bahan") & "' "
								'SQL = SQL & "and Kode_Perusahaan = '" & KodePerusahaan & "'"
								'Using Dr = OpenTrans(SQL)
								'    If Dr.Read Then
								'        jumlahKebutuhanPackaging = Val(HilangkanTanda(Format(Dr("jumlah_bahan"), "N4")))
								'        jumlahBarangPerBahan = Dr("jumlah_barang")
								'    End If
								'End Using

								Dim NilaiProduksiPck As Double = Val(HilangkanTanda(Format((Val(HilangkanTanda(CurrentJmlhInput)) / jumlahBarangPerBahan) * jumlahKebutuhanPackaging, "N4")))

								If .Rows(i).Item("Flag_Pembulatan_Produksi") = "Y" Then
									NilaiProduksiPck = Math.Ceiling(NilaiProduksiPck)
								End If

								'======                              =========='
								'======   Awal convert satuan barang =========='
								'=========                           =========='
								Dim convertKeSatuanAsli_pckg As String = ""
								Dim jumlahConvertPckg As Double = 0

								If NilaiProduksiPck > 0 Then
									SQL = "select satuan From barang where Kode_barang = '" & .Rows(i).Item("Kode_Bahan") & "' "
									SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' "
									Using Dr3 = OpenTrans(SQL)
										If Dr3.Read Then
											convertKeSatuanAsli_pckg = Dr3("satuan")
											'SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(i).Item("Kode_Bahan") & "',"
											'SQL = SQL & "'" & .Rows(i).Item("Satuan") & "','" & Dr3("satuan") & "',"
											'SQL = SQL & "" & HilangkanTanda(NilaiProduksiPck) & ") as Hasil "
											'Dr3.Close()
											'Using dr4 = OpenTrans(SQL)
											'    If dr4.Read Then
											'        If General_Class.CekNULL(dr4("Hasil")) <> "" Then
											'            If dr4("Hasil") = 0 Then
											'                dr4.Close()
											'                CloseTrans()
											'                CloseConn()
											'                MessageBox.Show("Satuan " & .Rows(i).Item("Satuan") & " Ke " & convertKeSatuanAsli_pckg & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											'                Exit Sub
											'            Else
											'                jumlahConvertPckg = Val(HilangkanTanda(Format(dr4("hasil"), "N4")))

											'            End If
											'        Else
											'            dr4.Close()
											'            CloseTrans()
											'            CloseConn()

											'            MessageBox.Show("Gagal Convert Satuan. . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											'            Exit Sub
											'        End If
											'    End If
											'End Using

											jumlahConvertPckg = Val(HilangkanTanda(Format(NilaiProduksiPck, "N4")))
										Else
											Dr3.Close()
											CloseTrans()
											CloseConn()
											MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									End Using

									'======                              =========='
									'======   Akhir convert satuan barang =========='
									'=========                           =========='
									SQL = "INSERT INTO N_EMI_Transaksi_Trial_Production_Results_Packaging_Detail(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,Nilai_Formula,Nilai_Produksi,Satuan,proses,"
									SQL = SQL & "nilai_barang,satuan_barang,userid,tanggal,jam) "
									SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "','" & .Rows(i).Item("Kode_Stock_Owner") & "','" & .Rows(i).Item("Kode_Bahan") & "',"
									SQL = SQL & "'" & HilangkanTanda(.Rows(i).Item("Jumlah")) & "','" & NilaiProduksiPck & "','" & .Rows(i).Item("Satuan") & "', '" & proses & "',"
									SQL = SQL & "'" & jumlahConvertPckg & "', '" & convertKeSatuanAsli_pckg & "', '" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
									SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "'"
									SQL = SQL & ") "
									ExecuteTrans(SQL)

									Dim x_ident_currentPackaging As Integer = 0
									SQL = "select IDENT_CURRENT('N_EMI_Transaksi_Trial_Production_Results_Packaging_Detail') as urutan"
									Using Dr = OpenTrans(SQL)
										If Dr.Read Then
											x_ident_currentPackaging = Dr("urutan")
										End If
									End Using

									SQL = "select round(good_stock,4) as good_stock, flag_ppn from barang where "
									SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
									SQL = SQL & "kode_stock_owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and "
									SQL = SQL & "kode_barang = '" & .Rows(i).Item("Kode_Bahan") & "'"
									Using Dss = BindingTrans(SQL)

										If Dss.Tables("MyTable").Rows.Count <> 0 Then
											If Dss.Tables("MyTable").Rows(0).Item("good_stock") - jumlahConvertPckg < BolehNegatif Then
												CloseTrans()
												CloseConn()
												MessageBox.Show("Proses membuat stock menjadi negatif untuk kode barang " & .Rows(i).Item("Kode_Bahan") & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
												Exit Sub
											Else
												SQL = "Update barang set good_stock = good_stock - " & jumlahConvertPckg & " where "
												SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
												SQL = SQL & "kode_stock_owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and "
												SQL = SQL & "kode_barang = '" & .Rows(i).Item("Kode_Bahan") & "'"
												'ExecuteTrans(SQL)
											End If
										Else
											CloseTrans()
											CloseConn()
											MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
											Exit Sub
										End If

									End Using

									Dim lewatin As String = "T"
									SQL = "select isnull(round(sum(jumlah),4), 0) as stock from barang_sn where "
									SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
									SQL = SQL & "kode_stock_owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and "
									SQL = SQL & "kode_barang = '" & .Rows(i).Item("Kode_Bahan") & "' and jumlah <> 0 "
									Using Dr = OpenTrans(SQL)
										If Dr.Read Then
											If Dr("stock") < Val(jumlahConvertPckg) Then
												lewatin = "Y"
											Else
												lewatin = "T"
											End If
										Else
											Dr.Close()
											CloseTrans()
											CloseConn()
											MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & .Rows(i).Item("Kode_Bahan") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									End Using

									'==================================================================================
									'======================  CHECK APAKAH FLAG POTONG STOK NYA Y atau T ================
									'==================================================================================
									'If LvPotStokPckg = "Y" Then
									If lewatin = "T" Then
										Dim sisa As Double = 0
										SQL = "select kode_stock_owner, kode_barang, serial_number, dbo.get_hpp(Serial_Number) as HPP, round(jumlah,4) as jumlah from barang_sn where "
										SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
										SQL = SQL & "kode_stock_owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and "
										SQL = SQL & "kode_barang = '" & .Rows(i).Item("Kode_Bahan") & "' and jumlah <> 0 "
										SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
										Using Dss = BindingTrans(SQL)
											If Dss.Tables("MyTable").Rows.Count <> 0 Then
												sisa = Val(jumlahConvertPckg)
												For h As Integer = 0 To Dss.Tables("MyTable").Rows.Count - 1
													If sisa = 0 Then
														Exit For
													ElseIf sisa < 0 Then
														CloseTrans()
														CloseConn()
														MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Exit Sub
													End If

													Dim hpp As Double = Dss.Tables("MyTable").Rows(h).Item("HPP")

													If sisa < Dss.Tables("MyTable").Rows(h).Item("jumlah") Or sisa = Dss.Tables("MyTable").Rows(h).Item("jumlah") Then
														SQL = "Update barang_sn set jumlah = jumlah - " & sisa & " where "
														SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
														SQL = SQL & "kode_stock_owner = '" & Dss.Tables("MyTable").Rows(h).Item("kode_stock_owner") & "' and "
														SQL = SQL & "kode_barang = '" & Dss.Tables("MyTable").Rows(h).Item("kode_barang") & "' and "
														SQL = SQL & "serial_number = '" & Dss.Tables("MyTable").Rows(h).Item("serial_number") & "'"
														'ExecuteTrans(SQL)

														SQL = "INSERT INTO N_EMI_Transaksi_Trial_Production_Results_Packaging_Det(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,"
														SQL = SQL & "Nilai,Serial_Number,no_urut_detail) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
														SQL = SQL & "'" & Dss.Tables("MyTable").Rows(h).Item("kode_stock_owner") & "','" & Dss.Tables("MyTable").Rows(h).Item("kode_barang") & "',"
														SQL = SQL & "" & sisa & ",'" & Dss.Tables("MyTable").Rows(h).Item("serial_number") & "', '" & x_ident_currentPackaging & "')"
														ExecuteTrans(SQL)

														Nilai_Packaging = Nilai_Packaging + (hpp * sisa)
														sisa = 0
													ElseIf sisa > Dss.Tables("MyTable").Rows(h).Item("jumlah") Then
														SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
														SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
														SQL = SQL & "kode_stock_owner = '" & Dss.Tables("MyTable").Rows(h).Item("kode_stock_owner") & "' and "
														SQL = SQL & "kode_barang = '" & Dss.Tables("MyTable").Rows(h).Item("kode_barang") & "' and "
														SQL = SQL & "serial_number = '" & Dss.Tables("MyTable").Rows(h).Item("serial_number") & "'"
														'ExecuteTrans(SQL)

														SQL = "INSERT INTO N_EMI_Transaksi_Trial_Production_Results_Packaging_Det(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,"
														SQL = SQL & "Nilai,Serial_Number,no_urut_detail) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
														SQL = SQL & "'" & Dss.Tables("MyTable").Rows(h).Item("kode_stock_owner") & "','" & Dss.Tables("MyTable").Rows(h).Item("kode_barang") & "',"
														SQL = SQL & "" & Dss.Tables("MyTable").Rows(h).Item("jumlah") & ",'" & Dss.Tables("MyTable").Rows(h).Item("serial_number") & "', '" & x_ident_currentPackaging & "')"
														ExecuteTrans(SQL)

														Nilai_Packaging = Nilai_Packaging + (hpp * Val(HilangkanTanda(Format(Dss.Tables("MyTable").Rows(h).Item("jumlah"), "N4"))))
														sisa = sisa - Val(HilangkanTanda(Format(Dss.Tables("MyTable").Rows(h).Item("jumlah"), "N4")))
													Else
														CloseTrans()
														CloseConn()
														MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & .Rows(i).Item("Kode_Bahan") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Exit Sub
													End If

													If Math.Round(sisa, 4) <> 0 And h = Dss.Tables("MyTable").Rows.Count - 1 Then
														CloseTrans()
														CloseConn()
														MessageBox.Show("Jumlah stock tidak mencukupi untuk kode barang " & .Rows(i).Item("Kode_Bahan") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Exit Sub
													End If
												Next ' for barang sn
											End If 'count <> 0

										End Using
									Else
										CloseTrans()
										CloseConn()
										MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & .Rows(i).Item("Kode_Bahan") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If
								End If

							Next
						Else
							'CloseTrans()
							'CloseConn()
							'MessageBox.Show("Data Packaging Terhadap No Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'Exit Sub
						End If
					End With
				End Using

				'Ini Nilai Packaging Per Detail

				Nilai_Packaging = Math.Round(Nilai_Packaging, 0)

				If Nilai_Packaging <> 0 Then

					nilai_packaging_Pcs = Val(HilangkanTanda(Format(Nilai_Packaging / Val(HilangkanTanda(CurrentJmlhInput)), "N0")))

					SQL = "insert into N_EMI_Transaksi_Trial_Production_Results_Detail_Biaya(kode_perusahaan, No_Transaksi, Proses, "
					SQL = SQL & "Jenis, Jumlah_Dosing, Hpp_Per_Pcs, HPP_Total, Urut_HPP, Jenis_Barang, Jumlah_Hitung) values( "
					SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & proses & "', "
					SQL = SQL & "'PACKAGING', '" & Nilai_berat_FG & "', '" & nilai_packaging_Pcs & "', '" & Nilai_Packaging & "', NULL, 'FG', '" & Nilai_berat_FG & "')"
					ExecuteTrans(SQL)

				End If

#End Region

#Region "Ambil Work Center"

				''FINISHED GooD
				SQL = "insert into N_EMI_Transaksi_Trial_Production_Results_Detail_Biaya(kode_perusahaan, No_Transaksi, Proses, "
				SQL = SQL & "Jenis, Jumlah_Dosing, Hpp_Per_Pcs, HPP_Total, Urut_HPP, Jenis_Barang, Jumlah_Hitung) values( "
				SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & proses & "', "
				SQL = SQL & "'BIAYA', '" & Nilai_berat_FG & "', '" & 0 & "', '" & 0 & "', NULL, 'FG', '" & Nilai_berat_FG & "')"
				ExecuteTrans(SQL)

				Dim x_ident_currentBiaya As Integer = 0
				SQL = "select IDENT_CURRENT('N_EMI_Transaksi_Trial_Production_Results_Detail_Biaya') as urutan"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						x_ident_currentBiaya = Dr("urutan")
					End If
				End Using

				SQL = "select kode_perusahaan from N_EMI_Transaksi_Trial_Production_Results_Detail_Biaya where "
				SQL = SQL & "kode_perusahaan='" & KodePerusahaan & "' and No_transaksi='" & TxtFormulator_NoFaktur.Text & "' and "
				SQL = SQL & "Proses='" & proses & "' and Jenis='BIAYA' and Urut='" & x_ident_currentBiaya & "' and Jenis_Barang ='FG' "
				Using dr = OpenTrans(SQL)
					If Not dr.Read Then
						dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Terjadi kesalahan . . !!, Ulangi Transaksi .", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				Dim ID_Routing As String = ""
				SQL = "Select Id_Routing "
				SQL = SQL & "From N_EMI_Transaksi_Trial_Order_Produksi a Where "
				SQL = SQL & "a.no_faktur ='" & No_Production_Order & "' and a.kode_Perusahaan ='" & KodePerusahaan & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						ID_Routing = dr("Id_Routing")
					End If
				End Using

				SQL = ";with cte as ( "
				SQL = SQL & "Select a.kode_perusahaan, a.Id_Jenis_Biaya_Produksi, a.Kode_Jenis_Biaya_Produksi, "
				SQL = SQL & "isnull((select top(1) no_faktur from Emi_Transaksi_Work_Center x where x.status Is null "
				SQL = SQL & "And x.Kode_Perusahaan=a.Kode_Perusahaan And x.jenis_biaya=a.Kode_Jenis_Biaya_Produksi order by id desc),NULL) as Faktur_WC "
				SQL = SQL & "From Emi_Jenis_Biaya_Produksi a "
				SQL = SQL & ")select a.kode_jenis_biaya_produksi, c.id_work_center, max(c.Nilai_Per_pcs) as Nilai_Per_pcs "
				SQL = SQL & "From cte a, Emi_Transaksi_Work_Center b, Emi_Transaksi_Work_Center_detail c Where "
				SQL = SQL & "a.kode_perusahaan = b.Kode_Perusahaan And a.faktur_WC = b.No_Faktur And "
				SQL = SQL & "b.kode_perusahaan = c.Kode_Perusahaan And b.No_Faktur = c.No_Faktur And c.Id_Routing = '" & ID_Routing & "' "
				SQL = SQL & "group by a.kode_jenis_biaya_produksi, c.id_work_center "
				Using Ds5 = BindingTrans(SQL)
					If Ds5.Tables("MyTable").Rows.Count <> 0 Then

						For indxx = 0 To Ds5.Tables("MyTable").Rows.Count - 1

							Dim id_WC As String = Ds5.Tables("MyTable").Rows(indxx).Item("id_work_center")
							Dim Jenis_biaya As String = Ds5.Tables("MyTable").Rows(indxx).Item("kode_jenis_biaya_produksi")
							Dim Nilai_WC As Double = Ds5.Tables("MyTable").Rows(indxx).Item("Nilai_Per_pcs")
							Dim Nilai_WC_Total As Double = Math.Round(Ds5.Tables("MyTable").Rows(indxx).Item("Nilai_Per_pcs") * Nilai_berat_FG, 0)

							If Nilai_WC <> 0 Then

								Hpp_Work_Center_total += Math.Round(Nilai_WC * Nilai_berat_FG)

								SQL = "insert into N_EMI_Transaksi_Trial_Production_Results_Detail_Biaya_WC(Kode_Perusahaan, No_Transaksi, Urut_detail, "
								SQL = SQL & "ID_Work_Center, Kode_Jenis_Biaya, Nilai, Total) values( "
								SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & x_ident_currentBiaya & "', "
								SQL = SQL & "'" & id_WC & "', '" & Jenis_biaya & "', '" & Nilai_WC & "', '" & Nilai_WC_Total & "') "
								ExecuteTrans(SQL)

							End If

						Next
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show("Biaya Belum di tambahkan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If

				End Using

				If Hpp_Work_Center_total <> 0 Then
					Hpp_Work_Center_Pcs = Val(HilangkanTanda(Format(Hpp_Work_Center_total / Val(HilangkanTanda(CurrentJmlhInput)), "N0")))

					SQL = "Update N_EMI_Transaksi_Trial_Production_Results_Detail_Biaya set "
					SQL = SQL & "Hpp_Per_Pcs ='" & Hpp_Work_Center_Pcs & "', HPP_Total='" & Hpp_Work_Center_total & "' "
					SQL = SQL & "where urut='" & x_ident_currentBiaya & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
					ExecuteTrans(SQL)

				End If

#Region "Komen"

				''''SCRAP
				'''SQL = "insert into N_Emi_Production_Results_Detail_Biaya(kode_perusahaan, No_Transaksi, Proses, "
				'''SQL = SQL & "Jenis, Jumlah_Dosing, Hpp_Per_Pcs, HPP_Total, Urut_HPP, Jenis_Barang, Jumlah_Hitung) values( "
				'''SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & proses & "', "
				'''SQL = SQL & "'BIAYA', '" & Val(HilangkanTanda(TxtJmlScrap.Text)) & "', '" & 0 & "', '" & 0 & "', NULL, 'SCP', '" & Val(HilangkanTanda(TxtJmlScrap.Text)) & "')"
				'''ExecuteTrans(SQL)

				'''Dim x_ident_currentBiayaSCP As Integer = 0
				'''SQL = "select IDENT_CURRENT('N_Emi_Production_Results_Detail_Biaya') as urutan"
				'''Using Dr = OpenTrans(SQL)
				'''    If Dr.Read Then
				'''        x_ident_currentBiayaSCP = Dr("urutan")
				'''    End If
				'''End Using

				'''SQL = "select kode_perusahaan from N_Emi_Production_Results_Detail_Biaya where "
				'''SQL = SQL & "kode_perusahaan='" & KodePerusahaan & "' and No_transaksi='" & TxtFormulator_NoFaktur.Text & "' and "
				'''SQL = SQL & "Proses='" & proses & "' and Jenis='BIAYA' and Urut='" & x_ident_currentBiayaSCP & "' and Jenis_Barang ='SCP' "
				'''Using dr = OpenTrans(SQL)
				'''    If Not dr.Read Then
				'''        dr.Close()
				'''        CloseTrans()
				'''        CloseConn()
				'''        MessageBox.Show("Terjadi kesalahan . . !!, Ulangi Transaksi .", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'''        Exit Sub
				'''    End If
				'''End Using

				'''SQL = ";with cte as ( "
				'''SQL = SQL & "Select a.kode_perusahaan, a.Id_Jenis_Biaya_Produksi, a.Kode_Jenis_Biaya_Produksi, "
				'''SQL = SQL & "isnull((select top(1) no_faktur from Emi_Transaksi_Work_Center x where x.status Is null "
				'''SQL = SQL & "And x.Kode_Perusahaan=a.Kode_Perusahaan And x.jenis_biaya=a.Kode_Jenis_Biaya_Produksi order by id desc),NULL) as Faktur_WC "
				'''SQL = SQL & "From Emi_Jenis_Biaya_Produksi a "
				'''SQL = SQL & ")select a.kode_jenis_biaya_produksi, c.id_work_center, max(c.Nilai_Per_pcs) as Nilai_Per_pcs "
				'''SQL = SQL & "From cte a, Emi_Transaksi_Work_Center b, Emi_Transaksi_Work_Center_detail c Where "
				'''SQL = SQL & "a.kode_perusahaan = b.Kode_Perusahaan And a.faktur_WC = b.No_Faktur And "
				'''SQL = SQL & "b.kode_perusahaan = c.Kode_Perusahaan And b.No_Faktur = c.No_Faktur And c.Id_Routing = '" & ID_Routing & "' "
				'''SQL = SQL & "group by a.kode_jenis_biaya_produksi, c.id_work_center "
				'''Using Ds5 = BindingTrans(SQL)
				'''    If Ds5.Tables("MyTable").Rows.Count <> 0 Then

				'''        For indxx = 0 To Ds5.Tables("MyTable").Rows.Count - 1

				'''            Dim id_WC As String = Ds5.Tables("MyTable").Rows(indxx).Item("id_work_center")
				'''            Dim Jenis_biaya As String = Ds5.Tables("MyTable").Rows(indxx).Item("kode_jenis_biaya_produksi")
				'''            Dim Nilai_WC As Double = Ds5.Tables("MyTable").Rows(indxx).Item("Nilai_Per_pcs")
				'''            Dim Nilai_WC_Total As Double = Math.Round(Ds5.Tables("MyTable").Rows(indxx).Item("Nilai_Per_pcs") * Val(HilangkanTanda(TxtJmlScrap.Text)), 0)

				'''            If Nilai_WC <> 0 Then

				'''                Hpp_Work_Center_totalSCP += Math.Round(Nilai_WC * Val(HilangkanTanda(TxtJmlScrap.Text)))

				'''                SQL = "insert into N_Emi_Production_Results_Detail_Biaya_WC(Kode_Perusahaan, No_Transaksi, Urut_detail, "
				'''                SQL = SQL & "ID_Work_Center, Kode_Jenis_Biaya, Nilai, Total) values( "
				'''                SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & x_ident_currentBiayaSCP & "', "
				'''                SQL = SQL & "'" & id_WC & "', '" & Jenis_biaya & "', '" & Nilai_WC & "', '" & Nilai_WC_Total & "') "
				'''                ExecuteTrans(SQL)

				'''            End If

				'''        Next

				'''    Else
				'''        CloseTrans()
				'''        CloseConn()
				'''        MessageBox.Show("Biaya Belum di tambahkan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'''        Exit Sub
				'''    End If

				'''End Using

				'''If Hpp_Work_Center_totalSCP <> 0 Then
				'''    Hpp_Work_Center_PcsSCP = Val(HilangkanTanda(Format(Hpp_Work_Center_totalSCP / Val(HilangkanTanda(TxtJmlScrap.Text)), "N0")))

				'''    SQL = "Update N_Emi_Production_Results_Detail_Biaya set "
				'''    SQL = SQL & "Hpp_Per_Pcs ='" & Hpp_Work_Center_PcsSCP & "', HPP_Total='" & Hpp_Work_Center_totalSCP & "' "
				'''    SQL = SQL & "where urut='" & x_ident_currentBiayaSCP & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
				'''    ExecuteTrans(SQL)

				'''End If

#End Region

#End Region

				'KALO ADA INPUT AJA DIA GENERATE
				'TODO : Barcode FG

#Region "Generate Barcode FG"

				If Val(HilangkanTanda(CurrentJmlhInput)) <> 0 Then

					Dim newBatch As String = ""

					SQL = "select top 1 b.Batch_Number "
					SQL = SQL & "from N_EMI_Transaksi_Trial_Production_Results a, N_EMI_Transaksi_Trial_Production_Results_Detail_Pallet b "
					SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
					SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
					SQL = SQL & "and a.Status is null "
					'SQL = SQL & "and b.Batch_Number is not null "
					SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
					SQL = SQL & "and a.No_Production_Order = '" & Txt_NoSplit.Text & "' "
					SQL = SQL & "order by b.proses DESC "
					Using Dr1 = OpenTrans(SQL)
						If Dr1.Read Then
							If Not General_Class.CekNULL(Dr1("Batch_Number")) = "" Then
								newBatch = Dr1("Batch_Number")
							Else
								newBatch = Generate_Batch_FG(tgl_skg, Prefix, DtpExpired.Value, Tahun_MulaiProduksi)
							End If
						Else
							newBatch = Generate_Batch_FG(tgl_skg, Prefix, DtpExpired.Value, Tahun_MulaiProduksi)
						End If
					End Using

					Dim newQrCode As String = Generate_QR_Batch(Txt_KdBarang.Text, newBatch)
					Dim Kode_Berjalan As String = Generate_Random_Kode(10)
					Dim Kode_Asal As String = Kode_Berjalan

					'=========================
					'=      INSERT DATA      =
					'=========================
					SQL = "insert into N_EMI_Transaksi_Trial_Produksi_Hasil_Perpallet (Kode_Perusahaan, No_Split, Lokasi, Tanggal, Jam, "
					SQL = SQL & "UserID, Kode_Stock_Owner, Kode_Barang, Jumlah, Satuan, Batch_Number, "
					SQL = SQL & "Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Jenis, Tgl_Expired, tgl_produksi) values "
					SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoSplit.Text & "', '" & Cmb_Lokasi.SelectedItem & "', "
					SQL = SQL & " '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "', "
					SQL = SQL & "'" & arrSO.Item(Cmb_LokasiSimpan.SelectedIndex) & "', '" & Txt_KdBarang.Text & "', '" & CurrentJmlhInput & "', "
					SQL = SQL & " '" & Satuan_Barang_Produksi & "', '" & newBatch & "', '" & newQrCode & "', '" & Kode_Berjalan & "', "
					SQL = SQL & " '" & Kode_Asal & "', '" & ArrDataJenis(CurrentIndexComboBoxJenisQuality).Kode_Warna & "', "
					SQL = SQL & " '" & Format(DtpExpired.Value, "yyyy-MM-dd") & "',  '" & Format(DtpProduksi.Value, "yyyy-MM-dd") & "') "
					ExecuteTrans(SQL)

					Dim MetodePengeluaranStok As String = ""
					SQL = "select Metode_Pengeluaran_Stok from Barang where kode_perusahaan  = '" & KodePerusahaan & "'  "
					SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text.Trim & "' and  Kode_Stock_Owner = '" & arrSO.Item(Cmb_LokasiSimpan.SelectedIndex) & "' "
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							MetodePengeluaranStok = Dr("metode_pengeluaran_stok")
						Else

							Dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("Metode Barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End Using

					'==================================
					'=      GENERATE NEW BARCODE      =
					'==================================

					Kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(random.Next(0, 10000), "00000")

					Dim fullNewQr As String = newQrCode & "-" & Kode_Berjalan

#Region "Generate Barcode Cara Baru"

					Using ImgBarcode2 As Image = Generate_QR_NoPadding(fullNewQr)
						Using ms2 As New MemoryStream()
							ImgBarcode2.Save(ms2, Imaging.ImageFormat.Jpeg)
							Dim rawData2 As Byte() = ms2.ToArray()

							Dim param2 As String = "@newBarcode" & Kode_unik_print
							Cmd.Parameters.Add(param2, SqlDbType.Image).Value = rawData2
						End Using
					End Using

					Dim barcode As String = "@newBarcode" & Kode_unik_print

#End Region

#Region "Generate Barcode Cara Lama"

					'barcode.Image = Generate_QR_NoPadding(fullNewQr)

					'Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_print & ".jpg")

					''   Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeFinishGood.jpg")

					''If Not (System.IO.File.Exists(FileToSaveAs1)) Then
					'Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
					''End If

					'fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
					'FileSize1 = fs1.Length
					'rawData1 = New Byte(FileSize1) {}
					'fs1.Read(rawData1, 0, FileSize1)
					'fs1.Close()
					'Cmd.Parameters.Add("@newBarcode", SqlDbType.Image).Value = rawData1

#End Region

					'========================
					'=     GET TOTAL GR     =
					'========================
					Dim Total_GR1_Cetak As Double = 1
					SQL = "select distinct top(1) Nomor from N_EMI_Transaksi_Trial_Production_Results_Detail_Pallet where "
					SQL = SQL & "no_transaksi = '" & TxtFormulator_NoFaktur.Text & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
					SQL = SQL & "order by Nomor Desc "
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							If General_Class.CekNULL(Dr("Nomor")) = "" Then
								Total_GR1_Cetak = 1
							Else
								Total_GR1_Cetak = Dr("Nomor") + 1
							End If
						Else
							Total_GR1_Cetak = 1
						End If
					End Using

					'=======================
					'=     GET ROUTING     =
					'=======================
					'Dim Id_Routing As String = ""
					Dim Routing As String = ""
					SQL = "select b.Id_Routing, c.Keterangan as Routing "
					SQL = SQL & "from N_EMI_Transaksi_Trial_Split_Production_Order a, N_EMI_Transaksi_Trial_Order_Produksi b, EMI_Master_Routing c "
					SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
					SQL = SQL & "and a.No_PO = b.No_Faktur "
					SQL = SQL & "and b.Id_Routing = c.Id_Routing "
					SQL = SQL & "and a.Status is null and b.Status is null "
					SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
					SQL = SQL & "and a.No_Transaksi = '" & Txt_NoSplit.Text & "' "
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							'Id_Routing = Dr("Id_Routing")
							Routing = Dr("Routing")
						Else
							Dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("Routing tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End Using

					SQL = "insert into N_EMI_Transaksi_Trial_Barcode_Label_Barcode_GR_1 (kode_perusahaan, no_split, Barcode, Kode_barang, Nama_Barang, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, "
					SQL = SQL & "Proses, Tahap, Jumlah, Satuan, Troli, Nomor, id_routing, routing, Kode_unik_print)  "
					SQL = SQL & "values ('" & KodePerusahaan & "', '" & Txt_NoSplit.Text & "', " & barcode & ", '" & Txt_KdBarang.Text & "', '" & Txt_NamaBarang.Text & "', '" & fullNewQr & "', '" & newQrCode & "', "
					SQL = SQL & "'" & Format(DtpProduksi.Value, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & proses & "', '" & CurrentBatch & "', '" & CurrentJmlhInput & "', '" & Satuan_Barang_Produksi & "', "
					SQL = SQL & "'" & CurrentTrolly & "', '" & Total_GR1_Cetak & "', '" & ID_Routing & "', '" & Routing & "', '" & Kode_unik_print & "') "
					ExecuteTrans(SQL)

					arr_kode_unik_print.Add(Kode_unik_print)

				End If

#End Region

				''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
				'''=================================================================================================================='''

				'''======================================== KODINGAN DARI PENERIMAAN BARANG ========================================='''

				'''=================================================================================================================='''
				''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

				SQL = "insert into N_EMI_Transaksi_Trial_Production_Results_Detail_Barang(kode_perusahaan,no_transaksi,proses,tanggal,jam,userid,kode_Stock_owner,"
				SQL = SQL & "kode_barang, qty_hasil_produksi, qty_good_stock, qty_bad_stock, satuan, qty_scrap, satuan_scrap, Kode_Barang_Scrap) values("
				SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & proses & "', "
				SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "', "
				SQL = SQL & "'" & SoProduction & "', '" & Txt_KdBarang.Text & "','" & CurrentJmlhInput & "', '" & goodstock & "', "
				SQL = SQL & "'" & badstock & "', '" & Satuan_Barang_Produksi & "', '" & TxtJmlScrap.Text & "', '" & CmbSatScrap.Text & "' ," & kd_barang_scrap & ") "
				ExecuteTrans(SQL)

#Region "INSERT PRODUKSI"

				'========================
				'=     GET TOTAL GR     =
				'========================
				Dim Total_GR1 As Double = 1

				SQL = "select distinct top(1) Nomor from N_EMI_Transaksi_Trial_Production_Results_Detail_Pallet where "
				SQL = SQL & "no_transaksi = '" & TxtFormulator_NoFaktur.Text & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
				SQL = SQL & "order by Nomor Desc "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						If General_Class.CekNULL(Dr("Nomor")) = "" Then
							Total_GR1 = 1
						Else
							Total_GR1 = Dr("Nomor") + 1
						End If
					Else
						Total_GR1 = 1
					End If
				End Using

				'==============================
				'=     INSERT KE PRODUKSI     =
				'==============================

				''GET DATA DI N_EMI_Transaksi_Trial_Produksi_Hasil_Perpallet
				SQL = "select a.No_Split, a.Kode_Unik_Berjalan, a.Kode_Unik_Asal, a.Qr_Code, a.Jumlah, a.Satuan, a.Batch_Number, a.Kode_Barang, a.ID as urut_oto, a.jenis, a.Serial_Number, Tanggal, Tgl_Expired, tgl_produksi "
				SQL = SQL & "from N_EMI_Transaksi_Trial_Produksi_Hasil_Perpallet a "
				SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and a.No_Split='" & Txt_NoSplit.Text & "' and flag_simpan_pallet is null "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For i As Integer = 0 To .Rows.Count - 1

								Dim Warna_stock As String = ""

								Warna_stock = .Rows(i).Item("Jenis")

								'====================================
								'=       CONVERT SATUAN KECIL       =
								'====================================
								Dim nilai_kecildetail As Double = Val(HilangkanTanda(Format(Ubah_Angka_Kecil(.Rows(i).Item("Kode_Barang"), .Rows(i).Item("Satuan"), TxtSatuanKecil.Text, .Rows(i).Item("Jumlah")), "N4")))

								''GET ID_WAREHOUSE YG KOSONG
								Dim available_Id_Warehouse As String = ""
								Dim available_NoPallet As String = ""

								SQL = "select top(1) a.id_wms_warehouse_position, 0 as nomor_urut from "
								SQL = SQL & "view_warehouse_position a "
								SQL = SQL & "where a.kode_Perusahaan ='" & KodePerusahaan & "' "
								SQL = SQL & "and a.Kode_Stock_Owner='" & arrSO(Cmb_LokasiSimpan.SelectedIndex) & "' "
								Using Dr2 = OpenTrans(SQL)
									Do While Dr2.Read
										available_Id_Warehouse = Dr2("id_wms_warehouse_position")
										available_NoPallet = Dr2("nomor_urut")
									Loop
								End Using

								SQL = ";with cte as ( "
								SQL = SQL & "Select a.kode_perusahaan, a.Id_Jenis_Biaya_Produksi, a.Kode_Jenis_Biaya_Produksi, "
								SQL = SQL & "isnull((select top(1) no_faktur from Emi_Transaksi_Work_Center x where x.status Is null "
								SQL = SQL & "And x.Kode_Perusahaan=a.Kode_Perusahaan And x.jenis_biaya=a.Kode_Jenis_Biaya_Produksi order by id desc),NULL) as Faktur_WC "
								SQL = SQL & "From Emi_Jenis_Biaya_Produksi a "
								SQL = SQL & ")select a.kode_jenis_biaya_produksi, c.id_work_center, max(c.Nilai_Per_pcs) as Nilai_Per_pcs "
								SQL = SQL & "From cte a, Emi_Transaksi_Work_Center b, Emi_Transaksi_Work_Center_detail c Where "
								SQL = SQL & "a.kode_perusahaan = b.Kode_Perusahaan And a.faktur_WC = b.No_Faktur And "
								SQL = SQL & "b.kode_perusahaan = c.Kode_Perusahaan And b.No_Faktur = c.No_Faktur And c.Id_Routing = '" & ID_Routing & "' "
								SQL = SQL & "group by a.kode_jenis_biaya_produksi, c.id_work_center "
								Using Dss = BindingTrans(SQL)
									If Dss.Tables("MyTable").Rows.Count = 0 Then
										CloseTrans()
										CloseConn()
										MessageBox.Show("Biaya Belum di tambahkan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If

								End Using

								'========================
								'=     GET URUT HPP     =
								'========================
								Dim Urut_HPP As String = ""
								Dim Jumlah_Dosing_By_HPP As Double = 0
								SQL = "select Urut, Jumlah_Dosing "
								SQL &= $"from N_EMI_Transaksi_Trial_Production_Results_HPP "
								SQL &= $"where No_Transaksi = '{TxtFormulator_NoFaktur.Text.Trim}' "
								SQL &= $"and Proses = '{CurrentBatch}' "
								Using Dr = OpenTrans(SQL)
									If Dr.Read Then
										Urut_HPP = Dr("Urut").ToString.Trim
										Jumlah_Dosing_By_HPP = Val(HilangkanTanda(Dr("Jumlah_Dosing")))
									End If
								End Using

								'================================
								'=     INSERT DETAIL PALLET     =
								'================================

#Region "Kode Baru"

								SQL = "insert into N_EMI_Transaksi_Trial_Production_Results_Detail_Pallet (Kode_Perusahaan, No_Transaksi, Kode_Unik_Berjalan, Kode_Unik_Asal, Qr_Code, Jumlah, Satuan, NIlai_Barang, "
								SQL = SQL & "Satuan_Barang, Batch_Number, Id_Warehouse, Nomor_Pallet, proses, serial_number, Jenis, Tgl_Produksi, Tgl_Expired, Urut_HPP, Tahap, Troli, Nomor) values "
								SQL = SQL & "('" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & .Rows(i).Item("Kode_Unik_Berjalan") & "', '" & .Rows(i).Item("Kode_Unik_Asal") & "', "
								SQL = SQL & "'" & .Rows(i).Item("Qr_Code") & "', '" & nilai_kecildetail & "', '" & .Rows(i).Item("Satuan") & "', '" & nilai_kecildetail & "', '" & TxtSatuanKecil.Text & "', "
								SQL = SQL & "'" & .Rows(i).Item("Batch_Number") & "', '" & available_Id_Warehouse & "', '" & available_NoPallet & "', '" & proses & "', '', '" & .Rows(i).Item("Jenis") & "', "
								SQL = SQL & " '" & .Rows(i).Item("tgl_produksi") & "','" & .Rows(i).Item("Tgl_Expired") & "', '" & Urut_HPP & "', '" & CurrentBatch & "', '" & CurrentTrolly & "', '" & Total_GR1 & "') "
								ExecuteTrans(SQL)

								Dim Jenis_Berat As String = ""
								SQL = "Select isnull(flag_tampil_berat,'T') as flag_tampil_berat from emi_satuan where "
								SQL = SQL & "satuan='" & TxtSatuanKecil.Text & "' and kode_perusahaan='" & KodePerusahaan & "' "
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

								Dim nilai_sisa_Pcs As Double = nilai_kecildetail
								'If Jenis_Berat = "Y" Then
								'    nilai_sisa_Pcs = Math.Round(Ubah_Angka_Kecil(.Rows(i).Item("Kode_Barang"), "KG", TxtSatuanKecil.Text, nilai_kecildetail), 4)
								'Else
								'    nilai_sisa_Pcs = Math.Floor(Ubah_Angka_Kecil(.Rows(i).Item("Kode_Barang"), "KG", TxtSatuanKecil.Text, nilai_kecildetail))
								'End If

								Dim Hpp_Bahan_baku_Total As Double = 0
								SQL = "Select b.Total_Bahan_Baku As Total, b.satuan as satuan_barang  "
								SQL = SQL & "from N_EMI_Transaksi_Trial_Production_Results a, N_EMI_Transaksi_Trial_Production_Results_HPP b where "
								SQL = SQL & "a.kode_perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi And a.status Is null And "
								SQL = SQL & "a.Kode_Perusahaan='" & KodePerusahaan & "' and a.No_Transaksi='" & TxtFormulator_NoFaktur.Text & "' and b.Urut='" & Urut_HPP & "' "
								'SQL = SQL & "group by satuan "
								Using dr = OpenTrans(SQL)
									If dr.Read Then
										Hpp_Bahan_baku_Total = Val(HilangkanTanda(Format(dr("Total"), "N0")))
									End If
								End Using

								Dim nilai_potong = nilai_sisa_Pcs
								Dim nilai_pakai As Double = Ubah_Angka_Kecil(.Rows(i).Item("Kode_Barang"), TxtSatuanKecil.Text, "KG", nilai_potong)
								Dim Nilai_Bahan_Baku As Double = Math.Floor((Hpp_Bahan_baku_Total / Jumlah_Dosing_By_HPP * nilai_pakai))
								Dim Nilai_Bahan_Baku_Pcs As Double = Val(HilangkanTanda(Format(Nilai_Bahan_Baku / nilai_potong, "N0")))

								SQL = "insert into N_EMI_Transaksi_Trial_Production_Results_Detail_Biaya(kode_perusahaan, No_Transaksi, Proses, "
								SQL = SQL & "Jenis, Jumlah_Dosing, Hpp_Per_Pcs, HPP_Total, Urut_HPP, Jenis_Barang, Jumlah_Hitung) values( "
								SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & proses & "', "
								SQL = SQL & "'BAHAN', '" & Nilai_berat_FG & "', '" & Nilai_Bahan_Baku_Pcs & "', '" & Nilai_Bahan_Baku & "', '" & Urut_HPP & "', 'FG', '" & nilai_pakai & "')"
								ExecuteTrans(SQL)

								SQL = "Update N_EMI_Transaksi_Trial_Production_Results_HPP set "
								SQL = SQL & "jumlah_Terpakai = jumlah_Terpakai + " & nilai_pakai & " "
								SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
								SQL = SQL & "Urut = '" & Urut_HPP & "' "
								ExecuteTrans(SQL)

#End Region

#Region "Kode Lama"

								''Cek HPP Per Dosing
								'Dim sisa_barang As Double = nilai_kecildetail
								'SQL = "Select b.Proses, a.No_Production_Order, b.jumlah_dosing, b.jumlah_dosing_pcs, b.jumlah_dosing - b.jumlah_Terpakai As sisa, Total_bahan_baku, "
								'SQL = SQL & "total_biaya_produksi, nilai_loss_production, total_packaging, b.urut "
								'SQL = SQL & "From N_EMI_Transaksi_Trial_Production_Results a, N_EMI_Transaksi_Trial_Production_Results_HPP b Where "
								'SQL = SQL & "a.kode_Perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi And a.status Is null "
								'SQL = SQL & "And round(jumlah_dosing,4)<>round(jumlah_terpakai,4) "
								'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
								'SQL = SQL & "and a.No_Transaksi = '" & TxtFormulator_NoFaktur.Text & "' "
								'SQL = SQL & "and b.Tanggal is not null "
								'SQL = SQL & "order by proses "
								'Using dss = BindingTrans(SQL)
								'    For ind = 0 To dss.Tables("MyTable").Rows.Count - 1

								'        If sisa_barang = 0 Then
								'            Exit For
								'        End If

								'        Dim urut_HPP As String = dss.Tables("MyTable").Rows(ind).Item("urut")
								'        Dim sisa As Double = Val(HilangkanTanda(Format(dss.Tables("MyTable").Rows(ind).Item("sisa"), "N4")))

								'        Dim jumlah_dosing As Double = Val(HilangkanTanda(Format(dss.Tables("MyTable").Rows(ind).Item("jumlah_dosing"), "N4")))
								'        Dim jumlah_dosing_pcs As Double = dss.Tables("MyTable").Rows(ind).Item("jumlah_dosing_pcs")

								'        Dim Proses_Result_Hpp As Integer = proses
								'        Dim NoSplit As String = dss.Tables("MyTable").Rows(ind).Item("No_Production_Order")

								'        'Ubah Nilai Dosing(Nilai Dosing Dalam KG), ke Nilai Pcs

								'        Dim Jenis_Berat As String = ""
								'        SQL = "Select isnull(flag_tampil_berat,'T') as flag_tampil_berat from emi_satuan where "
								'        SQL = SQL & "satuan='" & TxtSatuanKecil.Text & "' and kode_perusahaan='" & KodePerusahaan & "' "
								'        Using dr = OpenTrans(SQL)
								'            If dr.Read Then
								'                Jenis_Berat = dr("flag_tampil_berat")
								'            Else
								'                dr.Close()
								'                CloseTrans()
								'                CloseConn()
								'                MessageBox.Show("data Satuan Tidak ada . . ! ! ")
								'                Exit Sub
								'            End If
								'        End Using

								'        Dim nilai_sisa_Pcs As Double = 0

								'        If Jenis_Berat = "Y" Then
								'            nilai_sisa_Pcs = Math.Round(Ubah_Angka_Kecil(.Rows(i).Item("Kode_Barang"), "KG", TxtSatuanKecil.Text, sisa), 4)
								'        Else
								'            nilai_sisa_Pcs = Math.Floor(Ubah_Angka_Kecil(.Rows(i).Item("Kode_Barang"), "KG", TxtSatuanKecil.Text, sisa))
								'        End If

								'        Dim nilai_potong As Double = 0
								'        If nilai_sisa_Pcs > sisa_barang Then

								'            nilai_potong = sisa_barang
								'            sisa_barang -= sisa_barang
								'        Else

								'            nilai_potong = nilai_sisa_Pcs
								'            sisa_barang -= nilai_sisa_Pcs

								'        End If

								'        'in untuk ubah Pcs ke KG lagi
								'        Dim nilai_pakai As Double = Ubah_Angka_Kecil(.Rows(i).Item("Kode_Barang"), TxtSatuanKecil.Text, "KG", nilai_potong)

								'        SQL = "insert into N_EMI_Transaksi_Trial_Production_Results_Detail_Pallet (Kode_Perusahaan, No_Transaksi, Kode_Unik_Berjalan, Kode_Unik_Asal, Qr_Code, Jumlah, Satuan, NIlai_Barang, "
								'        SQL = SQL & "Satuan_Barang, Batch_Number, Id_Warehouse, Nomor_Pallet, proses, serial_number, Jenis, Tgl_Produksi, Tgl_Expired, Urut_HPP, Tahap, Troli, Nomor) values "
								'        SQL = SQL & "('" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & .Rows(i).Item("Kode_Unik_Berjalan") & "', '" & .Rows(i).Item("Kode_Unik_Asal") & "', "
								'        SQL = SQL & "'" & .Rows(i).Item("Qr_Code") & "', '" & nilai_potong & "', '" & .Rows(i).Item("Satuan") & "', '" & nilai_potong & "', '" & TxtSatuanKecil.Text & "', "
								'        SQL = SQL & "'" & .Rows(i).Item("Batch_Number") & "', '" & available_Id_Warehouse & "', '" & available_NoPallet & "', '" & proses & "', '', '" & .Rows(i).Item("Jenis") & "', "
								'        SQL = SQL & " '" & .Rows(i).Item("tgl_produksi") & "','" & .Rows(i).Item("Tgl_Expired") & "', '" & urut_HPP & "', '" & CurrentBatch & "', '" & CurrentTrolly & "', '" & Total_GR1 & "') "
								'        ExecuteTrans(SQL)

								'        Dim x_ident_currentPallet As Integer = 0
								'        SQL = "select IDENT_CURRENT('N_EMI_Transaksi_Trial_Production_Results_Detail_Pallet') as urutan"
								'        Using Dr = OpenTrans(SQL)
								'            If Dr.Read Then
								'                x_ident_currentPallet = Dr("urutan")
								'            End If
								'        End Using

								'        Dim Kode_barang_inq As String = ""
								'        SQL = "select top(1) Kode_Barang_inq from barang where "
								'        SQL = SQL & "kode_perusahaan='" & KodePerusahaan & "' and kode_barang='" & Txt_KdBarang.Text & "' "
								'        Using Dr = OpenTrans(SQL)
								'            If Dr.Read Then
								'                Kode_barang_inq = Dr("Kode_Barang_inq")
								'            End If
								'        End Using

								'        Dim flag_FG As String = ""
								'        SQL = "select isnull(flag_finished_good,'T') as flag_finished_good "
								'        SQL = SQL & "from emi_group_jenis a, barang b where a.kode_perusahaan = '" & KodePerusahaan & "' "
								'        SQL = SQL & "and a.id_group_jenis = b.id_group_jenis and b.kode_barang='" & Txt_KdBarang.Text & "' "
								'        Using Dr = OpenTrans(SQL)
								'            If Dr.Read Then
								'                flag_FG = Dr("flag_finished_good")
								'            Else
								'                Dr.Close()
								'                CloseTrans()
								'                CloseConn()
								'                MessageBox.Show("Barang detail satuan tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                Exit Sub
								'            End If
								'        End Using

								'        '======================================================= RAGU ==============================================================

								'        Dim selesai As Boolean = True

								'        Dim Persen_loss_production As Double = 0
								'        Dim Nilai_loss_production As Double = 0
								'        Dim Nilai_loss_production_Pcs As Double = 0

								'        Dim Nilai_Bahan_Baku As Double = 0
								'        Dim Nilai_Bahan_Baku_Pcs As Double = 0

								'        Dim satuan_bahan As String = ""

								'        Dim Hpp_Bahan_baku_Total As Double = 0
								'        SQL = "Select b.Total_Bahan_Baku As Total, b.satuan as satuan_barang  "
								'        SQL = SQL & "from N_EMI_Transaksi_Trial_Production_Results a, N_EMI_Transaksi_Trial_Production_Results_HPP b where "
								'        SQL = SQL & "a.kode_perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi And a.status Is null And "
								'        SQL = SQL & "a.Kode_Perusahaan='" & KodePerusahaan & "' and a.No_Transaksi='" & TxtFormulator_NoFaktur.Text & "' and b.Urut='" & urut_HPP & "' "
								'        'SQL = SQL & "group by satuan "
								'        Using dr = OpenTrans(SQL)
								'            If dr.Read Then
								'                satuan_bahan = dr("satuan_barang")
								'                Hpp_Bahan_baku_Total = Val(HilangkanTanda(Format(dr("Total"), "N0")))
								'            End If
								'        End Using

								'        SQL = "select Nilai_Persen from "
								'        SQL = SQL & "Emi_Budgeting_Loss_Production where "
								'        SQL = SQL & "Kode_Perusahaan='" & KodePerusahaan & "' "
								'        SQL = SQL & "order by Urut desc "
								'        Using dr = OpenTrans(SQL)
								'            If dr.Read Then
								'                Persen_loss_production = dr("Nilai_Persen")

								'            End If
								'        End Using

								'        Nilai_loss_production = Math.Round((Math.Round(Hpp_Bahan_baku_Total * Persen_loss_production / 100, 0) / jumlah_dosing * nilai_pakai), 0)

								'        Nilai_loss_production_Total += Nilai_loss_production

								'        Nilai_loss_production_Pcs = Val(HilangkanTanda(Format(Nilai_loss_production / nilai_potong, "N0")))

								'        SQL = "insert into N_EMI_Transaksi_Trial_Production_Results_Detail_Biaya(kode_perusahaan, No_Transaksi, Proses, "
								'        SQL = SQL & "Jenis, Jumlah_Dosing, Hpp_Per_Pcs, HPP_Total, Urut_HPP, Jenis_Barang, Jumlah_Hitung) values( "
								'        SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & proses & "', "
								'        SQL = SQL & "'LOSS', '" & Nilai_berat_FG & "', '" & Nilai_loss_production_Pcs & "', '" & Nilai_loss_production & "', '" & urut_HPP & "', 'FG', '" & nilai_pakai & "')"
								'        ExecuteTrans(SQL)

								'        Nilai_Bahan_Baku = Math.Floor((Hpp_Bahan_baku_Total / jumlah_dosing * nilai_pakai))

								'        Nilai_Bahan_Baku_Total += Nilai_Bahan_Baku

								'        Nilai_Bahan_Baku_Pcs = Val(HilangkanTanda(Format(Nilai_Bahan_Baku / nilai_potong, "N0")))

								'        SQL = "insert into N_EMI_Transaksi_Trial_Production_Results_Detail_Biaya(kode_perusahaan, No_Transaksi, Proses, "
								'        SQL = SQL & "Jenis, Jumlah_Dosing, Hpp_Per_Pcs, HPP_Total, Urut_HPP, Jenis_Barang, Jumlah_Hitung) values( "
								'        SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & proses & "', "
								'        SQL = SQL & "'BAHAN', '" & Nilai_berat_FG & "', '" & Nilai_Bahan_Baku_Pcs & "', '" & Nilai_Bahan_Baku & "', '" & urut_HPP & "', 'FG', '" & nilai_pakai & "')"
								'        ExecuteTrans(SQL)

								'        Dim nilai_HPP_pcs As Double = Val(HilangkanTanda(Format(Nilai_Bahan_Baku_Pcs + Hpp_Work_Center_Pcs + Nilai_loss_production_Pcs + nilai_packaging_Pcs, "N0")))

								'        TotalHPP += (nilai_HPP_pcs * nilai_potong)

								'        Dim Rand As New Random
								'        Dim str As String = Format(Rand.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
								'        Dim Kode_Unik As String = str.Substring(0, 5) & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
								'        Dim SN As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & nilai_HPP_pcs & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

								'        Dim blok_sn As String = "NULL"
								'        Dim jumlah_bags As Double = 0
								'        If flag_FG = "Y" Then
								'            blok_sn = "'Y'"
								'            jumlah_bags = nilai_potong
								'        End If

								'        SQL = "Update barang set "
								'        SQL = SQL & "Good_Stock = Good_Stock + " & nilai_potong & ", Jumlah_Bags=Jumlah_Bags+" & jumlah_bags & " "
								'        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
								'        SQL = SQL & "kode_stock_owner = '" & arrSO(Cmb_LokasiSimpan.SelectedIndex) & "' and kode_barang = '" & Kd_Brg & "'"
								'        'ExecuteTrans(SQL)

								'        'Cek Apakah Sn baru sama dengan Sn pada Barang Split
								'        'SQL = "select kode_barang from barang_sn where "
								'        'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
								'        'SQL = SQL & "kode_stock_owner = '" & arrSO(Cmb_LokasiSimpan.SelectedIndex) & "' and "
								'        'SQL = SQL & "kode_barang = '" & Kd_Brg & "' and serial_number = '" & SN & "'"
								'        'Using Dr = OpenTrans(SQL)
								'        '    If Dr.Read Then
								'        '        Dr.Close()
								'        '        CloseTrans()
								'        '        CloseConn()
								'        '        MessageBox.Show("Terjadi kesalahan pada Barang . . !, Silahkan Ulangi Transaksi . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'        '        Exit Sub
								'        '    Else

								'        '        'Insert Barang SN
								'        '        SQL = "insert into barang_sn(kode_perusahaan, kode_stock_owner, kode_barang, "
								'        '        SQL = SQL & "serial_number, Jumlah, Jumlah_Bags, Warna, Kode_Unik_Berjalan, Kode_Unik_Asal, "
								'        '        SQL = SQL & "Qr_Code, Batch_Number, Id_Warehouse, Nomor_Pallet, Tgl_Produksi, Tgl_Expired, tgl_masuk, blok_sn) values('" & KodePerusahaan & "', "
								'        '        SQL = SQL & "'" & arrSO(Cmb_LokasiSimpan.SelectedIndex) & "', '" & Kd_Brg & "', "
								'        '        SQL = SQL & "'" & SN & "', " & nilai_potong & ", " & jumlah_bags & ", '" & Warna_stock & "', "
								'        '        SQL = SQL & "'" & .Rows(i).Item("Kode_Unik_Berjalan") & "', '" & .Rows(i).Item("Kode_Unik_Asal") & "', '" & .Rows(i).Item("Qr_Code") & "', "
								'        '        SQL = SQL & "'" & .Rows(i).Item("Batch_Number") & "', '" & available_Id_Warehouse & "', '" & available_NoPallet & "', "
								'        '        SQL = SQL & "'" & .Rows(i).Item("tgl_produksi") & "','" & .Rows(i).Item("Tgl_Expired") & "', '" & .Rows(i).Item("Tanggal") & "', " & blok_sn & ") "
								'        '        Dr.Close()
								'        '        ExecuteTrans(SQL)
								'        '    End If
								'        'End Using

								'        'SQL = "Update N_EMI_Transaksi_Trial_Production_Results_Detail_Pallet set "
								'        'SQL = SQL & "serial_number = '" & SN & "', SN_Baru ='" & SN & "', Lokasi_Gudang='" & arrSO(Cmb_LokasiSimpan.SelectedIndex) & "' "
								'        'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
								'        'SQL = SQL & "Urut_oto = '" & x_ident_currentPallet & "' "
								'        'ExecuteTrans(SQL)

								'        'Nilai yg di pakai, di kembalikan ke satuan dosing(KG)

								'        SQL = "Update N_EMI_Transaksi_Trial_Production_Results_HPP set "
								'        SQL = SQL & "jumlah_Terpakai = jumlah_Terpakai + " & nilai_pakai & " "
								'        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
								'        SQL = SQL & "Urut = '" & urut_HPP & "' "
								'        ExecuteTrans(SQL)

								'    Next
								'End Using

								'If sisa_barang <> 0 Then
								'    CloseTrans()
								'    CloseConn()
								'    MessageBox.Show("Terjadi Kesalahan ! !, Jumlah Input Melebihi Jumlah Produksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'    Exit Sub
								'End If

#End Region

								TotalTf = TotalTf + nilai_kecildetail

							Next

							'====================================
							'=     CEK APAKAH JUMLAH SESUAI     =
							'====================================
							'SQL = "SELECT "
							'SQL = SQL & "ROUND(SUM(good_stock), 4) AS good_stock, "
							'SQL = SQL & "ISNULL((SELECT ROUND(SUM(jumlah), 4) FROM Barang_sn x WHERE a.kode_Barang = x.kode_Barang AND a.Kode_Stock_Owner = x.Kode_Stock_Owner AND a.kode_Perusahaan = x.kode_Perusahaan), 0) AS Jumlah_sn, "
							'SQL = SQL & "ISNULL(ROUND(SUM(jumlah_bags), 4), 0) AS jumlah_bags_barang, "
							'SQL = SQL & "ISNULL((SELECT ROUND(SUM(Jumlah_Bags), 4) FROM Barang_sn y WHERE a.kode_Barang = y.kode_Barang AND a.Kode_Stock_Owner = y.Kode_Stock_Owner AND a.kode_Perusahaan = y.Kode_Perusahaan), 0) AS jumlah_bags_sn "
							'SQL = SQL & "FROM "
							'SQL = SQL & "barang a "
							'SQL = SQL & "WHERE "
							'SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' And a.Kode_Stock_Owner = '" & Kd_So & "' AND a.Kode_Barang = '" & Kd_Brg & "' "
							'SQL = SQL & "GROUP BY "
							'SQL = SQL & "a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan"
							'Using Ds2 = BindingTrans(SQL)
							'    If Ds2.Tables("MyTable").Rows.Count <> 0 Then

							'        Dim Stock_Barang As String = Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(0).Item("good_stock"), "N4")))
							'        Dim Stock_Sn As String = Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(0).Item("Jumlah_sn"), "N4")))
							'        Dim Bags_Barang As String = Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_barang"), "N4")))
							'        Dim Bags_Sn As String = Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_sn"), "N4")))

							'        If Stock_Barang <> Stock_Sn Or Bags_Barang <> Bags_Sn Then
							'            CloseTrans()
							'            CloseConn()
							'            MessageBox.Show("Terjadi Kesalahan Pada SN . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'            Exit Sub
							'        End If
							'    Else
							'        CloseTrans()
							'        CloseConn()
							'        MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'        Exit Sub

							'    End If

							'End Using

						End If
					End With
				End Using

#End Region

				SQL = "update N_EMI_Transaksi_Trial_Produksi_Hasil_Perpallet set flag_simpan_pallet = 'Y'  where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and No_Split = '" & Txt_NoSplit.Text & "' "
				ExecuteTrans(SQL)

				Dim inisial_faktur_dari As String = ""
				Dim fso As String = ""
				SQL = "Select b.Inisial_Faktur,a.Kode_Stock_Owner from N_EMI_Transaksi_Trial_Split_Production_Order a,Stock_Owner_Gudang b "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Stock_Owner = b.Kode_Stock_Owner "
				SQL = SQL & "And a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & Txt_NoSplit.Text & "' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						inisial_faktur_dari = Dr("inisial_faktur")
						fso = Dr("Kode_Stock_Owner")
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				Dim akun_Selisih_pembulatan As String = ""
				SQL = "select Hutang_Supplier, Hutang_Perjalanan, Hutang_PPN, PPN_Pembelian, Selisih_Pembulatan "
				SQL = SQL & "from stock_owner "
				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Lokasi & "' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then

						akun_Selisih_pembulatan = Dr("Selisih_Pembulatan")
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				Dim fFlag_Semi_FG As String = ""
				Dim fFlag_Finished_Good As String = ""

				SQL = "select b.Kode_Barang,d.Flag_Semi_FG,d.Flag_Finished_Good "
				SQL = SQL & "from N_EMI_Transaksi_Trial_Production_Results a,N_EMI_Transaksi_Trial_Split_Production_Order b,Barang c, EMI_Group_Jenis d "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Production_Order = b.No_Transaksi "
				SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
				SQL = SQL & "and b.Kode_Barang = c.Kode_Barang and c.Kode_Perusahaan = d.Kode_Perusahaan and a.status is null "
				SQL = SQL & "and c.Id_Group_Jenis = d.Id_Group_Jenis and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and a.No_Transaksi = '" & TxtFormulator_NoFaktur.Text & "' "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For h As Integer = 0 To .Rows.Count - 1
								fFlag_Semi_FG = .Rows(h).Item("Flag_Semi_FG")
								fFlag_Finished_Good = .Rows(h).Item("Flag_Finished_Good")
							Next
						Else
							CloseTrans()
							CloseConn()
							MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End With
				End Using

				'==================
				'=     JURNAL     =
				'==================

#Region "Bagian Jurnal"

				'                Dim Akun_Persediaan_Dalam_Proses As String = ""

				'                Dim Akun_PersediaanScrap As String = ""
				'                Dim akun_HPP_FG As String = ""
				'                Dim Akun_HPPScrap As String = ""
				'                Dim Akun_Pembulatan_FG As String = ""

				'                Dim keterangan2 As String = ""
				'                Dim keterangan3 As String = ""

				'                SQL = "select HPP_Barang_Setengah_Jadi, Persediaan_Barang_Dalam_Proses, "
				'                SQL = SQL & "HPP,Pembulatan_Finished_Good,Pembulatan_Semi_FG, Persediaan_Scrap from stock_owner_gudang "
				'                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & fso & "' "
				'                Using Dr = OpenTrans(SQL)
				'                    If Dr.Read Then

				'                        Akun_PersediaanScrap = Dr("Persediaan_Scrap")
				'                        Akun_HPPScrap = Dr("Persediaan_Scrap")

				'                        Akun_Persediaan_Dalam_Proses = Dr("Persediaan_Barang_Dalam_Proses")

				'                        'If fFlag_Semi_FG = "Y" Then

				'                        akun_HPP_FG = Dr("HPP_Barang_Setengah_Jadi")
				'                        keterangan2 = "HPP Barang Setengah Jadi "

				'                        Akun_Pembulatan_FG = Dr("Pembulatan_Semi_FG")
				'                        keterangan3 = "Pembulatan HPP Barang Setengah Jadi "
				'                        'ElseIf fFlag_Finished_Good = "Y" Then

				'                        '    akun_HPP_FG = Dr("HPP")
				'                        '    keterangan2 = "HPP Barang Jadi "

				'                        '    Akun_Pembulatan_FG = Dr("Pembulatan_Finished_Good")
				'                        '    keterangan3 = "Pembulatan HPP Barang Jadi "
				'                        'End If
				'                    Else
				'                        Dr.Close()
				'                        CloseTrans()
				'                        CloseConn()
				'                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                        Exit Sub
				'                    End If
				'                End Using

				'                Dim akun_Loss_production As String = ""

				'                Dim ket_loss_production As String = ""

				'                'awal persediaan barang dalam proses
				'                SQL = "select Persediaan_Barang_Dalam_Proses, Penyusutan_Barang_Dalam_Proses from stock_owner_gudang "
				'                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & fso & "' "
				'                Using Dr = OpenTrans(SQL)
				'                    If Dr.Read Then

				'                        akun_Loss_production = Dr("Penyusutan_Barang_Dalam_Proses")
				'                        ket_loss_production = "Budget Loss "
				'                    Else
				'                        Dr.Close()
				'                        CloseTrans()
				'                        CloseConn()
				'                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                        Exit Sub
				'                    End If
				'                End Using

				'                If TotalHPP <> 0 Then

				'                    Dim Akun_Persediaan As String = ""
				'                    Dim keterangan As String = ""
				'                    SQL = "select c.akun_Persediaan, a.kode_group_jenis "
				'                    SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
				'                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
				'                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
				'                    SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
				'                    SQL = SQL & "and b.kode_stock_owner = '" & arrSO.Item(Cmb_LokasiSimpan.SelectedIndex) & "' and b.Kode_Barang='" & Txt_KdBarang.Text & "' "
				'                    Using Dr = OpenTrans(SQL)
				'                        If Dr.Read Then
				'                            Akun_Persediaan = Dr("akun_Persediaan")
				'                            keterangan = "Persediaan " & Dr("kode_group_jenis")
				'                        Else
				'                            Dr.Close()
				'                            CloseTrans()
				'                            CloseConn()
				'                            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                            Exit Sub
				'                        End If
				'                    End Using

				'#Region "Jurnal 1"

				'                    'HPP Pada Barang Dalam Proses

				'                    Dim Kode_voucher As String = ""
				'                    Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
				'                    Dim pagenumber As Integer = 1

				'                    SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
				'                    SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
				'                    SQL = SQL & "'" & Kode_voucher & "', "
				'                    SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
				'                    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
				'                    SQL = SQL & "'" & KodeProyek & "', 'Penerimaan Barang Jadi " & TxtFormulator_NoFaktur.Text & "', '', "
				'                    SQL = SQL & "'-', '" & UserID & "')"
				'                    ExecuteTrans(SQL)

				'                    'Insert HPP Total
				'                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_HPP_FG, 1),
				'                     Strings.Mid(akun_HPP_FG, 2, 1),
				'                     Strings.Mid(Ganti(akun_HPP_FG), 3),
				'                     KodePerusahaan, KodeProyek, keterangan2 & TxtFormulator_NoFaktur.Text, TotalHPP, "0", pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
				'                    ExecuteTrans(SQL)
				'                    pagenumber = pagenumber + 1

				'                    'Insert Data Bahan dan Packaging yg dipakai
				'                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Akun_Persediaan_Dalam_Proses, 1),
				'                                  Strings.Mid(Akun_Persediaan_Dalam_Proses, 2, 1),
				'                                  Strings.Mid(Ganti(Akun_Persediaan_Dalam_Proses), 3),
				'                                  KodePerusahaan, KodeProyek, "Persediaan Dalam Proses " & TxtFormulator_NoFaktur.Text, "0", Nilai_Bahan_Baku_Total, pagenumber, fso, Bahasa_Pilihan, Ket_Cost_Center_HO)
				'                    ExecuteTrans(SQL)
				'                    pagenumber = pagenumber + 1

				'                    If Nilai_loss_production_Total <> 0 Then

				'                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_Loss_production, 1),
				'                        Strings.Mid(akun_Loss_production, 2, 1),
				'                        Strings.Mid(Ganti(akun_Loss_production), 3),
				'                        KodePerusahaan, KodeProyek, ket_loss_production & TxtFormulator_NoFaktur.Text, "0", Nilai_loss_production_Total, pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
				'                        ExecuteTrans(SQL)
				'                        pagenumber = pagenumber + 1

				'                    End If

				'                    If Nilai_Packaging <> 0 Then
				'                        'HPP Pada Barang Dalam Proses

				'                        Dim ket_packaging As String = ""
				'                        Dim akun_kredit_packaging As String = ""
				'                        Dim lok_packaging As String = ""

				'                        SQL = "select top(1) "
				'                        SQL = SQL & "b.Id_Group_Jenis, b.kode_stock_owner, c.akun_persediaan, Kode_Group_Jenis "
				'                        SQL = SQL & "from N_EMI_Transaksi_Trial_Production_Results_Packaging_Det a, Barang b, EMI_Group_Jenis_Akun c, "
				'                        SQL = SQL & "N_EMI_Transaksi_Trial_Production_Results_Packaging_Detail e, EMI_Group_Jenis f where "
				'                        SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
				'                        SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
				'                        SQL = SQL & "and b.Kode_Perusahaan = f.Kode_Perusahaan and b.Id_Group_Jenis = f.Id_Group_Jenis "
				'                        SQL = SQL & "and f.Kode_Perusahaan = c.Kode_Perusahaan and f.Id_Group_Jenis = c.Id_Group_Jenis "
				'                        SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
				'                        SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.No_Transaksi = e.No_Transaksi "
				'                        SQL = SQL & "and a.Kode_Stock_Owner = e.Kode_Stock_Owner and a.Kode_Barang = e.Kode_Barang "
				'                        SQL = SQL & "and a.No_Urut_Detail = e.Urut "
				'                        SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				'                        SQL = SQL & "and a.No_Transaksi = '" & TxtFormulator_NoFaktur.Text & "' "
				'                        SQL = SQL & "and e.status is null "
				'                        Using Ds = BindingTrans(SQL)
				'                            With Ds.Tables("MyTable")
				'                                If .Rows.Count <> 0 Then
				'                                    For h As Integer = 0 To .Rows.Count - 1

				'                                        lok_packaging = .Rows(h).Item("kode_stock_owner")
				'                                        akun_kredit_packaging = .Rows(h).Item("akun_persediaan")
				'                                        ket_packaging = "Persediaan " + .Rows(h).Item("Kode_Group_Jenis")

				'                                    Next
				'                                End If
				'                            End With
				'                        End Using

				'                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_kredit_packaging, 1),
				'                   Strings.Mid(akun_kredit_packaging, 2, 1),
				'                   Strings.Mid(Ganti(akun_kredit_packaging), 3),
				'                   KodePerusahaan, KodeProyek, ket_packaging & TxtFormulator_NoFaktur.Text, "0", Nilai_Packaging, pagenumber, lok_packaging, Bahasa_Pilihan, Ket_Cost_Center_HO)
				'                        ExecuteTrans(SQL)
				'                        pagenumber = pagenumber + 1

				'                    End If

				'                    SQL = "select b.id_work_center, b.kode_jenis_biaya, sum(b.total) as Nilai from "
				'                    SQL = SQL & "N_EMI_Transaksi_Trial_Production_Results_Detail_Biaya a, N_EMI_Transaksi_Trial_Production_Results_Detail_Biaya_WC b where "
				'                    SQL = SQL & "a.kode_perusahaan=b.kode_perusahaan and a.no_transaksi=b.No_transaksi and a.urut=b.urut_detail "
				'                    SQL = SQL & "and a.kode_perusahaan='" & KodePerusahaan & "' and a.No_transaksi='" & TxtFormulator_NoFaktur.Text & "' and a.Proses='" & proses & "' and a.jenis='BIAYA' and Jenis_Barang='FG' "
				'                    SQL = SQL & "group by b.kode_jenis_biaya, b.id_work_center "
				'                    Using Ds = BindingTrans(SQL)
				'                        With Ds.Tables("MyTable")
				'                            If .Rows.Count <> 0 Then
				'                                For h As Integer = 0 To .Rows.Count - 1

				'                                    Dim kode_jenis_biaya As String = .Rows(h).Item("kode_jenis_biaya")
				'                                    Dim Total_jenis_biaya As String = .Rows(h).Item("Nilai")
				'                                    Dim ID_Work_Center As String = .Rows(h).Item("id_work_center")

				'                                    Dim akun As String = ""
				'                                    Dim ket As String = ""

				'                                    SQL = "Select Kode_Akun_Biaya, Kode_Akun_Budget, a.keterangan "
				'                                    SQL = SQL & "From Emi_Jenis_Biaya_Produksi a where "
				'                                    SQL = SQL & " a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				'                                    SQL = SQL & "and kode_jenis_biaya_Produksi = '" & kode_jenis_biaya & "' "
				'                                    Using dr = OpenTrans(SQL)
				'                                        If dr.Read Then
				'                                            akun = dr("Kode_Akun_Budget")
				'                                            ket = dr("keterangan")
				'                                        End If
				'                                    End Using

				'                                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun, 1),
				'                                   Strings.Mid(akun, 2, 1),
				'                                   Strings.Mid(Ganti(akun), 3),
				'                                   KodePerusahaan, KodeProyek, ket & " " & TxtFormulator_NoFaktur.Text, "0", Total_jenis_biaya, pagenumber, Lokasi, Bahasa_Pilihan, ID_Work_Center)
				'                                    ExecuteTrans(SQL)
				'                                    pagenumber = pagenumber + 1

				'                                Next
				'                            End If
				'                        End With
				'                    End Using

				'                    Dim nilai_selisih As Double = Math.Round(TotalHPP - (Nilai_Bahan_Baku_Total + Nilai_Packaging + Nilai_loss_production_Total + Hpp_Work_Center_total), 4)

				'                    If nilai_selisih > 20000 Then
				'                        CloseTrans()
				'                        CloseConn()
				'                        MessageBox.Show("Terjadiiii !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                        Exit Sub
				'                    End If

				'                    If nilai_selisih <> 0 Then
				'                        If nilai_selisih < 0 Then
				'                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_Selisih_pembulatan, 1),
				'                        Strings.Mid(akun_Selisih_pembulatan, 2, 1),
				'                        Strings.Mid(Ganti(akun_Selisih_pembulatan), 3),
				'                        KodePerusahaan, KodeProyek, "Selisih Pembulatan; ", Math.Abs(nilai_selisih), "0", pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
				'                            ExecuteTrans(SQL)
				'                            pagenumber = pagenumber + 1
				'                        Else
				'                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_Selisih_pembulatan, 1),
				'                        Strings.Mid(akun_Selisih_pembulatan, 2, 1),
				'                        Strings.Mid(Ganti(akun_Selisih_pembulatan), 3),
				'                        KodePerusahaan, KodeProyek, "Selisih Pembulatan; ", "0", nilai_selisih, pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
				'                            ExecuteTrans(SQL)
				'                            pagenumber = pagenumber + 1

				'                        End If
				'                    End If

				'                    SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
				'                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
				'                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
				'                    Using Dr = OpenTrans(SQL)
				'                        If Dr.Read Then
				'                            If Dr("debit") <> Dr("kredit") Then
				'                                Dr.Close()
				'                                CloseTrans()
				'                                CloseConn()
				'                                MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                                Exit Sub
				'                            End If
				'                        Else
				'                            Dr.Close()
				'                            CloseTrans()
				'                            CloseConn()
				'                            MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                            Exit Sub
				'                        End If
				'                    End Using

				'                    SQL = "insert into N_EMI_Transaksi_Trial_Production_Results_Jurnal (Kode_Perusahaan,No_Transaksi,Kode_Voucher,Proses, Jenis) values ("
				'                    SQL = SQL & "'" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "','" & Kode_voucher & "',"
				'                    SQL = SQL & "'" & proses & "', 'GR1') "
				'                    ExecuteTrans(SQL)

				'#End Region

				'#Region "Jurnal 2"

				'                    'Persediaan Pada HPP

				'                    Dim Kode_voucher2 As String = ""
				'                    Kode_voucher2 = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
				'                    Dim pagenumber2 As Integer = 1

				'                    SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
				'                    SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
				'                    SQL = SQL & "'" & Kode_voucher2 & "', "
				'                    SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
				'                    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
				'                    SQL = SQL & "'" & KodeProyek & "', 'Pengeluaran Bahan Baku " & TxtFormulator_NoFaktur.Text & "', '', "
				'                    SQL = SQL & "'-', '" & UserID & "')"
				'                    ExecuteTrans(SQL)

				'                    'Insert HPP Total
				'                    SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(Akun_Persediaan, 1),
				'                     Strings.Mid(Akun_Persediaan, 2, 1),
				'                     Strings.Mid(Ganti(Akun_Persediaan), 3),
				'                     KodePerusahaan, KodeProyek, keterangan2 & TxtFormulator_NoFaktur.Text, TotalHPP, "0", pagenumber2, arrSO(Cmb_LokasiSimpan.SelectedIndex), Bahasa_Pilihan, Ket_Cost_Center_HO)
				'                    ExecuteTrans(SQL)
				'                    pagenumber2 = pagenumber2 + 1

				'                    'Insert Data Bahan dan Packaging yg dipakai
				'                    SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(akun_HPP_FG, 1),
				'                                  Strings.Mid(akun_HPP_FG, 2, 1),
				'                                  Strings.Mid(Ganti(akun_HPP_FG), 3),
				'                                  KodePerusahaan, KodeProyek, "Persediaan Dalam Proses " & TxtFormulator_NoFaktur.Text, "0", TotalHPP, pagenumber2, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
				'                    ExecuteTrans(SQL)
				'                    pagenumber2 = pagenumber2 + 1

				'                    SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
				'                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
				'                    SQL = SQL & "kode_voucher = '" & Kode_voucher2 & "'"
				'                    Using Dr = OpenTrans(SQL)
				'                        If Dr.Read Then
				'                            If Dr("debit") <> Dr("kredit") Then
				'                                Dr.Close()
				'                                CloseTrans()
				'                                CloseConn()
				'                                MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                                Exit Sub
				'                            End If
				'                        Else
				'                            Dr.Close()
				'                            CloseTrans()
				'                            CloseConn()
				'                            MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				'                            Exit Sub
				'                        End If
				'                    End Using

				'                    SQL = "insert into N_EMI_Transaksi_Trial_Production_Results_Jurnal (Kode_Perusahaan,No_Transaksi,Kode_Voucher,Proses, Jenis) values ("
				'                    SQL = SQL & "'" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "','" & Kode_voucher2 & "',"
				'                    SQL = SQL & "'" & proses & "', 'GR2') "
				'                    ExecuteTrans(SQL)

				'#End Region

				'                End If

#End Region

			Next

#Region "Comment"

			'DIKOMEN KARENA ADA STEP SELANJUTNYA
			'===============================================================
			'=     GET Emi_Production_Results_Detail_Pallet SEBELUMNYA     =
			'===============================================================
			'''SQL = "select a.No_Transaksi, a.Proses, a.Jumlah, a.NIlai_Barang, a.Satuan, a.Satuan_Barang, a.Id_Warehouse, a.Serial_Number, a.Jenis, a.Nomor_Pallet, a.Proses, a.urut_oto, Kode_Unik_Berjalan, Kode_Unik_Asal, Batch_Number, Qr_Code, Tgl_Produksi, Tgl_Expired "
			'''SQL = SQL & "from Emi_Production_Results_Detail_Pallet a "
			'''SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'''SQL = SQL & "and a.No_Transaksi ='" & TxtFormulator_NoFaktur.Text & "' "
			'''SQL = SQL & "and a.Proses = '" & proses & "'"
			'''Using Ds = BindingTrans(SQL)
			'''    With Ds.Tables("MyTable")
			'''        If .Rows.Count <> 0 Then
			'''            For i As Integer = 0 To .Rows.Count - 1

			'''                Dim SnLama As String = .Rows(i).Item("Serial_Number")
			'''                Dim WarnaLama As String = .Rows(i).Item("Jenis")
			'''                Dim HargaHpp2 As String = Get_Harga_SN(SnLama)

			'''                'Generate Sn Baru
			'''                Dim Rand2 As New Random
			'''                Dim sts2 As String = Format(Rand2.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
			'''                Dim Kode_Unik2 As String = sts2.Substring(0, 5) & Chr(64 + sts2.Substring(6, 1)) & sts2.Substring(6, Len(sts2) - 6)
			'''                Dim SN2 As String = Kode_Unik2 & Tanda_SN & "01" & Tanda_SN & HargaHpp2 & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

			'''                '================================
			'''                '=     POTONG SN SEBELUMNYA     =
			'''                '================================
			'''                SQL = "update Barang_SN set Jumlah = Jumlah - " & .Rows(i).Item("NIlai_Barang") & " where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SnLama & "' "
			'''                SQL = SQL & "and Warna = '" & WarnaLama & "'"
			'''                ExecuteTrans(SQL)

			'''                SQL = "Update barang set "
			'''                SQL = SQL & "Good_Stock = Good_Stock - " & .Rows(i).Item("NIlai_Barang") & " "
			'''                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
			'''                SQL = SQL & "kode_stock_owner = '" & Kd_So & "' and kode_barang = '" & Kd_Brg & "'"
			'''                ExecuteTrans(SQL)

			'''                SQL = "SELECT "
			'''                SQL = SQL & "ROUND(SUM(good_stock), 4) AS good_stock, "
			'''                SQL = SQL & "ISNULL((SELECT ROUND(SUM(jumlah), 4) FROM Barang_sn x WHERE a.kode_Barang = x.kode_Barang AND a.Kode_Stock_Owner = x.Kode_Stock_Owner AND a.kode_Perusahaan = x.kode_Perusahaan), 0) AS Jumlah_sn, "
			'''                SQL = SQL & "ISNULL(ROUND(SUM(jumlah_bags), 4), 0) AS jumlah_bags_barang, "
			'''                SQL = SQL & "ISNULL((SELECT ROUND(SUM(Jumlah_Bags), 4) FROM Barang_sn y WHERE a.kode_Barang = y.kode_Barang AND a.Kode_Stock_Owner = y.Kode_Stock_Owner AND a.kode_Perusahaan = y.Kode_Perusahaan), 0) AS jumlah_bags_sn "
			'''                SQL = SQL & "FROM "
			'''                SQL = SQL & "barang a "
			'''                SQL = SQL & "WHERE "
			'''                SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' And a.Kode_Stock_Owner = '" & Kd_So & "' AND a.Kode_Barang = '" & Kd_Brg & "' "
			'''                SQL = SQL & "GROUP BY "
			'''                SQL = SQL & "a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan"
			'''                Using Ds2 = BindingTrans(SQL)
			'''                    If Ds2.Tables("MyTable").Rows.Count <> 0 Then

			'''                        Dim Stock_Barang As String = Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(0).Item("good_stock"), "N4")))
			'''                        Dim Stock_Sn As String = Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(0).Item("Jumlah_sn"), "N4")))
			'''                        Dim Bags_Barang As String = Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_barang"), "N4")))
			'''                        Dim Bags_Sn As String = Val(HilangkanTanda(Format(Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_sn"), "N4")))

			'''                        If Stock_Barang <> Stock_Sn Or Bags_Barang <> Bags_Sn Then
			'''                            CloseTrans()
			'''                            CloseConn()
			'''                            MessageBox.Show("Terjadi Kesalahan Pada SN . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'''                            Exit Sub
			'''                        End If

			'''                    Else
			'''                        CloseTrans()
			'''                        CloseConn()
			'''                        MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'''                        Exit Sub

			'''                    End If

			'''                End Using

			'''                '==========================
			'''                '=     CEK SN HARUS 0     =
			'''                '==========================
			'''                SQL = "select Jumlah from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SnLama & "' and Warna = '" & WarnaLama & "'"
			'''                Using Dr = OpenTrans(SQL)
			'''                    If Dr.Read Then
			'''                        If Val(Dr("Jumlah")) <> 0 Then
			'''                            Dr.Close()
			'''                            CloseTrans()
			'''                            CloseConn()
			'''                            MessageBox.Show("Terjadi Kesalahan Pada SN . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'''                            Exit Sub
			'''                        End If
			'''                    Else
			'''                        Dr.Close()
			'''                        CloseTrans()
			'''                        CloseConn()
			'''                        MessageBox.Show("SN Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'''                        Exit Sub
			'''                    End If

			'''                End Using

			'''                SQL = "update Emi_Production_Results_Detail_Pallet set SN_Baru ='" & SN2 & "', Lokasi_Gudang='" & arrSO(Cmb_LokasiSimpan.SelectedIndex) & "' where Kode_Perusahaan = '" & KodePerusahaan & "' "
			'''                SQL = SQL & "and urut_oto = '" & .Rows(i).Item("urut_oto") & "'"
			'''                ExecuteTrans(SQL)

			'''                '================================
			'''                '=     GET WAREHOUSE KOSONG     =
			'''                '================================
			'''                Dim available_Id_Warehouse2 As String = ""
			'''                Dim available_NoPallet2 As String = ""

			'''                SQL = "select top(1) a.id_wms_warehouse_position, b.nomor_urut from "
			'''                SQL = SQL & "view_warehouse_position a, view_warehouse_position_detail b "
			'''                SQL = SQL & "where a.Id_WMS_Warehouse_Position=b.Id_WMS_Warehouse_Position "
			'''                SQL = SQL & " And a.kode_Perusahaan = b.kode_Perusahaan And a.kode_Perusahaan ='" & KodePerusahaan & "' "
			'''                SQL = SQL & "and a.Kode_Stock_Owner='" & arrSO(Cmb_LokasiSimpan.SelectedIndex) & "' and b.Kode_Barang is null"
			'''                Using Dr2 = OpenTrans(SQL)
			'''                    If Dr2.Read Then
			'''                        available_Id_Warehouse2 = Dr2("id_wms_warehouse_position")
			'''                        available_NoPallet2 = Dr2("nomor_urut")
			'''                    Else
			'''                        Dr2.Close()
			'''                        CloseTrans()
			'''                        CloseConn()
			'''                        MessageBox.Show("Rak sudah Penuh")
			'''                        Exit Sub
			'''                    End If
			'''                End Using

			'''                SQL = "insert into barang_sn_sementara(kode_perusahaan, kode_stock_owner, kode_barang, "
			'''                SQL = SQL & "serial_number, Jumlah, Warna, Kode_Unik_Berjalan, Kode_Unik_Asal, "
			'''                SQL = SQL & "Qr_Code, Batch_Number, Id_Warehouse, Nomor_Pallet, Flag_Produksi, Flag_QI, Tgl_Produksi, Tgl_Expired) values('" & KodePerusahaan & "', "
			'''                SQL = SQL & "'" & arrSO(Cmb_LokasiSimpan.SelectedIndex) & "', '" & Kd_Brg & "', "
			'''                SQL = SQL & "'" & SN2 & "', " & .Rows(i).Item("NIlai_Barang") & ", '" & WarnaLama & "', "
			'''                SQL = SQL & "'" & .Rows(i).Item("Kode_Unik_Berjalan") & "', '" & .Rows(i).Item("Kode_Unik_Asal") & "', '" & .Rows(i).Item("Qr_Code") & "', "
			'''                SQL = SQL & "'" & .Rows(i).Item("Batch_Number") & "', '" & available_Id_Warehouse2 & "', '" & available_NoPallet2 & "', 'Y', 'Y', "
			'''                SQL = SQL & " '" & .Rows(i).Item("Tgl_Produksi") & "', '" & .Rows(i).Item("Tgl_Expired") & "')"
			'''                ExecuteTrans(SQL)

			'''            Next
			'''        End If
			'''    End With
			'''End Using

#End Region

			'========================
			'=     PROSES SCRAP     =
			'========================

#Region "Proses Scrap"

			'If Val(TxtJmlScrap.Text) <> 0 Then

			'    'GENERATE BAROCDE
			'    'TODO : Barcode Scrap
			'    Dim newBatchScrap As String = ""
			'    'Dim newBatchScrap As String = Generate_Batch_FG(tgl_skg, Prefix, DtpExpired.Value, Tahun_MulaiProduksi)

			'    SQL = "select top 1 b.Batch_Number "
			'    SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Scrap b "
			'    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
			'    SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
			'    SQL = SQL & "and a.Status is null "
			'    'SQL = SQL & "and b.Batch_Number is not null "
			'    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'    SQL = SQL & "and a.No_Production_Order = '" & Txt_NoSplit.Text & "' "
			'    SQL = SQL & "order by b.proses DESC "
			'    Using Dr1 = OpenTrans(SQL)
			'        If Dr1.Read Then
			'            If Not General_Class.CekNULL(Dr1("Batch_Number")) = "" Then
			'                newBatchScrap = Dr1("Batch_Number")
			'            Else
			'                newBatchScrap = Generate_Batch_FG(tgl_skg, Prefix, DtpExpired.Value, Tahun_MulaiProduksi)
			'            End If
			'        Else
			'            newBatchScrap = Generate_Batch_FG(tgl_skg, Prefix, DtpExpired.Value, Tahun_MulaiProduksi)
			'        End If
			'    End Using

			'    Dim newQrCodeScrap As String = Generate_QR_Batch(arrKdBarangSrap(CmbSisaProduksi.SelectedIndex), newBatchScrap)
			'    Dim Kode_BerjalanScrap As String = Generate_Random_Kode(10)
			'    Dim Kode_AsalScrap As String = Kode_BerjalanScrap

			'    Dim nilai_kecildetail As Double = Val(HilangkanTanda(Format(Val(HilangkanTanda(TxtJmlScrap.Text)), "N4")))
			'    'SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa', '" & arrKdBarangSrap.Item(CmbSisaProduksi.SelectedIndex) & "', '" & CmbSatScrap.Text & "', "
			'    'SQL = SQL & "'" & TxtSatScrapKecil.Text & " ', '" & TxtJmlScrap.Text & "' ) as hasil "
			'    'Using Dr1 = OpenTrans(SQL)
			'    '    If Dr1.Read Then
			'    '        If General_Class.CekNULL(Dr1("hasil")) = "" Then
			'    '            Dr1.Close()
			'    '            CloseTrans()
			'    '            CloseConn()
			'    '            MessageBox.Show("data konversi satuan kirim tidak ada ")
			'    '            Exit Sub
			'    '        End If

			'    '        nilai_kecildetail = Val(HilangkanTanda(Format(Dr1("hasil"), "N4")))
			'    '    Else
			'    '        Dr1.Close()
			'    '        CloseTrans()
			'    '        CloseConn()
			'    '        MessageBox.Show("data konversi satuan kirim tidak ada ")
			'    '        Exit Sub
			'    '    End If
			'    'End Using

			'    Dim available_Id_Warehouse As String = ""
			'    Dim available_NoPallet As String = ""

			'    SQL = "select top(1) a.id_wms_warehouse_position, 0 as nomor_urut from "
			'    SQL = SQL & "view_warehouse_position a "
			'    SQL = SQL & "where a.kode_Perusahaan ='" & KodePerusahaan & "' "
			'    SQL = SQL & "and a.Kode_Stock_Owner='" & Kd_So & "' "
			'    Using Dr2 = OpenTrans(SQL)
			'        Do While Dr2.Read
			'            available_Id_Warehouse = Dr2("id_wms_warehouse_position")
			'            available_NoPallet = Dr2("nomor_urut")
			'        Loop
			'    End Using

			'    '===========================
			'    '=     GET TOTAL SCRAP     =
			'    '===========================
			'    Dim TotalCountScrap As Double = 0
			'    SQL = "select distinct top(1) Nomor from Emi_Production_Results_Detail_Scrap where "
			'    SQL = SQL & "no_transaksi = '" & TxtFormulator_NoFaktur.Text & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
			'    SQL = SQL & "order by Nomor Desc "
			'    Using Dr = OpenTrans(SQL)
			'        If Dr.Read Then
			'            If General_Class.CekNULL(Dr("Nomor")) = "" Then
			'                TotalCountScrap = 1
			'            Else
			'                TotalCountScrap = Dr("Nomor") + 1
			'            End If
			'        Else
			'            TotalCountScrap = 1
			'        End If
			'    End Using

			'    Dim sisa_barang As Double = 0
			'    sisa_barang = nilai_kecildetail
			'    SQL = "Select b.jumlah_dosing, a.No_Production_Order, b.jumlah_dosing_pcs, b.jumlah_dosing - b.jumlah_Terpakai As sisa, Total_bahan_baku, "
			'    SQL = SQL & "total_biaya_produksi, nilai_loss_production, total_packaging, b.urut "
			'    SQL = SQL & "From EMI_Production_Results a, Emi_Production_Results_HPP b Where "
			'    SQL = SQL & "a.kode_Perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi And a.status Is null "
			'    SQL = SQL & "And round(jumlah_dosing,4)<>round(jumlah_terpakai,4) "
			'    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'    SQL = SQL & "and a.No_Transaksi = '" & TxtFormulator_NoFaktur.Text & "' "
			'    SQL = SQL & "and b.Tanggal is not null "
			'    SQL = SQL & "order by proses "
			'    Using dss = BindingTrans(SQL)
			'        For ind = 0 To dss.Tables("MyTable").Rows.Count - 1

			'            Dim urut_HPP As String = dss.Tables("MyTable").Rows(ind).Item("urut")
			'            Dim sisa As Double = Val(HilangkanTanda(Format(dss.Tables("MyTable").Rows(ind).Item("sisa"), "N4")))

			'            Dim jumlah_dosing As Double = Val(HilangkanTanda(Format(dss.Tables("MyTable").Rows(ind).Item("jumlah_dosing"), "N4")))
			'            Dim jumlah_dosing_pcs As Double = Val(HilangkanTanda(Format(dss.Tables("MyTable").Rows(ind).Item("jumlah_dosing_pcs"), "N4")))

			'            Dim NoSplit As String = dss.Tables("MyTable").Rows(ind).Item("No_Production_Order")

			'            Dim Persen_loss_production As Double = 0
			'            Dim Nilai_loss_production As Double = 0
			'            Dim Nilai_loss_production_Pcs As Double = 0

			'            Dim Nilai_Bahan_Baku As Double = 0
			'            Dim Nilai_Bahan_Baku_Pcs As Double = 0

			'            Dim Hpp_Bahan_baku_Total As Double = 0

			'            Dim satuan_bahan As String = ""

			'            Dim nilai_potong As Double = 0
			'            If sisa > sisa_barang Then

			'                nilai_potong = sisa_barang
			'                sisa_barang -= sisa_barang
			'            Else

			'                nilai_potong = sisa
			'                sisa_barang -= sisa

			'            End If

			'            SQL = "Select b.Total_Bahan_Baku As Total, b.satuan as satuan_barang  "
			'            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_HPP b where "
			'            SQL = SQL & "a.kode_perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi And a.status Is null And "
			'            SQL = SQL & "a.Kode_Perusahaan='" & KodePerusahaan & "' and a.No_Transaksi='" & TxtFormulator_NoFaktur.Text & "' and b.Urut='" & urut_HPP & "' "
			'            'SQL = SQL & "group by satuan "
			'            Using dr = OpenTrans(SQL)
			'                If dr.Read Then
			'                    satuan_bahan = dr("satuan_barang")
			'                    Hpp_Bahan_baku_Total = Val(HilangkanTanda(Format(dr("Total"), "N0")))
			'                End If
			'            End Using

			'            SQL = "select Nilai_Persen from "
			'            SQL = SQL & "Emi_Budgeting_Loss_Production where "
			'            SQL = SQL & "Kode_Perusahaan='" & KodePerusahaan & "' "
			'            SQL = SQL & "order by Urut desc "
			'            Using dr = OpenTrans(SQL)
			'                If dr.Read Then
			'                    Persen_loss_production = dr("Nilai_Persen")

			'                End If
			'            End Using

			'            '''Nilai_loss_production = Math.Round((Math.Round(Hpp_Bahan_baku_Total * Persen_loss_production / 100, 0) / nilai_kecildetail * nilai_potong), 0)

			'            '''Nilai_loss_production_TotalSCP += Nilai_loss_production

			'            '''Nilai_loss_production_Pcs = Val(HilangkanTanda(Format(Nilai_loss_production / nilai_potong, "N0")))

			'            '''SQL = "insert into N_Emi_Production_Results_Detail_Biaya(kode_perusahaan, No_Transaksi, Proses, "
			'            '''SQL = SQL & "Jenis, Jumlah_Dosing, Hpp_Per_Pcs, HPP_Total, Urut_HPP, Jenis_Barang, Jumlah_Hitung) values( "
			'            '''SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & proses & "', "
			'            '''SQL = SQL & "'LOSS', '" & nilai_kecildetail & "', '" & Nilai_loss_production_Pcs & "', '" & Nilai_loss_production & "', '" & urut_HPP & "', 'SCP', '" & nilai_potong & "')"
			'            '''ExecuteTrans(SQL)

			'            Nilai_Bahan_Baku = Math.Floor((Hpp_Bahan_baku_Total / jumlah_dosing * nilai_potong))

			'            Nilai_Bahan_Baku_TotalSCP += Nilai_Bahan_Baku

			'            Nilai_Bahan_Baku_Pcs = Val(HilangkanTanda(Format(Nilai_Bahan_Baku / nilai_potong, "N0")))

			'            SQL = "insert into N_Emi_Production_Results_Detail_Biaya(kode_perusahaan, No_Transaksi, Proses, "
			'            SQL = SQL & "Jenis, Jumlah_Dosing, Hpp_Per_Pcs, HPP_Total, Urut_HPP, Jenis_Barang, Jumlah_Hitung) values( "
			'            SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & proses & "', "
			'            SQL = SQL & "'BAHAN', '" & nilai_kecildetail & "', '" & Nilai_Bahan_Baku_Pcs & "', '" & Nilai_Bahan_Baku & "', '" & urut_HPP & "', 'SCP', '" & nilai_potong & "')"
			'            ExecuteTrans(SQL)

			'            'Dim nilai_bahan_KG As Double = Val(HilangkanTanda(Format(dss.Tables("MyTable").Rows(ind).Item("Total_bahan_baku") / jumlah_dosing, "N0")))
			'            'Dim nilai_biaya_KG As Double = Val(HilangkanTanda(Format(dss.Tables("MyTable").Rows(ind).Item("total_biaya_produksi") / jumlah_dosing, "N0")))
			'            'Dim nilai_loss_KG As Double = Val(HilangkanTanda(Format(dss.Tables("MyTable").Rows(ind).Item("nilai_loss_production") / jumlah_dosing, "N0")))

			'            Dim nilai_HPP_pcs As Double = Math.Round(Nilai_Bahan_Baku_Pcs + Hpp_Work_Center_PcsSCP + Nilai_loss_production_Pcs, 0)

			'            Dim Rand As New Random
			'            Dim str As String = Format(Rand.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
			'            Dim Kode_Unik As String = str.Substring(0, 5) & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
			'            Dim SN As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & nilai_HPP_pcs & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

			'            TotalHPPScrap += Math.Round(nilai_potong * nilai_HPP_pcs, 0)

			'            SQL = "Update barang set "
			'            SQL = SQL & "good_stock = good_stock + " & nilai_potong & " "
			'            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
			'            SQL = SQL & "kode_stock_owner = '" & arrSoGudangSisa(Cmb_Lokasi_Gudang_Sisa.SelectedIndex) & "' and kode_barang = '" & arrKdBarangSrap.Item(CmbSisaProduksi.SelectedIndex) & "'"
			'            ExecuteTrans(SQL)

			'            SQL = "select kode_barang from barang_sn where "
			'            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
			'            SQL = SQL & "kode_stock_owner = '" & arrSoGudangSisa(Cmb_Lokasi_Gudang_Sisa.SelectedIndex) & "' and "
			'            SQL = SQL & "kode_barang = '" & arrKdBarangSrap.Item(CmbSisaProduksi.SelectedIndex) & "' and serial_number = '" & SN & "'"
			'            Using Dr = OpenTrans(SQL)
			'                If Dr.Read Then
			'                    Dr.Close()
			'                    CloseTrans()
			'                    CloseConn()
			'                    MessageBox.Show("Terjadi kesalahan pada Barang . . !, Silahkan Ulangi Transaksi . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                    Exit Sub
			'                Else
			'                    SQL = "insert into barang_sn(kode_perusahaan, kode_stock_owner, kode_barang, "
			'                    SQL = SQL & "serial_number, jumlah, Jumlah_Bags, Warna, Kode_Unik_Berjalan, Kode_Unik_Asal, "
			'                    SQL = SQL & "Qr_Code, Batch_Number, Id_Warehouse, Nomor_Pallet, Tgl_Produksi, Tgl_Expired, tgl_masuk) values('" & KodePerusahaan & "', "
			'                    SQL = SQL & "'" & arrSoGudangSisa(Cmb_Lokasi_Gudang_Sisa.SelectedIndex) & "', '" & arrKdBarangSrap.Item(CmbSisaProduksi.SelectedIndex) & "', "
			'                    SQL = SQL & "'" & SN & "', " & nilai_potong & ", 0, 'HIJAU', '" & Kode_BerjalanScrap & "', "
			'                    SQL = SQL & "'" & Kode_AsalScrap & "', '" & newQrCodeScrap & "', "
			'                    SQL = SQL & "'" & newBatchScrap & "', '" & available_Id_Warehouse & "', '" & available_NoPallet & "', "
			'                    SQL = SQL & "'" & Format(DtpProduksi.Value, "yyyy-MM-dd") & "', '" & Format(DtpProduksi.Value, "yyyy-MM-dd") & "', '" & Format(DtpProduksi.Value, "yyyy-MM-dd") & "')"
			'                    Dr.Close()
			'                    ExecuteTrans(SQL)
			'                End If
			'            End Using

			'            SQL = "insert into Emi_Production_Results_Detail_Scrap (Kode_Perusahaan, No_Transaksi, Kode_Unik_Berjalan, Kode_Unik_Asal, Qr_Code, Jumlah, Satuan, NIlai_Barang, "
			'            SQL = SQL & "Satuan_Barang, Batch_Number, Id_Warehouse, Nomor_Pallet, proses, serial_number, Urut_HPP, Lokasi_Gudang_Sisa, Nomor) values "
			'            SQL = SQL & "('" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & Kode_BerjalanScrap & "', '" & Kode_AsalScrap & "', "
			'            SQL = SQL & "'" & newQrCodeScrap & "', '" & nilai_potong & "', '" & CmbSatScrap.Text & "', '" & nilai_potong & "', '" & TxtSatScrapKecil.Text & "', "
			'            SQL = SQL & "'" & newBatchScrap & "', '" & available_Id_Warehouse & "', '" & available_NoPallet & "', '" & proses & "', '" & SN & "', '" & urut_HPP & "', "
			'            SQL = SQL & "'" & arrSoGudangSisa(Cmb_Lokasi_Gudang_Sisa.SelectedIndex) & "', '" & TotalCountScrap & "')"
			'            ExecuteTrans(SQL)

			'            SQL = "Update Emi_Production_Results_HPP set "
			'            SQL = SQL & "jumlah_Terpakai = jumlah_Terpakai + " & nilai_potong & " "
			'            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
			'            SQL = SQL & "Urut = '" & urut_HPP & "' "
			'            ExecuteTrans(SQL)

			'        Next
			'    End Using

			'    If sisa_barang <> 0 Then
			'        CloseTrans()
			'        CloseConn()
			'        MessageBox.Show("Terjadi Kesalahan ! ! . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        Exit Sub
			'    End If

			'    Dim MetodePengeluaranStokScrap As String = ""
			'    SQL = "select top 1 Metode_Pengeluaran_Stok from Barang where kode_perusahaan  = '" & KodePerusahaan & "'  "
			'    SQL = SQL & "and Kode_Barang = '" & arrKdBarangSrap(CmbSisaProduksi.SelectedIndex) & "'"
			'    Using Dr = OpenTrans(SQL)
			'        If Dr.Read Then
			'            MetodePengeluaranStokScrap = Dr("metode_pengeluaran_stok")
			'        Else

			'            Dr.Close()
			'            CloseTrans()
			'            CloseConn()
			'            MessageBox.Show("Metode Barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'            Exit Sub
			'        End If
			'    End Using

			'    '==================================
			'    '=      GENERATE NEW BARCODE      =
			'    '==================================
			'    'HAPUS TABEL SEMENTARA
			'    SQL = "truncate table N_EMI_Barcode_Label_Barcode_GR_1_Scrap "
			'    ExecuteTrans(SQL)

			'    kode_unik_print_scrap = Format(tgl_skg, "MMddHHmmss") & Format(random.Next(0, 10000), "00000")

			'    Dim fullNewQrScrap As String = newQrCodeScrap & "-" & Kode_BerjalanScrap

			'    Barcode.Image = Generate_QR_NoPadding(fullNewQrScrap)

			'    Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_print_scrap & ".jpg")

			'    '   Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeFinishGood.jpg")

			'    'If Not (System.IO.File.Exists(FileToSaveAs1)) Then
			'    Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
			'    'End If

			'    fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
			'    FileSize1 = fs1.Length
			'    rawData1 = New Byte(FileSize1) {}
			'    fs1.Read(rawData1, 0, FileSize1)
			'    fs1.Close()
			'    Cmd.Parameters.Add("@newBarcodescrap", SqlDbType.Image).Value = rawData1

			'    '=======================
			'    '=     GET ROUTING     =
			'    '=======================
			'    'Dim Id_Routing As String = ""
			'    Dim Routing As String = ""
			'    SQL = "select b.Id_Routing, c.Keterangan as Routing "
			'    SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Master_Routing c "
			'    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
			'    SQL = SQL & "and a.No_PO = b.No_Faktur "
			'    SQL = SQL & "and b.Id_Routing = c.Id_Routing "
			'    SQL = SQL & "and a.Status is null and b.Status is null "
			'    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'    SQL = SQL & "and a.No_Transaksi = '" & Txt_NoSplit.Text & "' "
			'    Using Dr = OpenTrans(SQL)
			'        If Dr.Read Then

			'            Routing = Dr("Routing")
			'        Else
			'            Dr.Close()
			'            CloseTrans()
			'            CloseConn()
			'            MessageBox.Show("Routing tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'            Exit Sub
			'        End If
			'    End Using

			'    ''INSERT TABEL CETAK QR
			'    'SQL = "insert into Cetak_Finish_Good (Kode_Perusahaan, Kode_Barang, Barcode, QrUtuh, Qr, Tgl_Expired, batch, tgl_produksi, kode_unik_print, tanggal_masuk,metode_pengeluaran_stok) values "
			'    ''SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_KdBarang.Text & "', @newBarcode, '" & Txt_NamaBarang.Text & "', "
			'    'SQL = SQL & "('" & KodePerusahaan & "', '" & arrKdBarangSrap(CmbSisaProduksi.SelectedIndex) & "', @newBarcodescrap, "
			'    'SQL = SQL & "'" & fullNewQrScrap & "', '" & newQrCodeScrap & "', '" & Format(Date.Parse(DtpExpired.Value), "yyyy-MM-dd") & "', '" & newBatchScrap & "',  '" & Format(DtpProduksi.Value, "yyyy-MM-dd") & "', "
			'    'SQL = SQL & "'" & kode_unik_print_scrap & "', '" & Format(DtpProduksi.Value, "yyyy-MM-dd") & "', '" & MetodePengeluaranStokScrap & "'"
			'    'SQL = SQL & ")"
			'    'ExecuteTrans(SQL)

			'    SQL = "insert into N_EMI_Barcode_Label_Barcode_GR_1_Scrap (kode_perusahaan, no_split, Barcode, Kode_barang, Nama_Barang, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, "
			'    SQL = SQL & "Proses, Jumlah, Satuan, Nomor, id_routing, routing, Kode_unik_print)  "
			'    SQL = SQL & "values ('" & KodePerusahaan & "', '" & Txt_NoSplit.Text & "', @newBarcodescrap, '" & arrKdBarangSrap(CmbSisaProduksi.SelectedIndex) & "', '" & CmbSisaProduksi.Text & "', '" & fullNewQrScrap & "', '" & newQrCodeScrap & "', "
			'    SQL = SQL & "'" & Format(DtpProduksi.Value, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & proses & "', '" & TxtJmlScrap.Text & "', '" & CmbSatScrap.Text & "', "
			'    SQL = SQL & "'" & TotalCountScrap & "', '" & ID_Routing & "', '" & Routing & "', '" & kode_unik_print_scrap & "') "
			'    ExecuteTrans(SQL)

			'End If

#End Region

#Region "Jurnal 3"

			'========================
			'=     JURNAL SCRAP     =
			'========================

			'Persediaan Scrap Pada Barang Dalam Proses
			'If TotalHPPScrap <> 0 Then

			'    Dim keteranganScrap As String = ""
			'    SQL = "select c.akun_Persediaan, a.kode_group_jenis "
			'    SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
			'    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
			'    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
			'    SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'    SQL = SQL & "and b.kode_stock_owner = '" & arrSoGudangSisa(Cmb_Lokasi_Gudang_Sisa.SelectedIndex) & "' and b.Kode_Barang='" & arrKdBarangSrap.Item(CmbSisaProduksi.SelectedIndex) & "' "
			'    Using Dr = OpenTrans(SQL)
			'        If Dr.Read Then
			'            Akun_PersediaanScrap = Dr("akun_Persediaan")
			'            keteranganScrap = "Persediaan " & Dr("kode_group_jenis")
			'        Else
			'            Dr.Close()
			'            CloseTrans()
			'            CloseConn()
			'            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'            Exit Sub
			'        End If
			'    End Using

			'    Dim Kode_voucher3 As String = ""
			'    Kode_voucher3 = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
			'    Dim pagenumber3 As Integer = 1

			'    SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
			'    SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
			'    SQL = SQL & "'" & Kode_voucher3 & "', "
			'    SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
			'    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
			'    SQL = SQL & "'" & KodeProyek & "', 'Pengeluaran Bahan Baku " & TxtFormulator_NoFaktur.Text & "', '', "
			'    SQL = SQL & "'-', '" & UserID & "')"
			'    ExecuteTrans(SQL)

			'    'Insert HPP Total
			'    SQL = Get_Detail_Jurnal(Kode_voucher3, Strings.Left(Akun_PersediaanScrap, 1),
			'             Strings.Mid(Akun_PersediaanScrap, 2, 1),
			'             Strings.Mid(Ganti(Akun_PersediaanScrap), 3),
			'             KodePerusahaan, KodeProyek, keteranganScrap & TxtFormulator_NoFaktur.Text, TotalHPPScrap, "0", pagenumber3, arrSoGudangSisa(Cmb_Lokasi_Gudang_Sisa.SelectedIndex), Bahasa_Pilihan, Ket_Cost_Center_HO)
			'    ExecuteTrans(SQL)
			'    pagenumber3 = pagenumber3 + 1

			'    'Insert Data Bahan dan Packaging yg dipakai
			'    SQL = Get_Detail_Jurnal(Kode_voucher3, Strings.Left(Akun_Persediaan_Dalam_Proses, 1),
			'                          Strings.Mid(Akun_Persediaan_Dalam_Proses, 2, 1),
			'                          Strings.Mid(Ganti(Akun_Persediaan_Dalam_Proses), 3),
			'                          KodePerusahaan, KodeProyek, "Persediaan Dalam Proses " & TxtFormulator_NoFaktur.Text, "0", Nilai_Bahan_Baku_TotalSCP, pagenumber3, fso, Bahasa_Pilihan, Ket_Cost_Center_HO)
			'    ExecuteTrans(SQL)
			'    pagenumber3 = pagenumber3 + 1

			'    If Nilai_loss_production_TotalSCP <> 0 Then

			'        SQL = Get_Detail_Jurnal(Kode_voucher3, Strings.Left(akun_Loss_production, 1),
			'            Strings.Mid(akun_Loss_production, 2, 1),
			'            Strings.Mid(Ganti(akun_Loss_production), 3),
			'            KodePerusahaan, KodeProyek, ket_loss_production & TxtFormulator_NoFaktur.Text, "0", Nilai_loss_production_TotalSCP, pagenumber3, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
			'        ExecuteTrans(SQL)
			'        pagenumber3 = pagenumber3 + 1

			'    End If

			'    SQL = "select b.id_work_center, b.kode_jenis_biaya, sum(b.total) as Nilai from "
			'    SQL = SQL & "N_Emi_Production_Results_Detail_Biaya a, N_Emi_Production_Results_Detail_Biaya_WC b where "
			'    SQL = SQL & "a.kode_perusahaan=b.kode_perusahaan and a.no_transaksi=b.No_transaksi and a.urut=b.urut_detail "
			'    SQL = SQL & "and a.kode_perusahaan='" & KodePerusahaan & "' and a.No_transaksi='" & TxtFormulator_NoFaktur.Text & "' and a.Proses='" & proses & "' and a.jenis='BIAYA' and Jenis_Barang='SCP' "
			'    SQL = SQL & "group by b.kode_jenis_biaya, b.id_work_center "
			'    Using Ds = BindingTrans(SQL)
			'        With Ds.Tables("MyTable")
			'            If .Rows.Count <> 0 Then
			'                For h As Integer = 0 To .Rows.Count - 1

			'                    Dim kode_jenis_biaya As String = .Rows(h).Item("kode_jenis_biaya")
			'                    Dim Total_jenis_biaya As String = .Rows(h).Item("Nilai")
			'                    Dim ID_Work_Center As String = .Rows(h).Item("id_work_center")

			'                    Dim akun As String = ""
			'                    Dim ket As String = ""

			'                    SQL = "Select Kode_Akun_Biaya, Kode_Akun_Budget, a.keterangan "
			'                    SQL = SQL & "From Emi_Jenis_Biaya_Produksi a where "
			'                    SQL = SQL & " a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'                    SQL = SQL & "and kode_jenis_biaya_Produksi = '" & kode_jenis_biaya & "' "
			'                    Using dr = OpenTrans(SQL)
			'                        If dr.Read Then
			'                            akun = dr("Kode_Akun_Biaya")
			'                            ket = dr("keterangan")
			'                        End If
			'                    End Using

			'                    SQL = Get_Detail_Jurnal(Kode_voucher3, Strings.Left(akun, 1),
			'                       Strings.Mid(akun, 2, 1),
			'                       Strings.Mid(Ganti(akun), 3),
			'                       KodePerusahaan, KodeProyek, ket & " " & TxtFormulator_NoFaktur.Text, "0", Total_jenis_biaya, pagenumber3, Lokasi, Bahasa_Pilihan, ID_Work_Center)
			'                    ExecuteTrans(SQL)
			'                    pagenumber3 = pagenumber3 + 1

			'                Next
			'            End If
			'        End With
			'    End Using

			'    Dim nilai_selisih As Double = Math.Round(TotalHPPScrap - (Nilai_Bahan_Baku_TotalSCP + Nilai_loss_production_TotalSCP + Hpp_Work_Center_totalSCP), 4)

			'    If nilai_selisih > 20000 Then
			'        CloseTrans()
			'        CloseConn()
			'        MessageBox.Show("Terjadiiii !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        Exit Sub
			'    End If

			'    If nilai_selisih <> 0 Then
			'        If nilai_selisih < 0 Then
			'            SQL = Get_Detail_Jurnal(Kode_voucher3, Strings.Left(akun_Selisih_pembulatan, 1),
			'            Strings.Mid(akun_Selisih_pembulatan, 2, 1),
			'            Strings.Mid(Ganti(akun_Selisih_pembulatan), 3),
			'            KodePerusahaan, KodeProyek, "Selisih Pembulatan; ", Math.Abs(nilai_selisih), "0", pagenumber3, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
			'            ExecuteTrans(SQL)
			'            pagenumber3 = pagenumber3 + 1
			'        Else
			'            SQL = Get_Detail_Jurnal(Kode_voucher3, Strings.Left(akun_Selisih_pembulatan, 1),
			'            Strings.Mid(akun_Selisih_pembulatan, 2, 1),
			'            Strings.Mid(Ganti(akun_Selisih_pembulatan), 3),
			'            KodePerusahaan, KodeProyek, "Selisih Pembulatan; ", "0", nilai_selisih, pagenumber3, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
			'            ExecuteTrans(SQL)
			'            pagenumber3 = pagenumber3 + 1

			'        End If
			'    End If

			'    SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
			'    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
			'    SQL = SQL & "kode_voucher = '" & Kode_voucher3 & "'"
			'    Using Dr = OpenTrans(SQL)
			'        If Dr.Read Then
			'            If Dr("debit") <> Dr("kredit") Then
			'                Dr.Close()
			'                CloseTrans()
			'                CloseConn()
			'                MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                Exit Sub
			'            End If
			'        Else
			'            Dr.Close()
			'            CloseTrans()
			'            CloseConn()
			'            MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'            Exit Sub
			'        End If
			'    End Using

			'    SQL = "insert into Emi_Production_Results_Jurnal (Kode_Perusahaan,No_Transaksi,Kode_Voucher,Proses, Jenis) values ("
			'    SQL = SQL & "'" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "','" & Kode_voucher3 & "',"
			'    SQL = SQL & "'" & proses & "', 'GR3') "
			'    ExecuteTrans(SQL)
			'End If

#End Region

			'==================================================
			'=     CEK APAKAH BATCH SUDAH TERPENUHI SEMUA     =
			'==================================================
			Dim Batch_Terpenuhi As New List(Of Integer)
			SQL = "select a.Proses "
			SQL &= $"from N_EMI_Transaksi_Trial_Production_Results_HPP a "
			SQL &= $"inner join N_EMI_Transaksi_Trial_Production_Results b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
			SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and b.Status is null "
			SQL &= $"and b.No_Production_Order = '{Txt_NoSplit.Text.Trim}' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Batch_Terpenuhi.Add(Val(Dr("Proses")))
				Loop
			End Using

			Dim TotalBatchBySplit As Integer = 0
			SQL = "select Jumlah_Batch, Status "
			SQL &= $"from N_EMI_Transaksi_Trial_Split_Production_Order "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and No_Transaksi = '{Txt_NoSplit.Text.Trim}' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then

					If General_Class.CekNULL(Dr("Status")) = "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, No Split {Txt_NoSplit.Text.Trim} Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If

					TotalBatchBySplit = Dr("Jumlah_Batch")
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"No Split {Txt_NoSplit.Text.Trim} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			' Cek Apakah ada Batch yang Belum Terpenuhi
			Dim missingBatch = Enumerable.Range(1, TotalBatchBySplit).Except(Batch_Terpenuhi).ToList()

			If missingBatch.Count = 0 Then

				SQL = "update N_EMI_Transaksi_Trial_Split_Production_Order set Flag_Hasil_Produksi_GR = 'Y', UserID_Selesai_GR = '" & UserID & "', "
				SQL = SQL & "Tgl_Hasil_Produksi_GR = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Hasil_Produksi_GR = '" & Format(tgl_skg, "HH:mm:ss") & "'  "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and no_transaksi = '" & Txt_NoSplit.Text.Trim & "' "
				ExecuteTrans(SQL)

			End If

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Data Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'===========================
		'=      CETAK BARCODE      =
		'===========================
		'TODO : Cetak

		For i As Integer = 0 To arr_kode_unik_print.Count - 1

			Try
				Dim CrDoc As New Object

				Dim KertasBesar As String = "BarcodeFG"

				SQL = "select Kode_Perusahaan from N_EMI_Transaksi_Trial_Barcode_Label_Barcode_GR_1 where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Barang='" & Txt_KdBarang.Text & "' and Kode_Unik_Print = '" & arr_kode_unik_print(i) & "' "
				Dim SelectionFormula As String = "{N_EMI_Transaksi_Trial_Barcode_Label_Barcode_GR_1.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Transaksi_Trial_Barcode_Label_Barcode_GR_1.Kode_Barang} = '" & Txt_KdBarang.Text & "' and {N_EMI_Transaksi_Trial_Barcode_Label_Barcode_GR_1.Kode_Unik_Print} = '" & arr_kode_unik_print(i) & "' "

				Cetak_Barcode(New N_EMI_Label_Barcode_Trial_GR_1, "Ceta Barcode Trial GR 1", SQL, SelectionFormula, PrinterBarcode, KertasBesar)
			Catch ex As Exception
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try

		Next

		MessageBox.Show("Berhasil Cetak Barcode", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

#Region "Cetak Scrap"

		'If Val(TxtJmlScrap.Text) <> 0 Then

		'    Try
		'        OpenConn()
		'        Dim CrDoc As New Object

		'        Dim KertasBesar As String = "BarcodeFG"
		'        Dim KertasKecil As String = "BarcodeQC"

		'        SQL = "select Kode_Perusahaan from N_EMI_Barcode_Label_Barcode_GR_1_Scrap where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Barang='" & arrKdBarangSrap(CmbSisaProduksi.SelectedIndex) & "' and Kode_Unik_Print = '" & kode_unik_print_scrap & "' "
		'        Using Ds = BindingTrans(SQL)
		'            If Ds.Tables("MyTable").Rows.Count <> 0 Then

		'                Dim printerDitemukan As Boolean = False
		'                '==========================
		'                '=     BARCODEE BESAR     =
		'                '==========================
		'                For Each printer As String In PrinterSettings.InstalledPrinters
		'                    If printer.ToLower() = PrinterBarcode.ToLower() Then
		'                        printerDitemukan = True
		'                        Exit For
		'                    End If
		'                Next

		'                If printerDitemukan Then
		'                    CrDoc = New N_EMI_Label_Barcode_GR_1_Scrap

		'                    'With A_Place_For_Printing2
		'                    '    CrDoc.SetDataSource(Ds)
		'                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
		'                    '    CrDoc.PrintOptions.PrinterName = ""
		'                    '    CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_1_Scrap.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Barcode_Label_Barcode_GR_1_Scrap.Kode_Barang} = '" & arrKdBarangSrap(CmbSisaProduksi.SelectedIndex) & "' and {N_EMI_Barcode_Label_Barcode_GR_1_Scrap.Kode_Unik_Print} = '" & kode_unik_print_scrap & "'  "
		'                    '    CrDoc.SummaryInfo.ReportTitle = "New Barcode Finish Good"
		'                    '    .Text = "New Barcode Finish Good"
		'                    '    .CrystalReportViewer1.ReportSource = CrDoc
		'                    '    .Refresh()
		'                    '    .Show()
		'                    'End With

		'                    '============================================

		'                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
		'                    CrDoc.SetDataSource(Ds)
		'                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
		'                    CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_1_Scrap.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Barcode_Label_Barcode_GR_1_Scrap.Kode_Barang} = '" & arrKdBarangSrap(CmbSisaProduksi.SelectedIndex) & "' and {N_EMI_Barcode_Label_Barcode_GR_1_Scrap.Kode_Unik_Print} = '" & kode_unik_print_scrap & "'  "
		'                    CrDoc.PrintOptions.PrinterName = PrinterBarcode

		'                    doctoprint.PrinterSettings.PrinterName = PrinterBarcode

		'                    Dim rawKind As Integer
		'                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
		'                    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
		'                        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = KertasBesar Then
		'                            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
		'                            CrDoc.PrintOptions.PaperSize = rawKind
		'                            Exit For
		'                        End If
		'                    Next

		'                    CrDoc.PrintToPrinter(1, False, 1, 2500)
		'                Else
		'                    MessageBox.Show("Printer FG Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

		'                End If

		'                printerDitemukan = False

		'                '==========================
		'                '=     BARCODEE KECIL     =
		'                '==========================
		'                'For Each printer As String In PrinterSettings.InstalledPrinters
		'                '    If printer.ToLower() = PrinterBarcodeQC.ToLower() Then
		'                '        'printerDitemukan = True
		'                '        Exit For
		'                '    End If
		'                'Next
		'                'CrDoc = New NewBarcodeFinishGoodKecil

		'                'If printerDitemukan Then

		'                '    'With A_Place_For_Printing2
		'                '    '    CrDoc.SetDataSource(Ds)
		'                '    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
		'                '    '    CrDoc.PrintOptions.PrinterName = ""
		'                '    '    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & arrKdBarangSrap(CmbSisaProduksi.SelectedIndex) & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print_scrap & "' "
		'                '    '    CrDoc.SummaryInfo.ReportTitle = "New Barcode Finish Good"
		'                '    '    .Text = "New Barcode Finish Good"
		'                '    '    .CrystalReportViewer1.ReportSource = CrDoc
		'                '    '    .Refresh()
		'                '    '    .Show()
		'                '    'End With

		'                '    '============================================

		'                '    Dim doctoprint2 As New System.Drawing.Printing.PrintDocument()
		'                '    CrDoc.SetDataSource(Ds)
		'                '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
		'                '    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & arrKdBarangSrap(CmbSisaProduksi.SelectedIndex) & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print_scrap & "' "
		'                '    CrDoc.PrintOptions.PrinterName = PrinterBarcodeQC

		'                '    doctoprint2.PrinterSettings.PrinterName = PrinterBarcodeQC

		'                '    Dim rawKind2 As Integer
		'                '    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
		'                '    For i = 0 To doctoprint2.PrinterSettings.PaperSizes.Count - 1
		'                '        If doctoprint2.PrinterSettings.PaperSizes(i).PaperName = KertasKecil Then
		'                '            rawKind2 = CInt(doctoprint2.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint2.PrinterSettings.PaperSizes(i)))
		'                '            CrDoc.PrintOptions.PaperSize = rawKind2
		'                '            Exit For
		'                '        End If
		'                '    Next

		'                '    CrDoc.PrintToPrinter(1, False, 1, 2500)
		'                'Else
		'                '    MessageBox.Show("Printer QC Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

		'                'End If

		'            End If
		'        End Using

		'        CloseConn()
		'    Catch ex As Exception
		'        CloseConn()
		'        MessageBox.Show(ex.Message)
		'        Exit Sub
		'    End Try

		'End If

#End Region

		Dim TanyaInput As String = MessageBox.Show("Lanjut Input . . ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
		If TanyaInput = vbYes Then
			kosong()
		Else
			N_EMI_Transaksi_Trial_Good_Received.Kosong()
			kosong()
			Me.Close()
		End If

	End Sub

	Private Sub Dgv_Batch_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Batch.CellEndEdit
		If Dgv_Batch.Rows.Count = 0 Then Exit Sub

		Dim rowIdx As Integer = Dgv_Batch.CurrentRow.Index
		Dim colIdx As Integer = Dgv_Batch.CurrentCell.ColumnIndex

		Dim isChecked As Boolean = Equals(Dgv_Batch.Rows(rowIdx).Cells(1).Value, True)

		If Not IsNumeric(Dgv_Batch.Rows(rowIdx).Cells(2).Value) AndAlso isChecked Then
			Dgv_Batch.Rows(rowIdx).Cells(2).Value = ""
		End If

		If isChecked Then
			Dim focusBatch As String = Dgv_Batch.Rows(rowIdx).Cells(0).Value?.ToString()

			Dgv_Batch.Rows(rowIdx).Cells(2).ReadOnly = False
			Dgv_Batch.Rows(rowIdx).Cells(4).Value = $"{focusBatch}-1"

			Dim targetIdx As Integer = ArrDataJenis.FindIndex(Function(x) x.Kode_Warna = "KUNING")
			If targetIdx <> -1 Then
				Dim cbo = DirectCast(Dgv_Batch.Rows(rowIdx).Cells(3), DataGridViewComboBoxCell)
				Dgv_Batch.Rows(rowIdx).Cells(3).Value = cbo.Items(targetIdx)
			End If
		Else

			Dgv_Batch.Rows(rowIdx).Cells(2).Value = ""
			Dgv_Batch.Rows(rowIdx).Cells(2).ReadOnly = True
			Dgv_Batch.Rows(rowIdx).Cells(3).Value = Nothing
			Dgv_Batch.Rows(rowIdx).Cells(4).Value = ""
		End If

		get_packaging()

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

	Private Sub Txt_Troli_KeyPress(sender As Object, e As KeyPressEventArgs)

		If Not (Char.IsDigit(e.KeyChar) OrElse e.KeyChar = Chr(8) OrElse e.KeyChar = "-"c) Then
			e.KeyChar = Chr(0)
		End If
	End Sub

	Private Sub TxtJmlScrap_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtJmlScrap.KeyPress
		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)

	End Sub

	Protected Overrides Sub WndProc(ByRef m As Message)
		' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
		If m.Msg = &HA3 Then
			Return  ' Abaikan pesan, sehingga form tidak maximize
		End If

		MyBase.WndProc(m)
	End Sub

End Class