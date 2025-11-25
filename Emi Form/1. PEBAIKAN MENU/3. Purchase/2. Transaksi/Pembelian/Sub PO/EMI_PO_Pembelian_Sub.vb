Imports System.Globalization

Public Class EMI_PO_Pembelian_Sub
    Public FlagSelisihPO As String
    Public Asal As String = ""

    Dim isError As Boolean = True

    Dim judulForm As String = "Transaksi Purchase Order Existing"

    Dim Jenis = "Po_Bahan"
    Public arrPembayaran As New ArrayList
    Dim arrInisialFaktur, arrMUA, arrCrByr, ArrAkunCrByr As New ArrayList
    Dim arrFakPenawaran, arrNoPenawaran, arrSatuanPenawaran, arrHargaPenawaran As New ArrayList
    Dim arrTempoPenawaran, arrJatuhTempo As New ArrayList
    Dim arrEkspedisi As New ArrayList
    Dim arrNoUrutPr As New ArrayList

    Dim fakturSubmitPO As String = ""
    Dim arrInisialFakturSubmitPO As String = ""
    Dim no_Faktur_Sementara As String = ""
    Public PPN As Double = 0
    Dim canDeleteLv As Boolean

    Dim lvPO_Lokasi As String
    Dim lvPO_KdBarang As String
    Dim lvPO_NmBarang As String
    Dim lvPO_Harga As String
    Dim lvPO_JumlahPO As String
    Dim lvPO_Sisa As String
    Dim lvPO_Jumlah As String
    Dim lvPO_Satuan As String
    Dim lvPO_Harga_SB As String
    Dim lvPO_Jumlah_SB As String
    Dim lvPO_Satuan_SB As String
    Dim lvPO_NoPenawaran As String
    Dim lvPO_ID As String
    Dim lvPO_Total As String
    Dim lvPO_Urut As String
    Dim lvPO_PR As String
    Dim LvPO_TempoPembayaran As String
    Dim LvPO_JatuhTempo As String
    Dim LvPO_FakPenawaran As String
    Dim LvPO_Fakinduk, LvPO_UrutDet As String
    Dim LvPO_JumlahPOInput As String
    Dim LvPO_SatuanInput As String
    Dim LvPO_HargaInput As String
    Dim LvPO_SisaPOInput As String
    Dim LvPO_HargaDisplay As String


    Public cellPO_Lokasi As Integer = 0
    Public cellPO_KdBarang As Integer = 1
    Public cellPO_NmBarang As Integer = 2
    Public cellPO_Harga As Integer = 3
    Public cellPO_JumlahPO As Integer = 4
    Public cellPO_Sisa As Integer = 5
    Public cellPO_Jumlah As Integer = 6
    Public cellPO_Satuan As Integer = 7
    Public cellPO_Harga_SB As Integer = 8
    Public cellPO_Jumlah_SB As Integer = 9
    Public cellPO_Satuan_SB As Integer = 10
    Public cellPO_NoPenawaran As Integer = 11
    Public cellPO_ID As Integer = 12
    Public cellPO_Total As Integer = 13
    Public cellPO_Urut As Integer = 14
    Public cellPO_PR As Integer = 15
    Public cellTempoPembayaran As Integer = 16
    Public cellJatuhTempo As Integer = 17
    Public cellFakPenawaran As Integer = 18
    Public cellFakInduk As Integer = 19
    Public cellUrutDet As Integer = 20
    Public cellJnsKategori As Integer = 21
    Public cellJumlahPOInput As Integer = 22
    Public cellSatuanInput As Integer = 23
    Public cellHargaInput As Integer = 24
    Public cellSisaPOInput As Integer = 25
    Public cellHargaDisplay As Integer = 26

    Public No_SJ As String
    Public No_Plat As String
    Dim Fstatus As String = ""

    Private Sub get_no_faktur()
        TxtPO_NoFaktur.Text = fPO_EMI & arrInisialFaktur.Item(CmbPO_Lokasi.SelectedIndex) & "-" & Format(DtpPO_Tgl.Value, "MM/yy") & "-" &
                                     General_Class.Get_Last_Number2("EMI_Pembelian_PO", "no_faktur", Jumlah_Digit,
                                     "Kode_perusahaan", KodePerusahaan,
                                     "And", "substring(no_faktur,1," & Len(fPO_EMI) + Len(arrInisialFaktur.Item(CmbPO_Lokasi.SelectedIndex)) + 6 & ")", fPO_EMI & arrInisialFaktur.Item(CmbPO_Lokasi.SelectedIndex) & "-" & Format(DtpPO_Tgl.Value, "MM/yy"))
    End Sub

    Private Sub get_no_faktur_submit_Po()
        fakturSubmitPO = FSubmitPO & arrInisialFakturSubmitPO & "-" & Format(tgl_skg, "MM/yy") & "-" &
                          General_Class.Get_Last_Number2("Submit_PO", "No_Faktur", JumlahDigit,
                          "Kode_perusahaan", KodePerusahaan,
                          "And", "substring(No_Faktur,1," & Len(FSubmitPO) + Len(arrInisialFakturSubmitPO) + 6 & ")",
                           FSubmitPO & arrInisialFakturSubmitPO & "-" & Format(tgl_skg, "MM/yy"))
    End Sub

    Private Sub get_no_fakturLokasi()
        No_Fak = fLokasi_PO & arrInisialFaktur.Item(CmbPO_Lokasi.SelectedIndex) & "-" & Format(DtpPO_Tgl.Value, "MM/yy") & "-" &
                                     General_Class.Get_Last_Number2("EMI_Pembelian_Lokasi_Tujuan", "no_faktur", Jumlah_Digit,
                                     "Kode_perusahaan", KodePerusahaan,
                                     "And", "substring(no_faktur,1," & Len(fLokasi_PO) + Len(arrInisialFaktur.Item(CmbPO_Lokasi.SelectedIndex)) + 6 & ")", fLokasi_PO & arrInisialFaktur.Item(CmbPO_Lokasi.SelectedIndex) & "-" & Format(DtpPO_Tgl.Value, "MM/yy"))
    End Sub

    Private Sub Get_Isi_Listview(ByVal No_Index As Integer)
        lvPO_Lokasi = LvPO_DataPO.Rows(No_Index).Cells(cellPO_Lokasi).Value
        lvPO_KdBarang = LvPO_DataPO.Rows(No_Index).Cells(cellPO_KdBarang).Value
        lvPO_NmBarang = LvPO_DataPO.Rows(No_Index).Cells(cellPO_NmBarang).Value
        lvPO_Harga = LvPO_DataPO.Rows(No_Index).Cells(cellPO_Harga).Value
        lvPO_Jumlah = LvPO_DataPO.Rows(No_Index).Cells(cellPO_Jumlah).Value
        lvPO_Satuan = LvPO_DataPO.Rows(No_Index).Cells(cellPO_Satuan).Value
        lvPO_Harga_SB = LvPO_DataPO.Rows(No_Index).Cells(cellPO_Harga_SB).Value
        lvPO_Jumlah_SB = LvPO_DataPO.Rows(No_Index).Cells(cellPO_Jumlah_SB).Value
        lvPO_Satuan_SB = LvPO_DataPO.Rows(No_Index).Cells(cellPO_Satuan_SB).Value
        lvPO_NoPenawaran = LvPO_DataPO.Rows(No_Index).Cells(cellPO_NoPenawaran).Value
        lvPO_ID = LvPO_DataPO.Rows(No_Index).Cells(cellPO_ID).Value
        lvPO_Total = LvPO_DataPO.Rows(No_Index).Cells(cellPO_Total).Value
        lvPO_Urut = LvPO_DataPO.Rows(No_Index).Cells(cellPO_Urut).Value
        lvPO_PR = LvPO_DataPO.Rows(No_Index).Cells(cellPO_PR).Value
        LvPO_Fakinduk = LvPO_DataPO.Rows(No_Index).Cells(cellFakInduk).Value
        LvPO_UrutDet = LvPO_DataPO.Rows(No_Index).Cells(cellUrutDet).Value
        LvPO_JumlahPOInput = LvPO_DataPO.Rows(No_Index).Cells(cellJumlahPOInput).Value
        LvPO_SatuanInput = LvPO_DataPO.Rows(No_Index).Cells(cellSatuanInput).Value
        LvPO_HargaInput = LvPO_DataPO.Rows(No_Index).Cells(cellHargaInput).Value
        LvPO_SisaPOInput = LvPO_DataPO.Rows(No_Index).Cells(cellSisaPOInput).Value
        LvPO_HargaDisplay = LvPO_DataPO.Rows(No_Index).Cells(cellHargaDisplay).Value
    End Sub

    Private Sub HitungGrandTotal()
        Dim Grand As Double = 0
        Dim diskon As Double = 0
        Dim TotalSeluruh As Double = 0
        Dim PPN As Double = 0
        Dim totalPajak As Double = 0

        For i As Integer = 0 To LvPO_DataPO.Rows.Count - 1
            Get_Isi_Listview(i)

            Grand = Grand + HilangkanTanda(lvPO_Total)
        Next

        'End If
        TotalSeluruh = Grand * Val(TxtPO_Kurs.Text)
        PPN = TotalSeluruh * Val(TxtPO_PersenPPN.Text) / 100

        TxtPO_Total.Text = Format(Grand, "N2")
        TxtPO_TotalSblmPPN.Text = Format(TotalSeluruh, "N2")
        TxtPO_NilaiPPN.Text = Format(PPN, "N2")
        LblPO_TotalBiaya.Text = Format(TotalSeluruh + PPN, "N2")
        TxtPO_GrandTotal.Text = Format(TotalSeluruh + PPN, "N2")

        Try
            OpenConn()

            Dim berat As Double = 0
            For index = 0 To LvPO_DataPO.Rows.Count - 1
                Get_Isi_Listview(index)
                SQL = "select isnull(dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA',Kode_Barang,'Gram','Gram', berat),0) as hasil "
                SQL = SQL & "from barang where Kode_Barang='" & lvPO_KdBarang & "' and Kode_Stock_Owner='" & lvPO_Lokasi & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        berat += Dr("Hasil") * HilangkanTanda(lvPO_Jumlah_SB)
                    End If
                End Using
            Next

            Dim harga_satuan_besar As Double = 0
            SQL = "Select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & lvPO_KdBarang & "',"
            SQL = SQL & "'Gram','KG',"
            SQL = SQL & "" & berat & ") as Hasil "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    TxtPO_Berat.Text = Format(dr("hasil"), "N0") & " KG"
                End If
            End Using

            SQL = "select Persentase, Kode_Tarif from EMI_Detail_PPH_PO_Induk where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_Faktur_Induk.Text & "' and flag_ppn is null "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    totalPajak += Math.Round(Grand * (Val(HilangkanTanda(Dr("Persentase"))) / 100))
                Loop
            End Using

            Txt_GrandPPH.Text = Format(totalPajak, "N2")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub EMI_PO_Pembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LvSupplier2.Visible = False
        'LvSupplier.Location = New Point(165, 99)

        no_Faktur_Sementara = String.Empty
        no_Faktur_Sementara = TxtPO_NoFaktur.Text

        If Asal = "" Then
            LvPO_DataPO.Rows.Clear()
            kosong()
        End If


    End Sub

    Private Sub Kuncing_POSub()

        TxtPO_NoFaktur.Enabled = False
        DtpPO_Tgl.Enabled = False
        DtpPO_ETD.Enabled = False
        TxtPO_NoNota.Enabled = False
        TxtPO_KdSupplier.Enabled = True
        TxtPO_NmSupplier.Enabled = True
        CmbPO_MataUang.Enabled = False
        TxtPO_KdBrg.Enabled = False
        TxtPO_NmBrg.Enabled = False
        CmbPO_Harga.Enabled = False
        cmb_pr.Enabled = False
        txtSisaPr.Enabled = False
        txtSatuanSisa.Enabled = False
        TxtPO_Jml.Enabled = False
        CmbPO_Satuan.Enabled = False
        CmbPO_JnsBayar.Enabled = False
        cmbJenisPengiriman.Enabled = False
        txtJatuhTempo.Enabled = False
        TxtPO_Berat.Enabled = False
        CmbPO_JnsEkspedisi.Enabled = False
        CmbPO_CaraBayar.Enabled = False
        TxtPO_Total.Enabled = False
        TxtPO_TotalSblmPPN.Enabled = False
        'ChkPO_PPN.Enabled = False
        TxtPO_PersenPPN.Enabled = False
        TxtPO_NilaiPPN.Enabled = False
        TxtPO_GrandTotal.Enabled = False

        Button1.Enabled = True
        BtnPO_Clear.Enabled = False
        BtnPO_Ok.Enabled = False

    End Sub



    Public Sub kosong()

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        get_jam()

        LvPO_DataPO.Columns(cellFakInduk).DisplayIndex = 1

        LvPO_DataPO.Columns(cellHargaDisplay).DisplayIndex = 3
        LvPO_DataPO.Columns(cellJumlahPOInput).DisplayIndex = 5
        LvPO_DataPO.Columns(cellSatuanInput).DisplayIndex = 8
        LvPO_DataPO.Columns(cellSisaPOInput).DisplayIndex = 6

        LblPO_CaraBayar.Text = Base_Language.Lang_Global_CaraBayar
        LblPO_Diskon.Text = Base_Language.Lang_Global_Diskon_Persen
        LblPO_GrandTotal.Text = Base_Language.Lang_Global_Grand
        LblPO_Hrg.Text = Base_Language.Lang_Global_Harga
        LblPO_Jml.Text = Base_Language.Lang_Global_Jumlah
        LblPO_KdBarang.Text = Base_Language.Lang_Global_KodeBarang
        LblPO_Kurs.Text = Base_Language.Lang_Global_Kurs
        LblPO_MataUang.Text = Base_Language.Lang_Global_MataUang
        LblPO_NmBarang.Text = Base_Language.Lang_Global_NamaBarang
        LblPO_NoNota.Text = "Keterangan"
        LblPO_Pembayaran.Text = Base_Language.Lang_Global_JenisPembayaran
        LblPO_PPN.Text = Base_Language.Lang_Global_PPN
        LblPO_Supplier.Text = Base_Language.Lang_Global_Supplier
        LblPO_Tgl.Text = Base_Language.Lang_Global_Tanggal
        LblPO_TotalBiaya.Text = Base_Language.Lang_Global_Total
        LblPO_TotalMUA.Text = Base_Language.Lang_Global_TotalMUA
        LblPO_TotalSblmPPN.Text = Base_Language.Lang_Global_GrandSblmPPN

        LblPO_Berat.Text = Base_Language.Lang_Global_Berat
        LblPO_biaya.Text = Base_Language.Lang_Global_biaya
        LblPO_ETD.Text = Base_Language.Lang_Global_ETD
        LblPO_Ekspedisi.Text = Base_Language.Lang_Global_Ekspedisi
        LblPO_Satuan.Text = Base_Language.Lang_Global_Satuan
        LblPO_satBarang.Text = Base_Language.Lang_Global_Satuan_Barang

        BtnPO_Clear.Text = "Clear"
        BtnPO_Ok.Text = "OK"
        BtnPO_Refresh.Text = Base_Language.Lang_Global_Refresh
        BtnPO_Simpan.Text = Base_Language.Lang_Global_Simpan

        LblPO_Judul.Text = Base_Language.Lang_PO_Bahan_Judul

        Txt_GrandPPH.Text = ""
        No_SJ = ""
        No_Plat = ""
        FlagSelisihPO = ""
        TxtPO_NoNota.Text = ""
        DtpPO_Tgl.Value = tgl_skg
        DtpPO_TglBayar.Value = tgl_skg
        TxtPO_NoPO.Text = ""
        TxtPO_KdSupplier.Text = ""
        TxtPO_NmSupplier.Text = ""
        TxtPO_PersenPPN.Text = "0"
        TxtPO_Biaya.Text = "0"
        TxtPO_Kurs.Text = 1


        TxtPO_Total.Text = "0"
        TxtPO_TotalSblmPPN.Text = "0"
        TxtPO_GrandTotal.Text = "0"

        DtpPO_TglBayar.Visible = False
        'CmbPO_RangeBayar.Visible = False
        CmbPO_CaraBayar.Enabled = True
        ChkPO_PPN.Checked = False
        ListView1.Visible = False

        If Asal = "" Then
            CmbPO_MataUang.Enabled = True
        End If

        CmbPO_MataUang.Enabled = True




        ListView1.Columns.Clear()
        ListView1.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        ListView1.Columns.Add("Nama Barang", 300, HorizontalAlignment.Left)
        ListView1.View = View.Details

        'LvPO_DataPO.Rows.Clear()

        CmbPO_Lokasi.Enabled = False


        Cmb_Ekspedisi.Items.Clear()
        Cmb_Ekspedisi.Items.Add("Pilih Sendiri")
        Cmb_Ekspedisi.Items.Add("Ekepedisi")

        Try
            OpenConn()
            CmbPO_Lokasi.Items.Clear() : arrInisialFaktur.Clear()
            SQL = "select Kode_Stock_Owner, persediaan ,inisial_faktur from Stock_Owner where kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' order by Kode_Stock_Owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbPO_Lokasi.Items.Add(dr("Kode_Stock_Owner")) : arrInisialFaktur.Add(dr("inisial_faktur"))
                Loop
            End Using

            CmbPO_LokasiGudang.Items.Clear()
            SQL = "select Kode_Stock_Owner from View_Lokasi_Stock where kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' order by Kode_Stock_Owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbPO_LokasiGudang.Items.Add(dr("Kode_Stock_Owner"))
                Loop
            End Using

            CmbPO_Lokasi.Text = "HEAD OFFICE"

            SQL = "select Kode_Stock_Owner_gudang from Binding_Lokasi_Gudang where kode_perusahaan = '" & KodePerusahaan & "' and Gudang_Default = 'Y' and Kode_Stock_Owner='" & CmbPO_Lokasi.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    CmbPO_LokasiGudang.Text = dr("Kode_Stock_Owner_gudang")
                End If
            End Using

            CmbPO_CaraBayar.Items.Clear()
            CmbPO_MataUang.Items.Clear()
            arrCrByr.Clear()
            ArrAkunCrByr.Clear()

            'FMenu.MenuStrip1.Visible = True
            CmbPO_CaraBayar.Items.Add("-- Cara Bayar --") : arrCrByr.Add("NULL") : ArrAkunCrByr.Add("")
            CmbPO_CaraBayar.SelectedIndex = 0
            SQL = "select kode_cb, keterangan, kode_account_cb from cara_bayar where kode_perusahaan = '" & KodePerusahaan & "'  order by keterangan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    CmbPO_CaraBayar.Items.Add(Dr("keterangan")) : arrCrByr.Add(Dr("kode_cb")) : ArrAkunCrByr.Add(Dr("kode_account_cb"))
                Loop
            End Using

            CmbPO_MataUang.Items.Add("-- Mata Uang --") : arrMUA.Add("")
            'CmbPO_MataUang.Enabled = True
            CmbPO_MataUang.SelectedIndex = 0
            SQL = "select kode_mata_uang from mata_uang where kode_perusahaan = '" & KodePerusahaan & "' order by kode_mata_uang"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    CmbPO_MataUang.Items.Add(Dr("kode_mata_uang")) : arrMUA.Add(Dr("kode_mata_uang"))
                Loop
            End Using

            CmbPO_MataUang.Text = "RP"


            cmbJenisPengiriman.Items.Clear()
            SQL = "select Kode_Jenis_Perhitungan_JT,Keterangan from Emi_Master_Perhitungan_Jatuh_Tempo "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    cmbJenisPengiriman.Items.Add(Dr("kode_jenis_perhitungan_jt"))
                Loop
            End Using

            cmbJenisPengiriman.SelectedIndex = -1

            If CmbPO_MataUang.Text.Trim = "" Then
                CloseConn()
                MessageBox.Show("Mata Uang Blum Ada")
                Exit Sub
            End If


            SQL = "Delete Emi_Expedition_PO_Sementara where Kode_Perusahaan = '" & KodePerusahaan & "' and userid = '" & UserID & "'"
            ExecuteTrans(SQL)



            LvSupplier2.Visible = False

            get_no_faktur()


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'CmbPO_RangeBayar.Items.Clear()
        'For i As Integer = 1 To Jatuh_Tempo_Pembelian
        '    CmbPO_RangeBayar.Items.Add(i)
        'Next
        'CmbPO_RangeBayar.SelectedIndex = 0

        CmbPO_JnsBayar.Items.Clear() : arrPembayaran.Clear()
        CmbPO_JnsBayar.Items.Add("Tunai") : arrPembayaran.Add("T")
        CmbPO_JnsBayar.Items.Add("Non-Tunai") : arrPembayaran.Add("N")

        CmbPO_JnsEkspedisi.Items.Clear() : arrEkspedisi.Clear()
        CmbPO_JnsEkspedisi.Items.Add("-- Ekspedisi --") : arrEkspedisi.Add("NULL")
        CmbPO_JnsEkspedisi.Items.Add("Sendiri") : arrEkspedisi.Add("1")
        CmbPO_JnsEkspedisi.Items.Add("Lain - Lain") : arrEkspedisi.Add("2")
        CmbPO_JnsEkspedisi.SelectedIndex = 0
        TxtPO_Biaya.Enabled = False

        TxtPO_KdSupplier.Enabled = True
        TxtPO_NmSupplier.Enabled = True
        TxtPO_KdBrg.Enabled = True
        TxtPO_Jml.Enabled = True
        CmbPO_Satuan.Enabled = True

        DtpPO_ETD.Enabled = True
        DtpPO_Tgl.Enabled = True
        DtpPO_TglBayar.Enabled = True
        TxtPO_NoPO.Enabled = True
        TxtPO_KdSupplier.Enabled = True
        TxtPO_NmSupplier.Enabled = True
        CmbPO_JnsBayar.Enabled = True
        'CmbPO_RangeBayar.Enabled = True
        CmbPO_JnsEkspedisi.Enabled = True
        CmbPO_CaraBayar.Enabled = True
        TxtPO_Biaya.Enabled = True
        ChkPO_PPN.Enabled = True
        TxtPO_NoNota.Enabled = True
        Cmb_Ekspedisi.Enabled = False
        Cmb_Ekspedisi.SelectedIndex = -1

        Button2.Visible = False
        Btn_Ekspedisi.Visible = False
        Lbl_BiayaEkspedisi.Visible = False
        Txt_BiayaEkspedisi.Visible = False

        Txt_BiayaEkspedisi.Text = ""

        BtnPO_Simpan.Tag = "&Simpan"

        LvPO_DataPO.Enabled = True
        canDeleteLv = False

        bersihsebagian()
        HitungGrandTotal()

        TxtPO_NmSupplier.Focus()
        LvSupplier2.Location = New Point(132, 179)

        Kuncing_POSub()


    End Sub

    Private Sub TxtPO_KdBrg_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPO_KdBrg.KeyDown
        If e.KeyCode = Keys.Down Then ListView1.Focus()
    End Sub

    Private Sub TxtPO_KdBrg_TextChanged(sender As Object, e As EventArgs) Handles TxtPO_KdBrg.TextChanged

        CmbPO_Satuan.SelectedIndex = -1
        'ComboBox1.Enabled = False
        If TxtPO_KdBrg.Text.Length >= 3 Then

            If TxtPO_KdBrg.Text.Trim.Length = 0 Then
                ListView1.Visible = False : Exit Sub
            Else
                ListView1.Visible = True
            End If

            Try

                OpenConn()

                ListView1.Items.Clear() : cmb_pr.Items.Clear()

                SQL = "Select a.kode_barang,a.nama, a.satuan from "
                SQL = SQL & "barang a, emi_group_jenis b, Emi_Role_Kategori_PO e "
                SQL = SQL & "where a.kode_Perusahaan = b.kode_Perusahaan And a.id_group_jenis = b.Id_group_jenis "
                SQL = SQL & " And a.Kode_Perusahaan = e.Kode_Perusahaan And a.Id_Kategori_PO = e.Kategori_PO "
                SQL = SQL & "And a.kode_stock_owner = '" & CmbPO_LokasiGudang.Text & "' "
                SQL = SQL & " And a.nama Like  '%" & TxtPO_KdBrg.Text & "%' and (Flag_Raw_Material='Y' or Flag_Packaging='Y' or Flag_bahan_bakar = 'Y')  "
                SQL = SQL & " And e.UserID = '" & UserID & "' and a.kode_perusahaan = '" & KodePerusahaan & "' "

                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim LvPO_DataPO As ListViewItem
                        LvPO_DataPO = ListView1.Items.Add(Dr("kode_barang"))
                        LvPO_DataPO.SubItems.Add(Dr("nama"))
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else

            ListView1.Visible = False

        End If
    End Sub

    Private Sub bersihsebagian()
        TxtPO_KdBrg.Clear()
        TxtPO_NmBrg.Clear()
        TxtPO_SatuanBarang.Clear()
        txtSatuanSisa.Clear()
        txtSisaPr.Clear()
        TxtPO_Jml.Clear()
        CmbPO_Satuan.Items.Clear()
        CmbPO_Harga.Items.Clear()
        cmb_pr.Items.Clear()

        LblPO_TotalBiaya.Text = Format(0, "N2")
        TxtPO_Berat.Text = Format(0, "N2")
        TxtPO_TotalSblmPPN.Text = Format(0, "N2")
        TxtPO_PersenPPN.Text = 0
        TxtPO_NilaiPPN.Text = Format(0, "N2")
        TxtPO_GrandTotal.Text = Format(0, "N2")
        TxtPO_Total.Text = Format(0, "N2")


    End Sub

    Public Sub TxtPO_KdBrg_Leave(sender As Object, e As EventArgs) Handles TxtPO_KdBrg.Leave
        If TxtPO_KdBrg.Text.Length = 0 Then
            ListView1.Visible = False : Exit Sub
        Else
            ListView1.Visible = True
        End If

        If ListView1.Focused = True Then Exit Sub

        If CmbPO_LokasiGudang.Text.Trim.Length = 0 Then
            MessageBox.Show("Stock owner harus diisi dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtPO_KdBrg.Text = "" : CmbPO_Lokasi.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "Select a.kode_barang,a.nama, a.satuan from "
            SQL = SQL & "barang a, emi_group_jenis b, Emi_Role_Kategori_PO e "
            SQL = SQL & "where a.kode_Perusahaan = b.kode_Perusahaan And a.id_group_jenis = b.Id_group_jenis "
            SQL = SQL & " And a.Kode_Perusahaan = e.Kode_Perusahaan And a.Id_Kategori_PO = e.Kategori_PO "
            SQL = SQL & "And a.kode_stock_owner = '" & CmbPO_LokasiGudang.Text & "' "
            SQL = SQL & " And a.Kode_barang = '" & TxtPO_KdBrg.Text & "' and (Flag_Raw_Material='Y' or Flag_Packaging='Y' or b.Flag_bahan_bakar = 'Y')  "
            SQL = SQL & " And e.UserID = '" & UserID & "' and a.kode_perusahaan = '" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    TxtPO_KdBrg.Text = dr("kode_barang")
                    TxtPO_NmBrg.Text = dr("nama")
                    TxtPO_SatuanBarang.Text = dr("satuan")
                    dr.Close()
                    CmbPO_Satuan.Items.Clear()

                    Dim indexSatuanTampilDisplay As Integer

                    SQL = "select Satuan, flag_tampil_display from barang_Detail_Satuan where Kode_Barang= '" & Trim(TxtPO_KdBrg.Text) & "' "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")

                            If .Rows.Count <> 0 Then

                                For i As Integer = 0 To .Rows.Count - 1

                                    If General_Class.CekNULL(.Rows(i).Item("flag_tampil_display")) = "Y" Then
                                        indexSatuanTampilDisplay = i
                                    End If
                                    CmbPO_Satuan.Items.Add(.Rows(i).Item("satuan"))

                                Next

                            End If

                        End With

                        'Do While dr2.Read
                        '    CmbPO_Satuan.Items.Add(dr2("satuan"))
                        'Loop
                    End Using

                    CmbPO_Satuan.SelectedIndex = indexSatuanTampilDisplay
                    CmbPO_Satuan.Enabled = False

                    CmbPO_Harga.Items.Clear() : arrNoPenawaran.Clear() : arrSatuanPenawaran.Clear() : arrHargaPenawaran.Clear()
                    arrTempoPenawaran.Clear() : arrJatuhTempo.Clear()
                    SQL = "select a.No_Faktur,a.no_penawaran,a.Kode_Supplier, c.Nama,b.satuan, b.Nilai_Barang,b.harga_satuan, b.satuan_Barang,  "

                    SQL = SQL & "isnull((select top(1)  x.Lama_Pembayaran from EMI_Master_Penawaran_Jatuh_Tempo x where a.Kode_Perusahaan = x.Kode_Perusahaan "
                    SQL = SQL & "and a.No_Faktur = x.No_Faktur), 0) as jatuh_Tempo,"

                    SQL = SQL & "isnull((select top(1) x.Tempo_Pembayaran from EMI_Master_Penawaran_Jatuh_Tempo x where a.Kode_Perusahaan = x.Kode_Perusahaan "
                    SQL = SQL & "and a.No_Faktur = x.No_Faktur), null) as Tempo_Pembayaran "


                    SQL = SQL & "from EMI_Master_Penawaran a, EMI_Master_Penawaran_Detail b, Suppliers c "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
                    SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Supplier = c.Kode_Supplier "
                    SQL = SQL & "and b.kode_barang = '" & TxtPO_KdBrg.Text & "' and a.Kode_Supplier='" & TxtPO_KdSupplier.Text & "' "
                    SQL = SQL & "and flag_release = 'Y' and status is null and b.mata_uang = '" & CmbPO_MataUang.Text & "' and a.Selesai is null and '" & Format(tgl_skg, "yyyy-MM-dd") & "' between a.Tgl_Penawaran_Hrg and a.Periode_Akhir_Penawaran  "
                    Using dr2 = OpenTrans(SQL)
                        Do While dr2.Read
                            CmbPO_Harga.Items.Add(Format(dr2("harga_satuan")) & "/" & dr2("satuan").ToString.Trim & "-" & dr2("nama"))
                            arrFakPenawaran.Add(dr2("No_Faktur")) : arrNoPenawaran.Add(dr2("no_penawaran"))
                            arrSatuanPenawaran.Add(dr2("satuan_Barang")) : arrHargaPenawaran.Add(dr2("Nilai_Barang"))
                            arrJatuhTempo.Add(dr2("jatuh_tempo"))
                            If General_Class.CekNULL(dr2("tempo_pembayaran")) = "" Then
                                arrTempoPenawaran.Add("-")
                            Else
                                arrTempoPenawaran.Add(dr2("tempo_pembayaran"))
                            End If
                        Loop
                    End Using

                    cmb_pr.Items.Clear() : arrNoUrutPr.Clear()
                    SQL = "select a.No_Faktur, b.no_Urut,b.tanggal_delivery  From EMI_Purchase_Requisition a, EMI_Purchase_Requisition_Detail b "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
                    SQL = SQL & "and a.Status is null and flag_release = 'Y' and b.Kode_Barang = '" & TxtPO_KdBrg.Text & "' and b.flag_sudah_po is null and b.flag_tolak is null "
                    '   SQL = SQL & "group by a.no_faktur"
                    Using dr3 = OpenTrans(SQL)
                        Do While dr3.Read
                            cmb_pr.Items.Add(dr3("no_faktur") & " / " & dr3("tanggal_delivery")) : arrNoUrutPr.Add(dr3("no_urut"))
                        Loop
                    End Using

                    CmbPO_Harga.Focus()
                Else
                    bersihsebagian()
                    TxtPO_KdBrg.Focus()
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        ListView1.Visible = False
    End Sub

    Private Sub TxtPO_KdBrg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPO_KdBrg.KeyPress
        If e.KeyChar = Chr(13) Then
            If TxtPO_KdBrg.Text.Trim.Length = 0 Then
                ListView1.Visible = False : TxtPO_KdBrg.Focus() : Exit Sub
            End If
            TxtPO_KdBrg_Leave(TxtPO_KdBrg, e)
        End If
    End Sub

    Private Sub ListView1_KeyDown(sender As Object, e As KeyEventArgs) Handles ListView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView1_DoubleClick(Me, e)
        End If
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        If ListView1.Items.Count = 0 Then Exit Sub
        Dim Kode As String = ListView1.FocusedItem.Text
        Dim Nama As String = ListView1.FocusedItem.SubItems(1).Text

        TxtPO_KdBrg.Text = Kode
        TxtPO_NmBrg.Text = Nama

        ''Try
        ''    OpenConn()

        ''    SQL = "select satuan from Barang_Detail_Satuan where kode_perusahaan = '" & KodePerusahaan & "' "
        ''    SQL = SQL & " and kode_barang = '" & Kode & "' order by satuan "
        ''    Using Dr = OpenTrans(SQL)
        ''        If Dr.Read Then
        ''            CmbPO_Satuan.Items.Clear()
        ''            CmbPO_Satuan.Items.Add(Dr("satuan"))

        ''        Else
        ''            CloseConn()
        ''            MessageBox.Show("Satuan barang belum di isi")
        ''            Exit Sub
        ''        End If
        ''    End Using

        ''    CloseConn()
        ''Catch ex As Exception
        ''    CloseConn()
        ''    MessageBox.Show(ex.Message)
        ''    Exit Sub
        ''End Try

        ListView1.Visible = False
        'ComboBox1.Enabled = True
        TxtPO_KdBrg_Leave(ListView1, e)
        CmbPO_Harga.Focus()
    End Sub

    Private Sub TxtPO_Hrg_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then
            TxtPO_Jml.Focus()
        End If
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TxtPO_Jml_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPO_Jml.KeyPress
        If e.KeyChar = Chr(13) Then
            CmbPO_Satuan.Focus()
        End If

        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbPO_Satuan.KeyPress
        'If e.KeyChar = Chr(13) Then
        '    RdoPO_Persen.Focus()
        'End If
    End Sub

    Private Sub RdoPO_Persen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles RdoPO_Persen.KeyPress
        If e.KeyChar = Chr(13) Then
            TxtPO_Diskon.Focus()
        End If

    End Sub

    Private Sub TxtPO_Diskon_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPO_Diskon.KeyPress
        If e.KeyChar = Chr(13) Then
            BtnPO_Ok.Focus()
            BtnPO_Ok_Click(Me, e)
        End If
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TxtPO_Kurs_TextChanged(sender As Object, e As EventArgs) Handles TxtPO_Kurs.TextChanged
        HitungGrandTotal()
    End Sub


    Private Sub BtnPO_Simpan_Click(sender As Object, e As EventArgs) Handles BtnPO_Simpan.Click
        'If TxtPO_NoPO.Text.Trim.Length = 0 Then
        '    MessageBox.Show("No PO Harus di isi")
        '    TxtPO_NoPO.Focus() : Exit Sub
        'Else
        If TxtPO_NoNota.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_NoNota & " " & Base_Language.Lang_Global_Belum_Diisi & ". . .! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtPO_NoNota.Focus() : Exit Sub
        ElseIf TxtPO_KdSupplier.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Supplier & " " & Base_Language.Lang_Global_Belum_Diisi & ". . .! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtPO_KdSupplier.Focus() : Exit Sub
        ElseIf TxtPO_Kurs.Text.Trim.Length = 0 Or TxtPO_Kurs.Text = "0" Then
            MessageBox.Show(Base_Language.Lang_Global_Kurs & " " & Base_Language.Lang_Global_Belum_Diisi & ". . .! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtPO_Kurs.Focus() : Exit Sub
        ElseIf CmbPO_Lokasi.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Lokasi & " " & Base_Language.Lang_Global_Belum_Diisi & ". . .! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbPO_Lokasi.Focus() : Exit Sub
        ElseIf CmbPO_JnsBayar.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_JenisPembayaran & " " & Base_Language.Lang_Global_Belum_Diisi & ". . .! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbPO_JnsBayar.Focus() : Exit Sub
        ElseIf CmbPO_MataUang.Text.Trim.Length = 0 Or CmbPO_MataUang.SelectedIndex = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_MataUang & " " & Base_Language.Lang_Global_Belum_Diisi & ". . .! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbPO_MataUang.Focus() : Exit Sub
        ElseIf LvPO_DataPO.Rows.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            LvPO_DataPO.Focus() : Exit Sub
        ElseIf Cmb_Ekspedisi.SelectedIndex = -1 Then
            MessageBox.Show("Expedisi Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Ekspedisi.Focus() : Exit Sub
        ElseIf arrPembayaran.Item(CmbPO_JnsBayar.SelectedIndex) = "T" Then
            'If CmbPO_CaraBayar.Text.Trim.Length = 0 Or CmbPO_CaraBayar.SelectedIndex = 0 Then
            '    MessageBox.Show(Base_Language.Lang_Global_CaraBayar & " " & Base_Language.Lang_Global_Belum_Diisi & ". . .! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    CmbPO_CaraBayar.Focus() : Exit Sub
            'End If

        End If

        If Cmb_Ekspedisi.SelectedIndex = 1 Then
            If Val(HilangkanTanda(Txt_BiayaEkspedisi.Text)) = 0 Then
                MessageBox.Show("Expedisi Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Btn_Ekspedisi.Focus() : Exit Sub
            End If
        End If

        get_jam()

        Dim cb As String = ""
        Dim Tgl_Jatuh_Tempo As String = ""
        If arrPembayaran.Item(CmbPO_JnsBayar.SelectedIndex) = "T" Then 'tunai

            Dim selectedValue As String = arrCrByr.Item(CmbPO_CaraBayar.SelectedIndex).ToString().ToUpper()

            cb = If(selectedValue = "NULL", "NULL", "'" & selectedValue & "'")
            Tgl_Jatuh_Tempo = "NULL"
        Else
            cb = "NULL"
            Tgl_Jatuh_Tempo = "'" & Format(DtpPO_TglBayar.Value, "yyyy-MM-dd") & "'"
        End If

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction


            If BtnPO_Simpan.Tag = "&Simpan" Then
                get_no_faktur()



                Dim no_po As String = ""
                If TxtPO_NoPO.Text.Trim.Length = 0 Then
                    no_po = "NULL"
                Else
                    no_po = "'" & TxtPO_NoPO.Text & "'"
                End If


                Dim flag_kategori_Supplier As String = ""
                '========== CEK JENIS NYA IMPORT ATAU LOKAL ===========================================
                SQL = "select b.Flag_Jenis_Import From Suppliers a, Suppliers_Kategori b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and  "
                SQL = SQL & "a.ID_Kategori_Suppliers = b.ID_Kategori_Suppliers "
                SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and Kode_Supplier	 = '" & TxtPO_KdSupplier.Text.Trim & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        flag_kategori_Supplier = General_Class.CekNULL(Dr("flag_jenis_import"))
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_GLOBAL_Kategori_Supplier & " " & Base_Language.Lang_GLOBAL_Tidak_Ditemukan & ". . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                Dim Import As String = "NULL"

                If flag_kategori_Supplier = "Y" Then
                    Import = "'Y'"
                End If

                Dim terbilang As String = General_Class.SayMUA(Math.Round(Val(HilangkanTanda(TxtPO_GrandTotal.Text)), 0), CmbPO_MataUang.Text)


                SQL = "insert into emi_pembelian_PO(Kode_Perusahaan, No_Faktur, No_Faktur_Induk, No_Nota, Tanggal, Jam, UserID, "
                SQL = SQL & "Kode_Supplier, Lokasi,Jenis_Pembayaran, Mata_Uang, Kurs, Cara_Bayar, Total_MUA, "
                SQL = SQL & "Total_IDR, Grand_Sebelum_PPN, PPN,Grand, No_Prepare_Bahan, ETD_Simulasi, "
                SQL = SQL & "Tgl_Jatuh_Tempo,ekspedisi,biaya, Flag_Import, tempo_pembayaran, Lama_Pembayaran, Grand_Total_Terbilang, Grand_PPH) values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & TxtPO_NoFaktur.Text & "', NULL, '" & TxtPO_NoNota.Text & "', "
                SQL = SQL & "'" & Format(DtpPO_Tgl.Value, " yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
                SQL = SQL & "'" & UserID & "', '" & TxtPO_KdSupplier.Text & "', '" & CmbPO_Lokasi.Text & "', "
                SQL = SQL & "'" & arrPembayaran.Item(CmbPO_JnsBayar.SelectedIndex) & "', "
                SQL = SQL & "'" & CmbPO_MataUang.Text & "', '" & TxtPO_Kurs.Text & "', " & cb & ", "
                SQL = SQL & "'" & HilangkanTanda(TxtPO_Total.Text) & "', '" & HilangkanTanda(TxtPO_Total.Text) & "', "
                SQL = SQL & "'" & HilangkanTanda(TxtPO_TotalSblmPPN.Text) & "', '" & TxtPO_PersenPPN.Text & "', "
                SQL = SQL & "'" & HilangkanTanda(TxtPO_GrandTotal.Text) & "', " & no_po & ", "
                SQL = SQL & "'" & Format(DtpPO_ETD.Value, "yyyy-MM-dd") & "'," & Tgl_Jatuh_Tempo & ", "
                SQL = SQL & "'" & CmbPO_JnsEkspedisi.Text & "', '" & TxtPO_Biaya.Text & "', " & Import & ", '" & cmbJenisPengiriman.Text & "', "
                SQL = SQL & Val(HilangkanTanda(txtJatuhTempo.Text)) & ", '" & terbilang & "', '" & HilangkanTanda(Txt_GrandPPH.Text) & "' )"
                ExecuteTrans(SQL)

                '==========================
                '=     GET DETAIL PPH     =
                '==========================
                SQL = "select Persentase, Kode_Tarif, Flag_PPN, Kode_Akun from EMI_Detail_PPH_PO_Induk where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_Faktur_Induk.Text & "' "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                '=============================
                                '=     INSERT DETAIL PPH     =
                                '=============================
                                Dim totalPajak As Double = Math.Round(HilangkanTanda(TxtPO_Total.Text) * (Val(HilangkanTanda(.Rows(i).Item("Persentase"))) / 100))

                                SQL = "insert into EMI_Detail_PPH_PO (Kode_Perusahaan, No_faktur_Induk, No_faktur, Persentase, Nilai, Kode_Tarif, Flag_PPN, Kode_Akun) values "
                                SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_Faktur_Induk.Text & "', '" & TxtPO_NoFaktur.Text & "', '" & HilangkanTanda(.Rows(i).Item("Persentase")) & "', '" & HilangkanTanda(totalPajak) & "', '" & HilangkanTanda(.Rows(i).Item("Kode_Tarif")) & "', "
                                SQL = SQL & If(General_Class.CekNULL(.Rows(i).Item("Flag_PPN")) = "Y", "'Y'", "NULL") & ", '" & .Rows(i).Item("Kode_Akun") & "')"
                                ExecuteTrans(SQL)
                            Next

                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data PPH Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With

                End Using



                '============================
                '=     INSERT EKSPEDISI     =
                '============================
                Dim TotTarif As Double = 0
                Dim TotBiayaLain As Double = 0
                Dim TotGrand As Double = 0

                'Insert Detail
                SQL = "select Kode_Perusahaan, No_Faktur_Ekspedisi, No_Penawaran, Kode_Biaya, urut_penawaran "
                SQL = SQL & "from Emi_Expedition_PO_Sementara where Kode_Perusahaan = '" & KodePerusahaan & "' and userid = '" & UserID & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1

                                SQL = "select a.Kode_Perusahaan, a.No_Faktur, a.No_Penawaran, a.Tanggal_Mulai, a.Tanggal_Selesai, c.Keterangan as Jenis_Pengiriman, d.Nama as Perusahaan_Biaya_Import, a.Kode_Biaya, a.Mata_Uang, "
                                SQL = SQL & "e.Keterangan as Jenis_Kendaraan, (CAST(e.Kapasitas as Varchar)  +' ' + e.Satuan_Kapasitas) as Kapasitas_Kendaraan, b.Tarif, b.Biaya_Lain, b.Total, "
                                SQL = SQL & "a.Jenis_Pengiriman, a.Kode_Perusahaan_Biaya_import, b.Jenis_Kendaraan, b.No_Urut "
                                SQL = SQL & "from Emi_Expedition_PO a, Emi_Expedition_PO_Detail b, EMI_Cara_Kirim c, perusahaan_biaya_import d, Master_Kendaraan e "
                                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan    "
                                SQL = SQL & "and b.Kode_Perusahaan = e.Kode_Perusahaan "
                                SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                                SQL = SQL & "and a.Kode_Perusahaan_Biaya_import = d.Kode_Perusahaan_Biaya_import "
                                SQL = SQL & "and a.Jenis_Pengiriman = c.Id_Cara_Kirim "
                                SQL = SQL & "and b.Jenis_Kendaraan = e.Id_Kendaraan "
                                SQL = SQL & "and a.status is null "
                                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "AND a.No_Faktur = '" & .Rows(i).Item("No_Faktur_Ekspedisi") & "' and a.No_Penawaran = '" & .Rows(i).Item("No_Penawaran") & "' and a.Kode_Biaya = '" & .Rows(i).Item("Kode_Biaya") & "' "
                                SQL = SQL & "AND a.Tanggal_Mulai <= '" & Format(tgl_skg, "yyyy-MM-dd") & "' AND a.Tanggal_Selesai >= '" & Format(tgl_skg, "yyyy-MM-dd") & "' and b.No_Urut ='" & .Rows(i).Item("urut_penawaran") & "' "
                                SQL = SQL & "order by d.Nama, a.Tanggal_Mulai "
                                Using Ds2 = BindingTrans(SQL)
                                    If Ds2.Tables("MyTable").Rows.Count <> 0 Then

                                        SQL = "insert into EMI_Pembelian_PO_Ekspedisi_Detail(Kode_Perusahaan, No_Faktur_PO, No_Faktur_Ekspedisi, No_Penawaran, Tgl_Mulai_Penawaran, Tgl_Selesai_Penawaran, Tarif, Biaya_Lain, Total, Urut_Ekspedisi) "
                                        SQL = SQL & "values ('" & KodePerusahaan & "', '" & TxtPO_NoFaktur.Text & "', '" & Ds2.Tables("MyTable").Rows(0).Item("No_Faktur") & "', "
                                        SQL = SQL & "'" & Ds2.Tables("MyTable").Rows(0).Item("No_Penawaran") & "', '" & Format(Ds2.Tables("MyTable").Rows(0).Item("Tanggal_Mulai"), "yyyy-MM-dd") & "', "
                                        SQL = SQL & "'" & Format(Ds2.Tables("MyTable").Rows(0).Item("Tanggal_Selesai"), "yyyy-MM-dd") & "', " & Val(HilangkanTanda(Ds2.Tables("MyTable").Rows(0).Item("Tarif"))) & ", "
                                        SQL = SQL & Val(HilangkanTanda(Ds2.Tables("MyTable").Rows(0).Item("Biaya_Lain"))) & ", '" & Val(HilangkanTanda(Ds2.Tables("MyTable").Rows(0).Item("Total"))) & "', '" & Ds2.Tables("MyTable").Rows(0).Item("No_Urut") & "')"
                                        ExecuteTrans(SQL)

                                        TotTarif += Val(HilangkanTanda(Ds2.Tables("MyTable").Rows(0).Item("Tarif")))
                                        TotBiayaLain += Val(HilangkanTanda(Ds2.Tables("MyTable").Rows(0).Item("Biaya_Lain")))
                                        TotGrand += Val(HilangkanTanda(Ds2.Tables("MyTable").Rows(0).Item("Total")))
                                    Else
                                        'Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("data ekspedisi tidak ditemukan. . ! ! (1)", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                            Next
                        End If
                    End With
                End Using

                'Insert Induk Ekspedisi
                SQL = "insert into EMI_Pembelian_PO_Ekspedisi (Kode_Perusahaan, No_Faktur_PO, Total_Tarif, Total_Biaya_Lain, Grand, Tanggal, Jam, UserID) "
                SQL = SQL & "values ('" & KodePerusahaan & "', '" & TxtPO_NoFaktur.Text & "', " & Val(HilangkanTanda(TotTarif)) & ", " & Val(HilangkanTanda(TotBiayaLain)) & ", "
                SQL = SQL & "'" & Val(HilangkanTanda(TotGrand)) & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "')"
                ExecuteTrans(SQL)



                Dim Flag_PPn As String = ""

                For index = 0 To LvPO_DataPO.Rows.Count - 1
                    Get_Isi_Listview(index)

                    '=======================
                    '=     UBAH SATUAN     =
                    '=======================
                    Dim jumlahConvert As Double = 0
                    SQL = "select isnull((" & HilangkanTanda(lvPO_Jumlah) & " * a.Nilai), 0) as Hasil "
                    SQL = SQL & "from N_EMI_Master_Satuan a "
                    SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and a.Kode_Barang = '" & lvPO_KdBarang & "' "
                    SQL = SQL & "and a.Satuan = '" & LvPO_SatuanInput & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            jumlahConvert = Dr("Hasil")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Satuan " & LvPO_SatuanInput & " pada barang " & lvPO_NmBarang & " Tidak Ditemukan pada Master Barang ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using







                    SQL = "select flag_PPn from barang where "
                    SQL = SQL & "Kode_barang='" & lvPO_KdBarang & "' and Kode_Stock_Owner ='" & lvPO_Lokasi & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            If index = 0 Then
                                Flag_PPn = General_Class.CekNULL(dr("flag_PPn"))
                            End If

                            If Flag_PPn <> General_Class.CekNULL(dr("flag_PPn")) Then
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show(Base_Language.Lang_Global_PPN & " " & Base_Language.Lang_Global_Tidak_Bisa_Berbeda & ". . ! ! (1)", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show(Base_Language.Lang_Global_Barang & " " & Base_Language.Lang_GLOBAL_Tidak_Ditemukan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    If Val(HilangkanTanda(jumlahConvert)) = 0 Then
                        Continue For
                    End If


                    SQL = "select no_faktur from emi_pembelian_po_detail where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and no_faktur = '" & TxtPO_NoFaktur.Text & "' "
                    SQL = SQL & "and kode_stock_owner = '" & lvPO_Lokasi & "' "
                    SQL = SQL & "and kode_barang = '" & lvPO_KdBarang & "' "
                    SQL = SQL & "and No_Penawaran = '" & lvPO_NoPenawaran & "' "
                    SQL = SQL & "and satuan = '" & lvPO_Satuan & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()

                            SQL = "update emi_pembelian_po_detail set "
                            SQL = SQL & "jumlah = jumlah + " & HilangkanTanda(jumlahConvert) & ", "
                            SQL = SQL & "nilai_barang = nilai_barang + " & lvPO_Jumlah_SB & ", "
                            SQL = SQL & "total = total + " & HilangkanTanda(lvPO_Total) & ", "
                            SQL = SQL & "Jumlah_Input = Jumlah_Input + " & HilangkanTanda(lvPO_Jumlah) & " "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and no_faktur = '" & TxtPO_NoFaktur.Text & "' "
                            SQL = SQL & "and kode_stock_owner = '" & lvPO_Lokasi & "' "
                            SQL = SQL & "and kode_barang = '" & lvPO_KdBarang & "' "
                            SQL = SQL & "and satuan = '" & lvPO_Satuan & "' "
                            ExecuteTrans(SQL)

                        Else
                            Dr.Close()

                            SQL = "insert into EMI_Pembelian_PO_Detail(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, "
                            SQL = SQL & "Kode_Barang, Jumlah, Satuan, Harga, Nilai_Barang, Satuan_Barang, Harga_Barang, "
                            SQL = SQL & "Total, No_Penawaran, Flag_Prepare, Jumlah_Input, Satuan_Input) values( "
                            SQL = SQL & "'" & KodePerusahaan & "', '" & TxtPO_NoFaktur.Text & "', '" & lvPO_Lokasi & "', "
                            SQL = SQL & "'" & lvPO_KdBarang & "', '" & HilangkanTanda(jumlahConvert) & "', '" & lvPO_Satuan & "', "
                            SQL = SQL & "'" & HilangkanTanda(lvPO_Harga) & "', '" & lvPO_Jumlah_SB & "', '" & lvPO_Satuan_SB & "', "
                            SQL = SQL & "'" & lvPO_Harga_SB & "', '" & HilangkanTanda(lvPO_Total) & "', "
                            SQL = SQL & "'" & lvPO_NoPenawaran & "','" & lvPO_ID & "', '" & HilangkanTanda(lvPO_Jumlah) & "', '" & LvPO_SatuanInput & "') "
                            ExecuteTrans(SQL)

                        End If
                    End Using

                    SQL = "insert into EMI_Pembelian_PO_Det(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, "
                    SQL = SQL & "Kode_Barang, Jumlah, Satuan, Harga, Nilai_Barang, Satuan_Barang, Harga_Barang, "
                    SQL = SQL & "Total, No_Penawaran, no_urut_pr, No_FakInduk, urut_det_induk, Jumlah_Input, Satuan_Input) values( "
                    SQL = SQL & "'" & KodePerusahaan & "', '" & TxtPO_NoFaktur.Text & "', '" & lvPO_Lokasi & "', "
                    SQL = SQL & "'" & lvPO_KdBarang & "', '" & HilangkanTanda(jumlahConvert) & "', '" & lvPO_Satuan & "', "
                    SQL = SQL & "'" & HilangkanTanda(lvPO_Harga) & "', '" & lvPO_Jumlah_SB & "', '" & lvPO_Satuan_SB & "', "
                    SQL = SQL & "'" & lvPO_Harga_SB & "', '" & HilangkanTanda(lvPO_Total) & "', "
                    SQL = SQL & "'" & lvPO_NoPenawaran & "','" & lvPO_PR & "', '" & LvPO_Fakinduk & "', '" & LvPO_UrutDet & "', "
                    SQL = SQL & "'" & HilangkanTanda(lvPO_Jumlah) & "', '" & LvPO_SatuanInput & "')"
                    ExecuteTrans(SQL)



                    '============================================
                    '=     CEK APAKAH BARANG SUDAH MEMENUHI     =
                    '============================================
                    SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.No_Urut as Urut_Det, "
                    SQL = SQL & "(b.jumlah - ISNULL(( SELECT SUM(x.Jumlah)  "
                    SQL = SQL & "FROM EMI_Pembelian_PO z, EMI_Pembelian_PO_Det x  "
                    SQL = SQL & "WHERE b.Kode_Perusahaan = z.Kode_Perusahaan and z.Kode_Perusahaan = x.Kode_Perusahaan  "
                    SQL = SQL & "AND b.No_Faktur = x.No_FakInduk and z.No_Faktur = x.No_Faktur  "
                    SQL = SQL & "AND b.Kode_Stock_Owner = x.Kode_Stock_Owner AND b.Kode_Barang = x.Kode_Barang and z.status is null "
                    SQL = SQL & "and x.urut_det_induk = b.No_Urut GROUP BY z.Kode_Perusahaan, x.Kode_Barang, x.Satuan_Barang "
                    SQL = SQL & "), 0)) AS Sisa "
                    SQL = SQL & "from EMI_Pembelian_PO_Induk a, EMI_Pembelian_PO_Det_Induk b "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                    SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and a.no_faktur = '" & LvPO_Fakinduk & "' "
                    SQL = SQL & "and b.No_Urut = '" & LvPO_UrutDet & "'"
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                If Val(HilangkanTanda(.Rows(0).Item("Sisa"))) = 0 Then

                                    SQL = "update EMI_Pembelian_PO_Det_Induk set Flag_Selesai = 'Y' "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & LvPO_Fakinduk & "' and No_Urut = '" & LvPO_UrutDet & "'"
                                    ExecuteTrans(SQL)

                                End If
                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show($"Data Barang {lvPO_KdBarang} Tidak ditemukan di PO", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End With
                    End Using


                    '===========================
                    '=     UPDATE PO INDUK     =
                    '===========================
                    SQL = "select Kode_Perusahaan from EMI_Pembelian_PO_Det_Induk where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & LvPO_Fakinduk & "' and Flag_Selesai is null"
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count = 0 Then
                                SQL = "update EMI_Pembelian_PO_Induk set Flag_Selesai_SubPO = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & LvPO_Fakinduk & "' "
                                ExecuteTrans(SQL)
                            End If
                        End With
                    End Using



                Next

                '=================================
                '=     HAPUS TABEL SEMENTARA     =
                '=================================
                SQL = "Delete Emi_Expedition_PO_Sementara where Kode_Perusahaan = '" & KodePerusahaan & "' and userid = '" & UserID & "'"
                ExecuteTrans(SQL)


                'If ChkPO_PPN.Checked = True Then
                '    If Flag_PPn <> "Y" Then
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show(Base_Language.Lang_Global_PPN & " " & Base_Language.Lang_Global_Tidak_Bisa_Berbeda & ". . ! ! (2)", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'Else
                '    If Flag_PPn <> "T" Then
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show(Base_Language.Lang_Global_PPN & " " & Base_Language.Lang_Global_Tidak_Bisa_Berbeda & ". . ! ! (3)", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If

                'End If

                '===================================================
                '===       CODINGAN UPDATE PO PEMBELIAN         ====
                '===================================================
                'Else
                '    Dim terbilang As String = General_Class.SayMUA(HilangkanTanda(TxtPO_GrandTotal.Text), CmbPO_MataUang.Text)

                '    'update po pembelian
                '    SQL = "update  EMI_Pembelian_PO set "
                '    SQL = SQL & "etd_simulasi = '" & Format(DtpPO_ETD.Value, "yyyy-MM-dd") & "',"
                '    SQL = SQL & "no_nota = '" & TxtPO_NoNota.Text.Trim & "',"
                '    SQL = SQL & "jenis_pembayaran = '" & arrPembayaran.Item(CmbPO_JnsBayar.SelectedIndex) & "', "
                '    SQL = SQL & "tgl_jatuh_tempo = '" & Format(DtpPO_TglBayar.Value, "yyyy-MM-dd") & "',"
                '    SQL = SQL & "ppn = '" & TxtPO_PersenPPN.Text.Trim & "', "
                '    SQL = SQL & "Total_MUA='" & HilangkanTanda(TxtPO_Total.Text) & "', "
                '    SQL = SQL & "Total_IDR='" & HilangkanTanda(TxtPO_Total.Text) & "', "
                '    SQL = SQL & "Grand_Sebelum_PPN='" & HilangkanTanda(TxtPO_TotalSblmPPN.Text) & "', "
                '    SQL = SQL & "Grand='" & HilangkanTanda(TxtPO_GrandTotal.Text) & "', "
                '    SQL = SQL & "Grand_Total_Terbilang='" & terbilang & "' "
                '    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                '    SQL = SQL & "and no_faktur = '" & TxtPO_NoFaktur.Text & "' "
                '    ExecuteTrans(SQL)



                '    'hapus dulu detail po pembelian lalu insert ulang
                '    SQL = "delete from emi_pembelian_po_detail where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & TxtPO_NoFaktur.Text & "' "
                '    ExecuteTrans(SQL)

                '    'delete det nya

                '    SQL = "delete from emi_pembelian_po_det where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & TxtPO_NoFaktur.Text & "' "
                '    ExecuteTrans(SQL)

                '    For i As Integer = 0 To LvPO_DataPO.Rows.Count - 1
                '        Get_Isi_Listview(i)


                '        'insert lagi ke detail
                '        SQL = "select no_faktur from emi_pembelian_po_detail where kode_perusahaan = '" & KodePerusahaan & "' "
                '        SQL = SQL & "and no_faktur = '" & TxtPO_NoFaktur.Text & "'  "
                '        SQL = SQL & "and kode_stock_owner = '" & lvPO_Lokasi & "' "
                '        SQL = SQL & "and kode_barang = '" & lvPO_KdBarang & "' "
                '        SQL = SQL & "and satuan = '" & lvPO_Satuan & "' "
                '        Using Dr = OpenTrans(SQL)
                '            If Dr.Read Then
                '                Dr.Close()
                '                SQL = "update emi_pembelian_po_detail set jumlah = jumlah + " & HilangkanTanda(lvPO_Jumlah) & ", "
                '                SQL = SQL & "nilai_barang = nilai_barang + " & lvPO_Jumlah_SB & ", "
                '                SQL = SQL & "total = total +   " & HilangkanTanda(lvPO_Total) & " "
                '                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                '                SQL = SQL & "and no_faktur = '" & TxtPO_NoFaktur.Text & "' "
                '                SQL = SQL & "and kode_stock_owner = '" & lvPO_Lokasi & "' "
                '                SQL = SQL & "and kode_barang = '" & lvPO_KdBarang & "' "
                '                SQL = SQL & "and satuan = '" & lvPO_Satuan & "' "
                '                ExecuteTrans(SQL)
                '            Else
                '                Dr.Close()
                '                SQL = "insert into EMI_Pembelian_PO_Detail(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, "
                '                SQL = SQL & "Kode_Barang, Jumlah, Satuan, Harga, Nilai_Barang, Satuan_Barang, Harga_Barang, "
                '                SQL = SQL & "Total, No_Penawaran, Flag_Prepare) values( "
                '                SQL = SQL & "'" & KodePerusahaan & "', '" & TxtPO_NoFaktur.Text & "', '" & lvPO_Lokasi & "', "
                '                SQL = SQL & "'" & lvPO_KdBarang & "', '" & HilangkanTanda(lvPO_Jumlah) & "', '" & lvPO_Satuan & "', "
                '                SQL = SQL & "'" & HilangkanTanda(lvPO_Harga) & "', '" & lvPO_Jumlah_SB & "', '" & lvPO_Satuan_SB & "', "
                '                SQL = SQL & "'" & lvPO_Harga_SB & "', '" & HilangkanTanda(lvPO_Total) & "', '" & lvPO_NoPenawaran & "','" & lvPO_ID & "') "
                '                ExecuteTrans(SQL)
                '            End If
                '        End Using

                '        'lalu insert ulang
                '        SQL = "insert into EMI_Pembelian_PO_Det(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, "
                '        SQL = SQL & "Kode_Barang, Jumlah, Satuan, Harga, Nilai_Barang, Satuan_Barang, Harga_Barang, "
                '        SQL = SQL & "Total, No_Penawaran, no_urut_pr) values( "
                '        SQL = SQL & "'" & KodePerusahaan & "', '" & TxtPO_NoFaktur.Text & "', '" & lvPO_Lokasi & "', "
                '        SQL = SQL & "'" & lvPO_KdBarang & "', '" & HilangkanTanda(lvPO_Jumlah) & "', '" & lvPO_Satuan & "', "
                '        SQL = SQL & "'" & HilangkanTanda(lvPO_Harga) & "', '" & lvPO_Jumlah_SB & "', '" & lvPO_Satuan_SB & "', "
                '        SQL = SQL & "'" & lvPO_Harga_SB & "', '" & HilangkanTanda(lvPO_Total) & "', '" & lvPO_NoPenawaran & "','" & lvPO_PR & "') "
                '        ExecuteTrans(SQL)




            End If

            '===============================
            '=     UPDATE FLAG RELEASE     =
            '===============================
            isError = True
            Button2_Click(Me, e)

            If isError Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Terjadi Kesalahan saat Release", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            isError = True


            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        CetakReport()

        kosong()

        Me.Close()
        EMI_PO_Pembelian_Display_Sub.BtnRefresh_Click(Button2, e)
    End Sub

    Private Sub BtnPO_Refresh_Click(sender As Object, e As EventArgs) Handles BtnPO_Refresh.Click
        kosong()
    End Sub

    Private Sub bersihkanpilihan()
        TxtPO_Diskon.Clear()
        TxtPO_KdBrg.Clear()
        TxtPO_NmBrg.Clear()
        CmbPO_Harga.Items.Clear()
        TxtPO_Jml.Clear()
        CmbPO_Satuan.SelectedIndex = -1
        RdoPO_Persen.Checked = False
        RdoPO_Rp.Checked = False

        TxtPO_KdBrg.Focus()
    End Sub

    Private Sub TxtPO_KdSupplier_TextChanged(sender As Object, e As EventArgs) Handles TxtPO_KdSupplier.TextChanged
        If TxtPO_KdSupplier.Text.Trim.Length = 0 Then
            LvSupplier2.Visible = False : Exit Sub
        Else
            LvSupplier2.Visible = True
        End If

        LvSupplier2.Items.Clear()
        Dim lv As New ListViewItem

        bersihsebagian()
        LvPO_DataPO.Rows.Clear()

        Try

            OpenConn()

            SQL = "Select b.kode_supplier, b.nama "
            SQL = SQL & "from suppliers b "
            SQL = SQL & "where  "
            SQL = SQL & "b.kode_perusahaan = '" & KodePerusahaan & "' and b.kode_supplier like '%" & TxtPO_KdSupplier.Text & "%' "

            'If CmbLokasi.SelectedIndex = 0 Then
            '    SQL = SQL & " and a.lokasi in("
            '    Dim list_kota As String = ""
            '    For x As Integer = 1 To CmbLokasi.Items.Count - 1
            '        list_kota = list_kota & "'" & CmbLokasi.Items(x).ToString & "', "
            '    Next

            '    list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

            '    SQL = SQL & list_kota & ")"
            'Else
            '    SQL = SQL & " and a.lokasi = '" & CmbLokasi.Text & "'"
            'End If

            SQL = SQL & "order by b.kode_supplier"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = LvSupplier2.Items.Add(Dr("kode_supplier"))
                    lv.SubItems.Add(Dr("nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CmbPO_JnsBayar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPO_JnsBayar.SelectedIndexChanged
        If CmbPO_JnsBayar.SelectedIndex = 0 Then
            DtpPO_TglBayar.Visible = False
            'CmbPO_RangeBayar.Visible = False
            CmbPO_CaraBayar.Enabled = True
            CmbPO_CaraBayar.SelectedIndex = 0
            cmbJenisPengiriman.Visible = False
            cmbJenisPengiriman.SelectedIndex = 0
            txtJatuhTempo.Visible = False
            txtJatuhTempo.Text = ""
        Else
            DtpPO_TglBayar.Visible = True
            'CmbPO_RangeBayar.Visible = True
            CmbPO_CaraBayar.Enabled = False
            CmbPO_CaraBayar.SelectedIndex = 0
            cmbJenisPengiriman.Visible = True
            cmbJenisPengiriman.SelectedIndex = 0
            txtJatuhTempo.Visible = True
            txtJatuhTempo.Text = ""

            If LvPO_DataPO.Rows.Count <> 0 Then
                'If LvPO_DataPO.Items(0).SubItems(14).Text <> "" Then
                '    cmbJenisPengiriman.Text = LvPO_DataPO.Items(0).SubItems(14).Text
                '    txtJatuhTempo.Text = LvPO_DataPO.Items(0).SubItems(15).Text
                'End If

            End If
        End If
    End Sub

    Private Sub Cmb_Ekspedisi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Ekspedisi.SelectedIndexChanged

        If Cmb_Ekspedisi.SelectedIndex = 0 Then
            Btn_Ekspedisi.Visible = False
            Lbl_BiayaEkspedisi.Visible = False
            Txt_BiayaEkspedisi.Visible = False

            Try
                OpenConn()

                SQL = "Delete Emi_Expedition_PO_Sementara where Kode_Perusahaan = '" & KodePerusahaan & "' and userid = '" & UserID & "'"
                ExecuteTrans(SQL)

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        ElseIf Cmb_Ekspedisi.SelectedIndex = 1 Then
            Btn_Ekspedisi.Visible = True
            Lbl_BiayaEkspedisi.Visible = True
            Txt_BiayaEkspedisi.Visible = True

            CekEkspedisiPO()
        End If

    End Sub



    Private Sub BtnPO_Ok_Click(sender As Object, e As EventArgs) Handles BtnPO_Ok.Click
        'If CmbPO_Harga.Text.Trim.Length = 0 Then
        '    MessageBox.Show(Base_Language.Lang_Global_Harga & " " & Base_Language.Lang_Global_Belum_Diisi & ". . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    CmbPO_Harga.Focus()
        '    Exit Sub
        'ElseIf TxtPO_Jml.Text.Trim.Length = 0 Then
        '    MessageBox.Show(Base_Language.Lang_Global_Jumlah & " " & Base_Language.Lang_Global_Belum_Diisi & ". . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    TxtPO_Jml.Focus()
        '    Exit Sub
        'ElseIf CmbPO_Satuan.SelectedIndex = -1 Then
        '    MessageBox.Show(Base_Language.Lang_Global_Satuan & " " & Base_Language.Lang_Global_Belum_Diisi & ". . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    CmbPO_Satuan.Focus()
        '    Exit Sub
        'End If

        'If CmbPO_Harga.SelectedIndex = -1 Then
        '    MessageBox.Show("Pilih Dahulu Harga . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    CmbPO_Harga.Focus()
        '    Exit Sub
        'ElseIf cmb_pr.SelectedIndex = -1 Then
        '    MessageBox.Show("Pilih Dahulu PR . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    cmb_pr.Focus()
        '    Exit Sub
        'End If

        'For i As Integer = 0 To LvPO_DataPO.Rows.Count - 1
        '    If LvPO_DataPO.Rows(i).Cells(cellPO_KdBarang).Value = TxtPO_KdBrg.Text Then
        '        If LvPO_DataPO.Rows(i).Cells(cellPO_Satuan).Value <> CmbPO_Satuan.Text Then
        '            MessageBox.Show(Base_Language.Lang_Global_Satuan & " " & Base_Language.Lang_Global_Tidak_Bisa_Berbeda & ". . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        End If

        '        If LvPO_DataPO.Rows(i).Cells(cellPO_ID).Value = "T" Then
        '            If LvPO_DataPO.Rows(i).Cells(cellPO_NoPenawaran).Value = arrNoPenawaran.Item(CmbPO_Harga.SelectedIndex) Then
        '                If LvPO_DataPO.Rows(i).Cells(cellPO_PR).Value = arrNoUrutPr.Item(cmb_pr.SelectedIndex) Then
        '                    MessageBox.Show(Base_Language.Lang_Global_Data_Sdh_Ada & ". . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                    Exit Sub
        '                End If
        '            End If
        '        End If
        '    End If
        'Next

        'Try
        '    OpenConn()
        '    Dim harga_satuan_besar As Double = 0
        '    SQL = "Select dbo.Ubah_Satuan('" & KodePerusahaan & "','UANG','" & TxtPO_KdBrg.Text & "',"
        '    SQL = SQL & "'" & arrSatuanPenawaran.Item(CmbPO_Harga.SelectedIndex) & "','" & CmbPO_Satuan.Text & "',"
        '    SQL = SQL & "" & arrHargaPenawaran.Item(CmbPO_Harga.SelectedIndex) & ") as Hasil "
        '    Using dr = OpenTrans(SQL)
        '        If dr.Read Then

        '            If General_Class.CekNULL(dr("Hasil")) <> "" Then
        '                If dr("Hasil") = 0 Then
        '                    MessageBox.Show("Satuan " & arrSatuanPenawaran.Item(CmbPO_Harga.SelectedIndex) & " Ke " & CmbPO_Satuan.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                    Exit Sub
        '                Else
        '                    harga_satuan_besar = dr("hasil")
        '                End If
        '            Else
        '                MessageBox.Show("Satuan " & arrSatuanPenawaran.Item(CmbPO_Harga.SelectedIndex) & " Ke " & CmbPO_Satuan.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Exit Sub
        '            End If
        '        End If
        '    End Using

        '    Dim Jumlah_satuan_Kecil As Double = 0
        '    SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & TxtPO_KdBrg.Text & "',"
        '    SQL = SQL & "'" & CmbPO_Satuan.Text & "','" & TxtPO_SatuanBarang.Text & "',"
        '    SQL = SQL & "" & HilangkanTanda(TxtPO_Jml.Text) & ") as Hasil "
        '    Using dr = OpenTrans(SQL)
        '        If dr.Read Then

        '            If General_Class.CekNULL(dr("Hasil")) <> "" Then
        '                If dr("Hasil") = 0 Then
        '                    MessageBox.Show("Satuan " & CmbPO_Satuan.Text & " Ke " & TxtPO_SatuanBarang.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                    Exit Sub
        '                Else
        '                    Jumlah_satuan_Kecil = dr("hasil")
        '                End If
        '            Else
        '                MessageBox.Show("Satuan " & CmbPO_Satuan.Text & " Ke " & TxtPO_SatuanBarang.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Exit Sub
        '            End If
        '        End If
        '    End Using

        '    Dim jumlah_sisa_satuan_kecil As Double = 0
        '    SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & TxtPO_KdBrg.Text & "',"
        '    SQL = SQL & "'" & txtSatuanSisa.Text & "','" & TxtPO_SatuanBarang.Text & "',"
        '    SQL = SQL & "" & HilangkanTanda(txtSisaPr.Text) & ") as Hasil "
        '    Using dr = OpenTrans(SQL)
        '        If dr.Read Then

        '            If General_Class.CekNULL(dr("Hasil")) <> "" Then
        '                If dr("Hasil") = 0 Then
        '                    MessageBox.Show("Satuan " & CmbPO_Satuan.Text & " Ke " & TxtPO_SatuanBarang.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                    Exit Sub
        '                Else
        '                    jumlah_sisa_satuan_kecil = dr("hasil")
        '                End If
        '            Else
        '                MessageBox.Show("Satuan " & CmbPO_Satuan.Text & " Ke " & TxtPO_SatuanBarang.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Exit Sub
        '            End If
        '        End If
        '    End Using

        '    Dim lokasi_gudang_bahan As String = ""

        '    SQL = "select a.Kode_Stock_Owner_Gudang From Binding_Lokasi_Gudang a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
        '    SQL = SQL & "a.Kode_Stock_Owner = '" & CmbPO_Lokasi.Text & "' and a.Gudang_Default = 'Y'"
        '    Using dr = OpenTrans(SQL)
        '        If dr.Read Then
        '            lokasi_gudang_bahan = dr("Kode_Stock_Owner_Gudang")
        '        Else
        '            dr.Close()
        '            CloseConn()
        '            MessageBox.Show(Base_Language.Lang_Global_LokasiGudang & " " & Base_Language.Lang_GLOBAL_Tidak_Ditemukan & ". . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        End If
        '    End Using

        '    'SQL = "select top(1) c.lokasi_gudang from EMI_Kategori_Gudang a, barang b, EMI_Kategori_Gudang_PerLokasi c  "
        '    'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Gudang = b.Id_Kategori_Gudang "
        '    'SQL = SQL & "and  a.Id_Kategori_Gudang = c.ID_Kategori_Gudang and a.Kode_Perusahaan = c.Kode_Perusahaan "
        '    'SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and c.kode_stock_owner = '" & CmbPO_Lokasi.Text & "' "
        '    'SQL = SQL & "and b.kode_barang = '" & TxtPO_KdBrg.Text & "' "
        '    'Using dr = OpenTrans(SQL)
        '    '    If dr.Read Then
        '    '        lokasi_gudang_bahan = dr("lokasi_gudang")
        '    '    Else
        '    '        dr.Close()
        '    '        CloseConn()
        '    '        MessageBox.Show(Base_Language.Lang_Global_LokasiGudang & " " & Base_Language.Lang_GLOBAL_Tidak_Ditemukan & ". . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    '        Exit Sub
        '    '    End If
        '    'End Using

        '    If jumlah_sisa_satuan_kecil < Jumlah_satuan_Kecil Then
        '        MessageBox.Show("Jumlah po tidak boleh lebih besar dari jumlah PR!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Exit Sub
        '    End If

        '    Dim flagBolehLewat As Boolean = True

        '    SQL = "select "
        '    SQL = SQL & "a.tanggal_delivery,DATEDIFF(DAY, a.Tanggal_Delivery , DATEADD(day,b.Waktu_Pabrikasi + b.Waktu_Pengiriman,'" & Format(tgl_skg, "yyyy-MM-dd") & "' ) ) as  Waktu_Proses_Pengiriman,"
        '    SQL = SQL & "DATEADD(day,Waktu_Pabrikasi + Waktu_Pengiriman, '" & Format(tgl_skg, "yyyy-MM-dd") & "') as tanggal_actual_delivery "
        '    SQL = SQL & "from EMI_Purchase_Requisition_Detail a, emi_detail_proses_pengiriman_po b, Suppliers c "
        '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang  "
        '    SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Kategori_Supplier = c.ID_Kategori_Suppliers "
        '    SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and c.Kode_Supplier = '" & TxtPO_KdSupplier.Text & "' "
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then

        '            If Dr("waktu_proses_pengiriman") > 0 Then
        '                Dim tanya As String = MessageBox.Show("Terdapat data yang melewati estimasi delivery  " & vbNewLine & vbNewLine & TxtPO_NmBrg.Text.Trim & vbNewLine & "- Tanggal Estimasi Delivery : " & Format(Dr("tanggal_delivery"), "dd MMM yyyy") & vbNewLine & "- Tanggal Actual Delivery : " & Format(Dr("tanggal_actual_delivery"), "dd MMM yyyy") & vbNewLine & vbNewLine & "Apakah ingin melanjutkan transaksi ? ", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        '                If tanya = vbNo Then
        '                    CloseConn()
        '                    MessageBox.Show("Transaksi dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                    Exit Sub
        '                End If

        '            End If

        '        End If
        '    End Using



        '    Dim LvPO_DataPO As ListViewItem
        '    LvPO_DataPO = LvPO_DataPO.Rows.Add(lokasi_gudang_bahan)
        '    LvPO_DataPO.SubItems.Add(TxtPO_KdBrg.Text)
        '    LvPO_DataPO.SubItems.Add(TxtPO_NmBrg.Text)
        '    LvPO_DataPO.SubItems.Add(Format(Val(harga_satuan_besar), "N2"))
        '    LvPO_DataPO.SubItems.Add(Format(Val(TxtPO_Jml.Text), "N2"))
        '    LvPO_DataPO.SubItems.Add(CmbPO_Satuan.Text)
        '    LvPO_DataPO.SubItems.Add(arrHargaPenawaran.Item(CmbPO_Harga.SelectedIndex))
        '    LvPO_DataPO.SubItems.Add(Jumlah_satuan_Kecil)
        '    LvPO_DataPO.SubItems.Add(TxtPO_SatuanBarang.Text)
        '    'LvPO_DataPO.SubItems.Add(arrNoPenawaran.Item(CmbPO_Harga.SelectedIndex))
        '    LvPO_DataPO.SubItems.Add(arrFakPenawaran.Item(CmbPO_Harga.SelectedIndex))


        '    LvPO_DataPO.SubItems.Add("T")

        '    LvPO_DataPO.SubItems.Add(Format(Jumlah_satuan_Kecil * Val(arrHargaPenawaran.Item(CmbPO_Harga.SelectedIndex)), "N2"))
        '    LvPO_DataPO.SubItems.Add("")
        '    LvPO_DataPO.SubItems.Add(arrNoUrutPr.Item(cmb_pr.SelectedIndex))

        '    LvPO_DataPO.SubItems.Add(arrTempoPenawaran.Item(CmbPO_Harga.SelectedIndex))
        '    LvPO_DataPO.SubItems.Add(arrJatuhTempo.Item(CmbPO_Harga.SelectedIndex))
        '    CmbPO_MataUang.Enabled = False


        '    '==========================
        '    '=     SET PEMBAYARAN     =
        '    '==========================
        '    SQL = "select top 1 a.Kode_Supplier, b.Kode_Barang, c.Jenis_Pembayaran, c.Tempo_Pembayaran, c.Lama_Pembayaran "
        '    SQL = SQL & "from EMI_Master_Penawaran a, EMI_Master_Penawaran_Detail b, EMI_Master_Penawaran_Jatuh_Tempo c "
        '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
        '    SQL = SQL & "and a.No_Faktur = b.No_Faktur "
        '    SQL = SQL & "and a.No_Faktur = c.No_Faktur "
        '    SQL = SQL & "and a.Selesai is null and flag_release = 'Y' "
        '    SQL = SQL & "and Status is null "
        '    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and a.Kode_Supplier = '" & TxtPO_KdSupplier.Text & "' "
        '    SQL = SQL & "and b.Kode_Barang = '" & TxtPO_KdBrg.Text & "' "
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then

        '            If Dr("Jenis_Pembayaran") = "N" Then

        '                CmbPO_JnsBayar.SelectedIndex = 1

        '                If Not General_Class.CekNULL(Dr("Tempo_Pembayaran")) = "" Then

        '                    cmbJenisPengiriman.SelectedItem = Dr("Tempo_Pembayaran")
        '                    txtJatuhTempo.Text = Dr("Lama_Pembayaran")

        '                End If

        '            Else
        '                CmbPO_JnsBayar.SelectedIndex = 0
        '            End If


        '        End If
        '    End Using



        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        'bersihsebagian()

        'HitungGrandTotal()
    End Sub

    Private Sub LvPO_DataPO_DoubleClick(sender As Object, e As EventArgs)
        If LvPO_DataPO.Rows.Count = 0 Then
            MessageBox.Show("Pilih dahulu Data barang yang mau dihapus!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Get_Isi_Listview(LvPO_DataPO.CurrentRow.Index)

        If lvPO_ID = "Y" Or lvPO_ID = "X" Then
            MessageBox.Show("Data Tidak Bisa dihapus . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If canDeleteLv = True Then
            LvPO_DataPO.Rows.Remove(LvPO_DataPO.CurrentRow)


            If LvPO_DataPO.Rows.Count = 0 Then
                CmbPO_MataUang.Enabled = True
            End If

        End If




        HitungGrandTotal()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnRelease.Click
        EMI_PO_Pembelian_Display.asal = Jenis
        EMI_PO_Pembelian_Display.filter_tambahan = " And a.Flag_Sudah_PO Is null "
        EMI_PO_Pembelian_Display.ShowDialog()
    End Sub

    'Private Sub CmbPO_RangeBayar_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Dim xx As Integer = 0
    '    If CmbPO_RangeBayar.SelectedIndex = -1 Then
    '        xx = 0
    '    Else
    '        xx = Val(CmbPO_RangeBayar.Text)
    '    End If
    '    DtpPO_TglBayar.Value = DateAdd(DateInterval.Day, xx, DtpPO_Tgl.Value)
    'End Sub

    Private Sub CmbPO_JnsEkspedisi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPO_JnsEkspedisi.SelectedIndexChanged
        If CmbPO_JnsEkspedisi.SelectedIndex = -1 Then
            Exit Sub
        End If

        If arrEkspedisi.Item(CmbPO_JnsEkspedisi.SelectedIndex) = "1" Then
            TxtPO_Biaya.Enabled = False
        Else
            TxtPO_Biaya.Enabled = True
        End If
    End Sub

    Private Sub cmb_pr_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_pr.SelectedIndexChanged
        If cmb_pr.SelectedIndex = -1 Then
            MessageBox.Show("Silahkan pilih kode barang terlebih dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()
            txtSatuanSisa.Text = ""
            txtSisaPr.Text = ""
            SQL = "Select a.Jumlah - isnull((Select sum(y.Jumlah) from EMI_Pembelian_PO x, EMI_Pembelian_PO_Det y   "
            SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur "
            SQL = SQL & "And y.Kode_Perusahaan = a.Kode_Perusahaan And y.no_urut_pr = a.No_Urut And x.status Is null "
            SQL = SQL & "), 0) As sisa, a.satuan "
            SQL = SQL & "from  EMI_Purchase_Requisition_Detail a, EMI_Purchase_Requisition b "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Perusahaan= b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and b.status is null and b.Flag_Release = 'Y' "
            SQL = SQL & "and a.no_urut = '" & arrNoUrutPr.Item(cmb_pr.SelectedIndex) & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    txtSisaPr.Text = Format(dr("sisa"), "N0")
                    txtSatuanSisa.Text = dr("satuan")
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Terjadi kesalahan, Purchase Requisition tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Public Sub disableSebagian()
        BtnPO_Simpan.Visible = True

        DtpPO_ETD.Enabled = True
        LvPO_DataPO.Enabled = True
        CmbPO_MataUang.Enabled = True

        TxtPO_KdSupplier.Enabled = False
        TxtPO_NmSupplier.Enabled = False
        CmbPO_JnsBayar.Enabled = True
        cmbJenisPengiriman.Enabled = True
        txtJatuhTempo.Enabled = True
        Button1.Enabled = True
        TxtPO_NoNota.Enabled = True



        DtpPO_Tgl.Enabled = True
        DtpPO_TglBayar.Enabled = False
        TxtPO_NoPO.Enabled = False
        'CmbPO_RangeBayar.Enabled = False
        CmbPO_JnsEkspedisi.Enabled = False
        CmbPO_CaraBayar.Enabled = False
        ChkPO_PPN.Enabled = False

        TxtPO_KdBrg.Enabled = False
        CmbPO_Harga.Enabled = False
        cmb_pr.Enabled = False
        TxtPO_Jml.Enabled = False
        CmbPO_Satuan.Enabled = False

        canDeleteLv = False

        Button2.Visible = False
        Button2.Enabled = False


        BtnPO_Clear.Enabled = False
        BtnPO_Ok.Enabled = False


        TxtPO_Biaya.Enabled = False


    End Sub

    Private Sub enableSebagian()

        TxtPO_NoNota.Enabled = True
        DtpPO_ETD.Enabled = True
        DtpPO_Tgl.Enabled = False
        DtpPO_TglBayar.Enabled = True
        TxtPO_NoPO.Enabled = False
        TxtPO_KdSupplier.Enabled = True
        TxtPO_NmSupplier.Enabled = True
        CmbPO_JnsBayar.Enabled = True
        cmbJenisPengiriman.Enabled = True
        txtJatuhTempo.Enabled = True
        ''CmbPO_RangeBayar.Enabled = True
        CmbPO_JnsEkspedisi.Enabled = True
        'CmbPO_CaraBayar.Enabled = True
        ChkPO_PPN.Enabled = True

        TxtPO_KdBrg.Enabled = True
        CmbPO_Harga.Enabled = True
        cmb_pr.Enabled = True
        TxtPO_Jml.Enabled = True
        CmbPO_Satuan.Enabled = True

        LvPO_DataPO.Enabled = True
        canDeleteLv = True
        BtnPO_Simpan.Visible = True
        Button2.Visible = True
        Button2.Enabled = True

        Button1.Enabled = True
        BtnPO_Clear.Enabled = True
        BtnPO_Ok.Enabled = True
        BtnPO_Simpan.Enabled = True

        TxtPO_Biaya.Enabled = True

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        SQL = "select status,selesai,flag_release from EMI_Pembelian_PO "
        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
        SQL = SQL & "and no_faktur = '" & TxtPO_NoFaktur.Text & "' "
        Using dr = OpenTrans(SQL)
            If dr.Read Then
                If General_Class.CekNULL(dr("status")) = "Y" Then
                    dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_Global_DataSudahBatal, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf General_Class.CekNULL(dr("selesai")) = "Y" Then
                    dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Transaksi tidak bisa dilanjutkan, karena PO sudah selesai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf General_Class.CekNULL(dr("flag_release")) = "Y" Then
                    dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Faktur PO Pembelian ini sudah pernah direlease!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Else
                dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Data Pembelian PO tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End Using

        SQL = "update EMI_Pembelian_PO set flag_release = 'Y', "
        SQL = SQL & "tanggal_release = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
        SQL = SQL & "jam_release = '" & Format(tgl_skg, "HH:mm:ss") & "', "
        SQL = SQL & "user_release = '" & UserID & "'"
        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & TxtPO_NoFaktur.Text & "'"
        ExecuteTrans(SQL)

        Dim flag_kategori_Supplier As String = ""
        '========== CEK JENIS NYA IMPORT ATAU LOKAL ===========================================
        SQL = "select b.Flag_Jenis_Import From Suppliers a, Suppliers_Kategori b "
        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and  "
        SQL = SQL & "a.ID_Kategori_Suppliers = b.ID_Kategori_Suppliers "
        SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and Kode_Supplier	 = '" & TxtPO_KdSupplier.Text.Trim & "' "
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                flag_kategori_Supplier = General_Class.CekNULL(Dr("flag_jenis_import"))
            Else
                Dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show(Base_Language.Lang_GLOBAL_Kategori_Supplier & " " & Base_Language.Lang_GLOBAL_Tidak_Ditemukan & ". . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End Using



        If flag_kategori_Supplier = "Y" Then


            Dim idRencana_Order As String = ""

            Dim kode_kontainer As String = ""
            Dim Qty_Kontainer As Integer = 0

            For indexxxx = 0 To LvPO_DataPO.Rows.Count - 1
                Get_Isi_Listview(indexxxx)


                '====== CEK DATA KONTAINER, PO SUDAH HARUS ADA KONTAINER DULU
                SQL = "select kode_barang,kode_kontainer,qty from barang_per_kontainer "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and kode_barang = '" & lvPO_KdBarang & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        kode_kontainer = Dr("kode_kontainer")
                        Qty_Kontainer = Val(Dr("qty"))
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_GLOBAL_Data_Kontainer & " " & Base_Language.Lang_GLOBAL_Tidak_Ditemukan & ". . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                SQL = "select id_rencana from rencana_order where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & " '"
                SQL = SQL & "and kode_supplier = '" & TxtPO_KdSupplier.Text.Trim & "' "
                SQL = SQL & "and no_po ='" & TxtPO_NoFaktur.Text & "' "
                SQL = SQL & "and kode_kontainer = '" & kode_kontainer & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        idRencana_Order = Dr("id_rencana")
                    Else
                        Dr.Close()
                        'insert rencana order baru

                        SQL = "insert into rencana_order(kode_perusahaan,periode,kode_supplier,lokasi,kode_kontainer,no_prepare_bahan_baku,no_po,tanggal_po,tanggal_input,jam_input,userid,keterangan,kolom)values("
                        SQL = SQL & "'" & KodePerusahaan & "', '" & Format(tgl_skg, "MMyyyy") & "','" & TxtPO_KdSupplier.Text.Trim & "', '" & CmbPO_Lokasi.Text & "', "
                        SQL = SQL & "'" & kode_kontainer & "', '-','" & TxtPO_NoFaktur.Text & "','" & Format(tgl_skg, "yyyy-MM-dd") & "' ,'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "', '','1' )"
                        ExecuteTrans(SQL)

                        SQL = "select IDENT_CURRENT('rencana_order') as urut"
                        Using Dr1 = OpenTrans(SQL)
                            If Dr1.Read Then
                                idRencana_Order = Dr1("urut")
                            End If
                        End Using

                    End If

                End Using

                Dim satuan_kirim As String = ""
                Dim nilai_kirim As Double = 0
                SQL = "select satuan from barang_detail_satuan where kode_barang='" & lvPO_KdBarang & "' "
                SQL = SQL & "and kode_Perusahaan='" & KodePerusahaan & "' and flag_kirim='Y' "
                Using dr3 = OpenTrans(SQL)
                    If dr3.Read Then
                        satuan_kirim = dr3("satuan")
                    Else
                        dr3.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("data satuan kirim tidak ada ")
                        Exit Sub
                    End If
                End Using

                SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & lvPO_KdBarang & "', '" & lvPO_Satuan_SB & "',"
                SQL = SQL & "'" & satuan_kirim & "', '" & HilangkanTanda(lvPO_Jumlah_SB) & "' ) as hasil"
                Using Dr1 = OpenTrans(SQL)
                    If Dr1.Read Then
                        If General_Class.CekNULL(Dr1("hasil")) = "" Then
                            Dr1.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("data konversi satuan kirim tidak ada ")
                            Exit Sub
                        End If

                        nilai_kirim = Dr1("hasil")
                    Else
                        Dr1.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("data konversi satuan kirim tidak ada ")
                        Exit Sub
                    End If
                End Using

                '===== INSERT DATA KE DETAIL RENCANA ORDER ====
                SQL = "select kode_perusahaan from detail_rencana_order  "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and id_rencana = '" & idRencana_Order & "' "
                SQL = SQL & "and kode_stock_owner='" & lvPO_Lokasi & "' and kode_barang='" & lvPO_KdBarang & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()

                        SQL = "update detail_rencana_order set "
                        SQL = SQL & "jumlah_po = jumlah_PO+" & nilai_kirim & " , "
                        SQL = SQL & "jumlah_minimal = jumlah_minimal+" & nilai_kirim & "  "
                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and id_rencana = '" & idRencana_Order & "' "
                        SQL = SQL & "and kode_stock_owner='" & lvPO_Lokasi & "' and kode_barang='" & lvPO_KdBarang & "' "
                        ExecuteTrans(SQL)

                    Else
                        Dr.Close()
                        'insert detail rencana order baru

                        SQL = "insert into detail_rencana_order(id_rencana, kode_Perusahaan,kode_Barang,jumlah_po, "
                        SQL = SQL & "isi_satuan_besar, jumlah_minimal, kode_stock_owner, jumlah_per_konte)"
                        SQL = SQL & "values( "
                        SQL = SQL & "'" & idRencana_Order & "', '" & KodePerusahaan & "','" & lvPO_KdBarang & "', "
                        SQL = SQL & "'" & nilai_kirim & "', 1, '" & nilai_kirim & "', '" & lvPO_Lokasi & "', "
                        SQL = SQL & "'" & Qty_Kontainer & "') "
                        ExecuteTrans(SQL)

                    End If

                End Using

                SQL = "select top(1) a.kode_Perusahaan from rencana_order a, detail_rencana_order b where "
                SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.ID_Rencana=b.ID_Rencana and b.kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "a.Kode_Supplier='" & TxtPO_KdSupplier.Text.Trim & "' and b.Kode_barang='" & lvPO_KdBarang & "' "
                SQL = SQL & "and b.id_rencana <> '" & idRencana_Order & "' "
                Using drr = OpenTrans(SQL)
                    If Not drr.Read Then
                        drr.Close()

                        SQL = "insert into detail_rencana_order(id_rencana, kode_Perusahaan,kode_Barang,jumlah_po, isi_satuan_besar, "
                        SQL = SQL & "jumlah_minimal, kode_stock_owner, jumlah_per_konte) "

                        SQL = SQL & "select c.id_rencana, '" & KodePerusahaan & "', '" & lvPO_KdBarang & "', "
                        SQL = SQL & "0, 1, 0, '" & lvPO_Lokasi & "', "
                        SQL = SQL & "'" & Qty_Kontainer & "' "

                        SQL = SQL & "from rencana_order c "
                        SQL = SQL & "where c.kode_supplier = '" & TxtPO_KdSupplier.Text.Trim & "' and "
                        SQL = SQL & "c.id_rencana <> '" & idRencana_Order & "' "
                        ExecuteTrans(SQL)

                    End If
                End Using
            Next

            Dim totalJumlahPo As Integer = 0
            Dim totalJumlahTotalPersen As Integer = 0

            SQL = "select sum((jumlah_po / jumlah_per_konte) * 100 ) as total_persen, "
            SQL = SQL & "sum(Jumlah_PO) as total_jml from Detail_Rencana_order where "
            SQL = SQL & "kode_perusahaan= '" & KodePerusahaan & "' and id_rencana = '" & idRencana_Order & "' "
            Using Dr4 = OpenTrans(SQL)
                If Dr4.Read Then
                    totalJumlahPo = Val(HilangkanTanda(Format(Dr4("total_jml"), "N0")))
                    totalJumlahTotalPersen = Val(HilangkanTanda(Format(Dr4("total_persen"))))
                Else
                    Dr4.Close()
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_GLOBAL_Id_Rencana & " " & Base_Language.Lang_GLOBAL_Tidak_Ditemukan & ". . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "update rencana_order set total_jml = " & totalJumlahPo & " , "
            SQL = SQL & "total_persen = " & totalJumlahTotalPersen & " where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and id_rencana = '" & idRencana_Order & "' "
            ExecuteTrans(SQL)


            '======================== INSERT SUBMIT PO IMPORT =====================================================

            'cek faktur
            SQL = "select inisial_faktur from stock_owner "
            SQL = SQL & "where Kode_stock_Owner = '" & CmbPO_Lokasi.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    arrInisialFakturSubmitPO = dr("inisial_faktur")
                Else
                    dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Inisial Faktur Tidak ditemukan")
                    Exit Sub
                End If
            End Using

            get_no_faktur_submit_Po()

            Dim Mata_Uang_Declare As String = ""
            Dim ind As Integer = 0
            SQL = "Select Mata_Uang_Rek, Mata_Uang_Declare From suppliers where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and Kode_Supplier = '" & TxtPO_KdSupplier.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then

                    Mata_Uang_Declare = dr("Mata_Uang_Declare")
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Mata Uang Declare/Rekening Tidak ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End Using

            SQL = "insert into submit_PO(kode_perusahaan, no_faktur, Id_rencana, tanggal, jam, UserID, Jenis_Transaksi, "
            SQL = SQL & "Mata_Uang, No_Rekening, Kode_Supplier, Kurs, Grand_total ) "
            SQL = SQL & "values('" & KodePerusahaan & "','" & fakturSubmitPO & "','" & idRencana_Order & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "',"
            SQL = SQL & "'" & UserID & "', '" & arrPembayaran.Item(CmbPO_JnsBayar.SelectedIndex) & "', '" & Mata_Uang_Declare & "', "
            SQL = SQL & "'" & TxtPO_Kurs.Text & "', '" & TxtPO_KdSupplier.Text & "', null, null)"
            ExecuteTrans(SQL)

            SQL = "select cast(c.rv as int) as rvx, a.Kode_Stock_Owner, b.kode_barang, b.Nama, Jumlah_PO, b.Harga_Declare, (b.Harga_Declare*Jumlah_PO) as total, (b.Panjang*b.Lebar*b.Tinggi) as volume,  "
            SQL = SQL & "(Jumlah_PO/a.isi_satuan_besar) as tot_sat_bsr,a.isi_satuan_besar, Berat, berat_kotor,b.isi_satuan_besar as isi_satuan_besar_invoice,  "
            SQL = SQL & "(Berat*Jumlah_PO) as tot_berat_brsh, (Berat_kotor*Jumlah_PO) as tot_berat_kotor, panjang, lebar, tinggi, a.no_urut, b.mata_uang, b.Harga_Declare_Satuan_Besar, b.nama_declare  "
            SQL = SQL & "from detail_rencana_order a, barang b, rencana_order c where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and "
            SQL = SQL & "b.kode_perusahaan = c.kode_perusahaan and "
            SQL = SQL & "a.kode_stock_owner = b.kode_stock_owner and "
            SQL = SQL & "a.kode_barang = b.kode_Barang and "
            SQL = SQL & "a.id_rencana = c.id_rencana and "
            SQL = SQL & "Jumlah_PO <> 0 and "
            SQL = SQL & "a.id_rencana = '" & idRencana_Order & "' "
            SQL = SQL & "order by b.nama"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")

                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            If IsDBNull(.Rows(i).Item("nama_declare")) Or IsDBNull(.Rows(i).Item("harga_declare")) Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Nama Declare Belum di isi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            End If

                            SQL = "insert into detail_submit_PO (kode_perusahaan, No_faktur, Kode_Stock_Owner, Kode_Barang, Jumlah, "
                            SQL = SQL & "Harga_Declare, Total, Volume, Jml_Satuan_Besar, Isi_Satuan_Besar, Berat_Bersih, Berat_Kotor, Total_Berat_Bersih, "
                            SQL = SQL & " Total_Berat_kotor, Panjang, Lebar, Tinggi, Urut_Rencana, Mata_Uang, Harga_Declare_Satuan_Besar,isi_satuan_besar_invoice, Barang_Free) values( "
                            SQL = SQL & "'" & KodePerusahaan & "', '" & fakturSubmitPO & "', "
                            SQL = SQL & "'" & .Rows(i).Item("kode_stock_owner") & "', '" & .Rows(i).Item("kode_barang") & "', "
                            SQL = SQL & "'" & .Rows(i).Item("jumlah_po") & "', '" & .Rows(i).Item("harga_declare") & "', "
                            SQL = SQL & "'" & .Rows(i).Item("total") & "', '" & .Rows(i).Item("volume") & "', "
                            SQL = SQL & "'" & .Rows(i).Item("tot_sat_bsr") & "', '" & .Rows(i).Item("isi_satuan_besar") & "', '" & .Rows(i).Item("Berat") & "', '" & .Rows(i).Item("berat_kotor") & "', "
                            SQL = SQL & "'" & .Rows(i).Item("tot_berat_brsh") & "', '" & .Rows(i).Item("tot_berat_kotor") & "', '" & .Rows(i).Item("panjang") & "', '" & .Rows(i).Item("lebar") & "', '" & .Rows(i).Item("tinggi") & "', "
                            SQL = SQL & "'" & .Rows(i).Item("no_urut") & "', '" & .Rows(i).Item("mata_uang") & "', '" & .Rows(i).Item("Harga_Declare_Satuan_Besar") & "', '" & .Rows(i).Item("isi_satuan_besar_invoice") & "', '0')"
                            ExecuteTrans(SQL)
                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi kesalahan!..", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                End With
            End Using

            SQL = "update rencana_Order set Flag_Submit_PO = 'Y' "
            SQL = SQL & " where Id_Rencana = '" & idRencana_Order & "'"
            ExecuteTrans(SQL)

            ' cek
            SQL = Simpan_Status_Rencana_Order(idRencana_Order, "SUBMIT PO", fakturSubmitPO)
            ExecuteTrans(SQL)
        End If


        isError = False

    End Sub

    Private Sub CetakReport()
        '==========================
        '=      CETAK REPORT      =
        '==========================
        Try
            OpenConn()

            'SQL = "select a.No_Faktur from EMI_Pembelian_PO a, EMI_Pembelian_PO_Detail b, barang c, suppliers d "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            'SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            'SQL = SQL & "and a.Kode_Supplier = d.Kode_Supplier "
            'SQL = SQL & "and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Faktur='" & no_Faktur_Sementara & "' "

            SQL = "select Kode_Perusahaan, No_Faktur from View_Laporan_PO2 "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & no_Faktur_Sementara & "' "
            'SQL = SQL & "and No_Faktur = '" & TxtPO_NoFaktur.Text & "' "


            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    With Ds.Tables(0)
                        Dim CrDoc As New Faktur_Purchase_Order2
                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Laporan Faktur Purchase Order"
                            CrDoc.RecordSelectionFormula = " {View_Laporan_PO2.Kode_Perusahaan} = '" & KodePerusahaan & "' and {View_Laporan_PO2.No_Faktur} = '" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "'"

                            .Text = "Laporan Faktur Purchase Order"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                            .Refresh()
                            .Show()
                        End With
                    End With
                Else
                    MessageBox.Show("Data tidak ditemukan!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub TxtPO_NmSupplier_TextChanged(sender As Object, e As EventArgs) Handles TxtPO_NmSupplier.TextChanged
        If TxtPO_NmSupplier.Text.Trim.Length = 0 Then
            LvSupplier2.Visible = False : Exit Sub
        Else
            LvSupplier2.Visible = True
        End If

        LvSupplier2.Items.Clear()
        Dim lv As New ListViewItem

        Try

            OpenConn()

            SQL = "Select b.kode_supplier, b.nama "
            SQL = SQL & "from suppliers b "
            SQL = SQL & "where  "
            SQL = SQL & "b.kode_perusahaan = '" & KodePerusahaan & "' and b.nama like '%" & TxtPO_NmSupplier.Text & "%' "

            'If CmbLokasi.SelectedIndex = 0 Then
            '    SQL = SQL & " and a.lokasi in("
            '    Dim list_kota As String = ""
            '    For x As Integer = 1 To CmbLokasi.Items.Count - 1
            '        list_kota = list_kota & "'" & CmbLokasi.Items(x).ToString & "', "
            '    Next

            '    list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

            '    SQL = SQL & list_kota & ")"
            'Else
            '    SQL = SQL & " and a.lokasi = '" & CmbLokasi.Text & "'"
            'End If

            SQL = SQL & "order by b.kode_supplier"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = LvSupplier2.Items.Add(Dr("kode_supplier"))
                    lv.SubItems.Add(Dr("nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtPO_NmSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPO_NmSupplier.KeyPress
        If e.KeyChar = Chr(13) Then CmbPO_MataUang.Focus()
    End Sub

    Private Sub LvSupplier_DoubleClick(sender As Object, e As EventArgs) Handles LvSupplier.DoubleClick

        If LvSupplier.Items.Count = 0 Then Exit Sub

        Dim Kode As String = LvSupplier.FocusedItem.Text
        Dim Nama As String = LvSupplier.FocusedItem.SubItems(1).Text

        TxtPO_KdSupplier.Text = Kode
        TxtPO_NmSupplier.Text = Nama
        LvSupplier.Visible = False
        LvPO_DataPO.Focus()

    End Sub

    Private Sub LvSupplier_KeyDown(sender As Object, e As KeyEventArgs) Handles LvSupplier.KeyDown
        If e.KeyCode = Keys.Enter Then
            LvSupplier_DoubleClick(LvSupplier, e)
        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click


        SD_Sub_PO.Kode_Supplier = TxtPO_KdSupplier.Text
        SD_Sub_PO.ShowDialog()

    End Sub

    Private Sub CmbPO_MataUang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbPO_MataUang.KeyPress
        If e.KeyChar = Chr(13) Then
            CmbPO_CaraBayar.Focus()
        End If
    End Sub

    'Private Sub CmbPO_JnsBayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbPO_JnsBayar.KeyPress
    '    If e.KeyChar = Chr(13) Then
    '        If CmbPO_JnsBayar.SelectedIndex = -1 Then
    '            TxtPO_KdBrg.Focus()
    '        Else
    '            CmbPO_RangeBayar.Focus()
    '        End If
    '    End If
    'End Sub

    Private Sub txtJatuhTempo_TextChanged(sender As Object, e As EventArgs) Handles txtJatuhTempo.TextChanged
        Dim xx As Integer = 0
        If txtJatuhTempo.Text.Trim.Length = 0 Then
            xx = 0
        Else
            xx = Val(txtJatuhTempo.Text)
        End If
        DtpPO_TglBayar.Value = DateAdd(DateInterval.Day, xx, DtpPO_Tgl.Value)


    End Sub



    Private Sub txtJatuhTempo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJatuhTempo.KeyPress
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub



    Private Sub CmbPO_MataUang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPO_MataUang.SelectedIndexChanged
        bersihsebagian()
        LvPO_DataPO.Rows.Clear()
    End Sub



    Private Sub CmbPO_CaraBayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbPO_CaraBayar.KeyPress
        If e.KeyChar = Chr(13) Then TxtPO_KdBrg.Focus()
    End Sub



    Public Sub TxtPO_NoFaktur_Leave(sender As Object, e As EventArgs) Handles TxtPO_NoFaktur.Leave, Txt_Faktur_Induk.Leave

        'get_jam()

        'Try

        '    OpenConn()
        '    Dim checkPPN As Integer = 0
        '    Dim checkFlagRelease As String = ""
        '    SQL = "select a.status,a.No_Nota,a.Kode_Supplier, b.Nama_Supplier as nama,lokasi,a.tanggal, "
        '    SQL = SQL & "a.Jenis_Pembayaran,a.Cara_Bayar, a.lama_pembayaran, Tgl_Jatuh_Tempo,Total_MUA, Mata_Uang,kurs, "
        '    SQL = SQL & "Total_IDR,Grand_Sebelum_PPN,a.ppn,Grand,ETD_Simulasi, ekspedisi,biaya, flag_release "
        '    SQL = SQL & "from EMI_Pembelian_PO a, Suppliers b "
        '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan  "
        '    SQL = SQL & "and a.Kode_Supplier = b.Kode_Supplier  "
        '    SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & TxtPO_NoFaktur.Text & "' "
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then
        '            If General_Class.CekNULL("status") = "" Then
        '                CloseConn()
        '                kosong()
        '                Exit Sub
        '            End If

        '            CmbPO_MataUang.Text = Dr("Mata_Uang")
        '            DtpPO_Tgl.Value = Dr("tanggal")
        '            DtpPO_ETD.Value = Dr("etd_simulasi")
        '            TxtPO_NoNota.Text = Dr("no_nota")
        '            TxtPO_KdSupplier.Text = Dr("kode_supplier")
        '            TxtPO_NmSupplier.Text = Dr("nama")

        '            LvSupplier2.Items.Clear()
        '            LvSupplier2.Visible = False

        '            If Dr("jenis_pembayaran") = "N" Then
        '                DtpPO_TglBayar.Value = Dr("tgl_jatuh_Tempo")
        '            End If

        '            CmbPO_JnsEkspedisi.Text = Dr("ekspedisi")
        '            TxtPO_Biaya.Text = Dr("biaya")
        '            'CmbPO_CaraBayar.Text = Dr("cara_bayar")

        '            CmbPO_JnsBayar.Text = Dr("jenis_pembayaran")

        '            For i As Integer = 0 To arrPembayaran.Count - 1

        '                If arrPembayaran.Item(i) = Dr("jenis_pembayaran") Then
        '                    CmbPO_JnsBayar.SelectedIndex = i
        '                    CmbPO_JnsBayar_SelectedIndexChanged(TxtPO_NoFaktur, e)
        '                    txtJatuhTempo.Text = General_Class.CekNULL(Dr("lama_pembayaran"))
        '                    Exit For
        '                End If

        '            Next

        '            If Dr("jenis_pembayaran") = "T" Then
        '                If General_Class.CekNULL(Dr("cara_bayar")) = "" Then
        '                    CmbPO_CaraBayar.SelectedIndex = 0
        '                Else
        '                    CmbPO_CaraBayar.Text = Dr("cara_bayar")
        '                End If

        '            End If

        '            For i As Integer = 0 To arrEkspedisi.Count - 1
        '                If arrEkspedisi.Item(i) = Dr("ekspedisi") Then
        '                    CmbPO_JnsEkspedisi.SelectedIndex = i
        '                    CmbPO_JnsEkspedisi_SelectedIndexChanged(TxtPO_NoFaktur, e)
        '                End If
        '            Next

        '            'CmbPO_CaraBayar.Text = Dr("cara_bayar")
        '            checkPPN = Dr("ppn")

        '            If General_Class.CekNULL(Dr("flag_release")) = "" Then
        '                checkFlagRelease = "T"
        '            Else
        '                checkFlagRelease = "Y"
        '            End If

        '            If Fstatus = "Y" Then
        '                If checkFlagRelease = "Y" Then
        '                    disableSebagian()
        '                Else
        '                    enableSebagian()
        '                    TxtPO_KdSupplier.Enabled = False
        '                    TxtPO_NmSupplier.Enabled = False
        '                    Button2.Enabled = True
        '                    CmbPO_JnsEkspedisi.Enabled = False
        '                End If

        '            ElseIf Fstatus = "T" Then
        '                disableSebagian()
        '            End If
        '        Else
        '            Dr.Close()
        '            get_no_faktur()
        '            CloseConn()

        '            'kosong()
        '            Exit Sub
        '        End If
        '    End Using

        '    LvPO_DataPO.Items.Clear()
        '    SQL = "select a.No_Faktur,a.Kode_Stock_Owner,a.Kode_Barang,b.Nama,a.No_Urut,a.Jumlah,a.satuan,a.harga,a.Nilai_Barang, "
        '    SQL = SQL & "a.Satuan_Barang,a.Harga_Barang,a.Total,a.No_Penawaran,a.No_Urut_PR "
        '    SQL = SQL & "from EMI_Pembelian_PO_Det a, Barang b "
        '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
        '    SQL = SQL & "and a.kode_stock_owner = b.kode_stock_owner  "
        '    SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
        '    SQL = SQL & "and a.no_faktur = '" & TxtPO_NoFaktur.Text & "'"
        '    Using Ds = BindingTrans(SQL)
        '        With Ds.Tables("MyTable")
        '            If .Rows.Count <> 0 Then

        '                For i As Integer = 0 To .Rows.Count - 1
        '                    Dim LvPO_DataPO As ListViewItem

        '                    LvPO_DataPO = LvPO_DataPO.Items.Add(.Rows(i).Item("kode_stock_owner"))
        '                    LvPO_DataPO.SubItems.Add(.Rows(i).Item("kode_barang"))
        '                    LvPO_DataPO.SubItems.Add(.Rows(i).Item("nama"))
        '                    LvPO_DataPO.SubItems.Add(Format(.Rows(i).Item("harga"), "N2"))
        '                    LvPO_DataPO.SubItems.Add(Format(Val(.Rows(i).Item("jumlah")), "N2"))
        '                    LvPO_DataPO.SubItems.Add(.Rows(i).Item("satuan"))
        '                    LvPO_DataPO.SubItems.Add(.Rows(i).Item("harga_barang"))
        '                    LvPO_DataPO.SubItems.Add(.Rows(i).Item("nilai_barang"))
        '                    LvPO_DataPO.SubItems.Add(.Rows(i).Item("satuan_barang"))
        '                    LvPO_DataPO.SubItems.Add(.Rows(i).Item("no_penawaran"))


        '                    LvPO_DataPO.SubItems.Add("T")


        '                    LvPO_DataPO.SubItems.Add(Format(.Rows(i).Item("nilai_barang") * Val(.Rows(i).Item("harga_barang")), "N2"))
        '                    LvPO_DataPO.SubItems.Add("")
        '                    LvPO_DataPO.SubItems.Add(.Rows(i).Item("No_Urut_PR"))
        '                Next
        '            Else
        '                CloseConn()
        '                MessageBox.Show("PO pembelian tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                kosong()
        '                bersihkanpilihan()
        '                bersihsebagian()
        '                Exit Sub
        '            End If
        '        End With
        '    End Using

        '    If checkPPN <> 0 Then
        '        ChkPO_PPN.Checked = True
        '        HitungGrandTotal()
        '    Else
        '        HitungGrandTotal()
        '    End If
        '    BtnPO_Simpan.Tag = "&Update"

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub



    Private Sub TxtPO_NoFaktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPO_NoFaktur.KeyPress, Txt_Faktur_Induk.KeyPress
        If e.KeyChar = Chr(13) Then LvPO_DataPO.Focus()
    End Sub

    Private Sub LvSupplier2_DoubleClick(sender As Object, e As EventArgs) Handles LvSupplier2.DoubleClick
        If LvSupplier2.Items.Count = 0 Then Exit Sub

        Dim Kode As String = LvSupplier2.FocusedItem.Text
        Dim Nama As String = LvSupplier2.FocusedItem.SubItems(1).Text

        TxtPO_KdSupplier.Text = Kode
        TxtPO_NmSupplier.Text = Nama
        LvSupplier2.Visible = False
        Button1.Focus()
    End Sub



    Private Sub LvSupplier2_KeyDown(sender As Object, e As KeyEventArgs) Handles LvSupplier2.KeyDown
        If e.KeyCode = Keys.Enter Then
            LvSupplier2_DoubleClick(LvSupplier2, e)
        End If
    End Sub



    Private Sub LvPO_DataPO_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles LvPO_DataPO.CellEndEdit

        If Not IsNumeric(LvPO_DataPO.CurrentRow.Cells(cellPO_Jumlah).Value) Then
            LvPO_DataPO.CurrentRow.Cells(cellPO_Jumlah).Value = Format(0, "N2")
            LvPO_DataPO.CurrentRow.Cells(cellPO_Total).Value = Format(0, "N4")
            HitungGrandTotal()
            Exit Sub
        End If

        If Not LvPO_DataPO.Rows.Count = 0 Then
            '======================
            '=     SET FORMAT     =
            '======================
            Dim culture As CultureInfo = CultureInfo.CurrentCulture

            Dim JumlahConvert As Double = 0

            Try
                OpenConn()

                '=======================
                '=     UBAH SATUAN     =
                '=======================

                SQL = "select isnull((" & LvPO_DataPO.CurrentRow.Cells(cellPO_Jumlah).Value & " * a.Nilai), 0) as Hasil "
                SQL = SQL & "from N_EMI_Master_Satuan a "
                SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Kode_Barang = '" & LvPO_DataPO.CurrentRow.Cells(cellPO_KdBarang).Value & "' "
                SQL = SQL & "and a.Satuan = '" & LvPO_DataPO.CurrentRow.Cells(cellSatuanInput).Value & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        JumlahConvert = Dr("Hasil")
                    Else
                        CloseConn()
                        MessageBox.Show("Data Satuan " & LvPO_DataPO.CurrentRow.Cells(cellSatuanInput).Value & " pada Barang " & LvPO_DataPO.CurrentRow.Cells(cellPO_NmBarang).Value & " Tidak Ditemukan Dimaster Satuan",
                            Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            If LvPO_DataPO.CurrentCell.ColumnIndex = cellPO_Jumlah Then

                Dim cellKuantity As String = LvPO_DataPO.CurrentCell.Value.ToString()
                Dim sisa As Double = Format(Val(HilangkanTanda(LvPO_DataPO.CurrentRow.Cells(cellPO_Sisa).Value)), "N4")

                If cellKuantity.Contains(",") Then
                    MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    LvPO_DataPO.CurrentCell.Value = Format(0, "N2")
                    HitungGrandTotal()
                    Exit Sub
                End If

                If JumlahConvert > sisa Then
                    MessageBox.Show("Jumlah Tidak Boleh lebih besar dari sisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    LvPO_DataPO.CurrentCell.Value = Format(0, "N2")
                    LvPO_DataPO.CurrentRow.Cells(cellPO_Total).Value = Format(0, "N4")
                    HitungGrandTotal()
                    Exit Sub
                End If

                Dim nilai As Decimal = Decimal.Parse(cellKuantity)
                Dim formattedValue As String = nilai.ToString("N2", culture)

                Dim nilaiInput As Decimal = Decimal.Parse(JumlahConvert)
                Dim formattedValue_Input As String = nilaiInput.ToString("N4", culture)

                LvPO_DataPO.CurrentCell.Value = formattedValue
                Try
                    OpenConn()

                    Dim KdBarang As String = LvPO_DataPO.CurrentRow.Cells(cellPO_KdBarang).Value
                    Dim satuanBesar As String = LvPO_DataPO.CurrentRow.Cells(cellPO_Satuan).Value
                    Dim satuanKecil As String = LvPO_DataPO.CurrentRow.Cells(cellPO_Satuan_SB).Value
                    'UBAH SATUAN
                    SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & KdBarang & "', '" & satuanBesar & "', '" & satuanKecil & "', '" & HilangkanTanda(formattedValue_Input) & "' ) as hasil"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            LvPO_DataPO.CurrentRow.Cells(cellPO_Jumlah_SB).Value = Dr("hasil")
                        Else
                            Dr.Close()
                        End If
                    End Using

                    CloseConn()
                Catch ex As Exception
                    CloseConn()
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try

                Dim harga As Double = LvPO_DataPO.CurrentRow.Cells(cellPO_Harga_SB).Value
                Dim jumlahKecil As Double = LvPO_DataPO.CurrentRow.Cells(cellPO_Jumlah_SB).Value

                LvPO_DataPO.CurrentRow.Cells(cellPO_Total).Value = Format((harga * jumlahKecil), "N4")

            End If

            HitungGrandTotal()
        End If
    End Sub



    Private Sub LvPO_DataPO_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles LvPO_DataPO.CellEnter
        If Not LvPO_DataPO.Rows.Count = 0 Then
            '======================
            '=     SET FORMAT     =
            '======================

            If LvPO_DataPO.CurrentCell.ColumnIndex = cellPO_Jumlah Then
                Dim cellKuantity As String = LvPO_DataPO.CurrentCell.Value.ToString()

                If cellKuantity = "" Then
                    Exit Sub
                End If

                Dim cleanedStr As String = HilangkanTanda(cellKuantity) ' Menghapus titik
                Dim nilai As Decimal = Decimal.Parse(cleanedStr)

                LvPO_DataPO.CurrentCell.Value = nilai
            End If
        End If
    End Sub



    Private Sub LvPO_DataPO_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles LvPO_DataPO.CellLeave
        If Not LvPO_DataPO.Rows.Count = 0 Then

            '======================
            '=     SET FORMAT     =
            '======================
            Dim culture As CultureInfo = CultureInfo.CurrentCulture

            If LvPO_DataPO.CurrentCell.ColumnIndex = cellPO_Jumlah Then
                Dim cellKuantity As String = LvPO_DataPO.CurrentCell.Value.ToString()

                If cellKuantity = "" Then
                    Exit Sub
                End If


                Dim nilai As Decimal = Decimal.Parse(cellKuantity)
                Dim formattedValue As String = nilai.ToString("N2", culture)

                LvPO_DataPO.CurrentCell.Value = formattedValue

            End If
        End If
    End Sub

    Private Sub Txt_GrandPPH_DoubleClick(sender As Object, e As EventArgs) Handles Txt_GrandPPH.DoubleClick
        If Txt_GrandPPH.Text.Trim.Length = 0 Or HilangkanTanda(Txt_GrandPPH.Text) = 0 Then
            Exit Sub
        End If

        SD_Detail_PajakPO_Sub.Txt_NoFakInduk.Text = Txt_Faktur_Induk.Text
        SD_Detail_PajakPO_Sub.TxtPO_GrandTotal.Text = TxtPO_Total.Text
        SD_Detail_PajakPO_Sub.asal = "SUBPO"
        SD_Detail_PajakPO_Sub.ShowDialog()
    End Sub



    Private Sub HapusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HapusToolStripMenuItem.Click
        If LvPO_DataPO.CurrentRow IsNot Nothing Then
            LvPO_DataPO.Rows.Remove(LvPO_DataPO.CurrentRow)
        End If
    End Sub


    Private Sub Btn_Ekspedisi_Click(sender As Object, e As EventArgs) Handles Btn_Ekspedisi.Click


        SD_Expedisi_PO.NoSubPO = TxtPO_NoFaktur.Text
        SD_Expedisi_PO.Show()


    End Sub



    Public Sub CekEkspedisiPO()

        Try
            OpenConn()


            SQL = "select sum(Total) as Total from Emi_Expedition_PO_Sementara where Kode_Perusahaan = '" & KodePerusahaan & "' and userid = '" & UserID & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Total")) = "" Then
                        Txt_BiayaEkspedisi.Text = 0
                    Else
                        Txt_BiayaEkspedisi.Text = Format(Dr("Total"), "N0")
                    End If
                Else
                    Txt_BiayaEkspedisi.Text = 0
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub EMI_PO_Pembelian_Sub_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Try
            OpenConn()

            SQL = "Delete Emi_Expedition_PO_Sementara where Kode_Perusahaan = '" & KodePerusahaan & "' and userid = '" & UserID & "'"
            ExecuteTrans(SQL)


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub



    '==================================================================================================================================================================================
    '=     HANDLE KEY PRESS
    '==================================================================================================================================================================================
    Private Sub TxtPO_NoNota_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPO_NoNota.KeyPress
        If e.KeyChar = Chr(13) Then LvPO_DataPO.Focus()
    End Sub
    Private Sub CmbPO_JnsBayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbPO_JnsBayar.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_Ekspedisi.Focus()
    End Sub
    Private Sub Cmb_Ekspedisi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Ekspedisi.KeyPress
        If e.KeyChar = Chr(13) Then BtnPO_Simpan.Focus()
    End Sub
    Private Sub TxtPO_KdSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPO_KdSupplier.KeyPress
        If e.KeyChar = Chr(13) Then Button1.Focus()
    End Sub
    Private Sub TxtPO_KdSupplier_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPO_KdSupplier.KeyDown
        If e.KeyCode = Keys.Down Then LvSupplier2.Focus()
    End Sub


End Class