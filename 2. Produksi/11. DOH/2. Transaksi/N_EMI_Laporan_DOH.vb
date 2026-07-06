Public Class N_EMI_Laporan_DOH

	Private Sub Txt_KdBarang_Leave(sender As Object, e As EventArgs) Handles Txt_KdBarang.Leave
		If Txt_KdBarang.Text.Trim.Length = 0 Then Exit Sub
		If Lv_Barang.Focused = True Then Exit Sub

		Try
			OpenConn()

			If Not Txt_KdBarang.Text = OpsiSeluruh Then

				SQL = "select Distinct a.Kode_Barang, a.Nama "
				SQL = SQL & "from barang a, EMI_Group_Jenis b "
				SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
				SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
				SQL = SQL & "AND (b.Flag_Finished_Good = 'Y' OR b.Flag_Semi_FG = 'Y') "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and a.Kode_Barang = '" & Txt_KdBarang.Text & "' "
				Using Dr = Open(SQL)
					If Dr.Read Then
						Txt_KdBarang.Text = Dr("Kode_Barang")
						Txt_NmBarang.Text = Dr("Nama")
					Else
						MessageBox.Show("Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_KdBarang.Text = ""
						Txt_NmBarang.Text = ""
						Txt_KdBarang.Focus()
					End If

					Lv_Barang.Location = New Point(1200, 152)
					Lv_Barang.Visible = False
				End Using
			Else
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
			Txt_KdBarang_Leave(Txt_KdBarang, e)

			Lv_Barang.Location = New Point(1200, 152)
			Lv_Barang.Visible = False
		End If
	End Sub

	Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
	End Sub

	Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
		If Txt_KdBarang.Text.Trim.Length = 0 Then
			Lv_Barang.Location = New Point(1200, 152)
			Lv_Barang.Visible = False
			Txt_KdBarang.Text = ""
			Txt_NmBarang.Text = ""
			Exit Sub
		Else
			Lv_Barang.Visible = True
			Lv_Barang.Location = New Point(91, 106)
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Barang.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)

			SQL = "select Distinct a.Kode_Barang, a.Nama "
			SQL = SQL & "from barang a, EMI_Group_Jenis b "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
			SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
			SQL = SQL & "AND (b.Flag_Finished_Good = 'Y' OR b.Flag_Semi_FG = 'Y') "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Kode_Barang like '%" & Txt_KdBarang.Text & "%' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Dr("Nama"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Btn_ExportExcel_Click(sender As Object, e As EventArgs) Handles Btn_ExportExcel.Click
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			SQL = Get_Query()
			Dim ds As DataSet = BindingTrans(SQL)
			If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
				CloseTrans()
				CloseConn()
				MessageBox.Show("Tidak ada data untuk di-export!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
				Return
			End If

			Dim dt As DataTable = ds.Tables(0)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()

			Dim bgHeader As Color = Color.FromArgb(180, 198, 231)
			Dim config As New ExcelExportHelper.ExportConfig()
			config.FileName = "Laporan_DOH_" & Now.ToString("dd_MM_yyyy_HH_mm") & ".xlsx"
			config.ShowBorder = True
			config.FreezePanes = True
			config.AutoFilter = True

			Dim captions() As String = {
				"Kode Barang", "Nama Barang", "Avg Pouch", "Avg Can", "Avg Total", "Satuan",
				"Source", "Lead Time (Hari)", "Minimum Standby Stock", "Stock",
				"DOH (Hari)", "Selisih yang Harus Datang KG", "OS PR", "OS PO",
				"Open PR", "DOH (Incl PR dan PO)"
			}
            Dim headerRow As New ExcelExportHelper.HeaderRow()

            For Each cap As String In captions
                headerRow.AddCell(New ExcelExportHelper.HeaderCell(cap, backColor:=bgHeader))
            Next
            config.Headers.Add(headerRow)

            Dim textCols() As String = {"A", "B", "F", "G"}
			For Each col As String In textCols
				config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat(
			col, "@", ExcelExportHelper.ExcelAlignment.Left, forceText:=True))
			Next

			Dim n4Cols() As String = {"C", "D", "E", "I", "J", "L", "M", "N", "O"}
			For Each col As String In n4Cols
				config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat(
			col, "N4", ExcelExportHelper.ExcelAlignment.Right, forceNumber:=True))
			Next

			Dim n0Cols() As String = {"H", "K", "P"}
			For Each col As String In n0Cols
				config.ColumnFormats.Add(New ExcelExportHelper.ColumnFormat(
			col, "N0", ExcelExportHelper.ExcelAlignment.Right, forceNumber:=True))
			Next

			config.ColumnFormulas.Add(New ExcelExportHelper.ColumnFormula("E", "=SUM(C{ROW}:D{ROW})"))
			config.ColumnFormulas.Add(New ExcelExportHelper.ColumnFormula("I", "=IFERROR(E{ROW}*(H{ROW}/30),"""")"))
			config.ColumnFormulas.Add(New ExcelExportHelper.ColumnFormula("K", "=IFERROR(J{ROW}/(E{ROW}/26),"""")"))
			config.ColumnFormulas.Add(New ExcelExportHelper.ColumnFormula("L", "=IFERROR(I{ROW}-J{ROW},"""")"))
			config.ColumnFormulas.Add(New ExcelExportHelper.ColumnFormula("O", "=IFERROR(I{ROW}-J{ROW}-M{ROW}-N{ROW},"""")"))
			config.ColumnFormulas.Add(New ExcelExportHelper.ColumnFormula("P", "=IFERROR(IF(SUM(J{ROW},N{ROW}:O{ROW})/(E{ROW}/26)=0,"""",SUM(J{ROW},N{ROW}:O{ROW})/(E{ROW}/26)),"""")"))
			ExcelExportHelper.ExportFromDataTableDebug(dt, config)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show("Gagal export excel: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
		If e.KeyChar = Chr(13) Then
			Txt_KdBarang_Leave(Txt_NmBarang, e)

			Lv_Barang.Location = New Point(1200, 152)
			Lv_Barang.Visible = False
		End If
	End Sub

	Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
		If Txt_NmBarang.Text.Trim.Length = 0 Then
			Lv_Barang.Location = New Point(1200, 152)
			Lv_Barang.Visible = False
			Txt_KdBarang.Text = ""
			Txt_NmBarang.Text = ""
			Exit Sub
		Else
			Lv_Barang.Visible = True
			Lv_Barang.Location = New Point(91, 106)
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Barang.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)

			SQL = "select Distinct a.Kode_Barang, a.Nama "
			SQL = SQL & "from barang a, EMI_Group_Jenis b "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
			SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
			SQL = SQL & "AND (b.Flag_Finished_Good = 'Y' OR b.Flag_Semi_FG = 'Y') "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Nama like '%" & Txt_NmBarang.Text & "%' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Dr("Nama"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
	End Sub

	Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_Barang_DoubleClick(Lv_Barang, e)
		End If
	End Sub

	Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
		If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

		Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
		Dim Nmbarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

		Txt_KdBarang.Text = KdBarang
		Txt_NmBarang.Text = Nmbarang

		Lv_Barang.Location = New Point(1200, 152)
		Lv_Barang.Visible = False
	End Sub

	Private Sub Me_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub N_EMI_Laporan_DOH_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		Lv_Barang.Columns.Clear()
		Lv_Barang.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
		Lv_Barang.Columns.Add("Nama Barang", 260, HorizontalAlignment.Left)
		Lv_Barang.View = View.Details

		Kosong()
	End Sub

	Private Sub Kosong()
		'Tgl1.Value = DateTime.Today : Tgl2.Value = DateTime.Today
		Txt_KdBarang.Text = OpsiSeluruh : Txt_NmBarang.Text = OpsiSeluruh
		Lv_Barang.Visible = False
		Dgv_Data.Rows.Clear()
	End Sub

	Private Sub Load_Data()
		Dgv_Data.Rows.Clear()

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			SQL = Get_Query()
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							Dgv_Data.Rows.Add()
							Dgv_Data.Rows(i).Cells(0).Value = .Rows(i).Item("Kode_Bahan")
							Dgv_Data.Rows(i).Cells(1).Value = .Rows(i).Item("Nama")
							Dgv_Data.Rows(i).Cells(2).Value = If(General_Class.CekNULL(.Rows(i).Item("Avg_Pouch")) = "", 0, Format(.Rows(i).Item("Avg_Pouch"), "N4"))
							Dgv_Data.Rows(i).Cells(3).Value = If(General_Class.CekNULL(.Rows(i).Item("Avg_Can")) = "", 0, Format(.Rows(i).Item("Avg_Can"), "N4"))
							Dgv_Data.Rows(i).Cells(4).Value = If(General_Class.CekNULL(.Rows(i).Item("Avg_Total")) = "", 0, Format(.Rows(i).Item("Avg_Total"), "N4"))
							Dgv_Data.Rows(i).Cells(5).Value = .Rows(i).Item("Satuan")
							Dgv_Data.Rows(i).Cells(6).Value = .Rows(i).Item("Source")
							Dgv_Data.Rows(i).Cells(7).Value = If(General_Class.CekNULL(.Rows(i).Item("Lead_Time")) = "", 0, Format(.Rows(i).Item("Lead_Time"), "N0"))
							Dgv_Data.Rows(i).Cells(8).Value = If(General_Class.CekNULL(.Rows(i).Item("Min_Standby_Stock")) = "", 0, Format(.Rows(i).Item("Min_Standby_Stock"), "N4"))
							Dgv_Data.Rows(i).Cells(9).Value = If(General_Class.CekNULL(.Rows(i).Item("Stock")) = "", 0, Format(.Rows(i).Item("Stock"), "N4"))
							Dgv_Data.Rows(i).Cells(10).Value = If(General_Class.CekNULL(.Rows(i).Item("DOH")) = "", 0, Format(.Rows(i).Item("DOH"), "N0"))
							Dgv_Data.Rows(i).Cells(11).Value = If(General_Class.CekNULL(.Rows(i).Item("Stock_Kurang")) = "", 0, Format(.Rows(i).Item("Stock_Kurang"), "N4"))
							Dgv_Data.Rows(i).Cells(12).Value = If(General_Class.CekNULL(.Rows(i).Item("OS_PR")) = "", 0, Format(.Rows(i).Item("OS_PR"), "N4"))
							Dgv_Data.Rows(i).Cells(13).Value = If(General_Class.CekNULL(.Rows(i).Item("OS_PO")) = "", 0, Format(.Rows(i).Item("OS_PO"), "N4"))
							Dgv_Data.Rows(i).Cells(14).Value = If(General_Class.CekNULL(.Rows(i).Item("Open_PR")) = "", 0, Format(.Rows(i).Item("Open_PR"), "N4"))
							Dgv_Data.Rows(i).Cells(15).Value = If(General_Class.CekNULL(.Rows(i).Item("DOH_Include_PR_PO")) = "", 0, Format(.Rows(i).Item("DOH_Include_PR_PO"), "N0"))
						Next
					Else
						CloseTrans()
						CloseConn()
						MessageBox.Show("Data DOH tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show("Gagal load data DOH: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End Try
	End Sub

	Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
		Load_Data()
	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
		Kosong()
	End Sub

	Private Function Get_Query() As String
		Dim FilterBarang As String = ""
		If Txt_KdBarang.Text.Trim <> OpsiSeluruh AndAlso Txt_KdBarang.Text.Trim.Length > 0 Then
			FilterBarang = "AND b.Kode_Barang = '" & Txt_KdBarang.Text.Trim & "' "
		End If

		'Dim TglMulai As String = Tgl1.Value.ToString("yyyy-MM-dd")
		'Dim TglAkhir As String = Tgl2.Value.ToString("yyyy-MM-dd")

		SQL = $"
            ;WITH cte AS (
                SELECT DISTINCT
                    ISNULL(f.No_Faktur, '') AS kode_formula,
                    x.kode_barang_inq,
                    x.kode_perusahaan
                FROM barang x
                INNER JOIN emI_group_jenis y
                    ON x.kode_perusahaan = y.kode_perusahaan
                   AND x.id_group_jenis = y.id_group_jenis
                OUTER APPLY (
                    SELECT TOP 1
                        c.No_Faktur,
                        flag_invalid
                    FROM N_EMI_Transaksi_Formulator_Binding a
                    INNER JOIN N_EMI_Transaksi_Formulator_Binding_Detail b
                        ON a.Kode_Perusahaan = b.Kode_Perusahaan
                       AND a.No_Faktur = b.No_Faktur
                    INNER JOIN Emi_Transaksi_Formulator c
                        ON b.Kode_Perusahaan = c.Kode_Perusahaan
                       AND b.No_Formulator = c.No_Faktur
                       AND c.Status IS NULL
                    WHERE a.Status IS NULL
                      AND a.Flag_Validasi_Main = 'Y'
                      AND a.Kode_Perusahaan = x.kode_perusahaan
                      AND a.Kode_Barang = x.kode_barang_inq
                      AND c.Flag_Validasi_Formula_Produksi_BOD = 'Y'
                      AND no_prioritas = 1
                    ORDER BY a.Tanggal DESC, a.Jam DESC, b.No_Prioritas ASC
                ) f
                WHERE (y.Flag_Finished_Good = 'Y' OR y.Flag_Semi_FG = 'Y')
                  AND flag_invalid IS NULL
            ),

            cte_b AS (
                -- =========================
                -- SOURCE 1
                -- =========================
                SELECT
                    '1' AS dari,
                    b.Kode_Barang,
                    bb.Tipe_Produk,
                    a.bulan,
                    a.tahun,
                    b.kode_Barang AS kode_fg,
                    d.kode_barang AS Kode_Bahan,
                    d.Kode_Stock_Owner,
                    d.Nilai_Barang,
                    d.satuan_barang,
                    c.No_Faktur AS Kode_Formula,
                    (
                        (b.nilai_ppic - f_sum_beda_formula.total_qty_beda_formula)
                        * bb.Berat / 1000
                    ) AS nilai_ppic,
                    --(
                    --    (b.nilai_ppic - f_sum_beda_formula.total_qty_beda_formula - f_sum_sudah_po.total_sudah_po)
                    --    * bb.Berat / 1000
                    --) AS nilai_ppic,
                    c.hasil AS nilai_Formula,
                    z.Kode_Perusahaan,
                    z.id_group_jenis,
                    z.Nama
                FROM emi_transaksi_sales_forecasting a
                INNER JOIN emi_transaksi_sales_forecasting_detail b
                    ON a.Kode_Perusahaan = b.kode_Perusahaan
                   AND a.no_faktur = b.no_faktur
                INNER JOIN barang bb
                    ON b.kode_perusahaan = bb.kode_perusahaan
                   AND b.kode_barang = bb.kode_barang
                   AND b.kode_stock_owner = bb.kode_stock_owner
                INNER JOIN cte bc
                    ON bb.kode_perusahaan = bc.kode_perusahaan
                   AND bb.kode_barang_inq = bc.kode_barang_inq
                INNER JOIN Emi_Transaksi_Formulator c
                    ON c.Kode_Perusahaan = bc.Kode_Perusahaan
                   AND c.No_Faktur = bc.Kode_Formula
                INNER JOIN emi_transaksi_formulator_detail_Bahan d
                    ON c.Kode_Perusahaan = d.Kode_Perusahaan
                   AND c.no_faktur = d.no_faktur
                INNER JOIN barang z
                    ON d.Kode_Perusahaan = z.Kode_Perusahaan
                   AND d.Kode_Barang = z.Kode_Barang
                   AND d.Kode_Stock_Owner = z.Kode_Stock_Owner

                OUTER APPLY (
                    SELECT ISNULL(SUM(f.Jumlah - ISNULL(po.Jumlah_PO, 0)), 0) AS total_qty_beda_formula
                    FROM N_EMI_Production_Plan_Schedule_Detail f
                    INNER JOIN N_EMI_Production_Plan_Schedule g
                        ON f.No_Transaksi = g.No_Transaksi
                       AND f.Kode_Perusahaan = g.Kode_Perusahaan
                    LEFT JOIN (
                        SELECT
                            Urut_Production_Schedule,
                            Kode_Perusahaan,
                            SUM(Jumlah) AS Jumlah_PO
                        FROM EMI_Order_Produksi
                        WHERE Status IS NULL
                        GROUP BY Urut_Production_Schedule, Kode_Perusahaan
                    ) po
                        ON po.Urut_Production_Schedule = f.No_Urut
                       AND po.Kode_Perusahaan = f.Kode_Perusahaan
                    WHERE g.Status IS NULL
                      AND f.kode_formula IS NOT NULL
                      AND f.Kode_Perusahaan = b.Kode_Perusahaan
                      AND f.Urut_Production_Plan = b.urut
                ) f_sum_beda_formula

                --OUTER APPLY (
                --    SELECT ISNULL(SUM(y.Jumlah), 0) AS total_sudah_po
                --    FROM N_EMI_Production_Plan_Schedule_Detail x
                --    INNER JOIN emi_order_produksi y
                --        ON x.Kode_Perusahaan = y.Kode_Perusahaan
                --       AND x.No_Urut = y.Urut_Production_Schedule
                --    INNER JOIN n_emi_production_plan_schedule z
                --        ON x.Kode_Perusahaan = z.Kode_Perusahaan
                --       AND x.No_Transaksi = z.No_Transaksi
                --    WHERE x.Urut_Production_Plan = b.urut
                --      AND y.Status IS NULL
                --      AND z.Status IS NULL
                --) f_sum_sudah_po

                WHERE a.status IS NULL
                  AND b.Kode_Perusahaan = '{KodePerusahaan}'
                  AND b.flag_validasi = 'Y'
                  AND b.flag_validasi_PPIC = 'Y'
                  AND c.status IS NULL
                  AND a.bulan = MONTH(GETDATE())
                  AND a.tahun = YEAR(GETDATE())
                  {FilterBarang}

                UNION ALL

                -- =========================
                -- SOURCE 2
                -- =========================
                SELECT
                    '2' AS dari,
                    b.Kode_Barang,
                    bb.Tipe_Produk,
                    a.bulan,
                    a.tahun,
                    b.kode_Barang AS kode_fg,
                    d.kode_barang AS Kode_Bahan,
                    d.Kode_Stock_Owner,
                    d.Nilai_Barang,
                    d.satuan_barang,
                    f.Kode_Formula,
                    ISNULL((f.Jumlah), f.Jumlah) * bb.Berat / 1000 AS nilai_ppic,
                    --ISNULL((f.Jumlah - k.Qty_PO), f.Jumlah) * bb.Berat / 1000 AS nilai_ppic,
                    c.hasil AS nilai_Formula,
                    z.Kode_Perusahaan,
                    z.id_group_jenis,
                    z.Nama
                FROM emi_transaksi_sales_forecasting a
                INNER JOIN emi_transaksi_sales_forecasting_detail b
                    ON a.Kode_Perusahaan = b.kode_Perusahaan
                   AND a.no_faktur = b.no_faktur
                INNER JOIN barang bb
                    ON b.kode_perusahaan = bb.kode_perusahaan
                   AND b.kode_barang = bb.kode_barang
                   AND b.kode_stock_owner = bb.kode_stock_owner
                INNER JOIN N_EMI_Production_Plan_Schedule_Detail f
                    ON f.Kode_Perusahaan = b.Kode_Perusahaan
                   AND f.Urut_Production_Plan = b.urut
                INNER JOIN N_EMI_Production_Plan_Schedule g
                    ON f.Kode_Perusahaan = g.Kode_Perusahaan
                   AND f.No_Transaksi = g.No_Transaksi
                   AND g.Status IS NULL
                INNER JOIN emi_transaksi_formulator c
                    ON f.Kode_Perusahaan = c.Kode_Perusahaan
                   AND f.Kode_Formula = c.No_Faktur
                   AND c.Status IS NULL
                INNER JOIN emi_transaksi_formulator_detail_Bahan d
                    ON c.Kode_Perusahaan = d.Kode_Perusahaan
                   AND c.no_faktur = d.no_faktur
                INNER JOIN barang z
                    ON d.Kode_Perusahaan = z.Kode_Perusahaan
                   AND d.Kode_Barang = z.Kode_Barang
                   AND d.Kode_Stock_Owner = z.Kode_Stock_Owner
                INNER JOIN init e
                    ON a.kode_Perusahaan = e.kode_Perusahaan

                --OUTER APPLY (
                --    SELECT SUM(xyz.Jumlah) AS Qty_PO
                --    FROM EMI_Order_Produksi xyz
                --    WHERE xyz.Kode_Perusahaan = f.Kode_Perusahaan
                --      AND xyz.Urut_Production_Schedule = f.No_Urut
                --      AND xyz.Status IS NULL
                --) k

                WHERE a.status IS NULL
                  AND b.Kode_Perusahaan = '{KodePerusahaan}'
                  AND b.flag_validasi = 'Y'
                  AND b.flag_validasi_PPIC = 'Y'
                  AND c.status IS NULL
                  AND g.Status IS NULL
                  AND a.bulan = MONTH(GETDATE())
                  AND a.tahun = YEAR(GETDATE())
                  {FilterBarang}
            ),
            hasil_grouping_formula AS (
                SELECT
                    a.Kode_Perusahaan,
                    a.Kode_Barang,
                    a.Tipe_Produk,
                    a.Kode_Bahan,
                    a.satuan_barang,
                    a.bulan,
                    a.tahun,
                    ISNULL(ROUND(SUM(a.Nilai_Barang * (a.nilai_ppic / a.nilai_Formula)), 2), 0) AS Nilai,
                    b.satuan AS satuan_display,
                    CASE WHEN b.satuan IS NULL THEN 1 ELSE 0 END AS flag_satuan_kosong,
                    a.Nama
                FROM cte_b a
                INNER JOIN emi_group_jenis m
                    ON a.kode_perusahaan = m.kode_perusahaan
                   AND a.id_group_jenis = m.id_group_jenis
                LEFT JOIN Barang_Detail_Satuan b
                    ON b.kode_barang = a.Kode_Bahan
                   AND b.kode_perusahaan = '{KodePerusahaan}'
                   AND b.flag_tampil_display = 'Y'
                WHERE m.flag_raw_material = 'Y'
                GROUP BY
                    a.Tipe_Produk,
                    a.Kode_Perusahaan,
                    a.Kode_Bahan,
                    a.Nama,
                    a.satuan_barang,
                    a.bulan,
                    a.tahun,
                    b.satuan,
                    a.Kode_Barang,
                    a.Kode_Formula
            ),
            Pemakaian AS (
                SELECT
                    Kode_Perusahaan,
                    Kode_Barang,
                    Tipe_Produk,
                    Kode_Bahan,
                    Nama,
                    Satuan_barang,
                    Bulan,
                    a.tahun,
                    ROUND(SUM(a.Nilai), 2) AS Nilai,
                    satuan_display,
                    flag_satuan_kosong
                FROM hasil_grouping_formula a
                GROUP BY
                    Kode_Perusahaan,
                    Kode_Barang,
                    Tipe_Produk,
                    Kode_Bahan,
                    Nama,
                    Satuan_barang,
                    Bulan,
                    a.tahun,
                    satuan_display,
                    flag_satuan_kosong
            ),
            Pemakaian_Summary AS (
                SELECT
                    Kode_Perusahaan,
                    Kode_Bahan,
                    Nama,
                    MAX(Satuan_Barang) AS Satuan_Barang,
                    SUM(CASE WHEN Tipe_Produk = 'POUCH' THEN Nilai ELSE 0 END) AS Avg_Pouch,
                    SUM(CASE WHEN Tipe_Produk = 'CAN' THEN Nilai ELSE 0 END) AS Avg_Can,
                    SUM(Nilai) AS Avg_Total
                FROM Pemakaian
                WHERE Nilai <> 0
                GROUP BY
                    Kode_Perusahaan,
                    Kode_Bahan,
                    Nama
            )
            SELECT
                ps.Kode_Bahan,
                ps.Nama,
                ps.Avg_Pouch,
                ps.Avg_Can,
                ps.Avg_Total,
                ps.Satuan_Barang AS Satuan,
                CASE
                    WHEN sp.Flag_Jenis_Import = 'Y' THEN 'IMPORT'
                    WHEN sp.Flag_Jenis_Lokal = 'Y' THEN 'LOKAL'
                    ELSE '-'
                END AS Source,
                (pp.Waktu_Pabrikasi + pp.Waktu_Pengiriman) AS Lead_Time,
                ps.Avg_Total / ((pp.Waktu_Pabrikasi + pp.Waktu_Pengiriman) / 30.0) AS Min_Standby_Stock,
                (sg.Stock_Wharehouse + sg.Stock_Unloading) AS Stock,
                (sg.Stock_Wharehouse + sg.Stock_Unloading) / NULLIF(ps.Avg_Total / 26.0, 0) AS DOH,
                (
                    ps.Avg_Total / ((pp.Waktu_Pabrikasi + pp.Waktu_Pengiriman) / 30.0)
                    - (sg.Stock_Wharehouse + sg.Stock_Unloading)
                ) AS Stock_Kurang,
                SUM(mrp.OutStanding_PR) AS OS_PR,
                SUM(mrp.OutStanding_PO) AS OS_PO,
                (
                    ps.Avg_Total / ((pp.Waktu_Pabrikasi + pp.Waktu_Pengiriman) / 30.0)
                    - (sg.Stock_Wharehouse + sg.Stock_Unloading)
                    - SUM(mrp.OutStanding_PR)
                    - SUM(mrp.OutStanding_PO)
                ) AS Open_PR,
                NULLIF(
                    (
                        ISNULL(sg.Stock_Wharehouse + sg.Stock_Unloading, 0)
                        + ISNULL(SUM(mrp.OutStanding_PR), 0)
                        + ISNULL(SUM(mrp.OutStanding_PO), 0)
                    ) / NULLIF(ps.Avg_Total / 26.0, 0),
                    0
                ) AS DOH_Include_PR_PO
            FROM Pemakaian_Summary ps
            JOIN EMI_Detail_Proses_Pengiriman_PO pp
                ON pp.Kode_Perusahaan = ps.Kode_Perusahaan
               AND pp.Kode_Barang = ps.Kode_Bahan
            JOIN Suppliers_Kategori sp
                ON sp.Kode_Perusahaan = pp.Kode_Perusahaan
               AND sp.ID_Kategori_Suppliers = pp.Id_Kategori_Supplier
            JOIN N_Emi_View_Stock_Gantung sg
                ON sg.Kode_Barang = ps.Kode_Bahan
            JOIN N_EMI_View_Material_Requirement_Planning mrp
                ON mrp.Kode_Perusahaan = ps.Kode_Perusahaan
               AND mrp.Kode_Barang = ps.Kode_Bahan
            GROUP BY
                ps.Kode_Bahan,
                ps.Nama,
                ps.Avg_Pouch,
                ps.Avg_Can,
                ps.Avg_Total,
                ps.Satuan_Barang,
                pp.Waktu_Pabrikasi,
                pp.Waktu_Pengiriman,
                sp.Flag_Jenis_Import,
                sp.Flag_Jenis_Lokal,
                sg.Stock_Wharehouse,
                sg.Stock_Unloading;
        "

		Return SQL
	End Function

End Class