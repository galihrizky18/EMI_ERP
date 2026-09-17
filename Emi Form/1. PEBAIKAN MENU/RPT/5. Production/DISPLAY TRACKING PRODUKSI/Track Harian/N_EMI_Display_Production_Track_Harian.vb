Imports excel = Microsoft.Office.Interop.Excel

Public Class N_EMI_Display_Production_Track_Harian

    Dim judulForm As String = "Display dan Laporan Final GI GR"

    Dim arrJenis, arr_tab_2_jenis As New ArrayList

    Dim cell_Step As Integer = 0
    Dim cell_NoPO As Integer = 1
    Dim cell_NoSplit As Integer = 2
    Dim cell_TglProduksi As Integer = 3
    Dim cell_JamProduksi As Integer = 4
    Dim cell_KdBarang As Integer = 5
    Dim cell_NmBarang As Integer = 6
    Dim cell_Routing As Integer = 7
    Dim cell_Batch As Integer = 8
    Dim cell_TotalOutput As Integer = 9
    Dim cell_TotalReject As Integer = 10
    Dim cell_TotalReject_Persen As Integer = 11

    Dim cell_global_NoSplit As Integer = 0
    Dim cell_global_Tanggal As Integer = 1
    Dim cell_global_Jam As Integer = 2
    Dim cell_global_UserID As Integer = 3
    Dim cell_global_Batch As Integer = 4
    Dim cell_global_Hari_Sampling As Integer = 5
    Dim cell_global_Nama_Barang As Integer = 6
    Dim cell_global_Qty_Produksi As Integer = 7
    Dim cell_global_Satuan As Integer = 8
    Dim cell_global_Hasil_Lab As Integer = 9
    Dim cell_global_Military_Sampling_1 As Integer = 10
    Dim cell_global_Penerimaan_Barang As Integer = 11
    Dim cell_global_Military_Sampling_2 As Integer = 12

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub



    Private Sub N_EMI_Laporan_Final_GI_GR_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Me.WindowState = FormWindowState.Maximized
        Me.Dock = DockStyle.Fill



        Lv_Routing.Columns.Clear()
        Lv_Routing.Columns.Add("Id Routing", 193, HorizontalAlignment.Center)
        Lv_Routing.Columns.Add("Nama Routing", 260, HorizontalAlignment.Center)
        Lv_Routing.View = View.Details

        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 193, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Nama Barang", 260, HorizontalAlignment.Center)
        Lv_Barang.View = View.Details

        Lv_Split.Columns.Clear()
        Lv_Split.Columns.Add("No Split", 193, HorizontalAlignment.Left)
        Lv_Split.Columns.Add("Keerangan", 260, HorizontalAlignment.Center)
        Lv_Split.View = View.Details


        Tab2_Cmb_Step.Items.Clear() : arr_tab_2_jenis.Clear()
        Tab2_Cmb_Step.Items.Add(OpsiSeluruh) : arr_tab_2_jenis.Add(OpsiSeluruh)
        Tab2_Cmb_Step.Items.Add("Hasil Lab") : arr_tab_2_jenis.Add("and hasil_lab.status <> 'T'")
        Tab2_Cmb_Step.Items.Add("Military Sampling 1") : arr_tab_2_jenis.Add("and Military_Sampling_1.Status <> 'T'")
        Tab2_Cmb_Step.Items.Add("Validasi Penerimaan Barang") : arr_tab_2_jenis.Add("and GR_2.Status <> 'T'")
        Tab2_Cmb_Step.Items.Add("Military Sampling 2") : arr_tab_2_jenis.Add("and Military_Sampling_2.Status <> 'T'")


        Kosong()
        'Kosong_Tab2()

    End Sub

    Private Sub Kosong()

        Tgl1.Value = DateTime.Today : Tgl2.Value = DateTime.Today
        Txt_IdRouting.Text = OpsiSeluruh : Txt_NmRouting.Text = OpsiSeluruh
        Txt_KdBarang.Text = OpsiSeluruh : Txt_NmBarang.Text = OpsiSeluruh
        Txt_NoSplit.Text = OpsiSeluruh

        Lv_Routing.Visible = False : Lv_Barang.Visible = False : Lv_Split.Visible = False

        Try
            OpenConn()

            Cmb_Jenis.Items.Clear() : arrJenis.Clear()
            Cmb_Jenis.Items.Add("Rejected") : arrJenis.Add("REJECTED")
            'Cmb_Jenis.Items.Add(OpsiSeluruh) : arrJenis.Add(OpsiSeluruh)
            'SQL = "select Kode_Group_Jenis, "
            'SQL = SQL & "case when Flag_Finished_Good = 'Y' then 'Barang Jadi' "
            'SQL = SQL & "when Flag_Semi_FG = 'Y' then 'Barang Setengah Jadi' "
            'SQL = SQL & "end as 'Keterangan', "
            'SQL = SQL & "case when Flag_Finished_Good = 'Y' and Flag_Semi_FG ='T' then 'Y' "
            'SQL = SQL & "when Flag_Semi_FG = 'Y' and Flag_Finished_Good = 'T' then 'T' "
            'SQL = SQL & "end as 'Flag' "
            'SQL = SQL & "from EMI_Group_Jenis where kode_perusahaan = '" & KodePerusahaan & "' and (Flag_Finished_Good = 'Y' or Flag_Semi_FG = 'Y') "
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read
            '        Cmb_Jenis.Items.Add(dr("Keterangan")) : arrJenis.Add(dr("Flag"))
            '    Loop
            'End Using
            Cmb_Jenis.SelectedIndex = 0


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Dgv_Rekap.Rows.Clear()
        Dim StartDinamicColumn As Integer = cell_TotalReject_Persen + 1
        For x As Integer = Dgv_Rekap.Columns.Count - 1 To StartDinamicColumn Step -1
            Dgv_Rekap.Columns.RemoveAt(x)
        Next

        Tgl2.Value = Now.Date : Tgl1.Value = Now.Date.AddDays(-7)
        LoadData2()


    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub


    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        If Txt_IdRouting.Text.Trim.Length = 0 Then
            MessageBox.Show("Routing harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_IdRouting.Focus() : Exit Sub
        ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarang.Focus() : Exit Sub
        ElseIf Cmb_Jenis.SelectedIndex = -1 Then
            MessageBox.Show("Jenis harus Dipilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarang.Focus() : Exit Sub
        End If



        'LoadData()
        LoadData2()


    End Sub
    Private Sub Cmb_Jenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Jenis.SelectedIndexChanged
        If Cmb_Jenis.SelectedIndex = -1 Then Exit Sub

        '=======================
        '=     SET DECIMAL     =
        '=======================

        'If arrJenis(Cmb_Jenis.SelectedIndex) = "Y" Then

        '    Dgv_Rekap.Columns(8).HeaderText = "Pro-RQ (PCS)"
        '    Dgv_Rekap.Columns(25).HeaderText = "GR Inspection (PCS)"
        '    Dgv_Rekap.Columns(27).HeaderText = "Final GR (PCS)"

        '    'Dgv_Detail2.Columns(8).HeaderText = "PRO-RQ (PCS)"
        '    'Dgv_Detail2.Columns(19).HeaderText = "GR 1 (PCS)"
        '    'Dgv_Detail2.Columns(22).HeaderText = "Waste GR 2 (PCS)"
        '    'Dgv_Detail2.Columns(24).HeaderText = "Reject GR 2 (PCS)"
        '    'Dgv_Detail2.Columns(26).HeaderText = "Scrap GR 2 (PCS)"
        '    'Dgv_Detail2.Columns(28).HeaderText = "GR 2 (PCS)"
        '    'Dgv_Detail2.Columns(29).HeaderText = "Stock Sementara (PCS)"
        '    'Dgv_Detail2.Columns(31).HeaderText = "Waste GR 3 (PCS)"
        '    'Dgv_Detail2.Columns(33).HeaderText = "Reject GR 3 (PCS)"
        '    'Dgv_Detail2.Columns(35).HeaderText = "Scrap GR 3 (PCS)"
        '    'Dgv_Detail2.Columns(37).HeaderText = "Final GR (PCS)"
        '    'Dgv_Detail2.Columns(38).HeaderText = "Stock Sementara (PCS)"



        'ElseIf arrJenis(Cmb_Jenis.SelectedIndex) = "T" Then

        '    Dgv_Rekap.Columns(8).HeaderText = "Pro-RQ (KG)"
        '    Dgv_Rekap.Columns(25).HeaderText = "GR Inspection (KG)"
        '    Dgv_Rekap.Columns(27).HeaderText = "Final GR (KG)"

        '    'Dgv_Detail2.Columns(8).HeaderText = "PRO-RQ (KG)"
        '    'Dgv_Detail2.Columns(19).HeaderText = "GR 1 (KG)"
        '    'Dgv_Detail2.Columns(22).HeaderText = "Waste GR 2 (KG)"
        '    'Dgv_Detail2.Columns(24).HeaderText = "Reject GR 2 (KG)"
        '    'Dgv_Detail2.Columns(26).HeaderText = "Scrap GR 2 (KG)"
        '    'Dgv_Detail2.Columns(28).HeaderText = "GR 2 (KG)"
        '    'Dgv_Detail2.Columns(29).HeaderText = "Stock Sementara (KG)"
        '    'Dgv_Detail2.Columns(31).HeaderText = "Waste GR 3 (KG)"
        '    'Dgv_Detail2.Columns(33).HeaderText = "Reject GR 3 (KG)"
        '    'Dgv_Detail2.Columns(35).HeaderText = "Scrap GR 3 (KG)"
        '    'Dgv_Detail2.Columns(37).HeaderText = "Final GR (KG)"
        '    'Dgv_Detail2.Columns(38).HeaderText = "Stock Sementara (KG)"

        'Else

        '    Dgv_Rekap.Columns(8).HeaderText = "Pro-RQ (PCS)"
        '    Dgv_Rekap.Columns(25).HeaderText = "GR Inspection (PCS)"
        '    Dgv_Rekap.Columns(27).HeaderText = "Final GR (PCS)"

        '    'Dgv_Detail2.Columns(8).HeaderText = "PRO-RQ (PCS)"
        '    'Dgv_Detail2.Columns(19).HeaderText = "GR 1 (PCS)"
        '    'Dgv_Detail2.Columns(22).HeaderText = "Waste GR 2 (PCS)"
        '    'Dgv_Detail2.Columns(24).HeaderText = "Reject GR 2 (PCS)"
        '    'Dgv_Detail2.Columns(26).HeaderText = "Scrap GR 2 (PCS)"
        '    'Dgv_Detail2.Columns(28).HeaderText = "GR 2 (PCS)"
        '    'Dgv_Detail2.Columns(29).HeaderText = "Stock Sementara (PCS)"
        '    'Dgv_Detail2.Columns(31).HeaderText = "Waste GR 3 (PCS)"
        '    'Dgv_Detail2.Columns(33).HeaderText = "Reject GR 3 (PCS)"
        '    'Dgv_Detail2.Columns(35).HeaderText = "Scrap GR 3 (PCS)"
        '    'Dgv_Detail2.Columns(37).HeaderText = "Final GR (PCS)"
        '    'Dgv_Detail2.Columns(38).HeaderText = "Stock Sementara (PCS)"

        'End If






    End Sub

    Private Sub LoadData()

        Dim tanggalMulai As Date = Tgl1.Value.Date
        Dim tanggalBerakhir As Date = Tgl2.Value.Date

        ' Hitung selisih hari
        Dim RangeTanggal As Integer = (tanggalBerakhir - tanggalMulai).Days

        If RangeTanggal > 31 Then
            MessageBox.Show("Rentang tanggal tidak boleh lebih dari 1 bulan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If


        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim StartDinamicColumn As Integer = cell_TotalReject_Persen + 1
            For x As Integer = Dgv_Rekap.Columns.Count - 1 To StartDinamicColumn Step -1
                Dgv_Rekap.Columns.RemoveAt(x)
            Next


            '===========================
            '=     LOAD DATA UTAMA     =
            '===========================


            Dgv_Rekap.Rows.Clear()
            SQL = "select a.Kode_Perusahaan, a.No_Faktur as No_PO, d.No_Transaksi as No_Split, a.Tanggal_Release as Tgl_PO, a.Jam_Release as Jam_PO, d.Tgl_Produksi, d.Jam_Produksi, "
            SQL = SQL & "a.Id_Routing, b.Keterangan as Routing, a.Kode_Barang, c.Nama as Nama_Barang, f.Proses as Batch, "
            SQL = SQL & "(sum(dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang, f.Satuan, a.Satuan, f.jumlah))) as Total_Output, "

            SQL = SQL & "ISNULL((select case when z.Flag_Sampling_2 <> 'T' then 'Military Sampling 2' else case when z.Flag_Sampling_1 <> 'T' then 'Military Sampling 1' "
            SQL = SQL & "else case when z.Flag_GR2 <> 'T' then 'Validasi Penerimaan Barang' else case when z.Flag_Lab_Analysis <> 'T' then 'Validasi Lab' else "
            SQL = SQL & "case when z.Flag_GR1 <> 'T' then 'Penerimaan Barang' else 'Tidak Diketahui' end end end end end as Step "
            SQL = SQL & "from N_EMI_View_Production_Tracker z where a.Kode_Perusahaan = z.kode_perusahaan and a.No_Faktur = z.No_PO and d.No_Transaksi = z.No_Transaksi "
            SQL = SQL & "), '-') as step "

            SQL = SQL & "from EMI_Order_Produksi a, EMI_Master_Routing b, barang c, Emi_Split_Production_Order d, Emi_Production_Results e, Emi_Production_Results_Detail_Pallet f "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan and d.Kode_Perusahaan = e.Kode_Perusahaan and e.Kode_Perusahaan = f.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Routing = b.Id_Routing "
            SQL = SQL & "and a.Kode_stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.No_Faktur = d.No_PO "
            SQL = SQL & "and d.No_Transaksi = e.No_Production_Order "
            SQL = SQL & "and e.No_Transaksi = f.No_Transaksi "
            SQL = SQL & "and a.Status is null and d.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Faktur = 'PR0825-00007' "

            If Not Txt_IdRouting.Text.Trim.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and a.Id_Routing = '" & Txt_IdRouting.Text & "' "
            End If

            If Not Txt_KdBarang.Text.Trim.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and a.Kode_Barang = '" & Txt_KdBarang.Text & "' "
            End If

            If Not Txt_NoSplit.Text.Trim.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and d.No_Transaksi = '" & Txt_NoSplit.Text & "' "
            End If

            SQL = SQL & "group by a.Kode_Perusahaan, a.No_Faktur, d.No_Transaksi , a.Tanggal_Release , a.Jam_Release, d.Tgl_Produksi, d.Jam_Produksi, "
            SQL = SQL & "a.Id_Routing, b.Keterangan, a.Kode_Barang, c.Nama, f.Proses  "
            SQL = SQL & "order by a.No_Faktur, d.No_Transaksi, d.Tgl_Produksi, d.Jam_Produksi "
            Using Ds0 = BindingTrans(SQL)
                If Ds0.Tables("MyTable").Rows.Count <> 0 Then
                    For i As Integer = 0 To Ds0.Tables("MyTable").Rows.Count - 1

                        Dgv_Rekap.Rows.Add(1)
                        Dgv_Rekap.Rows(i).Cells(cell_Step).Value = Ds0.Tables("MyTable").Rows(i).Item("Step")
                        Dgv_Rekap.Rows(i).Cells(cell_NoPO).Value = Ds0.Tables("MyTable").Rows(i).Item("No_PO")
                        Dgv_Rekap.Rows(i).Cells(cell_NoSplit).Value = Ds0.Tables("MyTable").Rows(i).Item("No_Split")
                        Dgv_Rekap.Rows(i).Cells(cell_TglProduksi).Value = Format(Ds0.Tables("MyTable").Rows(i).Item("Tgl_Produksi"), "dd MMM yyyy")
                        Dgv_Rekap.Rows(i).Cells(cell_JamProduksi).Value = Ds0.Tables("MyTable").Rows(i).Item("Jam_Produksi")
                        Dgv_Rekap.Rows(i).Cells(cell_KdBarang).Value = Ds0.Tables("MyTable").Rows(i).Item("Kode_Barang")
                        Dgv_Rekap.Rows(i).Cells(cell_NmBarang).Value = Ds0.Tables("MyTable").Rows(i).Item("Nama_Barang")
                        Dgv_Rekap.Rows(i).Cells(cell_Routing).Value = Ds0.Tables("MyTable").Rows(i).Item("Routing")
                        Dgv_Rekap.Rows(i).Cells(cell_Batch).Value = Ds0.Tables("MyTable").Rows(i).Item("Batch")
                        Dgv_Rekap.Rows(i).Cells(cell_TotalOutput).Value = Format(Ds0.Tables("MyTable").Rows(i).Item("Total_Output"), "N0")

                        Dgv_Rekap.Rows(i).Cells(cell_Step).Style.BackColor = Color.LightBlue
                        Dgv_Rekap.Rows(i).Cells(cell_TotalOutput).Style.BackColor = Color.LightGreen

                        Dim TotalReject As Double = 0
                        Dim currentDate As Date = tanggalMulai
                        Dim Jumlah_Reject_GR2 As Double = 0
                        Dim Jumlah_Reject_GR3 As Double = 0
                        For j As Integer = 0 To RangeTanggal
                            Dim hasReject_GR2 As Boolean = False
                            Dim hasReject_GR3 As Boolean = False
                            '===========================
                            '=     GET REJECT GR 2     =
                            '===========================
                            SQL = "select isnull((sum(dbo.ubah_satuan(z.Kode_Perusahaan, 'masa',x.Kode_Barang, x.Satuan, 'PCS', x.Jumlah))), 0) as Jumlah "
                            SQL = SQL & "from Emi_Production_Results_Validation z, Emi_Production_Results_Validation_Detail x "
                            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
                            SQL = SQL & "and z.No_Transaksi = x.No_Transaksi "
                            SQL = SQL & "and z.Status is null "
                            SQL = SQL & "and x.Jenis <> 'Finished Good' "
                            SQL = SQL & "and z.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and x.Tahap = " & Ds0.Tables("MyTable").Rows(i).Item("Batch") & " "
                            SQL = SQL & "and z.No_Production_Order = '" & Ds0.Tables("MyTable").Rows(i).Item("No_Split") & "' "
                            SQL = SQL & "and z.tanggal = '" & Format(currentDate, "yyyy-MM-dd") & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    Jumlah_Reject_GR2 += HilangkanTanda(Ds1.Tables("MyTable").Rows(0).Item("Jumlah"))
                                    If Val(HilangkanTanda(Ds1.Tables("MyTable").Rows(0).Item("Jumlah"))) > 0 Then
                                        hasReject_GR2 = True
                                    End If
                                End If
                            End Using

                            '===========================
                            '=     GET REJECT GR 3     =
                            '===========================
                            SQL = "select isnull((sum(dbo.ubah_satuan(z.Kode_Perusahaan, 'masa',y.Kode_Barang, y.Satuan, 'PCS', y.Jumlah))), 0) as Jumlah "
                            SQL = SQL & "from Emi_Production_Results_Validation z, Emi_Production_Results_Validation_Detail x, N_EMI_Validation_GR_3_Detail y, N_EMI_Validation_GR_3 w "
                            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Perusahaan = y.Kode_Perusahaan and z.Kode_Perusahaan = w.Kode_Perusahaan and y.Kode_Perusahaan = w.Kode_Perusahaan "
                            SQL = SQL & "and z.No_Transaksi = x.No_Transaksi "
                            SQL = SQL & "and z.No_Transaksi = y.No_Transaksi_GR2 "
                            SQL = SQL & "and z.No_Production_Order = w.No_Production_Order "
                            SQL = SQL & "and y.No_Transaksi = w.No_Transaksi "
                            SQL = SQL & "and z.Status is null "
                            SQL = SQL & "and x.Jenis <> 'Finished Good' "
                            SQL = SQL & "and z.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and x.Tahap = " & Ds0.Tables("MyTable").Rows(i).Item("Batch") & " "
                            SQL = SQL & "and z.No_Production_Order = '" & Ds0.Tables("MyTable").Rows(i).Item("No_Split") & "' "
                            SQL = SQL & "and w.tanggal = '" & Format(currentDate, "yyyy-MM-dd") & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    Jumlah_Reject_GR3 += HilangkanTanda(Ds1.Tables("MyTable").Rows(0).Item("Jumlah"))
                                    If Val(HilangkanTanda(Ds1.Tables("MyTable").Rows(0).Item("Jumlah"))) > 0 Then
                                        hasReject_GR3 = True
                                    End If
                                End If
                            End Using

                            Dim FoundColumn As Boolean = False
                            For k As Integer = StartDinamicColumn To Dgv_Rekap.Columns.Count - 1
                                If Dgv_Rekap.Columns(k).HeaderText = Format(currentDate, "dd MMM yyyy") Then
                                    FoundColumn = True
                                    If Not hasReject_GR2 And Not hasReject_GR3 Then
                                        Dgv_Rekap.Rows(i).Cells(k).Value = Format(0, "N0")
                                    Else
                                        Dgv_Rekap.Rows(i).Cells(k).Value = Format((Jumlah_Reject_GR2 + Jumlah_Reject_GR3), "N0")
                                    End If
                                    Exit For
                                End If
                            Next
                            'Dim searchHeader As String = Format(currentDate, "dd MMM yyyy")

                            ' Cari index kolom dari posisi StartDinamicColumn
                            '                    Dim colIndex As Integer = Enumerable.Range(0, Dgv_Rekap.Columns.Count) _
                            '                        .Where(Function(idx) Dgv_Rekap.Columns(idx).HeaderText = searchHeader) _
                            '                        .DefaultIfEmpty(-1) _
                            '                        .FirstOrDefault()

                            '                    ' Jika ditemukan
                            '                    If colIndex <> -1 Then
                            '                        FoundColumn = True
                            '                        Dgv_Rekap.Rows(i).Cells(colIndex).Value =
                            'Format((Jumlah_Reject_GR2 + Jumlah_Reject_GR3), "N0")
                            '                    End If

                            If Not FoundColumn Then
                                Dgv_Rekap.Columns.Add(Format(currentDate, "dd MMM yyyy"), Format(currentDate, "dd MMM yyyy"))
                                Dgv_Rekap.Columns(Dgv_Rekap.Columns.Count - 1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                                Dgv_Rekap.Columns(Dgv_Rekap.Columns.Count - 1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                                If Not hasReject_GR2 And Not hasReject_GR3 Then
                                    Dgv_Rekap.Rows(i).Cells(Dgv_Rekap.Columns.Count - 1).Value = Format(0, "N0")
                                Else
                                    Dgv_Rekap.Rows(i).Cells(Dgv_Rekap.Columns.Count - 1).Value = Format((Jumlah_Reject_GR2 + Jumlah_Reject_GR3), "N0")
                                End If

                            End If

                            currentDate = currentDate.AddDays(1)
                        Next



                        Dim colIndex As Integer = Dgv_Rekap.Columns.Cast(Of DataGridViewColumn)().ToList().FindIndex(Function(c) c.HeaderText = "TOTAL")
                        If colIndex <> -1 Then
                            Dgv_Rekap.Rows(i).Cells(colIndex).Value = Format((Jumlah_Reject_GR2 + Jumlah_Reject_GR3), "N0")

                            Dgv_Rekap.Rows(i).Cells(colIndex).Style.BackColor = Color.LightGray
                        Else
                            Dgv_Rekap.Columns.Add("TOTAL", "TOTAL")
                            Dgv_Rekap.Columns(Dgv_Rekap.Columns.Count - 1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                            Dgv_Rekap.Columns(Dgv_Rekap.Columns.Count - 1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                            Dgv_Rekap.Rows(i).Cells(Dgv_Rekap.Columns.Count - 1).Value = Format((Jumlah_Reject_GR2 + Jumlah_Reject_GR3), "N0")

                            Dgv_Rekap.Rows(i).Cells(Dgv_Rekap.Columns.Count - 1).Style.BackColor = Color.LightGray
                        End If




                        '===========================
                        '=     GET REJECT GR 2     =
                        '===========================
                        Dim A As Double = 0
                        Dim B As Double = 0
                        SQL = "select isnull((sum(dbo.ubah_satuan(z.Kode_Perusahaan, 'masa',x.Kode_Barang, x.Satuan, 'PCS', x.Jumlah))), 0) as Jumlah "
                        SQL = SQL & "from Emi_Production_Results_Validation z, Emi_Production_Results_Validation_Detail x "
                        SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
                        SQL = SQL & "and z.No_Transaksi = x.No_Transaksi "
                        SQL = SQL & "and z.Status is null "
                        SQL = SQL & "and x.Jenis <> 'Finished Good' "
                        SQL = SQL & "and z.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and x.Tahap = " & Ds0.Tables("MyTable").Rows(i).Item("Batch") & " "
                        SQL = SQL & "and z.No_Production_Order = '" & Ds0.Tables("MyTable").Rows(i).Item("No_Split") & "' "
                        SQL = SQL & "AND z.tanggal BETWEEN DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1) AND GETDATE() "
                        Using Ds1 = BindingTrans(SQL)
                            If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                A += HilangkanTanda(Ds1.Tables("MyTable").Rows(0).Item("Jumlah"))
                            End If
                        End Using

                        '===========================
                        '=     GET REJECT GR 3     =
                        '===========================
                        SQL = "select isnull((sum(dbo.ubah_satuan(z.Kode_Perusahaan, 'masa',y.Kode_Barang, y.Satuan, 'PCS', y.Jumlah))), 0) as Jumlah "
                        SQL = SQL & "from Emi_Production_Results_Validation z, Emi_Production_Results_Validation_Detail x, N_EMI_Validation_GR_3_Detail y, N_EMI_Validation_GR_3 w "
                        SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Perusahaan = y.Kode_Perusahaan and z.Kode_Perusahaan = w.Kode_Perusahaan and y.Kode_Perusahaan = w.Kode_Perusahaan "
                        SQL = SQL & "and z.No_Transaksi = x.No_Transaksi "
                        SQL = SQL & "and z.No_Transaksi = y.No_Transaksi_GR2 "
                        SQL = SQL & "and z.No_Production_Order = w.No_Production_Order "
                        SQL = SQL & "and y.No_Transaksi = w.No_Transaksi "
                        SQL = SQL & "and z.Status is null "
                        SQL = SQL & "and x.Jenis <> 'Finished Good' "
                        SQL = SQL & "and z.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and x.Tahap = " & Ds0.Tables("MyTable").Rows(i).Item("Batch") & " "
                        SQL = SQL & "and z.No_Production_Order = '" & Ds0.Tables("MyTable").Rows(i).Item("No_Split") & "' "
                        SQL = SQL & "AND z.tanggal BETWEEN DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1) AND GETDATE() "
                        Using Ds1 = BindingTrans(SQL)
                            If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                B += HilangkanTanda(Ds1.Tables("MyTable").Rows(0).Item("Jumlah"))
                            End If
                        End Using

                        Dim PersenTotalReject As Double = ((A + B) / Val(HilangkanTanda(Ds0.Tables("MyTable").Rows(i).Item("Total_Output")))) * 100

                        Dgv_Rekap.Rows(i).Cells(cell_TotalReject).Value = Format((A + B), "N0")
                        Dgv_Rekap.Rows(i).Cells(cell_TotalReject_Persen).Value = Format(PersenTotalReject, "N2")


                    Next
                End If
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




    Private Sub LoadData2()
        Dim tanggalMulai As Date = Tgl1.Value.Date
        Dim tanggalBerakhir As Date = Tgl2.Value.Date

        Dim RangeTanggal As Integer = (tanggalBerakhir - tanggalMulai).Days

        If RangeTanggal > 31 Then
            MessageBox.Show("Rentang tanggal tidak boleh lebih dari 1 bulan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            OpenConn()

            '=====================
            '=     RESET DGV     =
            '=====================
            Dgv_Rekap.Rows.Clear()
            Dim StartDinamicColumn As Integer = cell_TotalReject_Persen + 1
            For x As Integer = Dgv_Rekap.Columns.Count - 1 To StartDinamicColumn Step -1
                Dgv_Rekap.Columns.RemoveAt(x)
            Next


            '===========================
            '=     LAOD DATA UTAMA     =
            '===========================

            Dim DataUtama As DataSet
            SQL = "select a.Kode_Perusahaan, a.No_Faktur as No_PO, d.No_Transaksi as No_Split, a.Tanggal_Release as Tgl_PO, a.Jam_Release as Jam_PO, d.Tgl_Produksi, d.Jam_Produksi, "
            SQL = SQL & "a.Id_Routing, b.Keterangan as Routing, a.Kode_Barang, c.Nama as Nama_Barang, f.Proses as Batch, "
            SQL = SQL & "(sum(dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang, f.Satuan, a.Satuan, f.jumlah))) as Total_Output, "

            SQL = SQL & "ISNULL((select case when z.Flag_Sampling_2 <> 'T' then 'Military Sampling 2' else case when z.Flag_Sampling_1 <> 'T' then 'Military Sampling 1' "
            SQL = SQL & "else case when z.Flag_GR2 <> 'T' then 'Validasi Penerimaan Barang' else case when z.Flag_Lab_Analysis <> 'T' then 'Validasi Lab' else "
            SQL = SQL & "case when z.Flag_GR1 <> 'T' then 'Penerimaan Barang' else 'Tidak Diketahui' end end end end end as Step "
            SQL = SQL & "from N_EMI_View_Production_Tracker z where a.Kode_Perusahaan = z.kode_perusahaan and a.No_Faktur = z.No_PO and d.No_Transaksi = z.No_Transaksi "
            SQL = SQL & "), '-') as step "

            SQL = SQL & "from EMI_Order_Produksi a, EMI_Master_Routing b, barang c, Emi_Split_Production_Order d, Emi_Production_Results e, Emi_Production_Results_Detail_Pallet f "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan and d.Kode_Perusahaan = e.Kode_Perusahaan and e.Kode_Perusahaan = f.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Routing = b.Id_Routing "
            SQL = SQL & "and a.Kode_stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.No_Faktur = d.No_PO "
            SQL = SQL & "and d.No_Transaksi = e.No_Production_Order "
            SQL = SQL & "and e.No_Transaksi = f.No_Transaksi "
            SQL = SQL & "and a.Status is null and d.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Faktur = 'PR0825-00007' "

            If Not Txt_IdRouting.Text.Trim.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and a.Id_Routing = '" & Txt_IdRouting.Text & "' "
            End If

            If Not Txt_KdBarang.Text.Trim.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and a.Kode_Barang = '" & Txt_KdBarang.Text & "' "
            End If

            If Not Txt_NoSplit.Text.Trim.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and d.No_Transaksi = '" & Txt_NoSplit.Text & "' "
            End If

            SQL = SQL & "group by a.Kode_Perusahaan, a.No_Faktur, d.No_Transaksi , a.Tanggal_Release , a.Jam_Release, d.Tgl_Produksi, d.Jam_Produksi, "
            SQL = SQL & "a.Id_Routing, b.Keterangan, a.Kode_Barang, c.Nama, f.Proses  "
            SQL = SQL & "order by a.No_Faktur, d.No_Transaksi, d.Tgl_Produksi, d.Jam_Produksi "
            Using Ds0 = BindingTrans(SQL)
                If Ds0.Tables("MyTable").Rows.Count <> 0 Then
                    DataUtama = Ds0
                Else
                    CloseConn()
                    MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            '============================
            '=     TAMPUNG NO SPLIT     =
            '============================
            Dim listNoSplit As New List(Of String)
            For Each row As DataRow In DataUtama.Tables("MyTable").Rows
                If Not listNoSplit.Contains("'" & row.Item("No_Split") & "'") Then
                    listNoSplit.Add("'" & row.Item("No_Split") & "'")
                End If
            Next


            '===========================
            '=     GET DATA REJECT     =
            '===========================
            Dim Data_Reject As DataTable
            Dim JoinSplit As String = String.Join(",", listNoSplit)
            SQL = "select z.No_Production_Order, x.Tahap, z.tanggal, isnull((sum(dbo.ubah_satuan(z.Kode_Perusahaan, 'masa',x.Kode_Barang, x.Satuan, 'PCS', x.Jumlah))), 0) as Jumlah "
            SQL = SQL & "from Emi_Production_Results_Validation z, Emi_Production_Results_Validation_Detail x "
            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and z.No_Transaksi = x.No_Transaksi "
            SQL = SQL & "and z.Status is null "
            SQL = SQL & "and x.Jenis <> 'Finished Good' "
            SQL = SQL & "and z.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and z.No_Production_Order IN (" & JoinSplit & ") "
            SQL = SQL & "and z.tanggal BETWEEN '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
            SQL = SQL & "group by z.No_Production_Order, x.Tahap, z.tanggal "

            SQL = SQL & "union all "

            SQL = SQL & "select z.No_Production_Order, x.Tahap, w.tanggal, isnull((sum(dbo.ubah_satuan(z.Kode_Perusahaan, 'masa',y.Kode_Barang, y.Satuan, 'PCS', y.Jumlah))), 0) as Jumlah "
            SQL = SQL & "from Emi_Production_Results_Validation z, Emi_Production_Results_Validation_Detail x, N_EMI_Validation_GR_3_Detail y, N_EMI_Validation_GR_3 w "
            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Perusahaan = y.Kode_Perusahaan and z.Kode_Perusahaan = w.Kode_Perusahaan and y.Kode_Perusahaan = w.Kode_Perusahaan "
            SQL = SQL & "and z.No_Transaksi = x.No_Transaksi "
            SQL = SQL & "and z.No_Transaksi = y.No_Transaksi_GR2 "
            SQL = SQL & "and z.No_Production_Order = w.No_Production_Order "
            SQL = SQL & "and y.No_Transaksi = w.No_Transaksi "
            SQL = SQL & "and z.Status is null "
            SQL = SQL & "and x.Jenis <> 'Finished Good' "
            SQL = SQL & "and z.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and z.No_Production_Order IN (" & JoinSplit & ") "
            SQL = SQL & "and w.tanggal BETWEEN '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
            SQL = SQL & "group by z.No_Production_Order, x.Tahap, w.tanggal "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Data_Reject = Ds.Tables("MyTable")
                End If
            End Using



            '==============================
            '=     INSERT DATA KE DGV     =
            '==============================
            ' Buat kolom tanggal
            Dim currentDate As Date = tanggalMulai
            For j As Integer = 0 To RangeTanggal
                Dim colName As String = Format(currentDate, "dd MMM yyyy")
                Dgv_Rekap.Columns.Add(colName, colName)
                With Dgv_Rekap.Columns(colName)
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Format = "N0"
                End With
                currentDate = currentDate.AddDays(1)
            Next
            Dgv_Rekap.Columns.Add("TOTAL", "TOTAL")
            With Dgv_Rekap.Columns("TOTAL")
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.BackColor = Color.LightGray
                .DefaultCellStyle.Format = "N0"
            End With


            ' Isi DataGridView dari data yang sudah diambil
            For i As Integer = 0 To DataUtama.Tables("MyTable").Rows.Count - 1
                Dim mainRow As DataRow = DataUtama.Tables("MyTable").Rows(i)
                Dgv_Rekap.Rows.Add(1)

                ' Isi utama
                Dgv_Rekap.Rows(i).Cells(cell_Step).Value = mainRow.Item("Step")
                Dgv_Rekap.Rows(i).Cells(cell_NoPO).Value = mainRow.Item("No_PO")
                Dgv_Rekap.Rows(i).Cells(cell_NoSplit).Value = mainRow.Item("No_Split")
                Dgv_Rekap.Rows(i).Cells(cell_TglProduksi).Value = Format(mainRow.Item("Tgl_Produksi"), "dd MMM yyyy")
                Dgv_Rekap.Rows(i).Cells(cell_JamProduksi).Value = mainRow.Item("Jam_Produksi")
                Dgv_Rekap.Rows(i).Cells(cell_KdBarang).Value = mainRow.Item("Kode_Barang")
                Dgv_Rekap.Rows(i).Cells(cell_NmBarang).Value = mainRow.Item("Nama_Barang")
                Dgv_Rekap.Rows(i).Cells(cell_Routing).Value = mainRow.Item("Routing")
                Dgv_Rekap.Rows(i).Cells(cell_Batch).Value = mainRow.Item("Batch")
                Dgv_Rekap.Rows(i).Cells(cell_TotalOutput).Value = mainRow.Item("Total_Output")

                Dgv_Rekap.Rows(i).Cells(cell_Step).Style.BackColor = Color.LightBlue
                Dgv_Rekap.Rows(i).Cells(cell_TotalOutput).Style.BackColor = Color.LightGreen

                Dim totalRejectPeriode As Double = 0
                Dim currentSplitNo As String = mainRow.Item("No_Split")
                Dim currentBatch As Integer = Format(mainRow.Item("Batch"), "N0")

                ' Isi kolom sesuai dengan tanggal
                currentDate = tanggalMulai
                If Not Data_Reject Is Nothing Then
                    For j As Integer = 0 To RangeTanggal
                        Dim tglCari As Date = currentDate
                        Dim colName As String = Format(tglCari, "dd MMM yyyy")

                        ' Cari data menggunakan LINQ, lebih cepat dari looping (dapet dari Gemini hehe )
                        Dim dailyReject As Double = Data_Reject.AsEnumerable().
                               Where(Function(r) r.Field(Of String)("No_Production_Order") = currentSplitNo AndAlso
                                                 r.Field(Of Integer?)("Tahap") = currentBatch AndAlso
                                                 r.Field(Of Date?)("tanggal")?.Date = tglCari.Date).
                               Sum(Function(r) Convert.ToDouble(r("Jumlah")))

                        Dgv_Rekap.Rows(i).Cells(colName).Value = dailyReject
                        totalRejectPeriode += dailyReject
                        currentDate = currentDate.AddDays(1)
                    Next

                Else
                    For j As Integer = 0 To RangeTanggal
                        Dim tglCari As Date = currentDate
                        Dim colName As String = Format(tglCari, "dd MMM yyyy")


                        Dgv_Rekap.Rows(i).Cells(colName).Value = 0
                        totalRejectPeriode += 0
                        currentDate = currentDate.AddDays(1)
                    Next
                End If


                ' Isi kolom total dan persentase
                Dgv_Rekap.Rows(i).Cells("TOTAL").Value = Format(totalRejectPeriode, "N0")
                Dgv_Rekap.Rows(i).Cells(cell_TotalReject).Value = Format(totalRejectPeriode, "N0")

                Dim totalOutput As Double = mainRow.Item("Total_Output")
                Dim persenTotalReject As Double = If(totalOutput > 0, (totalRejectPeriode / totalOutput) * 100, 0)
                Dgv_Rekap.Rows(i).Cells(cell_TotalReject_Persen).Value = Format(persenTotalReject, "N2")
            Next


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    '===============================================================================================================================================================================
    '=     HANDLE TEXT CHANGED 
    '===============================================================================================================================================================================
    Private Sub Txt_IdRouting_TextChanged(sender As Object, e As EventArgs) Handles Txt_IdRouting.TextChanged
        If Txt_IdRouting.Text.Trim.Length = 0 Then
            Lv_Routing.Visible = False
            Lv_Routing.Location = New Point(1200, 165)
            Txt_IdRouting.Text = ""
            Txt_NmRouting.Text = ""
            Exit Sub
        Else
            Lv_Routing.Location = New Point(146, 165)
            Lv_Routing.Visible = True
        End If

        Try
            OpenConn()

            Lv_Routing.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Routing.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Id_Routing, Keterangan from EMI_Master_Routing where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Routing like '%" & Txt_IdRouting.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Routing.Items.Add(Dr("Id_Routing"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_NmRouting_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmRouting.TextChanged
        If Txt_NmRouting.Text.Trim.Length = 0 Then
            Lv_Routing.Visible = False
            Lv_Routing.Location = New Point(1200, 165)
            Txt_IdRouting.Text = ""
            Txt_NmRouting.Text = ""
            Exit Sub
        Else
            Lv_Routing.Location = New Point(146, 165)
            Lv_Routing.Visible = True
        End If

        Try
            OpenConn()

            Lv_Routing.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Routing.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select Id_Routing, Keterangan from EMI_Master_Routing where Kode_Perusahaan = '" & KodePerusahaan & "' and Keterangan like '%" & Txt_NmRouting.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Routing.Items.Add(Dr("Id_Routing"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
        If Txt_KdBarang.Text.Trim.Length = 0 Then
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1200, 193)
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Lv_Barang.Location = New Point(146, 193)
            Lv_Barang.Visible = True
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

    Private Sub Txt_NoSplit_TextChanged(sender As Object, e As EventArgs) Handles Txt_NoSplit.TextChanged
        If Txt_NoSplit.Text.Trim.Length = 0 Then
            Lv_Split.Visible = False
            Lv_Split.Location = New Point(1200, 219)
            Txt_NoSplit.Text = ""
            Exit Sub
        Else
            Lv_Split.Location = New Point(146, 219)
            Lv_Split.Visible = True
        End If

        Try
            OpenConn()

            Lv_Split.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Split.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select a.No_Transaksi, b.Keterangan from Emi_Split_Production_Order a, EMI_Order_Produksi b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_PO = b.No_Faktur "
            SQL = SQL & "and a.Status is null and b.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi like '%" & Txt_NoSplit.Text & "%' "
            SQL = SQL & "order by a.No_Transaksi "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Split.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_NoSplit_Leave(sender As Object, e As EventArgs) Handles Txt_NoSplit.Leave
        If Txt_NoSplit.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Split.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_NoSplit.Text = OpsiSeluruh Then

                SQL = "select a.No_Transaksi, b.Keterangan from Emi_Split_Production_Order a, EMI_Order_Produksi b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.No_PO = b.No_Faktur "
                SQL = SQL & "and a.Status is null and b.Status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Transaksi = '" & Txt_NoSplit.Text & "' "
                SQL = SQL & "order by a.No_Transaksi "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_NoSplit.Text = Dr("No_Transaksi")
                        Cmb_Jenis.Focus()
                    Else
                        MessageBox.Show("No Split tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_NoSplit.Text = ""
                        Txt_NoSplit.Focus()
                    End If

                    Lv_Split.Visible = False
                    Lv_Split.Location = New Point(1200, 219)
                End Using
            Else
                Cmb_Jenis.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
        If Txt_NmBarang.Text.Trim.Length = 0 Then
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1200, 193)
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Lv_Barang.Location = New Point(146, 193)
            Lv_Barang.Visible = True
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

    '===============================================================================================================================================================================
    '=     HANDLE LEAVE
    '===============================================================================================================================================================================
    Private Sub Txt_IdRouting_Leave(sender As Object, e As EventArgs) Handles Txt_IdRouting.Leave
        If Txt_IdRouting.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Routing.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_IdRouting.Text = OpsiSeluruh Then

                SQL = "select Kode_Perusahaan_Biaya_Import, Nama from Perusahaan_Biaya_Import where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Perusahaan_Biaya_Import = '" & Txt_IdRouting.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_IdRouting.Text = Dr("Kode_Perusahaan_Biaya_Import")
                        Txt_NmRouting.Text = Dr("Nama")
                        Txt_KdBarang.Focus()
                    Else
                        MessageBox.Show("Routing tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_IdRouting.Text = ""
                        Txt_NmRouting.Text = ""
                        Txt_IdRouting.Focus()
                    End If

                    Lv_Routing.Visible = False
                    Lv_Routing.Location = New Point(1200, 165)
                End Using
            Else
                Txt_KdBarang.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
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
                        Txt_NoSplit.Focus()
                    Else
                        MessageBox.Show("Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_KdBarang.Text = ""
                        Txt_NmBarang.Text = ""
                        Txt_KdBarang.Focus()
                    End If

                    Lv_Barang.Visible = False
                    Lv_Barang.Location = New Point(1200, 193)
                End Using
            Else
                Txt_NoSplit.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub




    '===============================================================================================================================================================================
    '=     HANDLE KEYPRESS
    '===============================================================================================================================================================================
    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress, Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then Txt_IdRouting.Focus()
    End Sub
    Private Sub Txt_IdRouting_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_IdRouting.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_IdRouting.Text.Trim.Length = 0 Then Txt_IdRouting.Focus()
            Txt_IdRouting_Leave(Txt_IdRouting, e)


            Lv_Routing.Visible = False
            Lv_Routing.Location = New Point(1200, 165)

            'Txt_KdKategori.Focus()
        End If
    End Sub
    Private Sub Txt_IdRouting_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_IdRouting.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Routing.Focus()
    End Sub
    Private Sub Txt_NmRouting_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmRouting.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_IdRouting_Leave(Txt_NmRouting, e)

            Lv_Routing.Visible = False
            Lv_Routing.Location = New Point(1200, 165)

            'Txt_KdKategori.Focus()
        End If
    End Sub
    Private Sub Txt_NmRouting_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmRouting.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Routing.Focus()
    End Sub
    Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
            Txt_KdBarang_Leave(Txt_KdBarang, e)

            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1200, 193)

            'Txt_KdKategori.Focus()
        End If
    End Sub
    Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub
    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdBarang_Leave(Txt_NmBarang, e)

            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1200, 193)

            'Txt_KdKategori.Focus()
        End If
    End Sub
    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Routing.Focus()
    End Sub

    Private Sub Txt_NoSplit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NoSplit.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_NoSplit.Text.Trim.Length = 0 Then Txt_NoSplit.Focus()
            Txt_NoSplit_Leave(Txt_NoSplit, e)

            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1200, 219)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Cmb_Jenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Jenis.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub




    '===============================================================================================================================================================================
    '=     HANDLE LV
    '===============================================================================================================================================================================
    Private Sub Lv_Routing_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Routing.DoubleClick
        If Lv_Routing.Items.Count = 0 Or Lv_Routing.FocusedItem.Index = -1 Then Exit Sub

        Dim IdRouting As String = Lv_Routing.FocusedItem.SubItems(0).Text
        Dim NmRouting As String = Lv_Routing.FocusedItem.SubItems(1).Text

        Txt_IdRouting.Text = IdRouting
        Txt_NmRouting.Text = NmRouting

        Lv_Routing.Visible = False
        Lv_Routing.Location = New Point(1200, 165)

        Txt_KdBarang.Focus()
    End Sub
    Private Sub Lv_Routing_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Routing.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Routing_DoubleClick(Lv_Routing, e)
        End If
    End Sub
    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
        Dim Nmbarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

        Txt_KdBarang.Text = KdBarang
        Txt_NmBarang.Text = Nmbarang

        Lv_Barang.Visible = False
        Lv_Barang.Location = New Point(1200, 193)

        Txt_NoSplit.Focus()
    End Sub
    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub


    '===============================================================================================================================================================================
    '=     HANDLE CETAK
    '===============================================================================================================================================================================

    Private Sub Btn_Cetak_Rekap_Click(sender As Object, e As EventArgs) Handles Btn_Cetak_Rekap.Click
        If Txt_IdRouting.Text.Trim.Length = 0 Then
            MessageBox.Show("Routing harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_IdRouting.Focus() : Exit Sub
        ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarang.Focus() : Exit Sub
        End If

        'Try
        '    OpenConn()

        '    '====================
        '    '=     CEK ROLE     =
        '    '====================
        '    If CekButtonRole("Cetak_FinalGIGR_Main_Rekap") = "T" Then
        '        CloseTrans()
        '        CloseConn()
        '        MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Cetak Laporan Rekap GI GR", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Exit Sub
        '    End If

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        If MessageBox.Show("Yakin Ingin Cetak Rekap Laporan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

        Generate_Excel_Rekap2()

    End Sub

    Private Sub Btn_Cetak_Detail_Click(sender As Object, e As EventArgs) Handles Btn_Cetak_Detail.Click
        If Txt_IdRouting.Text.Trim.Length = 0 Then
            MessageBox.Show("Routing harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_IdRouting.Focus() : Exit Sub
        ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarang.Focus() : Exit Sub
        End If

        'Try
        '    OpenConn()

        '    '====================
        '    '=     CEK ROLE     =
        '    '====================
        '    If CekButtonRole("Cetak_FinalGIGR_Main_Detail") = "T" Then
        '        CloseTrans()
        '        CloseConn()
        '        MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Cetak Laporan Detail GI GR", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Exit Sub
        '    End If

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        If MessageBox.Show("Yakin Ingin Cetak Detail Laporan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

        Generate_Excel_Detai2()

        'Try
        '    OpenConn()

        '    Dim SF As String = ""

        '    SQL = "select No_PO, no_split, Tgl_Produksi, Jam_Produksi, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, satuan, batch, Berat_GI, Jumlah_Dosing, NilaiGR1_Pcs, NilaiGR1_KG, ScrapGR1_KG, TotalGR1_KG, Loss_Production, Loss_Production_Persen, Persen_WasteGR1, WaktuGR1, "
        '    SQL = SQL & "NilaiGR2_Pcs, NilaiGR2_KG, RejectGR2_PCS, ScrapGR2_KG, TotalGR2_KG, Persen_WasteGR2, WaktuGR2, NilaiAfterGR_Pcs, NilaiAfterGR_KG, Persen_WasteGR3, WaktuGR3, NilaiGRFinal_Pcs, NilaiGRFinal_KG, ScrapGRFinal_KG, Loss_Production_Final_GR, Loss_Production_Final_GR_Persen, Total_Waste "
        '    SQL = SQL & "from Laporan_Akhir_GIGR "
        '    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and Tgl_Produksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

        '    SF = "{Laporan_Akhir_GIGR.kode_perusahaan} = '" & KodePerusahaan & "' "
        '    SF = SF & "and {Laporan_Akhir_GIGR.Tgl_Produksi} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
        '    SF = SF & "{Laporan_Akhir_GIGR.Tgl_Produksi} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

        '    If Not Txt_IdRouting.Text.ToUpper = OpsiSeluruh.ToUpper Then
        '        SQL = SQL & "and Id_Routing = '" & Txt_IdRouting.Text & "' "
        '        SF = SF & "And {Laporan_Akhir_GIGR.Id_Routing} = '" & Txt_IdRouting.Text & "'"
        '    End If

        '    If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
        '        SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
        '        SF = SF & "And {Laporan_Akhir_GIGR.Kode_Barang} = '" & Txt_KdBarang.Text & "' "
        '    End If

        '    If Txt_NoSplit.Text.Trim.Length > 0 AndAlso Txt_NoSplit.Text.ToUpper <> OpsiSeluruh.ToUpper Then
        '        SQL = SQL & "and no_split like '%" & Txt_NoSplit.Text & "%' "
        '    End If

        '    SQL = SQL & "order by no_split, Tgl_Produksi, Jam_Produksi "
        '    Using DS = BindingTrans(SQL)
        '        With DS.Tables("MyTable")
        '            If .Rows.Count <> 0 Then

        '                'Dim CrDoc As New Rpt_Laporan_Final_GI_GR

        '                'CrDoc.SetDataSource(DS)
        '                'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
        '                'CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
        '                '                                                    Format(Tgl2.Value, "dd/MMM/yyyy")
        '                'CrDoc.RecordSelectionFormula = SF

        '                'With A_Place_For_Printing2
        '                '    .Text = "Laporan Final GI GR"
        '                '    .CrystalReportViewer1.ReportSource = CrDoc
        '                '    .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        '                '    .Refresh()
        '                '    .Show()
        '                'End With

        '                'Generate_Excel_Detail(SQL)
        '                Generate_Excel_Detai2(SQL)

        '            Else

        '                CloseConn()
        '                MessageBox.Show("Data Tidak Ditemukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Exit Sub

        '            End If
        '        End With
        '    End Using


        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub

    Private Sub Generate_Excel_Detail(ByVal sql As String)
        Try

            get_jam()

            Dim xlApp As excel.Application = New Microsoft.Office.Interop.Excel.Application()

            '=======================================
            '=     CEK APAKAH EXCEL TERINSTALL     =
            '=======================================
            If xlApp Is Nothing Then
                MessageBox.Show("Excel is not properly installed!!")
                Return
            End If

            Dim JudulLaporan As String = "LAPORAN FINAL GI GR"

            Dim xlWorkBook As excel.Workbook
            Dim xlWorkSheet As excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value

            'Dim lokasi_file As String = Forms.Application.StartupPath & "\" & My.Computer.Name

            'If System.IO.Directory.Exists(lokasi_file) = False Then
            '    System.IO.Directory.CreateDirectory(lokasi_file)
            'End If

            Dim format_akhir As String = Format(Now(), "ddMMMyyyyHHmmss")
            Dim nama_file As String = "Testing_Excel " & format_akhir & ".xlsx"

            xlWorkBook = xlApp.Workbooks.Add(misValue)
            xlWorkSheet = xlWorkBook.Sheets("Sheet1")

            '==================================
            '=     DEFINISIKAN NAMA KOLOM     =
            '==================================
#Region "Generate Coloms"

            Dim dataKoloms As New List(Of Dictionary(Of String, String)) From {
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Step"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No PO"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No Split"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Tanggal Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Kode Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Nama Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Routing"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Batch"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Total Output"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}}
            }

            Dim rangeKolom As New Dictionary(Of String, Dictionary(Of String, Object)) From {
                {"Default", New Dictionary(Of String, Object) From {
                    {"Default", "Default"},
                    {"Kolom", 0}
                }}
            }

            For i As Integer = 0 To dataKoloms.Count - 1
                Dim kolom As Dictionary(Of String, String) = dataKoloms(i)

                If kolom("Identifier") = "Main" Then

                    xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Merge()

                    xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    'BORDER
                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "GR1" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Line Production (Good Received Lv I)"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                ElseIf kolom("Identifier") = "Val" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Quality Inspection (Good Received Lv II)"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                ElseIf kolom("Identifier") = "Rejected" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Good Received Rejected"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "Final" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Final Good Received"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                End If

            Next

#End Region


            '=========================
            '=     GENERATE BODY     =
            '=========================

            Dim stringCenter As New List(Of Integer) From {3, 4, 9, 10, 20, 26, 30}

            Dim numberColumn As New List(Of Integer) From {8, 11, 12, 13, 14, 15, 16, 17, 18, 19, 21, 22, 23, 24, 25, 27, 28, 29, 31, 32, 33, 34, 35, 36}

            Dim NumberN0 As New List(Of Integer) From {8, 11, 13, 21, 23, 28, 32}

            Dim defaultRowIndex As Integer = 5
            Try

                ' Ambil format sesuai culture
                Dim culture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentCulture
                xlApp.UseSystemSeparators = True

                '==  AMBIL SEPARATOR DARI EXCEL =='
                Dim decimalSep As String = xlApp.DecimalSeparator
                Dim groupSep As String = xlApp.ThousandsSeparator

                '==  AMBIL SEPARATOR DARI SISTEM =='
                'Dim decimalSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator
                'Dim groupSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator

                If decimalSep = "," Then
                    decimalSep = "."
                ElseIf decimalSep = "." Then
                    decimalSep = ","
                End If

                If groupSep = "." Then
                    groupSep = ","
                ElseIf groupSep = "," Then
                    groupSep = "."
                End If

                Dim templateFormat As String = "#GROUP##0DEC0000"
                Dim excelFormat As String = templateFormat _
                    .Replace("GROUP", groupSep) _
                    .Replace("DEC", decimalSep)

                Dim templateFormatN0 As String = "#GROUP##0"
                Dim excelFormatN0 As String = templateFormatN0 _
                    .Replace("GROUP", groupSep)





                Dim row As Integer = 0
                'sql = "select No_PO, no_split, Tgl_Produksi, Jam_Produksi, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, satuan, batch, Jumlah_Dosing, NilaiGR1_Pcs, NilaiGR1_KG, ScrapGR1_KG, Loss_Production, Persen_WasteGR1, "
                'sql = sql & "NilaiGR2_Pcs, NilaiGR2_KG, ScrapGR2_KG, Persen_WasteGR2, NilaiAfterGR_Pcs, NilaiAfterGR_KG, Persen_WasteGR3, NilaiGRFinal_Pcs, NilaiGRFinal_KG, Total_Waste "
                'sql = sql & "from Laporan_Akhir_GIGR "
                'sql = sql & "where Kode_Perusahaan = '001' "
                'sql = sql & "and Tgl_Produksi between '2022-12-20 00:00:00.000' and '2030-12-20 00:00:00.000' "
                Using Ds = BindingTrans(sql)
                    With Ds.Tables("MyTable")

                        For i As Integer = 0 To .Rows.Count - 1

                            For colIndex As Integer = 0 To .Columns.Count - 1
                                Dim cell = xlWorkSheet.Cells(i + defaultRowIndex, colIndex + 1)
                                If colIndex = 6 Then
                                    cell.NumberFormat = "@"
                                End If

                                cell.Value = General_Class.CekNULL(.Rows(i).Item(colIndex))

                                cell.VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                                ' Format numerik (N4)
                                If numberColumn.Contains(colIndex) Then
                                    Dim nilai As Double = If(General_Class.CekNULL(.Rows(i).Item(colIndex)) = "", 0, .Rows(i).Item(colIndex))

                                    If NumberN0.Contains(colIndex) Then
                                        cell.NumberFormat = excelFormatN0
                                    Else
                                        cell.NumberFormat = excelFormat
                                    End If

                                    cell.Value = nilai

                                End If



                                '== ATUR ALIGMENT CELL =='
                                Select Case .Columns(colIndex).DataType.Name
                                    Case "String"
                                        cell.HorizontalAlignment = If(stringCenter.Contains(colIndex), excel.XlHAlign.xlHAlignCenter, excel.XlHAlign.xlHAlignLeft)
                                    Case "DateTime"
                                        cell.HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                                        cell.Value = Format(CDate(.Rows(i).Item(colIndex)), "dd MMM yyyy")
                                    Case "Int32", "Double"
                                        cell.HorizontalAlignment = excel.XlHAlign.xlHAlignRight
                                        cell.HorizontalAlignment = If(stringCenter.Contains(colIndex), excel.XlHAlign.xlHAlignCenter, excel.XlHAlign.xlHAlignRight)
                                End Select

                                ' BORDER
                                With cell.Borders
                                    .LineStyle = excel.XlLineStyle.xlContinuous
                                    .ColorIndex = 0
                                    .Weight = excel.XlBorderWeight.xlThin
                                End With

                                ' BG COLOR
                                Select Case colIndex
                                    Case 13 To 20
                                        If .Columns(colIndex).ColumnName = "WaktuGR1" Then
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(252, 105, 108))
                                        Else
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow)
                                        End If
                                    Case 21 To 27
                                        If .Columns(colIndex).ColumnName = "WaktuGR2" Then
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(181, 230, 162))
                                        Else
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                                        End If
                                    Case 28 To 31
                                        If .Columns(colIndex).ColumnName = "WaktuGR3" Then
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
                                        Else
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
                                        End If
                                    Case 32 To 37
                                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen)
                                End Select

                                xlWorkSheet.Cells(1, 1).Interior.TintAndShade = 0.2

                            Next

                            row += 1

                        Next

                    End With
                End Using

                ' AutoFit kolom setelah semua data dimasukkan
                xlWorkSheet.Columns.AutoFit()

                '==========================
                '=     HEADER LAPORAN     =
                '==========================
                Dim panjangKolom As Integer = dataKoloms.Count

                xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, panjangKolom)).Merge()

                xlWorkSheet.Cells(1, 1).Value = JudulLaporan
                xlWorkSheet.Cells(1, 1).Font.Size = 14
                xlWorkSheet.Cells(1, 1).Font.Bold = True
                xlWorkSheet.Cells(1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells(1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                xlWorkSheet.Columns(1).AutoFit()

                '==========================
                '=     FOOTER LAPORAN     =
                '==========================
                Dim jumlahRows As Integer = row + defaultRowIndex

                Dim Footer As String = "| " & Format(tgl_skg, "dd MMM yyyy") & " | " & Format(tgl_skg, "HH:mm:ss")

                xlWorkSheet.Cells(jumlahRows + 1, 1).Value = Footer
                xlWorkSheet.Cells(jumlahRows + 1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells(jumlahRows + 1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                xlWorkSheet.Columns(1).AutoFit()


                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try



            '=====================
            '=     SAVE FILE     =
            '=====================
            Dim saveFileDialog As New SaveFileDialog()

            ' Set File Filter
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*"
            saveFileDialog.Title = "Save As"


            'Tampilkan Show Dialog Save as
            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Try
                    Dim filePath As String = saveFileDialog.FileName

                    xlWorkBook.SaveAs(filePath, excel.XlFileFormat.xlOpenXMLWorkbook)

                    'MessageBox.Show("File berhasil disimpan di: " & filePath)

                    ' Menutup workbook dan aplikasi Excel
                    xlWorkBook.Close()
                    xlApp.Quit()

                    ' Membebaskan objek Excel
                    releaseObject(xlWorkSheet)
                    releaseObject(xlWorkBook)
                    releaseObject(xlApp)

                Catch ex As Exception
                    MessageBox.Show("Terjadi kesalahan saat menyimpan file: " & ex.Message)
                End Try
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Generate_Excel_Detai2()
        Try

            get_jam()

            Dim xlApp As excel.Application = New Microsoft.Office.Interop.Excel.Application()

            '=======================================
            '=     CEK APAKAH EXCEL TERINSTALL     =
            '=======================================
            If xlApp Is Nothing Then
                MessageBox.Show("Excel is not properly installed!!")
                Return
            End If

            Dim JudulLaporan As String = "LAPORAN FINAL GI GR"

            Dim xlWorkBook As excel.Workbook
            Dim xlWorkSheet As excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value

            'Dim lokasi_file As String = Forms.Application.StartupPath & "\" & My.Computer.Name

            'If System.IO.Directory.Exists(lokasi_file) = False Then
            '    System.IO.Directory.CreateDirectory(lokasi_file)
            'End If

            Dim format_akhir As String = Format(Now(), "ddMMMyyyyHHmmss")
            Dim nama_file As String = "Testing_Excel " & format_akhir & ".xlsx"

            xlWorkBook = xlApp.Workbooks.Add(misValue)
            xlWorkSheet = xlWorkBook.Sheets("Sheet1")

            '==================================
            '=     DEFINISIKAN NAMA KOLOM     =
            '==================================
#Region "Generate Coloms"

            Dim dataKoloms As New List(Of Dictionary(Of String, String)) From {
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No PO"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No Split"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Tanggal Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Jam Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Routing"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Keterangan"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Kode Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Nama Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "PRO-RQ (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Batch"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Isi (Gram)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Good Issue (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Waste (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},'Mulai GR 1
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Reject (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Scrap (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Loss (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Loss (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Good Received (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Good Received (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Stock Sementara (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Time (Day)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Waste (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}}, 'Mulai Val
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Reject (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Reject (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Scrap (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Scrap (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Good Received (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Stock Sementara (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Time (Day)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Waste (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}}, 'Mulai Rejected
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Reject (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Reject (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Scrap (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Scrap (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Final GR (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Stock Sementara (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Time (Day)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}}
            }

            Dim rangeKolom As New Dictionary(Of String, Dictionary(Of String, Object)) From {
                {"Default", New Dictionary(Of String, Object) From {
                    {"Default", "Default"},
                    {"Kolom", 0}
                }}
            }

            For i As Integer = 0 To dataKoloms.Count - 1
                Dim kolom As Dictionary(Of String, String) = dataKoloms(i)

                If kolom("Identifier") = "Main" Then

                    xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Merge()

                    xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    'BORDER
                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "GR1" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Line Production (Good Received Lv I)"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                ElseIf kolom("Identifier") = "Val" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Quality Inspection (Good Received Lv II)"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                ElseIf kolom("Identifier") = "Rejected" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Warehouse"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "Final" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Final Good Received"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                End If

            Next

#End Region


            '=========================
            '=     GENERATE BODY     =
            '=========================

            Dim stringCenter As New List(Of Integer) From {3, 4, 9, 21, 30, 39}

            Dim numberColumn As New List(Of Integer) From {8, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 22, 23, 24, 25, 26, 27, 28, 29, 31, 32, 33, 34, 35, 36, 37, 38}

            Dim DecimalColumn As New List(Of Integer) From {8, 19, 22, 24, 26, 28, 29, 31, 33, 35, 37, 38}

            Dim NumberN0 As New List(Of Integer) From {8, 19, 22, 24, 26, 28, 29, 31, 33, 35, 37, 38}

            Dim defaultRowIndex As Integer = 5
            Try
                OpenConn()

                ' Ambil format sesuai culture
                Dim culture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentCulture
                xlApp.UseSystemSeparators = True

                '==  AMBIL SEPARATOR DARI EXCEL =='
                Dim decimalSep As String = xlApp.DecimalSeparator
                Dim groupSep As String = xlApp.ThousandsSeparator

                '==  AMBIL SEPARATOR DARI SISTEM =='
                'Dim decimalSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator
                'Dim groupSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator

                If decimalSep = "," Then
                    decimalSep = "."
                ElseIf decimalSep = "." Then
                    decimalSep = ","
                End If

                If groupSep = "." Then
                    groupSep = ","
                ElseIf groupSep = "," Then
                    groupSep = "."
                End If

                Dim templateFormat As String = "#GROUP##0DEC0000"
                Dim excelFormat As String = templateFormat _
                    .Replace("GROUP", groupSep) _
                    .Replace("DEC", decimalSep)

                Dim templateFormatN0 As String = "#GROUP##0"
                Dim excelFormatN0 As String = templateFormatN0 _
                    .Replace("GROUP", groupSep)



                Dim row As Integer = 0
                SQL = "select No_po, no_split, Tgl_Produksi, Jam_Produksi, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, batch, Berat_GI, Jumlah_Dosing, "
                SQL = SQL & "WasteGR1_KG, WasteGR1_Persen, Reject_GR1_KG, ScrapGR1_KG, Loss_Production, Loss_Production_Persen, NilaiGR1_KG, NilaiGR1_Pcs, GR1_StockSementara, WaktuGR1, "
                SQL = SQL & "WasteGR2_Pcs, WasteGR2_Persen, Reject_GR2_PCS, Reject_GR2_KG, ScrapGR2_PCS, ScrapGR2_KG,NilaiGR2_Pcs, GR2_StockSementara, WaktuGR2, "
                SQL = SQL & "WasteGR3_Pcs, WasteGR3_Persen, Reject_GR3_PCS, Reject_GR3_KG, ScrapGR3_PCS, ScrapGR3_KG, NilaiGRFinal_Pcs, GR3_StockSementara, WaktuGR3  "
                SQL = SQL & "from Laporan_Akhir_GIGR2 "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Tgl_Produksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" ' & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                If Not Txt_IdRouting.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Id_Routing = '" & Txt_IdRouting.Text & "' "
                End If
                If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
                End If
                If Txt_NoSplit.Text.Trim.Length > 0 AndAlso Txt_NoSplit.Text.ToUpper <> OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and no_split like '%" & Txt_NoSplit.Text & "%' "
                End If
                If Not Cmb_Jenis.SelectedIndex = 0 Then
                    SQL = SQL & "and Group_Jenis = '" & arrJenis(Cmb_Jenis.SelectedIndex) & "' "
                End If
                SQL = SQL & "order by no_split, Tgl_Produksi, Jam_Produksi "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            For i As Integer = 0 To .Rows.Count - 1

                                For colIndex As Integer = 0 To .Columns.Count - 1
                                    Dim cell = xlWorkSheet.Cells(i + defaultRowIndex, colIndex + 1)
                                    If colIndex = 6 Then
                                        cell.NumberFormat = "@"
                                    End If

                                    cell.Value = General_Class.CekNULL(.Rows(i).Item(colIndex))

                                    cell.VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                                    ' Format numerik (N4)
                                    If numberColumn.Contains(colIndex) Then
                                        Dim nilai As Double = If(General_Class.CekNULL(.Rows(i).Item(colIndex)) = "", 0, .Rows(i).Item(colIndex))

                                        If NumberN0.Contains(colIndex) Then
                                            cell.NumberFormat = excelFormatN0
                                        Else
                                            cell.NumberFormat = excelFormat
                                        End If

                                        If DecimalColumn.Contains(colIndex) Then
                                            If arrJenis(Cmb_Jenis.SelectedIndex) = "Y" Then
                                                cell.NumberFormat = excelFormatN0
                                            ElseIf arrJenis(Cmb_Jenis.SelectedIndex) = "T" Then
                                                cell.NumberFormat = excelFormat
                                            End If
                                        End If

                                        cell.Value = nilai

                                    End If



                                    '== ATUR ALIGMENT CELL =='
                                    Select Case .Columns(colIndex).DataType.Name
                                        Case "String"
                                            cell.HorizontalAlignment = If(stringCenter.Contains(colIndex), excel.XlHAlign.xlHAlignCenter, excel.XlHAlign.xlHAlignLeft)
                                        Case "DateTime"
                                            cell.HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                                            cell.Value = Format(CDate(.Rows(i).Item(colIndex)), "dd MMM yyyy")
                                        Case "Int32", "Double"
                                            cell.HorizontalAlignment = excel.XlHAlign.xlHAlignRight
                                            cell.HorizontalAlignment = If(stringCenter.Contains(colIndex), excel.XlHAlign.xlHAlignCenter, excel.XlHAlign.xlHAlignRight)
                                    End Select

                                    ' BORDER
                                    With cell.Borders
                                        .LineStyle = excel.XlLineStyle.xlContinuous
                                        .ColorIndex = 0
                                        .Weight = excel.XlBorderWeight.xlThin
                                    End With

                                    ' BG COLOR
                                    Select Case colIndex
                                        Case 12 To 21
                                            If .Columns(colIndex).ColumnName = "WaktuGR1" Then
                                                cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(252, 105, 108))
                                            Else
                                                cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow)
                                            End If
                                        Case 22 To 30
                                            If .Columns(colIndex).ColumnName = "WaktuGR2" Then
                                                cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(181, 230, 162))
                                            Else
                                                cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                                            End If
                                        Case 31 To 39
                                            If .Columns(colIndex).ColumnName = "WaktuGR3" Then
                                                cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
                                            Else
                                                cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
                                            End If

                                    End Select

                                    xlWorkSheet.Cells(1, 1).Interior.TintAndShade = 0.2

                                Next

                                row += 1

                            Next

                        Else
                            CloseConn()
                            MessageBox.Show("Data Tidak Ditemukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                    End With
                End Using

                ' AutoFit kolom setelah semua data dimasukkan
                xlWorkSheet.Columns.AutoFit()

                '==========================
                '=     HEADER LAPORAN     =
                '==========================
                Dim panjangKolom As Integer = dataKoloms.Count

                xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, panjangKolom)).Merge()

                xlWorkSheet.Cells(1, 1).Value = JudulLaporan
                xlWorkSheet.Cells(1, 1).Font.Size = 14
                xlWorkSheet.Cells(1, 1).Font.Bold = True
                xlWorkSheet.Cells(1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells(1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                xlWorkSheet.Columns(1).AutoFit()

                '==========================
                '=     FOOTER LAPORAN     =
                '==========================
                Dim jumlahRows As Integer = row + defaultRowIndex

                Dim Footer As String = "| " & Format(tgl_skg, "dd MMM yyyy") & " | " & Format(tgl_skg, "HH:mm:ss")

                xlWorkSheet.Cells(jumlahRows + 1, 1).Value = Footer
                xlWorkSheet.Cells(jumlahRows + 1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells(jumlahRows + 1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                xlWorkSheet.Columns(1).AutoFit()


                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try



            '=====================
            '=     SAVE FILE     =
            '=====================
            Dim saveFileDialog As New SaveFileDialog()

            ' Set File Filter
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*"
            saveFileDialog.Title = "Save As"


            'Tampilkan Show Dialog Save as
            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Try
                    Dim filePath As String = saveFileDialog.FileName

                    xlWorkBook.SaveAs(filePath, excel.XlFileFormat.xlOpenXMLWorkbook)

                    'MessageBox.Show("File berhasil disimpan di: " & filePath)

                    ' Menutup workbook dan aplikasi Excel
                    xlWorkBook.Close()
                    xlApp.Quit()

                    ' Membebaskan objek Excel
                    releaseObject(xlWorkSheet)
                    releaseObject(xlWorkBook)
                    releaseObject(xlApp)

                Catch ex As Exception
                    MessageBox.Show("Terjadi kesalahan saat menyimpan file: " & ex.Message)
                End Try
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Generate_Excel_Rekap2()
        Try
            OpenConn()
            get_jam()


            Dim tanggalMulai As Date = Tgl1.Value.Date
            Dim tanggalBerakhir As Date = Tgl2.Value.Date

            Dim RangeTanggal As Integer = (tanggalBerakhir - tanggalMulai).Days

            Dim StartDinamicColumn As Integer = cell_TotalReject_Persen + 1
            Dim defaultRowIndex As Integer = 5

            Dim xlApp As excel.Application = New Microsoft.Office.Interop.Excel.Application()

            '=======================================
            '=     CEK APAKAH EXCEL TERINSTALL     =
            '=======================================
            If xlApp Is Nothing Then
                MessageBox.Show("Excel is not properly installed!!")
                Return
            End If

            Dim JudulLaporan As String = "LAPORAN FINAL GI GR"

            Dim xlWorkBook As excel.Workbook
            Dim xlWorkSheet As excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value

            'Dim lokasi_file As String = Forms.Application.StartupPath & "\" & My.Computer.Name

            'If System.IO.Directory.Exists(lokasi_file) = False Then
            '    System.IO.Directory.CreateDirectory(lokasi_file)
            'End If

            Dim format_akhir As String = Format(Now(), "ddMMMyyyyHHmmss")
            Dim nama_file As String = "Testing_Excel " & format_akhir & ".xlsx"

            xlWorkBook = xlApp.Workbooks.Add(misValue)
            xlWorkSheet = xlWorkBook.Sheets("Sheet1")

#Region "Get Data"

            '===========================
            '=     LAOD DATA UTAMA     =
            '===========================

            Dim DataUtama As DataSet
            SQL = "select ISNULL((select case when z.Flag_Sampling_2 <> 'T' then 'Military Sampling 2' else case when z.Flag_Sampling_1 <> 'T' then 'Military Sampling 1' "
            SQL = SQL & "else case when z.Flag_GR2 <> 'T' then 'Validasi Penerimaan Barang' else case when z.Flag_Lab_Analysis <> 'T' then 'Validasi Lab' else "
            SQL = SQL & "case when z.Flag_GR1 <> 'T' then 'Penerimaan Barang' else 'Tidak Diketahui' end end end end end as Step "
            SQL = SQL & "from N_EMI_View_Production_Tracker z where a.Kode_Perusahaan = z.kode_perusahaan and a.No_Faktur = z.No_PO and d.No_Transaksi = z.No_Transaksi "
            SQL = SQL & "), '-') as step, "
            SQL = SQL & "a.No_Faktur as No_PO, d.No_Transaksi as No_Split, d.Tgl_Produksi, d.Jam_Produksi,  "
            SQL = SQL & "a.Kode_Barang, c.Nama as Nama_Barang, b.Keterangan as Routing, f.Proses as Batch, "
            SQL = SQL & "(sum(dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang, f.Satuan, a.Satuan, f.jumlah))) as Total_Output "

            SQL = SQL & "from EMI_Order_Produksi a, EMI_Master_Routing b, barang c, Emi_Split_Production_Order d, Emi_Production_Results e, Emi_Production_Results_Detail_Pallet f "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan and d.Kode_Perusahaan = e.Kode_Perusahaan and e.Kode_Perusahaan = f.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Routing = b.Id_Routing "
            SQL = SQL & "and a.Kode_stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.No_Faktur = d.No_PO "
            SQL = SQL & "and d.No_Transaksi = e.No_Production_Order "
            SQL = SQL & "and e.No_Transaksi = f.No_Transaksi "
            SQL = SQL & "and a.Status is null and d.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Faktur = 'PR0825-00007' "

            If Not Txt_IdRouting.Text.Trim.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and a.Id_Routing = '" & Txt_IdRouting.Text & "' "
            End If

            If Not Txt_KdBarang.Text.Trim.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and a.Kode_Barang = '" & Txt_KdBarang.Text & "' "
            End If

            If Not Txt_NoSplit.Text.Trim.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and d.No_Transaksi = '" & Txt_NoSplit.Text & "' "
            End If

            SQL = SQL & "group by a.Kode_Perusahaan, a.No_Faktur, d.No_Transaksi , a.Tanggal_Release , a.Jam_Release, d.Tgl_Produksi, d.Jam_Produksi, "
            SQL = SQL & "a.Id_Routing, b.Keterangan, a.Kode_Barang, c.Nama, f.Proses  "
            SQL = SQL & "order by a.No_Faktur, d.No_Transaksi, d.Tgl_Produksi, d.Jam_Produksi "
            Using Ds0 = BindingTrans(SQL)
                If Ds0.Tables("MyTable").Rows.Count <> 0 Then
                    DataUtama = Ds0
                Else
                    CloseConn()
                    MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            '============================
            '=     TAMPUNG NO SPLIT     =
            '============================
            Dim listNoSplit As New List(Of String)
            For Each row As DataRow In DataUtama.Tables("MyTable").Rows
                If Not listNoSplit.Contains("'" & row.Item("No_Split") & "'") Then
                    listNoSplit.Add("'" & row.Item("No_Split") & "'")
                End If
            Next


            '===========================
            '=     GET DATA REJECT     =
            '===========================
            Dim Data_Reject As DataTable
            Dim JoinSplit As String = String.Join(",", listNoSplit)
            SQL = "select z.No_Production_Order, x.Tahap, z.tanggal, isnull((sum(dbo.ubah_satuan(z.Kode_Perusahaan, 'masa',x.Kode_Barang, x.Satuan, 'PCS', x.Jumlah))), 0) as Jumlah "
            SQL = SQL & "from Emi_Production_Results_Validation z, Emi_Production_Results_Validation_Detail x "
            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and z.No_Transaksi = x.No_Transaksi "
            SQL = SQL & "and z.Status is null "
            SQL = SQL & "and x.Jenis <> 'Finished Good' "
            SQL = SQL & "and z.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and z.No_Production_Order IN (" & JoinSplit & ") "
            SQL = SQL & "and z.tanggal BETWEEN '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
            SQL = SQL & "group by z.No_Production_Order, x.Tahap, z.tanggal "

            SQL = SQL & "union all "

            SQL = SQL & "select z.No_Production_Order, x.Tahap, w.tanggal, isnull((sum(dbo.ubah_satuan(z.Kode_Perusahaan, 'masa',y.Kode_Barang, y.Satuan, 'PCS', y.Jumlah))), 0) as Jumlah "
            SQL = SQL & "from Emi_Production_Results_Validation z, Emi_Production_Results_Validation_Detail x, N_EMI_Validation_GR_3_Detail y, N_EMI_Validation_GR_3 w "
            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Perusahaan = y.Kode_Perusahaan and z.Kode_Perusahaan = w.Kode_Perusahaan and y.Kode_Perusahaan = w.Kode_Perusahaan "
            SQL = SQL & "and z.No_Transaksi = x.No_Transaksi "
            SQL = SQL & "and z.No_Transaksi = y.No_Transaksi_GR2 "
            SQL = SQL & "and z.No_Production_Order = w.No_Production_Order "
            SQL = SQL & "and y.No_Transaksi = w.No_Transaksi "
            SQL = SQL & "and z.Status is null "
            SQL = SQL & "and x.Jenis <> 'Finished Good' "
            SQL = SQL & "and z.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and z.No_Production_Order IN (" & JoinSplit & ") "
            SQL = SQL & "and w.tanggal BETWEEN '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
            SQL = SQL & "group by z.No_Production_Order, x.Tahap, w.tanggal "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Data_Reject = Ds.Tables("MyTable")
                End If
            End Using

#End Region


            '==================================
            '=     DEFINISIKAN NAMA KOLOM     =
            '==================================

            Dim DigitDecimal As String = ""

            Select Case arrJenis(Cmb_Jenis.SelectedIndex)
                Case "Y"
                    DigitDecimal = "PCS"
                Case "T"
                    DigitDecimal = "KG"
                Case Else
                    DigitDecimal = "PCS"
            End Select

#Region "Generate Coloms"

            Dim dataKoloms As New List(Of Dictionary(Of String, String)) From {
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Step"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No PO"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No Split"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Tanggal Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Jam Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Kode Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Nama Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Routing"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Batch"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Total Output"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Total Reject"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Total Reject (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}}
            }

            Dim rangeKolom As New Dictionary(Of String, Dictionary(Of String, Object)) From {
                {"Default", New Dictionary(Of String, Object) From {
                    {"Default", "Default"},
                    {"Kolom", 0}
                }}
            }

            For i As Integer = 0 To dataKoloms.Count - 1
                Dim kolom As Dictionary(Of String, String) = dataKoloms(i)

                If kolom("Identifier") = "Main" Then

                    xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Merge()

                    xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    'BORDER
                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "Reject" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Reject"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "Scrap" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Scrap"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                ElseIf kolom("Identifier") = "Waste" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Waste"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                ElseIf kolom("Identifier") = "Loss" Then

                    'Menambah nilai Range
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Merge()

                    xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).ColumnWidth = 25

                    'BORDER
                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "Inspection" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Inspection Good Received"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "Final_GR" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Good Received"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True


                ElseIf kolom("Identifier") = "Final" Then

                    'Menambah nilai Range
                    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    Else

                        Dim innerData As New Dictionary(Of String, Object)
                        innerData.Add("awal", i + 1)
                        innerData.Add("akhir", i + 1)

                        rangeKolom.Add(kolom("Identifier"), innerData)

                    End If

                    xlWorkSheet.Cells(4, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(4, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(4, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    xlWorkSheet.Cells(3, indexAwal).Value = "Final Report"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                    'BORDER
                    With xlWorkSheet.Cells(4, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                End If

            Next

#End Region


            Dim currentDate As Date = tanggalMulai
            '==============================
            '=     LOAD DINAMIS KOLUM     =
            '==============================
            Dim lastColumn As Integer = StartDinamicColumn
            For i As Integer = 0 To RangeTanggal
                Dim colName As String = Format(currentDate, "dd MMM yyyy")

                xlWorkSheet.Range(xlWorkSheet.Cells(3, (StartDinamicColumn + i) + 1), xlWorkSheet.Cells(4, (StartDinamicColumn + i) + 1)).Merge()

                xlWorkSheet.Cells(3, (StartDinamicColumn + i) + 1).Value = colName
                xlWorkSheet.Cells(3, (StartDinamicColumn + i) + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells(3, (StartDinamicColumn + i) + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                xlWorkSheet.Columns((StartDinamicColumn + i) + 1).AutoFit()

                'BORDER
                With xlWorkSheet.Range(xlWorkSheet.Cells(3, (StartDinamicColumn + i) + 1), xlWorkSheet.Cells(4, (StartDinamicColumn + i) + 1)).Borders
                    .LineStyle = excel.XlLineStyle.xlContinuous
                    .ColorIndex = 0
                    .TintAndShade = 0
                    .Weight = excel.XlBorderWeight.xlThin
                End With

                'BG COLOR
                xlWorkSheet.Cells(3, (StartDinamicColumn + i) + 1).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)

                'FONT
                xlWorkSheet.Cells(3, (StartDinamicColumn + i) + 1).Font.Bold = True


                currentDate = currentDate.AddDays(1)
                lastColumn += 1

            Next

            'Tambah Kolom Terakhir
#Region "Tambah Kolom Terakhir"

            xlWorkSheet.Range(xlWorkSheet.Cells(3, lastColumn + 1), xlWorkSheet.Cells(4, lastColumn + 1)).Merge()
            xlWorkSheet.Cells(3, lastColumn + 1).Value = "TOTAL"
            xlWorkSheet.Cells(3, lastColumn + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
            xlWorkSheet.Cells(3, lastColumn + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
            xlWorkSheet.Columns(lastColumn + 1).AutoFit()

            'BORDER
            With xlWorkSheet.Range(xlWorkSheet.Cells(3, lastColumn + 1), xlWorkSheet.Cells(4, lastColumn + 1)).Borders
                .LineStyle = excel.XlLineStyle.xlContinuous
                .ColorIndex = 0
                .TintAndShade = 0
                .Weight = excel.XlBorderWeight.xlThin
            End With

            'BG COLOR
            xlWorkSheet.Cells(3, lastColumn + 1).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)

            'FONT
            xlWorkSheet.Cells(3, lastColumn + 1).Font.Bold = True

#End Region

            Dim stringCenter As New List(Of Integer) From {0, 3, 4, 7, 8}


            '=========================
            '=     GENERATE BODY     =
            '=========================
            Try
                Dim rows As Integer = 0
                For i As Integer = 0 To DataUtama.Tables("MyTable").Rows.Count - 1
                    Dim mainRow As DataRow = DataUtama.Tables("MyTable").Rows(i)

                    For j As Integer = 0 To DataUtama.Tables("MyTable").Columns.Count - 1

                        If j + 1 = 11 Or j + 1 = 12 Then Continue For

                        Dim cell3 = xlWorkSheet.Cells(defaultRowIndex + i, j + 1)

                        cell3.Value = mainRow(j).ToString()

                        If j = 0 Then
                            cell3.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                        End If
                        If j + 1 = 10 Then
                            cell3.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen)
                        End If

                        If stringCenter.Contains(j) Then
                            cell3.HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                        End If

                        cell3.VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    Next


                    ' Isi utama
                    'Dgv_Rekap.Rows(i).Cells(cell_Step).Value = mainRow.Item("Step")
                    'Dgv_Rekap.Rows(i).Cells(cell_NoPO).Value = mainRow.Item("No_PO")
                    'Dgv_Rekap.Rows(i).Cells(cell_NoSplit).Value = mainRow.Item("No_Split")
                    'Dgv_Rekap.Rows(i).Cells(cell_TglProduksi).Value = Format(mainRow.Item("Tgl_Produksi"), "dd MMM yyyy")
                    'Dgv_Rekap.Rows(i).Cells(cell_JamProduksi).Value = mainRow.Item("Jam_Produksi")
                    'Dgv_Rekap.Rows(i).Cells(cell_KdBarang).Value = mainRow.Item("Kode_Barang")
                    'Dgv_Rekap.Rows(i).Cells(cell_NmBarang).Value = mainRow.Item("Nama_Barang")
                    'Dgv_Rekap.Rows(i).Cells(cell_Routing).Value = mainRow.Item("Routing")
                    'Dgv_Rekap.Rows(i).Cells(cell_Batch).Value = mainRow.Item("Batch")
                    'Dgv_Rekap.Rows(i).Cells(cell_TotalOutput).Value = mainRow.Item("Total_Output")

                    'Dgv_Rekap.Rows(i).Cells(cell_Step).Style.BackColor = Color.LightBlue
                    'Dgv_Rekap.Rows(i).Cells(cell_TotalOutput).Style.BackColor = Color.LightGreen

                    Dim totalRejectPeriode As Double = 0
                    Dim currentSplitNo As String = mainRow.Item("No_Split")
                    Dim currentBatch As Integer = Format(mainRow.Item("Batch"), "N0")

                    Dim lastColumn1 As Integer = StartDinamicColumn
                    ' Isi kolom sesuai dengan tanggal
                    currentDate = tanggalMulai
                    If Not Data_Reject Is Nothing Then
                        For j As Integer = 0 To RangeTanggal
                            Dim tglCari As Date = currentDate
                            Dim colName As String = Format(tglCari, "dd MMM yyyy")

                            ' Cari data menggunakan LINQ, lebih cepat dari looping (dapet dari Gemini hehe )
                            Dim dailyReject As Double = Data_Reject.AsEnumerable().
                               Where(Function(r) r.Field(Of String)("No_Production_Order") = currentSplitNo AndAlso
                                                 r.Field(Of Integer?)("Tahap") = currentBatch AndAlso
                                                 r.Field(Of Date?)("tanggal")?.Date = tglCari.Date).
                               Sum(Function(r) Convert.ToDouble(r("Jumlah")))

                            'Dgv_Rekap.Rows(i).Cells(colName).Value = dailyReject

                            Dim adasda As String = xlWorkSheet.Cells(3, (StartDinamicColumn + j) + 1).Value

                            If xlWorkSheet.Cells(3, (StartDinamicColumn + j) + 1).Value = colName Then
                                Dim cell0 = xlWorkSheet.Cells(defaultRowIndex + i, (StartDinamicColumn + j) + 1)

                                cell0.Value = Format(dailyReject, "N0")
                                cell0.VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                            End If



                            totalRejectPeriode += dailyReject
                            currentDate = currentDate.AddDays(1)
                            lastColumn1 += 1
                        Next

                    Else
                        For j As Integer = 0 To RangeTanggal
                            Dim tglCari As Date = currentDate
                            Dim colName As String = Format(tglCari, "dd MMM yyyy")
                            If xlWorkSheet.Cells(3, (StartDinamicColumn + j) + 1).Value = colName Then
                                Dim cell0 = xlWorkSheet.Cells(defaultRowIndex + i, (StartDinamicColumn + j) + 1)

                                cell0.Value = 0
                                cell0.VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                            End If

                            'Dgv_Rekap.Rows(i).Cells(colName).Value = 0
                            totalRejectPeriode += 0
                            currentDate = currentDate.AddDays(1)
                            lastColumn1 += 1
                        Next
                    End If

                    '' Isi kolom total dan persentase
                    'Dgv_Rekap.Rows(i).Cells("TOTAL").Value = Format(totalRejectPeriode, "N0")
                    'Dgv_Rekap.Rows(i).Cells(cell_TotalReject).Value = Format(totalRejectPeriode, "N0")

                    Dim cell4 = xlWorkSheet.Cells(defaultRowIndex + i, lastColumn1 + 1)
                    cell4.Value = Format(totalRejectPeriode, "N0")
                    cell4.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)


                    Dim cell = xlWorkSheet.Cells(defaultRowIndex + i, 11)
                    cell.Value = Format(totalRejectPeriode, "N0")

                    Dim totalOutput As Double = mainRow.Item("Total_Output")
                    Dim persenTotalReject As Double = If(totalOutput > 0, (totalRejectPeriode / totalOutput) * 100, 0)
                    'Dgv_Rekap.Rows(i).Cells(cell_TotalReject_Persen).Value = Format(persenTotalReject, "N2")

                    Dim cell2 = xlWorkSheet.Cells(defaultRowIndex + i, 12)
                    cell2.Value = Format(persenTotalReject, "N2")


                    cell4.VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    cell.VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    cell2.VerticalAlignment = excel.XlVAlign.xlVAlignCenter



                    rows += 1
                Next



                '==============================
                '=     GENERATE BODY LAMA     =
                '==============================


                'Dim stringCenter As New List(Of Integer) From {3, 4, 6, 29}

                'Dim numberColumn As New List(Of Integer) From {8, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33}

                'Dim DecimalColumn As New List(Of Integer) From {8, 25, 27}

                'Dim NumberN0 As New List(Of Integer) From {8, 9, 21, 25, 27}





                '    ' Ambil format sesuai culture
                '    Dim culture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentCulture
                '    xlApp.UseSystemSeparators = True

                '    '==  AMBIL SEPARATOR DARI EXCEL =='
                '    Dim decimalSep As String = xlApp.DecimalSeparator
                '    Dim groupSep As String = xlApp.ThousandsSeparator

                '    '==  AMBIL SEPARATOR DARI SISTEM =='
                '    'Dim decimalSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator
                '    'Dim groupSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator

                '    If decimalSep = "," Then
                '        decimalSep = "."
                '    ElseIf decimalSep = "." Then
                '        decimalSep = ","
                '    End If

                '    If groupSep = "." Then
                '        groupSep = ","
                '    ElseIf groupSep = "," Then
                '        groupSep = "."
                '    End If

                '    Dim templateFormat As String = "#GROUP##0DEC0000"
                '    Dim excelFormat As String = templateFormat _
                '        .Replace("GROUP", groupSep) _
                '        .Replace("DEC", decimalSep)

                '    Dim templateFormatN0 As String = "#GROUP##0"
                '    Dim excelFormatN0 As String = templateFormatN0 _
                '        .Replace("GROUP", groupSep)





                '    Dim row As Integer = 0
                '    SQL = "select no_po, No_split, Tgl_Produksi, Jam_Produksi, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, Berat_GI, Jumlah_Dosing, "
                '    SQL = SQL & "Pro_Reject_KG, Qc_Reject_KG, Warehouse_Reject_KG, Tot_Reject_KG, " ' Reject LINE
                '    SQL = SQL & "ScrapGR1_KG, ScrapGR2_KG, ScrapGR3_KG, ScrapTotal_KG, "
                '    SQL = SQL & "WasteGR1_KG, WasteGR2_KG, WasteGR3_KG, WasteTotal_KG, Loss_Production_Final_GR, "
                '    SQL = SQL & "NilaiGRFinal_KG, NilaiGRFinal_Pcs, "
                '    SQL = SQL & "Reject_Final_Persen, ScrapTotal_Persen, WasteTotal_Persen, Loss_Production_Final_GR_Persen, NilaiGRFinal_Persen, GR_Inspection_KG, GR_Inspection_PCS, GR_Inspection_Persen, Status "
                '    SQL = SQL & "from Laporan_Akhir_GIGR_Rekap2 "
                '    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                '    SQL = SQL & "and Tgl_Produksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" '& Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                '    If Not Txt_IdRouting.Text.ToUpper = OpsiSeluruh.ToUpper Then
                '        SQL = SQL & "and Id_Routing = '" & Txt_IdRouting.Text & "' "
                '    End If
                '    If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                '        SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
                '    End If
                '    If Txt_NoSplit.Text.Trim.Length > 0 AndAlso Txt_NoSplit.Text.ToUpper <> OpsiSeluruh.ToUpper Then
                '        SQL = SQL & "and no_split like '%" & Txt_NoSplit.Text & "%' "
                '    End If

                '    If Not Cmb_Jenis.SelectedIndex = 0 Then
                '        SQL = SQL & "and Group_Jenis = '" & arrJenis(Cmb_Jenis.SelectedIndex) & "' "
                '    End If
                '    SQL = SQL & "order by no_split, Tgl_Produksi, Jam_Produksi "
                '    Using Ds = BindingTrans(SQL)
                '        With Ds.Tables("MyTable")

                '            For i As Integer = 0 To .Rows.Count - 1

                '                For colIndex As Integer = 0 To .Columns.Count - 1
                '                    Dim cell = xlWorkSheet.Cells(i + defaultRowIndex, colIndex + 1)
                '                    If colIndex = 6 Then
                '                        cell.NumberFormat = "@"
                '                    End If

                '                    cell.Value = General_Class.CekNULL(.Rows(i).Item(colIndex))

                '                    cell.VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                '                    ' Format numerik (N4)
                '                    If numberColumn.Contains(colIndex) Then
                '                        Dim nilai As Double = If(General_Class.CekNULL(.Rows(i).Item(colIndex)) = "", 0, .Rows(i).Item(colIndex))

                '                        If NumberN0.Contains(colIndex) Then
                '                            cell.NumberFormat = excelFormatN0
                '                        Else
                '                            cell.NumberFormat = excelFormat
                '                        End If

                '                        If DecimalColumn.Contains(colIndex) Then
                '                            If arrJenis(Cmb_Jenis.SelectedIndex) = "Y" Then
                '                                cell.NumberFormat = excelFormatN0
                '                            ElseIf arrJenis(Cmb_Jenis.SelectedIndex) = "T" Then
                '                                cell.NumberFormat = excelFormat
                '                            End If
                '                        End If


                '                        cell.Value = nilai

                '                    End If



                '                    '== ATUR ALIGMENT CELL =='
                '                    Select Case .Columns(colIndex).DataType.Name
                '                        Case "String"
                '                            cell.HorizontalAlignment = If(stringCenter.Contains(colIndex), excel.XlHAlign.xlHAlignCenter, excel.XlHAlign.xlHAlignLeft)
                '                        Case "DateTime"
                '                            cell.HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                '                            cell.Value = Format(CDate(.Rows(i).Item(colIndex)), "dd MMM yyyy")
                '                        Case "Int32", "Double"
                '                            cell.HorizontalAlignment = excel.XlHAlign.xlHAlignRight
                '                            cell.HorizontalAlignment = If(stringCenter.Contains(colIndex), excel.XlHAlign.xlHAlignCenter, excel.XlHAlign.xlHAlignRight)
                '                    End Select

                '                    ' BORDER
                '                    With cell.Borders
                '                        .LineStyle = excel.XlLineStyle.xlContinuous
                '                        .ColorIndex = 0
                '                        .Weight = excel.XlBorderWeight.xlThin
                '                    End With

                '                    ' BG COLOR
                '                    Select Case colIndex
                '                        Case 11 To 14
                '                            If .Columns(colIndex).ColumnName = "WaktuGR1" Then
                '                                cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 217, 153))
                '                            Else
                '                                cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 217, 153))
                '                            End If
                '                        Case 15 To 18
                '                            If .Columns(colIndex).ColumnName = "WaktuGR1" Then
                '                                cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(252, 105, 108))
                '                            Else
                '                                cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow)
                '                            End If
                '                        Case 19 To 22
                '                            If .Columns(colIndex).ColumnName = "WaktuGR2" Then
                '                                cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(181, 230, 162))
                '                            Else
                '                                cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                '                            End If
                '                        Case 23
                '                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen)
                '                        Case 24 To 25
                '                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightCoral)
                '                        Case 26 To 27
                '                            If .Columns(colIndex).ColumnName = "WaktuGR3" Then
                '                                cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
                '                            Else
                '                                cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
                '                            End If
                '                        Case 28 To 33
                '                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightCyan)

                '                        Case 34
                '                            If .Rows(i).Item("Status") = "PRODUCTION" Then
                '                                cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow)
                '                            ElseIf .Rows(i).Item("Status") = "INSPECTION" Then
                '                                cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                '                            ElseIf .Rows(i).Item("Status") = "WAREHOUSE" Then
                '                                cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
                '                            End If
                '                    End Select

                '                    xlWorkSheet.Cells(1, 1).Interior.TintAndShade = 0.2

                '                Next

                '                row += 1

                '            Next

                '        End With
                '    End Using

                ' AutoFit kolom setelah semua data dimasukkan
                xlWorkSheet.Columns.AutoFit()

                '==========================
                '=     HEADER LAPORAN     =
                '==========================
                Dim panjangKolom As Integer = dataKoloms.Count

                xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, panjangKolom)).Merge()

                xlWorkSheet.Cells(1, 1).Value = JudulLaporan
                xlWorkSheet.Cells(1, 1).Font.Size = 14
                xlWorkSheet.Cells(1, 1).Font.Bold = True
                xlWorkSheet.Cells(1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells(1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                xlWorkSheet.Columns(1).AutoFit()

                '==========================
                '=     FOOTER LAPORAN     =
                '==========================
                Dim jumlahRows As Integer = rows + defaultRowIndex

                Dim Footer As String = "| " & Format(tgl_skg, "dd MMM yyyy") & " | " & Format(tgl_skg, "HH:mm:ss")

                xlWorkSheet.Cells(jumlahRows + 1, 1).Value = Footer
                xlWorkSheet.Cells(jumlahRows + 1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells(jumlahRows + 1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                xlWorkSheet.Columns(1).AutoFit()


            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try



            '=====================
            '=     SAVE FILE     =
            '=====================
            Dim saveFileDialog As New SaveFileDialog()

            ' Set File Filter
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*"
            saveFileDialog.Title = "Save As"


            'Tampilkan Show Dialog Save as
            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Try
                    Dim filePath As String = saveFileDialog.FileName

                    xlWorkBook.SaveAs(filePath, excel.XlFileFormat.xlOpenXMLWorkbook)

                    'MessageBox.Show("File berhasil disimpan di: " & filePath)

                    ' Menutup workbook dan aplikasi Excel
                    xlWorkBook.Close()
                    xlApp.Quit()

                    ' Membebaskan objek Excel
                    releaseObject(xlWorkSheet)
                    releaseObject(xlWorkBook)
                    releaseObject(xlApp)

                Catch ex As Exception
                    MessageBox.Show("Terjadi kesalahan saat menyimpan file: " & ex.Message)
                End Try
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Generate_Excel_Rekap(ByVal sql As String)
        Try

            get_jam()

            Dim xlApp As excel.Application = New Microsoft.Office.Interop.Excel.Application()

            '=======================================
            '=     CEK APAKAH EXCEL TERINSTALL     =
            '=======================================
            If xlApp Is Nothing Then
                MessageBox.Show("Excel is not properly installed!!")
                Return
            End If

            Dim JudulLaporan As String = "LAPORAN FINAL GI GR REKAP"

            Dim xlWorkBook As excel.Workbook
            Dim xlWorkSheet As excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value

            'Dim lokasi_file As String = Forms.Application.StartupPath & "\" & My.Computer.Name

            'If System.IO.Directory.Exists(lokasi_file) = False Then
            '    System.IO.Directory.CreateDirectory(lokasi_file)
            'End If

            Dim format_akhir As String = Format(Now(), "ddMMMyyyyHHmmss")
            Dim nama_file As String = "Testing_Excel " & format_akhir & ".xlsx"

            xlWorkBook = xlApp.Workbooks.Add(misValue)
            xlWorkSheet = xlWorkBook.Sheets("Sheet1")

            '==================================
            '=     DEFINISIKAN NAMA KOLOM     =
            '==================================
#Region "Generate Coloms"

            Dim dataKoloms As New List(Of Dictionary(Of String, String)) From {
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No PO"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "No Split"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Tanggal Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Jam Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Routing"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Keterangan"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Kode Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Nama Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Jumlah"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Satuan"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Berat (Gram)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Good Issue (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Good Received Lv1 (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Waste Lv1 (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Good Received Lv2 (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Waste Received Lv2 (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Waste After Received Lv2 (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 151, 193))}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Final Good Received (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Waste (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(160, 160, 160))}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(160, 160, 160))}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Loss (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(185, 255, 255))}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Loss (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(185, 255, 255))}}
            }

            Dim rangeKolom As New Dictionary(Of String, Dictionary(Of String, Object)) From {
                {"Default", New Dictionary(Of String, Object) From {
                    {"Default", "Default"},
                    {"Kolom", 0}
                }}
            }

            For i As Integer = 0 To dataKoloms.Count - 1
                Dim kolom As Dictionary(Of String, String) = dataKoloms(i)

                If kolom("Identifier") = "Main" Then

                    'xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Merge()

                    xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
                    xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Columns(i + 1).AutoFit()

                    'BORDER
                    'With xlWorkSheet.Range(xlWorkSheet.Cells(3, i + 1), xlWorkSheet.Cells(4, i + 1)).Borders
                    '    .LineStyle = excel.XlLineStyle.xlContinuous
                    '    .ColorIndex = 0
                    '    .TintAndShade = 0
                    '    .Weight = excel.XlBorderWeight.xlThin
                    'End With

                    With xlWorkSheet.Cells(3, i + 1).Borders
                        .LineStyle = excel.XlLineStyle.xlContinuous
                        .ColorIndex = 0
                        .TintAndShade = 0
                        .Weight = excel.XlBorderWeight.xlThin
                    End With

                    'BG COLOR
                    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")

                    'FONT
                    xlWorkSheet.Cells(3, i + 1).Font.Bold = True


                    'ElseIf kolom("Identifier") = "GR1" Then

                    '    'Menambah nilai Range
                    '    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                    '        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    '    Else

                    '        Dim innerData As New Dictionary(Of String, Object)
                    '        innerData.Add("awal", i + 1)
                    '        innerData.Add("akhir", i + 1)

                    '        rangeKolom.Add(kolom("Identifier"), innerData)

                    '    End If

                    '    xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
                    '    xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    '    xlWorkSheet.Cells(3, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    '    xlWorkSheet.Columns(i + 1).AutoFit()

                    '    'Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    '    'Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    '    'xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    '    'xlWorkSheet.Cells(3, indexAwal).Value = "Line Production (Good Received Lv I)"
                    '    'xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    '    'xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    '    'xlWorkSheet.Range(xlWorkSheet.Columns(3), xlWorkSheet.Columns(i + 1)).EntireColumn.AutoFit()

                    '    'BORDER
                    '    With xlWorkSheet.Cells(3, i + 1).Borders
                    '        .LineStyle = excel.XlLineStyle.xlContinuous
                    '        .ColorIndex = 0
                    '        .TintAndShade = 0
                    '        .Weight = excel.XlBorderWeight.xlThin
                    '    End With

                    '    'With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                    '    '    .LineStyle = excel.XlLineStyle.xlContinuous
                    '    '    .ColorIndex = 0
                    '    '    .TintAndShade = 0
                    '    '    .Weight = excel.XlBorderWeight.xlThin
                    '    'End With

                    '    'BG COLOR
                    '    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    '    'xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    '    'FONT
                    '    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    '    'xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                    'ElseIf kolom("Identifier") = "GR2" Then

                    '    'Menambah nilai Range
                    '    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                    '        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    '    Else

                    '        Dim innerData As New Dictionary(Of String, Object)
                    '        innerData.Add("awal", i + 1)
                    '        innerData.Add("akhir", i + 1)

                    '        rangeKolom.Add(kolom("Identifier"), innerData)

                    '    End If

                    '    xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
                    '    xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    '    xlWorkSheet.Cells(3, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    '    xlWorkSheet.Columns(i + 1).AutoFit()

                    '    'Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    '    'Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    '    'xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    '    'xlWorkSheet.Cells(3, indexAwal).Value = "Quality Inspection (Good Received Lv II)"
                    '    'xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    '    'xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    '    'xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).EntireColumn.AutoFit()

                    '    'BORDER
                    '    With xlWorkSheet.Cells(3, i + 1).Borders
                    '        .LineStyle = excel.XlLineStyle.xlContinuous
                    '        .ColorIndex = 0
                    '        .TintAndShade = 0
                    '        .Weight = excel.XlBorderWeight.xlThin
                    '    End With

                    '    'With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                    '    '    .LineStyle = excel.XlLineStyle.xlContinuous
                    '    '    .ColorIndex = 0
                    '    '    .TintAndShade = 0
                    '    '    .Weight = excel.XlBorderWeight.xlThin
                    '    'End With

                    '    'BG COLOR
                    '    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    '    'xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    '    'FONT
                    '    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    '    xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                    'ElseIf kolom("Identifier") = "GR3" Then

                    '    'Menambah nilai Range
                    '    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                    '        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    '    Else

                    '        Dim innerData As New Dictionary(Of String, Object)
                    '        innerData.Add("awal", i + 1)
                    '        innerData.Add("akhir", i + 1)

                    '        rangeKolom.Add(kolom("Identifier"), innerData)

                    '    End If

                    '    xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
                    '    xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    '    xlWorkSheet.Cells(3, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    '    xlWorkSheet.Columns(i + 1).AutoFit()

                    '    'Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    '    'Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    '    'xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    '    'xlWorkSheet.Cells(3, indexAwal).Value = "Good Received Rejected"
                    '    'xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    '    'xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    '    'xlWorkSheet.Range(xlWorkSheet.Columns(3), xlWorkSheet.Columns(i + 1)).EntireColumn.AutoFit()

                    '    'BORDER
                    '    With xlWorkSheet.Cells(3, i + 1).Borders
                    '        .LineStyle = excel.XlLineStyle.xlContinuous
                    '        .ColorIndex = 0
                    '        .TintAndShade = 0
                    '        .Weight = excel.XlBorderWeight.xlThin
                    '    End With

                    '    'With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                    '    '    .LineStyle = excel.XlLineStyle.xlContinuous
                    '    '    .ColorIndex = 0
                    '    '    .TintAndShade = 0
                    '    '    .Weight = excel.XlBorderWeight.xlThin
                    '    'End With

                    '    'BG COLOR
                    '    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    '    'xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    '    'FONT
                    '    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    '    'xlWorkSheet.Cells(4, i + 1).Font.Bold = True


                    'ElseIf kolom("Identifier") = "GR4" Then

                    '    'Menambah nilai Range
                    '    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                    '        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    '    Else

                    '        Dim innerData As New Dictionary(Of String, Object)
                    '        innerData.Add("awal", i + 1)
                    '        innerData.Add("akhir", i + 1)

                    '        rangeKolom.Add(kolom("Identifier"), innerData)

                    '    End If

                    '    xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
                    '    xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    '    xlWorkSheet.Cells(3, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    '    xlWorkSheet.Columns(i + 1).AutoFit()

                    '    'Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    '    'Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    '    'xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    '    'xlWorkSheet.Cells(3, indexAwal).Value = "Final Good Received"
                    '    'xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    '    'xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    '    'xlWorkSheet.Range(xlWorkSheet.Columns(3), xlWorkSheet.Columns(i + 1)).EntireColumn.AutoFit()

                    '    'BORDER
                    '    With xlWorkSheet.Cells(3, i + 1).Borders
                    '        .LineStyle = excel.XlLineStyle.xlContinuous
                    '        .ColorIndex = 0
                    '        .TintAndShade = 0
                    '        .Weight = excel.XlBorderWeight.xlThin
                    '    End With

                    '    'With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                    '    '    .LineStyle = excel.XlLineStyle.xlContinuous
                    '    '    .ColorIndex = 0
                    '    '    .TintAndShade = 0
                    '    '    .Weight = excel.XlBorderWeight.xlThin
                    '    'End With

                    '    'BG COLOR
                    '    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    '    'xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    '    'FONT
                    '    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    '    'xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                    'ElseIf kolom("Identifier") = "GR5" Then

                    '    'Menambah nilai Range
                    '    If rangeKolom.ContainsKey(kolom("Identifier")) Then
                    '        rangeKolom(kolom("Identifier"))("akhir") = i + 1
                    '    Else

                    '        Dim innerData As New Dictionary(Of String, Object)
                    '        innerData.Add("awal", i + 1)
                    '        innerData.Add("akhir", i + 1)

                    '        rangeKolom.Add(kolom("Identifier"), innerData)

                    '    End If

                    '    xlWorkSheet.Cells(3, i + 1).Value = kolom("Kolom")
                    '    xlWorkSheet.Cells(3, i + 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    '    xlWorkSheet.Cells(3, i + 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    '    xlWorkSheet.Columns(i + 1).AutoFit()

                    '    'Dim indexAwal As Integer = rangeKolom(kolom("Identifier"))("awal")
                    '    'Dim indexAkhir As Integer = rangeKolom(kolom("Identifier"))("akhir")
                    '    'xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).Merge()
                    '    'xlWorkSheet.Cells(3, indexAwal).Value = "Final Good Received"
                    '    'xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    '    'xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    '    'xlWorkSheet.Range(xlWorkSheet.Columns(3), xlWorkSheet.Columns(i + 1)).EntireColumn.AutoFit()

                    '    'BORDER
                    '    With xlWorkSheet.Cells(3, i + 1).Borders
                    '        .LineStyle = excel.XlLineStyle.xlContinuous
                    '        .ColorIndex = 0
                    '        .TintAndShade = 0
                    '        .Weight = excel.XlBorderWeight.xlThin
                    '    End With

                    '    'With xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(4, indexAkhir)).Borders
                    '    '    .LineStyle = excel.XlLineStyle.xlContinuous
                    '    '    .ColorIndex = 0
                    '    '    .TintAndShade = 0
                    '    '    .Weight = excel.XlBorderWeight.xlThin
                    '    'End With

                    '    'BG COLOR
                    '    xlWorkSheet.Cells(3, i + 1).Interior.Color = kolom("Warna")
                    '    'xlWorkSheet.Cells(4, i + 1).Interior.Color = kolom("Warna")

                    '    'FONT
                    '    xlWorkSheet.Cells(3, i + 1).Font.Bold = True
                    '    'xlWorkSheet.Cells(4, i + 1).Font.Bold = True

                End If

            Next

#End Region


            '=========================
            '=     GENERATE BODY     =
            '=========================

            Dim stringCenter As New List(Of Integer) From {3, 6, 9}

            Dim numberColumn As New List(Of Integer) From {8, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21}

            Dim defaultRowIndex As Integer = 4
            Try
                OpenConn()

                ' Ambil format sesuai culture
                Dim culture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentCulture
                xlApp.UseSystemSeparators = True


                '==  AMBIL SEPARATOR DARI EXCEL =='
                'Dim decimalSep As String = xlApp.DecimalSeparator
                'Dim groupSep As String = xlApp.ThousandsSeparator

                '==  AMBIL SEPARATOR DARI SISTEM =='
                Dim decimalSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator
                Dim groupSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator

                If decimalSep = "," Then
                    decimalSep = "."
                ElseIf decimalSep = "." Then
                    decimalSep = ","
                End If

                If groupSep = "." Then
                    groupSep = ","
                ElseIf groupSep = "," Then
                    groupSep = "."
                End If

                Dim templateFormat As String = "#GROUP##0DEC0000"
                Dim excelFormat As String = templateFormat _
                    .Replace("GROUP", groupSep) _
                    .Replace("DEC", decimalSep)





                Dim row As Integer = 0
                'sql = "select No_PO, no_split, Tgl_Produksi, Jam_Produksi, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, satuan, batch, Jumlah_Dosing, NilaiGR1_Pcs, NilaiGR1_KG, ScrapGR1_KG, Loss_Production, Persen_WasteGR1, "
                'sql = sql & "NilaiGR2_Pcs, NilaiGR2_KG, ScrapGR2_KG, Persen_WasteGR2, NilaiAfterGR_Pcs, NilaiAfterGR_KG, Persen_WasteGR3, NilaiGRFinal_Pcs, NilaiGRFinal_KG, Total_Waste "
                'sql = sql & "from Laporan_Akhir_GIGR "
                'sql = sql & "where Kode_Perusahaan = '001' "
                'sql = sql & "and Tgl_Produksi between '2022-12-20 00:00:00.000' and '2030-12-20 00:00:00.000' "
                Using Ds = BindingTrans(sql)
                    With Ds.Tables("MyTable")

                        For i As Integer = 0 To .Rows.Count - 1

                            For colIndex As Integer = 0 To .Columns.Count - 1
                                Dim cell = xlWorkSheet.Cells(i + defaultRowIndex, colIndex + 1)
                                If colIndex = 6 Then
                                    cell.NumberFormat = "@"
                                End If

                                cell.Value = General_Class.CekNULL(.Rows(i).Item(colIndex))

                                cell.VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                                ' Format numerik (N4)
                                If numberColumn.Contains(colIndex) Then
                                    Dim nilai As Double = If(General_Class.CekNULL(.Rows(i).Item(colIndex)) = "", 0, .Rows(i).Item(colIndex))

                                    cell.NumberFormat = excelFormat
                                    cell.Value = nilai

                                End If





                                '== ATUR ALIGMENT CELL =='
                                Select Case .Columns(colIndex).DataType.Name
                                    Case "String"
                                        cell.HorizontalAlignment = If(stringCenter.Contains(colIndex), excel.XlHAlign.xlHAlignCenter, excel.XlHAlign.xlHAlignLeft)
                                    Case "DateTime"
                                        cell.HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                                        cell.Value = Format(CDate(.Rows(i).Item(colIndex)), "dd MMM yyyy")
                                    Case "Int32", "Double"
                                        cell.HorizontalAlignment = excel.XlHAlign.xlHAlignRight
                                        cell.HorizontalAlignment = If(stringCenter.Contains(colIndex), excel.XlHAlign.xlHAlignCenter, excel.XlHAlign.xlHAlignRight)
                                End Select

                                ' BORDER
                                With cell.Borders
                                    .LineStyle = excel.XlLineStyle.xlContinuous
                                    .ColorIndex = 0
                                    .Weight = excel.XlBorderWeight.xlThin
                                End With

                                ' BG COLOR
                                Select Case colIndex
                                    Case 0 To 10
                                        cell.EntireColumn.AutoFit()
                                    Case 11
                                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
                                        cell.Columns(i).ColumnWidth = 28
                                    Case 12 To 13
                                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow)
                                        cell.Columns(i).ColumnWidth = 28
                                    Case 14 To 15
                                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                                        cell.Columns(i).ColumnWidth = 28
                                    Case 16
                                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightPink)
                                        cell.Columns(i).ColumnWidth = 28
                                    Case 17
                                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen)
                                        cell.Columns(i).ColumnWidth = 28
                                    Case 18 To 19
                                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
                                        cell.Columns(i).ColumnWidth = 28
                                    Case 20 To 21
                                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightCyan)
                                        cell.Columns(i).ColumnWidth = 28
                                        'Case 20 To 21
                                        '    cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen)
                                        '    cell.Columns(i).ColumnWidth = 20
                                End Select

                                xlWorkSheet.Cells(1, 1).Interior.TintAndShade = 0.2

                            Next

                            row += 1

                        Next

                    End With
                End Using

                ' AutoFit kolom setelah semua data dimasukkan
                'xlWorkSheet.Columns.AutoFit()

                '==========================
                '=     HEADER LAPORAN     =
                '==========================
                Dim panjangKolom As Integer = dataKoloms.Count

                xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, panjangKolom)).Merge()

                xlWorkSheet.Cells(1, 1).Value = JudulLaporan
                xlWorkSheet.Cells(1, 1).Font.Size = 14
                xlWorkSheet.Cells(1, 1).Font.Bold = True
                xlWorkSheet.Cells(1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells(1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter


                '==========================
                '=     FOOTER LAPORAN     =
                '==========================
                Dim jumlahRows As Integer = row + defaultRowIndex

                Dim Footer As String = "| " & Format(tgl_skg, "dd MMM yyyy") & " | " & Format(tgl_skg, "HH:mm:ss")

                xlWorkSheet.Cells(jumlahRows + 1, 1).Value = Footer
                xlWorkSheet.Cells(jumlahRows + 1, 1).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells(jumlahRows + 1, 1).VerticalAlignment = excel.XlVAlign.xlVAlignCenter

                'xlWorkSheet.Columns(1).AutoFit()


                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try



            '=====================
            '=     SAVE FILE     =
            '=====================
            Dim saveFileDialog As New SaveFileDialog()

            ' Set File Filter
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*"
            saveFileDialog.Title = "Save As"


            'Tampilkan Show Dialog Save as
            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Try
                    Dim filePath As String = saveFileDialog.FileName

                    xlWorkBook.SaveAs(filePath, excel.XlFileFormat.xlOpenXMLWorkbook)

                    'MessageBox.Show("File berhasil disimpan di: " & filePath)

                    ' Menutup workbook dan aplikasi Excel
                    xlWorkBook.Close()
                    xlApp.Quit()

                    ' Membebaskan objek Excel
                    releaseObject(xlWorkSheet)
                    releaseObject(xlWorkBook)
                    releaseObject(xlApp)

                Catch ex As Exception
                    MessageBox.Show("Terjadi kesalahan saat menyimpan file: " & ex.Message)
                End Try
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_NoSplit_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NoSplit.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Split.Focus()
    End Sub

    Private Sub Lv_Split_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Split.DoubleClick
        If Lv_Split.Items.Count = 0 Or Lv_Split.FocusedItem.Index = -1 Then Exit Sub

        Dim NoFaktur As String = Lv_Split.FocusedItem.SubItems(0).Text

        Txt_NoSplit.Text = NoFaktur

        Lv_Split.Visible = False
        Lv_Split.Location = New Point(1200, 219)

        Cmb_Jenis.Focus()
    End Sub

    Private Sub Lv_Split_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Split.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Split_DoubleClick(Lv_Split, e)
        End If
    End Sub

    Private Sub Tgl1_Leave(sender As Object, e As EventArgs) Handles Tgl1.Leave, Tgl2.Leave
        If Tgl1.Focused Or Tgl2.Focused Then Exit Sub

        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        End If

        Dim selisih As TimeSpan = Tgl2.Value.Date - Tgl1.Value.Date

        If selisih.Days > 30 Then
            MessageBox.Show("Range lebih dari 1 bulan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        End If

    End Sub



    '=========================================================================================================================================================================================================
    '=     TRACKING GLOBAL
    '=========================================================================================================================================================================================================


    Private Sub TabControl2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl2.SelectedIndexChanged
        If TabControl2.SelectedIndex = 1 Then
            Kosong_Tab2()
        End If
    End Sub

    Private Sub Kosong_Tab2()
        Tgl_Prod_1.Value = Now.Date : Tgl_Prod_2.Value = Now.Date
        Tab2_Cmb_Step.SelectedIndex = 0

        Tab2_Dgv_Data_Global.Rows.Clear()
        Load_Lv_Global()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Btn_Global_Cari.Click

        Load_Lv_Global(True)
    End Sub

    Private Sub Btn_Global_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Global_Refresh.Click


        Kosong_Tab2()
    End Sub



    Private Sub Load_Lv_Global(ByVal Optional Filter As Boolean = False)
        If Tab2_Cmb_Step.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tab2_Cmb_Step.DroppedDown = True : Tab2_Cmb_Step.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            Tab2_Dgv_Data_Global.Rows.Clear()
            SQL = "select Distinct a.No_Production_Order, a.Tanggal, a.Jam, a.UserID, d.Tahap as Batch, e.Qty_Hasil_Produksi, e.Satuan, e.Kode_Barang, f.nama as Nama_Barang, "
            'SQL = SQL & "DATEDIFF(DAY, a.Tanggal, GETDATE()) AS jumlah_hari, "

            SQL = SQL & "ISNULL( CASE "
            SQL = SQL & "WHEN Military_Sampling_2.Status = 'READY FOR PACKAGING' "
            SQL = SQL & "AND Military_Sampling_2.Tanggal IS NOT NULL "
            SQL = SQL & "THEN DATEDIFF(DAY, a.Tanggal, Military_Sampling_2.Tanggal) "
            SQL = SQL & "else  DATEDIFF(DAY, a.Tanggal, GETDATE()) "
            SQL = SQL & "END, 0) AS jumlah_hari, "

            SQL = SQL & "isnull(hasil_lab.status, 'T') as Lab_Status, "
            SQL = SQL & "isnull(hasil_lab.Tanggal, '-') as Lab_Tanggal, "
            SQL = SQL & "isnull(hasil_lab.Jam, '-') as Lab_Jam, "
            SQL = SQL & "isnull(hasil_lab.id_user, '-') as Lab_UserID, "

            SQL = SQL & "isnull(Military_Sampling_1.Status, 'T') as Military_Sampling_1_Status, "
            SQL = SQL & "isnull(Military_Sampling_1.Tanggal, '-') as Military_Sampling_1_Tanggal, "
            SQL = SQL & "isnull(Military_Sampling_1.Jam, '-') as Military_Sampling_1_Jam, "
            SQL = SQL & "isnull(Military_Sampling_1.UserID, '-') as Military_Sampling_1_UserID, "

            SQL = SQL & "isnull(GR_2.Status, 'T') as GR_2_Status, "
            SQL = SQL & "isnull(GR_2.Tanggal, '-') as GR_2_Tanggal, "
            SQL = SQL & "isnull(GR_2.Jam, '-') as GR_2_Jam, "
            SQL = SQL & "isnull(GR_2.UserID, '-') as GR_2_UserID, "

            SQL = SQL & "isnull(Military_Sampling_2.Status, 'T') as Military_Sampling_2_Status, "
            SQL = SQL & "isnull(Military_Sampling_2.Tanggal, '-') as Military_Sampling_2_Tanggal, "
            SQL = SQL & "isnull(Military_Sampling_2.Jam, '-') as Military_Sampling_2_Jam, "
            SQL = SQL & "isnull(Military_Sampling_2.UserID, '-') as Military_Sampling_2_UserID "

            SQL = SQL & "from Emi_Production_Results a "
            SQL = SQL & "inner join Emi_Split_Production_Order b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Production_Order = b.No_Transaksi "
            SQL = SQL & "inner join EMI_Order_Produksi c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_PO = c.No_Faktur "
            SQL = SQL & "inner join Emi_Production_Results_Detail_Pallet d on a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Transaksi = d.No_Transaksi "
            SQL = SQL & "inner join emi_production_results_detail_barang e on a.Kode_Perusahaan = e.Kode_Perusahaan and a.No_Transaksi = e.No_Transaksi and d.Proses = e.Proses "
            SQL = SQL & "inner join barang f on e.kode_perusahaan = f.kode_perusahaan and e.Kode_Stock_Owner = f.Kode_Stock_Owner and e.Kode_Barang = f.Kode_Barang "

            SQL = SQL & "OUTER APPLY ( "
            SQL = SQL & "SELECT TOP 1 "
            SQL = SQL & "CASE WHEN z.Flag_Ok = 'Y' THEN 'VALIDATED' "
            SQL = SQL & "WHEN z.Flag_Ok IS NULL THEN 'ON PROCESS' "
            SQL = SQL & "ELSE 'UNDEFINED' "
            SQL = SQL & "END AS Status, "
            SQL = SQL & "ISNULL(FORMAT(z.Tanggal, 'dd MMM yyyy'), '-') AS Tanggal, "
            SQL = SQL & "ISNULL(FORMAT(CAST(z.jam AS datetime), 'HH:mm:ss'), '-') AS Jam, "
            SQL = SQL & "isnull(z.Id_User, '-') as Id_User "
            SQL = SQL & "FROM N_EMI_LAB_Hasil_Uji_Validasi_Final z "
            SQL = SQL & "WHERE z.No_Po = c.No_Faktur "
            SQL = SQL & "and z.No_Split_Po = a.No_Production_Order "
            SQL = SQL & "and z.No_Batch = d.Tahap "
            SQL = SQL & "ORDER BY z.Tanggal, z.jam DESC "
            SQL = SQL & ") hasil_lab "

            SQL = SQL & "OUTER APPLY ( "
            SQL = SQL & "select top 1 case when z.Flag_Military_Sampling = 'Y' and z.Flag_Ready_For_Packaging = 'Y' then 'READY FOR PACKAGING' "
            SQL = SQL & "when z.Flag_Military_Sampling = 'Y' and z.Flag_Ready_For_Packaging is null then 'HOLD' "
            SQL = SQL & "when z.Flag_Military_Sampling is null and z.Flag_Ready_For_Packaging is null then 'UNTEST' "
            SQL = SQL & "else 'UNDEFINED' "
            SQL = SQL & "end as Status, "
            SQL = SQL & "ISNULL(FORMAT(z.Tanggal, 'dd MMM yyyy'), '-') AS Tanggal,  "
            SQL = SQL & "ISNULL(FORMAT(CAST(z.jam AS datetime), 'HH:mm:ss'), '-') AS Jam, "
            SQL = SQL & "ISNULL(z.Userid, '-') as UserID "
            SQL = SQL & "from N_EMI_Military_Sampling z "
            SQL = SQL & "where z.Kode_Perusahaan = a.kode_perusahaan "
            SQL = SQL & "and z.Status is null "
            SQL = SQL & "and z.No_Split = a.no_production_order "
            SQL = SQL & "and z.No_Batch = d.tahap "
            SQL = SQL & "and z.No_GR = '1' "
            SQL = SQL & "order by z.Tahap_Military_Sampling DESC, z.Tanggal DESC, z.Jam DESC "
            SQL = SQL & ") Military_Sampling_1 "

            SQL = SQL & "OUTER APPLY ( select top 1 'COMPLETED' as Status, "
            SQL = SQL & "ISNULL(FORMAT(z.Tanggal, 'dd MMM yyyy'), '-') AS Tanggal,  "
            SQL = SQL & "ISNULL(FORMAT(CAST(z.jam AS datetime), 'HH:mm:ss'), '-') AS Jam, "
            SQL = SQL & "ISNULL(z.Userid, '-') as UserID "
            SQL = SQL & "from Emi_Production_Results_Validation z, Emi_Production_Results_Validation_Detail x "
            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and z.No_Transaksi = x.No_Transaksi "
            SQL = SQL & "and z.Status is null "
            SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and z.No_Production_Order = a.no_production_order "
            SQL = SQL & "and x.Tahap = d.Tahap "
            SQL = SQL & "order by z.Tanggal DESC, z.Jam DESC, x.Nomor DESC "
            SQL = SQL & ") GR_2 "

            SQL = SQL & "OUTER APPLY ( "
            SQL = SQL & "select top 1 case when z.Flag_Military_Sampling = 'Y' and z.Flag_Ready_For_Packaging = 'Y' then 'READY FOR PACKAGING' "
            SQL = SQL & "when z.Flag_Military_Sampling = 'Y' and z.Flag_Ready_For_Packaging is null then 'HOLD' "
            SQL = SQL & "when z.Flag_Military_Sampling is null and z.Flag_Ready_For_Packaging is null then 'UNTEST' "
            SQL = SQL & "else 'UNDEFINED' "
            SQL = SQL & "end as Status, "
            SQL = SQL & "ISNULL(FORMAT(z.Tanggal, 'dd MMM yyyy'), '-') AS Tanggal, "
            SQL = SQL & "ISNULL(FORMAT(CAST(z.jam AS datetime), 'HH:mm:ss'), '-') AS Jam, "
            SQL = SQL & "ISNULL(z.Userid, '-') as UserID "
            SQL = SQL & "from N_EMI_Military_Sampling z "
            SQL = SQL & "where z.Kode_Perusahaan = a.kode_perusahaan "
            SQL = SQL & "and z.Status is null "
            SQL = SQL & "and z.No_Split = a.no_production_order "
            SQL = SQL & "and z.No_Batch = d.tahap "
            SQL = SQL & "and z.No_GR = '2' "
            SQL = SQL & "order by z.Tahap_Military_Sampling DESC, z.Tanggal DESC, z.Jam DESC "
            SQL = SQL & ") Military_Sampling_2 "

            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Status is null and b.Status is null and c.Status is null "
            'SQL = SQL & "and a.No_Production_Order = 'PR0825-00007-1' "
            If Filter Then
                SQL = SQL & "and a.Tanggal between '" & Format(Tgl_Prod_1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl_Prod_2.Value, "yyyy-MM-dd") & "' "
                If Tab2_Cmb_Step.SelectedIndex > 0 Then

                    SQL = SQL & arr_tab_2_jenis(Tab2_Cmb_Step.SelectedIndex) & " "
                End If
            End If
            SQL = SQL & "order by a.No_Production_Order, Tanggal, Jam, d.Tahap "
            Using DS = BindingTrans(SQL)
                With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Tab2_Dgv_Data_Global.Rows.Add(1)
                            Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_NoSplit).Value = .Rows(i).Item("No_Production_Order")
                            Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Tanggal).Value = Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy")
                            Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Jam).Value = .Rows(i).Item("Jam")
                            Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_UserID).Value = .Rows(i).Item("UserID")
                            Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Batch).Value = Format(.Rows(i).Item("Batch"), "N0")
                            Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Hari_Sampling).Value = Format(.Rows(i).Item("jumlah_hari"), "N0")
                            Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Nama_Barang).Value = .Rows(i).Item("Nama_Barang")
                            Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Qty_Produksi).Value = Format(.Rows(i).Item("Qty_Hasil_Produksi"), "N0")
                            Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Satuan).Value = .Rows(i).Item("Satuan")

                            Dim Status_Lab As String = ""
                            If Not .Rows(i).Item("Lab_Status") = "T" Then
                                Status_Lab = "Status : " & .Rows(i).Item("Lab_Status") & vbCrLf &
                                                       "Tanggal : " & .Rows(i).Item("Lab_Tanggal") & vbCrLf &
                                                       "User : " & .Rows(i).Item("Lab_UserID")

                                If .Rows(i).Item("Lab_Status").ToString.ToUpper = "VALIDATED" Then
                                    Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Hasil_Lab).Style.BackColor = Color.LightGreen

                                ElseIf .Rows(i).Item("Lab_Status").ToString.ToUpper = "ON PROCESS" Then
                                    Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Hasil_Lab).Style.BackColor = Color.LightYellow

                                Else
                                    Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Hasil_Lab).Style.BackColor = Color.LightGray

                                End If

                            Else
                                Status_Lab = "Status : - " & vbCrLf &
                                                     "Tanggal : - " & vbCrLf &
                                                     "User : -"
                                Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Hasil_Lab).Style.BackColor = Color.White
                            End If
                            Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Hasil_Lab).Value = Status_Lab

                            Dim Status_Military_1 As String = ""
                            If Not .Rows(i).Item("Military_Sampling_1_Status") = "T" Then
                                Status_Military_1 = "Status : " & .Rows(i).Item("Military_Sampling_1_Status") & vbCrLf &
                                                       "Tanggal : " & .Rows(i).Item("Military_Sampling_1_Tanggal") & vbCrLf &
                                                       "User : " & .Rows(i).Item("Military_Sampling_1_UserID")

                                If .Rows(i).Item("Military_Sampling_1_Status").ToString.ToUpper = "READY FOR PACKAGING" Then
                                    Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Military_Sampling_1).Style.BackColor = Color.LightGreen

                                ElseIf .Rows(i).Item("Military_Sampling_1_Status").ToString.ToUpper = "HOLD" Then
                                    Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Military_Sampling_1).Style.BackColor = Color.LightYellow

                                Else
                                    Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Military_Sampling_1).Style.BackColor = Color.LightGray
                                End If

                            Else
                                Status_Military_1 = "Status : - " & vbCrLf &
                                                      "Tanggal : - " & vbCrLf &
                                                      "User : - "
                                Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Military_Sampling_1).Style.BackColor = Color.White
                            End If
                            Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Military_Sampling_1).Value = Status_Military_1

                            Dim GR2 As String = ""
                            If Not .Rows(i).Item("GR_2_Status") = "T" Then
                                GR2 = "Status : " & .Rows(i).Item("GR_2_Status") & vbCrLf &
                                                       "Tanggal : " & .Rows(i).Item("GR_2_Tanggal") & vbCrLf &
                                                       "User : " & .Rows(i).Item("GR_2_UserID")

                                If .Rows(i).Item("GR_2_Status").ToString.ToUpper = "COMPLETED" Then
                                    Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Penerimaan_Barang).Style.BackColor = Color.LightGreen

                                Else
                                    Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Penerimaan_Barang).Style.BackColor = Color.LightGray
                                End If


                            Else
                                GR2 = "Status : - " & vbCrLf &
                                        "Tanggal : - " & vbCrLf &
                                        "User : - "

                                Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Penerimaan_Barang).Style.BackColor = Color.White
                            End If
                            Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Penerimaan_Barang).Value = GR2

                            Dim Status_Military_2 As String = ""
                            If Not .Rows(i).Item("Military_Sampling_2_Status") = "T" Then
                                Status_Military_2 = "Status : " & .Rows(i).Item("Military_Sampling_2_Status") & vbCrLf &
                                                        "Tanggal : " & .Rows(i).Item("Military_Sampling_2_Tanggal") & vbCrLf &
                                                        "User : " & .Rows(i).Item("Military_Sampling_2_UserID")


                                If .Rows(i).Item("Military_Sampling_2_Status").ToString.ToUpper = "READY FOR PACKAGING" Then
                                    Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Military_Sampling_2).Style.BackColor = Color.LightGreen

                                ElseIf .Rows(i).Item("Military_Sampling_2_Status").ToString.ToUpper = "HOLD" Then
                                    Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Military_Sampling_2).Style.BackColor = Color.LightYellow
                                Else
                                    Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Military_Sampling_2).Style.BackColor = Color.LightGray
                                End If

                            Else
                                Status_Military_2 = "Status - : " & vbCrLf &
                                                       "Tanggal : - " & vbCrLf &
                                                       "User : - "
                                Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Military_Sampling_2).Style.BackColor = Color.White
                            End If
                            Tab2_Dgv_Data_Global.Rows(i).Cells(cell_global_Military_Sampling_2).Value = Status_Military_2

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


End Class