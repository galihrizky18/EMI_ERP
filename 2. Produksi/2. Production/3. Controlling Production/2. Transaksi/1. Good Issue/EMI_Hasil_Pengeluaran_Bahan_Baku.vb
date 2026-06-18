Imports System.Globalization

Public Class EMI_Hasil_Pengeluaran_Bahan_Baku
	Public fno_po, fso As String

	Dim Arr_Detail_Biaya As New List(Of (akun As String, keterangan As String, nilai As Double, kd_so As String, kd_barang As String))

	Dim CellKode_So As Integer = 0
	Dim CellKode_So_Pckg As Integer = 0
	Dim CellKode_Bahan As Integer = 1
	Dim CellKode_Bahan_Pckg As Integer = 1
	Dim CellNilai_Formula As Integer = 2
	Dim CellNilai_Formula_Pckg As Integer = 2
	Dim CellNilai_Produksi As Integer = 3
	Dim CellNilai_Produksi_Pckg As Integer = 3
	Dim CellPotStokBhn As Integer = 5
	Dim CellPotStokPckg As Integer = 5
	Dim CellSatuan As Integer = 4
	Dim CellSatuan_Pckg As Integer = 4
	Dim CellStandarPrice As Integer = 6
	Dim CellStandarPricePckg As Integer = 6
	Dim Jenis = "Display_Production_Order"
	Dim LvKode_Bahan As String
	Dim LvKode_Bahan_Pckg As String
	Dim LvKode_So As String
	Dim LvKode_So_Pckg As String
	Dim LvNilai_Formula As String

	'  Dim LvNama_Bahan_Pckg As String
	Dim LvNilai_Formula_Pckg As String

	Dim LvNilai_Produksi As String
	Dim LvNilai_Produksi_Pckg As String
	Dim LvPotStokBhn As String
	Dim LvPotStokPckg As String
	Dim LvSatuan As String
	Dim LvSatuan_Pckg As String
	Dim LvStandarPrice As String
	Dim LvStandarPricePckg As String

	Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
		LvKode_So = Dgv_HslProduction.Rows(No_Index).Cells(CellKode_So).Value
		LvKode_Bahan = Dgv_HslProduction.Rows(No_Index).Cells(CellKode_Bahan).Value
		LvNilai_Formula = Dgv_HslProduction.Rows(No_Index).Cells(CellNilai_Formula).Value
		LvNilai_Produksi = Dgv_HslProduction.Rows(No_Index).Cells(CellNilai_Produksi).Value
		LvSatuan = Dgv_HslProduction.Rows(No_Index).Cells(CellSatuan).Value
		LvPotStokBhn = Dgv_HslProduction.Rows(No_Index).Cells(CellPotStokBhn).Value
		LvStandarPrice = Dgv_HslProduction.Rows(No_Index).Cells(CellStandarPrice).Value
	End Sub

	Public Sub Get_Isi_Listview_Pckg(ByVal No_Index As Integer)

		LvKode_So_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellKode_So_Pckg).Value
		LvKode_Bahan_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellKode_Bahan_Pckg).Value

		LvNilai_Formula_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellNilai_Formula_Pckg).Value
		LvNilai_Produksi_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellNilai_Produksi_Pckg).Value
		LvSatuan_Pckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellSatuan_Pckg).Value
		LvPotStokPckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellPotStokPckg).Value
		LvStandarPricePckg = Dgv_Hasil_Production_Packaging.Rows(No_Index).Cells(CellStandarPricePckg).Value
	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If TxtFormulator_NoFaktur.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Global_Error_No_Transaksi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TxtFormulator_NoFaktur.Focus() : Exit Sub
			'ElseIf TextBox5.Text.Trim.Length = 0 Then
			'    MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty1, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    TextBox5.Focus() : Exit Sub
			'ElseIf TextBox8.Text.Trim.Length = 0 Then
			'    MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty2, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    TextBox8.Focus() : Exit Sub
			'ElseIf TextBox7.Text.Trim.Length = 0 Then
			'    MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty3, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    TextBox7.Focus() : Exit Sub
			'ElseIf Dgv_HslProduction.CurrentRow.Cells(CellNilai_Produksi).Value = "0" Then
			'    MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Qty4, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'    Exit Sub
		End If
		get_jam()

		Try
			OpenConn()

			'   get_no_faktur()
			Cmd.Transaction = Cn.BeginTransaction

			Dim Kd_So As String = ""
			Dim Kd_Brg As String = ""
			SQL = "Select b.Status, b.Selesai, b.Kode_Stock_Owner, b.Kode_Barang "
			SQL = SQL & "from Emi_Split_Production_Order a,EMI_Order_Produksi b "
			SQL = SQL & "where a.No_PO = b.No_Faktur "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Transaksi = '" & TextBox4.Text & "'"
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					Kd_So = dr("Kode_Stock_Owner")
					Kd_Brg = dr("Kode_Barang")
					If General_Class.CekNULL(dr("Status")) <> "" Then
						dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show(Base_Language.Lang_Global_NoFaktur & " " & Base_Language.Lang_Global_DataSudahBatal, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End If
			End Using

			'Cek apakah sudah ada data di DGV
			Dim ada_data As Boolean = False
			For a As Integer = 0 To Dgv_HslProduction.Rows.Count - 1
				Get_Isi_Listview(a)
				If Val(HilangkanTanda(LvNilai_Produksi)) <> 0 Then
					ada_data = True
				End If
			Next

			If ada_data = False Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Tidak ada Data yang di input ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			Dim proses As Integer
			SQL = "select no_transaksi, "
			SQL = SQL & "isnull((select top(1) proses from ( "
			SQL = SQL & "Select proses from Emi_Production_Results_Detail x where "
			SQL = SQL & "a.Kode_Perusahaan = x.Kode_Perusahaan And a.No_Transaksi = x.No_Transaksi "
			SQL = SQL & "union all "
			SQL = SQL & "Select proses from Emi_Production_Results_Packaging_Detail x where "
			SQL = SQL & "a.Kode_Perusahaan = x.Kode_Perusahaan And a.No_Transaksi = x.No_Transaksi "
			SQL = SQL & ") as Data order by proses desc ),0) as proses "
			SQL = SQL & "from Emi_Production_Results a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Production_Order = '" & TextBox4.Text & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					TxtFormulator_NoFaktur.Text = Dr("no_transaksi")
					'proses = Dr("proses") + 1
				Else
					Dr.Close()

					get_no_faktur()

					SQL = "INSERT INTO Emi_Production_Results(Kode_Perusahaan,No_Transaksi,No_Production_Order,Tanggal,Jam,UserID"
					SQL = SQL & ") VALUES('" & KodePerusahaan & "',"
					SQL = SQL & "'" & TxtFormulator_NoFaktur.Text & "','" & TextBox4.Text.Trim & "','" & Format(tgl_skg, "yyyy-MM-dd") & "',"
					SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & UserID & "')"
					ExecuteTrans(SQL)

					'proses = 1
				End If
			End Using

			Dim Nilai_Bahan As Double = 0
			Dim Nilai_Packaging As Double = 0
			Dim Nilai_loss_production As Double = 0
			Dim arr_biaya_Produksi, arrID_Work_Center, arrJenis_Biaya As New ArrayList
			Dim Hpp_Work_Center_total As Double = 0

			Arr_Detail_Biaya.Clear()
			'Insert Bahan Sesuai yg di input

#Region "Insert Bahan"

			For a As Integer = 0 To Dgv_HslProduction.Rows.Count - 1
				Get_Isi_Listview(a)

				If Val(HilangkanTanda(LvNilai_Produksi)) > 0 Then

					'======                              =========='
					'======   Awal convert satuan barang =========='
					'=========                           =========='

					Dim convertKeSatuanAsli_bhn As String = ""
					Dim jumlahConvertBhn As Double = 0

					SQL = "select satuan From barang where Kode_barang = '" & LvKode_Bahan & "' "
					SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & LvKode_So & "' "
					Using Dr3 = OpenTrans(SQL)
						If Dr3.Read Then

							convertKeSatuanAsli_bhn = Dr3("satuan")
							'SQL = "select dbo.a tuan('" & KodePerusahaan & "','MASA','" & LvKode_Bahan & "',"
							'SQL = SQL & "'" & LvSatuan & "','" & Dr3("satuan") & "',"
							'SQL = SQL & HilangkanTanda(LvNilai_Produksi) & ") as Hasil "
							'Dr3.Close()

							'Using dr4 = OpenTrans(SQL)
							'    If dr4.Read Then
							'        If General_Class.CekNULL(dr4("Hasil")) <> "" Then
							'            If dr4("Hasil") = 0 Then
							'                dr4.Close()
							'                CloseTrans()
							'                CloseConn()
							'                MessageBox.Show("Satuan " & LvSatuan & " Ke " & convertKeSatuanAsli_bhn & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'                Exit Sub
							'            Else
							'                jumlahConvertBhn = Val(HilangkanTanda(Format(dr4("hasil"), "N4")))

							'            End If
							'        Else
							'            dr4.Close()
							'            CloseTrans()
							'            CloseConn()
							'            MessageBox.Show("Satuan " & LvSatuan & " Ke " & convertKeSatuanAsli_bhn & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'            Exit Sub
							'        End If
							'    End If
							'End Using

							jumlahConvertBhn = Val(HilangkanTanda(Format(Val(HilangkanTanda(LvNilai_Produksi)), "N4")))
						Else
							Dr3.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					End Using

					'=========================
					'=    CEK URUT PROSES    =
					'=========================
					Dim Urut_Proses As Integer = 0
					Dim Jumlah_Dosing As Double = 0
					Dim Selesai_Dosing As String = ""
					SQL = "select top 1 a.No_Transaksi, a.No_Production_Order, b.Proses, isnull(b.Selesai,'T') as Selesai, b.urut, b.Nilai_Produksi "
					SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail b "
					SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
					SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
					SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
					SQL = SQL & "and a.No_Transaksi = '" & TxtFormulator_NoFaktur.Text & "' "
					SQL = SQL & "and b.Kode_Stock_Owner = '" & LvKode_So & "' "
					SQL = SQL & "and b.Kode_Barang = '" & LvKode_Bahan & "' "
					SQL = SQL & "order by b.proses desc "
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then

							If Dr("Selesai") = "Y" Then
								proses = Val(HilangkanTanda(Dr("Proses"))) + 1
							Else
								proses = Val(HilangkanTanda(Dr("Proses")))
								Urut_Proses = Dr("urut")
								Jumlah_Dosing = Val(HilangkanTanda(Format(Dr("Nilai_Produksi"), "N4")))
							End If

							Selesai_Dosing = Dr("Selesai")
						Else
							Dr.Close()

							proses = 1

							Selesai_Dosing = "Y"

						End If
					End Using

					SQL = "Select Kode_Perusahaan from "
					SQL = SQL & "Emi_Production_Results_HPP a where "
					SQL = SQL & "a.Kode_Perusahaan ='" & KodePerusahaan & "' and "
					SQL = SQL & "No_Transaksi='" & TxtFormulator_NoFaktur.Text & "' and "
					SQL = SQL & "proses='" & proses & "' "
					Using dr = OpenTrans(SQL)
						If Not dr.Read Then
							dr.Close()

							SQL = "insert into Emi_Production_Results_HPP "
							SQL = SQL & "(Kode_Perusahaan, No_Transaksi, Proses) "
							SQL = SQL & "Values('" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & proses & "')"
							ExecuteTrans(SQL)
						End If
					End Using

					'======                              =========='
					'======   Akhir convert satuan barang =========='
					'=========                           =========='

					Dim Toleransi_timbang_Min As Double = 0
					Dim Toleransi_timbang_Max As Double = 0
					SQL = "Select top(1) Toleransi_Timbang_Min, Toleransi_Timbang_Max from "
					SQL = SQL & "barang a where "
					SQL = SQL & "a.Kode_Perusahaan ='" & KodePerusahaan & "' and "
					SQL = SQL & "Kode_barang='" & LvKode_Bahan & "' "
					Using dr = OpenTrans(SQL)
						If dr.Read Then
							Toleransi_timbang_Min = dr("Toleransi_Timbang_Min")
							Toleransi_timbang_Max = dr("Toleransi_Timbang_Max")
						Else
							dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("Kode barang tidak ditemukan . . ! !")
							Exit Sub
						End If
					End Using

					Dim Nilai_Formula As Double = 0
					SQL = "Select c.Kode_Barang, "
					SQL = SQL & "isnull(( "

					SQL = SQL & "round( "

					SQL = SQL & "(c.Jumlah / (select z.Hasil from Emi_Transaksi_Formulator z "
					SQL = SQL & "where z.Kode_Perusahaan = c.Kode_Perusahaan And z.No_Faktur = c.No_Faktur) "
					SQL = SQL & ") "

					SQL = SQL & "* "

					SQL = SQL & "(ISNULL((a.Qty_Batch), 0)),4)), 0) as Nilai_Formula "
					SQL = SQL & "From Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c "
					SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And b.Kode_Perusahaan = c.Kode_Perusahaan "
					SQL = SQL & "And a.No_PO = b.No_Faktur "
					SQL = SQL & "And b.Kode_Formula = c.No_Faktur "
					SQL = SQL & "And a.No_Transaksi = '" & TextBox4.Text & "' "
					SQL = SQL & "And c.Kode_Barang = '" & LvKode_Bahan & "' "
					Using dr = OpenTrans(SQL)
						If dr.Read Then
							Nilai_Formula = Val(HilangkanTanda(Format(dr("Nilai_Formula"), "N4")))
						Else
							dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("Nilai Formula Tidak di temukan . . ! !")
							Exit Sub
						End If
					End Using

					If (Jumlah_Dosing + Val(HilangkanTanda(LvNilai_Produksi))) > Nilai_Formula + ((Toleransi_timbang_Max / 100) * Nilai_Formula) Then
						CloseTrans()
						CloseConn()
						MessageBox.Show("Nilai yang di input melebihi Toleransi . . ! !")
						Exit Sub
					End If

					Dim Proses_selesai As String = "NULL"
					If (Jumlah_Dosing + Val(HilangkanTanda(LvNilai_Produksi))) > Nilai_Formula - ((Toleransi_timbang_Max / 100) * Nilai_Formula) Then
						Proses_selesai = "'Y'"
					End If

					If Selesai_Dosing = "Y" Then
						SQL = "INSERT INTO Emi_Production_Results_Detail(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,Nilai_Formula,Nilai_Produksi,Satuan,proses,"
						SQL = SQL & "nilai_barang,satuan_barang,userid,tanggal,jam, Selesai ) "
						SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "','" & LvKode_So & "','" & LvKode_Bahan & "',"
						SQL = SQL & "'" & Nilai_Formula & "','" & HilangkanTanda(LvNilai_Produksi) & "','" & LvSatuan & "' , '" & proses & "', "
						SQL = SQL & "'" & jumlahConvertBhn & "', '" & convertKeSatuanAsli_bhn & "', '" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
						SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', " & Proses_selesai & " "
						SQL = SQL & ")"
						ExecuteTrans(SQL)
					Else
						SQL = "update Emi_Production_Results_Detail set Nilai_Produksi+=" & HilangkanTanda(LvNilai_Produksi) & ", nilai_barang+=" & HilangkanTanda(LvNilai_Produksi) & ", Selesai=" & Proses_selesai & " "
						SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Urut= '" & Urut_Proses & "' "
						ExecuteTrans(SQL)
					End If

					Dim x_ident_currentBahan As Integer = 0
					SQL = "select IDENT_CURRENT('Emi_Production_Results_Detail') as urutan"
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							x_ident_currentBahan = Dr("urutan")
						End If
					End Using

#Region "Potong Stock Bahan"

					SQL = "select round(good_stock,4) as good_stock, flag_ppn from barang where "
					SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
					SQL = SQL & "kode_stock_owner = '" & LvKode_So & "' and "
					SQL = SQL & "kode_barang = '" & LvKode_Bahan & "'"
					Using Ds = BindingTrans(SQL)
						With Ds.Tables("MyTable")
							If .Rows.Count <> 0 Then
								'If LvPotStokBhn = "Y" Then
								If Val(HilangkanTanda(Format(.Rows(0).Item("good_stock"), "N4"))) - Val(jumlahConvertBhn) < BolehNegatif Then
									CloseTrans()
									CloseConn()
									MessageBox.Show("Proses membuat stock menjadi negatif untuk kode barang " & LvKode_Bahan & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								Else
									SQL = "Update barang set good_stock = good_stock - " & jumlahConvertBhn & " where "
									SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
									SQL = SQL & "kode_stock_owner = '" & LvKode_So & "' and "
									SQL = SQL & "kode_barang = '" & LvKode_Bahan & "'"
									ExecuteTrans(SQL)
								End If
								'End If
							Else
								CloseTrans()
								CloseConn()
								MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
								Exit Sub
							End If
						End With
					End Using

					Dim lewatin As String = "T"
					SQL = "select isnull(round(sum(jumlah),4), 0) as stock from barang_sn where "
					SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
					SQL = SQL & "kode_stock_owner = '" & LvKode_So & "' and "
					SQL = SQL & "kode_barang = '" & LvKode_Bahan & "' and jumlah <> 0 "
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							If Val(HilangkanTanda(Format(Dr("stock"), "N4"))) < Val(jumlahConvertBhn) Then
								lewatin = "Y"
							Else
								lewatin = "T"
							End If
						Else
							Dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & LvKode_Bahan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
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
						SQL = SQL & "kode_stock_owner = '" & LvKode_So & "' and "
						SQL = SQL & "kode_barang = '" & LvKode_Bahan & "' and jumlah <> 0 "
						SQL = SQL & "and No_Reservasi is null "
						SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
						Using Ds = BindingTrans(SQL)
							With Ds.Tables("MyTable")
								If .Rows.Count <> 0 Then
									sisa = Val(jumlahConvertBhn)
									For h As Integer = 0 To .Rows.Count - 1
										If sisa = 0 Then
											Exit For
										ElseIf sisa < 0 Then
											CloseTrans()
											CloseConn()
											MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If

										Dim hpp As Double = .Rows(h).Item("HPP")
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
										SQL = SQL & "and a.id_group_jenis = b.id_group_jenis and b.kode_barang='" & .Rows(h).Item("kode_barang") & "' "
										SQL = SQL & "and a.id_group_jenis = c.id_group_jenis and b.kode_stock_owner='" & .Rows(h).Item("kode_stock_owner") & "' "
										Using Dr = OpenTrans(SQL)
											If Dr.Read Then
												Kd_Akun_Biaya = Dr("Akun_Persediaan")
												Ket_Group_Jenis = Dr("Kode_Group_Jenis")
											Else
												Dr.Close()
												CloseTrans()
												CloseConn()
												MessageBox.Show("Barang detail jenis tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
												Exit Sub
											End If
										End Using

										If sisa < .Rows(h).Item("jumlah") Or sisa = .Rows(h).Item("jumlah") Then
											SQL = "Update barang_sn set jumlah = jumlah - " & sisa & " where "
											SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
											SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
											SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
											SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
											ExecuteTrans(SQL)

											SQL = "INSERT INTO Emi_Production_Results_det(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,"
											SQL = SQL & "Nilai,Serial_Number,no_urut_detail) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
											SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "','" & .Rows(h).Item("kode_barang") & "',"
											SQL = SQL & "" & sisa & ",'" & .Rows(h).Item("serial_number") & "', '" & x_ident_currentBahan & "')"
											ExecuteTrans(SQL)

											JumlahInsert = sisa

											Nilai_Bahan = Nilai_Bahan + (Math.Round(hpp * sisa, 0))
											sisa = 0
										ElseIf sisa > .Rows(h).Item("jumlah") Then
											SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
											SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
											SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
											SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
											SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
											ExecuteTrans(SQL)

											SQL = "INSERT INTO Emi_Production_Results_det(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,"
											SQL = SQL & "Nilai,Serial_Number,no_urut_detail) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
											SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "','" & .Rows(h).Item("kode_barang") & "',"
											SQL = SQL & "" & .Rows(h).Item("jumlah") & ",'" & .Rows(h).Item("serial_number") & "', '" & x_ident_currentBahan & "')"
											ExecuteTrans(SQL)

											JumlahInsert = .Rows(h).Item("jumlah")

											Nilai_Bahan = Nilai_Bahan + (Math.Round(hpp * .Rows(h).Item("jumlah"), 0))
											sisa = sisa - .Rows(h).Item("jumlah")
										Else
											CloseTrans()
											CloseConn()
											MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & LvKode_Bahan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If

										'TODO : CEK GROUP BY KODE BARANG, TAMBAH ARRAY DETAIL

										Dim kdBarang As String = .Rows(h).Item("kode_barang")
										Dim kdSO As String = .Rows(h).Item("kode_stock_owner")

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
												kd_so:=kdSO,
												kd_barang:=kdBarang
											))
										End If

										If Math.Round(sisa, 4) <> 0 And h = .Rows.Count - 1 Then
											CloseTrans()
											CloseConn()
											MessageBox.Show("Jumlah stock tidak mencukupi untuk kode barang " & LvKode_Bahan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If

									Next ' for barang sn
								End If 'count <> 0
							End With
						End Using
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & LvKode_Bahan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If

#End Region

				End If

			Next

#End Region

			' Cek Apakah  ada Proses yg sudah selesai
			SQL = "Select Proses from "
			SQL = SQL & "Emi_Production_Results_HPP a where "
			SQL = SQL & "a.Kode_Perusahaan ='" & KodePerusahaan & "' and "
			SQL = SQL & "a.No_Transaksi='" & TxtFormulator_NoFaktur.Text & "' and "
			SQL = SQL & "a.tanggal is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For index = 0 To .Rows.Count - 1

							Dim proses_temp As Integer = .Rows(index).Item("Proses")
							Dim selesai As Boolean = True

							'cek apakah bahan per formula sudah selesai
							SQL = "Select b.Kode_Barang, "
							SQL = SQL & "isnull((select 'Y' from Emi_Production_Results x, Emi_Production_Results_Detail y "
							SQL = SQL & "where x.kode_Perusahaan = y.kode_perusahaan And x.No_Transaksi = y.No_Transaksi And x.status Is null "
							SQL = SQL & "And x.Kode_Perusahaan = a.Kode_Perusahaan And x.No_Production_Order = a.No_Transaksi And "
							SQL = SQL & "y.Kode_Barang = b.Kode_Barang And y.Proses = '" & proses_temp & "' and y.status is null and y.selesai='Y'),'T') as Terpenuhi "
							SQL = SQL & "From Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Bahan b Where "
							SQL = SQL & "a.kode_Perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Faktur And a.status Is null "
							SQL = SQL & " And a.no_transaksi ='" & TextBox4.Text & "' and a.kode_Perusahaan ='" & KodePerusahaan & "' "
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
								SQL = SQL & "Emi_Production_Results a, Emi_Production_Results_Detail b where "
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
								SQL = SQL & "From Emi_Split_Production_Order a, barang b Where "
								SQL = SQL & "a.kode_Perusahaan = b.Kode_Perusahaan And "
								SQL = SQL & "a.Kode_Barang = b.Kode_barang And "
								SQL = SQL & "a.kode_stock_owner=b.kode_stock_owner "
								SQL = SQL & "and a.no_transaksi ='" & TextBox4.Text & "' and a.kode_Perusahaan ='" & KodePerusahaan & "' "
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
								SQL = SQL & "From EMI_Order_Produksi a Where "
								SQL = SQL & "a.no_faktur ='" & No_Production_Order & "' and a.kode_Perusahaan ='" & KodePerusahaan & "' "
								Using dr = OpenTrans(SQL)
									If dr.Read Then
										ID_Routing = dr("Id_Routing")
									End If
								End Using

								'INI TETEP, KARENA UBAH KG ke PCS
								SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Kd_barang & "',"
								SQL = SQL & "'" & satuan_dosing & "','" & satuan_barang & "',"
								SQL = SQL & "" & Jumlah_Dosing & ") as Hasil "

								Using dr4 = OpenTrans(SQL)
									If dr4.Read Then
										If General_Class.CekNULL(dr4("Hasil")) <> "" Then

											Jumlah_Dosing_Pcs = Math.Floor(dr4("hasil"))
										Else
											dr4.Close()
											CloseTrans()
											CloseConn()

											MessageBox.Show("Satuan " & satuan_dosing & " Ke " & satuan_barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											Exit Sub
										End If
									End If
								End Using

								Dim satuan_bahan As String = ""

								Dim Hpp_Packaging_Total As Double = 0

								Dim Hpp_Bahan_baku_Total As Double = 0

								Dim Persen_loss_production As Double = 0

								SQL = "Select isnull(sum(nilai * dbo.get_hpp(c.Serial_Number)),0) As Total, b.satuan_barang  "
								SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail b, Emi_Production_Results_det c where "
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

								'Nilai_loss_production = Math.Round(Hpp_Bahan_baku_Total * Persen_loss_production / 100, 0)

								'SQL = "Select isnull(sum(nilai * dbo.get_hpp(c.Serial_Number)),0) As Total from "
								'SQL = SQL & "Emi_Production_Results a, Emi_Production_Results_packaging_detail b, Emi_Production_Results_packaging_det c where "
								'SQL = SQL & "a.kode_perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi And a.status Is null And "
								'SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.No_Transaksi = c.No_Transaksi And b.Urut = c.No_Urut_detail "
								'SQL = SQL & "And a.Kode_Perusahaan='" & KodePerusahaan & "' and a.No_Transaksi='" & TxtFormulator_NoFaktur.Text & "' and b.proses='" & proses_temp & "' "
								'Using dr = OpenTrans(SQL)
								'    If dr.Read Then
								'        Hpp_Packaging_Total = Val(HilangkanTanda(Format(dr("Total"), "N4")))
								'    End If
								'End Using

								' Hpp_Packaging_Pcs = Math.Round(Hpp_Packaging_Total / Jumlah_Dosing_Pcs, 2)

								''Dim bulan As String = Format(tgl_skg, "MM")
								''Dim tahun As String = Format(tgl_skg, "yyyy")

								''SQL = ";with cte as ( "
								''SQL = SQL & "Select a.kode_perusahaan, a.Id_Jenis_Biaya_Produksi, a.Kode_Jenis_Biaya_Produksi, "
								''SQL = SQL & "isnull((select top(1) no_faktur from Emi_Transaksi_Work_Center x where x.status Is null "
								''SQL = SQL & "And x.Kode_Perusahaan=a.Kode_Perusahaan And x.jenis_biaya=a.Kode_Jenis_Biaya_Produksi order by id desc),NULL) as Faktur_WC "
								''SQL = SQL & "From Emi_Jenis_Biaya_Produksi a "
								''SQL = SQL & ")select a.kode_jenis_biaya_produksi, c.id_work_center, max(c.Nilai_Per_pcs) as Nilai_Per_pcs "
								''SQL = SQL & "From cte a, Emi_Transaksi_Work_Center b, Emi_Transaksi_Work_Center_detail c Where "
								''SQL = SQL & "a.kode_perusahaan = b.Kode_Perusahaan And a.faktur_WC = b.No_Faktur And "
								''SQL = SQL & "b.kode_perusahaan = c.Kode_Perusahaan And b.No_Faktur = c.No_Faktur And c.Id_Routing = '" & ID_Routing & "' "
								''SQL = SQL & "group by a.kode_jenis_biaya_produksi, c.id_work_center "
								''Using Dss = BindingTrans(SQL)
								''    If Dss.Tables("MyTable").Rows.Count <> 0 Then

								''        For indxx = 0 To Dss.Tables("MyTable").Rows.Count - 1

								''            Dim id_WC As String = Dss.Tables("MyTable").Rows(indxx).Item("id_work_center")
								''            Dim Jenis_biaya As String = Dss.Tables("MyTable").Rows(indxx).Item("kode_jenis_biaya_produksi")
								''            Dim Nilai_WC As Double = Dss.Tables("MyTable").Rows(indxx).Item("Nilai_Per_pcs")

								''            If Nilai_WC <> 0 Then
								''                arrID_Work_Center.Add(id_WC)
								''                arrJenis_Biaya.Add(Jenis_biaya)
								''                arr_biaya_Produksi.Add(Math.Round(Nilai_WC * Jumlah_Dosing))
								''                Hpp_Work_Center_total += Math.Round(Nilai_WC * Jumlah_Dosing)

								''                SQL = "insert into Emi_Production_Results_HPP_Detail_Work_Center ("
								''                SQL = SQL & "kode_Perusahaan, No_Transaksi, proses, ID_Work_Center, Kode_Jenis_Biaya, Nilai) values( "
								''                SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFormulator_NoFaktur.Text & "', '" & proses_temp & "', "
								''                SQL = SQL & "'" & id_WC & "', '" & Jenis_biaya & "', '" & Nilai_WC & "') "
								''                ExecuteTrans(SQL)
								''            End If

								''        Next

								''    Else
								''        CloseTrans()
								''        CloseConn()
								''        MessageBox.Show("Biaya Belum di tambahkan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								''        Exit Sub
								''    End If

								''End Using

								SQL = "Update Emi_Production_Results_HPP set "
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
				End With
			End Using

			'awal stenly jurnal

#Region "JURNAL"

			Dim inisial_faktur_dari As String = ""

			SQL = "Select b.Inisial_Faktur,a.Kode_Stock_Owner from Emi_Split_Production_Order a,Stock_Owner_Gudang b "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Stock_Owner = b.Kode_Stock_Owner "
			SQL = SQL & "And a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & TextBox4.Text & "' "
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
				CloseTrans()
				CloseConn()
				MessageBox.Show("tidak ada data yang di jurnal...!!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
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
				SQL = SQL & "from Emi_Production_Results_det a, Barang b, EMI_Group_Jenis_Akun c, "
				SQL = SQL & "Emi_Production_Results_Detail e, EMI_Group_Jenis f  "
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
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For h As Integer = 0 To .Rows.Count - 1

								lok_material = .Rows(h).Item("kode_stock_owner")
								akun_kredit_material = .Rows(h).Item("akun_persediaan")
								ket_material = "Persediaan " + .Rows(h).Item("Kode_Group_Jenis")

							Next
						End If
					End With
				End Using

				'TODO : Insert dengan cara loop array detail
				For i As Integer = 0 To Arr_Detail_Biaya.Count - 1
					SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Arr_Detail_Biaya(i).akun, 1),
						Strings.Mid(Arr_Detail_Biaya(i).akun, 2, 1),
						Strings.Mid(Ganti(Arr_Detail_Biaya(i).akun), 3),
						KodePerusahaan, KodeProyek, $"Persediaan {Arr_Detail_Biaya(i).keterangan}", "0", Math.Round(Arr_Detail_Biaya(i).nilai, 0), pagenumber, Arr_Detail_Biaya(i).kd_so, Bahasa_Pilihan, Ket_Cost_Center_HO)
					ExecuteTrans(SQL)

					Temp_Nilai_Bahan += Math.Round(Arr_Detail_Biaya(i).nilai, 0)
				Next

				'SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_kredit_material, 1),
				'  Strings.Mid(akun_kredit_material, 2, 1),
				'  Strings.Mid(Ganti(akun_kredit_material), 3),
				'  KodePerusahaan, KodeProyek, ket_material & TxtFormulator_NoFaktur.Text, "0", Nilai_Bahan, pagenumber, lok_material, Bahasa_Pilihan, Ket_Cost_Center_HO)
				'ExecuteTrans(SQL)
				pagenumber = pagenumber + 1
			End If

			If Nilai_Bahan <> Math.Round(Temp_Nilai_Bahan, 0) Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Terjadi Kesalahan, Nilai Bahan Tidak Sama !!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			If Nilai_Packaging <> 0 Then
				SQL = "select top(1) "
				SQL = SQL & "b.Id_Group_Jenis, b.kode_stock_owner, c.akun_persediaan, Kode_Group_Jenis "
				SQL = SQL & "from Emi_Production_Results_Packaging_Det a, Barang b, EMI_Group_Jenis_Akun c, "
				SQL = SQL & "Emi_Production_Results_Packaging_Detail e, EMI_Group_Jenis f where "
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
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For h As Integer = 0 To .Rows.Count - 1

								lok_packaging = .Rows(h).Item("kode_stock_owner")
								akun_kredit_packaging = .Rows(h).Item("akun_persediaan")
								ket_packaging = "Persediaan " + .Rows(h).Item("Kode_Group_Jenis")

							Next
						End If
					End With
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
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For h As Integer = 0 To .Rows.Count - 1

								Dim akun As String = .Rows(h).Item("Kode_Akun_Budget")
								Dim ket As String = .Rows(h).Item("keterangan")

								SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun, 1),
									Strings.Mid(akun, 2, 1),
									Strings.Mid(Ganti(akun), 3),
									KodePerusahaan, KodeProyek, ket & " " & TxtFormulator_NoFaktur.Text, "0", arr_biaya_Produksi.Item(index), pagenumber, Lokasi, Bahasa_Pilihan, arrID_Work_Center.Item(index))
								ExecuteTrans(SQL)
								pagenumber = pagenumber + 1

							Next
						End If
					End With
				End Using
			Next

			SQL = "select round(sum(debit),2) as debit, round(sum(kredit),2) as kredit from detail_jurnal where "
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

			SQL = "insert into Emi_Production_Results_Jurnal (Kode_Perusahaan,No_Transaksi,Kode_Voucher,Proses, Jenis) values ("
			SQL = SQL & "'" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "','" & Kode_voucher & "',"
			SQL = SQL & "'" & proses & "', 'GI') "
			ExecuteTrans(SQL)

#End Region

			'akhir stenly jurnal

			Cmd.Transaction.Commit()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
		EMI_Display_Pengeluaran_Bahan_Baku.Button1_Click(Btn_Simpan, e)
		EMI_Controlling_Produksi.Kosong()
		Me.Close()
	End Sub

	Private Sub Dgv_HslProduction_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_HslProduction.CellEndEdit
		Get_Isi_Listview(Dgv_HslProduction.CurrentRow.Index)

		If Not Dgv_HslProduction.Rows.Count = 0 Then
			'======================
			'=     SET FORMAT     =
			'======================
			Dim culture As CultureInfo = CultureInfo.CurrentCulture

			If Dgv_HslProduction.CurrentCell.ColumnIndex = CellNilai_Produksi Then

				Dim cellKuantity As String = Dgv_HslProduction.CurrentCell.Value.ToString()

				If Not IsNumeric(cellKuantity) Then
					Dgv_HslProduction.CurrentCell.Value = 0
					Exit Sub
				End If

				If cellKuantity.Contains(",") Then
					MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Dgv_HslProduction.CurrentCell.Value = 0
					Exit Sub
				End If

				'Dim nilai As Decimal = Decimal.Parse(cellKuantity)
				'Dim formattedValue As String = nilai.ToString("N2", culture)

				'Dgv_HslProduction.CurrentCell.Value = formattedValue
			End If
		End If

	End Sub

	Private Sub get_no_faktur()
		Dim FPro_Results As String = "PRS"
		TxtFormulator_NoFaktur.Text = FPro_Results & Format(tgl_skg, "MMyy") & "-" &
							 General_Class.Get_Last_Number2("Emi_Production_Results", "No_Transaksi", 5,
							 "Kode_perusahaan", KodePerusahaan,
							 "And", "substring(No_Transaksi, 1, " & Len(FPro_Results) + 4 & ")", FPro_Results & Format(tgl_skg, "MMyy"))
	End Sub

	Private Sub Transaksi_Produksi_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub Transaksi_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		Try
			OpenConn()

			Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
			Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

			Label1.Text = "Transaksi - Pengeluaran Bahan Baku (Dosing)"
			' Label8.Text = Base_Language.Lang_Display_Production_Order_Qty_Produksi
			Label6.Text = Base_Language.Lang_Global_NoFaktur
			Label7.Text = Base_Language.Lang_Global_Tanggal_Produksi
			Label2.Text = Base_Language.Lang_Global_Jam
			Label10.Text = Base_Language.Lang_Global_NamaBarang
			'  Label9.Text = Base_Language.Lang_Display_Production_Order_Qty_Produksi2
			Btn_Simpan.Text = Base_Language.Lang_Global_Simpan

			'ListView2.Columns.Clear()
			'ListView2.Columns.Add(Base_Language.Lang_Global_No_PO, 140, HorizontalAlignment.Left)
			'ListView2.Columns.Add(Base_Language.Lang_Global_Lokasi, 0, HorizontalAlignment.Left)
			'ListView2.Columns.Add(Base_Language.Lang_Global_KodeCustomer, 140, HorizontalAlignment.Left)
			'ListView2.Columns.Add(Base_Language.Lang_Global_NamaCustomer, 200, HorizontalAlignment.Left)
			'ListView2.Columns.Add(Base_Language.Lang_Global_KodeBarang, 130, HorizontalAlignment.Left)
			'ListView2.Columns.Add(Base_Language.Lang_Global_NamaBarang, 220, HorizontalAlignment.Left)
			'ListView2.Columns.Add(Base_Language.Lang_Global_Jumlah, 100, HorizontalAlignment.Center)
			'ListView2.Columns.Add(Base_Language.Lang_Global_Satuan, 90, HorizontalAlignment.Center)
			'ListView2.View = View.Details

			Dgv_HslProduction.Columns(0).HeaderText = Base_Language.Lang_Global_Lokasi
			Dgv_HslProduction.Columns(1).HeaderText = Base_Language.Lang_Global_Kode_Bahan
			' Dgv_HslProduction.Columns(2).HeaderText = Base_Language.Lang_Global_Nama
			Dgv_HslProduction.Columns(2).HeaderText = Base_Language.Lang_Display_Production_Order_Nilai_Produksi
			'Dgv_HslProduction.Columns(4).HeaderText = Base_Language.Lang_Display_Production_Order_Hasil_Produksi
			Dgv_HslProduction.Columns(4).HeaderText = Base_Language.Lang_Global_Satuan

			Dgv_Hasil_Production_Packaging.Columns(0).HeaderText = Base_Language.Lang_Global_Lokasi
			Dgv_Hasil_Production_Packaging.Columns(1).HeaderText = Base_Language.Lang_Global_Kode_Bahan
			' Dgv_Hasil_Production_Packaging.Columns(2).HeaderText = Base_Language.Lang_Global_Nama
			Dgv_Hasil_Production_Packaging.Columns(2).HeaderText = Base_Language.Lang_Display_Production_Order_Nilai_Produksi
			'Dgv_Hasil_Production_Packaging.Columns(4).HeaderText = Base_Language.Lang_Display_Production_Order_Hasil_Produksi
			Dgv_Hasil_Production_Packaging.Columns(4).HeaderText = Base_Language.Lang_Global_Satuan

			Dgv_HslProduction.Rows.Clear()
			Dgv_Hasil_Production_Packaging.Rows.Clear()
			'SQL = "select c.Kode_Stock_Owner,c.Kode_Barang,d.Nama,c.Jumlah,c.Persentase,e.Satuan from "
			'SQL = SQL & "Emi_Order_Produksi_Detail a,Emi_Transaksi_Formulator b,EMI_Transaksi_Formulator_Detail_Bahan c,Barang d,Barang_Detail_Satuan e "
			'SQL = SQL & "where a.No_Formula = b.No_Faktur and b.Status is null and b.No_Faktur = c.No_Faktur and a.Kode_Perusahaan = b.Kode_Perusahaan "
			'SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
			'SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and d.Kode_Barang = e.Kode_barang and e.Flag_Tampil_Display = 'Y' and "
			'SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & TextBox4.Text & "' and a.Urut = '" & fUrut & "' "

			SQL = "select a.No_Transaksi, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama, b.Jumlah ,b.Satuan, c.flag_potong_stok, isnull(c.standar_price,0) as standar_price from  "
			SQL = SQL & "Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Bahan b, barang c "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
			SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
			SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & TextBox4.Text & "' "
			SQL = SQL & "order by c.nama"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							Dgv_HslProduction.Rows.Add()
							Dgv_HslProduction.Rows.Item(i).Cells(CellKode_So).Value = .Rows(i).Item("Kode_Stock_Owner")
							Dgv_HslProduction.Rows.Item(i).Cells(CellKode_Bahan).Value = .Rows(i).Item("Kode_Barang")
							' Dgv_HslProduction.Rows.Item(i).Cells(CellNama_Bahan).Value = .Rows(i).Item("Nama")
							' Dim nhasil As Double = 0
							' nhasil = .Rows(i).Item("Jumlah") * .Rows(i).Item("Persentase") / 100
							Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Formula).Value = Format(.Rows(i).Item("jumlah"), "N4")
							Dgv_HslProduction.Rows.Item(i).Cells(CellNilai_Produksi).Value = "0"
							Dgv_HslProduction.Rows.Item(i).Cells(CellSatuan).Value = .Rows(i).Item("Satuan")

							If General_Class.CekNULL(.Rows(i).Item("flag_potong_stok")) = "" Then
								Dgv_HslProduction.Rows.Item(i).Cells(CellPotStokBhn).Value = ""
							Else
								Dgv_HslProduction.Rows.Item(i).Cells(CellPotStokBhn).Value = .Rows(i).Item("flag_potong_stok")
							End If

							Dgv_HslProduction.Rows.Item(i).Cells(CellStandarPrice).Value = .Rows(i).Item("standar_price")

						Next
					End If
				End With
			End Using

			SQL = "select a.No_Transaksi, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama, b.Jumlah, b.Satuan, c.flag_potong_stok,isnull(c.standar_price,0) as standar_price from  "
			SQL = SQL & "Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Packaging b, barang c "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur "
			SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.Kode_Stock_Owner = b.Kode_Stock_Owner "
			SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_transaksi = '" & TextBox4.Text & "' "
			SQL = SQL & "order by c.nama"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							Dgv_Hasil_Production_Packaging.Rows.Add()
							Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellKode_So).Value = .Rows(i).Item("Kode_Stock_Owner")
							Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellKode_Bahan).Value = .Rows(i).Item("Kode_Barang")
							'Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellNama_Bahan).Value = .Rows(i).Item("Nama")
							' Dim nhasil As Double = 0
							' nhasil = .Rows(i).Item("Jumlah") * .Rows(i).Item("Persentase") / 100
							Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellNilai_Formula).Value = Format(.Rows(i).Item("jumlah"), "N4")
							Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellNilai_Produksi).Value = "0"
							Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellSatuan).Value = .Rows(i).Item("Satuan")

							If General_Class.CekNULL(.Rows(i).Item("flag_potong_stok")) = "" Then
								Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellPotStokPckg).Value = ""
							Else
								Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellPotStokPckg).Value = .Rows(i).Item("flag_potong_stok")
							End If

							Dgv_Hasil_Production_Packaging.Rows.Item(i).Cells(CellStandarPricePckg).Value = .Rows(i).Item("standar_price")
						Next
					End If
				End With
			End Using

			get_no_faktur()

			Arr_Detail_Biaya.Clear()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	'Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
	'    If TextBox5.Text.Trim.Length = 0 Then
	'        Exit Sub
	'    ElseIf TextBox8.Text.Trim.Length = 0 Then
	'        Exit Sub
	'    End If
	'    Dim a As Double = 0
	'    a = Val(HilangkanTanda(TextBox5.Text)) - Val(HilangkanTanda(TextBox8.Text))
	'    TextBox7.Text = Format(a, "N2")
	'End Sub

	'Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
	'    If e.KeyChar = Chr(13) Then TextBox8.Focus()
	'    If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
	'End Sub

	'Private Sub TextBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox8.KeyPress
	'    If e.KeyChar = Chr(13) Then Dgv_HslProduction.Focus()
	'    If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
	'End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim Data As New List(Of (KdBarang As String, jumlah As Double))

			SQL = $"
				SELECT c.Kode_Barang,
					   ISNULL((ROUND(   (c.Jumlah /
										 (
											 SELECT z.Hasil
											 FROM Emi_Transaksi_Formulator z
											 WHERE z.Kode_Perusahaan = c.Kode_Perusahaan
												   AND z.No_Faktur = c.No_Faktur
										 )
										) * (ISNULL((a.Qty_Batch), 0)),
										4
									)
							  ),
							  0
							 ) AS Nilai_Formula
				FROM Emi_Split_Production_Order a,
					 EMI_Order_Produksi b,
					 EMI_Transaksi_Formulator_Detail_Bahan c
				WHERE a.Kode_Perusahaan = b.Kode_Perusahaan
					  AND b.Kode_Perusahaan = c.Kode_Perusahaan
					  AND a.No_PO = b.No_Faktur
					  AND b.Kode_Formula = c.No_Faktur
					  AND a.No_Transaksi = '{TextBox4.Text.Trim}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.HasRows Then
					While Dr.Read
						Data.Add((Dr("Kode_Barang"), Dr("Nilai_Formula")))
					End While
				End If
			End Using

			For Each row As DataGridViewRow In Dgv_HslProduction.Rows
				If Not row.IsNewRow Then
					Dim kdBarang As String = row.Cells(1).Value?.ToString()
					Dim Jumlah As Double = Data.FirstOrDefault(Function(x) x.KdBarang.Trim = kdBarang.Trim).jumlah

					row.Cells(3).Value = Jumlah

				End If
			Next

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

End Class