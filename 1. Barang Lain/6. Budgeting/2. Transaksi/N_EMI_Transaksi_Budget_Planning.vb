Public Class N_EMI_Transaksi_Budget_Planning

	Dim DataDepartment As New Dictionary(Of String, String)
	Dim DataLayer1 As New Dictionary(Of Integer, String)
	Dim DataLayer3 As New Dictionary(Of Integer, List(Of DetailDataLayer3))
	Dim DataBudgetPlanning As New Dictionary(Of (ID_Sub_Kategori_1 As Integer, Tahun As Integer, Bulan As Integer), DetailDataBudgetPlanning)
	Dim ListRoleAksesInput As New List(Of RoleAksesInput)

	Dim DataUpdate As New List(Of DetailDataUpdate)

	Dim BatasBulanInput As Integer = 120

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
		Cmb_Kategori.SelectedIndex = -1
		Cmb_Kategori.Enabled = False

		Dtp_PeriodeAwal.Value = New DateTime(Now.Year, Now.Month, 1)
		Dtp_PeriodeAkhir.Value = New DateTime(Now.Year, Now.Month, 1)

		Dtp_PeriodeAwal.Value = Now
		Dtp_PeriodeAkhir.Value = Now

		DataUpdate.Clear()
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
							DataLayer3.Add(IDLayer3, New List(Of DetailDataLayer3) From {DetailLayer3})
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

			'===========================
			'=     LOAD ROLE AKSES     =
			'===========================
			ListRoleAksesInput.Clear()
			SQL = $"
				select Tanggal_Awal, Tanggal_Akhir, Bulan, M1, M2, M3, M4, M5, M6, M7, M8, M9, M10, M11, M12
				from N_EMI_Master_Role_Akses_Budget_Planning
				where Kode_Perusahaan = '{KodePerusahaan}'
				and UserID = '{UserID}'
				and Bulan = '{CurrentMonth}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						ListRoleAksesInput.Add(New RoleAksesInput With {
							.TanggalAwal = HilangkanTanda(Dr("Tanggal_Awal")),
							.TanggalAkhir = HilangkanTanda(Dr("Tanggal_Akhir")),
							.Bulan = HilangkanTanda(Dr("Bulan")),
							.M1 = If(General_Class.CekNULL(Dr("M1")) = "Y", True, False),
							.M2 = If(General_Class.CekNULL(Dr("M2")) = "Y", True, False),
							.M3 = If(General_Class.CekNULL(Dr("M3")) = "Y", True, False),
							.M4 = If(General_Class.CekNULL(Dr("M4")) = "Y", True, False),
							.M5 = If(General_Class.CekNULL(Dr("M5")) = "Y", True, False),
							.M6 = If(General_Class.CekNULL(Dr("M6")) = "Y", True, False),
							.M7 = If(General_Class.CekNULL(Dr("M7")) = "Y", True, False),
							.M8 = If(General_Class.CekNULL(Dr("M8")) = "Y", True, False),
							.M9 = If(General_Class.CekNULL(Dr("M9")) = "Y", True, False),
							.M10 = If(General_Class.CekNULL(Dr("M10")) = "Y", True, False),
							.M11 = If(General_Class.CekNULL(Dr("M11")) = "Y", True, False),
							.M12 = If(General_Class.CekNULL(Dr("M12")) = "Y", True, False)
						})
					Loop While Dr.Read
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Terjadi Kesalahan, Data Role Akses Input Tidak Ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
			'==========================================
			'=     LOAD INITIAL KOLOM DATAGRIDVIEW    =
			'==========================================
			Dgv_Data.SuspendLayout()
			Dgv_Data.Columns.Clear()

			Dim colID As New DataGridViewTextBoxColumn() With {.Name = "ID_Sub_Kategori_Jenis_1", .HeaderText = "ID", .Frozen = True, .ReadOnly = True, .Visible = False}
			Dgv_Data.Columns.Add(colID)

			Dim colKategori As New DataGridViewTextBoxColumn() With {.Name = "Layer_3", .HeaderText = "Kategori Layer 3", .Width = 200, .Frozen = True, .ReadOnly = True}
			Dgv_Data.Columns.Add(colKategori)

			Dgv_Data.ResumeLayout()
		Catch ex As Exception
			MessageBox.Show(ex.Message)
		End Try
	End Sub

	Private Sub GetDataBudget()
		Dim SelectedindexCMB As Integer = Cmb_Department.SelectedIndex
		Dim SelectedDepartment_KodeBinding As String = DataDepartment.Keys.ToList()(SelectedindexCMB)
		Dim SelectedIDLayer1 As Integer = DataLayer1.Keys.ToList()(Cmb_Kategori.SelectedIndex)

		Dim SelectedTahunAwal As Integer = Dtp_PeriodeAwal.Value.Year
		Dim SelectedBulanAwal As Integer = Dtp_PeriodeAwal.Value.Month
		Dim SelectedTahunAkhir As Integer = Dtp_PeriodeAkhir.Value.Year
		Dim SelectedBulanAkhir As Integer = Dtp_PeriodeAkhir.Value.Month

		Dim PeriodeAwal As Integer = (SelectedTahunAwal * 100) + SelectedBulanAwal
		Dim PeriodeAkhir As Integer = (SelectedTahunAkhir * 100) + SelectedBulanAkhir

		If PeriodeAkhir < PeriodeAwal Then
			MessageBox.Show("Periode akhir tidak boleh lebih kecil dari periode awal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
			Exit Sub
		End If

		Dim ListDataPRM As New Dictionary(Of (Kode_Binding As String, Tahun As Integer, Bulan As Integer, IDLayer1 As Integer, IDLayer3 As Integer), Double)
		Dim ListDataTF As New Dictionary(Of (Kode_Binding As String, Tahun As Integer, Bulan As Integer, IDLayer1 As Integer, IDLayer3 As Integer), (JumlahTf As Double, NominalTF As Double))

		'========================================
		'=     GET DATA BESAR DARI DATABASE     =
		'========================================
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'========================
			'=     GET DATA PRM     =
			'========================
			SQL = $"
				;with DataPRM as (
									 select a.Kode_Perusahaan,
											ISNULL(YEAR(a.Tanggal), 0) as Tahun,
											ISNULL(MONTH(a.Tanggal), 0) as Bulan,
											d.ID_Kategori_Jenis,
											d.Id_Sub_Kategori_Jenis_1,
											a.Kode_Kategori_Gudang,
											sum(b.jumlah) as JumlahPR
									 from N_EMI_Purchase_Requisition_Barang_Lain_Departement a
										  inner join N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail b
													 on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
										  inner join barang_lain c
													 on b.Kode_Perusahaan = c.Kode_Perusahaan and
														b.Kode_Stock_Owner = c.Kode_Stock_Owner and
														b.Kode_Barang = c.Kode_Barang
										  inner join View_Kategori_Turunan d
													 on b.Kode_Perusahaan = d.Kode_Perusahaan and
														c.Id_Sub_Kategori_Jenis_3 = d.Id_Sub_Kategori_Jenis_3
									 where a.Status is null
									   and a.Flag_Pra_Release = 'Y'
									   and a.Kode_Perusahaan = '{KodePerusahaan}'
									   and (ISNULL(YEAR(a.Tanggal), 0) * 100 + ISNULL(MONTH(a.Tanggal), 0)) between {PeriodeAwal} and {PeriodeAkhir}
									   and d.ID_Kategori_Jenis = '{SelectedIDLayer1}'
									 group by a.Kode_Perusahaan, a.Tanggal, d.ID_Kategori_Jenis, d.Id_Sub_Kategori_Jenis_1,
											  a.Kode_Kategori_Gudang
								 )
				 select z.Kode_Perusahaan,
						z.Kode_Binding,
						y.Tahun, y.Bulan,
						y.ID_Kategori_Jenis,
						y.Id_Sub_Kategori_Jenis_1,
						sum(y.JumlahPR) as JumlahPR
				 from N_EMI_Binding_Department z
					  inner join N_EMI_Binding_Department_Detail x
								 on z.Kode_Perusahaan = x.Kode_Perusahaan and
									z.Kode_Binding = x.Kode_Binding
					  inner join DataPRM y
								 on x.Kode_Perusahaan = y.Kode_Perusahaan and
									x.Kode_Kategori_Gudang = y.Kode_Kategori_Gudang
				 where z.Status is null
				   and z.Kode_Perusahaan = '{KodePerusahaan}'
				   and z.Kode_Binding = '{SelectedDepartment_KodeBinding}'
				 group by z.Kode_Perusahaan, y.Tahun, y.Bulan, z.Kode_Binding,
						  y.ID_Kategori_Jenis,
						  y.Id_Sub_Kategori_Jenis_1
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim Key = (
							Kode_Binding:=Dr("Kode_Binding"),
							Tahun:=Dr("Tahun"),
							Bulan:=Dr("Bulan"),
							IDLayer1:=Dr("ID_Kategori_Jenis"),
							IDLayer3:=Dr("Id_Sub_Kategori_Jenis_1")
						)

						ListDataPRM.Add(Key, Val(HilangkanTanda(Dr("JumlahPR"))))
					Loop While Dr.Read
				End If
			End Using

			'=========================================
			'=     DATA TRANSFER DAN PENGELUARAN     =
			'=========================================
			SQL = $"
				;with DataPRM as (
									 select a.Kode_Perusahaan,
											d.ID_Kategori_Jenis,
											d.Id_Sub_Kategori_Jenis_1,
											a.Kode_Kategori_Gudang,
											b.No_Urut
									 from N_EMI_Purchase_Requisition_Barang_Lain_Departement a
										  inner join N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail b
													 on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
										  inner join barang_lain c
													 on b.Kode_Perusahaan = c.Kode_Perusahaan and
														b.Kode_Stock_Owner = c.Kode_Stock_Owner and
														b.Kode_Barang = c.Kode_Barang
										  inner join View_Kategori_Turunan d
													 on b.Kode_Perusahaan = d.Kode_Perusahaan and
														c.Id_Sub_Kategori_Jenis_3 = d.Id_Sub_Kategori_Jenis_3
									 where a.Status is null
									   and a.Flag_Pra_Release = 'Y'
									   and a.Kode_Perusahaan = '{KodePerusahaan}'
									   and d.ID_Kategori_Jenis = '{SelectedIDLayer1}'
									 group by a.Kode_Perusahaan, a.Tanggal, d.ID_Kategori_Jenis, d.Id_Sub_Kategori_Jenis_1,
											  a.Kode_Kategori_Gudang, b.No_Urut
								 ),
					  DataTF as (
									 select z.Kode_Perusahaan,
											l.Kode_Kategori_Gudang,
											ISNULL(YEAR(z.Tanggal), 0) as Tahun,
											ISNULL(MONTH(z.Tanggal), 0) as Bulan,
											l.ID_Kategori_Jenis,
											l.Id_Sub_Kategori_Jenis_1,
											sum(k.Jumlah) as TotalJumlahTF,
									        sum(dbo.get_hpp(m.Serial_Number)) as TotalNominal
									 from N_EMI_Transfer_Stock_Barang_Lain z
										  inner join N_EMI_Transfer_Stock_Barang_Lain_Detail x
													 on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
										  inner join N_EMI_Transfer_Stock_Barang_Lain_Det y
													 on x.Kode_Perusahaan = y.Kode_Perusahaan and
														x.No_Faktur = y.No_Faktur and
														x.Urut_Oto = y.Urut_TF
										  inner join N_EMI_Transfer_Stock_Barang_Lain_Det2 k
													 on y.Kode_Perusahaan = k.Kode_Perusahaan and
														y.No_Faktur = k.No_Faktur and
														y.Urut_Oto = k.Urut_Det
										  inner join DataPRM l
													 on x.Kode_Perusahaan = l.Kode_Perusahaan and x.Urut_PR_Dept = l.No_Urut
									      inner join Barang_Lain_SN m on k.Kode_Perusahaan = m.Kode_Perusahaan and k.Serial_Number = m.Serial_Number
									 where z.Status is null
									   and y.Selesai = 'Y'
									   and z.Kode_Perusahaan = '{KodePerusahaan}'
									   and (ISNULL(YEAR(z.Tanggal), 0) * 100 + ISNULL(MONTH(z.Tanggal), 0)) between {PeriodeAwal} and {PeriodeAkhir}
									 group by z.Kode_Perusahaan, l.Kode_Kategori_Gudang, z.Tanggal,
											  l.ID_Kategori_Jenis,
											  l.Id_Sub_Kategori_Jenis_1
								 ),
					  DataPengeluaran as (
									 select a.Kode_Perusahaan,
											l.Kode_Kategori_Gudang,
											ISNULL(YEAR(a.Tanggal), 0) as Tahun,
											ISNULL(MONTH(a.Tanggal), 0) as Bulan,
											l.ID_Kategori_Jenis,
											l.Id_Sub_Kategori_Jenis_1,
											sum(c.Jumlah) as TotalJumlahPengeluaran,
											sum(dbo.get_hpp(m.Serial_Number)) as TotalNominal
									 from EMI_Pengeluaran_Stock_parent_barang_lain a
										  inner join EMI_Pengeluaran_Stock_barang_lain b
													 on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
										  inner join EMI_Pengeluaran_Stock_Det_Barang_Lain c
													 on b.Kode_Perusahaan = c.Kode_Perusahaan and
														b.No_Faktur = c.No_Faktur and
														b.Urut_Oto = c.Urut_TF
										  inner join DataPRM l
													 on b.Kode_Perusahaan = l.Kode_Perusahaan and b.Urut_PR_Dept = l.No_Urut
									      inner join Barang_Lain_SN m on c.Kode_Perusahaan = m.Kode_Perusahaan and c.Serial_Number_Awal = m.Serial_Number
									 where a.Status is null
									   and a.Kode_Perusahaan = '{KodePerusahaan}'
									   and c.Selesai = 'Y'
									   and (ISNULL(YEAR(a.Tanggal), 0) * 100 + ISNULL(MONTH(a.Tanggal), 0)) between {PeriodeAwal} and {PeriodeAkhir}
									 group by a.Kode_Perusahaan, a.Tanggal, l.Kode_Kategori_Gudang,
											  l.ID_Kategori_Jenis,
											  l.Id_Sub_Kategori_Jenis_1
								 ),
					  Data_Union as (
									 select Kode_Perusahaan, Tahun, Bulan, Kode_Kategori_Gudang, ID_Kategori_Jenis,
											Id_Sub_Kategori_Jenis_1,
											TotalJumlahTF as Jumlah,
											TotalNominal as Nominal
									 from DataTF

									 union all

									 select Kode_Perusahaan, Tahun, Bulan, Kode_Kategori_Gudang, ID_Kategori_Jenis,
											Id_Sub_Kategori_Jenis_1,
											TotalJumlahPengeluaran as Jumlah,
											TotalNominal as Nominal
									 from DataPengeluaran
								 )
				 select z.Kode_Perusahaan,
						z.Kode_Binding,
						y.Tahun, y.Bulan,
						y.ID_Kategori_Jenis,
						y.Id_Sub_Kategori_Jenis_1,
						sum(y.Jumlah) as JumlahTF,
						sum(y.Nominal) as NominalTF
				 from N_EMI_Binding_Department z
					  inner join N_EMI_Binding_Department_Detail x
								 on z.Kode_Perusahaan = x.Kode_Perusahaan and
									z.Kode_Binding = x.Kode_Binding
					  inner join Data_Union y
								 on x.Kode_Perusahaan = y.Kode_Perusahaan and
									x.Kode_Kategori_Gudang = y.Kode_Kategori_Gudang
				 where z.Status is null
				   and z.Kode_Perusahaan = '{KodePerusahaan}'
				   and z.Kode_Binding = '{SelectedDepartment_KodeBinding}'
				 group by z.Kode_Perusahaan, y.Tahun, y.Bulan, z.Kode_Binding,
						  y.ID_Kategori_Jenis,
						  y.Id_Sub_Kategori_Jenis_1
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim Key = (
							Kode_Binding:=Dr("Kode_Binding"),
							Tahun:=Dr("Tahun"),
							Bulan:=Dr("Bulan"),
							IDLayer1:=Dr("ID_Kategori_Jenis"),
							IDLayer3:=Dr("Id_Sub_Kategori_Jenis_1")
						)

						ListDataTF.Add(Key, (Val(HilangkanTanda(Dr("JumlahTF"))), Val(HilangkanTanda(Dr("NominalTF")))))
					Loop While Dr.Read
				End If
			End Using

			'==============================
			'=     GET DATA BUDGETING     =
			'==============================
			SQL = $"
				select a.Kode_Binding,
					   b.ID_Kategori_Jenis,
					   c.ID_Sub_Kategori_Jenis_1,
					   d.Keterangan as Layer_3,
					   b.Bulan,
					   b.Tahun,
					   c.Jumlah_Budget,
					   c.Nominal_Budget,
					   c.Urut_Oto
				from N_EMI_Transaksi_Budget_Planning a
					 inner join N_EMI_Transaksi_Budget_Planning_Detail b
								on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Binding = b.Kode_Binding
					 inner join N_EMI_Transaksi_Budget_Planning_Det c
								on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Binding = c.Kode_Binding
									and b.ID_Kategori_Jenis = c.ID_Kategori_Jenis and b.Urut_Oto = c.Urut_Detail
					 inner join N_EMI_Master_Sub_Kategori_Jenis_1 d
								on c.Kode_Perusahaan = d.Kode_Perusahaan and
								   c.ID_Sub_Kategori_Jenis_1 = d.Id_Sub_Kategori_Jenis_1
				where a.Status is null
				  and d.Flag_Is_Budget = 'Y'
				  and a.Kode_Perusahaan = '{KodePerusahaan}'
				  and a.Kode_Binding = '{SelectedDepartment_KodeBinding}'
				  and b.ID_Kategori_Jenis = '{SelectedIDLayer1}'
				  and (b.Tahun * 100 + b.Bulan) between {PeriodeAwal} and {PeriodeAkhir}
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim kodeBinding As String = Dr("Kode_Binding").ToString().Trim()
						Dim tahun As Integer = Val(HilangkanTanda(Dr("Tahun")))
						Dim bulan As Integer = Val(HilangkanTanda(Dr("Bulan")))
						Dim idKategori As Integer = Val(HilangkanTanda(Dr("ID_Kategori_Jenis")))
						Dim ID_Sub_Kategori_1 As Integer = Val(HilangkanTanda(Dr("ID_Sub_Kategori_Jenis_1")))
						Dim jumlahBudget As Integer = Val(HilangkanTanda(Dr("Jumlah_Budget")))
						Dim nominalBudget As Integer = Val(HilangkanTanda(Dr("Nominal_Budget")))

						Dim Key = (kodeBinding, tahun, bulan, idKategori, ID_Sub_Kategori_1)
						Dim KeyBudget = (ID_Sub_Kategori_1, tahun, bulan)

						Dim JumlahPR As Double = 0
						Dim JumlahTF As Double = 0
						Dim NominalTF As Double = 0

						ListDataPRM.TryGetValue(Key, JumlahPR)
						ListDataTF.TryGetValue(Key, (JumlahTF, NominalTF))

						Dim selisih As Double = jumlahBudget - JumlahPR

						Dim dataDetailBudget As New DetailDataBudgetPlanning With {
							.Keterangan_Layer3 = Dr("Layer_3").ToString().Trim(),
							.Kode_Binding = kodeBinding,
							.ID_Kategori_Jenis = idKategori,
							.Bulan = bulan,
							.Tahun = tahun,
							.Jumlah_Budget = jumlahBudget,
							.Nominal_Budget = nominalBudget,
							.Jumlah_Selisih = selisih,
							.Jumlah_PR = JumlahPR,
							.Jumlah_Transfer = JumlahTF,
							.Nominal_Transfer = NominalTF
						}

						'JIKA DATA ADA MAKA UPDATE JIKA TIDAK MAKA ADD OTOMATIS
						DataBudgetPlanning(KeyBudget) = dataDetailBudget

					Loop While Dr.Read

				End If
			End Using

			'=====================
			'=     KODE LAMA     =
			'=====================

#Region "KODE LAMA"

			''============================================================
			''=     GET ALL DATA BERDASARKAN DEPARTMENT YANG DIPILIH     =
			''============================================================
			'DataBudgetPlanning.Clear() : DataUpdate.Clear()

			'SQL = $"
			'	;with
			'		-- 1. Base PRM: Dibuat seringkas mungkin dengan filter perusahaan agresif
			'		DataPRM_Base as (
			'							select a.Kode_Perusahaan,
			'								   ISNULL(YEAR(a.Tanggal), 0) as Tahun,
			'								   ISNULL(MONTH(a.Tanggal), 0) as Bulan,
			'								   b.No_Urut,
			'								   d.ID_Kategori_Jenis,
			'								   d.Id_Sub_Kategori_Jenis_1,
			'								   a.Kode_Kategori_Gudang,
			'								   b.jumlah as JumlahPR
			'							from N_EMI_Purchase_Requisition_Barang_Lain_Departement a
			'								 inner join N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail b
			'											on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
			'								 inner join barang_lain c
			'											on b.Kode_Perusahaan = c.Kode_Perusahaan and
			'											   b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang
			'								 inner join View_Kategori_Turunan d
			'											on b.Kode_Perusahaan = d.Kode_Perusahaan and
			'											   c.Id_Sub_Kategori_Jenis_3 = d.Id_Sub_Kategori_Jenis_3
			'							where a.Status is null
			'							  and a.Flag_Pra_Release = 'Y'
			'							  and a.Kode_Perusahaan = '{KodePerusahaan}'
			'						),

			'		-- 2. Ambil Agregasi PRM langsung dari Base (Tidak scan ulang tabel fisik)
			'		DataPRM_Agg as (
			'							select Kode_Perusahaan,
			'								   Tahun, Bulan,
			'								   Kode_Kategori_Gudang,
			'								   ID_Kategori_Jenis,
			'								   Id_Sub_Kategori_Jenis_1,
			'								   sum(JumlahPR) as TotalJumlahPR
			'							from DataPRM_Base
			'							group by Kode_Perusahaan, Tahun, Bulan, Kode_Kategori_Gudang, ID_Kategori_Jenis,
			'									 Id_Sub_Kategori_Jenis_1
			'						),

			'		-- 3. Agregasi Transfer Stock
			'		DataTF_Agg as (
			'							select z.Kode_Perusahaan,
			'								   l.Kode_Kategori_Gudang,
			'								   ISNULL(YEAR(z.Tanggal), 0) as Tahun,
			'								   ISNULL(MONTH(z.Tanggal), 0) as Bulan,
			'								   l.ID_Kategori_Jenis,
			'								   l.Id_Sub_Kategori_Jenis_1,
			'								   sum(k.Jumlah) as TotalJumlahTF
			'							from N_EMI_Transfer_Stock_Barang_Lain z
			'								 inner join N_EMI_Transfer_Stock_Barang_Lain_Detail x
			'											on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
			'								 inner join N_EMI_Transfer_Stock_Barang_Lain_Det y
			'											on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and
			'											   x.Urut_Oto = y.Urut_TF
			'								 inner join N_EMI_Transfer_Stock_Barang_Lain_Det2 k
			'											on y.Kode_Perusahaan = k.Kode_Perusahaan and y.No_Faktur = k.No_Faktur and
			'											   y.Urut_Oto = k.Urut_Det
			'								 inner join DataPRM_Base l
			'											on x.Kode_Perusahaan = l.Kode_Perusahaan and x.Urut_PR_Dept = l.No_Urut
			'							where z.Status is null
			'							  and y.Selesai = 'Y'
			'							  and z.Kode_Perusahaan = '{KodePerusahaan}'
			'							group by z.Kode_Perusahaan, l.Kode_Kategori_Gudang, z.Tanggal, l.ID_Kategori_Jenis,
			'									 l.Id_Sub_Kategori_Jenis_1
			'						),

			'		-- 4. Agregasi Pengeluaran Stock
			'		DataPengeluaran_Agg as (
			'							select a.Kode_Perusahaan,
			'								   l.Kode_Kategori_Gudang,
			'								   ISNULL(YEAR(a.Tanggal), 0) as Tahun,
			'								   ISNULL(MONTH(a.Tanggal), 0) as Bulan,
			'								   l.ID_Kategori_Jenis,
			'								   l.Id_Sub_Kategori_Jenis_1,
			'								   sum(c.Jumlah) as TotalJumlahPengeluaran
			'							from EMI_Pengeluaran_Stock_parent_barang_lain a
			'								 inner join EMI_Pengeluaran_Stock_barang_lain b
			'											on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
			'								 inner join EMI_Pengeluaran_Stock_Det_Barang_Lain c
			'											on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and
			'											   b.Urut_Oto = c.Urut_TF
			'								 inner join DataPRM_Base l
			'											on b.Kode_Perusahaan = l.Kode_Perusahaan and b.Urut_PR_Dept = l.No_Urut
			'							where a.Status is null
			'							  and a.Kode_Perusahaan = '{KodePerusahaan}'
			'							  and c.Selesai = 'Y'
			'							group by a.Kode_Perusahaan, a.Tanggal, l.Kode_Kategori_Gudang, l.ID_Kategori_Jenis,
			'									 l.Id_Sub_Kategori_Jenis_1
			'						),

			'		-- 5. Satukan TF dan Pengeluaran di level data mentah sebelum join ke Master Binding (Jauh Lebih Cepat!)
			'		DataTF_Union as (
			'							select Kode_Perusahaan, Tahun, Bulan, Kode_Kategori_Gudang, ID_Kategori_Jenis,
			'								   Id_Sub_Kategori_Jenis_1,
			'								   TotalJumlahTF as Jumlah
			'							from DataTF_Agg
			'							union all
			'							select Kode_Perusahaan, Tahun, Bulan, Kode_Kategori_Gudang, ID_Kategori_Jenis,
			'								   Id_Sub_Kategori_Jenis_1,
			'								   TotalJumlahPengeluaran as Jumlah
			'							from DataPengeluaran_Agg
			'						),

			'		-- 6. Join ke Binding Department untuk PRM
			'		Binding_PRM as (
			'							select z.Kode_Perusahaan,
			'								   z.Kode_Binding,
			'								   y.Tahun, y.Bulan,
			'								   y.ID_Kategori_Jenis,
			'								   y.Id_Sub_Kategori_Jenis_1,
			'								   sum(y.TotalJumlahPR) as JumlahPR
			'							from N_EMI_Binding_Department z
			'								 inner join N_EMI_Binding_Department_Detail x
			'											on z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Binding = x.Kode_Binding
			'								 inner join DataPRM_Agg y
			'											on x.Kode_Perusahaan = y.Kode_Perusahaan and
			'											   x.Kode_Kategori_Gudang = y.Kode_Kategori_Gudang
			'							where z.Status is null
			'							  and z.Kode_Perusahaan = '{KodePerusahaan}'
			'							  and z.Kode_Binding = '{SelectedDepartment_KodeBinding}'
			'							group by z.Kode_Perusahaan, y.Tahun, y.Bulan, z.Kode_Binding, y.ID_Kategori_Jenis,
			'									 y.Id_Sub_Kategori_Jenis_1
			'						),

			'		-- 7. Join ke Binding Department untuk TF (Hanya butuh 1 block query)
			'		Binding_TF as (
			'							select z.Kode_Perusahaan,
			'								   z.Kode_Binding,
			'								   y.Tahun, y.Bulan,
			'								   y.ID_Kategori_Jenis,
			'								   y.Id_Sub_Kategori_Jenis_1,
			'								   sum(y.Jumlah) as JumlahTF
			'							from N_EMI_Binding_Department z
			'								 inner join N_EMI_Binding_Department_Detail x
			'											on z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Binding = x.Kode_Binding
			'								 inner join DataTF_Union y
			'											on x.Kode_Perusahaan = y.Kode_Perusahaan and
			'											   x.Kode_Kategori_Gudang = y.Kode_Kategori_Gudang
			'							where z.Status is null
			'							  and z.Kode_Perusahaan = '{KodePerusahaan}'
			'							  and z.Kode_Binding = '{SelectedDepartment_KodeBinding}'
			'							group by z.Kode_Perusahaan, y.Tahun, y.Bulan, z.Kode_Binding, y.ID_Kategori_Jenis,
			'									 y.Id_Sub_Kategori_Jenis_1
			'						)

			'	-- 8. QUERY UTAMA
			'	select a.Kode_Binding,
			'		   b.ID_Kategori_Jenis,
			'		   c.ID_Sub_Kategori_Jenis_1,
			'		   d.Keterangan as Layer_3,
			'		   b.Bulan,
			'		   b.Tahun,
			'		   c.Jumlah_Budget,
			'		   CASE
			'			   WHEN isnull(e.JumlahPR, 0) > c.Jumlah_Budget
			'				   THEN isnull(e.JumlahPR, 0) - c.Jumlah_Budget
			'			   ELSE 0
			'		   END AS Jumlah_Selisih,
			'		   isnull(e.JumlahPR, 0) as JumlahPR,
			'		   isnull(f.JumlahTF, 0) as JumlahTF,
			'		   c.Urut_Oto
			'	from N_EMI_Transaksi_Budget_Planning a
			'		 inner join N_EMI_Transaksi_Budget_Planning_Detail b
			'					on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Binding = b.Kode_Binding
			'		 inner join N_EMI_Transaksi_Budget_Planning_Det c
			'					on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Binding = c.Kode_Binding
			'						and b.ID_Kategori_Jenis = c.ID_Kategori_Jenis and b.Urut_Oto = c.Urut_Detail
			'		 inner join N_EMI_Master_Sub_Kategori_Jenis_1 d
			'					on c.Kode_Perusahaan = d.Kode_Perusahaan and c.ID_Sub_Kategori_Jenis_1 = d.Id_Sub_Kategori_Jenis_1
			'		 left join Binding_PRM e
			'				   on a.Kode_Perusahaan = e.Kode_Perusahaan
			'					   and a.Kode_Binding = e.Kode_Binding
			'					   and b.ID_Kategori_Jenis = e.ID_Kategori_Jenis
			'					   and c.ID_Sub_Kategori_Jenis_1 = e.Id_Sub_Kategori_Jenis_1
			'					   and b.Tahun = e.Tahun and b.Bulan = e.Bulan
			'		 left join Binding_TF f
			'				   on a.Kode_Perusahaan = f.Kode_Perusahaan
			'					   and a.Kode_Binding = f.Kode_Binding
			'					   and b.ID_Kategori_Jenis = f.ID_Kategori_Jenis
			'					   and c.ID_Sub_Kategori_Jenis_1 = f.Id_Sub_Kategori_Jenis_1
			'					   and b.Tahun = f.Tahun and b.Bulan = f.Bulan
			'	where a.Status is null
			'	  and d.Flag_Is_Budget = 'Y'
			'	  and a.Kode_Perusahaan = '{KodePerusahaan}'
			'	  and a.Kode_Binding = '{SelectedDepartment_KodeBinding}'
			'	  and b.ID_Kategori_Jenis = '{SelectedIDLayer1}'
			'	  and (b.Tahun * 100 + b.Bulan) between {PeriodeAwal} and {PeriodeAkhir}
			'"

			'Using Dr = OpenTrans(SQL)
			'	If Dr.Read Then
			'		Do
			'			Dim ID_Sub_Kategori_1 As Integer = Dr("ID_Sub_Kategori_Jenis_1").ToString.Trim

			'			Dim DataDetailBudget As New DetailDataBudgetPlanning With {
			'				.Keterangan_Layer3 = Dr("Layer_3").ToString.Trim,
			'				.Kode_Binding = Dr("Kode_Binding").ToString.Trim,
			'				.ID_Kategori_Jenis = Dr("ID_Kategori_Jenis"),
			'				.Bulan = Dr("Bulan"),
			'				.Tahun = Dr("Tahun"),
			'				.Jumlah_Budget = Val(HilangkanTanda(Dr("Jumlah_Budget"))),
			'				.Jumlah_Selisih = Val(HilangkanTanda(Dr("Jumlah_Selisih"))),
			'				.Jumlah_PR = Val(HilangkanTanda(Dr("JumlahPR"))),
			'				.Jumlah_Transfer = Val(HilangkanTanda(Dr("JumlahTF")))
			'			}

			'			If Not DataBudgetPlanning.ContainsKey(ID_Sub_Kategori_1) Then
			'				Dim ListDetailBudget As New List(Of DetailDataBudgetPlanning)
			'				ListDetailBudget.Add(DataDetailBudget)
			'				DataBudgetPlanning.Add(ID_Sub_Kategori_1, ListDetailBudget)
			'			Else
			'				DataBudgetPlanning(ID_Sub_Kategori_1).Add(DataDetailBudget)
			'			End If

			'		Loop While Dr.Read
			'	End If
			'End Using

#End Region

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

			'============================
			'=     HAPUS KOLOM LAMA     =
			'============================
			If Dgv_Data.Columns.Count > 2 Then
				For idx As Integer = Dgv_Data.Columns.Count - 1 To 2 Step -1
					Dgv_Data.Columns.RemoveAt(idx)
				Next
			End If

			Dgv_Data.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
			Dgv_Data.ColumnHeadersHeight = 55

			'=======================================
			'=     GENERATE KOLOM LINTAS TAHUN     =
			'=======================================
			Dim currYear As Integer = SelectedTahunAwal
			Dim currMonth As Integer = SelectedBulanAwal
			Dim currPeriode As Integer = (currYear * 100) + currMonth
			Dim cultureID As New System.Globalization.CultureInfo("id-ID")

			While currPeriode <= PeriodeAkhir
				Dim SufiksKolom As String = currPeriode.ToString()
				Dim NamaBulan As String = cultureID.DateTimeFormat.GetMonthName(currMonth) & " " & currYear

				Dim colBdg As New DataGridViewTextBoxColumn() With {.Name = $"QtyBudget_{SufiksKolom}", .HeaderText = "Qty Budget", .Width = 100, .Tag = currPeriode}
				Dim colNmlBdg As New DataGridViewTextBoxColumn() With {.Name = $"NmlBudget_{SufiksKolom}", .HeaderText = "Nominal Budget", .Width = 100, .Tag = currPeriode}
				Dim colPR As New DataGridViewTextBoxColumn() With {.Name = $"QtyPR_{SufiksKolom}", .HeaderText = "Qty PR", .Width = 100, .Tag = currPeriode, .ReadOnly = True}
				Dim colTF As New DataGridViewTextBoxColumn() With {.Name = $"QtyTF_{SufiksKolom}", .HeaderText = "Qty TF", .Width = 100, .Tag = currPeriode, .ReadOnly = True}
				Dim colNmlTF As New DataGridViewTextBoxColumn() With {.Name = $"NmlTF_{SufiksKolom}", .HeaderText = "Nominal TF", .Width = 100, .Tag = currPeriode, .ReadOnly = True}
				Dim colSelisih As New DataGridViewTextBoxColumn() With {.Name = $"QtySelisih_{SufiksKolom}", .HeaderText = "Qty Selisih", .Width = 100, .Tag = currPeriode, .ReadOnly = True}

				'============================
				'=     ATUR SIFAT KOLOM     =
				'============================
				colBdg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
				colNmlBdg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
				colPR.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
				colTF.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
				colNmlTF.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
				colSelisih.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

				colBdg.DefaultCellStyle.BackColor = Color.FromArgb(224, 236, 244)
				colNmlBdg.DefaultCellStyle.BackColor = Color.FromArgb(253, 235, 208)
				colPR.DefaultCellStyle.BackColor = Color.FromArgb(247, 241, 231)
				colTF.DefaultCellStyle.BackColor = Color.FromArgb(238, 233, 245)
				colNmlTF.DefaultCellStyle.BackColor = Color.FromArgb(253, 235, 208)
				colSelisih.DefaultCellStyle.BackColor = Color.FromArgb(233, 240, 228)

				colBdg.DefaultCellStyle.Format = "N0"
				colNmlBdg.DefaultCellStyle.Format = "N2"
				colPR.DefaultCellStyle.Format = "N0"
				colTF.DefaultCellStyle.Format = "N0"
				colNmlTF.DefaultCellStyle.Format = "N2"
				colSelisih.DefaultCellStyle.Format = "N0"

				'==========================================================
				'=     BATASI KOLOM INPUT JIKA LEWAT DARI BULAN TAHUN     =
				'==========================================================

#Region "Kode Lama"

				'If (currYear < CurrentYear) OrElse (currYear = CurrentYear AndAlso currMonth <= CurrentMonth) Then
				'	colBdg.ReadOnly = True
				'End If

#End Region

				colBdg.ReadOnly = True
				colNmlBdg.ReadOnly = True

				Dim tglSekarang As Integer = DateTime.Now.Day
				Dim blnSekarang As Integer = DateTime.Now.Month
				Dim thnSekarang As Integer = DateTime.Now.Year

				If currYear < thnSekarang OrElse (currYear = thnSekarang AndAlso currMonth <= blnSekarang) Then
					colBdg.ReadOnly = True
					colNmlBdg.ReadOnly = True
				Else
					Dim roleAkses = ListRoleAksesInput.FirstOrDefault(Function(x) tglSekarang >= x.TanggalAwal AndAlso
																  tglSekarang <= x.TanggalAkhir AndAlso
																  blnSekarang = x.Bulan)

					If roleAkses IsNot Nothing Then
						Dim isBulanBisaDiinput As Boolean = False

						Select Case currMonth
							Case 1 : isBulanBisaDiinput = roleAkses.M1
							Case 2 : isBulanBisaDiinput = roleAkses.M2
							Case 3 : isBulanBisaDiinput = roleAkses.M3
							Case 4 : isBulanBisaDiinput = roleAkses.M4
							Case 5 : isBulanBisaDiinput = roleAkses.M5
							Case 6 : isBulanBisaDiinput = roleAkses.M6
							Case 7 : isBulanBisaDiinput = roleAkses.M7
							Case 8 : isBulanBisaDiinput = roleAkses.M8
							Case 9 : isBulanBisaDiinput = roleAkses.M9
							Case 10 : isBulanBisaDiinput = roleAkses.M10
							Case 11 : isBulanBisaDiinput = roleAkses.M11
							Case 12 : isBulanBisaDiinput = roleAkses.M12
						End Select

						colBdg.ReadOnly = Not isBulanBisaDiinput
						colNmlBdg.ReadOnly = Not isBulanBisaDiinput
					End If
				End If

				'==========================================================
				'=     SEMBUNYIKAN PR, TF, SELISIH UNTUK BULAN DEPAN      =
				'==========================================================
				Dim periodeSekarang As Integer = (thnSekarang * 100) + blnSekarang

				colBdg.Width = Math.Max(100, 130)
				colNmlBdg.Width = Math.Max(100, 130)

				If currPeriode > periodeSekarang Then
					colPR.Visible = False
					colTF.Visible = False
					colNmlTF.Visible = False
					colSelisih.Visible = False

				End If

				'==================================
				'=     TAMBAH KE DATAGRIDVIEW     =
				'==================================
				Dgv_Data.Columns.AddRange(colBdg, colNmlBdg, colPR, colTF, colNmlTF, colSelisih)

				'===================================
				'=     HANDLE PERGANTIAN TAHUN     =
				'===================================
				currMonth += 1
				If currMonth > 12 Then
					currMonth = 1
					currYear += 1
				End If
				currPeriode = (currYear * 100) + currMonth

			End While

			'=============================
			'=     LOAD DATA LAYER 3     =
			'=============================
			Dgv_Data.Rows.Clear()
			Dim colMap As New Dictionary(Of String, Integer)
			For cIdx As Integer = 0 To Dgv_Data.Columns.Count - 1
				colMap(Dgv_Data.Columns(cIdx).Name) = cIdx
			Next

			For Each kvp As KeyValuePair(Of Integer, List(Of DetailDataLayer3)) In DataLayer3
				Dim ID_Sub_Kategori_1 As Integer = kvp.Key

				Dim listDetail As List(Of DetailDataLayer3) = kvp.Value.Where(Function(x) x.IDKategoriLayer1 = SelectedIDLayer1).ToList()

				If listDetail.Count = 0 Then Continue For

				Dim rowIndex As Integer = Dgv_Data.Rows.Add()
				Dim row As DataGridViewRow = Dgv_Data.Rows(rowIndex)
				row.Cells(0).Value = ID_Sub_Kategori_1
				row.Cells(1).Value = listDetail(0).keteranganKategoriLayer3

				currYear = SelectedTahunAwal
				currMonth = SelectedBulanAwal
				currPeriode = (currYear * 100) + currMonth

				While currPeriode <= PeriodeAkhir
					Dim SufiksKolom As String = currPeriode.ToString()

					' Buat Composite Key untuk pencarian data PR dan TF
					Dim KeyTransaksi = (
						Kode_Binding:=SelectedDepartment_KodeBinding,
						Tahun:=currYear,
						Bulan:=currMonth,
						IDLayer1:=SelectedIDLayer1,
						IDLayer3:=ID_Sub_Kategori_1
					)

					' Buat Composite Key untuk pencarian data Budget
					Dim KeyBudget = (ID_Sub_Kategori_1, currYear, currMonth)

					' Inisialisasi variabel penampung data
					Dim JumlahBudget As Double = 0
					Dim NominalBudget As Double = 0
					Dim JumlahPR As Double = 0
					Dim JumlahTF As Double = 0
					Dim NominalTF As Double = 0
					Dim detailBudget As DetailDataBudgetPlanning = Nothing

					' Ambil Nilai PR dari ListDataPRM (Kompleksitas O(1))
					ListDataPRM.TryGetValue(KeyTransaksi, JumlahPR)

					'Ambil Nilai TF dari ListDataTF (Kompleksitas O(1))
					ListDataTF.TryGetValue(KeyTransaksi, (JumlahTF, NominalTF))

					'Ambil Nilai Budget
					If DataBudgetPlanning.TryGetValue(KeyBudget, detailBudget) Then
						JumlahBudget = detailBudget.Jumlah_Budget
						NominalBudget = detailBudget.Nominal_Budget
					End If

					Dim JumlahSelisih As Double = JumlahBudget - JumlahPR

					Dim colQtyBudget As String = $"QtyBudget_{SufiksKolom}"
					Dim colNmlBudget As String = $"NmlBudget_{SufiksKolom}"
					Dim colQtyPR As String = $"QtyPR_{SufiksKolom}"
					Dim colQtyTF As String = $"QtyTF_{SufiksKolom}"
					Dim colNmlTF As String = $"NmlTF_{SufiksKolom}"
					Dim colQtySelisih As String = $"QtySelisih_{SufiksKolom}"

					' 5. Pemetaan Data ke Sel DataGridView

					Dim colIndex As Integer

					If colMap.TryGetValue(colQtyBudget, colIndex) Then
						row.Cells(colIndex).Value = JumlahBudget
					End If

					If colMap.TryGetValue(colNmlBudget, colIndex) Then
						row.Cells(colIndex).Value = NominalBudget
					End If

					If colMap.TryGetValue(colQtyPR, colIndex) Then
						row.Cells(colIndex).Value = JumlahPR
					End If

					If colMap.TryGetValue(colQtyTF, colIndex) Then
						row.Cells(colIndex).Value = JumlahTF
					End If

					If colMap.TryGetValue(colNmlTF, colIndex) Then
						row.Cells(colIndex).Value = NominalTF
					End If

					If colMap.TryGetValue(colQtySelisih, colIndex) Then
						row.Cells(colIndex).Value = JumlahSelisih
					End If

					currMonth += 1
					If currMonth > 12 Then
						currMonth = 1
						currYear += 1
					End If
					currPeriode = (currYear * 100) + currMonth
				End While
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

	Private Sub CetakLaporanRealtime(ByVal isAllDept As Boolean)
		'=========================================
		'=  1. AMBIL PARAMETER & SIAPKAN VARIABEL=
		'=========================================
		Dim SelectedindexCMB As Integer = Cmb_Department.SelectedIndex
		Dim SelectedDepartment_KodeBinding As String = DataDepartment.Keys.ToList()(SelectedindexCMB)
		Dim SelectedIDLayer1 As Integer = DataLayer1.Keys.ToList()(Cmb_Kategori.SelectedIndex)

		Dim SelectedTahunAwal As Integer = Dtp_PeriodeAwal.Value.Year
		Dim SelectedBulanAwal As Integer = Dtp_PeriodeAwal.Value.Month
		Dim SelectedTahunAkhir As Integer = Dtp_PeriodeAkhir.Value.Year
		Dim SelectedBulanAkhir As Integer = Dtp_PeriodeAkhir.Value.Month

		Dim PeriodeAwal As Integer = (SelectedTahunAwal * 100) + SelectedBulanAwal
		Dim PeriodeAkhir As Integer = (SelectedTahunAkhir * 100) + SelectedBulanAkhir

		Dim LocalDataBudget As New Dictionary(Of Integer, List(Of DetailDataBudgetPlanning))

		Dim ListDataPRM As New Dictionary(Of (Kode_Binding As String, Tahun As Integer, Bulan As Integer, IDLayer1 As Integer, IDLayer3 As Integer), Double)
		Dim ListDataTF As New Dictionary(Of (Kode_Binding As String, Tahun As Integer, Bulan As Integer, IDLayer1 As Integer, IDLayer3 As Integer), (JumlahTF As Double, NominalTF As Double))

		Dim

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'========================================
			'=     GET DATA BESAR DARI DATABASE     =
			'========================================
			'========================
			'=     GET DATA PRM     =
			'========================
			SQL = $"
				;with DataPRM as (
									 select a.Kode_Perusahaan,
											ISNULL(YEAR(a.Tanggal), 0) as Tahun,
											ISNULL(MONTH(a.Tanggal), 0) as Bulan,
											d.ID_Kategori_Jenis,
											d.Id_Sub_Kategori_Jenis_1,
											a.Kode_Kategori_Gudang,
											sum(b.jumlah) as JumlahPR
									 from N_EMI_Purchase_Requisition_Barang_Lain_Departement a
										  inner join N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail b
													 on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
										  inner join barang_lain c
													 on b.Kode_Perusahaan = c.Kode_Perusahaan and
														b.Kode_Stock_Owner = c.Kode_Stock_Owner and
														b.Kode_Barang = c.Kode_Barang
										  inner join View_Kategori_Turunan d
													 on b.Kode_Perusahaan = d.Kode_Perusahaan and
														c.Id_Sub_Kategori_Jenis_3 = d.Id_Sub_Kategori_Jenis_3
									 where a.Status is null
									   and a.Flag_Pra_Release = 'Y'
									   and a.Kode_Perusahaan = '{KodePerusahaan}'
									   and (ISNULL(YEAR(a.Tanggal), 0) * 100 + ISNULL(MONTH(a.Tanggal), 0)) between {PeriodeAwal} and {PeriodeAkhir}
									   and d.ID_Kategori_Jenis = '{SelectedIDLayer1}'
									 group by a.Kode_Perusahaan, a.Tanggal, d.ID_Kategori_Jenis, d.Id_Sub_Kategori_Jenis_1,
											  a.Kode_Kategori_Gudang
								 )
				 select z.Kode_Perusahaan,
						z.Kode_Binding,
						y.Tahun, y.Bulan,
						y.ID_Kategori_Jenis,
						y.Id_Sub_Kategori_Jenis_1,
						sum(y.JumlahPR) as JumlahPR
				 from N_EMI_Binding_Department z
					  inner join N_EMI_Binding_Department_Detail x
								 on z.Kode_Perusahaan = x.Kode_Perusahaan and
									z.Kode_Binding = x.Kode_Binding
					  inner join DataPRM y
								 on x.Kode_Perusahaan = y.Kode_Perusahaan and
									x.Kode_Kategori_Gudang = y.Kode_Kategori_Gudang
				 where z.Status is null
				   and z.Kode_Perusahaan = '{KodePerusahaan}'
				   and z.Kode_Binding = '{SelectedDepartment_KodeBinding}'
				 group by z.Kode_Perusahaan, y.Tahun, y.Bulan, z.Kode_Binding,
						  y.ID_Kategori_Jenis,
						  y.Id_Sub_Kategori_Jenis_1
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim Key = (
							Kode_Binding:=Dr("Kode_Binding"),
							Tahun:=Dr("Tahun"),
							Bulan:=Dr("Bulan"),
							IDLayer1:=Dr("ID_Kategori_Jenis"),
							IDLayer3:=Dr("Id_Sub_Kategori_Jenis_1")
						)

						ListDataPRM.Add(Key, Val(HilangkanTanda(Dr("JumlahPR"))))
					Loop While Dr.Read
				End If
			End Using

			'=========================================
			'=     DATA TRANSFER DAN PENGELUARAN     =
			'=========================================
			SQL = $"
				;with DataPRM as (
									 select a.Kode_Perusahaan,
											d.ID_Kategori_Jenis,
											d.Id_Sub_Kategori_Jenis_1,
											a.Kode_Kategori_Gudang,
											b.No_Urut
									 from N_EMI_Purchase_Requisition_Barang_Lain_Departement a
										  inner join N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail b
													 on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
										  inner join barang_lain c
													 on b.Kode_Perusahaan = c.Kode_Perusahaan and
														b.Kode_Stock_Owner = c.Kode_Stock_Owner and
														b.Kode_Barang = c.Kode_Barang
										  inner join View_Kategori_Turunan d
													 on b.Kode_Perusahaan = d.Kode_Perusahaan and
														c.Id_Sub_Kategori_Jenis_3 = d.Id_Sub_Kategori_Jenis_3
									 where a.Status is null
									   and a.Flag_Pra_Release = 'Y'
									   and a.Kode_Perusahaan = '{KodePerusahaan}'
									   and d.ID_Kategori_Jenis = '{SelectedIDLayer1}'
									 group by a.Kode_Perusahaan, a.Tanggal, d.ID_Kategori_Jenis, d.Id_Sub_Kategori_Jenis_1,
											  a.Kode_Kategori_Gudang, b.No_Urut
								 ),
					  DataTF as (
									 select z.Kode_Perusahaan,
											l.Kode_Kategori_Gudang,
											ISNULL(YEAR(z.Tanggal), 0) as Tahun,
											ISNULL(MONTH(z.Tanggal), 0) as Bulan,
											l.ID_Kategori_Jenis,
											l.Id_Sub_Kategori_Jenis_1,
											sum(k.Jumlah) as TotalJumlahTF,
											sum(dbo.get_hpp(m.Serial_Number)) as TotalNominal
									 from N_EMI_Transfer_Stock_Barang_Lain z
										  inner join N_EMI_Transfer_Stock_Barang_Lain_Detail x
													 on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
										  inner join N_EMI_Transfer_Stock_Barang_Lain_Det y
													 on x.Kode_Perusahaan = y.Kode_Perusahaan and
														x.No_Faktur = y.No_Faktur and
														x.Urut_Oto = y.Urut_TF
										  inner join N_EMI_Transfer_Stock_Barang_Lain_Det2 k
													 on y.Kode_Perusahaan = k.Kode_Perusahaan and
														y.No_Faktur = k.No_Faktur and
														y.Urut_Oto = k.Urut_Det
										  inner join DataPRM l
													 on x.Kode_Perusahaan = l.Kode_Perusahaan and x.Urut_PR_Dept = l.No_Urut
									      inner join Barang_Lain_SN m on k.Kode_Perusahaan = m.Kode_Perusahaan and k.Serial_Number = m.Serial_Number
									 where z.Status is null
									   and y.Selesai = 'Y'
									   and z.Kode_Perusahaan = '{KodePerusahaan}'
									   and (ISNULL(YEAR(z.Tanggal), 0) * 100 + ISNULL(MONTH(z.Tanggal), 0)) between {PeriodeAwal} and {PeriodeAkhir}
									 group by z.Kode_Perusahaan, l.Kode_Kategori_Gudang, z.Tanggal,
											  l.ID_Kategori_Jenis,
											  l.Id_Sub_Kategori_Jenis_1
								 ),
					  DataPengeluaran as (
									 select a.Kode_Perusahaan,
											l.Kode_Kategori_Gudang,
											ISNULL(YEAR(a.Tanggal), 0) as Tahun,
											ISNULL(MONTH(a.Tanggal), 0) as Bulan,
											l.ID_Kategori_Jenis,
											l.Id_Sub_Kategori_Jenis_1,
											sum(c.Jumlah) as TotalJumlahPengeluaran,
											sum(dbo.get_hpp(m.Serial_Number)) as TotalNominal
									 from EMI_Pengeluaran_Stock_parent_barang_lain a
										  inner join EMI_Pengeluaran_Stock_barang_lain b
													 on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
										  inner join EMI_Pengeluaran_Stock_Det_Barang_Lain c
													 on b.Kode_Perusahaan = c.Kode_Perusahaan and
														b.No_Faktur = c.No_Faktur and
														b.Urut_Oto = c.Urut_TF
										  inner join DataPRM l
													 on b.Kode_Perusahaan = l.Kode_Perusahaan and b.Urut_PR_Dept = l.No_Urut
										  inner join Barang_Lain_SN m on c.Kode_Perusahaan = m.Kode_Perusahaan and c.Serial_Number_Awal = m.Serial_Number
									 where a.Status is null
									   and a.Kode_Perusahaan = '{KodePerusahaan}'
									   and c.Selesai = 'Y'
									   and (ISNULL(YEAR(a.Tanggal), 0) * 100 + ISNULL(MONTH(a.Tanggal), 0)) between {PeriodeAwal} and {PeriodeAkhir}
									 group by a.Kode_Perusahaan, a.Tanggal, l.Kode_Kategori_Gudang,
											  l.ID_Kategori_Jenis,
											  l.Id_Sub_Kategori_Jenis_1
								 ),
					  Data_Union as (
									 select Kode_Perusahaan, Tahun, Bulan, Kode_Kategori_Gudang, ID_Kategori_Jenis,
											Id_Sub_Kategori_Jenis_1,
											TotalJumlahTF as Jumlah, TotalNominal as Nominal
									 from DataTF

									 union all

									 select Kode_Perusahaan, Tahun, Bulan, Kode_Kategori_Gudang, ID_Kategori_Jenis,
											Id_Sub_Kategori_Jenis_1,
											TotalJumlahPengeluaran as Jumlah, TotalNominal as Nominal
									 from DataPengeluaran
								 )
				 select z.Kode_Perusahaan,
						z.Kode_Binding,
						y.Tahun, y.Bulan,
						y.ID_Kategori_Jenis,
						y.Id_Sub_Kategori_Jenis_1,
						sum(y.Jumlah) as JumlahTF, sum(y.Nominal) as NominalTF
				 from N_EMI_Binding_Department z
					  inner join N_EMI_Binding_Department_Detail x
								 on z.Kode_Perusahaan = x.Kode_Perusahaan and
									z.Kode_Binding = x.Kode_Binding
					  inner join Data_Union y
								 on x.Kode_Perusahaan = y.Kode_Perusahaan and
									x.Kode_Kategori_Gudang = y.Kode_Kategori_Gudang
				 where z.Status is null
				   and z.Kode_Perusahaan = '{KodePerusahaan}'
				   and z.Kode_Binding = '{SelectedDepartment_KodeBinding}'
				 group by z.Kode_Perusahaan, y.Tahun, y.Bulan, z.Kode_Binding,
						  y.ID_Kategori_Jenis,
						  y.Id_Sub_Kategori_Jenis_1
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim Key = (
							Kode_Binding:=Dr("Kode_Binding"),
							Tahun:=Dr("Tahun"),
							Bulan:=Dr("Bulan"),
							IDLayer1:=Dr("ID_Kategori_Jenis"),
							IDLayer3:=Dr("Id_Sub_Kategori_Jenis_1")
						)

						ListDataTF.Add(Key, (Val(HilangkanTanda(Dr("JumlahTF"))), Val(HilangkanTanda(Dr("NominalTF")))))
					Loop While Dr.Read
				End If
			End Using

			'==============================
			'=     GET DATA BUDGETING     =
			'==============================
			SQL = $"
				select a.Kode_Binding,
					   b.ID_Kategori_Jenis,
					   c.ID_Sub_Kategori_Jenis_1,
					   d.Keterangan as Layer_3,
					   b.Bulan,
					   b.Tahun,
					   c.Jumlah_Budget,
					   c.Nominal_Budget,
					   c.Urut_Oto
				from N_EMI_Transaksi_Budget_Planning a
					 inner join N_EMI_Transaksi_Budget_Planning_Detail b
								on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Binding = b.Kode_Binding
					 inner join N_EMI_Transaksi_Budget_Planning_Det c
								on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Binding = c.Kode_Binding
									and b.ID_Kategori_Jenis = c.ID_Kategori_Jenis and b.Urut_Oto = c.Urut_Detail
					 inner join N_EMI_Master_Sub_Kategori_Jenis_1 d
								on c.Kode_Perusahaan = d.Kode_Perusahaan and
								   c.ID_Sub_Kategori_Jenis_1 = d.Id_Sub_Kategori_Jenis_1
				where a.Status is null
				  and d.Flag_Is_Budget = 'Y'
				  and a.Kode_Perusahaan = '{KodePerusahaan}'
				  and a.Kode_Binding = '{SelectedDepartment_KodeBinding}'
				  and b.ID_Kategori_Jenis = '{SelectedIDLayer1}'
				  and (b.Tahun * 100 + b.Bulan) between {PeriodeAwal} and {PeriodeAkhir}
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim kodeBinding As String = Dr("Kode_Binding").ToString().Trim()
						Dim tahun As Integer = Val(HilangkanTanda(Dr("Tahun")))
						Dim bulan As Integer = Val(HilangkanTanda(Dr("Bulan")))
						Dim idKategori As Integer = Val(HilangkanTanda(Dr("ID_Kategori_Jenis")))
						Dim ID_Sub_Kategori_1 As Integer = Val(HilangkanTanda(Dr("ID_Sub_Kategori_Jenis_1")))
						Dim jumlahBudget As Integer = Val(HilangkanTanda(Dr("Jumlah_Budget")))
						Dim nominalBudget As Integer = Val(HilangkanTanda(Dr("Nominal_Budget")))

						Dim Key = (kodeBinding, tahun, bulan, idKategori, ID_Sub_Kategori_1)
						Dim KeyBudget = (ID_Sub_Kategori_1, tahun, bulan)

						Dim JumlahPR As Double = 0
						Dim JumlahTF As Double = 0
						Dim NominalTF As Double = 0

						ListDataPRM.TryGetValue(Key, JumlahPR)
						ListDataTF.TryGetValue(Key, (JumlahTF, NominalTF))

						Dim selisih As Double = jumlahBudget - JumlahPR

						Dim dataDetailBudget As New DetailDataBudgetPlanning With {
							.Keterangan_Layer3 = Dr("Layer_3").ToString().Trim(),
							.Kode_Binding = kodeBinding,
							.ID_Kategori_Jenis = idKategori,
							.Bulan = bulan,
							.Tahun = tahun,
							.Jumlah_Budget = jumlahBudget,
							.Nominal_Budget = nominalBudget,
							.Jumlah_Selisih = selisih,
							.Jumlah_PR = JumlahPR,
							.Jumlah_Transfer = JumlahTF,
							.Nominal_Transfer = NominalTF
						}

						'JIKA DATA ADA MAKA UPDATE JIKA TIDAK MAKA ADD OTOMATIS
						DataBudgetPlanning(KeyBudget) = dataDetailBudget

					Loop While Dr.Read

				End If
			End Using

			'=====================
			'=     KODE LAMA     =
			'=====================

#Region "KodeLama"

			'SQL = $"
			'         ;with
			'             DataPRM_Base as (
			'                 select a.Kode_Perusahaan, ISNULL(YEAR(a.Tanggal), 0) as Tahun, ISNULL(MONTH(a.Tanggal), 0) as Bulan,
			'                        b.No_Urut, d.ID_Kategori_Jenis, d.Id_Sub_Kategori_Jenis_1, a.Kode_Kategori_Gudang, b.jumlah as JumlahPR
			'                 from N_EMI_Purchase_Requisition_Barang_Lain_Departement a
			'                      inner join N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail b   on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
			'                      inner join barang_lain c   on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang
			'                      inner join View_Kategori_Turunan d   on b.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Sub_Kategori_Jenis_3 = d.Id_Sub_Kategori_Jenis_3
			'                 where a.Status is null and a.Flag_Pra_Release = 'Y' and a.Kode_Perusahaan = '{KodePerusahaan}'
			'             ),
			'             DataPRM_Agg as (
			'                 select Kode_Perusahaan, Tahun, Bulan, Kode_Kategori_Gudang, ID_Kategori_Jenis, Id_Sub_Kategori_Jenis_1, sum(JumlahPR) as TotalJumlahPR
			'                 from DataPRM_Base group by Kode_Perusahaan, Tahun, Bulan, Kode_Kategori_Gudang, ID_Kategori_Jenis, Id_Sub_Kategori_Jenis_1
			'             ),
			'             DataTF_Agg as (
			'                 select z.Kode_Perusahaan, l.Kode_Kategori_Gudang, ISNULL(YEAR(z.Tanggal), 0) as Tahun, ISNULL(MONTH(z.Tanggal), 0) as Bulan,
			'                        l.ID_Kategori_Jenis, l.Id_Sub_Kategori_Jenis_1, sum(k.Jumlah) as TotalJumlahTF
			'                 from N_EMI_Transfer_Stock_Barang_Lain z
			'                      inner join N_EMI_Transfer_Stock_Barang_Lain_Detail x   on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
			'                      inner join N_EMI_Transfer_Stock_Barang_Lain_Det y   on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF
			'                      inner join N_EMI_Transfer_Stock_Barang_Lain_Det2 k   on y.Kode_Perusahaan = k.Kode_Perusahaan and y.No_Faktur = k.No_Faktur and y.Urut_Oto = k.Urut_Det
			'                      inner join DataPRM_Base l on x.Kode_Perusahaan = l.Kode_Perusahaan and x.Urut_PR_Dept = l.No_Urut
			'                 where z.Status is null and y.Selesai = 'Y' and z.Kode_Perusahaan = '{KodePerusahaan}'
			'                 group by z.Kode_Perusahaan, l.Kode_Kategori_Gudang, z.Tanggal, l.ID_Kategori_Jenis, l.Id_Sub_Kategori_Jenis_1
			'             ),
			'             DataPengeluaran_Agg as (
			'                 select a.Kode_Perusahaan, l.Kode_Kategori_Gudang, ISNULL(YEAR(a.Tanggal), 0) as Tahun, ISNULL(MONTH(a.Tanggal), 0) as Bulan,
			'                        l.ID_Kategori_Jenis, l.Id_Sub_Kategori_Jenis_1, sum(c.Jumlah) as TotalJumlahPengeluaran
			'                 from EMI_Pengeluaran_Stock_parent_barang_lain a
			'                      inner join EMI_Pengeluaran_Stock_barang_lain b   on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
			'                      inner join EMI_Pengeluaran_Stock_Det_Barang_Lain c   on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF
			'                      inner join DataPRM_Base l on b.Kode_Perusahaan = l.Kode_Perusahaan and b.Urut_PR_Dept = l.No_Urut
			'                 where a.Status is null and a.Kode_Perusahaan = '{KodePerusahaan}' and c.Selesai = 'Y'
			'                 group by a.Kode_Perusahaan, a.Tanggal, l.Kode_Kategori_Gudang, l.ID_Kategori_Jenis, l.Id_Sub_Kategori_Jenis_1
			'             ),
			'             DataTF_Union as (
			'                 select Kode_Perusahaan, Tahun, Bulan, Kode_Kategori_Gudang, ID_Kategori_Jenis, Id_Sub_Kategori_Jenis_1, TotalJumlahTF as Jumlah from DataTF_Agg
			'                 union all
			'                 select Kode_Perusahaan, Tahun, Bulan, Kode_Kategori_Gudang, ID_Kategori_Jenis, Id_Sub_Kategori_Jenis_1, TotalJumlahPengeluaran as Jumlah from DataPengeluaran_Agg
			'             ),
			'             Binding_PRM as (
			'                 select z.Kode_Perusahaan, z.Kode_Binding, y.Tahun, y.Bulan, y.ID_Kategori_Jenis, y.Id_Sub_Kategori_Jenis_1, sum(y.TotalJumlahPR) as JumlahPR
			'                 from N_EMI_Binding_Department z
			'                      inner join N_EMI_Binding_Department_Detail x   on z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Binding = x.Kode_Binding
			'                      inner join DataPRM_Agg y on x.Kode_Perusahaan = y.Kode_Perusahaan and x.Kode_Kategori_Gudang = y.Kode_Kategori_Gudang
			'                 where z.Status is null and z.Kode_Perusahaan = '{KodePerusahaan}' and z.Kode_Binding = '{SelectedDepartment_KodeBinding}'
			'                 group by z.Kode_Perusahaan, y.Tahun, y.Bulan, z.Kode_Binding, y.ID_Kategori_Jenis, y.Id_Sub_Kategori_Jenis_1
			'             ),
			'             Binding_TF as (
			'                 select z.Kode_Perusahaan, z.Kode_Binding, y.Tahun, y.Bulan, y.ID_Kategori_Jenis, y.Id_Sub_Kategori_Jenis_1, sum(y.Jumlah) as JumlahTF
			'                 from N_EMI_Binding_Department z
			'                      inner join N_EMI_Binding_Department_Detail x   on z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Binding = x.Kode_Binding
			'                      inner join DataTF_Union y on x.Kode_Perusahaan = y.Kode_Perusahaan and x.Kode_Kategori_Gudang = y.Kode_Kategori_Gudang
			'                 where z.Status is null and z.Kode_Perusahaan = '{KodePerusahaan}' and z.Kode_Binding = '{SelectedDepartment_KodeBinding}'
			'                 group by z.Kode_Perusahaan, y.Tahun, y.Bulan, z.Kode_Binding, y.ID_Kategori_Jenis, y.Id_Sub_Kategori_Jenis_1
			'             )
			'         select a.Kode_Binding, b.ID_Kategori_Jenis, c.ID_Sub_Kategori_Jenis_1, d.Keterangan as Layer_3, b.Bulan, b.Tahun, c.Jumlah_Budget,
			'                CASE WHEN isnull(e.JumlahPR, 0) > c.Jumlah_Budget THEN isnull(e.JumlahPR, 0) - c.Jumlah_Budget ELSE 0 END AS Jumlah_Selisih,
			'                isnull(e.JumlahPR, 0) as JumlahPR, isnull(f.JumlahTF, 0) as JumlahTF, c.Urut_Oto
			'         from N_EMI_Transaksi_Budget_Planning a
			'              inner join N_EMI_Transaksi_Budget_Planning_Detail b   on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Binding = b.Kode_Binding
			'              inner join N_EMI_Transaksi_Budget_Planning_Det c   on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Binding = c.Kode_Binding and b.ID_Kategori_Jenis = c.ID_Kategori_Jenis and b.Urut_Oto = c.Urut_Detail
			'              inner join N_EMI_Master_Sub_Kategori_Jenis_1 d   on c.Kode_Perusahaan = d.Kode_Perusahaan and c.ID_Sub_Kategori_Jenis_1 = d.Id_Sub_Kategori_Jenis_1
			'              left join Binding_PRM e on a.Kode_Perusahaan = e.Kode_Perusahaan and a.Kode_Binding = e.Kode_Binding and b.ID_Kategori_Jenis = e.ID_Kategori_Jenis and c.ID_Sub_Kategori_Jenis_1 = e.Id_Sub_Kategori_Jenis_1 and b.Tahun = e.Tahun and b.Bulan = e.Bulan
			'              left join Binding_TF f on a.Kode_Perusahaan = f.Kode_Perusahaan and a.Kode_Binding = f.Kode_Binding and b.ID_Kategori_Jenis = f.ID_Kategori_Jenis and c.ID_Sub_Kategori_Jenis_1 = f.Id_Sub_Kategori_Jenis_1 and b.Tahun = f.Tahun and b.Bulan = f.Bulan
			'         where a.Status is null and d.Flag_Is_Budget = 'Y' and a.Kode_Perusahaan = '{KodePerusahaan}' and a.Kode_Binding = '{SelectedDepartment_KodeBinding}'
			'           and b.ID_Kategori_Jenis = '{SelectedIDLayer1}'
			'           and (b.Tahun * 100 + b.Bulan) between {PeriodeAwal} and {PeriodeAkhir}"

			'Using Dr = OpenTrans(SQL)
			'	If Dr.Read Then
			'		Do
			'			Dim ID_Sub_1 As Integer = Dr("ID_Sub_Kategori_Jenis_1").ToString.Trim
			'			Dim dataDetail As New DetailDataBudgetPlanning With {
			'				.Keterangan_Layer3 = Dr("Layer_3").ToString.Trim,
			'				.Bulan = Dr("Bulan"),
			'				.Tahun = Dr("Tahun"),
			'				.Jumlah_Budget = Val(HilangkanTanda(Dr("Jumlah_Budget"))),
			'				.Jumlah_Selisih = Val(HilangkanTanda(Dr("Jumlah_Selisih"))),
			'				.Jumlah_PR = Val(HilangkanTanda(Dr("JumlahPR"))),
			'				.Jumlah_Transfer = Val(HilangkanTanda(Dr("JumlahTF")))
			'			}
			'			If Not LocalDataBudget.ContainsKey(ID_Sub_1) Then
			'				LocalDataBudget.Add(ID_Sub_1, New List(Of DetailDataBudgetPlanning) From {dataDetail})
			'			Else
			'				LocalDataBudget(ID_Sub_1).Add(dataDetail)
			'			End If
			'		Loop While Dr.Read
			'	End If
			'End Using

#End Region

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show("Gagal mengambil data terbaru: " & ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Exit Sub
		End Try

		Try
			'=============================================
			'=     PENYESUNAN DATATABLE UNTUK EXPORT     =
			'=============================================
			Dim dtExport As New DataTable()
			dtExport.Columns.Add("Department", GetType(String))
			dtExport.Columns.Add("Kategori Layer 3", GetType(String))

			Dim currYear As Integer = SelectedTahunAwal
			Dim currMonth As Integer = SelectedBulanAwal
			Dim currPeriode As Integer = (currYear * 100) + currMonth
			Dim cultureID As New System.Globalization.CultureInfo("id-ID")

			Dim thnSekarang As Integer = DateTime.Now.Year
			Dim blnSekarang As Integer = DateTime.Now.Month
			Dim periodeSekarang As Integer = (thnSekarang * 100) + blnSekarang

			'===============================
			'=     HANDLE LINTAS TAHUN     =
			'===============================
			While currPeriode <= PeriodeAkhir
				Dim SufiksKolom As String = currPeriode.ToString()
				dtExport.Columns.Add($"QtyBudget_{SufiksKolom}", GetType(Double))
				dtExport.Columns.Add($"NmlBudget_{SufiksKolom}", GetType(Double))

				'======================================================
				'=     JIKA BULAN SEBELUMNYA MAKA TAMPILKAN KOLOM     =
				'======================================================
				If currPeriode <= periodeSekarang Then
					dtExport.Columns.Add($"QtyPR_{SufiksKolom}", GetType(Double))
					dtExport.Columns.Add($"QtyTF_{SufiksKolom}", GetType(Double))
					dtExport.Columns.Add($"NmlTF_{SufiksKolom}", GetType(Double))
					dtExport.Columns.Add($"QtySelisih_{SufiksKolom}", GetType(Double))

				End If

				currMonth += 1
				If currMonth > 12 Then
					currMonth = 1
					currYear += 1
				End If
				currPeriode = (currYear * 100) + currMonth
			End While

			'==========================
			'=     PENGISIAN DATA     =
			'==========================
			If DataLayer3 IsNot Nothing Then
				For Each kvp As KeyValuePair(Of Integer, List(Of DetailDataLayer3)) In DataLayer3
					Dim ID_Sub_Kategori_1 As Integer = kvp.Key
					Dim listDetail As List(Of DetailDataLayer3) = kvp.Value.Where(Function(x) x.IDKategoriLayer1 = SelectedIDLayer1).ToList()

					If listDetail.Count = 0 Then Continue For

					Dim row As DataRow = dtExport.NewRow()
					row("Department") = Cmb_Department.Text.Trim
					row("Kategori Layer 3") = listDetail(0).keteranganKategoriLayer3

					currYear = SelectedTahunAwal
					currMonth = SelectedBulanAwal
					currPeriode = (currYear * 100) + currMonth

					While currPeriode <= PeriodeAkhir
						Dim SufiksKolom As String = currPeriode.ToString()

						Dim KeyTransaksi = (
							Kode_Binding:=SelectedDepartment_KodeBinding,
							Tahun:=currYear,
							Bulan:=currMonth,
							IDLayer1:=SelectedIDLayer1,
							IDLayer3:=ID_Sub_Kategori_1
						)
						Dim KeyBudget = (ID_Sub_Kategori_1, currYear, currMonth)

						Dim JumlahBudget As Double = 0
						Dim NominalBudget As Double = 0
						Dim JumlahPR As Double = 0
						Dim JumlahTF As Double = 0
						Dim NominalTF As Double = 0
						Dim detailBudget As DetailDataBudgetPlanning = Nothing

						ListDataPRM.TryGetValue(KeyTransaksi, JumlahPR)
						ListDataTF.TryGetValue(KeyTransaksi, (JumlahTF, NominalTF))

						If DataBudgetPlanning.TryGetValue(KeyBudget, detailBudget) Then
							JumlahBudget = detailBudget.Jumlah_Budget
							NominalBudget = detailBudget.Nominal_Budget
						End If

						Dim JumlahSelisih As Double = JumlahBudget - JumlahPR

						row($"QtyBudget_{SufiksKolom}") = JumlahBudget
						row($"NmlBudget_{SufiksKolom}") = NominalBudget

						' PENTING: Hanya isi data PR, TF, Selisih jika bukan bulan depan
						If currPeriode <= periodeSekarang Then
							row($"QtyPR_{SufiksKolom}") = JumlahPR
							row($"QtyTF_{SufiksKolom}") = JumlahTF
							row($"NmlTF_{SufiksKolom}") = NominalTF
							row($"QtySelisih_{SufiksKolom}") = JumlahSelisih
						End If

						currMonth += 1
						If currMonth > 12 Then
							currMonth = 1
							currYear += 1
						End If
						currPeriode = (currYear * 100) + currMonth
					End While

					dtExport.Rows.Add(row)
				Next
			End If

			If dtExport.Rows.Count = 0 Then
				MessageBox.Show("Data kosong.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
				Exit Sub
			End If

			'=========================================
			'=     KONFIGURASI EXPORT EXCEL          =
			'=========================================
			Dim config As New ExcelExportHelper.ExportConfig() With {
				.FileName = "Laporan_Budget_Planning_" & Now.ToString("dd_MM_yyyy_HH_mm") & ".xlsx",
				.SheetName = "Data Budget",
				.FreezePanes = True,
				.FooterText = $"{Now.ToString("dd MMMM yyyy HH:mm:ss", cultureID)} | {UserID}"
			}

			Dim headerRow1 As New ExcelExportHelper.HeaderRow()
			Dim headerRow2 As New ExcelExportHelper.HeaderRow()

			Dim colorBudget As Color = Color.FromArgb(224, 236, 244)
			Dim colorNominal As Color = Color.FromArgb(253, 235, 208) ' Soft Peach
			Dim colorSelisih As Color = Color.FromArgb(233, 240, 228)
			Dim colorPR As Color = Color.FromArgb(247, 241, 231)
			Dim colorTF As Color = Color.FromArgb(238, 233, 245)
			Dim colorNmlTF As Color = Color.FromArgb(253, 235, 208)

			headerRow1.AddCell(New ExcelExportHelper.HeaderCell("Department", rowSpan:=2, colSpan:=1, backColor:=Color.WhiteSmoke))
			headerRow1.AddCell(New ExcelExportHelper.HeaderCell("Kategori Layer 3", rowSpan:=2, colSpan:=1, backColor:=Color.WhiteSmoke))
			config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat("A", "@", ExcelExportHelper.ExcelAlignment.Left))

			Dim excelColIndex As Integer = 2

			currYear = SelectedTahunAwal
			currMonth = SelectedBulanAwal
			currPeriode = (currYear * 100) + currMonth

			While currPeriode <= PeriodeAkhir
				' PENTING: Kutip tunggal wajib agar bulan (Sep, Nov) tidak diubah jadi tanggal oleh Excel
				Dim namaBulan As String = "'" & New DateTime(currYear, currMonth, 1).ToString("MMMM yyyy", cultureID)

				If currPeriode <= periodeSekarang Then
					headerRow1.AddCell(New ExcelExportHelper.HeaderCell(namaBulan, rowSpan:=1, colSpan:=6, backColor:=Color.Gainsboro))
					headerRow2.AddCell(New ExcelExportHelper.HeaderCell("Qty Budget", backColor:=Color.WhiteSmoke))
					headerRow2.AddCell(New ExcelExportHelper.HeaderCell("Nominal Budget", backColor:=Color.WhiteSmoke))
					headerRow2.AddCell(New ExcelExportHelper.HeaderCell("Qty PR", backColor:=Color.WhiteSmoke))
					headerRow2.AddCell(New ExcelExportHelper.HeaderCell("Qty TF", backColor:=Color.WhiteSmoke))
					headerRow2.AddCell(New ExcelExportHelper.HeaderCell("Nominal TF", backColor:=Color.WhiteSmoke))
					headerRow2.AddCell(New ExcelExportHelper.HeaderCell("Qty Selisih", backColor:=Color.WhiteSmoke))

					excelColIndex += 1
					config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat(GetExcelColumnName(excelColIndex), "N0", ExcelExportHelper.ExcelAlignment.Right, backColor:=colorBudget))
					excelColIndex += 1
					config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat(GetExcelColumnName(excelColIndex), "N2", ExcelExportHelper.ExcelAlignment.Right, backColor:=colorNominal))
					excelColIndex += 1
					config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat(GetExcelColumnName(excelColIndex), "N0", ExcelExportHelper.ExcelAlignment.Right, backColor:=colorPR))
					excelColIndex += 1
					config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat(GetExcelColumnName(excelColIndex), "N0", ExcelExportHelper.ExcelAlignment.Right, backColor:=colorTF))
					excelColIndex += 1
					config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat(GetExcelColumnName(excelColIndex), "N2", ExcelExportHelper.ExcelAlignment.Right, backColor:=colorNmlTF))
					excelColIndex += 1
					config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat(GetExcelColumnName(excelColIndex), "N0", ExcelExportHelper.ExcelAlignment.Right, backColor:=colorSelisih))
				Else
					headerRow1.AddCell(New ExcelExportHelper.HeaderCell(namaBulan, rowSpan:=1, colSpan:=2, backColor:=Color.Gainsboro))
					headerRow2.AddCell(New ExcelExportHelper.HeaderCell("Qty Budget", backColor:=Color.WhiteSmoke))
					headerRow2.AddCell(New ExcelExportHelper.HeaderCell("Nominal Budget", backColor:=Color.WhiteSmoke))

					excelColIndex += 1
					config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat(GetExcelColumnName(excelColIndex), "N0", ExcelExportHelper.ExcelAlignment.Right, backColor:=colorBudget))
					excelColIndex += 1
					config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat(GetExcelColumnName(excelColIndex), "N2", ExcelExportHelper.ExcelAlignment.Right, backColor:=colorNominal))
				End If

				currMonth += 1
				If currMonth > 12 Then
					currMonth = 1
					currYear += 1
				End If
				currPeriode = (currYear * 100) + currMonth
			End While

			config.Headers.Add(headerRow1)
			config.Headers.Add(headerRow2)

			'==============================
			'=     5. EKSEKUSI EXPORT     =
			'==============================
			ExcelExportHelper.ExportFromDataTable(dtExport, config)
		Catch ex As Exception
			MessageBox.Show("Gagal ketika setup excel: " & ex.Message, "Error Excel", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Exit Sub
		End Try

	End Sub

	Private Function GetExcelColumnName(columnNumber As Integer) As String
		Dim dividend As Integer = columnNumber
		Dim columnName As String = String.Empty
		Dim modulo As Integer
		While dividend > 0
			modulo = (dividend - 1) Mod 26
			columnName = Convert.ToChar(65 + modulo).ToString() & columnName
			dividend = (dividend - 1) \ 26
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

	Private Sub ProsesSimpan()
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
		End If

		If Dgv_Data.Rows.Count = 0 Then
			MessageBox.Show("Tidak Ada Data yang Akan Disimpan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
			Btn_Get_Data.Focus()
			Exit Sub
		End If

		If DataUpdate.Count = 0 Then
			MessageBox.Show("Tidak Ada Data yang Akan Disimpan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
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

			Dim PeriodeAwal As Integer = (Dtp_PeriodeAwal.Value.Year * 100) + Dtp_PeriodeAwal.Value.Month
			Dim PeriodeAkhir As Integer = (Dtp_PeriodeAkhir.Value.Year * 100) + Dtp_PeriodeAkhir.Value.Month

			Dim ListLayer3 As List(Of String) = DataUpdate.Select(Function(x) x.ID_Layer3.ToString()).Distinct().ToList()
			Dim ListBulan As List(Of ModelCekBulan) = DataUpdate.Select(Function(x) x.Bulan) _
				.Distinct() _
				.Select(Function(b) New ModelCekBulan With {.Bulan = b, .FlagHasDataInDB = False}) _
				.ToList()

			Dim FilterListLayer3 As String = "'" & String.Join("', '", ListLayer3) & "'"
			Dim FilterListBulan As String = String.Join(", ", ListBulan.Select(Function(x) x.Bulan))

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

			'===========================
			'=  CEK DATA HEADER BULAN  =
			'===========================
			SQL = $"
				select (Tahun * 100 + Bulan) as Periode
				from N_EMI_Transaksi_Budget_Planning_Detail
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Kode_Binding = '{KodeBinding}'
				and ID_Kategori_Jenis = '{IDlayer1}'
				and (Tahun * 100 + Bulan) in ({FilterListBulan})
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim periodeDb As Integer = Val(HilangkanTanda(Dr("Periode")))
						Dim dataBulan = ListBulan.FirstOrDefault(Function(x) x.Bulan = periodeDb)

						If dataBulan IsNot Nothing Then
							dataBulan.FlagHasDataInDB = True
						End If
					Loop While Dr.Read
				End If
			End Using

			For Each item In ListBulan.Where(Function(x) x.FlagHasDataInDB = False)
				' Pecah kembali YYYYMM menjadi Tahun dan Bulan untuk di-insert ke DB
				Dim insertTahun As Integer = item.Bulan \ 100
				Dim insertBulan As Integer = item.Bulan Mod 100

				SQL = $"
					INSERT INTO N_EMI_Transaksi_Budget_Planning_Detail
						(Kode_Perusahaan, Kode_Binding, ID_Kategori_Jenis, Bulan, Tahun, Total_Budget, Total_Nominal)
					VALUES
						('{KodePerusahaan}', '{KodeBinding}', '{IDlayer1}', '{insertBulan}', '{insertTahun}',
						0, 0)
				"
				ExecuteTrans(SQL)
			Next

			Dim dtUrutBulan As New DataTable()
			SQL = $"
				select (Tahun * 100 + Bulan) as Periode, Urut_Oto
				from N_EMI_Transaksi_Budget_Planning_Detail
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Kode_Binding = '{KodeBinding}'
				and ID_Kategori_Jenis = '{IDlayer1}'
				and (Tahun * 100 + Bulan) in ({FilterListBulan})
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
					select a.Kode_Binding, b.ID_Kategori_Jenis, c.ID_Sub_Kategori_Jenis_1,
						   (b.Tahun * 100 + b.Bulan) as Periode,
						   b.Urut_Oto as Urut_Detail, c.Urut_Oto as UrutDet,
						   c.Jumlah_Budget, c.Nominal_Budget
					from N_EMI_Transaksi_Budget_Planning a
						inner join N_EMI_Transaksi_Budget_Planning_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Binding = b.Kode_Binding
						inner join N_EMI_Transaksi_Budget_Planning_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Binding = c.Kode_Binding and b.Urut_Oto = c.Urut_Detail
					where a.Status is null
					and a.Kode_Perusahaan = '{KodePerusahaan}'
					and a.Kode_Binding = '{KodeBinding}'
					and b.ID_Kategori_Jenis = '{IDlayer1}'
					and (b.Tahun * 100 + b.Bulan) in ({FilterListBulan})
					and c.ID_Sub_Kategori_Jenis_1 in ({FilterListLayer3})
				"
				Using Ds = BindingTrans(SQL)
					dtEksis = Ds.Tables("MyTable").Copy()
				End Using

				' Loop data eksis untuk UPDATE
				For Each row As DataRow In dtEksis.Rows
					Dim ID_Sub_Kategori_Jenis_1 As Integer = row("ID_Sub_Kategori_Jenis_1")
					Dim Periode As Integer = row("Periode")
					Dim Jumlah_Budget_Awal As Double = Val(HilangkanTanda(row("Jumlah_Budget")))
					Dim Nominal_Budget_Awal As Double = Val(HilangkanTanda(row("Nominal_Budget")))
					Dim UrutDet As Integer = row("UrutDet")

					Dim dataEksis As DetailDataUpdate = DataUpdate.FirstOrDefault(Function(x) x.ID_Layer3 = ID_Sub_Kategori_Jenis_1 And x.Bulan = Periode And x.FlagUpdate = False)

					If dataEksis IsNot Nothing Then
						Dim logTahun As Integer = dataEksis.Bulan \ 100
						Dim logBulan As Integer = dataEksis.Bulan Mod 100

						'======================
						'=     INSERT LOG     =
						'======================
						SQL = $"
							INSERT INTO N_EMI_Transaksi_Budget_Planning_Log
								(Kode_Perusahaan, Kode_Binding, ID_Kategori_Jenis, ID_Sub_Kategori_Jenis_1, Jenis, Bulan, Tahun, Jumlah_Budget, Jumlah_Update, Nominal_Budget, Nominal_Update,
								 Tanggal, Jam, UserID)
							VALUES
								('{KodePerusahaan}', '{KodeBinding}', '{IDlayer1}', '{ID_Sub_Kategori_Jenis_1}', 'UPDATE',
								'{logBulan}', '{logTahun}', '{Jumlah_Budget_Awal}', '{dataEksis.JumlahUpdate}', '{Nominal_Budget_Awal}', '{dataEksis.NominalUpdate}',
								'{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', '{UserID}')"
						ExecuteTrans(SQL)

						'======================
						'=     UPDATE DET     =
						'======================
						SQL = $"
							update N_EMI_Transaksi_Budget_Planning_Det set Jumlah_Budget = {Val(HilangkanTanda(dataEksis.JumlahUpdate))},
							Nominal_Budget = {Val(HilangkanTanda(dataEksis.NominalUpdate))}
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

				' Loop data BARU untuk INSERT
				For Each item As DetailDataUpdate In DataUpdate.Where(Function(x) x.FlagUpdate = False)

					Dim rowUrut As DataRow() = dtUrutBulan.Select($"Periode = {item.Bulan}")

					If rowUrut.Length > 0 Then
						Dim UrutDetail As Integer = rowUrut(0)("Urut_Oto")

						SQL = $"
							INSERT INTO N_EMI_Transaksi_Budget_Planning_Det
								(Kode_Perusahaan, Kode_Binding, ID_Kategori_Jenis, ID_Sub_Kategori_Jenis_1, Jumlah_Budget, Nominal_Budget,
								 Urut_Detail)
							VALUES
								('{KodePerusahaan}', '{KodeBinding}', '{IDlayer1}', '{item.ID_Layer3}',
								'{Val(HilangkanTanda(item.JumlahUpdate))}', '{Val(HilangkanTanda(item.NominalUpdate))}', {UrutDetail});
						"
						ExecuteTrans(SQL)
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Data Detail untuk Periode {item.Bulan} Tidak Ditemukan di Memory", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
					set a.Total_Budget = b.TotalDet, a.Total_Nominal = b.TotalNominalDet
					from N_EMI_Transaksi_Budget_Planning_Detail a
					inner join (
						select Kode_Binding, ID_Kategori_Jenis, Urut_Detail, SUM(Jumlah_Budget) AS TotalDet, sum(Nominal_Budget) as TotalNominalDet
						from N_EMI_Transaksi_Budget_Planning_Det
						where Kode_Perusahaan = '{KodePerusahaan}'
						group by Kode_Binding, ID_Kategori_Jenis, Urut_Detail
					) b on b.Kode_Binding = a.Kode_Binding
						 and b.ID_Kategori_Jenis = a.ID_Kategori_Jenis
						 and b.Urut_Detail = a.Urut_Oto
					where a.Kode_Perusahaan = '{KodePerusahaan}'
					and a.Kode_Binding = '{KodeBinding}'
					and a.ID_Kategori_Jenis = '{IDlayer1}'
					and (a.Tahun * 100 + a.Bulan) between {PeriodeAwal} and {PeriodeAkhir}
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

		Btn_Get_Data_Click(Btn_Simpan, New EventArgs)
	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		Kosong()
	End Sub

	'=========================================================================================================================================
	'=     HANDLE
	'=========================================================================================================================================

	Private Sub Dgv_Data_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data.CellEndEdit
		If Dgv_Data.Rows.Count = 0 Then Exit Sub

		Dim cell As DataGridViewCell = Dgv_Data.Rows(e.RowIndex).Cells(e.ColumnIndex)

		If cell IsNot Nothing AndAlso cell.ColumnIndex > 1 Then
			Dim ValueCell As Object = cell.Value
			Dim NameCell As String = cell.OwningColumn.Name
			If Not IsNumeric(ValueCell) Then
				SetCellDataAndColor(cell, 0, PersentasePenggelapanWarnaCellInput)
				Exit Sub
			End If

			Dim IDLayer3 As Integer = Val(HilangkanTanda(Dgv_Data.CurrentRow.Cells(0).Value))
			Dim Bulan As Integer = Val(HilangkanTanda(cell.OwningColumn.Tag))

			Dim numericValue As Double = Val(HilangkanTanda(ValueCell))

			SetCellDataAndColor(cell, numericValue, PersentasePenggelapanWarnaCellInput)

			CheckValueChandedCell(IDLayer3, Bulan, ValueCell, NameCell)

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

	Private Sub Cmb_Bulan_Akhir_KeyPress(sender As Object, e As KeyPressEventArgs)
		If e.KeyChar = Chr(13) Then Btn_Get_Data.Focus()
	End Sub

	Private Sub Dgv_Data_MouseMove(sender As Object, e As MouseEventArgs) Handles Dgv_Data.MouseMove
		HandleDataGridViewHover(sender, e)
	End Sub

	Private Sub ValidasiDanEksekusi(aksi As String)
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
		End If

		Dim PeriodeAwal As Integer = (Dtp_PeriodeAwal.Value.Year * 100) + Dtp_PeriodeAwal.Value.Month
		Dim PeriodeAkhir As Integer = (Dtp_PeriodeAkhir.Value.Year * 100) + Dtp_PeriodeAkhir.Value.Month

		If PeriodeAkhir < PeriodeAwal Then
			MessageBox.Show("Periode akhir tidak boleh lebih kecil dari periode awal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
			Dtp_PeriodeAkhir.Focus()
			Exit Sub
		End If

		Dim SelisihBulan As Integer = DateDiff(DateInterval.Month,
									   New Date(Dtp_PeriodeAwal.Value.Year, Dtp_PeriodeAwal.Value.Month, 1),
									   New Date(Dtp_PeriodeAkhir.Value.Year, Dtp_PeriodeAkhir.Value.Month, 1))

		If SelisihBulan > BatasBulanInput Then
			MessageBox.Show("Rentang periode maksimal 10 tahun!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
			Dtp_PeriodeAkhir.Focus()
			Exit Sub
		End If

		Select Case aksi
			Case "GET"
				GetDataBudget()
			Case "CETAK"
				If Dgv_Data.Rows.Count = 0 Then
					MessageBox.Show("Lakukan Get Data Terlebih Dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
					Exit Sub
				End If
				CetakLaporanRealtime(isAllDept:=False)
			Case "CETAK_SELURUH"
				If Dgv_Data.Rows.Count = 0 Then
					MessageBox.Show("Lakukan Get Data Terlebih Dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
					Exit Sub
				End If
				CetakLaporanRealtime(isAllDept:=True)
			Case "SIMPAN"
				If Dgv_Data.Rows.Count = 0 Then
					MessageBox.Show("Tidak Ada Data yang Akan Disimpan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
					Btn_Get_Data.Focus()
					Exit Sub
				End If

				ProsesSimpan()
		End Select
	End Sub

	Private Sub Btn_Get_Data_Click(sender As Object, e As EventArgs) Handles Btn_Get_Data.Click
		ValidasiDanEksekusi("GET")
	End Sub

	Private Sub Btn_Cetak_Laporan_Click(sender As Object, e As EventArgs) Handles Btn_Cetak_Laporan.Click
		ValidasiDanEksekusi("CETAK")
	End Sub

	Private Sub Btn_Cetak_Seluruh_Click(sender As Object, e As EventArgs) Handles Btn_Cetak_Seluruh.Click
		ValidasiDanEksekusi("CETAK_SELURUH")
	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		ValidasiDanEksekusi("SIMPAN")
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
		Public Property Nominal_Budget As Double
		Public Property Jumlah_Selisih As Double
		Public Property Jumlah_PR As Double
		Public Property Jumlah_Transfer As Double
		Public Property Nominal_Transfer As Double

	End Class

	Private Class DetailDataUpdate
		Public Property ID_Layer3 As Integer
		Public Property Tahun As Integer
		Public Property Bulan As Integer
		Public Property JumlahAwal As Double
		Public Property JumlahUpdate As Double
		Public Property NominalAwal As Double
		Public Property NominalUpdate As Double
		Public Property FlagUpdate As Boolean = False

	End Class

	Private Class ModelCekBulan
		Public Property Bulan As Integer
		Public Property FlagHasDataInDB As Boolean = False
	End Class

	Private Class RoleAksesInput
		Public Property TanggalAwal As Integer
		Public Property TanggalAkhir As Integer
		Public Property Bulan As Integer

		Public Property M1 As Boolean
		Public Property M2 As Boolean
		Public Property M3 As Boolean
		Public Property M4 As Boolean
		Public Property M5 As Boolean
		Public Property M6 As Boolean
		Public Property M7 As Boolean
		Public Property M8 As Boolean
		Public Property M9 As Boolean
		Public Property M10 As Boolean
		Public Property M11 As Boolean
		Public Property M12 As Boolean
	End Class

	Private Sub CheckValueChandedCell(ByVal IDLayer3 As Integer, ByVal Periode As Integer, ByVal NilaiUbah As Double, NameCell As String)
		Dim targetTahun As Integer = Periode \ 100
		Dim targetBulan As Integer = Periode Mod 100

		'============================
		'=      GET DATA DEFAULT    =
		'============================
		Dim detailBudget As DetailDataBudgetPlanning = Nothing
		Dim KeyBudget = (IDLayer3, targetTahun, targetBulan)
		DataBudgetPlanning.TryGetValue(KeyBudget, detailBudget)

		Dim JumlahDefault As Double = If(detailBudget IsNot Nothing, detailBudget.Jumlah_Budget, 0)
		Dim NominalDefault As Double = If(detailBudget IsNot Nothing, detailBudget.Nominal_Budget, 0)

		Dim valUbah As Double = Val(HilangkanTanda(NilaiUbah))

		Dim updateItem = DataUpdate.FirstOrDefault(Function(x) x.ID_Layer3 = IDLayer3 AndAlso x.Bulan = Periode)

		'======================
		'=      UPDATE        =
		'======================
		If NameCell.StartsWith("QtyBudget_") Then
			If valUbah <> JumlahDefault Then
				If updateItem Is Nothing Then
					updateItem = New DetailDataUpdate With {
						.ID_Layer3 = IDLayer3, .Bulan = Periode, .Tahun = targetTahun,
						.JumlahAwal = JumlahDefault, .JumlahUpdate = valUbah,
						.NominalAwal = NominalDefault, .NominalUpdate = NominalDefault
					}
					DataUpdate.Add(updateItem)
				Else
					updateItem.JumlahAwal = JumlahDefault
					updateItem.JumlahUpdate = valUbah
				End If
			End If

		ElseIf NameCell.StartsWith("NmlBudget_") Then
			If valUbah <> NominalDefault Then
				If updateItem Is Nothing Then
					updateItem = New DetailDataUpdate With {
						.ID_Layer3 = IDLayer3, .Bulan = Periode, .Tahun = targetTahun,
						.JumlahAwal = JumlahDefault, .JumlahUpdate = JumlahDefault,
						.NominalAwal = NominalDefault, .NominalUpdate = valUbah
					}
					DataUpdate.Add(updateItem)
				Else
					updateItem.NominalAwal = NominalDefault
					updateItem.NominalUpdate = valUbah
				End If
			End If

		End If

		'======================
		'=      CLEANUP       =
		'======================
		' Hapus dari list jika nilai kembali persis sama dengan default DB Artinya tidak ada data yang di update
		If updateItem IsNot Nothing AndAlso updateItem.JumlahUpdate = updateItem.JumlahAwal AndAlso updateItem.NominalUpdate = updateItem.NominalAwal Then
			DataUpdate.Remove(updateItem)
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
		Dim kodePeriode As Integer = 0

		If col.Tag IsNot Nothing AndAlso Integer.TryParse(col.Tag.ToString(), kodePeriode) Then

			' Validasi angka YYYYMM (harus > 100000 agar aman)
			If kodePeriode > 100000 Then
				' Pecah Tahun dan Bulan
				Dim tahunKolom As Integer = kodePeriode \ 100
				Dim bulanKolom As Integer = kodePeriode Mod 100

				' [REVISI]: Hitung index dinamis berdasarkan kesamaan TAG, mengabaikan Mod 4
				Dim currentTag As String = col.Tag.ToString()
				Dim firstColIndex As Integer = colIdx
				While firstColIndex > 2 AndAlso dgv.Columns(firstColIndex - 1).Tag IsNot Nothing AndAlso dgv.Columns(firstColIndex - 1).Tag.ToString() = currentTag
					firstColIndex -= 1
				End While

				Dim lastColIndex As Integer = colIdx
				While lastColIndex < dgv.Columns.Count - 1 AndAlso dgv.Columns(lastColIndex + 1).Tag IsNot Nothing AndAlso dgv.Columns(lastColIndex + 1).Tag.ToString() = currentTag
					lastColIndex += 1
				End While

				' Dapatkan Nama Bulan dan Tahun (Misal: "Januari 2026")
				Dim cultureID As New System.Globalization.CultureInfo("id-ID")
				Dim namaBulanTarget As String = cultureID.DateTimeFormat.GetMonthName(bulanKolom) & " " & tahunKolom

				' Hitung lebar total grup bulan HANYA untuk kolom yang terlihat (Visible = True)
				Dim totalWidth As Integer = 0
				Dim firstVisibleLeft As Integer = -1

				For idx As Integer = firstColIndex To lastColIndex
					If idx < dgv.Columns.Count AndAlso dgv.Columns(idx).Visible Then
						Dim rectCol As Rectangle = dgv.GetColumnDisplayRectangle(idx, True)
						If rectCol.Width > 0 Then
							If firstVisibleLeft = -1 Then firstVisibleLeft = rectCol.X
							totalWidth += rectCol.Width
						End If
					End If
				Next

				' Jika seluruh kolom bulan ini tersembunyi di luar scroll, tidak perlu digambar
				If firstVisibleLeft = -1 Then Exit Sub

				Dim midHeight As Integer = e.CellBounds.Height \ 2

				Dim rectHeaderAtasGrup As New Rectangle(firstVisibleLeft, e.CellBounds.Y, totalWidth, midHeight)
				Dim rectHeaderAtasCell As New Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, midHeight)
				Dim rectHeaderBawah As New Rectangle(e.CellBounds.X, e.CellBounds.Y + midHeight, e.CellBounds.Width, midHeight)

				' A. Gambar Latar Belakang Sub-Header Bawah (Putih Abu-abu)
				Using brushBgBawah As New SolidBrush(Color.WhiteSmoke)
					e.Graphics.FillRectangle(brushBgBawah, rectHeaderBawah)
				End Using

				' Tulis teks sub-header bawaan kolom (Qty Budget, dll)
				If e.Value IsNot Nothing Then
					Dim sfSub As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
					Using brushTextSub As New SolidBrush(dgv.ColumnHeadersDefaultCellStyle.ForeColor)
						e.Graphics.DrawString(e.Value.ToString(), dgv.ColumnHeadersDefaultCellStyle.Font, brushTextSub, rectHeaderBawah, sfSub)
					End Using
				End If

				' B. Gambar Latar Belakang Header Atas (Anti-Ghosting)
				Using brushBgAtas As New SolidBrush(Color.Gainsboro)
					e.Graphics.FillRectangle(brushBgAtas, rectHeaderAtasCell)
				End Using

				' C. Cetak Nama Bulan dan Tahun
				Dim sfAtas As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
				Using brushTextAtas As New SolidBrush(dgv.ColumnHeadersDefaultCellStyle.ForeColor)
					e.Graphics.DrawString(namaBulanTarget, dgv.ColumnHeadersDefaultCellStyle.Font, brushTextAtas, rectHeaderAtasGrup, sfAtas)
				End Using

				' D. Gambar Ulang Garis Kisi (Gridlines) Pembatas
				e.Graphics.DrawLine(Pens.DarkGray, e.CellBounds.Left, rectHeaderAtasCell.Bottom, e.CellBounds.Right, rectHeaderAtasCell.Bottom)

				e.Graphics.DrawLine(Pens.DarkGray, e.CellBounds.Left, rectHeaderBawah.Y, e.CellBounds.Left, rectHeaderBawah.Bottom)
				e.Graphics.DrawLine(Pens.DarkGray, e.CellBounds.Right - 1, rectHeaderBawah.Y, e.CellBounds.Right - 1, rectHeaderBawah.Bottom)
				e.Graphics.DrawLine(Pens.DarkGray, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1)

				' Kunci Garis Tebal Samping pembatas antar Bulan (Mencari visible column terakhir/pertama dalam grup)
				Dim isLastVisibleCol As Boolean = True
				For checkIdx = colIdx + 1 To lastColIndex
					If dgv.Columns(checkIdx).Visible Then isLastVisibleCol = False : Exit For
				Next
				If isLastVisibleCol Then
					e.Graphics.DrawLine(Pens.DarkGray, e.CellBounds.Right - 1, e.CellBounds.Y, e.CellBounds.Right - 1, e.CellBounds.Bottom)
				End If

				Dim isFirstVisibleCol As Boolean = True
				For checkIdx = firstColIndex To colIdx - 1
					If dgv.Columns(checkIdx).Visible Then isFirstVisibleCol = False : Exit For
				Next
				If isFirstVisibleCol Then
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

	Private Sub Dtp_PeriodeAwal_Enter(sender As Object, e As EventArgs) Handles Dtp_PeriodeAwal.Enter, Dtp_PeriodeAkhir.Enter
		Dim dtp As DateTimePicker = DirectCast(sender, DateTimePicker)
		'=================================
		'=     SET TANGGAL MENJADI 1     =
		'=================================
		dtp.Value = New DateTime(dtp.Value.Year, dtp.Value.Month, 1)
	End Sub

	'===========================================================================================================================================

End Class