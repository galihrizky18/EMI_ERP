Imports DocumentFormat.OpenXml.Math

Public Class N_EMI_Display_Validasi_Tracking_RFID
	Public isDialogOpen As Boolean = False
	Dim isInValidGI As Boolean = False

	Private Sub N_EMI_Display_Validasi_Tracking_RFID_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Fetch_Tracking_RFID()

		cmbFilter.Items.Clear()
		cmbFilter.Items.Add("No Production Order")
		cmbFilter.Items.Add("Batch")
	End Sub

	Private Sub OpenValidasiDialog(ByVal isBypassTrackingRFID As Boolean)
		If DgvData.CurrentRow Is Nothing Then Exit Sub

		Dim SelectedSplit As String = DgvData.CurrentRow.Cells(0).Value
		Dim SelectedBatch As String = DgvData.CurrentRow.Cells(1).Value

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'Cek apakah urutan batch GI sesuai
			SQL = $"
				SELECT 
					a.No_Production_Order,
					COUNT(CASE WHEN b.Tanggal IS NOT NULL THEN b.Urut END) AS Total_Batch_Selesai
				FROM Emi_Production_Results a
				JOIN Emi_Production_Results_HPP b 
					ON a.Kode_Perusahaan = b.Kode_Perusahaan 
				   AND a.No_Transaksi = b.No_Transaksi
				WHERE a.Kode_Perusahaan = '{KodePerusahaan}' AND a.No_Production_Order = '{SelectedSplit}'
				GROUP BY a.No_Production_Order;
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If (Val(General_Class.CekNULL(Dr("Total_Batch_Selesai"))) + 1) <> Val(SelectedBatch) Then
						Dr.Close()
						CloseConn()
						MessageBox.Show($"Tidak dapat melakukan GI untuk batch ini. Silahkan lakukan GI secara berurutan sesuai batch.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						isInValidGI = True
						isDialogOpen = False
						Exit Sub
					End If
				Else
					If Val(SelectedBatch) <> 1 Then
						Dr.Close()
						CloseConn()
						MessageBox.Show($"Tidak dapat melakukan GI untuk batch ini. Silahkan lakukan GI secara berurutan sesuai batch.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						isInValidGI = True
						isDialogOpen = False
						Exit Sub
					End If
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

		isDialogOpen = True

		Dim TotMixeInPouch As Integer = Val(HilangkanTanda(DgvData.CurrentRow.Cells(5).Value))
		Dim TotMixeInCan As Integer = Val(HilangkanTanda(DgvData.CurrentRow.Cells(6).Value))

		Try
			OpenConn()

			'Cek stock barang SN
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
								and q.Batch = '{SelectedBatch}'
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

			If Not isBypassTrackingRFID Then
				SQL = $"
					WITH CTE_A AS (
						SELECT Kode_Perusahaan, No_Split_Production_Order, Batch, Lokasi_Pairing, Lokasi_IN
						FROM N_EMI_Pairing_RFID
						WHERE (
							(
								(Lokasi_Pairing = 'COLD_STORAGE' AND Lokasi_IN = 'GRINDER_IN') OR
								(Lokasi_Pairing = 'GRINDER_OUT' AND Lokasi_IN IN ('MIXER_POUCH_IN', 'MIXER_CAN_IN')) OR
								(Lokasi_Pairing = 'PREMIX' AND Lokasi_IN IN ('PREMIX_MIXER_POUCH_IN', 'PREMIX_MIXER_CAN_IN','MIXER_POUCH_IN', 'MIXER_CAN_IN'))
							)
							OR (Lokasi_Pairing IS NOT NULL AND Lokasi_IN IS NULL)
						)
						AND No_Split_Production_Order IS NOT NULL
						AND Batch IS NOT NULL

						UNION ALL

						SELECT Kode_Perusahaan, No_Split_Production_Order, Batch, Lokasi_Pairing, Lokasi_IN
						FROM N_EMI_Pairing_RFID_Log
						WHERE Flag_Hapus_Android IS NULL
						  AND Flag_Cut_Off_Monitoring_Suhu IS NULL
						  AND (
							(
								(Lokasi_Pairing = 'COLD_STORAGE' AND Lokasi_IN = 'GRINDER_IN') OR
								(Lokasi_Pairing = 'GRINDER_OUT' AND Lokasi_IN IN ('MIXER_POUCH_IN', 'MIXER_CAN_IN')) OR
								(Lokasi_Pairing = 'PREMIX' AND Lokasi_IN IN ('PREMIX_MIXER_POUCH_IN', 'PREMIX_MIXER_CAN_IN','MIXER_POUCH_IN', 'MIXER_CAN_IN'))
							)
							OR (Lokasi_Pairing IN ('COLD_STORAGE', 'GRINDER_OUT', 'PREMIX') AND Lokasi_IN IS NULL)
						  )
						  AND No_Split_Production_Order IS NOT NULL
						  AND Batch IS NOT NULL
					), CTE_B AS (
						SELECT
							Kode_Perusahaan, 
							No_Split_Production_Order,
							Batch,
							MAX(CASE WHEN Lokasi_Pairing = 'COLD_STORAGE' THEN 1 ELSE 0 END) AS Box_CS,
							MAX(CASE WHEN Lokasi_IN = 'GRINDER_IN' THEN 1 ELSE 0 END) AS Box_GI,
							MAX(CASE WHEN Lokasi_Pairing = 'GRINDER_OUT' THEN 1 ELSE 0 END) AS Box_GO,
							MAX(CASE WHEN Lokasi_Pairing = 'PREMIX' THEN 1 ELSE 0 END) AS Box_PX,
							MAX(CASE WHEN Lokasi_IN IN ('MIXER_POUCH_IN', 'PREMIX_MIXER_POUCH_IN') THEN 1 ELSE 0 END) AS Box_MP,
							MAX(CASE WHEN Lokasi_IN IN ('MIXER_CAN_IN', 'PREMIX_MIXER_CAN_IN') THEN 1 ELSE 0 END) AS Box_MC
						FROM CTE_A
						GROUP BY Kode_Perusahaan, No_Split_Production_Order, Batch
					)
					SELECT * FROM CTE_B WHERE Kode_Perusahaan = '{KodePerusahaan}' AND No_Split_Production_Order = '{SelectedSplit}' AND Batch = '{SelectedBatch}'
				"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dim Box_CS As Double = If(General_Class.CekNULL(Dr("Box_CS")) = "", 0, Val(HilangkanTanda(Dr("Box_CS"))))
						Dim Box_PX As Double = If(General_Class.CekNULL(Dr("Box_PX")) = "", 0, Val(HilangkanTanda(Dr("Box_PX"))))
						Dim Box_MP As Double = If(General_Class.CekNULL(Dr("Box_MP")) = "", 0, Val(HilangkanTanda(Dr("Box_MP"))))
						Dim Box_MC As Double = If(General_Class.CekNULL(Dr("Box_MC")) = "", 0, Val(HilangkanTanda(Dr("Box_MC"))))

						If Box_CS = 0 Then
							Dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("GI tidak dapat dilakukan karena tidak ada Box dari gudang Cold Storage!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						ElseIf Box_PX = 0 Then
							Dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("GI tidak dapat dilakukan karena tidak ada Box dari gudang Premix!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						ElseIf (Box_MP + Box_MC) = 0 Then
							Dr.Close()
							CloseTrans()
							CloseConn()
							MessageBox.Show("GI tidak dapat dilakukan karena belum ada box yang sampai di Mixer!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub
						End If
					Else
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show("Tidak ada data box untuk split dan batch ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using
			End If

			'Cek apakah data sudah GI
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
			MessageBox.Show($"Terjadi kesalahan saat memvalidasi data split {SelectedSplit} dan batch {SelectedBatch}" & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			isInValidGI = True
			Exit Sub
		End Try

		With EMI_Hasil_Pengeluaran_Bahan_Baku_Baru
			.StartPosition = FormStartPosition.CenterScreen
			.asal = "DISPLAY_RFID"
			.RFID_SelectedSplit = SelectedSplit
			.RFID_SelectedBatch = SelectedBatch
			.isBypassTrackingRFID = isBypassTrackingRFID
			.FormBorderStyle = FormBorderStyle.None
			.WindowState = FormWindowState.Maximized
			.Show()
			.Focus()
		End With
	End Sub

	Private Sub Lepas_Gantungan_Data_Tracking_RFID()
		If DgvData.SelectedRows.Count = 0 Then Exit Sub

		Dim selectedRow = DgvData.SelectedRows(0)

		If selectedRow.DefaultCellStyle.BackColor = Color.LightGreen Then
			Exit Sub
		End If

		Dim SelectedSplit As String = DgvData.CurrentRow.Cells(0).Value
		Dim SelectedBatch As String = DgvData.CurrentRow.Cells(1).Value

		If MessageBox.Show($"Yakin Ingin Melakukan Validasi Split {SelectedSplit} dan Batch {SelectedBatch}?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

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
                WHERE No_Split_Production_Order = '{SelectedSplit}'
                  AND batch = '{SelectedBatch}'
            "
			ExecuteTrans(SQL)

			SQL = $"
                DELETE FROM N_EMI_Pairing_RFID
                WHERE No_Split_Production_Order = '{SelectedSplit}'
                  AND batch = '{SelectedBatch}'
            "
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseConn()

			MessageBox.Show("Data berhasil divalidasi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

			Fetch_Tracking_RFID()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show($"Gagal melepas gantungan data RFID untuk split {SelectedSplit} dan batch {SelectedBatch}" & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
		End Try
	End Sub

	Private Sub DgvData_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvData.CellDoubleClick
		If e.RowIndex >= 0 Then
			Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin melakukan validasi dengan bypass tracking RFID?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

			Try
				OpenConn()

				If CekButtonRole("Lewati_Validasi_Tracking_RFID") = "T" Then
					MessageBox.Show("Anda tidak memiliki akses untuk melakukan bypass validasi tracking RFID.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
					Exit Sub
				End If

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show("Gagal cek role user: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End Try

			If result = DialogResult.Yes Then
				OpenValidasiDialog(isBypassTrackingRFID:=True)

				If Not isInValidGI Then
					Lepas_Gantungan_Data_Tracking_RFID()
				End If
			End If
		End If
	End Sub

	Private Sub Fetch_Tracking_RFID()
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim filterValue As String = tbSearch.Text.Trim()
			Dim filterIndex As Integer = cmbFilter.SelectedIndex
			Dim whereFilter As String = ""

			If filterValue <> "" AndAlso filterIndex = -1 Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Silahkan pilih filter pencarian.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			End If

			If filterValue <> "" Then
				If filterIndex = 0 Then
					whereFilter = $"AND b.No_Split_Production_Order LIKE '%{filterValue}%'"
				ElseIf filterIndex = 1 Then
					whereFilter = $"AND b.batch LIKE '%{filterValue}%'"
				End If
			End If

			SQL = $"
                WITH CTE_A AS (
					SELECT Kode_Perusahaan, No_Split_Production_Order, Batch, Lokasi_Pairing, Lokasi_IN
					FROM N_EMI_Pairing_RFID
					WHERE (
						(
							(Lokasi_Pairing = 'COLD_STORAGE' AND Lokasi_IN = 'GRINDER_IN') OR
							(Lokasi_Pairing = 'GRINDER_OUT' AND Lokasi_IN IN ('MIXER_POUCH_IN', 'MIXER_CAN_IN')) OR
							(Lokasi_Pairing = 'PREMIX' AND Lokasi_IN IN ('PREMIX_MIXER_POUCH_IN', 'PREMIX_MIXER_CAN_IN','MIXER_POUCH_IN', 'MIXER_CAN_IN'))
						)
						OR (Lokasi_Pairing IN ('COLD_STORAGE', 'GRINDER_OUT', 'PREMIX') AND Lokasi_IN IS NULL)
					)
					AND No_Split_Production_Order IS NOT NULL
					AND Batch IS NOT NULL

					UNION ALL

					SELECT Kode_Perusahaan, No_Split_Production_Order, Batch, Lokasi_Pairing, Lokasi_IN
					FROM N_EMI_Pairing_RFID_Log
					WHERE Flag_Hapus_Android IS NULL
					  AND Flag_Cut_Off_Monitoring_Suhu IS NULL
					  AND (
						(
							(Lokasi_Pairing = 'COLD_STORAGE' AND Lokasi_IN = 'GRINDER_IN') OR
							(Lokasi_Pairing = 'GRINDER_OUT' AND Lokasi_IN IN ('MIXER_POUCH_IN', 'MIXER_CAN_IN')) OR
							(Lokasi_Pairing = 'PREMIX' AND Lokasi_IN IN ('PREMIX_MIXER_POUCH_IN', 'PREMIX_MIXER_CAN_IN','MIXER_POUCH_IN', 'MIXER_CAN_IN'))
						)
						OR (Lokasi_Pairing IS NOT NULL AND Lokasi_IN IS NULL)
					  )
					  AND No_Split_Production_Order IS NOT NULL
					  AND Batch IS NOT NULL
				), CTE_B AS (
					SELECT
						Kode_Perusahaan, 
						No_Split_Production_Order,
						Batch,
						MAX(CASE WHEN Lokasi_Pairing = 'COLD_STORAGE' THEN 1 ELSE 0 END) AS Box_CS,
						MAX(CASE WHEN Lokasi_IN = 'GRINDER_IN' THEN 1 ELSE 0 END) AS Box_GI,
						MAX(CASE WHEN Lokasi_Pairing = 'GRINDER_OUT' THEN 1 ELSE 0 END) AS Box_GO,
						MAX(CASE WHEN Lokasi_Pairing = 'PREMIX' THEN 1 ELSE 0 END) AS Box_PX,
						MAX(CASE WHEN Lokasi_IN IN ('MIXER_POUCH_IN', 'PREMIX_MIXER_POUCH_IN') THEN 1 ELSE 0 END) AS Box_MP,
						MAX(CASE WHEN Lokasi_IN IN ('MIXER_CAN_IN', 'PREMIX_MIXER_CAN_IN') THEN 1 ELSE 0 END) AS Box_MC
					FROM CTE_A
					GROUP BY Kode_Perusahaan, No_Split_Production_Order, Batch
				),
				CTE_D AS (
					SELECT *
					FROM (
						SELECT 
							tfs.*,
							ROW_NUMBER() OVER (PARTITION BY tfs.Kode_Perusahaan, tfs.No_Split, tfs.Batch ORDER BY tfs.Tanggal DESC, tfs.Jam DESC) AS rn
						FROM N_EMI_Transaksi_Transfer_Stock_Sementara tfs
						WHERE tfs.Status IS NULL
					) x
					WHERE x.rn = 1
				)
				SELECT b.No_Split_Production_Order, b.Batch, b.Box_CS, b.Box_GI, b.Box_GO, b.Box_PX, b.Box_MP, b.Box_MC, d.Tanggal, d.Jam FROM CTE_B b
				LEFT JOIN CTE_D d ON d.Kode_Perusahaan = b.Kode_Perusahaan AND d.No_Split = b.No_Split_Production_Order AND d.Batch = b.Batch
				LEFT JOIN Emi_Production_Results a
					ON b.Kode_Perusahaan = a.Kode_Perusahaan
				   AND b.No_Split_Production_Order = a.No_Production_Order
				LEFT JOIN Emi_Production_Results_HPP c
					ON a.Kode_Perusahaan = c.Kode_Perusahaan
				   AND a.No_Transaksi = c.No_Transaksi
				   AND c.Proses = b.Batch
				WHERE (c.Tanggal IS NULL OR c.No_Transaksi IS NULL)
				{whereFilter}
				GROUP BY b.No_Split_Production_Order, b.Batch, b.Box_CS, b.Box_GI, b.Box_GO, b.Box_PX, b.Box_MP, b.Box_MC, d.Tanggal, d.Jam;
            "
			DgvData.Rows.Clear()
			Using Dr = OpenTrans(SQL)
				While Dr.Read()
					Dim totalPouch = Val(General_Class.CekNULL(Dr("Box_MP")))
					Dim totalCan = Val(General_Class.CekNULL(Dr("Box_MC")))
					Dim tanggalTfs = General_Class.CekNULL(Dr("Tanggal"))
					Dim jamTfs = General_Class.CekNULL(Dr("Jam"))
					Dim isTfsNotNull = (tanggalTfs <> "" AndAlso jamTfs <> "")

					Dim rowIndex = DgvData.Rows.Add(
						General_Class.CekNULL(Dr("No_Split_Production_Order")),
						General_Class.CekNULL(Dr("Batch")),
						Val(General_Class.CekNULL(Dr("Box_CS"))),
						Val(General_Class.CekNULL(Dr("Box_PX"))),
						Val(General_Class.CekNULL(Dr("Box_GI"))),
						Val(General_Class.CekNULL(Dr("Box_GO"))),
						totalPouch,
						totalCan,
						"Validasi"
					)

					Dim isPouchValid = totalPouch > 0
					Dim isCanValid = totalCan > 0
					Dim isValid = (isPouchValid OrElse isCanValid)

					If isValid AndAlso isTfsNotNull Then
						DgvData.Rows(rowIndex).DefaultCellStyle.BackColor = Color.LightGreen

						DgvData.Rows(rowIndex).Cells(8).Style.BackColor = Color.FromArgb(15, 86, 122)
						DgvData.Rows(rowIndex).Cells(8).Style.ForeColor = Color.White
					End If

				End While
			End Using

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show("Gagal load data tracking RFID: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
			Dim row = DgvData.Rows(e.RowIndex)

			DgvData.ClearSelection()
			row.Selected = True

			OpenValidasiDialog(isBypassTrackingRFID:=False)
			If Not isInValidGI Then
				Lepas_Gantungan_Data_Tracking_RFID()
			End If
		End If
	End Sub

	Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
		cmbFilter.SelectedIndex = -1
		tbSearch.Text = ""

		Fetch_Tracking_RFID()
	End Sub

	Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
		Fetch_Tracking_RFID()
	End Sub
End Class