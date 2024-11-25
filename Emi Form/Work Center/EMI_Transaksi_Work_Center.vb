Public Class EMI_Transaksi_Work_Center
    Dim arrBulan, arrBulanMM As New ArrayList

    Dim LvSO As String
    Dim LvKd_Brg As String
    Dim LvNm_Brg As String
    Dim LvId As String
    Dim LVKeterangan As String
    Dim LvTotal As String
    Dim LvNilai_Per_Pcs As String

    Dim CellSO As Integer = 0
    Dim CellKd_Brg As Integer = 1
    Dim CellNm_Brg As Integer = 2
    Dim CellId As Integer = 3
    Dim CellKeterangan As Integer = 4

    Private Sub Emi_Transaksi_Work_Center_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Emi_Transaksi_Work_Center_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Kosong()
    End Sub

    Private Sub Kosong()
        get_jam()

        Try
            OpenConn()

            CmbBulan.Items.Clear() : arrBulan.Clear() : arrBulanMM.Clear()
            CmbBulan.Items.Add("January") : arrBulan.Add("1") : arrBulanMM.Add("01")
            CmbBulan.Items.Add("February") : arrBulan.Add("2") : arrBulanMM.Add("02")
            CmbBulan.Items.Add("Maret") : arrBulan.Add("3") : arrBulanMM.Add("03")
            CmbBulan.Items.Add("April") : arrBulan.Add("4") : arrBulanMM.Add("04")
            CmbBulan.Items.Add("Mei") : arrBulan.Add("5") : arrBulanMM.Add("05")
            CmbBulan.Items.Add("Juni") : arrBulan.Add("6") : arrBulanMM.Add("06")
            CmbBulan.Items.Add("Juli") : arrBulan.Add("7") : arrBulanMM.Add("07")
            CmbBulan.Items.Add("Agustus") : arrBulan.Add("8") : arrBulanMM.Add("08")
            CmbBulan.Items.Add("September") : arrBulan.Add("9") : arrBulanMM.Add("09")
            CmbBulan.Items.Add("Oktober") : arrBulan.Add("10") : arrBulanMM.Add("10")
            CmbBulan.Items.Add("November") : arrBulan.Add("11") : arrBulanMM.Add("11")
            CmbBulan.Items.Add("Desember") : arrBulan.Add("12") : arrBulanMM.Add("12")

            CmbBulan.SelectedIndex = CInt(Format(Now.Date, "MM")) - 1
            'CmbBulan.SelectedIndex = -1
            CmbBulan.Enabled = True

            CmbTahun.Items.Clear()
            Dim tahun_awal As Integer = Date.Now.Year - 2
            Dim tahun_akhir As Integer = Date.Now.Year + 2
            For a As Integer = tahun_awal To tahun_akhir
                CmbTahun.Items.Add(a)
            Next
            CmbTahun.Text = Format(Now.Date, "yyyy")
            CmbTahun.Enabled = True

            CmbLokasi.Items.Clear()
            SQL = "select Kode_Stock_Owner from Stock_Owner "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        CmbLokasi.Items.Add(.Rows(i).Item("Kode_Stock_Owner"))
                    Next
                End With
            End Using
            CmbLokasi.Text = Lokasi

            Dim lok_gudang As String = ""
            SQL = "select Kode_Stock_Owner_Gudang from binding_lokasi_gudang where gudang_default = 'Y' and kode_stock_owner ='" & CmbLokasi.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    lok_gudang = Dr("Kode_Stock_Owner_Gudang")
                End If
            End Using

            DataGridView1.Rows.Clear()
            DataGridView1.Columns.Clear()

            DataGridView1.Columns.Add("Kode_SO", "Kode SO")
            DataGridView1.Columns(0).Width = 0
            DataGridView1.Columns(0).Visible = False

            DataGridView1.Columns.Add("Kode_Barang", "Kode Barang")
            DataGridView1.Columns(1).Width = 200

            DataGridView1.Columns.Add("Nama_Barang", "Nama Barang")
            DataGridView1.Columns(2).Width = 200

            DataGridView1.Columns.Add("ID", "IDSO")
            DataGridView1.Columns(3).Width = 0
            DataGridView1.Columns(3).Visible = False

            DataGridView1.Columns.Add("Nama", "Nama")
            DataGridView1.Columns(4).Width = 200

            For i As Integer = 0 To 4
                DataGridView1.Columns(i).ReadOnly = True
            Next

            BtnSimpan.Enabled = True

            get_no_faktur(arrBulanMM(CmbBulan.SelectedIndex) & Strings.Right(CmbTahun.Text, 2))

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub get_no_faktur(ByVal BulanTahun As String)
        Dim FPro_Results As String = "TCC"
        TxtBarangMasuk_NoFaktur.Text = FPro_Results & BulanTahun & "-" &
                             General_Class.Get_Last_Number2("Emi_Transaksi_work_Center", "No_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Faktur, 1, " & Len(FPro_Results) + 4 & ")", FPro_Results & BulanTahun)
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        If Not IsNumeric(DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
            DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 0
        End If
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        Kosong()
    End Sub

    Private Sub Data()
        get_jam()

        '============================================================================================================================
        ' TAMBAH KOLOM SESUAI JENIS BIAYA
        '============================================================================================================================
        Dim ColNum As Integer = 5
        Try
            OpenConn()

            SQL = "Select kode_jenis_biaya_produksi from emi_jenis_biaya_produksi where kode_perusahaan = '" & KodePerusahaan & "'"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    DataGridView1.Columns.Add(dr("kode_jenis_biaya_produksi"), dr("kode_jenis_biaya_produksi"))
                    DataGridView1.Columns(ColNum).Width = 130
                    DataGridView1.Columns(ColNum).ReadOnly = False
                    DataGridView1.Columns(ColNum).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    ColNum += 1
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        '============================================================================================================================

        Try
            OpenConn()

            SQL = "select a.No_Faktur,b.Kode_Stock_Owner,b.Kode_Barang,d.Nama,b.Id_Work_Center,c.Keterangan,b.Total,b.Nilai_Per_pcs "
            SQL = SQL & "from Emi_Transaksi_Work_Center a,Emi_Transaksi_Work_Center_Detail b,EMI_Master_Work_Center c,Barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.Status is null "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Work_Center = c.Id_Work_Center "
            SQL = SQL & "and b.Kode_Perusahaan = d.Kode_Perusahaan and b.kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & arrBulanMM.Item(CmbBulan.SelectedIndex) & "' "
            SQL = SQL & "and a.Tahun = '" & CmbTahun.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        TxtBarangMasuk_NoFaktur.Text = .Rows(0).Item("No_Faktur")

                        DataGridView1.Rows.Clear()
                        For i As Integer = 0 To .Rows.Count - 1
                            DataGridView1.Rows.Add(1)
                            DataGridView1.Rows(i).Cells(CellSO).Value = .Rows(i).Item("Kode_Stock_Owner")
                            DataGridView1.Rows(i).Cells(CellKd_Brg).Value = .Rows(i).Item("Kode_Barang")
                            DataGridView1.Rows(i).Cells(CellNm_Brg).Value = .Rows(i).Item("Nama")
                            DataGridView1.Rows(i).Cells(CellId).Value = .Rows(i).Item("Id_Work_Center")
                            DataGridView1.Rows(i).Cells(CellKeterangan).Value = .Rows(i).Item("Keterangan")

                            '========================================================================================================================================================================================
                            ' CEK DATA DETAIL PER MESIN
                            '========================================================================================================================================================================================
                            ColNum = 5

                            SQL = "SELECT  dbo.Emi_Transaksi_Work_Center_Detail_Per_Mesin.Jenis_Biaya, dbo.Emi_Transaksi_Work_Center_Detail_Per_Mesin.Total "

                            SQL = SQL & "FROM dbo.Emi_Transaksi_Work_Center INNER JOIN "
                            SQL = SQL & "dbo.Emi_Transaksi_Work_Center_Detail ON dbo.Emi_Transaksi_Work_Center.Kode_Perusahaan = dbo.Emi_Transaksi_Work_Center_Detail.Kode_Perusahaan AND "
                            SQL = SQL & "dbo.Emi_Transaksi_Work_Center.No_Faktur = dbo.Emi_Transaksi_Work_Center_Detail.No_Faktur INNER JOIN "
                            SQL = SQL & "dbo.Emi_Transaksi_Work_Center_Detail_Per_Mesin ON dbo.Emi_Transaksi_Work_Center_Detail.Kode_Perusahaan = dbo.Emi_Transaksi_Work_Center_Detail_Per_Mesin.Kode_Perusahaan AND "
                            SQL = SQL & "dbo.Emi_Transaksi_Work_Center_Detail.No_Faktur = dbo.Emi_Transaksi_Work_Center_Detail_Per_Mesin.No_Faktur AND "
                            SQL = SQL & "dbo.Emi_Transaksi_Work_Center_Detail.Kode_Stock_Owner = dbo.Emi_Transaksi_Work_Center_Detail_Per_Mesin.Kode_Stock_Owner AND "
                            SQL = SQL & "dbo.Emi_Transaksi_Work_Center_Detail.Kode_Barang = dbo.Emi_Transaksi_Work_Center_Detail_Per_Mesin.Kode_Barang AND "
                            SQL = SQL & "dbo.Emi_Transaksi_Work_Center_Detail.Id_Work_Center = dbo.Emi_Transaksi_Work_Center_Detail_Per_Mesin.Id_Work_Center "

                            SQL = SQL & "WHERE dbo.Emi_Transaksi_Work_Center.Bulan = '" & arrBulanMM.Item(CmbBulan.SelectedIndex) & "' AND dbo.Emi_Transaksi_Work_Center.Tahun = '" & CmbTahun.Text & "' "

                            SQL = SQL & "and dbo.Emi_Transaksi_Work_Center_Detail_Per_Mesin.Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' "
                            SQL = SQL & "and dbo.Emi_Transaksi_Work_Center_Detail_Per_Mesin.Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                            SQL = SQL & "and dbo.Emi_Transaksi_Work_Center_Detail_Per_Mesin.Id_Work_Center = '" & .Rows(i).Item("Id_Work_Center") & "' "

                            Using DS1 = BindingTrans(SQL)
                                If DS1.Tables("MyTable").Rows.Count <> 0 Then
                                    For z As Integer = 0 To DS1.Tables("MyTable").Rows.Count - 1

                                        For j = ColNum To DataGridView1.Columns.Count - 1
                                            If DS1.Tables("MyTable").Rows(z).Item("jenis_biaya") = DataGridView1.Columns(j).HeaderText Then
                                                DataGridView1.Rows(i).Cells(j).Value = DS1.Tables("MyTable").Rows(z).Item("total")
                                                Exit For
                                            End If
                                        Next

                                    Next
                                End If
                            End Using

                            '========================================================================================================================================================================================

                        Next

                        CmbBulan.Enabled = False
                        CmbTahun.Enabled = False
                        BtnSimpan.Enabled = False

                    Else

                        get_no_faktur(arrBulanMM(CmbBulan.SelectedIndex) & Strings.Right(CmbTahun.Text, 2))

                        CmbBulan.Enabled = True
                        CmbTahun.Enabled = True
                        BtnSimpan.Enabled = True

                        Dim lok_gudang As String = ""
                        SQL = "select Kode_Stock_Owner_Gudang from binding_lokasi_gudang where gudang_default = 'Y' and kode_stock_owner ='" & CmbLokasi.Text & "' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                lok_gudang = Dr("Kode_Stock_Owner_Gudang")
                            End If
                        End Using

                        DataGridView1.Rows.Clear()
                        SQL = "Select a.Kode_Stock_Owner,a.Kode_Barang,a.Nama,e.Id_Work_Center,e.Keterangan from "
                        SQL = SQL & "barang a, EMI_Group_Jenis b, EMI_Master_Routing c, emi_master_routing_detail d, EMI_Master_Work_Center e "
                        SQL = SQL & "where a.Kode_Perusahaan ='" & KodePerusahaan & "' and a.Kode_Stock_Owner='" & lok_gudang & "' "
                        SQL = SQL & " And a.kode_Perusahaan = b.Kode_Perusahaan And a.Id_Group_Jenis = b.Id_Group_Jenis And b.flag_Finished_Good ='Y' "
                        SQL = SQL & "And a.Kode_Perusahaan = c.Kode_Perusahaan And a.Id_routing = c.Id_Routing "
                        SQL = SQL & "And c.Kode_Perusahaan=d.Kode_Perusahaan And c.Id_routing=d.Id_Routing "
                        SQL = SQL & "And d.Kode_Perusahaan=e.Kode_Perusahaan And d.id_work_center=e.id_work_center "
                        SQL = SQL & "Order By a.Kode_Barang "
                        Using Ds2 = BindingTrans(SQL)
                            With Ds2.Tables("MyTable")
                                For i As Integer = 0 To .Rows.Count - 1
                                    DataGridView1.Rows.Add(1)

                                    DataGridView1.Rows(i).Cells(CellSO).Value = .Rows(i).Item("Kode_Stock_Owner")
                                    DataGridView1.Rows(i).Cells(CellKd_Brg).Value = .Rows(i).Item("Kode_Barang")
                                    DataGridView1.Rows(i).Cells(CellNm_Brg).Value = .Rows(i).Item("Nama")
                                    DataGridView1.Rows(i).Cells(CellId).Value = .Rows(i).Item("Id_Work_Center")
                                    DataGridView1.Rows(i).Cells(CellKeterangan).Value = .Rows(i).Item("Keterangan")
                                Next
                            End With
                        End Using

                    End If
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvSO = DataGridView1.Rows(No_Index).Cells(CellSO).Value.ToString
        LvKd_Brg = DataGridView1.Rows(No_Index).Cells(CellKd_Brg).Value.ToString
        LvNm_Brg = DataGridView1.Rows(No_Index).Cells(CellNm_Brg).Value.ToString
        LvId = DataGridView1.Rows(No_Index).Cells(CellId).Value.ToString
        LVKeterangan = DataGridView1.Rows(No_Index).Cells(CellKeterangan).Value.ToString
    End Sub

    Private Sub BtnCari_Click(sender As Object, e As EventArgs) Handles BtnCari.Click
        If CmbLokasi.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbLokasi.Focus() : Exit Sub
        ElseIf CmbBulan.SelectedIndex = -1 Then
            MessageBox.Show("Bulan harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbBulan.Focus() : Exit Sub
        ElseIf CmbTahun.SelectedIndex = -1 Then
            MessageBox.Show("Tahun harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbTahun.Focus() : Exit Sub
        End If

        Data()
    End Sub

    Private Sub CmbBulan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbBulan.SelectedIndexChanged
        If CmbBulan.SelectedIndex = -1 Then Exit Sub
        If CmbTahun.SelectedIndex = -1 Then Exit Sub

        OpenConn()

        get_no_faktur(arrBulanMM(CmbBulan.SelectedIndex) & Strings.Right(CmbTahun.Text, 2))
    End Sub

    Private Sub CmbTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbTahun.SelectedIndexChanged
        If CmbBulan.SelectedIndex = -1 Then Exit Sub
        If CmbTahun.SelectedIndex = -1 Then Exit Sub

        OpenConn()

        get_no_faktur(arrBulanMM(CmbBulan.SelectedIndex) & Strings.Right(CmbTahun.Text, 2))
    End Sub

    Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles BtnSimpan.Click
        get_jam()
        If TxtBarangMasuk_NoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show("No transaksi Harus diisi....!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtBarangMasuk_NoFaktur.Focus() : Exit Sub
        ElseIf CmbBulan.Text.Trim.Length = 0 Then
            MessageBox.Show("Bulan Harus diisi....!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbBulan.Focus() : Exit Sub
        ElseIf CmbTahun.Text.Trim.Length = 0 Then
            MessageBox.Show("Tahun Harus diisi....!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbTahun.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            SQL = "INSERT INTO Emi_Transaksi_Work_Center(Kode_Perusahaan,No_Faktur,Bulan,Tahun,UserID,Tanggal,Jam) VALUES("
            SQL = SQL & "'" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & arrBulanMM.Item(CmbBulan.SelectedIndex) & "',"
            SQL = SQL & "'" & CmbTahun.Text & "','" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "') "
            ExecuteTrans(SQL)

            'Emi_Transaksi_Work_Center_detail
            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                Dim Total As Double = 0
                For j As Integer = 5 To DataGridView1.Columns.Count - 1
                    Total += DataGridView1.Rows(i).Cells(j).Value
                Next

                Get_Isi_Listview(i)

                SQL = "INSERT INTO Emi_Transaksi_Work_Center_Detail (kode_perusahaan,no_faktur,kode_stock_owner,kode_barang,id_work_center,total,nilai_per_pcs)"
                SQL = SQL & "Values('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & LvSO & "','"
                SQL = SQL & LvKd_Brg & "','" & LvId & "','" & Total & "','" & Total & "')"
                ExecuteTrans(SQL)
            Next

            'Emi_Transaksi_Work_Center_Detail_Per_Mesin
            For i As Integer = 0 To DataGridView1.Rows.Count - 1

                Get_Isi_Listview(i)

                For j As Integer = 5 To DataGridView1.Columns.Count - 1
                    SQL = "INSERT INTO Emi_Transaksi_Work_Center_Detail_Per_Mesin(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Id_Work_Center,Jenis_Biaya,Total,Nilai_Per_Pcs) "
                    SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtBarangMasuk_NoFaktur.Text & "','" & LvSO & "','" & LvKd_Brg & "','" & LvId & "','"
                    SQL = SQL & DataGridView1.Columns(j).HeaderText & "','" & DataGridView1.Rows(i).Cells(j).Value & "','" & DataGridView1.Rows(i).Cells(j).Value & "')"
                    ExecuteTrans(SQL)
                Next
            Next

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Data berhasil disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Kosong()
    End Sub
End Class