Public Class EMI_Request_Material_List

	Dim JudulForm As String = "Display Request Material"

	Dim arrcari, arr_tgl, arr_Lain As New ArrayList
	Dim Jenis = "Display_Transaksi_Formula"
	Private Sub Display_Trx_Formula_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Try
			OpenConn()

			Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
			Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

			CheckBox3.Text = Base_Language.Lang_Global_Hari_ini
			CheckBox1.Text = Base_Language.Lang_Global_Para_Tbl
			CheckBox2.Text = Base_Language.Lang_Global_Para_lain

			ListView1.Columns.Add(Base_Language.Lang_Global_NoFaktur, 120, HorizontalAlignment.Left)
			ListView1.Columns.Add(Base_Language.Lang_Global_No_Produksi, 120, HorizontalAlignment.Left)
			ListView1.Columns.Add("Kode Stock Owner", 0, HorizontalAlignment.Left)
			ListView1.Columns.Add(Base_Language.Lang_Global_KodeBarang, 140, HorizontalAlignment.Left)
			ListView1.Columns.Add(Base_Language.Lang_Global_Nama, 250, HorizontalAlignment.Left)
			ListView1.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
			ListView1.Columns.Add(Base_Language.Lang_Global_Tanggal, 110, HorizontalAlignment.Center)
			ListView1.Columns.Add(Base_Language.Lang_Global_Jam, 100, HorizontalAlignment.Center)
			ListView1.Columns.Add("User", 100, HorizontalAlignment.Center)

			ListView1.View = View.Details
			ListView1.Items.Clear()

			arr_tgl.Clear() : ComboBox3.Items.Clear()
			ComboBox3.Items.Add(Base_Language.Lang_Display_Transaksi_Formula_Tgl) : arr_tgl.Add("a.tanggal")

			arr_Lain.Clear() : ComboBox2.Items.Clear()
			ComboBox2.Items.Add(Base_Language.Lang_Display_Transaksi_Formula_No_Faktur) : arr_Lain.Add("a.no_faktur")
			ComboBox2.Items.Add("No Production Order") : arr_Lain.Add("a.no_faktur_order")
			ComboBox2.Items.Add("Kode Barang") : arr_Lain.Add("a.kode_barang")
			ComboBox2.Items.Add(Base_Language.Lang_Display_Transaksi_Formula_Nama_barang) : arr_Lain.Add("b.Nama")


			ComboBox6.Items.Clear()
			ComboBox6.Items.Add("-- Seluruh --")
			xSplit = CekKotaRole().Split(",")

			SQL = "Select kode_stock_owner From "
			SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and kode_kota in("
			For i As Integer = 0 To xSplit.Count - 1
				SQL = SQL & "'" & xSplit(i).Trim & "', "
			Next
			SQL = Strings.Left(SQL, Len(SQL) - 2)

			SQL = SQL & ") "
			SQL = SQL & "order by kode_stock_owner"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					ComboBox6.Items.Add(dr("kode_stock_owner"))
				Loop
			End Using
			ComboBox6.Text = Lokasi

			CheckBox3.Checked = False
			CheckBox1.Checked = False
			CheckBox2.Checked = False
			ComboBox3.Enabled = False
			ComboBox3.SelectedIndex = -1
			DateTimePicker1.Enabled = False
			DateTimePicker2.Enabled = False
			ComboBox2.Enabled = False
			TextBox4.Enabled = False
			TextBox4.Text = ""
			DateTimePicker1.Value = Now
			DateTimePicker2.Value = Now

			TabPage1.Text = Base_Language.Lang_Display_Transaksi_Formula_Detail_Step
			TabPage2.Text = Base_Language.Lang_Display_Transaksi_Formula_Komposisi

			DataGridView1.Columns(0).HeaderText = Base_Language.Lang_Display_Transaksi_Formula_No_Step
			DataGridView1.Columns(1).HeaderText = Base_Language.Lang_Display_Transaksi_Formula_Tipe
			DataGridView1.Columns(2).HeaderText = Base_Language.Lang_Display_Transaksi_Formula_Kode
			DataGridView1.Columns(3).HeaderText = Base_Language.Lang_Display_Transaksi_Formula_Deskripsi
			DataGridView1.Columns(4).HeaderText = Base_Language.Lang_Display_Transaksi_Formula_Jumlah
			DataGridView1.Columns(5).HeaderText = Base_Language.Lang_Display_Transaksi_Formula_Satuan
			DataGridView1.Columns(6).HeaderText = Base_Language.Lang_Display_Transaksi_Formula_Presentase
			DataGridView1.Rows.Clear()

			DataGridView1.Columns(0).HeaderText = Base_Language.Lang_Display_Transaksi_Formula_Lokasi
			DataGridView1.Columns(1).HeaderText = Base_Language.Lang_Display_Transaksi_Formula_Kode
			DataGridView1.Columns(2).HeaderText = Base_Language.Lang_Display_Transaksi_Formula_Deskripsi
			DataGridView1.Columns(3).HeaderText = Base_Language.Lang_Display_Transaksi_Formula_Jumlah
			DataGridView1.Columns(4).HeaderText = Base_Language.Lang_Display_Transaksi_Formula_Satuan
			DataGridView1.Columns(5).HeaderText = Base_Language.Lang_Display_Transaksi_Formula_Presentase
			DataGridView2.Rows.Clear()

			DataGridView2.Columns(6).DisplayIndex = 4
			DataGridView2.Columns(7).DisplayIndex = 5

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
		If CheckBox3.Checked = True Then
			CheckBox1.Checked = False
			ComboBox3.SelectedIndex = -1
			ComboBox3.Enabled = False
			DateTimePicker1.Enabled = False
			DateTimePicker2.Enabled = False
			DateTimePicker1.Value = Now
			DateTimePicker2.Value = Now

			Button1_Click(CheckBox3, e)
		End If
	End Sub


	Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
		If CheckBox1.Checked = True Then
			CheckBox3.Checked = False
			ComboBox3.Enabled = True
			DateTimePicker1.Enabled = True
			DateTimePicker2.Enabled = True
		Else
			ComboBox3.SelectedIndex = -1
			ComboBox3.Enabled = False
			DateTimePicker1.Enabled = False
			DateTimePicker2.Enabled = False
			DateTimePicker1.Value = Now
			DateTimePicker2.Value = Now
		End If
	End Sub


	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False Then
			MessageBox.Show(Base_Language.Lang_Global_Error_Paramater, Base_Language.Lang_Global_Perhatian)
			CheckBox1.Focus() : Exit Sub
		End If
		If CheckBox1.Checked Then
			If ComboBox3.SelectedIndex = -1 Then
				MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Tgl, Base_Language.Lang_Global_Perhatian)
				ComboBox3.Focus() : Exit Sub
			ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
				MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Tgl2, Base_Language.Lang_Global_Perhatian)
				DateTimePicker1.Value = Now : DateTimePicker2.Value = Now
				Exit Sub
			End If
			If CheckBox2.Checked Then
				If ComboBox2.SelectedIndex = -1 Then
					MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain, Base_Language.Lang_Global_Perhatian)
					ComboBox2.Focus() : Exit Sub
				ElseIf TextBox4.Text.Trim.Length = 0 Then
					MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain2, Base_Language.Lang_Global_Perhatian)
					TextBox4.Focus() : Exit Sub
				End If
			End If
		End If

		Try
			OpenConn()
			ListView1.Items.Clear()
			DataGridView1.Rows.Clear()
			DataGridView2.Rows.Clear()
			SQL = "select a.No_Faktur, a.No_Faktur_Order, a.Kode_Stock_Owner, a.Kode_Barang, a.Keterangan, b.Nama, a.Id_Group_Jenis, c.Kode_Group_Jenis, a.UserId, a.Tanggal, a.Jam, a.status "
			SQL = SQL & "from Emi_Material_Requisition a, Barang b, EMI_Group_Jenis c "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
			SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Group_Jenis = c.Id_Group_Jenis "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			'SQL = SQL & "and a.Status is null "

			If ComboBox6.SelectedIndex = 0 Then
				SQL = SQL & " and a.lokasi in("
				Dim list_kota As String = ""
				For x As Integer = 1 To ComboBox6.Items.Count - 1
					list_kota = list_kota & "'" & ComboBox6.Items(x).ToString & "', "
				Next

				list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

				SQL = SQL & list_kota & ") "
			Else
				SQL = SQL & " and a.lokasi = '" & ComboBox6.Text & "' "
			End If

			If CheckBox3.Checked Then
				'Pasang And
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

				SQL = SQL & "a.tanggal between '"
				SQL = SQL & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' and '" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' "
			End If

			If CheckBox1.Checked Then
				'Pasang And
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

				SQL = SQL & arr_tgl.Item(ComboBox3.SelectedIndex) & " between '"
				SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
			End If

			If CheckBox2.Checked Then
				'Pasang And
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

				SQL = SQL & arr_Lain.Item(ComboBox2.SelectedIndex) & " like '%" & Trim(TextBox4.Text) & "%' "
			End If
			SQL = SQL & "order by a.tanggal desc"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					For i As Integer = 0 To .Rows.Count - 1
						Dim Lvw As ListViewItem
						Lvw = ListView1.Items.Add(.Rows(i).Item("no_faktur"))
						Lvw.SubItems.Add(.Rows(i).Item("no_faktur_order"))

						Lvw.SubItems.Add(.Rows(i).Item("kode_stock_owner"))
						Lvw.SubItems.Add(.Rows(i).Item("kode_barang"))
						If General_Class.CekNULL(.Rows(i).Item("Nama")) = "" Then
							Lvw.SubItems.Add("-")
						Else
							Lvw.SubItems.Add(.Rows(i).Item("Nama"))
						End If
						Lvw.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("Keterangan")) = "", "-", .Rows(i).Item("Keterangan")))
						Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal"), "dd MMM yyyy"))
						Lvw.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("Jam")) = "", "-", .Rows(i).Item("Jam")))
						Lvw.SubItems.Add(.Rows(i).Item("userid"))
						ListView1.Items(i).ForeColor = Color.Blue


						If General_Class.CekNULL(.Rows(i).Item("status")) = "" Then
							ListView1.Items(i).BackColor = Color.White
							ListView1.Items(i).ForeColor = Color.Black
						Else
							ListView1.Items(i).BackColor = Color.DarkRed
							ListView1.Items(i).ForeColor = Color.White
						End If
					Next
				End With
			End Using
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
		If CheckBox2.Checked = True Then
			ComboBox2.Enabled = True
			TextBox4.Enabled = True
		Else
			ComboBox2.Enabled = False
			TextBox4.Enabled = False
			ComboBox2.SelectedIndex = -1
			TextBox4.Text = ""
		End If
	End Sub



	Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
		If ListView1.Items.Count = 0 Then Exit Sub
		Try
			OpenConn()


			DataGridView2.Rows.Clear()

			'SQL = "select a.No_Faktur,a.Kode_Stock_Owner ,a.Kode_Barang,b.Nama , a.Kebutuhan,a.Jumlah,a.Satuan,a.jenis_material, a.Kode_Stock_Owner_Tujuan "
			'SQL = SQL & "From Emi_Material_Requisition_Det a, Barang b "
			'SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
			'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & ListView1.FocusedItem.Text & "' "
			'SQL = SQL & "order by b.Nama "

			SQL = "select a.No_Faktur,a.Kode_Stock_Owner ,a.Kode_Barang,b.Nama , a.Kebutuhan, a.Jumlah, a.Satuan,a.jenis_material, a.Kode_Stock_Owner_Tujuan, "
			SQL = SQL & "ISNULL((select sum(w.jumlah) from tf_stock_parent x, Tf_Stock y, Tf_Stock_det z, Tf_Stock_det2 w,  "
			SQL = SQL & "emi_material_requisition_det_convert m, emi_material_requisition n where  "
			SQL = SQL & "x.kode_Perusahaan = y.kode_perusahaan And x.no_faktur = y.no_faktur And x.status Is null And  "
			SQL = SQL & "y.kode_Perusahaan = z.kode_perusahaan And y.no_faktur = z.no_faktur And y.urut_oto = z.urut_tf And (z.selesai Is null Or z.selesai ='Y') and  "
			SQL = SQL & "z.kode_Perusahaan = w.kode_perusahaan And z.no_faktur = w.no_faktur And z.urut_oto = w.Urut_Det And  "
			SQL = SQL & "m.Kode_Perusahaan = y.Kode_Perusahaan And m.Urut_Oto = y.urut_material_requisition_convert and  "
			SQL = SQL & "y.Flag_Jenis_Request = 'PRODUKSI' and  "
			SQL = SQL & "m.kode_Perusahaan = n.kode_perusahaan And m.no_faktur = n.no_faktur and n.status is null and  "
			SQL = SQL & "a.Kode_Perusahaan = m.Kode_Perusahaan and a.No_Faktur = m.No_Faktur and a.Kode_Stock_Owner = m.Kode_Stock_Owner and a.Kode_Barang = m.Kode_Barang and a.Urut_Oto = m.No_Urut_Det "
			SQL = SQL & "), '0') as Total_TF "
			SQL = SQL & "From Emi_Material_Requisition_Det a, Barang b  "
			SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang  "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Faktur = '" & ListView1.FocusedItem.Text & "' "
			SQL = SQL & "order by b.Nama "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							DataGridView2.Rows.Add(1)
							DataGridView2.Rows.Item(i).Cells(0).Value = If(General_Class.CekNULL(.Rows(i).Item("Kode_Stock_Owner_Tujuan")) = "", "-", .Rows(i).Item("Kode_Stock_Owner_Tujuan"))
							DataGridView2.Rows.Item(i).Cells(1).Value = .Rows(i).Item("kode_barang")
							DataGridView2.Rows.Item(i).Cells(2).Value = .Rows(i).Item("Nama")
							DataGridView2.Rows.Item(i).Cells(3).Value = Format(.Rows(i).Item("jumlah"), "N2")
							DataGridView2.Rows.Item(i).Cells(4).Value = .Rows(i).Item("satuan")
							DataGridView2.Rows.Item(i).Cells(5).Value = .Rows(i).Item("jenis_material")

							Dim TotalTf As Double = If(General_Class.CekNULL(.Rows(i).Item("Total_TF")) = "", 0, .Rows(i).Item("Total_TF"))
							Dim JumlahRequest As Double = If(General_Class.CekNULL(.Rows(i).Item("jumlah")) = "", 0, .Rows(i).Item("jumlah"))

							DataGridView2.Rows.Item(i).Cells(6).Value = Format(TotalTf, "N2")
							Dim sisa As Double = Val(HilangkanTanda(JumlahRequest)) - Val(HilangkanTanda(TotalTf))
							DataGridView2.Rows.Item(i).Cells(7).Value = Format(sisa, "N2")
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


	Private Sub BatalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalToolStripMenuItem.Click
		If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
			MessageBox.Show(Base_Language.Lang_Display_Transaksi_Formula_Error_Batal, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If


		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim noFakturRM As String = ListView1.FocusedItem.Text

			If CekButtonRole("Pembatalan_RM") = "T" Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Request Material", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan Faktur Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			If tanya = vbNo Then
				CloseTrans()
				CloseConn()
				Exit Sub
			End If

			'===========================================
			'=     CEK APAKAH RM SUDAH DI BATALKAN     =
			'===========================================
			SQL = "select Kode_Perusahaan from Emi_Material_Requisition where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & noFakturRM & "' and status is not null "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Request Material Sudah Dibatalkan Sebelumnya", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'========================================================
			'=     CEK APAKAH RM SUDAH DIGUNAKAN TRANSFER STOCK     =
			'========================================================
			SQL = "select a.Kode_Perusahaan, a.No_Faktur, c.Kode_Barang, c.Urut_Oto, d.Urut_Material_Requisition_Convert "
			SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_Det b, Emi_Material_Requisition_Det_Convert c, Tf_Stock d, Tf_Stock_Parent e "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Perusahaan = e.Kode_Perusahaan "
			SQL = SQL & "and a.No_Faktur = b.No_Faktur "
			SQL = SQL & "and b.No_Faktur = c.No_Faktur "
			SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and b.Urut_Oto = c.No_Urut_Det "
			SQL = SQL & "and c.Urut_Oto = d.Urut_Material_Requisition_Convert "
			SQL = SQL & "and d.No_Faktur = e.No_Faktur "
			SQL = SQL & "and a.status is null and e.status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Faktur = '" & noFakturRM & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Request Material Tidak Bisa Dibatalkan ; Request Material Sudah DiTransfer", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using


			'=====================================================
			'=     CEK APAKAH RM SUDAH DIGUNAKAN SPLIT STOCK     =
			'=====================================================
			SQL = "select a.Kode_Perusahaan, a.No_Faktur, c.Kode_Barang, c.Urut_Oto, d.Urut_Material_Requisition_Convert "
			SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_Det b, Emi_Material_Requisition_Det_Convert c, Tf_Stock_QC_Detail d, Tf_Stock_QC e "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Perusahaan = e.Kode_Perusahaan "
			SQL = SQL & "and a.No_Faktur = b.No_Faktur "
			SQL = SQL & "and b.No_Faktur = c.No_Faktur "
			SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and b.Urut_Oto = c.No_Urut_Det "
			SQL = SQL & "and c.Urut_Oto = d.Urut_Material_Requisition_Convert "
			SQL = SQL & "and d.No_Faktur = e.No_Faktur "
			SQL = SQL & "and a.status is null and e.status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Faktur = '" & noFakturRM & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Request Material Tidak Bisa Dibatalkan ; Request Material Sudah DiSplit", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'========================================
			'=     UPDATE FLAG REQUEST MATERIAL     =
			'========================================
			SQL = "update Emi_Material_Requisition set Status = 'Y', "
			SQL = SQL & "UserID_Batal = '" & UserID & "', Tanggal_Batal = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Batal = '" & Format(tgl_skg, "HH:mm:ss") & "' "
			SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & noFakturRM & "' and status is null "
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
		MessageBox.Show(Base_Language.Lang_Global_Berhasil_Batal, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		Button1_Click(BatalToolStripMenuItem, e)
		Exit Sub
	End Sub



	Private Sub SalinNoTransaksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoTransaksiToolStripMenuItem.Click
		If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Or ListView1.FocusedItem Is Nothing Then
			MessageBox.Show(Base_Language.Lang_Pilih_Dahulu_No_Transaksi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Clipboard.SetText(ListView1.FocusedItem.Text)
	End Sub


	Private Sub CetakUlangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangToolStripMenuItem.Click
		If ListView1.Items.Count = 0 Or ListView1.FocusedItem Is Nothing Or ListView1.FocusedItem.Index = -1 Then Exit Sub


		Try
			OpenConn()

			Dim CrDoc As New Object
			Dim SF As String = ""
			Dim kertas As String = ""

			Dim noFaktur As String = ListView1.FocusedItem.Text
			Dim noFakturOrder As String = ListView1.FocusedItem.SubItems(1).Text


			Dim tanya As String = MessageBox.Show("Yakin Ingin Cetak Ulang Faktur Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			If tanya = vbNo Then
				CloseConn()
				Exit Sub
			End If

			'===========================
			'=     GET DATA GUDANG     =
			'===========================
			SQL = "select distinct b.Kode_Stock_Owner_Tujuan "
			SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_Det b "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
			SQL = SQL & "and a.No_Faktur = b.No_Faktur "
			SQL = SQL & "and a.status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Faktur = '" & noFaktur & "' "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							Dim Lokasi As String = .Rows(i).Item("Kode_Stock_Owner_Tujuan")

							SQL = "select kode_perusahaan from Vw_Laporan_Faktur_Request_Material "
							SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
							SF = "{Vw_Laporan_Faktur_Request_Material.kode_perusahaan} = '" & KodePerusahaan & "' "

							SQL = SQL & "and no_faktur = '" & noFaktur & "' "
							SF = SF & "And {Vw_Laporan_Faktur_Request_Material.no_faktur} = '" & noFaktur & "' "

							SQL = SQL & "and Kode_Stock_Owner_Tujuan = '" & Lokasi & "' "
							SF = SF & "And {Vw_Laporan_Faktur_Request_Material.Kode_Stock_Owner_Tujuan} = '" & Lokasi & "' "

							SQL = SQL & "and no_faktur_Order = '" & noFakturOrder & "' "
							SF = SF & "And {Vw_Laporan_Faktur_Request_Material.no_faktur_Order} = '" & noFakturOrder & "' "
							Using Ds1 = BindingTrans(SQL)
								If Ds1.Tables("MyTable").Rows.Count <> 0 Then

									CrDoc = New Faktur_Request_Material_EMI

									'With A_Place_For_Printing2
									'    CrDoc.SetDataSource(Ds)
									'    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
									'    'CrDoc.PrintOptions.PrinterName = ""
									'    CrDoc.RecordSelectionFormula = SF
									'    CrDoc.SummaryInfo.ReportTitle = "Faktur Request Material "
									'    .Text = "Faktur Request Material"
									'    .CrystalReportViewer1.ReportSource = CrDoc
									'    .Refresh()
									'    .Show()
									'End With


									'=====================================

									kertas = "Faktur"

									CrDoc.SetDataSource(Ds)
									CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
									CrDoc.PrintOptions.PrinterName = PrinterNameSPB
									CrDoc.RecordSelectionFormula = SF
									'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

									Dim doctoprint As New System.Drawing.Printing.PrintDocument()
									doctoprint.PrinterSettings.PrinterName = PrinterNameSPB
									Dim rawKind As Integer
									CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
									For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
										If doctoprint.PrinterSettings.PaperSizes(j).PaperName = kertas Then
											rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
											CrDoc.PrintOptions.PaperSize = rawKind
											Exit For
										End If
									Next

									'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)

									'=======================================
									'=     CEK APAKAH KERTAS DITEMUKAN     =
									'=======================================
									If rawKind <> -1 Then
										CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
									Else
										CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
										Debug.Print("Ukuran kertas tidak ditemukan, menggunakan default.")
									End If

									CrDoc.PrintToPrinter(1, False, 1, 99)

									MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

								Else
									CloseConn()
									MessageBox.Show("Laporan Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using
						Next

					Else
						CloseConn()
						MessageBox.Show("No Request Material Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub

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

	Private Sub CetakUlangToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CetakUlangToolStripMenuItem1.Click
		If DataGridView2.Rows.Count = 0 Then Exit Sub

		Try
			OpenConn()

			Dim CrDoc As New Object
			Dim SF As String = ""
			Dim kertas As String = ""

			Dim noFaktur As String = ListView1.FocusedItem.Text
			Dim noFakturOrder As String = ListView1.FocusedItem.SubItems(1).Text

			Dim LokasiPilih As String = DataGridView2.Rows(DataGridView2.CurrentRow.Index).Cells(0).Value


			Dim tanya As String = MessageBox.Show("Yakin Ingin Cetak Ulang Faktur Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			If tanya = vbNo Then
				CloseConn()
				Exit Sub
			End If

			'===========================
			'=     GET DATA GUDANG     =
			'===========================
			SQL = "select distinct b.Kode_Stock_Owner_Tujuan "
			SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_Det b "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
			SQL = SQL & "and a.No_Faktur = b.No_Faktur "
			SQL = SQL & "and a.status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.No_Faktur = '" & noFaktur & "' "
			SQL = SQL & "and b.Kode_Stock_Owner_Tujuan = '" & LokasiPilih & "'"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							Dim Lokasi As String = .Rows(i).Item("Kode_Stock_Owner_Tujuan")

							SQL = "select kode_perusahaan from Vw_Laporan_Faktur_Request_Material "
							SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
							SF = "{Vw_Laporan_Faktur_Request_Material.kode_perusahaan} = '" & KodePerusahaan & "' "

							SQL = SQL & "and no_faktur = '" & noFaktur & "' "
							SF = SF & "And {Vw_Laporan_Faktur_Request_Material.no_faktur} = '" & noFaktur & "' "

							SQL = SQL & "and Kode_Stock_Owner_Tujuan = '" & Lokasi & "' "
							SF = SF & "And {Vw_Laporan_Faktur_Request_Material.Kode_Stock_Owner_Tujuan} = '" & Lokasi & "' "

							SQL = SQL & "and no_faktur_Order = '" & noFakturOrder & "' "
							SF = SF & "And {Vw_Laporan_Faktur_Request_Material.no_faktur_Order} = '" & noFakturOrder & "' "
							Using Ds1 = BindingTrans(SQL)
								If Ds1.Tables("MyTable").Rows.Count <> 0 Then

									CrDoc = New Faktur_Request_Material_EMI

									'With A_Place_For_Printing2
									'    CrDoc.SetDataSource(Ds)
									'    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
									'    'CrDoc.PrintOptions.PrinterName = ""
									'    CrDoc.RecordSelectionFormula = SF
									'    CrDoc.SummaryInfo.ReportTitle = "Faktur Request Material "
									'    .Text = "Faktur Request Material"
									'    .CrystalReportViewer1.ReportSource = CrDoc
									'    .Refresh()
									'    .Show()
									'End With


									'=====================================

									kertas = "Faktur"

									CrDoc.SetDataSource(Ds)
									CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
									CrDoc.PrintOptions.PrinterName = PrinterNameSPB
									CrDoc.RecordSelectionFormula = SF
									'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

									Dim doctoprint As New System.Drawing.Printing.PrintDocument()
									doctoprint.PrinterSettings.PrinterName = PrinterNameSPB
									Dim rawKind As Integer
									CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
									For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
										If doctoprint.PrinterSettings.PaperSizes(j).PaperName = kertas Then
											rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
											CrDoc.PrintOptions.PaperSize = rawKind
											Exit For
										End If
									Next

									'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)

									'=======================================
									'=     CEK APAKAH KERTAS DITEMUKAN     =
									'=======================================
									If rawKind <> -1 Then
										CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
									Else
										CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
										Debug.Print("Ukuran kertas tidak ditemukan, menggunakan default.")
									End If

									CrDoc.PrintToPrinter(1, False, 1, 99)
									CrDoc.Close()

									MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)



								Else
									CloseConn()
									MessageBox.Show("Laporan Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Exit Sub
								End If
							End Using
						Next

					Else
						CloseConn()
						MessageBox.Show("No Request Material Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub

					End If
				End With
			End Using

			'=== pastikan spooler dan Crystal Report menutup koneksi ===
			GC.Collect()
			GC.WaitForPendingFinalizers()

			' Beri jeda 1-2 detik supaya spooler sempat menyelesaikan job
			Threading.Thread.Sleep(1000)

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub


End Class