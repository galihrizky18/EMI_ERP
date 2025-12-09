Public Class N_EMI_Purchase_Requisition_Penawaran_Barang_Lain


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

    Private Sub N_EMI_Purchase_Requisition_Penawaran_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TabControl1.SelectedTab = TabPage2
    End Sub

    Private Sub Fetch_Suppliers(searchText As String)
        Try
            OpenConn()

            Dim SQL As String = If(searchText.Length = 0,
                "SELECT TOP 10 Kode_Supplier, Nama FROM Suppliers ORDER BY Kode_Supplier",
                "SELECT TOP 10 Kode_Supplier, Nama FROM Suppliers WHERE Kode_Supplier LIKE @Search OR Nama LIKE @Search ORDER BY Kode_Supplier")

            If searchText.Length > 0 Then
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@Search", "%" & searchText & "%")
            End If

            LvSuppliers.Items.Clear()
            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    Dim item As New ListViewItem(Dr("Kode_Supplier").ToString())
                    item.SubItems.Add(Dr("Nama").ToString())
                    LvSuppliers.Items.Add(item)
                End While
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
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

    Private Sub TxtKodeSupplier_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPO_KdSupplier.KeyDown
        If e.KeyCode = Keys.Down AndAlso LvSuppliers.Visible AndAlso LvSuppliers.Items.Count > 0 Then
            e.Handled = True
            LvSuppliers.Focus()
            If LvSuppliers.SelectedItems.Count = 0 Then
                LvSuppliers.Items(0).Selected = True
            End If
        ElseIf e.KeyCode = Keys.Return Then
            e.Handled = True
            LvSuppliers.Visible = False
        End If
    End Sub

    Public Sub Fetch_PR_Offered()
        Try
            OpenConn()

            Dim SQL As String = "
                SELECT b.No_Urut, a.No_Faktur AS No_PR, b.Kode_Barang, c.Nama AS Nama_Barang,
                       b.No_Penawaran, b.Keterangan, b.Keterangan_Penawaran, b.Jumlah AS Qty,
                       f.Nama AS Nama_Supplier, d.Harga_Satuan, d.Mata_Uang, e.Periode_Akhir_Penawaran
                FROM EMI_Purchase_Requisition_Barang_Lain a
                JOIN EMI_Purchase_Requisition_Barang_Lain_Detail b 
                    ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Faktur = b.No_Faktur
                JOIN Barang_Lain c 
                    ON b.Kode_Barang = c.Kode_Barang AND c.Kode_Perusahaan = b.Kode_Perusahaan 
                    AND c.Kode_Stock_Owner = b.Kode_Stock_Owner
                JOIN EMI_Master_Penawaran_Detail_Barang_Lain d 
                    ON d.No_Faktur = b.No_Penawaran AND d.Kode_Perusahaan = b.Kode_Perusahaan
                    AND d.Kode_Barang = b.Kode_Barang
                JOIN EMI_Master_Penawaran_Barang_Lain e
                    ON e.No_Faktur = d.No_Faktur AND e.Kode_Perusahaan = b.Kode_Perusahaan
                JOIN Suppliers f ON f.Kode_Supplier = e.Kode_Supplier
                WHERE a.Kode_Perusahaan = @KodePerusahaan AND b.Flag_Sudah_PO IS NULL 
                  AND a.Status IS NULL AND b.No_Penawaran IS NOT NULL
                  AND b.Flag_Pengajuan_Selesai is null
            "

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)

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
                            DataGridView1.Rows(rowIndex).DefaultCellStyle.BackColor = Color.IndianRed
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

    Private Sub Fetch_PR_Waiting_Offer()
        Try
            OpenConn()

            Dim SQL As String = "
                SELECT b.No_Urut, a.No_Faktur AS No_PR, b.Kode_Barang, c.Nama AS Nama_Barang,
                       b.Jumlah AS Qty, b.Keterangan, b.Keterangan_Penawaran,
                       ISNULL(p.No_Penawaran, '') AS No_Penawaran, ISNULL(p.Mata_Uang, '') AS Mata_Uang,
                       ISNULL(p.Harga_Satuan, '') AS Harga_Satuan,
                       CASE WHEN p.No_Penawaran IS NOT NULL THEN 0 ELSE 1 END AS Sort_Order
                FROM EMI_Purchase_Requisition_Barang_Lain a
                JOIN EMI_Purchase_Requisition_Barang_Lain_Detail b
                    ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Faktur = b.No_Faktur
                JOIN Barang_Lain c
                    ON b.Kode_Barang = c.Kode_Barang AND c.Kode_Perusahaan = b.Kode_Perusahaan
                    AND c.Kode_Stock_Owner = b.Kode_Stock_Owner
                LEFT JOIN (
                    SELECT f.Kode_Perusahaan, f.Kode_Barang, f.Harga_Satuan, f.Mata_Uang, f.No_Faktur AS No_Penawaran
                    FROM EMI_Master_Penawaran_Detail_Barang_Lain f
                    JOIN EMI_Master_Penawaran_Barang_Lain e
                        ON f.Kode_Perusahaan = e.Kode_Perusahaan AND f.No_Faktur = e.No_Faktur
                    WHERE e.Kode_Supplier = @KodeSupplier AND e.Selesai IS NULL AND e.Flag_Release = 'Y'
                      AND e.Status IS NULL AND CAST(e.Periode_Akhir_Penawaran AS DATE) >= CAST(GETDATE() AS DATE)
                ) p ON p.Kode_Perusahaan = b.Kode_Perusahaan AND p.Kode_Barang = b.Kode_Barang
                WHERE a.Kode_Perusahaan = @KodePerusahaan AND b.Flag_Sudah_PO IS NULL 
                  AND a.Status IS NULL AND b.No_Penawaran IS NULL
                ORDER BY Sort_Order, b.No_Urut"

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@KodeSupplier", TxtPO_KdSupplier.Text)

            DataGridView2.SuspendLayout()
            DataGridView2.Rows.Clear()

            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    DataGridView2.Rows.Add(
                        False, Dr("No_Urut"), Dr("No_PR"), Dr("Kode_Barang"), Dr("Nama_Barang"),
                        Dr("No_Penawaran"), Dr("Mata_Uang"), Dr("Harga_Satuan"),
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

    Public Sub Fetch_PR_Waiting_Offer_All()
        Try
            OpenConn()

            Dim SQL As String = "
                SELECT b.No_Urut, a.No_Faktur AS No_PR, b.Kode_Barang, c.Nama AS Nama_Barang,
                       b.Jumlah AS Qty, b.Keterangan, b.Keterangan_Penawaran
                FROM EMI_Purchase_Requisition_Barang_Lain a
                JOIN EMI_Purchase_Requisition_Barang_Lain_Detail b
                    ON a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Faktur = b.No_Faktur
                JOIN Barang_Lain c
                    ON b.Kode_Barang = c.Kode_Barang AND c.Kode_Perusahaan = b.Kode_Perusahaan
                    AND c.Kode_Stock_Owner = b.Kode_Stock_Owner
                WHERE a.Kode_Perusahaan = @KodePerusahaan AND b.Flag_Sudah_PO IS NULL 
                  AND a.Status IS NULL AND b.No_Penawaran IS NULL
                ORDER BY b.No_Urut"

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)

            DataGridView2.SuspendLayout()
            DataGridView2.Rows.Clear()

            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    DataGridView2.Rows.Add(
                        False, Dr("No_Urut"), Dr("No_PR"), Dr("Kode_Barang"), Dr("Nama_Barang"),
                        "", "", "", Dr("Keterangan"), Dr("Keterangan_Penawaran"))
                End While
            End Using

            DataGridView2.ResumeLayout()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
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

                Dim noPenawaran = DataGridView2.Rows(i).Cells(5).Value


                Dim noUrut = DataGridView2.Rows(i).Cells(1).Value
                Dim noPR = DataGridView2.Rows(i).Cells(2).Value
                Dim kodeBarang = DataGridView2.Rows(i).Cells(3).Value
                Dim keterangan = If(DataGridView2.Rows(i).Cells(9).Value IsNot Nothing, DataGridView2.Rows(i).Cells(9).Value, "")

                If noPenawaran Is Nothing OrElse noPenawaran.ToString.Trim = "" Then
                    CloseConn()
                    MessageBox.Show($"Error: No Penawaran Pada Kode Barang {kodeBarang} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                Dim sqlSelect = "SELECT No_Penawaran, Keterangan_Penawaran FROM EMI_Purchase_Requisition_Barang_Lain_Detail WHERE No_Urut = @NoUrut AND Kode_Barang = @KodeBarang"
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

                Dim sqlUpdate = "UPDATE EMI_Purchase_Requisition_Barang_Lain_Detail SET Status_Validasi = 'Y', No_Penawaran = @NoPenawaran, Keterangan_Penawaran = @Keterangan_Penawaran WHERE No_Urut = @NoUrut AND Kode_Barang = @KodeBarang"
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@NoPenawaran", noPenawaran)
                Cmd.Parameters.AddWithValue("@Keterangan_Penawaran", If(String.IsNullOrEmpty(keterangan.ToString.Trim), DBNull.Value, keterangan.ToString.Trim))
                Cmd.Parameters.AddWithValue("@NoUrut", noUrut)
                Cmd.Parameters.AddWithValue("@KodeBarang", kodeBarang)
                Cmd.CommandText = sqlUpdate
                Cmd.ExecuteNonQuery()
                cntUpdated += 1

                InsertLog(KodePerusahaan, tanggal, jam, UserID, noPR, noUrut, noPenawaranLama, noPenawaran, keteranganLama, DBNull.Value)
            Next

            CloseConn()

            If cntUpdated > 0 Then
                Fetch_PR_Offered()
                If Not String.IsNullOrEmpty(TxtPO_KdSupplier.Text) Then
                    Fetch_PR_Waiting_Offer()
                Else
                    Fetch_PR_Waiting_Offer_All()
                End If
                MessageBox.Show($"Berhasil menambahkan {cntUpdated} data penawaran!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub InsertLog(kodePerusahaan, tanggal, jam, userId, noFaktur, noUrut, noPenawaranLama, noPenawaranBaru, keteranganLama, keteranganBaru)
        Dim sqlLog = "INSERT INTO N_EMI_LOG_Purchase_Requisition_Barang_Lain_Detail (Kode_Perusahaan, Tanggal, Jam, UserID, No_Faktur, No_Urut, No_Penawaran_Lama, No_Penawaran_Baru, Keterangan_Lama, Keterangan_Baru) VALUES (@KodePerusahaan, @Tanggal, @Jam, @UserID, @NoFaktur, @NoUrut, @NoPenawaranLama, @NoPenawaranBaru, @KeteranganLama, @KeteranganBaru)"
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
        Try
            If TabControl1.SelectedTab Is TabPage1 Then
                Fetch_PR_Offered()
                BtnBatalkan.Visible = True
                BtnSimpan.Visible = False
            ElseIf TabControl1.SelectedTab Is TabPage2 Then
                If Not String.IsNullOrEmpty(TxtPO_KdSupplier.Text) Then
                    Fetch_PR_Waiting_Offer()
                Else
                    Fetch_PR_Waiting_Offer_All()
                End If
                BtnBatalkan.Visible = False
                BtnSimpan.Visible = True
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Form_Click(sender As Object, e As EventArgs) Handles Me.Click
        If LvSuppliers.Visible Then
            LvSuppliers.Visible = False
            Me.ActiveControl = Nothing
        End If
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        TxtPO_KdSupplier.Text = ""
        TxtPO_NmSupplier.Text = ""
        LvSuppliers.Visible = False
        TabControl1.SelectedTab = TabPage2
        Fetch_PR_Waiting_Offer_All()
        Fetch_PR_Offered()
    End Sub

    Private Sub BtnBatalkan_Click(sender As Object, e As EventArgs) Handles BtnBatalkan.Click
        Dim cntChecked As Integer = DataGridView1.Rows.Cast(Of DataGridViewRow)().Where(Function(r) CBool(r.Cells(0).Value)).Count()

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

                Dim noUrut As String = If(DataGridView1.Rows(i).Cells(1).Value IsNot Nothing, DataGridView1.Rows(i).Cells(1).Value.ToString().Trim(), "")
                Dim noPR As String = If(DataGridView1.Rows(i).Cells(2).Value IsNot Nothing, DataGridView1.Rows(i).Cells(2).Value.ToString().Trim(), "")
                Dim kodeBarang As String = If(DataGridView1.Rows(i).Cells(3).Value IsNot Nothing, DataGridView1.Rows(i).Cells(3).Value.ToString().Trim(), "")

                If String.IsNullOrEmpty(noUrut) OrElse String.IsNullOrEmpty(kodeBarang) Then Continue For

                Dim sqlSelect = "SELECT No_Penawaran, Keterangan_Penawaran FROM EMI_Purchase_Requisition_Barang_Lain_Detail WHERE No_Urut = @NoUrut AND Kode_Barang = @KodeBarang"
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

                Dim sqlUpdate = "UPDATE EMI_Purchase_Requisition_Barang_Lain_Detail SET Status_Validasi = 'T', No_Penawaran = NULL, Keterangan_Penawaran = NULL WHERE No_Urut = @NoUrut AND Kode_Barang = @KodeBarang"
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@NoUrut", noUrut)
                Cmd.Parameters.AddWithValue("@KodeBarang", kodeBarang)
                Cmd.CommandText = sqlUpdate
                Cmd.ExecuteNonQuery()
                cntUpdated += 1

                Dim keteranganBaru As String = If(DataGridView1.Rows(i).Cells(10).Value IsNot Nothing, DataGridView1.Rows(i).Cells(10).Value.ToString().Trim(), "")
                InsertLog(KodePerusahaan, tanggal, jam, UserID, noPR, noUrut, noPenawaranLama, DBNull.Value, keteranganLama, If(String.IsNullOrEmpty(keteranganBaru), DBNull.Value, keteranganBaru))
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

    Private Sub TxtPO_KdSupplier_TextChanged(sender As Object, e As EventArgs) Handles TxtPO_KdSupplier.TextChanged
        If TxtPO_KdSupplier.Text.Trim.Length = 0 Then
            LvSupplier2.Visible = False
            Exit Sub
        End If

        LvSupplier2.Location = New Point(80, 94)
        LvSupplier2.Visible = True
        LvSupplier2.Items.Clear()

        Try
            OpenConn()

            Dim SQL = "SELECT b.kode_supplier, b.nama FROM suppliers b 
                       WHERE b.kode_perusahaan = @KodePerusahaan AND b.kode_supplier LIKE @KodeSupplier 
                       ORDER BY b.kode_supplier"

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@KodeSupplier", "%" & TxtPO_KdSupplier.Text & "%")

            Using Dr = OpenTrans(SQL)
                While Dr.Read
                    Dim lv As New ListViewItem(Dr("kode_supplier").ToString())
                    lv.SubItems.Add(Dr("nama").ToString())
                    LvSupplier2.Items.Add(lv)
                End While
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub LvSupplier2_KeyDown(sender As Object, e As KeyEventArgs) Handles LvSupplier2.KeyDown
        If e.KeyCode = Keys.Enter Then
            LvSupplier2_DoubleClick(LvSupplier2, e)
        End If
    End Sub

    Private Sub LvSupplier2_DoubleClick(sender As Object, e As EventArgs) Handles LvSupplier2.DoubleClick
        If LvSupplier2.Items.Count = 0 Then Exit Sub

        Dim Kode As String = LvSupplier2.FocusedItem.Text
        Dim Nama As String = LvSupplier2.FocusedItem.SubItems(1).Text

        TxtPO_KdSupplier.Text = Kode
        TxtPO_NmSupplier.Text = Nama
        LvSupplier2.Visible = False

        DataGridView1.Focus()
        Fetch_PR_Waiting_Offer()
    End Sub

    Private Sub TxtPO_KdSupplier_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPO_KdSupplier.KeyDown
        If e.KeyCode = Keys.Down Then
            LvSupplier2.Focus()
        End If
    End Sub

    Private Sub PengajuanSelesaiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PengajuanSelesaiToolStripMenuItem.Click
        If DataGridView1.Rows.Count = 0 Then Exit Sub
        Dim currentRow = DataGridView1.CurrentRow.Index
        Dim currentCell = DataGridView1.CurrentCellAddress.X

        Try
            OpenConn()

            If CekButtonRole("Pengajuan_Batal_PR_Barang_Lain") = "T" Then
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

        Try
            OpenConn()

            SQL = $"
                select Kode_Stock_Owner, jumlah, Satuan, tanggal_delivery,
	                DateAdd(Day, c.Waktu_Pabrikasi + c.Waktu_Pengiriman, '2025-12-06') as tanggal_actual_delivery
                FROM EMI_Purchase_Requisition_barang_lain a
	                inner join EMI_Purchase_Requisition_barang_lain_Detail b on a.kode_perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
	                outer APPLY (
		                SELECT top 1 z.Waktu_Pabrikasi, z.Waktu_Pengiriman
		                FROM emi_detail_proses_pengiriman_po_Barang_Lain z
		                WHERE z.Kode_Perusahaan = a.Kode_Perusahaan
		                and z.Kode_Barang = b.Kode_Barang
	                ) AS c
                where a.Kode_Perusahaan = '{KodePerusahaan}'
                and a.Status is NULL
                and a.No_Faktur = '{DataGridView1.CurrentRow.Cells(item_No_PR).Value}'
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
            SD_Pengajuan_Selesai_PR_Barang_Lain.UrutPR = UrutPR
            SD_Pengajuan_Selesai_PR_Barang_Lain.Txt_NoPR.Text = NoPR
            SD_Pengajuan_Selesai_PR_Barang_Lain.Txt_KdSo.Text = Lokasi
            SD_Pengajuan_Selesai_PR_Barang_Lain.Txt_KdBrang.Text = KdBarang
            SD_Pengajuan_Selesai_PR_Barang_Lain.Txt_NmBarang.Text = NmBarang
            SD_Pengajuan_Selesai_PR_Barang_Lain.Txt_SisaPR.Text = Format(Val(HilangkanTanda(SisaPR)), "N2")
            SD_Pengajuan_Selesai_PR_Barang_Lain.DTP_TglDelivery.Value = Dtp_TglDelivery
            SD_Pengajuan_Selesai_PR_Barang_Lain.DTP_TglEstimasi.Value = Dtp_TglEstimasi

            SD_Pengajuan_Selesai_PR_Barang_Lain.Cmd_SatuanSisa.Items.Add(SatuanPR)
            SD_Pengajuan_Selesai_PR_Barang_Lain.Cmd_SatuanSisa.SelectedIndex = 0

            SD_Pengajuan_Selesai_PR_Barang_Lain.asal = "PR_PENAWARAN"

            SD_Pengajuan_Selesai_PR_Barang_Lain.ShowDialog()
        Else
            MessageBox.Show("Pilih Dahulu Data yang Ingin di Ajukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
    End Sub
End Class