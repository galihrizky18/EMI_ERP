Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class EMI_Pelunasan_Hutang_Bahan_Stock

    Dim JT As String
    Dim ArrCari, ArrMataUangRek As New ArrayList
    Dim arrCrByr, ArrAkunCB1, ArrAkunRek1, ArrMUA1, arrSupp, ArrRencana As New ArrayList

    Dim LvFak As String
    Dim LvTgl As String
    Dim LvTglJthTmp As String
    Dim LvKdCus As String
    Dim LvNmCus As String
    Dim LvJml As String
    Dim LvMataUang As String
    Dim LvKursLama As String
    Dim LvTotIdrKursLm As String
    Dim LvKursBr As String
    Dim LvTotIdrKursBr As String
    Dim LvTotSelisih As String
    Dim LvTotSelisihSebelum As String
    Dim varMataUang As String
    Dim kursUangBaru As Double
    Dim checkSisaHutang As String
    Dim checkSisaselisih As String

    Dim x_alamat, x_Kota, x_negara, x_telp As String

    Private Sub Validasi_Pemb_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

    End Sub

    Private Sub Validasi_Pembelian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        ListView2.Columns.Add("No Faktur", 100, HorizontalAlignment.Left) '0
        ListView2.Columns.Add("Tgl Transaksi", 80, HorizontalAlignment.Center) '1
        ListView2.Columns.Add("Tgl Jth Tmpo", 80, HorizontalAlignment.Center) '2
        ListView2.Columns.Add("Kode Supplier", 100, HorizontalAlignment.Left) '3
        ListView2.Columns.Add("Nama Supplier", 220, HorizontalAlignment.Left) '4
        ListView2.Columns.Add("Total", 115, HorizontalAlignment.Right) '5
        ListView2.Columns.Add("Dibayar", 115, HorizontalAlignment.Right) '6
        ListView2.Columns.Add("Sisa", 115, HorizontalAlignment.Right) '7
        ListView2.Columns.Add("Mata Uang", 80, HorizontalAlignment.Center) '8
        ListView2.Columns.Add("Kurs", 0, HorizontalAlignment.Right) '9
        ListView2.Columns.Add("Kurs Lama", 80, HorizontalAlignment.Right) '10
        ListView2.Columns.Add("JenisBiaya", 0, HorizontalAlignment.Right) '11
        ListView2.View = View.Details

        ListView1.Columns.Add("No Faktur", 100, HorizontalAlignment.Left) '0
        ListView1.Columns.Add("Tgl Transaksi", 80, HorizontalAlignment.Center) '1
        ListView1.Columns.Add("Tgl Jth Tmpo", 80, HorizontalAlignment.Center) '2
        ListView1.Columns.Add("Kode Supplier", 100, HorizontalAlignment.Left) '3
        ListView1.Columns.Add("Nama Supplier", 170, HorizontalAlignment.Left) '4
        ListView1.Columns.Add("Jumlah", 100, HorizontalAlignment.Center) '5
        ListView1.Columns.Add("Mata Uang", 100, HorizontalAlignment.Center) '6
        ListView1.Columns.Add("Kurs Lama", 100, HorizontalAlignment.Center) '7
        ListView1.Columns.Add("Total Kurs Lama", 100, HorizontalAlignment.Center) '8
        ListView1.Columns.Add("Kurs Baru", 100, HorizontalAlignment.Right) '9
        ListView1.Columns.Add("Total Kurs Baru", 100, HorizontalAlignment.Right) ' 10
        ListView1.Columns.Add("JenisBiaya", 0, HorizontalAlignment.Right) ' 11
        ListView1.View = View.Details


        Lv_RekTujuan.Columns.Add("No Rekening", 100, HorizontalAlignment.Left)
        Lv_RekTujuan.Columns.Add("Nama", 200, HorizontalAlignment.Left)
        Lv_RekTujuan.Columns.Add("Bank", 100, HorizontalAlignment.Left)
        Lv_RekTujuan.Location = New Point(1221, 631)
        Lv_RekTujuan.Visible = False

        TextBox6.Enabled = False
        Kosong()
        TxtFaktur.Focus()

    End Sub

    Private Sub Kosong()
        DateTimePicker1.Value = CDate(FMenuDev.ToolStripStatusLabel3.Text)
        DateTimePicker1.Enabled = True

        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBoxa.Text = ""
        Txt_Kurs.Text = ""
        TextBox1.Text = ""
        Txt_RekTujuan.Text = ""
        Txt_NoRekTujuan.Text = ""
        Cmb_BankTujuan.SelectedIndex = -1
        Cmb_BankTujuan.Text = ""
        txt_JenisBiaya.Text = ""

        btnSimpan.Text = "&Simpan"

        Txt_Kurs.Enabled = True
        Cmb_Bank.Enabled = True
        Cmb_Rek.Enabled = True

        Cmb_MataUang.Enabled = False
        TextBox1.Enabled = False

        ListView1.Items.Clear()
        ListView2.Items.Clear()
        Cmb_Rek.Items.Clear()

        Lv_RekTujuan.Items.Clear()

        ComboBox1.Items.Clear() : ArrCari.Clear()
        ComboBox1.Items.Add("--SEMUA--") : ArrCari.Add("SEMUA")
        ComboBox1.Items.Add("No Faktur") : ArrCari.Add("a.no_faktur")
        ComboBox1.Items.Add("Kode Supplier") : ArrCari.Add("a.kode_supplier")
        ComboBox1.Items.Add("Nama Supplier") : ArrCari.Add("e.nama")
        ComboBox1.SelectedIndex = 0

        Kosong_Bawah()

        Try

            OpenConn()

            Get_No_Faktur()

            'ComboBoxCb1.Items.Clear() : ArrCB1.Clear()
            'ComboBoxCb1.Items.Add("-- Cara Bayar --") : ArrCB1.Add("")

            'ComboBoxCb1.SelectedIndex = 0
            'SQL = "select kode_cb, keterangan from cara_bayar where kode_perusahaan = '" & KodePerusahaan & "' order by keterangan"
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        ComboBoxCb1.Items.Add(Dr("kode_cb")) : ArrCB1.Add(Dr("kode_cb"))
            '    Loop
            'End Using

            ComboBoxCb1.Items.Clear() : arrCrByr.Clear() : ArrAkunCB1.Clear()
            ComboBoxCb1.Items.Add("-- Cara Bayar --") : arrCrByr.Add("") : ArrAkunCB1.Add("")
            ComboBoxCb1.SelectedIndex = 0
            SQL = "select kode_cb, keterangan, kode_account_cb from cara_bayar where kode_perusahaan = '" & KodePerusahaan & "' order by keterangan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBoxCb1.Items.Add(Dr("keterangan")) : arrCrByr.Add(Dr("kode_cb")) : ArrAkunCB1.Add(Dr("kode_account_cb"))
                Loop
            End Using

            Cmb_Bank.Items.Clear()
            SQL = "SELECT kode_bank from bank where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' ORDER BY kode_bank"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Bank.Items.Add(dr("kode_bank"))
                Loop
            End Using

            Cmb_MataUang.Items.Clear()
            SQL = "select Kode_Mata_uang from Mata_Uang where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_Mata_Uang"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_MataUang.Items.Add(Dr("Kode_Mata_uang"))
                Loop
            End Using

            Cmb_BankTujuan.Items.Clear()
            SQL = "SELECT Kode_Bank FROM Bank_tujuan WHERE Kode_Perusahaan = '" & KodePerusahaan & "' ORDER BY Kode_Bank"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_BankTujuan.Items.Add(dr("Kode_Bank"))
                Loop
            End Using

            Cmb_Lokasi.Items.Clear()
            SQL = "select kode_stock_owner from Stock_Owner where kode_perusahaan = '" & KodePerusahaan & "'"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Lokasi.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using
            Cmb_Lokasi.SelectedIndex = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Hitung()
        pilihMataUang()

    End Sub

    Private Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvFak = ListView1.Items(No_Index).Text '0
        LvTgl = ListView1.Items(No_Index).SubItems(1).Text '1
        LvTglJthTmp = ListView1.Items(No_Index).SubItems(2).Text '2
        LvKdCus = ListView1.Items(No_Index).SubItems(3).Text
        LvNmCus = ListView1.Items(No_Index).SubItems(4).Text
        LvJml = ListView1.Items(No_Index).SubItems(5).Text
        LvMataUang = ListView1.Items(No_Index).SubItems(6).Text
        LvKursLama = ListView1.Items(No_Index).SubItems(7).Text
        LvTotIdrKursLm = ListView1.Items(No_Index).SubItems(8).Text
        LvKursBr = ListView1.Items(No_Index).SubItems(9).Text
        LvTotIdrKursBr = ListView1.Items(No_Index).SubItems(10).Text
        '    LvTotSelisih = ListView1.Items(No_Index).SubItems(11).Text
        '   LvTotSelisihSebelum = ListView1.Items(No_Index).SubItems(12).Text
    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        If Cmb_Bank.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Bank", "Pelunasan Pembelian Bahan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Bank.Focus() : Exit Sub
        ElseIf Cmb_Rek.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Rekening", "Pelunasan Pembelian Bahan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Rek.Focus() : Exit Sub
        ElseIf Cmb_MataUang.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Mata Uang", "Pelunasan Pembelian Bahan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_MataUang.Focus() : Exit Sub
        ElseIf Txt_Kurs.Text.Trim.Length = 0 Then
            MessageBox.Show("Kurs Tidak Boleh Kosong", "Pelunasan Pembelian Bahan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Kurs.Focus() : Exit Sub
        End If

        Cari("Tidak")
    End Sub

    Private Sub Cari(ByVal param As String)

        If param = "Tidak" Then
            If ComboBox1.SelectedIndex <> 0 Then
                If ComboBox1.SelectedIndex = -1 Then
                    MessageBox.Show("Paramater belum dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ComboBox1.Focus() : Exit Sub

                    If TextBox1.Text.Trim.Length = 0 Then
                        MessageBox.Show("Value belum dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        TextBox1.Focus() : Exit Sub
                    End If
                End If
            End If
        End If

        Try
            OpenConn()

            Dim lv As New ListViewItem


            ListView2.Items.Clear()
            'SQL = "select  a.No_Faktur,b.No_PO,a.tanggal + a.jam as tgl,a.Kode_Supplier, e.Nama as namasupplier, d.mata_uang,d.kurs "
            'SQL = SQL & ",isnull((sum(b.jumlah * c.harga)),0) as grand, "
            'SQL = SQL & "isnull((select sum(y.byr) from EMI_Pelunasan_Pembelian x, EMI_Pelunasan_Pembelian_Detail y where  "
            'SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_val = y.no_val and "
            'SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
            'SQL = SQL & "y.no_faktur = a.no_faktur), 0) as pernah_val, d.tgl_jatuh_tempo "
            'SQL = SQL & "From EMI_Pembelian_Loading a, EMI_Pembelian_Loading_Detail b, EMI_Pembelian_PO_Detail c, EMI_Pembelian_PO d, Suppliers e "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur  "
            'SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Urut_PO = c.No_Urut "
            'SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur "
            'SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.Kode_Supplier = e.Kode_Supplier "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Status is null and a.flag_pembelian is null  "

            'If param = "Tidak" Then
            '    If ComboBox1.SelectedIndex <> 0 Then
            '        SQL = SQL & " and "
            '        SQL = SQL & "" & ArrCari.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox1.Text.Trim & "%'  "
            '    End If
            'End If
            'SQL = SQL & " group by a.No_Faktur,b.No_PO,a.tanggal + a.jam ,a.Kode_Supplier, e.Nama,d.mata_uang,d.kurs, a.Kode_Perusahaan, d.tgl_jatuh_tempo "
            'SQL = SQL & "order by tgl"

            SQL = "select  a.No_Faktur, b.No_PO, a.tanggal + a.jam as tgl,a.Kode_Supplier, e.Nama as namasupplier, d.mata_uang, d.kurs , "
            SQL = SQL & "ISNULL(CASE WHEN b.Flag_Masuk_Hutang = 'Y' THEN SUM(b.Jumlah_Masuk * c.Harga_Barang) ELSE SUM(b.Jumlah_Barang * c.Harga_Barang) END, 0 ) AS grand,"
            SQL = SQL & "isnull((select sum(y.byr) from EMI_Pelunasan_Pembelian x, EMI_Pelunasan_Pembelian_Detail y where  x.kode_perusahaan = y.kode_perusahaan "
            SQL = SQL & "and x.no_val = y.no_val and x.kode_perusahaan = a.kode_perusahaan and x.status is null and y.no_faktur = a.no_faktur ), 0) as pernah_val, "
            SQL = SQL & "d.tgl_jatuh_tempo, '1' as kurs_lama, "

            SQL = SQL & "ISNULL(( "
            SQL = SQL & "select x.Kode_Kategori_Suppliers from Suppliers z, Suppliers_Kategori x where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_Supplier = a.Kode_Supplier "
            SQL = SQL & "and z.Kode_Perusahaan = x.Kode_Perusahaan and z.ID_Kategori_Suppliers = x.ID_Kategori_Suppliers "
            SQL = SQL & "), '-') as JenisBiaya "

            SQL = SQL & "From EMI_Pembelian_Loading a, EMI_Pembelian_Loading_Detail b, EMI_Pembelian_PO_Detail c, EMI_Pembelian_PO d, Suppliers e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur  and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Urut_PO = c.No_Urut "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and a.Kode_Perusahaan = e.Kode_Perusahaan  "
            SQL = SQL & "and a.Kode_Supplier = e.Kode_Supplier "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.flag_pembelian='Y' "
            SQL = SQL & "and d.Mata_Uang = '" & Cmb_MataUang.Text & "' "
            SQL = SQL & "and d.Flag_Import is null "

            If param = "Tidak" Then
                If ComboBox1.SelectedIndex <> 0 Then
                    SQL = SQL & " and "
                    SQL = SQL & "" & ArrCari.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox1.Text.Trim & "%'  "
                End If
            End If

            SQL = SQL & "group by a.No_Faktur,b.No_PO,a.tanggal + a.jam ,a.Kode_Supplier, e.Nama,d.mata_uang,d.kurs, a.Kode_Perusahaan, d.tgl_jatuh_tempo,  b.Flag_Masuk_Hutang "

            SQL = SQL & "union all "

            SQL = SQL & "select  a.No_Faktur, b.No_PO, a.tanggal + a.jam as tgl,a.Kode_Supplier, e.Nama as namasupplier, d.mata_uang, d.kurs , "
            SQL = SQL & "ISNULL( CASE WHEN b.Flag_Masuk_Hutang = 'Y' THEN SUM(b.Jumlah_Masuk * c.Harga_Barang) ELSE SUM(b.Jumlah_Barang * c.Harga_Barang) END, 0 ) AS grand, "
            SQL = SQL & "isnull((select sum(y.byr) "
            SQL = SQL & "from EMI_Pelunasan_Pembelian x, EMI_Pelunasan_Pembelian_Detail y "
            SQL = SQL & "where  x.kode_perusahaan = y.kode_perusahaan and x.no_val = y.no_val and x.kode_perusahaan = a.kode_perusahaan and x.status is null and y.no_faktur = a.no_faktur "
            SQL = SQL & "), 0) as pernah_val, "
            SQL = SQL & "d.tgl_jatuh_tempo, "
            SQL = SQL & "isnull((select top (1) z.nilai from Kurs_HPP_import z where z.Kode_Perusahaan  = a.Kode_Perusahaan and a.No_Fak_HPP = z.No_Faktur and z.Mata_Uang = d.Mata_Uang "
            SQL = SQL & "and z.Jenis = 'UTAMA' and a.Flag_Import_HPP = 'Y' "
            SQL = SQL & "),0) as kurs_lama, "

            SQL = SQL & "ISNULL(( "
            SQL = SQL & "select x.Kode_Kategori_Suppliers from Suppliers z, Suppliers_Kategori x where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_Supplier = a.Kode_Supplier "
            SQL = SQL & "and z.Kode_Perusahaan = x.Kode_Perusahaan and z.ID_Kategori_Suppliers = x.ID_Kategori_Suppliers "
            SQL = SQL & "), '-') as JenisBiaya "

            SQL = SQL & "From EMI_Pembelian_Loading a, EMI_Pembelian_Loading_Detail b, EMI_Pembelian_PO_Detail c, EMI_Pembelian_PO d, Suppliers e  "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur  and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Urut_PO = c.No_Urut "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and a.Kode_Perusahaan = e.Kode_Perusahaan  "
            SQL = SQL & "and a.Kode_Supplier = e.Kode_Supplier "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.flag_pembelian='Y' "
            SQL = SQL & "and d.Mata_Uang =  '" & Cmb_MataUang.Text & "' "
            SQL = SQL & "and d.Flag_Import = 'Y' "

            If param = "Tidak" Then
                If ComboBox1.SelectedIndex <> 0 Then
                    SQL = SQL & " and "
                    SQL = SQL & "" & ArrCari.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox1.Text.Trim & "%'  "
                End If
            End If

            SQL = SQL & "group by a.No_Faktur,b.No_PO,a.tanggal + a.jam ,a.Kode_Supplier, e.Nama,d.mata_uang,d.kurs, a.Kode_Perusahaan, d.tgl_jatuh_tempo,  b.Flag_Masuk_Hutang, a.No_Fak_HPP, a.Flag_Import_HPP "
            SQL = SQL & "order by tgl  "

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = ListView2.Items.Add(Dr("no_faktur")) '0
                    lv.SubItems.Add(Format(Dr("tgl"), "dd MMM yyyy")) '1

                    If General_Class.CekNULL(Dr("tgl_Jatuh_tempo")) = "" Then
                        lv.SubItems.Add("-") '2
                    Else
                        lv.SubItems.Add(Format(Dr("tgl_jatuh_tempo"), "dd MMM yyyy")) '2
                    End If

                    lv.SubItems.Add(Dr("kode_supplier")) '3
                    lv.SubItems.Add(Dr("namasupplier")) '4
                    'lv.SubItems.Add(Dr("alamat"))
                    Dim x_tot As Double = Dr("grand")
                    lv.SubItems.Add(Format(x_tot, "N2")) '5
                    lv.SubItems.Add(Format(Dr("pernah_val"), "N2")) '6
                    lv.SubItems.Add(Format(x_tot - Dr("pernah_val"), "N2")) '7
                    lv.SubItems.Add(Dr("Mata_uang")) '8
                    lv.SubItems.Add(Format(Dr("kurs"), "N2")) '9
                    lv.SubItems.Add(Format(Dr("kurs_lama"), "N2")) '10
                    lv.SubItems.Add(Dr("JenisBiaya")) '11
                    'lv.SubItems.Add(Format(Dr("sisa_selisih"), "N0"))
                Loop
            End Using

            Cmb_MataUang.Enabled = False
            Txt_Kurs.Enabled = False
            Cmb_Bank.Enabled = False
            Cmb_Rek.Enabled = False

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
        Dim kursIdrLama As Double = 0
        Dim kursIdrBaru As Double = 0
        'Dim selisihKursLamaBaru As Double = 0
        'Dim ttlselisihsebelum As Double = 0
        For i As Integer = 0 To ListView1.Items.Count - 1
            Get_Isi_Listview(i)

            Grand = Grand + Val(HilangkanTanda(LvJml))
            kursIdrLama = kursIdrLama + Val(HilangkanTanda(LvTotIdrKursLm))
            kursIdrBaru = kursIdrBaru + Val(HilangkanTanda(LvTotIdrKursBr))
            ' selisihKursLamaBaru = selisihKursLamaBaru + Val(HilangkanTanda(LvTotSelisih))
            ' ttlselisihsebelum = ttlselisihsebelum + Val(HilangkanTanda(LvTotSelisihSebelum))
        Next

        ' TextBoxttlselisih.Text = (Format(ttlselisihsebelum, "N2"))
        totalIdrKursLama.Text = (Format(kursIdrLama, "N2"))
        totalIdrKursBaru.Text = (Format(kursIdrBaru, "N2"))
        'totalSelisih.Text = (Format(selisihKursLamaBaru, "N2"))
        TextBoxa.Text = (Format(Grand, "N2"))
    End Sub

    Private Sub Get_No_Faktur()
        TxtFaktur.Text = fValPemb & Format(DateTimePicker1.Value, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("EMI_Pelunasan_Pembelian", "no_val", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_val, 1, " & Len(fValPemb) + 4 & ")", fValPemb & Format(DateTimePicker1.Value, "MMyy"))
    End Sub

    Private Sub Kosong_Bawah()
        TextBox9.Text = ""
        TextBox3.Text = ""
        'TextBox4.Text = ""
        TextBox7.Text = ""
        TextBox10.Text = ""
        TextBox11.Text = ""
        TextBox12.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        'TextBoxselisih.Text = ""
        TextBox6.Enabled = False
    End Sub

    Private Sub Validasi_Pembelian_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Label1.Size = New Point(Me.Width, 33)
    End Sub

    Private Sub ListView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView2.DoubleClick
        For i As Integer = 0 To ListView1.Items.Count - 1
            If ListView1.Items(i).Text.Trim.ToUpper = ListView2.FocusedItem.Text.Trim.ToUpper Then
                MessageBox.Show("Faktur ini sudah dimasukkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If ListView1.Items(i).SubItems(11).Text.Trim.ToUpper <> ListView2.FocusedItem.SubItems(11).Text.Trim.ToUpper Then
                MessageBox.Show("Jenis Biaya tidak boleh berbeda") : Exit Sub
                Exit Sub
            End If
        Next

        'ListView1.Columns.Add("No Faktur", 100, HorizontalAlignment.Left)
        'ListView1.Columns.Add("Tgl Transaksi", 80, HorizontalAlignment.Center)
        'ListView1.Columns.Add("Tgl Jth Tmpo", 80, HorizontalAlignment.Center)
        'ListView1.Columns.Add("Customer", 300, HorizontalAlignment.Left)
        'ListView1.Columns.Add("Jumlah", 100, HorizontalAlignment.Right)
        'ListView1.View = View.Details

        'ListView2.Columns.Add("No Faktur", 100, HorizontalAlignment.Left)
        'ListView2.Columns.Add("Tgl Transaksi", 80, HorizontalAlignment.Center)
        'ListView2.Columns.Add("Tgl Jth Tmpo", 80, HorizontalAlignment.Center)
        'ListView2.Columns.Add("Customer", 200, HorizontalAlignment.Left)
        'ListView2.Columns.Add("Alamat", 200, HorizontalAlignment.Left)
        'ListView2.Columns.Add("Total", 90, HorizontalAlignment.Right)
        'ListView2.Columns.Add("Dibayar", 90, HorizontalAlignment.Right)
        'ListView2.Columns.Add("Sisa", 90, HorizontalAlignment.Right)

        If Cmb_MataUang.Text <> ListView2.FocusedItem.SubItems(8).Text Then
            MessageBox.Show("Mata uang tidak boleh berbeda") : Exit Sub
        End If


        TextBox9.Text = ListView2.FocusedItem.Text
        TextBox3.Text = ListView2.FocusedItem.SubItems(1).Text
        TextBox4.Text = ListView2.FocusedItem.SubItems(2).Text
        TextBox7.Text = ListView2.FocusedItem.SubItems(3).Text
        TextBox5.Text = ListView2.FocusedItem.SubItems(4).Text
        checkSisaHutang = HilangkanTanda(ListView2.FocusedItem.SubItems(7).Text)
        TextBox10.Text = ListView2.FocusedItem.SubItems(8).Text
        TextBox6.Text = HilangkanTanda(ListView2.FocusedItem.SubItems(7).Text)
        TextBox12.Text = HilangkanTanda(Txt_Kurs.Text)
        TextBox11.Text = HilangkanTanda(ListView2.FocusedItem.SubItems(10).Text)
        'TextBoxselisih.Text = HilangkanTanda(ListView2.FocusedItem.SubItems(10).Text)

        txt_JenisBiaya.Text = ListView2.FocusedItem.SubItems(11).Text

        'checkSisaselisih = HilangkanTanda(ListView2.FocusedItem.SubItems(10).Text)
        TextBox6.Enabled = True
        TextBox6.Focus()
    End Sub

    Private Sub pilihMataUang()
        Try
            OpenConn()

            cbxMataUang.Items.Clear()
            cbxMataUang.Items.Add("--Pilih Mata Uang--")
            cbxMataUang.SelectedIndex = 0

            SQL = "select distinct d.Mata_Uang from EMI_Pembelian_Loading a, EMI_Pembelian_Loading_Detail b, EMI_Pembelian_PO_Detail c, EMI_Pembelian_PO d  "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur  "
            SQL = SQL & "and  b.Kode_Perusahaan = c.Kode_Perusahaan and b.Urut_PO = c.No_Urut  "
            SQL = SQL & "and  c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    cbxMataUang.Items.Add(Dr("mata_uang"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        'cbxMataUang.Items.Add("USD")
        'cbxMataUang.Items.Add("CNY")
        cbxMataUang.Enabled = True
        TextBox8.Text = ""
        TextBox8.Enabled = True
        TextBox1.Enabled = False
        btnCari.Enabled = False

    End Sub


    Private Sub BtnPO_Refresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Kosong()
        DateTimePicker1.Focus()
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
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
            'ElseIf ComboBoxCb1.SelectedIndex = -1 Or ComboBoxCb1.SelectedIndex = 0 Then
            '    MessageBox.Show("Cara bayar harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    ComboBoxCb1.Focus()
            '    Exit Sub
        ElseIf Txt_RekTujuan.Text.Trim.Length = 0 Then
            MessageBox.Show("Tujuan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_RekTujuan.Focus()
            Exit Sub
        ElseIf Txt_NoRekTujuan.Text.Trim.Length = 0 Then
            MessageBox.Show("Tujuan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_RekTujuan.Focus()
            Exit Sub
        ElseIf Cmb_BankTujuan.Text.Trim.Length = 0 Then
            MessageBox.Show("Tujuan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_RekTujuan.Focus()
            Exit Sub
        End If

        If btnSimpan.Text = "&Simpan" Then
            Dim tny As String = MessageBox.Show("Yakin akan disimpan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If tny = vbNo Then Exit Sub

            GetTime()
            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                Get_No_Faktur()

                '=====================
                '=     CEK TUJUAN    =
                '=====================
                SQL = "select no_rekening, nama, kode_bank, Alamat_Penerima, Kota_Penerima, "
                SQL = SQL & "Negara_Penerima, Telp_Penerima from rekening_tujuan where kode_perusahaan = '" & KodePerusahaan & "' and nama = '" & Txt_RekTujuan.Text & "' order by nama"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Nama Tujuan Tidak ditemukan", "Pelunasan Biaya", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_RekTujuan.Focus()
                        Exit Sub
                    End If

                End Using

                Dim Kode_Voucher As String = GetLastNumberJurnal(Format(DateTimePicker1.Value, "yyyyMM"), fJU & fValPemb, KodePerusahaan)

                Dim pagenumber As Integer = 1

                SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                SQL = SQL & "'" & Kode_Voucher & "', "
                SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                SQL = SQL & "'" & KodeProyek & "', 'Pelunasan hutang " & TxtFaktur.Text.Trim & "', '', "
                SQL = SQL & "'-', '" & UserID & "')"
                ExecuteTrans(SQL)

                Dim total_deposit As Double = Val(HilangkanTanda(TextBoxa.Text))

                SQL = "insert into emi_pelunasan_pembelian(kode_perusahaan, no_val, tanggal, jam, "
                SQL = SQL & "keterangan, uservalidasi, cara_bayar, total,total_idr_kurs_lama, total_idr_kurs_baru, Kode_Bank, No_Rek, "
                SQL = SQL & "Jenis, Kode_Bank_Tujuan, No_Rek_Tujuan, Nama_Penerima, Alamat_Penerima, Kota_Penerima, Negara_Penerima, Telp_Penerima, Lokasi) "
                SQL = SQL & "values('" & KodePerusahaan & "',"
                SQL = SQL & "'" & TxtFaktur.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                SQL = SQL & "'" & TextBox2.Text.Trim & "', '" & UserID & "', "
                SQL = SQL & "'" & arrCrByr.Item(ComboBoxCb1.SelectedIndex) & "', "

                SQL = SQL & "" & HilangkanTanda(TextBoxa.Text) & ","
                SQL = SQL & "" & HilangkanTanda(totalIdrKursLama.Text) & ","
                SQL = SQL & "" & HilangkanTanda(totalIdrKursBaru.Text) & ", "
                SQL = SQL & "'" & Cmb_Bank.Text & "', '" & Cmb_Rek.Text & "', "

                SQL = SQL & "'" & txt_JenisBiaya.Text & "', '" & Cmb_BankTujuan.Text & "', '" & Txt_NoRekTujuan.Text & "', '" & Txt_RekTujuan.Text & "', "
                SQL = SQL & "'" & x_alamat & "', '" & x_Kota & "', '" & x_negara & "', '" & x_telp & "', '" & Cmb_Lokasi.Text & "' "
                SQL = SQL & ")"
                ExecuteTrans(SQL)

                For i As Integer = 0 To ListView1.Items.Count - 1
                    Get_Isi_Listview(i)

                    Dim selisihKurs As Double = Val(HilangkanTanda(LvKursBr)) - Val(HilangkanTanda(LvKursLama))

                    SQL = "insert into emi_pelunasan_pembelian_detail(kode_perusahaan, no_val, no_faktur, byr,"
                    SQL = SQL & " mata_uang, kurs_lama, total_kurs_lama, kurs_baru, total_kurs_baru, Selisih_Total_Kurs)"
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
                    SQL = SQL & "'" & LvFak.Trim & "', " & HilangkanTanda(LvJml) & ","
                    SQL = SQL & " '" & LvMataUang & "', " & HilangkanTanda(LvKursLama) & ", " & HilangkanTanda(LvTotIdrKursLm) & ", "
                    SQL = SQL & " " & HilangkanTanda(LvKursBr) & " , " & HilangkanTanda(LvTotIdrKursBr) & ", '" & selisihKurs & "') "
                    '  SQL = SQL & " " & LvTotSelisih & ", '" & HilangkanTanda(LvTotSelisihSebelum) & "')  "
                    ExecuteTrans(SQL)

                    Dim cekSisa As Double = 0

                    SQL = "select a.No_Faktur, isnull((sum(b.jumlah * c.harga)),0) as grand, "

                    SQL = SQL & "isnull((select sum(y.byr) from EMI_Pelunasan_Pembelian x, EMI_Pelunasan_Pembelian_Detail y where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_val = y.no_val and x.kode_perusahaan = a.kode_perusahaan "
                    SQL = SQL & "and x.status is null and y.no_faktur = a.no_faktur), 0) as pernah_val "

                    SQL = SQL & "From EMI_Pembelian_Loading a, EMI_Pembelian_Loading_Detail b,  "
                    SQL = SQL & "EMI_Pembelian_PO_Detail c, EMI_Pembelian_PO d, Suppliers e where a.Kode_Perusahaan = b.Kode_Perusahaan "
                    SQL = SQL & "and a.No_Faktur = b.No_Faktur  and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Urut_PO = c.No_Urut and "
                    SQL = SQL & "c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and a.Kode_Perusahaan = e.Kode_Perusahaan  "
                    SQL = SQL & "and a.Kode_Supplier = e.Kode_Supplier and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Status is null and a.flag_pembelian='Y'  "
                    SQL = SQL & "and a.No_Faktur= '" & LvFak.Trim & "' "
                    SQL = SQL & "group by  a.No_Faktur, a.Kode_Perusahaan   "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then

                            cekSisa = Val(Dr("grand")) - Val(Dr("pernah_val"))
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi kesalahan, no faktur pembelian tidak ditemukan!", Judul, MessageBoxButtons.OK)
                        End If
                    End Using

                    If cekSisa = 0 Then
                        SQL = "update emi_pembelian_loading set flag_pembelian  = 'Y' where "
                        SQL = SQL & "no_faktur = '" & LvFak & "' and kode_perusahaan = '" & KodePerusahaan & "' "
                        ExecuteTrans(SQL)
                    End If

                    Dim akun_hutang_sup As String = ""
                    Dim akun_selisih As String = ""

                    SQL = "select Hutang_Supplier, akun_selisih_PO "
                    SQL = SQL & "from stock_owner "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Lokasi & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            akun_hutang_sup = Dr("Hutang_Supplier")
                            akun_selisih = Dr("akun_selisih_PO")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_sup & "' and debit <> 0 "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            'update

                            SQL = "update detail_jurnal set debit = debit+ " & HilangkanTanda(LvTotIdrKursLm) & " where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang_sup & "' and debit <> 0 "
                            ExecuteTrans(SQL)
                        Else
                            Dr.Close()
                            'insert

                            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(akun_hutang_sup, 1),
                                          Strings.Mid(akun_hutang_sup, 2, 1),
                                          Strings.Mid(Ganti(akun_hutang_sup), 3),
                                          KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, HilangkanTanda(HilangkanTanda(LvTotIdrKursLm)), "0", pagenumber, "TSSS")
                            ExecuteTrans(SQL)
                            pagenumber = pagenumber + 1

                        End If
                    End Using

                    Dim selisih As Double = Val(HilangkanTanda(LvTotIdrKursBr)) - Val(HilangkanTanda(LvTotIdrKursLm))
                    If selisih <> 0 Then
                        If selisih > 0 Then
                            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_selisih & "' and debit <> 0"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Dr.Close()
                                    'update 

                                    SQL = "update detail_jurnal set debit = debit+ " & HilangkanTanda(selisih) & " where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_selisih & "' and debit <> 0"
                                    ExecuteTrans(SQL)
                                Else
                                    Dr.Close()
                                    'insert
                                    pagenumber += 1
                                    SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(akun_selisih, 1),
                                              Strings.Mid(akun_selisih, 2, 1),
                                              Strings.Mid(Ganti(akun_selisih), 3),
                                              KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, HilangkanTanda(selisih), "0", pagenumber, Ket_Lokasi_HO)
                                    ExecuteTrans(SQL)
                                End If
                            End Using
                        Else
                            SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_selisih & "' and kredit <> 0"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Dr.Close()
                                    'update 

                                    SQL = "update detail_jurnal set kredit = kredit+ " & Math.Abs(Val(HilangkanTanda(selisih))) & " where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_selisih & "' and kredit <> 0"
                                    ExecuteTrans(SQL)
                                Else
                                    Dr.Close()
                                    'insert
                                    pagenumber += 1
                                    SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(akun_selisih, 1),
                                              Strings.Mid(akun_selisih, 2, 1),
                                              Strings.Mid(Ganti(akun_selisih), 3),
    KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, "0", Math.Abs(Val(HilangkanTanda(selisih))), pagenumber, Ket_Lokasi_HO)
                                    ExecuteTrans(SQL)
                                End If
                            End Using
                        End If
                    End If

                Next

                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & ArrAkunRek1.Item(Cmb_Rek.SelectedIndex) & "' and kredit <> 0 "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update

                        SQL = "update detail_jurnal set kredit = kredit+ " & HilangkanTanda(totalIdrKursBaru.Text) & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & ArrAkunRek1.Item(Cmb_Rek.SelectedIndex) & "' and kredit <> 0 "
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert

                        SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(ArrAkunRek1.Item(Cmb_Rek.SelectedIndex), 1),
                      Strings.Mid(ArrAkunRek1.Item(Cmb_Rek.SelectedIndex), 2, 1),
                      Strings.Mid(Ganti(ArrAkunRek1.Item(Cmb_Rek.SelectedIndex)), 3),
                      KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, "0", HilangkanTanda(totalIdrKursBaru.Text), pagenumber, "TSSS")
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                    End If
                End Using


                SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "'"
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



                MessageBox.Show("Data berhasil disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Cmd.Transaction.Commit()

                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        End If

        'Dim TanyaCetak As String = MessageBox.Show("Mau dicetak?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        'If TanyaCetak = vbYes Then
        '    cetak()
        'End If

        Dim TanyaCetak As String = MessageBox.Show("Mau dicetak?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If TanyaCetak = vbYes Then
            cetak()
        End If

        Kosong()
        DateTimePicker1.Focus()
    End Sub

    Private Sub cetak()
        Try

            OpenConn()

            Dim SF As String

            SQL = "select Kode_Perusahaan from View_Laporan_Pelunasan_Bahan "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Val = '" & TxtFaktur.Text & "' and lokasi = '" & Cmb_Lokasi.Text & "'"

            SF = "{View_Laporan_Pelunasan_Bahan.Kode_Perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {View_Laporan_Pelunasan_Bahan.No_Val} = '" & TxtFaktur.Text & "' "
            SF = SF & "and {View_Laporan_Pelunasan_Bahan.lokasi} = '" & Cmb_Lokasi.Text & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    Dim CrDoc As New Laporan_Pelunasan_Hutang_Bahan    'Nama file CR

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.RecordSelectionFormula = SF
                    With A_Place_For_Printing2
                        .Text = "Pelunasan Bahan"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                        .Refresh()
                        .Show()
                    End With


                    '======================================================

                    'Dim CrDoc As New Faktur_Pelunasan_Pemb    'Nama file CR
                    'CrDoc.SetDataSource(Ds)
                    'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    'CrDoc.PrintOptions.PrinterName = PrinterName
                    'CrDoc.RecordSelectionFormula = "{val_pemb.Kode_Perusahaan} = '" & KodePerusahaan & "' and {val_pemb.no_val} = '" & TxtFaktur.Text.Trim & "'"

                    'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    'doctoprint.PrinterSettings.PrinterName = PrinterName
                    'Dim rawKind As Integer
                    'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    'For i = doctoprint.PrinterSettings.PaperSizes.Count - 1 To 0 Step -1
                    '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Faktur" Then
                    '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                    '        CrDoc.PrintOptions.PaperSize = rawKind
                    '        Exit For
                    '    End If
                    'Next

                    'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                    'CrDoc.PrintToPrinter(1, False, 1, 99)
                End If
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub cbxMataUang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxMataUang.SelectedIndexChanged
        If cbxMataUang.SelectedIndex <> 0 Then
            TextBox8.Visible = True
            TextBox8.Focus()
        Else
            TextBox8.Visible = False
        End If
    End Sub

    Private Sub cbxMataUang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbxMataUang.KeyPress
        If cbxMataUang.SelectedIndex <> 0 Then
            TextBox8.Visible = True
            TextBox8.Focus()
        Else
            TextBox8.Visible = False
        End If

    End Sub

    Private Sub TextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress
        If e.KeyChar = Chr(13) Then
            If (TextBox6.Text.Trim.Length = 0 And TextBox9.Text.Trim.Length = 0) Then
                MessageBox.Show("Silahkan pilih faktur terlebih dahulu")
                TextBox6.Text = "" : Exit Sub
            ElseIf TextBox9.Text.Trim.Length = 0 Then
                MessageBox.Show("Silahkan pilih faktur terlebih dahulu")
                TextBox6.Text = "" : Exit Sub
            End If

            If (checkSisaHutang < Val(TextBox6.Text)) Then
                MessageBox.Show("Pembayaran tidak boleh melebihi sisa hutang") : Exit Sub
            End If

            'If Math.Abs(Val(checkSisaselisih)) < Math.Abs(Val(TextBoxselisih.Text)) Then
            '    MessageBox.Show("Selisih tidak boleh melebihi sisa Selisih") : Exit Sub
            'End If

            For i As Integer = 0 To ListView1.Items.Count - 1
                If ListView1.Items(i).Text.Trim.ToUpper = TextBox9.Text.Trim.ToUpper Then
                    MessageBox.Show("Faktur ini sudah dimasukkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Next
            Dim totalKursLama As Double
            Dim totalKursBaru As Double
            '  Dim selisihTotalKurs As Double

            Dim JenisBiaya As String = txt_JenisBiaya.Text


            totalKursLama = Format(Val(TextBox11.Text), "N5") * Format(Val(TextBox6.Text), "N2")
            totalKursBaru = Format(Val(TextBox12.Text), "N5") * Format(Val(TextBox6.Text), "N2")
            'selisihTotalKurs = Format(totalKursBaru, "N2") - Format(totalKursLama, "N2")
            Dim lv As New ListViewItem
            lv = ListView1.Items.Add(TextBox9.Text) '0
            lv.SubItems.Add(TextBox3.Text) '1
            lv.SubItems.Add(TextBox4.Text) '2
            lv.SubItems.Add(TextBox7.Text) '3
            lv.SubItems.Add(TextBox5.Text) '4
            lv.SubItems.Add(Format(Val(TextBox6.Text), "N2")) '5
            lv.SubItems.Add(TextBox10.Text) '6
            lv.SubItems.Add(Format(Val(TextBox11.Text), "N2")) '7
            lv.SubItems.Add(Format(totalKursLama, "N2")) '8
            lv.SubItems.Add(Format(Val(TextBox12.Text), "N2")) '9
            lv.SubItems.Add(Format(totalKursBaru, "N2")) '10
            lv.SubItems.Add(JenisBiaya) '11

            '  lv.SubItems.Add(selisihTotalKurs)
            'lv.SubItems.Add(Format(Val(TextBoxselisih.Text), "N2"))

            Hitung()
            Kosong_Bawah()
            ListView2.Focus()
        End If
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = -1 Then Exit Sub
        TextBox1.Text = ""
        TextBox1.Enabled = True
    End Sub

    Private Sub Txt_Kurs_Leave(sender As Object, e As EventArgs) Handles Txt_Kurs.Leave
        If Txt_Kurs.Text.Trim.Length = 0 Then Exit Sub

        Txt_Kurs.Text = Format(Val(HilangkanTanda(Txt_Kurs.Text)), "N2")
    End Sub


    Private Sub Txt_Kurs_Enter(sender As Object, e As EventArgs) Handles Txt_Kurs.Enter
        If Txt_Kurs.Text.Trim.Length = 0 Then Exit Sub

        Txt_Kurs.Text = HilangkanTanda(Txt_Kurs.Text)
    End Sub

    Private Sub Txt_RekTujuan_TextChanged(sender As Object, e As EventArgs) Handles Txt_RekTujuan.TextChanged
        If Txt_RekTujuan.Text.Trim.Length = 0 Then
            Lv_RekTujuan.Visible = False
            Lv_RekTujuan.Location = New Point(1221, 621)
            Exit Sub
        Else
            Lv_RekTujuan.Location = New Point(137, 621)
            Lv_RekTujuan.Visible = True
        End If


        Try
            OpenConn()

            Lv_RekTujuan.Items.Clear()
            SQL = "select no_rekening, nama, kode_bank, Alamat_Penerima, Kota_Penerima, "
            SQL = SQL & "Negara_Penerima, Telp_Penerima from rekening_tujuan where kode_perusahaan = '" & KodePerusahaan & "' and nama like '%" & Txt_RekTujuan.Text & "%' order by nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem
                    lv = Lv_RekTujuan.Items.Add(Dr("no_rekening"))
                    lv.SubItems.Add(Dr("nama"))
                    lv.SubItems.Add(Dr("kode_bank"))
                    lv.SubItems.Add(Dr("Alamat_Penerima"))
                    lv.SubItems.Add(Dr("Kota_Penerima"))
                    lv.SubItems.Add(Dr("Negara_Penerima"))
                    lv.SubItems.Add(Dr("Telp_Penerima"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Lv_RekTujuan_DoubleClick(sender As Object, e As EventArgs) Handles Lv_RekTujuan.DoubleClick
        If Lv_RekTujuan.Items.Count = 0 Then Exit Sub

        Dim kode As String = Lv_RekTujuan.FocusedItem.Text
        Dim nama As String = Lv_RekTujuan.FocusedItem.SubItems(1).Text
        Dim kode_bank As String = Lv_RekTujuan.FocusedItem.SubItems(2).Text
        Dim alamat As String = Lv_RekTujuan.FocusedItem.SubItems(3).Text
        Dim kota As String = Lv_RekTujuan.FocusedItem.SubItems(4).Text
        Dim negara As String = Lv_RekTujuan.FocusedItem.SubItems(5).Text
        Dim telp As String = Lv_RekTujuan.FocusedItem.SubItems(6).Text

        Txt_RekTujuan.Text = nama
        Txt_NoRekTujuan.Text = kode
        Cmb_BankTujuan.Text = kode_bank

        x_alamat = alamat
        x_Kota = kota
        x_negara = negara
        x_telp = telp

        Lv_RekTujuan.Items.Clear()
        Lv_RekTujuan.Location = New Point(1221, 631)
        Lv_RekTujuan.Visible = False

    End Sub

    Private Sub Txt_Kurs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kurs.KeyPress

        If e.KeyChar = Chr(13) Then
            btnCari.Enabled = True
        End If

        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        TextBox9.Text = ListView1.FocusedItem.Text
        TextBox3.Text = ListView1.FocusedItem.SubItems(1).Text
        TextBox4.Text = ListView1.FocusedItem.SubItems(2).Text
        TextBox7.Text = ListView1.FocusedItem.SubItems(3).Text

        TextBox5.Text = ListView1.FocusedItem.SubItems(4).Text
        TextBox10.Text = ListView1.FocusedItem.SubItems(6).Text
        TextBox11.Text = HilangkanTanda(ListView1.FocusedItem.SubItems(7).Text)
        TextBox12.Text = HilangkanTanda(ListView1.FocusedItem.SubItems(9).Text)
        TextBox6.Text = HilangkanTanda(ListView1.FocusedItem.SubItems(5).Text)
        'TextBoxselisih.Text = HilangkanTanda(ListView1.FocusedItem.SubItems(12).Text)
        ListView1.FocusedItem.Remove()
        TextBox6.Enabled = True
        TextBox6.Focus()
        Hitung()
    End Sub

    Private Sub Cmb_Rek_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Rek.SelectedIndexChanged
        If Cmb_Rek.SelectedIndex = -1 Then Exit Sub

        Cmb_MataUang.SelectedItem = ArrMataUangRek(Cmb_Rek.SelectedIndex)

        Cmb_MataUang.Enabled = False
    End Sub

    Private Sub Cmb_Bank_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Bank.SelectedIndexChanged
        If Cmb_Bank.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()

            Cmb_MataUang.SelectedIndex = -1 : Cmb_MataUang.Text = "" : Txt_Kurs.Text = ""

            Cmb_Rek.Items.Clear() : ArrAkunRek1.Clear() : ArrMataUangRek.Clear()
            'LvCheque.Items.Clear()
            SQL = "SELECT r.No_Rek, Kode_Akun, Mata_Uang FROM Rekening r WHERE "
            SQL = SQL & "r.Kode_Perusahaan = '" & KodePerusahaan & "' "
            If Cmb_Bank.SelectedIndex <> -1 Then
                SQL = SQL & "and r.kode_bank = '" & Cmb_Bank.Text & "' "
            End If
            SQL = SQL & "ORDER BY r.No_Rek"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Rek.Items.Add(dr("no_rek")) : ArrAkunRek1.Add(dr("Kode_Akun")) : ArrMataUangRek.Add(dr("Mata_Uang"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox8_KeyPress_1(sender As Object, e As KeyPressEventArgs) Handles TextBox8.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox8.Text.Trim.Length = 0 Then
                MessageBox.Show("Silahkan isi kurs terlebih dahulu") : Exit Sub
            End If

            TextBox8.Enabled = False
            cbxMataUang.Enabled = False
            TextBox1.Enabled = True
            btnCari.Enabled = True
            TextBox1.Focus()
            varMataUang = cbxMataUang.Text
            kursUangBaru = TextBox8.Text
        End If
    End Sub




    Private Sub TextBox8_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'If e.KeyChar = Chr(13) Then

        TextBox1.Enabled = True
        btnCari.Enabled = True
        TextBox1.Focus()

        'End If
        'If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    'Private Sub TextBoxselisih_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxselisih.KeyPress
    '    If e.KeyChar = Chr(13) Then
    '        TextBox6.Focus()
    '    End If
    '    If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)

    'End Sub
End Class