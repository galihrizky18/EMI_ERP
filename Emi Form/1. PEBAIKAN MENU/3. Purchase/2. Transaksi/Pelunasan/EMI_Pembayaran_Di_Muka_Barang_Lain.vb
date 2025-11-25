Imports System.Globalization

Public Class EMI_Pembayaran_Di_Muka_Barang_Lain
    Dim Jenis = "Display_Production_Order"
    Public fno_po As String

    Dim arrInisialFaktur, arrIdRekening As New ArrayList

    Dim LvKode_So As String
    Dim LvKode_Bahan As String
    Dim LvNama_Bahan As String
    Dim LvNilai_Formula As String
    Dim LvNilai_Produksi As String
    Dim LvSatuan As String

    Dim CellKode_So As Integer = 0
    Dim CellKode_Bahan As Integer = 1
    Dim CellNama_Bahan As Integer = 2
    Dim CellNilai_Formula As Integer = 3
    Dim CellNilai_Produksi As Integer = 4
    Dim CellSatuan As Integer = 5

    Dim LvKode_So_Pckg As String
    Dim LvKode_Bahan_Pckg As String
    Dim LvNama_Bahan_Pckg As String
    Dim LvNilai_Formula_Pckg As String
    Dim LvNilai_Produksi_Pckg As String
    Dim LvSatuan_Pckg As String

    Dim CellKode_So_Pckg As Integer = 0
    Dim CellKode_Bahan_Pckg As Integer = 1
    Dim CellNama_Bahan_Pckg As Integer = 2
    Dim CellNilai_Formula_Pckg As Integer = 3
    Dim CellNilai_Produksi_Pckg As Integer = 4
    Dim CellSatuan_Pckg As Integer = 5
    Dim arrKodeBankTujuan, arrRekeningTujuan, arrNamaPemilikiRekTujuan, arrKotaTujuan, arrNegaraTujuan As New ArrayList

    Dim no_fakturPengajuan As String

    Private Sub Transaksi_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        kosong()

        LvSupplier.Location = New Point(120, 53)
    End Sub

    Private Sub kosong()

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan

            CmbLokasi.Items.Clear() : arrInisialFaktur.Clear()
            SQL = "select Kode_Stock_Owner, persediaan ,inisial_faktur from Stock_Owner where kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' order by Kode_Stock_Owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbLokasi.Items.Add(dr("Kode_Stock_Owner")) : arrInisialFaktur.Add(dr("inisial_faktur"))
                Loop
            End Using

            CmbMUA.Items.Clear()
            SQL = "select Kode_Mata_uang from Mata_Uang where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_Mata_Uang"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    CmbMUA.Items.Add(Dr("Kode_Mata_uang"))
                Loop
            End Using

            'CmbRekening.Items.Clear() : arrIdRekening.Clear()
            'SQL = "select Id_Bank, Keterangan from EMI_Bank   where kode_perusahaan = '" & KodePerusahaan & "' "
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read
            '        CmbRekening.Items.Add(dr("Keterangan")) : arrIdRekening.Add(dr("Id_Bank"))
            '    Loop
            'End Using

            CmbLokasi.Text = Lokasi

            get_no_faktur()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        TxtKodeSupplier.Text = ""
        TxtNamaSupplier.Text = ""
        CmbNoPO.Items.Clear()
        CbNoFaktur.Checked = False
        Chk_Persen.Checked = False
        TxtNilai.Text = ""
        CmbMUA.Enabled = True
        TxtKurs.Text = ""
        TxtTotalIDR.Text = ""
        TxtKeterangan.Text = ""
        Txt_JmlhPO.Text = ""
        Txt_NilaiDPP.Text = ""

        Txt_PPNPersen.Text = ""
        Txt_PPN.Text = ""
        Txt_PPHPersen.Text = ""
        Txt_PPH.Text = ""

        Txt_DPPO.Text = ""
        Txt_DPPO.Visible = False
        Chk_Persen.Enabled = False
        Txt_Persen.Enabled = False
        Cmb_Persen.Enabled = False

        CmbRekening.Items.Clear() : arrRekeningTujuan.Clear() : arrKodeBankTujuan.Clear() : arrNamaPemilikiRekTujuan.Clear() : arrKotaTujuan.Clear() : arrNegaraTujuan.Clear()

    End Sub

    Private Sub get_no_faktur()

        TxtFakturPembayaran.Text = fDownPay & arrInisialFaktur.Item(CmbLokasi.SelectedIndex) & "-" & Format(Dtp1.Value, "MMyy") & "-" &
                            General_Class.Get_Last_Number2("EMI_Transaksi_Pembayaran_Dimuka_Asset", "no_transaksi", JumlahDigit,
                            "Kode_perusahaan", KodePerusahaan,
                            "And", "substring(no_transaksi, 1, " & Len(fDownPay) + Len(arrInisialFaktur.Item(CmbLokasi.SelectedIndex)) + 5 & ")", fDownPay & arrInisialFaktur.Item(CmbLokasi.SelectedIndex) & "-" & Format(Dtp1.Value, "MMyy"))
    End Sub

    Private Sub Get_No_Faktur_Pengajuan()
        no_fakturPengajuan = fPengajuanTemp & Format(Dtp1.Value, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Pengajuan_temp", "No_Pengajuan", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Pengajuan, 1, " & Len(fPengajuanTemp) + 4 & ")", fPengajuanTemp & Format(Dtp1.Value, "MMyy"))
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If TxtFakturPembayaran.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Error_No_Transaksi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtFakturPembayaran.Focus() : Exit Sub
        ElseIf TxtKodeSupplier.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Supplier harus di isi!", Judul, MessageBoxButtons.OK)
            TxtKodeSupplier.Focus()
            Exit Sub

        ElseIf TxtNilai.Text.Trim.Length = 0 Then
            MessageBox.Show("Nilai harus di isi!", Judul, MessageBoxButtons.OK)
            TxtNilai.Focus()
            Exit Sub
        ElseIf CmbMUA.Text.Trim.Length = 0 Then
            MessageBox.Show("Mata Uang harus di isi!", Judul, MessageBoxButtons.OK)
            CmbMUA.Focus()
            Exit Sub
        ElseIf TxtKurs.Text.Trim.Length = 0 Then
            MessageBox.Show("Kurs harus di isi!", Judul, MessageBoxButtons.OK)
            TxtKurs.Focus()
            Exit Sub
        ElseIf TxtKeterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan harus di isi!", Judul, MessageBoxButtons.OK)
            TxtKeterangan.Focus()
            Exit Sub
        End If

        If CbNoFaktur.Checked Then
            If CmbNoPO.Text.Trim.Length = 0 Then
                MessageBox.Show("NO PO harus di isi!", Judul, MessageBoxButtons.OK)
                TxtKodeSupplier.Focus()
                Exit Sub
            End If

            If Val(HilangkanTanda(TxtNilai.Text)) > Val(HilangkanTanda(Txt_JmlhPO.Text)) Then
                MessageBox.Show("Nilai Tidak Boleh Lebih Besar dari Nilai PO", Judul, MessageBoxButtons.OK)
                TxtNilai.Focus()
                Exit Sub
            End If
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekSudahTutupSaldo(Dtp1.Value) = "Y" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Sudah tutup saldo di bulan ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            get_no_faktur()

            Dim noFakturPO_Fix As String = ""
            Dim FlagCB As String = ""
            Dim Nilai As String = ""

            If CbNoFaktur.Checked = True Then
                Nilai = TxtNilai.Text
                FlagCB = "'Y'"
                noFakturPO_Fix = "'" & CmbNoPO.Text & "'"
            Else
                Nilai = 0
                FlagCB = "NULL"
                noFakturPO_Fix = "NULL"
            End If

            Dim Akun_DP As String = ""
            SQL = "select Akun_DP_Asset "
            SQL = SQL & "from stock_owner "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Lokasi & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Akun_DP = Dr("Akun_DP_Asset")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Get_No_Faktur_Pengajuan()

            'KODE LAMA
            'SQL = "INSERT INTO pengajuan_Temp(kode_perusahaan, no_pengajuan, tanggal, jam, keterangan, userid, grand, pbk, "
            'SQL = SQL & "Validasi, Jenis_Asal) "
            'SQL = SQL & "values('" & KodePerusahaan & "', '" & no_fakturPengajuan & "', '" & Format(Dtp1.Value, "yyyy-MM-dd") & "', "
            'SQL = SQL & "'" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & TxtKeterangan.Text & "', '" & UserID & "', "
            'SQL = SQL & "" & HilangkanTanda(TxtTotalIDR.Text) & ", 'T', NULL, 'ASSET')"
            'ExecuteTrans(SQL)

            Dim NilaiTranfer As Double = HilangkanTanda(TxtTotalIDR.Text) - Val(HilangkanTanda(Txt_PPH.Text))

            SQL = "INSERT INTO pengajuan_Temp(kode_perusahaan, no_pengajuan, tanggal, jam, keterangan, userid, grand, pbk, "
            SQL = SQL & "Validasi, Jenis_Asal) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & no_fakturPengajuan & "', '" & Format(Dtp1.Value, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & TxtKeterangan.Text & "', '" & UserID & "', "
            SQL = SQL & "" & HilangkanTanda(NilaiTranfer) & ", 'T', NULL, 'ASSET')"
            ExecuteTrans(SQL)

            SQL = "insert into EMI_Transaksi_Pembayaran_Dimuka_Asset(Kode_Perusahaan,No_Transaksi, Tanggal, Jam, UserID, "
            SQL = SQL & "Kode_Supplier, id_rekening, Nilai, flag_po, No_Rek_Tujuan, Mata_Uang, Kurs, Total_IDR, No_Pengajuan, Keterangan) values ( "
            SQL = SQL & "'" & KodePerusahaan & "' , '" & TxtFakturPembayaran.Text.Trim & "', '" & Format(Dtp1.Value, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "', '" & TxtKodeSupplier.Text.Trim & "', "
            SQL = SQL & "NULL, '" & HilangkanTanda(TxtNilai.Text) & "', " & FlagCB & ", '" & arrRekeningTujuan.Item(CmbRekening.SelectedIndex) & "', "
            SQL = SQL & "'" & CmbMUA.Text & "', '" & HilangkanTanda(TxtKurs.Text) & "', '" & HilangkanTanda(TxtTotalIDR.Text) & "', "
            SQL = SQL & "'" & no_fakturPengajuan & "', '" & TxtKeterangan.Text & "') "
            ExecuteTrans(SQL)

            '''INSERT KE DETAIL PENGAJUAN TEMP
            'SQL = "INSERT INTO detail_pengajuan_Temp(kode_perusahaan, no_pengajuan, kode_master_acc, kode_acc, kode_detail_acc, "
            'SQL = SQL & "keterangan_detail, tgl_jatuh_tempo, jumlah, kode_bank_tujuan, no_rek_tujuan, nama_penerima, "
            'SQL = SQL & "Alamat_Penerima, Kota_Penerima, Negara_Penerima, Telp_Penerima, Lokasi, Kode_Account, "
            'SQL = SQL & "Id_Cost_Center, No_Pelunasan, Urut_Pelunasan, Tgl_Bayar, Flag_Pengajuan, Flag_Pelunasan) "
            'SQL = SQL & "values('" & KodePerusahaan & "', '" & no_fakturPengajuan & "', '" & Strings.Left(Akun_DP, 1) & "', "
            'SQL = SQL & "'" & Strings.Mid(Akun_DP, 2, 1) & "', '" & Strings.Mid(Ganti(Akun_DP), 3) & "', "
            'SQL = SQL & "'" & TxtKeterangan.Text & "', '" & Format(Dtp1.Value, "yyyy-MM-dd") & "', " & HilangkanTanda(TxtTotalIDR.Text) & ", "
            'SQL = SQL & "'" & arrKodeBankTujuan.Item(CmbRekening.SelectedIndex) & "', '" & arrRekeningTujuan.Item(CmbRekening.SelectedIndex) & "', '" & arrNamaPemilikiRekTujuan.Item(CmbRekening.SelectedIndex) & "', "
            'SQL = SQL & "'-', '" & arrKotaTujuan.Item(CmbRekening.SelectedIndex) & "', '" & arrNegaraTujuan.Item(CmbRekening.SelectedIndex) & "', '-','" & CmbLokasi.Text & "','" & Akun_DP & "', "
            'SQL = SQL & "'0', '" & TxtFakturPembayaran.Text.Trim & "', NULL, '" & Format(Dtp1.Value, "yyyy-MM-dd") & "', 'Y', 'Y') "
            'ExecuteTrans(SQL)

            SQL = "INSERT INTO detail_pengajuan_Temp(kode_perusahaan, no_pengajuan, kode_master_acc, kode_acc, kode_detail_acc, "
            SQL = SQL & "keterangan_detail, tgl_jatuh_tempo, jumlah, kode_bank_tujuan, no_rek_tujuan, nama_penerima, "
            SQL = SQL & "Alamat_Penerima, Kota_Penerima, Negara_Penerima, Telp_Penerima, Lokasi, Kode_Account, "
            SQL = SQL & "Id_Cost_Center, No_Pelunasan, Urut_Pelunasan, Tgl_Bayar, Flag_Pengajuan, Flag_Pelunasan) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & no_fakturPengajuan & "', '" & Strings.Left(Akun_DP, 1) & "', "
            SQL = SQL & "'" & Strings.Mid(Akun_DP, 2, 1) & "', '" & Strings.Mid(Ganti(Akun_DP), 3) & "', "
            SQL = SQL & "'" & TxtKeterangan.Text & "', '" & Format(Dtp1.Value, "yyyy-MM-dd") & "', " & HilangkanTanda(NilaiTranfer) & ", "
            SQL = SQL & "'" & arrKodeBankTujuan.Item(CmbRekening.SelectedIndex) & "', '" & arrRekeningTujuan.Item(CmbRekening.SelectedIndex) & "', '" & arrNamaPemilikiRekTujuan.Item(CmbRekening.SelectedIndex) & "', "
            SQL = SQL & "'-', '" & arrKotaTujuan.Item(CmbRekening.SelectedIndex) & "', '" & arrNegaraTujuan.Item(CmbRekening.SelectedIndex) & "', '-','" & CmbLokasi.Text & "','" & Akun_DP & "', "
            SQL = SQL & "'0', '" & TxtFakturPembayaran.Text.Trim & "', NULL, '" & Format(Dtp1.Value, "yyyy-MM-dd") & "', 'Y', 'Y') "
            ExecuteTrans(SQL)

            Dim x_no_urut_det_pengajuan As String = ""
            SQL = "select IDENT_CURRENT('detail_pengajuan_Temp') as urutan"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    x_no_urut_det_pengajuan = Dr("urutan")
                End If
            End Using

            SQL = "select urut from detail_pengajuan_Temp where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "no_pengajuan = '" & no_fakturPengajuan & "' and urut = '" & x_no_urut_det_pengajuan & "'"
            Using Dr = OpenTrans(SQL)
                If Not (Dr.Read) Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Harap ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '============================
            '=     INSERT JURNAL DP     =
            '============================
            SQL = "insert into Detail_Pengajuan5_Temp(Kode_Perusahaan,Urut_Detail_Pengajuan, Kode_Account,Debit, Kredit, Keterangan,Tgl, lokasi)"
            SQL = SQL & "values('" & KodePerusahaan & "', '" & x_no_urut_det_pengajuan & "', '" & Akun_DP & "', '" & HilangkanTanda(TxtTotalIDR.Text) & "','0', "
            SQL = SQL & "'Pembayaran Di Muka " & TxtFakturPembayaran.Text.Trim & ";" & TxtKeterangan.Text.Trim & "', '" & Format(Dtp1.Value, "yyyy-MM-dd") & "', '" & Lokasi & "')"
            ExecuteTrans(SQL)


            '=============================
            '=     INSERT JURNAL PPH     =
            '=============================
            If CbNoFaktur.Checked Then
                SQL = "select No_Faktur, Persentase, Kode_Tarif, Kode_Akun, Flag_PPN from EMI_Detail_PPH_PO_Induk_Barang_Lain  "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & CmbNoPO.Text & "' "
                SQL = SQL & "order by No_Faktur "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1

                                Dim NilaiValue As Double = Val(HilangkanTanda(Format(Val(HilangkanTanda(Txt_NilaiDPP.Text)) * (Val(HilangkanTanda(.Rows(i).Item("Persentase"))) / 100), "N0")))

                                If General_Class.CekNULL(.Rows(i).Item("Flag_PPN")) = "Y" Then

                                    SQL = "insert into EMI_Transaksi_Pembayaran_Dimuka_Asset_Pajak (Kode_Perusahaan, No_Faktur_Induk, No_Faktur, Persentase, Nilai, Kode_Tarif, Flag_PPN, Kode_Akun) "
                                    SQL = SQL & "values ('" & KodePerusahaan & "', '" & CmbNoPO.Text & "', '" & TxtFakturPembayaran.Text & "', '" & .Rows(i).Item("Persentase") & "', "
                                    SQL = SQL & "'" & HilangkanTanda(NilaiValue) & "', '" & .Rows(i).Item("Kode_Tarif") & "', 'Y', '" & .Rows(i).Item("Kode_Akun") & "') "
                                    ExecuteTrans(SQL)

                                ElseIf General_Class.CekNULL(.Rows(i).Item("Flag_PPN")) = "" Then

                                    SQL = "insert into EMI_Transaksi_Pembayaran_Dimuka_Asset_Pajak (Kode_Perusahaan, No_Faktur_Induk, No_Faktur, Persentase, Nilai, Kode_Tarif, Flag_PPN, Kode_Akun) "
                                    SQL = SQL & "values ('" & KodePerusahaan & "', '" & CmbNoPO.Text & "', '" & TxtFakturPembayaran.Text & "', '" & .Rows(i).Item("Persentase") & "', "
                                    SQL = SQL & "'" & HilangkanTanda(NilaiValue) & "', '" & .Rows(i).Item("Kode_Tarif") & "', NULL, '" & .Rows(i).Item("Kode_Akun") & "') "
                                    ExecuteTrans(SQL)

                                    SQL = "insert into Detail_Pengajuan5_Temp(Kode_Perusahaan, Urut_Detail_Pengajuan, Kode_Account,Debit, Kredit, Keterangan, Tgl, lokasi)"
                                    SQL = SQL & "values('" & KodePerusahaan & "', '" & x_no_urut_det_pengajuan & "', '" & .Rows(i).Item("Kode_Akun") & "', '0','" & HilangkanTanda(NilaiValue) & "', "
                                    SQL = SQL & "'Pembayaran " & .Rows(i).Item("Kode_Tarif") & " " & TxtFakturPembayaran.Text.Trim & ";" & TxtKeterangan.Text.Trim & "', '" & Format(Dtp1.Value, "yyyy-MM-dd") & "', '" & Lokasi & "')"
                                    ExecuteTrans(SQL)

                                End If



                            Next
                        End If
                    End With
                End Using


            End If

            'insert ke binding/ detail
            If CbNoFaktur.Checked = True Then

                Dim PersenDP As String = "NULL"
                If Chk_Persen.Checked Then
                    PersenDP = "'" & HilangkanTanda(Txt_Persen.Text) & "'"
                End If
                SQL = "insert into EMI_Transaksi_Pembayaran_Dimuka_Detail_Asset(Kode_Perusahaan,No_Transaksi,Tanggal, Jam,UserId,no_fak_po, Nilai, Nilai_IDR, Persen_DP) "
                SQL = SQL & "values ( "
                SQL = SQL & "'" & KodePerusahaan & "' , '" & TxtFakturPembayaran.Text.Trim & "', '" & Format(Dtp1.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "', " & noFakturPO_Fix & ", "
                SQL = SQL & "'" & HilangkanTanda(Nilai) & "', '" & HilangkanTanda(TxtTotalIDR.Text) & "', " & PersenDP & " )"
                ExecuteTrans(SQL)
            End If


            '======================
            '=     CEK JURNAL     =
            '======================
            SQL = "select grand from pengajuan_Temp where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Pengajuan = '" & no_fakturPengajuan & "' and UserID = '" & UserID & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        If .Rows.Count > 1 Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terdapat Kesahalan Pada Jurnal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub

                        Else
                            Dim NilaiTransfer As Double = Val(HilangkanTanda(.Rows(0).Item("grand")))
                            Dim NilaiJurnal As Double = 0

                            SQL = "select round(sum(debit), 2) - round(sum(kredit), 2) as Nilai_jurnal "
                            SQL = SQL & "from Detail_Pengajuan5_Temp where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "Urut_Detail_Pengajuan = '" & x_no_urut_det_pengajuan & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    NilaiJurnal = Val(HilangkanTanda(Dr("Nilai_jurnal")))
                                End If
                            End Using


                            If NilaiTransfer <> NilaiJurnal Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terdapat Kesahalan Pada Jurnal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If

                        End If
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terdapat Kesahalan Pada Jurnal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub

                    End If
                End With
            End Using

            'akhir tutup

            Cmd.Transaction.Commit()
            MessageBox.Show("Data berhasil disimpan ", Judul, MessageBoxButtons.OK)

            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosong()

    End Sub

    Private Sub Transaksi_Produksi_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox9_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TxtKodeSupplier.TextChanged
        If TxtKodeSupplier.Text.Trim.Length = 0 Then
            LvSupplier.Visible = False : Exit Sub
        Else
            LvSupplier.Visible = True
        End If

        LvSupplier.Items.Clear()
        Dim lv As New ListViewItem

        Try

            OpenConn()

            SQL = "Select b.kode_supplier, b.nama "
            SQL = SQL & "from suppliers b "
            SQL = SQL & "where  "
            SQL = SQL & "b.kode_perusahaan = '" & KodePerusahaan & "' and b.kode_supplier like '%" & TxtKodeSupplier.Text & "%' "

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
                    lv = LvSupplier.Items.Add(Dr("kode_supplier"))
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

    Private Sub LvSupplier_KeyDown(sender As Object, e As KeyEventArgs) Handles LvSupplier.KeyDown
        If e.KeyCode = Keys.Enter Then
            LvSupplier_DoubleClick(LvSupplier, e)
        End If
    End Sub

    Private Sub CmbNoPO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbNoPO.SelectedIndexChanged
        If CmbNoPO.SelectedIndex = -1 Then
            Exit Sub
        End If

        Try
            OpenConn()

            'CmbNoPO.Items.Clear() : CmbNoPO.Enabled = True

            SQL = "select Mata_Uang, grand "
            SQL = SQL & "From EMI_Pembelian_PO_Induk_Barang_Lain a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.no_faktur = '" & CmbNoPO.Text & "' and a.status is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    CmbMUA.Text = Dr("Mata_Uang")
                    Txt_JmlhPO.Text = Format(Val(HilangkanTanda(Dr("grand"))), "N0")
                End If
            End Using

            '====================================
            '=     GET NILAI DP TERHADAP PO     =
            '====================================
            SQL = "select sum(b.nilai) as Jumlah_DP "
            SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka_Asset a, EMI_Transaksi_Pembayaran_Dimuka_Detail_Asset b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & "and b.No_Fak_PO = '" & CmbNoPO.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dim Nilai As Double = If(General_Class.CekNULL(Dr("Jumlah_DP")) = "", 0, General_Class.CekNULL(Dr("Jumlah_DP")))
                    Txt_DPPO.Text = Format(Nilai, "N0")
                End If
            End Using

            Chk_Persen.Checked = False
            Txt_Persen.Text = ""
            TxtNilai.Text = ""
            Txt_NilaiDPP.Text = ""
            Txt_PPNPersen.Text = ""
            Txt_PPN.Text = ""
            Txt_PPHPersen.Text = ""
            Txt_PPH.Text = ""
            TxtKurs.Text = ""
            TxtTotalIDR.Text = ""

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub TxtNilai_TextChanged(sender As Object, e As EventArgs) Handles TxtNilai.TextChanged
        If TxtNilai.Text.Trim.Length = 0 Then Exit Sub

        If CbNoFaktur.Checked Then
            If Val(HilangkanTanda(TxtNilai.Text)) > Val(HilangkanTanda(Txt_JmlhPO.Text)) Then
                TxtNilai.Text = ""
                Txt_PPNPersen.Text = ""
                Txt_PPN.Text = ""
                Txt_PPHPersen.Text = ""
                Txt_PPH.Text = ""
                Txt_NilaiDPP.Text = ""
                Exit Sub
            End If

            If Val(HilangkanTanda(TxtNilai.Text)) > (Val(HilangkanTanda(Txt_JmlhPO.Text)) - Val(HilangkanTanda(Txt_DPPO.Text))) Then
                MessageBox.Show("Nilai Melebihi Sisa DP Terhadap PO")
                TxtNilai.Text = ""
                Txt_PPNPersen.Text = ""
                Txt_PPN.Text = ""
                Txt_PPHPersen.Text = ""
                Txt_PPH.Text = ""
                Txt_NilaiDPP.Text = ""
                TxtNilai.Focus()
                Exit Sub
            End If
        End If

        HitungPPNPPH(CmbNoPO.Text, TxtNilai.Text)

        If TxtNilai.Text.Trim.Length = 0 Or TxtKurs.Text.Trim.Length = 0 Then
            TxtTotalIDR.Text = 0
            Exit Sub
        End If

        TxtTotalIDR.Text = Format(Val(HilangkanTanda(TxtNilai.Text)) * Val(TxtKurs.Text), "N0")

    End Sub

    Private Sub LvSupplier_DoubleClick(sender As Object, e As EventArgs) Handles LvSupplier.DoubleClick
        If LvSupplier.Items.Count = 0 Then Exit Sub

        Dim Kode As String = LvSupplier.FocusedItem.Text
        Dim Nama As String = LvSupplier.FocusedItem.SubItems(1).Text

        TxtKodeSupplier.Text = Kode
        TxtNamaSupplier.Text = Nama

        LvSupplier.Visible = False
        CbNoFaktur.Focus()

        TxtKodeSupplier_Leave(LvSupplier, e)

    End Sub

    Private Sub TxtKurs_TextChanged(sender As Object, e As EventArgs) Handles TxtKurs.TextChanged
        If TxtNilai.Text.Trim.Length = 0 Or TxtKurs.Text.Trim.Length = 0 Then
            TxtTotalIDR.Text = 0
            Exit Sub
        End If

        TxtTotalIDR.Text = Format(Val(HilangkanTanda(TxtNilai.Text)) * Val(TxtKurs.Text), "N0")
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CbNoFaktur.CheckedChanged
        Txt_JmlhPO.Text = ""
        If CbNoFaktur.Checked = True Then

            Try
                OpenConn()

                CmbNoPO.Items.Clear() : CmbNoPO.Enabled = True

                SQL = ";with cte as ( "
                SQL = SQL & "select a.Kode_Perusahaan, a.No_Faktur, "
                SQL = SQL & "isnull((select Top 1 'Y' from EMI_Transaksi_Pembayaran_Dimuka_Asset x, EMI_Transaksi_Pembayaran_Dimuka_Detail_Asset y where "
                SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_transaksi = y.no_transaksi  "
                SQL = SQL & "and a.Kode_Perusahaan = y.Kode_Perusahaan and a.No_Faktur = y.No_Fak_PO and x.status is null "
                SQL = SQL & "), null) as Flag "
                SQL = SQL & "From  EMI_Pembelian_PO_Induk_Barang_Lain a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Supplier = '" & TxtKodeSupplier.Text & "' and a.status is null "
                SQL = SQL & ") select * From cte where kode_perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and flag is null "
                SQL = SQL & "order by no_faktur"
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        CmbNoPO.Items.Add(Dr("no_faktur"))
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            Txt_DPPO.Text = ""
            Txt_DPPO.Visible = True

            CmbMUA.Enabled = False
            Chk_Persen.Enabled = True
        Else
            CmbMUA.Enabled = True
            CmbNoPO.Items.Clear()
            Txt_DPPO.Visible = False : Txt_DPPO.Text = ""
            CmbNoPO.Enabled = False
            Chk_Persen.Enabled = False
            Txt_Persen.Text = "" : Cmb_Persen.Items.Clear()
        End If
    End Sub

    Private Sub TxtNamaSupplier_TextChanged(sender As Object, e As EventArgs) Handles TxtNamaSupplier.TextChanged
        If TxtNamaSupplier.Text.Trim.Length = 0 Then
            LvSupplier.Visible = False : Exit Sub
        Else
            LvSupplier.Visible = True
        End If

        LvSupplier.Items.Clear()
        Dim lv As New ListViewItem

        Try

            OpenConn()

            SQL = "Select b.kode_supplier, b.nama "
            SQL = SQL & "from suppliers b "
            SQL = SQL & "where  "
            SQL = SQL & "b.kode_perusahaan = '" & KodePerusahaan & "' and b.nama like '%" & TxtNamaSupplier.Text & "%' "

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
                    lv = LvSupplier.Items.Add(Dr("kode_supplier"))
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

    Private Sub BtnFormulator_Refresh_Click(sender As Object, e As EventArgs) Handles BtnFormulator_Refresh.Click
        kosong()
    End Sub

    Private Sub TxtKodeSupplier_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtKodeSupplier.KeyDown
        If e.KeyCode = Keys.Down Then
            LvSupplier.Focus()
        End If
    End Sub

    Private Sub TxtKodeSupplier_Leave(sender As Object, e As EventArgs) Handles TxtKodeSupplier.Leave
        If TxtKodeSupplier.Text.Trim.Length = 0 Then Exit Sub
        If LvSupplier.Focused = True Then Exit Sub

        Dim ada_data As String = "T"
        Try
            OpenConn()

            SQL = "SELECT Kode_Supplier, nama FROM suppliers WHERE Kode_Perusahaan = '" & KodePerusahaan & "' and kode_supplier like '" & TxtKodeSupplier.Text & "%' ORDER BY nama"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TxtKodeSupplier.Text = Dr("kode_supplier")
                    TxtNamaSupplier.Text = Dr("nama")

                    ada_data = "Y"

                    CbNoFaktur.Focus()
                Else
                    CmbRekening.Items.Clear() : arrRekeningTujuan.Clear() : arrKodeBankTujuan.Clear() : arrNamaPemilikiRekTujuan.Clear() : arrKotaTujuan.Clear() : arrNegaraTujuan.Clear()

                    MessageBox.Show("Supplier tidak ditemukan . . ! !", Judul)

                    TxtKodeSupplier.Text = ""
                    TxtNamaSupplier.Text = ""
                    TxtKodeSupplier.Focus()
                End If
                LvSupplier.Visible = False
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try

        If ada_data = "Y" Then
            CbNoFaktur.Checked = False

            Try
                OpenConn()

                CmbRekening.Items.Clear() : arrRekeningTujuan.Clear() : arrKodeBankTujuan.Clear() : arrNamaPemilikiRekTujuan.Clear() : arrKotaTujuan.Clear() : arrNegaraTujuan.Clear()
                SQL = "select Nama_Pemilik, Nama_Bank, No_Rekening	, Kota_Pemilik, Negara_Pemilik "
                SQL = SQL & "from Rekening_Suppliers "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Supplier = '" & TxtKodeSupplier.Text & "' "
                SQL = SQL & "and status is null "

                Using Dr2 = OpenTrans(SQL)
                    Do While Dr2.Read

                        Dim TemplateView As String = $"{Dr2("Nama_Pemilik")}-{Dr2("Nama_Bank")}-{Dr2("No_Rekening")}"
                        CmbRekening.Items.Add(TemplateView)

                        arrKodeBankTujuan.Add(Dr2("Nama_Bank"))
                        arrRekeningTujuan.Add(Dr2("No_Rekening"))
                        arrNamaPemilikiRekTujuan.Add(Dr2("Nama_Pemilik"))
                        arrKotaTujuan.Add(Dr2("Kota_Pemilik"))
                        arrNegaraTujuan.Add(Dr2("Negara_Pemilik"))

                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub

            End Try
        End If
    End Sub

    Private Sub TxtNilai_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtNilai.KeyPress
        'If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
        If Not (Char.IsDigit(e.KeyChar) OrElse e.KeyChar = ChrW(8) OrElse e.KeyChar = "."c) Then e.Handled = True
        If e.KeyChar = Chr(13) Then TxtKurs.Focus()

    End Sub

    Private Sub TxtKurs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKurs.KeyPress
        'If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
        If Not (Char.IsDigit(e.KeyChar) OrElse e.KeyChar = ChrW(8) OrElse e.KeyChar = "."c) Then e.Handled = True
        If e.KeyChar = Chr(13) Then CmbRekening.Focus()
    End Sub

    Private Sub Txt_Persen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Persen.KeyPress

        'If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
        If Not (Char.IsDigit(e.KeyChar) OrElse e.KeyChar = ChrW(8) OrElse e.KeyChar = "."c) Then e.Handled = True
        If e.KeyChar = Chr(13) Then TxtKurs.Focus()

    End Sub

    Private Sub TxtKodeSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKodeSupplier.KeyPress

        If e.KeyChar = Chr(13) Then
            If TxtKodeSupplier.Text.Trim.Length = 0 Then TxtKodeSupplier.Focus()
            TxtKodeSupplier_Leave(TxtKodeSupplier, e)

            LvSupplier.Visible = False
        End If

    End Sub

    Private Sub TxtNilai_Leave(sender As Object, e As EventArgs) Handles TxtNilai.Leave

        '======================
        '=     SET FORMAT     =
        '======================
        Dim culture As CultureInfo = CultureInfo.CurrentCulture

        If Not TxtNilai.Text.Trim.Length = 0 Then
            Dim cellKuantity As String = TxtNilai.Text

            If cellKuantity = "" Then
                Exit Sub
            End If

            Dim nilai As Decimal = Decimal.Parse(cellKuantity)
            Dim formattedValue As String = nilai.ToString("N0", culture)

            TxtNilai.Text = formattedValue

        End If
    End Sub

    Private Sub TxtNilai_Enter(sender As Object, e As EventArgs) Handles TxtNilai.Enter
        '======================
        '=     SET FORMAT     =
        '======================

        If Not TxtNilai.Text.Trim.Length = 0 Then
            Dim cellKuantity As String = TxtNilai.Text

            If cellKuantity = "" Then
                Exit Sub
            End If

            Dim cleanedStr As String = HilangkanTanda(cellKuantity) ' Menghapus titik
            Dim nilai As Decimal = Decimal.Parse(cleanedStr)

            TxtNilai.Text = nilai
        End If

    End Sub

    Private Sub TxtNamaSupplier_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtNamaSupplier.KeyDown
        If e.KeyCode = Keys.Down Then
            LvSupplier.Focus()
        End If
    End Sub

    Private Sub CbNoFaktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CbNoFaktur.KeyPress
        If e.KeyChar = Chr(13) Then TxtNilai.Focus()
    End Sub

    Private Sub CmbNoPO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbNoPO.KeyPress
        If e.KeyChar = Chr(13) Then Chk_Persen.Focus()
    End Sub

    Private Sub Txt_JmlhPO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_JmlhPO.KeyPress
        If e.KeyChar = Chr(13) Then Chk_Persen.Focus()
    End Sub

    Private Sub Chk_Persen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_Persen.KeyPress
        If e.KeyChar = Chr(13) Then TxtNilai.Focus()
    End Sub

    Private Sub CmbRekening_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbRekening.KeyPress
        If e.KeyChar = Chr(13) Then TxtKeterangan.Focus()
    End Sub

    Private Sub TxtKeterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKeterangan.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

    Private Sub S(sender As Object, e As EventArgs) Handles CmbLokasi.SelectedIndexChanged

    End Sub

    Private Sub TxtNamaSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtNamaSupplier.KeyPress
        If e.KeyChar = Chr(13) Then
            If TxtKodeSupplier.Text.Trim.Length = 0 Then TxtNamaSupplier.Text = "" : LvSupplier.Visible = False ': Exit Sub
            TxtNilai.Focus()
        End If
    End Sub

    Private Sub TxtNamaSupplier_Leave(sender As Object, e As EventArgs) Handles TxtNamaSupplier.Leave
        If LvSupplier.Focused = True Then Exit Sub
        If TxtNamaSupplier.ReadOnly = True Then Exit Sub
        TxtKodeSupplier.Text = "" : TxtNamaSupplier.Text = ""
    End Sub

    Private Sub Chk_Persen_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Persen.CheckedChanged
        If Chk_Persen.Checked = True Then

            Txt_Persen.Text = ""
            Txt_Persen.Enabled = True
            TxtNilai.Enabled = False
            TxtNilai.Text = ""
            Cmb_Persen.Text = ""
            Cmb_Persen.Items.Clear()
            Cmb_Persen.Items.Add("%")
            Cmb_Persen.SelectedIndex = 0
        Else

            Txt_Persen.Text = ""
            Txt_Persen.Enabled = False
            TxtNilai.Enabled = True
            TxtNilai.Text = ""
            Cmb_Persen.Text = ""
            Txt_PPNPersen.Text = ""
            Txt_PPN.Text = ""
            Txt_PPHPersen.Text = ""
            Txt_PPH.Text = ""
            Cmb_Persen.Items.Clear()

        End If
    End Sub

    Private Sub Txt_Persen_TextChanged(sender As Object, e As EventArgs) Handles Txt_Persen.TextChanged
        If Txt_Persen.Text.Trim.Length = 0 Then Exit Sub

        Dim jumlahPO As Double = HilangkanTanda(Txt_JmlhPO.Text)

        TxtNilai.Text = Format(jumlahPO * (Val(HilangkanTanda(Txt_Persen.Text)) / 100), "N0")

    End Sub

    Private Sub HitungPPNPPH(ByVal no_faktur As String, ByVal Nilai As Double)
        If no_faktur.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            Dim PersenPPN As Double = 0
            Dim PersenPPH As Double = 0
            Dim arrPPH As New ArrayList
            Dim NilaiPPH As Double = 0

            '====================================
            '     GET DATA PERSENTASE BY PO     =
            '====================================
            arrPPH.Clear()
            SQL = "select No_Faktur, Persentase, Kode_Tarif, Kode_Akun, Flag_PPN from EMI_Detail_PPH_PO_Induk_Barang_Lain  "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & no_faktur & "' "
            SQL = SQL & "order by No_Faktur "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            If General_Class.CekNULL(.Rows(i).Item("Flag_PPN")) = "Y" Then
                                PersenPPN += .Rows(i).Item("Persentase")
                            ElseIf General_Class.CekNULL(.Rows(i).Item("Flag_PPN")) = "" Then
                                PersenPPH += .Rows(i).Item("Persentase")
                                arrPPH.Add(Val(HilangkanTanda(.Rows(i).Item("Persentase"))))
                            End If

                        Next
                    Else
                        PersenPPN = 0
                        PersenPPH = 0
                    End If
                End With
            End Using

            '=================================================
            '=     AMBIL NILAI PO BERSIH (TANPA PPN PPH)     =
            '=================================================
            Dim Persentase As Double = 1 + (HilangkanTanda(PersenPPN) / 100) '- (HilangkanTanda(PersenPPH) / 100)
            Dim DPP As Double = Nilai / Persentase

            Dim NilaiPPN As Double = DPP * (PersenPPN / 100)

            If arrPPH.Count <> 0 Then
                For i As Integer = 0 To arrPPH.Count - 1
                    NilaiPPH += Val(HilangkanTanda(Format(DPP * (arrPPH(i) / 100), "N0")))
                Next
            End If

            Txt_NilaiDPP.Text = Format(DPP, "N0")
            Txt_PPNPersen.Text = Format(PersenPPN, "N0")
            Txt_PPN.Text = Format(NilaiPPN, "N0")
            Txt_PPHPersen.Text = Format(PersenPPH, "N0")
            Txt_PPH.Text = Format(NilaiPPH, "N0")

            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

End Class