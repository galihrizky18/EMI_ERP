Public Class N_EMI_Display_Approval_Waste_Process

	Private lastHoverItem As ListViewItem = Nothing
	Private originalItemColor As Color

	Dim arrFilterLokasiGudang, arrFilterTanggal, arrFilterParamLain As New ArrayList
	Dim arrFilterTanggal_2, arrFilterParamLain_2 As New ArrayList

	Dim Lv_Process_NoTransaksiApproval, Lv_Process_NoFaktur, Lv_Process_KdStock_Owner, Lv_Process_Lokasi, Lv_Process_Tanggal, Lv_Process_Jam, Lv_Process_Keterangan, Lv_Process_UserInput As String

	Dim Item_Process_NoTransaksiApproval As Integer = 0
	Dim Item_Process_NoFaktur As Integer = 1
	Dim Item_Process__KdStock_Owner As Integer = 2
	Dim Item_Process_Lokasi As Integer = 3
	Dim Item_Process_Tanggal As Integer = 4
	Dim Item_Process_Jam As Integer = 5
	Dim Item_Process_Keterangan As Integer = 6
	Dim Item_Process_UserInput As Integer = 7

	Dim Lv_User_Approve_Username, Lv_User_Approve_Level, Lv_User_Approve_Status, Lv_User_Approve_TanggalApprove, Lv_User_Approve_JamApprove, Lv_User_Approve_iduser As String

	Dim item_User_Approve_Username As Integer = 0
	Dim item_User_Approve_Level As Integer = 1
	Dim item_User_Approve_Status As Integer = 2
	Dim item_User_Approve_TanggalApprove As Integer = 3
	Dim item_User_Approve_JamApprove As Integer = 4
	Dim item_User_Approve_iduser As Integer = 5

	Dim arrFilterTab1 As New ArrayList

	Dim Lv_Product_NoTransaksiApproval, Lv_Product_NoFaktur, Lv_Product_KdStock_Owner, Lv_Product_Lokasi, Lv_Product_Tanggal, Lv_Product_Jam, Lv_Product_Keterangan, Lv_Product_UserInput As String

	Dim Item_Product_NoTransaksiApproval As Integer = 0
	Dim Item_Product_NoFaktur As Integer = 1
	Dim Item_Product__KdStock_Owner As Integer = 2
	Dim Item_Product_Lokasi As Integer = 3
	Dim Item_Product_Tanggal As Integer = 4
	Dim Item_Product_Jam As Integer = 5
	Dim Item_Product_Keterangan As Integer = 6
	Dim Item_Product_UserInput As Integer = 7
	Dim arrFilterTab2 As New ArrayList

	Private Sub N_EMI_Display_Approval_Waste_Process_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub N_EMI_Display_Approval_Waste_Process_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		EnableDoubleBuffer(Lv_Product_Data)
		EnableDoubleBuffer(Lv_Product_User_Approve)
		EnableDoubleBuffer(Lv_Product_Detail_Barang)
		EnableDoubleBuffer(Lv_Product_Detail_Barcode)
		EnableDoubleBuffer(Lv_Process_Data)
		EnableDoubleBuffer(Lv_Process_User_Approve)
		EnableDoubleBuffer(Lv_Process_Detail_Barang)
		EnableDoubleBuffer(Lv_Process_Detail_Barcode)

#Region "HANDLE ADD HANDLER"

		AddHandler Lv_Process_Data.MouseMove, AddressOf ListView_MouseMove
		AddHandler Lv_Process_User_Approve.MouseMove, AddressOf ListView_MouseMove
		AddHandler Lv_Process_Detail_Barang.MouseMove, AddressOf ListView_MouseMove
		AddHandler Lv_Product_Data.MouseMove, AddressOf ListView_MouseMove
		AddHandler Lv_Product_User_Approve.MouseMove, AddressOf ListView_MouseMove
		AddHandler Lv_Product_Detail_Barang.MouseMove, AddressOf ListView_MouseMove

		AddHandler Lv_Process_Data.MouseLeave, AddressOf ListView_MouseLeave
		AddHandler Lv_Process_User_Approve.MouseLeave, AddressOf ListView_MouseLeave
		AddHandler Lv_Process_Detail_Barang.MouseLeave, AddressOf ListView_MouseLeave
		AddHandler Lv_Product_Data.MouseLeave, AddressOf ListView_MouseLeave
		AddHandler Lv_Product_User_Approve.MouseLeave, AddressOf ListView_MouseLeave
		AddHandler Lv_Product_Detail_Barang.MouseLeave, AddressOf ListView_MouseLeave

#End Region

#Region "WASTE PROCESS"

		Lv_Process_Data.Columns.Clear()
		Lv_Process_Data.Columns.Add("No Approval", 130, HorizontalAlignment.Left)
		Lv_Process_Data.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
		Lv_Process_Data.Columns.Add("Lokasi", 130, HorizontalAlignment.Center)
		Lv_Process_Data.Columns.Add("Kode Stock Owner", 150, HorizontalAlignment.Left)
		Lv_Process_Data.Columns.Add("Tanggal", 130, HorizontalAlignment.Center)
		Lv_Process_Data.Columns.Add("Jam", 110, HorizontalAlignment.Center)
		Lv_Process_Data.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
		Lv_Process_Data.Columns.Add("User Input", 130, HorizontalAlignment.Left)
		Lv_Process_Data.View = View.Details

		Lv_Process_User_Approve.Columns.Clear()
		Lv_Process_User_Approve.Columns.Add("Username", 200, HorizontalAlignment.Left)
		Lv_Process_User_Approve.Columns.Add("Approval Level", 100, HorizontalAlignment.Center)
		Lv_Process_User_Approve.Columns.Add("Status", 137, HorizontalAlignment.Center)
		Lv_Process_User_Approve.Columns.Add("Tanggal Approve", 110, HorizontalAlignment.Center)
		Lv_Process_User_Approve.Columns.Add("Jam Approve", 100, HorizontalAlignment.Center)
		Lv_Process_User_Approve.Columns.Add("id_user", 0, HorizontalAlignment.Left)
		Lv_Process_User_Approve.Columns.Add("Jabatan", 150, HorizontalAlignment.Left)
		Lv_Process_User_Approve.Columns.Add("Keterangan Approval", 250, HorizontalAlignment.Left)
		Lv_Process_User_Approve.View = View.Details

		Lv_Process_User_Approve.Columns(6).DisplayIndex = 3

		Lv_Process_Detail_Barang.Columns.Clear()
		Lv_Process_Detail_Barang.Columns.Add("Kode Stock Owner", 130, HorizontalAlignment.Left)
		Lv_Process_Detail_Barang.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left)
		Lv_Process_Detail_Barang.Columns.Add("Nama Barang", 190, HorizontalAlignment.Left)
		Lv_Process_Detail_Barang.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
		Lv_Process_Detail_Barang.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
		Lv_Process_Detail_Barang.View = View.Details

		Lv_Process_Detail_Barcode.Columns.Clear()
		Lv_Process_Detail_Barcode.Columns.Add("", 0, HorizontalAlignment.Left)
		Lv_Process_Detail_Barcode.Columns.Add("Jenis", 130, HorizontalAlignment.Left)
		Lv_Process_Detail_Barcode.Columns.Add("No Transaksi", 130, HorizontalAlignment.Left)
		Lv_Process_Detail_Barcode.Columns.Add("Barcode Awal", 180, HorizontalAlignment.Left)
		Lv_Process_Detail_Barcode.Columns.Add("Barcode Tujuan", 180, HorizontalAlignment.Left)
		Lv_Process_Detail_Barcode.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
		Lv_Process_Detail_Barcode.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
		Lv_Process_Detail_Barcode.Columns.Add("Status", 100, HorizontalAlignment.Center).DisplayIndex = 2
		Lv_Process_Detail_Barcode.Columns.Add("No Split", 130, HorizontalAlignment.Left).DisplayIndex = 3
		Lv_Process_Detail_Barcode.View = View.Details

#End Region

#Region "WASTE PRODUCT"

		Lv_Product_Data.Columns.Clear()
		Lv_Product_Data.Columns.Add("No Approval", 130, HorizontalAlignment.Left)
		Lv_Product_Data.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
		Lv_Product_Data.Columns.Add("Lokasi", 130, HorizontalAlignment.Center)
		Lv_Product_Data.Columns.Add("Kode Stock Owner", 150, HorizontalAlignment.Left)
		Lv_Product_Data.Columns.Add("Tanggal", 130, HorizontalAlignment.Center)
		Lv_Product_Data.Columns.Add("Jam", 110, HorizontalAlignment.Center)
		Lv_Product_Data.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
		Lv_Product_Data.Columns.Add("User Input", 130, HorizontalAlignment.Left)
		Lv_Product_Data.View = View.Details

		Lv_Product_User_Approve.Columns.Clear()
		Lv_Product_User_Approve.Columns.Add("Username", 200, HorizontalAlignment.Left)
		Lv_Product_User_Approve.Columns.Add("Approval Level", 100, HorizontalAlignment.Center)
		Lv_Product_User_Approve.Columns.Add("Status", 137, HorizontalAlignment.Center)
		Lv_Product_User_Approve.Columns.Add("Tanggal Approve", 110, HorizontalAlignment.Center)
		Lv_Product_User_Approve.Columns.Add("Jam Approve", 100, HorizontalAlignment.Center)
		Lv_Product_User_Approve.Columns.Add("id_user", 0, HorizontalAlignment.Left)
		Lv_Product_User_Approve.Columns.Add("Jabatan", 150, HorizontalAlignment.Left)
		Lv_Product_User_Approve.Columns.Add("Keterangan Approval", 250, HorizontalAlignment.Left)
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

#End Region

		Try
			OpenConn()

			Cmb_Lokasi.Items.Clear() : Cmb_Lokasi2.Items.Clear() : arrFilterLokasiGudang.Clear()
			SQL = "select Kode_Stock_Owner from stock_owner where Kode_Perusahaan = '" & KodePerusahaan & "' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Lokasi.Items.Add(Dr("Kode_Stock_Owner"))
					Cmb_Lokasi2.Items.Add(Dr("Kode_Stock_Owner"))
					arrFilterLokasiGudang.Add(Dr("Kode_Stock_Owner"))
				Loop
			End Using

			Cmb_Tanggal.Items.Clear() : arrFilterTanggal.Clear()
			Cmb_Tanggal.Items.Add("Tanggal Transaksi") : arrFilterTanggal.Add("a.Tanggal")

			Cmb_Param_Lain.Items.Clear() : arrFilterParamLain.Clear()
			Cmb_Param_Lain.Items.Add("No Approval") : arrFilterParamLain.Add("b.No_Transaksi")
			Cmb_Param_Lain.Items.Add("No Faktur") : arrFilterParamLain.Add("a.No_Faktur")
			Cmb_Param_Lain.Items.Add("Lokasi") : arrFilterParamLain.Add("a.Lokasi")
			Cmb_Param_Lain.Items.Add("Kode Stock Owner") : arrFilterParamLain.Add("a.Kode_Stock_Owner")
			Cmb_Param_Lain.Items.Add("User Input") : arrFilterParamLain.Add("a.UserID")

			Cmb_Tanggal2.Items.Clear() : arrFilterTanggal_2.Clear()
			Cmb_Tanggal2.Items.Add("Tanggal Transaksi") : arrFilterTanggal_2.Add("a.Tanggal")

			Cmb_Param_Lain2.Items.Clear() : arrFilterParamLain_2.Clear()
			Cmb_Param_Lain2.Items.Add("No Approval") : arrFilterParamLain_2.Add("b.No_Transaksi")
			Cmb_Param_Lain2.Items.Add("No Faktur") : arrFilterParamLain_2.Add("a.No_Faktur")
			Cmb_Param_Lain2.Items.Add("Lokasi") : arrFilterParamLain_2.Add("a.Lokasi")
			Cmb_Param_Lain2.Items.Add("Kode Stock Owner") : arrFilterParamLain_2.Add("a.Kode_Stock_Owner")
			Cmb_Param_Lain2.Items.Add("User Input") : arrFilterParamLain_2.Add("a.UserID")

			Cmb2_Gudang.Items.Clear() : Cmb_Gudang.Items.Clear()
			SQL = $"
				select a.Kode_Stock_Owner, a.Jenis
				from N_EMI_Role_Akses_Gudang_Waste a
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.UserID = '{UserID}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						If Dr("Jenis").ToString.Trim = "Waste_Produk" Then
							Cmb2_Gudang.Items.Add(Dr("Kode_Stock_Owner"))
						ElseIf Dr("Jenis").ToString.Trim = "Waste_Process" Then
							Cmb_Gudang.Items.Add(Dr("Kode_Stock_Owner"))
						End If
					Loop While Dr.Read
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'Kosong_Tab_1()
		Kosong_Tab_2()

	End Sub

	Private Sub Tab1_Get_Lv_Process_Data(ByVal index As Integer)
		Lv_Process_NoTransaksiApproval = Lv_Process_Data.Items(index).SubItems(Item_Process_NoTransaksiApproval).Text
		Lv_Process_NoFaktur = Lv_Process_Data.Items(index).SubItems(Item_Process_NoFaktur).Text
		Lv_Process_KdStock_Owner = Lv_Process_Data.Items(index).SubItems(Item_Process__KdStock_Owner).Text
		Lv_Process_Lokasi = Lv_Process_Data.Items(index).SubItems(Item_Process_Lokasi).Text
		Lv_Process_Tanggal = Lv_Process_Data.Items(index).SubItems(Item_Process_Tanggal).Text
		Lv_Process_Jam = Lv_Process_Data.Items(index).SubItems(Item_Process_Jam).Text
		Lv_Process_Keterangan = Lv_Process_Data.Items(index).SubItems(Item_Process_Keterangan).Text
		Lv_Process_UserInput = Lv_Process_Data.Items(index).SubItems(Item_Process_UserInput).Text
	End Sub

	Private Sub Tab1_Get_Lv_Process_User(ByVal index As Integer)
		Lv_User_Approve_Username = Lv_Process_User_Approve.Items(index).SubItems(item_User_Approve_Username).Text
		Lv_User_Approve_Level = Lv_Process_User_Approve.Items(index).SubItems(item_User_Approve_Level).Text
		Lv_User_Approve_Status = Lv_Process_User_Approve.Items(index).SubItems(item_User_Approve_Status).Text
		Lv_User_Approve_TanggalApprove = Lv_Process_User_Approve.Items(index).SubItems(item_User_Approve_TanggalApprove).Text
		Lv_User_Approve_JamApprove = Lv_Process_User_Approve.Items(index).SubItems(item_User_Approve_JamApprove).Text
		Lv_User_Approve_iduser = Lv_Process_User_Approve.Items(index).SubItems(item_User_Approve_iduser).Text
	End Sub

	Private Sub Tab2_Get_Lv_Product_Data(ByVal index As Integer)
		Lv_Product_NoTransaksiApproval = Lv_Product_Data.Items(index).SubItems(Item_Product_NoTransaksiApproval).Text
		Lv_Product_NoFaktur = Lv_Product_Data.Items(index).SubItems(Item_Product_NoFaktur).Text
		Lv_Product_KdStock_Owner = Lv_Product_Data.Items(index).SubItems(Item_Product__KdStock_Owner).Text
		Lv_Product_Lokasi = Lv_Product_Data.Items(index).SubItems(Item_Product_Lokasi).Text
		Lv_Product_Tanggal = Lv_Product_Data.Items(index).SubItems(Item_Product_Tanggal).Text
		Lv_Product_Jam = Lv_Product_Data.Items(index).SubItems(Item_Product_Jam).Text
		Lv_Product_Keterangan = Lv_Product_Data.Items(index).SubItems(Item_Product_Keterangan).Text
		Lv_Product_UserInput = Lv_Product_Data.Items(index).SubItems(Item_Product_UserInput).Text
	End Sub

	Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
		If TabControl1.SelectedIndex = 0 Then
			Kosong_Tab_2()

		ElseIf TabControl1.SelectedIndex = 1 Then
			Kosong_Tab_1()
		Else
			Kosong_Tab_1()
			Kosong_Tab_2()
		End If
	End Sub

	Private Sub Kosong_Tab_1()

		Lv_Process_Data.Items.Clear()
		Lv_Process_User_Approve.Items.Clear()
		Lv_Process_Detail_Barang.Items.Clear()

		Cmb_Lokasi.SelectedItem = Ket_Lokasi_HO

		If Cmb_Gudang.Items.Count > 0 Then
			Cmb_Gudang.SelectedIndex = 0
		Else
			Cmb_Gudang.SelectedIndex = -1
		End If

		'Load_Data_Process_Waste()
		Cb_Hari_Ini.Checked = True
		Rd_Cetak_Semua.Checked = True
		Btn_Cari.PerformClick()

	End Sub

	Private Sub Kosong_Tab_2()

		Lv_Product_Data.Items.Clear()
		Lv_Product_User_Approve.Items.Clear()
		Lv_Product_Detail_Barang.Items.Clear()

		Cmb_Lokasi2.SelectedItem = Ket_Lokasi_HO

		If Cmb2_Gudang.Items.Count > 0 Then
			Cmb2_Gudang.SelectedIndex = 0
		Else
			Cmb2_Gudang.SelectedIndex = -1
		End If

		'Load_Data_Product_Waste()
		Cb_Hari_Ini2.Checked = True
		Rd_Cetak_Semua2.Checked = True
		Btn_Cari2.PerformClick()

	End Sub

	Private Sub Load_Data_Process_Waste()
		Try
			OpenConn()

			Lv_Process_Data.Items.Clear() : Lv_Process_User_Approve.Items.Clear() : Lv_Process_Detail_Barang.Items.Clear() : Lv_Process_Detail_Barcode.Items.Clear()
			SQL = "select distinct b.No_Transaksi, a.No_Faktur, a.Lokasi, a.Kode_Stock_Owner, a.Tanggal, a.Jam, a.Keterangan, a.UserID as User_Input, "
			SQL = SQL & "isnull((x.isCompleted), 'Y') as isCompleted, a.Flag_Cetak_Faktur, isnull(y.Flag, 'Y') as isAllRejected "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
			SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
			SQL = SQL & "outer apply( "
			SQL = SQL & "select top 1 'T' as isCompleted "
			SQL = SQL & "from N_EMI_Transaksi_Approval_Waste z "
			SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan "
			SQL = SQL & "and z.No_Faktur_Waste = a.No_Faktur "
			SQL = SQL & "and z.Status is null "
			SQL = SQL & "and z.Flag_Approve is null) x "
			SQL = SQL & "LEFT JOIN ( "
			SQL = SQL & "select distinct z.Kode_Perusahaan, z.No_Faktur, 'T' as Flag "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Det z "
			SQL = SQL & ") y on a.Kode_Perusahaan = y.Kode_Perusahaan and a.No_Faktur = y.No_Faktur "
			SQL = SQL & "where a.Status is null and b.Status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Kode_Stock_Owner = '" & Cmb_Gudang.Text.Trim & "' "
			SQL = SQL & "and a.Flag_Waste_Proses = 'Y' "

			If Cmb_Lokasi.SelectedIndex <> 0 Then
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
				SQL &= $" a.Lokasi = '{arrFilterLokasiGudang.Item(Cmb_Lokasi.SelectedIndex)}' "
			End If

			If Cb_Hari_Ini.Checked Then
				'Pasang And
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

				SQL &= $" a.tanggal between '"
				SQL &= Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
			End If

			If Cb_Tanggal.Checked Then
				'Pasang And
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

				SQL &= arrFilterTanggal.Item(Cmb_Tanggal.SelectedIndex) & " between ' "
				SQL &= Format(Tgl_1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl_2.Value, "yyyy-MM-dd") & "' "
			End If

			If Cb_Param_Lain.Checked Then
				'Pasang And
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

				SQL &= arrFilterParamLain.Item(Cmb_Param_Lain.SelectedIndex) & " like '%" & Trim(Txt_Param_Lain.Text) & "%' "
			End If

			If Not Rd_Cetak_Semua.Checked Then
				If Rd_Cetak_Belum.Checked Then
					SQL = SQL & "and a.Flag_Cetak_Faktur is null "
				ElseIf Rd_Cetak_Sudah.Checked Then
					SQL = SQL & "and a.Flag_Cetak_Faktur = 'Y' "
				End If
			End If

			SQL = SQL & "order by a.Tanggal, a.Jam "

			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Process_Data.Items.Add(Dr("No_Transaksi"))
					Lv.SubItems.Add(Dr("No_Faktur"))
					Lv.SubItems.Add(Dr("Lokasi"))
					Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
					Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
					Lv.SubItems.Add(Dr("Jam"))
					Lv.SubItems.Add(Dr("Keterangan"))
					Lv.SubItems.Add(Dr("User_Input"))

					If Dr("isCompleted").ToString.Trim = "T" Then
						Lv.BackColor = Color.White
					ElseIf Dr("isCompleted").ToString.Trim = "Y" Then
						Lv.BackColor = Color.LightGreen
					Else
						Lv.BackColor = Color.White
					End If

					If Dr("isCompleted").ToString.Trim = "Y" And General_Class.CekNULL(Dr("Flag_Cetak_Faktur")).Trim = "Y" Then
						Lv.BackColor = Color.Goldenrod
					End If

					If Dr("isCompleted").ToString.Trim = "Y" And General_Class.CekNULL(Dr("isAllRejected")).Trim = "Y" Then
						Lv.BackColor = Color.DarkRed
						Lv.ForeColor = Color.White

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

	Private Sub Lv_Process_Data_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Process_Data.SelectedIndexChanged
		If Lv_Process_Data.Items.Count = 0 OrElse Lv_Process_Data.FocusedItem Is Nothing Then Exit Sub

		Try
			OpenConn()
			Tab1_Get_Lv_Process_Data(Lv_Process_Data.FocusedItem.Index)

			Dim No_faktur As String = Lv_Process_NoFaktur
			Dim No_Transaksi As String = Lv_Process_NoTransaksiApproval

			Lv_Process_User_Approve.Items.Clear()
			SQL = "select case when b.Approval_Level<>'1' then c.username else d.UserName end as username, "
			SQL = SQL & "b.Approval_Level, b.Flag_Approve, b.Tanggal_Approve, b.Jam_Approve, b.Id_User_Android_Approve, b.jabatan, "
			SQL = SQL & "b.Keterangan as Keterangan_Android "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
			SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
			SQL = SQL & "left join Emi_Users c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_User_Android_Approve = c.id "
			SQL = SQL & "left join users d on b.Kode_Perusahaan = d.Kode_Perusahaan and b.User_ID_Desktop = d.UserID "
			SQL = SQL & "where a.Status is null and b.Status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Flag_Waste_Proses = 'Y' "
			SQL = SQL & "and a.No_Faktur = '" & No_faktur & "' "
			SQL = SQL & "and b.No_Transaksi = '" & No_Transaksi & "' "
			SQL = SQL & "order by b.Approval_Level ASC "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Process_User_Approve.Items.Add(Dr("username"))
					Lv.SubItems.Add(Dr("Approval_Level"))

					If General_Class.CekNULL(Dr("Flag_Approve")).Trim = "Y" Then
						Lv.SubItems.Add("Approved")
						Lv.BackColor = Color.LightGreen
					ElseIf General_Class.CekNULL(Dr("Flag_Approve")).Trim = "T" Then
						Lv.SubItems.Add("Rejected")
						Lv.ForeColor = Color.White
						Lv.BackColor = Color.DarkRed
					Else
						Lv.SubItems.Add("On Process")
					End If

					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Tanggal_Approve")).Trim = "", "-", Format(Dr("Tanggal_Approve"), "dd MMM yyyy")))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jam_Approve")).Trim = "", "-", Dr("Jam_Approve")))
					Lv.SubItems.Add(Dr("Id_User_Android_Approve"))
					Lv.SubItems.Add(Dr("jabatan"))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Keterangan_Android")).Trim = "", "-", Dr("Keterangan_Android")))
				Loop
			End Using

			Lv_Process_Detail_Barang.Items.Clear()
			SQL = "select a.Kode_Stock_Owner as Lokasi, b.kode_barang, c.Nama as Nama_Barang, b.Total, b.Satuan "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
			SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
			SQL = SQL & "inner join barang c on a.kode_perusahaan = c.kode_perusahaan and a.kode_stock_owner = c.kode_Stock_owner and b.kode_barang = c.kode_barang "
			SQL = SQL & "where a.Status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Flag_Waste_Proses = 'Y' "
			SQL = SQL & "and a.No_Faktur = '" & No_faktur & "' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Process_Detail_Barang.Items.Add(Dr("Lokasi"))
					Lv.SubItems.Add(Dr("kode_barang"))
					Lv.SubItems.Add(Dr("Nama_Barang"))
					Lv.SubItems.Add(Format(Dr("Total"), "N4"))
					Lv.SubItems.Add(Dr("Satuan"))
				Loop
			End Using

			Lv_Process_Detail_Barcode.Items.Clear()

#Region "Kode Lama 23 Mei 2026"

			'SQL = "with Cte as ( "
			'SQL = SQL & "select a.kode_perusahaan, 'Good Receiver 1' as asal, a.No_Faktur, f.No_Transaksi, "
			'SQL = SQL & "(g.qr_code +'-'+g.kode_unik_berjalan) as Barcode_Awal, "
			'SQL = SQL & "(h.qr_code +'-'+h.kode_unik_berjalan) as Barcode_Akhir, "
			'SQL = SQL & "c.Jumlah, b.Satuan, isnull(i.No_Transaksi, NULL) as No_Transaksi_Waste "
			'SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
			'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
			'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Det c on b.Kode_Perusahaan = c.kode_perusahaan and b.No_Faktur = c.no_faktur and b.Urut_Oto = c.urut_tf "
			'SQL = SQL & "left join N_EMI_Transaksi_Transfer_Waste_Det2 d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
			'SQL = SQL & "inner join Emi_Production_Results_Detail_Scrap e on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Serial_Number_Awal = e.Serial_Number "
			'SQL = SQL & "inner join Emi_Production_Results f on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Transaksi = f.No_Transaksi and f.Status is null "
			'SQL = SQL & "inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and c.Serial_Number_Awal = g.Serial_Number "
			'SQL = SQL & "left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number "
			'SQL = SQL & "left join ( "
			'SQL = SQL & "select z.kode_perusahaan, z.No_Transaksi, z.No_Split, z.Proses "
			'SQL = SQL & "from N_EMI_Transaksi_Waste_Sampel_GR_1 z "
			''SQL = SQL & "inner join N_EMI_Transaksi_Waste_Sampel_GR_1_Detail x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Transaksi = x.No_Transaksi "
			'SQL = SQL & "where z.Status is null "
			'SQL = SQL & ") i on f.Kode_Perusahaan = i.kode_perusahaan and f.No_Production_Order = i.No_Split and i.Proses = e.Proses "

			'SQL = SQL & "union all "

			'SQL = SQL & "select a.kode_perusahaan, 'Good Receiver 2' as asal, a.No_Faktur, f.No_Transaksi, "
			'SQL = SQL & "(g.qr_code +'-'+g.kode_unik_berjalan) as Barcode_Awal, "
			'SQL = SQL & "(h.qr_code +'-'+h.kode_unik_berjalan) as Barcode_Akhir, "
			'SQL = SQL & "e.Jumlah, e.Satuan, NULL as No_Transaksi_Waste "
			'SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
			'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
			'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Det c on b.Kode_Perusahaan = c.kode_perusahaan and b.No_Faktur = c.no_faktur and b.Urut_Oto = c.urut_tf "
			'SQL = SQL & "left join N_EMI_Transaksi_Transfer_Waste_Det2 d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
			'SQL = SQL & "inner join Emi_Production_Results_Validation_Detail e on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Serial_Number_Awal = e.Serial_Number_Tujuan "
			'SQL = SQL & "inner join Emi_Production_Results_Validation f on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Transaksi = f.No_Transaksi and f.Status is null "
			'SQL = SQL & "inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and c.Serial_Number_Awal = g.Serial_Number "
			'SQL = SQL & "left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number "

			'SQL = SQL & "union all "

			'SQL = SQL & "select a.kode_perusahaan, 'Pemindahan Waste' as asal, a.No_Faktur, e.No_Faktur as No_Transaksi, "
			'SQL = SQL & "(g.qr_code +'-'+g.kode_unik_berjalan) as Barcode_Awal, "
			'SQL = SQL & "(h.qr_code +'-'+h.kode_unik_berjalan) as Barcode_Akhir, "
			'SQL = SQL & "j.Jumlah, b.Satuan, NULL as No_Transaksi_Waste "
			'SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
			'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
			'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Det c on b.Kode_Perusahaan = c.kode_perusahaan and b.No_Faktur = c.no_faktur and b.Urut_Oto = c.urut_tf "
			'SQL = SQL & "left join N_EMI_Transaksi_Transfer_Waste_Det2 d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
			'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk e on a.Kode_Perusahaan = e.Kode_Perusahaan and c.No_Faktur_Produk = e.No_Faktur "
			'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det2 j on e.Kode_Perusahaan = j.Kode_Perusahaan and e.No_Faktur = j.No_Faktur and c.Serial_Number_Awal = j.Serial_Number "
			'SQL = SQL & "inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and j.Serial_Number = g.Serial_Number "
			'SQL = SQL & "left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number ) "

			'SQL = SQL & "select asal, No_Faktur, No_Transaksi, Barcode_Awal, Barcode_Akhir, Jumlah, Satuan, No_Transaksi_Waste "
			'SQL = SQL & "from cte  "
			'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
			'SQL = SQL & "and No_Faktur = '" & No_faktur & "' "
			'SQL = SQL & "order by No_Faktur"

#End Region

			SQL = $"
				select a.kode_perusahaan, 'Goods Received 1' as asal, 'Approved' as StatusBarcode, a.No_Faktur, f.No_Transaksi, f.No_Production_Order as No_Split,
								   (g.qr_code + '-' + g.kode_unik_berjalan) as Barcode_Awal,
								   (h.qr_code + '-' + h.kode_unik_berjalan) as Barcode_Akhir, sum(c.Jumlah) as Jumlah, b.Satuan,
								   isnull(i.No_Transaksi, NULL) as No_Transaksi_Waste
				from N_EMI_Transaksi_Transfer_Waste a
								 inner join N_EMI_Transaksi_Transfer_Waste_Detail b
											on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
								 inner join N_EMI_Transaksi_Transfer_Waste_Det c
											on b.Kode_Perusahaan = c.kode_perusahaan and b.No_Faktur = c.no_faktur and b.Urut_Oto = c.urut_tf
								 left join N_EMI_Transaksi_Transfer_Waste_Det2 d
										   on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det
								 inner join Emi_Production_Results_Detail_Scrap e
											on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Serial_Number_Awal = e.Serial_Number
								 inner join Emi_Production_Results f
											on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Transaksi = f.No_Transaksi and f.Status is null
								 inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and c.Serial_Number_Awal = g.Serial_Number
								 left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number
								 left join (select z.kode_perusahaan, z.No_Transaksi, z.No_Split, z.Proses
											from N_EMI_Transaksi_Waste_Sampel_GR_1 z
											where z.Status is null) i
										   on f.Kode_Perusahaan = i.kode_perusahaan and f.No_Production_Order = i.No_Split and i.Proses = e.Proses
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				  and a.No_Faktur = '{No_faktur}'
				group by a.kode_perusahaan, a.No_Faktur, f.No_Transaksi, f.No_Production_Order,
								   (g.qr_code + '-' + g.kode_unik_berjalan),
								   (h.qr_code + '-' + h.kode_unik_berjalan), b.Satuan,
								   isnull(i.No_Transaksi, NULL)

				union all

				select a.kode_perusahaan, 'Goods Received 1' as asal, 'Rejected' as StatusBarcode, a.No_Faktur, f.No_Transaksi, f.No_Production_Order as No_Split,
								   (g.qr_code + '-' + g.kode_unik_berjalan) as Barcode_Awal,
								   (h.qr_code + '-' + h.kode_unik_berjalan) as Barcode_Akhir, sum(c.Jumlah) as Jumlah, b.Satuan,
								   isnull(i.No_Transaksi, NULL) as No_Transaksi_Waste
				from N_EMI_Transaksi_Transfer_Waste a
								 inner join N_EMI_Transaksi_Transfer_Waste_Detail b
											on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
								 inner join N_EMI_Transaksi_Transfer_Waste_Det_Log_Approval c
											on b.Kode_Perusahaan = c.kode_perusahaan and b.No_Faktur = c.no_faktur and b.Urut_Oto = c.urut_tf and
											   c.Status is null
								 left join N_EMI_Transaksi_Transfer_Waste_Det2 d
										   on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det
								 inner join Emi_Production_Results_Detail_Scrap e
											on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Serial_Number_Awal = e.Serial_Number
								 inner join Emi_Production_Results f
											on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Transaksi = f.No_Transaksi and f.Status is null
								 inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and c.Serial_Number_Awal = g.Serial_Number
								 left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number
								 left join (select z.kode_perusahaan, z.No_Transaksi, z.No_Split, z.Proses
											from N_EMI_Transaksi_Waste_Sampel_GR_1 z
											where z.Status is null) i
										   on f.Kode_Perusahaan = i.kode_perusahaan and f.No_Production_Order = i.No_Split and i.Proses = e.Proses
								 left join N_EMI_Transaksi_Transfer_Waste_Det j
										   on b.Kode_Perusahaan = j.kode_perusahaan and b.No_Faktur = j.no_faktur and b.Urut_Oto = j.urut_tf and
											  c.Serial_Number_Awal = j.Serial_Number_Awal
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				  and a.No_Faktur = '{No_faktur}'
				  and j.Kode_Perusahaan is null
				group by a.kode_perusahaan, a.No_Faktur, f.No_Transaksi, f.No_Production_Order,
								   (g.qr_code + '-' + g.kode_unik_berjalan),
								   (h.qr_code + '-' + h.kode_unik_berjalan), b.Satuan,
								   isnull(i.No_Transaksi, NULL)

				union all

				select a.kode_perusahaan, 'Goods Received 2' as asal, 'Approved' as StatusBarcode, a.No_Faktur, f.No_Transaksi, f.No_Production_Order as No_Split,
								   (g.qr_code + '-' + g.kode_unik_berjalan) as Barcode_Awal,
								   (h.qr_code + '-' + h.kode_unik_berjalan) as Barcode_Akhir, sum(e.Jumlah) as Jumlah, e.Satuan, NULL as No_Transaksi_Waste
				from N_EMI_Transaksi_Transfer_Waste a
								 inner join N_EMI_Transaksi_Transfer_Waste_Detail b
											on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
								 inner join N_EMI_Transaksi_Transfer_Waste_Det c
											on b.Kode_Perusahaan = c.kode_perusahaan and b.No_Faktur = c.no_faktur and b.Urut_Oto = c.urut_tf
								 left join N_EMI_Transaksi_Transfer_Waste_Det2 d
										   on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det
								 inner join Emi_Production_Results_Validation_Detail e
											on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Serial_Number_Awal = e.Serial_Number_Tujuan
								 inner join Emi_Production_Results_Validation f
											on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Transaksi = f.No_Transaksi and f.Status is null
								 inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and c.Serial_Number_Awal = g.Serial_Number
								 left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				  and a.No_Faktur = '{No_faktur}'
				group by a.kode_perusahaan, a.No_Faktur, f.No_Transaksi, f.No_Production_Order,
								   (g.qr_code + '-' + g.kode_unik_berjalan),
								   (h.qr_code + '-' + h.kode_unik_berjalan), e.Satuan

				union all

				select a.kode_perusahaan, 'Goods Received 2' as asal, 'Rejected' as StatusBarcode, a.No_Faktur, f.No_Transaksi, f.No_Production_Order as No_Split,
								   (g.qr_code + '-' + g.kode_unik_berjalan) as Barcode_Awal,
								   (h.qr_code + '-' + h.kode_unik_berjalan) as Barcode_Akhir, sum(e.Jumlah) as Jumlah, e.Satuan, NULL as No_Transaksi_Waste
				from N_EMI_Transaksi_Transfer_Waste a
								 inner join N_EMI_Transaksi_Transfer_Waste_Detail b
											on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
								 inner join N_EMI_Transaksi_Transfer_Waste_Det_Log_Approval c
											on b.Kode_Perusahaan = c.kode_perusahaan and b.No_Faktur = c.no_faktur and b.Urut_Oto = c.urut_tf and
											   c.Status is null
								 left join N_EMI_Transaksi_Transfer_Waste_Det2 d
										   on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det
								 inner join Emi_Production_Results_Validation_Detail e
											on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Serial_Number_Awal = e.Serial_Number_Tujuan
								 inner join Emi_Production_Results_Validation f
											on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Transaksi = f.No_Transaksi and f.Status is null
								 inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and c.Serial_Number_Awal = g.Serial_Number
								 left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number
								 left join N_EMI_Transaksi_Transfer_Waste_Det i
										   on b.Kode_Perusahaan = i.kode_perusahaan and b.No_Faktur = i.no_faktur and b.Urut_Oto = i.urut_tf and
											  c.Serial_Number_Awal = i.Serial_Number_Awal
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				  and a.No_Faktur = '{No_faktur}'
				  and i.Kode_Perusahaan is null
				group by a.kode_perusahaan, a.No_Faktur, f.No_Transaksi, f.No_Production_Order,
								   (g.qr_code + '-' + g.kode_unik_berjalan),
								   (h.qr_code + '-' + h.kode_unik_berjalan), e.Satuan

				union all

				select a.kode_perusahaan, 'Pemindahan Waste' as asal, 'Approved' as StatusBarcode, a.No_Faktur, '-' as No_Split,
								   e.No_Faktur as No_Transaksi, (g.qr_code + '-' + g.kode_unik_berjalan) as Barcode_Awal,
								   (h.qr_code + '-' + h.kode_unik_berjalan) as Barcode_Akhir, sum(j.Jumlah) as Jumlah, b.Satuan, NULL as No_Transaksi_Waste
				from N_EMI_Transaksi_Transfer_Waste a
								 inner join N_EMI_Transaksi_Transfer_Waste_Detail b
											on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
								 inner join N_EMI_Transaksi_Transfer_Waste_Det c
											on b.Kode_Perusahaan = c.kode_perusahaan and b.No_Faktur = c.no_faktur and b.Urut_Oto = c.urut_tf
								 left join N_EMI_Transaksi_Transfer_Waste_Det2 d
										   on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det
								 inner join N_EMI_Transaksi_Transfer_Waste_Produk e
											on a.Kode_Perusahaan = e.Kode_Perusahaan and c.No_Faktur_Produk = e.No_Faktur
								 inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det2 j
											on e.Kode_Perusahaan = j.Kode_Perusahaan and e.No_Faktur = j.No_Faktur and
											   c.Serial_Number_Awal = j.Serial_Number
								 inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and j.Serial_Number = g.Serial_Number
								 left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				  and a.No_Faktur = '{No_faktur}'
				group by a.kode_perusahaan, a.No_Faktur,
								   e.No_Faktur, (g.qr_code + '-' + g.kode_unik_berjalan),
								   (h.qr_code + '-' + h.kode_unik_berjalan), b.Satuan

				union all

				select a.kode_perusahaan, 'Pemindahan Waste' as asal, 'Rejected' as StatusBarcode, a.No_Faktur, '-' as No_Split,
								   e.No_Faktur as No_Transaksi, (g.qr_code + '-' + g.kode_unik_berjalan) as Barcode_Awal,
								   (h.qr_code + '-' + h.kode_unik_berjalan) as Barcode_Akhir, sum(j.Jumlah) as Jumlah, b.Satuan, NULL as No_Transaksi_Waste
				from N_EMI_Transaksi_Transfer_Waste a
								 inner join N_EMI_Transaksi_Transfer_Waste_Detail b
											on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
								 inner join N_EMI_Transaksi_Transfer_Waste_Det_Log_Approval c
											on b.Kode_Perusahaan = c.kode_perusahaan and b.No_Faktur = c.no_faktur and b.Urut_Oto = c.urut_tf and
											   c.Status is null
								 left join N_EMI_Transaksi_Transfer_Waste_Det2 d
										   on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det
								 inner join N_EMI_Transaksi_Transfer_Waste_Produk e
											on a.Kode_Perusahaan = e.Kode_Perusahaan and c.No_Faktur_Produk = e.No_Faktur
								 inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det2 j
											on e.Kode_Perusahaan = j.Kode_Perusahaan and e.No_Faktur = j.No_Faktur and
											   c.Serial_Number_Awal = j.Serial_Number
								 inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and j.Serial_Number = g.Serial_Number
								 left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number
								 left join N_EMI_Transaksi_Transfer_Waste_Det i
										   on b.Kode_Perusahaan = i.kode_perusahaan and b.No_Faktur = i.no_faktur and b.Urut_Oto = i.urut_tf and
											  c.Serial_Number_Awal = i.Serial_Number_Awal
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				  and a.No_Faktur = '{No_faktur}'
				  and i.Kode_Perusahaan is null
				group by a.kode_perusahaan, a.No_Faktur,
								   e.No_Faktur, (g.qr_code + '-' + g.kode_unik_berjalan),
								   (h.qr_code + '-' + h.kode_unik_berjalan), b.Satuan
			"

			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Process_Detail_Barcode.Items.Add("")

					If General_Class.CekNULL(Dr("No_Transaksi_Waste")).Trim = "" Then
						Lv.SubItems.Add(Dr("asal"))
					Else
						Lv.SubItems.Add("Waste Sampel")
					End If

					Lv.SubItems.Add(Dr("No_Transaksi"))
					Lv.SubItems.Add(Dr("Barcode_Awal"))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Barcode_Akhir")).Trim = "", "-", Dr("Barcode_Akhir")))
					Lv.SubItems.Add(Format(Dr("Jumlah"), "N4"))
					Lv.SubItems.Add(Dr("Satuan"))
					Lv.SubItems.Add(Dr("StatusBarcode"))
					Lv.SubItems.Add(Dr("No_Split"))

					If General_Class.CekNULL(Dr("StatusBarcode")) = "Rejected" Then
						Lv.BackColor = Color.DarkRed
						Lv.ForeColor = Color.White
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

	Private Sub CetakFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakFakturToolStripMenuItem.Click
		If Lv_Process_Data.Items.Count = 0 AndAlso Lv_Process_Data.FocusedItem Is Nothing Then Exit Sub

		Tab1_Get_Lv_Process_Data(Lv_Process_Data.FocusedItem.Index)
		Dim No_Approval As String = Lv_Process_NoTransaksiApproval
		Dim No_Faktur As String = Lv_Process_NoFaktur

		Try
			OpenConn()

			'===========================
			'=     CEK BUTTON ROLE     =
			'===========================
			If CekButtonRole("Cetak_Faktur_Waste_Process") = "T" Then
				CloseConn()
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Cetak Faktur Waste Process", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			'===============================
			'=     CEK TRANSAKSI WASTE     =
			'===============================
			SQL = "select top 1 a.Kode_Perusahaan "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
			SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
			SQL = SQL & "where a.Status is null and b.status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Flag_Waste_Proses = 'Y' "
			SQL = SQL & "and a.No_Faktur = '" & No_Faktur & "' "
			SQL = SQL & "and b.no_transaksi = '" & No_Approval & "' "
			Using Dr = OpenTrans(SQL)
				If Not Dr.Read Then
					CloseConn()
					MessageBox.Show("No Transaksi Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'===============================================
			'=     CEK APAKAH SEMUA USER SUDAH APPROVE     =
			'===============================================
			SQL = "select top 1 a.Kode_Perusahaan "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
			SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
			SQL = SQL & "where a.Status is null and b.status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Flag_Waste_Proses = 'Y' "
			SQL = SQL & "and a.No_Faktur = '" & No_Faktur & "' "
			SQL = SQL & "and b.no_transaksi = '" & No_Approval & "' "
			SQL = SQL & "and b.flag_approve is null "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					CloseConn()
					MessageBox.Show("Terdapat User yang Belum Melakukan Approval", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=======================================
			'=     CEK APAKAH ADA YANG DITOLAK     =
			'=======================================
			SQL = "select top 1 a.Kode_Perusahaan "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
			SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
			SQL = SQL & "where a.Status is null and b.status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Flag_Waste_Proses = 'Y' "
			SQL = SQL & "and a.No_Faktur = '" & No_Faktur & "' "
			SQL = SQL & "and b.no_transaksi = '" & No_Approval & "' "
			SQL = SQL & "and b.flag_approve = 'T' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					CloseConn()
					MessageBox.Show("Transaksi Ditolak", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()

			Dim CrDoc As New Object
			Dim kertas As String = ""

			SQL = "select kode_perusahaan from N_EMI_View_Berita_Acara_Waste_Process "
			SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & No_Faktur & "' and Jenis_Approval = 'Waste_Process' "
			Using Ds = BindingTrans(SQL)
				If Ds.Tables("MyTable").Rows.Count <> 0 Then

					CrDoc = New N_EMI_CR_Berita_Acara_Waste_Proses
					kertas = "Faktur"

					With A_Place_For_Printing2
						CrDoc.SetDataSource(Ds)
						CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						CrDoc.PrintOptions.PrinterName = ""
						CrDoc.RecordSelectionFormula = "{N_EMI_View_Berita_Acara_Waste_Process.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Berita_Acara_Waste_Process.no_faktur}='" & No_Faktur & "' and {N_EMI_View_Berita_Acara_Waste_Process.Jenis_Approval}='Waste_Process' "
						CrDoc.SummaryInfo.ReportTitle = "Waste Process"
						.Text = "Waste Process"
						.CrystalReportViewer1.ReportSource = CrDoc
						.Refresh()
						.Show()
					End With

					'============================================================================================================================================
					'============================================================================================================================================
					'CrDoc.SetDataSource(Ds)
					'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
					'CrDoc.PrintOptions.PrinterName = PrinterNameTS
					'CrDoc.RecordSelectionFormula = "{N_EMI_View_Berita_Acara_Waste_Process.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Berita_Acara_Waste_Process.no_faktur}='" & No_Faktur & "' and {N_EMI_View_Berita_Acara_Waste_Process.Jenis_Approval}='Waste_Process' "
					''CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

					'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
					'doctoprint.PrinterSettings.PrinterName = PrinterNameTS
					''doctoprint.DefaultPageSettings.Landscape = True
					'Dim rawKind As Integer
					'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
					'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
					'    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
					'        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
					'        CrDoc.PrintOptions.PaperSize = rawKind
					'        Exit For
					'    End If
					'Next

					'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
					'CrDoc.PrintToPrinter(1, False, 1, 99)

					'MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Else
					CloseConn()
					MessageBox.Show($"Faktur BA dengan Nomor {No_Faktur} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=======================
			'=     UPDATE FLAG     =
			'=======================
			SQL = "update N_EMI_Transaksi_Transfer_Waste set Flag_Cetak_Faktur = 'Y' "
			SQL &= $"where status is null and Kode_Perusahaan = '{KodePerusahaan}' and No_Faktur = '{No_Faktur}' "
			ExecuteTrans(SQL)

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub CetakFakturDetailToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CetakFakturDetailToolStripMenuItem1.Click
		If Lv_Process_Data.Items.Count = 0 AndAlso Lv_Process_Data.FocusedItem Is Nothing Then Exit Sub

		Tab1_Get_Lv_Process_Data(Lv_Process_Data.FocusedItem.Index)
		Dim No_Approval As String = Lv_Process_NoTransaksiApproval
		Dim No_Faktur As String = Lv_Process_NoFaktur

		Try
			OpenConn()

			'===========================
			'=     CEK BUTTON ROLE     =
			'===========================
			If CekButtonRole("Cetak_Faktur_Waste_Process") = "T" Then
				CloseConn()
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Cetak Faktur Waste Process", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			'===============================
			'=     CEK TRANSAKSI WASTE     =
			'===============================
			SQL = "select top 1 a.Kode_Perusahaan "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
			SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
			SQL = SQL & "where a.Status is null and b.status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Flag_Waste_Proses = 'Y' "
			SQL = SQL & "and a.No_Faktur = '" & No_Faktur & "' "
			SQL = SQL & "and b.no_transaksi = '" & No_Approval & "' "
			Using Dr = OpenTrans(SQL)
				If Not Dr.Read Then
					CloseConn()
					MessageBox.Show("No Transaksi Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'===============================================
			'=     CEK APAKAH SEMUA USER SUDAH APPROVE     =
			'===============================================
			SQL = "select top 1 a.Kode_Perusahaan "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
			SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
			SQL = SQL & "where a.Status is null and b.status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Flag_Waste_Proses = 'Y' "
			SQL = SQL & "and a.No_Faktur = '" & No_Faktur & "' "
			SQL = SQL & "and b.no_transaksi = '" & No_Approval & "' "
			SQL = SQL & "and b.flag_approve is null "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					CloseConn()
					MessageBox.Show("Terdapat User yang Belum Melakukan Approval", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=======================================
			'=     CEK APAKAH ADA YANG DITOLAK     =
			'=======================================
			SQL = "select top 1 a.Kode_Perusahaan "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
			SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
			SQL = SQL & "where a.Status is null and b.status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Flag_Waste_Proses = 'Y' "
			SQL = SQL & "and a.No_Faktur = '" & No_Faktur & "' "
			SQL = SQL & "and b.no_transaksi = '" & No_Approval & "' "
			SQL = SQL & "and b.flag_approve = 'T' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					CloseConn()
					MessageBox.Show("Transaksi Ditolak", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()

			Dim CrDoc As New Object
			Dim kertas As String = ""

			SQL = "select kode_perusahaan from N_EMI_View_Berita_Acara_Waste_Process_Detail "
			SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & No_Faktur & "' and Jenis_Approval = 'Waste_Process' "
			Using Ds = BindingTrans(SQL)
				If Ds.Tables("MyTable").Rows.Count <> 0 Then

					CrDoc = New N_EMI_CR_Berita_Acara_Waste_Proses_Detail
					kertas = "Faktur"

					With A_Place_For_Printing2
						CrDoc.SetDataSource(Ds)
						CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						CrDoc.PrintOptions.PrinterName = ""
						CrDoc.RecordSelectionFormula = "{N_EMI_View_Berita_Acara_Waste_Process_Detail.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Berita_Acara_Waste_Process_Detail.no_faktur}='" & No_Faktur & "' and {N_EMI_View_Berita_Acara_Waste_Process_Detail.Jenis_Approval}='Waste_Process' "
						CrDoc.SummaryInfo.ReportTitle = "Waste Process Detail"
						.Text = "Waste Process"
						.CrystalReportViewer1.ReportSource = CrDoc
						.Refresh()
						.Show()
					End With

					'============================================================================================================================================
					'============================================================================================================================================
					'CrDoc.SetDataSource(Ds)
					'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
					'CrDoc.PrintOptions.PrinterName = PrinterNameTS
					'CrDoc.RecordSelectionFormula = "{N_EMI_View_Berita_Acara_Waste_Process_Detail.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Berita_Acara_Waste_Process_Detail.no_faktur}='" & No_Faktur & "' and {N_EMI_View_Berita_Acara_Waste_Process_Detail.Jenis_Approval}='Waste_Process' "
					''CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

					'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
					'doctoprint.PrinterSettings.PrinterName = PrinterNameTS
					''doctoprint.DefaultPageSettings.Landscape = True
					'Dim rawKind As Integer
					'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
					'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
					'    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
					'        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
					'        CrDoc.PrintOptions.PaperSize = rawKind
					'        Exit For
					'    End If
					'Next

					'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
					'CrDoc.PrintToPrinter(1, False, 1, 99)

					'MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Else
					CloseConn()
					MessageBox.Show($"Faktur BA dengan Nomor {No_Faktur} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=======================
			'=     UPDATE FLAG     =
			'=======================
			SQL = "update N_EMI_Transaksi_Transfer_Waste set Flag_Cetak_Faktur = 'Y' "
			SQL &= $"where status is null and Kode_Perusahaan = '{KodePerusahaan}' and No_Faktur = '{No_Faktur}' "
			ExecuteTrans(SQL)

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Load_Data_Product_Waste()
		Try
			OpenConn()

			Lv_Product_Data.Items.Clear() : Lv_Product_User_Approve.Items.Clear() : Lv_Product_Detail_Barang.Items.Clear() : Lv_Product_Detail_Barcode.Items.Clear()

			SQL = "select distinct b.No_Transaksi, a.No_Faktur, a.Lokasi, a.Kode_Stock_Owner, a.Tanggal, a.Jam, a.Keterangan, a.UserID as User_Input, "
			SQL = SQL & "isnull((x.isCompleted), 'Y') as isCompleted, a.Flag_Cetak_Faktur , isnull(y.Flag, 'Y') as isAllRejected "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
			SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
			SQL = SQL & "outer apply( "
			SQL = SQL & "select top 1 'T' as isCompleted "
			SQL = SQL & "from N_EMI_Transaksi_Approval_Waste z "
			SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan "
			SQL = SQL & "and z.No_Faktur_Waste = a.No_Faktur "
			SQL = SQL & "and z.Status is null "
			SQL = SQL & "and z.Flag_Approve is null "
			SQL = SQL & ") x "
			SQL = SQL & "LEFT JOIN ( select distinct z.Kode_Perusahaan, z.No_Faktur, 'T' as Flag "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk_Det z "
			SQL = SQL & ") y on a.Kode_Perusahaan = y.Kode_Perusahaan and a.No_Faktur = y.No_Faktur "
			SQL = SQL & "where a.Status is null and b.Status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Kode_Stock_Owner = '" & Cmb2_Gudang.Text.Trim & "' "
			SQL = SQL & "and b.Jenis_Approval = 'Waste_Produk' "
			SQL = SQL & "and a.Flag_Waste_Product = 'Y'	"

			If Cmb_Lokasi2.SelectedIndex <> 0 Then
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
				SQL &= $" a.Lokasi = '{arrFilterLokasiGudang.Item(Cmb_Lokasi2.SelectedIndex)}' "
			End If

			If Cb_Hari_Ini2.Checked Then
				'Pasang And
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

				SQL &= $" a.tanggal between '"
				SQL &= Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
			End If

			If Cb_Tanggal2.Checked Then
				'Pasang And
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

				SQL &= arrFilterTanggal_2.Item(Cmb_Tanggal2.SelectedIndex) & " between ' "
				SQL &= Format(Tgl_1_2.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl_2_2.Value, "yyyy-MM-dd") & "' "
			End If

			If Cb_Param_Lain2.Checked Then
				'Pasang And
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

				SQL &= arrFilterParamLain_2.Item(Cmb_Param_Lain2.SelectedIndex) & " like '%" & Trim(Txt_Param_Lain2.Text) & "%' "
			End If

			If Not Rd_Cetak_Semua2.Checked Then
				If Rd_Cetak_Belum2.Checked Then
					SQL = SQL & "and a.Flag_Cetak_Faktur is null "
				ElseIf Rd_Cetak_Sudah2.Checked Then
					SQL = SQL & "and a.Flag_Cetak_Faktur = 'Y' "
				End If
			End If

			SQL = SQL & "order by a.Tanggal, a.Jam "

			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Product_Data.Items.Add(Dr("No_Transaksi"))
					Lv.SubItems.Add(Dr("No_Faktur"))
					Lv.SubItems.Add(Dr("Lokasi"))
					Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
					Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
					Lv.SubItems.Add(Dr("Jam"))
					Lv.SubItems.Add(Dr("Keterangan"))
					Lv.SubItems.Add(Dr("User_Input"))

					If Dr("isCompleted").ToString.Trim = "T" Then
						Lv.BackColor = Color.White
					ElseIf Dr("isCompleted").ToString.Trim = "Y" Then
						Lv.BackColor = Color.LightGreen
					Else
						Lv.BackColor = Color.White
					End If

					If Dr("isCompleted").ToString.Trim = "Y" And General_Class.CekNULL(Dr("Flag_Cetak_Faktur")).Trim = "Y" Then
						Lv.BackColor = Color.Goldenrod
					End If

					If Dr("isCompleted").ToString.Trim = "Y" And General_Class.CekNULL(Dr("isAllRejected")).Trim = "Y" Then
						Lv.BackColor = Color.DarkRed
						Lv.ForeColor = Color.White
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

	Private Sub Lv_Product_Data_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Product_Data.SelectedIndexChanged
		If Lv_Product_Data.Items.Count = 0 OrElse Lv_Product_Data.FocusedItem Is Nothing Then Exit Sub

		Try
			OpenConn()
			Tab2_Get_Lv_Product_Data(Lv_Product_Data.FocusedItem.Index)

			Dim No_faktur As String = Lv_Product_NoFaktur
			Dim No_Transaksi As String = Lv_Product_NoTransaksiApproval

			Lv_Product_User_Approve.Items.Clear()
			SQL = "select case when b.Approval_Level<>'1' then c.username else d.UserName end as username, "
			SQL = SQL & "b.Approval_Level, b.Flag_Approve, b.Tanggal_Approve, b.Jam_Approve, b.Id_User_Android_Approve, b.jabatan, "
			SQL = SQL & "b.Keterangan as Keterangan_Android "
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
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Product_User_Approve.Items.Add(If(General_Class.CekNULL(Dr("username")).Trim = "", "-", Dr("username")))
					Lv.SubItems.Add(Dr("Approval_Level"))

					If General_Class.CekNULL(Dr("Flag_Approve")).Trim = "Y" Then
						Lv.SubItems.Add("Approved")
						Lv.BackColor = Color.LightGreen
					ElseIf General_Class.CekNULL(Dr("Flag_Approve")).Trim = "T" Then
						Lv.SubItems.Add("Rejected")
						Lv.ForeColor = Color.White
						Lv.BackColor = Color.DarkRed
					Else
						Lv.SubItems.Add("On Process")
					End If

					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Tanggal_Approve")).Trim = "", "-", Format(Dr("Tanggal_Approve"), "dd MMM yyyy")))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jam_Approve")).Trim = "", "-", Dr("Jam_Approve")))
					Lv.SubItems.Add(Dr("Id_User_Android_Approve"))
					Lv.SubItems.Add(Dr("jabatan"))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Keterangan_Android")).Trim = "", "-", Dr("Keterangan_Android")))
				Loop
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
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Product_Detail_Barang.Items.Add(Dr("Lokasi"))
					Lv.SubItems.Add(Dr("kode_barang"))
					Lv.SubItems.Add(Dr("Nama_Barang"))
					Lv.SubItems.Add(Format(Dr("Total"), "N4"))
					Lv.SubItems.Add(Dr("Satuan"))
				Loop
			End Using

			'=======================================
			'=     LOAD DATA DETAIL PERBARCODE     =
			'=======================================
			Lv_Product_Detail_Barcode.Items.Clear()

#Region " Kode Lama 24 Mei 2026"

			'SQL = "with Cte as ( "
			'SQL = SQL & "select a.kode_perusahaan, 'Good Receiver 1' as asal, a.No_Faktur, f.No_Transaksi, "
			'SQL = SQL & "(g.qr_code +'-'+g.kode_unik_berjalan) as Barcode_Awal, "
			'SQL = SQL & "(h.qr_code +'-'+h.kode_unik_berjalan) as Barcode_Akhir, "
			'SQL = SQL & "c.Jumlah, b.Satuan "
			'SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
			'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
			'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det c on b.Kode_Perusahaan = c.kode_perusahaan and b.No_Faktur = c.no_faktur and b.Urut_Oto = c.urut_tf "
			'SQL = SQL & "left join N_EMI_Transaksi_Transfer_Waste_Produk_Det2 d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
			'SQL = SQL & "inner join Emi_Production_Results_Detail_Scrap e on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Serial_Number_Awal = e.Serial_Number "
			'SQL = SQL & "inner join Emi_Production_Results f on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Transaksi = f.No_Transaksi and f.Status is null "
			'SQL = SQL & "inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and c.Serial_Number_Awal = g.Serial_Number "
			'SQL = SQL & "left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number "
			'SQL = SQL & "union all "
			'SQL = SQL & "select a.kode_perusahaan, 'Good Receiver 2' as asal, a.No_Faktur, f.No_Transaksi, "
			'SQL = SQL & "(g.qr_code +'-'+g.kode_unik_berjalan) as Barcode_Awal, "
			'SQL = SQL & "(h.qr_code +'-'+h.kode_unik_berjalan) as Barcode_Akhir, "
			'SQL = SQL & "e.Jumlah, e.Satuan "
			'SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
			'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
			'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det c on b.Kode_Perusahaan = c.kode_perusahaan and b.No_Faktur = c.no_faktur and b.Urut_Oto = c.urut_tf "
			'SQL = SQL & "left join N_EMI_Transaksi_Transfer_Waste_Produk_Det2 d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
			'SQL = SQL & "inner join Emi_Production_Results_Validation_Detail e on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Serial_Number_Awal = e.Serial_Number_Tujuan "
			'SQL = SQL & "inner join Emi_Production_Results_Validation f on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Transaksi = f.No_Transaksi and f.Status is null "
			'SQL = SQL & "inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and c.Serial_Number_Awal = g.Serial_Number "
			'SQL = SQL & "left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number "
			'SQL = SQL & "union all "
			'SQL = SQL & "select a.kode_perusahaan, 'Waste Packaging' as asal, a.No_Faktur, f.No_Transaksi, "
			'SQL = SQL & "(g.qr_code +'-'+g.kode_unik_berjalan) as Barcode_Awal, "
			'SQL = SQL & "(h.qr_code +'-'+h.kode_unik_berjalan) as Barcode_Akhir, "
			'SQL = SQL & "e.Jumlah_Tujuan as Jumlah, e.Satuan_Tujuan as Satuan "
			'SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
			'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
			'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det c on b.Kode_Perusahaan = c.kode_perusahaan and b.No_Faktur = c.no_faktur and b.Urut_Oto = c.urut_tf "
			'SQL = SQL & "left join N_EMI_Transaksi_Transfer_Waste_Produk_Det2 d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
			'SQL = SQL & "inner join EMI_Production_Results_Detail_Change_Packaging_Detail e on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Serial_Number_Awal = e.SN_Scrap "
			'SQL = SQL & "inner join EMI_Production_Results_Detail_Change_Packaging f on e.Kode_Perusahaan = f.Kode_Perusahaan and e.no_transaksi = f.no_transaksi and f.Status is null "
			'SQL = SQL & "inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and c.Serial_Number_Awal = g.Serial_Number "
			'SQL = SQL & "left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number ) "
			'SQL = SQL & "select asal, No_Faktur, No_Transaksi, Barcode_Awal, Barcode_Akhir, Jumlah, Satuan "
			'SQL = SQL & "from cte "
			'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
			'SQL = SQL & "and No_Faktur = '" & No_faktur & "' "
			'SQL = SQL & "order by No_Faktur "

#End Region

			SQL = $"
				select a.kode_perusahaan, 'Goods Received 1' as asal, 'Approved' as StatusBarcode, a.No_Faktur, f.No_Transaksi, f.No_Production_Order as No_Split,
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

				select a.kode_perusahaan, 'Goods Received 1' as asal, 'Rejected' as StatusBarcode, a.No_Faktur, f.No_Transaksi, f.No_Production_Order,
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

				select a.kode_perusahaan, 'Goods Received 2' as asal, 'Approved' as StatusBarcode, a.No_Faktur, f.No_Transaksi, f.No_Production_Order as No_Split,
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

				select a.kode_perusahaan, 'Goods Received 2' as asal, 'Rejected' as StatusBarcode, a.No_Faktur, f.No_Transaksi, f.No_Production_Order as No_Split,
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

				  select a.kode_perusahaan, 'Goods Received 3' as asal, 'Approved' as StatusBarcode, a.No_Faktur, f.No_Transaksi,
						   k.No_Production_Order as No_Split,
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
						 inner join Emi_Production_Results_Validation_Detail j on e.Kode_Perusahaan = j.Kode_Perusahaan and e.Serial_Number_Awal = j.Serial_Number_Akhir
						 inner join Emi_Production_Results_Validation k on j.Kode_Perusahaan = k.Kode_Perusahaan and j.No_Transaksi = k.No_Transaksi
						 inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and c.Serial_Number_Awal = g.Serial_Number
						 left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number
					where a.Kode_Perusahaan = '{KodePerusahaan}'
					  and a.No_Faktur = '{No_faktur}'
					group by a.kode_perusahaan, a.No_Faktur, f.No_Transaksi, e.Satuan, (g.qr_code + '-' + g.kode_unik_berjalan),
							 (h.qr_code + '-' + h.kode_unik_berjalan), k.No_Production_Order

					  union all

					  select a.kode_perusahaan, 'Goods Received 3' as asal, 'Rejected' as StatusBarcode, a.No_Faktur, f.No_Transaksi,
						   k.No_Production_Order as No_Split,
						   (g.qr_code + '-' + g.kode_unik_berjalan) as Barcode_Awal,
						   (h.qr_code + '-' + h.kode_unik_berjalan) as Barcode_Akhir, sum(e.Jumlah) as Jumlah, e.Satuan
					from N_EMI_Transaksi_Transfer_Waste_Produk a
						 inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b
									on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
						 inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det_Log_Approval c
									on b.Kode_Perusahaan = c.kode_perusahaan and b.No_Faktur = c.no_faktur and b.Urut_Oto = c.urut_tf
						 left join N_EMI_Transaksi_Transfer_Waste_Produk_Det2 d
								   on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det
						 inner join N_EMI_Validation_GR_3_Detail e
									on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Serial_Number_Awal = e.Serial_Number_Tujuan
						 inner join N_EMI_Validation_GR_3 f
									on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Transaksi = f.No_Transaksi and f.Status is null
						 inner join Emi_Production_Results_Validation_Detail j on e.Kode_Perusahaan = j.Kode_Perusahaan and e.Serial_Number_Awal = j.Serial_Number_Akhir
						 inner join Emi_Production_Results_Validation k on j.Kode_Perusahaan = k.Kode_Perusahaan and j.No_Transaksi = k.No_Transaksi
						 inner join Barang_SN g on a.Kode_Perusahaan = g.Kode_Perusahaan and c.Serial_Number_Awal = g.Serial_Number
						 left join Barang_SN h on a.Kode_Perusahaan = h.Kode_Perusahaan and d.Serial_Number = h.Serial_Number
						 left join N_EMI_Transaksi_Transfer_Waste_Produk_Det i
								   on b.Kode_Perusahaan = i.kode_perusahaan and b.No_Faktur = i.no_faktur and b.Urut_Oto = i.urut_tf and
									  c.Serial_Number_Awal = i.Serial_Number_Awal
					where a.Kode_Perusahaan = '{KodePerusahaan}'
					  and a.No_Faktur = '{No_faktur}'
					  and i.Kode_Perusahaan is null
					group by a.kode_perusahaan, a.No_Faktur, f.No_Transaksi, e.Satuan, (g.qr_code + '-' + g.kode_unik_berjalan),
							 (h.qr_code + '-' + h.kode_unik_berjalan), k.No_Production_Order

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
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Product_Detail_Barcode.Items.Add(Dr("asal"))
					Lv.SubItems.Add(Dr("No_Transaksi"))
					Lv.SubItems.Add(Dr("Barcode_Awal"))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Barcode_Akhir")).Trim = "", "-", Dr("Barcode_Akhir")))
					Lv.SubItems.Add(Format(Dr("Jumlah"), "N4"))
					Lv.SubItems.Add(Dr("Satuan"))
					Lv.SubItems.Add(Dr("StatusBarcode"))
					Lv.SubItems.Add(Dr("No_Split"))

					If General_Class.CekNULL(Dr("StatusBarcode")).Trim = "Rejected" Then
						Lv.BackColor = Color.DarkRed
						Lv.ForeColor = Color.White
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

	Private Sub CetakFakturToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CetakFakturToolStripMenuItem1.Click
		If Lv_Product_Data.Items.Count = 0 AndAlso Lv_Product_Data.FocusedItem Is Nothing Then Exit Sub

		Tab2_Get_Lv_Product_Data(Lv_Product_Data.FocusedItem.Index)
		Dim No_Approval As String = Lv_Product_NoTransaksiApproval
		Dim No_Faktur As String = Lv_Product_NoFaktur

		Try
			OpenConn()

			'===========================
			'=     CEK BUTTON ROLE     =
			'===========================
			If CekButtonRole("Cetak_Faktur_Waste_Produk") = "T" Then
				CloseConn()
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Cetak Faktur Waste Product", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			'===============================
			'=     CEK TRANSAKSI WASTE     =
			'===============================
			SQL = "select top 1 a.Kode_Perusahaan "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
			SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur_Waste "
			SQL = SQL & "where a.Status Is null And b.status Is null "
			SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Flag_Waste_Product = 'Y' "
			SQL = SQL & "and a.No_Faktur = '" & No_Faktur & "' "
			SQL = SQL & "and b.no_transaksi = '" & No_Approval & "' "
			Using Dr = OpenTrans(SQL)
				If Not Dr.Read Then
					CloseConn()
					MessageBox.Show("No Transaksi Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'SQL = "select top 1 a.Kode_Perusahaan "
			'SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
			'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
			'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
			'SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur_Produk = d.No_Faktur_Waste "
			'SQL = SQL & "where a.Status is null and d.status is null "
			'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			''SQL = SQL & "and a.Flag_Waste_Product = 'Y' "
			'SQL = SQL & "and c.No_Faktur_Produk = '" & No_Faktur & "' "
			'SQL = SQL & "and d.no_transaksi = '" & No_Approval & "' "
			'Using Dr = OpenTrans(SQL)
			'    If Not Dr.Read Then
			'        CloseConn()
			'        MessageBox.Show("No Transaksi Belum Melakukan Pengajuan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        Exit Sub
			'    End If
			'End Using

			'===============================================
			'=     CEK APAKAH SEMUA USER SUDAH APPROVE     =
			'===============================================
			SQL = "select top 1 a.Kode_Perusahaan "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
			SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
			SQL = SQL & "where a.Status is null and b.status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Flag_Waste_Product = 'Y' "
			SQL = SQL & "and a.No_Faktur = '" & No_Faktur & "' "
			SQL = SQL & "and b.no_transaksi = '" & No_Approval & "' "
			SQL = SQL & "and b.flag_approve is null "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					CloseConn()
					MessageBox.Show("Terdapat User yang Belum Melakukan Approval", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=======================================
			'=     CEK APAKAH ADA YANG DITOLAK     =
			'=======================================
			SQL = "select top 1 a.Kode_Perusahaan "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
			SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
			SQL = SQL & "where a.Status is null and b.status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Flag_Waste_Product = 'Y' "
			SQL = SQL & "and a.No_Faktur = '" & No_Faktur & "' "
			SQL = SQL & "and b.no_transaksi = '" & No_Approval & "' "
			SQL = SQL & "and b.flag_approve = 'T' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					CloseConn()
					MessageBox.Show("Transaksi Ditolak", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()

			Dim CrDoc As New Object
			Dim kertas As String = ""

			SQL = "select kode_perusahaan from N_EMI_View_Berita_Acara_pemusnahan_waste_produk "
			SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & No_Faktur & "' and Jenis_Approval = 'Waste_Produk' "
			Using Ds = BindingTrans(SQL)
				If Ds.Tables("MyTable").Rows.Count <> 0 Then

					CrDoc = New N_EMI_CR_Berita_Acara_Pemusnahan_Waste_Produk
					kertas = "Faktur"

					With A_Place_For_Printing2
						CrDoc.SetDataSource(Ds)
						CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						CrDoc.PrintOptions.PrinterName = ""
						CrDoc.RecordSelectionFormula = "{N_EMI_View_Berita_Acara_pemusnahan_waste_produk.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Berita_Acara_pemusnahan_waste_produk.no_faktur}='" & No_Faktur & "' and {N_EMI_View_Berita_Acara_pemusnahan_waste_produk.Jenis_Approval}='Waste_Produk' "
						CrDoc.SummaryInfo.ReportTitle = "Waste Process"
						.Text = "Waste Process"
						.CrystalReportViewer1.ReportSource = CrDoc
						.Refresh()
						.Show()
					End With

					'============================================================================================================================================
					'============================================================================================================================================
					'CrDoc.SetDataSource(Ds)
					'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
					'CrDoc.PrintOptions.PrinterName = PrinterNameTS
					'CrDoc.RecordSelectionFormula = "{N_EMI_View_Berita_Acara_pemusnahan_waste_produk.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Berita_Acara_pemusnahan_waste_produk.no_faktur}='" & No_Faktur & "' and {N_EMI_View_Berita_Acara_pemusnahan_waste_produk.Jenis_Approval}='Waste_Produk' "
					''CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

					'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
					'doctoprint.PrinterSettings.PrinterName = PrinterNameTS
					''doctoprint.DefaultPageSettings.Landscape = True
					'Dim rawKind As Integer
					'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
					'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
					'    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
					'        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
					'        CrDoc.PrintOptions.PaperSize = rawKind
					'        Exit For
					'    End If
					'Next

					'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
					'CrDoc.PrintToPrinter(1, False, 1, 99)

					'MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Else
					CloseConn()
					MessageBox.Show($"Faktur BA dengan Nomor {No_Faktur} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub

				End If
			End Using

			'=======================
			'=     UPDATE FLAG     =
			'=======================
			SQL = "update N_EMI_Transaksi_Transfer_Waste_Produk set Flag_Cetak_Faktur = 'Y' "
			SQL &= $"where status is null and Kode_Perusahaan = '{KodePerusahaan}' and No_Faktur = '{No_Faktur}' "
			ExecuteTrans(SQL)

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub CetakFakturDetailToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakFakturDetailToolStripMenuItem.Click
		If Lv_Product_Data.Items.Count = 0 AndAlso Lv_Product_Data.FocusedItem Is Nothing Then Exit Sub

		Tab2_Get_Lv_Product_Data(Lv_Product_Data.FocusedItem.Index)
		Dim No_Approval As String = Lv_Product_NoTransaksiApproval
		Dim No_Faktur As String = Lv_Product_NoFaktur

		Try
			OpenConn()

			'===========================
			'=     CEK BUTTON ROLE     =
			'===========================
			If CekButtonRole("Cetak_Faktur_Waste_Produk") = "T" Then
				CloseConn()
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Cetak Faktur Waste Product", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			'===============================
			'=     CEK TRANSAKSI WASTE     =
			'===============================
			SQL = "select top 1 a.Kode_Perusahaan "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
			SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur_Waste "
			SQL = SQL & "where a.Status Is null And b.status Is null "
			SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Flag_Waste_Product = 'Y' "
			SQL = SQL & "and a.No_Faktur = '" & No_Faktur & "' "
			SQL = SQL & "and b.no_transaksi = '" & No_Approval & "' "
			Using Dr = OpenTrans(SQL)
				If Not Dr.Read Then
					CloseConn()
					MessageBox.Show("No Transaksi Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'SQL = "select top 1 a.Kode_Perusahaan "
			'SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
			'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
			'SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
			'SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur_Produk = d.No_Faktur_Waste "
			'SQL = SQL & "where a.Status is null and d.status is null "
			'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			''SQL = SQL & "and a.Flag_Waste_Product = 'Y' "
			'SQL = SQL & "and c.No_Faktur_Produk = '" & No_Faktur & "' "
			'SQL = SQL & "and d.no_transaksi = '" & No_Approval & "' "
			'Using Dr = OpenTrans(SQL)
			'    If Not Dr.Read Then
			'        CloseConn()
			'        MessageBox.Show("No Transaksi Belum Melakukan Pengajuan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'        Exit Sub
			'    End If
			'End Using

			'===============================================
			'=     CEK APAKAH SEMUA USER SUDAH APPROVE     =
			'===============================================
			SQL = "select top 1 a.Kode_Perusahaan "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
			SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
			SQL = SQL & "where a.Status is null and b.status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Flag_Waste_Product = 'Y' "
			SQL = SQL & "and a.No_Faktur = '" & No_Faktur & "' "
			SQL = SQL & "and b.no_transaksi = '" & No_Approval & "' "
			SQL = SQL & "and b.flag_approve is null "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					CloseConn()
					MessageBox.Show("Terdapat User yang Belum Melakukan Approval", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=======================================
			'=     CEK APAKAH ADA YANG DITOLAK     =
			'=======================================
			SQL = "select top 1 a.Kode_Perusahaan "
			SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
			SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
			SQL = SQL & "where a.Status is null and b.status is null "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Flag_Waste_Product = 'Y' "
			SQL = SQL & "and a.No_Faktur = '" & No_Faktur & "' "
			SQL = SQL & "and b.no_transaksi = '" & No_Approval & "' "
			SQL = SQL & "and b.flag_approve = 'T' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					CloseConn()
					MessageBox.Show("Transaksi Ditolak", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try
			OpenConn()

			Dim CrDoc As New Object
			Dim kertas As String = ""

			SQL = "select kode_perusahaan from N_EMI_View_Berita_Acara_Pemusnahan_Waste_Produk_Detail "
			SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & No_Faktur & "' and Jenis_Approval = 'Waste_Produk' "
			Using Ds = BindingTrans(SQL)
				If Ds.Tables("MyTable").Rows.Count <> 0 Then

					CrDoc = New N_EMI_CR_Berita_Acara_Pemusnahan_Waste_Produk_Detail
					kertas = "Faktur"

					With A_Place_For_Printing2
						CrDoc.SetDataSource(Ds)
						CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						CrDoc.PrintOptions.PrinterName = ""
						CrDoc.RecordSelectionFormula = "{N_EMI_View_Berita_Acara_Pemusnahan_Waste_Produk_Detail.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Berita_Acara_Pemusnahan_Waste_Produk_Detail.no_faktur}='" & No_Faktur & "' and {N_EMI_View_Berita_Acara_Pemusnahan_Waste_Produk_Detail.Jenis_Approval}='Waste_Produk' "
						CrDoc.SummaryInfo.ReportTitle = "Waste Process"
						.Text = "Waste Process"
						.CrystalReportViewer1.ReportSource = CrDoc
						.Refresh()
						.Show()
					End With

					'============================================================================================================================================
					'============================================================================================================================================
					'CrDoc.SetDataSource(Ds)
					'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
					'CrDoc.PrintOptions.PrinterName = PrinterNameTS
					'CrDoc.RecordSelectionFormula = "{N_EMI_View_Berita_Acara_Pemusnahan_Waste_Produk_Detail.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Berita_Acara_Pemusnahan_Waste_Produk_Detail.no_faktur}='" & No_Faktur & "' and {N_EMI_View_Berita_Acara_Pemusnahan_Waste_Produk_Detail.Jenis_Approval}='Waste_Produk' "
					''CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

					'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
					'doctoprint.PrinterSettings.PrinterName = PrinterNameTS
					''doctoprint.DefaultPageSettings.Landscape = True
					'Dim rawKind As Integer
					'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
					'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
					'    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
					'        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
					'        CrDoc.PrintOptions.PaperSize = rawKind
					'        Exit For
					'    End If
					'Next

					'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
					'CrDoc.PrintToPrinter(1, False, 1, 99)

					'MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Else
					CloseConn()
					MessageBox.Show($"Faktur BA dengan Nomor {No_Faktur} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub

				End If
			End Using

			'=======================
			'=     UPDATE FLAG     =
			'=======================
			SQL = "update N_EMI_Transaksi_Transfer_Waste_Produk set Flag_Cetak_Faktur = 'Y' "
			SQL &= $"where status is null and Kode_Perusahaan = '{KodePerusahaan}' and No_Faktur = '{No_Faktur}' "
			ExecuteTrans(SQL)

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
		If Cb_Hari_Ini.Checked = False And Cb_Tanggal.Checked = False And Cb_Param_Lain.Checked = False Then
			MessageBox.Show("Check salah satu filter dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cb_Hari_Ini.Focus() : Exit Sub
		End If

		If Cb_Tanggal.Checked Then
			If Cmb_Tanggal.SelectedIndex = -1 Then
				MessageBox.Show("Parameter Tanggal Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Cmb_Tanggal.DroppedDown = True : Cmb_Tanggal.Focus() : Exit Sub
			ElseIf Tgl_1.Value > Tgl_2.Value Then
				MessageBox.Show("Periode I Tidak Boleh Lebih Dari periode II!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Tgl_1.Value = Now.Date : Tgl_2.Value = Now.Date
				Exit Sub
			End If
		End If

		If Cb_Param_Lain.Checked Then
			If Cmb_Param_Lain.SelectedIndex = -1 Then
				MessageBox.Show("Parameter Lain Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Cmb_Param_Lain.DroppedDown = True : Cmb_Param_Lain.Focus() : Exit Sub
			ElseIf Txt_Param_Lain.Text.Trim.Length = 0 Then
				MessageBox.Show("Value Filter Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Txt_Param_Lain.Focus() : Exit Sub
			End If
		End If

		Load_Data_Process_Waste()
	End Sub

	Private Sub Btn_Cari2_Click(sender As Object, e As EventArgs) Handles Btn_Cari2.Click
		If Cb_Hari_Ini2.Checked = False And Cb_Tanggal2.Checked = False And Cb_Param_Lain2.Checked = False Then
			MessageBox.Show("Check salah satu filter dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cb_Hari_Ini2.Focus() : Exit Sub
		End If

		If Cb_Tanggal2.Checked Then
			If Cmb_Tanggal2.SelectedIndex = -1 Then
				MessageBox.Show("Parameter Tanggal Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Cmb_Tanggal2.DroppedDown = True : Cmb_Tanggal2.Focus() : Exit Sub
			ElseIf Tgl_1_2.Value > Tgl_2_2.Value Then
				MessageBox.Show("Periode I Tidak Boleh Lebih Dari periode II!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Tgl_1_2.Value = Now.Date : Tgl_2_2.Value = Now.Date
				Exit Sub
			End If
		End If

		If Cb_Param_Lain2.Checked Then
			If Cmb_Param_Lain2.SelectedIndex = -1 Then
				MessageBox.Show("Parameter Lain Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Cmb_Param_Lain2.DroppedDown = True : Cmb_Param_Lain2.Focus() : Exit Sub
			ElseIf Txt_Param_Lain2.Text.Trim.Length = 0 Then
				MessageBox.Show("Value Filter Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Txt_Param_Lain2.Focus() : Exit Sub
			End If
		End If

		Load_Data_Product_Waste()

	End Sub

	'====================================================================================================================================================================================

#Region "HANDLE KEYPRESS TAB 1"

	Private Sub Cb_Hari_Ini_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_Hari_Ini.CheckedChanged
		If Cb_Hari_Ini.Checked = True Then
			Cb_Tanggal.Checked = False
			Btn_Cari.PerformClick()
		End If
	End Sub

	Private Sub Cb_Tanggal_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_Tanggal.CheckedChanged

		If Cb_Tanggal.Checked Then
			Cb_Hari_Ini.Checked = False
			Cmb_Tanggal.Enabled = True
		Else
			Cmb_Tanggal.Enabled = False
		End If
		Tgl_1.Enabled = False : Tgl_2.Enabled = False
		Tgl_1.Value = Now.Date : Tgl_2.Value = Now.Date
		Cmb_Tanggal.SelectedIndex = -1

	End Sub

	Private Sub Cb_Param_Lain_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_Param_Lain.CheckedChanged
		If Cb_Param_Lain.Checked Then
			Cmb_Param_Lain.Enabled = True : Txt_Param_Lain.Enabled = True
		Else
			Cmb_Param_Lain.Enabled = False : Txt_Param_Lain.Enabled = False
			Cmb_Param_Lain.SelectedIndex = -1 : Txt_Param_Lain.Text = ""
		End If
	End Sub

	Private Sub Cmb_Lokasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lokasi.KeyPress
		If e.KeyChar = Chr(13) Then Cb_Hari_Ini.Focus()
	End Sub

	Private Sub Cb_Hari_Ini_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cb_Hari_Ini.KeyPress
		If e.KeyChar = Chr(13) Then Cb_Tanggal.Focus()
	End Sub

	Private Sub Cb_Tanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cb_Tanggal.KeyPress
		If e.KeyChar = Chr(13) Then
			If Cb_Tanggal.Checked Then
				Cmb_Tanggal.DroppedDown = True
				Cmb_Tanggal.Focus()
			Else
				Cb_Param_Lain.Focus()
			End If

		End If
	End Sub

	Private Sub Cb_Param_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cb_Param_Lain.KeyPress
		If e.KeyChar = Chr(13) Then
			If Cb_Param_Lain.Checked Then
				Cmb_Param_Lain.DroppedDown = True
				Cmb_Param_Lain.Focus()
			Else
				Btn_Cari.Focus()
			End If

		End If
	End Sub

	Private Sub Cmb2_Gudang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb2_Gudang.SelectedIndexChanged
		'Btn_Cari2.PerformClick()
	End Sub

	Private Sub Cmb_Gudang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Gudang.SelectedIndexChanged
		'Btn_Cari.PerformClick()
	End Sub

	Private Sub Cmb_Tanggal2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Tanggal2.SelectedIndexChanged
		If Cmb_Tanggal2.Items.Count = 0 Then Exit Sub

		If Cmb_Tanggal2.SelectedIndex <> -1 Then
			Tgl_1_2.Enabled = True : Tgl_2_2.Enabled = True
		Else
			Tgl_1_2.Enabled = False : Tgl_2_2.Enabled = False
		End If
		Tgl_1_2.Value = Now.Date : Tgl_2_2.Value = Now.Date
	End Sub

	Private Sub SalinNoApprovalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoApprovalToolStripMenuItem.Click
		If Lv_Product_Data.Items.Count = 0 Or Lv_Product_Data.SelectedItems.Count = 0 Or Lv_Product_Data.FocusedItem Is Nothing Then
			MessageBox.Show("Pilih dahulu no approval yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Clipboard.SetText(Lv_Product_Data.FocusedItem.SubItems(Item_Product_NoTransaksiApproval).Text)
	End Sub

	Private Sub SalinNoFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoFakturToolStripMenuItem.Click
		If Lv_Product_Data.Items.Count = 0 Or Lv_Product_Data.SelectedItems.Count = 0 Or Lv_Product_Data.FocusedItem Is Nothing Then
			MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Clipboard.SetText(Lv_Product_Data.FocusedItem.SubItems(Item_Product_NoFaktur).Text)
	End Sub

	Private Sub SalinNoApprovalToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SalinNoApprovalToolStripMenuItem1.Click
		If Lv_Process_Data.Items.Count = 0 Or Lv_Process_Data.SelectedItems.Count = 0 Or Lv_Process_Data.FocusedItem Is Nothing Then
			MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Clipboard.SetText(Lv_Process_Data.FocusedItem.SubItems(Item_Process_NoTransaksiApproval).Text)
	End Sub

	Private Sub SalinNoFakturToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SalinNoFakturToolStripMenuItem1.Click
		If Lv_Process_Data.Items.Count = 0 Or Lv_Process_Data.SelectedItems.Count = 0 Or Lv_Process_Data.FocusedItem Is Nothing Then
			MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Clipboard.SetText(Lv_Process_Data.FocusedItem.SubItems(Item_Process_NoFaktur).Text)
	End Sub

	Private Sub Cmb_Tanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Tanggal.KeyPress
		If e.KeyChar = Chr(13) Then Tgl_1.Focus()
	End Sub

	Private Sub Tgl_1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl_1.KeyPress
		If e.KeyChar = Chr(13) Then Tgl_2.Focus()
	End Sub

	Private Sub Tgl_2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl_2.KeyPress
		If e.KeyChar = Chr(13) Then Cb_Param_Lain.Focus()
	End Sub

	Private Sub Cmb_Tanggal_SelectedValueChanged(sender As Object, e As EventArgs) Handles Cmb_Tanggal.SelectedValueChanged
		If Cmb_Tanggal.Items.Count = 0 Then Exit Sub

		If Cmb_Tanggal.SelectedIndex <> -1 Then
			Tgl_1.Enabled = True : Tgl_2.Enabled = True
		Else
			Tgl_1.Enabled = False : Tgl_2.Enabled = False
		End If
		Tgl_1.Value = Now.Date : Tgl_2.Value = Now.Date
	End Sub

	Private Sub Cmb_Param_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Param_Lain.KeyPress
		If e.KeyChar = Chr(13) Then Txt_Param_Lain.Focus()
	End Sub

	Private Sub Txt_Param_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Param_Lain.KeyPress
		If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
	End Sub

#End Region

	'====================================================================================================================================================================================

#Region "HANDLE KEYPRESS TAB 2"

	Private Sub Cmb_Lokasi2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lokasi2.KeyPress
		If e.KeyChar = Chr(13) Then Cb_Hari_Ini2.Focus()
	End Sub

	Private Sub Cb_Hari_Ini2_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_Hari_Ini2.CheckedChanged
		If Cb_Hari_Ini2.Checked = True Then
			Cb_Tanggal2.Checked = False
			Btn_Cari2.PerformClick()
		End If
	End Sub

	Private Sub Cb_Tanggal2_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_Tanggal2.CheckedChanged
		If Cb_Tanggal2.Checked Then
			'Cmb_Tanggal2.Enabled = True : Tgl_1_2.Enabled = True : Tgl_2_2.Enabled = True
			Cb_Hari_Ini2.Checked = False
			Cmb_Tanggal2.Enabled = True
		Else
			Cmb_Tanggal2.Enabled = False
		End If
		Tgl_1_2.Enabled = False : Tgl_2_2.Enabled = False
		Tgl_1_2.Value = Now.Date : Tgl_2_2.Value = Now.Date
		Cmb_Tanggal2.SelectedIndex = -1
	End Sub

	Private Sub Cb_Param_Lain2_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_Param_Lain2.CheckedChanged
		If Cb_Param_Lain2.Checked Then
			Cmb_Param_Lain2.Enabled = True : Txt_Param_Lain2.Enabled = True
		Else
			Cmb_Param_Lain2.Enabled = False : Txt_Param_Lain2.Enabled = False
			Cmb_Param_Lain2.SelectedIndex = -1 : Txt_Param_Lain2.Text = ""
		End If

	End Sub

	Private Sub Cb_Hari_Ini2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cb_Hari_Ini2.KeyPress
		If e.KeyChar = Chr(13) Then Cb_Tanggal2.Focus()
	End Sub

	Private Sub Cb_Tanggal2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cb_Tanggal2.KeyPress
		If e.KeyChar = Chr(13) Then
			If Cb_Tanggal2.Checked Then
				Cmb_Tanggal2.DroppedDown = True
				Cmb_Tanggal2.Focus()
			Else
				Cb_Param_Lain2.Focus()
			End If

		End If
	End Sub

	Private Sub Cmb_Tanggal2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Tanggal2.KeyPress
		If e.KeyChar = Chr(13) Then Tgl_1_2.Focus()
	End Sub

	Private Sub Tgl_1_2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl_1_2.KeyPress
		If e.KeyChar = Chr(13) Then Tgl_2_2.Focus()
	End Sub

	Private Sub Tgl_2_2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl_2_2.KeyPress
		If e.KeyChar = Chr(13) Then Cb_Param_Lain2.Focus()
	End Sub

	Private Sub Cmb_Param_Lain2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Param_Lain2.KeyPress
		If e.KeyChar = Chr(13) Then Txt_Param_Lain2.Focus()
	End Sub

	Private Sub Txt_Param_Lain2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Param_Lain2.KeyPress
		If e.KeyChar = Chr(13) Then Btn_Cari2.Focus()
	End Sub

#End Region

	Private Sub ListView_MouseLeave(sender As Object, e As EventArgs)
		DirectCast(sender, ListView).Cursor = Cursors.Default
	End Sub

	Private Sub ListView_MouseMove(sender As Object, e As MouseEventArgs)

		Dim lv As ListView = DirectCast(sender, ListView)

		Dim info As ListViewHitTestInfo = lv.HitTest(e.Location)

		If info.Item IsNot Nothing Then
			lv.Cursor = Cursors.Hand
		Else
			lv.Cursor = Cursors.Default
		End If

	End Sub

	Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
		If Lv_Process_Data.Items.Count = 0 Then
			e.Cancel = True
			Exit Sub
		End If

		'=========================================================
		'=     CEK APAKAH MOUSE BERADA DI ATAS ROWS LISTVIEW     =
		'=========================================================
		Dim mousePos As Point = Lv_Process_Data.PointToClient(Cursor.Position)
		Dim info As ListViewHitTestInfo = Lv_Process_Data.HitTest(mousePos)

		If info.Item Is Nothing Then
			e.Cancel = True
			Exit Sub
		End If

		Lv_Process_Data.FocusedItem = info.Item
		info.Item.Selected = True
	End Sub

	Private Sub ContextMenuStrip2_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip2.Opening
		If Lv_Product_Data.Items.Count = 0 Then
			e.Cancel = True
			Exit Sub
		End If

		'=========================================================
		'=     CEK APAKAH MOUSE BERADA DI ATAS ROWS LISTVIEW     =
		'=========================================================
		Dim mousePos As Point = Lv_Product_Data.PointToClient(Cursor.Position)
		Dim info As ListViewHitTestInfo = Lv_Product_Data.HitTest(mousePos)

		If info.Item Is Nothing Then
			e.Cancel = True
			Exit Sub
		End If

		Lv_Product_Data.FocusedItem = info.Item
		info.Item.Selected = True
	End Sub

	Protected Overrides Sub WndProc(ByRef m As Message)
		If m.Msg = &HA3 Then
			Return
		End If

		MyBase.WndProc(m)
	End Sub

	Private Sub TabControl3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl3.SelectedIndexChanged
		If TabControl3.SelectedIndex = 0 Then
			Panel_Warna_Detail_Process.Visible = False
		ElseIf TabControl3.SelectedIndex = 1 Then
			Panel_Warna_Detail_Process.Visible = True
		End If
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

	Private Sub Lv_Product_Data_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Product_Data.MouseMove, Lv_Product_User_Approve.MouseMove, Lv_Product_Detail_Barang.MouseMove, Lv_Product_Detail_Barcode.MouseMove,
			Lv_Process_Data.MouseMove, Lv_Process_User_Approve.MouseMove, Lv_Process_Detail_Barang.MouseMove, Lv_Process_Detail_Barcode.MouseMove
		HandleListViewHover(sender, e)
	End Sub

End Class