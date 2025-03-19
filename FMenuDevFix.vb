Public Class FMenuDevFix

    Private Sub FMenuDevFix_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub FMenuDevFix_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        'Automation_Forecast_Release()

        Try
            OpenConn()

            Using Dr = OpenTrans("select dateadd(hh, " & selisihjam & ", getdate()) as Jam")
                If Dr.Read Then
                    ToolStripStatusLabel3.Text = Format(Dr("jam"), "yyyy-MM-dd HH:mm:ss")
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        ToolStripStatusLabel1.Text = "Login : " & UserID
        ToolStripStatusLabel4.Text = "Lokasi : " & Lokasi

        Timer1_Tick(Me, Nothing)

        Dim C As Control

        For Each C In Me.Controls
            If TypeOf C Is MdiClient Then
                C.BackColor = Color.LightGray
                Exit For
            End If
        Next

        C = Nothing
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ToolStripStatusLabel3.Text = Format(DateAdd(DateInterval.Second, 1, CDate(ToolStripStatusLabel3.Text)), "yyyy-MM-dd HH:mm:ss")
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Try
            OpenConn()

            Using Dr = OpenTrans("select dateadd(hh, " & selisihjam & ", getdate()) as Jam")
                If Dr.Read Then
                    'ToolStripStatusLabel3.Text = Format(Dr("jam"), "dd MMM yyyy HH:mm:ss")
                    ToolStripStatusLabel3.Text = Format(Dr("jam"), "yyyy-MM-dd HH:mm:ss")
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    '=====================
    '=     LOAD MENU     =
    '=====================

    Private Sub InputMenuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InputMenuToolStripMenuItem.Click
        Master_Menu2.StartPosition = FormStartPosition.CenterScreen

        Master_Menu2.MdiParent = Me
        Master_Menu2.Show()
        Master_Menu2.Focus()
    End Sub

    Private Sub InputRoleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InputRoleToolStripMenuItem.Click
        Master_Role.StartPosition = FormStartPosition.CenterScreen

        Master_Role.MdiParent = Me
        Master_Role.Show()
        Master_Role.Focus()
    End Sub


    '== MASTER MENU =='
    Private Sub MasterEkspedisiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterEkspedisiToolStripMenuItem.Click
        Master_Ekspedisi.StartPosition = FormStartPosition.CenterScreen

        Master_Ekspedisi.MdiParent = Me
        Master_Ekspedisi.Show()
        Master_Ekspedisi.Focus()
    End Sub

    Private Sub MasterSelisihJenisToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterSelisihJenisToolStripMenuItem.Click
        Master_Jenis_Selisih.StartPosition = FormStartPosition.CenterScreen

        Master_Jenis_Selisih.MdiParent = Me
        Master_Jenis_Selisih.Show()
        Master_Jenis_Selisih.Focus()
    End Sub

    Private Sub MasterKategoriHargaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterKategoriHargaToolStripMenuItem.Click
        Master_Kategori_Harga.StartPosition = FormStartPosition.CenterScreen

        Master_Kategori_Harga.MdiParent = Me
        Master_Kategori_Harga.Show()
        Master_Kategori_Harga.Focus()
    End Sub

    Private Sub MasterKategoriHargaDetailToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterKategoriHargaDetailToolStripMenuItem.Click
        Master_Kategori_Harga_Detail.StartPosition = FormStartPosition.CenterScreen

        Master_Kategori_Harga_Detail.MdiParent = Me
        Master_Kategori_Harga_Detail.Show()
        Master_Kategori_Harga_Detail.Focus()
    End Sub

    Private Sub MasterKateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterKateToolStripMenuItem.Click
        Master_Kategori_PO.StartPosition = FormStartPosition.CenterScreen

        Master_Kategori_PO.MdiParent = Me
        Master_Kategori_PO.Show()
        Master_Kategori_PO.Focus()
    End Sub

    Private Sub MasterKategoriPORoleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterKategoriPORoleToolStripMenuItem.Click
        Master_Kategori_PO_Role.StartPosition = FormStartPosition.CenterScreen

        Master_Kategori_PO_Role.MdiParent = Me
        Master_Kategori_PO_Role.Show()
        Master_Kategori_PO_Role.Focus()
    End Sub

    Private Sub MasterMediaKirimToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterMediaKirimToolStripMenuItem.Click
        Master_Media_Kirim.StartPosition = FormStartPosition.CenterScreen

        Master_Media_Kirim.MdiParent = Me
        Master_Media_Kirim.Show()
        Master_Media_Kirim.Focus()
    End Sub

    Private Sub MasterJatuhTempoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterJatuhTempoToolStripMenuItem.Click
        Master_Perhitungan_Jatuh_Tempo.StartPosition = FormStartPosition.CenterScreen

        Master_Perhitungan_Jatuh_Tempo.MdiParent = Me
        Master_Perhitungan_Jatuh_Tempo.Show()
        Master_Perhitungan_Jatuh_Tempo.Focus()
    End Sub

    Private Sub MasterSupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterSupplierToolStripMenuItem.Click
        Master_Suppliers.StartPosition = FormStartPosition.CenterScreen

        Master_Suppliers.MdiParent = Me
        Master_Suppliers.Show()
        Master_Suppliers.Focus()
    End Sub

    '== TRANSAKSI =='
    '== Penawaran =='
    Private Sub PenawaranToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PenawaranToolStripMenuItem1.Click
        Transaksi_Penawaran.StartPosition = FormStartPosition.CenterScreen

        Transaksi_Penawaran.MdiParent = Me
        Transaksi_Penawaran.Show()
        Transaksi_Penawaran.Focus()
    End Sub

    '== Pembelian =='
    Private Sub POToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles POToolStripMenuItem.Click
        EMI_PO_Pembelian_Display.StartPosition = FormStartPosition.CenterScreen

        EMI_PO_Pembelian_Display.MdiParent = Me
        EMI_PO_Pembelian_Display.Show()
        EMI_PO_Pembelian_Display.Focus()
    End Sub

    Private Sub PurcahseRequisitionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PurcahseRequisitionToolStripMenuItem.Click
        Purchase_Requisition.StartPosition = FormStartPosition.CenterScreen

        Purchase_Requisition.MdiParent = Me
        Purchase_Requisition.Show()
        Purchase_Requisition.Focus()
    End Sub

    '== Pelunasan =='
    Private Sub PelunasanBiayaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelunasanBiayaToolStripMenuItem.Click
        EMI_Pelunasan.StartPosition = FormStartPosition.CenterScreen

        EMI_Pelunasan.MdiParent = Me
        EMI_Pelunasan.Show()
        EMI_Pelunasan.Focus()
    End Sub

    Private Sub PembayaranDimukaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PembayaranDimukaToolStripMenuItem.Click
        EMI_DownPayment.StartPosition = FormStartPosition.CenterScreen

        EMI_DownPayment.MdiParent = Me
        EMI_DownPayment.Show()
        EMI_DownPayment.Focus()
    End Sub

    Private Sub BindingPembayaranDimukaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BindingPembayaranDimukaToolStripMenuItem.Click
        EMI_DownPayment_Binding.StartPosition = FormStartPosition.CenterScreen

        EMI_DownPayment_Binding.MdiParent = Me
        EMI_DownPayment_Binding.Show()
        EMI_DownPayment_Binding.Focus()
    End Sub

    '== DISPLAY =='
    '== Penawaran =='
    Private Sub PenawaranToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles PenawaranToolStripMenuItem3.Click
        EMI_Penawaran_Harga_Summary_Data.StartPosition = FormStartPosition.CenterScreen

        EMI_Penawaran_Harga_Summary_Data.MdiParent = Me
        EMI_Penawaran_Harga_Summary_Data.Show()
        EMI_Penawaran_Harga_Summary_Data.Focus()
    End Sub

    Private Sub PenawaranBerakhirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenawaranBerakhirToolStripMenuItem.Click
        Emi_Display_Barang_Penawaran.StartPosition = FormStartPosition.CenterScreen

        Emi_Display_Barang_Penawaran.MdiParent = Me
        Emi_Display_Barang_Penawaran.Show()
        Emi_Display_Barang_Penawaran.Focus()
    End Sub

    '== Pembelian =='
    Private Sub PurchaseOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PurchaseOrderToolStripMenuItem.Click
        EMI_Pembelian_PO_Summary_Data.StartPosition = FormStartPosition.CenterScreen

        EMI_Pembelian_PO_Summary_Data.MdiParent = Me
        EMI_Pembelian_PO_Summary_Data.Show()
        EMI_Pembelian_PO_Summary_Data.Focus()
    End Sub

    Private Sub PurchaseRequisitionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PurchaseRequisitionToolStripMenuItem.Click
        EMI_Pembelian_PR_Summary_Data.StartPosition = FormStartPosition.CenterScreen

        EMI_Pembelian_PR_Summary_Data.MdiParent = Me
        EMI_Pembelian_PR_Summary_Data.Show()
        EMI_Pembelian_PR_Summary_Data.Focus()
    End Sub

    Private Sub HPPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HPPToolStripMenuItem.Click
        EMI_Display_HPP.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_HPP.MdiParent = Me
        EMI_Display_HPP.Show()
        EMI_Display_HPP.Focus()
    End Sub

    '== Pelunasan =='
    Private Sub PelunasanToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles PelunasanToolStripMenuItem2.Click
        Display_Emi_Pelunasan.StartPosition = FormStartPosition.CenterScreen

        Display_Emi_Pelunasan.MdiParent = Me
        Display_Emi_Pelunasan.Show()
        Display_Emi_Pelunasan.Focus()
    End Sub

    Private Sub PelunasanHutangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelunasanHutangToolStripMenuItem.Click
        Display_Emi_Pelunasan_Hutang.StartPosition = FormStartPosition.CenterScreen

        Display_Emi_Pelunasan_Hutang.MdiParent = Me
        Display_Emi_Pelunasan_Hutang.Show()
        Display_Emi_Pelunasan_Hutang.Focus()
    End Sub

    Private Sub SupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupplierToolStripMenuItem.Click
        'EMI_Hutang_PO_Summary_Data.StartPosition = FormStartPosition.CenterScreen

        'EMI_Hutang_PO_Summary_Data.MdiParent = Me
        'EMI_Hutang_PO_Summary_Data.Show()
        'EMI_Hutang_PO_Summary_Data.Focus()
    End Sub

    Private Sub AgentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AgentToolStripMenuItem.Click
        'Laporan_Hutang_Biaya_Import_Summary_Data.StartPosition = FormStartPosition.CenterScreen

        'Laporan_Hutang_Biaya_Import_Summary_Data.MdiParent = Me
        'Laporan_Hutang_Biaya_Import_Summary_Data.Show()
        'Laporan_Hutang_Biaya_Import_Summary_Data.Focus()
    End Sub

    '== LAPORAN =='

    Private Sub LaporanPurchaseOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanPurchaseOrderToolStripMenuItem.Click
        Laporan_Purchase_Order.StartPosition = FormStartPosition.CenterScreen

        Laporan_Purchase_Order.MdiParent = Me
        Laporan_Purchase_Order.Show()
        Laporan_Purchase_Order.Focus()
    End Sub

    Private Sub LaporanPurchaseRequisitionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanPurchaseRequisitionToolStripMenuItem.Click
        Laporan_Purchase_Requisition.StartPosition = FormStartPosition.CenterScreen

        Laporan_Purchase_Requisition.MdiParent = Me
        Laporan_Purchase_Requisition.Show()
        Laporan_Purchase_Requisition.Focus()
    End Sub

    Private Sub HPPImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HPPImportToolStripMenuItem.Click
        Laporan_HPP_Import.StartPosition = FormStartPosition.CenterScreen

        Laporan_HPP_Import.MdiParent = Me
        Laporan_HPP_Import.Show()
        Laporan_HPP_Import.Focus()
    End Sub

    Private Sub HPPLokalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HPPLokalToolStripMenuItem.Click
        Laporan_HPP_Local.StartPosition = FormStartPosition.CenterScreen

        Laporan_HPP_Local.MdiParent = Me
        Laporan_HPP_Local.Show()
        Laporan_HPP_Local.Focus()
    End Sub

    Private Sub PembelianToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles PembelianToolStripMenuItem3.Click

        EMI_PO_Pembelian_Display2.StartPosition = FormStartPosition.CenterScreen

        EMI_PO_Pembelian_Display2.MdiParent = Me
        EMI_PO_Pembelian_Display2.Show()
        EMI_PO_Pembelian_Display2.Focus()
    End Sub

    Private Sub ValidasiBarangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiBarangMasukToolStripMenuItem.Click
        Emi_Selisih_Barang_Masuk_Display.StartPosition = FormStartPosition.CenterScreen

        Emi_Selisih_Barang_Masuk_Display.MdiParent = Me
        Emi_Selisih_Barang_Masuk_Display.Show()
        Emi_Selisih_Barang_Masuk_Display.Focus()
    End Sub

    Private Sub OrderProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrderProduksiToolStripMenuItem.Click
        EMI_Production_Order.StartPosition = FormStartPosition.CenterScreen

        EMI_Production_Order.MdiParent = Me
        EMI_Production_Order.Show()
        EMI_Production_Order.Focus()
    End Sub

    Private Sub OrderIndependentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrderIndependentToolStripMenuItem.Click
        EMI_Independent_Order.StartPosition = FormStartPosition.CenterScreen

        EMI_Independent_Order.MdiParent = Me
        EMI_Independent_Order.Show()
        EMI_Independent_Order.Focus()
    End Sub

    Private Sub JadwalProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JadwalProduksiToolStripMenuItem.Click
        EMI_Schedule.StartPosition = FormStartPosition.CenterScreen

        EMI_Schedule.MdiParent = Me
        EMI_Schedule.Show()
        EMI_Schedule.Focus()
    End Sub

    Private Sub CompareWorkCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompareWorkCenterToolStripMenuItem.Click
        EMI_Compare_Work_Center.StartPosition = FormStartPosition.CenterScreen

        EMI_Compare_Work_Center.MdiParent = Me
        EMI_Compare_Work_Center.Show()
        EMI_Compare_Work_Center.Focus()
    End Sub

    Private Sub HPPProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HPPProduksiToolStripMenuItem.Click
        EMI_HPP_Production.StartPosition = FormStartPosition.CenterScreen

        EMI_HPP_Production.MdiParent = Me
        EMI_HPP_Production.Show()
        EMI_HPP_Production.Focus()
    End Sub

    Private Sub ActualBiayaProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualBiayaProduksiToolStripMenuItem.Click
        EMI_Transaksi_Actual_Biaya_Produksi.StartPosition = FormStartPosition.CenterScreen

        EMI_Transaksi_Actual_Biaya_Produksi.MdiParent = Me
        EMI_Transaksi_Actual_Biaya_Produksi.Show()
        EMI_Transaksi_Actual_Biaya_Produksi.Focus()
    End Sub

    Private Sub WorkCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WorkCenterToolStripMenuItem.Click
        EMI_Transaksi_Work_Center.StartPosition = FormStartPosition.CenterScreen

        EMI_Transaksi_Work_Center.MdiParent = Me
        EMI_Transaksi_Work_Center.Show()
        EMI_Transaksi_Work_Center.Focus()
    End Sub

    Private Sub WorkCenterPerbulanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WorkCenterPerbulanToolStripMenuItem.Click
        EMI_Transaksi_Work_Center_PerBulan.StartPosition = FormStartPosition.CenterScreen

        EMI_Transaksi_Work_Center_PerBulan.MdiParent = Me
        EMI_Transaksi_Work_Center_PerBulan.Show()
        EMI_Transaksi_Work_Center_PerBulan.Focus()
    End Sub

    Private Sub ProduksiToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ProduksiToolStripMenuItem1.Click
        EMI_Display_Mulai_Produksi.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Mulai_Produksi.MdiParent = Me
        EMI_Display_Mulai_Produksi.Show()
        EMI_Display_Mulai_Produksi.Focus()
    End Sub

    Private Sub RequestMaterialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RequestMaterialToolStripMenuItem.Click
        Emi_Request_Material_Display.StartPosition = FormStartPosition.CenterScreen

        Emi_Request_Material_Display.MdiParent = Me
        Emi_Request_Material_Display.Show()
        Emi_Request_Material_Display.Focus()
    End Sub

    Private Sub TransferQualityToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferQualityToolStripMenuItem.Click
        Emi_Transfer_Quality_Production.StartPosition = FormStartPosition.CenterScreen

        Emi_Transfer_Quality_Production.MdiParent = Me
        Emi_Transfer_Quality_Production.Show()
        Emi_Transfer_Quality_Production.Focus()
    End Sub

    Private Sub HasilPengeluaranBahanBakuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HasilPengeluaranBahanBakuToolStripMenuItem.Click
        EMI_Display_Pengeluaran_Bahan_Baku.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Pengeluaran_Bahan_Baku.MdiParent = Me
        EMI_Display_Pengeluaran_Bahan_Baku.Show()
        EMI_Display_Pengeluaran_Bahan_Baku.Focus()
    End Sub

    Private Sub HasilProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HasilProduksiToolStripMenuItem.Click
        EMI_Display_Hasil_Produksi.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Hasil_Produksi.MdiParent = Me
        EMI_Display_Hasil_Produksi.Show()
        EMI_Display_Hasil_Produksi.Focus()
    End Sub

    Private Sub HasilProduksiFGToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HasilProduksiFGToolStripMenuItem.Click
        EMI_Display_Hasil_ProduksiFG.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Hasil_ProduksiFG.MdiParent = Me
        EMI_Display_Hasil_ProduksiFG.Show()
        EMI_Display_Hasil_ProduksiFG.Focus()
    End Sub

    Private Sub BarcodeProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarcodeProduksiToolStripMenuItem.Click
        Emi_Production_Barcode.StartPosition = FormStartPosition.CenterScreen

        Emi_Production_Barcode.MdiParent = Me
        Emi_Production_Barcode.Show()
        Emi_Production_Barcode.Focus()
    End Sub

    Private Sub ActualBiayaProduksiToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ActualBiayaProduksiToolStripMenuItem1.Click
        EMI_Transaksi_Actual_Biaya_Produksi_Display.StartPosition = FormStartPosition.CenterScreen

        EMI_Transaksi_Actual_Biaya_Produksi_Display.MdiParent = Me
        EMI_Transaksi_Actual_Biaya_Produksi_Display.Show()
        EMI_Transaksi_Actual_Biaya_Produksi_Display.Focus()
    End Sub

    Private Sub HasilProduksiToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HasilProduksiToolStripMenuItem1.Click
        EMI_Display_Production_Result.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Production_Result.MdiParent = Me
        EMI_Display_Production_Result.Show()
        EMI_Display_Production_Result.Focus()
    End Sub

    Private Sub SplitProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SplitProduksiToolStripMenuItem.Click
        EMI_Display_Split_Production_Order.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Split_Production_Order.MdiParent = Me
        EMI_Display_Split_Production_Order.Show()
        EMI_Display_Split_Production_Order.Focus()
    End Sub

    Private Sub RequestMaterialToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RequestMaterialToolStripMenuItem1.Click
        EMI_Request_Material_List.StartPosition = FormStartPosition.CenterScreen

        EMI_Request_Material_List.MdiParent = Me
        EMI_Request_Material_List.Show()
        EMI_Request_Material_List.Focus()
    End Sub

    Private Sub OrderProduksiToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OrderProduksiToolStripMenuItem1.Click
        EMI_Production_Order_Summary_Data.StartPosition = FormStartPosition.CenterScreen

        EMI_Production_Order_Summary_Data.MdiParent = Me
        EMI_Production_Order_Summary_Data.Show()
        EMI_Production_Order_Summary_Data.Focus()
    End Sub

    Private Sub ActualBiayaProduksiToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ActualBiayaProduksiToolStripMenuItem2.Click
        Laporan_Actual_Biaya_Produksi.StartPosition = FormStartPosition.CenterScreen

        Laporan_Actual_Biaya_Produksi.MdiParent = Me
        Laporan_Actual_Biaya_Produksi.Show()
        Laporan_Actual_Biaya_Produksi.Focus()
    End Sub

    Private Sub LaporanGIGRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanGIGRToolStripMenuItem.Click
        Laporan_GI_GR.StartPosition = FormStartPosition.CenterScreen

        Laporan_GI_GR.MdiParent = Me
        Laporan_GI_GR.Show()
        Laporan_GI_GR.Focus()
    End Sub

    Private Sub LaporanPenggunaanBahanBakuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanPenggunaanBahanBakuToolStripMenuItem.Click
        Laporan_Summary_Usage_RM.StartPosition = FormStartPosition.CenterScreen

        Laporan_Summary_Usage_RM.MdiParent = Me
        Laporan_Summary_Usage_RM.Show()
        Laporan_Summary_Usage_RM.Focus()
    End Sub

    Private Sub TesPrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TesPrintToolStripMenuItem.Click
        TesPrint.StartPosition = FormStartPosition.CenterScreen

        TesPrint.MdiParent = Me
        TesPrint.Show()
        TesPrint.Focus()
    End Sub

    Private Sub SetPRinterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetPRinterToolStripMenuItem.Click
        Global_Setting.StartPosition = FormStartPosition.CenterScreen

        Global_Setting.MdiParent = Me
        Global_Setting.Show()
        Global_Setting.Focus()
    End Sub

    Private Sub AsdaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AsdaToolStripMenuItem.Click
        EMI_Display_Transfer_Tidak_Timbang.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Transfer_Tidak_Timbang.MdiParent = Me
        EMI_Display_Transfer_Tidak_Timbang.Show()
        EMI_Display_Transfer_Tidak_Timbang.Focus()
    End Sub

    Private Sub HPPToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HPPToolStripMenuItem1.Click
        Laporan_HPP.StartPosition = FormStartPosition.CenterScreen

        Laporan_HPP.MdiParent = Me
        Laporan_HPP.Show()
        Laporan_HPP.Focus()
    End Sub

    Private Sub TransferQualityToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles TransferQualityToolStripMenuItem1.Click
        EMI_Display_QC.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_QC.MdiParent = Me
        EMI_Display_QC.Show()
        EMI_Display_QC.Focus()
    End Sub

    Private Sub DisplayForecastOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayForecastOrderToolStripMenuItem.Click
        EMI_Display_ForecastOrder.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_ForecastOrder.MdiParent = Me
        EMI_Display_ForecastOrder.Show()
        EMI_Display_ForecastOrder.Focus()
    End Sub

    Private Sub DisplayLogForecastToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayLogForecastToolStripMenuItem.Click
        EMI_Display_Log_ForecastOrder.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Log_ForecastOrder.MdiParent = Me
        EMI_Display_Log_ForecastOrder.Show()
        EMI_Display_Log_ForecastOrder.Focus()
    End Sub

    Private Sub DisplayLogMaterialRequisitionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayLogMaterialRequisitionToolStripMenuItem.Click
        EMI_Display_Log_MaterialRequisition.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Log_MaterialRequisition.MdiParent = Me
        EMI_Display_Log_MaterialRequisition.Show()
        EMI_Display_Log_MaterialRequisition.Focus()
    End Sub

    Private Sub DisplayOrderPlanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayOrderPlanToolStripMenuItem.Click
        EMI_Display_OrderPlan.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_OrderPlan.MdiParent = Me
        EMI_Display_OrderPlan.Show()
        EMI_Display_OrderPlan.Focus()
    End Sub

    Private Sub ForecastOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ForecastOrderToolStripMenuItem.Click
        EMI_Transaksi_ForecastOrder.StartPosition = FormStartPosition.CenterScreen

        EMI_Transaksi_ForecastOrder.MdiParent = Me
        EMI_Transaksi_ForecastOrder.Show()
        EMI_Transaksi_ForecastOrder.Focus()
    End Sub

    Private Sub SalesForecastingByPPICToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalesForecastingByPPICToolStripMenuItem.Click
        EMI_Transaksi_ForecastOrder.StartPosition = FormStartPosition.CenterScreen

        EMI_Transaksi_ForecastOrder.MdiParent = Me
        EMI_Transaksi_ForecastOrder.Show()
        EMI_Transaksi_ForecastOrder.Focus()
    End Sub

    Private Sub MaterialRequisitionByPPICToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MaterialRequisitionByPPICToolStripMenuItem.Click
        EMI_Transaksi_MaterialRequisition.StartPosition = FormStartPosition.CenterScreen

        EMI_Transaksi_MaterialRequisition.MdiParent = Me
        EMI_Transaksi_MaterialRequisition.Show()
        EMI_Transaksi_MaterialRequisition.Focus()
    End Sub

    Private Sub MaterialRequisitionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MaterialRequisitionToolStripMenuItem.Click
        EMI_Transaksi_MaterialRequisition.StartPosition = FormStartPosition.CenterScreen

        EMI_Transaksi_MaterialRequisition.MdiParent = Me
        EMI_Transaksi_MaterialRequisition.Show()
        EMI_Transaksi_MaterialRequisition.Focus()
    End Sub

    Private Sub DisplayTransaksiBiayaLokalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayTransaksiBiayaLokalToolStripMenuItem.Click
        EMI_Display_Transaksi_Biaya_Lokal.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Transaksi_Biaya_Lokal.MdiParent = Me
        EMI_Display_Transaksi_Biaya_Lokal.Show()
        EMI_Display_Transaksi_Biaya_Lokal.Focus()
    End Sub

    Private Sub AsdasdasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AsdasdasToolStripMenuItem.Click
        Transfer_Stock_QC.StartPosition = FormStartPosition.CenterScreen

        Transfer_Stock_QC.MdiParent = Me
        Transfer_Stock_QC.Show()
        Transfer_Stock_QC.Focus()
    End Sub

    Private Sub BudgetingWorkCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BudgetingWorkCenterToolStripMenuItem.Click
        EMI_Budgeting_Work_Center.StartPosition = FormStartPosition.CenterScreen

        EMI_Budgeting_Work_Center.MdiParent = Me
        EMI_Budgeting_Work_Center.Show()
        EMI_Budgeting_Work_Center.Focus()
    End Sub

    Private Sub TimbangUNloadingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TimbangUNloadingToolStripMenuItem.Click
        Emi_Display_Timbang_FloorScale.StartPosition = FormStartPosition.CenterScreen

        Emi_Display_Timbang_FloorScale.MdiParent = Me
        Emi_Display_Timbang_FloorScale.Show()
        Emi_Display_Timbang_FloorScale.Focus()
    End Sub

    Private Sub MasterMesinToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterMesinToolStripMenuItem.Click
        Master_Mesin.StartPosition = FormStartPosition.CenterScreen

        Master_Mesin.MdiParent = Me
        Master_Mesin.Show()
        Master_Mesin.Focus()
    End Sub

    Private Sub MasterWorkCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterWorkCenterToolStripMenuItem.Click
        Master_Work_Center.StartPosition = FormStartPosition.CenterScreen

        Master_Work_Center.MdiParent = Me
        Master_Work_Center.Show()
        Master_Work_Center.Focus()
    End Sub

    Private Sub SplitStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SplitStockToolStripMenuItem.Click
        'Emi_Split_Stock_QC.StartPosition = FormStartPosition.CenterScreen

        'Emi_Split_Stock_QC.MdiParent = Me
        'Emi_Split_Stock_QC.Show()
        'Emi_Split_Stock_QC.Focus()
    End Sub

    Private Sub DisplaySplitStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplaySplitStockToolStripMenuItem.Click
        Emi_Display_Tf_Stock_QC.StartPosition = FormStartPosition.CenterScreen

        Emi_Display_Tf_Stock_QC.MdiParent = Me
        Emi_Display_Tf_Stock_QC.Show()
        Emi_Display_Tf_Stock_QC.Focus()
    End Sub

    Private Sub ValidasiHPPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiHPPToolStripMenuItem.Click
        EMI_Display_Validasi_HPP_Produksi.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Validasi_HPP_Produksi.MdiParent = Me
        EMI_Display_Validasi_HPP_Produksi.Show()
        EMI_Display_Validasi_HPP_Produksi.Focus()
    End Sub

    Private Sub TimbangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TimbangToolStripMenuItem.Click
        Emi_Display_Transfer.StartPosition = FormStartPosition.CenterScreen

        Emi_Display_Transfer.MdiParent = Me
        Emi_Display_Transfer.Show()
        Emi_Display_Transfer.Focus()
    End Sub

    Private Sub GlobalSettingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GlobalSettingToolStripMenuItem.Click
        Global_Setting.StartPosition = FormStartPosition.CenterScreen

        Global_Setting.MdiParent = Me
        Global_Setting.Show()
        Global_Setting.Focus()
    End Sub

    Private Sub TransferTableDatabaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferTableDatabaseToolStripMenuItem.Click
        SyncMenus.StartPosition = FormStartPosition.CenterScreen

        SyncMenus.MdiParent = Me
        SyncMenus.Show()
        SyncMenus.Focus()
    End Sub

    Private Sub IncommingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IncommingToolStripMenuItem.Click
        EMI_Display_Timbang.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Timbang.MdiParent = Me
        EMI_Display_Timbang.Show()
        EMI_Display_Timbang.Focus()
    End Sub

    Private Sub DisplayPalletMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayPalletMasukToolStripMenuItem.Click
        EMI_Display_Pallet_Masuk.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Pallet_Masuk.MdiParent = Me
        EMI_Display_Pallet_Masuk.Show()
        EMI_Display_Pallet_Masuk.Focus()
    End Sub

    Private Sub CetakUlangPalletMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangPalletMasukToolStripMenuItem.Click
        EMI_Display_Pallet_Masuk_Data.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Pallet_Masuk_Data.MdiParent = Me
        EMI_Display_Pallet_Masuk_Data.Show()
        EMI_Display_Pallet_Masuk_Data.Focus()
    End Sub

    Private Sub PembayaranBiayaProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PembayaranBiayaProduksiToolStripMenuItem.Click
        Pembayaran_Biaya_Produksi.StartPosition = FormStartPosition.CenterScreen

        Pembayaran_Biaya_Produksi.MdiParent = Me
        Pembayaran_Biaya_Produksi.Show()
        Pembayaran_Biaya_Produksi.Focus()
    End Sub


    Private Sub MToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MToolStripMenuItem.Click
        EMI_Master_Meteran.StartPosition = FormStartPosition.CenterScreen

        EMI_Master_Meteran.MdiParent = Me
        EMI_Master_Meteran.Show()
        EMI_Master_Meteran.Focus()
    End Sub

    Private Sub BindingMeteranToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BindingMeteranToolStripMenuItem.Click
        EMI_Binding_Meteran.StartPosition = FormStartPosition.CenterScreen

        EMI_Binding_Meteran.MdiParent = Me
        EMI_Binding_Meteran.Show()
        EMI_Binding_Meteran.Focus()
    End Sub

    Private Sub PersentaseBudgetingWorkCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PersentaseBudgetingWorkCenterToolStripMenuItem.Click
        EMI_Persentase_Budgeting_WorkCenter.StartPosition = FormStartPosition.CenterScreen

        EMI_Persentase_Budgeting_WorkCenter.MdiParent = Me
        EMI_Persentase_Budgeting_WorkCenter.Show()
        EMI_Persentase_Budgeting_WorkCenter.Focus()
    End Sub

    Private Sub ValidasiBudgetWorkCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiBudgetWorkCenterToolStripMenuItem.Click
        EMI_Validasi_Budget_Work_Center.StartPosition = FormStartPosition.CenterScreen

        EMI_Validasi_Budget_Work_Center.MdiParent = Me
        EMI_Validasi_Budget_Work_Center.Show()
        EMI_Validasi_Budget_Work_Center.Focus()
    End Sub

    Private Sub DisplayValidasiBudgetWorkCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayValidasiBudgetWorkCenterToolStripMenuItem.Click
        Display_Validasi_Budget_Work_Center.StartPosition = FormStartPosition.CenterScreen

        Display_Validasi_Budget_Work_Center.MdiParent = Me
        Display_Validasi_Budget_Work_Center.Show()
        Display_Validasi_Budget_Work_Center.Focus()
    End Sub

    Private Sub MasterJenisBiayaProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterJenisBiayaProduksiToolStripMenuItem.Click
        Master_Jenis_Biaya_Produksi.StartPosition = FormStartPosition.CenterScreen

        Master_Jenis_Biaya_Produksi.MdiParent = Me
        Master_Jenis_Biaya_Produksi.Show()
        Master_Jenis_Biaya_Produksi.Focus()
    End Sub

    Private Sub DisplayBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayBarangToolStripMenuItem.Click
        Display_Barang.StartPosition = FormStartPosition.CenterScreen

        Display_Barang.MdiParent = Me
        Display_Barang.Show()
        Display_Barang.Focus()
    End Sub

    Private Sub TfMaterialToMaterialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TfMaterialToMaterialToolStripMenuItem.Click
        Tf_Material_To_Material.StartPosition = FormStartPosition.CenterScreen

        Tf_Material_To_Material.MdiParent = Me
        Tf_Material_To_Material.Show()
        Tf_Material_To_Material.Focus()
    End Sub

    Private Sub ValidasiMaterialToMaterialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiMaterialToMaterialToolStripMenuItem.Click
        'EMI_Validasi_Tf_Material_To_Material.StartPosition = FormStartPosition.CenterScreen

        'EMI_Validasi_Tf_Material_To_Material.MdiParent = Me
        'EMI_Validasi_Tf_Material_To_Material.Show()
        'EMI_Validasi_Tf_Material_To_Material.Focus()
    End Sub

    Private Sub TransferStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferStockToolStripMenuItem.Click
        Transfer_Stock_3.StartPosition = FormStartPosition.CenterScreen

        Transfer_Stock_3.MdiParent = Me
        Transfer_Stock_3.Show()
        Transfer_Stock_3.Focus()
    End Sub

    Private Sub SummaryBarangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SummaryBarangMasukToolStripMenuItem.Click
        EMI_Barang_Masuk_Summary_Data.StartPosition = FormStartPosition.CenterScreen

        EMI_Barang_Masuk_Summary_Data.MdiParent = Me
        EMI_Barang_Masuk_Summary_Data.Show()
        EMI_Barang_Masuk_Summary_Data.Focus()
    End Sub

    Private Sub PengeluaranBahanBakuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PengeluaranBahanBakuToolStripMenuItem.Click
        EMI_Display_Pengeluaran_Bahan_Baku.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Pengeluaran_Bahan_Baku.MdiParent = Me
        EMI_Display_Pengeluaran_Bahan_Baku.Show()
        EMI_Display_Pengeluaran_Bahan_Baku.Focus()
    End Sub

    Private Sub ControllingProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ControllingProduksiToolStripMenuItem.Click
        EMI_Controlling_Produksi.StartPosition = FormStartPosition.CenterScreen

        EMI_Controlling_Produksi.MdiParent = Me
        EMI_Controlling_Produksi.Show()
        EMI_Controlling_Produksi.Focus()
    End Sub

    Private Sub MasterRoutingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterRoutingToolStripMenuItem.Click
        Master_Routing.StartPosition = FormStartPosition.CenterScreen

        Master_Routing.MdiParent = Me
        Master_Routing.Show()
        Master_Routing.Focus()
    End Sub

    Private Sub ValidasiHPPToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ValidasiHPPToolStripMenuItem1.Click
        EMI_Display_Validasi_HPP_Produksi.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Validasi_HPP_Produksi.MdiParent = Me
        EMI_Display_Validasi_HPP_Produksi.Show()
        EMI_Display_Validasi_HPP_Produksi.Focus()
    End Sub

    Private Sub MasterBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterBarangToolStripMenuItem.Click
        Master_Barang_New.StartPosition = FormStartPosition.CenterScreen

        Master_Barang_New.MdiParent = Me
        Master_Barang_New.Show()
        Master_Barang_New.Focus()
    End Sub

    Private Sub CompareBudgetingWorkCenterFIXToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompareBudgetingWorkCenterFIXToolStripMenuItem.Click
        EMI_Compare_Budgeting.StartPosition = FormStartPosition.CenterScreen

        EMI_Compare_Budgeting.MdiParent = Me
        EMI_Compare_Budgeting.Show()
        EMI_Compare_Budgeting.Focus()
    End Sub

    Private Sub CompareBudgetingWorkCenterBiayaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompareBudgetingWorkCenterBiayaToolStripMenuItem.Click
        EMI_Compare_Budgeting2.StartPosition = FormStartPosition.CenterScreen

        EMI_Compare_Budgeting2.MdiParent = Me
        EMI_Compare_Budgeting2.Show()
        EMI_Compare_Budgeting2.Focus()
    End Sub

    Private Sub TransferStockToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles TransferStockToolStripMenuItem1.Click
        Transfer_Stock_3.StartPosition = FormStartPosition.CenterScreen

        Transfer_Stock_3.MdiParent = Me
        Transfer_Stock_3.Show()
        Transfer_Stock_3.Focus()
    End Sub

    Private Sub DisplayTransferStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayTransferStockToolStripMenuItem.Click
        Emi_Display_Transfer.StartPosition = FormStartPosition.CenterScreen

        Emi_Display_Transfer.MdiParent = Me
        Emi_Display_Transfer.Show()
        Emi_Display_Transfer.Focus()
    End Sub

    Private Sub DisplayTransferStockTidakTimbangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayTransferStockTidakTimbangToolStripMenuItem.Click
        EMI_Display_Transfer_Tidak_Timbang.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Transfer_Tidak_Timbang.MdiParent = Me
        EMI_Display_Transfer_Tidak_Timbang.Show()
        EMI_Display_Transfer_Tidak_Timbang.Focus()
    End Sub

    Private Sub DipToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DipToolStripMenuItem.Click
        Emi_Display_Transfer_Stock.StartPosition = FormStartPosition.CenterScreen

        Emi_Display_Transfer_Stock.MdiParent = Me
        Emi_Display_Transfer_Stock.Show()
        Emi_Display_Transfer_Stock.Focus()
    End Sub

    Private Sub ValidasiSplitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiSplitToolStripMenuItem.Click
        Emi_Display_Tf_Stock_QC.StartPosition = FormStartPosition.CenterScreen

        Emi_Display_Tf_Stock_QC.MdiParent = Me
        Emi_Display_Tf_Stock_QC.Show()
        Emi_Display_Tf_Stock_QC.Focus()
    End Sub

    Private Sub SplitBarangToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SplitBarangToolStripMenuItem1.Click
        Emi_Split_Stock_QC.StartPosition = FormStartPosition.CenterScreen

        Emi_Split_Stock_QC.MdiParent = Me
        Emi_Split_Stock_QC.Show()
        Emi_Split_Stock_QC.Focus()
    End Sub

    Private Sub DisplaySplitBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplaySplitBarangToolStripMenuItem.Click
        EMI_Display_Split_Stock.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Split_Stock.MdiParent = Me
        EMI_Display_Split_Stock.Show()
        EMI_Display_Split_Stock.Focus()
    End Sub

    Private Sub SYNCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SYNCToolStripMenuItem.Click
        Server_Sinkronasi_B2B.StartPosition = FormStartPosition.CenterScreen

        Server_Sinkronasi_B2B.MdiParent = Me
        Server_Sinkronasi_B2B.Show()
        Server_Sinkronasi_B2B.Focus()
    End Sub

    Private Sub TFQualityToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TFQualityToolStripMenuItem.Click
        EMI_Transfer_Quality_QC.StartPosition = FormStartPosition.CenterScreen

        EMI_Transfer_Quality_QC.MdiParent = Me
        EMI_Transfer_Quality_QC.Show()
        EMI_Transfer_Quality_QC.Focus()
    End Sub

    Private Sub PengeluaranBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PengeluaranBarangToolStripMenuItem.Click
        Pengeluaran_Barang.StartPosition = FormStartPosition.CenterScreen

        Pengeluaran_Barang.MdiParent = Me
        Pengeluaran_Barang.Show()
        Pengeluaran_Barang.Focus()
    End Sub
End Class