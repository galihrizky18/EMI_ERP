Public Class N_EMI_Purchase_Requisition_Penawaran_BackupLama
    Private CurrentSelectedSupplier As String = ""
    Private searchTimer As Timer
    Private lastSearchText As String = ""


    Dim Lv_ChkBox, Lv_No_Urut, Lv_No_Pr, Lv_Kd_Barang, Lv_Nm_Barang, Lv_Nm_Supplier, Lv_No_Penawaran, Lv_Mata_Uang, Lv_Harga, Lv_Keterangan_PR, Lv_Keterangan As String

    Dim item_ChkBox as integer = 0
    Dim item_No_Urut as integer = 1
    Dim item_No_PR as integer = 2
    Dim item_Kd_Barang as integer = 3
    Dim item_Nm_Barang as integer = 4
    Dim item_nm_Supllier as integer = 5
    Dim item_No_Penawaran as integer = 6
    Dim item_Mata_Uang as integer = 7
    Dim item_Harga As Integer = 8
    Dim item_Keterangan_PR as integer = 9
    Dim item_Keterangan as integer = 10



    Private Sub N_EMI_Purchase_Requisition_Penawaran_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Kosong()
    End Sub

    Private Sub Kosong()
        TabControl1.SelectedTab = TabPage1
        TabControl1_SelectedIndexChanged(Nothing, Nothing)

        SetupLvSuppliers()
        SetupSearchTimer()

        TxtKodeSupplier.Text = ""
        TxtNamaSupplier.Text = ""
        LvSuppliers.Visible = False
    End Sub

    Private Sub SetupSearchTimer()
        searchTimer = New Timer()
        searchTimer.Interval = 300
        AddHandler searchTimer.Tick, Sub(s, e) PerformSearch()
    End Sub

    Private Sub SetupLvSuppliers()
        With LvSuppliers
            .View = View.Details
            .Columns.Clear()
            .Columns.Add("Kode Supplier", 100)
            .Columns.Add("Nama Supplier", 200)
            .FullRowSelect = True
            .GridLines = True
        End With
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

    Private Sub TxtKodeSupplier_GotFocus(sender As Object, e As EventArgs) Handles TxtKodeSupplier.GotFocus
        Dim searchText As String = TxtKodeSupplier.Text.Trim()

        If searchText.Length = 0 Then
            Fetch_Suppliers("")
        Else
            Fetch_Suppliers(searchText)
        End If

        LvSuppliers.Visible = True
    End Sub

    Private Sub LvSuppliers_LostFocus(sender As Object, e As EventArgs) Handles LvSuppliers.LostFocus
        LvSuppliers.Visible = False
    End Sub

    Private Sub TxtKodeSupplier_TextChanged(sender As Object, e As EventArgs) Handles TxtKodeSupplier.TextChanged
        searchTimer.Stop()
        Dim searchText As String = TxtKodeSupplier.Text.Trim()

        If lastSearchText <> searchText Then
            lastSearchText = searchText
            searchTimer.Start()
        End If
    End Sub

    Private Sub PerformSearch()
        searchTimer.Stop()
        Dim searchText As String = TxtKodeSupplier.Text.Trim()
        Fetch_Suppliers(searchText)
        LvSuppliers.Visible = True
    End Sub

    Private Sub Fetch_Suppliers(searchText As String)
        Try
            OpenConn()

            Dim SQL As String
            If searchText.Length = 0 Then
                SQL = "SELECT TOP 10 Kode_Supplier, Nama FROM Suppliers ORDER BY Kode_Supplier"
                Dim Dr = OpenTrans(SQL)
                LvSuppliers.Items.Clear()
                While Dr.Read()
                    Dim item As New ListViewItem(Dr("Kode_Supplier").ToString())
                    item.SubItems.Add(Dr("Nama").ToString())
                    LvSuppliers.Items.Add(item)
                End While
            Else
                SQL = "SELECT TOP 10 Kode_Supplier, Nama FROM Suppliers WHERE Kode_Supplier LIKE @Search OR Nama LIKE @Search ORDER BY Kode_Supplier"
                Cmd.Parameters.Clear()
                Cmd.Parameters.AddWithValue("@Search", "%" & searchText & "%")
                Dim Dr = OpenTrans(SQL)
                LvSuppliers.Items.Clear()
                While Dr.Read()
                    Dim item As New ListViewItem(Dr("Kode_Supplier").ToString())
                    item.SubItems.Add(Dr("Nama").ToString())
                    LvSuppliers.Items.Add(item)
                End While
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub LvSuppliers_ItemActivate(sender As Object, e As EventArgs) Handles LvSuppliers.ItemActivate
        SelectSupplierFromListView()
    End Sub

    Private Sub LvSuppliers_KeyDown(sender As Object, e As KeyEventArgs) Handles LvSuppliers.KeyDown
        If e.KeyCode = Keys.Return Then
            e.Handled = True
            SelectSupplierFromListView()
        End If

        If e.KeyCode = Keys.Escape Then
            e.Handled = True
            LvSuppliers.Visible = False
            TxtKodeSupplier.Focus()
        End If
    End Sub

    Private Sub SelectSupplierFromListView()
        If LvSuppliers.SelectedItems.Count > 0 Then
            Dim selectedItem = LvSuppliers.SelectedItems(0)

            CurrentSelectedSupplier = selectedItem.Text
            TxtKodeSupplier.Text = selectedItem.Text
            TxtNamaSupplier.Text = selectedItem.SubItems(1).Text

            LvSuppliers.Visible = False

            Fetch_PR_Waiting_Offer()
        End If
    End Sub

    Private Sub TxtKodeSupplier_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtKodeSupplier.KeyDown
        If e.KeyCode = Keys.Down AndAlso LvSuppliers.Visible AndAlso LvSuppliers.Items.Count > 0 Then
            e.Handled = True
            LvSuppliers.Focus()
            If LvSuppliers.SelectedItems.Count = 0 Then
                LvSuppliers.Items(0).Selected = True
            End If
        End If

        If e.KeyCode = Keys.Return Then
            e.Handled = True
            LvSuppliers.Visible = False
        End If
    End Sub

    Public Sub Fetch_PR_Offered()
        Try
            OpenConn()

            Dim SQL As String = "
                SELECT
                    b.No_Urut,
                    a.No_Faktur AS No_PR,
                    b.Kode_Barang,
                    c.Nama AS Nama_Barang,
                    b.No_Penawaran,
                    b.Keterangan,
                    b.Keterangan_Penawaran,
                    b.Jumlah AS Qty,
                    f.Nama AS Nama_Supplier,
                    d.Harga_Satuan,
                    d.Mata_Uang
                FROM EMI_Purchase_Requisition a
                JOIN EMI_Purchase_Requisition_Detail b 
                    ON a.Kode_Perusahaan = b.Kode_Perusahaan 
                    AND a.No_Faktur = b.No_Faktur
                JOIN Barang c 
                    ON b.Kode_Barang = c.Kode_Barang 
                    AND c.Kode_Perusahaan = b.Kode_Perusahaan 
                    AND c.Kode_Stock_Owner = b.Kode_Stock_Owner
                JOIN EMI_Master_Penawaran_Detail d 
                    ON d.No_Faktur = b.No_Penawaran
                    AND d.Kode_Perusahaan = b.Kode_Perusahaan
                    AND d.Kode_Barang = b.Kode_Barang
                JOIN EMI_Master_Penawaran e
                    ON e.No_Faktur = d.No_Faktur
                    AND e.Kode_Perusahaan = b.Kode_Perusahaan
                JOIN Suppliers f
                    ON f.Kode_Supplier = e.Kode_Supplier
                WHERE a.Kode_Perusahaan = @KodePerusahaan
                  AND b.Flag_Sudah_PO IS NULL 
                  AND a.Status IS NULL
                  AND b.No_Penawaran IS NOT NULL
                  AND b.Flag_Pengajuan_Selesai is null
            "
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)

            Dim Dr = OpenTrans(SQL)

            DataGridView1.SuspendLayout()
            DataGridView1.Rows.Clear()

            Dim rows As New List(Of Object())
            While Dr.Read()
                rows.Add(New Object() {
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
                    Dr("Keterangan_Penawaran")
                })
            End While

            For Each row In rows
                DataGridView1.Rows.Add(row)
            Next

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
                SELECT
                    b.No_Urut,
                    a.No_Faktur AS No_PR,
                    b.Kode_Barang,
                    c.Nama AS Nama_Barang,
                    b.Jumlah AS Qty,
                    b.Keterangan,
                    b.Keterangan_Penawaran,
                    ISNULL(p.No_Penawaran, '') AS No_Penawaran,
                    ISNULL(p.Mata_Uang, '') AS Mata_Uang,
                    ISNULL(p.Harga_Satuan, '') AS Harga_Satuan,
                    CASE WHEN p.No_Penawaran IS NOT NULL THEN 0 ELSE 1 END AS Sort_Order
                FROM EMI_Purchase_Requisition a
                JOIN EMI_Purchase_Requisition_Detail b
                    ON a.Kode_Perusahaan = b.Kode_Perusahaan
                    AND a.No_Faktur = b.No_Faktur
                JOIN Barang c
                    ON b.Kode_Barang = c.Kode_Barang
                    AND c.Kode_Perusahaan = b.Kode_Perusahaan
                    AND c.Kode_Stock_Owner = b.Kode_Stock_Owner
                LEFT JOIN (
                    SELECT 
                        f.Kode_Perusahaan,
                        f.Kode_Barang,
                        f.Harga_Satuan,
                        f.Mata_Uang,
                        f.No_Faktur AS No_Penawaran
                    FROM EMI_Master_Penawaran_Detail f
                    JOIN EMI_Master_Penawaran e
                        ON f.Kode_Perusahaan = e.Kode_Perusahaan
                        AND f.No_Faktur = e.No_Faktur
                    WHERE e.Kode_Supplier = @KodeSupplier
                      AND e.Selesai IS NULL
                      AND e.Flag_Release = 'Y'
                      AND e.Status IS NULL
                ) p
                    ON p.Kode_Perusahaan = b.Kode_Perusahaan
                    AND p.Kode_Barang = b.Kode_Barang
                WHERE a.Kode_Perusahaan = @KodePerusahaan
                  AND b.Flag_Sudah_PO IS NULL
                  AND a.Status IS NULL
                  AND b.No_Penawaran IS NULL
                ORDER BY Sort_Order, b.No_Urut
            "

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@KodeSupplier", CurrentSelectedSupplier)

            Dim Dr = OpenTrans(SQL)
            DataGridView2.SuspendLayout()
            DataGridView2.Rows.Clear()

            Dim rows As New List(Of Object())
            While Dr.Read()
                rows.Add(New Object() {
                    False,
                    Dr("No_Urut"),
                    Dr("No_PR"),
                    Dr("Kode_Barang"),
                    Dr("Nama_Barang"),
                    Dr("No_Penawaran"),
                    Dr("Mata_Uang"),
                    Dr("Harga_Satuan"),
                    Dr("Keterangan"),
                    Dr("Keterangan_Penawaran")
                })
            End While

            For Each row In rows
                DataGridView2.Rows.Add(row)
            Next

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
                SELECT
                    b.No_Urut,
                    a.No_Faktur AS No_PR,
                    b.Kode_Barang,
                    c.Nama AS Nama_Barang,
                    b.Jumlah AS Qty,
                    b.Keterangan,
                    b.Keterangan_Penawaran,
                    '' AS No_Penawaran,
                    '' AS Mata_Uang,
                    '' AS Harga_Satuan
                FROM EMI_Purchase_Requisition a
                JOIN EMI_Purchase_Requisition_Detail b
                    ON a.Kode_Perusahaan = b.Kode_Perusahaan
                    AND a.No_Faktur = b.No_Faktur
                JOIN Barang c
                    ON b.Kode_Barang = c.Kode_Barang
                    AND c.Kode_Perusahaan = b.Kode_Perusahaan
                    AND c.Kode_Stock_Owner = b.Kode_Stock_Owner
                WHERE a.Kode_Perusahaan = @KodePerusahaan
                  AND b.Flag_Sudah_PO IS NULL
                  AND a.Status IS NULL
                  AND b.No_Penawaran IS NULL
                ORDER BY b.No_Urut
            "

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)

            Dim Dr = OpenTrans(SQL)
            DataGridView2.SuspendLayout()
            DataGridView2.Rows.Clear()

            Dim rows As New List(Of Object())
            While Dr.Read()
                rows.Add(New Object() {
                    False,
                    Dr("No_Urut"),
                    Dr("No_PR"),
                    Dr("Kode_Barang"),
                    Dr("Nama_Barang"),
                    Dr("No_Penawaran"),
                    Dr("Mata_Uang"),
                    Dr("Harga_Satuan"),
                    Dr("Keterangan"),
                    Dr("Keterangan_Penawaran")
                })
            End While

            For Each row In rows
                DataGridView2.Rows.Add(row)
            Next

            DataGridView2.ResumeLayout()
            CloseConn()

        Catch ex As Exception
            CloseConn()
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub DataGridView2_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles DataGridView2.CurrentCellDirtyStateChanged
        If DataGridView2.IsCurrentCellDirty Then
            Dim cell = DataGridView2.CurrentCell
            If TypeOf cell Is DataGridViewCheckBoxCell Then
                DataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit)
            End If
        End If
    End Sub

    Private Sub DataGridView2_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellValueChanged
        If e.RowIndex < 0 OrElse e.ColumnIndex <> 0 Then Return

        Dim changedRow = DataGridView2.Rows(e.RowIndex)
        Dim isChecked As Boolean

        Try
            isChecked = Convert.ToBoolean(If(changedRow.Cells(0).Value, False))
        Catch
            isChecked = False
        End Try

        If isChecked Then
            Dim currentNoPR As String = Convert.ToString(If(changedRow.Cells(2).Value, String.Empty))
            Dim currentKodeBarang As String = Convert.ToString(If(changedRow.Cells(3).Value, String.Empty))

            For i As Integer = 0 To DataGridView2.Rows.Count - 1
                If i = e.RowIndex Then Continue For

                Dim rowNoPR As String = Convert.ToString(If(DataGridView2.Rows(i).Cells(2).Value, String.Empty))
                Dim rowKodeBarang As String = Convert.ToString(If(DataGridView2.Rows(i).Cells(3).Value, String.Empty))

                If rowNoPR = currentNoPR AndAlso rowKodeBarang = currentKodeBarang Then
                    DataGridView2.Rows(i).Cells(0).Value = False
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
                Dim isChecked As Boolean = CBool(DataGridView2.Rows(i).Cells(0).Value)

                If isChecked Then
                    Dim noPR = DataGridView2.Rows(i).Cells(2).Value
                    Dim kodeBarang = DataGridView2.Rows(i).Cells(3).Value
                    Dim noUrut = DataGridView2.Rows(i).Cells(1).Value
                    Dim noPenawaran = DataGridView2.Rows(i).Cells(5).Value
                    Dim keterangan = DataGridView2.Rows(i).Cells(9).Value

                    If noPenawaran Is Nothing OrElse noPenawaran.ToString.Trim = "" Then
                        CloseConn()
                        MessageBox.Show($"Error: No Penawaran Pada Kode Barang {kodeBarang} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    Dim sqlSelect = "SELECT No_Penawaran, Keterangan_Penawaran FROM EMI_Purchase_Requisition_Detail WHERE No_Urut = @NoUrut AND Kode_Barang = @KodeBarang"

                    Cmd.Parameters.Clear()
                    Cmd.Parameters.AddWithValue("@NoUrut", noUrut)
                    Cmd.Parameters.AddWithValue("@KodeBarang", kodeBarang)

                    Dim noPenawaranLama As String = ""
                    Dim keteranganLama As String = ""

                    Dim drOld = OpenTrans(sqlSelect)
                    If drOld.Read Then
                        noPenawaranLama = If(drOld("No_Penawaran") IsNot Nothing, drOld("No_Penawaran").ToString, "")
                        keteranganLama = If(drOld("Keterangan_Penawaran") IsNot Nothing, drOld("Keterangan_Penawaran").ToString, "")
                    End If
                    drOld.Close()

                    Dim keteranganBaru As String = If(keterangan Is Nothing, "", keterangan.ToString.Trim)
                    Dim sqlUpdate = "UPDATE EMI_Purchase_Requisition_Detail SET Status_Validasi = 'Y', No_Penawaran = @NoPenawaran, Keterangan_Penawaran = @Keterangan_Penawaran WHERE No_Urut = @NoUrut AND Kode_Barang = @KodeBarang"

                    Cmd.Parameters.Clear()
                    Cmd.Parameters.AddWithValue("@NoPenawaran", noPenawaran)
                    Cmd.Parameters.AddWithValue("@Keterangan_Penawaran", If(String.IsNullOrEmpty(keteranganBaru), DBNull.Value, CObj(keteranganBaru)))
                    Cmd.Parameters.AddWithValue("@NoUrut", noUrut)
                    Cmd.Parameters.AddWithValue("@KodeBarang", kodeBarang)
                    Cmd.CommandText = sqlUpdate
                    Cmd.ExecuteNonQuery()

                    cntUpdated += 1

                    Dim sqlLog = "INSERT INTO N_EMI_LOG_Purchase_Requisition_Detail (Kode_Perusahaan, Tanggal, Jam, UserID, No_Faktur, No_Urut, No_Penawaran_Lama, No_Penawaran_Baru, Keterangan_Lama, Keterangan_Baru) VALUES (@KodePerusahaan, @Tanggal, @Jam, @UserID, @NoFaktur, @NoUrut, @NoPenawaranLama, @NoPenawaranBaru, @KeteranganLama, @KeteranganBaru)"

                    Cmd.Parameters.Clear()
                    Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                    Cmd.Parameters.AddWithValue("@Tanggal", tanggal)
                    Cmd.Parameters.AddWithValue("@Jam", jam)
                    Cmd.Parameters.AddWithValue("@UserID", UserID)
                    Cmd.Parameters.AddWithValue("@NoFaktur", noPR)
                    Cmd.Parameters.AddWithValue("@NoUrut", noUrut)
                    Cmd.Parameters.AddWithValue("@NoPenawaranLama", If(String.IsNullOrEmpty(noPenawaranLama), DBNull.Value, CObj(noPenawaranLama)))
                    Cmd.Parameters.AddWithValue("@NoPenawaranBaru", noPenawaran)
                    Cmd.Parameters.AddWithValue("@KeteranganLama", If(String.IsNullOrEmpty(keteranganLama), DBNull.Value, CObj(keteranganLama)))
                    Cmd.Parameters.AddWithValue("@KeteranganBaru", DBNull.Value)
                    Cmd.CommandText = sqlLog
                    Cmd.ExecuteNonQuery()
                End If
            Next

            CloseConn()

            If cntUpdated > 0 Then
                Fetch_PR_Offered()
                If Not String.IsNullOrEmpty(CurrentSelectedSupplier) Then
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

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Try
            If TabControl1.SelectedTab Is TabPage1 Then
                Fetch_PR_Offered()
                BtnBatalkan.Visible = True
                BtnSimpan.Visible = False
            End If

            If TabControl1.SelectedTab Is TabPage2 Then
                Fetch_PR_Waiting_Offer_All()
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

    Public Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        TxtKodeSupplier.Text = ""
        TxtNamaSupplier.Text = ""
        Fetch_PR_Offered()
        Fetch_PR_Waiting_Offer_All()
        LvSuppliers.Visible = False
    End Sub

    Private Sub BtnBatalkan_Click(sender As Object, e As EventArgs) Handles BtnBatalkan.Click
        Dim cntChecked As Integer = 0

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).Cells(0).Value IsNot Nothing AndAlso
                Not IsDBNull(DataGridView1.Rows(i).Cells(0).Value) AndAlso
                CBool(DataGridView1.Rows(i).Cells(0).Value) Then
                cntChecked += 1
            End If
        Next

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
                Dim isChecked As Boolean = False
                If DataGridView1.Rows(i).Cells(0).Value IsNot Nothing AndAlso
                    Not IsDBNull(DataGridView1.Rows(i).Cells(0).Value) Then
                    isChecked = CBool(DataGridView1.Rows(i).Cells(0).Value)
                End If

                If isChecked Then
                    Dim noUrut As String = If(IsDBNull(DataGridView1.Rows(i).Cells(1).Value), "", DataGridView1.Rows(i).Cells(1).Value.ToString().Trim())
                    Dim noPR As String = If(IsDBNull(DataGridView1.Rows(i).Cells(2).Value), "", DataGridView1.Rows(i).Cells(2).Value.ToString().Trim())
                    Dim kodeBarang As String = If(IsDBNull(DataGridView1.Rows(i).Cells(3).Value), "", DataGridView1.Rows(i).Cells(3).Value.ToString().Trim())
                    Dim noPenawaranLama As String = If(IsDBNull(DataGridView1.Rows(i).Cells(5).Value), "", DataGridView1.Rows(i).Cells(5).Value.ToString().Trim())
                    Dim keteranganBaru As String = If(IsDBNull(DataGridView1.Rows(i).Cells(10).Value), "", DataGridView1.Rows(i).Cells(10).Value.ToString().Trim())

                    If String.IsNullOrEmpty(noUrut) OrElse String.IsNullOrEmpty(kodeBarang) Then
                        Continue For
                    End If

                    Dim sqlSelect = "SELECT No_Penawaran, Keterangan_Penawaran FROM EMI_Purchase_Requisition_Detail WHERE No_Urut = @NoUrut AND Kode_Barang = @KodeBarang"

                    Cmd.Parameters.Clear()
                    Cmd.Parameters.AddWithValue("@NoUrut", noUrut)
                    Cmd.Parameters.AddWithValue("@KodeBarang", kodeBarang)

                    Dim keteranganLama As String = ""

                    Dim drOld = OpenTrans(sqlSelect)
                    If drOld.Read Then
                        keteranganLama = If(IsDBNull(drOld("Keterangan_Penawaran")), "", drOld("Keterangan_Penawaran").ToString().Trim())
                    End If
                    drOld.Close()

                    Dim sqlUpdate = "UPDATE EMI_Purchase_Requisition_Detail SET Status_Validasi = 'T', No_Penawaran = NULL, Keterangan_Penawaran = NULL WHERE No_Urut = @NoUrut AND Kode_Barang = @KodeBarang"

                    Cmd.Parameters.Clear()
                    Cmd.Parameters.AddWithValue("@NoUrut", noUrut)
                    Cmd.Parameters.AddWithValue("@KodeBarang", kodeBarang)
                    Cmd.CommandText = sqlUpdate
                    Cmd.ExecuteNonQuery()

                    cntUpdated += 1

                    Dim sqlLog = "INSERT INTO N_EMI_LOG_Purchase_Requisition_Detail (Kode_Perusahaan, Tanggal, Jam, UserID, No_Faktur, No_Urut, No_Penawaran_Lama, No_Penawaran_Baru, Keterangan_Lama, Keterangan_Baru) VALUES (@KodePerusahaan, @Tanggal, @Jam, @UserID, @NoFaktur, @NoUrut, @NoPenawaranLama, @NoPenawaranBaru, @KeteranganLama, @KeteranganBaru)"

                    Cmd.Parameters.Clear()
                    Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
                    Cmd.Parameters.AddWithValue("@Tanggal", tanggal)
                    Cmd.Parameters.AddWithValue("@Jam", jam)
                    Cmd.Parameters.AddWithValue("@UserID", UserID)
                    Cmd.Parameters.AddWithValue("@NoFaktur", noPR)
                    Cmd.Parameters.AddWithValue("@NoUrut", noUrut)
                    Cmd.Parameters.AddWithValue("@NoPenawaranLama", If(String.IsNullOrEmpty(noPenawaranLama), DBNull.Value, CObj(noPenawaranLama)))
                    Cmd.Parameters.AddWithValue("@NoPenawaranBaru", DBNull.Value)
                    Cmd.Parameters.AddWithValue("@KeteranganLama", If(String.IsNullOrEmpty(keteranganLama), DBNull.Value, CObj(keteranganLama)))
                    Cmd.Parameters.AddWithValue("@KeteranganBaru", If(String.IsNullOrEmpty(keteranganBaru), DBNull.Value, CObj(keteranganBaru)))
                    Cmd.CommandText = sqlLog
                    Cmd.ExecuteNonQuery()
                End If
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

        Kosong()

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

        Try
            OpenConn()

            SQL = $"
                select Kode_Stock_Owner, jumlah, Satuan, tanggal_delivery,
	                DateAdd(Day, c.Waktu_Pabrikasi + c.Waktu_Pengiriman, '2025-12-06') as tanggal_actual_delivery
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
End Class