Public Class N_EMI_Transaksi_Pelunasan_Kredit_Per_DO



    Dim JT As String
    Dim ArrCari, ArrUrut, ArrNilai, ArrUrutDpt, ArrNilaiDpt, ArrDptPPH, ArrDptPPN, ArrDptNilaiPPH, ArrDptNilaiPPN, ArrDptFlagReimburse, ArrNilaiKlaimDpt As New ArrayList

    Dim LvFak As String
    Dim LvTgl As String
    Dim LvTglJthTmp As String
    Dim LvKdCus As String
    Dim LvNmCus As String
    Dim LvJml As String
    Dim LvNoProforma As String
    'Dim LvPertamaKrm As String

    Dim arrCrByr, ArrAkunCB1 As New ArrayList
    Dim persendiskoncash As Double
    Public cek_data As Boolean = False


    Private Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvFak = ListView1.Items(No_Index).Text
        LvTgl = ListView1.Items(No_Index).SubItems(1).Text
        LvTglJthTmp = ListView1.Items(No_Index).SubItems(2).Text
        LvKdCus = ListView1.Items(No_Index).SubItems(3).Text
        LvNmCus = ListView1.Items(No_Index).SubItems(4).Text
        LvJml = ListView1.Items(No_Index).SubItems(5).Text
        LvNoProforma = ListView1.Items(No_Index).SubItems(6).Text
    End Sub

    Private Sub cetak()
        Try

            OpenConn()

            SQL = "select kode_perusahaan from val_do where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "no_val = '" & TxtFaktur.Text.Trim & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Dim CrDoc As New N_EMI_CR_Faktur_Pelunasan_DO_Kredit     'Nama file CR
                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.PrintOptions.PrinterName = PrinterName
                    CrDoc.RecordSelectionFormula = "{val_do.Kode_Perusahaan} = '" & KodePerusahaan & "' and {val_do.no_val} = '" & TxtFaktur.Text.Trim & "'"

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterName
                    Dim rawKind As Integer
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    For i = doctoprint.PrinterSettings.PaperSizes.Count - 1 To 0 Step -1
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

    Private Sub Cari(ByVal param As String)
        ListView3.Items.Clear()
        If param = "Tidak" Then
            If ComboBox1.SelectedIndex = -1 Then
                MessageBox.Show("Paramater belum dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBox1.Focus() : Exit Sub
            ElseIf TextBox1.Text.Trim.Length = 0 Then
                MessageBox.Show("Value belum dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox1.Focus() : Exit Sub
            End If
        End If

        Try
            OpenConn()

            Dim lv As New ListViewItem

            ListView2.Items.Clear()
            SQL = "select c.lama_diskon_sementara, c.diskon_sementara, a.ngrand as grand, a.no_faktur, a.flag_lunas_do as flag_lunas, a.tanggal + a.jam as tgl, c.kode_customer, "
            SQL = SQL & "b.nama as namacustomer, b.alamat, "
            SQL = SQL & "(case when a.tgl_jth_tempo_baru is null then dateadd(d, a.Lama_JT, a.tanggal) else a.tgl_jth_tempo_baru end) as tgl_jatuh_tempo, "

            SQL = SQL & "isnull((select sum(y.byr) from val_do x, detail_val_do y where "
            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_val = y.no_val and "
            SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
            SQL = SQL & "y.no_faktur = a.no_faktur), 0) as pernah_val_do, "

            'SQL = SQL & "isnull((select sum(y.byr + y.disc_cash) from val_penj_tunai x, detail_val_penj_tunai y where "
            'SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_val = y.no_val and "
            'SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
            'SQL = SQL & "y.no_faktur = a.no_faktur), 0) as pernah_val_penj, "

            SQL = SQL & "sudah_dilunasi as pernah_val_penj, "

            SQL = SQL & "isnull((select sum(x.ngrand) from retur_do x where "
            SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
            SQL = SQL & "x.no_do = a.no_do), 0) as pernah_ret_do, "

            SQL = SQL & "isnull((select sum(y.nilai) from retur_mt x,Retur_MT_Det y  where "
            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and  x.No_Faktur = y.No_Faktur and "
            SQL = SQL & "y.Kode_Perusahaan = a.Kode_Perusahaan and y.No_DO = a.No_DO and x.Status is null "
            SQL = SQL & "), 0) as pernah_ret_MT, "

            'SQL = SQL & "isnull((select sum(x.grand) from retur_penjualan x where "
            'SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
            'SQL = SQL & "x.no_faktur_jual = a.no_faktur and x.no_do_dari_validasi = a.no_do), 0) as pernah_ret_penj, "

            SQL = SQL & "a.tanggal as pertama_kirim, a.no_do "

            SQL = SQL & "from do_new a, customers b, penjualan c where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and "
            SQL = SQL & "a.no_faktur = c.no_faktur and "
            SQL = SQL & "b.kode_customer = c.kode_customer and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.status is null and c.status is null and "

            If param = "Tidak" Then
                SQL = SQL & "" & ArrCari.Item(ComboBox1.SelectedIndex) & " like '" & TextBox1.Text.Trim & "%' and "
            End If

            SQL = SQL & "c.jenis_transaksi = 'N' and a.flag_lunas_do is null "

            SQL = SQL & "order by b.nama, tgl"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = ListView2.Items.Add(Dr("no_do"))
                    lv.SubItems.Add(Format(Dr("tgl"), "dd MMM yyyy"))
                    lv.SubItems.Add("-") 'lv.SubItems.Add(Format(Dr("tgl_jatuh_tempo"), "dd MMM yyyy"))
                    lv.SubItems.Add(Dr("kode_customer"))
                    lv.SubItems.Add(Dr("namacustomer"))
                    lv.SubItems.Add(Dr("alamat"))
                    Dim x_tot As Double = Dr("grand") - Dr("pernah_ret_do") - Dr("pernah_ret_mt")
                    lv.SubItems.Add(Format(x_tot, "N0"))
                    lv.SubItems.Add("0")
                    lv.SubItems.Add(Format(Dr("pernah_val_do") + Dr("pernah_val_penj"), "N0"))
                    lv.SubItems.Add(Format(Dr("grand") - Dr("pernah_ret_do") - Dr("pernah_ret_mt") - Dr("pernah_val_do") - Dr("pernah_val_penj"), "N0"))
                    lv.SubItems.Add(Format(Dr("pernah_ret_do") + Dr("pernah_ret_mt"), "N0"))
                    lv.SubItems.Add(Dr("diskon_sementara"))
                    If General_Class.CekNULL(Dr("pertama_kirim")) = "" Then
                        lv.SubItems.Add("")
                    Else
                        lv.SubItems.Add(Format(Dr("pertama_kirim"), "yyyy-MM-dd"))
                    End If
                    lv.SubItems.Add(Dr("grand"))
                    lv.SubItems.Add(Dr("lama_diskon_sementara"))
                    lv.SubItems.Add(Dr("no_faktur"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            'CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Hitung()
        Dim Grand As Double = 0


        For i As Integer = 0 To ListView1.Items.Count - 1
            Get_Isi_Listview(i)

            Grand = Grand + Val(HilangkanTanda(LvJml))

        Next

        TextBoxa.Text = (Format(Grand, "N0"))
    End Sub

    Private Sub Get_No_Faktur()
        TxtFaktur.Text = fValDOKredit & Format(DateTimePicker1.Value, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("val_do", "no_val", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_val, 1, " & Len(fValDOKredit) + 4 & ")", fValDOKredit & Format(DateTimePicker1.Value, "MMyy"))
    End Sub

    Private Sub Kosong_Bawah()
        TextBox9.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox7.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox12.Text = ""
        TextBox13.Text = ""
        TextBox14.Text = ""
        hide_combobox()
    End Sub

    Private Sub Kosong()
        get_jam()
        DateTimePicker1.Value = tgl_skg
        DateTimePicker1.Enabled = True
        persendiskoncash = 0

        TextBox1.Text = "" : TextBox2.Text = ""
        TextBoxa.Text = ""

        Button1.Text = "&Simpan"

        ListView1.Items.Clear()
        ListView2.Items.Clear()
        ListView3.Items.Clear()

        ComboBox1.Items.Clear() : ArrCari.Clear()
        ComboBox1.Items.Add("No DO") : ArrCari.Add("a.no_do")
        ComboBox1.Items.Add("No Faktur") : ArrCari.Add("a.no_faktur")
        ComboBox1.Items.Add("Kode Customer") : ArrCari.Add("c.kode_customer")
        ComboBox1.Items.Add("Nama Customer") : ArrCari.Add("b.nama")
        ComboBox1.SelectedIndex = 0

        Kosong_Bawah()

        Try

            OpenConn()

            Get_No_Faktur()
            get_unik()

            ComboBoxCb1.Items.Clear() : arrCrByr.Clear() : ArrAkunCB1.Clear()
            ComboBoxCb1.Items.Add("-- Cara Bayar --") : arrCrByr.Add("") : ArrAkunCB1.Add("")
            ComboBoxCb1.SelectedIndex = 0
            SQL = "select kode_cb, keterangan, kode_account_cb from cara_bayar where kode_perusahaan = '" & KodePerusahaan & "' order by keterangan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBoxCb1.Items.Add(Dr("keterangan")) : arrCrByr.Add(Dr("kode_cb")) : ArrAkunCB1.Add(Dr("kode_account_cb"))
                Loop
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Hitung()
        hide_combobox()
    End Sub

    Private Sub Validasi_Penj_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

    End Sub

    Private Sub hide_combobox()
        If ListView1.Items.Count = 0 Then
            ComboBoxCb1.Enabled = True
        Else
            ComboBoxCb1.Enabled = False
        End If
    End Sub

    Private Sub Validasi_Pembelian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        ListView1.Columns.Add("No DO", 100, HorizontalAlignment.Left)
        ListView1.Columns.Add("Tgl Transaksi", 80, HorizontalAlignment.Center) '1
        ListView1.Columns.Add("Tgl Jth Tmpo", 0, HorizontalAlignment.Center) '2
        ListView1.Columns.Add("Kode Customer", 100, HorizontalAlignment.Left) '3
        ListView1.Columns.Add("Nama Customer", 270, HorizontalAlignment.Left) '4
        ListView1.Columns.Add("Jumlah", 100, HorizontalAlignment.Right) '5  
        ListView1.Columns.Add("No Faktur", 100, HorizontalAlignment.Left).DisplayIndex = 1 '6
        ListView1.View = View.Details

        ListView2.Columns.Add("No DO", 100, HorizontalAlignment.Left) '0
        ListView2.Columns.Add("Tgl Transaksi", 80, HorizontalAlignment.Center) '1
        ListView2.Columns.Add("Tgl Jth Tmpo", 0, HorizontalAlignment.Center) '2
        ListView2.Columns.Add("Kode Customer", 100, HorizontalAlignment.Left) '3
        ListView2.Columns.Add("Nama Customer", 150, HorizontalAlignment.Left) '4
        ListView2.Columns.Add("Alamat", 130, HorizontalAlignment.Left) '5
        ListView2.Columns.Add("Total-Retur", 70, HorizontalAlignment.Right) '6
        ListView2.Columns.Add("#", 0, HorizontalAlignment.Right) '7
        ListView2.Columns.Add("Dibayar", 70, HorizontalAlignment.Right) '8
        ListView2.Columns.Add("Sisa", 70, HorizontalAlignment.Right) '9
        ListView2.Columns.Add("Retur", 70, HorizontalAlignment.Right) '10
        ListView2.Columns.Add("Est. Disc. Cash", 0, HorizontalAlignment.Right) '11
        ListView2.Columns.Add("Pertama Kirim", 0, HorizontalAlignment.Center) '12
        ListView2.Columns.Add("Grand", 0, HorizontalAlignment.Right) '13
        ListView2.Columns.Add("Lama Disc. Cash", 0, HorizontalAlignment.Center) '14
        ListView2.Columns.Add("No Faktur", 100, HorizontalAlignment.Left).DisplayIndex = 1 '15
        ListView2.View = View.Details

        '878
        ListView3.Columns.Add("Gudang", 150, HorizontalAlignment.Center)
        ListView3.Columns.Add("Kode Barang", 178, HorizontalAlignment.Left)
        ListView3.Columns.Add("Nama Barang", 300, HorizontalAlignment.Left)
        ListView3.Columns.Add("Jumlah", 80, HorizontalAlignment.Right)
        ListView3.Columns.Add("Retur", 80, HorizontalAlignment.Right)
        ListView3.Columns.Add("Satuan", 90, HorizontalAlignment.Left)
        ListView3.View = View.Details

        Kosong()
        TxtFaktur.Focus()
    End Sub

    Private Sub get_unik()
        get_jam()
        Dim rand As New Random
        Label11.Text = Format(tgl_skg, "MMddHHmmss") & Format(rand.Next(0, 100000), "00000") & Format(rand.Next(0, 10000000), "0000000")
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Kosong()
        DateTimePicker1.Focus()
    End Sub

    Private Sub Validasi_Pembelian_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Label1.Size = New Point(Me.Width, 33)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Cari("Semua")
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Cari("Tidak")
    End Sub

    Private Sub ListView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView2.DoubleClick
        For i As Integer = 0 To ListView1.Items.Count - 1
            If ListView1.Items(i).Text.Trim.ToUpper = ListView2.FocusedItem.Text.Trim.ToUpper Then
                MessageBox.Show("Faktur ini sudah dimasukkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Next

        DateTimePicker1.Enabled = False

        'ListView2.Columns.Add("No Faktur", 100, HorizontalAlignment.Left) '0
        'ListView2.Columns.Add("Tgl Transaksi", 80, HorizontalAlignment.Center) '1
        'ListView2.Columns.Add("Tgl Jth Tmpo", 0, HorizontalAlignment.Center) '2
        'ListView2.Columns.Add("Kode Customer", 250, HorizontalAlignment.Left) '3
        'ListView2.Columns.Add("Nama Customer", 150, HorizontalAlignment.Left) '4
        'ListView2.Columns.Add("Alamat", 130, HorizontalAlignment.Left) '5
        'ListView2.Columns.Add("Total-Retur-Disc Cash", 70, HorizontalAlignment.Right) '6
        'ListView2.Columns.Add("#", 0, HorizontalAlignment.Right) '7
        'ListView2.Columns.Add("Dibayar", 70, HorizontalAlignment.Right) '8
        'ListView2.Columns.Add("Sisa", 70, HorizontalAlignment.Right) '9
        'ListView2.Columns.Add("Retur", 70, HorizontalAlignment.Right) '10
        'ListView2.Columns.Add("Est. Disc. Cash", 100, HorizontalAlignment.Right) '11
        'ListView2.Columns.Add("Pertama Kirim", 100, HorizontalAlignment.Center) '12
        'ListView2.Columns.Add("Grand", 0, HorizontalAlignment.Right) '13
        'ListView2.Columns.Add("Lama Disc. Cash", 100, HorizontalAlignment.Center) '14
        'ListView2.View = View.Details

        TextBox9.Text = ListView2.FocusedItem.Text
        TextBox3.Text = ListView2.FocusedItem.SubItems(1).Text
        TextBox4.Text = ListView2.FocusedItem.SubItems(12).Text
        TextBox7.Text = ListView2.FocusedItem.SubItems(3).Text
        TextBox5.Text = ListView2.FocusedItem.SubItems(4).Text
        TextBox12.Text = ListView2.FocusedItem.SubItems(14).Text 'lama
        TextBox13.Text = ListView2.FocusedItem.SubItems(13).Text 'grand
        TextBox14.Text = ListView2.FocusedItem.SubItems(15).Text 'grand
        TextBox6.Text = HilangkanTanda(ListView2.FocusedItem.SubItems(9).Text)

        'ListView2.Columns.Add("No DO", 100, HorizontalAlignment.Left) '0
        'ListView2.Columns.Add("Tgl Transaksi", 80, HorizontalAlignment.Center) '1
        'ListView2.Columns.Add("Tgl Jth Tmpo", 0, HorizontalAlignment.Center) '2
        'ListView2.Columns.Add("Kode Customer", 100, HorizontalAlignment.Left) '3
        'ListView2.Columns.Add("Nama Customer", 150, HorizontalAlignment.Left) '4
        'ListView2.Columns.Add("Alamat", 130, HorizontalAlignment.Left) '5
        'ListView2.Columns.Add("Total-Retur-Disc Cash", 70, HorizontalAlignment.Right) '6
        'ListView2.Columns.Add("#", 0, HorizontalAlignment.Right) '7
        'ListView2.Columns.Add("Dibayar", 70, HorizontalAlignment.Right) '8
        'ListView2.Columns.Add("Sisa", 70, HorizontalAlignment.Right) '9
        'ListView2.Columns.Add("Retur", 70, HorizontalAlignment.Right) '10
        'ListView2.Columns.Add("Est. Disc. Cash", 100, HorizontalAlignment.Right) '11
        'ListView2.Columns.Add("Pertama Kirim", 100, HorizontalAlignment.Center) '12
        'ListView2.Columns.Add("Grand", 0, HorizontalAlignment.Right) '13
        'ListView2.Columns.Add("Lama Disc. Cash", 100, HorizontalAlignment.Center) '14
        'ListView2.Columns.Add("No Faktur", 100, HorizontalAlignment.Left).DisplayIndex = 1 '15
        'ListView2.View = View.Details

        TextBox6.Focus()
    End Sub

    Private Sub Cek_Check_in()
        SQL = "select * from Check_In_Pelunasan where Kode_Perusahaan = '" & KodePerusahaan & "' "
        SQL = SQL & "and Tgl_Keluar is null"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                Dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("ada transaksi yang belum selesai! Proses tidak dapat dilanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End Using
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TxtFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show("No pelunasan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtFaktur.Focus()
            Exit Sub
        ElseIf ListView1.Items.Count = 0 Then
            MessageBox.Show("Yang akan dilunasi harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus()
            Exit Sub
        ElseIf TextBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox2.Focus()
            Exit Sub
        ElseIf ComboBoxCb1.SelectedIndex = -1 Or ComboBoxCb1.SelectedIndex = 0 Then
            MessageBox.Show("Cara bayar harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBoxCb1.Focus()
            Exit Sub
        End If


        Dim RRRRRR As Integer = 0
        Dim SSSSSS As Integer = 0
        If Button1.Text = "&Simpan" Then
            Dim tny As String = MessageBox.Show("Yakin akan disimpan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If tny = vbNo Then Exit Sub

            GetTime()
            get_jam()

            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction
                If SSSSSS = 1 Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("ada transaksi yang belum selesai! Proses tidak dapat dilanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                Else
                    SSSSSS = 1
                End If

                Cek_Check_in()

                SQL = "INSERT INTO Check_In_Pelunasan(Kode_Perusahaan,Kode_Unik,Tgl_Masuk,Jam_Masuk,"
                SQL = SQL & "UserId_Masuk) VALUES('" & KodePerusahaan & "','" & Label11.Text & "',"
                SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "',"
                SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "',"
                SQL = SQL & "'" & UserID & "')"
                ExecuteTrans(SQL)

                'SQL = "select shift from close_shift where kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "jam_in is not NULL and jam_out is NULL"
                'Using Dr = OpenTrans(SQL)
                '    If Not (Dr.Read) Then
                '        Dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("Tidak ada shift yang di open! Proses tidak dapat dilanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using

                RRRRRR = 1

                Get_No_Faktur()
                Get_Data_Acc()



                Dim SisaHutang As Double = 0
                Dim Jumlah_Hutang As Double = 0
                Dim JT As String = ""
                Dim KodeCust As String = ""

                Dim Kode_Voucher As String = GetLastNumberJurnal(Format(DateTimePicker1.Value, "yyyyMM"), fJU & fValPenj, KodePerusahaan)
                SQL = "select * from val_do where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "status is null and kode_voucher = '" & Kode_Voucher & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Kode Voucher sudah digunakan !!, Proses tidak dapat dilanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                RRRRRR = 2
                SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                SQL = SQL & "'" & Kode_Voucher & "', "
                SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                SQL = SQL & "'" & KodeProyek & "', 'Pelunasan piutang " & TxtFaktur.Text.Trim & "', '', "
                SQL = SQL & "'-', '" & UserID & "')"
                ExecuteTrans(SQL)

                'CODING POTONG DEPOSIT, TAMBAHAN VOUCHER BARU UNTUK DEPOSIT
                Dim ada_nilai_di_deposit As String = ""
                SQL = "select isnull(sum(a.Nilai_yang_diinput), 0) as total, isnull(sum(a.Nilai_yang_diKlaim), 0) as total_klaim, "
                SQL = SQL & "isnull(sum(a.NilaiPPN), 0) as total_PPN, isnull(sum(a.NilaiPPH), 0) as total_PPH from "
                SQL = SQL & "Perlunasan_Per_Step_sementara_deposit a where "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_unik = '" & Label11.Text & "' and No_do='" & LvFak.Trim & "' and asal ='PROMO' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Dr("total") > 0 Then
                            ada_nilai_di_deposit = "Y"
                        End If

                    End If
                End Using

                Dim Kode_Voucher2 As String = ""
                Dim Kode_voucher2_ As String = "NULL"


                If ada_nilai_di_deposit = "Y" Then
                    Kode_Voucher2 = GetLastNumberJurnal(Format(DateTimePicker1.Value, "yyyyMM"), fJU & fValPenj, KodePerusahaan)
                    Kode_voucher2_ = "'" & Kode_Voucher2 & "'"
                    SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                    SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                    SQL = SQL & "'" & Kode_Voucher2 & "', "
                    SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                    SQL = SQL & "'" & KodeProyek & "', 'Pelunasan piutang " & TxtFaktur.Text.Trim & "', '', "
                    SQL = SQL & "'-', '" & UserID & "')"
                    ExecuteTrans(SQL)

                End If

                RRRRRR = 3

                SQL = "INSERT INTO Cek_Pelunasan_Per_Step"
                SQL = SQL & "(Kode_Perusahaan, No_Val, No_Faktur, Tanggal, Jam,Jenis, Urut_Form)"
                SQL = SQL & "VALUES('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & "-" & "', "
                SQL = SQL & "'" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
                SQL = SQL & "'" & "KREDIT" & "',1)"
                ExecuteTrans(SQL)

                Dim pagenumber As Integer = 1
                Dim pagenumber2 As Integer = 1
                Dim jumlah_Um As Double = 0


                SQL = "select isnull(sum(a.Nilai_yang_diinput), 0) as total from "
                SQL = SQL & "Perlunasan_Per_Step_sementara a where "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_unik = '" & Label11.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        jumlah_Um = Dr("total")
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("UM tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using



                RRRRRR = 4
                If jumlah_Um <> 0 Then

                    SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(X_Pelunasan_Dimuka, 1),
                                       Strings.Mid(X_Pelunasan_Dimuka, 2, 1),
                                       Strings.Mid(Ganti(X_Pelunasan_Dimuka), 3),
                                       KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, jumlah_Um, "0", pagenumber, Ket_Lokasi_HO, Bahasa_Pilihan, Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1
                End If



                RRRRRR = 5
                SQL = "INSERT INTO Cek_Pelunasan_Per_Step"
                SQL = SQL & "(Kode_Perusahaan, No_Val, No_Faktur, Tanggal, Jam,Jenis, Urut_Form)"
                SQL = SQL & "VALUES('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & "-" & "', "
                SQL = SQL & "'" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
                SQL = SQL & "'" & "KREDIT" & "',2)"
                ExecuteTrans(SQL)

                Dim Deposit As Double = 0

                For i As Integer = 0 To ListView1.Items.Count - 1
                    Get_Isi_Listview(i)

                    Dim coa_disc_cash As String = ""
                    Dim coa_piutang As String = ""
                    Dim coa_ppn_penj As String = ""

                    Dim nilai_dpp_diskon_cash_per_faktur As Double = 0
                    Dim nilai_ppn_diskon_cash_per_faktur As Double = 0
                    Dim jns_penj As String = ""
                    Dim lokasi_penj As String = ""

                    'awal coding stenly
                    SQL = "select tanggal from do_new where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_do = '" & LvFak.Trim & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If Format(Dr("tanggal"), "yyyy-MM-dd") > Format(DateTimePicker1.Value, "yyyy-MM-dd") Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Tanggal Pelunasan " & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "tidak boleh lebih kecil dari tanggal do ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End If
                    End Using
                    'akhir coding stenly

                    'SQL = "select a.flag_cabang_sendiri, a.val_diskon_cash, a.jenis, b.coa_diskon_cash, b.ppn_penjualan, a.diskon_sementara, a.akun_kas, a.lokasi, a.grand, a.status, a.jenis_transaksi, a.kode_customer, "
                    'SQL = SQL & "isnull((select sum(x.grand) as ttl_retur from retur_penjualan x where x.kode_perusahaan = a.kode_perusahaan and x.no_faktur_jual = a.no_faktur and x.status is null), 0) as ttl_retur, "
                    'SQL = SQL & "isnull((select sum(y.byr + y.disc_cash) from val_do x, detail_val_do y where "
                    'SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_val = y.no_val and "
                    'SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
                    'SQL = SQL & "y.no_faktur = a.no_faktur), 0) as ttl_validasi, "

                    'SQL = SQL & "(select top(1) x.tanggal from do_new x where x.kode_perusahaan = a.kode_perusahaan and "
                    'SQL = SQL & "x.no_faktur = a.No_Faktur and x.status is null order by tanggal asc) as pertama_kirim "

                    'SQL = SQL & "from penjualan a, stock_owner b where a.kode_perusahaan = b.kode_perusahaan and a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "a.lokasi = b.kode_stock_owner and a.no_faktur = '" & LvFak.Trim & "' "

                    RRRRRR = 6


                    SQL = "select c.lokasi, c.flag_cabang_sendiri, a.status, c.jenis_transaksi, c.jenis, c.lama_diskon_sementara, c.diskon_sementara, a.ngrand as grand, a.no_faktur, a.flag_lunas_do as flag_lunas, a.tanggal + a.jam as tgl, c.kode_customer, "
                    SQL = SQL & "b.nama as namacustomer, b.alamat, "
                    SQL = SQL & "(case when a.tgl_jth_tempo_baru is null then dateadd(d, a.Lama_JT, a.tanggal) else a.tgl_jth_tempo_baru end) as tgl_jatuh_tempo, "
                    SQL = SQL & "isnull((select sum(y.byr) from val_do x, detail_val_do y where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_val = y.no_val and "
                    SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
                    SQL = SQL & "y.no_faktur = a.no_faktur), 0) as pernah_val_do, "

                    'SQL = SQL & "isnull((select sum(y.byr + y.disc_cash) from val_penj_tunai x, detail_val_penj_tunai y where "
                    'SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_val = y.no_val and "
                    'SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
                    'SQL = SQL & "y.no_faktur = a.no_faktur), 0) as pernah_val_penj, "

                    SQL = SQL & "sudah_dilunasi as pernah_val_penj, "
                    SQL = SQL & "isnull((select sum(x.ngrand) from retur_do x where "
                    SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
                    SQL = SQL & "x.no_do = a.no_do), 0) as pernah_ret_do, "

                    SQL = SQL & "isnull((select sum(y.nilai) from retur_mt x,Retur_MT_Det y  where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and  x.No_Faktur = y.No_Faktur and "
                    SQL = SQL & "y.Kode_Perusahaan = a.Kode_Perusahaan and y.No_DO = a.No_DO and x.Status is null "
                    SQL = SQL & "), 0) as pernah_ret_MT, "

                    'SQL = SQL & "isnull((select sum(x.grand) from retur_penjualan x where "
                    'SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
                    'SQL = SQL & "x.no_faktur_jual = a.no_faktur and x.no_do_dari_validasi = a.no_do), 0) as pernah_ret_penj, "

                    SQL = SQL & "a.tanggal as pertama_kirim, a.no_do, "
                    SQL = SQL & "d.coa_diskon_cash, d.ppn_penjualan, c.coa_piutang "
                    SQL = SQL & "from do_new a, customers b, penjualan c, stock_owner d where "
                    SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and "
                    SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan and "
                    SQL = SQL & "a.no_faktur = c.no_faktur and "
                    SQL = SQL & "b.kode_customer = c.kode_customer and "
                    SQL = SQL & "c.lokasi = d.kode_stock_owner and "
                    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "a.status is null and c.status is null and "
                    SQL = SQL & "a.no_do = '" & LvFak.Trim & "' and "
                    SQL = SQL & "c.jenis_transaksi = 'N' and a.flag_lunas_do is null "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            jns_penj = Dr("jenis")
                            lokasi_penj = Dr("lokasi")
                            'Dim x_tot As Double = Dr("grand") - Dr("pernah_ret_do")
                            'lv.SubItems.Add(Format(x_tot, "N0"))
                            'lv.SubItems.Add("0")
                            'lv.SubItems.Add(Format(Dr("pernah_val_do") + Dr("pernah_val_penj"), "N0"))
                            'lv.SubItems.Add(Format(Dr("grand") - Dr("pernah_ret_do") - Dr("pernah_val_do") - Dr("pernah_val_penj"), "N0"))
                            'lv.SubItems.Add(Format(Dr("pernah_ret_do"), "N0"))
                            RRRRRR = 7
                            SisaHutang = Dr("grand") - Dr("pernah_ret_do") - Dr("pernah_ret_mt") - Dr("pernah_val_do") - Dr("pernah_val_penj")
                            Jumlah_Hutang = Dr("grand") - Dr("pernah_ret_do") - Dr("pernah_ret_mt")
                            JT = Dr("jenis_transaksi")
                            KodeCust = Dr("kode_customer")

                            coa_piutang = General_Class.CekNULL(Dr("coa_piutang"))
                            coa_disc_cash = Dr("coa_diskon_cash")
                            coa_ppn_penj = Dr("ppn_penjualan")

                            If JT = "T" Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Transaksi ini termasuk transaksi tunai. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf General_Class.CekNULL(Dr("status")) = "Y" Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Transaksi ini sudah di batalkan. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                                'ElseIf General_Class.CekNULL(Dr("val_diskon_cash")) = "Y" Then
                                '    Dr.Close()
                                '    CloseTrans()
                                '    CloseConn()
                                '    MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini sudah di validasi cash sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                '    Exit Sub
                            ElseIf General_Class.CekNULL(Dr("coa_piutang")) = "" Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Akun kas tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf SisaHutang < Val(HilangkanTanda(LvJml)) Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pembayaran tidak boleh lebih dari sisa piutang. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf KodeCust <> LvKdCus Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Customer sudah diubah sebelumnya. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf Dr("flag_cabang_sendiri") = "Y" Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Customer bukan reseller. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf Dr("flag_cabang_sendiri") = "T" Then
                                If Format(DateTimePicker1.Value, "yyyyMM") <> Format(tgl_skg, "yyyyMM") Then
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Pelunasan tidak boleh dibulan mundur!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    DateTimePicker1.Focus()
                                    Exit Sub
                                End If
                            End If
                            RRRRRR = 8
                            Dr.Close()


                            SQL = "update do_new set sudah_dilunasi = sudah_dilunasi + " & (Val(HilangkanTanda(LvJml))) & " where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "no_do = '" & LvFak.Trim & "'"
                            ExecuteTrans(SQL)
                            RRRRRR = 9
                            SQL = "INSERT INTO Cek_Pelunasan_Per_Step"
                            SQL = SQL & "(Kode_Perusahaan, No_Val, No_Faktur, Tanggal, Jam,Jenis, Urut_Form)"
                            SQL = SQL & "VALUES('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & LvFak.Trim & "', "
                            SQL = SQL & "'" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
                            SQL = SQL & "'" & "KREDIT" & "',3)"
                            ExecuteTrans(SQL)
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Nomor DO tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using
                    RRRRRR = 10

                    'CODING POTONG DEPOSIT, AMBIL DATA YG TELAH DIINPUT DI SEMENTARA
                    '_________________________________________________________________________________________________________________
                    'INI UNTUK DEPOSIT REBATE
                    Dim coa_BudgetPromo As String = ""
                    Dim coa_HutangPromo As String = ""
                    Dim coa_Rebate As String = ""
                    Dim coa_B2B As String = ""
                    Dim akunPPH As String = ""
                    Dim akunPiutangPPH As String = ""
                    Dim akunPPN As String = ""
                    Dim inisial_faktur As String = ""


                    Dim jenis_PPH As String = ""
                    SQL = "select Jenis_PPH from customers where "
                    SQL = SQL & "kode_Perusahaan='" & KodePerusahaan & "' and Kode_Customer='" & KodeCust & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then

                            jenis_PPH = General_Class.CekNULL(dr("Jenis_PPH"))

                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Customer Tidak ditemukan . . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using


                    SQL = "select top(1) inisial_faktur, Akun_Rebate, Akun_B2B, Hutang_Promo_Rebate, Budget_Promo_Rebate,ppn_pembelian,Akun_PPH23, Akun_PPH21,Akun_Piutang_PPH from stock_owner where Kode_Stock_Owner='" & lokasi_penj & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            coa_BudgetPromo = Dr("Budget_Promo_Rebate")
                            coa_HutangPromo = Dr("Hutang_Promo_Rebate")
                            inisial_faktur = Dr("inisial_faktur")
                            coa_Rebate = Dr("Akun_Rebate")
                            coa_B2B = Dr("Akun_B2B")
                            akunPPN = Dr("ppn_pembelian")
                            akunPiutangPPH = Dr("Akun_Piutang_PPH")


                            If jenis_PPH = "21" Then
                                akunPPH = Dr("Akun_PPH21")
                            ElseIf jenis_PPH = "23" Then
                                akunPPH = Dr("Akun_PPH23")
                            Else
                                akunPPH = ""
                            End If

                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Lokasi Tidak di Temukan . .  !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If
                    End Using


                    Dim deposit_inputRBT As Double = 0
                    Dim deposit_inputB2B As Double = 0
                    Dim deposit_PPN As Double = 0
                    Dim deposit_PPH As Double = 0

                    Dim deposit_FlagReimbursePPH As String = ""
                    SQL = "select top(1) Flag_reimburse "
                    SQL = SQL & "from "
                    SQL = SQL & "Perlunasan_Per_Step_sementara_deposit a where "
                    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_unik = '" & Label11.Text & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            deposit_FlagReimbursePPH = General_Class.CekNULL(Dr("Flag_reimburse"))
                        End If
                    End Using


                    SQL = "select isnull(sum(a.Nilai_yang_diinput), 0) as total, isnull(sum(a.Nilai_yang_diKlaim), 0) as total_klaim, "
                    SQL = SQL & "isnull(sum(a.NilaiPPN), 0) as total_PPN, isnull(sum(a.NilaiPPH), 0) as total_PPH from "
                    SQL = SQL & "Perlunasan_Per_Step_sementara_deposit a where "
                    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_unik = '" & Label11.Text & "' and No_do='" & LvFak.Trim & "' and asal ='PROMO' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Deposit = Deposit + Dr("total_klaim")
                            deposit_PPN = Dr("total_PPN")
                            deposit_PPH = Dr("total_PPH")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("UM tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select isnull(sum(a.Nilai_yang_diinput), 0) as total, isnull(sum(a.Nilai_yang_diKlaim), 0) as total_klaim, "
                    SQL = SQL & "isnull(sum(a.NilaiPPN), 0) as total_PPN, isnull(sum(a.NilaiPPH), 0) as total_PPH from "
                    SQL = SQL & "Perlunasan_Per_Step_sementara_deposit a where "
                    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_unik = '" & Label11.Text & "' and Flag_B2B is null and No_do='" & LvFak.Trim & "'  and asal ='PROMO' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            deposit_inputRBT = Dr("total")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("UM tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select isnull(sum(a.Nilai_yang_diinput), 0) as total, isnull(sum(a.Nilai_yang_diKlaim), 0) as total_klaim, "
                    SQL = SQL & "isnull(sum(a.NilaiPPN), 0) as total_PPN, isnull(sum(a.NilaiPPH), 0) as total_PPH from "
                    SQL = SQL & "Perlunasan_Per_Step_sementara_deposit a where "
                    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_unik = '" & Label11.Text & "'  and Flag_B2B='Y' and No_do='" & LvFak.Trim & "'  and asal ='PROMO' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            deposit_inputB2B = Dr("total")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("UM tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    If deposit_inputRBT <> 0 Then
                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Rebate & "'  and lokasi_detail = '" & lokasi_penj & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update 

                                SQL = "update detail_jurnal set debit = debit + " & deposit_inputRBT & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Rebate & "'"
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert
                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_Rebate, 1),
                                        Strings.Mid(coa_Rebate, 2, 1),
                                        Strings.Mid(Ganti(coa_Rebate), 3),
                                        KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, deposit_inputRBT, "0", pagenumber, lokasi_penj, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                                pagenumber = pagenumber + 1
                            End If
                        End Using

                    End If

                    If deposit_inputB2B <> 0 Then
                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_B2B & "'  and lokasi_detail = '" & lokasi_penj & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update 

                                SQL = "update detail_jurnal set debit = debit + " & deposit_inputB2B & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_B2B & "'"
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert
                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_B2B, 1),
                                        Strings.Mid(coa_B2B, 2, 1),
                                        Strings.Mid(Ganti(coa_B2B), 3),
                                        KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, deposit_inputB2B, "0", pagenumber, lokasi_penj, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                                pagenumber = pagenumber + 1
                            End If
                        End Using

                    End If

                    If deposit_PPN <> 0 Then
                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPPN & "'  and lokasi_detail = '" & lokasi_penj & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update 

                                SQL = "update detail_jurnal set debit = debit + " & deposit_PPN & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPPN & "'"
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert
                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(akunPPN, 1),
                                        Strings.Mid(akunPPN, 2, 1),
                                        Strings.Mid(Ganti(akunPPN), 3),
                                        KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, deposit_PPN, "0", pagenumber, lokasi_penj, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                                pagenumber = pagenumber + 1
                            End If
                        End Using
                    End If

                    '_________________________________________________________________________________________________________________
                    'INI UNTUK DEPOSIT SUBSIDI ONGKIR

                    Dim coa_SubsidiEkspedisi As String = ""
                    Dim coa_SubsidiDiskon As String = ""
                    Dim akunPPHSubsidi As String = ""
                    Dim akunPiutangPPHSubsidi As String = ""
                    Dim akunPPNSubsidi As String = ""



                    SQL = "select top(1) Akun_Subsidi_Diskon, Akun_Subsidi_Ekspedisi, ppn_penjualan, Akun_PPH23, Akun_PPH21 from stock_owner where Kode_Stock_Owner='" & lokasi_penj & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            coa_SubsidiEkspedisi = Dr("Akun_Subsidi_Ekspedisi")
                            coa_SubsidiDiskon = Dr("Akun_Subsidi_Diskon")
                            akunPPNSubsidi = Dr("ppn_penjualan")
                            akunPiutangPPHSubsidi = Dr("Akun_Subsidi_Ekspedisi")

                            If jenis_PPH = "21" Then
                                akunPPHSubsidi = Dr("Akun_PPH21")
                            ElseIf jenis_PPH = "23" Then
                                akunPPHSubsidi = Dr("Akun_PPH23")
                            Else
                                akunPPHSubsidi = ""
                            End If
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Lokasi Tidak di Temukan . .  !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If
                    End Using


                    Dim deposit_inputSubsidiEkspedisi As Double = 0
                    Dim deposit_inputSubsidiDiskon As Double = 0
                    Dim deposit_PPNSubsidi As Double = 0
                    Dim deposit_PPHSubsidi As Double = 0

                    Dim deposit_FlagReimbursePPHSubsidi As String = ""


                    SQL = "select isnull(sum(a.Nilai_yang_diinput), 0) as total, isnull(sum(a.Nilai_yang_diKlaim), 0) as total_klaim, "
                    SQL = SQL & "isnull(sum(a.NilaiPPN), 0) as total_PPN, isnull(sum(a.NilaiPPH), 0) as total_PPH from "
                    SQL = SQL & "Perlunasan_Per_Step_sementara_deposit a where "
                    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_unik = '" & Label11.Text & "' and No_do='" & LvFak.Trim & "' and asal ='SUBSIDI' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Deposit = Deposit + Dr("total_klaim")
                            deposit_PPNSubsidi = Dr("total_PPN")
                            deposit_PPHSubsidi = Dr("total_PPH")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("UM tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select isnull(sum(a.Nilai_yang_diinput), 0) as total, isnull(sum(a.Nilai_yang_diKlaim), 0) as total_klaim, "
                    SQL = SQL & "isnull(sum(a.NilaiPPN), 0) as total_PPN, isnull(sum(a.NilaiPPH), 0) as total_PPH from "
                    SQL = SQL & "Perlunasan_Per_Step_sementara_deposit a where "
                    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_unik = '" & Label11.Text & "' and No_do='" & LvFak.Trim & "' and asal ='SUBSIDI' and Jenis_Klaim='A' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            deposit_inputSubsidiEkspedisi = Dr("total")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("UM tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select isnull(sum(a.Nilai_yang_diinput), 0) as total, isnull(sum(a.Nilai_yang_diKlaim), 0) as total_klaim, "
                    SQL = SQL & "isnull(sum(a.NilaiPPN), 0) as total_PPN, isnull(sum(a.NilaiPPH), 0) as total_PPH from "
                    SQL = SQL & "Perlunasan_Per_Step_sementara_deposit a where "
                    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_unik = '" & Label11.Text & "' and No_do='" & LvFak.Trim & "' and asal ='SUBSIDI' and Jenis_Klaim='B' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            deposit_inputSubsidiDiskon = Dr("total")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("UM tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    If deposit_inputSubsidiEkspedisi <> 0 Then
                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_SubsidiEkspedisi & "'  and lokasi_detail = '" & lokasi_penj & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update 

                                SQL = "update detail_jurnal set debit = debit + " & deposit_inputSubsidiEkspedisi & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_SubsidiEkspedisi & "'"
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert
                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_SubsidiEkspedisi, 1),
                                        Strings.Mid(coa_SubsidiEkspedisi, 2, 1),
                                        Strings.Mid(Ganti(coa_SubsidiEkspedisi), 3),
                                        KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, deposit_inputSubsidiEkspedisi, "0", pagenumber, lokasi_penj, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                                pagenumber = pagenumber + 1
                            End If
                        End Using

                    End If

                    If deposit_inputSubsidiDiskon <> 0 Then
                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_SubsidiDiskon & "'  and lokasi_detail = '" & lokasi_penj & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update 

                                SQL = "update detail_jurnal set debit = debit + " & deposit_inputSubsidiDiskon & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_SubsidiDiskon & "'"
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert
                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_SubsidiDiskon, 1),
                                        Strings.Mid(coa_SubsidiDiskon, 2, 1),
                                        Strings.Mid(Ganti(coa_SubsidiDiskon), 3),
                                        KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, deposit_inputSubsidiDiskon, "0", pagenumber, lokasi_penj, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                                pagenumber = pagenumber + 1
                            End If
                        End Using

                    End If

                    If deposit_PPNSubsidi <> 0 Then
                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPPNSubsidi & "'  and lokasi_detail = '" & lokasi_penj & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update 

                                SQL = "update detail_jurnal set debit = debit + " & deposit_PPNSubsidi & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPPNSubsidi & "'"
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert
                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(akunPPNSubsidi, 1),
                                        Strings.Mid(akunPPNSubsidi, 2, 1),
                                        Strings.Mid(Ganti(akunPPNSubsidi), 3),
                                        KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, deposit_PPNSubsidi, "0", pagenumber, lokasi_penj, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                                pagenumber = pagenumber + 1
                            End If
                        End Using
                    End If


                    'SQL = "select um from customers where kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "kode_customer = '" & LvKdCus.Trim & "'"
                    'Using Dr = OpenTrans(SQL)
                    '    If Dr.Read Then

                    '        If Dr("um") - HilangkanTanda(LvJml) < 0 Then
                    '            Dr.Close()
                    '            CloseTrans()
                    '            CloseConn()
                    '            MessageBox.Show("Proses membuat uang masuk negatif. " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '            Exit Sub
                    '        Else
                    '            Dr.Close()
                    '            RRRRRR = 11
                    '            SQL = "update customers set um = um - " & HilangkanTanda(LvJml) & " where "
                    '            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    '            SQL = SQL & "kode_customer = '" & LvKdCus.Trim & "'"
                    '            ExecuteTrans(SQL)
                    '            RRRRRR = 12
                    '            SQL = "INSERT INTO Cek_Pelunasan_Per_Step"
                    '            SQL = SQL & "(Kode_Perusahaan, No_Val, No_Faktur, Tanggal, Jam,Jenis, Urut_Form)"
                    '            SQL = SQL & "VALUES('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & LvFak.Trim & "', "
                    '            SQL = SQL & "'" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
                    '            SQL = SQL & "'" & "KREDIT" & "',4)"
                    '            ExecuteTrans(SQL)
                    '        End If
                    '    Else
                    '        Dr.Close()
                    '        CloseTrans()
                    '        CloseConn()
                    '        MessageBox.Show("Customer tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        Exit Sub
                    '    End If
                    'End Using

                    'SQL = "select piutang from customers where kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "kode_customer = '" & KodeCust & "'"
                    'Using Dr = OpenTrans(SQL)
                    '    If Dr.Read Then
                    '        If Dr("piutang") - Val(HilangkanTanda(LvJml)) < 0 Then
                    '            Dr.Close()
                    '            CloseTrans()
                    '            CloseConn()
                    '            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat piutang " & LvNmCus & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '            Exit Sub
                    '        Else
                    '            Dr.Close()
                    '            'kurangin piutangnya
                    '            SQL = "update customers set piutang = piutang - " & HilangkanTanda(LvJml) & " where "
                    '            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    '            SQL = SQL & "kode_customer = '" & KodeCust & "'"
                    '            ExecuteTrans(SQL)
                    '        End If
                    '    Else
                    '        Dr.Close()
                    '        CloseTrans()
                    '        CloseConn()
                    '        MessageBox.Show("Customer tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        Exit Sub
                    '    End If
                    'End Using
                    RRRRRR = 13
                    If nilai_dpp_diskon_cash_per_faktur <> 0 Then
                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_disc_cash & "' and lokasi_detail = '" & lokasi_penj & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update 
                                RRRRRR = 14
                                SQL = "update detail_jurnal set debit = debit + " & nilai_dpp_diskon_cash_per_faktur & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_disc_cash & "' and lokasi_detail = '" & lokasi_penj & "'"
                                ExecuteTrans(SQL)
                            Else
                                RRRRRR = 15
                                Dr.Close()
                                'insert
                                pagenumber = pagenumber + 1

                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_disc_cash, 1),
                                       Strings.Mid(coa_disc_cash, 2, 1),
                                       Strings.Mid(Ganti(coa_disc_cash), 3),
                                       KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, HilangkanTanda(nilai_dpp_diskon_cash_per_faktur), "0", pagenumber + 1, lokasi_penj, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                            End If
                        End Using

                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_ppn_penj & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update 
                                RRRRRR = 16
                                SQL = "update detail_jurnal set debit = debit + " & nilai_ppn_diskon_cash_per_faktur & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_ppn_penj & "'"
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert
                                pagenumber = pagenumber + 1
                                RRRRRR = 17
                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_ppn_penj, 1),
                                       Strings.Mid(coa_ppn_penj, 2, 1),
                                       Strings.Mid(Ganti(coa_ppn_penj), 3),
                                       KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, HilangkanTanda(nilai_ppn_diskon_cash_per_faktur), "0", pagenumber + 1, Ket_Lokasi_HO, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                            End If
                        End Using


                    End If
                    RRRRRR = 18

                    'CODING PTOMNG DEPOSIT, TAMBAHAN NILAI PPH
                    '______________________________________________________________________________________
                    'INI UNTUK DEPOSIT REBATE
                    If deposit_PPH <> 0 Then

                        If jenis_PPH = "" Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Jenis PPH Belum ditentukan . . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                        If deposit_FlagReimbursePPH = "Y" Then
                            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPiutangPPH & "'  and lokasi_detail = '" & lokasi_penj & "'"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Dr.Close()
                                    'update 

                                    SQL = "update detail_jurnal set debit = debit + " & deposit_PPH & " where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPiutangPPH & "'"
                                    ExecuteTrans(SQL)
                                Else
                                    Dr.Close()
                                    'insert
                                    SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(akunPiutangPPH, 1),
                                            Strings.Mid(akunPiutangPPH, 2, 1),
                                            Strings.Mid(Ganti(akunPiutangPPH), 3),
                                            KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, deposit_PPH, "0", pagenumber, lokasi_penj, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                    ExecuteTrans(SQL)
                                    pagenumber = pagenumber + 1
                                End If
                            End Using

                            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPPH & "'  and lokasi_detail = '" & lokasi_penj & "'"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Dr.Close()
                                    'update 

                                    SQL = "update detail_jurnal set Kredit = Kredit + " & deposit_PPH & " where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPPH & "'"
                                    ExecuteTrans(SQL)
                                Else
                                    Dr.Close()
                                    'insert
                                    SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(akunPPH, 1),
                                            Strings.Mid(akunPPH, 2, 1),
                                            Strings.Mid(Ganti(akunPPH), 3),
                                            KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, "0", deposit_PPH, pagenumber, lokasi_penj, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                    ExecuteTrans(SQL)
                                    pagenumber = pagenumber + 1
                                End If
                            End Using
                        Else
                            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPPH & "'  and lokasi_detail = '" & lokasi_penj & "'"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Dr.Close()
                                    'update 

                                    SQL = "update detail_jurnal set Kredit = Kredit + " & deposit_PPH & " where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPPH & "'"
                                    ExecuteTrans(SQL)
                                Else
                                    Dr.Close()
                                    'insert
                                    SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(akunPPH, 1),
                                            Strings.Mid(akunPPH, 2, 1),
                                            Strings.Mid(Ganti(akunPPH), 3),
                                            KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, "0", deposit_PPH, pagenumber, lokasi_penj, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                    ExecuteTrans(SQL)
                                    pagenumber = pagenumber + 1
                                End If
                            End Using
                        End If
                    End If

                    '______________________________________________________________________________________
                    'INI UNTUK SUBSIDI ONGKIR
                    If deposit_PPHSubsidi <> 0 Then

                        If jenis_PPH = "" Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Jenis PPH Belum ditentukan . . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                        If deposit_FlagReimbursePPH = "Y" Then
                            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPiutangPPHSubsidi & "'  and lokasi_detail = '" & lokasi_penj & "'"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Dr.Close()
                                    'update 

                                    SQL = "update detail_jurnal set debit = debit + " & deposit_PPHSubsidi & " where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPiutangPPHSubsidi & "'"
                                    ExecuteTrans(SQL)
                                Else
                                    Dr.Close()
                                    'insert
                                    SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(akunPiutangPPHSubsidi, 1),
                                            Strings.Mid(akunPiutangPPHSubsidi, 2, 1),
                                            Strings.Mid(Ganti(akunPiutangPPHSubsidi), 3),
                                            KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, deposit_PPHSubsidi, "0", pagenumber, lokasi_penj, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                    ExecuteTrans(SQL)
                                    pagenumber = pagenumber + 1
                                End If
                            End Using

                            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPPHSubsidi & "'  and lokasi_detail = '" & lokasi_penj & "'"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Dr.Close()
                                    'update 

                                    SQL = "update detail_jurnal set Kredit = Kredit + " & deposit_PPHSubsidi & " where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPPHSubsidi & "'"
                                    ExecuteTrans(SQL)
                                Else
                                    Dr.Close()
                                    'insert
                                    SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(akunPPHSubsidi, 1),
                                            Strings.Mid(akunPPHSubsidi, 2, 1),
                                            Strings.Mid(Ganti(akunPPHSubsidi), 3),
                                            KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, "0", deposit_PPHSubsidi, pagenumber, lokasi_penj, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                    ExecuteTrans(SQL)
                                    pagenumber = pagenumber + 1
                                End If
                            End Using
                        Else
                            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPPHSubsidi & "'  and lokasi_detail = '" & lokasi_penj & "'"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Dr.Close()
                                    'update 

                                    SQL = "update detail_jurnal set Kredit = Kredit + " & deposit_PPHSubsidi & " where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akunPPHSubsidi & "'"
                                    ExecuteTrans(SQL)
                                Else
                                    Dr.Close()
                                    'insert
                                    SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(akunPPHSubsidi, 1),
                                            Strings.Mid(akunPPHSubsidi, 2, 1),
                                            Strings.Mid(Ganti(akunPPHSubsidi), 3),
                                            KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, "0", deposit_PPHSubsidi, pagenumber, lokasi_penj, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                    ExecuteTrans(SQL)
                                    pagenumber = pagenumber + 1
                                End If
                            End Using
                        End If
                    End If

                    'INI VOUCHER PIUTANG
                    SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_piutang & "' and lokasi_detail = '" & lokasi_penj & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            'update 
                            RRRRRR = 19
                            SQL = "update detail_jurnal set kredit = kredit + " & (Val(HilangkanTanda(LvJml))) & " where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_piutang & "' and lokasi_detail = '" & lokasi_penj & "'"
                            ExecuteTrans(SQL)
                        Else
                            Dr.Close()
                            'insert
                            RRRRRR = 20
                            pagenumber = pagenumber + 1

                            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_piutang, 1),
                                   Strings.Mid(coa_piutang, 2, 1),
                                   Strings.Mid(Ganti(coa_piutang), 3),
                                   KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, "0", (Val(HilangkanTanda(LvJml))), pagenumber + 1, lokasi_penj, Bahasa_Pilihan, Ket_Cost_Center_HO)
                            ExecuteTrans(SQL)
                        End If
                    End Using



                    ''If deposit_FlagReimbursePPH = "Y" Then
                    ''    SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    ''    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_piutang & "' and lokasi_detail = '" & lokasi_penj & "'"
                    ''    Using Dr = OpenTrans(SQL)
                    ''        If Dr.Read Then
                    ''            Dr.Close()
                    ''            'update 
                    ''            RRRRRR = 19
                    ''            SQL = "update detail_jurnal set kredit = kredit + " & (Val(HilangkanTanda(LvJml))) & " where "
                    ''            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    ''            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_piutang & "' and lokasi_detail = '" & lokasi_penj & "'"
                    ''            ExecuteTrans(SQL)
                    ''        Else
                    ''            Dr.Close()
                    ''            'insert
                    ''            RRRRRR = 20
                    ''            pagenumber = pagenumber + 1

                    ''            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_piutang, 1), _
                    ''                   Strings.Mid(coa_piutang, 2, 1), _
                    ''                   Strings.Mid(Ganti(coa_piutang), 3), _
                    ''                   KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, "0", (Val(HilangkanTanda(LvJml))) + deposit_PPN, pagenumber + 1, lokasi_penj)
                    ''            ExecuteTrans(SQL)
                    ''        End If
                    ''    End Using
                    ''Else
                    ''    SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    ''    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_piutang & "' and lokasi_detail = '" & lokasi_penj & "'"
                    ''    Using Dr = OpenTrans(SQL)
                    ''        If Dr.Read Then
                    ''            Dr.Close()
                    ''            'update 
                    ''            RRRRRR = 19
                    ''            SQL = "update detail_jurnal set kredit = kredit + " & (Val(HilangkanTanda(LvJml))) + deposit_PPN - deposit_PPH & " where "
                    ''            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    ''            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_piutang & "' and lokasi_detail = '" & lokasi_penj & "'"
                    ''            ExecuteTrans(SQL)
                    ''        Else
                    ''            Dr.Close()
                    ''            'insert
                    ''            RRRRRR = 20
                    ''            pagenumber = pagenumber + 1

                    ''            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_piutang, 1), _
                    ''                   Strings.Mid(coa_piutang, 2, 1), _
                    ''                   Strings.Mid(Ganti(coa_piutang), 3), _
                    ''                   KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, "0", (Val(HilangkanTanda(LvJml))) + deposit_PPN - deposit_PPH, pagenumber + 1, lokasi_penj)
                    ''            ExecuteTrans(SQL)
                    ''        End If
                    ''    End Using
                    ''End If

                    'CODING POTONG DEPOSIT, KODE VOUCHER Ke 2 UNTUK PINDAHIN HUTANG PROMOSI KE REBATE/B2B
                    '_________________________________________________________________________________________________________________________________
                    If deposit_inputRBT <> 0 Then
                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_HutangPromo & "'  and lokasi_detail = '" & lokasi_penj & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update 

                                SQL = "update detail_jurnal set debit = debit + " & deposit_inputRBT & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_HutangPromo & "'"
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert
                                SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_HutangPromo, 1),
                                        Strings.Mid(coa_HutangPromo, 2, 1),
                                        Strings.Mid(Ganti(coa_HutangPromo), 3),
                                        KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, deposit_inputRBT, "0", pagenumber, lokasi_penj, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                                pagenumber2 = pagenumber2 + 1
                            End If
                        End Using

                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Rebate & "'  and lokasi_detail = '" & lokasi_penj & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update 

                                SQL = "update detail_jurnal set Kredit = Kredit + " & deposit_inputRBT & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Rebate & "'"
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert
                                SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_Rebate, 1),
                                        Strings.Mid(coa_Rebate, 2, 1),
                                        Strings.Mid(Ganti(coa_Rebate), 3),
                                        KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, "0", deposit_inputRBT, pagenumber, lokasi_penj, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                                pagenumber2 = pagenumber2 + 1
                            End If
                        End Using
                    End If

                    If deposit_inputB2B <> 0 Then

                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_HutangPromo & "'  and lokasi_detail = '" & lokasi_penj & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update 

                                SQL = "update detail_jurnal set debit = debit + " & deposit_inputRBT & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_HutangPromo & "'"
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert
                                SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_HutangPromo, 1),
                                        Strings.Mid(coa_HutangPromo, 2, 1),
                                        Strings.Mid(Ganti(coa_HutangPromo), 3),
                                        KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, deposit_inputRBT, "0", pagenumber, lokasi_penj, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                                pagenumber2 = pagenumber2 + 1
                            End If
                        End Using

                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_B2B & "'  and lokasi_detail = '" & lokasi_penj & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update 

                                SQL = "update detail_jurnal set Kredit = Kredit + " & deposit_inputB2B & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_B2B & "'"
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert
                                SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_B2B, 1),
                                        Strings.Mid(coa_B2B, 2, 1),
                                        Strings.Mid(Ganti(coa_B2B), 3),
                                        KodePerusahaan, KodeProyek, "Pelunasan piutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, "0", deposit_inputB2B, pagenumber, lokasi_penj, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                                pagenumber2 = pagenumber2 + 1
                            End If
                        End Using

                    End If
                    '_________________________________________________________________________________________________________________________________


                    RRRRRR = 21
                    SQL = "INSERT INTO Cek_Pelunasan_Per_Step"
                    SQL = SQL & "(Kode_Perusahaan, No_Val, No_Faktur, Tanggal, Jam,Jenis, Urut_Form)"
                    SQL = SQL & "VALUES('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & LvFak.Trim & "', "
                    SQL = SQL & "'" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
                    SQL = SQL & "'" & "KREDIT" & "',5)"
                    ExecuteTrans(SQL)
                    RRRRRR = 22
                    If SisaHutang = Val(HilangkanTanda(LvJml)) Then
                        'inii
                        'SQL = "Update penjualan set flag_lunas_tunai = 'Y', "
                        'SQL = SQL & "Tgl_lunas_tunai = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                        'SQL = SQL & "jam_lunas_tunai = '" & Format(CDate(Fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                        'SQL = SQL & "uservalidasi_tunai = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                        'SQL = SQL & "no_faktur = '" & LvFak.Trim & "'"
                        'ExecuteTrans(SQL)
                        'LvKdCus = "01"
                        RRRRRR = 23
                        SQL = "select * from member_log_Harian where Tanggal = '" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "' "
                        Using Dr = OpenTrans(SQL)
                            If Not Dr.Read Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses tidak dapat dilanjutkan, Harus Menunggu DO dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            End If
                        End Using
                        RRRRRR = 24
                        Dim plafon As Double = 0
                        Dim Number_Member As Integer = 0
                        Dim Tambahan_Plafon As Double = 0
                        Dim Persen_Plafon As Double = 0
                        Dim Hari As Integer = 0
                        Dim MemberSebelum As String = ""
                        Dim KategoriMemberSebelum As String = ""
                        RRRRRR = 25
                        SQL = "select Persen_Kenaikan, a.Plafon, a.Number_Member, a.Kode_Member, a.Kode_Kategori_Member "
                        SQL = SQL & "from customers a, Member B where a.Kode_Member = b.Kode_Member "
                        SQL = SQL & "and a.Kode_Kategori_Member = b.Kode_Kategori and a.kode_perusahaan = '" & KodePerusahaan & "' and Kode_Customer ='" & LvKdCus & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                RRRRRR = 26
                                plafon = Dr("Plafon")
                                Number_Member = Dr("Number_Member")
                                Tambahan_Plafon = HilangkanTanda(Format((Dr("Persen_Kenaikan") * Jumlah_Hutang) / 100, "N0"))
                                Persen_Plafon = Dr("Persen_Kenaikan")
                                MemberSebelum = Dr("Kode_Member")
                                KategoriMemberSebelum = Dr("Kode_Kategori_Member")
                            Else
                                RRRRRR = 27
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Kategori member tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using

                        'Hari = DateDiff(DateInterval.Day, CDate("2023-01-01"), DateTimePicker1.Value)
                        Hari = DateDiff(DateInterval.Day, CDate(LvTgl), DateTimePicker1.Value)
                        RRRRRR = 28
                        SQL = "insert into member_log_check_pelunasan "
                        SQL = SQL & "(Kode_Perusahaan, Kode_customer, No_Pelunasan, No_DO, Tanggal, Jam, Urut_Customer) "
                        SQL = SQL & " Values('" & KodePerusahaan & "', '" & LvKdCus & "', '" & TxtFaktur.Text.Trim & "', '" & LvFak.Trim & "', "
                        SQL = SQL & "'" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & Number_Member & "') "
                        ExecuteTrans(SQL)
                        RRRRRR = 29
                        SQL = "insert into member_Data_Masuk "
                        SQL = SQL & "(Kode_Perusahaan, No_Faktur, No_DO_Pelunasan, Jenis) "
                        SQL = SQL & " Values('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & LvFak.Trim & "','PELUNASAN KREDIT') "
                        ExecuteTrans(SQL)
                        RRRRRR = 30
                        SQL = "select Kode_Member, Kode_Kategori, Plafond_Dari, "
                        SQL = SQL & "Plafond_Sampai, Hari_Dari, Hari_Sampai, Urutan from Member "
                        SQL = SQL & "order by Urutan "
                        Using Ds2 = BindingTrans(SQL)
                            For index3 As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1
                                RRRRRR = 31
                                If plafon + Tambahan_Plafon >= Ds2.Tables("MyTable").Rows(index3).Item("Plafond_Dari") And
                                plafon + Tambahan_Plafon <= Ds2.Tables("MyTable").Rows(index3).Item("Plafond_Sampai") And
                                Hari >= Ds2.Tables("MyTable").Rows(index3).Item("Hari_Dari") And
                                Hari <= Ds2.Tables("MyTable").Rows(index3).Item("Hari_Sampai") Then
                                    RRRRRR = 32
                                    SQL = "insert into member_log_check_all "
                                    SQL = SQL & "(Kode_Perusahaan, Tanggal, Jam, Plafond_Tambahan, Kode_Customer, Nilai_Plafond, Kode_Member, "
                                    SQL = SQL & "Kode_Kategori, Plafond_Dari, Plafond_Sampai, Hari_Dari, Hari_Sampai, Urutan_Member, No_Faktur, Jenis, Total_Plafond, Plafon_Sebelum, Member_Sebelum, Kode_Kategori_Sebelum, Urut_Customer, No_DO_Pelunasan) "
                                    SQL = SQL & "Values('" & KodePerusahaan & "', '" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
                                    SQL = SQL & "'" & Tambahan_Plafon & "', '" & LvKdCus & "', '" & Persen_Plafon & "', "
                                    SQL = SQL & "'" & Ds2.Tables("MyTable").Rows(index3).Item("Kode_Member") & "', '" & Ds2.Tables("MyTable").Rows(index3).Item("Kode_Kategori") & "', "
                                    SQL = SQL & "'" & Ds2.Tables("MyTable").Rows(index3).Item("Plafond_Dari") & "', '" & Ds2.Tables("MyTable").Rows(index3).Item("Plafond_Sampai") & "', "
                                    SQL = SQL & "'" & Ds2.Tables("MyTable").Rows(index3).Item("Hari_Dari") & "', '" & Ds2.Tables("MyTable").Rows(index3).Item("Hari_Sampai") & "', "
                                    SQL = SQL & "'" & Ds2.Tables("MyTable").Rows(index3).Item("Urutan") & "', '" & TxtFaktur.Text.Trim & "', 'PELUNASAN KREDIT','" & plafon + Tambahan_Plafon & "', "
                                    SQL = SQL & "'" & plafon & "', '" & MemberSebelum & "', '" & KategoriMemberSebelum & "','" & Number_Member & "', '" & LvFak.Trim & "')"
                                    ExecuteTrans(SQL)
                                    Number_Member += 1


                                    RRRRRR = 33

                                    SQL = "Update Customers set Kode_Member ='" & Ds2.Tables("MyTable").Rows(index3).Item("Kode_Member") & "', "
                                    SQL = SQL & "Kode_Kategori_Member = '" & Ds2.Tables("MyTable").Rows(index3).Item("Kode_Kategori") & "', "
                                    SQL = SQL & "Plafon = plafon + " & Tambahan_Plafon & ", Number_Member = '" & Number_Member & "' "
                                    SQL = SQL & "where Kode_customer ='" & LvKdCus & "' "
                                    ExecuteTrans(SQL)
                                    RRRRRR = 34
                                    Exit For
                                End If

                            Next
                        End Using

                        RRRRRR = 34


                        SQL = "INSERT INTO Cek_Pelunasan_Per_Step"
                        SQL = SQL & "(Kode_Perusahaan, No_Val, No_Faktur, Tanggal, Jam,Jenis, Urut_Form)"
                        SQL = SQL & "VALUES('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & LvFak.Trim & "', "
                        SQL = SQL & "'" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
                        SQL = SQL & "'" & "KREDIT" & "',6)"
                        ExecuteTrans(SQL)
                        RRRRRR = 35
                        SQL = "Update do_new set flag_lunas_do = 'Y', "
                        SQL = SQL & "nTgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                        SQL = SQL & "njam_lunas = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                        SQL = SQL & "nuservalidasi = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "no_do = '" & LvFak.Trim & "'"
                        ExecuteTrans(SQL)
                        RRRRRR = 36

                        SQL = "INSERT INTO Cek_Pelunasan_Per_Step"
                        SQL = SQL & "(Kode_Perusahaan, No_Val, No_Faktur, Tanggal, Jam,Jenis, Urut_Form)"
                        SQL = SQL & "VALUES('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & LvFak.Trim & "', "
                        SQL = SQL & "'" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
                        SQL = SQL & "'" & "KREDIT" & "',7)"
                        ExecuteTrans(SQL)
                        RRRRRR = 37
                        SQL = "select a.no_do from do_new a, penjualan b, Member_Penurunan_Plafond C where "
                        SQL = SQL & "a.Kode_Perusahaan= b.Kode_Perusahaan and a.No_Faktur= b.no_faktur and "
                        SQL = SQL & "a.kode_perusahaan = c.Kode_Perusahaan and a.cek_member= c.Kode_Plafond and "
                        SQL = SQL & "a.Flag_Lunas_DO is null and b.Kode_Customer='" & LvKdCus & "' and c.Flag_Blacklist ='Y' "
                        Using dr = OpenTrans(SQL)
                            If Not dr.Read Then
                                dr.Close()
                                RRRRRR = 38
                                SQL = "Update Customers set Blacklist ='T' "
                                SQL = SQL & "where Kode_customer ='" & LvKdCus & "' "
                                ExecuteTrans(SQL)
                            End If
                        End Using

                    End If
                    RRRRRR = 39
                    'If nilai_dpp_diskon_cash_per_faktur <> 0 Then
                    '    SQL = "update penjualan set val_diskon_cash = 'Y', "
                    '    SQL = SQL & "tgl_val_diskon_cash = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                    '    SQL = SQL & "jam_val_diskon_cash = '" & Format(CDate(Fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                    '    SQL = SQL & "user_val_diskon_cash = '" & UserID & "', "
                    '    SQL = SQL & "nilai_val_diskon_cash = '" & nilai_dpp_diskon_cash_per_faktur & "', "
                    '    SQL = SQL & "ppn_val_diskon_cash = '" & nilai_ppn_diskon_cash_per_faktur & "', "
                    '    SQL = SQL & "ket_val_diskon_cash = '-' where "
                    '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    '    SQL = SQL & "no_faktur = '" & LvFak.Trim & "'"
                    '    ExecuteTrans(SQL)
                    'End If




                Next

                If jumlah_Um + Deposit <> Val(HilangkanTanda(TextBoxa.Text)) Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Total Uang Masuk Tidak sama dengan Bayar . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

                SQL = "insert into val_do(kode_perusahaan, no_val, tanggal, jam, "
                SQL = SQL & "keterangan, uservalidasi, cara_bayar, grand, kode_voucher, hrs_update, kode_unik, Kode_VOucher2) values('" & KodePerusahaan & "', "
                SQL = SQL & "'" & TxtFaktur.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', "
                SQL = SQL & "'" & TextBox2.Text.Trim & "', '" & UserID & "', "
                SQL = SQL & "'" & arrCrByr.Item(ComboBoxCb1.SelectedIndex) & "', "
                SQL = SQL & "" & HilangkanTanda(TextBoxa.Text) & ", '" & Kode_Voucher & "', 'Y', '" & Label11.Text & "', " & Kode_voucher2_ & ")"
                ExecuteTrans(SQL)

                SQL = "INSERT INTO Cek_Pelunasan_Per_Step"
                SQL = SQL & "(Kode_Perusahaan, No_Val, No_Faktur, Tanggal, Jam,Jenis, Urut_Form)"
                SQL = SQL & "VALUES('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '-', "
                SQL = SQL & "'" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
                SQL = SQL & "'" & "KREDIT" & "',8)"
                ExecuteTrans(SQL)



                For i As Integer = 0 To ListView1.Items.Count - 1
                    Get_Isi_Listview(i)
                    'inii
                    SQL = "insert into detail_val_do(kode_perusahaan, no_val, no_faktur, byr, sisa) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
                    SQL = SQL & "'" & LvFak.Trim & "', " & HilangkanTanda(LvJml) & ", "
                    SQL = SQL & "" & Val(HilangkanTanda(LvJml)) & ")"
                    ExecuteTrans(SQL)


                    SQL = "select a.flag_cabang_sendiri, a.coa_piutang, a.lokasi, a.grand, a.status, a.jenis_transaksi, a.kode_customer, "

                    SQL = SQL & "isnull((select sum(x.grand) as ttl_retur from retur_penjualan x where x.kode_perusahaan = a.kode_perusahaan and "
                    SQL = SQL & "x.no_faktur_jual = a.no_faktur and x.status is null), 0) as ttl_retur, "

                    SQL = SQL & "isnull((select sum(y.nilai) from retur_mt x,Retur_MT_Det y, do_new z where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and "
                    SQL = SQL & "y.No_Faktur = z.No_Faktur And y.no_faktur = z.no_do and "
                    SQL = SQL & "y.Kode_Perusahaan = a.Kode_Perusahaan and z.no_faktur = a.No_faktur and x.Status is null "
                    SQL = SQL & "), 0) as pernah_ret_MT, "

                    SQL = SQL & "isnull((select sum(y.byr) from val_penj x, detail_val_penj y where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_val = y.no_val and "
                    SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
                    SQL = SQL & "y.no_faktur = a.no_faktur), 0) as ttl_validasi_kredit, "

                    SQL = SQL & "isnull((select sum(y.byr + y.disc_cash) from val_penj_tunai x, detail_val_penj_tunai y where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_val = y.no_val and "
                    SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
                    SQL = SQL & "y.no_faktur = a.no_faktur), 0) as ttl_validasi_tunai, "

                    SQL = SQL & "isnull((select sum(y.byr) from val_do x, detail_val_do y, do_new z where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and "
                    SQL = SQL & "x.no_val = y.no_val And y.no_faktur = z.no_do and "
                    SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and z.status is null and "
                    SQL = SQL & "z.no_faktur = a.no_faktur), 0) as ttl_validasi_do_kredit, "

                    SQL = SQL & "isnull((select sum(y.byr + y.disc_cash) from val_do_tunai x, detail_val_do_tunai y, do_new z where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and "
                    SQL = SQL & "x.no_val = y.no_val And y.no_faktur = z.no_do and "
                    SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and z.status is null and "
                    SQL = SQL & "z.no_faktur = a.no_faktur), 0) as ttl_validasi_do_tunai "

                    SQL = SQL & "from penjualan a where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_faktur = '" & LvNoProforma.Trim & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If Dr("jenis_transaksi") = "T" Then
                                If Dr("grand") - Dr("ttl_retur") - Dr("pernah_ret_MT") - Dr("ttl_validasi_kredit") - Dr("ttl_validasi_tunai") - Dr("ttl_validasi_do_kredit") - Dr("ttl_validasi_do_tunai") = 0 Then
                                    Dr.Close()

                                    SQL = "Update penjualan set flag_lunas_tunai = 'Y', "
                                    SQL = SQL & "Tgl_lunas_tunai = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                                    SQL = SQL & "jam_lunas_tunai = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                                    SQL = SQL & "uservalidasi_tunai = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "no_faktur = '" & LvFak.Trim & "'"
                                    ExecuteTrans(SQL)
                                End If
                            Else
                                If Dr("grand") - Dr("ttl_retur") - Dr("pernah_ret_MT") - Dr("ttl_validasi_kredit") - Dr("ttl_validasi_tunai") - Dr("ttl_validasi_do_kredit") - Dr("ttl_validasi_do_tunai") = 0 Then
                                    Dr.Close()

                                    SQL = "Update penjualan set flag_lunas = 'Y', "
                                    SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                                    SQL = SQL & "jam_lunas = '" & Format(tgl_skg, "HH:mm:ss") & "', "
                                    SQL = SQL & "uservalidasi = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "no_faktur = '" & LvFak.Trim & "'"
                                    ExecuteTrans(SQL)
                                End If
                            End If

                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Nomor faktur tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "INSERT INTO Cek_Pelunasan_Per_Step"
                    SQL = SQL & "(Kode_Perusahaan, No_Val, No_Faktur, Tanggal, Jam,Jenis, Urut_Form)"
                    SQL = SQL & "VALUES('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & LvFak.Trim & "', "
                    SQL = SQL & "'" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
                    SQL = SQL & "'" & "KREDIT" & "',9)"
                    ExecuteTrans(SQL)

                    Dim sisa As Double = 0



                    'awal coding stenly
                    ArrUrut.Clear() : ArrNilai.Clear()
                    SQL = "select urut_um,nilai_yang_diinput from Perlunasan_Per_Step_sementara where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_customer = '" & LvKdCus.Trim & "' and "
                    SQL = SQL & "kode_unik = '" & Label11.Text & "' and "
                    SQL = SQL & "No_DO ='" & LvFak.Trim & "' and "
                    SQL = SQL & "kode_cb = '" & arrCrByr.Item(ComboBoxCb1.SelectedIndex) & "' order by urut_um "
                    Using Dr = OpenTrans(SQL)
                        Do While Dr.Read
                            ArrUrut.Add(Dr("urut_um"))
                            ArrNilai.Add(Dr("nilai_yang_diinput"))
                        Loop
                    End Using

                    ArrUrutDpt.Clear() : ArrNilaiDpt.Clear()
                    ArrDptPPH.Clear() : ArrDptPPN.Clear()
                    ArrDptNilaiPPH.Clear() : ArrDptNilaiPPN.Clear()
                    ArrDptFlagReimburse.Clear() : ArrNilaiKlaimDpt.Clear()
                    SQL = "select urut_dpt,nilai_yang_diinput, persenPPH, NilaiPPH, persenPPN, NIlaiPPN, Flag_Reimburse, nilai_yang_diKlaim  "
                    SQL = SQL & "from Perlunasan_Per_Step_sementara_deposit where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_customer = '" & LvKdCus.Trim & "' and "
                    SQL = SQL & "kode_unik = '" & Label11.Text & "' and "
                    SQL = SQL & "No_DO ='" & LvFak.Trim & "' "
                    SQL = SQL & "order by urut_dpt "
                    Using Dr = OpenTrans(SQL)
                        Do While Dr.Read
                            ArrUrutDpt.Add(Dr("urut_dpt"))
                            ArrNilaiDpt.Add(Dr("nilai_yang_diinput"))
                            ArrDptPPH.Add(Dr("persenPPH"))
                            ArrDptPPN.Add(Dr("persenPPN"))
                            ArrDptNilaiPPH.Add(Dr("NilaiPPH"))
                            ArrDptNilaiPPN.Add(Dr("NIlaiPPN"))
                            ArrDptFlagReimburse.Add(General_Class.CekNULL(Dr("Flag_Reimburse")))
                            ArrNilaiKlaimDpt.Add(Dr("nilai_yang_diKlaim"))
                        Loop
                    End Using


                    sisa = Val(HilangkanTanda(LvJml))

                    '=================================================
                    '=     DI KOMEN SEMENTARA (07 JANURARI 2025)     =
                    '=================================================
#Region "DI KOMEN SEMENTARA (07 JANURARI 2025)"

                    For z As Integer = 0 To ArrUrut.Count - 1


                        SQL = "select a.kode_cb, a.no_val, a.sisa as total from "
                        SQL = SQL & "um_global a where "
                        SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "sisa > 0 and "
                        SQL = SQL & "a.kode_cb = '" & arrCrByr.Item(ComboBoxCb1.SelectedIndex) & "' and "
                        SQL = SQL & "a.urut in (" & ArrUrut.Item(z).ToString & ") "
                        SQL = SQL & "order by a.urut"
                        Using Ds = BindingTrans(SQL)
                            With Ds.Tables("MyTable")
                                If .Rows.Count <> 0 Then


                                    For h As Integer = 0 To .Rows.Count - 1

                                        If .Rows(h).Item("total") - Val(ArrNilai.Item(z)) < 0 Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Nilai UM/Deposit Tidak Cukup . . .!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            Exit Sub
                                        End If

                                        SQL = "Update um_global set sisa = sisa - " & ArrNilai.Item(z) & " where "
                                        SQL = SQL & "urut = '" & ArrUrut.Item(z) & "' "
                                        ExecuteTrans(SQL)

                                        SQL = "insert into det_do_um(kode_perusahaan, no_do, "
                                        SQL = SQL & "no_val, jumlah, no_pelunasan) values('" & KodePerusahaan & "', "
                                        SQL = SQL & "'" & LvFak & "', '" & .Rows(h).Item("no_val") & "', "
                                        SQL = SQL & "'" & ArrNilai.Item(z) & "', '" & TxtFaktur.Text.Trim & "')"
                                        ExecuteTrans(SQL)
                                        'akhir coding stenly

                                        sisa = sisa - Val(ArrNilai.Item(z))
                                    Next
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data uang masuk tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End With
                        End Using


                    Next

                    For z As Integer = 0 To ArrUrutDpt.Count - 1


                        SQL = "select a.no_Klaim, a.sisa as total from "
                        SQL = SQL & "deposit_customers a where "
                        SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "round(sisa,0) > 0 and "
                        SQL = SQL & "a.urut in (" & ArrUrutDpt.Item(z).ToString & ") "
                        SQL = SQL & "order by a.urut"
                        Using Ds = BindingTrans(SQL)
                            With Ds.Tables("MyTable")
                                If .Rows.Count <> 0 Then


                                    For h As Integer = 0 To .Rows.Count - 1

                                        If .Rows(h).Item("total") - Val(ArrNilaiDpt.Item(z)) < 0 Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Nilai UM/Deposit Tidak Cukup . . .!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            Exit Sub
                                        End If

                                        SQL = "Update deposit_customers set sisa = sisa - " & ArrNilaiDpt.Item(z) & " where "
                                        SQL = SQL & "urut = '" & ArrUrutDpt.Item(z) & "' "
                                        ExecuteTrans(SQL)

                                        Dim flag_reimburse As String = "NULL"
                                        If ArrDptFlagReimburse.Item(z) = "Y" Then
                                            flag_reimburse = "'Y'"
                                        End If

                                        SQL = "insert into deposit_customers_log(kode_perusahaan, no_do_Tagihan, "
                                        SQL = SQL & "no_Klaim, nilai, no_pelunasan_Tagihan, Kode_Customer, userid, Tanggal, Jam, Urut_Deposit, Jenis, PersenPPH, NilaiPPH, PersenPPN, NilaiPPN, Flag_Reimburse, Total_Klaim)"
                                        SQL = SQL & " values('" & KodePerusahaan & "', "
                                        SQL = SQL & "'" & LvFak & "', '" & .Rows(h).Item("no_Klaim") & "', "
                                        SQL = SQL & "'" & ArrNilaiDpt.Item(z) & "', '" & TxtFaktur.Text.Trim & "', '" & LvKdCus & "', "
                                        SQL = SQL & "'" & UserID & "', '" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & ArrUrutDpt.Item(z) & "', 'POT TAGIHAN', "
                                        SQL = SQL & "'" & ArrDptPPH.Item(z) & "', '" & ArrDptNilaiPPH.Item(z) & "', '" & ArrDptPPN.Item(z) & "', '" & ArrDptNilaiPPN.Item(z) & "', " & flag_reimburse & ", '" & ArrNilaiKlaimDpt.Item(z) & "')"
                                        ExecuteTrans(SQL)


                                        'akhir coding stenly
                                        sisa = sisa - Val(ArrNilaiKlaimDpt.Item(z))
                                    Next
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data uang masuk tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End With
                        End Using


                    Next

                    If sisa <> 0 Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Nilai DO lebih besar dari nilai input  . .  !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

#End Region


                    SQL = "INSERT INTO Cek_Pelunasan_Per_Step "
                    SQL = SQL & "(Kode_Perusahaan, No_Val, No_Faktur, Tanggal, Jam,Jenis, Urut_Form)"
                    SQL = SQL & "VALUES('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & LvFak.Trim & "', "
                    SQL = SQL & "'" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
                    SQL = SQL & "'" & "KREDIT" & "',10)"
                    ExecuteTrans(SQL)
                    'Next
                Next

                'For i As Integer = 0 To ListView1.Items.Count - 1
                '    Get_Isi_Listview(i)
                '    'inii
                '    Dim xarr_total_sisa, xarr_no_do As New ArrayList

                '    SQL = "select no_do, ngrand - sudah_dilunasi as ttl from "
                '    SQL = SQL & "do_new where "
                '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '    SQL = SQL & "no_do = '" & LvFak.Trim & "' and flag_lunas_do is null order by tanggal_do"
                '    Using Dr = OpenTrans(SQL)
                '        Do While Dr.Read
                '            xarr_no_do.Add(Dr("no_do"))
                '            xarr_total_sisa.Add(Dr("ttl"))
                '        Loop
                '    End Using

                '    Dim sisa As Double = 0
                '    For yy As Integer = 0 To xarr_no_do.Count - 1


                '        SQL = "select a.no_val, b.sisa as total from "
                '        SQL = SQL & "val_do a, detail_val_do b where "
                '        SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.no_val = b.no_val and "
                '        SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                '        SQL = SQL & "b.no_faktur = '" & LvFak.Trim & "' and a.status is null and sisa <> 0 "
                '        SQL = SQL & "order by a.tanggal"
                '        Using Ds = BindingTrans(SQL)
                '            With Ds.Tables("MyTable")
                '                If .Rows.Count <> 0 Then
                '                    sisa = xarr_total_sisa.Item(yy)

                '                    For h As Integer = 0 To .Rows.Count - 1
                '                        If sisa = 0 Then
                '                            Exit For
                '                        ElseIf sisa < 0 Then
                '                            CloseTrans()
                '                            CloseConn()
                '                            MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                            Exit Sub
                '                        End If

                '                        If sisa < .Rows(h).Item("total") Or sisa = .Rows(h).Item("total") Then
                '                            'SQL = "select (total_baru_dikurang_diskon + nilai_ppn_baru) - sudah_dilunasi as ttl from "
                '                            'SQL = SQL & "rekap_sub_invoice where "
                '                            'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '                            'SQL = SQL & "no_do = '" & xarr_no_do.Item(yy) & "' and flag_lunas_do is null"
                '                            'Using Dr = OpenTrans(SQL)
                '                            '    If Dr.Read Then
                '                            '        'If Dr("ttl") <> sisa Then
                '                            '        '    Dr.Close()
                '                            '        '    CloseTrans()
                '                            '        '    CloseConn()
                '                            '        '    MessageBox.Show("Nilai sisa DO ini sudah berubah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                            '        '    Exit Sub
                '                            '        'Else
                '                            '        'Dr.Close()
                '                            '        'SQL = "update rekap_sub_invoice set flag_lunas_do = 'Y', sudah_dilunasi = sudah_dilunasi + " & sisa & " where "
                '                            '        'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '                            '        'SQL = SQL & "no_do = '" & xarr_no_do.Item(yy) & "'"
                '                            '        'ExecuteTrans(SQL)
                '                            '        ' End If
                '                            '    Else
                '                            '        Dr.Close()
                '                            '        CloseTrans()
                '                            '        CloseConn()
                '                            '        MessageBox.Show("No DO tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                            '        Exit Sub
                '                            '    End If
                '                            'End Using

                '                            SQL = "update do_new set flag_lunas_do = 'Y', sudah_dilunasi = sudah_dilunasi + " & sisa & " where "
                '                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '                            SQL = SQL & "no_do = '" & xarr_no_do.Item(yy) & "'"
                '                            ExecuteTrans(SQL)

                '                            SQL = "Update detail_val_do set sisa = sisa - " & sisa & " where "
                '                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '                            SQL = SQL & "no_val = '" & .Rows(h).Item("no_val") & "' and no_faktur = '" & LvFak.Trim & "'"
                '                            ExecuteTrans(SQL)

                '                            SQL = "insert into det_do_pelunasan(kode_perusahaan, no_do, "
                '                            SQL = SQL & "no_val, jumlah) values('" & KodePerusahaan & "', "
                '                            SQL = SQL & "'" & xarr_no_do.Item(yy) & "', '" & .Rows(h).Item("no_val") & "', "
                '                            SQL = SQL & "'" & sisa & "')"
                '                            ExecuteTrans(SQL)

                '                            sisa = 0
                '                        ElseIf sisa > .Rows(h).Item("total") Then
                '                            'SQL = "insert into det_penj(kode_perusahaan, no_faktur, "
                '                            'SQL = SQL & "kode_stock_owner, kode_barang, serial_number, no_urut, "
                '                            'SQL = SQL & "jumlah) values('" & KodePerusahaan & "', "
                '                            'SQL = SQL & "'" & TxtFaktur.Text.Trim & "', "
                '                            'SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
                '                            'SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
                '                            'SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
                '                            'SQL = SQL & "" & x_no_urut_det_penj & ", "
                '                            'SQL = SQL & "'" & .Rows(h).Item("jumlah") & "')"
                '                            'ExecuteTrans(SQL)

                '                            'SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
                '                            'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '                            'SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                '                            'SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                '                            'SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                '                            'ExecuteTrans(SQL)

                '                            '  total_hpp = total_hpp + (.Rows(h).Item("jumlah") * Get_Harga_SN(.Rows(h).Item("serial_number")))

                '                            SQL = "update do_new set sudah_dilunasi = sudah_dilunasi + " & .Rows(h).Item("total") & " where "
                '                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '                            SQL = SQL & "no_do = '" & xarr_no_do.Item(yy) & "'"
                '                            ExecuteTrans(SQL)

                '                            SQL = "Update detail_val_do set sisa = sisa - sisa where "
                '                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '                            SQL = SQL & "no_val = '" & .Rows(h).Item("no_val") & "' and no_faktur = '" & LvFak.Trim & "'"
                '                            ExecuteTrans(SQL)

                '                            SQL = "insert into det_do_pelunasan(kode_perusahaan, no_do, "
                '                            SQL = SQL & "no_val, jumlah) values('" & KodePerusahaan & "', "
                '                            SQL = SQL & "'" & xarr_no_do.Item(yy) & "', '" & .Rows(h).Item("no_val") & "', "
                '                            SQL = SQL & "'" & .Rows(h).Item("total") & "')"
                '                            ExecuteTrans(SQL)

                '                            sisa = sisa - .Rows(h).Item("total")
                '                        Else
                '                            CloseTrans()
                '                            CloseConn()
                '                            MessageBox.Show("Detail pelunasan terjadi kesalahan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                            Exit Sub
                '                        End If
                '                    Next
                '                Else
                '                    'CloseTrans()
                '                    'CloseConn()
                '                    'MessageBox.Show("Pelunasan tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                    'Exit Sub
                '                End If
                '            End With
                '        End Using
                '    Next
                'Next

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


                If ada_nilai_di_deposit = "Y" Then
                    SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
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

                SQL = "update Check_In_Pelunasan set "
                SQL = SQL & "Tgl_Keluar = '" & Format(tgl_skg, "yyyy-MM-dd") & "',"
                SQL = SQL & "Jam_Keluar = '" & Format(tgl_skg, "HH:mm:ss") & "',"
                SQL = SQL & "UserId_Keluar = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Unik = '" & Label11.Text & "'"
                ExecuteTrans(SQL)





                Cmd.Transaction.Commit()
                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show("Error : " & RRRRRR & " || " & SQL & "  | " & ex.Message)
                'MessageBox.Show("DEVAAAAAAAAAAAAAAAAAAAAAAAA |||||| " & RRRRRR & " ||||| " & SQL & "  |||| " & ex.Message)
                'MessageBox.Show("DEVAAAAAAAAAAAAAAAAAAAAAAAA |||||| " & RRRRRR & " ||||| " & SQL & "  |||| " & ex.Message)
                'MessageBox.Show("DEVAAAAAAAAAAAAAAAAAAAAAAAA |||||| " & RRRRRR & " ||||| " & SQL & "  |||| " & ex.Message)
                'MessageBox.Show("DEVAAAAAAAAAAAAAAAAAAAAAAAA |||||| " & RRRRRR & " ||||| " & SQL & "  |||| " & ex.Message)
                'MessageBox.Show("DEVAAAAAAAAAAAAAAAAAAAAAAAA |||||| " & RRRRRR & " ||||| " & SQL & "  |||| " & ex.Message)
                Exit Sub
            End Try

        End If

        Dim TanyaCetak As String = MessageBox.Show("Mau dicetak?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If TanyaCetak = vbYes Then
            cetak()
        End If

        Kosong()
        DateTimePicker1.Focus()
    End Sub

    Private Sub DateTimePicker1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox2.Focus()
    End Sub

    Private Sub DateTimePicker1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.Leave
        If Button1.Text.ToUpper = "&SIMPAN" Then
            Try

                OpenConn()

                Get_No_Faktur()

                CloseConn()

            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then ComboBoxCb1.Focus()
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox1.Focus()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Cari("Tidak")
        End If
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        'TextBox9.Text = ListView1.FocusedItem.Text
        'TextBox3.Text = ListView1.FocusedItem.SubItems(1).Text
        'TextBox4.Text = ListView1.FocusedItem.SubItems(2).Text
        'TextBox7.Text = ListView1.FocusedItem.SubItems(3).Text
        'TextBox3.Text = ListView1.FocusedItem.SubItems(4).Text
        'TextBox6.Text = HilangkanTanda(ListView1.FocusedItem.SubItems(5).Text)

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = "delete from Perlunasan_Per_Step_Sementara where Kode_unik='" & Label11.Text & "' and no_do ='" & ListView1.FocusedItem.Text & "' "
            ExecuteTrans(SQL)

            'SQL = "delete from Perlunasan_Per_Step_Sementara_deposit where Kode_unik='" & Label11.Text & "' and no_do ='" & ListView1.FocusedItem.Text & "' "
            'ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        ListView1.FocusedItem.Remove()
        TextBox6.Focus()
        Hitung()
        hide_combobox()
    End Sub

    Private Sub TxtFaktur_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFaktur.KeyPress
        If e.KeyChar = Chr(13) Then
            If DateTimePicker1.Enabled = True Then
                DateTimePicker1.Focus()
            Else
                TextBox2.Focus()
            End If
        End If
    End Sub

    Private Sub TxtFaktur_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtFaktur.Leave
        Try

            If TxtFaktur.Text.Trim.Length = 0 Then
                OpenConn()

                Get_No_Faktur()

                CloseConn()
            End If

            OpenConn()

            Dim lv As New ListViewItem

            SQL = "select a.no_faktur, a.tanggal, a.tgl_jatuh_tempo, c.keterangan, c.cara_bayar, "
            SQL = SQL & "c.grand, c.no_val, a.kode_customer, b.nama as namacustomer, d.byr from "
            SQL = SQL & "penjualan a, customers b, val_do c, detail_val_do d where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And b.kode_perusahaan = c.kode_perusahaan And "
            SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan And a.kode_customer = b.kode_customer and "
            SQL = SQL & "c.no_val = d.no_val and a.no_faktur = d.no_faktur and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and c.no_val = '" & TxtFaktur.Text.Trim & "' "
            SQL = SQL & "order by d.urut "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    'DateTimePicker1.Enabled = False

                    DateTimePicker1.Value = Dr("tanggal")
                    TxtFaktur.Text = Dr("no_val")
                    TextBox2.Text = Dr("keterangan")

                    For i As Integer = 0 To ComboBoxCb1.Items.Count - 1
                        If Dr("cara_bayar") = arrCrByr.Item(i) Then
                            ComboBoxCb1.SelectedIndex = i
                            Exit For
                        End If
                    Next

                    TextBoxa.Text = Format(Dr("grand"), "N0")

                    ListView2.Items.Clear() : ListView1.Items.Clear()

                    lv = ListView1.Items.Add(Dr("no_faktur"))
                    lv.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
                    lv.SubItems.Add(Format(Dr("tgl_jatuh_tempo"), "dd MMM yyyy"))
                    lv.SubItems.Add(Dr("kode_customer"))
                    lv.SubItems.Add(Dr("namacustomer"))
                    lv.SubItems.Add(Format(Dr("byr"), "N0"))

                    Do While Dr.Read
                        lv = ListView1.Items.Add(Dr("no_faktur"))
                        lv.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
                        lv.SubItems.Add(Format(Dr("tgl_jatuh_tempo"), "dd MMM yyyy"))
                        lv.SubItems.Add(Dr("kode_customer"))
                        lv.SubItems.Add(Dr("namacustomer"))
                        lv.SubItems.Add(Format(Dr("byr"), "N0"))
                    Loop

                    Button1.Text = "&Update"
                Else
                    Kosong()
                End If

            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress
        If e.KeyChar = Chr(13) Then

            If ComboBoxCb1.SelectedIndex = -1 Or ComboBoxCb1.SelectedIndex = 0 Then
                MessageBox.Show("Cara Bayar Harus di isi Terlebih dahulu . .  !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If


            For i As Integer = 0 To ListView1.Items.Count - 1
                If ListView1.Items(i).Text.Trim.ToUpper = TextBox9.Text.Trim.ToUpper Then
                    MessageBox.Show("Faktur ini sudah dimasukkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Next


            cek_data = False
            N_EMI_SD_Pilih_Uang_Masuk.LabelDari.Text = "KR"
            N_EMI_SD_Pilih_Uang_Masuk.TextBox2.Text = TextBox9.Text
            N_EMI_SD_Pilih_Uang_Masuk.Cara_Bayar()
            N_EMI_SD_Pilih_Uang_Masuk.ComboBoxCb1.Text = ComboBoxCb1.Text
            N_EMI_SD_Pilih_Uang_Masuk.TextBox3.Text = TextBox7.Text
            N_EMI_SD_Pilih_Uang_Masuk.TextBox1.Text = TextBox6.Text
            N_EMI_SD_Pilih_Uang_Masuk.Label4.Text = Label11.Text
            N_EMI_SD_Pilih_Uang_Masuk.ShowDialog()

            get_jam()
            '==============================================================
            '=     LANGSUNG INSERT TANPA MALALUI SD (07 JANUARI 2025)     =
            '==============================================================
#Region "LANGSUNG INSERT TANPA MALALUI SD (07 JANUARI 2025)"

            'Try
            '    OpenConn()
            '    Cmd.Transaction = Cn.BeginTransaction

            '    SQL = "INSERT INTO Perlunasan_Per_Step_Sementara (Kode_Perusahaan,No_DO,Kode_Unik,No_Val,"
            '    SQL = SQL & "Tanggal,Nilai_Yang_Diinput,Urut_UM,kode_cb,kode_customer) VALUES('" & KodePerusahaan & "',"
            '    SQL = SQL & "'" & TextBox9.Text & "','" & Label11.Text & "','-', "
            '    SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "','" & HilangkanTanda(TextBox6.Text) & "'"
            '    SQL = SQL & ",'" & HilangkanTanda(0) & "','" & arrCrByr.Item(ComboBoxCb1.SelectedIndex) & "'"
            '    SQL = SQL & ",'" & TextBox7.Text & "')"
            '    ExecuteTrans(SQL)

            '    'SQL = "INSERT INTO Perlunasan_Per_Step_Sementara_Deposit (Kode_Perusahaan,No_DO,Kode_Unik,No_Klaim,"
            '    'SQL = SQL & "Tanggal,Nilai_Yang_Diinput,Urut_Dpt,kode_customer, PersenPPN, PersenPPH, NilaiPPN, NilaiPPH, Flag_reimburse, Nilai_Yang_DiKlaim, Flag_B2B, Asal, Jenis_Klaim) VALUES('" & KodePerusahaan & "',"
            '    'SQL = SQL & "'" & TextBox9.Text & "','" & Label11.Text & "','-',"
            '    'SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "','" & HilangkanTanda(TextBox6.Text) & "', "
            '    'SQL = SQL & "'0','" & TextBox7.Text & "','0','0', "
            '    'SQL = SQL & "'0','0',NULL, '" & HilangkanTanda(TextBox6.Text) & "', NULL,'-','-') "
            '    'ExecuteTrans(SQL)

            '    cek_data = True

            '    Cmd.Transaction.Commit()
            '    CloseConn()
            'Catch ex As Exception
            '    CloseTrans()
            '    CloseConn()
            '    MessageBox.Show(ex.Message)
            '    Exit Sub
            'End Try

#End Region

            If cek_data = True Then

                Dim lv As New ListViewItem
                lv = ListView1.Items.Add(TextBox9.Text)
                lv.SubItems.Add(TextBox3.Text)
                lv.SubItems.Add(TextBox4.Text)
                lv.SubItems.Add(TextBox7.Text)
                lv.SubItems.Add(TextBox5.Text)
                lv.SubItems.Add(Format(Val(TextBox6.Text), "N0"))
                lv.SubItems.Add(TextBox14.Text)

                Hitung()
                Kosong_Bawah()
                ListView2.Focus()
            End If
            hide_combobox()
            'ListView1.Columns.Add("No DO", 100, HorizontalAlignment.Left)
            'ListView1.Columns.Add("Tgl Transaksi", 80, HorizontalAlignment.Center) '1
            'ListView1.Columns.Add("Tgl Jth Tmpo", 0, HorizontalAlignment.Center) '2
            'ListView1.Columns.Add("Kode Customer", 100, HorizontalAlignment.Left) '3
            'ListView1.Columns.Add("Nama Customer", 200, HorizontalAlignment.Left) '4
            'ListView1.Columns.Add("Jumlah", 100, HorizontalAlignment.Right) '5  
            'ListView1.Columns.Add("No Faktur", 100, HorizontalAlignment.Left).DisplayIndex = 1 '6

            'ListView2.Columns.Add("No DO", 100, HorizontalAlignment.Left) '0
            'ListView2.Columns.Add("Tgl Transaksi", 80, HorizontalAlignment.Center) '1
            'ListView2.Columns.Add("Tgl Jth Tmpo", 0, HorizontalAlignment.Center) '2
            'ListView2.Columns.Add("Kode Customer", 100, HorizontalAlignment.Left) '3
            'ListView2.Columns.Add("Nama Customer", 150, HorizontalAlignment.Left) '4
            'ListView2.Columns.Add("Alamat", 130, HorizontalAlignment.Left) '5
            'ListView2.Columns.Add("Total-Retur-Disc Cash", 70, HorizontalAlignment.Right) '6
            'ListView2.Columns.Add("#", 0, HorizontalAlignment.Right) '7
            'ListView2.Columns.Add("Dibayar", 70, HorizontalAlignment.Right) '8
            'ListView2.Columns.Add("Sisa", 70, HorizontalAlignment.Right) '9
            'ListView2.Columns.Add("Retur", 70, HorizontalAlignment.Right) '10
            'ListView2.Columns.Add("Est. Disc. Cash", 100, HorizontalAlignment.Right) '11
            'ListView2.Columns.Add("Pertama Kirim", 100, HorizontalAlignment.Center) '12
            'ListView2.Columns.Add("Grand", 0, HorizontalAlignment.Right) '13
            'ListView2.Columns.Add("Lama Disc. Cash", 100, HorizontalAlignment.Center) '14
            'ListView2.Columns.Add("No Faktur", 100, HorizontalAlignment.Left).DisplayIndex = 1 '15
            'ListView2.View = View.Details

        End If
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub ComboBoxCb1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBoxCb1.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox1.Focus()
    End Sub

    Private Sub TxtFaktur_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtFaktur.TextChanged

    End Sub

    Private Sub ListView2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView2.SelectedIndexChanged
        Try
            If ListView2.Items.Count = 0 Then Exit Sub

            OpenConn()

            Dim Grand As Double = 0
            ListView3.Items.Clear()
            SQL = "Select a.kode_stock_owner, a.Kode_barang, b.nama, a.jumlah, b.satuan, "

            SQL = SQL & "(select isnull(sum(y.good_stock + y.bad_stock), 0) from retur_penjualan x, detail_r_penjualan y where "
            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_retur_jual = y.no_retur_jual and x.kode_perusahaan = a.kode_perusahaan and "
            SQL = SQL & "x.status is null and y.urut = a.no_urut and x.no_faktur_jual = a.no_faktur) as rtr "

            SQL = SQL & "from detail_penjualan a, barang b where "
            SQL = SQL & "a.kode_perusahaan = b.kode_Perusahaan and a.kode_barang = b.kode_barang and "
            SQL = SQL & "a.kode_stock_owner = b.kode_stock_owner and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & ListView2.FocusedItem.SubItems(15).Text & "' "
            SQL = SQL & "order by a.no_urut"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim GrandTotal As Double = 0
                    Dim Lvw As ListViewItem
                    Lvw = ListView3.Items.Add(Dr("kode_stock_owner"))
                    Lvw.SubItems.Add(Dr("kode_barang"))
                    Lvw.SubItems.Add(Dr("nama"))
                    Lvw.SubItems.Add(Format(Dr("jumlah"), "N0"))
                    Lvw.SubItems.Add(Format(Dr("rtr"), "N0"))
                    Lvw.SubItems.Add(Dr("satuan"))
                Loop
            End Using

            'Dim Grand As Double = 0
            'ListView3.Items.Clear()
            'SQL = "Select a.kode_stock_owner, a.Kode_barang, b.nama, a.jumlah, b.satuan, "

            'SQL = SQL & "(select isnull(sum(y.good_stock + y.bad_stock), 0) from retur_do x, detail_r_g y where "
            'SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_retur_jual = y.no_retur_jual and x.kode_perusahaan = a.kode_perusahaan and "
            'SQL = SQL & "x.status is null and y.urut = a.no_urut and x.no_faktur_jual = a.no_faktur) as rtr "

            'SQL = SQL & "isnull((select sum(x.ngrand) from retur_do x where "
            'SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
            'SQL = SQL & "x.no_do = a.no_do), 0) as pernah_ret_do, "

            'SQL = SQL & "isnull((select sum(x.grand) from retur_penjualan x where "
            'SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
            'SQL = SQL & "x.no_faktur_jual = a.no_faktur and x.no_retur_do is null), 0) as pernah_ret_penj, "

            'SQL = SQL & "from detail_do_new a, barang b, do_new c where "
            'SQL = SQL & "a.kode_perusahaan = b.kode_Perusahaan and b.kode_perusahaan = c.kode_Perusahaan and "
            'SQL = SQL & "a.kode_barang = b.kode_barang And "
            'SQL = SQL & "a.kode_stock_owner = b.kode_stock_owner and a.no_do = c.no do and "
            'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and c.no_do = '" & ListView2.FocusedItem.Text & "' "
            'SQL = SQL & "order by "
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Dim GrandTotal As Double = 0
            '        Dim Lvw As ListViewItem
            '        Lvw = ListView3.Items.Add(Dr("kode_stock_owner"))
            '        Lvw.SubItems.Add(Dr("kode_barang"))
            '        Lvw.SubItems.Add(Dr("nama"))
            '        Lvw.SubItems.Add(Format(Dr("jumlah"), "N0"))
            '        Lvw.SubItems.Add(Format(Dr("rtr"), "N0"))
            '        Lvw.SubItems.Add(Dr("satuan"))
            '    Loop
            'End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub














    '==============================================================================================================================
    '= UTILITY
    '==============================================================================================================================

    Protected Overrides Sub WndProc(ByRef m As Message)
        ' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
        If m.Msg = &HA3 Then
            Return  ' Abaikan pesan, sehingga form tidak maximize
        End If

        MyBase.WndProc(m)
    End Sub

End Class