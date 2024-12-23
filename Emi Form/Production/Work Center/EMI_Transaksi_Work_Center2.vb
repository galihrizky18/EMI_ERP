Imports System.Net
Imports System.Security.Cryptography

Public Class EMI_Transaksi_Work_Center2

    Dim arrBulan, arrBulanMM, arrSelectedRouting As New ArrayList

    Dim ColDinamis As Integer = 4

    Dim DgvRouting_IDRouting, DgvRouting_Keterangan, DgvRouting_CheckBox, DgvRouting_KdRouting, DgvRouting_PrefixCode, DgvRouting_IdJenisProduk As String
    Dim DgvWork_IDRouting, DgvWork_IDWorkCenter, DgvWork_Routing, DgvWork_Mesin As String

    Dim item_DgvRouting_IDRouting As Integer = 0
    Dim item_DgvRouting_Keterangan As Integer = 1
    Dim item_DgvRouting_CheckBox As Integer = 2
    Dim item_DgvRouting_KodeRouting As Integer = 3
    Dim item_DgvRouting_PrefixCode As Integer = 4
    Dim item_DgvRouting_IDJenisProduk As Integer = 5

    Dim item_DGVWork_IDRouting As Integer = 0
    Dim item_DGVWork_IDWorkCenter As Integer = 1
    Dim item_DGVWork_Routing As Integer = 2
    Dim item_DGVWork_Mesin As Integer = 3


    Private Sub EMI_Transaksi_Work_Center2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Kosong()

    End Sub


    Private Sub Kosong()

        get_jam()

        Try
            OpenConn()

            dgv_routing.Rows.Clear()
            dgv_workcenter.Rows.Clear()

            arrSelectedRouting.Clear()

            '======================
            '=     GET LOKASI     =
            '======================
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

            '=====================
            '=     GET BULAN     =
            '=====================
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

            '=====================
            '=     GET TAHUN     =
            '=====================
            CmbTahun.Items.Clear()
            Dim tahun_awal As Integer = Date.Now.Year - 2
            Dim tahun_akhir As Integer = Date.Now.Year + 2
            For a As Integer = tahun_awal To tahun_akhir
                CmbTahun.Items.Add(a)
            Next
            CmbTahun.Text = Format(Now.Date, "yyyy")
            CmbTahun.Enabled = True











            Get_Data_Routing()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Kosong_DGVWorkCenter()
        '=================================================
        '=     MENGHAPUS KOLOM MULAI DARI INDEX KE 4     =
        '=================================================
        For i As Integer = dgv_workcenter.Columns.Count - 1 To ColDinamis Step -1
            dgv_workcenter.Columns.RemoveAt(i)
        Next
    End Sub

    Private Sub Get_Data_DGVRouting(ByVal index As Integer)

        DgvRouting_IDRouting = dgv_routing.Rows(index).Cells(item_DgvRouting_IDRouting).Value
        DgvRouting_Keterangan = dgv_routing.Rows(index).Cells(item_DgvRouting_Keterangan).Value
        DgvRouting_CheckBox = dgv_routing.Rows(index).Cells(item_DgvRouting_CheckBox).Value
        DgvRouting_KdRouting = dgv_routing.Rows(index).Cells(item_DgvRouting_KodeRouting).Value
        DgvRouting_PrefixCode = dgv_routing.Rows(index).Cells(item_DgvRouting_PrefixCode).Value
        DgvRouting_IdJenisProduk = dgv_routing.Rows(index).Cells(item_DgvRouting_IDJenisProduk).Value

    End Sub

    Private Sub Get_data_DGVWorkCenter(ByVal index As Integer)

        DgvWork_IDRouting = dgv_workcenter.Rows(index).Cells(item_DGVWork_IDRouting).Value
        DgvWork_IDWorkCenter = dgv_workcenter.Rows(index).Cells(item_DGVWork_IDWorkCenter).Value
        DgvWork_Routing = dgv_workcenter.Rows(index).Cells(item_DGVWork_Routing).Value
        DgvWork_Mesin = dgv_workcenter.Rows(index).Cells(item_DGVWork_Mesin).Value

    End Sub

    Private Sub get_no_faktur(ByVal BulanTahun As String)
        Dim FPro_Results As String = "TCC"
        TxtBarangMasuk_NoFaktur.Text = FPro_Results & BulanTahun & "-" &
                             General_Class.Get_Last_Number2("Emi_Transaksi_work_Center", "No_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Faktur, 1, " & Len(FPro_Results) + 4 & ")", FPro_Results & BulanTahun)
    End Sub

    Private Sub BtnCari_Click(sender As Object, e As EventArgs) Handles BtnCari.Click
        If CmbLokasi.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbLokasi.Focus() : Kosong_DGVWorkCenter() : Exit Sub
        ElseIf CmbBulan.SelectedIndex = -1 Then
            MessageBox.Show("Bulan harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbBulan.Focus() : Kosong_DGVWorkCenter() : Exit Sub
        ElseIf CmbTahun.SelectedIndex = -1 Then
            MessageBox.Show("Tahun harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbTahun.Focus() : Kosong_DGVWorkCenter() : Exit Sub
        ElseIf arrSelectedRouting.Count = 0 Then
            MessageBox.Show("Harus Pilih Minimal 1 Routing", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            dgv_routing.Focus() : Kosong_DGVWorkCenter() : Exit Sub
        End If

        LoadData()
    End Sub

    Private Sub Get_Data_Routing()

        Dim row As Integer = 0
        SQL = "select Id_Routing, Keterangan, Kode_Routing, prefix_code, Id_Jenis_Produk from EMI_Master_Routing where Kode_Perusahaan = '" & KodePerusahaan & "'"
        Using Dr = OpenTrans(SQL)
            Do While Dr.Read
                dgv_routing.Rows.Add(1)
                dgv_routing.Rows(row).Cells(item_DgvRouting_IDRouting).Value = Dr("Id_Routing")
                dgv_routing.Rows(row).Cells(item_DgvRouting_Keterangan).Value = Dr("Keterangan")
                dgv_routing.Rows(row).Cells(item_DgvRouting_KodeRouting).Value = Dr("Kode_Routing")
                dgv_routing.Rows(row).Cells(item_DgvRouting_PrefixCode).Value = Dr("prefix_code")
                dgv_routing.Rows(row).Cells(item_DgvRouting_IDJenisProduk).Value = Dr("Id_Jenis_Produk")

                row += 1

            Loop
        End Using

    End Sub

    Private Sub dgv_routing_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_routing.CellEndEdit
        If dgv_routing.RowCount = 0 Then Exit Sub

        arrSelectedRouting.Clear()
        Kosong_DGVWorkCenter()

        For i As Integer = 0 To dgv_routing.RowCount - 1
            Get_Data_DGVRouting(i)

            If DgvRouting_CheckBox = "True" Then
                arrSelectedRouting.Add(DgvRouting_IDRouting)
            End If

        Next

        dgv_workcenter.Rows.Clear()
    End Sub

    Private Sub LoadData()
        If arrSelectedRouting.Count = 0 Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            'Reset Kolom
            Kosong_DGVWorkCenter()

            '=========================================
            '=     GET JENIS BIAYA (ADD COLUMN)      =
            '=========================================
            Dim ColNum As Integer = ColDinamis
            SQL = "Select kode_jenis_biaya_produksi, Keterangan from emi_jenis_biaya_produksi where kode_perusahaan = '" & KodePerusahaan & "'"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    dgv_workcenter.Columns.Add(dr("kode_jenis_biaya_produksi"), dr("kode_jenis_biaya_produksi"))
                    dgv_workcenter.Columns(ColNum).Width = 130
                    dgv_workcenter.Columns(ColNum).ReadOnly = False
                    dgv_workcenter.Columns(ColNum).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    ColNum += 1
                Loop
            End Using

            '========================================================================================================================================================================================

            Dim formatIdRouting As String = "'" & String.Join("', '", arrSelectedRouting.ToArray()) & "'"

            '=======================================
            '=     GET DATA MESIN PER ROUTING      =
            '=======================================
            SQL = "select a.No_Faktur, c.Id_Routing, c.Keterangan as Routing, e.Id_Work_Center, e.Keterangan as Mesin "
            SQL = SQL & "from Emi_Transaksi_Work_Center a, Emi_Transaksi_Work_Center_Detail b, emi_master_routing c, emi_master_routing_detail d, EMI_Master_Work_Center e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Id_Routing = c.Id_Routing "
            SQL = SQL & "and c.Id_Routing = d.Id_Routing "
            SQL = SQL & "and b.Id_Work_Center = e.Id_Work_Center "
            SQL = SQL & "and b.Id_Work_Center = d.Id_Work_Center "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Bulan = '" & arrBulanMM.Item(CmbBulan.SelectedIndex) & "' "
            SQL = SQL & "and a.Tahun = '" & CmbTahun.Text & "' "
            SQL = SQL & "and c.Id_Routing in (" & formatIdRouting & ") "
            SQL = SQL & "order by c.Id_Routing, e.Id_Work_Center"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        For i As Integer = 0 To .Rows.Count - 1
                            dgv_workcenter.Rows.Add(1)
                            dgv_workcenter.Rows(i).Cells(item_DGVWork_IDRouting).Value = .Rows(i).Item("Id_Routing")
                            dgv_workcenter.Rows(i).Cells(item_DGVWork_IDWorkCenter).Value = .Rows(i).Item("Id_Work_Center")
                            dgv_workcenter.Rows(i).Cells(item_DGVWork_Routing).Value = .Rows(i).Item("Routing")
                            dgv_workcenter.Rows(i).Cells(item_DGVWork_Mesin).Value = .Rows(i).Item("Mesin")

                            '======================================
                            '=     GET DATA DETAIL PER-MESIN      =
                            '======================================
                            SQL = "select a.Total, a.Jenis_Biaya "
                            SQL = SQL & "from Emi_Transaksi_Work_Center_Detail_Per_Mesin a "
                            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and a.No_Faktur = '" & .Rows(i).Item("No_Faktur") & "' "
                            SQL = SQL & "and a.id_Routing = '" & .Rows(i).Item("Id_Routing") & "' "
                            SQL = SQL & "and a.Id_Work_Center = '" & .Rows(i).Item("Id_Work_Center") & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    For j As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1

                                        For k As Integer = ColDinamis To dgv_workcenter.Columns.Count - 1
                                            If Ds1.Tables("MyTable").Rows(j).Item("Jenis_Biaya") = dgv_workcenter.Columns(k).HeaderText Then
                                                dgv_workcenter.Rows(i).Cells(k).Value = Ds1.Tables("MyTable").Rows(j).Item("Total")
                                                Exit For
                                            End If
                                        Next


                                    Next
                                End If
                            End Using



                        Next

                    Else

                        get_no_faktur(arrBulanMM(CmbBulan.SelectedIndex) & Strings.Right(CmbTahun.Text, 2))

                        CmbBulan.Enabled = True
                        CmbTahun.Enabled = True
                        BtnSimpan.Enabled = True

                        SQL = "select a.Id_Routing, a.Keterangan as Routing, c.Id_Work_Center, c.Keterangan as Mesin "
                        SQL = SQL & "from emi_master_routing a, emi_master_routing_detail b, EMI_Master_Work_Center c "
                        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                        SQL = SQL & "and a.Id_Routing = b.Id_Routing "
                        SQL = SQL & "and b.Id_Work_Center = c.Id_Work_Center "
                        SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and a.Id_Routing in (" & formatIdRouting & ") "
                        SQL = SQL & "order by a.Id_Routing, c.Id_Work_Center"
                        Using Ds2 = BindingTrans(SQL)
                            With Ds2.Tables("MyTable")
                                For i As Integer = 0 To .Rows.Count - 1

                                    dgv_workcenter.Rows.Add(1)
                                    dgv_workcenter.Rows(i).Cells(item_DGVWork_IDRouting).Value = Ds2.Tables("MyTable").Rows(i).Item("Id_Routing")
                                    dgv_workcenter.Rows(i).Cells(item_DGVWork_IDWorkCenter).Value = Ds2.Tables("MyTable").Rows(i).Item("Id_Work_Center")
                                    dgv_workcenter.Rows(i).Cells(item_DGVWork_Routing).Value = Ds2.Tables("MyTable").Rows(i).Item("Routing")
                                    dgv_workcenter.Rows(i).Cells(item_DGVWork_Mesin).Value = Ds2.Tables("MyTable").Rows(i).Item("Mesin")

                                Next
                            End With
                        End Using




                    End If
                End With
            End Using






            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

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
            For i As Integer = 0 To dgv_workcenter.Rows.Count - 1
                Dim Total As Double = 0
                For j As Integer = ColDinamis To dgv_workcenter.Columns.Count - 1
                    Total += dgv_workcenter.Rows(i).Cells(j).Value
                Next

                Get_data_DGVWorkCenter(i)

                SQL = "INSERT INTO Emi_Transaksi_Work_Center_Detail (kode_perusahaan, no_faktur, id_routing, id_work_center, total, nilai_per_pcs)"
                SQL = SQL & "Values('" & KodePerusahaan & "', '" & TxtBarangMasuk_NoFaktur.Text & "', "
                SQL = SQL & "'" & DgvWork_IDRouting & "', '" & DgvWork_IDWorkCenter & "', '" & Total & "','" & Total & "')"
                ExecuteTrans(SQL)
            Next

            'Emi_Transaksi_Work_Center_Detail_Per_Mesin
            For i As Integer = 0 To dgv_workcenter.Rows.Count - 1

                Get_data_DGVWorkCenter(i)

                For j As Integer = ColDinamis To dgv_workcenter.Columns.Count - 1
                    SQL = "INSERT INTO Emi_Transaksi_Work_Center_Detail_Per_Mesin(Kode_Perusahaan, No_Faktur, id_routing, Id_Work_Center, Jenis_Biaya, Total, Nilai_Per_Pcs) "
                    SQL = SQL & "VALUES('" & KodePerusahaan & "', '" & TxtBarangMasuk_NoFaktur.Text & "', '" & DgvWork_IDRouting & "', '" & DgvWork_IDWorkCenter & "', "
                    SQL = SQL & "'" & dgv_workcenter.Columns(j).HeaderText & "', '" & dgv_workcenter.Rows(i).Cells(j).Value & "', '" & dgv_workcenter.Rows(i).Cells(j).Value & "')"
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