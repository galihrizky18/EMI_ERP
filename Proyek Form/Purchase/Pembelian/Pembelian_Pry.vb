Imports System.IO
Imports System.Net
Public Class Pembelian_Pry
    Dim total As Double
    Dim lv As New ListViewItem
    Dim Hrg_Minimum As Double
    Public total_bayar As Double
    Dim sayTerbilang, xNo_PO As String
    Dim RVPO As Integer

    Dim LvSO As String
    Dim LvKB As String
    Dim LvNm As String
    Dim LvSerialNumber As String
    Dim LvExpire As String
    Dim LvHrg As String
    Dim LvJml As String
    Dim LvSat As String
    Dim LvDiscp As String
    Dim LvDiscrp As String
    Dim LvSubttl As String
    Dim LvPakaiSN As String
    Dim LvModal As String
    Dim LvUpdateHPP As String
    Dim LvUrutBarangMasuk As String

    Public IndexSO As Integer = 0
    Public IndexKB As Integer = 1
    Public IndexNm As Integer = 2
    Public IndexSerialNumber As Integer = 3
    Public IndexExpire As Integer = 4
    Public IndexHrg As Integer = 5
    Public IndexJml As Integer = 6
    Public IndexSat As Integer = 7
    Public IndexDiscp As Integer = 8
    Public IndexDiscrp As Integer = 9
    Public IndexSubttl As Integer = 10
    Public IndexPakaiSN As Integer = 11
    Public IndexModal As Integer = 12
    Public IndexUpdateHPP As Integer = 13
    Public IndexUrutBarangDetal As Integer = 14

    Dim arrPersediaan, arrInisialFaktur, arrCrByr, ArrAkunCB1, arrAkunJasa As New ArrayList

    Private Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvSO = Listview1.Items(No_Index).Text
        LvKB = Listview1.Items(No_Index).SubItems(1).Text
        LvNm = Listview1.Items(No_Index).SubItems(2).Text
        LvSerialNumber = Listview1.Items(No_Index).SubItems(3).Text
        LvExpire = Listview1.Items(No_Index).SubItems(4).Text
        LvHrg = Listview1.Items(No_Index).SubItems(5).Text
        LvJml = Listview1.Items(No_Index).SubItems(6).Text
        LvSat = Listview1.Items(No_Index).SubItems(7).Text
        LvDiscp = Listview1.Items(No_Index).SubItems(8).Text
        LvDiscrp = Listview1.Items(No_Index).SubItems(9).Text
        LvSubttl = Listview1.Items(No_Index).SubItems(10).Text
        LvPakaiSN = Listview1.Items(No_Index).SubItems(11).Text
        LvModal = Listview1.Items(No_Index).SubItems(12).Text
        LvUpdateHPP = Listview1.Items(No_Index).SubItems(13).Text
        LvUrutBarangMasuk = Listview1.Items(No_Index).SubItems(14).Text
    End Sub

    Private Sub cetak()
        Try
            OpenConn()

            SQL = "select kode_perusahaan from pembelian_proyek where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & TxtFaktur.Text.Trim & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Dim CrDoc As New Faktur_Pembelian_Proyek      'Nama file CR
                    With A_Place_For_Printing
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        ' CrDoc.PrintOptions.PrinterName = PrinterName
                        CrDoc.RecordSelectionFormula = "{pembelian_proyek.Kode_Perusahaan} = '" & KodePerusahaan & "' and {pembelian_proyek.No_faktur} = '" & TxtFaktur.Text.Trim & "'"
                        CrDoc.SummaryInfo.ReportTitle = "Faktur Pembelian"
                        .Text = "Faktur Pembelian"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .CrystalReportViewer1.DisplayGroupTree = False
                        .Refresh()
                        .Show()
                    End With
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub cari_po(ByVal no_po As String)
        TextBox7.Text = ""
        TextBox8.Text = ""
        RVPO = 0

        TextBox2.Enabled = False
        TextBox10.Enabled = False
        TextBox11.Enabled = False
        ComboBox2.Enabled = True
        ComboBox5.Enabled = True
        DateTimePicker2.Enabled = True
        CheckBox1.Enabled = True
        CheckBox2.Enabled = True
        CheckBox3.Enabled = True
        Discx.Enabled = True
        ComboBoxCb1.SelectedIndex = -1
        ComboBoxCb1.Enabled = True
        TextBox12.Text = ""
        CheckBox3.Enabled = False
        ComboBoxCb1.Enabled = False
        ComboBox2.Enabled = False
        DateTimePicker2.Enabled = False
        'ComboBox5.Enabled = False
        Try
            OpenConn()

            SQL = "select a.ppn, a.no_so, cast(e.rv as bigint) as rvx, e.no_faktur, a.no_nota, a.kode_supplier, c.nama as nama_supplier,a.no_faktur as no_po,"
            SQL = SQL & "b.kode_Stock_owner, b.kode_barang,d.nama as nama_barang, f.jumlah, d.satuan, b.Harga, f.Urut as urut_barang_masuk, "
            SQL = SQL & "f.Jumlah * b.Harga as subtotal, Jenis_Transaksi, Tgl_Jatuh_Tempo, kode_cb, datediff(day, a.Tanggal,Tgl_Jatuh_Tempo) as jumlah_tempo from "

            SQL = SQL & "po_pembelian_proyek a, detail_po_pembelian_proyek b, suppliers c, barang_proyek d, "
            SQL = SQL & "Barang_Masuk_Proyek e, Barang_Masuk_Proyek_Detail f where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and "
            SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan and "
            SQL = SQL & "a.no_faktur = b.no_faktur and a.kode_supplier = c.kode_Supplier and "
            SQL = SQL & "b.kode_Stock_owner = d.kode_Stock_owner and b.kode_barang = d.kode_barang and "
            SQL = SQL & "a.Kode_Perusahaan = e.Kode_Perusahaan and a.No_Faktur = e.No_PO and "
            SQL = SQL & "b.Kode_Perusahaan = f.Kode_Perusahaan and b.No_Urut = f.Urut_PO and "
            SQL = SQL & "e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Faktur = f.No_Faktur and "
            SQL = SQL & "e.kode_perusahaan = '" & KodePerusahaan & "' and e.no_faktur = '" & no_po & "' and "
            SQL = SQL & "e.lokasi = '" & ComboBox4.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TextBox8.Text = Dr("no_faktur")
                    TextBox2.Text = Dr("no_nota")
                    TextBox10.Text = Dr("kode_supplier")
                    TextBox11.Text = Dr("nama_supplier")
                    ListView10.Visible = False
                    RVPO = Dr("rvx")

                    xNo_PO = Dr("no_po")

                    TextBox12.Text = General_Class.CekNULL(Dr("no_so"))

                    ComboBox5.SelectedIndex = 0
                    ComboBox2.SelectedIndex = 0

                    DateTimePicker2.Visible = False
                    ComboBox5.Visible = False
                    ComboBoxCb1.Enabled = True

                    CheckBox1.Checked = False
                    CheckBox2.Checked = False

                    discz.Text = "0"
                    Discx.Text = "0"
                    Discx.Enabled = False

                    If Dr("ppn") <> 0 Then
                        CheckBox3.Checked = True
                        TextBox3.Text = Dr("ppn")
                    Else
                        CheckBox3.Checked = False
                        TextBox3.Text = "0"
                    End If

                    If Dr("Jenis_Transaksi") = "T" Then
                        ComboBox2.SelectedIndex = 0
                        ComboBox2_SelectedIndexChanged(Button1, Nothing)

                        For index As Integer = 0 To ComboBoxCb1.Items.Count - 1

                            If arrCrByr.Item(index) = Dr("kode_cb") Then
                                ComboBoxCb1.SelectedIndex = index
                                Exit For
                            End If

                        Next

                    Else
                        ComboBox2.SelectedIndex = 1
                        ComboBox2_SelectedIndexChanged(Button1, Nothing)
                        ComboBox5.Text = Dr("jumlah_tempo")
                        DateTimePicker2.Value = Dr("Tgl_Jatuh_Tempo")
                    End If

                    TextBox5.Text = "0"

                    Listview1.Items.Clear()

                    'LvSO = Listview1.Items(No_Index).Text
                    'LvKB = Listview1.Items(No_Index).SubItems(1).Text
                    'LvNm = Listview1.Items(No_Index).SubItems(2).Text
                    'LvSerialNumber = Listview1.Items(No_Index).SubItems(3).Text
                    'LvExpire = Listview1.Items(No_Index).SubItems(4).Text
                    'LvHrg = Listview1.Items(No_Index).SubItems(5).Text
                    'LvJml = Listview1.Items(No_Index).SubItems(6).Text
                    'LvSat = Listview1.Items(No_Index).SubItems(7).Text
                    'LvDiscp = Listview1.Items(No_Index).SubItems(8).Text
                    'LvDiscrp = Listview1.Items(No_Index).SubItems(9).Text
                    'LvSubttl = Listview1.Items(No_Index).SubItems(10).Text
                    'LvPakaiSN = Listview1.Items(No_Index).SubItems(11).Text
                    'LvModal = Listview1.Items(No_Index).SubItems(12).Text
                    'LvUpdateHPP = Listview1.Items(No_Index).SubItems(13).Text

                    Dim lv As New ListViewItem
                    lv = Listview1.Items.Add(Dr("kode_stock_owner"))
                    lv.SubItems.Add(Dr("kode_barang"))
                    lv.SubItems.Add(Dr("nama_barang"))
                    lv.SubItems.Add("-")
                    lv.SubItems.Add("2050-01-01")
                    lv.SubItems.Add(Format(Dr("harga"), "N2"))
                    lv.SubItems.Add(Dr("jumlah"))
                    lv.SubItems.Add(Dr("satuan"))
                    lv.SubItems.Add("0")
                    lv.SubItems.Add("0")
                    lv.SubItems.Add(Format(Dr("subtotal"), "N0"))
                    lv.SubItems.Add("Y")
                    lv.SubItems.Add(Format(Dr("harga"), "N0"))
                    lv.SubItems.Add("T")
                    lv.SubItems.Add(Dr("urut_barang_masuk"))


                    Do While Dr.Read
                        lv = Listview1.Items.Add(Dr("kode_stock_owner"))
                        lv.SubItems.Add(Dr("kode_barang"))
                        lv.SubItems.Add(Dr("nama_barang"))
                        lv.SubItems.Add("-")
                        lv.SubItems.Add("2050-01-01")
                        lv.SubItems.Add(Format(Dr("harga"), "N2"))
                        lv.SubItems.Add(Dr("jumlah"))
                        lv.SubItems.Add(Dr("satuan"))
                        lv.SubItems.Add("0")
                        lv.SubItems.Add("0")
                        lv.SubItems.Add(Format(Dr("subtotal"), "N0"))
                        lv.SubItems.Add("Y")
                        lv.SubItems.Add(Format(Dr("harga"), "N0"))
                        lv.SubItems.Add("T")
                        lv.SubItems.Add(Dr("urut_barang_masuk"))

                    Loop
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Data tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                HitungGrandTotal()
            End Using

            '================================
            '=     GET DATA DOWNPAYMENT     =
            '================================
            Dim Total_DP As Double = 0
            SQL = ";with Cte as ( "
            SQL = SQL & "select a.Nilai as Nilai_DP, ( "
            SQL = SQL & "a.Nilai - "
            SQL = SQL & "ISNULL(( "
            SQL = SQL & "select z.nilai from Val_Pemb_Proyek_Detail_DP z, Val_Pemb_Proyek w "
            SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan and z.urut_DP = a.No_Urut and "
            SQL = SQL & "z.kode_perusahaan=w.kode_Perusahaan and z.no_val=w.no_val and w.status is null "
            SQL = SQL & "), 0) ) as Sisa "
            SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka_Detail_Proyek a, EMI_Transaksi_Pembayaran_Dimuka_Proyek b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "And a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "And b.Status Is null "
            SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Fak_PO in ( "
            SQL = SQL & "select x.No_Faktur "
            SQL = SQL & "from PO_Pembelian_Proyek x "
            SQL = SQL & "where x.Status is null "
            SQL = SQL & "and x.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and x.No_Faktur = '" & xNo_PO & "' "
            SQL = SQL & "group by x.No_Faktur ) ) "
            SQL = SQL & "select sum(Sisa) as Nilai_DP from Cte "
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For index = 0 To .Rows.Count - 1
                            If General_Class.CekNULL(.Rows(index).Item("Nilai_DP")) <> "" Then
                                Total_DP = .Rows(index).Item("Nilai_DP")
                            Else
                                Total_DP = "0"
                            End If
                        Next
                    End If
                End With
            End Using
            TextBox14.Text = Format((Total_DP), "N0")

            CloseConn()
        Catch ex As Exception
            Listview1.Items.Clear()
            TextBox10.Text = ""
            TextBox12.Text = ""

            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        HitungGrandTotal()
    End Sub

    Public Sub ubah_listview(ByVal bariske As Integer, ByVal hrgbl As Double)
        Listview1.Items(bariske).SubItems(IndexHrg).Text = Format(hrgbl, "N0")
        Listview1.Items(bariske).SubItems(IndexSubttl).Text = Format(hrgbl * Val(HilangkanTanda(Listview1.Items(bariske).SubItems(IndexJml).Text)), "N0")
        Listview1.Items(bariske).SubItems(IndexModal).Text = Format(hrgbl, "N0")

        HitungGrandTotal()
    End Sub

    Private Sub get_no_faktur(ByVal pembayaran As String)
        TxtFaktur.Text = fb & arrInisialFaktur.Item(ComboBox4.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy") & "-" &
                                     General_Class.Get_Last_Number2("pembelian_proyek", "no_faktur", JumlahDigit,
                                     "Kode_perusahaan", KodePerusahaan,
                                     "And", "substring(no_faktur,1," & Len(fb) + Len(arrInisialFaktur.Item(ComboBox4.SelectedIndex)) + 6 & ")", fb & arrInisialFaktur.Item(ComboBox4.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))
    End Sub

    Private Function get_no_faktur_sementara() As String
        Dim nfaktur As String
        nfaktur = fbSementara & arrInisialFaktur.Item(ComboBox4.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy") & "-" &
                                     General_Class.Get_Last_Number2("pembelian_sementara", "no_faktur", JumlahDigit,
                                     "Kode_perusahaan", KodePerusahaan,
                                     "And", "substring(no_faktur,1," & Len(fbSementara) + Len(arrInisialFaktur.Item(ComboBox4.SelectedIndex)) + 6 & ")", fbSementara & arrInisialFaktur.Item(ComboBox4.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))

        Return nfaktur
    End Function

    Private Sub get_unik()
        Dim rand As New Random
        Label1.Text = Format(tgl_skg, "MMddHHmmss") & Format(rand.Next(0, 100000), "00000")
    End Sub

    Private Sub cek_diskon()
        If RadioButton1.Checked Then
            lv.SubItems.Add(disc.Text)
            lv.SubItems.Add("0")
            total = (Val(hrg.Text) * HilangkanTanda(TampilanDesimal(Val(jml.Text)))) - (Val(hrg.Text) * HilangkanTanda(TampilanDesimal(Val(jml.Text))) * Val(disc.Text) / 100)
        Else
            lv.SubItems.Add("0")
            lv.SubItems.Add(Format(Val(disc.Text), "N0"))
            total = (Val(hrg.Text) - Val(disc.Text)) * HilangkanTanda(TampilanDesimal(Val(jml.Text)))
        End If
    End Sub

    Private Sub HitungGrandTotal()
        Try
            OpenConn()

            Dim Grand As Double = 0
            Dim diskon As Double = 0
            Dim TotalSeluruh As Double = 0
            'Dim PPN As Double = 0

            For i As Integer = 0 To Listview1.Items.Count - 1
                Get_Isi_Listview(i)

                Grand = Grand + HilangkanTanda(LvSubttl)
            Next
            'total_bayar = Grand
            'If discz.Text.Trim.Length = 0 Then
            '    discz.Text = 0
            'End If

            'If CheckBox1.Checked = True Then

            'End If
            'If RadioButton10.Checked Then
            '    diskon = Grand * Val(Trim(discz.Text)) / 100
            'Else
            '    diskon = Val(Trim(discz.Text))
            'End If

            diskon = Grand * HilangkanTanda(TampilanDesimal(Val(Trim(discz.Text)))) / 100
            diskon = HilangkanTanda(Format(diskon, "N0"))
            TextBox6.Text = Format(Grand - diskon - Val(Trim(Discx.Text)), "N0")
            TextBox5.Text = Format((Val(HilangkanTanda(TextBox6.Text)) - diskon - Val(Trim(Discx.Text))) * Val(TextBox3.Text) / 100, "N0")

            Dim totalpph As Double = 0
            SQL = "select b.Persentase from Barang_Masuk_Proyek a, Detail_PO_Pembelian_Proyek_PPH b, PO_Pembelian_Proyek c "
            SQL = SQL & "where a.No_PO = c.No_Faktur and a.Status is null and c.Status is null and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Flag_PPN is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & TextBox8.Text & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    If dr("Persentase") <> 0 Then
                        totalpph = totalpph + (Val(HilangkanTanda(TextBox6.Text)) * dr("Persentase") / 100)
                    End If
                Loop
            End Using
            TotalSeluruh = Val(HilangkanTanda(TextBox6.Text)) - diskon - Val(Trim(Discx.Text)) + Val(HilangkanTanda(TextBox5.Text))

            TxtSub.Text = Format(Grand, "N0")
            TextBox1.Text = Format(diskon, "N0")
            Label12.Text = Format(TotalSeluruh, "N0")
            TextBox13.Text = Format(totalpph, "N0")
            TxtTotal.Text = Format(TotalSeluruh, "N0")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub BersihSebagian()
        kd.Text = "" : nm.Text = "" : TextBox4.Text = ""
        TextBox4.Enabled = False : ket.Text = "Keterangan. . ."
        DateTimePicker3.Value = CDate(FMenuDevFix.ToolStripStatusLabel3.Text)
        hrg.Text = "" : jml.Text = "" : sat.Text = "" : disc.Text = ""
        RadioButton1.Checked = True
    End Sub

    Public Sub BersihSeluruh()
        DateTimePicker1.Value = CDate(FMenuDevFix.ToolStripStatusLabel3.Text)
        DateTimePicker2.Value = CDate(FMenuDevFix.ToolStripStatusLabel3.Text)

        BersihSebagian()

        Label12.Text = "0"
        Listview1.Items.Clear()
        ListView3.Items.Clear()

        get_unik()

        TxtFaktur.Text = "" : TextBox7.Text = "" : TextBox8.Text = ""


        TextBox9.Text = ""

        TextBox2.Enabled = False : TextBox10.Enabled = False : TextBox11.Enabled = False
        ComboBox2.Enabled = True : ComboBox5.Enabled = True : DateTimePicker2.Enabled = True
        CheckBox1.Enabled = True : CheckBox2.Enabled = True : CheckBox3.Enabled = True
        Discx.Enabled = True : ComboBoxCb1.Enabled = True

        get_jam()

        Try
            OpenConn()

            ComboBox4.Items.Clear() : arrPersediaan.Clear() : arrInisialFaktur.Clear() : arrAkunJasa.Clear()
            SQL = "Select kode_stock_owner, persediaan, inisial_faktur, Akun_Jasa From stock_owner_proyek where kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox4.Items.Add(dr("kode_stock_owner")) : arrPersediaan.Add(dr("persediaan")) : arrInisialFaktur.Add(dr("inisial_faktur")) : arrAkunJasa.Add(dr("Akun_Jasa"))
                Loop
            End Using

            'ComboBox4.Text = Lokasi_Proyek
            ComboBox4.SelectedItem = Lokasi_Proyek

            get_no_faktur("T")

            ComboBoxCb1.Items.Clear()
            arrCrByr.Clear()
            ArrAkunCB1.Clear()

            ComboBoxCb1.Items.Add("-- Cara Bayar --") : arrCrByr.Add("") : ArrAkunCB1.Add("")
            ComboBoxCb1.SelectedIndex = 0
            SQL = "select kode_cb, keterangan, kode_account_cb from cara_bayar where kode_perusahaan = '" & KodePerusahaan & "' and Lokasi_Proyek = '" & ComboBox4.Text & "' order by keterangan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBoxCb1.Items.Add(Dr("keterangan")) : arrCrByr.Add(Dr("kode_cb")) : ArrAkunCB1.Add(Dr("kode_account_cb"))
                Loop
            End Using

            'ListView3.Items.Clear()
            'Dim lvw As New ListViewItem
            'SQL = "Select a.no_faktur, a.no_nota, a.tanggal, b.nama From "
            'SQL = SQL & "pembelian_sementara a, suppliers b where "
            'SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.kode_supplier = b.kode_supplier and "
            'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.lokasi = '" & ComboBox4.Text & "' and "
            'SQL = SQL & "a.validasi = 'Y' and a.pakai is null and a.status is null order by a.tanggal + a.jam"
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read
            '        lv = ListView3.Items.Add(dr("no_faktur"))
            '        lv.SubItems.Add(dr("no_nota"))
            '        lv.SubItems.Add(Format(dr("tanggal"), "dd MMM yyyy"))
            '        lv.SubItems.Add(dr("nama"))
            '    Loop
            'End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        TextBox2.Text = ""
        TextBox12.Text = ""

        TextBox10.Text = ""
        TextBox11.Text = ""

        ComboBox5.Items.Clear()
        For i As Integer = 1 To 120
            ComboBox5.Items.Add(i)
        Next
        ComboBox5.SelectedIndex = 0 : ComboBox2.SelectedIndex = 0

        DateTimePicker2.Visible = False : ComboBox5.Visible = False : ComboBoxCb1.Enabled = True

        TxtSub.Text = "0" : TxtTotal.Text = "0" : TextBox14.Text = "0"

        CheckBox1.Checked = False : discz.Enabled = False
        discz.Text = "0" : TextBox1.Text = "0"

        CheckBox2.Checked = False : Discx.Enabled = False
        Discx.Text = "0" : TxtPPN.Text = "0" : TextBox13.Text = "0"

        CheckBox3.Checked = False : TextBox3.Text = "0" : TextBox5.Text = "0"

        hrg.Enabled = True

        TextBox10.Enabled = True : TextBox11.Enabled = True : DateTimePicker1.Enabled = True

        Button2.Text = "&Simpan"

        Try
            Dim adaga As Integer = 0

            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            SQL = "select p.Kode_Perusahaan, p.No_Faktur, (select Lokasi from pembelian_proyek where Kode_Perusahaan = p.Kode_Perusahaan and No_Faktur = p.No_Faktur) lokasi, (select concat(kode_supplier, '/', nama) from suppliers where Kode_Perusahaan = p.Kode_Perusahaan and Kode_Supplier = p.Kode_Supplier) as supplier, string_agg(b.Nama, ';') nama_barang, STRING_AGG(dp.Harga, ';') harga, STRING_AGG(b.Harga_Agen, ';') harga_agen from "
            SQL = SQL & "pembelian_proyek p, Detail_Pembelian_proyek dp, barang_proyek b, suppliers sp where "
            SQL = SQL & "p.Kode_perusahaan = dp.Kode_Perusahaan and "
            SQL = SQL & "dp.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "b.Kode_Perusahaan = sp.Kode_Perusahaan and "
            SQL = SQL & "p.Kode_Supplier = sp.kode_supplier and "
            SQL = SQL & "p.No_Faktur = dp.No_Faktur and "
            SQL = SQL & "dp.kode_barang = b.kode_barang and "
            SQL = SQL & "p.Lokasi = b.Kode_Stock_Owner and "
            SQL = SQL & "p.Kode_Supplier = sp.Kode_Supplier and "
            SQL = SQL & "(p.Sudah_Cek is null or p.Sudah_Cek = 'T') and "
            SQL = SQL & "dp.Harga > b.Harga_Agen + (b.Harga_Agen * (b.Persentase / 100.0)) "
            SQL = SQL & "group by p.Kode_Perusahaan, p.no_faktur, p.Kode_Supplier"

            Dim msg = "Pembelian_proyek" & vbCrLf & vbCrLf
            msg = msg & "--------------------------------------------------------------------" & vbCrLf
            Dim pembelianList As New List(Of String)
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    adaga = 1

                    Dim kodePerusahaan = dr("Kode_Perusahaan")
                    Dim noFaktur = dr("No_Faktur")
                    Dim Lokasi_Proyek = dr("Lokasi")
                    Dim supplier = dr("Supplier")
                    Dim namaBarangArr = dr("Nama_Barang").ToString().Split(";")
                    Dim hargaArr = dr("Harga").ToString().Split(";")
                    Dim hargaAgenArr = dr("Harga_Agen").ToString().Split(";")

                    pembelianList.Add(kodePerusahaan & ";" & noFaktur)

                    msg = msg & "No. Faktur: " & noFaktur & vbCrLf
                    msg = msg & "Lokasi: " & Lokasi_Proyek & vbCrLf
                    msg = msg & "Supplier: " & supplier & vbCrLf & vbCrLf
                    For i As Integer = 0 To namaBarangArr.Length - 1
                        msg = msg & namaBarangArr(i) & ": " & Format(Val(hargaArr(i)), "n0") & " (Harga agen " & Format(Val(hargaAgenArr(i)), "n0") & ")" & vbCrLf
                    Next

                    msg = msg & "--------------------------------------------------------------------" & vbCrLf
                    msg = msg & "--------------------------------------------------------------------" & vbCrLf
                Loop
            End Using

            If adaga = 1 Then
                Using dr = OpenTrans("SELECT No_Hp FROM Wa_No_Tujuan")
                    Do While dr.Read
                        Dim No_Hp = dr("No_Hp")

                        Dim param_wa As String = ""
                        Dim Rand As New Random
                        Dim custom_uid As String = ""

                        Dim Request As HttpWebRequest
                        Dim Response As HttpWebResponse
                        Dim responseReader As StreamReader
                        Dim result As String

                        custom_uid = Format(Rand.Next(0, 999), "000") & "A" & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "ddMMyyHHmmss")
                        msg = Replace(msg, " ", "+")

                        param_wa = "https://www.waboxapp.com/api/send/chat?token=" & token_wa & "&uid=" & uid_wa & "&"
                        param_wa = param_wa & "to=" & No_Hp & "&text=" & msg & "&"
                        param_wa = param_wa & "custom_uid=" & custom_uid

                        Request = HttpWebRequest.Create(param_wa)
                        Response = Request.GetResponse
                        responseReader = New StreamReader(Response.GetResponseStream())
                        result = responseReader.ReadToEnd()

                        If Strings.Mid(result, 3, 7) <> "success" Then 'sukses
                            MessageBox.Show("Kirim WA gagal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Loop
                End Using
            End If

            For i As Integer = 0 To pembelianList.Count - 1
                Dim kodePerusahaan = pembelianList(i).Split(";")(0)
                Dim noFaktur = pembelianList(i).Split(";")(1)
                Dim SQL1 As String = "UPDATE Pembelian_proyek SET Sudah_Cek = 'Y' WHERE Kode_Perusahaan = '" & kodePerusahaan & "' AND No_Faktur = '" & noFaktur & "'"
                ExecuteTrans(SQL1)
            Next

            Cmd.Transaction.Commit()

            CloseConn()

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MsgBox(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Pembelian_New_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Penjualan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        BersihSeluruh()

        Listview1.Columns.Add("Stock Owner", 0, HorizontalAlignment.Left)
        Listview1.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Listview1.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left)
        Listview1.Columns.Add("Serial Number", 0, HorizontalAlignment.Left)
        Listview1.Columns.Add("Expire", 100, HorizontalAlignment.Center)
        Listview1.Columns.Add("Harga", 140, HorizontalAlignment.Right)
        Listview1.Columns.Add("Jumlah", 120, HorizontalAlignment.Center)
        Listview1.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        Listview1.Columns.Add("Disc(%)", 0, HorizontalAlignment.Right)
        Listview1.Columns.Add("Disc(Rp.)", 0, HorizontalAlignment.Right)
        Listview1.Columns.Add("Total", 160, HorizontalAlignment.Right)
        Listview1.Columns.Add("Pakai SN", 0, HorizontalAlignment.Left)
        Listview1.Columns.Add("Modal", 0, HorizontalAlignment.Left)
        Listview1.Columns.Add("Upd HPP", 0, HorizontalAlignment.Left) 'update hpp
        Listview1.Columns.Add("Urut Barang Masuk", 0, HorizontalAlignment.Left)
        Listview1.View = View.Details

        ListView2.Columns.Add("Stock Owner", 87, HorizontalAlignment.Center)
        ListView2.Columns.Add("Kode Barang", 143, HorizontalAlignment.Center)
        ListView2.Columns.Add("Nama", 320, HorizontalAlignment.Left)
        ListView2.Columns.Add("Satuan", 60, HorizontalAlignment.Left)
        ListView2.Columns.Add("Harga Beli", 90, HorizontalAlignment.Right)
        ListView2.Columns.Add("Good Stock", 70, HorizontalAlignment.Right)
        ListView2.Columns.Add("Disc(%)", 50, HorizontalAlignment.Right)
        ListView2.Columns.Add("Disc(Rp.)", 60, HorizontalAlignment.Right)
        ListView2.View = View.Details

        ListView10.Columns.Add("Kode Supplier", 85, HorizontalAlignment.Left)
        ListView10.Columns.Add("Nama", 195, HorizontalAlignment.Left)
        ListView10.Columns.Add("Alamat", 240, HorizontalAlignment.Left)
        ListView10.Columns.Add("Telepon", 100, HorizontalAlignment.Left)
        ListView10.Columns.Add("HP", 100, HorizontalAlignment.Left)
        ListView10.View = View.Details


        ListView2.Location = New Point(6, 221)
        ListView2.Visible = False

        ListView10.Location = New Point(6, 120)
        ListView10.Visible = False

        TextBox10.Focus()
    End Sub

    Private Sub ket_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ket.GotFocus
        If ket.Text.ToUpper = "KETERANGAN. . ." Then
            ket.Text = ""
        End If
    End Sub

    Private Sub ket_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ket.LostFocus
        If ket.Text.Trim.Length = 0 Then
            ket.Text = "Keterangan. . ."
        End If
    End Sub

    Private Sub kd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles kd.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView2.Items.Count = 0 Then Exit Sub
            ListView2.Focus()
        End If
    End Sub

    Private Sub kd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles kd.KeyPress
        If e.KeyChar = Chr(13) Then
            DateTimePicker3.Focus()
        End If
        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub nm_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles nm.KeyPress
        If e.KeyChar = Chr(13) Then ket.Focus()
    End Sub

    Private Sub ket_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ket.KeyPress
        If e.KeyChar = Chr(13) Then
            DateTimePicker3.Focus()
        End If
        If e.KeyChar = Chr(Asc("'")) Or e.KeyChar = Chr(Asc("[")) Or e.KeyChar = Chr(Asc("]")) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub jml_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles jml.KeyPress
        If e.KeyChar = Chr(13) Then disc.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub hrg_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles hrg.KeyPress
        If e.KeyChar = Chr(13) Then jml.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub disc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles disc.KeyPress
        If e.KeyChar = Chr(13) Then ok_Click(disc, e)
        If RadioButton1.Checked Then
            'If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
            If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
        Else
            If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
        End If
    End Sub

    Private Sub RadioButton1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles RadioButton1.KeyPress
        If e.KeyChar = Chr(13) Then disc.Focus()
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        'disc.Width = 28
        'disc.MaxLength = 2
        disc.Text = ""
        disc.Focus()
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        'disc.Width = 70
        'disc.MaxLength = 10
        disc.Text = ""
        disc.Focus()
    End Sub

    Private Sub discz_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles discz.KeyPress
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
        'If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub Discx_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Discx.KeyPress
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub discz_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles discz.TextChanged
        HitungGrandTotal()
        'TextBox1.Text = Format(Val(HilangkanTanda(TxtSub.Text)) * Val(discz.Text) / 100, "N0")
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        BersihSebagian()
        kd.Focus()
    End Sub

    Private Sub ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ok.Click
        'If TextBox7.Text.Trim.Length <> 0 Then Exit Sub

        'If ComboBox4.Text.Trim.Length = 0 Then
        '    MessageBox.Show("Stock owner harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    ComboBox4.Focus() : Exit Sub
        'ElseIf kd.Text.Trim.Length = 0 Then
        '    MessageBox.Show("Kode barang_proyek belum diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    kd.Focus()
        '    Exit Sub
        'ElseIf jml.Text.Trim.Length = 0 Then
        '    MessageBox.Show("Jumlah belum diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    jml.Focus()
        '    Exit Sub
        'ElseIf jml.Text.Trim = "0" Then
        '    MessageBox.Show("Jumlah tidak boleh nol.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    jml.Focus()
        '    Exit Sub
        'ElseIf Format(DateTimePicker3.Value, "yyyy-MM-dd") < Format(DateTimePicker1.Value, "yyyy-MM-dd") Then
        '    MessageBox.Show("Tanggal expire lebih kecil dari tanggal pembelian.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    DateTimePicker3.Focus()
        '    Exit Sub
        'ElseIf DateDiff(DateInterval.Day, DateTimePicker1.Value, DateTimePicker3.Value) < 365 Then
        '    MessageBox.Show("Tanggal expire minimal harus 365 hari", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    DateTimePicker3.Focus()
        '    Exit Sub
        'ElseIf hrg.Text.Trim.Length = 0 Then
        '    MessageBox.Show("Harga belum diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    hrg.Focus()
        '    Exit Sub
        'End If

        'If TextBox4.Enabled = True Then
        '    If TextBox4.Text.Trim.Length = 0 Then
        '        MessageBox.Show("Serial number belum diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        TextBox4.Focus()
        '        Exit Sub
        '        'ElseIf Val(jml.Text) <> 1 Then
        '        '    MessageBox.Show("barang_proyek yang memakai serial number jumlahnya harus 1.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        '    jml.Focus()
        '        '    Exit Sub
        '    End If
        'End If

        'If disc.Text.Trim.Length = 0 Then
        '    disc.Text = "0"
        'End If

        'If _gabung = "Y" Then
        '    If listview1.Items.Count > 0 Then
        '        For i As Integer = 0 To listview1.Items.Count - 1
        '            Get_Isi_Listview(i)

        '            If ComboBox4.Text.Trim.ToUpper = LvSO.Trim.ToUpper And kd.Text.Trim.ToUpper = LvKB.Trim.ToUpper Then
        '                MessageBox.Show("Kode barang_proyek sudah Anda masukkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Exit Sub
        '            End If
        '        Next
        '    End If
        'End If

        'If TextBox4.Enabled = True Then
        '    For i As Integer = 0 To listview1.Items.Count - 1
        '        Get_Isi_Listview(i)

        '        If LvPakaiSN = "Y" Then
        '            If ComboBox4.Text.Trim.ToUpper = LvSO.Trim.ToUpper And kd.Text.Trim.ToUpper = LvKB.Trim.ToUpper And TextBox4.Text.Trim.ToUpper = LvSerialNumber.Trim.ToUpper Then
        '                MessageBox.Show("barang_proyek dengan serial number ini sudah dimasukan sebelumnya.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                TextBox4.Focus()
        '                Exit Sub
        '            End If
        '        End If
        '    Next
        'End If

        'lv = listview1.Items.Add(ComboBox4.Text)
        'lv.SubItems.Add(kd.Text)
        'If ket.Text.ToUpper = "KETERANGAN. . ." Or ket.Text.Trim.Length = 0 Then
        '    lv.SubItems.Add(nm.Text)
        'Else
        '    lv.SubItems.Add(nm.Text & "[" & Trim(ket.Text) & "]")
        'End If
        'If TextBox4.Enabled = True Then
        '    lv.SubItems.Add(TextBox4.Text.Trim)
        'Else
        '    lv.SubItems.Add("-")
        'End If
        'lv.SubItems.Add(Format(DateTimePicker3.Value, "dd MMM yyyy"))
        'lv.SubItems.Add(Format(Val(hrg.Text), "N0"))
        'lv.SubItems.Add(TampilanDesimal(Val(jml.Text)))
        'lv.SubItems.Add(sat.Text)
        'cek_diskon()
        'lv.SubItems.Add(Format(total, "N0"))

        'If TextBox4.Enabled = True Then
        '    lv.SubItems.Add("Y")
        'Else
        '    lv.SubItems.Add("T")
        'End If

        'Dim modal As Double = 0
        'If RadioButton1.Checked = True Then
        '    modal = Val(hrg.Text) - (Val(hrg.Text) * disc.Text / 100)
        'Else
        '    modal = Val(hrg.Text) - disc.Text
        'End If
        'lv.SubItems.Add(Format(modal, "N0"))
        'lv.SubItems.Add("T")

        'HitungGrandTotal()
        'bersihsebagian()
        'kd.Focus()
    End Sub

    Private Sub kd_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles kd.Leave
        If kd.Text.Trim.Length = 0 Then Exit Sub
        If ListView2.Focused = True Then Exit Sub

        If ComboBox4.Text.Trim.Length = 0 Then
            MessageBox.Show("Stock owner harus diisi dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kd.Text = "" : ComboBox4.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "Select pakai_sn, kode_stock_owner, kode_barang, Nama, satuan, harga_beli, harga_jual, good_stock, disc1, disc2 From barang_proyek "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and kode_stock_owner = '" & ComboBox4.Text & "' and "
            SQL = SQL & "kode_barang = '" & Trim(kd.Text) & "' and kode_pembeda in(" & list_pembeda & ")"
            Using dr = Open(SQL)
                If dr.Read Then
                    TextBox4.Text = ""
                    If dr("pakai_sn") = "Y" Then
                        TextBox4.Enabled = True
                    Else
                        TextBox4.Enabled = False
                    End If
                    TextBox4.Text = "-"
                    kd.Text = dr("kode_barang")
                    nm.Text = dr("nama")
                    ket.Text = "Keterangan. . ."
                    hrg.Text = dr("harga_beli")
                    Hrg_Minimum = dr("harga_beli")
                    jml.Text = "1"
                    sat.Text = dr("satuan")

                    RadioButton1.Checked = True
                    disc.Text = "0"

                    DateTimePicker3.Focus()

                    ListView2.Visible = False
                Else
                    BersihSebagian()
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub listview1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Listview1.DoubleClick
        If Listview1.Items.Count = 0 Then Exit Sub
        Get_Isi_Listview(Listview1.FocusedItem.Index)

        Dim hrgbeli As String = HilangkanTanda(LvHrg)

        SD_Ubah_Harga.TextBox2.Text = Listview1.FocusedItem.Index
        SD_Ubah_Harga.TextBox1.Text = hrgbeli
        SD_Ubah_Harga.ShowDialog()

        'If TextBox7.Text.Trim.Length = 0 Then
        '    Get_Isi_Listview(Listview1.FocusedItem.Index)

        '    Dim kb As String = LvKB
        '    Dim nm As String = LvNm
        '    Dim hrgbeli As String = HilangkanTanda(LvHrg)

        '    Edit_Barang_Pembelian.TextBox1.Text = Listview1.FocusedItem.Index
        '    Edit_Barang_Pembelian.kd.Text = kb
        '    Edit_Barang_Pembelian.nm.Text = nm
        '    Edit_Barang_Pembelian.DateTimePicker3.Text = Now
        '    Edit_Barang_Pembelian.hrg.Text = hrgbeli
        '    Edit_Barang_Pembelian.DateTimePicker3.Focus()

        '    Edit_Barang_Pembelian.ShowDialog()
        'End If

        'listview1.Items(0).SubItems(IndexExpire).Text = Format(DateTimePicker3.Value, "dd MMM yyyy")

        'If TextBox7.Text.Trim.Length = 0 Then

        '    If listview1.Items.Count = 0 Then Exit Sub

        '    ComboBox4.Text = listview1.FocusedItem.Text
        '    kd.Text = listview1.FocusedItem.SubItems(1).Text
        '    kd_Leave(listview1, e)

        '    Dim cari As Integer = InStr(listview1.FocusedItem.SubItems(2).Text, "[")
        '    Dim keterangan As String

        '    If cari = 0 Then
        '        keterangan = ""
        '    Else
        '        keterangan = Replace(Strings.Mid(listview1.FocusedItem.SubItems(2).Text, cari + 1), "]", "")
        '    End If
        '    If TextBox4.Enabled = True Then
        '        TextBox4.Text = listview1.FocusedItem.SubItems(3).Text
        '    Else
        '        TextBox4.Text = ""
        '    End If
        '    ket.Text = keterangan
        '    ket_Leave(listview1, e)
        '    If Strings.Right(listview1.FocusedItem.SubItems(6).Text, 3) = ".00" Then
        '        jml.Text = HilangkanTanda(Format(Val(HilangkanTanda(listview1.FocusedItem.SubItems(6).Text)), "N0"))
        '    Else
        '        jml.Text = HilangkanTanda(listview1.FocusedItem.SubItems(6).Text)
        '    End If
        '    If listview1.FocusedItem.SubItems(9).Text = 0 Then
        '        RadioButton1.Checked = True
        '        disc.Text = listview1.FocusedItem.SubItems(8).Text
        '    Else
        '        RadioButton2.Checked = True
        '        disc.Text = HilangkanTanda(listview1.FocusedItem.SubItems(9).Text)
        '    End If
        '    If cari = 0 Then
        '        ket.Focus()
        '    Else
        '        hrg.Focus()
        '    End If
        '    hrg.Text = HilangkanTanda(listview1.FocusedItem.SubItems(5).Text)

        '    listview1.FocusedItem.Remove()
        '    HitungGrandTotal()

        'End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Listview1.Items.Count = 0 Then ComboBox4.Focus() : Exit Sub
        GetTime()

        If TxtFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show("Faktur harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtFaktur.Focus()
            Exit Sub
        ElseIf TextBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("No nota harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox2.Focus()
            Exit Sub
            'ElseIf Format(DateTimePicker1.Value, "yyyyMM") <> Format(Tanggal_Sekarang, "yyyyMM") Then
            '    MessageBox.Show("Pembelian tidak boleh dibulan mundur!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    DateTimePicker1.Focus()
            '    Exit Sub
        ElseIf Format(DateTimePicker1.Value, "yyyy-MM-dd") > Format(Tanggal_Sekarang, "yyyy-MM-dd") Then
            MessageBox.Show("Pembelian tidak boleh lebih dari tanggal hari ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DateTimePicker1.Focus()
            Exit Sub
        ElseIf TextBox10.Text.Trim.Length = 0 Then
            MessageBox.Show("Supplier harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox10.Focus()
            Exit Sub
        ElseIf ComboBox2.SelectedIndex = -1 Then
            MessageBox.Show("Pembayaran harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus()
            Exit Sub
        ElseIf TextBox8.Text.Trim.Length = 0 Then
            MessageBox.Show("No PO harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox8.Focus()
            Exit Sub
            'ElseIf LvBM.Items.Count = 0 Then
            '    MessageBox.Show("No barang_proyek masuk harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    TxtNoBM.Focus()
            '    Exit Sub
        End If

        If ComboBox2.SelectedIndex = 1 Then
            If Format(DateTimePicker2.Value, "yyyy-MM-dd") < Format(DateTimePicker1.Value, "yyyy-MM-dd") Then
                MessageBox.Show("Tanggal jatuh tempo tidak boleh kurang dari tanggal sekarang.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                DateTimePicker2.Focus()
                Exit Sub
            End If
        End If

        If ComboBox2.SelectedIndex = 0 Then 'tunai
            If ComboBoxCb1.SelectedIndex = -1 Then
                MessageBox.Show("Cara bayar harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBox2.Focus()
                Exit Sub
            ElseIf ComboBoxCb1.SelectedIndex = 0 Then
                MessageBox.Show("Cara bayar harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBox2.Focus()
                Exit Sub
            End If
        End If

        If discz.Text.Trim.Length = 0 Then discz.Text = 0
        If Discx.Text.Trim.Length = 0 Then Discx.Text = 0

        get_jam()

        If Button2.Text.ToUpper = "&SIMPAN" Then

            Try
                OpenConn()
                OpenConnMySQL()
                Cmd.Transaction = Cn.BeginTransaction
                CmdMySQL.Transaction = CnMySQL.BeginTransaction

                If CekSudahTutupSaldo(tgl_skg) = "Y" Then
                    CloseTransMySQL()
                    CloseConnMySQL()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Sudah tutup saldo di bulan ini.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                Dim TJT As String = ""
                If ComboBox2.SelectedIndex = 0 Then 'Tunai
                    TJT = "NULL"
                Else
                    TJT = "'" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "'"
                End If

                Dim diskon1 As Double = 0
                Dim diskon2 As Double = 0
                If CheckBox1.Checked = True Then
                    diskon1 = discz.Text
                Else
                    diskon1 = 0
                End If

                If CheckBox2.Checked = True Then
                    diskon2 = Discx.Text
                Else
                    diskon2 = 0
                End If

                get_no_faktur("T")

                sayTerbilang = General_Class.SayRupiah(HilangkanTanda(TxtTotal.Text))

                Dim cb As String = ""
                If ComboBox2.SelectedIndex = 0 Then 'tunai
                    cb = "'" & arrCrByr.Item(ComboBoxCb1.SelectedIndex) & "'"
                Else
                    cb = "NULL"
                End If

                Dim simpan_sementara As Integer = 0



                Dim tampung_rv As Integer = 0
                Dim no_faktur_sementara_di_po As String = ""
                Dim id_rencana As Integer = 0

                SQL = "select status, flag_pakai, cast(a.rv as bigint) as rvx, count(b.kode_perusahaan) as total_baris from "
                SQL = SQL & "Barang_Masuk_Proyek a, Barang_Masuk_Proyek_detail b where "
                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur and "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "a.no_faktur = '" & TextBox8.Text.Trim & "' "
                SQL = SQL & "group by status, flag_pakai, rv"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        tampung_rv = dr("rvx")
                        'no_faktur_sementara_di_po = General_Class.CekNULL(dr("no_faktur_sementara"))
                        'id_rencana = General_Class.CekZERO(dr("id_rencana"))

                        If General_Class.CekNULL(dr("status")) = "Y" Then
                            CloseTransMySQL()
                            CloseConnMySQL()
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(dr("flag_pakai")) = "Y" Then
                            CloseTransMySQL()
                            CloseConnMySQL()
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini sudah dipakai di pembelian sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf dr("total_baris") <> Listview1.Items.Count Then
                            CloseTransMySQL()
                            CloseConnMySQL()
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena total barang_proyek berbeda dengan total barang_proyek di PO!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Else
                        CloseTransMySQL()
                        CloseConnMySQL()
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("No PO pembelian tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                'awal coding stenly
                Dim flag_opm As String = ""

                SQL = "select flag_opname,buka_pembelian from stock_owner_proyek "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & ComboBox4.Text & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Dr("flag_opname") = "Y" Then
                            If Dr("buka_pembelian") = 0 Then
                                CloseTransMySQL()
                                CloseConnMySQL()
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show(err_msg_opname, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf Dr("buka_pembelian") > 0 Then
                                Dr.Close()
                                SQL = "update stock_owner_proyek set buka_pembelian = buka_pembelian - 1 "
                                SQL = SQL & "where kode_perusahaan ='" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_stock_owner = '" & ComboBox4.Text & "'"
                                ExecuteTrans(SQL)

                                flag_opm = "'Y'"
                            Else
                                CloseTransMySQL()
                                CloseConnMySQL()
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terjadi kesalahan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            flag_opm = "NULL"
                        End If
                    Else
                        CloseTransMySQL()
                        CloseConnMySQL()
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                End Using

                'akhir coding stenly

                If TextBox7.Text.Trim.Length = 0 Then
                    If tampung_rv <> RVPO Then
                        CloseTransMySQL()
                        CloseConnMySQL()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("PO pembelian sudah diedit sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    If no_faktur_sementara_di_po.ToUpper <> TextBox7.Text.Trim.ToUpper Then
                        CloseTransMySQL()
                        CloseConnMySQL()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("No PO ini sedang dipakai sementara!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If


                If simpan_sementara = 0 Then
                    Get_Data_Acc_Proyek()
                    Dim Kode_Voucher As String = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), fJU & arrInisialFaktur(ComboBox4.SelectedIndex), KodePerusahaan)

                    Dim no_sementara As String = ""
                    If TextBox7.Text.Trim.Length = 0 Then
                        no_sementara = "NULL"
                    Else
                        no_sementara = "'" & TextBox7.Text.Trim & "'"

                        SQL = "select status, validasi, pakai from pembelian_sementara where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "no_faktur = '" & TextBox7.Text.Trim & "'"
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then
                                If General_Class.CekNULL(dr("status")) = "Y" Then
                                    CloseTransMySQL()
                                    CloseConnMySQL()
                                    dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                ElseIf General_Class.CekNULL(dr("validasi")) = "" Then
                                    CloseTransMySQL()
                                    CloseConnMySQL()
                                    dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini belum divalidasi sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                ElseIf General_Class.CekNULL(dr("pakai")) = "Y" Then
                                    CloseTransMySQL()
                                    CloseConnMySQL()
                                    dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini sudah dipakai di pembelian sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                End If
                            Else
                                CloseTransMySQL()
                                CloseConnMySQL()
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("No pembelian sementara tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using
                    End If

                    SQL = "update Barang_Masuk_Proyek set flag_pakai = 'Y', no_fak_pembelian = '" & TxtFaktur.Text.Trim & "' "
                    If id_rencana <> 0 Then
                        SQL = SQL & ", id_rencana = '" & id_rencana & "' "
                    End If
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_faktur = '" & TextBox8.Text.Trim & "'"
                    ExecuteTrans(SQL)

                    '---------------------------------------------- insert ke Mysql
                    SQLMySQL = "update barang_masuk_proyek set flag_pakai = 'Y', no_fak_pembelian = '" & TxtFaktur.Text.Trim & "' "
                    SQLMySQL = SQLMySQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQLMySQL = SQLMySQL & "no_faktur = '" & TextBox8.Text.Trim & "'"
                    ExecuteTransMySQL(SQLMySQL)


                    'If id_rencana <> 0 Then
                    '    SQL = "update rencana_order set selesai = 'Y' where kode_perusahaan = '" & KodePerusahaan & "' and id_rencana = '" & id_rencana & "' "
                    '    ExecuteTrans(SQL)
                    'End If

                    '===================================================================================
                    ' CODING TAMBAHAN BY : HERI
                    '===================================================================================

                    'INI CODING KALAU DIBUTUHKAN ADA UPDATE DI BUTTON SIMPAN

                    'SQL = "Select no_faktur_bm from pembelian_bm "
                    'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "no_faktur_beli = '" & TxtFaktur.Text & "'"
                    'Using DSBeliBM = BindingTrans(SQL)
                    '    If DSBeliBM.Tables("MyTable").Rows.Count <> 0 Then
                    '        For xx As Integer = 0 To DSBeliBM.Tables("MyTable").Rows.Count - 1
                    '            SQL = "update barang_masuk set pakai = NULL, no_fak_pembelian = NULL "
                    '            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                    '            SQL = SQL & "no_faktur = '" & DSBeliBM.Tables("MyTable").Rows(xx).Item("no_faktur_bm") & "'"
                    '            ExecuteTrans(SQL)
                    '        Next
                    '    End If
                    'End Using

                    'SQL = "Delete From Pembelian_BM "
                    'SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "no_faktur_beli = '" & TxtFaktur.Text & "'"
                    'ExecuteTrans(SQL)

                    'For i As Integer = 0 To LvBM.Items.Count - 1
                    '    SQL = "update barang_masuk set pakai = 'Y', no_fak_pembelian = '" & TxtFaktur.Text.Trim & "' "
                    '    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                    '    SQL = SQL & "no_faktur = '" & LvBM.Items(i).Text & "'"
                    '    ExecuteTrans(SQL)

                    '    SQL = "Insert Into Pembelian_BM (kode_perusahaan,no_faktur_beli,no_faktur_bm)"
                    '    SQL = SQL & "Values('" & KodePerusahaan & "','" & TxtFaktur.Text & "','" & LvBM.Items(i).Text & "')"
                    '    ExecuteTrans(SQL)
                    'Next
                    '===================================================================================

                    Dim no_so As String = ""
                    If TextBox12.Text.Trim.Length = 0 Then
                        no_so = "NULL"
                    Else
                        no_so = "'" & TextBox12.Text.Trim & "'"
                    End If

                    SQL = "insert into pembelian_proyek(Kode_perusahaan,no_faktur,Tanggal,jam,kode_supplier,"
                    SQL = SQL & "Jenis_transaksi,Tgl_Jatuh_tempo,UserId,disc1,disc2,no_nota,terbilang, "
                    SQL = SQL & "grand, kode_voucher, lokasi, kode_cb, ppn, nilai_ppn, nilai_sblm_ppn, "
                    SQL = SQL & "no_sementara, no_po, no_so, x_termz, flag_opm,Tanggal_Pembelian) "
                    SQL = SQL & "values ('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & Format(tgl_skg, "HH:mm") & "', '" & TextBox10.Text.Trim & "', '" & Strings.Left(ComboBox2.Text, 1) & "', "
                    SQL = SQL & "" & TJT & ", '" & UserID & "', " & diskon1 & ", " & diskon2 & ", '" & TextBox2.Text.Trim & "', '" & sayTerbilang & "', "
                    SQL = SQL & "" & HilangkanTanda(TxtTotal.Text) & ", '" & Kode_Voucher & "', '" & ComboBox4.Text & "', " & cb & ", "
                    SQL = SQL & "'" & TextBox3.Text & "', '" & HilangkanTanda(TextBox5.Text) & "', '" & HilangkanTanda(TextBox6.Text) & "', "
                    SQL = SQL & "" & no_sementara & ", '" & TextBox8.Text.Trim & "', " & no_so & ", 'x', " & flag_opm & ", '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' )"
                    ExecuteTrans(SQL)

                    Dim kategori_bsr1 As String = ""
                    For i As Integer = 0 To Listview1.Items.Count - 1
                        Get_Isi_Listview(i)
                        SQL = "select b.Jenis_Kategori from Barang_Proyek a, Kategori_Besar_Proyek b where "
                        SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and "
                        SQL = SQL & "a.Kode_Kategori_Besar = b.Kode_Kategori_Besar and "
                        SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "a.Kode_stock_owner = '" & LvSO & "' and "
                        SQL = SQL & "a.Kode_Barang = '" & LvKB & "' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                If i = 0 Then
                                    kategori_bsr1 = Dr("Jenis_Kategori")
                                End If

                                If kategori_bsr1 <> Dr("Jenis_Kategori") Then
                                    CloseTransMySQL()
                                    CloseConnMySQL()
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("kategori barang berbeda", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    DateTimePicker3.Focus()
                                    Exit Sub
                                End If
                            End If
                        End Using
                    Next

                    Dim xpph As Double = 0
                    Dim xpph2 As Double = 0
                    If kategori_bsr1.ToUpper = "JASA" Then
                        SQL = "select b.Kode_Tarif,b.Persentase, b.flag_ppn, b.kode_akun from "
                        SQL = SQL & "Barang_Masuk_Proyek a, Detail_PO_Pembelian_Proyek_PPH b, PO_Pembelian_Proyek c "
                        SQL = SQL & "where a.No_PO = c.No_Faktur and a.Status is null and c.Status is null and a.Kode_Perusahaan = c.Kode_Perusahaan "
                        SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.flag_ppn is null "
                        SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & TextBox8.Text & "' "
                        Using Ds = BindingTrans(SQL)
                            With Ds.Tables("MyTable")
                                If .Rows.Count <> 0 Then
                                    For index As Integer = 0 To .Rows.Count - 1
                                        If .Rows(index).Item("Persentase") <> 0 Then
                                            xpph = (Val(HilangkanTanda(TextBox6.Text)) * .Rows(index).Item("Persentase") / 100)
                                            xpph2 = (Val(HilangkanTanda(Format(xpph, "N0"))))

                                            SQL = "insert into Detail_Pembelian_Proyek_PPH (Kode_Perusahaan,No_Faktur,Persentase,Nilai,Kode_Tarif,kode_akun) "
                                            SQL = SQL & "values ('" & KodePerusahaan & "','" & TxtFaktur.Text & "','" & .Rows(index).Item("Persentase") & "',"
                                            SQL = SQL & "'" & xpph2 & "','" & .Rows(index).Item("Kode_Tarif") & "', '" & .Rows(index).Item("kode_akun") & "')"
                                            ExecuteTrans(SQL)
                                        End If
                                    Next
                                End If
                            End With
                        End Using
                    End If

                    Dim xppn As Double = 0
                    Dim xppn2 As Double = 0
                    If Val(HilangkanTanda(TextBox3.Text)) <> 0 Then
                        SQL = "select b.Kode_Tarif,b.Persentase, b.flag_ppn, b.kode_akun from "
                        SQL = SQL & "Barang_Masuk_Proyek a, Detail_PO_Pembelian_Proyek_PPH b, PO_Pembelian_Proyek c "
                        SQL = SQL & "where a.No_PO = c.No_Faktur and a.Status is null and c.Status is null and a.Kode_Perusahaan = c.Kode_Perusahaan "
                        SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.flag_ppn = 'Y' "
                        SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & TextBox8.Text & "' "
                        Using Ds = BindingTrans(SQL)
                            With Ds.Tables("MyTable")
                                If .Rows.Count <> 0 Then
                                    For index As Integer = 0 To .Rows.Count - 1
                                        xppn = (Val(HilangkanTanda(TextBox6.Text)) * .Rows(index).Item("Persentase") / 100)
                                        xppn2 = (Val(HilangkanTanda(Format(xppn, "N0"))))

                                        SQL = "insert into Detail_Pembelian_Proyek_PPH (Kode_Perusahaan,No_Faktur,Persentase,Nilai,Kode_Tarif,kode_akun,flag_ppn) "
                                        SQL = SQL & "values ('" & KodePerusahaan & "','" & TxtFaktur.Text & "','" & .Rows(index).Item("Persentase") & "',"
                                        SQL = SQL & "'" & xppn2 & "','" & .Rows(index).Item("Kode_Tarif") & "', '" & .Rows(index).Item("kode_akun") & "','Y')"
                                        ExecuteTrans(SQL)

                                    Next
                                End If
                            End With
                        End Using
                    End If

                    Dim tot_qty As Double = 0
                    For n As Integer = 0 To Listview1.Items.Count - 1
                        tot_qty = tot_qty + Val(HilangkanTanda(LvJml))
                    Next

                    Dim x As Integer = 1
                    'For j As Integer = 0 To Listview1.Items.Count - 1
                    '    Get_Isi_Listview(j)
                    '    SQL = "select Kode_Kategori_Besar from Barang_Proyek where "
                    '    SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                    '    SQL = SQL & "Kode_stock_owner = '" & LvSO & "' and "
                    '    SQL = SQL & "Kode_Barang = '" & LvKB & "' "
                    '    Using Dr = OpenTrans(SQL)
                    '        Do While Dr.Read
                    '            If j = 0 Then
                    '                kategori_bsr = Dr("Kode_Kategori_Besar")
                    '            End If

                    '            If kategori_bsr <> Dr("Kode_Kategori_Besar") Then
                    '                CloseTrans()
                    '                CloseConn()
                    '                MessageBox.Show("kategori barang berbeda", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                DateTimePicker3.Focus()
                    '                Exit Sub
                    '            End If
                    '        Loop
                    '    End Using
                    'Next

                    Dim kategori_bsr As String = ""
                    For i As Integer = 0 To Listview1.Items.Count - 1
                        Get_Isi_Listview(i)

                        If Format(CDate(LvExpire), "yyyy-MM-dd") = "1900-01-01" Then
                            CloseTransMySQL()
                            CloseConnMySQL()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Tanggal expire lebih kecil dari tanggal pembelian.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            DateTimePicker3.Focus()
                            Exit Sub
                        ElseIf Format(CDate(LvExpire), "yyyy-MM-dd") < Format(DateTimePicker1.Value, "yyyy-MM-dd") Then
                            CloseTransMySQL()
                            CloseConnMySQL()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Tanggal expire lebih kecil dari tanggal pembelian.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            DateTimePicker3.Focus()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Day, DateTimePicker1.Value, CDate(LvExpire)) < 365 Then
                            CloseTransMySQL()
                            CloseConnMySQL()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Tanggal expire minimal harus 365 hari", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            DateTimePicker3.Focus()
                            Exit Sub
                        End If

                        SQL = "select b.Jenis_Kategori from Barang_Proyek a, Kategori_Besar_Proyek b where "
                        SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and "
                        SQL = SQL & "a.Kode_Kategori_Besar = b.Kode_Kategori_Besar and "
                        SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "a.Kode_stock_owner = '" & LvSO & "' and "
                        SQL = SQL & "a.Kode_Barang = '" & LvKB & "' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                If i = 0 Then
                                    kategori_bsr = Dr("Jenis_Kategori")
                                End If

                                If kategori_bsr <> Dr("Jenis_Kategori") Then
                                    CloseTransMySQL()
                                    CloseConnMySQL()
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("kategori barang berbeda", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    DateTimePicker3.Focus()
                                    Exit Sub
                                End If
                            End If
                        End Using


                        Dim cari As Integer = InStr(LvNm, "[")
                        Dim keterangan As String

                        If cari = 0 Then
                            keterangan = "NULL"
                        Else
                            keterangan = "'" & Strings.Mid(LvNm, cari) & "'"
                        End If

                        Dim xserial As String = ""
                        If LvPakaiSN = "Y" Then
                            xserial = "'" & LvSerialNumber & "'"
                        Else
                            xserial = "NULL"
                        End If


                        Dim tambahan_modal_1 As Double = 0
                        Dim tambahan_modal_2 As Double = 0

                        tambahan_modal_1 = Val(HilangkanTanda(LvModal)) * Val(discz.Text) / 100
                        tambahan_modal_2 = Discx.Text / tot_qty

                        tambahan_modal_1 = HilangkanTanda(Format(tambahan_modal_1, "N0"))
                        tambahan_modal_2 = HilangkanTanda(Format(tambahan_modal_2, "N0"))



                        'Dim SN As String = LvSerialNumber & Tanda_SN & "01" & Tanda_SN & Val(HilangkanTanda(LvModal)) - tambahan_modal_1 - tambahan_modal_2 & Tanda_SN & "02" & Tanda_SN & Format(DateTimePicker1.Value, "yyyy-MM-dd")

                        'Insert data to detail_pembelian_proyek
                        SQL = "insert into detail_pembelian_proyek (kode_perusahaan, no_faktur, kode_stock_owner, kode_barang, "
                        SQL = SQL & "serial_number, keterangan, jumlah, harga, persen_diskon, nilai_diskon, x, "
                        SQL = SQL & "HB_Baru, HJ_Baru, Flag_hb, flag_hj, hb_lama, hj_lama, userid_ubah, pakai_sn, expire, subtotal) values ('" & KodePerusahaan & "', "
                        SQL = SQL & "'" & TxtFaktur.Text.Trim & "', '" & LvSO & "',"
                        SQL = SQL & "'" & LvKB & "', NULL, " & keterangan & ", "
                        SQL = SQL & "'" & HilangkanTanda(LvJml) & "', "
                        SQL = SQL & "'" & HilangkanTanda(LvHrg) & "', "
                        SQL = SQL & "" & HilangkanTanda(LvDiscp) & ", "
                        SQL = SQL & "" & HilangkanTanda(LvDiscrp) & ", " & x & ", "
                        SQL = SQL & "" & HilangkanTanda(LvHrg) & ", 0, "
                        SQL = SQL & "'T', 'T', "
                        SQL = SQL & "" & HilangkanTanda(LvHrg) & ", 0, "
                        SQL = SQL & "'" & UserID & "', '" & LvPakaiSN & "', '" & Format(CDate(LvExpire), "yyyy-MM-dd") & "', '" & HilangkanTanda(LvSubttl) & "')"
                        ExecuteTrans(SQL)

                        ''Get last inserted data from rencana_order
                        'Dim urut_rencana_order As Integer = 0
                        'SQL = "select IDENT_CURRENT('rencana_order') as urut"
                        'Using Dr1 = OpenTrans(SQL)
                        '    If Dr1.Read Then
                        '        urut_rencana_order = Dr1("urut")
                        '    End If
                        'End Using

                        'Get last inserted data from detail_pembelian_proyek
                        Dim urut_detail_pembelian_proyek As Integer = 0
                        SQL = "select IDENT_CURRENT('detail_pembelian_proyek') as urut"
                        Using Dr1 = OpenTrans(SQL)
                            If Dr1.Read Then
                                urut_detail_pembelian_proyek = Dr1("urut")
                            End If
                        End Using


                        SQL = "select f.id as ID_Proyek, f.keterangan as Proyek, e.id as Id_Sub_Proyek, e.keterangan as Sub_Proyek, "
                        SQL = SQL & "c.no_faktur as No_Request, h.no_faktur as Pembelian, a.no_faktur as Barang_Masuk, g.no_faktur as PO, "
                        SQL = SQL & "b.kode_barang, b.kode_stock_owner, sum(b.Jumlah) as jumlah, b.Urut_Detail_BM from "

                        SQL = SQL & "barang_masuk_proyek a, barang_masuk_proyek_det b, request_material c, "
                        SQL = SQL & "request_material_detail d, web_subproyeks e, web_Proyeks f, Detail_PO_Pembelian_Proyek g, Pembelian_Proyek h "


                        SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.no_faktur=b.no_faktur and a.status is null "
                        SQL = SQL & "and b.urut_detail_request=d.id and d.no_faktur=c.No_faktur and c.status is null "
                        SQL = SQL & "and c.Sub_Proyek_ID=e.id and e.proyek_id=f.id and g.Kode_Perusahaan = b.Kode_Perusahaan and g.No_Urut = b.Urut_Detail_PO "
                        SQL = SQL & "and a.no_fak_pembelian = h.No_Faktur and a.Kode_Perusahaan=h.Kode_Perusahaan and h.Status is null  "
                        SQL = SQL & "and a.no_faktur='" & TextBox8.Text.Trim & "' and Urut_Detail_BM='" & LvUrutBarangMasuk & "' "
                        SQL = SQL & "and b.kode_barang='" & LvKB & "' and b.Kode_Stock_Owner='" & LvSO & "' "
                        SQL = SQL & "group by c.no_faktur, h.no_faktur, e.id,e.keterangan, f.id,f.keterangan,a.no_faktur, g.no_faktur, b.Urut_Detail_BM "
                        SQL = SQL & ",b.kode_barang, b.kode_stock_owner "
                        Using Ds = BindingTrans(SQL)
                            With Ds.Tables("MyTable")
                                If .Rows.Count <> 0 Then
                                    For index As Integer = 0 To .Rows.Count - 1
                                        If LvPakaiSN = "Y" Then
                                            Dim Rand As New Random

                                            Dim str As String = Format(Rand.Next(0, 999), "000") & Format(DateTimePicker1.Value, "HHmmss")
                                            Dim Kode_Unik As String = str.Substring(0, 5) & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)

                                            Dim SN As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & Val(HilangkanTanda(LvModal)) - tambahan_modal_1 - tambahan_modal_2 & Tanda_SN & "02" & Tanda_SN & Format(DateTimePicker1.Value, "yyyy-MM-dd")


                                            SQL = "select kode_barang from barang_sn_proyek where "
                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                                            SQL = SQL & "kode_barang = '" & LvKB & "' and serial_number = '" & SN & "'"
                                            Using Dr = OpenTrans(SQL)
                                                If Dr.Read Then
                                                    Dr.Close()
                                                    CloseTransMySQL()
                                                    CloseConnMySQL()
                                                    CloseTrans()
                                                    CloseConn()
                                                    MessageBox.Show("Terjadi kesalahan pada Barang . . !, Silahkan Ulangi Transaksi . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                    'SQL = "Update barang_sn_proyek set jumlah = jumlah + " & HilangkanTanda(LvJml) & " where "
                                                    'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                    'SQL = SQL & "kode_stock_owner = '" & LvSO & "' and kode_barang = '" & LvKB & "' and "
                                                    'SQL = SQL & "serial_number = '" & SN & "'"
                                                    'ExecuteTrans(SQL)
                                                Else

                                                    SQL = "insert into barang_sn_proyek(kode_perusahaan, kode_stock_owner, kode_barang, "
                                                    SQL = SQL & "serial_number, jumlah, Kode_Proyek, Kode_Sub_Proyek, No_Faktur_Req_Material, No_Faktur_Pembelian, "
                                                    SQL = SQL & "No_Faktur_BM, No_Faktur_PO) "
                                                    SQL = SQL & "values('" & KodePerusahaan & "', "
                                                    SQL = SQL & "'" & LvSO & "', '" & LvKB & "', "
                                                    SQL = SQL & "'" & SN & "', '" & HilangkanTanda(.Rows(index).Item("jumlah").ToString) & "', "
                                                    SQL = SQL & "'" & .Rows(index).Item("ID_Proyek").ToString & "', '" & .Rows(index).Item("Id_Sub_Proyek").ToString & "', "
                                                    SQL = SQL & "'" & .Rows(index).Item("No_Request").ToString & "', '" & .Rows(index).Item("Pembelian").ToString & "', "
                                                    SQL = SQL & "'" & .Rows(index).Item("Barang_Masuk").ToString & "', '" & .Rows(index).Item("PO").ToString & "')"
                                                    Dr.Close()
                                                    ExecuteTrans(SQL)

                                                    'insert to det_Pembelian_Proyek
                                                    SQL = "insert into det_Pembelian_Proyek (No_Faktur, ID_Proyek, ID_Sub_Proyek, Kode_Stock_Owner, No_Request, PO, Kode_Barang, "
                                                    SQL = SQL & "Barang_Masuk, Serial_Number, Jumlah, Urut_Detail_BM, Urut_Pembelian_Proyek) Values "
                                                    SQL = SQL & "('" & .Rows(index).Item("Pembelian").ToString & "', '" & .Rows(index).Item("ID_Proyek").ToString & "', "
                                                    SQL = SQL & "'" & .Rows(index).Item("Id_Sub_Proyek").ToString & "', '" & .Rows(index).Item("kode_stock_owner").ToString & "', "
                                                    SQL = SQL & "'" & .Rows(index).Item("No_Request").ToString & "', '" & .Rows(index).Item("PO").ToString & "', "
                                                    SQL = SQL & "'" & .Rows(index).Item("kode_barang").ToString & "', '" & .Rows(index).Item("Barang_Masuk").ToString & "', "
                                                    SQL = SQL & "'" & SN & "', '" & .Rows(index).Item("jumlah").ToString & "', '" & .Rows(index).Item("Urut_Detail_BM").ToString & "', "
                                                    SQL = SQL & "'" & urut_detail_pembelian_proyek.ToString & "')"

                                                    Dr.Close()
                                                    ExecuteTrans(SQL)
                                                End If
                                            End Using
                                        End If
                                    Next
                                Else
                                    CloseTransMySQL()
                                    CloseConnMySQL()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data Tidak di Temukan . . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End With
                        End Using

                        'If LvPakaiSN = "Y" Then
                        '    SQL = "select kode_barang from barang_sn_proyek where "
                        '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        '    SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                        '    SQL = SQL & "kode_barang = '" & LvKB & "' and serial_number = '" & SN & "'"
                        '    Using Dr = OpenTrans(SQL)
                        '        If Dr.Read Then
                        '            Dr.Close()
                        '            CloseTransMySQL()
                        '            CloseConnMySQL()
                        '            CloseTrans()
                        '            CloseConn()
                        '            MessageBox.Show("Terjadi kesalahan pada Barang . . !, Silahkan Ulangi Transaksi . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '            Exit Sub
                        '            'SQL = "Update barang_sn_proyek set jumlah = jumlah + " & HilangkanTanda(LvJml) & " where "
                        '            'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        '            'SQL = SQL & "kode_stock_owner = '" & LvSO & "' and kode_barang = '" & LvKB & "' and "
                        '            'SQL = SQL & "serial_number = '" & SN & "'"
                        '            'ExecuteTrans(SQL)
                        '        Else

                        '            SQL = "insert into barang_sn_proyek(kode_perusahaan, kode_stock_owner, kode_barang, "
                        '            SQL = SQL & "serial_number, jumlah) values('" & KodePerusahaan & "', "
                        '            SQL = SQL & "'" & LvSO & "', '" & LvKB & "', "
                        '            SQL = SQL & "'" & SN & "', " & HilangkanTanda(LvJml) & ")"
                        '            Dr.Close()
                        '            ExecuteTrans(SQL)

                        '            'insert ke det

                        '        End If
                        '    End Using
                        'End If

                        Dim ppn_ga As String = ""
                        If CheckBox3.Checked = True Then
                            ppn_ga = "Y"
                        Else
                            ppn_ga = "T"
                        End If

                        'SQL = "select kode_barang, flag_ppn from barang_proyek where "
                        'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & LvSO & "' and "
                        'SQL = SQL & "kode_barang = '" & LvKB & "'"
                        'Using Dr = OpenTrans(SQL)
                        '    If Dr.Read Then
                        '        If ppn_ga <> Dr("flag_ppn") Then
                        '            Dr.Close()
                        '            CloseTrans()
                        '            CloseConn()
                        '            MessageBox.Show("Flag PPN " & LvNm & " berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '            Exit Sub
                        '        End If
                        '    Else
                        '        Dr.Close()
                        '        CloseTrans()
                        '        CloseConn()
                        '        MessageBox.Show("Data brg tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '        Exit Sub
                        '    End If
                        'End Using

                        'SQL = "insert into detail_pembelian_proyek (kode_perusahaan, no_faktur, kode_stock_owner, kode_barang, "
                        'SQL = SQL & "serial_number, keterangan, jumlah, harga, persen_diskon, nilai_diskon, x, "
                        'SQL = SQL & "HB_Baru, HJ_Baru, Flag_hb, flag_hj, hb_lama, hj_lama, userid_ubah, pakai_sn, expire, subtotal) values ('" & KodePerusahaan & "', "
                        'SQL = SQL & "'" & TxtFaktur.Text.Trim & "', '" & LvSO & "',"
                        'SQL = SQL & "'" & LvKB & "', NULL, " & keterangan & ", "
                        'SQL = SQL & "'" & HilangkanTanda(LvJml) & "', "
                        'SQL = SQL & "'" & HilangkanTanda(LvHrg) & "', "
                        'SQL = SQL & "" & HilangkanTanda(LvDiscp) & ", "
                        'SQL = SQL & "" & HilangkanTanda(LvDiscrp) & ", " & x & ", "
                        'SQL = SQL & "" & HilangkanTanda(LvHrg) & ", 0, "
                        'SQL = SQL & "'T', 'T', "
                        'SQL = SQL & "" & HilangkanTanda(LvHrg) & ", 0, "
                        'SQL = SQL & "'" & UserID & "', '" & LvPakaiSN & "', '" & Format(CDate(LvExpire), "yyyy-MM-dd") & "', '" & HilangkanTanda(LvSubttl) & "')"
                        'ExecuteTrans(SQL)

                        SQL = "Update barang_proyek set "
                        SQL = SQL & "good_stock = good_stock + " & HilangkanTanda(LvJml) & " "
                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_stock_owner = '" & LvSO & "' and kode_barang = '" & LvKB & "'"
                        ExecuteTrans(SQL)

                        SQL = "Update barang_proyek set "
                        SQL = SQL & "last_hpp = " & HilangkanTanda(LvHrg) & " "
                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                        SQL = SQL & "kode_barang = '" & LvKB & "'"
                        ExecuteTrans(SQL)

                        'If LvUpdateHPP = "Y" Then

                        '    Dim nilai_hrg_dist_min As String = ""
                        '    Dim nilai_hrg_dist_std As String = ""
                        '    Dim nilai_hrg_dist_max As String = ""
                        '    Dim nilai_hrg_dist_spc As String = ""
                        '    Dim nilai_hrg_dist_agency As String = ""

                        '    Dim nilai_hrg_dist_modern As String = ""
                        '    Dim nilai_hrg_dist_online As String = ""
                        '    Dim nilai_hrg_dist_mid As String = ""

                        '    Dim nilai_hrg_ritel_min As String = ""
                        '    Dim nilai_hrg_ritel_std As String = ""
                        '    Dim nilai_hrg_ritel_max As String = ""
                        '    Dim nilai_hrg_ritel_spc As String = ""

                        '    Dim nilai_hrg_resell_min As String = ""
                        '    Dim nilai_hrg_resell_std As String = ""
                        '    Dim nilai_hrg_resell_max As String = ""
                        '    Dim nilai_hrg_resell_spc As String = ""

                        '    Dim nilai_hrg_resell_distributor As String = ""

                        '    Dim pakai_ppn As String = ""

                        '    SQL = "select a.flag_ppn, b.harga_min, b.harga_std, b.harga_max, b.harga_special, b.harga_agency, b.harga_modern, b.harga_online, b.harga_mid, "
                        '    SQL = SQL & "b.harga_retail_min, b.harga_retail_std, b.harga_retail_max, b.harga_retail_special, "
                        '    SQL = SQL & "b.harga_reseller_min, b.harga_reseller_std, b.harga_reseller_max, b.harga_reseller_special, b.harga_reseller_distributor from "
                        '    SQL = SQL & "barang_proyek a, kategori_barang_proyek b where "
                        '    SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and "
                        '    SQL = SQL & "a.kode_kategori = b.kode_kategori and a.kode_perusahaan = '" & KodePerusahaan & "' and "
                        '    SQL = SQL & "a.kode_stock_owner = '" & LvSO & "' and "
                        '    SQL = SQL & "a.kode_barang = '" & LvKB & "'"
                        '    Using Dr = OpenTrans(SQL)
                        '        If Dr.Read Then
                        '            pakai_ppn = Dr("flag_ppn")

                        '            nilai_hrg_dist_min = "round(" & Val(HilangkanTanda(LvHrg)) & " + (" & Val(HilangkanTanda(LvHrg)) & ".0 * " & Dr("harga_min") & " / 100), 0)"
                        '            nilai_hrg_dist_std = "round(" & Val(HilangkanTanda(LvHrg)) & " + (" & Val(HilangkanTanda(LvHrg)) & ".0 * " & Dr("harga_std") & " / 100), 0)"
                        '            nilai_hrg_dist_max = "round(" & Val(HilangkanTanda(LvHrg)) & " + (" & Val(HilangkanTanda(LvHrg)) & ".0 * " & Dr("harga_max") & " / 100), 0)"
                        '            nilai_hrg_dist_spc = "round(" & Val(HilangkanTanda(LvHrg)) & " + (" & Val(HilangkanTanda(LvHrg)) & ".0 * " & Dr("harga_special") & " / 100), 0)"
                        '            nilai_hrg_dist_agency = "round(" & Val(HilangkanTanda(LvHrg)) & " + (" & Val(HilangkanTanda(LvHrg)) & ".0 * " & Dr("harga_agency") & " / 100), 0)"

                        '            If Dr("harga_modern") = 0 Then
                        '                nilai_hrg_dist_modern = "0"
                        '            Else
                        '                nilai_hrg_dist_modern = "round(" & Val(HilangkanTanda(LvHrg)) & " + (" & Val(HilangkanTanda(LvHrg)) & ".0 * " & Dr("harga_modern") & " / 100), 0)"
                        '            End If

                        '            If Dr("harga_online") = 0 Then
                        '                nilai_hrg_dist_online = "0"
                        '            Else
                        '                nilai_hrg_dist_online = "round(" & Val(HilangkanTanda(LvHrg)) & " + (" & Val(HilangkanTanda(LvHrg)) & ".0 * " & Dr("harga_online") & " / 100), 0)"
                        '            End If

                        '            If Dr("harga_mid") = 0 Then
                        '                nilai_hrg_dist_mid = "0"
                        '            Else
                        '                nilai_hrg_dist_mid = "round(" & Val(HilangkanTanda(LvHrg)) & " + (" & Val(HilangkanTanda(LvHrg)) & ".0 * " & Dr("harga_mid") & " / 100), 0)"
                        '            End If

                        '            nilai_hrg_ritel_min = "round(" & Val(HilangkanTanda(LvHrg)) & " + (" & Val(HilangkanTanda(LvHrg)) & ".0 * " & Dr("harga_retail_min") & " / 100), 0)"
                        '            nilai_hrg_ritel_std = "round(" & Val(HilangkanTanda(LvHrg)) & " + (" & Val(HilangkanTanda(LvHrg)) & ".0 * " & Dr("harga_retail_std") & " / 100), 0)"
                        '            nilai_hrg_ritel_max = "round(" & Val(HilangkanTanda(LvHrg)) & " + (" & Val(HilangkanTanda(LvHrg)) & ".0 * " & Dr("harga_retail_max") & " / 100), 0)"
                        '            nilai_hrg_ritel_spc = "round(" & Val(HilangkanTanda(LvHrg)) & " + (" & Val(HilangkanTanda(LvHrg)) & ".0 * " & Dr("harga_retail_special") & " / 100), 0)"

                        '            nilai_hrg_resell_min = "round(" & Val(HilangkanTanda(LvHrg)) & " + (" & Val(HilangkanTanda(LvHrg)) & ".0 * " & Dr("harga_reseller_min") & " / 100), 0)"
                        '            nilai_hrg_resell_std = "round(" & Val(HilangkanTanda(LvHrg)) & " + (" & Val(HilangkanTanda(LvHrg)) & ".0 * " & Dr("harga_reseller_std") & " / 100), 0)"
                        '            nilai_hrg_resell_max = "round(" & Val(HilangkanTanda(LvHrg)) & " + (" & Val(HilangkanTanda(LvHrg)) & ".0 * " & Dr("harga_reseller_max") & " / 100), 0)"
                        '            nilai_hrg_resell_spc = "round(" & Val(HilangkanTanda(LvHrg)) & " + (" & Val(HilangkanTanda(LvHrg)) & ".0 * " & Dr("harga_reseller_special") & " / 100), 0)"

                        '            nilai_hrg_resell_distributor = "round(" & Val(HilangkanTanda(LvHrg)) & " + (" & Val(HilangkanTanda(LvHrg)) & ".0 * " & Dr("harga_reseller_distributor") & " / 100), 0)"

                        '        Else
                        '            Dr.Close()
                        '            CloseTrans()
                        '            CloseConn()
                        '            MessageBox.Show("Kategori tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '            Exit Sub
                        '        End If
                        '    End Using

                        '    SQL = "Update a Set "
                        '    SQL = SQL & "a.harga_beli = " & HilangkanTanda(LvHrg) & ", "
                        '    SQL = SQL & "a.penentu_harga_csi = " & HilangkanTanda(LvHrg) & ", "

                        '    SQL = SQL & "a.x_hrg_min = " & nilai_hrg_dist_min & ", "
                        '    SQL = SQL & "a.harga_jual = " & nilai_hrg_dist_std & ", "
                        '    SQL = SQL & "a.harga_jual_agen = " & nilai_hrg_dist_std & ", "
                        '    SQL = SQL & "a.x_hrg_max = " & nilai_hrg_dist_max & ", "
                        '    SQL = SQL & "a.x_hrg_special = " & nilai_hrg_dist_spc & ", "
                        '    SQL = SQL & "a.x_hrg_agency = " & nilai_hrg_dist_agency & ", "

                        '    SQL = SQL & "a.x_hrg_modern = " & nilai_hrg_dist_modern & ", "
                        '    SQL = SQL & "a.x_hrg_online = " & nilai_hrg_dist_online & ", "
                        '    SQL = SQL & "a.x_hrg_mid = " & nilai_hrg_dist_mid & ", "

                        '    SQL = SQL & "a.f_hrg_ritel_min = " & nilai_hrg_ritel_min & ", "
                        '    SQL = SQL & "a.f_hrg_ritel_std = " & nilai_hrg_ritel_std & ", "
                        '    SQL = SQL & "a.f_hrg_ritel_max = " & nilai_hrg_ritel_max & ", "
                        '    SQL = SQL & "a.f_hrg_ritel_special = " & nilai_hrg_ritel_spc & ", "

                        '    SQL = SQL & "a.f_hrg_resell_min = " & nilai_hrg_resell_min & ", "
                        '    SQL = SQL & "a.f_hrg_resell_std = " & nilai_hrg_resell_std & ", "
                        '    SQL = SQL & "a.f_hrg_resell_max = " & nilai_hrg_resell_max & ", "
                        '    SQL = SQL & "a.f_hrg_resell_special = " & nilai_hrg_resell_spc & ", "

                        '    SQL = SQL & "a.f_hrg_resell_distributor = " & nilai_hrg_resell_distributor & " "

                        '    SQL = SQL & "from barang_proyek a, stock_owner_proyek b where "
                        '    SQL = SQL & "a.kode_perusahaan  = b.kode_perusahaan  and "
                        '    SQL = SQL & "a.kode_stock_owner = b.kode_stock_owner and "
                        '    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                        '    SQL = SQL & "a.kode_barang = '" & LvKB & "' and b.hrg_khusus = 'T'"
                        '    ExecuteTrans(SQL)

                        '    SQL = "Update a Set "
                        '    SQL = SQL & "a.harga_beli = " & HilangkanTanda(LvHrg) & ", "

                        '    SQL = SQL & "a.x_hrg_min = " & nilai_hrg_dist_min & ", "
                        '    SQL = SQL & "a.harga_jual = " & nilai_hrg_dist_std & ", "
                        '    SQL = SQL & "a.harga_jual_agen = " & nilai_hrg_dist_std & ", "
                        '    SQL = SQL & "a.x_hrg_max = " & nilai_hrg_dist_max & ", "
                        '    SQL = SQL & "a.x_hrg_special = " & nilai_hrg_dist_spc & ", "
                        '    SQL = SQL & "a.x_hrg_agency = " & nilai_hrg_dist_agency & ", "

                        '    SQL = SQL & "a.x_hrg_modern = " & nilai_hrg_dist_modern & ", "
                        '    SQL = SQL & "a.x_hrg_online = " & nilai_hrg_resell_spc & ", "
                        '    SQL = SQL & "a.x_hrg_mid = " & nilai_hrg_resell_max & ", "

                        '    SQL = SQL & "a.f_hrg_ritel_min = " & nilai_hrg_ritel_min & ", "
                        '    SQL = SQL & "a.f_hrg_ritel_std = " & nilai_hrg_ritel_std & ", "
                        '    SQL = SQL & "a.f_hrg_ritel_max = " & nilai_hrg_ritel_max & ", "
                        '    SQL = SQL & "a.f_hrg_ritel_special = " & nilai_hrg_ritel_spc & ", "

                        '    SQL = SQL & "a.f_hrg_resell_min = " & nilai_hrg_resell_min & ", "
                        '    SQL = SQL & "a.f_hrg_resell_std = " & nilai_hrg_resell_min & ", "
                        '    SQL = SQL & "a.f_hrg_resell_max = " & nilai_hrg_resell_max & ", "
                        '    SQL = SQL & "a.f_hrg_resell_special = " & nilai_hrg_resell_spc & ", "

                        '    SQL = SQL & "a.f_hrg_resell_distributor = " & nilai_hrg_resell_distributor & " "

                        '    SQL = SQL & "from barang_proyek a, stock_owner_proyek b where "
                        '    SQL = SQL & "a.kode_perusahaan  = b.kode_perusahaan  and "
                        '    SQL = SQL & "a.kode_stock_owner = b.kode_stock_owner and "
                        '    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                        '    SQL = SQL & "a.kode_barang = '" & LvKB & "' and b.hrg_khusus = 'Y'"
                        '    ExecuteTrans(SQL)

                        '    'SQL = "Update " & DB_Agency & "barang_proyek Set "
                        '    'SQL = SQL & "harga_beli = " & HilangkanTanda(LvHrg) & ", "

                        '    'SQL = SQL & "x_hrg_min = " & nilai_hrg_dist_min & ", "
                        '    'SQL = SQL & "harga_jual = " & nilai_hrg_dist_std & ", "
                        '    'SQL = SQL & "harga_jual_agen = " & nilai_hrg_dist_std & ", "
                        '    'SQL = SQL & "x_hrg_max = " & nilai_hrg_dist_max & ", "
                        '    'SQL = SQL & "x_hrg_special = " & nilai_hrg_dist_spc & ", "
                        '    'SQL = SQL & "x_hrg_agency = " & nilai_hrg_dist_agency & ", "

                        '    'SQL = SQL & "x_hrg_modern = " & nilai_hrg_dist_modern & ", "
                        '    'SQL = SQL & "x_hrg_online = " & nilai_hrg_dist_online & ", "
                        '    'SQL = SQL & "x_hrg_mid = " & nilai_hrg_dist_mid & ", "

                        '    'SQL = SQL & "f_hrg_ritel_min = " & nilai_hrg_ritel_min & ", "
                        '    'SQL = SQL & "f_hrg_ritel_std = " & nilai_hrg_ritel_std & ", "
                        '    'SQL = SQL & "f_hrg_ritel_max = " & nilai_hrg_ritel_max & ", "
                        '    'SQL = SQL & "f_hrg_ritel_special = " & nilai_hrg_ritel_spc & ", "

                        '    'SQL = SQL & "f_hrg_resell_min = " & nilai_hrg_resell_min & ", "
                        '    'SQL = SQL & "f_hrg_resell_std = " & nilai_hrg_resell_std & ", "
                        '    'SQL = SQL & "f_hrg_resell_max = " & nilai_hrg_resell_max & ", "
                        '    'SQL = SQL & "f_hrg_resell_special = " & nilai_hrg_resell_spc & ", "

                        '    'SQL = SQL & "f_hrg_resell_distributor = " & nilai_hrg_resell_distributor & " "

                        '    'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                        '    'SQL = SQL & "kode_barang = '" & LvKB & "'"
                        '    'ExecuteTrans(SQL)

                        '    '============
                        '    'update ke toko
                        '    '============ 

                        '    Dim x_hrg_beli_mana_ya As String = "" 'ini untuk hrg beli di toko. rumusnya hrg jual dist kalo ada ppn + 10%
                        '    'Dim x_hrg_retail_mana_ya As String = ""
                        '    'Dim x_hrg_resell_mana_ya As String = ""


                        '    SQL = "select kode_stock_owner, pakai_hrg_mana from " & DB_Tetangga & "stock_owner where "
                        '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' "
                        '    SQL = SQL & "order by kode_stock_owner"
                        '    Using Ds = BindingTrans(SQL)
                        '        With Ds.Tables("MyTable")
                        '            If .Rows.Count <> 0 Then
                        '                For mm As Integer = 0 To .Rows.Count - 1
                        '                    If .Rows(mm).Item("pakai_hrg_mana") = "MIN" Then
                        '                        If pakai_ppn = "Y" Then
                        '                            x_hrg_beli_mana_ya = nilai_hrg_dist_min & "+ (" & nilai_hrg_dist_min & " * 11 / 100)"
                        '                            'x_hrg_retail_mana_ya = nilai_hrg_ritel_min & "+ (" & nilai_hrg_ritel_min & " * 10 / 100)"
                        '                            'x_hrg_resell_mana_ya = nilai_hrg_resell_min & "+ (" & nilai_hrg_resell_min & " * 10 / 100)"
                        '                        Else
                        '                            x_hrg_beli_mana_ya = nilai_hrg_dist_min
                        '                            'x_hrg_retail_mana_ya = nilai_hrg_ritel_min
                        '                            'x_hrg_resell_mana_ya = nilai_hrg_resell_min
                        '                        End If
                        '                    ElseIf .Rows(mm).Item("pakai_hrg_mana") = "STD" Then
                        '                        If pakai_ppn = "Y" Then
                        '                            x_hrg_beli_mana_ya = nilai_hrg_dist_std & "+ (" & nilai_hrg_dist_std & " * 11 / 100)"
                        '                            'x_hrg_retail_mana_ya = nilai_hrg_ritel_std & "+ (" & nilai_hrg_ritel_std & " * 10 / 100)"
                        '                            'x_hrg_resell_mana_ya = nilai_hrg_resell_std & "+ (" & nilai_hrg_resell_std & " * 10 / 100)"
                        '                        Else
                        '                            x_hrg_beli_mana_ya = nilai_hrg_dist_std
                        '                            'x_hrg_retail_mana_ya = nilai_hrg_ritel_std
                        '                            'x_hrg_resell_mana_ya = nilai_hrg_resell_std
                        '                        End If
                        '                    ElseIf .Rows(mm).Item("pakai_hrg_mana") = "MAX" Then
                        '                        If pakai_ppn = "Y" Then
                        '                            x_hrg_beli_mana_ya = nilai_hrg_dist_max & "+ (" & nilai_hrg_dist_max & " * 11 / 100)"
                        '                            'x_hrg_retail_mana_ya = nilai_hrg_ritel_max & "+ (" & nilai_hrg_ritel_max & " * 10 / 100)"
                        '                            'x_hrg_resell_mana_ya = nilai_hrg_resell_max & "+ (" & nilai_hrg_resell_max & " * 10 / 100)"
                        '                        Else
                        '                            x_hrg_beli_mana_ya = nilai_hrg_dist_max
                        '                            'x_hrg_retail_mana_ya = nilai_hrg_ritel_max
                        '                            'x_hrg_resell_mana_ya = nilai_hrg_resell_max
                        '                        End If
                        '                    ElseIf .Rows(mm).Item("pakai_hrg_mana") = "SPC" Then
                        '                        If pakai_ppn = "Y" Then
                        '                            x_hrg_beli_mana_ya = nilai_hrg_dist_spc & "+ (" & nilai_hrg_dist_spc & " * 11 / 100)"
                        '                            'x_hrg_retail_mana_ya = nilai_hrg_ritel_spc & "+ (" & nilai_hrg_ritel_spc & " * 10 / 100)"
                        '                            'x_hrg_resell_mana_ya = nilai_hrg_resell_spc & "+ (" & nilai_hrg_resell_spc & " * 10 / 100)"
                        '                        Else
                        '                            x_hrg_beli_mana_ya = nilai_hrg_dist_spc
                        '                            'x_hrg_retail_mana_ya = nilai_hrg_ritel_spc
                        '                            'x_hrg_resell_mana_ya = nilai_hrg_resell_spc
                        '                        End If
                        '                    Else
                        '                        CloseTrans()
                        '                        CloseConn()
                        '                        MessageBox.Show("Data tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '                        Exit Sub
                        '                    End If

                        '                    SQL = "Update " & DB_Tetangga & "barang_proyek Set "
                        '                    SQL = SQL & "harga_beli = round(" & x_hrg_beli_mana_ya & ", 0) "
                        '                    'SQL = SQL & "harga_jual = round(" & x_hrg_retail_mana_ya & ", 0), "
                        '                    'SQL = SQL & "harga_jual_agen = round(" & x_hrg_retail_mana_ya & ", 0), "
                        '                    'SQL = SQL & "harga_reseller = round(" & x_hrg_resell_mana_ya & ", 0), "
                        '                    'SQL = SQL & "penentu_harga_baru = " & LvHrg & " "
                        '                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                        '                    SQL = SQL & "kode_stock_owner = '" & .Rows(mm).Item("kode_stock_owner") & "' and "
                        '                    SQL = SQL & "kode_barang = '" & LvKB & "'"
                        '                    ExecuteTrans(SQL)
                        '                Next
                        '            Else
                        '                CloseTrans()
                        '                CloseConn()
                        '                MessageBox.Show("Data Lokasi_Proyek tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '                Exit Sub
                        '            End If
                        '        End With
                        '    End Using

                        '    'Dim nilai_hrg_min As Double = Val(HilangkanTanda(Format(Val(HilangkanTanda(LvHrg)) + (Val(HilangkanTanda(LvHrg)) * persen_hrg_min / 100), "N0")))
                        '    'Dim nilai_hrg_std As Double = Val(HilangkanTanda(Format(Val(HilangkanTanda(LvHrg)) + (Val(HilangkanTanda(LvHrg)) * persen_hrg_std / 100), "N0")))
                        '    'Dim nilai_hrg_max As Double = Val(HilangkanTanda(Format(Val(HilangkanTanda(LvHrg)) + (Val(HilangkanTanda(LvHrg)) * persen_hrg_max / 100), "N0")))

                        '    'SQL = "Update barang_proyek set "
                        '    'SQL = SQL & "harga_beli = " & HilangkanTanda(LvHrg) & ", "
                        '    'SQL = SQL & "harga_jual = " & nilai_hrg_std & ", "
                        '    'SQL = SQL & "harga_jual_agen = " & nilai_hrg_std & ", "
                        '    'SQL = SQL & "x_hrg_min = " & nilai_hrg_min & ", "
                        '    'SQL = SQL & "x_hrg_max = " & nilai_hrg_max & " "
                        '    'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                        '    'SQL = SQL & "kode_barang = '" & LvKB & "'"
                        '    'ExecuteTrans(SQL)

                        'End If

                        If i = x * JmlBrg - 1 Then
                            x = x + 1
                        End If
                    Next

                    If ComboBox2.SelectedIndex = 1 Then ' Non Tunai maka piutang bertambah
                        SQL = "select kode_perusahaan from suppliers where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and kode_supplier = '" & TextBox10.Text.Trim & "'"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()

                                SQL = "update suppliers set hutang = hutang + " & HilangkanTanda(TxtTotal.Text) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_supplier = '" & TextBox10.Text.Trim & "'"
                                ExecuteTrans(SQL)
                            Else
                                CloseTransMySQL()
                                CloseConnMySQL()
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Supplier tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using
                    End If

                    'If TextBox7.Text.Trim.Length <> 0 Then
                    '    SQL = "update pembelian_sementara set pakai = 'Y', "
                    '    SQL = SQL & "no_fak_pembelian = '" & TxtFaktur.Text.Trim & "' where "
                    '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    '    SQL = SQL & "no_faktur = '" & TextBox7.Text.Trim & "'"
                    '    ExecuteTrans(SQL)
                    'End If

                    'If TextBox12.Text.Trim.Length <> 0 Then
                    '    SQL = "update permintaan_keluar_err set semua = 'Y' where "
                    '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    '    SQL = SQL & "no_faktur = '" & TextBox12.Text.Trim & "'"
                    '    ExecuteTrans(SQL)
                    'End If




                    'awal coding stenly

                    'SQL = "insert into pembelian_vs_bm(Kode_Perusahaan, No_Faktur, Kode_stock_owner, "
                    'SQL = SQL & "Kode_barang, jumlah, Jumlah_masuk)"

                    'SQL = SQL & "SELECT a.Kode_Perusahaan, a.No_Faktur, b.kode_stock_owner, b.kode_barang, "
                    'SQL = SQL & "b.jumlah, "

                    'SQL = SQL & "ISNULL("
                    'SQL = SQL & "("
                    'SQL = SQL & "SELECT SUM(y.Jumlah) "
                    'SQL = SQL & "FROM Barang_Masuk AS x INNER JOIN "
                    'SQL = SQL & "Barang_Masuk_Detail AS y ON x.Kode_Perusahaan = y.Kode_Perusahaan AND "
                    'SQL = SQL & "x.No_Faktur = y.No_Faktur "
                    'SQL = SQL & "WHERE x.Kode_Perusahaan = a.Kode_Perusahaan AND x.No_Fak_Pembelian = a.No_Faktur AND "
                    'SQL = SQL & "y.Kode_Stock_Owner = b.Kode_Stock_Owner And y.kode_barang = b.kode_barang "
                    'SQL = SQL & "),0) AS JlhMasuk "

                    'SQL = SQL & "FROM Pembelian as a INNER JOIN "
                    'SQL = SQL & "Detail_Pembelian as b ON a.Kode_Perusahaan = b.Kode_Perusahaan AND "
                    'SQL = SQL & "a.No_Faktur = b.No_Faktur INNER JOIN "
                    'SQL = SQL & "Suppliers as c ON a.Kode_Perusahaan = c.Kode_Perusahaan AND "
                    'SQL = SQL & "a.Kode_Supplier = c.Kode_Supplier INNER JOIN "
                    'SQL = SQL & "barang_proyek as d ON b.Kode_Perusahaan = d.Kode_Perusahaan AND "
                    'SQL = SQL & "b.Kode_Stock_Owner = d.Kode_Stock_Owner And "
                    'SQL = SQL & "b.kode_barang = d.kode_barang "

                    'SQL = SQL & "WHERE b.kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "b.No_Faktur = '" & TxtFaktur.Text.Trim & "'"

                    'SQL = SQL & "union all "

                    'SQL = SQL & "select a.Kode_Perusahaan, e.No_Faktur, c.Kode_Stock_Owner, "
                    'SQL = SQL & "c.Kode_Barang, 0 as jumlah, "
                    'SQL = SQL & "sum(c.jumlah) AS JlhMasuk "
                    'SQL = SQL & "from Pembelian_BM a, barang_masuk b, barang_masuk_detail c, "
                    'SQL = SQL & "suppliers d, pembelian e, barang_proyek f where "
                    'SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and "
                    'SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan and d.kode_perusahaan = e.kode_perusahaan and "
                    'SQL = SQL & "e.kode_perusahaan = f.kode_perusahaan and "
                    'SQL = SQL & "a.no_faktur_bm = b.no_faktur and b.no_faktur = c.no_faktur and "
                    'SQL = SQL & "d.kode_supplier = e.kode_supplier and e.no_faktur = a.no_faktur_beli and "
                    'SQL = SQL & "c.kode_stock_owner = f.kode_stock_owner and c.kode_barang = f.kode_barang and "
                    'SQL = SQL & "c.kode_barang in "
                    'SQL = SQL & "("
                    'SQL = SQL & "select kode_barang from Barang_Masuk_Detail x where "
                    'SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan And x.no_faktur = a.no_faktur_bm "
                    'SQL = SQL & "except "
                    'SQL = SQL & "select kode_barang from Detail_Pembelian x where "
                    'SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan And x.no_faktur = a.no_faktur_beli "
                    'SQL = SQL & ") and "

                    'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "e.No_Faktur = '" & TxtFaktur.Text.Trim & "' "

                    'SQL = SQL & "group by a.Kode_Perusahaan, e.No_Faktur, e.No_Nota, e.Tanggal, "
                    'SQL = SQL & "e.Jam, e.Kode_Supplier, e.PPN, d.Nama, d.Alamat, "
                    'SQL = SQL & "e.UserID, e.Terbilang, c.Kode_Stock_Owner, "
                    'SQL = SQL & "c.Kode_Barang, f.Nama, f.Satuan "
                    'ExecuteTrans(SQL)

                    'akhir di sini stenly
                    '==============================================

                    Get_Data_Acc_Proyek()
                    Dim pagenumber As Integer = 1

                    SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                    SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                    SQL = SQL & "'" & Kode_Voucher & "', "
                    SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                    SQL = SQL & "'" & KodeProyek & "', 'Persediaan " & TxtFaktur.Text & "', '', "
                    SQL = SQL & "'-', '" & UserID & "')"
                    ExecuteTrans(SQL)


                    Dim total As Double = 0
                    If kategori_bsr = "JASA" Then

                        SQL = "select round(sum(round(b.Jumlah * g.Harga,2)),0) as total,f.Akun_Jasa  from barang_masuk_proyek a, barang_masuk_proyek_det b, request_material c, "
                        SQL = SQL & "request_material_detail d, web_subproyeks e, web_Proyeks f, Detail_PO_Pembelian_Proyek g where "
                        SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.no_faktur=b.no_faktur and a.status is null "
                        SQL = SQL & "and b.urut_detail_request=d.id and d.no_faktur=c.No_faktur and c.status is null  "
                        SQL = SQL & "and c.Sub_Proyek_ID=e.id and e.proyek_id=f.id and g.Kode_Perusahaan = b.Kode_Perusahaan and g.No_Urut = b.Urut_Detail_PO "
                        SQL = SQL & "and  a.no_faktur = '" & TextBox8.Text & "' group by f.akun_jasa "
                        Using Ds = BindingTrans(SQL)

                            With Ds.Tables("MyTable")
                                If .Rows.Count <> 0 Then
                                    For index As Integer = 0 To .Rows.Count - 1
                                        total = total + .Rows(index).Item("total")
                                        If General_Class.CekNULL(.Rows(index).Item("akun_jasa")) = "" Then
                                            CloseTransMySQL()
                                            CloseConnMySQL()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Kode akun tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(.Rows(index).Item("akun_jasa"), 1),
                                        Strings.Mid(.Rows(index).Item("akun_jasa"), 2, 1),
                                        Strings.Mid(Ganti(.Rows(index).Item("akun_jasa")), 3),
                                        KodePerusahaan, KodeProyek, "Persediaan; " & TxtFaktur.Text & "; " & Strings.Left(TextBox11.Text.Trim, 27) & "; " & Strings.Left(TextBox2.Text.Trim, 28), HilangkanTanda(.Rows(index).Item("total")), "0", pagenumber, Ket_Lokasi_HO_Proyek, Cost_center:=Ket_Cost_Center_HO_Proyek)
                                        ExecuteTrans(SQL)
                                        pagenumber = pagenumber + 1


                                        If index = .Rows.Count - 1 Then
                                            Dim tmbhn As Double = Val(HilangkanTanda(TxtSub.Text)) - total
                                            If tmbhn <> 0 Then
                                                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & .Rows(index).Item("akun_jasa") & "'"
                                                Using Dr = OpenTrans(SQL)
                                                    If Dr.Read Then
                                                        Dr.Close()
                                                        'update 

                                                        SQL = "update detail_jurnal set debit = debit+ " & tmbhn & " where "
                                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & .Rows(index).Item("akun_jasa") & "'"
                                                        ExecuteTrans(SQL)
                                                    Else
                                                        Dr.Close()
                                                        CloseTransMySQL()
                                                        CloseConnMySQL()
                                                        CloseTrans()
                                                        CloseConn()
                                                        MessageBox.Show("Jurnal 2 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End Using
                                            End If

                                        End If

                                    Next
                                Else
                                    CloseTransMySQL()
                                    CloseConnMySQL()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data barang masuk tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub

                                End If
                            End With
                        End Using

                    Else

                        SQL = "select round(sum(round(b.Jumlah * g.Harga,2)),0) as total,f.akun_persediaan  from barang_masuk_proyek a, barang_masuk_proyek_det b, request_material c, "
                        SQL = SQL & "request_material_detail d, web_subproyeks e, web_Proyeks f, Detail_PO_Pembelian_Proyek g where "
                        SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.no_faktur=b.no_faktur and a.status is null "
                        SQL = SQL & "and b.urut_detail_request=d.id and d.no_faktur=c.No_faktur and c.status is null  "
                        SQL = SQL & "and c.Sub_Proyek_ID=e.id and e.proyek_id=f.id and g.Kode_Perusahaan = b.Kode_Perusahaan and g.No_Urut = b.Urut_Detail_PO "
                        SQL = SQL & "and  a.no_faktur = '" & TextBox8.Text & "' group by f.akun_persediaan "
                        Using Ds = BindingTrans(SQL)

                            With Ds.Tables("MyTable")
                                If .Rows.Count <> 0 Then


                                    For index As Integer = 0 To .Rows.Count - 1
                                        total = total + .Rows(index).Item("total")
                                        If General_Class.CekNULL(.Rows(index).Item("akun_persediaan")) = "" Then
                                            CloseTransMySQL()
                                            CloseConnMySQL()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Kode akun tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(.Rows(index).Item("akun_persediaan"), 1),
                                        Strings.Mid(.Rows(index).Item("akun_persediaan"), 2, 1),
                                        Strings.Mid(Ganti(.Rows(index).Item("akun_persediaan")), 3),
                                        KodePerusahaan, KodeProyek, "Persediaan; " & TxtFaktur.Text & "; " & Strings.Left(TextBox11.Text.Trim, 27) & "; " & Strings.Left(TextBox2.Text.Trim, 28), HilangkanTanda(.Rows(index).Item("total")), "0", pagenumber, Ket_Lokasi_HO_Proyek, Cost_center:=Ket_Cost_Center_HO_Proyek)
                                        ExecuteTrans(SQL)
                                        pagenumber = pagenumber + 1

                                        If index = .Rows.Count - 1 Then
                                            Dim tmbhn As Double = Val(HilangkanTanda(TxtSub.Text)) - total
                                            If tmbhn <> 0 Then
                                                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & .Rows(index).Item("akun_persediaan") & "'"
                                                Using Dr = OpenTrans(SQL)
                                                    If Dr.Read Then
                                                        Dr.Close()
                                                        'update 

                                                        SQL = "update detail_jurnal set debit = debit+ " & tmbhn & " where "
                                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & .Rows(index).Item("akun_persediaan") & "'"
                                                        ExecuteTrans(SQL)
                                                    Else
                                                        Dr.Close()
                                                        CloseTransMySQL()
                                                        CloseConnMySQL()
                                                        CloseTrans()
                                                        CloseConn()
                                                        MessageBox.Show("Jurnal 2 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End Using
                                            End If

                                        End If
                                    Next
                                Else
                                    CloseTransMySQL()
                                    CloseConnMySQL()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data barang masuk tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub

                                End If
                            End With

                        End Using

                    End If


                    'Dim tgrand As Double = Val(HilangkanTanda(TxtTotal.Text)) + Val(HilangkanTanda(TextBox13.Text))
                    If ComboBox2.SelectedIndex = 1 Then 'non tunai
                        SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(X_Hutang, 1),
                                 Strings.Mid(X_Hutang, 2, 1),
                                 Strings.Mid(Ganti(X_Hutang), 3),
                                 KodePerusahaan, KodeProyek, "Pembelian; " & TxtFaktur.Text & "; " & Strings.Left(TextBox11.Text.Trim, 27) & "; " & Strings.Left(TextBox2.Text.Trim, 28), "0", HilangkanTanda(TxtTotal.Text), pagenumber, Ket_Lokasi_HO_Proyek, Cost_center:=Ket_Cost_Center_HO_Proyek)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1
                    Else
                        SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex), 1),
                                 Strings.Mid(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex), 2, 1),
                                 Strings.Mid(Ganti(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex)), 3),
                                 KodePerusahaan, KodeProyek, "Pembelian; " & TxtFaktur.Text & "; " & Strings.Left(TextBox11.Text.Trim, 27) & "; " & Strings.Left(TextBox2.Text.Trim, 28), "0", HilangkanTanda(TxtTotal.Text), pagenumber, Ket_Lokasi_HO_Proyek, Cost_center:=Ket_Cost_Center_HO_Proyek)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1
                    End If

                    If Val(HilangkanTanda(TextBox3.Text)) <> 0 Then
                        SQL = "select b.Kode_Tarif,b.Persentase,b.Kode_Akun, b.flag_ppn from "
                        SQL = SQL & "Barang_Masuk_Proyek a, Detail_PO_Pembelian_Proyek_PPH b, PO_Pembelian_Proyek c "
                        SQL = SQL & "where a.No_PO = c.No_Faktur and a.Status is null and c.Status is null and a.Kode_Perusahaan = c.Kode_Perusahaan "
                        SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.flag_ppn = 'Y' "
                        SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & TextBox8.Text & "' "
                        Using Ds = BindingTrans(SQL)
                            With Ds.Tables("MyTable")
                                If .Rows.Count <> 0 Then
                                    For index As Integer = 0 To .Rows.Count - 1
                                        SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(.Rows(index).Item("Kode_Akun"), 1),
                                                    Strings.Mid(.Rows(index).Item("Kode_Akun"), 2, 1),
                                                    Strings.Mid(Ganti(.Rows(index).Item("Kode_Akun")), 3),
                                                    KodePerusahaan, KodeProyek, "Pemb; " & TxtFaktur.Text & "; " & Strings.Left(TextBox11.Text.Trim, 27) & "; " & Strings.Left(TextBox2.Text.Trim, 28), HilangkanTanda(TextBox5.Text), "0", pagenumber, Ket_Lokasi_HO_Proyek, Cost_center:=Ket_Cost_Center_HO_Proyek)
                                        ExecuteTrans(SQL)
                                        pagenumber = pagenumber + 1
                                    Next
                                Else
                                    CloseTransMySQL()
                                    CloseConnMySQL()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data Tidak di Temukan . . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End With
                        End Using
                    End If

                    SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If Dr("debit") <> Dr("kredit") Then
                                Dr.Close()
                                CloseTransMySQL()
                                CloseConnMySQL()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            Dr.Close()
                            CloseTransMySQL()
                            CloseConnMySQL()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    'downpayment
                    If ComboBox2.SelectedIndex = 1 Then 'non tunai
                        If Val(HilangkanTanda(TextBox14.Text)) <> 0 Then
                            Dim uang_dp As Double = 0
                            If Val(HilangkanTanda(TxtTotal.Text)) <= Val(HilangkanTanda(TextBox14.Text)) Then
                                uang_dp = Val(HilangkanTanda(TxtTotal.Text))
                            Else
                                uang_dp = Val(HilangkanTanda(TextBox14.Text))
                            End If

                            Dim inisial_faktur_dari As String = ""
                            SQL = "select inisial_faktur from stock_owner_proyek "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ComboBox4.Text & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    'akun_persediaan_dari = Dr("persediaan")
                                    inisial_faktur_dari = Dr("inisial_faktur")
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                End If
                            End Using

                            Dim Kode_voucher2 As String = ""
                            Kode_voucher2 = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
                            Dim pagenumber1 As Integer = 1

                            Dim no_val_pemb_proyek As String = ""
                            no_val_pemb_proyek = fValPemb & Format(DateTimePicker1.Value, "MMyy") & "-" &
                                    General_Class.Get_Last_Number2("val_pemb_proyek", "no_val", 5,
                                    "Kode_perusahaan", KodePerusahaan,
                                    "And", "substring(no_val, 1, " & Len(fValPemb) + 4 & ")", fValPemb & Format(DateTimePicker1.Value, "MMyy"))

                            Dim SisaHutang As Double = 0
                            Dim JT As String = ""
                            Dim KodeCust As String = ""
                            Dim lks As String = ""
                            SQL = "select a.lokasi, a.grand, a.status, a.jenis_transaksi, a.kode_supplier, "
                            SQL = SQL & "isnull((select sum(x.grand) as ttl_retur from retur_pembelian x where x.kode_perusahaan = a.kode_perusahaan and x.no_faktur_beli = a.no_faktur and x.status is null), 0) as ttl_retur, "
                            SQL = SQL & "isnull((select sum(y.byr) from val_pemb_proyek x, detail_val_pemb_proyek y where "
                            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_val = y.no_val and "
                            SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
                            SQL = SQL & "y.no_faktur = a.no_faktur), 0) as ttl_validasi, "
                            SQL = SQL & "isnull((select sum(z.Nilai) from Detail_Pembelian_Proyek_PPH z "
                            SQL = SQL & "where z.Flag_PPN is null and z.Kode_Perusahaan = a.kode_perusahaan "
                            SQL = SQL & "and z.No_Faktur = a.no_faktur), 0) as Ttl_pph "
                            SQL = SQL & "from pembelian_proyek a where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "no_faktur = '" & TxtFaktur.Text & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    SisaHutang = Dr("grand") - Dr("ttl_retur") - Dr("ttl_validasi")
                                    JT = Dr("jenis_transaksi")
                                    KodeCust = Dr("kode_supplier")
                                    lks = Dr("lokasi")

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
                                    End If
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Nomor faktur tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            SQL = "select hutang from suppliers where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_supplier = '" & KodeCust & "'"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    If Dr("hutang") - Val(HilangkanTanda(uang_dp)) < 0 Then
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat hutang " & KodeCust & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Exit Sub
                                    Else
                                        Dr.Close()
                                        'kurangin hutangnya
                                        SQL = "update suppliers set hutang = hutang - " & HilangkanTanda(uang_dp) & " where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_supplier = '" & KodeCust & "'"
                                        ExecuteTrans(SQL)
                                    End If
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Supplier tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                End If
                            End Using

                            If SisaHutang = Val(HilangkanTanda(uang_dp)) Then
                                SQL = "Update pembelian_proyek set flag_lunas = 'Y', "
                                SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                                SQL = SQL & "jam_lunas = '" & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                                SQL = SQL & "uservalidasi = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "no_faktur = '" & TxtFaktur.Text & "'"
                                ExecuteTrans(SQL)
                            End If

                            Dim coa_hutang As String = ""
                            Dim coa_dp As String = ""
                            SQL = "select top(1) hutang,Akun_DP from stock_owner_proyek where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & lks & "'"
                            Using dr = OpenTrans(SQL)
                                If dr.Read Then
                                    coa_hutang = dr("hutang")
                                    coa_dp = dr("akun_dp")
                                Else
                                    dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data Lokasi_Proyek tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            SQL = "insert into val_pemb_proyek(kode_perusahaan, no_val, tanggal, jam, "
                            SQL = SQL & "keterangan, uservalidasi, grand, kode_voucher, cara_bayar, No_Pengajuan, Kode_Bank_Tujuan, "
                            SQL = SQL & "No_Rek_Tujuan, Nama_Penerima, Alamat_Penerima, Kota_Penerima, Negara_Penerima, Telp_Penerima, Flag_Otomatis "
                            SQL = SQL & ") values('" & KodePerusahaan & "', '" & no_val_pemb_proyek & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                            SQL = SQL & "'" & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                            SQL = SQL & "'-', '" & UserID & "', "
                            SQL = SQL & "" & HilangkanTanda(uang_dp) & ", '" & Kode_voucher2 & "', "
                            SQL = SQL & "NULL, '-', '-', '-' , '-', '-',"
                            SQL = SQL & "'-', '-' , '-', 'Y' )"
                            ExecuteTrans(SQL)

                            SQL = "update Pembelian_Proyek set Nilai_DP = " & HilangkanTanda(uang_dp) & " where kode_perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and no_faktur = '" & TxtFaktur.Text & "' "
                            ExecuteTrans(SQL)

                            SQL = "insert into detail_val_pemb_proyek(kode_perusahaan, no_val, no_faktur, byr) "
                            SQL = SQL & "values('" & KodePerusahaan & "', '" & no_val_pemb_proyek & "', "
                            SQL = SQL & "'" & TxtFaktur.Text.Trim & "', " & HilangkanTanda(uang_dp) & ")"
                            ExecuteTrans(SQL)

                            Dim x_no_urut_detail_pelunasan As Integer = 0
                            SQL = "Select IDENT_CURRENT('detail_val_pemb_proyek') as urutan"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    x_no_urut_detail_pelunasan = Dr("urutan")
                                End If
                            End Using

                            SQL = "select urut from detail_val_pemb_proyek where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "No_Val = '" & no_val_pemb_proyek & "' and urut = '" & x_no_urut_detail_pelunasan & "'"
                            Using Dr = OpenTrans(SQL)
                                If Not Dr.Read Then
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Harap ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            Dim xpph1 As Double = 0
                            Dim xpph21 As Double = 0
                            If kategori_bsr1.ToUpper = "JASA" Then
                                SQL = "select b.Kode_Tarif,b.Persentase, b.flag_ppn, b.kode_akun from "
                                SQL = SQL & "Barang_Masuk_Proyek a, Detail_PO_Pembelian_Proyek_PPH b, PO_Pembelian_Proyek c "
                                SQL = SQL & "where a.No_PO = c.No_Faktur and a.Status is null and c.Status is null and a.Kode_Perusahaan = c.Kode_Perusahaan "
                                SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.flag_ppn is null "
                                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & TextBox8.Text & "' "
                                Using Ds = BindingTrans(SQL)
                                    With Ds.Tables("MyTable")
                                        If .Rows.Count <> 0 Then
                                            For index As Integer = 0 To .Rows.Count - 1
                                                If .Rows(index).Item("Persentase") <> 0 Then
                                                    xpph1 = (Val(HilangkanTanda(uang_dp)) * .Rows(index).Item("Persentase") / 100)
                                                    xpph21 = (Val(HilangkanTanda(Format(xpph1, "N0"))))

                                                    SQL = "insert into Detail_Val_Pemb_Proyek_PPH(Kode_Perusahaan,No_Val,No_Faktur,Kode_Akun,Persentase,Nilai_PPH) values ("
                                                    SQL = SQL & "'" & KodePerusahaan & "','" & no_val_pemb_proyek & "', '" & TxtFaktur.Text & "','" & .Rows(index).Item("kode_akun") & "',"
                                                    SQL = SQL & "'" & .Rows(index).Item("Persentase") & "','" & HilangkanTanda(xpph1) & "')"
                                                    ExecuteTrans(SQL)
                                                End If
                                            Next
                                        End If
                                    End With
                                End Using
                            End If

                            SQL = ";with Cte as ( "
                            SQL = SQL & "select a.Nilai as Nilai_DP, ( "
                            SQL = SQL & "a.Nilai - "
                            SQL = SQL & "ISNULL(( "
                            SQL = SQL & "select z.nilai from Val_Pemb_Proyek_Detail_DP z, Val_Pemb_Proyek w "
                            SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan and z.urut_DP = a.No_Urut and "
                            SQL = SQL & "z.kode_perusahaan=w.kode_Perusahaan and z.no_val=w.no_val and w.status is null "
                            SQL = SQL & "), 0) ) as Sisa "
                            SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka_Detail_Proyek a, EMI_Transaksi_Pembayaran_Dimuka_Proyek b "
                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                            SQL = SQL & "And a.No_Transaksi = b.No_Transaksi "
                            SQL = SQL & "And b.Status Is null "
                            SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and a.No_Fak_PO in ( "
                            SQL = SQL & "select x.No_Faktur "
                            SQL = SQL & "from PO_Pembelian_Proyek x "
                            SQL = SQL & "where x.Status is null "
                            SQL = SQL & "and x.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and x.No_Faktur = '" & xNo_PO & "' "
                            SQL = SQL & "group by x.No_Faktur ) ) "
                            SQL = SQL & "select sum(Sisa) as Nilai_DP from Cte "
                            Using ds = BindingTrans(SQL)
                                With ds.Tables("MyTable")
                                    If .Rows.Count <> 0 Then
                                        For index = 0 To .Rows.Count - 1
                                            If .Rows(index).Item("Nilai_DP") <> Val(HilangkanTanda(TextBox14.Text)) Then
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Nilai uamg dimuka berbeda,Harap ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        Next
                                    End If
                                End With
                                'If Dr.Read Then
                                '    Total_DP = Dr("Nilai_DP")
                                'End If
                            End Using

                            Dim DpDipakai As Double = uang_dp
                            SQL = "with Cte as ( select a.Nilai as Nilai_DP, a.no_urut, ( "
                            SQL = SQL & "(a.Nilai-"
                            SQL = SQL & "ISNULL(( select z.nilai from Val_Pemb_Proyek_Detail_DP z, Val_Pemb_Proyek w where "
                            SQL = SQL & "z.Kode_Perusahaan = a.Kode_Perusahaan and z.urut_DP = a.No_Urut and "
                            SQL = SQL & "z.kode_perusahaan=w.kode_Perusahaan and z.no_val=w.no_val and w.status is null "
                            SQL = SQL & " ), 0)) ) as Sisa "
                            SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka_Detail_Proyek a, EMI_Transaksi_Pembayaran_Dimuka_Proyek b  "
                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                            SQL = SQL & "And a.No_Transaksi = b.No_Transaksi  "
                            SQL = SQL & "And b.Status Is null  "
                            SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "'  "
                            SQL = SQL & "and a.No_Fak_PO in ( "
                            SQL = SQL & "select x.No_Faktur  "
                            SQL = SQL & "from PO_Pembelian_Proyek x "
                            SQL = SQL & "where x.Status is null  "
                            SQL = SQL & "and x.Kode_Perusahaan = '" & KodePerusahaan & "'  "
                            SQL = SQL & "and x.No_Faktur = '" & xNo_PO & "'  "
                            SQL = SQL & "group by x.No_Faktur ) "
                            SQL = SQL & ")select no_urut, sisa as Nilai_DP from Cte where sisa<>0 "
                            SQL = SQL & "order by No_Urut "
                            Using Ds = BindingTrans(SQL)
                                With Ds.Tables("MyTable")
                                    If .Rows.Count <> 0 Then

                                        For i As Integer = 0 To .Rows.Count - 1
                                            Dim JumlahDp As Double = Val(HilangkanTanda(.Rows(i).Item("Nilai_DP")))
                                            If DpDipakai = 0 Then
                                                Exit For
                                            ElseIf DpDipakai < 0 Then
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If

                                            If JumlahDp >= DpDipakai Then
                                                SQL = "insert into Val_Pemb_Proyek_Detail_DP (Kode_Perusahaan, no_val, Urut_Detail_Pelunasan, Urut_DP, nilai) values "
                                                SQL = SQL & "('" & KodePerusahaan & "', '" & no_val_pemb_proyek & "', '" & x_no_urut_detail_pelunasan & "', "
                                                SQL = SQL & "'" & .Rows(i).Item("no_urut") & "', '" & HilangkanTanda(DpDipakai) & "')"
                                                ExecuteTrans(SQL)

                                                DpDipakai = 0
                                            ElseIf JumlahDp <= DpDipakai Then
                                                SQL = "insert into Val_Pemb_Proyek_Detail_DP (Kode_Perusahaan, no_val, Urut_Detail_Pelunasan, Urut_DP, nilai) values "
                                                SQL = SQL & "('" & KodePerusahaan & "', '" & no_val_pemb_proyek & "', '" & x_no_urut_detail_pelunasan & "', "
                                                SQL = SQL & "'" & .Rows(i).Item("no_urut") & "', '" & HilangkanTanda(JumlahDp) & "')"
                                                ExecuteTrans(SQL)

                                                DpDipakai -= JumlahDp
                                            Else
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Downpayment terjadi kesalahan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If

                                            If DpDipakai <> 0 And i = .Rows.Count - 1 Then
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Jumlah Downpayment tidak mencukupi !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        Next

                                    End If
                                End With
                            End Using

                            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                            SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                            SQL = SQL & "'" & Kode_voucher2 & "', "
                            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                            SQL = SQL & "'" & KodeProyek & "', 'Pembelian " & TxtFaktur.Text & "', '', "
                            SQL = SQL & "'-', '" & UserID & "')"
                            ExecuteTrans(SQL)

                            SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(coa_hutang, 1),
                              Strings.Mid(coa_hutang, 2, 1),
                              Strings.Mid(Ganti(coa_hutang), 3),
                              KodePerusahaan, KodeProyek, "Hutang; " & TxtFaktur.Text & "; " & Strings.Left(TextBox11.Text.Trim, 27) & "; " & Strings.Left(TextBox2.Text.Trim, 28), uang_dp, "0", pagenumber1, Ket_Lokasi_HO_Proyek, Bahasa_Pilihan, Ket_Cost_Center_HO)
                            ExecuteTrans(SQL)
                            pagenumber1 = pagenumber1 + 1

                            SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(coa_dp, 1),
                             Strings.Mid(coa_dp, 2, 1),
                             Strings.Mid(Ganti(coa_dp), 3),
                             KodePerusahaan, KodeProyek, "Hutang; " & TxtFaktur.Text & "; " & Strings.Left(TextBox11.Text.Trim, 27) & "; " & Strings.Left(TextBox2.Text.Trim, 28), "0", uang_dp, pagenumber1, Ket_Lokasi_HO_Proyek, Bahasa_Pilihan, Ket_Cost_Center_HO)
                            ExecuteTrans(SQL)
                            pagenumber1 = pagenumber1 + 1

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
                                    End If
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                End If
                            End Using
                        End If
                    End If

                Else '=========================================================

                    Dim f_no_fak_sementara As String = get_no_faktur_sementara()

                    SQL = "update po_pembelian_proyek set no_faktur_sementara = '" & f_no_fak_sementara & "' where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_faktur = '" & TextBox8.Text.Trim & "'"
                    ExecuteTrans(SQL)

                    '===================================================================================
                    ' CODING TAMBAHAN BY : HERI
                    '===================================================================================

                    'INI CODING KALAU DIBUTUHKAN ADA UPDATE DI BUTTON SIMPAN

                    'SQL = "Select no_faktur_bm from pembelian_bm "
                    'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "no_faktur_beli = '" & TxtFaktur.Text & "'"
                    'Using DSBeliBM = BindingTrans(SQL)
                    '    If DSBeliBM.Tables("MyTable").Rows.Count <> 0 Then
                    '        For xx As Integer = 0 To DSBeliBM.Tables("MyTable").Rows.Count - 1
                    '            SQL = "update barang_masuk set pakai = NULL, no_fak_pembelian = NULL "
                    '            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                    '            SQL = SQL & "no_faktur = '" & DSBeliBM.Tables("MyTable").Rows(xx).Item("no_faktur_bm") & "'"
                    '            ExecuteTrans(SQL)
                    '        Next
                    '    End If
                    'End Using

                    'SQL = "Delete From Pembelian_BM "
                    'SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "no_faktur_beli = '" & TxtFaktur.Text & "'"
                    'ExecuteTrans(SQL)

                    'For i As Integer = 0 To LvBM.Items.Count - 1
                    '    SQL = "update barang_masuk set no_faktur_sementara = '" & f_no_fak_sementara & "' "
                    '    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                    '    SQL = SQL & "no_faktur = '" & LvBM.Items(i).Text & "'"
                    '    ExecuteTrans(SQL)

                    '    SQL = "Insert Into Pembelian_BM_sementara (kode_perusahaan,no_faktur_beli,no_faktur_bm)"
                    '    SQL = SQL & "Values('" & KodePerusahaan & "','" & f_no_fak_sementara & "','" & LvBM.Items(i).Text & "')"
                    '    ExecuteTrans(SQL)
                    'Next
                    '===================================================================================

                    Dim no_so As String = ""
                    If TextBox12.Text.Trim.Length = 0 Then
                        no_so = "NULL"
                    Else
                        no_so = "'" & TextBox12.Text.Trim & "'"
                    End If

                    SQL = "insert into pembelian_sementara(Kode_perusahaan,no_faktur,Tanggal,jam,kode_supplier,"
                    SQL = SQL & "Jenis_transaksi,Tgl_Jatuh_tempo,UserId,disc1,disc2,no_nota, "
                    SQL = SQL & "grand, lokasi, kode_cb, ppn, nilai_ppn, nilai_sblm_ppn, no_po, no_so, Tanggal_Pembelian) "
                    SQL = SQL & "values ('" & KodePerusahaan & "', '" & f_no_fak_sementara & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & Format(DateTimePicker1.Value, "HH:mm") & "', '" & TextBox10.Text.Trim & "', '" & Strings.Left(ComboBox2.Text, 1) & "', "
                    SQL = SQL & "" & TJT & ", '" & UserID & "', " & diskon1 & ", " & diskon2 & ", '" & TextBox2.Text.Trim & "', "
                    SQL = SQL & "" & HilangkanTanda(TxtTotal.Text) & ", '" & ComboBox4.Text & "', " & cb & ", "
                    SQL = SQL & "'" & TextBox3.Text & "', '" & HilangkanTanda(TextBox5.Text) & "', '" & HilangkanTanda(TextBox6.Text) & "', '" & TextBox8.Text.Trim & "', " & no_so & ","
                    SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "')"
                    ExecuteTrans(SQL)

                    Dim x As Integer = 1
                    For i As Integer = 0 To Listview1.Items.Count - 1
                        Get_Isi_Listview(i)

                        If Format(CDate(LvExpire), "yyyy-MM-dd") = "1900-01-01" Then
                            CloseTransMySQL()
                            CloseConnMySQL()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Tanggal expire lebih kecil dari tanggal pembelian.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            DateTimePicker3.Focus()
                            Exit Sub
                        ElseIf Format(CDate(LvExpire), "yyyy-MM-dd") < Format(DateTimePicker1.Value, "yyyy-MM-dd") Then
                            CloseTransMySQL()
                            CloseConnMySQL()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Tanggal expire lebih kecil dari tanggal pembelian.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            DateTimePicker3.Focus()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Day, DateTimePicker1.Value, CDate(LvExpire)) < 365 Then
                            CloseTransMySQL()
                            CloseConnMySQL()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Tanggal expire minimal harus 365 hari", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            DateTimePicker3.Focus()
                            Exit Sub
                        End If

                        SQL = "insert into detail_pembelian_sementara (kode_perusahaan, no_faktur, kode_stock_owner, kode_barang, "
                        SQL = SQL & "jumlah, harga, persen_diskon, nilai_diskon, "
                        SQL = SQL & "expire, subtotal) values ('" & KodePerusahaan & "', "
                        SQL = SQL & "'" & f_no_fak_sementara & "', '" & LvSO & "',"
                        SQL = SQL & "'" & LvKB & "', "
                        SQL = SQL & "'" & HilangkanTanda(LvJml) & "', "
                        SQL = SQL & "'" & HilangkanTanda(LvHrg) & "', "
                        SQL = SQL & "" & HilangkanTanda(LvDiscp) & ", "
                        SQL = SQL & "" & HilangkanTanda(LvDiscrp) & ", "
                        SQL = SQL & "'" & Format(CDate(LvExpire), "yyyy-MM-dd") & "', "
                        SQL = SQL & "'" & HilangkanTanda(LvSubttl) & "')"
                        ExecuteTrans(SQL)
                    Next



                End If
                '==============================================

                CmdMySQL.Transaction.Commit()
                Cmd.Transaction.Commit()

                CloseConnMySQL()
                CloseConn()
            Catch ex As Exception
                CloseTransMySQL()
                CloseTrans()

                CloseConnMySQL()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        Else
            'tidak ada edit data
        End If

        Dim TanyaCetak As String = MessageBox.Show("Mau dicetak?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If TanyaCetak = vbYes Then
            cetak()
        End If

        BersihSeluruh()
        DateTimePicker1.Focus()
    End Sub

    Private Sub kd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles kd.TextChanged
        If kd.Text.Length >= 3 Then
            If kd.Text.Trim.Length = 0 Then
                ListView2.Visible = False : Exit Sub
            Else
                ListView2.Visible = True
            End If

            If kd.Text.Trim.Length = 0 Then
                ListView2.Visible = False : Exit Sub
            Else
                ListView2.Visible = True
            End If

            Try
                OpenConn()

                ListView2.Items.Clear()
                Dim Lvw As ListViewItem

                SQL = "Select top(100) kode_stock_owner, kode_barang, Nama, satuan, harga_beli, good_stock, disc1, disc2 From barang_proyek where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and kode_stock_owner = '" & ComboBox4.Text & "' and "
                SQL = SQL & "nama like '%" & kd.Text & "%' "
                SQL = SQL & "order by nama"
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        Lvw = ListView2.Items.Add(dr("kode_stock_owner"))
                        Lvw.SubItems.Add(dr("kode_barang"))
                        Lvw.SubItems.Add(dr("Nama"))
                        Lvw.SubItems.Add(dr("satuan"))
                        Lvw.SubItems.Add(Format(dr("harga_beli"), "N0"))
                        Lvw.SubItems.Add(Format(dr("good_stock"), "N0"))
                        Lvw.SubItems.Add(General_Class.CekZERO(dr("disc1")))
                        Lvw.SubItems.Add(Format(General_Class.CekZERO(dr("disc2")), "N0"))
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        Else
            ListView2.Visible = False
        End If
    End Sub

    Private Sub ListView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView2.DoubleClick
        If ListView2.Items.Count = 0 Then Exit Sub

        kd.Text = ListView2.FocusedItem.SubItems(1).Text
        kd.Focus()
        DateTimePicker3.Focus()
        ListView2.Visible = False
    End Sub

    Private Sub ListView2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView2.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView2_DoubleClick(ListView2, e)
        End If
    End Sub

    Private Sub ket_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles ket.Leave
        If Pemilik = "1" Then 'Nippon
            If ket.Text.Trim.Length = 0 Or ket.Text.Trim.ToUpper = "KETERANGAN. . ." Then Exit Sub

            If ComboBox4.Text.Trim.Length = 0 Then
                MessageBox.Show("Stock owner harus diisi dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                kd.Text = "" : ComboBox4.Focus()
                Exit Sub
            End If

            xSplit = ComboBox4.Text.Split("-")

            OpenConn()

            SQL = "select * from warna where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ComboBox4.Text & "' and kode_barang = '" & kd.Text & "' and kode_warna = '[" & ket.Text & "]'"
            Using Dr = Open(SQL)
                If Dr.Read Then
                    hrg.Text = Dr("harga")
                    Hrg_Minimum = Dr("harga")
                End If
            End Using

            CloseConn()
        End If
    End Sub


    Private Sub TextBox10_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox10.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView10.Items.Count = 0 Then Exit Sub
            ListView10.Focus()
        End If
    End Sub

    Private Sub TextBox10_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox10.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox10.Text.Trim.Length = 0 Then
                ListView10.Visible = False : TextBox11.Focus() : Exit Sub
            End If
            TextBox10_Leave(TextBox10, e)
        End If
        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox10_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox10.Leave
        If TextBox10.Text.Trim.Length = 0 Then
            ListView10.Visible = False : Exit Sub
        Else
            ListView10.Visible = True
        End If
        If ListView10.Focused = True Then Exit Sub

        'If Button2.Text = "&Update" Then Exit Sub

        OpenConn()

        Dim Lanjut As Boolean = False
        SQL = "select * from suppliers where kode_perusahaan = '" & KodePerusahaan & "' and kode_supplier = '" & Trim(TextBox10.Text) & "'"
        Using Dr = Open(SQL)
            If Dr.Read Then
                TextBox10.Text = Dr("kode_supplier")
                TextBox11.Text = Dr("nama")
                Lanjut = True
                ComboBox2.Focus()
            Else
                TextBox10.Text = ""
                TextBox11.Text = ""
                Lanjut = False
                TextBox11.Focus()
            End If
            ListView10.Visible = False
        End Using

        CloseConn()
    End Sub

    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged
        If TextBox10.Text.Trim.Length = 0 Then
            ListView10.Visible = False : Exit Sub
        Else
            ListView10.Visible = True
        End If

        Try
            OpenConn()

            ListView10.Items.Clear()
            Dim Lvw As ListViewItem

            SQL = "select kode_supplier, Nama, Alamat, Telepon, fax from suppliers where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and kode_supplier like '%" & Trim(TextBox10.Text) & "%' order by nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lvw = ListView10.Items.Add(Dr("kode_supplier"))
                    Lvw.SubItems.Add(Dr("Nama"))
                    Lvw.SubItems.Add(Dr("Alamat"))
                    Lvw.SubItems.Add(Dr("Telepon"))
                    Lvw.SubItems.Add(Dr("fax"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox11_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox11.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView10.Items.Count = 0 Then Exit Sub
            ListView10.Focus()
        End If
    End Sub

    Private Sub TextBox11_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox11.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox10.Text.Trim.Length = 0 Then TextBox11.Text = "" : ListView10.Visible = False ': Exit Sub
            ComboBox2.Focus()
        End If
        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox11_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox11.Leave
        If ListView10.Focused = True Then Exit Sub
        TextBox10.Text = "" : TextBox11.Text = ""
    End Sub

    Private Sub TextBox11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged
        If TextBox11.Text.Trim.Length = 0 Then
            ListView10.Visible = False : Exit Sub
        Else
            ListView10.Visible = True
        End If

        Try

            OpenConn()

            ListView10.Items.Clear()
            Dim Lvw As ListViewItem

            SQL = "select kode_supplier, Nama, Alamat, Telepon, fax from suppliers where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and nama like '%" & Trim(TextBox11.Text) & "%' order by nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lvw = ListView10.Items.Add(Dr("kode_supplier"))
                    Lvw.SubItems.Add(Dr("Nama"))
                    Lvw.SubItems.Add(Dr("Alamat"))
                    Lvw.SubItems.Add(Dr("Telepon"))
                    Lvw.SubItems.Add(Dr("fax"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView10_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView10.DoubleClick
        If ListView10.Items.Count = 0 Then Exit Sub
        TextBox10.Text = ListView10.FocusedItem.Text

        TextBox10.Focus()
        ComboBox2.Focus()

        ListView10.Visible = False
    End Sub

    Private Sub ListView10_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView10.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView10_DoubleClick(ListView10, e)
        End If
    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            If ComboBox2.SelectedIndex = 0 Then

            Else
                ComboBox5.Focus()
            End If
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedIndex = 0 Then
            DateTimePicker2.Visible = False
            ComboBox5.Visible = False
            ComboBoxCb1.Enabled = True
            ComboBoxCb1.SelectedIndex = 0
        Else
            DateTimePicker2.Visible = True
            ComboBox5.Visible = True
            ComboBoxCb1.Enabled = False
            ComboBoxCb1.SelectedIndex = 0
        End If
    End Sub

    Private Sub ComboBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox5.KeyPress

    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox5.SelectedIndexChanged
        Dim xx As Integer = 0
        If ComboBox5.SelectedIndex = -1 Then
            xx = 0
        Else
            xx = Val(ComboBox5.Text)
        End If
        DateTimePicker2.Value = DateAdd(DateInterval.Day, xx, DateTimePicker1.Value)
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            discz.Enabled = True ': discz.Focus()
            CheckBox2.Checked = False
        Else
            discz.Enabled = False
            discz.Text = "0" : TextBox1.Text = "0"
        End If
        HitungGrandTotal()
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            Discx.Enabled = True ': Discx.Focus()
            CheckBox1.Checked = False
        Else
            Discx.Enabled = False
            Discx.Text = "0"
        End If
        HitungGrandTotal()
    End Sub

    Private Sub Discx_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Discx.TextChanged
        HitungGrandTotal()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        BersihSeluruh()
        DateTimePicker1.Focus()
    End Sub

    Private Sub TxtFaktur_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFaktur.KeyPress
        If e.KeyChar = Chr(13) Then TextBox2.Focus()
    End Sub

    Private Sub TxtFaktur_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtFaktur.Leave
        OpenConn()

        SQL = "select b.ppn, b.nilai_ppn, b.kode_voucher, b.lokasi, b.kode_cb, b.no_nota, c.keterangan, b.grand, b.kode_perusahaan, b.no_faktur, "
        SQL = SQL & "b.tanggal, b.jam, b.jenis_transaksi, b.tgl_jatuh_tempo, b.flag_lunas, "
        SQL = SQL & "b.tgl_lunas, b.userid, b.uservalidasi, b.disc1, b.disc2, b.status, "
        SQL = SQL & "d.kode_kategori, b.kode_supplier, d.nama, c.kode_stock_owner, "
        SQL = SQL & "c.kode_barang, a.nama as namabarang, c.serial_number, c.harga, c.jumlah, a.satuan, c.expire, "
        SQL = SQL & "c.persen_diskon, c.nilai_diskon, c.hb_baru, c.hj_baru, "
        SQL = SQL & "c.flag_hb, c.flag_hj, c.hb_lama, c.hj_lama, c.userid_ubah, c.pakai_sn "
        SQL = SQL & "from barang_proyek a, pembelian_proyek b, detail_pembelian_proyek c, suppliers d "
        SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and c.kode_perusahaan = d.kode_perusahaan and "
        SQL = SQL & "a.kode_barang = c.kode_barang and a.kode_stock_owner = c.kode_stock_owner and b.no_faktur = c.no_faktur and "
        SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and b.kode_supplier = d.kode_supplier and b.no_faktur = '" & TxtFaktur.Text.Trim & "' and status is null order by c.no_urut"
        Using Dr = Open(SQL)
            If Dr.Read Then
                TextBox2.Focus()
                DateTimePicker1.Enabled = False
                DateTimePicker1.Value = Dr("tanggal")
                Dim Ket As String = ""
                Dim Ket_Di_Array As String = ""
                Listview1.Items.Clear()
                TextBox10.Text = Dr("kode_supplier")
                TextBox11.Text = Dr("nama")
                ListView10.Visible = False
                TextBox2.Text = Dr("no_nota")

                If Dr("jenis_transaksi") = "T" Then
                    ComboBox2.SelectedIndex = 0
                    DateTimePicker2.Value = CDate(FMenuDevFix.ToolStripStatusLabel3.Text)
                    ComboBoxCb1.Text = Dr("kode_cb")
                Else
                    ComboBox2.SelectedIndex = 1
                    DateTimePicker2.Value = Dr("tgl_jatuh_tempo")
                    ComboBoxCb1.SelectedIndex = -1
                End If
                ComboBox5.SelectedIndex = -1

                ComboBox4.Text = Dr("lokasi")

                If Dr("disc1") = 0 Then
                    CheckBox1.Checked = False
                Else
                    CheckBox1.Checked = True
                End If
                If Dr("disc2") = 0 Then
                    CheckBox2.Checked = False
                Else
                    CheckBox2.Checked = True
                End If
                discz.Text = Dr("disc1")
                Discx.Text = Dr("disc2")

                If Dr("ppn") <> 0 Then
                    CheckBox3.Checked = True
                Else
                    CheckBox3.Checked = False
                End If

                TextBox3.Text = Dr("ppn")
                TextBox5.Text = Dr("nilai_ppn")

                If General_Class.CekNULL(Dr("keterangan")) = "" Then
                    Ket = ""
                    Ket_Di_Array = "[]"
                Else
                    Ket = Dr("keterangan")
                    Ket_Di_Array = Dr("keterangan")
                End If

                Dim lv As New ListViewItem
                Dim GrandTotal As Double = 0

                lv = Listview1.Items.Add(Dr("kode_stock_owner"))
                lv.SubItems.Add(Dr("kode_barang"))
                lv.SubItems.Add(Dr("namabarang") & Ket)
                If Dr("pakai_sn") = "Y" Then
                    lv.SubItems.Add(Dr("serial_number"))
                Else
                    lv.SubItems.Add("-")
                End If
                lv.SubItems.Add(Format(Dr("expire"), "dd MMM yyyy"))
                lv.SubItems.Add(Format(Dr("harga"), "N2"))
                'lv.SubItems.Add(TampilanDesimal(Dr("jumlah")))
                lv.SubItems.Add(Dr("jumlah"))
                lv.SubItems.Add(Dr("satuan"))
                lv.SubItems.Add(Dr("persen_diskon"))
                lv.SubItems.Add(Format(Dr("nilai_diskon"), "N0"))
                If Dr("persen_diskon") = 0 Then
                    GrandTotal = HasilDiskon(Dr("harga"), Dr("jumlah"), 0, Dr("nilai_diskon"), "RP")
                Else
                    GrandTotal = HasilDiskon(Dr("harga"), Dr("jumlah"), Dr("persen_diskon"), 0, "PERSEN")
                End If
                lv.SubItems.Add(Format(GrandTotal, "N0"))
                lv.SubItems.Add(Format(Dr("hb_baru"), "N0"))
                lv.SubItems.Add(Format(Dr("hj_baru"), "N0"))
                lv.SubItems.Add(Dr("flag_hb"))
                lv.SubItems.Add(Dr("flag_hj"))
                lv.SubItems.Add(Format(Dr("hb_lama"), "N0"))
                lv.SubItems.Add(Format(Dr("hj_lama"), "N0"))
                lv.SubItems.Add(Dr("userid_ubah"))
                lv.SubItems.Add(Dr("pakai_sn"))

                Do While Dr.Read
                    GrandTotal = 0
                    If General_Class.CekNULL(Dr("keterangan")) = "" Then
                        Ket = ""
                        Ket_Di_Array = "[]"
                    Else
                        Ket = Dr("keterangan")
                        Ket_Di_Array = Dr("keterangan")
                    End If
                    lv = Listview1.Items.Add(Dr("kode_stock_owner"))
                    lv.SubItems.Add(Dr("kode_barang"))
                    lv.SubItems.Add(Dr("namabarang") & Ket)
                    If Dr("pakai_sn") = "Y" Then
                        lv.SubItems.Add(Dr("serial_number"))
                    Else
                        lv.SubItems.Add("-")
                    End If
                    lv.SubItems.Add(Format(Dr("expire"), "dd MMM yyyy"))
                    lv.SubItems.Add(Format(Dr("harga"), "N0"))
                    lv.SubItems.Add(TampilanDesimal(Dr("jumlah")))
                    lv.SubItems.Add(Dr("satuan"))
                    lv.SubItems.Add(Dr("persen_diskon"))
                    lv.SubItems.Add(Format(Dr("nilai_diskon"), "N0"))

                    If Dr("persen_diskon") = 0 Then
                        GrandTotal = HasilDiskon(Dr("harga"), Dr("jumlah"), 0, Dr("nilai_diskon"), "RP")
                    Else
                        GrandTotal = HasilDiskon(Dr("harga"), Dr("jumlah"), Dr("persen_diskon"), 0, "PERSEN")
                    End If
                    lv.SubItems.Add(Format(GrandTotal, "N0"))
                    lv.SubItems.Add(Format(Dr("hb_baru"), "N0"))
                    lv.SubItems.Add(Format(Dr("hj_baru"), "N0"))
                    lv.SubItems.Add(Dr("flag_hb"))
                    lv.SubItems.Add(Dr("flag_hj"))
                    lv.SubItems.Add(Format(Dr("hb_lama"), "N0"))
                    lv.SubItems.Add(Format(Dr("hj_lama"), "N0"))
                    lv.SubItems.Add(Dr("userid_ubah"))
                    lv.SubItems.Add(Dr("pakai_sn"))
                Loop

                '===================================================================================
                ' CODING TAMBAHAN BY : HERI
                '===================================================================================
                'LvBM.Items.Clear()

                'SQL = "Select no_faktur_bm from pembelian_bm "
                'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "no_faktur_beli = '" & TxtFaktur.Text & "'"
                'Using DrBeliBM = OpenTrans(SQL)
                '    Do While DrBeliBM.Read
                '        lv = LvBM.Items.Add(DrBeliBM("no_faktur_bm"))
                '    Loop
                'End Using
                '===================================================================================

                Button2.Text = "&Update"
            Else
                BersihSeluruh()
                TextBox10.Focus()
            End If
        End Using

        CloseConn()

        HitungGrandTotal()
    End Sub

    Private Sub DateTimePicker1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox2.Focus()
    End Sub

    Private Sub DateTimePicker1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.Leave
        Try
            OpenConn()

            get_no_faktur("T")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox10.Enabled = True Then
                TextBox10.Focus()
            Else
                ComboBox2.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Chr(13) Then ket.Focus()
        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub ket_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ket.TextChanged

    End Sub

    Private Sub DateTimePicker3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker3.KeyPress
        If e.KeyChar = Chr(13) Then
            If hrg.Enabled = True Then
                hrg.Focus()
            Else
                jml.Focus()
            End If
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            TextBox3.Text = "11"
        Else
            TextBox3.Text = "0"
        End If
        HitungGrandTotal()
    End Sub

    Private Sub jml_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles jml.TextChanged

    End Sub

    Private Sub TextBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then disc.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub ListView3_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView3.DoubleClick
        TextBox7.Text = ""
        TextBox8.Text = ""

        TextBox2.Enabled = False
        TextBox10.Enabled = False
        TextBox11.Enabled = False
        ComboBox2.Enabled = False
        ComboBox5.Enabled = False
        DateTimePicker2.Enabled = False
        CheckBox1.Enabled = False
        CheckBox2.Enabled = False
        CheckBox3.Enabled = False
        Discx.Enabled = False
        ComboBoxCb1.SelectedIndex = -1
        ComboBoxCb1.Enabled = False

        TextBox12.Text = ""

        Try
            OpenConn()

            SQL = "select a.no_so, a.no_po, a.no_faktur, a.no_nota, a.kode_supplier, c.nama as nama_supplier, a.jenis_transaksi, "
            SQL = SQL & "a.tgl_jatuh_tempo, b.kode_Stock_owner, b.kode_barang, d.nama as nama_barang, "
            SQL = SQL & "expire, harga, jumlah, satuan, persen_diskon, nilai_diskon, subtotal, "
            SQL = SQL & "a.disc1, a.disc2, ppn, nilai_ppn, b.update_hpp from "
            SQL = SQL & "pembelian_sementara a, detail_pembelian_sementara b, suppliers c, barang_proyek d where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and "
            SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan and "
            SQL = SQL & "a.no_faktur = b.no_faktur and a.kode_supplier = c.kode_Supplier and "
            SQL = SQL & "b.kode_Stock_owner = d.kode_Stock_owner and b.kode_barang = d.kode_barang and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & ListView3.FocusedItem.Text & "' and "
            SQL = SQL & "a.lokasi = '" & ComboBox4.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TextBox7.Text = Dr("no_faktur")
                    TextBox2.Text = Dr("no_nota")
                    TextBox10.Text = Dr("kode_supplier")
                    TextBox11.Text = Dr("nama_supplier")
                    ListView10.Visible = False
                    TextBox8.Text = Dr("no_po")
                    TextBox12.Text = General_Class.CekNULL(Dr("no_so"))

                    If Dr("jenis_transaksi") = "T" Then
                        ComboBox2.SelectedIndex = 0
                        ComboBox5.SelectedIndex = -1
                        ComboBox5.Enabled = False
                        DateTimePicker2.Value = CDate(FMenuDevFix.ToolStripStatusLabel3.Text)
                        ComboBoxCb1.SelectedIndex = 0
                        ComboBoxCb1.Enabled = True
                    Else
                        ComboBox2.SelectedIndex = 1
                        ComboBox5.SelectedIndex = -1
                        ComboBox5.Enabled = False
                        DateTimePicker2.Value = Dr("tgl_jatuh_tempo")
                        ComboBoxCb1.SelectedIndex = 0
                        ComboBoxCb1.Enabled = False
                    End If

                    If Dr("disc1") = 0 Then
                        CheckBox1.Checked = False
                    Else
                        CheckBox1.Checked = True
                    End If
                    If Dr("disc2") = 0 Then
                        CheckBox2.Checked = False
                    Else
                        CheckBox2.Checked = True
                    End If
                    discz.Text = Dr("disc1")
                    Discx.Text = Dr("disc2")
                    Discx.Enabled = False

                    If Dr("ppn") <> 0 Then
                        CheckBox3.Checked = True
                    Else
                        CheckBox3.Checked = False
                    End If

                    TextBox3.Text = Dr("ppn")
                    TextBox5.Text = Dr("nilai_ppn")

                    Listview1.Items.Clear()

                    Dim lv As New ListViewItem
                    lv = Listview1.Items.Add(Dr("kode_stock_owner"))
                    lv.SubItems.Add(Dr("kode_barang"))
                    lv.SubItems.Add(Dr("nama_barang"))
                    lv.SubItems.Add("-")
                    lv.SubItems.Add(Format(Dr("expire"), "dd MMM yyyy"))
                    lv.SubItems.Add(Format(Dr("harga"), "N2"))
                    lv.SubItems.Add(Format(Dr("jumlah"), "N2"))
                    lv.SubItems.Add(Dr("satuan"))
                    lv.SubItems.Add(Dr("persen_diskon"))
                    lv.SubItems.Add(Format(Dr("nilai_diskon"), "N0"))
                    lv.SubItems.Add(Format(Dr("subtotal"), "N0"))
                    lv.SubItems.Add("Y")
                    Dim modal As Double = 0
                    If Dr("persen_diskon") <> 0 Then
                        modal = Dr("harga") - (Dr("harga") * Dr("persen_diskon") / 100)
                    Else
                        modal = Dr("harga") - Dr("nilai_diskon")
                    End If
                    lv.SubItems.Add(Format(modal, "N0"))
                    If General_Class.CekNULL(Dr("update_hpp")) = "" Then
                        lv.SubItems.Add("-")
                    Else
                        lv.SubItems.Add(Dr("update_hpp"))
                    End If

                    Do While Dr.Read
                        lv = Listview1.Items.Add(Dr("kode_stock_owner"))
                        lv.SubItems.Add(Dr("kode_barang"))
                        lv.SubItems.Add(Dr("nama_barang"))
                        lv.SubItems.Add("-")
                        lv.SubItems.Add(Format(Dr("expire"), "dd MMM yyyy"))
                        lv.SubItems.Add(Format(Dr("harga"), "N2"))
                        lv.SubItems.Add(Format(Dr("jumlah"), "N2"))
                        lv.SubItems.Add(Dr("satuan"))
                        lv.SubItems.Add(Dr("persen_diskon"))
                        lv.SubItems.Add(Format(Dr("nilai_diskon"), "N0"))
                        lv.SubItems.Add(Format(Dr("subtotal"), "N0"))
                        lv.SubItems.Add("Y")
                        modal = 0
                        If Dr("persen_diskon") <> 0 Then
                            modal = Dr("harga") - (Dr("harga") * Dr("persen_diskon") / 100)
                        Else
                            modal = Dr("harga") - Dr("nilai_diskon")
                        End If
                        lv.SubItems.Add(Format(modal, "N0"))
                        If General_Class.CekNULL(Dr("update_hpp")) = "" Then
                            lv.SubItems.Add("-")
                        Else
                            lv.SubItems.Add(Dr("update_hpp"))
                        End If
                    Loop
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Data tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                HitungGrandTotal()
            End Using

            CloseConn()
        Catch ex As Exception
            ' LvBM.Items.Clear()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        HitungGrandTotal()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Display_Cari_po_pembelian_.ShowDialog()
    End Sub

    Private Sub TextBox13_DoubleClick(sender As Object, e As EventArgs) Handles TextBox13.DoubleClick
        If TextBox6.Text = "" Then Exit Sub
        SD_PPh_Pembelian_Proyek.kd_sup = TextBox8.Text
        SD_PPh_Pembelian_Proyek.TxtSub.Text = TextBox6.Text
        SD_PPh_Pembelian_Proyek.ShowDialog()
    End Sub

End Class
