Public Class Emi_Validasi_HPP_Produksi
    Dim LvBatch As String
    Dim LvQty_Rw_Formula As String
    Dim LvQty_Rw_Dosing As String
    Dim LvSatuan As String
    Dim LvFG_Seharus As String
    Dim LvTotal_Bahan_Dosing As String
    Dim LvTotal_Pack_Dosing As String
    Dim LvTotal_Biaya_Dosing As String
    Dim LvNilai_Loss_Pro As String
    Dim LvNilai_HPP_PerPcs As String
    Dim LvJml_FG_Real As String
    Dim LvTotal_HPP_FG_Real As String
    Dim LvTotal_Selisih_FG As String

    Dim LvPackagingBatch As String
    Dim LvPackagingKdBarang As String
    Dim LvPackagingNama As String
    Dim LvPackagingSatuan As String
    Dim LvPackagingJumlahDosing As String
    Dim LvPackagingJumlahReal As String
    Dim LvPackagingSelisih As String



    Dim CellBatch As Integer = 0
    Dim CellQty_Rw_Formula As Integer = 1
    Dim CellQty_Rw_Dosing As Integer = 2
    Dim CellSatuan As Integer = 3
    Dim CellFG_Seharus As Integer = 4
    Dim CellTotal_Bahan_Dosing As Integer = 5
    Dim CellTotal_Pack_Dosing As Integer = 6
    Dim CellTotal_Biaya_Dosing As Integer = 7
    Dim CellNilai_Loss_Pro As Integer = 8
    Dim CellNilai_HPP_PerPcs As Integer = 9
    Dim CellJml_FG_Real As Integer = 10
    Dim CellTotal_HPP_FG_Real As Integer = 11
    Dim CellTotal_Selisih_FG As Integer = 12

    Dim CellPackagingBatch As Integer = 0
    Dim CellPackagingKdBarang As Integer = 1
    Dim CellPackagingNama As Integer = 2
    Dim CellPackagingSatuan As Integer = 3
    Dim CellPackagingJumlahDosing As Integer = 4
    Dim CellPackagingJumlahReal As Integer = 5
    Dim CellPackagingSelisih As Integer = 6

    Dim CellFGTanggal As Integer = 0
    Dim CellFGJam As Integer = 1
    Dim CellFGBatchNUmber As Integer = 2
    Dim CellFG_KdBarang As Integer = 3
    Dim CellFG_Barang As Integer = 4
    Dim CellFGQrCode As Integer = 5
    Dim CellFGJumlah As Integer = 6
    Dim CellFGSatuan As Integer = 7
    Dim CellFGHPP As Integer = 8
    Dim CellFGTotal As Integer = 9
    Dim cellFGBatch As Integer = 10

    Dim LvFGTanggal As String
    Dim LvFGJam As String
    Dim LvFGBatchNUmber As String
    Dim LvFGKdBarang As String
    Dim LvFGNmBarang As String
    Dim LvFGQrCode As String
    Dim LvFGJumlah As String
    Dim LvFGSatuan As String
    Dim LvFGHPP As String
    Dim LvFGTotal As String
    Dim lvFGBatch As String

    Dim CellScp_Tanggal As Integer = 0
    Dim CellScp_Jam As Integer = 1
    Dim CellScp_BatchNUmber As Integer = 2
    Dim CellScp_KdBarang As Integer = 3
    Dim CellScp_Barang As Integer = 4
    Dim CellScp_QrCode As Integer = 5
    Dim CellScp_Jumlah As Integer = 6
    Dim CellScp_Satuan As Integer = 7
    Dim CellScp_HPP As Integer = 8
    Dim CellScp_Total As Integer = 9
    Dim CellScp_Batch As Integer = 10

    Dim LvSCP_Tanggal As String
    Dim LvSCP_Jam As String
    Dim LvSCP_BatchNUmber As String
    Dim LvSCP_KdBarang As String
    Dim LvSCP_NmBarang As String
    Dim LvSCP_QrCode As String
    Dim LvSCP_Jumlah As String
    Dim LvSCP_Satuan As String
    Dim LvSCP_HPP As String
    Dim LvSCP_Total As String
    Dim LvSCP_Batch As String


    Private Function CekNothing(ByVal str As String) As String
        Dim hasil As String = ""

        If str Is Nothing Then
            hasil = ""
        Else
            hasil = str
        End If

        Return hasil
    End Function
    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvBatch = CekNothing(DgvDosing.Rows(No_Index).Cells(CellBatch).Value)
        LvQty_Rw_Formula = CekNothing(DgvDosing.Rows(No_Index).Cells(CellQty_Rw_Formula).Value)
        LvQty_Rw_Dosing = CekNothing(DgvDosing.Rows(No_Index).Cells(CellQty_Rw_Dosing).Value)
        LvSatuan = CekNothing(DgvDosing.Rows(No_Index).Cells(CellSatuan).Value)
        LvFG_Seharus = CekNothing(DgvDosing.Rows(No_Index).Cells(CellFG_Seharus).Value)
        LvTotal_Bahan_Dosing = CekNothing(DgvDosing.Rows(No_Index).Cells(CellTotal_Bahan_Dosing).Value)
        LvTotal_Pack_Dosing = CekNothing(DgvDosing.Rows(No_Index).Cells(CellTotal_Pack_Dosing).Value)
        LvTotal_Biaya_Dosing = CekNothing(DgvDosing.Rows(No_Index).Cells(CellTotal_Biaya_Dosing).Value)
        LvNilai_Loss_Pro = CekNothing(DgvDosing.Rows(No_Index).Cells(CellNilai_Loss_Pro).Value)
        LvNilai_HPP_PerPcs = CekNothing(DgvDosing.Rows(No_Index).Cells(CellNilai_HPP_PerPcs).Value)
        LvJml_FG_Real = CekNothing(DgvDosing.Rows(No_Index).Cells(CellJml_FG_Real).Value)
        LvTotal_HPP_FG_Real = CekNothing(DgvDosing.Rows(No_Index).Cells(CellTotal_HPP_FG_Real).Value)
    End Sub

    Public Sub Get_Isi_Listview_FG(ByVal No_Index As Integer)
        LvFGTanggal = CekNothing(DgvHPP.Rows(No_Index).Cells(CellFGTanggal).Value)
        LvFGJam = CekNothing(DgvHPP.Rows(No_Index).Cells(CellFGJam).Value)
        LvFGBatchNUmber = CekNothing(DgvHPP.Rows(No_Index).Cells(CellFGBatchNUmber).Value)
        LvFGKdBarang = CekNothing(DgvHPP.Rows(No_Index).Cells(CellFG_KdBarang).Value)
        LvFGNmBarang = CekNothing(DgvHPP.Rows(No_Index).Cells(CellFG_Barang).Value)
        LvFGQrCode = CekNothing(DgvHPP.Rows(No_Index).Cells(CellFGQrCode).Value)
        LvFGJumlah = CekNothing(DgvHPP.Rows(No_Index).Cells(CellFGJumlah).Value)
        LvFGSatuan = CekNothing(DgvHPP.Rows(No_Index).Cells(CellFGSatuan).Value)
        LvFGHPP = CekNothing(DgvHPP.Rows(No_Index).Cells(CellFGHPP).Value)
        LvFGTotal = CekNothing(DgvHPP.Rows(No_Index).Cells(CellFGTotal).Value)
        lvFGBatch = CekNothing(DgvHPP.Rows(No_Index).Cells(cellFGBatch).Value)
    End Sub

    Public Sub Get_Isi_Listview_Scrap(ByVal No_Index As Integer)
        LvSCP_Tanggal = CekNothing(Dgv_Hpp_Scrap.Rows(No_Index).Cells(CellScp_Tanggal).Value)
        LvSCP_Jam = CekNothing(Dgv_Hpp_Scrap.Rows(No_Index).Cells(CellScp_Jam).Value)
        LvSCP_BatchNUmber = CekNothing(Dgv_Hpp_Scrap.Rows(No_Index).Cells(CellScp_BatchNUmber).Value)
        LvSCP_KdBarang = CekNothing(Dgv_Hpp_Scrap.Rows(No_Index).Cells(CellScp_KdBarang).Value)
        LvSCP_NmBarang = CekNothing(Dgv_Hpp_Scrap.Rows(No_Index).Cells(CellScp_Barang).Value)
        LvSCP_QrCode = CekNothing(Dgv_Hpp_Scrap.Rows(No_Index).Cells(CellScp_QrCode).Value)
        LvSCP_Jumlah = CekNothing(Dgv_Hpp_Scrap.Rows(No_Index).Cells(CellScp_Jumlah).Value)
        LvSCP_Satuan = CekNothing(Dgv_Hpp_Scrap.Rows(No_Index).Cells(CellScp_Satuan).Value)
        LvSCP_HPP = CekNothing(Dgv_Hpp_Scrap.Rows(No_Index).Cells(CellScp_HPP).Value)
        LvSCP_Total = CekNothing(Dgv_Hpp_Scrap.Rows(No_Index).Cells(CellScp_Total).Value)
        LvSCP_Batch = CekNothing(Dgv_Hpp_Scrap.Rows(No_Index).Cells(CellScp_Batch).Value)
    End Sub


    Private Function Ubah_Angka_Kecil(ByVal kodeBarang As String, ByVal satuanBesar As String, ByVal satuanKecil As String, ByVal jumlahConvert As String) As Double

        Dim total_kecil As Double = 0
        SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & kodeBarang & "', '" & satuanBesar & "',"
        SQL = SQL & "'" & satuanKecil & "', '" & HilangkanTanda(jumlahConvert) & "' ) as hasil"
        Using Dr1 = OpenTrans(SQL)
            If Dr1.Read Then
                If General_Class.CekNULL(Dr1("hasil")) = "" Then
                    Dr1.Close()
                    'CloseTrans()
                    CloseConn()
                    MessageBox.Show("data konversi satuan kirim tidak ada ")
                End If

                total_kecil = Dr1("hasil")
            Else
                Dr1.Close()
                'CloseTrans()
                CloseConn()
                MessageBox.Show("data konversi satuan kirim tidak ada ")
            End If
        End Using

        Return total_kecil
    End Function
    Private Sub Emi_Validasi_HPP_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            OpenConn()

            TextBox3.Text = ""

            DgvDosing.Rows.Clear()
            SQL = "select a.Proses,a.Jumlah_Formula,a.Jumlah_Dosing,a.Satuan,a.Jumlah_Dosing_Pcs,a.Total_Bahan_Baku,a.Total_Packaging,a.Total_Biaya_Produksi,"
            SQL = SQL & "a.Nilai_Loss_Production,a.Jumlah_Terpakai,a.Persen_Loss_Production, c.kode_barang "
            SQL = SQL & "from Emi_Production_Results_HPP a,Emi_Production_Results b, Emi_Split_Production_Order c  "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.kode_Perusahaan=c.kode_Perusahaan and b.No_Production_Order= c.No_Transaksi and c.status is null "
            SQL = SQL & "and b.No_Production_Order = '" & Txt_NoSplitProduksi.Text & "' and a.tanggal is not null order by a.Proses "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        DgvDosing.Rows.Add(1)
                        TextBox3.Text = Format(.Rows(i).Item("Persen_Loss_Production"), "N0")

                        DgvDosing.Rows(i).Cells(CellBatch).Value = "" & .Rows(i).Item("Proses")
                        DgvDosing.Rows(i).Cells(CellQty_Rw_Formula).Value = Format(.Rows(i).Item("Jumlah_Formula"), "N0")
                        DgvDosing.Rows(i).Cells(CellQty_Rw_Dosing).Value = Format(.Rows(i).Item("Jumlah_Dosing"), "N4")
                        DgvDosing.Rows(i).Cells(CellSatuan).Value = .Rows(i).Item("Satuan")
                        DgvDosing.Rows(i).Cells(CellFG_Seharus).Value = Format(.Rows(i).Item("Jumlah_Dosing_Pcs"), "N0")
                        DgvDosing.Rows(i).Cells(CellTotal_Bahan_Dosing).Value = Format(.Rows(i).Item("Total_Bahan_Baku"), "N0")
                        DgvDosing.Rows(i).Cells(CellTotal_Pack_Dosing).Value = Format(.Rows(i).Item("Total_Packaging"), "N0")
                        DgvDosing.Rows(i).Cells(CellTotal_Biaya_Dosing).Value = Format(.Rows(i).Item("Total_Biaya_Produksi"), "N0")
                        DgvDosing.Rows(i).Cells(CellNilai_Loss_Pro).Value = Format(.Rows(i).Item("Nilai_Loss_Production"), "N0")
                        DgvDosing.Rows(i).Cells(CellNilai_HPP_PerPcs).Value = Format(.Rows(i).Item("Jumlah_Dosing"), "N0") 'blm ok

                        Dim nilai_sisa_Pcs As Double = Ubah_Angka_Kecil(.Rows(i).Item("Kode_Barang"), "KG", "Pcs", .Rows(i).Item("Jumlah_Terpakai"))

                        DgvDosing.Rows(i).Cells(CellJml_FG_Real).Value = Format(nilai_sisa_Pcs, "N0")
                        DgvDosing.Rows(i).Cells(CellTotal_HPP_FG_Real).Value = Format(.Rows(i).Item("Jumlah_Dosing"), "N0") 'blm ok

                        DgvDosing.Rows(i).Cells(CellTotal_Selisih_FG).Value = Format(.Rows(i).Item("Jumlah_Dosing_Pcs") - nilai_sisa_Pcs, "N0")

                    Next
                End With
            End Using

            'DgvPackaging.Rows.Clear()
            'SQL = "Select b.proses, b.kode_barang, d.nama, b.satuan, sum(Nilai) As Jumlah, sum(jumlah_pakai) As pakai "
            'SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Packaging_Detail b, Emi_Production_Results_Packaging_Det c, barang d "
            'SQL = SQL & "where a.kode_perusahaan = b.kode_Perusahaan And a.No_Transaksi = b.no_transaksi And a.status Is null And "
            'SQL = SQL & "b.kode_perusahaan = c.kode_Perusahaan And b.No_Transaksi = c.no_transaksi And b.urut = c.No_Urut_detail And "
            'SQL = SQL & "b.Kode_Perusahaan = d.Kode_Perusahaan And b.kode_barang = d.Kode_Barang And b.Kode_Stock_Owner = d.Kode_Stock_Owner "
            'SQL = SQL & "And a.Kode_Perusahaan='" & KodePerusahaan & "' and a.No_Production_Order = '" & Txt_NoSplitProduksi.Text & "' "
            'SQL = SQL & "group by b.proses, b.kode_barang, d.nama, b.satuan "
            'Using Ds = BindingTrans(SQL)
            '    With Ds.Tables("MyTable")
            '        For i As Integer = 0 To .Rows.Count - 1
            '            DgvPackaging.Rows.Add(1)

            '            DgvPackaging.Rows(i).Cells(CellPackagingBatch).Value = "" & .Rows(i).Item("Proses")
            '            DgvPackaging.Rows(i).Cells(CellPackagingKdBarang).Value = "" & .Rows(i).Item("kode_barang")
            '            DgvPackaging.Rows(i).Cells(CellPackagingNama).Value = "" & .Rows(i).Item("nama")
            '            DgvPackaging.Rows(i).Cells(CellPackagingSatuan).Value = "" & .Rows(i).Item("satuan")

            '            DgvPackaging.Rows(i).Cells(CellPackagingJumlahDosing).Value = Format(.Rows(i).Item("Jumlah"), "N0")
            '            DgvPackaging.Rows(i).Cells(CellPackagingJumlahReal).Value = Format(.Rows(i).Item("pakai"), "N0")
            '            DgvPackaging.Rows(i).Cells(CellPackagingSelisih).Value = Format(.Rows(i).Item("Jumlah") - .Rows(i).Item("pakai"), "N0")

            '        Next
            '    End With
            'End Using

            DgvHPP.Rows.Clear()
            'SQL = "Select b.Tanggal, b.Jam, c.Batch_Number, c.Qr_Code+'-'+Kode_Unik_Berjalan as Qr_Code, c.Jumlah, b.Satuan, dbo.get_hpp(c.Serial_Number) as Harga "
            'SQL = SQL & "From Emi_Production_Results a, EMI_Production_Results_Detail_Barang b, Emi_Production_Results_Detail_Pallet c "
            'SQL = SQL & "Where a.kode_perusahaan = b.kode_Perusahaan And a.No_Transaksi = b.no_transaksi And a.status Is null And "
            'SQL = SQL & "b.kode_perusahaan = c.kode_Perusahaan And b.No_Transaksi = c.no_transaksi And b.Proses = c.Proses "
            'SQL = SQL & "And a.Kode_Perusahaan='" & KodePerusahaan & "' and a.No_Production_Order = '" & Txt_NoSplitProduksi.Text & "' "

            SQL = "Select a.No_Production_Order, b.Tanggal, b.Jam, c.Batch_Number, b.Kode_Barang, d.Nama as Nama_Barang, c.Qr_Code+'-'+Kode_Unik_Berjalan as Qr_Code, "
            SQL = SQL & "sum(c.Jumlah) as Jumlah, b.Satuan, sum(dbo.get_hpp(c.Serial_Number)) as Harga, c.tahap "
            SQL = SQL & "From Emi_Production_Results a, EMI_Production_Results_Detail_Barang b, Emi_Production_Results_Detail_Pallet c, barang d "
            SQL = SQL & "Where a.kode_perusahaan = b.kode_Perusahaan And a.No_Transaksi = b.no_transaksi And a.status Is null and b.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and b.kode_perusahaan = c.kode_Perusahaan And b.No_Transaksi = c.no_transaksi And b.Proses = c.Proses "
            SQL = SQL & "and b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "And a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & Txt_NoSplitProduksi.Text & "' "
            SQL = SQL & "group by a.No_Production_Order, b.Tanggal, b.Jam, c.Batch_Number, b.Kode_Barang, d.Nama, (c.Qr_Code+'-'+Kode_Unik_Berjalan), b.Satuan, c.tahap "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        DgvHPP.Rows.Add(1)

                        DgvHPP.Rows(i).Cells(CellFGTanggal).Value = Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy")
                        DgvHPP.Rows(i).Cells(CellFGJam).Value = .Rows(i).Item("Jam")
                        DgvHPP.Rows(i).Cells(CellFGBatchNUmber).Value = .Rows(i).Item("Batch_Number")
                        DgvHPP.Rows(i).Cells(CellFG_KdBarang).Value = .Rows(i).Item("Kode_Barang")
                        DgvHPP.Rows(i).Cells(CellFG_Barang).Value = .Rows(i).Item("Nama_Barang")
                        DgvHPP.Rows(i).Cells(CellFGQrCode).Value = .Rows(i).Item("Qr_Code")
                        DgvHPP.Rows(i).Cells(CellFGJumlah).Value = Format(.Rows(i).Item("Jumlah"), "N0")
                        DgvHPP.Rows(i).Cells(CellFGSatuan).Value = .Rows(i).Item("satuan")
                        DgvHPP.Rows(i).Cells(CellFGHPP).Value = Format(.Rows(i).Item("Harga"), "N0")

                        DgvHPP.Rows(i).Cells(CellFGTotal).Value = Format(.Rows(i).Item("Harga") * .Rows(i).Item("Jumlah"), "N0")
                        DgvHPP.Rows(i).Cells(cellFGBatch).Value = .Rows(i).Item("tahap")

                    Next
                End With
            End Using



            Dgv_Hpp_Scrap.Rows.Clear()
            SQL = "Select a.No_Transaksi, a.No_Production_Order, b.Tanggal, b.Jam, c.Batch_Number, e.Kode_Barang, e.Nama as Nama_Barang, c.Qr_Code+'-'+c.Kode_Unik_Berjalan as Qr_Code, "
            SQL = SQL & "sum(c.Jumlah) as Jumlah, b.Satuan, sum(dbo.get_hpp(c.Serial_Number)) as Harga, f.Tahap "
            SQL = SQL & "From Emi_Production_Results a, EMI_Production_Results_Detail_Barang b, EMI_Production_Results_Detail_Scrap c, Barang_sn d, Barang e, Emi_Production_Results_Detail_Pallet f "
            SQL = SQL & "Where a.kode_perusahaan = b.kode_Perusahaan And a.No_Transaksi = b.no_transaksi And a.status Is null and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and b.kode_perusahaan = c.kode_Perusahaan And b.No_Transaksi = c.no_transaksi And b.Proses = c.Proses "
            SQL = SQL & "and c.Serial_Number = d.Serial_Number and c.Kode_Unik_Berjalan = d.Kode_Unik_Berjalan and c.Kode_Unik_Asal = d.Kode_Unik_Asal "
            SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and d.Kode_Stock_Owner = e.Kode_Stock_Owner and d.Kode_Barang = e.Kode_Barang "
            SQL = SQL & "and b.Kode_Perusahaan = f.Kode_Perusahaan and b.No_Transaksi = f.No_Transaksi and b.Proses = f.Proses "
            SQL = SQL & "And a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & Txt_NoSplitProduksi.Text & "' "
            SQL = SQL & "and c.Jumlah <> 0 "
            SQL = SQL & "group by a.No_Transaksi, a.No_Production_Order, b.Tanggal, b.Jam, c.Batch_Number, e.Kode_Barang, e.Nama, c.Qr_Code+'-'+c.Kode_Unik_Berjalan, b.Satuan, f.Tahap "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Dgv_Hpp_Scrap.Rows.Add(1)

                        Dgv_Hpp_Scrap.Rows(i).Cells(CellScp_Tanggal).Value = Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy")
                        Dgv_Hpp_Scrap.Rows(i).Cells(CellScp_Jam).Value = .Rows(i).Item("Jam")
                        Dgv_Hpp_Scrap.Rows(i).Cells(CellScp_BatchNUmber).Value = .Rows(i).Item("Batch_Number")
                        Dgv_Hpp_Scrap.Rows(i).Cells(CellScp_KdBarang).Value = .Rows(i).Item("Kode_Barang")
                        Dgv_Hpp_Scrap.Rows(i).Cells(CellScp_Barang).Value = .Rows(i).Item("Nama_Barang")
                        Dgv_Hpp_Scrap.Rows(i).Cells(CellScp_QrCode).Value = .Rows(i).Item("Qr_Code")
                        Dgv_Hpp_Scrap.Rows(i).Cells(CellScp_Jumlah).Value = Format(.Rows(i).Item("Jumlah"), "N0")
                        Dgv_Hpp_Scrap.Rows(i).Cells(CellScp_Satuan).Value = .Rows(i).Item("satuan")
                        Dgv_Hpp_Scrap.Rows(i).Cells(CellScp_HPP).Value = Format(.Rows(i).Item("Harga"), "N0")
                        Dgv_Hpp_Scrap.Rows(i).Cells(CellScp_Total).Value = Format(.Rows(i).Item("Harga") * .Rows(i).Item("Jumlah"), "N0")

                        Dgv_Hpp_Scrap.Rows(i).Cells(CellScp_Batch).Value = .Rows(i).Item("Tahap")
                    Next
                End With
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        HitungSelisih()
    End Sub

    Private Sub Btn_Validasi_Click(sender As Object, e As EventArgs) Handles Btn_Validasi.Click
        If DgvDosing.Rows.Count = 0 Then
            MessageBox.Show("data tidak ada.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim pertanyaan As String = MessageBox.Show("Yakin ingin validasi?", "Validasi HPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If pertanyaan = vbNo Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = "select a.Status as status_split,a.Flag_Val_HPP_Produksi,b.Status as status_results, a.Flag_Hasil_Produksi_GI, a.Flag_Hasil_Produksi_GR "
            SQL = SQL & "from Emi_Split_Production_Order a, Emi_Production_Results b "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & Txt_NoSplitProduksi.Text & "' "
            SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Production_Order "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If General_Class.CekNULL(dr("status_split")) <> "" Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data split order ini sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("Flag_Val_HPP_Produksi")) <> "" Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data split order ini sudah divalidasi hpp produksi sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("status_results")) <> "" Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data result produksi ini sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("Flag_Hasil_Produksi_GI")) = "" Or General_Class.CekNULL(dr("Flag_Hasil_Produksi_GR")) = "" Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data result produksi ini belum selesai GI ataupun GR!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
            End Using

            Dim TotalHPP_Packaging As Double = 0
#Region "Pengembalian Packaging"

            ''==============================
            ''=     GET DATA PACKAGING     =
            ''==============================
            'SQL = "select a.Kode_Perusahaan, a.No_Transaksi, a.No_Production_Order, b.Kode_Stock_Owner, b.Kode_Barang, c.Serial_Number, c.Nilai, c.jumlah_pakai, dbo.get_hpp(c.Serial_Number) as harga "
            'SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Packaging_Detail b, Emi_Production_Results_Packaging_Det c "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            'SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Urut = c.No_Urut_Detail "
            'SQL = SQL & "and a.Status is null "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Production_Order = '" & Txt_NoSplitProduksi.Text & "' "
            'SQL = SQL & "order by c.Urut "
            'Using Ds = BindingTrans(SQL)
            '    With Ds.Tables("MyTable")
            '        If .Rows.Count <> 0 Then
            '            For i As Integer = 0 To .Rows.Count - 1

            '                'HANDLE NULL
            '                If General_Class.CekNULL(.Rows(i).Item("jumlah_pakai")) = "" Then
            '                    CloseTrans()
            '                    CloseConn()
            '                    MessageBox.Show("Terjadi Kesahalan : Jumlah Pakai = NULL ", "Validasi HPP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                    Exit Sub
            '                ElseIf General_Class.CekNULL(.Rows(i).Item("Nilai")) = "" Then
            '                    CloseTrans()
            '                    CloseConn()
            '                    MessageBox.Show("Terjadi Kesahalan : Nilai = NULL ", "Validasi HPP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                    Exit Sub
            '                End If

            '                Dim IsiPerBags As Double = 0
            '                '=============================================
            '                '=     CEK APAKAH ORIGINAL BAGS ATAU NON     =
            '                '=============================================
            '                SQL = "select Isi_Per_Bags, Good_Stock, Jumlah_Bags, Jenis_Kemasan from barang where Kode_Perusahaan = '" & KodePerusahaan & "' "
            '                SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
            '                Using Ds1 = BindingTrans(SQL)
            '                    If Ds1.Tables("MyTable").Rows.Count <> 0 Then
            '                        If Ds1.Tables("MyTable").Rows(0).Item("Isi_Per_Bags") = 0 Then
            '                            IsiPerBags = 0
            '                        Else
            '                            IsiPerBags = Val(Ds1.Tables("MyTable").Rows(0).Item("Isi_Per_Bags"))
            '                        End If

            '                    End If
            '                End Using


            '                If (Val(HilangkanTanda(.Rows(i).Item("Nilai"))) - Val(HilangkanTanda(.Rows(i).Item("jumlah_pakai")))) < 0 Then
            '                    CloseTrans()
            '                    CloseConn()
            '                    MessageBox.Show("Ada Masalah Pada Packaging yang di pakai", "Validasi HPP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                    Exit Sub
            '                End If

            '                '==============================
            '                '=     MENGEMBALIKAN STOCK    =
            '                '==============================

            '                Dim SelisihStock As Double = Val(HilangkanTanda(.Rows(i).Item("Nilai"))) - Val(HilangkanTanda(.Rows(i).Item("jumlah_pakai")))
            '                Dim hpp_packaging As Double = .Rows(i).Item("harga")

            '                Dim jumlahBags As Double = 0
            '                If Not IsiPerBags = 0 Then
            '                    jumlahBags = Val(HilangkanTanda(SelisihStock)) / Val(HilangkanTanda(IsiPerBags))
            '                End If

            '                '-== UPDATE BARANG SN ==-'
            '                SQL = "update barang_sn set Jumlah = Jumlah + " & Val(HilangkanTanda(SelisihStock)) & ", "
            '                SQL = SQL & "Jumlah_Bags = Jumlah_Bags + " & Math.Floor(jumlahBags) & " "
            '                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            '                SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' "
            '                SQL = SQL & "and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
            '                SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "' "
            '                ExecuteTrans(SQL)

            '                '-== UPDATE BARANG ==-'
            '                SQL = " update barang set Good_Stock = Good_Stock + " & Val(HilangkanTanda(SelisihStock)) & ", "
            '                SQL = SQL & "Jumlah_Bags = Jumlah_Bags + " & Math.Floor(jumlahBags) & " "
            '                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            '                SQL = SQL & "and Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' "
            '                SQL = SQL & "and Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
            '                ExecuteTrans(SQL)

            '                TotalHPP_Packaging += Math.Round(hpp_packaging * SelisihStock, 0)
            '                '====================================
            '                '=       CEK KESESUAIAN STOCK       =
            '                '====================================
            '                SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
            '                SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
            '                SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
            '                SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
            '                SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
            '                SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
            '                SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & .Rows(i).Item("Kode_Stock_Owner") & "' "
            '                SQL = SQL & "AND a.Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            '                SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
            '                Using Ds2 = BindingTrans(SQL)
            '                    With Ds2.Tables("MyTable")
            '                        If Ds2.Tables("MyTable").Rows.Count <> 0 Then
            '                            If Ds2.Tables("MyTable").Rows(0).Item("good_stock") <> Ds2.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
            '                                CloseTrans()
            '                                CloseConn()
            '                                MessageBox.Show("Stock Tidak Sesuai . . ! !S", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                                Exit Sub
            '                            End If
            '                        Else
            '                            CloseTrans()
            '                            CloseConn()
            '                            MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                            Exit Sub
            '                        End If
            '                    End With
            '                End Using

            '            Next
            '        End If
            '    End With
            'End Using


#End Region

            Dim inisial_faktur_dari As String = ""
            Dim fso As String = ""
            SQL = "Select b.Inisial_Faktur,a.Kode_Stock_Owner from Emi_Split_Production_Order a,Stock_Owner_Gudang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "And a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & Txt_NoSplitProduksi.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    inisial_faktur_dari = Dr("inisial_faktur")
                    fso = Dr("Kode_Stock_Owner")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim akun_persedian_brg_dlm_proses As String = ""
            Dim akun_Loss_production As String = ""
            Dim akun_packaging As String = ""

            Dim lok_packaging As String = ""

            Dim ket_persedian_brg_dlm_proses As String = ""
            Dim ket_loss_production As String = ""
            Dim ket_packaging As String = ""


            SQL = "select Persediaan_Barang_Dalam_Proses, Penyusutan_Barang_Dalam_Proses from stock_owner_gudang "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & fso & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    akun_persedian_brg_dlm_proses = Dr("Persediaan_Barang_Dalam_Proses")
                    ket_persedian_brg_dlm_proses = "Persediaan Barang Dalam Proses "

                    akun_Loss_production = Dr("Penyusutan_Barang_Dalam_Proses")
                    ket_loss_production = "Penyusutan Barang Dalam Proses "
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select top(1) "
            SQL = SQL & "b.Id_Group_Jenis, b.kode_stock_owner, c.akun_persediaan, Kode_Group_Jenis "
            SQL = SQL & "from Emi_Production_Results_Packaging_Det a, Barang b, EMI_Group_Jenis_Akun c, "
            SQL = SQL & "Emi_Production_Results_Packaging_Detail e, EMI_Group_Jenis f, Emi_Production_Results g where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and b.Kode_Perusahaan = f.Kode_Perusahaan and b.Id_Group_Jenis = f.Id_Group_Jenis "
            SQL = SQL & "and f.Kode_Perusahaan = c.Kode_Perusahaan and f.Id_Group_Jenis = c.Id_Group_Jenis "
            SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.No_Transaksi = e.No_Transaksi "
            SQL = SQL & "and a.Kode_Stock_Owner = e.Kode_Stock_Owner and a.Kode_Barang = e.Kode_Barang "
            SQL = SQL & "and a.No_Urut_Detail = e.Urut "
            SQL = SQL & "and e.Kode_Perusahaan = g.Kode_Perusahaan and e.No_Transaksi = g.No_Transaksi "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and g.no_production_order = '" & Txt_NoSplitProduksi.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For h As Integer = 0 To .Rows.Count - 1

                            lok_packaging = .Rows(h).Item("kode_stock_owner")
                            akun_packaging = .Rows(h).Item("akun_persediaan")
                            ket_packaging = "Persediaan " + .Rows(h).Item("Kode_Group_Jenis")

                        Next
                    End If
                End With
            End Using

            Dim Total_loss As Double = 0


            SQL = "Select b.Total_Bahan_Baku-isnull((select sum(x.Hpp_Total) from N_Emi_Production_Results_Detail_Biaya x "
            SQL = SQL & "where x.Kode_Perusahaan=b.Kode_Perusahaan and x.No_Transaksi=b.No_Transaksi "
            SQL = SQL & "and x.jenis='BAHAN' and x.Urut_HPP=b.urut),0) as Selisih "
            SQL = SQL & "From EMI_Production_Results a, Emi_Production_Results_HPP b Where "
            SQL = SQL & "a.kode_Perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi And a.status Is null "
            SQL = SQL & " And a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.no_production_order = '" & Txt_NoSplitProduksi.Text & "' order by proses  "
            Using dss = BindingTrans(SQL)
                For ind = 0 To dss.Tables("MyTable").Rows.Count - 1

                    Dim sisa As Double = dss.Tables("MyTable").Rows(ind).Item("Selisih")

                    Total_loss += sisa
                Next
            End Using


#Region "Jurnal 1"

            'Balikin Nilai Dosing

            Dim Kode_voucher As String = ""
            Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
            Dim pagenumber As Integer = 1

            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
            SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
            SQL = SQL & "'" & Kode_voucher & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
            SQL = SQL & "'" & KodeProyek & "', 'Aktualisasi " & Txt_NoSplitProduksi.Text & "', '', "
            SQL = SQL & "'-', '" & UserID & "')"
            ExecuteTrans(SQL)

            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_Loss_production, 1),
                     Strings.Mid(akun_Loss_production, 2, 1),
                     Strings.Mid(Ganti(akun_Loss_production), 3),
                     KodePerusahaan, KodeProyek, "BUdget Loss Production " & Txt_NoSplitProduksi.Text, Total_loss, "0", pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1


            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persedian_brg_dlm_proses, 1),
                                  Strings.Mid(akun_persedian_brg_dlm_proses, 2, 1),
                                  Strings.Mid(Ganti(akun_persedian_brg_dlm_proses), 3),
                                  KodePerusahaan, KodeProyek, ket_persedian_brg_dlm_proses & Txt_NoSplitProduksi.Text, "0", Total_loss, pagenumber, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("debit") <> Dr("kredit") Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using
#End Region

#Region "Jurnal 2"

            'Dim Kode_voucher2 As String = ""
            'Kode_voucher2 = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
            'Dim pagenumber2 As Integer = 1

            'SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
            'SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
            'SQL = SQL & "'" & Kode_voucher2 & "', "
            'SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            'SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
            'SQL = SQL & "'" & KodeProyek & "', 'Pengeluaran Bahan Baku " & Txt_NoSplitProduksi.Text & "', '', "
            'SQL = SQL & "'-', '" & UserID & "')"
            'ExecuteTrans(SQL)

            ''Insert HPP Total
            'SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(akun_packaging, 1),
            '         Strings.Mid(akun_packaging, 2, 1),
            '         Strings.Mid(Ganti(akun_packaging), 3),
            '         KodePerusahaan, KodeProyek, ket_packaging & Txt_NoSplitProduksi.Text, TotalHPP_Packaging, "0", pagenumber2, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
            'ExecuteTrans(SQL)
            'pagenumber2 = pagenumber2 + 1



            ''Insert Data Bahan dan Packaging yg dipakai
            'SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(akun_persedian_brg_dlm_proses, 1),
            '                      Strings.Mid(akun_persedian_brg_dlm_proses, 2, 1),
            '                      Strings.Mid(Ganti(akun_persedian_brg_dlm_proses), 3),
            '                      KodePerusahaan, KodeProyek, "Persediaan Dalam Proses " & Txt_NoSplitProduksi.Text, "0", TotalHPP_Packaging, pagenumber2, Lokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
            'ExecuteTrans(SQL)
            'pagenumber2 = pagenumber2 + 1

            'SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
            'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & "kode_voucher = '" & Kode_voucher2 & "'"
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        If Dr("debit") <> Dr("kredit") Then
            '            Dr.Close()
            '            CloseTrans()
            '            CloseConn()
            '            MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            Exit Sub
            '        End If
            '    Else
            '        Dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using
#End Region


            SQL = "update Emi_Split_Production_Order set Flag_Val_HPP_Produksi = 'Y',Tgl_Val_HPP_Produksi = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
            SQL = SQL & "Jam_Val_HPP_Produksi = '" & Format(tgl_skg, "HH:mm:ss") & "',UserId_Val_HPP_Produksi = '" & UserID & "', Kode_Voucher='" & Kode_voucher & "' "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & Txt_NoSplitProduksi.Text & "' "
            ExecuteTrans(SQL)



            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data berhasil divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        EMI_Display_Validasi_HPP_Produksi.Button1_Click(Btn_Validasi, e)
        Me.Close()
    End Sub


    Private Sub HitungSelisih()

    End Sub

    Private Sub DgvDosing_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvDosing.CellDoubleClick

        If DgvDosing.Rows.Count = 0 Then Exit Sub

        Get_Isi_Listview(DgvDosing.CurrentRow.Index)

        SD_Detail_Validasi_HPP.noSplit = Txt_NoSplitProduksi.Text
        SD_Detail_Validasi_HPP.Proses = LvBatch

        SD_Detail_Validasi_HPP.Kosong()
        SD_Detail_Validasi_HPP.ShowDialog()



    End Sub

    Private Sub DgvHPP_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvHPP.CellDoubleClick
        'N_EMI_SD_Validasi_HPP_Produksi_Detail_Biaya

        If DgvHPP.Rows.Count = 0 Then Exit Sub

        Get_Isi_Listview_FG(DgvHPP.CurrentRow.Index)

        N_EMI_SD_Validasi_HPP_Produksi_Detail_Biaya.noSplit = Txt_NoSplitProduksi.Text
        N_EMI_SD_Validasi_HPP_Produksi_Detail_Biaya.Proses = lvFGBatch

        N_EMI_SD_Validasi_HPP_Produksi_Detail_Biaya.ShowDialog()

    End Sub


    Private Sub Dgv_Hpp_Scrap_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Hpp_Scrap.CellDoubleClick
        If Dgv_Hpp_Scrap.Rows.Count = 0 Then Exit Sub

        Get_Isi_Listview_Scrap(Dgv_Hpp_Scrap.CurrentRow.Index)

        N_EMI_SD_Validasi_HPP_Produksi_Detail_Biaya_Scrap.noSplit = Txt_NoSplitProduksi.Text
        N_EMI_SD_Validasi_HPP_Produksi_Detail_Biaya_Scrap.Proses = LvSCP_Batch

        N_EMI_SD_Validasi_HPP_Produksi_Detail_Biaya_Scrap.ShowDialog()

    End Sub


    Private Sub Btn_ControlProduksi_Click(sender As Object, e As EventArgs) Handles Btn_ControlProduksi.Click



        EMI_Controlling_Produksi.asal = "VALIDASI HPP"
        EMI_Controlling_Produksi.NoSplit = Txt_NoFaktur.Text

        EMI_Controlling_Produksi.Kosong()
        EMI_Controlling_Produksi.ShowDialog()



    End Sub


End Class
