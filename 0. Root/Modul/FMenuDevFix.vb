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

    'Private Sub InputMenuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InputMenuToolStripMenuItem.Click
    '    Master_Menu2.StartPosition = FormStartPosition.CenterScreen

    '    Master_Menu2.MdiParent = Me
    '    Master_Menu2.Show()
    '    Master_Menu2.Focus()
    'End Sub

    'Private Sub InputRoleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InputRoleToolStripMenuItem.Click
    '    Master_Role.StartPosition = FormStartPosition.CenterScreen

    '    Master_Role.MdiParent = Me
    '    Master_Role.Show()
    '    Master_Role.Focus()
    'End Sub


    ''== MASTER MENU =='
    'Private Sub MasterEkspedisiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterEkspedisiToolStripMenuItem.Click
    '    Master_Ekspedisi.StartPosition = FormStartPosition.CenterScreen

    '    Master_Ekspedisi.MdiParent = Me
    '    Master_Ekspedisi.Show()
    '    Master_Ekspedisi.Focus()
    'End Sub

    'Private Sub MasterSelisihJenisToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterSelisihJenisToolStripMenuItem.Click
    '    Master_Jenis_Selisih.StartPosition = FormStartPosition.CenterScreen

    '    Master_Jenis_Selisih.MdiParent = Me
    '    Master_Jenis_Selisih.Show()
    '    Master_Jenis_Selisih.Focus()
    'End Sub

    'Private Sub MasterKategoriHargaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterKategoriHargaToolStripMenuItem.Click
    '    Master_Kategori_Harga.StartPosition = FormStartPosition.CenterScreen

    '    Master_Kategori_Harga.MdiParent = Me
    '    Master_Kategori_Harga.Show()
    '    Master_Kategori_Harga.Focus()
    'End Sub

    'Private Sub MasterKategoriHargaDetailToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterKategoriHargaDetailToolStripMenuItem.Click
    '    Master_Kategori_Harga_Detail.StartPosition = FormStartPosition.CenterScreen

    '    Master_Kategori_Harga_Detail.MdiParent = Me
    '    Master_Kategori_Harga_Detail.Show()
    '    Master_Kategori_Harga_Detail.Focus()
    'End Sub

    'Private Sub MasterKateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterKateToolStripMenuItem.Click
    '    Master_Kategori_PO.StartPosition = FormStartPosition.CenterScreen

    '    Master_Kategori_PO.MdiParent = Me
    '    Master_Kategori_PO.Show()
    '    Master_Kategori_PO.Focus()
    'End Sub

    'Private Sub MasterKategoriPORoleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterKategoriPORoleToolStripMenuItem.Click
    '    Master_Kategori_PO_Role.StartPosition = FormStartPosition.CenterScreen

    '    Master_Kategori_PO_Role.MdiParent = Me
    '    Master_Kategori_PO_Role.Show()
    '    Master_Kategori_PO_Role.Focus()
    'End Sub

    'Private Sub MasterMediaKirimToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterMediaKirimToolStripMenuItem.Click
    '    Master_Media_Kirim.StartPosition = FormStartPosition.CenterScreen

    '    Master_Media_Kirim.MdiParent = Me
    '    Master_Media_Kirim.Show()
    '    Master_Media_Kirim.Focus()
    'End Sub

    'Private Sub MasterJatuhTempoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterJatuhTempoToolStripMenuItem.Click
    '    Master_Perhitungan_Jatuh_Tempo.StartPosition = FormStartPosition.CenterScreen

    '    Master_Perhitungan_Jatuh_Tempo.MdiParent = Me
    '    Master_Perhitungan_Jatuh_Tempo.Show()
    '    Master_Perhitungan_Jatuh_Tempo.Focus()
    'End Sub

    'Private Sub MasterSupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterSupplierToolStripMenuItem.Click
    '    Master_Suppliers.StartPosition = FormStartPosition.CenterScreen

    '    Master_Suppliers.MdiParent = Me
    '    Master_Suppliers.Show()
    '    Master_Suppliers.Focus()
    'End Sub

    ''== TRANSAKSI =='
    ''== Penawaran =='
    'Private Sub PenawaranToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PenawaranToolStripMenuItem1.Click
    '    Transaksi_Penawaran.StartPosition = FormStartPosition.CenterScreen

    '    Transaksi_Penawaran.MdiParent = Me
    '    Transaksi_Penawaran.Show()
    '    Transaksi_Penawaran.Focus()
    'End Sub

    ''== Pembelian =='
    'Private Sub POToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles POToolStripMenuItem.Click
    '    EMI_PO_Pembelian_Display.StartPosition = FormStartPosition.CenterScreen

    '    EMI_PO_Pembelian_Display.MdiParent = Me
    '    EMI_PO_Pembelian_Display.Show()
    '    EMI_PO_Pembelian_Display.Focus()
    'End Sub

    Private Sub PurcahseRequisitionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PurcahseRequisitionToolStripMenuItem.Click
        Purchase_Requisition.StartPosition = FormStartPosition.CenterScreen

        Purchase_Requisition.MdiParent = Me
        Purchase_Requisition.Show()
        Purchase_Requisition.Focus()
    End Sub

    ''== Pelunasan =='
    'Private Sub PelunasanBiayaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelunasanBiayaToolStripMenuItem.Click
    '    EMI_Pelunasan.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pelunasan.MdiParent = Me
    '    EMI_Pelunasan.Show()
    '    EMI_Pelunasan.Focus()
    'End Sub

    'Private Sub PembayaranDimukaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PembayaranDimukaToolStripMenuItem.Click
    '    EMI_Pembayaran_Di_Muka.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pembayaran_Di_Muka.MdiParent = Me
    '    EMI_Pembayaran_Di_Muka.Show()
    '    EMI_Pembayaran_Di_Muka.Focus()
    'End Sub

    'Private Sub BindingPembayaranDimukaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BindingPembayaranDimukaToolStripMenuItem.Click
    '    EMI_Pembayaran_Di_Muka_Binding.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pembayaran_Di_Muka_Binding.MdiParent = Me
    '    EMI_Pembayaran_Di_Muka_Binding.Show()
    '    EMI_Pembayaran_Di_Muka_Binding.Focus()
    'End Sub

    ''== DISPLAY =='
    ''== Penawaran =='
    'Private Sub PenawaranToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles PenawaranToolStripMenuItem3.Click
    '    EMI_Penawaran_Harga_Summary_Data.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Penawaran_Harga_Summary_Data.MdiParent = Me
    '    EMI_Penawaran_Harga_Summary_Data.Show()
    '    EMI_Penawaran_Harga_Summary_Data.Focus()
    'End Sub

    'Private Sub PenawaranBerakhirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenawaranBerakhirToolStripMenuItem.Click
    '    Emi_Display_Barang_Penawaran.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Display_Barang_Penawaran.MdiParent = Me
    '    Emi_Display_Barang_Penawaran.Show()
    '    Emi_Display_Barang_Penawaran.Focus()
    'End Sub

    ''== Pembelian =='
    'Private Sub PurchaseOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PurchaseOrderToolStripMenuItem.Click
    '    EMI_Pembelian_PO_Summary_Data.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pembelian_PO_Summary_Data.MdiParent = Me
    '    EMI_Pembelian_PO_Summary_Data.Show()
    '    EMI_Pembelian_PO_Summary_Data.Focus()
    'End Sub

    'Private Sub PurchaseRequisitionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PurchaseRequisitionToolStripMenuItem.Click
    '    EMI_Pembelian_PR_Summary_Data.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pembelian_PR_Summary_Data.MdiParent = Me
    '    EMI_Pembelian_PR_Summary_Data.Show()
    '    EMI_Pembelian_PR_Summary_Data.Focus()
    'End Sub

    'Private Sub HPPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HPPToolStripMenuItem.Click
    '    EMI_Display_HPP.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_HPP.MdiParent = Me
    '    EMI_Display_HPP.Show()
    '    EMI_Display_HPP.Focus()
    'End Sub

    ''== Pelunasan =='
    'Private Sub PelunasanToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles PelunasanToolStripMenuItem2.Click
    '    Display_Emi_Pelunasan.StartPosition = FormStartPosition.CenterScreen

    '    Display_Emi_Pelunasan.MdiParent = Me
    '    Display_Emi_Pelunasan.Show()
    '    Display_Emi_Pelunasan.Focus()
    'End Sub

    'Private Sub PelunasanHutangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelunasanHutangToolStripMenuItem.Click
    '    Display_Emi_Pelunasan_Hutang.StartPosition = FormStartPosition.CenterScreen

    '    Display_Emi_Pelunasan_Hutang.MdiParent = Me
    '    Display_Emi_Pelunasan_Hutang.Show()
    '    Display_Emi_Pelunasan_Hutang.Focus()
    'End Sub

    'Private Sub SupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupplierToolStripMenuItem.Click
    '    'EMI_Hutang_PO_Summary_Data.StartPosition = FormStartPosition.CenterScreen

    '    'EMI_Hutang_PO_Summary_Data.MdiParent = Me
    '    'EMI_Hutang_PO_Summary_Data.Show()
    '    'EMI_Hutang_PO_Summary_Data.Focus()
    'End Sub

    'Private Sub AgentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AgentToolStripMenuItem.Click
    '    'Laporan_Hutang_Biaya_Import_Summary_Data.StartPosition = FormStartPosition.CenterScreen

    '    'Laporan_Hutang_Biaya_Import_Summary_Data.MdiParent = Me
    '    'Laporan_Hutang_Biaya_Import_Summary_Data.Show()
    '    'Laporan_Hutang_Biaya_Import_Summary_Data.Focus()
    'End Sub

    ''== LAPORAN =='

    'Private Sub LaporanPurchaseOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanPurchaseOrderToolStripMenuItem.Click
    '    Laporan_Purchase_Order.StartPosition = FormStartPosition.CenterScreen

    '    Laporan_Purchase_Order.MdiParent = Me
    '    Laporan_Purchase_Order.Show()
    '    Laporan_Purchase_Order.Focus()
    'End Sub

    'Private Sub LaporanPurchaseRequisitionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanPurchaseRequisitionToolStripMenuItem.Click
    '    Laporan_Purchase_Requisition.StartPosition = FormStartPosition.CenterScreen

    '    Laporan_Purchase_Requisition.MdiParent = Me
    '    Laporan_Purchase_Requisition.Show()
    '    Laporan_Purchase_Requisition.Focus()
    'End Sub

    'Private Sub HPPImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HPPImportToolStripMenuItem.Click
    '    Laporan_HPP_Import.StartPosition = FormStartPosition.CenterScreen

    '    Laporan_HPP_Import.MdiParent = Me
    '    Laporan_HPP_Import.Show()
    '    Laporan_HPP_Import.Focus()
    'End Sub

    'Private Sub HPPLokalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HPPLokalToolStripMenuItem.Click
    '    Laporan_HPP_Local.StartPosition = FormStartPosition.CenterScreen

    '    Laporan_HPP_Local.MdiParent = Me
    '    Laporan_HPP_Local.Show()
    '    Laporan_HPP_Local.Focus()
    'End Sub

    'Private Sub PembelianToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles PembelianToolStripMenuItem3.Click

    '    EMI_PO_Pembelian_Display2.StartPosition = FormStartPosition.CenterScreen

    '    EMI_PO_Pembelian_Display2.MdiParent = Me
    '    EMI_PO_Pembelian_Display2.Show()
    '    EMI_PO_Pembelian_Display2.Focus()
    'End Sub

    'Private Sub ValidasiBarangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiBarangMasukToolStripMenuItem.Click
    '    Emi_Selisih_Barang_Masuk_Display.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Selisih_Barang_Masuk_Display.MdiParent = Me
    '    Emi_Selisih_Barang_Masuk_Display.Show()
    '    Emi_Selisih_Barang_Masuk_Display.Focus()
    'End Sub

    Private Sub OrderProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrderProduksiToolStripMenuItem.Click
        EMI_Production_Order.StartPosition = FormStartPosition.CenterScreen

        EMI_Production_Order.MdiParent = Me
        EMI_Production_Order.Show()
        EMI_Production_Order.Focus()
    End Sub

    'Private Sub OrderIndependentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrderIndependentToolStripMenuItem.Click
    '    EMI_Independent_Order.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Independent_Order.MdiParent = Me
    '    EMI_Independent_Order.Show()
    '    EMI_Independent_Order.Focus()
    'End Sub

    'Private Sub JadwalProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JadwalProduksiToolStripMenuItem.Click
    '    EMI_Schedule.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Schedule.MdiParent = Me
    '    EMI_Schedule.Show()
    '    EMI_Schedule.Focus()
    'End Sub

    'Private Sub CompareWorkCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompareWorkCenterToolStripMenuItem.Click
    '    EMI_Compare_Work_Center.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Compare_Work_Center.MdiParent = Me
    '    EMI_Compare_Work_Center.Show()
    '    EMI_Compare_Work_Center.Focus()
    'End Sub

    'Private Sub HPPProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HPPProduksiToolStripMenuItem.Click
    '    EMI_HPP_Production.StartPosition = FormStartPosition.CenterScreen

    '    EMI_HPP_Production.MdiParent = Me
    '    EMI_HPP_Production.Show()
    '    EMI_HPP_Production.Focus()
    'End Sub

    'Private Sub ActualBiayaProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualBiayaProduksiToolStripMenuItem.Click
    '    EMI_Transaksi_Actual_Biaya_Produksi.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Transaksi_Actual_Biaya_Produksi.MdiParent = Me
    '    EMI_Transaksi_Actual_Biaya_Produksi.Show()
    '    EMI_Transaksi_Actual_Biaya_Produksi.Focus()
    'End Sub

    'Private Sub WorkCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WorkCenterToolStripMenuItem.Click
    '    EMI_Transaksi_Work_Center.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Transaksi_Work_Center.MdiParent = Me
    '    EMI_Transaksi_Work_Center.Show()
    '    EMI_Transaksi_Work_Center.Focus()
    'End Sub

    'Private Sub WorkCenterPerbulanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WorkCenterPerbulanToolStripMenuItem.Click
    '    EMI_Transaksi_Work_Center_PerBulan.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Transaksi_Work_Center_PerBulan.MdiParent = Me
    '    EMI_Transaksi_Work_Center_PerBulan.Show()
    '    EMI_Transaksi_Work_Center_PerBulan.Focus()
    'End Sub

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

    'Private Sub TransferQualityToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferQualityToolStripMenuItem.Click
    '    Emi_Transfer_Quality_Production.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Transfer_Quality_Production.MdiParent = Me
    '    Emi_Transfer_Quality_Production.Show()
    '    Emi_Transfer_Quality_Production.Focus()
    'End Sub

    'Private Sub HasilPengeluaranBahanBakuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HasilPengeluaranBahanBakuToolStripMenuItem.Click
    '    EMI_Display_Pengeluaran_Bahan_Baku.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Pengeluaran_Bahan_Baku.MdiParent = Me
    '    EMI_Display_Pengeluaran_Bahan_Baku.Show()
    '    EMI_Display_Pengeluaran_Bahan_Baku.Focus()
    'End Sub

    'Private Sub HasilProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HasilProduksiToolStripMenuItem.Click
    '    EMI_Display_Hasil_Produksi.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Hasil_Produksi.MdiParent = Me
    '    EMI_Display_Hasil_Produksi.Show()
    '    EMI_Display_Hasil_Produksi.Focus()
    'End Sub

    'Private Sub HasilProduksiFGToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HasilProduksiFGToolStripMenuItem.Click
    '    EMI_Display_Hasil_ProduksiFG.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Hasil_ProduksiFG.MdiParent = Me
    '    EMI_Display_Hasil_ProduksiFG.Show()
    '    EMI_Display_Hasil_ProduksiFG.Focus()
    'End Sub

    'Private Sub BarcodeProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarcodeProduksiToolStripMenuItem.Click
    '    Emi_Production_Barcode.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Production_Barcode.MdiParent = Me
    '    Emi_Production_Barcode.Show()
    '    Emi_Production_Barcode.Focus()
    'End Sub

    'Private Sub ActualBiayaProduksiToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ActualBiayaProduksiToolStripMenuItem1.Click
    '    EMI_Transaksi_Actual_Biaya_Produksi_Display.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Transaksi_Actual_Biaya_Produksi_Display.MdiParent = Me
    '    EMI_Transaksi_Actual_Biaya_Produksi_Display.Show()
    '    EMI_Transaksi_Actual_Biaya_Produksi_Display.Focus()
    'End Sub

    'Private Sub HasilProduksiToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HasilProduksiToolStripMenuItem1.Click
    '    EMI_Display_Production_Result.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Production_Result.MdiParent = Me
    '    EMI_Display_Production_Result.Show()
    '    EMI_Display_Production_Result.Focus()
    'End Sub

    'Private Sub SplitProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SplitProduksiToolStripMenuItem.Click
    '    EMI_Display_Split_Production_Order.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Split_Production_Order.MdiParent = Me
    '    EMI_Display_Split_Production_Order.Show()
    '    EMI_Display_Split_Production_Order.Focus()
    'End Sub

    'Private Sub RequestMaterialToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RequestMaterialToolStripMenuItem1.Click
    '    EMI_Request_Material_List.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Request_Material_List.MdiParent = Me
    '    EMI_Request_Material_List.Show()
    '    EMI_Request_Material_List.Focus()
    'End Sub

    'Private Sub OrderProduksiToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OrderProduksiToolStripMenuItem1.Click
    '    EMI_Production_Order_Summary_Data.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Production_Order_Summary_Data.MdiParent = Me
    '    EMI_Production_Order_Summary_Data.Show()
    '    EMI_Production_Order_Summary_Data.Focus()
    'End Sub

    'Private Sub ActualBiayaProduksiToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ActualBiayaProduksiToolStripMenuItem2.Click
    '    Laporan_Actual_Biaya_Produksi.StartPosition = FormStartPosition.CenterScreen

    '    Laporan_Actual_Biaya_Produksi.MdiParent = Me
    '    Laporan_Actual_Biaya_Produksi.Show()
    '    Laporan_Actual_Biaya_Produksi.Focus()
    'End Sub

    'Private Sub LaporanGIGRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanGIGRToolStripMenuItem.Click
    '    Laporan_GI_GR.StartPosition = FormStartPosition.CenterScreen

    '    Laporan_GI_GR.MdiParent = Me
    '    Laporan_GI_GR.Show()
    '    Laporan_GI_GR.Focus()
    'End Sub

    'Private Sub LaporanPenggunaanBahanBakuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanPenggunaanBahanBakuToolStripMenuItem.Click
    '    Laporan_Summary_Usage_RM.StartPosition = FormStartPosition.CenterScreen

    '    Laporan_Summary_Usage_RM.MdiParent = Me
    '    Laporan_Summary_Usage_RM.Show()
    '    Laporan_Summary_Usage_RM.Focus()
    'End Sub

    'Private Sub TesPrintToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    TesPrint.StartPosition = FormStartPosition.CenterScreen

    '    TesPrint.MdiParent = Me
    '    TesPrint.Show()
    '    TesPrint.Focus()
    'End Sub

    'Private Sub SetPRinterToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    Global_Setting.StartPosition = FormStartPosition.CenterScreen

    '    Global_Setting.MdiParent = Me
    '    Global_Setting.Show()
    '    Global_Setting.Focus()
    'End Sub

    Private Sub AsdaToolStripMenuItem_Click(sender As Object, e As EventArgs)
        EMI_Display_Transfer_Tidak_Timbang.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Transfer_Tidak_Timbang.MdiParent = Me
        EMI_Display_Transfer_Tidak_Timbang.Show()
        EMI_Display_Transfer_Tidak_Timbang.Focus()
    End Sub

    'Private Sub HPPToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HPPToolStripMenuItem1.Click
    '    Laporan_HPP.StartPosition = FormStartPosition.CenterScreen

    '    Laporan_HPP.MdiParent = Me
    '    Laporan_HPP.Show()
    '    Laporan_HPP.Focus()
    'End Sub

    'Private Sub TransferQualityToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles TransferQualityToolStripMenuItem1.Click
    '    EMI_Display_QC.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_QC.MdiParent = Me
    '    EMI_Display_QC.Show()
    '    EMI_Display_QC.Focus()
    'End Sub

    'Private Sub DisplayForecastOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayForecastOrderToolStripMenuItem.Click
    '    EMI_Display_ForecastOrder.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_ForecastOrder.MdiParent = Me
    '    EMI_Display_ForecastOrder.Show()
    '    EMI_Display_ForecastOrder.Focus()
    'End Sub

    'Private Sub DisplayLogForecastToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayLogForecastToolStripMenuItem.Click
    '    EMI_Display_Log_ForecastOrder.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Log_ForecastOrder.MdiParent = Me
    '    EMI_Display_Log_ForecastOrder.Show()
    '    EMI_Display_Log_ForecastOrder.Focus()
    'End Sub

    'Private Sub DisplayLogMaterialRequisitionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayLogMaterialRequisitionToolStripMenuItem.Click
    '    EMI_Display_Log_MaterialRequisition.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Log_MaterialRequisition.MdiParent = Me
    '    EMI_Display_Log_MaterialRequisition.Show()
    '    EMI_Display_Log_MaterialRequisition.Focus()
    'End Sub

    'Private Sub DisplayOrderPlanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayOrderPlanToolStripMenuItem.Click
    '    EMI_Display_OrderPlan.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_OrderPlan.MdiParent = Me
    '    EMI_Display_OrderPlan.Show()
    '    EMI_Display_OrderPlan.Focus()
    'End Sub

    'Private Sub ForecastOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ForecastOrderToolStripMenuItem.Click
    '    EMI_Transaksi_ForecastOrder.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Transaksi_ForecastOrder.MdiParent = Me
    '    EMI_Transaksi_ForecastOrder.Show()
    '    EMI_Transaksi_ForecastOrder.Focus()
    'End Sub

    'Private Sub SalesForecastingByPPICToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalesForecastingByPPICToolStripMenuItem.Click
    '    EMI_Transaksi_ForecastOrder.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Transaksi_ForecastOrder.MdiParent = Me
    '    EMI_Transaksi_ForecastOrder.Show()
    '    EMI_Transaksi_ForecastOrder.Focus()
    'End Sub

    'Private Sub MaterialRequisitionByPPICToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MaterialRequisitionByPPICToolStripMenuItem.Click
    '    EMI_Transaksi_MaterialRequisition.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Transaksi_MaterialRequisition.MdiParent = Me
    '    EMI_Transaksi_MaterialRequisition.Show()
    '    EMI_Transaksi_MaterialRequisition.Focus()
    'End Sub

    'Private Sub MaterialRequisitionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MaterialRequisitionToolStripMenuItem.Click
    '    EMI_Transaksi_MaterialRequisition.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Transaksi_MaterialRequisition.MdiParent = Me
    '    EMI_Transaksi_MaterialRequisition.Show()
    '    EMI_Transaksi_MaterialRequisition.Focus()
    'End Sub

    'Private Sub DisplayTransaksiBiayaLokalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayTransaksiBiayaLokalToolStripMenuItem.Click
    '    EMI_Display_Transaksi_Biaya_Lokal.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Transaksi_Biaya_Lokal.MdiParent = Me
    '    EMI_Display_Transaksi_Biaya_Lokal.Show()
    '    EMI_Display_Transaksi_Biaya_Lokal.Focus()
    'End Sub

    'Private Sub AsdasdasToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    Transfer_Stock_QC.StartPosition = FormStartPosition.CenterScreen

    '    Transfer_Stock_QC.MdiParent = Me
    '    Transfer_Stock_QC.Show()
    '    Transfer_Stock_QC.Focus()
    'End Sub

    'Private Sub BudgetingWorkCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BudgetingWorkCenterToolStripMenuItem.Click
    '    EMI_Budgeting_Work_Center.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Budgeting_Work_Center.MdiParent = Me
    '    EMI_Budgeting_Work_Center.Show()
    '    EMI_Budgeting_Work_Center.Focus()
    'End Sub

    'Private Sub TimbangUNloadingToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    Emi_Display_Timbang_FloorScale.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Display_Timbang_FloorScale.MdiParent = Me
    '    Emi_Display_Timbang_FloorScale.Show()
    '    Emi_Display_Timbang_FloorScale.Focus()
    'End Sub

    'Private Sub MasterMesinToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterMesinToolStripMenuItem.Click
    '    Master_Mesin.StartPosition = FormStartPosition.CenterScreen

    '    Master_Mesin.MdiParent = Me
    '    Master_Mesin.Show()
    '    Master_Mesin.Focus()
    'End Sub

    'Private Sub MasterWorkCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterWorkCenterToolStripMenuItem.Click
    '    Master_Work_Center.StartPosition = FormStartPosition.CenterScreen

    '    Master_Work_Center.MdiParent = Me
    '    Master_Work_Center.Show()
    '    Master_Work_Center.Focus()
    'End Sub

    'Private Sub SplitStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SplitStockToolStripMenuItem.Click
    '    Emi_Split_Stock_QC.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Split_Stock_QC.MdiParent = Me
    '    Emi_Split_Stock_QC.Show()
    '    Emi_Split_Stock_QC.Focus()
    'End Sub

    'Private Sub DisplaySplitStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplaySplitStockToolStripMenuItem.Click
    '    Emi_Display_Tf_Stock_QC.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Display_Tf_Stock_QC.MdiParent = Me
    '    Emi_Display_Tf_Stock_QC.Show()
    '    Emi_Display_Tf_Stock_QC.Focus()
    'End Sub

    'Private Sub ValidasiHPPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiHPPToolStripMenuItem.Click
    '    EMI_Display_Validasi_HPP_Produksi.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Validasi_HPP_Produksi.MdiParent = Me
    '    EMI_Display_Validasi_HPP_Produksi.Show()
    '    EMI_Display_Validasi_HPP_Produksi.Focus()
    'End Sub

    Private Sub TimbangToolStripMenuItem_Click(sender As Object, e As EventArgs)
        EMI_Display_Transfer.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Transfer.MdiParent = Me
        EMI_Display_Transfer.Show()
        EMI_Display_Transfer.Focus()
    End Sub

    'Private Sub GlobalSettingToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    Global_Setting.StartPosition = FormStartPosition.CenterScreen

    '    Global_Setting.MdiParent = Me
    '    Global_Setting.Show()
    '    Global_Setting.Focus()
    'End Sub

    'Private Sub TransferTableDatabaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferTableDatabaseToolStripMenuItem.Click
    '    SyncMenus.StartPosition = FormStartPosition.CenterScreen

    '    SyncMenus.MdiParent = Me
    '    SyncMenus.Show()
    '    SyncMenus.Focus()
    'End Sub

    'Private Sub IncommingToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    EMI_Display_Timbang.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Timbang.MdiParent = Me
    '    EMI_Display_Timbang.Show()
    '    EMI_Display_Timbang.Focus()
    'End Sub

    'Private Sub DisplayPalletMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayPalletMasukToolStripMenuItem.Click
    '    EMI_Display_Pallet_Masuk.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Pallet_Masuk.MdiParent = Me
    '    EMI_Display_Pallet_Masuk.Show()
    '    EMI_Display_Pallet_Masuk.Focus()
    'End Sub

    'Private Sub CetakUlangPalletMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangPalletMasukToolStripMenuItem.Click
    '    EMI_Display_Pallet_Masuk_Data.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Pallet_Masuk_Data.MdiParent = Me
    '    EMI_Display_Pallet_Masuk_Data.Show()
    '    EMI_Display_Pallet_Masuk_Data.Focus()
    'End Sub

    'Private Sub PembayaranBiayaProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    Pembayaran_Biaya_Produksi.StartPosition = FormStartPosition.CenterScreen

    '    Pembayaran_Biaya_Produksi.MdiParent = Me
    '    Pembayaran_Biaya_Produksi.Show()
    '    Pembayaran_Biaya_Produksi.Focus()
    'End Sub


    'Private Sub MToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MToolStripMenuItem.Click
    '    EMI_Master_Meteran.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Master_Meteran.MdiParent = Me
    '    EMI_Master_Meteran.Show()
    '    EMI_Master_Meteran.Focus()
    'End Sub

    'Private Sub BindingMeteranToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BindingMeteranToolStripMenuItem.Click
    '    EMI_Binding_Meteran.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Binding_Meteran.MdiParent = Me
    '    EMI_Binding_Meteran.Show()
    '    EMI_Binding_Meteran.Focus()
    'End Sub

    'Private Sub PersentaseBudgetingWorkCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PersentaseBudgetingWorkCenterToolStripMenuItem.Click
    '    EMI_Persentase_Budgeting_WorkCenter.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Persentase_Budgeting_WorkCenter.MdiParent = Me
    '    EMI_Persentase_Budgeting_WorkCenter.Show()
    '    EMI_Persentase_Budgeting_WorkCenter.Focus()
    'End Sub

    'Private Sub ValidasiBudgetWorkCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiBudgetWorkCenterToolStripMenuItem.Click
    '    EMI_Validasi_Budget_Work_Center.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Validasi_Budget_Work_Center.MdiParent = Me
    '    EMI_Validasi_Budget_Work_Center.Show()
    '    EMI_Validasi_Budget_Work_Center.Focus()
    'End Sub

    'Private Sub DisplayValidasiBudgetWorkCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayValidasiBudgetWorkCenterToolStripMenuItem.Click
    '    Display_Validasi_Budget_Work_Center.StartPosition = FormStartPosition.CenterScreen

    '    Display_Validasi_Budget_Work_Center.MdiParent = Me
    '    Display_Validasi_Budget_Work_Center.Show()
    '    Display_Validasi_Budget_Work_Center.Focus()
    'End Sub

    'Private Sub MasterJenisBiayaProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterJenisBiayaProduksiToolStripMenuItem.Click
    '    Master_Jenis_Biaya_Produksi.StartPosition = FormStartPosition.CenterScreen

    '    Master_Jenis_Biaya_Produksi.MdiParent = Me
    '    Master_Jenis_Biaya_Produksi.Show()
    '    Master_Jenis_Biaya_Produksi.Focus()
    'End Sub

    'Private Sub DisplayBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayBarangToolStripMenuItem.Click
    '    Display_Barang.StartPosition = FormStartPosition.CenterScreen

    '    Display_Barang.MdiParent = Me
    '    Display_Barang.Show()
    '    Display_Barang.Focus()
    'End Sub


    'Private Sub ValidasiMaterialToMaterialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiMaterialToMaterialToolStripMenuItem.Click
    '    EMI_Validasi_Tf_Material_To_Material.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Validasi_Tf_Material_To_Material.MdiParent = Me
    '    EMI_Validasi_Tf_Material_To_Material.Show()
    '    EMI_Validasi_Tf_Material_To_Material.Focus()
    'End Sub

    Private Sub TransferStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferStockToolStripMenuItem.Click
        Transfer_Stock_3.StartPosition = FormStartPosition.CenterScreen

        Transfer_Stock_3.MdiParent = Me
        Transfer_Stock_3.Show()
        Transfer_Stock_3.Focus()
    End Sub

    'Private Sub SummaryBarangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SummaryBarangMasukToolStripMenuItem.Click
    '    EMI_Barang_Masuk_Summary_Data.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Barang_Masuk_Summary_Data.MdiParent = Me
    '    EMI_Barang_Masuk_Summary_Data.Show()
    '    EMI_Barang_Masuk_Summary_Data.Focus()
    'End Sub

    'Private Sub PengeluaranBahanBakuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PengeluaranBahanBakuToolStripMenuItem.Click
    '    EMI_Display_Pengeluaran_Bahan_Baku.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Pengeluaran_Bahan_Baku.MdiParent = Me
    '    EMI_Display_Pengeluaran_Bahan_Baku.Show()
    '    EMI_Display_Pengeluaran_Bahan_Baku.Focus()
    'End Sub

    Private Sub ControllingProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ControllingProduksiToolStripMenuItem.Click
        EMI_Controlling_Produksi.StartPosition = FormStartPosition.CenterScreen

        EMI_Controlling_Produksi.MdiParent = Me
        EMI_Controlling_Produksi.Show()
        EMI_Controlling_Produksi.Focus()
    End Sub

    'Private Sub ControllingProduksiLamaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ControllingProduksiLamaToolStripMenuItem.Click
    '    EMI_Controlling_Produksi_Lama.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Controlling_Produksi_Lama.MdiParent = Me
    '    EMI_Controlling_Produksi_Lama.Show()
    '    EMI_Controlling_Produksi_Lama.Focus()
    'End Sub

    'Private Sub MasterRoutingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterRoutingToolStripMenuItem.Click
    '    Master_Routing.StartPosition = FormStartPosition.CenterScreen

    '    Master_Routing.MdiParent = Me
    '    Master_Routing.Show()
    '    Master_Routing.Focus()
    'End Sub

    'Private Sub ValidasiHPPToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ValidasiHPPToolStripMenuItem1.Click
    '    EMI_Display_Validasi_HPP_Produksi.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Validasi_HPP_Produksi.MdiParent = Me
    '    EMI_Display_Validasi_HPP_Produksi.Show()
    '    EMI_Display_Validasi_HPP_Produksi.Focus()
    'End Sub

    Private Sub MasterBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterBarangToolStripMenuItem.Click
        Master_Barang_New.StartPosition = FormStartPosition.CenterScreen

        Master_Barang_New.MdiParent = Me
        Master_Barang_New.Show()
        Master_Barang_New.Focus()
    End Sub

    'Private Sub CompareBudgetingWorkCenterFIXToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompareBudgetingWorkCenterFIXToolStripMenuItem.Click
    '    EMI_Compare_Budgeting.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Compare_Budgeting.MdiParent = Me
    '    EMI_Compare_Budgeting.Show()
    '    EMI_Compare_Budgeting.Focus()
    'End Sub

    Private Sub TransferStockToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles TransferStockToolStripMenuItem1.Click
        Transfer_Stock_3.StartPosition = FormStartPosition.CenterScreen

        Transfer_Stock_3.MenuAsal = "TRANSFER_STOCK"
        Transfer_Stock_3.MdiParent = Me
        Transfer_Stock_3.Show()
        Transfer_Stock_3.Focus()
    End Sub

    Private Sub DisplayTransferStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayTransferStockToolStripMenuItem.Click
        EMI_Display_Transfer.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Transfer.MdiParent = Me
        EMI_Display_Transfer.Show()
        EMI_Display_Transfer.Focus()
    End Sub

    Private Sub DisplayTransferStockTidakTimbangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayTransferStockTidakTimbangToolStripMenuItem.Click
        EMI_Display_Transfer_Tidak_Timbang.StartPosition = FormStartPosition.CenterScreen

        EMI_Display_Transfer_Tidak_Timbang.MdiParent = Me
        EMI_Display_Transfer_Tidak_Timbang.Show()
        EMI_Display_Transfer_Tidak_Timbang.Focus()
    End Sub

    'Private Sub DipToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DipToolStripMenuItem.Click
    '    Emi_Display_Transfer_Stock.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Display_Transfer_Stock.MdiParent = Me
    '    Emi_Display_Transfer_Stock.Show()
    '    Emi_Display_Transfer_Stock.Focus()
    'End Sub

    'Private Sub ValidasiSplitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiSplitToolStripMenuItem.Click
    '    Emi_Display_Tf_Stock_QC.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Display_Tf_Stock_QC.MdiParent = Me
    '    Emi_Display_Tf_Stock_QC.Show()
    '    Emi_Display_Tf_Stock_QC.Focus()
    'End Sub

    'Private Sub SplitBarangToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SplitBarangToolStripMenuItem1.Click
    '    Emi_Split_Stock_QC.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Split_Stock_QC.MdiParent = Me
    '    Emi_Split_Stock_QC.Show()
    '    Emi_Split_Stock_QC.Focus()
    'End Sub

    'Private Sub DisplaySplitBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplaySplitBarangToolStripMenuItem.Click
    '    EMI_Display_Split_Stock.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Split_Stock.MdiParent = Me
    '    EMI_Display_Split_Stock.Show()
    '    EMI_Display_Split_Stock.Focus()
    'End Sub

    'Private Sub SYNCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SYNCToolStripMenuItem.Click
    '    Server_Sinkronasi_B2B.StartPosition = FormStartPosition.CenterScreen

    '    Server_Sinkronasi_B2B.MdiParent = Me
    '    Server_Sinkronasi_B2B.Show()
    '    Server_Sinkronasi_B2B.Focus()
    'End Sub

    'Private Sub TFQualityToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TFQualityToolStripMenuItem.Click
    '    EMI_Transfer_Quality_QC.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Transfer_Quality_QC.MdiParent = Me
    '    EMI_Transfer_Quality_QC.Show()
    '    EMI_Transfer_Quality_QC.Focus()
    'End Sub

    'Private Sub PengeluaranBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PengeluaranBarangToolStripMenuItem.Click
    '    Pengeluaran_Barang.StartPosition = FormStartPosition.CenterScreen

    '    Pengeluaran_Barang.MdiParent = Me
    '    Pengeluaran_Barang.Show()
    '    Pengeluaran_Barang.Focus()
    'End Sub

    Private Sub LaporanBiayaProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanBiayaProduksiToolStripMenuItem.Click
        Laporan_Biaya_Produksi.StartPosition = FormStartPosition.CenterScreen

        Laporan_Biaya_Produksi.MdiParent = Me
        Laporan_Biaya_Produksi.Show()
        Laporan_Biaya_Produksi.Focus()
    End Sub

    'Private Sub LaporanGIGRToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LaporanGIGRToolStripMenuItem1.Click
    '    Laporan_GI_GR.StartPosition = FormStartPosition.CenterScreen

    '    Laporan_GI_GR.MdiParent = Me
    '    Laporan_GI_GR.Show()
    '    Laporan_GI_GR.Focus()
    'End Sub

    'Private Sub LaporanHPPPerBatchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanHPPPerBatchToolStripMenuItem.Click
    '    Laporan_HPP_Per_Batch.StartPosition = FormStartPosition.CenterScreen

    '    Laporan_HPP_Per_Batch.MdiParent = Me
    '    Laporan_HPP_Per_Batch.Show()
    '    Laporan_HPP_Per_Batch.Focus()
    'End Sub

    'Private Sub RequestMaterialGeneralToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RequestMaterialGeneralToolStripMenuItem.Click
    '    EMI_Request_Material_General.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Request_Material_General.MdiParent = Me
    '    EMI_Request_Material_General.Show()
    '    EMI_Request_Material_General.Focus()
    'End Sub

    'Private Sub AsdaToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AsdaToolStripMenuItem1.Click
    '    Tes_C.StartPosition = FormStartPosition.CenterScreen

    '    Tes_C.MdiParent = Me
    '    Tes_C.Show()
    '    Tes_C.Focus()
    'End Sub

    Private Sub LaporanBiayaProduksiToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LaporanBiayaProduksiToolStripMenuItem1.Click
        Laporan_Biaya_Produksi.StartPosition = FormStartPosition.CenterScreen

        Laporan_Biaya_Produksi.MdiParent = Me
        Laporan_Biaya_Produksi.Show()
        Laporan_Biaya_Produksi.Focus()
    End Sub

    'Private Sub PurchaseOrderSUBToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PurchaseOrderSUBToolStripMenuItem.Click
    '    EMI_PO_Pembelian_Display_Sub.StartPosition = FormStartPosition.CenterScreen

    '    EMI_PO_Pembelian_Display_Sub.MdiParent = Me
    '    EMI_PO_Pembelian_Display_Sub.Show()
    '    EMI_PO_Pembelian_Display_Sub.Focus()
    'End Sub

    'Private Sub LaporanSummarySubPOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanSummarySubPOToolStripMenuItem.Click
    '    EMI_Pembelian_PO_Summary_Data.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pembelian_PO_Summary_Data.MdiParent = Me
    '    EMI_Pembelian_PO_Summary_Data.Show()
    '    EMI_Pembelian_PO_Summary_Data.Focus()
    'End Sub

    'Private Sub DisplayPOIndukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayPOIndukToolStripMenuItem.Click
    '    Display_Summary_POInduk.StartPosition = FormStartPosition.CenterScreen

    '    Display_Summary_POInduk.MdiParent = Me
    '    Display_Summary_POInduk.Show()
    '    Display_Summary_POInduk.Focus()
    'End Sub

    'Private Sub SelisihBarangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelisihBarangMasukToolStripMenuItem.Click
    '    Emi_Selisih_Barang_Masuk_Display.MdiParent = Me
    '    Emi_Selisih_Barang_Masuk_Display.Show()
    '    Emi_Selisih_Barang_Masuk_Display.Focus()
    'End Sub

    'Private Sub LaporanPurchaseOrderIndukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanPurchaseOrderIndukToolStripMenuItem.Click
    '    Laporan_Purchase_Order_Induk.MdiParent = Me
    '    Laporan_Purchase_Order_Induk.Show()
    '    Laporan_Purchase_Order_Induk.Focus()
    'End Sub

    'Private Sub DisplayProductionResultToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayProductionResultToolStripMenuItem.Click
    '    EMI_Display_Production_Result.MdiParent = Me
    '    EMI_Display_Production_Result.Show()
    '    EMI_Display_Production_Result.Focus()
    'End Sub

    'Private Sub PengeluaranStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PengeluaranStockToolStripMenuItem.Click
    '    EMI_Pengeluaran_Stock.MdiParent = Me
    '    EMI_Pengeluaran_Stock.MenuAsal = "PENGELUARAN_STOCK"
    '    EMI_Pengeluaran_Stock.Show()
    '    EMI_Pengeluaran_Stock.Focus()
    'End Sub

    'Private Sub PengeluaranStockRejectedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PengeluaranStockRejectedToolStripMenuItem.Click
    '    EMI_Pengeluaran_Stock.MdiParent = Me
    '    EMI_Pengeluaran_Stock.MenuAsal = "PENGELUARAN_STOCK_REJECTED"
    '    EMI_Pengeluaran_Stock.Show()
    '    EMI_Pengeluaran_Stock.Focus()
    'End Sub

    'Private Sub PelunasanToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles PelunasanToolStripMenuItem4.Click
    '    EMI_Pelunasan.MdiParent = Me
    '    EMI_Pelunasan.Show()
    '    EMI_Pelunasan.Focus()
    'End Sub

    'Private Sub RoutingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RoutingToolStripMenuItem.Click
    '    Master_Routing.MdiParent = Me
    '    Master_Routing.Show()
    '    Master_Routing.Focus()
    'End Sub

    'Private Sub ValidasiPelunasanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiPelunasanToolStripMenuItem.Click
    '    EMI_Validasi_Pengeluaran_Stock.MdiParent = Me
    '    EMI_Validasi_Pengeluaran_Stock.Show()
    '    EMI_Validasi_Pengeluaran_Stock.Focus()
    'End Sub

    'Private Sub Pembelian2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Pembelian2ToolStripMenuItem.Click
    '    EMI_PO_Pembelian_Display2.MdiParent = Me
    '    EMI_PO_Pembelian_Display2.Show()
    '    EMI_PO_Pembelian_Display2.Focus()
    'End Sub



    'Private Sub ScanPalletDesktopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScanPalletDesktopToolStripMenuItem.Click
    '    EMI_Display_Barang_Masuk_Per_Pallet.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Barang_Masuk_Per_Pallet.MdiParent = Me
    '    EMI_Display_Barang_Masuk_Per_Pallet.Show()
    '    EMI_Display_Barang_Masuk_Per_Pallet.Focus()
    'End Sub

    'Private Sub TimbangKeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TimbangKeluarToolStripMenuItem.Click
    '    EMI_Display_Timbang.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Timbang.asal = "Unloading_Barang"
    '    EMI_Display_Timbang.filter_tambahan = "timbang_keluar='Y'"

    '    EMI_Display_Timbang.MdiParent = Me
    '    EMI_Display_Timbang.Show()
    '    EMI_Display_Timbang.Focus()
    'End Sub

    Private Sub LaporanBiayaProduksiToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles LaporanBiayaProduksiToolStripMenuItem2.Click
        Laporan_Biaya_Produksi.StartPosition = FormStartPosition.CenterScreen

        Laporan_Biaya_Produksi.MdiParent = Me
        Laporan_Biaya_Produksi.Show()
        Laporan_Biaya_Produksi.Focus()
    End Sub

    'Private Sub UpdateJurnalPembelianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateJurnalPembelianToolStripMenuItem.Click
    '    FormTest_UpdateJurnal.StartPosition = FormStartPosition.CenterScreen

    '    FormTest_UpdateJurnal.MdiParent = Me
    '    FormTest_UpdateJurnal.Show()
    '    FormTest_UpdateJurnal.Focus()
    'End Sub

    'Private Sub PAJAKToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PAJAKToolStripMenuItem.Click
    '    Master_Pajak.StartPosition = FormStartPosition.CenterScreen

    '    Master_Pajak.MdiParent = Me
    '    Master_Pajak.Show()
    '    Master_Pajak.Focus()
    'End Sub

    'Private Sub MutasiBahanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MutasiBahanToolStripMenuItem.Click
    '    Laporan_Mutasi_Bahan.StartPosition = FormStartPosition.CenterScreen

    '    Laporan_Mutasi_Bahan.MdiParent = Me
    '    Laporan_Mutasi_Bahan.Show()
    '    Laporan_Mutasi_Bahan.Focus()
    'End Sub

    'Private Sub MulaiProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MulaiProduksiToolStripMenuItem.Click
    '    EMI_Display_Mulai_Produksi.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Mulai_Produksi.MdiParent = Me
    '    EMI_Display_Mulai_Produksi.Show()
    '    EMI_Display_Mulai_Produksi.Focus()
    'End Sub

    'Private Sub DisplaySplitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplaySplitToolStripMenuItem.Click
    '    EMI_Display_Split_Production_Order.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Split_Production_Order.MdiParent = Me
    '    EMI_Display_Split_Production_Order.Show()
    '    EMI_Display_Split_Production_Order.Focus()
    'End Sub

    'Private Sub HasilProduksiGIToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HasilProduksiGIToolStripMenuItem.Click
    '    EMI_Display_Hasil_ProduksiGI.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Hasil_ProduksiGI.MdiParent = Me
    '    EMI_Display_Hasil_ProduksiGI.Show()
    '    EMI_Display_Hasil_ProduksiGI.Focus()
    'End Sub

    'Private Sub HasilProduksiGRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HasilProduksiGRToolStripMenuItem.Click
    '    EMI_Display_Hasil_ProduksiGR.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Hasil_ProduksiGR.MdiParent = Me
    '    EMI_Display_Hasil_ProduksiGR.Show()
    '    EMI_Display_Hasil_ProduksiGR.Focus()
    'End Sub

    'Private Sub HasilProduksiToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles HasilProduksiToolStripMenuItem3.Click
    '    EMI_Display_Hasil_Produksi.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Hasil_Produksi.MdiParent = Me
    '    EMI_Display_Hasil_Produksi.Show()
    '    EMI_Display_Hasil_Produksi.Focus()
    'End Sub

    'Private Sub LaporanPersediaanBahanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanPersediaanBahanToolStripMenuItem.Click
    '    EMI_Laporan_Outstanding_PO_VS_Stock.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Laporan_Outstanding_PO_VS_Stock.MdiParent = Me
    '    EMI_Laporan_Outstanding_PO_VS_Stock.Show()
    '    EMI_Laporan_Outstanding_PO_VS_Stock.Focus()
    'End Sub

    'Private Sub ValidasiPengajuanSelesaiPRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiPengajuanSelesaiPRToolStripMenuItem.Click
    '    EMI_Validasi_Pengajuan_Selesai_PR.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Validasi_Pengajuan_Selesai_PR.MdiParent = Me
    '    EMI_Validasi_Pengajuan_Selesai_PR.Show()
    '    EMI_Validasi_Pengajuan_Selesai_PR.Focus()
    'End Sub

    'Private Sub ReturPembelianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturPembelianToolStripMenuItem.Click
    '    EMI_Retur_Pembelian.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Retur_Pembelian.MdiParent = Me
    '    EMI_Retur_Pembelian.Show()
    '    EMI_Retur_Pembelian.Focus()
    'End Sub

    'Private Sub DisplayTransferStockToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DisplayTransferStockToolStripMenuItem1.Click
    '    Emi_Display_Transfer_Stock.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Display_Transfer_Stock.MdiParent = Me
    '    Emi_Display_Transfer_Stock.Show()
    '    Emi_Display_Transfer_Stock.Focus()
    'End Sub

    'Private Sub DisplayReturPembelianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayReturPembelianToolStripMenuItem.Click
    '    Emi_Display_Retur_Pembelian.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Display_Retur_Pembelian.MdiParent = Me
    '    Emi_Display_Retur_Pembelian.Show()
    '    Emi_Display_Retur_Pembelian.Focus()
    'End Sub

    'Private Sub AdjustmentStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdjustmentStockToolStripMenuItem.Click
    '    Emi_Adj_Stock.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Adj_Stock.MdiParent = Me
    '    Emi_Adj_Stock.Show()
    '    Emi_Adj_Stock.Focus()
    'End Sub

    'Private Sub DisplayAdustmentStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayAdustmentStockToolStripMenuItem.Click
    '    Emi_Display_Adjusment_Stock.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Display_Adjusment_Stock.MdiParent = Me
    '    Emi_Display_Adjusment_Stock.Show()
    '    Emi_Display_Adjusment_Stock.Focus()
    'End Sub

    'Private Sub TmibangUnloadingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TmibangUnloadingToolStripMenuItem.Click
    '    Emi_Display_Timbang_FloorScale.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Display_Timbang_FloorScale.MdiParent = Me
    '    Emi_Display_Timbang_FloorScale.Show()
    '    Emi_Display_Timbang_FloorScale.Focus()
    'End Sub

    'Private Sub AssetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AssetToolStripMenuItem.Click
    '    EMI_Pembayaran_Di_Muka_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pembayaran_Di_Muka_Barang_Lain.MdiParent = Me
    '    EMI_Pembayaran_Di_Muka_Barang_Lain.Show()
    '    EMI_Pembayaran_Di_Muka_Barang_Lain.Focus()
    'End Sub

    'Private Sub BindingAssetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BindingAssetToolStripMenuItem.Click
    '    EMI_Pembayaran_Di_Muka_Binding_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pembayaran_Di_Muka_Binding_Barang_Lain.MdiParent = Me
    '    EMI_Pembayaran_Di_Muka_Binding_Barang_Lain.Show()
    '    EMI_Pembayaran_Di_Muka_Binding_Barang_Lain.Focus()
    'End Sub

    Private Sub TfStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TfStockToolStripMenuItem.Click
        Transfer_Stock_3.StartPosition = FormStartPosition.CenterScreen

        Transfer_Stock_3.MdiParent = Me
        Transfer_Stock_3.Show()
        Transfer_Stock_3.Focus()
    End Sub

    'Private Sub PengeluaranStockToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PengeluaranStockToolStripMenuItem1.Click
    '    EMI_Pengeluaran_Stock.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pengeluaran_Stock.MdiParent = Me
    '    EMI_Pengeluaran_Stock.Show()
    '    EMI_Pengeluaran_Stock.Focus()
    'End Sub




    'Private Sub ValidasiPelunasanToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ValidasiPelunasanToolStripMenuItem1.Click
    '    EMI_Validasi_Pengeluaran_Stock.MdiParent = Me
    '    EMI_Validasi_Pengeluaran_Stock.Show()
    '    EMI_Validasi_Pengeluaran_Stock.Focus()
    'End Sub

    'Private Sub PembelianBarangLainToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PembelianBarangLainToolStripMenuItem.Click
    '    EMI_PO_Pembelian_Display_Barang_Lain.MdiParent = Me
    '    EMI_PO_Pembelian_Display_Barang_Lain.Show()
    '    EMI_PO_Pembelian_Display_Barang_Lain.Focus()
    'End Sub

    'Private Sub PRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PRToolStripMenuItem.Click
    '    Purchase_Requisition_Barang_Lain.MdiParent = Me
    '    Purchase_Requisition_Barang_Lain.Show()
    '    Purchase_Requisition_Barang_Lain.Focus()
    'End Sub

    'Private Sub LaporanPenambahanStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanPenambahanStockToolStripMenuItem.Click
    '    Laporan_Penambahan_Stock_Barang.MdiParent = Me
    '    Laporan_Penambahan_Stock_Barang.Show()
    '    Laporan_Penambahan_Stock_Barang.Focus()
    'End Sub

    'Private Sub PRToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PRToolStripMenuItem1.Click
    '    EMI_Pembelian_PR_Summary_Data.MdiParent = Me
    '    EMI_Pembelian_PR_Summary_Data.Show()
    '    EMI_Pembelian_PR_Summary_Data.Focus()
    'End Sub

    'Private Sub POIndukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles POIndukToolStripMenuItem.Click
    '    Display_Summary_POInduk.MdiParent = Me
    '    Display_Summary_POInduk.Show()
    '    Display_Summary_POInduk.Focus()
    'End Sub

    'Private Sub SubPOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SubPOToolStripMenuItem.Click
    '    EMI_Pembelian_PO_Summary_Data.MdiParent = Me
    '    EMI_Pembelian_PO_Summary_Data.Show()
    '    EMI_Pembelian_PO_Summary_Data.Focus()
    'End Sub

    'Private Sub AsdasdassdToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AsdasdassdToolStripMenuItem.Click
    '    Import_Product_XML.MdiParent = Me
    '    Import_Product_XML.Show()
    '    Import_Product_XML.Focus()
    'End Sub

    'Private Sub PalletMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PalletMasukToolStripMenuItem.Click
    '    EMI_Display_Pallet_Masuk_Data.MdiParent = Me
    '    EMI_Display_Pallet_Masuk_Data.Show()
    '    EMI_Display_Pallet_Masuk_Data.Focus()
    'End Sub

    'Private Sub MasterBiayaLokalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterBiayaLokalToolStripMenuItem.Click
    '    Master_Detail_Biaya_Lokal.MdiParent = Me
    '    Master_Detail_Biaya_Lokal.Show()
    '    Master_Detail_Biaya_Lokal.Focus()
    'End Sub

    'Private Sub SyncToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SyncToolStripMenuItem1.Click
    '    Server_Sinkronasi_B2B.MdiParent = Me
    '    Server_Sinkronasi_B2B.Show()
    '    Server_Sinkronasi_B2B.Focus()
    'End Sub

    'Private Sub MasterKendaraanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterKendaraanToolStripMenuItem.Click
    '    Master_Kendaraan.MdiParent = Me
    '    Master_Kendaraan.Show()
    '    Master_Kendaraan.Focus()
    'End Sub

    'Private Sub TransaksiBiayaLokalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransaksiBiayaLokalToolStripMenuItem.Click
    '    EMI_Display_Transaksi_Biaya_Lokal.MdiParent = Me
    '    EMI_Display_Transaksi_Biaya_Lokal.Show()
    '    EMI_Display_Transaksi_Biaya_Lokal.Focus()
    'End Sub

    'Private Sub DelivaryOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DelivaryOrderToolStripMenuItem.Click
    '    DO_Reseller_New.MdiParent = Me
    '    DO_Reseller_New.Show()
    '    DO_Reseller_New.Focus()
    'End Sub

    'Private Sub DownPaymentPembelianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DownPaymentPembelianToolStripMenuItem.Click
    '    EMI_Pembayaran_Di_Muka.MdiParent = Me
    '    EMI_Pembayaran_Di_Muka.Show()
    '    EMI_Pembayaran_Di_Muka.Focus()
    'End Sub

    'Private Sub DisplayDownPaymentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayDownPaymentToolStripMenuItem.Click
    '    Emi_Display_Pembayaran_Di_Muka.MdiParent = Me
    '    Emi_Display_Pembayaran_Di_Muka.Show()
    '    Emi_Display_Pembayaran_Di_Muka.Focus()
    'End Sub

    'Private Sub DownPaymentAssetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DownPaymentAssetToolStripMenuItem.Click
    '    EMI_Pembayaran_Di_Muka_Barang_Lain.MdiParent = Me
    '    EMI_Pembayaran_Di_Muka_Barang_Lain.Show()
    '    EMI_Pembayaran_Di_Muka_Barang_Lain.Focus()
    'End Sub

    'Private Sub DownPaymentProyekToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DownPaymentProyekToolStripMenuItem.Click
    '    EMI_Pembayaran_Di_Muka_Proyek.MdiParent = Me
    '    EMI_Pembayaran_Di_Muka_Proyek.Show()
    '    EMI_Pembayaran_Di_Muka_Proyek.Focus()
    'End Sub

    'Private Sub DisplayDownPaymentAssetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayDownPaymentAssetToolStripMenuItem.Click
    '    Emi_Display_Pembayaran_Di_Muka_Barang_Lain.MdiParent = Me
    '    Emi_Display_Pembayaran_Di_Muka_Barang_Lain.Show()
    '    Emi_Display_Pembayaran_Di_Muka_Barang_Lain.Focus()
    'End Sub

    'Private Sub DisplayDownPaymentProyekToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayDownPaymentProyekToolStripMenuItem.Click
    '    Emi_Display_Pembayaran_Di_Muka_Proyek.MdiParent = Me
    '    Emi_Display_Pembayaran_Di_Muka_Proyek.Show()
    '    Emi_Display_Pembayaran_Di_Muka_Proyek.Focus()
    'End Sub

    'Private Sub LaporanBarangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanBarangMasukToolStripMenuItem.Click
    '    Emi_Laporan_Barang_Masuk.MdiParent = Me
    '    Emi_Laporan_Barang_Masuk.Show()
    '    Emi_Laporan_Barang_Masuk.Focus()
    'End Sub

    'Private Sub LaporanPembelianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanPembelianToolStripMenuItem.Click
    '    Emi_Laporan_Pembelian.MdiParent = Me
    '    Emi_Laporan_Pembelian.Show()
    '    Emi_Laporan_Pembelian.Focus()
    'End Sub

    'Private Sub DisplayPOIndukToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DisplayPOIndukToolStripMenuItem1.Click
    '    EMI_Pembelian_Summary_Data.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pembelian_Summary_Data.MdiParent = Me
    '    EMI_Pembelian_Summary_Data.Show()
    '    EMI_Pembelian_Summary_Data.Focus()
    'End Sub

    'Private Sub DisplaySubPOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplaySubPOToolStripMenuItem.Click
    '    EMI_Pembelian_PO_Summary_Data.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pembelian_PO_Summary_Data.MdiParent = Me
    '    EMI_Pembelian_PO_Summary_Data.Show()
    '    EMI_Pembelian_PO_Summary_Data.Focus()
    'End Sub

    'Private Sub DisplayPOIndukToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles DisplayPOIndukToolStripMenuItem2.Click
    '    Display_Summary_POInduk.StartPosition = FormStartPosition.CenterScreen

    '    Display_Summary_POInduk.MdiParent = Me
    '    Display_Summary_POInduk.Show()
    '    Display_Summary_POInduk.Focus()
    'End Sub

    'Private Sub LaporanPelunasanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanPelunasanToolStripMenuItem.Click
    '    Emi_Laporan_Pelunasan.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Laporan_Pelunasan.MdiParent = Me
    '    Emi_Laporan_Pelunasan.Show()
    '    Emi_Laporan_Pelunasan.Focus()
    'End Sub

    'Private Sub LaporanPOIndukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanPOIndukToolStripMenuItem.Click
    '    Emi_Laporan_PO.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Laporan_PO.MdiParent = Me
    '    Emi_Laporan_PO.Show()
    '    Emi_Laporan_PO.Focus()
    'End Sub

    'Private Sub LaporanSubPOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanSubPOToolStripMenuItem.Click
    '    Emi_Laporan_Sub_PO.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Laporan_Sub_PO.MdiParent = Me
    '    Emi_Laporan_Sub_PO.Show()
    '    Emi_Laporan_Sub_PO.Focus()
    'End Sub

    'Private Sub PenawaranToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles PenawaranToolStripMenuItem5.Click
    '    Laporan_Penawaran.StartPosition = FormStartPosition.CenterScreen

    '    Laporan_Penawaran.MdiParent = Me
    '    Laporan_Penawaran.Show()
    '    Laporan_Penawaran.Focus()
    'End Sub

    'Private Sub RequestMaterialSplitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RequestMaterialSplitToolStripMenuItem.Click
    '    EMI_Request_Material_List.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Request_Material_List.MdiParent = Me
    '    EMI_Request_Material_List.Show()
    '    EMI_Request_Material_List.Focus()
    'End Sub

    'Private Sub LaporanRequestMaterialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanRequestMaterialToolStripMenuItem.Click
    '    Emi_Laporan_Request_Material.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Laporan_Request_Material.MdiParent = Me
    '    Emi_Laporan_Request_Material.Show()
    '    Emi_Laporan_Request_Material.Focus()
    'End Sub

    'Private Sub BarangMasukPerpalletToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarangMasukPerpalletToolStripMenuItem.Click
    '    EMI_Display_Barang_Masuk_Per_Pallet.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Barang_Masuk_Per_Pallet.MdiParent = Me
    '    EMI_Display_Barang_Masuk_Per_Pallet.Show()
    '    EMI_Display_Barang_Masuk_Per_Pallet.Focus()
    'End Sub

    'Private Sub LaporanTransferStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanTransferStockToolStripMenuItem.Click
    '    Emi_Laporan_Transfer_Stock.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Laporan_Transfer_Stock.MdiParent = Me
    '    Emi_Laporan_Transfer_Stock.Show()
    '    Emi_Laporan_Transfer_Stock.Focus()
    'End Sub

    'Private Sub PelunasanProyekToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelunasanProyekToolStripMenuItem.Click
    '    Validasi_Pemb_Proyek.StartPosition = FormStartPosition.CenterScreen

    '    Validasi_Pemb_Proyek.MdiParent = Me
    '    Validasi_Pemb_Proyek.Show()
    '    Validasi_Pemb_Proyek.Focus()
    'End Sub

    Private Sub ValidasiPeneToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiPeneToolStripMenuItem.Click
        EMI_Validasi_GR.StartPosition = FormStartPosition.CenterScreen

        EMI_Validasi_GR.MdiParent = Me
        EMI_Validasi_GR.Show()
        EMI_Validasi_GR.Focus()
    End Sub

    'Private Sub PembelianAssetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PembelianAssetToolStripMenuItem.Click
    '    EMI_PO_Pembelian_Display2_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    EMI_PO_Pembelian_Display2_Barang_Lain.MdiParent = Me
    '    EMI_PO_Pembelian_Display2_Barang_Lain.Show()
    '    EMI_PO_Pembelian_Display2_Barang_Lain.Focus()
    'End Sub

    'Private Sub LaporanFinalGIGRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanFinalGIGRToolStripMenuItem.Click
    '    Emi_Laporan_Final_GI_GR.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Laporan_Final_GI_GR.MdiParent = Me
    '    Emi_Laporan_Final_GI_GR.Show()
    '    Emi_Laporan_Final_GI_GR.Focus()
    'End Sub

    'Private Sub LaporanSplitStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanSplitStockToolStripMenuItem.Click
    '    Emi_Laporan_Split_Stock.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Laporan_Split_Stock.MdiParent = Me
    '    Emi_Laporan_Split_Stock.Show()
    '    Emi_Laporan_Split_Stock.Focus()
    'End Sub

    'Private Sub PelunasanAssetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelunasanAssetToolStripMenuItem.Click
    '    EMI_Pelunasan_Asset.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pelunasan_Asset.MdiParent = Me
    '    EMI_Pelunasan_Asset.Show()
    '    EMI_Pelunasan_Asset.Focus()
    'End Sub

    'Private Sub DetailAccountToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DetailAccountToolStripMenuItem.Click
    '    Detail_Account_New.StartPosition = FormStartPosition.CenterScreen

    '    Detail_Account_New.MdiParent = Me
    '    Detail_Account_New.Show()
    '    Detail_Account_New.Focus()
    'End Sub

    'Private Sub PelunasanAssetToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PelunasanAssetToolStripMenuItem1.Click
    '    Display_Emi_Pelunasan_Asset.StartPosition = FormStartPosition.CenterScreen

    '    Display_Emi_Pelunasan_Asset.MdiParent = Me
    '    Display_Emi_Pelunasan_Asset.Show()
    '    Display_Emi_Pelunasan_Asset.Focus()
    'End Sub

    Private Sub DisplayValidasiPenerimaanBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayValidasiPenerimaanBarangToolStripMenuItem.Click
        EMI_Validasi_GR_Display.StartPosition = FormStartPosition.CenterScreen

        EMI_Validasi_GR_Display.MdiParent = Me
        EMI_Validasi_GR_Display.Show()
        EMI_Validasi_GR_Display.Focus()
    End Sub

    Private Sub ValidasiPenerimaanBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiPenerimaanBarangToolStripMenuItem.Click
        EMI_Validasi_GR.StartPosition = FormStartPosition.CenterScreen

        EMI_Validasi_GR.MenuAsal = "VALIDASI_GR"
        EMI_Validasi_GR.MdiParent = Me
        EMI_Validasi_GR.Show()
        EMI_Validasi_GR.Focus()
    End Sub

    'Private Sub DisplayBarangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayBarangMasukToolStripMenuItem.Click
    '    EMI_Barang_Masuk_Summary_Data.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Barang_Masuk_Summary_Data.MdiParent = Me
    '    EMI_Barang_Masuk_Summary_Data.Show()
    '    EMI_Barang_Masuk_Summary_Data.Focus()
    'End Sub

    'Private Sub PengeluaranStockToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles PengeluaranStockToolStripMenuItem3.Click
    '    EMI_Pengeluaran_Stock.MdiParent = Me
    '    EMI_Pengeluaran_Stock.MenuAsal = "PENGELUARAN_STOCK"
    '    EMI_Pengeluaran_Stock.Show()
    '    EMI_Pengeluaran_Stock.Focus()
    'End Sub

    'Private Sub PengeluaranStockRejectedToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PengeluaranStockRejectedToolStripMenuItem1.Click
    '    EMI_Pengeluaran_Stock.MdiParent = Me
    '    EMI_Pengeluaran_Stock.MenuAsal = "PENGELUARAN_STOCK_REJECTED"
    '    EMI_Pengeluaran_Stock.Show()
    '    EMI_Pengeluaran_Stock.Focus()
    'End Sub

    'Private Sub DisplayQCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayQCToolStripMenuItem.Click
    '    Display_Hasil_Quality_Control.StartPosition = FormStartPosition.CenterScreen

    '    Display_Hasil_Quality_Control.MdiParent = Me
    '    Display_Hasil_Quality_Control.Show()
    '    Display_Hasil_Quality_Control.Focus()
    'End Sub

    'Private Sub TimbangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TimbangMasukToolStripMenuItem.Click
    '    EMI_Display_Timbang.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Timbang.MdiParent = Me
    '    EMI_Display_Timbang.asal = "Unloading_Barang"
    '    EMI_Display_Timbang.filter_tambahan = "timbang_masuk='Y'"
    '    EMI_Display_Timbang.Show()
    '    EMI_Display_Timbang.Focus()
    'End Sub

    'Private Sub TimbangKeluarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles TimbangKeluarToolStripMenuItem1.Click
    '    EMI_Display_Timbang.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Timbang.MdiParent = Me
    '    EMI_Display_Timbang.asal = "Unloading_Barang"
    '    EMI_Display_Timbang.filter_tambahan = "timbang_keluar='Y'"
    '    EMI_Display_Timbang.Show()
    '    EMI_Display_Timbang.Focus()
    'End Sub

    'Private Sub QCBarangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QCBarangMasukToolStripMenuItem.Click
    '    Emi_Display_Quality_Control.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Display_Quality_Control.MdiParent = Me
    '    Emi_Display_Quality_Control.Show()
    '    Emi_Display_Quality_Control.Focus()
    'End Sub

    'Private Sub CetakToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakToolStripMenuItem.Click
    '    EMI_Display_Pallet_Masuk.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Pallet_Masuk.MdiParent = Me
    '    EMI_Display_Pallet_Masuk.Show()
    '    EMI_Display_Pallet_Masuk.Focus()
    'End Sub

    'Private Sub CetakBarcodeFloorScaleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakBarcodeFloorScaleToolStripMenuItem.Click
    '    Emi_Display_Timbang_FloorScale.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Display_Timbang_FloorScale.MdiParent = Me
    '    Emi_Display_Timbang_FloorScale.Show()
    '    Emi_Display_Timbang_FloorScale.Focus()
    'End Sub

    'Private Sub QualityControlBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QualityControlBarangToolStripMenuItem.Click
    '    Master_Quality_Control_Barang.StartPosition = FormStartPosition.CenterScreen

    '    Master_Quality_Control_Barang.MdiParent = Me
    '    Master_Quality_Control_Barang.Show()
    '    Master_Quality_Control_Barang.Focus()
    'End Sub

    'Private Sub MasterMenuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterMenuToolStripMenuItem.Click
    '    Master_Menu_x.StartPosition = FormStartPosition.CenterScreen

    '    Master_Menu_x.MdiParent = Me
    '    Master_Menu_x.Show()
    '    Master_Menu_x.Focus()
    'End Sub

    'Private Sub LoadingBarangImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadingBarangImportToolStripMenuItem.Click
    '    Loading_Barang_Import.StartPosition = FormStartPosition.CenterScreen

    '    Loading_Barang_Import.MdiParent = Me
    '    Loading_Barang_Import.Show()
    '    Loading_Barang_Import.Focus()
    'End Sub

    'Private Sub LaporanSummaryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanSummaryToolStripMenuItem.Click
    '    N_EMI_Laporan_Quality_Control_Summary.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Quality_Control_Summary.MdiParent = Me
    '    N_EMI_Laporan_Quality_Control_Summary.Show()
    '    N_EMI_Laporan_Quality_Control_Summary.Focus()
    'End Sub

    'Private Sub RestockToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RestockToolStripMenuItem1.Click
    '    EMI_Restock.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Restock.MdiParent = Me
    '    EMI_Restock.Show()
    '    EMI_Restock.Focus()
    'End Sub

    'Private Sub DisplayRestockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayRestockToolStripMenuItem.Click
    '    EMI_Display_Restock.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Restock.MdiParent = Me
    '    EMI_Display_Restock.Show()
    '    EMI_Display_Restock.Focus()
    'End Sub

    'Private Sub TFBahanBakarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles TFBahanBakarToolStripMenuItem1.Click
    '    EMI_Transfer_Bahan_Bakar.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Transfer_Bahan_Bakar.MdiParent = Me
    '    EMI_Transfer_Bahan_Bakar.Show()
    '    EMI_Transfer_Bahan_Bakar.Focus()
    'End Sub

    'Private Sub DisplayBahanBakarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayBahanBakarToolStripMenuItem.Click
    '    EMI_Display_Pengeluaran_Bahan_Bakar.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Pengeluaran_Bahan_Bakar.MdiParent = Me
    '    EMI_Display_Pengeluaran_Bahan_Bakar.Show()
    '    EMI_Display_Pengeluaran_Bahan_Bakar.Focus()
    'End Sub

    'Private Sub DisplayPengeluaranStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayPengeluaranStockToolStripMenuItem.Click
    '    EMI_Display_Pengeluaran_Stock.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Pengeluaran_Stock.MdiParent = Me
    '    EMI_Display_Pengeluaran_Stock.Show()
    '    EMI_Display_Pengeluaran_Stock.Focus()
    'End Sub

    'Private Sub VallidasiPengeluaranStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VallidasiPengeluaranStockToolStripMenuItem.Click
    '    EMI_Validasi_Pengeluaran_Stock.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Validasi_Pengeluaran_Stock.MdiParent = Me
    '    EMI_Validasi_Pengeluaran_Stock.Show()
    '    EMI_Validasi_Pengeluaran_Stock.Focus()
    'End Sub

    'Private Sub CetakSaldoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakSaldoToolStripMenuItem.Click
    '    Hr_Cetak_Saldo_Akhir.StartPosition = FormStartPosition.CenterScreen

    '    Hr_Cetak_Saldo_Akhir.MdiParent = Me
    '    Hr_Cetak_Saldo_Akhir.Show()
    '    Hr_Cetak_Saldo_Akhir.Focus()
    'End Sub

    'Private Sub ExportToExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportToExcelToolStripMenuItem.Click
    '    Tes_Export_Excel_EPPLUS.StartPosition = FormStartPosition.CenterScreen

    '    Tes_Export_Excel_EPPLUS.MdiParent = Me
    '    Tes_Export_Excel_EPPLUS.Show()
    '    Tes_Export_Excel_EPPLUS.Focus()
    'End Sub

    'Private Sub TFMaterial2MaterialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TFMaterial2MaterialToolStripMenuItem.Click
    '    Tf_Material_To_Material.StartPosition = FormStartPosition.CenterScreen

    '    Tf_Material_To_Material.MdiParent = Me
    '    Tf_Material_To_Material.Show()
    '    Tf_Material_To_Material.Focus()
    'End Sub

    'Private Sub DisplayToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles DisplayToolStripMenuItem5.Click
    '    N_EMI_TF_Material_To_Material_Display.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_TF_Material_To_Material_Display.MdiParent = Me
    '    N_EMI_TF_Material_To_Material_Display.Show()
    '    N_EMI_TF_Material_To_Material_Display.Focus()
    'End Sub

    'Private Sub LaporanPengeluaranStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanPengeluaranStockToolStripMenuItem.Click
    '    N_EMI_Laporan_Pengeluaran_Stock.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Pengeluaran_Stock.MdiParent = Me
    '    N_EMI_Laporan_Pengeluaran_Stock.Show()
    '    N_EMI_Laporan_Pengeluaran_Stock.Focus()
    'End Sub

    'Private Sub PengajuanPemusnahanBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PengajuanPemusnahanBarangToolStripMenuItem.Click
    '    N_EMI_Transaksi_Pemusnahan_Barang.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Pemusnahan_Barang.MdiParent = Me
    '    N_EMI_Transaksi_Pemusnahan_Barang.Show()
    '    N_EMI_Transaksi_Pemusnahan_Barang.Focus()
    'End Sub

    'Private Sub ValidasiPemusnahanBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiPemusnahanBarangToolStripMenuItem.Click
    '    N_EMI_Transaksi_Pemusnahan_Barang_Validasi.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Pemusnahan_Barang_Validasi.MdiParent = Me
    '    N_EMI_Transaksi_Pemusnahan_Barang_Validasi.Show()
    '    N_EMI_Transaksi_Pemusnahan_Barang_Validasi.Focus()
    'End Sub

    'Private Sub ValidasiTimbangPemusnahanBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiTimbangPemusnahanBarangToolStripMenuItem.Click
    '    N_EMI_Display_Pemusnahan_Barang_Timbang.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Display_Pemusnahan_Barang_Timbang.MdiParent = Me
    '    N_EMI_Display_Pemusnahan_Barang_Timbang.Show()
    '    N_EMI_Display_Pemusnahan_Barang_Timbang.Focus()
    'End Sub

    'Private Sub ValidasiTimbangPemusnahanBarangTidakTimbangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiTimbangPemusnahanBarangTidakTimbangToolStripMenuItem.Click
    '    N_EMI_Transaksi_Pemusnahan_Barang_Barcode2.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Pemusnahan_Barang_Barcode2.MdiParent = Me
    '    N_EMI_Transaksi_Pemusnahan_Barang_Barcode2.Show()
    '    N_EMI_Transaksi_Pemusnahan_Barang_Barcode2.Focus()
    'End Sub

    'Private Sub DisplayPemusnahanBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayPemusnahanBarangToolStripMenuItem.Click
    '    N_EMI_Display_Pemusnahan_Barang.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Display_Pemusnahan_Barang.MdiParent = Me
    '    N_EMI_Display_Pemusnahan_Barang.Show()
    '    N_EMI_Display_Pemusnahan_Barang.Focus()
    'End Sub

    'Private Sub MasterBarangToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles MasterBarangToolStripMenuItem1.Click
    '    Master_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    Master_Barang_Lain.MdiParent = Me
    '    Master_Barang_Lain.Show()
    '    Master_Barang_Lain.Focus()
    'End Sub

    'Private Sub KategoriBarangAssetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KategoriBarangAssetToolStripMenuItem.Click
    '    N_EMI_Master_Kategori_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Master_Kategori_Barang_Lain.MdiParent = Me
    '    N_EMI_Master_Kategori_Barang_Lain.Show()
    '    N_EMI_Master_Kategori_Barang_Lain.Focus()
    'End Sub

    'Private Sub KelompokBarangAssetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KelompokBarangAssetToolStripMenuItem.Click
    '    N_EMI_Master_Kelompok_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Master_Kelompok_Barang_Lain.MdiParent = Me
    '    N_EMI_Master_Kelompok_Barang_Lain.Show()
    '    N_EMI_Master_Kelompok_Barang_Lain.Focus()
    'End Sub

    'Private Sub MasterGedungAssetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterGedungAssetToolStripMenuItem.Click
    '    N_EMI_Master_Gedung_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Master_Gedung_Lain.MdiParent = Me
    '    N_EMI_Master_Gedung_Lain.Show()
    '    N_EMI_Master_Gedung_Lain.Focus()
    'End Sub

    'Private Sub MasterAreaBarangAssetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterAreaBarangAssetToolStripMenuItem.Click
    '    N_EMI_Master_Area_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Master_Area_Barang_Lain.MdiParent = Me
    '    N_EMI_Master_Area_Barang_Lain.Show()
    '    N_EMI_Master_Area_Barang_Lain.Focus()
    'End Sub

    'Private Sub PToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PToolStripMenuItem.Click
    '    Purchase_Requisition_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    Purchase_Requisition_Barang_Lain.MdiParent = Me
    '    Purchase_Requisition_Barang_Lain.Show()
    '    Purchase_Requisition_Barang_Lain.Focus()
    'End Sub

    'Private Sub BarangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarangMasukToolStripMenuItem.Click
    '    EMI_Display_Pallet_Masuk_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Pallet_Masuk_Barang_Lain.MdiParent = Me
    '    EMI_Display_Pallet_Masuk_Barang_Lain.Show()
    '    EMI_Display_Pallet_Masuk_Barang_Lain.Focus()
    'End Sub

    'Private Sub PengeluaranStockAssetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PengeluaranStockAssetToolStripMenuItem.Click
    '    EMI_Pengeluaran_Stock_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pengeluaran_Stock_Barang_Lain.MenuAsal = "PENGELUARAN_STOCK"
    '    EMI_Pengeluaran_Stock_Barang_Lain.MdiParent = Me
    '    EMI_Pengeluaran_Stock_Barang_Lain.Show()
    '    EMI_Pengeluaran_Stock_Barang_Lain.Focus()
    'End Sub

    'Private Sub PengeluaranStockAssetRejectedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PengeluaranStockAssetRejectedToolStripMenuItem.Click
    '    EMI_Pengeluaran_Stock_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pengeluaran_Stock_Barang_Lain.MenuAsal = "PENGELUARAN_STOCK"
    '    EMI_Pengeluaran_Stock_Barang_Lain.MdiParent = Me
    '    EMI_Pengeluaran_Stock_Barang_Lain.Show()
    '    EMI_Pengeluaran_Stock_Barang_Lain.Focus()
    'End Sub

    'Private Sub PemakaianStockAssetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PemakaianStockAssetToolStripMenuItem.Click
    '    N_EMI_Pemakaian_Stock_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Pemakaian_Stock_Barang_Lain.MenuAsal = "PEMAKAIAN_STOCK"
    '    N_EMI_Pemakaian_Stock_Barang_Lain.MdiParent = Me
    '    N_EMI_Pemakaian_Stock_Barang_Lain.Show()
    '    N_EMI_Pemakaian_Stock_Barang_Lain.Focus()
    'End Sub

    'Private Sub ValidasiPengeluaranStockAssetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiPengeluaranStockAssetToolStripMenuItem.Click
    '    EMI_Validasi_Pengeluaran_Stock_Lain.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Validasi_Pengeluaran_Stock_Lain.MdiParent = Me
    '    EMI_Validasi_Pengeluaran_Stock_Lain.Show()
    '    EMI_Validasi_Pengeluaran_Stock_Lain.Focus()
    'End Sub

    'Private Sub ValidasiPemakaianStockAssetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiPemakaianStockAssetToolStripMenuItem.Click
    '    N_EMI_Validasi_Pemakaian_Stock_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Validasi_Pemakaian_Stock_Barang_Lain.MdiParent = Me
    '    N_EMI_Validasi_Pemakaian_Stock_Barang_Lain.Show()
    '    N_EMI_Validasi_Pemakaian_Stock_Barang_Lain.Focus()
    'End Sub

    'Private Sub POIndukToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles POIndukToolStripMenuItem1.Click
    '    EMI_PO_Pembelian_Display_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    EMI_PO_Pembelian_Display_Barang_Lain.MdiParent = Me
    '    EMI_PO_Pembelian_Display_Barang_Lain.Show()
    '    EMI_PO_Pembelian_Display_Barang_Lain.Focus()
    'End Sub

    'Private Sub SubPOToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SubPOToolStripMenuItem1.Click
    '    EMI_PO_Pembelian_Display_Sub_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    EMI_PO_Pembelian_Display_Sub_Barang_Lain.MdiParent = Me
    '    EMI_PO_Pembelian_Display_Sub_Barang_Lain.Show()
    '    EMI_PO_Pembelian_Display_Sub_Barang_Lain.Focus()
    'End Sub

    'Private Sub PenawaranToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles PenawaranToolStripMenuItem6.Click
    '    Transaksi_Penawaran_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    Transaksi_Penawaran_Barang_Lain.MdiParent = Me
    '    Transaksi_Penawaran_Barang_Lain.Show()
    '    Transaksi_Penawaran_Barang_Lain.Focus()
    'End Sub

    'Private Sub ValToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValToolStripMenuItem.Click
    '    Emi_Selisih_Barang_Masuk_Display_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Selisih_Barang_Masuk_Display_Barang_Lain.MdiParent = Me
    '    Emi_Selisih_Barang_Masuk_Display_Barang_Lain.Show()
    '    Emi_Selisih_Barang_Masuk_Display_Barang_Lain.Focus()
    'End Sub

    'Private Sub TransaksiBiayaLokalToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles TransaksiBiayaLokalToolStripMenuItem1.Click
    '    EMI_Display_Transaksi_Biaya_Lokal_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Transaksi_Biaya_Lokal_Barang_Lain.MdiParent = Me
    '    EMI_Display_Transaksi_Biaya_Lokal_Barang_Lain.Show()
    '    EMI_Display_Transaksi_Biaya_Lokal_Barang_Lain.Focus()
    'End Sub

    'Private Sub PembelianAssetToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PembelianAssetToolStripMenuItem1.Click
    '    EMI_PO_Pembelian_Display2_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    EMI_PO_Pembelian_Display2_Barang_Lain.MdiParent = Me
    '    EMI_PO_Pembelian_Display2_Barang_Lain.Show()
    '    EMI_PO_Pembelian_Display2_Barang_Lain.Focus()
    'End Sub

    'Private Sub LaporanFinalGIGRToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LaporanFinalGIGRToolStripMenuItem1.Click
    '    N_EMI_Laporan_Final_GI_GR.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Final_GI_GR.MdiParent = Me
    '    N_EMI_Laporan_Final_GI_GR.Show()
    '    N_EMI_Laporan_Final_GI_GR.Focus()
    'End Sub

    'Private Sub LaporanHPPProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanHPPProduksiToolStripMenuItem.Click
    '    N_EMI_Laporan_HPP_Produksi.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_HPP_Produksi.MdiParent = Me
    '    N_EMI_Laporan_HPP_Produksi.Show()
    '    N_EMI_Laporan_HPP_Produksi.Focus()
    'End Sub

    Private Sub TToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TToolStripMenuItem.Click
        Transfer_Stock_3.StartPosition = FormStartPosition.CenterScreen

        Transfer_Stock_3.MenuAsal = "TRANSFER_WASTE"
        Transfer_Stock_3.MdiParent = Me
        Transfer_Stock_3.Show()
        Transfer_Stock_3.Focus()
    End Sub

    Private Sub RequestMaterialQCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RequestMaterialQCToolStripMenuItem.Click
        N_EMI_Display_Request_Material_QC.StartPosition = FormStartPosition.CenterScreen

        N_EMI_Display_Request_Material_QC.MdiParent = Me
        N_EMI_Display_Request_Material_QC.Show()
        N_EMI_Display_Request_Material_QC.Focus()
    End Sub

    Private Sub TransferStockToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles TransferStockToolStripMenuItem2.Click
        N_EMI_Transaksi_Request_Material_QC_Validasi.StartPosition = FormStartPosition.CenterScreen

        N_EMI_Transaksi_Request_Material_QC_Validasi.MdiParent = Me
        N_EMI_Transaksi_Request_Material_QC_Validasi.Show()
        N_EMI_Transaksi_Request_Material_QC_Validasi.Focus()
    End Sub

    'Private Sub MasterQCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterQCToolStripMenuItem.Click
    '    Master_Quality_Control.StartPosition = FormStartPosition.CenterScreen

    '    Master_Quality_Control.MdiParent = Me
    '    Master_Quality_Control.Show()
    '    Master_Quality_Control.Focus()
    'End Sub

    'Private Sub SampleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SampleToolStripMenuItem.Click
    '    Form3.StartPosition = FormStartPosition.CenterScreen

    '    Form3.MdiParent = Me
    '    Form3.Show()
    '    Form3.Focus()
    'End Sub

    'Private Sub TesPrintToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles TesPrintToolStripMenuItem1.Click
    '    TESTING_PRINT.StartPosition = FormStartPosition.CenterScreen

    '    TESTING_PRINT.MdiParent = Me
    '    TESTING_PRINT.Show()
    '    TESTING_PRINT.Focus()
    'End Sub

    'Private Sub LaporanFinalGIGRMainToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanFinalGIGRMainToolStripMenuItem.Click

    '    N_EMI_Laporan_Final_GI_GR_Main.StartPosition = FormStartPosition.CenterScreen
    '    N_EMI_Laporan_Final_GI_GR_Main.MdiParent = Me
    '    N_EMI_Laporan_Final_GI_GR_Main.Show()
    '    N_EMI_Laporan_Final_GI_GR_Main.Focus()
    'End Sub

    'Private Sub RequestMaterialQCToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RequestMaterialQCToolStripMenuItem1.Click
    '    N_EMI_Display_Request_Material_QC_Summary.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Display_Request_Material_QC_Summary.MdiParent = Me
    '    N_EMI_Display_Request_Material_QC_Summary.Show()
    '    N_EMI_Display_Request_Material_QC_Summary.Focus()
    'End Sub


    'Private Sub LaporanRequestMaterialQCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanRequestMaterialQCToolStripMenuItem.Click
    '    N_EMI_Laporan_Request_Material_QC_Validasi.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Request_Material_QC_Validasi.MdiParent = Me
    '    N_EMI_Laporan_Request_Material_QC_Validasi.Show()
    '    N_EMI_Laporan_Request_Material_QC_Validasi.Focus()
    'End Sub

    'Private Sub ValidasiInkubasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiInkubasiToolStripMenuItem.Click
    '    N_EMI_Display_Validasi_Inkubasi_Produksi.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Display_Validasi_Inkubasi_Produksi.MdiParent = Me
    '    N_EMI_Display_Validasi_Inkubasi_Produksi.Show()
    '    N_EMI_Display_Validasi_Inkubasi_Produksi.Focus()
    'End Sub

    'Private Sub PengeluaranStockToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles PengeluaranStockToolStripMenuItem4.Click
    '    N_EMI_Laporan_Pengeluaran_Stock_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Pengeluaran_Stock_Barang_Lain.MdiParent = Me
    '    N_EMI_Laporan_Pengeluaran_Stock_Barang_Lain.Show()
    '    N_EMI_Laporan_Pengeluaran_Stock_Barang_Lain.Focus()
    'End Sub

    'Private Sub PemakaianStockToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    N_EMI_Pemakaian_Stock_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Pemakaian_Stock_Barang_Lain.MenuAsal = "PEMAKAIAN_STOCK"
    '    N_EMI_Pemakaian_Stock_Barang_Lain.MdiParent = Me
    '    N_EMI_Pemakaian_Stock_Barang_Lain.Show()
    '    N_EMI_Pemakaian_Stock_Barang_Lain.Focus()
    'End Sub

    'Private Sub ValidasiPemakaianStockToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    N_EMI_Validasi_Pemakaian_Stock_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Validasi_Pemakaian_Stock_Barang_Lain.MdiParent = Me
    '    N_EMI_Validasi_Pemakaian_Stock_Barang_Lain.Show()
    '    N_EMI_Validasi_Pemakaian_Stock_Barang_Lain.Focus()
    'End Sub

    'Private Sub PemakaianStockToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PemakaianStockToolStripMenuItem1.Click
    '    N_EMI_Laporan_Pemakaian_Stock_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Pemakaian_Stock_Barang_Lain.MdiParent = Me
    '    N_EMI_Laporan_Pemakaian_Stock_Barang_Lain.Show()
    '    N_EMI_Laporan_Pemakaian_Stock_Barang_Lain.Focus()
    'End Sub

    'Private Sub PembelianAssetToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles PembelianAssetToolStripMenuItem2.Click
    '    EMI_PO_Pembelian_Display2_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    EMI_PO_Pembelian_Display2_Barang_Lain.MdiParent = Me
    '    EMI_PO_Pembelian_Display2_Barang_Lain.Show()
    '    EMI_PO_Pembelian_Display2_Barang_Lain.Focus()
    'End Sub

    'Private Sub ProductionProcessTrackerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductionProcessTrackerToolStripMenuItem.Click
    '    N_EMI_Display_Production_Process_Tracker.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Display_Production_Process_Tracker.MdiParent = Me
    '    N_EMI_Display_Production_Process_Tracker.Show()
    '    N_EMI_Display_Production_Process_Tracker.Focus()
    'End Sub

    Private Sub ValidasiGR3ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiGR3ToolStripMenuItem.Click
        N_EMI_Transaksi_Validasi_GR_3.StartPosition = FormStartPosition.CenterScreen

        N_EMI_Transaksi_Validasi_GR_3.MdiParent = Me
        N_EMI_Transaksi_Validasi_GR_3.Show()
        N_EMI_Transaksi_Validasi_GR_3.Focus()
    End Sub

    'Private Sub AdasdaToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    N_EMI_Display_Barang_Masuk_Asset.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Display_Barang_Masuk_Asset.MdiParent = Me
    '    N_EMI_Display_Barang_Masuk_Asset.Show()
    '    N_EMI_Display_Barang_Masuk_Asset.Focus()
    'End Sub

    Private Sub DisplayGr3ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayGr3ToolStripMenuItem.Click
        N_EMI_Display_Validasi_GR_3.StartPosition = FormStartPosition.CenterScreen

        N_EMI_Display_Validasi_GR_3.MdiParent = Me
        N_EMI_Display_Validasi_GR_3.Show()
        N_EMI_Display_Validasi_GR_3.Focus()
    End Sub

    'Private Sub LaporanKaryawanPerTahapanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanKaryawanPerTahapanToolStripMenuItem.Click
    '    N_EMI_Laporan_List_Karyawan_Per_Tahapan.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_List_Karyawan_Per_Tahapan.MdiParent = Me
    '    N_EMI_Laporan_List_Karyawan_Per_Tahapan.Show()
    '    N_EMI_Laporan_List_Karyawan_Per_Tahapan.Focus()
    'End Sub


    'Private Sub LaporanPelunasanCutOffToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanPelunasanCutOffToolStripMenuItem.Click
    '    N_EMI_Laporan_Pelunasan_Cut_Off.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Pelunasan_Cut_Off.MdiParent = Me
    '    N_EMI_Laporan_Pelunasan_Cut_Off.Show()
    '    N_EMI_Laporan_Pelunasan_Cut_Off.Focus()
    'End Sub

    'Private Sub ProductionTrackHarianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductionTrackHarianToolStripMenuItem.Click
    '    N_EMI_Display_Production_Track_Harian.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Display_Production_Track_Harian.MdiParent = Me
    '    N_EMI_Display_Production_Track_Harian.Show()
    '    N_EMI_Display_Production_Track_Harian.Focus()
    'End Sub

    'Private Sub LaporanMilitarySamplingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanMilitarySamplingToolStripMenuItem.Click
    '    N_EMI_Laporan_MIlitary_Sampling.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_MIlitary_Sampling.MdiParent = Me
    '    N_EMI_Laporan_MIlitary_Sampling.Show()
    '    N_EMI_Laporan_MIlitary_Sampling.Focus()
    'End Sub

    'Private Sub PRToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles PRToolStripMenuItem2.Click
    '    EMI_Pembelian_PR_Summary_Data_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pembelian_PR_Summary_Data_Barang_Lain.MdiParent = Me
    '    EMI_Pembelian_PR_Summary_Data_Barang_Lain.Show()
    '    EMI_Pembelian_PR_Summary_Data_Barang_Lain.Focus()
    'End Sub

    'Private Sub SelisihBarangMasukToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SelisihBarangMasukToolStripMenuItem1.Click
    '    Emi_Selisih_Barang_Masuk_Display.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Selisih_Barang_Masuk_Display.MdiParent = Me
    '    Emi_Selisih_Barang_Masuk_Display.Show()
    '    Emi_Selisih_Barang_Masuk_Display.Focus()
    'End Sub

    'Private Sub DisplaySelisihBarangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplaySelisihBarangMasukToolStripMenuItem.Click
    '    N_EMI_Display_Selisih_Barang_Masuk.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Display_Selisih_Barang_Masuk.MdiParent = Me
    '    N_EMI_Display_Selisih_Barang_Masuk.Show()
    '    N_EMI_Display_Selisih_Barang_Masuk.Focus()
    'End Sub

    'Private Sub LaporanDownPaymentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanDownPaymentToolStripMenuItem.Click
    '    N_EMI_Laporan_Pembayaran_Di_Muka.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Pembayaran_Di_Muka.MdiParent = Me
    '    N_EMI_Laporan_Pembayaran_Di_Muka.Show()
    '    N_EMI_Laporan_Pembayaran_Di_Muka.Focus()
    'End Sub

    'Private Sub LaporanDownPaymentAssetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanDownPaymentAssetToolStripMenuItem.Click
    '    N_EMI_Laporan_Pembayaran_Di_Muka_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Pembayaran_Di_Muka_Barang_Lain.MdiParent = Me
    '    N_EMI_Laporan_Pembayaran_Di_Muka_Barang_Lain.Show()
    '    N_EMI_Laporan_Pembayaran_Di_Muka_Barang_Lain.Focus()
    'End Sub

    'Private Sub LaporanDownPaymentProyekToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanDownPaymentProyekToolStripMenuItem.Click
    '    N_EMI_Laporan_Pembayaran_Di_Muka_Proyek.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Pembayaran_Di_Muka_Proyek.MdiParent = Me
    '    N_EMI_Laporan_Pembayaran_Di_Muka_Proyek.Show()
    '    N_EMI_Laporan_Pembayaran_Di_Muka_Proyek.Focus()
    'End Sub

    'Private Sub LaporanMutasiBahanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanMutasiBahanToolStripMenuItem.Click
    '    N_EMI_Laporan_Mutasi_Bahan.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Mutasi_Bahan.MdiParent = Me
    '    N_EMI_Laporan_Mutasi_Bahan.Show()
    '    N_EMI_Laporan_Mutasi_Bahan.Focus()
    'End Sub

    'Private Sub HutangToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HutangToolStripMenuItem1.Click
    '    N_EMI_Display_Pelunasan_Hutang_Asset.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Display_Pelunasan_Hutang_Asset.MdiParent = Me
    '    N_EMI_Display_Pelunasan_Hutang_Asset.Show()
    '    N_EMI_Display_Pelunasan_Hutang_Asset.Focus()
    'End Sub

    'Private Sub AdjStockToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    Emi_Adj_Stock.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Adj_Stock.MdiParent = Me
    '    Emi_Adj_Stock.Show()
    '    Emi_Adj_Stock.Focus()
    'End Sub

    'Private Sub PengeluarnaStockToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    EMI_Pengeluaran_Stock.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pengeluaran_Stock.MenuAsal = "PENGELUARAN_STOCK"
    '    EMI_Pengeluaran_Stock.MdiParent = Me
    '    EMI_Pengeluaran_Stock.Show()
    '    EMI_Pengeluaran_Stock.Focus()
    'End Sub

    'Private Sub RMGeneralToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    EMI_Request_Material_General.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Request_Material_General.MdiParent = Me
    '    EMI_Request_Material_General.Show()
    '    EMI_Request_Material_General.Focus()
    'End Sub

    'Private Sub RestockToolStripMenuItem2_Click(sender As Object, e As EventArgs)
    '    EMI_Restock.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Restock.MdiParent = Me
    '    EMI_Restock.Show()
    '    EMI_Restock.Focus()
    'End Sub

    'Private Sub SplitStockToolStripMenuItem1_Click(sender As Object, e As EventArgs)
    '    Emi_Split_Stock_QC.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Split_Stock_QC.MdiParent = Me
    '    Emi_Split_Stock_QC.Show()
    '    Emi_Split_Stock_QC.Focus()
    'End Sub

    'Private Sub TfBahanBakarToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    EMI_Transfer_Bahan_Bakar.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Transfer_Bahan_Bakar.MdiParent = Me
    '    EMI_Transfer_Bahan_Bakar.Show()
    '    EMI_Transfer_Bahan_Bakar.Focus()
    'End Sub

    'Private Sub TfQualityToolStripMenuItem1_Click(sender As Object, e As EventArgs)
    '    EMI_Transfer_Quality_QC.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Transfer_Quality_QC.MdiParent = Me
    '    EMI_Transfer_Quality_QC.Show()
    '    EMI_Transfer_Quality_QC.Focus()
    'End Sub

    'Private Sub PemusnahanBarangToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    N_EMI_Transaksi_Pemusnahan_Barang.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Pemusnahan_Barang.MdiParent = Me
    '    N_EMI_Transaksi_Pemusnahan_Barang.Show()
    '    N_EMI_Transaksi_Pemusnahan_Barang.Focus()
    'End Sub

    'Private Sub PemusnahanBarangBarocdeToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    N_EMI_Transaksi_Pemusnahan_Barang_Barcode2.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Pemusnahan_Barang_Barcode2.MdiParent = Me
    '    N_EMI_Transaksi_Pemusnahan_Barang_Barcode2.Show()
    '    N_EMI_Transaksi_Pemusnahan_Barang_Barcode2.Focus()
    'End Sub

    'Private Sub PemusnahanBarangValidsaiToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    N_EMI_Transaksi_Pemusnahan_Barang_Validasi.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Pemusnahan_Barang_Validasi.MdiParent = Me
    '    N_EMI_Transaksi_Pemusnahan_Barang_Validasi.Show()
    '    N_EMI_Transaksi_Pemusnahan_Barang_Validasi.Focus()
    'End Sub

    'Private Sub PengeluaranBahanToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    Pengeluaran_Barang.StartPosition = FormStartPosition.CenterScreen

    '    Pengeluaran_Barang.MdiParent = Me
    '    Pengeluaran_Barang.Show()
    '    Pengeluaran_Barang.Focus()
    'End Sub

    'Private Sub MaterialToMaterialToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    Tf_Material_To_Material.StartPosition = FormStartPosition.CenterScreen

    '    Tf_Material_To_Material.MdiParent = Me
    '    Tf_Material_To_Material.Show()
    '    Tf_Material_To_Material.Focus()
    'End Sub

    Private Sub TransferStockToolStripMenuItem3_Click(sender As Object, e As EventArgs)
        Transfer_Stock_3.StartPosition = FormStartPosition.CenterScreen
        Transfer_Stock_3.MenuAsal = "TRANSFER_STOCK"
        Transfer_Stock_3.MdiParent = Me
        Transfer_Stock_3.Show()
        Transfer_Stock_3.Focus()
    End Sub

    'Private Sub LaporanMutasiBahanDalamProsesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanMutasiBahanDalamProsesToolStripMenuItem.Click
    '    N_EMI_Laporan_Mutasi_Bahan_Dalam_Proses.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Mutasi_Bahan_Dalam_Proses.MdiParent = Me
    '    N_EMI_Laporan_Mutasi_Bahan_Dalam_Proses.Show()
    '    N_EMI_Laporan_Mutasi_Bahan_Dalam_Proses.Focus()
    'End Sub

    'Private Sub NominalStockBySplitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NominalStockBySplitToolStripMenuItem.Click
    '    N_EMI_Transaksi_Add_Nominal_Stock.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Add_Nominal_Stock.MdiParent = Me
    '    N_EMI_Transaksi_Add_Nominal_Stock.Show()
    '    N_EMI_Transaksi_Add_Nominal_Stock.Focus()
    'End Sub

    'Private Sub ValidasiHPPToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ValidasiHPPToolStripMenuItem2.Click
    '    EMI_Display_Validasi_HPP_Produksi.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Validasi_HPP_Produksi.MdiParent = Me
    '    EMI_Display_Validasi_HPP_Produksi.Show()
    '    EMI_Display_Validasi_HPP_Produksi.Focus()
    'End Sub

    'Private Sub LaporanPelunasanCutOffAssetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanPelunasanCutOffAssetToolStripMenuItem.Click
    '    N_EMI_Laporan_Pelunasan_Barang_Lain_Cut_Off.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Pelunasan_Barang_Lain_Cut_Off.MdiParent = Me
    '    N_EMI_Laporan_Pelunasan_Barang_Lain_Cut_Off.Show()
    '    N_EMI_Laporan_Pelunasan_Barang_Lain_Cut_Off.Focus()
    'End Sub

    'Private Sub LaporanPelunasanToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LaporanPelunasanToolStripMenuItem1.Click
    '    N_EMI_Laporan_Pelunasan_Asset.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Pelunasan_Asset.MdiParent = Me
    '    N_EMI_Laporan_Pelunasan_Asset.Show()
    '    N_EMI_Laporan_Pelunasan_Asset.Focus()
    'End Sub

    'Private Sub LaporanPembelianToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LaporanPembelianToolStripMenuItem1.Click
    '    N_EMI_Laporan_Pembelian_Asset.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Pembelian_Asset.MdiParent = Me
    '    N_EMI_Laporan_Pembelian_Asset.Show()
    '    N_EMI_Laporan_Pembelian_Asset.Focus()
    'End Sub

    'Private Sub LaporanBarangMasukToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LaporanBarangMasukToolStripMenuItem1.Click
    '    N_EMI_Laporan_Barang_Masuk_Asset.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Barang_Masuk_Asset.MdiParent = Me
    '    N_EMI_Laporan_Barang_Masuk_Asset.Show()
    '    N_EMI_Laporan_Barang_Masuk_Asset.Focus()
    'End Sub

    'Private Sub BypassMilitarySamplingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BypassMilitarySamplingToolStripMenuItem.Click
    '    N_EMI_Transaksi_Bypass_Military_Sampling.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Bypass_Military_Sampling.MdiParent = Me
    '    N_EMI_Transaksi_Bypass_Military_Sampling.Show()
    '    N_EMI_Transaksi_Bypass_Military_Sampling.Focus()
    'End Sub

    'Private Sub LaporanHPPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanHPPToolStripMenuItem.Click
    '    N_EMI_Laporan_HPP.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_HPP.MdiParent = Me
    '    N_EMI_Laporan_HPP.Show()
    '    N_EMI_Laporan_HPP.Focus()
    'End Sub

    'Private Sub FleverToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FleverToolStripMenuItem.Click
    '    Master_Flever.StartPosition = FormStartPosition.CenterScreen

    '    Master_Flever.MdiParent = Me
    '    Master_Flever.Show()
    '    Master_Flever.Focus()
    'End Sub

    'Private Sub LaporanPengeluaranToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanPengeluaranToolStripMenuItem.Click
    '    N_EMI_Laporan_Pengeluaran_Stock_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Pengeluaran_Stock_Barang_Lain.MdiParent = Me
    '    N_EMI_Laporan_Pengeluaran_Stock_Barang_Lain.Show()
    '    N_EMI_Laporan_Pengeluaran_Stock_Barang_Lain.Focus()
    'End Sub

    'Private Sub LaporanMutasiBahanToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LaporanMutasiBahanToolStripMenuItem1.Click
    '    N_EMI_Laporan_Mutasi_Bahan_Asset.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Mutasi_Bahan_Asset.MdiParent = Me
    '    N_EMI_Laporan_Mutasi_Bahan_Asset.Show()
    '    N_EMI_Laporan_Mutasi_Bahan_Asset.Focus()
    'End Sub

    'Private Sub KonfigurasiHargaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KonfigurasiHargaToolStripMenuItem.Click
    '    N_EMI_Transaksi_Konfigurasi_Harga_Jual.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Konfigurasi_Harga_Jual.MdiParent = Me
    '    N_EMI_Transaksi_Konfigurasi_Harga_Jual.Show()
    '    N_EMI_Transaksi_Konfigurasi_Harga_Jual.Focus()
    'End Sub

    'Private Sub FleverToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles FleverToolStripMenuItem1.Click
    '    Master_Flever.StartPosition = FormStartPosition.CenterScreen

    '    Master_Flever.MdiParent = Me
    '    Master_Flever.Show()
    '    Master_Flever.Focus()
    'End Sub

    'Private Sub WasteProsesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WasteProsesToolStripMenuItem.Click
    '    N_EMI_Transaksi_Waste_Proses.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Waste_Proses.MdiParent = Me
    '    N_EMI_Transaksi_Waste_Proses.asal_menu = "PROCESS"
    '    N_EMI_Transaksi_Waste_Proses.Show()
    '    N_EMI_Transaksi_Waste_Proses.Focus()
    'End Sub

    'Private Sub WasteProductToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WasteProductToolStripMenuItem.Click
    '    N_EMI_Transaksi_Waste_Product_Transfer.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Waste_Product_Transfer.MdiParent = Me
    '    N_EMI_Transaksi_Waste_Product_Transfer.Show()
    '    N_EMI_Transaksi_Waste_Product_Transfer.Focus()
    'End Sub

    'Private Sub WasteProductReceivedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WasteProductReceivedToolStripMenuItem.Click
    '    N_EMI_Transaksi_Waste_Product_Received.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Waste_Product_Received.MdiParent = Me
    '    N_EMI_Transaksi_Waste_Product_Received.Show()
    '    N_EMI_Transaksi_Waste_Product_Received.Focus()
    'End Sub

    Private Sub IndependentGoodIssueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IndependentGoodIssueToolStripMenuItem.Click
        'EMI_Hasil_Pengeluaran_Bahan_Baku.StartPosition = FormStartPosition.CenterScreen

        'EMI_Hasil_Pengeluaran_Bahan_Baku.WindowState = FormWindowState.Maximized
        'EMI_Hasil_Pengeluaran_Bahan_Baku.asal = "INDEPENDENT"
        'EMI_Hasil_Pengeluaran_Bahan_Baku.MdiParent = Me
        'EMI_Hasil_Pengeluaran_Bahan_Baku.Show()
        'EMI_Hasil_Pengeluaran_Bahan_Baku.Focus()

        'Me.Hide()

        With EMI_Hasil_Pengeluaran_Bahan_Baku_Baru
            .StartPosition = FormStartPosition.CenterScreen
            .asal = "INDEPENDENT"
            .FormBorderStyle = FormBorderStyle.None
            .WindowState = FormWindowState.Maximized
            .Show()
            .Focus()
        End With

    End Sub

    'Private Sub RequestMaterialTambahanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RequestMaterialTambahanToolStripMenuItem.Click
    '    N_EMI_Transaksi_Request_Material_Tambahan.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Request_Material_Tambahan.MdiParent = Me
    '    N_EMI_Transaksi_Request_Material_Tambahan.Show()
    '    N_EMI_Transaksi_Request_Material_Tambahan.Focus()
    'End Sub

    'Private Sub ValidasiRequestMaterialTambahanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiRequestMaterialTambahanToolStripMenuItem.Click
    '    N_EMI_Validasi_Transaksi_Request_Material_Tambahan.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Validasi_Transaksi_Request_Material_Tambahan.MdiParent = Me
    '    N_EMI_Validasi_Transaksi_Request_Material_Tambahan.Show()
    '    N_EMI_Validasi_Transaksi_Request_Material_Tambahan.Focus()
    'End Sub

    'Private Sub SummaryPalletMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SummaryPalletMasukToolStripMenuItem.Click
    '    EMI_Display_Pallet_Masuk_Data_Lain.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Display_Pallet_Masuk_Data_Lain.MdiParent = Me
    '    EMI_Display_Pallet_Masuk_Data_Lain.Show()
    '    EMI_Display_Pallet_Masuk_Data_Lain.Focus()
    'End Sub

    'Private Sub ReturPembelianToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ReturPembelianToolStripMenuItem1.Click
    '    N_EMI_Transaksi_Retur_Packaging.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Retur_Packaging.MdiParent = Me
    '    N_EMI_Transaksi_Retur_Packaging.Show()
    '    N_EMI_Transaksi_Retur_Packaging.Focus()
    'End Sub

    'Private Sub PersentaseMarginBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PersentaseMarginBarangToolStripMenuItem.Click
    '    N_EMI_Master_Persentase_Margin_Barang.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Master_Persentase_Margin_Barang.MdiParent = Me
    '    N_EMI_Master_Persentase_Margin_Barang.Show()
    '    N_EMI_Master_Persentase_Margin_Barang.Focus()
    'End Sub

    'Private Sub PersentasePenentuBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PersentasePenentuBarangToolStripMenuItem.Click
    '    N_EMI_Master_Persentase_Penentu_Barang.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Master_Persentase_Penentu_Barang.MdiParent = Me
    '    N_EMI_Master_Persentase_Penentu_Barang.Show()
    '    N_EMI_Master_Persentase_Penentu_Barang.Focus()
    'End Sub

    Private Sub TransferStockBaruToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferStockBaruToolStripMenuItem.Click
        Transfer_Stock_3.StartPosition = FormStartPosition.CenterScreen

        Transfer_Stock_3.MenuAsal = "TRANSFER_STOCK"
        Transfer_Stock_3.MdiParent = Me
        Transfer_Stock_3.Show()
        Transfer_Stock_3.Focus()
    End Sub

    'Private Sub DisplayBarangToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DisplayBarangToolStripMenuItem1.Click
    '    Display_Barang.StartPosition = FormStartPosition.CenterScreen

    '    Display_Barang.MdiParent = Me
    '    Display_Barang.Show()
    '    Display_Barang.Focus()
    'End Sub



    'Private Sub PenjualanToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PenjualanToolStripMenuItem1.Click
    '    Penjualan_New.StartPosition = FormStartPosition.CenterScreen

    '    Penjualan_New.MdiParent = Me
    '    Penjualan_New.Show()
    '    Penjualan_New.Focus()
    'End Sub

    'Private Sub PengeluaranStockToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles PengeluaranStockToolStripMenuItem5.Click
    '    EMI_Pengeluaran_Stock.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pengeluaran_Stock.MenuAsal = "PENGELUARAN_STOCK"
    '    EMI_Pengeluaran_Stock.MdiParent = Me
    '    EMI_Pengeluaran_Stock.Show()
    '    EMI_Pengeluaran_Stock.Focus()
    'End Sub

    'Private Sub ValidasiPengeluaranStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiPengeluaranStockToolStripMenuItem.Click
    '    EMI_Validasi_Pengeluaran_Stock.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Validasi_Pengeluaran_Stock.MdiParent = Me
    '    EMI_Validasi_Pengeluaran_Stock.Show()
    '    EMI_Validasi_Pengeluaran_Stock.Focus()
    'End Sub

    'Private Sub DisplayFloorScaleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayFloorScaleToolStripMenuItem.Click
    '    Emi_Display_Timbang_FloorScale.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Display_Timbang_FloorScale.MdiParent = Me
    '    Emi_Display_Timbang_FloorScale.Show()
    '    Emi_Display_Timbang_FloorScale.Focus()
    'End Sub

    Private Sub FormulaProduksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FormulaProduksiToolStripMenuItem.Click
        Transaksi_Formula.StartPosition = FormStartPosition.CenterScreen

        Transaksi_Formula.MdiParent = Me
        Transaksi_Formula.Show()
        Transaksi_Formula.Focus()
    End Sub

    'Private Sub TesTransaction2FormToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TesTransaction2FormToolStripMenuItem.Click
    '    Test_Trasanction_2_Form_Induk.StartPosition = FormStartPosition.CenterScreen

    '    Test_Trasanction_2_Form_Induk.MdiParent = Me
    '    Test_Trasanction_2_Form_Induk.Show()
    '    Test_Trasanction_2_Form_Induk.Focus()
    'End Sub

    'Private Sub GlobalSettingToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles GlobalSettingToolStripMenuItem1.Click
    '    Global_Setting.StartPosition = FormStartPosition.CenterScreen

    '    Global_Setting.MdiParent = Me
    '    Global_Setting.Show()
    '    Global_Setting.Focus()
    'End Sub

    Private Sub MasterBarangToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles MasterBarangToolStripMenuItem2.Click
        Master_Barang_New.StartPosition = FormStartPosition.CenterScreen

        Master_Barang_New.MdiParent = Me
        Master_Barang_New.Show()
        Master_Barang_New.Focus()
    End Sub

    'Private Sub TesToolTipToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TesToolTipToolStripMenuItem.Click
    '    Testing_ToolTip.StartPosition = FormStartPosition.CenterScreen

    '    Testing_ToolTip.MdiParent = Me
    '    Testing_ToolTip.Show()
    '    Testing_ToolTip.Focus()
    'End Sub


    'Private Sub DisplayAdjustmentStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayAdjustmentStockToolStripMenuItem.Click
    '    Emi_Display_Adjusment_Stock.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Display_Adjusment_Stock.MdiParent = Me
    '    Emi_Display_Adjusment_Stock.Show()
    '    Emi_Display_Adjusment_Stock.Focus()
    'End Sub

    'Private Sub TesPaginationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TesPaginationToolStripMenuItem.Click
    '    Tes_Pagination.StartPosition = FormStartPosition.CenterScreen

    '    Tes_Pagination.MdiParent = Me
    '    Tes_Pagination.Show()
    '    Tes_Pagination.Focus()
    'End Sub

    'Private Sub BarangTerkirimToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarangTerkirimToolStripMenuItem.Click
    '    Hr_Cetak_Barang_Terkirim.StartPosition = FormStartPosition.CenterScreen

    '    Hr_Cetak_Barang_Terkirim.MdiParent = Me
    '    Hr_Cetak_Barang_Terkirim.Show()
    '    Hr_Cetak_Barang_Terkirim.Focus()
    'End Sub

    'Private Sub BarangTerkirimGudangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarangTerkirimGudangToolStripMenuItem.Click
    '    Jf_Cetak_Barang_Terkirim_Gudang.StartPosition = FormStartPosition.CenterScreen

    '    Jf_Cetak_Barang_Terkirim_Gudang.MdiParent = Me
    '    Jf_Cetak_Barang_Terkirim_Gudang.Show()
    '    Jf_Cetak_Barang_Terkirim_Gudang.Focus()
    'End Sub

    'Private Sub DisplayBarangToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles DisplayBarangToolStripMenuItem2.Click
    '    Display_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    Display_Barang_Lain.MdiParent = Me
    '    Display_Barang_Lain.Show()
    '    Display_Barang_Lain.Focus()
    'End Sub

    'Private Sub BarangTerkirimPerbarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarangTerkirimPerbarcodeToolStripMenuItem.Click
    '    N_EMI_Laporan_Cetak_Barang_Terkirim_Barcode.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Cetak_Barang_Terkirim_Barcode.MdiParent = Me
    '    N_EMI_Laporan_Cetak_Barang_Terkirim_Barcode.Show()
    '    N_EMI_Laporan_Cetak_Barang_Terkirim_Barcode.Focus()
    'End Sub

    'Private Sub BarangTerkirimPerbarcodeWarehouseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarangTerkirimPerbarcodeWarehouseToolStripMenuItem.Click
    '    N_EMI_Laporan_Cetak_Barang_Terkirim_Barcode_Warehouse.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Cetak_Barang_Terkirim_Barcode_Warehouse.MdiParent = Me
    '    N_EMI_Laporan_Cetak_Barang_Terkirim_Barcode_Warehouse.Show()
    '    N_EMI_Laporan_Cetak_Barang_Terkirim_Barcode_Warehouse.Focus()
    'End Sub

    'Private Sub DisplayPenjualanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayPenjualanToolStripMenuItem.Click
    '    Display_Data_DO_Penjualan_Bisa_Dibuat_SubInvoice.StartPosition = FormStartPosition.CenterScreen

    '    Display_Data_DO_Penjualan_Bisa_Dibuat_SubInvoice.MdiParent = Me
    '    Display_Data_DO_Penjualan_Bisa_Dibuat_SubInvoice.Show()
    '    Display_Data_DO_Penjualan_Bisa_Dibuat_SubInvoice.Focus()
    'End Sub

    'Private Sub LaporanPelunasanCutoffToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LaporanPelunasanCutoffToolStripMenuItem1.Click
    '    N_EMI_Laporan_Pelunasan_Barang_Lain_Cut_Off.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Pelunasan_Barang_Lain_Cut_Off.MdiParent = Me
    '    N_EMI_Laporan_Pelunasan_Barang_Lain_Cut_Off.Show()
    '    N_EMI_Laporan_Pelunasan_Barang_Lain_Cut_Off.Focus()
    'End Sub

    'Private Sub DisplayApprovalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayApprovalToolStripMenuItem.Click
    '    N_EMI_Display_Approval_Waste_Process.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Display_Approval_Waste_Process.MdiParent = Me
    '    N_EMI_Display_Approval_Waste_Process.Show()
    '    N_EMI_Display_Approval_Waste_Process.Focus()
    'End Sub

    'Private Sub ValidasiPemusnahanBarangToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ValidasiPemusnahanBarangToolStripMenuItem1.Click
    '    N_EMI_Transaksi_Pemusnahan_Barang_Validasi.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Pemusnahan_Barang_Validasi.MdiParent = Me
    '    N_EMI_Transaksi_Pemusnahan_Barang_Validasi.Show()
    '    N_EMI_Transaksi_Pemusnahan_Barang_Validasi.Focus()
    'End Sub

    'Private Sub TesSuncronizeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TesSuncronizeToolStripMenuItem.Click
    '    Tes_Approval_Waste.StartPosition = FormStartPosition.CenterScreen

    '    Tes_Approval_Waste.MdiParent = Me
    '    Tes_Approval_Waste.Show()
    '    Tes_Approval_Waste.Focus()
    'End Sub

    'Private Sub ReturDOSementaraToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturDOSementaraToolStripMenuItem.Click
    '    Retur_DO_Reseller_Sementara.StartPosition = FormStartPosition.CenterScreen

    '    Retur_DO_Reseller_Sementara.MdiParent = Me
    '    Retur_DO_Reseller_Sementara.Show()
    '    Retur_DO_Reseller_Sementara.Focus()
    'End Sub

    'Private Sub ReturDOToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ReturDOToolStripMenuItem1.Click
    '    Retur_DO_Reseller.StartPosition = FormStartPosition.CenterScreen

    '    Retur_DO_Reseller.MdiParent = Me
    '    Retur_DO_Reseller.Show()
    '    Retur_DO_Reseller.Focus()
    'End Sub

    'Private Sub DisplatReturDOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplatReturDOToolStripMenuItem.Click
    '    Display_Data_Retur_DO.StartPosition = FormStartPosition.CenterScreen

    '    Display_Data_Retur_DO.MdiParent = Me
    '    Display_Data_Retur_DO.Show()
    '    Display_Data_Retur_DO.Focus()
    'End Sub

    'Private Sub TesMODALToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TesMODALToolStripMenuItem.Click
    '    TES_MODAL.StartPosition = FormStartPosition.CenterScreen

    '    TES_MODAL.MdiParent = Me
    '    TES_MODAL.Show()
    '    TES_MODAL.Focus()
    'End Sub

    'Private Sub LaporanReturDOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanReturDOToolStripMenuItem.Click
    '    Laporan_Retur_DO_Barcode.StartPosition = FormStartPosition.CenterScreen

    '    Laporan_Retur_DO_Barcode.MdiParent = Me
    '    Laporan_Retur_DO_Barcode.Show()
    '    Laporan_Retur_DO_Barcode.Focus()
    'End Sub

    'Private Sub LaporanDPCutOffToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanDPCutOffToolStripMenuItem.Click
    '    N_EMI_Laporan_Down_Payment_CutOff.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Down_Payment_CutOff.MdiParent = Me
    '    N_EMI_Laporan_Down_Payment_CutOff.Show()
    '    N_EMI_Laporan_Down_Payment_CutOff.Focus()
    'End Sub

    'Private Sub LaporanDPCutOffAssetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanDPCutOffAssetToolStripMenuItem.Click
    '    N_EMI_Laporan_Down_Payment_CutOff_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Down_Payment_CutOff_Barang_Lain.MdiParent = Me
    '    N_EMI_Laporan_Down_Payment_CutOff_Barang_Lain.Show()
    '    N_EMI_Laporan_Down_Payment_CutOff_Barang_Lain.Focus()
    'End Sub

    'Private Sub CetakSaldoAkhirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakSaldoAkhirToolStripMenuItem.Click
    '    Hr_Cetak_Saldo_Akhir.StartPosition = FormStartPosition.CenterScreen

    '    Hr_Cetak_Saldo_Akhir.MdiParent = Me
    '    Hr_Cetak_Saldo_Akhir.Show()
    '    Hr_Cetak_Saldo_Akhir.Focus()
    'End Sub

    'Private Sub LaporanHRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanHRToolStripMenuItem.Click
    '    N_EMI_Laporan_Saldo_Akhir_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Saldo_Akhir_Barang_Lain.MdiParent = Me
    '    N_EMI_Laporan_Saldo_Akhir_Barang_Lain.Show()
    '    N_EMI_Laporan_Saldo_Akhir_Barang_Lain.Focus()
    'End Sub

    'Private Sub PelunasanCutOffToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelunasanCutOffToolStripMenuItem.Click
    '    N_EMI_Laporan_Pelunasan_Proyek_Cut_Off.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Pelunasan_Proyek_Cut_Off.MdiParent = Me
    '    N_EMI_Laporan_Pelunasan_Proyek_Cut_Off.Show()
    '    N_EMI_Laporan_Pelunasan_Proyek_Cut_Off.Focus()
    'End Sub

    'Private Sub ValidasiPRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiPRToolStripMenuItem.Click
    '    N_EMI_Validasi_Proc_Purchase_Requisition.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Validasi_Proc_Purchase_Requisition.MdiParent = Me
    '    N_EMI_Validasi_Proc_Purchase_Requisition.Show()
    '    N_EMI_Validasi_Proc_Purchase_Requisition.Focus()
    'End Sub

    'Private Sub LaporanDPCutOffProyekToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanDPCutOffProyekToolStripMenuItem.Click
    '    N_EMI_Laporan_Pembayaran_Di_Muka_CutOff_Proyek.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Pembayaran_Di_Muka_CutOff_Proyek.MdiParent = Me
    '    N_EMI_Laporan_Pembayaran_Di_Muka_CutOff_Proyek.Show()
    '    N_EMI_Laporan_Pembayaran_Di_Muka_CutOff_Proyek.Focus()
    'End Sub


    'Private Sub CompareBudgetingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompareBudgetingToolStripMenuItem.Click
    '    EMI_Compare_Budgeting.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Compare_Budgeting.MdiParent = Me
    '    EMI_Compare_Budgeting.Show()
    '    EMI_Compare_Budgeting.Focus()
    'End Sub

    'Private Sub ValidasiCompareToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiCompareToolStripMenuItem.Click
    '    EMI_Validasi_Budget_Work_Center.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Validasi_Budget_Work_Center.MdiParent = Me
    '    EMI_Validasi_Budget_Work_Center.Show()
    '    EMI_Validasi_Budget_Work_Center.Focus()
    'End Sub

    'Private Sub LaporanCompareBudgetingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanCompareBudgetingToolStripMenuItem.Click
    '    N_EMI_Laporan_Compare_Budgeting.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Compare_Budgeting.MdiParent = Me
    '    N_EMI_Laporan_Compare_Budgeting.Show()
    '    N_EMI_Laporan_Compare_Budgeting.Focus()
    'End Sub

    'Private Sub PemBaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PemBaToolStripMenuItem.Click
    '    N_EMI_Laporan_Down_Payment_CutOff.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Down_Payment_CutOff.MdiParent = Me
    '    N_EMI_Laporan_Down_Payment_CutOff.Show()
    '    N_EMI_Laporan_Down_Payment_CutOff.Focus()
    'End Sub

    'Private Sub PembayaranDimukaBarangLainCutOffToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PembayaranDimukaBarangLainCutOffToolStripMenuItem.Click
    '    N_EMI_Laporan_Down_Payment_CutOff_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Down_Payment_CutOff_Barang_Lain.MdiParent = Me
    '    N_EMI_Laporan_Down_Payment_CutOff_Barang_Lain.Show()
    '    N_EMI_Laporan_Down_Payment_CutOff_Barang_Lain.Focus()
    'End Sub

    'Private Sub PembayaranDimukaProyekCutOffToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PembayaranDimukaProyekCutOffToolStripMenuItem.Click
    '    N_EMI_Laporan_Pembayaran_Di_Muka_CutOff_Proyek.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Pembayaran_Di_Muka_CutOff_Proyek.MdiParent = Me
    '    N_EMI_Laporan_Pembayaran_Di_Muka_CutOff_Proyek.Show()
    '    N_EMI_Laporan_Pembayaran_Di_Muka_CutOff_Proyek.Focus()
    'End Sub

    'Private Sub SelisihBarangMasukBarangLainToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelisihBarangMasukBarangLainToolStripMenuItem.Click
    '    Emi_Selisih_Barang_Masuk_Display_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Selisih_Barang_Masuk_Display_Barang_Lain.MdiParent = Me
    '    Emi_Selisih_Barang_Masuk_Display_Barang_Lain.Show()
    '    Emi_Selisih_Barang_Masuk_Display_Barang_Lain.Focus()
    'End Sub

    'Private Sub MasterKategoriGudangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterKategoriGudangToolStripMenuItem.Click
    '    N_EMI_Master_Kategori_Gudang_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Master_Kategori_Gudang_Barang_Lain.MdiParent = Me
    '    N_EMI_Master_Kategori_Gudang_Barang_Lain.Show()
    '    N_EMI_Master_Kategori_Gudang_Barang_Lain.Focus()
    'End Sub

    'Private Sub MasterBindingKategoriGudangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterBindingKategoriGudangToolStripMenuItem.Click
    '    N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain.MdiParent = Me
    '    N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain.Show()
    '    N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain.Focus()
    'End Sub

    'Private Sub MasterBindingUserKategoriGudangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterBindingUserKategoriGudangToolStripMenuItem.Click
    '    N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain.MdiParent = Me
    '    N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain.Show()
    '    N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain.Focus()
    'End Sub

    'Private Sub PurchaseRequisitionDepartemtnToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PurchaseRequisitionDepartemtnToolStripMenuItem.Click
    '    N_EMI_Purchase_Requisition_Barang_Lain_Departement.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Purchase_Requisition_Barang_Lain_Departement.MdiParent = Me
    '    N_EMI_Purchase_Requisition_Barang_Lain_Departement.Show()
    '    N_EMI_Purchase_Requisition_Barang_Lain_Departement.Focus()
    'End Sub

    'Private Sub TransferStockToolStripMenuItem3_Click_1(sender As Object, e As EventArgs) Handles TransferStockToolStripMenuItem3.Click
    '    N_EMI_Transfer_Stock_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transfer_Stock_Barang_Lain.MdiParent = Me
    '    N_EMI_Transfer_Stock_Barang_Lain.Show()
    '    N_EMI_Transfer_Stock_Barang_Lain.Focus()
    'End Sub

    'Private Sub ValidasiTransferStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiTransferStockToolStripMenuItem.Click
    '    N_EMI_Display_Transfer_Tidak_Timbang_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Display_Transfer_Tidak_Timbang_Barang_Lain.MdiParent = Me
    '    N_EMI_Display_Transfer_Tidak_Timbang_Barang_Lain.Show()
    '    N_EMI_Display_Transfer_Tidak_Timbang_Barang_Lain.Focus()
    'End Sub

    'Private Sub CetakBarcodeStockGudangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakBarcodeStockGudangToolStripMenuItem.Click
    '    N_EMI_Transaksi_Cetak_Barcode_Stock_Per_Gudang.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Cetak_Barcode_Stock_Per_Gudang.MdiParent = Me
    '    N_EMI_Transaksi_Cetak_Barcode_Stock_Per_Gudang.Show()
    '    N_EMI_Transaksi_Cetak_Barcode_Stock_Per_Gudang.Focus()
    'End Sub

    'Private Sub ValidasiTransferMaterialRequisitionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiTransferMaterialRequisitionToolStripMenuItem.Click
    '    N_EMI_Display_Request_Material_QC_Summary.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Display_Request_Material_QC_Summary.MdiParent = Me
    '    N_EMI_Display_Request_Material_QC_Summary.Show()
    '    N_EMI_Display_Request_Material_QC_Summary.Focus()
    'End Sub

    'Private Sub ReturDOMarketingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturDOMarketingToolStripMenuItem.Click
    '    Retur_DO_Reseller_Marketing.StartPosition = FormStartPosition.CenterScreen

    '    Retur_DO_Reseller_Marketing.MdiParent = Me
    '    Retur_DO_Reseller_Marketing.Show()
    '    Retur_DO_Reseller_Marketing.Focus()
    'End Sub

    'Private Sub BalikStockPremixBukanTanamToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BalikStockPremixBukanTanamToolStripMenuItem.Click
    '    CutOFF_PremixSN.StartPosition = FormStartPosition.CenterScreen

    '    CutOFF_PremixSN.MdiParent = Me
    '    CutOFF_PremixSN.Show()
    '    CutOFF_PremixSN.Focus()
    'End Sub

    'Private Sub LaporanPemakaianBahanBakuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanPemakaianBahanBakuToolStripMenuItem.Click
    '    N_EMI_Laporan_Pemakaian_Bahan_Baku.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Pemakaian_Bahan_Baku.MdiParent = Me
    '    N_EMI_Laporan_Pemakaian_Bahan_Baku.Show()
    '    N_EMI_Laporan_Pemakaian_Bahan_Baku.Focus()
    'End Sub

    'Private Sub LaporanPenjualanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanPenjualanToolStripMenuItem.Click
    '    N_EMI_Laporan_Penjualan.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Penjualan.MdiParent = Me
    '    N_EMI_Laporan_Penjualan.Show()
    '    N_EMI_Laporan_Penjualan.Focus()
    'End Sub

    'Private Sub PenyelesaianPRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenyelesaianPRToolStripMenuItem.Click
    '    N_EMI_Transaksi_Validasi_Pengajuan_Selesai_PR_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Validasi_Pengajuan_Selesai_PR_Barang_Lain.MdiParent = Me
    '    N_EMI_Transaksi_Validasi_Pengajuan_Selesai_PR_Barang_Lain.Show()
    '    N_EMI_Transaksi_Validasi_Pengajuan_Selesai_PR_Barang_Lain.Focus()

    'End Sub

    'Private Sub ValidasiProsesPRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiProsesPRToolStripMenuItem.Click
    '    N_EMI_Validasi_Proc_Purchase_Requisition.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Validasi_Proc_Purchase_Requisition.MdiParent = Me
    '    N_EMI_Validasi_Proc_Purchase_Requisition.Show()
    '    N_EMI_Validasi_Proc_Purchase_Requisition.Focus()
    'End Sub

    'Private Sub PRPenawaranToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PRPenawaranToolStripMenuItem.Click
    '    N_EMI_Purchase_Requisition_Penawaran.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Purchase_Requisition_Penawaran.MdiParent = Me
    '    N_EMI_Purchase_Requisition_Penawaran.Show()
    '    N_EMI_Purchase_Requisition_Penawaran.Focus()
    'End Sub

    'Private Sub MasterEstimasiBarangMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterEstimasiBarangMasukToolStripMenuItem.Click
    '    N_EMI_Master_Estimasi_Barang_Masuk.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Master_Estimasi_Barang_Masuk.MdiParent = Me
    '    N_EMI_Master_Estimasi_Barang_Masuk.Show()
    '    N_EMI_Master_Estimasi_Barang_Masuk.Focus()
    'End Sub


    'Private Sub DisplayPRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayPRToolStripMenuItem.Click
    '    EMI_Pembelian_PR_Summary_Data.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pembelian_PR_Summary_Data.MdiParent = Me
    '    EMI_Pembelian_PR_Summary_Data.Show()
    '    EMI_Pembelian_PR_Summary_Data.Focus()
    'End Sub

    'Private Sub PRPenawranToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PRPenawranToolStripMenuItem.Click
    '    N_EMI_Purchase_Requisition_Penawaran_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Purchase_Requisition_Penawaran_Barang_Lain.MdiParent = Me
    '    N_EMI_Purchase_Requisition_Penawaran_Barang_Lain.Show()
    '    N_EMI_Purchase_Requisition_Penawaran_Barang_Lain.Focus()
    'End Sub

    'Private Sub MasterEstimasiBarangMasukBarangLainToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterEstimasiBarangMasukBarangLainToolStripMenuItem.Click
    '    N_EMI_Master_Estimasi_Barang_Masuk_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Master_Estimasi_Barang_Masuk_Barang_Lain.MdiParent = Me
    '    N_EMI_Master_Estimasi_Barang_Masuk_Barang_Lain.Show()
    '    N_EMI_Master_Estimasi_Barang_Masuk_Barang_Lain.Focus()
    'End Sub

    'Private Sub LaporanCompareBudgetingToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LaporanCompareBudgetingToolStripMenuItem1.Click
    '    N_EMI_Laporan_Compare_Budgeting.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Compare_Budgeting.MdiParent = Me
    '    N_EMI_Laporan_Compare_Budgeting.Show()
    '    N_EMI_Laporan_Compare_Budgeting.Focus()
    'End Sub

    'Private Sub TesCetakBarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TesCetakBarcodeToolStripMenuItem.Click
    '    Testing_Cetak_Barcode.StartPosition = FormStartPosition.CenterScreen

    '    Testing_Cetak_Barcode.MdiParent = Me
    '    Testing_Cetak_Barcode.Show()
    '    Testing_Cetak_Barcode.Focus()
    'End Sub

    'Private Sub MasterKategoriJenis5LayerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterKategoriJenis5LayerToolStripMenuItem.Click
    '    N_EMI_Master_Kategori_Jenis_Sub_Kategori.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Master_Kategori_Jenis_Sub_Kategori.MdiParent = Me
    '    N_EMI_Master_Kategori_Jenis_Sub_Kategori.Show()
    '    N_EMI_Master_Kategori_Jenis_Sub_Kategori.Focus()
    'End Sub

    'Private Sub ValiidasiTransferStockTimbangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValiidasiTransferStockTimbangToolStripMenuItem.Click
    '    N_EMI_Transfer_Stock_Validasi_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transfer_Stock_Validasi_Barang_Lain.MdiParent = Me
    '    N_EMI_Transfer_Stock_Validasi_Barang_Lain.Show()
    '    N_EMI_Transfer_Stock_Validasi_Barang_Lain.Focus()
    'End Sub


    'Private Sub DOResselerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DOResselerToolStripMenuItem.Click
    '    DO_Reseller_New.StartPosition = FormStartPosition.CenterScreen

    '    DO_Reseller_New.MdiParent = Me
    '    DO_Reseller_New.Show()
    '    DO_Reseller_New.Focus()
    'End Sub

    'Private Sub PelunasnaKreditDOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelunasnaKreditDOToolStripMenuItem.Click
    '    N_EMI_Transaksi_Pelunasan_Kredit_Per_DO_St2.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Pelunasan_Kredit_Per_DO_St2.MdiParent = Me
    '    N_EMI_Transaksi_Pelunasan_Kredit_Per_DO_St2.Show()
    '    N_EMI_Transaksi_Pelunasan_Kredit_Per_DO_St2.Focus()
    'End Sub

    'Private Sub PelunasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelunasToolStripMenuItem.Click
    '    N_EMI_Transaksi_Pelunasan_Tunai_Per_DO_St3.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Pelunasan_Tunai_Per_DO_St3.MdiParent = Me
    '    N_EMI_Transaksi_Pelunasan_Tunai_Per_DO_St3.Show()
    '    N_EMI_Transaksi_Pelunasan_Tunai_Per_DO_St3.Focus()
    'End Sub

    'Private Sub DisplayPelunasanKreditDOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayPelunasanKreditDOToolStripMenuItem.Click
    '    N_EMI_Display_Data_Pelunasan_Kredit_Per_DO.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Display_Data_Pelunasan_Kredit_Per_DO.MdiParent = Me
    '    N_EMI_Display_Data_Pelunasan_Kredit_Per_DO.Show()
    '    N_EMI_Display_Data_Pelunasan_Kredit_Per_DO.Focus()
    'End Sub

    'Private Sub DisplayPelunasanTunaiDOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayPelunasanTunaiDOToolStripMenuItem.Click
    '    N_EMI_Display_Data_Pelunasan_Tunai_Per_DO.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Display_Data_Pelunasan_Tunai_Per_DO.MdiParent = Me
    '    N_EMI_Display_Data_Pelunasan_Tunai_Per_DO.Show()
    '    N_EMI_Display_Data_Pelunasan_Tunai_Per_DO.Focus()
    'End Sub

    'Private Sub CompareBudgeting2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompareBudgeting2ToolStripMenuItem.Click
    '    EMI_Compare_Budgeting2.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Compare_Budgeting2.MdiParent = Me
    '    EMI_Compare_Budgeting2.Show()
    '    EMI_Compare_Budgeting2.Focus()
    'End Sub

    'Private Sub PembelianToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles PembelianToolStripMenuItem4.Click
    '    Pembelian_Pry.StartPosition = FormStartPosition.CenterScreen

    '    Pembelian_Pry.MdiParent = Me
    '    Pembelian_Pry.Show()
    '    Pembelian_Pry.Focus()
    'End Sub

    Private Sub DisplayGR3ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DisplayGR3ToolStripMenuItem1.Click
        N_EMI_Display_Validasi_GR_3.StartPosition = FormStartPosition.CenterScreen

        N_EMI_Display_Validasi_GR_3.MdiParent = Me
        N_EMI_Display_Validasi_GR_3.Show()
        N_EMI_Display_Validasi_GR_3.Focus()
    End Sub

    'Private Sub PRDepartementToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PRDepartementToolStripMenuItem.Click
    '    N_EMI_Display_Purchase_Requisition_Barang_Lain_Departement.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Display_Purchase_Requisition_Barang_Lain_Departement.MdiParent = Me
    '    N_EMI_Display_Purchase_Requisition_Barang_Lain_Departement.Show()
    '    N_EMI_Display_Purchase_Requisition_Barang_Lain_Departement.Focus()
    'End Sub

    'Private Sub POIndukToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles POIndukToolStripMenuItem2.Click
    '    Display_Summary_POInduk_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    Display_Summary_POInduk_Barang_Lain.MdiParent = Me
    '    Display_Summary_POInduk_Barang_Lain.Show()
    '    Display_Summary_POInduk_Barang_Lain.Focus()
    'End Sub

    'Private Sub MergeBarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MergeBarcodeToolStripMenuItem.Click
    '    N_EMI_Transaksi_Barcode_Merge.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Barcode_Merge.MdiParent = Me
    '    N_EMI_Transaksi_Barcode_Merge.Show()
    '    N_EMI_Transaksi_Barcode_Merge.Focus()
    'End Sub

    'Private Sub DisplayMergeBarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayMergeBarcodeToolStripMenuItem.Click
    '    N_EMI_Display_Barcode_Merge.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Display_Barcode_Merge.MdiParent = Me
    '    N_EMI_Display_Barcode_Merge.Show()
    '    N_EMI_Display_Barcode_Merge.Focus()
    'End Sub

    'Private Sub PenyelesaianPR2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenyelesaianPR2ToolStripMenuItem.Click
    '    EMI_Validasi_Pengajuan_Selesai_PR_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Validasi_Pengajuan_Selesai_PR_Barang_Lain.MdiParent = Me
    '    EMI_Validasi_Pengajuan_Selesai_PR_Barang_Lain.Show()
    '    EMI_Validasi_Pengajuan_Selesai_PR_Barang_Lain.Focus()
    'End Sub

    'Private Sub LaporanBarcodeMergeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanBarcodeMergeToolStripMenuItem.Click
    '    N_EMI_Laporan_Barcode_Merge.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Barcode_Merge.MdiParent = Me
    '    N_EMI_Laporan_Barcode_Merge.Show()
    '    N_EMI_Laporan_Barcode_Merge.Focus()
    'End Sub

    'Private Sub POToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles POToolStripMenuItem1.Click
    '    EMI_Pembelian_PO_Summary_Data_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

    '    EMI_Pembelian_PO_Summary_Data_Barang_Lain.MdiParent = Me
    '    EMI_Pembelian_PO_Summary_Data_Barang_Lain.Show()
    '    EMI_Pembelian_PO_Summary_Data_Barang_Lain.Focus()
    'End Sub

    'Private Sub InputOtomatisPengajuanWasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InputOtomatisPengajuanWasteToolStripMenuItem.Click
    '    N_EMI_Transaksi_Bypass_Pemusnahan_Barang_Per_Barcode.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Bypass_Pemusnahan_Barang_Per_Barcode.MdiParent = Me
    '    N_EMI_Transaksi_Bypass_Pemusnahan_Barang_Per_Barcode.Show()
    '    N_EMI_Transaksi_Bypass_Pemusnahan_Barang_Per_Barcode.Focus()
    'End Sub

    'Private Sub ReturPackagingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturPackagingToolStripMenuItem.Click
    '    N_EMI_Transaksi_Retur_Packaging.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Retur_Packaging.MdiParent = Me
    '    N_EMI_Transaksi_Retur_Packaging.Show()
    '    N_EMI_Transaksi_Retur_Packaging.Focus()
    'End Sub

    'Private Sub TesTabControlToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TesTabControlToolStripMenuItem.Click
    '    Tes_Tab_Control.StartPosition = FormStartPosition.CenterScreen

    '    Tes_Tab_Control.MdiParent = Me
    '    Tes_Tab_Control.Show()
    '    Tes_Tab_Control.Focus()
    'End Sub

    'Private Sub TesLoadingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TesLoadingToolStripMenuItem.Click
    '    Tes_Form_Loading.StartPosition = FormStartPosition.CenterScreen

    '    Tes_Form_Loading.MdiParent = Me
    '    Tes_Form_Loading.Show()
    '    Tes_Form_Loading.Focus()
    'End Sub

    'Private Sub ValidasiAdustmentStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiAdustmentStockToolStripMenuItem.Click
    '    N_EMI_Transaksi_Validasi_Adjustment_Stock.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Validasi_Adjustment_Stock.MdiParent = Me
    '    N_EMI_Transaksi_Validasi_Adjustment_Stock.Show()
    '    N_EMI_Transaksi_Validasi_Adjustment_Stock.Focus()
    'End Sub

    'Private Sub ReturDoToolStripMenuItem2_Click(sender As Object, e As EventArgs)
    '    Laporan_Retur_DO.StartPosition = FormStartPosition.CenterScreen

    '    Laporan_Retur_DO.MdiParent = Me
    '    Laporan_Retur_DO.Show()
    '    Laporan_Retur_DO.Focus()
    'End Sub

    'Private Sub ReturDOBarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturDOBarcodeToolStripMenuItem.Click
    '    Laporan_Retur_DO_Barcode.StartPosition = FormStartPosition.CenterScreen

    '    Laporan_Retur_DO_Barcode.MdiParent = Me
    '    Laporan_Retur_DO_Barcode.Show()
    '    Laporan_Retur_DO_Barcode.Focus()
    'End Sub

    'Private Sub DisplayReturDOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayReturDOToolStripMenuItem.Click
    '    Display_Data_Retur_DO.StartPosition = FormStartPosition.CenterScreen

    '    Display_Data_Retur_DO.MdiParent = Me
    '    Display_Data_Retur_DO.Show()
    '    Display_Data_Retur_DO.Focus()
    'End Sub

    'Private Sub FormulaBindingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FormulaBindingToolStripMenuItem.Click
    '    Display_Formula_Binding.StartPosition = FormStartPosition.CenterScreen

    '    Display_Formula_Binding.MdiParent = Me
    '    Display_Formula_Binding.Show()
    '    Display_Formula_Binding.Focus()
    'End Sub

    'Private Sub PelunasanKreditDOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelunasanKreditDOToolStripMenuItem.Click
    '    N_EMI_Transaksi_Pelunasan_Kredit_Per_DO_St2.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Pelunasan_Kredit_Per_DO_St2.MdiParent = Me
    '    N_EMI_Transaksi_Pelunasan_Kredit_Per_DO_St2.Show()
    '    N_EMI_Transaksi_Pelunasan_Kredit_Per_DO_St2.Focus()
    'End Sub

    'Private Sub PelunasanTunaiDOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelunasanTunaiDOToolStripMenuItem.Click
    '    N_EMI_Transaksi_Pelunasan_Tunai_Per_DO_St3.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Pelunasan_Tunai_Per_DO_St3.MdiParent = Me
    '    N_EMI_Transaksi_Pelunasan_Tunai_Per_DO_St3.Show()
    '    N_EMI_Transaksi_Pelunasan_Tunai_Per_DO_St3.Focus()
    'End Sub

    'Private Sub WasteProsesProductToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WasteProsesProductToolStripMenuItem.Click
    '    N_EMI_Transaksi_Waste_Proses.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Waste_Proses.MdiParent = Me
    '    N_EMI_Transaksi_Waste_Proses.asal_menu = "PRODUCT"
    '    N_EMI_Transaksi_Waste_Proses.Show()
    '    N_EMI_Transaksi_Waste_Proses.Focus()
    'End Sub

    'Private Sub TesSyncApprovalWasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TesSyncApprovalWasteToolStripMenuItem.Click
    '    Tes_Sync_Approval_Waste.StartPosition = FormStartPosition.CenterScreen

    '    Tes_Sync_Approval_Waste.MdiParent = Me
    '    Tes_Sync_Approval_Waste.Show()
    '    Tes_Sync_Approval_Waste.Focus()
    'End Sub

    'Private Sub MasterApprovalLevelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MasterApprovalLevelToolStripMenuItem.Click
    '    N_EMI_Master_Approval_Hierarchy_Waste.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Master_Approval_Hierarchy_Waste.MdiParent = Me
    '    N_EMI_Master_Approval_Hierarchy_Waste.Show()
    '    N_EMI_Master_Approval_Hierarchy_Waste.Focus()

    'End Sub

    'Private Sub DisplayReturPaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayReturPaToolStripMenuItem.Click
    '    N_EMI_Display_Retur_Packaging.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Display_Retur_Packaging.MdiParent = Me
    '    N_EMI_Display_Retur_Packaging.Show()
    '    N_EMI_Display_Retur_Packaging.Focus()
    'End Sub

    'Private Sub LaporanReturPackagingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanReturPackagingToolStripMenuItem.Click
    '    N_EMI_Laporan_Retur_Packaging.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Retur_Packaging.MdiParent = Me
    '    N_EMI_Laporan_Retur_Packaging.Show()
    '    N_EMI_Laporan_Retur_Packaging.Focus()
    'End Sub

    'Private Sub LaporanWasteProductToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanWasteProductToolStripMenuItem.Click
    '    N_EMI_Laporan_Transaksi_Waste_Items.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Transaksi_Waste_Items.MdiParent = Me
    '    N_EMI_Laporan_Transaksi_Waste_Items.Show()
    '    N_EMI_Laporan_Transaksi_Waste_Items.Focus()
    'End Sub

    'Private Sub TransferStockSementaraToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferStockSementaraToolStripMenuItem.Click
    '    N_EMI_Transaksi_Transfer_Stock_Sementara.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Transfer_Stock_Sementara.MenuAsal = "TRANSFER_STOCK"
    '    N_EMI_Transaksi_Transfer_Stock_Sementara.MdiParent = Me
    '    N_EMI_Transaksi_Transfer_Stock_Sementara.Show()
    '    N_EMI_Transaksi_Transfer_Stock_Sementara.Focus()
    'End Sub

    'Private Sub ValidasiTransferStockSementaraTidakTimbangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiTransferStockSementaraTidakTimbangToolStripMenuItem.Click
    '    N_EMI_Transaksi_Transfer_Stock_Sementara_Tidak_Timbang.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Transfer_Stock_Sementara_Tidak_Timbang.MdiParent = Me
    '    N_EMI_Transaksi_Transfer_Stock_Sementara_Tidak_Timbang.Show()
    '    N_EMI_Transaksi_Transfer_Stock_Sementara_Tidak_Timbang.Focus()
    'End Sub

    'Private Sub ValidasiTransferStockSementaraTimbangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiTransferStockSementaraTimbangToolStripMenuItem.Click
    '    N_EMI_Transaksi_Transfer_Stock_Sementara_Timbang.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Transaksi_Transfer_Stock_Sementara_Timbang.MdiParent = Me
    '    N_EMI_Transaksi_Transfer_Stock_Sementara_Timbang.Show()
    '    N_EMI_Transaksi_Transfer_Stock_Sementara_Timbang.Focus()
    'End Sub

    Private Sub PairingRFIDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PairingRFIDToolStripMenuItem.Click
        N_EMI_Pairing_RFID_Touchscreen.StartPosition = FormStartPosition.CenterScreen

        N_EMI_Pairing_RFID_Touchscreen.MdiParent = Me
        N_EMI_Pairing_RFID_Touchscreen.Show()
        N_EMI_Pairing_RFID_Touchscreen.Focus()
    End Sub

    Private Sub DisplayTransferStockSementaraToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayTransferStockSementaraToolStripMenuItem.Click
        N_EMI_Display_Transfer_Stock_Sementara.StartPosition = FormStartPosition.CenterScreen

        N_EMI_Display_Transfer_Stock_Sementara.MdiParent = Me
        N_EMI_Display_Transfer_Stock_Sementara.Show()
        N_EMI_Display_Transfer_Stock_Sementara.Focus()
    End Sub

    Private Sub ValidasiPenerimaanBarangMergeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiPenerimaanBarangMergeToolStripMenuItem.Click
        EMI_Validasi_GR.StartPosition = FormStartPosition.CenterScreen

        EMI_Validasi_GR.MenuAsal = "VALIDASI_GR_MERGE"
        EMI_Validasi_GR.MdiParent = Me
        EMI_Validasi_GR.Show()
        EMI_Validasi_GR.Focus()
    End Sub

    Private Sub PengajuanBarangBaruToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PengajuanBarangBaruToolStripMenuItem.Click
        N_EMI_Transaksi_Pengajuan_Barang_Baru.StartPosition = FormStartPosition.CenterScreen

        N_EMI_Transaksi_Pengajuan_Barang_Baru.MdiParent = Me
        N_EMI_Transaksi_Pengajuan_Barang_Baru.Show()
        N_EMI_Transaksi_Pengajuan_Barang_Baru.Focus()
    End Sub
    Private Sub AdjustmentBarangLainToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdjustmentBarangLainToolStripMenuItem.Click
        N_EMI_Transaksi_Adjustment_Stock_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

        N_EMI_Transaksi_Adjustment_Stock_Barang_Lain.MdiParent = Me
        N_EMI_Transaksi_Adjustment_Stock_Barang_Lain.Show()
        N_EMI_Transaksi_Adjustment_Stock_Barang_Lain.Focus()
    End Sub

    Private Sub ValidasiAdjustmentStockBarangLainToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiAdjustmentStockBarangLainToolStripMenuItem.Click
        N_EMI_Transaksi_Validasi_Adjustment_Stock_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

        N_EMI_Transaksi_Validasi_Adjustment_Stock_Barang_Lain.MdiParent = Me
        N_EMI_Transaksi_Validasi_Adjustment_Stock_Barang_Lain.Show()
        N_EMI_Transaksi_Validasi_Adjustment_Stock_Barang_Lain.Focus()
    End Sub

    Private Sub DisplayAdjustmentStockBarangLainToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayAdjustmentStockBarangLainToolStripMenuItem.Click
        N_EMI_Display_Adjustment_Stock_Barang_Lain.StartPosition = FormStartPosition.CenterScreen

        N_EMI_Display_Adjustment_Stock_Barang_Lain.MdiParent = Me
        N_EMI_Display_Adjustment_Stock_Barang_Lain.Show()
        N_EMI_Display_Adjustment_Stock_Barang_Lain.Focus()
    End Sub

    Private Sub PengajuanBarangBaruToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PengajuanBarangBaruToolStripMenuItem1.Click
        N_EMI_Display_Pengajuan_Barang_Baru.StartPosition = FormStartPosition.CenterScreen

        N_EMI_Display_Pengajuan_Barang_Baru.MdiParent = Me
        N_EMI_Display_Pengajuan_Barang_Baru.Show()
        N_EMI_Display_Pengajuan_Barang_Baru.Focus()
    End Sub

    Private Sub PurchaseRequisitionTrialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PurchaseRequisitionTrialToolStripMenuItem.Click
        N_EMI_Transaksi_Purchase_Requisition_Trial.StartPosition = FormStartPosition.CenterScreen

        N_EMI_Transaksi_Purchase_Requisition_Trial.MdiParent = Me
        N_EMI_Transaksi_Purchase_Requisition_Trial.Show()
        N_EMI_Transaksi_Purchase_Requisition_Trial.Focus()
    End Sub

    Private Sub ProductionOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductionOrderToolStripMenuItem.Click
        N_EMI_Transaksi_Trial_Production_Order.StartPosition = FormStartPosition.CenterScreen

        N_EMI_Transaksi_Trial_Production_Order.MdiParent = Me
        N_EMI_Transaksi_Trial_Production_Order.Show()
        N_EMI_Transaksi_Trial_Production_Order.Focus()
    End Sub

    Private Sub SplitProduksiToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SplitProduksiToolStripMenuItem1.Click
        N_EMI_Display_Trial_Mulai_Produksi.StartPosition = FormStartPosition.CenterScreen

        N_EMI_Display_Trial_Mulai_Produksi.MdiParent = Me
        N_EMI_Display_Trial_Mulai_Produksi.Show()
        N_EMI_Display_Trial_Mulai_Produksi.Focus()
    End Sub

    Private Sub CreateFormulaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateFormulaToolStripMenuItem.Click
        Transaksi_Formula.StartPosition = FormStartPosition.CenterScreen

        Transaksi_Formula.MdiParent = Me
        Transaksi_Formula.Show()
        Transaksi_Formula.Focus()
    End Sub

    Private Sub ValidasiFormulaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiFormulaToolStripMenuItem.Click
        N_EMI_Transaksi_Validasi_Formula.StartPosition = FormStartPosition.CenterScreen

        N_EMI_Transaksi_Validasi_Formula.MdiParent = Me
        N_EMI_Transaksi_Validasi_Formula.Show()
        N_EMI_Transaksi_Validasi_Formula.Focus()
    End Sub



    'Private Sub LaporanTransferStoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanTransferStoToolStripMenuItem.Click
    '    N_EMI_Laporan_Transfer_Stock_Sementara.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Laporan_Transfer_Stock_Sementara.MdiParent = Me
    '    N_EMI_Laporan_Transfer_Stock_Sementara.Show()
    '    N_EMI_Laporan_Transfer_Stock_Sementara.Focus()
    'End Sub

    'Private Sub LaporanSplitStockToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LaporanSplitStockToolStripMenuItem1.Click
    '    Emi_Laporan_Split_Stock.StartPosition = FormStartPosition.CenterScreen

    '    Emi_Laporan_Split_Stock.MdiParent = Me
    '    Emi_Laporan_Split_Stock.Show()
    '    Emi_Laporan_Split_Stock.Focus()
    'End Sub

    'Private Sub RFIDTAGToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RFIDTAGToolStripMenuItem.Click
    '    N_EMI_Registrasi_RFID_Tag.StartPosition = FormStartPosition.CenterScreen

    '    N_EMI_Registrasi_RFID_Tag.MdiParent = Me
    '    N_EMI_Registrasi_RFID_Tag.Show()
    '    N_EMI_Registrasi_RFID_Tag.Focus()
    'End Sub
End Class