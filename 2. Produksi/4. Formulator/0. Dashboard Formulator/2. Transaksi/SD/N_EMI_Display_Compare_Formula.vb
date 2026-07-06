Imports System.IO

Public Class N_EMI_Display_Compare_Formula
    Public KodeBarang As String = ""

    Private Sub N_EMI_Display_Compare_Formula_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CMB_Filter.Items.Clear()

        CMB_Filter.Items.Add("Kode Barang")
        CMB_Filter.Items.Add("Nama Barang")

        CMB_Filter.SelectedIndex = 0

        With LV_Data

            .View = View.Details
            .FullRowSelect = True
            .GridLines = True
            .CheckBoxes = True

            .Columns.Clear()

            .Columns.Add("No Faktur", 120)
            .Columns.Add("Jenis Trial", 100)
            .Columns.Add("No Trial", 120)
            .Columns.Add("Kode Barang", 120)
            .Columns.Add("Nama Barang", 250)
            .Columns.Add("Hasil", 100, HorizontalAlignment.Right)
            .Columns.Add("Satuan", 60, HorizontalAlignment.Center)

        End With

        TB_Value.Text = KodeBarang
        Load_Data()
    End Sub

    Private Sub Load_Data()

        Try

            LV_Data.Items.Clear()

            OpenConn()

            SQL = $"
                SELECT *
                FROM
                (
                    SELECT
                        a.No_Faktur,
                        b.Kode_Barang,
                        b.Nama,
                        a.Hasil,
                        a.Satuan_Hasil,
                        'Trial Kitchen' AS Jenis_Trial,
                        tk.No_Transaksi AS No_Trial
                    FROM Emi_Transaksi_Formulator a
                    JOIN Barang b
                        ON a.Kode_Perusahaan = b.Kode_Perusahaan
                        AND a.Kode_Barang = b.Kode_Barang_Inq
                        AND a.Kode_Stock_Owner = b.Kode_Stock_Owner
                    OUTER APPLY (
                        SELECT TOP 1 y.No_Transaksi
                        FROM N_EMI_Transaksi_Trial_Order_Produksi x
                        JOIN N_EMI_Transaksi_Trial_Split_Production_Order y
                            ON y.Kode_Perusahaan = x.Kode_Perusahaan
                            AND y.No_PO = x.No_Faktur
                        WHERE x.Kode_Formula = a.No_Faktur
                        ORDER BY y.Tanggal DESC, y.Jam DESC
                    ) tk
                    WHERE
                        a.Kode_Perusahaan = '{KodePerusahaan}'
                        AND a.Status IS NULL
                        AND tk.No_Transaksi IS NOT NULL

                    UNION ALL

                    SELECT
                        a.No_Faktur,
                        b.Kode_Barang,
                        b.Nama,
                        a.Hasil,
                        a.Satuan_Hasil,
                        'Trial Produksi' AS Jenis_Trial,
                        tp.No_Transaksi AS No_Trial
                    FROM Emi_Transaksi_Formulator a
                    JOIN Barang b
                        ON a.Kode_Perusahaan = b.Kode_Perusahaan
                        AND a.Kode_Barang = b.Kode_Barang_Inq
                        AND a.Kode_Stock_Owner = b.Kode_Stock_Owner
                    OUTER APPLY (
                        SELECT TOP 1 y.No_Transaksi
                        FROM EMI_Order_Produksi x
                        JOIN Emi_Split_Production_Order y
                            ON y.Kode_Perusahaan = x.Kode_Perusahaan
                            AND y.No_PO = x.No_Faktur
                        WHERE
                            x.Kode_Formula = a.No_Faktur
                            AND x.Flag_Trial_Produksi = 'Y'
                        ORDER BY y.Tanggal DESC, y.Jam DESC
                    ) tp
                    WHERE
                        a.Kode_Perusahaan = '{KodePerusahaan}'
                        AND a.Status IS NULL
                        AND tp.No_Transaksi IS NOT NULL
                ) x
                WHERE 1 = 1
            "

            If TB_Value.Text.Trim <> "" Then

                If CMB_Filter.Text = "Kode Barang" Then

                    SQL &= $"
                    AND x.Kode_Barang LIKE '%{TB_Value.Text.Trim.Replace("'", "''")}%'
                "

                Else

                    SQL &= $"
                    AND x.Nama LIKE '%{TB_Value.Text.Trim.Replace("'", "''")}%'
                "

                End If

            End If

            SQL &= "
            ORDER BY
                x.No_Trial,
                x.Jenis_Trial
        "

            Using Ds = BindingTrans(SQL)

                With Ds.Tables("MyTable")

                    For i As Integer = 0 To .Rows.Count - 1

                        Dim LVI As ListViewItem

                        LVI = LV_Data.Items.Add(.Rows(i).Item("No_Faktur").ToString)
                        LVI.SubItems.Add(.Rows(i).Item("Jenis_Trial").ToString)
                        LVI.SubItems.Add(.Rows(i).Item("No_Trial").ToString)
                        LVI.SubItems.Add(.Rows(i).Item("Kode_Barang").ToString)
                        LVI.SubItems.Add(.Rows(i).Item("Nama").ToString)
                        LVI.SubItems.Add(Format(Val(.Rows(i).Item("Hasil")), "N4"))
                        LVI.SubItems.Add(.Rows(i).Item("Satuan_Hasil").ToString)

                    Next

                End With

            End Using

            CloseConn()

        Catch ex As Exception

            CloseConn()

            MessageBox.Show(
            "Gagal load data formula : " & ex.Message,
            Judul,
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        )

            Exit Sub

        End Try

    End Sub

    Private Sub BTN_Cari_Click(sender As Object, e As EventArgs) Handles BTN_Cari.Click
        Load_Data()
    End Sub

    Private Sub BTN_Refresh_Click(sender As Object, e As EventArgs) Handles BTN_Refresh.Click

        TB_Value.Clear()

        CMB_Filter.SelectedIndex = 0

        LV_Data.Items.Clear()

    End Sub

    Private Sub LV_Data_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles LV_Data.ItemCheck

        If e.NewValue <> CheckState.Checked Then Exit Sub

        Dim TotalCheck As Integer = 0
        Dim JenisPertama As String = ""

        For Each Item As ListViewItem In LV_Data.Items

            If Item.Checked Then

                TotalCheck += 1

                If JenisPertama = "" Then
                    JenisPertama = Item.SubItems(1).Text.Trim
                End If

            End If

        Next

        ' Maksimal 2 item
        If TotalCheck >= 2 Then

            MessageBox.Show(
                "Maksimal hanya boleh memilih 2 Formula.",
                Judul,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            e.NewValue = CheckState.Unchecked

            Exit Sub

        End If

        ' Cek jenis trial harus sama
        If JenisPertama <> "" Then

            Dim JenisDipilih As String =
            LV_Data.Items(e.Index).SubItems(1).Text.Trim

            If JenisPertama.ToUpper <> JenisDipilih.ToUpper Then

                MessageBox.Show(
                    "Formula yang dibandingkan harus memiliki jenis trial yang sama.",
                    Judul,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )

                e.NewValue = CheckState.Unchecked

                Exit Sub

            End If

        End If

    End Sub

    Private Sub BTN_Cetak_Click(sender As Object, e As EventArgs) Handles BTN_Cetak.Click

        Dim FormulaList As New List(Of String)
        Dim NoSplitList As New List(Of String)
        Dim JenisTrial As String = ""

        For Each Item As ListViewItem In LV_Data.Items

            If Item.Checked Then

                FormulaList.Add(Item.SubItems(0).Text)
                NoSplitList.Add(Item.SubItems(2).Text)
                JenisTrial = Item.SubItems(1).Text.Trim

            End If

        Next

        If FormulaList.Count <> 2 Then

            MessageBox.Show(
                "Pilih tepat 2 Formula.",
                Judul,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Exit Sub

        End If

        If JenisTrial = "Trial Kitchen" Then
            Dim formulas As New List(Of Dictionary(Of String, Object))

            If FormulaList.Count = 0 Or NoSplitList.Count = 0 Then
                MessageBox.Show("Data formula / split kosong.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Try
                OpenConn()

                For i As Integer = 0 To NoSplitList.Count - 1

                    Dim no_split As String = NoSplitList(i)
                    Dim no_formula As String = FormulaList(i)

                    '========================
                    ' RESET PER FORMULA (WAJIB)
                    '========================
                    Dim bahan As New List(Of Dictionary(Of String, Object))
                    Dim hpp As New Dictionary(Of String, Object)

                    Dim namaFormula As String = ""
                    Dim kategoriProduk As String = ""
                    Dim tanggalUji As String = ""
                    Dim tanggalValidasi As String = ""
                    Dim ketFormula As String = ""
                    Dim cookingStep As String = ""

                    '========================
                    ' COOKING STEP
                    '========================
                    SQL = $"
                        SELECT TOP 1 Cooking_Step 
                        FROM Emi_Transaksi_Formulator_Cooking_Steps
                        WHERE Kode_Perusahaan = '{KodePerusahaan}'
                          AND No_Faktur = '{no_formula}'
                          AND Status IS NULL
                    "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read() Then
                            cookingStep = Dr("Cooking_Step").ToString()
                        End If
                    End Using

                    '========================
                    ' KETERANGAN
                    '========================
                    SQL = $"
                        SELECT Keterangan 
                        FROM N_EMI_Transaksi_Trial_Split_Production_Order 
                        WHERE No_Transaksi = '{no_split}'
                    "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read() Then
                            ketFormula = Dr("Keterangan").ToString()
                        End If
                    End Using

                    '========================
                    ' HEADER FORMULA
                    '========================
                    SQL = $"
                        SELECT TOP 1 
                            a.kode_formula,
                            a.nama_produk,
                            FORMAT(b.tanggal, 'dd MMM yyyy') AS tanggal_uji,
                            FORMAT(CONVERT(DATETIME, 
                                CONVERT(VARCHAR(10), c.tanggal_selesai_trial_kitchen, 120) + ' ' + c.jam_selesai_trial_kitchen, 120),
                                'dd MMMM yyyy, HH:mm'
                            ) AS tanggal_validasi
                        FROM N_EMI_View_Laporan_Formula_Rpt a 
                        JOIN N_LIMS_PO_Sampel b 
                            ON a.Kode_Perusahaan = b.Kode_Perusahaan 
                           AND a.No_Transaksi = b.No_Split_Po
                        JOIN EMI_Transaksi_Formulator c 
                            ON c.Kode_Perusahaan = a.Kode_Perusahaan 
                           AND c.No_Faktur = a.Kode_Formula
                        WHERE a.No_Transaksi = '{no_split}'
                    "

                    Using Dr = OpenTrans(SQL)
                        If Dr.Read() Then
                            namaFormula = Dr("kode_formula").ToString()
                            kategoriProduk = Dr("nama_produk").ToString()
                            tanggalUji = Dr("tanggal_uji").ToString()
                            tanggalValidasi = Dr("tanggal_validasi").ToString()
                        End If
                    End Using

                    '========================
                    ' BAHAN MATERIAL
                    '========================
                    SQL = $"
                        SELECT Nama_bahan, Jumlah, Persentase, Est_HPP, Est_HPP_Per_Pcs 
                        FROM N_EMI_View_Laporan_Formula_Rpt 
                        WHERE No_Transaksi = '{no_split}'
                    "

                    Using Dr = OpenTrans(SQL)
                        While Dr.Read()
                            bahan.Add(New Dictionary(Of String, Object) From {
                                {"Nama_bahan", Dr("Nama_bahan")},
                                {"Jumlah", Dr("Jumlah")},
                                {"Persentase", Dr("Persentase")},
                                {"Est_HPP", Dr("Est_HPP")},
                                {"Est_HPP_Per_Pcs", Dr("Est_HPP_Per_Pcs")}
                            })
                        End While
                    End Using

                    '========================
                    ' HPP
                    '========================
                    SQL = $"
                        SELECT 
                            SUM(Est_HPP_Per_Pcs) AS hpp_bahan_baku,
                            HPP_Packaging,
                            HPP_Produksi,
                            satuan
                        FROM N_EMI_View_Laporan_Formula_Rpt 
                        WHERE No_Transaksi = '{no_split}'
                        GROUP BY HPP_Packaging, HPP_Produksi, satuan
                    "

                    Using Dr = OpenTrans(SQL)
                        If Dr.Read() Then
                            hpp("hpp_bahan_baku") = Dr("hpp_bahan_baku")
                            hpp("hpp_packaging") = Dr("hpp_packaging")
                            hpp("hpp_produksi") = Dr("hpp_produksi")
                            hpp("satuan") = "Per " & Dr("satuan").ToString()
                        End If
                    End Using

                    '========================
                    ' BUILD FORMULA ITEM
                    '========================
                    formulas.Add(New Dictionary(Of String, Object) From {
                        {"no_split", no_split},
                        {"nama_formula", namaFormula},
                        {"kategori_produk", kategoriProduk},
                        {"tanggal_uji", tanggalUji},
                        {"tanggal_validasi", tanggalValidasi},
                        {"keterangan", ketFormula},
                        {"cooking_step", cookingStep},
                        {"bahan", bahan},
                        {"hpp", hpp}
                    })

                Next

                CloseConn()

            Catch ex As Exception
                CloseConn()
                MessageBox.Show("Gagal generate laporan compare: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End Try

            'Hit API untuk mendapatkan file PDF laporan
            Try
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                Dim payload As New Dictionary(Of String, Object) From {
                    {"formulas", formulas}
                }

                Dim json As String = Newtonsoft.Json.JsonConvert.SerializeObject(payload)
                Dim headers As New Dictionary(Of String, String) From {{"X-Signature", GenerateHmac(json, Secret_Api_Laporan_Formulator)}}
                Dim pdfStream As MemoryStream = Helper_API.CallAPIFile(Url_Api_Laporan_Formulator_Compare, "POST", payload, headers)

                If pdfStream Is Nothing OrElse pdfStream.Length = 0 Then
                    Throw New Exception("PDF stream kosong")
                End If

                Dim tempPath As String = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() & ".pdf")

                Using file As New FileStream(tempPath, FileMode.Create, FileAccess.Write)
                    pdfStream.CopyTo(file)
                End Using

                Dim frm As New N_EMI_PDF_Viewer(tempPath, "Laporan Compare Formula Trial Kitchen")
                frm.ShowDialog()

                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show($"Gagal mendapatkan laporan compare formula: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End Try
        ElseIf JenisTrial = "Trial Produksi" Then
            Dim formulas As New List(Of Dictionary(Of String, Object))

            If FormulaList.Count = 0 Or NoSplitList.Count = 0 Then
                MessageBox.Show("Data formula / split kosong.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Try
                OpenConn()

                For i As Integer = 0 To NoSplitList.Count - 1
                    Dim no_split As String = NoSplitList(i)
                    Dim no_formula As String = FormulaList(i)

                    '========================
                    ' RESET PER FORMULA (WAJIB)
                    '========================
                    Dim bahan As New List(Of Dictionary(Of String, Object))
                    Dim hpp As New Dictionary(Of String, Object)

                    Dim namaFormula As String = ""
                    Dim kategoriProduk As String = ""
                    Dim tanggalUji As String = ""
                    Dim tanggalValidasi As String = ""
                    Dim ketFormula As String = ""
                    Dim cookingStep As String = ""

                    SQL = $"
                        SELECT TOP 1 Cooking_Step 
                        FROM Emi_Transaksi_Formulator_Cooking_Steps
                        WHERE Kode_Perusahaan = '{KodePerusahaan}'
                          AND No_Faktur = '{no_formula}'
                          AND Status IS NULL
                          AND Flag_Trial_Produksi = 'Y'
                          AND Flag_Trial_Kitchen IS NULL
                        ORDER BY Urut_Oto DESC
                    "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read() Then
                            cookingStep = Dr("Cooking_Step").ToString()
                        End If
                    End Using

                    SQL = $"SELECT Keterangan FROM Emi_Split_Production_Order WHERE No_Transaksi = '{no_split}'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read() Then
                            ketFormula = Dr("Keterangan").ToString()
                        End If
                    End Using

                    SQL = $"select c.No_Faktur as kode_formula, b.Nama as nama_produk, format(a.Tanggal, 'dd MMM yyyy') as tanggal_uji, FORMAT(CONVERT(DATETIME, CONVERT(VARCHAR(10), c.Tanggal_Selesai_Trial_Produksi, 120) + ' ' + c.Jam_Selesai_Trial_Produksi, 120), 'dd MMMM yyyy, HH:mm') AS tanggal_validasi
				        from N_EMI_LAB_PO_Sampel a
				        cross apply (
					        select top 1 *
					        from Barang b
					        where a.Kode_Perusahaan = b.Kode_Perusahaan
					        and a.Kode_Barang = b.Kode_Barang
					        order by b.Kode_Stock_Owner
				        ) b
				        join Emi_Transaksi_Formulator c 
					        on c.Kode_Perusahaan = a.Kode_Perusahaan 
					        and c.Kode_Barang = b.Kode_Barang_Inq
					        and c.Status is null
					        and c.No_Faktur = '{no_formula}'
				        where a.No_Split_Po = '{no_split}' and a.Flag_Trial_Produksi = 'Y'
			        "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read() Then
                            namaFormula = Dr("kode_formula").ToString()
                            kategoriProduk = Dr("nama_produk").ToString()
                            tanggalUji = Dr("tanggal_uji").ToString()
                            tanggalValidasi = Dr("tanggal_validasi").ToString()
                        End If
                    End Using

                    SQL = $"
                        WITH cte_bahan AS (
                            SELECT 
                                a.Kode_Barang,
                                a.Kode_Bahan,
                                a.Jumlah_Barang,

                                (
                                    ISNULL(
                                        (
                                            SELECT TOP (1)
                                                dbo.get_hpp(x.serial_number)
                                            FROM barang_sn x
                                            WHERE x.kode_barang = a.kode_bahan
                                              AND x.blok_sn IS NULL
                                              AND dbo.get_hpp(x.serial_number) <> 0
                                            ORDER BY x.Tgl_masuk DESC
                                        ),
                                        b.estimasi_harga
                                    )
                                ) / NULLIF(a.Jumlah_Barang, 0) AS hpp

                            FROM Barang_Detail_Bahan_Penolong a

                            INNER JOIN Barang b
                                ON a.Kode_Bahan = b.Kode_Barang

                            GROUP BY a.Kode_Bahan, a.Kode_Barang, a.Jumlah_Barang, b.Estimasi_Harga
                        ),

                        cte_wc AS (
                            SELECT 
                                a.Kode_Perusahaan,
                                a.Kode_Jenis_Biaya_Produksi,

                                (
                                    SELECT TOP (1) x.no_faktur
                                    FROM Emi_Transaksi_Work_Center x
                                    WHERE x.status IS NULL
                                      AND x.Kode_Perusahaan = a.Kode_Perusahaan
                                      AND x.jenis_biaya = a.Kode_Jenis_Biaya_Produksi
                                    ORDER BY x.id DESC
                                ) AS Faktur_WC

                            FROM Emi_Jenis_Biaya_Produksi a
                        ),

                        cte_produksi AS (
                            SELECT 
                                c.Id_Routing,
                                c.id_work_center,
                                MAX(c.Nilai_Per_pcs) AS Nilai_Per_Kg

                            FROM cte_wc a

                            JOIN Emi_Transaksi_Work_Center b
                                ON a.Kode_Perusahaan = b.Kode_Perusahaan
                               AND a.Faktur_WC = b.No_Faktur

                            JOIN Emi_Transaksi_Work_Center_detail c
                                ON b.Kode_Perusahaan = c.Kode_Perusahaan
                               AND b.No_Faktur = c.No_Faktur

                            GROUP BY 
                                c.Id_Routing,
                                c.id_work_center
                        )

                        SELECT 
                            d.Satuan,

                            ISNULL((
                                SELECT SUM(x.hpp)
                                FROM cte_bahan x
                                WHERE x.Kode_Barang = b.Kode_Barang
                            ), 0) AS HPP_Packaging,

                            ISNULL((
                                SELECT SUM(x.Nilai_Per_Kg) / 1000 * d.Berat
                                FROM cte_produksi x
                                WHERE d.Id_Routing = x.Id_Routing
                            ), 0) AS HPP_Produksi,

                            ISNULL((
                                SELECT SUM(ISNULL(x.Est_HPP_Per_Pcs, 0))
                                FROM EMI_Transaksi_Formulator_Detail_Bahan x
                                WHERE x.Kode_Perusahaan = b.Kode_Perusahaan
                                  AND x.No_Faktur = b.No_Faktur
                            ), 0) AS HPP_Bahan_Baku

                        FROM Emi_Transaksi_Formulator b

                        JOIN Barang d
                            ON b.Kode_Perusahaan = d.Kode_Perusahaan
                            AND b.Kode_Barang = d.Kode_Barang_inq
                            AND b.Kode_Stock_Owner = d.Kode_Stock_Owner

                        WHERE b.No_Faktur = '{no_formula}'

                        GROUP BY 
                            b.No_Faktur,
                            b.Kode_Perusahaan,
                            b.Kode_Barang,
                            d.Nama,
                            d.Satuan,
                            d.Berat,
                            d.Id_Routing
                    "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read() Then
                            hpp("hpp_bahan_baku") = Dr("HPP_Bahan_Baku")
                            hpp("hpp_packaging") = Dr("HPP_Packaging")
                            hpp("hpp_produksi") = Dr("HPP_Produksi")
                            hpp("satuan") = Dr("Satuan")
                        End If
                    End Using

                    SQL = $"
                            SELECT
                                e.Nama AS Nama_Bahan,
                                c.Jumlah,
                                c.Persentase,
                                ISNULL(c.Est_HPP, 0) AS Est_HPP,
                                ISNULL(c.Est_HPP_Per_Pcs, 0) AS Est_HPP_Per_Pcs

                            FROM Emi_Transaksi_Formulator b 

                            INNER JOIN EMI_Transaksi_Formulator_Detail_Bahan c
                                ON b.Kode_Perusahaan = c.Kode_Perusahaan
                                AND b.No_Faktur = c.No_Faktur

                            INNER JOIN Barang d
                                ON b.Kode_Perusahaan = d.Kode_Perusahaan
                                AND b.Kode_Stock_Owner = d.Kode_Stock_Owner
                                AND b.Kode_Barang = d.Kode_Barang_Inq

                            INNER JOIN Barang e
                                ON c.Kode_Perusahaan = e.Kode_Perusahaan
                                AND c.Kode_Stock_Owner = e.Kode_Stock_Owner
                                AND c.Kode_Barang = e.Kode_Barang

                            WHERE
                                b.Status IS NULL
                                AND b.Kode_Perusahaan = '{KodePerusahaan}'
                                AND b.No_Faktur = '{no_formula}'
                    "
                    Using Dr = OpenTrans(SQL)
                        Do While Dr.Read()
                            bahan.Add(New Dictionary(Of String, Object) From {
                            {"Nama_bahan", Dr("Nama_bahan")},
                            {"Jumlah", Dr("Jumlah")},
                            {"Persentase", Dr("Persentase")},
                            {"Est_HPP_Per_Pcs", Dr("Est_HPP_Per_Pcs")},
                            {"Est_HPP", Dr("Est_HPP")}
                        })
                        Loop
                    End Using

                    '========================
                    ' BUILD FORMULA ITEM
                    '========================
                    formulas.Add(New Dictionary(Of String, Object) From {
                        {"no_split", no_split},
                        {"nama_formula", namaFormula},
                        {"kategori_produk", kategoriProduk},
                        {"tanggal_uji", tanggalUji},
                        {"tanggal_validasi", tanggalValidasi},
                        {"keterangan", ketFormula},
                        {"cooking_step", cookingStep},
                        {"bahan", bahan},
                        {"hpp", hpp}
                    })
                Next

                CloseConn()

            Catch ex As Exception
                CloseConn()
                MessageBox.Show("Gagal generate laporan compare: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End Try

            'Hit API untuk mendapatkan file PDF laporan
            Try
                Me.Cursor = Cursors.WaitCursor
                Application.DoEvents()

                Dim payload As New Dictionary(Of String, Object) From {
                    {"formulas", formulas}
                }

                Dim json As String = Newtonsoft.Json.JsonConvert.SerializeObject(payload)
                Dim headers As New Dictionary(Of String, String) From {{"X-Signature", GenerateHmac(json, Secret_Api_Laporan_Formulator)}}
                Dim pdfStream As MemoryStream = Helper_API.CallAPIFile(Url_Api_Laporan_Formulator_Trial_Produksi_Compare, "POST", payload, headers)

                If pdfStream Is Nothing OrElse pdfStream.Length = 0 Then
                    Throw New Exception("PDF stream kosong")
                End If

                Dim tempPath As String = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() & ".pdf")

                Using file As New FileStream(tempPath, FileMode.Create, FileAccess.Write)
                    pdfStream.CopyTo(file)
                End Using

                Dim frm As New N_EMI_PDF_Viewer(tempPath, "Laporan Compare Formula Trial Produksi")
                frm.ShowDialog()

                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show($"Gagal mendapatkan laporan compare formula: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End Try
        Else
            MessageBox.Show("Jenis laporan tidak valid.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
    End Sub

End Class