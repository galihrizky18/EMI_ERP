Public Class N_EMI_SD_List_Purchase_Requisition_Department_Barang_Lain

	Public lokasi_asal As String

	Private PageSize As Integer = 20
	Dim CurrentPage As Integer = 1

	Private lastHoverItem As ListViewItem = Nothing
	Private originalItemColor As Color

	Dim Item_NoPr As Integer = 0
	Dim Item_Tanggal As Integer = 1
	Dim Item_KdKategoriGudang As Integer = 2
	Dim Item_KodeBarang As Integer = 3
	Dim Item_NmBarang As Integer = 4
	Dim Item_JumlahPR As Integer = 5
	Dim Item_JumlahPengeluaran As Integer = 6
	Dim Item_JumlahKeepStock As Integer = 7
	Dim Item_JumlahTransfer As Integer = 8
	Dim Item_Sisa As Integer = 9
	Dim Item_SatuanDisplay As Integer = 10
	Dim Item_KdSo As Integer = 11
	Dim Item_UrutPR As Integer = 12
	Dim Item_MetPengeluaranStock As Integer = 13
	Dim Item_JenisKemasan As Integer = 14
	Dim Item_Satuan As Integer = 15

	Dim ArrFilter As New List(Of (ValueCmb As String, Sql As String)) From {
		(OpsiSeluruh, OpsiSeluruh),
		("No PR", "a.No_Faktur"),
		("Kode Stock Owner", "a.Kode_Kategori_Gudang"),
		("Kode Barang", "b.Kode_Barang"),
		("Nama Barang", "c.Nama")
	}

	Private Sub N_EMI_SD_List_Purchase_Requisition_Department_Barang_Lain_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub N_EMI_SD_List_Purchase_Requisition_Department_Barang_Lain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		EnableDoubleBuffer(Lv_Data_PR)

		'asda
		Lv_Data_PR.Columns.Clear()
		Lv_Data_PR.Columns.Add("No PR", 130, HorizontalAlignment.Left) '0
		Lv_Data_PR.Columns.Add("Tanggal", 100, HorizontalAlignment.Center) '1
		Lv_Data_PR.Columns.Add("Kode Stock Owner", 150, HorizontalAlignment.Left) '2
		Lv_Data_PR.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left) '3
		Lv_Data_PR.Columns.Add("Nama Barang", 200, HorizontalAlignment.Left) '4
		Lv_Data_PR.Columns.Add("Jumlah PR", 130, HorizontalAlignment.Right) '5
		Lv_Data_PR.Columns.Add("Jumlah Pengeluaran", 130, HorizontalAlignment.Right) '6
		Lv_Data_PR.Columns.Add("Jumlah Keep Stock", 130, HorizontalAlignment.Right) '7
		Lv_Data_PR.Columns.Add("Jumlah Transfer", 130, HorizontalAlignment.Right) '8
		Lv_Data_PR.Columns.Add("Sisa", 130, HorizontalAlignment.Right) '9
		Lv_Data_PR.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '10
		'Hide
		Lv_Data_PR.Columns.Add("KdSO", 0, HorizontalAlignment.Left) '11
		Lv_Data_PR.Columns.Add("UrutPR", 0, HorizontalAlignment.Left) '12
		Lv_Data_PR.Columns.Add("MetodePengeluaranStock", 0, HorizontalAlignment.Left) '13
		Lv_Data_PR.Columns.Add("JenisKemasan", 0, HorizontalAlignment.Left) '14
		Lv_Data_PR.Columns.Add("SatuanHitung", 0, HorizontalAlignment.Left) '15
		Lv_Data_PR.View = View.Details

		Cmb_Filter.Items.Clear()
		For Each items In ArrFilter
			Cmb_Filter.Items.Add(items.ValueCmb)
		Next

		Kosong()

	End Sub

	Public Sub Kosong()

		Cmb_Filter.SelectedIndex = 0
		'Txt_Value_Filter.Text = OpsiSeluruh

		CurrentPage = 1

		Lv_Data_PR.Items.Clear()

		If lokasi_asal.Trim.Length = 0 Then
			MessageBox.Show("Lokasi Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.Close()
			Exit Sub
		End If

		LoadData()

	End Sub

	Private Sub LoadData()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim Filter As String = ""
			If Cmb_Filter.SelectedIndex > 0 Then
				Filter &= $"and {ArrFilter(Cmb_Filter.SelectedIndex).Sql} like '%{Txt_Value_Filter.Text.Trim}%' "
			End If

			Dim Offset As Integer = (CurrentPage - 1) * PageSize
			Dim rowCount As Integer = 0

			Lv_Data_PR.Items.Clear()
			SQL = $"
				;with Akses_User as (
									   SELECT a.Kode_Perusahaan,
											  b.Kode_Stock_Owner_Gudang,
											  a.User_ID,
											  d.Id_Sub_Kategori_Jenis,
											  d.Id_Kategori_Jenis
									   FROM N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain a with (nolock)
											INNER JOIN N_EMI_Master_Kategori_Gudang_Barang_Lain b with (nolock)
													   ON a.Kode_Perusahaan = b.Kode_Perusahaan
														   AND a.Id_Kategori_Gudang = b.Urut_Oto
											INNER JOIN N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain c with (nolock)
													   ON b.Kode_Perusahaan = c.Kode_Perusahaan
														   AND c.Id_Kategori_Gudang = b.Urut_Oto
											INNER JOIN N_EMI_Master_Sub_Kategori_Jenis d with (nolock)
													   ON c.Kode_Perusahaan = d.Kode_Perusahaan
														   AND c.Id_Sub_Kategori_Jenis = d.Id_Sub_Kategori_Jenis
									   WHERE a.Status IS NULL
										 AND b.Status IS NULL
										 AND c.Status IS NULL

								   ),
					 Pengeluaran_Stock_Agg as (
									   select a.Kode_Perusahaan, b.Kode_Barang, b.Urut_PR_Dept,
											  sum(c.Jumlah) as Jumlah, sum(c.Jumlah_Bags) as Jumlah_Bags
									   from EMI_Pengeluaran_Stock_parent_barang_lain a with (nolock)
											inner join EMI_Pengeluaran_Stock_Barang_Lain b with (nolock)
													   on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
											inner join EMI_Pengeluaran_Stock_Det_Barang_Lain c with (nolock)
													   on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and
														  b.Urut_Oto = c.Urut_TF
									   where a.Status is null
										 and c.Selesai = 'Y'
									   group by a.Kode_Perusahaan, b.Kode_Barang, b.Urut_PR_Dept
											  ),
					 Keep_Stock_Agg as (
									   SELECT zr.Kode_Perusahaan,
											  zr.Urut_Departement,
											  isnull(SUM(zr.Jumlah), 0) AS Total_Keep,
											  isnull(sum(zr.Jmlh_Transfer), 0) as Total_Transfer
									   FROM N_EMI_Keep_Stock_Barang_Lain_Departement zr with (nolock)
									   WHERE zr.Status IS NULL
										 AND zr.Flag_Selesai_Pengeluaran_Barang IS NULL
									   GROUP BY zr.Kode_Perusahaan,
												zr.Urut_Departement
											  )
				select
					   a.No_Faktur as No_PR,
					   b.Keterangan,
					   a.Tanggal, a.Jam, a.UserId, b.No_Urut,
					   ISNULL(a.Kode_Kategori_Gudang, '-') AS Kode_Stock_Owner,
					   b.Kode_Stock_Owner as Kd_SO_Barang_Lain,
					   b.Kode_Barang,
					   c.Nama as Nama_Barang,
					   b.Jumlah as Jumlah_PR,
					   isnull(g.Jumlah, 0) as Jumlah_Pengeluaran,
					   isnull(h.Total_Keep, 0) as Jumlah_KeepStock,
					   isnull(h.Total_Transfer, 0) as Jumlah_Transfer,
					   (
						   isnull(b.Jumlah, 0) - (isnull(g.Jumlah, 0) + isnull(h.Total_Keep, 0) + isnull(h.Total_Transfer, 0))
					   ) as Sisa,
					   b.Satuan, i.Satuan as Satuan_Display, c.Metode_Pengeluaran_Stok, c.Jenis_Kemasan
				from N_EMI_Purchase_Requisition_Barang_Lain_Departement a
					inner join N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
					inner join Barang_Lain c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang
					inner join View_Kategori_Turunan d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Sub_Kategori_Jenis_3 = d.Id_Sub_Kategori_Jenis_3
					inner join Akses_User e on d.Kode_Perusahaan = e.Kode_Perusahaan and d.Id_Kategori_Jenis = e.Id_Kategori_Jenis and d.Id_Sub_Kategori_Jenis = e.Id_Sub_Kategori_Jenis
					inner join N_EMI_Master_Kategori_Gudang_Barang_Lain f on a.Kode_Perusahaan = f.Kode_Perusahaan and a.Kode_Kategori_Gudang= f.Kode_Kategori_Gudang
																				 and e.Kode_Stock_Owner_Gudang = f.Kode_Stock_Owner_Gudang
					inner join Barang_Detail_Satuan_Lain i on b.Kode_Perusahaan =i.Kode_Perusahaan and b.Kode_Barang = i.Kode_barang
					left join Pengeluaran_Stock_Agg g on b.Kode_Perusahaan = g.Kode_Perusahaan and b.Kode_Barang = g.Kode_Barang and b.No_Urut = g.Urut_PR_Dept
					left join Keep_Stock_Agg h on b.Kode_Perusahaan = h.Kode_Perusahaan and b.No_Urut = h.Urut_Departement
				where a.Status is null
				and i.flag_tampil_display = 'Y'
				and a.Kode_Perusahaan = '{KodePerusahaan}'
				and e.User_ID = '{UserID}'
				and f.Kode_Stock_Owner_Gudang = '{lokasi_asal}'
				{Filter}
				and (isnull(b.Jumlah, 0) - (isnull(g.Jumlah, 0) + isnull(h.Total_Keep, 0) + isnull(h.Total_Transfer, 0))) > 0
				order by a.Tanggal, a.Jam, a.No_Faktur, b.Kode_Barang
				OFFSET {Offset} ROWS FETCH NEXT {PageSize} ROWS ONLY
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						rowCount += 1

						Dim Lv As ListViewItem
						Lv = Lv_Data_PR.Items.Add(Dr("No_PR"))
						Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
						Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
						Lv.SubItems.Add(Dr("Kode_Barang"))
						Lv.SubItems.Add(Dr("Nama_Barang"))
						Lv.SubItems.Add(Format(Dr("Jumlah_PR"), "N0"))
						Lv.SubItems.Add(Format(Dr("Jumlah_Pengeluaran"), "N0"))
						Lv.SubItems.Add(Format(Dr("Jumlah_KeepStock"), "N0"))
						Lv.SubItems.Add(Format(Dr("Jumlah_Transfer"), "N0"))
						Lv.SubItems.Add(Format(Dr("Sisa"), "N0"))
						Lv.SubItems.Add(Dr("Satuan_Display"))
						'Hide
						Lv.SubItems.Add(Dr("Kd_SO_Barang_Lain"))
						Lv.SubItems.Add(Dr("No_Urut"))
						Lv.SubItems.Add(Dr("Metode_Pengeluaran_Stok"))
						Lv.SubItems.Add(Dr("Jenis_Kemasan"))
						Lv.SubItems.Add(Dr("Satuan"))

					Loop While Dr.Read
				End If
			End Using

			Txt_Pages.Text = CurrentPage.ToString()
			Btn_First.Enabled = (CurrentPage > 1)
			Btn_Prev.Enabled = (CurrentPage > 1)
			Btn_Next.Enabled = (rowCount = PageSize)

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

	Private Sub Lv_Data_PR_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data_PR.DoubleClick
		If Lv_Data_PR.Items.Count = 0 Or Lv_Data_PR.FocusedItem Is Nothing Then
			MessageBox.Show("Tidak Ada Data yang Dapat Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End If

		Dim KdBarang As String = Lv_Data_PR.FocusedItem.SubItems(Item_KodeBarang).Text
		Dim KdSo As String = Lv_Data_PR.FocusedItem.SubItems(Item_KdKategoriGudang).Text
		Dim NmBarang As String = Lv_Data_PR.FocusedItem.SubItems(Item_NmBarang).Text
		Dim Satuan As String = Lv_Data_PR.FocusedItem.SubItems(Item_Satuan).Text
		Dim SatuanDisplay As String = Lv_Data_PR.FocusedItem.SubItems(Item_SatuanDisplay).Text
		Dim MetPotongStock As String = Lv_Data_PR.FocusedItem.SubItems(Item_MetPengeluaranStock).Text
		Dim JnsKemasan As String = Lv_Data_PR.FocusedItem.SubItems(Item_JenisKemasan).Text
		Dim UrutPrDept As String = Lv_Data_PR.FocusedItem.SubItems(Item_UrutPR).Text
		Dim JumlahPermintaan As String = Lv_Data_PR.FocusedItem.SubItems(Item_Sisa).Text
		Dim JumlahStock As Double = 0
		Dim JumlahBags As Double = 0

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'==============================
			'=     GET STOCK REALTIME     =
			'==============================

			SQL = $"
				select Kode_Barang, Good_Stock, Jumlah_Bags
				from Barang_Lain
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Kode_Stock_Owner = '{lokasi_asal}'
				and Kode_Barang = '{KdBarang}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						JumlahStock = Val(HilangkanTanda(Dr("Good_Stock")))
						JumlahBags = Val(HilangkanTanda(Dr("Jumlah_Bags")))
					Loop While Dr.Read
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Kode Barang {KdBarang} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
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

		EMI_Pengeluaran_Stock_Barang_Lain.TxtKd_Barang.Text = KdBarang
		EMI_Pengeluaran_Stock_Barang_Lain.Lv_DetBarang.Visible = False
		EMI_Pengeluaran_Stock_Barang_Lain.Txt_SO.Text = lokasi_asal
		EMI_Pengeluaran_Stock_Barang_Lain.TxtNm_Barang.Text = NmBarang
		EMI_Pengeluaran_Stock_Barang_Lain.TxtSatuan.Text = SatuanDisplay
		EMI_Pengeluaran_Stock_Barang_Lain.TxtSatuanKecil.Text = Satuan
		EMI_Pengeluaran_Stock_Barang_Lain.TxtStock.Text = JumlahStock
		EMI_Pengeluaran_Stock_Barang_Lain.TxtBags.Text = JumlahBags
		EMI_Pengeluaran_Stock_Barang_Lain.TxtMetPotStok.Text = MetPotongStock
		EMI_Pengeluaran_Stock_Barang_Lain.TxtJenisBags.Text = JnsKemasan
		EMI_Pengeluaran_Stock_Barang_Lain.Txt_OtoMaterial_req.Text = UrutPrDept

		EMI_Pengeluaran_Stock_Barang_Lain.Txt_Permintaan_Display.Text = $"{JumlahPermintaan} {SatuanDisplay}"
		EMI_Pengeluaran_Stock_Barang_Lain.Txt_Permintaan.Text = JumlahPermintaan

		EMI_Pengeluaran_Stock_Barang_Lain.TxtStockDisplay.Text = Format(Val(HilangkanTanda(JumlahStock)), "N2") + " " + Satuan

		EMI_Pengeluaran_Stock_Barang_Lain.Btn_Insert_Click(sender, e)

		Me.Close()
	End Sub

	Private Sub Btn_cari_Click(sender As Object, e As EventArgs) Handles Btn_cari.Click
		If Cmb_Filter.SelectedIndex > 0 Then
			If Txt_Value_Filter.Text.Trim.Length = 0 Then
				MessageBox.Show("Value Filter Harus Diisi Terlebih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Txt_Value_Filter.Focus()
				Exit Sub
			End If
		End If

		CurrentPage = 1

		LoadData()
	End Sub

	Private Sub Cmb_Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter.SelectedIndexChanged
		If Cmb_Filter.Items.Count = 0 Then Exit Sub

		If Cmb_Filter.SelectedIndex = 0 Then
			Txt_Value_Filter.Text = OpsiSeluruh
			Txt_Value_Filter.Enabled = False
			Txt_Value_Filter.BackColor = Color.FromArgb(235, 235, 235)
		Else
			Txt_Value_Filter.Text = ""
			Txt_Value_Filter.Enabled = True
			Txt_Value_Filter.BackColor = Color.White
		End If

	End Sub

	Private Sub Btn_First_Click(sender As Object, e As EventArgs) Handles Btn_First.Click
		CurrentPage = 1
		LoadData()
	End Sub

	Private Sub Btn_Prev_Click(sender As Object, e As EventArgs) Handles Btn_Prev.Click
		If CurrentPage > 1 Then
			CurrentPage -= 1
			LoadData()
		End If
	End Sub

	Private Sub Btn_Next_Click(sender As Object, e As EventArgs) Handles Btn_Next.Click
		CurrentPage += 1
		LoadData()
	End Sub

	Private Sub Lv_Data_PR_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Data_PR.MouseMove
		HandleListViewHover(sender, e)
	End Sub

	'==============================================================================================================================
	'=     UTILITY
	'==============================================================================================================================

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

End Class