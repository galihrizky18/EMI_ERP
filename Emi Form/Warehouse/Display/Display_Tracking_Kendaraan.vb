Public Class Display_Tracking_Kendaraan

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

        Lv_DetBahan.Columns.Add("Nama", 270, HorizontalAlignment.Center)
        Lv_DetBahan.Columns.Add("Lokasi", 130, HorizontalAlignment.Center)
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

            SQL = ";With cte as ( Select a.No_Faktur, a.Lokasi, c.Nama as Supplier, a.No_SJ, a.No_Plat, a.Driver, a.Tanggal as tanggal_sampai,  "
            SQL = SQL & "a.Jam, a.Tanggal_OTW, a.ETA, "


            SQL = SQL & "isnull((select top(1) 'Y' from EMI_Pembelian_Loading x where a.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = x.No_Faktur and a.Flag_Security is null and a.Flag_QC_Pertama is null  "
            SQL = SQL & "and a.Flag_Timbang is null and a.Flag_Proses_Loading is null and a.Flag_Timbang_Keluar is null ),'-') as Barang_Dalam_Perjalanan,"

            SQL = SQL & "isnull((select top(1) 'Y' from EMI_Pembelian_Loading x where a.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = x.No_Faktur and a.Flag_Security = 'Y' and a.Flag_QC_Pertama is null  "
            SQL = SQL & "and a.Flag_Timbang is null and a.Flag_Proses_Loading is null and a.Flag_Timbang_Keluar is null ),'-') as Flag_Security,"

            SQL = SQL & "isnull((select top(1) 'Y' from EMI_Pembelian_Loading x where a.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = x.No_Faktur and a.Flag_Security = 'Y' and a.Flag_QC_Pertama = 'Y'  "
            SQL = SQL & "and a.Flag_Timbang is null and a.Flag_Proses_Loading is null and a.Flag_Timbang_Keluar is null),'-') as Flag_QC, "

            SQL = SQL & "isnull((select top(1) x.Hasil from EMI_Hasil_Quality_Control  x where a.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = x.No_Fak_Loading_Barang and a.Flag_Security = 'Y' and a.Flag_QC_Pertama = 'Y' "
            SQL = SQL & "and a.Flag_Timbang is null and a.Flag_Proses_Loading is null and a.Flag_Timbang_Keluar is null order by x.Step desc),'-') as Keterangan_QC, "

            SQL = SQL & "isnull((select top(1) cast( x.jenis_qc as varchar(10)) from EMI_Hasil_Quality_Control  x where a.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = x.No_Fak_Loading_Barang and a.Flag_Security = 'Y' and a.Flag_QC_Pertama = 'Y'  "
            SQL = SQL & "and a.Flag_Timbang is null and a.Flag_Proses_Loading is null and a.Flag_Timbang_Keluar is null order by x.Step desc),'-') as jenis_qc, "


            SQL = SQL & "isnull((select top(1) cast( x.Step as varchar(10)) from EMI_Hasil_Quality_Control  x where a.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = x.No_Fak_Loading_Barang and a.Flag_Security = 'Y' and a.Flag_QC_Pertama = 'Y'  "
            SQL = SQL & "and a.Flag_Timbang is null and a.Flag_Proses_Loading is null and a.Flag_Timbang_Keluar is null order by x.Step desc),'-') as Step_QC, "

            SQL = SQL & "isnull((select top(1) 'Y' from EMI_Pembelian_Loading x where a.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = x.No_Faktur and a.Flag_Security = 'Y' and a.Flag_QC_Pertama = 'Y'  "
            SQL = SQL & "and a.Flag_Timbang ='Y' and a.Flag_Proses_Loading is null and a.Flag_Timbang_Keluar is null),'-') as Bongkar, "

            SQL = SQL & "isnull((select top(1) 'Y' from EMI_Pembelian_Loading x where a.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = x.No_Faktur and a.Flag_Security = 'Y' and a.Flag_QC_Pertama = 'Y'  "
            SQL = SQL & "and a.Flag_Timbang ='Y' and a.Flag_Proses_Loading = 'Y' and a.Flag_Timbang_Keluar is null),'-') as Keluar,"

            SQL = SQL & "isnull((select top(1) 'Y' from EMI_Pembelian_Loading x where a.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = x.No_Faktur and a.Flag_Security = 'Y' and a.Flag_QC_Pertama = 'Y'  "
            SQL = SQL & "and a.Flag_Timbang ='Y' and a.Flag_Proses_Loading = 'Y' and a.Flag_Timbang_Keluar = 'Y'),'-') as Selesai "

            SQL = SQL & "From EMI_Pembelian_Loading a, Suppliers c where   a.Kode_Perusahaan=c.Kode_Perusahaan and a.Kode_Supplier=c.Kode_Supplier ) select * from cte "



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
                    Dgv_Tracking.Rows(idx).Cells(cellTanggalBerangkat).Value = Format(Dr("Tanggal_OTW"), "dd MMMM yyyy")

                    If Dr("Flag_Security") = "Y" Then
                        Dgv_Tracking.Rows(idx).Cells(cellStatus).Value = "Cek Security" & Environment.NewLine &
                            "Tanggal : " & Format(Dr("tanggal_sampai"), "dd MMMM yyyy") & " " &
                            "Jam : " & Dr("Jam")
                        Dgv_Tracking.Rows(idx).Cells(cellStatus).Style.BackColor = Color.LightBlue

                    ElseIf Dr("Flag_QC") = "Y" Then
                        Dgv_Tracking.Rows(idx).Cells(cellStatus).Value = "Quality Control Assisment " & Dr("jenis_qc") & " (" & Dr("step_qc") & ")" & Environment.NewLine &
                        "Catatan : " & Dr("keterangan_qc") & Environment.NewLine &
                           "Tanggal : " & Format(Dr("tanggal_sampai"), "dd MMMM yyyy") & " " &
                           "Jam : " & Dr("Jam")
                        Dgv_Tracking.Rows(idx).Cells(cellStatus).Style.BackColor = Color.LightYellow

                    ElseIf Dr("Bongkar") = "Y" Then
                        Dgv_Tracking.Rows(idx).Cells(cellStatus).Value = "Proses Bongkar" & Environment.NewLine &
                           "Tanggal : " & Format(Dr("tanggal_sampai"), "dd MMMM yyyy") & " " &
                           "Jam : " & Dr("Jam")
                        Dgv_Tracking.Rows(idx).Cells(cellStatus).Style.BackColor = Color.LightCyan

                    ElseIf Dr("Keluar") = "Y" Then
                        Dgv_Tracking.Rows(idx).Cells(cellStatus).Value = "Timbang Keluar " & Environment.NewLine &
                           "Tanggal : " & Format(Dr("tanggal_sampai"), "dd MMMM yyyy") & " " &
                           "Jam : " & Dr("Jam")
                        Dgv_Tracking.Rows(idx).Cells(cellStatus).Style.BackColor = Color.LightSkyBlue

                    ElseIf Dr("Selesai") = "Y" Then
                        Dgv_Tracking.Rows(idx).Cells(cellStatus).Value = "Selesai" & Environment.NewLine &
                           "Tanggal : " & Format(Dr("tanggal_sampai"), "dd MMMM yyyy") & " " &
                           "Jam : " & Dr("Jam")
                        Dgv_Tracking.Rows(idx).Cells(cellStatus).Style.BackColor = Color.LightGreen
                    Else
                        Dgv_Tracking.Rows(idx).Cells(cellStatus).Value = "Barang Dalam Perjalanan" & Environment.NewLine &
                           "Tanggal : " & Format(Dr("tanggal_sampai"), "dd MMMM yyyy") & " " &
                           "Jam : " & Dr("Jam")
                        Dgv_Tracking.Rows(idx).Cells(cellStatus).Style.BackColor = Color.LightGray
                    End If

                    'Hidden
                    Dgv_Tracking.Rows(idx).Cells(cellTanggalSampai).Value = Format(Dr("tanggal_sampai"), "dd MMNM yyyy")
                    '  Dgv_Tracking.Rows(idx).Cells(cellEkspedisi).Value = Dr("Nama_Ekspedisi")
                    Dgv_Tracking.Rows(idx).Cells(cellDriver).Value = Dr("Driver")
                    Dgv_Tracking.Rows(idx).Cells(cellNoPlat).Value = Dr("No_Plat")
                    Dgv_Tracking.Rows(idx).Cells(cellETA).Value = Format(Dr("ETA"), "dd MMMM yyyy")

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

            'SQL = "select distinct b.Nama, a.Kode_Stock_Owner,a.kode_barang ,a.Jumlah_Masuk, a.Satuan_Barang "
            'SQL = SQL & "From EMI_Pembelian_Loading_Detail a, Barang b "
            'SQL = SQL & "where a.Kode_Perusahaan=b.kode_perusahaan and a.Kode_Barang = b.Kode_Barang "
            'SQL = SQL & "and a.No_Faktur='" & Dgv_NoFak & "' "
            'Using Ds = BindingTrans(SQL)
            '    With Ds.Tables("MyTable")
            '        If .Rows.Count <> 0 Then

            '            For i As Integer = 0 To .Rows.Count - 1
            '                Dim lv As New ListViewItem
            '                lv = Lv_DetBahan.Items.Add(.)
            '                lv.SubItems.Add(Dr("Kode_Stock_Owner"))
            '                lv.SubItems.Add(Format(isNUll(Dr("Jumlah_Masuk")), "N2"))
            '                lv.SubItems.Add(Dr("Satuan_Barang"))


            '            Next

            '        End If
            '    End With
            'End Using


            SQL = "select distinct b.Nama, a.Kode_Stock_Owner ,a.jumlah, a.satuan "
            SQL = SQL & "From EMI_Pembelian_Loading_Detail a, Barang b "
            SQL = SQL & "where a.Kode_Perusahaan=b.kode_perusahaan and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.No_Faktur='" & Dgv_NoFak & "' "

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem
                    lv = Lv_DetBahan.Items.Add(Dr("Nama"))
                    lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    lv.SubItems.Add(Format(Dr("jumlah"), "N2"))
                    lv.SubItems.Add(Dr("satuan"))
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