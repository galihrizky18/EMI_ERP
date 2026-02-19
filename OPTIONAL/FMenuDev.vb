Public Class FMenuDev

#Region "INITIAL FUNCTION"

    Private Sub FMenu_AutoSizeChanged(sender As Object, e As EventArgs) Handles Me.AutoSizeChanged
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub FMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ToolStripStatusLabel3.Text = Format(DateAdd(DateInterval.Second, 1, CDate(ToolStripStatusLabel3.Text)), "yyyy-MM-dd HH:mm:ss")
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
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

#End Region

    '=====================
    '=     LOAD MENU     =
    '=====================
    'Private Sub SalesToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles SalesToolStripMenuItem2.Click
    '    EMI_Transaksi_ForecastOrder.StartPosition = FormStartPosition.CenterScreen
    '    EMI_Transaksi_ForecastOrder.fStatus = "Transaksi_ForecastOrder_Sales"

    '    EMI_Transaksi_ForecastOrder.MdiParent = Me
    '    EMI_Transaksi_ForecastOrder.Show()
    '    EMI_Transaksi_ForecastOrder.Focus()
    'End Sub

    'Private Sub PPICToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PPICToolStripMenuItem1.Click
    '    EMI_Transaksi_ForecastOrder.StartPosition = FormStartPosition.CenterScreen
    '    EMI_Transaksi_ForecastOrder.fStatus = "Transaksi_ForecastOrder_PPIC"

    '    EMI_Transaksi_ForecastOrder.MdiParent = Me
    '    EMI_Transaksi_ForecastOrder.Show()
    '    EMI_Transaksi_ForecastOrder.Focus()
    'End Sub

    'Private Sub MaterialRequisitionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MaterialRequisitionToolStripMenuItem.Click
    '    EMI_Transaksi_MaterialRequisition.StartPosition = FormStartPosition.CenterScreen
    '    EMI_Transaksi_MaterialRequisition.fstatus = "MRP_PPIC"

    '    EMI_Transaksi_MaterialRequisition.MdiParent = Me
    '    EMI_Transaksi_MaterialRequisition.Show()
    '    EMI_Transaksi_MaterialRequisition.Focus()
    'End Sub

    'Private Sub PuchaseOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PuchaseOrderToolStripMenuItem.Click
    '    EMI_PO_Pembelian_Display.StartPosition = FormStartPosition.CenterScreen
    '    EMI_PO_Pembelian_Display.asal = "PO_Bahan"

    '    EMI_PO_Pembelian_Display.MdiParent = Me
    '    EMI_PO_Pembelian_Display.Show()
    '    EMI_PO_Pembelian_Display.Focus()
    'End Sub

    'Private Sub RefraksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefraksiToolStripMenuItem.Click
    '    EMI_Refraksi_Display2.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Refraksi_Display2.MdiParent = Me
    '    EMI_Refraksi_Display2.Show()
    '    EMI_Refraksi_Display2.Focus()
    'End Sub

    'Private Sub RefraksiToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RefraksiToolStripMenuItem1.Click
    '    EMI_Refraksi_Display.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Refraksi_Display.MdiParent = Me
    '    EMI_Refraksi_Display.Show()
    '    EMI_Refraksi_Display.Focus()
    'End Sub

    'Private Sub PurchaseOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PurchaseOrderToolStripMenuItem.Click
    '    EMI_Pembelian_PO_Summary_Data.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pembelian_PO_Summary_Data.MdiParent = Me
    '    EMI_Pembelian_PO_Summary_Data.Show()
    '    EMI_Pembelian_PO_Summary_Data.Focus()
    'End Sub


    'Private Sub QualityControlToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles QualityControlToolStripMenuItem1.Click
    '    Emi_Display_Quality_Control.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Display_Quality_Control.MdiParent = Me
    '    Emi_Display_Quality_Control.Show()
    '    Emi_Display_Quality_Control.Focus()
    'End Sub

    'Private Sub PalletMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PalletMasukToolStripMenuItem.Click
    '    EMI_Display_Pallet_Masuk.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Pallet_Masuk.MdiParent = Me
    '    EMI_Display_Pallet_Masuk.Show()
    '    EMI_Display_Pallet_Masuk.Focus()
    'End Sub

    'Private Sub TimbangFloorScaleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TimbangFloorScaleToolStripMenuItem.Click

    '    Emi_Display_Timbang_FloorScale.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Display_Timbang_FloorScale.MdiParent = Me
    '    Emi_Display_Timbang_FloorScale.Show()
    '    Emi_Display_Timbang_FloorScale.Focus()
    'End Sub

    'Private Sub MaterRecrutmentToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    Jf_Master_Rekrutmen_Display.StartPosition = FormStartPosition.CenterScreen

    '    Jf_Master_Rekrutmen_Display.MdiParent = Me
    '    Jf_Master_Rekrutmen_Display.Show()
    '    Jf_Master_Rekrutmen_Display.Focus()
    'End Sub

    'Private Sub MasterQCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterQCToolStripMenuItem.Click
    '    Master_Quality_Control.StartPosition = FormStartPosition.CenterScreen

    '    Master_Quality_Control.MdiParent = Me
    '    Master_Quality_Control.Show()
    '    Master_Quality_Control.Focus()
    'End Sub

    'Private Sub QCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QCToolStripMenuItem.Click
    '    EMI_Display_QC_Produksi.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_QC_Produksi.MdiParent = Me
    '    EMI_Display_QC_Produksi.Show()
    '    EMI_Display_QC_Produksi.Focus()
    'End Sub

    'Private Sub TransferStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferStockToolStripMenuItem.Click
    '    Transfer_Stock_3.StartPosition = FormStartPosition.CenterScreen

    '    Transfer_Stock_3.MdiParent = Me
    '    Transfer_Stock_3.Show()
    '    Transfer_Stock_3.Focus()
    'End Sub

    'Private Sub DisplayTransferToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayTransferToolStripMenuItem.Click
    '    Emi_Display_Transfer.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Display_Transfer.MdiParent = Me
    '    Emi_Display_Transfer.Show()
    '    Emi_Display_Transfer.Focus()
    'End Sub

    'Private Sub DisplayTransferTidakTimbangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayTransferTidakTimbangToolStripMenuItem.Click
    '    EMI_Display_Transfer_Tidak_Timbang.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Transfer_Tidak_Timbang.MdiParent = Me
    '    EMI_Display_Transfer_Tidak_Timbang.Show()
    '    EMI_Display_Transfer_Tidak_Timbang.Focus()
    'End Sub

    'Private Sub FormulaToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles FormulaToolStripMenuItem1.Click
    '    Transaksi_Formula.StartPosition = FormStartPosition.CenterScreen

    '    Transaksi_Formula.MdiParent = Me
    '    Transaksi_Formula.Show()
    '    Transaksi_Formula.Focus()
    'End Sub

    'Private Sub FormulaToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles FormulaToolStripMenuItem2.Click

    '    Display_Formula.StartPosition = FormStartPosition.CenterScreen

    '    Display_Formula.MdiParent = Me
    '    Display_Formula.Show()
    '    Display_Formula.Focus()
    'End Sub

    'Private Sub BindingFormulaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BindingFormulaToolStripMenuItem.Click
    '    Display_Formula_Binding.StartPosition = FormStartPosition.CenterScreen

    '    Display_Formula_Binding.MdiParent = Me
    '    Display_Formula_Binding.Show()
    '    Display_Formula_Binding.Focus()
    'End Sub

    'Private Sub PurchaseRequisitionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PurchaseRequisitionToolStripMenuItem.Click
    '    Purchase_Requisition.StartPosition = FormStartPosition.CenterScreen

    '    Purchase_Requisition.MdiParent = Me
    '    Purchase_Requisition.Show()
    '    Purchase_Requisition.Focus()
    'End Sub

    'Private Sub PenawaranToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenawaranToolStripMenuItem.Click
    '    Transaksi_Penawaran.StartPosition = FormStartPosition.CenterScreen

    '    Transaksi_Penawaran.MdiParent = Me
    '    Transaksi_Penawaran.Show()
    '    Transaksi_Penawaran.Focus()
    'End Sub

    'Private Sub SalesForecastringByPPICToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalesForecastringByPPICToolStripMenuItem.Click
    '    EMI_Transaksi_ForecastOrder.StartPosition = FormStartPosition.CenterScreen
    '    EMI_Transaksi_ForecastOrder.fStatus = "Transaksi_ForecastOrder_PPIC"

    '    EMI_Transaksi_ForecastOrder.MdiParent = Me
    '    EMI_Transaksi_ForecastOrder.Show()
    '    EMI_Transaksi_ForecastOrder.Focus()
    'End Sub

    'Private Sub MaterialRequisitionByPPICToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MaterialRequisitionByPPICToolStripMenuItem.Click
    '    EMI_Transaksi_MaterialRequisition.StartPosition = FormStartPosition.CenterScreen
    '    EMI_Transaksi_MaterialRequisition.fstatus = "MRP_PPIC"

    '    EMI_Transaksi_MaterialRequisition.MdiParent = Me
    '    EMI_Transaksi_MaterialRequisition.Show()
    '    EMI_Transaksi_MaterialRequisition.Focus()
    'End Sub

    'Private Sub PenawaranToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PenawaranToolStripMenuItem1.Click
    '    EMI_Penawaran_Harga_Summary_Data.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Penawaran_Harga_Summary_Data.MdiParent = Me
    '    EMI_Penawaran_Harga_Summary_Data.Show()
    '    EMI_Penawaran_Harga_Summary_Data.Focus()
    'End Sub

    'Private Sub KendaraanTidakSesuaiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KendaraanTidakSesuaiToolStripMenuItem.Click
    '    Display_Kendaraan_Masuk_Tidak_Sesuai.StartPosition = FormStartPosition.CenterScreen

    '    Display_Kendaraan_Masuk_Tidak_Sesuai.MdiParent = Me
    '    Display_Kendaraan_Masuk_Tidak_Sesuai.Show()
    '    Display_Kendaraan_Masuk_Tidak_Sesuai.Focus()
    'End Sub

    'Private Sub TimbangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TimbangMasukToolStripMenuItem.Click
    '    EMI_Display_Timbang.StartPosition = FormStartPosition.CenterScreen
    '    EMI_Display_Timbang.asal = "Unloading_Barang"
    '    EMI_Display_Timbang.filter_tambahan = "timbang_masuk='Y'"

    '    EMI_Display_Timbang.MdiParent = Me
    '    EMI_Display_Timbang.Show()
    '    EMI_Display_Timbang.Focus()
    'End Sub

    'Private Sub TimbangKeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TimbangKeluarToolStripMenuItem.Click
    '    EMI_Display_Timbang.StartPosition = FormStartPosition.CenterScreen
    '    EMI_Display_Timbang.asal = "Unloading_Barang"
    '    EMI_Display_Timbang.filter_tambahan = "timbang_keluar='Y'"

    '    EMI_Display_Timbang.MdiParent = Me
    '    EMI_Display_Timbang.Show()
    '    EMI_Display_Timbang.Focus()
    'End Sub

    'Private Sub OrderProduksiToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OrderProduksiToolStripMenuItem1.Click

    '    Emi_Request_Material_Display.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Request_Material_Display.MdiParent = Me
    '    Emi_Request_Material_Display.Show()
    '    Emi_Request_Material_Display.Focus()
    'End Sub

    'Private Sub TransferMaterialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferMaterialToolStripMenuItem.Click
    '    Master_Flever.StartPosition = FormStartPosition.CenterScreen

    '    Master_Flever.MdiParent = Me
    '    Master_Flever.Show()
    '    Master_Flever.Focus()
    'End Sub

    'Private Sub TransferMaterialToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles TransferMaterialToolStripMenuItem1.Click
    '    EMI_Flever3.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Flever3.MdiParent = Me
    '    EMI_Flever3.Show()
    '    EMI_Flever3.Focus()
    'End Sub

    'Private Sub DisplayMaterialTidakTimbangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayMaterialTidakTimbangToolStripMenuItem.Click
    '    EMI_Display_Flever_Tidak_Timbang.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Flever_Tidak_Timbang.MdiParent = Me
    '    EMI_Display_Flever_Tidak_Timbang.Show()
    '    EMI_Display_Flever_Tidak_Timbang.Focus()
    'End Sub

    'Private Sub SuppliersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SuppliersToolStripMenuItem.Click
    '    Master_Suppliers.StartPosition = FormStartPosition.CenterScreen

    '    Master_Suppliers.MdiParent = Me
    '    Master_Suppliers.Show()
    '    Master_Suppliers.Focus()
    'End Sub

    'Private Sub LoadingBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadingBarangToolStripMenuItem.Click
    '    Loading_Barang_Import.StartPosition = FormStartPosition.CenterScreen

    '    Loading_Barang_Import.MdiParent = Me
    '    Loading_Barang_Import.Show()
    '    Loading_Barang_Import.Focus()
    'End Sub

    'Private Sub HitungHPPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HitungHPPToolStripMenuItem.Click
    '    Hitung_HPP_Import.StartPosition = FormStartPosition.CenterScreen

    '    Hitung_HPP_Import.MdiParent = Me
    '    Hitung_HPP_Import.Show()
    '    Hitung_HPP_Import.Focus()
    'End Sub

    'Private Sub WorkCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WorkCenterToolStripMenuItem.Click
    '    EMI_Transaksi_Work_Center.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Transaksi_Work_Center.MdiParent = Me
    '    EMI_Transaksi_Work_Center.Show()
    '    EMI_Transaksi_Work_Center.Focus()
    'End Sub

    'Private Sub CompareWorkCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompareWorkCenterToolStripMenuItem.Click
    '    EMI_Compare_Work_Center.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Compare_Work_Center.MdiParent = Me
    '    EMI_Compare_Work_Center.Show()
    '    EMI_Compare_Work_Center.Focus()
    'End Sub

    'Private Sub QualityControlBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QualityControlBarangToolStripMenuItem.Click
    '    Master_Quality_Control_Barang.StartPosition = FormStartPosition.CenterScreen

    '    Master_Quality_Control_Barang.MdiParent = Me
    '    Master_Quality_Control_Barang.Show()
    '    Master_Quality_Control_Barang.Focus()
    'End Sub

    'Private Sub DetailKategoriQCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DetailKategoriQCToolStripMenuItem.Click
    '    Master_Quality_Control_Kategori_Detail.StartPosition = FormStartPosition.CenterScreen

    '    Master_Quality_Control_Kategori_Detail.MdiParent = Me
    '    Master_Quality_Control_Kategori_Detail.Show()
    '    Master_Quality_Control_Kategori_Detail.Focus()
    'End Sub

    'Private Sub WorkCenterAutomationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WorkCenterAutomationToolStripMenuItem.Click
    '    EMI_Transaksi_Work_Center_By_Automation.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Transaksi_Work_Center_By_Automation.MdiParent = Me
    '    EMI_Transaksi_Work_Center_By_Automation.Show()
    '    EMI_Transaksi_Work_Center_By_Automation.Focus()
    'End Sub

    'Private Sub AdjustmentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdjustmentToolStripMenuItem.Click
    '    EMI_Adjustment_Dist.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Adjustment_Dist.MdiParent = Me
    '    EMI_Adjustment_Dist.Show()
    '    EMI_Adjustment_Dist.Focus()
    'End Sub

    'Private Sub RestockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestockToolStripMenuItem.Click
    '    EMI_Restock.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Restock.MdiParent = Me
    '    EMI_Restock.Show()
    '    EMI_Restock.Focus()
    'End Sub

    'Private Sub RestockToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RestockToolStripMenuItem1.Click
    '    EMI_Display_Restock.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Restock.MdiParent = Me
    '    EMI_Display_Restock.Show()
    '    EMI_Display_Restock.Focus()
    'End Sub

    'Private Sub MasterRoutingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterRoutingToolStripMenuItem.Click
    '    Master_Routing.StartPosition = FormStartPosition.CenterScreen

    '    Master_Routing.MdiParent = Me
    '    Master_Routing.Show()
    '    Master_Routing.Focus()
    'End Sub

    'Private Sub MasterWorkCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterWorkCenterToolStripMenuItem.Click
    '    Master_Work_Center.StartPosition = FormStartPosition.CenterScreen

    '    Master_Work_Center.MdiParent = Me
    '    Master_Work_Center.Show()
    '    Master_Work_Center.Focus()
    'End Sub

    'Private Sub WorkCenter2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WorkCenter2ToolStripMenuItem.Click
    '    EMI_Transaksi_Work_Center2.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Transaksi_Work_Center2.MdiParent = Me
    '    EMI_Transaksi_Work_Center2.Show()
    '    EMI_Transaksi_Work_Center2.Focus()
    'End Sub

    'Private Sub MasterBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterBarangToolStripMenuItem.Click
    '    Master_Barang_New.StartPosition = FormStartPosition.CenterScreen

    '    Master_Barang_New.MdiParent = Me
    '    Master_Barang_New.Show()
    '    Master_Barang_New.Focus()
    'End Sub

    'Private Sub TesToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles TesToolStripMenuItem1.Click
    '    Tes.StartPosition = FormStartPosition.CenterScreen

    '    Tes.MdiParent = Me
    '    Tes.Show()
    '    Tes.Focus()
    'End Sub

    'Private Sub MasterRekrutmentDisplayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterRekrutmentDisplayToolStripMenuItem.Click
    '    Jf_Master_Rekrutmen_Display_Input.StartPosition = FormStartPosition.CenterScreen

    '    Jf_Master_Rekrutmen_Display_Input.MdiParent = Me
    '    Jf_Master_Rekrutmen_Display_Input.Show()
    '    Jf_Master_Rekrutmen_Display_Input.Focus()
    'End Sub

    'Private Sub TesPrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TesPrintToolStripMenuItem.Click

    '    TesPrint.StartPosition = FormStartPosition.CenterScreen

    '    TesPrint.MdiParent = Me
    '    TesPrint.Show()
    '    TesPrint.Focus()
    'End Sub

    'Private Sub SummaryBarangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SummaryBarangMasukToolStripMenuItem.Click
    '    EMI_Barang_Masuk_Summary_Data.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Barang_Masuk_Summary_Data.MdiParent = Me
    '    EMI_Barang_Masuk_Summary_Data.Show()
    '    EMI_Barang_Masuk_Summary_Data.Focus()
    'End Sub

    'Private Sub PrinterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrinterToolStripMenuItem.Click
    '    Global_Setting.StartPosition = FormStartPosition.CenterScreen

    '    Global_Setting.MdiParent = Me
    '    Global_Setting.Show()
    '    Global_Setting.Focus()
    'End Sub

    'Private Sub MasterMenuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterMenuToolStripMenuItem.Click
    '    Master_Menu2.StartPosition = FormStartPosition.CenterScreen

    '    Master_Menu2.MdiParent = Me
    '    Master_Menu2.Show()
    '    Master_Menu2.Focus()
    'End Sub

    'Private Sub MasterJenisBiayaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterJenisBiayaToolStripMenuItem.Click
    '    Master_Jenis_Biaya_Produksi.StartPosition = FormStartPosition.CenterScreen

    '    Master_Jenis_Biaya_Produksi.MdiParent = Me
    '    Master_Jenis_Biaya_Produksi.Show()
    '    Master_Jenis_Biaya_Produksi.Focus()
    'End Sub

    'Private Sub AktualBiayaProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AktualBiayaProduksiToolStripMenuItem.Click
    '    EMI_Transaksi_Actual_Biaya_Produksi.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Transaksi_Actual_Biaya_Produksi.MdiParent = Me
    '    EMI_Transaksi_Actual_Biaya_Produksi.Show()
    '    EMI_Transaksi_Actual_Biaya_Produksi.Focus()
    'End Sub

    'Private Sub KategoriBiayaImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KategoriBiayaImportToolStripMenuItem.Click
    '    Kategori_Biaya_Import.StartPosition = FormStartPosition.CenterScreen

    '    Kategori_Biaya_Import.MdiParent = Me
    '    Kategori_Biaya_Import.Show()
    '    Kategori_Biaya_Import.Focus()
    'End Sub

    'Private Sub HasilProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HasilProduksiToolStripMenuItem.Click
    '    EMI_Display_Production_Result.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Production_Result.MdiParent = Me
    '    EMI_Display_Production_Result.Show()
    '    EMI_Display_Production_Result.Focus()
    'End Sub

    'Private Sub DetailAccount3ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DetailAccount3ToolStripMenuItem.Click
    '    Detail_Account_New.StartPosition = FormStartPosition.CenterScreen

    '    Detail_Account_New.MdiParent = Me
    '    Detail_Account_New.Show()
    '    Detail_Account_New.Focus()
    'End Sub


    'Private Sub SummaryUsageRMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SummaryUsageRMToolStripMenuItem.Click

    '    Laporan_Summary_Usage_RM.StartPosition = FormStartPosition.CenterScreen

    '    Laporan_Summary_Usage_RM.MdiParent = Me
    '    Laporan_Summary_Usage_RM.Show()
    '    Laporan_Summary_Usage_RM.Focus()
    'End Sub

    'Private Sub DisplayHasilQCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayHasilQCToolStripMenuItem.Click

    '    Display_Hasil_Quality_Control.StartPosition = FormStartPosition.CenterScreen

    '    Display_Hasil_Quality_Control.MdiParent = Me
    '    Display_Hasil_Quality_Control.Show()
    '    Display_Hasil_Quality_Control.Focus()
    'End Sub

    'Private Sub HasilQCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HasilQCToolStripMenuItem.Click

    '    Emi_Display_Quality_Control.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Display_Quality_Control.MdiParent = Me
    '    Emi_Display_Quality_Control.Show()
    '    Emi_Display_Quality_Control.Focus()
    'End Sub

    'Private Sub TransferStockQCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferStockQCToolStripMenuItem.Click
    '    Transfer_Stock_QC.StartPosition = FormStartPosition.CenterScreen

    '    Transfer_Stock_QC.MdiParent = Me
    '    Transfer_Stock_QC.Show()
    '    Transfer_Stock_QC.Focus()
    'End Sub

    'Private Sub TfSrtockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TfSrtockToolStripMenuItem.Click
    '    Transfer_Stock_QC.StartPosition = FormStartPosition.CenterScreen

    '    Transfer_Stock_QC.MdiParent = Me
    '    Transfer_Stock_QC.Show()
    '    Transfer_Stock_QC.Focus()
    'End Sub

    'Private Sub DisplayTidakTimbangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayTidakTimbangToolStripMenuItem.Click
    '    EMI_Display_Transfer_Tidak_Timbang_QC.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Transfer_Tidak_Timbang_QC.MdiParent = Me
    '    EMI_Display_Transfer_Tidak_Timbang_QC.Show()
    '    EMI_Display_Transfer_Tidak_Timbang_QC.Focus()
    'End Sub

    'Private Sub DisplayTimbangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayTimbangToolStripMenuItem.Click

    '    EMI_Display_Transfer_QC.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Transfer_QC.MdiParent = Me
    '    EMI_Display_Transfer_QC.Show()
    '    EMI_Display_Transfer_QC.Focus()
    'End Sub

    'Private Sub RequestMaterialToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RequestMaterialToolStripMenuItem1.Click
    '    Emi_Request_Material_Display.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Request_Material_Display.MdiParent = Me
    '    Emi_Request_Material_Display.Show()
    '    Emi_Request_Material_Display.Focus()
    'End Sub

    'Private Sub BudgetingPerCostCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BudgetingPerCostCenterToolStripMenuItem.Click
    '    Budgeting_Per_CostCenter.StartPosition = FormStartPosition.CenterScreen

    '    Budgeting_Per_CostCenter.MdiParent = Me
    '    Budgeting_Per_CostCenter.Show()
    '    Budgeting_Per_CostCenter.Focus()
    'End Sub

    'Private Sub LaporanGIGRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanGIGRToolStripMenuItem.Click
    '    Laporan_GI_GR.StartPosition = FormStartPosition.CenterScreen

    '    Laporan_GI_GR.MdiParent = Me
    '    Laporan_GI_GR.Show()
    '    Laporan_GI_GR.Focus()
    'End Sub

    'Private Sub MulaiProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MulaiProduksiToolStripMenuItem.Click
    '    EMI_Display_Mulai_Produksi.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Mulai_Produksi.MdiParent = Me
    '    EMI_Display_Mulai_Produksi.Show()
    '    EMI_Display_Mulai_Produksi.Focus()
    'End Sub

    'Private Sub TrackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TrackToolStripMenuItem.Click

    '    Jf_Display_Rencana_Order1.StartPosition = FormStartPosition.CenterScreen

    '    Jf_Display_Rencana_Order1.MdiParent = Me
    '    Jf_Display_Rencana_Order1.Show()
    '    Jf_Display_Rencana_Order1.Focus()
    'End Sub

    'Private Sub TransferStockToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles TransferStockToolStripMenuItem1.Click
    '    Emi_Display_Transfer_Stock.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Display_Transfer_Stock.MdiParent = Me
    '    Emi_Display_Transfer_Stock.Show()
    '    Emi_Display_Transfer_Stock.Focus()
    'End Sub

    'Private Sub TransferQualityQCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferQualityQCToolStripMenuItem.Click
    '    EMI_Transfer_Quality_QC.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Transfer_Quality_QC.MdiParent = Me
    '    EMI_Transfer_Quality_QC.Show()
    '    EMI_Transfer_Quality_QC.Focus()
    'End Sub

    'Private Sub SyncServerB2BToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SyncServerB2BToolStripMenuItem.Click
    '    Server_Sinkronasi_B2B.StartPosition = FormStartPosition.CenterScreen

    '    Server_Sinkronasi_B2B.MdiParent = Me
    '    Server_Sinkronasi_B2B.Show()
    '    Server_Sinkronasi_B2B.Focus()
    'End Sub

    'Private Sub MasterGudangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterGudangToolStripMenuItem.Click
    '    Master_Gudang.StartPosition = FormStartPosition.CenterScreen

    '    Master_Gudang.MdiParent = Me
    '    Master_Gudang.Show()
    '    Master_Gudang.Focus()
    'End Sub

    'Private Sub DisplayPO2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayPO2ToolStripMenuItem.Click
    '    EMI_PO_Pembelian_Display2.StartPosition = FormStartPosition.CenterScreen

    '    EMI_PO_Pembelian_Display2.MdiParent = Me
    '    EMI_PO_Pembelian_Display2.Show()
    '    EMI_PO_Pembelian_Display2.Focus()
    'End Sub

    'Private Sub DisplayPalletMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayPalletMasukToolStripMenuItem.Click
    '    EMI_Display_Pallet_Masuk_Data.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Pallet_Masuk_Data.MdiParent = Me
    '    EMI_Display_Pallet_Masuk_Data.Show()
    '    EMI_Display_Pallet_Masuk_Data.Focus()
    'End Sub



    'Private Sub LaporanImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanImportToolStripMenuItem.Click
    '    Laporan_HPP_Import.StartPosition = FormStartPosition.CenterScreen

    '    Laporan_HPP_Import.MdiParent = Me
    '    Laporan_HPP_Import.Show()
    '    Laporan_HPP_Import.Focus()
    'End Sub

    'Private Sub LaporanLokalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanLokalToolStripMenuItem.Click
    '    Laporan_HPP_Local.StartPosition = FormStartPosition.CenterScreen

    '    Laporan_HPP_Local.MdiParent = Me
    '    Laporan_HPP_Local.Show()
    '    Laporan_HPP_Local.Focus()
    'End Sub

    'Private Sub PelunasanBiayaToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PelunasanBiayaToolStripMenuItem1.Click
    '    EMI_Pelunasan_Biaya_Import_By_Perusahaan_Lokal.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pelunasan_Biaya_Import_By_Perusahaan_Lokal.MdiParent = Me
    '    EMI_Pelunasan_Biaya_Import_By_Perusahaan_Lokal.Show()
    '    EMI_Pelunasan_Biaya_Import_By_Perusahaan_Lokal.Focus()
    'End Sub

    'Private Sub PelunasanBahanToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PelunasanBahanToolStripMenuItem1.Click
    '    EMI_Pelunasan_Hutang_Bahan_Stock.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pelunasan_Hutang_Bahan_Stock.MdiParent = Me
    '    EMI_Pelunasan_Hutang_Bahan_Stock.Show()
    '    EMI_Pelunasan_Hutang_Bahan_Stock.Focus()
    'End Sub

    'Private Sub PelunasanBiayaImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelunasanBiayaImportToolStripMenuItem.Click
    '    Display_Emi_Pelunasan.StartPosition = FormStartPosition.CenterScreen

    '    Display_Emi_Pelunasan.MdiParent = Me
    '    Display_Emi_Pelunasan.Show()
    '    Display_Emi_Pelunasan.Focus()
    'End Sub

    'Private Sub PelunasanSupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelunasanSupplierToolStripMenuItem.Click
    '    Display_Pelunasan_Supplier.StartPosition = FormStartPosition.CenterScreen

    '    Display_Pelunasan_Supplier.MdiParent = Me
    '    Display_Pelunasan_Supplier.Show()
    '    Display_Pelunasan_Supplier.Focus()
    'End Sub

    'Private Sub TransaksiBiayaImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransaksiBiayaImportToolStripMenuItem.Click
    '    Display_Transaksi_Biaya_Import.StartPosition = FormStartPosition.CenterScreen

    '    Display_Transaksi_Biaya_Import.MdiParent = Me
    '    Display_Transaksi_Biaya_Import.Show()
    '    Display_Transaksi_Biaya_Import.Focus()
    'End Sub

    'Private Sub PelunasanPerbaikanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelunasanPerbaikanToolStripMenuItem.Click
    '    Emi_Pelunasan.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Pelunasan.MdiParent = Me
    '    Emi_Pelunasan.Show()
    '    Emi_Pelunasan.Focus()
    'End Sub

    'Private Sub HutangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HutangToolStripMenuItem.Click
    '    Display_Emi_Pelunasan_Hutang.StartPosition = FormStartPosition.CenterScreen

    '    Display_Emi_Pelunasan_Hutang.MdiParent = Me
    '    Display_Emi_Pelunasan_Hutang.Show()
    '    Display_Emi_Pelunasan_Hutang.Focus()
    'End Sub

    'Private Sub HPPToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HPPToolStripMenuItem1.Click
    '    EMI_Display_HPP.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_HPP.MdiParent = Me
    '    EMI_Display_HPP.Show()
    '    EMI_Display_HPP.Focus()
    'End Sub

    'Private Sub MasterRoleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterRoleToolStripMenuItem.Click
    '    Master_Role.StartPosition = FormStartPosition.CenterScreen

    '    Master_Role.MdiParent = Me
    '    Master_Role.Show()
    '    Master_Role.Focus()
    'End Sub

    'Private Sub MasterSupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterSupplierToolStripMenuItem.Click
    '    Master_Suppliers.StartPosition = FormStartPosition.CenterScreen

    '    Master_Suppliers.MdiParent = Me
    '    Master_Suppliers.Show()
    '    Master_Suppliers.Focus()
    'End Sub

    'Private Sub TrackKendaraanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TrackKendaraanToolStripMenuItem.Click
    '    Display_Tracking_Kendaraan.StartPosition = FormStartPosition.CenterScreen

    '    Display_Tracking_Kendaraan.MdiParent = Me
    '    Display_Tracking_Kendaraan.Show()
    '    Display_Tracking_Kendaraan.Focus()
    'End Sub




























    'Private Sub AsyncToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AsyncToolStripMenuItem.Click
    '    TesAsync.StartPosition = FormStartPosition.CenterScreen

    '    TesAsync.MdiParent = Me
    '    TesAsync.Show()
    '    TesAsync.Focus()
    'End Sub


End Class