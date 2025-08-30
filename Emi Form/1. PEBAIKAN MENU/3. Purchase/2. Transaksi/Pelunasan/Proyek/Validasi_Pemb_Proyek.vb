Public Class Validasi_Pemb_Proyek
    Dim JT As String
    Dim ArrCari As New ArrayList

    Dim LvFak As String
    Dim LvTgl As String
    Dim LvTglJthTmp As String
    Dim LvKdCus As String
    Dim LvNmCus As String
    Dim LvJml As String
    Dim LvPPN As String
    Dim LvPPH, LvFlagDP, LvNilaiDP As String

    Dim arrCrByr, ArrAkunCB1 As New ArrayList
    Dim no_fakturPengajuan, no_fakturToken As String
    Public x_alamat, x_Kota, x_negara, x_telp As String

    Dim LvAtas_Fak As String
    Dim LvAtas_Tgl As String
    Dim LvAtas_TglJthTmp As String
    Dim LvAtas_KdSup As String
    Dim LvAtas_NmSup As String
    Dim LvAtas_Alamat As String
    Dim LvAtas_Ttl As String
    Dim LvAtas_Byr As String
    Dim LvAtas_Sisa As String
    Dim Lvdpp As String

    Dim LvPPHFak As String
    Dim LvPPHPersen As String
    Dim LvPPHNilai As String
    Dim LvPPHKdAkun As String

    Private Sub Get_No_Faktur_Pengajuan()
        Dim fNB = "RN"
        no_fakturPengajuan = fNB & Format(DateTimePicker1.Value, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Pengajuan_temp", "No_Pengajuan", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Pengajuan, 1, " & Len(fNB) + 4 & ")", fNB & Format(DateTimePicker1.Value, "MMyy"))
    End Sub

    Private Sub Get_No_Faktur_Token()
        Dim fNB = "NT"
        no_fakturToken = fNB & Format(DateTimePicker1.Value, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Pengajuan_token", "No_Pengajuan", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Pengajuan, 1, " & Len(fNB) + 4 & ")", fNB & Format(DateTimePicker1.Value, "MMyy"))
    End Sub

    Private Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvFak = ListView1.Items(No_Index).Text
        LvTgl = ListView1.Items(No_Index).SubItems(1).Text
        LvTglJthTmp = ListView1.Items(No_Index).SubItems(2).Text
        LvKdCus = ListView1.Items(No_Index).SubItems(3).Text
        LvNmCus = ListView1.Items(No_Index).SubItems(4).Text
        LvJml = ListView1.Items(No_Index).SubItems(5).Text
        LvPPN = ListView1.Items(No_Index).SubItems(6).Text
        LvPPH = ListView1.Items(No_Index).SubItems(7).Text
        LvFlagDP = ListView1.Items(No_Index).SubItems(8).Text
        LvNilaiDP = ListView1.Items(No_Index).SubItems(9).Text

    End Sub

    Private Sub Get_Isi_Listview_PPH(ByVal No_Index As Integer)
        LvPPHFak = ListView4.Items(No_Index).Text
        LvPPHPersen = ListView4.Items(No_Index).SubItems(1).Text
        LvPPHNilai = ListView4.Items(No_Index).SubItems(2).Text
        LvPPHKdAkun = ListView4.Items(No_Index).SubItems(3).Text
    End Sub

    Private Sub Get_Isi_Listvie_Atas(ByVal No_Index As Integer)
        LvAtas_Fak = ListView2.Items(No_Index).Text
        LvAtas_Tgl = ListView2.Items(No_Index).SubItems(1).Text
        LvAtas_TglJthTmp = ListView2.Items(No_Index).SubItems(2).Text
        LvAtas_KdSup = ListView2.Items(No_Index).SubItems(3).Text
        LvAtas_NmSup = ListView2.Items(No_Index).SubItems(4).Text
        LvAtas_Alamat = ListView2.Items(No_Index).SubItems(5).Text
        LvAtas_Ttl = ListView2.Items(No_Index).SubItems(6).Text
        LvAtas_Byr = ListView2.Items(No_Index).SubItems(7).Text
        LvAtas_Sisa = ListView2.Items(No_Index).SubItems(8).Text
        Lvdpp = ListView2.Items(No_Index).SubItems(9).Text
    End Sub

    Private Sub cetak()
        Try

            OpenConn()

            SQL = "select kode_perusahaan from val_pemb_proyek where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "no_val = '" & TxtFaktur.Text.Trim & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Dim CrDoc As New Faktur_Pelunasan_Pemb    'Nama file CR
                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.PrintOptions.PrinterName = PrinterName
                    CrDoc.RecordSelectionFormula = "{val_pemb_proyek.Kode_Perusahaan} = '" & KodePerusahaan & "' and {val_pemb_proyek.no_val} = '" & TxtFaktur.Text.Trim & "'"

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
            SQL = "select a.grand, a.no_faktur, a.flag_lunas, a.tanggal + a.jam as tgl, a.kode_supplier, "
            SQL = SQL & "b.nama as namasupplier, b.alamat, a.tgl_jatuh_tempo, a.Nilai_Sblm_PPN, "
            SQL = SQL & "isnull((select sum(z.Nilai) from Detail_Pembelian_Proyek_PPH z "
            SQL = SQL & "where z.Flag_PPN is null and z.Kode_Perusahaan = a.kode_perusahaan "
            SQL = SQL & "and z.No_Faktur = a.no_faktur), 0) as Ttl_pph, "
            SQL = SQL & "isnull((select sum(y.byr) from val_pemb_proyek x, detail_val_pemb_proyek y where "
            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_val = y.no_val and "
            SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
            SQL = SQL & "y.no_faktur = a.no_faktur), 0) as pernah_val "
            SQL = SQL & "from pembelian_proyek a, suppliers b where a.kode_perusahaan = b.kode_perusahaan and "
            SQL = SQL & "a.kode_supplier = b.kode_supplier and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.status is null and "

            If param = "Tidak" Then
                SQL = SQL & "" & ArrCari.Item(ComboBox1.SelectedIndex) & " like '" & TextBox1.Text.Trim & "%' and "
            End If

            SQL = SQL & "a.jenis_transaksi = 'N' and a.flag_lunas is null "

            SQL = SQL & "order by tgl"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim nilai_total_hutang As Double = .Rows(i).Item("grand")

                            lv = ListView2.Items.Add(.Rows(i).Item("no_faktur"))
                            lv.SubItems.Add(Format(.Rows(i).Item("tgl"), "dd MMM yyyy"))
                            lv.SubItems.Add(Format(.Rows(i).Item("tgl_jatuh_tempo"), "dd MMM yyyy"))
                            lv.SubItems.Add(.Rows(i).Item("kode_supplier"))
                            lv.SubItems.Add(.Rows(i).Item("namasupplier"))
                            lv.SubItems.Add(.Rows(i).Item("alamat"))
                            Dim x_tot As Double = nilai_total_hutang
                            lv.SubItems.Add(Format(x_tot, "N0"))
                            lv.SubItems.Add(Format(.Rows(i).Item("pernah_val"), "N0"))
                            lv.SubItems.Add(Format(x_tot - .Rows(i).Item("pernah_val"), "N0"))
                            lv.SubItems.Add(Format(.Rows(i).Item("Nilai_Sblm_PPN"), "N0"))


                            '========================
                            '=     GET NILAI DP     =
                            '========================
                            SQL = ";with Cte as ( "
                            SQL = SQL & "select a.Nilai as Nilai_DP, a.no_urut, ( a.Nilai - ISNULL(( "
                            SQL = SQL & "select sum(z.nilai) from Val_Pemb_Proyek_Detail_DP z, Val_Pemb_Proyek w "
                            SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan and z.urut_DP = a.No_Urut and "
                            SQL = SQL & "z.kode_perusahaan=w.kode_Perusahaan and z.no_val=w.no_val and w.status is null ), 0) ) as Sisa "
                            SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka_Detail_Proyek a, EMI_Transaksi_Pembayaran_Dimuka_Proyek b "
                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                            SQL = SQL & "And a.No_Transaksi = b.No_Transaksi "
                            SQL = SQL & "And b.Status Is null "
                            SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and a.No_Fak_PO in ( "
                            SQL = SQL & "select x.No_PO "
                            SQL = SQL & "from Pembelian_Proyek z, Barang_Masuk_proyek x "
                            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
                            SQL = SQL & "and z.No_PO = x.No_Faktur "
                            SQL = SQL & "and z.Status is null and x.Status is null "
                            SQL = SQL & "and z.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and z.No_Faktur = '" & .Rows(i).Item("no_faktur") & "' "
                            SQL = SQL & "group by x.No_PO ) "
                            SQL = SQL & ") select sum(isnull(Sisa, 0)) as Nilai_DP from Cte "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Dim Nilai As Double = If(General_Class.CekNULL(Dr("Nilai_DP")) = "", 0, Dr("Nilai_DP"))
                                    lv.SubItems.Add(Format(Nilai, "N0"))
                                End If
                            End Using


                        Next
                    End If
                End With
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
        Dim xppn As Double = 0
        Dim xpph As Double = 0

        For i As Integer = 0 To ListView1.Items.Count - 1
            Get_Isi_Listview(i)

            Grand = Grand + Val(HilangkanTanda(LvJml))
            xpph = xpph + Val(HilangkanTanda(LvPPH))
            xppn = xppn + Val(HilangkanTanda(LvPPN))
        Next

        TextBoxa.Text = (Format(Grand, "N0"))
        TextPPN.Text = (Format(xppn, "N0"))
        TextPPH.Text = (Format(xpph, "N0"))
        TextBox11.Text = Val(HilangkanTanda(TextBoxa.Text)) - Val(HilangkanTanda(TextPPH.Text))
    End Sub

    Private Sub Get_No_Faktur()
        TxtFaktur.Text = fValPemb & Format(DateTimePicker1.Value, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("val_pemb_proyek", "no_val", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_val, 1, " & Len(fValPemb) + 4 & ")", fValPemb & Format(DateTimePicker1.Value, "MMyy"))
    End Sub

    Private Sub Kosong_Bawah()
        TextBox9.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox7.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        Txt_DP.Text = ""
    End Sub

    Private Sub Kosong()
        DateTimePicker1.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
        DateTimePicker1.Enabled = True

        TextBox8.Text = "" : TextBox10.Text = ""
        TextBox1.Text = "" : TextBox2.Text = ""
        TextBoxa.Text = "" : TextPPN.Text = ""
        TextPPH.Text = "" : TextBox11.Text = ""
        Txt_DP.Text = ""

        Button1.Text = "&Simpan"

        ListView1.Items.Clear()
        ListView2.Items.Clear()
        ListView4.Items.Clear()

        ComboBox1.Items.Clear() : ArrCari.Clear()
        ComboBox1.Items.Add("No Faktur") : ArrCari.Add("a.no_faktur")
        ComboBox1.Items.Add("Kode Supplier") : ArrCari.Add("a.kode_supplier")
        ComboBox1.Items.Add("Nama Supplier") : ArrCari.Add("b.nama")
        ComboBox1.SelectedIndex = 2

        Kosong_Bawah()

        x_alamat = ""
        x_Kota = ""
        x_negara = ""
        x_telp = ""


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

            'ComboBoxCb1.Items.Clear() : arrCrByr.Clear() : ArrAkunCB1.Clear()
            'ComboBoxCb1.Items.Add("-- Cara Bayar --") : arrCrByr.Add("") : ArrAkunCB1.Add("")
            'ComboBoxCb1.SelectedIndex = 0
            'SQL = "select kode_cb, keterangan, kode_account_cb from cara_bayar where kode_perusahaan = '" & KodePerusahaan & "' order by keterangan"
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        ComboBoxCb1.Items.Add(Dr("keterangan")) : arrCrByr.Add(Dr("kode_cb")) : ArrAkunCB1.Add(Dr("kode_account_cb"))
            '    Loop
            'End Using


            ComboBox3.Items.Clear()
            SQL = "SELECT Kode_Bank FROM Bank_tujuan WHERE Kode_Perusahaan = '" & KodePerusahaan & "' ORDER BY Kode_Bank"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox3.Items.Add(dr("Kode_Bank"))
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

    Private Sub Validasi_Pemb_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

    End Sub

    Private Sub Validasi_Pembelian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        ListView1.Columns.Add("No Faktur", 130, HorizontalAlignment.Left) '0
        ListView1.Columns.Add("Tgl Transaksi", 100, HorizontalAlignment.Center) '1
        ListView1.Columns.Add("Tgl Jth Tmpo", 100, HorizontalAlignment.Center) '2
        ListView1.Columns.Add("Kode Supplier", 150, HorizontalAlignment.Left) '3
        ListView1.Columns.Add("Nama Supplier", 360, HorizontalAlignment.Left) '4
        ListView1.Columns.Add("Jumlah", 100, HorizontalAlignment.Right) '5
        ListView1.Columns.Add("PPN", 0, HorizontalAlignment.Right) '6
        ListView1.Columns.Add("PPH", 0, HorizontalAlignment.Right) '7
        ListView1.Columns.Add("isDP", 0, HorizontalAlignment.Center) '8
        ListView1.Columns.Add("DpDigunakan", 0, HorizontalAlignment.Center) '9
        ListView1.View = View.Details


        ListView2.Columns.Add("No Faktur", 100, HorizontalAlignment.Left) '0
        ListView2.Columns.Add("Tgl Transaksi", 80, HorizontalAlignment.Center) '1
        ListView2.Columns.Add("Tgl Jth Tmpo", 80, HorizontalAlignment.Center) '2
        ListView2.Columns.Add("Kode Supplier", 70, HorizontalAlignment.Left) '3
        ListView2.Columns.Add("Nama Supplier", 150, HorizontalAlignment.Left) '4
        ListView2.Columns.Add("Alamat", 130, HorizontalAlignment.Left) '5
        ListView2.Columns.Add("Total", 80, HorizontalAlignment.Right) '6
        ListView2.Columns.Add("Dibayar", 80, HorizontalAlignment.Right) '7
        ListView2.Columns.Add("Sisa", 80, HorizontalAlignment.Right) '8
        ListView2.Columns.Add("dpp", 0, HorizontalAlignment.Right) '9
        ListView2.Columns.Add("Pembayaran Dimuka", 80, HorizontalAlignment.Right) '10
        ListView2.View = View.Details

        ListView2.Columns(10).DisplayIndex = 6


        ListView3.Columns.Add("No Rekening", 120, HorizontalAlignment.Left)
        ListView3.Columns.Add("Nama", 200, HorizontalAlignment.Left)
        ListView3.Columns.Add("Bank", 100, HorizontalAlignment.Left)
        ListView3.Location = New Point(476, 95)
        ListView3.Visible = False

        ListView4.Columns.Add("No Faktur", 120, HorizontalAlignment.Left)
        ListView4.Columns.Add("Persentase", 100, HorizontalAlignment.Left)
        ListView4.Columns.Add("Nilai PPH", 100, HorizontalAlignment.Left)
        ListView4.Columns.Add("Kode_akun", 100, HorizontalAlignment.Left)
        ListView4.View = View.Details

        Kosong()
        TxtFaktur.Focus()
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

        Dim isDP As Boolean = False

        If ListView1.Items.Count <> 0 Then
            For i As Integer = 0 To ListView1.Items.Count - 1
                If ListView1.Items(i).SubItems(8).Text = "Y" Then
                    isDP = True
                Else
                    isDP = False
                End If
            Next
        End If

        Try
            OpenConn()

            Get_Isi_Listvie_Atas(ListView2.FocusedItem.Index)
            'Dim totalpph As Double = 0
            'Dim totalppn As Double = 0
            'SQL = "select b.Flag_PPN,b.Nilai from Detail_Pembelian_Proyek_PPH b,Pembelian_Proyek a "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.Status is null "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & LvAtas_Fak & "' "
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read
            '        If General_Class.CekNULL(dr("Flag_PPN")) = "" Then
            '            totalpph = totalpph + dr("Nilai")
            '        Else
            '            totalppn = totalppn + dr("Nilai")
            '        End If
            '    Loop
            'End Using

            '==================
            '=     CEK DP     =
            '==================
            Dim Flag As String = "T"
            Dim DPDigunakan As Double = 0
            SQL = ";with Cte as ( select a.Nilai as Nilai_DP, ( a.Nilai - "
            SQL = SQL & "ISNULL(( select sum(z.nilai) from Val_Pemb_Proyek_Detail_DP z, Val_Pemb_Proyek w "
            SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan and z.urut_DP = a.No_Urut "
            SQL = SQL & "and z.kode_perusahaan=w.kode_Perusahaan and z.no_val=w.no_val and w.status is null), 0) ) as Sisa "
            SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka_Detail_Proyek a, EMI_Transaksi_Pembayaran_Dimuka_Proyek b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "And a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "And b.Status Is null "
            SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Fak_PO in ( "
            SQL = SQL & "select x.No_PO "
            SQL = SQL & "from Pembelian_Proyek z, Barang_Masuk_proyek x "
            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and z.No_PO = x.No_Faktur "
            SQL = SQL & "and z.Status is null and x.Status is null "
            SQL = SQL & "and z.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and z.No_Faktur = '" & LvAtas_Fak & "' "
            SQL = SQL & "group by x.No_PO ) "
            SQL = SQL & ") select sum(Sisa) as Nilai_DP from Cte "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dim NilaiDP As Double = If(General_Class.CekNULL(Dr("Nilai_DP")) = "", 0, Dr("Nilai_DP"))

                    If Not Val(HilangkanTanda(NilaiDP)) = 0 Then

                        'Txt_DP.Text = Format(NilaiDP, "N0")

                        '==============================================
                        '=     GET NILAI DP YANG SUDAH DI GUNAKAN     =
                        '==============================================
                        Dim UsedDP As Double = 0
                        For i As Integer = 0 To ListView1.Items.Count - 1
                            If ListView1.Items(i).SubItems(8).Text = "Y" Then
                                UsedDP += Val(HilangkanTanda(ListView1.Items(i).SubItems(9).Text))
                            End If
                        Next

                        If LvAtas_Sisa <= (Val(HilangkanTanda(NilaiDP)) - UsedDP) Then
                            DPDigunakan = Val(HilangkanTanda(LvAtas_Sisa))
                        Else
                            If (Val(HilangkanTanda(NilaiDP)) - UsedDP) <= 0 Then
                                CloseConn()
                                MessageBox.Show("DP Tidak Cukup", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            Else
                                DPDigunakan = (Val(HilangkanTanda(NilaiDP)) - UsedDP)
                            End If
                        End If
                        Flag = "Y"
                    Else
                        Flag = "T"
                        DPDigunakan = 0
                        'Txt_DP.Text = Format(0, "N0")
                    End If
                Else
                    DPDigunakan = 0
                    Flag = "T"
                    'Txt_DP.Text = Format(0, "N0")
                End If
            End Using


            If ListView1.Items.Count <> 0 Then
                If isDP And Flag = "T" Then
                    CloseConn()
                    MessageBox.Show("Transaksi pembelian ini mewajibkan adanya pembayaran uang muka (DP). Silakan tambahkan DP sebelum melanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf Not isDP And Flag = "Y" Then
                    CloseConn()
                    MessageBox.Show("Transaksi pembelian ini tidak boleh menggunakan DP. Silakan pastikan tidak ada DP sebelum melanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End If

            Dim NTotal As Double = 0

            If Flag = "T" Then
                NTotal = Val(HilangkanTanda(LvAtas_Sisa)) + Val(HilangkanTanda(0))
            Else
                NTotal = Val(HilangkanTanda(Format(DPDigunakan, "N0"))) + Val(HilangkanTanda(0))
            End If

            Dim totalpph2 As Double = 0
            Dim totalppn2 As Double = 0
            SQL = "select b.Flag_PPN,b.Persentase from Detail_Pembelian_Proyek_PPH b,Pembelian_Proyek a "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & LvAtas_Fak & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    If General_Class.CekNULL(dr("Flag_PPN")) = "" Then
                        totalpph2 = totalpph2 + dr("Persentase")
                    Else
                        totalppn2 = totalppn2 + dr("Persentase")
                    End If
                Loop
            End Using
            'Dim Persentase As Double = 1 + ((Val(totalppn2) / 100) - (Val(totalpph2) / 100))
            Dim Persentase As Double = 1 + ((Val(totalppn2) / 100))
            Dim DPP As Double = NTotal / Persentase

            Dim Nppn As Double = 0
            Nppn = DPP * Val(totalppn2) / 100
            Dim HslNppn As Double = Format(Nppn, "N0")

            Dim Npph As Double = 0
            'Npph = DPP * Val(totalpph2) / 100
            Dim spph As Double = 0
            SQL = "select b.Flag_PPN, b.Persentase, b.Kode_Akun from Detail_Pembelian_Proyek_PPH b, Pembelian_Proyek a "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.Status is null and b.flag_ppn is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & LvAtas_Fak & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    spph = Val(HilangkanTanda(Format(DPP * dr("Persentase") / 100, "N0")))
                    Npph = Npph + spph
                    Dim lvw As New ListViewItem
                    lvw = ListView4.Items.Add(LvAtas_Fak)
                    lvw.SubItems.Add(Format(dr("Persentase"), "N2"))
                    lvw.SubItems.Add(Format(spph, "N2"))
                    lvw.SubItems.Add(dr("Kode_Akun"))
                Loop
            End Using
            Dim HslNpph As Double = Format(Npph, "N0")



            Dim lv As New ListViewItem
            lv = ListView1.Items.Add(LvAtas_Fak)
            lv.SubItems.Add(LvAtas_Tgl)
            lv.SubItems.Add(LvAtas_TglJthTmp)
            lv.SubItems.Add(LvAtas_KdSup)
            lv.SubItems.Add(LvAtas_NmSup)

            If Flag = "T" Then
                lv.SubItems.Add(LvAtas_Sisa)
            Else
                lv.SubItems.Add(Format(DPDigunakan, "N0"))
            End If

            lv.SubItems.Add(HslNppn) 'Nilai PPN
            lv.SubItems.Add(HslNpph) ' Nilai PPh
            lv.SubItems.Add(Flag) ' Flag DP

            If Flag = "T" Then
                lv.SubItems.Add(0)
            Else
                lv.SubItems.Add(Format(DPDigunakan, "N0"))
            End If



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Hitung()

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

        'TextBox9.Text = ListView2.FocusedItem.Text
        'TextBox3.Text = ListView2.FocusedItem.SubItems(1).Text
        'TextBox4.Text = ListView2.FocusedItem.SubItems(2).Text
        'TextBox7.Text = ListView2.FocusedItem.SubItems(3).Text
        'TextBox5.Text = ListView2.FocusedItem.SubItems(4).Text
        'TextBox6.Text = HilangkanTanda(ListView2.FocusedItem.SubItems(8).Text)
        'TextBox6.Focus()
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
            'ElseIf ComboBoxCb1.SelectedIndex = -1 Or ComboBoxCb1.SelectedIndex = 0 Then
            '    MessageBox.Show("Cara bayar harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    ComboBoxCb1.Focus()
            '    Exit Sub
        ElseIf TextBox8.Text.Trim.Length = 0 Then
            MessageBox.Show("Nama Tujuan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox8.Focus()
            Exit Sub
        ElseIf ComboBox3.SelectedIndex = -1 Then
            MessageBox.Show("Bank Tujuan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox8.Focus()
            Exit Sub
        ElseIf TextBox10.Text.Trim.Length = 0 Then
            MessageBox.Show("No Rek Tujuan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox10.Focus()
            Exit Sub
        End If

        If Button1.Text = "&Simpan" Then
            Dim tny As String = MessageBox.Show("Yakin akan disimpan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If tny = vbNo Then Exit Sub

            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekSudahTutupSaldo(DateTimePicker1.Value) = "Y" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Sudah tutup saldo di bulan ini.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If


                Get_No_Faktur()
                Get_No_Faktur_Pengajuan()

                Dim SisaHutang As Double = 0
                Dim JT As String = ""
                Dim KodeCust As String = ""

                'Dim Kode_Voucher As String = "NULL" ' GetLastNumberJurnal(Format(DateTimePicker1.Value, "yyyyMM"), fJU & fValPemb, KodePerusahaan)

                Dim pagenumber As Integer = 0

                'SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                'SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                'SQL = SQL & "'" & Kode_Voucher & "', "
                'SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                'SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                'SQL = SQL & "'" & KodeProyek & "', 'Pelunasan hutang " & TxtFaktur.Text.Trim & "', '', "
                'SQL = SQL & "'-', '" & UserID & "')"
                'ExecuteTrans(SQL)

                Dim coa_hutang As String = ""
                Dim Akun_DP As String = ""
                Dim jenis_PPH As String = ""
                Dim lks As String = ""
                Dim kd_suppx As String = ""

                Dim Kode_Voucher As String = ""
                Dim Kode_VoucherX As String = ""


                Dim jenis_DP As String = ""
                For i As Integer = 0 To ListView1.Items.Count - 1
                    jenis_DP = ListView1.Items(i).SubItems(8).Text
                Next

                If jenis_DP = "Y" Then

                    '=================================
                    '=     LANGSUNG INPUT JURNAL     =
                    '=================================

                    Kode_Voucher = GetLastNumberJurnal(Format(DateTimePicker1.Value, "yyyyMM"), fJU & fValPemb, KodePerusahaan)
                    Kode_VoucherX = "'" & Kode_Voucher & "'"

                    SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                    SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                    SQL = SQL & "'" & Kode_Voucher & "', "
                    SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                    SQL = SQL & "'" & KodeProyek & "', 'Pelunasan hutang " & TxtFaktur.Text.Trim & "', '', "
                    SQL = SQL & "'-', '" & UserID & "')"
                    ExecuteTrans(SQL)



                Else

                    '==============================================================
                    '=     JIKA BUKAN MASUK DALAM DP AKAN MELAKUKAN PENGAJUAN     =
                    '==============================================================

                    SQL = "INSERT INTO pengajuan_Temp(kode_perusahaan, no_pengajuan, tanggal, jam, keterangan, userid, grand, pbk, "
                    SQL = SQL & "Validasi,Flag_Proyek) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & no_fakturPengajuan & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & TextBox2.Text & "', '" & UserID & "', "
                    SQL = SQL & "" & HilangkanTanda(TextBoxa.Text) & ", 'T', NULL,'Y')"
                    ExecuteTrans(SQL)


                End If


                SQL = "insert into val_pemb_proyek(kode_perusahaan, no_val, tanggal, jam, "
                SQL = SQL & "keterangan, uservalidasi, grand, kode_voucher, cara_bayar, No_Pengajuan, Kode_Bank_Tujuan, "
                SQL = SQL & "No_Rek_Tujuan, Nama_Penerima, Alamat_Penerima, Kota_Penerima, Negara_Penerima, Telp_Penerima) values('" & KodePerusahaan & "', "
                SQL = SQL & "'" & TxtFaktur.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                SQL = SQL & "'" & TextBox2.Text.Trim & "', '" & UserID & "', "
                'SQL = SQL & "'" & ArrCB1.Item(ComboBoxCb1.SelectedIndex) & "', "
                SQL = SQL & "" & HilangkanTanda(TextBoxa.Text) & ", " & If(jenis_DP = "Y", ("'" & Kode_Voucher & "'"), "NULL") & ", "
                SQL = SQL & "NULL, " & If(jenis_DP = "Y", "NULL", ("'" & no_fakturPengajuan & "'")) & ", '" & ComboBox3.Text & "', '" & TextBox10.Text & "' , '" & TextBox8.Text & "', '" & x_alamat & "',"
                SQL = SQL & "'" & x_Kota & "', '" & x_negara & "' , '" & x_telp & "')"
                ExecuteTrans(SQL)

                Dim grand_Nilai_Total As Double = 0
                For i As Integer = 0 To ListView1.Items.Count - 1
                    Get_Isi_Listview(i)

                    If i = 0 Then
                        kd_suppx = LvKdCus
                    Else
                        If kd_suppx <> LvKdCus Then
                            MessageBox.Show("supplier yang dimasukan harus sama!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            CloseTrans()
                            CloseConn()
                            Exit Sub
                        End If
                    End If



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
                    SQL = SQL & "no_faktur = '" & LvFak.Trim & "' "
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
                            ElseIf SisaHutang < Val(HilangkanTanda(LvJml)) Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pembayaran tidak boleh lebih dari sisa hutang. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf KodeCust <> LvKdCus Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Supplier sudah diubah sebelumnya. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                            If Dr("hutang") - Val(HilangkanTanda(LvJml)) < 0 Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat hutang " & LvNmCus & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            Else
                                Dr.Close()
                                'kurangin hutangnya
                                SQL = "update suppliers set hutang = hutang - " & HilangkanTanda(LvJml) & " where "
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


                    '============================
                    '=     CEK APAKAH LUNAS     =
                    '============================
                    If SisaHutang = Val(HilangkanTanda(LvJml)) Then
                        SQL = "Update pembelian_proyek set flag_lunas = 'Y', "
                        SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                        SQL = SQL & "jam_lunas = '" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                        SQL = SQL & "uservalidasi = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "no_faktur = '" & LvFak.Trim & "'"
                        ExecuteTrans(SQL)
                    End If


                    SQL = "select top(1) hutang, akun_selisih_PO, Akun_DP from stock_owner_proyek where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_stock_owner = '" & lks & "'"
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            coa_hutang = dr("hutang")
                            Akun_DP = dr("Akun_DP")
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data Lokasi_Proyek tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select Jenis_PPH from suppliers where "
                    SQL = SQL & "kode_Perusahaan='" & KodePerusahaan & "' and Kode_Supplier='" & LvKdCus & "' "
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


                    '===================================
                    '=     INSERT DETAIL PELUNASAN     =
                    '===================================
                    SQL = "insert into detail_val_pemb_proyek(kode_perusahaan, no_val, no_faktur, byr) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
                    SQL = SQL & "'" & LvFak.Trim & "', " & HilangkanTanda(LvJml) & ")"
                    ExecuteTrans(SQL)

                    Dim x_no_urut_detail_pelunasan As Integer = 0
                    SQL = "Select IDENT_CURRENT('detail_val_pemb_proyek') as urutan"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            x_no_urut_detail_pelunasan = Dr("urutan")
                        End If
                    End Using

                    SQL = "select urut from detail_val_pemb_proyek where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "No_Val = '" & TxtFaktur.Text.Trim & "' and urut = '" & x_no_urut_detail_pelunasan & "'"
                    Using Dr = OpenTrans(SQL)
                        If Not Dr.Read Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Harap ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using


                    '====================================
                    '=     INSERT DETAIL DP DIPAKAI     =
                    '====================================
                    If jenis_DP = "Y" Then
                        Dim DpDigunakan As Double = Val(HilangkanTanda(LvNilaiDP))
                        SQL = ";with Cte as ( "
                        SQL = SQL & "select a.Nilai as Nilai_DP, a.no_urut, ( a.Nilai - "
                        SQL = SQL & "ISNULL(( select isnull((sum(z.nilai)),0) from Val_Pemb_Proyek_Detail_DP z, Val_Pemb_Proyek w "
                        SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan and z.urut_DP = a.No_Urut and "
                        SQL = SQL & "z.kode_perusahaan=w.kode_Perusahaan and z.no_val=w.no_val and w.status is null "
                        SQL = SQL & "), 0) ) as Sisa "
                        SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka_Detail_Proyek a, EMI_Transaksi_Pembayaran_Dimuka_Proyek b "
                        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                        SQL = SQL & "And a.No_Transaksi = b.No_Transaksi "
                        SQL = SQL & "And b.Status Is null "
                        SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and a.No_Fak_PO in ( "
                        SQL = SQL & "select x.No_PO "
                        SQL = SQL & "from Pembelian_Proyek z, Barang_Masuk_proyek x "
                        SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
                        SQL = SQL & "and z.No_PO = x.No_Faktur "
                        SQL = SQL & "and z.Status is null and x.Status is null "
                        SQL = SQL & "and z.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and z.No_Faktur = '" & LvFak & "' "
                        SQL = SQL & "group by x.No_PO ) "
                        SQL = SQL & ")select isnull(Sisa, 0) as Nilai_DP, no_urut  from Cte "
                        Using Ds = BindingTrans(SQL)
                            With Ds.Tables("MyTable")
                                If .Rows.Count <> 0 Then
                                    For j As Integer = 0 To .Rows.Count - 1

                                        If DpDigunakan < 0 Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi Kesalaham", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        If DpDigunakan = 0 Then
                                            Exit For
                                        End If


                                        Dim Tot_DP As Double = .Rows(j).Item("Nilai_DP")

                                        If Tot_DP >= DpDigunakan Then
                                            SQL = "insert into Val_Pemb_Proyek_Detail_DP (Kode_Perusahaan, no_val, Urut_Detail_Pelunasan, Urut_DP, nilai) values "
                                            SQL = SQL & "('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & x_no_urut_detail_pelunasan & "', "
                                            SQL = SQL & "'" & .Rows(j).Item("no_urut") & "', '" & HilangkanTanda(DpDigunakan) & "')"
                                            ExecuteTrans(SQL)

                                            DpDigunakan = 0
                                        Else


                                            SQL = "insert into Val_Pemb_Proyek_Detail_DP (Kode_Perusahaan, no_val, Urut_Detail_Pelunasan, Urut_DP, nilai) values "
                                            SQL = SQL & "('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & x_no_urut_detail_pelunasan & "', "
                                            SQL = SQL & "'" & .Rows(j).Item("no_urut") & "', '" & HilangkanTanda(Tot_DP) & "')"
                                            ExecuteTrans(SQL)

                                            DpDigunakan -= Tot_DP

                                        End If

                                    Next
                                End If
                            End With
                        End Using

                        If Math.Round(DpDigunakan, 2) <> 0 Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi Kesalahan !!!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                    End If


                    '=============================
                    '=     INSERT DETAIL PPH     =
                    '=============================
                    'SQL = "select Persentase, Kode_Akun "
                    'SQL = SQL & "from Detail_Pembelian_Proyek_PPH "
                    'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    'SQL = SQL & "and No_Faktur = '" & LvFak & "' "
                    'SQL = SQL & "and Flag_PPN is null "
                    'SQL = SQL & "order by Kode_Tarif "
                    'Using Ds = BindingTrans(SQL)
                    '    With Ds.Tables("MyTable")
                    '        If .Rows.Count <> 0 Then
                    '            For j As Integer = 0 To .Rows.Count - 1

                    '                Dim Nilai As Double = Val(HilangkanTanda(LvJml)) * (Val(HilangkanTanda(.Rows(j).Item("Persentase"))) / 100)

                    '                SQL = "insert into Detail_Val_Pemb_Proyek_PPH(Kode_Perusahaan, No_Val, No_Faktur, Kode_Akun, Persentase, Nilai_PPH) values ( "
                    '                SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & LvFak & "', '" & .Rows(j).Item("Kode_Akun") & "', "
                    '                SQL = SQL & "'" & .Rows(j).Item("Persentase") & "','" & HilangkanTanda(Nilai) & "')"
                    '                ExecuteTrans(SQL)

                    '                grand_PPH += Nilai
                    '            Next
                    '        End If
                    '    End With
                    'End Using


                    ''=============================
                    ''=     INSERT DETAIL PPN     =
                    ''=============================
                    'SQL = "select Persentase, Kode_Akun "
                    'SQL = SQL & "from Detail_Pembelian_Proyek_PPH "
                    'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    'SQL = SQL & "and No_Faktur = '" & LvFak & "' "
                    'SQL = SQL & "and Flag_PPN = 'Y' "
                    'SQL = SQL & "order by Kode_Tarif "
                    'Using Ds = BindingTrans(SQL)
                    '    With Ds.Tables("MyTable")
                    '        If .Rows.Count <> 0 Then

                    '            Dim Nilai As Double = Val(HilangkanTanda(LvJml)) * (Val(HilangkanTanda(.Rows(0).Item("Persentase"))) / 100)

                    '            SQL = "insert into Detail_Val_Pemb_Proyek_PPH(Kode_Perusahaan, No_Val, No_Faktur, Kode_Akun, Persentase, Nilai_PPH) values ( "
                    '            SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & LvFak & "', '" & .Rows(0).Item("Kode_Akun") & "', "
                    '            SQL = SQL & "'" & .Rows(0).Item("Persentase") & "','" & HilangkanTanda(Nilai) & "')"
                    '            ExecuteTrans(SQL)


                    '        End If
                    '    End With
                    'End Using



                    If jenis_DP = "Y" Then

                        '============================
                        '=     INSERT JURNAL DP     =
                        '============================
                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "' and debit <> 0"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update

                                SQL = "update detail_jurnal set debit = debit+ " & HilangkanTanda(LvJml) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "' and debit <> 0"
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert
                                pagenumber += 1
                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_hutang, 1),
                                                  Strings.Mid(coa_hutang, 2, 1),
                                                  Strings.Mid(Ganti(coa_hutang), 3),
                                                  KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, HilangkanTanda(LvJml), "0", pagenumber, Ket_Lokasi_HO, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                            End If
                        End Using


                        SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Akun_DP & "' and kredit <> 0 "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Dr.Close()
                                'update

                                SQL = "update detail_jurnal set kredit = kredit+ " & HilangkanTanda(LvJml) & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & Akun_DP & "' and kredit <> 0 "
                                ExecuteTrans(SQL)
                            Else
                                Dr.Close()
                                'insert

                                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(Akun_DP, 1),
                                          Strings.Mid(Akun_DP, 2, 1),
                                          Strings.Mid(Ganti(Akun_DP), 3),
                                          KodePerusahaan, KodeProyek, "Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, "0", HilangkanTanda(LvJml), pagenumber, "TSSS", Bahasa_Pilihan, Ket_Cost_Center_HO)
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




                    End If

                    Dim Nilai_Total As Double = (Val(HilangkanTanda(LvJml)) - Val(HilangkanTanda(LvPPH)))
                    grand_Nilai_Total = grand_Nilai_Total + Nilai_Total



                    ''    SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    ''    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "'"
                    ''    Using Dr = OpenTrans(SQL)
                    ''        If Dr.Read Then
                    ''            Dr.Close()
                    ''            'update 

                    ''            SQL = "update detail_jurnal set debit = debit+ " & HilangkanTanda(LvJml) & " where "
                    ''            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    ''            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "'"
                    ''            ExecuteTrans(SQL)
                    ''        Else
                    ''            Dr.Close()
                    ''            'insert

                    ''            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_hutang, 1), _
                    ''                  Strings.Mid(coa_hutang, 2, 1), _
                    ''                  Strings.Mid(Ganti(coa_hutang), 3), _
                    ''                  KodePerusahaan, KodeProyek, "Pelunasan hutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, HilangkanTanda(LvJml), "0", pagenumber + 1)
                    ''            ExecuteTrans(SQL)
                    ''        End If
                    ''    End Using
                Next




                '=== JIKA BUKAN DP ==='
                Dim x_no_urut_det_pengajuan As Integer = 0
                If jenis_DP <> "Y" Then
                    Dim xgrand_dpp As Double = Val(HilangkanTanda(TextBoxa.Text)) - Val(HilangkanTanda(TextPPH.Text))

                    SQL = "INSERT INTO detail_pengajuan_Temp(kode_perusahaan, no_pengajuan, kode_master_acc, kode_acc, kode_detail_acc, "
                    SQL = SQL & "keterangan_detail, tgl_jatuh_tempo, jumlah, kode_bank_tujuan, no_rek_tujuan, nama_penerima, "
                    SQL = SQL & "Alamat_Penerima, Kota_Penerima, Negara_Penerima, Telp_Penerima, Lokasi, Kode_Account, "
                    SQL = SQL & "Id_Cost_Center, No_Pelunasan, Tgl_Bayar) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & no_fakturPengajuan & "', '" & Strings.Left(coa_hutang, 1) & "', "
                    SQL = SQL & "'" & Strings.Mid(coa_hutang, 2, 1) & "', '" & Strings.Mid(Ganti(coa_hutang), 3) & "', "
                    SQL = SQL & "'" & TextBox2.Text & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', " & HilangkanTanda(xgrand_dpp) & ", "
                    SQL = SQL & "'" & ComboBox3.Text & "', '" & TextBox10.Text & "', '" & TextBox8.Text & "', "
                    SQL = SQL & "'" & x_alamat & "', '" & x_Kota & "', '" & x_negara & "', '" & x_telp & "','" & Ket_Lokasi_HO_Proyek & "','" & coa_hutang & "', "
                    SQL = SQL & "'" & Ket_Cost_Center_HO_Proyek & "', '" & TxtFaktur.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "') "
                    ExecuteTrans(SQL)


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

                End If

                'For i As Integer = 0 To ListView1.Items.Count - 1
                '    Get_Isi_Listview(i)

                '    SQL = "insert into detail_val_pemb_proyek(kode_perusahaan, no_val, no_faktur, byr) "
                '    SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
                '    SQL = SQL & "'" & LvFak.Trim & "', " & HilangkanTanda(LvJml) & ")"
                '    ExecuteTrans(SQL)

                '    Dim x_no_urut_detail_pelunasan As Integer = 0
                '    SQL = "Select IDENT_CURRENT('detail_val_pemb_proyek') as urutan"
                '    Using Dr = OpenTrans(SQL)
                '        If Dr.Read Then
                '            x_no_urut_detail_pelunasan = Dr("urutan")
                '        End If
                '    End Using

                '    SQL = "select urut from detail_val_pemb_proyek where kode_perusahaan = '" & KodePerusahaan & "' and "
                '    SQL = SQL & "No_Val = '" & TxtFaktur.Text.Trim & "' and urut = '" & x_no_urut_detail_pelunasan & "'"
                '    Using Dr = OpenTrans(SQL)
                '        If Not Dr.Read Then
                '            Dr.Close()
                '            CloseTrans()
                '            CloseConn()
                '            MessageBox.Show("Harap ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            Exit Sub
                '        End If
                '    End Using

                '    'Dim Nilai_Total As Double = ((Val(HilangkanTanda(LvJml)) - Val(HilangkanTanda(LvPPN))) + Val(HilangkanTanda(LvPPH)))
                '    Dim Nilai_Total As Double = (Val(HilangkanTanda(LvJml)) - Val(HilangkanTanda(LvPPH)))
                '    'Dim Nilai_Total As Double = ((Val(HilangkanTanda(LvKursLamaTot)) + Val(HilangkanTanda(LvNilaiPPN))) - Val(HilangkanTanda(LvNilaiPPH))) + selisih
                '    grand_Nilai_Total = grand_Nilai_Total + Nilai_Total


                '    'Dim NTotal As Double = 0
                '    'NTotal = Val(HilangkanTanda(LvJml)) + Val(HilangkanTanda(0))

                '    'Dim totalpph2 As Double = 0
                '    'Dim totalppn2 As Double = 0
                '    'SQL = "select b.Flag_PPN,b.Persentase from Detail_Pembelian_Proyek_PPH b,Pembelian_Proyek a "
                '    'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.Status is null "
                '    'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & LvFak & "' "
                '    'Using dr = OpenTrans(SQL)
                '    '    Do While dr.Read
                '    '        If General_Class.CekNULL(dr("Flag_PPN")) = "" Then
                '    '            totalpph2 = totalpph2 + dr("Persentase")
                '    '        Else
                '    '            totalppn2 = totalppn2 + dr("Persentase")
                '    '        End If
                '    '    Loop
                '    'End Using

                '    ''Dim Persentase As Double = 1 + ((Val(totalppn2) / 100) - (Val(totalpph2) / 100))
                '    'Dim Persentase As Double = 1 + ((Val(totalppn2) / 100))
                '    'Dim DPP As Double = NTotal / Persentase

                '    'Dim znilai As Double = 0
                '    'Dim xnilai As Double = 0
                '    'Dim xPersen_pph As Double = 0
                '    'If Val(HilangkanTanda(LvPPH)) <> 0 Then
                '    '    SQL = "select b.Kode_Akun,b.Nilai,a.Lokasi,b.Persentase,d.Keterangan,b.No_faktur from Detail_Pembelian_Proyek_PPH b,Pembelian_Proyek a, EMI_Master_Pajak d  "
                '    '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.Status is null "
                '    '    SQL = SQL & "and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Tarif = d.Kode_Tarif "
                '    '    SQL = SQL & "and b.Flag_PPN is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & LvFak & "' "
                '    '    Using ds = BindingTrans(SQL)
                '    '        With ds.Tables("MyTable")
                '    '            For index As Integer = 0 To .Rows.Count - 1
                '    '                znilai = Val(DPP) * .Rows(index).Item("Persentase") / 100
                '    '                xnilai = Val(HilangkanTanda(Format(znilai, "N0")))
                '    '                grand_PPH = grand_PPH + xnilai

                '    '                'SQL = "select Urut_Oto from Detail_Pengajuan5_Temp where Kode_Perusahaan = '" & KodePerusahaan & "' and Urut_Detail_Pengajuan = '" & x_no_urut_det_pengajuan & "' and "
                '    '                'SQL = SQL & "Kode_Account = '" & .Rows(index).Item("Kode_Akun") & "' "
                '    '                'Using ds2 = BindingTrans(SQL)
                '    '                '    If ds2.Tables("MyTable").Rows.Count <> 0 Then
                '    '                '        For index2 As Integer = 0 To ds2.Tables("MyTable").Rows.Count - 1
                '    '                '            SQL = "update Detail_Pengajuan5_Temp set Kredit = Kredit + '" & HilangkanTanda(xnilai) & "' where "
                '    '                '            SQL = SQL & "Urut_Oto = '" & ds2.Tables("MyTable").Rows(index2).Item("Urut_Oto") & "' "
                '    '                '            ExecuteTrans(SQL)
                '    '                '        Next
                '    '                '    Else
                '    '                SQL = "insert into Detail_Pengajuan5_Temp(Kode_Perusahaan,Urut_Detail_Pengajuan, Kode_Account,Debit, Kredit, Keterangan,Tgl, lokasi)"
                '    '                SQL = SQL & "values('" & KodePerusahaan & "', '" & x_no_urut_det_pengajuan & "', '" & .Rows(index).Item("Kode_Akun") & "', '0', '" & HilangkanTanda(xnilai) & "', "
                '    '                SQL = SQL & "'" & .Rows(index).Item("Keterangan") & " " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim & " ; " & LvFak & " ', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Ket_Lokasi_HO & "')"
                '    '                ExecuteTrans(SQL)
                '    '                'End If

                '    '                'End Using
                '    '            Next
                '    '        End With
                '    '    End Using
                '    'End If

                'Next

                Dim grand_PPH As Double = 0
                For i As Integer = 0 To ListView4.Items.Count - 1
                    Get_Isi_Listview_PPH(i)

                    If jenis_DP <> "Y" Then

                        SQL = "select no_val from Detail_Val_Pemb_Proyek where kode_perusahaan = '" & KodePerusahaan & "' and no_val = '" & TxtFaktur.Text.Trim & "' and no_faktur = '" & LvPPHFak & "' "
                        Using dr = OpenTrans(SQL)
                            If Not dr.Read Then
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Harap ulangi transaksi ini lagi! ,kareta no faktur tidak ada dilistview pelunasan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using

                        SQL = "insert into Detail_Pengajuan5_Temp(Kode_Perusahaan,Urut_Detail_Pengajuan, Kode_Account,Debit, Kredit, Keterangan,Tgl, lokasi)"
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & x_no_urut_det_pengajuan & "', '" & LvPPHKdAkun & "', '0', '" & HilangkanTanda(LvPPHNilai) & "', "
                        SQL = SQL & "'PPH " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim & " ; " & LvPPHFak & " ', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Ket_Lokasi_HO & "')"
                        ExecuteTrans(SQL)

                    End If

                    SQL = "insert into Detail_Val_Pemb_Proyek_PPH(Kode_Perusahaan,No_Val,No_Faktur,Kode_Akun,Persentase,Nilai_PPH) values ("
                    SQL = SQL & "'" & KodePerusahaan & "','" & TxtFaktur.Text.Trim & "', '" & LvPPHFak & "','" & LvPPHKdAkun & "','" & HilangkanTanda(LvPPHPersen) & "','" & HilangkanTanda(LvPPHNilai) & "')"
                    ExecuteTrans(SQL)

                    grand_PPH = grand_PPH + Val(HilangkanTanda(LvPPHNilai))

                Next

                If HilangkanTanda(grand_PPH) <> HilangkanTanda(TextPPH.Text) Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("jumlah pph salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                If jenis_DP <> "Y" Then

                    SQL = "insert into Detail_Pengajuan5_Temp(Kode_Perusahaan,Urut_Detail_Pengajuan, Kode_Account,Debit, Kredit, Keterangan,Tgl, lokasi)"
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & x_no_urut_det_pengajuan & "', '" & coa_hutang & "', '" & HilangkanTanda(TextBoxa.Text) & "','0', "
                    SQL = SQL & "'Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Ket_Lokasi_HO & "')"
                    ExecuteTrans(SQL)

                    SQL = "select round(sum(debit), 2) - round(sum(kredit), 2) as data from Detail_Pengajuan5_Temp where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "Urut_Detail_Pengajuan = '" & x_no_urut_det_pengajuan & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If Dr("data") <> Val(HilangkanTanda(grand_Nilai_Total)) Then
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

                End If

                'SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex), 1), _
                '               Strings.Mid(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex), 2, 1), _
                '               Strings.Mid(Ganti(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex)), 3), _
                '               KodePerusahaan, KodeProyek, "Pelunasan hutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, "0", HilangkanTanda(TextBoxa.Text), pagenumber + 1)
                'ExecuteTrans(SQL)

                'SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "kode_voucher = '" & Kode_Voucher & "'"
                'Using Dr = OpenTrans(SQL)
                '    If Dr.Read Then
                '        If Dr("debit") <> Dr("kredit") Then
                '            Dr.Close()
                '            CloseTrans()
                '            CloseConn()
                '            MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            Exit Sub
                '        End If
                '    Else
                '        Dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using


                '24-04-2025
                'SQL = "INSERT INTO pengajuan(kode_perusahaan, no_pengajuan, tanggal, jam, keterangan, userid, grand, pbk, Validasi) "
                'SQL = SQL & "values('" & KodePerusahaan & "', '" & no_fakturPengajuan & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                'SQL = SQL & "'" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & TextBox2.Text.Trim & "', '" & UserID & "', "
                'SQL = SQL & "" & HilangkanTanda(TextBoxa.Text) & ", 'T', NULL)"
                'ExecuteTrans(SQL)


                'SQL = "INSERT INTO detail_pengajuan(kode_perusahaan, no_pengajuan, kode_master_acc, kode_acc, kode_detail_acc, "
                'SQL = SQL & "keterangan_detail, tgl_jatuh_tempo, jumlah, kode_bank_tujuan, no_rek_tujuan, nama_penerima, "
                'SQL = SQL & "Alamat_Penerima, Kota_Penerima, Negara_Penerima, Telp_Penerima, Lokasi, Kode_Account, id_cost_center) "
                'SQL = SQL & "values('" & KodePerusahaan & "', '" & no_fakturPengajuan & "', '" & Strings.Left(coa_hutang, 1) & "', "
                'SQL = SQL & "'" & Strings.Mid(coa_hutang, 2, 1) & "', '" & Strings.Mid(Ganti(coa_hutang), 3) & "', "
                'SQL = SQL & "'" & TextBox2.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', " & HilangkanTanda(TextBoxa.Text) & ", "
                'SQL = SQL & "'" & ComboBox3.Text & "', '" & TextBox10.Text & "', '" & TextBox8.Text & "', "
                'SQL = SQL & "'" & x_alamat & "', '" & x_Kota & "', '" & x_negara & "', '" & x_telp & "','" & Ket_Lokasi_HO_Proyek & "','" & coa_hutang & "', '" & Ket_Cost_Center_HO_Proyek & "') "
                'ExecuteTrans(SQL)
                '24-04-2025


                '======================================
                '=     PENGECEKAN APAKAH DP BENAR     =
                '======================================
                If jenis_DP = "Y" Then
                    SQL = "select round(sum(debit), 2) - round(sum(kredit), 2) as data from detail_jurnal "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "'  "
                    SQL = SQL & "and Kode_Voucher = '" & Kode_Voucher & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If Dr("data") <> 0 Then
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
                End If

                'If True Then
                '    CloseTrans()
                '    CloseConn()
                '    MessageBox.Show("Tahan")
                '    Exit Sub
                'End If


                Cmd.Transaction.Commit()

                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        Else 'update

            Dim tny As String = MessageBox.Show("Yakin akan diupdate?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If tny = vbNo Then Exit Sub

            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekButtonRole("update_pelunasan_pembelian") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                Dim kode_voucher_lama As String = ""
                SQL = "Select kode_voucher, status from val_pemb_proyek where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_val = '" & TxtFaktur.Text.Trim & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            kode_voucher_lama = .Rows(0).Item("kode_voucher")
                            If General_Class.CekNULL(.Rows(0).Item("status")) = "Y" Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pelunasan tidak bisa diupdate, karena sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            End If
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Transaksi tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

                SQL = "select a.kode_supplier, a.no_faktur, b.no_val, b.byr from pembelian_proyek a, detail_val_pemb_proyek b where "
                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And a.no_faktur = b.no_faktur And "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "b.no_val = '" & TxtFaktur.Text.Trim & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                SQL = "select hutang from suppliers where kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_supplier = '" & .Rows(i).Item("kode_supplier") & "'"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        Dr.Close()

                                        SQL = "update suppliers set hutang = hutang + " & .Rows(i).Item("byr") & " where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_supplier = '" & .Rows(i).Item("kode_supplier") & "'"
                                        ExecuteTrans(SQL)
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Supplier tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Exit Sub
                                    End If
                                End Using

                                SQL = "Update pembelian_proyek set flag_lunas = NULL, "
                                SQL = SQL & "Tgl_lunas = NULL, "
                                SQL = SQL & "jam_lunas = NULL, "
                                SQL = SQL & "uservalidasi = NULL where kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "no_faktur = '" & .Rows(i).Item("no_faktur") & "'"
                                ExecuteTrans(SQL)
                            Next
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Pelunasan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using


                '=============

                SQL = "delete from detail_val_pemb_proyek where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_val = '" & TxtFaktur.Text.Trim & "'"
                ExecuteTrans(SQL)

                Dim SisaHutang As Double = 0
                Dim JT As String = ""
                Dim KodeCust As String = ""

                For i As Integer = 0 To ListView1.Items.Count - 1
                    Get_Isi_Listview(i)

                    SQL = "select a.grand, a.status, a.jenis_transaksi, a.kode_supplier, "
                    SQL = SQL & "isnull((select sum(x.grand) as ttl_retur from retur_pembelian x where x.kode_perusahaan = a.kode_perusahaan and x.no_faktur_beli = a.no_faktur and x.status is null), 0) as ttl_retur, "
                    SQL = SQL & "isnull((select sum(y.byr) from val_pemb_proyek x, detail_val_pemb y where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_val = y.no_val and "
                    SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
                    SQL = SQL & "y.no_faktur = a.no_faktur), 0) as ttl_validasi "
                    SQL = SQL & "from pembelian_proyek a where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_faktur = '" & LvFak.Trim & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            SisaHutang = Dr("grand") - Dr("ttl_retur") - Dr("ttl_validasi")
                            JT = Dr("jenis_transaksi")
                            KodeCust = Dr("kode_supplier")

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
                            ElseIf SisaHutang < Val(HilangkanTanda(LvJml)) Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pembayaran tidak boleh lebih dari sisa hutang. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf KodeCust <> LvKdCus Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Supplier sudah diubah sebelumnya. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                            If Dr("hutang") - Val(HilangkanTanda(LvJml)) < 0 Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat hutang " & LvNmCus & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            Else
                                Dr.Close()
                                'kurangin hutangnya
                                SQL = "update suppliers set hutang = hutang - " & HilangkanTanda(LvJml) & " where "
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

                    If SisaHutang = Val(HilangkanTanda(LvJml)) Then
                        SQL = "Update pembelian_proyek set flag_lunas = 'Y', "
                        SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                        SQL = SQL & "jam_lunas = '" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                        SQL = SQL & "uservalidasi = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "no_faktur = '" & LvFak.Trim & "'"
                        ExecuteTrans(SQL)
                    End If
                Next

                SQL = "update val_pemb_proyek set keterangan = '" & TextBox2.Text.Trim & "', "
                'SQL = SQL & "cara_bayar = '" & arrCrByr.Item(ComboBoxCb1.SelectedIndex) & "', "
                SQL = SQL & "grand = " & HilangkanTanda(TextBoxa.Text) & " where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_val = '" & TxtFaktur.Text.Trim & "'"
                ExecuteTrans(SQL)

                For i As Integer = 0 To ListView1.Items.Count - 1
                    Get_Isi_Listview(i)

                    SQL = "insert into detail_val_pemb_proyek(kode_perusahaan, no_val, no_faktur, byr) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
                    SQL = SQL & "'" & LvFak.Trim & "', " & HilangkanTanda(LvJml) & ")"
                    ExecuteTrans(SQL)
                Next

                Dim coa_hutang As String = ""

                SQL = "select top(1) hutang from stock_owner_proyek where kode_perusahaan = '" & KodePerusahaan & "'"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        coa_hutang = dr("hutang")
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Lokasi_Proyek tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                'Dim pagenumber As Integer = 0

                'SQL = "delete from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "kode_voucher = '" & kode_voucher_lama & "'"
                'ExecuteTrans(SQL)

                'SQL = Get_Detail_Jurnal(kode_voucher_lama, Strings.Left(coa_hutang, 1), _
                '               Strings.Mid(coa_hutang, 2, 1), _
                '               Strings.Mid(Ganti(coa_hutang), 3), _
                '               KodePerusahaan, KodeProyek, "Pelunasan hutang " & TxtFaktur.Text.Trim, HilangkanTanda(TextBoxa.Text), "0", pagenumber + 1)
                'ExecuteTrans(SQL)


                'SQL = Get_Detail_Jurnal(kode_voucher_lama, Strings.Left(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex), 1), _
                '               Strings.Mid(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex), 2, 1), _
                '               Strings.Mid(Ganti(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex)), 3), _
                '               KodePerusahaan, KodeProyek, "Pelunasan hutang " & TxtFaktur.Text.Trim, "0", HilangkanTanda(TextBoxa.Text), pagenumber + 1)
                'ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
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
        Try

            OpenConn()

            Get_No_Faktur()

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        'If e.KeyChar = Chr(13) Then ComboBoxCb1.Focus()
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

        Get_Isi_Listview(ListView1.FocusedItem.Index)
        For jumlah As Integer = ListView4.Items.Count - 1 To 0 Step -1
            If LvFak = ListView4.Items(jumlah).Text Then
                ListView4.Items(jumlah).Remove()
            End If
        Next

        TextBox9.Text = ListView1.FocusedItem.Text
        TextBox3.Text = ListView1.FocusedItem.SubItems(1).Text
        TextBox4.Text = ListView1.FocusedItem.SubItems(2).Text
        TextBox7.Text = ListView1.FocusedItem.SubItems(3).Text
        TextBox5.Text = ListView1.FocusedItem.SubItems(4).Text
        TextBox6.Text = HilangkanTanda(ListView1.FocusedItem.SubItems(5).Text)

        ListView1.FocusedItem.Remove()
        TextBox6.Focus()
        Hitung()
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
        ' ''Try

        ' ''    If TxtFaktur.Text.Trim.Length = 0 Then
        ' ''        OpenConn()

        ' ''        Get_No_Faktur()

        ' ''        CloseConn()
        ' ''    End If

        ' ''    OpenConn()

        ' ''    Dim lv As New ListViewItem

        ' ''    SQL = "select c.cara_bayar, a.no_faktur, a.tanggal, a.tgl_jatuh_tempo, c.keterangan, "
        ' ''    SQL = SQL & "c.grand, c.no_val, a.kode_supplier, b.nama as namasupplier, d.byr from "
        ' ''    SQL = SQL & "pembelian_proyek a, suppliers b, val_pemb_proyek c, detail_val_pemb_proyek d where "
        ' ''    SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And b.kode_perusahaan = c.kode_perusahaan And "
        ' ''    SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan And a.kode_supplier = b.kode_supplier and "
        ' ''    SQL = SQL & "c.no_val = d.no_val and a.no_faktur = d.no_faktur and "
        ' ''    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and c.no_val = '" & TxtFaktur.Text.Trim & "' "
        ' ''    SQL = SQL & "order by d.urut "
        ' ''    Using Dr = OpenTrans(SQL)
        ' ''        If Dr.Read Then
        ' ''            DateTimePicker1.Enabled = False

        ' ''            DateTimePicker1.Value = Dr("tanggal")
        ' ''            TxtFaktur.Text = Dr("no_val")
        ' ''            TextBox2.Text = Dr("keterangan")
        ' ''            TextBoxa.Text = Format(Dr("grand"), "N0")
        ' ''            'For i As Integer = 0 To ComboBoxCb1.Items.Count - 1
        ' ''            '    If Dr("cara_bayar") = arrCrByr.Item(i) Then
        ' ''            '        ComboBoxCb1.SelectedIndex = i
        ' ''            '        Exit For
        ' ''            '    End If
        ' ''            'Next

        ' ''            ListView2.Items.Clear() : ListView1.Items.Clear()

        ' ''            lv = ListView1.Items.Add(Dr("no_faktur"))
        ' ''            lv.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
        ' ''            lv.SubItems.Add(Format(Dr("tgl_jatuh_tempo"), "dd MMM yyyy"))
        ' ''            lv.SubItems.Add(Dr("kode_supplier"))
        ' ''            lv.SubItems.Add(Dr("namasupplier"))
        ' ''            lv.SubItems.Add(Format(Dr("byr"), "N0"))

        ' ''            Do While Dr.Read
        ' ''                lv = ListView1.Items.Add(Dr("no_faktur"))
        ' ''                lv.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
        ' ''                lv.SubItems.Add(Format(Dr("tgl_jatuh_tempo"), "dd MMM yyyy"))
        ' ''                lv.SubItems.Add(Dr("kode_supplier"))
        ' ''                lv.SubItems.Add(Dr("namasupplier"))
        ' ''                lv.SubItems.Add(Format(Dr("byr"), "N0"))
        ' ''            Loop

        ' ''            Button1.Text = "&Update"
        ' ''        Else
        ' ''            Kosong()
        ' ''        End If

        ' ''    End Using

        ' ''    CloseConn()

        ' ''Catch ex As Exception
        ' ''    CloseConn()
        ' ''    MessageBox.Show(ex.Message)
        ' ''    Exit Sub
        ' ''End Try
    End Sub

    Private Sub TextBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress, Txt_DP.KeyPress

        If TextBox6.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            If e.KeyChar = Chr(13) Then

                '==================
                '=     CEK DP     =
                '==================
                Dim Flag As String = "T"
                Dim DPDigunakan As Double = 0
                SQL = ";with Cte as ( select a.Nilai as Nilai_DP, ( a.Nilai - "
                SQL = SQL & "ISNULL(( select sum(z.nilai) from Val_Pemb_Proyek_Detail_DP z, Val_Pemb_Proyek w "
                SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan and z.urut_DP = a.No_Urut "
                SQL = SQL & "and z.kode_perusahaan=w.kode_Perusahaan and z.no_val=w.no_val and w.status is null ), 0) ) as Sisa "
                SQL = SQL & "from EMI_Transaksi_Pembayaran_Dimuka_Detail_Proyek a, EMI_Transaksi_Pembayaran_Dimuka_Proyek b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "And a.No_Transaksi = b.No_Transaksi "
                SQL = SQL & "And b.Status Is null "
                SQL = SQL & "And a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Fak_PO in ( "
                SQL = SQL & "select x.No_PO "
                SQL = SQL & "from Pembelian_Proyek z, Barang_Masuk_proyek x "
                SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
                SQL = SQL & "and z.No_PO = x.No_Faktur "
                SQL = SQL & "and z.Status is null and x.Status is null "
                SQL = SQL & "and z.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and z.No_Faktur = '" & TextBox9.Text & "' "
                SQL = SQL & "group by x.No_PO ) "
                SQL = SQL & ") select isnull(sum(Sisa), 0) as Nilai_DP from Cte "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Not Val(HilangkanTanda(Dr("Nilai_DP"))) = 0 Then

                            '==============================================
                            '=     GET NILAI DP YANG SUDAH DI GUNAKAN     =
                            '==============================================
                            Dim UsedDP As Double = 0
                            For i As Integer = 0 To ListView1.Items.Count - 1
                                If ListView1.Items(i).SubItems(8).Text = "Y" Then
                                    UsedDP += Val(HilangkanTanda(ListView1.Items(i).SubItems(9).Text))
                                End If
                            Next

                            If Val(HilangkanTanda(TextBox6.Text)) <= (Val(HilangkanTanda(Dr("Nilai_DP"))) - UsedDP) Then
                                DPDigunakan = Val(HilangkanTanda(TextBox6.Text))
                            Else

                                If (Val(HilangkanTanda(Dr("Nilai_DP"))) - UsedDP) <= 0 Then
                                    CloseConn()
                                    MessageBox.Show("DP Tidak Cukup", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub

                                Else
                                    DPDigunakan = (Val(HilangkanTanda(Dr("Nilai_DP"))) - UsedDP)

                                End If
                            End If
                            Flag = "Y"
                        Else
                            Flag = "T"
                            DPDigunakan = 0
                        End If
                    Else
                        DPDigunakan = 0
                        Flag = "T"
                    End If
                End Using

                Dim kd_suppx As String = ""
                For i As Integer = 0 To ListView1.Items.Count - 1
                    If ListView1.Items(i).Text.Trim.ToUpper = TextBox9.Text.Trim.ToUpper Then
                        MessageBox.Show("Faktur ini sudah dimasukkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If i = 0 Then
                        kd_suppx = TextBox7.Text
                    Else
                        If kd_suppx <> TextBox7.Text Then
                            MessageBox.Show("supplier yang dimasukan harus sama!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End If
                Next

                Dim NTotal As Double = 0
                If Flag = "T" Then
                    NTotal = Val(HilangkanTanda(TextBox6.Text)) + Val(HilangkanTanda(0))
                Else
                    NTotal = Val(HilangkanTanda(Format(DPDigunakan, "N0"))) + Val(HilangkanTanda(0))
                End If



                Dim totalpph2 As Double = 0
                Dim totalppn2 As Double = 0
                SQL = "select b.Flag_PPN,b.Persentase from Detail_Pembelian_Proyek_PPH b,Pembelian_Proyek a "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.Status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & TextBox9.Text & "' "
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        If General_Class.CekNULL(dr("Flag_PPN")) = "" Then
                            totalpph2 = totalpph2 + dr("Persentase")
                        Else
                            totalppn2 = totalppn2 + dr("Persentase")
                        End If
                    Loop
                End Using
                Dim Persentase As Double = 1 + ((Val(totalppn2) / 100))
                'Dim Persentase As Double = 1 + ((Val(totalppn2) / 100) - (Val(totalpph2) / 100))
                Dim DPP As Double = NTotal / Persentase

                Dim Nppn As Double = 0
                Nppn = DPP * Val(totalppn2) / 100
                Dim HslNppn As Double = Format(Nppn, "N0")

                Dim Npph As Double = 0
                'Npph = DPP * Val(totalpph2) / 100
                Dim spph As Double = 0
                SQL = "select b.Flag_PPN,b.Persentase,b.Kode_Akun from Detail_Pembelian_Proyek_PPH b,Pembelian_Proyek a "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.Status is null and b.flag_ppn is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & TextBox9.Text & "' "
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        spph = Val(HilangkanTanda(Format(DPP * dr("Persentase") / 100, "N0")))
                        Npph = Npph + spph
                        Dim lvw As New ListViewItem
                        lvw = ListView4.Items.Add(TextBox9.Text)
                        lvw.SubItems.Add(Format(dr("Persentase"), "N2"))
                        lvw.SubItems.Add(Format(spph, "N2"))
                        lvw.SubItems.Add(dr("Kode_Akun"))
                    Loop
                End Using
                Dim HslNpph As Double = Format(Npph, "N0")

                'Dim totalpph As Double = 0
                'Dim totalppn As Double = 0
                'Dim znilai As Double = 0
                'SQL = "select b.Flag_PPN,b.Nilai,b.Persentase from Detail_Pembelian_Proyek_PPH b,Pembelian_Proyek a "
                'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.Status is null "
                'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & TextBox9.Text & "' "
                'Using dr = OpenTrans(SQL)
                '    Do While dr.Read
                '        znilai = Format(Val(HilangkanTanda(TextBox6.Text)) * dr("Persentase") / 100, "N0")
                '        If General_Class.CekNULL(dr("Flag_PPN")) = "" Then
                '            totalpph = totalpph + znilai
                '        Else
                '            totalppn = totalppn + znilai
                '        End If
                '    Loop
                'End Using





                Dim lv As New ListViewItem
                lv = ListView1.Items.Add(TextBox9.Text)
                lv.SubItems.Add(TextBox3.Text)
                lv.SubItems.Add(TextBox4.Text)
                lv.SubItems.Add(TextBox7.Text)
                lv.SubItems.Add(TextBox5.Text)

                If Flag = "T" Then
                    lv.SubItems.Add(Format(Val(TextBox6.Text), "N0"))
                Else
                    lv.SubItems.Add(Format(DPDigunakan, "N0"))
                End If

                'lv.SubItems.Add(Format(DPP, "N0"))
                lv.SubItems.Add(HslNppn) 'Nilai PPN
                lv.SubItems.Add(HslNpph) ' Nilai PPh
                lv.SubItems.Add(Flag) ' Flag DP

                If Flag = "T" Then
                    lv.SubItems.Add(0)
                Else
                    lv.SubItems.Add(Format(DPDigunakan, "N0"))
                End If



                Hitung()
                Kosong_Bawah()
                ListView2.Focus()
            End If
            If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub ComboBoxCb1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then ComboBox1.Focus()
    End Sub

    Private Sub ComboBoxCb1_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then ComboBox1.Focus()
    End Sub

    Private Sub ListView2_ItemChecked(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckedEventArgs) Handles ListView2.ItemChecked
        If ListView2.Items.Count = 0 Or ListView2.CheckedItems.Count = 0 Then
            ListView1.Items.Clear()
            Hitung()
            Exit Sub
        End If

        For i As Integer = 0 To ListView2.Items.Count - 1
            Get_Isi_Listvie_Atas(i)
            If ListView2.Items(i).Checked = False Then
                If ListView1.Items.Count = 0 Then
                    Exit Sub
                Else
                    For a As Integer = 0 To ListView1.Items.Count - 1
                        Get_Isi_Listview(a)
                        If LvAtas_Fak = LvFak Then
                            ListView1.Items(a).Remove()
                            Exit For
                        End If
                    Next
                End If
            End If
        Next

        Hitung()
    End Sub

    Private Sub TextBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyDown
        If e.KeyCode = Keys.Down Then
            ListView3.Focus()
        End If
    End Sub

    Private Sub TextBox8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox8.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox8.Text.Trim.Length = 0 Then
                ListView3.Visible = False : TextBox8.Focus() : Exit Sub
            End If
            TextBox8_Leave(TextBox4, e)
            Button1.Focus()
        End If
    End Sub

    Private Sub TextBox8_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox8.Leave
        If ListView3.Focused = True Then Exit Sub
        TextBox8.Text = "" : TextBox10.Text = "" : ComboBox3.SelectedIndex = -1
    End Sub

    Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged
        If TextBox8.Text.Trim.Length = 0 Then
            ListView3.Visible = False : Exit Sub
        Else
            ListView3.Visible = True
        End If

        ListView3.Items.Clear()
        Dim lv As New ListViewItem

        Try
            OpenConn()

            SQL = "select no_rekening, nama, kode_bank, Alamat_Penerima, Kota_Penerima, "
            SQL = SQL & "Negara_Penerima, Telp_Penerima from rekening_tujuan where kode_perusahaan = '" & KodePerusahaan & "' and nama like '%" & TextBox8.Text & "%' order by nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = ListView3.Items.Add(Dr("no_rekening"))
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

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
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
        ElseIf TextBox8.Text.Trim.Length = 0 Then
            MessageBox.Show("Nama Tujuan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox8.Focus()
            Exit Sub
        ElseIf ComboBox3.SelectedIndex = -1 Then
            MessageBox.Show("Bank Tujuan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox8.Focus()
            Exit Sub
        ElseIf TextBox10.Text.Trim.Length = 0 Then
            MessageBox.Show("No Rek Tujuan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox10.Focus()
            Exit Sub
        End If

        If Button1.Text = "&Simpan" Then
            Dim tny As String = MessageBox.Show("Yakin akan disimpan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If tny = vbNo Then Exit Sub

            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekSudahTutupSaldo(DateTimePicker1.Value) = "Y" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Sudah tutup saldo di bulan ini.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If


                Get_No_Faktur()
                Get_No_Faktur_Pengajuan()

                Dim SisaHutang As Double = 0
                Dim JT As String = ""
                Dim KodeCust As String = ""

                Dim Kode_Voucher As String = "NULL" ' GetLastNumberJurnal(Format(DateTimePicker1.Value, "yyyyMM"), fJU & fValPemb, KodePerusahaan)

                Dim pagenumber As Integer = 0

                'SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                'SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                'SQL = SQL & "'" & Kode_Voucher & "', "
                'SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                'SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                'SQL = SQL & "'" & KodeProyek & "', 'Pelunasan hutang " & TxtFaktur.Text.Trim & "', '', "
                'SQL = SQL & "'-', '" & UserID & "')"
                'ExecuteTrans(SQL)

                Dim coa_hutang As String = ""
                Dim lks As String = ""
                For i As Integer = 0 To ListView1.Items.Count - 1
                    Get_Isi_Listview(i)


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
                    SQL = SQL & "no_faktur = '" & LvFak.Trim & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            SisaHutang = Dr("grand") - Dr("ttl_retur") - Dr("ttl_validasi") - Dr("Ttl_pph")
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
                            ElseIf SisaHutang < Val(HilangkanTanda(LvJml)) Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pembayaran tidak boleh lebih dari sisa hutang. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf KodeCust <> LvKdCus Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Supplier sudah diubah sebelumnya. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                            If Dr("hutang") - Val(HilangkanTanda(LvJml)) < 0 Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat hutang " & LvNmCus & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            Else
                                Dr.Close()
                                'kurangin hutangnya
                                SQL = "update suppliers set hutang = hutang - " & HilangkanTanda(LvJml) & " where "
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

                    If SisaHutang = Val(HilangkanTanda(LvJml)) Then
                        SQL = "Update pembelian_proyek set flag_lunas = 'Y', "
                        SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                        SQL = SQL & "jam_lunas = '" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                        SQL = SQL & "uservalidasi = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "no_faktur = '" & LvFak.Trim & "'"
                        ExecuteTrans(SQL)
                    End If


                    SQL = "select top(1) hutang from stock_owner_proyek where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_stock_owner = '" & lks & "'"
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            coa_hutang = dr("hutang")
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data Lokasi_Proyek tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    ''    SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    ''    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "'"
                    ''    Using Dr = OpenTrans(SQL)
                    ''        If Dr.Read Then
                    ''            Dr.Close()
                    ''            'update 

                    ''            SQL = "update detail_jurnal set debit = debit+ " & HilangkanTanda(LvJml) & " where "
                    ''            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    ''            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "'"
                    ''            ExecuteTrans(SQL)
                    ''        Else
                    ''            Dr.Close()
                    ''            'insert

                    ''            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_hutang, 1), _
                    ''                  Strings.Mid(coa_hutang, 2, 1), _
                    ''                  Strings.Mid(Ganti(coa_hutang), 3), _
                    ''                  KodePerusahaan, KodeProyek, "Pelunasan hutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, HilangkanTanda(LvJml), "0", pagenumber + 1)
                    ''            ExecuteTrans(SQL)
                    ''        End If
                    ''    End Using
                Next

                SQL = "insert into val_pemb_proyek(kode_perusahaan, no_val, tanggal, jam, "
                SQL = SQL & "keterangan, uservalidasi, grand, kode_voucher, cara_bayar, No_Pengajuan, Kode_Bank_Tujuan, "
                SQL = SQL & "No_Rek_Tujuan, Nama_Penerima, Alamat_Penerima, Kota_Penerima, Negara_Penerima, Telp_Penerima) values('" & KodePerusahaan & "', "
                SQL = SQL & "'" & TxtFaktur.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                SQL = SQL & "'" & TextBox2.Text.Trim & "', '" & UserID & "', "
                '   SQL = SQL & "'" & ArrCB1.Item(ComboBoxCb1.SelectedIndex) & "', "
                SQL = SQL & "" & HilangkanTanda(TextBoxa.Text) & ", '', "
                SQL = SQL & "NULL, '" & no_fakturPengajuan & "', '" & ComboBox3.Text & "', '" & TextBox10.Text & "' , '" & TextBox8.Text & "', '" & x_alamat & "',"
                SQL = SQL & "'" & x_Kota & "', '" & x_negara & "' , '" & x_telp & "')"
                ExecuteTrans(SQL)

                SQL = "INSERT INTO pengajuan_Temp(kode_perusahaan, no_pengajuan, tanggal, jam, keterangan, userid, grand, pbk, "
                SQL = SQL & "Validasi,Flag_Proyek) "
                SQL = SQL & "values('" & KodePerusahaan & "', '" & no_fakturPengajuan & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & TextBox2.Text & "', '" & UserID & "', "
                SQL = SQL & "" & HilangkanTanda(TextBoxa.Text) & ", 'T', NULL,'Y')"
                ExecuteTrans(SQL)

                For i As Integer = 0 To ListView1.Items.Count - 1
                    Get_Isi_Listview(i)

                    SQL = "insert into detail_val_pemb_proyek(kode_perusahaan, no_val, no_faktur, byr) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
                    SQL = SQL & "'" & LvFak.Trim & "', " & HilangkanTanda(LvJml) & ")"
                    ExecuteTrans(SQL)

                    Dim x_no_urut_detail_pelunasan As Integer = 0
                    SQL = "select IDENT_CURRENT('detail_val_pemb_proyek') as urutan"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            x_no_urut_detail_pelunasan = Dr("urutan")
                        End If
                    End Using

                    SQL = "select urut from detail_val_pemb_proyek where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "No_Val = '" & TxtFaktur.Text.Trim & "' and urut = '" & x_no_urut_detail_pelunasan & "'"
                    Using Dr = OpenTrans(SQL)
                        If Not Dr.Read Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Harap ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    'Dim Nilai_Total As Double = ((Val(HilangkanTanda(LvJml)) - Val(HilangkanTanda(LvPPN))) + Val(HilangkanTanda(LvPPH)))
                    Dim Nilai_Total As Double = (Val(HilangkanTanda(LvJml)) + Val(HilangkanTanda(LvPPH)))
                    'Dim Nilai_Total As Double = ((Val(HilangkanTanda(LvKursLamaTot)) + Val(HilangkanTanda(LvNilaiPPN))) - Val(HilangkanTanda(LvNilaiPPH))) + selisih
                    SQL = "INSERT INTO detail_pengajuan_Temp(kode_perusahaan, no_pengajuan, kode_master_acc, kode_acc, kode_detail_acc, "
                    SQL = SQL & "keterangan_detail, tgl_jatuh_tempo, jumlah, kode_bank_tujuan, no_rek_tujuan, nama_penerima, "
                    SQL = SQL & "Alamat_Penerima, Kota_Penerima, Negara_Penerima, Telp_Penerima, Lokasi, Kode_Account, "
                    SQL = SQL & "Id_Cost_Center, No_Pelunasan, Urut_Pelunasan, Tgl_Bayar) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & no_fakturPengajuan & "', '" & Strings.Left(coa_hutang, 1) & "', "
                    SQL = SQL & "'" & Strings.Mid(coa_hutang, 2, 1) & "', '" & Strings.Mid(Ganti(coa_hutang), 3) & "', "
                    SQL = SQL & "'" & TextBox2.Text & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', " & HilangkanTanda(LvJml) & ", "
                    SQL = SQL & "'" & ComboBox3.Text & "', '" & TextBox10.Text & "', '" & TextBox8.Text & "', "
                    SQL = SQL & "'" & x_alamat & "', '" & x_Kota & "', '" & x_negara & "', '" & x_telp & "','" & Ket_Lokasi_HO_Proyek & "','" & coa_hutang & "', "
                    SQL = SQL & "'" & Ket_Cost_Center_HO_Proyek & "', '" & TxtFaktur.Text.Trim & "', '" & x_no_urut_detail_pelunasan & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "') "
                    ExecuteTrans(SQL)

                    Dim x_no_urut_det_pengajuan As Integer = 0
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

                    If Val(HilangkanTanda(LvJml)) <> 0 Then
                        SQL = "insert into Detail_Pengajuan5_Temp(Kode_Perusahaan,Urut_Detail_Pengajuan, Kode_Account,Debit, Kredit, Keterangan,Tgl, lokasi)"
                        SQL = SQL & "values('" & KodePerusahaan & "', '" & x_no_urut_det_pengajuan & "', '" & coa_hutang & "', '" & HilangkanTanda(Nilai_Total) & "','0', "
                        SQL = SQL & "'Pelunasan " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Ket_Lokasi_HO & "')"
                        ExecuteTrans(SQL)
                    End If

                    Dim NTotal As Double = 0
                    NTotal = Val(HilangkanTanda(LvJml)) + Val(HilangkanTanda(0))

                    Dim totalpph2 As Double = 0
                    Dim totalppn2 As Double = 0
                    SQL = "select b.Flag_PPN,b.Persentase from Detail_Pembelian_Proyek_PPH b,Pembelian_Proyek a "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.Status is null "
                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & LvFak & "' "
                    Using dr = OpenTrans(SQL)
                        Do While dr.Read
                            If General_Class.CekNULL(dr("Flag_PPN")) = "" Then
                                totalpph2 = totalpph2 + dr("Persentase")
                            Else
                                totalppn2 = totalppn2 + dr("Persentase")
                            End If
                        Loop
                    End Using
                    Dim Persentase As Double = 1 + ((Val(totalppn2) / 100) - (Val(totalpph2) / 100))
                    Dim DPP As Double = NTotal / Persentase

                    Dim znilai As Double = 0
                    Dim xnilai As Double = 0
                    'Dim persen As Double = 0

                    'If Val(HilangkanTanda(LvPPN)) <> 0 Then
                    '    Dim zppn As Double = 0
                    '    SQL = "select b.Kode_Akun,b.Nilai,a.Lokasi,b.Persentase from Detail_Pembelian_Proyek_PPH b,Pembelian_Proyek a "
                    '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.Status is null "
                    '    SQL = SQL & "and b.Flag_PPN = 'Y' and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & LvFak & "' "
                    '    Using ds = BindingTrans(SQL)
                    '        With ds.Tables("MyTable")
                    '            For index As Integer = 0 To .Rows.Count - 1
                    '                znilai = Val(DPP) * .Rows(index).Item("Persentase") / 100
                    '                xnilai = Val(HilangkanTanda(Format(znilai, "N0")))
                    '                SQL = "insert into Detail_Pengajuan5_Temp(Kode_Perusahaan,Urut_Detail_Pengajuan, Kode_Account,Debit, Kredit, Keterangan,Tgl, lokasi)"
                    '                SQL = SQL & "values('" & KodePerusahaan & "', '" & x_no_urut_det_pengajuan & "', '" & .Rows(index).Item("Kode_Akun") & "', '" & HilangkanTanda(xnilai) & "','0', "
                    '                SQL = SQL & "'PPN " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Ket_Lokasi_HO & "')"
                    '                ExecuteTrans(SQL)
                    '            Next
                    '        End With
                    '    End Using
                    'End If

                    If Val(HilangkanTanda(LvPPH)) <> 0 Then
                        SQL = "select b.Kode_Akun,b.Nilai,a.Lokasi,b.Persentase,d.Keterangan from Detail_Pembelian_Proyek_PPH b,Pembelian_Proyek a, EMI_Master_Pajak d  "
                        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.Status is null "
                        SQL = SQL & "and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Tarif = d.Kode_Tarif "
                        SQL = SQL & "and b.Flag_PPN is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & LvFak & "' "
                        Using ds = BindingTrans(SQL)
                            With ds.Tables("MyTable")
                                For index As Integer = 0 To .Rows.Count - 1
                                    znilai = Val(DPP) * .Rows(index).Item("Persentase") / 100
                                    xnilai = Val(HilangkanTanda(Format(znilai, "N0")))
                                    SQL = "insert into Detail_Pengajuan5_Temp(Kode_Perusahaan,Urut_Detail_Pengajuan, Kode_Account,Debit, Kredit, Keterangan,Tgl, lokasi)"
                                    SQL = SQL & "values('" & KodePerusahaan & "', '" & x_no_urut_det_pengajuan & "', '" & .Rows(index).Item("Kode_Akun") & "', '0', '" & HilangkanTanda(xnilai) & "', "
                                    SQL = SQL & "'" & .Rows(index).Item("Keterangan") & " " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Ket_Lokasi_HO & "')"
                                    ExecuteTrans(SQL)
                                Next
                            End With
                        End Using
                    End If

                    SQL = "select round(sum(debit), 2) - round(sum(kredit), 2) as data from Detail_Pengajuan5_Temp where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "Urut_Detail_Pengajuan = '" & x_no_urut_det_pengajuan & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If Dr("data") <> Val(HilangkanTanda(LvJml)) Then
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
                Next

                'SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex), 1), _
                '               Strings.Mid(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex), 2, 1), _
                '               Strings.Mid(Ganti(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex)), 3), _
                '               KodePerusahaan, KodeProyek, "Pelunasan hutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, "0", HilangkanTanda(TextBoxa.Text), pagenumber + 1)
                'ExecuteTrans(SQL)

                'SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "kode_voucher = '" & Kode_Voucher & "'"
                'Using Dr = OpenTrans(SQL)
                '    If Dr.Read Then
                '        If Dr("debit") <> Dr("kredit") Then
                '            Dr.Close()
                '            CloseTrans()
                '            CloseConn()
                '            MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            Exit Sub
                '        End If
                '    Else
                '        Dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using


                '24-04-2025
                'SQL = "INSERT INTO pengajuan(kode_perusahaan, no_pengajuan, tanggal, jam, keterangan, userid, grand, pbk, Validasi) "
                'SQL = SQL & "values('" & KodePerusahaan & "', '" & no_fakturPengajuan & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                'SQL = SQL & "'" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & TextBox2.Text.Trim & "', '" & UserID & "', "
                'SQL = SQL & "" & HilangkanTanda(TextBoxa.Text) & ", 'T', NULL)"
                'ExecuteTrans(SQL)


                'SQL = "INSERT INTO detail_pengajuan(kode_perusahaan, no_pengajuan, kode_master_acc, kode_acc, kode_detail_acc, "
                'SQL = SQL & "keterangan_detail, tgl_jatuh_tempo, jumlah, kode_bank_tujuan, no_rek_tujuan, nama_penerima, "
                'SQL = SQL & "Alamat_Penerima, Kota_Penerima, Negara_Penerima, Telp_Penerima, Lokasi, Kode_Account, id_cost_center) "
                'SQL = SQL & "values('" & KodePerusahaan & "', '" & no_fakturPengajuan & "', '" & Strings.Left(coa_hutang, 1) & "', "
                'SQL = SQL & "'" & Strings.Mid(coa_hutang, 2, 1) & "', '" & Strings.Mid(Ganti(coa_hutang), 3) & "', "
                'SQL = SQL & "'" & TextBox2.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', " & HilangkanTanda(TextBoxa.Text) & ", "
                'SQL = SQL & "'" & ComboBox3.Text & "', '" & TextBox10.Text & "', '" & TextBox8.Text & "', "
                'SQL = SQL & "'" & x_alamat & "', '" & x_Kota & "', '" & x_negara & "', '" & x_telp & "','" & Ket_Lokasi_HO_Proyek & "','" & coa_hutang & "', '" & Ket_Cost_Center_HO_Proyek & "') "
                'ExecuteTrans(SQL)
                '24-04-2025

                Cmd.Transaction.Commit()

                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        Else 'update

            Dim tny As String = MessageBox.Show("Yakin akan diupdate?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If tny = vbNo Then Exit Sub

            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekButtonRole("update_pelunasan_pembelian") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                Dim kode_voucher_lama As String = ""
                SQL = "Select kode_voucher, status from val_pemb_proyek where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_val = '" & TxtFaktur.Text.Trim & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            kode_voucher_lama = .Rows(0).Item("kode_voucher")
                            If General_Class.CekNULL(.Rows(0).Item("status")) = "Y" Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pelunasan tidak bisa diupdate, karena sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            End If
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Transaksi tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

                SQL = "select a.kode_supplier, a.no_faktur, b.no_val, b.byr from pembelian_proyek a, detail_val_pemb_proyek b where "
                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And a.no_faktur = b.no_faktur And "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "b.no_val = '" & TxtFaktur.Text.Trim & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                SQL = "select hutang from suppliers where kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_supplier = '" & .Rows(i).Item("kode_supplier") & "'"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        Dr.Close()

                                        SQL = "update suppliers set hutang = hutang + " & .Rows(i).Item("byr") & " where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_supplier = '" & .Rows(i).Item("kode_supplier") & "'"
                                        ExecuteTrans(SQL)
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Supplier tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Exit Sub
                                    End If
                                End Using

                                SQL = "Update pembelian_proyek set flag_lunas = NULL, "
                                SQL = SQL & "Tgl_lunas = NULL, "
                                SQL = SQL & "jam_lunas = NULL, "
                                SQL = SQL & "uservalidasi = NULL where kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "no_faktur = '" & .Rows(i).Item("no_faktur") & "'"
                                ExecuteTrans(SQL)
                            Next
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Pelunasan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using


                '=============

                SQL = "delete from detail_val_pemb_proyek where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_val = '" & TxtFaktur.Text.Trim & "'"
                ExecuteTrans(SQL)

                Dim SisaHutang As Double = 0
                Dim JT As String = ""
                Dim KodeCust As String = ""

                For i As Integer = 0 To ListView1.Items.Count - 1
                    Get_Isi_Listview(i)

                    SQL = "select a.grand, a.status, a.jenis_transaksi, a.kode_supplier, "
                    SQL = SQL & "isnull((select sum(x.grand) as ttl_retur from retur_pembelian x where x.kode_perusahaan = a.kode_perusahaan and x.no_faktur_beli = a.no_faktur and x.status is null), 0) as ttl_retur, "
                    SQL = SQL & "isnull((select sum(y.byr) from val_pemb_proyek x, detail_val_pemb y where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_val = y.no_val and "
                    SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
                    SQL = SQL & "y.no_faktur = a.no_faktur), 0) as ttl_validasi "
                    SQL = SQL & "from pembelian_proyek a where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_faktur = '" & LvFak.Trim & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            SisaHutang = Dr("grand") - Dr("ttl_retur") - Dr("ttl_validasi")
                            JT = Dr("jenis_transaksi")
                            KodeCust = Dr("kode_supplier")

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
                            ElseIf SisaHutang < Val(HilangkanTanda(LvJml)) Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pembayaran tidak boleh lebih dari sisa hutang. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf KodeCust <> LvKdCus Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Supplier sudah diubah sebelumnya. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                            If Dr("hutang") - Val(HilangkanTanda(LvJml)) < 0 Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat hutang " & LvNmCus & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            Else
                                Dr.Close()
                                'kurangin hutangnya
                                SQL = "update suppliers set hutang = hutang - " & HilangkanTanda(LvJml) & " where "
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

                    If SisaHutang = Val(HilangkanTanda(LvJml)) Then
                        SQL = "Update pembelian_proyek set flag_lunas = 'Y', "
                        SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                        SQL = SQL & "jam_lunas = '" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                        SQL = SQL & "uservalidasi = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "no_faktur = '" & LvFak.Trim & "'"
                        ExecuteTrans(SQL)
                    End If
                Next

                SQL = "update val_pemb_proyek set keterangan = '" & TextBox2.Text.Trim & "', "
                'SQL = SQL & "cara_bayar = '" & arrCrByr.Item(ComboBoxCb1.SelectedIndex) & "', "
                SQL = SQL & "grand = " & HilangkanTanda(TextBoxa.Text) & " where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_val = '" & TxtFaktur.Text.Trim & "'"
                ExecuteTrans(SQL)

                For i As Integer = 0 To ListView1.Items.Count - 1
                    Get_Isi_Listview(i)

                    SQL = "insert into detail_val_pemb_proyek(kode_perusahaan, no_val, no_faktur, byr) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
                    SQL = SQL & "'" & LvFak.Trim & "', " & HilangkanTanda(LvJml) & ")"
                    ExecuteTrans(SQL)
                Next

                Dim coa_hutang As String = ""

                SQL = "select top(1) hutang from stock_owner_proyek where kode_perusahaan = '" & KodePerusahaan & "'"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        coa_hutang = dr("hutang")
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Lokasi_Proyek tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                'Dim pagenumber As Integer = 0

                'SQL = "delete from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "kode_voucher = '" & kode_voucher_lama & "'"
                'ExecuteTrans(SQL)

                'SQL = Get_Detail_Jurnal(kode_voucher_lama, Strings.Left(coa_hutang, 1), _
                '               Strings.Mid(coa_hutang, 2, 1), _
                '               Strings.Mid(Ganti(coa_hutang), 3), _
                '               KodePerusahaan, KodeProyek, "Pelunasan hutang " & TxtFaktur.Text.Trim, HilangkanTanda(TextBoxa.Text), "0", pagenumber + 1)
                'ExecuteTrans(SQL)


                'SQL = Get_Detail_Jurnal(kode_voucher_lama, Strings.Left(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex), 1), _
                '               Strings.Mid(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex), 2, 1), _
                '               Strings.Mid(Ganti(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex)), 3), _
                '               KodePerusahaan, KodeProyek, "Pelunasan hutang " & TxtFaktur.Text.Trim, "0", HilangkanTanda(TextBoxa.Text), pagenumber + 1)
                'ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
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
        ElseIf TextBox8.Text.Trim.Length = 0 Then
            MessageBox.Show("Nama Tujuan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox8.Focus()
            Exit Sub
        ElseIf ComboBox3.SelectedIndex = -1 Then
            MessageBox.Show("Bank Tujuan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox8.Focus()
            Exit Sub
        ElseIf TextBox10.Text.Trim.Length = 0 Then
            MessageBox.Show("No Rek Tujuan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox10.Focus()
            Exit Sub
        End If

        If Button1.Text = "&Simpan" Then
            Dim tny As String = MessageBox.Show("Yakin akan disimpan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If tny = vbNo Then Exit Sub

            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekSudahTutupSaldo(DateTimePicker1.Value) = "Y" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Sudah tutup saldo di bulan ini.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If


                Get_No_Faktur()
                Get_No_Faktur_Pengajuan()

                Dim SisaHutang As Double = 0
                Dim JT As String = ""
                Dim KodeCust As String = ""

                Dim Kode_Voucher As String = "NULL" ' GetLastNumberJurnal(Format(DateTimePicker1.Value, "yyyyMM"), fJU & fValPemb, KodePerusahaan)

                Dim pagenumber As Integer = 0

                'SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                'SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                'SQL = SQL & "'" & Kode_Voucher & "', "
                'SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                'SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                'SQL = SQL & "'" & KodeProyek & "', 'Pelunasan hutang " & TxtFaktur.Text.Trim & "', '', "
                'SQL = SQL & "'-', '" & UserID & "')"
                'ExecuteTrans(SQL)

                Dim coa_hutang As String = ""
                Dim lks As String = ""
                For i As Integer = 0 To ListView1.Items.Count - 1
                    Get_Isi_Listview(i)


                    SQL = "select a.lokasi, a.grand, a.status, a.jenis_transaksi, a.kode_supplier, "
                    SQL = SQL & "isnull((select sum(x.grand) as ttl_retur from retur_pembelian x where x.kode_perusahaan = a.kode_perusahaan and x.no_faktur_beli = a.no_faktur and x.status is null), 0) as ttl_retur, "
                    SQL = SQL & "isnull((select sum(y.byr) from val_pemb_proyek x, detail_val_pemb_proyek y where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_val = y.no_val and "
                    SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
                    SQL = SQL & "y.no_faktur = a.no_faktur), 0) as ttl_validasi "
                    SQL = SQL & "from pembelian_proyek a where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_faktur = '" & LvFak.Trim & "' "
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
                            ElseIf SisaHutang < Val(HilangkanTanda(LvJml)) Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pembayaran tidak boleh lebih dari sisa hutang. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf KodeCust <> LvKdCus Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Supplier sudah diubah sebelumnya. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                            If Dr("hutang") - Val(HilangkanTanda(LvJml)) < 0 Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat hutang " & LvNmCus & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            Else
                                Dr.Close()
                                'kurangin hutangnya
                                SQL = "update suppliers set hutang = hutang - " & HilangkanTanda(LvJml) & " where "
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

                    If SisaHutang = Val(HilangkanTanda(LvJml)) Then
                        SQL = "Update pembelian_proyek set flag_lunas = 'Y', "
                        SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                        SQL = SQL & "jam_lunas = '" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                        SQL = SQL & "uservalidasi = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "no_faktur = '" & LvFak.Trim & "'"
                        ExecuteTrans(SQL)
                    End If




                    SQL = "select top(1) hutang from stock_owner_proyek where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_stock_owner = '" & lks & "'"
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            coa_hutang = dr("hutang")
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data Lokasi_Proyek tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    ''    SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    ''    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "'"
                    ''    Using Dr = OpenTrans(SQL)
                    ''        If Dr.Read Then
                    ''            Dr.Close()
                    ''            'update 

                    ''            SQL = "update detail_jurnal set debit = debit+ " & HilangkanTanda(LvJml) & " where "
                    ''            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''            SQL = SQL & "kode_voucher = '" & Kode_Voucher & "' and "
                    ''            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_hutang & "'"
                    ''            ExecuteTrans(SQL)
                    ''        Else
                    ''            Dr.Close()
                    ''            'insert

                    ''            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_hutang, 1), _
                    ''                  Strings.Mid(coa_hutang, 2, 1), _
                    ''                  Strings.Mid(Ganti(coa_hutang), 3), _
                    ''                  KodePerusahaan, KodeProyek, "Pelunasan hutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, HilangkanTanda(LvJml), "0", pagenumber + 1)
                    ''            ExecuteTrans(SQL)
                    ''        End If
                    ''    End Using
                Next

                SQL = "insert into val_pemb_proyek(kode_perusahaan, no_val, tanggal, jam, "
                SQL = SQL & "keterangan, uservalidasi, grand, kode_voucher, cara_bayar, No_Pengajuan, Kode_Bank_Tujuan, "
                SQL = SQL & "No_Rek_Tujuan, Nama_Penerima, Alamat_Penerima, Kota_Penerima, Negara_Penerima, Telp_Penerima) values('" & KodePerusahaan & "', "
                SQL = SQL & "'" & TxtFaktur.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                SQL = SQL & "'" & TextBox2.Text.Trim & "', '" & UserID & "', "
                '   SQL = SQL & "'" & ArrCB1.Item(ComboBoxCb1.SelectedIndex) & "', "
                SQL = SQL & "" & HilangkanTanda(TextBoxa.Text) & ", " & Kode_Voucher & ", "
                SQL = SQL & "NULL, '" & no_fakturPengajuan & "', '" & ComboBox3.Text & "', '" & TextBox10.Text & "' , '" & TextBox8.Text & "', '" & x_alamat & "',"
                SQL = SQL & "'" & x_Kota & "', '" & x_negara & "' , '" & x_telp & "')"
                ExecuteTrans(SQL)

                For i As Integer = 0 To ListView1.Items.Count - 1
                    Get_Isi_Listview(i)

                    SQL = "insert into detail_val_pemb_proyek(kode_perusahaan, no_val, no_faktur, byr) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
                    SQL = SQL & "'" & LvFak.Trim & "', " & HilangkanTanda(LvJml) & ")"
                    ExecuteTrans(SQL)
                Next




                'SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex), 1), _
                '               Strings.Mid(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex), 2, 1), _
                '               Strings.Mid(Ganti(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex)), 3), _
                '               KodePerusahaan, KodeProyek, "Pelunasan hutang " & TxtFaktur.Text.Trim & ";" & TextBox2.Text.Trim, "0", HilangkanTanda(TextBoxa.Text), pagenumber + 1)
                'ExecuteTrans(SQL)

                'SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "kode_voucher = '" & Kode_Voucher & "'"
                'Using Dr = OpenTrans(SQL)
                '    If Dr.Read Then
                '        If Dr("debit") <> Dr("kredit") Then
                '            Dr.Close()
                '            CloseTrans()
                '            CloseConn()
                '            MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            Exit Sub
                '        End If
                '    Else
                '        Dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using


                SQL = "INSERT INTO pengajuan(kode_perusahaan, no_pengajuan, tanggal, jam, keterangan, userid, grand, pbk, Validasi) "
                SQL = SQL & "values('" & KodePerusahaan & "', '" & no_fakturPengajuan & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & TextBox2.Text.Trim & "', '" & UserID & "', "
                SQL = SQL & "" & HilangkanTanda(TextBoxa.Text) & ", 'T', NULL)"
                ExecuteTrans(SQL)


                SQL = "INSERT INTO detail_pengajuan(kode_perusahaan, no_pengajuan, kode_master_acc, kode_acc, kode_detail_acc, "
                SQL = SQL & "keterangan_detail, tgl_jatuh_tempo, jumlah, kode_bank_tujuan, no_rek_tujuan, nama_penerima, "
                SQL = SQL & "Alamat_Penerima, Kota_Penerima, Negara_Penerima, Telp_Penerima, Lokasi, Kode_Account, id_cost_center) "
                SQL = SQL & "values('" & KodePerusahaan & "', '" & no_fakturPengajuan & "', '" & Strings.Left(coa_hutang, 1) & "', "
                SQL = SQL & "'" & Strings.Mid(coa_hutang, 2, 1) & "', '" & Strings.Mid(Ganti(coa_hutang), 3) & "', "
                SQL = SQL & "'" & TextBox2.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', " & HilangkanTanda(TextBoxa.Text) & ", "
                SQL = SQL & "'" & ComboBox3.Text & "', '" & TextBox10.Text & "', '" & TextBox8.Text & "', "
                SQL = SQL & "'" & x_alamat & "', '" & x_Kota & "', '" & x_negara & "', '" & x_telp & "','" & Ket_Lokasi_HO_Proyek & "','" & coa_hutang & "', '" & Ket_Cost_Center_HO_Proyek & "') "
                ExecuteTrans(SQL)


                Cmd.Transaction.Commit()

                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        Else 'update

            Dim tny As String = MessageBox.Show("Yakin akan diupdate?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If tny = vbNo Then Exit Sub

            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekButtonRole("update_pelunasan_pembelian") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                Dim kode_voucher_lama As String = ""
                SQL = "Select kode_voucher, status from val_pemb_proyek where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_val = '" & TxtFaktur.Text.Trim & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            kode_voucher_lama = .Rows(0).Item("kode_voucher")
                            If General_Class.CekNULL(.Rows(0).Item("status")) = "Y" Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pelunasan tidak bisa diupdate, karena sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            End If
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Transaksi tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

                SQL = "select a.kode_supplier, a.no_faktur, b.no_val, b.byr from pembelian_proyek a, detail_val_pemb_proyek b where "
                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And a.no_faktur = b.no_faktur And "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "b.no_val = '" & TxtFaktur.Text.Trim & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                SQL = "select hutang from suppliers where kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_supplier = '" & .Rows(i).Item("kode_supplier") & "'"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        Dr.Close()

                                        SQL = "update suppliers set hutang = hutang + " & .Rows(i).Item("byr") & " where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_supplier = '" & .Rows(i).Item("kode_supplier") & "'"
                                        ExecuteTrans(SQL)
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Supplier tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Exit Sub
                                    End If
                                End Using

                                SQL = "Update pembelian_proyek set flag_lunas = NULL, "
                                SQL = SQL & "Tgl_lunas = NULL, "
                                SQL = SQL & "jam_lunas = NULL, "
                                SQL = SQL & "uservalidasi = NULL where kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "no_faktur = '" & .Rows(i).Item("no_faktur") & "'"
                                ExecuteTrans(SQL)
                            Next
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Pelunasan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using


                '=============

                SQL = "delete from detail_val_pemb_proyek where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_val = '" & TxtFaktur.Text.Trim & "'"
                ExecuteTrans(SQL)

                Dim SisaHutang As Double = 0
                Dim JT As String = ""
                Dim KodeCust As String = ""

                For i As Integer = 0 To ListView1.Items.Count - 1
                    Get_Isi_Listview(i)

                    SQL = "select a.grand, a.status, a.jenis_transaksi, a.kode_supplier, "
                    SQL = SQL & "isnull((select sum(x.grand) as ttl_retur from retur_pembelian x where x.kode_perusahaan = a.kode_perusahaan and x.no_faktur_beli = a.no_faktur and x.status is null), 0) as ttl_retur, "
                    SQL = SQL & "isnull((select sum(y.byr) from val_pemb_proyek x, detail_val_pemb y where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_val = y.no_val and "
                    SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and x.status is null and "
                    SQL = SQL & "y.no_faktur = a.no_faktur), 0) as ttl_validasi "
                    SQL = SQL & "from pembelian_proyek a where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_faktur = '" & LvFak.Trim & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            SisaHutang = Dr("grand") - Dr("ttl_retur") - Dr("ttl_validasi")
                            JT = Dr("jenis_transaksi")
                            KodeCust = Dr("kode_supplier")

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
                            ElseIf SisaHutang < Val(HilangkanTanda(LvJml)) Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pembayaran tidak boleh lebih dari sisa hutang. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf KodeCust <> LvKdCus Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Supplier sudah diubah sebelumnya. Pelunasan tidak dapat di lanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                            If Dr("hutang") - Val(HilangkanTanda(LvJml)) < 0 Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat hutang " & LvNmCus & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            Else
                                Dr.Close()
                                'kurangin hutangnya
                                SQL = "update suppliers set hutang = hutang - " & HilangkanTanda(LvJml) & " where "
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

                    If SisaHutang = Val(HilangkanTanda(LvJml)) Then
                        SQL = "Update pembelian_proyek set flag_lunas = 'Y', "
                        SQL = SQL & "Tgl_lunas = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                        SQL = SQL & "jam_lunas = '" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                        SQL = SQL & "uservalidasi = '" & UserID & "' where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "no_faktur = '" & LvFak.Trim & "'"
                        ExecuteTrans(SQL)
                    End If
                Next

                SQL = "update val_pemb_proyek set keterangan = '" & TextBox2.Text.Trim & "', "
                'SQL = SQL & "cara_bayar = '" & arrCrByr.Item(ComboBoxCb1.SelectedIndex) & "', "
                SQL = SQL & "grand = " & HilangkanTanda(TextBoxa.Text) & " where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_val = '" & TxtFaktur.Text.Trim & "'"
                ExecuteTrans(SQL)

                For i As Integer = 0 To ListView1.Items.Count - 1
                    Get_Isi_Listview(i)

                    SQL = "insert into detail_val_pemb_proyek(kode_perusahaan, no_val, no_faktur, byr) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
                    SQL = SQL & "'" & LvFak.Trim & "', " & HilangkanTanda(LvJml) & ")"
                    ExecuteTrans(SQL)
                Next

                Dim coa_hutang As String = ""

                SQL = "select top(1) hutang from stock_owner_proyek where kode_perusahaan = '" & KodePerusahaan & "'"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        coa_hutang = dr("hutang")
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Lokasi_Proyek tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                'Dim pagenumber As Integer = 0

                'SQL = "delete from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "kode_voucher = '" & kode_voucher_lama & "'"
                'ExecuteTrans(SQL)

                'SQL = Get_Detail_Jurnal(kode_voucher_lama, Strings.Left(coa_hutang, 1), _
                '               Strings.Mid(coa_hutang, 2, 1), _
                '               Strings.Mid(Ganti(coa_hutang), 3), _
                '               KodePerusahaan, KodeProyek, "Pelunasan hutang " & TxtFaktur.Text.Trim, HilangkanTanda(TextBoxa.Text), "0", pagenumber + 1)
                'ExecuteTrans(SQL)


                'SQL = Get_Detail_Jurnal(kode_voucher_lama, Strings.Left(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex), 1), _
                '               Strings.Mid(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex), 2, 1), _
                '               Strings.Mid(Ganti(ArrAkunCB1.Item(ComboBoxCb1.SelectedIndex)), 3), _
                '               KodePerusahaan, KodeProyek, "Pelunasan hutang " & TxtFaktur.Text.Trim, "0", HilangkanTanda(TextBoxa.Text), pagenumber + 1)
                'ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
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

    Private Sub ListView3_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView3.DoubleClick
        If ListView3.Items.Count = 0 Then Exit Sub
        Dim kode As String = ListView3.FocusedItem.SubItems(0).Text
        Dim nama As String = ListView3.FocusedItem.SubItems(1).Text
        Dim kode_bank As String = ListView3.FocusedItem.SubItems(2).Text
        Dim alamat As String = ListView3.FocusedItem.SubItems(3).Text
        Dim kota As String = ListView3.FocusedItem.SubItems(4).Text
        Dim negara As String = ListView3.FocusedItem.SubItems(5).Text
        Dim telp As String = ListView3.FocusedItem.SubItems(6).Text

        TextBox10.Text = kode
        TextBox8.Text = nama
        ComboBox3.Text = kode_bank

        x_alamat = alamat
        x_Kota = kota
        x_negara = negara
        x_telp = telp

        ListView3.Visible = False
        Button1.Focus()
    End Sub

    Private Sub ListView3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView3.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView3_DoubleClick(ListView2, e)
        End If
    End Sub

    Private Sub BayarSebagianToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BayarSebagianToolStripMenuItem.Click
        For i As Integer = 0 To ListView1.Items.Count - 1
            If ListView1.Items(i).Text.Trim.ToUpper = ListView2.FocusedItem.Text.Trim.ToUpper Then
                MessageBox.Show("Faktur ini sudah dimasukkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

        ListView2.FocusedItem.Checked = True
        TextBox9.Text = ListView2.FocusedItem.Text
        TextBox3.Text = ListView2.FocusedItem.SubItems(1).Text
        TextBox4.Text = ListView2.FocusedItem.SubItems(2).Text
        TextBox7.Text = ListView2.FocusedItem.SubItems(3).Text
        TextBox5.Text = ListView2.FocusedItem.SubItems(4).Text
        Txt_DP.Text = ListView2.FocusedItem.SubItems(10).Text
        TextBox6.Text = HilangkanTanda(ListView2.FocusedItem.SubItems(8).Text)
        TextBox6.Focus()
    End Sub


End Class