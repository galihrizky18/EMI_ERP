Public Class EMI_Pembelian2

    Dim no_fakturPengajuan As String

    Dim tempDataPajak As New List(Of (Pajak As String, Nilai As String, akun As String, isPPN As Boolean))

    Dim JenisPO As String = ""
    Dim arrJnsB_Byr As New ArrayList
    Dim arrPersediaan, arrInisialFaktur, arrCrByr, ArrAkunCB1 As New ArrayList
    Dim isError As String
    Dim Jenis = "Pembelian"
    Dim LvDisc_Persen As String
    Dim LvDisc_Rp As String
    Dim LvHarga As String
    Dim LvHargaReal As String
    Dim LvJml_Brg As String
    Dim LvJml_Msk As String
    Dim LvJumlah As String
    Dim LvKd_Brg As String
    Dim LvModal As String
    Dim LvNm_Brg As String
    Dim LvNo_PO As String
    Dim LvPakai_SN As String
    Dim LvPersenPPN As String
    Dim LvPPN As String
    Dim LvSatuan As String
    Dim LvSerial As String
    Dim LvSisa As String
    Dim LvSo As String
    Dim LvTotal As String
    Dim LvTtl_Harga As String
    Dim LvUpd_Hpp As String
    Dim LvUrutLoading As String
    Dim LvNoLoading As String
    Dim LvJmlHutang As String
    Dim LvTotalPO As String
    Dim LvTotalHutang As String
    Dim LvBiayPerjalanan As String

    Dim LvDisc_PersenDet As String
    Dim LvDisc_RpDet As String
    Dim LvHargaDet As String
    Dim LvHargaRealDet As String
    Dim LvJml_BrgDet As String
    Dim LvJml_MskDet As String
    Dim LvJumlahDet As String
    Dim LvKd_BrgDet As String
    Dim LvModalDet As String
    Dim LvNm_BrgDet As String
    Dim LvNo_PODet As String
    Dim LvPakai_SNDet As String
    Dim LvPersenPPNDet As String
    'Dim LvJumlahHutangDet As String
    Dim LvPPNDet As String
    Dim LvSatuanDet As String
    Dim LvSerialDet As String
    Dim LvSisaDet As String
    Dim LvSoDet As String
    Dim LvTotalDet As String
    Dim LvTtl_HargaDet As String
    Dim LvUpd_HppDet As String
    Dim LvUrutLoadingDet As String
    Dim LvNoLoadingDet As String
    Dim LvJmlHutangDet As String
    Dim LvTotalPODet As String
    Dim LvTotalHutangDet As String
    Dim LvBiayPerjalananDet As String

    'Dim Ket_Cost_Center_HO As Integer = 0

    Private Sub get_no_faktur()
        TxtPembelian_NoFaktur.Text = FPembelian & arrInisialFaktur.Item(CmbPembelian_Lokasi.SelectedIndex) & "-" & Format(DtpPembelian_Tgl.Value, "MM/yy") & "-" &
                                  General_Class.Get_Last_Number2("EMI_Pembelian", "no_faktur", 5,
                                  "Kode_perusahaan", KodePerusahaan,
                                  "And", "substring(no_faktur,1," & Len(FPembelian) + Len(arrInisialFaktur.Item(CmbPembelian_Lokasi.SelectedIndex)) + 6 & ")", FPembelian & arrInisialFaktur.Item(CmbPembelian_Lokasi.SelectedIndex) & "-" & Format(DtpPembelian_Tgl.Value, "MM/yy"))
    End Sub

    Private Sub Generate_Faktur_Pelunasan()
        Txt_Faktur_Pelunasan.Text = fValPelBI & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("EMI_Pelunasan", "no_val", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_val, 1, " & Len(fValPelBI) + 4 & ")", fValPelBI & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub Get_No_Faktur_Pengajuan()
        Dim fNB = "NB"
        no_fakturPengajuan = fNB & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Pengajuan_temp", "No_Pengajuan", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Pengajuan, 1, " & Len(fNB) + 4 & ")", fNB & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub Pembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")


        ' Kosong()
    End Sub

    Public Sub Kosong()

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            BtnPembelian_Simpan.Text = Base_Language.Lang_Global_Simpan
            BtnPembelian_Refresh.Text = Base_Language.Lang_Global_Refresh
            Label31.Text = Base_Language.Lang_Pembelian_Judul
            LblPembelian_Tgl.Text = Base_Language.Lang_Global_Tanggal
            LblPembelian_NoNota.Text = Base_Language.Lang_Global_NoNota
            LblPembelian_NoPO.Text = Base_Language.Lang_Global_No_PO
            LblPembelian_NoSO.Text = Base_Language.Lang_Pembelian_No_SO
            BtnPembelian_CariPO.Text = Base_Language.Lang_Pembelian_Cari_PO
            LblPembelian_Supplier.Text = Base_Language.lang_global_Nama_Supplier
            LblPembelian_Pembayaran.Text = Base_Language.Lang_Global_JenisPembayaran
            LblPembelian_MataUang.Text = Base_Language.Lang_Global_MataUang
            LblPembelian_Kurs.Text = Base_Language.Lang_Global_Kurs
            LblPembelian_CaraBayar.Text = Base_Language.Lang_Global_CaraBayar
            LblPembelian_TotalMUA.Text = Base_Language.Lang_Global_TotalMUA
            LblPembelian_TotalSblmPPN.Text = Base_Language.Lang_Global_GrandSblmPPN
            LblPembelian_PPN.Text = Base_Language.Lang_Global_PPN

            LvPembelian_DataPembelian.Columns.Clear()
            LvPembelian_DataPembelian.Columns.Add("No PO", 100, HorizontalAlignment.Left) '0
            LvPembelian_DataPembelian.Columns.Add("Stock Owner", 0, HorizontalAlignment.Left) '1
            LvPembelian_DataPembelian.Columns.Add(Base_Language.Lang_Pembelian_Kd_Bahan, 150, HorizontalAlignment.Left) '2
            LvPembelian_DataPembelian.Columns.Add(Base_Language.Lang_Pembelian_Nm_Bahan, 350, HorizontalAlignment.Left) '3
            LvPembelian_DataPembelian.Columns.Add(Base_Language.Lang_Pembelian_No_Seri, 0, HorizontalAlignment.Left) '4
            LvPembelian_DataPembelian.Columns.Add("Harga Akhir", 150, HorizontalAlignment.Right) '5
            LvPembelian_DataPembelian.Columns.Add(Base_Language.Lang_Pembelian_Jumlah, 0, HorizontalAlignment.Right) '6
            LvPembelian_DataPembelian.Columns.Add("Jumlah PO", 150, HorizontalAlignment.Right) '7
            LvPembelian_DataPembelian.Columns.Add("Jumlah Masuk", 150, HorizontalAlignment.Right) '8
            LvPembelian_DataPembelian.Columns.Add(Base_Language.Lang_Pembelian_Satuan, 120, HorizontalAlignment.Center).DisplayIndex = 4 '9
            LvPembelian_DataPembelian.Columns.Add("Disc(%)", 0, HorizontalAlignment.Right) '10
            LvPembelian_DataPembelian.Columns.Add("Disc(Rp.)", 0, HorizontalAlignment.Right) '11
            LvPembelian_DataPembelian.Columns.Add("Total Akhir", 150, HorizontalAlignment.Right) '12
            LvPembelian_DataPembelian.Columns.Add("Pakai SN", 0, HorizontalAlignment.Left) '13
            LvPembelian_DataPembelian.Columns.Add("Modal", 0, HorizontalAlignment.Left) '14
            LvPembelian_DataPembelian.Columns.Add("Upd HPP", 0, HorizontalAlignment.Left) '15 update hpp
            LvPembelian_DataPembelian.Columns.Add(Base_Language.Lang_Pembelian_Sisa, 0, HorizontalAlignment.Right) '16
            LvPembelian_DataPembelian.Columns.Add(Base_Language.Lang_Pembelian_Total_Harga, 0, HorizontalAlignment.Right) '17   LvPembelian_DataPembelian.Columns.Add(Base_Language.Lang_Pembelian_Total_Harga, 0, HorizontalAlignment.Right) '17
            LvPembelian_DataPembelian.Columns.Add("harga PO", 150, HorizontalAlignment.Right).DisplayIndex = 5 '18
            LvPembelian_DataPembelian.Columns.Add("persen ppn", 0, HorizontalAlignment.Right) '19
            LvPembelian_DataPembelian.Columns.Add("ppn", 0, HorizontalAlignment.Right) '20
            LvPembelian_DataPembelian.Columns.Add("urut_loading", 0, HorizontalAlignment.Right) '21
            LvPembelian_DataPembelian.Columns.Add("no_loading", 0, HorizontalAlignment.Right) '22
            LvPembelian_DataPembelian.Columns.Add("Jumlah Hutang", 150, HorizontalAlignment.Right) '23
            LvPembelian_DataPembelian.Columns.Add("Total PO", 150, HorizontalAlignment.Right) '24
            LvPembelian_DataPembelian.Columns.Add("Total Hutang", 150, HorizontalAlignment.Right) '25
            LvPembelian_DataPembelian.Columns.Add("BiayPerjalanan", 0, HorizontalAlignment.Right) '26
            LvPembelian_DataPembelian.View = View.Details


            ListViewDet.Columns.Clear()

            ListViewDet.Columns.Add("Stock Owner", 0, HorizontalAlignment.Left) '0
            ListViewDet.Columns.Add("No PO", 0, HorizontalAlignment.Left) '1
            ListViewDet.Columns.Add(Base_Language.Lang_Pembelian_Kd_Bahan, 150, HorizontalAlignment.Left) '2
            ListViewDet.Columns.Add(Base_Language.Lang_Pembelian_Nm_Bahan, 350, HorizontalAlignment.Left) '3
            ListViewDet.Columns.Add(Base_Language.Lang_Pembelian_No_Seri, 0, HorizontalAlignment.Left) '4
            ListViewDet.Columns.Add("Harga Akhir", 150, HorizontalAlignment.Right) '5
            ListViewDet.Columns.Add(Base_Language.Lang_Pembelian_Jumlah, 0, HorizontalAlignment.Right) '6
            ListViewDet.Columns.Add("Jumlah PO", 150, HorizontalAlignment.Right) '7
            ListViewDet.Columns.Add("Jumlah Masuk", 150, HorizontalAlignment.Right) '8
            ListViewDet.Columns.Add(Base_Language.Lang_Pembelian_Satuan, 120, HorizontalAlignment.Center).DisplayIndex = 4 '9
            ListViewDet.Columns.Add("Disc(%)", 0, HorizontalAlignment.Right) '10
            ListViewDet.Columns.Add("Disc(Rp.)", 0, HorizontalAlignment.Right) '11
            ListViewDet.Columns.Add("Total Akhir", 150, HorizontalAlignment.Right) '12
            ListViewDet.Columns.Add("Pakai SN", 0, HorizontalAlignment.Left) '13
            ListViewDet.Columns.Add("Modal", 0, HorizontalAlignment.Left) '14
            ListViewDet.Columns.Add("Upd HPP", 0, HorizontalAlignment.Left) '15 update hpp
            ListViewDet.Columns.Add(Base_Language.Lang_Pembelian_Sisa, 0, HorizontalAlignment.Right) '16
            ListViewDet.Columns.Add(Base_Language.Lang_Pembelian_Total_Harga, 0, HorizontalAlignment.Right) '17   LvPembelian_DataPembelian.Columns.Add(Base_Language.Lang_Pembelian_Total_Harga, 0, HorizontalAlignment.Right) '17
            ListViewDet.Columns.Add("Harga PO", 150, HorizontalAlignment.Right).DisplayIndex = 5 '18
            ListViewDet.Columns.Add("Persen ppn", 0, HorizontalAlignment.Right) '19
            ListViewDet.Columns.Add("PPN", 0, HorizontalAlignment.Right) '20
            ListViewDet.Columns.Add("Jumlah Hutang", 150, HorizontalAlignment.Right) '21
            ListViewDet.Columns.Add("Total PO", 150, HorizontalAlignment.Right) '22
            ListViewDet.Columns.Add("Total Hutang", 150, HorizontalAlignment.Right) '23
            ListViewDet.Columns.Add("BiayPerjalanan", 0, HorizontalAlignment.Right) '24
            ListViewDet.View = View.Details

            ListViewDet.Columns(21).DisplayIndex = 12
            ListViewDet.Columns(22).DisplayIndex = 13

            LvDataMobil.Columns.Clear()
            LvDataMobil.Columns.Add("No Loading", 130, HorizontalAlignment.Left) '0

            TxtPembelian_NoNota.Text = ""
            TxtPembelian_KdSupplier.Text = ""
            TxtPembelian_NmSupplier.Text = ""
            TxtPembelian_NoPO.Text = ""

            LvPembelian_DataPembelian.Items.Clear()

            TxtPembelian_TotalMUA.Text = "0"
            TxtPembelian_TotalIDR.Text = "0"
            TxtPembelian_TotalSblmPPN.Text = "0"
            TxtPembelian_PersenPPN.Text = "0"
            TxtPembelian_NilaiPPN.Text = "0"
            TxtPembelian_GrandTotal.Text = "0"
            Txt_TotPerjalanan.Text = "0"
            Txt_TotPerjalanan.Visible = True

            CmbPembelian_MataUang.SelectedIndex = -1
            CmbPembelian_JnsBayar.SelectedIndex = -1
            CmbPembelian_RangeBayar.SelectedIndex = -1
            CmbPembelian_Lokasi.Text = Lokasi
            TxtPembelian_Kurs.Text = ""

            LblPembelian_TotalBiaya.Text = ""
            LvPembelian_DataPembelian.Items.Clear()
            ListViewDet.Items.Clear()


            CmbPembelian_JnsBayar.Items.Clear() : arrJnsB_Byr.Clear()
            CmbPembelian_JnsBayar.Items.Add(Base_Language.Lang_Global_Tunai)
            arrJnsB_Byr.Add("T")
            CmbPembelian_JnsBayar.Items.Add(Base_Language.Lang_Global_Non_Tunai)
            arrJnsB_Byr.Add("N")

            CmbPembelian_RangeBayar.Items.Clear()
            For i As Integer = 1 To Jatuh_Tempo_Pembelian
                CmbPembelian_RangeBayar.Items.Add(i)
            Next

            CmbPembelian_MataUang.Items.Clear()
            SQL = "select Kode_Mata_Uang from Mata_Uang where kode_perusahaan = '" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbPembelian_MataUang.Items.Add(dr("Kode_Mata_Uang"))
                Loop
            End Using

            CmbPembelian_Lokasi.Items.Clear() : arrPersediaan.Clear() : arrInisialFaktur.Clear()
            SQL = "Select kode_stock_owner, persediaan, inisial_faktur From stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbPembelian_Lokasi.Items.Add(dr("kode_stock_owner")) : arrPersediaan.Add(dr("persediaan")) : arrInisialFaktur.Add(dr("inisial_faktur"))
                Loop
            End Using
            CmbPembelian_Lokasi.Text = Lokasi

            CmbPembelian_CaraBayar.Items.Clear() : arrCrByr.Clear() : ArrAkunCB1.Clear()
            'CmbPembelian_CaraBayar.Items.Add(Base_Language.Lang_Global_CaraBayar) : arrCrByr.Add("") : ArrAkunCB1.Add("")
            'CmbPembelian_CaraBayar.SelectedIndex = 0
            SQL = "select kode_cb, keterangan, kode_account_cb from cara_bayar where kode_perusahaan = '" & KodePerusahaan & "' and lokasi = '" & CmbPembelian_Lokasi.Text & "' order by keterangan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    CmbPembelian_CaraBayar.Items.Add(Dr("keterangan")) : arrCrByr.Add(Dr("kode_cb")) : ArrAkunCB1.Add(Dr("kode_account_cb"))
                Loop
            End Using

            Dim as6 As String = ""
            get_no_faktur()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvNo_PO = LvPembelian_DataPembelian.Items(No_Index).Text
        LvSo = LvPembelian_DataPembelian.Items(No_Index).SubItems(1).Text
        LvKd_Brg = LvPembelian_DataPembelian.Items(No_Index).SubItems(2).Text
        LvNm_Brg = LvPembelian_DataPembelian.Items(No_Index).SubItems(3).Text
        LvSerial = LvPembelian_DataPembelian.Items(No_Index).SubItems(4).Text
        LvHarga = LvPembelian_DataPembelian.Items(No_Index).SubItems(5).Text
        LvJumlah = LvPembelian_DataPembelian.Items(No_Index).SubItems(6).Text
        LvJml_Brg = LvPembelian_DataPembelian.Items(No_Index).SubItems(7).Text
        LvJml_Msk = LvPembelian_DataPembelian.Items(No_Index).SubItems(8).Text
        LvSatuan = LvPembelian_DataPembelian.Items(No_Index).SubItems(9).Text
        LvDisc_Persen = LvPembelian_DataPembelian.Items(No_Index).SubItems(10).Text
        LvDisc_Rp = LvPembelian_DataPembelian.Items(No_Index).SubItems(11).Text
        LvTotal = LvPembelian_DataPembelian.Items(No_Index).SubItems(12).Text
        LvPakai_SN = LvPembelian_DataPembelian.Items(No_Index).SubItems(13).Text
        LvModal = LvPembelian_DataPembelian.Items(No_Index).SubItems(14).Text
        LvUpd_Hpp = LvPembelian_DataPembelian.Items(No_Index).SubItems(15).Text
        LvSisa = LvPembelian_DataPembelian.Items(No_Index).SubItems(16).Text
        LvTtl_Harga = LvPembelian_DataPembelian.Items(No_Index).SubItems(17).Text
        LvHargaReal = LvPembelian_DataPembelian.Items(No_Index).SubItems(18).Text
        LvPersenPPN = LvPembelian_DataPembelian.Items(No_Index).SubItems(19).Text
        LvPPN = LvPembelian_DataPembelian.Items(No_Index).SubItems(20).Text
        LvUrutLoading = LvPembelian_DataPembelian.Items(No_Index).SubItems(21).Text
        LvNoLoading = LvPembelian_DataPembelian.Items(No_Index).SubItems(22).Text
        LvJmlHutang = LvPembelian_DataPembelian.Items(No_Index).SubItems(23).Text
        LvTotalPO = LvPembelian_DataPembelian.Items(No_Index).SubItems(24).Text
        LvTotalHutang = LvPembelian_DataPembelian.Items(No_Index).SubItems(25).Text
        LvBiayPerjalanan = LvPembelian_DataPembelian.Items(No_Index).SubItems(26).Text
    End Sub

    Private Sub Get_Isi_Listview_Det(ByVal No_Index As Integer)
        LvSoDet = ListViewDet.Items(No_Index).Text
        LvNo_PODet = ListViewDet.Items(No_Index).SubItems(1).Text
        LvKd_BrgDet = ListViewDet.Items(No_Index).SubItems(2).Text
        LvNm_BrgDet = ListViewDet.Items(No_Index).SubItems(3).Text
        LvSerialDet = ListViewDet.Items(No_Index).SubItems(4).Text
        LvHargaDet = ListViewDet.Items(No_Index).SubItems(5).Text
        LvJumlahDet = ListViewDet.Items(No_Index).SubItems(6).Text
        LvJml_BrgDet = ListViewDet.Items(No_Index).SubItems(7).Text
        LvJml_MskDet = ListViewDet.Items(No_Index).SubItems(8).Text
        LvSatuanDet = ListViewDet.Items(No_Index).SubItems(9).Text
        LvDisc_PersenDet = ListViewDet.Items(No_Index).SubItems(10).Text
        LvDisc_RpDet = ListViewDet.Items(No_Index).SubItems(11).Text
        LvTotalDet = ListViewDet.Items(No_Index).SubItems(12).Text
        LvPakai_SNDet = ListViewDet.Items(No_Index).SubItems(13).Text
        LvModalDet = ListViewDet.Items(No_Index).SubItems(14).Text
        LvUpd_HppDet = ListViewDet.Items(No_Index).SubItems(15).Text
        LvSisaDet = ListViewDet.Items(No_Index).SubItems(16).Text
        LvTtl_HargaDet = ListViewDet.Items(No_Index).SubItems(17).Text
        LvHargaRealDet = ListViewDet.Items(No_Index).SubItems(18).Text
        LvPersenPPNDet = ListViewDet.Items(No_Index).SubItems(19).Text
        LvPPNDet = ListViewDet.Items(No_Index).SubItems(20).Text
        LvJmlHutangDet = ListViewDet.Items(No_Index).SubItems(21).Text
        LvTotalPODet = ListViewDet.Items(No_Index).SubItems(22).Text
        LvTotalHutangDet = ListViewDet.Items(No_Index).SubItems(23).Text
        LvBiayPerjalananDet = ListViewDet.Items(No_Index).SubItems(24).Text

    End Sub

    Private Sub HitungGrandTotal()
        Dim Grand As Double = 0
        Dim diskon As Double = 0
        Dim TotalSeluruh As Double = 0
        Dim PPN As Double = 0
        Dim Persen_PPN As Double = 0
        Dim TotalBiayaPerjalanan As Double = 0
        For i As Integer = 0 To ListViewDet.Items.Count - 1
            'Get_Isi_Listview(i)
            Get_Isi_Listview_Det(i)

            Grand = Grand + HilangkanTanda(LvTotalHutangDet)
            PPN = PPN + HilangkanTanda(LvPPNDet)
            Persen_PPN = HilangkanTanda(LvPersenPPNDet)
            TotalBiayaPerjalanan = TotalBiayaPerjalanan + HilangkanTanda(LvBiayPerjalananDet)
        Next

        'End If
        TotalSeluruh = Grand * Val(TxtPembelian_Kurs.Text)
        'PPN = TotalSeluruh * Val(TxtPembelian_PersenPPN.Text) / 100

        TxtPembelian_PersenPPN.Text = Persen_PPN
        TxtPembelian_TotalMUA.Text = Format(Grand, "N2")
        TxtPembelian_TotalIDR.Text = Format(TotalSeluruh, "N2")
        TxtPembelian_TotalSblmPPN.Text = Format(TotalSeluruh, "N2")
        TxtPembelian_NilaiPPN.Text = Format(PPN, "N2")
        LblPembelian_TotalBiaya.Text = Format(TotalSeluruh + PPN, "N2")
        TxtPembelian_GrandTotal.Text = Format(TotalSeluruh + PPN, "N2")


        Dim Flag_import As String = ""
        Try
            OpenConn()



            SQL = "select isnull(Flag_Import,'T') as Flag_Import "
            SQL = SQL & "from EMI_Pembelian_PO a "
            SQL = SQL & "where "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & TxtPembelian_NoPO.Text & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    Flag_import = dr("Flag_Import")
                End If
            End Using

            If Flag_import = "T" Then

                Dim biaya_lokal As Double = 0

                SQL = "select "
                SQL = SQL & "round(isnull(sum(b.total),0), 0) as Biaya "
                SQL = SQL & "from transaksi_biaya_Lokal a, transaksi_biaya_Lokal_detail b, Master_Kategori_Biaya_Import c where "
                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And "
                SQL = SQL & "a.no_faktur = b.no_faktur And a.status Is null and "
                SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Master_Kategori_Biaya_import = c.Kode_Master_Kategori_Biaya_Import and "
                SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.no_Po = '" & TxtPembelian_NoPO.Text & "' " 'and C.Flag_Gabungan = 'Y' "
                Using ds = BindingTrans(SQL)
                    With ds.Tables("MyTable")
                        For index3 As Integer = 0 To .Rows.Count - 1

                            biaya_lokal = biaya_lokal + Val(HilangkanTanda(Format(.Rows(index3).Item("Biaya"), "N0")))

                        Next
                    End With
                End Using

                Txt_TotPerjalanan.Text = Format(biaya_lokal, "N2")
                Txt_TotPerjalanan.Visible = True

            Else
                Txt_TotPerjalanan.Text = Format(0, "N2")
                Txt_TotPerjalanan.Visible = False
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



    End Sub

    Public Sub TxtPembelian_NoPO_Leave(sender As Object, e As EventArgs) Handles TxtPembelian_NoPO.Leave
        Try
            OpenConn()

            LvPembelian_DataPembelian.Items.Clear()
            ListViewDet.Items.Clear()

            Dim Flag_import As String = ""

            SQL = "select a.No_Faktur,a.No_Nota,a.Tanggal,a.Kode_Supplier as Supplier,b.Nama,a.Jenis_Pembayaran,"
            SQL = SQL & "a.Mata_Uang,a.Kurs,a.Cara_Bayar, "

            SQL = SQL & "isnull((select c.keterangan from cara_bayar c where "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.Lokasi = c.Lokasi and a.Cara_Bayar = c.Kode_CB "
            SQL = SQL & "),'') as keterangan, "

            SQL = SQL & "a.PPN, a.Total_MUA,a.Total_IDR,a.Grand_Sebelum_PPN,a.Grand, isnull(Flag_Import,'T') as Flag_Import "
            SQL = SQL & "from EMI_Pembelian_PO a,Suppliers b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & TxtPembelian_NoPO.Text & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then

                    Flag_import = dr("Flag_Import")

                    'DtpPembelian_Tgl.Value = Format(dr("Tanggal"), "dd MMMM yyyy")
                    TxtPembelian_NoNota.Text = dr("No_Nota")
                    TxtPembelian_KdSupplier.Text = dr("Supplier")
                    TxtPembelian_NmSupplier.Text = dr("Nama")
                    If dr("Jenis_Pembayaran") = "T" Then
                        CmbPembelian_JnsBayar.SelectedIndex = 0
                    Else
                        CmbPembelian_JnsBayar.SelectedIndex = 1
                    End If
                    CmbPembelian_MataUang.Text = dr("Mata_Uang")
                    TxtPembelian_Kurs.Text = Format(dr("Kurs"), "N2")
                    CmbPembelian_CaraBayar.Text = dr("Keterangan")
                    If dr("PPN") > 0 Then
                        TxtPembelian_PersenPPN.Text = Format(dr("PPN"), "N2")
                    Else
                        TxtPembelian_PersenPPN.Text = 0
                    End If

                    Dim nilai_ppn As Double = 0
                    nilai_ppn = dr("Grand_Sebelum_PPN") * dr("PPN") / 100
                    TxtPembelian_TotalMUA.Text = Format(dr("Total_MUA"), "N2")
                    TxtPembelian_TotalIDR.Text = Format(dr("Total_IDR"), "N2")
                    TxtPembelian_TotalSblmPPN.Text = Format(dr("Grand_Sebelum_PPN"), "N2")
                    TxtPembelian_NilaiPPN.Text = Format(nilai_ppn, "N2")
                    TxtPembelian_GrandTotal.Text = Format(dr("Grand"), "N2")

                End If
            End Using

            '==========================
            '=     GET DATA PAJAK     =
            '==========================
            Dim TotPPH As Double = 0
            SQL = "select Kode_Tarif, Persentase, Flag_PPN, Kode_Akun "
            SQL = SQL & "from EMI_Detail_PPH_PO where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & TxtPembelian_NoPO.Text & "' "
            Using Ds1 = BindingTrans(SQL)
                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                    For i As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1
                        If General_Class.CekNULL(Ds1.Tables("MyTable").Rows(i).Item("Flag_PPN")) = "" Then
                            tempDataPajak.Add((Ds1.Tables("MyTable").Rows(i).Item("Kode_Tarif"), Ds1.Tables("MyTable").Rows(i).Item("Persentase"), Ds1.Tables("MyTable").Rows(i).Item("Kode_Akun"), False))

                            TotPPH += Val(HilangkanTanda(TxtPembelian_TotalIDR.Text)) * (Val(HilangkanTanda(Ds1.Tables("MyTable").Rows(i).Item("Persentase"))) / 100)

                        Else
                            tempDataPajak.Add((Ds1.Tables("MyTable").Rows(i).Item("Kode_Tarif"), Ds1.Tables("MyTable").Rows(i).Item("Persentase"), Ds1.Tables("MyTable").Rows(i).Item("Kode_Akun"), True))

                            TxtPembelian_PersenPPN.Text = Ds1.Tables("MyTable").Rows(i).Item("Persentase")
                        End If
                    Next
                End If
            End Using

            Txt_GrandPPH.Text = Format(TotPPH, "N2")



            Dim No_Loading As String = ""
            Dim idx As Integer = 0
            LvDataMobil.Items.Clear()
            'SQL = "Select distinct c.no_faktur from "
            'SQL = SQL & "EMI_Pembelian_Selisih_Barang_Masuk a, EMI_Pembelian_Selisih_Barang_Masuk_det b, "
            'SQL = SQL & "EMI_Pembelian_Loading_Detail c "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.no_faktur And a.status Is null "
            'SQL = SQL & "And b.Kode_Perusahaan=c.Kode_Perusahaan And b.Urut_Loading=c.Urut_Oto And "

            SQL = "Select distinct c.no_faktur, isnull(d.flag_selisih_bm,'T') as flag_selisih_bm from  "
            SQL = SQL & "EMI_Pembelian_Loading_Detail c, EMI_Pembelian_Loading d, EMI_Pembelian_PO e  "
            SQL = SQL & "where  "
            SQL = SQL & "c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and  d.Status is null "
            'SQL = SQL & "and d.flag_selisih_bm = 'Y' "
            SQL = SQL & "and c.Kode_Perusahaan = e.Kode_Perusahaan and c.No_PO = e.No_Faktur and e.flag_selesai_po = 'Y' "

            SQL = SQL & "And c.Kode_Perusahaan='" & KodePerusahaan & "' and c.No_PO ='" & TxtPembelian_NoPO.Text & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read

                    If dr("flag_selisih_bm") = "T" Then
                        EMI_PO_Pembelian_Display2.bolehLewat = False
                        CloseConn()
                        MessageBox.Show("Pembelian Belum Selesai")
                        Me.Close()
                        Exit Sub
                    End If

                    Dim Lvw As ListViewItem
                    Lvw = LvDataMobil.Items.Add(dr("no_faktur"))
                    EMI_PO_Pembelian_Display2.bolehLewat = True

                    If idx <> 0 Then
                        No_Loading += ", "
                    End If

                    No_Loading += "'" + dr("no_faktur") + "'"

                    idx += 1
                Loop
            End Using

            If No_Loading.Trim.Length = 0 Then
                EMI_PO_Pembelian_Display2.bolehLewat = False
                CloseConn()
                MessageBox.Show("Pembelian Belum Selesai")
                Me.Close()
                Exit Sub
            End If

            SQL = "Select c.No_faktur,c.urut_oto, "
            SQL = SQL & "isnull((select top(1) Kode_Stock_Owner_Tujuan from "
            SQL = SQL & "EMI_Barang_Masuk_Perpallet x where x.No_Pembelian_Loading=c.no_faktur and x.status is null),NULL) as Kode_Stock_Owner, "
            SQL = SQL & "b.Kode_Barang, d.Nama, sum(b.Jumlah_PL) As Jumlah_PL, "
            SQL = SQL & "sum(Jumlah_BM + isnull(Qty_PenyelesaianPlus, 0) - isnull(Qty_PenyelesaianMin, 0)) As Jumlah_Masuk, "
            SQL = SQL & "b.Satuan as satuan, c.Harga_barang As Harga, c.Satuan as satuan_display, c.hpp_satuan_display, sum(b.Jumlah_Utang) as Jumlah_Utang "
            SQL = SQL & "From EMI_Pembelian_Selisih_Barang_Masuk a, EMI_Pembelian_Selisih_Barang_Masuk_det b, "
            SQL = SQL & "EMI_Pembelian_Loading_Detail c, barang d "
            SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.no_faktur And a.status Is null "
            SQL = SQL & "And b.Kode_Perusahaan=c.Kode_Perusahaan And b.Urut_Loading=c.Urut_Oto And "
            SQL = SQL & "b.Kode_Perusahaan = d.Kode_Perusahaan And b.Kode_Barang = d.Kode_Barang And b.Kode_Stock_Owner = d.Kode_Stock_Owner And "
            SQL = SQL & "c.No_PO ='" & TxtPembelian_NoPO.Text & "' and c.no_faktur in (" & No_Loading & ") And c.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "group by c.No_faktur,b.Kode_Stock_Owner,c.urut_oto, b.Kode_Barang, d.Nama, b.Satuan, c.Harga_barang, c.Satuan, c.hpp_satuan_display "
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For index = 0 To .Rows.Count - 1

                        If IsDBNull(.Rows(index).Item("hpp_satuan_display")) Or IsDBNull(.Rows(index).Item("Harga")) Then
                            EMI_PO_Pembelian_Display2.bolehLewat = False
                            CloseConn()
                            MessageBox.Show("HPP Belum Selesai")
                            Me.Close()
                            Exit Sub
                        End If

                        If IsDBNull(.Rows(index).Item("Kode_Stock_Owner")) Then
                            EMI_PO_Pembelian_Display2.bolehLewat = False
                            CloseConn()
                            MessageBox.Show("Barang Masuk Belum Selesai . . !")
                            Me.Close()
                            Exit Sub
                        End If

                        Dim harga_real As Double = Ubah_Satuan(.Rows(index).Item("Kode_Barang"), .Rows(index).Item("Harga"), .Rows(index).Item("Satuan"), .Rows(index).Item("satuan_display"), "UANG")
                        Dim harga As Double = .Rows(index).Item("hpp_satuan_display")
                        Dim jumlahkirim As Double = Ubah_Satuan(.Rows(index).Item("Kode_Barang"), .Rows(index).Item("Jumlah_PL"), .Rows(index).Item("Satuan"), .Rows(index).Item("satuan_display"), "MASA")
                        Dim Jumlahmasuk As Double = Ubah_Satuan(.Rows(index).Item("Kode_Barang"), .Rows(index).Item("Jumlah_Masuk"), .Rows(index).Item("Satuan"), .Rows(index).Item("satuan_display"), "MASA")
                        Dim JumlahHutang As Double = .Rows(index).Item("Jumlah_Utang")

                        Dim Persen_PPn As Double = 0
                        Dim Nilai_PPN As Double = 0

                        If Flag_import = "Y" Then
                            Dim id_rencana As String = ""
                            SQL = "select Lokasi, No_Fak_HPP, b.ID_Rencana "
                            SQL = SQL & "from emi_pembelian_loading a, HPP_Import b where "
                            SQL = SQL & "a.Kode_Perusahaan=b.kode_perusahaan and a.No_Fak_HPP=b.No_Faktur "
                            SQL = SQL & "and a.status is null and b.status is null "
                            SQL = SQL & "and a.No_Faktur = '" & .Rows(index).Item("No_faktur") & "' "
                            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            Using dr = OpenTrans(SQL)
                                If dr.Read Then
                                    id_rencana = dr("ID_Rencana")
                                Else
                                    dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("No Faktur Tidak ditemukan . . ! ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            SQL = "select Nilai_PPN/Jumlah as Nilai_PPN, Persen_PPN  "
                            SQL = SQL & "from Total_Billing a, Detail_Total_Billing b where a.Kode_Perusahaan=b.Kode_perusahaan and a.No_Faktur=b.No_Faktur "
                            SQL = SQL & "and a.ID_Rencana='" & id_rencana & "' and a.Status is nulL AND B.Kode_Barang='" & .Rows(index).Item("Kode_Barang") & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Persen_PPn = Dr("Persen_PPN")
                                    Nilai_PPN = Math.Round((Dr("Nilai_PPN") * (jumlahkirim)))
                                End If
                            End Using
                        Else
                            SQL = "select PPN from emi_pembelian_po "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & TxtPembelian_NoPO.Text & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Persen_PPn = Dr("PPN")
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            Nilai_PPN = Math.Round((JumlahHutang * harga_real) * Persen_PPn / 100)
                        End If

                        Dim Lvw As ListViewItem
                        Lvw = LvPembelian_DataPembelian.Items.Add(.Rows(index).Item("No_faktur"))
                        Lvw.SubItems.Add(.Rows(index).Item("Kode_Stock_Owner"))
                        Lvw.SubItems.Add(.Rows(index).Item("Kode_Barang"))
                        Lvw.SubItems.Add(.Rows(index).Item("Nama"))
                        Lvw.SubItems.Add("")
                        Lvw.SubItems.Add(Format(harga, "N2"))
                        Lvw.SubItems.Add(Format(0, "N2"))
                        Lvw.SubItems.Add(Format(jumlahkirim, "N2"))
                        Lvw.SubItems.Add(Format(Jumlahmasuk, "N2"))
                        Lvw.SubItems.Add(.Rows(index).Item("Satuan_display"))
                        Lvw.SubItems.Add("")
                        Lvw.SubItems.Add("")
                        Dim ftotal As Double = Jumlahmasuk * harga
                        Lvw.SubItems.Add(Format(ftotal, "N2"))
                        Lvw.SubItems.Add("")
                        Lvw.SubItems.Add("")
                        Lvw.SubItems.Add("")
                        Lvw.SubItems.Add("")
                        Lvw.SubItems.Add("")
                        Lvw.SubItems.Add(Format(harga_real, "N2"))
                        Lvw.SubItems.Add(Persen_PPn)
                        Lvw.SubItems.Add(Nilai_PPN)
                        Lvw.SubItems.Add(.Rows(index).Item("urut_oto"))
                        Lvw.SubItems.Add(.Rows(index).Item("no_faktur"))
                        Lvw.SubItems.Add(Format(JumlahHutang, "N2"))

                        Dim TotalPO As Double = harga_real * jumlahkirim
                        Lvw.SubItems.Add(Format(TotalPO, "N2")) '22
                        Dim TotalHutang As Double = harga_real * JumlahHutang
                        Lvw.SubItems.Add(Format(TotalHutang, "N2")) '23
                        Dim BiayaPerjalanan As Double = 0
                        If Flag_import = "Y" Then
                            BiayaPerjalanan = (harga - harga_real) * jumlahkirim
                        Else
                            BiayaPerjalanan = (harga - harga_real) * Jumlahmasuk

                        End If

                        Lvw.SubItems.Add(Format(BiayaPerjalanan, "N2")) '24

                        '==================================================================='
                        '=================== Check Untuk Grouping =========================='
                        '==================================================================='
                        Dim tambah_baru As Boolean = True
                        For index1 As Integer = 0 To ListViewDet.Items.Count - 1

                            If ListViewDet.Items(index1).SubItems(1).Text = .Rows(index).Item("Kode_Barang") Then
                                Dim jumlahPLTemp As Double = HilangkanTanda(ListViewDet.Items(index1).SubItems(7).Text) + Val(jumlahkirim)
                                Dim jumlahMasukTemp As Double = HilangkanTanda(ListViewDet.Items(index1).SubItems(8).Text) + Val(Jumlahmasuk)
                                Dim jumlahTotalTemp As Double = HilangkanTanda(ListViewDet.Items(index1).SubItems(12).Text) + Val(ftotal)
                                Dim nilaiPPnTemp As Double = HilangkanTanda(ListViewDet.Items(index1).SubItems(20).Text) + Val(Nilai_PPN)

                                Dim nilaiPOTemp As Double = HilangkanTanda(ListViewDet.Items(index1).SubItems(22).Text) + Val(TotalPO)
                                Dim nilaiHutangTemp As Double = HilangkanTanda(ListViewDet.Items(index1).SubItems(23).Text) + Val(TotalHutang)
                                Dim nilaiPerjalananTemp As Double = HilangkanTanda(ListViewDet.Items(index1).SubItems(24).Text) + Val(BiayaPerjalanan)


                                ListViewDet.Items(index1).SubItems(7).Text = Format(jumlahPLTemp, "N2")
                                ListViewDet.Items(index1).SubItems(8).Text = Format(jumlahMasukTemp, "N2")
                                ListViewDet.Items(index1).SubItems(12).Text = Format(jumlahTotalTemp, "N2")
                                ListViewDet.Items(index1).SubItems(20).Text = Format(nilaiPPnTemp, "N2")
                                ListViewDet.Items(index1).SubItems(22).Text = Format(nilaiPOTemp, "N2") '22
                                ListViewDet.Items(index1).SubItems(23).Text = Format(nilaiHutangTemp, "N2") '23
                                ListViewDet.Items(index1).SubItems(24).Text = Format(nilaiPerjalananTemp, "N2") '24

                                'ListViewDet.Items(index1).SubItems(6).Text = Format(Val(jumlahPLTemp) + Val(jumlahkirim), "N2")
                                'ListViewDet.Items(index1).SubItems(7).Text = Format(Val(jumlahMasukTemp) + Val(Jumlahmasuk), "N2")
                                'ListViewDet.Items(index1).SubItems(12).Text = Format(Val(jumlahTotalTemp) + Val(ftotal), "N2")
                                'ListViewDet.Items(index1).SubItems(19).Text = Format(Val(nilaiPPnTemp) + Val(Nilai_PPN), "N2")




                                tambah_baru = False


                            End If

                        Next

                        If tambah_baru Then
                            Dim Lvw1 As ListViewItem
                            Lvw1 = ListViewDet.Items.Add(.Rows(index).Item("Kode_Stock_Owner")) '0
                            Lvw1.SubItems.Add(.Rows(index).Item("No_faktur")) '1
                            Lvw1.SubItems.Add(.Rows(index).Item("Kode_Barang")) '2
                            Lvw1.SubItems.Add(.Rows(index).Item("Nama")) '3
                            Lvw1.SubItems.Add("") '4
                            Lvw1.SubItems.Add(Format(harga, "N2")) '5
                            Lvw1.SubItems.Add(Format(0, "N2")) '6
                            Lvw1.SubItems.Add(Format(jumlahkirim, "N2")) '7
                            Lvw1.SubItems.Add(Format(Jumlahmasuk, "N2")) '8
                            Lvw1.SubItems.Add(.Rows(index).Item("Satuan_display")) '9
                            Lvw1.SubItems.Add("") '10
                            Lvw1.SubItems.Add("") '11
                            Dim ftotal1 As Double = Jumlahmasuk * harga
                            Lvw1.SubItems.Add(Format(ftotal1, "N2")) '12
                            Lvw1.SubItems.Add("") '13
                            Lvw1.SubItems.Add("") '14
                            Lvw1.SubItems.Add("") '15
                            Lvw1.SubItems.Add("") '16
                            Lvw1.SubItems.Add("") '17
                            Lvw1.SubItems.Add(Format(harga_real, "N2")) '18
                            Lvw1.SubItems.Add(Persen_PPn) '19
                            Lvw1.SubItems.Add(Nilai_PPN) '20
                            Lvw1.SubItems.Add(Format(JumlahHutang, "N2")) '21

                            Lvw1.SubItems.Add(Format(TotalPO, "N2")) '22
                            Lvw1.SubItems.Add(Format(TotalHutang, "N2")) '23
                            Lvw1.SubItems.Add(Format(BiayaPerjalanan, "N2")) '24

                        End If


                        'SQL = "INSERT INTO EMI_Pembelian_Detail(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,"
                        'SQL = SQL & "Kode_Barang,Serial_Number,Harga,Jumlah,Satuan,Disc_Persen,Nilai_Disc,"
                        'SQL = SQL & "Total,Pakai_SN,Modal,Upd_HPP,Sisa,Total_Harga, "
                        'SQL = SQL & "Jumlah_Masuk, Harga_Akhir, PPN, Persen_PPN,urut_loading,no_loading) VALUES("
                        'SQL = SQL & "'" & KodePerusahaan & "','" & TxtPembelian_NoFaktur.Text & "',"
                        'SQL = SQL & "'" & LvSo & "','" & LvKd_Brg & "','" & LvSerial & "',"
                        'SQL = SQL & "'" & HilangkanTanda(LvHargaReal) & "','" & HilangkanTanda(LvJml_Brg) & "',"
                        'SQL = SQL & "'" & LvSatuan & "','" & HilangkanTanda(LvDisc_Persen) & "',"
                        'SQL = SQL & "'" & HilangkanTanda(LvDisc_Rp) & "','" & HilangkanTanda(LvTotal) & "',"
                        'SQL = SQL & "'" & LvPakai_SN & "','" & HilangkanTanda(LvModal) & "',"
                        'SQL = SQL & "'" & HilangkanTanda(LvUpd_Hpp) & "','" & HilangkanTanda(LvSisa) & "',"
                        'SQL = SQL & "'" & HilangkanTanda(LvTtl_Harga) & "', '" & HilangkanTanda(LvJml_Msk) & "', "
                        'SQL = SQL & "'" & HilangkanTanda(LvHarga) & "', '" & HilangkanTanda(LvPPN) & "', '" & HilangkanTanda(LvPersenPPN) & "' "
                        'SQL = SQL & "'" & LvUrutLoading & "', '" & LvNoLoading & "' )"


                        ''grouping
                        'If tambah_baru Then
                        '    Dim Lvw1 As ListViewItem
                        '    Lvw1 = ListViewDet.Items.Add(.Rows(index).Item("Kode_Stock_Owner"))
                        '    Lvw1.SubItems.Add(.Rows(index).Item("Kode_Barang"))
                        '    Lvw1.SubItems.Add(.Rows(index).Item("Nama"))
                        '    Lvw1.SubItems.Add("")
                        '    Lvw1.SubItems.Add(Format(harga, "N2"))
                        '    Lvw1.SubItems.Add(Format(0, "N2"))
                        '    Lvw1.SubItems.Add(Format(jumlahkirim, "N2"))
                        '    Lvw1.SubItems.Add(Format(Jumlahmasuk, "N2"))
                        '    Lvw1.SubItems.Add(.Rows(index).Item("Satuan_display"))
                        '    Lvw1.SubItems.Add("")
                        '    Lvw1.SubItems.Add("")
                        '    Dim ftotal1 As Double = Jumlahmasuk * harga
                        '    Lvw1.SubItems.Add(Format(ftotal, "N2"))
                        '    Lvw1.SubItems.Add("")
                        '    Lvw1.SubItems.Add("")
                        '    Lvw1.SubItems.Add("")
                        '    Lvw1.SubItems.Add("")
                        '    Lvw1.SubItems.Add("")
                        '    Lvw1.SubItems.Add(Format(harga_real, "N2"))
                        '    Lvw1.SubItems.Add(Persen_PPn)
                        '    Lvw1.SubItems.Add(Nilai_PPN)
                        'Else

                        'End If

                    Next


                End With

                ' grouping 


            End Using


            '================================
            '=     GET DATA DOWNPAYMENT     =
            '================================
            Dim Total_DP As Double = 0
            'SQL = "with Cte as ( select a.Nilai as Nilai_DP, ( "
            'SQL = SQL & "a.Nilai - ISNULL(( select z.nilai from EMI_Pelunasan_Detail_DP z, emi_pelunasan w where "
            'SQL = SQL & "z.Kode_Perusahaan = a.Kode_Perusahaan and z.urut_DP = a.No_Urut and "
            'SQL = SQL & "z.kode_perusahaan=w.kode_Perusahaan and z.no_val=w.no_val and w.status is null "
            'SQL = SQL & " ), 0) ) as Sisa "
            'SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka_Detail a, EMI_Transaksi_Pembayaran_Dimuka b  "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "And a.No_Transaksi = b.No_Transaksi  "
            'SQL = SQL & "And b.Status Is null  "
            'SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "'  "
            'SQL = SQL & "and a.No_Fak_PO in ( "
            'SQL = SQL & "select y.No_FakInduk  "
            'SQL = SQL & "from EMI_Pembelian_PO x, EMI_Pembelian_PO_Det y  "
            'SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan  "
            'SQL = SQL & "and x.No_Faktur = y.No_Faktur "
            'SQL = SQL & "and x.Status is null  "
            'SQL = SQL & "and x.Kode_Perusahaan = '" & KodePerusahaan & "'  "
            'SQL = SQL & "and x.No_Faktur = '" & TxtPembelian_NoPO.Text & "'  "
            'SQL = SQL & "group by y.No_FakInduk ) "
            'SQL = SQL & ")select sum(Sisa) as Nilai_DP from Cte "
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        Total_DP = Dr("Nilai_DP")
            '    End If
            'End Using

            SQL = ";with Cte as ( "
            SQL = SQL & "select a.Nilai as Nilai_DP, ( "
            SQL = SQL & "(a.Nilai- "
            SQL = SQL & "isnull(( "
            SQL = SQL & "select sum(x.nilai) from EMI_Transaksi_Pembayaran_Dimuka_Pajak x "
            SQL = SQL & "where x.kode_perusahaan=a.kode_perusahaan and x.no_faktur = a.No_Transaksi and x.flag_ppn is null ),0)) - "
            SQL = SQL & "ISNULL(( "
            SQL = SQL & "select z.nilai from EMI_Pelunasan_Detail_DP z, emi_pelunasan w "
            SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan and z.urut_DP = a.No_Urut and "
            SQL = SQL & "z.kode_perusahaan=w.kode_Perusahaan and z.no_val=w.no_val and w.status is null "
            SQL = SQL & "), 0) ) as Sisa "
            SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka_Detail a, EMI_Transaksi_Pembayaran_Dimuka b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "And a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "And b.Status Is null "
            SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Fak_PO in ( "
            SQL = SQL & "select y.No_FakInduk "
            SQL = SQL & "from EMI_Pembelian_PO x, EMI_Pembelian_PO_Det y "
            SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan "
            SQL = SQL & "and x.No_Faktur = y.No_Faktur "
            SQL = SQL & "and x.Status is null "
            SQL = SQL & "and x.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and x.No_Faktur = '" & TxtPembelian_NoPO.Text & "' "
            SQL = SQL & "group by y.No_FakInduk ) "
            SQL = SQL & ")select isnull(sum(Sisa),0) as Nilai_DP from Cte "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Total_DP = Dr("Nilai_DP")
                End If
            End Using
            Txt_PembayaranDimuka.Text = Format((Total_DP), "N0")









            ''grouping
            'SQL = "Select b.Kode_Stock_Owner, b.Kode_Barang, d.Nama, sum(b.Jumlah_PL) As Jumlah_PL, sum(Jumlah_BM + isnull(Qty_PenyelesaianPlus, 0) - isnull(Qty_PenyelesaianMin, 0)) As Jumlah_Masuk,  "
            'SQL = SQL & "b.Satuan as satuan, c.Harga_barang As Harga, c.Satuan as satuan_display, c.hpp_satuan_display From EMI_Pembelian_Selisih_Barang_Masuk a, EMI_Pembelian_Selisih_Barang_Masuk_det b, "
            'SQL = SQL & "EMI_Pembelian_Loading_Detail c, barang d Where a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "And a.No_Faktur = b.no_faktur And a.status Is null And b.Kode_Perusahaan=c.Kode_Perusahaan "
            'SQL = SQL & "And b.Urut_Loading=c.Urut_Oto And b.Kode_Perusahaan = d.Kode_Perusahaan "

            'SQL = SQL & "And b.Kode_Barang = d.Kode_Barang And b.Kode_Stock_Owner = d.Kode_Stock_Owner And "
            'SQL = SQL & "c.No_PO ='" & TxtPembelian_NoPO.Text & "' and c.no_faktur in (" & No_Loading & ") And c.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "group by b.Kode_Stock_Owner, b.Kode_Barang, d.Nama, b.Satuan, c.Harga_barang, c.Satuan, c.hpp_satuan_display "
            'Using ds = BindingTrans(SQL)
            '    With ds.Tables("MyTable")
            '        For index = 0 To .Rows.Count - 1

            '            If IsDBNull(.Rows(index).Item("hpp_satuan_display")) Or IsDBNull(.Rows(index).Item("Harga")) Then
            '                CloseConn()
            '                MessageBox.Show("HPP Belum Selesai")
            '                Me.Close()
            '                Exit Sub
            '            End If

            '            Dim harga_real As Double = Ubah_Satuan(.Rows(index).Item("Kode_Barang"), .Rows(index).Item("Harga"), .Rows(index).Item("Satuan"), .Rows(index).Item("satuan_display"), "UANG")
            '            Dim harga As Double = .Rows(index).Item("hpp_satuan_display")
            '            Dim jumlahkirim As Double = Ubah_Satuan(.Rows(index).Item("Kode_Barang"), .Rows(index).Item("Jumlah_PL"), .Rows(index).Item("Satuan"), .Rows(index).Item("satuan_display"), "MASA")
            '            Dim Jumlahmasuk As Double = Ubah_Satuan(.Rows(index).Item("Kode_Barang"), .Rows(index).Item("Jumlah_Masuk"), .Rows(index).Item("Satuan"), .Rows(index).Item("satuan_display"), "MASA")

            '            Dim Persen_PPn As Double = 0
            '            Dim Nilai_PPN As Double = 0

            '            If Flag_import = "Y" Then
            '                Dim id_rencana As String = ""
            '                SQL = "select Lokasi, No_Fak_HPP, b.ID_Rencana "
            '                SQL = SQL & "from emi_pembelian_loading a, HPP_Import b where "
            '                SQL = SQL & "a.Kode_Perusahaan=b.kode_perusahaan and a.No_Fak_HPP=b.No_Faktur "
            '                SQL = SQL & "and a.status is null and b.status is null "
            '                SQL = SQL & "and a.No_Faktur = '" & .Rows(index).Item("No_faktur") & "' "
            '                SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            '                Using dr = OpenTrans(SQL)
            '                    If dr.Read Then
            '                        id_rencana = dr("ID_Rencana")
            '                    Else
            '                        dr.Close()
            '                        CloseTrans()
            '                        CloseConn()
            '                        MessageBox.Show("No Faktur Tidak ditemukan . . ! ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                        Exit Sub
            '                    End If
            '                End Using

            '                SQL = "select Nilai_PPN/Jumlah as Nilai_PPN, Persen_PPN  "
            '                SQL = SQL & "from Total_Billing a, Detail_Total_Billing b where a.Kode_Perusahaan=b.Kode_perusahaan and a.No_Faktur=b.No_Faktur "
            '                SQL = SQL & "and a.ID_Rencana='" & id_rencana & "' and a.Status is nulL AND B.Kode_Barang='" & .Rows(index).Item("Kode_Barang") & "' "
            '                Using Dr = OpenTrans(SQL)
            '                    If Dr.Read Then
            '                        Persen_PPn = Dr("Persen_PPN")
            '                        Nilai_PPN = Math.Round((Dr("Nilai_PPN") * (jumlahkirim)))
            '                    End If
            '                End Using
            '            Else
            '                SQL = "select PPN from emi_pembelian_po "
            '                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & TxtPembelian_NoPO.Text & "' "
            '                Using Dr = OpenTrans(SQL)
            '                    If Dr.Read Then
            '                        Persen_PPn = Dr("PPN")
            '                    Else
            '                        Dr.Close()
            '                        CloseTrans()
            '                        CloseConn()
            '                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                        Exit Sub
            '                    End If
            '                End Using

            '                Nilai_PPN = Math.Round((jumlahkirim * harga_real) * PPN / 100)
            '            End If

            '            Dim Lvw As ListViewItem
            '            Lvw = LvPembelian_DataPembelian.Items.Add(.Rows(index).Item("No_faktur"))
            '            Lvw.SubItems.Add(.Rows(index).Item("Kode_Stock_Owner"))
            '            Lvw.SubItems.Add(.Rows(index).Item("Kode_Barang"))
            '            Lvw.SubItems.Add(.Rows(index).Item("Nama"))
            '            Lvw.SubItems.Add("")
            '            Lvw.SubItems.Add(Format(harga, "N2"))
            '            Lvw.SubItems.Add(Format(0, "N2"))
            '            Lvw.SubItems.Add(Format(jumlahkirim, "N2"))
            '            Lvw.SubItems.Add(Format(Jumlahmasuk, "N2"))
            '            Lvw.SubItems.Add(.Rows(index).Item("Satuan_display"))
            '            Lvw.SubItems.Add("")
            '            Lvw.SubItems.Add("")
            '            Dim ftotal As Double = Jumlahmasuk * harga
            '            Lvw.SubItems.Add(Format(ftotal, "N2"))
            '            Lvw.SubItems.Add("")
            '            Lvw.SubItems.Add("")
            '            Lvw.SubItems.Add("")
            '            Lvw.SubItems.Add("")
            '            Lvw.SubItems.Add("")
            '            Lvw.SubItems.Add(Format(harga_real, "N2"))
            '            Lvw.SubItems.Add(Persen_PPn)
            '            Lvw.SubItems.Add(Nilai_PPN)
            '            Lvw.SubItems.Add(.Rows(index).Item("urut_oto"))
            '            Lvw.SubItems.Add(.Rows(index).Item("no_faktur"))
            '        Next


            '    End With
            'End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        HitungGrandTotal()
    End Sub

    Private Sub BtnPembelian_CariPO_Click(sender As Object, e As EventArgs) Handles BtnPembelian_CariPO.Click
        'SD_Pilih_PO2.asal = Jenis
        'SD_Pilih_PO2.filter_tambahan = " and a.Flag_Timbang_Keluar = 'Y' and a.lokasi =  '" & CmbPembelian_Lokasi.Text & "' "
        'SD_Pilih_PO2.ShowDialog()
    End Sub

    Private Sub BtnPembelian_Refresh_Click(sender As Object, e As EventArgs) Handles BtnPembelian_Refresh.Click
        Kosong()
    End Sub

    Private Sub BtnPembelian_Simpan_Click(sender As Object, e As EventArgs) Handles BtnPembelian_Simpan.Click
        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur()
            Generate_Faktur_Pelunasan()
            Get_No_Faktur_Pengajuan()

            If LvPembelian_DataPembelian.Items.Count = 0 Then
                MessageBox.Show(Base_Language.Lang_Global_Error_Lv_Kosong, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            ElseIf TxtPembelian_NoPO.Text.Trim.Length = 0 Then
                MessageBox.Show(Base_Language.Lang_Global_Error_No_PO, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TxtPembelian_NoPO.Focus() : Exit Sub
            ElseIf CmbPembelian_JnsBayar.SelectedIndex = 1 Then
                'If CmbPembelian_RangeBayar.Text.Trim.Length = 0 Then
                '    MessageBox.Show(Base_Language.Lang_Global_Error_Jatuh_Tempo, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    TxtPembelian_NoPO.Focus() : Exit Sub
                'End If
            End If
            Dim cb As String = ""
            'If arrJnsB_Byr.Item(CmbPembelian_JnsBayar.SelectedIndex) = "N" Then
            cb = "NULL"
            'Else
            '    cb = "'" & arrCrByr.Item(CmbPembelian_CaraBayar.SelectedIndex) & "'"
            'End If

            SQL = "INSERT INTO EMI_Pembelian(Kode_Perusahaan,No_Faktur,Lokasi,Tanggal,Jam,"
            SQL = SQL & "No_PO,No_Nota,Jenis_Pembayaran,Tgl_Jatuh_Tempo,Cara_Bayar,Mata_Uang,"
            SQL = SQL & "Kurs,Total_MUA,Total_IDR,Grand_Sebelum_PPN,PPN,Nilai_PPN,Grand,UserID, Nilai_PPH, Nilai_DP) "
            SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtPembelian_NoFaktur.Text & "',"
            SQL = SQL & "'" & CmbPembelian_Lokasi.Text & "',"
            SQL = SQL & "'" & Format(DtpPembelian_Tgl.Value, "yyyy-MM-dd") & "',"
            SQL = SQL & "'" & Format(CDate(tgl_skg), "HH:mm:ss") & "',"
            SQL = SQL & "'" & TxtPembelian_NoPO.Text & "','" & "No Nota" & "',"
            SQL = SQL & "'" & arrJnsB_Byr.Item(CmbPembelian_JnsBayar.SelectedIndex) & "',"
            'If CmbPembelian_JnsBayar.SelectedIndex = 1 Then
            '    SQL = SQL & "'" & Format(DtpPembelian_TglBayar.Value, "yyyy-MM-dd") & "',"
            'Else
            SQL = SQL & "null,"
            'End If
            SQL = SQL & "" & cb & ","
            SQL = SQL & "'" & CmbPembelian_MataUang.Text & "',"
            SQL = SQL & "'" & HilangkanTanda(TxtPembelian_Kurs.Text) & "',"
            SQL = SQL & "'" & HilangkanTanda(TxtPembelian_TotalMUA.Text) & "',"
            SQL = SQL & "'" & HilangkanTanda(TxtPembelian_TotalIDR.Text) & "',"
            SQL = SQL & "'" & HilangkanTanda(TxtPembelian_TotalSblmPPN.Text) & "',"
            SQL = SQL & "'" & HilangkanTanda(TxtPembelian_PersenPPN.Text) & "',"
            SQL = SQL & "'" & HilangkanTanda(TxtPembelian_NilaiPPN.Text) & "',"
            SQL = SQL & "'" & HilangkanTanda(TxtPembelian_GrandTotal.Text) & "',"
            SQL = SQL & "'" & UserID & "', '" & HilangkanTanda(Txt_GrandPPH.Text) & "', '" & HilangkanTanda(Txt_PembayaranDimuka.Text) & "')"
            ExecuteTrans(SQL)


            '==========================
            '=     GET DETAIL PPH     =
            '==========================
            For i As Integer = 0 To tempDataPajak.Count - 1

                '=============================
                '=     INSERT DETAIL PPH     =
                '=============================
                Dim totalPajak As Double = Math.Round(HilangkanTanda(TxtPembelian_TotalIDR.Text) * (tempDataPajak(i).Nilai / 100))


                SQL = "insert into EMI_Detail_PPH_Pembelian (Kode_Perusahaan, No_faktur, No_faktur_PO, Persentase, Nilai, Kode_Tarif, Flag_PPN, Kode_Akun ) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text & "', '" & TxtPembelian_NoPO.Text & "', '" & tempDataPajak(i).Nilai & "', '" & HilangkanTanda(totalPajak) & "', '" & tempDataPajak(i).Pajak & "', "
                SQL = SQL & If(tempDataPajak(i).isPPN, "'Y'", "NULL") & ", '" & tempDataPajak(i).akun & "')"
                ExecuteTrans(SQL)


            Next

            For a As Integer = 0 To LvPembelian_DataPembelian.Items.Count - 1
                Get_Isi_Listview(a)
                SQL = "INSERT INTO EMI_Pembelian_Detail(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,"
                SQL = SQL & "Kode_Barang,Serial_Number,Harga,Jumlah,Satuan,Disc_Persen,Nilai_Disc,"
                SQL = SQL & "Total,Pakai_SN,Modal,Upd_HPP,Sisa,Total_Harga, "
                SQL = SQL & "Jumlah_Masuk, Harga_Akhir, PPN, Persen_PPN,urut_loading,no_loading, Jumlah_Hutang) VALUES("
                SQL = SQL & "'" & KodePerusahaan & "','" & TxtPembelian_NoFaktur.Text & "',"
                SQL = SQL & "'" & LvSo & "','" & LvKd_Brg & "','" & LvSerial & "',"
                SQL = SQL & "'" & HilangkanTanda(LvHargaReal) & "','" & HilangkanTanda(LvJml_Brg) & "',"
                SQL = SQL & "'" & LvSatuan & "','" & HilangkanTanda(LvDisc_Persen) & "',"
                SQL = SQL & "'" & HilangkanTanda(LvDisc_Rp) & "','" & HilangkanTanda(LvTotal) & "',"
                SQL = SQL & "'" & LvPakai_SN & "','" & HilangkanTanda(LvModal) & "',"
                SQL = SQL & "'" & HilangkanTanda(LvUpd_Hpp) & "','" & HilangkanTanda(LvSisa) & "',"
                SQL = SQL & "'" & HilangkanTanda(LvTtl_Harga) & "', '" & HilangkanTanda(LvJml_Msk) & "', "
                SQL = SQL & "'" & HilangkanTanda(LvHarga) & "', '" & HilangkanTanda(LvPPN) & "', '" & HilangkanTanda(LvPersenPPN) & "', "
                SQL = SQL & "'" & LvUrutLoading & "', '" & LvNoLoading & "', '" & HilangkanTanda(LvJmlHutang) & "'  )"
                ExecuteTrans(SQL)

                Dim No_Selisih As String = ""
                Dim Flag_Masuk_Hutang As String = ""
                SQL = "Select No_Faktur, Flag_Masuk_Hutang_Bahan from EMI_Pembelian_Selisih_Barang_Masuk "
                SQL = SQL & "where No_Faktur_BM ='" & LvNo_PO & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and Flag_Validasi is null and status is null "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        No_Selisih = Dr("No_Faktur")
                        Flag_Masuk_Hutang = General_Class.CekNULL(Dr("Flag_Masuk_Hutang_Bahan"))
                        Dr.Close()

                        SQL = "Update EMI_Pembelian_Selisih_Barang_Masuk_det set Flag_Validasi='Y' where "
                        SQL = SQL & "no_faktur ='" & No_Selisih & "' and kode_barang='" & LvKd_Brg & "' "
                        SQL = SQL & "And Kode_Perusahaan ='" & KodePerusahaan & "' "
                        ExecuteTrans(SQL)

                        SQL = "Update EMI_Pembelian_loading_detail set Flag_Pembelian='Y' where "
                        SQL = SQL & "no_faktur ='" & LvNo_PO & "' and kode_barang='" & LvKd_Brg & "' "
                        SQL = SQL & "And Kode_Perusahaan ='" & KodePerusahaan & "' "
                        ExecuteTrans(SQL)

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Tidak Di temukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                If Flag_Masuk_Hutang = "Y" Then
                    SQL = "Update EMI_Pembelian_loading_detail set Flag_Masuk_Hutang='Y' where "
                    SQL = SQL & "no_faktur ='" & LvNo_PO & "' and kode_barang='" & LvKd_Brg & "' "
                    SQL = SQL & "And Kode_Perusahaan ='" & KodePerusahaan & "' "
                    ExecuteTrans(SQL)
                End If

                SQL = "Select Kode_Perusahaan from EMI_Pembelian_loading_detail "
                SQL = SQL & "where No_Faktur ='" & LvNo_PO & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and Flag_Pembelian is null "
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        SQL = "Update EMI_Pembelian_loading set Flag_Pembelian='Y' where "
                        SQL = SQL & "no_faktur ='" & LvNo_PO & "' "
                        SQL = SQL & "And Kode_Perusahaan ='" & KodePerusahaan & "' "
                        ExecuteTrans(SQL)

                    End If
                End Using


#Region "Update Harga"

                SQL = "select Kode_Perusahaan, No_Faktur, No_Pembelian_Loading, Serial_Number as SN_Lama, Nilai_Barang as JumlahMasukKecil, Jumlah_Bags "
                SQL = SQL & "from EMI_Barang_Masuk_Perpallet a "
                SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Pembelian_Loading = '" & LvNo_PO & "' "
                'SQL = SQL & "and Flag_angkut = 'Y'"
                SQL = SQL & "and kode_barang = '" & LvKd_Brg & "' and Serial_Number_Import is null and a.status is null "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            For k As Integer = 0 To Ds.Tables("MyTable").Rows.Count - 1

                                If IsDBNull(.Rows(k).Item("SN_Lama")) Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang Masuk Belum Selesai . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                                '==========================
                                '=     INSERT SN BARU     =
                                '==========================
                                Dim Total_HPP As Double = Val(HilangkanTanda(LvHarga))

                                Dim tgl_skg2 As DateTime
                                SQL = "declare @ab int; declare @ac int; select @ab = Selisih_Jam, @ac= expired_proforma from Init; "
                                SQL = SQL & " Select FORMAT(DATEADD(hh, @ab, getdate()), 'yyyy-MM-dd HH:mm:ss')  as Tanggal_Sekarang , @ac as expired"
                                Using dr = OpenTrans(SQL)
                                    Do While dr.Read
                                        tgl_skg2 = dr("Tanggal_Sekarang")

                                    Loop
                                End Using

                                Dim Random As New Random()
                                Dim str As String = Format(Random.Next(0, 9999), "0000") & Format(tgl_skg2, "HHmmss")
                                Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                                Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & Total_HPP & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg2, "yyyy-MM-dd")

                                SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah, Warna, Jumlah_Bags, Lama, thn, Tgl_Expired, Tgl_Produksi, "
                                SQL = SQL & "Bulan, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, rr, Id_Nametag_pallet, Batch_Number, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, Flag_QI, Tgl_Masuk) "
                                SQL = SQL & "select Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, '" & SN_Baru & "', Jumlah, Warna, Jumlah_Bags, Lama, thn, Tgl_Expired, Tgl_Produksi, Bulan, Stock_PO, "
                                SQL = SQL & "Stock_Inquiry, Id_Warehouse, id_Susunan, rr, Id_Nametag_pallet, Batch_Number, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, Flag_QI, Tgl_Masuk "
                                SQL = SQL & "from Barang_SN where Serial_Number = '" & .Rows(k).Item("SN_Lama") & "' "
                                ExecuteTrans(SQL)

                                '========================
                                '=     POTONG STOCK     =
                                '========================
                                SQL = "update Barang_SN set Jumlah = Jumlah - " & .Rows(k).Item("JumlahMasukKecil") & ", Jumlah_Bags = Jumlah_Bags - " & .Rows(k).Item("Jumlah_Bags") & " where Serial_Number = '" & .Rows(k).Item("SN_Lama") & "' "
                                ExecuteTrans(SQL)

                                '==========================================
                                '=     CEK APAKAH BARANG_SN MENJADI 0     =
                                '==========================================
                                Dim kode_SO As String = ""
                                SQL = "select Jumlah, Kode_Stock_Owner from Barang_SN where Serial_Number = '" & .Rows(k).Item("SN_Lama") & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        kode_SO = Dr("Kode_Stock_Owner")

                                        If Val(Dr("Jumlah")) <> 0 Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terdapat Kesalahan saat Potong Stock : Jumlah tidak 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terdapat Kesalahan saat Potong Stock", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                '==============================
                                '=     CEK KESESUAIAN DATA    =
                                '==============================
                                SQL = "select a.Kode_Barang, a.Nama, "
                                SQL = SQL & "ISNULL(ROUND(sum(Good_Stock), 2), 0) as GoodStock, "
                                SQL = SQL & "ISNULL(ROUND(sum(Jumlah_Bags), 2), 0) as Bags_Barang, "
                                SQL = SQL & "ISNULL((select ROUND(sum(z.Jumlah),2) from Barang_SN z where  a.Kode_Perusahaan=z.Kode_Perusahaan "
                                SQL = SQL & "and a.kode_barang=z.Kode_Barang and a.Kode_Stock_Owner=z.Kode_Stock_Owner), 0) as Jumlah_Sn, "
                                SQL = SQL & "ISNULL((select ROUND(sum(z.Jumlah_Bags),2) from Barang_SN z where  a.Kode_Perusahaan=z.Kode_Perusahaan "
                                SQL = SQL & "and a.kode_barang=z.Kode_Barang and a.Kode_Stock_Owner=z.Kode_Stock_Owner), 0) as Bags_Sn "
                                SQL = SQL & "from barang a "
                                SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' and a.Kode_Stock_Owner='" & kode_SO & "' "
                                SQL = SQL & "and a.Kode_Barang='" & LvKd_Brg & "' "
                                SQL = SQL & "group by a.Kode_Barang, a.Nama, a.Kode_Perusahaan, a.Kode_Stock_Owner "
                                Using Ds2 = BindingTrans(SQL)
                                    With Ds2.Tables("MyTable")
                                        If .Rows.Count <> 0 Then
                                            If Ds2.Tables("MyTable").Rows(0).Item("GoodStock") <> Ds2.Tables("MyTable").Rows(0).Item("Jumlah_Sn") Or Ds2.Tables("MyTable").Rows(0).Item("Bags_Barang") <> Ds2.Tables("MyTable").Rows(0).Item("Bags_Sn") Then
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
                                    End With
                                End Using

                                '==========================
                                '=     INSERT SN BARU     =
                                '==========================
                                SQL = "update EMI_Barang_Masuk_Perpallet set Serial_Number_Import = '" & SN_Baru & "' where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and No_Faktur = '" & .Rows(k).Item("No_Faktur") & "' and No_Pembelian_Loading = '" & LvNo_PO & "'"
                                ExecuteTrans(SQL)

                            Next

                        End If
                    End With
                End Using

#End Region


            Next



            For a As Integer = 0 To ListViewDet.Items.Count - 1
                Get_Isi_Listview_Det(a)
                SQL = "INSERT INTO EMI_Pembelian_Detail2(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,"
                SQL = SQL & "Kode_Barang,Serial_Number,Harga,Jumlah,Satuan,Disc_Persen,Nilai_Disc,"
                SQL = SQL & "Total,Pakai_SN,Modal,Upd_HPP,Sisa,Total_Harga, "
                SQL = SQL & "Jumlah_Masuk, Harga_Akhir, PPN, Persen_PPN, Jumlah_Hutang) VALUES("
                SQL = SQL & "'" & KodePerusahaan & "','" & TxtPembelian_NoFaktur.Text & "',"
                SQL = SQL & "'" & LvSoDet & "','" & LvKd_BrgDet & "','" & LvSerialDet & "',"
                SQL = SQL & "'" & HilangkanTanda(LvHargaRealDet) & "','" & HilangkanTanda(LvJml_BrgDet) & "',"
                SQL = SQL & "'" & LvSatuanDet & "','" & HilangkanTanda(LvDisc_PersenDet) & "',"
                SQL = SQL & "'" & HilangkanTanda(LvDisc_RpDet) & "','" & HilangkanTanda(LvTotalDet) & "',"
                SQL = SQL & "'" & LvPakai_SNDet & "','" & HilangkanTanda(LvModalDet) & "',"
                SQL = SQL & "'" & HilangkanTanda(LvUpd_HppDet) & "','" & HilangkanTanda(LvSisaDet) & "',"
                SQL = SQL & "'" & HilangkanTanda(LvTtl_HargaDet) & "', '" & HilangkanTanda(LvJml_MskDet) & "', "
                SQL = SQL & "'" & HilangkanTanda(LvHargaDet) & "', '" & HilangkanTanda(LvPPNDet) & "', '" & HilangkanTanda(LvPersenPPNDet) & "', '" & HilangkanTanda(LvJmlHutangDet) & "'  "
                SQL = SQL & ")"
                ExecuteTrans(SQL)

            Next

            Dim flag_kategori_Supplier As String = ""
            SQL = "select b.kode_supplier, b.ID_Kategori_Suppliers, c.flag_jenis_import  from  Suppliers b, Suppliers_Kategori c "
            SQL = SQL & "where "
            SQL = SQL & "b.kode_perusahaan = c.kode_perusahaan and b.id_kategori_suppliers = c.id_kategori_suppliers "
            SQL = SQL & "and b.kode_perusahaan = '" & KodePerusahaan & "' and b.Kode_Supplier = '" & TxtPembelian_KdSupplier.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    flag_kategori_Supplier = General_Class.CekNULL(Dr("flag_jenis_import"))
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseTransB2B()
                    CloseConn()
                    CloseConnB2B()
                    MessageBox.Show("Supplier Tidak Ditemukan...!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub

                End If
            End Using

            isError = False
            If flag_kategori_Supplier = "Y" Then

                Jurnal_Import()

            Else
                Jurnal_Lokal()
            End If

            If isError = False Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Ada Masalah pada Jurnal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


#Region "INPUT PELUNASAN"

#End Region


            '=============================
            '=     CEK APAKAH ADA DP     =
            '=============================
            Dim JumlahDp As Double = 0
            SQL = ";with Cte as ( "
            SQL = SQL & "select a.Nilai as Nilai_DP, ( "
            SQL = SQL & "(a.Nilai- "
            SQL = SQL & "isnull(( "
            SQL = SQL & "select sum(x.nilai) from EMI_Transaksi_Pembayaran_Dimuka_Pajak x "
            SQL = SQL & "where x.kode_perusahaan=a.kode_perusahaan and x.no_faktur = a.No_Transaksi and x.flag_ppn is null ),0)) - "
            SQL = SQL & "ISNULL(( "
            SQL = SQL & "select z.nilai from EMI_Pelunasan_Detail_DP z, emi_pelunasan w "
            SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan and z.urut_DP = a.No_Urut and "
            SQL = SQL & "z.kode_perusahaan=w.kode_Perusahaan and z.no_val=w.no_val and w.status is null "
            SQL = SQL & "), 0) ) as Sisa "
            SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka_Detail a, EMI_Transaksi_Pembayaran_Dimuka b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "And a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "And b.Status Is null "
            SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Fak_PO in ( "
            SQL = SQL & "select y.No_FakInduk "
            SQL = SQL & "from EMI_Pembelian_PO x, EMI_Pembelian_PO_Det y "
            SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan "
            SQL = SQL & "and x.No_Faktur = y.No_Faktur "
            SQL = SQL & "and x.Status is null "
            SQL = SQL & "and x.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and x.No_Faktur = '" & TxtPembelian_NoPO.Text & "' "
            SQL = SQL & "group by y.No_FakInduk ) "
            SQL = SQL & ")select isnull(sum(Sisa),0) as Nilai_DP from Cte "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    JumlahDp = Dr("Nilai_DP")
                End If
            End Using

            If JumlahDp > 0 Then
                If Not Insert_Pelunasan() Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Ada Masalah pada Jurnal DP", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End If


            ' TODO : SImpan


            If Val(HilangkanTanda(Txt_PembayaranDimuka.Text)) >= Val(HilangkanTanda(TxtPembelian_GrandTotal.Text)) Then
                SQL = "Update Emi_Pembelian set flag_lunas = 'Y', "
                SQL = SQL & "Tgl_lunas = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                SQL = SQL & "jam_lunas = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                SQL = SQL & "user_lunas = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' "
                ExecuteTrans(SQL)
            End If



            'If True Then
            '    CloseTrans()
            '    CloseConn()
            '    MessageBox.Show("TAHAN", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If



            SQL = "update EMI_Pembelian_PO set flag_Pembelian='Y' where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & TxtPembelian_NoPO.Text & "'"
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Kosong()
        EMI_PO_Pembelian_Display2.Cari("Y")
        Me.Close()
    End Sub

    Private Sub CmbPembelian_JnsBayar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPembelian_JnsBayar.SelectedIndexChanged
        If CmbPembelian_JnsBayar.SelectedIndex = 0 Then
            DtpPembelian_TglBayar.Visible = False
            CmbPembelian_RangeBayar.Visible = False
        Else
            DtpPembelian_TglBayar.Visible = True
            CmbPembelian_RangeBayar.Visible = True
        End If
    End Sub

    Private Sub CmbPembelian_RangeBayar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPembelian_RangeBayar.SelectedIndexChanged
        Dim xx As Integer = 0
        If CmbPembelian_RangeBayar.SelectedIndex = -1 Then
            xx = 0
        Else
            xx = Val(CmbPembelian_RangeBayar.Text)
        End If
        DtpPembelian_TglBayar.Value = DateAdd(DateInterval.Day, xx, DtpPembelian_Tgl.Value)
    End Sub





    Private Sub TxtPembelian_NoPO_TextChanged(sender As Object, e As EventArgs) Handles TxtPembelian_NoPO.TextChanged

    End Sub

    Private Sub Jurnal_Import()

        'TODO : JURNAL IMPORT

        '=== INISIAL FAKTUR UNTUK JURNAL ======
        Dim inisial_faktur_dari As String
        SQL = "select inisial_faktur from stock_owner "
        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & CmbPembelian_Lokasi.Text & "' "
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
        Dim coa_ppn As String = ""
        Dim Metode_Hitung_Konte As String = ""

        Dim Flag_Average_Sup As String = ""

        SQL = "select hutang_pph_billing, pph_billing, Metode_Hitung_Konte, Hutang_Dalam_Proses, Selisih_Hutang_Import, Hutang_Billing_Import, "
        SQL = SQL & "Hutang_Storage_Import, Hutang_Freight_Import, Akun_Tot_Pot_Stock, "
        SQL = SQL & "Akun_Tdk_Pot_Stock, Akun_Tdk_Pot_Stock_Hutang_Utama, PPN_Pembelian, "
        SQL = SQL & "Akun_Tdk_Pot_Stock_Hutang_Penolong, Akun_pph, akun_pib, akun_selisih_pib, Akun_Selisih_AVG_Import, Akun_Selisih_PO, Akun_Selisih_PO_Biaya from stock_Owner "
        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Kode_stock_Owner = '" & CmbPembelian_Lokasi.Text & "' "
        Using dr = OpenTrans(SQL)
            If dr.Read Then
                Metode_Hitung_Konte = dr("Metode_Hitung_Konte")
                ' coa_Hutang_Dalam_Proses = dr("Hutang_Dalam_Proses")
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
                coa_ppn = dr("PPN_Pembelian")
            Else
                dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Lokasi Tidak ditemukan")
                Exit Sub
            End If
        End Using

        Dim akun_hutang_sup_selisih As String = ""
        Dim akun_hutang_perjalanan_selisih As String = ""

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
        SQL = SQL & "'" & KodeProyek & "', 'Pembelian " & TxtPembelian_NoFaktur.Text & "', '', "
        SQL = SQL & "'-', '" & UserID & "')"
        ExecuteTrans(SQL)

#Region "Declare PO"

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

#End Region

#Region "Declare Selisih"

        Dim Arr_Biaya_Import_Master_selisih As New ArrayList
        Dim Arr_Biaya_Import_selisih As New ArrayList
        Dim Arr_Biaya_Import_AVG_selisih As New ArrayList
        Dim Arr_Akun1_selisih As New ArrayList
        Dim Arr_Akun2_selisih As New ArrayList
        Dim Arr_Biaya_Import_Kategori_selisih As New ArrayList

        Dim Arr_Biaya_Bongkar_Import_Master_selisih As New ArrayList
        Dim Arr_Biaya_Bongkar_Import_selisih As New ArrayList
        Dim Arr_Lokasi_Bongkar_Import_selisih As New ArrayList
        Dim Arr_Akun1_Bongkar_selisih As New ArrayList
        Dim Arr_Akun2_Bongkar_selisih As New ArrayList
        Dim Arr_Biaya_Bongkar_Import_Kategori_selisih As New ArrayList

        Dim Biaya_Import_AVG_selisih As Double = 0
        Dim Selisih_Import_AVG_selisih As Double = 0
        Dim Biaya_Import_Total_selisih As Double = 0
        Dim Hutang_Dalam_Proses_selisih As Double = 0
        Dim Selisih_Hutang_selisih As Double = 0
        Dim Biaya_PPN_selisih As Double = 0
        Dim Billing_selisih As Double = 0
        Dim pib_selisih As Double = 0
        Dim pph_billing_selisih As Double = 0
        Dim BM_Billing_selisih As Double = 0
        Dim Selisih_PO_selisih As Double = 0
        Dim Selisih_PO_Biaya_selisih As Double = 0

        Dim freigt_selisih As Double = 0
        Dim Storage_selisih As Double = 0
        Dim Tot_Pot_Stock_IDR_selisih As Double = 0
        Dim Tdk_Pot_Stock_IDR_selisih As Double = 0
        Dim Tdk_Pot_Stock_Hutang_IDR_Utama_selisih As Double = 0
        Dim Tdk_Pot_Stock_Hutang_IDR_Penolong_selisih As Double = 0
        Dim pph_pakai_persentase_selisih As Double = 0

#End Region

        Dim ket As String
        For index = 0 To LvPembelian_DataPembelian.Items.Count - 1
            Get_Isi_Listview(index)

            SQL = "select c.akun_Persediaan "
            SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
            SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.kode_stock_owner = '" & LvSo & "' and b.Kode_Barang='" & LvKd_Brg & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    coa_Hutang_Dalam_Proses = Dr("akun_Persediaan")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=== GET DATA RENCANA ORDER, BERDASAR NO LOADING ===
            Dim id_rencana As Integer
            Dim Lokasi_Jurnal As String
            Dim id_rencana_group As String = ""

#Region "Get_ID_Rencana"

            SQL = "select Lokasi, No_Fak_HPP, b.ID_Rencana "
            SQL = SQL & "from emi_pembelian_loading a, HPP_Import b where "
            SQL = SQL & "a.Kode_Perusahaan=b.kode_perusahaan and a.No_Fak_HPP=b.No_Faktur "
            SQL = SQL & "and a.status is null and b.status is null "
            SQL = SQL & "and a.No_Faktur = '" & LvNo_PO & "' "
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

#End Region

            Dim lokasi_Barang As String = ""

            SQL = "select kode_stock_owner from EMI_Barang_Masuk_Perpallet a where "
            SQL = SQL & "Kode_Perusahaan='" & KodePerusahaan & "' and "
            SQL = SQL & "No_Pembelian_Loading='" & LvNo_PO & "' and status is null "
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

            SQL = "select c.akun_Persediaan "
            SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
            SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.kode_stock_owner = '" & LvSo & "' and b.Kode_Barang='" & LvKd_Brg & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    coa_Hutang_Dalam_Proses = Dr("akun_Persediaan")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

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

#Region "GET_KET"

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
                    For indx As Integer = 0 To .Rows.Count - 1
                        ix = .Rows.Count

                        SQL = "select top(1) d.Kode_Kategori_Besar from "
                        SQL = SQL & "submit_Po A, Detail_Submit_PO B, Barang C, Kategori_Besar d where "
                        SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur "
                        SQL = SQL & "and B.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner= "
                        SQL = SQL & "c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and "
                        SQL = SQL & "c.Kode_Perusahaan = d.Kode_Perusahaan And c.Kode_Kategori_Besar = d.Kode_Kategori_Besar "
                        SQL = SQL & "and id_rencana in(" & id_rencana_group & ")  and d.Jenis = '" & .Rows(indx).Item("Jenis") & "' and a.status is null "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                If indx <> 0 Then
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

            ket = Strings.Left(TxtPembelian_NoFaktur.Text & "; " & PO_Induk & "; " & Konte_group & "; " & Kategori_Group & "; " & Lokasi_Group, 180)

#End Region

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

            Dim HPP_BM As String = ""
            Dim HPP_PPH As String = ""
            Dim HPP_PPN As String = ""
            Dim HPP_FormE As String = ""
            Dim HPP_29 As String = ""
            Dim HPP_STORAGE As String = ""

            SQL = "select flag_HPP, keterangan from binding_hpp where "
            SQL = SQL & "kode_Perusahaan ='" & KodePerusahaan & "'  "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    If Dr("keterangan") = "BILLING BM" Then
                        HPP_BM = Dr("flag_HPP")
                    End If

                    If Dr("keterangan") = "BILLING PPH" Then
                        HPP_PPH = Dr("flag_HPP")
                    End If

                    If Dr("keterangan") = "BILLING PPN" Then
                        HPP_PPN = Dr("flag_HPP")
                    End If

                    If Dr("keterangan") = "FORM-E" Then
                        HPP_FormE = Dr("flag_HPP")
                    End If

                    If Dr("keterangan") = "PPH 29" Then
                        HPP_29 = Dr("flag_HPP")
                    End If

                    If Dr("keterangan") = "STORAGE" Then
                        HPP_STORAGE = Dr("flag_HPP")
                    End If

                Loop
            End Using

            Dim jmlhMasuk As Double = Val(HilangkanTanda(LvJml_Msk))
            Dim jmlhPO As Double = Val(HilangkanTanda(LvJml_Brg))
            Dim selisih_Barang_Masuk As Double = jmlhMasuk - jmlhPO
            Dim Satuan As String = LvSatuan

#Region "Data PO"

            Dim Arr_Biaya_Import_Master_temp As New ArrayList
            Dim Arr_Biaya_Import_temp As New ArrayList
            Dim Arr_Biaya_Import_AVG_temp As New ArrayList
            Dim Arr_Akun1_temp As New ArrayList
            Dim Arr_Akun2_temp As New ArrayList
            Dim Arr_Biaya_Import_Kategori_temp As New ArrayList

            Dim Arr_Biaya_Bongkar_Import_Master_temp As New ArrayList
            Dim Arr_Biaya_Bongkar_Import_temp As New ArrayList
            Dim Arr_Lokasi_Bongkar_Import_temp As New ArrayList
            Dim Arr_Akun1_Bongkar_temp As New ArrayList
            Dim Arr_Akun2_Bongkar_temp As New ArrayList
            Dim Arr_Biaya_Bongkar_Import_Kategori_temp As New ArrayList

            Dim Biaya_Import_AVG_temp As Double = 0
            Dim Selisih_Import_AVG_temp As Double = 0
            Dim Biaya_Import_Total_temp As Double = 0
            Dim Hutang_Dalam_Proses_temp As Double = 0
            Dim Selisih_Hutang_temp As Double = 0
            Dim Biaya_PPN_temp As Double = 0
            Dim Billing_temp As Double = 0
            Dim pib_temp As Double = 0
            Dim pph_billing_temp As Double = 0
            Dim BM_Billing_temp As Double = 0
            Dim Selisih_PO_temp As Double = 0
            Dim Selisih_PO_Biaya_temp As Double = 0

            Dim freigt_temp As Double = 0
            Dim Storage_temp As Double = 0
            Dim Tot_Pot_Stock_IDR_temp As Double = 0
            Dim Tdk_Pot_Stock_IDR_temp As Double = 0
            Dim Tdk_Pot_Stock_Hutang_IDR_Utama_temp As Double = 0
            Dim Tdk_Pot_Stock_Hutang_IDR_Penolong_temp As Double = 0
            Dim pph_pakai_persentase_temp As Double = 0

            SQL = "select a.No_faktur, a.ID_Rencana, b.Kode_stock_owner, b.Kode_barang, b.jumlah,"
            SQL = SQL & "b.Nilai_Pot_Stock/Jumlah as Nilai_Pot_Stock, Nilai_Tdk_Pot_stock_LNS/Jumlah as Nilai_Tdk_Pot_stock_LNS,"
            SQL = SQL & "b.Nilai_tdk_pot_stock_htg_utama/Jumlah as Nilai_tdk_pot_stock_htg_utama,"
            SQL = SQL & "b.Nilai_Tdk_Pot_Stock_HTG_Penolong/Jumlah as Nilai_Tdk_Pot_Stock_HTG_Penolong,"
            SQL = SQL & "b.PPH29/Jumlah as PPH29,Biaya_Billing/Jumlah as Biaya_Billing,Biaya_Kontainer/Jumlah as Biaya_Kontainer, b.Nilai_Selisih_PO/Jumlah as Nilai_Selisih_PO, b.Nilai_Selisih_PO_Biaya/Jumlah as Nilai_Selisih_PO_Biaya "
            SQL = SQL & "from HPP_Import a, Detail_HPP_Import b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_faktur And a.Status Is null And id_rencana ='" & id_rencana & "' AND B.Kode_Barang='" & LvKd_Brg & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If HPP_STORAGE = "Y" Then
                        Storage = Storage + Math.Round((Dr("biaya_kontainer") * jmlhPO), 0)
                    End If

                    If HPP_29 = "Y" Then
                        pph_pakai_persentase = pph_pakai_persentase + Math.Round((Dr("PPH29") * jmlhPO), 0)
                    End If

                    Billing = Billing + Math.Round((Dr("Biaya_Billing") * jmlhPO), 0)
                    Tot_Pot_Stock_IDR = Tot_Pot_Stock_IDR + Math.Round((Dr("Nilai_Pot_Stock") * jmlhPO), 0)
                    Tdk_Pot_Stock_IDR = Tdk_Pot_Stock_IDR + Math.Round((Dr("Nilai_Tdk_Pot_stock_LNS") * jmlhPO), 0)
                    Tdk_Pot_Stock_Hutang_IDR_Utama = Tdk_Pot_Stock_Hutang_IDR_Utama + Math.Round((Dr("Nilai_tdk_pot_stock_htg_utama") * jmlhPO), 0)
                    Tdk_Pot_Stock_Hutang_IDR_Penolong = Tdk_Pot_Stock_Hutang_IDR_Penolong + Math.Round((Dr("Nilai_Tdk_Pot_Stock_HTG_Penolong") * jmlhPO), 0)

                    Selisih_PO = Selisih_PO + Math.Round((Dr("Nilai_Selisih_PO") * jmlhPO), 0)
                    Selisih_PO_Biaya = Selisih_PO_Biaya + Math.Round((Dr("Nilai_Selisih_PO_Biaya") * jmlhPO), 0)

                    '-------------- Ini Temp --------------------
                    If HPP_STORAGE = "Y" Then
                        Storage_temp = Math.Round((Dr("biaya_kontainer") * jmlhPO), 0)
                    End If

                    If HPP_29 = "Y" Then
                        pph_pakai_persentase_temp = Math.Round((Dr("PPH29") * jmlhPO), 0)
                    End If

                    Billing_temp = Math.Round((Dr("Biaya_Billing") * jmlhPO), 0)
                    Tot_Pot_Stock_IDR_temp = Math.Round((Dr("Nilai_Pot_Stock") * jmlhPO), 0)
                    Tdk_Pot_Stock_IDR_temp = Math.Round((Dr("Nilai_Tdk_Pot_stock_LNS") * jmlhPO), 0)
                    Tdk_Pot_Stock_Hutang_IDR_Utama_temp = Math.Round((Dr("Nilai_tdk_pot_stock_htg_utama") * jmlhPO), 0)
                    Tdk_Pot_Stock_Hutang_IDR_Penolong_temp = Math.Round((Dr("Nilai_Tdk_Pot_Stock_HTG_Penolong") * jmlhPO), 0)

                    Selisih_PO_temp = Math.Round((Dr("Nilai_Selisih_PO") * jmlhPO), 0)
                    Selisih_PO_Biaya_temp = Math.Round((Dr("Nilai_Selisih_PO_Biaya") * jmlhPO), 0)
                End If
            End Using

            SQL = "select b.kode_stock_owner, b.Kode_Barang, jumlah, b.Nilai_PPH/Jumlah as Nilai_PPH, Nilai_PPN/Jumlah as Nilai_PPN, Nilai_BM/Jumlah as Nilai_BM "
            SQL = SQL & "from Total_Billing a, Detail_Total_Billing b where a.Kode_Perusahaan=b.Kode_perusahaan and a.No_Faktur=b.No_Faktur "
            SQL = SQL & "and a.ID_Rencana='" & id_rencana & "' and a.Status is nulL AND B.Kode_Barang='" & LvKd_Brg & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    BM_Billing = BM_Billing + Math.Round((Dr("Nilai_BM") * jmlhPO), 0)
                    pib = pib + Math.Round((Dr("Nilai_PPN") * jmlhPO), 0)
                    pph_billing = pph_billing + Math.Round((Dr("Nilai_PPH") * jmlhPO), 0)

                    '-------------- Ini Temp --------------------
                    BM_Billing_temp = Math.Round((Dr("Nilai_BM") * jmlhPO), 0)
                    pib_temp = Math.Round((Dr("Nilai_PPN") * jmlhPO), 0)
                    pph_billing_temp = Math.Round((Dr("Nilai_PPH") * jmlhPO), 0)
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
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & id_rencana & "' and b.Flag_Masuk_HPP='Y' " 'and C.Flag_Gabungan = 'Y' "
            SQL = SQL & "group by b.Kode_Perusahaan, b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal "
            SQL = SQL & "order by b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import"
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For index3 As Integer = 0 To .Rows.Count - 1

                        Dim cek As Boolean = False
                        Dim cek_temp As Boolean = False
                        SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.Jumlah,"
                        SQL = SQL & "c.Kode_Kategori_Biaya_Import, c.Biaya/Jumlah as Biaya, c.Biaya_AVG/Jumlah as Biaya_AVG, c.Flag_Average "
                        SQL = SQL & "from hpp_import a, detail_hpp_import b,detail_hpp_import_biaya c where "
                        SQL = SQL & "a.kode_perusahaan=b.Kode_Perusahaan and a.No_faktur=b.No_Faktur and b.no_faktur=c.No_Faktur "
                        SQL = SQL & "and b.Kode_Barang=c.Kode_Barang and b.Kode_stock_owner=c.Kode_stock_owner and "
                        SQL = SQL & "c.Kode_kategori_biaya_import ='" & .Rows(index3).Item("Kode_kategori_biaya_import") & "' and c.Kode_Barang='" & LvKd_Brg & "' and a.Status Is null And a.id_rencana ='" & id_rencana & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then

                                For index1 As Integer = 0 To Arr_Biaya_Import_Kategori.Count - 1

                                    If Arr_Biaya_Import_Kategori.Item(index1) = .Rows(index3).Item("Kode_kategori_biaya_import") Then

                                        Arr_Biaya_Import.Item(index1) += Val(HilangkanTanda(Format(dr("Biaya") * jmlhPO, "N0")))

                                        Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format(dr("Biaya") * jmlhPO, "N0")))
                                        Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format(dr("Biaya_AVG") * jmlhPO, "N0")))
                                        cek = True
                                    End If

                                Next

                                If cek = False Then
                                    Arr_Biaya_Import_Master.Add(.Rows(index3).Item("Kode_Master_Kategori_Biaya_Import"))
                                    Arr_Biaya_Import_Kategori.Add(dr("Kode_Kategori_Biaya_Import"))
                                    Arr_Biaya_Import.Add(Val(HilangkanTanda(Format(dr("Biaya") * jmlhPO, "N0"))))
                                    Arr_Akun1.Add(.Rows(index3).Item("Akun_1"))
                                    Arr_Akun2.Add(.Rows(index3).Item("Akun_2"))

                                    Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format(dr("Biaya") * jmlhPO, "N0")))
                                    Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format(dr("Biaya_AVG") * jmlhPO, "N0")))
                                End If

                                '-------------- Ini Temp --------------------
                                For index1 As Integer = 0 To Arr_Biaya_Import_Kategori_temp.Count - 1

                                    If Arr_Biaya_Import_Kategori_temp.Item(index1) = .Rows(index3).Item("Kode_kategori_biaya_import") Then

                                        Arr_Biaya_Import_temp.Item(index1) += Val(HilangkanTanda(Format(dr("Biaya") * jmlhPO, "N0")))

                                        Biaya_Import_Total_temp = Biaya_Import_Total_temp + Val(HilangkanTanda(Format(dr("Biaya") * jmlhPO, "N0")))
                                        Biaya_Import_AVG_temp = Biaya_Import_AVG_temp + Val(HilangkanTanda(Format(dr("Biaya_AVG") * jmlhPO, "N0")))
                                        cek_temp = True
                                    End If

                                Next

                                If cek_temp = False Then
                                    Arr_Biaya_Import_Master_temp.Add(.Rows(index3).Item("Kode_Master_Kategori_Biaya_Import"))
                                    Arr_Biaya_Import_Kategori_temp.Add(dr("Kode_Kategori_Biaya_Import"))
                                    Arr_Biaya_Import_temp.Add(Val(HilangkanTanda(Format(dr("Biaya") * jmlhPO, "N0"))))
                                    Arr_Akun1_temp.Add(.Rows(index3).Item("Akun_1"))
                                    Arr_Akun2_temp.Add(.Rows(index3).Item("Akun_2"))

                                    Biaya_Import_Total_temp = Biaya_Import_Total_temp + Val(HilangkanTanda(Format(dr("Biaya") * jmlhPO, "N0")))
                                    Biaya_Import_AVG_temp = Biaya_Import_AVG_temp + Val(HilangkanTanda(Format(dr("Biaya_AVG") * jmlhPO, "N0")))
                                End If
                            End If
                        End Using

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
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & id_rencana & "' and b.kode_stock_owner='" & Lokasi_Jurnal & "' and b.Flag_Masuk_HPP='Y' " ' and C.Flag_Gabungan = 'T' "
            SQL = SQL & "group by b.Kode_Perusahaan, b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal, B.Kode_Stock_Owner "
            SQL = SQL & "order by b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import"
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For index3 As Integer = 0 To .Rows.Count - 1

                        Dim cek As Boolean = False
                        Dim cek_temp As Boolean = False
                        SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.Jumlah, "
                        SQL = SQL & "c.Kode_Kategori_Biaya_Import, c.Biaya2/Jumlah as Biaya2, c.Biaya_AVG2/Jumlah as Biaya_AVG2, "
                        SQL = SQL & "c.Biayawetdry/Jumlah as Biayawetdry, c.Biayawetdry_AVG/Jumlah as Biayawetdry_AVG, c.Flag_Average "
                        SQL = SQL & "from hpp_import a, detail_hpp_import2 b,detail_hpp_import2_biaya c where "
                        SQL = SQL & "a.kode_perusahaan=b.Kode_Perusahaan and a.No_faktur=b.No_Faktur and b.no_faktur=c.No_Faktur "
                        SQL = SQL & "and b.Kode_Barang=c.Kode_Barang and b.Kode_stock_owner=c.Kode_stock_owner and b.Lokasi_Tujuan=c.lokasi_tujuan and "
                        SQL = SQL & "c.Kode_kategori_biaya_import ='" & .Rows(index3).Item("Kode_kategori_biaya_import") & "' and c.Kode_Barang='" & LvKd_Brg & "' and c.Lokasi_tujuan ='" & Lokasi_Jurnal & "'  and a.Status Is null And a.id_rencana ='" & id_rencana & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then

                                For index1 As Integer = 0 To Arr_Biaya_Bongkar_Import.Count - 1

                                    If Arr_Biaya_Bongkar_Import_Kategori.Item(index1) = .Rows(index3).Item("Kode_kategori_biaya_import") Then

                                        Arr_Biaya_Bongkar_Import.Item(index1) += Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * jmlhPO, "N0")))

                                        Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * jmlhPO, "N0")))
                                        Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format((dr("Biaya_AVG2") + dr("Biayawetdry_AVG")) * jmlhPO, "N0")))
                                        cek = True
                                    End If

                                Next

                                If cek = False Then

                                    Arr_Biaya_Bongkar_Import_Master.Add(.Rows(index3).Item("Kode_Master_Kategori_Biaya_Import"))
                                    Arr_Biaya_Bongkar_Import_Kategori.Add(.Rows(index3).Item("Kode_Kategori_Biaya_Import"))
                                    Arr_Biaya_Bongkar_Import.Add(Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * jmlhPO, "N0"))))
                                    Arr_Lokasi_Bongkar_Import.Add(Lokasi_Jurnal)
                                    Arr_Akun1_Bongkar.Add(.Rows(index3).Item("Akun_1"))
                                    Arr_Akun2_Bongkar.Add(.Rows(index3).Item("Akun_2"))

                                    Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * jmlhPO, "N0")))
                                    Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format((dr("Biaya_AVG2") + dr("Biayawetdry_AVG")) * jmlhPO, "N0")))
                                End If

                                '-------------- Ini Temp --------------------
                                For index1 As Integer = 0 To Arr_Biaya_Bongkar_Import_temp.Count - 1

                                    If Arr_Biaya_Bongkar_Import_Kategori_temp.Item(index1) = .Rows(index3).Item("Kode_kategori_biaya_import") Then

                                        Arr_Biaya_Bongkar_Import_temp.Item(index1) += Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * jmlhPO, "N0")))

                                        Biaya_Import_Total_temp = Biaya_Import_Total_temp + Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * jmlhPO, "N0")))
                                        Biaya_Import_AVG_temp = Biaya_Import_AVG_temp + Val(HilangkanTanda(Format((dr("Biaya_AVG2") + dr("Biayawetdry_AVG")) * jmlhPO, "N0")))
                                        cek_temp = True
                                    End If

                                Next

                                If cek_temp = False Then

                                    Arr_Biaya_Bongkar_Import_Master_temp.Add(.Rows(index3).Item("Kode_Master_Kategori_Biaya_Import"))
                                    Arr_Biaya_Bongkar_Import_Kategori_temp.Add(.Rows(index3).Item("Kode_Kategori_Biaya_Import"))
                                    Arr_Biaya_Bongkar_Import_temp.Add(Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * jmlhPO, "N0"))))
                                    Arr_Lokasi_Bongkar_Import_temp.Add(Lokasi_Jurnal)
                                    Arr_Akun1_Bongkar_temp.Add(.Rows(index3).Item("Akun_1"))
                                    Arr_Akun2_Bongkar_temp.Add(.Rows(index3).Item("Akun_2"))

                                    Biaya_Import_Total_temp = Biaya_Import_Total_temp + Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * jmlhPO, "N0")))
                                    Biaya_Import_AVG_temp = Biaya_Import_AVG_temp + Val(HilangkanTanda(Format((dr("Biaya_AVG2") + dr("Biayawetdry_AVG")) * jmlhPO, "N0")))
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
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & id_rencana & "' and b.Flag_Masuk_HPP='Y'  "
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
                        SQL = SQL & "c.Kode_kategori_biaya_import ='" & .Rows(index3).Item("Kode_kategori_biaya_import") & "' and c.Kode_Barang='" & LvKd_Brg & "'  and a.Status Is null And a.id_rencana ='" & id_rencana & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then

                                freigt = freigt + Val(HilangkanTanda(Format(dr("Biaya") * jmlhPO, "N0")))
                                Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format(dr("Biaya_AVG") * jmlhPO, "N0")))

                                '-------------- Ini Temp --------------------
                                freigt_temp = freigt_temp + Val(HilangkanTanda(Format(dr("Biaya") * jmlhPO, "N0")))
                                Biaya_Import_AVG_temp = Biaya_Import_AVG_temp + Val(HilangkanTanda(Format(dr("Biaya_AVG") * jmlhPO, "N0")))

                            End If
                        End Using

                    Next
                End With

            End Using

            Hutang_Dalam_Proses = Hutang_Dalam_Proses + Math.Round((jmlhPO * Val(HilangkanTanda(LvHarga))), 0)
            Hutang_Dalam_Proses_temp = Math.Round((jmlhPO * Val(HilangkanTanda(LvHarga))), 0)

#End Region

#Region "Jurnal PO"

            'Persediaan

#Region "Persediaan"

            'Jurnal Persediaan
            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Hutang_Dalam_Proses & "' and debit <> 0 "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'update

                    SQL = "update detail_jurnal set debit = debit+ " & Hutang_Dalam_Proses_temp & " where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Hutang_Dalam_Proses & "' and debit <> 0 "
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    'insert

                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Hutang_Dalam_Proses, 1),
                    Strings.Mid(coa_Hutang_Dalam_Proses, 2, 1),
                    Strings.Mid(Ganti(coa_Hutang_Dalam_Proses), 3),
                    KodePerusahaan, KodeProyek, ket + "; PERSEDIAAN; " + TxtPembelian_NmSupplier.Text.Trim, Hutang_Dalam_Proses_temp, "0", pagenumber, LvSo, Bahasa_Pilihan, Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                End If
            End Using

            'Data Persediaan
            SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
            SQL = SQL & "Kode = 'PERSEDIAAN' and "
            SQL = SQL & "Kode_Akun = '" & coa_Hutang_Dalam_Proses & "'  "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'update

                    SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Hutang_Dalam_Proses_temp & " where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                    SQL = SQL & "Kode = 'PERSEDIAAN' and "
                    SQL = SQL & "Kode_Akun = '" & coa_Hutang_Dalam_Proses & "'  "
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    'insert

                    SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                    SQL = SQL & "'PERSEDIAAN', '" & Hutang_Dalam_Proses_temp & "', "
                    SQL = SQL & "'" & coa_Hutang_Dalam_Proses & "')"
                    ExecuteTrans(SQL)

                End If
            End Using

#End Region

            '---------------------------------------------------------------------------------------------------------------------

#Region "PPN"

            If pib_temp <> 0 Then 'ada ppn

                'Jurnal PPN
                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_ppn & "' and debit <> 0 "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set debit = debit+ " & pib_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_ppn & "' and debit <> 0 "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_ppn, 1),
                            Strings.Mid(coa_ppn, 2, 1),
                            Strings.Mid(Ganti(coa_ppn), 3),
                            KodePerusahaan, KodeProyek, ket + "; PPN; " + TxtPembelian_NmSupplier.Text.Trim, pib_temp, "0", pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                    End If
                End Using

                'Data PPN
                SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                SQL = SQL & "Kode = 'PPN' and "
                SQL = SQL & "Kode_Akun = '" & coa_ppn & "'  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & pib_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = 'PPN' and "
                        SQL = SQL & "Kode_Akun = '" & coa_ppn & "'  "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                        SQL = SQL & "'PPN', '" & pib_temp & "', "
                        SQL = SQL & "'" & coa_ppn & "')"
                        ExecuteTrans(SQL)

                    End If
                End Using

            End If

#End Region

            '---------------------------------------------------------------------------------------------------------------------

            'Hutang

#Region "Biaya Import 1"

            For indexKategoriImport As Integer = 0 To Arr_Biaya_Import_Master_temp.Count - 1

                SQL = "select* from Detail_Account_Master where "
                SQL = SQL & "Lokasi = '" & CmbPembelian_Lokasi.Text & "' and Kode_master_Kategori_biaya_import = '" & Arr_Biaya_Import_Master_temp.Item(indexKategoriImport) & "' "
                SQL = SQL & "and Akun_1 ='" & Arr_Akun1_temp.Item(indexKategoriImport) & "'  and Akun_2 = '" & Arr_Akun2_temp.Item(indexKategoriImport) & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Akun " & Arr_Biaya_Import_Master_temp.Item(indexKategoriImport) & " Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End Using

            Next

            For indexKategoriImport As Integer = 0 To Arr_Biaya_Import_Master_temp.Count - 1
                If Val(Arr_Biaya_Import_temp.Item(indexKategoriImport)) <> 0 Then

                    'Jurnal Kategori Biaya Import
                    SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Arr_Akun2_temp.Item(indexKategoriImport) & "' and kredit <> 0 "
                    SQL = SQL & "and keterangan ='" & ket & "; " & Arr_Biaya_Import_Kategori_temp.Item(indexKategoriImport) & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            'update

                            SQL = "update detail_jurnal set kredit = kredit+ " & Arr_Biaya_Import_temp.Item(indexKategoriImport) & " where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Arr_Akun2_temp.Item(indexKategoriImport) & "' and kredit <> 0 "
                            SQL = SQL & "and keterangan ='" & ket & "; " & Arr_Biaya_Import_Kategori_temp.Item(indexKategoriImport) & "'"
                            ExecuteTrans(SQL)
                        Else
                            Dr.Close()
                            'insert

                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Arr_Akun2_temp.Item(indexKategoriImport), 1),
                                Strings.Mid(Arr_Akun2_temp.Item(indexKategoriImport), 2, 1),
                                Strings.Mid(Ganti(Arr_Akun2_temp.Item(indexKategoriImport)), 3),
                                KodePerusahaan, KodeProyek, ket & "; " & Arr_Biaya_Import_Kategori_temp.Item(indexKategoriImport) & TxtPembelian_NmSupplier.Text.Trim, "0", Arr_Biaya_Import_temp.Item(indexKategoriImport), pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                            ExecuteTrans(SQL)
                            pagenumber = pagenumber + 1

                        End If
                    End Using

                    'Data Kategori Biaya Import
                    SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                    SQL = SQL & "Kode = '" & Arr_Biaya_Import_Kategori_temp.Item(indexKategoriImport) & "' and "
                    SQL = SQL & "Kode_Akun = '" & Arr_Akun2_temp.Item(indexKategoriImport) & "'  "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            'update

                            SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Arr_Biaya_Import_temp.Item(indexKategoriImport) & " where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                            SQL = SQL & "Kode = '" & Arr_Biaya_Import_Kategori_temp.Item(indexKategoriImport) & "' and "
                            SQL = SQL & "Kode_Akun = '" & Arr_Akun2_temp.Item(indexKategoriImport) & "'  "
                            ExecuteTrans(SQL)
                        Else
                            Dr.Close()
                            'insert

                            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                            SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                            SQL = SQL & "'" & Arr_Biaya_Import_Kategori_temp.Item(indexKategoriImport) & "', '" & Arr_Biaya_Import_temp.Item(indexKategoriImport) & "', "
                            SQL = SQL & "'" & Arr_Akun2_temp.Item(indexKategoriImport) & "')"
                            ExecuteTrans(SQL)

                        End If
                    End Using
                End If
            Next

#End Region

            '---------------------------------------------------------------------------------------------------------------------

#Region "Biaya Import 2"

            For indexKategoriImport As Integer = 0 To Arr_Biaya_Bongkar_Import_Master_temp.Count - 1

                SQL = "select* from Detail_Account_Master where "
                SQL = SQL & "Lokasi = '" & CmbPembelian_Lokasi.Text & "' and Kode_master_Kategori_biaya_import = '" & Arr_Biaya_Bongkar_Import_Master_temp.Item(indexKategoriImport) & "' "
                SQL = SQL & "and Akun_1 ='" & Arr_Akun1_Bongkar_temp.Item(indexKategoriImport) & "'  and Akun_2 = '" & Arr_Akun2_Bongkar_temp.Item(indexKategoriImport) & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Akun " & Arr_Biaya_Bongkar_Import_Master_temp.Item(indexKategoriImport) & " Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End Using

            Next

            For indexKategoriImport As Integer = 0 To Arr_Biaya_Bongkar_Import_Master_temp.Count - 1
                Dim ket2 As String = Strings.Left(ket & "; " & Arr_Biaya_Bongkar_Import_Kategori_temp.Item(index) & "-" & Arr_Lokasi_Bongkar_Import_temp.Item(index), 180)
                If Val(Arr_Biaya_Bongkar_Import_temp.Item(indexKategoriImport)) <> 0 Then

                    'Jurnal Kategori Biaya Import
                    SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Arr_Akun2_Bongkar_temp.Item(indexKategoriImport) & "' and kredit <> 0 "
                    SQL = SQL & "and keterangan ='" & ket2 & " '"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            'update

                            SQL = "update detail_jurnal set kredit = kredit+ " & Arr_Biaya_Bongkar_Import_temp.Item(indexKategoriImport) & " where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Arr_Akun2_Bongkar_temp.Item(indexKategoriImport) & "' and kredit <> 0 "
                            SQL = SQL & "and keterangan ='" & ket2 & "' "
                            ExecuteTrans(SQL)
                        Else
                            Dr.Close()
                            'insert

                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Arr_Akun2_Bongkar_temp.Item(indexKategoriImport), 1),
                                Strings.Mid(Arr_Akun2_Bongkar_temp.Item(indexKategoriImport), 2, 1),
                                Strings.Mid(Ganti(Arr_Akun2_Bongkar_temp.Item(indexKategoriImport)), 3),
                                KodePerusahaan, KodeProyek, ket2 & "; " & TxtPembelian_NmSupplier.Text.Trim, "0", Arr_Biaya_Import_temp.Item(indexKategoriImport), pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                            ExecuteTrans(SQL)
                            pagenumber = pagenumber + 1

                        End If
                    End Using

                    'Data Kategori Biaya Import
                    SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                    SQL = SQL & "Kode = '" & Arr_Biaya_Bongkar_Import_Kategori_temp.Item(indexKategoriImport) & "' and "
                    SQL = SQL & "Kode_Akun = '" & Arr_Akun2_Bongkar_temp.Item(indexKategoriImport) & "'  "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            'update

                            SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Arr_Biaya_Bongkar_Import_temp.Item(indexKategoriImport) & " where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                            SQL = SQL & "Kode = '" & Arr_Biaya_Bongkar_Import_Kategori_temp.Item(indexKategoriImport) & "' and "
                            SQL = SQL & "Kode_Akun = '" & Arr_Akun2_Bongkar_temp.Item(indexKategoriImport) & "'  "
                            ExecuteTrans(SQL)
                        Else
                            Dr.Close()
                            'insert

                            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                            SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                            SQL = SQL & "'" & Arr_Biaya_Bongkar_Import_Kategori_temp.Item(indexKategoriImport) & "', '" & Arr_Biaya_Bongkar_Import_temp.Item(indexKategoriImport) & "', "
                            SQL = SQL & "'" & Arr_Akun2_Bongkar_temp.Item(indexKategoriImport) & "')"
                            ExecuteTrans(SQL)

                        End If
                    End Using
                End If
            Next

#End Region

            '---------------------------------------------------------------------------------------------------------------------

#Region "Storage"

            If Storage_temp <> 0 Then 'ada ppn

                'Jurnal STORAGE
                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Storage & "' and kredit <> 0 "
                SQL = SQL & "and keterangan ='" & ket & "; STORAGE" & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set kredit = kredit+ " & Storage_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Storage & "' and kredit <> 0 "
                        SQL = SQL & "and keterangan ='" & ket & "; STORAGE" & "' "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Storage, 1),
                            Strings.Mid(coa_Storage, 2, 1),
                            Strings.Mid(Ganti(coa_Storage), 3),
                            KodePerusahaan, KodeProyek, ket & "; STORAGE; " & TxtPembelian_NmSupplier.Text.Trim, "0", Storage_temp, pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                    End If
                End Using

                'Data STORAGE
                SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                SQL = SQL & "Kode = 'STORAGE' and "
                SQL = SQL & "Kode_Akun = '" & coa_Storage & "'  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Storage_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = 'STORAGE' and "
                        SQL = SQL & "Kode_Akun = '" & coa_Storage & "'  "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                        SQL = SQL & "'STORAGE', '" & Storage_temp & "', "
                        SQL = SQL & "'" & coa_Storage & "')"
                        ExecuteTrans(SQL)

                    End If
                End Using

            End If

#End Region

            '---------------------------------------------------------------------------------------------------------------------

#Region "Freight"

            If freigt_temp > 0 Then

                'Jurnal FREIGHT
                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_freigt & "' and kredit <> 0 "
                SQL = SQL & "and keterangan ='" & ket & "; FREIGHT" & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set kredit = kredit+ " & freigt_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_freigt & "' and kredit <> 0 "
                        SQL = SQL & "and keterangan ='" & ket & "; FREIGHT" & "' "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_freigt, 1),
                            Strings.Mid(coa_freigt, 2, 1),
                            Strings.Mid(Ganti(coa_freigt), 3),
                            KodePerusahaan, KodeProyek, ket & "; FREIGHT; " & TxtPembelian_NmSupplier.Text.Trim, "0", freigt_temp, pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                    End If
                End Using

                'Data FREIGHT
                SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                SQL = SQL & "Kode = 'FREIGHT' and "
                SQL = SQL & "Kode_Akun = '" & coa_freigt & "'  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & freigt_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = 'FREIGHT' and "
                        SQL = SQL & "Kode_Akun = '" & coa_freigt & "'  "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                        SQL = SQL & "'FREIGHT', '" & freigt_temp & "', "
                        SQL = SQL & "'" & coa_freigt & "')"
                        ExecuteTrans(SQL)

                    End If
                End Using

            End If

#End Region

            '---------------------------------------------------------------------------------------------------------------------

#Region "Billing"

            If Billing_temp > 0 Then

                'Jurnal BILLING
                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Billing & "' and kredit <> 0 "
                SQL = SQL & "and keterangan ='" & ket & "; BILLING" & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set kredit = kredit+ " & Billing_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Billing & "' and kredit <> 0 "
                        SQL = SQL & "and keterangan ='" & ket & "; BILLING" & "' "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Billing, 1),
                            Strings.Mid(coa_Billing, 2, 1),
                            Strings.Mid(Ganti(coa_Billing), 3),
                            KodePerusahaan, KodeProyek, ket & "; BILLING; " & TxtPembelian_NmSupplier.Text.Trim, "0", Billing_temp, pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                    End If
                End Using

                'Data BILLING
                SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                SQL = SQL & "Kode = 'BILLING' and "
                SQL = SQL & "Kode_Akun = '" & coa_Billing & "'  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Billing_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = 'BILLING' and "
                        SQL = SQL & "Kode_Akun = '" & coa_Billing & "'  "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                        SQL = SQL & "'BILLING', '" & Billing_temp & "', "
                        SQL = SQL & "'" & coa_Billing & "')"
                        ExecuteTrans(SQL)

                    End If
                End Using

            End If

#End Region

            '---------------------------------------------------------------------------------------------------------------------

#Region "PIB"

            If pib_temp <> 0 Then

                'Jurnal PIB
                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_pib & "' and kredit <> 0 "
                SQL = SQL & "and keterangan ='" & ket & "; PIB" & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set kredit = kredit+ " & pib_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_pib & "' and kredit <> 0 "
                        SQL = SQL & "and keterangan ='" & ket & "; PIB" & "' "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_pib, 1),
                            Strings.Mid(coa_pib, 2, 1),
                            Strings.Mid(Ganti(coa_pib), 3),
                            KodePerusahaan, KodeProyek, ket & "; PIB; " & TxtPembelian_NmSupplier.Text.Trim, "0", pib_temp, pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                    End If
                End Using

                'Data PIB
                SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                SQL = SQL & "Kode = 'PIB' and "
                SQL = SQL & "Kode_Akun = '" & coa_pib & "'  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & pib_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = 'PIB' and "
                        SQL = SQL & "Kode_Akun = '" & coa_pib & "'  "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                        SQL = SQL & "'PIB', '" & pib_temp & "', "
                        SQL = SQL & "'" & coa_pib & "')"
                        ExecuteTrans(SQL)

                    End If
                End Using

            End If

#End Region

            '---------------------------------------------------------------------------------------------------------------------

#Region "Bahan Baku"

            If Tdk_Pot_Stock_Hutang_IDR_Utama_temp <> 0 Then

                'Jurnal BAHAN UTAMA
                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Tdk_Pot_Stock_Hutang_IDR_Utama & "' and kredit <> 0 "
                SQL = SQL & "and keterangan ='" & ket + "; BAHAN UTAMA" & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set kredit = kredit+ " & Tdk_Pot_Stock_Hutang_IDR_Utama_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Tdk_Pot_Stock_Hutang_IDR_Utama & "' and kredit <> 0 "
                        SQL = SQL & "and keterangan ='" & ket + "; BAHAN UTAMA" & "' "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Tdk_Pot_Stock_Hutang_IDR_Utama, 1),
                            Strings.Mid(coa_Tdk_Pot_Stock_Hutang_IDR_Utama, 2, 1),
                            Strings.Mid(Ganti(coa_Tdk_Pot_Stock_Hutang_IDR_Utama), 3),
                            KodePerusahaan, KodeProyek, ket + "; BAHAN UTAMA; " & TxtPembelian_NmSupplier.Text.Trim, "0", Tdk_Pot_Stock_Hutang_IDR_Utama_temp, pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                    End If
                End Using

                'Data BAHAN UTAMA
                SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                SQL = SQL & "Kode = 'BAHAN UTAMA' and "
                SQL = SQL & "Kode_Akun = '" & coa_Tdk_Pot_Stock_Hutang_IDR_Utama & "'  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Tdk_Pot_Stock_Hutang_IDR_Utama_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = 'BAHAN UTAMA' and "
                        SQL = SQL & "Kode_Akun = '" & coa_Tdk_Pot_Stock_Hutang_IDR_Utama & "'  "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                        SQL = SQL & "'BAHAN UTAMA', '" & Tdk_Pot_Stock_Hutang_IDR_Utama_temp & "', "
                        SQL = SQL & "'" & coa_Tdk_Pot_Stock_Hutang_IDR_Utama & "')"
                        ExecuteTrans(SQL)

                    End If
                End Using

            End If

#End Region

            '---------------------------------------------------------------------------------------------------------------------

#End Region

#Region "Data Selisih"

            '-------------------- ################ ---------------------------------
            Dim Arr_Biaya_Import_Master_selisih_temp As New ArrayList
            Dim Arr_Biaya_Import_selisih_temp As New ArrayList
            Dim Arr_Biaya_Import_AVG_selisih_temp As New ArrayList
            Dim Arr_Akun1_selisih_temp As New ArrayList
            Dim Arr_Akun2_selisih_temp As New ArrayList
            Dim Arr_Biaya_Import_Kategori_selisih_temp As New ArrayList

            Dim Arr_Biaya_Bongkar_Import_Master_selisih_temp As New ArrayList
            Dim Arr_Biaya_Bongkar_Import_selisih_temp As New ArrayList
            Dim Arr_Lokasi_Bongkar_Import_selisih_temp As New ArrayList
            Dim Arr_Akun1_Bongkar_selisih_temp As New ArrayList
            Dim Arr_Akun2_Bongkar_selisih_temp As New ArrayList
            Dim Arr_Biaya_Bongkar_Import_Kategori_selisih_temp As New ArrayList

            Dim Biaya_Import_AVG_selisih_temp As Double = 0
            Dim Selisih_Import_AVG_selisih_temp As Double = 0
            Dim Biaya_Import_Total_selisih_temp As Double = 0
            Dim Hutang_Dalam_Proses_selisih_temp As Double = 0
            Dim Selisih_Hutang_selisih_temp As Double = 0
            Dim Biaya_PPN_selisih_temp As Double = 0
            Dim Billing_selisih_temp As Double = 0
            Dim pib_selisih_temp As Double = 0
            Dim pph_billing_selisih_temp As Double = 0
            Dim BM_Billing_selisih_temp As Double = 0
            Dim Selisih_PO_selisih_temp As Double = 0
            Dim Selisih_PO_Biaya_selisih_temp As Double = 0

            Dim freigt_selisih_temp As Double = 0
            Dim Storage_selisih_temp As Double = 0
            Dim Tot_Pot_Stock_IDR_selisih_temp As Double = 0
            Dim Tdk_Pot_Stock_IDR_selisih_temp As Double = 0
            Dim Tdk_Pot_Stock_Hutang_IDR_Utama_selisih_temp As Double = 0
            Dim Tdk_Pot_Stock_Hutang_IDR_Penolong_selisih_temp As Double = 0
            Dim pph_pakai_persentase_selisih_temp As Double = 0

            akun_hutang_sup_selisih = ""
            akun_hutang_perjalanan_selisih = ""

            SQL = "select Akun_Bahan, Akun_Perjalanan from EMI_Pembelian_Selisih_Barang_Masuk "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur_bm = '" & LvNo_PO & "' and status is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    akun_hutang_sup_selisih = Dr("Akun_Bahan")
                    akun_hutang_perjalanan_selisih = Dr("Akun_Perjalanan")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select a.No_faktur, a.ID_Rencana, b.Kode_stock_owner, b.Kode_barang, b.jumlah,"
            SQL = SQL & "b.Nilai_Pot_Stock/Jumlah as Nilai_Pot_Stock, Nilai_Tdk_Pot_stock_LNS/Jumlah as Nilai_Tdk_Pot_stock_LNS,"
            SQL = SQL & "b.Nilai_tdk_pot_stock_htg_utama/Jumlah as Nilai_tdk_pot_stock_htg_utama,"
            SQL = SQL & "b.Nilai_Tdk_Pot_Stock_HTG_Penolong/Jumlah as Nilai_Tdk_Pot_Stock_HTG_Penolong,"
            SQL = SQL & "b.PPH29/Jumlah as PPH29,Biaya_Billing/Jumlah as Biaya_Billing,Biaya_Kontainer/Jumlah as Biaya_Kontainer, b.Nilai_Selisih_PO/Jumlah as Nilai_Selisih_PO, b.Nilai_Selisih_PO_Biaya/Jumlah as Nilai_Selisih_PO_Biaya "
            SQL = SQL & "from HPP_Import a, Detail_HPP_Import b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_faktur And a.Status Is null And id_rencana ='" & id_rencana & "' AND B.Kode_Barang='" & LvKd_Brg & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If HPP_STORAGE = "Y" Then
                        Storage_selisih = Storage_selisih + (Dr("biaya_kontainer") * selisih_Barang_Masuk)
                    End If

                    If HPP_29 = "Y" Then
                        pph_pakai_persentase_selisih = pph_pakai_persentase_selisih + (Dr("PPH29") * selisih_Barang_Masuk)
                    End If

                    Billing_selisih = Billing_selisih + (Dr("Biaya_Billing") * selisih_Barang_Masuk)
                    Tot_Pot_Stock_IDR_selisih = Tot_Pot_Stock_IDR_selisih + (Dr("Nilai_Pot_Stock") * selisih_Barang_Masuk)
                    Tdk_Pot_Stock_IDR_selisih = Tdk_Pot_Stock_IDR_selisih + (Dr("Nilai_Tdk_Pot_stock_LNS") * selisih_Barang_Masuk)
                    Tdk_Pot_Stock_Hutang_IDR_Utama_selisih = Tdk_Pot_Stock_Hutang_IDR_Utama_selisih + (Dr("Nilai_tdk_pot_stock_htg_utama") * selisih_Barang_Masuk)
                    Tdk_Pot_Stock_Hutang_IDR_Penolong_selisih = Tdk_Pot_Stock_Hutang_IDR_Penolong_selisih + (Dr("Nilai_Tdk_Pot_Stock_HTG_Penolong") * selisih_Barang_Masuk)

                    Selisih_PO_selisih = Selisih_PO_selisih + (Dr("Nilai_Selisih_PO") * selisih_Barang_Masuk)
                    Selisih_PO_Biaya_selisih = Selisih_PO_Biaya_selisih + (Dr("Nilai_Selisih_PO_Biaya") * selisih_Barang_Masuk)

                    '-------------- Ini Temp --------------------

                    If HPP_STORAGE = "Y" Then
                        Storage_selisih_temp = (Dr("biaya_kontainer") * selisih_Barang_Masuk)
                    End If

                    If HPP_29 = "Y" Then
                        pph_pakai_persentase_selisih_temp = (Dr("PPH29") * selisih_Barang_Masuk)
                    End If

                    Billing_selisih_temp = (Dr("Biaya_Billing") * selisih_Barang_Masuk)
                    Tot_Pot_Stock_IDR_selisih_temp = (Dr("Nilai_Pot_Stock") * selisih_Barang_Masuk)
                    Tdk_Pot_Stock_IDR_selisih_temp = (Dr("Nilai_Tdk_Pot_stock_LNS") * selisih_Barang_Masuk)
                    Tdk_Pot_Stock_Hutang_IDR_Utama_selisih_temp = (Dr("Nilai_tdk_pot_stock_htg_utama") * selisih_Barang_Masuk)
                    Tdk_Pot_Stock_Hutang_IDR_Penolong_selisih_temp = (Dr("Nilai_Tdk_Pot_Stock_HTG_Penolong") * selisih_Barang_Masuk)

                    Selisih_PO_selisih_temp = (Dr("Nilai_Selisih_PO") * selisih_Barang_Masuk)
                    Selisih_PO_Biaya_selisih_temp = (Dr("Nilai_Selisih_PO_Biaya") * selisih_Barang_Masuk)
                End If
            End Using

            SQL = "select b.kode_stock_owner, b.Kode_Barang, jumlah, b.Nilai_PPH/Jumlah as Nilai_PPH, Nilai_PPN/Jumlah as Nilai_PPN, Nilai_BM/Jumlah as Nilai_BM "
            SQL = SQL & "from Total_Billing a, Detail_Total_Billing b where a.Kode_Perusahaan=b.Kode_perusahaan and a.No_Faktur=b.No_Faktur "
            SQL = SQL & "and a.ID_Rencana='" & id_rencana & "' and a.Status is nulL AND B.Kode_Barang='" & LvKd_Brg & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    BM_Billing_selisih = BM_Billing_selisih + (Dr("Nilai_BM") * selisih_Barang_Masuk)
                    pib_selisih = pib_selisih + (Dr("Nilai_PPN") * selisih_Barang_Masuk)
                    pph_billing_selisih = pph_billing_selisih + (Dr("Nilai_PPH") * selisih_Barang_Masuk)

                    '-------------- Ini Temp --------------------

                    BM_Billing_selisih_temp = (Dr("Nilai_BM") * selisih_Barang_Masuk)
                    pib_selisih_temp = (Dr("Nilai_PPN") * selisih_Barang_Masuk)
                    pph_billing_selisih_temp = (Dr("Nilai_PPH") * selisih_Barang_Masuk)
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
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & id_rencana & "' and b.Flag_Masuk_HPP='Y' " 'and C.Flag_Gabungan = 'Y' "
            SQL = SQL & "group by b.Kode_Perusahaan, b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal "
            SQL = SQL & "order by b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import"
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For index3 As Integer = 0 To .Rows.Count - 1

                        Dim cek As Boolean = False
                        Dim cek_temp As Boolean = False
                        SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.Jumlah,"
                        SQL = SQL & "c.Kode_Kategori_Biaya_Import, c.Biaya/Jumlah as Biaya, c.Biaya_AVG/Jumlah as Biaya_AVG, c.Flag_Average "
                        SQL = SQL & "from hpp_import a, detail_hpp_import b,detail_hpp_import_biaya c where "
                        SQL = SQL & "a.kode_perusahaan=b.Kode_Perusahaan and a.No_faktur=b.No_Faktur and b.no_faktur=c.No_Faktur "
                        SQL = SQL & "and b.Kode_Barang=c.Kode_Barang and b.Kode_stock_owner=c.Kode_stock_owner and "
                        SQL = SQL & "c.Kode_kategori_biaya_import ='" & .Rows(index3).Item("Kode_kategori_biaya_import") & "' and c.Kode_Barang='" & LvKd_Brg & "' and a.Status Is null And a.id_rencana ='" & id_rencana & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then

                                For index1 As Integer = 0 To Arr_Biaya_Import_Kategori_selisih.Count - 1

                                    If Arr_Biaya_Import_Kategori_selisih.Item(index1) = .Rows(index3).Item("Kode_kategori_biaya_import") Then

                                        Arr_Biaya_Import_selisih.Item(index1) += Val(HilangkanTanda(Format(dr("Biaya") * selisih_Barang_Masuk, "N0")))

                                        Biaya_Import_Total_selisih = Biaya_Import_Total_selisih + Val(HilangkanTanda(Format(dr("Biaya") * selisih_Barang_Masuk, "N0")))
                                        Biaya_Import_AVG_selisih = Biaya_Import_AVG_selisih + Val(HilangkanTanda(Format(dr("Biaya_AVG") * selisih_Barang_Masuk, "N0")))
                                        cek = True
                                    End If

                                Next

                                If cek = False Then
                                    Arr_Biaya_Import_Master_selisih.Add(.Rows(index3).Item("Kode_Master_Kategori_Biaya_Import"))
                                    Arr_Biaya_Import_Kategori_selisih.Add(dr("Kode_Kategori_Biaya_Import"))
                                    Arr_Biaya_Import_selisih.Add(Val(HilangkanTanda(Format(dr("Biaya") * selisih_Barang_Masuk, "N0"))))
                                    Arr_Akun1_selisih.Add(.Rows(index3).Item("Akun_1"))
                                    Arr_Akun2_selisih.Add(.Rows(index3).Item("Akun_2"))

                                    Biaya_Import_Total_selisih = Biaya_Import_Total_selisih + Val(HilangkanTanda(Format(dr("Biaya") * selisih_Barang_Masuk, "N0")))
                                    Biaya_Import_AVG_selisih = Biaya_Import_AVG_selisih + Val(HilangkanTanda(Format(dr("Biaya_AVG") * selisih_Barang_Masuk, "N0")))
                                End If

                                '-------------- Ini Temp --------------------

                                For index1 As Integer = 0 To Arr_Biaya_Import_Kategori_selisih_temp.Count - 1

                                    If Arr_Biaya_Import_Kategori_selisih_temp.Item(index1) = .Rows(index3).Item("Kode_kategori_biaya_import") Then

                                        Arr_Biaya_Import_selisih_temp.Item(index1) += Val(HilangkanTanda(Format(dr("Biaya") * selisih_Barang_Masuk, "N0")))

                                        Biaya_Import_Total_selisih_temp = Biaya_Import_Total_selisih_temp + Val(HilangkanTanda(Format(dr("Biaya") * selisih_Barang_Masuk, "N0")))
                                        Biaya_Import_AVG_selisih_temp = Biaya_Import_AVG_selisih_temp + Val(HilangkanTanda(Format(dr("Biaya_AVG") * selisih_Barang_Masuk, "N0")))
                                        cek_temp = True
                                    End If

                                Next

                                If cek_temp = False Then
                                    Arr_Biaya_Import_Master_selisih_temp.Add(.Rows(index3).Item("Kode_Master_Kategori_Biaya_Import"))
                                    Arr_Biaya_Import_Kategori_selisih_temp.Add(dr("Kode_Kategori_Biaya_Import"))
                                    Arr_Biaya_Import_selisih_temp.Add(Val(HilangkanTanda(Format(dr("Biaya") * selisih_Barang_Masuk, "N0"))))
                                    Arr_Akun1_selisih_temp.Add(.Rows(index3).Item("Akun_1"))
                                    Arr_Akun2_selisih_temp.Add(.Rows(index3).Item("Akun_2"))

                                    Biaya_Import_Total_selisih_temp = Biaya_Import_Total_selisih_temp + Val(HilangkanTanda(Format(dr("Biaya") * selisih_Barang_Masuk, "N0")))
                                    Biaya_Import_AVG_selisih_temp = Biaya_Import_AVG_selisih_temp + Val(HilangkanTanda(Format(dr("Biaya_AVG") * selisih_Barang_Masuk, "N0")))
                                End If
                            End If
                        End Using

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
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & id_rencana & "' and b.kode_stock_owner='" & Lokasi_Jurnal & "' and b.Flag_Masuk_HPP='Y' " ' and C.Flag_Gabungan = 'T' "
            SQL = SQL & "group by b.Kode_Perusahaan, b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal, B.Kode_Stock_Owner "
            SQL = SQL & "order by b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import"
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For index3 As Integer = 0 To .Rows.Count - 1

                        Dim cek As Boolean = False
                        Dim cek_temp As Boolean = False
                        SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.Jumlah, "
                        SQL = SQL & "c.Kode_Kategori_Biaya_Import, c.Biaya2/Jumlah as Biaya2, c.Biaya_AVG2/Jumlah as Biaya_AVG2, "
                        SQL = SQL & "c.Biayawetdry/Jumlah as Biayawetdry, c.Biayawetdry_AVG/Jumlah as Biayawetdry_AVG, c.Flag_Average "
                        SQL = SQL & "from hpp_import a, detail_hpp_import2 b,detail_hpp_import2_biaya c where "
                        SQL = SQL & "a.kode_perusahaan=b.Kode_Perusahaan and a.No_faktur=b.No_Faktur and b.no_faktur=c.No_Faktur "
                        SQL = SQL & "and b.Kode_Barang=c.Kode_Barang and b.Kode_stock_owner=c.Kode_stock_owner and b.Lokasi_Tujuan=c.lokasi_tujuan and "
                        SQL = SQL & "c.Kode_kategori_biaya_import ='" & .Rows(index3).Item("Kode_kategori_biaya_import") & "' and c.Kode_Barang='" & LvKd_Brg & "' and c.Lokasi_tujuan ='" & Lokasi_Jurnal & "'  and a.Status Is null And a.id_rencana ='" & id_rencana & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then

                                For index1 As Integer = 0 To Arr_Biaya_Bongkar_Import_selisih.Count - 1

                                    If Arr_Biaya_Bongkar_Import_Kategori_selisih.Item(index1) = .Rows(index3).Item("Kode_kategori_biaya_import") Then

                                        Arr_Biaya_Bongkar_Import_selisih.Item(index1) += Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * selisih_Barang_Masuk, "N0")))

                                        Biaya_Import_Total_selisih = Biaya_Import_Total_selisih + Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * selisih_Barang_Masuk, "N0")))
                                        Biaya_Import_AVG_selisih = Biaya_Import_AVG_selisih + Val(HilangkanTanda(Format((dr("Biaya_AVG2") + dr("Biayawetdry_AVG")) * selisih_Barang_Masuk, "N0")))
                                        cek = True
                                    End If

                                Next

                                If cek = False Then

                                    Arr_Biaya_Bongkar_Import_Master_selisih.Add(.Rows(index3).Item("Kode_Master_Kategori_Biaya_Import"))
                                    Arr_Biaya_Bongkar_Import_Kategori_selisih.Add(.Rows(index3).Item("Kode_Kategori_Biaya_Import"))
                                    Arr_Biaya_Bongkar_Import_selisih.Add(Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * selisih_Barang_Masuk, "N0"))))
                                    Arr_Lokasi_Bongkar_Import_selisih.Add(Lokasi_Jurnal)
                                    Arr_Akun1_Bongkar_selisih.Add(.Rows(index3).Item("Akun_1"))
                                    Arr_Akun2_Bongkar_selisih.Add(.Rows(index3).Item("Akun_2"))

                                    Biaya_Import_Total_selisih = Biaya_Import_Total_selisih + Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * selisih_Barang_Masuk, "N0")))
                                    Biaya_Import_AVG_selisih = Biaya_Import_AVG_selisih + Val(HilangkanTanda(Format((dr("Biaya_AVG2") + dr("Biayawetdry_AVG")) * selisih_Barang_Masuk, "N0")))
                                End If

                                '-------------- Ini Temp --------------------

                                For index1 As Integer = 0 To Arr_Biaya_Bongkar_Import_selisih_temp.Count - 1

                                    If Arr_Biaya_Bongkar_Import_Kategori_selisih_temp.Item(index1) = .Rows(index3).Item("Kode_kategori_biaya_import") Then

                                        Arr_Biaya_Bongkar_Import_selisih_temp.Item(index1) += Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * selisih_Barang_Masuk, "N0")))

                                        Biaya_Import_Total_selisih_temp = Biaya_Import_Total_selisih_temp + Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * selisih_Barang_Masuk, "N0")))
                                        Biaya_Import_AVG_selisih_temp = Biaya_Import_AVG_selisih_temp + Val(HilangkanTanda(Format((dr("Biaya_AVG2") + dr("Biayawetdry_AVG")) * selisih_Barang_Masuk, "N0")))
                                        cek_temp = True
                                    End If

                                Next

                                If cek_temp = False Then

                                    Arr_Biaya_Bongkar_Import_Master_selisih_temp.Add(.Rows(index3).Item("Kode_Master_Kategori_Biaya_Import"))
                                    Arr_Biaya_Bongkar_Import_Kategori_selisih_temp.Add(.Rows(index3).Item("Kode_Kategori_Biaya_Import"))
                                    Arr_Biaya_Bongkar_Import_selisih_temp.Add(Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * selisih_Barang_Masuk, "N0"))))
                                    Arr_Lokasi_Bongkar_Import_selisih_temp.Add(Lokasi_Jurnal)
                                    Arr_Akun1_Bongkar_selisih_temp.Add(.Rows(index3).Item("Akun_1"))
                                    Arr_Akun2_Bongkar_selisih_temp.Add(.Rows(index3).Item("Akun_2"))

                                    Biaya_Import_Total_selisih_temp = Biaya_Import_Total_selisih_temp + Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * selisih_Barang_Masuk, "N0")))
                                    Biaya_Import_AVG_selisih_temp = Biaya_Import_AVG_selisih_temp + Val(HilangkanTanda(Format((dr("Biaya_AVG2") + dr("Biayawetdry_AVG")) * selisih_Barang_Masuk, "N0")))
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
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & id_rencana & "' and b.Flag_Masuk_HPP='Y'  "
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
                        SQL = SQL & "c.Kode_kategori_biaya_import ='" & .Rows(index3).Item("Kode_kategori_biaya_import") & "' and c.Kode_Barang='" & LvKd_Brg & "'  and a.Status Is null And a.id_rencana ='" & id_rencana & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then

                                freigt_selisih = freigt_selisih + Val(HilangkanTanda(Format(dr("Biaya") * selisih_Barang_Masuk, "N0")))
                                Biaya_Import_AVG_selisih = Biaya_Import_AVG_selisih + Val(HilangkanTanda(Format(dr("Biaya_AVG") * selisih_Barang_Masuk, "N0")))

                                '-------------- Ini Temp --------------------

                                freigt_selisih_temp = freigt_selisih_temp + Val(HilangkanTanda(Format(dr("Biaya") * selisih_Barang_Masuk, "N0")))
                                Biaya_Import_AVG_selisih_temp = Biaya_Import_AVG_selisih_temp + Val(HilangkanTanda(Format(dr("Biaya_AVG") * selisih_Barang_Masuk, "N0")))

                            End If
                        End Using

                    Next
                End With

            End Using

            Hutang_Dalam_Proses_selisih = Hutang_Dalam_Proses_selisih + (selisih_Barang_Masuk * Val(HilangkanTanda(LvHarga)))
            Hutang_Dalam_Proses_selisih_temp = (selisih_Barang_Masuk * Val(HilangkanTanda(LvHarga)))

#End Region

#Region "Jurnal Selisih"

            'Persediaan

#Region "Persediaan"

            If Hutang_Dalam_Proses_selisih_temp > 0 Then
                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Hutang_Dalam_Proses & "' and debit <> 0 "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set debit = debit+ " & Hutang_Dalam_Proses_selisih_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Hutang_Dalam_Proses & "' and debit <> 0 "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Hutang_Dalam_Proses, 1),
                            Strings.Mid(coa_Hutang_Dalam_Proses, 2, 1),
                            Strings.Mid(Ganti(coa_Hutang_Dalam_Proses), 3),
                            KodePerusahaan, KodeProyek, ket & "; " & TxtPembelian_NmSupplier.Text.Trim, Hutang_Dalam_Proses_selisih_temp, "0", pagenumber, LvSo, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                    End If
                End Using

                SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                SQL = SQL & "Kode = 'PERSEDIAAN TAMBAHAN' and "
                SQL = SQL & "Kode_Akun = '" & coa_Hutang_Dalam_Proses & "'  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Hutang_Dalam_Proses_selisih_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = 'PERSEDIAAN TAMBAHAN' and "
                        SQL = SQL & "Kode_Akun = '" & coa_Hutang_Dalam_Proses & "'  "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                        SQL = SQL & "'PERSEDIAAN TAMBAHAN', '" & Hutang_Dalam_Proses_selisih_temp & "', "
                        SQL = SQL & "'" & coa_Hutang_Dalam_Proses & "')"
                        ExecuteTrans(SQL)

                    End If
                End Using

            ElseIf Hutang_Dalam_Proses_selisih_temp < 0 Then
                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Hutang_Dalam_Proses & "' and kredit <> 0 "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set kredit = kredit+ " & Math.Abs(Hutang_Dalam_Proses_selisih_temp) & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Hutang_Dalam_Proses & "' and kredit <> 0 "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Hutang_Dalam_Proses, 1),
                            Strings.Mid(coa_Hutang_Dalam_Proses, 2, 1),
                            Strings.Mid(Ganti(coa_Hutang_Dalam_Proses), 3),
                            KodePerusahaan, KodeProyek, ket & "; " & TxtPembelian_NmSupplier.Text.Trim, "0", Math.Abs(Hutang_Dalam_Proses_selisih_temp), pagenumber, LvSo, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                    End If
                End Using

                SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                SQL = SQL & "Kode = 'PERSEDIAAN TAMBAHAN' and "
                SQL = SQL & "Kode_Akun = '" & coa_Hutang_Dalam_Proses & "'  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Hutang_Dalam_Proses_selisih_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = 'PERSEDIAAN TAMBAHAN' and "
                        SQL = SQL & "Kode_Akun = '" & coa_Hutang_Dalam_Proses & "'  "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                        SQL = SQL & "'PERSEDIAAN TAMBAHAN', '" & Hutang_Dalam_Proses_selisih_temp & "', "
                        SQL = SQL & "'" & coa_Hutang_Dalam_Proses & "')"
                        ExecuteTrans(SQL)

                    End If
                End Using
            End If

#End Region

            '---------------------------------------------------------------------------------------------------------------------

            'Hutang

#Region "Biaya Import 1"

            For index_temp As Integer = 0 To Arr_Biaya_Import_Master_selisih_temp.Count - 1
                If Val(Arr_Biaya_Import_selisih_temp.Item(index_temp)) <> 0 Then
                    If Val(Arr_Biaya_Import_selisih_temp.Item(index_temp)) > 0 Then
                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and kredit <> 0 "
                        SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update

                                SQL = "update detail_jurnal set kredit = kredit+ " & Arr_Biaya_Import_selisih_temp.Item(index_temp) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and kredit <> 0 "
                                SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert

                                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_hutang_perjalanan_selisih, 1),
                                Strings.Mid(akun_hutang_perjalanan_selisih, 2, 1),
                                Strings.Mid(Ganti(akun_hutang_perjalanan_selisih), 3),
                                KodePerusahaan, KodeProyek, ket & "; BIAYA TAMBAHAN; " & TxtPembelian_NmSupplier.Text.Trim, "0", Arr_Biaya_Import_selisih_temp.Item(index_temp), pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                                pagenumber = pagenumber + 1

                            End If
                        End Using

                        SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                        SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update

                                SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Arr_Biaya_Import_selisih_temp.Item(index_temp) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                                SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                                SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert

                                SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                                SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                                SQL = SQL & "'BIAYA TAMBAHAN', '" & Arr_Biaya_Import_selisih_temp.Item(index_temp) & "', "
                                SQL = SQL & "'" & akun_hutang_perjalanan_selisih & "')"
                                ExecuteTrans(SQL)

                            End If
                        End Using

                    ElseIf Val(Arr_Biaya_Import_selisih_temp.Item(index_temp)) < 0 Then
                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and debit <> 0 "
                        SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update

                                SQL = "update detail_jurnal set debit = debit+ " & Math.Abs(Arr_Biaya_Import_selisih_temp.Item(index_temp)) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and debit <> 0 "
                                SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert

                                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_hutang_perjalanan_selisih, 1),
                                Strings.Mid(akun_hutang_perjalanan_selisih, 2, 1),
                                Strings.Mid(Ganti(akun_hutang_perjalanan_selisih), 3),
                                KodePerusahaan, KodeProyek, ket & "; BIAYA TAMBAHAN; " & TxtPembelian_NmSupplier.Text.Trim, Math.Abs(Arr_Biaya_Import_selisih_temp.Item(index_temp)), "0", pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                                pagenumber = pagenumber + 1

                            End If
                        End Using

                        SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                        SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update

                                SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Arr_Biaya_Import_selisih_temp.Item(index_temp) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                                SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                                SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert

                                SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                                SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                                SQL = SQL & "'BIAYA TAMBAHAN', '" & Arr_Biaya_Import_selisih_temp.Item(index_temp) & "', "
                                SQL = SQL & "'" & akun_hutang_perjalanan_selisih & "')"
                                ExecuteTrans(SQL)

                            End If
                        End Using
                    End If

                    'SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                    'SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                    'SQL = SQL & "'" & Arr_Biaya_Import_Kategori_selisih.Item(index_temp) & "', '" & Arr_Biaya_Import_selisih.Item(index_temp) & "', "
                    'SQL = SQL & "'" & akun_hutang_perjalanan_selisih & "')"
                    'ExecuteTrans(SQL)
                End If
            Next

#End Region

            '---------------------------------------------------------------------------------------------------------------------

#Region "Biaya Import 2"

            For index_temp As Integer = 0 To Arr_Biaya_Bongkar_Import_Master_selisih_temp.Count - 1
                Dim ket2 As String = Strings.Left(ket & "; " & Arr_Biaya_Bongkar_Import_Kategori_selisih_temp.Item(index_temp) & "-" & Arr_Lokasi_Bongkar_Import_selisih_temp.Item(index_temp), 180)
                If Arr_Biaya_Bongkar_Import_selisih_temp.Item(index_temp) <> 0 Then
                    If Arr_Biaya_Bongkar_Import_selisih_temp.Item(index_temp) > 0 Then
                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and kredit <> 0 "
                        SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update

                                SQL = "update detail_jurnal set kredit = kredit+ " & Arr_Biaya_Bongkar_Import_selisih_temp.Item(index_temp) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and kredit <> 0 "
                                SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert

                                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_hutang_perjalanan_selisih, 1),
                                    Strings.Mid(akun_hutang_perjalanan_selisih, 2, 1),
                                    Strings.Mid(Ganti(akun_hutang_perjalanan_selisih), 3),
                                    KodePerusahaan, KodeProyek, ket & "; BIAYA TAMBAHAN; " & TxtPembelian_NmSupplier.Text.Trim, "0", Arr_Biaya_Bongkar_Import_selisih_temp.Item(index_temp), pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                                pagenumber = pagenumber + 1

                            End If
                        End Using

                        SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                        SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update

                                SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Arr_Biaya_Bongkar_Import_selisih_temp.Item(index_temp) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                                SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                                SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert

                                SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                                SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                                SQL = SQL & "'BIAYA TAMBAHAN', '" & Arr_Biaya_Bongkar_Import_selisih_temp.Item(index_temp) & "', "
                                SQL = SQL & "'" & akun_hutang_perjalanan_selisih & "')"
                                ExecuteTrans(SQL)

                            End If
                        End Using
                    ElseIf Arr_Biaya_Bongkar_Import_selisih_temp.Item(index_temp) < 0 Then
                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and debit <> 0 "
                        SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update

                                SQL = "update detail_jurnal set debit = debit+ " & Math.Abs(Arr_Biaya_Bongkar_Import_selisih_temp.Item(index_temp)) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and debit <> 0 "
                                SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert

                                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_hutang_perjalanan_selisih, 1),
                                    Strings.Mid(akun_hutang_perjalanan_selisih, 2, 1),
                                    Strings.Mid(Ganti(akun_hutang_perjalanan_selisih), 3),
                                    KodePerusahaan, KodeProyek, ket & "; BIAYA TAMBAHAN; " & TxtPembelian_NmSupplier.Text.Trim, Math.Abs(Arr_Biaya_Bongkar_Import_selisih_temp.Item(index_temp)), "0", pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                                pagenumber = pagenumber + 1

                            End If
                        End Using

                        SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                        SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                        SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update

                                SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Arr_Biaya_Bongkar_Import_selisih_temp.Item(index_temp) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                                SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                                SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert

                                SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                                SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                                SQL = SQL & "'BIAYA TAMBAHAN', '" & Arr_Biaya_Bongkar_Import_selisih_temp.Item(index_temp) & "', "
                                SQL = SQL & "'" & akun_hutang_perjalanan_selisih & "')"
                                ExecuteTrans(SQL)

                            End If
                        End Using
                    End If

                    'SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                    'SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                    'SQL = SQL & "'" & Arr_Biaya_Bongkar_Import_Kategori_selisih.Item(index_temp) & "-" & Arr_Lokasi_Bongkar_Import_selisih.Item(index_temp) & "', '" & Arr_Biaya_Bongkar_Import.Item(index) & "', "
                    'SQL = SQL & "'" & akun_hutang_perjalanan_selisih & "')"
                    'ExecuteTrans(SQL)
                End If

            Next

#End Region

            '---------------------------------------------------------------------------------------------------------------------

#Region "Storage"

            If Storage_selisih_temp > 0 Then
                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and kredit <> 0 "
                SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set kredit = kredit+ " & Storage_selisih_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and kredit <> 0 "
                        SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_hutang_perjalanan_selisih, 1),
                        Strings.Mid(akun_hutang_perjalanan_selisih, 2, 1),
                        Strings.Mid(Ganti(akun_hutang_perjalanan_selisih), 3),
                        KodePerusahaan, KodeProyek, ket & "; BIAYA TAMBAHAN; " & TxtPembelian_NmSupplier.Text.Trim, "0", Storage_selisih_temp, pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                    End If
                End Using

                SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Storage_selisih_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                        SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                        SQL = SQL & "'BIAYA TAMBAHAN', '" & Storage_selisih_temp & "', "
                        SQL = SQL & "'" & akun_hutang_perjalanan_selisih & "')"
                        ExecuteTrans(SQL)

                    End If
                End Using

            ElseIf Storage_selisih_temp < 0 Then
                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and debit <> 0 "
                SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set debit = debit+ " & Math.Abs(Storage_selisih_temp) & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and debit <> 0 "
                        SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_hutang_perjalanan_selisih, 1),
                        Strings.Mid(akun_hutang_perjalanan_selisih, 2, 1),
                        Strings.Mid(Ganti(akun_hutang_perjalanan_selisih), 3),
                        KodePerusahaan, KodeProyek, ket & "; BIAYA TAMBAHAN; " & TxtPembelian_NmSupplier.Text.Trim, Math.Abs(Storage_selisih_temp), "0", pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                    End If
                End Using

                SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Storage_selisih_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                        SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                        SQL = SQL & "'BIAYA TAMBAHAN', '" & Storage_selisih_temp & "', "
                        SQL = SQL & "'" & akun_hutang_perjalanan_selisih & "')"
                        ExecuteTrans(SQL)

                    End If
                End Using

            End If

#End Region

            '---------------------------------------------------------------------------------------------------------------------

#Region "Freight"

            If freigt_selisih_temp > 0 Then
                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and kredit <> 0 "
                SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set kredit = kredit+ " & freigt_selisih_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and kredit <> 0 "
                        SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_hutang_perjalanan_selisih, 1),
                        Strings.Mid(akun_hutang_perjalanan_selisih, 2, 1),
                        Strings.Mid(Ganti(akun_hutang_perjalanan_selisih), 3),
                        KodePerusahaan, KodeProyek, ket & "; BIAYA TAMBAHAN; " & TxtPembelian_NmSupplier.Text.Trim, "0", freigt_selisih_temp, pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                    End If
                End Using

                SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & freigt_selisih_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                        SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                        SQL = SQL & "'BIAYA TAMBAHAN', '" & freigt_selisih_temp & "', "
                        SQL = SQL & "'" & akun_hutang_perjalanan_selisih & "')"
                        ExecuteTrans(SQL)

                    End If
                End Using

            ElseIf freigt_selisih_temp < 0 Then
                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and debit <> 0 "
                SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set debit = debit+ " & Math.Abs(freigt_selisih_temp) & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and debit <> 0 "
                        SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_hutang_perjalanan_selisih, 1),
                        Strings.Mid(akun_hutang_perjalanan_selisih, 2, 1),
                        Strings.Mid(Ganti(akun_hutang_perjalanan_selisih), 3),
                        KodePerusahaan, KodeProyek, ket & "; BIAYA TAMBAHAN; " & TxtPembelian_NmSupplier.Text.Trim, Math.Abs(freigt_selisih_temp), "0", pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                    End If
                End Using

                SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & freigt_selisih_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                        SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                        SQL = SQL & "'BIAYA TAMBAHAN', '" & freigt_selisih_temp & "', "
                        SQL = SQL & "'" & akun_hutang_perjalanan_selisih & "')"
                        ExecuteTrans(SQL)

                    End If
                End Using

            End If

#End Region

            '---------------------------------------------------------------------------------------------------------------------

#Region "Billing"

            If Billing_selisih_temp > 0 Then

                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and kredit <> 0 "
                SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set kredit = kredit+ " & Billing_selisih_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and kredit <> 0 "
                        SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_hutang_perjalanan_selisih, 1),
                        Strings.Mid(akun_hutang_perjalanan_selisih, 2, 1),
                        Strings.Mid(Ganti(akun_hutang_perjalanan_selisih), 3),
                        KodePerusahaan, KodeProyek, ket & "; BIAYA TAMBAHAN; " & TxtPembelian_NmSupplier.Text.Trim, "0", Billing_selisih_temp, pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                    End If
                End Using

                SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Billing_selisih_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                        SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                        SQL = SQL & "'BIAYA TAMBAHAN', '" & Billing_selisih_temp & "', "
                        SQL = SQL & "'" & akun_hutang_perjalanan_selisih & "')"
                        ExecuteTrans(SQL)

                    End If
                End Using

            ElseIf Billing_selisih_temp < 0 Then

                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and debit <> 0 "
                SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set debit = debit+ " & Math.Abs(Billing_selisih_temp) & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and debit <> 0 "
                        SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_hutang_perjalanan_selisih, 1),
                        Strings.Mid(akun_hutang_perjalanan_selisih, 2, 1),
                        Strings.Mid(Ganti(akun_hutang_perjalanan_selisih), 3),
                        KodePerusahaan, KodeProyek, ket & "; BIAYA TAMBAHAN; " & TxtPembelian_NmSupplier.Text.Trim, Math.Abs(Billing_selisih_temp), "0", pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                    End If
                End Using

                SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Billing_selisih_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                        SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                        SQL = SQL & "'BIAYA TAMBAHAN', '" & Billing_selisih_temp & "', "
                        SQL = SQL & "'" & akun_hutang_perjalanan_selisih & "')"
                        ExecuteTrans(SQL)

                    End If
                End Using

            End If

#End Region

            '---------------------------------------------------------------------------------------------------------------------

#Region "Bahan Baku"

            If Tdk_Pot_Stock_Hutang_IDR_Utama_selisih_temp > 0 Then

                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_sup_selisih & "' and kredit <> 0 "
                SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set kredit = kredit+ " & Tdk_Pot_Stock_Hutang_IDR_Utama_selisih_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_sup_selisih & "' and kredit <> 0 "
                        SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_hutang_sup_selisih, 1),
                        Strings.Mid(akun_hutang_sup_selisih, 2, 1),
                        Strings.Mid(Ganti(akun_hutang_sup_selisih), 3),
                        KodePerusahaan, KodeProyek, ket & "; BIAYA TAMBAHAN; " & TxtPembelian_NmSupplier.Text.Trim, "0", Tdk_Pot_Stock_Hutang_IDR_Utama_selisih_temp, pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                    End If
                End Using

                SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                SQL = SQL & "Kode_Akun = '" & akun_hutang_sup_selisih & "'  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Tdk_Pot_Stock_Hutang_IDR_Utama_selisih_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                        SQL = SQL & "Kode_Akun = '" & akun_hutang_sup_selisih & "'  "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                        SQL = SQL & "'BIAYA TAMBAHAN', '" & Tdk_Pot_Stock_Hutang_IDR_Utama_selisih_temp & "', "
                        SQL = SQL & "'" & akun_hutang_sup_selisih & "')"
                        ExecuteTrans(SQL)

                    End If
                End Using

            ElseIf Tdk_Pot_Stock_Hutang_IDR_Utama_selisih_temp < 0 Then

                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_sup_selisih & "' and debit <> 0 "
                SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set debit = debit+ " & Math.Abs(Tdk_Pot_Stock_Hutang_IDR_Utama_selisih_temp) & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_sup_selisih & "' and debit <> 0 "
                        SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_hutang_sup_selisih, 1),
                        Strings.Mid(akun_hutang_sup_selisih, 2, 1),
                        Strings.Mid(Ganti(akun_hutang_sup_selisih), 3),
                        KodePerusahaan, KodeProyek, ket & "; BIAYA TAMBAHAN; " & TxtPembelian_NmSupplier.Text.Trim, Math.Abs(Tdk_Pot_Stock_Hutang_IDR_Utama_selisih_temp), "0", pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                    End If
                End Using

                SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                SQL = SQL & "Kode_Akun = '" & akun_hutang_sup_selisih & "'  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Tdk_Pot_Stock_Hutang_IDR_Utama_selisih_temp & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                        SQL = SQL & "Kode_Akun = '" & akun_hutang_sup_selisih & "'  "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                        SQL = SQL & "'BIAYA TAMBAHAN', '" & Tdk_Pot_Stock_Hutang_IDR_Utama_selisih_temp & "', "
                        SQL = SQL & "'" & akun_hutang_sup_selisih & "')"
                        ExecuteTrans(SQL)

                    End If
                End Using

            End If

#End Region

            '---------------------------------------------------------------------------------------------------------------------

#End Region

        Next

#Region "Jurnal_PO2"

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

        If Flag_Average_Sup = "Y" Then
            Selisih_Import_AVG = Biaya_Import_AVG - (Biaya_Import_Total + freigt)
        End If

        Selisih_Hutang = (Hutang_Dalam_Proses + pib) - (Biaya_Import_Total + Selisih_Import_AVG + Billing + Storage + freigt + pph_pakai_persentase + Tot_Pot_Stock_IDR + Tdk_Pot_Stock_IDR + Tdk_Pot_Stock_Hutang_IDR_Utama + Tdk_Pot_Stock_Hutang_IDR_Penolong + pib + Selisih_PO + Selisih_PO_Biaya)
        Selisih_Hutang = Val(HilangkanTanda(Format(Selisih_Hutang, "N0")))

        If Selisih_Import_AVG > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Selisih_AVG_Import, 1),
                    Strings.Mid(coa_Selisih_AVG_Import, 2, 1),
                    Strings.Mid(Ganti(coa_Selisih_AVG_Import), 3),
                    KodePerusahaan, KodeProyek, ket, "0", Selisih_Import_AVG, pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
            SQL = SQL & "'SELISIH BIAYA AVG', '" & Selisih_Import_AVG & "', "
            SQL = SQL & "'" & coa_Selisih_AVG_Import & "')"
            ExecuteTrans(SQL)
        End If

        If Selisih_Import_AVG < 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Selisih_AVG_Import, 1),
                    Strings.Mid(coa_Selisih_AVG_Import, 2, 1),
                    Strings.Mid(Ganti(coa_Selisih_AVG_Import), 3),
                    KodePerusahaan, KodeProyek, ket & "; " & TxtPembelian_NmSupplier.Text.Trim, Math.Abs(Selisih_Import_AVG), "0", pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
            SQL = SQL & "'SELISIH BIAYA AVG', '" & Selisih_Import_AVG & "', "
            SQL = SQL & "'" & coa_Selisih_AVG_Import & "')"
            ExecuteTrans(SQL)
        End If

        If Selisih_Hutang <> 0 Then
            If Selisih_Hutang < 0 Then
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Selisih_Hutang_Import, 1),
                        Strings.Mid(coa_Selisih_Hutang_Import, 2, 1),
                        Strings.Mid(Ganti(coa_Selisih_Hutang_Import), 3),
                        KodePerusahaan, KodeProyek, ket & "; " & TxtPembelian_NmSupplier.Text.Trim, Math.Abs(Selisih_Hutang), "0", pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1
            Else
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Selisih_Hutang_Import, 1),
                        Strings.Mid(coa_Selisih_Hutang_Import, 2, 1),
                        Strings.Mid(Ganti(coa_Selisih_Hutang_Import), 3),
                        KodePerusahaan, KodeProyek, ket + "; SELISIH; " & TxtPembelian_NmSupplier.Text.Trim, "0", Selisih_Hutang, pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

            End If

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
            SQL = SQL & "'SELISIH HUTANG', '" & Selisih_Hutang & "', "
            SQL = SQL & "'" & coa_Selisih_Hutang_Import & "')"
            ExecuteTrans(SQL)
        End If

#End Region

#Region "Jurnal_Selisih2"

        pib_selisih = Val(HilangkanTanda(Format(pib_selisih, "N0")))

        pph_billing_selisih = Val(HilangkanTanda(Format(pph_billing_selisih, "N0")))
        Storage_selisih = Val(HilangkanTanda(Format(Storage_selisih, "N0")))

        Billing_selisih = Val(HilangkanTanda(Format(Billing_selisih, "N0")))

        Tot_Pot_Stock_IDR_selisih = Val(HilangkanTanda(Format(Tot_Pot_Stock_IDR_selisih, "N0")))

        Tdk_Pot_Stock_IDR_selisih = Val(HilangkanTanda(Format(Tdk_Pot_Stock_IDR_selisih, "N0")))

        Tdk_Pot_Stock_Hutang_IDR_Utama_selisih = Val(HilangkanTanda(Format(Tdk_Pot_Stock_Hutang_IDR_Utama_selisih, "N0")))

        Tdk_Pot_Stock_Hutang_IDR_Penolong_selisih = Val(HilangkanTanda(Format(Tdk_Pot_Stock_Hutang_IDR_Penolong_selisih, "N0")))

        pph_pakai_persentase_selisih = Val(HilangkanTanda(Format(pph_pakai_persentase_selisih, "N0")))

        Selisih_PO_selisih = Val(HilangkanTanda(Format(Selisih_PO_selisih, "N0")))

        Selisih_PO_Biaya_selisih = Val(HilangkanTanda(Format(Selisih_PO_Biaya_selisih, "N0")))

        If Flag_Average_Sup = "Y" Then
            Selisih_Import_AVG_selisih = Biaya_Import_AVG_selisih - (Biaya_Import_Total_selisih + freigt_selisih)
        End If

        Selisih_Hutang_selisih = (Hutang_Dalam_Proses_selisih) - (Biaya_Import_Total_selisih + Selisih_Import_AVG_selisih + Billing_selisih + Storage_selisih + freigt_selisih + pph_pakai_persentase_selisih + Tot_Pot_Stock_IDR_selisih + Tdk_Pot_Stock_IDR_selisih + Tdk_Pot_Stock_Hutang_IDR_Utama_selisih + Tdk_Pot_Stock_Hutang_IDR_Penolong_selisih + Selisih_PO_selisih + Selisih_PO_Biaya_selisih)
        Selisih_Hutang_selisih = Val(HilangkanTanda(Format(Selisih_Hutang_selisih, "N0")))

        If Selisih_Import_AVG_selisih > 0 Then

            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and kredit <> 0 "
            SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'update

                    SQL = "update detail_jurnal set kredit = kredit+ " & Selisih_Import_AVG_selisih & " where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and kredit <> 0 "
                    SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    'insert

                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_hutang_perjalanan_selisih, 1),
                    Strings.Mid(akun_hutang_perjalanan_selisih, 2, 1),
                    Strings.Mid(Ganti(akun_hutang_perjalanan_selisih), 3),
                    KodePerusahaan, KodeProyek, ket & "; BIAYA TAMBAHAN; " & TxtPembelian_NmSupplier.Text.Trim, "0", Selisih_Import_AVG_selisih, pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                End If
            End Using

            SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
            SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
            SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'update

                    SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Selisih_Import_AVG_selisih & " where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                    SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                    SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    'insert

                    SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                    SQL = SQL & "'BIAYA TAMBAHAN', '" & Selisih_Import_AVG_selisih & "', "
                    SQL = SQL & "'" & akun_hutang_perjalanan_selisih & "')"
                    ExecuteTrans(SQL)

                End If
            End Using
        ElseIf Selisih_Import_AVG_selisih < 0 Then
            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and debit <> 0 "
            SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'update

                    SQL = "update detail_jurnal set debit = debit+ " & Math.Abs(Selisih_Import_AVG_selisih) & " where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and debit <> 0 "
                    SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    'insert

                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_hutang_perjalanan_selisih, 1),
                    Strings.Mid(akun_hutang_perjalanan_selisih, 2, 1),
                    Strings.Mid(Ganti(akun_hutang_perjalanan_selisih), 3),
                    KodePerusahaan, KodeProyek, ket & "; BIAYA TAMBAHAN; " & TxtPembelian_NmSupplier.Text.Trim, Math.Abs(Selisih_Import_AVG_selisih), "0", pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                End If
            End Using

            SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
            SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
            SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'update

                    SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Selisih_Import_AVG_selisih & " where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                    SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                    SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    'insert

                    SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                    SQL = SQL & "'BIAYA TAMBAHAN', '" & Selisih_Import_AVG_selisih & "', "
                    SQL = SQL & "'" & akun_hutang_perjalanan_selisih & "')"
                    ExecuteTrans(SQL)

                End If
            End Using
        End If

        If Selisih_Hutang_selisih > 0 Then

            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and kredit <> 0 "
            SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'update

                    SQL = "update detail_jurnal set kredit = kredit+ " & Selisih_Hutang_selisih & " where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and kredit <> 0 "
                    SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    'insert

                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_hutang_perjalanan_selisih, 1),
                    Strings.Mid(akun_hutang_perjalanan_selisih, 2, 1),
                    Strings.Mid(Ganti(akun_hutang_perjalanan_selisih), 3),
                    KodePerusahaan, KodeProyek, ket & "; BIAYA TAMBAHAN; " & TxtPembelian_NmSupplier.Text.Trim, "0", Selisih_Hutang_selisih, pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                End If
            End Using

            SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
            SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
            SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'update

                    SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Selisih_Hutang_selisih & " where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                    SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                    SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    'insert

                    SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                    SQL = SQL & "'BIAYA TAMBAHAN', '" & Selisih_Hutang_selisih & "', "
                    SQL = SQL & "'" & akun_hutang_perjalanan_selisih & "')"
                    ExecuteTrans(SQL)

                End If
            End Using

        ElseIf Selisih_Hutang_selisih < 0 Then

            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and debit <> 0 "
            SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'update

                    SQL = "update detail_jurnal set debit = debit+ " & Math.Abs(Selisih_Hutang_selisih) & " where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_perjalanan_selisih & "' and debit <> 0 "
                    SQL = SQL & "and keterangan ='" & ket & "; BIAYA TAMBAHAN" & "' "
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    'insert

                    SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_hutang_perjalanan_selisih, 1),
                    Strings.Mid(akun_hutang_perjalanan_selisih, 2, 1),
                    Strings.Mid(Ganti(akun_hutang_perjalanan_selisih), 3),
                    KodePerusahaan, KodeProyek, ket & "; BIAYA TAMBAHAN; " & TxtPembelian_NmSupplier.Text.Trim, Math.Abs(Selisih_Hutang_selisih), "0", pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                End If
            End Using

            SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
            SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
            SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'update

                    SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Selisih_Hutang_selisih & " where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                    SQL = SQL & "Kode = 'BIAYA TAMBAHAN' and "
                    SQL = SQL & "Kode_Akun = '" & akun_hutang_perjalanan_selisih & "'  "
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    'insert

                    SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                    SQL = SQL & "'BIAYA TAMBAHAN', '" & Selisih_Hutang_selisih & "', "
                    SQL = SQL & "'" & akun_hutang_perjalanan_selisih & "')"
                    ExecuteTrans(SQL)

                End If
            End Using
        End If

#End Region

        SQL = "select round(sum(debit),0) as debit, round(sum(kredit),0) as kredit from detail_jurnal where "
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

        SQL = "Update EMI_Pembelian "
        SQL = SQL & "Set Kode_Voucher = '" & Kode_voucher & "' "
        SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
        SQL = SQL & "and No_Faktur = '" & TxtPembelian_NoFaktur.Text & "' "
        ExecuteTrans(SQL)

        isError = True
    End Sub

    Private Sub Jurnal_Lokal()

        'TODO :JURNAL LOKAL

        Dim inisial_faktur_dari As String = ""
        Dim lokasi_Barang As String = ""
        Dim persen_PPN As Integer = 0
        SQL = "select inisial_faktur from stock_owner "
        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & CmbPembelian_Lokasi.Text & "' "
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

        SQL = "select PPN from emi_pembelian_po "
        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & TxtPembelian_NoPO.Text & "' "
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                persen_PPN = Dr("PPN")
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
        SQL = SQL & "'" & KodeProyek & "', 'Pembelian " & TxtPembelian_NoFaktur.Text & "', '', "
        SQL = SQL & "'-', '" & UserID & "')"
        ExecuteTrans(SQL)

        Dim Total_Persediaan As Double = 0
        Dim Total_PPN As Double = 0
        Dim Total_Perjalanan As Double = 0
        Dim Total_Hutang As Double = 0
        Dim Total_SelisihHutang As Double = 0
        Dim Total_selisih As Double = 0
        Dim Biaya_Lokal_Total As Double = 0
        Dim akun_Selisih_pembulatan As String = ""

        Dim TotDebit As Double = 0
        Dim TotKredit As Double = 0

        For index = 0 To LvPembelian_DataPembelian.Items.Count - 1
            'Get_Isi_Listview(index)
            Get_Isi_Listview(index)

            Dim akun_persediaan_dari As String = ""
            Dim akun_ppn As String = ""
            Dim akun_hutang_sup As String = ""
            Dim akun_hutang_ppn As String = ""

            SQL = "select c.akun_Persediaan "
            SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
            SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.kode_stock_owner = '" & LvSo & "' and b.Kode_Barang='" & LvKd_Brg & "' "
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

            SQL = "select Hutang_Supplier, Hutang_Perjalanan, Hutang_PPN, PPN_Pembelian, Selisih_Pembulatan "
            SQL = SQL & "from stock_owner "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & CmbPembelian_Lokasi.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    akun_hutang_sup = Dr("Hutang_Supplier")
                    'akun_hutang_perjalanan = Dr("Hutang_Perjalanan")
                    akun_hutang_ppn = Dr("Hutang_Supplier")
                    akun_ppn = Dr("PPN_Pembelian")
                    akun_Selisih_pembulatan = Dr("Selisih_Pembulatan")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim HargaAkhir As Double = Val(HilangkanTanda(LvHarga))
            Dim HargaPO As Double = Val(HilangkanTanda(LvHargaReal))
            Dim JumlahMasuk As Double = Val(HilangkanTanda(LvJml_Msk))
            Dim JumlahHutang As Double = Val(HilangkanTanda(LvJmlHutang))
            Dim PersenPPN As Double = Val(HilangkanTanda(LvPersenPPN))

            'PERHITUNGAN
            Dim NilaiPersediaan As Double = Math.Round(JumlahMasuk * HargaAkhir)
            Dim NilaiPPN As Double = Math.Round((JumlahHutang * HargaPO) * (PersenPPN / 100))
            'Dim NilaiPerjalanan As Double = Math.Round((HargaAkhir - HargaPO) * JumlahMasuk)
            Dim NilaiHutang As Double = Math.Round((JumlahHutang * HargaPO) + NilaiPPN)
            Dim selisih As Double = Math.Round(JumlahHutang - JumlahMasuk, 2)
            Dim NilaiSelisihHutang As Double = Math.Round(selisih * HargaPO)
            'Dim TotalPO As Double = Math.Round(JumlahMasuk * HargaPO)

            Dim asa As Double = JumlahHutang * HargaPO

            Total_Persediaan += NilaiPersediaan
            Total_PPN += NilaiPPN
            'Total_Perjalanan += NilaiPerjalanan
            Total_Hutang += NilaiHutang
            Total_SelisihHutang += NilaiSelisihHutang

            '1
            'JURNAL PERSEDIAAN
            If NilaiPersediaan <> 0 Then

                If Not Insert_Jurnal(Kode_voucher, akun_persediaan_dari, "Persediaan; ", TxtPembelian_NmSupplier.Text.Trim, NilaiPersediaan, pagenumber, LvSo, "D") Then
                    isError = False
                    Exit Sub
                End If
            End If

            'JURNAL PPN
            If NilaiPPN <> 0 Then
                'RAgu pake akun yg mana :)

                Dim akunPPN As String

                For i As Integer = 0 To tempDataPajak.Count - 1
                    If tempDataPajak(i).isPPN Then
                        akunPPN = tempDataPajak(i).akun
                    End If
                Next


                If Not Insert_Jurnal(Kode_voucher, akun_ppn, "PPN; ", TxtPembelian_NmSupplier.Text.Trim, NilaiPPN, pagenumber, CmbPembelian_Lokasi.Text, "D") Then
                    isError = False
                    Exit Sub
                End If
            End If

            'JURNAL HUTANG SUPPLIER
            If NilaiHutang <> 0 Then
                If Not Insert_Jurnal(Kode_voucher, akun_hutang_sup, "Hutang Supplier; ", TxtPembelian_NmSupplier.Text.Trim, NilaiHutang, pagenumber, CmbPembelian_Lokasi.Text, "K") Then
                    isError = False
                    Exit Sub
                End If
            End If




            'JURNAL SELISIH HUTANG 
            If selisih <> 0 Then
                Dim akun_hutang_sup_selisih As String = ""
                Total_selisih += Math.Round(selisih * HargaPO)

                SQL = "select Akun_Bahan, Akun_Perjalanan from EMI_Pembelian_Selisih_Barang_Masuk "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur_bm = '" & LvNo_PO & "' and status is null "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        akun_hutang_sup_selisih = Dr("Akun_Perjalanan")
                        'akun_hutang_perjalanan_selisih = Dr("Akun_Perjalanan")
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                If selisih > 0 Then
                    If Not Insert_Jurnal(Kode_voucher, akun_hutang_sup_selisih, "Selisih Hutang; ", TxtPembelian_NmSupplier.Text.Trim, NilaiSelisihHutang, pagenumber, CmbPembelian_Lokasi.Text, "D") Then
                        isError = False
                        Exit Sub
                    End If
                ElseIf selisih < 0 Then
                    If Not Insert_Jurnal(Kode_voucher, akun_hutang_sup_selisih, "Selisih Hutang; ", TxtPembelian_NmSupplier.Text.Trim, Math.Abs(NilaiSelisihHutang), pagenumber, CmbPembelian_Lokasi.Text, "K") Then
                        isError = False
                        Exit Sub
                    End If
                End If
            End If

        Next


#Region "JURNAL PERJALANAN"

        'Get Data Perjalanan Lokal
        Dim Arr_Biaya_Lokal_Master As New ArrayList
        Dim Arr_Biaya_Lokal_Kategori As New ArrayList
        Dim Arr_Biaya_Lokal As New ArrayList
        Dim Arr_Akun1 As New ArrayList
        Dim Arr_Akun2 As New ArrayList

        SQL = "select b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal, "
        SQL = SQL & "round(sum(b.total), 0) as Biaya, "

        SQL = SQL & "isnull(("
        SQL = SQL & "select akun_1 from detail_account_master X where X.Kode_Perusahaan = b.Kode_Perusahaan and "
        SQL = SQL & "x.Kode_Master_Kategori_Biaya_import = b.Kode_Master_Kategori_Biaya_import and "
        SQL = SQL & "x.lokasi = '" & CmbPembelian_Lokasi.Text & "' "
        SQL = SQL & "),0) as Akun_1, "

        SQL = SQL & "isnull(("
        SQL = SQL & "select akun_2 from detail_account_master X where X.Kode_Perusahaan = b.Kode_Perusahaan and "
        SQL = SQL & "x.Kode_Master_Kategori_Biaya_import = b.Kode_Master_Kategori_Biaya_import and "
        SQL = SQL & "x.lokasi = '" & CmbPembelian_Lokasi.Text & "' "
        SQL = SQL & "),0) as Akun_2 "

        SQL = SQL & "from transaksi_biaya_Lokal a, transaksi_biaya_Lokal_detail b, Master_Kategori_Biaya_Import c where "
        SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And "
        SQL = SQL & "a.no_faktur = b.no_faktur And a.status Is null and "
        SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Master_Kategori_Biaya_import = c.Kode_Master_Kategori_Biaya_Import and "
        SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.no_Po = '" & TxtPembelian_NoPO.Text & "' " 'and C.Flag_Gabungan = 'Y' "
        SQL = SQL & "group by b.Kode_Perusahaan, b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal "
        SQL = SQL & "order by b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import"
        Using ds = BindingTrans(SQL)
            With ds.Tables("MyTable")
                For index3 As Integer = 0 To .Rows.Count - 1

                    Arr_Biaya_Lokal_Master.Add(.Rows(index3).Item("Kode_Master_Kategori_Biaya_Import"))
                    Arr_Biaya_Lokal_Kategori.Add(.Rows(index3).Item("kode_kategori_biaya_import"))
                    Arr_Biaya_Lokal.Add(Val(HilangkanTanda(Format(.Rows(index3).Item("Biaya"), "N0"))))
                    Arr_Akun1.Add(.Rows(index3).Item("Akun_1"))
                    Arr_Akun2.Add(.Rows(index3).Item("Akun_2"))

                    Biaya_Lokal_Total = Biaya_Lokal_Total + Val(HilangkanTanda(Format(.Rows(index3).Item("Biaya"), "N0")))

                Next
            End With

        End Using

        For indexKategoriImport As Integer = 0 To Arr_Biaya_Lokal_Master.Count - 1

            SQL = "select* from Detail_Account_Master where "
            SQL = SQL & "Lokasi = '" & CmbPembelian_Lokasi.Text & "' and Kode_master_Kategori_biaya_import = '" & Arr_Biaya_Lokal_Master.Item(indexKategoriImport) & "' "
            SQL = SQL & "and Akun_1 ='" & Arr_Akun1.Item(indexKategoriImport) & "'  and Akun_2 = '" & Arr_Akun2.Item(indexKategoriImport) & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Akun " & Arr_Biaya_Lokal_Master.Item(indexKategoriImport) & " Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End Using

        Next

        'INsert Jurnal
        For indexKategoriImport As Integer = 0 To Arr_Biaya_Lokal_Master.Count - 1
            If Val(Arr_Biaya_Lokal.Item(indexKategoriImport)) <> 0 Then

                'Jurnal Kategori Biaya Lokal
                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Arr_Akun2.Item(indexKategoriImport) & "' and kredit <> 0 "
                SQL = SQL & "and keterangan ='Hutang " & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "; " & TxtPembelian_NoFaktur.Text & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set kredit = kredit+ " & Arr_Biaya_Lokal.Item(indexKategoriImport) & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Arr_Akun2.Item(indexKategoriImport) & "' and kredit <> 0 "
                        SQL = SQL & "and keterangan ='Hutang " & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "; " & TxtPembelian_NoFaktur.Text & "'"
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Arr_Akun2.Item(indexKategoriImport), 1),
                                    Strings.Mid(Arr_Akun2.Item(indexKategoriImport), 2, 1),
                                    Strings.Mid(Ganti(Arr_Akun2.Item(indexKategoriImport)), 3),
                                    KodePerusahaan, KodeProyek, "Hutang " & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "; " & TxtPembelian_NoFaktur.Text & "; " & TxtPembelian_NmSupplier.Text.Trim, "0", Arr_Biaya_Lokal.Item(indexKategoriImport), pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                    End If
                End Using

                'Data Kategori Biaya Lokal
                SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                SQL = SQL & "Kode = '" & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "' and "
                SQL = SQL & "Kode_Akun = '" & Arr_Akun2.Item(indexKategoriImport) & "'  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Arr_Biaya_Lokal.Item(indexKategoriImport) & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                        SQL = SQL & "Kode = '" & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "' and "
                        SQL = SQL & "Kode_Akun = '" & Arr_Akun2.Item(indexKategoriImport) & "'  "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                        SQL = SQL & "'" & Arr_Biaya_Lokal_Kategori.Item(indexKategoriImport) & "', '" & Arr_Biaya_Lokal.Item(indexKategoriImport) & "', "
                        SQL = SQL & "'" & Arr_Akun2.Item(indexKategoriImport) & "')"
                        ExecuteTrans(SQL)

                    End If
                End Using
            End If
        Next

#End Region

        'SELISIH PEmbualatan
        Dim nilai_selisih As Double = (Total_Persediaan + Total_PPN + Total_SelisihHutang) - (Biaya_Lokal_Total + Total_Hutang)

        If nilai_selisih <> 0 Then
            If nilai_selisih < 0 Then
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_Selisih_pembulatan, 1),
                        Strings.Mid(akun_Selisih_pembulatan, 2, 1),
                        Strings.Mid(Ganti(akun_Selisih_pembulatan), 3),
                        KodePerusahaan, KodeProyek, "Selisih Pembulatan; " & TxtPembelian_NmSupplier.Text.Trim, Math.Abs(nilai_selisih), "0", pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1
            Else
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_Selisih_pembulatan, 1),
                        Strings.Mid(akun_Selisih_pembulatan, 2, 1),
                        Strings.Mid(Ganti(akun_Selisih_pembulatan), 3),
                        KodePerusahaan, KodeProyek, "Selisih Pembulatan; " & TxtPembelian_NmSupplier.Text.Trim, "0", nilai_selisih, pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

            End If

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
            SQL = SQL & "'Selisih Pembulatan', '" & nilai_selisih & "', "
            SQL = SQL & "'" & akun_Selisih_pembulatan & "')"
            ExecuteTrans(SQL)
        End If

        '==========================================================
        '=     UNTUK TES / LIAT DEBIT KREDIT BENAR ATAU SALAH     =
        '==========================================================
        'SQL = "select debit, kredit, Keterangan, Kode_Account from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and kode_voucher = '" & Kode_voucher & "' "
        'Using Ds = BindingTrans(SQL)
        '    With Ds.Tables("MyTable")
        '        If .Rows.Count <> 0 Then
        '            'CloseTrans()
        '            'CloseConn()
        '            'Exit Sub
        '        Else
        '            'CloseTrans()
        '            'CloseConn()
        '            'Exit Sub
        '        End If
        '    End With
        'End Using

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

        SQL = "Update emi_pembelian "
        SQL = SQL & "Set Kode_Voucher = '" & Kode_voucher & "' "
        SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
        SQL = SQL & "and No_Faktur = '" & TxtPembelian_NoFaktur.Text & "' "
        ExecuteTrans(SQL)

        isError = True
    End Sub


    Private Function Insert_Jurnal(ByVal Kode_voucher As String, ByVal akun As String, ByVal Kode_Akun_Pembelian As String, ByVal Keterangan As String, ByVal Nilai As String, ByVal pagenumber As Integer, ByVal lks As String, ByVal Type As String) As Boolean

        Try

            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun & "' "

            If Type = "D" Then
                SQL = SQL & "and debit <> 0 "
            Else
                SQL = SQL & "and kredit <> 0 "
            End If

            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'update


                    If Type = "D" Then
                        SQL = "update detail_jurnal set debit = debit+ " & Nilai & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun & "'  "
                        SQL = SQL & "and debit <> 0"

                    ElseIf Type = "K" Then
                        SQL = "update detail_jurnal set kredit = kredit+ " & Nilai & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun & "'  "
                        SQL = SQL & "and kredit <> 0"
                    End If
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    'insert
                    If Type = "D" Then
                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun, 1),
                              Strings.Mid(akun, 2, 1),
                              Strings.Mid(Ganti(akun), 3),
                              KodePerusahaan, KodeProyek, Kode_Akun_Pembelian & "; " & TxtPembelian_NoFaktur.Text.Trim & "; " & Keterangan, Nilai, "0", pagenumber, lks, Bahasa_Pilihan, Ket_Cost_Center_HO)

                    ElseIf Type = "K" Then
                        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun, 1),
                              Strings.Mid(akun, 2, 1),
                              Strings.Mid(Ganti(akun), 3),
                              KodePerusahaan, KodeProyek, Kode_Akun_Pembelian & "; " & TxtPembelian_NoFaktur.Text & "; " & Keterangan, "0", Nilai, pagenumber, lks, Bahasa_Pilihan, Ket_Cost_Center_HO)
                    End If
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                End If
            End Using

            SQL = "select kode_perusahaan from Pelunasan_Pembelian where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
            SQL = SQL & "Kode = '" & Kode_Akun_Pembelian & "' and "
            SQL = SQL & "Kode_Akun = '" & akun & "'  "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'update

                    SQL = "update Pelunasan_Pembelian set Nilai = Nilai+ " & Nilai & " where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "No_Faktur = '" & TxtPembelian_NoFaktur.Text.Trim & "' and "
                    SQL = SQL & "Kode = '" & Kode_Akun_Pembelian & "' and "
                    SQL = SQL & "Kode_Akun = '" & akun & "'  "
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    'insert

                    SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtPembelian_NoFaktur.Text.Trim & "', "
                    SQL = SQL & "'" & Kode_Akun_Pembelian & "', '" & Nilai & "', "
                    SQL = SQL & "'" & akun & "')"
                    ExecuteTrans(SQL)

                End If
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function


    Private Function Insert_Pelunasan() As Boolean

        'TODO : INSERT PELUNASAN

        Try

            Dim inisial_faktur_dari As String = ""
            SQL = "select inisial_faktur from stock_owner "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & CmbPembelian_Lokasi.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    'akun_persediaan_dari = Dr("persediaan")
                    inisial_faktur_dari = Dr("inisial_faktur")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
                End If
            End Using

            Dim Kode_voucher2 As String = ""
            Kode_voucher2 = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
            Dim pagenumber As Integer = 1

            Dim TotKurs As Double = Val(HilangkanTanda(TxtPembelian_Kurs.Text)) * Val(HilangkanTanda(TxtPembelian_GrandTotal.Text))

            Dim DpDimuka As Double = Val(HilangkanTanda(Txt_PembayaranDimuka.Text))
            Dim GrandTotal As Double = Val(HilangkanTanda(TxtPembelian_GrandTotal.Text)) - Val(HilangkanTanda(Txt_GrandPPH.Text))
            Dim DpDigunakan As Double = 0

            If DpDimuka < GrandTotal Then
                DpDigunakan = DpDimuka
            Else
                DpDigunakan = GrandTotal
            End If

            '=====================================
            '=     GET NILAI DP DIKURANG PPH     =
            '=====================================
            Dim JumlahDPA As Double = 0
            SQL = ";with Cte as ( "
            SQL = SQL & "select a.Nilai as Nilai_DP, ( "
            SQL = SQL & "(a.Nilai- "
            SQL = SQL & "isnull(( "
            SQL = SQL & "select sum(x.nilai) from EMI_Transaksi_Pembayaran_Dimuka_Pajak x "
            SQL = SQL & "where x.kode_perusahaan=a.kode_perusahaan and x.no_faktur = a.No_Transaksi and x.flag_ppn is null ),0)) - "
            SQL = SQL & "ISNULL(( "
            SQL = SQL & "select z.nilai from EMI_Pelunasan_Detail_DP z, emi_pelunasan w "
            SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan and z.urut_DP = a.No_Urut and "
            SQL = SQL & "z.kode_perusahaan=w.kode_Perusahaan and z.no_val=w.no_val and w.status is null "
            SQL = SQL & "), 0) ) as Sisa "
            SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka_Detail a, EMI_Transaksi_Pembayaran_Dimuka b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "And a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "And b.Status Is null "
            SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Fak_PO in ( "
            SQL = SQL & "select y.No_FakInduk "
            SQL = SQL & "from EMI_Pembelian_PO x, EMI_Pembelian_PO_Det y "
            SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan "
            SQL = SQL & "and x.No_Faktur = y.No_Faktur "
            SQL = SQL & "and x.Status is null "
            SQL = SQL & "and x.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and x.No_Faktur = '" & TxtPembelian_NoPO.Text & "' "
            SQL = SQL & "group by y.No_FakInduk ) "
            SQL = SQL & ")select isnull(sum(Sisa),0) as Nilai_DP from Cte "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    JumlahDPA = Val(HilangkanTanda(Format(Dr("Nilai_DP"), "N0")))
                Else
                    JumlahDPA = 0
                End If
            End Using

            If JumlahDPA <> Val(HilangkanTanda(Txt_PembayaranDimuka.Text)) Then
                MessageBox.Show("Harap Ulangi Transaksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If


            '=========================
            '=     GET NILAI PPH     =
            '=========================
            Dim PersenPPN As Double = 0
            Dim SumPersenPPH As Double = 0
            SQL = "select a.No_Faktur, a.Persentase, a.Kode_Akun, a.Flag_PPN "
            SQL = SQL & "from EMI_Detail_PPH_PO a, EMI_Pembelian_PO b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Faktur = '" & TxtPembelian_NoPO.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            If General_Class.CekNULL(.Rows(i).Item("Flag_PPN")) = "Y" Then
                                PersenPPN = Val(HilangkanTanda(.Rows(i).Item("Persentase")))
                            Else
                                SumPersenPPH += Val(HilangkanTanda(.Rows(i).Item("Persentase")))
                            End If
                        Next
                    End If
                End With
            End Using

            Dim Persentase As Double = 0
            Dim DPP As Double = 0
            Dim NilaiPPN As Double = 0
            Dim NilaiPPH As Double = 0
            Dim NilaiGrandDPP As Double = 0

            If DpDigunakan <> 0 Then

                Persentase = 1 + (Val(HilangkanTanda(PersenPPN)) / 100) - (Val(HilangkanTanda(SumPersenPPH)) / 100)
                DPP = Val(HilangkanTanda(Format(DpDigunakan / Persentase, "N0")))

                NilaiPPN = Val(HilangkanTanda(Format(DPP * (Val(HilangkanTanda(PersenPPN)) / 100), "N0")))
                NilaiPPH = Val(HilangkanTanda(Format(DPP * (Val(HilangkanTanda(SumPersenPPH)) / 100), "N0")))

                NilaiGrandDPP = Val(HilangkanTanda(Format((DPP + NilaiPPN), "N0")))

            End If



            '=================================
            '=     GET KATEGORI SUPPLIER     =
            '=================================
            Dim Kategori_Supplier As String = ""
            SQL = "select c.Kode_Kategori_Suppliers  from  Suppliers b, Suppliers_Kategori c "
            SQL = SQL & "where  b.kode_perusahaan = c.kode_perusahaan and b.id_kategori_suppliers = c.id_kategori_suppliers "
            SQL = SQL & "and b.kode_perusahaan = '" & KodePerusahaan & "' and b.Kode_Supplier = '" & TxtPembelian_KdSupplier.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Kategori_Supplier = "'" & Dr("Kode_Kategori_Suppliers") & "'"
                Else
                    Kategori_Supplier = "NULL"
                End If
            End Using


            '============================
            '=     INSERT PELUNASAN     =
            '============================
            'SQL = "insert into EMI_Pelunasan (Kode_Perusahaan, No_Val, Tanggal, Jam, Keterangan, UserValidasi, Kode_Voucher, "
            'SQL = SQL & "Mata_Uang, Total, Total_PPN, Total_PPH, Grand_Total, "
            'SQL = SQL & "Total_Kurs_Lama, Total_Kurs_Baru, jenis, No_Pengajuan) values "
            'SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_Faktur_Pelunasan.Text.Trim & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            'SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', 'PELUNASAN : PEMBELIAN', '" & UserID & "', '" & Kode_voucher2 & "', "
            'SQL = SQL & "'" & CmbPembelian_MataUang.Text & "', '" & HilangkanTanda(TxtPembelian_TotalIDR.Text) & "', '0', '0', "
            'SQL = SQL & "'" & HilangkanTanda(TxtPembelian_GrandTotal.Text) & "', '" & HilangkanTanda(TotKurs) & "', "
            'SQL = SQL & " '" & HilangkanTanda(TotKurs) & "', " & Kategori_Supplier & ", NULL)"
            'ExecuteTrans(SQL)

            SQL = "insert into EMI_Pelunasan (Kode_Perusahaan, No_Val, Tanggal, Jam, Keterangan, UserValidasi, Kode_Voucher, "
            SQL = SQL & "Mata_Uang, Total, Total_PPN, Total_PPH, Grand_Total, "
            SQL = SQL & "Total_Kurs_Lama, Total_Kurs_Baru, jenis, No_Pengajuan, Flag_Otomatis) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_Faktur_Pelunasan.Text.Trim & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', 'PELUNASAN : PEMBELIAN', '" & UserID & "', '" & Kode_voucher2 & "', "
            SQL = SQL & "'" & CmbPembelian_MataUang.Text & "', '" & HilangkanTanda(DpDigunakan) & "', '" & HilangkanTanda(NilaiPPN) & "', '" & HilangkanTanda(NilaiPPH) & "', "
            SQL = SQL & "'" & HilangkanTanda(DpDigunakan) & "', '" & HilangkanTanda(DPP) & "', "
            SQL = SQL & " '" & HilangkanTanda(DPP) & "', " & Kategori_Supplier & ", NULL, 'Y')"
            ExecuteTrans(SQL)


            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
            SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
            SQL = SQL & "'" & Kode_voucher2 & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
            SQL = SQL & "'" & KodeProyek & "', 'Pembelian " & TxtPembelian_NoFaktur.Text & "', '', "
            SQL = SQL & "'-', '" & UserID & "')"
            ExecuteTrans(SQL)


            Dim Akun_DP, Akun_Hutang As String
            SQL = "select Akun_DP, Hutang_Supplier "
            SQL = SQL & "from stock_owner "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Lokasi & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Akun_DP = Dr("Akun_DP")
                    Akun_Hutang = Dr("Hutang_Supplier")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
                End If
            End Using

            Dim Type As String = ""

            '==========================================
            '=     GET DATA KATEGORI BIAYA IMPORT     =
            '==========================================
            Dim Kode_Master_Kategori_Biaya_Import As String = ""
            SQL = "select top 1 b.kode_group_jenis "
            SQL = SQL & "from barang a, EMI_Group_Jenis b "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan "
            SQL = SQL & "and a.id_group_jenis = b.id_group_jenis "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.kode_barang = '" & ListViewDet.Items(0).SubItems(2).Text & "' "
            SQL = SQL & "order by b.Kode_Group_Jenis"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Kode_Master_Kategori_Biaya_Import = "'" & Dr("kode_group_jenis") & "'"
                Else
                    Kode_Master_Kategori_Biaya_Import = "NULL"
                End If
            End Using


            '===================================
            '=     INSERT DETAIL PELUNASAN     =
            '===================================
            SQL = "insert into EMI_Pelunasan_Detail(kode_perusahaan, no_val, no_faktur, kode_Perusahaan_biaya_import, Mata_Uang, byr, "
            SQL = SQL & "Persen_PPN, Persen_PPH, Nilai_PPN, Nilai_PPH, Kode_Master_Kategori_Biaya_Import, Kode_stock_Owner, Tambahan, Total_Tambahan, Subtotal, "
            SQL = SQL & "Kurs_lama, Total_Bayar_Kurs_Lama, Kurs_Baru, Total_Bayar_Kurs_Baru, Kode_Bank_Tujuan, No_Rek_Tujuan, Nama_Penerima, Kota_Penerima, Negara_Penerima, Tanggal_Bayar, Jenis1, Jenis2, DP_Digunakan) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & Txt_Faktur_Pelunasan.Text.Trim & "','" & TxtPembelian_NoFaktur.Text.Trim & "', '" & TxtPembelian_KdSupplier.Text & "', "
            SQL = SQL & "'" & CmbPembelian_MataUang.Text & "', " & HilangkanTanda(DpDigunakan) & ", "
            SQL = SQL & "" & HilangkanTanda(PersenPPN) & ", " & HilangkanTanda(SumPersenPPH) & ", " & HilangkanTanda(NilaiPPN) & ", " & HilangkanTanda(NilaiPPH) & ", " & Kode_Master_Kategori_Biaya_Import & ", "
            SQL = SQL & "'" & CmbPembelian_Lokasi.Text & "', 0, " & 0 & ", " & 0 & ", "
            SQL = SQL & HilangkanTanda(TxtPembelian_Kurs.Text) & ", " & HilangkanTanda(DpDigunakan) & ", " & HilangkanTanda(TxtPembelian_Kurs.Text) & ", " & HilangkanTanda(DpDigunakan) & ", "
            SQL = SQL & "NULL, NULL, NULL, NULL, NULL, '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "'SUPPLIER', 'A', " & HilangkanTanda(DpDigunakan) & ")"
            ExecuteTrans(SQL)



#Region "INSERT JURNAL HUTANG"

            Type = "D"
            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & Kode_voucher2 & "' and "
            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Akun_Hutang & "' "
            If Type = "D" Then
                SQL = SQL & "and debit <> 0 "
            Else
                SQL = SQL & "and kredit <> 0 "
            End If

            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    'update


                    If Type = "D" Then
                        SQL = "update detail_jurnal set debit = debit+ " & NilaiGrandDPP & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher2 & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Akun_Hutang & "'  "
                        SQL = SQL & "and debit <> 0"

                    ElseIf Type = "K" Then
                        SQL = "update detail_jurnal set kredit = kredit+ " & NilaiGrandDPP & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_voucher2 & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Akun_Hutang & "'  "
                        SQL = SQL & "and kredit <> 0"
                    End If
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    'insert
                    If Type = "D" Then
                        SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(Akun_Hutang, 1),
                              Strings.Mid(Akun_Hutang, 2, 1),
                              Strings.Mid(Ganti(Akun_Hutang), 3),
                              KodePerusahaan, KodeProyek, "Hutang ; " & TxtPembelian_NmSupplier.Text & "; " & TxtPembelian_NoFaktur.Text, NilaiGrandDPP, "0", pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)

                    ElseIf Type = "K" Then
                        SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(Akun_Hutang, 1),
                              Strings.Mid(Akun_Hutang, 2, 1),
                              Strings.Mid(Ganti(Akun_Hutang), 3),
                              KodePerusahaan, KodeProyek, "Hutang; " & TxtPembelian_NmSupplier.Text & "; " & TxtPembelian_NoFaktur.Text, "0", NilaiGrandDPP, pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                    End If
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                End If
            End Using

#End Region

            Dim x_no_urut_detail_pelunasan As Integer = 0
            SQL = "select IDENT_CURRENT('EMI_Pelunasan_Detail') as urutan"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    x_no_urut_detail_pelunasan = Dr("urutan")
                End If
            End Using

            SQL = "select urut from EMI_Pelunasan_Detail where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Val = '" & Txt_Faktur_Pelunasan.Text.Trim & "' and urut = '" & x_no_urut_detail_pelunasan & "'"
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Harap ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
                End If
            End Using

            Dim DpDipakai As Double = DpDigunakan

            '==========================================
            '=     INSERT EMI_Detail_DP_Pelunasan     =
            '==========================================

            SQL = "with Cte as ( select a.Nilai as Nilai_DP, a.no_urut, a.no_transaksi as No_DP,  ( "
            SQL = SQL & "(a.Nilai-isnull((select sum(x.nilai) from EMI_Transaksi_Pembayaran_Dimuka_Pajak x where "
            SQL = SQL & "x.kode_perusahaan=a.kode_perusahaan and x.no_faktur=a.No_Transaksi and x.flag_ppn is null ),0)) -  "
            SQL = SQL & "ISNULL(( select z.nilai from EMI_Pelunasan_Detail_DP z, emi_pelunasan w where "
            SQL = SQL & "z.Kode_Perusahaan = a.Kode_Perusahaan and z.urut_DP = a.No_Urut and "
            SQL = SQL & "z.kode_perusahaan=w.kode_Perusahaan and z.no_val=w.no_val and w.status is null "
            SQL = SQL & " ), 0) ) as Sisa "
            SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka_Detail a, EMI_Transaksi_Pembayaran_Dimuka b  "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "And a.No_Transaksi = b.No_Transaksi  "
            SQL = SQL & "And b.Status Is null  "
            SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & "and a.No_Fak_PO in ( "
            SQL = SQL & "select y.No_FakInduk  "
            SQL = SQL & "from EMI_Pembelian_PO x, EMI_Pembelian_PO_Det y  "
            SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan  "
            SQL = SQL & "and x.No_Faktur = y.No_Faktur "
            SQL = SQL & "and x.Status is null  "
            SQL = SQL & "and x.Kode_Perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & "and x.No_Faktur = '" & TxtPembelian_NoPO.Text.Trim & "'  "
            SQL = SQL & "group by y.No_FakInduk ) "
            SQL = SQL & ")select no_urut, isnull(sisa,0) as Nilai_DP, No_DP from Cte where sisa<>0 "
            SQL = SQL & "order by No_Urut "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        For i As Integer = 0 To .Rows.Count - 1

                            Dim JumlahDp As Double = Val(HilangkanTanda(.Rows(i).Item("Nilai_DP")))
                            Dim NoDP As String = .Rows(i).Item("No_DP")

                            If DpDipakai = 0 Then
                                Exit For
                            ElseIf DpDipakai < 0 Then
                                Return False
                            End If

                            If JumlahDp >= DpDipakai Then

                                SQL = "insert into EMI_Pelunasan_Detail_DP (Kode_Perusahaan, no_val, Urut_Detail_Pelunasan, Urut_DP, nilai) values "
                                SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_Faktur_Pelunasan.Text.Trim & "', '" & x_no_urut_detail_pelunasan & "', "
                                SQL = SQL & "'" & .Rows(i).Item("no_urut") & "', '" & HilangkanTanda(DpDipakai) & "')"
                                ExecuteTrans(SQL)


                                Dim DPPS As Double = Val(HilangkanTanda(Format(DpDipakai / Persentase, "N0")))

                                Dim NilaiPPNS As Double = Val(HilangkanTanda(Format(DPPS * (Val(HilangkanTanda(PersenPPN)) / 100), "N0")))

                                Dim NilaiGrandDPPS As Double = Val(HilangkanTanda(Format((DPPS + NilaiPPNS), "N0")))


                                SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(Akun_DP, 1),
                                            Strings.Mid(Akun_DP, 2, 1),
                                            Strings.Mid(Ganti(Akun_DP), 3),
                                            KodePerusahaan, KodeProyek, "Hutang; " & TxtPembelian_NmSupplier.Text & "; " & TxtPembelian_NoFaktur.Text & "; No DP : " & NoDP, "0", NilaiGrandDPPS, pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                                pagenumber = pagenumber + 1


                                DpDipakai = 0

                            Else

                                SQL = "insert into EMI_Pelunasan_Detail_DP (Kode_Perusahaan, no_val, Urut_Detail_Pelunasan, Urut_DP, nilai) values "
                                SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_Faktur_Pelunasan.Text.Trim & "', '" & x_no_urut_detail_pelunasan & "', "
                                SQL = SQL & "'" & .Rows(i).Item("no_urut") & "', '" & HilangkanTanda(JumlahDp) & "')"
                                ExecuteTrans(SQL)

                                Dim DPPS As Double = Val(HilangkanTanda(Format(JumlahDp / Persentase, "N0")))

                                Dim NilaiPPNS As Double = Val(HilangkanTanda(Format(DPPS * (Val(HilangkanTanda(PersenPPN)) / 100), "N0")))

                                Dim NilaiGrandDPPS As Double = Val(HilangkanTanda(Format((DPPS + NilaiPPNS), "N0")))


                                SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(Akun_DP, 1),
                                            Strings.Mid(Akun_DP, 2, 1),
                                            Strings.Mid(Ganti(Akun_DP), 3),
                                            KodePerusahaan, KodeProyek, "Hutang; " & TxtPembelian_NmSupplier.Text & "; " & TxtPembelian_NoFaktur.Text & "; No DP : " & NoDP, "0", NilaiGrandDPPS, pagenumber, CmbPembelian_Lokasi.Text, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                                pagenumber = pagenumber + 1

                                DpDipakai -= JumlahDp
                            End If

                        Next

                    End If
                End With
            End Using


            If Val(HilangkanTanda(Format(DpDipakai, "N2"))) <> 0 Then
                Return False
            End If





#Region "JURNAL DP"



#Region "INSERT JURNAL DP"



#End Region


#End Region


            '=========================
            '=     CEK JURNAL DP     =
            '=========================
            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & Kode_voucher2 & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("debit") <> Dr("kredit") Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Return False
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
                End If
            End Using

        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function


End Class