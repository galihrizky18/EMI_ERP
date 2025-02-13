Public Class FMenuDevFix

#Region "INITIAL MENU"

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
#End Region

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

#Region "PURCHASE MODUL"

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
    Private Sub DataPenawaranToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataPenawaranToolStripMenuItem.Click

    End Sub

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













#End Region

End Class