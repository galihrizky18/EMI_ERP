Imports excel = Microsoft.Office.Interop.Excel

Public Class N_EMI_Laporan_Final_GI_GR

    Dim judulForm As String = "Display dan Laporan Final GI GR"

    Dim LvDetail_No_PO, LvDetail_No_Split, LvDetail_Tgl_Produksi, LvDetail_Jam_Produksi, LvDetail_Routing, LvDetail_Keterangan,
        LvDetail_Kd_Barang, LvDetail_Nm_Barang, LvDetail_Jumlah, LvDetail_Satuan, LvDetail_Batch, LvDetail_Berat_KG, LvDetail_GI_KG,
        LvDetail_GR_1_PCS, LvDetail_GR_1_KG, LvDetail_Scrap_1_KG, LvDetail_Total_1_KG, LvDetail_Loss_1_KG, LvDetail_Loss_1_Persen,
        LvDetail_Waste_1_Persen, LvDetail_Time_1_Day, LvDetail_GR_2_PCS, LvDetail_GR_2_KG, LvDetail_Scrap_2_KG, LvDetail_Total_2_KG,
        LvDetail_Wate_2_Persen, LvDetail_Time_2_Day, LvDetail_GR_Reject_PCS, LvDetail_GR_Reject_KG, LvDetail_Waste_Reject_Persen,
        LvDetail_Time_Reject_Day, LvDetail_GR_Final_PCS, LvDetail_GR_Final_KG, LvDetail_Scrap_Final_KG, LvDetail_Loss_Final_KG,
        LvDetail_Loss_Final_Persen, LvDetail_Waste_Final_Persen As String

    Dim LvRekap_No_PO, LvRekap_No_Split, LvRekap_Tgl_Produksi, LvRekap_Jam_Produksi, LvRekap_Routing, LvRekap_Keterangan, LvRekap_Kd_Barang,
        LvRekap_Nm_Barang, LvRekap_Jumlah, LvRekap_Satuan, LvRekap_Berat_KG, LvRekap_GI_KG, LvRekap_GR_KG, LvRekap_Waste_KG, LvRekap_waste_Persen,
        LvRekap_Loss_KG, LvRekap_Loss_Persen As String



    Dim itemDetail_No_PO As Integer = 0
    Dim itemDetail_No_Split As Integer = 1
    Dim itemDetail_Tgl_Produksi As Integer = 2
    Dim itemDetail_Jam_Produksi As Integer = 3
    Dim itemDetail_Routing As Integer = 4
    Dim itemDetail_Keterangan As Integer = 5
    Dim itemDetail_Kd_Barang As Integer = 6
    Dim itemDetail_Nm_Barang As Integer = 7
    Dim itemDetail_Jumlah As Integer = 8
    Dim itemDetail_Satuan As Integer = 9
    Dim itemDetail_Batch As Integer = 10
    Dim itemDetail_Berat_KG As Integer = 11
    Dim itemDetail_GI_KG As Integer = 12
    Dim itemDetail_GR_1_PCS As Integer = 13
    Dim itemDetail_GR_1_KG As Integer = 14
    Dim itemDetail_Scrap_1_KG As Integer = 15
    Dim itemDetail_Total_1_KG As Integer = 16
    Dim itemDetail_Loss_1_KG As Integer = 17
    Dim itemDetail_Loss_1_Persen As Integer = 18
    Dim itemDetail_Waste_1_Persen As Integer = 19
    Dim itemDetail_Time_1_Day As Integer = 20
    Dim itemDetail_GR_2_PCS As Integer = 21
    Dim itemDetail_GR_2_KG As Integer = 22
    Dim itemDetail_Scrap_2_KG As Integer = 23
    Dim itemDetail_Total_2_KG As Integer = 24
    Dim itemDetail_Wate_2_Persen As Integer = 25
    Dim itemDetail_Time_2_Day As Integer = 26
    Dim itemDetail_GR_Reject_PCS As Integer = 27
    Dim itemDetail_GR_Reject_KG As Integer = 28
    Dim itemDetail_Waste_Reject_Persen As Integer = 29
    Dim itemDetail_Time_Reject_Day As Integer = 30
    Dim itemDetail_GR_Final_PCS As Integer = 31
    Dim itemDetail_GR_Final_KG As Integer = 32
    Dim itemDetail_Scrap_Final_KG As Integer = 33
    Dim itemDetail_Loss_Final_KG As Integer = 34
    Dim itemDetail_Loss_Final_Persen As Integer = 35
    Dim itemDetail_Waste_Final_Persen As Integer = 36
    Dim itemDetail_Tanggal_GI As Integer = 37
    Dim itemDetail_Jam_GI As Integer = 38



    Dim itemRekap_No_PO As Integer = 0
    Dim itemRekap_No_Split As Integer = 1
    Dim itemRekap_Tgl_Produksi As Integer = 2
    Dim itemRekap_Jam_Produksi As Integer = 3
    Dim itemRekap_Routing As Integer = 4
    Dim itemRekap_Keterangan As Integer = 5
    Dim itemRekap_Kd_Barang As Integer = 6
    Dim itemRekap_Nm_Barang As Integer = 7
    Dim itemRekap_Jumlah As Integer = 8
    Dim itemRekap_Satuan As Integer = 9
    Dim itemRekap_Berat_KG As Integer = 10
    Dim itemRekap_GI_KG As Integer = 11
    Dim itemRekap_GR_1_KG As Integer = 12
    Dim itemRekap_Waste_1_KG As Integer = 13
    Dim itemRekap_GR_2_KG As Integer = 14
    Dim itemRekap_Waste_2_KG As Integer = 15
    Dim itemRekap_Waste_3_KG As Integer = 16
    Dim itemRekap_GR_KG As Integer = 17
    Dim itemRekap_Waste_KG As Integer = 18
    Dim itemRekap_waste_Persen As Integer = 19
    Dim itemRekap_Loss_KG As Integer = 20
    Dim itemRekap_Loss_Persen As Integer = 21
    Dim itemRekap_Tanggal_GI As Integer = 22
    Dim itemRekap_Jam_GI As Integer = 23


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


        'Dgv_Detail.Columns.Clear() : Dgv_Detail.rows.clear()
        'Dgv_Detail.Columns.Add("No PO", 120, HorizontalAlignment.Left) '0
        'Dgv_Detail.Columns.Add("No Split", 120, HorizontalAlignment.Left) '1
        'Dgv_Detail.Columns.Add("Tanggal Produksi", 110, HorizontalAlignment.Center) '2
        'Dgv_Detail.Columns.Add("Jam Produksi", 100, HorizontalAlignment.Center) '3
        'Dgv_Detail.Columns.Add("Routing", 150, HorizontalAlignment.Left) '4
        'Dgv_Detail.Columns.Add("Keterangan", 200, HorizontalAlignment.Left) '5
        'Dgv_Detail.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left) '6
        'Dgv_Detail.Columns.Add("Nama Barang", 200, HorizontalAlignment.Left) '7
        'Dgv_Detail.Columns.Add("Jumlah", 130, HorizontalAlignment.Right) '8
        'Dgv_Detail.Columns.Add("Satuan", 90, HorizontalAlignment.Center) '9
        'Dgv_Detail.Columns.Add("Batch", 70, HorizontalAlignment.Center) '10
        'Dgv_Detail.Columns.Add("Berat (KG)", 130, HorizontalAlignment.Right) '11
        'Dgv_Detail.Columns.Add("Good Issue (KG)", 130, HorizontalAlignment.Right) '12
        'Dgv_Detail.Columns.Add("GR Lv 1 (PCS)", 130, HorizontalAlignment.Right) '13
        'Dgv_Detail.Columns.Add("GR Lv 1 (KG)", 130, HorizontalAlignment.Right) '14
        'Dgv_Detail.Columns.Add("Scrap Lv 1 (KG)", 130, HorizontalAlignment.Right) '15
        'Dgv_Detail.Columns.Add("Total Lv 1 (KG)", 130, HorizontalAlignment.Right) '16
        'Dgv_Detail.Columns.Add("Loss Lv 1 (KG)", 130, HorizontalAlignment.Right) '17
        'Dgv_Detail.Columns.Add("Loss Lv 1 (%)", 130, HorizontalAlignment.Right) '18
        'Dgv_Detail.Columns.Add("Waste Lv 1 (%)", 130, HorizontalAlignment.Right) '19
        'Dgv_Detail.Columns.Add("Time Lv 1 (Day)", 100, HorizontalAlignment.Center) '20
        'Dgv_Detail.Columns.Add("GR Lv 2 (PCS)", 130, HorizontalAlignment.Right) '21
        'Dgv_Detail.Columns.Add("GR Lv 2 (KG)", 130, HorizontalAlignment.Right) '22
        'Dgv_Detail.Columns.Add("Scrap Lv 2 (KG)", 130, HorizontalAlignment.Right) '23
        'Dgv_Detail.Columns.Add("Total Lv 2 (KG)", 130, HorizontalAlignment.Right) '24
        'Dgv_Detail.Columns.Add("Waste Lv 2 (%)", 130, HorizontalAlignment.Right) '25
        'Dgv_Detail.Columns.Add("Time Lv 2 (Day)", 100, HorizontalAlignment.Center) '26
        'Dgv_Detail.Columns.Add("GR Rejected (PCS)", 130, HorizontalAlignment.Right) '27
        'Dgv_Detail.Columns.Add("GR Rejected (KG)", 130, HorizontalAlignment.Right) '28
        'Dgv_Detail.Columns.Add("Waste Rejected (%)", 130, HorizontalAlignment.Right) '29
        'Dgv_Detail.Columns.Add("Time Rejected (Day)", 100, HorizontalAlignment.Center) '30
        'Dgv_Detail.Columns.Add("GR Final (PCS)", 130, HorizontalAlignment.Right) '31
        'Dgv_Detail.Columns.Add("GR Final (KG)", 130, HorizontalAlignment.Right) '32
        'Dgv_Detail.Columns.Add("Scrap Final (KG)", 130, HorizontalAlignment.Right) '33
        'Dgv_Detail.Columns.Add("Loss Final (KG)", 130, HorizontalAlignment.Right) '34
        'Dgv_Detail.Columns.Add("Loss Final (%)", 130, HorizontalAlignment.Right) '35
        'Dgv_Detail.Columns.Add("Waste Final (&)", 130, HorizontalAlignment.Right) '36
        'Dgv_Detail.View = View.Details


        'Dgv_Rekap.Columns.Clear() : Dgv_Rekap.Rows.Clear()
        'Dgv_Rekap.Columns.Add("No PO", 120, HorizontalAlignment.Left) '0
        'Dgv_Rekap.Columns.Add("No Split", 120, HorizontalAlignment.Left) '1
        'Dgv_Rekap.Columns.Add("Tanggal Produksi", 110, HorizontalAlignment.Center) '2
        'Dgv_Rekap.Columns.Add("Jam Produksi", 100, HorizontalAlignment.Center) '3
        'Dgv_Rekap.Columns.Add("Routing", 150, HorizontalAlignment.Left) '4
        'Dgv_Rekap.Columns.Add("Keterangan", 200, HorizontalAlignment.Left) '5
        'Dgv_Rekap.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left) '6
        'Dgv_Rekap.Columns.Add("Nama Barang", 200, HorizontalAlignment.Left) '7
        'Dgv_Rekap.Columns.Add("Jumlah", 130, HorizontalAlignment.Right) '8
        'Dgv_Rekap.Columns.Add("Satuan", 90, HorizontalAlignment.Center) '9
        'Dgv_Rekap.Columns.Add("Berat (KG)", 130, HorizontalAlignment.Right) '10
        'Dgv_Rekap.Columns.Add("Good Issue (KG)", 130, HorizontalAlignment.Right) '11
        'Dgv_Rekap.Columns.Add("Total Lv 1 (KG)", 130, HorizontalAlignment.Right) '12
        'Dgv_Rekap.Columns.Add("Loss Lv 1 (KG)", 130, HorizontalAlignment.Right) '13
        'Dgv_Rekap.Columns.Add("Loss Lv 1 (%)", 130, HorizontalAlignment.Right) '14
        'Dgv_Rekap.Columns.Add("Waste Lv 1 (%)", 130, HorizontalAlignment.Right) '15
        'Dgv_Rekap.Columns.Add("Total Lv 2 (KG)", 130, HorizontalAlignment.Right) '16
        'Dgv_Rekap.Columns.Add("Waste Lv 2 (%)", 130, HorizontalAlignment.Right) '17
        'Dgv_Rekap.Columns.Add("GR Rejected (KG)", 130, HorizontalAlignment.Right) '18
        'Dgv_Rekap.Columns.Add("Waste Rejected (%)", 130, HorizontalAlignment.Right) '19
        'Dgv_Rekap.Columns.Add("GR Final (KG)", 130, HorizontalAlignment.Right) '20
        'Dgv_Rekap.Columns.Add("Waste Final (&)", 130, HorizontalAlignment.Right) '21
        'Dgv_Rekap.View = View.Details



        Lv_Routing.Columns.Clear()
        Lv_Routing.Columns.Add("Id Routing", 150, HorizontalAlignment.Center)
        Lv_Routing.Columns.Add("Nama Routing", 260, HorizontalAlignment.Center)
        Lv_Routing.View = View.Details

        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Nama Barang", 260, HorizontalAlignment.Center)
        Lv_Barang.View = View.Details



        Dgv_Rekap.Columns(22).DisplayIndex = 4
        Dgv_Rekap.Columns(23).DisplayIndex = 5

        Dgv_Detail.Columns(itemDetail_Tanggal_GI).DisplayIndex = 4
        Dgv_Detail.Columns(itemDetail_Jam_GI).DisplayIndex = 5

        Kosong()

    End Sub

    Private Sub Kosong()

        Tgl1.Value = DateTime.Today : Tgl2.Value = DateTime.Today
        Txt_IdRouting.Text = OpsiSeluruh : Txt_NmRouting.Text = OpsiSeluruh
        Txt_KdBarang.Text = OpsiSeluruh : Txt_NmBarang.Text = OpsiSeluruh
        Txt_NoSplit.Text = OpsiSeluruh

        Lv_Routing.Visible = False : Lv_Barang.Visible = False

        LoadRekap()
        LoadDataDetail()

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub


    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Txt_IdRouting.Text.Trim.Length = 0 Then
            MessageBox.Show("Routing harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_IdRouting.Focus() : Exit Sub
        ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarang.Focus() : Exit Sub
        End If

        LoadRekap(True)
        LoadDataDetail(True)

    End Sub

    Private Sub LoadRekap(Optional ByVal isFilter As Boolean = False)
        Try
            OpenConn()

            Dgv_Rekap.Rows.Clear()
            SQL = "select No_PO, No_Split, Tgl_Produksi, Jam_Produksi, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, Satuan, Berat_GI, Jumlah_Dosing, "
            SQL = SQL & "Satuan_Dosing, NilaiGRFinal_KG, ScrapGRFinal_KG, Loss_Production_Final_GR, Loss_Production_Final_GR_Persen, Total_Waste, "
            SQL = SQL & "NilaiGR1_KG, ScrapGR1_KG, NilaiGR2_KG, ScrapGR2_KG, ScrapAfterGR_KG, Tanggal_GI, Jam_GI "
            SQL = SQL & "from Laporan_Akhir_GIGR_Rekap "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Tgl_Produksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value.AddDays(1), "yyyy-MM-dd") & "' "

            If isFilter Then

                If Not Txt_IdRouting.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Id_Routing = '" & Txt_IdRouting.Text & "' "
                End If

                If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
                End If

                If Txt_NoSplit.Text.Trim.Length > 0 AndAlso Txt_NoSplit.Text.ToUpper <> OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and no_split like '%" & Txt_NoSplit.Text & "%' "
                End If


            End If

            SQL = SQL & "order by no_split, Tgl_Produksi, Jam_Produksi "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dgv_Rekap.Rows.Add(1)
                            Dgv_Rekap.Rows(i).Cells(itemRekap_No_PO).Value = .Rows(i)("No_PO") '0
                            Dgv_Rekap.Rows(i).Cells(itemRekap_No_Split).Value = .Rows(i)("No_Split") '1
                            Dgv_Rekap.Rows(i).Cells(itemRekap_Tgl_Produksi).Value = Format(.Rows(i)("Tgl_Produksi"), "dd MMM yyyy") '2
                            Dgv_Rekap.Rows(i).Cells(itemRekap_Jam_Produksi).Value = .Rows(i)("Jam_Produksi") '3
                            Dgv_Rekap.Rows(i).Cells(itemRekap_Routing).Value = .Rows(i)("Nama_Routing") '4
                            Dgv_Rekap.Rows(i).Cells(itemRekap_Keterangan).Value = .Rows(i)("Keterangan") '5
                            Dgv_Rekap.Rows(i).Cells(itemRekap_Kd_Barang).Value = .Rows(i)("Kode_Barang") '6
                            Dgv_Rekap.Rows(i).Cells(itemRekap_Nm_Barang).Value = .Rows(i)("Nama") '7
                            Dgv_Rekap.Rows(i).Cells(itemRekap_Jumlah).Value = Format(.Rows(i)("Jumlah"), "N4") '8
                            Dgv_Rekap.Rows(i).Cells(itemRekap_Satuan).Value = .Rows(i)("Satuan") '9
                            Dgv_Rekap.Rows(i).Cells(itemRekap_Berat_KG).Value = Format(.Rows(i)("Berat_GI"), "N0") '10
                            Dgv_Rekap.Rows(i).Cells(itemRekap_GI_KG).Value = Format(.Rows(i)("Jumlah_Dosing"), "N4") '11

                            Dgv_Rekap.Rows(i).Cells(itemRekap_GR_1_KG).Value = Format(.Rows(i)("NilaiGR1_KG"), "N4") '12
                            Dgv_Rekap.Rows(i).Cells(itemRekap_Waste_1_KG).Value = Format(.Rows(i)("ScrapGR1_KG"), "N4") '12
                            Dgv_Rekap.Rows(i).Cells(itemRekap_GR_2_KG).Value = Format(.Rows(i)("NilaiGR2_KG"), "N4") '13
                            Dgv_Rekap.Rows(i).Cells(itemRekap_Waste_2_KG).Value = Format(.Rows(i)("ScrapGR2_KG"), "N4") '14
                            Dgv_Rekap.Rows(i).Cells(itemRekap_Waste_3_KG).Value = Format(.Rows(i)("ScrapAfterGR_KG"), "N4") '16

                            Dgv_Rekap.Rows(i).Cells(itemRekap_GR_KG).Value = Format(.Rows(i)("NilaiGRFinal_KG"), "N4") '17
                            Dgv_Rekap.Rows(i).Cells(itemRekap_Waste_KG).Value = Format(.Rows(i)("ScrapGRFinal_KG"), "N4") '18
                            Dgv_Rekap.Rows(i).Cells(itemRekap_waste_Persen).Value = Format(.Rows(i)("Total_Waste"), "N4") '19
                            Dgv_Rekap.Rows(i).Cells(itemRekap_Loss_KG).Value = Format(.Rows(i)("Loss_Production_Final_GR"), "N4") '20
                            Dgv_Rekap.Rows(i).Cells(itemRekap_Loss_Persen).Value = Format(.Rows(i)("Loss_Production_Final_GR_Persen"), "N4") '21

                            Dgv_Rekap.Rows(i).Cells(itemRekap_Tanggal_GI).Value = Format(.Rows(i)("Tanggal_GI"), "dd MMM yyyy") '22
                            Dgv_Rekap.Rows(i).Cells(itemRekap_Jam_GI).Value = .Rows(i)("Jam_GI") '23


                            '== WARNA =='

                            Dgv_Rekap.Rows(i).Cells(itemRekap_GR_1_KG).Style.BackColor = Color.LightYellow
                            Dgv_Rekap.Rows(i).Cells(itemRekap_Waste_1_KG).Style.BackColor = Color.LightYellow

                            Dgv_Rekap.Rows(i).Cells(itemRekap_GR_2_KG).Style.BackColor = Color.LightBlue
                            Dgv_Rekap.Rows(i).Cells(itemRekap_Waste_2_KG).Style.BackColor = Color.LightBlue

                            Dgv_Rekap.Rows(i).Cells(itemRekap_Waste_3_KG).Style.BackColor = Color.LightPink

                            Dgv_Rekap.Rows(i).Cells(itemRekap_GR_KG).Style.BackColor = Color.LightGreen


                            Dgv_Rekap.Rows(i).Cells(itemRekap_Waste_KG).Style.BackColor = Color.LightGray
                            Dgv_Rekap.Rows(i).Cells(itemRekap_waste_Persen).Style.BackColor = Color.LightGray

                            Dgv_Rekap.Rows(i).Cells(itemRekap_Loss_KG).Style.BackColor = Color.LightCyan
                            Dgv_Rekap.Rows(i).Cells(itemRekap_Loss_Persen).Style.BackColor = Color.LightCyan




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


    Private Sub LoadDataDetail(Optional ByVal isFilter As Boolean = False)

        Try
            OpenConn()

            Dgv_Detail.Rows.Clear()
            SQL = "select No_PO, no_split, Tgl_Produksi, Jam_Produksi, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, satuan, batch, "
            SQL = SQL & "Berat_GI, Jumlah_Dosing, NilaiGR1_Pcs, NilaiGR1_KG, ScrapGR1_KG, TotalGR1_KG, Loss_Production, Loss_Production_Persen, Persen_WasteGR1, WaktuGR1, "
            SQL = SQL & "NilaiGR2_Pcs, NilaiGR2_KG, ScrapGR2_KG, TotalGR2_KG, Persen_WasteGR2, WaktuGR2, NilaiAfterGR_Pcs, NilaiAfterGR_KG, Persen_WasteGR3, WaktuGR3, "
            SQL = SQL & "NilaiGRFinal_Pcs, NilaiGRFinal_KG, ScrapGRFinal_KG, Loss_Production_Final_GR, Loss_Production_Final_GR_Persen, Total_Waste, Tanggal_GI, Jam_GI "
            SQL = SQL & "from Laporan_Akhir_GIGR "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Tgl_Produksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value.AddDays(1), "yyyy-MM-dd") & "' "

            If isFilter Then

                If Not Txt_IdRouting.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Id_Routing = '" & Txt_IdRouting.Text & "' "
                End If

                If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
                End If

                If Txt_NoSplit.Text.Trim.Length > 0 AndAlso Txt_NoSplit.Text.ToUpper <> OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and no_split like '%" & Txt_NoSplit.Text & "%' "
                End If


            End If

            SQL = SQL & "order by no_split, Tgl_Produksi, Jam_Produksi "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dgv_Detail.Rows.Add(1)
                            Dgv_Detail.Rows(i).Cells(itemDetail_No_PO).Value = .Rows(i)("No_PO") '0
                            Dgv_Detail.Rows(i).Cells(itemDetail_No_Split).Value = .Rows(i)("No_Split") '1
                            Dgv_Detail.Rows(i).Cells(itemDetail_Tgl_Produksi).Value = Format(.Rows(i)("Tgl_Produksi"), "dd MMM yyyy") '2
                            Dgv_Detail.Rows(i).Cells(itemDetail_Jam_Produksi).Value = .Rows(i)("Jam_Produksi") '3
                            Dgv_Detail.Rows(i).Cells(itemDetail_Routing).Value = .Rows(i)("Nama_Routing") '4
                            Dgv_Detail.Rows(i).Cells(itemDetail_Keterangan).Value = .Rows(i)("Keterangan") '5
                            Dgv_Detail.Rows(i).Cells(itemDetail_Kd_Barang).Value = .Rows(i)("Kode_Barang") '6
                            Dgv_Detail.Rows(i).Cells(itemDetail_Nm_Barang).Value = .Rows(i)("Nama") '7
                            Dgv_Detail.Rows(i).Cells(itemDetail_Jumlah).Value = Format(.Rows(i)("Jumlah"), "N4") '8
                            Dgv_Detail.Rows(i).Cells(itemDetail_Satuan).Value = .Rows(i)("satuan") '9
                            Dgv_Detail.Rows(i).Cells(itemDetail_Batch).Value = .Rows(i)("batch") '10
                            Dgv_Detail.Rows(i).Cells(itemDetail_Berat_KG).Value = Format(.Rows(i)("Berat_GI"), "N4") '11
                            Dgv_Detail.Rows(i).Cells(itemDetail_GI_KG).Value = Format(.Rows(i)("Jumlah_Dosing"), "N4") '12

                            Dgv_Detail.Rows(i).Cells(itemDetail_GR_1_PCS).Value = Format(.Rows(i)("NilaiGR1_Pcs"), "N4") '13
                            Dgv_Detail.Rows(i).Cells(itemDetail_GR_1_KG).Value = Format(.Rows(i)("NilaiGR1_KG"), "N4") '14
                            Dgv_Detail.Rows(i).Cells(itemDetail_Scrap_1_KG).Value = Format(.Rows(i)("ScrapGR1_KG"), "N4") '15
                            Dgv_Detail.Rows(i).Cells(itemDetail_Total_1_KG).Value = Format(.Rows(i)("TotalGR1_KG"), "N4") '16
                            Dgv_Detail.Rows(i).Cells(itemDetail_Loss_1_KG).Value = Format(.Rows(i)("Loss_Production"), "N4") '17
                            Dgv_Detail.Rows(i).Cells(itemDetail_Loss_1_Persen).Value = Format(.Rows(i)("Loss_Production_Persen"), "N4") '18
                            Dgv_Detail.Rows(i).Cells(itemDetail_Waste_1_Persen).Value = Format(.Rows(i)("Persen_WasteGR1"), "N4") '19
                            Dgv_Detail.Rows(i).Cells(itemDetail_Time_1_Day).Value = Format(.Rows(i)("WaktuGR1"), "N0") '20

                            Dgv_Detail.Rows(i).Cells(itemDetail_GR_2_PCS).Value = Format(.Rows(i)("NilaiGR2_Pcs"), "N4") '21
                            Dgv_Detail.Rows(i).Cells(itemDetail_GR_2_KG).Value = Format(.Rows(i)("NilaiGR2_KG"), "N4") '22
                            Dgv_Detail.Rows(i).Cells(itemDetail_Scrap_2_KG).Value = Format(.Rows(i)("ScrapGR2_KG"), "N4") '23
                            Dgv_Detail.Rows(i).Cells(itemDetail_Total_2_KG).Value = Format(.Rows(i)("TotalGR2_KG"), "N4") '24
                            Dgv_Detail.Rows(i).Cells(itemDetail_Wate_2_Persen).Value = Format(.Rows(i)("Persen_WasteGR2"), "N4") '25
                            Dgv_Detail.Rows(i).Cells(itemDetail_Time_2_Day).Value = Format(.Rows(i)("WaktuGR2"), "N0") '26

                            Dgv_Detail.Rows(i).Cells(itemDetail_GR_Reject_PCS).Value = Format(.Rows(i)("NilaiAfterGR_Pcs"), "N4") '27
                            Dgv_Detail.Rows(i).Cells(itemDetail_GR_Reject_KG).Value = Format(.Rows(i)("NilaiAfterGR_KG"), "N4") '28
                            Dgv_Detail.Rows(i).Cells(itemDetail_Waste_Reject_Persen).Value = Format(.Rows(i)("Persen_WasteGR3"), "N4") '29
                            Dgv_Detail.Rows(i).Cells(itemDetail_Time_Reject_Day).Value = Format(.Rows(i)("WaktuGR3"), "N0") '30

                            Dgv_Detail.Rows(i).Cells(itemDetail_GR_Final_PCS).Value = Format(.Rows(i)("NilaiGRFinal_Pcs"), "N4") '31
                            Dgv_Detail.Rows(i).Cells(itemDetail_GR_Final_KG).Value = Format(.Rows(i)("NilaiGRFinal_KG"), "N4") '32
                            Dgv_Detail.Rows(i).Cells(itemDetail_Scrap_Final_KG).Value = Format(.Rows(i)("ScrapGRFinal_KG"), "N4") '33
                            Dgv_Detail.Rows(i).Cells(itemDetail_Loss_Final_KG).Value = Format(.Rows(i)("Loss_Production_Final_GR"), "N4") '34
                            Dgv_Detail.Rows(i).Cells(itemDetail_Loss_Final_Persen).Value = Format(.Rows(i)("Loss_Production_Final_GR_Persen"), "N4") '35
                            Dgv_Detail.Rows(i).Cells(itemDetail_Waste_Final_Persen).Value = Format(.Rows(i)("Total_Waste"), "N4") '36

                            Dgv_Detail.Rows(i).Cells(itemDetail_Tanggal_GI).Value = Format(.Rows(i)("Tanggal_GI"), "dd MMM yyyy") '37
                            Dgv_Detail.Rows(i).Cells(itemDetail_Jam_GI).Value = .Rows(i)("Jam_GI")  '38


                            '== WARNA =='
                            Dgv_Detail.Rows(i).Cells(itemDetail_GR_1_PCS).Style.BackColor = Color.LightYellow
                            Dgv_Detail.Rows(i).Cells(itemDetail_GR_1_KG).Style.BackColor = Color.LightYellow
                            Dgv_Detail.Rows(i).Cells(itemDetail_Scrap_1_KG).Style.BackColor = Color.LightYellow
                            Dgv_Detail.Rows(i).Cells(itemDetail_Total_1_KG).Style.BackColor = Color.LightYellow
                            Dgv_Detail.Rows(i).Cells(itemDetail_Loss_1_KG).Style.BackColor = Color.LightYellow
                            Dgv_Detail.Rows(i).Cells(itemDetail_Loss_1_Persen).Style.BackColor = Color.LightYellow
                            Dgv_Detail.Rows(i).Cells(itemDetail_Waste_1_Persen).Style.BackColor = Color.LightYellow
                            Dgv_Detail.Rows(i).Cells(itemDetail_Time_1_Day).Style.BackColor = Color.FromArgb(252, 105, 108)

                            Dgv_Detail.Rows(i).Cells(itemDetail_GR_2_PCS).Style.BackColor = Color.LightBlue
                            Dgv_Detail.Rows(i).Cells(itemDetail_GR_2_KG).Style.BackColor = Color.LightBlue
                            Dgv_Detail.Rows(i).Cells(itemDetail_Scrap_2_KG).Style.BackColor = Color.LightBlue
                            Dgv_Detail.Rows(i).Cells(itemDetail_Total_2_KG).Style.BackColor = Color.LightBlue
                            Dgv_Detail.Rows(i).Cells(itemDetail_Wate_2_Persen).Style.BackColor = Color.LightBlue
                            Dgv_Detail.Rows(i).Cells(itemDetail_Time_2_Day).Style.BackColor = Color.FromArgb(181, 230, 162)

                            Dgv_Detail.Rows(i).Cells(itemDetail_GR_Reject_PCS).Style.BackColor = Color.LightGray
                            Dgv_Detail.Rows(i).Cells(itemDetail_GR_Reject_KG).Style.BackColor = Color.LightGray
                            Dgv_Detail.Rows(i).Cells(itemDetail_Waste_Reject_Persen).Style.BackColor = Color.LightGray
                            Dgv_Detail.Rows(i).Cells(itemDetail_Time_Reject_Day).Style.BackColor = Color.White

                            Dgv_Detail.Rows(i).Cells(itemDetail_GR_Final_PCS).Style.BackColor = Color.LightGreen
                            Dgv_Detail.Rows(i).Cells(itemDetail_GR_Final_KG).Style.BackColor = Color.LightGreen
                            Dgv_Detail.Rows(i).Cells(itemDetail_Scrap_Final_KG).Style.BackColor = Color.LightGreen
                            Dgv_Detail.Rows(i).Cells(itemDetail_Loss_Final_KG).Style.BackColor = Color.LightGreen
                            Dgv_Detail.Rows(i).Cells(itemDetail_Loss_Final_Persen).Style.BackColor = Color.LightGreen
                            Dgv_Detail.Rows(i).Cells(itemDetail_Waste_Final_Persen).Style.BackColor = Color.LightGreen


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



    '===============================================================================================================================================================================
    '=     HANDLE TEXT CHANGED 
    '===============================================================================================================================================================================
    Private Sub Txt_IdRouting_TextChanged(sender As Object, e As EventArgs) Handles Txt_IdRouting.TextChanged
        If Txt_IdRouting.Text.Trim.Length = 0 Then
            Lv_Routing.Location = New Point(1200, 137)
            Lv_Routing.Visible = False
            Txt_IdRouting.Text = ""
            Txt_NmRouting.Text = ""
            Exit Sub
        Else
            Lv_Routing.Visible = True
            Lv_Routing.Location = New Point(140, 137)
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
            Lv_Routing.Location = New Point(1200, 137)
            Lv_Routing.Visible = False
            Txt_IdRouting.Text = ""
            Txt_NmRouting.Text = ""
            Exit Sub
        Else
            Lv_Routing.Visible = True
            Lv_Routing.Location = New Point(140, 137)
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
            Lv_Barang.Location = New Point(1200, 166)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(140, 166)
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

    Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
        If Txt_NmBarang.Text.Trim.Length = 0 Then
            Lv_Barang.Location = New Point(1200, 166)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(140, 166)
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

                    Lv_Routing.Location = New Point(1200, 137)
                    Lv_Routing.Visible = False
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

                    Lv_Barang.Location = New Point(1200, 166)
                    Lv_Barang.Visible = False
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
    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub
    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then Txt_IdRouting.Focus()
    End Sub
    Private Sub Txt_IdRouting_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_IdRouting.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_IdRouting.Text.Trim.Length = 0 Then Txt_IdRouting.Focus()
            Txt_IdRouting_Leave(Txt_IdRouting, e)


            Lv_Routing.Location = New Point(1200, 137)
            Lv_Routing.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub
    Private Sub Txt_IdRouting_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_IdRouting.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Routing.Focus()
    End Sub
    Private Sub Txt_NmRouting_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmRouting.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_IdRouting_Leave(Txt_NmRouting, e)

            Lv_Routing.Location = New Point(1200, 137)
            Lv_Routing.Visible = False

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

            Lv_Barang.Location = New Point(1200, 166)
            Lv_Barang.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub
    Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub
    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdBarang_Leave(Txt_NmBarang, e)

            Lv_Barang.Location = New Point(1200, 166)
            Lv_Barang.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub
    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Routing.Focus()
    End Sub

    Private Sub Txt_NoSplit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NoSplit.KeyPress
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

        Lv_Routing.Location = New Point(1200, 137)
        Lv_Routing.Visible = False

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

        Lv_Barang.Location = New Point(1200, 166)
        Lv_Barang.Visible = False

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
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Txt_IdRouting.Text.Trim.Length = 0 Then
            MessageBox.Show("Routing harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_IdRouting.Focus() : Exit Sub
        ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarang.Focus() : Exit Sub
        End If

        If MessageBox.Show("Yakin Ingin Cetak Rekap Laporan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub


        Try
            OpenConn()

            Dim SF As String = ""

            SQL = "select No_PO, No_Split, Tgl_Produksi, Jam_Produksi, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, Satuan, Berat_GI, Jumlah_Dosing, "
            SQL = SQL & "NilaiGR1_KG, ScrapGR1_KG, NilaiGR2_KG, ScrapGR2_KG, ScrapAfterGR_KG, "
            SQL = SQL & "NilaiGRFinal_KG, ScrapGRFinal_KG, Total_Waste, Loss_Production_Final_GR, Loss_Production_Final_GR_Persen "
            SQL = SQL & "from Laporan_Akhir_GIGR_Rekap "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Tgl_Produksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

            SF = "{Laporan_Akhir_GIGR_Rekap.kode_perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {Laporan_Akhir_GIGR_Rekap.Tgl_Produksi} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
            SF = SF & "{Laporan_Akhir_GIGR_Rekap.Tgl_Produksi} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

            If Not Txt_IdRouting.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and Id_Routing = '" & Txt_IdRouting.Text & "' "
                SF = SF & "And {Laporan_Akhir_GIGR_Rekap.Id_Routing} = '" & Txt_IdRouting.Text & "'"
            End If

            If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
                SF = SF & "And {Laporan_Akhir_GIGR_Rekap.Kode_Barang} = '" & Txt_KdBarang.Text & "' "
            End If

            If Txt_NoSplit.Text.Trim.Length > 0 AndAlso Txt_NoSplit.Text.ToUpper <> OpsiSeluruh.ToUpper Then
                SQL = SQL & "and no_split like '%" & Txt_NoSplit.Text & "%' "
            End If

            SQL = SQL & "order by no_split, Tgl_Produksi, Jam_Produksi "
            Using DS = BindingTrans(SQL)
                With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        'Dim CrDoc As New Rpt_Laporan_Final_GI_GR

                        'CrDoc.SetDataSource(DS)
                        'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        'CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                        '                                                    Format(Tgl2.Value, "dd/MMM/yyyy")
                        'CrDoc.RecordSelectionFormula = SF

                        'With A_Place_For_Printing2
                        '    .Text = "Laporan Final GI GR"
                        '    .CrystalReportViewer1.ReportSource = CrDoc
                        '    .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                        '    .Refresh()
                        '    .Show()
                        'End With

                        Generate_Excel_Rekap(SQL)

                    Else

                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub

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

    Private Sub Btn_Cetak_Detail_Click(sender As Object, e As EventArgs) Handles Btn_Cetak_Detail.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Txt_IdRouting.Text.Trim.Length = 0 Then
            MessageBox.Show("Routing harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_IdRouting.Focus() : Exit Sub
        ElseIf Txt_KdBarang.Text.Trim.Length = 0 Then
            MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarang.Focus() : Exit Sub
        End If

        If MessageBox.Show("Yakin Ingin Cetak Detail Laporan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub


        Try
            OpenConn()

            Dim SF As String = ""

            SQL = "select No_PO, no_split, Tgl_Produksi, Jam_Produksi, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, satuan, batch, Berat_GI, Jumlah_Dosing, NilaiGR1_Pcs, NilaiGR1_KG, ScrapGR1_KG, TotalGR1_KG, Loss_Production, Loss_Production_Persen, Persen_WasteGR1, WaktuGR1, "
            SQL = SQL & "NilaiGR2_Pcs, NilaiGR2_KG, ScrapGR2_KG, TotalGR2_KG, Persen_WasteGR2, WaktuGR2, NilaiAfterGR_Pcs, NilaiAfterGR_KG, Persen_WasteGR3, WaktuGR3, NilaiGRFinal_Pcs, NilaiGRFinal_KG, ScrapGRFinal_KG, Loss_Production_Final_GR, Loss_Production_Final_GR_Persen, Total_Waste "
            SQL = SQL & "from Laporan_Akhir_GIGR "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Tgl_Produksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

            SF = "{Laporan_Akhir_GIGR.kode_perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {Laporan_Akhir_GIGR.Tgl_Produksi} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
            SF = SF & "{Laporan_Akhir_GIGR.Tgl_Produksi} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

            If Not Txt_IdRouting.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and Id_Routing = '" & Txt_IdRouting.Text & "' "
                SF = SF & "And {Laporan_Akhir_GIGR.Id_Routing} = '" & Txt_IdRouting.Text & "'"
            End If

            If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
                SF = SF & "And {Laporan_Akhir_GIGR.Kode_Barang} = '" & Txt_KdBarang.Text & "' "
            End If

            If Txt_NoSplit.Text.Trim.Length > 0 AndAlso Txt_NoSplit.Text.ToUpper <> OpsiSeluruh.ToUpper Then
                SQL = SQL & "and no_split like '%" & Txt_NoSplit.Text & "%' "
            End If

            SQL = SQL & "order by no_split, Tgl_Produksi, Jam_Produksi "
            Using DS = BindingTrans(SQL)
                With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        'Dim CrDoc As New Rpt_Laporan_Final_GI_GR

                        'CrDoc.SetDataSource(DS)
                        'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        'CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                        '                                                    Format(Tgl2.Value, "dd/MMM/yyyy")
                        'CrDoc.RecordSelectionFormula = SF

                        'With A_Place_For_Printing2
                        '    .Text = "Laporan Final GI GR"
                        '    .CrystalReportViewer1.ReportSource = CrDoc
                        '    .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                        '    .Refresh()
                        '    .Show()
                        'End With

                        Generate_Excel_Detail(SQL)

                    Else

                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub

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
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Batch"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Berat (Gram)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Good Issue (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Good Received (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}}, 'Mulai GR 1
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Good Received (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Scrap (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Total (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Loss (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Loss (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Time (Day)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(253, 255, 167))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Good Received (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}}, 'Mulai Val
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Good Received (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Scrap (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Total (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Time (Day)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(131, 197, 217))}},
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Good Received Rejected (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}}, 'Mulai Rejected
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Good Received Rejected (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
                New Dictionary(Of String, String) From {{"Identifier", "Rejected"}, {"Kolom", "Time (Day)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(174, 174, 174))}},
                New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Good Received (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}}, 'Mulai FINAL GR
                New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Good Received (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}},
                New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Scrap (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}},
                New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Loss (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}},
                New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Loss (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}},
                New Dictionary(Of String, String) From {{"Identifier", "Final"}, {"Kolom", "Waste (%)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(89, 229, 89))}}
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
                                    Case 13 To 20
                                        If .Columns(colIndex).ColumnName = "WaktuGR1" Then
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(252, 105, 108))
                                        Else
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow)
                                        End If
                                    Case 21 To 26
                                        If .Columns(colIndex).ColumnName = "WaktuGR2" Then
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(181, 230, 162))
                                        Else
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                                        End If
                                    Case 27 To 30
                                        If .Columns(colIndex).ColumnName = "WaktuGR3" Then
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
                                        Else
                                            cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
                                        End If
                                    Case 31 To 36
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
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Waste After Received Lv2 (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 151, 166))}},
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

End Class
