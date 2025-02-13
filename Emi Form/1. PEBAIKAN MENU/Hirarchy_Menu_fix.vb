Public Class Hirarchy_Menu_fix

    Private Function Hirarchy_Menu(formToOpen)

        Select Case formToOpen

            '====================
            '=     PURCHASE     =
            '====================
#Region "MODUL PURCHASE"

             '=== MASTER DATA ==='
            Case "Master_Ekspedisi" : Return Master_Ekspedisi
            Case "Master_Jenis_Selisih" : Return Master_Jenis_Selisih
            Case "Master_Kategori_Harga" : Return Master_Kategori_Harga
            Case "Master_Kategori_Harga_Detail" : Return Master_Kategori_Harga_Detail
            Case "Master_Kategori_PO" : Return Master_Kategori_PO
            Case "Master_Kategori_PO_Role" : Return Master_Kategori_PO_Role
            Case "Master_Media_Kirim" : Return Master_Media_Kirim
            Case "Master_Perhitungan_Jatuh_Tempo" : Return Master_Perhitungan_Jatuh_Tempo
            Case "Master_Suppliers" : Return Master_Suppliers

                ' TRANSAKSI 
                '=== Penawaran ==='
            Case "Transaksi_Penawaran" : Return Transaksi_Penawaran

                '=== Pembelian ==='
            Case "EMI_PO_Pembelian" : Return EMI_PO_Pembelian
            Case "Purchase_Requisition" : Return Purchase_Requisition
            Case "Emi_Selisih_Barang_Masuk_Display" : Return Emi_Selisih_Barang_Masuk_Display
            Case "EMI_PO_Pembelian_Display2" : Return EMI_PO_Pembelian_Display2

                '=== Pelunasan ==='
            Case "EMI_Pelunasan" : Return EMI_Pelunasan
            Case "EMI_DownPayment" : Return EMI_DownPayment
            Case "EMI_DownPayment_Binding" : Return EMI_DownPayment_Binding

                 'DISPLAY 
                 '=== Penawaran ==='
            Case "Emi_Display_Barang_Penawaran" : Return Emi_Display_Barang_Penawaran
            Case "EMI_Penawaran_Harga_Summary_Data" : Return EMI_Penawaran_Harga_Summary_Data

                '=== Pembelian ==='
            Case "EMI_Display_HPP" : Return EMI_Display_HPP
            Case "EMI_Pembelian_PO_Summary_Data" : Return EMI_Pembelian_PO_Summary_Data
            Case "EMI_Pembelian_PR_Summary_Data" : Return EMI_Pembelian_PR_Summary_Data

                '=== Pelunasan ==='
            Case "Display_Emi_Pelunasan" : Return Display_Emi_Pelunasan
            Case "Display_Emi_Pelunasan_Hutang" : Return Display_Emi_Pelunasan_Hutang
                'Case "EMI_Hutang_PO_Summary_Data" : Return EMI_Hutang_PO_Summary_Data
                'Case "Laporan_Hutang_Biaya_Import_Summary_Data" : Return Laporan_Hutang_Biaya_Import_Summary_Data

                '=== LAPORAN ==='
                '=== Pembelian ==='
            Case "Laporan_Purchase_Order" : Return Laporan_Purchase_Order
            Case "Laporan_Purchase_Requisition" : Return Laporan_Purchase_Requisition

                '=== Pelunasan ==='
            Case "Laporan_HPP_Import" : Return Laporan_HPP_Import
            Case "Laporan_HPP_Local" : Return Laporan_HPP_Local


#End Region

#Region "Production"
            Case "EMI_Independent_Order" : Return EMI_Independent_Order
            Case "EMI_Production_Order" : Return EMI_Production_Order
            Case "EMI_Schedule" : Return EMI_Schedule

            Case "EMI_Compare_Work_Center" : Return EMI_Compare_Work_Center
            Case "EMI_HPP_Production" : Return EMI_HPP_Production
            Case "EMI_Transaksi_Actual_Biaya_Produksi" : Return EMI_Transaksi_Actual_Biaya_Produksi
            Case "EMI_Transaksi_Work_Center" : Return EMI_Transaksi_Work_Center
            Case "EMI_Transaksi_Work_Center_PerBulan" : Return EMI_Transaksi_Work_Center_PerBulan

            Case "EMI_Display_Hasil_Produksi" : Return EMI_Display_Hasil_Produksi
            Case "EMI_Hasil_Pengeluaran_Bahan_Baku" : Return EMI_Hasil_Pengeluaran_Bahan_Baku
            Case "EMI_Hasil_Production" : Return EMI_Hasil_Production
            Case "EMI_Hasil_ProductionFG" : Return EMI_Hasil_ProductionFG
            Case "Emi_Production_Barcode" : Return Emi_Production_Barcode
            Case "EMI_Produksi" : Return EMI_Produksi
            Case "Emi_Request_Material" : Return Emi_Request_Material
            Case "Emi_Transfer_Quality_Production" : Return Emi_Transfer_Quality_Production

            Case "EMI_Transaksi_Actual_Biaya_Produksi_Display" : Return EMI_Transaksi_Actual_Biaya_Produksi_Display
            Case "Emi_Transfer_Quality_Production" : Return Emi_Transfer_Quality_Production
            Case "Emi_Transfer_Quality_Production" : Return Emi_Transfer_Quality_Production
            Case "Emi_Transfer_Quality_Production" : Return Emi_Transfer_Quality_Production











#End Region

















        End Select

    End Function

End Class
