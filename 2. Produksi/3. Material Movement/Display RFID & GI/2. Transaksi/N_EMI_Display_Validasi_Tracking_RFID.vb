Public Class N_EMI_Display_Validasi_Tracking_RFID
	Private contextMenu As New ContextMenuStrip()
	Public isDialogOpen As Boolean = False
	Private WithEvents refreshTimer As New Timer()

	Private WithEvents clickTimer As New Timer With {.Interval = 500}
	Private clickCount As Integer = 0
	Private lastHit As DataGridView.HitTestInfo

	Dim isInValidGI As Boolean = False

	Private Sub N_EMI_Display_Validasi_Tracking_RFID_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Fetch_Tracking_RFID()

		Dim menuValidasi As New ToolStripMenuItem("Validasi")
		'AddHandler menuValidasi.Click, AddressOf Validasi_Click

		contextMenu.Items.Add(menuValidasi)
		DgvData.ContextMenuStrip = contextMenu

		refreshTimer.Interval = 5000
		refreshTimer.Start()

	End Sub

	Private Sub refreshTimer_Tick(sender As Object, e As EventArgs) Handles refreshTimer.Tick
		If Not isDialogOpen Then
			Fetch_Tracking_RFID()
		End If
	End Sub

	Private Sub DgvData_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DgvData.CellMouseDown

		'If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 Then
		'	Dim row = DgvData.Rows(e.RowIndex)

		'	DgvData.ClearSelection()
		'	row.Selected = True

		'	If row.DefaultCellStyle.BackColor <> Color.LightGreen Then
		'		contextMenu.Show(Cursor.Position)
		'	End If
		'End If

	End Sub

	Private Sub OpenValidasiDialog(ByVal IsBypass As Boolean)
		isDialogOpen = True

		Dim SelectedSplit As String = DgvData.CurrentRow.Cells(0).Value
		Dim SelectedBatch As String = DgvData.CurrentRow.Cells(1).Value

		Dim TotMixeInPouch As Integer = Val(HilangkanTanda(DgvData.CurrentRow.Cells(5).Value))
		Dim TotMixeInCan As Integer = Val(HilangkanTanda(DgvData.CurrentRow.Cells(6).Value))

		Try
			OpenConn()

			'============================================
			'=     CEK APAKAH DATA SIAP UNTUK DI GI     =
			'============================================
			' CEK TRANSFER STOCK SEMENTARA
			'SQL = $"
			'	select 1
			'	from N_EMI_Transaksi_Transfer_Stock_Sementara a
			'		inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
			'		inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF
			'		inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det
			'	where a.Status is null
			'	and a.Flag_Validasi_RFID = 'Y'
			'	and a.Kode_Perusahaan = '{KodePerusahaan}'
			'	and a.No_Split = '{SelectedSplit}'
			'	and a.Batch = '{SelectedBatch}'
			'"
			'Using Dr = OpenTrans(SQL)
			'	If Not Dr.Read Then
			'		Dr.Close()
			'		CloseConn()
			'		MessageBox.Show($"Terjadi Kesalahan Split {SelectedSplit} dan Batch {SelectedBatch} Belum Melakukan Transfer Stock Sementara", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'		Exit Sub
			'	End If
			'End Using

			' CEK APAKAH BAHANBAKU SUDAH TERPENUHI SEMUA
			'SQL = $"
			'	;with DataTF as (
			'		select z.Kode_Perusahaan, x.Kode_Barang, sum(k.Jumlah) as Jumlah
			'		from Tf_Stock_Parent z
			'			inner join Tf_Stock x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
			'			inner join Tf_Stock_det y on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF
			'			inner join TF_Stock_Det2 k on y.Kode_Perusahaan = k.Kode_Perusahaan and y.No_Faktur = k.No_Faktur and y.Urut_Oto = k.Urut_Det
			'		where z.Status is NULL
			'		and x.Urut_Material_Requisition_Convert in (
			'			select r.Urut_Oto
			'			from Emi_Material_Requisition q
			'				inner join Emi_Material_Requisition_Det w on q.Kode_Perusahaan = w.Kode_Perusahaan and q.No_Faktur = w.No_Faktur
			'				inner join Emi_Material_Requisition_Det_Convert r on w.Kode_Perusahaan = r.Kode_Perusahaan and w.No_Faktur = r.No_Faktur and w.Urut_Oto = r.No_Urut_Det
			'			where q.Status is NULL
			'			and q.Kode_Perusahaan = z.Kode_Perusahaan
			'			and q.No_Faktur_Order = '{SelectedSplit}'
			'			and q.Batch = '{SelectedBatch}'
			'		)
			'		group by z.Kode_Perusahaan, x.Kode_Barang

			'		union all

			'		select r.Kode_Perusahaan, r.Kode_Barang, sum(r.Jumlah) as Jumlah
			'		from N_EMI_Transaksi_Material_Requisition_QC z
			'			inner join N_EMI_Transaksi_Material_Requisition_QC_Detail x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
			'			inner join N_EMI_Transaksi_Material_Requisition_QC_Det y on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_Detail
			'			inner join N_EMI_Transaksi_Material_Requisition_QC_Validasi r on y.Kode_Perusahaan = r.Kode_Perusahaan and y.No_Faktur = r.No_Faktur_RM and y.Urut_Oto = r.Urut_Det_RM
			'		where z.Status is NULL and r.Status is NULL
			'		and z.No_Faktur_Order = '{SelectedSplit}'
			'		and x.Batch = '{SelectedBatch}'
			'		group by r.Kode_Perusahaan, r.Kode_Barang
			'	), Data_Transfer_Stock as (
			'		select Kode_Perusahaan, Kode_Barang, sum(Jumlah) as Jumlah
			'		from DataTF
			'		group by Kode_Perusahaan, Kode_Barang
			'	)
			'	select a.No_Transaksi as No_Split, b.Kode_Stock_Owner as Lokasi_Bahan, b.Kode_Barang as Kode_Bahan, c.Nama as Nama_Bahan,
			'		b.Jumlah as Jumlah_Formula, b.Satuan as Satuan_Bahan, c.flag_potong_stok, isnull(c.standar_price,0) as standar_price, c.Flag_Non_Barcode,

			'		isnull((
			'			select ((x.Jumlah) /
			'				(select dbo.Ubah_Satuan(z.Kode_Perusahaan, 'masa', x.Kode_Barang, r.Satuan_Hasil, x.satuan, r.Hasil) from Emi_Transaksi_Formulator r
			'				where r.Kode_Perusahaan = x.Kode_Perusahaan And r.No_Faktur = x.No_Faktur)
			'			) * a.Qty_Batch
			'			from EMI_Order_Produksi z, EMI_Transaksi_Formulator_Detail_Bahan x
			'			where z.Kode_Perusahaan = x.Kode_Perusahaan
			'				and z.Kode_Formula = x.No_Faktur
			'				and z.Status is null
			'				and a.Kode_Perusahaan = z.Kode_Perusahaan
			'				and a.No_PO = z.No_Faktur
			'				and x.Kode_Barang = b.Kode_Barang
			'		), 0) as JumlahKebutuhan,

			'		d.Jumlah as Jumlah_Transfer
			'	from Emi_Split_Production_Order a
			'		inner join Emi_Split_Production_Order_Detail_Bahan b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur
			'		inner join barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang
			'		left join Data_Transfer_Stock d on b.Kode_Perusahaan = d.kode_perusahaan and b.Kode_Barang = d.Kode_Barang
			'	where a.Status is NULL
			'	and a.Kode_Perusahaan = '{KodePerusahaan}'
			'	and a.No_Transaksi = '{SelectedSplit}'
			'"
			'Using Dr = OpenTrans(SQL)
			'	Do While Dr.Read

			'		Dim Kdbarang As String = Dr("Kode_Bahan")

			'		Dim JmlhKebutuhan As Double = Val(HilangkanTanda(If(General_Class.CekNULL(Dr("JumlahKebutuhan")) = "", 0, Dr("JumlahKebutuhan"))))
			'		Dim JumlahTransfer As Double = Val(HilangkanTanda(If(General_Class.CekNULL(Dr("Jumlah_Transfer")) = "", 0, Dr("Jumlah_Transfer"))))

			'		If General_Class.CekNULL(Dr("Flag_Non_Barcode")) = "" AndAlso JumlahTransfer = 0 Then
			'			Dr.Close()
			'			CloseConn()
			'			MessageBox.Show($"Terjadi Kesalahan, Kode Bahan {Kdbarang} Belum Belum Terpenuhi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'			isInValidGI = True
			'			isDialogOpen = False
			'			Exit Sub
			'		End If

			'		If JumlahTransfer < JmlhKebutuhan Then
			'			Dr.Close()
			'			CloseConn()
			'			MessageBox.Show($"Terjadi Kesalahan, Kode Bahan {Kdbarang} Belum Belum Terpenuhi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'			isInValidGI = True
			'			isDialogOpen = False
			'			Exit Sub
			'		End If

			'	Loop
			'End Using

			'===============================
			'=     CEK STOCK BARANG SN     =
			'===============================
			SQL = $"
				;with cte as (
				select z.Kode_Perusahaan, x.Kode_Barang, sum(k.Jumlah) as Jumlah_TF, isnull((
					select sum(a.Jumlah) from Barang_SN a
					where a.Kode_Perusahaan = z.Kode_Perusahaan
					and z.SO_Tujuan = a.Kode_Stock_Owner and x.Kode_Barang = a.Kode_Barang and k.Serial_Number = a.Serial_Number
					), 0) as Jumlah_SN
				from Tf_Stock_Parent z
					 inner join Tf_Stock x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
					 inner join Tf_Stock_det y
								on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF
					 inner join TF_Stock_Det2 k
								on y.Kode_Perusahaan = k.Kode_Perusahaan and y.No_Faktur = k.No_Faktur and y.Urut_Oto = k.Urut_Det
				where z.Status is NULL
					and x.Urut_Material_Requisition_Convert in (
						select r.Urut_Oto
							from Emi_Material_Requisition q
							inner join Emi_Material_Requisition_Det w on q.Kode_Perusahaan = w.Kode_Perusahaan and q.No_Faktur = w.No_Faktur
							inner join Emi_Material_Requisition_Det_Convert r on w.Kode_Perusahaan = r.Kode_Perusahaan and w.No_Faktur = r.No_Faktur and w.Urut_Oto = r.No_Urut_Det
							where q.Status is NULL
								and q.Kode_Perusahaan = z.Kode_Perusahaan
								and q.No_Faktur_Order = '{SelectedSplit}'
								and q.Batch = '{SelectedSplit}'
					)
				group by z.Kode_Perusahaan, z.SO_Tujuan, x.Kode_Barang, k.Serial_Number
				) select Kode_Perusahaan, Kode_Barang, sum(Jumlah_TF) as Jumlah_TF, sum(Jumlah_SN) as Jumlah_SN
				from cte
				where Kode_Perusahaan = '{KodePerusahaan}'
				group by Kode_Perusahaan, Kode_Barang
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim KodeBarang As String = Dr("Kode_Barang")
					Dim JumlahTF As Double = Val(HilangkanTanda(If(General_Class.CekNULL(Dr("Jumlah_TF")) = "", 0, Dr("Jumlah_TF"))))
					Dim JumlahSN As Double = Val(HilangkanTanda(If(General_Class.CekNULL(Dr("Jumlah_SN")) = "", 0, Dr("Jumlah_SN"))))

					If JumlahTF <> JumlahSN Then
						Dr.Close()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Stock Kode Bahan {KodeBarang} Sudah Digunakan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						isInValidGI = True
						isDialogOpen = False
						Exit Sub
					End If
				Loop
			End Using

			SQL = $"
				;with cte as (select r.Kode_Perusahaan, r.Kode_Barang, sum(r.Jumlah) as JumlahTF,
									 isnull((select sum(a.Jumlah)
											 from Barang_SN a
											 where a.Kode_Perusahaan = r.Kode_Perusahaan
											   and r.Kode_Stock_Owner_Tujuan = a.Kode_Stock_Owner
											   and r.Kode_Barang = a.Kode_Barang
											   and r.SN_Baru = a.Serial_Number), 0) as Jumlah_SN
							  from N_EMI_Transaksi_Material_Requisition_QC z
								   inner join N_EMI_Transaksi_Material_Requisition_QC_Detail x
											  on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
								   inner join N_EMI_Transaksi_Material_Requisition_QC_Det y
											  on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and
												 x.Urut_Oto = y.Urut_Detail
								   inner join N_EMI_Transaksi_Material_Requisition_QC_Validasi r
											  on y.Kode_Perusahaan = r.Kode_Perusahaan and y.No_Faktur = r.No_Faktur_RM and
												 y.Urut_Oto = r.Urut_Det_RM
							  where z.Status is NULL
								and r.Status is NULL
								and z.No_Faktur_Order = '{SelectedSplit}'
								and x.Batch = '{SelectedBatch}'
							  group by r.Kode_Perusahaan, r.Kode_Barang, r.Kode_Stock_Owner_Tujuan, r.SN_Baru
				)select Kode_Perusahaan, Kode_Barang, sum(JumlahTF) as Jumlah_TF, sum(Jumlah_SN) as Jumlah_SN
				from cte
				where Kode_Perusahaan = '{KodePerusahaan}'
				group by Kode_Perusahaan, Kode_Barang
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim KodeBarang As String = Dr("Kode_Barang")
					Dim JumlahTF As Double = Val(HilangkanTanda(If(General_Class.CekNULL(Dr("Jumlah_TF")) = "", 0, Dr("Jumlah_TF"))))
					Dim JumlahSN As Double = Val(HilangkanTanda(If(General_Class.CekNULL(Dr("Jumlah_SN")) = "", 0, Dr("Jumlah_SN"))))

					If JumlahTF <> JumlahSN Then
						Dr.Close()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Stock Kode Bahan {KodeBarang} Sudah Digunakan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						isInValidGI = True
						isDialogOpen = False
						Exit Sub
					End If
				Loop
			End Using

			If Not IsBypass Then

				' CEK APAKAH DATA SUDAH DI MIXER IN
				Dim IsPouch As Boolean = False
				SQL = $"
					;with DataRFID as (
						SELECT
							Kode_Perusahaan,
							No_Split_Production_Order,
							batch,
							Lokasi_Pairing,
							Lokasi_IN,
							RFID_Tag
						FROM N_EMI_Pairing_RFID
					) select a.No_Split_Production_Order, a.batch, Count(1) as Tot_Data,
						count(case when a.Lokasi_Pairing = 'COLD_STORAGE' then 1 end) as Tot_Pairing_Box_Cold_Storage,
						count(case when a.Lokasi_Pairing = 'PREMIX' then 1 end) as Tot_Pairing_Box_Premix,
						count(case when a.Lokasi_Pairing = 'COLD_STORAGE' and (a.Lokasi_IN = 'GRINDER_IN') then 1 end) as Tot_In_Box_Cold_Storage,
						count(case when a.Lokasi_Pairing = 'PREMIX' and (a.Lokasi_IN like '%MIXER_POUCH_IN' or a.Lokasi_IN like '%MIXER_CAN_IN') then 1 end) as Tot_In_Box_Premix
					from DataRFID a
					where a.kode_perusahaan = '{KodePerusahaan}'
					and a.No_Split_Production_Order = '{SelectedSplit}'
					and a.batch = '{SelectedBatch}'
					group by a.No_Split_Production_Order, a.batch
				"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then

						Dim Data_Pairing_Cold_Storage As Double = If(General_Class.CekNULL(Dr("Tot_Pairing_Box_Cold_Storage")) = "", 0, Val(HilangkanTanda(Dr("Tot_Pairing_Box_Cold_Storage"))))
						Dim Data_Pairing_Premix As Double = If(General_Class.CekNULL(Dr("Tot_Pairing_Box_Premix")) = "", 0, Val(HilangkanTanda(Dr("Tot_Pairing_Box_Premix"))))
						Dim Data_In_Cold_Storage As Double = If(General_Class.CekNULL(Dr("Tot_In_Box_Cold_Storage")) = "", 0, Val(HilangkanTanda(Dr("Tot_In_Box_Cold_Storage"))))
						Dim Data_In_Premix As Double = If(General_Class.CekNULL(Dr("Tot_In_Box_Premix")) = "", 0, Val(HilangkanTanda(Dr("Tot_In_Box_Premix"))))

						' 1. Cek Pairing
						If Data_Pairing_Cold_Storage = 0 AndAlso Data_Pairing_Premix = 0 Then
							Dr.Close()
							CloseConn()
							MessageBox.Show($"Terjadi Kesalahan, Tidak Ada Data Pairing Untuk Split {SelectedSplit} dan Batch {SelectedBatch}", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							isInValidGI = True
							isDialogOpen = False
							Exit Sub
							'ElseIf Data_Pairing_Cold_Storage <> 0 AndAlso Data_In_Cold_Storage = 0 Then
							'	Dr.Close()
							'	CloseConn()
							'	MessageBox.Show($"Terjadi Kesalahan, Tidak Ada Data Box Cold Storage yang Masuk DiGrinderIn", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'	Exit Sub
							'ElseIf Data_Pairing_Premix <> 0 AndAlso Data_In_Premix = 0 Then
							'	Dr.Close()
							'	CloseConn()
							'	MessageBox.Show($"Terjadi Kesalahan, Tidak Ada Data Box Premix yang Masuk DiGrinderIn", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'	Exit Sub
							'ElseIf Data_In_Cold_Storage = 0 And Data_In_Premix = 0 Then
							'	Dr.Close()
							'	CloseConn()
							'	MessageBox.Show($"Terjadi Kesalahan, Terdapat Box yang Belum Mencapai Step Mixer in", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'	Exit Sub
						End If

					End If
				End Using

				' CEK APAKAH BOX RFID SUDAH LENGKAP
				SQL = $"
					select e.Kode_Stock_Owner, e.Flag_QC, e.Flag_Cold_Storage
					from Emi_Split_Production_Order a
						inner join Emi_Split_Production_Order_Detail_Bahan b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Faktur
						inner join Barang c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang
						inner join EMI_Kategori_Gudang_PerLokasi d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Kategori_Gudang = d.ID_Kategori_Gudang
						inner join Stock_Owner_Gudang e on d.Kode_Perusahaan = e.Kode_Perusahaan and d.lokasi_gudang = e.Kode_Stock_Owner
					where a.Status is NULL
					and a.Kode_Perusahaan = '{KodePerusahaan}'
					and a.No_Transaksi = '{SelectedSplit}'
					and (e.Flag_Cold_Storage = 'Y' or e.Flag_QC = 'Y')
					group by e.Kode_Stock_Owner, e.Flag_QC, e.Flag_Cold_Storage
				"
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows().Count <> 0 Then
							For i As Integer = 0 To .Rows.Count - 1

								Dim hasGudangColdStorage As Boolean = If(General_Class.CekNULL(.Rows(i).Item("Flag_Cold_Storage")) = "", False, .Rows(i).Item("Flag_Cold_Storage") = "Y")
								Dim hasGudangPremix As Boolean = If(General_Class.CekNULL(.Rows(i).Item("Flag_QC")) = "", False, .Rows(i).Item("Flag_QC") = "Y")

								If hasGudangColdStorage Then
									SQL = $"
										select 1
										from N_EMI_Pairing_RFID
										where Kode_Perusahaan = '{KodePerusahaan}'
										and No_Split_Production_Order = '{SelectedSplit}'
										and batch = '{SelectedBatch}'
										and Lokasi_Pairing = 'COLD_STORAGE'
									"
									Using Dr = OpenTrans(SQL)
										If Not Dr.Read Then
											Dr.Close()
											CloseConn()
											MessageBox.Show($"GI tidak dapat dilakukan karena Box pada Gudang Cold Storage Belum Ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											isInValidGI = True
											isDialogOpen = False
											Exit Sub
										End If
									End Using

									SQL = $"
										select 1
										from N_EMI_Pairing_RFID
										where Kode_Perusahaan = '{KodePerusahaan}'
										and No_Split_Production_Order = '{SelectedSplit}'
										and batch = '{SelectedBatch}'
										and (Lokasi_IN = 'GRINDER_IN')
										and Lokasi_Pairing = 'COLD_STORAGE'
									"
									Using Dr = OpenTrans(SQL)
										If Not Dr.Read Then
											Dr.Close()
											CloseConn()
											MessageBox.Show($"GI tidak dapat dilakukan karena Box pada Gudang Cold Storage Belum Mencapai Step Mixer In", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											isInValidGI = True
											isDialogOpen = False
											Exit Sub
										End If
									End Using

									'SQL = $"
									'	select top 1 Lokasi_IN
									'	from N_EMI_Pairing_RFID
									'	where Kode_Perusahaan = '{KodePerusahaan}'
									'	and No_Split_Production_Order = '{SelectedSplit}'
									'	and batch = '{SelectedBatch}'
									'	and (lokasi_in like '%MIXER_POUCH_IN' or lokasi_in like '%MIXER_CAN_IN')
									'	order by Urut_Pairing desc
									'"
									'Using Dr = OpenTrans(SQL)
									'	If Not Dr.Read Then
									'		Dr.Close()
									'		CloseConn()
									'		MessageBox.Show($"GI tidak dapat dilakukan karena Box pada Gudang Cold Storage Mencapai Step Mixer In", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									'		isInValidGI = True
									'		Exit Sub
									'	End If
									'End Using

								End If

								If hasGudangPremix Then
									SQL = $"
									select 1
									from N_EMI_Pairing_RFID
									where Kode_Perusahaan = '{KodePerusahaan}'
									and No_Split_Production_Order = '{SelectedSplit}'
									and batch = '{SelectedBatch}'
									and Lokasi_Pairing = 'PREMIX'
								"
									Using Dr = OpenTrans(SQL)
										If Not Dr.Read Then
											Dr.Close()
											CloseConn()
											MessageBox.Show($"GI tidak dapat dilakukan karena Box pada Gudang Premix Belum Ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											isInValidGI = True
											isDialogOpen = False
											Exit Sub
										End If
									End Using

									SQL = $"
										select 1
										from N_EMI_Pairing_RFID
										where Kode_Perusahaan = '{KodePerusahaan}'
										and No_Split_Production_Order = '{SelectedSplit}'
										and batch = '{SelectedBatch}'
										and (Lokasi_IN like '%MIXER_POUCH_IN' or Lokasi_IN like '%MIXER_CAN_IN')
										and Lokasi_Pairing = 'PREMIX'
									"
									Using Dr = OpenTrans(SQL)
										If Not Dr.Read Then
											Dr.Close()
											CloseConn()
											MessageBox.Show($"GI tidak dapat dilakukan karena Box pada Gudang Premix Belum Mencapai Step Mixer In", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
											isInValidGI = True
											isDialogOpen = False
											Exit Sub
										End If
									End Using

								End If

							Next
						End If
					End With
				End Using

			End If

			'=======================================
			'=     CEK APAKAH DATA SUDAH DI GI     =
			'=======================================
			SQL = $"
				select 1
				from Emi_Production_Results z
					inner join Emi_Production_Results_HPP x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Transaksi = x.No_Transaksi
				where z.Kode_Perusahaan = '{KodePerusahaan}'
				and z.Status is null
				and z.No_Production_Order = '{SelectedSplit}'
				and x.Proses = '{SelectedBatch}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Split {SelectedSplit} dan Batch {SelectedBatch} Sudah Melakukan GI", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					isInValidGI = True
					isDialogOpen = False
					Exit Sub
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			isInValidGI = True
			Exit Sub
		End Try

		With EMI_Hasil_Pengeluaran_Bahan_Baku_Baru
			.StartPosition = FormStartPosition.CenterScreen
			.asal = "DISPLAY_RFID"
			.RFID_SelectedSplit = SelectedSplit
			.RFID_SelectedBatch = SelectedBatch
			.FormBorderStyle = FormBorderStyle.None
			.WindowState = FormWindowState.Maximized
			.Show()
			.Focus()
		End With

	End Sub

	Private Sub Validasi_Click()
		If DgvData.SelectedRows.Count = 0 Then Exit Sub

		Dim selectedRow = DgvData.SelectedRows(0)

		If selectedRow.DefaultCellStyle.BackColor = Color.LightGreen Then
			Exit Sub
		End If

		Dim noSplit = selectedRow.Cells(0).Value.ToString()
		Dim batch = selectedRow.Cells(1).Value.ToString()

		If MessageBox.Show($"Yakin Ingin Melakukan Validasi Split {noSplit} dan Batch {batch}???", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			SQL = $"
                INSERT INTO N_EMI_Pairing_RFID_Log (
                    Kode_Perusahaan,
                    No_Split_Production_Order,
                    Kode_Stock_Owner,
                    RFID_Tag,
                    Lokasi_Pairing,
                    Tanggal_Pairing,
                    Jam_Pairing,
                    UserID_Pairing,
                    Urut_Pairing,
                    Lokasi_IN,
                    Tanggal_IN,
                    Jam_IN,
                    UserID_IN,
                    Flag_Scan_Manual,
                    batch,
                    Flag_Cut_Off_Monitoring_Suhu
                )
                SELECT
                    Kode_Perusahaan,
                    No_Split_Production_Order,
                    Kode_Stock_Owner,
                    RFID_Tag,
                    Lokasi_Pairing,
                    Tanggal_Pairing,
                    Jam_Pairing,
                    UserID_Pairing,
                    Urut_Pairing,
                    Lokasi_IN,
                    Tanggal_IN,
                    Jam_IN,
                    UserID_IN,
                    Flag_Scan_Manual,
                    batch,
                    Flag_Cut_Off_Monitoring_Suhu
                FROM N_EMI_Pairing_RFID
                WHERE No_Split_Production_Order = '{noSplit}'
                  AND batch = '{batch}'
            "
			ExecuteTrans(SQL)

			SQL = $"
                DELETE FROM N_EMI_Pairing_RFID
                WHERE No_Split_Production_Order = '{noSplit}'
                  AND batch = '{batch}'
            "
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseConn()

			MessageBox.Show("Data berhasil divalidasi.")

			Fetch_Tracking_RFID()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
		End Try
	End Sub

	Private Sub DgvData_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvData.CellDoubleClick
		If e.RowIndex >= 0 Then
			OpenValidasiDialog(IsBypass:=True)
			If Not isInValidGI Then
				Validasi_Click()
			End If
		End If
	End Sub

	Private Sub Fetch_Tracking_RFID()
		Try
			OpenConn()

			SQL = $"
                WITH AllData AS (
                   SELECT
						a.Kode_Perusahaan,
						a.No_Split_Production_Order,
						a.batch,
						a.Lokasi_Pairing,
						a.Lokasi_IN,
						a.RFID_Tag
					FROM N_EMI_Pairing_RFID a, Emi_split_Production_Order b where
								a.kode_perusahaan = b.kode_perusahaan and a.no_split_production_order = b.No_Transaksi and b.Flag_Hasil_Produksi_GI is null and
					(select count(k.kode_perusahaan) from emi_production_results k, emi_production_results_hpp l
					where k.no_transaksi=l.no_transaksi and l.tanggal is not null and k.no_production_order=a.No_Split_Production_Order and l.proses=a.batch) <a.batch
                ),
                Aggregated AS (
                    SELECT
                        a.No_Split_Production_Order,
                        a.batch,
                        COUNT(CASE WHEN a.Lokasi_Pairing = 'COLD_STORAGE' THEN RFID_Tag END) AS Total_COLD_STORAGE,
						COUNT(CASE WHEN a.Lokasi_Pairing = 'PREMIX' THEN RFID_Tag END) AS Total_PREMIX,
                        COUNT(CASE WHEN a.Lokasi_IN = 'GRINDER_IN' THEN RFID_Tag END) AS Total_GRINDER_IN,
                        COUNT(CASE WHEN a.Lokasi_Pairing = 'GRINDER_OUT' THEN RFID_Tag END) AS Total_GRINDER_OUT,
                        COUNT(CASE WHEN a.Lokasi_IN = 'MIXER_POUCH_IN' THEN RFID_Tag END) AS Total_MIXER_POUCH_IN,
                        COUNT(CASE WHEN a.Lokasi_IN = 'MIXER_CAN_IN' THEN RFID_Tag END) AS Total_MIXER_CAN_IN,
                        tfs.Tanggal AS Tanggal_TFS,
                        tfs.Jam AS Jam_TFS
                    FROM AllData a
                    LEFT JOIN N_EMI_Transaksi_Transfer_Stock_Sementara tfs
                        ON tfs.Kode_Perusahaan = a.Kode_Perusahaan
                        AND tfs.No_Split = a.No_Split_Production_Order
                        AND tfs.Batch = a.Batch
                        AND tfs.Status IS NULL
                    GROUP BY
                        a.No_Split_Production_Order,
                        a.batch,
                        tfs.Tanggal,
                        tfs.Jam
                )
                SELECT *,
                    CASE
                        WHEN
                            (Total_MIXER_POUCH_IN > 0 AND Total_MIXER_CAN_IN = 0 AND Total_GRINDER_OUT = Total_MIXER_POUCH_IN)
                            OR
                            (Total_MIXER_CAN_IN > 0 AND Total_MIXER_POUCH_IN = 0 AND Total_GRINDER_OUT = Total_MIXER_CAN_IN)
                        THEN 1 ELSE 0
                    END AS IsValid
                FROM Aggregated
                ORDER BY IsValid DESC, No_Split_Production_Order, batch;
            "

			DgvData.Rows.Clear()
			Using Dr = OpenTrans(SQL)
				While Dr.Read()

					Dim totalPouch = If(IsDBNull(Dr("Total_MIXER_POUCH_IN")), 0, Convert.ToInt32(Dr("Total_MIXER_POUCH_IN")))
					Dim totalCan = If(IsDBNull(Dr("Total_MIXER_CAN_IN")), 0, Convert.ToInt32(Dr("Total_MIXER_CAN_IN")))
					Dim tanggalTfs = Dr("Tanggal_TFS")
					Dim jamTfs = Dr("Jam_TFS")

					Dim rowIndex = DgvData.Rows.Add(
						Dr("No_Split_Production_Order").ToString(),
						Dr("batch").ToString(),
						If(IsDBNull(Dr("Total_COLD_STORAGE")), 0, Dr("Total_COLD_STORAGE")),
						If(IsDBNull(Dr("Total_PREMIX")), 0, Dr("Total_PREMIX")),
						If(IsDBNull(Dr("Total_GRINDER_IN")), 0, Dr("Total_GRINDER_IN")),
						If(IsDBNull(Dr("Total_GRINDER_OUT")), 0, Dr("Total_GRINDER_OUT")),
						totalPouch,
						totalCan,
						"Validasi"
					)

					Dim totalGrinderOut = If(IsDBNull(Dr("Total_GRINDER_OUT")), 0, Convert.ToInt32(Dr("Total_GRINDER_OUT")))
					Dim isTfsNotNull = Not IsDBNull(tanggalTfs) AndAlso Not IsDBNull(jamTfs)

					'Dim isPouchValid = (totalPouch > 0 AndAlso totalCan = 0 AndAlso totalGrinderOut = totalPouch)
					'Dim isCanValid = (totalCan > 0 AndAlso totalPouch = 0 AndAlso totalGrinderOut = totalCan)

					Dim isPouchValid = totalPouch > 0
					Dim isCanValid = totalCan > 0

					Dim isValid = (isPouchValid OrElse isCanValid)

					If isValid AndAlso isTfsNotNull Then
						DgvData.Rows(rowIndex).DefaultCellStyle.BackColor = Color.LightGreen

						DgvData.Rows(rowIndex).Cells((8)).Style.BackColor = Color.FromArgb(15, 86, 122)
						DgvData.Rows(rowIndex).Cells((8)).Style.ForeColor = Color.White

					End If

				End While
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
		End Try
	End Sub

	Private Sub DgvData_MouseMove(sender As Object, e As MouseEventArgs) Handles DgvData.MouseMove
		Dim info As DataGridView.HitTestInfo = DgvData.HitTest(e.X, e.Y)

		If info.RowIndex >= 0 AndAlso info.ColumnIndex >= 0 Then
			DgvData.Cursor = Cursors.Hand
		Else
			DgvData.Cursor = Cursors.Default
		End If

	End Sub

	Private Sub DgvData_MouseLeave(sender As Object, e As EventArgs) Handles DgvData.MouseLeave
		DgvData.Cursor = Cursors.Default
	End Sub

	Private Sub DgvData_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvData.CellContentClick
		If e.RowIndex >= 0 AndAlso e.ColumnIndex = (8) Then

			'Dim noOrder As String = DgvData.Rows(e.RowIndex).Cells(0).Value.ToString()

			Dim row = DgvData.Rows(e.RowIndex)

			DgvData.ClearSelection()
			row.Selected = True

			OpenValidasiDialog(IsBypass:=False)
			If Not isInValidGI Then
				Validasi_Click()
			End If

			'If row.DefaultCellStyle.BackColor.ToArgb() <> Color.LightGreen.ToArgb() Then
			'	'contextMenu.Show(Cursor.Position)

			'End If
		End If
	End Sub

	Private Sub N_EMI_Display_Validasi_Tracking_RFID_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
		refreshTimer.Stop()
		refreshTimer.Enabled = False
	End Sub

End Class