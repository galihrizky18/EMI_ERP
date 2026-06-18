Public Class SD_ValidasiGR_Detail_Packaging

	Dim judulForm As String = "Validasi GR Packaging"

	Public No_Split, Kd_Barang As String
	Public JumlahInput As Double

	Private Sub SD_ValidasiGR_Detail_Packaging_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Kosog()
	End Sub

	Private Sub Kosog()

		Lv_Data.Columns.Clear() : Lv_Data.Items.Clear()
		Lv_Data.Columns.Add("Kode Stock Owner", 130, HorizontalAlignment.Left)
		Lv_Data.Columns.Add("Kode Barang", 110, HorizontalAlignment.Left)
		Lv_Data.Columns.Add("Jumlah Stock", 120, HorizontalAlignment.Right)
		Lv_Data.Columns.Add("Jumlah", 120, HorizontalAlignment.Right)
		Lv_Data.Columns.Add("Satuan", 70, HorizontalAlignment.Center)
		Lv_Data.Columns.Add("No Split", 120, HorizontalAlignment.Center)
		Lv_Data.View = View.Details

		Lv_Data.Columns(5).DisplayIndex = 0

		LoadData()

	End Sub

	Private Sub LoadData()
		If No_Split.Trim.Length = 0 Or Kd_Barang.Trim.Length = 0 Then
			MessageBox.Show("Terdapat Data yang Tidak Lengkap", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Try
			OpenConn()

			Dim kd_inq As String = ""
			SQL = "select top(1) kode_barang_inq from barang a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.kode_barang = '" & Kd_Barang & "'  "
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

			Dim arrPackaging As New List(Of (KodeStockOwner As String, KodeBahan As String, JumlahStock As Double, PackagingDigunakan As Double,
								Satuan As String, NoTransaksi As String, isPembulatan As Boolean))

			Lv_Data.Items.Clear()

#Region "Kode Lama"

			'SQL = "select a.No_Transaksi, sum(d.Jumlah) as Jumlah, "
			'SQL = SQL & "isnull(( select z.kode_barang_inq from barang z "
			'SQL = SQL & "where a.kode_perusahaan = z.kode_perusahaan and a.kode_stock_owner = z.kode_stock_owner and a.kode_barang = z.kode_barang ), "
			'SQL = SQL & "'-') as Kode_Barang, b.Kode_Stock_Owner, b.Kode_Barang as Kode_Bahan, c.Nama , "
			'SQL = SQL & "isnull(( select SUM(z.Jumlah) from Barang_SN z "
			'SQL = SQL & "where b.Kode_Perusahaan = z.Kode_Perusahaan and b.Kode_Stock_Owner = z.Kode_Stock_Owner and b.Kode_Barang = z.Kode_Barang  "
			'SQL = SQL & "), 0) as JumlahStock, b.Jumlah, b.Satuan, c.flag_potong_stok, "
			'SQL = SQL & "isnull(c.standar_price,0) as standar_price, isnull(c.Flag_Pembulatan_Produksi,'T') as Flag_Pembulatan_Produksi, "
			'SQL = SQL & "b.jumlah_barang, b.Jumlah_Bahan "
			'SQL = SQL & "from Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Packaging b, barang c, N_EMI_Validation_GR_Temp d  "
			'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
			'SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
			'SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Transaksi = d.No_Production_Order "
			'SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
			'SQL = SQL & "and a.no_transaksi in (" & No_Split & ") "
			'SQL = SQL & "and a.Status is null "
			'SQL = SQL & "and b.Kode_Barang not in ( "
			'SQL = SQL & "select z.Kode_Bahan from Barang_Detail_Bahan_Penolong z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_Barang = '" & kd_inq & "' and jenis = 'KEMASAN UTAMA') "
			'SQL = SQL & "group by a.Kode_Perusahaan, b.Kode_Perusahaan, c.Standar_Price, c.Flag_Pembulatan_Produksi, a.Kode_Stock_Owner, a.Kode_Barang, a.No_Transaksi, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama , b.Jumlah, b.Satuan, c.flag_potong_stok, b.jumlah_barang, b.Jumlah_Bahan "
			'SQL = SQL & "order by c.nama "

#End Region

			SQL = "select a.No_Transaksi, sum(d.Jumlah) as Jumlah, "
			SQL = SQL & "isnull(( select z.kode_barang_inq from barang z "
			SQL = SQL & "where a.kode_perusahaan = z.kode_perusahaan and a.kode_stock_owner = z.kode_stock_owner and a.kode_barang = z.kode_barang ), "
			SQL = SQL & "'-') as Kode_Barang, b.Kode_Stock_Owner, b.Kode_Barang as Kode_Bahan, c.Nama , "
			SQL = SQL & "isnull(( select SUM(z.Jumlah) from Barang_SN z "
			SQL = SQL & "where b.Kode_Perusahaan = z.Kode_Perusahaan and b.Kode_Stock_Owner = z.Kode_Stock_Owner and b.Kode_Barang = z.Kode_Barang  "
			SQL = SQL & "), 0) as JumlahStock, b.Satuan, c.flag_potong_stok, "
			SQL = SQL & "isnull(c.standar_price,0) as standar_price, isnull(c.Flag_Pembulatan_Produksi,'T') as Flag_Pembulatan_Produksi, "
			SQL = SQL & "b.jumlah_barang, b.Jumlah_Bahan "
			SQL = SQL & "from Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Packaging_Packing_Set b, barang c, N_EMI_Validation_GR_Temp d  "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
			SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
			SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Transaksi = d.No_Production_Order "
			SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.no_transaksi in (" & No_Split & ") "
			SQL = SQL & "and a.Status is null "
			SQL = SQL & "and b.Jenis <> 'KEMASAN UTAMA' "
			SQL = SQL & "and b.Deskripsi = d.Deskripsi "
			SQL = SQL & "group by a.Kode_Perusahaan, b.Kode_Perusahaan, c.Standar_Price, c.Flag_Pembulatan_Produksi, a.Kode_Stock_Owner, a.Kode_Barang, a.No_Transaksi, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama , b.Jumlah, b.Satuan, c.flag_potong_stok, b.jumlah_barang, b.Jumlah_Bahan "
			SQL = SQL & "order by c.nama "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							''================================
							''=     GET JUMLAH KEBUTUHAN     =
							''================================
							Dim KebutuhanBarang As Double = 0
							Dim KebutuhanBahan As Double = 0
							'SQL = "select a.Kode_Bahan, a.Jumlah_Bahan, a.Jumlah_Barang "
							'SQL = SQL & "from Barang_Detail_Bahan_Penolong a "
							'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
							'SQL = SQL & "and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
							'SQL = SQL & "and Kode_Bahan = '" & .Rows(i).Item("Kode_Bahan") & "' "
							'Using Dr = OpenTrans(SQL)
							'    If Dr.Read Then
							'        KebutuhanBarang = Val(HilangkanTanda(Format(Dr("Jumlah_Barang"), "N4")))
							'        KebutuhanBahan = Val(HilangkanTanda(Format(Dr("Jumlah_Bahan"), "N4")))
							'    End If
							'End Using

							'Dim PackagingDigunakan As Double = Val(HilangkanTanda(Format((Val(HilangkanTanda(JumlahInput)) / KebutuhanBarang) * KebutuhanBahan, "N4")))

							KebutuhanBarang = Val(HilangkanTanda(Format(.Rows(i).Item("Jumlah_Barang"), "N4")))
							KebutuhanBahan = Val(HilangkanTanda(Format(.Rows(i).Item("Jumlah_Bahan"), "N4")))

							'Hitung Kebutuhan Packaging Untuk 1 Data
							'Dim PackagingDigunakan As Double = Val(HilangkanTanda(Format((Val(HilangkanTanda(JumlahInput)) / KebutuhanBarang) * KebutuhanBahan, "N4")))
							Dim PackagingDigunakan As Double = Val(HilangkanTanda(Format((Val(HilangkanTanda(.Rows(i).Item("Jumlah"))) / KebutuhanBarang) * KebutuhanBahan, "N4")))

							Dim isPembulatan As Boolean = False
							If .Rows(i).Item("Flag_Pembulatan_Produksi") = "Y" Then
								'PackagingDigunakan = Math.Ceiling(PackagingDigunakan)
								isPembulatan = True
							End If

							Dim targetData = arrPackaging.FirstOrDefault(Function(x) x.NoTransaksi = .Rows(i).Item("No_Transaksi") And x.KodeBahan = .Rows(i).Item("Kode_Bahan"))

							If targetData.KodeBahan IsNot Nothing Then
								Dim index As Integer = arrPackaging.IndexOf(targetData)

								arrPackaging(index) = (
									targetData.KodeStockOwner,
									targetData.KodeBahan,
									targetData.JumlahStock + PackagingDigunakan,
									targetData.PackagingDigunakan,
									targetData.Satuan,
									targetData.NoTransaksi,
									targetData.isPembulatan
								)
							Else
								arrPackaging.Add((.Rows(i).Item("Kode_Stock_Owner"), .Rows(i).Item("Kode_Bahan"),
												 Format(.Rows(i).Item("JumlahStock"), "N2"), PackagingDigunakan,
												 .Rows(i).Item("Satuan"), .Rows(i).Item("No_Transaksi"), isPembulatan))
							End If

							'Dim Lv As ListViewItem
							'Lv = Lv_Data.Items.Add(.Rows(i).Item("Kode_Stock_Owner"))
							'Lv.SubItems.Add(.Rows(i).Item("Kode_Bahan"))
							'Lv.SubItems.Add(Format(.Rows(i).Item("JumlahStock"), "N2"))
							'Lv.SubItems.Add(Format(PackagingDigunakan, "N2"))
							'Lv.SubItems.Add(.Rows(i).Item("Satuan"))
							'Lv.SubItems.Add(.Rows(i).Item("No_Transaksi"))

						Next
					End If
				End With
			End Using

			For Each item In arrPackaging

				Dim PackagingDigunakanDisplay As Double = 0

				If item.isPembulatan Then
					PackagingDigunakanDisplay = Math.Ceiling(item.PackagingDigunakan)
				Else
					PackagingDigunakanDisplay = item.PackagingDigunakan
				End If

				Dim Lv As ListViewItem
				Lv = Lv_Data.Items.Add(item.KodeStockOwner)
				Lv.SubItems.Add(item.KodeBahan)
				Lv.SubItems.Add(Format(item.JumlahStock, "N2"))
				Lv.SubItems.Add(Format(PackagingDigunakanDisplay, "N2"))
				Lv.SubItems.Add(item.Satuan)
				Lv.SubItems.Add(item.NoTransaksi)
			Next

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

End Class