Imports System.IO.Ports

Public Class EMI_Timbang_Unloading
    Public filterDetailBarang As String = ""
    Public jenisMasuk As String = ""
    Public Txt_Ekspedisi As String = ""
    Dim arrcari As New ArrayList
    Dim arrIdJenisMuatan, arrMetodeTruckScale As New ArrayList
    Dim arrNamaBarang, arrKodeBarang As New ArrayList
    Private isError As Boolean = False
    Dim ItemJumlah As Integer = 7
    Dim ItemJumlahBags As Integer = 9
    Dim ItemJumlahMasuk As Integer = 8
    Dim ItemKdBarang As Integer = 1
    Dim ItemNama As Integer = 2
    Dim ItemNoPO As Integer = 0
    Dim ItemSatuan As Integer = 6
    Dim ItemTglExp As Integer = 3
    Dim ItemTglProd As Integer = 4
    Dim ItemTimbangBeratBags As Integer = 4
    Dim ItemTimbangBeratBarang As Integer = 7
    Dim ItemTimbangFlagTolak As Integer = 8
    Dim ItemTimbangJmlBags As Integer = 3
    Dim ItemTimbangJmlBarang As Integer = 6
    Dim ItemTimbangJmlPallet As Integer = 5
    Dim ItemTimbangKdBarang As Integer = 0
    Dim ItemTimbangNmBarang As Integer = 1
    Dim ItemTimbangSatuan As Integer = 2
    Dim ItemUrutLoading As Integer = 10
    Dim ItemFlagTolak As Integer = 11
    Dim ItemUrutPO As Integer = 5
    Dim Jenis = "Transaksi_Timbang_Kosong"
    Dim LokasiGudangUnloading As String = ""
    Dim LvJumlah As String
    Dim LvJumlahBagMasuk As String
    Dim LvJumlahMasuk As String
    Dim LvKdBarang As String
    Dim LvNama As String
    Dim LvNoPO As String
    Dim LvSatuan As String
    Dim LvTglExp As String
    Dim LvTglProd As String
    Dim LvTimbangBeratBags As String
    Dim LvTimbangBeratBarang As String
    Dim LvTimbangJmlBags As String
    Dim LvTimbangjmlBarang As String
    Dim LvTimbangJmlPallet As String
    Dim LvTimbangKdBarang As String
    Dim LvTimbangNmBarang As String
    Dim LvTimbangSatuan As String
    Dim LvUrutLoading As String
    Dim LvUrutPO As String
    Dim No_Faktur As String = ""
    Public Sub Get_DGVKeluar()

        Try
            OpenConn()

            ListView2.Items.Clear()
            ListView2.View = View.Details

            DgvPO.Rows.Clear()

            Dim id As Integer = 0
            SQL = " select c.no_PO, c.Kode_Barang, d.nama as Nama_Barang, c.Tanggal_Expired, c.Tanggal_Produksi, "
            SQL = SQL & "c.Urut_PO, c.Jumlah, c.Urut_Oto as Urut_Loading, c.satuan, c.Flag_Tolak  "
            SQL = SQL & "from EMI_Timbang_Unloading a, EMI_Timbang_Unloading_PO_Det b, EMI_Pembelian_Loading_Detail c, barang d "
            SQL = SQL & "where a.Kode_Perusahaan =b.Kode_Perusahaan and a.no_faktur=b.no_faktur and "
            SQL = SQL & "b.Kode_Perusahaan=c.Kode_Perusahaan and b.Urut_loading=c.Urut_Oto "
            SQL = SQL & "and c.Kode_Perusahaan=d.Kode_Perusahaan and c.Kode_Barang=d.Kode_Barang and c.Kode_Stock_Owner=d.Kode_Stock_Owner "
            SQL = SQL & "and a.status is null and a.No_faktur='" & Txt_NoFaktur.Text & "' and a.kode_Perusahaan ='" & KodePerusahaan & "' "

            'FILTER QC
            'SQL = SQL & "and (c.Flag_Tolak is null or c.Flag_Tolak <> 'Y')	"

            SQL = SQL & "Order By d.nama "
            Using dr = OpenTrans(SQL)

                Do While dr.Read

                    DgvPO.Rows.Add(1)
                    DgvPO.Rows(id).Cells(ItemNoPO).Value = dr("No_Po")
                    DgvPO.Rows(id).Cells(ItemKdBarang).Value = dr("Kode_Barang")
                    DgvPO.Rows(id).Cells(ItemNama).Value = dr("Nama_Barang")
                    DgvPO.Rows(id).Cells(ItemTglExp).Value = Format(dr("Tanggal_Expired"), "dd MMM yyyy")
                    DgvPO.Rows(id).Cells(ItemTglProd).Value = Format(dr("tanggal_Produksi"), "dd MMM yyyy")
                    DgvPO.Rows(id).Cells(ItemUrutPO).Value = dr("Urut_PO")
                    DgvPO.Rows(id).Cells(ItemSatuan).Value = dr("Satuan")
                    DgvPO.Rows(id).Cells(ItemJumlah).Value = Format(dr("Jumlah"), "N2")
                    DgvPO.Rows(id).Cells(ItemJumlahMasuk).Value = 0
                    DgvPO.Rows(id).Cells(ItemJumlahBags).Value = 0
                    DgvPO.Rows(id).Cells(ItemUrutLoading).Value = dr("Urut_Loading")
                    DgvPO.Rows(id).Cells(ItemFlagTolak).Value = General_Class.CekNULL(dr("Flag_Tolak"))

                    If General_Class.CekNULL(dr("Flag_Tolak")) = "" Then
                        DgvPO.Rows(id).Cells(ItemJumlahMasuk).ReadOnly = True
                        DgvPO.Rows(id).Cells(ItemJumlahBags).ReadOnly = True
                    End If

                    id += 1
                Loop
            End Using

            DgvTimbang.Rows.Clear()

            SQL = "select a.Kode_Perusahaan, a.no_faktur, b.Kode_Barang, c.nama, b.Satuan, b.Satuan_Per_Bag, "
            SQL = SQL & "b.Jumlah_Per_Bag,c.Berat_Bags, c.Satuan_Berat_Bags "

            SQL = SQL & ",isnull((select sum(Jumlah_Bags) from emi_barang_masuk_perpallet x where "
            SQL = SQL & "x.Kode_Perusahaan=a.Kode_Perusahaan and x.No_Pembelian_Loading=a.No_Faktur and x.status is null "
            SQL = SQL & "and x.Kode_Barang=b.Kode_Barang),0) as Jumlah_Bags "

            SQL = SQL & ",isnull((select sum(Jumlah) from emi_barang_masuk_perpallet x where "
            SQL = SQL & "x.Kode_Perusahaan=a.Kode_Perusahaan and x.No_Pembelian_Loading=a.No_Faktur and x.status is null "
            SQL = SQL & "and x.Kode_Barang=b.Kode_Barang),0) as Jumlah_Barang "

            SQL = SQL & ",isnull((select sum(Jumlah_Pallet) from emi_barang_masuk_perpallet x where "
            SQL = SQL & "x.Kode_Perusahaan=a.Kode_Perusahaan and x.No_Pembelian_Loading=a.No_Faktur and x.status is null "
            SQL = SQL & "and x.Kode_Barang=b.Kode_Barang),0) as Jumlah_Pallet, b.Flag_Tolak "

            SQL = SQL & "from EMI_Pembelian_Loading a, EMI_Pembelian_Loading_Detail b, barang c where "
            SQL = SQL & "a.No_Faktur=b.No_Faktur and a.Kode_Perusahaan=b.Kode_Perusahaan and a.status is null "
            SQL = SQL & "and b.Kode_Perusahaan=c.Kode_Perusahaan and b.Kode_Barang=c.Kode_Barang and b.Kode_Stock_Owner=c.Kode_Stock_Owner "
            SQL = SQL & "and a.kode_Perusahaan ='" & KodePerusahaan & "' and a.No_faktur='" & TxtNo_Loading.Text & "' "

            'FILTER QC
            'SQL = SQL & "and (b.Flag_Tolak is null or b.Flag_Tolak <> 'Y') "

            SQL = SQL & "and b.kode_Barang in( "

            SQL = SQL & "Select distinct b.Kode_Barang from EMI_Timbang_Unloading a, EMI_Timbang_Unloading_PO_Det b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur And a.status Is null "
            SQL = SQL & "And a.Kode_Perusahaan='" & KodePerusahaan & "' and a.No_Faktur='" & Txt_NoFaktur.Text & "' "

            SQL = SQL & ") "

            SQL = SQL & "group by a.Kode_Perusahaan, a.no_faktur,b.Kode_Barang, c.nama, b.Satuan, b.Satuan_Per_Bag, "
            SQL = SQL & "b.Jumlah_Per_Bag,c.Berat_Bags, c.Satuan_Berat_Bags, b.Flag_Tolak "

            SQL = SQL & "Order By c.nama "
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For id2 = 0 To .Rows.Count - 1

                        Dim berat_bags As Double
                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(id2).Item("Kode_Barang") & "',"
                        SQL = SQL & "'" & .Rows(id2).Item("Satuan_Berat_Bags") & "','" & CmbSatuan.Text & "',"
                        SQL = SQL & "" & .Rows(id2).Item("Jumlah_Bags") * .Rows(id2).Item("Berat_Bags") & ") as Hasil "
                        Using dr3 = OpenTrans(SQL)
                            If dr3.Read Then
                                If General_Class.CekNULL(dr3("Hasil")) <> "" Then
                                    berat_bags = dr3("Hasil")
                                Else
                                    MessageBox.Show("Satuan " & .Rows(id2).Item("Satuan_Berat_Bags") & " Ke " & CmbSatuan.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If
                        End Using

                        Dim berat_barang As Double
                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(id2).Item("Kode_Barang") & "',"
                        SQL = SQL & "'" & .Rows(id2).Item("Satuan") & "','" & CmbSatuan.Text & "',"
                        SQL = SQL & "" & .Rows(id2).Item("Jumlah_Barang") & ") as Hasil "
                        Using dr3 = OpenTrans(SQL)
                            If dr3.Read Then
                                If General_Class.CekNULL(dr3("Hasil")) <> "" Then
                                    berat_barang = dr3("Hasil")
                                Else
                                    MessageBox.Show("Satuan " & .Rows(id2).Item("Satuan_Berat_Bags") & " Ke " & CmbSatuan.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If
                        End Using

                        DgvTimbang.Rows.Add(1)
                        DgvTimbang.Rows(id2).Cells(ItemTimbangKdBarang).Value = .Rows(id2).Item("Kode_Barang")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangNmBarang).Value = .Rows(id2).Item("Nama")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangSatuan).Value = .Rows(id2).Item("Satuan")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangJmlBags).Value = Format(.Rows(id2).Item("Jumlah_Bags"), "N0")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangBeratBags).Value = Format(berat_bags, "N2")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangJmlPallet).Value = Format(.Rows(id2).Item("Jumlah_Pallet"), "N0")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangJmlBarang).Value = Format(.Rows(id2).Item("Jumlah_Barang"), "N2")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangBeratBarang).Value = Format(berat_barang, "N2")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangFlagTolak).Value = .Rows(id2).Item("Flag_Tolak")

                    Next
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        get_jumlahPO_Otomatis()

        getSumOfBerat()
    End Sub

    Public Sub Get_DGVMasuk()

        Try
            OpenConn()

            ListView2.Items.Clear()
            ListView2.View = View.Details

            DgvPO.Rows.Clear()

            Dim id As Integer = 0
            SQL = "select a.kode_Perusahaan, a.no_faktur, a.kode_supplier, C.nama as Nama_Barang, a.Lokasi, a.No_SJ, a.No_Plat, "
            SQL = SQL & "a.Driver, b.No_Po, B.Urut_PO, B.Kode_Stock_Owner, b.Kode_Barang, D.nama, b.tanggal_Produksi, b.Tanggal_Expired, "
            SQL = SQL & "b.Jumlah, b.Satuan ,b.urut_Oto as Urut_Loading, b.satuan, b.Flag_Tolak "
            SQL = SQL & "from EMI_Pembelian_Loading a, EMI_Pembelian_Loading_Detail b, barang c, Suppliers d "
            SQL = SQL & "where a.kode_Perusahaan=b.kode_Perusahaan and a.no_faktur=b.no_faktur and a.status is null and "
            SQL = SQL & "b.kode_Barang=c.kode_Barang and b.kode_stock_Owner=c.Kode_Stock_Owner and b.kode_Perusahaan=c.kode_Perusahaan "
            SQL = SQL & "and a.kode_Perusahaan=d.Kode_Perusahaan and a.kode_Supplier=d.Kode_Supplier "
            SQL = SQL & "and a.kode_Perusahaan ='" & KodePerusahaan & "' and b.Flag_Timbang_Masuk is null and a.No_faktur='" & TxtNo_Loading.Text & "' "

            'FILTER QC
            'SQL = SQL & "and (b.Flag_Tolak is null or b.Flag_Tolak <> 'Y') "
            If CmbBarang.SelectedIndex <> -1 Then
                SQL = SQL & "and b.kode_Barang='" & arrKodeBarang.Item(CmbBarang.SelectedIndex) & "'"
            End If
            SQL = SQL & "Order By d.nama "
            Using dr = OpenTrans(SQL)

                Do While dr.Read

                    DgvPO.Rows.Add(1)
                    DgvPO.Rows(id).Cells(ItemNoPO).Value = dr("No_Po")
                    DgvPO.Rows(id).Cells(ItemKdBarang).Value = dr("Kode_Barang")
                    DgvPO.Rows(id).Cells(ItemNama).Value = dr("Nama_Barang")
                    DgvPO.Rows(id).Cells(ItemTglExp).Value = Format(dr("Tanggal_Expired"), "dd MMM yyyy")
                    DgvPO.Rows(id).Cells(ItemTglProd).Value = Format(dr("tanggal_Produksi"), "dd MMM yyyy")
                    DgvPO.Rows(id).Cells(ItemUrutPO).Value = dr("Urut_PO")
                    DgvPO.Rows(id).Cells(ItemSatuan).Value = dr("Satuan")
                    DgvPO.Rows(id).Cells(ItemJumlah).Value = Format(dr("Jumlah"), "N2")
                    DgvPO.Rows(id).Cells(ItemJumlahMasuk).Value = 0
                    DgvPO.Rows(id).Cells(ItemUrutLoading).Value = dr("Urut_Loading")
                    DgvPO.Rows(id).Cells(ItemFlagTolak).Value = General_Class.CekNULL(dr("Flag_Tolak"))

                    If General_Class.CekNULL(dr("Flag_Tolak")) = "" Then
                        DgvPO.Rows(id).Cells(ItemJumlahMasuk).ReadOnly = True
                    End If

                    id += 1
                Loop
            End Using

            DgvTimbang.Rows.Clear()

            SQL = "select a.Kode_Perusahaan, a.no_faktur, b.Kode_Barang, c.nama, b.Satuan, b.Satuan_Per_Bag, "
            SQL = SQL & "b.Jumlah_Per_Bag,c.Berat_Bags, c.Satuan_Berat_Bags "

            SQL = SQL & ",isnull((select sum(Jumlah_Bags) from emi_barang_masuk_perpallet x where "
            SQL = SQL & "x.Kode_Perusahaan=a.Kode_Perusahaan and x.No_Pembelian_Loading=a.No_Faktur and x.status is null "
            SQL = SQL & "and x.Kode_Barang=b.Kode_Barang),0) as Jumlah_Bags "

            SQL = SQL & ",isnull((select sum(Jumlah) from emi_barang_masuk_perpallet x where "
            SQL = SQL & "x.Kode_Perusahaan=a.Kode_Perusahaan and x.No_Pembelian_Loading=a.No_Faktur and x.status is null "
            SQL = SQL & "and x.Kode_Barang=b.Kode_Barang),0) as Jumlah_Barang "

            SQL = SQL & ",isnull((select sum(Jumlah_Pallet) from emi_barang_masuk_perpallet x where "
            SQL = SQL & "x.Kode_Perusahaan=a.Kode_Perusahaan and x.No_Pembelian_Loading=a.No_Faktur and x.status is null "
            SQL = SQL & "and x.Kode_Barang=b.Kode_Barang),0) as Jumlah_Pallet, b.Flag_Tolak "

            SQL = SQL & "from EMI_Pembelian_Loading a, EMI_Pembelian_Loading_Detail b, barang c where "
            SQL = SQL & "a.No_Faktur=b.No_Faktur and a.Kode_Perusahaan=b.Kode_Perusahaan and a.status is null "
            SQL = SQL & "and b.Kode_Perusahaan=c.Kode_Perusahaan and b.Kode_Barang=c.Kode_Barang and b.Kode_Stock_Owner=c.Kode_Stock_Owner "
            SQL = SQL & "and a.kode_Perusahaan ='" & KodePerusahaan & "' and b.Flag_Timbang_Masuk is null and a.No_faktur='" & TxtNo_Loading.Text & "' "
            'FILTER QC
            'SQL = SQL & "and (b.Flag_Tolak is null or b.Flag_Tolak <> 'Y') "

            If CmbBarang.SelectedIndex <> -1 Then
                SQL = SQL & "and b.kode_Barang='" & arrKodeBarang.Item(CmbBarang.SelectedIndex) & "'"
            End If

            SQL = SQL & "group by a.Kode_Perusahaan, a.no_faktur,b.Kode_Barang, c.nama, b.Satuan, b.Satuan_Per_Bag, "
            SQL = SQL & "b.Jumlah_Per_Bag,c.Berat_Bags, c.Satuan_Berat_Bags, b.Flag_Tolak "

            SQL = SQL & "Order By c.nama "
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For id2 = 0 To .Rows.Count - 1

                        Dim berat_bags As Double
                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(id2).Item("Kode_Barang") & "',"
                        SQL = SQL & "'" & .Rows(id2).Item("Satuan_Berat_Bags") & "','" & CmbSatuan.Text & "',"
                        SQL = SQL & "" & .Rows(id2).Item("Jumlah_Bags") * .Rows(id2).Item("Berat_Bags") & ") as Hasil "
                        Using dr3 = OpenTrans(SQL)
                            If dr3.Read Then
                                If General_Class.CekNULL(dr3("Hasil")) <> "" Then
                                    berat_bags = dr3("Hasil")
                                Else
                                    MessageBox.Show("Satuan " & .Rows(id2).Item("Satuan_Berat_Bags") & " Ke " & CmbSatuan.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If
                        End Using

                        Dim berat_barang As Double
                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(id2).Item("Kode_Barang") & "',"
                        SQL = SQL & "'" & .Rows(id2).Item("Satuan") & "','" & CmbSatuan.Text & "',"
                        SQL = SQL & "" & .Rows(id2).Item("Jumlah_Barang") & ") as Hasil "
                        Using dr3 = OpenTrans(SQL)
                            If dr3.Read Then
                                If General_Class.CekNULL(dr3("Hasil")) <> "" Then
                                    berat_barang = dr3("Hasil")
                                Else
                                    MessageBox.Show("Satuan " & .Rows(id2).Item("Satuan_Berat_Bags") & " Ke " & CmbSatuan.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If
                        End Using

                        DgvTimbang.Rows.Add(1)
                        DgvTimbang.Rows(id2).Cells(ItemTimbangKdBarang).Value = .Rows(id2).Item("Kode_Barang")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangNmBarang).Value = .Rows(id2).Item("Nama")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangSatuan).Value = .Rows(id2).Item("Satuan")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangJmlBags).Value = Format(.Rows(id2).Item("Jumlah_Bags"), "N0")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangBeratBags).Value = Format(berat_bags, "N2")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangJmlPallet).Value = Format(.Rows(id2).Item("Jumlah_Pallet"), "N0")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangJmlBarang).Value = Format(.Rows(id2).Item("Jumlah_Barang"), "N2")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangBeratBarang).Value = Format(berat_barang, "N2")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangFlagTolak).Value = .Rows(id2).Item("Flag_Tolak")

                    Next
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'Data PO berdasarkan Supplier
    Public Sub get_jumlahPO_Otomatis()
        For index = 0 To DgvTimbang.Rows.Count - 1
            Get_Isi_DataGridViewTimbang(index)

            'Ambil Data Timbang Dulu
            Dim jumlah As Double = Val(HilangkanTanda(LvTimbangjmlBarang))
            Dim bags As Double = Val(HilangkanTanda(LvTimbangJmlBags))

            Dim jumlah_timbang As Double = Val(HilangkanTanda(LvTimbangjmlBarang))

            'INI UNTUK ISI JUMLAH PO
            Dim id_terakhir As Double
            'Isi Tabel PO sesuai Data Timbang
            For index2 = 0 To DgvPO.Rows.Count - 1
                Get_Isi_DataGridView(index2)

                'Ambil data baran yg sama
                If LvTimbangKdBarang = LvKdBarang Then

                    'Data Masuk Semua ny di reset dahulu
                    Dim jumlah_pakai As Double = 0

                    If jumlah_timbang > Val(HilangkanTanda(LvJumlah)) Then
                        jumlah_pakai = Val(HilangkanTanda(LvJumlah))
                    Else
                        jumlah_pakai = jumlah_timbang
                    End If

                    'update Jumlah PO sesuai Urutan
                    DgvPO.Rows(index2).Cells(ItemJumlahMasuk).Value = jumlah_pakai

                    jumlah_timbang -= jumlah_pakai

                    'Ambil ID Terkahir yg ada Barang Tersebut
                    id_terakhir = index2
                End If

                'klo data timbang 0, di selesaikan
                If jumlah_timbang = 0 Then
                    Exit For
                End If

            Next

            If jumlah_timbang <> 0 Then
                Get_Isi_DataGridView(id_terakhir)
                DgvPO.Rows(id_terakhir).Cells(ItemJumlahMasuk).Value = Val(HilangkanTanda(LvJumlahMasuk)) + jumlah_timbang

                jumlah_timbang -= jumlah_timbang
            End If

            If jumlah_timbang <> 0 Then
                MessageBox.Show("Terjadi Kesalahan . . ! ! !")
                Exit Sub
            End If

            'INI UNTUK ISI JUMLAH BAGS
            For index2 = 0 To DgvPO.Rows.Count - 1
                Get_Isi_DataGridView(index2)

                If LvTimbangKdBarang = LvKdBarang Then

                    Dim JumlahFinalBags As Double = 0
                    Dim BeratPerBags As Double = jumlah / bags
                    JumlahFinalBags = Val(HilangkanTanda(LvJumlahMasuk)) / BeratPerBags

                    DgvPO.Rows(index2).Cells(ItemJumlahBags).Value = Math.Round(JumlahFinalBags)

                End If

            Next

        Next

    End Sub

    Public Sub Hitung_Netto()

        If Txt_Timbang2.Text.Trim <> "" Then

            Txt_Netto.Text = Format(Math.Max(0, Val(HilangkanTanda(Txt_Timbang1.Text)) - Val(HilangkanTanda(Txt_Timbang2.Text))), "N0")

            If CmbJenisMuatan.SelectedIndex <> -1 And DgvTimbang.RowCount <> 0 Then
                Dim indexSelected As Integer = CmbJenisMuatan.SelectedIndex
                Dim metodeTruckScale As String = arrMetodeTruckScale(indexSelected).ToString.ToUpper.Trim

                If metodeTruckScale = "TRUCK SCALE" Then
                    DgvTimbang.Rows(0).Cells(ItemTimbangJmlBarang).Value = Val(HilangkanTanda(Txt_Netto.Text))
                    DgvTimbang.Rows(0).Cells(ItemTimbangBeratBarang).Value = Val(HilangkanTanda(Txt_Netto.Text))
                End If

                getSumOfBerat()
            End If

        End If
        get_jumlahPO_Otomatis()

    End Sub

    Public Sub kosong()

        ListView2.Items.Clear()
        TxtNoSJ.Text = ""
        Txt_Timbangan.Text = "99999"
        Txt_NoFaktur.Text = ""
        Txt_Supplier.Text = ""
        Txt_Supir.Text = ""
        Txt_Ekspedisi = ""
        Txt_Supir.Text = ""
        Txt_PlatNomor.Text = ""
        Txt_Timbang1.Text = ""
        Txt_Timbang2.Text = ""
        Txt_Netto.Text = ""
        Txt_Timbang1.Enabled = True
        Txt_Timbang2.Enabled = True
        Txt_Netto.Enabled = False
        CmbBarang.Text = ""
        CmbJenisMuatan.Text = ""

        Btn_Simpan.Tag = "&SimpanBruto"
        Btn_Simpan.Text = "&Simpan Bruto"

        Try
            OpenConn()

            CmbSatuan.Items.Clear()
            SQL = "select satuan from emi_satuan where kode_Perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSatuan.Items.Add(dr("satuan"))
                Loop
            End Using

            Dim satuan_timbang As String = ""
            SQL = "select Satuan_Timbang from init where kode_Perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    satuan_timbang = dr("Satuan_Timbang")
                End If
            End Using

            CmbSatuan.Text = satuan_timbang
            CmbSatuan.Enabled = False
            get_no_faktur()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        '''Tampil_Kamera()
        'Popup_Timbang.Show()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        '''If Val(Txt_Timbangan.Text) = 0 Then
        '''    MessageBox.Show("TOLONG DIBUATIN LANGUAGE timbangan masih kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '''    Exit Sub
        '''ElseIf StreamPlayerControl1.IsPlaying = False Then
        '''    MessageBox.Show("TOLONG DIBUATIN LANGUAGE terjadi kesalahan pada kamera", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '''    Exit Sub
        '''ElseIf StreamPlayerControl2.IsPlaying = False Then
        '''    MessageBox.Show("TOLONG DIBUATIN LANGUAGE terjadi kesalahan pada kamera", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '''    Exit Sub
        '''End If

        get_jam()

        If CmbJenisMuatan.SelectedIndex = -1 Then
            MessageBox.Show("jenis Muatan Harus di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbJenisMuatan.Focus()
            Exit Sub
        ElseIf Txt_Supplier.Text.Trim.Length = 0 Then
            MessageBox.Show("Supplier Tidak ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim indexSelected As Integer = CmbJenisMuatan.SelectedIndex
        Dim metodeTruckScale As String = arrMetodeTruckScale(indexSelected).ToString.ToUpper.Trim

        Dim Init_Akhir As String = ""
        If jenisMasuk = "MASUK" Then
            Init_Akhir = "_BR"
        Else
            Init_Akhir = "_TR"
        End If

        '''Dim Image_1 As Bitmap = StreamPlayerControl1.GetCurrentFrame()
        '''Dim ImageCompress_1 As New Bitmap(Image_1, 640, 640) ' 1024, 768)

        '''Dim Image_2 As Bitmap = StreamPlayerControl2.GetCurrentFrame()
        '''Dim ImageCompress_2 As New Bitmap(Image_2, 640, 640)

        '''Dim TempFolder As String = System.IO.Path.GetTempPath()
        '''

        '''Dim BlobName_1 As String = Nama_File_1
        '''Dim FilePath_1 As String = TempFolder & Nama_File_1

        '''Dim BlobName_2 As String = Nama_File_2
        '''Dim FilePath_2 As String = TempFolder & Nama_File_2

        '''Dim Container As BlobContainerClient = New BlobContainerClient(ConnectionStringAzure, ContainerName)

        '''ImageCompress_1.Save(FilePath_1) 'simpan ke lokal
        '''ImageCompress_2.Save(FilePath_2) 'simpan ke lokal

        'JANGAN LUPA DI UNCOMMENT
        'Dim Nama_File_1 As String = Txt_NoFaktur.Text.Trim & "_" & Format(CDate(FormDevleopment.ToolStripStatusLabel3.Text), "yyyyMMddHHmmss") & Init_Akhir & "_A.jpg"
        'Dim Nama_File_2 As String = Txt_NoFaktur.Text.Trim & "_" & Format(CDate(FormDevleopment.ToolStripStatusLabel3.Text), "yyyyMMddHHmmss") & Init_Akhir & "_B.jpg"

        Dim Nama_File_1 As String = Txt_NoFaktur.Text.Trim & "_" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "yyyyMMddHHmmss") & Init_Akhir & "_A.jpg"
        Dim Nama_File_2 As String = Txt_NoFaktur.Text.Trim & "_" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "yyyyMMddHHmmss") & Init_Akhir & "_B.jpg"

        Try

            If jenisMasuk = "MASUK" Then

                If metodeTruckScale = "TRUCK SCALE" Then
                    If CmbBarang.SelectedIndex = -1 Then
                        MessageBox.Show("Barang Harus di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        CmbBarang.Focus()
                        Exit Sub
                    End If
                End If

                OpenConn()

                get_no_faktur()
                get_jam()

                No_Faktur = Txt_NoFaktur.Text

                Cmd.Transaction = Cn.BeginTransaction

                SQL = "Insert into EMI_Timbang_Unloading (Kode_Perusahaan, "
                SQL = SQL & "No_Faktur, No_Loading, Timbang_Masuk, Tgl_Timbang_Masuk, Jam_Timbang_Masuk, Foto_Timbang_Masuk_1, "
                SQL = SQL & "Foto_Timbang_Masuk_2, id_jenis_muatan, Satuan) values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "','" & TxtNo_Loading.Text & "', "
                SQL = SQL & "'" & HilangkanTanda(Txt_Timbang1.Text) & "', '" & Format(DTP_1.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(CDate(DTP_Bruto.Value), "HH:mm:ss") & "', '" & Nama_File_1 & "', '" & Nama_File_2 & "', '" & arrIdJenisMuatan.Item(CmbJenisMuatan.SelectedIndex) & "', '" & CmbSatuan.Text & "')"
                ExecuteTrans(SQL)

                '''SIMPAN Unloading PO
                Dim noFaktur As String = ""
                Dim noSuratJalan As String = ""
                Dim noPO As String = ""

                'For i As Integer = 0 To ListView2.Items.Count - 1
                For i As Integer = 0 To DgvPO.RowCount - 1
                    Get_Isi_DataGridView(i)

                    If LvNoPO <> noPO Then
                        SQL = "Insert into EMI_Timbang_Unloading_PO ("
                        SQL = SQL & "Kode_Perusahaan, No_Faktur, No_PO)"
                        SQL = SQL & "Values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "', "
                        SQL = SQL & "'" & LvNoPO & "') "
                        ExecuteTrans(SQL)

                        noPO = LvNoPO

                    End If

                    SQL = "Insert into EMI_Timbang_Unloading_PO_Det ("
                    SQL = SQL & "Kode_Perusahaan, No_Faktur, No_PO, Urut_Loading, Kode_Barang, Kode_Stock_owner)"
                    SQL = SQL & "Values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "', "
                    SQL = SQL & "'" & LvNoPO & "', '" & LvUrutLoading & "', '" & LvKdBarang & "', '" & LokasiGudangUnloading & "') "
                    ExecuteTrans(SQL)

                    SQL = "update EMI_Pembelian_Loading_Detail set flag_timbang_masuk='Y' where No_Faktur='" & TxtNo_Loading.Text & "' "
                    SQL = SQL & "and urut_oto='" & LvUrutLoading & "' and kode_barang='" & LvKdBarang & "'"
                    ExecuteTrans(SQL)

                Next

                'FLAGING BRUTO
                SQL = "update EMI_Pembelian_Loading set "
                SQL = SQL & "ID_Jenis_Muatan=" & arrIdJenisMuatan.Item(CmbJenisMuatan.SelectedIndex) & ", "
                SQL = SQL & "flag_proses_loading = 'Y' "
                SQL = SQL & "where No_faktur='" & TxtNo_Loading.Text & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
                ExecuteTrans(SQL)

                SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading_detail where "
                SQL = SQL & "No_faktur='" & TxtNo_Loading.Text & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and Flag_Timbang_masuk is null "
                Using dr = OpenTrans(SQL)
                    If Not dr.Read Then
                        dr.Close()
                        SQL = "update EMI_Pembelian_Loading set Flag_Timbang ='Y' "
                        SQL = SQL & "where No_faktur='" & TxtNo_Loading.Text & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
                        ExecuteTrans(SQL)

                    End If
                End Using

                '''Dim Blob_1 As BlobClient = Container.GetBlobClient(BlobName_1)
                '''Blob_1.Upload(FilePath_1, New BlobHttpHeaders With {.ContentType = "image/jpeg"})

                '''Dim Blob_2 As BlobClient = Container.GetBlobClient(BlobName_2)
                '''Blob_2.Upload(FilePath_2, New BlobHttpHeaders With {.ContentType = "image/jpeg"})

                Cmd.Transaction.Commit()
                CloseConn()
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                kosong()
                EMI_Display_Timbang.kosong()
                Me.Close()
                'Exit Sub
            ElseIf jenisMasuk = "KELUAR" Then

                'If  Then

                'End If
                getSumOfBerat()
                Dim totalJumlah As Double = 0

                For i As Integer = 0 To DgvTimbang.RowCount - 1
                    Get_Isi_DataGridViewTimbang(i)

                    Dim nilai As Double = Val(HilangkanTanda(LvTimbangjmlBarang))
                    Dim totalPO As Double = 0
                    Dim kd_barang = LvTimbangKdBarang

                    For index = 0 To DgvPO.RowCount - 1
                        Get_Isi_DataGridView(index)

                        If kd_barang = LvKdBarang Then
                            totalPO = totalPO + Val(HilangkanTanda(LvJumlahMasuk))
                        End If

                    Next

                    If nilai <> totalPO Then
                        MessageBox.Show(LvTimbangNmBarang & " Berbeda dengan PO", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Next

                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction

                No_Faktur = Txt_NoFaktur.Text

                Dim inisial_faktur_dari As String = ""
                Dim lokasi_Barang As String = ""

                SQL = "select kode_stock_owner from EMI_Barang_Masuk_Perpallet a where "
                SQL = SQL & "Kode_Perusahaan='" & KodePerusahaan & "' and "
                SQL = SQL & "No_Pembelian_Loading='" & TxtNo_Loading.Text & "' and status is null and Flag_Timbang_Keluar is null "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        lokasi_Barang = dr("kode_stock_owner")
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Tidak ditemukan . . ! !")
                        Exit Sub
                    End If
                End Using

                SQL = "select inisial_faktur from stock_owner_gudang "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & lokasi_Barang & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        'akun_persediaan_dari = Dr("persediaan")
                        inisial_faktur_dari = Dr("inisial_faktur")
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "Update EMI_Timbang_Unloading "
                SQL = SQL & "Set Timbang_Keluar = '" & HilangkanTanda(Txt_Timbang2.Text) & "', "
                SQL = SQL & "Tgl_Timbang_Keluar = '" & Format(DTP_Tara.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "Jam_Timbang_Keluar = '" & Format(CDate(DTP_Tara.Value), "HH:mm:ss") & "', "
                SQL = SQL & "User_Timbang_Keluar = '" & UserID & "', "
                SQL = SQL & "Foto_Timbang_Keluar_1 = '" & Nama_File_1 & "', "
                SQL = SQL & "Foto_Timbang_Keluar_2 = '" & Nama_File_2 & "', "
                SQL = SQL & "Netto = '" & HilangkanTanda(Txt_Netto.Text) & "', "
                SQL = SQL & "flag_Selesai = 'Y', "
                SQL = SQL & "Jumlah_Bags = " & HilangkanTanda(Tot_Bags.Text) & " "
                SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and No_Faktur = '" & Txt_NoFaktur.Text & "' "
                ExecuteTrans(SQL)

                Dim flag_import As String = ""
                Dim flag_HPP As String = ""
                SQL = "Select Flag_Import, Flag_Import_HPP from EMI_Pembelian_Loading "
                SQL = SQL & "where No_Faktur='" & TxtNo_Loading.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        flag_HPP = General_Class.CekNULL(Dr("Flag_Import_HPP"))
                        flag_import = General_Class.CekNULL(Dr("Flag_Import"))
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                'For i As Integer = 0 To ListView2.Items.Count - 1
                Dim sat_brg As String = ""
                Dim Total_HPP_PO As Double = 0
                For i As Integer = 0 To DgvPO.RowCount - 1

                    Get_Isi_DataGridView(i)

                    Dim Harga As Double = 0
                    Dim PPN As Double = 0

                    Dim flag_refraksi As String = ""
                    SQL = "Select (case when a.flag_refraksi Is null then a.hpp_satuan_display else a.Harga_Refraksi end) as harga, c.PPN, a.Flag_Permintaan_Refraksi, a.flag_refraksi "
                    SQL = SQL & "From EMI_Pembelian_Loading_Detail a, EMI_Pembelian_PO_Detail b, EMI_Pembelian_PO c Where "
                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.Urut_PO = b.No_Urut And "
                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.No_Faktur = c.No_Faktur and c.status is null And "
                    SQL = SQL & "Urut_Oto = '" & LvUrutLoading & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then

                            If General_Class.CekNULL(dr("Flag_Permintaan_Refraksi")) = "Y" Then
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terdapat Data yang Harus di Refraksi . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If

                            flag_refraksi = General_Class.CekNULL(dr("flag_refraksi"))

                            If flag_import = "Y" Then

                                If flag_HPP = "" Then
                                    Harga = 0
                                    PPN = 0
                                Else
                                    Harga = dr("Harga")
                                    PPN = dr("PPN")
                                End If
                            Else
                                Harga = If(General_Class.CekNULL(dr("Harga")) = "", 0, General_Class.CekNULL(dr("Harga")))
                                PPN = General_Class.CekNULL(dr("PPN"))
                            End If
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("PO Tidak ditemukan . . ! !")
                            Exit Sub
                        End If
                    End Using

                    Dim jmlhMasuk As Double = Val(LvJumlahMasuk)
                    Dim Satuan As String = LvSatuan

                    Dim jumlah_masuk_Barang As Double = 0
                    Dim Satuan_Barang As String = ""

                    SQL = "select distinct Satuan from Barang where "
                    SQL = SQL & "Kode_Barang='" & LvKdBarang & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
                    Using dr2 = OpenTrans(SQL)
                        If dr2.Read Then
                            Satuan_Barang = dr2("Satuan")
                        Else
                            dr2.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang Tidak ditemukan . . ! !")
                            Exit Sub
                        End If
                    End Using
                    sat_brg = Satuan_Barang

                    'UBAH KE SATUAN PO
                    SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & LvKdBarang & "',"
                    SQL = SQL & "'" & Satuan & "','" & Satuan_Barang & "',"
                    SQL = SQL & "" & jmlhMasuk & ") as Hasil "
                    Using dr3 = OpenTrans(SQL)
                        If dr3.Read Then
                            If General_Class.CekNULL(dr3("Hasil")) <> "" Then
                                jumlah_masuk_Barang = dr3("Hasil")
                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Satuan " & Satuan & " Ke " & Satuan_Barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End If
                    End Using

                    If flag_refraksi = "" Then
                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','UANG','" & LvKdBarang & "',"
                        SQL = SQL & "'" & Satuan & "','" & Satuan_Barang & "',"
                        SQL = SQL & "" & Harga & ") as Hasil "
                        Using dr3 = OpenTrans(SQL)
                            If dr3.Read Then
                                If General_Class.CekNULL(dr3("Hasil")) <> "" Then
                                    Harga = dr3("Hasil")
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Satuan " & Satuan & " Ke " & Satuan_Barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If
                        End Using
                    End If

                    Dim TotalHPP As Double = Math.Round(jumlah_masuk_Barang * Harga)
                    Dim Nilai_PPN As Double = Math.Round(TotalHPP * PPN / 100)
                    Total_HPP_PO += TotalHPP

                    'UPDATE PEMBELIAN LOADING DETAIL

                    SQL = "update EMI_Pembelian_Loading_Detail set Jumlah_Masuk = Jumlah_Masuk + " & jumlah_masuk_Barang & " "
                    SQL = SQL & ", flag_timbang_keluar='Y'"
                    SQL = SQL & "where No_Faktur='" & TxtNo_Loading.Text & "' and Urut_Oto='" & LvUrutLoading & "'"
                    ExecuteTrans(SQL)

                    SQL = "update EMI_Timbang_Unloading_PO_Det set "
                    SQL = SQL & "jumlah ='" & jmlhMasuk & "', satuan ='" & Satuan & "', "
                    SQL = SQL & "nilai_barang ='" & jumlah_masuk_Barang & "', "
                    SQL = SQL & "Satuan_Barang ='" & Satuan_Barang & "', "
                    SQL = SQL & "Jumlah_Bag = '" & LvJumlahBagMasuk & "', "
                    SQL = SQL & "Harga='" & Harga & "' "
                    SQL = SQL & "where No_Faktur='" & Txt_NoFaktur.Text & "' "
                    SQL = SQL & "and Urut_Loading='" & LvUrutLoading & "' "
                    ExecuteTrans(SQL)

                Next

                Dim jumlah_masuk_BarangTimbang As Double = 0
                If metodeTruckScale = "TRUCK SCALE" Then

                    SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & LvKdBarang & "',"
                    SQL = SQL & "'" & CmbSatuan.Text & "','" & sat_brg & "',"
                    SQL = SQL & "" & HilangkanTanda(Txt_Netto.Text) & ") as Hasil "
                    Using dr3 = OpenTrans(SQL)
                        If dr3.Read Then
                            If General_Class.CekNULL(dr3("Hasil")) <> "" Then
                                jumlah_masuk_BarangTimbang = dr3("Hasil")
                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Satuan " & CmbSatuan.Text & " Ke " & sat_brg & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End If
                    End Using

                    SQL = "Update EMI_Barang_Masuk_Perpallet set "
                    SQL = SQL & "Flag_Timbang = 'Y', "
                    SQL = SQL & "jumlah = '" & HilangkanTanda(Txt_Netto.Text) & "', "
                    SQL = SQL & "Nilai_Barang = '" & jumlah_masuk_BarangTimbang & "', "
                    SQL = SQL & "tanggal_Timbang = '" & Format(CDate(tgl_skg), "yyyy-MM-dd") & "', "
                    SQL = SQL & "jam_Timbang = '" & Format(CDate(tgl_skg), "HH:mm:ss") & "', "
                    SQL = SQL & "user_Timbang = '" & UserID & "' "

                    SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and No_pembelian_loading = '" & TxtNo_Loading.Text & "' "
                    SQL = SQL & "and Metode_timbang = 'TRUCK SCALE' "
                    ExecuteTrans(SQL)

                End If

                Dim total_hpp As Double = 0

                SQL = "select No_Pembelian_Loading, No_SJ, Flag_angkut, Selesai, Sdh_Cetak, no_faktur, "
                SQL = SQL & "kode_perusahaan, kode_stock_owner, kode_barang, "
                SQL = SQL & "serial_number, Nilai_Barang as jumlah, Tgl_Produksi_Real as Tgl_Produksi, "
                SQL = SQL & "Tgl_Expired_Real as Tgl_Expired, Id_Warehouse, "
                SQL = SQL & "id_Susunan,  Kode_Unik_Asal, Kode_Unik_Berjalan, "
                SQL = SQL & "Jumlah_Bags, Qr_Code, Batch_Number, warna from EMI_Barang_Masuk_Perpallet a where "
                SQL = SQL & "Kode_Perusahaan='" & KodePerusahaan & "' and "
                SQL = SQL & "No_Pembelian_Loading='" & TxtNo_Loading.Text & "' and status is null and Flag_Timbang_Keluar is null "
                Using ds = BindingTrans(SQL)
                    With ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For index = 0 To .Rows.Count - 1

                                If General_Class.CekNULL(.Rows(index).Item("Sdh_Cetak")) = "" Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Proses tidak bisa dilanjutkan, barang Belum Selesai Bongkar !!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                                Dim harga As Double = 0
                                SQL = "Select Top(1)(case when "
                                SQL = SQL & "a.flag_refraksi Is null then a.hpp_satuan_display else a.Harga_Refraksi end) As harga "
                                SQL = SQL & "From EMI_Pembelian_Loading_Detail a, EMI_Pembelian_PO_Detail b, "
                                SQL = SQL & "EMI_Barang_Masuk_Perpallet_Detail c Where "
                                SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.Urut_PO = b.No_Urut And "
                                SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan And a.Urut_Oto = c.Urut_Loading "
                                SQL = SQL & "and c.kode_Perusahaan='" & KodePerusahaan & "' and c.no_faktur='" & .Rows(index).Item("no_faktur") & "' "
                                Using dr = OpenTrans(SQL)
                                    If dr.Read Then

                                        If flag_import = "Y" Then

                                            If flag_HPP = "" Then
                                                harga = 0
                                            Else
                                                harga = dr("Harga")

                                            End If
                                        Else
                                            harga = dr("Harga")

                                        End If
                                    Else
                                        dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("PO Tidak ditemukan . . ! !")
                                        Exit Sub
                                    End If
                                End Using

                                Dim Satuan As String = LvSatuan
                                Dim Satuan_Barang As String = ""

                                SQL = "select distinct Satuan from Barang where "
                                SQL = SQL & "Kode_Barang='" & LvKdBarang & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
                                Using dr2 = OpenTrans(SQL)
                                    If dr2.Read Then
                                        Satuan_Barang = dr2("Satuan")
                                    Else
                                        dr2.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang Tidak ditemukan . . ! !")
                                        Exit Sub
                                    End If
                                End Using

                                'UBAH KE SATUAN PO
                                SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','UANG','" & LvKdBarang & "',"
                                SQL = SQL & "'" & Satuan & "','" & Satuan_Barang & "',"
                                SQL = SQL & "" & harga & ") as Hasil "
                                Using dr3 = OpenTrans(SQL)
                                    If dr3.Read Then
                                        If General_Class.CekNULL(dr3("Hasil")) <> "" Then
                                            harga = dr3("Hasil")
                                        Else
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Satuan " & Satuan & " Ke " & Satuan_Barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End If
                                End Using

                                total_hpp += (harga * .Rows(index).Item("jumlah"))

                                Dim Random As New Random()
                                Dim str As String = Format(Random.Next(0, 999), "000") & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "HHmmss")
                                Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                                Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & harga & Tanda_SN & "02" & Tanda_SN & Format(DateTime.Now, "yyyy-MM-dd")

                                SQL = "Update barang Set "
                                SQL = SQL & "good_stock = good_stock + " & .Rows(index).Item("jumlah") & ", "
                                SQL = SQL & "Jumlah_Bags = Jumlah_Bags +" & .Rows(index).Item("Jumlah_Bags") & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' And "
                                SQL = SQL & "kode_stock_owner = '" & .Rows(index).Item("kode_stock_owner") & "' And "
                                SQL = SQL & "kode_barang = '" & .Rows(index).Item("kode_barang") & "' "
                                ExecuteTrans(SQL)

                                ''GET ID_WAREHOUSE YG KOSONG
                                Dim available_Id_Warehouse As String = ""
                                Dim available_NoPallet As String = ""

                                SQL = "select top(1) a.id_wms_warehouse_position, b.nomor_urut from "
                                SQL = SQL & "view_warehouse_position a, view_warehouse_position_detail b "
                                SQL = SQL & "where a.Id_WMS_Warehouse_Position=b.Id_WMS_Warehouse_Position "
                                SQL = SQL & " And a.kode_Perusahaan = b.kode_Perusahaan And a.kode_Perusahaan ='" & KodePerusahaan & "' "
                                SQL = SQL & "and a.Kode_Stock_Owner='" & .Rows(index).Item("kode_stock_owner") & "' and b.Kode_Barang is null"
                                Using Dr2 = OpenTrans(SQL)
                                    Do While Dr2.Read
                                        available_Id_Warehouse = Dr2("id_wms_warehouse_position")
                                        available_NoPallet = Dr2("nomor_urut")
                                    Loop
                                End Using

                                '=========================
                                '=     GET TGL MASUK     =
                                '=========================
                                Dim TglMasuk As String = ""
                                SQL = "select Tanggal_Masuk from EMI_Pembelian_Loading where No_Faktur = '" & .Rows(index).Item("No_Pembelian_Loading") & "' and No_SJ = '" & .Rows(index).Item("No_SJ") & "' "
                                SQL = SQL & "and Flag_Timbang_Keluar is null and status is null "
                                Using Dr2 = OpenTrans(SQL)
                                    If Dr2.Read Then

                                        TglMasuk = Format(Dr2("Tanggal_Masuk"), "yyyy-MM-dd")
                                    Else
                                        Dr2.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Tanggal Masuk Tidak DiTemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                '========================
                                '=     CEK WARNA QC     =
                                '========================
                                Dim warnaQC, HasilQC, warnaFinal As String
                                SQL = "select Warna, Hasil "
                                SQL = SQL & "from EMI_Hasil_Quality_Control "
                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and No_Fak_Loading_Barang = '" & .Rows(index).Item("No_Pembelian_Loading") & "' "
                                Using Dr2 = OpenTrans(SQL)
                                    If Dr2.Read Then

                                        warnaQC = Dr2("Warna")
                                        HasilQC = Dr2("Hasil")
                                    Else
                                        Dr2.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data Barang pada QC tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using


                                If warnaQC = "KUNING" And HasilQC = "DITERIMA" Then
                                    warnaFinal = "HIJAU"
                                    'ElseIf warnaQC = "KUNING" And HasilQC = "TOLAK SELURUH" Then
                                    '    warnaFinal = "MERAH"
                                ElseIf HasilQC = "TOLAK SELURUH" Then
                                    warnaFinal = "MERAH"
                                ElseIf warnaQC = "HIJAU" Then
                                    warnaFinal = "HIJAU"
                                Else
                                    warnaFinal = "KUNING"
                                End If


                                SQL = "insert into Barang_SN(kode_perusahaan, kode_stock_owner, kode_barang, "
                                SQL = SQL & "serial_number, jumlah, Tgl_Produksi, Tgl_Expired, Id_Warehouse, "
                                SQL = SQL & "id_Susunan, Nomor_Pallet, Kode_Unik_Asal, Kode_Unik_Berjalan, "
                                SQL = SQL & "Jumlah_Bags, Qr_Code, Batch_Number, warna, Tgl_masuk) "
                                SQL = SQL & "Values( "
                                SQL = SQL & "'" & KodePerusahaan & "','" & .Rows(index).Item("kode_stock_owner") & "', "
                                SQL = SQL & "'" & .Rows(index).Item("kode_barang") & "','" & SN_Baru & "', "
                                SQL = SQL & "'" & .Rows(index).Item("jumlah") & "','" & .Rows(index).Item("Tgl_Produksi") & "', "
                                SQL = SQL & "'" & .Rows(index).Item("Tgl_Expired") & "','" & available_Id_Warehouse & "', "
                                SQL = SQL & "'" & .Rows(index).Item("id_Susunan") & "','" & available_NoPallet & "', "
                                SQL = SQL & "'" & .Rows(index).Item("Kode_Unik_Asal") & "','" & .Rows(index).Item("Kode_Unik_Berjalan") & "', "
                                SQL = SQL & "'" & .Rows(index).Item("Jumlah_Bags") & "','" & .Rows(index).Item("Qr_Code") & "', "
                                SQL = SQL & "'" & .Rows(index).Item("Batch_Number") & "','" & warnaFinal & "', "
                                SQL = SQL & "'" & TglMasuk & "')"
                                ExecuteTrans(SQL)

                                SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
                                SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                                SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                                SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                                SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                                SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                                SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & .Rows(index).Item("kode_stock_owner") & "' "
                                SQL = SQL & "AND a.Kode_Barang = '" & .Rows(index).Item("kode_barang") & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                                Using Ds4 = BindingTrans(SQL)

                                    If Ds4.Tables("MyTable").Rows.Count <> 0 Then
                                        If Ds4.Tables("MyTable").Rows(0).Item("good_stock") <> Ds4.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds4.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds4.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                End Using

                                SQL = "Update EMI_Barang_Masuk_Perpallet set "
                                SQL = SQL & "serial_number_awal = '" & SN_Baru & "', "
                                SQL = SQL & "Flag_Timbang_Keluar = 'Y' "
                                SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "no_faktur='" & .Rows(index).Item("no_faktur") & "' "
                                ExecuteTrans(SQL)

                            Next
                        Else

                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("data Tidak ditemukan . . ! !")
                            Exit Sub

                        End If
                    End With
                End Using

                '''If total_hpp <> Total_HPP_PO Then
                '''    CloseTrans()
                '''    CloseConn()
                '''    MessageBox.Show("Data Tidak Sinkron")
                '''    Exit Sub
                '''End If

                SQL = "Update EMI_Pembelian_Loading "
                SQL = SQL & "Set Flag_Proses_loading = null, flag_sdh_update = 'Y' "
                SQL = SQL & "Where No_Faktur = '" & TxtNo_Loading.Text & "' "
                ExecuteTrans(SQL)

                'CEK APAKAH PO TERPENUHI
                SQL = "select * from EMI_Pembelian_Loading_Detail where No_Faktur='" & TxtNo_Loading.Text & "' "
                SQL = SQL & "and Flag_Timbang_Keluar is null"
                Using dr = OpenTrans(SQL)
                    If Not dr.Read Then
                        dr.Close()
                        SQL = "Update EMI_Pembelian_Loading "
                        SQL = SQL & "Set Flag_Timbang_Keluar = 'Y', "
                        SQL = SQL & "Flag_Proses_loading = 'Y' "
                        SQL = SQL & "Where No_Faktur = '" & TxtNo_Loading.Text & "' "
                        ExecuteTrans(SQL)
                    End If
                End Using

                '''Dim Blob_1 As BlobClient = Container.GetBlobClient(BlobName_1)
                '''Blob_1.Upload(FilePath_1, New BlobHttpHeaders With {.ContentType = "image/jpeg"})

                '''Dim Blob_2 As BlobClient = Container.GetBlobClient(BlobName_2)
                '''Blob_2.Upload(FilePath_2, New BlobHttpHeaders With {.ContentType = "image/jpeg"})
                'Btn_Simpan.Tag = "&SimpanBruto"
                'Btn_Simpan.Text = "&Simpan Bruto"
                ' kosong()

                ''isError = False
                ''If flag_import = "Y" Then

                ''    If flag_HPP = "Y" Then
                ''        Jurnal_Import()
                ''    Else
                ''        isError = True
                ''    End If

                ''Else
                ''    Jurnal_Lokal()
                ''End If

                ''If isError = False Then
                ''    CloseTrans()
                ''    CloseConn()
                ''    MessageBox.Show("Ada Masalah pada Jurnal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ''    Exit Sub
                ''End If

                Cmd.Transaction.Commit()
                CloseConn()

                MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                'Exit Sub

            End If
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        '=====================
        '=       CETAK       =
        '=====================
        Try
            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            ''REPORT

            If jenisMasuk = "MASUK" Then

                SQL = "select top 1 No_Faktur from EMI_Timbang_Unloading_PO_Det where no_Faktur='" & No_Faktur & "'"
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        'CrDoc = New Rpt_Surat_Perintah_Bongkar
                        'With A_Place_For_Printing2
                        '    CrDoc.SetDataSource(Ds)
                        '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        '    CrDoc.PrintOptions.PrinterName = ""
                        '    CrDoc.RecordSelectionFormula = "{EMI_Timbang_Unloading_PO_Det.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Timbang_Unloading_PO_Det.No_Faktur}='" & No_Faktur & "' "
                        '    CrDoc.SummaryInfo.ReportTitle = "Surat Perintah Bongkar"
                        '    .Text = "Surat Perintah Bongkar"
                        '    .CrystalReportViewer1.ReportSource = CrDoc
                        '    .Refresh()
                        '    .Show()
                        'End With

                        'CrDoc = New Rpt_Surat_Perintah_Bongkar
                        'kertas = "Faktur"

                        'CrDoc.SetDataSource(Ds)
                        'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        ''CrDoc.PrintOptions.PrinterName = "EPSON LX-310 ESC/P ESC/P"

                        'Dim printDialog As New PrintDialog()
                        'If printDialog.ShowDialog() = DialogResult.OK Then
                        '    CrDoc.PrintOptions.PrinterName = printDialog.PrinterSettings.PrinterName
                        '    CrDoc.PrintToPrinter(1, False, 0, 0)
                        'End If

                        'CrDoc.RecordSelectionFormula = "{EMI_Timbang_Unloading_PO_Det.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Timbang_Unloading_PO_Det.No_Faktur}='" & No_Faktur & "' "
                        ''CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                        'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        'doctoprint.PrinterSettings.PrinterName = PrinterName
                        'Dim rawKind As Integer
                        'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                        '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                        '        CrDoc.PrintOptions.PaperSize = rawKind
                        '        Exit For
                        '    End If
                        'Next

                        'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                        'CrDoc.PrintToPrinter(1, False, 1, 99)

                        '================================================================================================================================================================================================================================
                        '================================================================================================================================================================================================================================

                        CrDoc = New Rpt_Surat_Perintah_Bongkar
                        kertas = "Faktur"

                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = PrinterNameSPB
                        CrDoc.RecordSelectionFormula = "{EMI_Timbang_Unloading_PO_Det.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Timbang_Unloading_PO_Det.No_Faktur}='" & No_Faktur & "' "
                        'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        doctoprint.PrinterSettings.PrinterName = PrinterNameSPB
                        Dim rawKind As Integer
                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                            If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                                rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                                CrDoc.PrintOptions.PaperSize = rawKind
                                Exit For
                            End If
                        Next

                        CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                        CrDoc.PrintToPrinter(1, False, 1, 99)

                        MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End Using

            ElseIf jenisMasuk = "KELUAR" Then

                SQL = "select top 1 No_Faktur from EMI_Timbang_Unloading_PO_Det where no_Faktur='" & No_Faktur & "'"
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        'CrDoc = New Rpt_Bukti_Penerimaan_Barang
                        'With A_Place_For_Printing2
                        '    CrDoc.SetDataSource(Ds)
                        '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        '    CrDoc.PrintOptions.PrinterName = ""
                        '    CrDoc.RecordSelectionFormula = "{EMI_Timbang_Unloading_PO_Det.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Timbang_Unloading_PO_Det.No_Faktur}='" & No_Faktur & "' "
                        '    CrDoc.SummaryInfo.ReportTitle = "Surat Bukti Penerimaan Barang"
                        '    .Text = "Surat Bukti Penerimaan Barang"
                        '    .CrystalReportViewer1.ReportSource = CrDoc
                        '    .Refresh()
                        '    .Show()
                        'End With

                        CrDoc = New Rpt_Bukti_Penerimaan_Barang
                        kertas = "Faktur"

                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = PrinterNameBPB
                        CrDoc.RecordSelectionFormula = "{EMI_Timbang_Unloading_PO_Det.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Timbang_Unloading_PO_Det.No_Faktur}='" & No_Faktur & "' "
                        'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        doctoprint.PrinterSettings.PrinterName = PrinterNameBPB
                        Dim rawKind As Integer
                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                            If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                                rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                                CrDoc.PrintOptions.PaperSize = rawKind
                                Exit For
                            End If
                        Next

                        CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                        CrDoc.PrintToPrinter(1, False, 1, 99)
                    End If
                End Using

                SQL = "select Kode_Jenis_Muatan from Vw_Bukti_Timbang where No_Faktur = '" & No_Faktur & "'"
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        'CrDoc = New Rpt_Bukti_Timbang
                        'With A_Place_For_Printing3
                        '    CrDoc.SetDataSource(Ds)
                        '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        '    CrDoc.PrintOptions.PrinterName = ""
                        '    CrDoc.RecordSelectionFormula = "{EMI_Timbang_Unloading.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Timbang_Unloading.No_Faktur}='" & No_Faktur & "' "
                        '    CrDoc.SummaryInfo.ReportTitle = "Surat Bukti Penerimaan Barang"
                        '    .Text = "Surat Bukti Timbang"
                        '    .CrystalReportViewer1.ReportSource = CrDoc
                        '    .Refresh()
                        '    .Show()
                        'End With

                        CrDoc = New Rpt_Bukti_Timbang
                        kertas = "Faktur"

                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = PrinterNameBuktiTimbang
                        CrDoc.RecordSelectionFormula = "{Vw_Bukti_Timbang.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_Bukti_Timbang.No_Faktur}='" & No_Faktur & "' "
                        'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        doctoprint.PrinterSettings.PrinterName = PrinterNameBuktiTimbang
                        Dim rawKind As Integer
                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                            If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                                rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                                CrDoc.PrintOptions.PaperSize = rawKind
                                Exit For
                            End If
                        Next

                        CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                        CrDoc.PrintToPrinter(1, False, 1, 99)

                    End If
                End Using

                MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

            kosong()
            EMI_Display_Timbang.kosong()
            Me.Close()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Function CekNothing(ByVal str As String) As String
        Dim hasil As String = ""

        If str Is Nothing OrElse str = "" Then
            hasil = "0"
        Else
            hasil = str
        End If

        Return hasil
    End Function

    Private Sub CmbBarang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbBarang.SelectedIndexChanged
        Get_DGVMasuk()
    End Sub

    Private Sub CmbJenisMuatan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbJenisMuatan.SelectedIndexChanged

        Dim indexSelected As Integer = CmbJenisMuatan.SelectedIndex
        Dim metodeTruckScale As String = arrMetodeTruckScale(indexSelected).ToString.ToUpper.Trim

        CmbBarang.SelectedIndex = -1
        If metodeTruckScale = "TRUCK SCALE" Then
            CmbBarang.Enabled = True
        Else
            CmbBarang.Enabled = False

        End If

    End Sub

    Private Sub CmbSatuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSatuan.SelectedIndexChanged

    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)

        Get_Isi_DataGridView(DgvPO.CurrentRow.Index)

        If IsNumeric(LvJumlahMasuk) = False Or Val(LvJumlahMasuk) < 0 Then
            DgvPO.CurrentRow.Cells(ItemJumlahMasuk).Value = 0
            Exit Sub
        End If

        'If getSumOfJumlah() > Val(HilangkanTanda(Txt_Netto.Text)) Then

        '    MessageBox.Show("Jumlah Berlebih dari berat Netto", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    DgvPO.CurrentRow.Cells(ItemJumlahMasuk).Value = 0
        '    Exit Sub
        'End If

    End Sub

    Private Sub DataGridView1_CellEndEdit_1(sender As Object, e As DataGridViewCellEventArgs) Handles DgvPO.CellEndEdit
        If IsNumeric(DgvPO.CurrentRow.Cells(ItemJumlahMasuk).Value) = False Then
            DgvPO.CurrentRow.Cells(ItemJumlahMasuk).Value = ""
        End If

        Dim curentKodeBarang As String = DgvPO.CurrentRow.Cells(ItemKdBarang).Value
        Dim curentJumlahMasuk As String = DgvPO.CurrentRow.Cells(ItemJumlahMasuk).Value

        'If Not curentJumlahMasuk = "" Or Not curentJumlahMasuk.Trim.Length = 0 Then

        '    Dim JumlahBagsTimbang As Double = 0
        '    Dim JumlahBarangPO As Double = 0

        '    Dim BeratPerBags As Double = 0

        '    For i As Integer = 0 To DgvTimbang.Rows.Count - 1
        '        Get_Isi_DataGridViewTimbang(i)

        '        If LvTimbangKdBarang = curentKodeBarang Then
        '            JumlahBagsTimbang = Val(HilangkanTanda(LvTimbangJmlBags))
        '            JumlahBarangPO = Val(HilangkanTanda(LvTimbangjmlBarang))
        '            Exit For
        '        End If

        '    Next

        '    If JumlahBagsTimbang <> 0 And JumlahBarangPO <> 0 Then

        '        Dim JumlahFinalBags As Double = 0

        '        BeratPerBags = JumlahBarangPO / JumlahBagsTimbang

        '        JumlahFinalBags = curentJumlahMasuk / BeratPerBags

        '        DgvPO.CurrentRow.Cells(ItemJumlahBags).Value = Math.Round(JumlahFinalBags)

        '    End If

        'End If

    End Sub

    Private Sub DgvTimbang_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvTimbang.CellContentClick

    End Sub

    Private Sub EMI_Timbang_Unloading_Invalidated(sender As Object, e As InvalidateEventArgs) Handles Me.Invalidated

    End Sub

    Private Sub Get_Data_Timbangan()
        Try
            Dim sp = New SerialPort(My.Settings.Port_Timbangan, 9600, Parity.None, 8, StopBits.One)
            If Not (sp Is Nothing) Then
                sp.Open()
                sp.ReadLine()

                sp.Close()
                sp.Dispose()
                sp = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Get_Isi_DataGridView(ByVal NoIndex As Integer)
        LvNoPO = DgvPO.Rows(NoIndex).Cells(ItemNoPO).Value
        LvKdBarang = DgvPO.Rows(NoIndex).Cells(ItemKdBarang).Value
        LvNama = DgvPO.Rows(NoIndex).Cells(ItemNama).Value
        LvTglExp = DgvPO.Rows(NoIndex).Cells(ItemTglExp).Value
        LvTglProd = CekNothing(DgvPO.Rows(NoIndex).Cells(ItemTglProd).Value)
        LvUrutPO = DgvPO.Rows(NoIndex).Cells(ItemUrutPO).Value
        LvSatuan = DgvPO.Rows(NoIndex).Cells(ItemSatuan).Value
        LvJumlah = DgvPO.Rows(NoIndex).Cells(ItemJumlah).Value
        LvJumlahMasuk = DgvPO.Rows(NoIndex).Cells(ItemJumlahMasuk).Value
        LvJumlahBagMasuk = DgvPO.Rows(NoIndex).Cells(ItemJumlahBags).Value
        LvUrutLoading = DgvPO.Rows(NoIndex).Cells(ItemUrutLoading).Value
    End Sub

    Private Sub Get_Isi_DataGridViewTimbang(ByVal NoIndex As Integer)
        LvTimbangKdBarang = DgvTimbang.Rows(NoIndex).Cells(ItemTimbangKdBarang).Value
        LvTimbangNmBarang = DgvTimbang.Rows(NoIndex).Cells(ItemTimbangNmBarang).Value
        LvTimbangSatuan = DgvTimbang.Rows(NoIndex).Cells(ItemTimbangSatuan).Value
        LvTimbangJmlBags = DgvTimbang.Rows(NoIndex).Cells(ItemTimbangJmlBags).Value
        LvTimbangBeratBags = DgvTimbang.Rows(NoIndex).Cells(ItemTimbangBeratBags).Value
        LvTimbangJmlPallet = DgvTimbang.Rows(NoIndex).Cells(ItemTimbangJmlPallet).Value
        LvTimbangjmlBarang = DgvTimbang.Rows(NoIndex).Cells(ItemTimbangJmlBarang).Value
        LvTimbangBeratBarang = DgvTimbang.Rows(NoIndex).Cells(ItemTimbangBeratBarang).Value
    End Sub
    Private Sub get_no_faktur()
        Txt_NoFaktur.Text = fTransTimbanganKosong & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("EMI_Timbang_Unloading", "No_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_Faktur, 1, " & Len(fTransTimbanganKosong) + 4 & ")", fTransTimbanganKosong & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub getSumOfBerat()
        Dim totalJumlah As Double = 0
        Dim totalBags As Double = 0

        For i As Integer = 0 To DgvTimbang.RowCount - 1
            Get_Isi_DataGridViewTimbang(i)

            Dim nilai As Double = Val(HilangkanTanda(LvTimbangBeratBarang)) - Val(HilangkanTanda(LvTimbangBeratBags))
            totalJumlah = totalJumlah + nilai

            totalBags = totalBags + Val(HilangkanTanda(LvTimbangJmlBags))

        Next

        TxtTotalBeratBarang.Text = Format(totalJumlah, "N2")
        Tot_Bags.Text = Format(totalBags, "N2")
    End Sub

    'Private Sub TextBoxColumn3_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
    '        e.Handled = True
    '    End If
    'End Sub
    Private Sub getSumOfJumlah()
        Dim totalJumlah As Double = 0

        For i As Integer = 0 To DgvTimbang.RowCount - 1
            Get_Isi_DataGridViewTimbang(i)

            Dim nilai As Double = Val(HilangkanTanda(LvTimbangjmlBarang))
            Dim totalPO As Double = 0
            Dim kd_barang = LvTimbangKdBarang

            For index = 0 To DgvPO.RowCount - 1
                Get_Isi_DataGridView(index)

                If kd_barang = LvKdBarang Then
                    totalPO = totalPO + Val(HilangkanTanda(LvJumlahMasuk))
                End If

            Next

            If nilai <> totalPO Then
                MessageBox.Show(LvTimbangNmBarang & " Berbeda dengan PO")
                Exit Sub
            End If
        Next

    End Sub

    Private Sub Jurnal_Import()

        '=== GET DATA RENCANA ORDER, BERDASAR NO LOADING ===
        Dim id_rencana As Integer
        Dim Lokasi_Jurnal As String
        SQL = "select Lokasi, No_Fak_HPP, b.ID_Rencana "
        SQL = SQL & "from emi_pembelian_loading a, HPP_Import b where "
        SQL = SQL & "a.Kode_Perusahaan=b.kode_perusahaan and a.No_Fak_HPP=b.No_Faktur "
        SQL = SQL & "and a.status is null and b.status is null "
        SQL = SQL & "and a.No_Faktur = '" & TxtNo_Loading.Text & "' "
        SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
        Using dr = OpenTrans(SQL)
            If dr.Read Then
                id_rencana = dr("ID_Rencana")
                Lokasi_Jurnal = dr("Lokasi")
            Else
                dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("No Faktur Tidak ditemukan . . ! ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End Using

        '=== GET DATA RENCANA ORDER, JIKA TERDAPAT GABUNG RO ===
        Dim id_rencana_group As String = ""

        Dim i As Integer = 0
        SQL = "select Flag_Gabungan from rencana_order where "
        SQL = SQL & "Id_rencana = '" & id_rencana & "'"
        Using Dr2 = OpenTrans(SQL)
            If Dr2.Read Then
                If General_Class.CekNULL(Dr2("Flag_Gabungan")) = "Y" Then
                    Dr2.Close()
                    SQL = "select a.id_rencana, a.Total_Persen from rencana_order a, rencana_order_gabungan b where "
                    SQL = SQL & "a.id_rencana = b.Id_rencana and b.Id_rencana_induk = '" & id_rencana & "'"
                    Using Dr = OpenTrans(SQL)
                        Do While Dr.Read
                            If i <> 0 Then
                                id_rencana_group = id_rencana_group & ", "
                            End If

                            id_rencana_group = id_rencana_group & "'" & Dr("id_rencana") & "'"
                            i += 1
                        Loop
                    End Using
                Else
                    id_rencana_group = "'" & id_rencana & "'"
                End If
            Else
                Dr2.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Id Rencana tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End Using

        Dim lokasi_Barang As String = ""

        SQL = "select kode_stock_owner from EMI_Barang_Masuk_Perpallet a where "
        SQL = SQL & "Kode_Perusahaan='" & KodePerusahaan & "' and "
        SQL = SQL & "No_Pembelian_Loading='" & TxtNo_Loading.Text & "' and status is null "
        '    SQL = SQL & "and Flag_Timbang_Keluar is null "
        Using dr = OpenTrans(SQL)
            If dr.Read Then
                lokasi_Barang = dr("kode_stock_owner")
            Else
                dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Data Tidak ditemukan . . ! !")
                Exit Sub
            End If
        End Using

        '=== INISIAL FAKTUR UNTUK JURNAL ======
        Dim inisial_faktur_dari As String
        SQL = "select inisial_faktur from stock_owner_gudang "
        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & lokasi_Barang & "' "
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                'akun_persediaan_dari = Dr("persediaan")
                inisial_faktur_dari = Dr("inisial_faktur")
            Else
                Dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End Using

        '=== AMBIL DATA UNTUK KETERANGAN PO ====
        Dim Lokasi_Group As String = ""
        Dim Konte_group As String = ""
        Dim PO_Induk As String = ""
        Dim Kategori_Group As String = ""

        SQL = "select Tanggal_PO from Rencana_Order a where "
        SQL = SQL & "a.id_rencana ='" & id_rencana & "'"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                PO_Induk = "PO " & Format(Dr("Tanggal_PO"), "MM.dd")
            Else
                Dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Id Rencana tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End Using

        Dim Konte As Double = 0
        Dim xxz As Integer = 0
        SQL = "select a.id_rencana, a.Total_Persen, a.Lokasi, B.Inisial_Faktur "
        SQL = SQL & "from rencana_order a, Stock_Owner b where "
        SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and A.Lokasi = B.Kode_Stock_Owner and "
        SQL = SQL & "a.id_rencana in(" & id_rencana_group & ")"
        Using Dr = OpenTrans(SQL)
            Do While Dr.Read
                If xxz <> 0 Then
                    Lokasi_Group = Lokasi_Group & ", "
                End If
                Lokasi_Group = Lokasi_Group & Dr("Inisial_Faktur")
                Konte = Konte + Dr("Total_Persen")

                xxz += 1
            Loop
        End Using

        'If Lokasi_Group.Length <> 0 Then
        '    Lokasi_Group = Strings.Left(Lokasi_Group, Len(Lokasi_Group) - 2)
        'End If

        Dim kontainer As Integer = Konte / 100
        Dim jumlah As Integer = kontainer * 100
        Dim selisih As Integer = Konte - jumlah

        If selisih = 0 Or selisih <= 99 Then
            kontainer = kontainer
        Else
            kontainer = kontainer + 1
        End If

        Konte_group = kontainer & " C"
        Dim ix As Integer = 0

        SQL = "select d.Jenis from "
        SQL = SQL & "submit_Po A, Detail_Submit_PO B, Barang C, Kategori_Besar d where "
        SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur "
        SQL = SQL & "and B.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner= "
        SQL = SQL & "c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and "
        SQL = SQL & "c.Kode_Perusahaan = d.Kode_Perusahaan And c.Kode_Kategori_Besar = d.Kode_Kategori_Besar "
        SQL = SQL & "and id_rencana in(" & id_rencana_group & ") and a.status is null "
        SQL = SQL & "group by d.Jenis "
        Using Ds = BindingTrans(SQL)
            With Ds.Tables("MyTable")
                For index As Integer = 0 To .Rows.Count - 1
                    ix = .Rows.Count

                    SQL = "select top(1) d.Kode_Kategori_Besar from "
                    SQL = SQL & "submit_Po A, Detail_Submit_PO B, Barang C, Kategori_Besar d where "
                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur "
                    SQL = SQL & "and B.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner= "
                    SQL = SQL & "c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and "
                    SQL = SQL & "c.Kode_Perusahaan = d.Kode_Perusahaan And c.Kode_Kategori_Besar = d.Kode_Kategori_Besar "
                    SQL = SQL & "and id_rencana in(" & id_rencana_group & ")  and d.Jenis = '" & .Rows(index).Item("Jenis") & "' and a.status is null "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If index <> 0 Then
                                Kategori_Group = Kategori_Group & ", "
                            End If
                            Kategori_Group = Kategori_Group & Dr("Kode_Kategori_Besar")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Jenis Kategori Barang Tidak Di Temukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                Next
            End With
        End Using

        Dim ket As String = Strings.Left(Txt_NoFaktur.Text & "; " & PO_Induk & "; " & Konte_group & "; " & Kategori_Group & "; " & Lokasi_Group, 180)

        'inser jurnal ard
        Dim coa_Hutang_Dalam_Proses As String = ""
        Dim coa_Selisih_Hutang_Import As String = ""
        Dim coa_Billing As String = ""
        Dim coa_freigt As String = ""
        Dim coa_Storage As String = ""
        Dim coa_Tot_Pot_Stock_IDR As String = ""
        Dim coa_Tdk_Pot_Stock_IDR As String = ""
        Dim coa_Tdk_Pot_Stock_Hutang_IDR_Utama As String = ""
        Dim coa_Tdk_Pot_Stock_Hutang_IDR_Penolong As String = ""
        Dim coa_pph As String = ""
        Dim coa_pib As String = ""
        Dim coa_selisih_pib As String = ""
        Dim coa_pph_billing As String = ""
        Dim coa_hutang_pph_billing As String = ""
        Dim coa_Selisih_AVG_Import As String = ""
        Dim coa_Selisih_PO As String = ""
        Dim coa_Selisih_PO_Biaya As String = ""
        Dim coa_selisih_new
        Dim Metode_Hitung_Konte As String = ""

        SQL = "select hutang_pph_billing, pph_billing, Metode_Hitung_Konte, Hutang_Dalam_Proses, Selisih_Hutang_Import, Hutang_Billing_Import, "
        SQL = SQL & "Hutang_Storage_Import, Hutang_Freight_Import, Akun_Tot_Pot_Stock, "
        SQL = SQL & "Akun_Tdk_Pot_Stock, Akun_Tdk_Pot_Stock_Hutang_Utama, "
        SQL = SQL & "Akun_Tdk_Pot_Stock_Hutang_Penolong, Akun_pph, akun_pib, akun_selisih_pib, Akun_Selisih_AVG_Import, Akun_Selisih_PO, Akun_Selisih_PO_Biaya from stock_Owner "
        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Kode_stock_Owner = '" & Lokasi_Jurnal & "' "
        Using dr = OpenTrans(SQL)
            If dr.Read Then
                Metode_Hitung_Konte = dr("Metode_Hitung_Konte")
                coa_Hutang_Dalam_Proses = dr("Hutang_Dalam_Proses")
                coa_Selisih_Hutang_Import = dr("Selisih_Hutang_Import")
                coa_Billing = dr("Hutang_Billing_Import")
                coa_freigt = dr("Hutang_Freight_Import")
                coa_Storage = dr("Hutang_Storage_Import")
                coa_Tot_Pot_Stock_IDR = dr("Akun_Tot_Pot_Stock")
                coa_Tdk_Pot_Stock_IDR = dr("Akun_Tdk_Pot_Stock")
                coa_Tdk_Pot_Stock_Hutang_IDR_Utama = dr("Akun_Tdk_Pot_Stock_Hutang_Utama")
                coa_Tdk_Pot_Stock_Hutang_IDR_Penolong = dr("Akun_Tdk_Pot_Stock_Hutang_Penolong")
                coa_pph = dr("Akun_pph")
                coa_pib = dr("akun_pib")
                coa_selisih_pib = dr("akun_selisih_pib")
                coa_pph_billing = dr("pph_billing")
                coa_hutang_pph_billing = dr("hutang_pph_billing")
                coa_Selisih_AVG_Import = dr("Akun_Selisih_AVG_Import")
                coa_Selisih_PO = dr("Akun_Tdk_Pot_Stock_Hutang_Utama")
                coa_Selisih_PO_Biaya = dr("Akun_Tdk_Pot_Stock_Hutang_Utama")
                coa_selisih_new = dr("Akun_Selisih_PO")
            Else
                dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Lokasi Tidak ditemukan")
                Exit Sub
            End If
        End Using

        Dim Flag_Average_Sup As String = ""
        SQL = "select Flag_average "
        SQL = SQL & "from Transaksi_Biaya_Import a where  a.Id_rencana = '" & id_rencana & "' and status is null"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                Flag_Average_Sup = Dr("Flag_average")
            Else
                Dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Supplier Tidak ditemukan")
                Exit Sub
            End If
        End Using

        Dim Flag_Average_Sup3 As String = ""
        SQL = "select Flag_average "
        SQL = SQL & "from Transaksi_Biaya_Import3 a where  a.Id_rencana = '" & id_rencana & "' and status is null"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                Flag_Average_Sup3 = Dr("Flag_average")
            Else
                Dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Supplier Tidak ditemukan")
                Exit Sub
            End If
        End Using

        Dim Arr_Biaya_Import_Master As New ArrayList
        Dim Arr_Biaya_Import As New ArrayList
        Dim Arr_Biaya_Import_AVG As New ArrayList
        Dim Arr_Akun1 As New ArrayList
        Dim Arr_Akun2 As New ArrayList
        Dim Arr_Biaya_Import_Kategori As New ArrayList

        Dim Arr_Biaya_Bongkar_Import_Master As New ArrayList
        Dim Arr_Biaya_Bongkar_Import As New ArrayList
        Dim Arr_Lokasi_Bongkar_Import As New ArrayList
        Dim Arr_Akun1_Bongkar As New ArrayList
        Dim Arr_Akun2_Bongkar As New ArrayList
        Dim Arr_Biaya_Bongkar_Import_Kategori As New ArrayList

        Dim Biaya_Import_AVG As Double = 0
        Dim Selisih_Import_AVG As Double = 0
        Dim Biaya_Import_Total As Double = 0
        Dim Hutang_Dalam_Proses As Double = 0
        Dim Selisih_Hutang As Double = 0
        Dim Biaya_PPN As Double = 0
        Dim Billing As Double = 0
        Dim pib As Double = 0
        Dim pph_billing As Double = 0
        Dim BM_Billing As Double = 0
        Dim Selisih_PO As Double = 0
        Dim Selisih_PO_Biaya As Double = 0

        Dim freigt As Double = 0
        Dim Storage As Double = 0
        Dim Tot_Pot_Stock_IDR As Double = 0
        Dim Tdk_Pot_Stock_IDR As Double = 0
        Dim Tdk_Pot_Stock_Hutang_IDR_Utama As Double = 0
        Dim Tdk_Pot_Stock_Hutang_IDR_Penolong As Double = 0
        Dim pph_pakai_persentase As Double = 0

        Dim PPN As Double = 0
        For index = 0 To DgvPO.Rows.Count - 1
            Get_Isi_DataGridView(index)

            'SQL = "Select c.PPN "
            'SQL = SQL & "From EMI_Pembelian_Loading_Detail a, EMI_Pembelian_PO_Detail b, EMI_Pembelian_PO c Where "
            'SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.Urut_PO = b.No_Urut And "
            'SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.No_Faktur = c.No_Faktur and c.status is null And "
            'SQL = SQL & "Urut_Oto = '" & LvUrutLoading & "' "
            'Using dr = OpenTrans(SQL)
            '    If dr.Read Then
            '        PPN = dr("PPN")
            '    Else
            '        dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("PO Tidak ditemukan . . ! !")
            '        Exit Sub
            '    End If
            'End Using

            SQL = "select a.No_faktur, a.ID_Rencana, b.Kode_stock_owner, b.Kode_barang, b.jumlah,"
            SQL = SQL & "b.Nilai_Pot_Stock/Jumlah as Nilai_Pot_Stock, Nilai_Tdk_Pot_stock_LNS/Jumlah as Nilai_Tdk_Pot_stock_LNS,"
            SQL = SQL & "b.Nilai_tdk_pot_stock_htg_utama/Jumlah as Nilai_tdk_pot_stock_htg_utama,"
            SQL = SQL & "b.Nilai_Tdk_Pot_Stock_HTG_Penolong/Jumlah as Nilai_Tdk_Pot_Stock_HTG_Penolong,"
            SQL = SQL & "b.PPH29/Jumlah as PPH29,Biaya_Billing/Jumlah as Biaya_Billing,Biaya_Kontainer/Jumlah as Biaya_Kontainer, b.Nilai_Selisih_PO/Jumlah as Nilai_Selisih_PO, b.Nilai_Selisih_PO_Biaya/Jumlah as Nilai_Selisih_PO_Biaya "
            SQL = SQL & "from HPP_Import a, Detail_HPP_Import b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_faktur And a.Status Is null And id_rencana ='" & id_rencana & "' AND B.Kode_Barang='" & LvKdBarang & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Storage = Storage + (Dr("biaya_kontainer") * LvJumlah)
                    Billing = Billing + (Dr("Biaya_Billing") * LvJumlah)
                    Tot_Pot_Stock_IDR = Tot_Pot_Stock_IDR + (Dr("Nilai_Pot_Stock") * LvJumlah)
                    Tdk_Pot_Stock_IDR = Tdk_Pot_Stock_IDR + (Dr("Nilai_Tdk_Pot_stock_LNS") * LvJumlah)
                    Tdk_Pot_Stock_Hutang_IDR_Utama = Tdk_Pot_Stock_Hutang_IDR_Utama + (Dr("Nilai_tdk_pot_stock_htg_utama") * LvJumlah)
                    Tdk_Pot_Stock_Hutang_IDR_Penolong = Tdk_Pot_Stock_Hutang_IDR_Penolong + (Dr("Nilai_Tdk_Pot_Stock_HTG_Penolong") * LvJumlah)
                    pph_pakai_persentase = pph_pakai_persentase + (Dr("PPH29") * LvJumlah)
                    Selisih_PO = Selisih_PO + (Dr("Nilai_Selisih_PO") * LvJumlah)
                    Selisih_PO_Biaya = Selisih_PO_Biaya + (Dr("Nilai_Selisih_PO_Biaya") * LvJumlah)
                End If
            End Using

            SQL = "select b.kode_stock_owner, b.Kode_Barang, jumlah, b.Nilai_PPH/Jumlah as Nilai_PPH, Nilai_PPN/Jumlah as Nilai_PPN, Nilai_BM/Jumlah as Nilai_BM "
            SQL = SQL & "from Total_Billing a, Detail_Total_Billing b where a.Kode_Perusahaan=b.Kode_perusahaan and a.No_Faktur=b.No_Faktur "
            SQL = SQL & "and a.ID_Rencana='" & id_rencana & "' and a.Status is nulL AND B.Kode_Barang='" & LvKdBarang & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    BM_Billing = BM_Billing + (Dr("Nilai_BM") * LvJumlah)
                    pib = pib + (Dr("Nilai_PPN") * LvJumlah)
                    pph_billing = pph_billing + (Dr("Nilai_PPH") * LvJumlah)
                End If
            End Using

            '1
            SQL = "select b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal, "
            SQL = SQL & "round(sum(b.total), 0) as Biaya, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select akun_1 from detail_account_master X where X.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "x.Kode_Master_Kategori_Biaya_import = b.Kode_Master_Kategori_Biaya_import and "
            SQL = SQL & "x.lokasi = '" & Lokasi_Jurnal & "' "
            SQL = SQL & "),0) as Akun_1, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select akun_2 from detail_account_master X where X.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "x.Kode_Master_Kategori_Biaya_import = b.Kode_Master_Kategori_Biaya_import and "
            SQL = SQL & "x.lokasi = '" & Lokasi_Jurnal & "' "
            SQL = SQL & "),0) as Akun_2 "

            SQL = SQL & "from transaksi_biaya_import a, detail_transaksi_biaya_import b, Master_Kategori_Biaya_Import c where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And "
            SQL = SQL & "a.no_faktur = b.no_faktur And a.status Is null and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Master_Kategori_Biaya_import = c.Kode_Master_Kategori_Biaya_Import and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & id_rencana & "' " 'and C.Flag_Gabungan = 'Y' "
            SQL = SQL & "group by b.Kode_Perusahaan, b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal "
            SQL = SQL & "order by b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import"
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For index3 As Integer = 0 To .Rows.Count - 1

                        Dim cek As Boolean = False
                        SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.Jumlah,"
                        SQL = SQL & "c.Kode_Kategori_Biaya_Import, c.Biaya/Jumlah as Biaya, c.Biaya_AVG/Jumlah as Biaya_AVG, c.Flag_Average "
                        SQL = SQL & "from hpp_import a, detail_hpp_import b,detail_hpp_import_biaya c where "
                        SQL = SQL & "a.kode_perusahaan=b.Kode_Perusahaan and a.No_faktur=b.No_Faktur and b.no_faktur=c.No_Faktur "
                        SQL = SQL & "and b.Kode_Barang=c.Kode_Barang and b.Kode_stock_owner=c.Kode_stock_owner and "
                        SQL = SQL & "c.Kode_kategori_biaya_import ='" & .Rows(index3).Item("Kode_kategori_biaya_import") & "' and c.Kode_Barang='" & LvKdBarang & "' and a.Status Is null And a.id_rencana ='" & id_rencana & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then

                                For index1 As Integer = 0 To Arr_Biaya_Import_Kategori.Count - 1

                                    If Arr_Biaya_Import_Kategori.Item(index1) = .Rows(index3).Item("Kode_kategori_biaya_import") Then

                                        Arr_Biaya_Import.Item(index1) += Val(HilangkanTanda(Format(dr("Biaya") * LvJumlah, "N0")))

                                        Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format(dr("Biaya") * LvJumlah, "N0")))
                                        Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format(dr("Biaya_AVG") * LvJumlah, "N0")))
                                        cek = True
                                    End If

                                Next

                                If cek = False Then
                                    Arr_Biaya_Import_Master.Add(.Rows(index3).Item("Kode_Master_Kategori_Biaya_Import"))
                                    Arr_Biaya_Import_Kategori.Add(dr("Kode_Kategori_Biaya_Import"))
                                    Arr_Biaya_Import.Add(Val(HilangkanTanda(Format(dr("Biaya") * LvJumlah, "N0"))))
                                    Arr_Akun1.Add(.Rows(index3).Item("Akun_1"))
                                    Arr_Akun2.Add(.Rows(index3).Item("Akun_2"))

                                    Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format(dr("Biaya") * LvJumlah, "N0")))
                                    Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format(dr("Biaya_AVG") * LvJumlah, "N0")))
                                End If

                            End If
                        End Using

                        ' ''SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.Jumlah, "
                        ' ''SQL = SQL & "c.Kode_Kategori_Biaya_Import, c.Biaya2/Jumlah as Biaya2, c.Biaya_AVG2/Jumlah as Biaya_AVG2, "
                        ' ''SQL = SQL & "c.Biayawetdry/Jumlah as Biayawetdry, c.Biayawetdry_AVG/Jumlah as Biayawetdry_AVG, c.Flag_Average "
                        ' ''SQL = SQL & "from hpp_import a, detail_hpp_import2 b,detail_hpp_import2_biaya c where "
                        ' ''SQL = SQL & "a.kode_perusahaan=b.Kode_Perusahaan and a.No_faktur=b.No_Faktur and b.no_faktur=c.No_Faktur "
                        ' ''SQL = SQL & "and b.Kode_Barang=c.Kode_Barang and b.Kode_stock_owner=c.Kode_stock_owner and b.Lokasi_Tujuan=c.lokasi_tujuan and "
                        ' ''SQL = SQL & "c.Kode_kategori_biaya_import ='" & .Rows(index3).Item("Kode_kategori_biaya_import") & "' and c.Kode_Barang='" & LvKdbarang & "' and c.Lokasi_tujuan ='" & LvSO & "'  and a.Status Is null And a.id_rencana ='" & id_rencana & "' "
                        ' ''Using dr = OpenTrans(SQL)
                        ' ''    If dr.Read Then

                        ' ''        For index1 As Integer = 0 To Arr_Biaya_Import_Kategori.Count - 1

                        ' ''            If Arr_Biaya_Import_Kategori.Item(index1) = .Rows(index3).Item("Kode_kategori_biaya_import") Then

                        ' ''                Arr_Biaya_Import.Item(index1) += Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * LvJumlah, "N0")))

                        ' ''                Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * LvJumlah, "N0")))
                        ' ''                Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format((dr("Biaya_AVG2") + dr("Biayawetdry_AVG")) * LvJumlah, "N0")))
                        ' ''                cek = True
                        ' ''            End If

                        ' ''        Next

                        ' ''        If cek = False Then
                        ' ''            Arr_Biaya_Import_Master.Add(.Rows(index3).Item("Kode_Master_Kategori_Biaya_Import"))
                        ' ''            Arr_Biaya_Import_Kategori.Add(dr("Kode_Kategori_Biaya_Import"))
                        ' ''            Arr_Biaya_Import.Add(Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * LvJumlah, "N0"))))
                        ' ''            Arr_Akun1.Add(.Rows(index3).Item("Akun_1"))
                        ' ''            Arr_Akun2.Add(.Rows(index3).Item("Akun_2"))

                        ' ''            Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * LvJumlah, "N0")))
                        ' ''            Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format((dr("Biaya_AVG2") + dr("Biayawetdry_AVG")) * LvJumlah, "N0")))
                        ' ''        End If

                        ' ''    End If
                        ' ''End Using

                    Next
                End With

            End Using

            SQL = "select b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal, B.Kode_Stock_Owner, "
            SQL = SQL & "round(sum(b.total), 0) as Biaya, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select akun_1 from detail_account_master X where X.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "x.Kode_Master_Kategori_Biaya_import = b.Kode_Master_Kategori_Biaya_import and "
            SQL = SQL & "x.lokasi = '" & Lokasi_Jurnal & "' "
            SQL = SQL & "),0) as Akun_1, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select akun_2 from detail_account_master X where X.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "x.Kode_Master_Kategori_Biaya_import = b.Kode_Master_Kategori_Biaya_import and "
            SQL = SQL & "x.lokasi = '" & Lokasi_Jurnal & "' "
            SQL = SQL & "),0) as Akun_2 "

            SQL = SQL & "from transaksi_biaya_import a, detail_transaksi_biaya_import b, Master_Kategori_Biaya_Import c where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And "
            SQL = SQL & "a.no_faktur = b.no_faktur And a.status Is null and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Master_Kategori_Biaya_import = c.Kode_Master_Kategori_Biaya_Import and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & id_rencana & "' and b.kode_stock_owner='" & Lokasi_Jurnal & "' " ' and C.Flag_Gabungan = 'T' "
            SQL = SQL & "group by b.Kode_Perusahaan, b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal, B.Kode_Stock_Owner "
            SQL = SQL & "order by b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import"
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For index3 As Integer = 0 To .Rows.Count - 1

                        Dim cek As Boolean = False

                        SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.Jumlah, "
                        SQL = SQL & "c.Kode_Kategori_Biaya_Import, c.Biaya2/Jumlah as Biaya2, c.Biaya_AVG2/Jumlah as Biaya_AVG2, "
                        SQL = SQL & "c.Biayawetdry/Jumlah as Biayawetdry, c.Biayawetdry_AVG/Jumlah as Biayawetdry_AVG, c.Flag_Average "
                        SQL = SQL & "from hpp_import a, detail_hpp_import2 b,detail_hpp_import2_biaya c where "
                        SQL = SQL & "a.kode_perusahaan=b.Kode_Perusahaan and a.No_faktur=b.No_Faktur and b.no_faktur=c.No_Faktur "
                        SQL = SQL & "and b.Kode_Barang=c.Kode_Barang and b.Kode_stock_owner=c.Kode_stock_owner and b.Lokasi_Tujuan=c.lokasi_tujuan and "
                        SQL = SQL & "c.Kode_kategori_biaya_import ='" & .Rows(index3).Item("Kode_kategori_biaya_import") & "' and c.Kode_Barang='" & LvKdBarang & "' and c.Lokasi_tujuan ='" & Lokasi_Jurnal & "'  and a.Status Is null And a.id_rencana ='" & id_rencana & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then

                                For index1 As Integer = 0 To Arr_Biaya_Bongkar_Import.Count - 1

                                    If Arr_Biaya_Bongkar_Import_Kategori.Item(index1) = .Rows(index3).Item("Kode_kategori_biaya_import") Then

                                        Arr_Biaya_Bongkar_Import.Item(index1) += Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * LvJumlah, "N0")))

                                        Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * LvJumlah, "N0")))
                                        Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format((dr("Biaya_AVG2") + dr("Biayawetdry_AVG")) * LvJumlah, "N0")))
                                        cek = True
                                    End If

                                Next

                                If cek = False Then

                                    Arr_Biaya_Bongkar_Import_Master.Add(.Rows(index3).Item("Kode_Master_Kategori_Biaya_Import"))
                                    Arr_Biaya_Bongkar_Import_Kategori.Add(.Rows(index3).Item("Kode_Kategori_Biaya_Import"))
                                    Arr_Biaya_Bongkar_Import.Add(Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * LvJumlah, "N0"))))
                                    Arr_Lokasi_Bongkar_Import.Add(Lokasi_Jurnal)
                                    Arr_Akun1_Bongkar.Add(.Rows(index3).Item("Akun_1"))
                                    Arr_Akun2_Bongkar.Add(.Rows(index3).Item("Akun_2"))

                                    Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * LvJumlah, "N0")))
                                    Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format((dr("Biaya_AVG2") + dr("Biayawetdry_AVG")) * LvJumlah, "N0")))
                                End If

                            End If
                        End Using

                    Next
                End With

            End Using

            SQL = "select b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal "

            SQL = SQL & "from transaksi_biaya_import3 a, detail_transaksi_biaya_import3 b, Master_Kategori_Biaya_Import c where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And "
            SQL = SQL & "a.no_faktur = b.no_faktur And a.status Is null and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Master_Kategori_Biaya_import = c.Kode_Master_Kategori_Biaya_Import and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & id_rencana & "' "
            SQL = SQL & "group by b.Kode_Perusahaan, b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal "
            SQL = SQL & "order by b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import"
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For index3 As Integer = 0 To .Rows.Count - 1

                        SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.Jumlah, "
                        SQL = SQL & "c.Kode_Kategori_Biaya_Import, c.Biaya/Jumlah as Biaya, c.Biaya_AVG/Jumlah as Biaya_AVG, c.Flag_Average "
                        SQL = SQL & "from hpp_import a, detail_hpp_import b,detail_hpp_import_biaya c where "
                        SQL = SQL & "a.kode_perusahaan=b.Kode_Perusahaan and a.No_faktur=b.No_Faktur and b.no_faktur=c.No_Faktur "
                        SQL = SQL & "and b.Kode_Barang=c.Kode_Barang and b.Kode_stock_owner=c.Kode_stock_owner and "
                        SQL = SQL & "c.Kode_kategori_biaya_import ='" & .Rows(index3).Item("Kode_kategori_biaya_import") & "' and c.Kode_Barang='" & LvKdBarang & "'  and a.Status Is null And a.id_rencana ='" & id_rencana & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then

                                freigt = freigt + Val(HilangkanTanda(Format(dr("Biaya") * LvJumlah, "N0")))
                                Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format(dr("Biaya_AVG") * LvJumlah, "N0")))

                            End If
                        End Using

                    Next
                End With

            End Using
        Next

        pib = Val(HilangkanTanda(Format(pib, "N0")))

        pph_billing = Val(HilangkanTanda(Format(pph_billing, "N0")))

        Storage = Val(HilangkanTanda(Format(Storage, "N0")))

        Billing = Val(HilangkanTanda(Format(Billing, "N0")))

        Tot_Pot_Stock_IDR = Val(HilangkanTanda(Format(Tot_Pot_Stock_IDR, "N0")))

        Tdk_Pot_Stock_IDR = Val(HilangkanTanda(Format(Tdk_Pot_Stock_IDR, "N0")))

        Tdk_Pot_Stock_Hutang_IDR_Utama = Val(HilangkanTanda(Format(Tdk_Pot_Stock_Hutang_IDR_Utama, "N0")))

        Tdk_Pot_Stock_Hutang_IDR_Penolong = Val(HilangkanTanda(Format(Tdk_Pot_Stock_Hutang_IDR_Penolong, "N0")))

        pph_pakai_persentase = Val(HilangkanTanda(Format(pph_pakai_persentase, "N0")))

        Selisih_PO = Val(HilangkanTanda(Format(Selisih_PO, "N0")))

        Selisih_PO_Biaya = Val(HilangkanTanda(Format(Selisih_PO_Biaya, "N0")))

        SQL = "select sum(Jumlah * hpp_satuan_display) as Biaya from EMI_Pembelian_Loading_detail "
        SQL = SQL & "where No_Faktur = '" & TxtNo_Loading.Text.Trim & "'"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                Hutang_Dalam_Proses = Dr("Biaya")
            Else
                Dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show(" Nilai Hutang Dalam Proses Tidak ditemukan")
                Exit Sub
            End If
        End Using

        For index As Integer = 0 To Arr_Biaya_Import_Master.Count - 1

            SQL = "select* from Detail_Account_Master where "
            SQL = SQL & "Lokasi = '" & Lokasi_Jurnal & "' and Kode_master_Kategori_biaya_import = '" & Arr_Biaya_Import_Master.Item(index) & "' "
            SQL = SQL & "and Akun_1 ='" & Arr_Akun1.Item(index) & "'  and Akun_2 = '" & Arr_Akun2.Item(index) & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Akun " & Arr_Biaya_Import_Master.Item(index) & " Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End Using

        Next

        For index As Integer = 0 To Arr_Biaya_Bongkar_Import_Master.Count - 1

            SQL = "select* from Detail_Account_Master where "
            SQL = SQL & "Lokasi = '" & Lokasi_Jurnal & "' and Kode_master_Kategori_biaya_import = '" & Arr_Biaya_Bongkar_Import_Master.Item(index) & "' "
            SQL = SQL & "and Akun_1 ='" & Arr_Akun1_Bongkar.Item(index) & "'  and Akun_2 = '" & Arr_Akun2_Bongkar.Item(index) & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Akun " & Arr_Biaya_Bongkar_Import_Master.Item(index) & " Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End Using

        Next

        If PPN <> 0 Then
            Biaya_PPN = HilangkanTanda(Format(Hutang_Dalam_Proses * PPN / 100, "N0"))
        End If

        If Flag_Average_Sup = "Y" Then
            Selisih_Import_AVG = Biaya_Import_AVG - (Biaya_Import_Total + freigt)
        End If

        Selisih_Hutang = (Hutang_Dalam_Proses + Biaya_PPN + pph_billing) - (Biaya_Import_Total + Selisih_Import_AVG + Billing + Storage + freigt + pph_pakai_persentase + Tot_Pot_Stock_IDR + Tdk_Pot_Stock_IDR + Tdk_Pot_Stock_Hutang_IDR_Utama + Tdk_Pot_Stock_Hutang_IDR_Penolong + pib + (Biaya_PPN - pib) + pph_billing + Selisih_PO + Selisih_PO_Biaya)
        Selisih_Hutang = Val(HilangkanTanda(Format(Selisih_Hutang, "N0")))

        Dim Kode_voucher As String = ""
        Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)

        Dim kode_voucher2_ As String = "NULL"
        Dim Kode_Voucher2 As String = ""

        Dim sudah_jurnal As Boolean = False

        Dim pagenumber As Integer = 1
        Dim pagenumber2 As Integer = 1

        SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
        SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
        SQL = SQL & "'" & Kode_voucher & "', "
        SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
        SQL = SQL & "'" & Format(CDate(tgl_skg), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
        SQL = SQL & "'" & KodeProyek & "', 'Pembelian " & Txt_NoFaktur.Text & "', '', "
        SQL = SQL & "'-', '" & UserID & "')"
        ExecuteTrans(SQL)

        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Hutang_Dalam_Proses, 1),
                    Strings.Mid(coa_Hutang_Dalam_Proses, 2, 1),
                    Strings.Mid(Ganti(coa_Hutang_Dalam_Proses), 3),
                    KodePerusahaan, KodeProyek, ket, Hutang_Dalam_Proses, "0", pagenumber, "BELUM")
        ExecuteTrans(SQL)
        pagenumber = pagenumber + 1

        If PPN <> 0 Then 'ada ppn
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Hutang_Dalam_Proses, 1),
                      Strings.Mid(coa_Hutang_Dalam_Proses, 2, 1),
                      Strings.Mid(Ganti(coa_Hutang_Dalam_Proses), 3),
                      KodePerusahaan, KodeProyek, ket, Biaya_PPN, "0", pagenumber, "BELUM")
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1
        End If

        If pph_billing <> 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_pph_billing, 1),
                     Strings.Mid(coa_pph_billing, 2, 1),
                     Strings.Mid(Ganti(coa_pph_billing), 3),
                     KodePerusahaan, KodeProyek, ket, pph_billing, "0", pagenumber, "BELUM")
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1
        End If

        For index As Integer = 0 To Arr_Biaya_Import_Master.Count - 1
            If Val(Arr_Biaya_Import.Item(index)) <> 0 Then
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Arr_Akun2.Item(index), 1),
                     Strings.Mid(Arr_Akun2.Item(index), 2, 1),
                     Strings.Mid(Ganti(Arr_Akun2.Item(index)), 3),
                     KodePerusahaan, KodeProyek, ket & "; " & Arr_Biaya_Import_Kategori.Item(index), "0", Arr_Biaya_Import.Item(index), pagenumber, "BELUM")
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', "
                SQL = SQL & "'" & Arr_Biaya_Import_Kategori.Item(index) & "', '" & Arr_Biaya_Import.Item(index) & "', "
                SQL = SQL & "'" & Arr_Akun2.Item(index) & "')"
                ExecuteTrans(SQL)
            End If
        Next

        For index As Integer = 0 To Arr_Biaya_Bongkar_Import_Master.Count - 1
            Dim ket2 As String = Strings.Left(ket & "; " & Arr_Biaya_Bongkar_Import_Kategori.Item(index) & "-" & Arr_Lokasi_Bongkar_Import.Item(index), 180)
            If Arr_Biaya_Bongkar_Import.Item(index) <> 0 Then
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Arr_Akun2_Bongkar.Item(index), 1),
                      Strings.Mid(Arr_Akun2_Bongkar.Item(index), 2, 1),
                      Strings.Mid(Ganti(Arr_Akun2_Bongkar.Item(index)), 3),
                      KodePerusahaan, KodeProyek, ket2, "0", Arr_Biaya_Bongkar_Import.Item(index), pagenumber, "BELUM")
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', "
                SQL = SQL & "'" & Arr_Biaya_Bongkar_Import_Kategori.Item(index) & "-" & Arr_Lokasi_Bongkar_Import.Item(index) & "', '" & Arr_Biaya_Bongkar_Import.Item(index) & "', "
                SQL = SQL & "'" & Arr_Akun2_Bongkar.Item(index) & "')"
                ExecuteTrans(SQL)
            End If

        Next

        If Selisih_Import_AVG > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Selisih_AVG_Import, 1),
                    Strings.Mid(coa_Selisih_AVG_Import, 2, 1),
                    Strings.Mid(Ganti(coa_Selisih_AVG_Import), 3),
                    KodePerusahaan, KodeProyek, ket, "0", Selisih_Import_AVG, pagenumber, "BELUM")
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1
        End If

        If Selisih_Import_AVG < 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Selisih_AVG_Import, 1),
                    Strings.Mid(coa_Selisih_AVG_Import, 2, 1),
                    Strings.Mid(Ganti(coa_Selisih_AVG_Import), 3),
                    KodePerusahaan, KodeProyek, ket, Math.Abs(Selisih_Import_AVG), "0", pagenumber, "BELUM")
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1
        End If

        If Billing > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Billing, 1),
                    Strings.Mid(coa_Billing, 2, 1),
                    Strings.Mid(Ganti(coa_Billing), 3),
                    KodePerusahaan, KodeProyek, ket, "0", Billing, pagenumber, "BELUM")
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', "
            SQL = SQL & "'BILLING', '" & Billing & "', "
            SQL = SQL & "'" & coa_Billing & "')"
            ExecuteTrans(SQL)

        End If

        If Storage > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Storage, 1),
                    Strings.Mid(coa_Storage, 2, 1),
                    Strings.Mid(Ganti(coa_Storage), 3),
                    KodePerusahaan, KodeProyek, ket & "; STORAGE", "0", Storage, pagenumber, "BELUM")
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', "
            SQL = SQL & "'STORAGE', '" & Storage & "', "
            SQL = SQL & "'" & coa_Storage & "')"
            ExecuteTrans(SQL)

        End If

        If freigt > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_freigt, 1),
                    Strings.Mid(coa_freigt, 2, 1),
                    Strings.Mid(Ganti(coa_freigt), 3),
                    KodePerusahaan, KodeProyek, ket, "0", freigt, pagenumber, "BELUM")
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', "
            SQL = SQL & "'FREIGHT', '" & freigt & "', "
            SQL = SQL & "'" & coa_freigt & "')"
            ExecuteTrans(SQL)

        End If

        If pph_pakai_persentase > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_pph, 1),
                Strings.Mid(coa_pph, 2, 1),
                Strings.Mid(Ganti(coa_pph), 3),
                KodePerusahaan, KodeProyek, ket, "0", pph_pakai_persentase, pagenumber, "BELUM")
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', "
            SQL = SQL & "'PPH29', '" & pph_pakai_persentase & "', "
            SQL = SQL & "'" & coa_pph & "')"
            ExecuteTrans(SQL)

        End If

        'zzzz

        If Tot_Pot_Stock_IDR > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Tot_Pot_Stock_IDR, 1),
                    Strings.Mid(coa_Tot_Pot_Stock_IDR, 2, 1),
                    Strings.Mid(Ganti(coa_Tot_Pot_Stock_IDR), 3),
                    KodePerusahaan, KodeProyek, ket, "0", Tot_Pot_Stock_IDR, pagenumber, "BELUM")
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', "
            SQL = SQL & "'Total Potong Stock', '" & Tot_Pot_Stock_IDR & "', "
            SQL = SQL & "'" & coa_Tot_Pot_Stock_IDR & "')"
            ExecuteTrans(SQL)

        End If

        'SQL = "select kode_perusahaan from detail_jurnal where Kode_Voucher = '" & Kode_Voucher & "' "
        'SQL = SQL & "and Kode_Master_Acc+Kode_Acc+Kode_Detail_Acc = '" & coa_Tot_Pot_Stock_IDR & "'"
        'Using Dr = OpenTrans(SQL)
        '    If Dr.Read Then
        '        Dr.Close()

        '        SQL = "update Detail_Jurnal set Kredit = Kredit + " & Tot_Pot_Stock_IDR & " "
        '        SQL = SQL & "where Kode_Voucher = '" & Kode_Voucher & "' "
        '        SQL = SQL & "and Kode_Master_Acc+Kode_Acc+Kode_Detail_Acc = '" & coa_Tot_Pot_Stock_IDR & "' "
        '        ExecuteTrans(SQL)
        '    Else
        '        If Tot_Pot_Stock_IDR > 0 Then
        '            Dr.Close()
        '            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_Tot_Pot_Stock_IDR, 1), _
        '                    Strings.Mid(coa_Tot_Pot_Stock_IDR, 2, 1), _
        '                    Strings.Mid(Ganti(coa_Tot_Pot_Stock_IDR), 3), _
        '                    KodePerusahaan, KodeProyek, ket, "0", Tot_Pot_Stock_IDR, pagenumber, "BELUM")
        '            ExecuteTrans(SQL)
        '            pagenumber = pagenumber + 1
        '        End If
        '    End If
        'End Using

        If Tdk_Pot_Stock_IDR > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Tdk_Pot_Stock_IDR, 1),
                    Strings.Mid(coa_Tdk_Pot_Stock_IDR, 2, 1),
                    Strings.Mid(Ganti(coa_Tdk_Pot_Stock_IDR), 3),
                    KodePerusahaan, KodeProyek, ket, "0", Tdk_Pot_Stock_IDR, pagenumber, "BELUM")
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', "
            SQL = SQL & "'Tidak Potong Stock Lunas', '" & Tdk_Pot_Stock_IDR & "', "
            SQL = SQL & "'" & coa_Tdk_Pot_Stock_IDR & "')"
            ExecuteTrans(SQL)

        End If

        'SQL = "select kode_perusahaan from detail_jurnal where Kode_Voucher = '" & Kode_Voucher & "' "
        'SQL = SQL & "and Kode_Master_Acc+Kode_Acc+Kode_Detail_Acc = '" & coa_Tdk_Pot_Stock_IDR & "'"
        'Using Dr = OpenTrans(SQL)
        '    If Dr.Read Then
        '        Dr.Close()

        '        SQL = "update Detail_Jurnal set Kredit = Kredit + " & Tdk_Pot_Stock_IDR & " "
        '        SQL = SQL & "where Kode_Voucher = '" & Kode_Voucher & "' "
        '        SQL = SQL & "and Kode_Master_Acc+Kode_Acc+Kode_Detail_Acc = '" & coa_Tdk_Pot_Stock_IDR & "' "
        '        ExecuteTrans(SQL)
        '    Else
        '        If Tdk_Pot_Stock_IDR > 0 Then
        '            Dr.Close()

        '            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_Tdk_Pot_Stock_IDR, 1), _
        '                    Strings.Mid(coa_Tdk_Pot_Stock_IDR, 2, 1), _
        '                    Strings.Mid(Ganti(coa_Tdk_Pot_Stock_IDR), 3), _
        '                    KodePerusahaan, KodeProyek, ket, "0", Tdk_Pot_Stock_IDR, pagenumber, "BELUM")
        '            ExecuteTrans(SQL)
        '            pagenumber = pagenumber + 1
        '        End If
        '    End If
        'End Using

        If Tdk_Pot_Stock_Hutang_IDR_Utama > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Tdk_Pot_Stock_Hutang_IDR_Utama, 1),
                    Strings.Mid(coa_Tdk_Pot_Stock_Hutang_IDR_Utama, 2, 1),
                    Strings.Mid(Ganti(coa_Tdk_Pot_Stock_Hutang_IDR_Utama), 3),
                    KodePerusahaan, KodeProyek, ket, "0", Tdk_Pot_Stock_Hutang_IDR_Utama, pagenumber, "BELUM")
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', "
            SQL = SQL & "'Tidak Potong Stock Bahan Utama Hutang', '" & Tdk_Pot_Stock_Hutang_IDR_Utama & "', "
            SQL = SQL & "'" & coa_Tdk_Pot_Stock_Hutang_IDR_Utama & "')"
            ExecuteTrans(SQL)

        End If

        'SQL = "select kode_perusahaan from detail_jurnal where Kode_Voucher = '" & Kode_Voucher & "' "
        'SQL = SQL & "and Kode_Master_Acc+Kode_Acc+Kode_Detail_Acc = '" & coa_Tdk_Pot_Stock_Hutang_IDR_Utama & "'"
        'Using Dr = OpenTrans(SQL)
        '    If Dr.Read Then
        '        Dr.Close()

        '        SQL = "update Detail_Jurnal set Kredit = Kredit + " & Tdk_Pot_Stock_Hutang_IDR_Utama & " "
        '        SQL = SQL & "where Kode_Voucher = '" & Kode_Voucher & "' "
        '        SQL = SQL & "and Kode_Master_Acc+Kode_Acc+Kode_Detail_Acc = '" & coa_Tdk_Pot_Stock_Hutang_IDR_Utama & "' "
        '        ExecuteTrans(SQL)
        '    Else
        '        If Tdk_Pot_Stock_Hutang_IDR_Utama > 0 Then
        '            Dr.Close()
        '            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_Tdk_Pot_Stock_Hutang_IDR_Utama, 1), _
        '                    Strings.Mid(coa_Tdk_Pot_Stock_Hutang_IDR_Utama, 2, 1), _
        '                    Strings.Mid(Ganti(coa_Tdk_Pot_Stock_Hutang_IDR_Utama), 3), _
        '                    KodePerusahaan, KodeProyek, ket, "0", Tdk_Pot_Stock_Hutang_IDR_Utama, pagenumber, "BELUM")
        '            ExecuteTrans(SQL)
        '            pagenumber = pagenumber + 1
        '        End If
        '    End If
        'End Using

        If Tdk_Pot_Stock_Hutang_IDR_Penolong > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Tdk_Pot_Stock_Hutang_IDR_Penolong, 1),
                    Strings.Mid(coa_Tdk_Pot_Stock_Hutang_IDR_Penolong, 2, 1),
                    Strings.Mid(Ganti(coa_Tdk_Pot_Stock_Hutang_IDR_Penolong), 3),
                    KodePerusahaan, KodeProyek, ket, "0", Tdk_Pot_Stock_Hutang_IDR_Penolong, pagenumber, "BELUM")
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', "
            SQL = SQL & "'Tidak Potong Stock Bahan Penolong Hutang', '" & Tdk_Pot_Stock_Hutang_IDR_Penolong & "', "
            SQL = SQL & "'" & coa_Tdk_Pot_Stock_Hutang_IDR_Penolong & "')"
            ExecuteTrans(SQL)

        End If

        'SQL = "select kode_perusahaan from detail_jurnal where Kode_Voucher = '" & Kode_Voucher & "' "
        'SQL = SQL & "and Kode_Master_Acc+Kode_Acc+Kode_Detail_Acc = '" & coa_Tdk_Pot_Stock_Hutang_IDR_Penolong & "'"
        'Using Dr = OpenTrans(SQL)
        '    If Dr.Read Then
        '        Dr.Close()

        '        SQL = "update Detail_Jurnal set Kredit = Kredit + " & Tdk_Pot_Stock_Hutang_IDR_Penolong & " "
        '        SQL = SQL & "where Kode_Voucher = '" & Kode_Voucher & "' "
        '        SQL = SQL & "and Kode_Master_Acc+Kode_Acc+Kode_Detail_Acc = '" & coa_Tdk_Pot_Stock_Hutang_IDR_Penolong & "' "
        '        ExecuteTrans(SQL)
        '    Else
        '        If Tdk_Pot_Stock_Hutang_IDR_Penolong > 0 Then
        '            Dr.Close()
        '            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_Tdk_Pot_Stock_Hutang_IDR_Penolong, 1), _
        '                    Strings.Mid(coa_Tdk_Pot_Stock_Hutang_IDR_Penolong, 2, 1), _
        '                    Strings.Mid(Ganti(coa_Tdk_Pot_Stock_Hutang_IDR_Penolong), 3), _
        '                    KodePerusahaan, KodeProyek, ket, "0", Tdk_Pot_Stock_Hutang_IDR_Penolong, pagenumber, "BELUM")
        '            ExecuteTrans(SQL)
        '            pagenumber = pagenumber + 1
        '        End If
        '    End If
        'End Using

        If Selisih_PO <> 0 Then
            If Selisih_PO < 0 Then
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Selisih_PO, 1),
                        Strings.Mid(coa_Selisih_PO, 2, 1),
                        Strings.Mid(Ganti(coa_Selisih_PO), 3),
                        KodePerusahaan, KodeProyek, ket, Math.Abs(Selisih_PO), "0", pagenumber, "BELUM")
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1
            Else
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Selisih_PO, 1),
                        Strings.Mid(coa_Selisih_PO, 2, 1),
                        Strings.Mid(Ganti(coa_Selisih_PO), 3),
                        KodePerusahaan, KodeProyek, ket, "0", Selisih_PO, pagenumber, "BELUM")
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

            End If
            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', "
            SQL = SQL & "'Selisih PO', '" & Selisih_PO & "', "
            SQL = SQL & "'" & coa_Selisih_PO & "')"
            ExecuteTrans(SQL)
        End If

        If Selisih_PO_Biaya <> 0 Then
            If Selisih_PO_Biaya < 0 Then
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Selisih_PO_Biaya, 1),
                        Strings.Mid(coa_Selisih_PO_Biaya, 2, 1),
                        Strings.Mid(Ganti(coa_Selisih_PO_Biaya), 3),
                        KodePerusahaan, KodeProyek, ket, Math.Abs(Selisih_PO_Biaya), "0", pagenumber, "BELUM")
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1
            Else
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Selisih_PO_Biaya, 1),
                        Strings.Mid(coa_Selisih_PO_Biaya, 2, 1),
                        Strings.Mid(Ganti(coa_Selisih_PO_Biaya), 3),
                        KodePerusahaan, KodeProyek, ket, "0", Selisih_PO_Biaya, pagenumber, "BELUM")
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

            End If

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', "
            SQL = SQL & "'Selisih PO Biaya', '" & Selisih_PO_Biaya & "', "
            SQL = SQL & "'" & coa_Selisih_PO_Biaya & "')"
            ExecuteTrans(SQL)
        End If

        If Selisih_Hutang <> 0 Then
            If Selisih_Hutang < 0 Then
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Selisih_Hutang_Import, 1),
                        Strings.Mid(coa_Selisih_Hutang_Import, 2, 1),
                        Strings.Mid(Ganti(coa_Selisih_Hutang_Import), 3),
                        KodePerusahaan, KodeProyek, ket, Math.Abs(Selisih_Hutang), "0", pagenumber, "BELUM")
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1
            Else
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Selisih_Hutang_Import, 1),
                        Strings.Mid(coa_Selisih_Hutang_Import, 2, 1),
                        Strings.Mid(Ganti(coa_Selisih_Hutang_Import), 3),
                        KodePerusahaan, KodeProyek, ket, "0", Selisih_Hutang, pagenumber, "BELUM")
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

            End If
            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', "
            SQL = SQL & "'Selisih Hutang', '" & Selisih_Hutang & "', "
            SQL = SQL & "'" & coa_Selisih_Hutang_Import & "')"
            ExecuteTrans(SQL)
        End If

        If pib > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_pib, 1),
                       Strings.Mid(coa_pib, 2, 1),
                       Strings.Mid(Ganti(coa_pib), 3),
                       KodePerusahaan, KodeProyek, ket, "0", pib, pagenumber, "BELUM")
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', "
            SQL = SQL & "'PIB', '" & pib & "', "
            SQL = SQL & "'" & coa_pib & "')"
            ExecuteTrans(SQL)

        End If

        'Metode_Hitung_Konte = Dr("Metode_Hitung_Konte")
        'coa_Hutang_Dalam_Proses = Dr("Hutang_Dalam_Proses")
        'coa_Selisih_Hutang_Import = Dr("Selisih_Hutang_Import")
        'coa_Billing = Dr("Hutang_Billing_Import")
        'coa_freigt = Dr("Hutang_Freight_Import")
        'coa_Storage = Dr("Hutang_Storage_Import")
        'coa_Tot_Pot_Stock_IDR = Dr("Akun_Tot_Pot_Stock")
        'coa_Tdk_Pot_Stock_IDR = Dr("Akun_Tdk_Pot_Stock")
        'coa_Tdk_Pot_Stock_Hutang_IDR_Utama = Dr("Akun_Tdk_Pot_Stock_Hutang_Utama")
        'coa_Tdk_Pot_Stock_Hutang_IDR_Penolong = Dr("Akun_Tdk_Pot_Stock_Hutang_Penolong")
        'coa_pph = Dr("Akun_pph")
        'coa_pib = Dr("akun_pib")
        'coa_selisih_pib = Dr("akun_selisih_pib")
        'coa_pph_billing = Dr("pph_billing")
        'coa_hutang_pph_billing = Dr("hutang_pph_billing")

        If Biaya_PPN - pib <> 0 Then 'ini <> 0 supaya kl minus tetep masuk & masuk jebakan
            If Biaya_PPN - pib < 0 Then
                'CloseTrans()
                'CloseConn()
                'MessageBox.Show("Terjadi kesalahan di jurnal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'Exit Sub

                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_selisih_pib, 1),
                    Strings.Mid(coa_selisih_pib, 2, 1),
                    Strings.Mid(Ganti(coa_selisih_pib), 3),
                    KodePerusahaan, KodeProyek, ket, -(Biaya_PPN - pib), "0", pagenumber, "BELUM")
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', "
                SQL = SQL & "'Selisih PIB', '" & -(Biaya_PPN - pib) & "', "
                SQL = SQL & "'" & coa_selisih_pib & "')"
                ExecuteTrans(SQL)
            Else
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_selisih_pib, 1),
                    Strings.Mid(coa_selisih_pib, 2, 1),
                    Strings.Mid(Ganti(coa_selisih_pib), 3),
                    KodePerusahaan, KodeProyek, ket, "0", Biaya_PPN - pib, pagenumber, "BELUM")
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', "
                SQL = SQL & "'Selisih PIB', '" & Biaya_PPN - pib & "', "
                SQL = SQL & "'" & coa_selisih_pib & "')"
                ExecuteTrans(SQL)

            End If

            'If Biaya_PPN - pib < 0 Then
            '    CloseTrans()
            '    CloseConn()
            '    MessageBox.Show("Terjadi kesalahan di jurnal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If

            'SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_selisih_pib, 1), _
            '         Strings.Mid(coa_selisih_pib, 2, 1), _
            '         Strings.Mid(Ganti(coa_selisih_pib), 3), _
            '         KodePerusahaan, KodeProyek, ket, "0", Biaya_PPN - pib, pagenumber, "BELUM")
            'ExecuteTrans(SQL)
            'pagenumber = pagenumber + 1

            'SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            'SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
            'SQL = SQL & "'Selisih PIB', '" & Biaya_PPN - pib & "', "
            'SQL = SQL & "'" & coa_selisih_pib & "')"
            'ExecuteTrans(SQL)

        End If

        If pph_billing <> 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_hutang_pph_billing, 1),
                     Strings.Mid(coa_hutang_pph_billing, 2, 1),
                     Strings.Mid(Ganti(coa_hutang_pph_billing), 3),
                     KodePerusahaan, KodeProyek, ket, "0", pph_billing, pagenumber, "BELUM")
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "', "
            SQL = SQL & "'PPH BILLING', '" & pph_billing & "', "
            SQL = SQL & "'" & coa_hutang_pph_billing & "')"
            ExecuteTrans(SQL)

        End If

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

        If Selisih_PO + Selisih_PO_Biaya <> 0 Then

            If sudah_jurnal = False Then
                Kode_Voucher2 = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), fJU & fValPemb, KodePerusahaan)
                kode_voucher2_ = "'" & Kode_Voucher2 & "'"

                SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                SQL = SQL & "'" & Kode_Voucher2 & "', "
                SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(CDate(tgl_skg), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                SQL = SQL & "'" & KodeProyek & "', 'Pembelian " & Txt_NoFaktur.Text.Trim & "', '', "
                SQL = SQL & "'-', '" & UserID & "')"
                ExecuteTrans(SQL)

                sudah_jurnal = True
            End If

            'voucher selisih sebelum
            Dim total_selish As Double = Selisih_PO + Selisih_PO_Biaya
            If total_selish > 0 Then
                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_selisih_new & "' and kredit <> 0"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set kredit = kredit+ " & total_selish & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_selisih_new & "' and kredit <> 0"
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert
                        pagenumber2 += 1
                        SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_selisih_new, 1),
                              Strings.Mid(coa_selisih_new, 2, 1),
                              Strings.Mid(Ganti(coa_selisih_new), 3),
                              KodePerusahaan, KodeProyek, ket, "0", total_selish, pagenumber2, "BELUM")
                        ExecuteTrans(SQL)
                    End If
                End Using

                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Tdk_Pot_Stock_Hutang_IDR_Utama & "' and debit <> 0"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set debit = debit+ " & Math.Abs(total_selish) & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Tdk_Pot_Stock_Hutang_IDR_Utama & "' and debit <> 0"
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert
                        pagenumber2 += 1
                        SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_Tdk_Pot_Stock_Hutang_IDR_Utama, 1),
                              Strings.Mid(coa_Tdk_Pot_Stock_Hutang_IDR_Utama, 2, 1),
                              Strings.Mid(Ganti(coa_Tdk_Pot_Stock_Hutang_IDR_Utama), 3),
                              KodePerusahaan, KodeProyek, ket, Math.Abs(total_selish), "0", pagenumber2, "BELUM")
                        ExecuteTrans(SQL)
                    End If
                End Using
            Else
                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_selisih_new & "' and debit <> 0"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set debit = debit+ " & Math.Abs(total_selish) & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_selisih_new & "' and debit <> 0"
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert
                        pagenumber2 += 1
                        SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_selisih_new, 1),
                              Strings.Mid(coa_selisih_new, 2, 1),
                              Strings.Mid(Ganti(coa_selisih_new), 3),
                              KodePerusahaan, KodeProyek, ket, Math.Abs(total_selish), "0", pagenumber2, "BELUM")
                        ExecuteTrans(SQL)
                    End If
                End Using

                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Tdk_Pot_Stock_Hutang_IDR_Utama & "' and kredit <> 0"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set kredit = kredit+ " & Math.Abs(total_selish) & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Tdk_Pot_Stock_Hutang_IDR_Utama & "' and kredit <> 0"
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert
                        pagenumber2 += 1
                        SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_Tdk_Pot_Stock_Hutang_IDR_Utama, 1),
                              Strings.Mid(coa_Tdk_Pot_Stock_Hutang_IDR_Utama, 2, 1),
                              Strings.Mid(Ganti(coa_Tdk_Pot_Stock_Hutang_IDR_Utama), 3),
                              KodePerusahaan, KodeProyek, ket, "0", Math.Abs(total_selish), pagenumber2, "BELUM")
                        ExecuteTrans(SQL)
                    End If
                End Using
            End If

        End If

        If Kode_Voucher2 <> "" Then
            SQL = "select round(sum(debit), 2) as debit, round(sum(kredit), 2) as kredit from detail_jurnal where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("debit") <> Dr("kredit") Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Jurnal 2 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data jurnal 2 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using
        End If

        SQL = "Update EMI_Timbang_Unloading "
        SQL = SQL & "Set Kode_Voucher = '" & Kode_voucher & "' "
        SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
        SQL = SQL & "and No_Faktur = '" & Txt_NoFaktur.Text & "' "
        ExecuteTrans(SQL)

        isError = True
    End Sub

    Private Sub Jurnal_Lokal()
        Dim inisial_faktur_dari As String = ""
        Dim lokasi_Barang As String = ""

        SQL = "select kode_stock_owner from EMI_Barang_Masuk_Perpallet a where "
        SQL = SQL & "Kode_Perusahaan='" & KodePerusahaan & "' and "
        SQL = SQL & "No_Pembelian_Loading='" & TxtNo_Loading.Text & "' and status is null "
        ' SQL = SQL & "and Flag_Timbang_Keluar is null "
        Using dr = OpenTrans(SQL)
            If dr.Read Then
                lokasi_Barang = dr("kode_stock_owner")
            Else
                dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Data Tidak ditemukan . . ! !")
                Exit Sub
            End If
        End Using

        SQL = "select inisial_faktur from stock_owner_gudang "
        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & lokasi_Barang & "' "
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                'akun_persediaan_dari = Dr("persediaan")
                inisial_faktur_dari = Dr("inisial_faktur")
            Else
                Dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End Using

        Dim flag_import As String = ""
        Dim flag_HPP As String = ""
        SQL = "Select Flag_Import, Flag_Import_HPP from EMI_Pembelian_Loading "
        SQL = SQL & "where No_Faktur='" & TxtNo_Loading.Text & "' "
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then

                flag_HPP = General_Class.CekNULL(Dr("Flag_Import_HPP"))
                flag_import = General_Class.CekNULL(Dr("Flag_Import"))
            Else
                Dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End Using

        Dim Kode_voucher As String = ""
        Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
        Dim pagenumber As Integer = 1

        SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
        SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
        SQL = SQL & "'" & Kode_voucher & "', "
        SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
        SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
        SQL = SQL & "'" & KodeProyek & "', 'Barang Masuk " & Txt_NoFaktur.Text & "', '', "
        SQL = SQL & "'-', '" & UserID & "')"
        ExecuteTrans(SQL)

        For index = 0 To DgvPO.Rows.Count - 1
            Get_Isi_DataGridView(index)

            Dim akun_persediaan_dari As String = ""
            Dim akun_ppn As String = ""
            Dim akun_hutang As String = ""

            SQL = "select c.akun_Persediaan "
            SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
            SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.kode_stock_owner = '" & lokasi_Barang & "' and b.Kode_Barang='" & LvKdBarang & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    akun_persediaan_dari = Dr("akun_Persediaan")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select inisial_faktur, hutang, PPN_Pembelian "
            SQL = SQL & "from stock_owner_gudang "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & lokasi_Barang & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    akun_hutang = Dr("hutang")
                    akun_ppn = Dr("PPN_Pembelian")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim Harga As Double = 0
            Dim PPN As Double = 0
            SQL = "Select (case when a.flag_refraksi Is null then b.Harga_barang else a.Harga_Refraksi end) as harga, c.PPN, a.Flag_Permintaan_Refraksi "
            SQL = SQL & "From EMI_Pembelian_Loading_Detail a, EMI_Pembelian_PO_Detail b, EMI_Pembelian_PO c Where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.Urut_PO = b.No_Urut And "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.No_Faktur = c.No_Faktur and c.status is null And "
            SQL = SQL & "Urut_Oto = '" & LvUrutLoading & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then

                    If General_Class.CekNULL(dr("Flag_Permintaan_Refraksi")) = "Y" Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terdapat Data yang Harus di Refraksi . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If flag_import = "Y" Then

                        If flag_HPP = "" Then
                            Harga = 0
                            PPN = 0
                        Else
                            Harga = dr("Harga")
                            PPN = dr("PPN")
                        End If
                    Else
                        Harga = dr("Harga")
                        PPN = dr("PPN")
                    End If
                Else
                    dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("PO Tidak ditemukan . . ! !")
                    Exit Sub
                End If
            End Using

            'If Val(HilangkanTanda(LvJumlahMasuk)) = 0 Then
            '    MessageBox.Show("Jumlah masuk harus di isi")
            '    CloseTrans()
            '    CloseConn()
            '    Exit Sub
            'End If

            Dim jmlhMasuk As Double = Val(LvJumlahMasuk)
            Dim Satuan As String = LvSatuan

            Dim jumlah_masuk_Barang As Double = 0
            Dim Satuan_Barang As String = ""

            SQL = "select distinct Satuan from Barang where "
            SQL = SQL & "Kode_Barang='" & LvKdBarang & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
            Using dr2 = OpenTrans(SQL)
                If dr2.Read Then
                    Satuan_Barang = dr2("Satuan")
                Else
                    dr2.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Barang Tidak ditemukan . . ! !")
                    Exit Sub
                End If
            End Using

            'UBAH KE SATUAN PO
            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & LvKdBarang & "',"
            SQL = SQL & "'" & Satuan & "','" & Satuan_Barang & "',"
            SQL = SQL & "" & jmlhMasuk & ") as Hasil "
            Using dr3 = OpenTrans(SQL)
                If dr3.Read Then
                    If General_Class.CekNULL(dr3("Hasil")) <> "" Then
                        jumlah_masuk_Barang = dr3("Hasil")
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Satuan " & Satuan & " Ke " & Satuan_Barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
            End Using

            Dim TotalHPP As Double = Math.Round(jumlah_masuk_Barang * Harga)
            Dim Nilai_PPN As Double = Math.Round(TotalHPP * PPN / 100)

            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_persediaan_dari & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'update

                    SQL = "update detail_jurnal set debit = debit+ " & TotalHPP & " where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_persediaan_dari & "' "
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    'insert

                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
                          Strings.Mid(akun_persediaan_dari, 2, 1),
                          Strings.Mid(Ganti(akun_persediaan_dari), 3),
                          KodePerusahaan, KodeProyek, "Persedian " & Txt_NoFaktur.Text, TotalHPP, "0", pagenumber, "TSSS")
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                End If
            End Using

            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_ppn & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'update

                    SQL = "update detail_jurnal set debit = debit+ " & Nilai_PPN & " where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_ppn & "' "
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    'insert

                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_ppn, 1),
                          Strings.Mid(akun_ppn, 2, 1),
                          Strings.Mid(Ganti(akun_ppn), 3),
                          KodePerusahaan, KodeProyek, "PPN " & Txt_NoFaktur.Text, Nilai_PPN, "0", pagenumber, "TSSS")
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                End If
            End Using

            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'update

                    SQL = "update detail_jurnal set kredit = kredit+ " & Nilai_PPN + TotalHPP & " where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang & "' "
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    'insert

                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_hutang, 1),
                          Strings.Mid(akun_hutang, 2, 1),
                          Strings.Mid(Ganti(akun_hutang), 3),
                          KodePerusahaan, KodeProyek, "Hutang " & Txt_NoFaktur.Text, "0", Nilai_PPN + TotalHPP, pagenumber, "TSSS")
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                End If
            End Using
        Next

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

        SQL = "Update EMI_Timbang_Unloading "
        SQL = SQL & "Set Kode_Voucher = '" & Kode_voucher & "' "
        SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
        SQL = SQL & "and No_Faktur = '" & Txt_NoFaktur.Text & "' "
        ExecuteTrans(SQL)

        isError = True
    End Sub

    Private Sub LblSatuan_Click(sender As Object, e As EventArgs) Handles LblSatuan.Click

    End Sub

    Private Sub loadJenisMuatan()
        Try
            OpenConn()

            SQL = "select Id_Jenis_Muatan, Kode_Jenis_Muatan, Keterangan, Metode_Timbang "
            SQL = SQL & "from EMI_Master_Jenis_Muatan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    CmbJenisMuatan.Items.Add(Dr("Keterangan")) : arrIdJenisMuatan.Add(Dr("Id_Jenis_Muatan")) : arrMetodeTruckScale.Add(Dr("Metode_Timbang"))

                Loop
            End Using

            If jenisMasuk = "KELUAR" Then
                Dim idmuatan As String = ""

                SQL = "select timbang_masuk, id_jenis_muatan, tgl_timbang_masuk, Jam_Timbang_Masuk from EMI_Timbang_Unloading where "
                SQL = SQL & "no_faktur='" & Txt_NoFaktur.Text & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        idmuatan = Dr("id_jenis_muatan")
                        Txt_Timbang1.Text = Format(Dr("timbang_masuk"), "N2")
                        If General_Class.CekNULL(Dr("tgl_timbang_masuk")) = "" Then
                            DTP_Bruto.Value = DateTime.Now
                        Else
                            DTP_Bruto.Value = Convert.ToDateTime(Dr("tgl_timbang_masuk")).Date.Add(Convert.ToDateTime(Dr("Jam_Timbang_Masuk")).TimeOfDay)
                        End If
                    End If
                End Using

                For index = 0 To arrIdJenisMuatan.Count - 1
                    If arrIdJenisMuatan.Item(index) = idmuatan Then
                        CmbJenisMuatan.SelectedIndex = index
                        Exit For
                    End If
                Next

                CmbJenisMuatan.Enabled = False
                CmbBarang.Enabled = False

                Hitung_Netto()

            End If

            If jenisMasuk = "MASUK" Then

                '===================================
                '=     CEK APAKAH TIMBANG KE 2     =
                '===================================
                SQL = "select top 1 a.ID_Jenis_Muatan, c.Keterangan "
                SQL = SQL & "from EMI_Timbang_Unloading a, EMI_Pembelian_Loading b, EMI_Master_Jenis_Muatan c "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
                SQL = SQL & "and a.No_Loading = b.No_Faktur "
                SQL = SQL & "and a.ID_Jenis_Muatan = c.Id_Jenis_Muatan "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Loading = '" & TxtNo_Loading.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        CmbJenisMuatan.Enabled = False
                        CmbJenisMuatan.SelectedItem = Dr("Keterangan")
                    Else
                        Dr.Close()
                    End If
                End Using
            End If

            Dim nama_barang As String = ""
            Dim id As Integer = 0
            SQL = "select distinct a.kode_supplier, D.nama, b.Kode_Barang, C.nama as Nama_Barang, a.Lokasi, a.No_SJ, a.No_Plat, a.Driver "
            SQL = SQL & "from EMI_Pembelian_Loading a, EMI_Pembelian_Loading_Detail b, barang c, Suppliers d "
            SQL = SQL & "where a.kode_Perusahaan=b.kode_Perusahaan and a.no_faktur=b.no_faktur and a.status is null and "
            SQL = SQL & "b.kode_Barang=c.kode_Barang and b.kode_stock_Owner=c.Kode_Stock_Owner and b.kode_Perusahaan=c.kode_Perusahaan "
            SQL = SQL & "and a.kode_Perusahaan=d.Kode_Perusahaan and a.kode_Supplier=d.Kode_Supplier "
            SQL = SQL & "and a.kode_Perusahaan ='" & KodePerusahaan & "' and a.No_faktur='" & TxtNo_Loading.Text & "' "
            If jenisMasuk = "MASUK" Then
                SQL = SQL & "and b.Flag_Timbang_Masuk is null "
            ElseIf jenisMasuk = "KELUAR" Then
                SQL = SQL & "and b.Flag_Timbang_Keluar is null "
            End If
            SQL = SQL & "Order By d.nama "
            Using dr = OpenTrans(SQL)
                Do While dr.Read

                    If id = 0 Then
                        TxtNoSJ.Text = dr("No_SJ")
                        Txt_PlatNomor.Text = dr("No_Plat")
                        Txt_Supir.Text = dr("Driver")
                        Txt_Supplier.Text = dr("nama")
                        Lbl_KodeSupplier.Text = dr("kode_supplier")
                    End If

                    arrNamaBarang.Add(dr("Nama_Barang")) : arrKodeBarang.Add(dr("Kode_Barang"))
                    CmbBarang.Items.Add(dr("Nama_Barang"))
                    id += 1
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Tampil_Kamera()
        StreamPlayerControl1.Show()
        StreamPlayerControl2.Show()

        Try

            'If StreamPlayerControl1.IsPlaying = True Then
            '    StreamPlayerControl1.Stop()
            '    StreamPlayerControl2.Stop()
            'End If

            'SQL = "select User_IPCAM, Password_IPCAM, IPPORT_CAM from Emi_CAM"
            'Using dr = OpenTrans(SQL)
            '    Dim stream As Integer = 1
            '    Do While dr.Read
            '        Dim controlName As String = "StreamPlayerControl" & stream
            '        Dim control As Object = Me.GetType().GetField(controlName, Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(Me)

            '        If control IsNot Nothing Then
            '            control.StartPlay((New Uri("rtsp://" & dr("User_IPCAM") & ":" & dr("Password_IPCAM") & "@" & dr("IPPORT_CAM") & "/Streaming/channels/102/")))
            '            stream += 1
            '        End If

            '    Loop
            'End Using

            Dim user1 As String = "" : Dim pass1 As String = "" : Dim ipaddr1 As String = ""
            Dim user2 As String = "" : Dim pass2 As String = "" : Dim ipaddr2 As String = ""

            Try
                OpenConn()
                SQL = "select UserName, Password, IP_Address, CAM_Number from Emi_CAM"
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        If dr("CAM_Number") = "CAM 1" Then
                            user1 = dr("UserName")
                            pass1 = dr("Password")
                            ipaddr1 = dr("IP_Address")

                        ElseIf dr("CAM_Number") = "CAM 2" Then
                            user2 = dr("UserName")
                            pass2 = dr("Password")
                            ipaddr2 = dr("IP_Address")
                        End If
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
            End Try

            StreamPlayerControl1.StartPlay((New Uri("rtsp://" & user1 & ":" & pass1 & "@" & ipaddr1 & "/Streaming/channels/102/")))
            StreamPlayerControl2.StartPlay((New Uri("rtsp://" & user2 & ":" & pass2 & "@" & ipaddr2 & "/Streaming/channels/102/")))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then Txt_Supir.Focus()
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

    'Private Sub DataGridView1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DataGridView1.EditingControlShowing
    '    AddHandler e.Control.KeyPress, AddressOf TextBoxColumn3_KeyPress
    'End Sub
    Private Sub Tot_Bags_TextChanged(sender As Object, e As EventArgs) Handles Tot_Bags.TextChanged

    End Sub

    Private Sub Transaksi_Timbang_Unloading_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Transaksi_Timbang_Unloading_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Transaksi_Timbang_Unloading_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            DgvPO.Columns(ItemUrutPO).Visible = False
            DgvPO.Columns(ItemUrutLoading).Visible = False

            If jenisMasuk = "MASUK" Then
                Lbl_Judul.Text = "Transaksi - Timbang 1 " 'Base_Language.Lang_TransUnloading_Judul + " | " + Base_Language.Lang_Global_Bruto
                DgvPO.Columns(ItemJumlahMasuk).Visible = False
                DgvPO.Columns(ItemJumlahMasuk).ReadOnly = True
                DgvPO.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                Txt_Timbang1.Text = Txt_Timbangan.Text
                Txt_Timbang2.Enabled = False

                DTP_Bruto.Value = DateTime.Now
                DTP_Tara.Value = DateTime.Now

            ElseIf jenisMasuk = "KELUAR" Then
                Lbl_Judul.Text = "Transaksi - Timbang 2 " 'Base_Language.Lang_TransUnloading_Judul + " | " + Base_Language.Lang_Global_Tara
                DgvPO.Columns(ItemJumlahMasuk).Visible = True
                DgvPO.Columns(ItemJumlahMasuk).ReadOnly = False
                DgvPO.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                Txt_Timbang2.Text = Txt_Timbangan.Text
                Txt_Timbang1.Enabled = False

                SQL = "select No_Faktur from EMI_Timbang_Unloading a where "
                SQL = SQL & "kode_Perusahaan='" & KodePerusahaan & "' and no_loading='" & TxtNo_Loading.Text & "' "
                SQL = SQL & "and status is null " 'and flag_selesai is null  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Txt_NoFaktur.Text = Dr("No_Faktur")
                    End If
                End Using

                DTP_Tara.Value = DateTime.Now
            Else
                MessageBox.Show("Terjadi Kesalahan  . .  !")
                Exit Sub
            End If

            '================================
            '=     GET GUDANG UNLOADING     =
            '================================
            SQL = "select b.Kode_Stock_Owner from binding_lokasi_gudang a, stock_owner_gudang b "
            SQL = SQL & "where a.kode_stock_owner='" & Lokasi & "' and b.Kode_Stock_Owner=a.Kode_Stock_Owner_gudang "
            SQL = SQL & "and flag_unloading='Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    LokasiGudangUnloading = Dr("Kode_Stock_Owner")
                Else
                    Dr.Close()
                    MessageBox.Show("Lokasi Gudang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            loadJenisMuatan()
            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
            Lbl_Supplier.Text = Base_Language.Lang_Global_Supplier
            Lbl_Supir.Text = Base_Language.Lang_Global_Supir
            Lbl_PlatNomor.Text = Base_Language.Lang_Global_PlatNomor
            Lbl_Timbang1.Text = "Timbang 1"
            Lbl_Timbang2.Text = "Timbang 2"
            Lbl_FotoKendaraan.Text = Base_Language.Lang_Global_FotoKendaraan

            ListView2.Columns.Clear()
            ListView2.Columns.Add("No SJ", 160, HorizontalAlignment.Left)
            ListView2.Columns.Add("No PO", 160, HorizontalAlignment.Left)
            ListView2.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try

        If jenisMasuk = "MASUK" Then
            Get_DGVMasuk()
        ElseIf jenisMasuk = "KELUAR" Then
            Get_DGVKeluar()
        End If

        'kosong()
        Tampil_Kamera()
    End Sub
    Private Sub Txt_Timbang1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Timbang1.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Txt_Timbang1_Leave(sender As Object, e As EventArgs) Handles Txt_Timbang1.Leave
        For i As Integer = 0 To DgvPO.RowCount - 1
            DgvPO.Rows(i).Cells(ItemJumlahMasuk).Value = 0
        Next
    End Sub

    Private Sub Txt_Timbang1_TextChanged(sender As Object, e As EventArgs) Handles Txt_Timbang1.TextChanged
        Hitung_Netto()

    End Sub

    Private Sub Txt_Timbang2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Timbang2.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Txt_Timbang2_Leave(sender As Object, e As EventArgs) Handles Txt_Timbang2.Leave
        For i As Integer = 0 To DgvPO.RowCount - 1
            DgvPO.Rows(i).Cells(ItemJumlahMasuk).Value = 0
        Next

        get_jumlahPO_Otomatis()
    End Sub

    Private Sub Txt_Timbang2_TextChanged(sender As Object, e As EventArgs) Handles Txt_Timbang2.TextChanged
        Hitung_Netto()
    End Sub
End Class