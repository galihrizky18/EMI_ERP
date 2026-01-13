Public Class N_EMI_Purchase_Requisition_Penawaran
    Dim Lv_ChkBox, Lv_No_Urut, Lv_No_Pr, Lv_Kd_Barang, Lv_Nm_Barang, Lv_Nm_Supplier, Lv_No_Penawaran, Lv_Mata_Uang, Lv_Harga, Lv_Keterangan_PR, Lv_Keterangan As String

    Dim item_ChkBox As Integer = 0
    Dim item_No_Urut As Integer = 1
    Dim item_No_PR As Integer = 2
    Dim item_Kd_Barang As Integer = 3
    Dim item_Nm_Barang As Integer = 4
    Dim item_nm_Supllier As Integer = 5
    Dim item_No_Penawaran As Integer = 6
    Dim item_Mata_Uang As Integer = 7
    Dim item_Harga As Integer = 8
    Dim item_Keterangan_PR As Integer = 9
    Dim item_Keterangan As Integer = 10

    Dim item2_Chekbox As Integer = 0
    Dim item2_No_Urut As Integer = 1
    Dim item2_No_PR As Integer = 2
    Dim item2_Kd_Barang As Integer = 3
    Dim item2_Nm_Barang As Integer = 4
    Dim item2_No_Penawaran As Integer = 6
    Dim item2_Mata_Uang As Integer = 7
    Dim item2_Harga As Integer = 8
    Dim item2_Keterangan_PR As Integer = 9
    Dim item2_Keterangan As Integer = 10

    Private Sub N_EMI_Purchase_Requisition_Penawaran_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TabControl1.SelectedTab = TabPage2
    End Sub

    Private Sub Get_Lv_Data(ByVal index As Integer)
        Lv_ChkBox = DataGridView1.Rows(index).Cells(item_ChkBox).Value
        Lv_No_Urut = DataGridView1.Rows(index).Cells(item_No_Urut).Value
        Lv_No_Pr = DataGridView1.Rows(index).Cells(item_No_PR).Value
        Lv_Kd_Barang = DataGridView1.Rows(index).Cells(item_Kd_Barang).Value
        Lv_Nm_Barang = DataGridView1.Rows(index).Cells(item_Nm_Barang).Value
        Lv_Nm_Supplier = DataGridView1.Rows(index).Cells(item_nm_Supllier).Value
        Lv_No_Penawaran = DataGridView1.Rows(index).Cells(item_No_Penawaran).Value
        Lv_Mata_Uang = DataGridView1.Rows(index).Cells(item_Mata_Uang).Value
        Lv_Harga = DataGridView1.Rows(index).Cells(item_Harga).Value
        Lv_Keterangan_PR = DataGridView1.Rows(index).Cells(item_Keterangan_PR).Value
        Lv_Keterangan = DataGridView1.Rows(index).Cells(item_Keterangan).Value
    End Sub

    Public Sub Fetch_PR_Offered()
        Try
            OpenConn()
            Dim SQL As String = "
            SELECT b.No_Urut, a.No_Faktur AS No_PR, b.Kode_Barang, c.Nama AS Nama_Barang,
                   b.No_Penawaran, b.Keterangan, b.Keterangan_Penawaran, b.Jumlah AS Qty,
                   f.Kode_Supplier, f.Nama AS Nama_Supplier, d.Harga_Satuan, d.Mata_Uang, e.Periode_Akhir_Penawaran
            FROM EMI_Purchase_Requisition a
            JOIN EMI_Purchase_Requisition_Detail b 
                ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Faktur = b.No_Faktur
            JOIN Barang c 
                ON b.Kode_Barang = c.Kode_Barang AND c.Kode_Perusahaan = b.Kode_Perusahaan 
                AND c.Kode_Stock_Owner = b.Kode_Stock_Owner
            JOIN EMI_Master_Penawaran_Detail d 
                ON d.No_Faktur = b.No_Penawaran AND d.Kode_Perusahaan = b.Kode_Perusahaan
                AND d.Kode_Barang = b.Kode_Barang
            JOIN EMI_Master_Penawaran e
                ON e.No_Faktur = d.No_Faktur AND e.Kode_Perusahaan = b.Kode_Perusahaan
            JOIN Suppliers f ON f.Kode_Supplier = e.Kode_Supplier
            WHERE a.Kode_Perusahaan = @KodePerusahaan AND b.Flag_Sudah_PO IS NULL 
                AND a.Status IS NULL AND b.No_Penawaran IS NOT NULL
                AND b.Flag_Pengajuan_Selesai IS NULL"

            If Not String.IsNullOrWhiteSpace(ComboBox_Filter.Text) AndAlso Not String.IsNullOrWhiteSpace(Filter_Text.Text) Then
                Select Case ComboBox_Filter.Text
                    Case "No PR"
                        SQL &= " AND a.No_Faktur LIKE @FilterValue"
                    Case "Kode Barang"
                        SQL &= " AND b.Kode_Barang LIKE @FilterValue"
                    Case "Nama Barang"
                        SQL &= " AND c.Nama LIKE @FilterValue"
                    Case "No Penawaran"
                        SQL &= " AND b.No_Penawaran LIKE @FilterValue"
                    Case "Kode Supplier"
                        SQL &= " AND e.Kode_Supplier LIKE @FilterValue"
                    Case "Nama Supplier"
                        SQL &= " AND f.Nama LIKE @FilterValue"
                    Case "Mata Uang"
                        SQL &= " AND d.Mata_Uang LIKE @FilterValue"
                End Select
            End If

            SQL &= " ORDER BY
                a.No_Faktur,
                b.Kode_Barang,
                c.Nama,
                f.Kode_Supplier,
                f.Nama
            "

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)

            If Not String.IsNullOrWhiteSpace(ComboBox_Filter.Text) AndAlso Not String.IsNullOrWhiteSpace(Filter_Text.Text) Then
                Cmd.Parameters.AddWithValue("@FilterValue", "%" & Filter_Text.Text & "%")
            End If

            DataGridView1.SuspendLayout()
            DataGridView1.Rows.Clear()
            Dim hariIni As DateTime = DateTime.Now.Date
            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    DataGridView1.Rows.Add(
                    False,
                    Dr("No_Urut"),
                    Dr("No_PR"),
                    Dr("Kode_Barang"),
                    Dr("Nama_Barang"),
                    Dr("Nama_Supplier"),
                    Dr("No_Penawaran"),
                    Dr("Mata_Uang"),
                    Dr("Harga_Satuan"),
                    Dr("Keterangan"),
                    Dr("Keterangan_Penawaran"))
                    If Not IsDBNull(Dr("Periode_Akhir_Penawaran")) Then
                        Dim periodeAkhir As DateTime = CDate(Dr("Periode_Akhir_Penawaran")).Date
                        If hariIni > periodeAkhir Then
                            Dim rowIndex As Integer = DataGridView1.Rows.Count - 1
                            DataGridView1.Rows(rowIndex).DefaultCellStyle.BackColor = Color.Red
                            DataGridView1.Rows(rowIndex).DefaultCellStyle.ForeColor = Color.White
                        End If
                    End If
                End While
            End Using
            DataGridView1.ResumeLayout()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Public Sub Fetch_PR_Waiting_Offer()
        Try
            OpenConn()

            Dim SQL As String = "
            SELECT
                b.No_Urut,
                a.No_Faktur AS No_PR,
                b.Kode_Barang,
                c.Nama AS Nama_Barang,
                b.Jumlah AS Qty,
                b.Keterangan,
                b.Keterangan_Penawaran,
                p.No_Penawaran,
                p.Mata_Uang,
                p.Harga_Satuan,
                p.Kode_Supplier,
                p.Nama_Supplier,
                CASE
                    WHEN p.No_Penawaran IS NOT NULL THEN 0
                    ELSE 1
                END AS Sort_Order
            FROM EMI_Purchase_Requisition a
            INNER JOIN EMI_Purchase_Requisition_Detail b
                ON a.Kode_Perusahaan = b.Kode_Perusahaan
               AND a.No_Faktur       = b.No_Faktur
            INNER JOIN Barang c
                ON b.Kode_Barang      = c.Kode_Barang
               AND c.Kode_Perusahaan  = b.Kode_Perusahaan
               AND c.Kode_Stock_Owner = b.Kode_Stock_Owner
            LEFT JOIN (
                SELECT
                    f.Kode_Perusahaan,
                    f.Kode_Barang,
                    f.Harga_Satuan,
                    f.Mata_Uang,
                    f.No_Faktur AS No_Penawaran,
                    g.Kode_Supplier,
                    g.Nama AS Nama_Supplier
                FROM EMI_Master_Penawaran_Detail f
                INNER JOIN EMI_Master_Penawaran e
                    ON f.Kode_Perusahaan = e.Kode_Perusahaan
                   AND f.No_Faktur       = e.No_Faktur
                INNER JOIN Suppliers g
                    ON g.Kode_Perusahaan = e.Kode_Perusahaan
                    AND g.Kode_Supplier = e.Kode_Supplier
                WHERE e.Selesai IS NULL
                  AND e.Flag_Release = 'Y'
                  AND e.Status IS NULL
                  AND CAST(e.Periode_Akhir_Penawaran AS DATE) >= CAST(GETDATE() AS DATE)
            ) p
                ON p.Kode_Perusahaan = b.Kode_Perusahaan
               AND p.Kode_Barang     = b.Kode_Barang
            WHERE a.Kode_Perusahaan = @KodePerusahaan
              AND a.Status IS NULL
              AND a.Flag_Release = 'Y'
              AND b.Flag_Sudah_PO IS NULL
              AND b.No_Penawaran IS NULL
              AND b.Flag_Pengajuan_Selesai IS NULL"

            If Not String.IsNullOrWhiteSpace(ComboBox_Filter.Text) AndAlso Not String.IsNullOrWhiteSpace(Filter_Text.Text) Then
                Select Case ComboBox_Filter.Text
                    Case "No PR"
                        SQL &= " AND a.No_Faktur LIKE @FilterValue"
                    Case "Kode Barang"
                        SQL &= " AND b.Kode_Barang LIKE @FilterValue"
                    Case "Nama Barang"
                        SQL &= " AND c.Nama LIKE @FilterValue"
                    Case "No Penawaran"
                        SQL &= " AND p.No_Penawaran LIKE @FilterValue"
                    Case "Kode Supplier"
                        SQL &= " AND p.Kode_Supplier LIKE @FilterValue"
                    Case "Nama Supplier"
                        SQL &= " AND p.Nama_Supplier LIKE @FilterValue"
                    Case "Mata Uang"
                        SQL &= " AND p.Mata_Uang LIKE @FilterValue"
                End Select
            End If

            SQL &= " ORDER BY
                Sort_Order,
                a.No_Faktur,
                b.Kode_Barang,
                c.Nama,
                p.Kode_Supplier,
                p.Nama_Supplier
                "

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)

            If Not String.IsNullOrWhiteSpace(ComboBox_Filter.Text) AndAlso Not String.IsNullOrWhiteSpace(Filter_Text.Text) Then
                Cmd.Parameters.AddWithValue("@FilterValue", "%" & Filter_Text.Text & "%")
            End If

            DataGridView2.SuspendLayout()
            DataGridView2.Rows.Clear()
            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    DataGridView2.Rows.Add(
                    False, Dr("No_Urut"), Dr("No_PR"), Dr("Kode_Barang"), Dr("Nama_Barang"),
                    Dr("Nama_Supplier"), Dr("No_Penawaran"), Dr("Mata_Uang"), Dr("Harga_Satuan"),
                    Dr("Keterangan"), Dr("Keterangan_Penawaran"))
                End While
            End Using
            DataGridView2.ResumeLayout()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If ComboBox_Filter.SelectedIndex = -1 Then

            MessageBox.Show(
            "Filter tidak valid." & vbCrLf &
            "Silakan pilih filter terlebih dahulu.",
            "Informasi",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        )

            Exit Sub
        End If

        If TabControl1.SelectedTab Is TabPage1 Then
            Fetch_PR_Offered()
        ElseIf TabControl1.SelectedTab Is TabPage2 Then
            Fetch_PR_Waiting_Offer()
        End If
    End Sub

    Private Sub DataGridView2_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles DataGridView2.CurrentCellDirtyStateChanged
        If DataGridView2.IsCurrentCellDirty AndAlso TypeOf DataGridView2.CurrentCell Is DataGridViewCheckBoxCell Then
            DataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub DataGridView2_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellValueChanged
        If e.RowIndex < 0 OrElse e.ColumnIndex <> 0 Then Return

        Dim changedRow = DataGridView2.Rows(e.RowIndex)
        Dim isChecked As Boolean = Convert.ToBoolean(If(changedRow.Cells(0).Value, False))

        If isChecked Then
            Dim currentNoPR As String = Convert.ToString(If(changedRow.Cells(2).Value, String.Empty))
            Dim currentKodeBarang As String = Convert.ToString(If(changedRow.Cells(3).Value, String.Empty))

            For i As Integer = 0 To DataGridView2.Rows.Count - 1
                If i <> e.RowIndex Then
                    Dim rowNoPR As String = Convert.ToString(If(DataGridView2.Rows(i).Cells(2).Value, String.Empty))
                    Dim rowKodeBarang As String = Convert.ToString(If(DataGridView2.Rows(i).Cells(3).Value, String.Empty))

                    If rowNoPR = currentNoPR AndAlso rowKodeBarang = currentKodeBarang Then
                        DataGridView2.Rows(i).Cells(0).Value = False
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles BtnSimpan.Click
        Try
            OpenConn()
            Dim cntUpdated As Integer = 0
            Dim tanggal As String = Format(DateTime.Now, "yyyy-MM-dd")
            Dim jam As String = Format(DateTime.Now, "HH:mm:ss")

            For i As Integer = 0 To DataGridView2.Rows.Count - 1
                If Not CBool(DataGridView2.Rows(i).Cells(0).Value) Then Continue For

                Dim noPenawaran = DataGridView2.Rows(i).Cells(6).Value
                If noPenawaran Is Nothing OrElse noPenawaran.ToString.Trim = "" Then Continue For

                Dim noUrut = DataGridView2.Rows(i).Cells(1).Value
                Dim noPR = DataGridView2.Rows(i).Cells(2).Value
                Dim kodeBarang = DataGridView2.Rows(i).Cells(3).Value
                Dim keterangan = If(IsDBNull(DataGridView2.Rows(i).Cells(10).Value), DBNull.Value, DataGridView2.Rows(i).Cells(10).Value.ToString().Trim())

                Dim sqlSelect = "SELECT No_Penawaran, Keterangan_Penawaran FROM EMI_Purchase_Requisition_Detail WHERE No_Urut = @NoUrut AND Kode_Barang = @KodeBarang"
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@NoUrut", noUrut)
                Cmd.Parameters.AddWithValue("@KodeBarang", kodeBarang)

                Dim noPenawaranLama As String = ""
                Dim keteranganLama As String = ""

                Using drOld = OpenTrans(sqlSelect)
                    If drOld.Read Then
                        noPenawaranLama = If(drOld("No_Penawaran") IsNot Nothing, drOld("No_Penawaran").ToString, "")
                        keteranganLama = If(drOld("Keterangan_Penawaran") IsNot Nothing, drOld("Keterangan_Penawaran").ToString, "")
                    End If
                End Using

                Dim sqlUpdate = "UPDATE EMI_Purchase_Requisition_Detail SET Status_Validasi = 'Y', No_Penawaran = @NoPenawaran, Keterangan_Penawaran = @Keterangan_Penawaran WHERE No_Urut = @NoUrut AND Kode_Barang = @KodeBarang"
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@NoPenawaran", noPenawaran)
                Cmd.Parameters.AddWithValue("@Keterangan_Penawaran", keterangan)
                Cmd.Parameters.AddWithValue("@NoUrut", noUrut)
                Cmd.Parameters.AddWithValue("@KodeBarang", kodeBarang)
                Cmd.CommandText = sqlUpdate
                Cmd.ExecuteNonQuery()
                cntUpdated += 1

                InsertLog(KodePerusahaan, tanggal, jam, UserID, noPR, noUrut, noPenawaranLama, noPenawaran, keteranganLama, keterangan)
            Next

            CloseConn()

            If cntUpdated > 0 Then
                Fetch_PR_Offered()
                Fetch_PR_Waiting_Offer()
                MessageBox.Show($"Berhasil menambahkan {cntUpdated} data penawaran!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub InsertLog(kodePerusahaan, tanggal, jam, userId, noFaktur, noUrut, noPenawaranLama, noPenawaranBaru, keteranganLama, keteranganBaru)
        Dim sqlLog = "INSERT INTO N_EMI_LOG_Purchase_Requisition_Detail (Kode_Perusahaan, Tanggal, Jam, UserID, No_Faktur, No_Urut, No_Penawaran_Lama, No_Penawaran_Baru, Keterangan_Lama, Keterangan_Baru) VALUES (@KodePerusahaan, @Tanggal, @Jam, @UserID, @NoFaktur, @NoUrut, @NoPenawaranLama, @NoPenawaranBaru, @KeteranganLama, @KeteranganBaru)"
        Cmd.Parameters.Clear()
        Cmd.Parameters.AddWithValue("@KodePerusahaan", kodePerusahaan)
        Cmd.Parameters.AddWithValue("@Tanggal", tanggal)
        Cmd.Parameters.AddWithValue("@Jam", jam)
        Cmd.Parameters.AddWithValue("@UserID", userId)
        Cmd.Parameters.AddWithValue("@NoFaktur", noFaktur)
        Cmd.Parameters.AddWithValue("@NoUrut", noUrut)
        Cmd.Parameters.AddWithValue("@NoPenawaranLama", If(String.IsNullOrEmpty(noPenawaranLama), DBNull.Value, noPenawaranLama))
        Cmd.Parameters.AddWithValue("@NoPenawaranBaru", noPenawaranBaru)
        Cmd.Parameters.AddWithValue("@KeteranganLama", If(String.IsNullOrEmpty(keteranganLama), DBNull.Value, keteranganLama))
        Cmd.Parameters.AddWithValue("@KeteranganBaru", keteranganBaru)
        Cmd.CommandText = sqlLog
        Cmd.ExecuteNonQuery()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        ComboBox_Filter.SelectedIndex = -1
        Filter_Text.Text = ""

        Try
            If TabControl1.SelectedTab Is TabPage1 Then
                Fetch_PR_Offered()
                BtnBatalkan.Visible = True
                BtnSimpan.Visible = False
            ElseIf TabControl1.SelectedTab Is TabPage2 Then
                Fetch_PR_Waiting_Offer()
                BtnBatalkan.Visible = False
                BtnSimpan.Visible = True
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        ComboBox_Filter.SelectedIndex = -1
        Filter_Text.Text = ""

        If TabControl1.SelectedTab Is TabPage1 Then
            Fetch_PR_Offered()
        ElseIf TabControl1.SelectedTab Is TabPage2 Then
            Fetch_PR_Waiting_Offer()
        End If
    End Sub

    Private Sub BtnBatalkan_Click(sender As Object, e As EventArgs) Handles BtnBatalkan.Click
        Dim cntChecked As Integer = DataGridView1.Rows.Cast(Of DataGridViewRow)().Count(Function(r) CBool(r.Cells(0).Value))

        If cntChecked = 0 Then
            MessageBox.Show("Pilih minimal 1 data untuk dibatalkan!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If MessageBox.Show($"Yakin ingin membatalkan {cntChecked} data penawaran?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Exit Sub
        End If

        Try
            OpenConn()
            Dim cntUpdated As Integer = 0
            Dim tanggal As String = Format(DateTime.Now, "yyyy-MM-dd")
            Dim jam As String = Format(DateTime.Now, "HH:mm:ss")

            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                If Not CBool(DataGridView1.Rows(i).Cells(0).Value) Then Continue For

                Dim noUrut As String = If(IsDBNull(DataGridView1.Rows(i).Cells(1).Value), "", DataGridView1.Rows(i).Cells(1).Value.ToString().Trim())
                Dim noPR As String = If(IsDBNull(DataGridView1.Rows(i).Cells(2).Value), "", DataGridView1.Rows(i).Cells(2).Value.ToString().Trim())
                Dim kodeBarang As String = If(IsDBNull(DataGridView1.Rows(i).Cells(3).Value), "", DataGridView1.Rows(i).Cells(3).Value.ToString().Trim())

                If String.IsNullOrEmpty(noUrut) OrElse String.IsNullOrEmpty(kodeBarang) Then Continue For

                Dim sqlSelect = "SELECT No_Penawaran, Keterangan_Penawaran FROM EMI_Purchase_Requisition_Detail WHERE No_Urut = @NoUrut AND Kode_Barang = @KodeBarang"
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@NoUrut", noUrut)
                Cmd.Parameters.AddWithValue("@KodeBarang", kodeBarang)

                Dim noPenawaranLama As String = ""
                Dim keteranganLama As String = ""

                Using drOld = OpenTrans(sqlSelect)
                    If drOld.Read Then
                        noPenawaranLama = If(IsDBNull(drOld("No_Penawaran")), "", drOld("No_Penawaran").ToString().Trim())
                        keteranganLama = If(IsDBNull(drOld("Keterangan_Penawaran")), "", drOld("Keterangan_Penawaran").ToString().Trim())
                    End If
                End Using

                Dim sqlUpdate = "UPDATE EMI_Purchase_Requisition_Detail SET Status_Validasi = 'T', No_Penawaran = NULL, Keterangan_Penawaran = NULL WHERE No_Urut = @NoUrut AND Kode_Barang = @KodeBarang"
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@NoUrut", noUrut)
                Cmd.Parameters.AddWithValue("@KodeBarang", kodeBarang)
                Cmd.CommandText = sqlUpdate
                Cmd.ExecuteNonQuery()
                cntUpdated += 1

                Dim keteranganBaru = If(IsDBNull(DataGridView1.Rows(i).Cells(10).Value), DBNull.Value, DataGridView1.Rows(i).Cells(10).Value.ToString().Trim())
                InsertLog(KodePerusahaan, tanggal, jam, UserID, noPR, noUrut, noPenawaranLama, DBNull.Value, keteranganLama, keteranganBaru)
            Next

            CloseConn()

            If cntUpdated > 0 Then
                Fetch_PR_Offered()
                MessageBox.Show($"Berhasil membatalkan {cntUpdated} data penawaran!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub PengajuanSelesaiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PengajuanSelesaiToolStripMenuItem.Click
        If DataGridView1.Rows.Count = 0 Then Exit Sub
        Dim currentRow = DataGridView1.CurrentRow.Index
        Dim currentCell = DataGridView1.CurrentCellAddress.X

        Try
            OpenConn()

            If CekButtonRole("Penyelesaian_PR_Offered") = "T" Then
                MessageBox.Show("User Tidak Ada Akses Pengajuan Selesai PR", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If (MessageBox.Show("Yakin ingin melakukan penyelesaian PR ini??", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = vbNo Then Exit Sub

        Dim Lokasi As String = ""
        Dim SisaPR As String = ""
        Dim SatuanPR As String = ""
        Dim Dtp_TglDelivery As String = ""
        Dim Dtp_TglEstimasi As String = ""

        get_jam()

        Try
            OpenConn()

            SQL = $"
                select Kode_Stock_Owner, jumlah, Satuan, tanggal_delivery,
	                DateAdd(Day, isnull(c.Waktu_Pabrikasi,0) + isnull(c.Waktu_Pabrikasi,0), '{Format(tgl_skg, "yyyy-MM-dd")}') as tanggal_actual_delivery
                FROM EMI_Purchase_Requisition a
	                inner join EMI_Purchase_Requisition_Detail b on a.kode_perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
	                outer APPLY (
		                SELECT top 1 z.Waktu_Pabrikasi, z.Waktu_Pengiriman
		                FROM emi_detail_proses_pengiriman_po z
		                WHERE z.Kode_Perusahaan = a.Kode_Perusahaan
		                and z.Kode_Barang = b.Kode_Barang
	                ) AS c
                where a.Kode_Perusahaan = '{KodePerusahaan}'
                and a.Status is NULL
                and a.No_Faktur = '{DataGridView1.CurrentRow.Cells(item_No_PR).Value}'
                and b.no_urut = '{DataGridView1.CurrentRow.Cells(item_No_Urut).Value}'
            "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Lokasi = Dr("Kode_Stock_Owner")
                    SisaPR = Dr("jumlah")
                    SatuanPR = Dr("Satuan")
                    Dtp_TglDelivery = Dr("tanggal_delivery")
                    Dtp_TglEstimasi = Dr("tanggal_actual_delivery")
                Else
                    CloseConn()
                    MessageBox.Show("Data PR Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Dim NoPR As String = DataGridView1.Rows(currentRow).Cells(item_No_PR).Value
        Dim UrutPR As String = DataGridView1.Rows(currentRow).Cells(item_No_Urut).Value
        Dim KdBarang As String = DataGridView1.Rows(currentRow).Cells(item_Kd_Barang).Value
        Dim NmBarang As String = DataGridView1.Rows(currentRow).Cells(item_Nm_Barang).Value


        If Not String.IsNullOrEmpty(UrutPR) Then
            SD_Pengajuan_Selesai_PR.UrutPR = UrutPR
            SD_Pengajuan_Selesai_PR.Txt_NoPR.Text = NoPR
            SD_Pengajuan_Selesai_PR.Txt_KdSo.Text = Lokasi
            SD_Pengajuan_Selesai_PR.Txt_KdBrang.Text = KdBarang
            SD_Pengajuan_Selesai_PR.Txt_NmBarang.Text = NmBarang
            SD_Pengajuan_Selesai_PR.Txt_SisaPR.Text = Format(Val(HilangkanTanda(SisaPR)), "N2")
            SD_Pengajuan_Selesai_PR.DTP_TglDelivery.Value = Dtp_TglDelivery
            SD_Pengajuan_Selesai_PR.DTP_TglEstimasi.Value = Dtp_TglEstimasi

            SD_Pengajuan_Selesai_PR.Cmd_SatuanSisa.Items.Add(SatuanPR)
            SD_Pengajuan_Selesai_PR.Cmd_SatuanSisa.SelectedIndex = 0
            SD_Pengajuan_Selesai_PR.asal = "PR_PENAWARAN"

            SD_Pengajuan_Selesai_PR.ShowDialog()
        Else
            MessageBox.Show("Pilih Dahulu Data yang Ingin di Ajukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
    End Sub

    Private Sub PengajuanSelesaiToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PengajuanSelesaiToolStripMenuItem1.Click
        If DataGridView2.Rows.Count = 0 Then Exit Sub
        Dim currentRow = DataGridView2.CurrentRow.Index
        Dim currentCell = DataGridView2.CurrentCellAddress.X

        Try
            OpenConn()

            If CekButtonRole("Penyelesaian_PR_Waiting_Offer") = "T" Then
                MessageBox.Show("User Tidak Ada Akses Pengajuan Selesai PR", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If (MessageBox.Show("Yakin ingin melakukan penyelesaian PR ini??", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = vbNo Then Exit Sub

        Dim Lokasi As String = ""
        Dim SisaPR As String = ""
        Dim SatuanPR As String = ""
        Dim Dtp_TglDelivery As String = ""
        Dim Dtp_TglEstimasi As String = ""

        get_jam()

        Try
            OpenConn()

            SQL = $"
                select Kode_Stock_Owner, jumlah, Satuan, tanggal_delivery,
	                DateAdd(Day, isnull(c.Waktu_Pabrikasi,0) + isnull(c.Waktu_Pabrikasi,0), '{Format(tgl_skg, "yyyy-MM-dd")}') as tanggal_actual_delivery
                FROM EMI_Purchase_Requisition a
	                inner join EMI_Purchase_Requisition_Detail b on a.kode_perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
	                outer APPLY (
		                SELECT top 1 z.Waktu_Pabrikasi, z.Waktu_Pengiriman
		                FROM emi_detail_proses_pengiriman_po z
		                WHERE z.Kode_Perusahaan = a.Kode_Perusahaan
		                and z.Kode_Barang = b.Kode_Barang
	                ) AS c
                where a.Kode_Perusahaan = '{KodePerusahaan}'
                and a.Status is NULL
                and a.No_Faktur = '{DataGridView2.CurrentRow.Cells(item2_No_PR).Value}'
                and b.no_urut = '{DataGridView2.CurrentRow.Cells(item2_No_Urut).Value}'

            "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Lokasi = Dr("Kode_Stock_Owner")
                    SisaPR = Dr("jumlah")
                    SatuanPR = Dr("Satuan")
                    Dtp_TglDelivery = Dr("tanggal_delivery")
                    Dtp_TglEstimasi = Dr("tanggal_actual_delivery")
                Else
                    CloseConn()
                    MessageBox.Show("Data PR Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Dim NoPR As String = DataGridView2.Rows(currentRow).Cells(item2_No_PR).Value
        Dim UrutPR As String = DataGridView2.Rows(currentRow).Cells(item2_No_Urut).Value
        Dim KdBarang As String = DataGridView2.Rows(currentRow).Cells(item2_Kd_Barang).Value
        Dim NmBarang As String = DataGridView2.Rows(currentRow).Cells(item2_Nm_Barang).Value


        If Not String.IsNullOrEmpty(UrutPR) Then
            SD_Pengajuan_Selesai_PR.UrutPR = UrutPR
            SD_Pengajuan_Selesai_PR.Txt_NoPR.Text = NoPR
            SD_Pengajuan_Selesai_PR.Txt_KdSo.Text = Lokasi
            SD_Pengajuan_Selesai_PR.Txt_KdBrang.Text = KdBarang
            SD_Pengajuan_Selesai_PR.Txt_NmBarang.Text = NmBarang
            SD_Pengajuan_Selesai_PR.Txt_SisaPR.Text = Format(Val(HilangkanTanda(SisaPR)), "N2")
            SD_Pengajuan_Selesai_PR.DTP_TglDelivery.Value = Dtp_TglDelivery
            SD_Pengajuan_Selesai_PR.DTP_TglEstimasi.Value = Dtp_TglEstimasi

            SD_Pengajuan_Selesai_PR.Cmd_SatuanSisa.Items.Add(SatuanPR)
            SD_Pengajuan_Selesai_PR.Cmd_SatuanSisa.SelectedIndex = 0
            SD_Pengajuan_Selesai_PR.asal = "PR_PENAWARAN"

            SD_Pengajuan_Selesai_PR.ShowDialog()
        Else
            MessageBox.Show("Pilih Dahulu Data yang Ingin di Ajukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
    End Sub
End Class