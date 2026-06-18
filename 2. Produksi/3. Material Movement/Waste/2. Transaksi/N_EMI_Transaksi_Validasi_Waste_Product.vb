Public Class N_EMI_Transaksi_Validasi_Waste_Product

	Dim DgvParent_NoTransaksi, DgvParent_NoFaktur, DgvParent_Lokasi, DgvParent_KodeStockOwner, DgvParent_Tanggal, DgvParent_Jam,
		DgvParent_Keterangan, DgvParent_UserInput, DgvParent_Btn As String

	Dim CellParent_NoTransaksi As Integer = 0
	Dim CellParent_NoFaktur As Integer = 1
	Dim CellParent_Lokasi As Integer = 2
	Dim CellParent_KodeStockOwner As Integer = 3
	Dim CellParent_Tanggal As Integer = 4
	Dim CellParent_Jam As Integer = 5
	Dim CellParent_Keterangan As Integer = 6
	Dim CellParent_UserInput As Integer = 7
	Dim CellParent_Btn As Integer = 8

	Dim Random As New Random()

	Private lastIndex As Integer = -1
	Private originalColor As Color

	Private lastHoverItem As ListViewItem = Nothing
	Private originalItemColor As Color

	Dim ArrFilter As New List(Of (ValueCombo As String, Sql As String)) From {
		(OpsiSeluruh, OpsiSeluruh),
		("No Transaksi", "b.No_Transaksi"),
		("No Faktur", "a.No_Faktur"),
		("Lokasi", "a.Lokasi"),
		("Kd SO", "a.Kode_Stock_Owner"),
		("Keterangan", "a.Keterangan"),
		("User Input", "a.UserID")
	}

	Private Sub N_EMI_Transaksi_Validasi_Waste_Product_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		EnableDoubleBufferDGV(Dgv_Parent)
		EnableDoubleBuffer(Lv_Product_User_Approve)
		EnableDoubleBuffer(Lv_Product_Detail_Barang)
		EnableDoubleBuffer(Lv_Product_Detail_Barcode)

		Lv_Product_User_Approve.Columns.Clear()
		Lv_Product_User_Approve.Columns.Add("Username", 200, HorizontalAlignment.Left)
		Lv_Product_User_Approve.Columns.Add("Approval Level", 100, HorizontalAlignment.Center)
		Lv_Product_User_Approve.Columns.Add("Status", 137, HorizontalAlignment.Center)
		Lv_Product_User_Approve.Columns.Add("Tanggal Approve", 110, HorizontalAlignment.Center)
		Lv_Product_User_Approve.Columns.Add("Jam Approve", 100, HorizontalAlignment.Center)
		Lv_Product_User_Approve.Columns.Add("id_user", 0, HorizontalAlignment.Left)
		Lv_Product_User_Approve.Columns.Add("Jabatan", 150, HorizontalAlignment.Left)
		Lv_Product_User_Approve.View = View.Details

		Lv_Product_User_Approve.Columns(6).DisplayIndex = 3

		Lv_Product_Detail_Barang.Columns.Clear()
		Lv_Product_Detail_Barang.Columns.Add("Kode Stock Owner", 130, HorizontalAlignment.Left)
		Lv_Product_Detail_Barang.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left)
		Lv_Product_Detail_Barang.Columns.Add("Nama Barang", 190, HorizontalAlignment.Left)
		Lv_Product_Detail_Barang.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
		Lv_Product_Detail_Barang.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
		Lv_Product_Detail_Barang.View = View.Details

		Lv_Product_Detail_Barcode.Columns.Clear()
		Lv_Product_Detail_Barcode.Columns.Add("Jenis", 130, HorizontalAlignment.Left)
		Lv_Product_Detail_Barcode.Columns.Add("No Transaksi", 130, HorizontalAlignment.Left)
		Lv_Product_Detail_Barcode.Columns.Add("Barcode Awal", 180, HorizontalAlignment.Left)
		Lv_Product_Detail_Barcode.Columns.Add("Barcode Tujuan", 180, HorizontalAlignment.Left)
		Lv_Product_Detail_Barcode.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
		Lv_Product_Detail_Barcode.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
		Lv_Product_Detail_Barcode.Columns.Add("Status", 100, HorizontalAlignment.Center).DisplayIndex = 1
		Lv_Product_Detail_Barcode.Columns.Add("No Split", 130, HorizontalAlignment.Left).DisplayIndex = 2
		Lv_Product_Detail_Barcode.View = View.Details

		For Each item In ArrFilter
			Cmb_Filter.Items.Add(item.ValueCombo)
		Next

		Kosong()
	End Sub

	Private Function Get_No_Faktur(ByVal InitialFaktur As String, ByVal NamaTabel As String, ByVal KolomTransaksi As String) As String
		Return InitialFaktur & Format(tgl_skg, "MMyy") & "-" &
							General_Class.Get_Last_Number2(NamaTabel, KolomTransaksi, 5,
							"Kode_perusahaan", KodePerusahaan,
							"And", "substring(" & KolomTransaksi & ", 1, " & Len(InitialFaktur) + 4 & ")", InitialFaktur & Format(tgl_skg, "MMyy"))
	End Function

	Private Sub Kosong()

		If Cmb_Filter.Items.Count > 0 Then
			Cmb_Filter.SelectedIndex = 0
		End If

		get_jam()
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Txt_No_Transaksi.Text = Get_No_Faktur("VPW", "N_EMI_Transaksi_Validasi_Pengajuan_Pemindahan_Waste", "No_Transaksi")

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		LoadDataParent()

	End Sub

	Private Sub Get_Lv_Data(ByVal index As Integer)
		DgvParent_NoTransaksi = Dgv_Parent.Rows(index).Cells(CellParent_NoTransaksi).Value
		DgvParent_NoFaktur = Dgv_Parent.Rows(index).Cells(CellParent_NoFaktur).Value
		DgvParent_Lokasi = Dgv_Parent.Rows(index).Cells(CellParent_Lokasi).Value
		DgvParent_KodeStockOwner = Dgv_Parent.Rows(index).Cells(CellParent_KodeStockOwner).Value
		DgvParent_Tanggal = Dgv_Parent.Rows(index).Cells(CellParent_Tanggal).Value
		DgvParent_Jam = Dgv_Parent.Rows(index).Cells(CellParent_Jam).Value
		DgvParent_Keterangan = Dgv_Parent.Rows(index).Cells(CellParent_Keterangan).Value
		DgvParent_UserInput = Dgv_Parent.Rows(index).Cells(CellParent_UserInput).Value
		DgvParent_Btn = Dgv_Parent.Rows(index).Cells(CellParent_Btn).Value
	End Sub

	Private Sub LoadDataParent()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim Filter As String = ""
			If Cmb_Filter.SelectedIndex > 0 Then
				Dim selectedFilter = ArrFilter(Cmb_Filter.SelectedIndex).Sql
				Filter = $"and {selectedFilter} like '%{Txt_Filter.Text}%'"
			End If

			Dgv_Parent.Rows.Clear() : Lv_Product_User_Approve.Items.Clear() : Lv_Product_Detail_Barang.Items.Clear() : Lv_Product_Detail_Barcode.Items.Clear()
			SQL = $"
				select b.No_Transaksi, a.No_Faktur, a.Lokasi, a.Kode_Stock_Owner, a.Tanggal, a.Jam, a.Keterangan,
								a.UserID as User_Input,
								isnull((x.isCompleted), 'Y') as isCompleted, a.Flag_Cetak_Faktur, isnull(y.Flag, 'Y') as isAllRejected
				from N_EMI_Transaksi_Transfer_Waste_Produk a
					 inner join N_EMI_Transaksi_Approval_Waste b
								on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste
					 outer apply(
									select top 1 'T' as isCompleted
									from N_EMI_Transaksi_Approval_Waste z
									where z.Kode_Perusahaan = a.Kode_Perusahaan
									  and z.No_Faktur_Waste = a.No_Faktur
									  and z.Status is null
									  and z.Flag_Approve is null
								) x
					 LEFT JOIN (
								   select distinct z.Kode_Perusahaan, z.No_Faktur, 'T' as Flag
								   from N_EMI_Transaksi_Transfer_Waste_Produk_Det z
							   ) y on a.Kode_Perusahaan = y.Kode_Perusahaan and a.No_Faktur = y.No_Faktur
				where a.Status is null
				  and b.Status is null
				  and a.Kode_Perusahaan = '{KodePerusahaan}'
				  and b.Jenis_Approval = 'Waste_Produk'
				  and a.Flag_Waste_Product = 'Y'
				  and b.Approval_Level = 1
				  and b.Flag_Selesai is null
				  and a.Flag_Data_Baru_Validasi_Pemindahan = 'Y'
				  {Filter}
				  and x.isCompleted is null
				  and y.Flag is not null
				 order by b.Tanggal, b.Jam
			"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							Dgv_Parent.Rows.Add()
							Dgv_Parent.Rows(i).Cells(CellParent_NoTransaksi).Value = .Rows(i).Item("No_Transaksi")
							Dgv_Parent.Rows(i).Cells(CellParent_NoFaktur).Value = .Rows(i).Item("No_Faktur")
							Dgv_Parent.Rows(i).Cells(CellParent_Lokasi).Value = .Rows(i).Item("Lokasi")
							Dgv_Parent.Rows(i).Cells(CellParent_KodeStockOwner).Value = .Rows(i).Item("Kode_Stock_Owner")
							Dgv_Parent.Rows(i).Cells(CellParent_Tanggal).Value = Format(.Rows(i).Item("Tanggal"), "dd MMM yyyyS")
							Dgv_Parent.Rows(i).Cells(CellParent_Jam).Value = .Rows(i).Item("Jam")
							Dgv_Parent.Rows(i).Cells(CellParent_Keterangan).Value = .Rows(i).Item("Keterangan")
							Dgv_Parent.Rows(i).Cells(CellParent_UserInput).Value = .Rows(i).Item("User_Input")
							Dgv_Parent.Rows(i).Cells(CellParent_Btn).Value = "Validasi"

							With Dgv_Parent.Rows(i).Cells(CellParent_Btn).Style
								.BackColor = Color.FromArgb(15, 86, 122)
								.ForeColor = Color.White
							End With

						Next
						'Else
						'	CloseTrans()
						'	CloseConn()
						'	MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'	Exit Sub
					End If
				End With
			End Using

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

	Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
		LoadDataParent()
	End Sub

	Private Sub Dgv_Parent_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Parent.CellContentClick
		If e.RowIndex < 0 Then Exit Sub

		If e.ColumnIndex = CellParent_Btn Then
			If (MessageBox.Show($"Yakin Ingin Memvalidasi data ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = vbNo Then Exit Sub

			get_jam()
			Dim NoTransaksi As String = Dgv_Parent.Rows(e.RowIndex).Cells(CellParent_NoTransaksi).Value
			Dim NoFaktur As String = Dgv_Parent.Rows(e.RowIndex).Cells(CellParent_NoFaktur).Value

			Try
				OpenConn()
				Cmd.Transaction = Cn.BeginTransaction

				Txt_No_Transaksi.Text = Get_No_Faktur("VPW", "N_EMI_Transaksi_Validasi_Pengajuan_Pemindahan_Waste", "No_Transaksi")

				'==========================================================
				'=     CEK APAKAH DATA SUDAH APPROVE SEMUA ATAU BELUM     =
				'==========================================================
				SQL = $"
				select b.No_Transaksi, a.No_Faktur, b.Jenis_Approval,
					isnull((x.isCompleted), 'Y') as isCompleted, isnull(y.Flag, 'Y') as isAllRejected
				from N_EMI_Transaksi_Transfer_Waste_Produk a
					 inner join N_EMI_Transaksi_Approval_Waste b
								on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste
					 outer apply(
									select top 1 'T' as isCompleted
									from N_EMI_Transaksi_Approval_Waste z
									where z.Kode_Perusahaan = a.Kode_Perusahaan
									  and z.No_Faktur_Waste = a.No_Faktur
									  and z.Status is null
									  and z.Flag_Approve is null
								) x
					 LEFT JOIN (
								   select distinct z.Kode_Perusahaan, z.No_Faktur, 'T' as Flag
								   from N_EMI_Transaksi_Transfer_Waste_Produk_Det z
							   ) y on a.Kode_Perusahaan = y.Kode_Perusahaan and a.No_Faktur = y.No_Faktur
				where a.Status is null
				  and b.Status is null
				  and a.Kode_Perusahaan = '{KodePerusahaan}'
				  and b.Jenis_Approval = 'Waste_Produk'
				  and a.Flag_Waste_Product = 'Y'
				  and b.Approval_Level = 1
				  and b.Flag_Selesai is null
				  and b.No_Transaksi = '{NoTransaksi}'
				  and a.No_Faktur = '{NoFaktur}'
			"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then

						Dim asdsadas As String = Dr("Jenis_Approval")
						If General_Class.CekNULL(Dr("isCompleted")) <> "Y" Then
							Dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show($"Terjadi Kesalahan, Data Approval {NoTransaksi} Belum Selesai", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						ElseIf General_Class.CekNULL(Dr("isAllRejected")) = "Y" Then
							Dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show($"Terjadi Kesalahan, Data Approval {NoTransaksi} Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						ElseIf General_Class.CekNULL(Dr("Jenis_Approval")).ToString.Trim <> "Waste_Produk" Then
							Dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show($"Terjadi Kesalahan, Data Approval {NoTransaksi} Bukan Waste Produk", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Data Approval {NoTransaksi} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'=========================
				'=     INSERT PARENT     =
				'=========================
				SQL = $"
					INSERT INTO N_EMI_Transaksi_Validasi_Pengajuan_Pemindahan_Waste
						(Kode_Perusahaan, No_Transaksi, No_Transaksi_Approval, No_Faktur_Waste, Tanggal, Jam, User_ID)
					VALUES
						('{KodePerusahaan}', '{Txt_No_Transaksi.Text.Trim}', '{NoTransaksi}', '{NoFaktur}',
						'{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', '{UserID}')
				"
				ExecuteTrans(SQL)

				'===============================
				'=     HANDLE PROSES WASTE     =
				'===============================
				Handle_Process_Waste(NoFaktur, NoTransaksi)

				Cmd.Transaction.Commit()
				CloseTrans()
				CloseConn()
				MessageBox.Show($"Transaksi {NoTransaksi} Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
			Catch ex As Exception
				CloseTrans()
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try

			Kosong()

		End If
	End Sub

	Private Sub Dgv_Parent_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Parent.CellClick
		If Dgv_Parent.Rows.Count = 0 Then Exit Sub

		Try
			OpenConn()

			Dim No_faktur As String = Dgv_Parent.CurrentRow.Cells(CellParent_NoFaktur).Value
			Dim No_Transaksi As String = Dgv_Parent.CurrentRow.Cells(CellParent_NoTransaksi).Value

			Lv_Product_User_Approve.Items.Clear()
			SQL = "select case when b.Approval_Level<>'1' then c.username else d.UserName end as username, "
			SQL = SQL & "b.Approval_Level, b.Flag_Approve, b.Tanggal_Approve, b.Jam_Approve, b.Id_User_Android_Approve, b.jabatan "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
			SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
			SQL = SQL & "left join Emi_Users c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_User_Android_Approve = c.id "
			SQL = SQL & "left join users d on b.Kode_Perusahaan = d.Kode_Perusahaan and b.User_ID_Desktop = d.UserID "
			SQL = SQL & "where a.Status is null and b.Status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Flag_Waste_Product = 'Y' "
			SQL = SQL & "and a.No_Faktur = '" & No_faktur & "' "
			SQL = SQL & "and b.No_Transaksi = '" & No_Transaksi & "' "
			SQL = SQL & "order by b.Approval_Level ASC "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim Lv As ListViewItem
						Lv = Lv_Product_User_Approve.Items.Add(If(General_Class.CekNULL(Dr("username")) = "", "-", Dr("username")))
						Lv.SubItems.Add(Dr("Approval_Level"))

						If General_Class.CekNULL(Dr("Flag_Approve")) = "Y" Then
							Lv.SubItems.Add("Approved")
							Lv.BackColor = Color.LightGreen
						ElseIf General_Class.CekNULL(Dr("Flag_Approve")) = "T" Then
							Lv.SubItems.Add("Rejected")
							Lv.ForeColor = Color.White
							Lv.BackColor = Color.DarkRed
						Else
							Lv.SubItems.Add("On Process")
						End If

						Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Tanggal_Approve")) = "", "-", Format(Dr("Tanggal_Approve"), "dd MMM yyyy")))
						Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jam_Approve")) = "", "-", Dr("Jam_Approve")))
						Lv.SubItems.Add(Dr("Id_User_Android_Approve"))
						Lv.SubItems.Add(Dr("jabatan"))
					Loop While Dr.Read
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Data User Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			Lv_Product_Detail_Barang.Items.Clear()
			SQL = "select a.Kode_Stock_Owner as Lokasi, b.kode_barang, c.Nama as Nama_Barang, b.Total, b.Satuan "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
			SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
			SQL = SQL & "inner join barang c on a.kode_perusahaan = c.kode_perusahaan and a.kode_stock_owner = c.kode_Stock_owner and b.kode_barang = c.kode_barang "
			SQL = SQL & "where a.Status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Flag_Waste_Product = 'Y' "
			SQL = SQL & "and a.No_Faktur = '" & No_faktur & "' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim Lv As ListViewItem
						Lv = Lv_Product_Detail_Barang.Items.Add(Dr("Lokasi"))
						Lv.SubItems.Add(Dr("kode_barang"))
						Lv.SubItems.Add(Dr("Nama_Barang"))
						Lv.SubItems.Add(Format(Dr("Total"), "N4"))
						Lv.SubItems.Add(Dr("Satuan"))
					Loop While Dr.Read
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Data Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=======================================
			'=     LOAD DATA DETAIL PERBARCODE     =
			'=======================================
			Lv_Product_Detail_Barcode.Items.Clear()

			SQL = $"
				select a.kode_perusahaan, 'Good Receiver 1' as asal, 'Approved' as StatusBarcode, a.No_Faktur, f.No_Transaksi, f.No_Production_Order as No_Split,
					   (g.qr_code + '-' + g.kode_unik_berjalan) as Barcode_Awal,
					   (h.qr_code + '-' + h.kode_unik_berjalan) as Barcode_Akhir, sum(c.Jumlah) as Jumlah, b.Satuan
				from N_EMI_Transaksi_Transfer_Waste_Produk a
					 inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b
								on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
					 inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det c
								on b.Kode_Perusahaan = c.kode_perusahaan and b.No_Faktur = c.no_faktur and b.Urut_Oto = c.urut_tf
					 left join N_EMI_Transaksi_Transfer_Waste_Produk_Det2 d
							   on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det
					 inner join Emi_Production_Results_Detail_Scrap e
								on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Serial_Number_Awal = e.Serial_Number
					 inner join Emi_Production_Results f
								on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Transaksi = f.No_Transaksi and f.Status is null
					 inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and c.Serial_Number_Awal = g.Serial_Number
					 left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				  and a.No_Faktur = '{No_faktur}'
				group by a.kode_perusahaan, a.No_Faktur, f.No_Transaksi, b.Satuan, (g.qr_code + '-' + g.kode_unik_berjalan),
						 (h.qr_code + '-' + h.kode_unik_berjalan), f.No_Production_Order

				union all

				select a.kode_perusahaan, 'Good Receive 1' as asal, 'Rejected' as StatusBarcode, a.No_Faktur, f.No_Transaksi, f.No_Production_Order,
					   (g.qr_code + '-' + g.kode_unik_berjalan) as Barcode_Awal,
					   (h.qr_code + '-' + h.kode_unik_berjalan) as Barcode_Akhir, sum(c.Jumlah) as Jumlah, b.Satuan
				from N_EMI_Transaksi_Transfer_Waste_Produk a
					 inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b
								on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
					 inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det_Log_Approval c
								on b.Kode_Perusahaan = c.kode_perusahaan and b.No_Faktur = c.no_faktur and b.Urut_Oto = c.urut_tf
					 left join N_EMI_Transaksi_Transfer_Waste_Produk_Det2 d
							   on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det
					 inner join Emi_Production_Results_Detail_Scrap e
								on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Serial_Number_Awal = e.Serial_Number
					 inner join Emi_Production_Results f
								on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Transaksi = f.No_Transaksi and f.Status is null
					 inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and c.Serial_Number_Awal = g.Serial_Number
					 left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number
					 left join N_EMI_Transaksi_Transfer_Waste_Produk_Det i
							   on b.Kode_Perusahaan = i.kode_perusahaan and b.No_Faktur = i.no_faktur and b.Urut_Oto = i.urut_tf and
								  c.Serial_Number_Awal = i.Serial_Number_Awal
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				  and a.No_Faktur = '{No_faktur}'
				  and i.Kode_Perusahaan is null
				group by a.kode_perusahaan, a.No_Faktur, f.No_Transaksi, b.Satuan, (g.qr_code + '-' + g.kode_unik_berjalan),
						 (h.qr_code + '-' + h.kode_unik_berjalan), f.No_Production_Order

				union all

				select a.kode_perusahaan, 'Good Receive 2' as asal, 'Approved' as StatusBarcode, a.No_Faktur, f.No_Transaksi, f.No_Production_Order as No_Split,
					   (g.qr_code + '-' + g.kode_unik_berjalan) as Barcode_Awal,
					   (h.qr_code + '-' + h.kode_unik_berjalan) as Barcode_Akhir, sum(e.Jumlah) as Jumlah, e.Satuan
				from N_EMI_Transaksi_Transfer_Waste_Produk a
					 inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b
								on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
					 inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det c
								on b.Kode_Perusahaan = c.kode_perusahaan and b.No_Faktur = c.no_faktur and b.Urut_Oto = c.urut_tf
					 left join N_EMI_Transaksi_Transfer_Waste_Produk_Det2 d
							   on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det
					 inner join Emi_Production_Results_Validation_Detail e
								on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Serial_Number_Awal = e.Serial_Number_Tujuan
					 inner join Emi_Production_Results_Validation f
								on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Transaksi = f.No_Transaksi and f.Status is null
					 inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and c.Serial_Number_Awal = g.Serial_Number
					 left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				  and a.No_Faktur = '{No_faktur}'
				group by a.kode_perusahaan, a.No_Faktur, f.No_Transaksi, e.Satuan, (g.qr_code + '-' + g.kode_unik_berjalan),
						 (h.qr_code + '-' + h.kode_unik_berjalan), f.No_Production_Order

				union all

				select a.kode_perusahaan, 'Good Receive 2' as asal, 'Rejected' as StatusBarcode, a.No_Faktur, f.No_Transaksi, f.No_Production_Order as No_Split,
					   (g.qr_code + '-' + g.kode_unik_berjalan) as Barcode_Awal,
					   (h.qr_code + '-' + h.kode_unik_berjalan) as Barcode_Akhir, sum(e.Jumlah) as Jumlah, e.Satuan
				from N_EMI_Transaksi_Transfer_Waste_Produk a
					 inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b
								on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
					 inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det_Log_Approval c
								on b.Kode_Perusahaan = c.kode_perusahaan and b.No_Faktur = c.no_faktur and b.Urut_Oto = c.urut_tf
					 left join N_EMI_Transaksi_Transfer_Waste_Produk_Det2 d
							   on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det
					 inner join Emi_Production_Results_Validation_Detail e
								on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Serial_Number_Awal = e.Serial_Number_Tujuan
					 inner join Emi_Production_Results_Validation f
								on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Transaksi = f.No_Transaksi and f.Status is null
					 inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and c.Serial_Number_Awal = g.Serial_Number
					 left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number
					 left join N_EMI_Transaksi_Transfer_Waste_Produk_Det i
							   on b.Kode_Perusahaan = i.kode_perusahaan and b.No_Faktur = i.no_faktur and b.Urut_Oto = i.urut_tf and
								  c.Serial_Number_Awal = i.Serial_Number_Awal
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				  and a.No_Faktur = '{No_faktur}'
				  and i.Kode_Perusahaan is null
				group by a.kode_perusahaan, a.No_Faktur, f.No_Transaksi, e.Satuan, (g.qr_code + '-' + g.kode_unik_berjalan),
						 (h.qr_code + '-' + h.kode_unik_berjalan), f.No_Production_Order

				union all

				  select a.kode_perusahaan, 'Good Receive 3' as asal, 'Approved' as StatusBarcode, a.No_Faktur, f.No_Transaksi,
						   f.No_Production_Order as No_Split,
						   (g.qr_code + '-' + g.kode_unik_berjalan) as Barcode_Awal,
						   (h.qr_code + '-' + h.kode_unik_berjalan) as Barcode_Akhir, sum(e.Jumlah) as Jumlah, e.Satuan
					from N_EMI_Transaksi_Transfer_Waste_Produk a
						 inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b
									on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
						 inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det c
									on b.Kode_Perusahaan = c.kode_perusahaan and b.No_Faktur = c.no_faktur and b.Urut_Oto = c.urut_tf
						 left join N_EMI_Transaksi_Transfer_Waste_Produk_Det2 d
								   on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det
						 inner join N_EMI_Validation_GR_3_Detail e
									on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Serial_Number_Awal = e.Serial_Number_Tujuan
						 inner join N_EMI_Validation_GR_3 f
									on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Transaksi = f.No_Transaksi and f.Status is null
						 inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and c.Serial_Number_Awal = g.Serial_Number
						 left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number
						 left join N_EMI_Transaksi_Transfer_Waste_Produk_Det i
								   on b.Kode_Perusahaan = i.kode_perusahaan and b.No_Faktur = i.no_faktur and b.Urut_Oto = i.urut_tf and
									  c.Serial_Number_Awal = i.Serial_Number_Awal
					where a.Kode_Perusahaan = '{KodePerusahaan}'
					  and a.No_Faktur = '{No_faktur}'
					group by a.kode_perusahaan, a.No_Faktur, f.No_Transaksi, e.Satuan, (g.qr_code + '-' + g.kode_unik_berjalan),
							 (h.qr_code + '-' + h.kode_unik_berjalan), f.No_Production_Order

				  union all

				select a.kode_perusahaan, 'Waste Packaging' as asal, 'Approved' as StatusBarcode, a.No_Faktur, f.No_Transaksi, f.No_Split,
					   (g.qr_code + '-' + g.kode_unik_berjalan) as Barcode_Awal,
					   (h.qr_code + '-' + h.kode_unik_berjalan) as Barcode_Akhir, sum(e.Jumlah_Tujuan) as Jumlah,
					   e.Satuan_Tujuan as Satuan
				from N_EMI_Transaksi_Transfer_Waste_Produk a
					 inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b
								on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
					 inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det c
								on b.Kode_Perusahaan = c.kode_perusahaan and b.No_Faktur = c.no_faktur and b.Urut_Oto = c.urut_tf
					 left join N_EMI_Transaksi_Transfer_Waste_Produk_Det2 d
							   on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det
					 inner join EMI_Production_Results_Detail_Change_Packaging_Detail e
								on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Serial_Number_Awal = e.SN_Scrap
					 inner join EMI_Production_Results_Detail_Change_Packaging f
								on e.Kode_Perusahaan = f.Kode_Perusahaan and e.no_transaksi = f.no_transaksi and f.Status is null
					 inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and c.Serial_Number_Awal = g.Serial_Number
					 left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				  and a.No_Faktur = '{No_faktur}'
				group by a.kode_perusahaan, a.No_Faktur, f.No_Transaksi, e.Satuan_Tujuan, (g.qr_code + '-' + g.kode_unik_berjalan),
						 (h.qr_code + '-' + h.kode_unik_berjalan), f.No_Split

				union all

				select a.kode_perusahaan, 'Waste Packaging' as asal, 'Rejected' as StatusBarcode, a.No_Faktur, f.No_Transaksi, f.No_Split,
					   (g.qr_code + '-' + g.kode_unik_berjalan) as Barcode_Awal,
					   (h.qr_code + '-' + h.kode_unik_berjalan) as Barcode_Akhir, sum(e.Jumlah_Tujuan) as Jumlah,
					   e.Satuan_Tujuan as Satuan
				from N_EMI_Transaksi_Transfer_Waste_Produk a
					 inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b
								on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
					 inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det_Log_Approval c
								on b.Kode_Perusahaan = c.kode_perusahaan and b.No_Faktur = c.no_faktur and b.Urut_Oto = c.urut_tf
					 left join N_EMI_Transaksi_Transfer_Waste_Produk_Det2 d
							   on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det
					 inner join EMI_Production_Results_Detail_Change_Packaging_Detail e
								on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Serial_Number_Awal = e.SN_Scrap
					 inner join EMI_Production_Results_Detail_Change_Packaging f
								on e.Kode_Perusahaan = f.Kode_Perusahaan and e.no_transaksi = f.no_transaksi and f.Status is null
					 inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and c.Serial_Number_Awal = g.Serial_Number
					 left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number
					 left join N_EMI_Transaksi_Transfer_Waste_Produk_Det i
							   on b.Kode_Perusahaan = i.kode_perusahaan and b.No_Faktur = i.no_faktur and b.Urut_Oto = i.urut_tf and
								  c.Serial_Number_Awal = i.Serial_Number_Awal
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				  and a.No_Faktur = '{No_faktur}'
				  and i.Kode_Perusahaan is null
				group by a.kode_perusahaan, a.No_Faktur, f.No_Transaksi, e.Satuan_Tujuan, (g.qr_code + '-' + g.kode_unik_berjalan),
						 (h.qr_code + '-' + h.kode_unik_berjalan), f.No_Split
				order by No_Faktur

			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim Lv As ListViewItem
						Lv = Lv_Product_Detail_Barcode.Items.Add(Dr("asal"))
						Lv.SubItems.Add(Dr("No_Transaksi"))
						Lv.SubItems.Add(Dr("Barcode_Awal"))
						Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Barcode_Akhir")) = "", "-", Dr("Barcode_Akhir")))
						Lv.SubItems.Add(Format(Dr("Jumlah"), "N4"))
						Lv.SubItems.Add(Dr("Satuan"))
						Lv.SubItems.Add(Dr("StatusBarcode"))
						Lv.SubItems.Add(Dr("No_Split"))

						If General_Class.CekNULL(Dr("StatusBarcode")) = "Rejected" Then
							Lv.BackColor = Color.DarkRed
							Lv.ForeColor = Color.White
						End If
					Loop While Dr.Read
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Data Barcode tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Dgv_Parent_MouseMove(sender As Object, e As MouseEventArgs) Handles Dgv_Parent.MouseMove
		HandleDataGridViewHover(sender, e)
	End Sub

	Private Sub Cmb_Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter.SelectedIndexChanged
		If Cmb_Filter.Items.Count = 0 Then Exit Sub

		Txt_Filter.Text = ""
		If Cmb_Filter.SelectedIndex = 0 Then
			Txt_Filter.BackColor = Color.FromArgb(235, 235, 235)
			Txt_Filter.Enabled = False
		Else
			Txt_Filter.BackColor = Color.White
			Txt_Filter.Enabled = True
		End If
	End Sub

	'========================================================================================================================
	'=     UTILITY
	'========================================================================================================================

	Private Sub EnableDoubleBufferDGV(dgv As DataGridView)
		Dim t As Type = dgv.GetType()
		Dim prop = t.GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
		prop.SetValue(dgv, True, Nothing)
	End Sub

	Private Sub EnableDoubleBuffer(lvw As ListView)
		Dim t As Type = lvw.GetType()
		Dim prop = t.GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
		prop.SetValue(lvw, True, Nothing)
	End Sub

	Private Sub HandleListViewHover(lvw As ListView, e As MouseEventArgs)
		Dim hit As ListViewHitTestInfo = lvw.HitTest(e.Location)

		lvw.Cursor = If(hit.Item IsNot Nothing, Cursors.Hand, Cursors.Default)

		If hit.Item IsNot lastHoverItem Then
			lvw.BeginUpdate()

			If lastHoverItem IsNot Nothing Then
				lastHoverItem.BackColor = originalItemColor
			End If

			If hit.Item IsNot Nothing AndAlso hit.Item.Tag Is Nothing Then
				lastHoverItem = hit.Item
				originalItemColor = lastHoverItem.BackColor

				Dim amt As Integer = 10
				lastHoverItem.BackColor = Color.FromArgb(
				Math.Max(0, originalItemColor.R - amt),
				Math.Max(0, originalItemColor.G - amt),
				Math.Max(0, originalItemColor.B - amt)
			)
			Else
				lastHoverItem = Nothing
			End If

			lvw.EndUpdate()
		End If
	End Sub

	Private Sub HandleDataGridViewHover(dgv As DataGridView, e As MouseEventArgs)
		Dim hit As DataGridView.HitTestInfo = dgv.HitTest(e.X, e.Y)

		dgv.Cursor = If(hit.Type = DataGridViewHitTestType.Cell, Cursors.Hand, Cursors.Default)

		If hit.RowIndex <> lastIndex Then

			If lastIndex >= 0 AndAlso lastIndex < dgv.Rows.Count Then
				dgv.Rows(lastIndex).DefaultCellStyle.BackColor = originalColor
			End If

			If hit.Type = DataGridViewHitTestType.Cell AndAlso hit.RowIndex >= 0 Then
				lastIndex = hit.RowIndex

				Dim currentRow = dgv.Rows(lastIndex)

				originalColor = currentRow.DefaultCellStyle.BackColor

				Dim displayColor As Color = currentRow.InheritedStyle.BackColor

				Dim amount As Integer = 23
				currentRow.DefaultCellStyle.BackColor = Color.FromArgb(
					Math.Max(0, displayColor.R - amount),
					Math.Max(0, displayColor.G - amount),
					Math.Max(0, displayColor.B - amount)
				)
			Else
				lastIndex = -1
			End If
		End If
	End Sub

	Private Sub Lv_Product_User_Approve_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Product_User_Approve.MouseMove, Lv_Product_Detail_Barang.MouseMove, Lv_Product_Detail_Barcode.MouseMove
		HandleListViewHover(sender, e)
	End Sub

	Private Sub Handle_Process_Waste(ByVal No_Faktur_Waste As String, ByVal No_Transaksi_Approval As String)

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
								Dim Pesan As String = "Proses tidak bisa dilanjutkan, barang sudah dibatalkan!!"
								Throw New Exception(Pesan)
							ElseIf General_Class.CekNULL(Dr("selesai")) = "Y" Then
								Dr.Close()
								Dim Pesan As String = "Terjadi kesalahan, barang sudah selesai diproses!"
								Throw New Exception(Pesan)

							ElseIf General_Class.CekNULL(Dr("Flag_Timbang")) = "Y" Then
								Dr.Close()
								Dim Pesan As String = "Terjadi kesalahan, barang sudah Ditimbang!"
								Throw New Exception(Pesan)
							End If
						Else
							Dr.Close()
							Dim Pesan As String = "Data barang tidak ditemukan!"
							Throw New Exception(Pesan)

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

								Dim Pesan As String = "data konversi satuan kirim tidak ada"
								Throw New Exception(Pesan)

							End If

							nilai_kecildetail = Dr1("hasil")
						Else
							Dr1.Close()
							Dim Pesan As String = "data konversi satuan kirim tidak ada"
							Throw New Exception(Pesan)

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
							Dim Pesan As String = "Data SN tidak ditemukan!"
							Throw New Exception(Pesan)
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
								Dim Pesan As String = "Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif."
								Throw New Exception(Pesan)

							ElseIf dr("Jumlah_Bags") < JumlahBagsEstimasi Then
								dr.Close()
								Dim Pesan As String = "Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif."
								Throw New Exception(Pesan)
							Else
								dr.Close()
								SQL = "update barang set Good_Stock = Good_Stock - Round(" & JumlahEstimasi & ",4), Jumlah_Bags = Jumlah_Bags - " & JumlahBagsEstimasi & " "
								SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & KdSo & "' "
								SQL = SQL & " and Kode_Barang='" & KdBarang & "'"
								ExecuteTrans(SQL)
							End If
						Else
							dr.Close()
							Dim Pesan As String = "Barang " & Nama & " tidak ditemukan!"
							Throw New Exception(Pesan)

						End If
					End Using

					SQL = "select round(jumlah,4) as jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & KdSo & "' "
					SQL = SQL & "and Kode_Barang='" & KdBarang & "' "
					SQL = SQL & "and Serial_Number='" & SN & "'"
					Using dr = OpenTrans(SQL)
						If dr.Read Then
							If dr("jumlah") < JumlahEstimasi Then
								dr.Close()
								Dim Pesan As String = "Proses tidak dapat dilanjutkan karena akan membuat stock"
								Throw New Exception(Pesan)

							ElseIf dr("Jumlah_Bags") < JumlahBagsEstimasi Then
								dr.Close()
								Dim Pesan As String = "Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif."
								Throw New Exception(Pesan)
							Else
								dr.Close()
								SQL = "update barang_sn set jumlah = jumlah - Round(" & JumlahEstimasi & ",4), Jumlah_Bags = Jumlah_Bags - " & JumlahBagsEstimasi & " "
								SQL = SQL & "where Kode_Stock_Owner='" & KdSo & "' and Kode_Barang='" & KdBarang & "' "
								SQL = SQL & "and Serial_Number='" & SN & "'"
								ExecuteTrans(SQL)
							End If
						Else
							dr.Close()
							Dim Pesan As String = "Barang " & Nama & " tidak ditemukan!"
							Throw New Exception(Pesan)

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
								Dim Pesan As String = "Terjadi Kesalahan . . ! !, Stock Tidak Sama Saat Potong Stock"
								Throw New Exception(Pesan)

							End If
						Else
							Dim Pesan As String = "Waste Product: Data tidak ditemukan . . ! !"
							Throw New Exception(Pesan)

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
								Dim Pesan As String = "Terjadi Kesalahan . . ! !, Stock Tidak Sama Saat Potong Stock"
								Throw New Exception(Pesan)

							End If
						Else
							Dim Pesan As String = "Data tidak ditemukan . . ! , Saat Tambah Stock"
							Throw New Exception(Pesan)

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
							Dim Pesan As String = "Data akun tidak ditemukan!"
							Throw New Exception(Pesan)

						End If
					End Using

					SQL = "select akun_gantung_waste_produk from stock_owner_gudang where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & KdSoTujuan & "' "
					Using Dr = OpenTrans(SQL)
						If Dr.Read Then
							akun_persediaan_dari = Dr("akun_gantung_waste_produk")
						Else
							Dr.Close()
							Dim Pesan As String = "Data akun tidak ditemukan!"
							Throw New Exception(Pesan)

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
							Dim Pesan As String = "Data akun tidak ditemukan!"
							Throw New Exception(Pesan)

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
								Dim Pesan As String = "Jurnal salah!"
								Throw New Exception(Pesan)

							End If
						Else
							Dr.Close()
							Dim Pesan As String = "Data jurnal tidak ditemukan!"
							Throw New Exception(Pesan)

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
			End If
		End Using

#End Region

		SQL = "update N_EMI_Transaksi_Approval_Waste set Flag_Selesai = 'Y' "
		SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Status is null and No_Transaksi = '" & No_Transaksi_Approval & "' and No_Faktur_Waste = '" & No_Faktur_Waste & "' "
		ExecuteTrans(SQL)

	End Sub

End Class