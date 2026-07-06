Imports System.IO
Imports System.Net

Public Class insert_sql_to_mysql

	Dim arrId_Proyeks, arrKd_Brg, arrSO, arrNo_Rab, arrNo_Fak, arrEdit, arrHapus, arrKdSupplier, arrNo_Fak2, arrNoUrut As New ArrayList

	Private Sub insert_sql_to_mysql_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		ListView1.Columns.Add("Error", 160, HorizontalAlignment.Left)
		ListView1.View = View.Details

		'Dim Lvw As ListViewItem
		'Lvw = ListView1.Items.Add("Sql-MySql : Tes")
	End Sub

	Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrId_Proyeks.Clear()
			SQLMySQL = "select id from proyeks where flag_sdh_pindah_ke_sql is null order by id "

			Using DsMySQL = BindingTransMySQL(SQLMySQL)
				With DsMySQL.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrId_Proyeks.Add(.Rows(i).Item("id"))
					Next
				End With
			End Using

			SQLMySQL = "select id,kode_proyek,keterangan,target,tanggal_mulai,tanggal_selesai,lokasi_id "
			SQLMySQL = SQLMySQL & "from proyeks where flag_sdh_pindah_ke_sql is null order by id"
			Using DsMySQL = BindingTransMySQL(SQLMySQL)
				With DsMySQL.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						SQL = "insert into web_proyeks(id,kode_proyek,keterangan,target,tanggal_mulai,tanggal_selesai,lokasi_id) "
						SQL = SQL & "values ('" & .Rows(i).Item("id") & "','" & .Rows(i).Item("kode_proyek") & "',"
						SQL = SQL & "'" & .Rows(i).Item("keterangan") & "','" & .Rows(i).Item("target") & "','" & Format(.Rows(i).Item("tanggal_mulai"), "yyyy-MM-dd") & "',"
						SQL = SQL & "'" & Format(.Rows(i).Item("tanggal_selesai"), "yyyy-MM-dd") & "','" & .Rows(i).Item("lokasi_id") & "')"
						ExecuteTrans(SQL)
					Next
				End With
			End Using

			Dim j As Integer = 0
			For z As Integer = 1 To arrId_Proyeks.Count
				SQLMySQL = "update proyeks set flag_sdh_pindah_ke_sql = 'Y' WHERE id = " & arrId_Proyeks.Item(j).ToString & ""
				ExecuteTransMySQL(SQLMySQL)

				j = j + 1
			Next

			'MessageBox.Show("simpan berhasil", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "insert web proyek")
			Exit Sub
		End Try

	End Sub

	Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
		Try
			OpenConn()
			OpenConnMySQL()

			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrKd_Brg.Clear() : arrSO.Clear()
			SQL = "select Kode_Barang, Kode_Stock_Owner from Barang_Proyek where "
			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
			SQL = SQL & "flag_sdh_pindah is null"
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

				' ''SQL = "Select Kode_Perusahaan,Kode_Barang,Kode_Stock_Owner,Nama,'0',Satuan "
				' ''SQL = SQL & "from Barang_Proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				' ''SQL = SQL & "Kode_Stock_Owner = '" & arrSO.Item(j).ToString & "' and "
				' ''SQL = SQL & "Kode_Barang = '" & arrKd_Brg.Item(j).ToString & "' and "
				' ''SQL = SQL & "flag_sdh_pindah is null"
				' ''Using Ds = BindingTrans(SQL)
				' ''    With Ds.Tables("MyTable")
				' ''        For i As Integer = 0 To .Rows.Count - 1
				' ''            SQLMySQL = "insert into barang (kode_perusahaan,kode_barang,kode_stock_owner,nama,harga,satuan) "
				' ''            SQLMySQL = SQLMySQL & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("Kode_Barang") & "',"
				' ''            SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Kode_Stock_Owner") & "', '" & .Rows(i).Item("Nama") & "', "
				' ''            SQLMySQL = SQLMySQL & "'0', '" & .Rows(i).Item("Satuan") & "')"
				' ''            ExecuteTransMySQL(SQLMySQL)
				' ''        Next
				' ''    End With
				' ''End Using

				SQL = "Select Kode_Perusahaan,Kode_Barang,Kode_Stock_Owner,Nama,'0',Satuan,good_stock,kode_kategori_besar,kode_kategori_kecil "
				SQL = SQL & "from Barang_Proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "Kode_Stock_Owner = '" & arrSO.Item(j).ToString & "' and "
				SQL = SQL & "Kode_Barang = '" & arrKd_Brg.Item(j).ToString & "' and "
				SQL = SQL & "flag_sdh_pindah is null"
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							'Insert ke MySql
							SQLMySQL = "insert into barang (kode_perusahaan,kode_barang,kode_stock_owner,nama,harga,satuan,good_stock,kode_kategori_besar,kode_kategori_kecil) "
							SQLMySQL = SQLMySQL & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("Kode_Barang") & "',"
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Kode_Stock_Owner") & "', '" & .Rows(i).Item("Nama") & "', "
							SQLMySQL = SQLMySQL & "'0', '" & .Rows(i).Item("Satuan") & "', '" & .Rows(i).Item("good_stock") & "', '" & .Rows(i).Item("kode_kategori_besar") & "' , '" & .Rows(i).Item("kode_kategori_kecil") & "'  )"
							ExecuteTransMySQL(SQLMySQL)
							'Update Stock ke MySQL, kode_barang, kode_stock_owner, flag_sdh_pindah = 'Y'

						Next
					End With
				End Using

				SQL = "Update Barang_Proyek set flag_sdh_pindah = 'Y' where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
				SQL = SQL & "Kode_Stock_Owner = '" & arrSO.Item(j).ToString & "' and "
				SQL = SQL & "Kode_Barang = '" & arrKd_Brg.Item(j).ToString & "' "
				ExecuteTrans(SQL)

				j = j + 1
			Next

			'MessageBox.Show("simpan berhasil", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "insert update barang web")
			Exit Sub
		End Try
	End Sub

	Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrNo_Fak.Clear()
			SQL = "select no_faktur from Rab_Pembelian_Proyek where "
			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
			SQL = SQL & "flag_sdh_pindah is null"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrNo_Fak.Add(.Rows(i).Item("no_faktur"))
					Next
				End With
			End Using

			SQL = "select a.kode_perusahaan,a.no_faktur,a.no_nota,a.tanggal,a.jam,a.kode_supplier,a.jenis_transaksi,a.tgl_jatuh_tempo,a.flag_lunas,a.tgl_lunas,a.jam_lunas,a.userid,"
			SQL = SQL & "a.uservalidasi,a.disc1,a.disc2,a.status,a.terbilang,a.grand,a.kode_cb,a.lokasi,a.kode_voucher,a.ppn,a.nilai_ppn,a.nilai_sblm_ppn,a.tgl_input,a.sudah_fk,"
			SQL = SQL & "a.no_sementara,a.no_po,a.no_so,a.flag_sdh,a.sudah_cek,a.sudah_kirim,a.finish,a.validasi_cek,a.tgl_validasi_cek,a.jam_validasi_cek,a.user_validasi_cek,"
			SQL = SQL & "a.flag_opm,a.metode_stock,a.kode_proyek,a.pakai,a.pakaix,a.no_fak_pembelian,a.no_po_pemelian,a.flag_gabung,a.no_faktur_rab_gabungan,b.id "
			SQL = SQL & "from Rab_Pembelian_Proyek a,EMI_Proyek b where a.Kode_Perusahaan = b.Kode_Perusahaan and a.sub_pekerjaan_id = b.id and a.flag_sdh_pindah is null and a.kode_perusahaan  = '" & KodePerusahaan & "'"

			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						SQLMySQL = "insert into rab_pembelian_proyek (kode_perusahaan,no_faktur,no_nota,tanggal,jam,user_id,"
						SQLMySQL = SQLMySQL & "disc1,disc2,terbilang,grand,lokasi,kode_voucher,ppn,nilai_ppn,nilai_sblm_ppn,"
						SQLMySQL = SQLMySQL & "kode_proyek,flag_gabungan,sub_pekerjaan_id)"
						SQLMySQL = SQLMySQL & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "', '" & .Rows(i).Item("no_nota") & "', '" & Format(.Rows(i).Item("tanggal"), "yyyy-MM-dd") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("jam") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("userid") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("disc1") & "', '" & .Rows(i).Item("disc2") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("terbilang") & "', '" & .Rows(i).Item("grand") & "', '" & .Rows(i).Item("lokasi") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("kode_voucher") & "', '" & .Rows(i).Item("ppn") & "', '" & .Rows(i).Item("nilai_ppn") & "', '" & .Rows(i).Item("nilai_sblm_ppn") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("kode_proyek") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("flag_gabung") & "','" & .Rows(i).Item("id") & "')"
						ExecuteTransMySQL(SQLMySQL)
					Next
				End With
			End Using

			Dim j As Integer = 0

			For z As Integer = 1 To arrNo_Fak.Count

				SQL = "select kode_perusahaan,no_faktur,no_urut,kode_stock_owner,kode_barang,serial_number,keterangan,jumlah,harga,persen_diskon,"
				SQL = SQL & "nilai_diskon,x,hb_baru,hj_baru,flag_hb,flag_hj,hb_lama,hj_lama,userid_ubah,pakai_sn,expire,subtotal,pembelian,ambil from Detail_Rab_Pembelian_Proyek "
				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_faktur = '" & arrNo_Fak.Item(j).ToString & "'"

				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")

						For i As Integer = 0 To .Rows.Count - 1
							SQLMySQL = "insert into detail_rab_pembelian_proyek (kode_perusahaan,no_faktur,no_urut,kode_stock_owner,kode_barang,serial_number,keterangan,jumlah,harga,"
							SQLMySQL = SQLMySQL & "persen_diskon,nilai_diskon,x,hb_baru,hj_baru,flag_hb,flag_hj,hb_lama,hj_lama,userid_ubah,pakai_sn,expire,subtotal,pembelian,ambil) "
							SQLMySQL = SQLMySQL & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "', '" & .Rows(i).Item("no_urut") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("kode_stock_owner") & "', '" & .Rows(i).Item("kode_barang") & "', '" & .Rows(i).Item("serial_number") & "', '" & .Rows(i).Item("keterangan") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("jumlah") & "', '" & .Rows(i).Item("harga") & "', '" & .Rows(i).Item("persen_diskon") & "', '" & .Rows(i).Item("nilai_diskon") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("x") & "', '" & .Rows(i).Item("hb_baru") & "', '" & .Rows(i).Item("hj_baru") & "', '" & .Rows(i).Item("flag_hb") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("flag_hj") & "', '" & .Rows(i).Item("hb_lama") & "', '" & .Rows(i).Item("hj_lama") & "', '" & .Rows(i).Item("userid_ubah") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("pakai_sn") & "', '" & Format(.Rows(i).Item("expire"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("subtotal") & "', '" & .Rows(i).Item("pembelian") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("ambil") & "')"
							ExecuteTransMySQL(SQLMySQL)
						Next

					End With
				End Using

				SQL = "Update Rab_Pembelian_Proyek set flag_sdh_pindah = 'Y' where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
				SQL = SQL & "No_faktur = '" & arrNo_Fak.Item(j).ToString & "' "
				ExecuteTrans(SQL)

				j = j + 1
			Next

			'MessageBox.Show("simpan berhasil", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "insert rab web")
			Exit Sub
		End Try

	End Sub

	Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrId_Proyeks.Clear()

			SQLMySQL = "Select id from subproyeks where flag_sdh_pindah_ke_sql is null order by id "
			Using Ds = BindingTransMySQL(SQLMySQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrId_Proyeks.Add(.Rows(i).Item("id"))
					Next
				End With
			End Using

			SQLMySQL = "Select id,kode_sub_proyek,keterangan,lokasi_id,proyek_id "
			SQLMySQL = SQLMySQL & "from subproyeks where flag_sdh_pindah_ke_sql is null order by id"
			Using DsMySQL = BindingTransMySQL(SQLMySQL)
				With DsMySQL.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						SQL = "insert into web_subproyeks(id,kode_sub_proyek,keterangan,lokasi_id,proyek_id) "
						SQL = SQL & "values ('" & .Rows(i).Item("id") & "','" & .Rows(i).Item("kode_sub_proyek") & "',"
						SQL = SQL & "'" & .Rows(i).Item("keterangan") & "','" & .Rows(i).Item("lokasi_id") & "',"
						SQL = SQL & "'" & .Rows(i).Item("proyek_id") & "')"
						ExecuteTrans(SQL)
					Next
				End With
			End Using

			Dim j As Integer = 0
			For z As Integer = 1 To arrId_Proyeks.Count
				SQLMySQL = "update subproyeks set flag_sdh_pindah_ke_sql = 'Y' WHERE id = " & arrId_Proyeks.Item(j).ToString & ""
				ExecuteTransMySQL(SQLMySQL)

				j = j + 1
			Next

			'MessageBox.Show("simpan berhasil", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "insert web sub proyek")
			Exit Sub
		End Try
	End Sub

	Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrId_Proyeks.Clear()

			SQLMySQL = "select id from pekerjaans where flag_sdh_pindah_ke_sql is null order by id "

			Using DsMySQL = BindingTransMySQL(SQLMySQL)
				With DsMySQL.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrId_Proyeks.Add(.Rows(i).Item("id"))
					Next
				End With
			End Using

			SQLMySQL = "Select id,kode_pekerjaan,keterangan,lokasi_id,proyek_id,sub_proyek_id "
			SQLMySQL = SQLMySQL & "from pekerjaans where flag_sdh_pindah_ke_sql is null order by id "
			Using DsMySQL = BindingTransMySQL(SQLMySQL)
				With DsMySQL.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						SQL = "insert into web_pekerjaans(id,kode_pekerjaan,keterangan,lokasi_id,proyek_id,sub_proyek_id)"
						SQL = SQL & "values ('" & .Rows(i).Item("id") & "','" & .Rows(i).Item("kode_pekerjaan") & "',"
						SQL = SQL & "'" & .Rows(i).Item("keterangan") & "','" & .Rows(i).Item("lokasi_id") & "',"
						SQL = SQL & "'" & .Rows(i).Item("proyek_id") & "','" & .Rows(i).Item("sub_proyek_id") & "')"
						ExecuteTrans(SQL)
					Next
				End With
			End Using

			Dim j As Integer = 0
			For z As Integer = 1 To arrId_Proyeks.Count
				SQLMySQL = "update pekerjaans set flag_sdh_pindah_ke_sql = 'Y' WHERE id = " & arrId_Proyeks.Item(j).ToString & ""
				ExecuteTransMySQL(SQLMySQL)
				j = j + 1
			Next

			'MessageBox.Show("simpan berhasil", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "insert web pekerjaan")
			Exit Sub
		End Try
	End Sub

	Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrId_Proyeks.Clear()

			SQLMySQL = "select id from sub_pekerjaans where flag_sdh_pindah_ke_sql is null order by id"
			Using DsMySQL = BindingTransMySQL(SQLMySQL)
				With DsMySQL.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrId_Proyeks.Add(.Rows(i).Item("id"))
					Next
				End With
			End Using

			SQLMySQL = "select id,sub_pekerjaan,target,tanggal_mulai,tanggal_selesai,tanggal_mulai_real,tanggal_selesai_real,progress,id_user,tanggal,jam,flag_selesai,lokasi_id,proyek_id,sub_proyek_id,pekerjaan_id,persen_toleransi, bobot "
			SQLMySQL = SQLMySQL & "from sub_pekerjaans where flag_sdh_pindah_ke_sql is null order by id"
			Using DsMySQL = BindingTransMySQL(SQLMySQL)
				With DsMySQL.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						SQL = "insert into emi_proyek(kode_perusahaan,kode_proyek,keterangan,id,lokasi_id,proyek_id,"
						SQL = SQL & "sub_proyek_id,pekerjaan_id,tanggal_mulai,tanggal_selesai, bobot)"
						SQL = SQL & "values ('" & KodePerusahaan & "','" & .Rows(i).Item("sub_pekerjaan") & "','" & .Rows(i).Item("sub_pekerjaan") & "','" & .Rows(i).Item("id") & "',"
						SQL = SQL & "'" & .Rows(i).Item("lokasi_id") & "','" & .Rows(i).Item("proyek_id") & "',"
						SQL = SQL & "'" & .Rows(i).Item("sub_proyek_id") & "','" & .Rows(i).Item("pekerjaan_id") & "',"
						'SQL = SQL & "'" & .Rows(i).Item("target") & "',"
						SQL = SQL & "'" & Format(.Rows(i).Item("tanggal_mulai"), "yyyy-MM-dd") & "','" & Format(.Rows(i).Item("tanggal_selesai"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("bobot") & "')"
						'SQL = SQL & "'" & Format(.Rows(i).Item("tanggal_mulai_real"), "yyyy-MM-dd") & "','" & Format(.Rows(i).Item("tanggal_selesai_real"), "yyyy-MM-dd") & "',"
						'SQL = SQL & "'" & .Rows(i).Item("progress") & "','" & .Rows(i).Item("id_user") & "',"
						'SQL = SQL & "'" & Format(.Rows(i).Item("tanggal"), "yyyy-MM-dd") & "','" & .Rows(i).Item("jam") & "')"
						ExecuteTrans(SQL)
					Next
				End With
			End Using

			Dim j As Integer = 0
			For z As Integer = 1 To arrId_Proyeks.Count
				SQLMySQL = "update sub_pekerjaans set flag_sdh_pindah_ke_sql = 'Y' WHERE id = " & arrId_Proyeks.Item(j).ToString & ""
				ExecuteTransMySQL(SQLMySQL)
				j = j + 1
			Next

			'MessageBox.Show("simpan berhasil", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "insert web sub pekerjaan")
			Exit Sub
		End Try
	End Sub

	Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrNo_Fak.Clear()
			SQLMySQL = "select no_faktur from request_material where flag_sdh_pindah_ke_sql is null and flag_acc = 'Y' order by no_faktur"
			Using DsMySQL = BindingTransMySQL(SQLMySQL)
				With DsMySQL.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrNo_Fak.Add(.Rows(i).Item("no_faktur"))
					Next
				End With
			End Using

			SQLMySQL = "select no_faktur,sub_proyek_id, tanggal, jam, user_id, flag_acc, tanggal_acc, jam_acc, tanggal_pemakaian, keterangan, user_id_acc "
			SQLMySQL = SQLMySQL & "from request_material where flag_sdh_pindah_ke_sql is null and flag_acc = 'Y' order by no_faktur "

			Using DsMySQL = BindingTransMySQL(SQLMySQL)
				With DsMySQL.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1

						SQL = "insert into request_material(no_faktur, sub_proyek_id, tanggal, jam, user_id, flag_acc, "
						SQL = SQL & "tanggal_acc, jam_acc, tanggal_pemakaian, keterangan, user_acc) "
						SQL = SQL & "values ('" & .Rows(i).Item("no_faktur") & "','" & .Rows(i).Item("sub_proyek_id") & "','" & Format(.Rows(i).Item("tanggal"), "yyyy-MM-dd") & "','" & .Rows(i).Item("jam") & "', "
						SQL = SQL & "'" & .Rows(i).Item("user_id") & "', '" & .Rows(i).Item("flag_acc") & "', "
						'SQL = SQL & "'" & Format(.Rows(i).Item("tanggal_acc"), "yyyy-MM-dd") & "','" & .Rows(i).Item("jam_acc") & "', "
						If General_Class.CekNULL(.Rows(i).Item("tanggal_acc")) = "" Then
							SQL = SQL & "NULL, "
						Else
							SQL = SQL & "'" & Format(.Rows(i).Item("tanggal_acc"), "yyyy-MM-dd") & "', "
						End If

						If General_Class.CekNULL(.Rows(i).Item("jam_acc")) = "" Then
							SQL = SQL & "NULL, "
						Else
							SQL = SQL & "'" & .Rows(i).Item("jam_acc") & "', "
						End If

						If General_Class.CekNULL(.Rows(i).Item("tanggal_pemakaian")) = "" Then
							SQL = SQL & "NULL, "
						Else
							SQL = SQL & "'" & Format(.Rows(i).Item("tanggal_pemakaian"), "yyyy-MM-dd") & "', "
						End If

						If General_Class.CekNULL(.Rows(i).Item("keterangan")) = "" Then
							SQL = SQL & "NULL, "
						Else
							SQL = SQL & "'" & .Rows(i).Item("keterangan") & "', "
						End If

						'SQL = SQL & "'" & Format(.Rows(i).Item("tanggal_pemakaian"), "yyyy-MM-dd") & "','" & .Rows(i).Item("keterangan") & "', "
						SQL = SQL & "'" & .Rows(i).Item("user_id_acc") & "')"
						ExecuteTrans(SQL)
					Next
				End With
			End Using

			Dim j As Integer = 0
			For z As Integer = 1 To arrNo_Fak.Count
				SQLMySQL = "select no_faktur,kode_stock_owner, kode_barang, jumlah, satuan, keterangan, id "
				SQLMySQL = SQLMySQL & "from request_material_detail "
				SQLMySQL = SQLMySQL & "where no_faktur = '" & arrNo_Fak.Item(j).ToString & "'"

				Using DsMySQL = BindingTransMySQL(SQLMySQL)
					With DsMySQL.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							SQL = "insert into request_material_detail (no_faktur, kode_stock_owner, kode_barang, jumlah, "
							SQL = SQL & "satuan, keterangan, id) "
							SQL = SQL & "values ('" & .Rows(i).Item("no_faktur") & "','" & .Rows(i).Item("kode_stock_owner") & "',"
							SQL = SQL & "'" & .Rows(i).Item("kode_barang") & "','" & .Rows(i).Item("jumlah") & "',"
							SQL = SQL & "'" & .Rows(i).Item("satuan") & "','" & .Rows(i).Item("keterangan") & "','" & .Rows(i).Item("id") & "')"
							ExecuteTrans(SQL)
						Next
					End With
				End Using

				SQLMySQL = "Update request_material set flag_sdh_pindah_ke_sql = 'Y' where "
				SQLMySQL = SQLMySQL & "no_faktur = '" & arrNo_Fak.Item(j).ToString & "' "
				ExecuteTransMySQL(SQLMySQL)

				j = j + 1
			Next

			'MessageBox.Show("simpan berhasil", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "insert request material")
			Exit Sub
		End Try
	End Sub

	Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click

		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrEdit.Clear()
			SQL = "select no_faktur from Rab_Pembelian_Proyek where "
			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
			SQL = SQL & "flag_sdh_pindah = 'Y' and Flag_edit = 'Y'"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrEdit.Add(.Rows(i).Item("no_faktur"))
					Next
				End With
			End Using

			Dim a As Integer = 0
			For b As Integer = 1 To arrEdit.Count
				SQLMySQL = "delete from rab_pembelian_proyek where No_faktur = '" & arrEdit.Item(a).ToString & "' "
				ExecuteTransMySQL(SQLMySQL)

				SQLMySQL = "delete from detail_rab_pembelian_proyek where No_faktur = '" & arrEdit.Item(a).ToString & "' "
				ExecuteTransMySQL(SQLMySQL)

				SQL = "select a.kode_perusahaan,a.no_faktur,a.no_nota,a.tanggal,a.jam,a.kode_supplier,a.jenis_transaksi,a.tgl_jatuh_tempo,a.flag_lunas,a.tgl_lunas,a.jam_lunas,a.userid,"
				SQL = SQL & "a.uservalidasi,a.disc1,a.disc2,a.status,a.terbilang,a.grand,a.kode_cb,a.lokasi,a.kode_voucher,a.ppn,a.nilai_ppn,a.nilai_sblm_ppn,a.tgl_input,a.sudah_fk,"
				SQL = SQL & "a.no_sementara,a.no_po,a.no_so,a.flag_sdh,a.sudah_cek,a.sudah_kirim,a.finish,a.validasi_cek,a.tgl_validasi_cek,a.jam_validasi_cek,a.user_validasi_cek,"
				SQL = SQL & "a.flag_opm,a.metode_stock,a.kode_proyek,a.pakai,a.pakaix,a.no_fak_pembelian,a.no_po_pemelian,a.flag_gabung,a.no_faktur_rab_gabungan,b.id, a.Keterangan_Perubahan_RAB "
				SQL = SQL & "from Rab_Pembelian_Proyek a,EMI_Proyek b where a.Kode_Perusahaan = b.Kode_Perusahaan and a.sub_pekerjaan_id = b.id and a.flag_sdh_pindah = 'Y' and Flag_edit = 'Y' and a.kode_perusahaan  = '" & KodePerusahaan & "' "
				SQL = SQL & "and no_faktur = '" & arrEdit.Item(a).ToString & "' "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							Dim keterangan_perubahan_rab As String = "NULL"
							If General_Class.CekNULL((.Rows(i).Item("Keterangan_Perubahan_RAB"))) <> "" Then
								keterangan_perubahan_rab = "'" & .Rows(i).Item("Keterangan_Perubahan_RAB") & "' "
							End If

							SQLMySQL = "insert into rab_pembelian_proyek (kode_perusahaan,no_faktur,no_nota,tanggal,jam,user_id,"
							SQLMySQL = SQLMySQL & "disc1,disc2,terbilang,grand,lokasi,kode_voucher,ppn,nilai_ppn,nilai_sblm_ppn,"
							SQLMySQL = SQLMySQL & "kode_proyek,flag_gabungan,sub_pekerjaan_id, keterangan_perubahan_rab)"
							SQLMySQL = SQLMySQL & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "', '" & .Rows(i).Item("no_nota") & "', '" & Format(.Rows(i).Item("tanggal"), "yyyy-MM-dd") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("jam") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("userid") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("disc1") & "', '" & .Rows(i).Item("disc2") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("terbilang") & "', '" & .Rows(i).Item("grand") & "', '" & .Rows(i).Item("lokasi") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("kode_voucher") & "', '" & .Rows(i).Item("ppn") & "', '" & .Rows(i).Item("nilai_ppn") & "', '" & .Rows(i).Item("nilai_sblm_ppn") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("kode_proyek") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("flag_gabung") & "','" & .Rows(i).Item("id") & "', " & keterangan_perubahan_rab & ")"
							ExecuteTransMySQL(SQLMySQL)
						Next
					End With
				End Using

				SQL = "select kode_perusahaan,no_faktur,no_urut,kode_stock_owner,kode_barang,serial_number,keterangan,jumlah,harga,persen_diskon,"
				SQL = SQL & "nilai_diskon,x,hb_baru,hj_baru,flag_hb,flag_hj,hb_lama,hj_lama,userid_ubah,pakai_sn,expire,subtotal,pembelian,ambil from Detail_Rab_Pembelian_Proyek "
				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_faktur = '" & arrEdit.Item(a).ToString & "'"
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							SQLMySQL = "insert into detail_rab_pembelian_proyek (kode_perusahaan,no_faktur,no_urut,kode_stock_owner,kode_barang,serial_number,keterangan,jumlah,harga,"
							SQLMySQL = SQLMySQL & "persen_diskon,nilai_diskon,x,hb_baru,hj_baru,flag_hb,flag_hj,hb_lama,hj_lama,userid_ubah,pakai_sn,expire,subtotal,pembelian,ambil) "
							SQLMySQL = SQLMySQL & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "', '" & .Rows(i).Item("no_urut") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("kode_stock_owner") & "', '" & .Rows(i).Item("kode_barang") & "', '" & .Rows(i).Item("serial_number") & "', '" & .Rows(i).Item("keterangan") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("jumlah") & "', '" & .Rows(i).Item("harga") & "', '" & .Rows(i).Item("persen_diskon") & "', '" & .Rows(i).Item("nilai_diskon") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("x") & "', '" & .Rows(i).Item("hb_baru") & "', '" & .Rows(i).Item("hj_baru") & "', '" & .Rows(i).Item("flag_hb") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("flag_hj") & "', '" & .Rows(i).Item("hb_lama") & "', '" & .Rows(i).Item("hj_lama") & "', '" & .Rows(i).Item("userid_ubah") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("pakai_sn") & "', '" & Format(.Rows(i).Item("expire"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("subtotal") & "', '" & .Rows(i).Item("pembelian") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("ambil") & "')"
							ExecuteTransMySQL(SQLMySQL)
						Next
					End With
				End Using

				SQL = "Update Rab_Pembelian_Proyek set flag_edit = null where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
				SQL = SQL & "No_faktur = '" & arrEdit.Item(a).ToString & "' "
				ExecuteTrans(SQL)
				a = a + 1
			Next

			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "edit rab web dari sql server ")
			Exit Sub
		End Try
		'MessageBox.Show("simpan berhasil", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

	End Sub

	Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

		'Button1_Click(Timer1, e)
		'Button2_Click(Timer1, e)
		'Button3_Click(Timer1, e)
		'Button4_Click(Timer1, e)
		'Button5_Click(Timer1, e)
		'Button6_Click(Timer1, e)
		'Button7_Click(Timer1, e)
		'Button8_Click(Timer1, e)
		'Button9_Click(Timer1, e)
		'Button10_Click(Timer1, e)
		'Button11_Click(Timer1, e)
		'Button12_Click(Timer1, e)
		'Button13_Click(Timer1, e)
		'Button14_Click(Timer1, e)
		'Button15_Click(Timer1, e)
		'Button16_Click(Timer1, e)
		'Button17_Click(Timer1, e)
		'Button18_Click(Timer1, e)
		'Button19_Click(Timer1, e)
		'Button20_Click(Timer1, e)
		'Button21_Click(Timer1, e)
		'btn_KeBrgMskMYSQL_Click(Timer1, e)

		Button22_Click(Timer1, e)
		Button23_Click(Timer1, e)
	End Sub

	Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
		GetTime()
		Dim ada_data As Boolean = True
		Try
			OpenConn()

			SQL = "select * from Update_Barang_Web_Log_Harian where Tanggal = '" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "' "
			Using Dr = OpenTrans(SQL)
				If Not Dr.Read Then
					ada_data = False
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		If ada_data = False Then
			Dim jamSekarang As String = Tanggal_Sekarang.ToString("HH:mm:ss")
			Dim waktuSekarang As TimeSpan = TimeSpan.Parse(jamSekarang)

			Dim batasBawah As TimeSpan = New TimeSpan(0, 0, 0) ' 00:00:00
			Dim batasAtas As TimeSpan = New TimeSpan(5, 0, 0)  ' 05:00:00

			If waktuSekarang >= batasBawah AndAlso waktuSekarang < batasAtas Then
				Sync_Harian_Click(Timer2, e)
			End If

		End If

	End Sub

	Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrKdSupplier.Clear()
			SQL = "select kode_supplier from suppliers where "
			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Kategori = 'NONE' "
			SQL = SQL & "and flag_sdh_pindah is null"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrKdSupplier.Add(.Rows(i).Item("kode_supplier"))
					Next
				End With
			End Using

			SQL = "Select kode_perusahaan, kode_supplier, nama "
			SQL = SQL & "from suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Kategori = 'NONE' and "
			SQL = SQL & "flag_sdh_pindah is null"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						SQLMySQL = "insert into suppliers (kode_perusahaan,kode_supplier,nama) "
						SQLMySQL = SQLMySQL & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("kode_supplier") & "',"
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Nama") & "') "
						ExecuteTransMySQL(SQLMySQL)
					Next
				End With
			End Using

			Dim j As Integer = 0
			For z As Integer = 1 To arrKdSupplier.Count
				SQL = "Update suppliers set flag_sdh_pindah = 'Y' where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
				SQL = SQL & "kode_supplier = '" & arrKdSupplier.Item(j).ToString & "'"
				ExecuteTrans(SQL)

				j = j + 1
			Next

			'MessageBox.Show("simpan berhasil", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "insert supplier")
			Exit Sub
		End Try
	End Sub

	Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click

		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrNo_Fak.Clear()
			SQL = "select no_faktur from penjualan_proyek where "
			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and flag_sdh_pindah is null and status is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrNo_Fak.Add(.Rows(i).Item("no_faktur"))
					Next
				End With
			End Using

			SQL = "Select Kode_Perusahaan,No_Faktur,Tanggal,Jam,Kode_Customer,Jenis_Transaksi,Tgl_Jatuh_Tempo, "
			SQL = SQL & "Flag_Lunas,Tgl_Lunas,Jam_Lunas,Flag_Lunas_Tunai,Tgl_Lunas_Tunai,Jam_Lunas_Tunai,UserValidasi_Tunai,"
			SQL = SQL & "UserID,UserValidasi,Disc1,Disc2,Disc_Cash,Status,Terbilang,Grand,Bayar,"
			SQL = SQL & "Kode_Sales,PPN,Pembeda,Total_Point,Flag_Lipat,Kurs,Pakai_Point,Diskon_Promo,Diskon_Rupiah,Flag_Tagihan,"
			SQL = SQL & "Kode_CB,RV,No_Surat_Jalan,Lokasi,Lokasi_Gdg,Total,Total_U_Dis_Member,Hasil_Diskon,Hasil_Diskon_Cash,"
			SQL = SQL & "Kode_Voucher_1,Kode_Voucher_2,Kode_Voucher_3,Kode_Voucher_4,Kode_Voucher_5,Kode_Voucher_6,Kode_Voucher_7,Kode_Voucher_8,"
			SQL = SQL & "Jenis,Nilai_PPN,No_PO_Toko,No_DO,Kode_Karyawan,Flag_Cabang_Sendiri,Flag_Cabang_Agency,COA_Piutang,Sudah_FK,"
			SQL = SQL & "No_Fak_Sebelumnya,No_Fak_Setelahnya,Init_Custm,Ket_Custm,No_KB,Jns,Akun_Kas,Akun_Piutang,Akun_Piutang_Sementara,Disc_Tambahan,Hasil_Disc_Tambahan,Harus_Retur_Semua,"
			SQL = SQL & "Persen_Insentif_1,Persen_Insentif_2,Nilai_Insentif_1,Nilai_Insentif_2,Akun_Biaya_Insentif_1,Akun_Biaya_Insentif_2,Akun_Hutang_Insentif_1,Akun_Hutang_Insentif_2,"
			SQL = SQL & "Kepala,Sudah_Voucher,Tanggal_Voucher,Jam_Voucher,User_Voucher,Sudah_Upload,Sudah_Kirim,Flag_DO_Selesai,"
			SQL = SQL & "No_Faktur_Pajak,Tgl_Faktur_Pajak,Jam_Faktur_Pajak,User_Faktur_Pajak,Subtotal_Faktur_Pajak,PPN_Faktur_Pajak,Grand_Faktur_Pajak,"
			SQL = SQL & "Val_Diskon_Cash,Tgl_Jurnal_Diskon_Cash,Tgl_Val_Diskon_Cash,Jam_Val_Diskon_Cash,User_Val_Diskon_Cash,"
			SQL = SQL & "Nilai_Val_Diskon_Cash,PPN_Val_Diskon_Cash,Ket_Val_Diskon_Cash,CB_Val_Diskon_Cash,Akun_Val_Diskon_Cash,Kode_Voucher_Diskon_Cash,"
			SQL = SQL & "zzz,z1,Diskon_Sementara,Nilai_Diskon_Sementara,Harus_Diupdate,Lama_Diskon_Sementara,Metode_Pot_Stock,"
			SQL = SQL & "Flag_Opm,Flag_Sudah_Ke_Pusat,Flag_Sementara_Saat_Opm,Flag_ACC_Plafon,User_ACC_Plafon,Harus_Updtttt,Hrs_Updatex,Nfpx,"
			SQL = SQL & "Total_Stlh_Diskon,metode_budgeting,Mulai_Deskcall,tgl_input,Flag_Lunas_tes,Flag_Lunas_Tunai_tes,Kode_Sub_Pekerjaan,Id_Sub_Pekerjaan, keterangan "
			SQL = SQL & "from penjualan_proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null and status is null "

			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						' ''SQLMySQL = "insert into penjualan_proyek (Kode_Perusahaan,No_Faktur,Tanggal,Jam,Kode_Customer,Jenis_Transaksi,Tgl_Jatuh_Tempo,"
						' ''SQLMySQL = SQLMySQL & "Flag_Lunas,Tgl_Lunas,Jam_Lunas,Flag_Lunas_Tunai,Tgl_Lunas_Tunai,Jam_Lunas_Tunai,UserValidasi_Tunai,"
						' ''SQLMySQL = SQLMySQL & "UserID,UserValidasi,Disc1,Disc2,Disc_Cash,Status,Terbilang,Grand,Bayar,"
						' ''SQLMySQL = SQLMySQL & "Kode_Sales,PPN,Pembeda,Total_Point,Flag_Lipat,Kurs,Pakai_Point,Diskon_Promo,Diskon_Rupiah,Flag_Tagihan,"
						' ''SQLMySQL = SQLMySQL & "Kode_CB,RV,No_Surat_Jalan,Lokasi,Lokasi_Gdg,Total,Total_U_Dis_Member,Hasil_Diskon,Hasil_Diskon_Cash,"
						' ''SQLMySQL = SQLMySQL & "Kode_Voucher_1,Kode_Voucher_2,Kode_Voucher_3,Kode_Voucher_4,Kode_Voucher_5,Kode_Voucher_6,Kode_Voucher_7,Kode_Voucher_8,"
						' ''SQLMySQL = SQLMySQL & "Jenis,Nilai_PPN,No_PO_Toko,No_DO,Kode_Karyawan,Flag_Cabang_Sendiri,Flag_Cabang_Agency,COA_Piutang,Sudah_FK,"
						' ''SQLMySQL = SQLMySQL & "No_Fak_Sebelumnya,No_Fak_Setelahnya,Init_Custm,Ket_Custm,No_KB,Jns,Akun_Kas,Akun_Piutang,Akun_Piutang_Sementara,Disc_Tambahan,Hasil_Disc_Tambahan,Harus_Retur_Semua,"
						' ''SQLMySQL = SQLMySQL & "Persen_Insentif_1,Persen_Insentif_2,Nilai_Insentif_1,Nilai_Insentif_2,Akun_Biaya_Insentif_1,Akun_Biaya_Insentif_2,Akun_Hutang_Insentif_1,Akun_Hutang_Insentif_2,"
						' ''SQLMySQL = SQLMySQL & "Kepala,Sudah_Voucher,Tanggal_Voucher,Jam_Voucher,User_Voucher,Sudah_Upload,Sudah_Kirim,Flag_DO_Selesai,"
						' ''SQLMySQL = SQLMySQL & "No_Faktur_Pajak,Tgl_Faktur_Pajak,Jam_Faktur_Pajak,User_Faktur_Pajak,Subtotal_Faktur_Pajak,PPN_Faktur_Pajak,Grand_Faktur_Pajak,"
						' ''SQLMySQL = SQLMySQL & "Val_Diskon_Cash,Tgl_Jurnal_Diskon_Cash,Tgl_Val_Diskon_Cash,Jam_Val_Diskon_Cash,User_Val_Diskon_Cash,"
						' ''SQLMySQL = SQLMySQL & "Nilai_Val_Diskon_Cash,PPN_Val_Diskon_Cash,Ket_Val_Diskon_Cash,CB_Val_Diskon_Cash,Akun_Val_Diskon_Cash,Kode_Voucher_Diskon_Cash,"
						' ''SQLMySQL = SQLMySQL & "zzz,z1,Diskon_Sementara,Nilai_Diskon_Sementara,Harus_Diupdate,Lama_Diskon_Sementara,Metode_Pot_Stock,"
						' ''SQLMySQL = SQLMySQL & "Flag_Opm,Flag_Sudah_Ke_Pusat,Flag_Sementara_Saat_Opm,Flag_ACC_Plafon,User_ACC_Plafon,Harus_Updtttt,Hrs_Updatex,Nfpx,"
						' ''SQLMySQL = SQLMySQL & "Total_Stlh_Diskon,metode_budgeting,Mulai_Deskcall,tgl_input,Flag_Lunas_tes,Flag_Lunas_Tunai_tes,Kode_Sub_Pekerjaan,Id_Sub_Pekerjaan)"
						SQLMySQL = "insert into penjualan_proyek (kode_perusahaan,no_faktur,tanggal,jam, "
						SQLMySQL = SQLMySQL & "userid,disc_cash,lokasi,hasil_diskon_cash, "
						SQLMySQL = SQLMySQL & "kode_voucher_2,flag_cabang_agency,disc_tambahan,hasil_disc_tambahan, "
						SQLMySQL = SQLMySQL & "persen_insentif_1,persen_insentif_2,nilai_insentif_1,nilai_insentif_2, "
						SQLMySQL = SQLMySQL & "nilai_val_diskon_cash,diskon_sementara,nilai_diskon_sementara,lama_diskon_sementara, "
						SQLMySQL = SQLMySQL & "metode_pot_stock,tgl_input,sub_pekerjaan_id, keterangan)"

						'SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Kode_Perusahaan") & "', '" & .Rows(i).Item("No_Faktur") & "', '" & .Rows(i).Item("Tanggal") & "', '" & .Rows(i).Item("Jam") & "', '" & .Rows(i).Item("Kode_Customer") & "', '" & .Rows(i).Item("Jenis_Transaksi") & "', '" & .Rows(i).Item("Tgl_Jatuh_Tempo") & "', "
						'SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Flag_Lunas") & "', '" & .Rows(i).Item("Tgl_Lunas") & "', '" & .Rows(i).Item("Jam_Lunas") & "', '" & .Rows(i).Item("Flag_Lunas_Tunai") & "', '" & .Rows(i).Item("Tgl_Lunas_Tunai") & "', '" & .Rows(i).Item("Jam_Lunas_Tunai") & "', '" & .Rows(i).Item("UserValidasi_Tunai") & "', "
						'SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("UserID") & "', '" & .Rows(i).Item("UserValidasi") & "', '" & .Rows(i).Item("Disc1") & "', '" & .Rows(i).Item("Disc2") & "', '" & .Rows(i).Item("Disc_Cash") & "', '" & .Rows(i).Item("Status") & "', '" & .Rows(i).Item("Terbilang") & "', '" & .Rows(i).Item("Grand") & "', '" & .Rows(i).Item("Bayar") & "', "
						'SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Kode_Sales") & "', '" & .Rows(i).Item("PPN") & "', '" & .Rows(i).Item("Pembeda") & "', '" & .Rows(i).Item("Total_Point") & "', '" & .Rows(i).Item("Flag_Lipat") & "', '" & .Rows(i).Item("Kurs") & "', '" & .Rows(i).Item("Pakai_Point") & "', '" & .Rows(i).Item("Diskon_Promo") & "', '" & .Rows(i).Item("Diskon_Rupiah") & "', '" & .Rows(i).Item("Flag_Tagihan") & "', "
						'SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Kode_CB") & "', '" & .Rows(i).Item("RV") & "', '" & .Rows(i).Item("No_Surat_Jalan") & "', '" & .Rows(i).Item("Lokasi") & "', '" & .Rows(i).Item("Lokasi_Gdg") & "', '" & .Rows(i).Item("Total") & "', '" & .Rows(i).Item("Total_U_Dis_Member") & "', '" & .Rows(i).Item("Hasil_Diskon") & "', '" & .Rows(i).Item("Hasil_Diskon_Cash") & "', "
						'SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Kode_Voucher_1") & "', '" & .Rows(i).Item("Kode_Voucher_2") & "', '" & .Rows(i).Item("Kode_Voucher_3") & "', '" & .Rows(i).Item("Kode_Voucher_4") & "', '" & .Rows(i).Item("Kode_Voucher_5") & "', '" & .Rows(i).Item("Kode_Voucher_6") & "', '" & .Rows(i).Item("Kode_Voucher_7") & "', '" & .Rows(i).Item("Kode_Voucher_8") & "', "
						'SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Jenis") & "', '" & .Rows(i).Item("Nilai_PPN") & "', '" & .Rows(i).Item("No_PO_Toko") & "', '" & .Rows(i).Item("No_DO") & "', '" & .Rows(i).Item("Kode_Karyawan") & "', '" & .Rows(i).Item("Flag_Cabang_Sendiri") & "', '" & .Rows(i).Item("Flag_Cabang_Agency") & "', '" & .Rows(i).Item("COA_Piutang") & "', '" & .Rows(i).Item("Sudah_FK") & "', "
						'SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("No_Fak_Sebelumnya") & "', '" & .Rows(i).Item("No_Fak_Setelahnya") & "', '" & .Rows(i).Item("Init_Custm") & "', '" & .Rows(i).Item("Ket_Custm") & "', '" & .Rows(i).Item("No_KB") & "', '" & .Rows(i).Item("Jns") & "', '" & .Rows(i).Item("Akun_Kas") & "', '" & .Rows(i).Item("Akun_Piutang") & "', '" & .Rows(i).Item("Akun_Piutang_Sementara") & "', '" & .Rows(i).Item("Disc_Tambahan") & "', '" & .Rows(i).Item("Hasil_Disc_Tambahan") & "', '" & .Rows(i).Item("Harus_Retur_Semua") & "', "
						'SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Persen_Insentif_1") & "', '" & .Rows(i).Item("Persen_Insentif_2") & "', '" & .Rows(i).Item("Nilai_Insentif_1") & "', '" & .Rows(i).Item("Nilai_Insentif_2") & "', '" & .Rows(i).Item("Akun_Biaya_Insentif_1") & "', '" & .Rows(i).Item("Akun_Biaya_Insentif_2") & "', '" & .Rows(i).Item("Akun_Hutang_Insentif_1") & "', '" & .Rows(i).Item("Akun_Hutang_Insentif_2") & "', "
						'SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Kepala") & "', '" & .Rows(i).Item("Sudah_Voucher") & "', '" & .Rows(i).Item("Tanggal_Voucher") & "', '" & .Rows(i).Item("Jam_Voucher") & "', '" & .Rows(i).Item("User_Voucher") & "', '" & .Rows(i).Item("Sudah_Upload") & "', '" & .Rows(i).Item("Sudah_Kirim") & "', '" & .Rows(i).Item("Flag_DO_Selesai") & "', "
						'SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("No_Faktur_Pajak") & "', '" & .Rows(i).Item("Tgl_Faktur_Pajak") & "', '" & .Rows(i).Item("Jam_Faktur_Pajak") & "', '" & .Rows(i).Item("User_Faktur_Pajak") & "', '" & .Rows(i).Item("Subtotal_Faktur_Pajak") & "', '" & .Rows(i).Item("PPN_Faktur_Pajak") & "', '" & .Rows(i).Item("Grand_Faktur_Pajak") & "', "
						'SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Val_Diskon_Cash") & "', '" & .Rows(i).Item("Tgl_Jurnal_Diskon_Cash") & "', '" & .Rows(i).Item("Tgl_Val_Diskon_Cash") & "', '" & .Rows(i).Item("Jam_Val_Diskon_Cash") & "', '" & .Rows(i).Item("User_Val_Diskon_Cash") & "', "
						'SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Nilai_Val_Diskon_Cash") & "', '" & .Rows(i).Item("PPN_Val_Diskon_Cash") & "', '" & .Rows(i).Item("Ket_Val_Diskon_Cash") & "', '" & .Rows(i).Item("CB_Val_Diskon_Cash") & "', '" & .Rows(i).Item("Akun_Val_Diskon_Cash") & "', '" & .Rows(i).Item("Kode_Voucher_Diskon_Cash") & "', "
						'SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("zzz") & "', '" & .Rows(i).Item("z1") & "', '" & .Rows(i).Item("Diskon_Sementara") & "', '" & .Rows(i).Item("Nilai_Diskon_Sementara") & "', '" & .Rows(i).Item("Harus_Diupdate") & "', '" & .Rows(i).Item("Lama_Diskon_Sementara") & "', '" & .Rows(i).Item("Metode_Pot_Stock") & "', "
						'SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Flag_Opm") & "', '" & .Rows(i).Item("Flag_Sudah_Ke_Pusat") & "', '" & .Rows(i).Item("Flag_Sementara_Saat_Opm") & "', '" & .Rows(i).Item("Flag_ACC_Plafon") & "', '" & .Rows(i).Item("User_ACC_Plafon") & "', '" & .Rows(i).Item("Harus_Updtttt") & "', '" & .Rows(i).Item("Hrs_Updatex") & "', '" & .Rows(i).Item("Nfpx") & "', "
						'SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Total_Stlh_Diskon") & "', '" & .Rows(i).Item("metode_budgeting") & "', '" & .Rows(i).Item("Mulai_Deskcall") & "', '" & .Rows(i).Item("tgl_input") & "', '" & .Rows(i).Item("Flag_Lunas_tes") & "', '" & .Rows(i).Item("Flag_Lunas_Tunai_tes") & "', '" & .Rows(i).Item("Kode_Sub_Pekerjaan") & "', '" & .Rows(i).Item("Id_Sub_Pekerjaan") & "' "

						SQLMySQL = SQLMySQL & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "', '" & Format(.Rows(i).Item("tanggal"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("jam") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("userid") & "', '" & .Rows(i).Item("disc_cash") & "', '" & .Rows(i).Item("lokasi") & "', '" & .Rows(i).Item("hasil_diskon_cash") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("kode_voucher_2") & "', '" & .Rows(i).Item("flag_cabang_agency") & "', '" & .Rows(i).Item("disc_tambahan") & "', '" & .Rows(i).Item("hasil_disc_tambahan") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("persen_insentif_1") & "', '" & .Rows(i).Item("persen_insentif_2") & "', '" & .Rows(i).Item("nilai_insentif_1") & "', '" & .Rows(i).Item("nilai_insentif_2") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("nilai_val_diskon_cash") & "', '" & .Rows(i).Item("diskon_sementara") & "', '" & .Rows(i).Item("nilai_diskon_sementara") & "', '" & .Rows(i).Item("lama_diskon_sementara") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("metode_pot_stock") & "', '" & Format(.Rows(i).Item("tgl_input"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("id_sub_pekerjaan") & "', '" & .Rows(i).Item("keterangan") & "') "
						ExecuteTransMySQL(SQLMySQL)
					Next
				End With
			End Using

			''
			Dim j As Integer = 0
			For z As Integer = 1 To arrNo_Fak.Count

				SQL = "select Kode_Perusahaan,No_Faktur,No_Urut,Kode_Stock_Owner,Kode_Barang,Serial_Number,"
				SQL = SQL & "Keterangan,Jumlah,Harga,Persen_Diskon,Nilai_Diskon,x,kett,"
				SQL = SQL & "Harga_Min,Usermin,Pakai_SN,Barang_Hadiah,Modal,Nota_Kecil,"
				SQL = SQL & "Kode_Marketing,Subtotal,Flag_Sdr,Kode_Paket,Flag_Budgeting,Flag_Budgeting_Mbl,Flag_Budgeting_2,"
				SQL = SQL & "Harga_Terendah,Harga_Agen,Jml_Retur_Di_Pjk,Jml_Retur_Lain_Di_Pjk,Jml_Buat_Di_Pjk,Subtotal_Di_Pjk,"
				SQL = SQL & "Kode_Paket_2,Flag_Budgeting_3,Flag_Budgeting_4,Metode_Perhitungan,mdl,rvz,Flag_budgeting_new,Isi_Satuan_Besar "
				SQL = SQL & "from detail_penjualan_proyek "
				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_faktur = '" & arrNo_Fak.Item(j).ToString & "'"

				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")

						For i As Integer = 0 To .Rows.Count - 1

							SQLMySQL = "insert into detail_penjualan_proyek (kode_perusahaan,no_faktur,no_urut,kode_stock_owner,kode_barang,jumlah) "
							SQLMySQL = SQLMySQL & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "', '" & .Rows(i).Item("no_urut") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("kode_stock_owner") & "', '" & .Rows(i).Item("kode_barang") & "', '" & .Rows(i).Item("jumlah") & "') "
							ExecuteTransMySQL(SQLMySQL)

						Next
					End With
				End Using

				'------------------------------------------------------

				SQL = "select Kode_Perusahaan, No_faktur, no_Urut, Kode_stock_owner, Kode_barang, serial_number, jumlah, id_proyek, id_subproyek, urut_oto "
				SQL = SQL & "from det_penj_proyek "
				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_faktur = '" & arrNo_Fak.Item(j).ToString & "' "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")

						For i As Integer = 0 To .Rows.Count - 1

							SQLMySQL = "insert into det_penj_proyek (Kode_Perusahaan, No_faktur, no_Urut, Kode_stock_owner, Kode_barang, serial_number, jumlah, proyek_id, sub_proyek_id, urut_oto) "
							SQLMySQL = SQLMySQL & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "', '" & .Rows(i).Item("no_urut") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("kode_stock_owner") & "', '" & .Rows(i).Item("kode_barang") & "', '" & .Rows(i).Item("serial_number") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("jumlah") & "', "
							'Id Proyek
							If General_Class.CekNULL((.Rows(i).Item("id_proyek"))) = "" Then
								SQLMySQL = SQLMySQL & "NULL, "
							Else
								SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("id_proyek") & "', "
							End If
							'Id SubProyek
							If General_Class.CekNULL((.Rows(i).Item("id_subproyek"))) = "" Then
								SQLMySQL = SQLMySQL & "NULL, "
							Else
								SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("id_subproyek") & "', "
							End If
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("urut_oto") & "')"
							ExecuteTransMySQL(SQLMySQL)

						Next
					End With
				End Using

				SQL = "update penjualan_proyek set flag_sdh_pindah = 'Y' where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
				SQL = SQL & "No_faktur = '" & arrNo_Fak.Item(j).ToString & "' "
				ExecuteTrans(SQL)

				j = j + 1
			Next

			'MessageBox.Show("simpan berhasil", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "insert penjualan proyek")
			Exit Sub
		End Try

	End Sub

	Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrNo_Fak.Clear()
			SQL = "select no_faktur from PO_Pembelian_Proyek where "
			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
			SQL = SQL & "flag_sdh_pindah is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrNo_Fak.Add(.Rows(i).Item("no_faktur"))
					Next
				End With
			End Using

			SQL = "Select Kode_Perusahaan,No_Faktur,No_Nota,Tanggal,Jam, "
			SQL = SQL & "Kode_Supplier,Jenis_Transaksi,Tgl_Jatuh_Tempo, "
			SQL = SQL & "Flag_Lunas,Tgl_Lunas,Jam_Lunas, "
			SQL = SQL & "UserID,UserValidasi,Disc1,Disc2,Status,Terbilang,Grand,Kode_CB,Lokasi, "
			SQL = SQL & "Kode_Voucher,PPN,Nilai_PPN,Nilai_Sblm_PPN,Tgl_Input,sudah_fk,No_Sementara, "
			SQL = SQL & "No_PO,No_SO,Flag_Sdh,Sudah_Cek,Sudah_Kirim,X_TermZ,Finish, "
			SQL = SQL & "Validasi_Cek,Tgl_Validasi_Cek,Jam_Validasi_Cek,User_Validasi_Cek, "
			SQL = SQL & "Flag_Opm,Metode_Stock,Kode_Proyek,Pakai,Pakaix, "
			SQL = SQL & "No_Fak_Pembelian,No_Faktur_Rab,Flag_Barang_Masuk,Flag_Sdh_Pindah, eta, etd, Flag_PO_Ditandai "
			SQL = SQL & "from PO_Pembelian_Proyek "
			SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Flag_Sdh_Pindah is null and status is null"

			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						''SQLMySQL = "insert into po_pembelian_proyek (kode_perusahaan,no_faktur,no_nota,tanggal,jam, "
						''SQLMySQL = SQLMySQL & "kode_supplier,jenis_transaksi,tgl_jatuh_tempo, "
						''SQLMySQL = SQLMySQL & "flag_lunas,tgl_lunas,jam_lunas, "
						''SQLMySQL = SQLMySQL & "userid,uservalidasi,disc1,disc2,terbilang,grand,kode_cb,lokasi, "
						''SQLMySQL = SQLMySQL & "kode_voucher,ppn,nilai_ppn,nilai_sblm_ppn,tgl_input,sudah_fk,no_sementara, "
						''SQLMySQL = SQLMySQL & "no_po,no_so,flag_sdh,sudah_cek,sudah_kirim,x_term,finish, "
						''SQLMySQL = SQLMySQL & "validasi_cek,tgl_validasi_cek,jam_validasi_cek,user_validasi_cek, "
						''SQLMySQL = SQLMySQL & "flag_opm,metode_stock,kode_proyek,pakai,pakaix, "
						''SQLMySQL = SQLMySQL & "no_fak_pembelian,no_faktur_rab,flag_barang_masuk,flag_sdh_pindah) "
						''SQLMySQL = SQLMySQL & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "', '" & .Rows(i).Item("no_nota") & "', '" & .Rows(i).Item("no_nota") & "', '" & Format(.Rows(i).Item("tanggal"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("jam") & "', "
						''SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("kode_supplier") & "','" & .Rows(i).Item("jenis_transaksi") & "', '" & Format(.Rows(i).Item("tgl_jatuh_tempo"), "yyyy-MM-dd") & "', "
						''SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("flag_lunas") & "','" & Format(.Rows(i).Item("tgl_lunas"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("jam_lunas") & "', '" & .Rows(i).Item("jam_lunas") & "', "
						''SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("userid") & "','" & .Rows(i).Item("uservalidasi") & "', '" & .Rows(i).Item("disc1") & "', '" & .Rows(i).Item("disc2") & "', '" & .Rows(i).Item("terbilang") & "', '" & .Rows(i).Item("grand") & "', '" & .Rows(i).Item("kode_cb") & "', '" & .Rows(i).Item("lokasi") & "', "
						''SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("kode_voucher") & "', '" & .Rows(i).Item("ppn") & "', '" & .Rows(i).Item("nilai_ppn") & "', '" & .Rows(i).Item("nilai_sblm_ppn") & "', '" & Format(.Rows(i).Item("tgl_input"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("sudah_fk") & "', '" & .Rows(i).Item("no_sementara") & "', "
						''SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("no_po") & "','" & .Rows(i).Item("no_so") & "', '" & .Rows(i).Item("flag_sdh") & "', '" & .Rows(i).Item("sudah_cek") & "', '" & .Rows(i).Item("sudah_kirim") & "', '" & .Rows(i).Item("x_term") & "', '" & .Rows(i).Item("finish") & "', "
						''SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("validasi_cek") & "','" & Format(.Rows(i).Item("tgl_validasi_cek"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("jam_validasi_cek") & "', '" & .Rows(i).Item("user_validasi_cek") & "', "
						''SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("flag_opm") & "','" & .Rows(i).Item("metode_stock") & "', '" & .Rows(i).Item("kode_proyek") & "', '" & .Rows(i).Item("pakai") & "', '" & .Rows(i).Item("pakaix") & "', "
						''SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("no_fak_pembelian") & "','" & .Rows(i).Item("no_faktur_rab") & "', '" & .Rows(i).Item("flag_barang_masuk") & "', '" & .Rows(i).Item("flag_sdh_pindah") & "') "
						'KURANG tgl_jatuh_tempo, kode_cb, flag_barang_masuk
						SQLMySQL = "insert into po_pembelian_proyek (kode_perusahaan,no_faktur,no_nota,tanggal,jam, "
						SQLMySQL = SQLMySQL & "kode_supplier,"
						SQLMySQL = SQLMySQL & "userid,disc1,disc2,terbilang,grand,lokasi, "
						SQLMySQL = SQLMySQL & "kode_voucher,ppn,nilai_ppn,nilai_sblm_ppn, "
						SQLMySQL = SQLMySQL & "x_term, "
						SQLMySQL = SQLMySQL & "no_faktur_rab,eta,etd, "
						SQLMySQL = SQLMySQL & "tgl_jatuh_tempo,kode_cb,flag_barang_masuk, Flag_PO_Ditandai,jenis_transaksi)"
						SQLMySQL = SQLMySQL & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "', '" & .Rows(i).Item("no_nota") & "', '" & Format(.Rows(i).Item("tanggal"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("jam") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("kode_supplier") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("userid") & "','" & .Rows(i).Item("disc1") & "', '" & .Rows(i).Item("disc2") & "', '" & .Rows(i).Item("terbilang") & "', '" & .Rows(i).Item("grand") & "', '" & .Rows(i).Item("lokasi") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("kode_voucher") & "', '" & .Rows(i).Item("ppn") & "', '" & .Rows(i).Item("nilai_ppn") & "', '" & .Rows(i).Item("nilai_sblm_ppn") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("x_termz") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("no_faktur_rab") & "', "
						'eta
						If General_Class.CekNULL((.Rows(i).Item("eta"))) = "" Then
							SQLMySQL = SQLMySQL & "NULL, "
						Else
							SQLMySQL = SQLMySQL & "'" & Format(.Rows(i).Item("eta"), "yyyy-MM-dd") & "', "
						End If
						'etd
						If General_Class.CekNULL((.Rows(i).Item("etd"))) = "" Then
							SQLMySQL = SQLMySQL & "NULL, "
						Else
							SQLMySQL = SQLMySQL & "'" & Format(.Rows(i).Item("etd"), "yyyy-MM-dd") & "', "
						End If
						'tgl jatuh tempo
						If General_Class.CekNULL((.Rows(i).Item("tgl_jatuh_tempo"))) = "" Then
							SQLMySQL = SQLMySQL & "NULL, "
						Else
							SQLMySQL = SQLMySQL & "'" & Format(.Rows(i).Item("tgl_jatuh_tempo"), "yyyy-MM-dd") & "', "
						End If
						'kode cb
						If General_Class.CekNULL((.Rows(i).Item("kode_cb"))) = "" Then
							SQLMySQL = SQLMySQL & "NULL, "
						Else
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("kode_cb") & "', "
						End If
						'flag_barang_masuk
						If General_Class.CekNULL((.Rows(i).Item("flag_barang_masuk"))) = "" Then
							SQLMySQL = SQLMySQL & "NULL,"
						Else
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("flag_barang_masuk") & "', "
						End If

						'flag_ditandai
						If General_Class.CekNULL((.Rows(i).Item("Flag_PO_Ditandai"))) = "" Then
							SQLMySQL = SQLMySQL & "NULL, "
						Else
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Flag_PO_Ditandai") & "', "
						End If

						If General_Class.CekNULL((.Rows(i).Item("jenis_transaksi"))) = "" Then
							SQLMySQL = SQLMySQL & "NULL "
						Else
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("jenis_transaksi") & "' "
						End If

						SQLMySQL = SQLMySQL & ") "
						ExecuteTransMySQL(SQLMySQL)

						'----Insert Approval ('RandomPIN 6 Digit,'RandomKodeUnik, RandomKodeUrl 10 Digit)

						SQLMySQL = "select id from users where flag_acc_po ='Y'"
						Using DsMySQL = BindingTransMySQL(SQLMySQL)
							For indexxx As Integer = 0 To DsMySQL.Tables("MyTable").Rows.Count - 1

								Dim randomNumber As New Random()
								Dim randomPIN As Integer = randomNumber.Next(100000, 1000000)
								Dim randomKdUnik As Integer = randomNumber.Next(1000000000, Integer.MaxValue)
								Dim randomKdUrl As Integer = randomNumber.Next(1000000000, Integer.MaxValue)

								SQLMySQL = "insert into approval_po_pembelian_proyek (kode_perusahaan, no_fak_po_pembelian,  "
								SQLMySQL = SQLMySQL & "user_id, kode_unik, kode_url) "
								SQLMySQL = SQLMySQL & "values ('" & KodePerusahaan & "' , '" & .Rows(i).Item("No_Faktur") & "',  "
								SQLMySQL = SQLMySQL & "'" & DsMySQL.Tables("MyTable").Rows(indexxx).Item("id") & "', "
								SQLMySQL = SQLMySQL & "'" & randomKdUnik.ToString & "', '" & randomKdUrl.ToString & "')"
								ExecuteTransMySQL(SQLMySQL)

							Next
						End Using

					Next
				End With
			End Using

			''
			' ''arrNo_Fak2.Clear()
			' ''arrNoUrut.Clear()
			' ''SQL = "select no_faktur, no_urut from detail_po_pembelian_proyek where "
			' ''SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "'"
			' ''Using Ds = BindingTrans(SQL)
			' ''    With Ds.Tables("MyTable")
			' ''        For i As Integer = 0 To .Rows.Count - 1
			' ''            arrNo_Fak2.Add(.Rows(i).Item("no_faktur"))
			' ''            arrNoUrut.Add(.Rows(i).Item("no_urut"))
			' ''        Next
			' ''    End With
			' ''End Using

			Dim j As Integer = 0
			For z As Integer = 1 To arrNo_Fak.Count

				SQL = "select kode_perusahaan,no_faktur,no_urut,kode_stock_owner,kode_barang,serial_number,keterangan,jumlah,harga,persen_diskon,"
				SQL = SQL & "nilai_diskon,x,hb_baru,hj_baru,flag_hb,flag_hj,hb_lama,hj_lama,userid_ubah,pakai_sn,expire,subtotal,jumlah_bm, "
				SQL = SQL & "Flag_PO_Besar, Flag_Harga_tinggi, harga_satuan_rab, subtotal_rab, flag_beda_referensi from detail_po_pembelian_proyek "
				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_faktur = '" & arrNo_Fak.Item(j).ToString & "'"

				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")

						For i As Integer = 0 To .Rows.Count - 1

							SQLMySQL = "insert into detail_po_pembelian_proyek (kode_perusahaan,no_faktur,no_urut,kode_stock_owner,kode_barang,serial_number,keterangan,jumlah,harga,"
							SQLMySQL = SQLMySQL & "persen_diskon,nilai_diskon,x,hb_baru,hj_baru,flag_hb,flag_hj,hb_lama,hj_lama,userid_ubah,pakai_sn,expire,subtotal,jumlah_bm, Flag_PO_Besar, Flag_Harga_tinggi, harga_satuan_rab, subtotal_rab, flag_beda_referensi) "
							SQLMySQL = SQLMySQL & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "', '" & .Rows(i).Item("no_urut") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("kode_stock_owner") & "', '" & .Rows(i).Item("kode_barang") & "', '" & .Rows(i).Item("serial_number") & "', '" & .Rows(i).Item("keterangan") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("jumlah") & "', '" & .Rows(i).Item("harga") & "', '" & .Rows(i).Item("persen_diskon") & "', '" & .Rows(i).Item("nilai_diskon") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("x") & "', '" & .Rows(i).Item("hb_baru") & "', '" & .Rows(i).Item("hj_baru") & "', '" & .Rows(i).Item("flag_hb") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("flag_hj") & "', '" & .Rows(i).Item("hb_lama") & "', '" & .Rows(i).Item("hj_lama") & "', '" & .Rows(i).Item("userid_ubah") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("pakai_sn") & "', '" & Format(.Rows(i).Item("expire"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("subtotal") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("jumlah_bm") & "', "

							'flag_ditandai
							If General_Class.CekNULL((.Rows(i).Item("Flag_PO_Besar"))) = "" Then
								SQLMySQL = SQLMySQL & "NULL, "
							Else
								SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Flag_PO_Besar") & "', "
							End If

							'flag_ditandai
							If General_Class.CekNULL((.Rows(i).Item("Flag_Harga_tinggi"))) = "" Then
								SQLMySQL = SQLMySQL & "NULL, "
							Else
								SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Flag_Harga_tinggi") & "', "
							End If

							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("harga_satuan_rab") & "', '" & .Rows(i).Item("subtotal_rab") & "', "

							If General_Class.CekNULL((.Rows(i).Item("Flag_Beda_Referensi"))) = "" Then
								SQLMySQL = SQLMySQL & "NULL "
							Else
								SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Flag_Beda_Referensi") & "' "
							End If
							SQLMySQL = SQLMySQL & ")"

							ExecuteTransMySQL(SQLMySQL)

						Next
					End With
				End Using

				'''------------------------------------------------------
				SQL = "select kode_perusahaan,no_faktur,kode_stock_owner,kode_barang,jumlah,urut_detail_po,urut_detail_rab,urut "
				SQL = SQL & "from det_po_pembelian_proyek "
				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_faktur = '" & arrNo_Fak.Item(j).ToString & "' "

				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							SQLMySQL = "insert into det_po_pembelian_proyek (kode_perusahaan,no_faktur,kode_stock_owner,kode_barang,jumlah,urut_detail_po,urut_detail_rab) "
							SQLMySQL = SQLMySQL & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("kode_stock_owner") & "', '" & .Rows(i).Item("kode_barang") & "', '" & .Rows(i).Item("jumlah") & "', '" & .Rows(i).Item("urut_detail_po") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("urut_detail_rab") & "')"
							ExecuteTransMySQL(SQLMySQL)
						Next
					End With
				End Using

				SQL = "update po_pembelian_proyek set flag_sdh_pindah = 'Y' where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
				SQL = SQL & "No_faktur = '" & arrNo_Fak.Item(j).ToString & "' "
				ExecuteTrans(SQL)

				j = j + 1
			Next

			'''
			' ''Dim jj As Integer = 0
			' ''For zz As Integer = 1 To arrNo_Fak2.Count

			' ''    SQL = "select kode_perusahaan,no_faktur,kode_stock_owner,kode_barang,jumlah,urut_detail_po,urut_detail_rab,urut "
			' ''    SQL = SQL & "from det_po_pembelian_proyek "
			' ''    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_faktur = '" & arrNo_Fak2.Item(jj).ToString & "' and urut_detail_po = '" & arrNoUrut.Item(jj).ToString & "' "

			' ''    Using Ds = BindingTrans(SQL)
			' ''        With Ds.Tables("MyTable")
			' ''            For ii As Integer = 0 To .Rows.Count - 1
			' ''                SQLMySQL = "insert into det_po_pembelian_proyek (kode_perusahaan,no_faktur,kode_stock_owner,kode_barang,jumlah,urut_detail_po,urut_detail_rab) "
			' ''                SQLMySQL = SQLMySQL & "Values ('" & .Rows(ii).Item("kode_perusahaan") & "', '" & .Rows(ii).Item("no_faktur") & "', "
			' ''                SQLMySQL = SQLMySQL & "'" & .Rows(ii).Item("kode_stock_owner") & "', '" & .Rows(ii).Item("kode_barang") & "', '" & .Rows(ii).Item("jumlah") & "', '" & .Rows(ii).Item("urut_detail_po") & "', "
			' ''                SQLMySQL = SQLMySQL & "'" & .Rows(ii).Item("urut_detail_rab") & "')"
			' ''                ExecuteTransMySQL(SQLMySQL)
			' ''            Next
			' ''        End With
			' ''    End Using

			' ''    SQL = "update po_pembelian_proyek set flag_sdh_pindah = 'Y' where "
			' ''    SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
			' ''    SQL = SQL & "No_faktur = '" & arrNo_Fak2.Item(jj).ToString & "' "
			' ''    ExecuteTrans(SQL)

			' ''    jj = jj + 1
			' ''Next

			'MessageBox.Show("simpan berhasil", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "insert po pembelian proyek")
			Exit Sub
		End Try
	End Sub

	Private Sub btn_KeBrgMskMYSQL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_KeBrgMskMYSQL.Click
		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrNo_Fak.Clear()
			SQL = "select no_faktur from Barang_Masuk_Proyek where "
			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
			SQL = SQL & "flag_sdh_pindah is null"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrNo_Fak.Add(.Rows(i).Item("no_faktur"))
					Next
				End With
			End Using

			SQL = "select kode_perusahaan,No_Faktur,Tanggal,Jam,Status,No_PO,Keterangan, "
			SQL = SQL & "UserID,Lokasi,Flag_Pakai,no_fak_pembelian "
			SQL = SQL & "from Barang_Masuk_Proyek "
			SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null and status is null"

			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						SQLMySQL = "insert into barang_masuk_proyek (kode_perusahaan,no_faktur,tanggal,jam, "
						SQLMySQL = SQLMySQL & "no_po,keterangan,userid, lokasi, flag_pakai, "
						SQLMySQL = SQLMySQL & "no_fak_pembelian) "
						SQLMySQL = SQLMySQL & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "', '" & Format(.Rows(i).Item("tanggal"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("jam") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("no_po") & "','" & .Rows(i).Item("keterangan") & "', '" & .Rows(i).Item("userid") & "', '" & .Rows(i).Item("lokasi") & "', "
						If General_Class.CekNULL((.Rows(i).Item("flag_pakai"))) = "" Then
							SQLMySQL = SQLMySQL & "NULL, "
						Else
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("flag_pakai") & "', "
						End If
						If General_Class.CekNULL((.Rows(i).Item("no_fak_pembelian"))) = "" Then
							SQLMySQL = SQLMySQL & "NULL) "
						Else
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("no_fak_pembelian") & "') "
						End If
						ExecuteTransMySQL(SQLMySQL)
					Next
				End With
			End Using

			''
			''arrNo_Fak2.Clear()
			''arrNoUrut.Clear()
			''SQL = "select no_faktur, urut_po from barang_masuk_proyek_detail where "
			''SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "'"
			''Using Ds = BindingTrans(SQL)
			''    With Ds.Tables("MyTable")
			''        For i As Integer = 0 To .Rows.Count - 1
			''            arrNo_Fak2.Add(.Rows(i).Item("no_faktur"))
			''            arrNoUrut.Add(.Rows(i).Item("urut_po"))
			''        Next
			''    End With
			''End Using

			Dim j As Integer = 0
			For z As Integer = 1 To arrNo_Fak.Count

				SQL = "select kode_perusahaan,no_faktur,kode_barang,kode_stock_owner,urut_po,jumlah,urut,harga "
				SQL = SQL & "from barang_masuk_proyek_detail "
				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_faktur = '" & arrNo_Fak.Item(j).ToString & "'"

				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")

						For i As Integer = 0 To .Rows.Count - 1

							SQLMySQL = "insert into barang_masuk_proyek_detail (kode_perusahaan,no_faktur,kode_barang,kode_stock_owner,urut_po,jumlah,harga)"
							SQLMySQL = SQLMySQL & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "', '" & .Rows(i).Item("kode_barang") & "','" & .Rows(i).Item("kode_stock_owner") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("urut_po") & "', '" & .Rows(i).Item("jumlah") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("harga") & "')"
							ExecuteTransMySQL(SQLMySQL)

						Next
					End With
				End Using

				'''------------------------------------------------------
				SQL = "select kode_perusahaan,no_faktur,kode_stock_owner,kode_barang,jumlah,urut_detail_po,urut_detail_bm,urut, urut_detail_request "
				SQL = SQL & "from Barang_Masuk_Proyek_Det "
				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_faktur = '" & arrNo_Fak.Item(j).ToString & "' "
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							SQLMySQL = "insert into barang_masuk_proyek_det (kode_perusahaan,no_faktur,kode_stock_owner,kode_barang,jumlah,urut_detail_po,urut_detail_bm, urut_detail_request) "
							SQLMySQL = SQLMySQL & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("kode_stock_owner") & "', '" & .Rows(i).Item("kode_barang") & "', '" & .Rows(i).Item("jumlah") & "', '" & .Rows(i).Item("urut_detail_po") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("urut_detail_bm") & "', '" & .Rows(i).Item("urut_detail_request") & "')"
							ExecuteTransMySQL(SQLMySQL)
						Next
					End With
				End Using

				SQL = "update barang_masuk_proyek set flag_sdh_pindah = 'Y' where "
				SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
				SQL = SQL & "No_faktur = '" & arrNo_Fak.Item(j).ToString & "' "
				ExecuteTrans(SQL)

				j = j + 1
			Next

			'''
			' ''Dim jj As Integer = 0
			' ''For zz As Integer = 1 To arrNo_Fak2.Count

			' ''    SQL = "select kode_perusahaan,no_faktur,kode_stock_owner,kode_barang,jumlah,urut_detail_po,urut_detail_bm,urut "
			' ''    SQL = SQL & "from Barang_Masuk_Proyek_Det "
			' ''    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_faktur = '" & arrNo_Fak2.Item(jj).ToString & "' and urut_detail_po = '" & arrNoUrut.Item(jj).ToString & "' "

			' ''    Using Ds = BindingTrans(SQL)
			' ''        With Ds.Tables("MyTable")
			' ''            For ii As Integer = 0 To .Rows.Count - 1
			' ''                SQLMySQL = "insert into barang_masuk_proyek_det (kode_perusahaan,no_faktur,kode_stock_owner,kode_barang,jumlah,urut_detail_po,urut_detail_bm) "
			' ''                SQLMySQL = SQLMySQL & "Values ('" & .Rows(ii).Item("kode_perusahaan") & "', '" & .Rows(ii).Item("no_faktur") & "', "
			' ''                SQLMySQL = SQLMySQL & "'" & .Rows(ii).Item("kode_stock_owner") & "', '" & .Rows(ii).Item("kode_barang") & "', '" & .Rows(ii).Item("jumlah") & "', '" & .Rows(ii).Item("urut_detail_po") & "', "
			' ''                SQLMySQL = SQLMySQL & "'" & .Rows(ii).Item("urut_detail_bm") & "')"
			' ''                ExecuteTransMySQL(SQLMySQL)
			' ''            Next
			' ''        End With
			' ''    End Using

			' ''    SQL = "update barang_masuk_proyek set flag_sdh_pindah = 'Y' where "
			' ''    SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
			' ''    SQL = SQL & "No_faktur = '" & arrNo_Fak2.Item(jj).ToString & "' "
			' ''    ExecuteTrans(SQL)

			' ''    jj = jj + 1
			' ''Next

			'MessageBox.Show("simpan berhasil", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "insert barang masuk proyek")
			Exit Sub
		End Try
	End Sub

	Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrEdit.Clear()
			SQLMySQL = "select No_faktur from rab_pembelian_proyek where "
			SQLMySQL = SQLMySQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
			SQLMySQL = SQLMySQL & " flag_edit_rab = 'Y'"
			Using Ds = BindingTransMySQL(SQLMySQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrEdit.Add(.Rows(i).Item("no_faktur"))
					Next
				End With
			End Using

			Dim a As Integer = 0
			For b As Integer = 1 To arrEdit.Count
				SQLMySQL = "select Kode_Perusahaan, No_Faktur,flag_edit_rab, flag_mulai_rab, flag_selesai_rab from rab_pembelian_proyek rpp "
				SQLMySQL = SQLMySQL & "where Kode_Perusahaan ='" & KodePerusahaan & "' and No_Faktur ='" & arrEdit.Item(a).ToString & "' and flag_edit_rab ='Y'"
				Using DsMySQL = BindingTransMySQL(SQLMySQL)
					With DsMySQL.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							Dim flag_mulai As String = "NULL"
							Dim flag_selesai As String = "NULL"

							If General_Class.CekNULL(.Rows(i).Item("flag_mulai_rab")) <> "" Then
								flag_mulai = "'Y'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("flag_selesai_rab")) <> "" Then
								flag_selesai = "'Y'"
							End If
							SQL = "update rab_pembelian_proyek set flag_mulai_rab=" & flag_mulai & ", flag_selesai_rab=" & flag_selesai & " "
							SQL = SQL & "where kode_perusahaan='" & .Rows(i).Item("Kode_Perusahaan") & "' "
							SQL = SQL & "and no_faktur='" & .Rows(i).Item("No_Faktur") & "'"
							ExecuteTrans(SQL)
						Next
					End With
				End Using

				SQLMySQL = "Update rab_pembelian_proyek set flag_edit_rab = null where "
				SQLMySQL = SQLMySQL & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
				SQLMySQL = SQLMySQL & "No_faktur = '" & arrEdit.Item(a).ToString & "' "
				ExecuteTransMySQL(SQLMySQL)
				a = a + 1
			Next

			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "edit rab web dari mysql ")
			Exit Sub
		End Try
	End Sub

	Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrEdit.Clear()
			SQLMySQL = "select No_faktur from po_pembelian_proyek where "
			SQLMySQL = SQLMySQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
			SQLMySQL = SQLMySQL & " flag_edit_po = 'Y'"
			Using Ds = BindingTransMySQL(SQLMySQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrEdit.Add(.Rows(i).Item("no_faktur"))
					Next
				End With
			End Using

			Dim a As Integer = 0
			For b As Integer = 1 To arrEdit.Count
				SQLMySQL = "select Kode_Perusahaan, No_faktur, flag_acc_web, Tanggal_Acc_Web, Jam_acc_web, user_acc_web from po_pembelian_proyek rpp "
				SQLMySQL = SQLMySQL & "where Kode_Perusahaan ='" & KodePerusahaan & "' and No_Faktur ='" & arrEdit.Item(a).ToString & "' and flag_edit_po ='Y'"
				Using DsMySQL = BindingTransMySQL(SQLMySQL)
					With DsMySQL.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							Dim flag_acc As String = "NULL"
							Dim tanggal_acc As String = "NULL"
							Dim jam_acc As String = "NULL"
							Dim user_acc As String = "NULL"

							If General_Class.CekNULL(.Rows(i).Item("flag_acc_web")) <> "" Then
								flag_acc = "'" & .Rows(i).Item("flag_acc_web") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("Tanggal_Acc_Web")) <> "" Then
								tanggal_acc = "'" & Format(.Rows(i).Item("Tanggal_Acc_Web"), "yyyy-MM-dd") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("Jam_acc_web")) <> "" Then
								jam_acc = "'" & .Rows(i).Item("Jam_acc_web") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("user_acc_web")) <> "" Then
								user_acc = "'" & .Rows(i).Item("user_acc_web") & "'"
							End If

							SQL = "update po_pembelian_proyek set flag_acc_web=" & flag_acc & ", Tanggal_Acc_Web=" & tanggal_acc & ", Jam_acc_web=" & jam_acc & ", user_acc_web=" & user_acc & " "
							SQL = SQL & "where kode_perusahaan='" & .Rows(i).Item("Kode_Perusahaan") & "' "
							SQL = SQL & "and no_faktur='" & .Rows(i).Item("No_Faktur") & "'"
							ExecuteTrans(SQL)
						Next
					End With
				End Using

				SQLMySQL = "Update po_pembelian_proyek set flag_edit_po = null where "
				SQLMySQL = SQLMySQL & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
				SQLMySQL = SQLMySQL & "No_faktur = '" & arrEdit.Item(a).ToString & "' "
				ExecuteTransMySQL(SQLMySQL)
				a = a + 1
			Next

			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "edit po web")
			Exit Sub
		End Try
	End Sub

	Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click

		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrEdit.Clear()
			SQLMySQL = "select id from proyeks where "
			SQLMySQL = SQLMySQL & "flag_sdh_pindah_ke_sql = 'Y' and flag_edit_proyeks is null "
			Using Ds = BindingTransMySQL(SQLMySQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrEdit.Add(.Rows(i).Item("id"))
					Next
				End With
			End Using

			Dim a As Integer = 0
			For b As Integer = 1 To arrEdit.Count
				SQLMySQL = "select id, kode_proyek, keterangan, target, tanggal_mulai, tanggal_selesai from proyeks "
				SQLMySQL = SQLMySQL & "where id = '" & arrEdit.Item(a).ToString & "' "
				SQLMySQL = SQLMySQL & "and flag_sdh_pindah_ke_sql = 'Y' "
				SQLMySQL = SQLMySQL & "and flag_edit_proyeks is null "

				Using DsMySQL = BindingTransMySQL(SQLMySQL)
					With DsMySQL.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							Dim kode_proyek As String = "NULL"
							Dim keterangan As String = "NULL"
							Dim target As String = "NULL"
							Dim tanggal_mulai As String = "NULL"
							Dim tanggal_selesai As String = "NULL"

							If General_Class.CekNULL(.Rows(i).Item("kode_proyek")) <> "" Then
								kode_proyek = "'" & .Rows(i).Item("kode_proyek") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("keterangan")) <> "" Then
								keterangan = "'" & .Rows(i).Item("keterangan") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("target")) <> "" Then
								target = "'" & .Rows(i).Item("target") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("tanggal_mulai")) <> "" Then
								tanggal_mulai = "'" & Format(.Rows(i).Item("tanggal_mulai"), "yyyy-MM-dd") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("tanggal_selesai")) <> "" Then
								tanggal_selesai = "'" & Format(.Rows(i).Item("tanggal_selesai"), "yyyy-MM-dd") & "'"
							End If

							SQL = "update web_proyeks set kode_proyek = " & kode_proyek & ", "
							SQL = SQL & "keterangan = " & keterangan & ", "
							SQL = SQL & "target = " & target & ", "
							SQL = SQL & "tanggal_mulai = " & tanggal_mulai & ", "
							SQL = SQL & "tanggal_selesai = " & tanggal_selesai & " "
							SQL = SQL & "where id = '" & .Rows(i).Item("id") & "' "
							ExecuteTrans(SQL)
						Next
					End With
				End Using

				SQLMySQL = "update proyeks set flag_sdh_pindah_ke_sql = 'Y', flag_edit_proyeks = 'Y' "
				SQLMySQL = SQLMySQL & "where "
				SQLMySQL = SQLMySQL & "id = '" & arrEdit.Item(a).ToString & "' "
				ExecuteTransMySQL(SQLMySQL)
				a = a + 1
			Next

			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "Edit web proyek")
			Exit Sub
		End Try

	End Sub

	Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click

		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrEdit.Clear()
			SQLMySQL = "select id from subproyeks where "
			SQLMySQL = SQLMySQL & "flag_sdh_pindah_ke_sql = 'Y' and flag_edit_subproyeks is null "
			Using Ds = BindingTransMySQL(SQLMySQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrEdit.Add(.Rows(i).Item("id"))
					Next
				End With
			End Using

			Dim a As Integer = 0
			For b As Integer = 1 To arrEdit.Count
				SQLMySQL = "select id, kode_sub_proyek, keterangan from subproyeks "
				SQLMySQL = SQLMySQL & "where id = '" & arrEdit.Item(a).ToString & "' "
				SQLMySQL = SQLMySQL & "and flag_sdh_pindah_ke_sql = 'Y' "
				SQLMySQL = SQLMySQL & "and flag_edit_subproyeks is null "

				Using DsMySQL = BindingTransMySQL(SQLMySQL)
					With DsMySQL.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							Dim kode_sub_proyek As String = "NULL"
							Dim keterangan As String = "NULL"

							If General_Class.CekNULL(.Rows(i).Item("kode_sub_proyek")) <> "" Then
								kode_sub_proyek = "'" & .Rows(i).Item("kode_sub_proyek") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("keterangan")) <> "" Then
								keterangan = "'" & .Rows(i).Item("keterangan") & "'"
							End If

							SQL = "update web_subproyeks set kode_sub_proyek = " & kode_sub_proyek & ", "
							SQL = SQL & "keterangan = " & keterangan & " "
							SQL = SQL & "where id = '" & .Rows(i).Item("id") & "' "
							ExecuteTrans(SQL)
						Next
					End With
				End Using

				SQLMySQL = "update subproyeks set flag_sdh_pindah_ke_sql = 'Y', flag_edit_subproyeks = 'Y' "
				SQLMySQL = SQLMySQL & "where "
				SQLMySQL = SQLMySQL & "id = '" & arrEdit.Item(a).ToString & "' "
				ExecuteTransMySQL(SQLMySQL)
				a = a + 1
			Next

			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "edit web sub proyek")
			Exit Sub
		End Try

	End Sub

	Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click

		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrEdit.Clear()
			SQLMySQL = "select id from pekerjaans where "
			SQLMySQL = SQLMySQL & "flag_sdh_pindah_ke_sql = 'Y' and flag_edit_pekerjaans is null "
			Using Ds = BindingTransMySQL(SQLMySQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrEdit.Add(.Rows(i).Item("id"))
					Next
				End With
			End Using

			Dim a As Integer = 0
			For b As Integer = 1 To arrEdit.Count
				SQLMySQL = "select id, kode_pekerjaan, keterangan from pekerjaans "
				SQLMySQL = SQLMySQL & "where id = '" & arrEdit.Item(a).ToString & "' "
				SQLMySQL = SQLMySQL & "and flag_sdh_pindah_ke_sql = 'Y' "
				SQLMySQL = SQLMySQL & "and flag_edit_pekerjaans is null "

				Using DsMySQL = BindingTransMySQL(SQLMySQL)
					With DsMySQL.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							Dim kode_pekerjaan As String = "NULL"
							Dim keterangan As String = "NULL"

							If General_Class.CekNULL(.Rows(i).Item("kode_pekerjaan")) <> "" Then
								kode_pekerjaan = "'" & .Rows(i).Item("kode_pekerjaan") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("keterangan")) <> "" Then
								keterangan = "'" & .Rows(i).Item("keterangan") & "'"
							End If

							SQL = "update web_pekerjaans set kode_pekerjaan = " & kode_pekerjaan & ", "
							SQL = SQL & "keterangan = " & keterangan & " "
							SQL = SQL & "where id = '" & .Rows(i).Item("id") & "' "
							ExecuteTrans(SQL)
						Next
					End With
				End Using

				SQLMySQL = "update pekerjaans set flag_sdh_pindah_ke_sql = 'Y', flag_edit_pekerjaans = 'Y' "
				SQLMySQL = SQLMySQL & "where "
				SQLMySQL = SQLMySQL & "id = '" & arrEdit.Item(a).ToString & "' "
				ExecuteTransMySQL(SQLMySQL)
				a = a + 1
			Next

			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "edit web pekerjaan")
			Exit Sub
		End Try

	End Sub

	Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click

		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrEdit.Clear()
			SQLMySQL = "select id from sub_pekerjaans where "
			SQLMySQL = SQLMySQL & "flag_sdh_pindah_ke_sql = 'Y' and flag_edit_subpekerjaans is null "
			Using Ds = BindingTransMySQL(SQLMySQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrEdit.Add(.Rows(i).Item("id"))
					Next
				End With
			End Using

			Dim a As Integer = 0
			For b As Integer = 1 To arrEdit.Count
				SQLMySQL = "select id,sub_pekerjaan,target,tanggal_mulai,tanggal_selesai,tanggal_mulai_real,tanggal_selesai_real, "
				SQLMySQL = SQLMySQL & "progress,tanggal,jam,persen_toleransi,bobot "
				SQLMySQL = SQLMySQL & "from sub_pekerjaans "
				SQLMySQL = SQLMySQL & "where id = '" & arrEdit.Item(a).ToString & "' "
				SQLMySQL = SQLMySQL & "and flag_sdh_pindah_ke_sql = 'Y' "
				SQLMySQL = SQLMySQL & "and flag_edit_subpekerjaans is null "

				Using DsMySQL = BindingTransMySQL(SQLMySQL)
					With DsMySQL.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							Dim sub_pekerjaan As String = "NULL"
							Dim target As String = "NULL"
							Dim tanggal_mulai As String = "NULL"
							Dim tanggal_selesai As String = "NULL"
							Dim tanggal_mulai_real As String = "NULL"
							Dim tanggal_selesai_real As String = "NULL"
							Dim progress As String = "NULL"
							Dim tanggal As String = "NULL"
							Dim jam As String = "NULL"
							Dim persen_toleransi As String = "NULL"
							Dim bobot As String = "NULL"

							If General_Class.CekNULL(.Rows(i).Item("sub_pekerjaan")) <> "" Then
								sub_pekerjaan = "'" & .Rows(i).Item("sub_pekerjaan") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("target")) <> "" Then
								target = "'" & .Rows(i).Item("target") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("tanggal_mulai")) <> "" Then
								tanggal_mulai = "'" & Format(.Rows(i).Item("tanggal_mulai"), "yyyy-MM-dd") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("tanggal_selesai")) <> "" Then
								tanggal_selesai = "'" & Format(.Rows(i).Item("tanggal_selesai"), "yyyy-MM-dd") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("tanggal_mulai_real")) <> "" Then
								tanggal_mulai_real = "'" & Format(.Rows(i).Item("tanggal_mulai_real"), "yyyy-MM-dd") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("tanggal_selesai_real")) <> "" Then
								tanggal_selesai_real = "'" & Format(.Rows(i).Item("tanggal_selesai_real"), "yyyy-MM-dd") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("progress")) <> "" Then
								progress = "'" & .Rows(i).Item("progress") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("tanggal")) <> "" Then
								tanggal = "'" & Format(.Rows(i).Item("tanggal"), "yyyy-MM-dd") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("jam")) <> "" Then
								jam = "'" & .Rows(i).Item("jam") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("persen_toleransi")) <> "" Then
								persen_toleransi = "'" & .Rows(i).Item("persen_toleransi") & "'"
							End If

							If General_Class.CekNULL(.Rows(i).Item("bobot")) <> "" Then
								bobot = "'" & .Rows(i).Item("bobot") & "'"
							End If

							SQL = "update emi_proyek set kode_proyek = " & sub_pekerjaan & ", "
							SQL = SQL & "keterangan = " & sub_pekerjaan & ", "
							SQL = SQL & "target = " & target & ", "
							SQL = SQL & "tanggal_mulai = " & tanggal_mulai & ", "
							SQL = SQL & "tanggal_selesai = " & tanggal_selesai & ", "
							SQL = SQL & "tanggal_mulai_real = " & tanggal_mulai & ", "
							SQL = SQL & "tanggal_selesai_real = " & tanggal_selesai & ", "
							SQL = SQL & "progress = " & progress & ", "
							SQL = SQL & "tanggal = " & tanggal & ", "
							SQL = SQL & "jam = " & jam & ", "
							SQL = SQL & "persen_toleransi = " & persen_toleransi & ", "
							SQL = SQL & "bobot = " & bobot & " "
							SQL = SQL & "where id = '" & .Rows(i).Item("id") & "' "
							ExecuteTrans(SQL)
						Next
					End With
				End Using

				SQLMySQL = "update sub_pekerjaans set flag_sdh_pindah_ke_sql = 'Y', flag_edit_subpekerjaans = 'Y' "
				SQLMySQL = SQLMySQL & "where "
				SQLMySQL = SQLMySQL & "id = '" & arrEdit.Item(a).ToString & "' "
				ExecuteTransMySQL(SQLMySQL)
				a = a + 1
			Next

			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "edit web sub pekerjaan")
			Exit Sub
		End Try

	End Sub

	Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrHapus.Clear()
			SQLMySQL = "select id from proyeks where "
			SQLMySQL = SQLMySQL & "flag_sdh_pindah_ke_sql = 'Y' and deleted_at is not null and flag_sudah_hapus_di_sql is null "
			Using Ds = BindingTransMySQL(SQLMySQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrHapus.Add(.Rows(i).Item("id"))
					Next
				End With
			End Using

			Dim a As Integer = 0
			For b As Integer = 1 To arrHapus.Count
				SQLMySQL = "select id from proyeks "
				SQLMySQL = SQLMySQL & "where id = '" & arrHapus.Item(a).ToString & "' "
				SQLMySQL = SQLMySQL & "and flag_sdh_pindah_ke_sql = 'Y' "
				SQLMySQL = SQLMySQL & "and deleted_at is not null "
				SQLMySQL = SQLMySQL & "and flag_sudah_hapus_di_sql is null "

				Using DsMySQL = BindingTransMySQL(SQLMySQL)
					With DsMySQL.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							SQL = "delete from web_proyeks "
							SQL = SQL & "where id = '" & .Rows(i).Item("id") & "' "
							ExecuteTrans(SQL)
						Next
					End With
				End Using

				SQLMySQL = "update proyeks set flag_sudah_hapus_di_sql = 'Y' "
				SQLMySQL = SQLMySQL & "where "
				SQLMySQL = SQLMySQL & "id = '" & arrHapus.Item(a).ToString & "' "
				ExecuteTransMySQL(SQLMySQL)
				a = a + 1
			Next

			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "Delete web proyek")
			Exit Sub
		End Try
	End Sub

	Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrHapus.Clear()
			SQLMySQL = "select id from subproyeks where "
			SQLMySQL = SQLMySQL & "flag_sdh_pindah_ke_sql = 'Y' and deleted_at is not null and flag_sudah_hapus_di_sql is null "
			Using Ds = BindingTransMySQL(SQLMySQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrHapus.Add(.Rows(i).Item("id"))
					Next
				End With
			End Using

			Dim a As Integer = 0
			For b As Integer = 1 To arrHapus.Count
				SQLMySQL = "select id from subproyeks "
				SQLMySQL = SQLMySQL & "where id = '" & arrHapus.Item(a).ToString & "' "
				SQLMySQL = SQLMySQL & "and flag_sdh_pindah_ke_sql = 'Y' "
				SQLMySQL = SQLMySQL & "and deleted_at is not null "
				SQLMySQL = SQLMySQL & "and flag_sudah_hapus_di_sql is null "

				Using DsMySQL = BindingTransMySQL(SQLMySQL)
					With DsMySQL.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							SQL = "delete from web_subproyeks "
							SQL = SQL & "where id = '" & .Rows(i).Item("id") & "' "
							ExecuteTrans(SQL)
						Next
					End With
				End Using

				SQLMySQL = "update subproyeks set flag_sudah_hapus_di_sql = 'Y' "
				SQLMySQL = SQLMySQL & "where "
				SQLMySQL = SQLMySQL & "id = '" & arrHapus.Item(a).ToString & "' "
				ExecuteTransMySQL(SQLMySQL)
				a = a + 1
			Next

			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "Delete web sub proyek")
			Exit Sub
		End Try
	End Sub

	Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrHapus.Clear()
			SQLMySQL = "select id from pekerjaans where "
			SQLMySQL = SQLMySQL & "flag_sdh_pindah_ke_sql = 'Y' and deleted_at is not null and flag_sudah_hapus_di_sql is null "
			Using Ds = BindingTransMySQL(SQLMySQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrHapus.Add(.Rows(i).Item("id"))
					Next
				End With
			End Using

			Dim a As Integer = 0
			For b As Integer = 1 To arrHapus.Count
				SQLMySQL = "select id from pekerjaans "
				SQLMySQL = SQLMySQL & "where id = '" & arrHapus.Item(a).ToString & "' "
				SQLMySQL = SQLMySQL & "and flag_sdh_pindah_ke_sql = 'Y' "
				SQLMySQL = SQLMySQL & "and deleted_at is not null "
				SQLMySQL = SQLMySQL & "and flag_sudah_hapus_di_sql is null "

				Using DsMySQL = BindingTransMySQL(SQLMySQL)
					With DsMySQL.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							SQL = "delete from web_pekerjaans "
							SQL = SQL & "where id = '" & .Rows(i).Item("id") & "' "
							ExecuteTrans(SQL)
						Next
					End With
				End Using

				SQLMySQL = "update pekerjaans set flag_sudah_hapus_di_sql = 'Y' "
				SQLMySQL = SQLMySQL & "where "
				SQLMySQL = SQLMySQL & "id = '" & arrHapus.Item(a).ToString & "' "
				ExecuteTransMySQL(SQLMySQL)
				a = a + 1
			Next

			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "Delete web pekerjaan")
			Exit Sub
		End Try
	End Sub

	Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrHapus.Clear()
			SQLMySQL = "select id from sub_pekerjaans where "
			SQLMySQL = SQLMySQL & "flag_sdh_pindah_ke_sql = 'Y' and deleted_at is not null and flag_sudah_hapus_di_sql is null "
			Using Ds = BindingTransMySQL(SQLMySQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrHapus.Add(.Rows(i).Item("id"))
					Next
				End With
			End Using

			Dim a As Integer = 0
			For b As Integer = 1 To arrHapus.Count
				SQLMySQL = "select id from sub_pekerjaans "
				SQLMySQL = SQLMySQL & "where id = '" & arrHapus.Item(a).ToString & "' "
				SQLMySQL = SQLMySQL & "and flag_sdh_pindah_ke_sql = 'Y' "
				SQLMySQL = SQLMySQL & "and deleted_at is not null "
				SQLMySQL = SQLMySQL & "and flag_sudah_hapus_di_sql is null "

				Using DsMySQL = BindingTransMySQL(SQLMySQL)
					With DsMySQL.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1
							SQL = "delete from EMI_Proyek "
							SQL = SQL & "where id = '" & .Rows(i).Item("id") & "' "
							ExecuteTrans(SQL)
						Next
					End With
				End Using

				SQLMySQL = "update sub_pekerjaans set flag_sudah_hapus_di_sql = 'Y' "
				SQLMySQL = SQLMySQL & "where "
				SQLMySQL = SQLMySQL & "id = '" & arrHapus.Item(a).ToString & "' "
				ExecuteTransMySQL(SQLMySQL)
				a = a + 1
			Next

			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "Delete web sub pekerjaan")
			Exit Sub
		End Try
	End Sub

	Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click

		Dim listINSERT, listEDIT, listDELETE As New ArrayList

		Try

			OpenConnMySQL()

			'INSERT
			SQLMySQL = "select id, 'proyek' as dari from proyeks where flag_sdh_pindah_ke_sql is null "
			SQLMySQL = SQLMySQL & "union all "
			SQLMySQL = SQLMySQL & "Select id, 'sub_proyek' as dari from subproyeks where flag_sdh_pindah_ke_sql is null  "
			SQLMySQL = SQLMySQL & "union all "
			SQLMySQL = SQLMySQL & "select id, 'pekerjaan' as dari from pekerjaans where flag_sdh_pindah_ke_sql is null  "
			SQLMySQL = SQLMySQL & "union all "
			SQLMySQL = SQLMySQL & "select id, 'sub_pekerjaan' as dari from sub_pekerjaans where flag_sdh_pindah_ke_sql is null  "
			SQLMySQL = SQLMySQL & "union all "
			SQLMySQL = SQLMySQL & "select no_faktur, 'request_material' as dari from request_material where flag_sdh_pindah_ke_sql is null and flag_acc = 'Y' limit 0, 1 "
			Using DsMySQL = BindingTransMySQL(SQLMySQL)
				With DsMySQL.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						If .Rows(i).Item("dari") = "proyek" Then
							'Button1_Click(Button22, e)
							listINSERT.Add("proyek")
						ElseIf .Rows(i).Item("dari") = "sub_proyek" Then
							'Button4_Click(Button22, e)
							listINSERT.Add("sub_proyek")
						ElseIf .Rows(i).Item("dari") = "pekerjaan" Then
							'Button5_Click(Button22, e)
							listINSERT.Add("pekerjaan")
						ElseIf .Rows(i).Item("dari") = "sub_pekerjaan" Then
							'Button6_Click(Button22, e)
							listINSERT.Add("sub_pekerjaan")
						ElseIf .Rows(i).Item("dari") = "request_material" Then
							'Button7_Click(Button22, e)
							listINSERT.Add("request_material")
						End If
					Next
				End With
			End Using

			'EDIT
			SQLMySQL = "select id, 'proyek' as dari from proyeks where "
			SQLMySQL = SQLMySQL & "flag_sdh_pindah_ke_sql = 'Y' and flag_edit_proyeks is null  "
			SQLMySQL = SQLMySQL & "union all "
			SQLMySQL = SQLMySQL & "select id, 'sub_proyek' as dari from subproyeks where "
			SQLMySQL = SQLMySQL & "flag_sdh_pindah_ke_sql = 'Y' and flag_edit_subproyeks is null  "
			SQLMySQL = SQLMySQL & "union all "
			SQLMySQL = SQLMySQL & "select id, 'pekerjaan' as dari from pekerjaans where "
			SQLMySQL = SQLMySQL & "flag_sdh_pindah_ke_sql = 'Y' and flag_edit_pekerjaans is null  "
			SQLMySQL = SQLMySQL & "union all "
			SQLMySQL = SQLMySQL & "select id, 'sub_pekerjaan' as dari from sub_pekerjaans where "
			SQLMySQL = SQLMySQL & "flag_sdh_pindah_ke_sql = 'Y' and flag_edit_subpekerjaans is null  "
			SQLMySQL = SQLMySQL & "union all "
			SQLMySQL = SQLMySQL & "select 1 as id, 'rab_pembelian_proyek' as dari from rab_pembelian_proyek where "
			SQLMySQL = SQLMySQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and flag_edit_rab = 'Y'  "
			SQLMySQL = SQLMySQL & "union all "
			SQLMySQL = SQLMySQL & "select 1 as id, 'po_pembelian_proyek' as dari from po_pembelian_proyek where "
			SQLMySQL = SQLMySQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and flag_edit_po = 'Y' "
			SQLMySQL = SQLMySQL & "union all "
			SQLMySQL = SQLMySQL & "select 1 as id, 'penjualan_proyek_sementara' as dari from penjualan_proyek_sementara where "
			SQLMySQL = SQLMySQL & " flag_sdh_update = 'Y' "
			SQLMySQL = SQLMySQL & "union all "
			SQLMySQL = SQLMySQL & "select 1 as id, 'approval_penjualan_proyek' as dari from approval_penjualan_proyek where  flag_WA is null "
			SQLMySQL = SQLMySQL & "union all "
			SQLMySQL = SQLMySQL & "select 1 as id, 'approval_po_pembelian_proyek' as dari from approval_po_pembelian_proyek where  flag_WA is null "
			'SQLMySQL = SQLMySQL & " limit 0, 1 "
			Using DsMySQL = BindingTransMySQL(SQLMySQL)
				With DsMySQL.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						If .Rows(i).Item("dari") = "proyek" Then
							'Button14_Click(Button22, e)
							listEDIT.Add("proyek")

						ElseIf .Rows(i).Item("dari") = "sub_proyek" Then
							'Button15_Click(Button22, e)
							listEDIT.Add("sub_proyek")

						ElseIf .Rows(i).Item("dari") = "pekerjaan" Then
							'Button17_Click(Button22, e)
							listEDIT.Add("pekerjaan")

						ElseIf .Rows(i).Item("dari") = "sub_pekerjaan" Then
							'Button16_Click(Button22, e)
							listEDIT.Add("sub_pekerjaan")

						ElseIf .Rows(i).Item("dari") = "rab_pembelian_proyek" Then
							'Button12_Click(Button22, e)
							listEDIT.Add("rab_pembelian_proyek")

						ElseIf .Rows(i).Item("dari") = "po_pembelian_proyek" Then
							'Button13_Click(Button22, e)
							listEDIT.Add("po_pembelian_proyek")

						ElseIf .Rows(i).Item("dari") = "penjualan_proyek_sementara" Then
							listEDIT.Add("penjualan_proyek_sementara")

						ElseIf .Rows(i).Item("dari") = "approval_penjualan_proyek" Then
							listEDIT.Add("approval_penjualan_proyek")

						ElseIf .Rows(i).Item("dari") = "approval_po_pembelian_proyek" Then
							listEDIT.Add("approval_po_pembelian_proyek")

						End If
					Next
				End With
			End Using

			'DELETE
			SQLMySQL = "select id, 'proyek' as dari from proyeks where "
			SQLMySQL = SQLMySQL & "flag_sdh_pindah_ke_sql = 'Y' and deleted_at is not null and flag_sudah_hapus_di_sql is null "
			SQLMySQL = SQLMySQL & "union all "
			SQLMySQL = SQLMySQL & "select id, 'sub_proyek' as dari from subproyeks where "
			SQLMySQL = SQLMySQL & "flag_sdh_pindah_ke_sql = 'Y' and deleted_at is not null and flag_sudah_hapus_di_sql is null "
			SQLMySQL = SQLMySQL & "union all "
			SQLMySQL = SQLMySQL & "select id, 'pekerjaan' as dari from pekerjaans where "
			SQLMySQL = SQLMySQL & "flag_sdh_pindah_ke_sql = 'Y' and deleted_at is not null and flag_sudah_hapus_di_sql is null "
			SQLMySQL = SQLMySQL & "union all "
			SQLMySQL = SQLMySQL & "select id, 'sub_pekerjaan' as dari from sub_pekerjaans where "
			SQLMySQL = SQLMySQL & "flag_sdh_pindah_ke_sql = 'Y' and deleted_at is not null and flag_sudah_hapus_di_sql is null "
			Using DsMySQL = BindingTransMySQL(SQLMySQL)
				With DsMySQL.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						If .Rows(i).Item("dari") = "proyek" Then
							'Button1_Click(Button22, e)
							listDELETE.Add("proyek")
						ElseIf .Rows(i).Item("dari") = "sub_proyek" Then
							'Button4_Click(Button22, e)
							listDELETE.Add("sub_proyek")
						ElseIf .Rows(i).Item("dari") = "pekerjaan" Then
							'Button5_Click(Button22, e)
							listDELETE.Add("pekerjaan")
						ElseIf .Rows(i).Item("dari") = "sub_pekerjaan" Then
							'Button6_Click(Button22, e)
							listDELETE.Add("sub_pekerjaan")
						End If
					Next
				End With
			End Using

			CloseConnMySQL()
		Catch ex As Exception
			CloseTransMySQL()
			CloseConnMySQL()

			Dim Lvw As ListViewItem
			Lvw = ListView1.Items.Add("MySql-Sql : " & ex.Message)
			'MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'Execute INSERT
		For index As Integer = 0 To listINSERT.Count - 1
			Dim item As Object = listINSERT(index)
			If item = "proyek" Then
				Button1_Click(Button22, e)

			ElseIf item = "sub_proyek" Then
				Button4_Click(Button22, e)

			ElseIf item = "pekerjaan" Then
				Button5_Click(Button22, e)

			ElseIf item = "sub_pekerjaan" Then
				Button6_Click(Button22, e)

			ElseIf item = "request_material" Then
				Button7_Click(Button22, e)

			End If
		Next

		'Execute EDIT
		For index As Integer = 0 To listEDIT.Count - 1
			Dim item As Object = listEDIT(index)
			If item = "proyek" Then
				Button14_Click(Button22, e)

			ElseIf item = "sub_proyek" Then
				Button15_Click(Button22, e)

			ElseIf item = "pekerjaan" Then
				Button17_Click(Button22, e)

			ElseIf item = "sub_pekerjaan" Then
				Button16_Click(Button22, e)

			ElseIf item = "rab_pembelian_proyek" Then
				Button12_Click(Button22, e)

			ElseIf item = "po_pembelian_proyek" Then
				Button13_Click(Button22, e)

			ElseIf item = "penjualan_proyek_sementara" Then
				Button25_Click(Button22, e)

			ElseIf item = "approval_penjualan_proyek" Then
				Button26_Click(Button22, e)

			ElseIf item = "approval_po_pembelian_proyek" Then
				Button28_Click(Button22, e)

			End If
		Next

		'Execute DELETE
		For index As Integer = 0 To listDELETE.Count - 1
			Dim item As Object = listDELETE(index)
			If item = "proyek" Then
				Button21_Click(Button22, e)

			ElseIf item = "sub_proyek" Then
				Button20_Click(Button22, e)

			ElseIf item = "pekerjaan" Then
				Button19_Click(Button22, e)

			ElseIf item = "sub_pekerjaan" Then
				Button18_Click(Button22, e)

			End If
		Next

	End Sub

	Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
		get_jam()

		Try
			OpenConn()
			OpenConnMySQL()

			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			Dim Tanggal As String = Format(tgl_skg, "yyyy-MM-dd")
			Dim Jam As String = Format(tgl_skg, "HH:mm:ss")

			SQL = "
            WITH CTE AS (
                SELECT
                    Kode_Perusahaan,
                    Kode_Stock_Owner,
                    Kode_Barang,
                    Satuan,
                    ROW_NUMBER() OVER (
                        PARTITION BY Kode_Perusahaan, Kode_Barang
                        ORDER BY Kode_Stock_Owner
                    ) AS RN
                FROM Barang_Lain
                WHERE flag_sudah_sync_web IS NULL
            )
            SELECT
                Kode_Perusahaan,
                Kode_Stock_Owner,
                Kode_Barang,
                Satuan
            FROM CTE
            WHERE RN = 1"
			Dim Ds As DataSet = BindingTrans(SQL)

			For Each Row As DataRow In Ds.Tables("MyTable").Rows
				Dim Satuan As String
				If General_Class.CekNULL(Row("Satuan")) = "" Then
					Satuan = "NULL"
				Else
					Satuan = $"'{Replace(Row("Satuan").ToString, "'", "''")}'"
				End If

				SQLMySQL = $"
                       INSERT INTO Barang_Lain (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Satuan) VALUES (
        				'{Row("Kode_Perusahaan")}', '{Row("Kode_Stock_Owner")}', '{Row("Kode_Barang")}', {Satuan}
        			)"
				ExecuteTransMySQL(SQLMySQL)

				SQL = $"
                       UPDATE Barang_Lain SET
                               flag_sudah_sync_web = 'Y',
                               tanggal_sync_web = '{Tanggal}',
                               jam_sync_web = '{Jam}'
                       WHERE Kode_Perusahaan = '{Row("Kode_Perusahaan")}' AND Kode_Stock_Owner = '{Row("Kode_Stock_Owner")}' AND Kode_Barang = '{Row("Kode_Barang")}'"
				ExecuteTrans(SQL)
			Next

			CmdMySQL.Transaction.Commit()
			Cmd.Transaction.Commit()

			CloseConnMySQL()
			CloseConn()
		Catch ex As Exception
			CloseTransMySQL()
			CloseTrans()
			CloseConnMySQL()
			CloseConn()
			Dim Lvw As ListViewItem
			Lvw = ListView1.Items.Add("Gagal sync data Barang Lain: " & ex.Message)
		End Try
	End Sub

	Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click

		Dim listINSERT, listEDIT As New ArrayList

		Try
			OpenConn()

			'INSERT
			SQL = "select top(1) kode_barang, 'barang' as dari from barang_proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null "
			SQL = SQL & "union all "
			SQL = SQL & "Select top(1) no_faktur, 'rab_pembelian_proyek' as dari from rab_pembelian_proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null "
			SQL = SQL & "union all "
			SQL = SQL & "select top(1) kode_supplier, 'supplier' as dari from suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null "
			SQL = SQL & "union all "
			SQL = SQL & "select top(1) no_faktur, 'barang_masuk' as dari from barang_masuk_proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null "
			SQL = SQL & "union all "
			SQL = SQL & "select top(1) no_faktur, 'po_pembelian_proyek' as dari from po_pembelian_proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null "
			SQL = SQL & "union all "
			SQL = SQL & "select top(1) no_faktur, 'penjualan_proyek' as dari from penjualan_proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null "
			SQL = SQL & "union all "
			SQL = SQL & "select top(1) no_faktur, 'penjualan_proyek_sementara' as dari from penjualan_proyek_sementara where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null "
			SQL = SQL & "union all "
			SQL = SQL & "select top(1) no_val, 'val_pemb_proyek' as dari from val_pemb_proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah is null "
			SQL = SQL & "union all "
			SQL = SQL & "select top(1) kode_barang, 'barang_lain' as dari from barang_lain where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sudah_sync_web is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						If .Rows(i).Item("dari") = "barang" Then
							listINSERT.Add("barang")
						ElseIf .Rows(i).Item("dari") = "rab_pembelian_proyek" Then
							listINSERT.Add("rab_pembelian_proyek")
						ElseIf .Rows(i).Item("dari") = "supplier" Then
							listINSERT.Add("supplier")
						ElseIf .Rows(i).Item("dari") = "barang_masuk" Then
							listINSERT.Add("barang_masuk")
						ElseIf .Rows(i).Item("dari") = "po_pembelian_proyek" Then
							listINSERT.Add("po_pembelian_proyek")
						ElseIf .Rows(i).Item("dari") = "penjualan_proyek" Then
							listINSERT.Add("penjualan_proyek")
						ElseIf .Rows(i).Item("dari") = "penjualan_proyek_sementara" Then
							listINSERT.Add("penjualan_proyek_sementara")
						ElseIf .Rows(i).Item("dari") = "val_pemb_proyek" Then
							listINSERT.Add("val_pemb_proyek")
						ElseIf .Rows(i).Item("dari") = "barang_lain" Then
							listINSERT.Add("barang_lain")
						End If
					Next
				End With
			End Using

			'EDIT
			SQL = "select top(1) no_faktur, 'rab_pembelian_proyek' as dari from rab_pembelian_proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_sdh_pindah = 'Y' and flag_edit = 'Y' "
			SQL = SQL & "union all "
			SQL = SQL & "select top(1) kode_barang, 'barang_proyek' as dari from barang_proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_update_web='Y' "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						If .Rows(i).Item("dari") = "rab_pembelian_proyek" Then
							listEDIT.Add("rab_pembelian_proyek")
						ElseIf .Rows(i).Item("dari") = "barang_proyek" Then
							listEDIT.Add("barang_proyek")
						End If
					Next
				End With
			End Using

			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()

			Dim Lvw As ListViewItem
			Lvw = ListView1.Items.Add("Sql-MySql : " & ex.Message)

			'MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'Execute INSERT
		For index As Integer = 0 To listINSERT.Count - 1
			Dim item As Object = listINSERT(index)
			If item = "barang" Then
				Button2_Click(Button22, e)
				Button29_Click(Button22, e)

			ElseIf item = "rab_pembelian_proyek" Then
				Button3_Click(Button22, e)

			ElseIf item = "supplier" Then
				Button9_Click(Button22, e)

			ElseIf item = "barang_masuk" Then
				btn_KeBrgMskMYSQL_Click(Button22, e)

			ElseIf item = "po_pembelian_proyek" Then
				Button11_Click(Button22, e)

			ElseIf item = "penjualan_proyek" Then
				Button10_Click(Button22, e)

			ElseIf item = "penjualan_proyek_sementara" Then
				Button24_Click(Button22, e)

			ElseIf item = "val_pemb_proyek" Then
				Button27_Click(Button22, e)

			ElseIf item = "barang_lain" Then
				Button30_Click(Button30, e)

			End If
		Next

		'Execute EDIT
		For index As Integer = 0 To listEDIT.Count - 1
			Dim item As Object = listEDIT(index)
			If item = "rab_pembelian_proyek" Then
				Button8_Click(Button22, e)
			ElseIf item = "barang_proyek" Then
				Button29_Click(Button22, e)
			End If
		Next

	End Sub

	Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click
		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

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
						SQLMySQL = "insert into penjualan_proyek_sementara (kode_perusahaan,no_faktur,tanggal,jam, "
						SQLMySQL = SQLMySQL & "userid,disc_cash,lokasi,hasil_diskon_cash, "
						SQLMySQL = SQLMySQL & "kode_voucher_2,flag_cabang_agency,disc_tambahan,hasil_disc_tambahan, "
						SQLMySQL = SQLMySQL & "persen_insentif_1,persen_insentif_2,nilai_insentif_1,nilai_insentif_2, "
						SQLMySQL = SQLMySQL & "nilai_val_diskon_cash,diskon_sementara,nilai_diskon_sementara,lama_diskon_sementara, "
						SQLMySQL = SQLMySQL & "metode_pot_stock,tgl_input,sub_pekerjaan_id, keterangan)"
						SQLMySQL = SQLMySQL & "values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "', '" & Format(.Rows(i).Item("tanggal"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("jam") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("userid") & "', '" & .Rows(i).Item("disc_cash") & "', '" & .Rows(i).Item("lokasi") & "', '" & .Rows(i).Item("hasil_diskon_cash") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("kode_voucher_2") & "', '" & .Rows(i).Item("flag_cabang_agency") & "', '" & .Rows(i).Item("disc_tambahan") & "', '" & .Rows(i).Item("hasil_disc_tambahan") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("persen_insentif_1") & "', '" & .Rows(i).Item("persen_insentif_2") & "', '" & .Rows(i).Item("nilai_insentif_1") & "', '" & .Rows(i).Item("nilai_insentif_2") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("nilai_val_diskon_cash") & "', '" & .Rows(i).Item("diskon_sementara") & "', '" & .Rows(i).Item("nilai_diskon_sementara") & "', '" & .Rows(i).Item("lama_diskon_sementara") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("metode_pot_stock") & "', '" & Format(.Rows(i).Item("tgl_input"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("id_sub_pekerjaan") & "', '" & .Rows(i).Item("keterangan") & "') "
						ExecuteTransMySQL(SQLMySQL)

						'----Insert Approval ('RandomPIN 6 Digit,'RandomKodeUnik, RandomKodeUrl 10 Digit)

						SQLMySQL = "select id from users where flag_acc_pengeluaran ='Y'"
						Using DsMySQL = BindingTransMySQL(SQLMySQL)
							For indexxx As Integer = 0 To DsMySQL.Tables("MyTable").Rows.Count - 1

								Dim randomNumber As New Random()
								Dim randomPIN As Integer = randomNumber.Next(100000, 1000000)
								Dim randomKdUnik As Integer = randomNumber.Next(1000000000, Integer.MaxValue)
								Dim randomKdUrl As Integer = randomNumber.Next(1000000000, Integer.MaxValue)

								SQLMySQL = "insert into approval_penjualan_proyek (penjualan_proyek_id, pin, kode_unik, kode_url, user_id) "
								SQLMySQL = SQLMySQL & "values ('" & .Rows(i).Item("no_faktur") & "', '" & randomPIN.ToString & "', '" & randomKdUnik.ToString & "', '" & randomKdUrl.ToString & "','" & DsMySQL.Tables("MyTable").Rows(indexxx).Item("id") & "')"
								ExecuteTransMySQL(SQLMySQL)

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

							SQLMySQL = "insert into detail_penjualan_proyek_sementara (kode_perusahaan,no_faktur,no_urut,kode_stock_owner,kode_barang,jumlah) "
							SQLMySQL = SQLMySQL & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "', '" & .Rows(i).Item("no_urut") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("kode_stock_owner") & "', '" & .Rows(i).Item("kode_barang") & "', '" & .Rows(i).Item("jumlah") & "') "
							ExecuteTransMySQL(SQLMySQL)

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

							SQLMySQL = "insert into det_penj_proyek_sementara (kode_perusahaan, no_faktur, no_urut, kode_stock_owner, kode_barang, serial_number, "
							SQLMySQL = SQLMySQL & "jumlah, proyek_id, sub_proyek_id, urut_oto,proyek,subproyek) "
							SQLMySQL = SQLMySQL & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_faktur") & "',"
							SQLMySQL = SQLMySQL & " '" & .Rows(i).Item("no_urut") & "', '" & .Rows(i).Item("kode_stock_owner") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("kode_barang") & "', '" & .Rows(i).Item("serial_number") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("jumlah") & "', "
							'Id Proyek
							If General_Class.CekNULL((.Rows(i).Item("id_proyek"))) = "" Then
								SQLMySQL = SQLMySQL & "NULL, "
							Else
								SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("id_proyek") & "', "
							End If
							'Id SubProyek
							If General_Class.CekNULL((.Rows(i).Item("id_subproyek"))) = "" Then
								SQLMySQL = SQLMySQL & "NULL, "
							Else
								SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("id_subproyek") & "', "
							End If
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("urut_oto") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("proyek") & "', "
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("subproyek") & "')"
							ExecuteTransMySQL(SQLMySQL)

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
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "insert penjualan proyek sementara")
			Exit Sub
		End Try
	End Sub

	Private Sub Button25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button25.Click
		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			arrEdit.Clear()
			SQLMySQL = "select no_faktur from penjualan_proyek_sementara where "
			SQLMySQL = SQLMySQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
			SQLMySQL = SQLMySQL & " flag_sdh_update = 'Y' "
			Using Ds = BindingTransMySQL(SQLMySQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrEdit.Add(.Rows(i).Item("no_faktur"))
					Next
				End With
			End Using

			Dim a As Integer = 0
			For b As Integer = 1 To arrEdit.Count
				SQLMySQL = "select kode_perusahaan, no_faktur, flag_acc , tanggal_acc, jam_acc from penjualan_proyek_sementara rpp "
				SQLMySQL = SQLMySQL & "where kode_perusahaan ='" & KodePerusahaan & "' and no_faktur ='" & arrEdit.Item(a).ToString & "' and flag_sdh_update ='Y' "
				Using DsMySQL = BindingTransMySQL(SQLMySQL)
					With DsMySQL.Tables("MyTable")
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

				SQLMySQL = "update penjualan_proyek_sementara set flag_sdh_update = null where "
				SQLMySQL = SQLMySQL & "Kode_Perusahaan = '" & KodePerusahaan & "'  and "
				SQLMySQL = SQLMySQL & "No_faktur = '" & arrEdit.Item(a).ToString & "' "
				ExecuteTransMySQL(SQLMySQL)
				a = a + 1
			Next

			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "edit penjualan web")
			Exit Sub
		End Try
	End Sub

	Private Sub Button26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button26.Click
		Try
			OpenConn()
			OpenConnMySQL()

			arrEdit.Clear()
			SQLMySQL = "select id from approval_penjualan_proyek where "
			SQLMySQL = SQLMySQL & " Flag_WA is null "
			Using Ds = BindingTransMySQL(SQLMySQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrEdit.Add(.Rows(i).Item("id"))
					Next
				End With
			End Using

			Dim a As Integer = 0
			For b As Integer = 1 To arrEdit.Count

				Cmd.Transaction = Cn.BeginTransaction
				CmdMySQL.Transaction = CnMySQL.BeginTransaction

				SQLMySQL = "select kode_url from approval_penjualan_proyek rpp "
				SQLMySQL = SQLMySQL & "where id ='" & arrEdit.Item(a).ToString & "' and Flag_WA is null "
				Using DsMySQL = BindingTransMySQL(SQLMySQL)
					With DsMySQL.Tables("MyTable")
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
								CloseTransMySQL()
								CloseConn()
								CloseConnMySQL()
								MessageBox.Show(xMessage(1).Trim)
								MessageBox.Show(xcode(1).Trim)
								Exit Sub
							End If

						Next
					End With
				End Using

				SQLMySQL = "update approval_penjualan_proyek set Flag_WA='Y' "
				SQLMySQL = SQLMySQL & "where id='" & arrEdit.Item(a).ToString & "'"
				ExecuteTransMySQL(SQLMySQL)

				Cmd.Transaction.Commit()
				CmdMySQL.Transaction.Commit()

				a = a + 1
			Next

			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "edit approval penjualan sementara")
			Exit Sub
		End Try
	End Sub

	Private Sub Button28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button28.Click
		Try
			OpenConn()
			OpenConnMySQL()

			arrEdit.Clear()
			SQLMySQL = "select id from approval_po_pembelian_proyek where "
			SQLMySQL = SQLMySQL & " Flag_WA is null "
			Using Ds = BindingTransMySQL(SQLMySQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						arrEdit.Add(.Rows(i).Item("id"))
					Next
				End With
			End Using

			Dim a As Integer = 0
			For b As Integer = 1 To arrEdit.Count

				Cmd.Transaction = Cn.BeginTransaction
				CmdMySQL.Transaction = CnMySQL.BeginTransaction

				SQLMySQL = "select kode_url from approval_po_pembelian_proyek rpp "
				SQLMySQL = SQLMySQL & "where id ='" & arrEdit.Item(a).ToString & "' and Flag_WA is null "
				Using DsMySQL = BindingTransMySQL(SQLMySQL)
					With DsMySQL.Tables("MyTable")
						For i As Integer = 0 To .Rows.Count - 1

							Dim Request As HttpWebRequest
							Dim Response As HttpWebResponse
							Dim responseReader As StreamReader
							Dim result As String
							Dim custom_uid As String = ""
							'Dim param_wa As String = "192.168.13.23:8000/api/sentWaPembelianProyek/12341234"
							'2112444619:
							Dim param_wa As String = "https://pro.evomanufacturingindonesia.id/api/sentWaPembelianProyek/" & .Rows(i).Item("kode_url")
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
								CloseTransMySQL()
								CloseConn()
								CloseConnMySQL()
								MessageBox.Show(xMessage(1).Trim)
								MessageBox.Show(xcode(1).Trim)
								Exit Sub
							End If

						Next
					End With
				End Using

				SQLMySQL = "update approval_po_pembelian_proyek set Flag_WA='Y' "
				SQLMySQL = SQLMySQL & "where id='" & arrEdit.Item(a).ToString & "'"
				ExecuteTransMySQL(SQLMySQL)

				Cmd.Transaction.Commit()
				CmdMySQL.Transaction.Commit()

				a = a + 1
			Next

			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "edit approval PO Pembelian Proyek")
			Exit Sub
		End Try
	End Sub

	Private Sub Button27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button27.Click
		Try
			OpenConn()
			OpenConnMySQL()
			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

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
						SQLMySQL = "insert into val_pemb_proyek (kode_perusahaan,no_val,tanggal,jam, "
						SQLMySQL = SQLMySQL & "keterangan,uservalidasi,grand,kode_bank_tujuan,no_rek_tujuan,nama_penerima,alamat_penerima, "
						SQLMySQL = SQLMySQL & "kota_penerima,negara_penerima,telp_penerima,no_pengajuan )"
						SQLMySQL = SQLMySQL & "values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_val") & "',"
						SQLMySQL = SQLMySQL & " '" & Format(.Rows(i).Item("tanggal"), "yyyy-MM-dd") & "', '" & .Rows(i).Item("jam") & "', "
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Keterangan") & "', '" & .Rows(i).Item("UserValidasi") & "', '" & .Rows(i).Item("grand") & "',"
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Kode_Bank_Tujuan") & "', '" & .Rows(i).Item("No_Rek_Tujuan") & "',"
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Nama_Penerima") & "', '" & .Rows(i).Item("Alamat_Penerima") & "',"
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Kota_Penerima") & "', '" & .Rows(i).Item("Negara_Penerima") & "',"
						SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("Telp_Penerima") & "', '" & .Rows(i).Item("No_Pengajuan") & "') "
						ExecuteTransMySQL(SQLMySQL)

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

							SQLMySQL = "insert into detail_val_pemb_proyek (kode_perusahaan,no_val,no_faktur,byr,urut) "
							SQLMySQL = SQLMySQL & "Values ('" & .Rows(i).Item("kode_perusahaan") & "', '" & .Rows(i).Item("no_val") & "',"
							SQLMySQL = SQLMySQL & "'" & .Rows(i).Item("no_faktur") & "', '" & .Rows(i).Item("byr") & "', '" & .Rows(i).Item("urut") & "') "
							ExecuteTransMySQL(SQLMySQL)

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
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
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

	Private Sub Sync_Harian_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Sync_Harian.Click
		Try
			OpenConn()
			OpenConnMySQL()

			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			SQL = "Select Kode_Perusahaan,Kode_Barang,Kode_Stock_Owner,Nama,'0',Satuan,good_stock,kode_kategori_besar,kode_kategori_kecil "
			SQL = SQL & "from Barang_Proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'SQL = SQL & "Kode_Stock_Owner = '" & arrSO.Item(j).ToString & "' and "
			'SQL = SQL & "Kode_Barang = '" & arrKd_Brg.Item(j).ToString & "' and "
			SQL = SQL & "flag_sdh_pindah = 'Y' "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						'Update Stock ke MySQL, kode_barang, kode_stock_owner
						SQLMySQL = "update barang set "
						SQLMySQL = SQLMySQL & "good_stock = '" & .Rows(i).Item("good_stock") & "' "
						SQLMySQL = SQLMySQL & "where kode_perusahaan = '" & KodePerusahaan & "'  and "
						SQLMySQL = SQLMySQL & "kode_stock_owner = '" & .Rows(i).Item("kode_stock_owner") & "' and "
						SQLMySQL = SQLMySQL & "kode_barang = '" & .Rows(i).Item("kode_barang") & "' "
						ExecuteTransMySQL(SQLMySQL)
					Next
				End With
			End Using

			''Insert ke table log
			Dim currentDate As String = Now.Date.ToString("yyyy-MM-dd")
			Dim currentTime As String = Now.ToLocalTime().ToString("HH:mm:ss")
			SQLMySQL = "insert into barang_log_sinkronisasi "
			SQLMySQL = SQLMySQL & "(tanggal, jam, userid, flag_sdh_sinkron) "
			SQLMySQL = SQLMySQL & "Values ('" & currentDate & "', '" & currentTime & "', '" & UserID & "' , 'Y')"
			ExecuteTransMySQL(SQLMySQL)

			''INSERT KE LOG HARIAN

			Dim CurrentHour As String = Tanggal_Sekarang.ToString("HH:mm:ss")
			SQL = "insert into Update_Barang_Web_Log_Harian (Kode_Perusahaan, Tanggal, Jam) values "
			SQL = SQL & "('" & KodePerusahaan & "', '" & Tanggal_Sekarang & "', '" & CurrentHour & "')"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "insert update barang web")
			Exit Sub
		End Try
	End Sub

	Private Sub Button29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button29.Click
		Try
			OpenConn()
			OpenConnMySQL()

			Cmd.Transaction = Cn.BeginTransaction
			CmdMySQL.Transaction = CnMySQL.BeginTransaction

			'arrKd_Brg.Clear() : arrSO.Clear()
			'SQL = "select Kode_Barang, Kode_Stock_Owner from Barang_Proyek where "
			'SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'SQL = SQL & "flag_update_web is null "
			'Using Ds = BindingTrans(SQL)
			'    With Ds.Tables("MyTable")
			'        For i As Integer = 0 To .Rows.Count - 1
			'            arrKd_Brg.Add(.Rows(i).Item("Kode_Barang"))
			'            arrSO.Add(.Rows(i).Item("Kode_Stock_Owner"))
			'        Next
			'    End With
			'End Using

			'Dim j As Integer = 0

			SQL = "Select Kode_Perusahaan,Kode_Barang,Kode_Stock_Owner,Nama,'0',Satuan,good_stock,kode_kategori_besar,kode_kategori_kecil "
			SQL = SQL & "from Barang_Proyek where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			'SQL = SQL & "Kode_Stock_Owner = '" & arrSO.Item(j).ToString & "' and "
			'SQL = SQL & "Kode_Barang = '" & arrKd_Brg.Item(j).ToString & "' and "
			SQL = SQL & "flag_update_web = 'Y' "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						'Update Stock ke MySQL, kode_barang, kode_stock_owner
						SQLMySQL = "update barang set "
						SQLMySQL = SQLMySQL & "good_stock = '" & .Rows(i).Item("good_stock") & "' "
						SQLMySQL = SQLMySQL & "where kode_perusahaan = '" & KodePerusahaan & "'  and "
						SQLMySQL = SQLMySQL & "kode_stock_owner = '" & .Rows(i).Item("kode_stock_owner") & "' and "
						SQLMySQL = SQLMySQL & "kode_barang = '" & .Rows(i).Item("kode_barang") & "' "
						ExecuteTransMySQL(SQLMySQL)

						'SET Flag_Update_Web = NULL
						SQL = "update Barang_Proyek set Flag_Update_web = NULL where Kode_Perusahaan ='" & KodePerusahaan & "' "
						SQL = SQL & "and Kode_Stock_Owner='" & .Rows(i).Item("kode_stock_owner") & "' and Kode_Barang='" & .Rows(i).Item("kode_barang") & "'"
						ExecuteTrans(SQL)
					Next
				End With
			End Using

			''Insert ke table log
			Dim currentDate As String = Now.Date.ToString("yyyy-MM-dd")
			Dim currentTime As String = Now.ToLocalTime().ToString("HH:mm:ss")
			SQLMySQL = "insert into barang_log_sinkronisasi "
			SQLMySQL = SQLMySQL & "(tanggal, jam, userid, flag_sdh_sinkron) "
			SQLMySQL = SQLMySQL & "Values ('" & currentDate & "', '" & currentTime & "', '" & UserID & "' , 'Y')"
			ExecuteTransMySQL(SQLMySQL)

			'MessageBox.Show("simpan berhasil", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmd.Transaction.Commit()
			CmdMySQL.Transaction.Commit()
			CloseConn()
			CloseConnMySQL()
		Catch ex As Exception
			CloseTrans()
			CloseTransMySQL()
			CloseConn()
			CloseConnMySQL()
			MessageBox.Show(ex.Message & "insert update barang web")
			Exit Sub
		End Try
	End Sub

End Class