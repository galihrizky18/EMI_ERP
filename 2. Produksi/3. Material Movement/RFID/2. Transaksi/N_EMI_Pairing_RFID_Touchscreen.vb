Imports System.ComponentModel
Imports System.IO
Imports ERP_EMI.Devices.RFID.HW_VX6346KL

Public Class N_EMI_Pairing_RFID_Touchscreen

	Dim Random As New Random()

	'RFID Reader class
	Private RFIDReader As HW_VX6346KL_Reader

	'Buffer untuk menyimpan data scan dari scanner manual
	Private ScanBuffer As String = ""

	Private ScannedTags As New HashSet(Of String)

	'Pagination variables
	Private CurrentPage As Integer = 1

	Private PageSize As Integer = 19
	Private TotalData As Integer = 0
	Private TotalPage As Integer = 0

	'Flag untuk menentukan metode scan
	Private FlagScanManual As Boolean = False

	'IP RFID Reader
	Private RFIDReaderIP As String = "0.0.0.0"

	Private RFIDReaderPower As String = 3

	'No Transaksi
	Private SelectedNoFaktur As String = ""

	Private SelectedBatch As String = ""

	Dim SuccessPotongStock As Boolean = False

	Dim RFID_Non_Aktif As Boolean = False

	Private Sub N_EMI_Pairing_RFID_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Me.KeyPreview = True

		'Setup Lv RFID Tags
		With Lv_RFID_Tags
			.Clear()
			.View = View.Details
			.FullRowSelect = True
			.GridLines = True

			.Columns.Add("RFID Tag", 0, HorizontalAlignment.Left)
			.Columns.Add("RFID Label", 350, HorizontalAlignment.Left)
		End With

		Try
			OpenConn()

			SQL = "SELECT IP_Address, Power From N_EMI_Master_Data_RFID_Readers WHERE Kode_Perangkat='COLD_STORAGE_PAIRING' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read() Then
					RFIDReaderIP = Dr("IP_Address").ToString()
					If Not IsDBNull(Dr("Power")) Then
						RFIDReaderPower = Convert.ToInt32(Dr("Power"))
					Else
						RFIDReaderPower = 3
					End If

				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try

		Try
			OpenConn()

			'====================
			'=     CEK INIT     =
			'====================
			SQL = $"select Flag_Cold_Storage_RFID_Mati, Tag_RFID_Default from Init where Kode_Perusahaan = '{KodePerusahaan}'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If General_Class.CekNULL(Dr("Flag_Cold_Storage_RFID_Mati")) = "Y" Then
						If General_Class.CekNULL(Dr("Tag_RFID_Default")) = "" Then
							Dr.Close()
							CloseConn()
							MessageBox.Show("Terjadi Kesalahan, RFID Default Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
							Exit Sub
						End If
						RFID_Non_Aktif = True
					Else
						RFID_Non_Aktif = False
					End If
				Else
					Dr.Close()
					CloseConn()
					MessageBox.Show("Terjadi Kesalahan, Data Init Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
					Exit Sub
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		If RFID_Non_Aktif Then
			Dgv_Menunggu_Pairing.Columns(2).Visible = False
			Btn_Simpan_Pairing.Visible = False
			Btn_Disconnect_RFID.Visible = False
		Else
			Dgv_Menunggu_Pairing.Columns(2).Visible = True
			Btn_Simpan_Pairing.Visible = True
			Btn_Disconnect_RFID.Visible = True
		End If

		'Load Tab Menunggu Pairing
		TabControl1.SelectedIndex = 0
		Fetch_Menunggu_Pairing_RFID()

		'RFID Reader Setup
		RFIDReader = New HW_VX6346KL_Reader(RFIDReaderIP, 6000)
		AddHandler RFIDReader.Connected, AddressOf RFID_Connected
		AddHandler RFIDReader.Disconnected, AddressOf RFID_Disconnected
		AddHandler RFIDReader.TagDetected, AddressOf RFID_TagDetected

		Dgv_Menunggu_Pairing.Columns(4).DisplayIndex = 1

	End Sub

	Private Sub RFID_Connected()
		Btn_Disconnect_RFID.Visible = True
	End Sub

	Private Sub RFID_Disconnected()
		Btn_Disconnect_RFID.Visible = False
	End Sub

	'RFID Reader class ketika detect tag
	Private Sub RFID_TagDetected(tag As String)
		If Lv_RFID_Tags.InvokeRequired Then
			Lv_RFID_Tags.Invoke(New Action(Of String)(AddressOf RFID_TagDetected), tag)
			Return
		End If
		For Each itm As ListViewItem In Lv_RFID_Tags.Items
			If itm.Text = tag Then Return
		Next
		If Not IsRFIDTagCanBeUsed(tag) Then Return

		Dim RfIDLabel As String = ""
		Try
			OpenConn()

			SQL = $"select RFID_Tag, RFID_Label from N_EMI_Master_Data_RFID_Tags where Kode_Perusahaan = '{KodePerusahaan}' and RFID_Tag = '{tag}' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					RfIDLabel = Dr("RFID_Label")
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Dim Lv As ListViewItem
		Lv = Lv_RFID_Tags.Items.Add(tag)
		Lv.SubItems.Add(RfIDLabel)

		'Lv_RFID_Tags.Items.Add(New ListViewItem(tag))
	End Sub

	Private Function IsRFIDTagCanBeUsed(rfidTag As String) As Boolean
		Try
			Dim RfIDLabel As String = ""
			Try
				OpenConn()

				SQL = $"select RFID_Tag, RFID_Label from N_EMI_Master_Data_RFID_Tags where Kode_Perusahaan = '{KodePerusahaan}' and RFID_Tag = '{rfidTag}' "
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						RfIDLabel = Dr("RFID_Label")
					End If
				End Using

				CloseConn()
			Catch ex As Exception
				CloseConn()
				MessageBox.Show(ex.Message)
				Return False
			End Try

			OpenConn()

			' cek master tag
			Dim sqlMaster As String = $"
                SELECT No_Production_Order, Batch
                FROM N_EMI_Master_Data_RFID_Tags
                WHERE RFID_Tag = @RFID_Tag
            "

			Cmd.Parameters.Clear()
			Cmd.Parameters.AddWithValue("@RFID_Tag", rfidTag)

			Dim noPO As String = ""
			Dim batch As String = ""

			Using dr = OpenTrans(sqlMaster)
				If dr.Read() Then
					noPO = dr("No_Production_Order").ToString()
					batch = dr("Batch").ToString()
				End If
			End Using

			' jika kosong boleh langsung dipakai
			If String.IsNullOrEmpty(noPO) AndAlso String.IsNullOrEmpty(batch) Then
				Return True
			End If

			' cek pairing dan TF
			Dim sqlCheck As String = "
                SELECT
                    b.Tanggal AS TglTFSementara,
                    c.Tanggal AS TglTFParent
                FROM N_EMI_Pairing_RFID a
                LEFT JOIN N_EMI_Transaksi_Transfer_Stock_Sementara b
                    ON a.Kode_Perusahaan = b.Kode_Perusahaan
                    AND a.No_Split_Production_Order = b.No_Split
                    AND b.Status IS NULL
                LEFT JOIN Tf_Stock_Parent c
                    ON c.Kode_Perusahaan = a.Kode_Perusahaan
                    AND c.No_Split = a.No_Split_Production_Order
                    AND c.Status IS NULL
                WHERE a.RFID_Tag = @RFID_Tag
                  AND a.Lokasi_IN IS NULL
                  AND a.Tanggal_IN IS NULL
                  AND a.Jam_IN IS NULL
                  AND a.Lokasi_Pairing = 'COLD_STORAGE'
            "

			Cmd.Parameters.Clear()
			Cmd.Parameters.AddWithValue("@RFID_Tag", rfidTag)

			Using dr = OpenTrans(sqlCheck)

				If dr.Read() Then
					' cek TF parent
					If Not IsDBNull(dr("TglTFParent")) Then

						MessageBox.Show(
						$"Box {RfIDLabel} sudah dipakai di No Production {noPO} Batch {batch} dan sudah TF Stock.",
						"Informasi",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information)

						Return False

					ElseIf Not IsDBNull(dr("TglTFSementara")) Then

						MessageBox.Show(
						$"Box {RfIDLabel} sudah dipakai di No Production {noPO} Batch {batch} dan sudah TF Stock Sementara.",
						"Informasi",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information)

						Return False
					Else
						Dim res = MessageBox.Show(
						$"Box {RfIDLabel} sudah dipakai di No Production {noPO} Batch {batch}. Yakin mau pakai box ini?",
						"Konfirmasi",
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Warning)

						If res = DialogResult.No Then
							Return False
						End If

					End If

				End If

			End Using

			Return True
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return False
		End Try
	End Function

	Private Sub LoadTotalDataMenungguPairing()
		Dim sqlTotal As String = "
            SELECT COUNT(*) AS Total_Data
            FROM (
                SELECT a.No_Transaksi
                FROM Emi_Split_Production_Order a
                JOIN Emi_Material_Requisition b
                    ON b.Kode_Perusahaan = a.Kode_Perusahaan
                    AND b.No_Faktur_Order = a.No_Transaksi
                JOIN Emi_Material_Requisition_Det c
                    ON c.Kode_Perusahaan = b.Kode_Perusahaan
                    AND c.No_Faktur = b.No_Faktur
                JOIN Emi_Material_Requisition_Det_Convert d
                    ON d.Kode_Perusahaan = c.Kode_Perusahaan
                    AND d.No_Urut_Det = c.Urut_Oto
                WHERE NOT EXISTS (
                    SELECT 1
                    FROM N_EMI_Pairing_RFID x
                    WHERE x.No_Split_Production_Order = a.No_Transaksi AND x.Lokasi_Pairing = 'COLD_STORAGE'
                ) AND NOT EXISTS (
                    SELECT 1
                    FROM N_EMI_Pairing_RFID_Log x
                    WHERE x.No_Split_Production_Order = a.No_Transaksi AND x.Lokasi_Pairing = 'COLD_STORAGE'
                )
                AND a.Kode_Perusahaan = @KodePerusahaan
                AND a.Status IS NULL
                GROUP BY a.No_Transaksi, b.batch
            ) AS Total_Data;
        "

		Cmd.Parameters.Clear()
		Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)

		Using Dr = OpenTrans(sqlTotal)
			If Dr.Read() Then
				TotalData = If(IsDBNull(Dr("Total_Data")), 0, Convert.ToInt32(Dr("Total_Data")))
			End If
			Dr.Close()
		End Using

		TotalPage = Math.Ceiling(TotalData / PageSize)
	End Sub

	Private Sub LoadTotalDataSudahPairing()
		Dim sqlTotal As String = "
            SELECT COUNT(*) AS Total_Data
            FROM Emi_Split_Production_Order a
            WHERE EXISTS (
                SELECT 1
                FROM N_EMI_Pairing_RFID b
                WHERE b.No_Split_Production_Order = a.No_Transaksi AND b.Lokasi_Pairing = 'COLD_STORAGE'
            ) OR EXISTS (
                SELECT 1
                FROM N_EMI_Pairing_RFID_LOg b
                WHERE b.No_Split_Production_Order = a.No_Transaksi AND b.Lokasi_Pairing = 'COLD_STORAGE'
            )
            AND a.Kode_Perusahaan = @KodePerusahaan
            AND a.Status IS NULL;
        "

		Cmd.Parameters.Clear()
		Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)

		Using Dr = OpenTrans(sqlTotal)
			If Dr.Read() Then
				TotalData = If(IsDBNull(Dr("Total_Data")), 0, Convert.ToInt32(Dr("Total_Data")))
			End If
			Dr.Close()
		End Using

		TotalPage = Math.Ceiling(TotalData / PageSize)
	End Sub

	Private Sub UpdatePaginationUI()
		Dim StartData As Integer = ((CurrentPage - 1) * PageSize) + 1
		Dim EndData As Integer = Math.Min(CurrentPage * PageSize, TotalData)
		Dim ShowingData As Integer = If(TabControl1.SelectedIndex = 0, Dgv_Menunggu_Pairing.Rows.Count, Dgv_Sudah_Pairing.Rows.Count)

		Lb_Pagination.Text =
		$"Halaman {CurrentPage} dari {TotalPage}, menampilkan {ShowingData} dari {TotalData} data"

		Btn_Prev.Enabled = CurrentPage > 1
		Btn_Next.Enabled = CurrentPage < TotalPage
	End Sub

	Private Sub Fetch_Menunggu_Pairing_RFID()
		Try
			OpenConn()
			Dgv_Menunggu_Pairing.Rows.Clear()

			LoadTotalDataMenungguPairing()

			SQL = $"
                SELECT
                    a.No_Transaksi AS No_Split_PO,
                    a.Flag_Scan_Manual,
                    FORMAT(a.Tgl_Produksi, 'dd MMM yyy') AS Tanggal_Produksi,
                    a.Jam_Produksi,
                    a.No_Batch,
                    CASE
                        WHEN SUM(CASE WHEN d.Flag_Transfer = 'Y' THEN 1 ELSE 0 END) = COUNT(*)
                            THEN 'SELESAI PROSES'
                        WHEN SUM(CASE WHEN d.Flag_Transfer = 'Y' THEN 1 ELSE 0 END) > 0
                            THEN 'SEDANG DIPROSES'
                        ELSE 'BELUM DIPROSES'
                    END AS Status_Proses, b.Batch
                FROM Emi_Split_Production_Order a
                JOIN Emi_Material_Requisition b
                    ON b.Kode_Perusahaan = a.Kode_Perusahaan
                    AND b.No_Faktur_Order = a.No_Transaksi
                JOIN Emi_Material_Requisition_Det c
                    ON c.Kode_Perusahaan = b.Kode_Perusahaan
                    AND c.No_Faktur = b.No_Faktur
                JOIN Emi_Material_Requisition_Det_Convert d
                    ON d.Kode_Perusahaan = c.Kode_Perusahaan
                    AND d.No_Urut_Det = c.Urut_Oto
                inner join Stock_Owner_Gudang e on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Kode_Stock_Owner_Tujuan = e.Kode_Stock_Owner and e.Flag_Cold_Storage = 'Y'
                WHERE
                (
                    (
                        NOT EXISTS (
                            SELECT 1
                            FROM N_EMI_Pairing_RFID x
                            WHERE x.No_Split_Production_Order = a.No_Transaksi
                              AND x.batch = b.batch
                              AND x.Lokasi_Pairing = 'COLD_STORAGE'
                        )
                        AND
                        NOT EXISTS (
                            SELECT 1
                            FROM N_EMI_Pairing_RFID_Log x
                            WHERE x.No_Split_Production_Order = a.No_Transaksi
                              AND x.batch = b.batch
                              AND x.Lokasi_Pairing = 'COLD_STORAGE'
                        )
                    )
                    or not exists (
                        select 1
                        from N_EMI_Transaksi_Transfer_Stock_Sementara z
                            inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Detail x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
                            inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Det y on x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_TF and y.Flag_Validasi = 'Y'
                            inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 k on y.kode_perusahaan = k.kode_perusahaan and y.No_Faktur = k.No_Faktur and y.Urut_Oto = k.Urut_Det
                        where z.Kode_Perusahaan = d.Kode_Perusahaan
                        and x.Urut_Material_Requisition_Convert = d.Urut_Oto
                    )
                )
                AND a.Kode_Perusahaan = @KodePerusahaan
                AND a.Status IS NULL
                GROUP BY a.No_Transaksi, a.Tgl_Produksi, a.Jam_Produksi, a.No_Batch, a.Flag_Scan_Manual, b.Batch
                ORDER BY a.Tgl_Produksi DESC, a.Jam_Produksi DESC, a.No_Transaksi, b.Batch
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
            "

			Dim Offset As Integer = (CurrentPage - 1) * PageSize

			Cmd.Parameters.Clear()
			Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
			Cmd.Parameters.AddWithValue("@Offset", Offset)
			Cmd.Parameters.AddWithValue("@PageSize", PageSize)

			Using Dr = OpenTrans(SQL)
				While Dr.Read()
					Dim rowIndex As Integer = Dgv_Menunggu_Pairing.Rows.Add(
						If(IsDBNull(Dr("No_Split_PO")), "", Dr("No_Split_PO").ToString()),
						If(IsDBNull(Dr("Tanggal_Produksi")), "", Dr("Tanggal_Produksi").ToString()),
						"Mulai Pairing",
						If(IsDBNull(Dr("Flag_Scan_Manual")), "", Dr("Flag_Scan_Manual").ToString()),
						If(IsDBNull(Dr("Batch")), "", If(Dr("Batch") = "0", "Packaging", Dr("Batch")))
					)

					Dim status As String = If(IsDBNull(Dr("Status_Proses")), "", Dr("Status_Proses").ToString())

					Select Case status
						Case "SELESAI PROSES"
							Dgv_Menunggu_Pairing.Rows(rowIndex).DefaultCellStyle.BackColor = Color.LightGreen

						Case "SEDANG DIPROSES"
							Dgv_Menunggu_Pairing.Rows(rowIndex).DefaultCellStyle.BackColor = Color.LightYellow

						Case "BELUM DIPROSES"
							Dgv_Menunggu_Pairing.Rows(rowIndex).DefaultCellStyle.BackColor = Color.LightCoral
					End Select
				End While

				Dr.Close()
			End Using

			UpdatePaginationUI()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Private Sub Fetch_Sudah_Pairing_RFID()
		Try
			OpenConn()
			Dgv_Sudah_Pairing.Rows.Clear()

			LoadTotalDataSudahPairing()

			'SQL = $"
			'    SELECT
			'        a.No_Transaksi AS No_Split_PO, a.Flag_Scan_Manual,
			'        FORMAT(a.Tgl_Produksi, 'dd MMM yyy') AS Tanggal_Produksi, a.Jam_Produksi, a.No_Batch, c.batch
			'    FROM Emi_Split_Production_Order a
			'    inner join Emi_Material_Requisition c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Transaksi = c.No_Faktur_Order
			'    WHERE EXISTS (
			'        SELECT 1
			'        FROM N_EMI_Pairing_RFID b
			'        WHERE b.No_Split_Production_Order = a.No_Transaksi
			'        and b.batch = c.Batch
			'    )
			'    AND a.Kode_Perusahaan = @KodePerusahaan
			'    AND a.Status IS NULL
			'    ORDER BY a.Tgl_Produksi DESC, a.Jam_Produksi DESC
			'    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
			'"

			SQL = $"
                ;with Cte as (
                    select distinct z.Kode_Perusahaan, p.No_Faktur, p.Urut_Oto, o.batch, o.No_Faktur_Order
                    from N_EMI_Transaksi_Transfer_Stock_Sementara z
                        inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Detail x on z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur
                        inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Det k on x.Kode_Perusahaan = k.Kode_Perusahaan and x.No_Faktur = k.No_Faktur and x.Urut_Oto = k.Urut_TF
                            and k.Flag_Validasi = 'Y' and k.Selesai = 'Y'
                        inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 l on k.Kode_Perusahaan = l.Kode_Perusahaan and k.No_Faktur = l.No_Faktur and k.Urut_Oto = l.Urut_Det
                        inner join Emi_Material_Requisition_Det_Convert p on x.Kode_Perusahaan = p.Kode_Perusahaan and x.Urut_Material_Requisition_Convert = p.Urut_Oto
                        inner join Emi_Material_Requisition o on p.kode_perusahaan = o.kode_perusahaan and p.no_faktur = o.no_Faktur
                    where z.Status is null
                )
                SELECT
                    a.No_Transaksi AS No_Split_PO, a.Flag_Scan_Manual,
                    a.Tgl_Produksi AS Tanggal_Produksi, a.Jam_Produksi, a.No_Batch, c.batch, c.No_Faktur
                FROM Emi_Split_Production_Order a
                inner join Emi_Material_Requisition c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Transaksi = c.No_Faktur_Order
                WHERE
                (
                    --EXISTS (
                        --SELECT 1
                        --FROM N_EMI_Pairing_RFID p
                        --WHERE p.No_Split_Production_Order = a.No_Transaksi
                          --AND p.Batch = c.Batch
                    --)
                    (
		                EXISTS (
                            SELECT 1
                            FROM N_EMI_Pairing_RFID_Log x
            	                inner join Cte z on x.kode_perusahaan = z.kode_perusahaan and x.No_Split_Production_Order = z.No_Faktur_Order
                            WHERE x.No_Split_Production_Order = a.No_Transaksi
                              AND x.batch = z.batch
                              AND x.Lokasi_Pairing = 'COLD_STORAGE'
                        )
                        or
                        EXISTS (
                            SELECT 1
                            FROM N_EMI_Pairing_RFID x
            	                inner join Cte z on x.kode_perusahaan = z.kode_perusahaan and x.No_Split_Production_Order = z.No_Faktur_Order
                            WHERE x.No_Split_Production_Order = a.No_Transaksi
                                AND x.batch = z.batch
                                AND x.Lokasi_Pairing = 'COLD_STORAGE'
                        )

                    )
                    and
                    EXISTS (
                        SELECT 1
                        FROM Cte z
                        WHERE z.Kode_Perusahaan = c.Kode_Perusahaan AND z.No_Faktur = c.No_Faktur
                    )
                )
                AND a.Kode_Perusahaan = @KodePerusahaan
                AND a.Status IS NULL
                ORDER BY a.Tgl_Produksi DESC, a.Jam_Produksi DESC
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
            "

			Dim Offset As Integer = (CurrentPage - 1) * PageSize

			Cmd.Parameters.Clear()
			Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
			Cmd.Parameters.AddWithValue("@Offset", Offset)
			Cmd.Parameters.AddWithValue("@PageSize", PageSize)

			Using Dr = OpenTrans(SQL)
				While Dr.Read()
					Dim rowIndex As Integer = Dgv_Sudah_Pairing.Rows.Add(
						If(IsDBNull(Dr("No_Split_PO")), "", Dr("No_Split_PO").ToString()),
						If(IsDBNull(Dr("Tanggal_Produksi")), "", Format(Dr("Tanggal_Produksi"), "dd MMM yyyy")),
						"Pairing Ulang",
						If(IsDBNull(Dr("Flag_Scan_Manual")), "", Dr("Flag_Scan_Manual").ToString()),
						If(IsDBNull(Dr("Batch")), "", If(Dr("Batch") = "0", "Packaging", Dr("Batch")))
					)
				End While

				Dr.Close()
			End Using

			UpdatePaginationUI()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Private Sub Fetch_Dgv_Detail(ByVal NoFakturOrder As String, ByVal Batch As Integer)
		Select Case TabControl1.SelectedIndex
			Case 0
				If Dgv_Menunggu_Pairing.CurrentRow Is Nothing Then Exit Sub
			Case 1
				If Dgv_Sudah_Pairing.CurrentRow Is Nothing Then Exit Sub
		End Select

		Try
			OpenConn()
			Dgv_Detail.Rows.Clear()

			'SQL = "
			'    SELECT
			'        a.No_Faktur,
			'        b.Kode_Barang,
			'        c.Nama,
			'        b.Jumlah AS Qty_MR,
			'        COALESCE(d.Total, 0) AS Qty_TF,
			'        b.Satuan,
			'        CASE
			'            WHEN COALESCE(d.Total, 0) = 0 THEN 'BELUM DIPROSES'
			'            WHEN COALESCE(d.Total, 0) < b.Jumlah THEN 'SEDANG DIPROSES'
			'            ELSE 'SELESAI PROSES'
			'        END AS Status_Proses
			'    FROM Emi_Material_Requisition a
			'    JOIN Emi_Material_Requisition_Det_Convert b
			'        ON  b.Kode_Perusahaan = a.Kode_Perusahaan
			'        AND b.No_Faktur       = a.No_Faktur
			'    LEFT JOIN Tf_Stock d
			'        ON  d.Kode_Perusahaan = b.Kode_Perusahaan
			'        AND d.Urut_Material_Requisition_Convert = b.Urut_Oto
			'    JOIN Barang c
			'        ON  c.Kode_Perusahaan  = b.Kode_Perusahaan
			'        AND c.Kode_Barang      = b.Kode_Barang
			'        AND c.Kode_Stock_Owner = b.Kode_Stock_Owner
			'    WHERE a.No_Faktur_Order = @NoFakturOrder
			'        AND a.Kode_Perusahaan = @KodePerusahaan
			'"

			SQL = "
                ;WITH cte_tf AS (
                    SELECT x.Kode_Perusahaan, x.Urut_Material_Requisition_Convert, SUM(x.Jumlah) AS Qty_TF
                    FROM (
                        SELECT a.Kode_Perusahaan, b.Urut_Material_Requisition_Convert, d.Jumlah
                        FROM N_EMI_Transaksi_Transfer_Stock_Sementara a
                        JOIN N_EMI_Transaksi_Transfer_Stock_Sementara_Detail b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Faktur = b.No_Faktur
                        JOIN N_EMI_Transaksi_Transfer_Stock_Sementara_Det c ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.No_Faktur = c.No_Faktur AND b.Urut_Oto  = c.Urut_TF
                        JOIN N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 d ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.No_Faktur = d.No_Faktur AND c.Urut_Oto = d.Urut_Det
                        WHERE a.Status IS NULL
                    ) x
                    GROUP BY x.Kode_Perusahaan, x.Urut_Material_Requisition_Convert
                )

                SELECT
                    a.No_Faktur, b.Kode_Barang, e.Nama, b.Jumlah AS Qty_MR, COALESCE(f.Qty_TF, 0) AS Qty_TF, b.Satuan,
                    --CASE
                        --WHEN COALESCE(f.Qty_TF, 0) = 0 THEN 'BELUM DIPROSES'
                        --WHEN COALESCE(f.Qty_TF, 0) < b.Jumlah THEN 'SEDANG DIPROSES'
                        --ELSE 'SELESAI PROSES'
                    --END AS Status_Proses,
                    c.Kode_Stock_Owner_Tujuan,
                    CASE
                        WHEN COALESCE(f.Qty_TF, 0) = 0 THEN 'BELUM DIPROSES'

                        WHEN COALESCE(f.Qty_TF, 0) BETWEEN
                            (b.Jumlah - (b.Jumlah * e.Toleransi_Tf_Min / 100.0))
                            AND
                            (b.Jumlah + (b.Jumlah * e.Toleransi_Tf_Max / 100.0))
                        THEN 'SELESAI PROSES'
                        ELSE 'SEDANG DIPROSES'
                    END AS Status_Proses
                FROM Emi_Material_Requisition a
                    INNER JOIN Emi_Material_Requisition_Det_Convert b ON b.Kode_Perusahaan = a.Kode_Perusahaan AND b.No_Faktur = a.No_Faktur
                    INNER JOIN Emi_Material_Requisition_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and c.Urut_Oto = b.No_Urut_Det
                    INNER JOIN Stock_Owner_Gudang d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and d.Flag_Cold_Storage = 'Y'
                    INNER JOIN Barang e ON e.Kode_Perusahaan  = b.Kode_Perusahaan AND e.Kode_Barang = b.Kode_Barang AND e.Kode_Stock_Owner = b.Kode_Stock_Owner
                    LEFT JOIN cte_tf f ON f.Kode_Perusahaan = b.Kode_Perusahaan AND f.Urut_Material_Requisition_Convert = b.Urut_Oto
                WHERE a.Kode_Perusahaan = @KodePerusahaan
                    AND a.No_Faktur_Order = @NoFakturOrder
                    AND a.Batch = @Batch
            "

			Cmd.Parameters.Clear()
			Cmd.Parameters.AddWithValue("KodePerusahaan", KodePerusahaan)
			Cmd.Parameters.AddWithValue("NoFakturOrder", NoFakturOrder)
			Cmd.Parameters.AddWithValue("Batch", Batch)

			Using Dr = OpenTrans(SQL)
				While Dr.Read()
					'Dim rowIndex As Integer = Dgv_Detail.Rows.Add(
					'    If(IsDBNull(Dr("No_Faktur")), "", Dr("No_Faktur")),
					'    If(IsDBNull(Dr("Kode_Barang")), "", Dr("Kode_Barang")),
					'    If(IsDBNull(Dr("Satuan")), "", Dr("Satuan")),
					'    If(IsDBNull(Dr("Qty_MR")), 0, Convert.ToDecimal(Dr("Qty_MR"))),
					'    If(IsDBNull(Dr("Qty_TF")), 0, Convert.ToDecimal(Dr("Qty_TF")))
					')

					Dim rowIndex As Integer = Dgv_Detail.Rows.Add(
						If(IsDBNull(Dr("No_Faktur")), "", Dr("No_Faktur")),
						If(IsDBNull(Dr("Kode_Barang")), "", Dr("Kode_Barang")),
						If(IsDBNull(Dr("Satuan")), "", Dr("Satuan")),
						If(General_Class.CekNULL(Dr("Qty_MR")) = "", 0, Format(Val(HilangkanTanda(Dr("Qty_MR"))), "N4")),
						If(General_Class.CekNULL(Dr("Qty_TF")) = "", 0, Format(Val(HilangkanTanda(Dr("Qty_TF"))), "N4"))
					)

					Dim status As String = If(IsDBNull(Dr("Status_Proses")), "", Dr("Status_Proses").ToString())

					Select Case status
						Case "SELESAI PROSES"
							Dgv_Detail.Rows(rowIndex).DefaultCellStyle.BackColor = Color.LightGreen

						Case "SEDANG DIPROSES"
							Dgv_Detail.Rows(rowIndex).DefaultCellStyle.BackColor = Color.LightYellow

						Case "BELUM DIPROSES"
							Dgv_Detail.Rows(rowIndex).DefaultCellStyle.BackColor = Color.LightCoral
					End Select
				End While

				Dr.Close()
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
		'Clear dgv dan lv
		Lv_RFID_Tags.Items.Clear()
		Dgv_Detail.Rows.Clear()

		'Reset pagination
		CurrentPage = 1
		PageSize = 19
		TotalData = 0
		TotalPage = 0

		FlagScanManual = False

		SelectedNoFaktur = ""

		Select Case TabControl1.SelectedIndex
			Case 0
				Fetch_Menunggu_Pairing_RFID()
				Btn_Simpan.Visible = True
				Btn_Update.Visible = False
				Btn_Refresh.Location = New Point(1134, 630)

				Btn_Simpan_Pairing.Visible = True
				Btn_Disconnect_RFID.Visible = True

			Case 1
				Fetch_Sudah_Pairing_RFID()
				Btn_Simpan.Visible = False
				Btn_Update.Visible = False
				Btn_Refresh.Location = New Point(1234, 630)

				Btn_Simpan_Pairing.Visible = False
				Btn_Disconnect_RFID.Visible = False
		End Select
	End Sub

	Private Sub Dgv_Menunggu_Pairing_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Menunggu_Pairing.CellContentClick
		If e.RowIndex < 0 Then Exit Sub

		If General_Class.CekNULL(Dgv_Menunggu_Pairing.CurrentRow.Cells(4).Value) = "" Then
			MessageBox.Show("No Batch Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Dim Batch As String = If(Dgv_Menunggu_Pairing.CurrentRow.Cells(4).Value.ToString() = "Packaging", "0", Dgv_Menunggu_Pairing.CurrentRow.Cells(4).Value.ToString())
		Dim NoFakturOrder As String = If(IsDBNull(Dgv_Menunggu_Pairing.Rows(e.RowIndex).Cells("MenungguPairingRFIDNoFakturOrder").Value), "", Dgv_Menunggu_Pairing.Rows(e.RowIndex).Cells("MenungguPairingRFIDNoFakturOrder").Value.ToString())

		If SelectedNoFaktur <> NoFakturOrder Or SelectedBatch <> Batch Then
			RFIDReader.ResetTagData()
		End If

		SelectedBatch = Batch
		SelectedNoFaktur = NoFakturOrder

		'==========================
		'=     HIDE SEMENTARA     =
		'==========================

#Region "DI HIDE 3 FEBRUARI 2026"

		FlagScanManual = Dgv_Menunggu_Pairing.Rows(e.RowIndex).Cells("MenungguPairingFlagScanManual").Value = "Y"

		If Dgv_Menunggu_Pairing.Columns(e.ColumnIndex).Name <> "BtnMenungguPairingRFID" Then Exit Sub

		RichTextBox1.Focus()

		' Kalau mode manual, konfirmasi dulu
		If FlagScanManual Then
			Dim result = MessageBox.Show(
				"No Transaksi ini diperbolehkan untuk scan RFID manual." & vbCrLf &
				"Apakah anda ingin melanjutkan scan manual?",
				"Metode Pairing",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question
			)

			' YES = manual → stop di sini
			If result = DialogResult.Yes Then Exit Sub

			' NO = scan otomatis → lanjut connect RFID
			FlagScanManual = False
		End If

		' Pastikan RFID siap
		If Not EnsureRFIDConnected() Then Exit Sub

#End Region

	End Sub

	Private Sub Dgv_Sudah_Pairing_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Sudah_Pairing.CellContentClick
		If e.RowIndex < 0 Then Exit Sub

		Dim row = Dgv_Sudah_Pairing.Rows(e.RowIndex)
		Dim NoFakturOrder As String = If(IsDBNull(row.Cells("SudahPairingRFIDNoFakturOrder").Value), "", row.Cells("SudahPairingRFIDNoFakturOrder").Value.ToString())
		SelectedNoFaktur = NoFakturOrder
		FlagScanManual = row.Cells("SudahPairingFlagScanManual").Value = "Y"

		If Dgv_Sudah_Pairing.Columns(e.ColumnIndex).Name <> "BtnPairingUlangRFID" Then Exit Sub

		RichTextBox1.Focus()

		' Kalau mode manual, konfirmasi dulu
		If FlagScanManual Then
			Dim result = MessageBox.Show(
				"No Transaksi ini diperbolehkan untuk scan RFID manual." & vbCrLf &
				"Apakah anda ingin melanjutkan scan manual?",
				"Metode Pairing",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question
			)

			' YES = manual → stop di sini
			If result = DialogResult.Yes Then
				Lv_RFID_Tags.Items.Clear()
				Exit Sub
			End If

			' NO = scan otomatis → lanjut connect RFID
			FlagScanManual = False
		End If

		' Pastikan RFID siap
		If EnsureRFIDConnected() Then
			Lv_RFID_Tags.Items.Clear()
			Exit Sub
		End If
	End Sub

	Private Function EnsureRFIDConnected() As Boolean
		If RFIDReader.IsConnected Then Return True

		If Not PingHost(RFIDReaderIP) Then
			MessageBox.Show(
			"Reader RFID tidak terdeteksi di jaringan.",
			"Peringatan",
			MessageBoxButtons.OK,
			MessageBoxIcon.Warning
		)
			Return False
		End If

		If Not RFIDReader.Connect(500, RFIDReaderPower) Then
			MessageBox.Show(
			"Gagal terhubung ke reader RFID.",
			"Peringatan",
			MessageBoxButtons.OK,
			MessageBoxIcon.Warning
		)
			Return False
		End If

		Return True
	End Function

	Function PingHost(ip As String) As Boolean
		Try
			Dim ping As New Net.NetworkInformation.Ping()
			Dim reply = ping.Send(ip, 1000)
			Return reply.Status = Net.NetworkInformation.IPStatus.Success
		Catch
			Return False
		End Try
	End Function

	Private Sub Btn_Disconnect_RFID_Click(sender As Object, e As EventArgs) Handles Btn_Disconnect_RFID.Click
		If RFIDReader.IsConnected Then
			RFIDReader.Disconnect()
		End If
	End Sub

	Private Sub N_EMI_Pairing_RFID_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
		If RFIDReader.IsConnected Then
			RFIDReader.Disconnect()
		End If
	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		Lv_RFID_Tags.Items.Clear()
		Dgv_Detail.Rows.Clear()
		TabControl1.SelectedIndex = 0

		Btn_Disconnect_RFID.PerformClick()
		Fetch_Menunggu_Pairing_RFID()
	End Sub

	Private Sub Btn_Simpan_Pairing_Click(sender As Object, e As EventArgs) Handles Btn_Simpan_Pairing.Click
		If Lv_RFID_Tags.Items.Count = 0 Then
			MessageBox.Show("Tidak ada data RFID Tag untuk disimpan.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End If

		If RFIDReader.IsConnected Then
			RFIDReader.Disconnect()
		End If

		If SelectedBatch = "" Or String.IsNullOrWhiteSpace(SelectedBatch) Then
			MessageBox.Show("Batch Tidak Ditemukan Harap ulangi pairing", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End If

		Btn_Disconnect_RFID.PerformClick()

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'Store pairing rfid tags
			Dim NoFakturOrder As String = SelectedNoFaktur
			Dim NoBatchh As String = SelectedBatch

			If Lv_RFID_Tags.Items.Count = 0 Then
				CloseTrans()
				CloseConn()

				MessageBox.Show(
					"Minimal harus ada 1 tag RFID untuk di pairing!",
					Judul,
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation
				)
				Exit Sub
			End If

			' =========================
			' 3. Insert & Update RFID
			' =========================
			For i As Integer = 0 To Lv_RFID_Tags.Items.Count - 1

				Dim rfid_tag As String = Lv_RFID_Tags.Items(i).SubItems(0).Text

				SQL = "select 1 from N_EMI_Pairing_RFID "
				SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
				SQL &= $"and No_Split_Production_Order = '{SelectedNoFaktur}' "
				SQL &= $"and Batch = '{SelectedBatch}' "
				SQL &= $"and RFID_Tag = '{rfid_tag}' "
				SQL &= $"and Lokasi_Pairing = 'COLD_STORAGE' "
				Using Dr = OpenTrans(SQL)
					If Not Dr.Read Then

						Dr.Close()

						'===========================================================
						'=     CEK APAKAH RFID SUDAH DIGUNAKNA PADA BATCH LAIN     =
						'===========================================================
						'SQL = "select No_Production_Order, Batch from N_EMI_Master_Data_RFID_Tags "
						'SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
						'SQL &= $"and RFID_Tag = '{rfid_tag}' "
						'Using Dr2 = OpenTrans(SQL)
						'    If Dr2.Read Then
						'        If General_Class.CekNULL(Dr2("No_Production_Order")) <> "" Then
						'            Dr2.Close()
						'            CloseTrans()
						'            CloseConn()
						'            MessageBox.Show($"RDIF Tag {rfid_tag} tidak dapat digunakan karena sudah dipakai untuk batch yang lain", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'            Exit Sub
						'        End If

						'    Else
						'        Dr2.Close()
						'        CloseTrans()
						'        CloseConn()
						'        MessageBox.Show($"RDIF Tag {rfid_tag} tidak ditemukan pada data master", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'        Exit Sub
						'    End If
						'End Using

						' Pindahkan ke log data tag ini yang dipakai di No Split lain
						SQL = "
                            INSERT INTO N_EMI_Pairing_RFID_Log
                            (
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
                                Flag_Pairing_Ulang,
                                Flag_Scan_Manual,
                                batch
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
                                NULL,
                                Flag_Scan_Manual,
                                batch
                            FROM N_EMI_Pairing_RFID
                            WHERE Kode_Perusahaan = @KodePerusahaan
                              AND RFID_Tag = @RFID_Tag
                              AND Lokasi_Pairing = 'COLD_STORAGE'
                              AND Lokasi_IN IS NULL
                              AND Tanggal_IN IS NULL
                              AND Jam_IN IS NULL
                              AND NOT (
                                    No_Split_Production_Order = @NoFaktur
                                    AND Batch = @Batch
                              )
                        "
						Cmd.Parameters.Clear()
						Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
						Cmd.Parameters.AddWithValue("@NoFaktur", NoFakturOrder)
						Cmd.Parameters.AddWithValue("@Batch", NoBatchh)
						Cmd.Parameters.AddWithValue("@RFID_Tag", rfid_tag)
						ExecuteTrans(SQL)

						SQL = "
                        DELETE N_EMI_Pairing_RFID
                        WHERE Kode_Perusahaan = @KodePerusahaan
                              AND No_Split_Production_Order <> @NoFaktur
                              AND Batch <> @Batch
                              AND RFID_Tag = @RFID_Tag
                              AND Lokasi_Pairing = 'COLD_STORAGE'
                              AND Lokasi_IN IS NULL
                              AND Tanggal_IN IS NULL
                              AND Jam_IN IS NULL;
                        "
						Cmd.Parameters.Clear()
						Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
						Cmd.Parameters.AddWithValue("@NoFaktur", NoFakturOrder)
						Cmd.Parameters.AddWithValue("@Batch", NoBatchh)
						Cmd.Parameters.AddWithValue("@RFID_Tag", rfid_tag)
						ExecuteTrans(SQL)

						' --- INSERT pairing ---
						SQL = "
                            INSERT INTO N_EMI_Pairing_RFID
                            (Kode_Perusahaan, No_Split_Production_Order, Kode_Stock_Owner, RFID_Tag,
                             Tanggal_Pairing, Jam_Pairing, UserID_Pairing, Lokasi_Pairing, batch)
                            VALUES
                            (@KodePerusahaan, @NoFaktur, @KodeStockOwner, @RFID_Tag,
                             @TanggalPairing, @JamPairing, @UserIDPairing, @LokasiPairing, @Batchh)
                        "
						Cmd.Parameters.Clear()
						Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
						Cmd.Parameters.AddWithValue("@NoFaktur", NoFakturOrder)
						Cmd.Parameters.AddWithValue("@KodeStockOwner", Lokasi)
						Cmd.Parameters.AddWithValue("@LokasiPairing", "COLD_STORAGE")
						Cmd.Parameters.AddWithValue("@RFID_Tag", rfid_tag)
						Cmd.Parameters.AddWithValue("@TanggalPairing", Format(tgl_skg, "yyyy-MM-dd"))
						Cmd.Parameters.AddWithValue("@JamPairing", Format(tgl_skg, "HH:mm:ss"))
						Cmd.Parameters.AddWithValue("@UserIDPairing", UserID)
						Cmd.Parameters.AddWithValue("@Batchh", NoBatchh)

						ExecuteTrans(SQL)

						' --- UPDATE status tag ---
						SQL = "UPDATE N_EMI_Master_Data_RFID_Tags
                                SET No_Production_Order = @NoSplit, Batch = @NoBatch
                                WHERE RFID_Tag = @RFID_Tag
                        "

						Cmd.Parameters.Clear()
						Cmd.Parameters.AddWithValue("@RFID_Tag", rfid_tag)
						Cmd.Parameters.AddWithValue("@NoSplit", NoFakturOrder)
						Cmd.Parameters.AddWithValue("@NoBatch", NoBatchh)
						ExecuteTrans(SQL)
					End If
				End Using

			Next

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Berhasil Melakukan Set RFID", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Fetch_Menunggu_Pairing_RFID()
		Btn_Simpan.Visible = True
		Btn_Update.Visible = False
		Btn_Refresh.Location = New Point(1134, 630)

	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
		If Lv_RFID_Tags.Items.Count = 0 Then
			MessageBox.Show("Tidak ada data RFID Tag untuk disimpan.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End If

		If RFIDReader.IsConnected Then
			RFIDReader.Disconnect()
		End If

		If SelectedBatch = "" Or String.IsNullOrWhiteSpace(SelectedBatch) Then
			MessageBox.Show("Batch Tidak Ditemukan Harap ulangi pairing", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End If

		Btn_Disconnect_RFID.PerformClick()

		'==========================
		'=     HIDE SEMENTARA     =
		'==========================

#Region "Di Hide 3 FEBRUARI 2026"

		'Try
		'    OpenConn()
		'    Cmd.Transaction = Cn.BeginTransaction

		'    Dim noFaktur As String = SelectedNoFaktur
		'    If String.IsNullOrEmpty(noFaktur) Then
		'        MessageBox.Show("Silakan pilih No Transaksi terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
		'        Exit Sub
		'    End If

		'    For Each tag As ListViewItem In Lv_RFID_Tags.Items
		'        Dim rfid_tag As String = tag.Text
		'        SQL = "
		'            INSERT INTO N_EMI_Pairing_RFID
		'            (Kode_Perusahaan, No_Split_Production_Order, Kode_Stock_Owner, RFID_Tag,
		'             Tanggal_Pairing, Jam_Pairing, UserID_Pairing, Lokasi_Pairing, Flag_Scan_Manual)
		'            VALUES
		'            (@KodePerusahaan, @NoFaktur, @KodeStockOwner, @RFID_Tag,
		'             @TanggalPairing, @JamPairing, @UserIDPairing, @LokasiPairing, @FlagScanManual)
		'        "

		'        Cmd.Parameters.Clear()
		'        Cmd.Parameters.AddWithValue("KodePerusahaan", KodePerusahaan)
		'        Cmd.Parameters.AddWithValue("NoFaktur", noFaktur)
		'        Cmd.Parameters.AddWithValue("KodeStockOwner", Lokasi)
		'        Cmd.Parameters.AddWithValue("LokasiPairing", Lokasi)
		'        Cmd.Parameters.AddWithValue("RFID_Tag", rfid_tag)
		'        Cmd.Parameters.AddWithValue("TanggalPairing", Date.Now)
		'        Cmd.Parameters.AddWithValue("JamPairing", Date.Now.ToString("HH:mm:ss"))
		'        Cmd.Parameters.AddWithValue("UserIDPairing", UserID)
		'        Cmd.Parameters.AddWithValue(
		'            "@FlagScanManual",
		'            If(FlagScanManual, "Y", CType(DBNull.Value, Object))
		'        )

		'        ExecuteTrans(SQL)

		'        SQL = "UPDATE N_EMI_Master_Data_RFID_Tags SET Status = 'Y' WHERE RFID_Tag = @RFID_Tag"
		'        Cmd.Parameters.Clear()
		'        Cmd.Parameters.AddWithValue("RFID_Tag", rfid_tag)

		'        ExecuteTrans(SQL)
		'    Next

		'    Cmd.Transaction.Commit()
		'    CloseTrans()
		'    CloseConn()

		'    Lv_RFID_Tags.Items.Clear()
		'    Dgv_Detail.Rows.Clear()

		'    TabControl1.SelectedIndex = 0
		'    Fetch_Menunggu_Pairing_RFID()

		'    MessageBox.Show("Data pairing RFID Tag berhasil disimpan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
		'Catch ex As Exception
		'    If Cmd.Transaction IsNot Nothing Then
		'        Cmd.Transaction.Rollback()
		'    End If
		'    CloseTrans()
		'    CloseConn()
		'    MessageBox.Show("Gagal menyimpan data binding: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		'End Try

#End Region

		'Dim NoFakturOrder As String = Dgv_Menunggu_Pairing.CurrentRow.Cells(0).Value
		'Dim SelectedBatch As String = If(Dgv_Menunggu_Pairing.CurrentRow.Cells(4).Value = "Packaging", 0, Dgv_Menunggu_Pairing.CurrentRow.Cells(4).Value)

		Try
			OpenConn()

			'========================================
			'=     CEK APAKAH ADA TAG MINIMAL 1     =
			'========================================
			SQL = $"select 1 from N_EMI_Pairing_RFID where No_Split_Production_Order = '{SelectedNoFaktur}' and Batch = '{SelectedBatch}' "
			SQL &= "union all "
			SQL &= $"select 1 from N_EMI_Pairing_RFID_log where No_Split_Production_Order = '{SelectedNoFaktur}' and Batch = '{SelectedBatch}' and lokasi_pairing='COLD_STORAGE' "
			Using Dr = OpenTrans(SQL)
				If Not Dr.Read Then
					Dr.Close()
					CloseConn()
					MessageBox.Show($"Tidak Bisa Simpan Karena Tidak Ada RFID", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'=================================================================
			'=     CEK APAKAH DATA LISTVIEW BERBEDA DENGAN DI DATABSE        =
			'=================================================================
			SQL = "; with cte as( select distinct rfid_tag as Total_Data from N_EMI_Pairing_RFID "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and No_Split_Production_Order = '{SelectedNoFaktur}' "
			SQL &= $"and Batch = '{SelectedBatch}' and lokasi_pairing='COLD_STORAGE' "
			SQL &= "union all "
			SQL &= "select distinct rfid_tag as Total_Data from N_EMI_Pairing_RFID_log "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and No_Split_Production_Order = '{SelectedNoFaktur}' "
			SQL &= $"and Batch = '{SelectedBatch}' and lokasi_pairing='COLD_STORAGE') select count(total_data) as total_data from (select distinct total_data from cte ) data "

			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If Val(HilangkanTanda(Dr("Total_Data"))) <> Lv_RFID_Tags.Items.Count Then
						Dr.Close()
						CloseConn()
						MessageBox.Show("Harap Lakukan Simmpan Pairing Terlebih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		SuccessPotongStock = True
		Proses_Update_Stock_Dan_Jurnal(SelectedNoFaktur, SelectedBatch)

		If Not SuccessPotongStock Then
			Exit Sub
		End If

#Region "Komen "

		'Try
		'    OpenConn()
		'    Cmd.Transaction = Cn.BeginTransaction

		'    '======================
		'    '=     RESET RFID     =
		'    '======================
		'    For i As Integer = 0 To Lv_RFID_Tags.Items.Count - 1
		'        Dim RFIDTAG As String = Lv_RFID_Tags.Items(i).SubItems(0).Text

		'        SQL = "select 1 from N_EMI_Master_Data_RFID_Tags "
		'        SQL &= $"where RFID_Tag = '{RFIDTAG}' "
		'        Using Dr = OpenTrans(SQL)
		'            If Dr.Read Then

		'                Dr.Close()
		'                SQL = "update N_EMI_Master_Data_RFID_Tags set No_Production_Order = NULL, Batch = NULL "
		'                SQL &= $"where RFID_Tag = '{RFIDTAG}' "
		'                ExecuteTrans(SQL)

		'            Else
		'                Dr.Close()
		'                CloseTrans()
		'                CloseConn()
		'                MessageBox.Show($"RFID Tag {RFIDTAG} Tidak Ditemukan Pada Database", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                Exit Sub
		'            End If
		'        End Using

		'    Next

		'    Cmd.Transaction.Commit()
		'    CloseTrans()
		'    CloseConn()
		'Catch ex As Exception
		'    CloseTrans()
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try

#End Region

		Fetch_Menunggu_Pairing_RFID()
		Btn_Simpan.Visible = True
		Btn_Update.Visible = False
		Btn_Refresh.Location = New Point(1134, 630)

	End Sub

	Private Sub Dgv_Menunggu_Pairing_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Menunggu_Pairing.CellClick
		If e.RowIndex < 0 Then Exit Sub

		'Dim NoFakturOrder As String = If(IsDBNull(Dgv_Menunggu_Pairing.Rows(e.RowIndex).Cells("MenungguPairingRFIDNoFakturOrder").Value), "", Dgv_Menunggu_Pairing.Rows(e.RowIndex).Cells("MenungguPairingRFIDNoFakturOrder").Value.ToString())
		If General_Class.CekNULL(Dgv_Menunggu_Pairing.CurrentRow.Cells(4).Value) = "" Then
			MessageBox.Show("No Batch Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Dim NoFakturOrder As String = If(IsDBNull(Dgv_Menunggu_Pairing.Rows(e.RowIndex).Cells("MenungguPairingRFIDNoFakturOrder").Value), "", Dgv_Menunggu_Pairing.Rows(e.RowIndex).Cells("MenungguPairingRFIDNoFakturOrder").Value.ToString())
		Dim Batch As String = If(Dgv_Menunggu_Pairing.CurrentRow.Cells(4).Value.ToString = "Packaging", 0, Dgv_Menunggu_Pairing.CurrentRow.Cells(4).Value.ToString)

		If SelectedNoFaktur <> NoFakturOrder Or SelectedBatch <> Batch Then
			RFIDReader.ResetTagData()
		End If

		SelectedNoFaktur = NoFakturOrder
		SelectedBatch = Batch

		Fetch_Dgv_Detail(NoFakturOrder, Batch)
		Fetch_RFID_Pair(NoFakturOrder, Batch)
	End Sub

	Private Sub Btn_Cari_Click(sender As Object, e As EventArgs)
		Select Case TabControl1.SelectedIndex
			Case 0
				Fetch_Menunggu_Pairing_RFID()
			Case 1
				Fetch_Sudah_Pairing_RFID()
		End Select
	End Sub

	Private Sub Dgv_Sudah_Pairing_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Sudah_Pairing.CellClick
		If e.RowIndex < 0 Then Exit Sub

		Dim NoFakturOrder As String = If(IsDBNull(Dgv_Sudah_Pairing.Rows(e.RowIndex).Cells("SudahPairingRFIDNoFakturOrder").Value), "", Dgv_Sudah_Pairing.Rows(e.RowIndex).Cells("SudahPairingRFIDNoFakturOrder").Value.ToString())

		If General_Class.CekNULL(Dgv_Sudah_Pairing.CurrentRow.Cells(4).Value) = "" Then
			MessageBox.Show("No Batch Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Dim Batch As String = If(Dgv_Sudah_Pairing.CurrentRow.Cells(4).Value.ToString = "Packaging", 0, Dgv_Sudah_Pairing.CurrentRow.Cells(4).Value.ToString)

		Fetch_Dgv_Detail(NoFakturOrder, Batch)
		Fetch_RFID_Pair(NoFakturOrder, Batch)
	End Sub

	Private Sub Fetch_RFID_Pair(NoFakturOrder, Batch)
		Try
			OpenConn()
			Lv_RFID_Tags.Items.Clear()

			'SQL = $"
			'    SELECT Distinct a.RFID_Tag, b.RFID_Label
			'    FROM N_EMI_Pairing_RFID a
			'        inner join N_EMI_Master_Data_RFID_Tags b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.RFID_Tag = b.RFID_Tag
			'    WHERE a.No_Split_Production_Order = @NoFakturOrder and a.batch = @Batch
			'"

			SQL = $"
                ;with Cte as (
	                select Kode_Perusahaan, no_split_production_order, batch, RFID_Tag from N_EMI_Pairing_RFID z where lokasi_pairing='COLD_STORAGE'
	                union all
	                select Kode_Perusahaan, no_split_production_order, batch, RFID_Tag from N_EMI_Pairing_RFID_Log z where lokasi_pairing='COLD_STORAGE'
                )
                select Distinct a.RFID_Tag, a.RFID_Label
                from N_EMI_Master_Data_RFID_Tags a
	                inner join Cte b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.RFID_Tag = b.RFID_Tag
                WHERE b.No_Split_Production_Order = @NoFakturOrder and b.batch = @Batch
            "

			'SQL = $"
			'    ;with Cte as (
			'     select Kode_Perusahaan, no_split_production_order, batch, RFID_Tag from N_EMI_Pairing_RFID z
			'    )
			'    select Distinct a.RFID_Tag, a.RFID_Label
			'    from N_EMI_Master_Data_RFID_Tags a
			'     inner join Cte b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.RFID_Tag = b.RFID_Tag
			'    WHERE b.No_Split_Production_Order = @NoFakturOrder and b.batch = @Batch
			'"

			Cmd.Parameters.Clear()
			Cmd.Parameters.AddWithValue("NoFakturOrder", NoFakturOrder)
			Cmd.Parameters.AddWithValue("Batch", Batch)

			Using Dr = OpenTrans(SQL)
				While Dr.Read()
					Dim rfidTag As String = ""
					If Dr("RFID_Tag") IsNot DBNull.Value Then
						rfidTag = Dr("RFID_Tag").ToString()
					End If

					Dim Lv As ListViewItem
					Lv = Lv_RFID_Tags.Items.Add(rfidTag)
					Lv.SubItems.Add(Dr("RFID_Label"))

					'Dim item As New ListViewItem(rfidTag)
					'Lv_RFID_Tags.Items.Add(item)
				End While

				Dr.Close()
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Private Sub Btn_Update_Click(sender As Object, e As EventArgs) Handles Btn_Update.Click
		If Lv_RFID_Tags.Items.Count = 0 Then
			MessageBox.Show("Tidak ada data RFID Tag untuk disimpan.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End If

		If RFIDReader.IsConnected Then
			RFIDReader.Disconnect()
		End If

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim noFaktur As String = SelectedNoFaktur
			If String.IsNullOrEmpty(noFaktur) Then
				MessageBox.Show("Silakan pilih No Transaksi terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End If

			Dim urutPairingBaru As Integer = 1

			SQL = "
                SELECT ISNULL(MAX(Urut_Pairing), 0) + 1 AS UrutPairingBaru
                FROM N_EMI_Pairing_RFID
                WHERE Kode_Perusahaan = @KodePerusahaan
                  AND No_Split_Production_Order = @NoFaktur
            "

			Cmd.Parameters.Clear()
			Cmd.Parameters.AddWithValue("KodePerusahaan", KodePerusahaan)
			Cmd.Parameters.AddWithValue("NoFaktur", noFaktur)
			Using Dr = OpenTrans(SQL)
				If Dr.Read() Then
					urutPairingBaru = Convert.ToInt32(Dr("UrutPairingBaru"))
				End If
				Dr.Close()
			End Using

			SQL = "
                INSERT INTO N_EMI_Pairing_RFID_Log
                (Kode_Perusahaan, No_Split_Production_Order, Kode_Stock_Owner, RFID_Tag,
                 Tanggal_Pairing, Jam_Pairing, UserID_Pairing, Flag_Pairing_Ulang, Urut_Pairing, Lokasi_Pairing)
                SELECT
                    Kode_Perusahaan, No_Split_Production_Order, Kode_Stock_Owner, RFID_Tag,
                    Tanggal_Pairing, Jam_Pairing, UserID_Pairing, 'Y', Urut_Pairing, Lokasi_Pairing
                FROM N_EMI_Pairing_RFID
                WHERE Kode_Perusahaan = @KodePerusahaan
                  AND No_Split_Production_Order = @NoFaktur
            "

			Cmd.Parameters.Clear()
			Cmd.Parameters.AddWithValue("KodePerusahaan", KodePerusahaan)
			Cmd.Parameters.AddWithValue("NoFaktur", noFaktur)
			ExecuteTrans(SQL)

			SQL = "SELECT RFID_Tag FROM N_EMI_Pairing_RFID WHERE Kode_Perusahaan = @KodePerusahaan AND No_Split_Production_Order = @NoFaktur"
			Cmd.Parameters.Clear()
			Cmd.Parameters.AddWithValue("KodePerusahaan", KodePerusahaan)
			Cmd.Parameters.AddWithValue("NoFaktur", noFaktur)

			Using Dr = OpenTrans(SQL)
				While Dr.Read()
					Dim rfidTag As String = Dr("RFID_Tag").ToString()
					SQL = "UPDATE N_EMI_Master_Data_RFID_Tags SET Status = NULL WHERE RFID_Tag = @RFIDTag"
					Cmd.Parameters.Clear()
					Cmd.Parameters.AddWithValue("RFIDTag", rfidTag)
					ExecuteTrans(SQL)
				End While
				Dr.Close()
			End Using

			SQL = "
                DELETE FROM N_EMI_Pairing_RFID
                WHERE Kode_Perusahaan = @KodePerusahaan
                  AND No_Split_Production_Order = @NoFaktur
            "

			Cmd.Parameters.Clear()
			Cmd.Parameters.AddWithValue("KodePerusahaan", KodePerusahaan)
			Cmd.Parameters.AddWithValue("NoFaktur", noFaktur)
			ExecuteTrans(SQL)

			For Each tag As ListViewItem In Lv_RFID_Tags.Items
				SQL = "
                    INSERT INTO N_EMI_Pairing_RFID
                    (Kode_Perusahaan, No_Split_Production_Order, Kode_Stock_Owner,
                     RFID_Tag, Urut_Pairing, Tanggal_Pairing, Jam_Pairing, UserID_Pairing, Lokasi_Pairing)
                    VALUES
                    (@KodePerusahaan, @NoFaktur, @KodeStockOwner,
                     @RFID_Tag, @UrutPairing, @TanggalPairing, @JamPairing, @UserIDPairing, @LokasiPairing)
                "

				Cmd.Parameters.Clear()
				Cmd.Parameters.AddWithValue("KodePerusahaan", KodePerusahaan)
				Cmd.Parameters.AddWithValue("NoFaktur", noFaktur)
				Cmd.Parameters.AddWithValue("KodeStockOwner", Lokasi)
				Cmd.Parameters.AddWithValue("RFID_Tag", tag.Text)
				Cmd.Parameters.AddWithValue("UrutPairing", urutPairingBaru)
				Cmd.Parameters.AddWithValue("TanggalPairing", Format(tgl_skg, "yyyy-MM-dd"))
				Cmd.Parameters.AddWithValue("JamPairing", Format(tgl_skg, "HH:mm:ss"))
				Cmd.Parameters.AddWithValue("UserIDPairing", UserID)

				Cmd.Parameters.AddWithValue("LokasiPairing", Lokasi)

				ExecuteTrans(SQL)

				SQL = "UPDATE N_EMI_Master_Data_RFID_Tags SET Status = 'Y' WHERE RFID_Tag = @RFIDTag"
				Cmd.Parameters.Clear()
				Cmd.Parameters.AddWithValue("RFID_Tag", tag.Text)

				ExecuteTrans(SQL)
			Next

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()

			FlagScanManual = False

			MessageBox.Show("Pairing ulang RFID Tag berhasil disimpan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			If Cmd.Transaction IsNot Nothing Then
				Cmd.Transaction.Rollback()
			End If
			CloseTrans()
			CloseConn()
			MessageBox.Show("Gagal menyimpan pairing ulang: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Private Sub Btn_Prev_Click(sender As Object, e As EventArgs) Handles Btn_Prev.Click
		If CurrentPage > 1 Then
			CurrentPage -= 1
			Fetch_Menunggu_Pairing_RFID()
		End If
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Btn_Next.Click
		CurrentPage += 1
		Fetch_Menunggu_Pairing_RFID()
	End Sub

	Private Sub Proses_Update_Stock_Dan_Jurnal(ByVal NoSplit As String, ByVal Batch As String)

		Dim KdUnikPrintCetak As New ArrayList

		get_jam()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'=======================================================
			'=     CEK APAKAH DATA SUDAH PERNAH DI POTONG STOCK     =
			'=======================================================
			SQL = "SELECT a.No_Transaksi AS No_Split, b.No_Faktur AS Faktur_RM, d.kode_barang, "
			SQL &= $"CASE WHEN EXISTS ( SELECT 1 FROM N_EMI_Transaksi_Transfer_Stock_Sementara z "
			SQL &= $"INNER JOIN N_EMI_Transaksi_Transfer_Stock_Sementara_Detail x ON z.Kode_Perusahaan = x.Kode_Perusahaan AND z.No_Faktur = x.No_Faktur "
			SQL &= $"INNER JOIN N_EMI_Transaksi_Transfer_Stock_Sementara_Det y ON x.Kode_Perusahaan = y.Kode_Perusahaan AND x.No_Faktur = y.No_Faktur AND x.Urut_Oto = y.Urut_TF AND y.Flag_Validasi = 'Y' "
			SQL &= $"INNER JOIN N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 k ON y.Kode_Perusahaan = k.Kode_Perusahaan AND y.No_Faktur = k.No_Faktur AND y.Urut_Oto = k.Urut_Det "
			SQL &= $"WHERE z.Status IS NULL AND x.Urut_Material_Requisition_Convert = d.Urut_Oto ) THEN 'Y' ELSE 'T' END AS Has_Validasi "
			SQL &= $"FROM Emi_Split_Production_Order a "
			SQL &= $"INNER JOIN Emi_Material_Requisition b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Transaksi = b.No_Faktur_Order AND b.Status IS NULL "
			SQL &= $"INNER JOIN Emi_Material_Requisition_Det c ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.No_Faktur = c.No_Faktur "
			SQL &= $"INNER JOIN Emi_Material_Requisition_Det_Convert d ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.No_Faktur = d.No_Faktur AND c.Urut_Oto = d.No_Urut_Det "
			SQL &= $"INNER JOIN Stock_Owner_Gudang e on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Kode_Stock_Owner_Tujuan = e.Kode_Stock_Owner and e.Flag_Cold_Storage = 'Y' "
			SQL &= $"WHERE a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"AND a.No_Transaksi = '{NoSplit}' "
			SQL &= $"AND b.Batch = '{Batch}' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					If Dr("Has_Validasi") = "Y" Then
						Dim KdBarang As String = Dr("Kode_Barang")
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Kode Barang {KdBarang} Sudah Pernah Divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						SuccessPotongStock = False
						Exit Sub
					End If
				Loop
			End Using

			'=========================================
			'=     CEK APAKAH RM SUDAH TERPENUHI     =
			'=========================================
			'SQL = "WITH TF_Stock_AGG AS ("
			'SQL &= $"SELECT z.Kode_Perusahaan, x.Urut_Material_Requisition_Convert, z.SO_Tujuan, x.Kode_Barang, SUM(k.Jumlah) AS Total_Jumlah "
			'SQL &= $"FROM N_EMI_Transaksi_Transfer_Stock_Sementara z "
			'SQL &= $"INNER JOIN N_EMI_Transaksi_Transfer_Stock_Sementara_Detail x ON z.Kode_Perusahaan = x.Kode_Perusahaan AND z.No_Faktur = x.No_Faktur "
			'SQL &= $"INNER JOIN N_EMI_Transaksi_Transfer_Stock_Sementara_Det y ON x.Kode_Perusahaan = y.Kode_Perusahaan AND x.No_Faktur = y.No_Faktur AND x.Urut_Oto = y.Urut_TF "
			'SQL &= $"INNER JOIN N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 k ON y.Kode_Perusahaan = k.Kode_Perusahaan AND y.No_Faktur = k.No_Faktur AND y.Urut_Oto = k.Urut_Det "
			'SQL &= $"WHERE z.Status IS NULL "
			'SQL &= $"GROUP BY z.Kode_Perusahaan, x.Urut_Material_Requisition_Convert, z.SO_Tujuan, x.Kode_Barang "
			'SQL &= $") "
			'SQL &= $"SELECT a.No_Transaksi AS No_Split, b.No_Faktur AS Faktur_RM, b.Batch, d.Kode_Stock_Owner, d.Kode_Barang, d.Jumlah, d.Satuan, "
			'SQL &= $"ISNULL(ts.Total_Jumlah, 0) AS TotalTF "
			'SQL &= $"FROM Emi_Split_Production_Order a "
			'SQL &= $"INNER JOIN Emi_Material_Requisition b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Transaksi = b.No_Faktur_Order AND b.Status IS NULL "
			'SQL &= $"INNER JOIN Emi_Material_Requisition_Det c ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.No_Faktur = c.No_Faktur "
			'SQL &= $"INNER JOIN Emi_Material_Requisition_Det_Convert d ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.No_Faktur = d.No_Faktur AND c.Urut_Oto = d.No_Urut_Det "
			'SQL &= $"LEFT JOIN TF_Stock_AGG ts ON ts.Kode_Perusahaan = b.Kode_Perusahaan AND ts.Urut_Material_Requisition_Convert = d.Urut_Oto AND ts.SO_Tujuan = d.Kode_Stock_Owner AND ts.Kode_Barang = d.Kode_Barang "
			'SQL &= $"WHERE a.Kode_Perusahaan = '{KodePerusahaan}' "
			'SQL &= $"AND a.No_Transaksi = '{NoSplit}' "
			'SQL &= $"AND b.Batch = '{Batch}' "

			SQL = ";WITH TF_Stock_AGG AS ( "
			SQL &= $"SELECT x.Kode_Perusahaan, x.Urut_Material_Requisition_Convert, SUM(x.Jumlah) AS Total_Jumlah "
			SQL &= $"FROM ( "
			SQL &= $"SELECT a.Kode_Perusahaan, b.Urut_Material_Requisition_Convert, d.Jumlah "
			SQL &= $"FROM N_EMI_Transaksi_Transfer_Stock_Sementara a "
			SQL &= $"JOIN N_EMI_Transaksi_Transfer_Stock_Sementara_Detail b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Faktur = b.No_Faktur "
			SQL &= $"JOIN N_EMI_Transaksi_Transfer_Stock_Sementara_Det c ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.No_Faktur = c.No_Faktur AND b.Urut_Oto  = c.Urut_TF "
			SQL &= $"JOIN N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 d ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.No_Faktur = d.No_Faktur AND c.Urut_Oto = d.Urut_Det "
			SQL &= $"WHERE a.Status IS NULL "
			SQL &= $") x "
			SQL &= $"GROUP BY x.Kode_Perusahaan, x.Urut_Material_Requisition_Convert) "
			SQL &= $"SELECT a.No_Transaksi AS No_Split, b.No_Faktur AS Faktur_RM, b.Batch, d.Kode_Stock_Owner, d.Kode_Barang, d.Jumlah, d.Satuan, "
			SQL &= $"ISNULL(ts.Total_Jumlah, 0) AS TotalTF, f.Toleransi_Tf_Min, f.Toleransi_Tf_Max "
			SQL &= $"FROM Emi_Split_Production_Order a "
			SQL &= $"INNER JOIN Emi_Material_Requisition b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Transaksi = b.No_Faktur_Order AND b.Status IS NULL "
			SQL &= $"INNER JOIN Emi_Material_Requisition_Det c ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.No_Faktur = c.No_Faktur "
			SQL &= $"INNER JOIN Emi_Material_Requisition_Det_Convert d ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.No_Faktur = d.No_Faktur AND c.Urut_Oto = d.No_Urut_Det "
			SQL &= $"INNER JOIN Stock_Owner_Gudang e on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Kode_Stock_Owner_Tujuan = e.Kode_Stock_Owner and e.Flag_Cold_Storage = 'Y' "
			SQL &= $"inner join barang f on d.Kode_Perusahaan = f.Kode_Perusahaan and d.Kode_Stock_Owner = f.Kode_Stock_Owner and d.Kode_Barang = f.Kode_Barang "
			SQL &= $"LEFT JOIN TF_Stock_AGG ts ON ts.Kode_Perusahaan = b.Kode_Perusahaan AND ts.Urut_Material_Requisition_Convert = d.Urut_Oto "
			SQL &= $"WHERE a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"AND a.No_Transaksi = '{NoSplit}' "
			SQL &= $"AND b.Batch = '{Batch}' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					'===============================
					'=     GET NILAI TOLERANSI     =
					'===============================

					Dim Jumlah As Decimal = Val(HilangkanTanda(Dr("Jumlah")))
					Dim TotalTF As Decimal = Val(HilangkanTanda(Dr("TotalTF")))

					Dim Persen_Toleransi_Min As Decimal = Val(Dr("Toleransi_Tf_Min"))
					Dim Persen_Toleransi_Max As Decimal = Val(Dr("Toleransi_Tf_Max"))

					Dim Nilai_Toleransi_Min As Decimal = Jumlah - (Jumlah * Persen_Toleransi_Min / 100D)
					Dim Nilai_Toleransi_Max As Decimal = Jumlah + (Jumlah * Persen_Toleransi_Max / 100D)

					Dim KdBarang As String = Dr("Kode_Barang")
					'Cek toleransi
					If TotalTF < Nilai_Toleransi_Min Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Kode Barang {KdBarang} Belum Memenuhi Toleransi Minimum", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						SuccessPotongStock = False
						Exit Sub
					ElseIf TotalTF > Nilai_Toleransi_Max Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Kode Barang {KdBarang} Melampaui Toleransi Maksimum", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						SuccessPotongStock = False
						Exit Sub

					End If

					'Jika masuk toleransi → cek pemenuhan jumlah
					If TotalTF < Nilai_Toleransi_Min Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Kode Barang {KdBarang} Belum Terpenuhi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						SuccessPotongStock = False
						Exit Sub
					ElseIf TotalTF > Nilai_Toleransi_Max Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Kode Barang {KdBarang} Melebihi Jumlah Kebutuhan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						SuccessPotongStock = False
						Exit Sub
					End If

				Loop
			End Using

			Dim tglDuaHariSebelum As DateTime = tgl_skg.AddDays(-2)

			SQL = "delete from N_EMI_Cetak_Transfer_Stock_Sementara where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			SQL = SQL & "Tanggal_Cetak between '" & Format(tglDuaHariSebelum, "yyyy-MM-dd") & "' and '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
			ExecuteTrans(SQL)

			SQL = "delete from Cetak_TransferStock where Kode_Perusahaan = '" & KodePerusahaan & "' and "
			SQL = SQL & "Tanggal_Cetak between '" & Format(tglDuaHariSebelum, "yyyy-MM-dd") & "' and '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
			ExecuteTrans(SQL)

			KdUnikPrintCetak.Clear()
			'===============================
			'=     GET DETAIL TRANSFER     =
			'===============================
			SQL = "SELECT a.No_Transaksi as No_Split, b.No_Faktur as No_RM, f.No_Faktur as No_TF, f.SO_Awal, f.SO_Tujuan, e.Kode_Barang, g.Serial_Number_Awal, h.Jumlah, g.Jumlah_Bags, g.Satuan_Input, h.Urut_Oto as Urut_Det2, "
			SQL &= $"g.Id_Wms_Tujuan, e.Urut_Material_Requisition_Convert "
			SQL &= $"FROM Emi_Split_Production_Order a "
			SQL &= $"INNER JOIN Emi_Material_Requisition b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Transaksi = b.No_Faktur_Order and b.Status is null "
			SQL &= $"INNER JOIN Emi_Material_Requisition_Det c ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.No_Faktur = c.No_Faktur "
			SQL &= $"INNER JOIN Emi_Material_Requisition_Det_Convert d ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.No_Faktur = d.No_Faktur AND c.Urut_Oto = d.No_Urut_Det "
			SQL &= $"inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Detail e on d.Kode_Perusahaan = e.Kode_Perusahaan and d.Urut_Oto = e.Urut_Material_Requisition_Convert "
			SQL &= $"inner join N_EMI_Transaksi_Transfer_Stock_Sementara f on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Faktur = f.No_Faktur and f.status is null "
			SQL &= $"inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Det g on e.Kode_Perusahaan = g.Kode_Perusahaan and e.No_Faktur = g.No_Faktur and e.Urut_Oto = g.Urut_TF "
			SQL &= $"inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 h on g.Kode_Perusahaan = h.Kode_Perusahaan and g.No_Faktur = h.No_Faktur and g.Urut_Oto = h.Urut_Det "
			SQL &= $"inner join Stock_Owner_Gudang i on c.kode_perusahaan = i.kode_perusahaan and c.Kode_Stock_Owner_Tujuan = i.Kode_Stock_Owner and i.Flag_Cold_Storage = 'Y' "
			SQL &= $"WHERE a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and a.Status is null and b.Status is null "
			SQL &= $"AND a.No_Transaksi = '{NoSplit}' "
			SQL &= $"AND b.Batch = '{Batch}' "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							Dim No_Split As String = .Rows(i).Item("No_Split")
							Dim No_RM As String = .Rows(i).Item("No_RM")
							Dim No_TF As String = .Rows(i).Item("No_TF")
							Dim Kd_So_Awal As String = .Rows(i).Item("SO_Awal")
							Dim Kd_So_Tujuan As String = .Rows(i).Item("SO_Tujuan")
							Dim Kd_Barang As String = .Rows(i).Item("Kode_Barang")
							Dim Sn_Awal As String = .Rows(i).Item("Serial_Number_Awal")
							Dim Jumlah As String = .Rows(i).Item("Jumlah")
							Dim Jumlah_Bags As String = .Rows(i).Item("Jumlah_Bags")
							Dim Satuan As String = .Rows(i).Item("Satuan_Input")
							Dim Urut_TF_Det2 As String = .Rows(i).Item("Urut_Det2")
							Dim Rak_Tujuan As String = .Rows(i).Item("Id_Wms_Tujuan")
							Dim Urut_Det_Convert As String = .Rows(i).Item("Urut_Material_Requisition_Convert")

							'Ambil Data SN Berdasar Barcode
							Dim tglMsk As String = ""
							Dim metodePengeluaranStock As String = ""
							SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired,b.Metode_Pengeluaran_Stok,a.Tgl_Masuk, a.Blok_SN, a.id_jenis_kategori_produksi "
							SQL = SQL & "from barang_sn a, barang b "
							SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
							SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
							SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
							SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
							SQL = SQL & "and a.Jumlah <> 0 "
							SQL = SQL & "and a.Serial_Number ='" & Sn_Awal & "' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									tglMsk = General_Class.CekNULL(Dr("Tgl_Masuk"))
									metodePengeluaranStock = General_Class.CekNULL(Dr("Metode_Pengeluaran_Stok"))
								End If
							End Using

							'==============================================
							'=     CEK STATUS MASING MASING TRANSAKSI     =
							'==============================================
							' SPLIT PRODUCTION
							SQL = "select Status from Emi_Split_Production_Order "
							SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
							SQL &= $"and No_Transaksi = '{No_Split}' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then

									If General_Class.CekNULL(Dr("Status")) = "Y" Then
										Dr.Close()
										CloseTrans()
										CloseConn()
										MessageBox.Show($"No Transaksi Split {No_Split} Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										SuccessPotongStock = False
										Exit Sub
									End If
								Else
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show($"Data Transaksi Split {No_Split} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									SuccessPotongStock = False
									Exit Sub
								End If
							End Using

							' SPLIT PRODUCTION
							SQL = "select Status from Emi_Material_Requisition "
							SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
							SQL &= $"and No_Faktur = '{No_RM}' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then

									If General_Class.CekNULL(Dr("Status")) = "Y" Then
										Dr.Close()
										CloseTrans()
										CloseConn()
										MessageBox.Show($"No Transaksi Request Material {No_RM} Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										SuccessPotongStock = False
										Exit Sub
									End If
								Else
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show($"Data Transaksi Request Material {No_RM} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									SuccessPotongStock = False
									Exit Sub
								End If
							End Using

							' TRANSFER STOCK
							SQL = "select Status from N_EMI_Transaksi_Transfer_Stock_Sementara "
							SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
							SQL &= $"and No_Faktur = '{No_TF}' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then

									If General_Class.CekNULL(Dr("Status")) = "Y" Then
										Dr.Close()
										CloseTrans()
										CloseConn()
										MessageBox.Show($"No Transaksi Transfer Stock {No_TF} Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										SuccessPotongStock = False
										Exit Sub
									End If
								Else
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show($"Data Transaksi Transfer Stock {No_TF} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									SuccessPotongStock = False
									Exit Sub
								End If
							End Using

							'============================
							'=       POTONG STOCK       =
							'============================
							Dim nilai_persediaan_min As Double = 0
							SQL = "select round(dbo.get_hpp(serial_number) * " & Jumlah & ", 2) as rp_persediaan_min from barang_sn where "
							SQL = SQL & "Kode_Stock_Owner='" & Kd_So_Awal & "' and Kode_Barang='" & Kd_Barang & "' "
							SQL = SQL & "and Serial_Number='" & Sn_Awal & "'"
							Using dr = OpenTrans(SQL)
								If dr.Read Then
									nilai_persediaan_min = dr("rp_persediaan_min")
								Else
									dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Data SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									SuccessPotongStock = False
									Exit Sub
								End If
							End Using

							Dim Nama As String = ""
							'Dim jumlahAkhir As Double = Val(dgv_GoodStock) - Val(dgv_Jumlah)
							SQL = "select Nama, Kode_Barang, round(good_stock,4) as good_stock, Jumlah_Bags from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Kd_So_Awal & "' "
							SQL = SQL & "and Kode_Barang='" & Kd_Barang & "' "
							Using dr = OpenTrans(SQL)
								If dr.Read Then
									Nama = dr("Kode_Barang")
									Dim StockSblmPotong As Double = dr("good_stock")
									If dr("good_stock") < Jumlah Then
										dr.Close()
										CloseTrans()
										CloseConn()
										MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
										SuccessPotongStock = False
										Exit Sub
									ElseIf dr("Jumlah_Bags") < Jumlah_Bags Then
										dr.Close()
										CloseTrans()
										CloseConn()
										MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
										SuccessPotongStock = False
										Exit Sub
									Else
										dr.Close()
										SQL = "update barang set Good_Stock = Round(Good_Stock - " & Jumlah & ", 4), Jumlah_Bags = Round(Jumlah_Bags - " & Jumlah_Bags & ", 4) "
										SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Kd_So_Awal & "' "
										SQL = SQL & " and Kode_Barang='" & Kd_Barang & "'"
										ExecuteTrans(SQL)

										SQL = "insert into N_EMI_LOG_Transaksi_Validasi_Transfer_Stock "
										SQL &= $"(Kode_Perusahaan, No_Transaksi, Tanggal, Jam, Action, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah_Awal, Bags_Awal, Jumlah_Update, Bags_Update) "
										SQL &= $"values ('{KodePerusahaan}', '{NoSplit}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', "
										SQL &= $"'POTONG STOCK BARANG', '{Kd_So_Awal}', '{Kd_Barang}', '-', '{StockSblmPotong}', 0, '{Jumlah}', 0) "
										ExecuteTrans(SQL)

									End If
								Else
									dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									SuccessPotongStock = False
									Exit Sub
								End If
							End Using

							SQL = "select round(jumlah,4) as jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Kd_So_Awal & "' "
							SQL = SQL & "and Kode_Barang='" & Kd_Barang & "' "
							SQL = SQL & "and Serial_Number='" & Sn_Awal & "'"
							Using dr = OpenTrans(SQL)
								If dr.Read Then
									Dim StockSblmPotong As Double = dr("jumlah")
									If dr("jumlah") < Jumlah Then
										dr.Close()
										CloseTrans()
										CloseConn()
										MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
										SuccessPotongStock = False
										Exit Sub
									ElseIf dr("Jumlah_Bags") < Jumlah_Bags Then
										dr.Close()
										CloseTrans()
										CloseConn()
										MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
										SuccessPotongStock = False
										Exit Sub
									Else
										dr.Close()
										SQL = "update barang_sn set jumlah = Round(jumlah - " & Jumlah & ", 4), Jumlah_Bags = Round(Jumlah_Bags - " & Jumlah_Bags & ", 4) "
										SQL = SQL & "where Kode_Stock_Owner='" & Kd_So_Awal & "' and Kode_Barang='" & Kd_Barang & "' "
										SQL = SQL & "and Serial_Number='" & Sn_Awal & "'"
										ExecuteTrans(SQL)

										SQL = "insert into N_EMI_LOG_Transaksi_Validasi_Transfer_Stock "
										SQL &= $"(Kode_Perusahaan, No_Transaksi, Tanggal, Jam, Action, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah_Awal, Bags_Awal, Jumlah_Update, Bags_Update) "
										SQL &= $"values ('{KodePerusahaan}', '{NoSplit}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', "
										SQL &= $"'POTONG STOCK BARANG SN', '{Kd_So_Awal}', '{Kd_Barang}', '{Sn_Awal}', '{StockSblmPotong}', 0, '{Jumlah}', 0) "
										ExecuteTrans(SQL)

									End If
								Else
									dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									SuccessPotongStock = False
									Exit Sub
								End If
							End Using

							'====================================
							'=       CEK KESESUAIAN STOCK       =
							'====================================
							SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
							SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
							SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
							SQL = SQL & "isnull(round(SUM(jumlah_bags), 4), 0) AS jumlah_bags_barang, "
							SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 4) from Barang_sn y "
							SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
							SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & Kd_So_Awal & "' "
							SQL = SQL & "AND a.Kode_Barang = '" & Kd_Barang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
							SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
							Using Ds9 = BindingTrans(SQL)
								If Ds9.Tables("MyTable").Rows.Count <> 0 Then
									If Ds9.Tables("MyTable").Rows(0).Item("good_stock") <> Ds9.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds9.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds9.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
										CloseTrans()
										CloseConn()
										MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										SuccessPotongStock = False
										Exit Sub
									End If
								Else
									CloseTrans()
									CloseConn()
									MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									SuccessPotongStock = False
									Exit Sub
								End If
							End Using

							'==============================
							'=       INSERT SN BARU       =
							'==============================
							Dim hargaIsn As String = ""
							Dim warnaLama As String = ""

							Dim QrLama As String = ""
							Dim batchLama As String = ""
							Dim namaBarang As String = ""
							Dim expDate As String = ""

							'Ambil Data Lama
							SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.warna "
							SQL = SQL & "from barang_sn a, barang b "
							SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
							SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
							SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
							SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
							SQL = SQL & "and a.Kode_Stock_Owner='" & Kd_So_Awal & "' "
							SQL = SQL & "and a.Kode_Barang ='" & Kd_Barang & "' "
							SQL = SQL & "and a.Serial_Number='" & Sn_Awal & "' "
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

							Dim GetPalletTujuan As String = ""
							SQL = "select top 1 id_wms_warehouse_position, nomor_urut from dbo.N_EMI_Wharehouse_Position_Fn('" & KodePerusahaan & "', "
							SQL = SQL & "'" & Kd_So_Tujuan & "', '" & Rak_Tujuan & "') "
							SQL = SQL & "where kode_barang is null "
							SQL = SQL & "order by nomor_urut"
							Using dr = OpenTrans(SQL)
								If dr.Read Then
									GetPalletTujuan = dr("nomor_urut")
								Else
									dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("data Rak Sudah Penuh . . ! ! ")
									SuccessPotongStock = False
									Exit Sub
								End If
							End Using

							'============================
							'=       TAMBAH STOCK       =
							'============================

							'GENERATE SN BARU
							Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
							Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
							Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

							Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)

							Dim Stock_Sebelum_Insert As Double = 0
							Dim Stock_SN_Sebelum_Insert As Double = 0
							Dim Bags_Sebelum_Insert As Double = 0
							Dim Bags_SN_Sebelum_Insert As Double = 0
							SQL = "select isnull(sum(Good_Stock), 0) as Stock, sum(Jumlah_Bags) as Stock_Bags from barang WHERE kode_perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "AND Kode_Stock_Owner = '" & Kd_So_Tujuan & "' and kode_barang = '" & Kd_Barang & "' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									Stock_Sebelum_Insert = Math.Round(Dr("Stock"), 4)
									Bags_Sebelum_Insert = Math.Round(Dr("Stock_Bags"), 4)
								End If
							End Using

							'INSERT BARANG SN BARU
							SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah,  Jumlah_Bags, "
							SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk, Blok_SN, id_jenis_kategori_produksi, No_Reservasi) "
							SQL = SQL & "select Kode_Perusahaan, '" & Kd_So_Tujuan & "', Kode_Barang, '" & SN_Baru & "', '" & Jumlah & "', " & Jumlah_Bags & ", "
							SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, '" & Rak_Tujuan & "', id_Susunan , Qr_Code, '" & newKodeUnikBerjalan & "', "
							SQL = SQL & "Kode_Unik_Asal, '" & GetPalletTujuan & "', batch_number, '" & warnaLama & "', Tgl_Masuk, NULL, id_jenis_kategori_produksi, '" & NoSplit & "' "
							SQL = SQL & "from Barang_SN "
							SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
							SQL = SQL & "and Kode_Stock_Owner='" & Kd_So_Awal & "' "
							SQL = SQL & "and Kode_Barang='" & Kd_Barang & "' "
							SQL = SQL & "and Serial_Number='" & Sn_Awal & "' "
							ExecuteTrans(SQL)

							SQL = "insert into N_EMI_LOG_Transaksi_Validasi_Transfer_Stock "
							SQL &= $"(Kode_Perusahaan, No_Transaksi, Tanggal, Jam, Action, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah_Awal, Bags_Awal, Jumlah_Update, Bags_Update) "
							SQL &= $"values ('{KodePerusahaan}', '{NoSplit}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', "
							SQL &= $"'INSERT STOCK BARANG SN', '{Kd_So_Tujuan}', '{Kd_Barang}', '{SN_Baru}', '{Stock_Sebelum_Insert}', 0, '{Jumlah}', 0) "
							ExecuteTrans(SQL)

							SQL = "update barang set Good_Stock= Round(Good_Stock + " & Jumlah & ", 4), Jumlah_Bags = Jumlah_Bags + " & Jumlah_Bags & " "
							SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Kd_So_Tujuan & "' "
							SQL = SQL & " and Kode_Barang='" & Kd_Barang & "'"
							ExecuteTrans(SQL)

							SQL = "insert into N_EMI_LOG_Transaksi_Validasi_Transfer_Stock "
							SQL &= $"(Kode_Perusahaan, No_Transaksi, Tanggal, Jam, Action, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah_Awal, Bags_Awal, Jumlah_Update, Bags_Update) "
							SQL &= $"values ('{KodePerusahaan}', '{NoSplit}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', "
							SQL &= $"'INSERT STOCK BARANG', '{Kd_So_Tujuan}', '{Kd_Barang}', '-', '{Stock_Sebelum_Insert}', 0, '{Jumlah}', 0) "
							ExecuteTrans(SQL)

							'CEK KESESUAIAN STOCK
							SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
							SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
							SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
							SQL = SQL & "isnull(round(SUM(jumlah_bags), 4), 0) AS jumlah_bags_barang, "
							SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 4) from Barang_sn y "
							SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
							SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & Kd_So_Tujuan & "' "
							SQL = SQL & "AND a.Kode_Barang = '" & Kd_Barang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
							SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
							Using Ds9 = BindingTrans(SQL)
								If Ds9.Tables("MyTable").Rows.Count <> 0 Then
									If Ds9.Tables("MyTable").Rows(0).Item("good_stock") <> Ds9.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds9.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds9.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
										CloseTrans()
										CloseConn()
										MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										SuccessPotongStock = False
										Exit Sub
									End If
								Else
									CloseTrans()
									CloseConn()
									MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									SuccessPotongStock = False
									Exit Sub
								End If
							End Using

							'=======================
							'=     CEK SN BARU     =
							'=======================
							SQL = "SELECT Kode_Perusahaan from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' AND Serial_Number = '" & SN_Baru & "'"
							Using Dr = OpenTrans(SQL)
								If Not Dr.Read Then
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Data SN Baru Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									SuccessPotongStock = False
									Exit Sub
								End If
							End Using

							'==================
							'=     JURNAL     =
							'==================
							'dari
							Dim inisial_faktur_dari As String = ""
							Dim akun_persediaan_dari As String = ""
							Dim akun_persediaan_tujuan As String = ""

							SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
							SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Kd_So_Awal & "' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									'akun_persediaan_dari = Dr("persediaan")
									inisial_faktur_dari = Dr("inisial_faktur")
								Else
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									SuccessPotongStock = False
									Exit Sub
								End If
							End Using

							SQL = "select c.akun_Persediaan "
							SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
							SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
							SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
							SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "and b.kode_stock_owner = '" & Kd_So_Awal & "' and b.Kode_Barang='" & Kd_Barang & "' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									akun_persediaan_dari = Dr("akun_Persediaan")
								Else
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									SuccessPotongStock = False
									Exit Sub
								End If
							End Using

							SQL = "select c.akun_Persediaan "
							SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
							SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
							SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
							SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
							SQL = SQL & "and b.kode_stock_owner = '" & Kd_So_Tujuan & "' and b.Kode_Barang='" & Kd_Barang & "' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									akun_persediaan_tujuan = Dr("akun_Persediaan")
								Else
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									SuccessPotongStock = False
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
							SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock " & No_TF & "', '', "
							SQL = SQL & "'-', '" & UserID & "')"
							ExecuteTrans(SQL)

							SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
									  Strings.Mid(akun_persediaan_dari, 2, 1),
									  Strings.Mid(Ganti(akun_persediaan_dari), 3),
									  KodePerusahaan, KodeProyek, "Persedian " & No_TF, "0", nilai_persediaan_min, pagenumber, Kd_So_Awal, Bahasa_Pilihan, Ket_Cost_Center_HO)
							ExecuteTrans(SQL)
							pagenumber = pagenumber + 1

							SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
									 Strings.Mid(akun_persediaan_tujuan, 2, 1),
									 Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
									 KodePerusahaan, KodeProyek, "Persedian " & No_TF, nilai_persediaan_min, "0", pagenumber, Kd_So_Tujuan, Bahasa_Pilihan, Ket_Cost_Center_HO)
							ExecuteTrans(SQL)
							pagenumber = pagenumber + 1

							SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
							SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
							SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									If Dr("debit") <> Dr("kredit") Then
										Dr.Close()
										CloseTrans()
										CloseConn()
										MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										SuccessPotongStock = False
										Exit Sub
									End If
								Else
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									SuccessPotongStock = False
									Exit Sub
								End If
							End Using

							'=======================
							'=     UPDATE DET2     =
							'=======================
							SQL = "select Jumlah, Jumlah_Bags, Urut_Det from N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 "
							SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and No_Faktur = '{No_TF}' and Urut_Oto = '{Urut_TF_Det2}' "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then

									If Val(HilangkanTanda(Dr("Jumlah"))) <> Val(HilangkanTanda(Jumlah)) Or Val(HilangkanTanda(Dr("Jumlah_Bags"))) <> Val(HilangkanTanda(Jumlah_Bags)) Then
										Dr.Close()
										CloseTrans()
										CloseConn()
										MessageBox.Show($"Jumlah Tidak Sesuai dengan tabel Det 2 pada Kode Barang {Kd_Barang}", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
										SuccessPotongStock = False
										Exit Sub
									End If

									Dim UrutDet As String = Dr("Urut_Det")

									Dr.Close()
									SQL = $"update N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 set Serial_Number = '{SN_Baru}', Kode_Voucher = '{Kode_voucher}', "
									SQL &= $"Flag_Validasi_RFID = 'Y', Tanggal_Validasi_RFID = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Validasi_RFID = '{Format(tgl_skg, "HH:mm:ss")}', "
									SQL &= $"User_Validasi_RFID = '{UserID}' "
									SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and No_Faktur = '{No_TF}' and Urut_Oto = '{Urut_TF_Det2}' "
									ExecuteTrans(SQL)

									SQL = "select Jumlah from N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 "
									SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and No_Faktur = '{No_TF}' and Urut_Det = '{UrutDet}' and Serial_Number is null "
									Using Dr2 = OpenTrans(SQL)
										If Not Dr2.Read Then
											Dr2.Close()
											SQL = "update N_EMI_Transaksi_Transfer_Stock_Sementara_Det set Flag_Validasi = 'Y' "
											SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and No_Faktur = '{No_TF}' and Urut_Oto = '{UrutDet}'  "
											ExecuteTrans(SQL)
										End If
									End Using
								Else
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show($"Data Transfer Sementara Det 2 Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									SuccessPotongStock = False
									Exit Sub
								End If
							End Using

							'================================
							'=     CEK REQUEST MATERIAL     =
							'================================

#Region "RM"

							''=======================================================
							''=     CEK APAKAH DATA RM DAN CEK JUMLAH KEBUTUHAN     =
							''=======================================================
							'Dim Jumlah_Kebutuhan_Request As Double = 0
							'Dim isDataRequest As Boolean = False
							'SQL = "select c.Jumlah as Jumlah_Kebutuhan "
							'SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_Det b, Emi_Material_Requisition_Det_Convert c "
							'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
							'SQL = SQL & "and a.No_Faktur = b.No_Faktur "
							'SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.No_Urut_Det "
							'SQL = SQL & "and a.status is null "
							'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
							'SQL = SQL & "and c.Urut_Oto = " & Urut_Det_Convert & " "
							'Using Dr = OpenTrans(SQL)
							'    If Dr.Read Then
							'        Jumlah_Kebutuhan_Request = Dr("Jumlah_Kebutuhan")
							'        isDataRequest = True
							'    Else
							'        isDataRequest = False
							'    End If
							'End Using

							'If isDataRequest Then
							'    '================================
							'    '=     CEK APAKAH LAST DATA     =
							'    '================================
							'    Dim isLastData As Boolean = False
							'    SQL = "select c.Serial_Number_Awal, (d.Qr_Code+'-'+d.Kode_Unik_Berjalan) as barcode "
							'    SQL = SQL & "from N_EMI_Transaksi_Transfer_Stock_Sementara a, N_EMI_Transaksi_Transfer_Stock_Sementara_Detail b, N_EMI_Transaksi_Transfer_Stock_Sementara_Det c, barang_sn d "
							'    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
							'    SQL = SQL & "and a.No_Faktur = b.No_Faktur "
							'    SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
							'    SQL = SQL & "and c.Serial_Number_Awal = d.Serial_Number "
							'    SQL = SQL & "and a.Status is null "
							'    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
							'    SQL = SQL & "and a.No_Faktur = '" & No_TF & "' "
							'    SQL = SQL & "and not exists ( "
							'    SQL = SQL & "select 1 from N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 z where z.kode_perusahaan = c.Kode_Perusahaan and z.no_faktur = c.No_Faktur and z.Urut_Det = c.Urut_Oto and z.serial_number is not null) "
							'    Using Dr = OpenTrans(SQL)
							'        If Not Dr.Read Then
							'            isLastData = True
							'        End If
							'    End Using

							'    '=====================================
							'    '=     CEK JUMLAH SUDAH TRANSFER     =
							'    '=====================================
							'    Dim Jumlah_Sudah_Transfer As Double = 0
							'    SQL = "select isnull(sum(d.Jumlah), 0) as Jumlah_Transfer "
							'    SQL = SQL & "from N_EMI_Transaksi_Transfer_Stock_Sementara a, N_EMI_Transaksi_Transfer_Stock_Sementara_Detail b, N_EMI_Transaksi_Transfer_Stock_Sementara_Det c, N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 d "
							'    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
							'    SQL = SQL & "and a.No_Faktur = b.No_Faktur "
							'    SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
							'    SQL = SQL & "and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
							'    SQL = SQL & "and a.Status is null "
							'    SQL = SQL & "and d.Serial_Number is not null "
							'    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
							'    SQL = SQL & "and b.Urut_Material_Requisition_Convert = " & Urut_Det_Convert & " "
							'    Using Dr = OpenTrans(SQL)
							'        If Dr.Read Then
							'            Jumlah_Sudah_Transfer = Dr("Jumlah_Transfer")
							'        End If
							'    End Using

							'    '===========================================
							'    '=     GET NILAI TOLERANSI MIN DAN MAX     =
							'    '===========================================
							'    Dim Persen_Toleransi_Min As Double = 0
							'    Dim Persen_Toleransi_Max As Double = 0
							'    SQL = "select Toleransi_Tf_Min, Toleransi_Tf_Max from barang where Kode_Perusahaan = '" & KodePerusahaan & "' "
							'    SQL = SQL & "and Kode_Stock_Owner = '" & Kd_So_Awal & "' and Kode_Barang = '" & Kd_Barang & "' "
							'    Using Dr = OpenTrans(SQL)
							'        If Dr.Read Then
							'            Persen_Toleransi_Min = Dr("Toleransi_Tf_Min")
							'            Persen_Toleransi_Max = Dr("Toleransi_Tf_Max")
							'        End If
							'    End Using

							'    Dim nilai_toleransi_min As Double = Jumlah_Kebutuhan_Request - (Jumlah_Kebutuhan_Request * (Persen_Toleransi_Min / 100))
							'    Dim nilai_toleransi_max As Double = Jumlah_Kebutuhan_Request + (Jumlah_Kebutuhan_Request * (Persen_Toleransi_Max / 100))

							'    If Jumlah_Sudah_Transfer > nilai_toleransi_max Then
							'        CloseTrans()
							'        CloseConn()
							'        MessageBox.Show("Jumlah Sudah Transfer Lebih Dari Jumlah Request", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'        Exit Sub
							'    End If

							'    If isLastData Then

							'        If Jumlah_Sudah_Transfer < nilai_toleransi_min Then
							'            MessageBox.Show("Jumlah Sudah Transfer Kurang Dari Jumlah Request", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'        Else

							'            '================================
							'            '=       UPDATE DATA FLAG       =
							'            '================================
							'            SQL = "select a.Kode_Perusahaan "
							'            SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_Det b, Emi_Material_Requisition_Det_Convert c "
							'            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
							'            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
							'            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.No_Urut_Det "
							'            SQL = SQL & "and a.Status is null "
							'            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
							'            SQL = SQL & "and c.Urut_Oto = '" & Urut_Det_Convert & "' "
							'            Using Dr = OpenTrans(SQL)
							'                If Dr.Read Then

							'                    Dr.Close()
							'                    SQL = "update Emi_Material_Requisition_Det_Convert set Flag_Transfer = 'Y' "
							'                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Urut_Oto = '" & Urut_Det_Convert & "' "
							'                    ExecuteTrans(SQL)

							'                Else
							'                    Dr.Close()
							'                    CloseTrans()
							'                    CloseConn()
							'                    MessageBox.Show("Data Request Material Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'                    Exit Sub
							'                End If
							'            End Using

							'        End If

							'    Else
							'        If Jumlah_Sudah_Transfer >= nilai_toleransi_min And Jumlah_Sudah_Transfer <= nilai_toleransi_max Then
							'            '================================
							'            '=       UPDATE DATA FLAG       =
							'            '================================
							'            SQL = "select a.Kode_Perusahaan "
							'            SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_Det b, Emi_Material_Requisition_Det_Convert c "
							'            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
							'            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
							'            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.No_Urut_Det "
							'            SQL = SQL & "and a.Status is null "
							'            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
							'            SQL = SQL & "and c.Urut_Oto = '" & Urut_Det_Convert & "' "
							'            Using Dr = OpenTrans(SQL)
							'                If Dr.Read Then

							'                    Dr.Close()
							'                    SQL = "update Emi_Material_Requisition_Det_Convert set Flag_Transfer = 'Y' "
							'                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Urut_Oto = '" & Urut_Det_Convert & "' "
							'                    ExecuteTrans(SQL)

							'                Else
							'                    Dr.Close()
							'                    CloseTrans()
							'                    CloseConn()
							'                    MessageBox.Show("Data Request Material Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'                    Exit Sub
							'                End If
							'            End Using
							'        End If

							'    End If

							'End If

#End Region

							Dim kode_unik_print As String = ""
							'=====================================
							'=       GENERATE BARCODE BARU       =
							'=====================================
							kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")
							Dim fullNewQr As String = QrLama & "-" & newKodeUnikBerjalan

							Cmd.Parameters.Clear()
							Using ImgBarcode1 As Image = New_Generate_QR(fullNewQr)
								Using ms1 As New MemoryStream()
									ImgBarcode1.Save(ms1, Imaging.ImageFormat.Jpeg)
									Dim rawData1 As Byte() = ms1.ToArray()

									Dim param1 As String = "@newBarcode" & kode_unik_print
									Cmd.Parameters.Add(param1, SqlDbType.Image).Value = rawData1
								End Using
							End Using

							Dim BarcodeGenerate As String = "@newBarcode" & kode_unik_print

							'===================================
							'=       INSERT BARCODE BARU       =
							'===================================

							SQL = "insert into N_EMI_Cetak_Transfer_Stock_Sementara (kode_perusahaan, kode_barang, Barcode, Nama, QrUtuh, Qr, Tgl_Expired, batch, tanggal_cetak, kode_unik_print,tanggal_masuk,metode_pengeluaran_stok, No_Split, Jumlah, Satuan, No_Batch) values "
							SQL = SQL & "('" & KodePerusahaan & "', '" & Kd_Barang & "', " & BarcodeGenerate & ", '" & namaBarang & "', '" & fullNewQr & "', '" & QrLama & "', "
							SQL = SQL & "'" & expDate & "', '" & batchLama & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "','" & kode_unik_print & "' , "
							SQL = SQL & "'" & tglMsk & "', '" & metodePengeluaranStock & "', '" & No_Split & "', '" & Jumlah & "', '" & Satuan & "', '" & Batch & "') "
							ExecuteTrans(SQL)

							'SQL = "insert into Cetak_TransferStock (kode_perusahaan, kode_barang, Barcode, Nama, QrUtuh, Qr, Tgl_Expired, batch, tanggal_cetak, kode_unik_print,tanggal_masuk,metode_pengeluaran_stok) values "
							'SQL = SQL & "('" & KodePerusahaan & "', '" & Kd_Barang & "', " & BarcodeGenerate & ", '" & namaBarang & "', '" & fullNewQr & "', '" & QrLama & "', "
							'SQL = SQL & "'" & expDate & "', '" & batchLama & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "','" & kode_unik_print & "' , "
							'SQL = SQL & "'" & tglMsk & "', '" & metodePengeluaranStock & "' ) "
							'ExecuteTrans(SQL)

							KdUnikPrintCetak.Add(kode_unik_print)

						Next
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Data Transfer Sementara Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						SuccessPotongStock = False
						Exit Sub
					End If
				End With
			End Using

			'=======================================
			'=     INSERT TABEL TRANSFER STOCK     =
			'=======================================

			Dim No_Transaksi_Transfer As String = ""
			SQL = "SELECT distinct f.No_Faktur as No_TF, f.so_awal "
			SQL &= $"FROM Emi_Split_Production_Order a "
			SQL &= $"INNER JOIN Emi_Material_Requisition b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Transaksi = b.No_Faktur_Order and b.Status is null "
			SQL &= $"INNER JOIN Emi_Material_Requisition_Det c ON b.Kode_Perusahaan = c.Kode_Perusahaan AND b.No_Faktur = c.No_Faktur "
			SQL &= $"INNER JOIN Emi_Material_Requisition_Det_Convert d ON c.Kode_Perusahaan = d.Kode_Perusahaan AND c.No_Faktur = d.No_Faktur AND c.Urut_Oto = d.No_Urut_Det "
			SQL &= $"inner join N_EMI_Transaksi_Transfer_Stock_Sementara_Detail e on d.Kode_Perusahaan = e.Kode_Perusahaan and d.Urut_Oto = e.Urut_Material_Requisition_Convert "
			SQL &= $"inner join N_EMI_Transaksi_Transfer_Stock_Sementara f on e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Faktur = f.No_Faktur and f.status is null "
			SQL &= $"WHERE a.Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and a.Status is null and b.Status is null "
			SQL &= $"AND a.No_Transaksi = '{NoSplit}' "
			SQL &= $"AND b.Batch = '{Batch}' "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							Dim NoFakturTfSementara As String = .Rows(i).Item("No_TF")
							Dim KdSoAwal As String = .Rows(i).Item("so_awal")
							Dim InitialFaktur As String = ""

							SQL = $"
                                Select inisial_faktur
                                From Stock_Owner_Gudang
                                where kode_Stock_Owner = '{KdSoAwal}'
                            "
							Using Dr = OpenTrans(SQL)
								If Dr.Read Then
									InitialFaktur = Dr("inisial_faktur")
								Else
									Dr.Close()
									CloseTrans()
									CloseConn()
									MessageBox.Show($"Initial Faktur {KdSoAwal} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									SuccessPotongStock = False
									Exit Sub
								End If
							End Using

							Dim FPro_Results As String = "TS-"
							No_Transaksi_Transfer = FPro_Results & InitialFaktur & "-" & Format(tgl_skg, "MM/yy") & "-" &
									  General_Class.Get_Last_Number2("Tf_Stock_parent", "no_faktur", JumlahDigit,
									  "Kode_perusahaan", KodePerusahaan,
									  "And", "substring(no_faktur,1," & Len(FPro_Results) + Len(InitialFaktur) + 6 & ")", FPro_Results & InitialFaktur & "-" & Format(tgl_skg, "MM/yy"))

#Region "INSERT TF STOCK MANUAL"

							''===========================
							''=     GET DATA PARENT     =
							''===========================
							'SQL = "Select 1 from N_EMI_Transaksi_Transfer_Stock_Sementara "
							'SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
							'SQL &= $"and status is NULL "
							'SQL &= $"and No_Faktur = '{NoFakturTfSementara}'"
							'Using Dr = OpenTrans(SQL)
							'    If Dr.Read Then
							'        Dr.Close()

							'        SQL = "insert into tf_Stock_parent (Kode_Perusahaan, No_Faktur, No_Split, Status, SO_Awal, SO_Tujuan, Tanggal, Jam, UserID, Jenis_Transfer, Lokasi, Keterangan) "
							'        SQL &= $"select Kode_Perusahaan, '{No_Transaksi_Transfer}', No_Split, Status, SO_Awal, SO_Tujuan, Tanggal, Jam, UserID, Jenis_Transfer, Lokasi, Keterangan "
							'        SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
							'        SQL &= $"and status is NULL "
							'        SQL &= $"and No_Faktur = '{NoFakturTfSementara}'"
							'        ExecuteTrans(SQL)

							'    Else
							'        Dr.Close()
							'        CloseTrans()
							'        CloseConn()
							'        MessageBox.Show($"No Faktur {NoSplit} dan Batch {Batch} tidak ditemukan pada transaksi transfer stock sementara", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'        SuccessPotongStock = False
							'        Exit Sub
							'    End If
							'End Using

							''===========================
							''=     GET DATA DETAIL     =
							''===========================
							'SQL = $"
							'    select kode_barang, Urut_Oto
							'    from N_EMI_Transaksi_Transfer_Stock_Sementara_Detail
							'    where Kode_Perusahaan = '{KodePerusahaan}'
							'    and No_Faktur = '{NoFakturTfSementara}'
							'"
							'Using Ds1 = BindingTrans(SQL)
							'    If Ds1.Tables("MyTable").Rows.Count <> 0 Then
							'        For j As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1

							'            Dim KdBarang_sementara As String = Ds1.Tables("MyTable").Rows(j).Item("kode_barang")
							'            Dim urut_detail_Sementara As String = Ds1.Tables("MyTable").Rows(j).Item("Urut_Oto")

							'            SQL = $"
							'                insert into tf_Stock (Kode_Perusahaan, No_Faktur, Kode_Barang, Total, Satuan, Total_Barang, Satuan_Barang, Total_Bags, No_Split, Urut_Material_Requisition_Convert, Flag_Timbang, Flag_Jenis_Request, Total_Input, Satuan_Input)
							'                select Kode_Perusahaan, '{No_Transaksi_Transfer}', Kode_Barang, Total, Satuan, Total_Barang, Satuan_Barang, Total_Bags, No_Split, Urut_Material_Requisition_Convert, Flag_Timbang,
							'                    Flag_Jenis_Request, Total_Input, Satuan_Input
							'                from N_EMI_Transaksi_Transfer_Stock_Sementara_Detail
							'                where Kode_Perusahaan = '{KodePerusahaan}'
							'                and No_Faktur = '{NoFakturTfSementara}'
							'                and Kode_Barang = '{KdBarang_sementara}'
							'                and Urut_Oto = '{urut_detail_Sementara}'
							'            "
							'            ExecuteTrans(SQL)

							'            Dim x_ident_current As Integer = 0
							'            SQL = "select IDENT_CURRENT('Tf_Stock') as urutan"
							'            Using Dr = OpenTrans(SQL)
							'                If Dr.Read Then
							'                    x_ident_current = Dr("urutan")
							'                End If
							'            End Using

							'            '========================
							'            '=     GET DATA DET     =
							'            '========================
							'            SQL = "select kode_Perusahaan from tf_Stock where "
							'            SQL = SQL & "kode_Perusahaan='" & KodePerusahaan & "' and "
							'            SQL = SQL & "No_Faktur='" & No_Transaksi_Transfer & "' and "
							'            SQL = SQL & "Kode_barang='" & KdBarang_sementara & "' and "
							'            SQL = SQL & "urut_oto='" & x_ident_current & "' "
							'            Using Dr = OpenTrans(SQL)
							'                If Not Dr.Read Then
							'                    Dr.Close()
							'                    CloseTrans()
							'                    CloseConn()
							'                    MessageBox.Show("Terjadi Kesalahan, Silahkan Ulangi Transaksi  . . ! ! ")
							'                    Exit Sub
							'                End If
							'            End Using

							'            SQL = $"
							'                insert into Tf_Stock_det (Kode_Perusahaan, No_Faktur, Id_Wms_Awal, No_Pallet_Awal, Id_Wms_Tujuan, Serial_Number_Awal, Jumlah, Jumlah_Bags, Selesai, Warna, Urut_TF, Kode_Voucher, Jumlah_Barang, Flag_Sudah_Cetak, Satuan_Input)
							'                select Kode_Perusahaan, '{No_Transaksi_Transfer}', Id_Wms_Awal, No_Pallet_Awal, Id_Wms_Tujuan, Serial_Number_Awal, Jumlah, Jumlah_Bags, Selesai, Warna, '{x_ident_current}', Kode_Voucher, Jumlah_Barang, Flag_Sudah_Cetak, Satuan_Input
							'                from N_EMI_Transaksi_Transfer_Stock_Sementara_Det
							'                where Kode_Perusahaan = '{KodePerusahaan}'
							'                and No_Faktur = '{NoFakturTfSementara}'
							'                and urut_tf = '{urut_detail_Sementara}'
							'            "
							'            ExecuteTrans(SQL)

							'            Dim x_ident_current_det As Integer = 0
							'            SQL = "select IDENT_CURRENT('Tf_Stock_det') as urutan"
							'            Using Dr = OpenTrans(SQL)
							'                If Dr.Read Then
							'                    x_ident_current_det = Dr("urutan")
							'                End If
							'            End Using

							'            '========================
							'            '=     GET DATA DET     =
							'            '========================
							'            SQL = $"
							'                select No_Faktur, Urut_Oto
							'                from N_EMI_Transaksi_Transfer_Stock_Sementara_Det
							'                where Kode_Perusahaan = '{KodePerusahaan}'
							'                and No_Faktur = '{NoFakturTfSementara}'
							'                and Urut_TF = '{urut_detail_Sementara}'
							'            "
							'            Using Ds2 = BindingTrans(SQL)
							'                If Ds2.Tables("MyTable").Rows.Count <> 0 Then
							'                    For k As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

							'                        Dim urut_det_Sementara As String = Ds2.Tables("MyTable").Rows(k).Item("Urut_Oto")

							'                        SQL = $"
							'                            insert into Tf_Stock_det2 (Kode_Perusahaan, No_Faktur, Urut_Det, No_Pallet, Serial_Number, Jumlah, UserID, Tanggal, Jam, Kode_Voucher, Jumlah_bags)
							'                            select Kode_Perusahaan, '{No_Transaksi_Transfer}', '{x_ident_current_det}', No_Pallet, Serial_Number, Jumlah, UserID, Tanggal, Jam, Kode_Voucher, Jumlah_bags
							'                            from N_EMI_Transaksi_Transfer_Stock_Sementara_Det2
							'                            where Kode_Perusahaan = '{KodePerusahaan}'
							'                            and No_Faktur = '{NoFakturTfSementara}'
							'                            and Urut_Det = '{urut_det_Sementara}'
							'                        "
							'                        ExecuteTrans(SQL)

							'                    Next
							'                Else
							'                    CloseTrans()
							'                    CloseConn()
							'                    MessageBox.Show($"No Faktur {NoSplit} dan Batch {Batch} tidak ditemukan pada transaksi transfer stock sementara det", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'                    SuccessPotongStock = False
							'                    Exit Sub
							'                End If
							'            End Using

							'        Next

							'    Else
							'        CloseTrans()
							'        CloseConn()
							'        MessageBox.Show($"No Faktur {NoSplit} dan Batch {Batch} tidak ditemukan pada transaksi transfer stock sementara detail", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							'        SuccessPotongStock = False
							'        Exit Sub
							'    End If
							'End Using

#End Region

#Region "Versi Optimal"

							'============================
							' INSERT PARENT
							'============================
							SQL = "
                                DECLARE @row INT

                                INSERT INTO Tf_Stock_parent
                                (
                                    Kode_Perusahaan, No_Faktur, No_Split, Status,
                                    SO_Awal, SO_Tujuan, Tanggal, Jam,
                                    UserID, Jenis_Transfer, Lokasi, Keterangan
                                )
                                SELECT
                                    Kode_Perusahaan, '" & No_Transaksi_Transfer & "', No_Split, Status,
                                    SO_Awal, SO_Tujuan, '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "',
                                    UserID, Jenis_Transfer, Lokasi, Keterangan
                                FROM N_EMI_Transaksi_Transfer_Stock_Sementara
                                WHERE Kode_Perusahaan = '" & KodePerusahaan & "'
                                  AND Status IS NULL
                                  AND No_Faktur = '" & NoFakturTfSementara & "'

                                SET @row = @@ROWCOUNT
                                IF @row = 0
                                    RAISERROR('DATA_NOT_FOUND',16,1)
                            "
							ExecuteTrans(SQL)

							SQL = $"
                                update N_EMI_Transaksi_Transfer_Stock_Sementara set No_Transfer_Stock = '{No_Transaksi_Transfer}', Batch = '{Batch}', Flag_Validasi_RFID = 'Y'
                                WHERE Kode_Perusahaan = '{KodePerusahaan}'
                                    AND Status IS NULL
                                    AND No_Faktur = '{NoFakturTfSementara}'
                            "
							ExecuteTrans(SQL)

							'============================
							' GET DETAIL
							'============================
							SQL = "
                                SELECT *
                                FROM N_EMI_Transaksi_Transfer_Stock_Sementara_Detail
                                WHERE Kode_Perusahaan = '" & KodePerusahaan & "'
                                  AND No_Faktur = '" & NoFakturTfSementara & "'
                            "
							Using Ds1 = BindingTrans(SQL)
								If Ds1.Tables("MyTable").Rows.Count = 0 Then
									CloseTrans()
									CloseConn()
									MessageBox.Show($"No Faktur {NoSplit} dan Batch {Batch} tidak ditemukan pada transaksi transfer stock sementara detail", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									SuccessPotongStock = False
									Exit Sub
								End If

								For Each row As DataRow In Ds1.Tables("MyTable").Rows

									'============================
									' INSERT Tf_Stock
									'============================
									SQL = "
                                        INSERT INTO Tf_Stock
                                        (
                                            Kode_Perusahaan, No_Faktur, Kode_Barang,
                                            Total, Satuan, Total_Barang, Satuan_Barang,
                                            Total_Bags,
                                            Urut_Material_Requisition_Convert,
                                            Flag_Timbang, Flag_Jenis_Request,
                                            Total_Input, Satuan_Input
                                        )
                                        VALUES
                                        (
                                            '" & KodePerusahaan & "', '" & No_Transaksi_Transfer & "', '" & row("Kode_Barang") & "',
                                            " & row("Total") & ", '" & row("Satuan") & "',
                                            " & row("Total_Barang") & ", '" & row("Satuan_Barang") & "',
                                            " & row("Total_Bags") & ",
                                            '" & row("Urut_Material_Requisition_Convert") & "',
                                            '" & row("Flag_Timbang") & "', '" & row("Flag_Jenis_Request") & "',
                                            " & row("Total_Input") & ", '" & row("Satuan_Input") & "'
                                        )
                                        SELECT CAST(SCOPE_IDENTITY() AS INT)
                                    "
									Dim UrutTF As Integer
									Using Dr = OpenTrans(SQL)
										Dr.Read()
										UrutTF = Dr(0)
									End Using

									'============================
									' INSERT Tf_Stock_det
									'============================
									'SQL = "
									'    INSERT INTO Tf_Stock_det
									'    SELECT
									'        Kode_Perusahaan, '" & No_Transaksi_Transfer & "',
									'        Id_Wms_Awal, No_Pallet_Awal, Id_Wms_Tujuan,
									'        Serial_Number_Awal, Jumlah, Jumlah_Bags,
									'        Selesai, Warna, '" & UrutTF & "',
									'        Kode_Voucher, Jumlah_Barang,
									'        Flag_Sudah_Cetak, Satuan_Input
									'    FROM N_EMI_Transaksi_Transfer_Stock_Sementara_Det
									'    WHERE Kode_Perusahaan = '" & KodePerusahaan & "'
									'      AND No_Faktur = '" & NoFakturTfSementara & "'
									'      AND Urut_TF = '" & row("Urut_Oto") & "'
									'"
									'ExecuteTrans(SQL)

									'============================
									' INSERT Tf_Stock_det2 (FIX)
									'============================
									SQL = "
                                        DECLARE @MapUrut TABLE (
                                            Urut_Oto_Sementara INT,
                                            Urut_Oto_Baru INT
                                        )

                                        -- =========================================
                                        -- MERGE agar OUTPUT bisa akses source
                                        -- =========================================
                                        MERGE Tf_Stock_det AS target
                                        USING (
                                            SELECT *
                                            FROM N_EMI_Transaksi_Transfer_Stock_Sementara_Det
                                            WHERE Kode_Perusahaan = '" & KodePerusahaan & "'
                                              AND No_Faktur = '" & NoFakturTfSementara & "'
                                              AND Urut_TF = '" & row("Urut_Oto") & "'
                                        ) AS src
                                        ON 1 = 0   -- paksa INSERT saja
                                        WHEN NOT MATCHED THEN
                                        INSERT (
                                            Kode_Perusahaan, No_Faktur,
                                            Id_Wms_Awal, No_Pallet_Awal, Id_Wms_Tujuan,
                                            Serial_Number_Awal, Jumlah, Jumlah_Bags,
                                            Selesai, Warna, Urut_TF,
                                            Kode_Voucher, Jumlah_Barang,
                                            Flag_Sudah_Cetak, Satuan_Input
                                        )
                                        VALUES (
                                            src.Kode_Perusahaan,
                                            '" & No_Transaksi_Transfer & "',
                                            src.Id_Wms_Awal,
                                            src.No_Pallet_Awal,
                                            src.Id_Wms_Tujuan,
                                            src.Serial_Number_Awal,
                                            src.Jumlah,
                                            src.Jumlah_Bags,
                                            src.Selesai,
                                            src.Warna,
                                            '" & UrutTF & "',
                                            src.Kode_Voucher,
                                            src.Jumlah_Barang,
                                            src.Flag_Sudah_Cetak,
                                            src.Satuan_Input
                                        )
                                        OUTPUT
                                            src.Urut_Oto,          -- dari tabel sementara
                                            inserted.Urut_Oto      -- dari Tf_Stock_det
                                        INTO @MapUrut (Urut_Oto_Sementara, Urut_Oto_Baru);

                                        -- =========================================
                                        -- INSERT Tf_Stock_det2 (FK ke Tf_Stock_det)
                                        -- =========================================
                                        INSERT INTO Tf_Stock_det2
                                        (
                                            Kode_Perusahaan, No_Faktur, Urut_Det,
                                            No_Pallet, Serial_Number, Jumlah,
                                            UserID, Tanggal, Jam, Kode_Voucher, Jumlah_Bags
                                        )
                                        SELECT
                                            d2.Kode_Perusahaan,
                                            '" & No_Transaksi_Transfer & "',
                                            m.Urut_Oto_Baru,
                                            d2.No_Pallet,
                                            d2.Serial_Number,
                                            d2.Jumlah,
                                            d2.UserID,
                                            '" & Format(tgl_skg, "yyyy-MM-dd") & "',
                                            '" & Format(tgl_skg, "HH:mm:ss") & "',
                                            d2.Kode_Voucher,
                                            d2.Jumlah_Bags
                                        FROM N_EMI_Transaksi_Transfer_Stock_Sementara_Det2 d2
                                        JOIN @MapUrut m
                                            ON d2.Urut_Det = m.Urut_Oto_Sementara
                                        WHERE d2.Kode_Perusahaan = '" & KodePerusahaan & "'
                                          AND d2.No_Faktur = '" & NoFakturTfSementara & "'
                                    "
									ExecuteTrans(SQL)

								Next
							End Using

#End Region

						Next
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show($"No Faktur {NoSplit} dan Batch {Batch} tidak ditemukan pada transaksi transfer stock sementara", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						SuccessPotongStock = False
						Exit Sub
					End If
				End With
			End Using

			'=======================================
			'=     PEMINDAHAN TRANSAKSI KE LOG     =
			'=======================================

#Region "PROSES PINDAH KE GRAINDING"

			'SQL = "
			'        INSERT INTO N_EMI_Pairing_RFID_Log
			'        (Kode_Perusahaan, No_Split_Production_Order, Kode_Stock_Owner, RFID_Tag,
			'         Tanggal_Pairing, Jam_Pairing, UserID_Pairing, Flag_Pairing_Ulang, Urut_Pairing, Lokasi_Pairing, batch)
			'        SELECT
			'            Kode_Perusahaan, No_Split_Production_Order, Kode_Stock_Owner, RFID_Tag,
			'            Tanggal_Pairing, Jam_Pairing, UserID_Pairing, 'Y', Urut_Pairing, Lokasi_Pairing, batch
			'        FROM N_EMI_Pairing_RFID
			'        WHERE Kode_Perusahaan = @KodePerusahaan
			'            AND No_Split_Production_Order = @NoFaktur
			'            and batch  = @Batchh
			'    "

			'Cmd.Parameters.Clear()
			'Cmd.Parameters.AddWithValue("KodePerusahaan", KodePerusahaan)
			'Cmd.Parameters.AddWithValue("NoFaktur", SelectedNoFaktur)
			'Cmd.Parameters.AddWithValue("Batchh", SelectedBatch)
			'ExecuteTrans(SQL)

			'SQL = "
			'        DELETE FROM N_EMI_Pairing_RFID
			'        WHERE Kode_Perusahaan = @KodePerusahaan
			'            AND No_Split_Production_Order = @NoFaktur
			'            and batch  = @Batchh
			'    "

			'Cmd.Parameters.Clear()
			'Cmd.Parameters.AddWithValue("KodePerusahaan", KodePerusahaan)
			'Cmd.Parameters.AddWithValue("NoFaktur", SelectedNoFaktur)
			'Cmd.Parameters.AddWithValue("Batchh", SelectedBatch)
			'ExecuteTrans(SQL)

			'For i As Integer = 0 To Lv_RFID_Tags.Items.Count - 1

			'    Dim RFIDTAG As String = Lv_RFID_Tags.Items(i).SubItems(0).Text
			'    If String.IsNullOrEmpty(SelectedNoFaktur) Then
			'        MessageBox.Show("Silakan pilih No Transaksi terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			'        Exit Sub
			'    End If

			'    SQL = "select 1 from N_EMI_Master_Data_RFID_Tags "
			'    SQL &= $"where RFID_Tag = '{RFIDTAG}' "
			'    Using Dr = OpenTrans(SQL)
			'        If Dr.Read Then

			'            Dr.Close()
			'            SQL = "update N_EMI_Master_Data_RFID_Tags set No_Production_Order = NULL, Batch = NULL, Status = NULL "
			'            SQL &= $"where RFID_Tag = '{RFIDTAG}' "
			'            ExecuteTrans(SQL)

			'        Else
			'            Dr.Close()
			'            CloseTrans()
			'            CloseConn()
			'            MessageBox.Show($"RFID Tag {RFIDTAG} Tidak Ditemukan Pada Database", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			'            Exit Sub
			'        End If
			'    End Using

			'Next

#End Region

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Data Berhasil Di Potong Stock", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			SuccessPotongStock = False
			Exit Sub
		End Try

		'=========================
		'=     CETAK BARCODE     =
		'=========================
		Try
			OpenConn()

			Dim CrDoc As New Object
			Dim kertasBarcode As String = ""

			'=========================
			'=     CETAK BARCODE     =
			'=========================

			For x As Integer = 0 To KdUnikPrintCetak.Count - 1

				SQL = "select Kode_Perusahaan from N_EMI_Cetak_Transfer_Stock_Sementara where Kode_Perusahaan='" & KodePerusahaan & "' and kode_unik_print='" & KdUnikPrintCetak(x) & "'"

				'SQL = "select Kode_Perusahaan from Cetak_TransferStock where Kode_Perusahaan='" & KodePerusahaan & "' and kode_unik_print='" & KdUnikPrintCetak(x) & "'"

				Using Ds = BindingTrans(SQL)
					If Ds.Tables("MyTable").Rows.Count <> 0 Then
						CrDoc = New N_EMI_Barcode_Transfer_Stock_Sementara
						kertasBarcode = "BarcodeFG"

						'With A_Place_For_Printing2
						'    CrDoc.SetDataSource(Ds)
						'    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						'    CrDoc.PrintOptions.PrinterName = ""
						'    CrDoc.RecordSelectionFormula = "{N_EMI_Cetak_Transfer_Stock_Sementara.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Cetak_Transfer_Stock_Sementara.kode_unik_print} = '" & KdUnikPrintCetak(x) & "' "
						'    CrDoc.SummaryInfo.ReportTitle = "New Barcode Transfer Stock Sementara"
						'    .Text = "New Barcode Transfer Stock Sementara"
						'    .CrystalReportViewer1.ReportSource = CrDoc
						'    .Refresh()
						'    .Show()
						'End With

						'=================================================================================================================================================================

						CrDoc.SetDataSource(Ds)
						CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
						CrDoc.RecordSelectionFormula = "{N_EMI_Cetak_Transfer_Stock_Sementara.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Cetak_Transfer_Stock_Sementara.kode_unik_print} = '" & KdUnikPrintCetak(x) & "' "
						'CrDoc.RecordSelectionFormula = "{Cetak_TransferStock.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_TransferStock.kode_unik_print} = '" & KdUnikPrintCetak(x) & "' "

						CrDoc.PrintOptions.PrinterName = PrinterBarcode

						Dim doctoprint As New System.Drawing.Printing.PrintDocument()
						doctoprint.PrinterSettings.PrinterName = PrinterBarcode

						Dim rawKind As Integer
						Dim isPaperFound As Boolean = False
						CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
						For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
							If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertasBarcode Then
								rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
								CrDoc.PrintOptions.PaperSize = rawKind
								isPaperFound = True
								Exit For
							End If
						Next

						If Not isPaperFound Then
							'CloseConn()
							MessageBox.Show("Kertas Tidak DiTemukan, Kertas di set ke default", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
							'Exit Sub
						End If

						CrDoc.PrintToPrinter(1, False, 1, 99)

					End If
				End Using

			Next

			CloseConn()
			MessageBox.Show("Berhasil Cetak Barcode", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			SuccessPotongStock = False
			Exit Sub
		End Try

	End Sub

	Private Sub HapusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HapusToolStripMenuItem.Click
		If RFID_Non_Aktif Then Exit Sub
		If TabControl1.SelectedIndex <> 0 Then
			MessageBox.Show("Hapus RFID hanya Dapat Dilakukan Pada Tab 1", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If
		If Lv_RFID_Tags.Items.Count = 0 Or Lv_RFID_Tags.FocusedItem Is Nothing Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'====================
			'=     CEK ROLE     =
			'====================
			If CekButtonRole("Hapus_RFID_Tag") = "T" Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Anda Tidak Memiliki Akses Untuk Hapus RFID", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Exit Sub
			End If

			Dim RfidTag As String = Lv_RFID_Tags.FocusedItem.SubItems(0).Text.Trim

			'==========================
			'=     CEK MASTER TAG     =
			'==========================
			SQL = "select No_Production_Order, Batch from N_EMI_Master_Data_RFID_Tags "
			SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
			SQL &= $"and RFID_Tag = '{RfidTag}' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					If General_Class.CekNULL(Dr("No_Production_Order")).Trim <> SelectedNoFaktur.Trim Then
						'Dr.Close()
						'CloseTrans()
						'CloseConn()
						'MessageBox.Show("No Split Master Data RFID Tidak Sesuai", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'Exit Sub
					ElseIf General_Class.CekNULL(Dr("Batch")).Trim <> SelectedBatch.Trim Then
						'Dr.Close()
						'CloseTrans()
						'CloseConn()
						'MessageBox.Show("Batch Master Data RFID Tidak Sesuai", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						'Exit Sub
					End If
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Master Data RFID Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'====================
			'=     CEK DATA     =
			'====================
			SQL = "select 1 from N_EMI_Pairing_RFID "
			SQL &= $"where Kode_Perusahaan ='{KodePerusahaan}' "
			SQL &= $"and No_Split_Production_Order = '{SelectedNoFaktur}' "
			SQL &= $"and batch = '{SelectedBatch}' "
			SQL &= $"and RFID_Tag = '{RfidTag}' "
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then

					Dr.Close()
					SQL = "update N_EMI_Master_Data_RFID_Tags set No_Production_Order = NULL, Batch = NULL "
					SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
					SQL &= $"and RFID_Tag = '{RfidTag}' "
					ExecuteTrans(SQL)

					SQL = "delete FROM N_EMI_Pairing_RFID "
					SQL &= $"where Kode_Perusahaan ='{KodePerusahaan}' "
					SQL &= $"and No_Split_Production_Order = '{SelectedNoFaktur}' "
					SQL &= $"and batch = '{SelectedBatch}' "
					SQL &= $"and RFID_Tag = '{RfidTag}' "
					ExecuteTrans(SQL)
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Data RFID Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Data Berhasil Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Btn_Refresh.PerformClick()

	End Sub

	Public Function New_Generate_QR(ByVal isi As String)
		Dim options As New ZXing.QrCode.QrCodeEncodingOptions()

		options.DisableECI = True
		options.CharacterSet = "UTF-8"
		options.Width = 80
		options.Height = 80
		options.Margin = 0

		Dim qr As New ZXing.BarcodeWriter()
		qr.Format = ZXing.BarcodeFormat.QR_CODE
		qr.Options = options

		Dim result As New Bitmap(qr.Write(isi))
		Return result
	End Function

End Class