Public Class N_EMI_Transaksi_Budget_Planning_Backup

	Dim DataDepartment As New Dictionary(Of String, String)
	Dim DataLayer1 As New Dictionary(Of Integer, String)
	Dim DataLayer3 As New Dictionary(Of Integer, List(Of DetailDataLayer3))
	Dim DataBudgetPlanning As New Dictionary(Of Integer, List(Of DetailDataBudgetPlanning))

	Dim DataUpdate As New List(Of DetailDataUpdate)

	Private lastIndex As Integer = -1
	Private originalColor As Color

	Dim DataBulan As New List(Of (ValueComboBox As String, Sql As String, NamaBulan As String)) From {
		("01", "1", "Januari"),
		("02", "2", "Februari"),
		("03", "3", "Maret"),
		("04", "4", "April"),
		("05", "5", "Mei"),
		("06", "6", "Juni"),
		("07", "7", "Juli"),
		("08", "8", "Agustus"),
		("09", "9", "September"),
		("10", "10", "Oktober"),
		("11", "11", "November"),
		("12", "12", "Desember")
	}

	Dim CurrentMonth As Integer = 0
	Dim CurrentYear As Integer = 0

	Dim StartKolomDinamis As Integer = 2

	Dim Prefix_KolomJumlah As String = "Jumlah"
	Dim PersentasePenggelapanWarnaCellInput As Double = 23

	Private Sub N_EMI_Transaksi_Buget_Department_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub N_EMI_Transaksi_Buget_Department_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		Me.Dock = DockStyle.Fill

		EnableDoubleBufferDGV(Dgv_Data)

		LoadInitialData()

		Kosong()
	End Sub

	Private Sub Kosong()
		Cmb_Department.SelectedIndex = -1
		Cmb_Bulan_Awal.SelectedIndex = -1
		Cmb_Bulan_Akhir.SelectedIndex = -1

		Cmb_Kategori.Enabled = False
		Cmb_Bulan_Awal.Enabled = False
		Cmb_Bulan_Akhir.Enabled = False

		DataUpdate.Clear()

		If Cmb_Tahun.Items.Count > 0 Then
			Cmb_Tahun.Text = CurrentYear
			Cmb_Tahun_SelectedIndexChanged(Cmb_Tahun, EventArgs.Empty)
		End If

		DataBudgetPlanning.Clear()
		Cmb_Department.Focus()

		If Dgv_Data.Columns.Count > 2 Then
			For idx As Integer = Dgv_Data.Columns.Count - 1 To 2 Step -1
				Dgv_Data.Columns.RemoveAt(idx)
			Next
		End If
		Dgv_Data.Rows.Clear()

	End Sub

	Private Sub LoadInitialData()

		get_jam()
		CurrentMonth = tgl_skg.Month
		CurrentYear = tgl_skg.Year

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'================================
			'=     LOAD DATA DEPARTMENT     =
			'================================
			Cmb_Department.Items.Clear()
			SQL = $"
				select Kode_Binding, Keterangan
				from N_EMI_Binding_Department
				where Status is null
				and Kode_Perusahaan = '{KodePerusahaan}'
				order by Keterangan
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim key As String = Dr("Kode_Binding").ToString.Trim
						Dim value As String = Dr("Keterangan").ToString.Trim
						DataDepartment.Add(key, value)
						Cmb_Department.Items.Add(value)
					Loop While Dr.Read
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Terjadi Kesalahan, Data Binding Departement Tidak Ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'===========================
			'=     LOAD DATA LAYER     =
			'===========================
			Cmb_Kategori.Items.Clear()
			SQL = $"
				select a.Id_Kategori_Jenis, a.Kode_Kategori_Jenis, a.Keterangan as Keterangan_Kategori_Jenis,
					c.Id_Sub_Kategori_Jenis_1, c.Kode_Sub_Kategori_Jenis_1, c.Keterangan as Keterangan_Layer_3
				from N_EMI_Master_Kategori_Jenis a
					inner join N_EMI_Master_Sub_Kategori_Jenis b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Jenis = b.Id_Kategori_Jenis
					inner join N_EMI_Master_Sub_Kategori_Jenis_1 c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Sub_Kategori_Jenis = c.Id_Sub_Kategori_Jenis
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.Flag_Aktif = 'Y'
				and c.Flag_Is_Budget = 'Y'
				order by a.Kode_Kategori_Jenis, b.Kode_Sub_Kategori_Jenis, c.Kode_Sub_Kategori_Jenis_1
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim IDLayer1 As Integer = Dr("Id_Kategori_Jenis")
						Dim ValueLayer1 As String = Dr("Keterangan_Kategori_Jenis")

						Dim IDLayer3 As Integer = Dr("Id_Sub_Kategori_Jenis_1").ToString.Trim
						Dim ValueLayer3 As String = Dr("Keterangan_Layer_3").ToString.Trim

						If Not DataLayer1.ContainsKey(IDLayer1) Then
							DataLayer1.Add(IDLayer1, ValueLayer1)
							Cmb_Kategori.Items.Add(ValueLayer1)
						End If

						Dim DetailLayer3 As New DetailDataLayer3 With {
							.keteranganKategoriLayer1 = ValueLayer1,
							.IDKategoriLayer1 = IDLayer1,
							.keteranganKategoriLayer3 = ValueLayer3
						}

						If Not DataLayer3.ContainsKey(IDLayer3) Then
							Dim ListDetailLayer3 As New List(Of DetailDataLayer3)
							ListDetailLayer3.Add(DetailLayer3)
							DataLayer3.Add(IDLayer3, ListDetailLayer3)
						Else
							DataLayer3(IDLayer3).Add(DetailLayer3)
						End If

					Loop While Dr.Read
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Terjadi Kesalahan, Data Layering Tidak Ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

		Try

			Cmb_Tahun.Items.Clear()
			Dim tahunSekarang As Integer = tgl_skg.Year

			' Looping dari -1 (tahun lalu) sampai 1 (tahun depan)
			For i As Integer = -1 To 1
				Dim Tahun As String = (tahunSekarang + i).ToString()
				Cmb_Tahun.Items.Add(Tahun)
			Next

			Cmb_Bulan_Awal.Items.Clear() : Cmb_Bulan_Akhir.Items.Clear()
			Dim filterBulan As New Dictionary(Of String, String)()
			For i As Integer = 0 To DataBulan.Count - 1
				Dim keyBulan As String = i + 1
				Dim namaBulan As String = DataBulan(i).NamaBulan

				filterBulan.Add(keyBulan, namaBulan)
			Next

			Cmb_Bulan_Awal.DataSource = New BindingSource(filterBulan, Nothing)
			Cmb_Bulan_Awal.DisplayMember = "Value"
			Cmb_Bulan_Awal.ValueMember = "Key"

			Cmb_Bulan_Akhir.DataSource = New BindingSource(filterBulan, Nothing)
			Cmb_Bulan_Akhir.DisplayMember = "Value"
			Cmb_Bulan_Akhir.ValueMember = "Key"

			'==========================================
			'=     LOAD INIIAL KOLOM DATAGRIDVIEW     =
			'==========================================
			Dgv_Data.SuspendLayout()
			Dgv_Data.Columns.Clear()

			Dim colID As New DataGridViewTextBoxColumn() With {.Name = "ID_Sub_Kategori_Jenis_1", .HeaderText = "ID", .Frozen = True, .[ReadOnly] = True, .Visible = False}
			Dgv_Data.Columns.Add(colID)

			Dim colKategori As New DataGridViewTextBoxColumn() With {.Name = "Layer_3", .HeaderText = "Kategori Layer 3", .Width = 200, .Frozen = True, .[ReadOnly] = True}
			Dgv_Data.Columns.Add(colKategori)

			Dgv_Data.ResumeLayout()
		Catch ex As Exception
			MessageBox.Show(ex.Message)
		End Try

	End Sub

	Private Sub Btn_Get_Data_Click(sender As Object, e As EventArgs) Handles Btn_Get_Data.Click
		If Cmb_Department.SelectedIndex = -1 Then
			MessageBox.Show("Departement Harus Diisi Terlabih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Department.DroppedDown = True
			Cmb_Department.Focus()
			Exit Sub
		ElseIf Cmb_Kategori.SelectedIndex = -1 Then
			MessageBox.Show("Kategori Harus Diisi Terlabih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Kategori.DroppedDown = True
			Cmb_Kategori.Focus()
			Exit Sub
		ElseIf Cmb_Tahun.SelectedIndex = -1 Then
			MessageBox.Show("Tahun Harus Diisi Terlabih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Tahun.DroppedDown = True
			Cmb_Tahun.Focus()
			Exit Sub
		ElseIf Cmb_Bulan_Awal.SelectedIndex = -1 Then
			MessageBox.Show("Bulan Awal Harus Diisi Terlabih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Bulan_Awal.DroppedDown = True
			Cmb_Bulan_Awal.Focus()
			Exit Sub
		ElseIf Cmb_Bulan_Akhir.SelectedIndex = -1 Then
			MessageBox.Show("Bulan Akhir Harus Diisi Terlabih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Bulan_Akhir.DroppedDown = True
			Cmb_Bulan_Akhir.Focus()
			Exit Sub
		End If

		If Val(Cmb_Bulan_Akhir.SelectedValue) < Val(Cmb_Bulan_Awal.SelectedValue) Then
			MessageBox.Show("Bulan Akhir tidak boleh lebih kecil dari Bulan Awal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
			Cmb_Bulan_Akhir.SelectedValue = Cmb_Bulan_Awal.SelectedValue
			Exit Sub
		End If

		GetDataBudget()
	End Sub

	Private Sub Btn_Cetak_Laporan_Click(sender As Object, e As EventArgs) Handles Btn_Cetak_Laporan.Click
		If Cmb_Department.SelectedIndex = -1 Then
			MessageBox.Show("Departement Harus Diisi Terlabih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Department.DroppedDown = True
			Cmb_Department.Focus()
			Exit Sub
		ElseIf Cmb_Kategori.SelectedIndex = -1 Then
			MessageBox.Show("Kategori Harus Diisi Terlabih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Kategori.DroppedDown = True
			Cmb_Kategori.Focus()
			Exit Sub
		ElseIf Cmb_Tahun.SelectedIndex = -1 Then
			MessageBox.Show("Tahun Harus Diisi Terlabih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Tahun.DroppedDown = True
			Cmb_Tahun.Focus()
			Exit Sub
		ElseIf Cmb_Bulan_Awal.SelectedIndex = -1 Then
			MessageBox.Show("Bulan Awal Harus Diisi Terlabih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Bulan_Awal.DroppedDown = True
			Cmb_Bulan_Awal.Focus()
			Exit Sub
		ElseIf Cmb_Bulan_Akhir.SelectedIndex = -1 Then
			MessageBox.Show("Bulan Akhir Harus Diisi Terlabih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Bulan_Akhir.DroppedDown = True
			Cmb_Bulan_Akhir.Focus()
			Exit Sub
		End If

		If Val(Cmb_Bulan_Akhir.SelectedValue) < Val(Cmb_Bulan_Awal.SelectedValue) Then
			MessageBox.Show("Bulan Akhir tidak boleh lebih kecil dari Bulan Awal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
			Cmb_Bulan_Akhir.SelectedValue = Cmb_Bulan_Awal.SelectedValue
			Exit Sub
		End If

		If Dgv_Data.Rows.Count = 0 Then
			MessageBox.Show("Lakukan Get Data Terlebih Dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
			Exit Sub
		End If

		'CetakLaporan()
		CetakLaporanRealtime()

	End Sub

	Private Sub GetDataBudget()

		Dim SelectedindexCMB As Integer = Cmb_Department.SelectedIndex
		Dim SelectedDepartment_KodeBinding As String = DataDepartment.Keys.ToList()(SelectedindexCMB)
		Dim SelectedIDLayer1 As Integer = DataLayer1.Keys.ToList()(Cmb_Kategori.SelectedIndex)
		Dim SelectedTahun As Integer = Cmb_Tahun.Text.Trim
		Dim SelectedBulanAwal As Integer = Val(HilangkanTanda(Cmb_Bulan_Awal.SelectedValue))
		Dim SelectedBulanAkhir As Integer = Val(HilangkanTanda(Cmb_Bulan_Akhir.SelectedValue))

		'========================================
		'=     GET DATA BESAR DARI DATABASE     =
		'========================================
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'============================================================
			'=     GET ALL DATA BERDASARKAN DEPARTMENT YANG DIPILIH     =
			'============================================================
			DataBudgetPlanning.Clear() : DataUpdate.Clear()

			SQL = $"
				;with
					-- 1. Base PRM: Dibuat seringkas mungkin dengan filter perusahaan agresif
					DataPRM_Base as (
										select a.Kode_Perusahaan,
											   ISNULL(YEAR(a.Tanggal), 0) as Tahun,
											   ISNULL(MONTH(a.Tanggal), 0) as Bulan,
											   b.No_Urut,
											   d.ID_Kategori_Jenis,
											   d.Id_Sub_Kategori_Jenis_1,
											   a.Kode_Kategori_Gudang,
											   b.jumlah as JumlahPR
										from N_EMI_Purchase_Requisition_Barang_Lain_Departement a with (nolock)
											 inner join N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail b with (nolock)
														on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
											 inner join barang_lain c with (nolock)
														on b.Kode_Perusahaan = c.Kode_Perusahaan and
														   b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang
											 inner join View_Kategori_Turunan d with (nolock)
														on b.Kode_Perusahaan = d.Kode_Perusahaan and
														   c.Id_Sub_Kategori_Jenis_3 = d.Id_Sub_Kategori_Jenis_3
										where a.Status is null
										  and a.Kode_Perusahaan = '{KodePerusahaan}'
									),

					-- 2. Ambil Agregasi PRM langsung dari Base (Tidak scan ulang tabel fisik)
					DataPRM_Agg as (
										select Kode_Perusahaan,
											   Tahun, Bulan,
											   Kode_Kategori_Gudang,
											   ID_Kategori_Jenis,
											   Id_Sub_Kategori_Jenis_1,
											   sum(JumlahPR) as TotalJumlahPR
										from DataPRM_Base
										group by Kode_Perusahaan, Tahun, Bulan, Kode_Kategori_Gudang, ID_Kategori_Jenis,
												 Id_Sub_Kategori_Jenis_1
									),

					-- 3. Agregasi Transfer Stock
					DataTF_Agg as (
										select z.Kode_Perusahaan,
											   l.Kode_Kategori_Gudang,
											   ISNULL(YEAR(z.Tanggal), 0) as Tahun,
											   ISNULL(MONTH(z.Tanggal), 0) as Bulan,
											   l.ID_Kategori_Jenis,
											   l.Id_Sub_Kategori_Jenis_1,
											   sum(k.Jumlah) as TotalJumlahTF
										from N_EMI_Transfer_Stock_Barang_Lain z with (nolock)
											 inner join N_EMI_Transfer_Stock_Barang_Lain_Detail x with (nolock)
														on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
											 inner join N_EMI_Transfer_Stock_Barang_Lain_Det y with (nolock)
														on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and
														   x.Urut_Oto = y.Urut_TF
											 inner join N_EMI_Transfer_Stock_Barang_Lain_Det2 k with (nolock)
														on y.Kode_Perusahaan = k.Kode_Perusahaan and y.No_Faktur = k.No_Faktur and
														   y.Urut_Oto = k.Urut_Det
											 inner join DataPRM_Base l
														on x.Kode_Perusahaan = l.Kode_Perusahaan and x.Urut_PR_Dept = l.No_Urut
										where z.Status is null
										  and y.Selesai = 'Y'
										  and z.Kode_Perusahaan = '{KodePerusahaan}'
										group by z.Kode_Perusahaan, l.Kode_Kategori_Gudang, z.Tanggal, l.ID_Kategori_Jenis,
												 l.Id_Sub_Kategori_Jenis_1
									),

					-- 4. Agregasi Pengeluaran Stock
					DataPengeluaran_Agg as (
										select a.Kode_Perusahaan,
											   l.Kode_Kategori_Gudang,
											   ISNULL(YEAR(a.Tanggal), 0) as Tahun,
											   ISNULL(MONTH(a.Tanggal), 0) as Bulan,
											   l.ID_Kategori_Jenis,
											   l.Id_Sub_Kategori_Jenis_1,
											   sum(c.Jumlah) as TotalJumlahPengeluaran
										from EMI_Pengeluaran_Stock_parent_barang_lain a with (nolock)
											 inner join EMI_Pengeluaran_Stock_barang_lain b with (nolock)
														on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
											 inner join EMI_Pengeluaran_Stock_Det_Barang_Lain c with (nolock)
														on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and
														   b.Urut_Oto = c.Urut_TF
											 inner join DataPRM_Base l
														on b.Kode_Perusahaan = l.Kode_Perusahaan and b.Urut_PR_Dept = l.No_Urut
										where a.Status is null
										  and a.Kode_Perusahaan = '{KodePerusahaan}'
										  and c.Selesai = 'Y'
										group by a.Kode_Perusahaan, a.Tanggal, l.Kode_Kategori_Gudang, l.ID_Kategori_Jenis,
												 l.Id_Sub_Kategori_Jenis_1
									),

					-- 5. Satukan TF dan Pengeluaran di level data mentah sebelum join ke Master Binding (Jauh Lebih Cepat!)
					DataTF_Union as (
										select Kode_Perusahaan, Tahun, Bulan, Kode_Kategori_Gudang, ID_Kategori_Jenis,
											   Id_Sub_Kategori_Jenis_1,
											   TotalJumlahTF as Jumlah
										from DataTF_Agg
										union all
										select Kode_Perusahaan, Tahun, Bulan, Kode_Kategori_Gudang, ID_Kategori_Jenis,
											   Id_Sub_Kategori_Jenis_1,
											   TotalJumlahPengeluaran as Jumlah
										from DataPengeluaran_Agg
									),

					-- 6. Join ke Binding Department untuk PRM
					Binding_PRM as (
										select z.Kode_Perusahaan,
											   z.Kode_Binding,
											   y.Tahun, y.Bulan,
											   y.ID_Kategori_Jenis,
											   y.Id_Sub_Kategori_Jenis_1,
											   sum(y.TotalJumlahPR) as JumlahPR
										from N_EMI_Binding_Department z with (nolock)
											 inner join N_EMI_Binding_Department_Detail x with (nolock)
														on z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Binding = x.Kode_Binding
											 inner join DataPRM_Agg y
														on x.Kode_Perusahaan = y.Kode_Perusahaan and
														   x.Kode_Kategori_Gudang = y.Kode_Kategori_Gudang
										where z.Status is null
										  and z.Kode_Perusahaan = '{KodePerusahaan}'
										  and z.Kode_Binding = '{SelectedDepartment_KodeBinding}'
										group by z.Kode_Perusahaan, y.Tahun, y.Bulan, z.Kode_Binding, y.ID_Kategori_Jenis,
												 y.Id_Sub_Kategori_Jenis_1
									),

					-- 7. Join ke Binding Department untuk TF (Hanya butuh 1 block query)
					Binding_TF as (
										select z.Kode_Perusahaan,
											   z.Kode_Binding,
											   y.Tahun, y.Bulan,
											   y.ID_Kategori_Jenis,
											   y.Id_Sub_Kategori_Jenis_1,
											   sum(y.Jumlah) as JumlahTF
										from N_EMI_Binding_Department z with (nolock)
											 inner join N_EMI_Binding_Department_Detail x with (nolock)
														on z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Binding = x.Kode_Binding
											 inner join DataTF_Union y
														on x.Kode_Perusahaan = y.Kode_Perusahaan and
														   x.Kode_Kategori_Gudang = y.Kode_Kategori_Gudang
										where z.Status is null
										  and z.Kode_Perusahaan = '{KodePerusahaan}'
										  and z.Kode_Binding = '{SelectedDepartment_KodeBinding}'
										group by z.Kode_Perusahaan, y.Tahun, y.Bulan, z.Kode_Binding, y.ID_Kategori_Jenis,
												 y.Id_Sub_Kategori_Jenis_1
									)

				-- 8. QUERY UTAMA
				select a.Kode_Binding,
					   b.ID_Kategori_Jenis,
					   c.ID_Sub_Kategori_Jenis_1,
					   d.Keterangan as Layer_3,
					   b.Bulan,
					   b.Tahun,
					   c.Jumlah_Budget,
					   CASE
						   WHEN isnull(e.JumlahPR, 0) > c.Jumlah_Budget
							   THEN isnull(e.JumlahPR, 0) - c.Jumlah_Budget
						   ELSE 0
					   END AS Jumlah_Tambah,
					   isnull(e.JumlahPR, 0) as JumlahPR,
					   isnull(f.JumlahTF, 0) as JumlahTF,
					   c.Urut_Oto
				from N_EMI_Transaksi_Budget_Planning a with (nolock)
					 inner join N_EMI_Transaksi_Budget_Planning_Detail b with (nolock)
								on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Binding = b.Kode_Binding
					 inner join N_EMI_Transaksi_Budget_Planning_Det c with (nolock)
								on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Binding = c.Kode_Binding
									and b.ID_Kategori_Jenis = c.ID_Kategori_Jenis and b.Urut_Oto = c.Urut_Detail
					 inner join N_EMI_Master_Sub_Kategori_Jenis_1 d with (nolock)
								on c.Kode_Perusahaan = d.Kode_Perusahaan and c.ID_Sub_Kategori_Jenis_1 = d.Id_Sub_Kategori_Jenis_1
					 left join Binding_PRM e
							   on a.Kode_Perusahaan = e.Kode_Perusahaan
								   and a.Kode_Binding = e.Kode_Binding
								   and b.ID_Kategori_Jenis = e.ID_Kategori_Jenis
								   and c.ID_Sub_Kategori_Jenis_1 = e.Id_Sub_Kategori_Jenis_1
								   and b.Tahun = e.Tahun and b.Bulan = e.Bulan
					 left join Binding_TF f
							   on a.Kode_Perusahaan = f.Kode_Perusahaan
								   and a.Kode_Binding = f.Kode_Binding
								   and b.ID_Kategori_Jenis = f.ID_Kategori_Jenis
								   and c.ID_Sub_Kategori_Jenis_1 = f.Id_Sub_Kategori_Jenis_1
								   and b.Tahun = f.Tahun and b.Bulan = f.Bulan
				where a.Status is null
				  and d.Flag_Is_Budget = 'Y'
				  and a.Kode_Perusahaan = '{KodePerusahaan}'
				  and a.Kode_Binding = '{SelectedDepartment_KodeBinding}'
				  and b.ID_Kategori_Jenis = '{SelectedIDLayer1}'
				  and b.Tahun = {SelectedTahun}
				  and b.Bulan between {SelectedBulanAwal} and {SelectedBulanAkhir}
			"

			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim ID_Sub_Kategori_1 As Integer = Dr("ID_Sub_Kategori_Jenis_1").ToString.Trim

						Dim DataDetailBudget As New DetailDataBudgetPlanning With {
							.Keterangan_Layer3 = Dr("Layer_3").ToString.Trim,
							.Kode_Binding = Dr("Kode_Binding").ToString.Trim,
							.ID_Kategori_Jenis = Dr("ID_Kategori_Jenis"),
							.Bulan = Dr("Bulan"),
							.Tahun = Dr("Tahun"),
							.Jumlah_Budget = Val(HilangkanTanda(Dr("Jumlah_Budget"))),
							.Jumlah_Tambah = Val(HilangkanTanda(Dr("Jumlah_Tambah"))),
							.Jumlah_PR = Val(HilangkanTanda(Dr("JumlahPR"))),
							.Jumlah_Transfer = Val(HilangkanTanda(Dr("JumlahTF")))
						}

						If Not DataBudgetPlanning.ContainsKey(ID_Sub_Kategori_1) Then
							Dim ListDetailBudget As New List(Of DetailDataBudgetPlanning)
							ListDetailBudget.Add(DataDetailBudget)
							DataBudgetPlanning.Add(ID_Sub_Kategori_1, ListDetailBudget)
						Else
							DataBudgetPlanning(ID_Sub_Kategori_1).Add(DataDetailBudget)
						End If

					Loop While Dr.Read
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

		'======================================
		'=     BAGIAN MENGELOLA DATA FORM     =
		'======================================
		Try

			'==============================
			'=     LOAD KOLOM DINAMIS     =
			'==============================
			Dgv_Data.SuspendLayout()

			' Hapus Dulu Kolom Lama
			If Dgv_Data.Columns.Count > 2 Then
				' Loop mundur dari indeks terakhir (Count - 1) sampai indeks ke-2
				For idx As Integer = Dgv_Data.Columns.Count - 1 To 2 Step -1
					Dgv_Data.Columns.RemoveAt(idx)
				Next
			End If

			Dgv_Data.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
			Dgv_Data.ColumnHeadersHeight = 55

			For i As Integer = SelectedBulanAwal To SelectedBulanAkhir
				Dim Bulan As String = i.ToString()

				Dim colBdg As New DataGridViewTextBoxColumn() With {.Name = $"QtyBudget_{Bulan}", .HeaderText = "Qty Budget", .Width = 130, .Tag = i}
				Dim colTmb As New DataGridViewTextBoxColumn() With {.Name = $"QtyTambah_{Bulan}", .HeaderText = "Qty Tambah", .Width = 130, .Tag = i, .[ReadOnly] = True}
				Dim colPR As New DataGridViewTextBoxColumn() With {.Name = $"QtyPR_{Bulan}", .HeaderText = "Qty PR", .Width = 130, .Tag = i, .[ReadOnly] = True}
				Dim colTF As New DataGridViewTextBoxColumn() With {.Name = $"QtyTF_{Bulan}", .HeaderText = "Qty TF", .Width = 130, .Tag = i, .[ReadOnly] = True}

				'============================
				'=     ATUR SIFAT KOLOM     =
				'============================
				colBdg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
				colTmb.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
				colPR.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
				colTF.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

				colBdg.DefaultCellStyle.BackColor = Color.FromArgb(224, 236, 244)
				colTmb.DefaultCellStyle.BackColor = Color.FromArgb(233, 240, 228)
				colPR.DefaultCellStyle.BackColor = Color.FromArgb(247, 241, 231)
				colTF.DefaultCellStyle.BackColor = Color.FromArgb(238, 233, 245)

				colBdg.DefaultCellStyle.Format = "N0"
				colTmb.DefaultCellStyle.Format = "N0"
				colPR.DefaultCellStyle.Format = "N0"
				colTF.DefaultCellStyle.Format = "N0"

				If i <= CurrentMonth Then
					colBdg.ReadOnly = True

					'Dim warnaKunci As Color = Color.Gray
					'colBdg.DefaultCellStyle.ForeColor = warnaKunci
					'colTmb.DefaultCellStyle.ForeColor = warnaKunci
					'colPR.DefaultCellStyle.ForeColor = warnaKunci
					'colTF.DefaultCellStyle.ForeColor = warnaKunci
				End If

				'==================================
				'=     TAMBAH KE DATAGRIDVIEW     =
				'==================================
				Dgv_Data.Columns.Add(colBdg)
				Dgv_Data.Columns.Add(colTmb)
				Dgv_Data.Columns.Add(colPR)
				Dgv_Data.Columns.Add(colTF)
			Next

			'=============================
			'=     LOAD DATA LAYER 3     =
			'=============================
			Dgv_Data.Rows.Clear()
			For Each kvp As KeyValuePair(Of Integer, List(Of DetailDataLayer3)) In DataLayer3
				Dim ID_Sub_Kategori_1 As Integer = kvp.Key
				Dim listDetail As List(Of DetailDataLayer3) = kvp.Value

				If listDetail.Count = 0 Then Continue For

				Dim namaKategoriLayer3 As String = listDetail(0).keteranganKategoriLayer3

				'=====================================
				'=     LOAD DATA KE DATAGRIDVIEW     =
				'=====================================
				Dim rowIndex As Integer = Dgv_Data.Rows.Add()
				Dim row As DataGridViewRow = Dgv_Data.Rows(rowIndex)
				row.Cells(0).Value = ID_Sub_Kategori_1
				row.Cells(1).Value = namaKategoriLayer3

				'==================================================
				'=     GET DATA BUDGET BERDASARKAN ID LAYER 3     =
				'==================================================
				Dim listBudget As List(Of DetailDataBudgetPlanning) = Nothing
				If DataBudgetPlanning.ContainsKey(ID_Sub_Kategori_1) Then
					listBudget = DataBudgetPlanning(ID_Sub_Kategori_1)
				End If

				'=========================================================
				'=     GET DATA BULAN BERDASARKAN YANG DI PILIH USER     =
				'=========================================================
				Dim DataBulanYangDipilih = DataBulan.Where(Function(b)
															   Dim indexBulan As Integer = Val(HilangkanTanda(b.Sql))
															   Return indexBulan >= SelectedBulanAwal AndAlso indexBulan <= SelectedBulanAkhir
														   End Function)

				For Each bln In DataBulanYangDipilih
					Dim sufiksBulan As String = bln.Sql
					Dim kodeBulan As Integer = Val(HilangkanTanda(sufiksBulan))
					Dim detailBudget As DetailDataBudgetPlanning = Nothing

					If listBudget IsNot Nothing Then
						detailBudget = listBudget.FirstOrDefault(Function(x) x.Bulan = kodeBulan)
					End If

					If detailBudget IsNot Nothing Then
						If Dgv_Data.Columns.Contains($"QtyBudget_{sufiksBulan}") Then
							SetCellDataAndColor(row.Cells($"QtyBudget_{sufiksBulan}"), detailBudget.Jumlah_Budget, PersentasePenggelapanWarnaCellInput)
						End If

						If Dgv_Data.Columns.Contains($"QtyTambah_{sufiksBulan}") Then
							row.Cells($"QtyTambah_{sufiksBulan}").Value = detailBudget.Jumlah_Tambah
						End If

						If Dgv_Data.Columns.Contains($"QtyPR_{sufiksBulan}") Then
							row.Cells($"QtyPR_{sufiksBulan}").Value = detailBudget.Jumlah_PR
						End If

						If Dgv_Data.Columns.Contains($"QtyTF_{sufiksBulan}") Then
							row.Cells($"QtyTF_{sufiksBulan}").Value = detailBudget.Jumlah_Transfer
						End If
					Else
						If Dgv_Data.Columns.Contains($"QtyBudget_{sufiksBulan}") Then
							row.Cells($"QtyBudget_{sufiksBulan}").Value = 0
							row.Cells($"QtyBudget_{sufiksBulan}").Style.BackColor = Color.Empty
						End If
						If Dgv_Data.Columns.Contains($"QtyTambah_{sufiksBulan}") Then
							row.Cells($"QtyTambah_{sufiksBulan}").Value = 0
							row.Cells($"QtyTambah_{sufiksBulan}").Style.BackColor = Color.Empty
						End If
						If Dgv_Data.Columns.Contains($"QtyPR_{sufiksBulan}") Then
							row.Cells($"QtyPR_{sufiksBulan}").Value = 0
							row.Cells($"QtyPR_{sufiksBulan}").Style.BackColor = Color.Empty
						End If
						If Dgv_Data.Columns.Contains($"QtyTF_{sufiksBulan}") Then
							row.Cells($"QtyTF_{sufiksBulan}").Value = 0
							row.Cells($"QtyTF_{sufiksBulan}").Style.BackColor = Color.Empty
						End If
					End If
				Next

			Next

			'==============================
			'=     MATIKAN FITUR SORT     =
			'==============================
			For Each col As DataGridViewColumn In Dgv_Data.Columns
				col.SortMode = DataGridViewColumnSortMode.NotSortable
			Next

			Dgv_Data.ResumeLayout()
		Catch ex As Exception
			MessageBox.Show(ex.Message)
		End Try

	End Sub

	Private Sub CetakLaporan()
		If Dgv_Data.Rows.Count = 0 Then
			MessageBox.Show("Tidak ada data yang dapat diekspor.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
			Exit Sub
		End If

		' Paksa Index 0 (ID) menjadi False agar murni terabaikan helper
		Dgv_Data.Columns(0).Visible = False

		Dim visibleCols = Dgv_Data.Columns.Cast(Of DataGridViewColumn)().
					  Where(Function(c) c.Visible = True).
					  OrderBy(Function(c) c.DisplayIndex).ToList()

		If visibleCols.Count = 0 Then
			MessageBox.Show("Tidak ada kolom yang ditampilkan untuk diekspor.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
			Exit Sub
		End If

		Dim config As New ExcelExportHelper.ExportConfig()
		config.FileName = "Laporan_Budget_" & Now.ToString("dd_MM_yyyy_HH_mm") & ".xlsx"
		config.SheetName = "Data Budget"
		config.FreezePanes = True

		Dim headerRow1 As New ExcelExportHelper.HeaderRow()
		Dim headerRow2 As New ExcelExportHelper.HeaderRow()

		Dim colorBudget As Color = Color.FromArgb(217, 225, 242)
		Dim colorTambah As Color = Color.FromArgb(226, 239, 218)
		Dim colorPR As Color = Color.FromArgb(255, 242, 204)
		Dim colorTF As Color = Color.FromArgb(228, 223, 236)

		headerRow1.AddCell(New ExcelExportHelper.HeaderCell("Kategori Layer 3", rowSpan:=2, colSpan:=1, backColor:=Color.WhiteSmoke))

		Dim blnAwal As Integer = Convert.ToInt32(Cmb_Bulan_Awal.SelectedValue)
		Dim blnAkhir As Integer = Convert.ToInt32(Cmb_Bulan_Akhir.SelectedValue)
		Dim cultureID As New System.Globalization.CultureInfo("id-ID")

		For i As Integer = blnAwal To blnAkhir
			Dim namaBulan As String = cultureID.DateTimeFormat.GetMonthName(i)

			headerRow1.AddCell(New ExcelExportHelper.HeaderCell(namaBulan, rowSpan:=1, colSpan:=4, backColor:=Color.LightGray))

			headerRow2.AddCell(New ExcelExportHelper.HeaderCell("Qty Budget", backColor:=Color.White))
			headerRow2.AddCell(New ExcelExportHelper.HeaderCell("Qty Tambah", backColor:=Color.White))
			headerRow2.AddCell(New ExcelExportHelper.HeaderCell("Qty PR", backColor:=Color.White))
			headerRow2.AddCell(New ExcelExportHelper.HeaderCell("Qty TF", backColor:=Color.White))
		Next

		config.Headers.Add(headerRow1)
		config.Headers.Add(headerRow2)

		' Mapping Format, Alignment, dan Warna Data secara dinamis
		For i As Integer = 0 To visibleCols.Count - 1
			Dim dgvCol As DataGridViewColumn = visibleCols(i)
			Dim excelColName As String = GetExcelColumnName(i)

			If Not dgvCol.Name.Equals("Layer_3", StringComparison.OrdinalIgnoreCase) Then
				' Set Format Number dan Alignment
				config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat(excelColName, "N0", ExcelExportHelper.ExcelAlignment.Right))

				' Menentukan warna berdasar pola kolom (0 = Budget, 1 = Tambah, 2 = PR, 3 = TF)
				Dim colColor As Color
				Select Case (i - 1) Mod 4
					Case 0 : colColor = colorBudget
					Case 1 : colColor = colorTambah
					Case 2 : colColor = colorPR
					Case 3 : colColor = colorTF
				End Select

				' Terapkan rule untuk mewarnai seluruh cell di index kolom ini
				config.ConditionalRules.Add(New ExcelExportHelper.ConditionalRule(
				type:=ExcelExportHelper.ConditionalRule.RuleType.AlwaysApply,
				conditionColIdx:=0, ' Tidak peduli karena tipenya AlwaysApply
				conditionValue:="",
				targetColIdx:=i,    ' Terapkan warna di index kolom saat ini
				backColor:=colColor
			))
			Else
				config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat(excelColName, "@", ExcelExportHelper.ExcelAlignment.Left))
			End If
		Next

		ExcelExportHelper.ExportFromDataGridView(Dgv_Data, config, exportOnlyVisible:=True)
	End Sub

	Private Sub CetakLaporanRealtime()
		'=========================================
		'=  1. AMBIL PARAMETER & SIAPKAN VARIABEL=
		'=========================================
		Dim SelectedindexCMB As Integer = Cmb_Department.SelectedIndex
		Dim SelectedDepartment_KodeBinding As String = DataDepartment.Keys.ToList()(SelectedindexCMB)
		Dim SelectedIDLayer1 As Integer = DataLayer1.Keys.ToList()(Cmb_Kategori.SelectedIndex)
		Dim SelectedTahun As Integer = Cmb_Tahun.Text.Trim
		Dim SelectedBulanAwal As Integer = Val(HilangkanTanda(Cmb_Bulan_Awal.SelectedValue))
		Dim SelectedBulanAkhir As Integer = Val(HilangkanTanda(Cmb_Bulan_Akhir.SelectedValue))

		Dim LocalDataBudget As New Dictionary(Of Integer, List(Of DetailDataBudgetPlanning))

		'========================================
		'=  2. HIT DATABASE (QUERY REAL-TIME)   =
		'========================================
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			' (Gunakan Query CTE super cepat persis dari GetDataBudget Anda)
			SQL = $"
            ;with
                DataPRM_Base as (
                    select a.Kode_Perusahaan, ISNULL(YEAR(a.Tanggal), 0) as Tahun, ISNULL(MONTH(a.Tanggal), 0) as Bulan,
                           b.No_Urut, d.ID_Kategori_Jenis, d.Id_Sub_Kategori_Jenis_1, a.Kode_Kategori_Gudang, b.jumlah as JumlahPR
                    from N_EMI_Purchase_Requisition_Barang_Lain_Departement a with (nolock)
                         inner join N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail b with (nolock) on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
                         inner join barang_lain c with (nolock) on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang
                         inner join View_Kategori_Turunan d with (nolock) on b.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Sub_Kategori_Jenis_3 = d.Id_Sub_Kategori_Jenis_3
                    where a.Status is null and a.Kode_Perusahaan = '{KodePerusahaan}'
                ),
                DataPRM_Agg as (
                    select Kode_Perusahaan, Tahun, Bulan, Kode_Kategori_Gudang, ID_Kategori_Jenis, Id_Sub_Kategori_Jenis_1, sum(JumlahPR) as TotalJumlahPR
                    from DataPRM_Base group by Kode_Perusahaan, Tahun, Bulan, Kode_Kategori_Gudang, ID_Kategori_Jenis, Id_Sub_Kategori_Jenis_1
                ),
                DataTF_Agg as (
                    select z.Kode_Perusahaan, l.Kode_Kategori_Gudang, ISNULL(YEAR(z.Tanggal), 0) as Tahun, ISNULL(MONTH(z.Tanggal), 0) as Bulan,
                           l.ID_Kategori_Jenis, l.Id_Sub_Kategori_Jenis_1, sum(k.Jumlah) as TotalJumlahTF
                    from N_EMI_Transfer_Stock_Barang_Lain z with (nolock)
                         inner join N_EMI_Transfer_Stock_Barang_Lain_Detail x with (nolock) on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
                         inner join N_EMI_Transfer_Stock_Barang_Lain_Det y with (nolock) on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF
                         inner join N_EMI_Transfer_Stock_Barang_Lain_Det2 k with (nolock) on y.Kode_Perusahaan = k.Kode_Perusahaan and y.No_Faktur = k.No_Faktur and y.Urut_Oto = k.Urut_Det
                         inner join DataPRM_Base l on x.Kode_Perusahaan = l.Kode_Perusahaan and x.Urut_PR_Dept = l.No_Urut
                    where z.Status is null and y.Selesai = 'Y' and z.Kode_Perusahaan = '{KodePerusahaan}'
                    group by z.Kode_Perusahaan, l.Kode_Kategori_Gudang, z.Tanggal, l.ID_Kategori_Jenis, l.Id_Sub_Kategori_Jenis_1
                ),
                DataPengeluaran_Agg as (
                    select a.Kode_Perusahaan, l.Kode_Kategori_Gudang, ISNULL(YEAR(a.Tanggal), 0) as Tahun, ISNULL(MONTH(a.Tanggal), 0) as Bulan,
                           l.ID_Kategori_Jenis, l.Id_Sub_Kategori_Jenis_1, sum(c.Jumlah) as TotalJumlahPengeluaran
                    from EMI_Pengeluaran_Stock_parent_barang_lain a with (nolock)
                         inner join EMI_Pengeluaran_Stock_barang_lain b with (nolock) on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
                         inner join EMI_Pengeluaran_Stock_Det_Barang_Lain c with (nolock) on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF
                         inner join DataPRM_Base l on b.Kode_Perusahaan = l.Kode_Perusahaan and b.Urut_PR_Dept = l.No_Urut
                    where a.Status is null and a.Kode_Perusahaan = '{KodePerusahaan}' and c.Selesai = 'Y'
                    group by a.Kode_Perusahaan, a.Tanggal, l.Kode_Kategori_Gudang, l.ID_Kategori_Jenis, l.Id_Sub_Kategori_Jenis_1
                ),
                DataTF_Union as (
                    select Kode_Perusahaan, Tahun, Bulan, Kode_Kategori_Gudang, ID_Kategori_Jenis, Id_Sub_Kategori_Jenis_1, TotalJumlahTF as Jumlah from DataTF_Agg
                    union all
                    select Kode_Perusahaan, Tahun, Bulan, Kode_Kategori_Gudang, ID_Kategori_Jenis, Id_Sub_Kategori_Jenis_1, TotalJumlahPengeluaran as Jumlah from DataPengeluaran_Agg
                ),
                Binding_PRM as (
                    select z.Kode_Perusahaan, z.Kode_Binding, y.Tahun, y.Bulan, y.ID_Kategori_Jenis, y.Id_Sub_Kategori_Jenis_1, sum(y.TotalJumlahPR) as JumlahPR
                    from N_EMI_Binding_Department z with (nolock)
                         inner join N_EMI_Binding_Department_Detail x with (nolock) on z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Binding = x.Kode_Binding
                         inner join DataPRM_Agg y on x.Kode_Perusahaan = y.Kode_Perusahaan and x.Kode_Kategori_Gudang = y.Kode_Kategori_Gudang
                    where z.Status is null and z.Kode_Perusahaan = '{KodePerusahaan}' and z.Kode_Binding = '{SelectedDepartment_KodeBinding}'
                    group by z.Kode_Perusahaan, y.Tahun, y.Bulan, z.Kode_Binding, y.ID_Kategori_Jenis, y.Id_Sub_Kategori_Jenis_1
                ),
                Binding_TF as (
                    select z.Kode_Perusahaan, z.Kode_Binding, y.Tahun, y.Bulan, y.ID_Kategori_Jenis, y.Id_Sub_Kategori_Jenis_1, sum(y.Jumlah) as JumlahTF
                    from N_EMI_Binding_Department z with (nolock)
                         inner join N_EMI_Binding_Department_Detail x with (nolock) on z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Binding = x.Kode_Binding
                         inner join DataTF_Union y on x.Kode_Perusahaan = y.Kode_Perusahaan and x.Kode_Kategori_Gudang = y.Kode_Kategori_Gudang
                    where z.Status is null and z.Kode_Perusahaan = '{KodePerusahaan}' and z.Kode_Binding = '{SelectedDepartment_KodeBinding}'
                    group by z.Kode_Perusahaan, y.Tahun, y.Bulan, z.Kode_Binding, y.ID_Kategori_Jenis, y.Id_Sub_Kategori_Jenis_1
                )
            select a.Kode_Binding, b.ID_Kategori_Jenis, c.ID_Sub_Kategori_Jenis_1, d.Keterangan as Layer_3, b.Bulan, b.Tahun, c.Jumlah_Budget,
                   CASE WHEN isnull(e.JumlahPR, 0) > c.Jumlah_Budget THEN isnull(e.JumlahPR, 0) - c.Jumlah_Budget ELSE 0 END AS Jumlah_Tambah,
                   isnull(e.JumlahPR, 0) as JumlahPR, isnull(f.JumlahTF, 0) as JumlahTF, c.Urut_Oto
            from N_EMI_Transaksi_Budget_Planning a with (nolock)
                 inner join N_EMI_Transaksi_Budget_Planning_Detail b with (nolock) on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Binding = b.Kode_Binding
                 inner join N_EMI_Transaksi_Budget_Planning_Det c with (nolock) on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Binding = c.Kode_Binding and b.ID_Kategori_Jenis = c.ID_Kategori_Jenis and b.Urut_Oto = c.Urut_Detail
                 inner join N_EMI_Master_Sub_Kategori_Jenis_1 d with (nolock) on c.Kode_Perusahaan = d.Kode_Perusahaan and c.ID_Sub_Kategori_Jenis_1 = d.Id_Sub_Kategori_Jenis_1
                 left join Binding_PRM e on a.Kode_Perusahaan = e.Kode_Perusahaan and a.Kode_Binding = e.Kode_Binding and b.ID_Kategori_Jenis = e.ID_Kategori_Jenis and c.ID_Sub_Kategori_Jenis_1 = e.Id_Sub_Kategori_Jenis_1 and b.Tahun = e.Tahun and b.Bulan = e.Bulan
                 left join Binding_TF f on a.Kode_Perusahaan = f.Kode_Perusahaan and a.Kode_Binding = f.Kode_Binding and b.ID_Kategori_Jenis = f.ID_Kategori_Jenis and c.ID_Sub_Kategori_Jenis_1 = f.Id_Sub_Kategori_Jenis_1 and b.Tahun = f.Tahun and b.Bulan = f.Bulan
            where a.Status is null and d.Flag_Is_Budget = 'Y' and a.Kode_Perusahaan = '{KodePerusahaan}' and a.Kode_Binding = '{SelectedDepartment_KodeBinding}'
              and b.ID_Kategori_Jenis = '{SelectedIDLayer1}' and b.Tahun = {SelectedTahun} and b.Bulan between {SelectedBulanAwal} and {SelectedBulanAkhir}"

			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim ID_Sub_1 As Integer = Dr("ID_Sub_Kategori_Jenis_1").ToString.Trim
						Dim dataDetail As New DetailDataBudgetPlanning With {
							.Keterangan_Layer3 = Dr("Layer_3").ToString.Trim,
							.Bulan = Dr("Bulan"),
							.Jumlah_Budget = Val(HilangkanTanda(Dr("Jumlah_Budget"))),
							.Jumlah_Tambah = Val(HilangkanTanda(Dr("Jumlah_Tambah"))),
							.Jumlah_PR = Val(HilangkanTanda(Dr("JumlahPR"))),
							.Jumlah_Transfer = Val(HilangkanTanda(Dr("JumlahTF")))
						}
						If Not LocalDataBudget.ContainsKey(ID_Sub_1) Then
							LocalDataBudget.Add(ID_Sub_1, New List(Of DetailDataBudgetPlanning) From {dataDetail})
						Else
							LocalDataBudget(ID_Sub_1).Add(dataDetail)
						End If
					Loop While Dr.Read
				End If
			End Using

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show("Gagal mengambil data terbaru: " & ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Exit Sub
		End Try

		'=========================================
		'=  3. BANGUN DATATABLE UNTUK EXPORT     =
		'=========================================
		Dim dtExport As New DataTable()
		dtExport.Columns.Add("Kategori Layer 3", GetType(String))

		' Buat kolom dinamis sesuai rentang bulan
		Dim cultureID As New System.Globalization.CultureInfo("id-ID")
		For bln As Integer = SelectedBulanAwal To SelectedBulanAkhir
			dtExport.Columns.Add($"QtyBudget_{bln}", GetType(Double))
			dtExport.Columns.Add($"QtyTambah_{bln}", GetType(Double))
			dtExport.Columns.Add($"QtyPR_{bln}", GetType(Double))
			dtExport.Columns.Add($"QtyTF_{bln}", GetType(Double))
		Next

		' Isi DataTable menggunakan Master DataLayer3
		If DataLayer3 IsNot Nothing Then
			For Each kvp As KeyValuePair(Of Integer, List(Of DetailDataLayer3)) In DataLayer3
				Dim ID_Sub_1 As Integer = kvp.Key
				Dim listDetail As List(Of DetailDataLayer3) = kvp.Value
				If listDetail.Count = 0 Then Continue For

				Dim row As DataRow = dtExport.NewRow()
				row("Kategori Layer 3") = listDetail(0).keteranganKategoriLayer3

				Dim listBudget As List(Of DetailDataBudgetPlanning) = Nothing
				If LocalDataBudget.ContainsKey(ID_Sub_1) Then
					listBudget = LocalDataBudget(ID_Sub_1)
				End If

				For bln As Integer = SelectedBulanAwal To SelectedBulanAkhir
					Dim det As DetailDataBudgetPlanning = Nothing
					If listBudget IsNot Nothing Then det = listBudget.FirstOrDefault(Function(x) x.Bulan = bln)

					row($"QtyBudget_{bln}") = If(det IsNot Nothing, det.Jumlah_Budget, 0)
					row($"QtyTambah_{bln}") = If(det IsNot Nothing, det.Jumlah_Tambah, 0)
					row($"QtyPR_{bln}") = If(det IsNot Nothing, det.Jumlah_PR, 0)
					row($"QtyTF_{bln}") = If(det IsNot Nothing, det.Jumlah_Transfer, 0)
				Next
				dtExport.Rows.Add(row)
			Next
		End If

		If dtExport.Rows.Count = 0 Then
			MessageBox.Show("Data kosong.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
			Exit Sub
		End If

		'=========================================
		'=  4. KONFIGURASI EXPORT EXCEL          =
		'=========================================
		Dim config As New ExcelExportHelper.ExportConfig() With {
			.FileName = "Laporan_Budget_Realtime_" & Now.ToString("dd_MM_yyyy_HH_mm") & ".xlsx",
			.SheetName = "Data Budget",
			.FreezePanes = True
		}

		Dim headerRow1 As New ExcelExportHelper.HeaderRow()
		Dim headerRow2 As New ExcelExportHelper.HeaderRow()

		' Warna Pastel (Sesuai gambar & permintaan)
		Dim colorBudget As Color = Color.FromArgb(217, 225, 242)
		Dim colorTambah As Color = Color.FromArgb(226, 239, 218)
		Dim colorPR As Color = Color.FromArgb(255, 242, 204)
		Dim colorTF As Color = Color.FromArgb(228, 223, 236)

		' Header: Kolom Layer 3 [cite: 3]
		headerRow1.AddCell(New ExcelExportHelper.HeaderCell("Kategori Layer 3", rowSpan:=2, colSpan:=1, backColor:=Color.WhiteSmoke))

		' Build Header Bulanan & Setup Fast Column Formatting
		' Kolom Layer 3 (Kolom Index 0 / Excel Col A) -> Format Default Text
		config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat("A", "@", ExcelExportHelper.ExcelAlignment.Left))

		Dim excelColIndex As Integer = 1 ' Dimulai dari B (0 = A)

		For bln As Integer = SelectedBulanAwal To SelectedBulanAkhir
			Dim namaBulan As String = cultureID.DateTimeFormat.GetMonthName(bln)
			' Header Row 1 (Merge 4 Kolom) [cite: 3]
			headerRow1.AddCell(New ExcelExportHelper.HeaderCell(namaBulan, rowSpan:=1, colSpan:=4, backColor:=Color.LightGray))

			' Header Row 2 [cite: 3]
			headerRow2.AddCell(New ExcelExportHelper.HeaderCell("Qty Budget", backColor:=Color.White))
			headerRow2.AddCell(New ExcelExportHelper.HeaderCell("Qty Tambah", backColor:=Color.White))
			headerRow2.AddCell(New ExcelExportHelper.HeaderCell("Qty PR", backColor:=Color.White))
			headerRow2.AddCell(New ExcelExportHelper.HeaderCell("Qty TF", backColor:=Color.White))

			' Set Fast Column Formatting per sub-kolom agar block warna instan [cite: 4, 7, 31]
			' Qty Budget
			config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat(GetExcelColumnName(excelColIndex), "N0", ExcelExportHelper.ExcelAlignment.Right, backColor:=colorBudget))
			excelColIndex += 1
			' Qty Tambah
			config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat(GetExcelColumnName(excelColIndex), "N0", ExcelExportHelper.ExcelAlignment.Right, backColor:=colorTambah))
			excelColIndex += 1
			' Qty PR
			config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat(GetExcelColumnName(excelColIndex), "N0", ExcelExportHelper.ExcelAlignment.Right, backColor:=colorPR))
			excelColIndex += 1
			' Qty TF
			config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat(GetExcelColumnName(excelColIndex), "N0", ExcelExportHelper.ExcelAlignment.Right, backColor:=colorTF))
			excelColIndex += 1
		Next

		config.Headers.Add(headerRow1)
		config.Headers.Add(headerRow2)

		'=========================================
		'=  5. EKSEKUSI EXPORT                   =
		'=========================================
		' Jauh lebih cepat dari DGV karena tidak mengunci UI Thread
		ExcelExportHelper.ExportFromDataTable(dtExport, config)

	End Sub

	Private Function GetExcelColumnName(columnNumber As Integer) As String
		Dim dividend As Integer = columnNumber + 1
		Dim columnName As String = String.Empty
		Dim modulo As Integer
		While dividend > 0
			modulo = (dividend - 1) Mod 26
			columnName = Convert.ToChar(65 + modulo).ToString() & columnName
			dividend = CInt((dividend - modulo) / 26)
		End While
		Return columnName
	End Function

	Private Sub Cmb_Department_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Department.SelectedIndexChanged
		If Cmb_Department.Items.Count = 0 Then Exit Sub
		If Cmb_Department.SelectedIndex = -1 Then
			Cmb_Kategori.Enabled = False
		Else
			Cmb_Kategori.Enabled = True
		End If

		Cmb_Kategori.SelectedIndex = -1
	End Sub

	Private Sub Cmb_Tahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Tahun.SelectedIndexChanged
		If Cmb_Tahun.Items.Count = 0 Then Exit Sub

		Cmb_Bulan_Akhir.SelectedIndex = -1

		If Cmb_Tahun.SelectedIndex = -1 Then
			Cmb_Bulan_Awal.Enabled = False
			Cmb_Bulan_Akhir.Enabled = False
			Cmb_Bulan_Awal.SelectedIndex = -1
		Else
			Cmb_Bulan_Awal.Enabled = True
			Cmb_Bulan_Akhir.Enabled = True

			If Cmb_Bulan_Awal.Items.Count > 0 Then
				Cmb_Bulan_Awal.SelectedValue = CurrentMonth.ToString
			End If
		End If

	End Sub

#Region "asdsada"

	'Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
	'	If Cmb_Department.SelectedIndex = -1 Then
	'		MessageBox.Show("Department Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'		Cmb_Department.DroppedDown = True
	'		Cmb_Department.Focus()
	'		Exit Sub
	'	ElseIf Cmb_Kategori.SelectedIndex = -1 Then
	'		MessageBox.Show("Kategori Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'		Cmb_Kategori.DroppedDown = True
	'		Cmb_Kategori.Focus()
	'		Exit Sub
	'	ElseIf Cmb_Tahun.SelectedIndex = -1 Then
	'		MessageBox.Show("Tahun Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'		Cmb_Tahun.DroppedDown = True
	'		Cmb_Tahun.Focus()
	'		Exit Sub
	'	ElseIf Cmb_Bulan_Awal.SelectedIndex = -1 Then
	'		MessageBox.Show("Bulan Awal Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'		Cmb_Bulan_Awal.DroppedDown = True
	'		Cmb_Bulan_Awal.Focus()
	'		Exit Sub
	'	ElseIf Cmb_Bulan_Akhir.SelectedIndex = -1 Then
	'		MessageBox.Show("Bulan Akhir Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'		Cmb_Bulan_Akhir.DroppedDown = True
	'		Cmb_Bulan_Akhir.Focus()
	'		Exit Sub
	'	End If

	'	If Val(Cmb_Bulan_Akhir.SelectedValue) < Val(Cmb_Bulan_Awal.SelectedValue) Then
	'		MessageBox.Show("Bulan Akhir tidak boleh lebih kecil dari Bulan Awal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
	'		Cmb_Bulan_Akhir.DroppedDown = True
	'		Cmb_Bulan_Akhir.Focus()
	'		Exit Sub
	'	End If

	'	If Dgv_Data.Rows.Count = 0 Then
	'		MessageBox.Show("Tidak Ada Data yang Akan Disimpan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
	'		Btn_Get_Data.Focus()
	'		Exit Sub
	'	End If

	'	'=====================================================
	'	'=     SUSUN ARRAY AGAR TIDAK MEMBERATKAN SERVER     =
	'	'=====================================================

	'	get_jam()

	'	Try
	'		OpenConn()
	'		Cmd.Transaction = Cn.BeginTransaction

	'		Dim KodeBinding As String = DataDepartment.Keys.ToList()(Cmb_Department.SelectedIndex)
	'		Dim IDlayer1 As String = DataLayer1.Keys.ToList()(Cmb_Kategori.SelectedIndex)
	'		Dim SelectedTahun As Integer = Cmb_Tahun.Text.Trim
	'		Dim SelectedBulanAwal As Integer = Val(HilangkanTanda(Cmb_Bulan_Awal.SelectedValue)) + 1S
	'		Dim SelectedBulanAkhir As Integer = Val(HilangkanTanda(Cmb_Bulan_Akhir.SelectedValue))

	'		Dim ListLayer3 As List(Of String) = DataUpdate.Select(Function(x) x.ID_Layer3.ToString()).Distinct().ToList()
	'		Dim ListBulan As List(Of ModelCekBulan) = DataUpdate.Select(Function(x) x.Bulan) _
	'			.Distinct() _
	'			.Select(Function(b) New ModelCekBulan With {.Bulan = b, .FlagHasDataInDB = False}) _
	'			.ToList()
	'		Dim FilterListLayer3 As String = "'" & String.Join("', '", ListLayer3) & "'"
	'		Dim FilterListBulan As String = "'" & String.Join("', '", ListBulan.Select(Function(x) x.Bulan)) & "'"

	'		'===========================
	'		'=     CEK DATA PARENT     =
	'		'===========================
	'		SQL = $"
	'			select Status
	'			from N_EMI_Transaksi_Budget_Planning
	'			where Kode_Perusahaan = '{KodePerusahaan}'
	'			and Kode_Binding = '{KodeBinding}'
	'		"
	'		Using Dr = OpenTrans(SQL)
	'			If Dr.Read Then
	'				If General_Class.CekNULL(Dr("Status")) = "Y" Then
	'					Dr.Close()
	'					CloseTrans()
	'					CloseConn()
	'					MessageBox.Show($"Terjadi Kesalahan, Data Budget untuk Kode Binding {KodeBinding} Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'					Exit Sub
	'				End If
	'			Else
	'				Dr.Close()
	'				SQL = $"
	'					INSERT INTO N_EMI_Transaksi_Budget_Planning
	'						(Kode_Perusahaan, Kode_Binding, Tanggal, Jam, UserID)
	'					VALUES
	'						('{KodePerusahaan}', '{KodeBinding}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}',
	'						'{UserID}')
	'				"
	'				ExecuteTrans(SQL)
	'			End If
	'		End Using

	'		SQL = $"
	'			select Bulan
	'			from N_EMI_Transaksi_Budget_Planning_Detail
	'			where Kode_Perusahaan = '{KodePerusahaan}'
	'			and Kode_Binding = '{KodeBinding}'
	'			and ID_Kategori_Jenis = '{IDlayer1}'
	'			and Bulan in ({FilterListBulan})
	'		"
	'		Using Dr = OpenTrans(SQL)
	'			If Dr.Read Then
	'				Do
	'					Dim bulanDb As Integer = Val(HilangkanTanda(Dr("Bulan")))
	'					Dim dataBulan = ListBulan.FirstOrDefault(Function(x) x.Bulan = bulanDb)

	'					If dataBulan IsNot Nothing Then
	'						dataBulan.FlagHasDataInDB = True
	'					End If
	'				Loop While Dr.Read
	'			End If
	'		End Using

	'		For Each item In ListBulan.Where(Function(x) x.FlagHasDataInDB = False)
	'			SQL = $"
	'				INSERT INTO N_EMI_Transaksi_Budget_Planning_Detail
	'					(Kode_Perusahaan, Kode_Binding, ID_Kategori_Jenis, Bulan, Tahun, Total_Budget, Total_Nominal)
	'				VALUES
	'					('{KodePerusahaan}', '{KodeBinding}', '{IDlayer1}', '{item.Bulan}', '{SelectedTahun}',
	'					0, 0)
	'			"
	'			ExecuteTrans(SQL)
	'		Next

	'		'==============================================================================
	'		'=     CEK DATA APAKAH SUDAH ADA DI TABEL TRANSAKSI, JIKA ADA MAKA UPDATE     =
	'		'==============================================================================
	'		If ListLayer3.Count > 0 Then

	'			SQL = $"
	'				select a.Kode_Binding, b.ID_Kategori_Jenis, c.ID_Sub_Kategori_Jenis_1, b.Bulan,
	'					b.Urut_Oto as Urut_Detail, c.Urut_Oto as UrutDet,
	'					c.Jumlah_Budget
	'				from N_EMI_Transaksi_Budget_Planning a
	'					inner join N_EMI_Transaksi_Budget_Planning_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Binding = b.Kode_Binding
	'					inner join N_EMI_Transaksi_Budget_Planning_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Binding = c.Kode_Binding and b.Urut_Oto = c.Urut_Detail
	'				where a.Status is null
	'				and a.Kode_Perusahaan = '{KodePerusahaan}'
	'				and a.Kode_Binding = '{KodeBinding}'
	'				and b.Tahun = '{SelectedTahun}'
	'				and b.Bulan between {SelectedBulanAwal } and {SelectedBulanAkhir}
	'				and b.ID_Kategori_Jenis = '{IDlayer1}'
	'				and c.ID_Sub_Kategori_Jenis_1 in ({FilterListLayer3})
	'			"
	'			Using Ds = BindingTrans(SQL)
	'				With Ds.Tables("MyTable")
	'					If .Rows.Count <> 0 Then
	'						For i As Integer = 0 To .Rows.Count - 1

	'							Dim ID_Sub_Kategori_Jenis_1 As Integer = .Rows(i).Item("ID_Sub_Kategori_Jenis_1")
	'							Dim Bulan As Integer = .Rows(i).Item("Bulan")
	'							Dim Jumlah_Budget_Awal As Double = Val(HilangkanTanda(.Rows(i).Item("Jumlah_Budget")))
	'							Dim UrutDetail As Integer = .Rows(i).Item("Urut_Detail")
	'							Dim UrutDet As Integer = .Rows(i).Item("UrutDet")

	'							Dim dataEksis As DetailDataUpdate = DataUpdate.FirstOrDefault(Function(x) x.ID_Layer3 = ID_Sub_Kategori_Jenis_1 And x.Bulan = Bulan And x.FlagUpdate = False)

	'							If dataEksis IsNot Nothing Then

	'								'======================
	'								'=     INSERT LOG     =
	'								'======================
	'								SQL = $"
	'									INSERT INTO N_EMI_Transaksi_Budget_Planning_Log
	'										(Kode_Perusahaan, Kode_Binding, ID_Kategori_Jenis, ID_Sub_Kategori_Jenis_1, Jenis, Bulan, Tahun, Jumlah_Budget, Jumlah_Update, Nominal_Update,
	'										 Nominal_Budget, Tanggal, Jam, UserID)
	'									VALUES
	'										('{KodePerusahaan}', '{KodeBinding}', '{IDlayer1}', '{ID_Sub_Kategori_Jenis_1}', 'UPDATE',
	'										'{Bulan}', '{SelectedTahun}', '{Jumlah_Budget_Awal}', '{dataEksis.JumlahUpdate}', '0', '0',
	'										{Format(tgl_skg, "yyyy-MM-dd")}, '{Format(tgl_skg, "HH:mm:ss")}', '{UserID}')
	'								"
	'								ExecuteTrans(SQL)

	'								'======================
	'								'=     UPDATE DET     =
	'								'======================
	'								SQL = $"
	'									update N_EMI_Transaksi_Budget_Planning_Det set Jumlah_Budget = {Val(HilangkanTanda(dataEksis.JumlahUpdate))}
	'									where Kode_Perusahaan = '{KodePerusahaan}'
	'									and Kode_Binding = '{KodeBinding}'
	'									and ID_Kategori_Jenis = '{IDlayer1}'
	'									and ID_Sub_Kategori_Jenis_1 = '{ID_Sub_Kategori_Jenis_1}'
	'									and Urut_Oto = '{UrutDet}'
	'								"
	'								ExecuteTrans(SQL)

	'								dataEksis.FlagUpdate = True

	'							End If

	'						Next

	'					End If
	'				End With
	'			End Using

	'			For Each item As DetailDataUpdate In DataUpdate.Where(Function(x) x.FlagUpdate = False)

	'				'======================
	'				'=     UPDATE DET     =
	'				'======================
	'				SQL = $"
	'					select 1
	'					from N_EMI_Transaksi_Budget_Planning_Det a
	'						inner join N_EMI_Transaksi_Budget_Planning_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan
	'							and a.Kode_Binding = b.Kode_Binding and a.ID_Kategori_Jenis = b.ID_Kategori_Jenis and a.Urut_Detail = b.Urut_Oto
	'					where a.Kode_Perusahaan = '{KodePerusahaan}'
	'					and a.Kode_Binding = '{KodeBinding}'
	'					and a.ID_Kategori_Jenis = '{IDlayer1}'
	'					and a.ID_Sub_Kategori_Jenis_1 = '{item.ID_Layer3}'
	'					and b.Bulan = '{item.Bulan}'
	'				"
	'				Using Dr = OpenTrans(SQL)
	'					If Dr.Read Then
	'						Dr.Close()
	'						CloseTrans()
	'						CloseConn()
	'						MessageBox.Show($"Terjadi Kesalahan, pada IDLayer3 {item.ID_Layer3} ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'						Exit Sub
	'					Else
	'						Dr.Close()

	'						'==========================
	'						'=     GET URUT DETAIL    =
	'						'==========================
	'						SQL = $"
	'							select Urut_Oto
	'							from N_EMI_Transaksi_Budget_Planning_Detail
	'							where Kode_Perusahaan = '{KodePerusahaan}'
	'							and Kode_Binding = '{KodeBinding}'
	'							and ID_Kategori_Jenis = '{IDlayer1}'
	'							and Bulan = '{item.Bulan}'
	'						"
	'						Using Dr2 = OpenTrans(SQL)
	'							If Dr2.Read Then
	'								Dim UrutDetail As Integer = Dr2("Urut_Oto")
	'								Dr2.Close()
	'								SQL = $"
	'										INSERT INTO N_EMI_Transaksi_Budget_Planning_Det
	'											(Kode_Perusahaan, Kode_Binding, ID_Kategori_Jenis, ID_Sub_Kategori_Jenis_1, Jumlah_Budget, Nominal_Budget,
	'											 Urut_Detail)
	'										VALUES
	'											('{KodePerusahaan}', '{KodeBinding}', '{IDlayer1}', '{item.ID_Layer3}',
	'											'{Val(HilangkanTanda(item.JumlahUpdate))}', 0, {UrutDetail});
	'									"
	'								ExecuteTrans(SQL)
	'							Else
	'								Dr2.Close()
	'								CloseTrans()
	'								CloseConn()
	'								MessageBox.Show($"Terjadi Kesalahan, Data Detail untuk Bulan {item.Bulan} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	'								Exit Sub
	'							End If
	'						End Using

	'					End If
	'				End Using

	'			Next

	'			'=========================
	'			'=     UPDATE PARENT     =
	'			'=========================
	'			SQL = $"
	'				update N_EMI_Transaksi_Budget_Planning
	'				set Tanggal_Update = '{Format(tgl_skg, "yyyy-MM-dd")}',
	'				Jam_Update = '{Format(tgl_skg, "HH:mm:ss")}',
	'				UserID_Update = '{UserID}'
	'				where Kode_Perusahaan = '{KodePerusahaan}'
	'				and Kode_Binding = '{KodeBinding}'
	'			"
	'			ExecuteTrans(SQL)

	'			'=========================
	'			'=     UPDATE DETAIL     =
	'			'=========================
	'			SQL = $"
	'				update a
	'				set a.Total_Budget = b.TotalDet
	'				from N_EMI_Transaksi_Budget_Planning_Detail a
	'				inner join (
	'					select Kode_Binding, ID_Kategori_Jenis, Urut_Detail, SUM(Jumlah_Budget) AS TotalDet
	'					from N_EMI_Transaksi_Budget_Planning_Det
	'					where Kode_Perusahaan = '{KodePerusahaan}'
	'					group by Kode_Binding, ID_Kategori_Jenis, Urut_Detail
	'				) b on b.Kode_Binding = a.Kode_Binding
	'					 and b.ID_Kategori_Jenis = a.ID_Kategori_Jenis
	'					 and b.Urut_Detail = a.Urut_Oto
	'				where a.Kode_Perusahaan = '{KodePerusahaan}'
	'				and a.Kode_Binding = '{KodeBinding}'
	'				and a.ID_Kategori_Jenis = '{IDlayer1}'
	'				and a.Tahun = '{SelectedTahun}'
	'				and a.Bulan between {SelectedBulanAwal} and {SelectedBulanAkhir}
	'			"
	'			ExecuteTrans(SQL)

	'		End If

	'		Cmd.Transaction.Commit()
	'		CloseTrans()
	'		CloseConn()
	'		MessageBox.Show("Data Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
	'	Catch ex As Exception
	'		CloseTrans()
	'		CloseConn()
	'		MessageBox.Show(ex.Message)
	'		Exit Sub
	'	End Try

	'	Btn_Get_Data_Click(sender, e)

	'End Sub

#End Region

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If Cmb_Department.SelectedIndex = -1 Then
			MessageBox.Show("Department Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Department.DroppedDown = True
			Cmb_Department.Focus()
			Exit Sub
		ElseIf Cmb_Kategori.SelectedIndex = -1 Then
			MessageBox.Show("Kategori Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Kategori.DroppedDown = True
			Cmb_Kategori.Focus()
			Exit Sub
		ElseIf Cmb_Tahun.SelectedIndex = -1 Then
			MessageBox.Show("Tahun Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Tahun.DroppedDown = True
			Cmb_Tahun.Focus()
			Exit Sub
		ElseIf Cmb_Bulan_Awal.SelectedIndex = -1 Then
			MessageBox.Show("Bulan Awal Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Bulan_Awal.DroppedDown = True
			Cmb_Bulan_Awal.Focus()
			Exit Sub
		ElseIf Cmb_Bulan_Akhir.SelectedIndex = -1 Then
			MessageBox.Show("Bulan Akhir Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Bulan_Akhir.DroppedDown = True
			Cmb_Bulan_Akhir.Focus()
			Exit Sub
		End If

		If Val(Cmb_Bulan_Akhir.SelectedValue) < Val(Cmb_Bulan_Awal.SelectedValue) Then
			MessageBox.Show("Bulan Akhir tidak boleh lebih kecil dari Bulan Awal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
			Cmb_Bulan_Akhir.DroppedDown = True
			Cmb_Bulan_Akhir.Focus()
			Exit Sub
		End If

		If Dgv_Data.Rows.Count = 0 Then
			MessageBox.Show("Tidak Ada Data yang Akan Disimpan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
			Btn_Get_Data.Focus()
			Exit Sub
		End If

		'=====================================================
		'=     SUSUN ARRAY AGAR TIDAK MEMBERATKAN SERVER     =
		'=====================================================

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim KodeBinding As String = DataDepartment.Keys.ToList()(Cmb_Department.SelectedIndex)
			Dim IDlayer1 As String = DataLayer1.Keys.ToList()(Cmb_Kategori.SelectedIndex)
			Dim SelectedTahun As Integer = Cmb_Tahun.Text.Trim
			Dim SelectedBulanAwal As Integer = Val(HilangkanTanda(Cmb_Bulan_Awal.SelectedValue)) + 1S
			Dim SelectedBulanAkhir As Integer = Val(HilangkanTanda(Cmb_Bulan_Akhir.SelectedValue))

			Dim ListLayer3 As List(Of String) = DataUpdate.Select(Function(x) x.ID_Layer3.ToString()).Distinct().ToList()
			Dim ListBulan As List(Of ModelCekBulan) = DataUpdate.Select(Function(x) x.Bulan) _
				.Distinct() _
				.Select(Function(b) New ModelCekBulan With {.Bulan = b, .FlagHasDataInDB = False}) _
				.ToList()
			Dim FilterListLayer3 As String = "'" & String.Join("', '", ListLayer3) & "'"
			Dim FilterListBulan As String = "'" & String.Join("', '", ListBulan.Select(Function(x) x.Bulan)) & "'"

			'===========================
			'=     CEK DATA PARENT     =
			'===========================
			SQL = $"
				select Status
				from N_EMI_Transaksi_Budget_Planning
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Kode_Binding = '{KodeBinding}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If General_Class.CekNULL(Dr("Status")) = "Y" Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Data Budget untuk Kode Binding {KodeBinding} Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				Else
					Dr.Close()
					SQL = $"
						INSERT INTO N_EMI_Transaksi_Budget_Planning
							(Kode_Perusahaan, Kode_Binding, Tanggal, Jam, UserID)
						VALUES
							('{KodePerusahaan}', '{KodeBinding}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}',
							'{UserID}')
					"
					ExecuteTrans(SQL)
				End If
			End Using

			SQL = $"
				select Bulan
				from N_EMI_Transaksi_Budget_Planning_Detail
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Kode_Binding = '{KodeBinding}'
				and ID_Kategori_Jenis = '{IDlayer1}'
				and Bulan in ({FilterListBulan})
				and Tahun = '{SelectedTahun}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim bulanDb As Integer = Val(HilangkanTanda(Dr("Bulan")))
						Dim dataBulan = ListBulan.FirstOrDefault(Function(x) x.Bulan = bulanDb)

						If dataBulan IsNot Nothing Then
							dataBulan.FlagHasDataInDB = True
						End If
					Loop While Dr.Read
				End If
			End Using

			For Each item In ListBulan.Where(Function(x) x.FlagHasDataInDB = False)
				SQL = $"
					INSERT INTO N_EMI_Transaksi_Budget_Planning_Detail
						(Kode_Perusahaan, Kode_Binding, ID_Kategori_Jenis, Bulan, Tahun, Total_Budget, Total_Nominal)
					VALUES
						('{KodePerusahaan}', '{KodeBinding}', '{IDlayer1}', '{item.Bulan}', '{SelectedTahun}',
						0, 0)
				"
				ExecuteTrans(SQL)
			Next

			' 3. AMBIL URUT_OTO UNTUK MAPPING (Menghindari SELECT di dalam loop bawah)
			Dim dtUrutBulan As New DataTable()
			SQL = $"
				select Bulan, Urut_Oto
				from N_EMI_Transaksi_Budget_Planning_Detail
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Kode_Binding = '{KodeBinding}'
				and tahun = '{SelectedTahun}'
				and ID_Kategori_Jenis = '{IDlayer1}'
			"
			Using Ds = BindingTrans(SQL)
				dtUrutBulan = Ds.Tables("MyTable").Copy()
			End Using

			'==============================================================================
			'=     CEK DATA APAKAH SUDAH ADA DI TABEL TRANSAKSI, JIKA ADA MAKA UPDATE     =
			'==============================================================================
			If ListLayer3.Count > 0 Then
				Dim dtEksis As New DataTable()
				SQL = $"
					select a.Kode_Binding, b.ID_Kategori_Jenis, c.ID_Sub_Kategori_Jenis_1, b.Bulan,
						b.Urut_Oto as Urut_Detail, c.Urut_Oto as UrutDet,
						c.Jumlah_Budget
					from N_EMI_Transaksi_Budget_Planning a
						inner join N_EMI_Transaksi_Budget_Planning_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Binding = b.Kode_Binding
						inner join N_EMI_Transaksi_Budget_Planning_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Binding = c.Kode_Binding and b.Urut_Oto = c.Urut_Detail
					where a.Status is null
					and a.Kode_Perusahaan = '{KodePerusahaan}'
					and a.Kode_Binding = '{KodeBinding}'
					and b.Tahun = '{SelectedTahun}'
					and b.Bulan between {SelectedBulanAwal } and {SelectedBulanAkhir}
					and b.ID_Kategori_Jenis = '{IDlayer1}'
					and c.ID_Sub_Kategori_Jenis_1 in ({FilterListLayer3})
				"
				Using Ds = BindingTrans(SQL)
					dtEksis = Ds.Tables("MyTable").Copy()
				End Using

				' Loop data eksis untuk UPDATE (Gunakan StringBuilder jika ribuan, atau laksanakan langsung)
				For Each row As DataRow In dtEksis.Rows
					Dim ID_Sub_Kategori_Jenis_1 As Integer = row("ID_Sub_Kategori_Jenis_1")
					Dim Bulan As Integer = row("Bulan")
					Dim Jumlah_Budget_Awal As Double = Val(HilangkanTanda(row("Jumlah_Budget")))
					Dim UrutDet As Integer = row("UrutDet")

					Dim dataEksis As DetailDataUpdate = DataUpdate.FirstOrDefault(Function(x) x.ID_Layer3 = ID_Sub_Kategori_Jenis_1 And x.Bulan = Bulan And x.FlagUpdate = False)

					If dataEksis IsNot Nothing Then
						'======================
						'=     INSERT LOG     =
						'======================
						SQL = $"
							INSERT INTO N_EMI_Transaksi_Budget_Planning_Log
								(Kode_Perusahaan, Kode_Binding, ID_Kategori_Jenis, ID_Sub_Kategori_Jenis_1, Jenis, Bulan, Tahun, Jumlah_Budget, Jumlah_Update, Nominal_Update,
									Nominal_Budget, Tanggal, Jam, UserID)
							VALUES
								('{KodePerusahaan}', '{KodeBinding}', '{IDlayer1}', '{ID_Sub_Kategori_Jenis_1}', 'UPDATE',
								'{Bulan}', '{SelectedTahun}', '{Jumlah_Budget_Awal}', '{dataEksis.JumlahUpdate}', '0', '0',
								{Format(tgl_skg, "yyyy-MM-dd")}, '{Format(tgl_skg, "HH:mm:ss")}', '{UserID}')
"
						ExecuteTrans(SQL)

						'======================
						'=     UPDATE DET     =
						'======================
						SQL = $"
							update N_EMI_Transaksi_Budget_Planning_Det set Jumlah_Budget = {Val(HilangkanTanda(dataEksis.JumlahUpdate))}
							where Kode_Perusahaan = '{KodePerusahaan}'
							and Kode_Binding = '{KodeBinding}'
							and ID_Kategori_Jenis = '{IDlayer1}'
							and ID_Sub_Kategori_Jenis_1 = '{ID_Sub_Kategori_Jenis_1}'
							and Urut_Oto = '{UrutDet}'
						"
						ExecuteTrans(SQL)

						dataEksis.FlagUpdate = True
					End If
				Next

				For Each item As DetailDataUpdate In DataUpdate.Where(Function(x) x.FlagUpdate = False)

					Dim rowUrut As DataRow() = dtUrutBulan.Select($"Bulan = {item.Bulan}")

					If rowUrut.Length > 0 Then
						Dim UrutDetail As Integer = rowUrut(0)("Urut_Oto")

						SQL = $"
							INSERT INTO N_EMI_Transaksi_Budget_Planning_Det
								(Kode_Perusahaan, Kode_Binding, ID_Kategori_Jenis, ID_Sub_Kategori_Jenis_1, Jumlah_Budget, Nominal_Budget,
								 Urut_Detail)
							VALUES
								('{KodePerusahaan}', '{KodeBinding}', '{IDlayer1}', '{item.ID_Layer3}',
								'{Val(HilangkanTanda(item.JumlahUpdate))}', 0, {UrutDetail});
						"
						ExecuteTrans(SQL)
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Data Detail untuk Bulan {item.Bulan} Tidak Ditemukan di Memory", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				Next

				'=========================
				'=     UPDATE PARENT     =
				'=========================
				SQL = $"
					update N_EMI_Transaksi_Budget_Planning
					set Tanggal_Update = '{Format(tgl_skg, "yyyy-MM-dd")}',
					Jam_Update = '{Format(tgl_skg, "HH:mm:ss")}',
					UserID_Update = '{UserID}'
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Kode_Binding = '{KodeBinding}'
				"
				ExecuteTrans(SQL)

				'=========================
				'=     UPDATE DETAIL     =
				'=========================
				SQL = $"
					update a
					set a.Total_Budget = b.TotalDet
					from N_EMI_Transaksi_Budget_Planning_Detail a
					inner join (
						select Kode_Binding, ID_Kategori_Jenis, Urut_Detail, SUM(Jumlah_Budget) AS TotalDet
						from N_EMI_Transaksi_Budget_Planning_Det
						where Kode_Perusahaan = '{KodePerusahaan}'
						group by Kode_Binding, ID_Kategori_Jenis, Urut_Detail
					) b on b.Kode_Binding = a.Kode_Binding
						 and b.ID_Kategori_Jenis = a.ID_Kategori_Jenis
						 and b.Urut_Detail = a.Urut_Oto
					where a.Kode_Perusahaan = '{KodePerusahaan}'
					and a.Kode_Binding = '{KodeBinding}'
					and a.ID_Kategori_Jenis = '{IDlayer1}'
					and a.Tahun = '{SelectedTahun}'
					and a.Bulan between {SelectedBulanAwal} and {SelectedBulanAkhir}
				"
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

		Btn_Get_Data_Click(sender, e)

	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		Kosong()
	End Sub

	'=========================================================================================================================================
	'=     HANDLE
	'=========================================================================================================================================

	Private Sub Dgv_Data_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data.CellEndEdit
		If Dgv_Data.Rows.Count = 0 Then Exit Sub

		Dim cell As DataGridViewCell = Dgv_Data.CurrentCell

		If cell IsNot Nothing AndAlso cell.ColumnIndex > 1 Then
			Dim ValueCell As Object = cell.Value
			If Not IsNumeric(ValueCell) Then
				SetCellDataAndColor(cell, 0, PersentasePenggelapanWarnaCellInput)
				Exit Sub
			End If

			Dim IDLayer3 As Integer = Val(HilangkanTanda(Dgv_Data.CurrentRow.Cells(0).Value))
			Dim Bulan As Integer = Val(HilangkanTanda(cell.OwningColumn.Tag))

			Dim numericValue As Double = Val(HilangkanTanda(ValueCell))

			SetCellDataAndColor(cell, numericValue, PersentasePenggelapanWarnaCellInput)

			CheckValueChandedCell(IDLayer3, Bulan, ValueCell)

		End If

	End Sub

	Private Sub Dgv_Data_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data.CellClick
		If Dgv_Data.Rows.Count = 0 Then Exit Sub

		If Dgv_Data.CurrentCell.ColumnIndex > 1 Then
			'======================
			'=     SET FORMAT     =
			'======================

			If Dgv_Data.CurrentCell.ColumnIndex > 1 Then
				Dim cellKuantity As String = Dgv_Data.CurrentCell.Value

				If cellKuantity = "" Then
					Exit Sub
				End If

				Dim nilai As Decimal = Val(HilangkanTanda(cellKuantity))

				Dgv_Data.CurrentCell.Value = nilai
			End If
		End If

	End Sub

	Private Sub Cmb_Department_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Department.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Kategori.DroppedDown = True
			Cmb_Kategori.Focus()
		End If
	End Sub

	Private Sub Cmb_Kategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Kategori.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Tahun.DroppedDown = True
			Cmb_Tahun.Focus()
		End If
	End Sub

	Private Sub Cmb_Tahun_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Tahun.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Bulan_Awal.DroppedDown = True
			Cmb_Bulan_Awal.Focus()
		End If
	End Sub

	Private Sub Cmb_Bulan_Awal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Bulan_Awal.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Bulan_Akhir.DroppedDown = True
			Cmb_Bulan_Akhir.Focus()
		End If
	End Sub

	Private Sub Cmb_Bulan_Akhir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Bulan_Akhir.KeyPress
		If e.KeyChar = Chr(13) Then Btn_Get_Data.Focus()
	End Sub

	Private Sub Dgv_Data_MouseMove(sender As Object, e As MouseEventArgs) Handles Dgv_Data.MouseMove
		HandleDataGridViewHover(sender, e)
	End Sub

	'=========================================================================================================================================
	'=     HELPER
	'=========================================================================================================================================
	Private Class DetailDataLayer3
		Public Property keteranganKategoriLayer1 As String
		Public Property IDKategoriLayer1 As String
		Public Property keteranganKategoriLayer3 As String
	End Class

	Private Class DetailDataBudgetPlanning
		Public Property Keterangan_Layer3 As String
		Public Property Kode_Binding As String
		Public Property ID_Kategori_Jenis As Integer
		Public Property Bulan As Integer
		Public Property Tahun As Integer
		Public Property Jumlah_Budget As Double
		Public Property Jumlah_Tambah As Double
		Public Property Jumlah_PR As Double
		Public Property Jumlah_Transfer As Double

	End Class

	Private Class DetailDataUpdate
		Public Property ID_Layer3 As Integer
		Public Property Bulan As Integer
		Public Property JumlahAwal As Double
		Public Property JumlahUpdate As Double
		Public Property FlagUpdate As Boolean = False

	End Class

	Private Class ModelCekBulan
		Public Property Bulan As Integer
		Public Property FlagHasDataInDB As Boolean = False
	End Class

	Private Sub CheckValueChandedCell(ByVal IDLayer3 As Integer, ByVal Bulan As Integer, ByVal QtyUbah As Double)

		'============================
		'=     GET DATA DEFAULT     =
		'============================
		DataUpdate.RemoveAll(Function(x) x.ID_Layer3 = IDLayer3 AndAlso x.Bulan = Bulan)

		Dim JumlahDefault As Double = 0
		Dim DataBudget As DetailDataBudgetPlanning = Nothing

		If DataBudgetPlanning.ContainsKey(IDLayer3) Then
			DataBudget = DataBudgetPlanning(IDLayer3).FirstOrDefault(Function(x) x.Bulan = Bulan)
			If DataBudget IsNot Nothing Then
				JumlahDefault = DataBudget.Jumlah_Budget
			End If
		End If

		Dim valQtyUbah As Double = Val(HilangkanTanda(QtyUbah))

		If valQtyUbah <> JumlahDefault Then
			Dim DataDetailUpdate As New DetailDataUpdate With {
				.ID_Layer3 = IDLayer3,
				.Bulan = Bulan,
				.JumlahAwal = JumlahDefault,
				.JumlahUpdate = valQtyUbah
			}
			DataUpdate.Add(DataDetailUpdate)
		End If

	End Sub

	Private Sub Dgv_Data_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles Dgv_Data.CellPainting
		' 1. Saring Instan: Abaikan baris data biasa agar tidak lag
		If e.RowIndex <> -1 Then Exit Sub

		Dim dgv As DataGridView = DirectCast(sender, DataGridView)
		Dim colIdx As Integer = e.ColumnIndex

		' PROTEKSI: Abaikan jika indeks kolom tidak valid (Mencegah error klik pojok kiri atas / Top-Left Cell)
		If colIdx < 0 Then Exit Sub

		' 2. Atur Kolom Master (Index < 2) agar teks turun ke tengah bawah secara simetris
		If colIdx < 2 Then
			If colIdx >= 0 AndAlso e.Value IsNot Nothing Then
				e.PaintBackground(e.CellBounds, True)
				Dim sfMaster As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
				Using brushText As New SolidBrush(dgv.ColumnHeadersDefaultCellStyle.ForeColor)
					e.Graphics.DrawString(e.Value.ToString(), dgv.ColumnHeadersDefaultCellStyle.Font, brushText, e.CellBounds, sfMaster)
				End Using
				e.Graphics.DrawRectangle(Pens.DarkGray, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height)
				e.Handled = True
			End If
			Exit Sub
		End If

		' 3. Proses Penggabungan Header untuk Kolom Bulan (Index >= 2)
		Dim col As DataGridViewColumn = dgv.Columns(colIdx)
		Dim kodeBulan As Integer = 0

		' [REVISI]: Gunakan TryParse agar aman dari exception saat cast object Tag
		If col.Tag IsNot Nothing AndAlso Integer.TryParse(col.Tag.ToString(), kodeBulan) Then

			If kodeBulan >= 1 AndAlso kodeBulan <= 12 Then
				' Hitung index kolom pertama & terakhir untuk bulan ini secara matematika absolut
				Dim colOffset As Integer = colIdx - 2
				Dim firstColIndex As Integer = colIdx - (colOffset Mod 4)
				Dim lastColIndex As Integer = firstColIndex + 3

				' Cari nama bulan berdasarkan list DataBulan kustom Anda
				Dim bulanMatch = DataBulan.FirstOrDefault(Function(b) b.Sql = kodeBulan.ToString())
				Dim namaBulanTarget As String = If(bulanMatch.NamaBulan IsNot Nothing, bulanMatch.NamaBulan, "Bulan " & kodeBulan)

				' Hitung lebar total grup bulan HANYA untuk kolom yang benar-benar tampil di layar saat ini
				Dim totalWidth As Integer = 0
				Dim firstVisibleLeft As Integer = -1

				For idx As Integer = firstColIndex To lastColIndex
					If idx < dgv.Columns.Count Then
						Dim rectCol As Rectangle = dgv.GetColumnDisplayRectangle(idx, True)
						If rectCol.Width > 0 Then
							If firstVisibleLeft = -1 Then firstVisibleLeft = rectCol.X
							totalWidth += rectCol.Width
						End If
					End If
				Next

				' Jika seluruh kolom bulan ini tersembunyi di luar scroll, tidak perlu digambar
				If firstVisibleLeft = -1 Then Exit Sub

				' Tentukan area pembagian Header Atas (Bulan) dan Bawah (Sub-Header)
				Dim midHeight As Integer = e.CellBounds.Height \ 2

				Dim rectHeaderAtasGrup As New Rectangle(firstVisibleLeft, e.CellBounds.Y, totalWidth, midHeight)
				Dim rectHeaderAtasCell As New Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, midHeight)
				Dim rectHeaderBawah As New Rectangle(e.CellBounds.X, e.CellBounds.Y + midHeight, e.CellBounds.Width, midHeight)

				' -----------------------------------------------------------------
				' PROSES GAMBAR BERSIH (Anti-Ghosting / Anti-Scroll Bug)
				' -----------------------------------------------------------------
				' A. Gambar Latar Belakang Sub-Header Bawah (Putih Abu-abu)
				Using brushBgBawah As New SolidBrush(Color.WhiteSmoke)
					e.Graphics.FillRectangle(brushBgBawah, rectHeaderBawah)
				End Using

				' Tulis teks sub-header bawaan kolom (Qty Budget, Qty Tambah, dll)
				If e.Value IsNot Nothing Then
					Dim sfSub As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
					Using brushTextSub As New SolidBrush(dgv.ColumnHeadersDefaultCellStyle.ForeColor)
						e.Graphics.DrawString(e.Value.ToString(), dgv.ColumnHeadersDefaultCellStyle.Font, brushTextSub, rectHeaderBawah, sfSub)
					End Using
				End If

				' B. Gambar Latar Belakang Header Atas (Setiap sel ikut menghapus sisa gambar lama)
				Using brushBgAtas As New SolidBrush(Color.Gainsboro)
					e.Graphics.FillRectangle(brushBgAtas, rectHeaderAtasCell)
				End Using

				' C. Cetak Nama Bulan
				Dim sfAtas As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
				Using brushTextAtas As New SolidBrush(dgv.ColumnHeadersDefaultCellStyle.ForeColor)
					e.Graphics.DrawString(namaBulanTarget, dgv.ColumnHeadersDefaultCellStyle.Font, brushTextAtas, rectHeaderAtasGrup, sfAtas)
				End Using

				' D. Gambar Ulang Garis Kisi (Gridlines) Pembatas Agar Tetap Tajam Saat Di-scroll
				' Garis horizontal tengah pemisah tingkat
				e.Graphics.DrawLine(Pens.DarkGray, e.CellBounds.Left, rectHeaderAtasCell.Bottom, e.CellBounds.Right, rectHeaderAtasCell.Bottom)

				' Sekat vertikal bagian bawah (Pemisah antar sub-header)
				e.Graphics.DrawLine(Pens.DarkGray, e.CellBounds.Left, rectHeaderBawah.Y, e.CellBounds.Left, rectHeaderBawah.Bottom)
				e.Graphics.DrawLine(Pens.DarkGray, e.CellBounds.Right - 1, rectHeaderBawah.Y, e.CellBounds.Right - 1, rectHeaderBawah.Bottom)

				' Garis horizontal penutup paling bawah
				e.Graphics.DrawLine(Pens.DarkGray, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1)

				' Kunci Garis Tebal Samping sebagai pembatas antar Bulan
				If colIdx = lastColIndex Then
					e.Graphics.DrawLine(Pens.DarkGray, e.CellBounds.Right - 1, e.CellBounds.Y, e.CellBounds.Right - 1, e.CellBounds.Bottom)
				End If
				If colIdx = firstColIndex Then
					e.Graphics.DrawLine(Pens.DarkGray, e.CellBounds.Left, e.CellBounds.Y, e.CellBounds.Left, e.CellBounds.Bottom)
				End If

				e.Handled = True
			End If
		End If
	End Sub

	Private Sub SetCellDataAndColor(cell As DataGridViewCell, value As Double, persentaseGelap As Integer)

		cell.Value = value

		If value <> 0 Then
			If cell.Style.BackColor = Color.Empty Then
				Dim baseColor As Color = cell.InheritedStyle.BackColor
				If baseColor.ToArgb() = Color.Empty.ToArgb() Then baseColor = Color.White

				cell.Style.BackColor = Color.FromArgb(
				Math.Max(0, baseColor.R - persentaseGelap),
				Math.Max(0, baseColor.G - persentaseGelap),
				Math.Max(0, baseColor.B - persentaseGelap)
			)
			End If
		Else
			cell.Style.BackColor = Color.Empty
		End If
	End Sub

	Private Sub EnableDoubleBufferDGV(dgv As DataGridView)
		Dim t As Type = dgv.GetType()
		Dim prop = t.GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
		prop.SetValue(dgv, True, Nothing)

		'' =========================================================================
		'' PAKSA DATAGRIDVIEW MENGGUNAKAN MEMORI BAYANGAN (DOUBLE BUFFERED)
		'' =========================================================================
		'' Ini akan menghilangkan 100% bug teks bertumpuk/berkedip saat di-scroll
		'Dim dgvType As Type = dgv.GetType()
		'Dim pi As System.Reflection.PropertyInfo = dgvType.GetProperty("DoubleBuffered",
		'System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)

		'If pi IsNot Nothing Then
		'	pi.SetValue(dgv, True, Nothing)
		'End If
		'' =========================================================================

	End Sub

	Private Sub Dgv_Data_Scroll(sender As Object, e As ScrollEventArgs) Handles Dgv_Data.Scroll
		If e.ScrollOrientation = ScrollOrientation.HorizontalScroll Then
			For i As Integer = 2 To Dgv_Data.Columns.Count - 1
				Dgv_Data.InvalidateCell(i, -1)
			Next

		End If
	End Sub

	Private Sub HandleDataGridViewHover(dgv As DataGridView, e As MouseEventArgs)
		Dim hit As DataGridView.HitTestInfo = dgv.HitTest(e.X, e.Y)

		dgv.Cursor = If(hit.Type = DataGridViewHitTestType.Cell, Cursors.Hand, Cursors.Default)
	End Sub

End Class