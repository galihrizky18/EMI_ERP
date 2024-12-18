Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class EMI_Pembelian2
    Dim Jenis = "Pembelian"
    Dim arrPersediaan, arrInisialFaktur, arrCrByr, ArrAkunCB1 As New ArrayList
    Dim arrJnsB_Byr As New ArrayList

    Dim LvNo_PO As String
    Dim LvSo As String
    Dim LvKd_Brg As String
    Dim LvNm_Brg As String
    Dim LvSerial As String
    Dim LvHarga As String
    Dim LvJumlah As String
    Dim LvJml_Brg As String
    Dim LvJml_Msk As String
    Dim LvSatuan As String
    Dim LvDisc_Persen As String
    Dim LvDisc_Rp As String
    Dim LvTotal As String
    Dim LvPakai_SN As String
    Dim LvModal As String
    Dim LvUpd_Hpp As String
    Dim LvSisa As String
    Dim LvTtl_Harga As String
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
            LvPembelian_DataPembelian.Columns.Add("No PO", 130, HorizontalAlignment.Left) '0
            LvPembelian_DataPembelian.Columns.Add("Stock Owner Import", 0, HorizontalAlignment.Left) '1
            LvPembelian_DataPembelian.Columns.Add(Base_Language.Lang_Pembelian_Kd_Bahan, 150, HorizontalAlignment.Left) '2
            LvPembelian_DataPembelian.Columns.Add(Base_Language.Lang_Pembelian_Nm_Bahan, 350, HorizontalAlignment.Left) '3
            LvPembelian_DataPembelian.Columns.Add(Base_Language.Lang_Pembelian_No_Seri, 0, HorizontalAlignment.Left) '4
            LvPembelian_DataPembelian.Columns.Add(Base_Language.Lang_Pembelian_Harga, 150, HorizontalAlignment.Right) '5
            LvPembelian_DataPembelian.Columns.Add(Base_Language.Lang_Pembelian_Jumlah, 0, HorizontalAlignment.Right) '6
            LvPembelian_DataPembelian.Columns.Add("Jumlah Barang", 150, HorizontalAlignment.Center) '7
            LvPembelian_DataPembelian.Columns.Add("Jumlah Masuk", 150, HorizontalAlignment.Center) '8
            LvPembelian_DataPembelian.Columns.Add(Base_Language.Lang_Pembelian_Satuan, 120, HorizontalAlignment.Center).DisplayIndex = 4 '9
            LvPembelian_DataPembelian.Columns.Add("Disc(%)", 0, HorizontalAlignment.Right) '10
            LvPembelian_DataPembelian.Columns.Add("Disc(Rp.)", 0, HorizontalAlignment.Right) '11
            LvPembelian_DataPembelian.Columns.Add(Base_Language.Lang_Pembelian_Total, 150, HorizontalAlignment.Right) '12
            LvPembelian_DataPembelian.Columns.Add("Pakai SN", 0, HorizontalAlignment.Left) '13
            LvPembelian_DataPembelian.Columns.Add("Modal", 0, HorizontalAlignment.Left) '14
            LvPembelian_DataPembelian.Columns.Add("Upd HPP", 0, HorizontalAlignment.Left) '15 update hpp
            LvPembelian_DataPembelian.Columns.Add(Base_Language.Lang_Pembelian_Sisa, 0, HorizontalAlignment.Right) '16
            LvPembelian_DataPembelian.Columns.Add(Base_Language.Lang_Pembelian_Total_Harga, 0, HorizontalAlignment.Right) '17
            LvPembelian_DataPembelian.View = View.Details


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
            ChkPembelian_PPN.Checked = False

            CmbPembelian_MataUang.SelectedIndex = -1
            CmbPembelian_JnsBayar.SelectedIndex = -1
            CmbPembelian_RangeBayar.SelectedIndex = -1
            CmbPembelian_Lokasi.Text = Lokasi
            TxtPembelian_Kurs.Text = ""

            LblPembelian_TotalBiaya.Text = ""
            LvPembelian_DataPembelian.Items.Clear()

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

            get_no_faktur()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub HitungGrandTotal()
        Dim Grand As Double = 0
        Dim diskon As Double = 0
        Dim TotalSeluruh As Double = 0
        Dim PPN As Double = 0

        For i As Integer = 0 To LvPembelian_DataPembelian.Items.Count - 1
            Get_Isi_Listview(i)

            Grand = Grand + HilangkanTanda(LvTotal)
        Next

        'End If
        TotalSeluruh = Grand * Val(TxtPembelian_Kurs.Text)
        PPN = TotalSeluruh * Val(TxtPembelian_PersenPPN.Text) / 100

        TxtPembelian_TotalMUA.Text = Format(Grand, "N2")
        TxtPembelian_TotalIDR.Text = Format(TotalSeluruh, "N2")
        TxtPembelian_TotalSblmPPN.Text = Format(TotalSeluruh, "N2")
        TxtPembelian_NilaiPPN.Text = Format(PPN, "N2")
        LblPembelian_TotalBiaya.Text = Format(TotalSeluruh + PPN, "N2")
        TxtPembelian_GrandTotal.Text = Format(TotalSeluruh + PPN, "N2")

    End Sub
    Private Sub get_no_faktur()
        TxtPembelian_NoFaktur.Text = FPembelian & arrInisialFaktur.Item(CmbPembelian_Lokasi.SelectedIndex) & "-" & Format(DtpPembelian_Tgl.Value, "MM/yy") & "-" &
                                  General_Class.Get_Last_Number2("EMI_Pembelian", "no_faktur", 5,
                                  "Kode_perusahaan", KodePerusahaan,
                                  "And", "substring(no_faktur,1," & Len(FPembelian) + Len(arrInisialFaktur.Item(CmbPembelian_Lokasi.SelectedIndex)) + 6 & ")", FPembelian & arrInisialFaktur.Item(CmbPembelian_Lokasi.SelectedIndex) & "-" & Format(DtpPembelian_Tgl.Value, "MM/yy"))
    End Sub
    Private Sub Pembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        Try
            OpenConn()






            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        ' Kosong()
    End Sub

    Private Sub BtnPembelian_Simpan_Click(sender As Object, e As EventArgs) Handles BtnPembelian_Simpan.Click
        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur()

            If LvPembelian_DataPembelian.Items.Count = 0 Then
                MessageBox.Show(Base_Language.Lang_Global_Error_Lv_Kosong, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            ElseIf TxtPembelian_NoPO.Text.Trim.Length = 0 Then
                MessageBox.Show(Base_Language.Lang_Global_Error_No_PO, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TxtPembelian_NoPO.Focus() : Exit Sub
            ElseIf CmbPembelian_JnsBayar.SelectedIndex = 1 Then
                If CmbPembelian_RangeBayar.Text.Trim.Length = 0 Then
                    MessageBox.Show(Base_Language.Lang_Global_Error_Jatuh_Tempo, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    TxtPembelian_NoPO.Focus() : Exit Sub
                End If
            End If
            Dim cb As String = ""
            If arrJnsB_Byr.Item(CmbPembelian_JnsBayar.SelectedIndex) = "N" Then
                cb = "NULL"
            Else
                cb = "'" & arrCrByr.Item(CmbPembelian_CaraBayar.SelectedIndex) & "'"
            End If

            SQL = "INSERT INTO EMI_Pembelian(Kode_Perusahaan,No_Faktur,Lokasi,Tanggal,Jam,"
            SQL = SQL & "No_PO,No_Nota,Jenis_Pembayaran,Tgl_Jatuh_Tempo,Cara_Bayar,Mata_Uang,"
            SQL = SQL & "Kurs,Total_MUA,Total_IDR,Grand_Sebelum_PPN,PPN,Nilai_PPN,Grand,UserID) "
            SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtPembelian_NoFaktur.Text & "',"
            SQL = SQL & "'" & CmbPembelian_Lokasi.Text & "',"
            SQL = SQL & "'" & Format(DtpPembelian_Tgl.Value, "yyyy-MM-dd") & "',"
            SQL = SQL & "'" & Format(CDate(tgl_skg), "HH:mm:ss") & "',"
            SQL = SQL & "'" & TxtPembelian_NoPO.Text & "','" & TxtPembelian_NoNota.Text & "',"
            SQL = SQL & "'" & arrJnsB_Byr.Item(CmbPembelian_JnsBayar.SelectedIndex) & "',"
            If CmbPembelian_JnsBayar.SelectedIndex = 1 Then
                SQL = SQL & "'" & Format(DtpPembelian_TglBayar.Value, "yyyy-MM-dd") & "',"
            Else
                SQL = SQL & "null,"
            End If
            SQL = SQL & "" & cb & ","
            SQL = SQL & "'" & CmbPembelian_MataUang.Text & "',"
            SQL = SQL & "'" & HilangkanTanda(TxtPembelian_Kurs.Text) & "',"
            SQL = SQL & "'" & HilangkanTanda(TxtPembelian_TotalMUA.Text) & "',"
            SQL = SQL & "'" & HilangkanTanda(TxtPembelian_TotalIDR.Text) & "',"
            SQL = SQL & "'" & HilangkanTanda(TxtPembelian_TotalSblmPPN.Text) & "',"
            SQL = SQL & "'" & HilangkanTanda(TxtPembelian_PersenPPN.Text) & "',"
            SQL = SQL & "'" & HilangkanTanda(TxtPembelian_NilaiPPN.Text) & "',"
            SQL = SQL & "'" & HilangkanTanda(TxtPembelian_GrandTotal.Text) & "',"
            SQL = SQL & "'" & UserID & "')"
            ExecuteTrans(SQL)

            For a As Integer = 0 To LvPembelian_DataPembelian.Items.Count - 1
                Get_Isi_Listview(a)
                SQL = "INSERT INTO EMI_Pembelian_Detail(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,"
                SQL = SQL & "Kode_Barang,Serial_Number,Harga,Jumlah,Satuan,Disc_Persen,Nilai_Disc,"
                SQL = SQL & "Total,Pakai_SN,Modal,Upd_HPP,Sisa,Total_Harga) VALUES("
                SQL = SQL & "'" & KodePerusahaan & "','" & TxtPembelian_NoFaktur.Text & "',"
                SQL = SQL & "'" & LvSo & "','" & LvKd_Brg & "','" & LvSerial & "',"
                SQL = SQL & "'" & HilangkanTanda(LvHarga) & "','" & HilangkanTanda(LvJumlah) & "',"
                SQL = SQL & "'" & LvSatuan & "','" & HilangkanTanda(LvDisc_Persen) & "',"
                SQL = SQL & "'" & HilangkanTanda(LvDisc_Rp) & "','" & HilangkanTanda(LvTotal) & "',"
                SQL = SQL & "'" & LvPakai_SN & "','" & HilangkanTanda(LvModal) & "',"
                SQL = SQL & "'" & HilangkanTanda(LvUpd_Hpp) & "','" & HilangkanTanda(LvSisa) & "',"
                SQL = SQL & "'" & HilangkanTanda(LvTtl_Harga) & "')"
                ExecuteTrans(SQL)
            Next

            SQL = "update EMI_Pembelian_PO set Pakai = 'Y',flag_Pembelian='Y',Selesai='Y' where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & TxtPembelian_NoPO.Text & "'"
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Kosong()
        EMI_PO_Pembelian_Display2.Cari("Y")
        Me.Close()
    End Sub

    Private Sub BtnPembelian_Refresh_Click(sender As Object, e As EventArgs) Handles BtnPembelian_Refresh.Click
        Kosong()
    End Sub

    Private Sub BtnPembelian_CariPO_Click(sender As Object, e As EventArgs) Handles BtnPembelian_CariPO.Click
        SD_Pilih_PO2.asal = Jenis
        SD_Pilih_PO2.filter_tambahan = " and a.Flag_Timbang_Keluar = 'Y' and a.lokasi =  '" & CmbPembelian_Lokasi.Text & "' "
        SD_Pilih_PO2.ShowDialog()
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

    Public Sub TxtPembelian_NoPO_Leave(sender As Object, e As EventArgs) Handles TxtPembelian_NoPO.Leave
        Try
            OpenConn()

            LvPembelian_DataPembelian.Items.Clear()


            SQL = "Select a.No_PO,a.Kode_Stock_Owner,a.Kode_Barang,b.Nama,a.Jumlah,a.Jumlah_Barang, "
            SQL = SQL & "(Jumlah_BM+Qty_PenyelesaianPlus - Qty_PenyelesaianMin) As Jumlah_Masuk, a.Satuan_barang as satuan, c.Harga_barang as Harga, f.Satuan as Satuan_display "
            SQL = SQL & "From EMI_Pembelian_Loading_Detail a, Barang b, EMI_Pembelian_PO_Detail c, EMI_Pembelian_Selisih_Barang_Masuk_Det d, "
            SQL = SQL & "EMI_Pembelian_Selisih_Barang_Masuk e, Barang_Detail_Satuan f Where a.Kode_Perusahaan = b.Kode_Perusahaan And "
            SQL = SQL & "a.Kode_Stock_Owner = b.Kode_Stock_Owner And a.Kode_Barang = b.Kode_Barang And a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "And a.Urut_PO = c.No_Urut And d.urut_loading=a.urut_oto And d.Kode_Barang=a.Kode_Barang And d.Kode_Stock_Owner=a.Kode_Stock_Owner "
            SQL = SQL & "And d.Kode_Perusahaan=a.Kode_Perusahaan And  d.No_Faktur=e.no_faktur And d.Kode_Perusahaan=e.kode_perusahaan And e.status Is null "
            SQL = SQL & "And b.Kode_Barang=f.Kode_barang And b.Kode_Perusahaan=f.Kode_Perusahaan And f.Flag_Tampil_Display='Y' "
            SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & TxtPembelian_NoPO.Text & "' and e.Flag_Validasi='Y' "
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For index = 0 To .Rows.Count - 1

                        Dim harga As Double = Ubah_Satuan(.Rows(index).Item("Kode_Barang"), .Rows(index).Item("Harga"), .Rows(index).Item("Satuan"), .Rows(index).Item("satuan_display"), "UANG")
                        Dim jumlahkirim As Double = Ubah_Satuan(.Rows(index).Item("Kode_Barang"), .Rows(index).Item("Jumlah_Barang"), .Rows(index).Item("Satuan"), .Rows(index).Item("satuan_display"), "MASA")
                        Dim Jumlahmasuk As Double = Ubah_Satuan(.Rows(index).Item("Kode_Barang"), .Rows(index).Item("Jumlah_Masuk"), .Rows(index).Item("Satuan"), .Rows(index).Item("satuan_display"), "MASA")

                        Dim Lvw As ListViewItem
                        Lvw = LvPembelian_DataPembelian.Items.Add(.Rows(index).Item("No_PO"))
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
                    Next
                End With
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        HitungGrandTotal()
    End Sub

End Class
