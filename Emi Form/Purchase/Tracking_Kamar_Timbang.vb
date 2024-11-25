Public Class Tracking_Kamar_Timbang

    Dim Dgv_NoFak, Dgv_Lokasi, Dgv_Supplier, Dgv_NoSJ, Dgv_TglSJ, Dgv_TglSampai, Dgv_TglBerangkat, Dgv_Status As String
    Dim Dgv_Ekspedisi, Dgv_Driver, Dgv_NoPlat, Dgv_ETA As String

    Dim cellNoFaktur As Integer = 0
    Dim cellLokasi As Integer = 1
    Dim celSupplier As Integer = 2
    Dim cellNoSJ As Integer = 3
    Dim cellTanggalBerangkat As Integer = 4
    Dim cellStatus As Integer = 5
    Dim cellTanggalSampai As Integer = 6
    Dim cellEkspedisi As Integer = 7
    Dim cellDriver As Integer = 8
    Dim cellNoPlat As Integer = 9
    Dim cellETA As Integer = 10

    Private Sub Tracking_Kamar_Timbang_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_DetBahan.Columns.Add("Nama", 200, HorizontalAlignment.Center)
        Lv_DetBahan.Columns.Add("Lokasi", 200, HorizontalAlignment.Center)
        Lv_DetBahan.Columns.Add("Jumlah Masuk", 130, HorizontalAlignment.Center)
        Lv_DetBahan.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        Lv_DetBahan.View = View.Details

        kosong()

    End Sub

    Private Sub kosong()
        Load_Track()
        Lv_DetBahan.Items.Clear()
    End Sub

    Private Sub Get_DGV_Rows(ByVal Index As Integer)
        Dgv_NoFak = Dgv_Tracking.Rows(Index).Cells(cellNoFaktur).Value
        Dgv_Lokasi = Dgv_Tracking.Rows(Index).Cells(cellLokasi).Value
        Dgv_Supplier = Dgv_Tracking.Rows(Index).Cells(celSupplier).Value
        Dgv_NoSJ = Dgv_Tracking.Rows(Index).Cells(cellNoSJ).Value
        Dgv_TglBerangkat = Dgv_Tracking.Rows(Index).Cells(cellTanggalBerangkat).Value
        Dgv_Status = Dgv_Tracking.Rows(Index).Cells(cellStatus).Value
        Dgv_TglSampai = Dgv_Tracking.Rows(Index).Cells(cellTanggalSampai).Value
        Dgv_Ekspedisi = Dgv_Tracking.Rows(Index).Cells(cellEkspedisi).Value
        Dgv_Driver = Dgv_Tracking.Rows(Index).Cells(cellDriver).Value
        Dgv_NoPlat = Dgv_Tracking.Rows(Index).Cells(cellNoPlat).Value
        Dgv_ETA = Dgv_Tracking.Rows(Index).Cells(cellETA).Value
    End Sub

    Private Sub Load_Track(Optional filter As String = "")
        Try
            OpenConn()

            Dim idx As Integer = 0

            Dgv_Tracking.Rows.Clear()

            SQL = ";With cte as ( Select "
            SQL = SQL & "a.No_Faktur, a.Lokasi, c.Nama as Supplier, a.No_SJ, a.No_Plat, a.Driver, a.Tanggal as tanggal_sampai, a.Jam, b.Nama_Ekspedisi, "
            SQL = SQL & "a.Tanggal_OTW, a.ETA, "

            SQL = SQL & "ISNULL((Select top 1 'Y' from EMI_Pembelian_Loading_Detail z where z.No_Faktur = a.no_Faktur "
            SQL = SQL & "and z.Flag_Timbang_Masuk is null and z.Flag_Sudah_Bongkar_Android is null and z.Flag_Timbang_Keluar is null), '-') as Masuk, "

            SQL = SQL & "ISNULL((select top 1 'Y' from EMI_Pembelian_Loading_Detail z where z.No_Faktur = a.No_Faktur "
            SQL = SQL & "and z.Flag_Timbang_Masuk='Y' and z.Flag_Sudah_Bongkar_Android IS NULL and z.Flag_Timbang_Keluar is null), '-') as Bongkar, "

            SQL = SQL & "ISNULL((select top 1 'Y' from EMI_Pembelian_Loading_Detail z where z.No_Faktur = a.No_Faktur "
            SQL = SQL & "and z.Flag_Timbang_Masuk='Y' and z.Flag_Sudah_Bongkar_Android='Y' and z.Flag_Timbang_Keluar is null), '-') as Keluar, "

            SQL = SQL & "ISNULL((select top 1 'Y' from EMI_Pembelian_Loading_Detail z where z.No_Faktur = a.No_Faktur "
            SQL = SQL & "and z.Flag_Timbang_Masuk='Y' and z.Flag_Sudah_Bongkar_Android='Y' and z.Flag_Timbang_Keluar='Y'), '-') as Selesai "

            SQL = SQL & "From EMI_Pembelian_Loading a, EMI_Master_Ekspedisi b, Suppliers c "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Ekspedisi=b.id_ekspedisi "
            SQL = SQL & "and a.Kode_Perusahaan=c.Kode_Perusahaan and a.Kode_Supplier=c.Kode_Supplier ) "
            SQL = SQL & "select * from cte "

            If Not String.IsNullOrWhiteSpace(filter) AndAlso Not filter.Trim.Length = 0 Then
                SQL = SQL & filter

            End If
            SQL = SQL & "ORDER BY ETA DESC "

            Using Dr = OpenTrans(SQL)

                Do While Dr.Read
                    Dgv_Tracking.Rows.Add(1)

                    Dgv_Tracking.Rows(idx).Cells(cellNoFaktur).Value = Dr("No_Faktur")
                    Dgv_Tracking.Rows(idx).Cells(cellLokasi).Value = Dr("Lokasi")
                    Dgv_Tracking.Rows(idx).Cells(celSupplier).Value = Dr("Supplier")
                    Dgv_Tracking.Rows(idx).Cells(cellNoSJ).Value = Dr("No_SJ")
                    Dgv_Tracking.Rows(idx).Cells(cellTanggalBerangkat).Value = Dr("Tanggal_OTW")

                    If Dr("Masuk") = "Y" Then
                        Dgv_Tracking.Rows(idx).Cells(cellStatus).Value = "Timbang Masuk" & Environment.NewLine &
                            "Tanggal : " & Format(Dr("tanggal_sampai"), "dd MMMM yyyy") & " " &
                            "Jam : " & Dr("Jam")
                        Dgv_Tracking.Rows(idx).Cells(cellStatus).Style.BackColor = Color.LightBlue

                    ElseIf Dr("Bongkar") = "Y" Then
                        Dgv_Tracking.Rows(idx).Cells(cellStatus).Value = "Proses Bongkar" & Environment.NewLine &
                           "Tanggal : " & Format(Dr("tanggal_sampai"), "dd MMMM yyyy") & " " &
                           "Jam : " & Dr("Jam")
                        Dgv_Tracking.Rows(idx).Cells(cellStatus).Style.BackColor = Color.LightYellow

                    ElseIf Dr("Keluar") = "Y" Then
                        Dgv_Tracking.Rows(idx).Cells(cellStatus).Value = "Timbang Keluar" & Environment.NewLine &
                           "Tanggal : " & Format(Dr("tanggal_sampai"), "dd MMMM yyyy") & " " &
                           "Jam : " & Dr("Jam")
                        Dgv_Tracking.Rows(idx).Cells(cellStatus).Style.BackColor = Color.LightGray

                    ElseIf Dr("Selesai") = "Y" Then
                        Dgv_Tracking.Rows(idx).Cells(cellStatus).Value = "Selesai" & Environment.NewLine &
                           "Tanggal : " & Format(Dr("tanggal_sampai"), "dd MMMM yyyy") & " " &
                           "Jam : " & Dr("Jam")
                        Dgv_Tracking.Rows(idx).Cells(cellStatus).Style.BackColor = Color.LightGreen
                    End If

                    'Hidden
                    Dgv_Tracking.Rows(idx).Cells(cellTanggalSampai).Value = Dr("tanggal_sampai")
                    Dgv_Tracking.Rows(idx).Cells(cellEkspedisi).Value = Dr("Nama_Ekspedisi")
                    Dgv_Tracking.Rows(idx).Cells(cellDriver).Value = Dr("Driver")
                    Dgv_Tracking.Rows(idx).Cells(cellNoPlat).Value = Dr("No_Plat")
                    Dgv_Tracking.Rows(idx).Cells(cellETA).Value = Dr("ETA")

                    idx += 1
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Dgv_Tracking_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Tracking.CellClick
        If Dgv_Tracking.RowCount = 0 Then Exit Sub

        Dim selectdRows As Integer = Dgv_Tracking.CurrentRow.Index
        Get_DGV_Rows(selectdRows)

        Tb_TanggalBerangkat.Text = Dgv_TglBerangkat
        Tb_Ekspedisi.Text = Dgv_Ekspedisi
        Tb_Driver.Text = Dgv_Driver
        Tb_NoPlat.Text = Dgv_NoPlat
        Tb_ETA.Text = Dgv_ETA

        Try
            OpenConn()

            Lv_DetBahan.Items.Clear()

            SQL = "select distinct b.Nama, a.Kode_Stock_Owner ,a.Jumlah_Masuk, a.Satuan_Barang "
            SQL = SQL & "From EMI_Pembelian_Loading_Detail a, Barang b "
            SQL = SQL & "where a.Kode_Perusahaan=b.kode_perusahaan and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.No_Faktur='" & Dgv_NoFak & "' "

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem
                    lv = Lv_DetBahan.Items.Add(Dr("Nama"))
                    lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    lv.SubItems.Add(isNUll(Dr("Jumlah_Masuk")))
                    lv.SubItems.Add(Dr("Satuan_Barang"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    'HANDLE BUTTON
    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        Dim filter As String = "where tanggal_sampai between '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' "
        filter = filter & "and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "'"

        Load_Track(filter)

    End Sub

    'HANDLE KEYPRESS
    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub

    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

    'UTLITY FUNCTION
    Public Shared Function isNUll(ByVal xNullString As Object) As String
        Try
            If IsDBNull(xNullString) Then
                Return "0"
            Else
                Return xNullString
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try
    End Function

End Class