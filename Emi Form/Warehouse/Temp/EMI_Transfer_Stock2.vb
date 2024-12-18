
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class EMI_Transfer_Stock2
    Dim arrlist As New ArrayList
    Dim arrLokAwal, arrLokTujuan As New ArrayList
    Dim ArrSOAwal, ArrSOTujuan, ArrKode, ArrJml, arrInisialFaktur, Arr_COA_Persediaan, Arr_COA_Pending As New ArrayList

    Dim boleh_lihat_global As Boolean
    Dim tgl_skg As DateTime

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        'If Lv_Barang_TS.Items.Count = 0 Then
        '    MessageBox.Show("Belum ada barang yang mau di transfer!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    TxtKd_Barang.Focus() : Exit Sub
        'ElseIf TxtKeterangan.Text.Trim.Length = 0 Then
        '    MessageBox.Show("Keterangan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    TxtKeterangan.Focus() : Exit Sub
        'ElseIf CmbSO_Asal.SelectedIndex = -1 Then
        '    MessageBox.Show("SO asal harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    CmbSO_Asal.Focus() : Exit Sub
        'ElseIf CmbSo_Tujuan.SelectedIndex = -1 Then
        '    MessageBox.Show("SO tujuan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    CmbSo_Tujuan.Focus() : Exit Sub
        'ElseIf CmbSO_Asal.Text = CmbSo_Tujuan.Text Then
        '    MessageBox.Show("SO asal dan so tujuan sama", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    CmbSO_Asal.Focus() : Exit Sub
        'End If

        'get_jam()

        'Try
        '    OpenConn()
        '    Cmd.Transaction = Cn.BeginTransaction

        '    Dim total_hpp As Double = 0
        '    get_no_faktur()
        '    Dim Kode_Voucher As String = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), fJU & arrInisialFaktur(CmbSO_Asal.SelectedIndex), KodePerusahaan)

        '    Dim Ttl_Jumlah As Double = 0
        '    For i As Integer = 0 To Lv_Barang_TS.Items.Count - 1
        '        Ttl_Jumlah = Ttl_Jumlah + Val(HilangkanTanda(Lv_Barang_TS.Items(i).SubItems(4).Text))
        '    Next

        '    'awal coding stenly
        '    arrLokAwal.Clear()
        '    arrLokTujuan.Clear()
        '    Dim Group_awal As String = ""
        '    Dim Group_Tujuan As String = ""
        '    SQL = "select top 1 Group_Lokasi from Stock_Owner_gudang where kode_perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and Kode_Stock_Owner = '" & CmbSO_Asal.Text & "' "
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then
        '            Group_awal = Dr("Group_Lokasi")
        '        Else
        '            Dr.Close()
        '            CloseTrans()
        '            CloseConn()
        '            MessageBox.Show("Lokasi awal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        End If
        '    End Using

        '    SQL = "select top 1 Group_Lokasi from Stock_Owner_gudang where kode_perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and Kode_Stock_Owner = '" & CmbSo_Tujuan.Text & "' "
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then
        '            Group_Tujuan = Dr("Group_Lokasi")
        '        Else
        '            Dr.Close()
        '            CloseTrans()
        '            CloseConn()
        '            MessageBox.Show("Lokasi tujuan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        End If
        '    End Using

        '    Dim idlog As Integer = 0
        '    Dim idlog_untuk_simpan As String = "NULL"
        '    If Group_awal <> Group_Tujuan Then
        '        SQL = "select top 1 id_log from Emi_Log_Buka_TS where kode_perusahaan = '" & KodePerusahaan & "' "
        '        SQL = SQL & "and lokasi_awal = '" & CmbSO_Asal.Text & "' and lokasi_tujuan = '" & CmbSo_Tujuan.Text & "' "
        '        SQL = SQL & "and flag_sudah is null and flag_batal is null and tanggal_validasi_2 is not null "
        '        SQL = SQL & "and tanggal_validasi_3 is not null order by id_log"
        '        Using Dr = OpenTrans(SQL)
        '            If Dr.Read Then
        '                idlog = Dr("id_log")
        '            End If
        '        End Using

        '        If idlog = 0 Then
        '            CloseTrans()
        '            CloseConn()
        '            MessageBox.Show("Belum ada ACC transfer stock dari pusat!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        End If

        '        idlog_untuk_simpan = "'" & idlog & "'"
        '    End If

        '    Dim flag_opm As String = ""
        '    SQL = "select flag_opname,buka_transfer_stock from stock_owner_gudang "
        '    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
        '    SQL = SQL & "kode_stock_owner = '" & CmbSO_Asal.Text & "'"
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then
        '            If Dr("flag_opname") = "Y" Then
        '                If Dr("buka_transfer_stock") = 0 Then
        '                    Dr.Close()
        '                    CloseTrans()
        '                    CloseConn()
        '                    MessageBox.Show(err_msg_opname, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                    Exit Sub
        '                ElseIf Dr("buka_transfer_stock") > 0 Then
        '                    Dr.Close()
        '                    SQL = "update stock_owner set buka_transfer_stock = buka_transfer_stock - 1 "
        '                    SQL = SQL & "where kode_perusahaan ='" & KodePerusahaan & "' and "
        '                    SQL = SQL & "kode_stock_owner = '" & CmbSO_Asal.Text & "'"
        '                    ExecuteTrans(SQL)

        '                    flag_opm = "'Y'"
        '                Else
        '                    Dr.Close()
        '                    CloseTrans()
        '                    CloseConn()
        '                    MessageBox.Show("Data Tidak Ditemukan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                    Exit Sub
        '                End If
        '            Else
        '                flag_opm = "NULL"
        '            End If
        '        Else
        '            Dr.Close()
        '            CloseTrans()
        '            CloseConn()
        '            MessageBox.Show("Data Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        End If

        '    End Using

        '    SQL = "Insert Into Emi_Transfer_Stock(Kode_Perusahaan, Kode_Transfer, Tanggal, jam, Kode_SO_Tujuan, "
        '    SQL = SQL & "UserID, ttl_jumlah, keterangan, kode_voucher, flag_opm, "
        '    SQL = SQL & "id_log, xtermxx) Values('" & KodePerusahaan & "', '" & Trim(TxtNo_Transaksi.Text) & "', "
        '    SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
        '    SQL = SQL & "'" & CmbSo_Tujuan.Text & "', '" & UserID & "', " & Ttl_Jumlah & ", '" & TxtKeterangan.Text.Trim & "', "
        '    SQL = SQL & "'" & Kode_Voucher & "', " & flag_opm & ", " & idlog_untuk_simpan & ", 'x')"
        '    ExecuteTrans(SQL)

        '    For i As Integer = 0 To Lv_Barang_TS.Items.Count - 1
        '        SQL = "Insert Into Emi_Transfer_Stock_Detail(Kode_Perusahaan, "
        '        SQL = SQL & "Kode_Transfer, Kode_SO_Awal, Kode_Barang, Jumlah, x) Values("
        '        SQL = SQL & "'" & KodePerusahaan & "', '" & Trim(TxtNo_Transaksi.Text) & "', "
        '        SQL = SQL & "'" & CmbSO_Asal.Text & "', '" & Lv_Barang_TS.Items(i).SubItems(1).Text & "', "
        '        SQL = SQL & "" & HilangkanTanda(Lv_Barang_TS.Items(i).SubItems(4).Text) & ", '1')"
        '        ExecuteTrans(SQL)

        '        Dim Nama As String = ""
        '        SQL = "Select good_stock, nama From barang Where Kode_Perusahaan = '" & KodePerusahaan & "' and "
        '        SQL = SQL & "kode_stock_owner = '" & CmbSO_Asal.Text & "' and "
        '        SQL = SQL & "kode_barang = '" & Lv_Barang_TS.Items(i).SubItems(1).Text & "'"
        '        Using Dr = OpenTrans(SQL)
        '            If Dr.Read Then
        '                Nama = Dr("nama")
        '                If Dr("good_stock") < Val(HilangkanTanda(Lv_Barang_TS.Items(i).SubItems(4).Text)) Then
        '                    Dr.Close()
        '                    CloseTrans()
        '                    CloseConn()
        '                    MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                    Exit Sub
        '                Else
        '                    Dr.Close()

        '                    SQL = "update barang set good_stock = good_stock - " & Val(HilangkanTanda(Lv_Barang_TS.Items(i).SubItems(4).Text)) & " where "
        '                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
        '                    SQL = SQL & "kode_stock_owner = '" & CmbSO_Asal.Text & "' and "
        '                    SQL = SQL & "kode_barang = '" & Lv_Barang_TS.Items(i).SubItems(1).Text & "'"
        '                    ExecuteTrans(SQL)
        '                End If
        '            Else
        '                Dr.Close()
        '                CloseTrans()
        '                CloseConn()
        '                MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                Exit Sub
        '            End If
        '        End Using

        '        Dim sisa As Double = 0

        '        SQL = "select kode_stock_owner, kode_barang, serial_number, jumlah from barang_sn where "
        '        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
        '        SQL = SQL & "kode_stock_owner = '" & CmbSO_Asal.Text & "' and "
        '        SQL = SQL & "kode_barang = '" & Lv_Barang_TS.Items(i).SubItems(1).Text & "' and jumlah <> 0 "
        '        SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
        '        Using Ds = BindingTrans(SQL)
        '            With Ds.Tables("MyTable")
        '                If .Rows.Count <> 0 Then
        '                    sisa = HilangkanTanda(Lv_Barang_TS.Items(i).SubItems(4).Text)

        '                    For h As Integer = 0 To .Rows.Count - 1
        '                        If sisa = 0 Then
        '                            Exit For
        '                        ElseIf sisa < 0 Then
        '                            CloseTrans()
        '                            CloseConn()
        '                            MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                            Exit Sub
        '                        End If

        '                        If sisa < .Rows(h).Item("jumlah") Or sisa = .Rows(h).Item("jumlah") Then
        '                            SQL = "Update barang_sn set jumlah = jumlah - " & sisa & " where "
        '                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
        '                            SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
        '                            SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
        '                            SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
        '                            ExecuteTrans(SQL)

        '                            SQL = "insert into Emi_Det_TS(kode_perusahaan, kode_transfer, "
        '                            SQL = SQL & "kode_stock_owner, kode_barang, serial_number, no_urut, "
        '                            SQL = SQL & "jumlah) values('" & KodePerusahaan & "', "
        '                            SQL = SQL & "'" & TxtNo_Transaksi.Text.Trim & "', "
        '                            SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
        '                            SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
        '                            SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
        '                            SQL = SQL & "IDENT_CURRENT('detail_transfer_stock'), '" & sisa & "')"
        '                            ExecuteTrans(SQL)

        '                            total_hpp = total_hpp + (sisa * Get_Harga_SN(.Rows(h).Item("serial_number")))

        '                            sisa = 0
        '                        ElseIf sisa > .Rows(h).Item("jumlah") Then
        '                            SQL = "insert into Emi_Det_TS(kode_perusahaan, kode_transfer, "
        '                            SQL = SQL & "kode_stock_owner, kode_barang, serial_number, no_urut, "
        '                            SQL = SQL & "jumlah) values('" & KodePerusahaan & "', "
        '                            SQL = SQL & "'" & TxtNo_Transaksi.Text.Trim & "', "
        '                            SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
        '                            SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
        '                            SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
        '                            SQL = SQL & "IDENT_CURRENT('detail_transfer_stock'), "
        '                            SQL = SQL & "'" & .Rows(h).Item("jumlah") & "')"
        '                            ExecuteTrans(SQL)

        '                            SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
        '                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
        '                            SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
        '                            SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
        '                            SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
        '                            ExecuteTrans(SQL)

        '                            total_hpp = total_hpp + (.Rows(h).Item("jumlah") * Get_Harga_SN(.Rows(h).Item("serial_number")))

        '                            sisa = sisa - .Rows(h).Item("jumlah")
        '                        End If

        '                        If sisa <> 0 And h = .Rows.Count - 1 Then
        '                            CloseTrans()
        '                            CloseConn()
        '                            MessageBox.Show("Jumlah stock tidak mencukupi untuk barang " & Lv_Barang_TS.Items(i).SubItems(2).Text & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                            Exit Sub
        '                        End If
        '                    Next
        '                Else
        '                    CloseTrans()
        '                    CloseConn()
        '                    MessageBox.Show("SN untuk barang " & Lv_Barang_TS.Items(i).SubItems(2).Text & " tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                    Exit Sub
        '                End If
        '            End With
        '        End Using
        '    Next

        '    SQL = "update Emi_Transfer_Stock set grand = " & total_hpp & " where kode_perusahaan = '" & KodePerusahaan & "' and "
        '    SQL = SQL & "kode_transfer = '" & TxtNo_Transaksi.Text.Trim & "'"
        '    ExecuteTrans(SQL)

        '    SQL = "update Emi_Log_Buka_TS set flag_sudah = 'Y', no_ts = '" & TxtNo_Transaksi.Text.Trim & "' where id_log = '" & idlog & "'"
        '    ExecuteTrans(SQL)

        '    Dim pagenumber As Integer = 1

        '    'SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
        '    'SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
        '    'SQL = SQL & "'" & Kode_Voucher & "', "
        '    'SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
        '    'SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
        '    'SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock Keluar " & TxtNo_Transaksi.Text & "', '', "
        '    'SQL = SQL & "'-', '" & UserID & "', '" & CmbSO_Asal.Text & "')"
        '    'ExecuteTrans(SQL)

        '    'SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(Arr_COA_Pending.Item(CmbSo_Tujuan.SelectedIndex), 1),
        '    '                  Strings.Mid(Arr_COA_Pending.Item(CmbSo_Tujuan.SelectedIndex), 2, 1),
        '    '                  Strings.Mid(Ganti(Arr_COA_Pending.Item(CmbSo_Tujuan.SelectedIndex)), 3),
        '    '                  KodePerusahaan, KodeProyek, "Pending Persediaan " & TxtNo_Transaksi.Text.Trim, total_hpp, "0", pagenumber, Lokasi)
        '    'ExecuteTrans(SQL)
        '    'pagenumber = pagenumber + 1

        '    'SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(Arr_COA_Persediaan.Item(CmbSO_Asal.SelectedIndex), 1),
        '    '                   Strings.Mid(Arr_COA_Persediaan.Item(CmbSO_Asal.SelectedIndex), 2, 1),
        '    '                   Strings.Mid(Ganti(Arr_COA_Persediaan.Item(CmbSO_Asal.SelectedIndex)), 3),
        '    '                   KodePerusahaan, KodeProyek, "Persediaan " & TxtNo_Transaksi.Text.Trim, "0", total_hpp, pagenumber, Lokasi)
        '    'ExecuteTrans(SQL)
        '    'pagenumber = pagenumber + 1



        '    Cmd.Transaction.Commit()
        '    CloseConn()

        '    Dim Tanya_Cetak As String = MessageBox.Show("Cetak Faktur?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        '    If Tanya_Cetak = vbYes Then
        '        'cetak()
        '    End If

        '    kosong()
        '    'DateTimePicker1.Focus()
        '    CmbSO_Asal.Focus()

        'Catch ex As Exception
        '    CloseTrans()
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub

    Private Sub cetak()
        Try

            OpenConn()

            SQL = "select kode_perusahaan from detail_transfer_stock where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_transfer = '" & TxtNo_Transaksi.Text.Trim & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Dim CrDoc As New Rpt_Laporan_Bahan_Import   'Nama file CR
                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.PrintOptions.PrinterName = PrinterNameTS
                    CrDoc.RecordSelectionFormula = "{detail_transfer_stock.Kode_Perusahaan} = '" & KodePerusahaan & "' and {detail_transfer_stock.kode_transfer} = '" & TxtNo_Transaksi.Text.Trim & "'"
                    CrDoc.SummaryInfo.ReportTitle = "Faktur Transfer Stock"

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterNameTS
                    Dim rawKind As Integer
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Faktur" Then
                            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                            CrDoc.PrintOptions.PaperSize = rawKind
                            Exit For
                        End If
                    Next

                    CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                    CrDoc.PrintToPrinter(1, False, 1, 99)
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub EMI_Transfer_Stock_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub EMI_Transfer_Stock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()

            kosong()

            'Lv_Barang_TS.Columns.Add("SO Tujuan", 0, HorizontalAlignment.Center)
            'Lv_Barang_TS.Columns.Add("Kode Barang", 120, HorizontalAlignment.Center)
            'Lv_Barang_TS.Columns.Add("Nama", 300, HorizontalAlignment.Left)
            ''iniiii
            'If boleh_lihat_global = True Then
            '    Lv_Barang_TS.Columns.Add("Good Stock", 100, HorizontalAlignment.Right)
            'Else
            '    Lv_Barang_TS.Columns.Add("Good Stock", 0, HorizontalAlignment.Right)
            'End If
            ''iniiii
            ''ListView1.Columns.Add("stock", 70, HorizontalAlignment.Right)
            'Lv_Barang_TS.Columns.Add("Jumlah", 90, HorizontalAlignment.Right)
            ''Lv_Barang_TS.View = View.Details

            ListView2.Location = New Point(25, 293)
            ListView2.Columns.Add("Stock Owner", 0, HorizontalAlignment.Center)
            ListView2.Columns.Add("Kode Barang", 140, HorizontalAlignment.Center)
            ListView2.Columns.Add("Nama", 300, HorizontalAlignment.Left)
            ListView2.Columns.Add("Satuan", 48, HorizontalAlignment.Left)
            'iniiii
            If boleh_lihat_global = True Then
                ListView2.Columns.Add("Good Stock", 70, HorizontalAlignment.Right)
            Else
                ListView2.Columns.Add("Good Stock", 0, HorizontalAlignment.Right)
            End If
            'iniiii
            'ListView2.Columns.Add("Good Stock", 70, HorizontalAlignment.Right)
            'ListView2.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub get_jam()
        Try
            OpenConn()

            SQL = "declare @ab int; select @ab = Selisih_Jam from Init; "
            SQL = SQL & " Select FORMAT(DATEADD(hh, @ab, getdate()), 'yyyy-MM-dd HH:mm:ss') as Tanggal_Sekarang "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    tgl_skg = dr("Tanggal_Sekarang")
                Loop
            End Using

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub get_no_faktur()
        Dim FPro_Results As String = "TS-"
        'TxtNo_Transaksi.Text = FPro_Results & arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy") & "-" &
        '                             General_Class.Get_Last_Number2("Emi_Transfer_Stock", "kode_transfer", JumlahDigit,
        '                             "Kode_perusahaan", KodePerusahaan,
        '                             "And", "substring(kode_transfer,1," & Len(FPro_Results) + Len(arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex)) + 6 & ")", FPro_Results & arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))
        TxtNo_Transaksi.Text = FPro_Results & arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy") & "-" &
                                      General_Class.Get_Last_Number2("Emi_Transfer_Stock", "kode_transfer", JumlahDigit,
                                      "Kode_perusahaan", KodePerusahaan,
                                      "And", "substring(kode_transfer,1," & Len(FPro_Results) + Len(arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex)) + 6 & ")", FPro_Results & arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))

    End Sub
    Private Sub kosong()
        get_jam()
        Try
            OpenConn()

            CmbSo_Tujuan.Items.Clear() : CmbSo_Tujuan.SelectedIndex = -1
            CmbSO_Asal.Items.Clear() : CmbSO_Asal.SelectedIndex = -1
            arrInisialFaktur.Clear() : Arr_COA_Persediaan.Clear() : Arr_COA_Pending.Clear()
            SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan From Stock_Owner_Gudang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' "
            'SQL = SQL & "and Flag_Perusahaan_Lain = 'T' "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSo_Tujuan.Items.Add(dr("kode_stock_owner")) : Arr_COA_Pending.Add(dr("pending_persediaan"))
                    CmbSO_Asal.Items.Add(dr("kode_stock_owner")) : arrInisialFaktur.Add(dr("inisial_faktur")) : Arr_COA_Persediaan.Add(dr("persediaan"))
                Loop
            End Using

            CmbJnsTransfer.Items.Clear()
            CmbJnsTransfer.Items.Add("Antar Rak")
            CmbJnsTransfer.Items.Add("Antar Gudang")

            'CmbSO_Asal.Text = Lokasi
            TxtKeterangan.Text = ""
            TxtKd_Barang.Text = ""
            TxtNm_Barang.Text = ""
            'TxtStock.Text = ""
            TxtJumlah.Text = ""

            'iniiiii
            'Dim boleh_lihat_global As Boolean

            SQL = "select flag_hide_stock, "
            SQL = SQL & "ISNULL(("
            SQL = SQL & "select top(1) 'Y' from role_button a where a.kode_perusahaan = x.kode_perusahaan and "
            SQL = SQL & "a.userid = '" & UserID & "' and buttonname = 'LIHAT_STOCK'"
            SQL = SQL & "), 'T') AS boleh_lihat_stock "
            SQL = SQL & " from stock_owner x where x.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "x.kode_stock_owner = '" & CmbSO_Asal.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("flag_hide_stock") = "Y" Then
                        If Dr("boleh_lihat_stock") = "Y" Then
                            boleh_lihat_global = True
                        Else
                            boleh_lihat_global = False
                        End If
                    Else
                        boleh_lihat_global = True
                    End If
                Else
                    boleh_lihat_global = False
                End If
            End Using

            If CekButtonRole("Ganti_Lokasi_Transfer_Stock") = "T" Then
                CmbSO_Asal.Enabled = False
            Else
                CmbSO_Asal.Enabled = True
            End If

            ListView2.Visible = False
            TxtNo_Transaksi.Text = ""
            'Lv_Barang_TS.Items.Clear()

            'get_no_faktur()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtKd_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtKd_Barang.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView2.Items.Count = 0 Then Exit Sub
            ListView2.Focus()
        ElseIf e.KeyCode = Keys.Enter Then
            TxtJumlah.Focus()
        End If
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub CmbJnsTransfer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbJnsTransfer.SelectedIndexChanged
        If CmbJnsTransfer.SelectedIndex = -1 Then
            Exit Sub
        End If

        If CmbJnsTransfer.SelectedIndex = 0 Then
            CmbSo_Tujuan.Enabled = False
        Else
            CmbSo_Tujuan.Enabled = True

        End If
    End Sub

    Private Sub TxtNm_Barang_TextChanged(sender As Object, e As EventArgs) Handles TxtNm_Barang.TextChanged

    End Sub

    Private Sub TxtKd_Barang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKd_Barang.KeyPress
        If e.KeyChar = Chr(13) Then TxtNm_Barang.Focus()
    End Sub

    Private Sub TxtKd_Barang_Leave(sender As Object, e As EventArgs) Handles TxtKd_Barang.Leave
        If TxtKd_Barang.Text.Trim.Length = 0 Then Exit Sub
        If ListView2.Focused = True Then Exit Sub

        If CmbSO_Asal.SelectedIndex = -1 Then
            MessageBox.Show("SO awal harus diisi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSO_Asal.Focus() : Exit Sub
        End If

        'For i As Integer = 0 To Lv_Barang_TS.Items.Count - 1
        '    If TxtKd_Barang.Text.ToUpper = Lv_Barang_TS.Items(i).SubItems(1).Text.ToUpper Then
        '        MessageBox.Show("Kode barang sudah anda masukkan . . ! !", Judul)
        '        TxtKd_Barang.Text = ""
        '        TxtKd_Barang.Focus()
        '        Exit Sub
        '    End If
        'Next

        Try

            OpenConn()

            'iniiiii
            Dim boleh_lihat As Boolean

            SQL = "select flag_hide_stock, "
            SQL = SQL & "ISNULL(("
            SQL = SQL & "select top(1) 'Y' from role_button a where a.kode_perusahaan = x.kode_perusahaan and "
            SQL = SQL & "a.userid = '" & UserID & "' and buttonname = 'LIHAT_STOCK'"
            SQL = SQL & "), 'T') AS boleh_lihat_stock "
            SQL = SQL & " from stock_owner x where x.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "x.kode_stock_owner = '" & CmbSO_Asal.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("flag_hide_stock") = "Y" Then
                        If Dr("boleh_lihat_stock") = "Y" Then
                            boleh_lihat = True
                        Else
                            boleh_lihat = False
                        End If
                    Else
                        boleh_lihat = True
                    End If
                Else
                    boleh_lihat = False
                End If
            End Using

            SQL = "Select kode_barang, nama, good_stock From barang Where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_stock_owner = '" & CmbSO_Asal.Text & "' and good_stock <> 0 and "
            SQL = SQL & "kode_barang = '" & Trim(TxtKd_Barang.Text) & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    'For i As Integer = 0 To ComboBox2.Items.Count - 1
                    '    If Dr("kode_stock_owner") = xSplit(0).Trim Then
                    '        ComboBox2.SelectedIndex = i
                    '        Exit For
                    '    End If
                    'Next
                    TxtKd_Barang.Text = Dr("kode_barang")
                    TxtNm_Barang.Text = Dr("Nama")
                    'iniiiii
                    If boleh_lihat = True Then
                        ' TxtStock.Text = Format(Dr("good_stock"), "N0")
                    Else
                        'TxtStock.Text = ""
                    End If
                    'iniiiii
                    'TextBox3.Text = Format(Dr("good_stock"), "N0")
                    TxtJumlah.Text = ""
                    TxtJumlah.Focus()
                Else
                    TxtKd_Barang.Text = "" : TxtNm_Barang.Text = ""  'TxtStock.Text = ""
                    TxtJumlah.Text = ""
                    TxtKd_Barang.Focus()
                End If
                ListView2.Visible = False
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtKd_Barang_TextChanged(sender As Object, e As EventArgs) Handles TxtKd_Barang.TextChanged
        If TxtKd_Barang.Text.Trim.Length = 0 Then
            ListView2.Visible = False : Exit Sub
        Else
            ListView2.Visible = True
        End If

        Try
            OpenConn()

            ListView2.Items.Clear()

            'iniiiii
            Dim boleh_lihat As Boolean

            SQL = "select flag_hide_stock, "
            SQL = SQL & "ISNULL(("
            SQL = SQL & "select top(1) 'Y' from role_button a where a.kode_perusahaan = x.kode_perusahaan and "
            SQL = SQL & "a.userid = '" & UserID & "' and buttonname = 'LIHAT_STOCK'"
            SQL = SQL & "), 'T') AS boleh_lihat_stock "
            SQL = SQL & " from stock_owner x where x.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "x.kode_stock_owner = '" & CmbSO_Asal.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("flag_hide_stock") = "Y" Then
                        If Dr("boleh_lihat_stock") = "Y" Then
                            boleh_lihat = True
                        Else
                            boleh_lihat = False
                        End If
                    Else
                        boleh_lihat = True
                    End If
                Else
                    boleh_lihat = False
                End If
            End Using

            SQL = "Select top(25) kode_stock_owner, kode_barang, nama, satuan, good_stock From barang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & CmbSO_Asal.Text & "' and "
            SQL = SQL & "good_stock <> 0 and nama like '%" & TxtKd_Barang.Text & "%' order by kode_stock_owner,kode_barang"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView2.Items.Add(dr("kode_stock_owner"))
                    Lvw.SubItems.Add(dr("kode_barang"))
                    Lvw.SubItems.Add(dr("Nama"))
                    Lvw.SubItems.Add(dr("satuan"))
                    'iniiiii
                    If boleh_lihat = True Then
                        Lvw.SubItems.Add(Format(dr("good_stock"), "N0"))
                    Else
                        Lvw.SubItems.Add("")
                    End If
                    'iniiiii
                    'Lvw.SubItems.Add(Format(dr("good_stock"), "N0"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles ListView2.DoubleClick
        If ListView2.Items.Count = 0 Then Exit Sub

        TxtKd_Barang.Text = ListView2.FocusedItem.SubItems(1).Text
        TxtKd_Barang.Focus()
        TxtJumlah.Focus()
        ListView2.Visible = False
    End Sub

    Private Sub ListView2_KeyDown(sender As Object, e As KeyEventArgs) Handles ListView2.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView2_DoubleClick(ListView2, e)
        End If
    End Sub

    Private Sub CmbSO_Asal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSO_Asal.SelectedIndexChanged
        If CmbSO_Asal.Text.Trim.Length = 0 Then Exit Sub
        Try
            OpenConn()

            get_no_faktur()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If CmbJnsTransfer.SelectedIndex = 0 Then
            CmbSo_Tujuan.SelectedIndex = CmbSO_Asal.SelectedIndex
        End If

    End Sub

    Private Sub TxtJumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtJumlah.KeyPress
        If e.KeyChar = Chr(13) Then

            If TxtKd_Barang.Text.Trim.Length = 0 Then
                MessageBox.Show("Kode barang harus diisi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TxtKd_Barang.Focus() : Exit Sub
            ElseIf TxtJumlah.Text.Trim.Length = 0 Or TxtJumlah.Text.Trim = "0" Then
                MessageBox.Show("Jumlah barang harus diisi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TxtJumlah.Focus() : Exit Sub
            ElseIf CmbSo_Tujuan.SelectedIndex = -1 Then
                MessageBox.Show("SO tujuan harus diisi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbSo_Tujuan.Focus() : Exit Sub
            ElseIf CmbSo_Tujuan.Text = CmbSO_Asal.Text Then
                MessageBox.Show("SO tujuan tidak boleh sama dengan SO awal . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbSo_Tujuan.Focus() : Exit Sub
            End If

            OpenConn()

            Using Dr = Open("Select * From barang Where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & CmbSO_Asal.Text & "' and kode_barang = '" & TxtKd_Barang.Text & "'")
                Dr.Read()
                If Dr("good_stock") < Val(TxtJumlah.Text) Then
                    MessageBox.Show("Proses " & Replace(Btn_Simpan.Text, "&", "") & " membuat stock negatif. Proses tidak dapat dilanjutkan..", Judul)
                    CloseConn()
                    Exit Sub
                End If
            End Using

            'iniiiii
            Dim boleh_lihat As Boolean

            SQL = "select flag_hide_stock, "
            SQL = SQL & "ISNULL(("
            SQL = SQL & "select top(1) 'Y' from role_button a where a.kode_perusahaan = x.kode_perusahaan and "
            SQL = SQL & "a.userid = '" & UserID & "' and buttonname = 'LIHAT_STOCK'"
            SQL = SQL & "), 'T') AS boleh_lihat_stock "
            SQL = SQL & " from stock_owner x where x.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "x.kode_stock_owner = '" & CmbSO_Asal.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("flag_hide_stock") = "Y" Then
                        If Dr("boleh_lihat_stock") = "Y" Then
                            boleh_lihat = True
                        Else
                            boleh_lihat = False
                        End If
                    Else
                        boleh_lihat = True
                    End If
                Else
                    boleh_lihat = False
                End If
            End Using


            CloseConn()

            Dim lv As New ListViewItem

            'lv = Lv_Barang_TS.Items.Add(CmbSO_Asal.Text)
            'lv.SubItems.Add(TxtKd_Barang.Text)
            'lv.SubItems.Add(TxtNm_Barang.Text)
            ''iniiiii
            'If boleh_lihat = True Then
            '    lv.SubItems.Add(TxtStock.Text)
            'Else
            '    lv.SubItems.Add("")
            'End If
            ''iniiiii
            ''lv.SubItems.Add(TextBox3.Text)
            'lv.SubItems.Add(Format(Val(TxtJumlah.Text), "N0"))

            'TxtKd_Barang.Text = "" : TxtNm_Barang.Text = ""
            'TxtStock.Text = "" : TxtJumlah.Text = ""
            'TxtKd_Barang.Focus()
            'CmbSO_Asal.Enabled = False
            'CmbSo_Tujuan.Enabled = False

        End If
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub
End Class