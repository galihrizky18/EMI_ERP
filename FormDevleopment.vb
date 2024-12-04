Public Class FormDevleopment

#Region "INITIAL FUNCTION"

    Private Sub FormDevleopment_AutoSizeChanged(sender As Object, e As EventArgs) Handles Me.AutoSizeChanged
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub FormDevleopment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
    Private Sub SalesToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles SalesToolStripMenuItem2.Click
        EMI_Transaksi_ForecastOrder.StartPosition = FormStartPosition.CenterScreen
        EMI_Transaksi_ForecastOrder.fStatus = "Transaksi_ForecastOrder_Sales"

        EMI_Transaksi_ForecastOrder.MdiParent = Me
        EMI_Transaksi_ForecastOrder.Show()
        EMI_Transaksi_ForecastOrder.Focus()
    End Sub

    Private Sub PPICToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PPICToolStripMenuItem1.Click
        EMI_Transaksi_ForecastOrder.StartPosition = FormStartPosition.CenterScreen
        EMI_Transaksi_ForecastOrder.fStatus = "Transaksi_ForecastOrder_PPIC"

        EMI_Transaksi_ForecastOrder.MdiParent = Me
        EMI_Transaksi_ForecastOrder.Show()
        EMI_Transaksi_ForecastOrder.Focus()
    End Sub

    Private Sub MaterialRequisitionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MaterialRequisitionToolStripMenuItem.Click
        EMI_Transaksi_MaterialRequisition.StartPosition = FormStartPosition.CenterScreen
        EMI_Transaksi_MaterialRequisition.fstatus = "MRP_PPIC"

        EMI_Transaksi_MaterialRequisition.MdiParent = Me
        EMI_Transaksi_MaterialRequisition.Show()
        EMI_Transaksi_MaterialRequisition.Focus()
    End Sub

    Private Sub PuchaseOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PuchaseOrderToolStripMenuItem.Click
        EMI_PO_Pembelian_Display.StartPosition = FormStartPosition.CenterScreen
        EMI_PO_Pembelian_Display.asal = "PO_Bahan"

        EMI_PO_Pembelian_Display.MdiParent = Me
        EMI_PO_Pembelian_Display.Show()
        EMI_PO_Pembelian_Display.Focus()
    End Sub

    Private Sub RefraksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefraksiToolStripMenuItem.Click
        EMI_Refraksi_Display2.StartPosition = FormStartPosition.CenterScreen

        EMI_Refraksi_Display2.MdiParent = Me
        EMI_Refraksi_Display2.Show()
        EMI_Refraksi_Display2.Focus()
    End Sub

    Private Sub RefraksiToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RefraksiToolStripMenuItem1.Click
        EMI_Refraksi_Display.StartPosition = FormStartPosition.CenterScreen

        EMI_Refraksi_Display.MdiParent = Me
        EMI_Refraksi_Display.Show()
        EMI_Refraksi_Display.Focus()
    End Sub

    Private Sub PurchaseOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PurchaseOrderToolStripMenuItem.Click
        EMI_Pembelian_PO_Summary_Data.StartPosition = FormStartPosition.CenterScreen

        EMI_Pembelian_PO_Summary_Data.MdiParent = Me
        EMI_Pembelian_PO_Summary_Data.Show()
        EMI_Pembelian_PO_Summary_Data.Focus()
    End Sub


    Private Sub QualityControlToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles QualityControlToolStripMenuItem1.Click
        Emi_Display_Quality_Control.StartPosition = FormStartPosition.CenterScreen

        Emi_Display_Quality_Control.MdiParent = Me
        Emi_Display_Quality_Control.Show()
        Emi_Display_Quality_Control.Focus()
    End Sub

    Private Sub PalletMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PalletMasukToolStripMenuItem.Click
        EMI_Display_Pallet_Masuk.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Pallet_Masuk.MdiParent = Me
        EMI_Display_Pallet_Masuk.Show()
        EMI_Display_Pallet_Masuk.Focus()
    End Sub

    Private Sub TimbangFloorScaleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TimbangFloorScaleToolStripMenuItem.Click

        Emi_Display_Timbang_FloorScale.StartPosition = FormStartPosition.CenterScreen

        Emi_Display_Timbang_FloorScale.MdiParent = Me
        Emi_Display_Timbang_FloorScale.Show()
        Emi_Display_Timbang_FloorScale.Focus()
    End Sub

    Private Sub MaterRecrutmentToolStripMenuItem_Click(sender As Object, e As EventArgs) 
        Jf_Master_Rekrutmen_Display.StartPosition = FormStartPosition.CenterScreen

        Jf_Master_Rekrutmen_Display.MdiParent = Me
        Jf_Master_Rekrutmen_Display.Show()
        Jf_Master_Rekrutmen_Display.Focus()
    End Sub

    Private Sub MasterQCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterQCToolStripMenuItem.Click
        Master_Quality_Control.StartPosition = FormStartPosition.CenterScreen

        Master_Quality_Control.MdiParent = Me
        Master_Quality_Control.Show()
        Master_Quality_Control.Focus()
    End Sub

    Private Sub QCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QCToolStripMenuItem.Click
        EMI_Display_QC_Produksi.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_QC_Produksi.MdiParent = Me
        EMI_Display_QC_Produksi.Show()
        EMI_Display_QC_Produksi.Focus()
    End Sub

    Private Sub TransferStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferStockToolStripMenuItem.Click
        Transfer_Stock_3.StartPosition = FormStartPosition.CenterScreen

        Transfer_Stock_3.MdiParent = Me
        Transfer_Stock_3.Show()
        Transfer_Stock_3.Focus()
    End Sub

    Private Sub DisplayTransferToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayTransferToolStripMenuItem.Click
        Emi_Display_Transfer.StartPosition = FormStartPosition.CenterScreen

        Emi_Display_Transfer.MdiParent = Me
        Emi_Display_Transfer.Show()
        Emi_Display_Transfer.Focus()
    End Sub

    Private Sub DisplayTransferTidakTimbangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayTransferTidakTimbangToolStripMenuItem.Click
        EMI_Display_Transfer_Tidak_Timbang.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Transfer_Tidak_Timbang.MdiParent = Me
        EMI_Display_Transfer_Tidak_Timbang.Show()
        EMI_Display_Transfer_Tidak_Timbang.Focus()
    End Sub

    Private Sub FormulaToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles FormulaToolStripMenuItem1.Click
        Transaksi_Formula.StartPosition = FormStartPosition.CenterScreen

        Transaksi_Formula.MdiParent = Me
        Transaksi_Formula.Show()
        Transaksi_Formula.Focus()
    End Sub

    Private Sub FormulaToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles FormulaToolStripMenuItem2.Click

        Display_Formula.StartPosition = FormStartPosition.CenterScreen

        Display_Formula.MdiParent = Me
        Display_Formula.Show()
        Display_Formula.Focus()
    End Sub

    Private Sub BindingFormulaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BindingFormulaToolStripMenuItem.Click
        Display_Formula_Binding.StartPosition = FormStartPosition.CenterScreen

        Display_Formula_Binding.MdiParent = Me
        Display_Formula_Binding.Show()
        Display_Formula_Binding.Focus()
    End Sub

    Private Sub PurchaseRequisitionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PurchaseRequisitionToolStripMenuItem.Click
        Purchase_Requisition.StartPosition = FormStartPosition.CenterScreen

        Purchase_Requisition.MdiParent = Me
        Purchase_Requisition.Show()
        Purchase_Requisition.Focus()
    End Sub

    Private Sub PenawaranToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenawaranToolStripMenuItem.Click
        Transaksi_Penawaran.StartPosition = FormStartPosition.CenterScreen

        Transaksi_Penawaran.MdiParent = Me
        Transaksi_Penawaran.Show()
        Transaksi_Penawaran.Focus()
    End Sub

    Private Sub SalesForecastringByPPICToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalesForecastringByPPICToolStripMenuItem.Click
        EMI_Transaksi_ForecastOrder.StartPosition = FormStartPosition.CenterScreen
        EMI_Transaksi_ForecastOrder.fStatus = "Transaksi_ForecastOrder_PPIC"

        EMI_Transaksi_ForecastOrder.MdiParent = Me
        EMI_Transaksi_ForecastOrder.Show()
        EMI_Transaksi_ForecastOrder.Focus()
    End Sub

    Private Sub MaterialRequisitionByPPICToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MaterialRequisitionByPPICToolStripMenuItem.Click
        EMI_Transaksi_MaterialRequisition.StartPosition = FormStartPosition.CenterScreen
        EMI_Transaksi_MaterialRequisition.fstatus = "MRP_PPIC"

        EMI_Transaksi_MaterialRequisition.MdiParent = Me
        EMI_Transaksi_MaterialRequisition.Show()
        EMI_Transaksi_MaterialRequisition.Focus()
    End Sub

    Private Sub PenawaranToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PenawaranToolStripMenuItem1.Click
        EMI_Penawaran_Harga_Summary_Data.StartPosition = FormStartPosition.CenterScreen

        EMI_Penawaran_Harga_Summary_Data.MdiParent = Me
        EMI_Penawaran_Harga_Summary_Data.Show()
        EMI_Penawaran_Harga_Summary_Data.Focus()
    End Sub

    Private Sub KendaraanTidakSesuaiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KendaraanTidakSesuaiToolStripMenuItem.Click
        Display_Kendaraan_Masuk_Tidak_Sesuai.StartPosition = FormStartPosition.CenterScreen

        Display_Kendaraan_Masuk_Tidak_Sesuai.MdiParent = Me
        Display_Kendaraan_Masuk_Tidak_Sesuai.Show()
        Display_Kendaraan_Masuk_Tidak_Sesuai.Focus()
    End Sub

    Private Sub TimbangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TimbangMasukToolStripMenuItem.Click
        EMI_Display_Timbang.StartPosition = FormStartPosition.CenterScreen
        EMI_Display_Timbang.asal = "Unloading_Barang"
        EMI_Display_Timbang.filter_tambahan = "timbang_masuk='Y'"

        EMI_Display_Timbang.MdiParent = Me
        EMI_Display_Timbang.Show()
        EMI_Display_Timbang.Focus()
    End Sub

    Private Sub TimbangKeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TimbangKeluarToolStripMenuItem.Click
        EMI_Display_Timbang.StartPosition = FormStartPosition.CenterScreen
        EMI_Display_Timbang.asal = "Unloading_Barang"
        EMI_Display_Timbang.filter_tambahan = "timbang_keluar='Y'"

        EMI_Display_Timbang.MdiParent = Me
        EMI_Display_Timbang.Show()
        EMI_Display_Timbang.Focus()
    End Sub

    Private Sub OrderProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrderProduksiToolStripMenuItem.Click

        Emi_Request_Material_Display.StartPosition = FormStartPosition.CenterScreen

        Emi_Request_Material_Display.MdiParent = Me
        Emi_Request_Material_Display.Show()
        Emi_Request_Material_Display.Focus()
    End Sub


























    Private Sub AsyncToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AsyncToolStripMenuItem.Click
        TesAsync.StartPosition = FormStartPosition.CenterScreen

        TesAsync.MdiParent = Me
        TesAsync.Show()
        TesAsync.Focus()
    End Sub


End Class