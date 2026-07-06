Imports System.IO
Imports System.Net

Public Class Server_Sinkronasi_B2B

	Dim arrId_Proyeks, arrKd_Brg, arrSO, arrNo_Rab, arrNo_Fak, arrEdit, arrHapus, arrKdSupplier, arrNo_Fak2, arrNoUrut As New ArrayList

	Dim arrNo_PO, arrNoPo2, arrKodeSupplier, arrKodePerusahanBiayaImport, arrNoPenawaranPackaging, arrNoPenawaranBahanBaku, arrNoPoPembelianSelesai As New ArrayList

	Dim Faktur_Penawaran As String = ""

	Dim Random As New Random()

	Private Sub insert_sql_to_mysql_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		ListView1.Columns.Add("Error", 900, HorizontalAlignment.Left)
		ListView1.View = View.Details

		Dim Lvw As ListViewItem
		Lvw = ListView1.Items.Add("Sql-MySql : Tes")
	End Sub

	Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
		get_jam()
		Try
			OpenConn()
			OpenConnB2B()
			Cmd.Transaction = Cn.BeginTransaction
			CmdB2B.Transaction = CnB2B.BeginTransaction

			arrNo_PO.Clear()
			SQLB2B = "select no_faktur from B2B_Purchase_Order where status is null and flag_sudah_pindah is null and flag_selesai = 'Y'  order by no_faktur"
			Using DsSQL = BindingTransB2B(SQLB2B)
				With DsSQL.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrNo_PO.Add(.Rows(i).Item("no_faktur"))
					Next
				End With
			End Using

			SQLB2B = "select kode_perusahaan,no_faktur,no_do,lokasi,kode_supplier,Id_Kendaraan,Driver,ETD,eta,plat,telpon,tanggal,jam,Id_User,cara_kirim,harga "
			SQLB2B = SQLB2B & "from B2B_Purchase_Order where flag_sudah_pindah is null and status is null and flag_selesai = 'Y' order by No_Faktur  "
			Using DsSQL = BindingTransB2B(SQLB2B)
				With DsSQL.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1

						Dim no_do As String = ""
						If General_Class.CekNULL(.Rows(i).Item("no_do")) = "" Then
							no_do = "NULL,"
						Else
							no_do = "'" & .Rows(i).Item("no_do") & "',"
						End If

						SQL = "insert into emi_pembelian_loading(kode_perusahaan,no_faktur,no_sj,lokasi,kode_supplier,Driver,Tanggal_OTW,eta,No_Plat,telpon,tanggal,jam,UseriD,cara_kirim,biaya_perjalanan) "
						SQL = SQL & "values ('" & .Rows(i).Item("kode_perusahaan") & "','" & .Rows(i).Item("no_faktur") & "',"

						SQL = SQL & "" & no_do & " "

						SQL = SQL & "'" & .Rows(i).Item("lokasi") & "','" & .Rows(i).Item("kode_supplier") & "',"
						SQL = SQL & "'" & .Rows(i).Item("driver") & "', '" & Format(.Rows(i).Item("etd"), "yyyy-MM-dd") & "',"
						SQL = SQL & "'" & Format(.Rows(i).Item("eta"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("plat") & "','" & .Rows(i).Item("telpon") & "',"
						SQL = SQL & "'" & Format(.Rows(i).Item("tanggal"), "yyyy-MM-dd") & "','" & .Rows(i).Item("jam") & "','" & .Rows(i).Item("Id_User") & "',"
						SQL = SQL & "'" & .Rows(i).Item("cara_kirim") & "', '" & .Rows(i).Item("harga") & "' "
						SQL = SQL & ")"
						ExecuteTrans(SQL)
					Next
				End With
			End Using

			'==========================================
			' dapatkan total berat dalam gram
			'==========================================

			For z As Integer = 0 To arrNo_PO.Count - 1

				Dim pecahan As Double = 0
				Dim totalSeluruhBarangSatuanKecil As Double = 0

				'==========================================
				' dapatkan jumlah keseluruhan berat dalam gram
				'==========================================
				SQLB2B = "select a.Kode_Barang,  "
				SQLB2B = SQLB2B & "a.Qty_Kirim,a.Satuan, e.Berat, e.satuan as satuan_kecil, a.urut_po "
				SQLB2B = SQLB2B & "from B2B_Detail_Purchase_Order a,b2b_purchase_order c, barang e "
				SQLB2B = SQLB2B & "where a.Kode_Perusahaan = e.Kode_Perusahaan and a.Kode_Stock_Owner = e.Kode_Stock_Owner and a.Kode_Barang = e.Kode_Barang "
				SQLB2B = SQLB2B & "and a.kode_perusahaan = c.kode_perusahaan and a.No_Faktur = c.No_Faktur "
				SQLB2B = SQLB2B & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & arrNo_PO.Item(z).ToString & "' "
				SQLB2B = SQLB2B & "order by a.kode_barang  "
				Using DsB2B = BindingTransB2B(SQLB2B)
					With DsB2B.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1

							'==========================================
							' convert Satuan Besar ke satuan kecil
							'==========================================
							Dim jumlahConvert As Double = 0
							SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(i).Item("kode_barang") & "',"
							SQL = SQL & "'" & .Rows(i).Item("satuan") & "','" & .Rows(i).Item("satuan_kecil") & "',"
							SQL = SQL & "" & .Rows(i).Item("qty_kirim") & ") as Hasil "
							Using dr4 = OpenTrans(SQL)
								If dr4.Read Then
									If General_Class.CekNULL(dr4("Hasil")) <> "" Then
										If dr4("Hasil") = 0 Then
											MessageBox.Show("Satuan " & .Rows(i).Item("satuan") & " Ke " & .Rows(i).Item("satuan_kecil") & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											dr4.Close()
											CloseTrans()
											CloseTransB2B()
											CloseConn()
											CloseConnB2B()
											Exit Sub
										Else
											jumlahConvert = dr4("hasil")

										End If
									Else
										dr4.Close()
										CloseTrans()
										CloseTransB2B()
										CloseConn()
										CloseConnB2B()
										MessageBox.Show("Satuan " & .Rows(i).Item("satuan") & " Ke " & .Rows(i).Item("satuan_kecil") & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If
								End If
							End Using

							Dim totalSatuanKecilPerbarang As Double = 0

							totalSatuanKecilPerbarang = Val(jumlahConvert) * Val(.Rows(i).Item("berat"))

							totalSeluruhBarangSatuanKecil = totalSeluruhBarangSatuanKecil + totalSatuanKecilPerbarang

						Next
					End With
				End Using

				'==========================
				' Proses simpan ke EMI DB
				'=========================

				SQLB2B = "select  a.Kode_Perusahaan, c.harga as biaya_kirim	,a.No_Faktur,	a.No_PO,	a.Urut_PO,a.Kode_Stock_Owner,	a.Kode_Barang,d.Berat, "
				SQLB2B = SQLB2B & "b.Tanggal_Produksi,	b.Tanggal_Expired, b.Quantity,a.qty_order,	a.Satuan, b.urut_oto, b.No_Batch , d.Satuan as Satuan_Kecil "
				SQLB2B = SQLB2B & "from B2B_Detail_Purchase_Order a, B2B_Detail_Batch_Purchase_Order b, b2b_purchase_order c, barang d "
				SQLB2B = SQLB2B & "where a.kode_perusahaan = b.kode_perusahaan and a.No_Urut = b.No_Urut  "
				SQLB2B = SQLB2B & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Faktur = c.No_Faktur  "
				SQLB2B = SQLB2B & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Stock_Owner = d.Kode_Stock_Owner and a.Kode_Barang = d.Kode_Barang  "
				SQLB2B = SQLB2B & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & arrNo_PO.Item(z).ToString & "' "
				SQLB2B = SQLB2B & "order by a.kode_barang  "
				Using DsSQL = BindingTransB2B(SQLB2B)
					With DsSQL.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1

							Dim satuanBarang As String = ""
							Dim isi_Per_Bags As Double = 0
							Dim Satuan_Isi_Bags As String = ""
							SQL = "select satuan, isnull(Isi_Per_Bags,0) as Isi_Per_Bags, isnull(Satuan_Isi_Bags,'') as Satuan_Isi_Bags from barang where kode_perusahaan = '" & KodePerusahaan & "'  "
							SQL = SQL & "and kode_stock_owner = '" & .Rows(i).Item("kode_stock_owner") & "' "
							SQL = SQL & "and kode_barang = '" & .Rows(i).Item("kode_barang") & "' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									satuanBarang = Dr("satuan")
									isi_Per_Bags = Dr("isi_Per_Bags")
									Satuan_Isi_Bags = Dr("Satuan_Isi_Bags")
								Else
									CloseTrans()
									CloseTransB2B()
									CloseConn()
									CloseConnB2B()
									MessageBox.Show("error insert purchase order, satuan barang tidak ditemukan")
									Exit Sub
								End If
							End Using

							'==========================================
							' convert jumlah ke satuan kecil
							'==========================================
							Dim jumlahBarangDibutuhkan As Double = 0
							SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(i).Item("kode_barang") & "',"
							SQL = SQL & "'" & .Rows(i).Item("satuan") & "','" & satuanBarang & "',"
							SQL = SQL & "" & .Rows(i).Item("Quantity") & ") as Hasil "
							Using dr4 = OpenTrans(SQL)
								If dr4.Read Then
									If General_Class.CekNULL(dr4("Hasil")) <> "" Then
										If dr4("Hasil") = 0 Then
											MessageBox.Show("Satuan " & .Rows(i).Item("satuan") & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											dr4.Close()
											CloseTrans()
											CloseTransB2B()
											CloseConn()
											CloseConnB2B()
											Exit Sub
										Else
											jumlahBarangDibutuhkan = dr4("hasil")

										End If
									Else
										dr4.Close()
										CloseTrans()
										CloseTransB2B()
										CloseConn()
										CloseConnB2B()
										MessageBox.Show("Satuan " & .Rows(i).Item("satuan") & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										Exit Sub
									End If
								End If
							End Using

							Dim hargaPOSatuanKecil As Double = 0
							Dim hargaPOSatuanDisplay As Double = 0

							SQLB2B = "select harga_barang,harga From EMI_Pembelian_PO_Detail where No_Urut = " & .Rows(i).Item("urut_po") & " "
							Using DrB2B = OpenTransB2B(SQLB2B)
								If DrB2B.Read Then
									hargaPOSatuanKecil = DrB2B("harga_barang")
									hargaPOSatuanDisplay = DrB2B("harga")
								Else
									DrB2B.Close()
									CloseTrans()
									CloseTransB2B()
									CloseConn()
									CloseConnB2B()
									MessageBox.Show("Harga Penawaran Tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

							'==========================================
							' select satuan display barang
							'==========================================
							Dim satuanTampilDisplay As String

							SQL = "select satuan from barang_detail_satuan where kode_perusahaan = '" & KodePerusahaan & "' and kode_barang = '" & .Rows(i).Item("kode_barang") & "' "
							SQL = SQL & "and flag_tampil_display = 'Y' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									satuanTampilDisplay = Dr("satuan")
								Else
									Dr.Close()
									CloseTrans()
									CloseTransB2B()
									CloseConn()
									CloseConnB2B()
									MessageBox.Show("Detail satuan barang tampil display tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

							'Dim biayaPerGram As Double = 0
							Dim totalHppPerBarang As Double = 0
							'Dim biayaKirimPerbarang As Double = 0

							'biayaPerGram = Val(.Rows(i).Item("biaya_kirim")) / Val(totalSeluruhBarangSatuanKecil)

							'biayaKirimPerbarang = .Rows(i).Item("berat") * biayaPerGram

							'SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','UANG','" & .Rows(i).Item("kode_barang") & "',"
							'SQL = SQL & "'" & .Rows(i).Item("satuan_kecil") & "','" & satuanTampilDisplay & "',"
							'SQL = SQL & "" & biayaKirimPerbarang & ") as Hasil "
							'Using dr4 = OpenTrans(SQL)
							'    If dr4.Read Then
							'        If General_Class.CekNULL(dr4("Hasil")) <> "" Then
							'            'If dr4("Hasil") = 0 Then
							'            '    MessageBox.Show("Satuan " & .Rows(i).Item("satuan") & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'            '    dr4.Close()
							'            '    CloseTrans()
							'            '    CloseTransB2B()
							'            '    CloseConn()
							'            '    CloseConnB2B()
							'            '    Exit Sub
							'            'Else
							'            biayaKirimPerbarang = dr4("hasil")

							'            'End If
							'        Else
							'            dr4.Close()
							'            CloseTrans()
							'            CloseTransB2B()
							'            CloseConn()
							'            CloseConnB2B()
							'            MessageBox.Show("Satuan " & .Rows(i).Item("satuan") & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'            Exit Sub
							'        End If
							'    End If
							'End Using

							totalHppPerBarang = Val(hargaPOSatuanDisplay)

							SQL = "insert into EMI_Pembelian_Loading_Detail (kode_perusahaan,no_faktur,no_po,Urut_PO,Kode_Stock_Owner,Kode_Barang, "
							SQL = SQL & "Tanggal_Produksi,Tanggal_Expired,No_Urut_B2B,Jumlah,satuan,Jumlah_Barang, jumlah_masuk,satuan_barang,jumlah_per_bag, No_Batch, Satuan_Per_Bag,harga_barang, hpp_satuan_display ) "
							SQL = SQL & "values ('" & .Rows(i).Item("kode_perusahaan") & "','" & .Rows(i).Item("no_faktur") & "','" & .Rows(i).Item("no_po") & "',"
							SQL = SQL & "'" & .Rows(i).Item("urut_po") & "','" & .Rows(i).Item("kode_stock_owner") & "',"
							SQL = SQL & "'" & .Rows(i).Item("kode_barang") & "','" & Format(.Rows(i).Item("Tanggal_Produksi"), "yyyy-MM-dd") & "','" & Format(.Rows(i).Item("Tanggal_Expired"), "yyyy-MM-dd") & "',"
							SQL = SQL & "'" & .Rows(i).Item("urut_oto") & "','" & .Rows(i).Item("Quantity") & "',"
							SQL = SQL & "'" & .Rows(i).Item("satuan") & "'," & HilangkanTanda(Format(jumlahBarangDibutuhkan, "N2")) & ", 0 , '" & satuanBarang & "',"
							SQL = SQL & "" & isi_Per_Bags & ", '" & .Rows(i).Item("No_Batch") & "', '" & Satuan_Isi_Bags & "', '" & hargaPOSatuanKecil & "', '" & totalHppPerBarang & "' )"
							ExecuteTrans(SQL)
						Next
					End With
				End Using

				SQLB2B = "Update B2B_Purchase_Order set flag_sudah_pindah = 'Y' where "
				SQLB2B = SQLB2B & "no_faktur = '" & arrNo_PO.Item(z).ToString & "' "
				ExecuteTransB2B(SQLB2B)

			Next

			'==========================================
			' Biaya / total berat
			'==========================================

			'CloseTrans()
			'CloseTransB2B()
			'CloseConn()
			'CloseConnB2B()
			'MessageBox.Show("Pesan Harus dihapus!")
			'Exit Sub

			'Dim j As Integer = 0
			'For z As Integer = 1 To arrNo_PO.Count
			'    SQLB2B = "select  a.Kode_Perusahaan,	a.No_Faktur,	a.No_PO,	a.Urut_PO,a.Kode_Stock_Owner,	a.Kode_Barang,	b.Tanggal_Produksi,	b.Tanggal_Expired, "
			'    SQLB2B = SQLB2B & "b.Quantity,a.qty_order,	a.Satuan, b.urut_oto, b.No_Batch "
			'    SQLB2B = SQLB2B & "from B2B_Detail_Purchase_Order a, B2B_Detail_Batch_Purchase_Order b "
			'    SQLB2B = SQLB2B & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and "
			'    SQLB2B = SQLB2B & "a.no_urut = b.no_urut "
			'    '  SQLB2B = SQLB2B & "and a.No_PO = b.No_PO and a.Kode_Barang  = b.Kode_Barang "
			'    SQLB2B = SQLB2B & "and a.no_faktur = '" & arrNo_PO.Item(j).ToString & "'"

			'    Using DsSQL = BindingTransB2B(SQLB2B)
			'        With DsSQL.Tables("MyTable")
			'            For i As Integer = 0 To .Rows.Count - 1

			'                Dim satuanBarang As String = ""
			'                Dim isi_Per_Bags As Double = 0
			'                Dim Satuan_Isi_Bags As String = ""
			'                SQL = "select satuan, isnull(Isi_Per_Bags,0) as Isi_Per_Bags, isnull(Satuan_Isi_Bags,'') as Satuan_Isi_Bags from barang where kode_perusahaan = '" & KodePerusahaan & "'  "
			'                SQL = SQL & "and kode_stock_owner = '" & .Rows(i).Item("kode_stock_owner") & "' "
			'                SQL = SQL & "and kode_barang = '" & .Rows(i).Item("kode_barang") & "' "
			'                Using Dr = OpenTrans(SQL)
			'                    If Dr.Read Then
			'                        satuanBarang = Dr("satuan")
			'                        isi_Per_Bags = Dr("isi_Per_Bags")
			'                        Satuan_Isi_Bags = Dr("Satuan_Isi_Bags")
			'                    Else
			'                        CloseTrans()
			'                        CloseTransB2B()
			'                        CloseConn()
			'                        CloseConnB2B()
			'                        MessageBox.Show("error insert purchase order, satuan barang tidak ditemukan")
			'                        Exit Sub
			'                    End If
			'                End Using

			'                '==========================================
			'                ' convert jumlah ke satuan kecil
			'                '==========================================
			'                Dim jumlahBarangDibutuhkan As Double = 0
			'                SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(i).Item("kode_barang") & "',"
			'                SQL = SQL & "'" & .Rows(i).Item("satuan") & "','" & satuanBarang & "',"
			'                SQL = SQL & "" & .Rows(i).Item("Quantity") & ") as Hasil "
			'                Using dr4 = OpenTrans(SQL)
			'                    If dr4.Read Then
			'                        If General_Class.CekNULL(dr4("Hasil")) <> "" Then
			'                            If dr4("Hasil") = 0 Then
			'                                MessageBox.Show("Satuan " & .Rows(i).Item("satuan") & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                                dr4.Close()
			'                                CloseTrans()
			'                                CloseTransB2B()
			'                                CloseConn()
			'                                CloseConnB2B()
			'                                Exit Sub
			'                            Else
			'                                jumlahBarangDibutuhkan = dr4("hasil")

			'                            End If
			'                        Else
			'                            dr4.Close()
			'                            CloseTrans()
			'                            CloseTransB2B()
			'                            CloseConn()
			'                            CloseConnB2B()
			'                            MessageBox.Show("Satuan " & .Rows(i).Item("satuan") & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                            Exit Sub
			'                        End If
			'                    End If
			'                End Using

			'                '==========================================
			'                ' convert harga ke satuan kecil
			'                '==========================================

			'                Dim hargaPO As Double = 0

			'                SQL = "select harga_barang From EMI_Pembelian_PO_Detail where No_Urut = " & .Rows(i).Item("urut_po") & " "
			'                Using Dr5 = OpenTrans(SQL)
			'                    If Dr5.Read Then
			'                        hargaPO = Dr5("harga_barang")
			'                    Else
			'                        Dr5.Close()
			'                        CloseTrans()
			'                        CloseTransB2B()
			'                        CloseConn()
			'                        CloseConnB2B()
			'                        MessageBox.Show("Harga Penawaran Tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'                        Exit Sub
			'                    End If
			'                End Using

			'                SQL = "insert into EMI_Pembelian_Loading_Detail (kode_perusahaan,no_faktur,no_po,Urut_PO,Kode_Stock_Owner,Kode_Barang, "
			'                SQL = SQL & "Tanggal_Produksi,Tanggal_Expired,No_Urut_B2B,Jumlah,satuan,Jumlah_Barang, jumlah_masuk,satuan_barang,jumlah_per_bag, No_Batch, Satuan_Per_Bag,harga_barang ) "
			'                SQL = SQL & "values ('" & .Rows(i).Item("kode_perusahaan") & "','" & .Rows(i).Item("no_faktur") & "','" & .Rows(i).Item("no_po") & "',"
			'                SQL = SQL & "'" & .Rows(i).Item("urut_po") & "','" & .Rows(i).Item("kode_stock_owner") & "',"
			'                SQL = SQL & "'" & .Rows(i).Item("kode_barang") & "','" & .Rows(i).Item("Tanggal_Produksi") & "','" & .Rows(i).Item("Tanggal_Expired") & "',"
			'                SQL = SQL & "'" & .Rows(i).Item("urut_oto") & "','" & .Rows(i).Item("Quantity") & "',"
			'                SQL = SQL & "'" & .Rows(i).Item("satuan") & "'," & HilangkanTanda(Format(jumlahBarangDibutuhkan, "N2")) & ", 0 , '" & satuanBarang & "',"
			'                SQL = SQL & "" & isi_Per_Bags & ", '" & .Rows(i).Item("No_Batch") & "', '" & Satuan_Isi_Bags & "', '" & hargaPO & "' )"
			'                ExecuteTrans(SQL)
			'            Next
			'        End With
			'    End Using

			'    SQLB2B = "Update B2B_Purchase_Order set flag_sudah_pindah = 'Y' where "
			'    SQLB2B = SQLB2B & "no_faktur = '" & arrNo_PO.Item(j).ToString & "' "
			'    ExecuteTransB2B(SQLB2B)

			'    j = j + 1
			'Next

			'SQLB2B = "select  a.Kode_Perusahaan,	a.No_Faktur,	a.No_PO,	a.Urut_PO,a.Kode_Stock_Owner,	a.Kode_Barang,	b.Tanggal_Produksi,	b.Tanggal_Expired,a.Qty_Kirim,	a.Satuan "
			'SQLB2B = SQLB2B & "from B2B_Purchase_Order where flag_sudah_pindah is null and status is null order by No_Faktur  "
			'Using DsSQL = BindingTransB2B(SQLB2B)
			'    With DsSQL.Tables("MyTable")
			'        For i As Integer = 0 To .Rows.Count - 1
			'            SQL = "insert into emi_pembelian_loading(kode_perusahaan,no_faktur,no_sj,lokasi,kode_supplier,Id_Ekspedisi,Driver,ETD,eta,No_Plat,telpon,tanggal,jam,UseriD) "
			'            SQL = SQL & "values ('" & .Rows(i).Item("kode_perusahaan") & "','" & .Rows(i).Item("no_faktur") & "',"
			'            SQL = SQL & "'" & .Rows(i).Item("lokasi") & "','" & .Rows(i).Item("kode_supplier") & "','" & .Rows(i).Item("id_kendaraan") & "',"
			'            SQL = SQL & "'" & .Rows(i).Item("driver") & "', '" & Format(.Rows(i).Item("etd"), "yyyy-MM-dd") & "',"
			'            SQL = SQL & "'" & Format(.Rows(i).Item("eta"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("plat") & "','" & .Rows(i).Item("telpon") & "',"
			'            SQL = SQL & "'" & Format(.Rows(i).Item("tanggal"), "yyyy-MM-dd") & "','" & .Rows(i).Item("jam") & "','" & .Rows(i).Item("Id_User") & "')"
			'            ExecuteTrans(SQL)
			'        Next
			'    End With
			'End Using

			'MessageBox.Show("simpan berhasil", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmd.Transaction.Commit()
			CmdB2B.Transaction.Commit()
			CloseConn()
			CloseConnB2B()
		Catch ex As Exception
			CloseTrans()
			CloseTransB2B()
			CloseConn()
			CloseConnB2B()
			MessageBox.Show(ex.Message & "insert purchase order")
			Exit Sub
		End Try

	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		get_jam()
		Try
			OpenConn()
			OpenConnB2B()
			Cmd.Transaction = Cn.BeginTransaction
			CmdB2B.Transaction = CnB2B.BeginTransaction

			arrNo_PO.Clear()

			SQL = "select a.no_faktur from emi_pembelian_po a, suppliers b, suppliers_kategori c where "
			SQL = SQL & "a.kode_Perusahaan=b.kode_Perusahaan and a.Kode_supplier=b.kode_supplier and "
			SQL = SQL & "b.kode_Perusahaan=c.kode_Perusahaan and b.id_kategori_suppliers=c.id_kategori_suppliers and "
			SQL = SQL & "a.flag_sudah_pindah is null and c.flag_jenis_lokal='Y'  "
			SQL = SQL & "order by no_faktur"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrNo_PO.Add(.Rows(i).Item("no_faktur"))
					Next
				End With
			End Using

			Dim j As Integer = 0
			For z As Integer = 1 To arrNo_PO.Count

				SQL = " select kode_Perusahaan, no_faktur, No_Nota, Tanggal, jam, UserID, Kode_Supplier, lokasi, Jenis_Pembayaran, Cara_Bayar, "
				SQL = SQL & "Tgl_Jatuh_Tempo, Total_MUA, Mata_Uang, kurs, Total_IDR, Grand_Sebelum_PPN, ppn, grand, ETD_Simulasi, Ekspedisi, "
				SQL = SQL & "Biaya, Flag_Release, Tanggal_Release, Jam_Release, User_Release from emi_pembelian_po where "
				SQL = SQL & "flag_sudah_pindah is null and status is null and no_faktur = '" & arrNo_PO.Item(j).ToString & "'  "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1

							SQLB2B = "insert into emi_pembelian_po(kode_Perusahaan, no_faktur, No_Nota, Tanggal, jam, UserID, Kode_Supplier, Lokasi, Jenis_Pembayaran, Cara_Bayar, "
							SQLB2B = SQLB2B & "Tgl_Jatuh_Tempo, Total_MUA, mata_uang, kurs, Total_IDR, Grand_Sebelum_PPN, PPN, grand, ETD_Simulasi, Ekspedisi, "
							SQLB2B = SQLB2B & "Biaya, Flag_Release, Tanggal_Release, Jam_Release, User_Release) values ("
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("kode_Perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "','" & .Rows(i).Item("No_Nota") & "','" & Format(.Rows(i).Item("Tanggal"), "yyyy-MM-dd") & "',"
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("jam") & "', '" & .Rows(i).Item("UserID") & "',"
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Kode_Supplier") & "', '" & .Rows(i).Item("Lokasi") & "','" & .Rows(i).Item("Jenis_Pembayaran") & "',"

							If General_Class.CekNULL(.Rows(i).Item("Cara_Bayar")) = "" Then
								SQLB2B = SQLB2B & "NULL, "
							Else
								SQLB2B = SQLB2B & "'" & .Rows(i).Item("Cara_Bayar") & "', "
							End If

							If General_Class.CekNULL(.Rows(i).Item("Tgl_Jatuh_Tempo")) = "" Then
								SQLB2B = SQLB2B & "NULL, "
							Else
								SQLB2B = SQLB2B & "'" & .Rows(i).Item("Tgl_Jatuh_Tempo") & "', "
							End If

							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Total_MUA") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("mata_uang") & "','" & .Rows(i).Item("kurs") & "','" & .Rows(i).Item("Total_IDR") & "',"
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Grand_Sebelum_PPN") & "','" & .Rows(i).Item("PPN") & "','" & .Rows(i).Item("grand") & "',"
							SQLB2B = SQLB2B & "'" & Format(.Rows(i).Item("ETD_Simulasi"), "yyyy-MM-dd") & "','" & .Rows(i).Item("Ekspedisi") & "','" & .Rows(i).Item("Biaya") & "',"

							If General_Class.CekNULL(.Rows(i).Item("Flag_Release")) = "" Then
								SQLB2B = SQLB2B & "NULL,NULL,NULL,NULL)"
							Else
								SQLB2B = SQLB2B & "'" & .Rows(i).Item("Flag_Release") & "','" & Format(.Rows(i).Item("Tanggal_Release"), "yyyy-MM-dd") & "','" & .Rows(i).Item("Jam_Release") & "',"
								SQLB2B = SQLB2B & "'" & .Rows(i).Item("User_Release") & "')"
							End If

							ExecuteTransB2B(SQLB2B)

						Next
					End With
				End Using

				SQL = " select kode_Perusahaan, no_faktur, Kode_Stock_Owner, Kode_Barang, No_Urut, Jumlah, Satuan, Harga, "
				SQL = SQL & "Nilai_Barang, Satuan_Barang, Harga_Barang, Total, No_Penawaran, Flag_Prepare from "
				SQL = SQL & "EMI_Pembelian_PO_detail where "
				SQL = SQL & " no_faktur = '" & arrNo_PO.Item(j).ToString & "'"

				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							SQLB2B = " insert into EMI_Pembelian_PO_detail(kode_Perusahaan, no_faktur, Kode_Stock_Owner, Kode_Barang, "
							SQLB2B = SQLB2B & "No_Urut, Jumlah, Satuan, Harga, Nilai_Barang, Satuan_Barang, Harga_Barang, "
							SQLB2B = SQLB2B & "Total, No_Penawaran, Flag_Prepare) values ('" & .Rows(i).Item("kode_Perusahaan") & "','" & .Rows(i).Item("no_faktur") & "', "
							SQLB2B = SQLB2B & " '" & .Rows(i).Item("Kode_Stock_Owner") & "','" & .Rows(i).Item("Kode_Barang") & "',"
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("No_Urut") & "','" & .Rows(i).Item("Jumlah") & "',"
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Satuan") & "','" & .Rows(i).Item("Harga") & "','" & .Rows(i).Item("Nilai_Barang") & "',"
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Satuan_Barang") & "','" & .Rows(i).Item("Harga_Barang") & "',"
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Total") & "','" & .Rows(i).Item("No_Penawaran") & "', "
							SQLB2B = SQLB2B & "'T' )"
							ExecuteTransB2B(SQLB2B)
						Next
					End With
				End Using

				SQL = "Update EMI_Pembelian_PO set flag_sudah_pindah = 'Y' where "
				SQL = SQL & "no_faktur = '" & arrNo_PO.Item(j).ToString & "' "
				ExecuteTrans(SQL)

				j = j + 1
			Next

			Cmd.Transaction.Commit()
			CmdB2B.Transaction.Commit()
			CloseConn()
			CloseConnB2B()
		Catch ex As Exception
			CloseTrans()
			CloseTransB2B()
			CloseConn()
			CloseConnB2B()
			MessageBox.Show(ex.Message & "insert purchase order")
			Exit Sub
		End Try
	End Sub

	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		Try
			OpenConn()
			OpenConnB2B()
			Cmd.Transaction = Cn.BeginTransaction
			CmdB2B.Transaction = CnB2B.BeginTransaction

			arrKd_Brg.Clear() : arrSO.Clear()
			SQL = "select Kode_Barang, Kode_Stock_Owner from barang where "
			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
			SQL = SQL & "flag_sudah_pindah is null"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrKd_Brg.Add(.Rows(i).Item("Kode_Barang"))
						arrSO.Add(.Rows(i).Item("Kode_Stock_Owner"))
					Next
				End With
			End Using

			Dim j As Integer = 0
			For z As Integer = 1 To arrSO.Count

				SQL = "Select Kode_Perusahaan,Kode_Barang,Kode_Stock_Owner,Nama,Satuan,good_stock,kode_kategori_besar,kode_kategori_kecil, "
				SQL = SQL & "id_routing,metode_pengeluaran_stok,id_group_Jenis, berat "
				SQL = SQL & "from barang where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "Kode_Stock_Owner = '" & arrSO.Item(j).ToString & "' and "
				SQL = SQL & "Kode_Barang = '" & arrKd_Brg.Item(j).ToString & "' and "
				SQL = SQL & "flag_sudah_pindah is null"
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							'Insert ke MySql
							SQLB2B = "insert into barang (kode_perusahaan,kode_barang,kode_stock_owner,nama,satuan,good_stock,kode_kategori_besar,kode_kategori_kecil, "
							SQLB2B = SQLB2B & "id_routing,metode_pengeluaran_stok,id_group_jenis, flag_sendiri, berat "
							SQLB2B = SQLB2B & ")"
							SQLB2B = SQLB2B & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("Kode_Barang") & "',"
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Kode_Stock_Owner") & "', '" & .Rows(i).Item("Nama") & "', "
							SQLB2B = SQLB2B & " '" & .Rows(i).Item("Satuan") & "', '" & .Rows(i).Item("good_stock") & "', '" & .Rows(i).Item("kode_kategori_besar") & "' , '" & .Rows(i).Item("kode_kategori_kecil") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("id_routing") & "' , '" & .Rows(i).Item("metode_pengeluaran_stok") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("id_group_jenis") & "', 'Y', '" & .Rows(i).Item("berat") & "' "
							SQLB2B = SQLB2B & ")"
							ExecuteTransB2B(SQLB2B)

						Next
					End With
				End Using

				SQL = "Update barang set flag_sudah_pindah = 'Y' where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
				SQL = SQL & "Kode_Stock_Owner = '" & arrSO.Item(j).ToString & "' and "
				SQL = SQL & "Kode_Barang = '" & arrKd_Brg.Item(j).ToString & "' "
				ExecuteTrans(SQL)

				j = j + 1
			Next

			Cmd.Transaction.Commit()
			CmdB2B.Transaction.Commit()
			CloseConn()
			CloseConnB2B()
		Catch ex As Exception
			CloseTrans()
			CloseTransB2B()
			CloseConn()
			CloseConnB2B()
			MessageBox.Show(ex.Message & " insert barang")
			Exit Sub
		End Try
	End Sub

	Private Sub btnPnwrBahanBaku_Click(sender As Object, e As EventArgs) Handles btnPnwrBahanBaku.Click
		get_jam()
		Try
			OpenConn()
			OpenConnB2B()
			Cmd.Transaction = Cn.BeginTransaction
			CmdB2B.Transaction = CnB2B.BeginTransaction

			arrNoPenawaranBahanBaku.Clear()
			SQLB2B = "select a.No_Faktur From B2B_Penawaran_Bahan_Baku a , B2B_Penawaran_Bahan_Baku_Detail b "
			SQLB2B = SQLB2B & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
			SQLB2B = SQLB2B & "and a.Status is null "
			SQLB2B = SQLB2B & "and b.Flag_Approval = 'A' "
			SQLB2B = SQLB2B & "and b.Flag_Sudah_Pindah is null "
			'   SQLB2B = SQLB2B & "and b.flag_edit_extend_penawaran is null "
			SQLB2B = SQLB2B & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
			SQLB2B = SQLB2B & "group by a.No_Faktur "
			Using DsB2B = BindingTransB2B(SQLB2B)
				With DsB2B.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrNoPenawaranBahanBaku.Add(.Rows(i).Item("No_Faktur"))
					Next
				End With
			End Using

			For z As Integer = 0 To arrNoPenawaranBahanBaku.Count - 1

				get_no_faktur_penawaran()

				Dim flag_kategori_Supplier As String = ""
				Dim Data_Faktur_Penawaran As String = ""
				Dim arrDataDetailPenawaran As New ArrayList

				Dim hasInsert As Boolean = False

				SQLB2B = "select Kode_Perusahaan, no_faktur, No_Penawaran, Tanggal_Awal_Berlaku_Pnwr as Tgl_Penawaran_Hrg, "
				SQLB2B = SQLB2B & "Tanggal_Akhir_Berlaku_Pnwr as Periode_Akhir_Penawaran, Kode_Supplier, Lokasi, Tanggal, Jam, Id_User "
				SQLB2B = SQLB2B & "from B2B_Penawaran_Bahan_Baku "
				SQLB2B = SQLB2B & "where kode_perusahaan = '" & KodePerusahaan & "'"
				SQLB2B = SQLB2B & "and no_faktur = '" & arrNoPenawaranBahanBaku.Item(z).ToString & "' "
				Using DsB2B = BindingTransB2B(SQLB2B)
					With DsB2B.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1

							'==================================================
							'=     CEK APAKAH ADA DATA DI PENAWARAN INDUK     =
							'==================================================
							SQL = "select Top 1 Kode_Perusahaan, No_Faktur from emi_master_penawaran where Kode_Perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' and No_Faktur_B2B = '" & .Rows(i).Item("no_faktur") & "'"
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then

									Data_Faktur_Penawaran = Dr("No_Faktur")
								Else
									Dr.Close()
									SQL = "insert into emi_master_penawaran(Kode_Perusahaan, No_Faktur, No_Faktur_B2B, No_Penawaran, Tgl_Penawaran_Hrg, Periode_Akhir_Penawaran, Kode_Supplier, Lokasi, "
									SQL = SQL & "Tanggal, Jam, iduser) values ( "
									SQL = SQL & "'" & .Rows(i).Item("kode_perusahaan") & "', '" & Faktur_Penawaran & "', '" & .Rows(i).Item("no_faktur") & "', "
									SQL = SQL & "'" & .Rows(i).Item("No_Penawaran") & "', '" & .Rows(i).Item("tgl_penawaran_hrg") & "', "
									SQL = SQL & "'" & .Rows(i).Item("periode_akhir_penawaran") & "', '" & .Rows(i).Item("kode_supplier") & "', "
									SQL = SQL & "'" & .Rows(i).Item("lokasi") & "', '" & .Rows(i).Item("tanggal") & "', '" & .Rows(i).Item("jam") & "', '" & .Rows(i).Item("Id_User") & "' )"
									ExecuteTrans(SQL)

									Data_Faktur_Penawaran = Faktur_Penawaran
								End If
							End Using

							'===================
							'=     RELEASE     =
							'===================
							'GET KATEGORI SUPPLIER
							SQL = "select b.kode_supplier, b.ID_Kategori_Suppliers, c.flag_jenis_import  from  Suppliers b, Suppliers_Kategori c "
							SQL = SQL & "where "
							SQL = SQL & "b.kode_perusahaan = c.kode_perusahaan and b.id_kategori_suppliers = c.id_kategori_suppliers "
							SQL = SQL & "and b.kode_perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' and b.Kode_Supplier = '" & .Rows(i).Item("kode_supplier") & "' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									flag_kategori_Supplier = General_Class.CekNULL(Dr("flag_jenis_import"))
								Else
									Dr.Close()
									CloseTrans()
									CloseTransB2B()
									CloseConn()
									CloseConnB2B()
									MessageBox.Show("Supplier " & .Rows(i).Item("kode_supplier") & " Tidak Ditemukan...!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub

								End If
							End Using

							'MASUK KE DETAIL B2B
							SQLB2B = "select Kode_Perusahaan, No_Faktur, Kode_Stock_Owner,satuan, Kode_Barang, Mata_Uang, Harga, Satuan, MOQ, No_Urut "
							SQLB2B = SQLB2B & "from B2B_Penawaran_Bahan_Baku_Detail "
							SQLB2B = SQLB2B & "where Kode_Perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' "
							SQLB2B = SQLB2B & "and No_Faktur = '" & .Rows(i).Item("No_Faktur") & "' "
							SQLB2B = SQLB2B & "and Flag_Approval = 'A' "
							SQLB2B = SQLB2B & "and Flag_Sudah_Pindah is null "
							Using Ds2B2B = BindingTransB2B(SQLB2B)
								For j As Integer = 0 To Ds2B2B.Tables("MyTable").Rows.Count - 1

									hasInsert = True

									Dim Detail_KdPerusahaan As String = Ds2B2B.Tables("MyTable").Rows(j).Item("Kode_Perusahaan")
									Dim Detail_KdBarang As String = Ds2B2B.Tables("MyTable").Rows(j).Item("Kode_Barang")
									Dim Detail_MataUang As String = Ds2B2B.Tables("MyTable").Rows(j).Item("Mata_Uang")
									Dim Detail_Harga As String = Ds2B2B.Tables("MyTable").Rows(j).Item("Harga")
									Dim Detail_IdSatuan As String = Ds2B2B.Tables("MyTable").Rows(j).Item("Satuan")
									Dim Detail_MOQ As String = Ds2B2B.Tables("MyTable").Rows(j).Item("MOQ")
									Dim Detail_UrutB2B As String = Ds2B2B.Tables("MyTable").Rows(j).Item("No_Urut")

									'===================================
									'=     INSERT PENAWARAN DETAIL     =
									'===================================
									'GET KODE SATUAN

									'Dim Kode_Satuan As String = ""
									'SQLB2B = "select Kode_Satuan, Keterangan from b2b_satuan  "
									'SQLB2B = SQLB2B & "where Kode_Perusahaan = '" & Detail_KdPerusahaan & "' and Id_Satuan = '" & Detail_IdSatuan & "' "
									'Using DrB2B = OpenTransB2B(SQLB2B)
									'    If DrB2B.Read Then
									'        Kode_Satuan = DrB2B("Kode_Satuan")
									'    Else
									'        CloseTrans()
									'        CloseTransB2B()
									'        CloseConn()
									'        CloseConnB2B()
									'        MessageBox.Show("Satuan Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									'        Exit Sub
									'    End If
									'End Using

									' GET SATUAN KECIL BARANG
									Dim satuanBarang As String
									SQL = "select top(1) satuan from barang where "
									SQL = SQL & "kode_perusahaan = '" & Detail_KdPerusahaan & "' "
									SQL = SQL & "and kode_barang = '" & Detail_KdBarang & "' "
									'SQL = SQL & "and kode_stock_owner = '" & .Rows(i).Item("kode_stock_owner") & "' "
									Using Dr = OpenTrans(SQL)
										If Dr.Read Then
											satuanBarang = Dr("satuan")
										Else

											Dr.Close()
											CloseTrans()
											CloseTransB2B()
											CloseConn()
											CloseConnB2B()
											MessageBox.Show("Satuan " & Detail_IdSatuan & " Ke satuan dasar Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									End Using

									' CONVERT HARGA KESATUAN KECIL
									Dim ConvertHarga As Double = 0
									SQL = "select dbo.Ubah_Satuan_baru('" & Detail_KdPerusahaan & "','" & Detail_KdBarang & "',"
									SQL = SQL & "'" & Detail_IdSatuan & "', '" & satuanBarang & "',"
									SQL = SQL & "" & Detail_Harga & " , 'UANG') as Hasil "
									Using dr4 = OpenTrans(SQL)
										If dr4.Read Then
											If General_Class.CekNULL(dr4("Hasil")) <> "" Then
												If dr4("Hasil") = 0 Then
													dr4.Close()
													CloseTrans()
													CloseTransB2B()
													CloseConn()
													CloseConnB2B()
													MessageBox.Show("Satuan " & Detail_IdSatuan & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Exit Sub
												Else

													If General_Class.CekNULL(dr4("hasil")) = "" Then
														dr4.Close()
														CloseTrans()
														CloseTransB2B()
														CloseConn()
														CloseConnB2B()
														MessageBox.Show("Terjadi kesalahan pada satuan dasar!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Exit Sub
													End If

													ConvertHarga = dr4("hasil")

												End If
											Else
												dr4.Close()
												CloseTrans()
												CloseTransB2B()
												CloseConn()
												CloseConnB2B()
												MessageBox.Show("Satuan " & Detail_IdSatuan & " Ke " & .Rows(i).Item("satuan_kecil") & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
												Exit Sub
											End If
										End If
									End Using

									SQL = "insert into EMI_Master_Penawaran_Detail (kode_perusahaan, no_faktur, kode_barang, min_order, satuan, Harga_Satuan, Nilai_Barang, Satuan_Barang, Mata_Uang, urut_B2B,satuan_input) values ( "
									SQL = SQL & "'" & Detail_KdPerusahaan & "' , '" & Data_Faktur_Penawaran & "', '" & Detail_KdBarang & "', "
									SQL = SQL & "'" & Detail_MOQ & "', '" & satuanBarang & "', '" & Detail_Harga & "' , "
									SQL = SQL & "'" & ConvertHarga & "', '" & satuanBarang & "', '" & Detail_MataUang & "', '" & Detail_UrutB2B & "', '" & Detail_IdSatuan & "' ) "
									ExecuteTrans(SQL)

									'JIKA FLAG KATEGORI SUPPLIER = Y
									If flag_kategori_Supplier = "Y" Then

										'GET SATUAN KIRIM
										Dim satuan_kirim As String = ""
										SQL = "select satuan from barang_detail_satuan where kode_barang='" & Detail_KdBarang & "' "
										SQL = SQL & "and kode_Perusahaan='" & Detail_KdPerusahaan & "' and flag_kirim='Y' "
										Using dr3 = OpenTrans(SQL)
											If dr3.Read Then
												satuan_kirim = dr3("satuan")
											Else
												dr3.Close()
												CloseTrans()
												CloseTransB2B()
												CloseConn()
												CloseConnB2B()
												MessageBox.Show("data satuan kirim tidak ada ")
												Exit Sub
											End If
										End Using

										'GET NAMA BARANG
										Dim nama As String = ""
										SQL = "select top(1) nama from barang where kode_barang ='" & Detail_KdBarang & "' "
										Using Dr = OpenTrans(SQL)
											If Dr.Read Then
												nama = Dr("nama")
											End If
										End Using

										'INSERT KOMPOSISI BARANG JADI
										SQL = "select kode_Perusahaan from komposisi_barang_jadi where kode_barang='" & Detail_KdBarang & "' "
										SQL = SQL & "and kode_Perusahaan='" & Detail_KdPerusahaan & "' "
										Using dr33 = OpenTrans(SQL)
											If Not dr33.Read Then
												dr33.Close()
												SQL = "insert into komposisi_barang_jadi(kode_perusahaan, "
												SQL = SQL & "kode_barang, Qty) Values ("
												SQL = SQL & " '" & Detail_KdPerusahaan & "', '" & Detail_KdBarang & "', "
												SQL = SQL & "'1')"
												ExecuteTrans(SQL)

												SQL = "insert into detail_komposisi_barang_jadi(kode_perusahaan, "
												SQL = SQL & "kode_barang, Kode_Bahan, Qty_Bahan) Values("
												SQL = SQL & "'" & Detail_KdPerusahaan & "', '" & Detail_KdBarang & "', "
												SQL = SQL & "'" & Detail_KdBarang & "', '1')"
												ExecuteTrans(SQL)
											End If
										End Using

										'INSERT BAHAN IMPORT
										SQL = "select kode_Perusahaan from bahan_import where kode_bahan='" & Detail_KdBarang & "' "
										SQL = SQL & "and kode_Perusahaan='" & Detail_KdPerusahaan & "' "
										Using dr33 = OpenTrans(SQL)
											If Not dr33.Read Then
												dr33.Close()

												SQL = "select kode_stock_owner_import from stock_owner_import where kode_perusahaan = '" & Detail_KdPerusahaan & "' "
												SQL = SQL & "order by kode_stock_owner_import"
												Using Dsm = BindingTrans(SQL)
													If Dsm.Tables("MyTable").Rows.Count <> 0 Then
														For iii As Integer = 0 To Dsm.Tables("MyTable").Rows.Count - 1

															SQL = "Insert Into bahan_import(Kode_Perusahaan, Kode_STock_Owner_Import, kode_bahan, Kode_supplier, "
															SQL = SQL & "nama_bahan, kategori, mata_uang, harga, satuan, Flag_Potong_Stock) Values("
															SQL = SQL & "'" & Detail_KdPerusahaan & "', '" & Dsm.Tables("MyTable").Rows(iii).Item("kode_stock_owner_import") & "', "
															SQL = SQL & "'" & Detail_KdBarang & "','" & .Rows(i).Item("kode_supplier") & "',"
															SQL = SQL & "'" & nama & "', '" & "Utama" & "', '" & Detail_MataUang & "', "
															SQL = SQL & "'" & Val(HilangkanTanda(Detail_Harga)) & "','" & satuan_kirim & "', '" & "T" & "')"
															ExecuteTrans(SQL)

														Next
													Else
														CloseTrans()
														CloseTransB2B()
														CloseConn()
														CloseConnB2B()
														MessageBox.Show("Data lokasi import tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Exit Sub
													End If

												End Using
											End If
										End Using

										'UPDATE emi_master_penawaran_detail
										SQL = "update emi_master_penawaran_detail set "
										SQL = SQL & "flag_baru = 'Y' "
										SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Kode_barang='" & Detail_KdBarang & "' "
										SQL = SQL & "and no_faktur='" & .Rows(i).Item("No_Faktur") & "' and urut_B2B = '" & Detail_UrutB2B & "' "
										ExecuteTrans(SQL)

									End If

									SQLB2B = "Update B2B_Penawaran_Bahan_Baku_Detail set flag_sudah_pindah = 'Y' where "
									SQLB2B = SQLB2B & "Kode_Perusahaan = '" & Detail_KdPerusahaan & "'  "
									SQLB2B = SQLB2B & "and No_Faktur = '" & .Rows(i).Item("no_faktur") & "' "
									SQLB2B = SQLB2B & "and no_urut = '" & Detail_UrutB2B & "' "
									ExecuteTransB2B(SQLB2B)

								Next
							End Using

							'MASUK KE Penawaran Jatuh Tempo
							SQLB2B = "select no_urut,kode_perusahaan,no_faktur,no_penawaran,jenis_pembayaran,tempo_pembayaran,lama_pembayaran "
							SQLB2B = SQLB2B & "from B2B_Jatuh_Tempo_RawMaterial a "

							SQLB2B = SQLB2B & "where a.Kode_Perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' "
							SQLB2B = SQLB2B & "and a.no_faktur = '" & .Rows(i).Item("no_faktur") & "' "

							SQLB2B = SQLB2B & "order by no_urut "
							Using Ds2B2B = BindingTransB2B(SQLB2B)
								For j As Integer = 0 To Ds2B2B.Tables("MyTable").Rows.Count - 1

									hasInsert = True

									Dim Detail_KdPerusahaanT As String = Ds2B2B.Tables("MyTable").Rows(j).Item("kode_perusahaan")
									Dim no_urutT As String = Ds2B2B.Tables("MyTable").Rows(j).Item("no_urut")
									Dim no_fakturT As String = Ds2B2B.Tables("MyTable").Rows(j).Item("no_faktur")
									Dim no_penawaranT As String = Ds2B2B.Tables("MyTable").Rows(j).Item("no_penawaran")
									Dim jenis_pembayaranT As String = Ds2B2B.Tables("MyTable").Rows(j).Item("jenis_pembayaran")

									Dim tempo_pembayaranT As String = ""
									If IsDBNull(Ds2B2B.Tables("MyTable").Rows(j).Item("tempo_pembayaran")) Then
										tempo_pembayaranT = "NULL"
									Else
										tempo_pembayaranT = "'" & Ds2B2B.Tables("MyTable").Rows(j).Item("tempo_pembayaran") & "'"
									End If

									Dim lama_pembayaranT As String = ""
									If IsDBNull(Ds2B2B.Tables("MyTable").Rows(j).Item("lama_pembayaran")) Then
										lama_pembayaranT = "NULL"
									Else
										lama_pembayaranT = "'" & Ds2B2B.Tables("MyTable").Rows(j).Item("lama_pembayaran") & "'"
									End If

									SQL = "insert into EMI_Master_Penawaran_Jatuh_Tempo(kode_perusahaan,no_faktur,no_faktur_b2b,no_penawaran, "
									SQL = SQL & "urut_b2b,jenis_pembayaran,tempo_pembayaran,lama_pembayaran) values ( "
									SQL = SQL & "'" & Detail_KdPerusahaanT & "','" & Data_Faktur_Penawaran & "', '" & no_fakturT & "', '" & no_penawaranT & "' ,"
									SQL = SQL & "'" & no_urutT & "', '" & jenis_pembayaranT & "', " & tempo_pembayaranT & ", " & lama_pembayaranT & ""
									SQL = SQL & ")"
									ExecuteTrans(SQL)

								Next

							End Using

							'==================================================================
							'=     CEK APAKAH EMI_MASTER_PENAWARAN_DETAIL SUDAH DI INSERT     =
							'==================================================================
							SQL = "select top 1 * from EMI_Master_Penawaran_Detail where Kode_Perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' and No_Faktur = '" & Data_Faktur_Penawaran & "' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
								Else
									Dr.Close()
									CloseTrans()
									CloseTransB2B()
									CloseConn()
									CloseConnB2B()
									MessageBox.Show("Ada Masalah saat Insert Detail Penawaran", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

							If hasInsert Then
								'UPDATE FLAG RELEASE
								SQL = "update EMI_Master_Penawaran set "
								SQL = SQL & "flag_release = 'Y', "
								SQL = SQL & "Tanggal_Release = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
								SQL = SQL & "jam_release = '" & Format(tgl_skg, "HH:mm:ss") & "' , "
								SQL = SQL & "iduser_release = '" & UserID & "' "
								SQL = SQL & "where kode_perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' and No_Faktur='" & Faktur_Penawaran & "' "
								SQL = SQL & "and no_penawaran='" & .Rows(i).Item("No_Penawaran") & "' "
								ExecuteTrans(SQL)
							End If

						Next
					End With
				End Using
			Next

			Cmd.Transaction.Commit()
			CmdB2B.Transaction.Commit()
			CloseConn()
			CloseConnB2B()
		Catch ex As Exception
			CloseTrans()
			CloseTransB2B()
			CloseConn()
			CloseConnB2B()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Btn_BiayaLokal_Click(sender As Object, e As EventArgs) Handles Btn_BiayaLokal.Click
		Try
			OpenConn()
			OpenConnB2B()
			Cmd.Transaction = Cn.BeginTransaction
			CmdB2B.Transaction = CnB2B.BeginTransaction

			Dim arrBiayaLokalEMI As New ArrayList
			arrBiayaLokalEMI.Clear()

			'==================================
			'=    GET KDOE BIAYA FROM EMI     =
			'==================================
			SQL = "select Kode_Biaya "
			SQL = SQL & "from Biaya_B2B where Kode_Perusahaan = '" & KodePerusahaan & "' "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrBiayaLokalEMI.Add(.Rows(i).Item("Kode_Biaya"))
					Next
				End With
			End Using

			'============================
			'=    INSERT EMI -> B2B     =
			'============================
			For i As Integer = 0 To arrBiayaLokalEMI.Count - 1

				SQL = "select Kode_Perusahaan, Kode_Biaya, Perhitungan, Flag_lokal, Flag_Import  "
				SQL = SQL & "from Biaya_B2B where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Biaya = '" & arrBiayaLokalEMI(i) & "'"
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						For j As Integer = 0 To .Rows.Count - 1

							'Cek apakah data sudah ada
							SQLB2B = "select Kode_Biaya "
							SQLB2B = SQLB2B & "from Biaya_B2B where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Biaya = '" & .Rows(j).Item("Kode_Biaya") & "' "
							Using Ds1 = BindingTransB2B(SQLB2B)
								If Ds1.Tables("MyTable").Rows.Count = 0 Then
									'Insert ke B2B
									SQLB2B = "insert into Biaya_B2B (Kode_Perusahaan, Kode_Biaya, Perhitungan, Flag_lokal, Flag_Import) "
									SQLB2B = SQLB2B & "values ('" & .Rows(j).Item("Kode_Perusahaan") & "', '" & .Rows(j).Item("Kode_Biaya") & "', "
									SQLB2B = SQLB2B & "'" & .Rows(j).Item("Perhitungan") & "', '" & .Rows(j).Item("Flag_lokal") & "', '" & .Rows(j).Item("Flag_Import") & "') "
									ExecuteTransB2B(SQLB2B)
								End If
							End Using
						Next
					End With
				End Using

				SQL = "Update Biaya_B2B set Flag_Sudah_Pindah = 'Y'  "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Kode_Biaya = '" & arrBiayaLokalEMI(i) & "' "
				ExecuteTrans(SQL)

			Next

			Cmd.Transaction.Commit()
			CmdB2B.Transaction.Commit()
			CloseConn()
			CloseConnB2B()
		Catch ex As Exception
			CloseTrans()
			CloseTransB2B()
			CloseConn()
			CloseConnB2B()
			MessageBox.Show(ex.Message & " Insert Biaya Lokal")
			Exit Sub
		End Try

	End Sub

	Private Sub Btn_Update_BiayaLokal_Click(sender As Object, e As EventArgs) Handles Btn_Update_BiayaLokal.Click
		Try
			OpenConn()
			OpenConnB2B()
			Cmd.Transaction = Cn.BeginTransaction
			CmdB2B.Transaction = CnB2B.BeginTransaction

			Dim arrBiayaLokalEMI As New ArrayList
			arrBiayaLokalEMI.Clear()

			'==================================
			'=    GET KDOE BIAYA FROM EMI     =
			'==================================
			SQL = "select Kode_Biaya "
			SQL = SQL & "from Biaya_B2B where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Update ='Y' order by Kode_Biaya"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrBiayaLokalEMI.Add(.Rows(i).Item("Kode_Biaya"))
					Next
				End With
			End Using

			'============================
			'=    INSERT EMI -> B2B     =
			'============================
			For i As Integer = 0 To arrBiayaLokalEMI.Count - 1

				SQL = "select Kode_Perusahaan, Kode_Biaya, Perhitungan, Flag_lokal, Flag_Import  "
				SQL = SQL & "from Biaya_B2B where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Biaya = '" & arrBiayaLokalEMI(i) & "' and Flag_Update ='Y'"
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						For j As Integer = 0 To .Rows.Count - 1

							'Cek Update
							SQLB2B = "update Biaya_B2B set Perhitungan = '" & Ds.Tables("MyTable").Rows(j).Item("Perhitungan") & "', "
							SQLB2B = SQLB2B & "Flag_lokal = '" & Ds.Tables("MyTable").Rows(j).Item("Flag_lokal") & "', Flag_Import = '" & Ds.Tables("MyTable").Rows(j).Item("Flag_lokal") & "' "
							SQLB2B = SQLB2B & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Biaya = '" & arrBiayaLokalEMI(i) & "' "
							ExecuteTransB2B(SQLB2B)
						Next
					End With
				End Using

				SQL = "Update Biaya_B2B set Flag_Update = NULL  "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Kode_Biaya = '" & arrBiayaLokalEMI(i) & "' "
				ExecuteTrans(SQL)

			Next

			Cmd.Transaction.Commit()
			CmdB2B.Transaction.Commit()
			CloseConn()
			CloseConnB2B()
		Catch ex As Exception
			CloseTrans()
			CloseTransB2B()
			CloseConn()
			CloseConnB2B()
			MessageBox.Show(ex.Message & " Update Biaya Lokal")
			Exit Sub
		End Try
	End Sub

	Private Sub Btn_InsKendaraan_Click(sender As Object, e As EventArgs) Handles Btn_InsKendaraan.Click
		Try
			OpenConn()
			OpenConnB2B()
			Cmd.Transaction = Cn.BeginTransaction
			CmdB2B.Transaction = CnB2B.BeginTransaction

			Dim arrKendaraan As New ArrayList
			arrKendaraan.Clear()

			'==================================
			'=    GET KDOE BIAYA FROM EMI     =
			'==================================
			SQL = "select Id_Kendaraan from Master_Kendaraan where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Pindah is null"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrKendaraan.Add(.Rows(i).Item("Id_Kendaraan"))
					Next
				End With
			End Using

			'============================
			'=    INSERT EMI -> B2B     =
			'============================
			For i As Integer = 0 To arrKendaraan.Count - 1

				SQL = "select Kode_Perusahaan, Id_Kendaraan, Kode_Kendaraan, Keterangan, Kapasitas, Satuan_Kapasitas, id_jenisEkspedisi "
				SQL = SQL & "from Master_Kendaraan  "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Kendaraan = '" & arrKendaraan(i) & "' and Flag_Pindah is null "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For j As Integer = 0 To .Rows.Count - 1

								'Cek Apakah sudah ada data
								SQLB2B = "select Id_Kendaraan from B2B_Master_Kendaraan "
								SQLB2B = SQLB2B & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
								SQLB2B = SQLB2B & "and Id_Kendaraan = '" & .Rows(j).Item("Id_Kendaraan") & "' "
								Using Ds1 = BindingTransB2B(SQLB2B)
									If Ds1.Tables("MyTable").Rows.Count = 0 Then
										'Insert ke B2B

										Dim Kapasitas As String = .Rows(j).Item("Kapasitas") & " " & .Rows(j).Item("Satuan_Kapasitas")

										SQLB2B = "insert into B2B_Master_Kendaraan (Kode_Perusahaan, Id_Kendaraan, Kode_Kendaraan, Keterangan, Kapasitas, id_jenisEkspedisi) "
										SQLB2B = SQLB2B & "values ('" & .Rows(j).Item("Kode_Perusahaan") & "', '" & .Rows(j).Item("Id_Kendaraan") & "', '" & .Rows(j).Item("Kode_Kendaraan") & "', "
										SQLB2B = SQLB2B & "'" & .Rows(j).Item("Keterangan") & "', '" & Kapasitas & "', '" & .Rows(j).Item("id_jenisEkspedisi") & "') "
										ExecuteTransB2B(SQLB2B)

									End If
								End Using

							Next
						End If
					End With
				End Using

				SQL = "Update Master_Kendaraan set Flag_Pindah = 'Y'  "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Id_Kendaraan = '" & arrKendaraan(i) & "' "
				ExecuteTrans(SQL)

			Next

			Cmd.Transaction.Commit()
			CmdB2B.Transaction.Commit()
			CloseConn()
			CloseConnB2B()
		Catch ex As Exception
			CloseTrans()
			CloseTransB2B()
			CloseConn()
			CloseConnB2B()
			MessageBox.Show(ex.Message & "Insert Kendaraan")
			Exit Sub
		End Try
	End Sub

	Private Sub Btn_UpdateKendaraan_Click(sender As Object, e As EventArgs) Handles Btn_UpdateKendaraan.Click
		Try
			OpenConn()
			OpenConnB2B()
			Cmd.Transaction = Cn.BeginTransaction
			CmdB2B.Transaction = CnB2B.BeginTransaction

			Dim arrKendaraan As New ArrayList
			arrKendaraan.Clear()

			'==================================
			'=    GET ID KENDARAAN FROM EMI     =
			'==================================

			SQL = "select Id_Kendaraan from Master_Kendaraan where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Update = 'Y'"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrKendaraan.Add(.Rows(i).Item("Id_Kendaraan"))
					Next
				End With
			End Using

			'============================
			'=    INSERT EMI -> B2B     =
			'============================
			For i As Integer = 0 To arrKendaraan.Count - 1

				SQL = "select Kode_Perusahaan, Id_Kendaraan, Kode_Kendaraan, Keterangan, Kapasitas, Satuan_Kapasitas, id_jenisEkspedisi, "
				SQL = SQL & "Panjang, Lebar, Tinggi, Volume  "
				SQL = SQL & "from Master_Kendaraan  "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Kendaraan = '" & arrKendaraan(i) & "' and Flag_Update = 'Y' "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						For j As Integer = 0 To .Rows.Count - 1

							Dim Kapasitas As String = .Rows(j).Item("Kapasitas") & " " & .Rows(j).Item("Satuan_Kapasitas")

							SQLB2B = "update B2B_Master_Kendaraan set Keterangan = '" & .Rows(j).Item("Keterangan") & "', Kapasitas = '" & Kapasitas & "', id_jenisEkspedisi = '" & .Rows(j).Item("id_jenisEkspedisi") & "' "
							SQLB2B = SQLB2B & "where Kode_Perusahaan = '" & .Rows(j).Item("Kode_Perusahaan") & "' and Id_Kendaraan = '" & .Rows(j).Item("Id_Kendaraan") & "' "
							ExecuteTransB2B(SQLB2B)

						Next
					End With
				End Using

				SQL = "Update Master_Kendaraan set Flag_Update = NULL  "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Id_Kendaraan = '" & arrKendaraan(i) & "' "
				ExecuteTrans(SQL)

			Next

			Cmd.Transaction.Commit()
			CmdB2B.Transaction.Commit()
			CloseConn()
			CloseConnB2B()
		Catch ex As Exception
			CloseTrans()
			CloseTransB2B()
			CloseConn()
			CloseConnB2B()
			MessageBox.Show(ex.Message & " Update Kendaraan")
			Exit Sub
		End Try
	End Sub

	Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Btn_InsExpedisi.Click
		Try
			OpenConn()
			OpenConnB2B()
			Cmd.Transaction = Cn.BeginTransaction
			CmdB2B.Transaction = CnB2B.BeginTransaction

			SQLB2B = "select a.Kode_Perusahaan, c.No_Faktur, c.No_Penawaran, c.Tanggal, c.Jam, c.Jenis_Pengiriman, c.Kode_Supplier as Kode_Perusahaan_Biaya_Import, c.Kode_Biaya, c.Tanggal_Mulai, c.Tanggal_Selesai, "
			SQLB2B = SQLB2B & "b.Provinsi_Asal, b.Kota_Asal, b.Kecamatan_Asal, b.Provinsi_Tujuan, b.Kota_Tujuan, b.Kecamatan_Tujuan, a.Jenis_Kendaraan, a.Kapasitas, a.Tarif, a.Biaya_Lain, a.Total, a.No_Urut "
			SQLB2B = SQLB2B & "from B2B_Detail_Vehicle_Expedition a, B2B_Detail_Expedition b, B2B_Expedition c "
			SQLB2B = SQLB2B & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
			SQLB2B = SQLB2B & "and a.Id_Detail_Expedition = b.No_Urut "
			SQLB2B = SQLB2B & "and b.No_Penawaran = c.No_Penawaran "
			SQLB2B = SQLB2B & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQLB2B = SQLB2B & "and c.Status is null "
			SQLB2B = SQLB2B & "and a.Flag_Approval = 'A' "
			SQLB2B = SQLB2B & "and a.Flag_Sudah_Pindah is null "
			SQLB2B = SQLB2B & "order by c.No_Faktur "
			Using DsB2B = BindingTransB2B(SQLB2B)
				With DsB2B.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1

						Dim No_Faktur As String = .Rows(i).Item("No_Faktur")

						'=================================================
						'=     CEK APAKAH DATA INDUK SUDAH DI INSERT     =
						'=================================================
						SQL = "select No_Faktur from Emi_Expedition_PO where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & .Rows(i).Item("No_Faktur") & "' "
						Using Ds = BindingTrans(SQL)
							If Ds.Tables("MyTable").Rows.Count = 0 Then

								'========================
								'=     INSERT INDUK     =
								'========================
								SQL = "insert into Emi_Expedition_PO (Kode_Perusahaan, No_Faktur, No_Penawaran, Tanggal, Jam, Jenis_Pengiriman, Kode_Perusahaan_Biaya_import, Kode_Biaya, Mata_Uang, Tanggal_Mulai, Tanggal_Selesai ) "
								SQL = SQL & "values ('" & .Rows(i).Item("Kode_Perusahaan") & "', '" & .Rows(i).Item("No_Faktur") & "', '" & .Rows(i).Item("No_Penawaran") & "', "
								SQL = SQL & "'" & .Rows(i).Item("Tanggal") & "', '" & .Rows(i).Item("Jam") & "', '" & .Rows(i).Item("Jenis_Pengiriman") & "', "
								SQL = SQL & "'" & .Rows(i).Item("Kode_Perusahaan_Biaya_Import") & "', '" & .Rows(i).Item("Kode_Biaya") & "', 'RP', "
								SQL = SQL & "'" & .Rows(i).Item("Tanggal_Mulai") & "', '" & .Rows(i).Item("Tanggal_Selesai") & "')"
								ExecuteTrans(SQL)

							End If
						End Using

						'=========================
						'=     INSERT DETAIL     =
						'=========================
						SQL = "insert into Emi_Expedition_PO_Detail (Kode_Perusahaan, No_Faktur, Provinsi_Asal, Kota_Asal, Kecamatan_Asal, Provinsi_Tujuan, Kota_Tujuan, Kecamatan_Tujuan, Jenis_Kendaraan, Kapasitas, Tarif, Biaya_Lain, Total) "
						SQL = SQL & "values ('" & .Rows(i).Item("Kode_Perusahaan") & "', '" & .Rows(i).Item("No_Faktur") & "', '" & .Rows(i).Item("Provinsi_Asal") & "', '" & .Rows(i).Item("Kota_Asal") & "', '" & .Rows(i).Item("Kecamatan_Asal") & "', "
						SQL = SQL & "'" & .Rows(i).Item("Provinsi_Tujuan") & "', '" & .Rows(i).Item("Kota_Tujuan") & "', '" & .Rows(i).Item("Kecamatan_Tujuan") & "', '" & .Rows(i).Item("Jenis_Kendaraan") & "', '" & .Rows(i).Item("Kapasitas") & "', "
						SQL = SQL & "'" & .Rows(i).Item("Tarif") & "', '" & .Rows(i).Item("Biaya_Lain") & "', '" & .Rows(i).Item("Total") & "') "
						ExecuteTrans(SQL)

						'=======================
						'=     UPDATE FLAG     =
						'=======================
						SQLB2B = "Update B2B_Detail_Vehicle_Expedition set Flag_Sudah_Pindah = 'Y' where "
						SQLB2B = SQLB2B & "Kode_Perusahaan = '" & .Rows(i).Item("Kode_Perusahaan") & "' "
						SQLB2B = SQLB2B & "and no_urut = '" & .Rows(i).Item("No_Urut") & "' "
						ExecuteTransB2B(SQLB2B)
					Next
				End With
			End Using

			Cmd.Transaction.Commit()
			CmdB2B.Transaction.Commit()
			CloseConn()
			CloseConnB2B()
		Catch ex As Exception
			CloseTrans()
			CloseTransB2B()
			CloseConn()
			CloseConnB2B()
			MessageBox.Show(ex.Message & " Insert Expedisi")
			Exit Sub
		End Try
	End Sub

	Private Sub Button7_Click(sender As Object, e As EventArgs) Handles btn_Master_Satuan.Click
		Try
			OpenConn()
			OpenConnB2B()
			Cmd.Transaction = Cn.BeginTransaction
			CmdB2B.Transaction = CnB2B.BeginTransaction

			SQL = "select Kode_Perusahaan,Kode_Barang,Barang,Satuan, "
			SQL = SQL & "Nilai,Flag_Dasar,Flag_Default "
			SQL = SQL & "From N_EMI_Master_Satuan where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							SQLB2B = "insert into n_emi_master_satuan(kode_perusahaan,kode_barang,barang,satuan,nilai,flag_dasar,flag_default) values ( "
							SQLB2B = SQLB2B & "'" & KodePerusahaan & "', '" & .Rows(i).Item("kode_barang") & "' ,'" & .Rows(i).Item("barang") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("satuan") & "' , '" & .Rows(i).Item("nilai") & "' , '" & .Rows(i).Item("flag_dasar") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("flag_default") & "' ) "
							ExecuteTransB2B(SQLB2B)

							SQL = "update n_emi_master_satuan set flag_sdh_Pindah = 'Y' where "
							SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and kode_barang = '" & .Rows(i).Item("kode_barang") & "' and satuan = '" & .Rows(i).Item("satuan") & "' "
							ExecuteTrans(SQL)
						Next
					End If
				End With
			End Using

			'SQLB2B = "select a.Kode_Perusahaan, c.No_Faktur, c.No_Penawaran, c.Tanggal, c.Jam, c.Jenis_Pengiriman, c.Kode_Supplier as Kode_Perusahaan_Biaya_Import, c.Kode_Biaya, c.Tanggal_Mulai, c.Tanggal_Selesai, "
			'SQLB2B = SQLB2B & "b.Provinsi_Asal, b.Kota_Asal, b.Kecamatan_Asal, b.Provinsi_Tujuan, b.Kota_Tujuan, b.Kecamatan_Tujuan, a.Jenis_Kendaraan, a.Kapasitas, a.Tarif, a.Biaya_Lain, a.Total, a.No_Urut "
			'SQLB2B = SQLB2B & "from B2B_Detail_Vehicle_Expedition a, B2B_Detail_Expedition b, B2B_Expedition c "
			'SQLB2B = SQLB2B & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
			'SQLB2B = SQLB2B & "and a.Id_Detail_Expedition = b.No_Urut "
			'SQLB2B = SQLB2B & "and b.No_Penawaran = c.No_Penawaran "
			'SQLB2B = SQLB2B & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'SQLB2B = SQLB2B & "and c.Status is null "
			'SQLB2B = SQLB2B & "and a.Flag_Approval = 'A' "
			'SQLB2B = SQLB2B & "and a.Flag_Sudah_Pindah is null "
			'SQLB2B = SQLB2B & "order by c.No_Faktur "
			'Using DsB2B = BindingTransB2B(SQLB2B)
			'    With DsB2B.Tables("MyTable")
			'        For i As Integer = 0 To .Rows.Count - 1

			'            Dim No_Faktur As String = .Rows(i).Item("No_Faktur")

			'            '=================================================
			'            '=     CEK APAKAH DATA INDUK SUDAH DI INSERT     =
			'            '=================================================
			'            SQL = "select No_Faktur from Emi_Expedition_PO where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & .Rows(i).Item("No_Faktur") & "' "
			'            Using Ds = BindingTrans(SQL)
			'                If Ds.Tables("MyTable").Rows.Count = 0 Then

			'                    '========================
			'                    '=     INSERT INDUK     =
			'                    '========================
			'                    SQL = "insert into Emi_Expedition_PO (Kode_Perusahaan, No_Faktur, No_Penawaran, Tanggal, Jam, Jenis_Pengiriman, Kode_Perusahaan_Biaya_import, Kode_Biaya, Mata_Uang, Tanggal_Mulai, Tanggal_Selesai ) "
			'                    SQL = SQL & "values ('" & .Rows(i).Item("Kode_Perusahaan") & "', '" & .Rows(i).Item("No_Faktur") & "', '" & .Rows(i).Item("No_Penawaran") & "', "
			'                    SQL = SQL & "'" & .Rows(i).Item("Tanggal") & "', '" & .Rows(i).Item("Jam") & "', '" & .Rows(i).Item("Jenis_Pengiriman") & "', "
			'                    SQL = SQL & "'" & .Rows(i).Item("Kode_Perusahaan_Biaya_Import") & "', '" & .Rows(i).Item("Kode_Biaya") & "', 'RP', "
			'                    SQL = SQL & "'" & .Rows(i).Item("Tanggal_Mulai") & "', '" & .Rows(i).Item("Tanggal_Selesai") & "')"
			'                    ExecuteTrans(SQL)

			'                End If
			'            End Using

			'            '=========================
			'            '=     INSERT DETAIL     =
			'            '=========================
			'            SQL = "insert into Emi_Expedition_PO_Detail (Kode_Perusahaan, No_Faktur, Provinsi_Asal, Kota_Asal, Kecamatan_Asal, Provinsi_Tujuan, Kota_Tujuan, Kecamatan_Tujuan, Jenis_Kendaraan, Kapasitas, Tarif, Biaya_Lain, Total) "
			'            SQL = SQL & "values ('" & .Rows(i).Item("Kode_Perusahaan") & "', '" & .Rows(i).Item("No_Faktur") & "', '" & .Rows(i).Item("Provinsi_Asal") & "', '" & .Rows(i).Item("Kota_Asal") & "', '" & .Rows(i).Item("Kecamatan_Asal") & "', "
			'            SQL = SQL & "'" & .Rows(i).Item("Provinsi_Tujuan") & "', '" & .Rows(i).Item("Kota_Tujuan") & "', '" & .Rows(i).Item("Kecamatan_Tujuan") & "', '" & .Rows(i).Item("Jenis_Kendaraan") & "', '" & .Rows(i).Item("Kapasitas") & "', "
			'            SQL = SQL & "'" & .Rows(i).Item("Tarif") & "', '" & .Rows(i).Item("Biaya_Lain") & "', '" & .Rows(i).Item("Total") & "') "
			'            ExecuteTrans(SQL)

			'            '=======================
			'            '=     UPDATE FLAG     =
			'            '=======================
			'            SQLB2B = "Update B2B_Detail_Vehicle_Expedition set Flag_Sudah_Pindah = 'Y' where "
			'            SQLB2B = SQLB2B & "Kode_Perusahaan = '" & .Rows(i).Item("Kode_Perusahaan") & "' "
			'            SQLB2B = SQLB2B & "and no_urut = '" & .Rows(i).Item("No_Urut") & "' "
			'            ExecuteTransB2B(SQLB2B)
			'        Next
			'    End With
			'End Using

			Cmd.Transaction.Commit()
			CmdB2B.Transaction.Commit()
			CloseConn()
			CloseConnB2B()
		Catch ex As Exception
			CloseTrans()
			CloseTransB2B()
			CloseConn()
			CloseConnB2B()
			ListView1.Items.Add("Eror | Insert Satuan Master Barang  | " & ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles btnPerusahaanBIaya.Click
		get_jam()
		Try
			OpenConn()
			OpenConnB2B()
			Cmd.Transaction = Cn.BeginTransaction
			CmdB2B.Transaction = CnB2B.BeginTransaction

			arrKodePerusahanBiayaImport.Clear()
			SQL = "select Distinct a.Flag_Sudah_Pindah, kode_perusahaan_biaya_import "
			SQL = SQL & "from Perusahaan_biaya_Import a "
			SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Kode_Master_Kategori_Biaya_Import in ( "

			SQL = SQL & "select Distinct y.Kode_Master_Kategori_Biaya_Import "
			SQL = SQL & "from Biaya_B2B k, Biaya_Import z, Kategori_Biaya_Import x, Master_Kategori_Biaya_Import y "
			SQL = SQL & "where k.Kode_Perusahaan = z.Kode_Perusahaan and z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan "
			SQL = SQL & "and k.Kode_Biaya = z.Kode_Biaya "
			SQL = SQL & "and z.Kode_Kategori_Biaya_Import = x.Kode_Kategori_Biaya_Import "
			SQL = SQL & "and x.Kode_Master_Kategori_Biaya_Import = y.Kode_Master_Kategori_Biaya_Import "
			SQL = SQL & "and z.Kode_Perusahaan = '" & KodePerusahaan & "') "

			SQL = SQL & "and a.Flag_Sudah_Pindah is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrKodePerusahanBiayaImport.Add(.Rows(i).Item("kode_perusahaan_biaya_import"))
					Next
				End With
			End Using

			Dim j As Integer = 0
			For z As Integer = 1 To arrKodePerusahanBiayaImport.Count

				SQL = "select Kode_Perusahaan, kode_perusahaan_biaya_import, Nama "
				SQL = SQL & "from Perusahaan_biaya_Import where kode_perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "flag_sudah_pindah is null and "
				SQL = SQL & "kode_perusahaan_biaya_import = '" & arrKodePerusahanBiayaImport.Item(j).ToString & "' "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							'Insert ke MySql
							SQLB2B = "insert into suppliers (Kode_Perusahaan,Kode_Supplier,Nama, Nama_Supplier "
							SQLB2B = SQLB2B & ")"
							SQLB2B = SQLB2B & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("kode_perusahaan_biaya_import") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Nama") & "', '" & .Rows(i).Item("Nama") & "' "
							SQLB2B = SQLB2B & ")"
							ExecuteTransB2B(SQLB2B)

						Next
					End With
				End Using

				SQL = "Update Perusahaan_biaya_Import set flag_sudah_pindah = 'Y' where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
				SQL = SQL & "kode_perusahaan_biaya_import = '" & arrKodePerusahanBiayaImport.Item(j).ToString & "' "
				ExecuteTrans(SQL)

				j = j + 1
			Next

			Cmd.Transaction.Commit()
			CmdB2B.Transaction.Commit()
			CloseConn()
			CloseConnB2B()
		Catch ex As Exception
			CloseTrans()
			CloseTransB2B()
			CloseConn()
			CloseConnB2B()
			MessageBox.Show(ex.Message & " Insert Perusahaan")
			Exit Sub
		End Try
	End Sub

	Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click
		get_jam()
		Try
			OpenConn()
			OpenConnB2B()
			Cmd.Transaction = Cn.BeginTransaction
			CmdB2B.Transaction = CnB2B.BeginTransaction

			arrNoPoPembelianSelesai.Clear()
			SQLB2B = "select a.No_Faktur from EMI_Pembelian_PO a, EMI_Pembelian_PO_Detail b "
			SQLB2B = SQLB2B & "where a.Status is null and a.flag_selesai_po = 'Y' "
			SQLB2B = SQLB2B & "and a.Kode_Perusahaan = b.Kode_Perusahaan "
			SQLB2B = SQLB2B & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
			SQLB2B = SQLB2B & "and b.Flag_Sudah_Kirim = 'Y' and flag_edit_po = 'Y' "
			SQLB2B = SQLB2B & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
			SQLB2B = SQLB2B & "group by a.No_Faktur "
			Using DsB2B = BindingTransB2B(SQLB2B)
				With DsB2B.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrNoPoPembelianSelesai.Add(.Rows(i).Item("no_faktur"))
					Next
				End With
			End Using

			For z As Integer = 0 To arrNoPoPembelianSelesai.Count - 1

				SQL = "update emi_pembelian_Po set flag_selesai_po = 'Y' where kode_perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and no_faktur = '" & arrNoPoPembelianSelesai.Item(z).ToString & "' "
				ExecuteTrans(SQL)

				SQLB2B = "update emi_pembelian_PO set flag_edit_po = null where kode_perusahaan = '" & KodePerusahaan & "' "
				SQLB2B = SQLB2B & "and no_faktur = '" & arrNoPoPembelianSelesai.Item(z).ToString & "' "
				ExecuteTransB2B(SQLB2B)

			Next

			Cmd.Transaction.Commit()
			CmdB2B.Transaction.Commit()
			CloseConn()
			CloseConnB2B()
		Catch ex As Exception
			CloseTrans()
			CloseTransB2B()
			CloseConn()
			CloseConnB2B()
			MessageBox.Show(ex.Message & " Insert Penawaran Packaging")
			Exit Sub
		End Try
	End Sub

	Private Sub Button6_Click_2(sender As Object, e As EventArgs) Handles Button6.Click
		Handle_Proses_Waste_Process()
	End Sub

	Private Sub Button7_Click_1(sender As Object, e As EventArgs) Handles Button7.Click
		Handle_Proses_Waste_Product()
	End Sub

	Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
		Handle_Proses_Waste_Process_Stock()
	End Sub

	Private Sub btnPenawaranPackaging_Click(sender As Object, e As EventArgs) Handles btnPenawaranPackaging.Click
		get_jam()
		Try
			OpenConn()
			OpenConnB2B()
			Cmd.Transaction = Cn.BeginTransaction
			CmdB2B.Transaction = CnB2B.BeginTransaction

			arrNoPenawaranPackaging.Clear()
			SQLB2B = "select a.no_transaksi From b2b_packaging a , b2b_detail_packaging b "
			SQLB2B = SQLB2B & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.no_transaksi = b.no_transaksi "
			SQLB2B = SQLB2B & "and b.Flag_Approval = 'A' "
			SQLB2B = SQLB2B & "and a.Status is null  "
			SQLB2B = SQLB2B & "and b.Flag_Sudah_Pindah is null "
			SQLB2B = SQLB2B & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
			SQLB2B = SQLB2B & "group by a.no_transaksi "
			Using DsB2B = BindingTransB2B(SQLB2B)
				With DsB2B.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrNoPenawaranPackaging.Add(.Rows(i).Item("no_transaksi"))

					Next
				End With
			End Using

			For z As Integer = 0 To arrNoPenawaranPackaging.Count - 1

				get_no_faktur_penawaran()

				Dim flag_kategori_Supplier As String = ""
				Dim Data_Faktur_Penawaran As String = ""
				Dim arrDataDetailPenawaran As New ArrayList

				Dim hasInsert As Boolean = False

				SQLB2B = "select Kode_Perusahaan, No_Transaksi as no_faktur, No_Penawaran ,Tanggal_Mulai as Tgl_Penawaran_Hrg, "
				SQLB2B = SQLB2B & "Tanggal_Selesai as Periode_Akhir_Penawaran,Kode_Supplier, Lokasi, Tanggal, Jam, Id_User "
				SQLB2B = SQLB2B & "from B2B_Packaging where kode_perusahaan = '" & KodePerusahaan & "' "
				SQLB2B = SQLB2B & "and no_transaksi = '" & arrNoPenawaranPackaging.Item(z).ToString & "' "
				Using DsB2B = BindingTransB2B(SQLB2B)
					With DsB2B.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1

							'==================================================
							'=     CEK APAKAH ADA DATA DI PENAWARAN INDUK     =
							'==================================================
							SQL = "select Top 1 Kode_Perusahaan, No_Faktur from emi_master_penawaran where Kode_Perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' and No_Faktur_B2B = '" & .Rows(i).Item("no_faktur") & "'"
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then

									Data_Faktur_Penawaran = Dr("No_Faktur")
								Else
									Dr.Close()
									SQL = "insert into emi_master_penawaran(Kode_Perusahaan, No_Faktur, No_Faktur_B2B, No_Penawaran, Tgl_Penawaran_Hrg, Periode_Akhir_Penawaran, Kode_Supplier, Lokasi, "
									SQL = SQL & "Tanggal, Jam, iduser) values ( "
									SQL = SQL & "'" & .Rows(i).Item("kode_perusahaan") & "', '" & Faktur_Penawaran & "', '" & .Rows(i).Item("no_faktur") & "', "
									SQL = SQL & "'" & .Rows(i).Item("No_Penawaran") & "', '" & .Rows(i).Item("tgl_penawaran_hrg") & "', "
									SQL = SQL & "'" & .Rows(i).Item("periode_akhir_penawaran") & "', '" & .Rows(i).Item("kode_supplier") & "', "
									SQL = SQL & "'" & .Rows(i).Item("lokasi") & "', '" & .Rows(i).Item("tanggal") & "', '" & .Rows(i).Item("jam") & "', '" & .Rows(i).Item("Id_User") & "' )"
									ExecuteTrans(SQL)

									Data_Faktur_Penawaran = Faktur_Penawaran
								End If
							End Using

							'===================
							'=     RELEASE     =
							'===================
							'GET KATEGORI SUPPLIER
							SQL = "select b.kode_supplier, b.ID_Kategori_Suppliers, c.flag_jenis_import  from  Suppliers b, Suppliers_Kategori c "
							SQL = SQL & "where b.kode_perusahaan = c.kode_perusahaan and b.id_kategori_suppliers = c.id_kategori_suppliers "
							SQL = SQL & "and b.kode_perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' and b.Kode_Supplier = '" & .Rows(i).Item("kode_supplier") & "' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									flag_kategori_Supplier = General_Class.CekNULL(Dr("flag_jenis_import"))
								Else
									Dr.Close()
									CloseTrans()
									CloseTransB2B()
									CloseConn()
									CloseConnB2B()
									MessageBox.Show("Supplier " & .Rows(i).Item("kode_supplier") & " Tidak Ditemukan...!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub

								End If
							End Using

							'MASUK KE DETAIL PACKAGING
							SQLB2B = "select  a.kode_perusahaan, a.No_Transaksi AS no_faktur, a.kode_barang, a.MOQ, a.satuan, a.Harga, a.mata_uang, a.no_urut "
							SQLB2B = SQLB2B & "from B2B_Detail_Packaging a where "
							SQLB2B = SQLB2B & " a.Kode_Perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' "
							SQLB2B = SQLB2B & "and a.No_Transaksi = '" & .Rows(i).Item("no_faktur") & "' "
							SQLB2B = SQLB2B & "and a.Flag_Approval = 'A' "
							SQLB2B = SQLB2B & "and a.Flag_Sudah_Pindah is null "
							SQLB2B = SQLB2B & "order by a.Kode_Barang"
							Using Ds2B2B = BindingTransB2B(SQLB2B)
								For j As Integer = 0 To Ds2B2B.Tables("MyTable").Rows.Count - 1

									hasInsert = True

									Dim Detail_KdPerusahaan As String = Ds2B2B.Tables("MyTable").Rows(j).Item("kode_perusahaan")
									Dim Detail_KdBarang As String = Ds2B2B.Tables("MyTable").Rows(j).Item("kode_barang")
									'   Dim Detail_KodeSatuan As String = Ds2B2B.Tables("MyTable").Rows(j).Item("kode_satuan")
									Dim Detail_Harga As String = Ds2B2B.Tables("MyTable").Rows(j).Item("Harga")
									Dim Detail_MOQ As String = Ds2B2B.Tables("MyTable").Rows(j).Item("MOQ")
									Dim Detail_MataUang As String = Ds2B2B.Tables("MyTable").Rows(j).Item("mata_uang")
									Dim Detail_IdSatuan As String = Ds2B2B.Tables("MyTable").Rows(j).Item("Satuan")
									Dim Detail_UrutB2B As String = Ds2B2B.Tables("MyTable").Rows(j).Item("no_urut")

									'===================================
									'=     INSERT PENAWARAN DETAIL     =
									'===================================
									'GET KODE SATUAN
									'Dim Kode_Satuan As String = ""
									'SQLB2B = "select Kode_Satuan, Keterangan from b2b_satuan  "
									'SQLB2B = SQLB2B & "where Kode_Perusahaan = '" & Detail_KdPerusahaan & "' and Id_Satuan = '" & Detail_IdSatuan & "' "
									'Using DrB2B = OpenTransB2B(SQLB2B)
									'    If DrB2B.Read Then
									'        Kode_Satuan = DrB2B("Kode_Satuan")
									'    Else
									'        CloseTrans()
									'        CloseTransB2B()
									'        CloseConn()
									'        CloseConnB2B()
									'        MessageBox.Show("Satuan Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									'        Exit Sub
									'    End If
									'End Using

									' GET SATUAN KECIL BARANG
									Dim satuanBarang As String
									SQL = "select satuan from barang where "
									SQL = SQL & "kode_perusahaan = '" & Detail_KdPerusahaan & "' "
									SQL = SQL & "and kode_barang = '" & Detail_KdBarang & "' "
									'SQL = SQL & "and kode_stock_owner = '" & .Rows(i).Item("kode_stock_owner") & "' "
									Using Dr = OpenTrans(SQL)
										If Dr.Read Then
											satuanBarang = Dr("satuan")
										Else

											Dr.Close()
											CloseTrans()
											CloseTransB2B()
											CloseConn()
											CloseConnB2B()
											MessageBox.Show("Satuan " & Detail_IdSatuan & " Ke  satuan dasar Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									End Using

									' CONVERT HARGA KESATUAN KECIL
									Dim ConvertHarga As Double = 0
									SQL = "select dbo.Ubah_Satuan_baru('" & Detail_KdPerusahaan & "','" & Detail_KdBarang & "',"
									SQL = SQL & "'" & Detail_IdSatuan & "', '" & satuanBarang & "',"
									SQL = SQL & "" & Detail_Harga & " , 'UANG') as Hasil "
									Using dr4 = OpenTrans(SQL)
										If dr4.Read Then
											If General_Class.CekNULL(dr4("Hasil")) <> "" Then
												If dr4("Hasil") = 0 Then
													dr4.Close()
													CloseTrans()
													CloseTransB2B()
													CloseConn()
													CloseConnB2B()
													MessageBox.Show("Satuan " & Detail_IdSatuan & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Exit Sub
												Else
													ConvertHarga = dr4("hasil")

												End If
											Else
												dr4.Close()
												CloseTrans()
												CloseTransB2B()
												CloseConn()
												CloseConnB2B()
												MessageBox.Show("Satuan " & Detail_IdSatuan & " Ke " & satuanBarang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
												Exit Sub
											End If
										End If
									End Using

									SQL = "insert into EMI_Master_Penawaran_Detail (kode_perusahaan, no_faktur, kode_barang, min_order, satuan, Harga_Satuan, Nilai_Barang, Satuan_Barang, Mata_Uang, urut_B2B,satuan_input) values ( "
									SQL = SQL & "'" & Detail_KdPerusahaan & "' , '" & Data_Faktur_Penawaran & "', '" & Detail_KdBarang & "', "
									SQL = SQL & "'" & Detail_MOQ & "', '" & satuanBarang & "', '" & Detail_Harga & "' , "
									SQL = SQL & "'" & ConvertHarga & "', '" & satuanBarang & "', '" & Detail_MataUang & "', '" & Detail_UrutB2B & "', '" & Detail_IdSatuan & "' ) "
									ExecuteTrans(SQL)

									If flag_kategori_Supplier = "Y" Then

										'GET SATUAN KIRIM
										Dim satuan_kirim As String = ""
										SQL = "select satuan from barang_detail_satuan where kode_barang='" & Detail_KdBarang & "' "
										SQL = SQL & "and kode_Perusahaan='" & Detail_KdPerusahaan & "' and flag_kirim='Y' "
										Using dr3 = OpenTrans(SQL)
											If dr3.Read Then
												satuan_kirim = dr3("satuan")
											Else
												dr3.Close()
												CloseTrans()
												CloseTransB2B()
												CloseConn()
												CloseConnB2B()
												MessageBox.Show("data satuan kirim tidak ada ")
												Exit Sub
											End If
										End Using

										'GET NAMA BARANG
										Dim nama As String = ""
										SQL = "select top(1) nama from barang where kode_barang ='" & Detail_KdBarang & "' "
										Using Dr = OpenTrans(SQL)
											If Dr.Read Then
												nama = Dr("nama")
											End If
										End Using

										'INSERT KOMPOSISI BARANG JADI
										SQL = "select kode_Perusahaan from komposisi_barang_jadi where kode_barang='" & Detail_KdBarang & "' "
										SQL = SQL & "and kode_Perusahaan='" & Detail_KdPerusahaan & "' "
										Using dr33 = OpenTrans(SQL)
											If Not dr33.Read Then
												dr33.Close()
												SQL = "insert into komposisi_barang_jadi(kode_perusahaan, "
												SQL = SQL & "kode_barang, Qty) Values ("
												SQL = SQL & " '" & Detail_KdPerusahaan & "', '" & Detail_KdBarang & "', "
												SQL = SQL & "'1')"
												ExecuteTrans(SQL)

												SQL = "insert into detail_komposisi_barang_jadi(kode_perusahaan, "
												SQL = SQL & "kode_barang, Kode_Bahan, Qty_Bahan) Values("
												SQL = SQL & "'" & Detail_KdPerusahaan & "', '" & Detail_KdBarang & "', "
												SQL = SQL & "'" & Detail_KdBarang & "', '1')"
												ExecuteTrans(SQL)
											End If
										End Using

										'INSERT BAHAN IMPORT
										SQL = "select kode_Perusahaan from bahan_import where kode_bahan='" & Detail_KdBarang & "' "
										SQL = SQL & "and kode_Perusahaan='" & Detail_KdPerusahaan & "' "
										Using dr33 = OpenTrans(SQL)
											If Not dr33.Read Then
												dr33.Close()

												SQL = "select kode_stock_owner_import from stock_owner_import where kode_perusahaan = '" & Detail_KdPerusahaan & "' "
												SQL = SQL & "order by kode_stock_owner_import"
												Using Dsm = BindingTrans(SQL)
													If Dsm.Tables("MyTable").Rows.Count <> 0 Then
														For iii As Integer = 0 To Dsm.Tables("MyTable").Rows.Count - 1

															SQL = "Insert Into bahan_import(Kode_Perusahaan, Kode_STock_Owner_Import, kode_bahan, Kode_supplier, "
															SQL = SQL & "nama_bahan, kategori, mata_uang, harga, satuan, Flag_Potong_Stock) Values("
															SQL = SQL & "'" & Detail_KdPerusahaan & "', '" & Dsm.Tables("MyTable").Rows(iii).Item("kode_stock_owner_import") & "', "
															SQL = SQL & "'" & Detail_KdBarang & "','" & .Rows(i).Item("kode_supplier") & "',"
															SQL = SQL & "'" & nama & "', '" & "Utama" & "', '" & Detail_MataUang & "', "
															SQL = SQL & "'" & Val(HilangkanTanda(Detail_Harga)) & "','" & satuan_kirim & "', '" & "T" & "')"
															ExecuteTrans(SQL)

														Next
													Else
														CloseTrans()
														CloseTransB2B()
														CloseConn()
														CloseConnB2B()
														MessageBox.Show("Data lokasi import tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Exit Sub
													End If

												End Using
											End If
										End Using

										'UPDATE emi_master_penawaran_detail
										SQL = "update emi_master_penawaran_detail set "
										SQL = SQL & "flag_baru = 'Y' "
										SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Kode_barang='" & Detail_KdBarang & "' "
										SQL = SQL & "and no_faktur='" & .Rows(i).Item("No_Faktur") & "' "
										ExecuteTrans(SQL)

									End If

									SQLB2B = "Update B2B_Detail_Packaging set flag_sudah_pindah = 'Y' where "
									SQLB2B = SQLB2B & "Kode_Perusahaan = '" & Detail_KdPerusahaan & "' "
									SQLB2B = SQLB2B & "and no_transaksi = '" & .Rows(i).Item("no_faktur") & "' "
									SQLB2B = SQLB2B & "and no_urut = '" & Detail_UrutB2B & "' "
									ExecuteTransB2B(SQLB2B)

								Next

							End Using

							'==================================================================
							'=     CEK APAKAH EMI_MASTER_PENAWARAN_DETAIL SUDAH DI INSERT     =
							'==================================================================
							SQL = "select top 1 * from EMI_Master_Penawaran_Detail where Kode_Perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' and No_Faktur = '" & Data_Faktur_Penawaran & "' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
								Else
									Dr.Close()
									CloseTrans()
									CloseTransB2B()
									CloseConn()
									CloseConnB2B()
									MessageBox.Show("Ada Masalah saat Insert Detail Penawaran", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using

							'MASUK KE Penawaran Jatuh Tempo
							SQLB2B = "select no_urut,kode_perusahaan,no_faktur,no_penawaran,jenis_pembayaran,tempo_pembayaran,lama_pembayaran "
							SQLB2B = SQLB2B & "from B2B_Jatuh_Tempo_Packaging a "

							SQLB2B = SQLB2B & "where a.Kode_Perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' "
							SQLB2B = SQLB2B & "and a.no_faktur = '" & .Rows(i).Item("no_faktur") & "' "

							SQLB2B = SQLB2B & "order by no_urut "
							Using Ds2B2B = BindingTransB2B(SQLB2B)
								For j As Integer = 0 To Ds2B2B.Tables("MyTable").Rows.Count - 1

									hasInsert = True

									Dim Detail_KdPerusahaanT As String = Ds2B2B.Tables("MyTable").Rows(j).Item("kode_perusahaan")
									Dim no_urutT As String = Ds2B2B.Tables("MyTable").Rows(j).Item("no_urut")
									Dim no_fakturT As String = Ds2B2B.Tables("MyTable").Rows(j).Item("no_faktur")
									Dim no_penawaranT As String = Ds2B2B.Tables("MyTable").Rows(j).Item("no_penawaran")
									Dim jenis_pembayaranT As String = Ds2B2B.Tables("MyTable").Rows(j).Item("jenis_pembayaran")

									Dim tempo_pembayaranT As String = ""
									If IsDBNull(Ds2B2B.Tables("MyTable").Rows(j).Item("tempo_pembayaran")) Then
										tempo_pembayaranT = "NULL"
									Else
										tempo_pembayaranT = "'" & Ds2B2B.Tables("MyTable").Rows(j).Item("tempo_pembayaran") & "'"
									End If

									Dim lama_pembayaranT As String = ""
									If IsDBNull(Ds2B2B.Tables("MyTable").Rows(j).Item("lama_pembayaran")) Then
										lama_pembayaranT = "NULL"
									Else
										lama_pembayaranT = "'" & Ds2B2B.Tables("MyTable").Rows(j).Item("lama_pembayaran") & "'"
									End If

									SQL = "insert into EMI_Master_Penawaran_Jatuh_Tempo(kode_perusahaan,no_faktur,no_faktur_b2b,no_penawaran, "
									SQL = SQL & "urut_b2b,jenis_pembayaran,tempo_pembayaran,lama_pembayaran) values ( "
									SQL = SQL & "'" & Detail_KdPerusahaanT & "','" & Data_Faktur_Penawaran & "', '" & no_fakturT & "', '" & no_penawaranT & "' ,"
									SQL = SQL & "'" & no_urutT & "', '" & jenis_pembayaranT & "', " & tempo_pembayaranT & ", " & lama_pembayaranT & ""
									SQL = SQL & ")"
									ExecuteTrans(SQL)

								Next

							End Using

							If hasInsert Then
								'UPDATE FLAG RELEASE
								SQL = "update EMI_Master_Penawaran set "
								SQL = SQL & "flag_release = 'Y', "
								SQL = SQL & "Tanggal_Release = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
								SQL = SQL & "jam_release = '" & Format(tgl_skg, "HH:mm:ss") & "' , "
								SQL = SQL & "iduser_release = '" & UserID & "' "
								SQL = SQL & "where kode_perusahaan = '" & .Rows(i).Item("kode_perusahaan") & "' and No_Faktur='" & Data_Faktur_Penawaran & "' "
								SQL = SQL & "and no_penawaran='" & .Rows(i).Item("No_Penawaran") & "' "
								ExecuteTrans(SQL)
							End If

						Next
					End With
				End Using
			Next

			Cmd.Transaction.Commit()
			CmdB2B.Transaction.Commit()
			CloseConn()
			CloseConnB2B()
		Catch ex As Exception
			CloseTrans()
			CloseTransB2B()
			CloseConn()
			CloseConnB2B()
			MessageBox.Show(ex.Message & " Insert Penawaran Packaging")
			Exit Sub
		End Try
	End Sub

	Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
		Handle_Proses_Waste_ProductStock()
	End Sub

	Private Sub Button5_Click(sender As Object, e As EventArgs) Handles btnSupplierInsert.Click
		get_jam()
		Try
			OpenConn()
			OpenConnB2B()
			Cmd.Transaction = Cn.BeginTransaction
			CmdB2B.Transaction = CnB2B.BeginTransaction

			arrKodeSupplier.Clear()
			SQL = "select kode_supplier from suppliers where "
			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
			SQL = SQL & "flag_sudah_pindah is null"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrKodeSupplier.Add(.Rows(i).Item("kode_supplier"))
					Next
				End With
			End Using

			Dim j As Integer = 0
			For z As Integer = 1 To arrKodeSupplier.Count

				SQL = "select Kode_Perusahaan,Kode_Supplier,Nama,Alamat,Pemilik,Telepon,Fax,Contact_Person,HP_CP,Hutang,Kode_Kategori,Inisial_Sup	,Tampil_Di_PO, "
				SQL = SQL & "Negara,Kota,Port,Kategori_Import,Nama_Supplier,PIC,Mata_Uang_Rek,Mata_Uang_Declare,Mata_Uang_Bayar,Format_Faktur,Flag_Average	,Flag_Validasi_Declare, "
				SQL = SQL & "Perhitungan_Jatuh_Tempo,Ket_Perhitungan_Jatuh_Tempo,Flag_Form_E,	Biaya_Form_E,	Jenis_Laporan_Import,	Metode_Selisih_Declare, "
				SQL = SQL & "flag_gabung_declare,ID_Kategori_Suppliers	 "
				SQL = SQL & "from suppliers where kode_perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "flag_sudah_pindah is null and "
				SQL = SQL & "kode_supplier = '" & arrKodeSupplier.Item(j).ToString & "'"
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							'Insert ke MySql
							SQLB2B = "insert into suppliers (Kode_Perusahaan,Kode_Supplier,Nama,Alamat,Pemilik,Telepon,Fax,Contact_Person,HP_CP,Hutang,Kode_Kategori,Inisial_Sup	,Tampil_Di_PO,  "
							SQLB2B = SQLB2B & "Negara,Kota,Port,Kategori_Import,Nama_Supplier,PIC,Mata_Uang_Rek,Mata_Uang_Declare,Mata_Uang_Bayar,Format_Faktur,Flag_Average	,Flag_Validasi_Declare,"
							SQLB2B = SQLB2B & "Perhitungan_Jatuh_Tempo,Ket_Perhitungan_Jatuh_Tempo,Flag_Form_E,	Biaya_Form_E,	Jenis_Laporan_Import,	Metode_Selisih_Declare,"
							SQLB2B = SQLB2B & "flag_gabung_declare,ID_Kategori_Suppliers "
							SQLB2B = SQLB2B & ")"
							SQLB2B = SQLB2B & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("Kode_Supplier") & "',"
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Nama") & "', '" & .Rows(i).Item("Alamat") & "', "
							SQLB2B = SQLB2B & " '" & .Rows(i).Item("Pemilik") & "', '" & .Rows(i).Item("Telepon") & "', '" & .Rows(i).Item("Fax") & "' , '" & .Rows(i).Item("Contact_Person") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("HP_CP") & "' , '" & .Rows(i).Item("Hutang") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Kode_Kategori") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Inisial_Sup") & "','" & .Rows(i).Item("Tampil_Di_PO") & "', '" & .Rows(i).Item("Negara") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Kota") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Port") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Kategori_Import") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Nama_Supplier") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("PIC") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Mata_Uang_Rek") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Mata_Uang_Declare") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Mata_Uang_Bayar") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Format_Faktur") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Flag_Average") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Flag_Validasi_Declare") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Perhitungan_Jatuh_Tempo") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Ket_Perhitungan_Jatuh_Tempo") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Flag_Form_E") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Biaya_Form_E") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Jenis_Laporan_Import") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("Metode_Selisih_Declare") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("flag_gabung_declare") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("ID_Kategori_Suppliers") & "' "
							SQLB2B = SQLB2B & ")"
							ExecuteTransB2B(SQLB2B)

						Next
					End With
				End Using

				SQL = "Update suppliers set flag_sudah_pindah = 'Y' where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
				SQL = SQL & "kode_supplier = '" & arrKodeSupplier.Item(j).ToString & "' "
				ExecuteTrans(SQL)

				j = j + 1
			Next

			Cmd.Transaction.Commit()
			CmdB2B.Transaction.Commit()
			CloseConn()
			CloseConnB2B()
		Catch ex As Exception
			CloseTrans()
			CloseTransB2B()
			CloseConn()
			CloseConnB2B()
			MessageBox.Show(ex.Message & " Insert Suppliers")
			Exit Sub
		End Try
	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
		get_jam()
		Try
			OpenConn()
			OpenConnB2B()
			Cmd.Transaction = Cn.BeginTransaction
			CmdB2B.Transaction = CnB2B.BeginTransaction

			arrNoPo2.Clear()
			SQL = "select no_faktur from EMI_Pembelian_Loading where flag_sdh_update ='Y' order by no_faktur"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrNoPo2.Add(.Rows(i).Item("no_faktur"))
					Next
				End With
			End Using

			Dim j As Integer = 0
			For z As Integer = 1 To arrNoPo2.Count

				'================================================
				' update tanggal masuk b2b_purchase_order
				'================================================

				SQL = "select No_Faktur,tanggal_masuk, flag_sudah_bongkar_android from EMI_Pembelian_Loading where Status is null and Flag_Sdh_Update = 'Y'  "
				SQL = SQL & "and no_faktur = '" & arrNoPo2.Item(j).ToString() & "'"
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1

							Dim flagSelesai As String = ""
							If General_Class.CekNULL(.Rows(i).Item("flag_sudah_bongkar_android")) = "" Then
								flagSelesai = "NULL"
							Else
								flagSelesai = "'" & .Rows(i).Item("flag_sudah_bongkar_android").ToString() & "'"
							End If
							SQLB2B = "update B2B_Purchase_Order set "
							SQLB2B = SQLB2B & "flag_sampai = " & flagSelesai & ", "
							SQLB2B = SQLB2B & "actual_ta = '" & .Rows(i).Item("tanggal_masuk") & "' "
							SQLB2B = SQLB2B & "where kode_perusahaan = '" & KodePerusahaan & "'  "
							SQLB2B = SQLB2B & "and no_faktur = '" & .Rows(i).Item("no_faktur") & "' "
							ExecuteTransB2B(SQLB2B)

						Next
					End With
				End Using

				'================================================
				' update Jumlah masuk b2b_detail_batch_purchase_order
				'================================================

				SQL = "select a.No_Faktur,a.no_urut_b2b, "
				SQL = SQL & "dbo.Ubah_Satuan(a.kode_perusahaan,'MASA',a.Kode_Barang,a.Satuan_Barang,b.Satuan,a.Jumlah_Masuk) as Jumlah_Masuk,b.Satuan "
				SQL = SQL & "from EMI_Pembelian_Loading_Detail a, Barang_Detail_Satuan b "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_barang "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & arrNoPo2.Item(j).ToString() & "'  and b.Flag_Tampil_Display = 'Y'"

				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1

							SQLB2B = "update B2B_detail_batch_Purchase_Order set "
							SQLB2B = SQLB2B & "jumlah_masuk = '" & .Rows(i).Item("Jumlah_Masuk") & "' "
							SQLB2B = SQLB2B & "where urut_oto = '" & .Rows(i).Item("no_urut_b2b") & "' "
							ExecuteTransB2B(SQLB2B)
						Next
					End With
				End Using

				SQL = "Update EMI_Pembelian_Loading set flag_sdh_update = null where "
				SQL = SQL & "no_faktur = '" & arrNoPo2.Item(j).ToString & "' "
				ExecuteTrans(SQL)

				j = j + 1
			Next

			Cmd.Transaction.Commit()
			CmdB2B.Transaction.Commit()
			CloseConn()
			CloseConnB2B()
		Catch ex As Exception
			CloseTrans()
			CloseTransB2B()
			CloseConn()
			CloseConnB2B()
			MessageBox.Show(ex.Message & " update purchase order")
			Exit Sub
		End Try
	End Sub

	Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

		Button22_Click(Timer1, e)
		Button23_Click(Timer1, e)

	End Sub

	Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click

		Dim listINSERT, listEDIT, listDELETE As New ArrayList

		Try

			OpenConnB2B()

			'==================
			'=     INSERT     =
			'==================
			SQLB2B = "select top 1 Kode_Perusahaan, 'B2B_Purchase_Order' as dari from B2B_Purchase_Order where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null and flag_sudah_pindah is null and flag_selesai = 'Y' "
			SQLB2B = SQLB2B & "union all "
			SQLB2B = SQLB2B & "select top 1 a.Kode_Perusahaan, 'B2B_Penawaran_Bahan_Baku' as dari From B2B_Penawaran_Bahan_Baku a , B2B_Penawaran_Bahan_Baku_Detail b where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
			SQLB2B = SQLB2B & "and a.Status is null and b.Flag_Approval = 'A' and b.Flag_Sudah_Pindah is null and a.kode_perusahaan = '" & KodePerusahaan & "' "
			SQLB2B = SQLB2B & "union all "
			SQLB2B = SQLB2B & "select top 1 a.Kode_Perusahaan, 'b2b_packaging' as dari From b2b_packaging a , b2b_detail_packaging b where a.Kode_Perusahaan = b.Kode_Perusahaan and a.no_transaksi = b.no_transaksi  "
			SQLB2B = SQLB2B & "and b.Flag_Approval = 'A' and a.Status is null and b.Flag_Sudah_Pindah is null and a.kode_perusahaan = '" & KodePerusahaan & "' "
			SQLB2B = SQLB2B & "union all "
			SQLB2B = SQLB2B & "select c.No_Faktur, 'Expedisi' as dari from B2B_Detail_Vehicle_Expedition a, B2B_Detail_Expedition b, B2B_Expedition c "
			SQLB2B = SQLB2B & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Detail_Expedition = b.No_Urut and b.No_Penawaran = c.No_Penawaran "
			SQLB2B = SQLB2B & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and c.Status is null and a.Flag_Approval = 'A' and a.Flag_Sudah_Pindah is null  "
			Using DsSQL = BindingTransB2B(SQLB2B)
				With DsSQL.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						Select Case .Rows(i).Item("dari").ToString()
							Case "B2B_Purchase_Order"
								listINSERT.Add("B2B_Purchase_Order")
							Case "B2B_Penawaran_Bahan_Baku"
								listINSERT.Add("B2B_Penawaran_Bahan_Baku")
							Case "b2b_packaging"
								listINSERT.Add("b2b_packaging")
							Case "Expedisi"
								listINSERT.Add("Expedisi")
						End Select
					Next
				End With
			End Using

			'================
			'=     EDIT     =
			'================
			SQLB2B = "select top 1 a.Kode_Perusahaan, 'EMI_Pembelian_PO' as dari from EMI_Pembelian_PO a, EMI_Pembelian_PO_Detail b where a.Status is null and a.flag_selesai_po = 'Y' and a.Kode_Perusahaan = b.Kode_Perusahaan "
			SQLB2B = SQLB2B & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and b.Flag_Sudah_Kirim = 'Y' and flag_edit_po = 'Y' and a.kode_perusahaan = '" & KodePerusahaan & "' "
			Using DsSQL = BindingTransB2B(SQLB2B)
				With DsSQL.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						Select Case .Rows(i).Item("dari").ToString()
							Case "EMI_Pembelian_PO"
								listEDIT.Add("EMI_Pembelian_PO")
						End Select

					Next
				End With
			End Using

			CloseConnB2B()
		Catch ex As Exception
			CloseTransB2B()
			CloseConnB2B()

			Dim Lvw As ListViewItem
			Lvw = ListView1.Items.Add("MySql-Sql : " & ex.Message)
			'MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'Execute INSERT

		For index As Integer = 0 To listINSERT.Count - 1
			Dim item As Object = listINSERT(index)
			Select Case item
				Case "B2B_Purchase_Order"
					Button1_Click(Button1, e)
				Case "B2B_Penawaran_Bahan_Baku"
					btnPnwrBahanBaku_Click(btnPnwrBahanBaku, e)
				Case "b2b_packaging"
					btnPenawaranPackaging_Click(btnPenawaranPackaging, e)
				Case "Expedisi"
					Button6_Click(Btn_InsExpedisi, e)
			End Select
		Next

		'Execute EDIT
		For index As Integer = 0 To listEDIT.Count - 1
			Dim item As Object = listEDIT(index)
			Select Case item
				Case "EMI_Pembelian_PO"
					Button5_Click_1(Button5, e)
			End Select
		Next

	End Sub

	Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click

		Dim listINSERT, listEDIT As New ArrayList

		Try
			OpenConn()

			'==================
			'=     INSERT     =
			'==================
			SQL = "select top 1 Kode_Perusahaan, 'Pembelian_PO' as dari from emi_pembelian_po where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sudah_pindah is null "
			SQL = SQL & "union all "
			SQL = SQL & "select top 1 kode_perusahaan, 'Barang' as dari from barang where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Sudah_Pindah is null "
			SQL = SQL & "union all "
			SQL = SQL & "select top 1 kode_perusahaan, 'Supplier' as dari from suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Sudah_Pindah is null "
			SQL = SQL & "union all "
			SQL = SQL & "select top 1 kode_perusahaan, 'Biaya_Lokal' as dari from Biaya_B2B where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Sudah_Pindah is null "
			SQL = SQL & "union all "
			SQL = SQL & "select top 1 kode_perusahaan, 'Master_Kendaraan' as dari from Master_Kendaraan where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Pindah is null "
			SQL = SQL & "union all "
			SQL = SQL & "select top 1 kode_perusahaan, 'Master_Satuan' as dari from n_emi_master_satuan where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Sdh_Pindah is null "
			SQL = SQL & "union all "
			SQL = SQL & "select top 1 kode_perusahaan, 'Master_Perusahaan_Biaya' as dari from Perusahaan_biaya_Import where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sudah_pindah is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							Select Case .Rows(i).Item("dari").ToString()
								Case "Pembelian_PO"
									listINSERT.Add("Pembelian_PO")
								Case "Barang"
									listINSERT.Add("Barang")
								Case "Supplier"
									listINSERT.Add("Supplier")
								Case "Biaya_Lokal"
									listINSERT.Add("Biaya_Lokal")
								Case "Master_Satuan"
									listINSERT.Add("Master_Satuan")
								Case "Master_Kendaraan"
									listINSERT.Add("Master_Kendaraan")
								Case "Master_Perusahaan_Biaya"
									listINSERT.Add("Master_Perusahaan_Biaya")
							End Select
						Next
					End If
				End With
			End Using

			'================
			'=     EDIT     =
			'================
			SQL = "select top 1 kode_perusahaan, 'Pembelian_Loading' as dari from EMI_Pembelian_Loading where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_update = 'Y' "
			SQL = SQL & "Union all "
			SQL = SQL & "select top 1 kode_perusahaan, 'Biaya_B2B' as dari from Biaya_B2B where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Update = 'Y' "
			SQL = SQL & "Union all "
			SQL = SQL & "select top 1 kode_perusahaan, 'Master_Kendaraan' as dari from Master_Kendaraan where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Update = 'Y' "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							Select Case .Rows(i).Item("dari").ToString()
								Case "Pembelian_Loading"
									listEDIT.Add("Pembelian_Loading")
								Case "Biaya_B2B"
									listEDIT.Add("Biaya_B2B")
								Case "Master_Kendaraan"
									listEDIT.Add("Master_Kendaraan")
							End Select
						Next
					End If
				End With
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			Dim Lvw As ListViewItem
			Lvw = ListView1.Items.Add("Sql - MySql : " & ex.Message)
			'MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'Execute INSERT
		For index As Integer = 0 To listINSERT.Count - 1
			Dim item As Object = listINSERT(index)

			Select Case item
				Case "Pembelian_PO"
					Button2_Click(Button2, e)
				Case "Barang"
					Button4_Click(Button4, e)
				Case "Supplier"
					Button5_Click(btnSupplierInsert, e)
				Case "Biaya_Lokal"
					Btn_BiayaLokal_Click(Btn_BiayaLokal, e)
				Case "Master_Kendaraan"
					Btn_InsKendaraan_Click(Btn_InsKendaraan, e)
				Case "Master_Satuan"
					Button7_Click(btn_Master_Satuan, e)
				Case "Master_Perusahaan_Biaya"
					Button6_Click_1(btnPerusahaanBIaya, e)
			End Select

		Next

		'Execute EDIT
		For index As Integer = 0 To listEDIT.Count - 1
			Dim item As Object = listEDIT(index)

			Select Case item
				Case "Pembelian_Loading"
					Button3_Click(Button3, e)
				Case "Biaya_B2B"
					Btn_Update_BiayaLokal_Click(Btn_Update_BiayaLokal, e)
				Case "Master_Kendaraan"
					Btn_UpdateKendaraan_Click(Btn_UpdateKendaraan, e)
			End Select
		Next

		Button6_Click_2(Button6, e)
		Button7_Click_1(Button7, e)
		Button8_Click(Button8, e)
		Button9_Click(Button9, e)
	End Sub

	Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Try
			OpenConn()
			OpenConnB2B()
			Cmd.Transaction = Cn.BeginTransaction
			CmdB2B.Transaction = CnB2B.BeginTransaction

			arrNo_Fak.Clear()
			SQL = "select no_faktur from penjualan_proyek_sementara where "
			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and flag_sdh_pindah is null and status is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrNo_Fak.Add(.Rows(i).Item("no_faktur"))
					Next
				End With
			End Using

			SQL = "select Kode_Perusahaan,No_Faktur,Tanggal,Jam,Kode_Customer,Jenis_Transaksi,Tgl_Jatuh_Tempo, "
			SQL = SQL & "Flag_Lunas,Tgl_Lunas,Jam_Lunas,Flag_Lunas_Tunai,Tgl_Lunas_Tunai,Jam_Lunas_Tunai,UserValidasi_Tunai,UserID,UserValidasi, "
			SQL = SQL & "Disc1,Disc2,Disc_Cash,Status,Terbilang,Grand,Bayar,Kode_Sales,PPN,Pembeda,Total_Point, "
			SQL = SQL & "Flag_Lipat,Kurs,Pakai_Point,Diskon_Promo,Diskon_Rupiah,Flag_Tagihan, "
			SQL = SQL & "Kode_CB,RV,No_Surat_Jalan,Lokasi,Lokasi_Gdg,Total,Total_U_Dis_Member, "
			SQL = SQL & "Hasil_Diskon,Hasil_Diskon_Cash,Kode_Voucher_1,Kode_Voucher_2,Kode_Voucher_3,Kode_Voucher_4,Kode_Voucher_5,Kode_Voucher_6,Kode_Voucher_7,Kode_Voucher_8, "
			SQL = SQL & "Jenis,Nilai_PPN,No_PO_Toko,No_DO,Kode_Karyawan,Flag_Cabang_Sendiri,Flag_Cabang_Agency,COA_Piutang,Sudah_FK,No_Fak_Sebelumnya,No_Fak_Setelahnya, "
			SQL = SQL & "Init_Custm,Ket_Custm,No_KB,Jns,Akun_Kas,Akun_Piutang,Akun_Piutang_Sementara,Disc_Tambahan,Hasil_Disc_Tambahan,Harus_Retur_Semua, "
			SQL = SQL & "Persen_Insentif_1,Persen_Insentif_2,Nilai_Insentif_1,Nilai_Insentif_2,Akun_Biaya_Insentif_1,Akun_Biaya_Insentif_2,Akun_Hutang_Insentif_1,Akun_Hutang_Insentif_2, "
			SQL = SQL & "Kepala,Sudah_Voucher,Tanggal_Voucher,Jam_Voucher,User_Voucher,Sudah_Upload,Sudah_Kirim,Flag_DO_Selesai, "
			SQL = SQL & "No_Faktur_Pajak,Tgl_Faktur_Pajak,Jam_Faktur_Pajak,User_Faktur_Pajak,Subtotal_Faktur_Pajak,PPN_Faktur_Pajak,Grand_Faktur_Pajak, "
			SQL = SQL & "Val_Diskon_Cash,Tgl_Jurnal_Diskon_Cash,Tgl_Val_Diskon_Cash,Jam_Val_Diskon_Cash,User_Val_Diskon_Cash,Nilai_Val_Diskon_Cash,PPN_Val_Diskon_Cash,Ket_Val_Diskon_Cash, "
			SQL = SQL & "CB_Val_Diskon_Cash,Akun_Val_Diskon_Cash,Kode_Voucher_Diskon_Cash,zzz,z1,Diskon_Sementara,Nilai_Diskon_Sementara,Harus_Diupdate,Lama_Diskon_Sementara, "
			SQL = SQL & "Metode_Pot_Stock,Flag_Opm,Flag_Sudah_Ke_Pusat,Flag_Sementara_Saat_Opm,Flag_ACC_Plafon,User_ACC_Plafon,Harus_Updtttt,Hrs_Updatex,Nfpx,Total_Stlh_Diskon, "
			SQL = SQL & "metode_budgeting,Mulai_Deskcall,tgl_input,Flag_Lunas_tes,Flag_Lunas_Tunai_tes, "
			SQL = SQL & "Kode_Sub_Pekerjaan,Id_Sub_Pekerjaan,Flag_Sdh_Pindah,Keterangan,Kode_Unik,Flag_Pindah,Flag_ACC,Tanggal_ACC,Jam_ACC "
			SQL = SQL & "from penjualan_proyek_sementara where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null and status is null "

			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						SQLB2B = "insert into penjualan_proyek_sementara (kode_perusahaan,no_faktur,tanggal,jam, "
						SQLB2B = SQLB2B & "userid,disc_cash,lokasi,hasil_diskon_cash, "
						SQLB2B = SQLB2B & "kode_voucher_2,flag_cabang_agency,disc_tambahan,hasil_disc_tambahan, "
						SQLB2B = SQLB2B & "persen_insentif_1,persen_insentif_2,nilai_insentif_1,nilai_insentif_2, "
						SQLB2B = SQLB2B & "nilai_val_diskon_cash,diskon_sementara,nilai_diskon_sementara,lama_diskon_sementara, "
						SQLB2B = SQLB2B & "metode_pot_stock,tgl_input,sub_pekerjaan_id, keterangan)"
						SQLB2B = SQLB2B & "values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "', '" & Format(.Rows(i).Item("tanggal"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("jam") & "', "
						SQLB2B = SQLB2B & "'" & .Rows(i).Item("userid") & "', '" & .Rows(i).Item("disc_cash") & "', '" & .Rows(i).Item("lokasi") & "', '" & .Rows(i).Item("hasil_diskon_cash") & "', "
						SQLB2B = SQLB2B & "'" & .Rows(i).Item("kode_voucher_2") & "', '" & .Rows(i).Item("flag_cabang_agency") & "', '" & .Rows(i).Item("disc_tambahan") & "', '" & .Rows(i).Item("hasil_disc_tambahan") & "', "
						SQLB2B = SQLB2B & "'" & .Rows(i).Item("persen_insentif_1") & "', '" & .Rows(i).Item("persen_insentif_2") & "', '" & .Rows(i).Item("nilai_insentif_1") & "', '" & .Rows(i).Item("nilai_insentif_2") & "', "
						SQLB2B = SQLB2B & "'" & .Rows(i).Item("nilai_val_diskon_cash") & "', '" & .Rows(i).Item("diskon_sementara") & "', '" & .Rows(i).Item("nilai_diskon_sementara") & "', '" & .Rows(i).Item("lama_diskon_sementara") & "', "
						SQLB2B = SQLB2B & "'" & .Rows(i).Item("metode_pot_stock") & "', '" & Format(.Rows(i).Item("tgl_input"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("id_sub_pekerjaan") & "', '" & .Rows(i).Item("keterangan") & "') "
						ExecuteTransB2B(SQLB2B)

						'----Insert Approval ('RandomPIN 6 Digit,'RandomKodeUnik, RandomKodeUrl 10 Digit)

						SQLB2B = "select id from users where flag_acc_pengeluaran ='Y'"
						Using DsSQL = BindingTransB2B(SQLB2B)
							For indexxx As Integer = 0 To DsSQL.Tables("MyTable").Rows.Count - 1

								Dim randomNumber As New Random()
								Dim randomPIN As Integer = randomNumber.Next(100000, 1000000)
								Dim randomKdUnik As Integer = randomNumber.Next(1000000000, Integer.MaxValue)
								Dim randomKdUrl As Integer = randomNumber.Next(1000000000, Integer.MaxValue)

								SQLB2B = "insert into approval_penjualan_proyek (penjualan_proyek_id, pin, kode_unik, kode_url, user_id) "
								SQLB2B = SQLB2B & "values ('" & .Rows(i).Item("no_faktur") & "', '" & randomPIN.ToString & "', '" & randomKdUnik.ToString & "', '" & randomKdUrl.ToString & "','" & DsSQL.Tables("MyTable").Rows(indexxx).Item("id") & "')"
								ExecuteTransB2B(SQLB2B)

							Next
						End Using

					Next
				End With
			End Using

			Dim j As Integer = 0
			For z As Integer = 1 To arrNo_Fak.Count
				SQL = "select Kode_Perusahaan,No_Faktur,No_Urut,Kode_Stock_Owner,Kode_Barang,Serial_Number,"
				SQL = SQL & "Keterangan,Jumlah,Harga,Persen_Diskon,Nilai_Diskon,x,kett,"
				SQL = SQL & "Harga_Min,Usermin,Pakai_SN,Barang_Hadiah,Modal,Nota_Kecil,"
				SQL = SQL & "Kode_Marketing,Subtotal,Flag_Sdr,Kode_Paket,Flag_Budgeting,Flag_Budgeting_Mbl,Flag_Budgeting_2,"
				SQL = SQL & "Harga_Terendah,Harga_Agen,Jml_Retur_Di_Pjk,Jml_Retur_Lain_Di_Pjk,Jml_Buat_Di_Pjk,Subtotal_Di_Pjk,"
				SQL = SQL & "Kode_Paket_2,Flag_Budgeting_3,Flag_Budgeting_4,Metode_Perhitungan,mdl,rvz,Flag_budgeting_new,Isi_Satuan_Besar "
				SQL = SQL & "from detail_penjualan_proyek_sementara "
				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_faktur = '" & arrNo_Fak.Item(j).ToString & "'"

				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")

						For i As Integer = 0 To .Rows.Count - 1

							SQLB2B = "insert into detail_penjualan_proyek_sementara (kode_perusahaan,no_faktur,no_urut,kode_stock_owner,kode_barang,jumlah) "
							SQLB2B = SQLB2B & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "', '" & .Rows(i).Item("no_urut") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("kode_stock_owner") & "', '" & .Rows(i).Item("kode_barang") & "', '" & .Rows(i).Item("jumlah") & "') "
							ExecuteTransB2B(SQLB2B)

						Next
					End With
				End Using

				'------------------------------------------------------

				SQL = "select a.Kode_Perusahaan, a.No_faktur, a.no_Urut, a.Kode_stock_owner, a.Kode_barang, a.serial_number, "
				SQL = SQL & "a.jumlah, a.id_proyek, a.id_subproyek, a.urut_oto, "
				SQL = SQL & "ISNULL((select keterangan from web_proyeks x where x.id = a.Id_Proyek "
				SQL = SQL & "),'-') as Proyek, "
				SQL = SQL & "ISNULL((select keterangan from web_subproyeks x where x.id = a.Id_Subproyek "
				SQL = SQL & "),'-') as SubProyek "
				SQL = SQL & "from det_penj_proyek_sementara a "
				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_faktur = '" & arrNo_Fak.Item(j).ToString & "' "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")

						For i As Integer = 0 To .Rows.Count - 1

							SQLB2B = "insert into det_penj_proyek_sementara (kode_perusahaan, no_faktur, no_urut, kode_stock_owner, kode_barang, serial_number, "
							SQLB2B = SQLB2B & "jumlah, proyek_id, sub_proyek_id, urut_oto,proyek,subproyek) "
							SQLB2B = SQLB2B & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "',"
							SQLB2B = SQLB2B & " '" & .Rows(i).Item("no_urut") & "', '" & .Rows(i).Item("kode_stock_owner") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("kode_barang") & "', '" & .Rows(i).Item("serial_number") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("jumlah") & "', "
							'Id Proyek
							If General_Class.CekNULL((.Rows(i).Item("id_proyek"))) = "" Then
								SQLB2B = SQLB2B & "NULL, "
							Else
								SQLB2B = SQLB2B & "'" & .Rows(i).Item("id_proyek") & "', "
							End If
							'Id SubProyek
							If General_Class.CekNULL((.Rows(i).Item("id_subproyek"))) = "" Then
								SQLB2B = SQLB2B & "NULL, "
							Else
								SQLB2B = SQLB2B & "'" & .Rows(i).Item("id_subproyek") & "', "
							End If
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("urut_oto") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("proyek") & "', "
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("subproyek") & "')"
							ExecuteTransB2B(SQLB2B)

						Next

					End With
				End Using

				SQL = "update penjualan_proyek_sementara set flag_sdh_pindah = 'Y' where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
				SQL = SQL & "No_faktur = '" & arrNo_Fak.Item(j).ToString & "' "
				ExecuteTrans(SQL)

				j = j + 1
			Next

			Cmd.Transaction.Commit()
			CmdB2B.Transaction.Commit()
			CloseConn()
			CloseConnB2B()
		Catch ex As Exception
			CloseTrans()
			CloseTransB2B()
			CloseConn()
			CloseConnB2B()
			MessageBox.Show(ex.Message & "insert penjualan proyek sementara")
			Exit Sub
		End Try
	End Sub

	Private Sub Button25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Try
			OpenConn()
			OpenConnB2B()
			Cmd.Transaction = Cn.BeginTransaction
			CmdB2B.Transaction = CnB2B.BeginTransaction

			arrEdit.Clear()
			SQLB2B = "select no_faktur from penjualan_proyek_sementara where "
			SQLB2B = SQLB2B & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
			SQLB2B = SQLB2B & " flag_sdh_update = 'Y' "
			Using Ds = BindingTransB2B(SQLB2B)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrEdit.Add(.Rows(i).Item("no_faktur"))
					Next
				End With
			End Using

			Dim a As Integer = 0
			For b As Integer = 1 To arrEdit.Count
				SQLB2B = "select kode_perusahaan, no_faktur, flag_acc , tanggal_acc, jam_acc from penjualan_proyek_sementara rpp "
				SQLB2B = SQLB2B & "where kode_perusahaan ='" & KodePerusahaan & "' and no_faktur ='" & arrEdit.Item(a).ToString & "' and flag_sdh_update ='Y' "
				Using DsSQL = BindingTransB2B(SQLB2B)
					With DsSQL.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							Dim flag_acc As String = "NULL"
							Dim tanggal_acc As String = "NULL"
							Dim jam_acc As String = "NULL"

							If General_Class.CekNULL(.Rows(i).Item("flag_acc")) <> "" Then
								flag_acc = "'" & .Rows(i).Item("flag_acc") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("tanggal_acc")) <> "" Then
								tanggal_acc = "'" & Format(.Rows(i).Item("tanggal_acc"), "yyyy-MM-dd") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("jam_acc")) <> "" Then
								jam_acc = "'" & .Rows(i).Item("jam_acc") & "'"
							End If

							SQL = "update penjualan_proyek_sementara set flag_acc=" & flag_acc & ", tanggal_acc=" & tanggal_acc & ", jam_acc=" & jam_acc & " "
							SQL = SQL & "where kode_perusahaan='" & .Rows(i).Item("Kode_Perusahaan") & "' "
							SQL = SQL & "and no_faktur='" & .Rows(i).Item("No_Faktur") & "'"
							ExecuteTrans(SQL)
						Next
					End With
				End Using

				SQLB2B = "update penjualan_proyek_sementara set flag_sdh_update = null where "
				SQLB2B = SQLB2B & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
				SQLB2B = SQLB2B & "No_faktur = '" & arrEdit.Item(a).ToString & "' "
				ExecuteTransB2B(SQLB2B)
				a = a + 1
			Next

			Cmd.Transaction.Commit()
			CmdB2B.Transaction.Commit()
			CloseConn()
			CloseConnB2B()
		Catch ex As Exception
			CloseTrans()
			CloseTransB2B()
			CloseConn()
			CloseConnB2B()
			MessageBox.Show(ex.Message & "edit penjualan web")
			Exit Sub
		End Try
	End Sub

	Private Sub Button26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Try
			OpenConn()
			OpenConnB2B()
			Cmd.Transaction = Cn.BeginTransaction
			CmdB2B.Transaction = CnB2B.BeginTransaction

			arrEdit.Clear()
			SQLB2B = "select id from approval_penjualan_proyek where "
			SQLB2B = SQLB2B & " Flag_WA is null "
			Using Ds = BindingTransB2B(SQLB2B)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrEdit.Add(.Rows(i).Item("id"))
					Next
				End With
			End Using

			Dim a As Integer = 0
			For b As Integer = 1 To arrEdit.Count
				SQLB2B = "select kode_url from approval_penjualan_proyek rpp "
				SQLB2B = SQLB2B & "where id ='" & arrEdit.Item(a).ToString & "' and Flag_WA is null "
				Using DsSQL = BindingTransB2B(SQLB2B)
					With DsSQL.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1

							Dim Request As HttpWebRequest
							Dim Response As HttpWebResponse
							Dim responseReader As StreamReader
							Dim result As String
							Dim custom_uid As String = ""
							'Dim param_wa As String = "192.168.13.23:8000/api/sentWaPembelianProyek/12341234"

							Dim param_wa = "https://pro.evomanufacturingindonesia.id/api/sentWa/" & .Rows(i).Item("kode_url")

							Request = HttpWebRequest.Create(param_wa)
							Request.Method = "GET"
							Request.ContentType = "application/json"
							Request.ContentLength = 0
							Response = Request.GetResponse
							responseReader = New StreamReader(Response.GetResponseStream())
							result = responseReader.ReadToEnd()
							'MessageBox.Show(result)
							Dim xSplit2() As String
							Dim xSplit3() As String
							Dim xMessage() As String
							Dim xcode() As String

							xSplit = Split(result, "data" & """:{", , CompareMethod.Text)
							xSplit2 = xSplit(1).Split("}")
							xSplit3 = xSplit2(0).Split(",")

							'MessageBox.Show(xSplit3(0))
							'MessageBox.Show(xSplit3(1))
							'MessageBox.Show(xSplit3(2))

							xMessage = xSplit3(1).Split(":")
							xcode = xSplit3(2).Split(":")

							'MessageBox.Show(xMessage(1).Trim)
							'MessageBox.Show(xcode(1).Trim)

							If xcode(1).Trim <> "200" Then
								CloseTrans()
								CloseTransB2B()
								CloseConn()
								CloseConnB2B()
								MessageBox.Show(xMessage(1).Trim)
								MessageBox.Show(xcode(1).Trim)
								Exit Sub
							End If

						Next
					End With
				End Using

				SQLB2B = "update approval_penjualan_proyek set Flag_WA='Y' "
				SQLB2B = SQLB2B & "where id='" & arrEdit.Item(a).ToString & "'"
				ExecuteTransB2B(SQLB2B)

				a = a + 1
			Next

			Cmd.Transaction.Commit()
			CmdB2B.Transaction.Commit()
			CloseConn()
			CloseConnB2B()
		Catch ex As Exception
			CloseTrans()
			CloseTransB2B()
			CloseConn()
			CloseConnB2B()
			MessageBox.Show(ex.Message & "edit approval penjualan sementara")
			Exit Sub
		End Try
	End Sub

	Private Sub Button28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Try
			OpenConn()
			OpenConnB2B()
			Cmd.Transaction = Cn.BeginTransaction
			CmdB2B.Transaction = CnB2B.BeginTransaction

			arrEdit.Clear()
			SQLB2B = "select id from approval_po_pembelian_proyek where "
			SQLB2B = SQLB2B & " Flag_WA is null "
			Using Ds = BindingTransB2B(SQLB2B)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrEdit.Add(.Rows(i).Item("id"))
					Next
				End With
			End Using

			Dim a As Integer = 0
			For b As Integer = 1 To arrEdit.Count
				SQLB2B = "select kode_url from approval_po_pembelian_proyek rpp "
				SQLB2B = SQLB2B & "where id ='" & arrEdit.Item(a).ToString & "' and Flag_WA is null "
				Using DsSQL = BindingTransB2B(SQLB2B)
					With DsSQL.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1

							Dim Request As HttpWebRequest
							Dim Response As HttpWebResponse
							Dim responseReader As StreamReader
							Dim result As String
							Dim custom_uid As String = ""
							'Dim param_wa As String = "192.168.13.23:8000/api/sentWaPembelianProyek/12341234"

							Dim param_wa As String = "http://192.168.13.23:8000/api/sentWaPembelianProyek/" & .Rows(i).Item("kode_url")
							Request = HttpWebRequest.Create(param_wa)
							Request.Method = "GET"
							Request.ContentType = "application/json"
							Request.ContentLength = 0
							Response = Request.GetResponse
							responseReader = New StreamReader(Response.GetResponseStream())
							result = responseReader.ReadToEnd()
							'MessageBox.Show(result)
							Dim xSplit2() As String
							Dim xSplit3() As String
							Dim xMessage() As String
							Dim xcode() As String

							xSplit = Split(result, "data" & """:{", , CompareMethod.Text)
							xSplit2 = xSplit(1).Split("}")
							xSplit3 = xSplit2(0).Split(",")

							'MessageBox.Show(xSplit3(0))
							'MessageBox.Show(xSplit3(1))
							'MessageBox.Show(xSplit3(2))

							xMessage = xSplit3(1).Split(":")
							xcode = xSplit3(2).Split(":")

							'MessageBox.Show(xMessage(1).Trim)
							'MessageBox.Show(xcode(1).Trim)

							If xcode(1).Trim <> "200" Then
								CloseTrans()
								CloseTransB2B()
								CloseConn()
								CloseConnB2B()
								MessageBox.Show(xMessage(1).Trim)
								MessageBox.Show(xcode(1).Trim)
								Exit Sub
							End If

						Next
					End With
				End Using

				SQLB2B = "update approval_po_pembelian_proyek set Flag_WA='Y' "
				SQLB2B = SQLB2B & "where id='" & arrEdit.Item(a).ToString & "'"
				ExecuteTransB2B(SQLB2B)

				a = a + 1
			Next

			Cmd.Transaction.Commit()
			CmdB2B.Transaction.Commit()
			CloseConn()
			CloseConnB2B()
		Catch ex As Exception
			CloseTrans()
			CloseTransB2B()
			CloseConn()
			CloseConnB2B()
			MessageBox.Show(ex.Message & "edit approval PO Pembelian Proyek")
			Exit Sub
		End Try
	End Sub

	Private Sub Button27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Try
			OpenConn()
			OpenConnB2B()
			Cmd.Transaction = Cn.BeginTransaction
			CmdB2B.Transaction = CnB2B.BeginTransaction

			arrNo_Fak.Clear()
			SQL = "select no_val from Val_Pemb_Proyek where "
			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and flag_sdh_pindah is null and status is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrNo_Fak.Add(.Rows(i).Item("no_val"))
					Next
				End With
			End Using

			SQL = "select Kode_Perusahaan,No_Val,Tanggal,Jam,Keterangan,UserValidasi,Grand,Kode_Bank_Tujuan,No_Rek_Tujuan,Nama_Penerima,Alamat_Penerima,"
			SQL = SQL & "Kota_Penerima,Negara_Penerima,Telp_Penerima,No_Pengajuan "
			SQL = SQL & "from Val_Pemb_Proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null and status is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						SQLB2B = "insert into val_pemb_proyek (kode_perusahaan,no_val,tanggal,jam, "
						SQLB2B = SQLB2B & "keterangan,uservalidasi,grand,kode_bank_tujuan,no_rek_tujuan,nama_penerima,alamat_penerima, "
						SQLB2B = SQLB2B & "kota_penerima,negara_penerima,telp_penerima,no_pengajuan )"
						SQLB2B = SQLB2B & "values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_val") & "',"
						SQLB2B = SQLB2B & " '" & Format(.Rows(i).Item("tanggal"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("jam") & "', "
						SQLB2B = SQLB2B & "'" & .Rows(i).Item("Keterangan") & "', '" & .Rows(i).Item("UserValidasi") & "', '" & .Rows(i).Item("grand") & "',"
						SQLB2B = SQLB2B & "'" & .Rows(i).Item("Kode_Bank_Tujuan") & "', '" & .Rows(i).Item("No_Rek_Tujuan") & "',"
						SQLB2B = SQLB2B & "'" & .Rows(i).Item("Nama_Penerima") & "', '" & .Rows(i).Item("Alamat_Penerima") & "',"
						SQLB2B = SQLB2B & "'" & .Rows(i).Item("Kota_Penerima") & "', '" & .Rows(i).Item("Negara_Penerima") & "',"
						SQLB2B = SQLB2B & "'" & .Rows(i).Item("Telp_Penerima") & "', '" & .Rows(i).Item("No_Pengajuan") & "') "
						ExecuteTransB2B(SQLB2B)

					Next
				End With
			End Using

			Dim j As Integer = 0
			For z As Integer = 1 To arrNo_Fak.Count
				SQL = "select Kode_Perusahaan,No_Val,No_Faktur,Byr,Urut from Detail_Val_Pemb_Proyek "
				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_Val = '" & arrNo_Fak.Item(j).ToString & "'"
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")

						For i As Integer = 0 To .Rows.Count - 1

							SQLB2B = "insert into detail_val_pemb_proyek (kode_perusahaan,no_val,no_faktur,byr,urut) "
							SQLB2B = SQLB2B & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_val") & "',"
							SQLB2B = SQLB2B & "'" & .Rows(i).Item("no_faktur") & "', '" & .Rows(i).Item("byr") & "', '" & .Rows(i).Item("urut") & "') "
							ExecuteTransB2B(SQLB2B)

						Next
					End With
				End Using

				SQL = "update val_pemb_proyek set flag_sdh_pindah = 'Y' where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
				SQL = SQL & "No_val = '" & arrNo_Fak.Item(j).ToString & "' "
				ExecuteTrans(SQL)

				j = j + 1
			Next

			Cmd.Transaction.Commit()
			CmdB2B.Transaction.Commit()
			CloseConn()
			CloseConnB2B()
		Catch ex As Exception
			CloseTrans()
			CloseTransB2B()
			CloseConn()
			CloseConnB2B()
			MessageBox.Show(ex.Message & "insert val pemb proyek")
			Exit Sub
		End Try
	End Sub

	Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
		If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
			MessageBox.Show("Pilih dahulu yang mau copy!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Clipboard.Clear()
		Clipboard.SetText(ListView1.FocusedItem.Text)

		'Try
		'    Clipboard.Clear()
		'    Clipboard.SetText(ListView1.FocusedItem.Text)
		'Catch ex As Exception
		'End Try
	End Sub

	Private Sub get_no_faktur_penawaran()
		Faktur_Penawaran = fMasterPenawaran & Format(tgl_skg, "MMyy") & "-" &
							 General_Class.Get_Last_Number2("emi_master_penawaran", "no_Faktur", 5,
							 "Kode_perusahaan", KodePerusahaan,
							 "And", "substring(no_Faktur, 1, " & Len(fMasterPenawaran) + 4 & ")", fMasterPenawaran & Format(tgl_skg, "MMyy"))
	End Sub

	Private Sub Handle_Proses_Waste_Process()

		get_jam()

		Try
			OpenConn()

			'========================================
			'=     GET APPROVAL LEVEL TERTINGGI     =
			'========================================
			Dim ApprovalLevelTertinggi As Integer = 0
			SQL = "select isnull(max(Approval_Level), 0) as LevelTertinggi "
			SQL = SQL & "from N_EMI_Transaksi_Approval_Waste "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Approve is null and Jenis_Approval = 'Waste_Process' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					ApprovalLevelTertinggi = Dr("LevelTertinggi")
				End If
			End Using

			For i As Integer = 1 To ApprovalLevelTertinggi

				SQL = "select a.No_Transaksi, a.No_Faktur_Waste, a.ID_User_Android_Approve, a.No_HP, a.Flag_Approve, a.Flag_Sudah_Kirim_WA, "
				SQL = SQL & " a.tanggal, a.Jam, "
				SQL = SQL & "case when a.Jenis_Approval = 'Waste_Process' then 'Waste Process' "
				SQL = SQL & "when a.Jenis_Approval = 'Waste_Produk' then 'Waste Produk' end as Jenis_Pemusnahan, "
				SQL = SQL & "a.No_Berita_Acara "
				SQL = SQL & "from N_EMI_Transaksi_Approval_Waste a "
				SQL = SQL & ""
				SQL = SQL & "where a.Status is null "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and a.Approval_Level = " & i & " "
				SQL = SQL & "and a.Flag_Approve is null "
				SQL = SQL & "and a.Jenis_Approval = 'Waste_Process' "
				SQL = SQL & "order by a.approval_Level, a.No_Transaksi "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For j As Integer = 0 To .Rows.Count - 1

								If i > 1 Then

									'=====================================================
									'=     CEK APAKAH LEVEL SEBELUMNYA SUDAH APPROVE     =
									'=====================================================

									SQL = "select top 1 a.No_Transaksi, a.No_Faktur_Waste, a.ID_User_Android_Approve, a.No_HP, a.Flag_Approve, a.Flag_Sudah_Kirim_WA "
									SQL = SQL & "from N_EMI_Transaksi_Approval_Waste a "
									SQL = SQL & ""
									SQL = SQL & "where a.Status is null "
									SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
									SQL = SQL & "and a.Approval_Level = " & i - 1 & " "
									SQL = SQL & "and a.No_Transaksi = '" & .Rows(j).Item("No_Transaksi") & "' "
									SQL = SQL & "and a.No_Faktur_Waste = '" & .Rows(j).Item("No_Faktur_Waste") & "' "
									SQL = SQL & "and a.Flag_Sudah_Kirim_WA = 'Y' "
									SQL = SQL & "and a.Flag_Approve = 'Y' "
									SQL = SQL & "and a.Jenis_Approval = 'Waste_Process' "
									Using Dr = OpenTrans(SQL)
										If Not Dr.Read Then
											Continue For
										End If
									End Using

								End If

								If General_Class.CekNULL(.Rows(j).Item("Flag_Sudah_Kirim_WA")) = "" Then

									Cmd.Transaction = Cn.BeginTransaction

									Dim NoHp As String = .Rows(j).Item("No_HP")
									'Dim NoHp As String = "6285117547880"
									Dim Jenis_Pemusnahan As String = If(General_Class.CekNULL(.Rows(j).Item("Jenis_Pemusnahan")) = "", "-", .Rows(j).Item("Jenis_Pemusnahan"))
									Dim No_Ba As String = If(General_Class.CekNULL(.Rows(j).Item("No_Berita_Acara")) = "", "-", .Rows(j).Item("No_Berita_Acara"))
									Dim No_Faktur_Waste As String = If(General_Class.CekNULL(.Rows(j).Item("No_Faktur_Waste")) = "", "-", .Rows(j).Item("No_Faktur_Waste"))

									Dim Total As Double = 0
									Dim Satuan As String = ""
									SQL = "select b.Total, b.Satuan "
									SQL &= $"from N_EMI_Transaksi_Transfer_Waste a "
									SQL &= $"inner join N_EMI_Transaksi_Transfer_Waste_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
									SQL &= $"where a.status is NULL "
									SQL &= $"and a.Kode_Perusahaan = '{KodePerusahaan}' "
									SQL &= $"and a.No_Faktur = '{ .Rows(j).Item("No_Faktur_Waste")}' "
									Using Dr = OpenTrans(SQL)
										If Dr.Read Then
											Total = Format(Dr("Total"), "N4")
											Satuan = Dr("Satuan")
										End If
									End Using

									Dim payload As String = "{
										""messaging_product"": ""whatsapp"",
										""to"": """ & NoHp & """,
										""type"": ""template"",
										""template"": {
											""name"": ""emi_approval_pemushahan"",
											""language"": {
												""code"": ""id""
											},
											""components"": [
												{
													""type"": ""body"",
													""parameters"": [
														{""type"": ""text"", ""text"": """ & No_Ba & """},
														{""type"": ""text"", ""text"": """ & No_Faktur_Waste & """},
														{""type"": ""text"", ""text"": """ & Format(.Rows(j).Item("tanggal"), "dd MMM yyyy") & ", " & .Rows(j).Item("Jam") & """},
														{""type"": ""text"", ""text"": """ & Jenis_Pemusnahan & """},
														{""type"": ""text"", ""text"": """ & Format(Total, "N4") & " " & Satuan & """}
													]
												}

											]
										}
									}"

									Dim hasil As String = Helper_API.CallAPI(Url_WA_Business, "POST", Token_WA_Business, payload)

									If hasil.StartsWith("API Error") OrElse hasil.StartsWith("Error") Then
										' Ambil hanya isi pesan setelah titik dua
										Dim parts() As String = hasil.Split(New Char() {":"c}, 2, StringSplitOptions.None)
										Dim msg As String = If(parts.Length > 1, parts(1).Trim(), hasil)

										CloseTrans()
										CloseConn()
										MessageBox.Show("Terjadi error waktu call API: " & msg)
										Exit Sub
									End If

									SQL = "update N_EMI_Transaksi_Approval_Waste set Flag_Sudah_Kirim_WA = 'Y' "
									SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
									SQL &= $"and Flag_Sudah_Kirim_WA is NULL "
									SQL &= $"and Flag_Approve is null "
									SQL &= $"and No_Faktur_Waste = '{ .Rows(j).Item("No_Faktur_Waste")}' "
									SQL &= $"and No_Transaksi = '{ .Rows(j).Item("No_Transaksi")}' "
									SQL &= $"and Approval_Level = '{i}' "
									ExecuteTrans(SQL)

									Cmd.Transaction.Commit()
									Continue For

								End If

							Next
						End If
					End With
				End Using

			Next

			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Dim Lvw As ListViewItem
			Lvw = ListView1.Items.Add("Automatization - Waste Process: " & ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Handle_Proses_Waste_Process_Stock()

		get_jam()

		Try
			OpenConn()

			SQL = "select distinct a.No_Transaksi, a.No_Faktur_Waste, a.Jenis_Approval, "
			SQL = SQL & "case when exists ( select 1 from N_EMI_Transaksi_Approval_Waste z "
			SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan "
			SQL = SQL & "and z.No_Transaksi = a.No_Transaksi "
			SQL = SQL & "and z.No_Faktur_Waste = a.No_Faktur_Waste "
			SQL = SQL & "and z.Flag_Approve IS NULL "
			SQL = SQL & ") then 'T' else 'Y' end as isApprovedAll "
			SQL = SQL & "from N_EMI_Transaksi_Approval_Waste a "
			SQL = SQL & "where a.Status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Jenis_Approval = 'Waste_Process' "
			SQL = SQL & "and a.Flag_Selesai is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							If .Rows(i).Item("isApprovedAll") = "Y" Then

								Cmd.Transaction = Cn.BeginTransaction

								Dim No_Faktur_Waste As String = .Rows(i).Item("No_Faktur_Waste")
								Dim No_Transaksi_Approval As String = .Rows(i).Item("No_Transaksi")

								'Proses Waste

#Region "PROSES WASTE DI SINI"

								Dim QrLama As String = ""
								Dim expDate As String = ""
								Dim batchLama As String = ""
								Dim tglMsk As String = ""
								Dim SN As String = ""
								Dim UrutOto As String = ""
								Dim IdWarehouseTujuan As String = ""
								Dim PalletTujuan As String = ""
								Dim KdBarang As String = ""
								Dim KdSo As String = ""
								Dim SatuanBesar As String = ""
								Dim SatuanKecil As String = ""
								Dim JumlahEstimasi As String = ""
								Dim JumlahBagsEstimasi As String = ""

								SQL = "select a.No_Faktur, a.Kode_Stock_Owner, b.Kode_Barang, c.Serial_Number_Awal, c.Jumlah, c.Jumlah_Bags, b.Satuan, b.Satuan_Barang, "
								SQL = SQL & "d.Qr_Code, d.Batch_Number, d.Tgl_Expired, d.Tgl_Masuk, c.Urut_Oto, c.Id_Wms_Tujuan "
								SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a  "
								SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
								SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
								SQL = SQL & "inner join Barang_SN d on c.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang and c.Serial_Number_Awal = d.Serial_Number "
								SQL = SQL & "where a.Status is null "
								SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
								SQL = SQL & "and a.No_Faktur = '" & .Rows(i).Item("No_Faktur_Waste") & "' "
								SQL = SQL & "and c.Selesai is null "
								Using Ds2 = BindingTrans(SQL)
									If Ds2.Tables("MyTable").Rows.Count <> 0 Then
										For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

											QrLama = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Qr_Code"))
											batchLama = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Batch_Number"))
											SN = Ds2.Tables("MyTable").Rows(j).Item("Serial_Number_Awal")
											expDate = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Tgl_Expired"))
											tglMsk = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Tgl_Masuk"))
											UrutOto = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Urut_Oto"))
											IdWarehouseTujuan = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Id_Wms_Tujuan"))
											KdBarang = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Kode_Barang"))
											KdSo = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Kode_Stock_Owner"))
											SatuanBesar = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Satuan"))
											SatuanKecil = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Satuan_Barang"))
											JumlahEstimasi = HilangkanTanda(Ds2.Tables("MyTable").Rows(j).Item("Jumlah"))
											JumlahBagsEstimasi = HilangkanTanda(Ds2.Tables("MyTable").Rows(j).Item("Jumlah_Bags"))

											SQL = "select a.Status, c.Selesai, b.Flag_Timbang "
											SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Detail b, N_EMI_Transaksi_Transfer_Waste_Det c "
											SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.no_Faktur = b.No_Faktur and "
											SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.no_Faktur = c.No_Faktur and b.urut_oto=c.urut_TF "
											SQL = SQL & "and a.No_Faktur = '" & No_Faktur_Waste & "' and c.urut_oto = '" & UrutOto & "'  "
											Using Dr = OpenTrans(SQL)
												If Dr.Read Then

													If General_Class.CekNULL(Dr("status")) <> "" Then
														Dr.Close()
														CloseTrans()
														CloseConn()
														'MessageBox.Show("Proses tidak bisa dilanjutkan, barang sudah dibatalkan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Process: barang sudah dibatalkan!!")
														'MessageBox.Show(ex.Message)
														Exit Sub
													ElseIf General_Class.CekNULL(Dr("selesai")) = "Y" Then
														Dr.Close()
														CloseTrans()
														CloseConn()
														'MessageBox.Show("Terjadi kesalahan, barang sudah selesai diproses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Process: barang sudah selesai diproses!")
														Exit Sub
													ElseIf General_Class.CekNULL(Dr("Flag_Timbang")) = "Y" Then
														Dr.Close()
														CloseTrans()
														CloseConn()
														'MessageBox.Show("Terjadi kesalahan, ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Process: Terjadi kesalahan")
														Exit Sub
													End If
												Else
													Dr.Close()
													CloseTrans()
													CloseConn()
													'MessageBox.Show("Data barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Exit Sub
												End If
											End Using

											PalletTujuan = "0"

											Dim nilai_kecildetail As Double = 0
											SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & KdBarang & "', '" & SatuanBesar & "',"
											SQL = SQL & "'" & SatuanKecil & "', '" & JumlahEstimasi & "' ) as hasil"
											Using Dr1 = OpenTrans(SQL)
												If Dr1.Read Then
													If General_Class.CekNULL(Dr1("hasil")) = "" Then
														Dr1.Close()
														CloseTrans()
														CloseConn()
														'MessageBox.Show("data konversi satuan kirim tidak ada ")
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Process: data konversi satuan kirim tidak ada")
														Exit Sub
													End If

													nilai_kecildetail = Dr1("hasil")
												Else
													Dr1.Close()
													CloseTrans()
													CloseConn()
													'MessageBox.Show("data konversi satuan kirim tidak ada ")
													Dim Lvw As ListViewItem
													Lvw = ListView1.Items.Add("Automatization - Waste Process: data konversi satuan kirim tidak ada")
													Exit Sub
												End If
											End Using

											'============================
											'=       POTONG STOCK       =
											'============================
											Dim nilai_persediaan_min As Double = 0
											SQL = "select round(dbo.get_hpp(serial_number) * " & nilai_kecildetail & ", 2) as rp_persediaan_min from barang_sn where "
											SQL = SQL & "Kode_Stock_Owner='" & KdSo & "' and Kode_Barang='" & KdBarang & "' "
											SQL = SQL & "and Serial_Number='" & SN & "'"
											Using dr = OpenTrans(SQL)
												If dr.Read Then
													nilai_persediaan_min = dr("rp_persediaan_min")
												Else
													dr.Close()
													CloseTrans()
													CloseConn()
													'MessageBox.Show("Data SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Dim Lvw As ListViewItem
													Lvw = ListView1.Items.Add("Automatization - Waste Process: Data SN tidak ditemukan!")
													Exit Sub
												End If
											End Using

											Dim Nama As String = ""
											'Dim jumlahAkhir As Double = Val(dgv_GoodStock) - Val(dgv_Jumlah)
											SQL = "select Nama, Kode_Barang, round(good_stock,4) as good_stock, Jumlah_Bags from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & KdSo & "' "
											SQL = SQL & "and Kode_Barang='" & KdBarang & "' "
											Using dr = OpenTrans(SQL)
												If dr.Read Then
													Nama = dr("Kode_Barang")
													If dr("good_stock") < nilai_kecildetail Then
														dr.Close()
														CloseTrans()
														CloseConn()
														'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Process: Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.")
														Exit Sub
													ElseIf dr("Jumlah_Bags") < JumlahBagsEstimasi Then
														dr.Close()
														CloseTrans()
														CloseConn()
														'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Process: Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.")
														Exit Sub
													Else
														dr.Close()
														SQL = "update barang set Good_Stock = Good_Stock - Round(" & nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags - " & JumlahBagsEstimasi & " "
														SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & KdSo & "' "
														SQL = SQL & " and Kode_Barang='" & KdBarang & "'"
														ExecuteTrans(SQL)
													End If
												Else
													dr.Close()
													CloseTrans()
													CloseConn()
													'MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Dim Lvw As ListViewItem
													Lvw = ListView1.Items.Add("Automatization - Waste Process: Barang " & Nama & " tidak ditemukan!")
													Exit Sub
												End If
											End Using

											SQL = "select round(jumlah,4) as jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & KdSo & "' "
											SQL = SQL & "and Kode_Barang='" & KdBarang & "' "
											SQL = SQL & "and Serial_Number='" & SN & "'"
											Using dr = OpenTrans(SQL)
												If dr.Read Then
													If dr("jumlah") < nilai_kecildetail Then
														dr.Close()
														CloseTrans()
														CloseConn()
														'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Process: Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.")
														Exit Sub
													ElseIf dr("Jumlah_Bags") < JumlahBagsEstimasi Then
														dr.Close()
														CloseTrans()
														CloseConn()
														'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Process: Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.")
														Exit Sub
													Else
														dr.Close()
														SQL = "update barang_sn set jumlah = jumlah - Round(" & nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags - " & JumlahBagsEstimasi & " "
														SQL = SQL & "where Kode_Stock_Owner='" & KdSo & "' and Kode_Barang='" & KdBarang & "' "
														SQL = SQL & "and Serial_Number='" & SN & "'"
														ExecuteTrans(SQL)
													End If
												Else
													dr.Close()
													CloseTrans()
													CloseConn()
													'MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Dim Lvw As ListViewItem
													Lvw = ListView1.Items.Add("Automatization - Waste Process: Barang " & Nama & " tidak ditemukan!")
													Exit Sub
												End If
											End Using

											'====================================
											'=       CEK KESESUAIAN STOCK       =
											'====================================
											SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
											SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
											SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
											SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
											SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
											SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
											SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & KdSo & "' "
											SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
											SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
											Using D3 = BindingTrans(SQL)
												With D3.Tables("MyTable")
													If D3.Tables("MyTable").Rows.Count <> 0 Then
														If D3.Tables("MyTable").Rows(0).Item("good_stock") <> D3.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or D3.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> D3.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
															CloseTrans()
															CloseConn()
															'MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
															Dim Lvw As ListViewItem
															Lvw = ListView1.Items.Add("Automatization - Waste Process: Stock Tidak Sesuai Saat Potong Stock")
															Exit Sub
														End If
													Else
														CloseTrans()
														CloseConn()
														'MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Process: Data tidak ditemukan . . ! !")
														Exit Sub
													End If
												End With
											End Using

											'==============================
											'=       INSERT SN BARU       =
											'==============================

											Dim hargaIsn As String = ""
											Dim namaBarang As String = ""
											Dim warnaLama As String = ""

											'Ambil Data Lama
											SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.warna "
											SQL = SQL & "from barang_sn a, barang b "
											SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
											SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
											SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
											SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
											SQL = SQL & "and a.Kode_Stock_Owner='" & KdSo & "' "
											SQL = SQL & "and a.Kode_Barang ='" & KdBarang & "' "
											SQL = SQL & "and a.Serial_Number='" & SN & "' "
											'SQL = SQL & "and a.Jumlah <> 0 "
											Using Dr = OpenTrans(SQL)
												Do While Dr.Read
													hargaIsn = Get_Harga_SN(Dr("Serial_Number"))
													QrLama = General_Class.CekNULL(Dr("Qr_Code"))
													batchLama = General_Class.CekNULL(Dr("Batch_Number"))
													namaBarang = General_Class.CekNULL(Dr("Nama"))
													expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
													warnaLama = General_Class.CekNULL(Dr("warna"))
												Loop
											End Using

											'GENERATE SN BARU
											Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
											Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
											Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

											Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)

											'INSERT BARANG SN BARU
											SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah,  Jumlah_Bags, "
											SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk, Blok_SN) "
											SQL = SQL & "select Kode_Perusahaan, '" & KdSo & "', Kode_Barang, '" & SN_Baru & "', '" & nilai_kecildetail & "', " & JumlahBagsEstimasi & ", "
											SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, '" & IdWarehouseTujuan & "', id_Susunan , Qr_Code, '" & newKodeUnikBerjalan & "', "
											SQL = SQL & "Kode_Unik_Asal, '" & PalletTujuan & "', batch_number, '" & warnaLama & "', Tgl_Masuk, 'Y' "
											SQL = SQL & "from Barang_SN "
											SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
											SQL = SQL & "and Kode_Stock_Owner='" & KdSo & "' "
											SQL = SQL & "and Kode_Barang='" & KdBarang & "' "
											SQL = SQL & "and Serial_Number='" & SN & "' "
											ExecuteTrans(SQL)

											'============================
											'=       TAMBAH STOCK       =
											'============================

											SQL = "update barang set Good_Stock= Good_Stock + Round(" & nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags + " & JumlahBagsEstimasi & " "
											SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & KdSo & "' "
											SQL = SQL & " and Kode_Barang='" & KdBarang & "'"
											ExecuteTrans(SQL)

											'CEK KESESUAIAN STOCK
											SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
											SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
											SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
											SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
											SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
											SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
											SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & KdSo & "' "
											SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
											SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
											Using Ds3 = BindingTrans(SQL)
												With Ds3.Tables("MyTable")
													If Ds3.Tables("MyTable").Rows.Count <> 0 Then
														If Ds3.Tables("MyTable").Rows(0).Item("good_stock") <> Ds3.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds3.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds3.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
															CloseTrans()
															CloseConn()
															'MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
															Dim Lvw As ListViewItem
															Lvw = ListView1.Items.Add("Automatization - Waste Process: Stock Tidak Sesuai")
															Exit Sub
														End If
													Else
														CloseTrans()
														CloseConn()
														'MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Process: Data tidak ditemukan . . ! !")
														Exit Sub
													End If
												End With
											End Using

#Region "Jurnal"

											'dari
											Dim inisial_faktur_dari As String = ""
											Dim akun_persediaan_dari As String = ""
											Dim akun_persediaan_tujuan As String = ""

											SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
											SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & KdSo & "' "
											Using Dr = OpenTrans(SQL)
												If Dr.Read Then
													'akun_persediaan_dari = Dr("persediaan")
													inisial_faktur_dari = Dr("inisial_faktur")
												Else
													Dr.Close()
													CloseTrans()
													CloseConn()
													'MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Dim Lvw As ListViewItem
													Lvw = ListView1.Items.Add("Automatization - Waste Process: Data akun tidak ditemukan!")
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
													'MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Dim Lvw As ListViewItem
													Lvw = ListView1.Items.Add("Automatization - Waste Process: Data akun tidak ditemukan!")
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
													'MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Dim Lvw As ListViewItem
													Lvw = ListView1.Items.Add("Automatization - Waste Process: Data akun tidak ditemukan!")
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
											SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock " & No_Faktur_Waste & "', '', "
											SQL = SQL & "'-', '" & UserID & "')"
											ExecuteTrans(SQL)

											SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
							  Strings.Mid(akun_persediaan_dari, 2, 1),
							  Strings.Mid(Ganti(akun_persediaan_dari), 3),
							  KodePerusahaan, KodeProyek, "Persedian " & No_Faktur_Waste, "0", nilai_persediaan_min, pagenumber, KdSo, Bahasa_Pilihan, Ket_Cost_Center_HO)
											ExecuteTrans(SQL)
											pagenumber = pagenumber + 1

											SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
							 Strings.Mid(akun_persediaan_tujuan, 2, 1),
							 Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
							 KodePerusahaan, KodeProyek, "Persedian " & No_Faktur_Waste, nilai_persediaan_min, "0", pagenumber, KdSo, Bahasa_Pilihan, Ket_Cost_Center_HO)
											ExecuteTrans(SQL)
											pagenumber = pagenumber + 1

											SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
											SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
											SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
											Using Dr = OpenTrans(SQL)
												If Dr.Read Then
													If Dr("debit") <> Dr("kredit") Then
														Dr.Close()
														CloseTrans()
														CloseConn()
														'MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Process: Jurnal salah!")
														Exit Sub
													End If
												Else
													Dr.Close()
													CloseTrans()
													CloseConn()
													'MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Dim Lvw As ListViewItem
													Lvw = ListView1.Items.Add("Automatization - Waste Process: Data jurnal tidak ditemukan!")
													Exit Sub
												End If
											End Using

#End Region

											SQL = "insert into N_EMI_Transaksi_Transfer_Waste_Det2(kode_perusahaan, No_faktur, Urut_Det, No_Pallet, "
											SQL = SQL & "Serial_Number, Jumlah, UserID, Tanggal, Jam, Kode_Voucher, Jumlah_Bags) values( "
											SQL = SQL & "'" & KodePerusahaan & "', '" & No_Faktur_Waste & "', '" & UrutOto & "', "
											SQL = SQL & "'" & PalletTujuan & "', '" & SN_Baru & "', '" & nilai_kecildetail & "', "
											SQL = SQL & "'" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
											SQL = SQL & "'" & Kode_voucher & "', '" & JumlahBagsEstimasi & "') "
											ExecuteTrans(SQL)

											SQL = "update N_EMI_Transaksi_Transfer_Waste_Det set  "
											SQL = SQL & "Selesai = 'Y' "
											SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
											SQL = SQL & "and urut_oto = '" & UrutOto & "' "
											ExecuteTrans(SQL)

										Next
									End If
								End Using

#End Region

								SQL = "update N_EMI_Transaksi_Approval_Waste set Flag_Selesai = 'Y' "
								SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Status is null and No_Transaksi = '" & No_Transaksi_Approval & "' and No_Faktur_Waste = '" & No_Faktur_Waste & "' "
								ExecuteTrans(SQL)

								Cmd.Transaction.Commit()

							End If

						Next
					End If
				End With
			End Using

			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Dim Lvw As ListViewItem
			Lvw = ListView1.Items.Add("Automatization - Waste Process: " & ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Handle_Proses_Waste_Product()

		get_jam()

		Try
			OpenConn()

			'========================================
			'=     GET APPROVAL LEVEL TERTINGGI     =
			'========================================
			Dim ApprovalLevelTertinggi As Integer = 0
			SQL = "select isnull(max(Approval_Level), 0) as LevelTertinggi "
			SQL = SQL & "from N_EMI_Transaksi_Approval_Waste "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Approve is null and Jenis_Approval = 'Waste_Produk' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					ApprovalLevelTertinggi = Dr("LevelTertinggi")
				End If
			End Using

			For i As Integer = 1 To ApprovalLevelTertinggi

				SQL = "select a.No_Transaksi, a.No_Faktur_Waste, a.ID_User_Android_Approve, a.No_HP, a.Flag_Approve, a.Flag_Sudah_Kirim_WA, "
				SQL = SQL & "a.tanggal, a.Jam, "
				SQL = SQL & "case when a.Jenis_Approval = 'Waste_Process' then 'Waste Process' "
				SQL = SQL & "when a.Jenis_Approval = 'Waste_Produk' then 'Waste Produk' end as Jenis_Pemusnahan, "
				SQL = SQL & "a.No_Berita_Acara "
				SQL = SQL & "from N_EMI_Transaksi_Approval_Waste a "
				SQL = SQL & ""
				SQL = SQL & "where a.Status is null "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and a.Approval_Level = " & i & " "
				SQL = SQL & "and a.Flag_Approve is null "
				SQL = SQL & "and a.Jenis_Approval = 'Waste_Produk' "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For j As Integer = 0 To .Rows.Count - 1

								If i > 1 Then

									'=====================================================
									'=     CEK APAKAH LEVEL SEBELUMNYA SUDAH APPROVE     =
									'=====================================================
									SQL = "select top 1 a.No_Transaksi, a.No_Faktur_Waste, a.ID_User_Android_Approve, a.No_HP, a.Flag_Approve, a.Flag_Sudah_Kirim_WA "
									SQL = SQL & "from N_EMI_Transaksi_Approval_Waste a "
									SQL = SQL & " "
									SQL = SQL & "where a.Status is null "
									SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
									SQL = SQL & "and a.Approval_Level = " & i - 1 & " "
									SQL = SQL & "and a.No_Transaksi = '" & .Rows(j).Item("No_Transaksi") & "' "
									SQL = SQL & "and a.No_Faktur_Waste = '" & .Rows(j).Item("No_Faktur_Waste") & "' "
									SQL = SQL & "and a.Flag_Sudah_Kirim_WA = 'Y' "
									SQL = SQL & "and a.Flag_Approve = 'Y' "
									SQL = SQL & "and a.Jenis_Approval = 'Waste_Produk' "
									Using Dr = OpenTrans(SQL)
										If Not Dr.Read Then
											Continue For
										End If
									End Using

								End If

								If General_Class.CekNULL(.Rows(j).Item("Flag_Sudah_Kirim_WA")) = "" Then

									Cmd.Transaction = Cn.BeginTransaction

									Dim NoHp As String = .Rows(j).Item("No_HP")
									'Dim NoHp As String = "6285117547880"
									'Dim Jenis_Pemusnahan As String = .Rows(j).Item("Jenis_Pemusnahan")
									'Dim No_Ba As String = .Rows(j).Item("No_Berita_Acara")
									'Dim No_Faktur_Waste As String = .Rows(j).Item("No_Faktur_Waste")

									Dim Jenis_Pemusnahan As String = If(General_Class.CekNULL(.Rows(j).Item("Jenis_Pemusnahan")) = "", "-", .Rows(j).Item("Jenis_Pemusnahan"))
									Dim No_Ba As String = If(General_Class.CekNULL(.Rows(j).Item("No_Berita_Acara")) = "", "-", .Rows(j).Item("No_Berita_Acara"))
									Dim No_Faktur_Waste As String = If(General_Class.CekNULL(.Rows(j).Item("No_Faktur_Waste")) = "", "-", .Rows(j).Item("No_Faktur_Waste"))

									Dim Total As Double = 0
									Dim Satuan As String = ""
									SQL = "select b.Total, b.Satuan "
									SQL &= $"from N_EMI_Transaksi_Transfer_Waste_Produk a "
									SQL &= $"inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
									SQL &= $"where a.status is NULL "
									SQL &= $"and a.Kode_Perusahaan = '{KodePerusahaan}' "
									SQL &= $"and a.No_Faktur = '{ .Rows(j).Item("No_Faktur_Waste")}' "
									Using Dr = OpenTrans(SQL)
										If Dr.Read Then
											Total = Format(Dr("Total"), "N4")
											Satuan = Dr("Satuan")
										End If
									End Using

									Dim payload As String = "{
										""messaging_product"": ""whatsapp"",
										""to"": """ & NoHp & """,
										""type"": ""template"",
										""template"": {
											""name"": ""emi_approval_pemushahan"",
											""language"": {
												""code"": ""id""
											},
											""components"": [
												{
													""type"": ""body"",
													""parameters"": [
														{""type"": ""text"", ""text"": """ & No_Ba & """},
														{""type"": ""text"", ""text"": """ & No_Faktur_Waste & """},
														{""type"": ""text"", ""text"": """ & Format(.Rows(j).Item("tanggal"), "dd MMM yyyy") & ", " & .Rows(j).Item("Jam") & """},
														{""type"": ""text"", ""text"": """ & Jenis_Pemusnahan & """},
														{""type"": ""text"", ""text"": """ & Format(Total, "N4") & " " & Satuan & """}
													]
												}

											]
										}
									}"

									Dim hasil As String = Helper_API.CallAPI(Url_WA_Business, "POST", Token_WA_Business, payload)

									If hasil.StartsWith("API Error") OrElse hasil.StartsWith("Error") Then
										' Ambil hanya isi pesan setelah titik dua
										Dim parts() As String = hasil.Split(New Char() {":"c}, 2, StringSplitOptions.None)
										Dim msg As String = If(parts.Length > 1, parts(1).Trim(), hasil)

										CloseTrans()
										CloseConn()
										MessageBox.Show("Terjadi error waktu call API: " & msg)
										Exit Sub
									End If

									SQL = "update N_EMI_Transaksi_Approval_Waste set Flag_Sudah_Kirim_WA = 'Y' "
									SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
									SQL &= $"and Flag_Sudah_Kirim_WA is NULL "
									SQL &= $"and Flag_Approve is null "
									SQL &= $"and No_Faktur_Waste = '{ .Rows(j).Item("No_Faktur_Waste")}' "
									SQL &= $"and No_Transaksi = '{ .Rows(j).Item("No_Transaksi")}' "
									SQL &= $"and Approval_Level = '{i}' "
									ExecuteTrans(SQL)

									Cmd.Transaction.Commit()
									Continue For

								End If

							Next
						End If
					End With
				End Using

			Next

			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			Dim Lvw As ListViewItem
			Lvw = ListView1.Items.Add("Automatization - Waste Product: " & ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Handle_Proses_Waste_ProductStock()

		get_jam()

		Try
			OpenConn()

			SQL = "select distinct a.No_Transaksi, a.No_Faktur_Waste, a.Jenis_Approval, "
			SQL = SQL & "case when exists ( select 1 from N_EMI_Transaksi_Approval_Waste z "
			SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan "
			SQL = SQL & "and z.No_Transaksi = a.No_Transaksi "
			SQL = SQL & "and z.No_Faktur_Waste = a.No_Faktur_Waste "
			SQL = SQL & "and z.Flag_Approve IS NULL "
			SQL = SQL & ") then 'T' else 'Y' end as isApprovedAll "
			SQL = SQL & "from N_EMI_Transaksi_Approval_Waste a "
			SQL = SQL & "where a.Status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Jenis_Approval = 'Waste_Produk' "
			SQL = SQL & "and a.Flag_Selesai is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							If .Rows(i).Item("isApprovedAll") = "Y" Then

								Cmd.Transaction = Cn.BeginTransaction
								Dim No_Faktur_Waste As String = .Rows(i).Item("No_Faktur_Waste")
								Dim No_Transaksi_Approval As String = .Rows(i).Item("No_Transaksi")
								Dim SoTujuan As String = ""
								Dim SoAwal As String = ""

#Region "PROSES WASTE DI SINI"

								Dim QrLama As String = ""
								Dim expDate As String = ""
								Dim batchLama As String = ""
								Dim tglMsk As String = ""
								Dim SN As String = ""
								Dim UrutOto As String = ""
								Dim IdWarehouseTujuan As String = ""
								Dim PalletTujuan As String = ""
								Dim KdBarang As String = ""
								Dim KdSo As String = ""
								Dim KdSoTujuan As String = ""
								Dim SatuanBesar As String = ""
								Dim SatuanKecil As String = ""
								Dim JumlahEstimasi As String = ""
								Dim JumlahBagsEstimasi As String = ""
								Dim No_Faktur_Waste_Product As String = ""

								SQL = "select a.No_Faktur, a.Kode_Stock_Owner, a.Kode_Stock_Owner_Tujuan, b.Kode_Barang,c.Jumlah, c.Jumlah_Bags, b.Satuan, c.Serial_Number_Awal, "
								SQL = SQL & "b.Satuan_Barang, c.Urut_Oto, c.Warna, c.Id_Wms_Tujuan, d.Qr_Code, d.Batch_Number, d.Tgl_Expired, d.Tgl_Masuk "
								SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
								SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
								SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
								SQL = SQL & "inner join Barang_SN d on c.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang and c.Serial_Number_Awal = d.Serial_Number "
								SQL = SQL & "where a.Status is null "
								SQL = SQL & "and a.Flag_Data_Baru_Validasi_Pemindahan is null "
								SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
								SQL = SQL & "and a.No_Faktur = '" & No_Faktur_Waste & "' "
								Using Ds2 = BindingTrans(SQL)
									If Ds2.Tables("MyTable").Rows.Count <> 0 Then
										For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

											No_Faktur_Waste_Product = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("No_Faktur"))
											QrLama = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Qr_Code"))
											batchLama = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Batch_Number"))
											SN = Ds2.Tables("MyTable").Rows(j).Item("Serial_Number_Awal")
											expDate = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Tgl_Expired"))
											tglMsk = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Tgl_Masuk"))
											UrutOto = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Urut_Oto"))
											IdWarehouseTujuan = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Id_Wms_Tujuan"))
											KdBarang = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Kode_Barang"))
											KdSo = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Kode_Stock_Owner"))
											KdSoTujuan = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Kode_Stock_Owner_Tujuan"))
											SatuanBesar = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Satuan"))
											SatuanKecil = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Satuan_Barang"))
											JumlahEstimasi = HilangkanTanda(Ds2.Tables("MyTable").Rows(j).Item("Jumlah"))
											JumlahBagsEstimasi = HilangkanTanda(Ds2.Tables("MyTable").Rows(j).Item("Jumlah_Bags"))

											SoTujuan = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Kode_Stock_Owner_Tujuan"))
											SoAwal = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Kode_Stock_Owner"))

											SQL = "select a.Status, c.Selesai, b.Flag_Timbang "
											SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a, N_EMI_Transaksi_Transfer_Waste_Produk_Detail b, N_EMI_Transaksi_Transfer_Waste_Produk_Det c "
											SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.no_Faktur = b.No_Faktur and "
											SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.no_Faktur = c.No_Faktur and b.urut_oto=c.urut_TF "
											SQL = SQL & "and a.No_Faktur = '" & No_Faktur_Waste & "' and c.urut_oto = '" & UrutOto & "'  "
											Using Dr = OpenTrans(SQL)
												If Dr.Read Then

													If General_Class.CekNULL(Dr("status")) <> "" Then
														Dr.Close()
														CloseTrans()
														CloseConn()
														'MessageBox.Show("Proses tidak bisa dilanjutkan, barang sudah dibatalkan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Product: Proses tidak bisa dilanjutkan, barang sudah dibatalkan!!")
														Exit Sub
													ElseIf General_Class.CekNULL(Dr("selesai")) = "Y" Then
														Dr.Close()
														CloseTrans()
														CloseConn()
														'MessageBox.Show("Terjadi kesalahan, barang sudah selesai diproses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi kesalahan, barang sudah selesai diproses!")
														Exit Sub
													ElseIf General_Class.CekNULL(Dr("Flag_Timbang")) = "Y" Then
														Dr.Close()
														CloseTrans()
														CloseConn()
														'MessageBox.Show("Terjadi kesalahan, ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi kesalahan, barang sudah Ditimbang!")
														Exit Sub
													End If
												Else
													Dr.Close()
													CloseTrans()
													CloseConn()
													MessageBox.Show("Data barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Dim Lvw As ListViewItem
													Lvw = ListView1.Items.Add("Automatization - Waste Product: Data barang tidak ditemukan!")
													Exit Sub
												End If
											End Using

											PalletTujuan = 0

											'====================================
											'=       CONVERT SATUAN KECIL       =
											'====================================
											Dim nilai_kecildetail As Double = 0
											SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & KdBarang & "', '" & SatuanBesar & "',"
											SQL = SQL & "'" & SatuanKecil & "', '" & JumlahEstimasi & "' ) as hasil"
											Using Dr1 = OpenTrans(SQL)
												If Dr1.Read Then
													If General_Class.CekNULL(Dr1("hasil")) = "" Then
														Dr1.Close()
														CloseTrans()
														CloseConn()
														'MessageBox.Show("data konversi satuan kirim tidak ada ")
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Product: data konversi satuan kirim tidak ada ")
														Exit Sub
													End If

													nilai_kecildetail = Dr1("hasil")
												Else
													Dr1.Close()
													CloseTrans()
													CloseConn()
													'MessageBox.Show("data konversi satuan kirim tidak ada ")
													Dim Lvw As ListViewItem
													Lvw = ListView1.Items.Add("Automatization - Waste Product: data konversi satuan kirim tidak ada ")
													Exit Sub
												End If
											End Using

											'============================
											'=       POTONG STOCK       =
											'============================

											Dim nilai_persediaan_min As Double = 0
											SQL = "select round(dbo.get_hpp(serial_number) * " & JumlahEstimasi & ", 2) as rp_persediaan_min from barang_sn where "
											SQL = SQL & "Kode_Stock_Owner='" & KdSo & "' and Kode_Barang='" & KdBarang & "' "
											SQL = SQL & "and Serial_Number='" & SN & "'"
											Using dr = OpenTrans(SQL)
												If dr.Read Then
													nilai_persediaan_min = dr("rp_persediaan_min")
												Else
													dr.Close()
													CloseTrans()
													CloseConn()
													'MessageBox.Show("Data SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Dim Lvw As ListViewItem
													Lvw = ListView1.Items.Add("Automatization - Waste Product: Data SN tidak ditemukan!")
													Exit Sub
												End If
											End Using

											Dim Nama As String = ""
											'Dim jumlahAkhir As Double = Val(dgv_GoodStock) - Val(dgv_Jumlah)
											SQL = "select Nama, Kode_Barang, round(good_stock,4) as good_stock, Jumlah_Bags from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & KdSo & "' "
											SQL = SQL & "and Kode_Barang='" & KdBarang & "' "
											Using dr = OpenTrans(SQL)
												If dr.Read Then
													Nama = dr("Kode_Barang")
													If dr("good_stock") < JumlahEstimasi Then
														dr.Close()
														CloseTrans()
														CloseConn()
														'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Product: Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.")
														Exit Sub
													ElseIf dr("Jumlah_Bags") < JumlahBagsEstimasi Then
														dr.Close()
														CloseTrans()
														CloseConn()
														'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Product: Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.")
														Exit Sub
													Else
														dr.Close()
														SQL = "update barang set Good_Stock = Good_Stock - Round(" & JumlahEstimasi & ",4), Jumlah_Bags = Jumlah_Bags - " & JumlahBagsEstimasi & " "
														SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & KdSo & "' "
														SQL = SQL & " and Kode_Barang='" & KdBarang & "'"
														ExecuteTrans(SQL)
													End If
												Else
													dr.Close()
													CloseTrans()
													CloseConn()
													'MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Dim Lvw As ListViewItem
													Lvw = ListView1.Items.Add("Automatization - Waste Product: Barang " & Nama & " tidak ditemukan!")
													Exit Sub
												End If
											End Using

											SQL = "select round(jumlah,4) as jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & KdSo & "' "
											SQL = SQL & "and Kode_Barang='" & KdBarang & "' "
											SQL = SQL & "and Serial_Number='" & SN & "'"
											Using dr = OpenTrans(SQL)
												If dr.Read Then
													If dr("jumlah") < JumlahEstimasi Then
														dr.Close()
														CloseTrans()
														CloseConn()
														'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Product: Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.")
														Exit Sub
													ElseIf dr("Jumlah_Bags") < JumlahBagsEstimasi Then
														dr.Close()
														CloseTrans()
														CloseConn()
														'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Product: Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.")
														Exit Sub
													Else
														dr.Close()
														SQL = "update barang_sn set jumlah = jumlah - Round(" & JumlahEstimasi & ",4), Jumlah_Bags = Jumlah_Bags - " & JumlahBagsEstimasi & " "
														SQL = SQL & "where Kode_Stock_Owner='" & KdSo & "' and Kode_Barang='" & KdBarang & "' "
														SQL = SQL & "and Serial_Number='" & SN & "'"
														ExecuteTrans(SQL)
													End If
												Else
													dr.Close()
													CloseTrans()
													CloseConn()
													'MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Dim Lvw As ListViewItem
													Lvw = ListView1.Items.Add("Automatization - Waste Product: Barang " & Nama & " tidak ditemukan!")
													Exit Sub
												End If
											End Using

											'====================================
											'=       CEK KESESUAIAN STOCK       =
											'====================================
											SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
											SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
											SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
											SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
											SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
											SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
											SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & KdSo & "' "
											SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
											SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
											Using Ds7 = BindingTrans(SQL)
												If Ds7.Tables("MyTable").Rows.Count <> 0 Then
													If Ds7.Tables("MyTable").Rows(0).Item("good_stock") <> Ds7.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds7.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds7.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
														CloseTrans()
														CloseConn()
														'MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi Kesalahan . . ! !, Stock Tidak Sama Saat Potong Stock")
														Exit Sub
													End If
												Else
													CloseTrans()
													CloseConn()
													'MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Dim Lvw As ListViewItem
													Lvw = ListView1.Items.Add("Automatization - Waste Product: Data tidak ditemukan . . ! !")
													Exit Sub
												End If
											End Using

											'==============================
											'=       INSERT SN BARU       =
											'==============================

											Dim hargaIsn As String = ""
											Dim namaBarang As String = ""
											Dim warnaLama As String = ""

											'Ambil Data Lama
											SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.warna "
											SQL = SQL & "from barang_sn a, barang b "
											SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
											SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
											SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
											SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
											SQL = SQL & "and a.Kode_Stock_Owner='" & KdSo & "' "
											SQL = SQL & "and a.Kode_Barang ='" & KdBarang & "' "
											SQL = SQL & "and a.Serial_Number='" & SN & "' "
											'SQL = SQL & "and a.Jumlah <> 0 "
											Using Dr = OpenTrans(SQL)
												Do While Dr.Read
													hargaIsn = Get_Harga_SN(Dr("Serial_Number"))
													QrLama = General_Class.CekNULL(Dr("Qr_Code"))
													batchLama = General_Class.CekNULL(Dr("Batch_Number"))
													namaBarang = General_Class.CekNULL(Dr("Nama"))
													expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
													warnaLama = General_Class.CekNULL(Dr("warna"))
												Loop
											End Using

											'GENERATE SN BARU
											Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
											Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
											Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

											Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)

											'INSERT BARANG SN BARU
											SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah,  Jumlah_Bags, "
											SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk, Blok_SN) "
											SQL = SQL & "select Kode_Perusahaan, '" & KdSoTujuan & "', Kode_Barang, '" & SN_Baru & "', '" & nilai_kecildetail & "', " & JumlahBagsEstimasi & ", "
											SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, '" & IdWarehouseTujuan & "', id_Susunan , Qr_Code, '" & newKodeUnikBerjalan & "', "
											SQL = SQL & "Kode_Unik_Asal, '" & PalletTujuan & "', batch_number, '" & warnaLama & "', Tgl_Masuk, 'Y' "
											SQL = SQL & "from Barang_SN "
											SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
											SQL = SQL & "and Kode_Stock_Owner='" & KdSo & "' "
											SQL = SQL & "and Kode_Barang='" & KdBarang & "' "
											SQL = SQL & "and Serial_Number='" & SN & "' "
											ExecuteTrans(SQL)

											'============================
											'=       TAMBAH STOCK       =
											'============================

											SQL = "update barang set Good_Stock= Good_Stock + Round(" & nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags + " & JumlahBagsEstimasi & " "
											SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & KdSoTujuan & "' "
											SQL = SQL & " and Kode_Barang='" & KdBarang & "'"
											ExecuteTrans(SQL)

											'CEK KESESUAIAN STOCK
											SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
											SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
											SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
											SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
											SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
											SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
											SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & KdSoTujuan & "' "
											SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
											SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
											Using Ds4 = BindingTrans(SQL)
												If Ds4.Tables("MyTable").Rows.Count <> 0 Then
													If Ds4.Tables("MyTable").Rows(0).Item("good_stock") <> Ds4.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds4.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds4.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
														CloseTrans()
														CloseConn()
														'MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi Kesalahan . . ! !, Stock Tidak Sama Saat Potong Stock")
														Exit Sub
													End If
												Else
													CloseTrans()
													CloseConn()
													'MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Dim Lvw As ListViewItem
													Lvw = ListView1.Items.Add("Automatization - Waste Product: Data tidak ditemukan . . ! , Saat Tambah Stock")
													Exit Sub
												End If
											End Using

#Region "Jurnal"

											'dari
											Dim inisial_faktur_dari As String = ""
											Dim akun_persediaan_dari As String = ""
											Dim akun_persediaan_tujuan As String = ""

											SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
											SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & KdSo & "' "
											Using Dr = OpenTrans(SQL)
												If Dr.Read Then
													'akun_persediaan_dari = Dr("persediaan")
													inisial_faktur_dari = Dr("inisial_faktur")
												Else
													Dr.Close()
													CloseTrans()
													CloseConn()
													'MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Dim Lvw As ListViewItem
													Lvw = ListView1.Items.Add("Automatization - Waste Product: Data akun tidak ditemukan!")
													Exit Sub
												End If
											End Using

											SQL = "select akun_gantung_waste_produk from stock_owner_gudang where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & KdSoTujuan & "' "
											Using Dr = OpenTrans(SQL)
												If Dr.Read Then
													akun_persediaan_dari = Dr("akun_gantung_waste_produk")
												Else
													Dr.Close()
													CloseTrans()
													CloseConn()
													'MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Dim Lvw As ListViewItem
													Lvw = ListView1.Items.Add("Automatization - Waste Product: Data akun tidak ditemukan!")
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
													'MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Dim Lvw As ListViewItem
													Lvw = ListView1.Items.Add("Automatization - Waste Product: Data akun tidak ditemukan!")
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
											SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock " & No_Faktur_Waste & "', '', "
											SQL = SQL & "'-', '" & UserID & "')"
											ExecuteTrans(SQL)

											SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
										  Strings.Mid(akun_persediaan_dari, 2, 1),
										  Strings.Mid(Ganti(akun_persediaan_dari), 3),
										  KodePerusahaan, KodeProyek, "Persedian " & No_Faktur_Waste, "0", nilai_persediaan_min, pagenumber, KdSo, Bahasa_Pilihan, Ket_Cost_Center_HO)
											ExecuteTrans(SQL)
											pagenumber = pagenumber + 1

											SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
										 Strings.Mid(akun_persediaan_tujuan, 2, 1),
										 Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
										 KodePerusahaan, KodeProyek, "Persedian " & No_Faktur_Waste, nilai_persediaan_min, "0", pagenumber, KdSoTujuan, Bahasa_Pilihan, Ket_Cost_Center_HO)
											ExecuteTrans(SQL)
											pagenumber = pagenumber + 1

											SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
											SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
											SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
											Using Dr = OpenTrans(SQL)
												If Dr.Read Then
													If Dr("debit") <> Dr("kredit") Then
														Dr.Close()
														CloseTrans()
														CloseConn()
														'MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
														Dim Lvw As ListViewItem
														Lvw = ListView1.Items.Add("Automatization - Waste Product: Jurnal salah!")
														Exit Sub
													End If
												Else
													Dr.Close()
													CloseTrans()
													CloseConn()
													'MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
													Dim Lvw As ListViewItem
													Lvw = ListView1.Items.Add("Automatization - Waste Product: Data jurnal tidak ditemukan!")
													Exit Sub
												End If
											End Using

#End Region

											SQL = "insert into N_EMI_Transaksi_Transfer_Waste_Produk_Det2(kode_perusahaan, No_faktur, Urut_Det, No_Pallet, "
											SQL = SQL & "Serial_Number, Jumlah, UserID, Tanggal, Jam, Kode_Voucher, Jumlah_Bags) values( "
											SQL = SQL & "'" & KodePerusahaan & "', '" & No_Faktur_Waste & "', '" & UrutOto & "', "
											SQL = SQL & "'" & PalletTujuan & "', '" & SN_Baru & "', '" & nilai_kecildetail & "', "
											SQL = SQL & "'" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
											SQL = SQL & "'" & Kode_voucher & "', '" & JumlahBagsEstimasi & "') "
											ExecuteTrans(SQL)

											SQL = "update N_EMI_Transaksi_Transfer_Waste_Produk_Det set  "
											SQL = SQL & "Selesai = 'Y' "
											SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
											SQL = SQL & "and urut_oto = '" & UrutOto & "' "
											ExecuteTrans(SQL)

										Next

										SQL = "update N_EMI_Transaksi_Approval_Waste set Flag_Selesai = 'Y' "
										SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Status is null and No_Transaksi = '" & No_Transaksi_Approval & "' and No_Faktur_Waste = '" & No_Faktur_Waste & "' "
										ExecuteTrans(SQL)
									End If
								End Using

#End Region

#Region "PROSES PENGAJUAN PEMUSNAHAN BARANG"

								'                                Dim InitialFaktur As String = ""
								'                                SQL = "Select kode_stock_owner, inisial_faktur "
								'                                SQL = SQL & "From Stock_Owner_Gudang "
								'                                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and flag_waste='Y' "
								'                                SQL = SQL & "and Kode_Stock_Owner = '" & SoTujuan & "' "
								'                                Using Dr = OpenTrans(SQL)
								'                                    If Dr.Read Then
								'                                        InitialFaktur = Dr("inisial_faktur")
								'                                    End If
								'                                End Using

								'                                Dim Keterangan_Faktur_Pengajuan As String = $"Automation Waste Product Transfer Submissions - {No_Faktur_Waste}"

								'                                Dim Initial_Faktur As String = ""
								'                                SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From Stock_Owner_Gudang where "
								'                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and kode_Stock_owner = '" & SoAwal & "' "
								'                                SQL = SQL & "order by kode_stock_owner"
								'                                Using dr = OpenTrans(SQL)
								'                                    Do While dr.Read
								'                                        Initial_Faktur = dr("inisial_faktur")
								'                                    Loop
								'                                End Using

								'                                Dim FPro_Results As String = "PMB-"
								'                                Dim No_Faktur_Waste_process = FPro_Results & Initial_Faktur & "-" & Format(tgl_skg, "MM/yy") & "-" &
								'                                      General_Class.Get_Last_Number2("N_EMI_Transaksi_Transfer_Waste", "no_faktur", JumlahDigit,
								'                                      "Kode_perusahaan", KodePerusahaan,
								'                                      "And", "substring(no_faktur,1," & Len(FPro_Results) + Len(Initial_Faktur) + 6 & ")", FPro_Results & Initial_Faktur & "-" & Format(tgl_skg, "MM/yy"))

								'                                '=================================================
								'                                '=     INSERT N_EMI_Transaksi_Transfer_Waste     =
								'                                '=================================================
								'                                SQL = "insert into N_EMI_Transaksi_Transfer_Waste (kode_perusahaan, No_faktur, Kode_Stock_Owner, Tanggal, Jam, UserID, Lokasi, Keterangan, Flag_Waste_Product, No_Faktur_Produk) Values "
								'                                SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(No_Faktur_Waste_process) & "', '" & SoTujuan & "', "
								'                                SQL = SQL & "'" & tgl_skg & "', '" & tgl_skg.ToString("HH:mm:ss") & "', '" & UserID & "', "
								'                                SQL = SQL & "'" & Ket_Lokasi_HO & "', '" & Keterangan_Faktur_Pengajuan & "', 'Y', '" & No_Faktur_Waste & "')"
								'                                ExecuteTrans(SQL)

								'                                SQL = "select a.Kode_Stock_Owner, a.Kode_Stock_Owner_Tujuan, b.Kode_Barang, c.Serial_Number_Awal, d.Serial_Number, d.Jumlah, d.Jumlah_Bags, "
								'                                SQL = SQL & "b.Satuan, b.Satuan_Barang, c.Id_Wms_Awal, c.Id_Wms_Tujuan, c.No_Pallet_Awal, d.No_Pallet, c.Warna "
								'                                SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
								'                                SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
								'                                SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
								'                                SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det2 d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
								'                                SQL = SQL & "where a.Status is null "
								'                                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
								'                                SQL = SQL & "and a.No_Faktur = '" & No_Faktur_Waste & "'"
								'                                Using Ds2 = BindingTrans(SQL)
								'                                    If Ds2.Tables("MyTable").Rows.Count <> 0 Then
								'                                        For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

								'                                            Dim Produk_KdBarang As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Kode_Barang"))
								'                                            Dim Produk_Satuan As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Satuan"))
								'                                            Dim Produk_SatuanKecil As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Satuan_Barang"))
								'                                            Dim Produk_Jumlah As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Jumlah"))
								'                                            Dim Produk_Jumlah_Bags As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Jumlah_Bags"))
								'                                            Dim Produk_SN_Awal As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Serial_Number_Awal"))
								'                                            Dim Produk_SN As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Serial_Number"))
								'                                            Dim Produk_Kd_SO As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Kode_Stock_Owner_Tujuan"))
								'                                            Dim Produk_IDWarehouse As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Id_Wms_Awal"))
								'                                            Dim Produk_IDWarehouse_Tujuan As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Id_Wms_Tujuan"))
								'                                            Dim Produk_Pallet As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("No_Pallet_Awal"))
								'                                            Dim Produk_Pallet_Tujuan As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("No_Pallet"))
								'                                            Dim Produk_Warna As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Warna"))

								'                                            Dim nilai_kecil As Double = 0
								'                                            SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & Produk_KdBarang & "', '" & Produk_Satuan & "',"
								'                                            SQL = SQL & "'" & Produk_SatuanKecil & "', '" & HilangkanTanda(Produk_Jumlah) & "' ) as hasil"
								'                                            Using Dr1 = OpenTrans(SQL)
								'                                                If Dr1.Read Then
								'                                                    If General_Class.CekNULL(Dr1("hasil")) = "" Then
								'                                                        Dr1.Close()
								'                                                        CloseTrans()
								'                                                        CloseConn()
								'                                                        'MessageBox.Show("data konversi satuan kirim tidak ada ")
								'                                                        Dim Lvw As ListViewItem
								'                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: data konversi satuan kirim tidak ada")
								'                                                        Exit Sub
								'                                                    End If

								'                                                    nilai_kecil = Dr1("hasil")
								'                                                Else
								'                                                    Dr1.Close()
								'                                                    CloseTrans()
								'                                                    CloseConn()
								'                                                    'MessageBox.Show("data konversi satuan kirim tidak ada ")
								'                                                    Dim Lvw As ListViewItem
								'                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: data konversi satuan kirim tidak ada")
								'                                                    Exit Sub
								'                                                End If
								'                                            End Using

								'                                            Dim Jenis_Berat As String = ""
								'                                            SQL = "Select isnull(flag_tampil_berat,'T') as flag_tampil_berat from emi_satuan where "
								'                                            SQL = SQL & "satuan='" & Produk_Satuan & "' and kode_perusahaan='" & KodePerusahaan & "' "
								'                                            Using dr = OpenTrans(SQL)
								'                                                If dr.Read Then
								'                                                    Jenis_Berat = dr("flag_tampil_berat")
								'                                                Else
								'                                                    dr.Close()
								'                                                    CloseTrans()
								'                                                    CloseConn()
								'                                                    'MessageBox.Show("data Satuan Tidak ada . . ! ! ")
								'                                                    Dim Lvw As ListViewItem
								'                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: data Satuan Tidak ada . . ! !")
								'                                                    Exit Sub
								'                                                End If
								'                                            End Using

								'                                            Dim Jenis_kemasan As String = ""
								'                                            SQL = "Select Jenis_Kemasan from barang where "
								'                                            SQL = SQL & "Kode_Barang='" & Produk_KdBarang & "' and kode_perusahaan='" & KodePerusahaan & "' "
								'                                            Using dr = OpenTrans(SQL)
								'                                                If dr.Read Then
								'                                                    Jenis_kemasan = dr("Jenis_Kemasan")
								'                                                Else
								'                                                    dr.Close()
								'                                                    CloseTrans()
								'                                                    CloseConn()
								'                                                    'MessageBox.Show("data Satuan Tidak ada . . ! ! ")
								'                                                    Dim Lvw As ListViewItem
								'                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: data Satuan Tidak ada . . ! !")
								'                                                    Exit Sub
								'                                                End If
								'                                            End Using

								'                                            Dim Flag_Timbang As String = "T"

								'                                            '========================================================
								'                                            '=     INSERT N_EMI_Transaksi_Transfer_Waste_Detail     =
								'                                            '========================================================
								'                                            SQL = "select Kode_Perusahaan "
								'                                            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Detail "
								'                                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
								'                                            SQL = SQL & "and No_Faktur = '" & Trim(No_Faktur_Waste_process) & "' "
								'                                            SQL = SQL & "and Kode_Barang = '" & Produk_KdBarang & "' "
								'                                            Using Dr = OpenTrans(SQL)
								'                                                If Dr.Read Then
								'                                                    Dr.Close()
								'                                                    SQL = "update N_EMI_Transaksi_Transfer_Waste_Detail set total += " & HilangkanTanda(Produk_Jumlah) & ", "
								'                                                    SQL = SQL & "Total_Barang += " & HilangkanTanda(nilai_kecil) & ", Total_Bags += " & HilangkanTanda(Produk_Jumlah_Bags) & " "
								'                                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
								'                                                    SQL = SQL & "and No_Faktur = '" & Trim(No_Faktur_Waste_process) & "' "
								'                                                    SQL = SQL & "and Kode_Barang = '" & Produk_KdBarang & "' "
								'                                                    ExecuteTrans(SQL)
								'                                                Else
								'                                                    Dr.Close()
								'                                                    SQL = "insert into N_EMI_Transaksi_Transfer_Waste_Detail (Kode_Perusahaan, No_faktur, Kode_Barang, Total, Satuan, "
								'                                                    SQL = SQL & "Total_Barang, Satuan_Barang, Total_Bags, Flag_Timbang) values "
								'                                                    SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(No_Faktur_Waste_process) & "', '" & Produk_KdBarang & "', "
								'                                                    SQL = SQL & "'" & HilangkanTanda(Produk_Jumlah) & "', '" & Produk_Satuan & "', "
								'                                                    SQL = SQL & "'" & nilai_kecil & "', '" & Produk_SatuanKecil & "', "
								'                                                    SQL = SQL & "'" & HilangkanTanda(Produk_Jumlah_Bags) & "', '" & Flag_Timbang & "')"
								'                                                    ExecuteTrans(SQL)
								'                                                End If
								'                                            End Using

								'                                            Dim x_ident_current As Integer = 0
								'                                            SQL = "select IDENT_CURRENT('N_EMI_Transaksi_Transfer_Waste_Detail') as urutan"
								'                                            Using Dr = OpenTrans(SQL)
								'                                                If Dr.Read Then
								'                                                    x_ident_current = Dr("urutan")
								'                                                End If
								'                                            End Using

								'                                            SQL = "select kode_Perusahaan from N_EMI_Transaksi_Transfer_Waste_Detail where "
								'                                            SQL = SQL & "kode_Perusahaan='" & KodePerusahaan & "' and "
								'                                            SQL = SQL & "No_Faktur='" & No_Faktur_Waste_process & "' and "
								'                                            SQL = SQL & "Kode_barang='" & Produk_KdBarang & "' and "
								'                                            SQL = SQL & "urut_oto='" & x_ident_current & "' "
								'                                            Using Dr = OpenTrans(SQL)
								'                                                If Not Dr.Read Then
								'                                                    Dr.Close()
								'                                                    CloseTrans()
								'                                                    CloseConn()
								'                                                    'MessageBox.Show("Terjadi Kesalahan, Silahkan Ulangi Transaksi  . . ! ! ")
								'                                                    Dim Lvw As ListViewItem
								'                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi Kesalahan, Data Detail Tidak Ditemukan  . . ! !")
								'                                                    Exit Sub
								'                                                End If
								'                                            End Using

								'                                            '============================================
								'                                            '=     CEK APAKAH DATA BELUM DI TIMBANG     =
								'                                            '============================================

								'                                            SQL = "select a.Kode_Perusahaan from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Det b, barang_sn c  where "
								'                                            SQL = SQL & "a.kode_Perusahaan=b.kode_Perusahaan And a.No_Faktur=b.no_faktur and "
								'                                            SQL = SQL & "b.kode_Perusahaan=c.kode_Perusahaan And b.Serial_Number_Awal=c.serial_number and "
								'                                            SQL = SQL & "c.serial_number = '" & Produk_SN & "' and "
								'                                            SQL = SQL & "selesai is null and a.status is null "
								'                                            Using Dr = OpenTrans(SQL)
								'                                                If Dr.Read Then
								'                                                    Dr.Close()
								'                                                    CloseTrans()
								'                                                    CloseConn()
								'                                                    Exit Sub
								'                                                Else
								'                                                    Dr.Close()
								'                                                End If
								'                                            End Using

								'                                            Dim nilai_kecildetail As Double = 0
								'                                            SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & Produk_KdBarang & "', '" & Produk_Satuan & "',"
								'                                            SQL = SQL & "'" & Produk_SatuanKecil & "', '" & HilangkanTanda(Produk_Jumlah) & "' ) as hasil"
								'                                            Using Dr1 = OpenTrans(SQL)
								'                                                If Dr1.Read Then
								'                                                    If General_Class.CekNULL(Dr1("hasil")) = "" Then
								'                                                        Dr1.Close()
								'                                                        CloseTrans()
								'                                                        CloseConn()
								'                                                        'MessageBox.Show("data konversi satuan kirim tidak ada ")
								'                                                        Dim Lvw As ListViewItem
								'                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: data konversi satuan kirim tidak ada")
								'                                                        Exit Sub
								'                                                    End If

								'                                                    nilai_kecildetail = Dr1("hasil")
								'                                                Else
								'                                                    Dr1.Close()
								'                                                    CloseTrans()
								'                                                    CloseConn()
								'                                                    'MessageBox.Show("data konversi satuan kirim tidak ada ")
								'                                                    Dim Lvw As ListViewItem
								'                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: data konversi satuan kirim tidak ada")
								'                                                    Exit Sub
								'                                                End If
								'                                            End Using

								'                                            Dim jenis_bags As String = ""
								'                                            Dim isi_per_bags As Double = 0
								'                                            SQL = "select Jenis_Kemasan, isnull(Isi_Per_Bags,0) as Isi_Per_Bags from barang where "
								'                                            SQL = SQL & "kode_perusahaan='" & KodePerusahaan & "' and "
								'                                            SQL = SQL & "kode_barang='" & Produk_KdBarang & "' and "
								'                                            SQL = SQL & "kode_stock_owner='" & Produk_Kd_SO & "'"
								'                                            Using Dr = OpenTrans(SQL)
								'                                                If Dr.Read Then
								'                                                    jenis_bags = Dr("Jenis_Kemasan").ToString.ToUpper
								'                                                    isi_per_bags = Dr("Isi_Per_Bags")
								'                                                Else
								'                                                    Dr.Close()
								'                                                    CloseTrans()
								'                                                    CloseConn()
								'                                                    'MessageBox.Show("Data barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                    Dim Lvw As ListViewItem
								'                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data barang tidak ditemukan . . ! !")
								'                                                    Exit Sub
								'                                                End If
								'                                            End Using

								'                                            Dim sisaPotong As Double = 0
								'                                            Dim JumlahDipotong As Double = 0
								'                                            SQL = "select a.Jumlah as Stock_SN, a.serial_number "
								'                                            SQL = SQL & "from Barang_SN a where "
								'                                            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' "
								'                                            SQL = SQL & "and a.serial_number = '" & Produk_SN & "' "
								'                                            SQL = SQL & "and a.Kode_stock_owner = '" & Produk_Kd_SO & "' and a.jumlah<>0 "
								'                                            SQL = SQL & "order by a.Tgl_Expired "
								'                                            Using Ds3 = BindingTrans(SQL)
								'                                                If Ds3.Tables("MyTable").Rows.Count <> 0 Then

								'                                                    sisaPotong = Val(HilangkanTanda(nilai_kecildetail))

								'                                                    For Index As Integer = 0 To Ds3.Tables("MyTable").Rows.Count - 1
								'                                                        If sisaPotong = 0 Then
								'                                                            Exit For
								'                                                        ElseIf sisaPotong < 0 Then
								'                                                            CloseTrans()
								'                                                            CloseConn()
								'                                                            'MessageBox.Show("Terdapat Kesalahan saat Potong Barang Produksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                            Dim Lvw As ListViewItem
								'                                                            Lvw = ListView1.Items.Add("Automatization - Waste Product: Terdapat Kesalahan saat Potong Barang Produksi")
								'                                                            Exit Sub
								'                                                        End If

								'                                                        Dim JumlahInsert As Double = 0
								'                                                        Dim Satuan As String = ""

								'                                                        Dim Data_SN As String = Ds3.Tables("MyTable").Rows(Index).Item("serial_number")

								'                                                        If sisaPotong < Val(HilangkanTanda(Ds3.Tables("MyTable").Rows(Index).Item("Stock_SN"))) Or sisaPotong = Val(HilangkanTanda(Ds3.Tables("MyTable").Rows(Index).Item("Stock_SN"))) Then

								'                                                            JumlahInsert = sisaPotong
								'                                                            ' Satuan = .Rows(Index).Item("Satuan").ToString.Trim

								'                                                            JumlahDipotong += sisaPotong
								'                                                            sisaPotong = 0

								'                                                        ElseIf sisaPotong > Val(HilangkanTanda(Ds3.Tables("MyTable").Rows(Index).Item("Stock_SN"))) Then

								'                                                            JumlahInsert = Val(HilangkanTanda(Format(Ds3.Tables("MyTable").Rows(Index).Item("Stock_SN"), "N4")))
								'                                                            'Satuan = .Rows(Index).Item("Satuan").ToString.Trim

								'                                                            JumlahDipotong += Val(HilangkanTanda(Format(Ds3.Tables("MyTable").Rows(Index).Item("Stock_SN"), "N4")))
								'                                                            sisaPotong = sisaPotong - Val(HilangkanTanda(Format(Ds3.Tables("MyTable").Rows(Index).Item("Stock_SN"), "N4")))
								'                                                        Else
								'                                                            CloseTrans()
								'                                                            CloseConn()
								'                                                            'MessageBox.Show("Terjadi Kesalaham pada Barang SN untuk Kode Barang !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                            Dim Lvw As ListViewItem
								'                                                            Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi Kesalaham pada Barang SN untuk Kode Barang !")
								'                                                            Exit Sub
								'                                                        End If

								'                                                        Dim Jumlah_Bags = 0
								'                                                        If jenis_bags = "ORIGINAL BAGS" Then
								'                                                            Jumlah_Bags = JumlahInsert / isi_per_bags
								'                                                        End If

								'                                                        '========================================================
								'                                                        '=     INSERT N_EMI_Transaksi_Transfer_Waste_Detail     =
								'                                                        '========================================================
								'                                                        Dim nilai_BesarInsert As Double = 0
								'                                                        SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & Produk_KdBarang & "', '" & Produk_SatuanKecil & "',"
								'                                                        SQL = SQL & "'" & Produk_Satuan & "', '" & HilangkanTanda(JumlahInsert) & "' ) as hasil"
								'                                                        Using Dr1 = OpenTrans(SQL)
								'                                                            If Dr1.Read Then
								'                                                                If General_Class.CekNULL(Dr1("hasil")) = "" Then
								'                                                                    Dr1.Close()
								'                                                                    CloseTrans()
								'                                                                    CloseConn()
								'                                                                    'MessageBox.Show("data konversi satuan kirim tidak ada ")
								'                                                                    Dim Lvw As ListViewItem
								'                                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: data konversi satuan kirim tidak ada")
								'                                                                    Exit Sub
								'                                                                End If

								'                                                                nilai_BesarInsert = Dr1("hasil")
								'                                                            Else
								'                                                                Dr1.Close()
								'                                                                CloseTrans()
								'                                                                CloseConn()
								'                                                                'MessageBox.Show("data konversi satuan kirim tidak ada ")
								'                                                                Dim Lvw As ListViewItem
								'                                                                Lvw = ListView1.Items.Add("Automatization - Waste Product: data konversi satuan kirim tidak ada")
								'                                                                Exit Sub
								'                                                            End If
								'                                                        End Using

								'                                                        Dim ID_Warehouse_Akhir As String = ""
								'                                                        SQL = "Select top 1 a.Id_WMS_Warehouse_Position, a.Keterangan from "
								'                                                        SQL = SQL & "view_warehouse_position a "
								'                                                        SQL = SQL & "where a.kode_perusahaan='" & KodePerusahaan & "' "
								'                                                        SQL = SQL & "and isnull((select top(1)  'Y' from view_warehouse_position_detail b where "
								'                                                        SQL = SQL & "a.id_wms_warehouse_position = b.id_wms_warehouse_position And a.Kode_Perusahaan = b.KOde_Perusahaan "
								'                                                        SQL = SQL & "And b.kode_Barang Is null ),null) ='Y' "
								'                                                        SQL = SQL & "and a.Kode_Stock_Owner = '" & Produk_Kd_SO & "' "
								'                                                        SQL = SQL & "order by a.Keterangan "
								'                                                        Using Dr = OpenTrans(SQL)
								'                                                            If Dr.Read Then
								'                                                                ID_Warehouse_Akhir = Dr("Id_WMS_Warehouse_Position")
								'                                                            End If
								'                                                        End Using

								'                                                        SQL = "insert into N_EMI_Transaksi_Transfer_Waste_Det(Kode_Perusahaan, No_faktur, Id_Wms_Awal, No_Pallet_Awal, Id_Wms_Tujuan, "
								'                                                        SQL = SQL & "Serial_Number_Awal, Jumlah, Jumlah_Barang, Jumlah_Bags, Warna, Urut_TF) values( "
								'                                                        SQL = SQL & "'" & KodePerusahaan & "', '" & Trim(No_Faktur_Waste_process) & "', '" & Produk_IDWarehouse_Tujuan & "', "
								'                                                        SQL = SQL & "'" & Produk_Pallet_Tujuan & "', '" & ID_Warehouse_Akhir & "', '" & Data_SN & "', "
								'                                                        SQL = SQL & "'" & nilai_BesarInsert & "', '" & JumlahInsert & "', "
								'                                                        SQL = SQL & "'" & Jumlah_Bags & "', "
								'                                                        SQL = SQL & "'" & Produk_Warna & "', '" & x_ident_current & "')"
								'                                                        ExecuteTrans(SQL)

								'                                                    Next
								'                                                Else
								'                                                    CloseTrans()
								'                                                    CloseConn()
								'                                                    'MessageBox.Show("Terjadi Kesalahan Pada Barang !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                    Dim Lvw As ListViewItem
								'                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi Kesalahan Pada Barang !")
								'                                                    Exit Sub
								'                                                End If

								'                                            End Using

								'                                            If Val(HilangkanTanda(JumlahDipotong)) <> Val(HilangkanTanda(nilai_kecildetail)) Then
								'                                                CloseTrans()
								'                                                CloseConn()
								'                                                'MessageBox.Show("Terjadi Kesalahan Saat Memotong Stock Barang !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                Dim Lvw As ListViewItem
								'                                                Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi Kesalahan Saat Memotong Stock Barang !")
								'                                                Exit Sub
								'                                            End If

								'                                        Next
								'                                    End If
								'                                End Using

								'                                Dim Validasi_QrCode As String = ""
								'                                Dim Validasi_Batch As String = ""
								'                                Dim Validasi_SN As String = ""
								'                                Dim Validasi_ExpDate As String = ""
								'                                Dim Validasi_TglMasuk As String = ""
								'                                Dim Validasi_KDBarang As String = ""
								'                                Dim Validasi_Jumlah As String = ""
								'                                Dim Validasi_Jumlah_Bags As String = ""
								'                                Dim Validasi_Urut_Oto As String = ""
								'                                Dim Validasi_Warna As String = ""
								'                                Dim Validasi_Satuan As String = ""
								'                                Dim Validasi_Satuan_Kecil As String = ""
								'                                Dim Validasi_Rak_Tujuan As String = ""
								'                                Dim Validasi_Pallet_Tujuaan As String = ""
								'                                Dim Validasi_KDSo As String = ""

								'                                '==============================
								'                                '=     VALIDASI PENGAJUAN     =
								'                                '==============================
								'                                SQL = "select a.No_Faktur, a.Kode_Stock_Owner, b.Kode_Barang, c.Jumlah, c.Jumlah_Bags, b.Satuan, c.Serial_Number_Awal, "
								'                                SQL = SQL & "b.Satuan_Barang, c.Urut_Oto, c.Warna, c.Id_Wms_Tujuan, d.Qr_Code, d.Batch_Number, d.Tgl_Expired, d.Tgl_Masuk "
								'                                SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
								'                                SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
								'                                SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
								'                                SQL = SQL & "inner join Barang_SN d on c.kode_perusahaan = d.kode_perusahaan and c.Serial_Number_Awal = d.Serial_Number "
								'                                SQL = SQL & "where a.Status is null "
								'                                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
								'                                SQL = SQL & "and a.no_Faktur = '" & No_Faktur_Waste_process & "' "
								'                                Using Ds3 = BindingTrans(SQL)
								'                                    If Ds3.Tables("MyTable").Rows.Count <> 0 Then
								'                                        For k As Integer = 0 To Ds3.Tables("MyTable").Rows.Count - 1

								'                                            Validasi_QrCode = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Qr_Code"))
								'                                            Validasi_Batch = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Batch_Number"))
								'                                            Validasi_SN = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Serial_Number_Awal"))
								'                                            Validasi_ExpDate = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Tgl_Expired"))
								'                                            Validasi_TglMasuk = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Tgl_Masuk"))
								'                                            Validasi_KDBarang = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Kode_Barang"))
								'                                            Validasi_Jumlah = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Jumlah"))
								'                                            Validasi_Jumlah_Bags = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Jumlah_Bags"))
								'                                            Validasi_Urut_Oto = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Urut_Oto"))
								'                                            Validasi_Warna = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Warna"))
								'                                            Validasi_Satuan = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Satuan"))
								'                                            Validasi_Satuan_Kecil = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Satuan_Barang"))
								'                                            Validasi_Rak_Tujuan = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Id_Wms_Tujuan"))
								'                                            Validasi_KDSo = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Kode_Stock_Owner"))

								'                                            SQL = "select a.Status, c.Selesai, b.Flag_Timbang "
								'                                            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Detail b, N_EMI_Transaksi_Transfer_Waste_Det c "
								'                                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.no_Faktur = b.No_Faktur and "
								'                                            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.no_Faktur = c.No_Faktur and b.urut_oto=c.urut_TF "
								'                                            SQL = SQL & "and a.No_Faktur = '" & No_Faktur_Waste_process & "' and c.urut_oto = '" & Validasi_Urut_Oto & "'  "
								'                                            Using Dr = OpenTrans(SQL)
								'                                                If Dr.Read Then

								'                                                    If General_Class.CekNULL(Dr("status")) <> "" Then
								'                                                        Dr.Close()
								'                                                        CloseTrans()
								'                                                        CloseConn()
								'                                                        'MessageBox.Show("Proses tidak bisa dilanjutkan, barang sudah dibatalkan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                        Dim Lvw As ListViewItem
								'                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Proses tidak bisa dilanjutkan, barang sudah dibatalkan!!")
								'                                                        Exit Sub
								'                                                    ElseIf General_Class.CekNULL(Dr("selesai")) = "Y" Then
								'                                                        Dr.Close()
								'                                                        CloseTrans()
								'                                                        CloseConn()
								'                                                        'MessageBox.Show("Terjadi kesalahan, barang sudah selesai diproses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                        Dim Lvw As ListViewItem
								'                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi kesalahan, barang sudah selesai diproses!")
								'                                                        Exit Sub
								'                                                    ElseIf General_Class.CekNULL(Dr("Flag_Timbang")) = "Y" Then
								'                                                        Dr.Close()
								'                                                        CloseTrans()
								'                                                        CloseConn()
								'                                                        'MessageBox.Show("Terjadi kesalahan, ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                        Dim Lvw As ListViewItem
								'                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi kesalahan")
								'                                                        Exit Sub
								'                                                    End If
								'                                                Else
								'                                                    Dr.Close()
								'                                                    CloseTrans()
								'                                                    CloseConn()
								'                                                    'MessageBox.Show("Data barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                    Dim Lvw As ListViewItem
								'                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data barang tidak ditemukan!")
								'                                                    Exit Sub
								'                                                End If
								'                                            End Using

								'                                            SQL = "Select Top(1) nomor_urut from view_warehouse_position_detail where "
								'                                            SQL = SQL & "kode_Perusahaan ='" & KodePerusahaan & "' and kode_barang is null and "
								'                                            SQL = SQL & "id_wms_warehouse_position = '" & Validasi_Rak_Tujuan & "' "
								'                                            SQL = SQL & "order by nomor_urut "
								'                                            Using dr = OpenTrans(SQL)
								'                                                If dr.Read Then
								'                                                    Validasi_Pallet_Tujuaan = dr("nomor_urut")
								'                                                Else
								'                                                    dr.Close()
								'                                                    CloseTrans()
								'                                                    CloseConn()
								'                                                    'MessageBox.Show("data Rak Sudah Penuh . . ! ! ")
								'                                                    Dim Lvw As ListViewItem
								'                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: data Rak Sudah Penuh . . ! ! ")
								'                                                    Exit Sub
								'                                                End If
								'                                            End Using

								'                                            '====================================
								'                                            '=       CONVERT SATUAN KECIL       =
								'                                            '====================================
								'                                            Dim Validasi_nilai_kecildetail As Double = 0
								'                                            SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & Validasi_KDBarang & "', '" & Validasi_Satuan & "',"
								'                                            SQL = SQL & "'" & Validasi_Satuan_Kecil & "', '" & Validasi_Jumlah & "' ) as hasil"
								'                                            Using Dr1 = OpenTrans(SQL)
								'                                                If Dr1.Read Then
								'                                                    If General_Class.CekNULL(Dr1("hasil")) = "" Then
								'                                                        Dr1.Close()
								'                                                        CloseTrans()
								'                                                        CloseConn()
								'                                                        'MessageBox.Show("data konversi satuan kirim tidak ada ")
								'                                                        Dim Lvw As ListViewItem
								'                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: data konversi satuan kirim tidak ada ")
								'                                                        Exit Sub
								'                                                    End If

								'                                                    Validasi_nilai_kecildetail = Dr1("hasil")
								'                                                Else
								'                                                    Dr1.Close()
								'                                                    CloseTrans()
								'                                                    CloseConn()
								'                                                    'MessageBox.Show("data konversi satuan kirim tidak ada ")
								'                                                    Dim Lvw As ListViewItem
								'                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: data konversi satuan kirim tidak ada ")
								'                                                    Exit Sub
								'                                                End If
								'                                            End Using

								'                                            '============================
								'                                            '=       POTONG STOCK       =
								'                                            '============================

								'                                            Dim nilai_persediaan_min As Double = 0
								'                                            SQL = "select round(dbo.get_hpp(serial_number) * " & Validasi_nilai_kecildetail & ", 2) as rp_persediaan_min from barang_sn where "
								'                                            SQL = SQL & "Kode_Stock_Owner='" & Validasi_KDSo & "' and Kode_Barang='" & Validasi_KDBarang & "' "
								'                                            SQL = SQL & "and Serial_Number='" & Validasi_SN & "'"
								'                                            Using dr = OpenTrans(SQL)
								'                                                If dr.Read Then
								'                                                    nilai_persediaan_min = dr("rp_persediaan_min")
								'                                                Else
								'                                                    dr.Close()
								'                                                    CloseTrans()
								'                                                    CloseConn()
								'                                                    'MessageBox.Show("Data SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                    Dim Lvw As ListViewItem
								'                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data SN tidak ditemukan!")
								'                                                    Exit Sub
								'                                                End If
								'                                            End Using

								'                                            Dim Nama As String = ""
								'                                            'Dim jumlahAkhir As Double = Val(dgv_GoodStock) - Val(dgv_Jumlah)
								'                                            SQL = "select Nama, Kode_Barang, round(good_stock,4) as good_stock, Jumlah_Bags from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Validasi_KDSo & "' "
								'                                            SQL = SQL & "and Kode_Barang='" & Validasi_KDBarang & "' "
								'                                            Using dr = OpenTrans(SQL)
								'                                                If dr.Read Then
								'                                                    Nama = dr("Kode_Barang")
								'                                                    If dr("good_stock") < Validasi_nilai_kecildetail Then
								'                                                        dr.Close()
								'                                                        CloseTrans()
								'                                                        CloseConn()
								'                                                        'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
								'                                                        Dim Lvw As ListViewItem
								'                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.")
								'                                                        Exit Sub
								'                                                    ElseIf dr("Jumlah_Bags") < Validasi_Jumlah_Bags Then
								'                                                        dr.Close()
								'                                                        CloseTrans()
								'                                                        CloseConn()
								'                                                        'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
								'                                                        Dim Lvw As ListViewItem
								'                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.")
								'                                                        Exit Sub
								'                                                    Else
								'                                                        dr.Close()
								'                                                        SQL = "update barang set Good_Stock = Good_Stock - Round(" & Validasi_nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags - " & Validasi_Jumlah_Bags & " "
								'                                                        SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Validasi_KDSo & "' "
								'                                                        SQL = SQL & " and Kode_Barang='" & Validasi_KDBarang & "'"
								'                                                        ExecuteTrans(SQL)
								'                                                    End If
								'                                                Else
								'                                                    dr.Close()
								'                                                    CloseTrans()
								'                                                    CloseConn()
								'                                                    'MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                    Dim Lvw As ListViewItem
								'                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Barang " & Nama & " tidak ditemukan!")
								'                                                    Exit Sub
								'                                                End If
								'                                            End Using

								'                                            SQL = "select round(jumlah,4) as jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Validasi_KDSo & "' "
								'                                            SQL = SQL & "and Kode_Barang='" & Validasi_KDBarang & "' "
								'                                            SQL = SQL & "and Serial_Number='" & Validasi_SN & "'"
								'                                            Using dr = OpenTrans(SQL)
								'                                                If dr.Read Then
								'                                                    If dr("jumlah") < Validasi_nilai_kecildetail Then
								'                                                        dr.Close()
								'                                                        CloseTrans()
								'                                                        CloseConn()
								'                                                        'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
								'                                                        Dim Lvw As ListViewItem
								'                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.")
								'                                                        Exit Sub
								'                                                    ElseIf dr("Jumlah_Bags") < Validasi_Jumlah_Bags Then
								'                                                        dr.Close()
								'                                                        CloseTrans()
								'                                                        CloseConn()
								'                                                        'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
								'                                                        Dim Lvw As ListViewItem
								'                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.")
								'                                                        Exit Sub
								'                                                    Else
								'                                                        dr.Close()
								'                                                        SQL = "update barang_sn set jumlah = jumlah - Round(" & Validasi_nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags - " & Validasi_Jumlah_Bags & " "
								'                                                        SQL = SQL & "where Kode_Stock_Owner='" & Validasi_KDSo & "' and Kode_Barang='" & Validasi_KDBarang & "' "
								'                                                        SQL = SQL & "and Serial_Number='" & Validasi_SN & "'"
								'                                                        ExecuteTrans(SQL)
								'                                                    End If
								'                                                Else
								'                                                    dr.Close()
								'                                                    CloseTrans()
								'                                                    CloseConn()
								'                                                    'MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                    Dim Lvw As ListViewItem
								'                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Barang " & Nama & " tidak ditemukan!")
								'                                                    Exit Sub
								'                                                End If
								'                                            End Using

								'                                            '====================================
								'                                            '=       CEK KESESUAIAN STOCK       =
								'                                            '====================================
								'                                            SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
								'                                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
								'                                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
								'                                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
								'                                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
								'                                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
								'                                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & Validasi_KDSo & "' "
								'                                            SQL = SQL & "AND a.Kode_Barang = '" & Validasi_KDBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
								'                                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
								'                                            Using Ds4 = BindingTrans(SQL)
								'                                                If Ds4.Tables("MyTable").Rows.Count <> 0 Then
								'                                                    If Ds4.Tables("MyTable").Rows(0).Item("good_stock") <> Ds4.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds4.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds4.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
								'                                                        CloseTrans()
								'                                                        CloseConn()
								'                                                        'MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                        Dim Lvw As ListViewItem
								'                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi Kesalahan . . ! !")
								'                                                        Exit Sub
								'                                                    End If
								'                                                Else
								'                                                    CloseTrans()
								'                                                    CloseConn()
								'                                                    'MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                    Dim Lvw As ListViewItem
								'                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data tidak ditemukan . . ! !")
								'                                                    Exit Sub
								'                                                End If
								'                                            End Using

								'                                            '==============================
								'                                            '=       INSERT SN BARU       =
								'                                            '==============================

								'                                            Dim hargaIsn As String = ""
								'                                            Dim namaBarang As String = ""
								'                                            Dim warnaLama As String = ""

								'                                            'Ambil Data Lama
								'                                            SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.warna "
								'                                            SQL = SQL & "from barang_sn a, barang b "
								'                                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
								'                                            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
								'                                            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
								'                                            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
								'                                            SQL = SQL & "and a.Kode_Stock_Owner='" & Validasi_KDSo & "' "
								'                                            SQL = SQL & "and a.Kode_Barang ='" & Validasi_KDBarang & "' "
								'                                            SQL = SQL & "and a.Serial_Number='" & Validasi_SN & "' "
								'                                            'SQL = SQL & "and a.Jumlah <> 0 "
								'                                            Using Dr = OpenTrans(SQL)
								'                                                Do While Dr.Read
								'                                                    hargaIsn = Get_Harga_SN(Dr("Serial_Number"))
								'                                                    QrLama = General_Class.CekNULL(Dr("Qr_Code"))
								'                                                    batchLama = General_Class.CekNULL(Dr("Batch_Number"))
								'                                                    namaBarang = General_Class.CekNULL(Dr("Nama"))
								'                                                    expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
								'                                                    warnaLama = General_Class.CekNULL(Dr("warna"))
								'                                                Loop
								'                                            End Using

								'                                            'GENERATE SN BARU
								'                                            Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
								'                                            Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
								'                                            Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

								'                                            Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)

								'                                            'INSERT BARANG SN BARU
								'                                            SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah,  Jumlah_Bags, "
								'                                            SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk, Blok_SN) "
								'                                            SQL = SQL & "select Kode_Perusahaan, '" & Validasi_KDSo & "', Kode_Barang, '" & SN_Baru & "', '" & Validasi_nilai_kecildetail & "', " & Validasi_Jumlah_Bags & ", "
								'                                            SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, '" & Validasi_Rak_Tujuan & "', id_Susunan , Qr_Code, '" & newKodeUnikBerjalan & "', "
								'                                            SQL = SQL & "Kode_Unik_Asal, '" & Validasi_Pallet_Tujuaan & "', batch_number, '" & Validasi_Warna & "', Tgl_Masuk, 'Y' "
								'                                            SQL = SQL & "from Barang_SN "
								'                                            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
								'                                            SQL = SQL & "and Kode_Stock_Owner='" & Validasi_KDSo & "' "
								'                                            SQL = SQL & "and Kode_Barang='" & Validasi_KDBarang & "' "
								'                                            SQL = SQL & "and Serial_Number='" & Validasi_SN & "' "
								'                                            ExecuteTrans(SQL)

								'                                            '============================
								'                                            '=       TAMBAH STOCK       =
								'                                            '============================

								'                                            SQL = "update barang set Good_Stock= Good_Stock + Round(" & Validasi_nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags + " & Validasi_Jumlah_Bags & " "
								'                                            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Validasi_KDSo & "' "
								'                                            SQL = SQL & " and Kode_Barang='" & Validasi_KDBarang & "'"
								'                                            ExecuteTrans(SQL)

								'                                            'CEK KESESUAIAN STOCK
								'                                            SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
								'                                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
								'                                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
								'                                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
								'                                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
								'                                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
								'                                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & Validasi_KDSo & "' "
								'                                            SQL = SQL & "AND a.Kode_Barang = '" & Validasi_KDBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
								'                                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
								'                                            Using Ds4 = BindingTrans(SQL)

								'                                                If Ds4.Tables("MyTable").Rows.Count <> 0 Then
								'                                                    If Ds4.Tables("MyTable").Rows(0).Item("good_stock") <> Ds4.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds4.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds4.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
								'                                                        CloseTrans()
								'                                                        CloseConn()
								'                                                        'MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                        Dim Lvw As ListViewItem
								'                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi Kesalahan . . ! !")
								'                                                        Exit Sub
								'                                                    End If
								'                                                Else
								'                                                    CloseTrans()
								'                                                    CloseConn()
								'                                                    'MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                    Dim Lvw As ListViewItem
								'                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data tidak ditemukan . . ! !")
								'                                                    Exit Sub
								'                                                End If

								'                                            End Using

								'#Region "Jurnal"

								'                                            'dari
								'                                            Dim inisial_faktur_dari As String = ""
								'                                            Dim akun_persediaan_dari As String = ""
								'                                            Dim akun_persediaan_tujuan As String = ""

								'                                            SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
								'                                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Validasi_KDSo & "' "
								'                                            Using Dr = OpenTrans(SQL)
								'                                                If Dr.Read Then
								'                                                    'akun_persediaan_dari = Dr("persediaan")
								'                                                    inisial_faktur_dari = Dr("inisial_faktur")
								'                                                Else
								'                                                    Dr.Close()
								'                                                    CloseTrans()
								'                                                    CloseConn()
								'                                                    'MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                    Dim Lvw As ListViewItem
								'                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data akun tidak ditemukan!")
								'                                                    Exit Sub
								'                                                End If
								'                                            End Using

								'                                            SQL = "select c.akun_Persediaan "
								'                                            SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
								'                                            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
								'                                            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
								'                                            SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
								'                                            SQL = SQL & "and b.kode_stock_owner = '" & Validasi_KDSo & "' and b.Kode_Barang='" & Validasi_KDBarang & "' "
								'                                            Using Dr = OpenTrans(SQL)
								'                                                If Dr.Read Then
								'                                                    akun_persediaan_dari = Dr("akun_Persediaan")
								'                                                Else
								'                                                    Dr.Close()
								'                                                    CloseTrans()
								'                                                    CloseConn()
								'                                                    'MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                    Dim Lvw As ListViewItem
								'                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data akun tidak ditemukan!")
								'                                                    Exit Sub
								'                                                End If
								'                                            End Using

								'                                            SQL = "select c.akun_Persediaan "
								'                                            SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
								'                                            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
								'                                            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
								'                                            SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
								'                                            SQL = SQL & "and b.kode_stock_owner = '" & Validasi_KDSo & "' and b.Kode_Barang='" & Validasi_KDBarang & "' "
								'                                            Using Dr = OpenTrans(SQL)
								'                                                If Dr.Read Then
								'                                                    akun_persediaan_tujuan = Dr("akun_Persediaan")
								'                                                Else
								'                                                    Dr.Close()
								'                                                    CloseTrans()
								'                                                    CloseConn()
								'                                                    'MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                    Dim Lvw As ListViewItem
								'                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data akun tidak ditemukan!")
								'                                                    Exit Sub
								'                                                End If
								'                                            End Using

								'                                            Dim Kode_voucher As String = ""
								'                                            Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
								'                                            Dim pagenumber As Integer = 1

								'                                            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
								'                                            SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
								'                                            SQL = SQL & "'" & Kode_voucher & "', "
								'                                            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
								'                                            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
								'                                            SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock " & No_Faktur_Waste & "', '', "
								'                                            SQL = SQL & "'-', '" & UserID & "')"
								'                                            ExecuteTrans(SQL)

								'                                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
								'                                                                  Strings.Mid(akun_persediaan_dari, 2, 1),
								'                                                                  Strings.Mid(Ganti(akun_persediaan_dari), 3),
								'                                                                  KodePerusahaan, KodeProyek, "Persedian " & No_Faktur_Waste, "0", nilai_persediaan_min, pagenumber, Validasi_KDSo, Bahasa_Pilihan, Ket_Cost_Center_HO)
								'                                            ExecuteTrans(SQL)
								'                                            pagenumber = pagenumber + 1

								'                                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
								'                                                                 Strings.Mid(akun_persediaan_tujuan, 2, 1),
								'                                                                 Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
								'                                                                 KodePerusahaan, KodeProyek, "Persedian " & No_Faktur_Waste, nilai_persediaan_min, "0", pagenumber, Validasi_KDSo, Bahasa_Pilihan, Ket_Cost_Center_HO)
								'                                            ExecuteTrans(SQL)
								'                                            pagenumber = pagenumber + 1

								'                                            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
								'                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
								'                                            SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
								'                                            Using Dr = OpenTrans(SQL)
								'                                                If Dr.Read Then
								'                                                    If Dr("debit") <> Dr("kredit") Then
								'                                                        Dr.Close()
								'                                                        CloseTrans()
								'                                                        CloseConn()
								'                                                        'MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                        Dim Lvw As ListViewItem
								'                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Jurnal salah!")
								'                                                        Exit Sub
								'                                                    End If
								'                                                Else
								'                                                    Dr.Close()
								'                                                    CloseTrans()
								'                                                    CloseConn()
								'                                                    'MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								'                                                    Dim Lvw As ListViewItem
								'                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data jurnal tidak ditemukan!")
								'                                                    Exit Sub
								'                                                End If
								'                                            End Using

								'#End Region

								'                                            SQL = "insert into N_EMI_Transaksi_Transfer_Waste_Det2(kode_perusahaan, No_faktur, Urut_Det, No_Pallet, "
								'                                            SQL = SQL & "Serial_Number, Jumlah, UserID, Tanggal, Jam, Kode_Voucher, Jumlah_Bags) values( "
								'                                            SQL = SQL & "'" & KodePerusahaan & "', '" & No_Faktur_Waste_process & "', '" & Validasi_Urut_Oto & "', "
								'                                            SQL = SQL & "'" & Validasi_Pallet_Tujuaan & "', '" & SN_Baru & "', '" & Validasi_nilai_kecildetail & "', "
								'                                            SQL = SQL & "'" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
								'                                            SQL = SQL & "'" & Kode_voucher & "', '" & Validasi_Jumlah_Bags & "') "
								'                                            ExecuteTrans(SQL)

								'                                            SQL = "update N_EMI_Transaksi_Transfer_Waste_Det set  "
								'                                            SQL = SQL & "Selesai = 'Y' "
								'                                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
								'                                            SQL = SQL & "and urut_oto = '" & Validasi_Urut_Oto & "' "
								'                                            ExecuteTrans(SQL)

								'                                        Next
								'                                    End If
								'                                End Using

#End Region

								'SQL = "update N_EMI_Transaksi_Approval_Waste set Flag_Selesai = 'Y' "
								'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Status is null and No_Transaksi = '" & No_Transaksi_Approval & "' and No_Faktur_Waste = '" & No_Faktur_Waste & "' "
								'ExecuteTrans(SQL)

								Cmd.Transaction.Commit()
							End If

						Next
					End If
				End With
			End Using

			'If True Then
			'    CloseTrans()
			'    CloseConn()
			'    MessageBox.Show("TahanS")
			'    Exit Sub
			'End If

			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			Dim Lvw As ListViewItem
			Lvw = ListView1.Items.Add("Automatization - Waste Product: " & ex.Message)
			Exit Sub
		End Try
	End Sub

End Class