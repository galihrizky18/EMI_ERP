Public Class N_EMI_SD_Detail_Formulator
	Property NoFaktur As String

	Private Sub N_EMI_SD_Detail_Formulator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		With LvDetailBahan
			.View = View.Details
			.FullRowSelect = True
			.GridLines = True
			.MultiSelect = False
			.HideSelection = False
			.Columns.Clear()

			.Columns.Add("Kode Barang", 100)
			.Columns.Add("Nama Barang", 300)
			.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
			.Columns.Add("Jumlah", 100, HorizontalAlignment.Right)
			.Columns.Add("Persentase", 100, HorizontalAlignment.Right)
			.Columns.Add("Harga", 100, HorizontalAlignment.Right)
			.Columns.Add("Est. HPP Per Pcs", 100, HorizontalAlignment.Right)
		End With

		With LV_AnalisaLabTrialKitchen
			.View = View.Details
			.FullRowSelect = True
			.GridLines = True
			.HideSelection = False
			.Columns.Clear()

			.Columns.Add("Nama Mesin", 100, HorizontalAlignment.Left)
			.Columns.Add("Jenis Mesin", 200, HorizontalAlignment.Left)
			.Columns.Add("Standar Min", 100, HorizontalAlignment.Center)
			.Columns.Add("Standar Max", 100, HorizontalAlignment.Center)
			.Columns.Add("Avg Hasil", 100, HorizontalAlignment.Center)
			.Columns.Add("Hasil", 100, HorizontalAlignment.Center)
		End With

		With LV_AnalisaLabTrialProduksi
			.View = View.Details
			.FullRowSelect = True
			.GridLines = True
			.HideSelection = False
			.Columns.Clear()

			.Columns.Add("Nama Mesin", 100, HorizontalAlignment.Left)
			.Columns.Add("Jenis Mesin", 200, HorizontalAlignment.Left)
			.Columns.Add("Standar Min", 100, HorizontalAlignment.Center)
			.Columns.Add("Standar Max", 100, HorizontalAlignment.Center)
			.Columns.Add("Avg Hasil", 100, HorizontalAlignment.Center)
			.Columns.Add("Hasil", 100, HorizontalAlignment.Center)
		End With

		'With LvDetailKandungan
		'    .View = View.Details
		'    .FullRowSelect = True
		'    .GridLines = True
		'    .MultiSelect = False
		'    .HideSelection = False
		'    .Columns.Clear()

		'    .Columns.Add("Kode Uji", 100)
		'    .Columns.Add("Satuan", 80, HorizontalAlignment.Center)
		'    .Columns.Add("Hasil", 100, HorizontalAlignment.Right)
		'End With

		'With LvDetailStep
		'    .View = View.Details
		'    .FullRowSelect = True
		'    .GridLines = True
		'    .MultiSelect = False
		'    .HideSelection = False
		'    .Columns.Clear()

		'    .Columns.Add("No Step", 100)
		'    .Columns.Add("Kode", 80)
		'    .Columns.Add("Deskripsi", 300)
		'    .Columns.Add("Jumlah", 100, HorizontalAlignment.Right)
		'    .Columns.Add("Persentase", 100, HorizontalAlignment.Right)
		'End With

		Lv_Moisture_Content.Columns.Clear()
		Lv_Moisture_Content.Columns.Add("Kode Analisa", 130, HorizontalAlignment.Left)
		Lv_Moisture_Content.Columns.Add("Jenis Analisa", 230, HorizontalAlignment.Left)
		Lv_Moisture_Content.Columns.Add("Kategori", 180, HorizontalAlignment.Left)
		Lv_Moisture_Content.Columns.Add("Kriteria", 180, HorizontalAlignment.Left)
		Lv_Moisture_Content.Columns.Add("Range Awal", 150, HorizontalAlignment.Right)
		Lv_Moisture_Content.Columns.Add("Range Akhir", 150, HorizontalAlignment.Right)
		Lv_Moisture_Content.View = View.Details

		Fetch_DetailFormula()
		Fetch_LvDetailBahan()
		Fetch_LvDetailKandungan()
		Fetch_LvDetailStep()
		Fetch_LvDetailMoistureContent()
		Fetch_LvAnalisaLabTrialKitchen()
		Fetch_LvAnalisaLabTrialProduksi()

		Dim totalEstHPP As Decimal = 0
		Dim totalEstHPPPerPcs As Decimal = 0

		For Each item As ListViewItem In LvDetailBahan.Items
			totalEstHPP += Convert.ToDecimal(item.SubItems(5).Text)
		Next

		For Each item As ListViewItem In LvDetailBahan.Items
			totalEstHPPPerPcs += Convert.ToDecimal(item.SubItems(6).Text)
		Next

		'LbEstHPP.Text = "Est. HPP: " & FormatNumber(totalEstHPP, 4) & " | " & "Est. HPP Per Pcs: " & FormatNumber(totalEstHPPPerPcs, 4)
		LbEstHPP.Text = "Est. HPP Per Pcs: " & FormatNumber(totalEstHPPPerPcs, 4)
	End Sub

	Private Sub Fetch_LvAnalisaLabTrialKitchen()
		Try
			OpenConn()

			LV_AnalisaLabTrialKitchen.Items.Clear()

			Dim NoSplit As String = ""

			SQL = $"
				SELECT
					tk.No_Transaksi AS No_Split_Trial_Kitchen
				FROM EMI_Transaksi_Formulator a
				OUTER APPLY (
                    SELECT TOP 1 y.No_Transaksi
                    FROM N_EMI_Transaksi_Trial_Order_Produksi x
                    JOIN N_EMI_Transaksi_Trial_Split_Production_Order y ON y.Kode_Perusahaan = x.Kode_Perusahaan AND y.No_PO = x.No_Faktur
                    WHERE x.Kode_Formula = a.No_Faktur
                    ORDER BY y.Tanggal DESC, y.Jam DESC
                ) tk
				WHERE a.No_Faktur = '{NoFaktur}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					NoSplit = Dr("No_Split_Trial_Kitchen").ToString()
				End If
			End Using

			If NoSplit = "" Then
				CloseConn()
				Exit Sub
			End If

			SQL = $"
                    WITH cte AS (
                        SELECT
                            a.No_Split_Po,
                            a.No_Batch,
                            f.Nama_Mesin,
                            c.Kode_Aktivitas_Lab,
                            b.Id_Jenis_Analisa,
                            c.Jenis_Analisa,
                            b.No_Po_Sampel,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN CAST(ROUND(AVG(b.Hasil), 2) AS VARCHAR(50))
                                ELSE ISNULL(e.Keterangan_Kriteria, '-')
                            END AS keterangan_kriteria,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN ISNULL(CAST(d.Range_Awal AS VARCHAR(30)), '-')
                                ELSE '-'
                            END AS Std_Min,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                    THEN ISNULL(CAST(d.Range_Akhir AS VARCHAR(30)), '-')
                                ELSE '-'
                            END AS Std_Max,

                            CASE
                                WHEN b.Flag_Perhitungan = 'Y'
                                     AND ROUND(AVG(b.Hasil), 2)
                                         BETWEEN TRY_CAST(d.Range_Awal AS FLOAT)
                                         AND TRY_CAST(d.Range_Akhir AS FLOAT)
                                    THEN 'Lulus'

                                WHEN b.Flag_Perhitungan = 'Y'
                                     AND (
                                            ROUND(AVG(b.Hasil), 2) < TRY_CAST(d.Range_Awal AS FLOAT)
                                            OR ROUND(AVG(b.Hasil), 2) > TRY_CAST(d.Range_Akhir AS FLOAT)
                                         )
                                    THEN 'Tidak Lulus'

                                ELSE
                                    CASE
                                        WHEN e.Flag_Layak = 'Y'
                                            THEN 'Lulus'
                                        ELSE 'Tidak Lulus'
                                    END
                            END AS Hasil_Uji,

                            b.Status,
                            b.Flag_Final,
                            b.Flag_Approval,

                            CASE
                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LAB}'
                                                 AND b.Flag_Approval = 'T'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po) > 0
                                    THEN 'DITOLAK'

                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LAB}'
                                                 AND b.Flag_Approval = 'Y'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                     =
                                     SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LAB}'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                    THEN 'DISETUJUI'

                                ELSE 'MENUNGGU VALIDASI'
                            END AS status_lock_view_split,

                            CASE
                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LAB}'
                                                 AND b.Flag_Approval = 'T'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po) > 0
                                    THEN 'DITOLAK'

                                WHEN SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LAB}'
                                                 AND b.Flag_Approval = 'Y'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                     =
                                     SUM(
                                        CASE
                                            WHEN c.Kode_Aktivitas_Lab = '{KODE_ANALISA_LAB}'
                                                THEN 1
                                            ELSE 0
                                        END
                                     ) OVER(PARTITION BY a.No_Split_Po)
                                    THEN 'DISETUJUI'

                                ELSE 'MENUNGGU VALIDASI'
                            END AS status_analisa_lab_split

                        FROM N_LIMS_PO_Sampel a

                        JOIN N_EMI_LIMS_Uji_Sampel b
                            ON a.No_Sampel = b.No_Po_Sampel
                            AND b.Flag_Resampling IS NULL

                        JOIN N_EMI_LAB_Jenis_Analisa c
                            ON b.Id_Jenis_Analisa = c.id

                        LEFT JOIN N_EMI_LIMS_Uji_Pra_Final upf
                            ON b.No_Po_Sampel = upf.No_Sampel

                        LEFT JOIN N_EMI_LAB_Standar_Rentang d
                            ON b.Id_Jenis_Analisa = d.Id_Jenis_Analisa
                            AND b.Flag_Perhitungan = 'Y'
                            AND a.Kode_Barang = d.Kode_Barang

                        LEFT JOIN N_EMI_LAB_Standar_Rentang_Non_Perhitungan e
                            ON e.Nilai_Kriteria = b.Hasil
                            AND b.Flag_Perhitungan IS NULL
                            AND e.Kode_Role = '{KODE_ROLE_FLM}'

                        JOIN EMI_Master_Mesin f
                            ON a.Kode_Perusahaan = f.Kode_Perusahaan
                            AND a.Id_Mesin = f.Id_Master_Mesin

                        WHERE
                            b.Flag_Approval = 'Y'
                            AND a.Status IS NULL
                            AND b.Flag_Selesai = 'Y'
                            AND b.Status IS NULL

                        GROUP BY
                            a.No_Split_Po,
                            a.No_Batch,
                            f.Nama_Mesin,
                            c.Kode_Aktivitas_Lab,
                            b.Id_Jenis_Analisa,
                            c.Jenis_Analisa,
                            b.No_Po_Sampel,
                            d.Range_Awal,
                            d.Range_Akhir,
                            b.Status,
                            b.Flag_Final,
                            b.Flag_Approval,
                            b.Flag_Perhitungan,
                            e.Keterangan_Kriteria,
                            e.Flag_Layak
                    )

                    SELECT *
                    FROM cte

                    WHERE
                        status_lock_view_split = 'DISETUJUI'
                        AND status_analisa_lab_split = 'DISETUJUI'
                        AND No_Split_Po = '{NoSplit}'
                        AND Kode_Aktivitas_Lab = '{KODE_ANALISA_LAB}'

                    ORDER BY
                        Kode_Aktivitas_Lab
                "

			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim item As New ListViewItem(Dr("Nama_Mesin").ToString)

					item.SubItems.Add(Dr("Jenis_Analisa").ToString)
					item.SubItems.Add(Dr("Std_Min").ToString)
					item.SubItems.Add(Dr("Std_Max").ToString)
					item.SubItems.Add(Dr("keterangan_kriteria").ToString)
					item.SubItems.Add(Dr("Hasil_Uji").ToString)
					LV_AnalisaLabTrialKitchen.Items.Add(item)
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal mendapatkan data analisa lab trial kitchen: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try
	End Sub

	Private Sub Fetch_LvAnalisaLabTrialProduksi()
		Try
			OpenConn()

			LV_AnalisaLabTrialProduksi.Items.Clear()

			Dim NoSplit As String = ""

			SQL = $"
				SELECT
					tp.No_Transaksi AS No_Split_Trial_Produksi
				FROM EMI_Transaksi_Formulator a
				OUTER APPLY (
					SELECT TOP 1 y.No_Transaksi
					FROM EMI_Order_Produksi x
					JOIN Emi_Split_Production_Order y 
						ON y.Kode_Perusahaan = x.Kode_Perusahaan 
						AND y.No_PO = x.No_Faktur
					WHERE x.Kode_Formula = a.No_Faktur 
						AND x.Flag_Trial_Produksi = 'Y'
					ORDER BY y.Tanggal DESC, y.Jam DESC
				) tp
				WHERE a.No_Faktur = '{NoFaktur}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					NoSplit = Dr("No_Split_Trial_Produksi").ToString()
				End If
			End Using

			If NoSplit = "" Then
				CloseConn()
				Exit Sub
			End If

			SQL = $"
				SELECT 
					ROW_NUMBER() OVER (ORDER BY parameter, no_sample) AS no,
					x.*
				FROM (
					SELECT
						d.Jenis_Analisa AS parameter,
						a.No_Sampel AS no_sample,
						b.Nama_Mesin AS mesin,
						CASE
							WHEN e.Keterangan_Kriteria IS NULL THEN '-'
							ELSE e.Keterangan_Kriteria
						END AS hasil,
						'-' AS std_min,  
						'-' AS std_max,  
						CASE
							WHEN e.Flag_Layak = 'Y' THEN 'Lulus'
							ELSE 'Tidak Lulus'
						END AS status              
					FROM N_EMI_LAB_PO_Sampel a
					JOIN EMI_Master_Mesin b 
						ON a.Id_Mesin = b.Id_Master_Mesin
					JOIN N_EMI_LAB_Uji_Sampel c 
						ON c.No_Po_Sampel = a.No_Sampel
						AND c.Flag_Resampling IS NULL
					JOIN N_EMI_LAB_Jenis_Analisa d 
						ON d.id = c.Id_Jenis_Analisa
					LEFT JOIN N_EMI_LAB_Standar_Rentang_Non_Perhitungan e 
						ON e.Nilai_Kriteria = c.Hasil
						AND e.Kode_Role = '{KODE_ROLE_LAB}'
					WHERE a.No_Split_Po = '{NoSplit}' 
						AND a.Flag_Trial_Produksi = 'Y' 
						AND c.Flag_Perhitungan IS NULL
						AND d.Kode_Aktivitas_Lab = '{KODE_ANALISA_LAB}'
						AND c.Flag_Resampling IS NULL

					UNION 

					SELECT
						d.Jenis_Analisa AS parameter,
						a.No_Sampel AS no_sample,
						b.Nama_Mesin AS mesin,
						ISNULL(CAST(ROUND(AVG(c.Hasil), 2) AS VARCHAR(30)), '-') AS hasil,
						ISNULL(CAST(e.Range_Awal AS VARCHAR(30)), '-') AS std_min,                        
						ISNULL(CAST(e.Range_Akhir AS VARCHAR(30)), '-') AS std_max,
						CASE
							WHEN e.Id_Jenis_Analisa IS NULL
							THEN 'Lulus'

							WHEN d.Flag_Perhitungan = 'Y'
								AND ROUND(AVG(c.Hasil), 2)
									BETWEEN TRY_CAST(e.Range_Awal AS FLOAT)
									AND TRY_CAST(e.Range_Akhir AS FLOAT)
							THEN 'Lulus'

							WHEN d.Flag_Perhitungan = 'Y'
								AND (
										ROUND(AVG(c.Hasil), 2) < TRY_CAST(e.Range_Awal AS FLOAT)
									OR ROUND(AVG(c.Hasil), 2) > TRY_CAST(e.Range_Awal AS FLOAT)
									)
							THEN 'Tidak Lulus'

							ELSE 'Tidak Lulus'
						END AS status
					FROM N_EMI_LAB_PO_Sampel a
					JOIN EMI_Master_Mesin b 
						ON a.Id_Mesin = b.Id_Master_Mesin
					JOIN N_EMI_LAB_Uji_Sampel c 
						ON c.No_Po_Sampel = a.No_Sampel
						AND c.Flag_Resampling IS NULL
					JOIN N_EMI_LAB_Jenis_Analisa d 
						ON d.id = c.Id_Jenis_Analisa
					LEFT JOIN N_EMI_LAB_Standar_Rentang e 
						ON e.Id_Jenis_Analisa = c.Id_Jenis_Analisa
						AND e.Kode_Barang = a.Kode_Barang
						AND e.Kode_Role = '{KODE_ROLE_LAB}'
					WHERE a.No_Split_Po = '{NoSplit}' 
						AND a.Flag_Trial_Produksi = 'Y' 
						AND c.Flag_Perhitungan = 'Y'
						AND d.Kode_Aktivitas_Lab = '{KODE_ANALISA_LAB}'
						AND c.Flag_Resampling IS NULL
					GROUP BY
						d.Jenis_Analisa,
						a.No_Sampel,
						b.Nama_Mesin,
						e.Range_Awal,
						e.Range_Akhir,
						d.Flag_Perhitungan,
						e.Id_Jenis_Analisa
				) x
			"

			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim item As New ListViewItem(Dr("mesin").ToString)

					item.SubItems.Add(Dr("parameter").ToString)
					item.SubItems.Add(Dr("std_min").ToString)
					item.SubItems.Add(Dr("std_max").ToString)
					item.SubItems.Add(Dr("hasil").ToString)
					item.SubItems.Add(Dr("status").ToString)
					LV_AnalisaLabTrialProduksi.Items.Add(item)
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal mendapatkan data analisa lab trial produksi: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try
	End Sub

	Private Sub Fetch_DetailFormula()
		Try
			OpenConn()

			SQL = $"
                SELECT a.*, b.Nama AS Nama_Barang
                FROM Emi_Transaksi_Formulator a
                JOIN Barang b ON b.Kode_Perusahaan = a.Kode_Perusahaan AND b.Kode_Barang_Inq = a.Kode_Barang AND b.Kode_Stock_Owner = a.Kode_Stock_Owner
                WHERE a.Kode_Perusahaan = '{KodePerusahaan}' AND a.No_Faktur = '{NoFaktur}'

            "

			TlpDetailFormulator.Controls.Clear()

			TlpDetailFormulator.Controls.Add(New Label With {.Text = "No Formula", .AutoSize = True}, 0, 0)
			TlpDetailFormulator.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 0)
			TlpDetailFormulator.Controls.Add(New Label With {.Text = "Kode Barang", .AutoSize = True}, 0, 1)
			TlpDetailFormulator.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 1)
			TlpDetailFormulator.Controls.Add(New Label With {.Text = "Nama Barang", .AutoSize = True}, 0, 2)
			TlpDetailFormulator.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 2)
			TlpDetailFormulator.Controls.Add(New Label With {.Text = "Hasil", .AutoSize = True}, 0, 3)
			TlpDetailFormulator.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 1, 3)
			TlpDetailFormulator.Controls.Add(New Label With {.Text = "Tanggal Dibuat", .AutoSize = True}, 4, 0)
			TlpDetailFormulator.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 5, 0)
			TlpDetailFormulator.Controls.Add(New Label With {.Text = "Jam Dibuat", .AutoSize = True}, 4, 1)
			TlpDetailFormulator.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 5, 1)
			TlpDetailFormulator.Controls.Add(New Label With {.Text = "User", .AutoSize = True}, 4, 2)
			TlpDetailFormulator.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 5, 2)
			TlpDetailFormulator.Controls.Add(New Label With {.Text = "Tanggal Validasi", .AutoSize = True}, 8, 0)
			TlpDetailFormulator.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 9, 0)
			TlpDetailFormulator.Controls.Add(New Label With {.Text = "Jam Validasi", .AutoSize = True}, 8, 1)
			TlpDetailFormulator.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 9, 1)
			TlpDetailFormulator.Controls.Add(New Label With {.Text = "User", .AutoSize = True}, 8, 2)
			TlpDetailFormulator.Controls.Add(New Label With {.Text = ":", .AutoSize = True}, 9, 2)


			Using Dr = OpenTrans(SQL)
				If Dr.Read() Then
					TlpDetailFormulator.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("No_Faktur")), "", Dr("No_Faktur").ToString), .AutoSize = True}, 2, 0)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("Kode_Barang")), "", Dr("Kode_Barang").ToString), .AutoSize = True}, 2, 1)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("Nama_Barang")), "", Dr("Nama_Barang").ToString), .AutoSize = True}, 2, 2)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("Hasil")), "", Dr("Hasil").ToString) & " " & If(IsDBNull(Dr("Satuan_Hasil")), "", Dr("Satuan_Hasil").ToString), .AutoSize = True}, 2, 3)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("Tanggal")), "", Convert.ToDateTime(Dr("Tanggal")).ToString("dd MMM yyyy")), .AutoSize = True}, 6, 0)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("Jam")), "", Dr("Jam").ToString), .AutoSize = True}, 6, 1)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("UserID")), "", Dr("UserID").ToString), .AutoSize = True}, 6, 2)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("Tanggal_Validasi")), "", Convert.ToDateTime(Dr("Tanggal_Validasi")).ToString("dd MMM yyyy")), .AutoSize = True}, 10, 0)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("Jam_Validasi")), "", Dr("Jam_Validasi").ToString), .AutoSize = True}, 10, 1)
					TlpDetailFormulator.Controls.Add(New Label With {.Text = If(IsDBNull(Dr("User_Validasi")), "", Dr("User_Validasi").ToString), .AutoSize = True}, 10, 2)
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Fetch_LvDetailBahan()
		Try
			OpenConn()

			LvDetailBahan.Items.Clear()

			SQL = $"
            SELECT a.Kode_Barang, b.Nama AS Nama_Barang, a.Satuan,
                   a.Jumlah, a.Persentase, a.Est_HPP, a.Est_HPP_Per_Pcs
            FROM EMI_Transaksi_Formulator_Detail_Bahan a
            JOIN Barang b
                ON b.Kode_Perusahaan = a.Kode_Perusahaan
                AND b.Kode_Barang_Inq = a.Kode_Barang
                AND b.Kode_Stock_Owner = a.Kode_Stock_Owner
            WHERE a.Kode_Perusahaan = '{KodePerusahaan}'
            AND a.No_Faktur = '{NoFaktur}'
        "

			Using Dr = OpenTrans(SQL)
				While Dr.Read
					Dim item As New ListViewItem(Dr("Kode_Barang").ToString)

					item.SubItems.Add(If(IsDBNull(Dr("Nama_Barang")), "", Dr("Nama_Barang").ToString))
					item.SubItems.Add(If(IsDBNull(Dr("Satuan")), "", Dr("Satuan").ToString))

					item.SubItems.Add(If(IsDBNull(Dr("Jumlah")), "0", FormatNumber(Dr("Jumlah"), 2)))
					item.SubItems.Add(If(IsDBNull(Dr("Persentase")), "0", FormatNumber(Dr("Persentase"), 2)))

					item.SubItems.Add(If(IsDBNull(Dr("Est_HPP")), "0", FormatNumber(Dr("Est_HPP"), 4)))
					item.SubItems.Add(If(IsDBNull(Dr("Est_HPP_Per_Pcs")), "0", FormatNumber(Dr("Est_HPP_Per_Pcs"), 4)))

					LvDetailBahan.Items.Add(item)
				End While
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Fetch_LvDetailKandungan()
		Try
			OpenConn()

			'LvDetailKandungan.Items.Clear()

			'SQL = $"
			'             SELECT Kode_Uji, Satuan, Hasil
			'             FROM EMI_Transaksi_Formulator_Detail_Kandungan
			'             WHERE Kode_Perusahaan = '{KodePerusahaan}'
			'             AND No_Faktur = '{NoFaktur}'
			'         "

			'Using Dr = OpenTrans(SQL)
			'	While Dr.Read
			'		Dim item As New ListViewItem(Dr("Kode_Uji").ToString)

			'		item.SubItems.Add(If(IsDBNull(Dr("Satuan")), "", Dr("Satuan").ToString))
			'		item.SubItems.Add(If(IsDBNull(Dr("Hasil")), "", Dr("Hasil").ToString))

			'		LvDetailKandungan.Items.Add(item)
			'	End While
			'End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Fetch_LvDetailStep()
		Try
			OpenConn()

			'LvDetailStep.Items.Clear()

			'SQL = $"
			'             SELECT No_Step, Kode, Deskripsi, Jumlah, Satuan, Persentase
			'             FROM EMI_Transaksi_Formulator_Detail_Step
			'             WHERE Kode_Perusahaan = '{KodePerusahaan}'
			'             AND No_Faktur = '{NoFaktur}'
			'         "

			'Using Dr = OpenTrans(SQL)
			'	While Dr.Read
			'		Dim item As New ListViewItem(Dr("No_Step").ToString)

			'		item.SubItems.Add(If(IsDBNull(Dr("Kode")), "", Dr("Kode").ToString))
			'		item.SubItems.Add(If(IsDBNull(Dr("Deskripsi")), "", Dr("Deskripsi").ToString))
			'		item.SubItems.Add(If(IsDBNull(Dr("Jumlah")), "0", FormatNumber(Dr("Jumlah"), 2)))
			'		item.SubItems.Add(If(IsDBNull(Dr("Persentase")), "0", FormatNumber(Dr("Persentase"), 2)))

			'		LvDetailStep.Items.Add(item)
			'	End While
			'End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Fetch_LvDetailMoistureContent()
		Try
			OpenConn()

			Lv_Moisture_Content.Items.Clear()
			SQL = $"
				select b.id, b.Kode_Analisa, b.Jenis_Analisa, b.Flag_Perhitungan, b.Kode_Aktivitas_Lab, '-' as Value_Combobox, a.Range_Awal, a.Range_Akhir
				from N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang a
				inner join N_EMI_LAB_Jenis_Analisa b on a.Id_Jenis_Analisa = b.id
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Formula = '{NoFaktur}'
				union all
				select b.id, b.Kode_Analisa, b.Jenis_Analisa, b.Flag_Perhitungan, b.Kode_Aktivitas_Lab, c.label_keterangan as Value_Combobox, '' as Range_Awal, '' as Range_Akhir
				from N_EMI_Transaksi_Trial_Moisture_Content_Standar_Rentang_Non_Perhitungan a
				inner join N_EMI_LAB_Jenis_Analisa b on a.Id_Jenis_Analisa = b.id
				inner join EMI_Switch c on a.Kode_Perusahaan = c.kode_perusahaan and a.nilai_kriteria = c.keterangan
				where a.Kode_Perusahaan = '{KodePerusahaan}'
				and a.No_Formula = '{NoFaktur}'
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read

					Dim Flag_Perhitungan_Analisa As String = If(General_Class.CekNULL(Dr("Flag_Perhitungan")) = "", "T", Dr("Flag_Perhitungan"))

					Dim Lv As ListViewItem
					Lv = Lv_Moisture_Content.Items.Add(Dr("Kode_Analisa"))
					Lv.SubItems.Add(Dr("Jenis_Analisa"))
					Lv.SubItems.Add(If(Flag_Perhitungan_Analisa.Trim = "Y", "Perhitungan", "Non Perhitungan"))
					Lv.SubItems.Add(Dr("Value_Combobox"))
					Lv.SubItems.Add(Dr("Range_Awal"))
					Lv.SubItems.Add(Dr("Range_Akhir"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub BtnSimpanSudahBinding_Click(sender As Object, e As EventArgs) Handles BtnSimpanSudahBinding.Click
		Me.Close()
	End Sub

	Private Sub N_EMI_SD_Detail_Formulator_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
		If e.KeyCode = Keys.Escape Then
			Me.Close()
		End If
	End Sub
End Class