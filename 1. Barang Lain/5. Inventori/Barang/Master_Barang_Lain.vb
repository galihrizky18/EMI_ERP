Public Class Master_Barang_Lain
	Dim arrkolom, arrorder, arrJenisBarang, arrJenisGudang, arrKategoriGudang, arrId_kategori_qc, arrId_kategori_PO, arrId_Klasifikasi_Bahan, arrprefix_Klasifikasi_Bahan, arrid_Routing As New ArrayList
	Dim arrId_Klasifikasi_Bahan2, arrprefix_Klasifikasi_Bahan2, arrKategori, arrKelompok, arrKodeAktiva As New ArrayList
	Dim arrKategoriJenis, arrPrefix_Kategori As New ArrayList
	Dim arrSubKategoriJenis, arrPrefix_Sub_Kategori As New ArrayList
	Dim arrSubKategoriJenis1, arrPrefix_Sub_Kategori1 As New ArrayList
	Dim arrSubKategoriJenis2, arrPrefix_Sub_Kategori2 As New ArrayList
	Public arrSubKategoriJenis3, arrPrefix_Sub_Kategori3 As New ArrayList
	Dim hrg_jual_apa As String
	Dim text_hrg_jual_apa, xurut_departement, xNoFakturPengajuanBrgBru As String
	Dim boleh_lihat_global As Boolean
	Dim arrSatuanTurunan, arrGudangRawMaterial As New ArrayList
	Dim arrcari As New ArrayList
	Dim xdari As String
	Dim xFrom As String  ' warehouse | department
	Dim zNo_Pengajuan_Barang_Baru As String

	Public Sub Cari2(ByVal semua As String)
		Try
			OpenConn()

			DataGridView1.Rows.Clear()

			'SQL = "select a.No_Faktur,a.Kode_Stock_Owner, a.Kode_Barang, a.Nama_Barang, a.Jumlah, a.Jmlh_PR, a.Satuan, b.Keterangan as Cost_Center, a.No_Urut, d.Lokasi, d.UserID,"
			'SQL = SQL & "isnull(( select c.Keterangan from N_EMI_Master_Gedung_Barang_Lain c where "
			'SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.ID_Gedung = c.ID_Gedung ), NULL) as Gedung, "
			'SQL = SQL & "e.Kategori_Jenis, e.Sub_Kategori_Jenis, e.Sub_Kategori_Jenis_1, e.Sub_Kategori_Jenis_2, e.Sub_Kategori_Jenis_3, "
			'SQL = SQL & "a.Id_Kategori_Jenis, a.Id_Sub_Kategori_Jenis, a.Id_Sub_Kategori_Jenis_1, a.Id_Sub_Kategori_Jenis_2, a.Id_Sub_Kategori_Jenis_3 "
			'SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a, EMI_Master_Cost_Center b, N_EMI_Purchase_Requisition_Barang_Lain_Departement d, "
			'SQL = SQL & "View_Kategori_Turunan e  where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'SQL = SQL & "and a.Id_Kategori_Jenis = e.Id_Kategori_Jenis and a.Id_Sub_Kategori_Jenis = e.Id_Sub_Kategori_Jenis and a.Id_Sub_Kategori_Jenis_1 = e.Id_Sub_Kategori_Jenis_1 "
			'SQL = SQL & "and a.Id_Sub_Kategori_Jenis_2 = e.Id_Sub_Kategori_Jenis_2 and a.Id_Sub_Kategori_Jenis_3 = e.Id_Sub_Kategori_Jenis_3 "
			'SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Cost_Center = b.Id_Cost_Center "
			'SQL = SQL & "and a.Flag_Sudah_PR is null and a.Kode_Barang = '-' "
			'SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = d.No_Faktur and d.Flag_Release = 'Y' and d.Flag_PR is null and a.Flag_Ajukan = 'Y' "

			'SQL = "union all"

			SQL = "select * from   N_EMI_View_Pengajuan_Barang_Baru_Lain a where a.kode_perusahaan = '" & KodePerusahaan & "' "

			If semua = "T" Then
				If CheckBox1.Checked Then
					'Pasang And
					If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

					SQL = SQL & "d.Tanggal between '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
				End If

				If CheckBox2.Checked Then
					'Pasang And
					If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

					SQL = SQL & arrcari.Item(CmbSatuan_Kolom.SelectedIndex) & " like '%" & TxtSatuan_Value.Text & "%' "
				End If
			End If
			SQL = SQL & "order by a.nama_barang asc   "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							DataGridView1.Rows.Add(1)
							DataGridView1.Rows.Item(i).Cells(0).Value = .Rows(i).Item("No_Faktur")
							DataGridView1.Rows.Item(i).Cells(1).Value = .Rows(i).Item("Kode_Stock_Owner")
							DataGridView1.Rows.Item(i).Cells(2).Value = .Rows(i).Item("Kode_Barang")
							DataGridView1.Rows.Item(i).Cells(3).Value = .Rows(i).Item("Nama_Barang")
							DataGridView1.Rows.Item(i).Cells(4).Value = (Format(.Rows(i).Item("jumlah") - .Rows(i).Item("Jmlh_PR"), "N2"))
							DataGridView1.Rows.Item(i).Cells(5).Value = .Rows(i).Item("Satuan")
							DataGridView1.Rows.Item(i).Cells(6).Value = .Rows(i).Item("Cost_Center")
							DataGridView1.Rows.Item(i).Cells(7).Value = .Rows(i).Item("Gedung")
							DataGridView1.Rows.Item(i).Cells(8).Value = .Rows(i).Item("No_Urut")
							DataGridView1.Rows.Item(i).Cells(9).Value = .Rows(i).Item("Lokasi")
							DataGridView1.Rows.Item(i).Cells(10).Value = .Rows(i).Item("Kategori_Jenis")
							DataGridView1.Rows.Item(i).Cells(11).Value = .Rows(i).Item("Sub_Kategori_Jenis")
							DataGridView1.Rows.Item(i).Cells(12).Value = .Rows(i).Item("Sub_Kategori_Jenis_1")
							DataGridView1.Rows.Item(i).Cells(13).Value = .Rows(i).Item("Sub_Kategori_Jenis_2")
							DataGridView1.Rows.Item(i).Cells(14).Value = .Rows(i).Item("Sub_Kategori_Jenis_3")
							DataGridView1.Rows.Item(i).Cells(15).Value = .Rows(i).Item("UserId")
							DataGridView1.Rows.Item(i).Cells(16).Value = .Rows(i).Item("Dari")
							DataGridView1.Rows.Item(i).Cells(17).Value = .Rows(i).Item("No_Pengajuan_Barang_Baru")

							If .Rows(i).Item("Kode_Barang") = "-" Then
								DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightSteelBlue
							End If
						Next
						'Else
						'    CloseConn()
						'    MessageBox.Show("Data tidak ditemuakan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'    Exit Sub
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

	Private Sub Cari(ByVal semua As String)
		Try
			OpenConn()

			ListView1.Items.Clear()
			Dim Lvw As ListViewItem

			'iniiiii
			Dim boleh_lihat As Boolean

			SQL = "select flag_hide_stock, "
			SQL = SQL & "ISNULL(("
			SQL = SQL & "select top(1) 'Y' from role_button a where a.kode_perusahaan = x.kode_perusahaan and "
			SQL = SQL & "a.userid = '" & UserID & "' and buttonname = 'LIHAT_STOCK'"
			SQL = SQL & "), 'T') AS boleh_lihat_stock "
			SQL = SQL & " from stock_owner x where x.kode_perusahaan = '" & KodePerusahaan & "' and "
			SQL = SQL & "x.kode_stock_owner = '" & ComboBox2.Text & "'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If Dr("flag_hide_stock") = "Y" Then
						If Dr("boleh_lihat_stock") = "Y" Then
							boleh_lihat = True
						Else
							boleh_lihat = False
						End If
					Else
						boleh_lihat = True
					End If
				Else
					boleh_lihat = False
				End If
			End Using

			Dim lokasi_pergudang As String = ""

			SQL = " select  b.Kode_stock_owner_gudang from stock_owner a , Binding_Lokasi_Gudang_Lain b where "
			SQL = SQL & "  a.Kode_Stock_Owner = '" & ComboBox2.Text & "'    and b.Gudang_Default = 'Y'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					lokasi_pergudang = Dr("Kode_Stock_Owner_Gudang")
				End If
			End Using

			SQL = "Select a.kode_stock_owner, a.kode_barang, a.Nama, a.satuan, a.good_stock, a.harga_beli_x, a.barang_sendiri ,pakai_sn,"
			SQL = SQL & "isnull((select kode_group_jenis from EMI_Group_Jenis_Lain x where "
			SQL = SQL & "a.kode_perusahaan = x.kode_perusahaan and a.id_group_jenis = x.id_group_jenis), null) as kode_group_jenis ,"
			SQL = SQL & "isnull((select kode_kategori_gudang from emi_kategori_gudang_barang_lain x where "
			SQL = SQL & "a.kode_perusahaan = x.kode_perusahaan and a.Id_Kategori_Gudang = x.id_kategori_gudang), null) as kode_kategori_gudang ,"
			SQL = SQL & "isnull((select keterangan from emi_master_kategori_gudang x where a.kode_perusahaan = x.kode_perusahaan "
			SQL = SQL & "and a.id_master_kategori_gudang_x = x.id_master_kategori_gudang), null) as keterangan_master_kategori_gudang , "
			SQL = SQL & "isnull((select keterangan from emi_kategori_qc x where a.kode_perusahaan = x.kode_perusahaan "
			SQL = SQL & "and a.ID_Kategori_QC_X = x.ID_Kategori_QC), null) as keterangan_QC , "
			SQL = SQL & "isnull((select keterangan from emi_kategori_po x where a.kode_perusahaan = x.kode_perusahaan "
			SQL = SQL & "and a.ID_Kategori_po = x.ID_Kategori_po), null) as keterangan_PO , "

			SQL = SQL & "isnull((select keterangan from emi_klasifikasi_bahan x where a.kode_perusahaan = x.kode_perusahaan "
			SQL = SQL & "and a.Id_Klasifikasi_Bahan_X = x.ID_Klasifikasi_Bahan), null) as keterangan_Bhn , "
			SQL = SQL & "isnull((select keterangan from emi_klasifikasi_bahan2 x where a.kode_perusahaan = x.kode_perusahaan "
			SQL = SQL & "and a.Id_Klasifikasi_Bahan2_X = x.ID_Klasifikasi_Bahan2), null) as keterangan_Bhn2 , "

			SQL = SQL & "isnull((select keterangan from EMI_Master_Routing x where a.kode_perusahaan = x.kode_perusahaan "
			SQL = SQL & "and a.ID_Routing_X = x.Id_Routing), null) as keterangan_Routing , "

			SQL = SQL & " isnull((select x.Kode_Kategori_Jenis from view_kategori_turunan x where x.kode_perusahaan = a.kode_perusahaan and  x.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3), null) as Kode_Kategori_Jenis,"

			SQL = SQL & " isnull((select x.Kategori_Jenis from view_kategori_turunan x where x.kode_perusahaan = a.kode_perusahaan and  x.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3), null) as Kategori_Jenis,"

			SQL = SQL & " isnull((select x.Kode_Sub_Kategori_Jenis from view_kategori_turunan x where x.kode_perusahaan = a.kode_perusahaan and  x.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3), null) as Kode_Sub_Kategori_Jenis,"

			SQL = SQL & " isnull((select x.Sub_Kategori_Jenis from view_kategori_turunan x where x.kode_perusahaan = a.kode_perusahaan and  x.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3), null) as Sub_Kategori_Jenis,"

			SQL = SQL & " isnull((select x.Kode_Sub_Kategori_Jenis_1 from view_kategori_turunan x where x.kode_perusahaan = a.kode_perusahaan and  x.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3), null) as Kode_Sub_Kategori_Jenis_1,"

			SQL = SQL & " isnull((select x.Sub_Kategori_Jenis_1 from view_kategori_turunan x where x.kode_perusahaan = a.kode_perusahaan and  x.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3), null) as Sub_Kategori_Jenis_1,"

			SQL = SQL & " isnull((select x.Kode_Sub_Kategori_Jenis_2 from view_kategori_turunan x where x.kode_perusahaan = a.kode_perusahaan and  x.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3), null) as Kode_Sub_Kategori_Jenis_2,"

			SQL = SQL & " isnull((select x.Sub_Kategori_Jenis_2 from view_kategori_turunan x where x.kode_perusahaan = a.kode_perusahaan and  x.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3), null) as Sub_Kategori_Jenis_2,"

			SQL = SQL & " isnull((select x.Kode_Sub_Kategori_Jenis_3 from view_kategori_turunan x where x.kode_perusahaan = a.kode_perusahaan and  x.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3), null) as Kode_Sub_Kategori_Jenis_3,"

			SQL = SQL & " isnull((select x.Sub_Kategori_Jenis_3 from view_kategori_turunan x where x.kode_perusahaan = a.kode_perusahaan and  x.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3), null) as Sub_Kategori_Jenis_3,"

			'SQL = SQL & "isnull((select Kode_Kategori_Jenis from N_EMI_Master_Kategori_Jenis x where a.kode_perusahaan = x.kode_perusahaan "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis), null) as kode_kategori_Jenis , "

			'SQL = SQL & "isnull((select Keterangan from N_EMI_Master_Kategori_Jenis x where a.kode_perusahaan = x.kode_perusahaan "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis), null) as kategori_Jenis , "

			'SQL = SQL & "isnull((select y.Kode_Sub_Kategori_Jenis from N_EMI_Master_Kategori_Jenis x, N_EMI_Master_Sub_Kategori_Jenis y "
			'SQL = SQL & "where a.kode_perusahaan = x.kode_perusahaan and y.kode_perusahaan = x.kode_perusahaan "
			'SQL = SQL & "and y.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.kode_perusahaan = a.kode_perusahaan "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.Kode_Sub_Kategori_Jenis = a.Kode_Sub_Kategori_Jenis), null) "
			'SQL = SQL & "as Kode_Sub_Kategori_Jenis, "

			'SQL = SQL & "isnull((select y.Keterangan from N_EMI_Master_Kategori_Jenis x, N_EMI_Master_Sub_Kategori_Jenis y "
			'SQL = SQL & "where a.kode_perusahaan = x.kode_perusahaan and y.kode_perusahaan = x.kode_perusahaan "
			'SQL = SQL & "and y.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.kode_perusahaan = a.kode_perusahaan "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.Kode_Sub_Kategori_Jenis = a.Kode_Sub_Kategori_Jenis), null) "
			'SQL = SQL & "as Sub_Kategori_Jenis, "

			'SQL = SQL & "isnull((select z.Kode_Sub_Kategori_Jenis_1 from N_EMI_Master_Kategori_Jenis x, N_EMI_Master_Sub_Kategori_Jenis y, "
			'SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis_1 z "
			'SQL = SQL & "where a.kode_perusahaan = x.kode_perusahaan  and y.kode_perusahaan = a.kode_perusahaan "
			'SQL = SQL & "and y.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.kode_perusahaan = x.kode_perusahaan "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.Kode_Sub_Kategori_Jenis = a.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and x.Kode_Perusahaan = z.Kode_Perusahaan and x.Kode_Kategori_Jenis = z.Kode_Kategori_Jenis "
			'SQL = SQL & "and y.Kode_Perusahaan = z.Kode_Perusahaan and y.Kode_Sub_Kategori_Jenis = z.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = z.Kode_Kategori_Jenis and a.Kode_Sub_Kategori_Jenis = z.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis_1 = z.Kode_Sub_Kategori_Jenis_1), null) "
			'SQL = SQL & "as Kode_Sub_Kategori_Jenis_1, "

			'SQL = SQL & "isnull((select z.Keterangan from N_EMI_Master_Kategori_Jenis x, N_EMI_Master_Sub_Kategori_Jenis y, "
			'SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis_1 z "
			'SQL = SQL & "where a.kode_perusahaan = x.kode_perusahaan  and y.kode_perusahaan = a.kode_perusahaan "
			'SQL = SQL & "and y.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.kode_perusahaan = x.kode_perusahaan "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.Kode_Sub_Kategori_Jenis = a.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and x.Kode_Perusahaan = z.Kode_Perusahaan and x.Kode_Kategori_Jenis = z.Kode_Kategori_Jenis "
			'SQL = SQL & "and y.Kode_Perusahaan = z.Kode_Perusahaan and y.Kode_Sub_Kategori_Jenis = z.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = z.Kode_Kategori_Jenis and a.Kode_Sub_Kategori_Jenis = z.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis_1 = z.Kode_Sub_Kategori_Jenis_1), null) "
			'SQL = SQL & "as Sub_Kategori_Jenis_1, "

			'SQL = SQL & "isnull((select zz.Kode_Sub_Kategori_Jenis_2 from N_EMI_Master_Kategori_Jenis x, N_EMI_Master_Sub_Kategori_Jenis y, "
			'SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis_1 z, N_EMI_Master_Sub_Kategori_Jenis_2 zz "
			'SQL = SQL & "where a.kode_perusahaan = x.kode_perusahaan  and y.kode_perusahaan = a.kode_perusahaan "
			'SQL = SQL & "and y.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.kode_perusahaan = x.kode_perusahaan "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.Kode_Sub_Kategori_Jenis = a.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and x.Kode_Perusahaan = z.Kode_Perusahaan and x.Kode_Kategori_Jenis = z.Kode_Kategori_Jenis "
			'SQL = SQL & "and y.Kode_Perusahaan = z.Kode_Perusahaan and y.Kode_Sub_Kategori_Jenis = z.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = z.Kode_Kategori_Jenis and a.Kode_Sub_Kategori_Jenis = z.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis_1 = z.Kode_Sub_Kategori_Jenis_1 "
			'SQL = SQL & "and x.Kode_Perusahaan = zz.Kode_Perusahaan and x.Kode_Kategori_Jenis = zz.Kode_Kategori_Jenis "
			'SQL = SQL & "and y.Kode_Perusahaan = zz.Kode_Perusahaan and y.Kode_Sub_Kategori_Jenis = zz.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and z.Kode_Perusahaan = zz.Kode_Perusahaan and z.Kode_Sub_Kategori_Jenis_1 = zz.Kode_Sub_Kategori_Jenis_1 "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = zz.Kode_Kategori_Jenis and a.Kode_Sub_Kategori_Jenis = zz.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis_1 = zz.Kode_Sub_Kategori_Jenis_1 and a.Kode_Sub_Kategori_Jenis_2 = zz.Kode_Sub_Kategori_Jenis_2"
			'SQL = SQL & "), null) as Kode_Sub_Kategori_Jenis_2, "

			'SQL = SQL & "isnull((select zz.Keterangan from N_EMI_Master_Kategori_Jenis x, N_EMI_Master_Sub_Kategori_Jenis y, "
			'SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis_1 z, N_EMI_Master_Sub_Kategori_Jenis_2 zz "
			'SQL = SQL & "where a.kode_perusahaan = x.kode_perusahaan  and y.kode_perusahaan = a.kode_perusahaan "
			'SQL = SQL & "and y.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.kode_perusahaan = x.kode_perusahaan "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.Kode_Sub_Kategori_Jenis = a.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and x.Kode_Perusahaan = z.Kode_Perusahaan and x.Kode_Kategori_Jenis = z.Kode_Kategori_Jenis "
			'SQL = SQL & "and y.Kode_Perusahaan = z.Kode_Perusahaan and y.Kode_Sub_Kategori_Jenis = z.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = z.Kode_Kategori_Jenis and a.Kode_Sub_Kategori_Jenis = z.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis_1 = z.Kode_Sub_Kategori_Jenis_1 "
			'SQL = SQL & "and x.Kode_Perusahaan = zz.Kode_Perusahaan and x.Kode_Kategori_Jenis = zz.Kode_Kategori_Jenis "
			'SQL = SQL & "and y.Kode_Perusahaan = zz.Kode_Perusahaan and y.Kode_Sub_Kategori_Jenis = zz.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and z.Kode_Perusahaan = zz.Kode_Perusahaan and z.Kode_Sub_Kategori_Jenis_1 = zz.Kode_Sub_Kategori_Jenis_1 "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = zz.Kode_Kategori_Jenis and a.Kode_Sub_Kategori_Jenis = zz.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis_1 = zz.Kode_Sub_Kategori_Jenis_1 and a.Kode_Sub_Kategori_Jenis_2 = zz.Kode_Sub_Kategori_Jenis_2"
			'SQL = SQL & "), null) as Sub_Kategori_Jenis_2, "

			SQL = SQL & "a.harga_jual, a.stock_minimum, a.kode_kategori, a.aktif,  a.flag_ppn, a.id_master_kategori_gudang_x,a.ID_Kategori_QC_X, a.ID_Kategori_PO, a.Id_Klasifikasi_Bahan_X,a.ID_Routing_X From barang_lain a "
			SQL = SQL & "where a.kode_perusahaan = '001' and a.jenis = 'B' "
			' SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.kode_supplier = b.kode_supplier and "
			SQL = SQL & "and  a.kode_stock_owner = '" & lokasi_pergudang & "' and "
			If ComboBox7.SelectedIndex = -1 Then
				SQL = SQL & "a.aktif = 'Y'  "
			Else
				SQL = SQL & "a.aktif = '" & ComboBox7.Text & "'  "
			End If
			'  SQL = SQL & "a.kode_pembeda in(" & list_pembeda & ") "
			If semua = "T" Then
				SQL = SQL & "and " & arrkolom.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox8.Text & "%' "
			End If

			SQL = SQL & "order by " & arrorder.Item(ComboBox8.SelectedIndex)
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					Lvw = ListView1.Items.Add(dr("kode_stock_owner"))
					Lvw.SubItems.Add(dr("kode_barang"))
					Lvw.SubItems.Add(dr("Nama"))
					Lvw.SubItems.Add(dr("satuan"))

					'iniiiii
					If boleh_lihat = True Then
						Lvw.SubItems.Add(Format(dr("good_stock"), "N0"))
					Else
						Lvw.SubItems.Add("")
					End If
					'iniiiii
					Lvw.SubItems.Add(Format(dr("harga_beli_x"), "N0"))
					Lvw.SubItems.Add(Format(dr("stock_minimum"), "N0"))
					Lvw.SubItems.Add(dr("kode_group_jenis"))

					If General_Class.CekNULL(dr("barang_sendiri")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("barang_sendiri"))
					End If

					If General_Class.CekNULL(dr("kode_kategori")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("kode_kategori"))
					End If

					If General_Class.CekNULL(dr("pakai_sn")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("pakai_sn"))
					End If

					If General_Class.CekNULL(dr("flag_ppn")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("flag_ppn"))
					End If

					If General_Class.CekNULL(dr("kode_kategori_gudang")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("kode_kategori_gudang"))
					End If

					If General_Class.CekNULL(dr("keterangan_master_kategori_gudang")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("keterangan_master_kategori_gudang"))
					End If

					If General_Class.CekNULL(dr("ID_Kategori_QC_X")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("keterangan_QC"))
					End If

					If General_Class.CekNULL(dr("ID_Kategori_QC_X")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("ID_Kategori_QC_X"))
					End If

					If General_Class.CekNULL(dr("ID_Kategori_PO")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("keterangan_PO"))
					End If

					If General_Class.CekNULL(dr("ID_Kategori_PO")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("ID_Kategori_PO"))
					End If

					If General_Class.CekNULL(dr("Id_Klasifikasi_Bahan_X")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("keterangan_bhn"))
					End If

					If General_Class.CekNULL(dr("keterangan_Bhn2")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("keterangan_Bhn2"))
					End If

					If General_Class.CekNULL(dr("ID_Routing_X")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("keterangan_Routing"))
					End If

					If General_Class.CekNULL(dr("ID_Routing_X")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("ID_Routing_X"))
					End If

					'           Lvw.SubItems.Add(Format(dr("harga_reseller"), "N0"))
					'          Lvw.SubItems.Add(Format(dr("stock_minimum"), "N0"))
					'         Lvw.SubItems.Add(dr("lemari"))
					'        Lvw.SubItems.Add(Format(dr("bad_stock"), "N0"))

					'      Lvw.SubItems.Add(dr("kode_pembeda"))
					'     Lvw.SubItems.Add(dr("aktif"))

					' Lvw.SubItems.Add(dr("kode_supplier"))
					'Lvw.SubItems.Add(dr("nama_supplier"))
					'    Lvw.SubItems.Add(dr("flag_ppn"))
					If General_Class.CekNULL(dr("kode_kategori_Jenis")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("kode_kategori_Jenis"))
					End If

					If General_Class.CekNULL(dr("kategori_Jenis")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("kategori_Jenis"))
					End If

					If General_Class.CekNULL(dr("kode_sub_kategori_Jenis")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("kode_sub_kategori_Jenis"))
					End If

					If General_Class.CekNULL(dr("sub_kategori_Jenis")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("sub_kategori_Jenis"))
					End If

					If General_Class.CekNULL(dr("kode_sub_kategori_Jenis_1")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("kode_sub_kategori_Jenis_1"))
					End If

					If General_Class.CekNULL(dr("sub_kategori_Jenis_1")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("sub_kategori_Jenis_1"))
					End If

					If General_Class.CekNULL(dr("kode_sub_kategori_Jenis_2")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("kode_sub_kategori_Jenis_2"))
					End If

					If General_Class.CekNULL(dr("sub_kategori_Jenis_2")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("sub_kategori_Jenis_2"))
					End If

					If General_Class.CekNULL(dr("kode_sub_kategori_Jenis_3")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("kode_sub_kategori_Jenis_3"))
					End If

					If General_Class.CekNULL(dr("sub_kategori_Jenis_3")) = "" Then
						Lvw.SubItems.Add("-")
					Else
						Lvw.SubItems.Add(dr("sub_kategori_Jenis_3"))
					End If
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Kosong()
		boleh_lihat_global = False

		'Try
		'    OpenConn()
		'    Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
		'    Base_Language.Get_Languages(Bahasa_Pilihan, "Barang")
		'    'Base_Language.Get_Languages(Bahasa_Pilihan, "Ongkir")
		'    CloseConn()
		'Catch ex As Exception
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try

		PictureBox1.Image = Nothing
		PictureBox1.Refresh()

		lblKodeBrng.Text = Base_Language.Lang_Global_KodeBarang
		lblNama.Text = Base_Language.Lang_Global_Nama
		lblSatuan.Text = Base_Language.Lang_Global_Satuan
		lblPenentuHarga.Text = Base_Language.Lang_Global_Penentu_Harga
		lblStockMin.Text = Base_Language.Lang_Global_Stok_Min
		lblJenis.Text = Base_Language.Lang_Global_Jenis
		lblBarangSendiri.Text = Base_Language.Lang_Global_Barang_Sendiri
		lblKategori.Text = Base_Language.Lang_Global_Kategori
		lblStatusAktif.Text = Base_Language.Lang_Global_Aktif
		lblflagppn.Text = Base_Language.Lang_Global_Flag_PPN
		lblBeratBersih.Text = "Bersih"
		lblBeratKotor.Text = "Kotor"
		lblKategoriKecil.Text = Base_Language.Lang_Global_Kategori_Kecil
		lblKategoriBesar.Text = Base_Language.Lang_Global_Kategori_Besar
		lblLebar.Text = Base_Language.Lang_Global_L
		Label22.Text = Base_Language.Lang_Barang_Msg_Berat
		Label33.Text = Base_Language.Lang_Barang_Msg_Uk
		lblPanjang.Text = Base_Language.Lang_Global_P
		lblTinggi.Text = Base_Language.Lang_Global_T
		lblUkuran.Text = Base_Language.Lang_Global_Ukuran
		lblJenisGudang.Text = Base_Language.Lang_Global_Jenis_Gudang
		Lbl_KategoriGudang.Text = Base_Language.Lang_Global_KategoriGudang
		lblKolom.Text = Base_Language.Lang_Global_Kolom
		lblStatusAktif.Text = Base_Language.Lang_Global_Aktif
		Button5.Text = Base_Language.Lang_Global_Cari

		Button1.Text = Base_Language.Lang_Global_Simpan
		Button3.Text = Base_Language.Lang_Global_Refresh

		ComboBox11.Enabled = False

		DgvSatuanTerpilih.Rows.Clear()

		arrprefix_Klasifikasi_Bahan2.Clear()
		arrId_Klasifikasi_Bahan2.Clear()

		Try
			OpenConn()

			ComboBox2.Items.Clear()

			SQL = "Select kode_stock_owner, flag_default From stock_owner where kode_perusahaan = '" & KodePerusahaan & "' order by kode_stock_owner"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					ComboBox2.Items.Add(dr("kode_stock_owner"))
					'If dr("flag_default") = "Y" Then
					'    ComboBox2.Text = dr("kode_stock_owner")
					'End If
				Loop
			End Using

			Cmb_FlagPotongStok.Items.Clear() : txtStandarPrice.Text = ""
			Cmb_FlagPotongStok.Items.Add("Y")
			Cmb_FlagPotongStok.Items.Add("T")

			ComboBox2.Text = Lokasi

			ComboBox4.Items.Clear()
			SQL = "Select kode_kategori From kategori_barang where kode_perusahaan = '" & KodePerusahaan & "' order by kode_kategori"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					ComboBox4.Items.Add(dr("kode_kategori"))
				Loop
			End Using

			ComboBox12.Items.Clear()
			SQL = "Select Kode_Kategori_Besar From Kategori_Besar where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_Kategori_Besar"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					ComboBox12.Items.Add(dr("Kode_Kategori_Besar"))
				Loop
			End Using
			'iniiiii
			'Dim boleh_lihat_global As Boolean

			cmbSatuan.Items.Clear()
			SQL = "select satuan from emi_satuan where kode_perusahaan = '" & KodePerusahaan & "' And Flag_Barang='Y' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					cmbSatuan.Items.Add(Dr("satuan"))
				Loop
			End Using

			cmbJenis.Items.Clear() : arrJenisBarang.Clear() : arrGudangRawMaterial.Clear()
			SQL = "select id_group_jenis,kode_group_jenis, Flag_Raw_Material from EMI_Group_Jenis_Lain where "
			SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "order by kode_group_jenis"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					cmbJenis.Items.Add(dr("kode_group_jenis")) : arrJenisBarang.Add(dr("id_group_jenis"))

					If dr("Flag_Raw_Material") = "Y" Then
						arrGudangRawMaterial.Add(dr("id_group_jenis"))
					End If
				Loop
			End Using

			Cmb_Kd_Aktiva.Items.Clear() : arrKodeAktiva.Clear()
			SQL = "select ID_Aktiva, Kode_Aktiva from N_EMI_Master_Aktiva_Lain "
			SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "'  "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Kd_Aktiva.Items.Add(Dr("Kode_Aktiva")) : arrKodeAktiva.Add(CInt(Dr("ID_Aktiva")))
				Loop
			End Using

			CmbJnsGudang.Items.Clear() : arrJenisGudang.Clear()
			SQL = "select id_kategori_gudang, Kode_Kategori_Gudang from emi_kategori_gudang_barang_lain where "
			SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "order by Kode_Kategori_Gudang"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbJnsGudang.Items.Add(dr("Kode_Kategori_Gudang")) : arrJenisGudang.Add(dr("id_kategori_gudang"))
				Loop
			End Using

			CmbKatJenis.Items.Clear() : arrKategoriJenis.Clear() : arrPrefix_Kategori.Clear()
			CmbKatJenisSub.Items.Clear() : arrSubKategoriJenis.Clear() : arrPrefix_Sub_Kategori.Clear()
			CmbKatJenisSub1.Items.Clear() : arrSubKategoriJenis1.Clear() : arrPrefix_Sub_Kategori1.Clear()
			CmbKatJenisSub2.Items.Clear() : arrSubKategoriJenis2.Clear() : arrPrefix_Sub_Kategori2.Clear()
			CmbKatJenisSub3.Items.Clear() : arrSubKategoriJenis3.Clear() : arrPrefix_Sub_Kategori3.Clear()
			SQL = "select id_kategori_jenis, Keterangan, Prefix from N_EMI_Master_Kategori_Jenis where Kode_Perusahaan = '" & KodePerusahaan & "' order by Keterangan "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbKatJenis.Items.Add(dr("Keterangan"))
					arrKategoriJenis.Add(dr("id_kategori_jenis"))
					arrPrefix_Kategori.Add(dr("Prefix"))
				Loop
			End Using
			CmbKatJenis.SelectedIndex = -1 : CmbKatJenisSub.SelectedIndex = -1 : CmbKatJenisSub1.SelectedIndex = -1 : CmbKatJenisSub2.SelectedIndex = -1
			CmbKatJenis.Enabled = True
			CmbKatJenisSub.Enabled = True
			CmbKatJenisSub1.Enabled = True
			CmbKatJenisSub2.Enabled = True
			CmbKatJenisSub3.Enabled = True

			SQL = "select flag_hide_stock, "
			SQL = SQL & "ISNULL(("
			SQL = SQL & "select top(1) 'Y' from role_button a where a.kode_perusahaan = x.kode_perusahaan and "
			SQL = SQL & "a.userid = '" & UserID & "' and buttonname = 'LIHAT_STOCK'"
			SQL = SQL & "), 'T') AS boleh_lihat_stock "
			SQL = SQL & " from stock_owner x where x.kode_perusahaan = '" & KodePerusahaan & "' and "
			SQL = SQL & "x.kode_stock_owner = '" & ComboBox2.Text & "'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If Dr("flag_hide_stock") = "Y" Then
						If Dr("boleh_lihat_stock") = "Y" Then
							boleh_lihat_global = True
						Else
							boleh_lihat_global = False
						End If
					Else
						boleh_lihat_global = True
					End If
				Else
					boleh_lihat_global = False
				End If
			End Using

			ComboBox5.Items.Clear() : arrId_kategori_qc.Clear()
			SQL = "select ID_Kategori_QC,Keterangan from Emi_Kategori_QC where kode_perusahaan = '" & KodePerusahaan & "' order by Keterangan"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					ComboBox5.Items.Add(Dr("Keterangan"))
					arrId_kategori_qc.Add(Dr("ID_Kategori_QC"))
				Loop
			End Using

			ComboBox6.Items.Clear() : arrId_kategori_PO.Clear()
			SQL = "select ID_Kategori_PO,Keterangan from Emi_Kategori_PO where kode_perusahaan = '" & KodePerusahaan & "' order by Keterangan"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					ComboBox6.Items.Add(Dr("Keterangan"))
					arrId_kategori_PO.Add(Dr("ID_Kategori_PO"))
				Loop
			End Using

			ComboBox11.Items.Clear() : arrId_Klasifikasi_Bahan.Clear() : arrprefix_Klasifikasi_Bahan.Clear()
			SQL = "select ID_Klasifikasi_Bahan,Keterangan, Prefix_Klasifikasi_Bahan from Emi_Klasifikasi_Bahan where kode_perusahaan = '" & KodePerusahaan & "' order by Keterangan"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					ComboBox11.Items.Add(Dr("Keterangan"))
					arrId_Klasifikasi_Bahan.Add(Dr("ID_Klasifikasi_Bahan"))
					arrprefix_Klasifikasi_Bahan.Add(Dr("Prefix_Klasifikasi_Bahan"))
				Loop
			End Using

			ComboBox19.Items.Clear()
			ComboBox19.SelectedIndex = -1
			arrId_Klasifikasi_Bahan2.Clear() : arrprefix_Klasifikasi_Bahan2.Clear()

			'ComboBox5.Items.Clear()
			'ComboBox5.Items.Add("MK")
			'ComboBox5.SelectedIndex = 0
			'SQL = "Select kode_pembeda From pembeda where kode_perusahaan = '" & KodePerusahaan & "' order by kode_pembeda"
			'Using dr = OpenTrans(SQL)
			'    Do While dr.Read
			'        ComboBox5.Items.Add(dr("kode_pembeda"))
			'    Loop
			'End Using

			ComboBox14.Items.Clear() : arrid_Routing.Clear() : ComboBox14.SelectedIndex = -1
			SQL = "select Id_Routing,Keterangan from EMI_Master_Routing where Kode_Perusahaan = '" & KodePerusahaan & "' order by Keterangan "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					ComboBox14.Items.Add(Dr("Keterangan"))
					arrid_Routing.Add(Dr("Id_Routing"))
				Loop
			End Using

			ComboBox17.Items.Clear()
			SQL = "select Satuan_Berat_default from Init"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					ComboBox17.Items.Add(Dr("Satuan_Berat_default"))
				Loop
				ComboBox17.SelectedIndex = 0
			End Using

			ComboBox18.Items.Clear()
			'SQL = "select Satuan_Timbang from Init"
			'Using Dr = OpenTrans(SQL)
			'    Do While Dr.Read
			'        ComboBox18.Items.Add(Dr("Satuan_Timbang"))
			'    Loop
			'    ComboBox18.SelectedIndex = 0
			'End Using

			ComboBox15.Items.Clear()
			ComboBox15.Items.Add("Original Bags")
			ComboBox15.Items.Add("Non Original Bags")

			ComboBox16.Items.Clear()
			ComboBox16.Items.Add("FIFO")
			ComboBox16.Items.Add("FEFO")

			'DataGridView1.Rows.Clear()
			'SQL = "select a.No_Faktur,a.Kode_Stock_Owner, a.Kode_Barang, a.Nama_Barang, a.Jumlah, a.Jmlh_PR, a.Satuan, b.Keterangan as Cost_Center, a.No_Urut, d.Lokasi, "
			'SQL = SQL & "isnull(( select c.Keterangan from N_EMI_Master_Gedung_Barang_Lain c where "
			'SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.ID_Gedung = c.ID_Gedung ), NULL) as Gedung "
			'SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a, EMI_Master_Cost_Center b, N_EMI_Purchase_Requisition_Barang_Lain_Departement d "
			'SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Cost_Center = b.Id_Cost_Center "
			'SQL = SQL & "and a.Flag_Sudah_PR is null and a.Kode_Barang = '-' "
			'SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = d.No_Faktur and d.Flag_Release = 'Y' and d.Flag_PR is null and a.Flag_Ajukan = 'Y' "
			'SQL = SQL & "order by a.No_Faktur "
			'Using Ds = BindingTrans(SQL)
			'    With Ds.Tables("MyTable")
			'        If .Rows.Count <> 0 Then
			'            For i As Integer = 0 To .Rows.Count - 1
			'                DataGridView1.Rows.Add(1)
			'                DataGridView1.Rows.Item(i).Cells(0).Value = .Rows(i).Item("No_Faktur")
			'                DataGridView1.Rows.Item(i).Cells(1).Value = .Rows(i).Item("Kode_Stock_Owner")
			'                DataGridView1.Rows.Item(i).Cells(2).Value = .Rows(i).Item("Kode_Barang")
			'                DataGridView1.Rows.Item(i).Cells(3).Value = .Rows(i).Item("Nama_Barang")
			'                DataGridView1.Rows.Item(i).Cells(4).Value = (Format(.Rows(i).Item("jumlah") - .Rows(i).Item("Jmlh_PR"), "N2"))
			'                DataGridView1.Rows.Item(i).Cells(5).Value = .Rows(i).Item("Satuan")
			'                DataGridView1.Rows.Item(i).Cells(6).Value = .Rows(i).Item("Cost_Center")
			'                DataGridView1.Rows.Item(i).Cells(7).Value = .Rows(i).Item("Gedung")
			'                DataGridView1.Rows.Item(i).Cells(8).Value = .Rows(i).Item("No_Urut")
			'                DataGridView1.Rows.Item(i).Cells(9).Value = .Rows(i).Item("Lokasi")

			'                If .Rows(i).Item("Kode_Barang") = "-" Then
			'                    DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightSteelBlue
			'                End If
			'            Next
			'            'Else
			'            '    CloseConn()
			'            '    MessageBox.Show("Data tidak ditemuakan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'            '    Exit Sub
			'        End If
			'    End With
			'End Using

			DateTimePicker1.Value = Now
			DateTimePicker2.Value = Now

			CmbSatuan_Kolom.Items.Clear() : arrcari.Clear()
			CmbSatuan_Kolom.Items.Add("No Faktur") : arrcari.Add("a.No_Faktur")
			' CmbSatuan_Kolom.Items.Add("Kode Barang") : arrcari.Add("a.Kode_Barang")
			CmbSatuan_Kolom.Items.Add("Nama Barang") : arrcari.Add("a.Nama_Barang")
			CmbSatuan_Kolom.Items.Add("Kategori Jenis") : arrcari.Add("a.Kategori_Jenis")
			CmbSatuan_Kolom.Items.Add("Sub Kategori Jenis") : arrcari.Add("a.Sub_Kategori_Jenis")
			CmbSatuan_Kolom.Items.Add("Sub Kategori Jenis 1") : arrcari.Add("a.Sub_Kategori_Jenis_1")
			CmbSatuan_Kolom.Items.Add("Sub Kategori Jenis 2") : arrcari.Add("a.Sub_Kategori_Jenis_2")
			CmbSatuan_Kolom.Items.Add("Sub Kategori Jenis 3") : arrcari.Add("a.Sub_Kategori_Jenis_3")

			'CmbSatuan_Kolom.Items.Add("Cost Center") : arrcari.Add("b.Keterangan")
			'CmbSatuan_Kolom.Items.Add("Gedung") : arrcari.Add("c.Keterangan")

			CmbSatuan_Kolom.SelectedIndex = -1
			TxtSatuan_Value.Text = ""
			xurut_departement = ""
			xNoFakturPengajuanBrgBru = ""
			xFrom = ""
			zNo_Pengajuan_Barang_Baru = ""

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'====================================================

		If My.Settings.Punya = "MS" Then
			hrg_jual_apa = "harga_jual"
			text_hrg_jual_apa = "Harga Jual Std"
		ElseIf My.Settings.Punya = "MK" Then
			hrg_jual_apa = "harga_jual_agen"
			text_hrg_jual_apa = "Harga Jual Std"
		Else
			hrg_jual_apa = "xzxzxz"
			text_hrg_jual_apa = ""
		End If

		ComboBox1.Items.Clear() : arrkolom.Clear()
		ComboBox1.Items.Add("Kode Barang") : arrkolom.Add("a.Kode_Barang")
		ComboBox1.Items.Add("Nama") : arrkolom.Add("a.Nama")
		ComboBox1.Items.Add("Satuan") : arrkolom.Add("a.Satuan")
		'ComboBox1.Items.Add("Harga Beli") : arrkolom.Add("a.harga_beli_x")
		'ComboBox1.Items.Add(text_hrg_jual_apa) : arrkolom.Add(hrg_jual_apa)
		'ComboBox1.Items.Add("Good Stock") : arrkolom.Add("a.Good_Stock")
		ComboBox1.Items.Add("Stock Min.") : arrkolom.Add("a.Stock_Minimum")
		ComboBox1.Items.Add("Kode Kategori") : arrkolom.Add("a.Kode_Kategori")
		'ComboBox1.Items.Add("Kode Supplier") : arrkolom.Add("a.Kode_Supplier")
		'ComboBox1.Items.Add("Nama Supplier") : arrkolom.Add("b.nama")
		'ComboBox1.Items.Add("Barang Sendiri") : arrkolom.Add("a.flag_sendiri_x")
		ComboBox1.SelectedIndex = 0

		ComboBox8.Items.Clear() : arrorder.Clear()
		ComboBox8.Items.Add("Kode Barang") : arrorder.Add("Kode_Barang")
		ComboBox8.Items.Add("Nama") : arrorder.Add("Nama")
		ComboBox8.Items.Add("Kode Kategori") : arrorder.Add("Kode_Kategori")
		ComboBox8.SelectedIndex = 0

		TxtKdBarang.Text = "" : TxtNama.Text = "" : cmbSatuan.SelectedIndex = -1
		TextBox5.Text = "" : TextBox5.Text = ""
		TextBox6.Text = ""
		TextBox7.Text = "" : ComboBox3.SelectedIndex = 0
		'ComboBox5.SelectedIndex = -1
		TextBox12.Text = "" : TextBox15.Text = ""
		ComboBox12.SelectedIndex = -1 : ComboBox13.SelectedIndex = -1

		TextBox16.Text = "" : TextBox17.Text = "" : TextBox18.Text = ""

		'ComboBox11.Items.Clear()
		'ComboBox11.Items.Add("Y")
		'ComboBox11.Items.Add("T")

		'ComboBox6.Items.Clear()
		'ComboBox6.Items.Add("Y") : ComboBox6.Items.Add("T")
		'ComboBox6.SelectedIndex = 0

		ComboBox7.Items.Clear()
		ComboBox7.Items.Add("Y") : ComboBox7.Items.Add("T")
		ComboBox7.SelectedIndex = 0

		ComboBox9.Items.Clear()
		ComboBox9.Items.Add("Y") : ComboBox9.Items.Add("T")
		ComboBox9.SelectedIndex = 0

		ComboBox10.Items.Clear()
		ComboBox10.Items.Add("Y") : ComboBox10.Items.Add("T")

		Cmb_FlagGrouping.Items.Clear()
		Cmb_FlagGrouping.Items.Add("Y") : Cmb_FlagGrouping.Items.Add("T")
		Cmb_FlagGrouping.SelectedIndex = 0

		ComboBox15.SelectedIndex = -1
		ComboBox16.SelectedIndex = -1
		TextBox3.Text = ""
		TextBox4.Text = ""

		TextBox10.Text = ""
		TextBox11.Text = ""
		ListView10.Visible = False

		Txtket.Text = ""
		xdari = ""

		Button1.Text = "&Simpan" 'Button2.Enabled = False
		Button1.Enabled = True

		lvwSatuan.Visible = False

		Cmb_Kategori.Items.Clear() : Cmb_Kelompok.Items.Clear()
		' Cmb_Kd_Aktiva.Items.Clear()
		Cmb_Kategori.Enabled = False : Cmb_Kelompok.Enabled = False : Cmb_Kd_Aktiva.Enabled = False

		cmbSatuan.Enabled = True
		CmbKatJenis.Enabled = True
		CmbKatJenisSub.Enabled = True
		CmbKatJenisSub1.Enabled = True
		CmbKatJenisSub2.Enabled = True
		CmbKatJenisSub3.Enabled = True

		'cmbSatuan.Enabled = False
		'ComboBox16.Enabled = False
		'TextBox7.Enabled = False
		'TextBox12.Enabled = False
		'TextBox15.Enabled = False
		'TextBox16.Enabled = False
		'TextBox17.Enabled = False
		'TextBox18.Enabled = False

		Get_Kategori_Gudang()
	End Sub

	Private Sub Get_Kategori_Gudang()

		Try
			OpenConn()
			Cmb_KategoriGudang.Items.Clear() : arrKategoriGudang.Clear()
			SQL = "Select * From "
			SQL = SQL & "emi_master_kategori_gudang "
			SQL = SQL & "order by id_master_kategori_gudang"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					Cmb_KategoriGudang.Items.Add(dr("keterangan")) : arrKategoriGudang.Add(dr("id_master_kategori_gudang"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Perusahaan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Try
			OpenConn()

			Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
			Base_Language.Get_Languages(Bahasa_Pilihan, "Barang")
			'Base_Language.Get_Languages(Bahasa_Pilihan, "Ongkir")

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong()

		ListView1.Columns.Add(Base_Language.Lang_Global_Kode_SO, 0, HorizontalAlignment.Center)
		ListView1.Columns.Add(Base_Language.Lang_Global_KodeBarang, 100, HorizontalAlignment.Center)
		ListView1.Columns.Add(Base_Language.Lang_Global_NamaBarang, 320, HorizontalAlignment.Left)
		ListView1.Columns.Add(Base_Language.Lang_Global_Satuan, 70, HorizontalAlignment.Left)
		'iniiii
		If boleh_lihat_global = True Then
			ListView1.Columns.Add("Good Stock", 0, HorizontalAlignment.Right)
		Else
			ListView1.Columns.Add("Good Stock", 0, HorizontalAlignment.Right)
		End If
		'iniiii
		ListView1.Columns.Add(Base_Language.Lang_Global_Harga_Beli, 0, HorizontalAlignment.Right)
		'ListView1.Columns.Add(text_hrg_jual_apa, 90, HorizontalAlignment.Right)
		'ListView1.Columns.Add("Harga Jual Reseller", 0, HorizontalAlignment.Right)
		ListView1.Columns.Add("Stock Min.", 85, HorizontalAlignment.Right)
		'ListView1.Columns.Add("Lemari", 70, HorizontalAlignment.Left)
		'ListView1.Columns.Add("Bad Stock", 0, HorizontalAlignment.Right)

		'ListView1.Columns.Add("Pembeda", 100, HorizontalAlignment.Left)

		'ListView1.Columns.Add("Pakai Serial Number", 0, HorizontalAlignment.Center)
		'ListView1.Columns.Add("Kode Supplier", 120, HorizontalAlignment.Left)
		'ListView1.Columns.Add("Supplier", 200, HorizontalAlignment.Left)
		'ListView1.Columns.Add("Flag PPN", 60, HorizontalAlignment.Left)
		'ListView1.Columns.Add("Harga Jual Min", 90, HorizontalAlignment.Right).DisplayIndex = 6
		'ListView1.Columns.Add("Harga Jual Max", 90, HorizontalAlignment.Right).DisplayIndex = 8
		'ListView1.Columns.Add("Harga Jual Special", 90, HorizontalAlignment.Right).DisplayIndex = 9
		ListView1.Columns.Add(Base_Language.Lang_Global_Jenis, 120, HorizontalAlignment.Center)
		ListView1.Columns.Add(Base_Language.Lang_Global_Barang_Sendiri, 0, HorizontalAlignment.Left)
		ListView1.Columns.Add(Base_Language.Lang_Global_Kategori, 100, HorizontalAlignment.Left)
		ListView1.Columns.Add("Pakai SN", 75, HorizontalAlignment.Center)
		ListView1.Columns.Add(Base_Language.Lang_Global_Flag_PPN, 75, HorizontalAlignment.Left)
		ListView1.Columns.Add(Base_Language.Lang_Global_Jenis_Gudang, 0, HorizontalAlignment.Center)
		ListView1.Columns.Add("Kategori Gudang", 120, HorizontalAlignment.Center)
		ListView1.Columns.Add("Kategori QC", 0, HorizontalAlignment.Center)
		ListView1.Columns.Add("Id Kategori QC", 0, HorizontalAlignment.Center)
		ListView1.Columns.Add("Kategori PO", 100, HorizontalAlignment.Center)
		ListView1.Columns.Add("Id Kategori PO", 0, HorizontalAlignment.Center)
		ListView1.Columns.Add("Klasifikasi Bahan", 0, HorizontalAlignment.Center)
		ListView1.Columns.Add("Klasifikasi Bahan 2", 0, HorizontalAlignment.Center)
		'ListView1.Columns.Add("Id Kategori Bahan", 0, HorizontalAlignment.Center)
		ListView1.Columns.Add("Jenis Routing", 0, HorizontalAlignment.Center)
		ListView1.Columns.Add("Id Routing", 0, HorizontalAlignment.Center)
		ListView1.Columns.Add("kode kategori jenis", 0, HorizontalAlignment.Center)
		ListView1.Columns.Add("Kategori Jenis", 100, HorizontalAlignment.Center)
		ListView1.Columns.Add("kode sub kategori Jenis", 0, HorizontalAlignment.Center)
		ListView1.Columns.Add("sub Kategori Jenis", 100, HorizontalAlignment.Center)
		ListView1.Columns.Add("kode sub kategori Jenis 1", 0, HorizontalAlignment.Center)
		ListView1.Columns.Add("sub Kategori Jenis 1", 100, HorizontalAlignment.Center)
		ListView1.Columns.Add("kode sub kategori Jenis 2", 0, HorizontalAlignment.Center)
		ListView1.Columns.Add("sub Kategori Jenis 2", 100, HorizontalAlignment.Center)
		ListView1.Columns.Add("kode sub kategori Jenis 3", 0, HorizontalAlignment.Center)
		ListView1.Columns.Add("sub Kategori Jenis 3", 100, HorizontalAlignment.Center)

		ListView1.View = View.Details

		lvwSatuan.Items.Clear()
		lvwSatuan.Columns.Add(Base_Language.Lang_Barang_Flag_Tampil_Display, 150, HorizontalAlignment.Center)
		lvwSatuan.Columns.Add(Base_Language.Lang_Global_Satuan, 150, HorizontalAlignment.Left)

		lvwSatuan.View = View.Details

		ListView10.Columns.Add("Kode Supplier", 150, HorizontalAlignment.Left)
		ListView10.Columns.Add("Nama Supplier", 288, HorizontalAlignment.Left)
		ListView10.View = View.Details

		ListView10.Location = New Point(901, 88) 'Point(161, 194)

		Cari("Y")
		Cari2("Y")
		cmbJenis.Focus()

	End Sub

	Private Sub Get_Lv_SatuanTurunan()
		If DgvSatuanTerpilih.Rows.Count = 0 Then Exit Sub

		arrSatuanTurunan.Clear()
		For i As Integer = 0 To DgvSatuanTerpilih.RowCount - 1
			arrSatuanTurunan.Add(DgvSatuanTerpilih.Rows(i).Cells(0).Value)
		Next

	End Sub

	Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
		Me.Close()
	End Sub

	Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
		Kosong()
		TextBox8.Text = ""
		cmbJenis.Focus()
	End Sub

	Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtKdBarang.KeyPress
		If e.KeyChar = Chr(13) Then TxtNama.Focus()
	End Sub

	Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtNama.KeyPress
		If e.KeyChar = Chr(13) Then cmbSatuan.Focus()
	End Sub

	Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
		If e.KeyChar = Chr(13) Then TextBox5.Focus()
	End Sub

	Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
		If e.KeyChar = Chr(13) Then TextBox5.Focus()
	End Sub

	Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
		If e.KeyChar = Chr(13) Then TextBox7.Focus()
		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
	End Sub

	'Private Sub TextBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox7.KeyPress
	'    If e.KeyChar = Chr(13) Then TextBox9.Focus()
	'    If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
	'End Sub

	Private Sub TextBox8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox8.KeyPress
		If e.KeyChar = Chr(13) Then Button5_Click(TextBox8, e)
	End Sub

	Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
		If e.KeyChar = Chr(13) Then cmbSatuan.Focus()
	End Sub

	Private Sub TextBox1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtKdBarang.Leave
		If ComboBox2.Text.Trim.Length = 0 Then Exit Sub
		If TxtKdBarang.Text.Trim.Length = 0 Then Exit Sub

		Dim boleh_edit_hj As String = ""

		Try
			OpenConn()

			If CekButtonRole("edit_harga_jual") = "T" Then
				boleh_edit_hj = "T"
			Else
				boleh_edit_hj = "Y"
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()

			Dim lokasi_pergudang As String = ""

			SQL = " select  b.Kode_Stock_Owner_Gudang from stock_owner a , Binding_Lokasi_Gudang_Lain b where "
			SQL = SQL & "  a.Kode_Stock_Owner = '" & ComboBox2.Text & "'    and b.Gudang_Default = 'Y'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					lokasi_pergudang = Dr("Kode_Stock_Owner_Gudang")
				End If
			End Using

			lvwSatuan.Items.Clear() : cmbSatuan.SelectedIndex = -1
			CmbKatJenis.SelectedIndex = -1
			CmbKatJenisSub.SelectedIndex = -1
			CmbKatJenisSub1.SelectedIndex = -1
			CmbKatJenisSub2.SelectedIndex = -1

			SQL = "Select b.kode_group_jenis, "
			SQL = SQL & "a.kode_barang, a.satuan, a.flag_potong_stok_X, isnull(a.standar_price_X,0) as standar_price_X, a.Nama, a.harga_beli_x, a.last_hpp_X, a.stock_minimum, a.kode_kategori, "
			SQL = SQL & "a.flag_ppn, a.flag_sendiri_x, a.berat, a.berat_kotor, a.Panjang, a.Lebar, a.Tinggi, a.Kode_Kategori_Besar_X, a.Kode_Kategori_Kecil_X, "
			SQL = SQL & "a.id_group_jenis, a.id_master_kategori_gudang_x, a.Jenis_Kemasan_X, a.Metode_Pengeluaran_Stok, a.Berat_Bags_X, a.Isi_Per_Bags_X, a.Id_Kategori_Gudang, "

			SQL = SQL & "isnull((select kode_kategori_gudang from emi_kategori_gudang_barang_lain x where a.kode_perusahaan = x.kode_perusahaan "
			SQL = SQL & "and a.Id_Kategori_Gudang = x.id_kategori_gudang),NULL) as kode_kategori_gudang, "
			SQL = SQL & "isnull((select keterangan from emi_master_kategori_gudang x where a.kode_perusahaan = x.kode_perusahaan "
			SQL = SQL & "and a.id_master_kategori_gudang_x = x.Id_Master_Kategori_Gudang),NULL) as keterangan_master_kategori_gudang,"
			SQL = SQL & "isnull((select keterangan from Emi_Kategori_QC x where a.kode_perusahaan = x.kode_perusahaan "
			SQL = SQL & "and a.ID_Kategori_QC_X = x.ID_Kategori_QC),NULL) as Keterangan,"
			SQL = SQL & "isnull((select keterangan from Emi_Kategori_PO x where a.kode_perusahaan = x.kode_perusahaan "
			SQL = SQL & "and a.ID_Kategori_PO = x.ID_Kategori_PO),NULL) as KeteranganPO,"

			SQL = SQL & "isnull((select keterangan from Emi_Klasifikasi_Bahan x where a.kode_perusahaan = x.kode_perusahaan "
			SQL = SQL & "and a.Id_Klasifikasi_Bahan_X = x.ID_Klasifikasi_Bahan),NULL) as KeteranganBhn, "
			SQL = SQL & "isnull((select keterangan from emi_klasifikasi_bahan2 x where a.kode_perusahaan = x.kode_perusahaan "
			SQL = SQL & "and a.Id_Klasifikasi_Bahan2_X = x.ID_Klasifikasi_Bahan2), null) as keterangan_Bhn2 , "

			SQL = SQL & "isnull((select keterangan from EMI_Master_Routing x where a.kode_perusahaan = x.kode_perusahaan "
			SQL = SQL & "and a.ID_Routing_X = x.ID_Routing),NULL) as Keterangan_Routing, a.keterangan as Ket_Barang, "

			'SQL = SQL & "isnull((select Kode_Kategori_Jenis from N_EMI_Master_Kategori_Jenis x where a.kode_perusahaan = x.kode_perusahaan "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis), null) as kode_kategori_Jenis , "

			'SQL = SQL & "isnull((select Keterangan from N_EMI_Master_Kategori_Jenis x where a.kode_perusahaan = x.kode_perusahaan "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis), null) as kategori_Jenis , "

			SQL = SQL & " isnull((select x.Kode_Kategori_Jenis from view_kategori_turunan x where x.kode_perusahaan = a.kode_perusahaan and  x.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3), null) as Kode_Kategori_Jenis,"

			SQL = SQL & " isnull((select x.Kategori_Jenis from view_kategori_turunan x where x.kode_perusahaan = a.kode_perusahaan and  x.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3), null) as Kategori_Jenis,"

			SQL = SQL & " isnull((select x.Kode_Sub_Kategori_Jenis from view_kategori_turunan x where x.kode_perusahaan = a.kode_perusahaan and  x.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3), null) as Kode_Sub_Kategori_Jenis,"

			SQL = SQL & " isnull((select x.Sub_Kategori_Jenis from view_kategori_turunan x where x.kode_perusahaan = a.kode_perusahaan and  x.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3), null) as Sub_Kategori_Jenis,"

			SQL = SQL & " isnull((select x.Kode_Sub_Kategori_Jenis_1 from view_kategori_turunan x where x.kode_perusahaan = a.kode_perusahaan and  x.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3), null) as Kode_Sub_Kategori_Jenis_1,"

			SQL = SQL & " isnull((select x.Sub_Kategori_Jenis_1 from view_kategori_turunan x where x.kode_perusahaan = a.kode_perusahaan and  x.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3), null) as Sub_Kategori_Jenis_1,"

			SQL = SQL & " isnull((select x.Kode_Sub_Kategori_Jenis_2 from view_kategori_turunan x where x.kode_perusahaan = a.kode_perusahaan and  x.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3), null) as Kode_Sub_Kategori_Jenis_2,"

			SQL = SQL & " isnull((select x.Sub_Kategori_Jenis_2 from view_kategori_turunan x where x.kode_perusahaan = a.kode_perusahaan and  x.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3), null) as Sub_Kategori_Jenis_2,"

			SQL = SQL & " isnull((select x.Kode_Sub_Kategori_Jenis_3 from view_kategori_turunan x where x.kode_perusahaan = a.kode_perusahaan and  x.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3), null) as Kode_Sub_Kategori_Jenis_3,"

			SQL = SQL & " isnull((select x.Sub_Kategori_Jenis_3 from view_kategori_turunan x where x.kode_perusahaan = a.kode_perusahaan and  x.Id_Sub_Kategori_Jenis_3 = a.Id_Sub_Kategori_Jenis_3), null) as Sub_Kategori_Jenis_3,"
			'SQL = SQL & "isnull((select y.Kode_Sub_Kategori_Jenis from N_EMI_Master_Kategori_Jenis x, N_EMI_Master_Sub_Kategori_Jenis y "
			'SQL = SQL & "where a.kode_perusahaan = x.kode_perusahaan and y.kode_perusahaan = x.kode_perusahaan "
			'SQL = SQL & "and y.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.kode_perusahaan = a.kode_perusahaan "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.Kode_Sub_Kategori_Jenis = a.Kode_Sub_Kategori_Jenis), null) "
			'SQL = SQL & "as Kode_Sub_Kategori_Jenis, "

			'SQL = SQL & "isnull((select y.Keterangan from N_EMI_Master_Kategori_Jenis x, N_EMI_Master_Sub_Kategori_Jenis y "
			'SQL = SQL & "where a.kode_perusahaan = x.kode_perusahaan and y.kode_perusahaan = x.kode_perusahaan "
			'SQL = SQL & "and y.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.kode_perusahaan = a.kode_perusahaan "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.Kode_Sub_Kategori_Jenis = a.Kode_Sub_Kategori_Jenis), null) "
			'SQL = SQL & "as Sub_Kategori_Jenis, "

			'SQL = SQL & "isnull((select z.Kode_Sub_Kategori_Jenis_1 from N_EMI_Master_Kategori_Jenis x, N_EMI_Master_Sub_Kategori_Jenis y, "
			'SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis_1 z "
			'SQL = SQL & "where a.kode_perusahaan = x.kode_perusahaan  and y.kode_perusahaan = a.kode_perusahaan "
			'SQL = SQL & "and y.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.kode_perusahaan = x.kode_perusahaan "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.Kode_Sub_Kategori_Jenis = a.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and x.Kode_Perusahaan = z.Kode_Perusahaan and x.Kode_Kategori_Jenis = z.Kode_Kategori_Jenis "
			'SQL = SQL & "and y.Kode_Perusahaan = z.Kode_Perusahaan and y.Kode_Sub_Kategori_Jenis = z.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = z.Kode_Kategori_Jenis and a.Kode_Sub_Kategori_Jenis = z.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis_1 = z.Kode_Sub_Kategori_Jenis_1), null) "
			'SQL = SQL & "as Kode_Sub_Kategori_Jenis_1, "

			'SQL = SQL & "isnull((select z.Keterangan from N_EMI_Master_Kategori_Jenis x, N_EMI_Master_Sub_Kategori_Jenis y, "
			'SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis_1 z "
			'SQL = SQL & "where a.kode_perusahaan = x.kode_perusahaan  and y.kode_perusahaan = a.kode_perusahaan "
			'SQL = SQL & "and y.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.kode_perusahaan = x.kode_perusahaan "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.Kode_Sub_Kategori_Jenis = a.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and x.Kode_Perusahaan = z.Kode_Perusahaan and x.Kode_Kategori_Jenis = z.Kode_Kategori_Jenis "
			'SQL = SQL & "and y.Kode_Perusahaan = z.Kode_Perusahaan and y.Kode_Sub_Kategori_Jenis = z.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = z.Kode_Kategori_Jenis and a.Kode_Sub_Kategori_Jenis = z.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis_1 = z.Kode_Sub_Kategori_Jenis_1), null) "
			'SQL = SQL & "as Sub_Kategori_Jenis_1, "

			'SQL = SQL & "isnull((select zz.Kode_Sub_Kategori_Jenis_2 from N_EMI_Master_Kategori_Jenis x, N_EMI_Master_Sub_Kategori_Jenis y, "
			'SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis_1 z, N_EMI_Master_Sub_Kategori_Jenis_2 zz "
			'SQL = SQL & "where a.kode_perusahaan = x.kode_perusahaan  and y.kode_perusahaan = a.kode_perusahaan "
			'SQL = SQL & "and y.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.kode_perusahaan = x.kode_perusahaan "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.Kode_Sub_Kategori_Jenis = a.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and x.Kode_Perusahaan = z.Kode_Perusahaan and x.Kode_Kategori_Jenis = z.Kode_Kategori_Jenis "
			'SQL = SQL & "and y.Kode_Perusahaan = z.Kode_Perusahaan and y.Kode_Sub_Kategori_Jenis = z.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = z.Kode_Kategori_Jenis and a.Kode_Sub_Kategori_Jenis = z.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis_1 = z.Kode_Sub_Kategori_Jenis_1 "
			'SQL = SQL & "and x.Kode_Perusahaan = zz.Kode_Perusahaan and x.Kode_Kategori_Jenis = zz.Kode_Kategori_Jenis "
			'SQL = SQL & "and y.Kode_Perusahaan = zz.Kode_Perusahaan and y.Kode_Sub_Kategori_Jenis = zz.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and z.Kode_Perusahaan = zz.Kode_Perusahaan and z.Kode_Sub_Kategori_Jenis_1 = zz.Kode_Sub_Kategori_Jenis_1 "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = zz.Kode_Kategori_Jenis and a.Kode_Sub_Kategori_Jenis = zz.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis_1 = zz.Kode_Sub_Kategori_Jenis_1 and a.Kode_Sub_Kategori_Jenis_2 = zz.Kode_Sub_Kategori_Jenis_2"
			'SQL = SQL & "), null) as Kode_Sub_Kategori_Jenis_2, "

			'SQL = SQL & "isnull((select zz.Keterangan from N_EMI_Master_Kategori_Jenis x, N_EMI_Master_Sub_Kategori_Jenis y, "
			'SQL = SQL & "N_EMI_Master_Sub_Kategori_Jenis_1 z, N_EMI_Master_Sub_Kategori_Jenis_2 zz "
			'SQL = SQL & "where a.kode_perusahaan = x.kode_perusahaan  and y.kode_perusahaan = a.kode_perusahaan "
			'SQL = SQL & "and y.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.kode_perusahaan = x.kode_perusahaan "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = x.Kode_Kategori_Jenis and y.Kode_Sub_Kategori_Jenis = a.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and x.Kode_Perusahaan = z.Kode_Perusahaan and x.Kode_Kategori_Jenis = z.Kode_Kategori_Jenis "
			'SQL = SQL & "and y.Kode_Perusahaan = z.Kode_Perusahaan and y.Kode_Sub_Kategori_Jenis = z.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = z.Kode_Kategori_Jenis and a.Kode_Sub_Kategori_Jenis = z.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis_1 = z.Kode_Sub_Kategori_Jenis_1 "
			'SQL = SQL & "and x.Kode_Perusahaan = zz.Kode_Perusahaan and x.Kode_Kategori_Jenis = zz.Kode_Kategori_Jenis "
			'SQL = SQL & "and y.Kode_Perusahaan = zz.Kode_Perusahaan and y.Kode_Sub_Kategori_Jenis = zz.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and z.Kode_Perusahaan = zz.Kode_Perusahaan and z.Kode_Sub_Kategori_Jenis_1 = zz.Kode_Sub_Kategori_Jenis_1 "
			'SQL = SQL & "and a.Kode_Kategori_Jenis = zz.Kode_Kategori_Jenis and a.Kode_Sub_Kategori_Jenis = zz.Kode_Sub_Kategori_Jenis "
			'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis_1 = zz.Kode_Sub_Kategori_Jenis_1 and a.Kode_Sub_Kategori_Jenis_2 = zz.Kode_Sub_Kategori_Jenis_2"
			'SQL = SQL & "), null) as Sub_Kategori_Jenis_2, "

			SQL = SQL & "a.Flag_Grouping, a.ID_Kategori, a.ID_Kelompok, a.Kode_Aktiva "
			SQL = SQL & "From Barang_Lain a, EMI_Group_Jenis_Lain b, emi_kategori_gudang_barang_lain c,Emi_Kategori_PO e, Emi_Klasifikasi_Bahan f Where "
			SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And a.id_group_jenis = b.id_group_jenis "
			SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
			SQL = SQL & "a.kode_stock_owner = '" & lokasi_pergudang & "' and a.kode_barang = '" & TxtKdBarang.Text.Trim & "' "
			'SQL = SQL & "a.kode_stock_owner = '" & lokasi_pergudang & "' and a.kode_barang = '" & ListView1.FocusedItem.SubItems(1).Text & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dim kodeBarangTemp As String = ""
					Dim satuanBarang As String = ""

					kodeBarangTemp = Dr("kode_barang")
					satuanBarang = Dr("satuan")

					If General_Class.CekNULL(Dr("Id_Kategori_Gudang")) = "" Then
						CmbJnsGudang.SelectedIndex = -1
					Else
						CmbJnsGudang.Text = Dr("kode_kategori_gudang")
					End If

					Button1.Text = "&Update" 'Button2.Enabled = True

					If General_Class.CekNULL(Dr("flag_potong_stok_X")) = "Y" Then
						Cmb_FlagPotongStok.SelectedIndex = 0
						txtStandarPrice.Text = ""
					Else
						Cmb_FlagPotongStok.SelectedIndex = 1
						txtStandarPrice.Text = Dr("standar_price_X")

					End If

					TxtNama.Text = Dr("Nama")
					cmbSatuan.Text = Dr("satuan")
					'TextBox4.Text = Dr("keterangan")
					TextBox5.Text = Dr("harga_beli_x")
					If General_Class.CekNULL(Dr("last_hpp_X")) = "" Then
						TextBox6.Text = 0
					Else
						TextBox6.Text = Dr("last_hpp_X")
					End If

					TextBox7.Text = Dr("stock_minimum")
					'   TextBox9.Text = Dr("lemari")
					ComboBox4.Text = General_Class.CekNULL(Dr("kode_kategori"))

					'baru di komen
					'If UserLevel = "2" Then 'Kasir
					'    Button1.Enabled = False
					'    'Button2.Enabled = False
					'End If
					'  ComboBox5.Text = Dr("kode_pembeda")
					'ComboBox3.Text = Dr("aktif")
					ComboBox9.Text = Dr("flag_ppn")
					'   ComboBox6.Text = Dr("pakai_sn")
					ComboBox10.Text = Dr("flag_sendiri_x")
					'TextBox10.Text = Dr("kode_supplier")
					'TextBox11.Text = Dr("nama_supplier")

					If General_Class.CekNULL(Dr("berat")) = "" Then
						TextBox12.Text = 0
					Else
						TextBox12.Text = Dr("berat")
					End If

					If General_Class.CekNULL(Dr("berat_kotor")) = "" Then
						TextBox15.Text = 0
					Else
						TextBox15.Text = Dr("berat_kotor")
					End If

					''ComboBox11.Text = Dr("input_csi")

					If General_Class.CekNULL(Dr("panjang")) = "" Then
						TextBox16.Text = 0
					Else
						TextBox16.Text = Dr("Panjang")
					End If

					If General_Class.CekNULL(Dr("lebar")) = "" Then
						TextBox17.Text = 0
					Else
						TextBox17.Text = Dr("lebar")
					End If

					If General_Class.CekNULL(Dr("Tinggi")) = "" Then
						TextBox18.Text = 0
					Else
						TextBox18.Text = Dr("Tinggi")
					End If

					If General_Class.CekNULL(Dr("Kode_Kategori_Besar_X")) = "" Then
						ComboBox12.SelectedIndex = -1
					Else
						ComboBox12.Text = Dr("Kode_Kategori_Besar_X")
					End If

					If General_Class.CekNULL(Dr("Kode_Kategori_Kecil_X")) = "" Then
						ComboBox13.SelectedIndex = -1
					Else
						ComboBox13.Text = Dr("Kode_Kategori_Kecil_X")
					End If

					If General_Class.CekNULL(Dr("id_group_jenis")) = "" Then
						cmbJenis.SelectedIndex = -1
					Else
						cmbJenis.Text = Dr("kode_group_jenis")
						cmbJenis_SelectedIndexChanged(e, New EventArgs)
					End If

					If General_Class.CekNULL(Dr("id_master_kategori_gudang_x")) = "" Then
						Cmb_KategoriGudang.SelectedIndex = -1
					Else
						Cmb_KategoriGudang.Text = General_Class.CekNULL(Dr("keterangan_master_kategori_gudang"))
					End If

					If General_Class.CekNULL(Dr("Keterangan")) = "" Then
						ComboBox5.SelectedItem = -1
					Else
						ComboBox5.Text = Dr("keterangan")
					End If

					If General_Class.CekNULL(Dr("KeteranganPO")) = "" Then
						ComboBox6.SelectedItem = -1
					Else
						ComboBox6.Text = Dr("keteranganPO")
					End If

					If General_Class.CekNULL(Dr("KeteranganBhn")) = "" Then
						ComboBox11.SelectedItem = -1
					Else
						ComboBox11.Text = Dr("KeteranganBhn")
					End If

					If General_Class.CekNULL(Dr("keterangan_Bhn2")) = "" Then
						ComboBox19.SelectedItem = -1
					Else
						ComboBox19.Text = (Dr("keterangan_Bhn2"))
					End If

					If General_Class.CekNULL(Dr("Keterangan_Routing")) = "" Then
						ComboBox14.SelectedItem = -1
					Else
						ComboBox14.Text = Dr("Keterangan_Routing")
					End If

					'===============================
					If General_Class.CekNULL(Dr("Jenis_Kemasan_X")) = "" Then
						ComboBox15.SelectedItem = -1
					Else
						ComboBox15.Text = Dr("Jenis_Kemasan_X")
					End If

					If General_Class.CekNULL(Dr("Metode_Pengeluaran_Stok")) = "" Then
						ComboBox16.SelectedItem = -1
					Else
						ComboBox16.Text = Dr("Metode_Pengeluaran_Stok")
					End If

					If General_Class.CekNULL(Dr("Flag_Grouping")) = "" Then
						Cmb_FlagGrouping.SelectedItem = -1
					Else
						Cmb_FlagGrouping.Text = Dr("Flag_Grouping")
					End If

					If General_Class.CekNULL(Dr("kategori_Jenis")) = "" Then
						CmbKatJenis.SelectedItem = -1
					Else
						CmbKatJenis.Text = Dr("kategori_Jenis")
					End If

					If General_Class.CekNULL(Dr("sub_kategori_Jenis")) = "" Then
						CmbKatJenisSub.SelectedItem = -1
					Else
						CmbKatJenisSub.Text = Dr("sub_kategori_Jenis")
					End If

					If General_Class.CekNULL(Dr("sub_kategori_Jenis_1")) = "" Then
						CmbKatJenisSub1.SelectedItem = -1
					Else
						CmbKatJenisSub1.Text = Dr("sub_kategori_Jenis_1")
					End If

					If General_Class.CekNULL(Dr("sub_kategori_Jenis_2")) = "" Then
						CmbKatJenisSub2.SelectedItem = -1
					Else
						CmbKatJenisSub2.Text = Dr("sub_kategori_Jenis_2")
					End If

					If General_Class.CekNULL(Dr("sub_kategori_Jenis_3")) = "" Then
						CmbKatJenisSub3.SelectedItem = -1
					Else
						CmbKatJenisSub3.Text = Dr("sub_kategori_Jenis_3")
					End If

					If General_Class.CekNULL(Dr("ID_Kategori")) = "" Then
						Cmb_Kategori.SelectedItem = -1
					Else
						Cmb_Kategori.SelectedIndex = arrKategori.IndexOf(CInt(Dr("ID_Kategori").ToString.Trim))
						If General_Class.CekNULL(Dr("ID_Kelompok")) = "" Then
							Cmb_Kelompok.SelectedItem = -1
						Else
							Cmb_Kelompok.SelectedIndex = arrKelompok.IndexOf(CInt(Dr("ID_Kelompok").ToString.Trim))
							If General_Class.CekNULL(Dr("Kode_Aktiva")) = "" Then
								Cmb_Kd_Aktiva.SelectedItem = -1
							Else
								Cmb_Kd_Aktiva.SelectedIndex = arrKodeAktiva.IndexOf(CInt(Dr("Kode_Aktiva").ToString.Trim))
							End If
						End If
					End If

					cmbSatuan.Enabled = False
					CmbKatJenis.Enabled = False
					CmbKatJenisSub.Enabled = False
					CmbKatJenisSub1.Enabled = False
					CmbKatJenisSub2.Enabled = False
					CmbKatJenisSub3.Enabled = False
					TxtNama.Enabled = False

					TextBox3.Text = General_Class.CekNULL(Dr("Berat_Bags_X"))
					TextBox4.Text = General_Class.CekNULL(Dr("Isi_Per_Bags_X"))

					Txtket.Text = General_Class.CekNULL(Dr("ket_barang"))

					TxtKdBarang.Text = Dr("kode_barang")

					Dr.Close()

					OpenConn()

					DgvSatuanTerpilih.Rows.Clear()
					Dim rows As Integer = 0
					SQL = "select a.Satuan,a.Jumlah,a.Flag_Tampil_Display, a.Flag_Kirim, b. Flag_General from Barang_Detail_Satuan_Lain a , EMI_Satuan_Detail_Perhitungan b where   "
					SQL = SQL & " a.Kode_Perusahaan = b.Kode_Perusahaan and a.Satuan = b.Satuan_Akhir and Kode_barang = '" & kodeBarangTemp & "' "
					SQL = SQL & "and satuan_awal = '" & satuanBarang & "' "
					SQL = SQL & "group by a.Satuan,a.Jumlah,a.Flag_Tampil_Display,b. Flag_General, a.Flag_Kirim "
					Using Dr2 = OpenTrans(SQL)
						Do While Dr2.Read
							DgvSatuanTerpilih.Rows.Add(1)
							DgvSatuanTerpilih.Rows(rows).Cells(0).Value = Dr2("satuan")
							DgvSatuanTerpilih.Rows(rows).Cells(1).Value = Dr2("jumlah")

							If General_Class.CekNULL(Dr2("flag_tampil_display")) <> "" Then
								DgvSatuanTerpilih.Rows(rows).Cells(2).Value = True
							End If

							If General_Class.CekNULL(Dr2("flag_kirim")) <> "" Then
								DgvSatuanTerpilih.Rows(rows).Cells(4).Value = True
							End If

							If General_Class.CekNULL(Dr2("flag_general")) = "" Then
								DgvSatuanTerpilih.Rows(rows).Cells(3).Value = "T"
							ElseIf Dr2("flag_general") = "T" Then
								DgvSatuanTerpilih.Rows(rows).Cells(3).Value = "T"
							Else
								DgvSatuanTerpilih.Rows(rows).Cells(3).Value = "Y"
							End If

							If General_Class.CekNULL(Dr2("flag_General")) = "Y" Then
								DgvSatuanTerpilih.Rows(rows).Cells(1).ReadOnly = True
								DgvSatuanTerpilih.Rows(rows).Cells(1).Style.BackColor = Color.Yellow
							End If

							rows = rows + 1
						Loop
					End Using

					'For i As Integer = 0 To lvwSatuan.Items.Count - 1
					'    SQL = "select flag_tampil_display from Barang_Detail_Satuan_Lain where kode_perusahaan = '" & KodePerusahaan & "'  "
					'    SQL = SQL & "and kode_barang = '" & kodeBarangTemp & "' and satuan = '" & lvwSatuan.Items(i).SubItems(1).Text & "'"
					'    Using Dr2 = OpenTrans(SQL)
					'        If Dr2.Read Then
					'            If General_Class.CekNULL(Dr2("flag_tampil_display")) <> "" Then
					'                lvwSatuan.Items(i).Checked = True
					'            End If

					'        End If
					'    End Using
					'Next

					ListView10.Visible = False
					TxtNama.Focus()
				Else
					TxtNama.Text = "" : cmbSatuan.SelectedIndex = -1 : TextBox5.Text = ""

					TextBox6.Text = "" : TextBox7.Text = "" : ComboBox3.SelectedIndex = 0
					ComboBox4.SelectedIndex = -1
					TextBox10.Text = "" : TextBox11.Text = "" : ComboBox10.SelectedIndex = -1
					ComboBox9.SelectedIndex = 0
					TextBox12.Text = "" : TextBox15.Text = ""

					ComboBox5.SelectedIndex = -1 : ComboBox14.SelectedIndex = -1
					ComboBox15.SelectedIndex = -1 : ComboBox16.SelectedIndex = -1

					TextBox3.Text = "" : TextBox4.Text = ""

					TextBox16.Text = "" : TextBox17.Text = "" : TextBox18.Text = ""
					'  ComboBox11.SelectedIndex = -1
					ComboBox12.SelectedIndex = -1 : ComboBox13.SelectedIndex = -1

					Txtket.Text = ""
					'cmbJenis.SelectedIndex = -1

					Cmb_Kategori.SelectedIndex = -1 : Cmb_Kelompok.Items.Clear() : Cmb_Kd_Aktiva.Items.Clear()

					Button1.Text = "&Simpan" 'Button2.Enabled = False
					Button1.Enabled = True
				End If
			End Using

			xurut_departement = ""
			xurut_departement = ""
			xNoFakturPengajuanBrgBru = ""
			xFrom = ""

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Get_Lv_SatuanTurunan()
		If arrSatuanTurunan.Count > 0 Then

			ComboBox18.Items.Clear()
			For Each Satuan As String In arrSatuanTurunan
				ComboBox18.Items.Add(Satuan)
			Next

			ComboBox18.SelectedIndex = 0

		End If

	End Sub

	Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
		If cmbJenis.Text.Trim.Length = 0 Then
			MessageBox.Show("Jenis barang Belum di pilih . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			cmbJenis.Focus() : Exit Sub
		ElseIf ComboBox2.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Barang_Err_Kode_Stock_Owner, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox2.Focus() : Exit Sub
		ElseIf TxtKdBarang.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Barang_Err_Kode_Barang, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TxtKdBarang.Focus() : Exit Sub
		ElseIf TxtNama.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Barang_Err_Nama_Barang, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TxtNama.Focus() : Exit Sub
		ElseIf cmbSatuan.SelectedIndex = -1 Then
			MessageBox.Show(Base_Language.Lang_Global_Satuan_Barang, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			cmbSatuan.Focus() : Exit Sub
			'ElseIf TextBox4.Text.Trim.Length = 0 Then
			'    MessageBox.Show("Keterangan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    TextBox4.Focus() : Exit Sub
			'ElseIf TextBox5.Text.Trim.Length = 0 Then
			'    MessageBox.Show(Base_Language.Lang_Barang_Err_Harga_Barang, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    TextBox5.Focus() : Exit Sub
		ElseIf TextBox7.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Barang_Err_Stock_Min, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TextBox7.Focus() : Exit Sub

		ElseIf ComboBox4.SelectedIndex = -1 Then
			MessageBox.Show(Base_Language.Lang_Barang_Err_Kategori, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox4.Focus() : Exit Sub
		ElseIf ComboBox3.SelectedIndex = -1 Then
			MessageBox.Show(Base_Language.Lang_Barang_Err_Status_Aktif, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox3.Focus() : Exit Sub
		ElseIf ComboBox9.SelectedIndex = -1 Then
			MessageBox.Show(Base_Language.Lang_Barang_Err_Flag_PPN, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox9.Focus() : Exit Sub
			'ElseIf ComboBox5.SelectedIndex = -1 Then
			'    MessageBox.Show("Pembeda harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    ComboBox5.Focus() : Exit Sub
			'ElseIf ComboBox6.SelectedIndex = -1 Then
			'   MessageBox.Show("Pakai serial number harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'  ComboBox6.Focus() : Exit Sub
			'ElseIf TextBox10.Text.Trim.Length = 0 Then
			'    MessageBox.Show("Supplier harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    TextBox10.Focus() : Exit Sub
			'ElseIf ComboBox10.SelectedIndex = -1 Then
			'    MessageBox.Show(Base_Language.Lang_Barang_Err_Barang_Sendiri, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    ComboBox10.Focus() : Exit Sub
		ElseIf TextBox12.Text.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Barang_Err_Berat_Bersih, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TextBox12.Focus() : Exit Sub
		ElseIf TextBox15.Text.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Barang_Err_Berat_Kotor, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TextBox15.Focus() : Exit Sub

		ElseIf TextBox16.Text.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Barang_Err_Panjang, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TextBox16.Focus() : Exit Sub
		ElseIf TextBox17.Text.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Barang_Err_Lebar, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TextBox17.Focus() : Exit Sub
		ElseIf TextBox18.Text.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Barang_Err_Tinggi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TextBox18.Focus() : Exit Sub
			'ElseIf ComboBox12.SelectedIndex = -1 Then
			'    MessageBox.Show(Base_Language.Lang_Barang_Err_Kategori_Besar, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    ComboBox12.Focus() : Exit Sub
			'ElseIf ComboBox13.SelectedIndex = -1 Then
			'    MessageBox.Show(Base_Language.Lang_Barang_Err_Kategori_Kecil, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    ComboBox13.Focus() : Exit Sub
			'ElseIf CmbJnsGudang.SelectedIndex = -1 Then
			'    MessageBox.Show(Base_Language.Lang_Barang_Err_Jenis_Gudang, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    CmbJnsGudang.Focus() : Exit Sub
			'ElseIf ComboBox11.SelectedIndex = -1 Then
			'    MessageBox.Show("Input CSI harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    ComboBox11.Focus() : Exit Sub
			'ElseIf Cmb_KategoriGudang.SelectedIndex = -1 Then
			'    MessageBox.Show(Base_Language.Lang_Global_KategoriGudang, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    Cmb_KategoriGudang.Focus() : Exit Sub
			'ElseIf ComboBox5.SelectedIndex = -1 Then
			'    MessageBox.Show("Kategori QC harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    ComboBox5.Focus() : Exit Sub
		ElseIf ComboBox6.SelectedIndex = -1 Then
			MessageBox.Show("Kategori PO harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox6.Focus() : Exit Sub
			'ElseIf ComboBox14.SelectedIndex = -1 Then
			'    MessageBox.Show("Routing harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    ComboBox14.Focus() : Exit Sub
			'ElseIf ComboBox15.SelectedIndex = -1 Then
			'    MessageBox.Show("Jenis Kemasan harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    ComboBox15.Focus() : Exit Sub
		ElseIf ComboBox16.SelectedIndex = -1 Then
			MessageBox.Show("Metode Pengeluaran Stok harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox16.Focus() : Exit Sub

		ElseIf Cmb_FlagGrouping.SelectedIndex = -1 Then
			MessageBox.Show("Flag Grouping Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_FlagGrouping.Focus() : Exit Sub

			'ElseIf ComboBox20.SelectedIndex = -1 Then
			'    MessageBox.Show("Kategori Jenis Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    ComboBox20.Focus() : Exit Sub

			'ElseIf ComboBox21.SelectedIndex = -1 Then
			'    MessageBox.Show("Sub Kategori Jenis Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    ComboBox21.Focus() : Exit Sub

			'ElseIf ComboBox22.SelectedIndex = -1 Then
			'    MessageBox.Show("Sub Kategori Jenis 1 Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    ComboBox22.Focus() : Exit Sub

			'ElseIf ComboBox23.SelectedIndex = -1 Then
			'    MessageBox.Show("Sub Kategori Jenis 2 Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    ComboBox23.Focus() : Exit Sub
		End If

		If arrGudangRawMaterial.Contains(arrJenisBarang(cmbJenis.SelectedIndex)) Then
			If ComboBox11.SelectedIndex = -1 Then
				MessageBox.Show("Klasifikasi Bahan harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				ComboBox11.Focus() : Exit Sub
			End If
		End If

		'If Cmb_FlagPotongStok.SelectedIndex = -1 Then
		'    MessageBox.Show("Flag Potong Stok harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'    ComboBox16.Focus() : Exit Sub
		'End If

		'If Cmb_FlagPotongStok.SelectedIndex = 1 Then
		'    If txtStandarPrice.Text.Trim.Length = 0 Then
		'        MessageBox.Show("Standar Price harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'        ComboBox16.Focus() : Exit Sub
		'    End If
		'End If

		Dim cekDIsplayYangDIpilih As Integer = 0
		Dim cekkirimYangDIpilih As Integer = 0
		For i As Integer = 0 To DgvSatuanTerpilih.Rows.Count - 1

			If DgvSatuanTerpilih.Rows(i).Cells(3).Value = "T" And DgvSatuanTerpilih.Rows(i).Cells(1).Value < 1 Then
				MessageBox.Show(Base_Language.Lang_Barang_Err_Nilai_Pengali1 & " " & DgvSatuanTerpilih.Rows(i).Cells(0).Value & " " & Base_Language.Lang_Barang_Err_Nilai_Pengali2)
				Exit Sub
			End If

			If DgvSatuanTerpilih.Rows(i).Cells(2).Value = True Then
				cekDIsplayYangDIpilih = cekDIsplayYangDIpilih + 1
			End If

			If DgvSatuanTerpilih.Rows(i).Cells(4).Value = True Then
				cekkirimYangDIpilih = cekkirimYangDIpilih + 1
			End If
		Next

		If cekDIsplayYangDIpilih <> 1 Then
			MessageBox.Show(Base_Language.Lang_Barang_Err_Flag_Tampil_Display)
			Exit Sub
		End If

		'If cekkirimYangDIpilih <> 1 Then
		'    MessageBox.Show(Base_Language.Lang_Barang_Err_Flag_Tampil_Display)
		'    Exit Sub
		'End If
		' ComboBox5.SelectedIndex = 0

		get_jam()

		Try

			OpenConn()

			Cmd.Transaction = Cn.BeginTransaction

			Dim SelectedKategori As String = ""
			Dim SelectedKelompok As String = ""
			Dim SelectedKdAktiva As String = ""

			'=========================================
			'=     CEK APAKAH JENIS BARANG ASSET     =
			'=========================================
			SQL = "select Flag_Asset, Flag_Sparepart, Flag_Peralatan "
			SQL = SQL & "from EMI_Group_Jenis_Lain "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and Id_Group_Jenis = '" & arrJenisBarang(cmbJenis.SelectedIndex) & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then

					If Dr("Flag_Asset") = "Y" Then

						SelectedKategori = "NULL"
						SelectedKelompok = "NULL"

						If Cmb_Kd_Aktiva.SelectedIndex = -1 Then
							Dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("Kode Aktiva Harus Di Isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						Else
							SelectedKdAktiva = "'" & arrKodeAktiva(Cmb_Kd_Aktiva.SelectedIndex) & "'"
							SelectedKategori = "'" & arrSubKategoriJenis(CmbKatJenisSub.SelectedIndex) & "'"
							SelectedKelompok = "'" & arrSubKategoriJenis1(CmbKatJenisSub1.SelectedIndex) & "'"
						End If

						'If Cmb_Kategori.SelectedIndex = -1 Then
						'    Dr.Close()
						'    CloseTrans()
						'    CloseConn()
						'    MessageBox.Show("Kategori Harus Di Isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'    Exit Sub
						'Else
						'    SelectedKategori = "'" & arrKategori(Cmb_Kategori.SelectedIndex) & "'"

						'    If Cmb_Kelompok.SelectedIndex = -1 Then
						'        Dr.Close()
						'        CloseTrans()
						'        CloseConn()
						'        MessageBox.Show("Kelompok Harus Di Isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'        Exit Sub
						'    Else
						'        SelectedKelompok = "'" & arrKelompok(Cmb_Kelompok.SelectedIndex) & "'"
						'        If Cmb_Kd_Aktiva.SelectedIndex = -1 Then
						'            Dr.Close()
						'            CloseTrans()
						'            CloseConn()
						'            MessageBox.Show("Kode Aktiva Harus Di Isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'            Exit Sub
						'        Else
						'            SelectedKdAktiva = "'" & arrKodeAktiva(Cmb_Kd_Aktiva.SelectedIndex) & "'"
						'        End If
						'    End If
						'End If
					Else
						SelectedKategori = "NULL"
						SelectedKelompok = "NULL"
						SelectedKdAktiva = "NULL"
					End If
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Jenis Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			If Button1.Text = "&Simpan" Then

				If CmbKatJenis.SelectedIndex = -1 Then

					CloseTrans()
					CloseConn()
					MessageBox.Show("Kategori Jenis Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					CmbKatJenis.Focus() : Exit Sub

				ElseIf CmbKatJenisSub.SelectedIndex = -1 Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("Sub Kategori Jenis Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					CmbKatJenisSub.Focus() : Exit Sub

				ElseIf CmbKatJenisSub1.SelectedIndex = -1 Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("Sub Kategori Jenis 1 Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					CmbKatJenisSub1.Focus() : Exit Sub

				ElseIf CmbKatJenisSub2.SelectedIndex = -1 Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("Sub Kategori Jenis 2 Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					CmbKatJenisSub2.Focus() : Exit Sub
				ElseIf CmbKatJenisSub3.SelectedIndex = -1 Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("Sub Kategori Jenis 3 Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					CmbKatJenisSub3.Focus() : Exit Sub
				ElseIf CmbKatJenisSub3.SelectedIndex = -1 Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("Sub Kategori Jenis 3 Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					CmbKatJenisSub3.Focus() : Exit Sub
				End If

				If TxtKdBarang.Text.Length <> 10 Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("Terjadi kesalahan pada kode barang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					CmbKatJenisSub3.Focus() : Exit Sub
				End If

				TxtKdBarang.Text = arrPrefix_Kategori.Item(CmbKatJenis.SelectedIndex) &
						arrPrefix_Sub_Kategori.Item(CmbKatJenisSub.SelectedIndex) &
						arrPrefix_Sub_Kategori1.Item(CmbKatJenisSub1.SelectedIndex) &
						arrPrefix_Sub_Kategori2.Item(CmbKatJenisSub2.SelectedIndex) &
						arrPrefix_Sub_Kategori3.Item(CmbKatJenisSub3.SelectedIndex)

				SQL = "select top(1) kode_perusahaan from barang_lain where kode_perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "Id_Sub_Kategori_Jenis_3 = '" & arrSubKategoriJenis3.Item(CmbKatJenisSub3.SelectedIndex) & "'  "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Kategori Sudah ada ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				SQL = "select top(1) kode_perusahaan from barang_lain where kode_perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "Kode_Barang = '" & TxtKdBarang.Text & "'  "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Kode Barang Sudah ada ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				If arrGudangRawMaterial.Contains(arrJenisBarang(cmbJenis.SelectedIndex)) Then

					Dim No_Urut As String

					SQL = "select top(1) substring(kode_barang, 5, 3) as no_urut from barang_lain where kode_perusahaan = '" & KodePerusahaan & "' and "
					SQL = SQL & "Id_Klasifikasi_Bahan_X = '" & arrId_Klasifikasi_Bahan.Item(ComboBox11.SelectedIndex) & "' and "
					SQL = SQL & "Id_Klasifikasi_Bahan2_X = '" & arrId_Klasifikasi_Bahan2.Item(ComboBox19.SelectedIndex) & "' "
					SQL = SQL & "order by no_urut desc"

					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							'If IsDBNull(Dr("No_Urut")) Then
							'    No_Urut = "001"
							'Else
							'
							'End If
							No_Urut = Format(Val(Dr("No_Urut")) + 1, "000")
						Else
							No_Urut = "001"
						End If
						TxtKdBarang.Text = arrprefix_Klasifikasi_Bahan.Item(ComboBox11.SelectedIndex) & arrprefix_Klasifikasi_Bahan2.Item(ComboBox19.SelectedIndex) & No_Urut
					End Using

				End If

				Dim lokasi_pergudang As String = ""

				SQL = " select  b.Kode_Stock_Owner_Gudang from stock_owner a , Binding_Lokasi_Gudang_Lain b where "
				SQL = SQL & "  a.Kode_Stock_Owner = '" & ComboBox2.Text & "'    and b.Gudang_Default = 'Y'"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						lokasi_pergudang = Dr("Kode_Stock_Owner_Gudang")
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show(Base_Language.Lang_Barang_Err_Lokasi_Sudah_Ada)
						Exit Sub
					End If
				End Using

				SQL = "select kode_barang from  barang_lain where kode_perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "kode_stock_owner = '" & lokasi_pergudang & "' and "
				SQL = SQL & "kode_barang = '" & TxtKdBarang.Text.Trim & "'"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show(Base_Language.Lang_Barang_Err_Kode_Barang_Sudah_Ada, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					Else
						Dr.Close()
					End If
				End Using

				SQL = "select kode_stock_owner from View_Lokasi_Stock_Lain where kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y'  "
				SQL = SQL & "order by kode_stock_owner"
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For i As Integer = 0 To .Rows.Count - 1

								SQL = "insert into barang_lain(Kode_Perusahaan,Kode_Stock_Owner,Kode_Barang,Kode_Barang_Inq,Nama,Nama_Inq,Satuan, "
								'SQL = SQL & "harga_beli_x,last_hpp_X,"
								SQL = SQL & "Stock_Minimum,aktif,Kode_Kategori,barang_sendiri ,id_group_jenis,Flag_PPN,berat_kotor,berat,Panjang, "
								SQL = SQL & "Lebar,Tinggi,ID_Kategori_PO, id_kategori_gudang "
								'SQL = SQL & ", id_master_kategori_gudang_x,Kode_Kategori_Besar_X,Kode_Kategori_Kecil_X,ID_Kategori_QC_X"
								'If ComboBox11.SelectedIndex = -1 Then
								'Else
								'    SQL = SQL & ",Id_Klasifikasi_Bahan_X"
								'End If
								'If ComboBox19.SelectedIndex = -1 Then
								'Else
								'    SQL = SQL & ",Id_Klasifikasi_Bahan2_X"
								'End If
								SQL = SQL & ", Metode_Pengeluaran_Stok "
								'SQL = SQL & ", ID_Routing_X, Jenis_Kemasan_X, Berat_Bags_X, Satuan_Berat_Bags_X, Isi_Per_Bags_X, Satuan_Isi_Bags_X,flag_potong_stok_X,standar_price_X "
								SQL = SQL & ", Keterangan, Flag_Grouping, ID_Kategori, ID_Kelompok, Kode_Aktiva,  Id_Sub_Kategori_Jenis_3, Isi_Per_Bags, Satuan_Isi_Bags,Jenis_Kemasan) "
								SQL = SQL & "values ('" & KodePerusahaan & "', '" & .Rows(i).Item("kode_stock_owner") & "', "

								SQL = SQL & "'" & TxtKdBarang.Text.Trim & "', '" & TxtKdBarang.Text.Trim & "', "
								SQL = SQL & "'" & TxtNama.Text.Trim & "', '" & TxtNama.Text.Trim & "', '" & cmbSatuan.Text.Trim & "', "
								'SQL = SQL & "'" & TextBox5.Text & "', 0, "
								SQL = SQL & "'" & TextBox7.Text & "','" & ComboBox3.Text & "', '" & ComboBox4.Text & "', "
								SQL = SQL & "'Y', '" & arrJenisBarang.Item(cmbJenis.SelectedIndex) & "', "
								SQL = SQL & "'" & ComboBox9.Text & "', '" & TextBox15.Text & "' , '" & TextBox12.Text & "', "
								SQL = SQL & "'" & TextBox16.Text & "', '" & TextBox17.Text & "','" & TextBox18.Text & "', "
								SQL = SQL & "'" & arrId_kategori_PO.Item(ComboBox6.SelectedIndex) & "', '" & arrJenisGudang.Item(CmbJnsGudang.SelectedIndex) & "' "
								'SQL = SQL & "'" & arrKategoriGudang.Item(Cmb_KategoriGudang.SelectedIndex) & "', '" & ComboBox12.Text & "', '" & ComboBox13.Text & "', "
								'SQL = SQL & "'" & arrId_kategori_qc.Item(ComboBox5.SelectedIndex) & "' "
								'If ComboBox11.SelectedIndex = -1 Then
								'Else
								'    SQL = SQL & ",'" & arrId_Klasifikasi_Bahan.Item(ComboBox11.SelectedIndex) & "' "
								'End If
								'If ComboBox19.SelectedIndex = -1 Then
								'Else
								'    SQL = SQL & ",'" & arrId_Klasifikasi_Bahan2.Item(ComboBox19.SelectedIndex) & "' "
								'End If
								SQL = SQL & ",'" & ComboBox16.SelectedItem & "' "
								'SQL = SQL & ",'" & arrid_Routing.Item(ComboBox14.SelectedIndex) & "', "

								'SQL = SQL & "'" & ComboBox15.SelectedItem & "', "
								'If TextBox3.Text = "" Or String.IsNullOrWhiteSpace(TextBox3.Text) Then
								'    SQL = SQL & "Null, "
								'Else
								'    SQL = SQL & "'" & TextBox3.Text & "', "
								'End If

								'SQL = SQL & "'" & ComboBox17.SelectedItem & "', "

								'If TextBox4.Text = "" Or String.IsNullOrWhiteSpace(TextBox4.Text) Then
								'    SQL = SQL & "Null, "
								'Else
								'    SQL = SQL & "'" & TextBox4.Text & "', "
								'End If

								'SQL = SQL & "'" & ComboBox18.SelectedItem & "', "
								'SQL = SQL & "'" & Cmb_FlagPotongStok.Text & "' ,"
								'SQL = SQL & "'" & txtStandarPrice.Text.Trim & "' "
								SQL = SQL & ", '" & Txtket.Text & "', '" & Cmb_FlagGrouping.Text & "', " & SelectedKategori & ", "
								SQL = SQL & SelectedKelompok & ", " & SelectedKdAktiva & " ,"
								' SQL = SQL & ", '" & arrKategoriJenis.Item(ComboBox20.SelectedIndex) & "','" & arrSubKategoriJenis.Item(ComboBox21.SelectedIndex) & "', '" & arrSubKategoriJenis1.Item(ComboBox22.SelectedIndex) & "', '" & arrSubKategoriJenis2.Item(ComboBox23.SelectedIndex) & "','1','" & cmbSatuan.Text.Trim & "','Original Bags') "
								SQL = SQL & " '" & arrSubKategoriJenis3.Item(CmbKatJenisSub3.SelectedIndex) & "','1','" & cmbSatuan.Text.Trim & "','Original Bags') "
								ExecuteTrans(SQL)

							Next

							For i As Integer = 0 To DgvSatuanTerpilih.Rows.Count - 1

								Dim checkFlag As String = ""
								Dim checkFlagKirim As String = ""

								If DgvSatuanTerpilih.Rows(i).Cells(2).Value = True Then
									checkFlag = "'Y'"
									checkFlagKirim = "'Y'"
								Else
									checkFlag = "NULL"
									checkFlagKirim = "NULL"
								End If

								'If DgvSatuanTerpilih.Rows(i).Cells(4).Value = True Then
								'    checkFlagKirim = "'Y'"
								'Else
								'    checkFlagKirim = "NULL"
								'End If

								SQL = "insert into Barang_Detail_Satuan_Lain(kode_perusahaan,kode_barang,satuan,flag_tampil_display,jumlah,Flag_Kirim) values("
								SQL = SQL & "'" & KodePerusahaan & "', '" & TxtKdBarang.Text & "', '" & DgvSatuanTerpilih.Rows(i).Cells(0).Value & "',"
								SQL = SQL & "" & checkFlag & ", '" & DgvSatuanTerpilih.Rows(i).Cells(1).Value & "'," & checkFlagKirim & ")"
								ExecuteTrans(SQL)

							Next

							'For i As Integer = 0 To lvwSatuan.Items.Count - 1

							'    Dim checkFlag As String = ""

							'    If lvwSatuan.Items(i).Checked = True Then
							'        checkFlag = "'Y'"
							'    Else
							'        checkFlag = "NULL"
							'    End If

							'    SQL = "insert into Barang_Detail_Satuan_Lain(kode_perusahaan,kode_barang,satuan,flag_tampil_display) values("
							'    SQL = SQL & "'" & KodePerusahaan & "', '" & TextBox1.Text & "', '" & lvwSatuan.Items(i).SubItems(1).Text & "',"
							'    SQL = SQL & "" & checkFlag & ")"
							'    ExecuteTrans(SQL)

							'Next
						Else
							CloseTrans()
							CloseConn()
							MessageBox.Show(Base_Language.Lang_Barang_Err_Lokasi_Tidak_Ditemukan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End With
				End Using

				If xFrom = "departement" Then

					If xurut_departement <> "" Then
						Dim xlokasi As String = ""
						SQL = "select Kode_Stock_Owner_Gudang from Binding_Lokasi_Gudang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and Gudang_Default = 'Y' "
						Using dr = OpenTrans(SQL)
							If dr.Read Then
								xlokasi = dr("Kode_Stock_Owner_Gudang")
							Else
								dr.Close()
								CloseTrans()
								CloseConn()
								MessageBox.Show("lokasi gudang default gudang lain belum dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Exit Sub
							End If
						End Using

						SQL = "insert into N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail_Log(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Nama_Barang, Jumlah, Satuan, Tanggal_Delivery, keterangan, Link, "
						SQL = SQL & "Id_Kategori_Jenis, Id_Sub_Kategori_Jenis, Id_Sub_Kategori_Jenis_1 ,Id_Sub_Kategori_Jenis_2 ,Id_Sub_Kategori_Jenis_3, No_urut) "
						SQL = SQL & "select Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Nama_Barang, Jumlah,Satuan, Tanggal_Delivery, keterangan, Link, "
						SQL = SQL & "Id_Kategori_Jenis, Id_Sub_Kategori_Jenis, Id_Sub_Kategori_Jenis_1 ,Id_Sub_Kategori_Jenis_2 ,Id_Sub_Kategori_Jenis_3, No_urut "
						SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail where kode_perusahaan = '" & KodePerusahaan & "' and no_Urut = '" & xurut_departement & "' "
						ExecuteTrans(SQL)

						SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail set "
						SQL = SQL & "Kode_Stock_Owner = '" & xlokasi & "', "
						SQL = SQL & "Kode_Barang = '" & TxtKdBarang.Text & "', "
						SQL = SQL & "Nama_Barang = '" & TxtNama.Text & "', "
						SQL = SQL & "Satuan = '" & cmbSatuan.Text & "', Flag_Ajukan = NULL, "
						SQL = SQL & "Id_Kategori_Jenis = '" & arrKategoriJenis.Item(CmbKatJenis.SelectedIndex) & "', "
						SQL = SQL & "Id_Sub_Kategori_Jenis = '" & arrSubKategoriJenis.Item(CmbKatJenisSub.SelectedIndex) & "', "
						SQL = SQL & "Id_Sub_Kategori_Jenis_1 = '" & arrSubKategoriJenis1.Item(CmbKatJenisSub1.SelectedIndex) & "',"
						SQL = SQL & "Id_Sub_Kategori_Jenis_2 = '" & arrSubKategoriJenis2.Item(CmbKatJenisSub2.SelectedIndex) & "',"
						SQL = SQL & "Id_Sub_Kategori_Jenis_3 = '" & arrSubKategoriJenis3.Item(CmbKatJenisSub3.SelectedIndex) & "' "
						SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
						SQL = SQL & "and no_Urut = '" & xurut_departement & "' "
						ExecuteTrans(SQL)

						If zNo_Pengajuan_Barang_Baru <> "-" Then
							SQL = "update N_EMI_Pengajuan_Barang_Baru_Lain set "
							SQL = SQL & "Kode_Barang_baru = '" & TxtKdBarang.Text & "', "
							SQL = SQL & "flag_pengajuan_barang_baru = 'Y' ,"
							SQL = SQL & "tanggal_approve = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
							SQL = SQL & "jam_approve = '" & Format(tgl_skg, "HH:MM:ss") & "', "
							SQL = SQL & "userid_approve = '" & UserID & "'"
							SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "and no_faktur = '" & zNo_Pengajuan_Barang_Baru & "' "
							ExecuteTrans(SQL)
						End If

						'BtnCari_Click(Button1, e)
					End If

				ElseIf xFrom = "warehouse" Then
					SQL = "update N_EMI_Pengajuan_Barang_Baru_Lain set "
					SQL = SQL & "Kode_Barang_baru = '" & TxtKdBarang.Text & "', "
					SQL = SQL & "flag_pengajuan_barang_baru = 'Y' ,"
					SQL = SQL & "tanggal_approve = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
					SQL = SQL & "jam_approve = '" & Format(tgl_skg, "HH:MM:ss") & "', "
					SQL = SQL & "userid_approve = '" & UserID & "'"
					SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
					SQL = SQL & "and no_faktur = '" & xNoFakturPengajuanBrgBru & "' "
					ExecuteTrans(SQL)

				End If
			Else

				If CekButtonRole("update_barang_lain") = "T" Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

				SQL = "Update a Set a.nama = '" & TxtNama.Text.Trim & "', "
				'SQL = SQL & "a.satuan = '" & cmbSatuan.Text.Trim & "', "
				'SQL = SQL & "a.harga_beli_x = '" & TextBox5.Text & "', "
				SQL = SQL & "a.stock_minimum = '" & TextBox7.Text & "', "
				SQL = SQL & "a.aktif = '" & ComboBox3.Text & "', "
				SQL = SQL & "a.flag_ppn = '" & ComboBox9.Text & "', "
				SQL = SQL & "a.kode_kategori = '" & ComboBox4.Text & "', "
				SQL = SQL & "a.id_group_jenis = '" & arrJenisBarang(cmbJenis.SelectedIndex) & "', "

				'  SQL = SQL & "a.flag_update = 'Y', "
				'SQL = SQL & "a.flag_potong_stok_X = '" & Cmb_FlagPotongStok.Text & "', "
				'SQL = SQL & "a.standar_price_X = '" & txtStandarPrice.Text & "', "
				'SQL = SQL & "a.flag_sendiri_x = '" & ComboBox10.Text & "', "
				SQL = SQL & "a.berat = '" & TextBox12.Text.Trim & "', "
				SQL = SQL & "a.berat_Kotor = '" & TextBox15.Text.Trim & "', "
				SQL = SQL & "a.Panjang = '" & TextBox16.Text.Trim & "', "
				SQL = SQL & "a.Lebar = '" & TextBox17.Text.Trim & "', "
				SQL = SQL & "a.Tinggi = '" & TextBox18.Text.Trim & "', "
				'SQL = SQL & "a.Kode_Kategori_Besar_X = '" & ComboBox12.Text.Trim & "', "
				'SQL = SQL & "a.Kode_Kategori_Kecil_X = '" & ComboBox13.Text.Trim & "',"
				SQL = SQL & "a.Id_Kategori_Gudang = '" & arrJenisGudang.Item(CmbJnsGudang.SelectedIndex) & "',"
				'SQL = SQL & "a.id_master_kategori_gudang_x = '" & arrKategoriGudang.Item(Cmb_KategoriGudang.SelectedIndex) & "',"
				'SQL = SQL & "a.ID_Kategori_QC_X = '" & arrId_kategori_qc.Item(ComboBox5.SelectedIndex) & "', "
				SQL = SQL & "a.ID_Kategori_PO = '" & arrId_kategori_PO.Item(ComboBox6.SelectedIndex) & "', "
				'SQL = SQL & "a.ID_Routing_X = '" & arrid_Routing.Item(ComboBox14.SelectedIndex) & "', "

				'If ComboBox11.SelectedIndex = -1 Then
				'Else
				'    SQL = SQL & "a.ID_Klasifikasi_Bahan_x = '" & arrId_Klasifikasi_Bahan.Item(ComboBox11.SelectedIndex) & "', "
				'End If
				'If ComboBox19.SelectedIndex = -1 Then
				'Else
				'    SQL = SQL & "a.ID_Klasifikasi_Bahan2_x = '" & arrId_Klasifikasi_Bahan2.Item(ComboBox19.SelectedIndex) & "', "
				'End If

				'SQL = SQL & "a.Jenis_Kemasan_X='" & ComboBox15.SelectedItem & "', "
				SQL = SQL & "a.Metode_Pengeluaran_Stok='" & ComboBox16.SelectedItem & "', "
				'If TextBox3.Text = "" Or String.IsNullOrWhiteSpace(TextBox3.Text) Then
				'    SQL = SQL & "a.Berat_Bags_X = Null, "
				'Else
				'    SQL = SQL & "a.Berat_Bags_X = '" & TextBox3.Text & "', "
				'End If
				'SQL = SQL & "a.Satuan_Berat_Bags_X='" & ComboBox17.SelectedItem & "', "

				'If TextBox4.Text = "" Or String.IsNullOrWhiteSpace(TextBox4.Text) Then
				'    SQL = SQL & "a.Isi_Per_Bags_X = Null, "
				'Else
				'    SQL = SQL & "a.Isi_Per_Bags_X = '" & TextBox4.Text & "', "
				'End If

				'SQL = SQL & "a.Satuan_Isi_Bags_X = '" & ComboBox18.SelectedItem & "', "
				SQL = SQL & "a.keterangan = '" & Txtket.Text & "', "
				' SQL = SQL & "a.input_csi = '" & ComboBox11.Text & "', "

				'    SQL = SQL & "a.penentu_harga_csi = '" & TextBox5.Text & "' "

				SQL = SQL & "a.ID_Kategori = " & SelectedKategori & ", a.ID_Kelompok = " & SelectedKelompok & ", a.Kode_Aktiva = " & SelectedKdAktiva & ", Flag_Grouping = '" & Cmb_FlagGrouping.Text & "' "
				'SQL = SQL & "a.Kode_Kategori_Jenis = '" & arrKategoriJenis.Item(ComboBox20.SelectedIndex) & "', a.Kode_Sub_Kategori_Jenis = '" & arrSubKategoriJenis.Item(ComboBox21.SelectedIndex) & "', "
				'SQL = SQL & "a.Kode_Sub_Kategori_Jenis_1 = '" & arrSubKategoriJenis1.Item(ComboBox22.SelectedIndex) & "', a.Kode_Sub_Kategori_Jenis_2 = '" & arrSubKategoriJenis2.Item(ComboBox23.SelectedIndex) & "', "
				'SQL = SQL & "a.Isi_Per_Bags = '1',a.Satuan_Isi_Bags = '" & cmbSatuan.Text.Trim & "',a.Jenis_Kemasan = 'Original Bags' "

				SQL = SQL & "from barang_lain a, View_Lokasi_Stock_Lain b where "
				SQL = SQL & "a.kode_perusahaan  = b.kode_perusahaan  and "
				SQL = SQL & "a.kode_stock_owner = b.kode_stock_owner and "
				SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "a.kode_barang = '" & TxtKdBarang.Text.Trim & "' "
				ExecuteTrans(SQL)

				SQL = "delete Barang_Detail_Satuan_Lain where kode_perusahaan = '" & KodePerusahaan & "' and kode_barang = '" & TxtKdBarang.Text & "' "
				ExecuteTrans(SQL)

				For i As Integer = 0 To DgvSatuanTerpilih.Rows.Count - 1

					Dim checkFlag As String = ""

					If DgvSatuanTerpilih.Rows(i).Cells(2).Value = True Then
						checkFlag = "'Y'"
					Else
						checkFlag = "NULL"
					End If

					Dim checkFlagKirim As String = ""

					If DgvSatuanTerpilih.Rows(i).Cells(4).Value = True Then
						checkFlagKirim = "'Y'"
					Else
						checkFlagKirim = "NULL"
					End If

					SQL = "insert into Barang_Detail_Satuan_Lain(kode_perusahaan,kode_barang,satuan,flag_tampil_display,jumlah, flag_kirim) values("
					SQL = SQL & "'" & KodePerusahaan & "', '" & TxtKdBarang.Text & "', '" & DgvSatuanTerpilih.Rows(i).Cells(0).Value & "',"
					SQL = SQL & "" & checkFlag & ", '" & DgvSatuanTerpilih.Rows(i).Cells(1).Value & "'," & checkFlagKirim & ")"
					ExecuteTrans(SQL)

					'Dim checkFlag As String = ""

					'If DgvSatuanTerpilih.Rows(i).Cells(2).Value = True Then
					'    checkFlag = "'Y'"
					'Else
					'    checkFlag = "NULL"
					'End If

					'SQL = "select * from Barang_Detail_Satuan_Lain where kode_perusahaan = '" & KodePerusahaan & "' "
					'SQL = SQL & "and kode_barang = '" & TextBox1.Text & "' and satuan = '" & lvwSatuan.Items(i).SubItems(1).Text & "' "
					'Using Dr = OpenTrans(SQL)
					'    If Dr.Read Then
					'        Dr.Close()
					'        SQL = "update Barang_Detail_Satuan_Lain set flag_tampil_display = " & checkFlag & "  "
					'        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
					'        SQL = SQL & "kode_barang = '" & TextBox1.Text & "' and satuan = '" & DgvSatuanTerpilih.Rows(i).Cells(0).ErrorText & "'"
					'        ExecuteTrans(SQL)
					'    Else
					'        Dr.Close()
					'        SQL = "insert into Barang_Detail_Satuan_Lain(kode_perusahaan,kode_barang,satuan,flag_tampil_display) values("
					'        SQL = SQL & "'" & KodePerusahaan & "', '" & TextBox1.Text & "', '" & lvwSatuan.Items(i).SubItems(1).Text & "',"
					'        SQL = SQL & "" & checkFlag & ")"
					'        ExecuteTrans(SQL)

					'    End If
					'End Using

				Next

				'ListView1.FindItemWithText(TextBox1.Text.Trim).Selected = True

				'ListView1.FocusedItem.SubItems(2).Text = TextBox2.Text.Trim
				'ListView1.FocusedItem.SubItems(3).Text = cmbSatuan.Text.Trim
				'ListView1.FocusedItem.SubItems(5).Text = Format(Val(TextBox5.Text.Trim), "N0")
				''ListView1.FocusedItem.SubItems(6).Text = Format(Val(TextBox6.Text.Trim), "N0")
				'ListView1.FocusedItem.SubItems(7).Text = Format(0, "N0")
				'ListView1.FocusedItem.SubItems(8).Text = Format(Val(TextBox7.Text.Trim), "N0")
				''     ListView1.FocusedItem.SubItems(9).Text = TextBox9.Text.Trim
				'ListView1.FocusedItem.SubItems(11).Text = ComboBox4.Text.Trim
				''   ListView1.FocusedItem.SubItems(12).Text = ComboBox5.Text.Trim
				'ListView1.FocusedItem.SubItems(13).Text = ComboBox3.Text.Trim
				''  ListView1.FocusedItem.SubItems(14).Text = ComboBox6.Text.Trim
				'ListView1.FocusedItem.SubItems(15).Text = TextBox10.Text.Trim
				'ListView1.FocusedItem.SubItems(16).Text = TextBox11.Text.Trim
				'ListView1.FocusedItem.SubItems(17).Text = ComboBox9.Text
			End If

			Cmd.Transaction.Commit()

			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		If Button1.Text.ToUpper = "&SIMPAN" Then
			TextBox8.Text = ""
			'Cari("Y")
		End If

		Kosong()
		Cari("T")
		BtnCari_Click(Button1, e)
		cmbJenis.Focus()
	End Sub

	Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Dim Hapus1 As String = MessageBox.Show("Anda yakin data ini akan dihapus?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
		If Hapus1 = vbYes Then
			Try

				OpenConn()

				'Adjustment
				Using Dr1 = OpenTrans("Select top 1 kode_perusahaan from adjustment where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ComboBox2.Text & "' and kode_barang = '" & TxtKdBarang.Text.Trim & "'")
					If Dr1.Read Then
						MessageBox.Show("Penghapusan tidak dapat dilakukan, karena masih dipakai di data adjustment", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
						Kosong()
						TxtKdBarang.Focus()
						CloseConn()
						Exit Sub
					End If
				End Using

				'Detail Penjualan
				Using Dr1 = OpenTrans("Select top 1 kode_perusahaan from detail_penjualan where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ComboBox2.Text & "' and kode_barang = '" & TxtKdBarang.Text.Trim & "'")
					If Dr1.Read Then
						MessageBox.Show("Penghapusan tidak dapat dilakukan, karena masih dipakai di data penjualan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
						Kosong()
						TxtKdBarang.Focus()
						CloseConn()
						Exit Sub
					End If
				End Using

				'Detail_R_Penjualan
				Using Dr1 = OpenTrans("Select top 1 kode_perusahaan from detail_r_penjualan where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ComboBox2.Text & "' and kode_barang = '" & TxtKdBarang.Text.Trim & "'")
					If Dr1.Read Then
						MessageBox.Show("Penghapusan tidak dapat dilakukan, karena masih dipakai di data retur penjualan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
						Kosong()
						TxtKdBarang.Focus()
						CloseConn()
						Exit Sub
					End If
				End Using

				'Detail Pembelian
				Using Dr1 = OpenTrans("Select top 1 kode_perusahaan from detail_pembelian where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ComboBox2.Text & "' and kode_barang = '" & TxtKdBarang.Text.Trim & "'")
					If Dr1.Read Then
						MessageBox.Show("Penghapusan tidak dapat dilakukan, karena masih dipakai di data pembelian", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
						Kosong()
						TxtKdBarang.Focus()
						CloseConn()
						Exit Sub
					End If
				End Using

				'Detail_R_Pembelian
				Using Dr1 = OpenTrans("Select top 1 kode_perusahaan from detail_r_pembelian where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ComboBox2.Text & "' and kode_barang = '" & TxtKdBarang.Text.Trim & "'")
					If Dr1.Read Then
						MessageBox.Show("Penghapusan tidak dapat dilakukan, karena masih dipakai di data retur pembelian", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
						Kosong()
						TxtKdBarang.Focus()
						CloseConn()
						Exit Sub
					End If
				End Using

				SQL = "Delete From barang_proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ComboBox2.Text & "' and kode_barang = '" & TxtKdBarang.Text.Trim & "'"
				ExecuteTrans(SQL)

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try
		Else
			MessageBox.Show("Penghapusan dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		End If

		Kosong()
		Cari("T")
		TxtKdBarang.Focus()
	End Sub

	Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
		If e.KeyChar = Chr(13) Then TextBox8.Focus()
	End Sub

	Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
		Cari("T")
	End Sub

	Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
		If ListView1.Items.Count = 0 Then Exit Sub

		For i As Integer = 0 To ComboBox2.Items.Count - 1
			xSplit = ComboBox2.Items(i).split("-")
			If ListView1.FocusedItem.Text = xSplit(0).Trim Then
				ComboBox2.SelectedIndex = i
				Exit For
			End If
		Next

		TxtKdBarang.Text = ListView1.FocusedItem.SubItems(1).Text
		'ComboBox2.Text = ListView1.FocusedItem.Text
		TextBox1_Leave(ListView1, e)

		TabControl1.SelectedIndex = 0

	End Sub

	Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged
		'Cari("T")
	End Sub

	Private Sub ComboBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox3.KeyPress
		If e.KeyChar = Chr(13) Then ComboBox9.Focus()
	End Sub

	Private Sub TextBox9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
		If e.KeyChar = Chr(13) Then ComboBox10.Focus()
	End Sub

	Private Sub ComboBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox4.KeyPress
		If e.KeyChar = Chr(13) Then CmbKatJenis.Focus()
	End Sub

	Private Sub ComboBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox5.KeyPress
		If e.KeyChar = Chr(13) Then ComboBox6.Focus()
	End Sub

	Private Sub ComboBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox6.KeyPress
		If e.KeyChar = Chr(13) Then ComboBox16.Focus()
	End Sub

	Private Sub TextBox10_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox10.KeyDown
		If e.KeyCode = Keys.Down Then
			If ListView10.Items.Count = 0 Then Exit Sub
			ListView10.Focus()
		End If
	End Sub

	Private Sub TextBox10_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox10.KeyPress
		If e.KeyChar = Chr(13) Then
			If TextBox10.Text.Trim.Length = 0 Then
				ListView10.Visible = False : TextBox11.Focus() : Exit Sub
			End If
			TextBox10_Leave(TextBox10, e)
		End If
		If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
	End Sub

	Private Sub TextBox10_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox10.Leave
		If TextBox10.Text.Trim.Length = 0 Then
			ListView10.Visible = False : Exit Sub
		Else
			ListView10.Visible = True
		End If
		If ListView10.Focused = True Then Exit Sub

		'If Button2.Text = "&Update" Then Exit Sub

		OpenConn()

		Dim Lanjut As Boolean = False
		SQL = "select kode_supplier, nama from suppliers where kode_perusahaan = '" & KodePerusahaan & "' and kode_supplier = '" & Trim(TextBox10.Text) & "'"
		Using Dr = OpenTrans(SQL)
			If Dr.Read Then
				TextBox10.Text = Dr("kode_supplier")
				TextBox11.Text = Dr("nama")
				Lanjut = True
				TextBox12.Focus()
			Else
				TextBox10.Text = ""
				TextBox11.Text = ""
				Lanjut = False
				TextBox11.Focus()
			End If
			ListView10.Visible = False
		End Using

		CloseConn()
	End Sub

	Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged
		If TextBox10.Text.Trim.Length = 0 Then
			ListView10.Visible = False : Exit Sub
		Else
			ListView10.Visible = True
		End If

		Try
			OpenConn()

			ListView10.Items.Clear()
			Dim Lvw As ListViewItem

			SQL = "select kode_supplier, Nama, Alamat, Telepon, fax from suppliers where "
			SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and kode_supplier like '%" & Trim(TextBox10.Text) & "%' order by nama"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lvw = ListView10.Items.Add(Dr("kode_supplier"))
					Lvw.SubItems.Add(Dr("Nama"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub TextBox11_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox11.KeyDown
		If e.KeyCode = Keys.Down Then
			If ListView10.Items.Count = 0 Then Exit Sub
			ListView10.Focus()
		End If
	End Sub

	Private Sub TextBox11_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox11.KeyPress
		If e.KeyChar = Chr(13) Then
			If TextBox10.Text.Trim.Length = 0 Then TextBox11.Text = "" : ListView10.Visible = False ': Exit Sub
			TextBox12.Focus()
		End If
		If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
	End Sub

	Private Sub TextBox11_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox11.Leave
		If ListView10.Focused = True Then Exit Sub
		TextBox10.Text = "" : TextBox11.Text = ""
	End Sub

	Private Sub TextBox11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged
		If TextBox11.Text.Trim.Length = 0 Then
			ListView10.Visible = False : Exit Sub
		Else
			ListView10.Visible = True
		End If

		Try

			OpenConn()

			ListView10.Items.Clear()
			Dim Lvw As ListViewItem

			SQL = "select kode_supplier, Nama, Alamat, Telepon, fax from suppliers where "
			SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and nama like '%" & Trim(TextBox11.Text) & "%' order by nama"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lvw = ListView10.Items.Add(Dr("kode_supplier"))
					Lvw.SubItems.Add(Dr("Nama"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub ListView10_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView10.DoubleClick
		If ListView10.Items.Count = 0 Then Exit Sub
		TextBox10.Text = ListView10.FocusedItem.Text

		TextBox10.Focus()
		TextBox12.Focus()

		ListView10.Visible = False
	End Sub

	Private Sub ListView10_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView10.KeyDown
		If e.KeyCode = Keys.Enter Then
			ListView10_DoubleClick(ListView10, e)
		End If
	End Sub

	Private Sub TextBox12_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
		If e.KeyChar = Chr(13) Then TextBox7.Focus()
		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
	End Sub

	Private Sub ComboBox9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox9.KeyPress, Cmb_FlagGrouping.KeyPress, Cmb_Kategori.KeyPress, Cmb_Kd_Aktiva.KeyPress, Cmb_Kelompok.KeyPress
		If e.KeyChar = Chr(13) Then TxtEstimasiPengiriman.Focus()
	End Sub

	Private Sub ComboBox10_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox10.KeyPress
		If e.KeyChar = Chr(13) Then ComboBox4.Focus()
	End Sub

	Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged

	End Sub

	Private Sub TextBox12_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox12.KeyPress
		If e.KeyChar = Chr(13) Then TextBox15.Focus()
		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar <= Chr(Asc("."))) Then e.KeyChar = Chr(0)
	End Sub

	'Private Sub TextBox15_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox15.KeyPress
	'    If e.KeyChar = Chr(13) Then TextBox13.Focus()
	'    If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar <= Chr(Asc("."))) Then e.KeyChar = Chr(0)
	'End Sub

	Private Sub TextBox16_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox16.KeyPress
		If e.KeyChar = Chr(13) Then TextBox17.Focus()
		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
	End Sub

	Private Sub TextBox17_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox17.KeyPress
		If e.KeyChar = Chr(13) Then TextBox18.Focus()
		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
	End Sub

	Private Sub TextBox18_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox18.KeyPress
		If e.KeyChar = Chr(13) Then ComboBox6.Focus()
		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
	End Sub

	Private Sub ComboBox12_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox12.KeyPress
		If e.KeyChar = Chr(13) Then ComboBox13.Focus()
	End Sub

	Private Sub ComboBox12_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox12.SelectedIndexChanged

		Try
			OpenConn()
			ComboBox13.Items.Clear()
			SQL = "Select Kode_Kategori_Kecil From Kategori_Kecil where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Kategori_Besar ='" & ComboBox12.Text & "' order by Kode_Kategori_Besar"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					ComboBox13.Items.Add(dr("Kode_Kategori_Kecil"))
				Loop
			End Using
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub lvwSatuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwSatuan.SelectedIndexChanged

	End Sub

	'Private Sub TextBox13_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
	'    If e.KeyChar = Chr(13) Then TextBox14.Focus()
	'End Sub

	Private Sub TextBox14_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

	End Sub

	Private Sub cmbSatuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSatuan.KeyPress
		If e.KeyChar = Chr(13) Then Txtket.Focus()
	End Sub

	Private Sub TextBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox7.KeyPress
		If e.KeyChar = Chr(13) Then CmbJnsGudang.Focus()
		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
	End Sub

	Private Sub cmbJenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbJenis.KeyPress

		If e.KeyChar = Chr(13) Then
			CmbKatJenis.Focus()
			'If ComboBox11.Enabled = True Then
			'    ComboBox11.Focus()
			'Else
			'    TxtKdBarang.Focus()
			'End If
		End If

	End Sub

	Private Sub TextBox15_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox15.KeyPress
		If e.KeyChar = Chr(13) Then TextBox16.Focus()
		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
	End Sub

	Private Sub ComboBox13_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox13.KeyPress
		If e.KeyChar = Chr(13) Then CmbJnsGudang.Focus()
	End Sub

	Private Sub CmbJnsGudang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbJnsGudang.KeyPress
		If e.KeyChar = Chr(13) Then ComboBox4.Focus()
		' If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)

	End Sub

	Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles btnPilihSatuan.Click
		SD_Pilih_Satuan_Turunan.txtSatuan.Text = cmbSatuan.Text
		SD_Pilih_Satuan_Turunan.ShowDialog()
	End Sub

	Private Sub DgvSatuanTerpilih_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DgvSatuanTerpilih.CellEndEdit
		Dim currentRow = DgvSatuanTerpilih.CurrentRow.Index
		Dim currentCell = DgvSatuanTerpilih.CurrentCellAddress.X

		If currentCell = 1 Then
			If IsNumeric(DgvSatuanTerpilih.Rows(currentRow).Cells(1).Value) = False Or Val(DgvSatuanTerpilih.Rows(currentRow).Cells(1).Value) < 0 Then
				DgvSatuanTerpilih.Rows(currentRow).Cells(1).Value = 0

				Exit Sub
			End If

		End If

	End Sub

	Private Sub DgvSatuanTerpilih_KeyDown(sender As Object, e As KeyEventArgs) Handles DgvSatuanTerpilih.KeyDown
		If DgvSatuanTerpilih.Rows.Count = 0 Or DgvSatuanTerpilih.SelectedCells.Count = 0 Then
			Exit Sub
		End If

		Dim currentRow = DgvSatuanTerpilih.CurrentRow.Index
		Dim currentCell = DgvSatuanTerpilih.CurrentCellAddress.X

		If e.KeyCode = Keys.Delete Then
			If Not DgvSatuanTerpilih.Rows.Count = 0 Then

				If DgvSatuanTerpilih.Rows(currentRow).Cells(0).Value = cmbSatuan.Text Then
					MessageBox.Show("Satuan awal tidak boleh dihapus ..!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

				BeginInvoke(New MethodInvoker(Sub() DgvSatuanTerpilih.Rows.RemoveAt(currentRow)))

			End If
		End If
	End Sub

	'Private Sub ComboBox13_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox13.KeyPress
	'    If e.KeyChar = Chr(13) Then ComboBox11.Focus()
	'End Sub

	Private Sub cmbSatuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSatuan.SelectedIndexChanged

		If cmbSatuan.SelectedIndex <> -1 Then
			btnPilihSatuan.Enabled = True

			Try
				OpenConn()

				DgvSatuanTerpilih.Rows.Clear()
				SQL = "select satuan_akhir,nilai_pengali, flag_general from EMI_Satuan_Detail_Perhitungan where kode_perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and satuan_awal = '" & cmbSatuan.Text & "' and satuan_akhir = '" & cmbSatuan.Text & "'  and jenis = 'masa' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						DgvSatuanTerpilih.Rows.Add(1)
						DgvSatuanTerpilih.Rows(0).Cells(0).Value = Dr("satuan_akhir")
						DgvSatuanTerpilih.Rows(0).Cells(1).Value = Dr("nilai_pengali")
						DgvSatuanTerpilih.Rows(0).Cells(3).Value = Dr("flag_general")

						DgvSatuanTerpilih.Rows(0).Cells(1).ReadOnly = True
						DgvSatuanTerpilih.Rows(0).Cells(1).Style.BackColor = Color.Yellow
					Else
						Dr.Close()
						CloseConn()
						MessageBox.Show("Satuan tidak ada")
						Exit Sub
					End If
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try

			Get_Lv_SatuanTurunan()
			If arrSatuanTurunan.Count > 0 Then

				ComboBox18.Items.Clear()
				For Each Satuan As String In arrSatuanTurunan
					ComboBox18.Items.Add(Satuan)
				Next

				ComboBox18.SelectedIndex = 0

			End If
		Else
			btnPilihSatuan.Enabled = False
		End If

		'If cmbSatuan.SelectedIndex <> -1 Then

		'    Try
		'        OpenConn()
		'        lvwSatuan.Visible = True
		'        lvwSatuan.Items.Clear()
		'        SQL = "select satuan_akhir from EMI_Satuan_Detail_Perhitungan where kode_perusahaan = '" & KodePerusahaan & "' "
		'        SQL = SQL & "and satuan_awal = '" & cmbSatuan.Text & "' and jenis = 'masa' "
		'        Using Dr = OpenTrans(SQL)
		'            Do While Dr.Read
		'                Dim lvw As ListViewItem
		'                lvw = lvwSatuan.Items.Add("")
		'                lvw.SubItems.Add(Dr("satuan_akhir"))
		'            Loop
		'        End Using

		'        CloseConn()

		'    Catch ex As Exception
		'        CloseConn()
		'        MessageBox.Show(ex.Message)
		'        Exit Sub
		'    End Try

		'End If
	End Sub

	Private Sub lvwSatuan_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles lvwSatuan.ItemChecked
		If lvwSatuan.Items.Count = 0 Or lvwSatuan.SelectedItems.Count = 0 Then
			Exit Sub
		End If

		If lvwSatuan.FocusedItem.Checked = True Then
			Dim indexcheck As Integer = lvwSatuan.FocusedItem.Index
			For index = 0 To lvwSatuan.Items.Count - 1
				If index <> indexcheck Then
					If lvwSatuan.Items(index).Checked = True Then
						lvwSatuan.Items(index).Checked = False
					End If

				End If
			Next
		End If
	End Sub

	Private Sub Cmb_KategoriGudang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_KategoriGudang.KeyPress
		If e.KeyChar = Chr(13) Then ComboBox5.Focus()
	End Sub

	Private Sub ComboBox19_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox19.KeyPress
		If e.KeyChar = Chr(13) Then TxtNama.Focus()
	End Sub

	Private Sub ComboBox11_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox11.SelectedIndexChanged
		If ComboBox11.SelectedIndex = -1 Then Exit Sub

		Try
			OpenConn()

			ComboBox19.Text = ""
			ComboBox19.Items.Clear()
			ComboBox19.SelectedIndex = -1

			If Button1.Text = "&Simpan" Then
				TxtKdBarang.Text = String.Empty
			End If

			arrId_Klasifikasi_Bahan2.Clear() : arrprefix_Klasifikasi_Bahan2.Clear()
			SQL = "select Id_Klasifikasi_Bahan2, Keterangan, Prefix_Klasifikasi_Bahan from EMI_Klasifikasi_Bahan2 "
			SQL = SQL & "where kode_perusahaan='" & KodePerusahaan & "' and Id_Klasifikasi_Bahan1='" & arrId_Klasifikasi_Bahan(ComboBox11.SelectedIndex) & "'"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					ComboBox19.Items.Add(Dr("Keterangan"))
					arrId_Klasifikasi_Bahan2.Add(Dr("Id_Klasifikasi_Bahan2"))
					arrprefix_Klasifikasi_Bahan2.Add(Dr("Prefix_Klasifikasi_Bahan"))
				Loop

			End Using

			ComboBox19.Enabled = True

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub TextBox9_KeyPress_1(sender As Object, e As KeyPressEventArgs) Handles txtStandarPrice.KeyPress
		If e.KeyChar = Chr(13) Then
			Button1.Focus()
		End If

		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
	End Sub

	'Private Sub ComboBox21_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_FlagPotongStok.KeyPress
	'    If e.KeyChar = Chr(13) Then
	'        If Cmb_FlagPotongStok.SelectedIndex = 0 Then
	'            Button1.Focus()
	'        Else
	'            txtStandarPrice.Focus()
	'        End If
	'    End If
	'End Sub

	Private Sub ComboBox21_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbKatJenisSub.KeyPress
		If e.KeyChar = Chr(13) Then CmbKatJenisSub1.Focus()
	End Sub

	Private Sub Cmb_FlagPotongStok_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_FlagPotongStok.SelectedIndexChanged
		If Cmb_FlagPotongStok.SelectedIndex = 0 Then
			txtStandarPrice.Enabled = False
			txtStandarPrice.Text = ""
		Else
			txtStandarPrice.Enabled = True
			txtStandarPrice.Text = ""
		End If
	End Sub

	Private Sub TextBox3_KeyPress_1(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
		If e.KeyChar = Chr(13) Then TextBox7.Focus()
		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
	End Sub

	Private Sub BtnCari_Click(sender As Object, e As EventArgs) Handles BtnCari.Click
		If CheckBox1.Checked = True Then
			If DateTimePicker1.Value > DateTimePicker2.Value Then
				MessageBox.Show("Tanggal mulai tidak boleh lebih dari tanggal selesai . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information)
				DateTimePicker1.Focus() : Exit Sub
			End If
		ElseIf CheckBox2.Checked = True Then
			If CmbSatuan_Kolom.SelectedIndex = -1 Then
				MessageBox.Show("Parameter filter harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information)
				CmbSatuan_Kolom.Focus() : Exit Sub
			ElseIf TxtSatuan_Value.Text.Trim.Length = 0 Then
				MessageBox.Show("Value filter harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information)
				TxtSatuan_Value.Focus() : Exit Sub
			End If
		End If

		Cari2("T")
	End Sub

	Private Sub Cmb_Kelompok_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Kelompok.SelectedIndexChanged

		If Cmb_Kelompok.SelectedIndex = -1 Then
			Cmb_Kd_Aktiva.Enabled = False
			Cmb_Kd_Aktiva.Text = "" : arrKodeAktiva.Clear()

			Exit Sub
		Else
			Cmb_Kd_Aktiva.Enabled = True
			Cmb_Kd_Aktiva.Text = ""

			Try
				OpenConn()

				Cmb_Kd_Aktiva.Items.Clear() : arrKodeAktiva.Clear()
				SQL = "select ID_Aktiva, Kode_Aktiva from N_EMI_Master_Aktiva_Lain "
				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "'  "
				Using Dr = OpenTrans(SQL)
					Do While Dr.Read
						Cmb_Kd_Aktiva.Items.Add(Dr("Kode_Aktiva")) : arrKodeAktiva.Add(CInt(Dr("ID_Aktiva")))
					Loop
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try

		End If
	End Sub

	Private Sub DgvSatuanTerpilih_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DgvSatuanTerpilih.KeyPress
		If e.KeyChar = Chr(13) Then ComboBox19.Focus()
	End Sub

	Private Sub TolakToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TolakToolStripMenuItem.Click
		If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedCells.Count = 0 Then
			Exit Sub
		End If

		Dim currentRow = DataGridView1.CurrentRow.Index
		Dim currentCell = DataGridView1.CurrentCellAddress.X

		xurut_departement = DataGridView1.CurrentRow.Cells(8).Value
		xFrom = DataGridView1.CurrentRow.Cells(16).Value

		If xFrom = "departement" Then
			xNoFakturPengajuanBrgBru = DataGridView1.CurrentRow.Cells(17).Value
		ElseIf xFrom = "warehouse" Then
			xNoFakturPengajuanBrgBru = DataGridView1.CurrentRow.Cells(0).Value
		End If

		'N_EMI_SD_Alasan_Tolak_Kode_Barang_Baru.xurut_departement = xurut_departement
		'N_EMI_SD_Alasan_Tolak_Kode_Barang_Baru.xFrom = xFrom
		'N_EMI_SD_Alasan_Tolak_Kode_Barang_Baru.xNoFakturPengajuanBarang = xNoFakturPengajuanBrgBru
		'N_EMI_SD_Alasan_Tolak_Kode_Barang_Baru.ShowDialog()
	End Sub

	Public Sub CmbKatJenisSub3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbKatJenisSub3.SelectedIndexChanged
		If CmbKatJenisSub3.SelectedIndex = -1 Or CmbKatJenisSub3.Text.Trim.Length = 0 Then
			Exit Sub
		End If

		If CmbKatJenisSub3.SelectedIndex = 0 Then

			N_EMI_Master_Kategori_Jenis_Sub_Kategori.ListView5.Columns.Clear()

			N_EMI_Master_Kategori_Jenis_Sub_Kategori.Asal_proses = "MASTER"
			N_EMI_Master_Kategori_Jenis_Sub_Kategori.kosong5()

			Dim IdKat = N_EMI_Master_Kategori_Jenis_Sub_Kategori.arrid4.IndexOf(arrKategoriJenis.Item(CmbKatJenis.SelectedIndex))
			N_EMI_Master_Kategori_Jenis_Sub_Kategori.CmbSK3_Jenis.SelectedIndex = IdKat

			Dim IdSub = N_EMI_Master_Kategori_Jenis_Sub_Kategori.arridsub4.IndexOf(arrSubKategoriJenis.Item(CmbKatJenisSub.SelectedIndex))
			N_EMI_Master_Kategori_Jenis_Sub_Kategori.ComboBox11_SelectedIndexChanged(CmbKatJenisSub3, e)
			N_EMI_Master_Kategori_Jenis_Sub_Kategori.CmbSK3_JenisSub.SelectedIndex = IdSub

			Dim IdSub1 = N_EMI_Master_Kategori_Jenis_Sub_Kategori.arrid2sub4.IndexOf(arrSubKategoriJenis1.Item(CmbKatJenisSub1.SelectedIndex))
			N_EMI_Master_Kategori_Jenis_Sub_Kategori.ComboBox12_SelectedIndexChanged(CmbKatJenisSub3, e)
			N_EMI_Master_Kategori_Jenis_Sub_Kategori.CmbSK3_JenisSub1.SelectedIndex = IdSub1

			Dim IdSub2 = N_EMI_Master_Kategori_Jenis_Sub_Kategori.arrid3sub4.IndexOf(arrSubKategoriJenis2.Item(CmbKatJenisSub2.SelectedIndex))
			N_EMI_Master_Kategori_Jenis_Sub_Kategori.ComboBox13_SelectedIndexChanged(CmbKatJenisSub3, e)
			N_EMI_Master_Kategori_Jenis_Sub_Kategori.CmbSK3_JenisSub2.SelectedIndex = IdSub2

			N_EMI_Master_Kategori_Jenis_Sub_Kategori.ShowDialog()
		Else
			Try
				OpenConn()

				If Button1.Text = "&Simpan" Then

					TxtKdBarang.Text = arrPrefix_Kategori.Item(CmbKatJenis.SelectedIndex) &
							arrPrefix_Sub_Kategori.Item(CmbKatJenisSub.SelectedIndex) &
							arrPrefix_Sub_Kategori1.Item(CmbKatJenisSub1.SelectedIndex) &
							arrPrefix_Sub_Kategori2.Item(CmbKatJenisSub2.SelectedIndex) &
							arrPrefix_Sub_Kategori3.Item(CmbKatJenisSub3.SelectedIndex)

					TxtNama.Text =
						CmbKatJenisSub1.Text & " " &
						CmbKatJenisSub2.Text & " " &
						CmbKatJenisSub3.Text & " "

				End If

				Dim flag_baru As String = "T"
				SQL = "select Kode_Barang from Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "Kode_Barang = '" & TxtKdBarang.Text & "' "
				Using dr = OpenTrans(SQL)
					If dr.Read Then
						flag_baru = "Y"
					Else
						flag_baru = "T"
					End If
				End Using

				Dim blobnamePath As String = ""
				Dim containerPath As String = ""

				SQL = "select Blob_Storage,Container From N_EMI_Katalog_Barang_Lain where   Kode_Perusahaan = '" & KodePerusahaan & "'  and Kode_Barang = '" & TxtKdBarang.Text & "' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						If General_Class.CekNULL(Dr("Blob_Storage")) = "" Then
							blobnamePath = ""
						Else
							blobnamePath = Dr("Blob_Storage")
						End If

						If General_Class.CekNULL(Dr("container")) = "" Then
							containerPath = ""
						Else
							containerPath = Dr("container")
						End If

					End If
				End Using

				Dim result = AzureHelper_EMI.DownloadFromAzure(
							containerPath,
							blobnamePath
						)

				If Not result.Success Then
					' ===== GAGAL / TIDAK ADA DATA =====
					PictureBox1.Image = Nothing
				Else
					' ===== ADA DATA =====
					PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
					PictureBox1.BorderStyle = BorderStyle.FixedSingle

					PictureBox1.Image = Nothing
					PictureBox1.Refresh()
					PictureBox1.LoadAsync(result.Url)

				End If

				If flag_baru = "T" Then
					SQL = "select a.Satuan, a.Stock_Minimum, a.Berat, a.Berat_Kotor, a.Panjang, a.Lebar, a.Tinggi, a.Metode_Pengeluaran_Stok "
					SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis_3 a where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
					SQL = SQL & "and a.Id_Sub_Kategori_Jenis_3 = '" & arrSubKategoriJenis3.Item(CmbKatJenisSub3.SelectedIndex) & "' "
					Using dr = OpenTrans(SQL)
						If dr.Read Then
							If General_Class.CekNULL(dr("Satuan")) = "" Then
								cmbSatuan.SelectedIndex = -1
								'cmbSatuan.Enabled = True
							Else
								cmbSatuan.Text = dr("Satuan")
								'cmbSatuan.Enabled = False
							End If

							If General_Class.CekNULL(dr("Metode_Pengeluaran_Stok")) = "" Then
								ComboBox16.SelectedIndex = -1
								'ComboBox16.Enabled = True
							Else
								ComboBox16.Text = dr("Metode_Pengeluaran_Stok")
								'ComboBox16.Enabled = False
							End If

							If General_Class.CekNULL(dr("Stock_Minimum")) = "" Then
								TextBox7.Text = ""
								'TextBox7.Enabled = True
							Else
								TextBox7.Text = dr("Stock_Minimum")
								'TextBox7.Enabled = False
							End If

							If General_Class.CekNULL(dr("Berat")) = "" Then
								TextBox12.Text = ""
								'TextBox12.Enabled = True
							Else
								TextBox12.Text = dr("Berat")
								'TextBox12.Enabled = False
							End If

							If General_Class.CekNULL(dr("Berat_Kotor")) = "" Then
								TextBox15.Text = ""
								'TextBox15.Enabled = True
							Else
								TextBox15.Text = dr("Berat_Kotor")
								'TextBox15.Enabled = False
							End If

							If General_Class.CekNULL(dr("Panjang")) = "" Then
								TextBox16.Text = ""
								'TextBox16.Enabled = True
							Else
								TextBox16.Text = dr("Panjang")
								'TextBox16.Enabled = False
							End If

							If General_Class.CekNULL(dr("Lebar")) = "" Then
								TextBox17.Text = ""
								'TextBox17.Enabled = True
							Else
								TextBox17.Text = dr("Lebar")
								'TextBox17.Enabled = False
							End If

							If General_Class.CekNULL(dr("Tinggi")) = "" Then
								TextBox18.Text = ""
								'TextBox18.Enabled = True
							Else
								TextBox18.Text = dr("Tinggi")
								'TextBox18.Enabled = False
							End If
						End If
					End Using
				End If

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try
			'TextBox1_Leave(ListView1, e)
			'Txtket.Focus()
		End If

	End Sub

	Private Sub lblPanjang_Click(sender As Object, e As EventArgs) Handles lblPanjang.Click

	End Sub

	Private Sub CmbKatJenisSub3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbKatJenisSub3.KeyPress
		If e.KeyChar = Chr(13) Then
			ComboBox2.Focus()
		End If
	End Sub

	Private Sub CmbKatJenisSub1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbKatJenisSub1.KeyPress
		If e.KeyChar = Chr(13) Then
			CmbKatJenisSub2.Focus()
		End If
	End Sub

	Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

	End Sub

	Private Sub TxtEstimasiPengiriman_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtEstimasiPengiriman.KeyPress
		If e.KeyChar = Chr(13) Then TextBox12.Focus()
		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
	End Sub

	Private Sub CmbKatJenisSub2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbKatJenisSub2.KeyPress
		If e.KeyChar = Chr(13) Then
			CmbKatJenisSub3.Focus()
		End If
	End Sub

	Private Sub cmbJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbJenis.SelectedIndexChanged
		'If cmbJenis.SelectedItem = "RAW MATERIAL" Then
		If arrGudangRawMaterial.Contains(arrJenisBarang(cmbJenis.SelectedIndex)) Then
			ComboBox11.Enabled = True
			ComboBox19.Enabled = True
			ComboBox19.Text = ""
			'TextBox1.Enabled = False
			ComboBox11.Focus()
		Else
			ComboBox11.Enabled = False
			ComboBox19.Enabled = False
			'TextBox1.Enabled = True
			ComboBox19.Items.Clear()
			ComboBox11.SelectedIndex = -1
		End If

		If Button1.Text = "&Simpan" And xdari <> "Datagridview1" Then
			TxtKdBarang.Text = String.Empty
		End If

		Try
			OpenConn()

			'=======================================
			'=     CEK APAKAH ASSET ATAU BUKAN     =
			'=======================================

			SQL = "select Flag_Asset, Flag_Sparepart, Flag_Peralatan "
			SQL = SQL & "from EMI_Group_Jenis_Lain "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and Id_Group_Jenis = '" & arrJenisBarang(cmbJenis.SelectedIndex) & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then

					If Dr("Flag_Asset") = "Y" Then
						Cmb_Kategori.Enabled = True
						Cmb_Kelompok.Enabled = True
						Cmb_Kd_Aktiva.Enabled = True
					Else
						Cmb_Kategori.Enabled = False
						Cmb_Kelompok.Enabled = False
						Cmb_Kd_Aktiva.Enabled = False
					End If
				Else
					Dr.Close()
					CloseConn()
					MessageBox.Show("Jenis Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			Cmb_Kategori.Items.Clear() : arrKategori.Clear()
			SQL = "select ID_Kategori, Keterangan "
			SQL = SQL & "from N_EMI_Master_Kategori_Barang_Lain "
			SQL = SQL & "where Kode_Perusahaan = '001' "
			SQL = SQL & "order by Kode_Kategori "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Kategori.Items.Add(Dr("Keterangan")) : arrKategori.Add(CInt(Dr("ID_Kategori")))
				Loop
			End Using

			Cmb_Kategori.SelectedIndex = -1 : Cmb_Kategori.Text = ""
			Cmb_Kelompok.Items.Clear()

			'Cmb_Kd_Aktiva.Items.Clear()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Cmb_Kategori_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Kategori.SelectedIndexChanged

		If Cmb_Kategori.SelectedIndex = -1 Then
			Cmb_Kelompok.Enabled = False
			Cmb_Kelompok.Text = "" : arrKelompok.Clear()
			Cmb_Kd_Aktiva.Enabled = False
			Cmb_Kd_Aktiva.Text = ""

			Exit Sub
		Else
			Cmb_Kelompok.Enabled = True
			Cmb_Kelompok.Text = ""

			Try
				OpenConn()

				Cmb_Kelompok.Items.Clear() : arrKelompok.Clear() : Cmb_Kd_Aktiva.Items.Clear() : arrKodeAktiva.Clear()
				SQL = "select ID_Kelompok, Keterangan "
				SQL = SQL & "from N_EMI_Master_Kelompok_Barang_Lain "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and ID_Kategori = '" & arrKategori(Cmb_Kategori.SelectedIndex) & "' "
				SQL = SQL & "order by Kode_Kelompok "
				Using Dr = OpenTrans(SQL)
					Do While Dr.Read
						Cmb_Kelompok.Items.Add(Dr("Keterangan")) : arrKelompok.Add(CInt(Dr("ID_Kelompok")))
					Loop
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try

		End If

	End Sub

	Private Sub ComboBox15_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox15.SelectedIndexChanged
		If ComboBox15.Items.Count = 0 Then Exit Sub

		If ComboBox15.SelectedIndex = 1 Then
			TextBox4.Enabled = False
		Else
			TextBox4.Enabled = True
		End If

		TextBox4.Text = ""

	End Sub

	Private Sub TextBox3_Leave(sender As Object, e As EventArgs) Handles TextBox3.Leave
		If Not IsNumeric(TextBox3.Text) Then TextBox3.Text = ""
	End Sub

	Private Sub TextBox4_Leave(sender As Object, e As EventArgs) Handles TextBox4.Leave
		If Not IsNumeric(TextBox4.Text) Then TextBox4.Text = ""
	End Sub

	Private Sub TextBox4_KeyPress_1(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
		If ComboBox15.SelectedIndex = -1 Then e.Handled = True : Exit Sub
		If e.KeyChar = Chr(13) Then TextBox7.Focus()
		If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
	End Sub

	Private Sub ComboBox11_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox11.KeyPress
		If e.KeyChar = Chr(13) Then ComboBox19.Focus()
	End Sub

	Private Sub CmbJnsGudanng_Leave(sender As Object, e As EventArgs) Handles CmbJnsGudang.Leave
		'If CmbJnsGudang.SelectedIndex = 2 Then
		'    ComboBox11.Enabled = True
		'    TextBox1.Enabled = False
		'    TextBox1.Text = ""
		'    ComboBox11.Focus()
		'Else
		'    ComboBox11.Enabled = False
		'    ComboBox11.SelectedIndex = -1
		'    TextBox1.Enabled = True
		'End If
	End Sub

	Private Sub CmbJnsGudanng_TextChanged(sender As Object, e As EventArgs) Handles CmbJnsGudang.TextChanged
		If CmbJnsGudang.SelectedIndex = -1 Then Exit Sub

		CmbJnsGudanng_Leave(CmbJnsGudang, e)
	End Sub

	Private Sub ComboBox19_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox19.SelectedIndexChanged
		If Button1.Text = "&Simpan" Then
			Try

				OpenConn()

#Region "KodeLama"

				'Dim No_Urut As String
				'SQL = "SELECT b.Prefix_Klasifikasi_Bahan, (MAX(RIGHT(a.Kode_Barang, 4)) + 1) AS No_Urut "
				'SQL = SQL & "FROM barang_lain a LEFT JOIN (SELECT Prefix_Klasifikasi_Bahan, id_klasifikasi_bahan2 FROM  EMI_Klasifikasi_Bahan2 WHERE "
				'SQL = SQL & "id_klasifikasi_bahan2 = '" & arrId_Klasifikasi_Bahan2(ComboBox19.SelectedIndex) & "') b ON a.id_klasifikasi_bahan2 = b.id_klasifikasi_bahan2 WHERE SUBSTRING('20210001', 3, 2) = b.Prefix_Klasifikasi_Bahan "
				'SQL = SQL & "GROUP BY b.Prefix_Klasifikasi_Bahan "
				'Using Dr = OpenTrans(SQL)
				'    If Dr.Read Then
				'        If IsDBNull(Dr("No_Urut")) Then
				'            No_Urut = "0001"
				'        Else
				'            No_Urut = Format(Dr("No_Urut"), "0###")
				'        End If
				'    Else
				'        No_Urut = "0001"
				'    End If

				'End Using

				'TextBox1.Text = arrprefix_Klasifikasi_Bahan.Item(ComboBox11.SelectedIndex) & arrprefix_Klasifikasi_Bahan2(ComboBox19.SelectedIndex) & No_Urut

#End Region

				Dim No_Urut As String
				'SQL = " select b.Prefix_Klasifikasi_Bahan, (max(right(a.Kode_Barang,4)) + 1) as No_Urut "
				'SQL = SQL & "from barang_lain a left join (select Prefix_Klasifikasi_Bahan, id_klasifikasi_bahan from emi_klasifikasi_bahan where id_klasifikasi_bahan = '" & arrId_Klasifikasi_Bahan.Item(ComboBox11.SelectedIndex) & "') b on a.id_klasifikasi_bahan = b.id_klasifikasi_bahan "
				'SQL = SQL & "where left(a.kode_barang,2) = b.Prefix_Klasifikasi_Bahan "
				'SQL = SQL & "group by b.Prefix_Klasifikasi_Bahan "

				SQL = " Select b.Prefix As Prefix_Klasifikasi_Bahan, (max(right(a.Kode_Barang,3)) + 1) As No_Urut "
				SQL = SQL & "From barang_lain a left Join ( "
				SQL = SQL & "Select a.Prefix_Klasifikasi_Bahan +''+b.Prefix_Klasifikasi_Bahan as Prefix, b.id_klasifikasi_bahan1 "
				SQL = SQL & "From EMI_Klasifikasi_Bahan a, EMI_Klasifikasi_Bahan2 b Where "
				SQL = SQL & "a.Id_Klasifikasi_Bahan_X = b.id_klasifikasi_bahan1 And "
				SQL = SQL & "b.Id_Klasifikasi_Bahan2_X = '" & arrId_Klasifikasi_Bahan.Item(ComboBox11.SelectedIndex) & "') b "
				SQL = SQL & "on a.Id_Klasifikasi_Bahan_X = b.id_klasifikasi_bahan1 Where Left(a.kode_barang, 4) = Prefix "
				SQL = SQL & "Group By b.Prefix "

				SQL = "select top(1) substring(kode_barang, 5, 3) as no_urut from barang_lain where kode_perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "Id_Klasifikasi_Bahan_X = '" & arrId_Klasifikasi_Bahan.Item(ComboBox11.SelectedIndex) & "' and "
				SQL = SQL & "Id_Klasifikasi_Bahan2_X = '" & arrId_Klasifikasi_Bahan2.Item(ComboBox19.SelectedIndex) & "' "
				SQL = SQL & "order by no_urut desc"

				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						'If IsDBNull(Dr("No_Urut")) Then
						'    No_Urut = "001"
						'Else
						'
						'End If
						No_Urut = Format(Val(Dr("No_Urut")) + 1, "000")
					Else
						No_Urut = "001"
					End If
					TxtKdBarang.Text = arrprefix_Klasifikasi_Bahan.Item(ComboBox11.SelectedIndex) & arrprefix_Klasifikasi_Bahan2.Item(ComboBox19.SelectedIndex) & No_Urut
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try
		End If
	End Sub

	Private Sub Master_Barang_New_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub Txtket_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txtket.KeyPress
		If e.KeyChar = Chr(13) Then TextBox7.Focus()
	End Sub

	Private Sub ComboBox16_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox16.KeyPress
		If e.KeyChar = Chr(13) Then Button1.Focus()
	End Sub

	Private Sub ComboBox20_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbKatJenis.SelectedIndexChanged
		If CmbKatJenis.SelectedIndex = -1 Or CmbKatJenis.Text.Trim.Length = 0 Then
			Exit Sub
		End If
		Try
			OpenConn()

			CmbKatJenisSub.Items.Clear() : arrSubKategoriJenis.Clear() : arrPrefix_Sub_Kategori.Clear()
			CmbKatJenisSub1.Items.Clear() : arrSubKategoriJenis1.Clear() : arrPrefix_Sub_Kategori1.Clear()
			CmbKatJenisSub2.Items.Clear() : arrSubKategoriJenis2.Clear() : arrPrefix_Sub_Kategori2.Clear()
			SQL = "select id_Sub_Kategori_Jenis, Keterangan, Prefix from N_EMI_Master_Sub_Kategori_Jenis "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and id_Kategori_Jenis = '" & arrKategoriJenis.Item(CmbKatJenis.SelectedIndex) & "' "
			SQL = SQL & "order by Keterangan "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbKatJenisSub.Items.Add(dr("Keterangan"))
					arrSubKategoriJenis.Add(dr("id_Sub_Kategori_Jenis"))
					arrPrefix_Sub_Kategori.Add(dr("Prefix"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub ComboBox20_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbKatJenis.KeyPress
		If e.KeyChar = Chr(13) Then CmbKatJenisSub.Focus()
	End Sub

	Private Sub ComboBox21_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbKatJenisSub.SelectedIndexChanged
		If CmbKatJenisSub.SelectedIndex = -1 Or CmbKatJenisSub.Text.Trim.Length = 0 Then
			Exit Sub
		End If
		Try
			OpenConn()

			CmbKatJenisSub1.Items.Clear() : arrSubKategoriJenis1.Clear() : arrPrefix_Sub_Kategori1.Clear()
			CmbKatJenisSub2.Items.Clear() : arrSubKategoriJenis2.Clear() : arrPrefix_Sub_Kategori2.Clear()
			CmbKatJenisSub3.Items.Clear() : arrSubKategoriJenis3.Clear() : arrPrefix_Sub_Kategori3.Clear()
			SQL = "select id_Sub_Kategori_Jenis_1, Keterangan, Prefix from N_EMI_Master_Sub_Kategori_Jenis_1 "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and Id_Sub_Kategori_Jenis = '" & arrSubKategoriJenis.Item(CmbKatJenisSub.SelectedIndex) & "' "
			SQL = SQL & "order by Keterangan "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbKatJenisSub1.Items.Add(dr("Keterangan"))
					arrSubKategoriJenis1.Add(dr("id_Sub_Kategori_Jenis_1"))
					arrPrefix_Sub_Kategori1.Add(dr("Prefix"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
		If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedCells.Count = 0 Then
			Exit Sub
		End If

		Dim currentRow = DataGridView1.CurrentRow.Index
		Dim currentCell = DataGridView1.CurrentCellAddress.X

		xNoFakturPengajuanBrgBru = DataGridView1.CurrentRow.Cells(0).Value
		xurut_departement = DataGridView1.CurrentRow.Cells(8).Value
		CmbKatJenis.Text = DataGridView1.CurrentRow.Cells(10).Value
		CmbKatJenisSub.Text = DataGridView1.CurrentRow.Cells(11).Value
		CmbKatJenisSub1.Text = DataGridView1.CurrentRow.Cells(12).Value
		CmbKatJenisSub2.Text = DataGridView1.CurrentRow.Cells(13).Value
		CmbKatJenisSub3.Text = DataGridView1.CurrentRow.Cells(14).Value
		xdari = "Datagridview1"
		xFrom = DataGridView1.CurrentRow.Cells(16).Value
		zNo_Pengajuan_Barang_Baru = DataGridView1.CurrentRow.Cells(17).Value
		CmbKatJenis.Enabled = True
		CmbKatJenisSub.Enabled = True
		CmbKatJenisSub1.Enabled = True
		CmbKatJenisSub2.Enabled = True
		CmbKatJenisSub3.Enabled = True
		'TxtNama.Text = DataGridView1.CurrentRow.Cells(3).Value
		cmbJenis.Focus()
		TabControl1.SelectedIndex = 0

	End Sub

	Private Sub ComboBox22_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbKatJenisSub1.SelectedIndexChanged
		If CmbKatJenisSub1.SelectedIndex = -1 Or CmbKatJenisSub1.Text.Trim.Length = 0 Then
			Exit Sub
		End If
		Try
			OpenConn()

			CmbKatJenisSub2.Items.Clear() : arrSubKategoriJenis2.Clear() : arrPrefix_Sub_Kategori2.Clear()
			CmbKatJenisSub3.Items.Clear() : arrSubKategoriJenis3.Clear() : arrPrefix_Sub_Kategori3.Clear()
			SQL = "select id_Sub_Kategori_Jenis_2, Keterangan, Prefix from N_EMI_Master_Sub_Kategori_Jenis_2 "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "'  "
			'  SQL = SQL & "and Kode_Sub_Kategori_Jenis = '" & arrSubKategoriJenis.Item(ComboBox21.SelectedIndex) & "' "
			SQL = SQL & "and Id_Sub_Kategori_Jenis_1 = '" & arrSubKategoriJenis1.Item(CmbKatJenisSub1.SelectedIndex) & "' "
			SQL = SQL & "order by Keterangan "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbKatJenisSub2.Items.Add(dr("Keterangan"))
					arrSubKategoriJenis2.Add(dr("id_Sub_Kategori_Jenis_2"))
					arrPrefix_Sub_Kategori2.Add(dr("Prefix"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Public Sub ComboBox23_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbKatJenisSub2.SelectedIndexChanged
		If CmbKatJenisSub2.SelectedIndex = -1 Or CmbKatJenisSub2.Text.Trim.Length = 0 Then
			Exit Sub
		End If
		Try
			OpenConn()

			CmbKatJenisSub3.Items.Clear() : arrSubKategoriJenis3.Clear() : arrPrefix_Sub_Kategori3.Clear()

			CmbKatJenisSub3.Items.Add("ADD New Item")
			arrSubKategoriJenis3.Add("-")
			arrPrefix_Sub_Kategori3.Add("-")

			SQL = "select id_Sub_Kategori_Jenis_3, Keterangan, Prefix from N_EMI_Master_Sub_Kategori_Jenis_3 "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "'  "
			'  SQL = SQL & "and Kode_Sub_Kategori_Jenis = '" & arrSubKategoriJenis.Item(ComboBox21.SelectedIndex) & "' "
			SQL = SQL & "and Id_Sub_Kategori_Jenis_2 = '" & arrSubKategoriJenis2.Item(CmbKatJenisSub2.SelectedIndex) & "' "
			SQL = SQL & "order by Keterangan "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					CmbKatJenisSub3.Items.Add(dr("Keterangan"))
					arrSubKategoriJenis3.Add(dr("id_Sub_Kategori_Jenis_3"))
					arrPrefix_Sub_Kategori3.Add(dr("Prefix"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub CmbKatJenisSub3_DrawItem(sender As Object, e As DrawItemEventArgs) Handles CmbKatJenisSub3.DrawItem
		If e.Index < 0 Then Return

		' Ambil teks item
		Dim itemText As String = CmbKatJenisSub3.Items(e.Index).ToString()

		' Tentukan warna & font berbeda untuk index 0
		Dim textBrush As Brush
		Dim itemFont As Font

		If e.Index = 0 Then
			textBrush = Brushes.Red
			itemFont = New Font(e.Font, FontStyle.Italic Or FontStyle.Bold)
		Else
			textBrush = Brushes.Black
			itemFont = e.Font
		End If

		' Gambar background item
		e.DrawBackground()
		' Gambar teks
		e.Graphics.DrawString(itemText, itemFont, textBrush, e.Bounds)
		' Gambar fokus
		e.DrawFocusRectangle()
	End Sub

End Class