Public Class Display_Validasi_Selesai_Cheque
    Dim Arr1, Arr2, Arr3, arrbatal As New ArrayList

    Public LvBawahNomor As String
    Dim LvBawahNoPengajuan As String
    Dim LvBawahKodeAkun As String
    Dim LvBawahNamaAkun As String
    Dim LvBawahKeterangan As String
    Dim LvBawahJthTempo As String
    Public LvBawahJml As String
    Public LvBawahSudah As String
    Public LvBawahSisa As String
    Dim LvBawahUrut As String
    Dim LvBawahStatus As String
    Public LvBawahBankTujuan As String
    Dim LvBawahRekTujuan As String
    Dim LvBawahNamaTujuan As String
    Public LvBawahUrutOto As String

    Dim CellBawahNomor As Integer = 0
    Dim CellBawahNoPengajuan As Integer = 1
    Dim CellBawahKodeAkun As Integer = 2
    Dim CellBawahNamaAkun As Integer = 3
    Dim CellBawahKeterangan As Integer = 4
    Dim CellBawahJthTempo As Integer = 5
    Dim CellBawahJml As Integer = 6
    Dim CellBawahSudah As Integer = 7
    Dim CellBawahSisa As Integer = 8
    Dim CellBawahUrut As Integer = 9
    Dim CellBawahStatus As Integer = 10
    Dim CellBawahBankTujuan As Integer = 11
    Dim CellBawahRekTujuan As Integer = 12
    Dim CellBawahNamaTujuan As Integer = 13
    Public CellBawahUrutOto As Integer = 14

    Dim Kd_Voucher As String

    Public Sub Get_Isi_Listview_Bawah(ByVal No_Index As Integer)
        LvBawahNomor = LvDetailPengajuanBiaya.Items(No_Index).Text
        LvBawahNoPengajuan = LvDetailPengajuanBiaya.Items(No_Index).SubItems(1).Text
        LvBawahKodeAkun = LvDetailPengajuanBiaya.Items(No_Index).SubItems(2).Text
        LvBawahNamaAkun = LvDetailPengajuanBiaya.Items(No_Index).SubItems(3).Text
        LvBawahKeterangan = LvDetailPengajuanBiaya.Items(No_Index).SubItems(4).Text
        LvBawahJthTempo = LvDetailPengajuanBiaya.Items(No_Index).SubItems(5).Text
        LvBawahJml = LvDetailPengajuanBiaya.Items(No_Index).SubItems(6).Text
        LvBawahSudah = LvDetailPengajuanBiaya.Items(No_Index).SubItems(7).Text
        LvBawahSisa = LvDetailPengajuanBiaya.Items(No_Index).SubItems(8).Text
        LvBawahUrut = LvDetailPengajuanBiaya.Items(No_Index).SubItems(9).Text
        LvBawahStatus = LvDetailPengajuanBiaya.Items(No_Index).SubItems(10).Text
        LvBawahBankTujuan = LvDetailPengajuanBiaya.Items(No_Index).SubItems(11).Text
        LvBawahRekTujuan = LvDetailPengajuanBiaya.Items(No_Index).SubItems(12).Text
        LvBawahNamaTujuan = LvDetailPengajuanBiaya.Items(No_Index).SubItems(13).Text
        LvBawahUrutOto = LvDetailPengajuanBiaya.Items(No_Index).SubItems(14).Text
    End Sub

    Public LvTtlBankCheque As String
    Public LvTtlRekCheque As String
    Public LvTtlNoCheque As String
    Public LvTtlRp As String

    Public Sub Get_Isi_Listview_Total(ByVal No_Index As Integer)
        LvTtlBankCheque = LvTotalCheque.Items(No_Index).Text
        LvTtlRekCheque = LvTotalCheque.Items(No_Index).SubItems(1).Text
        LvTtlNoCheque = LvTotalCheque.Items(No_Index).SubItems(2).Text
        LvTtlRp = LvTotalCheque.Items(No_Index).SubItems(3).Text
    End Sub

    Dim Lvj_kd_voucher As String
    Dim Lvj_tgl As String
    Dim Lvj_Jam As String
    Dim Lvj_user As String
    Dim Lvj_nilai As String

    Private Sub Get_Isi_Listview_Jurnal(ByVal No_Index As Integer)
        Lvj_kd_voucher = ListView6.Items(No_Index).Text
        Lvj_tgl = ListView6.Items(No_Index).SubItems(1).Text
        Lvj_Jam = ListView6.Items(No_Index).SubItems(2).Text
        Lvj_user = ListView6.Items(No_Index).SubItems(3).Text
        Lvj_nilai = ListView6.Items(No_Index).SubItems(4).Text

    End Sub

    Private Sub HitungTotalCheque()
        'LvTotalCheque.Items.Clear()

        'For i As Integer = 0 To LvDetailPengajuanBiaya.Items.Count - 1
        '    Get_Isi_Listview_Bawah(i)

        '    Dim ada As Boolean
        '    Dim nomor As Integer = -1

        '    For j As Integer = 0 To LvTotalCheque.Items.Count - 1
        '        Get_Isi_Listview_Total(j)

        '        If LvTtlRekCheque = LvBawahRekCheque And LvTtlNoCheque = LvBawahNoCheque Then
        '            ada = True
        '            nomor = j
        '            Exit For
        '        Else
        '            ada = False
        '        End If
        '    Next

        '    If nomor = -1 Then
        '        Dim lv As New ListViewItem
        '        lv = LvTotalCheque.Items.Add(LvBawahBankCheque)
        '        lv.SubItems.Add(LvBawahRekCheque)
        '        lv.SubItems.Add(LvBawahNoCheque)
        '        lv.SubItems.Add(LvBawahJml)
        '        lv.SubItems.Add("1")
        '    Else
        '        Dim lv As New ListViewItem
        '        LvTotalCheque.Items(nomor).SubItems(3).Text = Val(HilangkanTanda(LvTotalCheque.Items(nomor).SubItems(3).Text)) + Val(HilangkanTanda(LvBawahJml))
        '        LvTotalCheque.Items(nomor).SubItems(4).Text = Val(HilangkanTanda(LvTotalCheque.Items(nomor).SubItems(4).Text)) + 1
        '    End If
        'Next
    End Sub

    Private Sub Jf_Display_Validasi_Pengajuan_Cheque_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Jf_Display_Pengajuan_Biaya_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        LvPengajuanBiaya.Columns.Add("No Pengajuan", 110, HorizontalAlignment.Left)
        LvPengajuanBiaya.Columns.Add("Tanggal", 110, HorizontalAlignment.Left)
        LvPengajuanBiaya.Columns.Add("Jam", 60, HorizontalAlignment.Left)
        LvPengajuanBiaya.Columns.Add("User ID", 120, HorizontalAlignment.Left)
        LvPengajuanBiaya.Columns.Add("Grand", 100, HorizontalAlignment.Right)
        LvPengajuanBiaya.Columns.Add("RV", 0, HorizontalAlignment.Left)
        LvPengajuanBiaya.View = View.Details

        LvPengajuanToken.Columns.Add("No Pengajuan", 110, HorizontalAlignment.Left)
        LvPengajuanToken.Columns.Add("Tanggal", 110, HorizontalAlignment.Left)
        LvPengajuanToken.Columns.Add("Jam", 60, HorizontalAlignment.Left)
        LvPengajuanToken.Columns.Add("User ID", 120, HorizontalAlignment.Left)
        LvPengajuanToken.Columns.Add("Grand", 100, HorizontalAlignment.Right)
        LvPengajuanToken.Columns.Add("RV", 0, HorizontalAlignment.Left)
        LvPengajuanToken.View = View.Details


        LvTotalCheque.Columns.Add("Bank", 90, HorizontalAlignment.Left)
        LvTotalCheque.Columns.Add("No. Rek.", 110, HorizontalAlignment.Left)
        LvTotalCheque.Columns.Add("No. Cheque", 100, HorizontalAlignment.Center)
        LvTotalCheque.Columns.Add("Rp. ", 100, HorizontalAlignment.Right)
        LvTotalCheque.View = View.Details

        LvDetailPengajuanBiayaCheque.Columns.Add("No", 40, HorizontalAlignment.Left)
        LvDetailPengajuanBiayaCheque.Columns.Add("No Pengajuan", 150, HorizontalAlignment.Left)
        LvDetailPengajuanBiayaCheque.Columns.Add("Rek. Tujuan", 150, HorizontalAlignment.Center)
        LvDetailPengajuanBiayaCheque.Columns.Add("Nama Tujuan", 250, HorizontalAlignment.Center)
        LvDetailPengajuanBiayaCheque.Columns.Add("Jumlah", 150, HorizontalAlignment.Right)
        LvDetailPengajuanBiayaCheque.Columns.Add("Bank Tujuan", 570, HorizontalAlignment.Left)
        LvDetailPengajuanBiayaCheque.View = View.Details

        LvDetailPengajuanToken1.Columns.Add("No", 40, HorizontalAlignment.Left)
        LvDetailPengajuanToken1.Columns.Add("No Pengajuan", 150, HorizontalAlignment.Left)
        LvDetailPengajuanToken1.Columns.Add("Rek. Tujuan", 150, HorizontalAlignment.Center)
        LvDetailPengajuanToken1.Columns.Add("Nama Tujuan", 250, HorizontalAlignment.Center)
        LvDetailPengajuanToken1.Columns.Add("Jumlah", 150, HorizontalAlignment.Right)
        LvDetailPengajuanToken1.Columns.Add("Bank Tujuan", 570, HorizontalAlignment.Left)
        LvDetailPengajuanToken1.View = View.Details

        LvDetailPengajuanBiaya.Columns.Add("No.", 40, HorizontalAlignment.Left)
        LvDetailPengajuanBiaya.Columns.Add("No Pengajuan", 0, HorizontalAlignment.Left)
        LvDetailPengajuanBiaya.Columns.Add("Kode Akun", 100, HorizontalAlignment.Left)
        LvDetailPengajuanBiaya.Columns.Add("Nama Akun", 190, HorizontalAlignment.Left)
        LvDetailPengajuanBiaya.Columns.Add("Keterangan", 250, HorizontalAlignment.Left)
        LvDetailPengajuanBiaya.Columns.Add("Cost Center", 150, HorizontalAlignment.Left)
        LvDetailPengajuanBiaya.Columns.Add("Jatuh Tempo", 80, HorizontalAlignment.Left)
        LvDetailPengajuanBiaya.Columns.Add("Jumlah", 90, HorizontalAlignment.Right)
        LvDetailPengajuanBiaya.Columns.Add("Sdh Dipakai", 90, HorizontalAlignment.Right)
        LvDetailPengajuanBiaya.Columns.Add("Sisa", 90, HorizontalAlignment.Right)
        LvDetailPengajuanBiaya.Columns.Add("Urut", 0, HorizontalAlignment.Left)
        LvDetailPengajuanBiaya.Columns.Add("Status", 0, HorizontalAlignment.Center)
        LvDetailPengajuanBiaya.Columns.Add("Bank Tujuan", 80, HorizontalAlignment.Center)
        LvDetailPengajuanBiaya.Columns.Add("Rek. Tujuan", 80, HorizontalAlignment.Center)
        LvDetailPengajuanBiaya.Columns.Add("Nama Tujuan", 80, HorizontalAlignment.Center)
        LvDetailPengajuanBiaya.Columns.Add("Urut Oto", 0, HorizontalAlignment.Center)
        LvDetailPengajuanBiaya.Columns.Add("id Cost Center", 0, HorizontalAlignment.Left)
        LvDetailPengajuanBiaya.View = View.Details

        LvDetailPengajuanToken.Columns.Add("No.", 40, HorizontalAlignment.Left)
        LvDetailPengajuanToken.Columns.Add("No Pengajuan", 0, HorizontalAlignment.Left)
        LvDetailPengajuanToken.Columns.Add("Tanggal", 80, HorizontalAlignment.Left)
        LvDetailPengajuanToken.Columns.Add("Kode Akun", 100, HorizontalAlignment.Left)
        LvDetailPengajuanToken.Columns.Add("Nama Akun", 190, HorizontalAlignment.Left)
        LvDetailPengajuanToken.Columns.Add("Keterangan", 250, HorizontalAlignment.Left)
        LvDetailPengajuanToken.Columns.Add("Cost Center", 150, HorizontalAlignment.Left)
        LvDetailPengajuanToken.Columns.Add("Jatuh Tempo", 80, HorizontalAlignment.Left)
        LvDetailPengajuanToken.Columns.Add("Jumlah", 90, HorizontalAlignment.Right)
        LvDetailPengajuanToken.Columns.Add("Bank Tujuan", 80, HorizontalAlignment.Center)
        LvDetailPengajuanToken.Columns.Add("Rek. Tujuan", 80, HorizontalAlignment.Center)
        LvDetailPengajuanToken.Columns.Add("Nama Tujuan", 80, HorizontalAlignment.Center)
        'LvDetailPengajuanToken.Columns.Add("Sisa", 90, HorizontalAlignment.Right)
        LvDetailPengajuanToken.Columns.Add("Urut", 0, HorizontalAlignment.Left)
        LvDetailPengajuanToken.Columns.Add("rv", 0, HorizontalAlignment.Center)
        LvDetailPengajuanToken.Columns.Add("Status", 90, HorizontalAlignment.Left)
        LvDetailPengajuanToken.Columns.Add("val2", 80, HorizontalAlignment.Center)
        LvDetailPengajuanToken.Columns.Add("Bank Dari", 80, HorizontalAlignment.Center)
        LvDetailPengajuanToken.Columns.Add("Rek. Dari", 80, HorizontalAlignment.Center)
        LvDetailPengajuanToken.Columns.Add("Nama Dari", 80, HorizontalAlignment.Center)
        LvDetailPengajuanToken.Columns.Add("Kode Akun Bank Dari", 80, HorizontalAlignment.Center)
        LvDetailPengajuanToken.Columns.Add("id Cost Center", 0, HorizontalAlignment.Left)
        'LvDetailPengajuanToken.Columns.Add("Urut Oto", 0, HorizontalAlignment.Center)
        LvDetailPengajuanToken.View = View.Details

        'ListView1.Columns.Add("Bank", 60, HorizontalAlignment.Left)
        'ListView1.Columns.Add("No Rekening", 80, HorizontalAlignment.Left)
        'ListView1.Columns.Add("Nama Rekening", 120, HorizontalAlignment.Left)
        'ListView1.Columns.Add("Saldo", 80, HorizontalAlignment.Right)
        'ListView1.View = View.Details

        ListView1.Columns.Add("Kode Account", 0, HorizontalAlignment.Left)
        ListView1.Columns.Add("Nama", 120, HorizontalAlignment.Left)
        ListView1.Columns.Add("Keterangan", 120, HorizontalAlignment.Left)
        ListView1.Columns.Add("Saldo", 80, HorizontalAlignment.Right)
        ListView1.Columns.Add("Saldo BB", 80, HorizontalAlignment.Right)
        ListView1.Columns.Add("Selisih", 80, HorizontalAlignment.Right)
        ListView1.View = View.Details

        LvCheque_Tanda_Tangan.Columns.Add("No Rek", 100, HorizontalAlignment.Left)
        LvCheque_Tanda_Tangan.Columns.Add("No Cheque", 100, HorizontalAlignment.Left)
        LvCheque_Tanda_Tangan.Columns.Add("Bank", 100, HorizontalAlignment.Left)
        LvCheque_Tanda_Tangan.Columns.Add("Tgl Tanda Tangan", 100, HorizontalAlignment.Left)
        LvCheque_Tanda_Tangan.Columns.Add("Jam Tanda Tangan", 100, HorizontalAlignment.Left)
        LvCheque_Tanda_Tangan.Columns.Add("User Tanda Tangan", 100, HorizontalAlignment.Left)
        LvCheque_Tanda_Tangan.View = View.Details

        ListView2.Columns.Add("No Rek", 160, HorizontalAlignment.Left)
        ListView2.Columns.Add("No Cheque", 160, HorizontalAlignment.Left)
        ListView2.Columns.Add("Bank", 160, HorizontalAlignment.Left)
        ListView2.View = View.Details

        ListView3.Columns.Add("Bank", 90, HorizontalAlignment.Left)
        ListView3.Columns.Add("No Rek", 100, HorizontalAlignment.Left)
        ListView3.Columns.Add("Nama", 150, HorizontalAlignment.Left)
        ListView3.Columns.Add("Saldo", 130, HorizontalAlignment.Right)
        ListView3.View = View.Details

        ListView4.Columns.Add("Bank", 90, HorizontalAlignment.Left)
        ListView4.Columns.Add("No Rek", 100, HorizontalAlignment.Left)
        ListView4.Columns.Add("Nama", 150, HorizontalAlignment.Left)
        ListView4.Columns.Add("Saldo", 130, HorizontalAlignment.Right)
        ListView4.View = View.Details

        ListView5.Columns.Add("Tanggal Berangkat Dari", 240, HorizontalAlignment.Center)
        ListView5.Columns.Add("Tanggal Berangkat Sampai", 240, HorizontalAlignment.Center)
        ListView5.View = View.Details

        ListView6.Columns.Add("Kode Voucher", 120, HorizontalAlignment.Left)
        ListView6.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
        ListView6.Columns.Add("Jam", 80, HorizontalAlignment.Center)
        ListView6.Columns.Add("User Id", 80, HorizontalAlignment.Left)
        ListView6.Columns.Add("Nilai", 120, HorizontalAlignment.Right)
        ListView6.View = View.Details

        ListView7.Columns.Add("Kode Account", 0, HorizontalAlignment.Left)
        ListView7.Columns.Add("Perkiraan", 110, HorizontalAlignment.Left)
        ListView7.Columns.Add("Keterangan", 110, HorizontalAlignment.Left)
        ListView7.Columns.Add("Debit", 90, HorizontalAlignment.Right)
        ListView7.Columns.Add("Kredit", 90, HorizontalAlignment.Right)
        ListView7.Columns.Add("Lokasi Detail", 100, HorizontalAlignment.Left)
        ListView7.View = View.Details

        Label7.BackColor = Color.FromArgb(255, 247, 209)

        BtnRefresh_Click(Me, Nothing)
    End Sub

    Private Sub Kosong_Cheque()
        LvCheque_Tanda_Tangan.Items.Clear()
        ListView2.Items.Clear()


        Try
            OpenConn()

            SQL = "select a.no_rek,a.no_cheque,b.kode_bank,a.tgl_tanda_tangan_sementara,a.jam_tanda_tangan_sementara, a.user_tanda_tangan_sementara from cheque a, rekening b "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Perusahaan = b.Kode_Perusahaan and a.no_rek = b.no_rek and a.batal is null and a.stok <> 0 and a.Flag_Tanda_Tangan_sementara = 'Y' and a.flag_tanda_tangan is null order by no_cheque"
            Ds = BindingTrans(SQL)
            For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
                With Ds.Tables("MyTable").Rows(i)
                    Dim lvi As ListViewItem
                    lvi = LvCheque_Tanda_Tangan.Items.Add(.Item("no_rek"))
                    lvi.SubItems.Add(.Item("no_cheque"))
                    lvi.SubItems.Add(.Item("kode_bank"))
                    lvi.SubItems.Add(Format(.Item("tgl_tanda_tangan_sementara"), "dd MMM yyyy"))
                    lvi.SubItems.Add(.Item("jam_tanda_tangan_sementara"))
                    lvi.SubItems.Add(.Item("user_tanda_tangan_sementara"))
                End With
            Next

            SQL = "select a.no_rek,a.no_cheque,b.kode_bank from cheque a, rekening b "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.kode_perusahaan = b.kode_perusahaan and a.no_rek = b.no_rek and a.batal is null and a.stok = 1 and a.Flag_tanda_tangan = 'Y' order by a.no_cheque"
            Ds = BindingTrans(SQL)
            For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
                With Ds.Tables("MyTable").Rows(i)
                    Dim lvi As ListViewItem
                    lvi = ListView2.Items.Add(.Item("no_rek"))
                    lvi.SubItems.Add(.Item("no_cheque"))
                    lvi.SubItems.Add(.Item("Kode_Bank"))
                End With
            Next

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Kosong_Saldo()
        TextBox5.Text = "0"
        TextBox6.Text = "0"

        ListView3.Items.Clear()
        ListView4.Items.Clear()

        Try
            OpenConn()

            Dim no As Integer = 0
            Dim saldo As Double = 0
            SQL = "SELECT b.posisi, dateadd(hh, -1, getdate()) as tgl, a.kode_account, b.keterangan, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select saldo from saldo x where x.kode_perusahaan = b.kode_perusahaan and "
            SQL = SQL & "x.kode_master_acc + x.kode_acc + x.kode_detail_acc = b.kode_master_acc + b.kode_acc + b.kode_detail_acc and "
            SQL = SQL & "x.tahun = '" & GetBulanMinSatu(CDate(FMenu.ToolStripStatusLabel3.Text)) & "'"
            SQL = SQL & "), 0) as saldo_bb, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select (case when b.posisi = 'D' then sum(debit-kredit) else sum(kredit-debit)  end) from jurnal x, detail_jurnal y where "
            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.kode_voucher = y.kode_voucher and "
            SQL = SQL & "x.kode_perusahaan = b.kode_perusahaan And "
            SQL = SQL & "y.kode_master_acc + y.kode_acc + y.kode_detail_acc = b.kode_master_acc + b.kode_acc + b.kode_detail_acc and "
            SQL = SQL & "x.tanggal between '" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyy-MM-01") & "' and "
            SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' "
            SQL = SQL & "), 0) as jurnal , c.kode_bank, c.No_Rek, c.nama_rek "

            SQL = SQL & "FROM bank_di_pengajuan a, detail_account b, rekening c WHERE "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and a.kode_account = c.kode_akun and "
            SQL = SQL & "a.kode_account = b.kode_master_acc + b.kode_acc + b.kode_detail_acc and b.flag_pbk = 'T' and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "ORDER BY c.kode_bank, c.nama_rek"
            Ds = BindingTrans(SQL)
            For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
                With Ds.Tables("MyTable").Rows(i)
                    Dim lvi As ListViewItem
                    lvi = ListView3.Items.Add(.Item("Kode_bank"))
                    lvi.SubItems.Add(.Item("no_rek"))
                    lvi.SubItems.Add(.Item("Nama_rek"))
                    lvi.SubItems.Add(Format(.Item("saldo_bb"), "n2"))
                    saldo = saldo + (.Item("saldo_bb"))
                End With
            Next
            TextBox5.Text = Format(saldo, "n2")

            Dim no1 As Integer = 0
            Dim saldo1 As Double = 0
            SQL = "SELECT b.posisi, dateadd(hh, -1, getdate()) as tgl, a.kode_account, b.keterangan, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select saldo from saldo x where x.kode_perusahaan = b.kode_perusahaan and "
            SQL = SQL & "x.kode_master_acc + x.kode_acc + x.kode_detail_acc = b.kode_master_acc + b.kode_acc + b.kode_detail_acc and "
            SQL = SQL & "x.tahun = '" & GetBulanMinSatu(CDate(FMenu.ToolStripStatusLabel3.Text)) & "'"
            SQL = SQL & "), 0) as saldo_bb, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select (case when b.posisi = 'D' then sum(debit-kredit) else sum(kredit-debit)  end) from jurnal x, detail_jurnal y where "
            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.kode_voucher = y.kode_voucher and "
            SQL = SQL & "x.kode_perusahaan = b.kode_perusahaan And "
            SQL = SQL & "y.kode_master_acc + y.kode_acc + y.kode_detail_acc = b.kode_master_acc + b.kode_acc + b.kode_detail_acc and "
            SQL = SQL & "x.tanggal between '" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyy-MM-01") & "' and "
            SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' "
            SQL = SQL & "), 0) as jurnal , c.kode_bank, c.No_Rek, c.nama_rek "

            SQL = SQL & "FROM bank_di_pengajuan a, detail_account b, rekening c WHERE "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c. kode_perusahaan and a.kode_account = c.kode_akun and "
            SQL = SQL & "a.kode_account = b.kode_master_acc + b.kode_acc + b.kode_detail_acc and b.flag_pbk = 'Y' and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "ORDER BY c.kode_bank, c.nama_rek"
            Ds = BindingTrans(SQL)
            For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
                With Ds.Tables("MyTable").Rows(i)
                    Dim lvi As ListViewItem
                    lvi = ListView4.Items.Add(.Item("Kode_bank"))
                    lvi.SubItems.Add(.Item("no_rek"))
                    lvi.SubItems.Add(.Item("Nama_rek"))
                    lvi.SubItems.Add(Format(.Item("saldo_bb"), "n2"))
                    saldo1 = saldo1 + (.Item("saldo_bb"))
                End With
            Next
            TextBox6.Text = Format(saldo1, "n2")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        TextBox1.Text = "0"
        TextBox2.Text = "0"
        TextBox3.Text = "0"
        TextBox4.Text = "0"
        'TextBox5.Text = "0"
        'TextBox6.Text = "0"

        LvPengajuanBiaya.Items.Clear()
        LvTotalCheque.Items.Clear()
        CheckBox1.Checked = False : CheckBox2.Checked = False
        LvDetailPengajuanBiaya.Items.Clear()
        ListView1.Items.Clear()
        DataGridView1.Rows.Clear()

        'LvCheque_Tanda_Tangan.Items.Clear()
        'ListView2.Items.Clear()
        'ListView3.Items.Clear()
        'ListView4.Items.Clear()

        LvPengajuanToken.Items.Clear()
        LvDetailPengajuanToken.Items.Clear()
        LvDetailPengajuanBiayaCheque.Items.Clear()

        DateTimePicker1.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
        DateTimePicker2.Value = CDate(FMenu.ToolStripStatusLabel3.Text)

        Try
            OpenConn()
            OpenConnSQL()

            SQL = "SELECT no_pengajuan, tanggal, jam, keterangan, userid, grand, CAST(rv AS BIGINT) rv "
            SQL = SQL & "FROM pengajuan_cheque WHERE kode_perusahaan = '" & KodePerusahaan & "' AND "
            SQL = SQL & "status IS NULL AND validasi ='Y' and sudah_slip = 'Y' and sudah_val_selesai is null "
            SQL = SQL & "ORDER BY tanggal + jam DESC"
            Ds = BindingTrans(SQL)
            For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
                With Ds.Tables("MyTable").Rows(i)
                    Dim lvi As ListViewItem
                    lvi = LvPengajuanBiaya.Items.Add(.Item("no_pengajuan"))
                    lvi.SubItems.Add(Format(.Item("tanggal"), "dd MMM yyyy"))
                    lvi.SubItems.Add(.Item("jam"))
                    lvi.SubItems.Add(.Item("userid"))
                    lvi.SubItems.Add(Format(.Item("grand"), "N0"))
                    lvi.SubItems.Add(.Item("rv"))

                    'LvPengajuanBiaya.Items(i).BackColor = Color.FromArgb(212, 246, 255)
                    'rgb(212, 246, 255)
                End With
            Next

            Dim Flag_app As String = "T"
            SQL = "SELECT no_pengajuan, tanggal, jam, keterangan, userid, grand, CAST(rv AS BIGINT) rv,Flag_Kirim_Wa "
            SQL = SQL & "FROM pengajuan_token_ok WHERE kode_perusahaan = '" & KodePerusahaan & "' AND "
            SQL = SQL & "status IS NULL AND validasi = 'Y' and sudah_slip = 'Y' and sudah_val_selesai is null "
            SQL = SQL & "ORDER BY tanggal + jam DESC"
            Ds = BindingTrans(SQL)
            For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
                With Ds.Tables("MyTable").Rows(i)
                    If General_Class.CekNULL(.Item("Flag_Kirim_Wa")) = "Y" Then
                        Flag_app = "Y"
                    Else
                        Flag_app = "T"
                    End If

                    Dim lvi As ListViewItem
                    lvi = LvPengajuanToken.Items.Add(.Item("no_pengajuan"))
                    lvi.SubItems.Add(Format(.Item("tanggal"), "dd MMM yyyy"))
                    lvi.SubItems.Add(.Item("jam"))
                    lvi.SubItems.Add(.Item("userid"))
                    lvi.SubItems.Add(Format(.Item("grand"), "N0"))
                    lvi.SubItems.Add(.Item("rv"))

                    If Flag_app = "Y" Then
                        SQLSQL = "select Pengajuan_Token_Id,Acc,b.Tanggal,b.Jam,b.UserID,b.Grand "
                        SQLSQL = SQLSQL & "from Approval_Pengajuan_Token a,Pengajuan_Token_Ok b "
                        SQLSQL = SQLSQL & "where a.Pengajuan_Token_Id = b.No_Pengajuan and b.No_Pengajuan = '" & .Item("no_pengajuan") & "' "
                        Using DrSQL = OpenTransSQL(SQLSQL)
                            Do While DrSQL.Read
                                If General_Class.CekNULL(DrSQL("Acc")) = "" Then
                                    LvPengajuanToken.Items(i).BackColor = Color.FromArgb(255, 247, 209)
                                End If
                            Loop
                        End Using
                    End If


                End With
            Next

            'SQL = "select a.no_rek,a.no_cheque,b.kode_bank,a.tgl_tanda_tangan_sementara,a.jam_tanda_tangan_sementara, a.user_tanda_tangan_sementara from cheque a, rekening b "
            'SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Perusahaan = b.Kode_Perusahaan and a.no_rek = b.no_rek and a.batal is null and a.stok <> 0 and a.Flag_Tanda_Tangan_sementara = 'Y' and a.flag_tanda_tangan is null order by no_cheque"
            'Ds = BindingTrans(SQL)
            'For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
            '    With Ds.Tables("MyTable").Rows(i)
            '        Dim lvi As ListViewItem
            '        lvi = LvCheque_Tanda_Tangan.Items.Add(.Item("no_rek"))
            '        lvi.SubItems.Add(.Item("no_cheque"))
            '        lvi.SubItems.Add(.Item("kode_bank"))
            '        lvi.SubItems.Add(Format(.Item("tgl_tanda_tangan_sementara"), "dd MMM yyyy"))
            '        lvi.SubItems.Add(.Item("jam_tanda_tangan_sementara"))
            '        lvi.SubItems.Add(.Item("user_tanda_tangan_sementara"))
            '    End With
            'Next

            'SQL = "select a.no_rek,a.no_cheque,b.kode_bank from cheque a, rekening b "
            'SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.kode_perusahaan = b.kode_perusahaan and a.no_rek = b.no_rek and a.batal is null and a.stok = 1 and a.Flag_tanda_tangan = 'Y' order by a.no_cheque"
            'Ds = BindingTrans(SQL)
            'For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
            '    With Ds.Tables("MyTable").Rows(i)
            '        Dim lvi As ListViewItem
            '        lvi = ListView2.Items.Add(.Item("no_rek"))
            '        lvi.SubItems.Add(.Item("no_cheque"))
            '        lvi.SubItems.Add(.Item("Kode_Bank"))
            '    End With
            'Next

            ''Dim tgl_tarik_bb As String
            ''tgl_tarik_bb = ""

            'Dim no As Integer = 0
            'Dim saldo As Double = 0
            'SQL = "SELECT b.posisi, dateadd(hh, -1, getdate()) as tgl, a.kode_account, b.keterangan, "

            'SQL = SQL & "isnull(("
            'SQL = SQL & "select saldo from saldo x where x.kode_perusahaan = b.kode_perusahaan and "
            'SQL = SQL & "x.kode_master_acc + x.kode_acc + x.kode_detail_acc = b.kode_master_acc + b.kode_acc + b.kode_detail_acc and "
            'SQL = SQL & "x.tahun = '" & GetBulanMinSatu(CDate(fmenu.ToolStripStatusLabel3.Text)) & "'"
            'SQL = SQL & "), 0) as saldo_bb, "

            'SQL = SQL & "isnull(("
            'SQL = SQL & "select (case when b.posisi = 'D' then sum(debit-kredit) else sum(kredit-debit)  end) from jurnal x, detail_jurnal y where "
            'SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.kode_voucher = y.kode_voucher and "
            'SQL = SQL & "x.kode_perusahaan = b.kode_perusahaan And "
            'SQL = SQL & "y.kode_master_acc + y.kode_acc + y.kode_detail_acc = b.kode_master_acc + b.kode_acc + b.kode_detail_acc and "
            'SQL = SQL & "x.tanggal between '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "yyyy-MM-01") & "' and "
            'SQL = SQL & "'" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' "
            'SQL = SQL & "), 0) as jurnal , c.kode_bank, c.No_Rek, c.nama_rek "

            'SQL = SQL & "FROM bank_di_pengajuan a, detail_account b, rekening c WHERE "
            'SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and a.kode_account = c.kode_akun and "
            'SQL = SQL & "a.kode_account = b.kode_master_acc + b.kode_acc + b.kode_detail_acc and b.flag_pbk = 'T' and "
            'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "ORDER BY c.kode_bank, c.nama_rek"
            'Ds = BindingTrans(SQL)
            'For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
            '    With Ds.Tables("MyTable").Rows(i)
            '        Dim lvi As ListViewItem
            '        lvi = ListView3.Items.Add(.Item("Kode_bank"))
            '        lvi.SubItems.Add(.Item("no_rek"))
            '        lvi.SubItems.Add(.Item("Nama_rek"))
            '        lvi.SubItems.Add(Format(.Item("saldo_bb"), "n2"))
            '        saldo = saldo + (.Item("saldo_bb"))
            '    End With
            'Next
            'TextBox5.Text = Format(saldo, "n2")

            'Dim no1 As Integer = 0
            'Dim saldo1 As Double = 0
            'SQL = "SELECT b.posisi, dateadd(hh, -1, getdate()) as tgl, a.kode_account, b.keterangan, "

            'SQL = SQL & "isnull(("
            'SQL = SQL & "select saldo from saldo x where x.kode_perusahaan = b.kode_perusahaan and "
            'SQL = SQL & "x.kode_master_acc + x.kode_acc + x.kode_detail_acc = b.kode_master_acc + b.kode_acc + b.kode_detail_acc and "
            'SQL = SQL & "x.tahun = '" & GetBulanMinSatu(CDate(fmenu.ToolStripStatusLabel3.Text)) & "'"
            'SQL = SQL & "), 0) as saldo_bb, "

            'SQL = SQL & "isnull(("
            'SQL = SQL & "select (case when b.posisi = 'D' then sum(debit-kredit) else sum(kredit-debit)  end) from jurnal x, detail_jurnal y where "
            'SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.kode_voucher = y.kode_voucher and "
            'SQL = SQL & "x.kode_perusahaan = b.kode_perusahaan And "
            'SQL = SQL & "y.kode_master_acc + y.kode_acc + y.kode_detail_acc = b.kode_master_acc + b.kode_acc + b.kode_detail_acc and "
            'SQL = SQL & "x.tanggal between '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "yyyy-MM-01") & "' and "
            'SQL = SQL & "'" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' "
            'SQL = SQL & "), 0) as jurnal , c.kode_bank, c.No_Rek, c.nama_rek "

            'SQL = SQL & "FROM bank_di_pengajuan a, detail_account b, rekening c WHERE "
            'SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c. kode_perusahaan and a.kode_account = c.kode_akun and "
            'SQL = SQL & "a.kode_account = b.kode_master_acc + b.kode_acc + b.kode_detail_acc and b.flag_pbk = 'Y' and "
            'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "ORDER BY c.kode_bank, c.nama_rek"
            'Ds = BindingTrans(SQL)
            'For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
            '    With Ds.Tables("MyTable").Rows(i)
            '        Dim lvi As ListViewItem
            '        lvi = ListView4.Items.Add(.Item("Kode_bank"))
            '        lvi.SubItems.Add(.Item("no_rek"))
            '        lvi.SubItems.Add(.Item("Nama_rek"))
            '        lvi.SubItems.Add(Format(.Item("saldo_bb"), "n2"))
            '        saldo1 = saldo1 + (.Item("saldo_bb"))
            '    End With
            'Next
            'TextBox6.Text = Format(saldo1, "n2")

            ListView5.Items.Clear()
            SQL = "select Tgl_Berangkat_Dari,Tgl_Berangkat_Sampai from Master_Tgl_Berangkat where Kode_Perusahaan = '" & KodePerusahaan & "' order by Tgl_Berangkat_Dari desc"
            Using Ds = BindingTrans(SQL)
                For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
                    With Ds.Tables("MyTable").Rows(i)
                        Dim lvi As ListViewItem
                        lvi = ListView5.Items.Add(Format(.Item("Tgl_Berangkat_Dari"), "dd MMMM yyyy"))
                        lvi.SubItems.Add(Format(.Item("Tgl_Berangkat_Sampai"), "dd MMMM yyyy"))
                    End With
                Next
            End Using

            ListView6.Items.Clear() : ListView7.Items.Clear()
            SQL = "select a.Kode_Voucher,a.Tanggal,a.Jam,a.userid,sum(b.debit) as debit "
            SQL = SQL & "from Jurnal_Sementara_New a,Detail_Jurnal_Sementara_New b "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.val is null and status is null "
            SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Voucher = b.Kode_Voucher "
            SQL = SQL & "group by a.Kode_Voucher,a.Tanggal,a.Jam,a.userid "
            SQL = SQL & "order by a.Tanggal+a.Jam "
            Using Ds = BindingTrans(SQL)
                For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
                    With Ds.Tables("MyTable").Rows(i)
                        Dim lvi As ListViewItem
                        lvi = ListView6.Items.Add(.Item("Kode_Voucher"))
                        lvi.SubItems.Add(Format(.Item("tanggal"), "dd MMM yyyy"))
                        lvi.SubItems.Add(.Item("jam"))
                        lvi.SubItems.Add(.Item("userid"))
                        lvi.SubItems.Add(Format(.Item("debit"), "N0"))
                    End With
                Next
            End Using

            CloseConn()
            CloseConnSQL()
        Catch ex As Exception
            CloseConn()
            CloseConnSQL()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong_Cheque()
    End Sub

    Private Sub LvPengajuanBiaya_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LvPengajuanBiaya.Click
        Dim total_cheque As Double = 0
        Dim total_biaya As Double = 0
        Dim total_saldo As Double = 0

        LvDetailPengajuanBiaya.Items.Clear()
        ' LvDetailPengajuanToken.Items.Clear()

        'LvPengajuanToken.Items.Clear()

        LvTotalCheque.Items.Clear()
        ListView1.Items.Clear()
        'ListView2.Items.Clear()
        'ListView3.Items.Clear()
        TextBox1.Text = "0"
        TextBox2.Text = "0"
        TextBox3.Text = "0"
        'TextBox4.Text = "0"
        'TextBox5.Text = "0"
        'TextBox6.Text = "0"

        Try
            If LvPengajuanBiaya.Items.Count = 0 Then Exit Sub

            OpenConn()

            Dim kode_unik As String = ""
            'LvDetailPengajuanBiaya.Items.Clear()
            'SQL = "SELECT da.letak, pc.Kode_unik_rekon, dp.kode_perusahaan, dp.no_pengajuan, dp.urut_oto, dp.jenis_slip, dp.kode_bank_tujuan, dp.NO_REK_TUJUAN, dp.NAMA_PENERIMA, "
            'SQL = SQL & "dp.kode_master_acc + dp.kode_acc + dp.kode_detail_acc as kode_akun, da.keterangan nama_akun, dp.keterangan_detail, "
            'SQL = SQL & "dp.tgl_jatuh_tempo, dp.jumlah, dp.urut_pengajuan, "
            'SQL = SQL & "isnull(("
            'SQL = SQL & "select sum(nilai) from detail_pengajuan_cheque3 x where x.kode_perusahaan = dp.kode_perusahaan and "
            'SQL = SQL & "x.urut_pengajuan_cheque = dp.urut_oto"
            'SQL = SQL & "), 0) as sudah "
            'SQL = SQL & "FROM detail_pengajuan_cheque dp, detail_account da,pengajuan_cheque pc "
            'SQL = SQL & "WHERE pc.kode_perusahaan = dp.kode_perusahaan and dp.kode_perusahaan = da.kode_perusahaan AND "
            'SQL = SQL & "dp.kode_master_acc + dp.kode_acc + dp.kode_detail_acc = da.kode_master_acc + da.kode_acc + da.kode_detail_acc AND "
            'SQL = SQL & "dp.kode_perusahaan = '" & KodePerusahaan & "' AND pc.no_pengajuan = dp.no_pengajuan and "
            'SQL = SQL & "dp.no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "' AND dp.val IS NULL "
            'SQL = SQL & "ORDER BY dp.urut_oto"
            'Using Ds = BindingTrans(SQL)
            '    For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
            '        With Ds.Tables("MyTable").Rows(i)
            '            Dim lvi As ListViewItem
            '            lvi = LvDetailPengajuanBiaya.Items.Add(i + 1)
            '            lvi.SubItems.Add(.Item("no_pengajuan"))
            '            lvi.SubItems.Add(.Item("kode_akun"))
            '            lvi.SubItems.Add(.Item("nama_akun"))
            '            lvi.SubItems.Add(.Item("keterangan_detail"))
            '            lvi.SubItems.Add(Format(.Item("tgl_jatuh_tempo"), "dd MMM yyyy"))
            '            lvi.SubItems.Add(Format(.Item("jumlah"), "N0"))
            '            lvi.SubItems.Add(Format(.Item("sudah"), "N0"))
            '            lvi.SubItems.Add(Format(.Item("jumlah") - .Item("sudah"), "N0"))
            '            lvi.SubItems.Add(.Item("urut_pengajuan"))
            '            lvi.SubItems.Add("")
            '            lvi.SubItems.Add(.Item("kode_bank_tujuan"))
            '            lvi.SubItems.Add(.Item("NO_REK_TUJUAN"))
            '            lvi.SubItems.Add(.Item("NAMA_PENERIMA"))
            '            lvi.SubItems.Add(.Item("urut_oto"))

            '            total_biaya = total_biaya + .Item("jumlah")
            '            kode_unik = (.Item("Kode_unik_rekon"))

            '            If (.Item("letak")) = "Neraca" Then
            '                LvDetailPengajuanBiaya.Items(i).ForeColor = Color.Red
            '            End If

            '        End With
            '    Next
            'End Using


            SQL = "select kode_unik_rekon from pengajuan_cheque where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    kode_unik = Dr("kode_unik_rekon")
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("No Pengajuan tidak tersedia ..!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            LvDetailPengajuanBiayaCheque.Items.Clear()

            SQL = "SELECT dp.kode_perusahaan, dp.no_pengajuan,dp.kode_bank_tujuan, dp.NO_REK_TUJUAN, dp.NAMA_PENERIMA,"
            SQL = SQL & "isnull(sum(dp.Jumlah), 0) as total FROM detail_pengajuan_cheque dp,pengajuan_cheque pc  where "
            SQL = SQL & "pc.kode_perusahaan = dp.kode_perusahaan AND dp.kode_perusahaan = '" & KodePerusahaan & "' AND pc.no_pengajuan = dp.no_pengajuan and "
            SQL = SQL & "dp.no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "' AND dp.val IS NULL "
            SQL = SQL & "group by dp.Kode_Perusahaan, dp.No_Pengajuan,dp.Kode_Bank_Tujuan,dp.No_Rek_Tujuan,dp.Nama_Penerima "
            SQL = SQL & "ORDER BY dp.No_Pengajuan + dp.Kode_Bank_Tujuan "
            Using Ds = BindingTrans(SQL)
                For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
                    With Ds.Tables("MyTable").Rows(i)
                        Dim lv As ListViewItem
                        lv = LvDetailPengajuanBiayaCheque.Items.Add(i + 1)
                        lv.SubItems.Add(.Item("no_pengajuan"))
                        lv.SubItems.Add(.Item("no_rek_tujuan"))
                        lv.SubItems.Add(.Item("nama_penerima"))
                        lv.SubItems.Add(Format(.Item("total"), "N0"))
                        lv.SubItems.Add(.Item("kode_bank_tujuan"))


                        total_biaya = total_biaya + .Item("total")
                    End With
                Next
            End Using

            LvTotalCheque.Items.Clear() : CheckBox1.Checked = False
            SQL = "SELECT a.kode_perusahaan, a.urut, a.sudah_print, b.kode_bank, b.no_rek, a.no_cheque, a.nilai, a.sudah_val_selesai "
            SQL = SQL & "FROM detail_pengajuan_cheque2 a, rekening b "
            SQL = SQL & "WHERE a.kode_perusahaan = b.kode_perusahaan AND "
            SQL = SQL & "a.no_rek = b.no_rek and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' AND "
            SQL = SQL & "a.no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "' and a.sudah_val_selesai is null "
            SQL = SQL & "ORDER BY a.urut"
            Using Ds = BindingTrans(SQL)
                For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
                    With Ds.Tables("MyTable").Rows(i)
                        Dim lvi As ListViewItem
                        lvi = LvTotalCheque.Items.Add(.Item("kode_bank"))
                        lvi.SubItems.Add(.Item("no_rek"))
                        lvi.SubItems.Add(.Item("no_cheque"))
                        lvi.SubItems.Add(Format(.Item("nilai"), "N0"))

                        total_cheque = total_cheque + .Item("nilai")
                    End With
                Next
            End Using

            'join tabel rekening dengan rekon_bank (join berdasar kode_akun) order by kode_bank
            ListView1.Items.Clear()
            'SQL = "select a.kode_bank, a.no_rek, a.nama_rek, b.saldo from rekening a, rekon_bank b "
            'SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and a.kode_akun = b.kode_account and a.kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and b.kode_unik_rekon = '" & kode_unik & "' order by a.kode_bank,a.nama_rek"
            'Using Ds = BindingTrans(SQL)
            '    For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
            '        With Ds.Tables("MyTable").Rows(i)
            '            Dim lvi As ListViewItem
            '            lvi = ListView1.Items.Add(.Item("kode_bank"))
            '            lvi.SubItems.Add(.Item("no_rek"))
            '            lvi.SubItems.Add(.Item("nama_rek"))
            '            lvi.SubItems.Add(Format(.Item("saldo"), "N0"))

            '            total_saldo = total_saldo + .Item("saldo")
            '        End With
            '    Next
            'End Using
            DataGridView1.Rows.Clear()
            Dim no As Integer = 0
            SQL = "select a.Kode_Account,b.Keterangan as Nama,a.Keterangan,a.Saldo,a.Saldo_BB,a.Selisih "
            SQL = SQL & "from Rekon_Bank a,Detail_Account b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Account = b.Kode_Account "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Tanggal = '" & Format(CDate(LvPengajuanBiaya.FocusedItem.SubItems(1).Text), "yyyy-MM-dd") & "' "
            Using Ds = BindingTrans(SQL)
                For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
                    With Ds.Tables("MyTable").Rows(i)
                        Dim lvi As ListViewItem
                        lvi = ListView1.Items.Add(.Item("Kode_Account"))
                        lvi.SubItems.Add(.Item("Nama"))
                        lvi.SubItems.Add(.Item("Keterangan"))
                        lvi.SubItems.Add(Format(.Item("Saldo"), "N0"))
                        lvi.SubItems.Add(Format(.Item("Saldo_BB"), "N0"))
                        lvi.SubItems.Add(Format(.Item("Selisih"), "N0"))

                        DataGridView1.Rows.Add(1)
                        DataGridView1.Rows.Item(no).Cells(0).Value = .Item("Nama") & Chr(13) & Chr(10) & .Item("Keterangan")
                        'DataGridView1.Rows.Item(no).Cells(0).Value = .Item("Nama") & Environment.NewLine & .Item("Keterangan")
                        DataGridView1.Rows.Item(no).Cells(1).Value = Format(.Item("Selisih"), "N0")
                        DataGridView1.Rows.Item(no).Cells(2).Value = Format(.Item("Saldo"), "N0")
                        DataGridView1.Rows.Item(no).Cells(3).Value = Format(.Item("Saldo_BB"), "N0")
                        DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

                        no = no + 1

                        total_saldo = total_saldo + .Item("saldo")
                    End With
                Next
            End Using

            CloseConn()
        Catch ex As Exception
            LvDetailPengajuanBiaya.Items.Clear()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        TextBox1.Text = Format(total_cheque, "N0")
        TextBox2.Text = Format(total_biaya, "N0")
        TextBox3.Text = Format(total_saldo, "N0")

        HitungTotalCheque()
    End Sub

    Private Sub LvPengajuanBiaya_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LvPengajuanBiaya.SelectedIndexChanged
        LvDetailPengajuanBiaya.Items.Clear()
        'LvDetailPengajuanToken.Items.Clear()

        'LvPengajuanToken.Items.Clear()

        LvTotalCheque.Items.Clear()

        ListView1.Items.Clear()
        DataGridView1.Rows.Clear()

        LvDetailPengajuanBiayaCheque.Items.Clear()
        'ListView1.Items.Clear()
        'ListView2.Items.Clear()
        'ListView3.Items.Clear()
        'TextBox1.Text = "0"
        TextBox2.Text = "0"
        'TextBox3.Text = "0"
        'TextBox4.Text = "0"
        'TextBox5.Text = "0"
        'TextBox6.Text = "0"
    End Sub

    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If LvPengajuanBiaya.Items.Count = 0 Then
        '    MessageBox.Show("Pilih dahulu no pengajuan yang mau validasi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'ElseIf LvDetailPengajuanBiaya.Items.Count = 0 Then
        '    MessageBox.Show("Pilih dahulu detail yang mau validasi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If

        'Dim ada As Integer = 0
        'For i As Integer = 0 To LvDetailPengajuanBiaya.Items.Count - 1
        '    Get_Isi_Listview_Bawah(i)

        '    If LvBawahStatus = "" Then
        '        ada = ada + 1
        '    End If
        'Next

        'If ada = 0 Then
        '    MessageBox.Show("Belum ada detail cheque yang akan divalidasi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If

        'Dim tanya As String = MessageBox.Show("Yakin akan validasi pengajuan cheque ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        'If tanya = vbNo Then Exit Sub

        'Try
        '    OpenConn()

        '    Cmd.Transaction = Cn.BeginTransaction

        '    SQL = "SELECT status, validasi, CAST(rv AS BIGINT) rv FROM pengajuan_cheque WHERE kode_perusahaan = '" & KodePerusahaan & "' AND "
        '    SQL = SQL & "no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "'"
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then
        '            ' cek rv
        '            If Dr("rv") <> LvPengajuanBiaya.FocusedItem.SubItems(5).Text Then
        '                CloseTrans()
        '                CloseConn()
        '                MessageBox.Show("Pengajuan cheque tidak dapat divalidasi karena terdapat perubahan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Exit Sub
        '            End If

        '            ' cek status
        '            If General_Class.CekNULL(Dr("status")) <> "" Then
        '                CloseTrans()
        '                CloseConn()
        '                MessageBox.Show("Pengajuan biaya tidak dapat divalidasi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Exit Sub
        '            End If

        '            ' cek validasi
        '            If General_Class.CekNULL(Dr("validasi")) <> "" Then
        '                CloseTrans()
        '                CloseConn()
        '                MessageBox.Show("Pengajuan biaya tidak dapat divalidasi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Exit Sub
        '            End If
        '        Else
        '            Dr.Close()
        '            CloseTrans()
        '            CloseConn()
        '            MessageBox.Show("Pengajuan biaya tidak dapat ditemukan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        End If
        '    End Using

        '    For i = 0 To LvDetailPengajuanBiaya.Items.Count - 1
        '        Get_Isi_Listview_Bawah(i)

        '        If LvBawahStatus = "" Then
        '            SQL = "UPDATE detail_pengajuan_cheque SET val = 'Y' WHERE urut_oto = '" & LvBawahUrutOto & "'"
        '            ExecuteTrans(SQL)
        '        ElseIf LvBawahStatus = "PENDING" Then
        '            SQL = "UPDATE detail_pengajuan_cheque SET pending = 'Y' WHERE urut_oto = '" & LvBawahUrutOto & "'"
        '            ExecuteTrans(SQL)

        '            SQL = "UPDATE detail_pengajuan SET val2 = NULL WHERE urut = '" & LvBawahUrut & "'"
        '            ExecuteTrans(SQL)

        '        ElseIf LvBawahStatus = "BATAL" Then
        '            SQL = "UPDATE detail_pengajuan_cheque SET batal = 'Y' WHERE urut_oto = '" & LvBawahUrutOto & "'"
        '            ExecuteTrans(SQL)
        '        End If
        '    Next

        '    SQL = "UPDATE pengajuan_cheque SET validasi = 'Y', "
        '    SQL = SQL & "Tgl_Validasi = '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', "
        '    SQL = SQL & "Jam_Validasi = '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
        '    SQL = SQL & "User_Validasi = '" & UserID & "' WHERE "
        '    SQL = SQL & "no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "'"
        '    ExecuteTrans(SQL)


        '    Cmd.Transaction.Commit()

        '    CloseConn()

        '    MessageBox.Show("Pengajuan cheque berhasil divalidasi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

        'Catch ex As Exception
        '    CloseTrans()
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        'BtnRefresh_Click(BtnUpdate, e)
    End Sub


    Private Sub Label1_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label1.SizeChanged

    End Sub

    Private Sub Jf_Display_Validasi_Pengajuan_Cheque_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Label1.Size = New Point(Me.Width, 33)
    End Sub

    Private Sub LvDetailPengajuanBiaya_DockChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LvDetailPengajuanBiaya.DockChanged

    End Sub

    Private Sub LvDetailPengajuanBiaya_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LvDetailPengajuanBiaya.DoubleClick


    End Sub

    Private Sub LvDetailPengajuanBiaya_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LvDetailPengajuanBiaya.SelectedIndexChanged

    End Sub

    Private Sub PrintChequeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintChequeToolStripMenuItem.Click
        If LvTotalCheque.Items.Count = 0 Or LvTotalCheque.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu cheque yang akan divalidasi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If


        Dim tanya As String = MessageBox.Show("Yakin akan divalidasi?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tanya = vbNo Then Exit Sub

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            SQL = "SELECT status, CAST(rv AS BIGINT) rv, Sudah_Val_Selesai FROM pengajuan_cheque WHERE kode_perusahaan = '" & KodePerusahaan & "' AND "
            SQL = SQL & "no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    ' cek rv
                    If Dr("rv") <> LvPengajuanBiaya.FocusedItem.SubItems(5).Text Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Pengajuan cheque tidak dapat divalidasi karena terdapat perubahan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(Dr("status")) <> "" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Pengajuan cheque sudah dibatalkan sebelumnya.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(Dr("Sudah_Val_Selesai")) <> "" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Pengajuan cheque sudah selesai divalidasi sebelumnya.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pengajuan cheque tidak dapat ditemukan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            Get_Isi_Listview_Total(LvTotalCheque.FocusedItem.Index)

            SQL = "SELECT Sudah_Val_Selesai FROM detail_pengajuan_cheque2 WHERE kode_perusahaan = '" & KodePerusahaan & "' AND "
            SQL = SQL & "no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "' and "
            SQL = SQL & "no_rek = '" & LvTtlRekCheque & "' and no_cheque = '" & LvTtlNoCheque & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    ' cek rv
                    If General_Class.CekNULL(Dr("Sudah_Val_Selesai")) <> "" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Pengajuan cheque sudah selesai divalidasi sebelumnya.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        Dr.Close()
                        SQL = "update detail_pengajuan_cheque2 set Sudah_Val_Selesai = 'Y' WHERE kode_perusahaan = '" & KodePerusahaan & "' AND "
                        SQL = SQL & "no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "' and "
                        SQL = SQL & "no_rek = '" & LvTtlRekCheque & "' and no_cheque = '" & LvTtlNoCheque & "'"
                        ExecuteTrans(SQL)
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pengajuan cheque tidak dapat ditemukan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "SELECT count(kode_perusahaan) as ttl FROM detail_pengajuan_cheque2 WHERE kode_perusahaan = '" & KodePerusahaan & "' AND "
            SQL = SQL & "no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "' and "
            SQL = SQL & "sudah_val_selesai is null"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("ttl") = 0 Then
                        Dr.Close()
                        SQL = "update pengajuan_cheque set Sudah_Val_Selesai = 'Y' WHERE kode_perusahaan = '" & KodePerusahaan & "' AND "
                        SQL = SQL & "no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "' "
                        ExecuteTrans(SQL)
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pengajuan cheque tidak dapat ditemukan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Berhasil divalidasi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        LvPengajuanBiaya_Click(PrintChequeToolStripMenuItem, e)
    End Sub

    Private Sub LvPengajuanToken_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LvPengajuanToken.Click
        Dim total_cheque As Double = 0
        Dim total_biaya As Double = 0
        Dim total_saldo As Double = 0

        ' LvDetailPengajuanBiaya.Items.Clear()
        LvDetailPengajuanToken.Items.Clear()

        '  LvPengajuanBiaya.Items.Clear()

        ' LvTotalCheque.Items.Clear()
        'ListView1.Items.Clear()
        'ListView2.Items.Clear()
        'ListView3.Items.Clear()
        'TextBox1.Text = "0"
        'TextBox2.Text = "0"
        'TextBox3.Text = "0"
        TextBox4.Text = "0"
        'TextBox5.Text = "0"
        'TextBox6.Text = "0"

        Try
            If LvPengajuanToken.Items.Count = 0 Then Exit Sub

            OpenConn()

            Dim kode_unik As String = ""
            'LvDetailPengajuanToken.Items.Clear()
            'SQL = "SELECT d.kode_bank as kode_bank_dari, d.no_rek as no_rek_dari, d.nama_rek as nama_rek_dari, d.kode_akun as kode_akun_dari, "
            'SQL = SQL & "b.kode_bank_tujuan, b.NO_REK_TUJUAN, b.NAMA_PENERIMA, b.val2, "
            'SQL = SQL & "a.no_pengajuan, a.tanggal, a.jam, CAST(rv AS BIGINT) rv, a.tanggal + a.jam as tgl, "
            'SQL = SQL & "b.kode_master_acc + b.kode_acc + b.kode_detail_acc as kode_account, c.keterangan as nama_akun, b.keterangan_detail, "
            'SQL = SQL & "b.tgl_jatuh_tempo, b.jumlah, b.urut_oto "
            'SQL = SQL & "FROM pengajuan_token_ok a, detail_pengajuan_token_ok b, detail_account c, rekening d WHERE "
            'SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and "
            'SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan and a.no_pengajuan = b.no_pengajuan and "
            'SQL = SQL & "b.kode_master_acc + b.kode_acc + b.kode_detail_acc = c.kode_master_acc + c.kode_acc + c.kode_detail_acc and "
            'SQL = SQL & "a.no_rek = d.no_rek and "
            'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' AND a.status IS NULL AND a.validasi = 'Y' and "
            'SQL = SQL & "b.val = 'Y' and b.batal is null and b.val2 is null and a.no_pengajuan = '" & LvPengajuanToken.FocusedItem.Text & "' "
            'SQL = SQL & "ORDER BY a.tanggal + a.jam, c.keterangan"
            'Using Ds = BindingTrans(SQL)
            '    For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
            '        With Ds.Tables("MyTable").Rows(i)
            '            Dim lvi As ListViewItem
            '            lvi = LvDetailPengajuanToken.Items.Add(i + 1)
            '            lvi.SubItems.Add(.Item("no_pengajuan"))
            '            lvi.SubItems.Add(Format(.Item("tgl"), "dd MMM yyyy"))
            '            lvi.SubItems.Add(.Item("kode_account"))
            '            lvi.SubItems.Add(.Item("nama_akun"))
            '            lvi.SubItems.Add(.Item("keterangan_detail"))
            '            lvi.SubItems.Add(Format(.Item("tgl_jatuh_tempo"), "dd MMM yyyy"))
            '            lvi.SubItems.Add(Format(.Item("jumlah"), "N0"))
            '            lvi.SubItems.Add(.Item("kode_bank_tujuan"))
            '            lvi.SubItems.Add(.Item("NO_REK_TUJUAN"))
            '            lvi.SubItems.Add(.Item("NAMA_PENERIMA"))
            '            lvi.SubItems.Add(.Item("urut_oto"))
            '            lvi.SubItems.Add(.Item("rv"))
            '            If General_Class.CekNULL(.Item("val2")) = "" Then
            '                lvi.SubItems.Add("Belum Val 2")
            '                lvi.SubItems.Add("T")
            '            Else
            '                lvi.SubItems.Add("Sudah Val 2")
            '                lvi.SubItems.Add("Y")
            '            End If

            '            lvi.SubItems.Add(.Item("kode_bank_dari"))
            '            lvi.SubItems.Add(.Item("no_rek_dari"))
            '            lvi.SubItems.Add(.Item("nama_rek_dari"))
            '            lvi.SubItems.Add(.Item("kode_akun_dari"))

            '        End With
            '    Next
            'End Using


            SQL = "select kode_unik_rekon from pengajuan_token_ok where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and no_pengajuan = '" & LvPengajuanToken.FocusedItem.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    kode_unik = Dr("kode_unik_rekon")
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("No Pengajuan tidak tersedia ..!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            LvDetailPengajuanToken1.Items.Clear()

            SQL = "SELECT dp.kode_perusahaan, dp.no_pengajuan,dp.kode_bank_tujuan, dp.NO_REK_TUJUAN, dp.NAMA_PENERIMA,"
            SQL = SQL & "isnull(sum(dp.Jumlah), 0) as total FROM Detail_Pengajuan_Token_Ok dp,pengajuan_token_ok pc  where "
            SQL = SQL & "pc.kode_perusahaan = dp.kode_perusahaan AND dp.kode_perusahaan = '" & KodePerusahaan & "' AND pc.no_pengajuan = dp.no_pengajuan and "
            SQL = SQL & "dp.no_pengajuan = '" & LvPengajuanToken.FocusedItem.Text & "' AND pc.validasi = 'Y' and dp.val = 'Y' and dp.batal is null and dp.val2 is null "
            SQL = SQL & "group by dp.Kode_Perusahaan, dp.No_Pengajuan,dp.Kode_Bank_Tujuan,dp.No_Rek_Tujuan,dp.Nama_Penerima "
            SQL = SQL & "ORDER BY dp.No_Pengajuan + dp.Kode_Bank_Tujuan "
            Using Ds = BindingTrans(SQL)
                For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
                    With Ds.Tables("MyTable").Rows(i)
                        Dim lv As ListViewItem
                        lv = LvDetailPengajuanToken1.Items.Add(i + 1)
                        lv.SubItems.Add(.Item("no_pengajuan"))
                        lv.SubItems.Add(.Item("no_rek_tujuan"))
                        lv.SubItems.Add(.Item("nama_penerima"))
                        lv.SubItems.Add(Format(.Item("total"), "N0"))
                        lv.SubItems.Add(.Item("kode_bank_tujuan"))

                        total_biaya = total_biaya + .Item("total")
                    End With
                Next
            End Using


            'join tabel rekening dengan rekon_bank (join berdasar kode_akun) order by kode_bank
            ListView1.Items.Clear()
            DataGridView1.Rows.Clear()
            'SQL = "select a.kode_bank, a.no_rek, a.nama_rek, b.saldo from rekening a, rekon_bank b "
            'SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and a.kode_akun = b.kode_account and a.kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and b.kode_unik_rekon = '" & kode_unik & "' order by a.kode_bank,a.nama_rek"
            'Using Ds = BindingTrans(SQL)
            '    For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
            '        With Ds.Tables("MyTable").Rows(i)
            '            Dim lvi As ListViewItem
            '            lvi = ListView1.Items.Add(.Item("kode_bank"))
            '            lvi.SubItems.Add(.Item("no_rek"))
            '            lvi.SubItems.Add(.Item("nama_rek"))
            '            lvi.SubItems.Add(Format(.Item("saldo"), "N0"))

            '            total_saldo = total_saldo + .Item("saldo")
            '        End With
            '    Next
            'End Using

            Dim no As Integer = 0
            SQL = "select a.Kode_Account,b.Keterangan as Nama,a.Keterangan,a.Saldo,a.Saldo_BB,a.Selisih "
            SQL = SQL & "from Rekon_Bank a,Detail_Account b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Account = b.Kode_Account "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Tanggal = '" & Format(CDate(LvPengajuanToken.FocusedItem.SubItems(1).Text), "yyyy-MM-dd") & "' "
            Using Ds = BindingTrans(SQL)
                For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
                    With Ds.Tables("MyTable").Rows(i)
                        Dim lvi As ListViewItem
                        lvi = ListView1.Items.Add(.Item("Kode_Account"))
                        lvi.SubItems.Add(.Item("Nama"))
                        lvi.SubItems.Add(.Item("Keterangan"))
                        lvi.SubItems.Add(Format(.Item("Saldo"), "N0"))
                        lvi.SubItems.Add(Format(.Item("Saldo_BB"), "N0"))
                        lvi.SubItems.Add(Format(.Item("Selisih"), "N0"))

                        DataGridView1.Rows.Add(1)
                        DataGridView1.Rows.Item(no).Cells(0).Value = .Item("Nama") & Chr(13) & .Item("Keterangan")
                        DataGridView1.Rows.Item(no).Cells(1).Value = Format(.Item("Selisih"), "N0")
                        DataGridView1.Rows.Item(no).Cells(2).Value = Format(.Item("Saldo"), "N0")
                        DataGridView1.Rows.Item(no).Cells(3).Value = Format(.Item("Saldo_BB"), "N0")
                        DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                        no = no + 1

                        total_saldo = total_saldo + .Item("saldo")
                    End With
                Next
            End Using

            CloseConn()
        Catch ex As Exception
            LvDetailPengajuanToken.Items.Clear()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        TextBox3.Text = Format(total_saldo, "N0")
        TextBox4.Text = Format(total_biaya, "N0")

        HitungTotalCheque()
    End Sub

    Private Sub LvPengajuanToken_ControlAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles LvPengajuanToken.ControlAdded

    End Sub


    Private Sub LvPengajuanToken_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LvPengajuanToken.SelectedIndexChanged
        'LvDetailPengajuanBiaya.Items.Clear()
        LvDetailPengajuanToken.Items.Clear()

        ' LvPengajuanBiaya.Items.Clear()

        'LvTotalCheque.Items.Clear()
        'ListView1.Items.Clear()
        'ListView2.Items.Clear()
        'ListView3.Items.Clear()
        'TextBox1.Text = "0"
        'TextBox2.Text = "0"
        'TextBox3.Text = "0"
        TextBox4.Text = "0"
        'TextBox5.Text = "0"
        'TextBox6.Text = "0"
    End Sub

    Private Sub ValidasiTandaTanganToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ValidasiTandaTanganToolStripMenuItem.Click
        If LvCheque_Tanda_Tangan.Items.Count = 0 Or LvCheque_Tanda_Tangan.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu Cheque yang mau ditanda tangan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tanya As String = MessageBox.Show("Yakin akan menanda tangani cheque ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tanya = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction

                If CekButtonRole("tanda_tangan_cheque") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                Dim stok As String = ""
                SQL = "select stok, flag_tanda_tangan_sementara,batal,flag_tanda_tangan from cheque where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_rek = '" & LvCheque_Tanda_Tangan.FocusedItem.Text & "' and no_cheque = '" & LvCheque_Tanda_Tangan.FocusedItem.SubItems(1).Text & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            stok = (.Rows(0).Item("stok"))
                            If stok = "0" Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Cheque tidak bisa ditanda tangan, karena cheque sudah dipakai sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            Else
                                If General_Class.CekNULL(.Rows(0).Item("flag_tanda_tangan_sementara")) = "" Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Cheque tidak bisa ditanda tangan, karena cheque belum diajukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                Else
                                    If General_Class.CekNULL(.Rows(0).Item("batal")) <> "" Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Cheque tidak bisa ditanda tangan, karena sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Exit Sub
                                    Else
                                        If General_Class.CekNULL(.Rows(0).Item("flag_tanda_tangan")) <> "" Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Cheque tidak bisa ditanda tangan, karena sudah ditanda tangan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Exit Sub
                                        Else
                                            SQL = "update cheque set Flag_tanda_tangan ='Y', tgl_tanda_tangan = '" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', jam_tanda_tangan = '" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "',user_tanda_tangan = '" & UserID & "' "
                                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_rek = '" & LvCheque_Tanda_Tangan.FocusedItem.Text & "' and no_cheque = '" & LvCheque_Tanda_Tangan.FocusedItem.SubItems(1).Text & "'"
                                            ExecuteTrans(SQL)
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Cheque tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

                SQL = "insert into log_cheque(kode_perusahaan, no_rek, no_cheque, tanggal, jam, userid,keterangan) values("
                SQL = SQL & "'" & KodePerusahaan & "', '" & LvCheque_Tanda_Tangan.FocusedItem.Text & "', '" & LvCheque_Tanda_Tangan.FocusedItem.SubItems(1).Text & "',"
                SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                SQL = SQL & "'" & UserID & "', 'VTT')"
                ExecuteTrans(SQL)
                Cmd.Transaction.Commit()

                CloseConn()

                MessageBox.Show("Cheque berhasil ditanda tangan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub

            End Try
        End If

        Kosong_Cheque()
    End Sub

    Private Sub BatalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BatalToolStripMenuItem.Click
        If LvPengajuanBiaya.Items.Count = 0 Or LvPengajuanBiaya.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu pengajuan biaya yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim tanya As String = MessageBox.Show("Yakin akan membatalkan transaksi ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tanya = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction

                If CekButtonRole("batal_pengajuan_biaya") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                SQL = "select status,sudah_val_selesai, validasi from Pengajuan_cheque where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            If General_Class.CekNULL(.Rows(0).Item("status")) <> "" Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pengajuan biaya sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf General_Class.CekNULL(.Rows(0).Item("validasi")) = "" Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pengajuan biaya belum divalidasi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf General_Class.CekNULL(.Rows(0).Item("sudah_val_selesai")) <> "" Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pengajuan biaya sudah divalidasi selesai sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            Else
                                SQL = "update pengajuan_cheque set status ='Y' where kode_perusahaan = '" & KodePerusahaan & "' and no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "'"
                                ExecuteTrans(SQL)
                            End If

                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Pengajuan Biaya tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

                SQL = "select no_cheque,no_rek from detail_pengajuan_cheque2 where kode_perusahaan = '" & KodePerusahaan & "' and no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                SQL = "select no_rek,no_cheque,stok from cheque where kode_perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and no_rek = '" & .Rows(i).Item("no_rek") & "' and no_cheque = '" & .Rows(i).Item("no_cheque") & "'"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        If Dr("stok") <> 0 Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Cheque belum di pakai", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        Else
                                            Dr.Close()
                                            SQL = "update cheque set stok ='1' where kode_perusahaan = '" & KodePerusahaan & "' and no_rek = '" & .Rows(i).Item("no_rek") & "' and no_cheque = '" & .Rows(i).Item("no_cheque") & "'"
                                            ExecuteTrans(SQL)
                                        End If
                                    End If
                                End Using
                            Next
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("cheque tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

                SQL = "select urut_pengajuan, asal from detail_pengajuan_cheque where kode_perusahaan = '" & KodePerusahaan & "' and no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1

                                If General_Class.CekNULL(.Rows(i).Item("asal")) = "" Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Asal dari Pengajuan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub

                                ElseIf .Rows(i).Item("asal") = "Biaya" Then

                                    SQL = "select urut, val2 from detail_pengajuan where kode_perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and urut = '" & .Rows(i).Item("urut_pengajuan") & "'"
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            If General_Class.CekNULL(Dr("val2")) = "" Then
                                                Dr.Close()
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Pengajuan belum divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            Else
                                                Dr.Close()
                                                SQL = "update detail_pengajuan set val2 = null where kode_perusahaan = '" & KodePerusahaan & "' and urut = '" & .Rows(i).Item("urut_pengajuan") & "'"
                                                ExecuteTrans(SQL)
                                            End If
                                        End If
                                    End Using

                                ElseIf .Rows(i).Item("asal") = "Pelunasan" Then

                                    SQL = "select urut, val2 from detail_pengajuan_Temp where kode_perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and urut = '" & .Rows(i).Item("urut_pengajuan") & "'"
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            If General_Class.CekNULL(Dr("val2")) = "" Then
                                                Dr.Close()
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Pengajuan belum divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            Else
                                                Dr.Close()
                                                SQL = "update detail_pengajuan_Temp set val2 = null where kode_perusahaan = '" & KodePerusahaan & "' and urut = '" & .Rows(i).Item("urut_pengajuan") & "'"
                                                ExecuteTrans(SQL)
                                            End If
                                        End If
                                    End Using

                                End If
                            Next

                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Pengajuan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

                'awal coding stenly
                SQL = "select kode_voucher_jrn from detail_pengajuan_cheque3 where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                If General_Class.CekNULL(.Rows(i).Item("kode_voucher_jrn")) <> "" Then
                                    SQL = "delete from jurnal where kode_perusahaan = '" & KodePerusahaan & "' and kode_voucher = '" & .Rows(i).Item("kode_voucher_jrn") & "'"
                                    ExecuteTrans(SQL)
                                End If
                            Next
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Pengajuan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

                SQL = "select kode_voucher_sementara from detail_pengajuan_cheque3 where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                If General_Class.CekNULL(.Rows(i).Item("kode_voucher_sementara")) <> "" Then
                                    SQL = "delete from jurnal where kode_perusahaan = '" & KodePerusahaan & "' and kode_voucher = '" & .Rows(i).Item("kode_voucher_sementara") & "'"
                                    ExecuteTrans(SQL)
                                End If
                            Next
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Pengajuan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using
                'akhir coding stenly

                Cmd.Transaction.Commit()
                CloseConn()
                MessageBox.Show("Pengajuan biaya berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
        BtnRefresh_Click(Me, Nothing)
    End Sub

    Private Sub BatalToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BatalToolStripMenuItem1.Click
        If LvPengajuanToken.Items.Count = 0 Or LvPengajuanToken.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu pengajuan biaya yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim tanya As String = MessageBox.Show("Yakin akan membatalkan transaksi ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tanya = vbYes Then
            Try
                OpenConn()
                OpenConnSQL()
                Cmd.Transaction = Cn.BeginTransaction
                CmdSQL.Transaction = CnSQL.BeginTransaction

                If CekButtonRole("batal_pengajuan_token") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                SQL = "select Flag_Kirim_Wa from Pengajuan_token_ok where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_pengajuan = '" & LvPengajuanToken.FocusedItem.Text & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            If General_Class.CekNULL(.Rows(0).Item("Flag_Kirim_Wa")) = "Y" Then
                                SQLSQL = "select Acc,Tolak from approval_pengajuan_token where "
                                SQLSQL = SQLSQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQLSQL = SQLSQL & "no_pengajuan = '" & LvPengajuanToken.FocusedItem.Text & "' "
                                Using DsSQL = BindingTransSQL(SQLSQL)
                                    With DsSQL.Tables("MyTable")
                                        If .Rows.Count <> 0 Then
                                            If General_Class.CekNULL(.Rows(0).Item("ACC")) <> "" Then
                                                CloseTrans()
                                                CloseTransSQL()
                                                CloseConn()
                                                CloseConnSQL()
                                                MessageBox.Show("Pengajuan token sudah divalidasi web selesai sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                Exit Sub
                                            ElseIf General_Class.CekNULL(.Rows(0).Item("Tolak")) = "Y" Then
                                                CloseTrans()
                                                CloseTransSQL()
                                                CloseConn()
                                                CloseConnSQL()
                                                MessageBox.Show("Pengajuan token sudah ditolak web selesai sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                Exit Sub
                                            End If
                                        Else
                                            CloseTrans()
                                            CloseTransSQL()
                                            CloseConn()
                                            CloseConnSQL()
                                            MessageBox.Show("Pengajuan token tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End With
                                End Using
                            End If
                        Else
                            CloseTrans()
                            CloseTransSQL()
                            CloseConn()
                            CloseConnSQL()
                            MessageBox.Show("Pengajuan token tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

                SQL = "select status,sudah_val_selesai, validasi from Pengajuan_token_ok where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_pengajuan = '" & LvPengajuanToken.FocusedItem.Text & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            If General_Class.CekNULL(.Rows(0).Item("status")) <> "" Then
                                CloseTrans()
                                CloseTransSQL()
                                CloseConn()
                                CloseConnSQL()
                                MessageBox.Show("Pengajuan token sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            Else
                                If General_Class.CekNULL(.Rows(0).Item("validasi")) = "" Then
                                    CloseTrans()
                                    CloseTransSQL()
                                    CloseConn()
                                    CloseConnSQL()
                                    MessageBox.Show("Pengajuan token belum divalidasi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                Else
                                    If General_Class.CekNULL(.Rows(0).Item("sudah_val_selesai")) <> "" Then
                                        CloseTrans()
                                        CloseTransSQL()
                                        CloseConn()
                                        CloseConnSQL()
                                        MessageBox.Show("Pengajuan token sudah divalidasi selesai sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Exit Sub
                                    Else
                                        SQL = "update pengajuan_token_ok set status ='Y' where kode_perusahaan = '" & KodePerusahaan & "' and no_pengajuan = '" & LvPengajuanToken.FocusedItem.Text & "'"
                                        ExecuteTrans(SQL)
                                    End If
                                End If
                            End If
                        Else
                            CloseTrans()
                            CloseTransSQL()
                            CloseConn()
                            CloseConnSQL()
                            MessageBox.Show("Pengajuan token tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

                SQL = "select urut_pengajuan, asal from detail_pengajuan_token_ok where kode_perusahaan = '" & KodePerusahaan & "' and no_pengajuan = '" & LvPengajuanToken.FocusedItem.Text & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1

                                If General_Class.CekNULL(.Rows(i).Item("asal")) = "" Then
                                    CloseTrans()
                                    CloseTransSQL()
                                    CloseConn()
                                    CloseConnSQL()
                                    MessageBox.Show("Asal dari Pengajuan token tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub

                                ElseIf .Rows(i).Item("asal") = "Biaya" Then

                                    SQL = "select urut, val2 from detail_pengajuan_token where kode_perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and urut = '" & .Rows(i).Item("urut_pengajuan") & "'"
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            If General_Class.CekNULL(Dr("val2")) = "" Then
                                                Dr.Close()
                                                CloseTrans()
                                                CloseTransSQL()
                                                CloseConn()
                                                CloseConnSQL()
                                                MessageBox.Show("Pengajuan belum divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            Else
                                                Dr.Close()
                                                SQL = "update detail_pengajuan_token set val2 = null where kode_perusahaan = '" & KodePerusahaan & "' and urut = '" & .Rows(i).Item("urut_pengajuan") & "'"
                                                ExecuteTrans(SQL)
                                            End If
                                        End If
                                    End Using

                                ElseIf .Rows(i).Item("asal") = "Pelunasan" Then

                                    SQL = "select urut, val2 from detail_pengajuan_Temp where kode_perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and urut = '" & .Rows(i).Item("urut_pengajuan") & "'"
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            If General_Class.CekNULL(Dr("val2")) = "" Then
                                                Dr.Close()
                                                CloseTrans()
                                                CloseTransSQL()
                                                CloseConn()
                                                CloseConnSQL()
                                                MessageBox.Show("Pengajuan belum divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            Else
                                                Dr.Close()
                                                SQL = "update detail_pengajuan_Temp set val2 = null where kode_perusahaan = '" & KodePerusahaan & "' and urut = '" & .Rows(i).Item("urut_pengajuan") & "'"
                                                ExecuteTrans(SQL)
                                            End If
                                        End If
                                    End Using

                                End If
                            Next
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Pengajuan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

                'awal coding stenly
                SQL = "select kode_voucher_jrn from detail_pengajuan_token_ok where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and no_pengajuan = '" & LvPengajuanToken.FocusedItem.Text & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                If General_Class.CekNULL(.Rows(i).Item("kode_voucher_jrn")) <> "" Then
                                    SQL = "delete from jurnal where kode_perusahaan = '" & KodePerusahaan & "' and kode_voucher = '" & .Rows(i).Item("kode_voucher_jrn") & "'"
                                    ExecuteTrans(SQL)
                                End If
                            Next
                        Else
                            CloseTrans()
                            CloseTransSQL()
                            CloseConn()
                            CloseConnSQL()
                            MessageBox.Show("Pengajuan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

                SQL = "select kode_voucher_sementara from detail_pengajuan_token_ok where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and no_pengajuan = '" & LvPengajuanToken.FocusedItem.Text & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                If General_Class.CekNULL(.Rows(i).Item("kode_voucher_sementara")) <> "" Then
                                    SQL = "delete from jurnal where kode_perusahaan = '" & KodePerusahaan & "' and kode_voucher = '" & .Rows(i).Item("kode_voucher_sementara") & "'"
                                    ExecuteTrans(SQL)
                                End If
                            Next
                        Else
                            CloseTrans()
                            CloseTransSQL()
                            CloseConn()
                            CloseConnSQL()
                            MessageBox.Show("Pengajuan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using
                'akhir coding stenly

                Cmd.Transaction.Commit()
                CmdSQL.Transaction.Commit()
                CloseConn()
                CloseConnSQL()
                MessageBox.Show("Pengajuan token berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                CloseTrans()
                CloseTransSQL()
                CloseConn()
                CloseConnSQL()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If

        LvPengajuanToken.FocusedItem.Remove()
        LvDetailPengajuanToken.Items.Clear()


        '  BtnRefresh_Click(Me, Nothing)
    End Sub

    Private Sub ValidasiSelesaiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ValidasiSelesaiToolStripMenuItem.Click
        If LvPengajuanToken.Items.Count = 0 Or LvPengajuanToken.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu pengajuan token yang akan divalidasi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If


        Dim tanya As String = MessageBox.Show("Yakin akan divalidasi?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tanya = vbNo Then Exit Sub

        Try
            OpenConn()
            OpenConnSQL()
            Cmd.Transaction = Cn.BeginTransaction
            CmdSQL.Transaction = CnSQL.BeginTransaction

            If CekButtonRole("Validasi_Selesai_Pengajuan_Token") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            SQL = "select Flag_Kirim_Wa from Pengajuan_token_ok where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "no_pengajuan = '" & LvPengajuanToken.FocusedItem.Text & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        If General_Class.CekNULL(.Rows(0).Item("Flag_Kirim_Wa")) = "Y" Then
                            SQLSQL = "select Acc, Tolak from approval_pengajuan_token where "
                            SQLSQL = SQLSQL & "pengajuan_token_id = '" & LvPengajuanToken.FocusedItem.Text & "' "
                            Using DsSQL = BindingTransSQL(SQLSQL)
                                With DsSQL.Tables("MyTable")
                                    If .Rows.Count <> 0 Then
                                        If General_Class.CekNULL(.Rows(0).Item("ACC")) = "" Then
                                            CloseTrans()
                                            CloseTransSQL()
                                            CloseConn()
                                            CloseConnSQL()
                                            MessageBox.Show("Pengajuan token belum divalidasi web !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Exit Sub
                                        ElseIf General_Class.CekNULL(.Rows(0).Item("Tolak")) = "Y" Then
                                            CloseTrans()
                                            CloseTransSQL()
                                            CloseConn()
                                            CloseConnSQL()
                                            MessageBox.Show("Pengajuan token sudah ditolak web selesai sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Exit Sub
                                        End If
                                    Else
                                        CloseTrans()
                                        CloseTransSQL()
                                        CloseConn()
                                        CloseConnSQL()
                                        MessageBox.Show("Pengajuan token tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End With
                            End Using
                        End If
                    Else
                        CloseTrans()
                        CloseTransSQL()
                        CloseConn()
                        CloseConnSQL()
                        MessageBox.Show("Pengajuan token tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            SQL = "SELECT status, CAST(rv AS BIGINT) rv, Sudah_Val_Selesai FROM pengajuan_token_ok WHERE kode_perusahaan = '" & KodePerusahaan & "' AND "
            SQL = SQL & "no_pengajuan = '" & LvPengajuanToken.FocusedItem.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    ' cek rv
                    If Dr("rv") <> LvPengajuanToken.FocusedItem.SubItems(5).Text Then
                        CloseTrans()
                        CloseTransSQL()
                        CloseConn()
                        CloseConnSQL()
                        MessageBox.Show("Pengajuan token tidak dapat divalidasi karena terdapat perubahan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(Dr("status")) <> "" Then
                        CloseTrans()
                        CloseTransSQL()
                        CloseConn()
                        CloseConnSQL()
                        MessageBox.Show("Pengajuan token sudah dibatalkan sebelumnya.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(Dr("Sudah_Val_Selesai")) <> "" Then
                        CloseTrans()
                        CloseTransSQL()
                        CloseConn()
                        CloseConnSQL()
                        MessageBox.Show("Pengajuan token sudah selesai divalidasi sebelumnya.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseTransSQL()
                    CloseConn()
                    CloseConnSQL()
                    MessageBox.Show("Pengajuan token tidak dapat ditemukan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            'Get_Isi_Listview_Total(LvTotalCheque.FocusedItem.Index)

            'SQL = "SELECT Sudah_Val_Selesai FROM detail_pengajuan_token_ok WHERE kode_perusahaan = '" & KodePerusahaan & "' AND "
            'SQL = SQL & "no_pengajuan = '" & LvPengajuanToken.FocusedItem.Text & "'"
            ''SQL = SQL & "no_rek = '" & LvTtlRekCheque & "' and no_cheque = '" & LvTtlNoCheque & "'"
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        ' cek rv
            '        If General_Class.CekNULL(Dr("Sudah_Val_Selesai")) <> "" Then
            '            Dr.Close()
            '            CloseTrans()
            '            CloseConn()
            '            MessageBox.Show("Pengajuan token sudah selesai divalidasi sebelumnya.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            Exit Sub
            '        Else
            '            Dr.Close()
            '            SQL = "update detail_pengajuan_token_ok set Sudah_Val_Selesai = 'Y' WHERE kode_perusahaan = '" & KodePerusahaan & "' AND "
            '            SQL = SQL & "no_pengajuan = '" & LvPengajuanToken.FocusedItem.Text & "'"
            '            'SQL = SQL & "no_rek = '" & LvTtlRekCheque & "' and no_cheque = '" & LvTtlNoCheque & "'"
            '            ExecuteTrans(SQL)
            '        End If
            '    Else
            '        Dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("Pengajuan token tidak dapat ditemukan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using

            'SQL = "SELECT count(kode_perusahaan) as ttl FROM detail_pengajuan_cheque2 WHERE kode_perusahaan = '" & KodePerusahaan & "' AND "
            'SQL = SQL & "no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "' and "
            'SQL = SQL & "sudah_val_selesai is null"
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        If Dr("ttl") = 0 Then
            'Dr.Close()

            SQL = "select Urut_Pengajuan,Asal from Detail_Pengajuan_Token_Ok where kode_perusahaan = '" & KodePerusahaan & "' AND "
            SQL = SQL & "no_pengajuan = '" & LvPengajuanToken.FocusedItem.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            If .Rows(i).Item("Asal") = "Pelunasan" Then
                                SQL = "update detail_pengajuan_Temp set Flag_pelunasan  = 'Y' where kode_perusahaan = '" & KodePerusahaan & "' AND "
                                SQL = SQL & "Urut = '" & .Rows(i).Item("Urut_Pengajuan") & "' "
                                ExecuteTrans(SQL)
                            End If
                        Next
                    End If
                End With
            End Using

            SQL = "update pengajuan_token_ok set Sudah_Val_Selesai = 'Y' WHERE kode_perusahaan = '" & KodePerusahaan & "' AND "
            SQL = SQL & "no_pengajuan = '" & LvPengajuanToken.FocusedItem.Text & "' "
            ExecuteTrans(SQL)

            '        End If
            '    Else
            'Dr.Close()
            'CloseTrans()
            'CloseConn()
            'MessageBox.Show("Pengajuan cheque tidak dapat ditemukan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'Exit Sub
            '    End If
            'End Using

            Cmd.Transaction.Commit()
            CmdSQL.Transaction.Commit()
            CloseConn()
            CloseConnSQL()

            MessageBox.Show("Berhasil divalidasi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            CloseTrans()
            CloseTransSQL()
            CloseConn()
            CloseConnSQL()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        LvPengajuanToken.FocusedItem.Remove()
        LvDetailPengajuanToken.Items.Clear()
        '  LvPengajuanBiaya_Click(PrintChequeToolStripMenuItem, e)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Kosong_Saldo()
    End Sub

    Private Sub LvDetailPengajuanCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LvDetailPengajuanBiayaCheque.Click
        'Dim total_cheque As Double = 0
        'Dim total_biaya As Double = 0
        'Dim total_saldo As Double = 0

        LvDetailPengajuanBiaya.Items.Clear()
        ' LvDetailPengajuanToken.Items.Clear()

        'LvPengajuanToken.Items.Clear()

        '  LvTotalCheque.Items.Clear()
        '  ListView1.Items.Clear()
        'ListView2.Items.Clear()
        'ListView3.Items.Clear()
        'TextBox1.Text = "0"
        'TextBox2.Text = "0"
        'TextBox3.Text = "0"
        'TextBox4.Text = "0"
        'TextBox5.Text = "0"
        'TextBox6.Text = "0"

        Try
            If LvPengajuanBiaya.Items.Count = 0 Then Exit Sub

            OpenConn()

            Dim kode_unik As String = ""
            LvDetailPengajuanBiaya.Items.Clear()
            SQL = "SELECT da.letak, pc.Kode_unik_rekon, dp.kode_perusahaan, dp.no_pengajuan, dp.urut_oto, dp.jenis_slip, dp.kode_bank_tujuan, dp.NO_REK_TUJUAN, dp.NAMA_PENERIMA, "
            SQL = SQL & "dp.kode_master_acc + dp.kode_acc + dp.kode_detail_acc as kode_akun, da.keterangan nama_akun, dp.keterangan_detail, "
            SQL = SQL & "dp.tgl_jatuh_tempo, dp.jumlah, dp.urut_pengajuan, "
            SQL = SQL & "isnull(("
            SQL = SQL & "select sum(nilai) from detail_pengajuan_cheque3 x where x.kode_perusahaan = dp.kode_perusahaan and "
            SQL = SQL & "x.urut_pengajuan_cheque = dp.urut_oto"
            SQL = SQL & "), 0) as sudah,dp.Id_Cost_Center, c.Keterangan as Cost_Center "
            SQL = SQL & "FROM detail_pengajuan_cheque dp, detail_account da,pengajuan_cheque pc,EMI_Master_Cost_Center c "
            SQL = SQL & "WHERE pc.kode_perusahaan = dp.kode_perusahaan and dp.kode_perusahaan = da.kode_perusahaan AND "
            SQL = SQL & "dp.kode_master_acc + dp.kode_acc + dp.kode_detail_acc = da.kode_master_acc + da.kode_acc + da.kode_detail_acc AND "
            SQL = SQL & "dp.kode_perusahaan =  c.kode_perusahaan and dp.Id_Cost_Center = c.Id_Cost_Center and "
            SQL = SQL & "dp.kode_perusahaan = '" & KodePerusahaan & "' AND pc.no_pengajuan = dp.no_pengajuan and "
            SQL = SQL & "dp.no_pengajuan = '" & LvDetailPengajuanBiayaCheque.FocusedItem.SubItems(1).Text & "' AND dp.val IS NULL "
            SQL = SQL & "and dp.Kode_Bank_Tujuan = '" & LvDetailPengajuanBiayaCheque.FocusedItem.SubItems(5).Text & "' "
            SQL = SQL & "and dp.no_rek_tujuan = '" & LvDetailPengajuanBiayaCheque.FocusedItem.SubItems(2).Text & "' "
            SQL = SQL & "ORDER BY dp.urut_oto"
            Using Ds = BindingTrans(SQL)
                For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
                    With Ds.Tables("MyTable").Rows(i)
                        Dim lvi As ListViewItem
                        lvi = LvDetailPengajuanBiaya.Items.Add(i + 1)
                        lvi.SubItems.Add(.Item("no_pengajuan"))
                        lvi.SubItems.Add(.Item("kode_akun"))
                        lvi.SubItems.Add(.Item("nama_akun"))
                        lvi.SubItems.Add(.Item("keterangan_detail"))
                        lvi.SubItems.Add(Format(.Item("tgl_jatuh_tempo"), "dd MMM yyyy"))
                        lvi.SubItems.Add(Format(.Item("jumlah"), "N0"))
                        lvi.SubItems.Add(Format(.Item("sudah"), "N0"))
                        lvi.SubItems.Add(Format(.Item("jumlah") - .Item("sudah"), "N0"))
                        lvi.SubItems.Add(.Item("urut_pengajuan"))
                        lvi.SubItems.Add("")
                        lvi.SubItems.Add(.Item("kode_bank_tujuan"))
                        lvi.SubItems.Add(.Item("NO_REK_TUJUAN"))
                        lvi.SubItems.Add(.Item("NAMA_PENERIMA"))
                        lvi.SubItems.Add(.Item("urut_oto"))

                        'total_biaya = total_biaya + .Item("jumlah")
                        'kode_unik = (.Item("Kode_unik_rekon"))

                        If (.Item("letak")) = "Neraca" Then
                            LvDetailPengajuanBiaya.Items(i).ForeColor = Color.Red
                        End If

                    End With
                Next
            End Using




            'LvTotalCheque.Items.Clear()
            'SQL = "SELECT a.kode_perusahaan, a.urut, a.sudah_print, b.kode_bank, b.no_rek, a.no_cheque, a.nilai, a.sudah_val_selesai "
            'SQL = SQL & "FROM detail_pengajuan_cheque2 a, rekening b "
            'SQL = SQL & "WHERE a.kode_perusahaan = b.kode_perusahaan AND "
            'SQL = SQL & "a.no_rek = b.no_rek and "
            'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' AND "
            'SQL = SQL & "a.no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "' and a.sudah_val_selesai is null "
            'SQL = SQL & "ORDER BY a.urut"
            'Using Ds = BindingTrans(SQL)
            '    For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
            '        With Ds.Tables("MyTable").Rows(i)
            '            Dim lvi As ListViewItem
            '            lvi = LvTotalCheque.Items.Add(.Item("kode_bank"))
            '            lvi.SubItems.Add(.Item("no_rek"))
            '            lvi.SubItems.Add(.Item("no_cheque"))
            '            lvi.SubItems.Add(Format(.Item("nilai"), "N0"))

            '            total_cheque = total_cheque + .Item("nilai")
            '        End With
            '    Next
            'End Using

            'join tabel rekening dengan rekon_bank (join berdasar kode_akun) order by kode_bank
            'ListView1.Items.Clear()
            'SQL = "select a.kode_bank, a.no_rek, a.nama_rek, b.saldo from rekening a, rekon_bank b "
            'SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and a.kode_akun = b.kode_account and a.kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and b.kode_unik_rekon = '" & kode_unik & "' order by a.kode_bank,a.nama_rek"
            'Using Ds = BindingTrans(SQL)
            '    For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
            '        With Ds.Tables("MyTable").Rows(i)
            '            Dim lvi As ListViewItem
            '            lvi = ListView1.Items.Add(.Item("kode_bank"))
            '            lvi.SubItems.Add(.Item("no_rek"))
            '            lvi.SubItems.Add(.Item("nama_rek"))
            '            lvi.SubItems.Add(Format(.Item("saldo"), "N0"))

            '            total_saldo = total_saldo + .Item("saldo")
            '        End With
            '    Next
            'End Using

            CloseConn()
        Catch ex As Exception
            LvDetailPengajuanBiaya.Items.Clear()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'TextBox1.Text = Format(total_cheque, "N0")
        'TextBox2.Text = Format(total_biaya, "N0")
        'TextBox3.Text = Format(total_saldo, "N0")

        HitungTotalCheque()
    End Sub






    Private Sub ListView5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LvDetailPengajuanToken1.Click
        'Dim total_cheque As Double = 0
        'Dim total_biaya As Double = 0
        'Dim total_saldo As Double = 0

        ' LvDetailPengajuanBiaya.Items.Clear()
        LvDetailPengajuanToken.Items.Clear()

        '  LvPengajuanBiaya.Items.Clear()

        ' LvTotalCheque.Items.Clear()
        'ListView1.Items.Clear()
        'ListView2.Items.Clear()
        'ListView3.Items.Clear()
        'TextBox1.Text = "0"
        'TextBox2.Text = "0"
        'TextBox3.Text = "0"
        '   TextBox4.Text = "0"
        'TextBox5.Text = "0"
        'TextBox6.Text = "0"

        Try
            If LvPengajuanToken.Items.Count = 0 Then Exit Sub

            OpenConn()

            Dim kode_unik As String = ""
            LvDetailPengajuanToken.Items.Clear()
            SQL = "SELECT d.kode_bank as kode_bank_dari, d.no_rek as no_rek_dari, d.nama_rek as nama_rek_dari, d.kode_akun as kode_akun_dari, "
            SQL = SQL & "b.kode_bank_tujuan, b.NO_REK_TUJUAN, b.NAMA_PENERIMA, b.val2, "
            SQL = SQL & "a.no_pengajuan, a.tanggal, a.jam, CAST(rv AS BIGINT) rv, a.tanggal + a.jam as tgl, "
            SQL = SQL & "b.kode_master_acc + b.kode_acc + b.kode_detail_acc as kode_account, c.keterangan as nama_akun, b.keterangan_detail, "
            SQL = SQL & "b.tgl_jatuh_tempo, b.jumlah, b.urut_oto,b.Id_Cost_Center, e.Keterangan as Cost_Center  "
            SQL = SQL & "FROM pengajuan_token_ok a, detail_pengajuan_token_ok b, detail_account c, rekening d, EMI_Master_Cost_Center e WHERE "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and "
            SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan and a.no_pengajuan = b.no_pengajuan and "
            SQL = SQL & "b.kode_master_acc + b.kode_acc + b.kode_detail_acc = c.kode_master_acc + c.kode_acc + c.kode_detail_acc and "
            SQL = SQL & "a.no_rek = d.no_rek and b.kode_perusahaan = e.kode_perusahaan and b.Id_Cost_Center = e.Id_Cost_Center and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' AND a.status IS NULL AND a.validasi = 'Y' and "
            SQL = SQL & "b.val = 'Y' and b.batal is null and b.val2 is null and a.no_pengajuan = '" & LvDetailPengajuanToken1.FocusedItem.SubItems(1).Text & "' "
            SQL = SQL & "and b.Kode_Bank_Tujuan = '" & LvDetailPengajuanToken1.FocusedItem.SubItems(5).Text & "' "
            SQL = SQL & "and b.no_rek_tujuan = '" & LvDetailPengajuanToken1.FocusedItem.SubItems(2).Text & "' "
            SQL = SQL & "ORDER BY a.tanggal + a.jam, c.keterangan"
            Using Ds = BindingTrans(SQL)
                For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
                    With Ds.Tables("MyTable").Rows(i)
                        Dim lvi As ListViewItem
                        lvi = LvDetailPengajuanToken.Items.Add(i + 1)
                        lvi.SubItems.Add(.Item("no_pengajuan"))
                        lvi.SubItems.Add(Format(.Item("tgl"), "dd MMM yyyy"))
                        lvi.SubItems.Add(.Item("kode_account"))
                        lvi.SubItems.Add(.Item("nama_akun"))
                        lvi.SubItems.Add(.Item("keterangan_detail"))
                        lvi.SubItems.Add(.Item("Cost_Center"))
                        lvi.SubItems.Add(Format(.Item("tgl_jatuh_tempo"), "dd MMM yyyy"))
                        lvi.SubItems.Add(Format(.Item("jumlah"), "N0"))
                        lvi.SubItems.Add(.Item("kode_bank_tujuan"))
                        lvi.SubItems.Add(.Item("NO_REK_TUJUAN"))
                        lvi.SubItems.Add(.Item("NAMA_PENERIMA"))
                        lvi.SubItems.Add(.Item("urut_oto"))
                        lvi.SubItems.Add(.Item("rv"))
                        If General_Class.CekNULL(.Item("val2")) = "" Then
                            lvi.SubItems.Add("Belum Val 2")
                            lvi.SubItems.Add("T")
                        Else
                            lvi.SubItems.Add("Sudah Val 2")
                            lvi.SubItems.Add("Y")
                        End If

                        lvi.SubItems.Add(.Item("kode_bank_dari"))
                        lvi.SubItems.Add(.Item("no_rek_dari"))
                        lvi.SubItems.Add(.Item("nama_rek_dari"))
                        lvi.SubItems.Add(.Item("kode_akun_dari"))
                        lvi.SubItems.Add(.Item("id_Cost_Center"))

                    End With
                Next
            End Using

            'join tabel rekening dengan rekon_bank (join berdasar kode_akun) order by kode_bank
            'ListView1.Items.Clear()
            'SQL = "select a.kode_bank, a.no_rek, a.nama_rek, b.saldo from rekening a, rekon_bank b "
            'SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and a.kode_akun = b.kode_account and a.kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and b.kode_unik_rekon = '" & kode_unik & "' order by a.kode_bank,a.nama_rek"
            'Using Ds = BindingTrans(SQL)
            '    For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
            '        With Ds.Tables("MyTable").Rows(i)
            '            Dim lvi As ListViewItem
            '            lvi = ListView1.Items.Add(.Item("kode_bank"))
            '            lvi.SubItems.Add(.Item("no_rek"))
            '            lvi.SubItems.Add(.Item("nama_rek"))
            '            lvi.SubItems.Add(Format(.Item("saldo"), "N0"))

            '            total_saldo = total_saldo + .Item("saldo")
            '        End With
            '    Next
            'End Using

            CloseConn()
        Catch ex As Exception
            LvDetailPengajuanToken.Items.Clear()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'TextBox4.Text = Format(total_biaya, "N0")

        'HitungTotalCheque()
    End Sub




    Private Sub CopyNoRekeningToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyNoRekeningToolStripMenuItem.Click
        If LvDetailPengajuanBiayaCheque.Items.Count = 0 Or LvDetailPengajuanBiayaCheque.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu yang mau copy!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(LvDetailPengajuanBiayaCheque.FocusedItem.SubItems(2).Text)

        'LvDetailPengajuanBiayaCheque.Columns.Add("No", 40, HorizontalAlignment.Left)
        'LvDetailPengajuanBiayaCheque.Columns.Add("No Pengajuan", 150, HorizontalAlignment.Left)
        'LvDetailPengajuanBiayaCheque.Columns.Add("Rek. Tujuan", 150, HorizontalAlignment.Center)
        'LvDetailPengajuanBiayaCheque.Columns.Add("Nama Tujuan", 250, HorizontalAlignment.Center)
        'LvDetailPengajuanBiayaCheque.Columns.Add("Jumlah", 150, HorizontalAlignment.Right)
        'LvDetailPengajuanBiayaCheque.Columns.Add("Bank Tujuan", 570, HorizontalAlignment.Left)
        'LvDetailPengajuanBiayaCheque.View = View.Details

    End Sub

    Private Sub CopyNamaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyNamaToolStripMenuItem.Click
        If LvDetailPengajuanBiayaCheque.Items.Count = 0 Or LvDetailPengajuanBiayaCheque.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu yang mau copy!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(LvDetailPengajuanBiayaCheque.FocusedItem.SubItems(3).Text)
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        'LvDetailPengajuanToken1.Columns.Add("No", 40, HorizontalAlignment.Left)
        'LvDetailPengajuanToken1.Columns.Add("No Pengajuan", 150, HorizontalAlignment.Left)
        'LvDetailPengajuanToken1.Columns.Add("Rek. Tujuan", 150, HorizontalAlignment.Center)
        'LvDetailPengajuanToken1.Columns.Add("Nama Tujuan", 250, HorizontalAlignment.Center)
        'LvDetailPengajuanToken1.Columns.Add("Jumlah", 150, HorizontalAlignment.Right)
        'LvDetailPengajuanToken1.Columns.Add("Bank Tujuan", 570, HorizontalAlignment.Left)
        'LvDetailPengajuanToken1.View = View.Details

        If LvDetailPengajuanToken1.Items.Count = 0 Or LvDetailPengajuanToken1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu yang mau copy!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(LvDetailPengajuanToken1.FocusedItem.SubItems(2).Text)

    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        If LvDetailPengajuanToken1.Items.Count = 0 Or LvDetailPengajuanToken1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu yang mau copy!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(LvDetailPengajuanToken1.FocusedItem.SubItems(3).Text)

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If DateTimePicker1.Value > DateTimePicker2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
            DateTimePicker1.Value = CDate(FMenu.ToolStripStatusLabel3.Text) : DateTimePicker2.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
            Exit Sub
        End If
        Try
            OpenConn()

            SQL = "insert into Master_Tgl_Berangkat(Kode_Perusahaan,Tgl_Berangkat_Dari,Tgl_Berangkat_Sampai) values('" & KodePerusahaan & "',"
            SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "','" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "')"
            ExecuteTrans(SQL)

            MessageBox.Show("Simpan Tanggal Berangkat Berhasil", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        BtnRefresh_Click(Button3, e)
    End Sub

    Private Sub LvDetailPengajuanBiayaCheque_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LvDetailPengajuanBiayaCheque.SelectedIndexChanged

    End Sub

    Private Sub LvDetailPengajuanToken1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LvDetailPengajuanToken1.SelectedIndexChanged

    End Sub

    Private Sub ContextMenuStrip4_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip4.Opening

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'If LvTotalCheque.Items.Count = 0 Or LvTotalCheque.SelectedItems.Count = 0 Then
        '    MessageBox.Show("Pilih dahulu cheque yang akan divalidasi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If

        Dim nflag As String = "T"
        For i As Integer = 0 To LvTotalCheque.Items.Count - 1
            If LvTotalCheque.Items(i).Checked = True Then
                nflag = "Y'"
                Exit For
            End If
        Next

        If nflag = "T" Then
            MessageBox.Show("Pilih dahulu cheque yang akan divalidasi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If


        Dim tanya As String = MessageBox.Show("Yakin akan divalidasi?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tanya = vbNo Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("validasi_selesai_pengajuan") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            SQL = "SELECT status, CAST(rv AS BIGINT) rv, Sudah_Val_Selesai FROM pengajuan_cheque WHERE kode_perusahaan = '" & KodePerusahaan & "' AND "
            SQL = SQL & "no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    ' cek rv
                    If Dr("rv") <> LvPengajuanBiaya.FocusedItem.SubItems(5).Text Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Pengajuan cheque tidak dapat divalidasi karena terdapat perubahan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(Dr("status")) <> "" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Pengajuan cheque sudah dibatalkan sebelumnya.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(Dr("Sudah_Val_Selesai")) <> "" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Pengajuan cheque sudah selesai divalidasi sebelumnya.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pengajuan cheque tidak dapat ditemukan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            'Get_Isi_Listview_Total(LvTotalCheque.FocusedItem.Index)

            For a As Integer = 0 To LvTotalCheque.Items.Count - 1
                Get_Isi_Listview_Total(a)
                Dim xurut As Integer = 0
                Dim xasal As String = ""
                If LvTotalCheque.Items(a).Checked = True Then
                    SQL = "SELECT a.Sudah_Val_Selesai, b.Urut_Pengajuan, c.Asal FROM detail_pengajuan_cheque2 a, detail_pengajuan_cheque b, detail_pengajuan_cheque3 c "
                    SQL = SQL & "WHERE a.kode_perusahaan = '" & KodePerusahaan & "' AND "
                    SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan AND a.no_pengajuan = b.no_pengajuan AND "
                    SQL = SQL & "b.kode_perusahaan = c.kode_perusahaan AND b.Urut_oto = c.Urut_Pengajuan_Cheque AND "
                    SQL = SQL & "a.no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "' and "
                    SQL = SQL & "a.no_rek = '" & LvTtlRekCheque & "' and a.no_cheque = '" & LvTtlNoCheque & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            ' cek rv
                            If General_Class.CekNULL(Dr("Sudah_Val_Selesai")) <> "" Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pengajuan cheque sudah selesai divalidasi sebelumnya.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            Else
                                xasal = Dr("asal")
                                xurut = Dr("Urut_Pengajuan")

                                Dr.Close()
                                SQL = "update detail_pengajuan_cheque2 set Sudah_Val_Selesai = 'Y' WHERE kode_perusahaan = '" & KodePerusahaan & "' AND "
                                SQL = SQL & "no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "' and "
                                SQL = SQL & "no_rek = '" & LvTtlRekCheque & "' and no_cheque = '" & LvTtlNoCheque & "'"
                                ExecuteTrans(SQL)

                                If xasal = "Pelunasan" Then
                                    SQL = "update detail_pengajuan_Temp set Flag_pelunasan  = 'Y' where kode_perusahaan = '" & KodePerusahaan & "' AND "
                                    SQL = SQL & "Urut = '" & xurut & "' "
                                    ExecuteTrans(SQL)
                                End If
                            End If
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Pengajuan cheque tidak dapat ditemukan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using
                End If
            Next

            SQL = "SELECT count(kode_perusahaan) as ttl FROM detail_pengajuan_cheque2 WHERE kode_perusahaan = '" & KodePerusahaan & "' AND "
            SQL = SQL & "no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "' and "
            SQL = SQL & "sudah_val_selesai is null"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("ttl") = 0 Then
                        Dr.Close()
                        SQL = "update pengajuan_cheque set Sudah_Val_Selesai = 'Y' WHERE kode_perusahaan = '" & KodePerusahaan & "' AND "
                        SQL = SQL & "no_pengajuan = '" & LvPengajuanBiaya.FocusedItem.Text & "' "
                        ExecuteTrans(SQL)
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pengajuan cheque tidak dapat ditemukan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Berhasil divalidasi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        LvPengajuanBiaya_Click(Button2, e)
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            For i As Integer = 0 To LvTotalCheque.Items.Count - 1
                LvTotalCheque.Items(i).Checked = True
            Next
        Else
            For i As Integer = 0 To LvTotalCheque.Items.Count - 1
                LvTotalCheque.Items(i).Checked = False
            Next
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            For i As Integer = 0 To ListView6.Items.Count - 1
                ListView6.Items(i).Checked = True
            Next
        Else
            For i As Integer = 0 To ListView6.Items.Count - 1
                ListView6.Items(i).Checked = False
            Next
        End If
    End Sub

    Private Sub ListView6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView6.Click
        Try
            OpenConn()

            If ListView6.Items.Count = 0 Then Exit Sub
            ListView7.Items.Clear()
            SQL = "select a.Kode_Account,b.Keterangan as Perkiraan,a.Keterangan,a.Debit,a.Kredit,a.Lokasi_Detail "
            SQL = SQL & "from Detail_Jurnal_Sementara_New a,Detail_Account b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Account = b.Kode_Account "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.Kode_Voucher = '" & ListView6.FocusedItem.Text & "' "
            SQL = SQL & "order by Nomor "
            Dim DS As DataSet = General_Class.Binding(SQL)
            With DS.Tables("MyTable")
                If Not .Rows.Count = 0 Then
                    For i As Integer = 0 To .Rows.Count - 1
                        Dim Lvw As ListViewItem
                        Lvw = ListView7.Items.Add(.Rows(i).Item("Kode_account"))
                        Lvw.SubItems.Add(.Rows(i).Item("Perkiraan"))
                        Lvw.SubItems.Add(.Rows(i).Item("Keterangan"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item("Debit"), "N0"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item("Kredit"), "N0"))
                        Lvw.SubItems.Add(.Rows(i).Item("lokasi_detail"))
                    Next
                End If
            End With

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Get_No_Faktur(ByVal _tgl As DateTime)
        Kd_Voucher = GetLastNumberEntryJurnal(Format(_tgl, "yyyyMM"), Kd_Jurnal, KodePerusahaan)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim nflag As String = "T"
        For i As Integer = 0 To ListView6.Items.Count - 1
            If ListView6.Items(i).Checked = True Then
                nflag = "Y'"
                Exit For
            End If
        Next

        If nflag = "T" Then
            MessageBox.Show("Pilih dahulu jurnal yang akan divalidasi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tanya As String = MessageBox.Show("Yakin akan divalidasi?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tanya = vbNo Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("validasi_jurnal_sementara") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            For a As Integer = 0 To ListView6.Items.Count - 1
                Get_Isi_Listview_Jurnal(a)
                If ListView6.Items(a).Checked = True Then
                    If CekSudahTutupSaldo(Format(CDate(Lvj_tgl), "yyyy-MM-dd")) = "Y" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Sudah tutup saldo di bulan ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    Dim pagenumber As Integer = 0
                    Get_No_Faktur(CDate(Lvj_tgl))

                    SQL = "Insert Into Jurnal(Kode_Voucher,Tanggal,Jam,Kode_Perusahaan,Kode_Proyek,"
                    SQL = SQL & "Keterangan,JudulBank,KetDK,userid, otomatis) "
                    SQL = SQL & "select '" & Kd_Voucher & "',Tanggal,Jam,Kode_Perusahaan,Kode_Proyek,"
                    SQL = SQL & "Keterangan,JudulBank,KetDK,userid, otomatis from Jurnal_Sementara_New "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Voucher = '" & Lvj_kd_voucher & "' "
                    ExecuteTrans(SQL)

                    SQL = "insert into Detail_Jurnal(Kode_Voucher,Kode_Master_Acc,Kode_Acc,Kode_Detail_Acc,"
                    SQL = SQL & "Kode_Perusahaan,Kode_Proyek,Keterangan,Debit,Kredit,PageNumber,Lokasi_Detail,Kode_Account,Id_Cost_Center) "
                    SQL = SQL & "select '" & Kd_Voucher & "',Kode_Master_Acc,Kode_Acc,Kode_Detail_Acc,Kode_Perusahaan,"
                    SQL = SQL & "Kode_Proyek,Keterangan,Debit,Kredit,PageNumber,Lokasi_Detail,Kode_Account,Id_Cost_Center from "
                    SQL = SQL & "Detail_Jurnal_Sementara_New where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and Kode_Voucher = '" & Lvj_kd_voucher & "' "
                    ExecuteTrans(SQL)

                    SQL = "update Jurnal_Sementara_New set "
                    SQL = SQL & "Val = 'Y',Kode_Vcr = '" & Kd_Voucher & "' "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and Kode_Voucher = '" & Lvj_kd_voucher & "' "
                    ExecuteTrans(SQL)
                End If
            Next

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Berhasil divalidasi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        BtnRefresh_Click(Button4, e)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            OpenConn()

            ListView6.Items.Clear() : ListView7.Items.Clear()
            SQL = "select a.Kode_Voucher,a.Tanggal,a.Jam,a.userid,sum(b.debit) as debit "
            SQL = SQL & "from Jurnal_Sementara_New a,Detail_Jurnal_Sementara_New b "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.val is null and status is null "
            SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Voucher = b.Kode_Voucher "
            SQL = SQL & "group by a.Kode_Voucher,a.Tanggal,a.Jam,a.userid "
            SQL = SQL & "order by a.Tanggal+a.Jam "
            Using Ds = BindingTrans(SQL)
                For i = 0 To Ds.Tables("MyTable").Rows.Count - 1
                    With Ds.Tables("MyTable").Rows(i)
                        Dim lvi As ListViewItem
                        lvi = ListView6.Items.Add(.Item("Kode_Voucher"))
                        lvi.SubItems.Add(Format(.Item("tanggal"), "dd MMM yyyy"))
                        lvi.SubItems.Add(.Item("jam"))
                        lvi.SubItems.Add(.Item("userid"))
                        lvi.SubItems.Add(Format(.Item("debit"), "N0"))
                    End With
                Next
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim nflag As String = "T"
        For i As Integer = 0 To ListView6.Items.Count - 1
            If ListView6.Items(i).Checked = True Then
                nflag = "Y'"
                Exit For
            End If
        Next

        If nflag = "T" Then
            MessageBox.Show("Pilih dahulu jurnal yang akan dibatal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tanya As String = MessageBox.Show("Yakin akan membatalkan jurnal ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tanya = vbNo Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("batal_jurnal_sementara") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            For a As Integer = 0 To ListView6.Items.Count - 1
                Get_Isi_Listview_Jurnal(a)
                If ListView6.Items(a).Checked = True Then
                    If CekSudahTutupSaldo(Format(CDate(Lvj_tgl), "yyyy-MM-dd")) = "Y" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Sudah tutup saldo di bulan ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    SQL = "update Jurnal_Sementara_New set "
                    SQL = SQL & "Status = 'Y' "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and Kode_Voucher = '" & Lvj_kd_voucher & "' "
                    ExecuteTrans(SQL)
                End If
            Next

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        BtnRefresh_Click(Button4, e)
    End Sub
End Class