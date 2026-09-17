Imports excel = Microsoft.Office.Interop.Excel

Public Class N_EMI_Laporan_HPP_Produksi

    Dim judulForm As String = "Display dan Laporan HPP Produksi"
    Dim arrJenis As New ArrayList

    Dim arrRoleColumn As New List(Of String)

    Dim RoleColumn_GI As String = "Column_GI"
    Dim RoleColumn_BahanBaku As String = "Column_BahanBaku"
    Dim RoleColumn_Packaging As String = "Column_Packaging"
    Dim RoleColumn_CostProduction As String = "Column_CostProduction"
    Dim RoleColumn_HPP As String = "Column_HPP"
    Dim RoleColumn_Waste As String = "Column_Waste"
    Dim RoleColumn_HppSementara As String = "Column_HPPSementara"
    Dim RoleColumn_PackSekunder As String = "Column_PackSekunder"
    Dim RoleColumn_HPPFinal As String = "Column_HPPFinal"

    Dim RoleColumn_TotalHPP As String = "Column_TotalHPP"
    Dim RoleColumn_Scrap As String = "Column_Scrap"
    Dim RoleColumn_Loss As String = "Column_Loss"
    Dim RoleColumn_FG As String = "Column_FG"




    Dim LvRekap_No_PO, LvRekap_No_Split, LvRekap_Tgl_Produksi, LvRekap_Jam_Produksi, LvRekap_Tgl_PO, LvRekap_Jam_PO, LvRekap_Routing, LvRekap_Keterangan,
        LvRekap_Kd_Barang, LvRekap_Nm_Barang, LvRekap_Jumlah, LvRekap_Satuan, LvRekap_GI, LvRekap_Satuan_GI, LvRekap_Tot_Bahan_Baku, LvRekap_Biaya_Produksi, LvRekap_Budget_Nilai_Loss,
        LvRekap_Budget_Persen_Loss, LvRekap_Tot_Packaging_1, LvRekap_Tot_Packaging_2, LvRekap_GR, LvRekap_HPP_GR, LvRekap_Total_GR, LvRekap_RJ,
        LvRekap_HPP_RJ, LvRekap_Total_RJ, LvRekap_Scp, LvRekap_HPP_Scp, LvRekap_Total_Scp, LvRekap_GR_Final, LvRekap_HPP_Final As String


    'Dim LvDetail_No_PO, LvDetail_No_Split, LvDetail_Tgl_Produksi, LvDetail_Jam_Produksi, LvDetail_Routing, LvDetail_Keterangan,
    '    LvDetail_Kd_Barang, LvDetail_Nm_Barang, LvDetail_Jumlah, LvDetail_Satuan, LvDetail_Batch, LvDetail_Berat_KG, LvDetail_GI_KG,
    '    LvDetail_GR_1_PCS, LvDetail_GR_1_KG, LvDetail_Scrap_1_KG, LvDetail_Total_1_KG, LvDetail_Loss_1_KG, LvDetail_Loss_1_Persen,
    '    LvDetail_Waste_1_Persen, LvDetail_Time_1_Day, LvDetail_GR_2_PCS, LvDetail_GR_2_KG, LvDetail_Scrap_2_KG, LvDetail_Total_2_KG,
    '    LvDetail_Wate_2_Persen, LvDetail_Time_2_Day, LvDetail_GR_Reject_PCS, LvDetail_GR_Reject_KG, LvDetail_Waste_Reject_Persen,
    '    LvDetail_Time_Reject_Day, LvDetail_GR_Final_PCS, LvDetail_GR_Final_KG, LvDetail_Scrap_Final_KG, LvDetail_Loss_Final_KG,
    '    LvDetail_Loss_Final_Persen, LvDetail_Waste_Final_Persen As String

    Dim itemRekap_No_PO As Integer = 0
    Dim itemRekap_No_Split As Integer = 1
    Dim itemRekap_Tgl_Produksi As Integer = 2
    Dim itemRekap_Jam_Produksi As Integer = 3
    Dim itemRekap_Tgl_PO As Integer = 4
    Dim itemRekap_Jam_PO As Integer = 5
    Dim itemRekap_Routing As Integer = 6
    Dim itemRekap_Keterangan As Integer = 7
    Dim itemRekap_Kd_Barang As Integer = 8
    Dim itemRekap_Nm_Barang As Integer = 9
    Dim itemRekap_Jumlah As Integer = 10
    Dim itemRekap_Satuan As Integer = 11
    Dim itemRekap_GI As Integer = 12
    Dim itemRekap_Satuan_GI As Integer = 13
    Dim itemRekap_Biaya_Produksi As Integer = 14
    Dim itemRekap_Budget_Nilai_Loss As Integer = 15
    Dim itemRekap_Budget_Persen_Loss As Integer = 16
    Dim itemRekap_Tot_Bahan_Baku As Integer = 17
    Dim itemRekap_Tot_Packaging_1 As Integer = 18
    Dim itemRekap_Tot_Packaging_2 As Integer = 19
    Dim itemRekap_Total As Integer = 20
    Dim itemRekap_GR As Integer = 21
    Dim itemRekap_HPP_GR As Integer = 22
    Dim itemRekap_Total_GR As Integer = 23
    Dim itemRekap_RJ As Integer = 24
    Dim itemRekap_HPP_RJ As Integer = 25
    Dim itemRekap_Total_RJ As Integer = 26
    Dim itemRekap_Scp As Integer = 27
    Dim itemRekap_HPP_Scp As Integer = 28
    Dim itemRekap_Total_Scp As Integer = 29
    Dim itemRekap_GR_Final As Integer = 30
    Dim itemRekap_HPP_Final As Integer = 31


    Dim itemRekap2_No_PO As Integer = 0
    Dim itemRekap2_No_Split As Integer = 1
    Dim itemRekap2_Tgl_Produksi As Integer = 2
    Dim itemRekap2_Jam_Produksi As Integer = 3
    Dim itemRekap2_Tgl_PO As Integer = 4
    Dim itemRekap2_Jam_PO As Integer = 5
    Dim itemRekap2_Routing As Integer = 6
    Dim itemRekap2_Keterangan As Integer = 7
    Dim itemRekap2_Kd_Barang As Integer = 8
    Dim itemRekap2_Nm_Barang As Integer = 9
    Dim itemRekap2_Jumlah As Integer = 10
    Dim itemRekap2_Satuan As Integer = 11
    Dim itemRekap2_GI As Integer = 12
    Dim itemRekap2_Satuan_GI As Integer = 13
    Dim itemRekap2_Biaya_Produksi As Integer = 14
    Dim itemRekap2_Budget_Nilai_Loss As Integer = 15
    Dim itemRekap2_Budget_Persen_Loss As Integer = 16
    Dim itemRekap2_Tot_Bahan_Baku As Integer = 17
    Dim itemRekap2_Tot_Packaging As Integer = 18
    Dim itemRekap2_CostProduction As Integer = 19
    Dim itemRekap2_HPP As Integer = 20



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
        Lv_Routing.Columns.Add("Id Routing", 150, HorizontalAlignment.Center)
        Lv_Routing.Columns.Add("Nama Routing", 260, HorizontalAlignment.Center)
        Lv_Routing.View = View.Details

        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Nama Barang", 260, HorizontalAlignment.Center)
        Lv_Barang.View = View.Details

        arrRoleColumn.Clear()


        'Dgv_Total.Enabled = False
        'Dgv_Total.ColumnHeadersVisible = False
        'Dgv_Total.RowHeadersVisible = False

        Try
            OpenConn()

            Cmb_Jenis.Items.Clear() : arrJenis.Clear()
            Cmb_Jenis.Items.Add(OpsiSeluruh) : arrJenis.Add(OpsiSeluruh)
            SQL = "select Kode_Group_Jenis, "
            SQL = SQL & "case when Flag_Finished_Good = 'Y' then 'Barang Jadi' "
            SQL = SQL & "when Flag_Semi_FG = 'Y' then 'Barang Setengah Jadi' "
            SQL = SQL & "end as 'Keterangan', "
            SQL = SQL & "case when Flag_Finished_Good = 'Y' and Flag_Semi_FG ='T' then 'Y' "
            SQL = SQL & "when Flag_Semi_FG = 'Y' and Flag_Finished_Good = 'T' then 'T' "
            SQL = SQL & "end as 'Flag' "
            SQL = SQL & "from EMI_Group_Jenis where kode_perusahaan = '" & KodePerusahaan & "' and (Flag_Finished_Good = 'Y' or Flag_Semi_FG = 'Y') "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Jenis.Items.Add(dr("Keterangan")) : arrJenis.Add(dr("Flag"))
                Loop
            End Using
            Cmb_Jenis.SelectedIndex = 0

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Report_HPPProduksi_Column_GI") = "Y" Then
                arrRoleColumn.Add(RoleColumn_GI)
            Else
                Dgv_Rekap2.Columns(9).Visible = False
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Report_HPPProduksi_Column_Bahan_Baku") = "Y" Then
                arrRoleColumn.Add(RoleColumn_BahanBaku)
            Else
                Dgv_Rekap2.Columns(10).Visible = False
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Report_HPPProduksi_Column_Packaging") = "Y" Then
                arrRoleColumn.Add(RoleColumn_Packaging)
            Else
                Dgv_Rekap2.Columns(11).Visible = False
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Report_HPPProduksi_Column_Cost_Production") = "Y" Then
                arrRoleColumn.Add(RoleColumn_CostProduction)
            Else
                Dgv_Rekap2.Columns(12).Visible = False
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Report_HPPProduksi_Column_Total_HPP") = "Y" Then
                arrRoleColumn.Add(RoleColumn_Scrap)
            Else
                Dgv_Rekap2.Columns(13).Visible = False
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Try
            OpenConn()

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Report_HPPProduksi_Column_Scrap") = "Y" Then
                arrRoleColumn.Add(RoleColumn_Scrap)
            Else
                Dgv_Rekap2.Columns(14).Visible = False
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Report_HPPProduksi_Column_Waste") = "Y" Then
                arrRoleColumn.Add(RoleColumn_Waste)
            Else
                Dgv_Rekap2.Columns(15).Visible = False
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Report_HPPProduksi_Column_Loss") = "Y" Then
                arrRoleColumn.Add(RoleColumn_Loss)
            Else
                Dgv_Rekap2.Columns(16).Visible = False
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Report_HPPProduksi_Column_FG") = "Y" Then
                arrRoleColumn.Add(RoleColumn_FG)
            Else
                Dgv_Rekap2.Columns(17).Visible = False
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Report_HPPProduksi_Column_HPPSementara") = "Y" Then
                arrRoleColumn.Add(RoleColumn_HppSementara)
            Else
                Dgv_Rekap2.Columns(18).Visible = False
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Report_HPPProduksi_Column_PackSekunder") = "Y" Then
                arrRoleColumn.Add(RoleColumn_PackSekunder)
            Else
                Dgv_Rekap2.Columns(19).Visible = False
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Report_HPPProduksi_Column_Final") = "Y" Then
                arrRoleColumn.Add(RoleColumn_HPPFinal)
            Else
                Dgv_Rekap2.Columns(20).Visible = False
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Report_HPPProduksi_Column_HPP") = "Y" Then
                arrRoleColumn.Add(RoleColumn_HPP)
            Else
                Dgv_Rekap2.Columns(21).Visible = False
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



        Kosong()
        SyncColumnWidths()
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
        ElseIf Cmb_Jenis.SelectedIndex = -1 Then
            MessageBox.Show("Jenis harus Dipilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarang.Focus() : Exit Sub
        End If

        LoadRekap(True)
        LoadDataDetail(True)

    End Sub



    Private Sub LoadRekap(Optional ByVal isFilter As Boolean = False)
        Try
            OpenConn()

            Dim SumJumlah As Double = 0
            Dim SumJmlhDosing As Double = 0
            Dim SumJmlhBhnBaku As Double = 0
            Dim SumJmlhPackaging As Double = 0
            Dim SumJmlhBiayaProduksi As Double = 0
            Dim SumTotalHPP As Double = 0
            Dim SumHPPScrap As Double = 0
            Dim SumHPPWaste As Double = 0
            Dim SumHPPLoss As Double = 0
            Dim SumHPPFG As Double = 0
            Dim SumHPPSementara As Double = 0
            Dim SumHPPSekunder As Double = 0
            Dim SumHPPFinal As Double = 0
            Dim SumHPPFinalPcs As Double = 0

            Dgv_Rekap2.Rows.Clear() : Dgv_Total.Rows.Clear()
            SQL = "select No_PO, No_Split, Tgl_Produksi, Jam_Produksi, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, Jumlah_Dosing, "
            SQL = SQL & "total_bahan_baku, Total_packaging_GR1 as Total_packaging, Total_Biaya_Produksi, HPP_AVG, "
            SQL = SQL & "HPP_Waste, HPP_Sementara, HPP_Packaging_Sekunder, HPP_Final, HPP_Scrap, HPP_Loss, HPP_FinishedGood, HPP_TotalSebelumFG, Status "
            SQL = SQL & "from N_EMI_Laporan_HPP_Produksi "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Tgl_Produksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

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

                If Not Cmb_Jenis.SelectedIndex = 0 Then
                    SQL = SQL & "and Group_Jenis = '" & arrJenis(Cmb_Jenis.SelectedIndex) & "' "
                End If


            End If

            SQL = SQL & "order by no_split, Tgl_Produksi, Jam_Produksi "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dgv_Rekap2.Rows.Add(1)
                            Dgv_Rekap2.Rows(i).Cells(0).Value = If(General_Class.CekNULL(.Rows(i)("No_PO")) = "", "-", .Rows(i)("No_PO"))
                            Dgv_Rekap2.Rows(i).Cells(1).Value = If(General_Class.CekNULL(.Rows(i)("No_Split")) = "", "-", .Rows(i)("No_Split"))
                            Dgv_Rekap2.Rows(i).Cells(2).Value = If(General_Class.CekNULL(.Rows(i)("Kode_Barang")) = "", "-", .Rows(i)("Kode_Barang"))
                            Dgv_Rekap2.Rows(i).Cells(3).Value = If(General_Class.CekNULL(.Rows(i)("Nama")) = "", "-", .Rows(i)("Nama"))
                            Dgv_Rekap2.Rows(i).Cells(4).Value = Format(.Rows(i)("Tgl_Produksi"), "dd MMM yyyy") '4
                            Dgv_Rekap2.Rows(i).Cells(5).Value = If(General_Class.CekNULL(.Rows(i)("Jam_Produksi")) = "", "-", .Rows(i)("Jam_Produksi"))
                            Dgv_Rekap2.Rows(i).Cells(6).Value = If(General_Class.CekNULL(.Rows(i)("Nama_Routing")) = "", "-", .Rows(i)("Nama_Routing"))
                            Dgv_Rekap2.Rows(i).Cells(7).Value = If(General_Class.CekNULL(.Rows(i)("Keterangan")) = "", "-", .Rows(i)("Keterangan"))
                            Dgv_Rekap2.Rows(i).Cells(8).Value = Format(.Rows(i)("Jumlah"), "N0") '8
                            SumJumlah += Val(.Rows(i)("Jumlah"))

                            If arrRoleColumn.Contains(RoleColumn_GI) Then
                                Dgv_Rekap2.Rows(i).Cells(9).Value = If(General_Class.CekNULL(.Rows(i)("Jumlah_Dosing")) = "", 0, Format(.Rows(i)("Jumlah_Dosing"), "N0"))
                                SumJmlhDosing += Val(.Rows(i)("Jumlah_Dosing"))
                            Else
                                Dgv_Rekap2.Rows(i).Cells(9).Value = "X" '9
                                SumJmlhDosing += 0
                            End If

                            If arrRoleColumn.Contains(RoleColumn_BahanBaku) Then
                                Dgv_Rekap2.Rows(i).Cells(10).Value = If(General_Class.CekNULL(.Rows(i)("total_bahan_baku")) = "", 0, Format(.Rows(i)("total_bahan_baku"), "N0"))
                                SumJmlhBhnBaku += Val(.Rows(i)("total_bahan_baku"))
                            Else
                                Dgv_Rekap2.Rows(i).Cells(10).Value = "X" '9
                                SumJmlhBhnBaku += 0
                            End If

                            If arrRoleColumn.Contains(RoleColumn_Packaging) Then
                                Dgv_Rekap2.Rows(i).Cells(11).Value = If(General_Class.CekNULL(.Rows(i)("Total_packaging")) = "", 0, Format(.Rows(i)("Total_packaging"), "N0"))
                                SumJmlhPackaging += Val(.Rows(i)("Total_packaging"))
                            Else
                                Dgv_Rekap2.Rows(i).Cells(11).Value = "X" '9
                                SumJmlhPackaging += 0
                            End If

                            If arrRoleColumn.Contains(RoleColumn_CostProduction) Then
                                Dgv_Rekap2.Rows(i).Cells(12).Value = If(General_Class.CekNULL(.Rows(i)("Total_Biaya_Produksi")) = "", 0, Format(.Rows(i)("Total_Biaya_Produksi"), "N0"))
                                SumJmlhBiayaProduksi += Val(.Rows(i)("Total_Biaya_Produksi"))
                            Else
                                Dgv_Rekap2.Rows(i).Cells(12).Value = "X" '9
                                SumJmlhBiayaProduksi += 0
                            End If

                            If arrRoleColumn.Contains(RoleColumn_CostProduction) Then
                                Dgv_Rekap2.Rows(i).Cells(13).Value = If(General_Class.CekNULL(.Rows(i)("HPP_TotalSebelumFG")) = "", 0, Format(.Rows(i)("HPP_TotalSebelumFG"), "N0"))
                                SumTotalHPP += Val(.Rows(i)("HPP_TotalSebelumFG"))
                            Else
                                Dgv_Rekap2.Rows(i).Cells(13).Value = "X" '9
                                SumTotalHPP += 0
                            End If

                            If arrRoleColumn.Contains(RoleColumn_Scrap) Then
                                Dgv_Rekap2.Rows(i).Cells(14).Value = If(General_Class.CekNULL(.Rows(i)("HPP_Scrap")) = "", 0, Format(.Rows(i)("HPP_Scrap"), "N0"))
                                SumHPPScrap += Val(.Rows(i)("HPP_Scrap"))
                            Else
                                Dgv_Rekap2.Rows(i).Cells(14).Value = "X" '9
                                SumHPPScrap += 0
                            End If

                            If arrRoleColumn.Contains(RoleColumn_Waste) Then
                                Dgv_Rekap2.Rows(i).Cells(15).Value = If(General_Class.CekNULL(.Rows(i)("HPP_Waste")) = "", 0, Format(.Rows(i)("HPP_Waste"), "N0"))
                                SumHPPWaste += Val(.Rows(i)("HPP_Waste"))
                            Else
                                Dgv_Rekap2.Rows(i).Cells(15).Value = "X" '9
                                SumHPPWaste += 0
                            End If

                            If arrRoleColumn.Contains(RoleColumn_Loss) Then
                                Dgv_Rekap2.Rows(i).Cells(16).Value = If(General_Class.CekNULL(.Rows(i)("HPP_Loss")) = "", 0, Format(.Rows(i)("HPP_Loss"), "N0"))
                                SumHPPLoss += Val(.Rows(i)("HPP_Loss"))
                            Else
                                Dgv_Rekap2.Rows(i).Cells(16).Value = "X" '9
                                SumHPPLoss += 0
                            End If

                            If arrRoleColumn.Contains(RoleColumn_FG) Then
                                Dgv_Rekap2.Rows(i).Cells(17).Value = If(General_Class.CekNULL(.Rows(i)("HPP_FinishedGood")) = "", 0, Format(.Rows(i)("HPP_FinishedGood"), "N0"))
                                SumHPPFG += Val(.Rows(i)("HPP_FinishedGood"))
                            Else
                                Dgv_Rekap2.Rows(i).Cells(17).Value = "X" '9
                                SumHPPFG += 0
                            End If

                            If arrRoleColumn.Contains(RoleColumn_HppSementara) Then
                                Dgv_Rekap2.Rows(i).Cells(18).Value = If(General_Class.CekNULL(.Rows(i)("HPP_Sementara")) = "", 0, Format(.Rows(i)("HPP_Sementara"), "N0"))
                                SumHPPSementara += Val(.Rows(i)("HPP_Sementara"))
                            Else
                                Dgv_Rekap2.Rows(i).Cells(18).Value = "X" '9
                                SumHPPSementara += 0
                            End If

                            If arrRoleColumn.Contains(RoleColumn_PackSekunder) Then
                                Dgv_Rekap2.Rows(i).Cells(19).Value = If(General_Class.CekNULL(.Rows(i)("HPP_Packaging_Sekunder")) = "", 0, Format(.Rows(i)("HPP_Packaging_Sekunder"), "N0"))
                                SumHPPSekunder += Val(.Rows(i)("HPP_Packaging_Sekunder"))
                            Else
                                Dgv_Rekap2.Rows(i).Cells(19).Value = "X" '9
                                SumHPPSekunder += 0
                            End If

                            If arrRoleColumn.Contains(RoleColumn_HPPFinal) Then
                                Dgv_Rekap2.Rows(i).Cells(20).Value = If(General_Class.CekNULL(.Rows(i)("HPP_Final")) = "", 0, Format(.Rows(i)("HPP_Final"), "N0"))
                                SumHPPFinal += Val(.Rows(i)("HPP_Final"))
                            Else
                                Dgv_Rekap2.Rows(i).Cells(20).Value = "X" '9
                                SumHPPFinal += 0
                            End If

                            If arrRoleColumn.Contains(RoleColumn_HPP) Then
                                Dgv_Rekap2.Rows(i).Cells(21).Value = If(General_Class.CekNULL(.Rows(i)("HPP_AVG")) = "", 0, Format(.Rows(i)("HPP_AVG"), "N0"))
                                SumHPPFinalPcs += Val(.Rows(i)("HPP_AVG"))
                            Else
                                Dgv_Rekap2.Rows(i).Cells(21).Value = "X" '9
                                SumHPPFinalPcs += 0
                            End If

                            Dgv_Rekap2.Rows(i).Cells(22).Value = .Rows(i).Item("Status")


                            If Not General_Class.CekNULL(.Rows(i).Item("Status")) = "" Then
                                If .Rows(i).Item("Status") = "PRODUCTION" Then
                                    Dgv_Rekap2.Rows(i).Cells(22).Style.BackColor = Color.LightYellow
                                ElseIf .Rows(i).Item("Status") = "INSPECTION" Then
                                    Dgv_Rekap2.Rows(i).Cells(22).Style.BackColor = Color.LightBlue
                                ElseIf .Rows(i).Item("Status") = "WAREHOUSE" Then
                                    Dgv_Rekap2.Rows(i).Cells(22).Style.BackColor = Color.LightGray
                                Else
                                    Dgv_Rekap2.Rows(i).Cells(22).Style.BackColor = Color.White
                                End If
                            Else
                                Dgv_Rekap2.Rows(i).Cells(22).Style.BackColor = Color.White
                            End If


                            '=== WARNA ==='

                            Dgv_Rekap2.Rows(i).Cells(9).Style.BackColor = Color.FromArgb(247, 183, 183)
                            Dgv_Rekap2.Rows(i).Cells(10).Style.BackColor = Color.FromArgb(247, 183, 183)
                            Dgv_Rekap2.Rows(i).Cells(11).Style.BackColor = Color.FromArgb(247, 183, 183)
                            Dgv_Rekap2.Rows(i).Cells(12).Style.BackColor = Color.FromArgb(247, 183, 183)
                            Dgv_Rekap2.Rows(i).Cells(13).Style.BackColor = Color.FromArgb(247, 183, 183)
                            Dgv_Rekap2.Rows(i).Cells(14).Style.BackColor = Color.FromArgb(247, 183, 183)
                            Dgv_Rekap2.Rows(i).Cells(15).Style.BackColor = Color.FromArgb(247, 183, 183)
                            Dgv_Rekap2.Rows(i).Cells(16).Style.BackColor = Color.FromArgb(247, 183, 183)
                            Dgv_Rekap2.Rows(i).Cells(17).Style.BackColor = Color.FromArgb(247, 183, 183)
                            Dgv_Rekap2.Rows(i).Cells(18).Style.BackColor = Color.FromArgb(247, 183, 183)
                            Dgv_Rekap2.Rows(i).Cells(19).Style.BackColor = Color.FromArgb(247, 183, 183)
                            Dgv_Rekap2.Rows(i).Cells(20).Style.BackColor = Color.FromArgb(247, 183, 183)
                            Dgv_Rekap2.Rows(i).Cells(21).Style.BackColor = Color.FromArgb(247, 183, 183)



                        Next
                    End If
                End With
            End Using


            Dgv_Total.Rows.Clear()
            Dgv_Total.Rows.Add(1)
            Dgv_Total.Rows(0).Cells(0).Value = ""
            Dgv_Total.Rows(0).Cells(1).Value = ""
            Dgv_Total.Rows(0).Cells(2).Value = ""
            Dgv_Total.Rows(0).Cells(3).Value = "TOTAL"
            Dgv_Total.Rows(0).Cells(4).Value = ""
            Dgv_Total.Rows(0).Cells(5).Value = ""
            Dgv_Total.Rows(0).Cells(6).Value = ""
            Dgv_Total.Rows(0).Cells(7).Value = ""
            Dgv_Total.Rows(0).Cells(8).Value = Format(SumJumlah, "N0")
            Dgv_Total.Rows(0).Cells(9).Value = Format(SumJmlhDosing, "N0")
            Dgv_Total.Rows(0).Cells(10).Value = Format(SumJmlhBhnBaku, "N0")
            Dgv_Total.Rows(0).Cells(11).Value = Format(SumJmlhPackaging, "N0")
            Dgv_Total.Rows(0).Cells(12).Value = Format(SumJmlhBiayaProduksi, "N0")
            Dgv_Total.Rows(0).Cells(13).Value = Format(SumTotalHPP, "N0")
            Dgv_Total.Rows(0).Cells(14).Value = Format(SumHPPScrap, "N0")
            Dgv_Total.Rows(0).Cells(15).Value = Format(SumHPPWaste, "N0")
            Dgv_Total.Rows(0).Cells(16).Value = Format(SumHPPLoss, "N0")
            Dgv_Total.Rows(0).Cells(17).Value = Format(SumHPPFG, "N0")
            Dgv_Total.Rows(0).Cells(18).Value = Format(SumHPPSementara, "N0")
            Dgv_Total.Rows(0).Cells(19).Value = Format(SumHPPSekunder, "N0")
            Dgv_Total.Rows(0).Cells(20).Value = Format(SumHPPFinal, "N0")
            Dgv_Total.Rows(0).Cells(21).Value = Format(SumHPPFinalPcs, "N0")
            Dgv_Total.Rows(0).Cells(22).Value = ""

            Dgv_Total.Rows(0).DefaultCellStyle.Font = New Font(Dgv_Total.Font, FontStyle.Bold)




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

            DGV_Detail2.Rows.Clear()
            SQL = "select No_PO, No_Split, Tgl_Produksi, Jam_Produksi, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, Jumlah_Dosing, "
            SQL = SQL & "total_bahan_baku, Total_packaging_GR1 as Total_packaging, Total_Biaya_Produksi, HPP_AVG as HPP_AVG_GR2_Finished_Good, "
            SQL = SQL & "HPP_Waste, HPP_Sementara, HPP_Packaging_Sekunder, HPP_Final, HPP_Scrap, HPP_Loss, HPP_FinishedGood "
            SQL = SQL & "from N_EMI_Laporan_HPP_Produksi_PCS "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Tgl_Produksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

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

                If Not Cmb_Jenis.SelectedIndex = 0 Then
                    SQL = SQL & "and Group_Jenis = '" & arrJenis(Cmb_Jenis.SelectedIndex) & "' "
                End If

            End If

            SQL = SQL & "order by no_split, Tgl_Produksi, Jam_Produksi "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            DGV_Detail2.Rows.Add(1)
                            DGV_Detail2.Rows(i).Cells(0).Value = .Rows(i)("No_PO") '0
                            DGV_Detail2.Rows(i).Cells(1).Value = .Rows(i)("No_Split") '1
                            DGV_Detail2.Rows(i).Cells(2).Value = .Rows(i)("Kode_Barang") '2
                            DGV_Detail2.Rows(i).Cells(3).Value = .Rows(i)("Nama") '3
                            DGV_Detail2.Rows(i).Cells(4).Value = Format(.Rows(i)("Tgl_Produksi"), "dd MMM yyyy") '4
                            DGV_Detail2.Rows(i).Cells(5).Value = .Rows(i)("Jam_Produksi") '5
                            DGV_Detail2.Rows(i).Cells(6).Value = .Rows(i)("Nama_Routing") '6
                            DGV_Detail2.Rows(i).Cells(7).Value = .Rows(i)("Keterangan") '7
                            DGV_Detail2.Rows(i).Cells(8).Value = Format(.Rows(i)("Jumlah"), "N0") '8

                            If arrRoleColumn.Contains(RoleColumn_GI) Then
                                DGV_Detail2.Rows(i).Cells(9).Value = If(General_Class.CekNULL(.Rows(i)("Jumlah_Dosing")) = "", 0, Format(.Rows(i)("Jumlah_Dosing"), "N0"))
                            Else
                                DGV_Detail2.Rows(i).Cells(9).Value = "X" '9
                            End If

                            If arrRoleColumn.Contains(RoleColumn_BahanBaku) Then
                                DGV_Detail2.Rows(i).Cells(10).Value = If(General_Class.CekNULL(.Rows(i)("total_bahan_baku")) = "", 0, Format(.Rows(i)("total_bahan_baku"), "N0"))
                            Else
                                DGV_Detail2.Rows(i).Cells(10).Value = "X" '9
                            End If

                            If arrRoleColumn.Contains(RoleColumn_Packaging) Then
                                DGV_Detail2.Rows(i).Cells(11).Value = If(General_Class.CekNULL(.Rows(i)("Total_packaging")) = "", 0, Format(.Rows(i)("Total_packaging"), "N0"))
                            Else
                                DGV_Detail2.Rows(i).Cells(11).Value = "X" '9
                            End If

                            If arrRoleColumn.Contains(RoleColumn_CostProduction) Then
                                DGV_Detail2.Rows(i).Cells(12).Value = If(General_Class.CekNULL(.Rows(i)("Total_Biaya_Produksi")) = "", 0, Format(.Rows(i)("Total_Biaya_Produksi"), "N0"))
                            Else
                                DGV_Detail2.Rows(i).Cells(12).Value = "X" '9
                            End If

                            If arrRoleColumn.Contains(RoleColumn_Scrap) Then
                                DGV_Detail2.Rows(i).Cells(13).Value = If(General_Class.CekNULL(.Rows(i)("HPP_Scrap")) = "", 0, Format(.Rows(i)("HPP_Scrap"), "N0"))
                            Else
                                DGV_Detail2.Rows(i).Cells(13).Value = "X" '9
                            End If

                            If arrRoleColumn.Contains(RoleColumn_Waste) Then
                                DGV_Detail2.Rows(i).Cells(14).Value = If(General_Class.CekNULL(.Rows(i)("HPP_Waste")) = "", 0, Format(.Rows(i)("HPP_Waste"), "N0"))
                            Else
                                DGV_Detail2.Rows(i).Cells(14).Value = "X" '9
                            End If

                            If arrRoleColumn.Contains(RoleColumn_Loss) Then
                                DGV_Detail2.Rows(i).Cells(15).Value = If(General_Class.CekNULL(.Rows(i)("HPP_Loss")) = "", 0, Format(.Rows(i)("HPP_Loss"), "N0"))
                            Else
                                DGV_Detail2.Rows(i).Cells(15).Value = "X" '9
                            End If

                            If arrRoleColumn.Contains(RoleColumn_FG) Then
                                DGV_Detail2.Rows(i).Cells(16).Value = If(General_Class.CekNULL(.Rows(i)("HPP_FinishedGood")) = "", 0, Format(.Rows(i)("HPP_FinishedGood"), "N0"))
                            Else
                                DGV_Detail2.Rows(i).Cells(16).Value = "X" '9
                            End If

                            If arrRoleColumn.Contains(RoleColumn_HppSementara) Then
                                DGV_Detail2.Rows(i).Cells(17).Value = If(General_Class.CekNULL(.Rows(i)("HPP_Sementara")) = "", 0, Format(.Rows(i)("HPP_Sementara"), "N0"))
                            Else
                                DGV_Detail2.Rows(i).Cells(17).Value = "X" '9
                            End If

                            If arrRoleColumn.Contains(RoleColumn_PackSekunder) Then
                                DGV_Detail2.Rows(i).Cells(18).Value = If(General_Class.CekNULL(.Rows(i)("HPP_Packaging_Sekunder")) = "", 0, Format(.Rows(i)("HPP_Packaging_Sekunder"), "N0"))
                            Else
                                DGV_Detail2.Rows(i).Cells(18).Value = "X" '9
                            End If

                            If arrRoleColumn.Contains(RoleColumn_HPPFinal) Then
                                DGV_Detail2.Rows(i).Cells(19).Value = If(General_Class.CekNULL(.Rows(i)("HPP_Final")) = "", 0, Format(.Rows(i)("HPP_Final"), "N0"))
                            Else
                                DGV_Detail2.Rows(i).Cells(19).Value = "X" '9
                            End If

                            If arrRoleColumn.Contains(RoleColumn_HPP) Then
                                DGV_Detail2.Rows(i).Cells(20).Value = If(General_Class.CekNULL(.Rows(i)("HPP_AVG_GR2_Finished_Good")) = "", 0, Format(.Rows(i)("HPP_AVG_GR2_Finished_Good"), "N0"))
                            Else
                                DGV_Detail2.Rows(i).Cells(20).Value = "X" '9
                            End If


                            '=== WARNA ==='

                            DGV_Detail2.Rows(i).Cells(9).Style.BackColor = Color.FromArgb(247, 183, 183)
                            DGV_Detail2.Rows(i).Cells(10).Style.BackColor = Color.FromArgb(247, 183, 183)
                            DGV_Detail2.Rows(i).Cells(11).Style.BackColor = Color.FromArgb(247, 183, 183)
                            DGV_Detail2.Rows(i).Cells(12).Style.BackColor = Color.FromArgb(247, 183, 183)
                            DGV_Detail2.Rows(i).Cells(13).Style.BackColor = Color.FromArgb(247, 183, 183)
                            DGV_Detail2.Rows(i).Cells(14).Style.BackColor = Color.FromArgb(247, 183, 183)
                            DGV_Detail2.Rows(i).Cells(15).Style.BackColor = Color.FromArgb(247, 183, 183)
                            DGV_Detail2.Rows(i).Cells(16).Style.BackColor = Color.FromArgb(247, 183, 183)
                            DGV_Detail2.Rows(i).Cells(17).Style.BackColor = Color.FromArgb(247, 183, 183)
                            DGV_Detail2.Rows(i).Cells(18).Style.BackColor = Color.FromArgb(247, 183, 183)
                            DGV_Detail2.Rows(i).Cells(19).Style.BackColor = Color.FromArgb(247, 183, 183)
                            DGV_Detail2.Rows(i).Cells(20).Style.BackColor = Color.FromArgb(247, 183, 183)




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
            Lv_Routing.Location = New Point(1200, 123)
            Lv_Routing.Visible = False
            Txt_IdRouting.Text = ""
            Txt_NmRouting.Text = ""
            Exit Sub
        Else
            Lv_Routing.Visible = True
            Lv_Routing.Location = New Point(140, 123)
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
            Lv_Routing.Location = New Point(1200, 123)
            Lv_Routing.Visible = False
            Txt_IdRouting.Text = ""
            Txt_NmRouting.Text = ""
            Exit Sub
        Else
            Lv_Routing.Visible = True
            Lv_Routing.Location = New Point(140, 123)
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
            Lv_Barang.Location = New Point(1200, 150)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(140, 150)
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
            Lv_Barang.Location = New Point(1200, 150)
            Lv_Barang.Visible = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Lv_Barang.Visible = True
            Lv_Barang.Location = New Point(140, 150)
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

                    Lv_Routing.Location = New Point(1200, 123)
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

                    Lv_Barang.Location = New Point(1200, 150)
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


            Lv_Routing.Location = New Point(1200, 123)
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

            Lv_Routing.Location = New Point(1200, 123)
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

            Lv_Barang.Location = New Point(1200, 150)
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

            Lv_Barang.Location = New Point(1200, 150)
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

        Lv_Routing.Location = New Point(1200, 123)
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

        Lv_Barang.Location = New Point(1200, 150)
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

        Generate_Excel_Rekap2()

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


        If MessageBox.Show("Yakin Ingin Cetak Laporan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

        Generate_Excel_HPPTotalPcs()
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
                New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Berat (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
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

                                ' Format numerik (N0)
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

            Dim JudulLaporan As String = "LAPORAN HPP PRODUKSI"

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
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Routing"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Keterangan"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Kode Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Nama Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Jumlah"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Satuan"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Good Issue (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Biaya Produksi"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Budget Nilai Loss"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Budget Persen Loss"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Biaya Bahan Baku"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Biaya Packaging Lv 1"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Biaya Packaging Lv 2"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Total HPP"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Jumlah (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 221, 91))}},
            New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "HPP (RP)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 221, 91))}},
            New Dictionary(Of String, String) From {{"Identifier", "GR1"}, {"Kolom", "Total"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 221, 91))}},
            New Dictionary(Of String, String) From {{"Identifier", "GR2"}, {"Kolom", "Jumlah (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(71, 211, 89))}},
            New Dictionary(Of String, String) From {{"Identifier", "GR2"}, {"Kolom", "HPP (RP)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(71, 211, 89))}},
            New Dictionary(Of String, String) From {{"Identifier", "GR2"}, {"Kolom", "Total"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(71, 211, 89))}},
            New Dictionary(Of String, String) From {{"Identifier", "Scrap"}, {"Kolom", "Jumlah (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(173, 173, 173))}},
            New Dictionary(Of String, String) From {{"Identifier", "Scrap"}, {"Kolom", "HPP (RP)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(173, 173, 173))}},
            New Dictionary(Of String, String) From {{"Identifier", "Scrap"}, {"Kolom", "Total"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(173, 173, 173))}},
            New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Jumlah Finished Good"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(77, 147, 217))}},
            New Dictionary(Of String, String) From {{"Identifier", "Val"}, {"Kolom", "Nilai HPP (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(77, 147, 217))}}
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
                    xlWorkSheet.Cells(3, indexAwal).Value = "Hasil Finished Good"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Range(xlWorkSheet.Columns(3), xlWorkSheet.Columns(i + 1)).EntireColumn.AutoFit()

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



                ElseIf kolom("Identifier") = "GR2" Then

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
                    xlWorkSheet.Cells(3, indexAwal).Value = "Hasil Rejected"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).EntireColumn.AutoFit()

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
                    xlWorkSheet.Cells(3, indexAwal).Value = "Hasil Scrap"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).EntireColumn.AutoFit()

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


                End If

            Next

#End Region


            '=========================
            '=     GENERATE BODY     =
            '=========================

            Dim stringCenter As New List(Of Integer) From {7}

            Dim numberColumn As New List(Of Integer) From {6, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26}

            'Dim currencyColumn As New List(Of Integer) From {20, 22, 25, 27}

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

                                ' Format numerik (N0)
                                If numberColumn.Contains(colIndex) Then
                                    Dim nilai As Double = If(General_Class.CekNULL(.Rows(i).Item(colIndex)) = "", 0, .Rows(i).Item(colIndex))

                                    cell.NumberFormat = excelFormat
                                    cell.Value = nilai
                                Else
                                    cell.Value = If(General_Class.CekNULL(.Rows(i).Item(colIndex)) = "", "-", General_Class.CekNULL(.Rows(i).Item(colIndex)))
                                End If

                                cell.VerticalAlignment = excel.XlVAlign.xlVAlignCenter





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
                                    Case 0 To 8
                                        cell.EntireColumn.AutoFit()
                                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
                                    Case 9 To 15
                                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(247, 183, 183))
                                        cell.Columns(i).ColumnWidth = 24
                                    Case 16 To 18
                                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow)
                                        cell.Columns(i).ColumnWidth = 24
                                    Case 19 To 21
                                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen)
                                        cell.Columns(i).ColumnWidth = 24
                                    Case 22 To 24
                                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
                                        cell.Columns(i).ColumnWidth = 24
                                    Case 25 To 26
                                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                                        cell.Columns(i).ColumnWidth = 24
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

    Private Sub Generate_Excel_Rekap2()
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

            Dim JudulLaporan As String = "LAPORAN TOTAL HPP PRODUKSI"

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
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "PRO-REQ (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Good Issue (KG)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Bahan Baku (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Packaging (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Cost Production"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Total HPP"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "HPP Scrap"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "HPP Waste"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "HPP Loss Production"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "HPP Finished Good"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "HPP Sementara"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "HPP Packaging Sekunder"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "HPP Final"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "HPP (Avg)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Status"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}}
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
                    xlWorkSheet.Cells(3, indexAwal).Value = "Hasil Finished Good"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Range(xlWorkSheet.Columns(3), xlWorkSheet.Columns(i + 1)).EntireColumn.AutoFit()

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



                ElseIf kolom("Identifier") = "GR2" Then

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
                    xlWorkSheet.Cells(3, indexAwal).Value = "Hasil Rejected"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).EntireColumn.AutoFit()

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
                    xlWorkSheet.Cells(3, indexAwal).Value = "Hasil Scrap"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).EntireColumn.AutoFit()

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


                End If

                ' ROLE COLUMN
                Select Case i + 1
                    Case 9
                        If Not arrRoleColumn.Contains(RoleColumn_GI) Then
                            xlWorkSheet.Columns(9).Hidden = True
                        End If
                    Case 10
                        If Not arrRoleColumn.Contains(RoleColumn_BahanBaku) Then
                            xlWorkSheet.Columns(10).Hidden = True
                        End If
                    Case 11
                        If Not arrRoleColumn.Contains(RoleColumn_Packaging) Then
                            xlWorkSheet.Columns(11).Hidden = True
                        End If
                    Case 12
                        If Not arrRoleColumn.Contains(RoleColumn_CostProduction) Then
                            xlWorkSheet.Columns(12).Hidden = True
                        End If
                    Case 13
                        If Not arrRoleColumn.Contains(RoleColumn_TotalHPP) Then
                            xlWorkSheet.Columns(13).Hidden = True
                        End If

                    Case 14
                        If Not arrRoleColumn.Contains(RoleColumn_Scrap) Then
                            xlWorkSheet.Columns(14).Hidden = True
                        End If

                    Case 15
                        If Not arrRoleColumn.Contains(RoleColumn_Waste) Then
                            xlWorkSheet.Columns(15).Hidden = True
                        End If

                    Case 16
                        If Not arrRoleColumn.Contains(RoleColumn_Loss) Then
                            xlWorkSheet.Columns(16).Hidden = True
                        End If

                    Case 17
                        If Not arrRoleColumn.Contains(RoleColumn_FG) Then
                            xlWorkSheet.Columns(17).Hidden = True
                        End If

                    Case 18
                        If Not arrRoleColumn.Contains(RoleColumn_HppSementara) Then
                            xlWorkSheet.Columns(18).Hidden = True
                        End If

                    Case 19
                        If Not arrRoleColumn.Contains(RoleColumn_PackSekunder) Then
                            xlWorkSheet.Columns(19).Hidden = True
                        End If

                    Case 20
                        If Not arrRoleColumn.Contains(RoleColumn_HPPFinal) Then
                            xlWorkSheet.Columns(20).Hidden = True
                        End If

                    Case 21
                        If Not arrRoleColumn.Contains(RoleColumn_HPP) Then
                            xlWorkSheet.Columns(21).Hidden = True
                        End If
                End Select

            Next

#End Region


            '=========================
            '=     GENERATE BODY     =
            '=========================

            Dim stringCenter As New List(Of Integer) From {6, 7}

            Dim numberColumn As New List(Of Integer) From {8, 9, 10, 11, 12, 13, 14, 15, 16, 17}

            Dim NumberN0 As New List(Of Integer) From {10, 11}

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
                SQL = "select No_PO, No_Split, Tgl_Produksi, Jam_Produksi, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, Jumlah_Dosing, "
                SQL = SQL & "total_bahan_baku, Total_packaging_GR1 as Total_packaging, Total_Biaya_Produksi, HPP_TotalSebelumFG, HPP_Scrap, "
                SQL = SQL & "HPP_Waste, HPP_Loss, HPP_FinishedGood, HPP_Sementara, HPP_Packaging_Sekunder, HPP_Final, HPP_AVG as HPP_AVG_GR2_Finished_Good, Status "
                SQL = SQL & "from N_EMI_Laporan_HPP_Produksi "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Tgl_Produksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                If Not Txt_IdRouting.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Id_Routing = '" & Txt_IdRouting.Text & "' "
                End If
                If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
                End If
                If Txt_NoSplit.Text.Trim.Length > 0 AndAlso Txt_NoSplit.Text.ToUpper <> OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and no_split like '%" & Txt_NoSplit.Text & "%' "
                End If
                SQL = SQL & "order by no_split, Tgl_Produksi, Jam_Produksi "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")

                        For i As Integer = 0 To .Rows.Count - 1

                            For colIndex As Integer = 0 To .Columns.Count - 1
                                Dim cell = xlWorkSheet.Cells(i + defaultRowIndex, colIndex + 1)
                                If colIndex = 6 Then
                                    cell.NumberFormat = "@"
                                End If

                                ' Format numerik (N0)
                                If numberColumn.Contains(colIndex) Then
                                    Dim nilai As Double = If(General_Class.CekNULL(.Rows(i).Item(colIndex)) = "", 0, .Rows(i).Item(colIndex))

                                    If NumberN0.Contains(colIndex) Then
                                        cell.NumberFormat = excelFormatN0
                                    Else
                                        cell.NumberFormat = excelFormat
                                    End If

                                    cell.Value = nilai
                                Else
                                    cell.Value = If(General_Class.CekNULL(.Rows(i).Item(colIndex)) = "", "-", General_Class.CekNULL(.Rows(i).Item(colIndex)))
                                End If

                                ' ROLE COLUMN
                                Select Case colIndex
                                    Case 9
                                        If Not arrRoleColumn.Contains(RoleColumn_GI) Then
                                            cell.Value = 0
                                        End If
                                    Case 10
                                        If Not arrRoleColumn.Contains(RoleColumn_BahanBaku) Then
                                            cell.Value = 0
                                        End If
                                    Case 11
                                        If Not arrRoleColumn.Contains(RoleColumn_Packaging) Then
                                            cell.Value = 0
                                        End If
                                    Case 12
                                        If Not arrRoleColumn.Contains(RoleColumn_CostProduction) Then
                                            cell.Value = 0
                                        End If

                                    Case 13
                                        If Not arrRoleColumn.Contains(RoleColumn_TotalHPP) Then
                                            cell.Value = 0
                                        End If

                                    Case 14
                                        If Not arrRoleColumn.Contains(RoleColumn_Scrap) Then
                                            cell.Value = 0
                                        End If

                                    Case 15
                                        If Not arrRoleColumn.Contains(RoleColumn_Waste) Then
                                            cell.Value = 0
                                        End If

                                    Case 16
                                        If Not arrRoleColumn.Contains(RoleColumn_Loss) Then
                                            cell.Value = 0
                                        End If

                                    Case 17
                                        If Not arrRoleColumn.Contains(RoleColumn_FG) Then
                                            cell.Value = 0
                                        End If

                                    Case 18
                                        If Not arrRoleColumn.Contains(RoleColumn_HppSementara) Then
                                            cell.Value = 0
                                        End If

                                    Case 19
                                        If Not arrRoleColumn.Contains(RoleColumn_PackSekunder) Then
                                            cell.Value = 0
                                        End If

                                    Case 20
                                        If Not arrRoleColumn.Contains(RoleColumn_HPPFinal) Then
                                            cell.Value = 0
                                        End If

                                    Case 21
                                        If Not arrRoleColumn.Contains(RoleColumn_HPP) Then
                                            cell.Value = 0
                                        End If
                                End Select

                                cell.VerticalAlignment = excel.XlVAlign.xlVAlignCenter


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
                                    Case 0 To 8
                                        cell.EntireColumn.AutoFit()
                                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
                                    Case 9 To 22
                                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(247, 183, 183))

                                        Select Case colIndex
                                            Case 9
                                                If Not arrRoleColumn.Contains(RoleColumn_GI) Then
                                                    cell.ColumnWidth = 0
                                                Else
                                                    cell.ColumnWidth = 24
                                                End If
                                            Case 10
                                                If Not arrRoleColumn.Contains(RoleColumn_BahanBaku) Then
                                                    cell.ColumnWidth = 0
                                                Else
                                                    cell.ColumnWidth = 24
                                                End If
                                            Case 11
                                                If Not arrRoleColumn.Contains(RoleColumn_Packaging) Then
                                                    cell.ColumnWidth = 0
                                                Else
                                                    cell.ColumnWidth = 24
                                                End If
                                            Case 12
                                                If Not arrRoleColumn.Contains(RoleColumn_CostProduction) Then
                                                    cell.ColumnWidth = 0
                                                Else
                                                    cell.ColumnWidth = 24
                                                End If

                                            Case 13
                                                If Not arrRoleColumn.Contains(RoleColumn_TotalHPP) Then
                                                    cell.ColumnWidth = 0
                                                Else
                                                    cell.ColumnWidth = 24
                                                End If
                                            Case 14
                                                If Not arrRoleColumn.Contains(RoleColumn_Scrap) Then
                                                    cell.ColumnWidth = 0
                                                Else
                                                    cell.ColumnWidth = 24
                                                End If

                                            Case 15
                                                If Not arrRoleColumn.Contains(RoleColumn_Waste) Then
                                                    cell.ColumnWidth = 0
                                                Else
                                                    cell.ColumnWidth = 24
                                                End If

                                            Case 16
                                                If Not arrRoleColumn.Contains(RoleColumn_Loss) Then
                                                    cell.ColumnWidth = 0
                                                Else
                                                    cell.ColumnWidth = 24
                                                End If

                                            Case 17
                                                If Not arrRoleColumn.Contains(RoleColumn_FG) Then
                                                    cell.ColumnWidth = 0
                                                Else
                                                    cell.ColumnWidth = 24
                                                End If

                                            Case 18
                                                If Not arrRoleColumn.Contains(RoleColumn_HppSementara) Then
                                                    cell.ColumnWidth = 0
                                                Else
                                                    cell.ColumnWidth = 24
                                                End If

                                            Case 19
                                                If Not arrRoleColumn.Contains(RoleColumn_PackSekunder) Then
                                                    cell.ColumnWidth = 0
                                                Else
                                                    cell.ColumnWidth = 24
                                                End If

                                            Case 20
                                                If Not arrRoleColumn.Contains(RoleColumn_HPPFinal) Then
                                                    cell.ColumnWidth = 0
                                                Else
                                                    cell.ColumnWidth = 24
                                                End If

                                            Case 21
                                                If Not arrRoleColumn.Contains(RoleColumn_HPP) Then
                                                    cell.ColumnWidth = 0
                                                Else
                                                    cell.ColumnWidth = 24
                                                End If

                                            Case 22
                                                cell.ColumnWidth = 24

                                                If .Rows(i).Item("Status") = "PRODUCTION" Then
                                                    cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow)
                                                ElseIf .Rows(i).Item("Status") = "INSPECTION" Then
                                                    cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                                                ElseIf .Rows(i).Item("Status") = "WAREHOUSE" Then
                                                    cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
                                                End If
                                        End Select

                                        'Case 16 To 18
                                        '    cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow)
                                        '    cell.Columns(i).ColumnWidth = 24
                                        'Case 19 To 21
                                        '    cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen)
                                        '    cell.Columns(i).ColumnWidth = 24
                                        'Case 22 To 24
                                        '    cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
                                        '    cell.Columns(i).ColumnWidth = 24
                                        'Case 25 To 26
                                        '    cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                                        '    cell.Columns(i).ColumnWidth = 24
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

    Private Sub Generate_Excel_HPPTotalPcs()
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

            Dim JudulLaporan As String = "LAPORAN TOTAL HPP PRODUKSI PCS"

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
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Barang"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "PRO-REQ (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Good Issue (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Bahan Baku (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Packaging (PCS)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "Cost Production"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "HPP Finished Good"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "HPP Packaging Sekunder (Estimasi)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}},
            New Dictionary(Of String, String) From {{"Identifier", "Main"}, {"Kolom", "HPP (Estimasi)"}, {"Warna", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(243, 71, 96))}}
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
                    xlWorkSheet.Cells(3, indexAwal).Value = "Hasil Finished Good"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Range(xlWorkSheet.Columns(3), xlWorkSheet.Columns(i + 1)).EntireColumn.AutoFit()

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



                ElseIf kolom("Identifier") = "GR2" Then

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
                    xlWorkSheet.Cells(3, indexAwal).Value = "Hasil Rejected"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).EntireColumn.AutoFit()

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
                    xlWorkSheet.Cells(3, indexAwal).Value = "Hasil Scrap"
                    xlWorkSheet.Cells(3, indexAwal).HorizontalAlignment = excel.XlHAlign.xlHAlignCenter
                    xlWorkSheet.Cells(3, indexAwal).VerticalAlignment = excel.XlVAlign.xlVAlignCenter
                    xlWorkSheet.Range(xlWorkSheet.Cells(3, indexAwal), xlWorkSheet.Cells(3, indexAkhir)).EntireColumn.AutoFit()

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


                End If

                ' ROLE COLUMN
                Select Case i + 1
                    Case 9
                        If Not arrRoleColumn.Contains(RoleColumn_GI) Then
                            xlWorkSheet.Columns(9).Hidden = True
                        End If
                    Case 10
                        If Not arrRoleColumn.Contains(RoleColumn_BahanBaku) Then
                            xlWorkSheet.Columns(10).Hidden = True
                        End If
                    Case 11
                        If Not arrRoleColumn.Contains(RoleColumn_Packaging) Then
                            xlWorkSheet.Columns(11).Hidden = True
                        End If
                    Case 12
                        If Not arrRoleColumn.Contains(RoleColumn_CostProduction) Then
                            xlWorkSheet.Columns(12).Hidden = True
                        End If

                    Case 13
                        If Not arrRoleColumn.Contains(RoleColumn_FG) Then
                            xlWorkSheet.Columns(17).Hidden = True
                        End If

                    Case 14
                        If Not arrRoleColumn.Contains(RoleColumn_PackSekunder) Then
                            xlWorkSheet.Columns(19).Hidden = True
                        End If

                    Case 15
                        If Not arrRoleColumn.Contains(RoleColumn_HPP) Then
                            xlWorkSheet.Columns(21).Hidden = True
                        End If

                        'Case 15
                        '    If Not arrRoleColumn.Contains(RoleColumn_HPPFinal) Then
                        '        xlWorkSheet.Columns(20).Hidden = True
                        '    End If




                        'Case 13
                        '    If Not arrRoleColumn.Contains(RoleColumn_TotalHPP) Then
                        '        xlWorkSheet.Columns(13).Hidden = True
                        '    End If

                        'Case 14
                        '    If Not arrRoleColumn.Contains(RoleColumn_Scrap) Then
                        '        xlWorkSheet.Columns(14).Hidden = True
                        '    End If

                        'Case 15
                        '    If Not arrRoleColumn.Contains(RoleColumn_Waste) Then
                        '        xlWorkSheet.Columns(15).Hidden = True
                        '    End If

                        'Case 16
                        '    If Not arrRoleColumn.Contains(RoleColumn_Loss) Then
                        '        xlWorkSheet.Columns(16).Hidden = True
                        '    End If



                        'Case 18
                        '    If Not arrRoleColumn.Contains(RoleColumn_HppSementara) Then
                        '        xlWorkSheet.Columns(18).Hidden = True
                        '    End If


                End Select

            Next

#End Region

            '=========================
            '=     GENERATE BODY     =
            '=========================

            Dim stringCenter As New List(Of Integer) From {6, 7}

            Dim numberColumn As New List(Of Integer) From {8, 9, 10, 11, 12, 13, 14, 15, 16, 17}

            Dim NumberN0 As New List(Of Integer) From {10, 11}

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
                SQL = "select No_PO, No_Split, Tgl_Produksi, Jam_Produksi, Nama_Routing, Keterangan, Kode_Barang, Nama, Jumlah, Jumlah_Dosing, "
                SQL = SQL & "total_bahan_baku, Total_packaging_GR1 as Total_packaging, Total_Biaya_Produksi, HPP_Scrap, "
                SQL = SQL & "HPP_Waste, HPP_Loss, HPP_FinishedGood, HPP_Sementara, HPP_Packaging_Sekunder, HPP_Final, HPP_AVG as HPP_AVG_GR2_Finished_Good "
                SQL = SQL & "from N_EMI_Laporan_HPP_Produksi "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Tgl_Produksi between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                If Not Txt_IdRouting.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Id_Routing = '" & Txt_IdRouting.Text & "' "
                End If
                If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
                End If
                If Txt_NoSplit.Text.Trim.Length > 0 AndAlso Txt_NoSplit.Text.ToUpper <> OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and no_split like '%" & Txt_NoSplit.Text & "%' "
                End If
                SQL = SQL & "order by no_split, Tgl_Produksi, Jam_Produksi "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")

                        For i As Integer = 0 To .Rows.Count - 1

                            For colIndex As Integer = 0 To .Columns.Count - 1
                                Dim cell = xlWorkSheet.Cells(i + defaultRowIndex, colIndex + 1)
                                If colIndex = 6 Then
                                    cell.NumberFormat = "@"
                                End If

                                ' Format numerik (N0)
                                If numberColumn.Contains(colIndex) Then
                                    Dim nilai As Double = If(General_Class.CekNULL(.Rows(i).Item(colIndex)) = "", 0, .Rows(i).Item(colIndex))

                                    If NumberN0.Contains(colIndex) Then
                                        cell.NumberFormat = excelFormatN0
                                    Else
                                        cell.NumberFormat = excelFormat
                                    End If

                                    cell.Value = nilai
                                Else
                                    cell.Value = If(General_Class.CekNULL(.Rows(i).Item(colIndex)) = "", "-", General_Class.CekNULL(.Rows(i).Item(colIndex)))
                                End If

                                ' ROLE COLUMN
                                Select Case colIndex
                                    Case 9
                                        If Not arrRoleColumn.Contains(RoleColumn_GI) Then
                                            cell.Value = 0
                                        End If
                                    Case 10
                                        If Not arrRoleColumn.Contains(RoleColumn_BahanBaku) Then
                                            cell.Value = 0
                                        End If
                                    Case 11
                                        If Not arrRoleColumn.Contains(RoleColumn_Packaging) Then
                                            cell.Value = 0
                                        End If
                                    Case 12
                                        If Not arrRoleColumn.Contains(RoleColumn_CostProduction) Then
                                            cell.Value = 0
                                        End If

                                    Case 13
                                        If Not arrRoleColumn.Contains(RoleColumn_FG) Then
                                            cell.Value = 0
                                        End If

                                    Case 14
                                        If Not arrRoleColumn.Contains(RoleColumn_PackSekunder) Then
                                            cell.Value = 0
                                        End If

                                    Case 15
                                        If Not arrRoleColumn.Contains(RoleColumn_HPP) Then
                                            cell.Value = 0
                                        End If




                                        'Case 15
                                        '    If Not arrRoleColumn.Contains(RoleColumn_HPPFinal) Then
                                        '        cell.Value = 0
                                        '    End If
                                        'Case 13
                                        '    If Not arrRoleColumn.Contains(RoleColumn_Waste) Then
                                        '        cell.Value = 0
                                        '    End If

                                        'Case 14
                                        '    If Not arrRoleColumn.Contains(RoleColumn_Waste) Then
                                        '        cell.Value = 0
                                        '    End If

                                        'Case 15
                                        '    If Not arrRoleColumn.Contains(RoleColumn_Loss) Then
                                        '        cell.Value = 0
                                        '    End If



                                        'Case 17
                                        '    If Not arrRoleColumn.Contains(RoleColumn_HppSementara) Then
                                        '        cell.Value = 0
                                        '    End If


                                End Select

                                cell.VerticalAlignment = excel.XlVAlign.xlVAlignCenter


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
                                    Case 0 To 8
                                        cell.EntireColumn.AutoFit()
                                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
                                    Case 9 To 15
                                        cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(247, 183, 183))

                                        Select Case colIndex
                                            Case 9
                                                If Not arrRoleColumn.Contains(RoleColumn_GI) Then
                                                    cell.ColumnWidth = 0
                                                Else
                                                    cell.ColumnWidth = 24
                                                End If
                                            Case 10
                                                If Not arrRoleColumn.Contains(RoleColumn_BahanBaku) Then
                                                    cell.ColumnWidth = 0
                                                Else
                                                    cell.ColumnWidth = 24
                                                End If
                                            Case 11
                                                If Not arrRoleColumn.Contains(RoleColumn_Packaging) Then
                                                    cell.ColumnWidth = 0
                                                Else
                                                    cell.ColumnWidth = 24
                                                End If
                                            Case 12
                                                If Not arrRoleColumn.Contains(RoleColumn_CostProduction) Then
                                                    cell.ColumnWidth = 0
                                                Else
                                                    cell.ColumnWidth = 24
                                                End If

                                            Case 13
                                                If Not arrRoleColumn.Contains(RoleColumn_FG) Then
                                                    cell.ColumnWidth = 0
                                                Else
                                                    cell.ColumnWidth = 24
                                                End If

                                            Case 14
                                                If Not arrRoleColumn.Contains(RoleColumn_PackSekunder) Then
                                                    cell.ColumnWidth = 0
                                                Else
                                                    cell.ColumnWidth = 24
                                                End If

                                            Case 15
                                                If Not arrRoleColumn.Contains(RoleColumn_HPP) Then
                                                    cell.ColumnWidth = 0
                                                Else
                                                    cell.ColumnWidth = 24
                                                End If


                                                'Case 19
                                                '    If Not arrRoleColumn.Contains(RoleColumn_HPPFinal) Then
                                                '        cell.ColumnWidth = 0
                                                '    Else
                                                '        cell.ColumnWidth = 24
                                                '    End If


                                                'Case 13
                                                '    If Not arrRoleColumn.Contains(RoleColumn_Scrap) Then
                                                '        cell.ColumnWidth = 0
                                                '    Else
                                                '        cell.ColumnWidth = 24
                                                '    End If

                                                'Case 14
                                                '    If Not arrRoleColumn.Contains(RoleColumn_Waste) Then
                                                '        cell.ColumnWidth = 0
                                                '    Else
                                                '        cell.ColumnWidth = 24
                                                '    End If

                                                'Case 15
                                                '    If Not arrRoleColumn.Contains(RoleColumn_Loss) Then
                                                '        cell.ColumnWidth = 0
                                                '    Else
                                                '        cell.ColumnWidth = 24
                                                '    End If



                                                'Case 17
                                                '    If Not arrRoleColumn.Contains(RoleColumn_HppSementara) Then
                                                '        cell.ColumnWidth = 0
                                                '    Else
                                                '        cell.ColumnWidth = 24
                                                '    End If




                                        End Select

                                        'Case 16 To 18
                                        '    cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow)
                                        '    cell.Columns(i).ColumnWidth = 24
                                        'Case 19 To 21
                                        '    cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen)
                                        '    cell.Columns(i).ColumnWidth = 24
                                        'Case 22 To 24
                                        '    cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
                                        '    cell.Columns(i).ColumnWidth = 24
                                        'Case 25 To 26
                                        '    cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue)
                                        '    cell.Columns(i).ColumnWidth = 24
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



    Private Sub Dgv_Rekap2_Scroll(sender As Object, e As ScrollEventArgs) Handles Dgv_Rekap2.Scroll
        If e.ScrollOrientation = ScrollOrientation.HorizontalScroll Then
            Dgv_Total.HorizontalScrollingOffset = Dgv_Rekap2.HorizontalScrollingOffset
        End If
    End Sub

    Private Sub Dgv_Rekap2_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles Dgv_Rekap2.ColumnWidthChanged
        SyncColumnWidths()
    End Sub
    Private Sub Dgv_Rekap2_SizeChanged(sender As Object, e As EventArgs) Handles Dgv_Rekap2.SizeChanged
        SyncColumnWidths()
    End Sub
    Private Sub N_EMI_Laporan_HPP_Produksi_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Dgv_Total.HorizontalScrollingOffset = Dgv_Rekap2.HorizontalScrollingOffset
        SyncColumnWidths()
    End Sub

    Private Sub SyncColumnWidths()
        'Dim minColumnCount As Integer = Math.Min(Dgv_Rekap2.Columns.Count, Dgv_Total.Columns.Count)
        'For i As Integer = 0 To minColumnCount - 1
        '    Dgv_Total.Columns(i).Width = Dgv_Rekap2.Columns(i).Width
        'Next
    End Sub



End Class