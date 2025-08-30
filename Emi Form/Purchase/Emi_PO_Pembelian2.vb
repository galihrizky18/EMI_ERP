Public Class Emi_PO_Pembelian2
    Public FlagSelisihPO As String
    Public Asal As String = ""

    Dim Jenis = "Po_Bahan"
    Dim arrInisialFaktur, arrPembayaran, arrMUA, arrCrByr, ArrAkunCrByr As New ArrayList
    Dim arrNoPenawaran, arrSatuanPenawaran, arrHargaPenawaran As New ArrayList
    Dim arrEkspedisi As New ArrayList
    Dim arrNoUrutPr As New ArrayList

    Dim fakturSubmitPO As String = ""
    Dim arrInisialFakturSubmitPO As String = ""
    Dim no_Faktur_Sementara As String = ""
    Dim PPN As Double = 0
    Dim canDeleteLv As Boolean

    Dim lvPO_Lokasi As String
    Dim lvPO_KdBarang As String
    Dim lvPO_NmBarang As String
    Dim lvPO_Harga As String
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

    Dim cellPO_Lokasi As Integer = 0
    Dim cellPO_KdBarang As Integer = 1
    Dim cellPO_NmBarang As Integer = 2
    Dim cellPO_Harga As Integer = 3
    Dim cellPO_Jumlah As Integer = 4
    Dim cellPO_Satuan As Integer = 5
    Dim cellPO_Harga_SB As Integer = 6
    Dim cellPO_Jumlah_SB As Integer = 7
    Dim cellPO_Satuan_SB As Integer = 8
    Dim cellPO_NoPenawaran As Integer = 9
    Dim cellPO_ID As Integer = 10
    Dim cellPO_Total As Integer = 11
    Dim cellPO_Urut As Integer = 12
    Dim cellPO_PR As Integer = 13

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
        lvPO_Lokasi = LvPO_DataPO.Items(No_Index).SubItems(cellPO_Lokasi).Text
        lvPO_KdBarang = LvPO_DataPO.Items(No_Index).SubItems(cellPO_KdBarang).Text
        lvPO_NmBarang = LvPO_DataPO.Items(No_Index).SubItems(cellPO_NmBarang).Text
        lvPO_Harga = LvPO_DataPO.Items(No_Index).SubItems(cellPO_Harga).Text
        lvPO_Jumlah = LvPO_DataPO.Items(No_Index).SubItems(cellPO_Jumlah).Text
        lvPO_Satuan = LvPO_DataPO.Items(No_Index).SubItems(cellPO_Satuan).Text
        lvPO_Harga_SB = LvPO_DataPO.Items(No_Index).SubItems(cellPO_Harga_SB).Text
        lvPO_Jumlah_SB = LvPO_DataPO.Items(No_Index).SubItems(cellPO_Jumlah_SB).Text
        lvPO_Satuan_SB = LvPO_DataPO.Items(No_Index).SubItems(cellPO_Satuan_SB).Text
        lvPO_NoPenawaran = LvPO_DataPO.Items(No_Index).SubItems(cellPO_NoPenawaran).Text
        lvPO_ID = LvPO_DataPO.Items(No_Index).SubItems(cellPO_ID).Text
        lvPO_Total = LvPO_DataPO.Items(No_Index).SubItems(cellPO_Total).Text
        lvPO_Urut = LvPO_DataPO.Items(No_Index).SubItems(cellPO_Urut).Text
        lvPO_PR = LvPO_DataPO.Items(No_Index).SubItems(cellPO_PR).Text
    End Sub

    Private Sub HitungGrandTotal()
        Dim Grand As Double = 0
        Dim diskon As Double = 0
        Dim TotalSeluruh As Double = 0
        Dim PPN As Double = 0

        For i As Integer = 0 To LvPO_DataPO.Items.Count - 1
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
            For index = 0 To LvPO_DataPO.Items.Count - 1
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
            kosong()
        End If
    End Sub

    Public Sub Ambil_Data()
        Try
            OpenConn()

            LvPO_DataPO.Items.Clear()
            SQL = "select b.Kode_Stock_Owner, a.Kode_Bahan, b.Nama,a.Harus_Order,Satuan_Order, "
            SQL = SQL & "Harga,Jml_Hrs_Order_SK,Satuan_Hrs_Order_SK,harga_sk, a.no_penawaran, a.Urut "
            SQL = SQL & "from EMI_Prepare_Bahan_Baku_Det_Order a, Barang b, emi_master_penawaran c "
            SQL = SQL & "where a.No_Faktur='" & TxtPO_NoPO.Text & "' and c.Kode_Supplier='" & TxtPO_KdSupplier.Text & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Bahan=b.Kode_Barang and b.Kode_Stock_Owner=a.Kode_STock_Owner "
            SQL = SQL & "and a.Kode_Perusahaan=c.Kode_Perusahaan and a.no_penawaran=c.no_penawaran "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim lvw As ListViewItem
                    lvw = LvPO_DataPO.Items.Add(dr("Kode_Stock_Owner"))
                    lvw.SubItems.Add(dr("Kode_Bahan"))
                    lvw.SubItems.Add(dr("Nama"))
                    lvw.SubItems.Add(Format(dr("Harga"), "N2"))
                    lvw.SubItems.Add(Format(dr("Harus_Order"), "N2"))
                    lvw.SubItems.Add(dr("Satuan_Order"))
                    lvw.SubItems.Add(dr("harga_sk"))
                    lvw.SubItems.Add(dr("Jml_Hrs_Order_SK"))
                    lvw.SubItems.Add(dr("Satuan_Hrs_Order_SK"))
                    lvw.SubItems.Add(dr("no_penawaran"))
                    lvw.SubItems.Add("Y")
                    lvw.SubItems.Add(Format(dr("harga_sk") * dr("Jml_Hrs_Order_SK"), "N2"))
                    lvw.SubItems.Add(dr("Urut"))
                Loop
            End Using

            'CmbPO_Lokasi.Text = Lokasi_PO
            'get_no_faktur()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        HitungGrandTotal()
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

        No_SJ = ""
        No_Plat = ""
        FlagSelisihPO = ""
        TxtPO_NoNota.Text = ""
        get_jam()
        DtpPO_Tgl.Value = tgl_skg
        DtpPO_TglBayar.Value = tgl_skg

        DtpPO_TglBayar.Visible = False
        CmbPO_RangeBayar.Visible = False
        CmbPO_CaraBayar.Enabled = True

        If Asal = "" Then
            CmbPO_MataUang.Enabled = True
        End If

        TxtPO_NoPO.Text = ""
        TxtPO_KdSupplier.Text = ""
        TxtPO_NmSupplier.Text = ""
        TxtPO_PersenPPN.Text = "0"
        TxtPO_Biaya.Text = "0"
        TxtPO_Kurs.Text = 1
        ChkPO_PPN.Checked = False
        ListView1.Visible = False

        TxtPO_Total.Text = "0"
        TxtPO_TotalSblmPPN.Text = "0"
        TxtPO_GrandTotal.Text = "0"
        ListView1.Columns.Clear()
        ListView1.Columns.Add("Kode Barang", 170, HorizontalAlignment.Left)
        ListView1.Columns.Add("Nama Barang", 420, HorizontalAlignment.Left)
        ListView1.View = View.Details

        LvPO_DataPO.Items.Clear()
        LvPO_DataPO.Columns.Clear()
        LvPO_DataPO.Columns.Add("Lokasi", 170, HorizontalAlignment.Left) '0
        LvPO_DataPO.Columns.Add("Kode Barang", 170, HorizontalAlignment.Left) '1
        LvPO_DataPO.Columns.Add("Nama Barang", 450, HorizontalAlignment.Left) '2
        LvPO_DataPO.Columns.Add("Harga", 152, HorizontalAlignment.Right) '3
        LvPO_DataPO.Columns.Add("Jumlah", 150, HorizontalAlignment.Right) '4
        LvPO_DataPO.Columns.Add("Satuan", 100, HorizontalAlignment.Center) '5
        LvPO_DataPO.Columns.Add("Harga_SB", 0, HorizontalAlignment.Right) '6
        LvPO_DataPO.Columns.Add("Jumlah_SB", 0, HorizontalAlignment.Right) '7
        LvPO_DataPO.Columns.Add("Satuan_SB", 0, HorizontalAlignment.Center) '8
        LvPO_DataPO.Columns.Add("No Penawaran", 0, HorizontalAlignment.Center) '9
        LvPO_DataPO.Columns.Add("ID", 0, HorizontalAlignment.Center) '10
        LvPO_DataPO.Columns.Add("Total", 260, HorizontalAlignment.Right) '11
        LvPO_DataPO.Columns.Add("Urut", 0, HorizontalAlignment.Center) '12
        LvPO_DataPO.Columns.Add("No PR", 0, HorizontalAlignment.Center) '13
        LvPO_DataPO.View = View.Details
        CmbPO_Lokasi.Enabled = False

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

            CmbPO_Lokasi.Text = Lokasi

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



            If CmbPO_MataUang.Text.Trim = "" Then
                CloseConn()
                MessageBox.Show("Mata Uang Blum Ada")
                Exit Sub
            End If

            get_no_faktur()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        CmbPO_RangeBayar.Items.Clear()
        For i As Integer = 1 To Jatuh_Tempo_Pembelian
            CmbPO_RangeBayar.Items.Add(i)
        Next
        CmbPO_RangeBayar.SelectedIndex = 0

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
        CmbPO_RangeBayar.Enabled = True
        CmbPO_JnsEkspedisi.Enabled = True
        CmbPO_CaraBayar.Enabled = True
        TxtPO_Biaya.Enabled = True
        ChkPO_PPN.Enabled = True
        TxtPO_NoNota.Enabled = True
        BtnPO_Simpan.Visible = False
        Button2.Visible = False

        BtnPO_Simpan.Tag = "&Simpan"

        LvPO_DataPO.Enabled = True
        canDeleteLv = False

        Try
            OpenConn()

            If CekButtonRole("edit_pembelian_po") = "Y" Then
                enableSebagian()
                Fstatus = "Y"
            Else
                disableSebagian()
                Fstatus = "T"
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        bersihsebagian()
        HitungGrandTotal()

        TxtPO_NmSupplier.Focus()
        LvSupplier2.Location = New Point(185, 191)

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
                        Dim lvw As ListViewItem
                        lvw = ListView1.Items.Add(Dr("kode_barang"))
                        lvw.SubItems.Add(Dr("nama"))
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
                    SQL = "select a.No_Faktur,a.no_penawaran,a.Kode_Supplier, c.Nama,b.satuan, b.Nilai_Barang,b.harga_satuan, b.satuan_Barang from EMI_Master_Penawaran a, EMI_Master_Penawaran_Detail b, Suppliers c "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
                    SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Supplier = c.Kode_Supplier "
                    SQL = SQL & "and b.kode_barang = '" & TxtPO_KdBrg.Text & "' and a.Kode_Supplier='" & TxtPO_KdSupplier.Text & "' "
                    SQL = SQL & "and flag_release = 'Y' and status is null and b.mata_uang = '" & CmbPO_MataUang.Text & "' and a.Selesai is null and '" & Format(tgl_skg, "yyyy-MM-dd") & "' between a.Tgl_Penawaran_Hrg and a.Periode_Akhir_Penawaran  "
                    Using dr2 = OpenTrans(SQL)
                        Do While dr2.Read
                            CmbPO_Harga.Items.Add(Format(dr2("harga_satuan")) & "/" & dr2("satuan").ToString.Trim & "-" & dr2("nama"))
                            arrNoPenawaran.Add(dr2("no_penawaran"))
                            arrSatuanPenawaran.Add(dr2("satuan_Barang")) : arrHargaPenawaran.Add(dr2("Nilai_Barang"))
                        Loop
                    End Using



                    cmb_pr.Items.Clear() : arrNoUrutPr.Clear()
                    SQL = "select a.No_Faktur, b.no_Urut,b.tanggal_delivery  From EMI_Purchase_Requisition a, EMI_Purchase_Requisition_Detail b "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
                    SQL = SQL & "and a.Status is null and flag_release = 'Y' and b.Kode_Barang = '" & TxtPO_KdBrg.Text & "' and b.flag_sudah_po is null "
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

    Private Sub ChkPO_PPN_CheckedChanged(sender As Object, e As EventArgs) Handles ChkPO_PPN.CheckedChanged
        If ChkPO_PPN.Checked = True Then
            TxtPO_PersenPPN.Text = PPN
        Else
            TxtPO_PersenPPN.Text = "0"
        End If
        HitungGrandTotal()
    End Sub

    Private Sub BtnPO_Simpan_Click(sender As Object, e As EventArgs) Handles BtnPO_Simpan.Click
        'If TxtPO_NoPO.Text.Trim.Length = 0 Then
        '    MessageBox.Show("No PO Harus di isi")
        '    TxtPO_NoPO.Focus() : Exit Sub
        'Else
        If TxtPO_NoNota.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan" & " " & Base_Language.Lang_Global_Belum_Diisi & ". . .! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
        ElseIf LvPO_DataPO.Items.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            LvPO_DataPO.Focus() : Exit Sub
        ElseIf arrPembayaran.Item(CmbPO_JnsBayar.SelectedIndex) = "T" Then
            'If CmbPO_CaraBayar.Text.Trim.Length = 0 Or CmbPO_CaraBayar.SelectedIndex = 0 Then
            '    MessageBox.Show(Base_Language.Lang_Global_CaraBayar & " " & Base_Language.Lang_Global_Belum_Diisi & ". . .! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    CmbPO_CaraBayar.Focus() : Exit Sub
            'End If

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

                SQL = "insert into emi_pembelian_PO(Kode_Perusahaan, No_Faktur, No_Nota, Tanggal, Jam, UserID, "
                SQL = SQL & "Kode_Supplier, Lokasi,Jenis_Pembayaran, Mata_Uang, Kurs, Cara_Bayar, Total_MUA, "
                SQL = SQL & "Total_IDR, Grand_Sebelum_PPN, PPN,Grand, No_Prepare_Bahan, ETD_Simulasi, Tgl_Jatuh_Tempo,ekspedisi,biaya) values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & TxtPO_NoFaktur.Text & "', '" & TxtPO_NoNota.Text & "', "
                SQL = SQL & "'" & Format(DtpPO_Tgl.Value, " yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "', "
                SQL = SQL & "'" & TxtPO_KdSupplier.Text & "', '" & CmbPO_Lokasi.Text & "', '" & arrPembayaran.Item(CmbPO_JnsBayar.SelectedIndex) & "', "
                SQL = SQL & "'" & CmbPO_MataUang.Text & "', '" & TxtPO_Kurs.Text & "', " & cb & ", "
                SQL = SQL & "'" & HilangkanTanda(TxtPO_Total.Text) & "', '" & HilangkanTanda(TxtPO_Total.Text) & "', '" & HilangkanTanda(TxtPO_TotalSblmPPN.Text) & "', "
                SQL = SQL & "'" & TxtPO_PersenPPN.Text & "', '" & HilangkanTanda(TxtPO_GrandTotal.Text) & "', " & no_po & ",'" & Format(DtpPO_ETD.Value, "yyyy-MM-dd") & "'," & Tgl_Jatuh_Tempo & ", "
                SQL = SQL & "'" & CmbPO_JnsEkspedisi.Text & "', '" & TxtPO_Biaya.Text & "' )"
                ExecuteTrans(SQL)

                If TxtPO_NoPO.Text.Trim <> "" Then

                    If FlagSelisihPO = "Y" Then
                        SQL = "select * from emi_pembelian_PO where no_faktur='" & TxtPO_NoFaktur.Text & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                dr.Close()
                                SQL = "update emi_pembelian_PO set Flag_ETD='Y' where no_faktur ='" & TxtPO_NoFaktur.Text & "'"
                                ExecuteTrans(SQL)
                            Else
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using

                        SQL = "select * from emi_pembelian_ETA where No_SJ='" & No_SJ & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                dr.Close()
                                SQL = "insert into EMI_Pembelian_ETA_Detail_PO(Kode_Perusahaan, NO_SJ, NO_PO) "
                                SQL = SQL & "values('" & KodePerusahaan & "', '" & No_SJ & "', '" & TxtPO_NoFaktur.Text & "') "
                                ExecuteTrans(SQL)
                            Else
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using
                    End If

                End If

                Dim Flag_PPn As String = ""
                Dim idRencana_Order As String = ""
                For index = 0 To LvPO_DataPO.Items.Count - 1
                    Get_Isi_Listview(index)
                    SQL = "select flag_PPn from barang where KOde_barang='" & lvPO_KdBarang & "' and Kode_STock_Owner ='" & lvPO_Lokasi & "' "
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

                    If FlagSelisihPO = "Y" Then
                        SQL = "select * from EMI_Pembelian_Barang_Masuk_sementara_det where no_faktur='" & TxtPO_NoPO.Text & "' and Kode_barang ='" & lvPO_KdBarang & "' and Kode_STock_Owner ='" & lvPO_Lokasi & "' and barang_diluar_PO ='X' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then

                                SQL = "update EMI_Pembelian_Barang_Masuk_sementara_det set barang_diluar_PO='Y' where urut ='" & dr("urut") & "'"
                                dr.Close()
                                ExecuteTrans(SQL)
                            Else
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using
                    End If

                    SQL = "select no_faktur from emi_pembelian_po_detail where kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and no_faktur = '" & TxtPO_NoFaktur.Text & "'  "
                    SQL = SQL & "and kode_stock_owner = '" & lvPO_Lokasi & "' "
                    SQL = SQL & "and kode_barang = '" & lvPO_KdBarang & "' "
                    SQL = SQL & "and satuan = '" & lvPO_Satuan & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            SQL = "update emi_pembelian_po_detail set jumlah = jumlah + " & HilangkanTanda(lvPO_Jumlah) & ", "
                            SQL = SQL & "nilai_barang = nilai_barang + " & lvPO_Jumlah_SB & ", "
                            SQL = SQL & "total = total +   " & HilangkanTanda(lvPO_Total) & " "
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
                            SQL = SQL & "Total, No_Penawaran, Flag_Prepare) values( "
                            SQL = SQL & "'" & KodePerusahaan & "', '" & TxtPO_NoFaktur.Text & "', '" & lvPO_Lokasi & "', "
                            SQL = SQL & "'" & lvPO_KdBarang & "', '" & HilangkanTanda(lvPO_Jumlah) & "', '" & lvPO_Satuan & "', "
                            SQL = SQL & "'" & HilangkanTanda(lvPO_Harga) & "', '" & lvPO_Jumlah_SB & "', '" & lvPO_Satuan_SB & "', "
                            SQL = SQL & "'" & lvPO_Harga_SB & "', '" & HilangkanTanda(lvPO_Total) & "', '" & lvPO_NoPenawaran & "','" & lvPO_ID & "') "
                            ExecuteTrans(SQL)
                        End If
                    End Using

                    SQL = "insert into EMI_Pembelian_PO_Det(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, "
                    SQL = SQL & "Kode_Barang, Jumlah, Satuan, Harga, Nilai_Barang, Satuan_Barang, Harga_Barang, "
                    SQL = SQL & "Total, No_Penawaran, no_urut_pr) values( "
                    SQL = SQL & "'" & KodePerusahaan & "', '" & TxtPO_NoFaktur.Text & "', '" & lvPO_Lokasi & "', "
                    SQL = SQL & "'" & lvPO_KdBarang & "', '" & HilangkanTanda(lvPO_Jumlah) & "', '" & lvPO_Satuan & "', "
                    SQL = SQL & "'" & HilangkanTanda(lvPO_Harga) & "', '" & lvPO_Jumlah_SB & "', '" & lvPO_Satuan_SB & "', "
                    SQL = SQL & "'" & lvPO_Harga_SB & "', '" & HilangkanTanda(lvPO_Total) & "', '" & lvPO_NoPenawaran & "','" & lvPO_PR & "') "
                    ExecuteTrans(SQL)

                    'check flag kategori supplier

                    Dim flag_kategori_Supplier As String = ""

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

                        Dim kode_kontainer As String = ""
                        Dim Qty_Kontainer As Integer = 0

                        SQL = "select kode_barang,kode_kontainer,qty from barang_per_kontainer "
                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_barang = '" & lvPO_KdBarang & "' "
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
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & " ' and kode_supplier = '" & TxtPO_KdSupplier.Text.Trim & "' "
                        SQL = SQL & "and no_prepare_bahan_Baku ='" & KodePerusahaan & "' and  kode_kontainer = '" & kode_kontainer & "' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                'hanya insert ke detail berdasarkan id_rencana yang sudah ada
                                Dim id_rencana_order As Integer = 0

                                id_rencana_order = Dr("id_rencana")

                                Dr.Close()

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

                                SQL = "update detail_rencana_order set "
                                SQL = SQL & "jumlah_po = jumlah_PO+" & nilai_kirim & " , "
                                SQL = SQL & "jumlah_minimal = jumlah_minimal+" & nilai_kirim & "  "
                                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and id_rencana = '" & id_rencana_order & "' "
                                SQL = SQL & "and kode_stock_owner='" & lvPO_Lokasi & "' and kode_barang='" & lvPO_KdBarang & "' "
                                ExecuteTrans(SQL)

                                SQL = "Select distinct b.kode_barang, b.no_faktur, b.Harga_Satuan, b.Mata_Uang  from emi_master_penawaran a, emi_master_penawaran_detail b  "
                                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.no_faktur = b.no_faktur And flag_baru Is null And a.kode_supplier ='" & TxtPO_KdSupplier.Text.Trim & "' "
                                Using dss = BindingTrans(SQL)
                                    For indexx = 0 To dss.Tables("MyTable").Rows.Count - 1
                                        SQL = "select top(1) a.kode_Perusahaan from rencana_order a, detail_rencana_order b where "
                                        SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.ID_Rencana=b.ID_Rencana and "
                                        SQL = SQL & "a.Kode_Supplier='" & TxtPO_KdSupplier.Text.Trim & "' and b.Kode_barang='" & dss.Tables("MyTable").Rows(indexx).Item("kode_barang") & "' and b.id_rencana <> '" & id_rencana_order & "'"
                                        Using drr = OpenTrans(SQL)
                                            If Not drr.Read Then
                                                drr.Close()

                                                SQL = "insert into komposisi_barang_jadi(kode_perusahaan, "
                                                SQL = SQL & "kode_barang, Qty) Values ("
                                                SQL = SQL & " '" & KodePerusahaan & "', '" & dss.Tables("MyTable").Rows(indexx).Item("kode_barang") & "', "
                                                SQL = SQL & "'1')"
                                                ExecuteTrans(SQL)

                                                SQL = "insert into detail_komposisi_barang_jadi(kode_perusahaan, "
                                                SQL = SQL & "kode_barang, Kode_Bahan, Qty_Bahan) Values("
                                                SQL = SQL & "'" & KodePerusahaan & "', '" & dss.Tables("MyTable").Rows(indexx).Item("kode_barang") & "', "
                                                SQL = SQL & "'" & dss.Tables("MyTable").Rows(indexx).Item("kode_barang") & "', '1')"
                                                ExecuteTrans(SQL)

                                                Dim nama As String = ""
                                                SQL = "select top(1) nama from barang where kode_barang ='" & dss.Tables("MyTable").Rows(indexx).Item("kode_barang") & "' "
                                                Using Dr1 = OpenTrans(SQL)
                                                    If Dr1.Read Then
                                                        nama = Dr1("nama")
                                                    End If
                                                End Using

                                                SQL = "select kode_stock_owner_import from stock_owner_import where kode_perusahaan = '" & KodePerusahaan & "' "
                                                SQL = SQL & "order by kode_stock_owner_import"
                                                Using Dsm = BindingTrans(SQL)

                                                    If Dsm.Tables("MyTable").Rows.Count <> 0 Then
                                                        For iii As Integer = 0 To Dsm.Tables("MyTable").Rows.Count - 1
                                                            SQL = "Insert Into bahan_import(Kode_Perusahaan, Kode_STock_Owner_Import, kode_bahan,Kode_supplier,nama_bahan,kategori,mata_uang,harga,satuan, Flag_Potong_Stock) Values("
                                                            SQL = SQL & "'" & KodePerusahaan & "', '" & Dsm.Tables("MyTable").Rows(iii).Item("kode_stock_owner_import") & "', '" & dss.Tables("MyTable").Rows(indexx).Item("kode_barang") & "','" & TxtPO_KdSupplier.Text.Trim & "',"
                                                            SQL = SQL & "'" & nama & "',"
                                                            SQL = SQL & "'" & "Utama" & "',"
                                                            SQL = SQL & "'" & dss.Tables("MyTable").Rows(indexx).Item("Mata_Uang") & "',"
                                                            SQL = SQL & "'" & dss.Tables("MyTable").Rows(indexx).Item("Harga_Satuan") & "',"
                                                            SQL = SQL & "'" & satuan_kirim & "', '" & "T" & "')"
                                                            ExecuteTrans(SQL)
                                                        Next
                                                    Else
                                                        CloseTrans()
                                                        CloseConn()
                                                        MessageBox.Show("Data lokasi import tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If

                                                End Using

                                                SQL = "insert into detail_rencana_order(id_rencana, kode_Perusahaan,kode_Barang,jumlah_po, isi_satuan_besar, jumlah_minimal, kode_stock_owner, jumlah_per_konte) "
                                                SQL = SQL & "select c.id_rencana, c.kode_perusahaan,b.kode_barang,0 as jumlah_po, 1 as isi_satuan_besar, 0 as jumlah_minimal, "

                                                SQL = SQL & "isnull((select top(1) z.lokasi_gudang from EMI_Kategori_Gudang x, barang y, EMI_Kategori_Gudang_PerLokasi z  "
                                                SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.Id_Kategori_Gudang = y.Id_Kategori_Gudang "
                                                SQL = SQL & "and  x.Id_Kategori_Gudang = z.ID_Kategori_Gudang and x.Kode_Perusahaan = z.Kode_Perusahaan "
                                                SQL = SQL & "and x.kode_perusahaan = c.kode_perusahaan and z.Kode_stock_Owner = '" & CmbPO_Lokasi.Text & "' and y.kode_barang = b.kode_barang ),NULL) as Kode_Stock_Owner, "

                                                SQL = SQL & "isnull((select x.qty from barang_per_kontainer x where x.kode_perusahaan = c.kode_perusahaan and x.kode_barang = b.kode_barang),NULL) as Qty_Konte "
                                                SQL = SQL & "from emi_master_penawaran a, emi_master_penawaran_detail b, rencana_order c "
                                                SQL = SQL & "where a.No_faktur=b.no_faktur and a.kode_supplier = c.kode_supplier and a.kode_perusahaan=c.kode_Perusahaan and c.id_rencana<>" & id_rencana_order & " "
                                                SQL = SQL & "and b.Kode_barang='" & dss.Tables("MyTable").Rows(indexx).Item("kode_barang") & "' "
                                                SQL = SQL & "group by c.kode_perusahaan, c.id_rencana,kode_barang "
                                                ExecuteTrans(SQL)

                                            End If
                                        End Using
                                        SQL = "update emi_master_penawaran_detail set "
                                        SQL = SQL & "flag_baru = 'Y' "
                                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Kode_barang='" & dss.Tables("MyTable").Rows(indexx).Item("kode_barang") & "' "
                                        SQL = SQL & "and no_faktur='" & dss.Tables("MyTable").Rows(indexx).Item("no_faktur") & "' "
                                        ExecuteTrans(SQL)
                                    Next
                                End Using

                                Dim totalJumlahPo As Double = 0
                                Dim totalJumlahTotalPersen As Double = 0

                                SQL = "select sum((jumlah_po / jumlah_per_konte) * 100 ) as total_persen, "
                                SQL = SQL & "sum(Jumlah_PO) as total_jml from Detail_Rencana_order where kode_perusahaan= '" & KodePerusahaan & "' and id_rencana = '" & id_rencana_order & " ' "
                                Using Dr4 = OpenTrans(SQL)
                                    If Dr4.Read Then
                                        totalJumlahPo = Val(HilangkanTanda(Format(Dr4("total_jml"), "N0")))
                                        totalJumlahTotalPersen = Val(HilangkanTanda(Format(Dr4("total_persen"), "N0")))
                                    Else
                                        Dr4.Close()
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show(Base_Language.Lang_GLOBAL_Id_Rencana & " " & Base_Language.Lang_GLOBAL_Tidak_Ditemukan & ". . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                SQL = "update rencana_order set total_jml = " & totalJumlahPo & " , total_persen = " & totalJumlahTotalPersen & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and id_rencana = '" & id_rencana_order & "' "
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert rencana order baru

                                SQL = "insert into rencana_order(kode_perusahaan,periode,kode_supplier,lokasi,kode_kontainer,no_prepare_bahan_baku,no_po,tanggal_po,tanggal_input,jam_input,userid,keterangan,kolom)values("
                                SQL = SQL & "'" & KodePerusahaan & "', '" & Format(tgl_skg, "MMyyyy") & "','" & TxtPO_KdSupplier.Text.Trim & "', '" & CmbPO_Lokasi.Text & "', "
                                SQL = SQL & "'" & kode_kontainer & "', '-','" & TxtPO_NoFaktur.Text & "','" & Format(tgl_skg, "yyyy-MM-dd") & "' ,'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "', '','1' )"
                                ExecuteTrans(SQL)

                                Dim x_urut_rencana_order As Integer = 0
                                SQL = "select IDENT_CURRENT('rencana_order') as urut"
                                Using Dr1 = OpenTrans(SQL)
                                    If Dr1.Read Then
                                        x_urut_rencana_order = Dr1("urut")
                                        idRencana_Order = Dr1("urut")
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

                                SQL = "insert into detail_rencana_order(id_rencana, kode_Perusahaan,kode_Barang,jumlah_po, isi_satuan_besar, jumlah_minimal, kode_stock_owner, jumlah_per_konte) "
                                SQL = SQL & "select c.id_rencana, c.kode_perusahaan,b.kode_barang,0 as jumlah_po, 1 as isi_satuan_besar, 0 as jumlah_minimal, "

                                SQL = SQL & "isnull((select top(1) z.lokasi_gudang from EMI_Kategori_Gudang x, barang y, EMI_Kategori_Gudang_PerLokasi z  "
                                SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.Id_Kategori_Gudang = y.Id_Kategori_Gudang "
                                SQL = SQL & "and  x.Id_Kategori_Gudang = z.ID_Kategori_Gudang and x.Kode_Perusahaan = z.Kode_Perusahaan "
                                SQL = SQL & "and x.kode_perusahaan = c.kode_perusahaan and z.Kode_stock_Owner = '" & CmbPO_Lokasi.Text & "' and y.kode_barang = b.kode_barang ),NULL) as Kode_Stock_Owner, "

                                SQL = SQL & "isnull((select x.qty from barang_per_kontainer x where x.kode_perusahaan = c.kode_perusahaan and x.kode_barang = b.kode_barang),NULL) as Qty_Konte "

                                SQL = SQL & "from emi_master_penawaran a, emi_master_penawaran_detail b, rencana_order c "
                                SQL = SQL & "where a.No_faktur=b.no_faktur and a.kode_supplier = c.kode_supplier and a.kode_perusahaan=c.kode_Perusahaan and c.id_rencana=" & x_urut_rencana_order & " "

                                SQL = SQL & "group by c.kode_perusahaan, c.id_rencana,kode_barang "
                                ExecuteTrans(SQL)

                                SQL = "update detail_rencana_order set "
                                SQL = SQL & "jumlah_po = jumlah_PO+" & nilai_kirim & " , "
                                SQL = SQL & "jumlah_minimal = jumlah_minimal+" & nilai_kirim & "  "
                                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and id_rencana = '" & x_urut_rencana_order & "' "
                                SQL = SQL & "and kode_stock_owner='" & lvPO_Lokasi & "' and kode_barang='" & lvPO_KdBarang & "' "
                                ExecuteTrans(SQL)

                                SQL = "Select distinct b.kode_barang, b.no_faktur, b.Harga_Satuan, b.Mata_Uang from emi_master_penawaran a, emi_master_penawaran_detail b  "
                                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.no_faktur = b.no_faktur And flag_baru Is null And a.kode_supplier ='" & TxtPO_KdSupplier.Text.Trim & "' "
                                Using dss = BindingTrans(SQL)
                                    For indexx = 0 To dss.Tables("MyTable").Rows.Count - 1
                                        SQL = "select top(1) a.kode_Perusahaan from rencana_order a, detail_rencana_order b where "
                                        SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.ID_Rencana=b.ID_Rencana and "
                                        SQL = SQL & "a.Kode_Supplier='" & TxtPO_KdSupplier.Text.Trim & "' and b.Kode_barang='" & dss.Tables("MyTable").Rows(indexx).Item("kode_barang") & "' and b.id_rencana <> '" & x_urut_rencana_order & "'"
                                        Using drr = OpenTrans(SQL)
                                            If Not drr.Read Then
                                                drr.Close()

                                                SQL = "insert into komposisi_barang_jadi(kode_perusahaan, "
                                                SQL = SQL & "kode_barang, Qty) Values ("
                                                SQL = SQL & " '" & KodePerusahaan & "', '" & dss.Tables("MyTable").Rows(indexx).Item("kode_barang") & "', "
                                                SQL = SQL & "'1')"
                                                ExecuteTrans(SQL)

                                                SQL = "insert into detail_komposisi_barang_jadi(kode_perusahaan, "
                                                SQL = SQL & "kode_barang, Kode_Bahan, Qty_Bahan) Values("
                                                SQL = SQL & "'" & KodePerusahaan & "', '" & dss.Tables("MyTable").Rows(indexx).Item("kode_barang") & "', "
                                                SQL = SQL & "'" & dss.Tables("MyTable").Rows(indexx).Item("kode_barang") & "', '1')"
                                                ExecuteTrans(SQL)

                                                Dim nama As String = ""
                                                SQL = "select top(1) nama from barang where kode_barang ='" & dss.Tables("MyTable").Rows(indexx).Item("kode_barang") & "' "
                                                Using Dr1 = OpenTrans(SQL)
                                                    If Dr1.Read Then
                                                        nama = Dr1("nama")
                                                    End If
                                                End Using

                                                SQL = "select kode_stock_owner_import from stock_owner_import where kode_perusahaan = '" & KodePerusahaan & "' "
                                                SQL = SQL & "order by kode_stock_owner_import"
                                                Using Dsm = BindingTrans(SQL)

                                                    If Dsm.Tables("MyTable").Rows.Count <> 0 Then
                                                        For iii As Integer = 0 To Dsm.Tables("MyTable").Rows.Count - 1
                                                            SQL = "Insert Into bahan_import(Kode_Perusahaan, Kode_STock_Owner_Import, kode_bahan,Kode_supplier,nama_bahan,kategori,mata_uang,harga,satuan, Flag_Potong_Stock) Values("
                                                            SQL = SQL & "'" & KodePerusahaan & "', '" & Dsm.Tables("MyTable").Rows(iii).Item("kode_stock_owner_import") & "', '" & dss.Tables("MyTable").Rows(indexx).Item("kode_barang") & "','" & TxtPO_KdSupplier.Text.Trim & "',"
                                                            SQL = SQL & "'" & nama & "',"
                                                            SQL = SQL & "'" & "Utama" & "',"
                                                            SQL = SQL & "'" & dss.Tables("MyTable").Rows(indexx).Item("Mata_Uang") & "',"
                                                            SQL = SQL & "'" & dss.Tables("MyTable").Rows(indexx).Item("Harga_Satuan") & "',"
                                                            SQL = SQL & "'" & satuan_kirim & "', '" & "T" & "')"
                                                            ExecuteTrans(SQL)
                                                        Next
                                                    Else
                                                        CloseTrans()
                                                        CloseConn()
                                                        MessageBox.Show("Data lokasi import tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If

                                                End Using

                                                SQL = "insert into detail_rencana_order(id_rencana, kode_Perusahaan,kode_Barang,jumlah_po, isi_satuan_besar, jumlah_minimal, kode_stock_owner, jumlah_per_konte) "
                                                SQL = SQL & "select c.id_rencana, c.kode_perusahaan,b.kode_barang,0 as jumlah_po, 1 as isi_satuan_besar, 0 as jumlah_minimal, "

                                                SQL = SQL & "isnull((select top(1) z.lokasi_gudang from EMI_Kategori_Gudang x, barang y, EMI_Kategori_Gudang_PerLokasi z  "
                                                SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.Id_Kategori_Gudang = y.Id_Kategori_Gudang "
                                                SQL = SQL & "and  x.Id_Kategori_Gudang = z.ID_Kategori_Gudang and x.Kode_Perusahaan = z.Kode_Perusahaan "
                                                SQL = SQL & "and x.kode_perusahaan = c.kode_perusahaan and z.Kode_stock_Owner = '" & CmbPO_Lokasi.Text & "' and y.kode_barang = b.kode_barang ),NULL) as Kode_Stock_Owner, "

                                                SQL = SQL & "isnull((select x.qty from barang_per_kontainer x where x.kode_perusahaan = c.kode_perusahaan and x.kode_barang = b.kode_barang),NULL) as Qty_Konte "

                                                SQL = SQL & "from emi_master_penawaran a, emi_master_penawaran_detail b, rencana_order c "
                                                SQL = SQL & "where a.No_faktur=b.no_faktur and a.kode_supplier = c.kode_supplier and a.kode_perusahaan=c.kode_Perusahaan and c.id_rencana<>" & x_urut_rencana_order & " "
                                                SQL = SQL & "and b.Kode_barang='" & dss.Tables("MyTable").Rows(indexx).Item("kode_barang") & "' "
                                                SQL = SQL & "group by c.kode_perusahaan, c.id_rencana,kode_barang "
                                                ExecuteTrans(SQL)

                                            End If
                                        End Using

                                        SQL = "update emi_master_penawaran_detail set "
                                        SQL = SQL & "flag_baru = 'Y' "
                                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Kode_barang='" & dss.Tables("MyTable").Rows(indexx).Item("kode_barang") & "' "
                                        SQL = SQL & "and no_faktur='" & dss.Tables("MyTable").Rows(indexx).Item("no_faktur") & "' "
                                        ExecuteTrans(SQL)
                                    Next
                                End Using

                                Dim totalJumlahPo As Integer = 0
                                Dim totalJumlahTotalPersen As Integer = 0

                                SQL = "select sum((jumlah_po / jumlah_per_konte) * 100 ) as total_persen, "
                                SQL = SQL & "sum(Jumlah_PO) as total_jml from Detail_Rencana_order where kode_perusahaan= '" & KodePerusahaan & "' and id_rencana = '" & x_urut_rencana_order & "' "
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

                                SQL = "update rencana_order set total_jml = " & totalJumlahPo & " , total_persen = " & totalJumlahTotalPersen & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and id_rencana = '" & x_urut_rencana_order & "' "
                                ExecuteTrans(SQL)

                                'lalu insert ke detai
                            End If

                        End Using

                    End If

                    SQL = "select a.Jumlah - isnull((select sum(y.Jumlah) from EMI_Pembelian_PO x, EMI_Pembelian_PO_Det y    "
                    SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and y.Kode_Perusahaan = a.Kode_Perusahaan "
                    SQL = SQL & "and y.no_urut_pr = a.No_Urut ), 0) as sisa, a.satuan from  EMI_Purchase_Requisition_Detail a,EMI_Purchase_Requisition b where a.kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and a.Kode_Perusahaan= b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and b.status is null and b.Flag_Release = 'Y' and a.no_urut = '" & lvPO_PR & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then

                            If Dr("sisa") = 0 Then
                                Dr.Close()

                                SQL = "update EMI_Purchase_Requisition_Detail set flag_sudah_po = 'Y' where kode_perusahaan = '" & KodePerusahaan & "' and no_urut = '" & lvPO_PR & "'"
                                ExecuteTrans(SQL)
                            End If
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("No PR tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                Next



                Dim flag_kategori_Supplier_submit_po As String = ""

                SQL = "select b.Flag_Jenis_Import From Suppliers a, Suppliers_Kategori b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and  "
                SQL = SQL & "a.ID_Kategori_Suppliers = b.ID_Kategori_Suppliers "
                SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and Kode_Supplier	 = '" & TxtPO_KdSupplier.Text.Trim & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        flag_kategori_Supplier_submit_po = General_Class.CekNULL(Dr("flag_jenis_import"))
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_GLOBAL_Kategori_Supplier & " " & Base_Language.Lang_GLOBAL_Tidak_Ditemukan & ". . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

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

                If flag_kategori_Supplier_submit_po = "Y" Then
                    'generate no faktur submit PO
                    get_no_faktur_submit_Po()

                    Dim Mata_Uang_Declare As String = ""
                    Dim ind As Integer = 0
                    SQL = "Select Mata_Uang_Rek, Mata_Uang_Declare From suppliers where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Supplier = '" & TxtPO_KdSupplier.Text & "' "
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
                    SQL = SQL & "(Berat*Jumlah_PO) as tot_berat_brsh, (Berat_kotor*Jumlah_PO) as tot_berat_kotor, panjang, lebar, tinggi, a.no_urut, b.mata_uang, b.Harga_Declare_Satuan_Besar  "
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
                                    SQL = "insert into detail_submit_PO (kode_perusahaan, No_faktur, Kode_Stock_Owner, Kode_Barang, Jumlah, "
                                    SQL = SQL & "Harga_Declare, Total, Volume, Jml_Satuan_Besar, Isi_Satuan_Besar, Berat_Bersih, Berat_Kotor, Total_Berat_Bersih, "
                                    SQL = SQL & " Total_Berat_kotor, Panjang, Lebar, Tinggi, Urut_Rencana, Mata_Uang, Harga_Declare_Satuan_Besar,isi_satuan_besar_invoice, Barang_Free) values( "
                                    SQL = SQL & "'" & KodePerusahaan & "', '" & fakturSubmitPO & "', "
                                    SQL = SQL & "'" & .Rows(i).Item("kode_stock_owner") & "', '" & .Rows(i).Item("kode_barang") & "', "
                                    SQL = SQL & "'" & .Rows(i).Item("jumlah_po") & "', '" & Format(.Rows(i).Item("harga_declare"), setN) & "', "
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

                'akhir

                SQL = "select * from EMI_Prepare_Bahan_Baku_Det_Order where no_faktur='" & TxtPO_NoPO.Text & "' and Flag_Selesai is null "
                Using dr = OpenTrans(SQL)
                    If Not dr.Read Then
                        dr.Close()
                        SQL = "update EMI_Prepare_Bahan_Baku set flag_sudah_PO='Y' where no_faktur ='" & TxtPO_NoPO.Text & "'"
                        ExecuteTrans(SQL)
                    End If
                End Using

                If ChkPO_PPN.Checked = True Then
                    If Flag_PPn <> "Y" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_PPN & " " & Base_Language.Lang_Global_Tidak_Bisa_Berbeda & ". . ! ! (2)", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    If Flag_PPn <> "T" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_PPN & " " & Base_Language.Lang_Global_Tidak_Bisa_Berbeda & ". . ! ! (3)", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                End If

                SQL = "UPDATE EMI_Pembelian_PO SET Flag_Lokasi_Tujuan = 'Y' WHERE "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' AND "
                SQL = SQL & "No_Faktur = '" & TxtPO_NoFaktur.Text & "'"
                ExecuteTrans(SQL)
            Else
                'update po pembelian
                SQL = "update  EMI_Pembelian_PO set "
                SQL = SQL & "etd_simulasi = '" & Format(DtpPO_ETD.Value, "yyyy-MM-dd") & "',"
                SQL = SQL & "no_nota = '" & TxtPO_NoNota.Text.Trim & "',"
                SQL = SQL & "jenis_pembayaran = '" & arrPembayaran.Item(CmbPO_JnsBayar.SelectedIndex) & "', "
                SQL = SQL & "tgl_jatuh_tempo = '" & Format(DtpPO_TglBayar.Value, "yyyy-MM-dd") & "',"
                SQL = SQL & "ppn = '" & TxtPO_PersenPPN.Text.Trim & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and no_faktur = '" & TxtPO_NoFaktur.Text & "' "
                ExecuteTrans(SQL)

                'hapus dulu detail po pembelian lalu insert ulang
                SQL = "delete from emi_pembelian_po_detail where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & TxtPO_NoFaktur.Text & "' "
                ExecuteTrans(SQL)

                'delete det nya

                SQL = "delete from emi_pembelian_po_det where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & TxtPO_NoFaktur.Text & "' "
                ExecuteTrans(SQL)

                For i As Integer = 0 To LvPO_DataPO.Items.Count - 1
                    Get_Isi_Listview(i)

                    'insert lagi ke detail
                    SQL = "select no_faktur from emi_pembelian_po_detail where kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and no_faktur = '" & TxtPO_NoFaktur.Text & "'  "
                    SQL = SQL & "and kode_stock_owner = '" & lvPO_Lokasi & "' "
                    SQL = SQL & "and kode_barang = '" & lvPO_KdBarang & "' "
                    SQL = SQL & "and satuan = '" & lvPO_Satuan & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            SQL = "update emi_pembelian_po_detail set jumlah = jumlah + " & HilangkanTanda(lvPO_Jumlah) & ", "
                            SQL = SQL & "nilai_barang = nilai_barang + " & lvPO_Jumlah_SB & ", "
                            SQL = SQL & "total = total +   " & HilangkanTanda(lvPO_Total) & " "
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
                            SQL = SQL & "Total, No_Penawaran, Flag_Prepare) values( "
                            SQL = SQL & "'" & KodePerusahaan & "', '" & TxtPO_NoFaktur.Text & "', '" & lvPO_Lokasi & "', "
                            SQL = SQL & "'" & lvPO_KdBarang & "', '" & HilangkanTanda(lvPO_Jumlah) & "', '" & lvPO_Satuan & "', "
                            SQL = SQL & "'" & HilangkanTanda(lvPO_Harga) & "', '" & lvPO_Jumlah_SB & "', '" & lvPO_Satuan_SB & "', "
                            SQL = SQL & "'" & lvPO_Harga_SB & "', '" & HilangkanTanda(lvPO_Total) & "', '" & lvPO_NoPenawaran & "','" & lvPO_ID & "') "
                            ExecuteTrans(SQL)
                        End If
                    End Using

                    'lalu insert ulang
                    SQL = "insert into EMI_Pembelian_PO_Det(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, "
                    SQL = SQL & "Kode_Barang, Jumlah, Satuan, Harga, Nilai_Barang, Satuan_Barang, Harga_Barang, "
                    SQL = SQL & "Total, No_Penawaran, no_urut_pr) values( "
                    SQL = SQL & "'" & KodePerusahaan & "', '" & TxtPO_NoFaktur.Text & "', '" & lvPO_Lokasi & "', "
                    SQL = SQL & "'" & lvPO_KdBarang & "', '" & HilangkanTanda(lvPO_Jumlah) & "', '" & lvPO_Satuan & "', "
                    SQL = SQL & "'" & HilangkanTanda(lvPO_Harga) & "', '" & lvPO_Jumlah_SB & "', '" & lvPO_Satuan_SB & "', "
                    SQL = SQL & "'" & lvPO_Harga_SB & "', '" & HilangkanTanda(lvPO_Total) & "', '" & lvPO_NoPenawaran & "','" & lvPO_PR & "') "
                    ExecuteTrans(SQL)
                Next

            End If

            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()

        If FlagSelisihPO = "Y" Then
            'EMI_Exclude_PO_Display.kosong()
        Else
            EMI_PO_Pembelian_Display.Cari("Y")
        End If
        Me.Close()
    End Sub

    Private Sub BtnPO_Refresh_Click(sender As Object, e As EventArgs) Handles BtnPO_Refresh.Click
        kosong()
    End Sub



    Private Sub CmbPO_Harga_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPO_Harga.SelectedIndexChanged

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

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles Groupbox.Enter

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
        LvPO_DataPO.Items.Clear()

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
            CmbPO_RangeBayar.Visible = False
            CmbPO_CaraBayar.Enabled = True
            CmbPO_CaraBayar.SelectedIndex = 0
        Else
            DtpPO_TglBayar.Visible = True
            CmbPO_RangeBayar.Visible = True
            CmbPO_CaraBayar.Enabled = False
            CmbPO_CaraBayar.SelectedIndex = 0
        End If
    End Sub

    Private Sub BtnPO_Ok_Click(sender As Object, e As EventArgs) Handles BtnPO_Ok.Click
        If CmbPO_Harga.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Harga & " " & Base_Language.Lang_Global_Belum_Diisi & ". . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbPO_Harga.Focus()
            Exit Sub
        ElseIf TxtPO_Jml.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Jumlah & " " & Base_Language.Lang_Global_Belum_Diisi & ". . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtPO_Jml.Focus()
            Exit Sub
        ElseIf CmbPO_Satuan.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_Satuan & " " & Base_Language.Lang_Global_Belum_Diisi & ". . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbPO_Satuan.Focus()
            Exit Sub
        End If

        For i As Integer = 0 To LvPO_DataPO.Items.Count - 1
            If LvPO_DataPO.Items(i).SubItems(cellPO_KdBarang).Text = TxtPO_KdBrg.Text Then
                If LvPO_DataPO.Items(i).SubItems(cellPO_Satuan).Text <> CmbPO_Satuan.Text Then
                    MessageBox.Show(Base_Language.Lang_Global_Satuan & " " & Base_Language.Lang_Global_Tidak_Bisa_Berbeda & ". . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                If LvPO_DataPO.Items(i).SubItems(cellPO_ID).Text = "T" Then
                    If LvPO_DataPO.Items(i).SubItems(cellPO_NoPenawaran).Text = arrNoPenawaran.Item(CmbPO_Harga.SelectedIndex) Then
                        If LvPO_DataPO.Items(i).SubItems(cellPO_PR).Text = arrNoUrutPr.Item(cmb_pr.SelectedIndex) Then
                            MessageBox.Show(Base_Language.Lang_Global_Data_Sdh_Ada & ". . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End If
                End If
            End If
        Next

        Try
            OpenConn()
            Dim harga_satuan_besar As Double = 0
            SQL = "Select dbo.Ubah_Satuan('" & KodePerusahaan & "','UANG','" & TxtPO_KdBrg.Text & "',"
            SQL = SQL & "'" & arrSatuanPenawaran.Item(CmbPO_Harga.SelectedIndex) & "','" & CmbPO_Satuan.Text & "',"
            SQL = SQL & "" & arrHargaPenawaran.Item(CmbPO_Harga.SelectedIndex) & ") as Hasil "
            Using dr = OpenTrans(SQL)
                If dr.Read Then

                    If General_Class.CekNULL(dr("Hasil")) <> "" Then
                        If dr("Hasil") = 0 Then
                            MessageBox.Show("Satuan " & arrSatuanPenawaran.Item(CmbPO_Harga.SelectedIndex) & " Ke " & CmbPO_Satuan.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        Else
                            harga_satuan_besar = dr("hasil")
                        End If
                    Else
                        MessageBox.Show("Satuan " & arrSatuanPenawaran.Item(CmbPO_Harga.SelectedIndex) & " Ke " & CmbPO_Satuan.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
            End Using

            Dim Jumlah_satuan_Kecil As Double = 0
            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & TxtPO_KdBrg.Text & "',"
            SQL = SQL & "'" & CmbPO_Satuan.Text & "','" & TxtPO_SatuanBarang.Text & "',"
            SQL = SQL & "" & HilangkanTanda(TxtPO_Jml.Text) & ") as Hasil "
            Using dr = OpenTrans(SQL)
                If dr.Read Then

                    If General_Class.CekNULL(dr("Hasil")) <> "" Then
                        If dr("Hasil") = 0 Then
                            MessageBox.Show("Satuan " & CmbPO_Satuan.Text & " Ke " & TxtPO_SatuanBarang.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        Else
                            Jumlah_satuan_Kecil = dr("hasil")
                        End If
                    Else
                        MessageBox.Show("Satuan " & CmbPO_Satuan.Text & " Ke " & TxtPO_SatuanBarang.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
            End Using

            Dim jumlah_sisa_satuan_kecil As Double = 0
            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & TxtPO_KdBrg.Text & "',"
            SQL = SQL & "'" & txtSatuanSisa.Text & "','" & TxtPO_SatuanBarang.Text & "',"
            SQL = SQL & "" & HilangkanTanda(txtSisaPr.Text) & ") as Hasil "
            Using dr = OpenTrans(SQL)
                If dr.Read Then

                    If General_Class.CekNULL(dr("Hasil")) <> "" Then
                        If dr("Hasil") = 0 Then
                            MessageBox.Show("Satuan " & CmbPO_Satuan.Text & " Ke " & TxtPO_SatuanBarang.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        Else
                            jumlah_sisa_satuan_kecil = dr("hasil")
                        End If
                    Else
                        MessageBox.Show("Satuan " & CmbPO_Satuan.Text & " Ke " & TxtPO_SatuanBarang.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
            End Using

            Dim lokasi_gudang_bahan As String = ""

            SQL = "select top(1) c.lokasi_gudang from EMI_Kategori_Gudang a, barang b, EMI_Kategori_Gudang_PerLokasi c  "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Gudang = b.Id_Kategori_Gudang "
            SQL = SQL & "and  a.Id_Kategori_Gudang = c.ID_Kategori_Gudang and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and c.kode_stock_owner = '" & CmbPO_Lokasi.Text & "' "
            SQL = SQL & "and b.kode_barang = '" & TxtPO_KdBrg.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    lokasi_gudang_bahan = dr("lokasi_gudang")
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_Global_LokasiGudang & " " & Base_Language.Lang_GLOBAL_Tidak_Ditemukan & ". . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If jumlah_sisa_satuan_kecil < Jumlah_satuan_Kecil Then
                MessageBox.Show("Jumlah po tidak boleh lebih besar dari jumlah PR!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim lvw As ListViewItem
            lvw = LvPO_DataPO.Items.Add(lokasi_gudang_bahan)
            lvw.SubItems.Add(TxtPO_KdBrg.Text)
            lvw.SubItems.Add(TxtPO_NmBrg.Text)
            lvw.SubItems.Add(Format(Val(harga_satuan_besar), "N2"))
            lvw.SubItems.Add(Format(Val(TxtPO_Jml.Text), "N2"))
            lvw.SubItems.Add(CmbPO_Satuan.Text)
            lvw.SubItems.Add(arrHargaPenawaran.Item(CmbPO_Harga.SelectedIndex))
            lvw.SubItems.Add(Jumlah_satuan_Kecil)
            lvw.SubItems.Add(TxtPO_SatuanBarang.Text)
            lvw.SubItems.Add(arrNoPenawaran.Item(CmbPO_Harga.SelectedIndex))
            If FlagSelisihPO = "Y" Then
                lvw.SubItems.Add("X")
            Else
                lvw.SubItems.Add("T")
            End If

            lvw.SubItems.Add(Format(Jumlah_satuan_Kecil * Val(arrHargaPenawaran.Item(CmbPO_Harga.SelectedIndex)), "N2"))
            lvw.SubItems.Add("")
            lvw.SubItems.Add(arrNoUrutPr.Item(cmb_pr.SelectedIndex))

            CmbPO_MataUang.Enabled = False

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        bersihsebagian()

        HitungGrandTotal()
    End Sub

    Private Sub LvPO_DataPO_DoubleClick(sender As Object, e As EventArgs) Handles LvPO_DataPO.DoubleClick
        If LvPO_DataPO.Items.Count = 0 Or LvPO_DataPO.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu Data barang yang mau dihapus!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Get_Isi_Listview(LvPO_DataPO.FocusedItem.Index)

        If lvPO_ID = "Y" Or lvPO_ID = "X" Then
            MessageBox.Show("Data Tidak Bisa dihapus . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If canDeleteLv = True Then
            LvPO_DataPO.FocusedItem.Remove()


            If LvPO_DataPO.Items.Count = 0 Then
                CmbPO_MataUang.Enabled = True
            End If

        End If




        HitungGrandTotal()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnRelease.Click
        EMI_PO_Pembelian_Display.asal = Jenis
        EMI_PO_Pembelian_Display.filter_tambahan = " and a.Flag_Sudah_PO is null "
        EMI_PO_Pembelian_Display.ShowDialog()
    End Sub

    Private Sub CmbPO_RangeBayar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPO_RangeBayar.SelectedIndexChanged
        Dim xx As Integer = 0
        If CmbPO_RangeBayar.SelectedIndex = -1 Then
            xx = 0
        Else
            xx = Val(CmbPO_RangeBayar.Text)
        End If
        DtpPO_TglBayar.Value = DateAdd(DateInterval.Day, xx, DtpPO_Tgl.Value)
    End Sub

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
            SQL = "select a.Jumlah - isnull((select sum(y.Jumlah) from EMI_Pembelian_PO x, EMI_Pembelian_PO_Det y   "
            SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur "
            SQL = SQL & "and y.Kode_Perusahaan = a.Kode_Perusahaan and y.no_urut_pr = a.No_Urut "
            SQL = SQL & "), 0) as sisa, a.satuan "
            SQL = SQL & "from  EMI_Purchase_Requisition_Detail a,EMI_Purchase_Requisition b "
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


    Private Sub disableSebagian()
        TxtPO_NoNota.Enabled = False
        DtpPO_ETD.Enabled = False
        DtpPO_Tgl.Enabled = False
        DtpPO_TglBayar.Enabled = False
        TxtPO_NoPO.Enabled = False
        TxtPO_KdSupplier.Enabled = False
        TxtPO_NmSupplier.Enabled = False
        CmbPO_JnsBayar.Enabled = False
        CmbPO_RangeBayar.Enabled = False
        CmbPO_JnsEkspedisi.Enabled = False
        CmbPO_CaraBayar.Enabled = False
        ChkPO_PPN.Enabled = False

        TxtPO_KdBrg.Enabled = False
        CmbPO_Harga.Enabled = False
        cmb_pr.Enabled = False
        TxtPO_Jml.Enabled = False
        CmbPO_Satuan.Enabled = False

        LvPO_DataPO.Enabled = True
        canDeleteLv = False
        BtnPO_Simpan.Visible = True
        Button2.Visible = False
        Button2.Enabled = False

        Button1.Enabled = False
        BtnPO_Clear.Enabled = False
        BtnPO_Ok.Enabled = False
        BtnPO_Simpan.Enabled = False

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
        CmbPO_RangeBayar.Enabled = True
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
        Try
            OpenConn()

            SQL = "select status,selesai,flag_release from EMI_Pembelian_PO "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and no_faktur = '" & TxtPO_NoFaktur.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If General_Class.CekNULL(dr("status")) = "Y" Then
                        dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Global_DataSudahBatal, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("selesai")) = "Y" Then
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("Transaksi tidak bisa dilanjutkan, karena PO sudah selesai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("flag_release")) = "Y" Then
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("Faktur PO Pembelian ini sudah pernah direlease!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    dr.Close()
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

            CloseConn()
            MessageBox.Show("Data berhasil direlease ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kosong()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        '==========================
        '=      CETAK REPORT      =
        '==========================
        Try
            OpenConn()

            SQL = "select a.No_Faktur from EMI_Pembelian_PO a, EMI_Pembelian_PO_Detail b, barang c, suppliers d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Kode_Supplier = d.Kode_Supplier "
            SQL = SQL & "and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur='" & no_Faktur_Sementara & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    With Ds.Tables(0)
                        Dim CrDoc As New Faktur_Purchase_Order
                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Laporan Faktur Purchase Order"
                            CrDoc.RecordSelectionFormula = " {EMI_Pembelian_PO.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Pembelian_PO.No_Faktur} = '" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "'"

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
        Me.Close()
        EMI_PO_Pembelian_Display.BtnRefresh_Click(Button2, e)
    End Sub

    Private Sub TxtPO_KdSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPO_KdSupplier.KeyPress
        If e.KeyChar = Chr(13) Then CmbPO_MataUang.Focus()
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

    Private Sub TxtPO_NoFaktur_TextChanged(sender As Object, e As EventArgs) Handles TxtPO_NoFaktur.TextChanged

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If TxtPO_KdSupplier.Text.Trim.Length = 0 Then
            MessageBox.Show("Supplier belum dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf CmbPO_MataUang.Text.Trim.Length = 0 Then
            MessageBox.Show("Mata Uang belum dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        EMI_PO_Pembelian_Display_User.LokasiPO = CmbPO_Lokasi.Text
        EMI_PO_Pembelian_Display_User.KdSupp = TxtPO_KdSupplier.Text
        EMI_PO_Pembelian_Display_User.MataUang = CmbPO_MataUang.Text
        EMI_PO_Pembelian_Display_User.ShowDialog()
    End Sub

    Private Sub CmbPO_MataUang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbPO_MataUang.KeyPress
        If e.KeyChar = Chr(13) Then
            CmbPO_CaraBayar.Focus()
        End If
    End Sub

    Private Sub CmbPO_JnsBayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbPO_JnsBayar.KeyPress
        If e.KeyChar = Chr(13) Then
            If CmbPO_JnsBayar.SelectedIndex = -1 Then
                TxtPO_KdBrg.Focus()
            Else
                CmbPO_RangeBayar.Focus()
            End If
        End If
    End Sub

    Private Sub CmbPO_MataUang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPO_MataUang.SelectedIndexChanged
        bersihsebagian()
        LvPO_DataPO.Items.Clear()
    End Sub

    Private Sub CmbPO_CaraBayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbPO_CaraBayar.KeyPress
        If e.KeyChar = Chr(13) Then TxtPO_KdBrg.Focus()
    End Sub

    Public Sub TxtPO_NoFaktur_Leave(sender As Object, e As EventArgs) Handles TxtPO_NoFaktur.Leave

        get_jam()

        Try

            OpenConn()
            Dim checkPPN As Integer = 0
            Dim checkFlagRelease As String = ""
            SQL = "select a.status,a.No_Nota,a.Kode_Supplier, b.Nama_Supplier as nama,lokasi,a.tanggal, "
            SQL = SQL & "Jenis_Pembayaran,Cara_Bayar,Tgl_Jatuh_Tempo,Total_MUA, Mata_Uang,kurs, "
            SQL = SQL & "Total_IDR,Grand_Sebelum_PPN,ppn,Grand,ETD_Simulasi, ekspedisi,biaya, flag_release "
            SQL = SQL & "from EMI_Pembelian_PO a, Suppliers b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan  "
            SQL = SQL & "and a.Kode_Supplier = b.Kode_Supplier  "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & TxtPO_NoFaktur.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL("status") = "" Then
                        CloseConn()
                        kosong()
                        Exit Sub
                    End If

                    DtpPO_Tgl.Value = Dr("tanggal")
                    DtpPO_ETD.Value = Dr("etd_simulasi")
                    TxtPO_NoNota.Text = Dr("no_nota")
                    TxtPO_KdSupplier.Text = Dr("kode_supplier")
                    TxtPO_NmSupplier.Text = Dr("nama")

                    LvSupplier2.Items.Clear()
                    LvSupplier2.Visible = False

                    If Dr("jenis_pembayaran") = "N" Then
                        DtpPO_TglBayar.Value = Dr("tgl_jatuh_Tempo")
                    End If

                    CmbPO_JnsEkspedisi.Text = Dr("ekspedisi")
                    TxtPO_Biaya.Text = Dr("biaya")
                    'CmbPO_CaraBayar.Text = Dr("cara_bayar")

                    CmbPO_JnsBayar.Text = Dr("jenis_pembayaran")

                    For i As Integer = 0 To arrPembayaran.Count - 1

                        If arrPembayaran.Item(i) = Dr("jenis_pembayaran") Then
                            CmbPO_JnsBayar.SelectedIndex = i
                            CmbPO_JnsBayar_SelectedIndexChanged(TxtPO_NoFaktur, e)
                            Exit For
                        End If

                    Next

                    If Dr("jenis_pembayaran") = "T" Then
                        If General_Class.CekNULL(Dr("cara_bayar")) = "" Then
                            CmbPO_CaraBayar.SelectedIndex = 0
                        Else
                            CmbPO_CaraBayar.Text = Dr("cara_bayar")
                        End If

                    End If

                    For i As Integer = 0 To arrEkspedisi.Count - 1
                        If arrEkspedisi.Item(i) = Dr("ekspedisi") Then
                            CmbPO_JnsEkspedisi.SelectedIndex = i
                            CmbPO_JnsEkspedisi_SelectedIndexChanged(TxtPO_NoFaktur, e)
                        End If
                    Next

                    'CmbPO_CaraBayar.Text = Dr("cara_bayar")
                    checkPPN = Dr("ppn")

                    If General_Class.CekNULL(Dr("flag_release")) = "" Then
                        checkFlagRelease = "T"
                    Else
                        checkFlagRelease = "Y"
                    End If

                    If Fstatus = "Y" Then
                        If checkFlagRelease = "Y" Then
                            disableSebagian()
                        Else
                            enableSebagian()
                            TxtPO_KdSupplier.Enabled = False
                            TxtPO_NmSupplier.Enabled = False
                            Button2.Enabled = True
                            CmbPO_JnsEkspedisi.Enabled = False
                        End If

                    ElseIf Fstatus = "T" Then
                        disableSebagian()
                    End If
                Else
                    Dr.Close()
                    get_no_faktur()
                    CloseConn()

                    'kosong()
                    Exit Sub
                End If
            End Using

            LvPO_DataPO.Items.Clear()
            SQL = "select a.No_Faktur,a.Kode_Stock_Owner,a.Kode_Barang,b.Nama,a.No_Urut,a.Jumlah,a.satuan,a.harga,a.Nilai_Barang, "
            SQL = SQL & "a.Satuan_Barang,a.Harga_Barang,a.Total,a.No_Penawaran,a.No_Urut_PR "
            SQL = SQL & "from EMI_Pembelian_PO_Det a, Barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.kode_stock_owner = b.kode_stock_owner  "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.no_faktur = '" & TxtPO_NoFaktur.Text & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        For i As Integer = 0 To .Rows.Count - 1
                            Dim lvw As ListViewItem

                            lvw = LvPO_DataPO.Items.Add(.Rows(i).Item("kode_stock_owner"))
                            lvw.SubItems.Add(.Rows(i).Item("kode_barang"))
                            lvw.SubItems.Add(.Rows(i).Item("nama"))
                            lvw.SubItems.Add(Format(.Rows(i).Item("harga"), "N2"))
                            lvw.SubItems.Add(Format(Val(.Rows(i).Item("jumlah")), "N2"))
                            lvw.SubItems.Add(.Rows(i).Item("satuan"))
                            lvw.SubItems.Add(.Rows(i).Item("harga_barang"))
                            lvw.SubItems.Add(.Rows(i).Item("nilai_barang"))
                            lvw.SubItems.Add(.Rows(i).Item("satuan_barang"))
                            lvw.SubItems.Add(.Rows(i).Item("no_penawaran"))

                            If FlagSelisihPO = "Y" Then
                                lvw.SubItems.Add("X")
                            Else
                                lvw.SubItems.Add("T")
                            End If

                            lvw.SubItems.Add(Format(.Rows(i).Item("nilai_barang") * Val(.Rows(i).Item("harga_barang")), "N2"))
                            lvw.SubItems.Add("")
                            lvw.SubItems.Add(.Rows(i).Item("No_Urut_PR"))
                        Next
                    Else
                        CloseConn()
                        MessageBox.Show("PO pembelian tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        kosong()
                        bersihkanpilihan()
                        bersihsebagian()
                        Exit Sub
                    End If
                End With
            End Using

            If checkPPN <> 0 Then
                ChkPO_PPN.Checked = True
                HitungGrandTotal()
            Else
                HitungGrandTotal()
            End If
            BtnPO_Simpan.Tag = "&Update"

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtPO_NoFaktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPO_NoFaktur.KeyPress
        If e.KeyChar = Chr(13) Then LvPO_DataPO.Focus()
    End Sub

    Private Sub LvSupplier2_DoubleClick(sender As Object, e As EventArgs) Handles LvSupplier2.DoubleClick
        If LvSupplier2.Items.Count = 0 Then Exit Sub

        Dim Kode As String = LvSupplier2.FocusedItem.Text
        Dim Nama As String = LvSupplier2.FocusedItem.SubItems(1).Text

        TxtPO_KdSupplier.Text = Kode
        TxtPO_NmSupplier.Text = Nama
        LvSupplier2.Visible = False
        LvPO_DataPO.Focus()
    End Sub

    Private Sub LvSupplier2_KeyDown(sender As Object, e As KeyEventArgs) Handles LvSupplier2.KeyDown
        If e.KeyCode = Keys.Enter Then
            LvSupplier2_DoubleClick(LvSupplier2, e)
        End If
    End Sub

End Class