Public Class Jf_Uang_Masuk_Global_St
    Dim LvCara_Byr As String
    Dim LvKeterangan As String
    Dim LvJml As String
    Dim LvKd_CB As String
    Dim LvKd_Akun As String

    Dim arrCrByr, ArrAkunCB1 As New ArrayList

    Private Sub Validasi_Pembelian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Kosong()
        TxtNoFak.Focus()
    End Sub

    Private Sub Kosong()
        get_jam()
        Tgl.Value = tgl_skg
        Tgl.Enabled = True

        TxtKet.Text = "" : TxtTotal.Text = ""

        LvUMG.Items.Clear()

        Try

            OpenConn()

            Get_No_Faktur()

            CmbCB.Items.Clear() : arrCrByr.Clear() : ArrAkunCB1.Clear()
            CmbCB.Items.Add("-- Cara Bayar --") : arrCrByr.Add("") : ArrAkunCB1.Add("")
            CmbCB.SelectedIndex = 0
            SQL = "select kode_cb, keterangan, kode_account_cb from cara_bayar where kode_perusahaan = '" & KodePerusahaan & "' order by keterangan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    CmbCB.Items.Add(Dr("keterangan")) : arrCrByr.Add(Dr("kode_cb")) : ArrAkunCB1.Add(Dr("kode_account_cb"))
                Loop
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Hitung()
    End Sub

    Private Sub Get_No_Faktur()
        Dim fUG = "UG"
        TxtNoFak.Text = fUG & Format(Tgl.Value, "MMyy") & "-" & _
                             General_Class.Get_Last_Number2("Uang_Masuk_Global", "No_Val", 5, _
                             "Kode_perusahaan", KodePerusahaan, _
                             "And", "substring(No_Val, 1, " & Len(fUG) + 4 & ")", fUG & Format(Tgl.Value, "MMyy"))
    End Sub

    Private Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvCara_Byr = LvUMG.Items(No_Index).Text
        LvKeterangan = LvUMG.Items(No_Index).SubItems(1).Text
        LvJml = LvUMG.Items(No_Index).SubItems(2).Text
        LvKd_CB = LvUMG.Items(No_Index).SubItems(3).Text
        LvKd_Akun = LvUMG.Items(No_Index).SubItems(4).Text
    End Sub

    Private Sub Hitung()
        Dim Grand As Double = 0

        For i As Integer = 0 To LvUMG.Items.Count - 1
            Get_Isi_Listview(i)

            Grand = Grand + Val(HilangkanTanda(LvJml))
        Next

        TxtTotal.Text = (Format(Grand, "N0"))
    End Sub

    Private Sub Kosong_Bawah(ByVal semua As String)
        TxtKeterangan.Text = "" : TxtJumlah.Text = ""


        If semua = "Y" Then
            CmbCB.SelectedIndex = 0 : CmbCB.Focus()
        Else
            TxtKeterangan.Focus()
        End If
    End Sub

    Private Sub Validasi_Penj_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        Tgl.Focus()
    End Sub

    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Kosong()
        Tgl.Focus()
    End Sub

    Private Sub Tgl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Tgl.KeyPress
        If e.KeyChar = Chr(13) Then TxtKet.Focus()
    End Sub

    Private Sub Tgl_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tgl.Leave
        Try

            OpenConn()

            Get_No_Faktur()

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub TxtKet_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtKet.KeyPress
        If e.KeyChar = Chr(13) Then CmbCB.Focus()
    End Sub

    Private Sub LvUMG_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LvUMG.DoubleClick
        TxtKeterangan.Text = LvUMG.FocusedItem.Text
        TxtJumlah.Text = HilangkanTanda(LvUMG.FocusedItem.SubItems(1).Text)

        LvUMG.FocusedItem.Remove()
        TxtJumlah.Focus()
        Hitung()
    End Sub

    Private Sub TxtNoFak_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtNoFak.KeyPress
        If e.KeyChar = Chr(13) Then Tgl.Focus()
    End Sub

    Private Sub TxtNoFak_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNoFak.Leave
        OpenConn()

        Get_No_Faktur()

        CloseConn()
    End Sub

    Private Sub TxtJumlah_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtJumlah.KeyPress
        If e.KeyChar = Chr(13) Then
            'For i As Integer = 0 To LvUMG.Items.Count - 1
            '    If LvUMG.Items(i).Text.Trim.ToUpper = CmbCB.Text.Trim.ToUpper Then
            '        MessageBox.Show("Cara Bayar sudah ada di list!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            '    'If LvUMG.Items(i).SubItems(1).Text.Trim.ToUpper = TxtKeterangan.Text.Trim.ToUpper Then
            '    '    MessageBox.Show("Keterangan sudah ada di list!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    '    Exit Sub
            '    'End If
            'Next

            Dim Keterangan = TxtKeterangan.Text.Trim
            Dim Jumlah = Format(Val(TxtJumlah.Text), "N0")
            Dim Cara_byr = CmbCB.Text.Trim
            Dim Kd_Cara_Byr = arrCrByr.Item(CmbCB.SelectedIndex)
            Dim Kd_Akun = ArrAkunCB1.Item(CmbCB.SelectedIndex)

            If Keterangan.Length = 0 Then
                MessageBox.Show("Keterangan wajib diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TxtKeterangan.Focus()
                Exit Sub
            ElseIf Cara_byr.Length = 0 Then
                MessageBox.Show("Cara bayar wajib diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TxtKeterangan.Focus()
                Exit Sub
            End If

            Dim lv As New ListViewItem
            lv = LvUMG.Items.Add(Cara_byr)
            lv.SubItems.Add(Keterangan)
            lv.SubItems.Add(Jumlah)
            lv.SubItems.Add(Kd_Cara_Byr)
            lv.SubItems.Add(Kd_Akun)

            Hitung()
            Kosong_Bawah("T")

            Dim nanya As String = MessageBox.Show("Input UM lain . . ? ?", "Perhatian", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If nanya = vbYes Then
                TxtKeterangan.Focus()
            Else
                BtnSimpan.Focus()
            End If

        End If
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub BtnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSimpan.Click
        get_jam()

        If TxtNoFak.Text.Trim.Length = 0 Then
            MessageBox.Show("No pelunasan global harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtNoFak.Focus() : Exit Sub
        ElseIf LvUMG.Items.Count = 0 Then
            MessageBox.Show("Yang akan dilunasi harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKeterangan.Focus() : Exit Sub
        ElseIf TxtKet.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKet.Focus() : Exit Sub
            'ElseIf CmbCB.SelectedIndex = -1 Or CmbCB.SelectedIndex = 0 Then
            '    MessageBox.Show("Cara bayar harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    CmbCB.Focus() : Exit Sub
        ElseIf Format(Tgl.Value, "yyyyMM") <> Format(tgl_skg, "yyyyMM") Then
            MessageBox.Show("Pelunasan tidak boleh dibulan mundur!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl.Value = Now.Date : Tgl.Focus() : Exit Sub
        End If

        Dim tny As String = MessageBox.Show("Yakin akan disimpan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If tny = vbNo Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Get_No_Faktur()
            Get_Data_Acc()

            SQL = "INSERT INTO uang_masuk_global(kode_perusahaan, no_val, tanggal, jam, keterangan,uservalidasi, grand) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtNoFak.Text.Trim & "', '" & Format(Tgl.Value, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', '" & TxtKet.Text.Trim & "', "
            SQL = SQL & "'" & UserID & "', " & HilangkanTanda(TxtTotal.Text) & ")"
            ExecuteTrans(SQL)

            For i As Integer = 0 To LvUMG.Items.Count - 1
                Get_Isi_Listview(i)
                Dim Kode_Voucher As String = GetLastNumberJurnal(Format(Tgl.Value, "yyyyMM"), fJU & fUM, KodePerusahaan)

                Dim lksi_jurnal As String = ""
                SQL = "select Lokasi_Default_UM from cara_bayar where kode_perusahaan = '" & KodePerusahaan & "' and kode_cb = '" & LvKd_CB.Trim & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        lksi_jurnal = Dr("Lokasi_Default_UM")
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Lokasi cara bayar tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "INSERT INTO detail_uang_masuk_global(kode_perusahaan, no_val, keterangan, byr, Nomor,cara_bayar,kode_voucher) "
                SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtNoFak.Text.Trim & "', "
                SQL = SQL & "'" & LvKeterangan.Trim & "', " & HilangkanTanda(LvJml) & ", '" & i & "','" & LvKd_CB.Trim & "','" & Kode_Voucher & "')"
                ExecuteTrans(SQL)

                SQL = "insert into um_global(kode_perusahaan, no_val, kode_cb, Nomor, tanggal, sisa,kode_voucher) "
                SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtNoFak.Text.Trim & "', '" & LvKd_CB.Trim & "', "
                SQL = SQL & "'" & i & "', '" & Format(Tgl.Value, "yyyy-MM-dd") & "', " & HilangkanTanda(LvJml) & ",'" & Kode_Voucher & "')"
                ExecuteTrans(SQL)

                Dim pagenumber As Integer = 0

                SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                SQL = SQL & "'" & Kode_Voucher & "', "
                SQL = SQL & "'" & Format(Tgl.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                SQL = SQL & "'" & KodeProyek & "', '" & LvKd_CB & "; UM Reseller; " & TxtNoFak.Text.Trim & "', '', "
                SQL = SQL & "'-', '" & UserID & "')"
                ExecuteTrans(SQL)

                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(LvKd_Akun, 1),
                               Strings.Mid(LvKd_Akun, 2, 1),
                               Strings.Mid(Ganti(LvKd_Akun), 3),
                               KodePerusahaan, KodeProyek, LvKd_CB & "; " & "UM Reseller" & "; " & TxtNoFak.Text.Trim, HilangkanTanda(LvJml), "0", pagenumber + 1, lksi_jurnal)
                ExecuteTrans(SQL)

                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(X_Pelunasan_Dimuka, 1),
                                  Strings.Mid(X_Pelunasan_Dimuka, 2, 1),
                                  Strings.Mid(Ganti(X_Pelunasan_Dimuka), 3),
                                 KodePerusahaan, KodeProyek, LvKd_CB & "; " & "UM Reseller" & "; " & TxtNoFak.Text.Trim, "0", HilangkanTanda(LvJml), pagenumber + 1, Ket_Lokasi_HO)
                ExecuteTrans(SQL)

                SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Dr("debit") <> Dr("kredit") Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "insert into Det_Val_Uang_Masuk(kode_perusahaan, no_val, kode_voucher) values("
                SQL = SQL & "'" & KodePerusahaan & "', '" & TxtNoFak.Text.Trim & "', '" & Kode_Voucher & "')"
                ExecuteTrans(SQL)
            Next

            Cmd.Transaction.Commit()
            CloseConn()

            Dim TanyaCetak As String = MessageBox.Show("Mau dicetak?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If TanyaCetak = vbYes Then
                CeTaK()
            End If

            Kosong()
            Tgl.Focus()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'Try
        '    OpenConn()

        '    Cmd.Transaction = Cn.BeginTransaction

        '    For i As Integer = 0 To LvUMG.Items.Count - 1
        '        Get_Isi_Listview(i)

        '        SQL = "insert into Detail_Val_Uang_Masuk(kode_perusahaan,no_val,kode_customer,byr) "
        '        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtNoFak.Text.Trim & "', "
        '        SQL = SQL & "'" & LvKdCus.Trim & "', " & HilangkanTanda(LvJml) & " )"
        '        ExecuteTrans(SQL)

        '        SQL = "insert into um(kode_perusahaan, no_val, kode_cb, kode_customer, tanggal, sisa) "
        '        SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtNoFak.Text.Trim & "', '" & arrCrByr.Item(CmbCB.SelectedIndex) & "', "
        '        SQL = SQL & "'" & LvKdCus.Trim & "', '" & Format(Tgl.Value, "yyyy-MM-dd") & "', " & HilangkanTanda(LvJml) & ")"
        '        ExecuteTrans(SQL)

        '        SQL = "select um from customers where kode_perusahaan = '" & KodePerusahaan & "' and "
        '        SQL = SQL & "kode_customer = '" & LvKdCus.Trim & "'"
        '        Using Dr = OpenTrans(SQL)
        '            If Dr.Read Then
        '                Dr.Close()

        '                SQL = "update customers set um = um + " & HilangkanTanda(LvJml) & " where "
        '                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
        '                SQL = SQL & "kode_customer = '" & LvKdCus.Trim & "'"
        '                ExecuteTrans(SQL)

        '            Else
        '                Dr.Close()
        '                CloseTrans()
        '                CloseConn()
        '                MessageBox.Show("Customer tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                Exit Sub
        '            End If
        '        End Using

        '        Dim Kode_Voucher As String = GetLastNumberJurnal(Format(Tgl.Value, "yyyyMM"), fJU & fUM, KodePerusahaan)

        '        SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
        '        SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
        '        SQL = SQL & "'" & Kode_Voucher & "', "
        '        SQL = SQL & "'" & Format(Tgl.Value, "yyyy-MM-dd") & "', "
        '        SQL = SQL & "'" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
        '        SQL = SQL & "'" & KodeProyek & "', 'Uang Masuk " & TxtNoFak.Text.Trim & "; " & arrCrByr.Item(CmbCB.SelectedIndex) & "; " & LvKdCus & "; " & LvNmCus & "', '', "
        '        SQL = SQL & "'-', '" & UserID & "')"
        '        ExecuteTrans(SQL)

        '        Dim pagenumber As Integer = 0

        '        SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(ArrAkunCB1.Item(CmbCB.SelectedIndex), 1), _
        '                       Strings.Mid(ArrAkunCB1.Item(CmbCB.SelectedIndex), 2, 1), _
        '                       Strings.Mid(Ganti(ArrAkunCB1.Item(CmbCB.SelectedIndex)), 3), _
        '                       KodePerusahaan, KodeProyek, "Uang Masuk " & TxtNoFak.Text.Trim & "; " & arrCrByr.Item(CmbCB.SelectedIndex) & "; " & LvKdCus & "; " & LvNmCus, HilangkanTanda(LvJml), "0", pagenumber + 1)
        '        ExecuteTrans(SQL)

        '        SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(X_Pelunasan_Dimuka, 1), _
        '                          Strings.Mid(X_Pelunasan_Dimuka, 2, 1), _
        '                          Strings.Mid(Ganti(X_Pelunasan_Dimuka), 3), _
        '                         KodePerusahaan, KodeProyek, "Uang Masuk " & TxtNoFak.Text.Trim & "; " & arrCrByr.Item(CmbCB.SelectedIndex) & "; " & LvKdCus & "; " & LvNmCus, "0", HilangkanTanda(LvJml), pagenumber + 1)
        '        ExecuteTrans(SQL)


        '        SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
        '        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
        '        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "'"
        '        Using Dr = OpenTrans(SQL)
        '            If Dr.Read Then
        '                If Dr("debit") <> Dr("kredit") Then
        '                    Dr.Close()
        '                    CloseTrans()
        '                    CloseConn()
        '                    MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                    Exit Sub
        '                End If
        '            Else
        '                Dr.Close()
        '                CloseTrans()
        '                CloseConn()
        '                MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Exit Sub
        '            End If
        '        End Using


        '        SQL = "insert into Det_Val_Uang_Masuk(kode_perusahaan, no_val, kode_voucher) values("
        '        SQL = SQL & "'" & KodePerusahaan & "', '" & TxtNoFak.Text.Trim & "', '" & Kode_Voucher & "')"
        '        ExecuteTrans(SQL)
        '    Next


        '    Cmd.Transaction.Commit()

        '    CloseConn()
        'Catch ex As Exception
        '    CloseTrans()
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub

    Private Sub CeTaK()
        Try

            OpenConn()

            'SQL = "SELECT 1 FROM uang_masuk_global "
            'SQL = SQL & "WHERE kode_perusahaan = '" & KodePerusahaan & "' AND no_val = '" & TxtNoFak.Text.Trim & "'"
            'Using Ds = BindingTrans(SQL)
            '    If Ds.Tables("MyTable").Rows.Count <> 0 Then
            '        Dim CrDoc As New Jf_Faktur_Uang_Masuk_Global    'Nama file CR
            '        CrDoc.SetDataSource(Ds)
            '        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '        CrDoc.PrintOptions.PrinterName = PrinterName
            '        CrDoc.RecordSelectionFormula = "{uang_masuk_global.Kode_Perusahaan} = '" & KodePerusahaan & "' and {uang_masuk_global.no_val} = '" & TxtNoFak.Text.Trim & "'"

            '        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
            '        doctoprint.PrinterSettings.PrinterName = PrinterName
            '        Dim rawKind As Integer
            '        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
            '        For i = doctoprint.PrinterSettings.PaperSizes.Count - 1 To 0 Step -1
            '            If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Faktur" Then
            '                rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
            '                CrDoc.PrintOptions.PaperSize = rawKind
            '                Exit For
            '            End If
            '        Next

            '        CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
            '        CrDoc.PrintToPrinter(1, False, 1, 99)
            '    End If
            'End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CmbCB_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbCB.KeyPress
        If e.KeyChar = Chr(13) Then TxtKeterangan.Focus()
    End Sub

    Private Sub TxtKeterangan_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtKeterangan.KeyPress
        If e.KeyChar = Chr(13) Then TxtJumlah.Focus()
    End Sub

    Private Sub CmbCB_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbCB.SelectedIndexChanged

    End Sub
End Class