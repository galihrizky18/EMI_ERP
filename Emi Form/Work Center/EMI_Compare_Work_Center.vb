Imports CrystalDecisions.CrystalReports.Engine

Public Class EMI_Compare_Work_Center

    Dim dataCompare As String() = {"Automation", "Real", "Budgeting"}

    Dim arrBulan, arrBulanMM, colLoad As New ArrayList
    Dim biayaCheck, biayaCheckKeterangan As New ArrayList

    Dim dgv1_KdBiaya, dgv1_Biaya, dgv1_Checklist As String

    Dim cell1_KodeBiaya As Integer = 0
    Dim cell1_Biaya As Integer = 1
    Dim cell1_Checklistt As Integer = 2

    Dim cell2_KodeSO As Integer = 0
    Dim cell2_KodeBarang As Integer = 1
    Dim cell2_NamaBarang As Integer = 2
    Dim cell2_Mesin As Integer = 3

    Private Sub EMI_Compare_Work_Center_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        kosong()

    End Sub

    Private Sub kosong()
        dgv_biaya.Rows.Clear()
        dgv_workcenter.Rows.Clear()

        CmbBulan.Items.Clear()
        colLoad.Clear()

        biayaCheck.Clear()
        biayaCheckKeterangan.Clear()

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
        CmbBulan.Enabled = True

        CmbTahun.Items.Clear()
        Dim tahun_awal As Integer = Date.Now.Year - 2
        Dim tahun_akhir As Integer = Date.Now.Year + 2
        For a As Integer = tahun_awal To tahun_akhir
            CmbTahun.Items.Add(a)
        Next
        CmbTahun.Text = Format(Now.Date, "yyyy")
        CmbTahun.Enabled = True

        getDataBiaya()

    End Sub

    Private Sub get_data_dgv1(ByVal index As Integer)

        dgv1_KdBiaya = dgv_biaya.Rows(index).Cells(cell1_KodeBiaya).Value
        dgv1_Biaya = dgv_biaya.Rows(index).Cells(cell1_Biaya).Value
        dgv1_Checklist = dgv_biaya.Rows(index).Cells(cell1_Checklistt).Value

    End Sub

    Private Sub dgv_biaya_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_biaya.CellEndEdit

        If dgv_biaya.RowCount = 0 Then Exit Sub

        biayaCheck.Clear() : biayaCheckKeterangan.Clear()

        For i As Integer = 0 To dgv_biaya.RowCount - 1
            get_data_dgv1(i)

            If dgv1_Checklist = "True" Then
                biayaCheck.Add(dgv1_KdBiaya) : biayaCheckKeterangan.Add(dgv1_Biaya)
            End If

        Next

    End Sub

    Private Sub getDataBiaya()
        Try
            OpenConn()

            Dim row As Integer = 0
            SQL = "select Kode_Jenis_Biaya_Produksi, keterangan from emi_jenis_biaya_produksi where Kode_Perusahaan = '" & KodePerusahaan & "' order by Kode_Jenis_Biaya_Produksi"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    dgv_biaya.Rows.Add(1)
                    dgv_biaya.Rows(row).Cells(cell1_KodeBiaya).Value = Dr("Kode_Jenis_Biaya_Produksi")
                    dgv_biaya.Rows(row).Cells(cell1_Biaya).Value = Dr("keterangan")

                    row = row + 1
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub getData()
        If CmbBulan.SelectedIndex = -1 Or CmbTahun.SelectedIndex = -1 Then Exit Sub

        dgv_workcenter.Rows.Clear()
        colLoad.Clear()

        Try
            OpenConn()

            Dim columnDinamis As Integer = 4
            '=================================================
            '=     MENGHAPUS KOLOM MULAI DARI INDEX KE 4     =
            '=================================================
            For i As Integer = dgv_workcenter.Columns.Count - 1 To columnDinamis Step -1
                dgv_workcenter.Columns.RemoveAt(i)
            Next

            '===========================================================================================================================
            '====================
            '=     GET DATA     =
            '====================
            SQL = "SELECT a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, d.Nama, b.Id_Work_Center, c.Keterangan, b.Total, b.Nilai_Per_pcs "
            SQL = SQL & "FROM Emi_Transaksi_Work_Center a, Emi_Transaksi_Work_Center_Detail b, EMI_Master_Work_Center c, Barang d "
            SQL = SQL & "WHERE  a.Kode_Perusahaan = b.Kode_Perusahaan AND b.Kode_Perusahaan = c.Kode_Perusahaan AND b.kode_Stock_Owner = d.Kode_Stock_Owner "
            SQL = SQL & "AND a.No_Faktur = b.No_Faktur "
            SQL = SQL & "AND b.Id_Work_Center = c.Id_Work_Center "
            SQL = SQL & "AND b.kode_Stock_Owner = d.Kode_Stock_Owner AND b.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "AND a.Status IS NULL "
            SQL = SQL & "AND a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "AND a.Bulan = '" & arrBulanMM.Item(CmbBulan.SelectedIndex) & "' "
            SQL = SQL & "AND a.Tahun = '" & CmbTahun.Text & "'"
            SQL = SQL & "order by b.Kode_Stock_Owner, b.Kode_Barang, b.Id_Work_Center "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("Mytable")
                    If .Rows.Count <> 0 Then

                        For i As Integer = 0 To .Rows.Count - 1

                            columnDinamis = 4

                            dgv_workcenter.Rows.Add(1)
                            dgv_workcenter.Rows(i).Cells(cell2_KodeSO).Value = .Rows(i).Item("Kode_Stock_Owner")
                            dgv_workcenter.Rows(i).Cells(cell2_KodeBarang).Value = .Rows(i).Item("Kode_Barang")
                            dgv_workcenter.Rows(i).Cells(cell2_NamaBarang).Value = .Rows(i).Item("Nama")
                            dgv_workcenter.Rows(i).Cells(cell2_Mesin).Value = .Rows(i).Item("Keterangan")

                            If biayaCheck.Count <> 0 Then

                                '============================
                                '=     GET DATA COMPARE     =
                                '============================

                                For j As Integer = 0 To biayaCheck.Count - 1
                                    For k As Integer = 0 To dataCompare.Count - 1

                                        If Not colLoad.Contains($"{dataCompare(k)}{biayaCheckKeterangan(j)}") Then
                                            dgv_workcenter.Columns.Add(dataCompare(k), $"{dataCompare(k)}{vbCrLf}{biayaCheckKeterangan(j)}")
                                            dgv_workcenter.Columns(columnDinamis).Width = 110
                                            dgv_workcenter.Columns(columnDinamis).ReadOnly = True
                                            dgv_workcenter.Columns(columnDinamis).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                                            dgv_workcenter.Columns(columnDinamis).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


                                            colLoad.Add($"{dataCompare(k)}{biayaCheckKeterangan(j)}")
                                        End If


                                        Dim So As String = .Rows(i).Item("Kode_Stock_Owner")
                                        Dim KdBarang As String = .Rows(i).Item("Kode_Barang")
                                        Dim idWorkCenter As String = .Rows(i).Item("Id_Work_Center")
                                        Dim jenisBiaya As String = biayaCheck(j)
                                        Dim row As Integer = i
                                        Dim col As Integer = columnDinamis

                                        If dataCompare(k) = "Automation" Then

                                            Dim table As String = "Emi_Transaksi_Work_Center_Detail_Per_Mesin_automation"
                                            GetDataComparePerMesin(table, So, KdBarang, idWorkCenter, jenisBiaya, row, col)

                                        ElseIf dataCompare(k) = "Real" Then

                                            dgv_workcenter.Rows(row).Cells(col).Style.BackColor = Color.LightBlue
                                            Dim table As String = "Emi_Transaksi_Work_Center_Detail_Per_Mesin_Real"
                                            GetDataComparePerMesin(table, So, KdBarang, idWorkCenter, jenisBiaya, row, col)

                                        ElseIf dataCompare(k) = "Budgeting" Then

                                            dgv_workcenter.Rows(row).Cells(col).Style.BackColor = Color.LightGreen
                                            Dim table As String = "Emi_Transaksi_Work_Center_Detail_Per_Mesin_Budgeting"
                                            GetDataComparePerMesin(table, So, KdBarang, idWorkCenter, jenisBiaya, row, col)

                                        End If


                                        'WARNA 
                                        If biayaCheck(j) = "Biaya_Air" Then

                                            dgv_workcenter.Rows(row).Cells(col).Style.BackColor = Color.LightBlue

                                        ElseIf biayaCheck(j) = "Biaya_Bahan_Bakar" Then

                                            dgv_workcenter.Rows(row).Cells(col).Style.BackColor = Color.LightYellow

                                        ElseIf biayaCheck(j) = "Biaya_Gaji" Then

                                            dgv_workcenter.Rows(row).Cells(col).Style.BackColor = Color.LightGreen

                                        ElseIf biayaCheck(j) = "Biaya_Listrik" Then

                                            dgv_workcenter.Rows(row).Cells(col).Style.BackColor = Color.FromArgb(102, 196, 242)


                                        End If


                                        columnDinamis = columnDinamis + 1
                                    Next

                                Next

                            End If

                        Next

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

    Private Sub BtnCari_Click(sender As Object, e As EventArgs) Handles BtnCari.Click

        If dgv_workcenter.ColumnCount = 0 Then Exit Sub

        If CmbBulan.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Bulan Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf CmbTahun.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Tahun Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        'If biayaCheck.Count = 0 Then Exit Sub

        getData()
    End Sub

    Private Sub GetDataComparePerMesin(ByVal table As String, ByVal SO As String, ByVal KdBarang As String, ByVal idWorkCenter As String, ByVal jenisBiaya As String, ByVal row As Integer, ByVal col As Integer)

        SQL = "select Total, Nilai_Per_Pcs "
        SQL = SQL & "from " & table & " "
        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
        SQL = SQL & "and Kode_Stock_Owner = '" & SO & "' "
        SQL = SQL & "and Kode_Barang = '" & KdBarang & "' "
        SQL = SQL & "and Id_Work_Center = '" & idWorkCenter & "' "
        SQL = SQL & "and Jenis_Biaya = '" & jenisBiaya & "' "
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then

                dgv_workcenter.Rows(row).Cells(col).Value = If(General_Class.CekNULL(Dr("Total")) = "", 0, Format(Val(Dr("Total")), "N2"))

            Else
                Dr.Close()

                dgv_workcenter.Rows(row).Cells(col).Value = Format(0, "N2")

            End If

        End Using




    End Sub

End Class