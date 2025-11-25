Imports System.Security.Cryptography
Imports System.Text

Public Class Penjualan_New
    Dim total As Double
    Dim lv As New ListViewItem
    'Dim Arr, ArrDetil, ArrSO, ArrKB, ArrJML, ArrJT, ArrCust, ArrGrand As New ArrayList
    'Dim ArrDetil, ArrSO, ArrKB, ArrJML, ArrJT, ArrCust, ArrGrand As New ArrayList
    Dim Hrg_Minimum, Hrg_Modal As Double
    Dim budgeting As String
    Public total_bayar As Double
    Dim sayTerbilang As String
    Public boleh_harga_min As String
    Public user_harga_min As String
    'Dim hrg_jual_apa As String
    Dim text_hrg_jual_apa As String
    Public hasil_input_uang As String
    Public kembalian As Double
    Dim kurs_point As Double = 0
    Public cb_default As String = ""
    Dim arrPersenBrgOrg, arrPersenBrgSdr As New ArrayList

    Dim hasil_path_inv_pdf As String = ""
    Dim hasil_nama_inv_pdf As String = ""

    Dim x_brg_sdr As Integer
    Dim x_brg_org As Integer
    Dim x_total_sebelumnya As Double

    Public RV_PO As Integer = 0
    Public RV_Permintaan_Keluar As Integer = 0
    Public RV_Permintaan_Agency As Integer = 0

    Dim user_ini_boleh_reseller As String = ""

    Dim arrKDpromo As New ArrayList
    Dim arrKDXpromo As New ArrayList
    Dim arrJmlpromo As New ArrayList

    'Dim ArrCB1 As New ArrayList

    Dim arrDariKolom As New ArrayList
    Dim arrDariNilai As New ArrayList
    Dim arrKeNilai As New ArrayList
    Dim arrDiskon As New ArrayList
    Dim arrMinimum As New ArrayList
    '  Dim ArrAkunCB1 As New ArrayList
    Dim arrInisialFaktur, arrMetodePerhitungan As New ArrayList

    Dim arrFlagBudgetingBS, arrAkunBiayaPromo, arrAkunBiayaHut1, arrAkunBiayaHut2, arrAkunBiayaHut3 As New ArrayList
    Dim arrFlagBudgetingMbl, arrAkunBiayaMbl, arrAkunBiayaHutMbl As New ArrayList
    Dim arrFlagDiskonCash, arrFlagKunciInv, arrJmlKunciInv As New ArrayList
    Dim arrPlafonTunai As New ArrayList
    Dim arrAkunBiayaNew, arrAkunBiayaHutNew As New ArrayList

    Dim arrAkunBiayaPromo2, arrAkunBiayaHut4 As New ArrayList

    Dim pkt2, kode_pkt2 As String

    Dim flag_reseller As New ArrayList
    Dim arrKategoriPenggantiReseller As New ArrayList

    'Dim Reseller As String
    Dim cabang_sendiri As String

    'Dim tgl_skrg As String
    Dim tgl_skrg As DateTime

    Dim pake_hrg_yg_mana As String

    Dim arrMetodePotStock As New ArrayList

    Dim LvSO As String
    Dim LvKB As String
    Dim LvNm As String
    Dim LvSerialNumber As String
    Dim LvHrg As String
    Dim LvJml As String
    Dim LvSat As String
    Dim LvDiscp As String
    Dim LvDiscrp As String
    Dim LvSubttl As String
    Dim LvHrgMin As String
    Dim LvUserID As String
    Dim LvPakaiSN As String
    Dim LvHadiah As String
    Dim LvModal As String
    Dim LvNotaKecil As String
    Dim LvKdSales As String
    Dim LvNmSales As String
    Dim LvFlagSendiri As String
    Public LvKodePaketRetail As String
    Dim LvUntukDiskonMember As String
    Dim LvBudgeting As String
    Dim LvPkt2 As String
    Dim LvKodePaketRetail2 As String
    Dim LvLokasiTujuan As String
    Dim LvIdGudang As String

    Public Sub Fokus_Customer()
        TextBox15.Focus()
    End Sub

    Private Sub get_jam()
        Try
            OpenConn()

            SQL = "declare @ab int; select @ab = Selisih_Jam from Init; "
            SQL = SQL & " Select FORMAT(DATEADD(hh, @ab, getdate()), 'yyyy-MM-dd HH:mm:ss') as Tanggal_Sekarang "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    tgl_skrg = dr("Tanggal_Sekarang")
                Loop
            End Using

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvSO = listview1.Items(No_Index).Text
        LvKB = listview1.Items(No_Index).SubItems(1).Text
        LvNm = listview1.Items(No_Index).SubItems(2).Text
        LvSerialNumber = listview1.Items(No_Index).SubItems(3).Text
        LvHrg = listview1.Items(No_Index).SubItems(4).Text
        LvJml = listview1.Items(No_Index).SubItems(5).Text
        LvSat = listview1.Items(No_Index).SubItems(6).Text
        LvDiscp = listview1.Items(No_Index).SubItems(7).Text
        LvDiscrp = listview1.Items(No_Index).SubItems(8).Text
        LvSubttl = listview1.Items(No_Index).SubItems(9).Text
        LvHrgMin = listview1.Items(No_Index).SubItems(10).Text
        LvUserID = listview1.Items(No_Index).SubItems(11).Text
        LvPakaiSN = listview1.Items(No_Index).SubItems(12).Text
        LvHadiah = listview1.Items(No_Index).SubItems(13).Text
        LvModal = listview1.Items(No_Index).SubItems(14).Text
        LvNotaKecil = listview1.Items(No_Index).SubItems(15).Text
        LvKdSales = listview1.Items(No_Index).SubItems(16).Text
        LvNmSales = listview1.Items(No_Index).SubItems(17).Text
        LvFlagSendiri = listview1.Items(No_Index).SubItems(18).Text
        LvKodePaketRetail = listview1.Items(No_Index).SubItems(19).Text
        LvUntukDiskonMember = listview1.Items(No_Index).SubItems(20).Text
        LvBudgeting = listview1.Items(No_Index).SubItems(21).Text
        LvPkt2 = listview1.Items(No_Index).SubItems(22).Text
        LvKodePaketRetail2 = listview1.Items(No_Index).SubItems(23).Text
        LvLokasiTujuan = listview1.Items(No_Index).SubItems(24).Text
        LvIdGudang = listview1.Items(No_Index).SubItems(25).Text
    End Sub


    'Private Sub cetak()
    '    Dim printers As New Printing.PrintDocument()
    '    Dim printerlama As String = printers.DefaultPageSettings.PrinterSettings.PrinterName
    '    Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", My.Settings.Prt_Name))

    '    Try
    '        OpenConn()

    '        SQL = "select b.subtotal, a.bayar, b.nota_kecil, b.kode_barang, a.jenis_transaksi, a.bayar, e.nama as namacustomer, "
    '        SQL = SQL & "c.nama as namabarang, b.harga, b.jumlah, d.nama as nama_perusahaan, "
    '        SQL = SQL & "d.alamat as alamat_perusahaan, d.telepon, a.no_faktur, f.username, "
    '        SQL = SQL & "a.tanggal, a.jam, a.disc1, a.disc2, a.grand, b.persen_diskon, b.nilai_diskon from "
    '        SQL = SQL & "penjualan a, detail_penjualan b, barang c, perusahaan d, customers e, users f where "
    '        SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and "
    '        SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan and d.kode_perusahaan = e.kode_perusahaan and "
    '        SQL = SQL & "e.kode_perusahaan = f.kode_perusahaan and "
    '        SQL = SQL & "a.no_faktur = b.no_faktur and b.kode_stock_owner = c.kode_stock_owner and "
    '        SQL = SQL & "b.kode_barang = c.kode_barang and a.userid = f.userid and a.kode_customer = e.kode_customer and "
    '        SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
    '        SQL = SQL & "a.no_faktur = '" & TxtFaktur.Text.Trim & "'"
    '        Using Ds = BindingTrans(SQL)
    '            With Ds.Tables("MyTable")
    '                Dim Prnt As New Printer
    '                Dim Subtotal As Double = 0
    '                Dim SubtotalTanpaDiskon As Double = 0
    '                Dim Grand As Double = 0
    '                '================

    '                '================

    '                'Prnt.Print()
    '                'Prnt.Print()
    '                'Prnt.Print()
    '                ' Prnt.Print()
    '                'Prnt.Print()
    '                'Prnt.Print()
    '                'Prnt.Print()
    '                'Prnt.Print()

    '                Prnt.Font = New Font("Courier New", 14)

    '                'Prnt.Print("".PadLeft((Pjg_Karakter_Print - Len(.Rows(0).Item("nama_perusahaan"))) / 3, " ") & .Rows(0).Item("nama_perusahaan"))
    '                Prnt.Print(.Rows(0).Item("nama_perusahaan"))
    '                Prnt.Font = New Font("Courier New", 8)
    '                Prnt.Print(.Rows(0).Item("alamat_perusahaan"))
    '                Prnt.Print(.Rows(0).Item("telepon"))
    '                Prnt.Print() 'Courier New
    '                Prnt.Font = New Font("Courier New", 8)
    '                Prnt.Print("=========================================")
    '                Prnt.Print(.Rows(0).Item("no_faktur"))
    '                Prnt.Print(Format(.Rows(0).Item("Tanggal"), "dd MMM yyyy") & " " & .Rows(0).Item("jam"))
    '                Prnt.Print(.Rows(0).Item("username"))
    '                Prnt.Print("=========================================")

    '                Prnt.Font = New Font("Courier New", 9)
    '                Dim tot_sblm_diskon As Double = 0
    '                Dim tot_diskon As Double = 0
    '                Dim tot_qty As Integer = 0

    '                For i As Integer = 0 To .Rows.Count - 1
    '                    SubtotalTanpaDiskon = .Rows(i).Item("harga") * .Rows(i).Item("jumlah")
    '                    Subtotal = SubtotalTanpaDiskon - (SubtotalTanpaDiskon * .Rows(i).Item("persen_diskon") / 100)
    '                    Subtotal = .Rows(i).Item("subtotal")
    '                    Prnt.Print(Strings.Left(.Rows(i).Item("kode_barang"), 15).PadRight(15, " ") & " X " & Format(.Rows(i).Item("jumlah"), "N0").PadLeft(4) & "  " & Strings.Left(.Rows(i).Item("nota_kecil"), 1) & " " & Strings.Mid(.Rows(i).Item("nota_kecil"), 2))
    '                    Prnt.Print(Format(.Rows(i).Item("harga"), "N0").ToString.PadLeft(9, " ") & " " & " " & Format(.Rows(i).Item("persen_diskon"), "N0").ToString.PadLeft(3, " ") & "% " & Format(.Rows(i).Item("nilai_diskon"), "N0").ToString.PadLeft(7, " ") & " " & Format(Subtotal, "N0").ToString.PadLeft(9, " "))
    '                    tot_sblm_diskon = tot_sblm_diskon + (.Rows(i).Item("jumlah") * .Rows(i).Item("harga"))
    '                    Grand = Grand + Subtotal
    '                    tot_qty = tot_qty + .Rows(i).Item("jumlah")
    '                Next

    '                Dim Kembali As Double = 0
    '                tot_diskon = tot_sblm_diskon - Grand
    '                If .Rows(0).Item("jenis_transaksi") = "T" Then
    '                    Kembali = .Rows(0).Item("bayar") - .Rows(0).Item("grand")
    '                Else
    '                    Kembali = 0
    '                End If

    '                Prnt.Print("-----------------------------------------")

    '                Prnt.Print("Subtotal   : Rp.".ToString.PadLeft(24, " ") & Format(tot_sblm_diskon, "N0").ToString.PadLeft(9, " "))
    '                Prnt.Print("Disc(Rp)   : Rp.".ToString.PadLeft(24, " ") & Format(tot_diskon, "N0").ToString.PadLeft(9, " "))
    '                Prnt.Print("Grand Total: Rp.".ToString.PadLeft(24, " ") & Format(.Rows(0).Item("grand"), "N0").ToString.PadLeft(9, " "))

    '                Prnt.Print("Bayar      : Rp.".ToString.PadLeft(24, " ") & Format(.Rows(0).Item("bayar"), "N0").ToString.PadLeft(9, " "))
    '                Prnt.Print("Kembali    : Rp.".ToString.PadLeft(24, " ") & Format(Kembali, "N0").ToString.PadLeft(9, " "))

    '                Prnt.Print()

    '                Prnt.Print("Total Qty  : " & Format(tot_qty, "N0"))

    '                Prnt.Print("-----------------------------------------")
    '                Prnt.Font = New Font("Courier New", 7, FontStyle.Regular)
    '                Prnt.Print("*** Terima Kasih ***".ToString.PadLeft(31, " "))
    '                Prnt.Print("  Barang yang sudah dibeli tidak dapat")
    '                Prnt.Print("       ditukar atau dikembalikan")

    '                Prnt.EndDoc()
    '            End With
    '        End Using

    '        Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", printerlama))

    '        CloseConn()

    '    Catch ex As Exception
    '        Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", printerlama))

    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    Private Sub Cetak()
        Try

            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""
            Dim boleh_cetak As String = "T"

            If CekButtonRole("Cetak_Faktur_Penjualan") = "T" Then
                boleh_cetak = "T"
            Else
                boleh_cetak = "Y"
            End If


            If boleh_cetak = "Y" Then
                SQL = "select a.kode_perusahaan, a.flag_cabang_sendiri, a.jenis from penjualan a, detail_penjualan b where "
                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur and "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & TxtFaktur.Text.Trim & "'"
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        CrDoc = New Faktur_Penjualan_Akhir_Reseller

                        kertas = "Faktur"

                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = PrinterName
                        CrDoc.RecordSelectionFormula = "{detail_penjualan.Kode_Perusahaan} = '" & KodePerusahaan & "' and {detail_penjualan.No_faktur} = '" & TxtFaktur.Text & "'"
                        'CrDoc.SummaryInfo.ReportTitle = "[DUPLIKAT]"

                        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        doctoprint.PrinterSettings.PrinterName = PrinterName
                        Dim rawKind As Integer
                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                            If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                                rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                                CrDoc.PrintOptions.PaperSize = rawKind
                                Exit For
                            End If
                        Next

                        CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                        CrDoc.PrintToPrinter(1, False, 1, 99)
                    End If
                End Using

            End If

            CloseConn()

        Catch ex As Exception

        End Try

    End Sub



    Private Sub get_no_faktur(ByVal pembayaran As String)
        TxtFaktur.Text = fj & TextBox21.Text.Trim & arrInisialFaktur.Item(ComboBox4.SelectedIndex) & "-" & Format(tgl_skrg, "MM/yy") & "-" &
                                 General_Class.Get_Last_Number2("penjualan", "no_faktur", JumlahDigit,
                                 "Kode_perusahaan", KodePerusahaan,
                                 "And", "substring(no_faktur,1," & Len(fj & TextBox21.Text.Trim) + Len(arrInisialFaktur.Item(ComboBox4.SelectedIndex)) + 6 & ")", fj & TextBox21.Text.Trim & arrInisialFaktur.Item(ComboBox4.SelectedIndex) & "-" & Format(tgl_skrg, "MM/yy"))

    End Sub

    Private Sub get_unik()
        Dim rand As New Random
        Label1.Text = Format(tgl_skrg, "MMddHHmmss") & Format(rand.Next(0, 100000), "00000")
    End Sub

    Private Sub cek_diskon()
        If RadioButton1.Checked Then
            lv.SubItems.Add(disc.Text)
            lv.SubItems.Add("0")
            Dim y_hrg As Double = Val(HilangkanTanda(hrg.Text))
            Dim y_disc As Double = Val(HilangkanTanda(Format(Val(disc.Text), "N2")))
            Dim y_jml As Double = Val(HilangkanTanda(jml.Text))

            If arrMetodePerhitungan.Item(ComboBox4.SelectedIndex) = "A" Then
                total = (Val(HilangkanTanda(hrg.Text)) * Val(HilangkanTanda(jml.Text))) - (Val(HilangkanTanda(hrg.Text)) * Val(HilangkanTanda(jml.Text)) * Val(disc.Text) / 100)
            ElseIf arrMetodePerhitungan.Item(ComboBox4.SelectedIndex) = "B" Then
                total = Hitung_Subtotal(y_hrg, y_disc, y_jml)
            Else
                total = -1
            End If
        Else
            lv.SubItems.Add("0")
            lv.SubItems.Add(Format(Val(disc.Text), "N0"))
            total = (Val(HilangkanTanda(hrg.Text)) - Val(disc.Text)) * Val(HilangkanTanda(jml.Text))
        End If
    End Sub


    Private Sub HitungDiskonCash()
        'Try
        '    OpenConn()

        '    Dim ada_dis_cash As Integer = 0
        '    Dim tdk_ada_dis_cash As Integer = 0
        '    Dim diskoncash_sblmnya As Double = 0

        '    For i As Integer = 0 To listview1.Items.Count - 1
        '        Get_Isi_Listview(i)

        '        If arrFlagDiskonCash.Item(ComboBox4.SelectedIndex) = "A" Then 'diskon cash berdasar customer & paket
        '            SQL = "select diskon from customer_diskon where kode_perusahaan = '" & KodePerusahaan & "' and "
        '            SQL = SQL & "kode_so = '" & ComboBox4.Text & "' and "
        '            If LvPkt2 = "Y" Then
        '                SQL = SQL & "kode_paket = '" & LvKodePaketRetail2 & "' and "
        '            Else
        '                SQL = SQL & "kode_paket = '" & LvKodePaketRetail & "' and "
        '            End If

        '            SQL = SQL & "kode_customer = '" & TextBox10.Text.Trim & "'"
        '            Using Dr = OpenTrans(SQL)
        '                If Dr.Read Then
        '                    If Dr("diskon") <> 0 Then
        '                        If ComboBox2.SelectedIndex = 0 Then 'tunai

        '                            If i = 0 Then
        '                                diskoncash_sblmnya = Dr("diskon")
        '                            End If

        '                            If diskoncash_sblmnya <> Dr("diskon") Then
        '                                TextBox27.Text = 0
        '                            Else
        '                                TextBox27.Text = Dr("diskon")
        '                            End If

        '                            diskoncash_sblmnya = Dr("diskon")

        '                            ada_dis_cash = ada_dis_cash + 1
        '                        Else
        '                            TextBox27.Text = "0"
        '                            diskoncash_sblmnya = 0
        '                            tdk_ada_dis_cash = tdk_ada_dis_cash + 1
        '                        End If
        '                    Else
        '                        TextBox27.Text = "0"
        '                        diskoncash_sblmnya = 0
        '                        tdk_ada_dis_cash = tdk_ada_dis_cash + 1
        '                    End If

        '                Else
        '                    TextBox27.Text = "0"
        '                    diskoncash_sblmnya = 0
        '                    tdk_ada_dis_cash = tdk_ada_dis_cash + 1
        '                End If
        '            End Using
        '        ElseIf arrFlagDiskonCash.Item(ComboBox4.SelectedIndex) = "B" Then 'diskon cash berdasar paket
        '            SQL = "select diskon_cash from paket_retail_new2 where kode_perusahaan = '" & KodePerusahaan & "' and "
        '            SQL = SQL & "kode_so = '" & ComboBox4.Text & "' and "
        '            If LvPkt2 = "Y" Then
        '                SQL = SQL & "kode_paket = '" & LvKodePaketRetail2 & "' "
        '            Else
        '                SQL = SQL & "kode_paket = '" & LvKodePaketRetail & "' "
        '            End If
        '            Using Dr = OpenTrans(SQL)
        '                If Dr.Read Then
        '                    If Dr("diskon_cash") <> 0 Then
        '                        If ComboBox2.SelectedIndex = 0 Then 'tunai

        '                            If i = 0 Then
        '                                diskoncash_sblmnya = Dr("diskon_cash")
        '                            End If

        '                            If diskoncash_sblmnya <> Dr("diskon_cash") Then
        '                                TextBox27.Text = 0
        '                            Else
        '                                TextBox27.Text = Dr("diskon_cash")
        '                            End If

        '                            diskoncash_sblmnya = Dr("diskon_cash")

        '                            ada_dis_cash = ada_dis_cash + 1
        '                        Else
        '                            TextBox27.Text = "0"
        '                            diskoncash_sblmnya = 0
        '                            tdk_ada_dis_cash = tdk_ada_dis_cash + 1
        '                        End If
        '                    Else
        '                        TextBox27.Text = "0"
        '                        diskoncash_sblmnya = 0
        '                        tdk_ada_dis_cash = tdk_ada_dis_cash + 1
        '                    End If

        '                Else
        '                    TextBox27.Text = "0"
        '                    diskoncash_sblmnya = 0
        '                    tdk_ada_dis_cash = tdk_ada_dis_cash + 1
        '                End If
        '            End Using
        '        End If
        '    Next

        '    CloseConn()

        '    If ada_dis_cash > 0 And tdk_ada_dis_cash = 0 Then
        '        GroupBox2.Visible = True
        '    Else
        '        GroupBox2.Visible = False
        '    End If
        '    'If ada_dis_cash > 0 And tdk_ada_dis_cash > 0 Then
        '    '    MessageBox.Show("Paket yang ada diskon cash harus pisah invoice!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    'End If
        'Catch ex As Exception
        '    listview1.Items.Clear()
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub

    Private Sub HitungGrandTotal()
        'Try
        '    OpenConn()

        '    Dim ada_dis_cash As Integer = 0
        '    Dim tdk_ada_dis_cash As Integer = 0

        '    For i As Integer = 0 To listview1.Items.Count - 1
        '        Get_Isi_Listview(i)

        '        SQL = "select * from paket_retail_new2 where kode_perusahaan = '" & KodePerusahaan & "' and "
        '        SQL = SQL & "kode_so = '" & ComboBox4.Text & "' and kode_paket = '" & LvKodePaketRetail & "'"
        '        Using Dr = OpenTrans(SQL)
        '            If Dr.Read Then
        '                TextBox24.Text = Dr("diskon_cash")
        '                ada_dis_cash = ada_dis_cash + 1
        '            Else
        '                TextBox24.Text = "0"
        '                tdk_ada_dis_cash = tdk_ada_dis_cash + 1
        '            End If
        '        End Using
        '    Next

        '    CloseConn()

        '    If ada_dis_cash > 0 And tdk_ada_dis_cash > 0 Then
        '        MessageBox.Show("Paket yang ada diskon cash harus pisah invoice!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    End If
        'Catch ex As Exception
        '    listview1.Items.Clear()
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        Dim Grand As Double = 0
        Dim diskon As Double = 0
        Dim TotalSeluruh As Double = 0
        Dim _ppn As Double = 0
        Dim tot_qty As Integer = 0
        Dim tot_sebelum_diskon As Double = 0
        Dim sub_ttl_tidak_diskon As Double = 0

        Dim ff_brg_sdr As Double = 0
        Dim ff_brg_org As Double = 0

        Dim GRAND_UNTUK_ITUNG_DISKON As Double = 0

        For i As Integer = 0 To listview1.Items.Count - 1
            Get_Isi_Listview(i)

            tot_qty = tot_qty + Val(HilangkanTanda(LvJml))
            Grand = Grand + Val(HilangkanTanda(LvSubttl))

            If LvKodePaketRetail = "" Then
                GRAND_UNTUK_ITUNG_DISKON = GRAND_UNTUK_ITUNG_DISKON + Val(HilangkanTanda(LvSubttl))
            End If

            If LvKodePaketRetail <> "" Then
                listview1.Items(i).BackColor = Color.SandyBrown
            End If

            'If LvFlagSendiri = "T" Then
            '    ff_brg_org = ff_brg_org + Val(HilangkanTanda(LvSubttl))
            '    sub_ttl_tidak_diskon = sub_ttl_tidak_diskon + Val(HilangkanTanda(LvSubttl))
            'Else
            '    ff_brg_sdr = ff_brg_sdr + Val(HilangkanTanda(LvSubttl))
            'End If

            If LvFlagSendiri = "Y" Then
                If LvUntukDiskonMember = "Y" Then
                    ff_brg_sdr = ff_brg_sdr + Val(HilangkanTanda(LvSubttl))
                End If
            Else
                If LvUntukDiskonMember = "Y" Then
                    ff_brg_org = ff_brg_org + Val(HilangkanTanda(LvSubttl))
                    sub_ttl_tidak_diskon = sub_ttl_tidak_diskon + Val(HilangkanTanda(LvSubttl))
                End If
            End If

            'If LvUntukDiskonMember = "Y" Then
            '    sub_ttl_tidak_diskon = sub_ttl_tidak_diskon + Val(HilangkanTanda(LvSubttl))
            'End If

            tot_sebelum_diskon = tot_sebelum_diskon + (Val(HilangkanTanda(LvJml)) * Val(HilangkanTanda(LvHrg)))
        Next

        ff_brg_sdr = ff_brg_sdr + x_brg_sdr
        ff_brg_org = ff_brg_org + x_brg_org

        Dim ff_pengali_persen As Double = 100 / (ff_brg_org + ff_brg_sdr)
        Dim memenuhi As String = ""
        If ff_pengali_persen * ff_brg_org <= Val(arrPersenBrgOrg.Item(ComboBox4.SelectedIndex)) And ff_pengali_persen * ff_brg_sdr >= Val(arrPersenBrgSdr.Item(ComboBox4.SelectedIndex)) Then
            memenuhi = "Y"
        Else
            memenuhi = "T"
        End If

        'Dim x_total_seluruh As Double = x_total_sebelumnya + Grand
        Dim x_total_seluruh As Double = x_total_sebelumnya + Grand 'GRAND_UNTUK_ITUNG_DISKON
        Dim xx_disc_promo As Double = 0
        If memenuhi = "Y" Then
            If x_total_seluruh >= 1500001 And x_total_seluruh <= 2500000 Then
                xx_disc_promo = 1
            ElseIf x_total_seluruh >= 2500001 And x_total_seluruh <= 5000000 Then
                xx_disc_promo = 1.5
            ElseIf x_total_seluruh >= 5000001 And x_total_seluruh <= 10000000 Then
                xx_disc_promo = 2
            ElseIf x_total_seluruh >= 10000001 And x_total_seluruh <= 15000000 Then
                xx_disc_promo = 2.5
            ElseIf x_total_seluruh >= 15000001 And x_total_seluruh <= 20000000 Then
                xx_disc_promo = 3
            ElseIf x_total_seluruh >= 20000001 And x_total_seluruh <= 25000000 Then
                xx_disc_promo = 3.5
            ElseIf x_total_seluruh >= 25000001 And x_total_seluruh <= 30000000 Then
                xx_disc_promo = 4
            ElseIf x_total_seluruh >= 30000001 Then
                xx_disc_promo = 4.5
            Else
                xx_disc_promo = 0
            End If
        End If

        TextBox13.Text = Format(tot_sebelum_diskon, "N0")
        TextBox14.Text = Format(tot_sebelum_diskon - Grand, "N0")
        TextBox16.Text = Format(sub_ttl_tidak_diskon, "N0")

        Label36.Text = Format(tot_qty, "N0")



        'Dim persen_diskon_member As Double = 0
        'For i As Integer = 0 To arrDariKolom.Count - 1
        '    If sub_ttl_tidak_diskon >= arrDariNilai.Item(i) And sub_ttl_tidak_diskon <= arrKeNilai.Item(i) Then
        '        'kalo memenuhi maka
        '        persen_diskon_member = arrDiskon.Item(i)
        '        Exit For
        '    End If
        'Next

        'If Reseller = "Y" Then
        '    If flag_reseller.Item(ComboBox4.SelectedIndex) = "Y" Then 'cabang ini boleh reseller?
        '        If user_ini_boleh_reseller = "Y" Then
        '            discz.Text = "0"
        '        Else
        '            discz.Text = persen_diskon_member
        '        End If
        '    Else
        '        discz.Text = persen_diskon_member
        '    End If
        'Else
        '    discz.Text = persen_diskon_member
        'End If

        If cabang_sendiri = "Y" Then
            discz.Text = "0"
        ElseIf cabang_sendiri = "T" Then
            discz.Text = xx_disc_promo
        Else
            discz.Text = "0"
        End If

        If Val(HilangkanTanda(TextBox16.Text)) = 0 Then
            discz.Text = "0"
        End If



        diskon = sub_ttl_tidak_diskon * discz.Text / 100
        diskon = HilangkanTanda(Format(diskon, "N0"))
        Discx.Text = Format(Val(TextBox6.Text) + Val(HilangkanTanda(TextBox5.Text)), "N0")

        Dim diskon_cash As Double = 0
        diskon_cash = Grand * TextBox24.Text / 100
        diskon_cash = HilangkanTanda(Format(diskon_cash, "N0"))

        Dim diskon_cash_sementara As Double = 0
        diskon_cash_sementara = Grand * Val(TextBox27.Text) / 100
        diskon_cash_sementara = HilangkanTanda(Format(diskon_cash_sementara, "N0"))


        TxtSub.Text = Format(Grand, "N0")
        TextBox1.Text = Format(diskon, "N0")
        TextBox25.Text = Format(diskon_cash, "N0")

        TextBox28.Text = Format(diskon_cash_sementara, "N0")
        TextBox29.Text = Format(Grand - diskon - diskon_cash_sementara, "N0")
        TextBox30.Text = TextBox18.Text
        TextBox31.Text = Format(Val(HilangkanTanda(Grand - diskon - diskon_cash_sementara)) * Val(TextBox30.Text) / 100, "N0")
        Dim TotalSeluruhSementara As Double = Grand - diskon - diskon_cash_sementara - Val(HilangkanTanda(Discx.Text)) + Val(HilangkanTanda(TextBox31.Text))
        TextBox32.Text = Format(TotalSeluruhSementara, "N0")

        TextBox17.Text = Format(Grand - diskon - diskon_cash, "N0")
        'TextBox18.Text = PPN
        TextBox19.Text = Format(Val(HilangkanTanda(Grand - diskon - diskon_cash)) * Val(TextBox18.Text) / 100, "N0")

        TotalSeluruh = Grand - diskon - diskon_cash - Val(HilangkanTanda(Discx.Text)) + Val(HilangkanTanda(TextBox19.Text))

        Label12.Text = Format(TotalSeluruh, "N0")
        TxtTotal.Text = Format(TotalSeluruh, "N0")
        Label21.Text = Format(Math.Floor(Val(HilangkanTanda(TxtTotal.Text)) / kurs_point) * Val(ComboBox1.Text), "N0")

        'diskon = Grand * HilangkanTanda(TampilanDesimal(Val(Trim(discz.Text)))) / 100
        'PPN = (Grand - diskon - Val(Trim(Discx.Text))) * Val(TxtPPN.Text) / 100
        'TotalSeluruh = Grand - diskon - Val(Trim(Discx.Text)) + PPN

        'TxtSub.Text = Format(Grand, "N0")
        'TextBox1.Text = Format(diskon, "N0")
        'Label12.Text = Format(TotalSeluruh, "N0")
        'TxtTotal.Text = Format(TotalSeluruh, "N0")
        'Label21.Text = Format(Math.Floor(Val(HilangkanTanda(TxtTotal.Text)) / kurs_point) * Val(ComboBox1.Text), "N0")
    End Sub

    Public Sub cari_sementara(ByVal no_unik As String)
        Try
            OpenConn()

            Label1.Text = no_unik

            listview1.Items.Clear()
            Dim lv As New ListViewItem

            SQL = "select a.lokasi, b.pegawai, b.driver, a.kode_cb, b.nota_kecil, b.kode_marketing, f.nama as nama_marketing, b.modal, "
            SQL = SQL & "a.tanggal, a.kode_customer, c.nama as nama_customer, a.kode_sales, d.nama as nama_sales, "
            SQL = SQL & "a.jenis_transaksi, a.jenis_transaksi, a.tgl_jatuh_tempo, a.discp, a.discrp, "
            SQL = SQL & "b.kode_stock_owner, b.kode_barang, e.nama as namabarang, b.keterangan, "
            SQL = SQL & "b.harga, b.jumlah, e.satuan, b.persen_diskon, b.nilai_diskon, b.harga_min, b.usermin, b.pakai_sn, b.barang_hadiah from "
            SQL = SQL & "sementara a, detail_sementara b, customers c, sales d, barang e, marketing f where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And b.kode_perusahaan = c.kode_perusahaan and "
            SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan And d.kode_perusahaan = e.kode_perusahaan and "
            SQL = SQL & "e.kode_perusahaan = f.kode_perusahaan And b.kode_marketing = f.kode_marketing and "
            SQL = SQL & "a.kode_unik = b.kode_unik And a.kode_customer = c.kode_customer and "
            SQL = SQL & "a.kode_sales = d.kode_sales and b.kode_stock_owner = e.kode_stock_owner and "
            SQL = SQL & "b.kode_barang = e.kode_barang and a.kode_unik = '" & no_unik & "' order by b.no_urut"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    'DateTimePicker1.Value = tgl_skrg
                    TextBox10.Text = Dr("kode_customer")
                    TextBox11.Text = Dr("nama_customer")
                    'ListView10.Visible = False
                    'TextBox2.Text = Dr("kode_sales")
                    'TextBox3.Text = Dr("nama_sales")
                    'ListView5.Visible = False
                    ComboBox5.SelectedIndex = -1



                    If Dr("jenis_transaksi") = "T" Then
                        ComboBox2.SelectedIndex = 0
                        DateTimePicker2.Value = tgl_skrg
                        'ComboBoxCb1.Text = Dr("kode_cb")
                    Else
                        ComboBox2.SelectedIndex = 1
                        DateTimePicker2.Value = Dr("tgl_jatuh_tempo")
                        'ComboBoxCb1.SelectedIndex = -1
                    End If

                    If Dr("discp") = 0 Then
                        CheckBox1.Checked = False
                    Else
                        CheckBox1.Checked = True
                    End If
                    If Dr("discrp") = 0 Then
                        CheckBox2.Checked = False
                    Else
                        CheckBox2.Checked = True
                    End If
                    discz.Text = Dr("discp")
                    Discx.Text = Dr("discrp")

                    Dim keterangan As String = ""
                    If General_Class.CekNULL(Dr("keterangan")) = "" Then
                        keterangan = ""
                    Else
                        keterangan = Dr("keterangan")
                    End If

                    Dim subtotal As Double = 0

                    lv = listview1.Items.Add(Dr("kode_stock_owner"))
                    lv.SubItems.Add(Dr("kode_barang"))
                    lv.SubItems.Add(Dr("namabarang") & keterangan)
                    lv.SubItems.Add("-")
                    lv.SubItems.Add(Format(Dr("harga"), "N0"))
                    lv.SubItems.Add(TampilanDesimal(Dr("jumlah")))
                    lv.SubItems.Add(Dr("satuan"))
                    lv.SubItems.Add(Dr("persen_diskon"))
                    lv.SubItems.Add(Format(Dr("nilai_diskon"), "N0"))

                    If Dr("persen_diskon") = 0 Then
                        subtotal = HasilDiskon(Dr("harga"), Dr("jumlah"), 0, Dr("nilai_diskon"), "RP")
                    Else
                        subtotal = HasilDiskon(Dr("harga"), Dr("jumlah"), Dr("persen_diskon"), 0, "PERSEN")
                    End If
                    lv.SubItems.Add(Format(subtotal, "N0"))
                    lv.SubItems.Add(Format(Dr("harga_min"), "N0"))
                    If General_Class.CekNULL(Dr("usermin")) = "" Then
                        lv.SubItems.Add("NULL")
                    Else
                        lv.SubItems.Add(Dr("usermin"))
                    End If
                    lv.SubItems.Add(Dr("pakai_sn"))
                    lv.SubItems.Add(Dr("barang_hadiah"))
                    lv.SubItems.Add(Dr("modal"))
                    lv.SubItems.Add(Dr("nota_kecil"))
                    lv.SubItems.Add(Dr("kode_marketing"))
                    lv.SubItems.Add(Dr("nama_marketing"))
                    If Dr("pegawai") = "" Then
                        lv.SubItems.Add("")
                    Else
                        lv.SubItems.Add(Dr("pegawai"))
                    End If
                    If Dr("driver") = "" Then
                        lv.SubItems.Add("")
                    Else
                        lv.SubItems.Add(Dr("driver"))
                    End If

                    Do While Dr.Read
                        lv = listview1.Items.Add(Dr("kode_stock_owner"))
                        lv.SubItems.Add(Dr("kode_barang"))
                        lv.SubItems.Add(Dr("namabarang") & keterangan)
                        lv.SubItems.Add("-")
                        lv.SubItems.Add(Format(Dr("harga"), "N0"))
                        lv.SubItems.Add(TampilanDesimal(Dr("jumlah")))
                        lv.SubItems.Add(Dr("satuan"))
                        lv.SubItems.Add(Dr("persen_diskon"))
                        lv.SubItems.Add(Format(Dr("nilai_diskon"), "N0"))

                        If Dr("persen_diskon") = 0 Then
                            subtotal = HasilDiskon(Dr("harga"), Dr("jumlah"), 0, Dr("nilai_diskon"), "RP")
                        Else
                            subtotal = HasilDiskon(Dr("harga"), Dr("jumlah"), Dr("persen_diskon"), 0, "PERSEN")
                        End If
                        lv.SubItems.Add(Format(subtotal, "N0"))
                        lv.SubItems.Add(Format(Dr("harga_min"), "N0"))
                        If General_Class.CekNULL(Dr("usermin")) = "" Then
                            lv.SubItems.Add("NULL")
                        Else
                            lv.SubItems.Add(Dr("usermin"))
                        End If
                        lv.SubItems.Add(Dr("pakai_sn"))
                        lv.SubItems.Add(Dr("barang_hadiah"))
                        lv.SubItems.Add(Dr("modal"))
                        lv.SubItems.Add(Dr("nota_kecil"))
                        lv.SubItems.Add(Dr("kode_marketing"))
                        lv.SubItems.Add(Dr("nama_marketing"))
                        If Dr("pegawai") = "" Then
                            lv.SubItems.Add("")
                        Else
                            lv.SubItems.Add(Dr("pegawai"))
                        End If
                        If Dr("driver") = "" Then
                            lv.SubItems.Add("")
                        Else
                            lv.SubItems.Add(Dr("driver"))
                        End If
                    Loop
                    HitungGrandTotal()
                    Disable_Customer()
                End If
            End Using

            CloseConn()

            OpenConn()

            If ComboBox2.SelectedIndex = 0 Then
                get_no_faktur("T")
            Else
                get_no_faktur("K")
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub bersihsebagian()
        kd.Text = ""

        TextBox4.Text = ""
        TextBox4.Enabled = False
        ket.Text = "Keterangan. . ."
        hrg.Text = ""
        jml.Text = "1"
        sat.Text = ""
        disc.Text = ""
        Flag_Sendiri.Text = ""
        RadioButton1.Checked = True

        'If TampilSO = "Y" Then
        '    ComboBox4.Focus()
        'Else
        '    kd.Focus()
        'End If
        Label14.Text = "0"
    End Sub

    Public Sub bersihseluruh()
        Button8.Visible = False
        pake_hrg_yg_mana = ""
        kd.Enabled = False
        Button7.Enabled = False
        Label51.Text = ""
        Timer2.Enabled = False
        TextBox21.Text = "C"

        If TextBox21.Text.Trim.Length = 0 Then
            MessageBox.Show("Harap ulangi transaksi! Close transaksi penjualan! Karena transaksi tidak bisa diproses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        If TextBox21.Text.Trim = "C" Then
            Me.Text = ".:: Transaksi Penjualan ::."
            Label31.Text = ""
            TextBox18.Text = "11"
        ElseIf TextBox21.Text.Trim = "R" Then
            Me.Text = ".:: Transaksi Penjualan ::."
            Label31.Text = ""
            TextBox18.Text = "0"
        End If

        'Try
        '    OpenConn()

        '    Using Dr = OpenTrans("select dateadd(hh, -1, getdate()) as Jam")
        '        If Dr.Read Then
        '            tgl_skrg = Format(Dr("jam"), "yyyy-MM-dd HH:mm:ss")
        '            fmenu.ToolStripStatusLabel3.Text = Format(Dr("jam"), "yyyy-MM-dd HH:mm:ss")

        '            'tgl_skrg = Format(CDate("2021-09-10 19:20:00"), "yyyy-MM-dd HH:mm:ss")
        '            'fmenu.ToolStripStatusLabel3.Text = Format(CDate("2021-09-10 19:20:00"), "yyyy-MM-dd HH:mm:ss")
        '            Timer2.Enabled = True
        '        End If
        '    End Using

        '    CloseConn()

        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show("Error pada jam!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    tgl_skrg = ""
        '    Timer2.Enabled = False

        'End Try

        get_jam()

        arrDariKolom.Clear() : arrDariNilai.Clear() : arrKeNilai.Clear() : arrDiskon.Clear() : arrMinimum.Clear()
        arrFlagBudgetingBS.Clear() : arrAkunBiayaPromo.Clear()
        arrAkunBiayaHut1.Clear() : arrAkunBiayaHut2.Clear() : arrAkunBiayaHut3.Clear()

        arrFlagBudgetingMbl.Clear() : arrAkunBiayaMbl.Clear()
        arrAkunBiayaHutMbl.Clear()
        arrFlagDiskonCash.Clear()
        arrFlagKunciInv.Clear() : arrJmlKunciInv.Clear()

        arrPlafonTunai.Clear()
        arrAkunBiayaNew.Clear() : arrAkunBiayaHutNew.Clear()
        arrAkunBiayaPromo2.Clear()
        arrAkunBiayaHut4.Clear()

        arrKDpromo.Clear() : arrKDXpromo.Clear() : arrJmlpromo.Clear()
        flag_reseller.Clear()
        arrKategoriPenggantiReseller.Clear()



        arrMetodePerhitungan.Clear()

        Timer1.Enabled = False

        RV_PO = 0
        RV_Permintaan_Keluar = 0
        RV_Permintaan_Agency = 0

        Panel1.Visible = False
        Panel2.Visible = False

        Label27.Visible = False

        cabang_sendiri = ""
        x_brg_sdr = 0
        x_brg_org = 0
        x_total_sebelumnya = 0

        CheckBox3.Checked = False
        TextBox22.Text = ""
        TextBox22.Enabled = True
        Label47.Text = ""

        TextBox24.Text = "0"
        TextBox25.Text = "0"

        GroupBox2.Visible = False
        TextBox27.Text = "0"
        TextBox28.Text = "0"
        TextBox29.Text = "0"
        TextBox30.Text = TextBox18.Text
        TextBox31.Text = "0"
        TextBox32.Text = "0"

        TextBox23.Text = ""

        'DateTimePicker1.Value = tgl_skrg
        DateTimePicker2.Value = tgl_skrg
        'DateTimePicker1.Enabled = False
        'DateTimePicker1.Enabled = False

        TextBox15.Enabled = True
        TextBox15.Text = ""
        TextBoxGudang.Text = ""

        bersihsebagian()
        Label12.Text = "0"
        listview1.Items.Clear()
        get_unik()
        nm.Text = ""

        user_ini_boleh_reseller = "T"

        cb_default = ""

        Try
            OpenConn()

            ComboBox4.Items.Clear() : flag_reseller.Clear()
            arrKategoriPenggantiReseller.Clear() : arrInisialFaktur.Clear()
            arrPersenBrgOrg.Clear() : arrPersenBrgSdr.Clear()
            arrFlagBudgetingBS.Clear() : arrAkunBiayaPromo.Clear()
            arrAkunBiayaHut1.Clear() : arrAkunBiayaHut2.Clear() : arrAkunBiayaHut3.Clear()
            arrFlagKunciInv.Clear() : arrJmlKunciInv.Clear()

            arrPlafonTunai.Clear()

            arrAkunBiayaPromo2.Clear()
            arrAkunBiayaHut4.Clear()

            arrFlagBudgetingMbl.Clear() : arrAkunBiayaMbl.Clear()
            arrAkunBiayaHutMbl.Clear()
            arrFlagDiskonCash.Clear()

            arrMetodePotStock.Clear()

            arrMetodePerhitungan.Clear()

            arrAkunBiayaNew.Clear() : arrAkunBiayaHutNew.Clear()


            SQL = "Select hutang_Promo_New, biaya_promo_new, metode_perhitungan, metode_pot_stock, plafon_tunai, flag_kunci_inv, jml_kunci_inv, flag_diskon_cash, flag_budgeting_mbl, biaya_promo_mbl, hutang_promo_mbl, flag_budgeting_bs, "
            SQL = SQL & "biaya_promo, hutang_promo_1, hutang_promo_2, hutang_promo_3, "
            SQL = SQL & "biaya_promo2, hutang_promo_4, "
            SQL = SQL & "persen_brg_org, persen_brg_sdr, kode_stock_owner, flag_default, flag_reseller, "
            SQL = SQL & "kategori_pengganti_reseller, inisial_faktur From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox4.Items.Add(dr("kode_stock_owner"))
                    flag_reseller.Add(dr("flag_reseller"))
                    arrKategoriPenggantiReseller.Add(General_Class.CekNULL(dr("kategori_pengganti_reseller")))
                    arrInisialFaktur.Add(dr("inisial_faktur"))
                    arrPersenBrgOrg.Add(dr("persen_brg_org"))
                    arrPersenBrgSdr.Add(dr("persen_brg_sdr"))

                    arrFlagBudgetingBS.Add(dr("flag_budgeting_bs"))
                    arrAkunBiayaPromo.Add(dr("biaya_promo"))
                    arrAkunBiayaHut1.Add(dr("hutang_promo_1"))
                    arrAkunBiayaHut2.Add(dr("hutang_promo_2"))
                    arrAkunBiayaHut3.Add(dr("hutang_promo_3"))

                    arrAkunBiayaPromo2.Add(dr("biaya_promo2"))
                    arrAkunBiayaHut4.Add(dr("hutang_promo_4"))

                    arrFlagBudgetingMbl.Add(dr("flag_budgeting_mbl"))
                    arrAkunBiayaMbl.Add(dr("biaya_promo_mbl"))
                    arrAkunBiayaHutMbl.Add(dr("hutang_promo_mbl"))
                    arrFlagDiskonCash.Add(dr("flag_diskon_cash"))
                    arrFlagKunciInv.Add(dr("flag_kunci_inv"))
                    arrJmlKunciInv.Add(dr("jml_kunci_inv"))
                    'yyyyyyyyyyy
                    arrPlafonTunai.Add(dr("plafon_tunai"))
                    arrMetodePotStock.Add(dr("metode_pot_stock"))
                    arrMetodePerhitungan.Add(dr("metode_perhitungan"))

                    arrAkunBiayaNew.Add(dr("biaya_promo_new"))
                    arrAkunBiayaHutNew.Add(dr("hutang_Promo_New"))

                    'If dr("flag_default") = "Y" Then
                    '    ComboBox4.Text = dr("kode_stock_owner")
                    'End If
                Loop
            End Using

            ComboBox4.Text = Lokasi

            get_no_faktur("T")

            'ComboBoxCb1.Items.Clear()
            'ArrCB1.Clear()
            'ArrAkunCB1.Clear()

            'ComboBoxCb1.Items.Add("-- Cara Bayar --") : ArrCB1.Add("") : ArrAkunCB1.Add("")

            'ComboBoxCb1.SelectedIndex = 0

            'SQL = "select kode_cb, keterangan, flag_default, kode_account_cb from cara_bayar where "
            'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and lokasi = '" & ComboBox4.Text & "' order by keterangan"
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        ComboBoxCb1.Items.Add(Dr("kode_cb")) : ArrCB1.Add(Dr("kode_cb")) : ArrAkunCB1.Add(Dr("kode_account_cb"))
            '    Loop
            'End Using

            SQL = "select nilai_kurs from kurs where kode_perusahaan = '" & KodePerusahaan & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    kurs_point = Dr("nilai_kurs")
                Else
                    kurs_point = 0
                End If
            End Using
            Label20.Text = "1 Point : " & Format(kurs_point, "N0")


            SQL = "select b.lokasi, b.init_custm, cast(b.rv as bigint) as rvx, b.no_do, sum(c.jumlah) as tot_jml, "
            SQL = SQL & "b.kode_perusahaan, b.no_faktur, b.tanggal, b.jam, "
            SQL = SQL & "b.status, c.kode_stock_owner, b.kode_customer, d.nama "
            SQL = SQL & "from barang a, Penjualan b, detail_penjualan c, Customers d "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and c.kode_perusahaan = d.kode_perusahaan and "
            SQL = SQL & "a.kode_barang = c.kode_barang and a.kode_stock_owner = c.kode_stock_owner and b.no_faktur = c.no_faktur and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and b.kode_customer = d.kode_customer and "
            SQL = SQL & "b.status is null and b.no_do is null and b.lokasi = '" & ComboBox4.Text & "' "
            SQL = SQL & "group by b.lokasi, b.init_custm, b.rv, b.no_do, "
            SQL = SQL & "b.kode_perusahaan, b.no_faktur, b.tanggal, b.jam, "
            SQL = SQL & "b.status, c.kode_stock_owner, b.kode_customer, d.nama "
            SQL = SQL & "Order by b.tanggal + b.jam Desc "

            ListView_DO.Items.Clear() : ListView_DO_Detail.Items.Clear()
            Dim Lvw As ListViewItem
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Lvw = ListView_DO.Items.Add(.Rows(i).Item("no_faktur"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal"), "dd-MMM-yyyy"))
                        Lvw.SubItems.Add(.Rows(i).Item("jam"))
                        If General_Class.CekNULL(.Rows(i).Item("init_custm")) = "" Then
                            Lvw.SubItems.Add(.Rows(i).Item("kode_customer"))
                            Lvw.SubItems.Add(.Rows(i).Item("nama"))
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("init_custm"))
                            Lvw.SubItems.Add(.Rows(i).Item("init_custm"))
                        End If
                        ' Lvw.SubItems.Add(Format(.Rows(i).Item("tot_jml"), "N0"))
                        Lvw.SubItems.Add(i + 1)
                        Lvw.SubItems.Add(.Rows(i).Item("rvx"))
                        Lvw.SubItems.Add(.Rows(i).Item("lokasi"))
                    Next
                End With
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            If flag_reseller.Item(ComboBox4.SelectedIndex) = "Y" Then 'cabang ini boleh reseller?
                If CekButtonRole("Penjualan_reseller") = "T" Then
                    user_ini_boleh_reseller = "T"
                    Label42.Visible = False
                Else
                    user_ini_boleh_reseller = "Y"
                    Label42.Visible = True
                End If
            Else
                user_ini_boleh_reseller = "T"
                Label42.Visible = False
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try

            OpenConn()

            If CekButtonRole("Cari_KB") = "T" Then
                Button8.Visible = False
            Else
                Button8.Visible = True
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        TextBox10.Text = ""
        TextBox11.Text = ""
        ListView10.Visible = False

        TextBox2.Text = ""
        TextBox3.Text = ""

        Disable_Customer()

        TextBox7.Text = "-"
        TextBox8.Text = "-"
        TextBox9.Text = "0"
        TextBox12.Text = "0"
        ListView20.Visible = False

        ComboBox5.Items.Clear()
        For i As Integer = 1 To Jatuh_Tempo_Penjualan
            ComboBox5.Items.Add(i)
        Next
        ComboBox5.SelectedIndex = 0
        ComboBox2.SelectedIndex = -1
        ComboBox2.Enabled = True

        DateTimePicker2.Visible = False
        ComboBox5.Visible = False

        'ComboBoxCb1.Text = cb_default
        'ComboBoxCb1.Enabled = True

        TxtSub.Text = "0"
        TxtTotal.Text = "0"
        TextBox5.Text = "0"

        CheckBox1.Checked = False
        discz.Enabled = False
        discz.Text = "0" : TextBox1.Text = "0"

        CheckBox2.Checked = False
        'Discx.Enabled = False
        'Discx.Text = "0"
        TextBox6.Enabled = False
        TextBox6.Text = "0"
        pkt2 = ""
        kode_pkt2 = ""

        If EditHJ = "Y" Then
            hrg.Enabled = True
        Else
            hrg.Enabled = False
        End If

        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("1")
        ComboBox1.Items.Add("2")
        ComboBox1.Items.Add("3")
        ComboBox1.SelectedIndex = 0


        Dim boleh_tampil As String = "T"
        Panel1.Visible = False
        Panel2.Visible = False
        TextBox2.Enabled = True
        TextBox3.Enabled = True

        Try
            OpenConn()

            If CekButtonRole("Penjualan_Tampil_Harga") = "T" Then
                boleh_tampil = "T"
            Else
                boleh_tampil = "Y"
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Me.Close()
            '            Exit Sub
        End Try

        If boleh_tampil = "T" Then
            Panel1.Visible = False
            Panel2.Visible = False
        Else
            Panel1.Visible = True
            Panel2.Visible = True
        End If

        listview1.Columns.Clear()
        listview1.Columns.Add("Gudang", 0, HorizontalAlignment.Left)
        listview1.Columns.Add("Kode Barang", 160, HorizontalAlignment.Left) '1

        If boleh_tampil = "T" Then
            listview1.Columns.Add("Nama Barang", 600, HorizontalAlignment.Left) '2
            listview1.Columns.Add("Serial Number", 0, HorizontalAlignment.Left) '3
            listview1.Columns.Add("Harga", 0, HorizontalAlignment.Right) '4
        Else
            listview1.Columns.Add("Nama Barang", 345, HorizontalAlignment.Left) '2
            listview1.Columns.Add("Serial Number", 0, HorizontalAlignment.Left) '3
            listview1.Columns.Add("Harga", 90, HorizontalAlignment.Right) '4
        End If
        listview1.Columns.Add("Jumlah", 60, HorizontalAlignment.Right) '5
        listview1.Columns.Add("Satuan", 55, HorizontalAlignment.Left) '6

        If boleh_tampil = "T" Then
            listview1.Columns.Add("Disc(%)", 0, HorizontalAlignment.Center) '7
            listview1.Columns.Add("Disc(Rp.)", 0, HorizontalAlignment.Right) '8
            listview1.Columns.Add("Total", 0, HorizontalAlignment.Right) '9
            listview1.Columns.Add("Harga Min", 0, HorizontalAlignment.Right) '10
        Else
            listview1.Columns.Add("Disc(%)", 63, HorizontalAlignment.Center) '7
            listview1.Columns.Add("Disc(Rp.)", 0, HorizontalAlignment.Right) '8
            listview1.Columns.Add("Total", 100, HorizontalAlignment.Right) '9
            listview1.Columns.Add("Harga Min", 0, HorizontalAlignment.Right) '10
        End If

        listview1.Columns.Add("UserID", 0, HorizontalAlignment.Left) '11
        listview1.Columns.Add("Pakai SN", 0, HorizontalAlignment.Left) '12
        listview1.Columns.Add("Brg Hadiah", 0, HorizontalAlignment.Left) '13
        listview1.Columns.Add("Modal", 0, HorizontalAlignment.Left) '14
        listview1.Columns.Add("No. Nota", 0, HorizontalAlignment.Center) '15
        listview1.Columns.Add("Kode Sales", 0, HorizontalAlignment.Left) '16
        listview1.Columns.Add("Sales", 0, HorizontalAlignment.Left) '17
        listview1.Columns.Add("Flag Sendiri", 0, HorizontalAlignment.Left) '18
        listview1.Columns.Add("Kode Paket", 0, HorizontalAlignment.Left) '19
        listview1.Columns.Add("Flag Diskon Member", 0, HorizontalAlignment.Left) '20
        listview1.Columns.Add("Budgeting", 0, HorizontalAlignment.Left) '21
        listview1.Columns.Add("Pkt2", 0, HorizontalAlignment.Left) '22
        listview1.Columns.Add("Kode Paket 2", 0, HorizontalAlignment.Left) '23
        listview1.Columns.Add("Lokasi tujuan", 200, HorizontalAlignment.Left).DisplayIndex = 0
        listview1.Columns.Add("Id Gudang", 0, HorizontalAlignment.Left)
        listview1.View = View.Details


        'ArrDetil.Clear() : ArrSO.Clear() : ArrKB.Clear() : ArrJML.Clear() : ArrJT.Clear() : ArrCust.Clear() : ArrGrand.Clear()

        'Dim sp = New SerialPort(My.Settings.Port_Cust_Display, 9600, Parity.None, 8, StopBits.One)
        'If Not (sp Is Nothing) Then
        '    sp.Open()
        '    '// to clear the display
        '    sp.Write(Convert.ToString(Chr(12)))

        '    '// first line goes here
        '    sp.WriteLine(Tulisan_Awal)
        '    sp.WriteLine(Chr(13) & NamaPerusahaan)

        '    sp.Close()
        '    sp.Dispose()
        '    sp = Nothing
        'End If

        Button2.Text = "&Simpan"
    End Sub

    Public Sub Disable_Customer()
        If listview1.Items.Count = 0 Then
            TextBox15.Enabled = True
            'CheckBox3.Enabled = True
            'TextBox22.Enabled = True
        Else
            TextBox15.Enabled = False
            'CheckBox3.Enabled = False
            'TextBox22.Enabled = False
        End If

        If listview1.Items.Count = 0 Then
            TextBox10.Text = ""
            TextBox11.Text = ""
        End If
    End Sub

    Private Sub Penjualan_New_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        TextBox15.Focus()
    End Sub

    Private Sub Penjualan_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        'Pilih_Customer.Close()

        'Split_Display_Penjualan.Close()
    End Sub

    Private Sub Penjualan_New_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        TextBox15.Focus()
    End Sub

    Private Sub Penjualan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        bersihseluruh()

        ListView_DO.Columns.Add("No Faktur", 100, HorizontalAlignment.Center)
        ListView_DO.Columns.Add("Tanggal", 85, HorizontalAlignment.Center)
        ListView_DO.Columns.Add("Jam", 60, HorizontalAlignment.Center)
        ListView_DO.Columns.Add("Kode Customer", 0, HorizontalAlignment.Left)
        ListView_DO.Columns.Add("Customer", 200, HorizontalAlignment.Left)
        'ListView1.Columns.Add("Total Jml", 80, HorizontalAlignment.Right)
        ListView_DO.Columns.Add("No. ", 40, HorizontalAlignment.Right).DisplayIndex = 0
        ListView_DO.Columns.Add("#", 0, HorizontalAlignment.Right)
        ListView_DO.Columns.Add("Lokasi", 100, HorizontalAlignment.Right)
        ListView_DO.View = View.Details

        ListView_DO_Detail.Columns.Add("Kode Stock Owner", 0, HorizontalAlignment.Center)
        ListView_DO_Detail.Columns.Add("Kode Barang", 0, HorizontalAlignment.Left)
        ListView_DO_Detail.Columns.Add("Nama Barang", 0, HorizontalAlignment.Left)
        ListView_DO_Detail.Columns.Add("Jumlah", 0, HorizontalAlignment.Right)
        ListView_DO_Detail.Columns.Add("Retur", 0, HorizontalAlignment.Right)
        ListView_DO_Detail.Columns.Add("Sisa", 0, HorizontalAlignment.Right)
        ListView_DO_Detail.Columns.Add("Satuan", 0, HorizontalAlignment.Left)
        ListView_DO_Detail.Columns.Add("Urut", 0, HorizontalAlignment.Left)
        ListView_DO_Detail.View = View.Details

        'Dim Lokasi As Integer = 0

        ListView2.Columns.Add("Stock Owner", 0, HorizontalAlignment.Center)
        ListView2.Columns.Add("Kode Barang", 275, HorizontalAlignment.Left)
        ListView2.Columns.Add("Nama", 315, HorizontalAlignment.Left)
        ListView2.Columns.Add("Satuan", 0, HorizontalAlignment.Left)
        ListView2.Columns.Add("Harga Jual", 120, HorizontalAlignment.Right)
        ListView2.Columns.Add("Stock", 100, HorizontalAlignment.Right)
        ListView2.Columns.Add("Disc(%)", 0, HorizontalAlignment.Right)
        ListView2.Columns.Add("Disc(Rp.)", 0, HorizontalAlignment.Right)
        ListView2.View = View.Details

        ListView3.Columns.Add("Serial Number", 179, HorizontalAlignment.Left)
        ListView3.View = View.Details

        ListView5.Columns.Add("Kode Sales", 100, HorizontalAlignment.Left)
        ListView5.Columns.Add("Nama", 200, HorizontalAlignment.Left)
        ListView5.View = View.Details

        ListView20.Columns.Add("Kode Sales", 100, HorizontalAlignment.Left)
        ListView20.Columns.Add("Nama", 200, HorizontalAlignment.Left)
        ListView20.View = View.Details

        ListView10.Columns.Add("1", 0, HorizontalAlignment.Center)
        ListView10.Columns.Add("2", 0, HorizontalAlignment.Center)
        ListView10.Columns.Add("Kode Customer", 150, HorizontalAlignment.Left)
        ListView10.Columns.Add("Nama", 213, HorizontalAlignment.Left)
        ListView10.Columns.Add("Alamat", 240, HorizontalAlignment.Left)
        ListView10.Columns.Add("HP", 100, HorizontalAlignment.Left)
        ListView10.Columns.Add("Telepon", 100, HorizontalAlignment.Left)
        ListView10.View = View.Details

        ListView2.Location = New Point(7, 176)
        ListView2.Visible = False

        ListView3.Location = New Point(359, 190)
        ListView3.Visible = False

        ListView10.Location = New Point(143, 109)
        ListView10.Visible = False

        ListView5.Location = New Point(98, 451)
        ListView5.Visible = False

        ListView20.Location = New Point(798, 215)
        ListView20.Visible = False

        kd.Focus()
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

        If e.KeyCode = Keys.F1 Then
            If IsNumeric(kd.Text) = True Then
                jml.Text = Val(kd.Text)
                kd.Text = ""
            Else
                jml.Text = "1"
                kd.Text = ""
            End If
        ElseIf e.KeyCode = Keys.Down Then
            If ListView2.Items.Count = 0 Then Exit Sub
            ListView2.Focus()
        End If
    End Sub

    Private Sub kd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles kd.KeyPress

        'If e.KeyChar = Chr(13) Then
        '    nm.Focus()
        'End If
        If e.KeyChar = Chr(13) Then
            Label14.Text = "0"
            jml.Focus()
        End If
        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub nm_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles nm.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox4.Enabled = True Then
                TextBox4.Focus()
            Else
                ket.Focus()
            End If
        End If
    End Sub

    Private Sub ket_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ket.KeyPress
        If e.KeyChar = Chr(13) Then
            If hrg.Enabled = True Then
                hrg.Focus()
            Else
                jml.Focus()
            End If
        End If
        If e.KeyChar = Chr(Asc("'")) Or e.KeyChar = Chr(Asc("[")) Or e.KeyChar = Chr(Asc("]")) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub jml_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles jml.KeyDown
        If e.KeyCode = Keys.Up Then
            kd.Focus()
        End If
    End Sub

    Private Sub jml_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles jml.KeyPress
        If e.KeyChar = Chr(13) Then disc.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(Asc("-")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub hrg_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles hrg.KeyPress
        If e.KeyChar = Chr(13) Then jml.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub disc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles disc.KeyDown
        If e.KeyCode = Keys.Up Then
            jml.Focus()
        End If
    End Sub

    Private Sub disc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles disc.KeyPress
        If e.KeyChar = Chr(13) Then TextBox7.Focus()
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
        'HitungGrandTotal()
        'TextBox1.Text = Format(Val(HilangkanTanda(TxtSub.Text)) * Val(discz.Text) / 100, "N0")
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        bersihsebagian()
        kd.Focus()
    End Sub

    Private Sub ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ok.Click
        If ComboBox4.Text.Trim.Length = 0 Then
            MessageBox.Show("Lokasi harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox4.Focus() : Exit Sub
        ElseIf TextBoxGudang.Text.Trim.Length = 0 Then 'vv
            MessageBox.Show("Lokasi gudang harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox4.Focus() : Exit Sub
        ElseIf kd.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode barang belum diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kd.Focus()
            Exit Sub
        ElseIf jml.Text.Trim.Length = 0 Then
            MessageBox.Show("Jumlah belum diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            jml.Focus()
            Exit Sub
        ElseIf IsNumeric(jml.Text) = False Then
            MessageBox.Show("Jumlah yang dimasukan harus angka yang benar.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            jml.Focus()
            Exit Sub
        ElseIf Val(jml.Text) = 0 Then
            MessageBox.Show("Jumlah tidak boleh nol.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            jml.Focus()
            Exit Sub
        ElseIf hrg.Text.Trim.Length = 0 Then
            MessageBox.Show("Harga belum diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            hrg.Focus()
            Exit Sub
        ElseIf TextBox7.Text.Trim.Length = 0 Then
            MessageBox.Show("No. Nota belum diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox7.Focus()
            Exit Sub
        ElseIf TextBox8.Text.Trim.Length = 0 Then
            MessageBox.Show("No. Nota belum diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox8.Focus()
            Exit Sub
        ElseIf TextBox9.Text.Trim.Length = 0 Then
            MessageBox.Show("Sales belum diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox9.Focus()
            Exit Sub
        ElseIf Flag_Sendiri.Text.Trim.Length = 0 Then
            MessageBox.Show("Flag sendiri belum diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Flag_Sendiri.Focus()
            Exit Sub
        End If

        'If TextBox4.Enabled = True Then
        '    If TextBox4.Text.Trim.Length = 0 Then
        '        MessageBox.Show("Serial number belum diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        TextBox4.Focus()
        '        Exit Sub
        '    ElseIf Val(jml.Text) <> 1 Then
        '        MessageBox.Show("Barang yang memakai serial number jumlahnya harus 1.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        jml.Focus()
        '        Exit Sub
        '    End If
        'End If

        boleh_harga_min = "T"
        user_harga_min = "NULL"

        'If CheckBox3.Checked = True Then
        'If hrg.Text < Hrg_Minimum Then
        '    Dim tanya As String = MessageBox.Show("Harga yang dimasukan dibawah harga minimum! Akan dilanjutkan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        '    If tanya = vbYes Then
        '        Auth_.ShowDialog()
        '    Else
        '        Exit Sub
        '    End If

        '    If boleh_harga_min = "T" Then
        '        Exit Sub
        '    End If
        'End If
        ''End If        

        'If disc.Text.Trim.Length = 0 Then
        '    disc.Text = "0"
        'End If

        'If _gabung = "Y" Then
        '    If listview1.Items.Count > 0 Then
        '        For i As Integer = 0 To listview1.Items.Count - 1
        '            Get_Isi_Listview(i)

        '            If ComboBox4.Text.Trim.ToUpper = LvSO.Trim.ToUpper And kd.Text.Trim.ToUpper = LvKB.Trim.ToUpper Then
        '                MessageBox.Show("Kode barang sudah Anda masukkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Exit Sub
        '            End If
        '        Next
        '    End If
        'End If

        Dim input_baru As String = "Y"

        If listview1.Items.Count > 0 Then
            For i As Integer = 0 To listview1.Items.Count - 1
                Get_Isi_Listview(i)

                ' If ComboBox4.Text.Trim.ToUpper = LvSO.Trim.ToUpper And kd.Text.Trim.ToUpper = LvKB.Trim.ToUpper And LvKodePaketRetail = "" Then vv
                If TextBoxGudang.Text.Trim.ToUpper = LvSO.Trim.ToUpper And kd.Text.Trim.ToUpper = LvKB.Trim.ToUpper And LvKodePaketRetail = "" And LvDiscp = disc.Text And LvPkt2 = pkt2 And LvKodePaketRetail2 = kode_pkt2 Then
                    input_baru = "T"
                    listview1.Items(i).SubItems(5).Text = Val(HilangkanTanda(LvJml)) + Val(jml.Text)
                    LvJml = Val(HilangkanTanda(LvJml)) + Val(jml.Text)

                    Dim ttl_hrg As Double = 0

                    If Val(HilangkanTanda(LvDiscp)) <> 0 Then
                        ttl_hrg = (Val(HilangkanTanda(LvHrg)) * HilangkanTanda(TampilanDesimal(Val(LvJml)))) - (Val(HilangkanTanda(LvHrg)) * HilangkanTanda(TampilanDesimal(Val(LvJml))) * Val(LvDiscp) / 100)
                    Else
                        ttl_hrg = (Val(HilangkanTanda(LvHrg)) - Val(LvDiscrp)) * HilangkanTanda(TampilanDesimal(Val(LvJml)))
                    End If

                    listview1.Items(i).SubItems(9).Text = Format(ttl_hrg, "N0")
                    Exit For
                End If
            Next
        End If

        If input_baru = "Y" Then
            'lv = listview1.Items.Add(ComboBox4.Text) vv
            lv = listview1.Items.Add(TextBoxGudang.Text.Trim)
            lv.SubItems.Add(kd.Text.Trim)

            If ket.Text.ToUpper = "KETERANGAN. . ." Or ket.Text.Trim.Length = 0 Then
                lv.SubItems.Add(nm.Text)
            Else
                lv.SubItems.Add(nm.Text & "[" & Trim(ket.Text) & "]")
            End If
            If TextBox4.Enabled = True Then
                lv.SubItems.Add("-")
            Else
                lv.SubItems.Add("-")
            End If
            lv.SubItems.Add(Format(Val(hrg.Text), "N0"))
            lv.SubItems.Add(Format(Val(jml.Text), "N0"))
            lv.SubItems.Add(sat.Text)
            cek_diskon()
            lv.SubItems.Add(Format(total, "N0"))
            lv.SubItems.Add(Format(Hrg_Minimum, "N0"))
            lv.SubItems.Add(user_harga_min)
            If TextBox4.Enabled = True Then
                lv.SubItems.Add("Y")
            Else
                lv.SubItems.Add("T")
            End If
            lv.SubItems.Add("T")
            lv.SubItems.Add(Format(Hrg_Modal, "N0"))
            lv.SubItems.Add(TextBox7.Text.Trim & Format(Val(TextBox8.Text.Trim), "0000"))
            lv.SubItems.Add(TextBox9.Text.Trim)
            lv.SubItems.Add(TextBox12.Text.Trim)
            lv.SubItems.Add(Flag_Sendiri.Text.Trim)
            lv.SubItems.Add("")
            If disc.Text = "0" Then
                lv.SubItems.Add("Y")
            Else
                lv.SubItems.Add("T")
            End If

            lv.SubItems.Add(budgeting)
            lv.SubItems.Add(pkt2)
            lv.SubItems.Add(kode_pkt2)

            If pkt2 = "Y" Then
                listview1.Items(listview1.Items.Count - 1).BackColor = Color.Tan
            End If

            If Flag_Sendiri.Text = "Y" Then
                listview1.Items(listview1.Items.Count - 1).ForeColor = Color.Red
            Else
                listview1.Items(listview1.Items.Count - 1).ForeColor = Color.Blue
            End If
        End If

        For i As Integer = listview1.Items.Count - 1 To 0 Step -1
            If listview1.Items(i).SubItems(13).Text = "Y" Then
                listview1.Items(i).Remove()
            End If
        Next

        arrKDpromo.Clear() : arrKDXpromo.Clear() : arrJmlpromo.Clear()
        TextBox5.Text = "0"
        Disable_Customer()

        'Try
        '    OpenConn()

        '    Cmd.Transaction = Cn.BeginTransaction

        '    Cek_Promo()

        '    Cmd.Transaction.Commit()

        '    CloseConn()
        'Catch ex As Exception
        '    TextBox5.Text = "0"
        '    For i As Integer = listview1.Items.Count - 1 To 0 Step -1
        '        If listview1.Items(i).SubItems(13).Text = "Y" Then
        '            listview1.Items(i).Remove()
        '        End If
        '    Next

        '    HitungGrandTotal()

        '    CloseConn()
        '    MessageBox.Show("Proses perhitungan promo gagal!!! Ulangi transasksi ini lagi atau hubungi administrator!!!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        HitungGrandTotal()

        '==================



        'Dim sp = New SerialPort(My.Settings.Port_Cust_Display, 9600, Parity.None, 8, StopBits.One)
        'If Not (sp Is Nothing) Then
        '    sp.Open()
        '    '// to clear the display
        '    sp.Write(Convert.ToString(Chr(12)))

        '    '// first line goes here
        '    sp.WriteLine(jml.Text & " x " & nm.Text.Trim)

        '    '// 2nd line goes here 
        '    'sp.WriteLine(Chr(13) & Format(Val(hrg.Text), "N0") & " " & disc.Text & "% = " & Format(total, "N0"))
        '    sp.WriteLine(Chr(13) & "Disc " & disc.Text & "% = " & Format(total, "N0"))

        '    sp.Close()
        '    sp.Dispose()
        '    sp = Nothing
        'End If
        '==================

        bersihsebagian()
        kd.Focus()
    End Sub


    Private Sub ok_Click2(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ComboBox4.Text.Trim.Length = 0 Then
            MessageBox.Show("Lokasi harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox4.Focus() : Exit Sub
        ElseIf TextBoxGudang.Text.Trim.Length = 0 Then 'vv
            MessageBox.Show("Lokasi gudang harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox4.Focus() : Exit Sub
        ElseIf kd.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode barang belum diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kd.Focus()
            Exit Sub
        ElseIf jml.Text.Trim.Length = 0 Then
            MessageBox.Show("Jumlah belum diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            jml.Focus()
            Exit Sub
        ElseIf IsNumeric(jml.Text) = False Then
            MessageBox.Show("Jumlah yang dimasukan harus angka yang benar.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            jml.Focus()
            Exit Sub
        ElseIf Val(jml.Text) = 0 Then
            MessageBox.Show("Jumlah tidak boleh nol.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            jml.Focus()
            Exit Sub
        ElseIf hrg.Text.Trim.Length = 0 Then
            MessageBox.Show("Harga belum diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            hrg.Focus()
            Exit Sub
        ElseIf TextBox7.Text.Trim.Length = 0 Then
            MessageBox.Show("No. Nota belum diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox7.Focus()
            Exit Sub
        ElseIf TextBox8.Text.Trim.Length = 0 Then
            MessageBox.Show("No. Nota belum diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox8.Focus()
            Exit Sub
        ElseIf TextBox9.Text.Trim.Length = 0 Then
            MessageBox.Show("Sales belum diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox9.Focus()
            Exit Sub
        ElseIf Flag_Sendiri.Text.Trim.Length = 0 Then
            MessageBox.Show("Flag sendiri belum diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Flag_Sendiri.Focus()
            Exit Sub
        End If

        'If TextBox4.Enabled = True Then
        '    If TextBox4.Text.Trim.Length = 0 Then
        '        MessageBox.Show("Serial number belum diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        TextBox4.Focus()
        '        Exit Sub
        '    ElseIf Val(jml.Text) <> 1 Then
        '        MessageBox.Show("Barang yang memakai serial number jumlahnya harus 1.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        jml.Focus()
        '        Exit Sub
        '    End If
        'End If

        boleh_harga_min = "T"
        user_harga_min = "NULL"

        'If CheckBox3.Checked = True Then
        'If hrg.Text < Hrg_Minimum Then
        '    Dim tanya As String = MessageBox.Show("Harga yang dimasukan dibawah harga minimum! Akan dilanjutkan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        '    If tanya = vbYes Then
        '        Auth_.ShowDialog()
        '    Else
        '        Exit Sub
        '    End If

        '    If boleh_harga_min = "T" Then
        '        Exit Sub
        '    End If
        'End If
        ''End If        

        'If disc.Text.Trim.Length = 0 Then
        '    disc.Text = "0"
        'End If

        'If _gabung = "Y" Then
        '    If listview1.Items.Count > 0 Then
        '        For i As Integer = 0 To listview1.Items.Count - 1
        '            Get_Isi_Listview(i)

        '            If ComboBox4.Text.Trim.ToUpper = LvSO.Trim.ToUpper And kd.Text.Trim.ToUpper = LvKB.Trim.ToUpper Then
        '                MessageBox.Show("Kode barang sudah Anda masukkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Exit Sub
        '            End If
        '        Next
        '    End If
        'End If

        Dim input_baru As String = "Y"

        'If listview1.Items.Count > 0 Then
        '    For i As Integer = 0 To listview1.Items.Count - 1
        '        Get_Isi_Listview(i)

        '        ' If ComboBox4.Text.Trim.ToUpper = LvSO.Trim.ToUpper And kd.Text.Trim.ToUpper = LvKB.Trim.ToUpper And LvKodePaketRetail = "" Then vv
        '        If TextBoxGudang.Text.Trim.ToUpper = LvSO.Trim.ToUpper And kd.Text.Trim.ToUpper = LvKB.Trim.ToUpper And LvKodePaketRetail = "" And LvDiscp = disc.Text And LvPkt2 = pkt2 And LvKodePaketRetail2 = kode_pkt2 Then
        '            input_baru = "T"
        '            listview1.Items(i).SubItems(5).Text = Val(HilangkanTanda(LvJml)) + Val(jml.Text)
        '            LvJml = Val(HilangkanTanda(LvJml)) + Val(jml.Text)

        '            Dim ttl_hrg As Double = 0

        '            If Val(HilangkanTanda(LvDiscp)) <> 0 Then
        '                ttl_hrg = (Val(HilangkanTanda(LvHrg)) * HilangkanTanda(TampilanDesimal(Val(LvJml)))) - (Val(HilangkanTanda(LvHrg)) * HilangkanTanda(TampilanDesimal(Val(LvJml))) * Val(LvDiscp) / 100)
        '            Else
        '                ttl_hrg = (Val(HilangkanTanda(LvHrg)) - Val(LvDiscrp)) * HilangkanTanda(TampilanDesimal(Val(LvJml)))
        '            End If

        '            listview1.Items(i).SubItems(9).Text = Format(ttl_hrg, "N0")
        '            Exit For
        '        End If
        '    Next
        'End If

        If input_baru = "Y" Then
            'lv = listview1.Items.Add(ComboBox4.Text) vv
            lv = listview1.Items.Add(TextBoxGudang.Text.Trim)
            lv.SubItems.Add(kd.Text.Trim)

            If ket.Text.ToUpper = "KETERANGAN. . ." Or ket.Text.Trim.Length = 0 Then
                lv.SubItems.Add(nm.Text)
            Else
                lv.SubItems.Add(nm.Text & "[" & Trim(ket.Text) & "]")
            End If
            If TextBox4.Enabled = True Then
                lv.SubItems.Add("-")
            Else
                lv.SubItems.Add("-")
            End If
            lv.SubItems.Add(Format(Val(hrg.Text), "N0"))
            lv.SubItems.Add(Format(Val(jml.Text), "N0"))
            lv.SubItems.Add(sat.Text)
            cek_diskon()
            lv.SubItems.Add(Format(total, "N0"))
            lv.SubItems.Add(Format(Hrg_Minimum, "N0"))
            lv.SubItems.Add(user_harga_min)
            If TextBox4.Enabled = True Then
                lv.SubItems.Add("Y")
            Else
                lv.SubItems.Add("T")
            End If
            lv.SubItems.Add("T")
            lv.SubItems.Add(Format(Hrg_Modal, "N0"))
            lv.SubItems.Add(TextBox7.Text.Trim & Format(Val(TextBox8.Text.Trim), "0000"))
            lv.SubItems.Add(TextBox9.Text.Trim)
            lv.SubItems.Add(TextBox12.Text.Trim)
            lv.SubItems.Add(Flag_Sendiri.Text.Trim)
            lv.SubItems.Add("")
            If disc.Text = "0" Then
                lv.SubItems.Add("Y")
            Else
                lv.SubItems.Add("T")
            End If

            lv.SubItems.Add(budgeting)
            lv.SubItems.Add(pkt2)
            lv.SubItems.Add(kode_pkt2)

            If pkt2 = "Y" Then
                listview1.Items(listview1.Items.Count - 1).BackColor = Color.Tan
            End If

            If Flag_Sendiri.Text = "Y" Then
                listview1.Items(listview1.Items.Count - 1).ForeColor = Color.Red
            Else
                listview1.Items(listview1.Items.Count - 1).ForeColor = Color.Blue
            End If
        End If

        For i As Integer = listview1.Items.Count - 1 To 0 Step -1
            If listview1.Items(i).SubItems(13).Text = "Y" Then
                listview1.Items(i).Remove()
            End If
        Next

        arrKDpromo.Clear() : arrKDXpromo.Clear() : arrJmlpromo.Clear()
        TextBox5.Text = "0"
        Disable_Customer()

        'Try
        '    OpenConn()

        '    Cmd.Transaction = Cn.BeginTransaction

        '    Cek_Promo()

        '    Cmd.Transaction.Commit()

        '    CloseConn()
        'Catch ex As Exception
        '    TextBox5.Text = "0"
        '    For i As Integer = listview1.Items.Count - 1 To 0 Step -1
        '        If listview1.Items(i).SubItems(13).Text = "Y" Then
        '            listview1.Items(i).Remove()
        '        End If
        '    Next

        '    HitungGrandTotal()

        '    CloseConn()
        '    MessageBox.Show("Proses perhitungan promo gagal!!! Ulangi transasksi ini lagi atau hubungi administrator!!!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        HitungGrandTotal()

        '==================
        'Dim sp = New SerialPort(My.Settings.Port_Cust_Display, 9600, Parity.None, 8, StopBits.One)
        'sp.ReadLine()




        'Dim sp = New SerialPort(My.Settings.Port_Cust_Display, 9600, Parity.None, 8, StopBits.One)
        'If Not (sp Is Nothing) Then
        '    sp.Open()
        '    '// to clear the display
        '    sp.Write(Convert.ToString(Chr(12)))

        '    '// first line goes here
        '    sp.WriteLine(jml.Text & " x " & nm.Text.Trim)

        '    '// 2nd line goes here 
        '    'sp.WriteLine(Chr(13) & Format(Val(hrg.Text), "N0") & " " & disc.Text & "% = " & Format(total, "N0"))
        '    sp.WriteLine(Chr(13) & "Disc " & disc.Text & "% = " & Format(total, "N0"))

        '    sp.Close()
        '    sp.Dispose()
        '    sp = Nothing
        'End If
        '==================

        bersihsebagian()
        kd.Focus()
    End Sub

    Public Sub kd_Leave(ByVal sender As Object, ByVal e As System.EventArgs, ByVal dapethrgapa As String, ByVal _diskon_ As String, ByVal xpkt2 As String, ByVal xkdpkt2 As String)
        If kd.Text.Trim.Length = 0 Then Exit Sub
        If ListView2.Focused = True Then Exit Sub

        If ComboBox4.Text.Trim.Length = 0 Then
            MessageBox.Show("Lokasi harus diisi dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kd.Text = "" : kd.Focus()
            Exit Sub
        ElseIf TextBoxGudang.Text.Trim.Length = 0 Then 'vv
            MessageBox.Show("Lokasi gudang harus diisi dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kd.Text = "" : kd.Focus()
            Exit Sub
        ElseIf TextBox10.Text.Trim.Length = 0 Then
            MessageBox.Show("Customer harus diisi dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox10.Text = ""
            Exit Sub
        End If

        'If CheckBox3.Checked = True Then
        '    If TextBox22.Text.Trim.Length = 0 Then
        '        MessageBox.Show("No faktur sebelumnya harus diisi dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        TextBox22.Text = ""
        '        Exit Sub
        '    End If
        'End If

        'TextBox4.Enabled = False

        'ADA kd_Leave_Retail
        'ADA kd_Leave_Retail
        'ADA kd_Leave_Retail
        budgeting = ""
        pkt2 = ""
        kode_pkt2 = ""

        Try
            OpenConn()

            SQL = "Select a.f_hrg_ritel_max, a.f_hrg_ritel_min, a.last_hpp, a.f_hrg_resell_distributor, a.x_hrg_agency, a.x_hrg_mid, a.x_hrg_online, "
            SQL = SQL & "a.f_hrg_resell_special, a.x_hrg_special, a.flag_sendiri, a.f_hrg_resell_min, "
            SQL = SQL & "a.f_hrg_resell_std, a.f_hrg_resell_max, a.x_hrg_modern, a.x_hrg_min, "
            SQL = SQL & "a.x_hrg_max, a.harga_reseller, a.harga_beli, a.pakai_sn, a.kode_stock_owner, "
            SQL = SQL & "a.kode_barang, a.Nama, a.satuan, a.harga_jual, a.good_stock, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select disc1 from barang x where x.kode_perusahaan = a.kode_perusahaan and "
            SQL = SQL & "x.kode_barang = a.kode_barang And "
            SQL = SQL & "x.kode_stock_owner = '" & ComboBox4.Text & "'"
            SQL = SQL & "), 0) as disc1, "

            SQL = SQL & " a.disc2, "
            SQL = SQL & "isnull(("
            SQL = SQL & "select disc_tk_sdr from barang x where x.kode_perusahaan = a.kode_perusahaan and x.kode_barang = a.kode_barang and "
            SQL = SQL & "x.kode_stock_owner = '" & ComboBox4.Text & "'"
            SQL = SQL & "), 0) as diskon_tk_sendiri "
            SQL = SQL & " From barang a "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.aktif = 'Y' and "
            SQL = SQL & "a.kode_stock_owner = '" & TextBoxGudang.Text.Trim & "' and a.jenis = 'B' and "
            'SQL = SQL & "kode_stock_owner = '" & ComboBox4.Text & "' and jenis = 'B' and " vv
            SQL = SQL & "a.kode_barang = '" & Trim(kd.Text) & "' "
            If TextBox21.Text = "R" Then
                SQL = SQL & "and a.flag_ppn = 'T' "
            ElseIf TextBox21.Text = "C" Then
                SQL = SQL & "and a.flag_ppn = 'Y' "
            End If
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    TextBox4.Text = ""
                    If dr("pakai_sn") = "Y" Then
                        TextBox4.Enabled = True
                    Else
                        TextBox4.Enabled = False
                    End If
                    kd.Text = dr("kode_barang")
                    nm.Text = dr("nama")
                    ket.Text = "Keterangan. . ."

                    If dapethrgapa = "x" Then
                        hrg.Text = dr(pake_hrg_yg_mana)
                        budgeting = "T"
                    Else
                        hrg.Text = dr(dapethrgapa)
                        If _diskon_ <> "" Then
                            budgeting = "Y"
                        Else
                            budgeting = "Y"
                        End If
                    End If

                    Hrg_Minimum = dr("harga_beli")
                    Hrg_Modal = dr("harga_beli")
                    'jml.Text = "1"
                    sat.Text = dr("satuan")
                    Flag_Sendiri.Text = dr("flag_sendiri")

                    If cabang_sendiri = "T" Then
                        'disc.Text = dr("disc1")
                        If dapethrgapa = "x" Then
                            disc.Text = dr("disc1")
                            pkt2 = ""
                            kode_pkt2 = ""

                        Else
                            If _diskon_ <> "" Then
                                disc.Text = _diskon_
                                pkt2 = xpkt2
                                kode_pkt2 = xkdpkt2
                            Else
                                disc.Text = "0"
                                pkt2 = xpkt2
                                kode_pkt2 = xkdpkt2
                            End If
                        End If
                    Else
                        disc.Text = dr("diskon_tk_sendiri")
                        pkt2 = ""
                        kode_pkt2 = ""
                    End If

                    'If Reseller = "Y" Then
                    '    If flag_reseller.Item(ComboBox4.SelectedIndex) = "Y" Then
                    '        disc.Text = "0"
                    '    Else
                    '        disc.Text = dr("disc1")
                    '    End If
                    'Else
                    '    If cabang_sendiri = "T" Then
                    '        disc.Text = dr("disc1")
                    '    Else
                    '        disc.Text = "0"
                    '    End If
                    'End If

                    Label14.Text = "0"

                    ListView2.Visible = False

                    If Not (e Is Nothing) Then
                        ok_Click(kd, e)
                    End If
                Else
                    kd.Text = ""
                    nm.Text = ""
                    TextBox4.Text = ""
                    TextBox4.Enabled = False
                    ket.Text = "Keterangan. . ."
                    hrg.Text = ""
                    jml.Text = "1"
                    sat.Text = ""
                    disc.Text = ""
                    Flag_Sendiri.Text = ""
                    RadioButton1.Checked = True
                    Label14.Text = "0"
                    budgeting = ""
                    pkt2 = ""
                    kode_pkt2 = ""

                    kd.Focus()
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Public Sub kd_Leave_cut_off(ByVal sender As Object, ByVal e As System.EventArgs, ByVal dapethrgapa As String, ByVal _diskon_ As String, ByVal xpkt2 As String, ByVal xkdpkt2 As String, ByVal hpp As Double)
        If kd.Text.Trim.Length = 0 Then Exit Sub
        If ListView2.Focused = True Then Exit Sub

        If ComboBox4.Text.Trim.Length = 0 Then
            MessageBox.Show("Lokasi harus diisi dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kd.Text = "" : kd.Focus()
            Exit Sub
        ElseIf TextBoxGudang.Text.Trim.Length = 0 Then 'vv
            MessageBox.Show("Lokasi gudang harus diisi dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kd.Text = "" : kd.Focus()
            Exit Sub
        ElseIf TextBox10.Text.Trim.Length = 0 Then
            MessageBox.Show("Customer harus diisi dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox10.Text = ""
            Exit Sub
        End If

        'If CheckBox3.Checked = True Then
        '    If TextBox22.Text.Trim.Length = 0 Then
        '        MessageBox.Show("No faktur sebelumnya harus diisi dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        TextBox22.Text = ""
        '        Exit Sub
        '    End If
        'End If

        'TextBox4.Enabled = False

        'ADA kd_Leave_Retail
        'ADA kd_Leave_Retail
        'ADA kd_Leave_Retail
        budgeting = ""
        pkt2 = ""
        kode_pkt2 = ""

        Try
            OpenConn()

            SQL = "Select a.last_hpp, a.f_hrg_resell_distributor, a.x_hrg_agency, a.x_hrg_mid, a.x_hrg_online, "
            SQL = SQL & "a.f_hrg_resell_special, a.x_hrg_special, a.flag_sendiri, a.f_hrg_resell_min, "
            SQL = SQL & "a.f_hrg_resell_std, a.f_hrg_resell_max, a.x_hrg_modern, a.x_hrg_min, "
            SQL = SQL & "a.x_hrg_max, a.harga_reseller, a.harga_beli, a.pakai_sn, a.kode_stock_owner, "
            SQL = SQL & "a.kode_barang, a.Nama, a.satuan, a.harga_jual, a.good_stock, a.disc1, a.disc2, "
            SQL = SQL & "isnull(("
            SQL = SQL & "select disc_tk_sdr from barang x where x.kode_perusahaan = a.kode_perusahaan and x.kode_barang = a.kode_barang and "
            SQL = SQL & "x.kode_stock_owner = '" & ComboBox4.Text & "'"
            SQL = SQL & "), 0) as diskon_tk_sendiri "
            SQL = SQL & " From barang a "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.aktif = 'Y' and "
            SQL = SQL & "a.kode_stock_owner = '" & TextBoxGudang.Text.Trim & "' and a.jenis = 'B' and "
            'SQL = SQL & "kode_stock_owner = '" & ComboBox4.Text & "' and jenis = 'B' and " vv
            SQL = SQL & "a.kode_barang = '" & Trim(kd.Text) & "' "
            If TextBox21.Text = "R" Then
                SQL = SQL & "and a.flag_ppn = 'T' "
            ElseIf TextBox21.Text = "C" Then
                SQL = SQL & "and a.flag_ppn = 'Y' "
            End If
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    TextBox4.Text = ""
                    If dr("pakai_sn") = "Y" Then
                        TextBox4.Enabled = True
                    Else
                        TextBox4.Enabled = False
                    End If
                    kd.Text = dr("kode_barang")
                    nm.Text = dr("nama")
                    ket.Text = "Keterangan. . ."

                    If dapethrgapa = "x" Then
                        hrg.Text = hpp
                        budgeting = "T"
                    Else
                        hrg.Text = hpp
                        If _diskon_ <> "" Then
                            budgeting = "Y"
                        Else
                            budgeting = "Y"
                        End If
                    End If

                    Hrg_Minimum = dr("harga_beli")
                    Hrg_Modal = dr("harga_beli")
                    'jml.Text = "1"
                    sat.Text = dr("satuan")
                    Flag_Sendiri.Text = dr("flag_sendiri")

                    If cabang_sendiri = "T" Then
                        'disc.Text = dr("disc1")
                        If dapethrgapa = "x" Then
                            disc.Text = dr("disc1")
                            pkt2 = ""
                            kode_pkt2 = ""

                        Else
                            If _diskon_ <> "" Then
                                disc.Text = _diskon_
                                pkt2 = xpkt2
                                kode_pkt2 = xkdpkt2
                            Else
                                disc.Text = "0"
                                pkt2 = xpkt2
                                kode_pkt2 = xkdpkt2
                            End If
                        End If
                    Else
                        disc.Text = "0" 'dr("diskon_tk_sendiri")
                        pkt2 = ""
                        kode_pkt2 = ""
                    End If

                    'If Reseller = "Y" Then
                    '    If flag_reseller.Item(ComboBox4.SelectedIndex) = "Y" Then
                    '        disc.Text = "0"
                    '    Else
                    '        disc.Text = dr("disc1")
                    '    End If
                    'Else
                    '    If cabang_sendiri = "T" Then
                    '        disc.Text = dr("disc1")
                    '    Else
                    '        disc.Text = "0"
                    '    End If
                    'End If

                    Label14.Text = "0"

                    ListView2.Visible = False

                    If Not (e Is Nothing) Then
                        ok_Click2(kd, e)
                    End If
                Else
                    kd.Text = ""
                    nm.Text = ""
                    TextBox4.Text = ""
                    TextBox4.Enabled = False
                    ket.Text = "Keterangan. . ."
                    hrg.Text = ""
                    jml.Text = "1"
                    sat.Text = ""
                    disc.Text = ""
                    Flag_Sendiri.Text = ""
                    RadioButton1.Checked = True
                    Label14.Text = "0"
                    budgeting = ""
                    pkt2 = ""
                    kode_pkt2 = ""

                    kd.Focus()
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Public Sub kd_Leave_Retail(ByVal kd_pkt As String, ByVal disc_pkt As String, ByVal harus_budgeting As String)
        If kd.Text.Trim.Length = 0 Then Exit Sub

        If ComboBox4.Text.Trim.Length = 0 Then
            MessageBox.Show("Lokasi harus diisi dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kd.Text = "" : kd.Focus()
            Exit Sub
        ElseIf TextBoxGudang.Text.Trim.Length = 0 Then 'vv
            MessageBox.Show("Lokasi gudang harus diisi dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kd.Text = "" : kd.Focus()
            Exit Sub
        ElseIf TextBox10.Text.Trim.Length = 0 Then
            MessageBox.Show("Customer harus diisi dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox10.Text = ""
            Exit Sub
        End If

        'If CheckBox3.Checked = True Then
        '    If TextBox22.Text.Trim.Length = 0 Then
        '        MessageBox.Show("No faktur sebelumnya harus diisi dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        TextBox22.Text = ""
        '        Exit Sub
        '    End If
        'End If

        'TextBox4.Enabled = False
        budgeting = ""

        Try
            OpenConn()
            Dim lks_tujuan As String = ""
            SQL = "select  z.nama_kabupaten_kota+' - '+ v.nama_Kecamatan as Lokasi_tujuan from Emi_Customer_Gudang x, tbl_provinsi y, "
            SQL = SQL & "tbl_kabupaten_kota z, tbl_kecamatan v, tbl_kelurahan w where x.kode_perusahaan='" & KodePerusahaan & "' and "
            SQL = SQL & "x.Urut_Oto='" & idgudang.Text & "' and x.Id_Provinsi=y.Id_Provinsi and x.Id_Kabupaten_Kota=z.id_kabupaten_kota "
            SQL = SQL & "and x.Id_Kecamatan=v.id_kecamatan and x.Id_Kelurahan=w.id_kelurahan"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    lks_tujuan = dr("Lokasi_tujuan")
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Lokasi Tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "Select a.flag_sendiri, b.harga_jual as F_Hrg_Resell_Std, "
            SQL = SQL & "a.pakai_sn, a.kode_stock_owner, a.kode_barang, a.Nama, "
            SQL = SQL & "a.satuan, a.good_stock, a.disc1, a.disc2 "
            SQL = SQL & "From barang a, barang_detail_harga_jual b where "
            SQL = SQL & "a.Kode_Perusahaan = b.kode_perusahaan and a.Kode_Barang = b.kode_barang and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.aktif = 'Y' and "
            SQL = SQL & "a.kode_stock_owner = '" & TextBoxGudang.Text.Trim & "' and a.jenis = 'B' and " 'vv
            SQL = SQL & "a.kode_barang = '" & Trim(kd.Text) & "' "
            If TextBox21.Text = "R" Then
                SQL = SQL & "and a.flag_ppn = 'T' "
            ElseIf TextBox21.Text = "C" Then
                SQL = SQL & "and a.flag_ppn = 'Y' "
            End If

            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    TextBox4.Text = ""
                    If dr("pakai_sn") = "Y" Then
                        TextBox4.Enabled = True
                    Else
                        TextBox4.Enabled = False
                    End If
                    kd.Text = dr("kode_barang")
                    nm.Text = dr("nama")
                    ket.Text = "Keterangan. . ."

                    'If Reseller = "Y" Then
                    '    If flag_reseller.Item(ComboBox4.SelectedIndex) = "Y" Then 'cabang ini boleh reseller?
                    '        If user_ini_boleh_reseller = "Y" Then
                    '            hrg.Text = dr("harga_reseller") ' + (HilangkanTanda(Format((dr("harga_beli") * Val(arrDiskon.Item(0)) / 100), "N0")))
                    '        Else
                    '            hrg.Text = dr(pake_hrg_yg_mana)
                    '        End If
                    '    Else
                    '        hrg.Text = dr(pake_hrg_yg_mana)
                    '    End If
                    'Else
                    '    hrg.Text = dr(pake_hrg_yg_mana)
                    'End If
                    hrg.Text = dr(pake_hrg_yg_mana)
                    '            hrg.Text = dr("hjx")

                    budgeting = "T"

                    Hrg_Minimum = "0"
                    Hrg_Modal = "0"
                    'jml.Text = "1"
                    sat.Text = dr("satuan")
                    Flag_Sendiri.Text = dr("flag_sendiri")
                    RadioButton1.Checked = True

                    disc.Text = disc_pkt

                    Label14.Text = "0"

                    ListView2.Visible = False

                    If harus_budgeting = "Y" Then
                        budgeting = "Y"
                    End If

                    '===============


                    'lv = listview1.Items.Add(ComboBox4.Text)
                    lv = listview1.Items.Add(TextBoxGudang.Text.Trim) 'vv
                    lv.SubItems.Add(kd.Text.Trim)

                    If ket.Text.ToUpper = "KETERANGAN. . ." Or ket.Text.Trim.Length = 0 Then
                        lv.SubItems.Add(nm.Text)
                    Else
                        lv.SubItems.Add(nm.Text & "[" & Trim(ket.Text) & "]")
                    End If
                    If TextBox4.Enabled = True Then
                        lv.SubItems.Add("-")
                    Else
                        lv.SubItems.Add("-")
                    End If
                    lv.SubItems.Add(Format(Val(hrg.Text), "N0"))
                    lv.SubItems.Add(Format(Val(jml.Text), "N0"))
                    lv.SubItems.Add(sat.Text)
                    cek_diskon()
                    lv.SubItems.Add(Format(total, "N0"))
                    lv.SubItems.Add(Format(Hrg_Minimum, "N0"))
                    lv.SubItems.Add(user_harga_min)
                    If TextBox4.Enabled = True Then
                        lv.SubItems.Add("Y")
                    Else
                        lv.SubItems.Add("T")
                    End If
                    lv.SubItems.Add("T")
                    lv.SubItems.Add(Format(Hrg_Modal, "N0"))
                    lv.SubItems.Add(TextBox7.Text.Trim & Format(Val(TextBox8.Text.Trim), "0000"))
                    lv.SubItems.Add(TextBox9.Text.Trim)
                    lv.SubItems.Add(TextBox12.Text.Trim)
                    lv.SubItems.Add(Flag_Sendiri.Text.Trim)
                    lv.SubItems.Add(kd_pkt)
                    lv.SubItems.Add("T")
                    lv.SubItems.Add(budgeting)
                    lv.SubItems.Add("")
                    lv.SubItems.Add("")
                    lv.SubItems.Add(lks_tujuan)
                    lv.SubItems.Add(idgudang.Text)
                    listview1.Items(listview1.Items.Count - 1).BackColor = Color.SandyBrown

                    HitungGrandTotal()
                    '===============
                Else
                    idgudang.Text = ""
                    kd.Text = ""
                    nm.Text = ""
                    TextBox4.Text = ""
                    TextBox4.Enabled = False
                    ket.Text = "Keterangan. . ."
                    hrg.Text = ""
                    jml.Text = "1"
                    sat.Text = ""
                    disc.Text = ""
                    Flag_Sendiri.Text = ""
                    RadioButton1.Checked = True
                    Label14.Text = "0"
                    budgeting = ""
                    kd.Focus()
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        HitungGrandTotal()
    End Sub



    Private Sub listview1_ColumnWidthChanging(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnWidthChangingEventArgs) Handles listview1.ColumnWidthChanging
        Dim DisableColumns As Integer() = {4, 7, 8, 9, 10, 14}
        For Each DCol As Integer In DisableColumns
            If e.ColumnIndex = DCol Then
                e.Cancel = True
                e.NewWidth = listview1.Columns(DCol).Width
            End If
        Next DCol
    End Sub

    Private Sub listview1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles listview1.DoubleClick
        If listview1.Items.Count = 0 Then Exit Sub

        If TextBox23.Text.Trim.Length <> 0 Then Exit Sub

        listview1.FocusedItem.Remove()

        Disable_Customer()

        HitungGrandTotal()

        '==================


        'If Not (sp Is Nothing) Then
        '    sp.Open()
        '    '// to clear the display
        '    sp.Write(Convert.ToString(Chr(12)))

        '    '// first line goes here
        '    sp.WriteLine("Total Rp. " & Label12.Text)

        '    sp.Close()
        '    sp.Dispose()
        '    sp = Nothing
        'End If
        '==================
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If listview1.Items.Count = 0 Then kd.Focus() : Exit Sub

        get_jam()

        If TextBox21.Text.Trim.Length = 0 Then
            MessageBox.Show("Harap ulangi transaksi! Close transaksi penjualan! Karena transaksi tidak bisa diproses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        'If tgl_skrg = "" Then
        '    MessageBox.Show("Ada kesalahan pada tanggal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        If TextBox23.Text.Trim.Length = 0 Then
            MessageBox.Show("No PO/KB harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox23.Focus()
            Exit Sub
        ElseIf ComboBox4.SelectedIndex = -1 Then 'vv
            MessageBox.Show("Lokasi harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox4.Focus()
            Exit Sub
        ElseIf TextBoxGudang.Text.Trim.Length = 0 Then 'vv
            MessageBox.Show("Gudang harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox4.Focus()
            Exit Sub
        End If

        If TextBox10.Text.Trim.Length = 0 Then
            MessageBox.Show("Customer harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox10.Focus()
            Exit Sub
        ElseIf TextBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("Sales harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox2.Focus()
            Exit Sub
        ElseIf ComboBox2.SelectedIndex = -1 Then
            MessageBox.Show("Pembayaran harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus()
            Exit Sub
        End If

        If ComboBox2.SelectedIndex = 0 Then
            'If ComboBoxCb1.SelectedIndex = -1 Or ComboBoxCb1.SelectedIndex = 0 Then
            '    MessageBox.Show("Cara bayar harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    ComboBoxCb1.Focus()
            '    Exit Sub
            'End If
        ElseIf ComboBox2.SelectedIndex = 1 Then
            If Format(DateTimePicker2.Value, "yyyy-MM-dd") < Format(tgl_skrg, "yyyy-MM-dd") Then
                MessageBox.Show("Tanggal jatuh tempo tidak boleh kurang dari tanggal sekarang.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                DateTimePicker2.Focus()
                Exit Sub
            End If
        End If

        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("Kelipatan point harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus()
            Exit Sub
        End If

        If TextBox5.Text.Trim.Length = 0 Then discz.Text = 0
        If TextBox6.Text.Trim.Length = 0 Then discz.Text = 0
        If discz.Text.Trim.Length = 0 Then discz.Text = 0
        If Discx.Text.Trim.Length = 0 Then Discx.Text = 0

        'Dim arrKDpromo As New ArrayList
        'Dim arrKDXpromo As New ArrayList
        'Dim arrJmlpromo As New ArrayList

        'arrKDpromo.Clear() : arrKDXpromo.Clear() : arrJmlpromo.Clear()
        'Dim diskonbaru As Double = 0

        TextBox5.Text = "0"
        arrKDpromo.Clear() : arrKDXpromo.Clear() : arrJmlpromo.Clear()

        For i As Integer = listview1.Items.Count - 1 To 0 Step -1
            If listview1.Items(i).SubItems(13).Text = "Y" Then
                listview1.Items(i).Remove()
            End If
        Next

        '==================

        'Dim sp = New SerialPort(My.Settings.Port_Cust_Display, 9600, Parity.None, 8, StopBits.One)
        'If Not (sp Is Nothing) Then
        '    sp.Open()
        '    '// to clear the display
        '    sp.Write(Convert.ToString(Chr(12)))

        '    '// first line goes here
        '    sp.WriteLine("Total Rp. " & Label12.Text)

        '    sp.Close()
        '    sp.Dispose()
        '    sp = Nothing
        'End If
        '==================



        If Button2.Text.ToUpper = "&SIMPAN" Then
            'hasil_input_uang = "NO"

            'If ComboBox2.SelectedIndex = 0 Then 'tunai
            '    Input_Uang.ShowDialog()

            '    If hasil_input_uang = "NO" Then
            '        Exit Sub
            '    End If
            'End If
            Dim boleh_jual_rugi As String = ""

            Try
                OpenConn()

                If CekButtonRole("jual_rugi") = "T" Then
                    boleh_jual_rugi = "T"
                Else
                    boleh_jual_rugi = "Y"
                End If

                CloseConn()

            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            'Try
            '    OpenConn()

            '    Dim nfk As String = ""
            '    SQL = "select a.no_faktur from penjualan_blm_selesai_kirim a, stock_owner b where "
            '    SQL = SQL & "a.kode_perusahaan = b.Kode_Perusahaan and a.lokasi = b.Kode_Stock_Owner and "
            '    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
            '    SQL = SQL & "a.kode_customer = '" & TextBox10.Text.Trim & "' and "
            '    SQL = SQL & "a.jumlah - a.rtr - a.sdh_selesai_validasi - a.retur_val_do <> 0 and "
            '    SQL = SQL & "DATEADD(day, b.lama_blm_kirim, a.tanggal) < '" & Format(tgl_skrg, "yyyy-MM-dd") & "'"
            '    Using Dr = OpenTrans(SQL)
            '        Do While Dr.Read
            '            nfk = nfk & Dr("no_faktur") & ", "
            '        Loop
            '    End Using

            '    CloseConn()

            '    If nfk <> "" Then
            '        nfk = Strings.Left(nfk, Len(nfk) - 2)

            '        MessageBox.Show("Customer ini ada Proforma yang belum di kirim! No : " & nfk, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    End If

            'Catch ex As Exception
            '    CloseConn()
            '    MessageBox.Show(ex.Message)
            '    Exit Sub
            'End Try




            Dim faktur_ACC = ""
            Dim Sudah_ACC As String = "NULL"
            Dim User_ACC As String = "NULL"



            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                'awal coding stenly
                Dim flag_opm As String = ""

                SQL = "select flag_opname,buka_proforma from stock_owner "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & ComboBox4.Text & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Dr("flag_opname") = "Y" Then
                            flag_opm = "'Y'"
                        Else
                            flag_opm = "NULL"
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                'akhir coding stenly

                Dim TJT As String = ""
                If ComboBox2.SelectedIndex = 0 Then 'Tunai
                    TJT = "NULL"
                Else
                    TJT = "'" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "'"
                End If

                Dim diskon1 As Double = 0
                Dim diskon2 As Double = 0

                diskon1 = discz.Text

                If CheckBox2.Checked = True Then
                    diskon2 = Discx.Text
                Else
                    diskon2 = 0
                End If

                If ComboBox2.SelectedIndex = 0 Then
                    get_no_faktur("T")
                Else
                    get_no_faktur("K")
                End If

                sayTerbilang = General_Class.SayRupiah(HilangkanTanda(TxtTotal.Text))

                Dim ttl_point As Double = 0
                If pakai_point = "Y" Then
                    ttl_point = HilangkanTanda(Label21.Text)
                Else
                    ttl_point = 0
                End If

                'Dim cb_1 As String = ""
                'If ComboBox2.SelectedIndex = 0 Then 'kalo tunai
                '    cb_1 = "'" & ComboBoxCb1.Text & "'"
                'Else
                '    cb_1 = "NULL"
                'End If

                '---1
                Dim cabang_sendiri As String = ""
                Dim flag_agency As String = ""
                Dim flag_audit As String = ""
                Dim flag_tidak_budgeting As String = ""
                Dim flag_tidak_budgeting_toto As String = ""
                Dim flag_tidak_budgeting_bio As String = ""

                'Dim f_jenis_trans As String = ""
                'Dim f_plafon As String = ""
                'Dim f_lama_jt As Integer = 0
                Dim f_baru As String = ""
                Dim f_trans_baru As Integer = 0
                Dim cust_ini_budgeting As String = ""

                SQL = "select flag_audit, flag_tidak_budgeting_bio, flag_tidak_budgeting_toto, flag_tidak_budgeting, masuk_budgeting_bs, "
                SQL = SQL & "flag_agency, blacklist, cabang_sendiri, jenis_trans, "
                SQL = SQL & "plafon, lama_jt, baru, transaksi_baru from "
                SQL = SQL & "customers where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_customer = '" & TextBox10.Text.Trim & "'"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        cabang_sendiri = dr("cabang_sendiri")
                        flag_agency = dr("flag_agency")
                        flag_audit = dr("flag_audit")
                        flag_tidak_budgeting = General_Class.CekNULL(dr("flag_tidak_budgeting"))
                        flag_tidak_budgeting_toto = General_Class.CekNULL(dr("flag_tidak_budgeting_toto"))
                        flag_tidak_budgeting_bio = General_Class.CekNULL(dr("flag_tidak_budgeting_bio"))

                        'f_jenis_trans = dr("jenis_trans")
                        'f_plafon = dr("plafon")
                        'f_lama_jt = dr("lama_jt")
                        f_baru = dr("baru")
                        f_trans_baru = dr("transaksi_baru")
                        cust_ini_budgeting = dr("masuk_budgeting_bs")

                        If dr("blacklist") = "Y" Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Customer ini sudah di blacklist!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data customer tidak ditemukan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                Dim masukbudgeting As String = ""
                For i As Integer = 0 To listview1.Items.Count - 1
                    Get_Isi_Listview(i)

                    If LvBudgeting = "Y" Then
                        masukbudgeting = "Y"
                        Exit For
                    End If
                Next

                If cust_ini_budgeting = "T" Then
                    If masukbudgeting = "Y" Then
                        cust_ini_budgeting = "Y"

                        SQL = "update customers set masuk_budgeting_bs = 'Y' where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_customer = '" & TextBox10.Text.Trim & "'"
                        ExecuteTrans(SQL)
                    End If
                End If


                'If cabang_sendiri = "T" Then
                '    If ComboBox2.SelectedIndex = 0 Then
                '        If f_jenis_trans = "T" Then
                '            Dim f_ttl_jual As Double = 0

                '            SQL = "select isnull(sum(a.grand), 0) as ttl "
                '            SQL = SQL & "from penjualan a where "
                '            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "a.kode_customer = '" & TextBox10.Text.Trim & "' and a.jenis_transaksi = 'T' and "
                '            SQL = SQL & "a.flag_lunas_tunai is null and a.status is null"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    f_ttl_jual = Dr("ttl")
                '                End If
                '            End Using

                '            Dim f_ttl_retur As Double = 0
                '            SQL = "select isnull(sum(a.grand), 0) as ttl from retur_penjualan a, penjualan b where "
                '            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur_Jual = b.no_faktur and "
                '            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "b.kode_customer = '" & TextBox10.Text.Trim & "' and b.jenis_transaksi = 'T' and "
                '            SQL = SQL & "b.flag_lunas_tunai is null and a.status is null"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    f_ttl_retur = Dr("ttl")
                '                End If
                '            End Using

                '            Dim f_ttl_validasi As Double = 0
                '            SQL = "select isnull(sum(y.byr + y.disc_cash), 0) as ttl from val_penj_tunai x, detail_val_penj_tunai y, penjualan z where "
                '            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and "
                '            SQL = SQL & "x.no_val = y.no_val and y.No_Faktur = z.no_faktur and "
                '            SQL = SQL & "x.kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "z.kode_customer = '" & TextBox10.Text.Trim & "' and "
                '            SQL = SQL & "z.jenis_transaksi = 'T' and "
                '            SQL = SQL & "z.flag_lunas_tunai is null and x.status is null"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    f_ttl_validasi = Dr("ttl")
                '                End If
                '            End Using

                '            Dim f_ttl_validasi_do As Double = 0
                '            SQL = "select isnull(sum(y.byr + y.disc_cash), 0) as ttl from val_do_tunai x, detail_val_do_tunai y, penjualan z, do_new r where "
                '            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and z.kode_perusahaan = r.kode_perusahaan and "
                '            SQL = SQL & "x.no_val = y.no_val and y.No_Faktur = r.no_do and z.no_faktur = r.no_faktur and "
                '            SQL = SQL & "x.kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "z.kode_customer = '" & TextBox10.Text.Trim & "' and "
                '            SQL = SQL & "z.jenis_transaksi = 'T' and "
                '            SQL = SQL & "z.flag_lunas_tunai is null and x.status is null"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    f_ttl_validasi_do = Dr("ttl")
                '                End If
                '            End Using

                '            If f_ttl_jual - f_ttl_retur - f_ttl_validasi - f_ttl_validasi_do + Val(HilangkanTanda(TxtTotal.Text)) > Val(arrPlafonTunai.Item(ComboBox4.SelectedIndex)) Then
                '                CloseTrans()
                '                CloseConn()
                '                'MessageBox.Show("Customer ini masih ada PIUTANG TUNAI yang belum dilunasi! Lunasi dahulu agar transaksi bisa dilanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                MessageBox.Show("Customer ini masih ada piutang TUNAI yang belum dilunasi! Lunasi dahulu agar transaksi bisa dilanjutkan! " & Chr(13) & "Tunai = Rp. " & Format((f_ttl_jual - f_ttl_retur - f_ttl_validasi - f_ttl_validasi_do), "N0") & Chr(13) & "Transaksi Ini = " & Format(Val(HilangkanTanda(TxtTotal.Text)), "N0") & Chr(13) & "Total = " & Format((f_ttl_jual - f_ttl_retur - f_ttl_validasi - f_ttl_validasi_do) + Val(HilangkanTanda(TxtTotal.Text)), "N0") & Chr(13) & "Plafon = " & Format(Val(arrPlafonTunai.Item(ComboBox4.SelectedIndex)), "N0") & Chr(13) & "Kekurangan = " & Format((f_ttl_jual - f_ttl_retur - f_ttl_validasi - f_ttl_validasi_do) + Val(HilangkanTanda(TxtTotal.Text)) - Val(arrPlafonTunai.Item(ComboBox4.SelectedIndex)), "N0"), Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                Exit Sub
                '            End If

                '            'If Val(HilangkanTanda(TxtTotal.Text)) > Val(arrPlafonTunai.Item(ComboBox4.SelectedIndex)) Then
                '            '    CloseTrans()
                '            '    CloseConn()
                '            '    MessageBox.Show("Customer ini masih ada piutang yang belum dilunasi! Lunasi dahulu agar transaksi bisa dilanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            '    Exit Sub
                '            'End If
                '        Else
                '            'baca plafon kredit

                '            SQL = "select top(1) tanggal from Cek_Jth_Tempo where "
                '            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "kode_customer = '" & TextBox10.Text.Trim & "' and jenis_transaksi = 'N' and "
                '            SQL = SQL & "flag_lunas is null and status is null and grand - rtr - val_penj - val_do > 20 order by tanggal + jam asc"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    If Format(DateAdd(DateInterval.Day, f_lama_jt, Dr("tanggal")), "yyyy-MM-dd") < Format(tgl_skrg, "yyyy-MM-dd") Then
                '                        Dr.Close()
                '                        CloseTrans()
                '                        CloseConn()
                '                        MessageBox.Show("Customer ini ada faktur yang sudah lewat jatuh tempo! Lunasi dahulu agar transaksi bisa dilanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                        Exit Sub
                '                    End If
                '                End If
                '            End Using


                '            Dim f_ttl_jual_kredit As Double = 0
                '            SQL = "select isnull(sum(a.grand), 0) as ttl "
                '            SQL = SQL & "from penjualan a where "
                '            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "a.kode_customer = '" & TextBox10.Text.Trim & "' and a.jenis_transaksi = 'N' and "
                '            SQL = SQL & "a.flag_lunas is null and a.status is null"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    f_ttl_jual_kredit = Dr("ttl")
                '                End If
                '            End Using

                '            Dim f_ttl_retur_kredit As Double = 0
                '            SQL = "select isnull(sum(a.grand), 0) as ttl from retur_penjualan a, penjualan b where "
                '            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur_Jual = b.no_faktur and "
                '            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "b.kode_customer = '" & TextBox10.Text.Trim & "' and b.jenis_transaksi = 'N' and "
                '            SQL = SQL & "b.flag_lunas is null and a.status is null"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    f_ttl_retur_kredit = Dr("ttl")
                '                End If
                '            End Using

                '            Dim f_ttl_validasi_kredit As Double = 0
                '            SQL = "select isnull(sum(y.byr), 0) as ttl from val_penj x, detail_val_penj y, penjualan z where "
                '            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and "
                '            SQL = SQL & "x.no_val = y.no_val and y.No_Faktur = z.no_faktur and "
                '            SQL = SQL & "x.kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "z.kode_customer = '" & TextBox10.Text.Trim & "' and "
                '            SQL = SQL & "z.jenis_transaksi = 'N' and "
                '            SQL = SQL & "z.flag_lunas is null and x.status is null"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    f_ttl_validasi_kredit = Dr("ttl")
                '                End If
                '            End Using

                '            Dim f_ttl_validasi_do_kredit As Double = 0
                '            SQL = "select isnull(sum(y.byr), 0) as ttl from val_do x, detail_val_do y, penjualan z, do_new r where "
                '            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and z.kode_perusahaan = r.kode_perusahaan and "
                '            SQL = SQL & "x.no_val = y.no_val and y.No_Faktur = r.no_do and z.no_faktur = r.no_faktur and "
                '            SQL = SQL & "x.kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "z.kode_customer = '" & TextBox10.Text.Trim & "' and "
                '            SQL = SQL & "z.jenis_transaksi = 'N' and "
                '            SQL = SQL & "z.flag_lunas_tunai is null and x.status is null"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    f_ttl_validasi_do_kredit = Dr("ttl")
                '                End If
                '            End Using

                '            '========tunai 

                '            Dim f_ttl_jual_tunai As Double = 0
                '            SQL = "select isnull(sum(a.grand), 0) as ttl "
                '            SQL = SQL & "from penjualan a where "
                '            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "a.kode_customer = '" & TextBox10.Text.Trim & "' and a.jenis_transaksi = 'T' and "
                '            SQL = SQL & "a.flag_lunas_tunai is null and a.status is null"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    f_ttl_jual_tunai = Dr("ttl")
                '                End If
                '            End Using

                '            Dim f_ttl_retur_tunai As Double = 0
                '            SQL = "select isnull(sum(a.grand), 0) as ttl from retur_penjualan a, penjualan b where "
                '            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur_Jual = b.no_faktur and "
                '            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "b.kode_customer = '" & TextBox10.Text.Trim & "' and b.jenis_transaksi = 'T' and "
                '            SQL = SQL & "b.flag_lunas_tunai is null and a.status is null"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    f_ttl_retur_tunai = Dr("ttl")
                '                End If
                '            End Using

                '            Dim f_ttl_validasi_tunai As Double = 0
                '            SQL = "select isnull(sum(y.byr + y.disc_cash), 0) as ttl from val_penj_tunai x, detail_val_penj_tunai y, penjualan z where "
                '            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and "
                '            SQL = SQL & "x.no_val = y.no_val and y.No_Faktur = z.no_faktur and "
                '            SQL = SQL & "x.kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "z.kode_customer = '" & TextBox10.Text.Trim & "' and "
                '            SQL = SQL & "z.jenis_transaksi = 'T' and "
                '            SQL = SQL & "z.flag_lunas_tunai is null and x.status is null"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    f_ttl_validasi_tunai = Dr("ttl")
                '                End If
                '            End Using

                '            Dim f_ttl_validasi_do_tunai As Double = 0
                '            SQL = "select isnull(sum(y.byr + y.disc_cash), 0) as ttl from val_do_tunai x, detail_val_do_tunai y, penjualan z, do_new r where "
                '            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and z.kode_perusahaan = r.kode_perusahaan and "
                '            SQL = SQL & "x.no_val = y.no_val and y.No_Faktur = r.no_do and z.no_faktur = r.no_faktur and "
                '            SQL = SQL & "x.kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "z.kode_customer = '" & TextBox10.Text.Trim & "' and "
                '            SQL = SQL & "z.jenis_transaksi = 'T' and "
                '            SQL = SQL & "z.flag_lunas_tunai is null and x.status is null"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    f_ttl_validasi_do_tunai = Dr("ttl")
                '                End If
                '            End Using

                '            '============
                '            If (f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit) + (f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai) + Val(HilangkanTanda(TxtTotal.Text)) > f_plafon Then
                '                CloseTrans()
                '                CloseConn()
                '                MessageBox.Show("Customer ini masih ada piutang TUNAI + KREDIT yang belum dilunasi! Lunasi dahulu agar transaksi bisa dilanjutkan! " & Chr(13) & "Tunai = Rp. " & Format((f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai), "N0") & Chr(13) & "Kredit = Rp. " & Format((f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit), "N0") & Chr(13) & "Transaksi Ini = " & Format(Val(HilangkanTanda(TxtTotal.Text)), "N0") & Chr(13) & "Total = " & Format((f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit) + (f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai) + Val(HilangkanTanda(TxtTotal.Text)), "N0") & Chr(13) & "Plafon = " & Format(Val(f_plafon), "N0") & Chr(13) & "Kekurangan = " & Format((f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit) + (f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai) + Val(HilangkanTanda(TxtTotal.Text)) - Val(f_plafon), "N0"), Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                Exit Sub
                '            End If
                '        End If
                '    End If

                '    If ComboBox2.SelectedIndex = 1 Then
                '        If f_jenis_trans = "N" Then
                '            SQL = "select top(1) tanggal from Cek_Jth_Tempo where "
                '            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "kode_customer = '" & TextBox10.Text.Trim & "' and jenis_transaksi = 'N' and "
                '            SQL = SQL & "flag_lunas is null and status is null and grand - rtr - val_penj - val_do > 20 order by tanggal + jam asc"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    If Format(DateAdd(DateInterval.Day, f_lama_jt, Dr("tanggal")), "yyyy-MM-dd") < Format(tgl_skrg, "yyyy-MM-dd") Then
                '                        Dr.Close()
                '                        CloseTrans()
                '                        CloseConn()
                '                        MessageBox.Show("Customer ini ada faktur yang sudah lewat jatuh tempo! Lunasi dahulu agar transaksi bisa dilanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                        Exit Sub
                '                    End If
                '                End If
                '            End Using


                '            Dim f_ttl_jual_kredit As Double = 0
                '            SQL = "select isnull(sum(a.grand), 0) as ttl "
                '            SQL = SQL & "from penjualan a where "
                '            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "a.kode_customer = '" & TextBox10.Text.Trim & "' and a.jenis_transaksi = 'N' and "
                '            SQL = SQL & "a.flag_lunas is null and a.status is null"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    f_ttl_jual_kredit = Dr("ttl")
                '                End If
                '            End Using

                '            Dim f_ttl_retur_kredit As Double = 0
                '            SQL = "select isnull(sum(a.grand), 0) as ttl from retur_penjualan a, penjualan b where "
                '            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur_Jual = b.no_faktur and "
                '            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "b.kode_customer = '" & TextBox10.Text.Trim & "' and b.jenis_transaksi = 'N' and "
                '            SQL = SQL & "b.flag_lunas is null and a.status is null"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    f_ttl_retur_kredit = Dr("ttl")
                '                End If
                '            End Using

                '            Dim f_ttl_validasi_kredit As Double = 0
                '            SQL = "select isnull(sum(y.byr), 0) as ttl from val_penj x, detail_val_penj y, penjualan z where "
                '            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and "
                '            SQL = SQL & "x.no_val = y.no_val and y.No_Faktur = z.no_faktur and "
                '            SQL = SQL & "x.kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "z.kode_customer = '" & TextBox10.Text.Trim & "' and "
                '            SQL = SQL & "z.jenis_transaksi = 'N' and "
                '            SQL = SQL & "z.flag_lunas is null and x.status is null"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    f_ttl_validasi_kredit = Dr("ttl")
                '                End If
                '            End Using


                '            Dim f_ttl_validasi_do_kredit As Double = 0
                '            SQL = "select isnull(sum(y.byr), 0) as ttl from val_do x, detail_val_do y, penjualan z, do_new r where "
                '            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and z.kode_perusahaan = r.kode_perusahaan and "
                '            SQL = SQL & "x.no_val = y.no_val and y.No_Faktur = r.no_do and z.no_faktur = r.no_faktur and "
                '            SQL = SQL & "x.kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "z.kode_customer = '" & TextBox10.Text.Trim & "' and "
                '            SQL = SQL & "z.jenis_transaksi = 'N' and "
                '            SQL = SQL & "z.flag_lunas_tunai is null and x.status is null"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    f_ttl_validasi_do_kredit = Dr("ttl")
                '                End If
                '            End Using

                '            '========tunai 

                '            Dim f_ttl_jual_tunai As Double = 0
                '            SQL = "select isnull(sum(a.grand), 0) as ttl "
                '            SQL = SQL & "from penjualan a where "
                '            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "a.kode_customer = '" & TextBox10.Text.Trim & "' and a.jenis_transaksi = 'T' and "
                '            SQL = SQL & "a.flag_lunas_tunai is null and a.status is null"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    f_ttl_jual_tunai = Dr("ttl")
                '                End If
                '            End Using

                '            Dim f_ttl_retur_tunai As Double = 0
                '            SQL = "select isnull(sum(a.grand), 0) as ttl from retur_penjualan a, penjualan b where "
                '            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur_Jual = b.no_faktur and "
                '            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "b.kode_customer = '" & TextBox10.Text.Trim & "' and b.jenis_transaksi = 'T' and "
                '            SQL = SQL & "b.flag_lunas_tunai is null and a.status is null"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    f_ttl_retur_tunai = Dr("ttl")
                '                End If
                '            End Using

                '            Dim f_ttl_validasi_tunai As Double = 0
                '            SQL = "select isnull(sum(y.byr + y.disc_cash), 0) as ttl from val_penj_tunai x, detail_val_penj_tunai y, penjualan z where "
                '            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and "
                '            SQL = SQL & "x.no_val = y.no_val and y.No_Faktur = z.no_faktur and "
                '            SQL = SQL & "x.kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "z.kode_customer = '" & TextBox10.Text.Trim & "' and "
                '            SQL = SQL & "z.jenis_transaksi = 'T' and "
                '            SQL = SQL & "z.flag_lunas_tunai is null and x.status is null"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    f_ttl_validasi_tunai = Dr("ttl")
                '                End If
                '            End Using

                '            Dim f_ttl_validasi_do_tunai As Double = 0
                '            SQL = "select isnull(sum(y.byr + y.disc_cash), 0) as ttl from val_do_tunai x, detail_val_do_tunai y, penjualan z, do_new r where "
                '            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and z.kode_perusahaan = r.kode_perusahaan and "
                '            SQL = SQL & "x.no_val = y.no_val and y.No_Faktur = r.no_do and z.no_faktur = r.no_faktur and "
                '            SQL = SQL & "x.kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "z.kode_customer = '" & TextBox10.Text.Trim & "' and "
                '            SQL = SQL & "z.jenis_transaksi = 'T' and "
                '            SQL = SQL & "z.flag_lunas_tunai is null and x.status is null"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    f_ttl_validasi_do_tunai = Dr("ttl")
                '                End If
                '            End Using

                '            '============
                '            If (f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit) + (f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai) + Val(HilangkanTanda(TxtTotal.Text)) > f_plafon Then
                '                CloseTrans()
                '                CloseConn()
                '                MessageBox.Show("Customer ini masih ada piutang TUNAI + KREDIT yang belum dilunasi! Lunasi dahulu agar transaksi bisa dilanjutkan! " & Chr(13) & "Tunai = Rp. " & Format((f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai), "N0") & Chr(13) & "Kredit = Rp. " & Format((f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit), "N0") & Chr(13) & "Transaksi Ini = " & Format(Val(HilangkanTanda(TxtTotal.Text)), "N0") & Chr(13) & "Total = " & Format((f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit) + (f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai) + Val(HilangkanTanda(TxtTotal.Text)), "N0") & Chr(13) & "Plafon = " & Format(Val(f_plafon), "N0") & Chr(13) & "Kekurangan = " & Format((f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit) + (f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai) + Val(HilangkanTanda(TxtTotal.Text)) - Val(f_plafon), "N0"), Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                Exit Sub
                '            End If

                '        Else

                '            CloseTrans()
                '            CloseConn()
                '            MessageBox.Show("Ada kesalahan pada jenis transaksi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            Exit Sub
                '        End If
                '    End If

                '    If f_baru = "Y" Then
                '        SQL = "select top(1) count(no_faktur) as ttl_faktur from penjualan where "
                '        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '        SQL = SQL & "kode_customer = '" & TextBox10.Text.Trim & "' and "
                '        SQL = SQL & "status is null"
                '        Using Dr = OpenTrans(SQL)
                '            If Dr.Read Then
                '                If Dr("ttl_faktur") <> 0 Then
                '                    Dr.Close()
                '                    CloseTrans()
                '                    CloseConn()
                '                    MessageBox.Show("Proses tidak dapat dilanjutkan! Customer ini belum diverifikasi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                    Exit Sub
                '                End If
                '            End If
                '        End Using

                '        SQL = "update customers set transaksi_baru = transaksi_baru + 1 where "
                '        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '        SQL = SQL & "kode_customer = '" & TextBox10.Text.Trim & "'"
                '        ExecuteTrans(SQL)
                '    End If
                'End If

                If cabang_sendiri = "T" Then
                    If f_baru = "Y" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan! Customer ini belum diverifikasi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub

                        'SQL = "select top(1) count(no_faktur) as ttl_faktur from penjualan where "
                        'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        'SQL = SQL & "kode_customer = '" & TextBox10.Text.Trim & "' and "
                        'SQL = SQL & "status is null"
                        'Using Dr = OpenTrans(SQL)
                        '    If Dr.Read Then
                        '        If Dr("ttl_faktur") <> 0 Then
                        '            Dr.Close()
                        '            CloseTrans()
                        '            CloseConn()
                        '            MessageBox.Show("Proses tidak dapat dilanjutkan! Customer ini belum diverifikasi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '            Exit Sub
                        '        End If
                        '    End If
                        'End Using

                        SQL = "update customers set transaksi_baru = transaksi_baru + 1 where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_customer = '" & TextBox10.Text.Trim & "'"
                        ExecuteTrans(SQL)
                    End If
                End If


                Get_Data_Acc()

                Dim no_po_toko As String = "NULL"
                ' Dim kode_cust_toko_sdr As String = ""


                '==========
                Dim no_kb As String = ""
                If TextBox23.Text.Trim.Length = 0 Then
                    no_kb = "NULL"
                Else
                    no_kb = "'" & TextBox23.Text.Trim & "'"

                    SQL = "select status, cast(rv as bigint) as rvx, flag_sudah from emi_po where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & TextBox23.Text.Trim & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If General_Class.CekNULL(Dr("status")) = "Y" Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini sudah dibatalkan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf General_Class.CekNULL(Dr("flag_sudah")) = "Y" Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini sudah selesai dibuat penjualan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf Dr("rvx") <> RV_Permintaan_Keluar Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini sudah diubah sebelumnya!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            End If
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data keluar barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using


                End If

                '==========

                Dim no_agency As String = ""

                Dim pakai_akun_piutang_apa As String = ""
                If cabang_sendiri = "Y" Then
                    If flag_agency = "T" Then
                        pakai_akun_piutang_apa = X_Piutang_Cabang_Sendiri
                    Else
                        pakai_akun_piutang_apa = X_Piutang_Sementara_Agency
                    End If
                Else
                    pakai_akun_piutang_apa = X_Piutang
                End If

                Dim no_fak_sebelumnya As String = ""
                If TextBox22.Text.Trim.Length = 0 Then
                    no_fak_sebelumnya = "NULL"
                Else
                    no_fak_sebelumnya = "'" & TextBox22.Text.Trim & "'"
                End If

                If TextBox22.Text.Trim.Length <> 0 And discz.Text.Trim.Length <> 0 Then
                    Dim sub_sblmnya_brg_org As Double = 0
                    Dim total_sblmnya As Double = 0
                    Dim ppn_sblmnya As Double = 0
                    Dim kode_vch_sblmnya As String = ""

                    SQL = "select total, ppn, kode_voucher_1 from penjualan where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_faktur = '" & TextBox22.Text.Trim & "' and status is null"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            total_sblmnya = Dr("total")
                            ppn_sblmnya = Dr("ppn")
                            kode_vch_sblmnya = Dr("kode_voucher_1")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data faktur sebelumnya tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select isnull(sum(subtotal), 0) as subttl from detail_penjualan where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_faktur = '" & TextBox22.Text.Trim & "' and flag_sdr = 'T'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            sub_sblmnya_brg_org = Dr("subttl")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data faktur sebelumnya tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    Dim hsl_dis As Double = HilangkanTanda(Format(sub_sblmnya_brg_org * discz.Text / 100, "N0"))
                    Dim nilai_ppn_sekarang As Double = HilangkanTanda(Format((total_sblmnya - hsl_dis) * ppn_sblmnya / 100, "N0"))
                    Dim grand_sekarang As Double = total_sblmnya - hsl_dis + nilai_ppn_sekarang
                    SQL = "update penjualan set total_u_dis_member = " & sub_sblmnya_brg_org & ", "
                    SQL = SQL & "disc1 = '" & discz.Text & "', "
                    SQL = SQL & "hasil_diskon = " & hsl_dis & ", "
                    SQL = SQL & "nilai_ppn = " & nilai_ppn_sekarang & ", "
                    SQL = SQL & "grand = " & grand_sekarang & ", "
                    SQL = SQL & "terbilang = '" & General_Class.SayRupiah(Convert.ToInt32(grand_sekarang)) & "', "
                    SQL = SQL & "no_fak_setelahnya = '" & TxtFaktur.Text.Trim & "' "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_faktur = '" & TextBox22.Text.Trim & "'"
                    ExecuteTrans(SQL)

                    'SQL = "update detail_jurnal set debit = " & grand_sekarang & " where "
                    'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "kode_voucher = '" & kode_vch_sblmnya & "' and debit <> 0"
                    'ExecuteTrans(SQL) 'kas/piutang

                    'SQL = "update detail_jurnal set kredit = " & total_sblmnya - hsl_dis & " where "
                    'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "kode_voucher = '" & kode_vch_sblmnya & "' and kredit <> 0 and "
                    'SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc <> '125020'"
                    'ExecuteTrans(SQL) 'ppn

                    'SQL = "update detail_jurnal set kredit = " & nilai_ppn_sekarang & " where "
                    'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "kode_voucher = '" & kode_vch_sblmnya & "' and kredit <> 0 and "
                    'SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '125020'"
                    'ExecuteTrans(SQL) 'ppn

                    'SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                    'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "kode_voucher = '" & kode_vch_sblmnya & "'"
                    'Using Dr = OpenTrans(SQL)
                    '    If Dr.Read Then
                    '        If Dr("debit") <> Dr("kredit") Then
                    '            Dr.Close()
                    '            CloseTrans()
                    '            CloseConn()
                    '            MessageBox.Show("Jurnal lama salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '            Exit Sub
                    '        End If
                    '    Else
                    '        Dr.Close()
                    '        CloseTrans()
                    '        CloseConn()
                    '        MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '        Exit Sub
                    '    End If
                    'End Using
                End If

                Dim kd_cust_asl As String = ""
                Dim ket_cust_asl As String = ""
                Dim kd_cust_fk As String = ""

                If TextBox21.Text.Trim.ToUpper = "C" Then 'ppn
                    'INI UNTUK GANJIL GENAP SMNTARA DI BLOK

                    'If Format(tgl_skrg, "dd") Mod 2 = 1 And cabang_sendiri = "Y" Then 'ganjil
                    '    SQL = "select init_cust_dist, kode_stock_owner from serverd2_ev.dbo.stock_owner where "
                    '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    '    SQL = SQL & "kode_cust_distributor = '" & TextBox10.Text & "'"
                    '    Using dr = OpenTrans(SQL)
                    '        If dr.Read Then
                    '            kd_cust_asl = "'" & dr("init_cust_dist") & "'"
                    '            ket_cust_asl = "'" & dr("kode_stock_owner") & "'"
                    '        Else
                    '            dr.Close()
                    '            CloseTrans()
                    '            CloseConn()
                    '            MessageBox.Show("Customer tidak ditemukan!!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '            Exit Sub
                    '        End If
                    '    End Using


                    '    SQL = "select top(1) kode_customer from customers where "
                    '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    '    SQL = SQL & "fk = 'Y' ORDER BY NEWID()"
                    '    Using dr = OpenTrans(SQL)
                    '        If dr.Read Then
                    '            kd_cust_fk = "'" & dr("kode_customer") & "'"
                    '        Else
                    '            dr.Close()
                    '            CloseTrans()
                    '            CloseConn()
                    '            MessageBox.Show("Customer tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '            Exit Sub
                    '        End If
                    '    End Using
                    'Else
                    '    kd_cust_fk = "'" & TextBox10.Text.Trim & "'"
                    '    kd_cust_asl = "NULL"
                    '    ket_cust_asl = "NULL"
                    'End If
                    kd_cust_fk = "'" & TextBox10.Text.Trim & "'"
                    kd_cust_asl = "NULL"
                    ket_cust_asl = "NULL"
                ElseIf TextBox21.Text.Trim.ToUpper = "R" Then 'non
                    kd_cust_fk = "'" & TextBox10.Text.Trim & "'"
                    kd_cust_asl = "NULL"
                    ket_cust_asl = "NULL"
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Ulangi transaksi lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                Dim lama_diskon_sementara As Integer = 0
                If Val(TextBox27.Text) <> 0 Then
                    lama_diskon_sementara = 14
                Else
                    lama_diskon_sementara = 0
                End If

                SQL = "insert into penjualan(Kode_perusahaan,no_faktur,Tanggal,jam,kode_customer,"
                SQL = SQL & "Jenis_transaksi,Tgl_Jatuh_tempo,UserId,disc1,disc2,terbilang,grand, "
                SQL = SQL & "kode_sales,ppn, pembeda, total_point, flag_lipat, kurs, pakai_point, "
                SQL = SQL & "diskon_promo, diskon_rupiah, bayar, lokasi, total, "
                SQL = SQL & "total_u_dis_member, hasil_diskon, jenis, nilai_ppn, "
                SQL = SQL & "no_po_toko, kode_karyawan, flag_cabang_sendiri, coa_piutang, "
                SQL = SQL & "no_fak_sebelumnya, init_custm, ket_custm, no_kb, disc_cash, "
                SQL = SQL & "hasil_diskon_cash, lokasi_gdg, flag_cabang_agency, diskon_sementara, "
                SQL = SQL & "nilai_diskon_sementara, lama_diskon_sementara, hrs_updatex, metode_pot_stock, metode_budgeting, "
                SQL = SQL & "flag_opm, Flag_ACC_Plafon, User_ACC_Plafon) "
                SQL = SQL & "values ('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & Format(tgl_skrg, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(tgl_skrg, "HH:mm:ss") & "', " & kd_cust_fk & ", '" & Strings.Left(ComboBox2.Text, 1) & "', "
                SQL = SQL & "" & TJT & ", '" & UserID & "', '" & diskon1 & "', " & diskon2 & ", '" & sayTerbilang & "', "
                SQL = SQL & "" & HilangkanTanda(TxtTotal.Text) & ", 'Umum', " & TextBox18.Text & ", '" & My.Settings.Punya & "', "
                SQL = SQL & "" & ttl_point & ", " & ComboBox1.Text & ", " & kurs_point & ", "
                SQL = SQL & "'" & pakai_point & "', " & HilangkanTanda(TextBox5.Text) & ", '" & HilangkanTanda(TextBox6.Text) & "', "
                SQL = SQL & "'" & total_bayar & "', '" & ComboBox4.Text & "', "
                SQL = SQL & "'" & HilangkanTanda(TxtSub.Text) & "', '" & HilangkanTanda(TextBox16.Text) & "', "
                SQL = SQL & "'" & HilangkanTanda(TextBox1.Text) & "', '" & TextBox21.Text & "', "
                SQL = SQL & "" & HilangkanTanda(TextBox19.Text) & ", " & no_po_toko & ", "
                SQL = SQL & "'" & TextBox2.Text.Trim & "', '" & cabang_sendiri & "', "
                SQL = SQL & "'" & pakai_akun_piutang_apa & "', " & no_fak_sebelumnya & ", " & kd_cust_asl & ", "
                SQL = SQL & "" & ket_cust_asl & ", " & no_kb & ", "
                SQL = SQL & "'" & TextBox24.Text & "', '" & HilangkanTanda(TextBox25.Text) & "', "
                SQL = SQL & "'" & TextBoxGudang.Text.Trim & "', '" & flag_agency & "', "
                SQL = SQL & "'" & TextBox27.Text & "', '" & HilangkanTanda(TextBox28.Text) & "', "
                SQL = SQL & "'" & lama_diskon_sementara & "', 'Y', '" & arrMetodePotStock.Item(ComboBox4.SelectedIndex) & "', "
                SQL = SQL & "'" & arrMetodePotStock.Item(ComboBox4.SelectedIndex) & "', " & flag_opm & "," & Sudah_ACC & ", " & User_ACC & ")" 'VV
                ExecuteTrans(SQL)

                ' Dim total_hpp As Double = 0
                Dim x As Integer = 1

                '=============

                If TextBox23.Text.Trim.Length <> 0 Then
                    SQL = "select top(1) pakai from detail_permintaan_keluar a, barang b where "
                    SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and "
                    SQL = SQL & "a.kode_stock_owner = b.kode_stock_owner and a.kode_barang = b.kode_barang and "
                    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "a.no_faktur = '" & TextBox23.Text.Trim & "' "
                    If TextBox21.Text = "R" Then
                        SQL = SQL & "and b.flag_ppn = 'T' "
                    ElseIf TextBox21.Text = "C" Then
                        SQL = SQL & "and b.flag_ppn = 'Y' "
                    End If
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If General_Class.CekNULL(Dr("pakai")) = "Y" Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("KB ini sudah pernah diinput penjualan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            Else
                                Dr.Close()

                                SQL = "update x set x.pakai = 'Y' from detail_permintaan_keluar x, "
                                SQL = SQL & "barang y where "
                                SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan And "
                                SQL = SQL & "x.kode_stock_owner = y.kode_stock_owner And "
                                SQL = SQL & "x.kode_barang = y.kode_barang And "
                                SQL = SQL & "x.kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "x.no_faktur = '" & TextBox23.Text.Trim & "' "
                                If TextBox21.Text = "R" Then
                                    SQL = SQL & "and y.flag_ppn = 'T' "
                                ElseIf TextBox21.Text = "C" Then
                                    SQL = SQL & "and y.flag_ppn = 'Y' "
                                End If
                                ExecuteTrans(SQL)
                            End If
                        End If
                    End Using
                End If


                Dim total_hpp As Double = 0


                For i As Integer = 0 To listview1.Items.Count - 1
                    Get_Isi_Listview(i)

                    Dim cari As Integer = InStr(LvNm, "[")
                    Dim keterangan As String

                    If cari = 0 Then
                        keterangan = "NULL"
                    Else
                        keterangan = "'" & Strings.Mid(LvNm, cari) & "'"
                    End If

                    Dim xUsrMin As String = ""
                    If LvUserID = "NULL" Then
                        xUsrMin = "NULL"
                    Else
                        xUsrMin = "'" & LvUserID & "'"
                    End If

                    Dim xserial As String = ""
                    If LvPakaiSN = "Y" Then
                        xserial = "'" & LvSerialNumber & "'"
                    Else
                        xserial = "NULL"
                    End If

                    Dim hrg_terendah As Double = 0
                    Dim hrg_agen As Double = 0

                    ''''ganti harga agen
                    Dim Flag_Harga_Agen As String = ""
                    SQL = "select b.flag_harga_agen from barang a, Kategori_Harga_Agen_Khusus b "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and "
                    SQL = SQL & "a.Kode_Kategori_Besar=b.Kode_kategori_besar and "
                    SQL = SQL & "a.Kode_Kategori_kecil=b.Kode_Kategori_kecil and "
                    SQL = SQL & "a.Kode_Stock_Owner=b.Kode_Stock_Owner and "
                    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.Kode_Barang = '" & LvKB & "' and "
                    SQL = SQL & "a.Kode_Stock_Owner = '" & ComboBox4.Text & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Flag_Harga_Agen = Dr("Flag_Harga_Agen")
                        Else

                            Dr.Close()
                            SQL = "select Flag_Harga_Agen from Stock_Owner where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ComboBox4.Text & "'"
                            Using Dr2 = OpenTrans(SQL)
                                If Dr2.Read Then
                                    Flag_Harga_Agen = Dr2("Flag_Harga_Agen")
                                Else
                                    Dr2.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Lokasi Tidak Di temukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                    Exit Sub
                                End If
                            End Using

                        End If
                    End Using


                    Dim flag_bdgt As String = "NULL"
                    hrg_terendah = 0
                    hrg_agen = 0
                    'SQL = "select b.kode_budget, b.kode_kategori2, x_hrg_online as harga_terendah, x_hrg_mid, "
                    'SQL = SQL & "f_hrg_resell_max as harga_agen2, f_hrg_resell_special as harga_terendah2, x_hrg_agency, x_hrg_max from barang a left outer join "
                    'SQL = SQL & "promo_budgeting_bs b on a.kode_perusahaan = b.kode_perusahaan and "
                    'SQL = SQL & "a.kode_kategori2 = b.kode_kategori2 where "
                    'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "a.kode_stock_owner = '" & LvSO & "' and "
                    'SQL = SQL & "a.kode_barang = '" & LvKB & "'"
                    'Using Ds = BindingTrans(SQL)
                    '    With Ds.Tables("MyTable")
                    '        If .Rows.Count <> 0 Then
                    '            If CekNULL(.Rows(0).Item("kode_kategori2")) <> "" And cust_ini_budgeting = "Y" And arrFlagBudgetingBS.Item(ComboBox4.SelectedIndex) = "Y" And LvBudgeting = "Y" Then
                    '                '  If LvBudgeting = "Y" Then
                    '                flag_bdgt = "'Y'"

                    '                If Flag_Harga_Agen.ToUpper = "HARGA_AGEN" Then

                    '                    hrg_terendah = .Rows(0).Item("harga_terendah")
                    '                    hrg_agen = .Rows(0).Item("x_hrg_mid")

                    '                ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN2" Then

                    '                    hrg_terendah = .Rows(0).Item("harga_terendah2")
                    '                    hrg_agen = .Rows(0).Item("harga_agen2")

                    '                ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN3" Then

                    '                    hrg_terendah = .Rows(0).Item("x_hrg_agency")
                    '                    hrg_agen = .Rows(0).Item("x_hrg_mid")

                    '                ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN4" Then

                    '                    hrg_terendah = .Rows(0).Item("x_hrg_max")
                    '                    hrg_agen = .Rows(0).Item("harga_agen2")
                    '                Else
                    '                    CloseTrans()
                    '                    CloseConn()
                    '                    MessageBox.Show("Flag Agen Tidak Di Temukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    '                    Exit Sub

                    '                End If


                    '                If arrMetodePotStock.Item(ComboBox4.SelectedIndex) = "A" Then
                    '                    For hk As Integer = 0 To 2
                    '                        Dim persen_budget As Double = 0

                    '                        If hk = 0 Then
                    '                            persen_budget = 70
                    '                        ElseIf hk = 1 Then
                    '                            persen_budget = 10
                    '                        ElseIf hk = 2 Then
                    '                            persen_budget = 20
                    '                        End If

                    '                        If .Rows(0).Item("kode_budget") <> "PROMO_BS" Then
                    '                            CloseTrans()
                    '                            CloseConn()
                    '                            MessageBox.Show("Kode budgeting tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                            Exit Sub
                    '                        End If
                    '                        'ssssssssssssssssss()
                    '                        Dim nilai_terendah As Double = 0
                    '                        Dim nilai_agen As Double = 0
                    '                        If Flag_Harga_Agen.ToUpper = "HARGA_AGEN" Then

                    '                            nilai_terendah = .Rows(0).Item("harga_terendah") * Val(HilangkanTanda(LvJml))
                    '                            nilai_agen = .Rows(0).Item("x_hrg_mid") * Val(HilangkanTanda(LvJml))

                    '                        ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN2" Then

                    '                            nilai_terendah = .Rows(0).Item("harga_terendah2") * Val(HilangkanTanda(LvJml))
                    '                            nilai_agen = .Rows(0).Item("harga_agen2") * Val(HilangkanTanda(LvJml))

                    '                        ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN3" Then

                    '                            nilai_terendah = .Rows(0).Item("x_hrg_agency") * Val(HilangkanTanda(LvJml))
                    '                            nilai_agen = .Rows(0).Item("x_hrg_mid") * Val(HilangkanTanda(LvJml))

                    '                        ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN4" Then

                    '                            nilai_terendah = .Rows(0).Item("x_hrg_max") * Val(HilangkanTanda(LvJml))
                    '                            nilai_agen = .Rows(0).Item("harga_agen2") * Val(HilangkanTanda(LvJml))

                    '                        Else
                    '                            CloseTrans()
                    '                            CloseConn()
                    '                            MessageBox.Show("Flag Agen Tidak Di Temukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    '                            Exit Sub

                    '                        End If
                    '                        Dim nilai_bdgt As Double = nilai_agen - nilai_terendah
                    '                        nilai_bdgt = nilai_bdgt + (nilai_bdgt * Val(TextBox18.Text) / 100)

                    '                        If nilai_bdgt < 0 Then
                    '                            CloseTrans()
                    '                            CloseConn()
                    '                            MessageBox.Show("Nilai tidak boleh dibawah nol!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                            Exit Sub
                    '                        End If

                    '                        SQL = "insert into penjualan_budgeting(kode_perusahaan, no_faktur, "
                    '                        SQL = SQL & "kode_kategori2, nilai, kode_budget, persen, hasil, jenis) values("
                    '                        SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
                    '                        SQL = SQL & "'" & .Rows(0).Item("kode_kategori2") & "', "
                    '                        SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt, "N0")) & "', "
                    '                        SQL = SQL & "'" & .Rows(0).Item("kode_budget") & "', "
                    '                        SQL = SQL & "'" & persen_budget & "', "
                    '                        SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt * persen_budget / 100, "N0")) & "', '" & hk + 1 & "')"
                    '                        ExecuteTrans(SQL)
                    '                    Next
                    '                End If
                    '            End If
                    '        Else
                    '            CloseTrans()
                    '            CloseConn()
                    '            MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    '            Exit Sub
                    '        End If
                    '    End With
                    'End Using

                    Dim flag_bdgt_mbl As String = "NULL"
                    'SQL = "select b.kode_budget, b.kode_kategori2, x_hrg_online as harga_terendah from barang a left outer join "
                    'SQL = SQL & "promo_budgeting_mbl b on a.kode_perusahaan = b.kode_perusahaan and "
                    'SQL = SQL & "a.kode_kategori2 = b.kode_kategori2 where "
                    'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "a.kode_stock_owner = '" & ComboBox4.Text & "' and "
                    'SQL = SQL & "a.kode_barang = '" & LvKB & "'"
                    'Using Ds = BindingTrans(SQL)
                    '    With Ds.Tables("MyTable")
                    '        If .Rows.Count <> 0 Then
                    '            If CekNULL(.Rows(0).Item("kode_kategori2")) <> "" And arrFlagBudgetingMbl.Item(ComboBox4.SelectedIndex) = "Y" Then

                    '                flag_bdgt_mbl = "'Y'"
                    '                Dim persen_budget As Double = 5

                    '                If .Rows(0).Item("kode_budget") <> "PROMO_MBL" Then
                    '                    CloseTrans()
                    '                    CloseConn()
                    '                    MessageBox.Show("Kode budgeting tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                    Exit Sub
                    '                End If
                    '                'ssssssssssssssssss()

                    '                Dim nilai_bdgt As Double = Val(HilangkanTanda(LvSubttl))
                    '                nilai_bdgt = nilai_bdgt + (nilai_bdgt * Val(TextBox18.Text) / 100)

                    '                If nilai_bdgt < 0 Then
                    '                    CloseTrans()
                    '                    CloseConn()
                    '                    MessageBox.Show("Nilai tidak boleh dibawah nol!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                    Exit Sub
                    '                End If

                    '                If arrMetodePotStock.Item(ComboBox4.SelectedIndex) = "A" Or cabang_sendiri = "Y" Then
                    '                    SQL = "insert into penjualan_budgeting_mbl(kode_perusahaan, no_faktur, "
                    '                    SQL = SQL & "kode_kategori2, nilai, kode_budget, persen, hasil, jenis) values("
                    '                    SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
                    '                    SQL = SQL & "'" & .Rows(0).Item("kode_kategori2") & "', "
                    '                    SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt, "N0")) & "', "
                    '                    SQL = SQL & "'" & .Rows(0).Item("kode_budget") & "', "
                    '                    SQL = SQL & "'" & persen_budget & "', "
                    '                    SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt * persen_budget / 100, "N0")) & "', '1')"
                    '                    ExecuteTrans(SQL)
                    '                End If
                    '            End If
                    '        Else
                    '            CloseTrans()
                    '            CloseConn()
                    '            MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    '            Exit Sub
                    '        End If
                    '    End With
                    'End Using

                    'Dim flag_bdgt_10 As String = "NULL"
                    'SQL = "select b.kode_budget, b.kode_kategori2, x_hrg_online as harga_terendah from barang a left outer join "
                    'SQL = SQL & "promo_budgeting_new b on a.kode_perusahaan = b.kode_perusahaan and "
                    'SQL = SQL & "a.kode_kategori2 = b.kode_kategori2 where "
                    'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "a.kode_stock_owner = '" & LvSO & "' and "
                    'SQL = SQL & "a.kode_barang = '" & LvKB & "'"
                    'Using Ds = BindingTrans(SQL)
                    '    With Ds.Tables("MyTable")
                    '        If .Rows.Count <> 0 Then
                    '            If CekNULL(.Rows(0).Item("kode_kategori2")) <> "" Then
                    '                flag_bdgt_10 = "'Y'"
                    '                Dim persen_budget As Double = 0.3

                    '                If .Rows(0).Item("kode_budget") <> "PROMO_LIBURAN" Then
                    '                    CloseTrans()
                    '                    CloseConn()
                    '                    MessageBox.Show("Kode budgeting tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                    Exit Sub
                    '                End If
                    '                'ssssssssssssssssss()

                    '                Dim nilai_bdgt As Double = Val(HilangkanTanda(LvSubttl))
                    '                nilai_bdgt = nilai_bdgt + (nilai_bdgt * Val(TextBox18.Text) / 100)

                    '                If nilai_bdgt < 0 Then
                    '                    CloseTrans()
                    '                    CloseConn()
                    '                    MessageBox.Show("Nilai tidak boleh dibawah nol!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                    Exit Sub
                    '                End If

                    '                ' If arrMetodePotStock.Item(ComboBox4.SelectedIndex) = "A" Or cabang_sendiri = "Y" Then
                    '                SQL = "insert into penjualan_budgeting_new(kode_perusahaan, no_faktur, "
                    '                SQL = SQL & "kode_kategori2, nilai, kode_budget, persen, hasil, jenis) values("
                    '                SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
                    '                SQL = SQL & "'" & .Rows(0).Item("kode_kategori2") & "', "
                    '                SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt, "N0")) & "', "
                    '                SQL = SQL & "'" & .Rows(0).Item("kode_budget") & "', "
                    '                SQL = SQL & "'" & persen_budget & "', "
                    '                SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt * persen_budget / 100, "N0")) & "', '1')"
                    '                ExecuteTrans(SQL)
                    '                'End If
                    '            End If
                    '        Else
                    '            CloseTrans()
                    '            CloseConn()
                    '            MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    '            Exit Sub
                    '        End If
                    '    End With
                    'End Using

                    ''''ganti harga agen
                    'Dim flag_bdgt_2 As String = "NULL"
                    'SQL = "select b.kode_budget, b.kode_kategori2, x_hrg_online as harga_terendah, x_hrg_mid, "
                    'SQL = SQL & "f_hrg_resell_max as harga_agen2, f_hrg_resell_special as harga_terendah2, x_hrg_agency, x_hrg_max from barang a left outer join "
                    'SQL = SQL & "promo_budgeting_2 b on a.kode_perusahaan = b.kode_perusahaan and "
                    'SQL = SQL & "a.kode_kategori2 = b.kode_kategori2 where "
                    'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "a.kode_stock_owner = '" & ComboBox4.Text & "' and "
                    'SQL = SQL & "a.kode_barang = '" & LvKB & "'"
                    'Using Ds = BindingTrans(SQL)
                    '    With Ds.Tables("MyTable")
                    '        If .Rows.Count <> 0 Then
                    '            If CekNULL(.Rows(0).Item("kode_kategori2")) <> "" And cabang_sendiri = "T" And flag_tidak_budgeting = "" Then
                    '                flag_bdgt_2 = "'Y'"

                    '                If Flag_Harga_Agen.ToUpper = "HARGA_AGEN" Then

                    '                    hrg_terendah = .Rows(0).Item("harga_terendah")
                    '                    hrg_agen = .Rows(0).Item("x_hrg_mid")

                    '                ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN2" Then

                    '                    hrg_terendah = .Rows(0).Item("harga_terendah2")
                    '                    hrg_agen = .Rows(0).Item("harga_agen2")

                    '                ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN3" Then

                    '                    hrg_terendah = .Rows(0).Item("x_hrg_agency")
                    '                    hrg_agen = .Rows(0).Item("x_hrg_mid")

                    '                ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN4" Then

                    '                    hrg_terendah = .Rows(0).Item("x_hrg_max")
                    '                    hrg_agen = .Rows(0).Item("harga_agen2")
                    '                Else
                    '                    CloseTrans()
                    '                    CloseConn()
                    '                    MessageBox.Show("Flag Agen Tidak Di Temukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    '                    Exit Sub

                    '                End If

                    '                If arrMetodePotStock.Item(ComboBox4.SelectedIndex) = "A" Then
                    '                    For hk As Integer = 0 To 2
                    '                        Dim persen_budget As Double = 0

                    '                        If hk = 0 Then
                    '                            persen_budget = 70
                    '                        ElseIf hk = 1 Then
                    '                            persen_budget = 10
                    '                        ElseIf hk = 2 Then
                    '                            persen_budget = 20
                    '                        End If

                    '                        If .Rows(0).Item("kode_budget") <> "PRM_BS" Then
                    '                            CloseTrans()
                    '                            CloseConn()
                    '                            MessageBox.Show("Kode budgeting tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                            Exit Sub
                    '                        End If
                    '                        'ssssssssssssssssss()
                    '                        'Dim nilai_terendah As Double = .Rows(0).Item("harga_budgeting") * Val(HilangkanTanda(LvJml))
                    '                        'Dim nilai_agen As Double = .Rows(0).Item("x_hrg_mid") * Val(HilangkanTanda(LvJml))

                    '                        Dim nilai_bdgt As Double = 0


                    '                        If Flag_Harga_Agen.ToUpper = "HARGA_AGEN" Then

                    '                            nilai_bdgt = (.Rows(0).Item("x_hrg_mid") - .Rows(0).Item("harga_terendah")) * Val(HilangkanTanda(LvJml))
                    '                            nilai_bdgt = nilai_bdgt + (nilai_bdgt * Val(TextBox18.Text) / 100)

                    '                        ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN2" Then

                    '                            nilai_bdgt = (.Rows(0).Item("harga_agen2") - .Rows(0).Item("harga_terendah2")) * Val(HilangkanTanda(LvJml))
                    '                            nilai_bdgt = nilai_bdgt + (nilai_bdgt * Val(TextBox18.Text) / 100)

                    '                        ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN3" Then

                    '                            nilai_bdgt = (.Rows(0).Item("x_hrg_mid") - .Rows(0).Item("x_hrg_agency")) * Val(HilangkanTanda(LvJml))
                    '                            nilai_bdgt = nilai_bdgt + (nilai_bdgt * Val(TextBox18.Text) / 100)

                    '                        ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN4" Then

                    '                            nilai_bdgt = (.Rows(0).Item("harga_agen2") - .Rows(0).Item("x_hrg_max")) * Val(HilangkanTanda(LvJml))
                    '                            nilai_bdgt = nilai_bdgt + (nilai_bdgt * Val(TextBox18.Text) / 100)

                    '                        Else
                    '                            CloseTrans()
                    '                            CloseConn()
                    '                            MessageBox.Show("Flag Agen Tidak Di Temukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    '                            Exit Sub

                    '                        End If

                    '                        If nilai_bdgt < 0 Then
                    '                            CloseTrans()
                    '                            CloseConn()
                    '                            MessageBox.Show("Nilai tidak boleh dibawah nol!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                            Exit Sub
                    '                        End If

                    '                        SQL = "insert into penjualan_budgeting_2(kode_perusahaan, no_faktur, "
                    '                        SQL = SQL & "kode_kategori2, nilai, kode_budget, persen, hasil, jenis) values("
                    '                        SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
                    '                        SQL = SQL & "'" & .Rows(0).Item("kode_kategori2") & "', "
                    '                        SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt, "N0")) & "', "
                    '                        SQL = SQL & "'" & .Rows(0).Item("kode_budget") & "', "
                    '                        SQL = SQL & "'" & persen_budget & "', "
                    '                        SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt * persen_budget / 100, "N0")) & "', '" & hk + 1 & "')"
                    '                        ExecuteTrans(SQL)
                    '                    Next
                    '                End If
                    '            End If
                    '        Else
                    '            CloseTrans()
                    '            CloseConn()
                    '            MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    '            Exit Sub
                    '        End If
                    '    End With
                    'End Using

                    ''''ganti harga agen
                    'Dim flag_bdgt_3 As String = "NULL"
                    'SQL = "select b.kode_budget, b.kode_kategori2, x_hrg_online as harga_terendah, x_hrg_mid, "
                    'SQL = SQL & "f_hrg_resell_max as harga_agen2, f_hrg_resell_special as harga_terendah2, x_hrg_agency, x_hrg_max from barang a left outer join "
                    'SQL = SQL & "promo_budgeting_3 b on a.kode_perusahaan = b.kode_perusahaan and "
                    'SQL = SQL & "a.kode_kategori2 = b.kode_kategori2 where "
                    'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "a.kode_stock_owner = '" & ComboBox4.Text & "' and "
                    'SQL = SQL & "a.kode_barang = '" & LvKB & "'"
                    'Using Ds = BindingTrans(SQL)
                    '    With Ds.Tables("MyTable")
                    '        If .Rows.Count <> 0 Then
                    '            If CekNULL(.Rows(0).Item("kode_kategori2")) <> "" And cabang_sendiri = "T" And flag_tidak_budgeting_toto = "" Then
                    '                flag_bdgt_3 = "'Y'"

                    '                If Flag_Harga_Agen.ToUpper = "HARGA_AGEN" Then

                    '                    hrg_terendah = .Rows(0).Item("harga_terendah")
                    '                    hrg_agen = .Rows(0).Item("x_hrg_mid")

                    '                ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN2" Then

                    '                    hrg_terendah = .Rows(0).Item("harga_terendah2")
                    '                    hrg_agen = .Rows(0).Item("harga_agen2")

                    '                ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN3" Then

                    '                    hrg_terendah = .Rows(0).Item("x_hrg_agency")
                    '                    hrg_agen = .Rows(0).Item("x_hrg_mid")

                    '                ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN4" Then

                    '                    hrg_terendah = .Rows(0).Item("x_hrg_max")
                    '                    hrg_agen = .Rows(0).Item("harga_agen2")
                    '                Else
                    '                    CloseTrans()
                    '                    CloseConn()
                    '                    MessageBox.Show("Flag Agen Tidak Di Temukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    '                    Exit Sub

                    '                End If

                    '                If arrMetodePotStock.Item(ComboBox4.SelectedIndex) = "A" Then
                    '                    For hk As Integer = 0 To 2
                    '                        Dim persen_budget As Double = 0

                    '                        If hk = 0 Then
                    '                            persen_budget = 70
                    '                        ElseIf hk = 1 Then
                    '                            persen_budget = 10
                    '                        ElseIf hk = 2 Then
                    '                            persen_budget = 20
                    '                        End If

                    '                        If .Rows(0).Item("kode_budget") <> "PRM_BS" Then
                    '                            CloseTrans()
                    '                            CloseConn()
                    '                            MessageBox.Show("Kode budgeting tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                            Exit Sub
                    '                        End If
                    '                        'ssssssssssssssssss()
                    '                        'Dim nilai_terendah As Double = .Rows(0).Item("harga_budgeting") * Val(HilangkanTanda(LvJml))
                    '                        'Dim nilai_agen As Double = .Rows(0).Item("x_hrg_mid") * Val(HilangkanTanda(LvJml))

                    '                        Dim nilai_bdgt As Double = 0

                    '                        If Flag_Harga_Agen.ToUpper = "HARGA_AGEN" Then

                    '                            nilai_bdgt = (.Rows(0).Item("x_hrg_mid") - .Rows(0).Item("harga_terendah")) * Val(HilangkanTanda(LvJml))
                    '                            nilai_bdgt = nilai_bdgt + (nilai_bdgt * Val(TextBox18.Text) / 100)

                    '                        ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN2" Then

                    '                            nilai_bdgt = (.Rows(0).Item("harga_agen2") - .Rows(0).Item("harga_terendah2")) * Val(HilangkanTanda(LvJml))
                    '                            nilai_bdgt = nilai_bdgt + (nilai_bdgt * Val(TextBox18.Text) / 100)

                    '                        ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN3" Then

                    '                            nilai_bdgt = (.Rows(0).Item("x_hrg_mid") - .Rows(0).Item("x_hrg_agency")) * Val(HilangkanTanda(LvJml))
                    '                            nilai_bdgt = nilai_bdgt + (nilai_bdgt * Val(TextBox18.Text) / 100)

                    '                        ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN4" Then

                    '                            nilai_bdgt = (.Rows(0).Item("harga_agen2") - .Rows(0).Item("x_hrg_max")) * Val(HilangkanTanda(LvJml))
                    '                            nilai_bdgt = nilai_bdgt + (nilai_bdgt * Val(TextBox18.Text) / 100)
                    '                        Else
                    '                            CloseTrans()
                    '                            CloseConn()
                    '                            MessageBox.Show("Flag Agen Tidak Di Temukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    '                            Exit Sub

                    '                        End If

                    '                        If nilai_bdgt < 0 Then
                    '                            CloseTrans()
                    '                            CloseConn()
                    '                            MessageBox.Show("Nilai tidak boleh dibawah nol!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                            Exit Sub
                    '                        End If

                    '                        SQL = "insert into penjualan_budgeting_3(kode_perusahaan, no_faktur, "
                    '                        SQL = SQL & "kode_kategori2, nilai, kode_budget, persen, hasil, jenis) values("
                    '                        SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
                    '                        SQL = SQL & "'" & .Rows(0).Item("kode_kategori2") & "', "
                    '                        SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt, "N0")) & "', "
                    '                        SQL = SQL & "'" & .Rows(0).Item("kode_budget") & "', "
                    '                        SQL = SQL & "'" & persen_budget & "', "
                    '                        SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt * persen_budget / 100, "N0")) & "', '" & hk + 1 & "')"
                    '                        ExecuteTrans(SQL)
                    '                    Next
                    '                End If
                    '            End If
                    '        Else
                    '            CloseTrans()
                    '            CloseConn()
                    '            MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    '            Exit Sub
                    '        End If
                    '    End With
                    'End Using

                    Dim nm_paket As String = ""
                    '" & LvKodePaketRetail & "', " & flag_bdgt & ", "
                    'SQL = SQL & "" & flag_bdgt_mbl & ", " & flag_bdgt_2 & ","

                    If LvKodePaketRetail <> "" Then
                        nm_paket = LvKodePaketRetail
                    End If

                    If LvKodePaketRetail2 <> "" Then
                        nm_paket = LvKodePaketRetail2
                    End If


                    '   Dim nilai_bdg_bio As Double = 0



                    ''''ganti harga agen
                    'Dim flag_bdgt_4 As String = "NULL"
                    'SQL = "select b.kode_budget, b.kode_kategori2, x_hrg_online as harga_terendah, x_hrg_mid, "
                    'SQL = SQL & "f_hrg_resell_max as harga_agen2, f_hrg_resell_special as harga_terendah2, x_hrg_agency, x_hrg_max from barang a left outer join "
                    'SQL = SQL & "promo_budgeting_4 b on a.kode_perusahaan = b.kode_perusahaan and "
                    'SQL = SQL & "a.kode_kategori2 = b.kode_kategori2 where "
                    'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "a.kode_stock_owner = '" & ComboBox4.Text & "' and "
                    'SQL = SQL & "a.kode_barang = '" & LvKB & "'"
                    'Using Ds = BindingTrans(SQL)
                    '    With Ds.Tables("MyTable")
                    '        If .Rows.Count <> 0 Then
                    '            'krn ini ada hadiah, jadi kayak pouch, semua dapet
                    '            If CekNULL(.Rows(0).Item("kode_kategori2")) <> "" And flag_tidak_budgeting_bio = "" Then
                    '                flag_bdgt_4 = "'Y'"

                    '                SQL = "select nilai_budgeting from paket_retail_detail_new2 where "
                    '                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    '                SQL = SQL & "kode_paket = '" & nm_paket & "' and "
                    '                SQL = SQL & "kode_stock_owner = '" & ComboBox4.Text & "' and "
                    '                SQL = SQL & "kode_barang = '" & LvKB & "'"
                    '                Using Dr = OpenTrans(SQL)
                    '                    If Dr.Read Then
                    '                        If CekNULL(Dr("nilai_budgeting")) = "" Then
                    '                            Dr.Close()

                    '                            If Flag_Harga_Agen.ToUpper = "HARGA_AGEN" Then

                    '                                hrg_terendah = .Rows(0).Item("harga_terendah")
                    '                                hrg_agen = .Rows(0).Item("x_hrg_mid")

                    '                            ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN2" Then

                    '                                hrg_terendah = .Rows(0).Item("harga_terendah2")
                    '                                hrg_agen = .Rows(0).Item("harga_agen2")

                    '                            ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN3" Then

                    '                                hrg_terendah = .Rows(0).Item("x_hrg_agency")
                    '                                hrg_agen = .Rows(0).Item("x_hrg_mid")

                    '                            ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN4" Then

                    '                                hrg_terendah = .Rows(0).Item("x_hrg_max")
                    '                                hrg_agen = .Rows(0).Item("harga_agen2")

                    '                            Else
                    '                                CloseTrans()
                    '                                CloseConn()
                    '                                MessageBox.Show("Flag Agen Tidak Di Temukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    '                                Exit Sub

                    '                            End If
                    '                        Else
                    '                            hrg_agen = Dr("nilai_budgeting")
                    '                            hrg_terendah = 0

                    '                            Dr.Close()
                    '                        End If
                    '                    Else
                    '                        Dr.Close()

                    '                        If Flag_Harga_Agen.ToUpper = "HARGA_AGEN" Then

                    '                            hrg_terendah = .Rows(0).Item("harga_terendah")
                    '                            hrg_agen = .Rows(0).Item("x_hrg_mid")

                    '                        ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN2" Then

                    '                            hrg_terendah = .Rows(0).Item("harga_terendah2")
                    '                            hrg_agen = .Rows(0).Item("harga_agen2")

                    '                        ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN3" Then

                    '                            hrg_terendah = .Rows(0).Item("x_hrg_agency")
                    '                            hrg_agen = .Rows(0).Item("x_hrg_mid")

                    '                        ElseIf Flag_Harga_Agen.ToUpper = "HARGA_AGEN4" Then

                    '                            hrg_terendah = .Rows(0).Item("x_hrg_max")
                    '                            hrg_agen = .Rows(0).Item("harga_agen2")
                    '                        Else
                    '                            CloseTrans()
                    '                            CloseConn()
                    '                            MessageBox.Show("Flag Agen Tidak Di Temukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    '                            Exit Sub

                    '                        End If
                    '                    End If
                    '                End Using



                    '                ' For hk As Integer = 0 To 2
                    '                'Dim persen_budget As Double = 0

                    '                'If hk = 0 Then
                    '                '    persen_budget = 70
                    '                'ElseIf hk = 1 Then
                    '                '    persen_budget = 10
                    '                'ElseIf hk = 2 Then
                    '                '    persen_budget = 20
                    '                'End If

                    '                If .Rows(0).Item("kode_budget") <> "PRM_BS" Then
                    '                    CloseTrans()
                    '                    CloseConn()
                    '                    MessageBox.Show("Kode budgeting tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                    Exit Sub
                    '                End If
                    '                'ssssssssssssssssss()
                    '                'Dim nilai_terendah As Double = .Rows(0).Item("harga_budgeting") * Val(HilangkanTanda(LvJml))
                    '                'Dim nilai_agen As Double = .Rows(0).Item("x_hrg_mid") * Val(HilangkanTanda(LvJml))

                    '                Dim nilai_bdgt As Double = (hrg_agen - hrg_terendah) * Val(HilangkanTanda(LvJml))
                    '                nilai_bdgt = nilai_bdgt + (nilai_bdgt * Val(TextBox18.Text) / 100)

                    '                If nilai_bdgt < 0 Then
                    '                    CloseTrans()
                    '                    CloseConn()
                    '                    MessageBox.Show("Nilai tidak boleh dibawah nol!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                    Exit Sub
                    '                End If

                    '                If arrMetodePotStock.Item(ComboBox4.SelectedIndex) = "A" Then
                    '                    SQL = "insert into penjualan_budgeting_4(kode_perusahaan, no_faktur, "
                    '                    SQL = SQL & "kode_kategori2, nilai, kode_budget, persen, hasil, jenis) values("
                    '                    SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
                    '                    SQL = SQL & "'" & .Rows(0).Item("kode_kategori2") & "', "
                    '                    SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt, "N0")) & "', "
                    '                    SQL = SQL & "'" & .Rows(0).Item("kode_budget") & "', "
                    '                    SQL = SQL & "'100', "
                    '                    SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt, "N0")) & "', '1')"
                    '                    ExecuteTrans(SQL)
                    '                End If
                    '                ' Next
                    '            End If
                    '        Else
                    '            CloseTrans()
                    '            CloseConn()
                    '            MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    '            Exit Sub
                    '        End If
                    '    End With
                    'End Using


                    Dim last_hpp__ As Double = 0

                    SQL = "select last_hpp from barang where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                    SQL = SQL & "kode_barang = '" & LvKB & "'"
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                last_hpp__ = .Rows(0).Item("last_hpp")
                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                Exit Sub
                            End If
                        End With
                    End Using


                    If Val(HilangkanTanda(LvSubttl)) < 0 Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Ada kesalahan pada perhitungan total barang! Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        Exit Sub
                    End If

                    SQL = "insert into detail_penjualan(kode_perusahaan, no_faktur, kode_stock_owner, "
                    SQL = SQL & "kode_barang, serial_number, keterangan, jumlah, harga, "
                    SQL = SQL & "persen_diskon, nilai_diskon, x, harga_min, usermin, "
                    SQL = SQL & "pakai_sn, barang_hadiah, modal, nota_kecil, kode_marketing, subtotal, "
                    SQL = SQL & "flag_sdr, kode_paket, "
                    SQL = SQL & "Harga_Terendah, harga_agen, kode_paket_2, "
                    SQL = SQL & "mdl, metode_perhitungan, flag_budgeting_new, Id_Gudang) values ("
                    SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', '" & LvSO & "',"
                    SQL = SQL & "'" & LvKB & "', NULL, " & keterangan & ", "
                    SQL = SQL & "'" & HilangkanTanda(LvJml) & "', "
                    SQL = SQL & "'" & HilangkanTanda(LvHrg) & "', "
                    SQL = SQL & "" & HilangkanTanda(LvDiscp) & ", "
                    SQL = SQL & "" & HilangkanTanda(LvDiscrp) & ", " & x & ", "
                    SQL = SQL & "" & HilangkanTanda(LvHrgMin) & ", "
                    SQL = SQL & "" & xUsrMin & ", '" & LvPakaiSN & "', '" & LvHadiah & "', '" & HilangkanTanda(LvModal) & "', "
                    SQL = SQL & "'" & LvNotaKecil & "', '" & LvKdSales & "', '" & HilangkanTanda(LvSubttl) & "', "
                    SQL = SQL & "'" & LvFlagSendiri & "', '" & LvKodePaketRetail & "', "
                    SQL = SQL & "" & hrg_terendah & ", "
                    SQL = SQL & "" & hrg_agen & ", '" & LvKodePaketRetail2 & "', "
                    SQL = SQL & "'" & last_hpp__ & "', "
                    SQL = SQL & "'" & arrMetodePerhitungan.Item(ComboBox4.SelectedIndex) & "', 'Y', '" & LvIdGudang & "')"
                    ExecuteTrans(SQL)

                    Dim x_no_urut_det_penj As Integer = 0
                    SQL = "select IDENT_CURRENT('detail_penjualan') as urutan"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            x_no_urut_det_penj = Dr("urutan")
                        End If
                    End Using

                    SQL = "select no_urut from detail_penjualan where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_faktur = '" & TxtFaktur.Text.Trim & "' and no_urut = '" & x_no_urut_det_penj & "'"
                    Using Dr = OpenTrans(SQL)
                        If Not (Dr.Read) Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Harap ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using


                    Dim flag_ppn As String = ""
                    If TextBox21.Text = "R" Then
                        flag_ppn = "T"
                    ElseIf TextBox21.Text = "C" Then
                        flag_ppn = "Y"
                    End If

                    'RGRG

                    If arrMetodePotStock.Item(ComboBox4.SelectedIndex) = "A" And cabang_sendiri = "T" Then
                        SQL = "select good_stock, flag_ppn from barang where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                        SQL = SQL & "kode_barang = '" & LvKB & "'"
                        Using Ds = BindingTrans(SQL)
                            With Ds.Tables("MyTable")
                                If .Rows.Count <> 0 Then

                                    If .Rows(0).Item("good_stock") - HilangkanTanda(LvJml) < BolehNegatif Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Proses membuat stock menjadi negatif untuk barang " & LvNm & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    ElseIf .Rows(0).Item("flag_ppn") <> flag_ppn Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("" & LvNm & " bukan barang flag PPN = " & flag_ppn & " Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    Else
                                        Dim untuk_jml_blm_krm As Double = 0
                                        If cabang_sendiri = "Y" Then
                                            untuk_jml_blm_krm = 0
                                        Else
                                            untuk_jml_blm_krm = HilangkanTanda(LvJml)
                                        End If

                                        SQL = "Update barang set good_stock = good_stock - " & HilangkanTanda(LvJml) & " where "
                                        'SQL = SQL & ",stock_blm_kirim = stock_blm_kirim + " & untuk_jml_blm_krm & " where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                                        SQL = SQL & "kode_barang = '" & LvKB & "'"
                                        ExecuteTrans(SQL)
                                    End If
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                    Exit Sub
                                End If
                            End With
                        End Using
                    End If


                    If LvHadiah = "Y" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan karena " & LvNm & " merupakan barang promo & menggunakan serial number!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    'RGRG
                    If arrMetodePotStock.Item(ComboBox4.SelectedIndex) = "A" And cabang_sendiri = "T" Then
                        Dim lewatin As String = "T"
                        SQL = "select isnull(sum(jumlah), 0) as stock from barang_sn where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                        SQL = SQL & "kode_barang = '" & LvKB & "' and jumlah <> 0 "
                        'SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                If Dr("stock") < Val(HilangkanTanda(LvJml)) Then
                                    lewatin = "Y"
                                Else
                                    lewatin = "T"
                                End If
                            End If
                        End Using

                        If lewatin = "T" Then

                            Dim sisa As Double = 0

                            SQL = "select kode_stock_owner, kode_barang, serial_number, jumlah from barang_sn where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                            SQL = SQL & "kode_barang = '" & LvKB & "' and jumlah <> 0 "
                            SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
                            Using Ds = BindingTrans(SQL)
                                With Ds.Tables("MyTable")
                                    If .Rows.Count <> 0 Then
                                        sisa = HilangkanTanda(LvJml)

                                        For h As Integer = 0 To .Rows.Count - 1
                                            If sisa = 0 Then
                                                Exit For
                                            ElseIf sisa < 0 Then
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If

                                            If sisa < .Rows(h).Item("jumlah") Or sisa = .Rows(h).Item("jumlah") Then
                                                SQL = "Update barang_sn set jumlah = jumlah - " & sisa & " where "
                                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                                                SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                                                SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                                                ExecuteTrans(SQL)

                                                Dim untuk_jml_do As Double = 0
                                                If cabang_sendiri = "Y" Then
                                                    untuk_jml_do = 0
                                                Else
                                                    untuk_jml_do = sisa
                                                End If

                                                SQL = "insert into det_penj(kode_perusahaan, no_faktur, "
                                                SQL = SQL & "kode_stock_owner, kode_barang, serial_number, no_urut, "
                                                SQL = SQL & "jumlah, jumlah_do) values('" & KodePerusahaan & "', "
                                                SQL = SQL & "'" & TxtFaktur.Text.Trim & "', "
                                                SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
                                                SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
                                                SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
                                                SQL = SQL & "" & x_no_urut_det_penj & ", '" & sisa & "', '" & untuk_jml_do & "')"
                                                ExecuteTrans(SQL)

                                                total_hpp = total_hpp + (sisa * Get_Harga_SN(.Rows(h).Item("serial_number")))

                                                sisa = 0
                                            ElseIf sisa > .Rows(h).Item("jumlah") Then
                                                Dim untuk_jml_do As Double = 0
                                                If cabang_sendiri = "Y" Then
                                                    untuk_jml_do = 0
                                                Else
                                                    untuk_jml_do = .Rows(h).Item("jumlah")
                                                End If

                                                SQL = "insert into det_penj(kode_perusahaan, no_faktur, "
                                                SQL = SQL & "kode_stock_owner, kode_barang, serial_number, no_urut, "
                                                SQL = SQL & "jumlah, jumlah_do) values('" & KodePerusahaan & "', "
                                                SQL = SQL & "'" & TxtFaktur.Text.Trim & "', "
                                                SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
                                                SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
                                                SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
                                                SQL = SQL & "" & x_no_urut_det_penj & ", "
                                                SQL = SQL & "'" & .Rows(h).Item("jumlah") & "', '" & untuk_jml_do & "')"
                                                ExecuteTrans(SQL)

                                                SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
                                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                                                SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                                                SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                                                ExecuteTrans(SQL)

                                                total_hpp = total_hpp + (.Rows(h).Item("jumlah") * Get_Harga_SN(.Rows(h).Item("serial_number")))

                                                sisa = sisa - .Rows(h).Item("jumlah")
                                            Else
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Barang SN terjadi kesalahan untuk barang " & LvNm & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If

                                            If sisa <> 0 And h = .Rows.Count - 1 Then
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Jumlah stock tidak mencukupi untuk barang " & LvNm & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If

                                            If Val(HilangkanTanda(LvSubttl)) <> 0 Then
                                                If Val(Get_Harga_SN(.Rows(h).Item("serial_number"))) + (Val(Get_Harga_SN(.Rows(h).Item("serial_number"))) * 5 / 100) > Val(HilangkanTanda(Format(Val(HilangkanTanda(LvSubttl)) / Val(HilangkanTanda(LvJml)), "N0"))) Then
                                                    If boleh_jual_rugi = "T" Then
                                                        CloseTrans()
                                                        CloseConn()
                                                        MessageBox.Show("Barang " & LvNm & " harus diinput pusat!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End If
                                        Next
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("SN untuk barang " & LvNm & " tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End With
                            End Using

                        Else

                            SQL = "insert into det_penj(kode_perusahaan, no_faktur, "
                            SQL = SQL & "kode_stock_owner, kode_barang, serial_number, no_urut, "
                            SQL = SQL & "jumlah, jumlah_do) values('" & KodePerusahaan & "', "
                            SQL = SQL & "'" & TxtFaktur.Text.Trim & "', "
                            SQL = SQL & "'" & LvSO & "', "
                            SQL = SQL & "'" & LvKB & "', "
                            SQL = SQL & "'-', "
                            SQL = SQL & "" & x_no_urut_det_penj & ", '0', '0')"
                            ExecuteTrans(SQL)
                        End If
                    End If


                    If i = x * JmlBrg - 1 Then
                        x = x + 1
                    End If

                Next


                If TextBox23.Text.Trim.Length <> 0 Then
                    SQL = "select count(kode_perusahaan) as total from detail_permintaan_keluar where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_faktur = '" & TextBox23.Text.Trim & "' and "
                    SQL = SQL & "pakai is null"
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            If dr("total") = 0 Then
                                dr.Close()

                                SQL = "update permintaan_keluar set semua = 'Y' where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "no_faktur = '" & TextBox23.Text.Trim & "'"
                                ExecuteTrans(SQL)
                            End If
                        End If
                    End Using
                End If


                SQL = "select kode_perusahaan from customers where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and kode_customer = '" & TextBox10.Text.Trim & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()

                        SQL = "update customers set point = point + " & HilangkanTanda(Label21.Text) & " "
                        If ComboBox2.SelectedIndex = 1 Then ' Non Tunai maka piutang bertambah
                            SQL = SQL & ", piutang = piutang + " & HilangkanTanda(TxtTotal.Text) & " "
                        End If
                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_customer = '" & TextBox10.Text.Trim & "'"
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Customer tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                '=============================================

                Dim coa_persediaan_Brg_Blm_Krm As String = ""
                Dim coa_Brg_Blm_Krm As String = ""

                Dim coa_persediaan_ As String = ""
                Dim coa_persediaan_sementara_ As String = "" 'VV
                Dim coa_persediaan_sementara_agency_ As String = "" 'VV

                SQL = "select top(1) persediaan_Brg_Blm_Krm, Brg_Blm_Krm, persediaan, "
                SQL = SQL & "persediaan_sementara, persediaan_sementara_agency from Stock_Owner_Gudang where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_Stock_owner = '" & TextBoxGudang.Text.Trim & "'"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        coa_persediaan_ = dr("persediaan")
                        coa_persediaan_sementara_ = dr("persediaan_sementara")
                        coa_persediaan_sementara_agency_ = dr("persediaan_sementara_agency")

                        coa_persediaan_Brg_Blm_Krm = dr("persediaan_Brg_Blm_Krm")
                        coa_Brg_Blm_Krm = dr("Brg_Blm_Krm")
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data lokasi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                'Dim coa_biaya_insentif_1 As String = ""
                'Dim coa_biaya_insentif_2 As String = ""
                'Dim coa_hutang_insentif_1 As String = ""
                'Dim coa_hutang_insentif_2 As String = ""
                'Dim akun_kas As String = ""
                'Dim akun_kas2 As String = ""

                'SQL = "select top(1) kas, biaya_insentif_1, biaya_insentif_2, hutang_insentif_1, hutang_insentif_2 from stock_owner where "
                'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "kode_Stock_owner = '" & ComboBox4.Text & "'"
                'Using dr = OpenTrans(SQL)
                '    If dr.Read Then
                '        coa_biaya_insentif_1 = dr("biaya_insentif_1")
                '        coa_biaya_insentif_2 = dr("biaya_insentif_2")
                '        coa_hutang_insentif_1 = dr("hutang_insentif_1")
                '        coa_hutang_insentif_2 = dr("hutang_insentif_2")
                '        akun_kas = dr("kas")
                '    Else
                '        dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("Data lokasi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using

                Dim persen_insentif_1 As Double = 0
                Dim persen_insentif_2 As Double = 0
                Dim kepala As String = ""

                Dim nilai_insentif_1 As Double = 0
                Dim nilai_insentif_2 As Double = 0

                'SQL = "select insentif_1, insentif_2, kepala from karyawan where kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "kode_karyawan = '" & TextBox2.Text.Trim & "'"
                'Using Dr = OpenTrans(SQL)
                '    If Dr.Read Then
                '        persen_insentif_1 = Dr("insentif_1")
                '        persen_insentif_2 = Dr("insentif_2")
                '        kepala = CekNULL(Dr("kepala"))
                '    Else
                '        Dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("Data karyawan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using

                'nilai_insentif_1 = Val(HilangkanTanda(TxtTotal.Text)) * persen_insentif_1 / 100
                'nilai_insentif_2 = Val(HilangkanTanda(TxtTotal.Text)) * persen_insentif_2 / 100
                'nilai_insentif_1 = HilangkanTanda(Format(nilai_insentif_1, "N0"))
                'nilai_insentif_2 = HilangkanTanda(Format(nilai_insentif_2, "N0"))



                'Dim pagenumber As Integer = 1

                ''iniiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii
                'Dim Kode_Voucher As String = ""
                'Dim Kode_Voucher2 As String = ""
                'Dim Kode_Voucher2x As String = ""

                'Dim __Kode_Voucher As String = "NULL"
                'Dim __Kode_Voucher2 As String = "NULL"
                'Dim __Kode_Voucher2x As String = "NULL"

                'If arrMetodePotStock.Item(ComboBox4.SelectedIndex) = "A" And cabang_sendiri = "T" Then
                '    Kode_Voucher = GetLastNumberJurnal(Format(tgl_skrg, "yyyyMM"), fJurnalJual & TextBox21.Text & arrInisialFaktur(ComboBox4.SelectedIndex), KodePerusahaan)
                '    __Kode_Voucher = "'" & Kode_Voucher & "'"

                '    Dim akun_piutang As String = ""
                '    Dim akun_piutang_sementara As String = "" 'VV INI SEMUA PAKAI DI PENJ JWA

                '    SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                '    SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                '    SQL = SQL & "'" & Kode_Voucher & "', "
                '    SQL = SQL & "'" & Format(tgl_skrg, "yyyy-MM-dd") & "', "
                '    SQL = SQL & "'" & Format(tgl_skrg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                '    SQL = SQL & "'" & KodeProyek & "', 'Penjualan " & TxtFaktur.Text & " ; " & TextBox11.Text.Trim & "', '', "
                '    SQL = SQL & "'-', '" & UserID & "', '" & ComboBox4.Text & "')"
                '    ExecuteTrans(SQL)

                '    If ComboBox2.SelectedIndex = 0 Then 'tunai
                '        SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(akun_kas, 1),
                '                       Strings.Mid(akun_kas, 2, 1),
                '                       Strings.Mid(Ganti(akun_kas), 3),
                '                       KodePerusahaan, KodeProyek, "Penjualan " & TxtFaktur.Text & " ; " & TextBox11.Text.Trim, HilangkanTanda(Label12.Text), "0", pagenumber, ComboBox4.Text)
                '        ExecuteTrans(SQL)
                '        pagenumber = pagenumber + 1

                '        akun_kas2 = "'" & akun_kas & "'"
                '        akun_piutang = "NULL"
                '        akun_piutang_sementara = "NULL"
                '    Else 'kalo kredit

                '        SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(X_Piutang, 1),
                '                      Strings.Mid(X_Piutang, 2, 1),
                '                      Strings.Mid(Ganti(X_Piutang), 3),
                '                      KodePerusahaan, KodeProyek, "Penjualan " & TxtFaktur.Text & " ; " & TextBox11.Text.Trim, HilangkanTanda(Label12.Text), "0", pagenumber, ComboBox4.Text)
                '        ExecuteTrans(SQL)
                '        pagenumber = pagenumber + 1

                '        akun_kas2 = "NULL"
                '        akun_piutang = "'" & X_Piutang & "'"
                '        akun_piutang_sementara = "NULL"
                '    End If

                '    If flag_audit = "Y" Then
                '        SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(X_Penjualan_Lainnya, 1),
                '                Strings.Mid(X_Penjualan_Lainnya, 2, 1),
                '                Strings.Mid(Ganti(X_Penjualan_Lainnya), 3),
                '                KodePerusahaan, KodeProyek, "Penjualan " & TxtFaktur.Text & " ; " & TextBox11.Text.Trim, "0", HilangkanTanda(TextBox17.Text), pagenumber, ComboBox4.Text)
                '        ExecuteTrans(SQL)
                '        pagenumber = pagenumber + 1
                '    Else
                '        SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(X_Penjualan, 1),
                '                Strings.Mid(X_Penjualan, 2, 1),
                '                Strings.Mid(Ganti(X_Penjualan), 3),
                '                KodePerusahaan, KodeProyek, "Penjualan " & TxtFaktur.Text & " ; " & TextBox11.Text.Trim, "0", HilangkanTanda(TextBox17.Text), pagenumber, ComboBox4.Text)
                '        ExecuteTrans(SQL)
                '        pagenumber = pagenumber + 1
                '    End If

                '    If Val(HilangkanTanda(TextBox19.Text)) <> 0 Then
                '        SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(X_PPN_Penjualan, 1),
                '                    Strings.Mid(X_PPN_Penjualan, 2, 1),
                '                    Strings.Mid(Ganti(X_PPN_Penjualan), 3),
                '                    KodePerusahaan, KodeProyek, "PPN Penjualan " & TxtFaktur.Text & " ; " & TextBox11.Text.Trim, "0", HilangkanTanda(TextBox19.Text), pagenumber, ComboBox4.Text)
                '        ExecuteTrans(SQL)
                '        pagenumber = pagenumber + 1
                '    End If

                '    Kode_Voucher2 = GetLastNumberJurnal(Format(tgl_skrg, "yyyyMM"), fJurnalJual & TextBox21.Text & arrInisialFaktur(ComboBox4.SelectedIndex), KodePerusahaan)
                '    __Kode_Voucher2 = "'" & Kode_Voucher2 & "'"

                '    pagenumber = 1

                '    SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                '    SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                '    SQL = SQL & "'" & Kode_Voucher2 & "', "
                '    SQL = SQL & "'" & Format(tgl_skrg, "yyyy-MM-dd") & "', "
                '    SQL = SQL & "'" & Format(tgl_skrg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                '    SQL = SQL & "'" & KodeProyek & "', 'Penjualan " & TxtFaktur.Text.Trim & " ; " & TextBox11.Text.Trim & "', '', "
                '    SQL = SQL & "'-', '" & UserID & "', '" & ComboBox4.Text & "')"
                '    ExecuteTrans(SQL)


                '    SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(X_HPP, 1),
                '                      Strings.Mid(X_HPP, 2, 1),
                '                      Strings.Mid(Ganti(X_HPP), 3),
                '                      KodePerusahaan, KodeProyek, "HPP Penjualan " & TxtFaktur.Text.Trim & " ; " & TextBox11.Text.Trim, total_hpp, "0", pagenumber, ComboBox4.Text)
                '    ExecuteTrans(SQL)
                '    pagenumber = pagenumber + 1
                '    'vv
                '    SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_persediaan_, 1),
                '                      Strings.Mid(coa_persediaan_, 2, 1),
                '                      Strings.Mid(Ganti(coa_persediaan_), 3),
                '                      KodePerusahaan, KodeProyek, "HPP Penjualan " & TxtFaktur.Text.Trim & " ; " & TextBox11.Text.Trim, "0", total_hpp, pagenumber, TextBoxGudang.Text.Trim)
                '    ExecuteTrans(SQL)
                '    pagenumber = pagenumber + 1


                '    Kode_Voucher2x = GetLastNumberJurnal(Format(tgl_skrg, "yyyyMM"), fJurnalJual & TextBox21.Text & arrInisialFaktur(ComboBox4.SelectedIndex), KodePerusahaan)
                '    __Kode_Voucher2x = "'" & Kode_Voucher2x & "'"

                '    pagenumber = 1

                '    SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                '    SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                '    SQL = SQL & "'" & Kode_Voucher2x & "', "
                '    SQL = SQL & "'" & Format(tgl_skrg, "yyyy-MM-dd") & "', "
                '    SQL = SQL & "'" & Format(tgl_skrg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                '    SQL = SQL & "'" & KodeProyek & "', 'Penjualan " & TxtFaktur.Text.Trim & " ; " & TextBox11.Text.Trim & "', '', "
                '    SQL = SQL & "'-', '" & UserID & "', '" & ComboBox4.Text & "')"
                '    ExecuteTrans(SQL)


                '    SQL = Get_Detail_Jurnal(Kode_Voucher2x, Strings.Left(coa_persediaan_Brg_Blm_Krm, 1),
                '                      Strings.Mid(coa_persediaan_Brg_Blm_Krm, 2, 1),
                '                      Strings.Mid(Ganti(coa_persediaan_Brg_Blm_Krm), 3),
                '                      KodePerusahaan, KodeProyek, "Penjualan " & TxtFaktur.Text.Trim & " ; " & TextBox11.Text.Trim, total_hpp, "0", pagenumber, TextBoxGudang.Text.Trim)
                '    ExecuteTrans(SQL)
                '    pagenumber = pagenumber + 1
                '    'vv
                '    SQL = Get_Detail_Jurnal(Kode_Voucher2x, Strings.Left(coa_Brg_Blm_Krm, 1),
                '                      Strings.Mid(coa_Brg_Blm_Krm, 2, 1),
                '                      Strings.Mid(Ganti(coa_Brg_Blm_Krm), 3),
                '                      KodePerusahaan, KodeProyek, "Penjualan " & TxtFaktur.Text.Trim & " ; " & TextBox11.Text.Trim, "0", total_hpp, pagenumber, TextBoxGudang.Text.Trim)
                '    ExecuteTrans(SQL)
                '    pagenumber = pagenumber + 1

                '    SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '    SQL = SQL & "kode_voucher = '" & Kode_Voucher2x & "'"
                '    Using Dr = OpenTrans(SQL)
                '        If Dr.Read Then
                '            If Dr("debit") <> Dr("kredit") Then
                '                Dr.Close()
                '                CloseTrans()
                '                CloseConn()
                '                MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                Exit Sub
                '            End If
                '        Else
                '            Dr.Close()
                '            CloseTrans()
                '            CloseConn()
                '            MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            Exit Sub
                '        End If
                '    End Using

                '    SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "'"
                '    Using Dr = OpenTrans(SQL)
                '        If Dr.Read Then
                '            If Dr("debit") <> Dr("kredit") Then
                '                Dr.Close()
                '                CloseTrans()
                '                CloseConn()
                '                MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                Exit Sub
                '            End If
                '        Else
                '            Dr.Close()
                '            CloseTrans()
                '            CloseConn()
                '            MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            Exit Sub
                '        End If
                '    End Using

                '    SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '    SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "'"
                '    Using Dr = OpenTrans(SQL)
                '        If Dr.Read Then
                '            If Dr("debit") <> Dr("kredit") Then
                '                Dr.Close()
                '                CloseTrans()
                '                CloseConn()
                '                MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                Exit Sub
                '            End If
                '        Else
                '            Dr.Close()
                '            CloseTrans()
                '            CloseConn()
                '            MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            Exit Sub
                '        End If
                '    End Using

                '    ''=============================================

                '    SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '    SQL = SQL & "kode_voucher = '" & Kode_Voucher & "'"
                '    Using Dr = OpenTrans(SQL)
                '        If Dr.Read Then
                '            If Dr("debit") <> Dr("kredit") Then
                '                Dr.Close()
                '                CloseTrans()
                '                CloseConn()
                '                MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                Exit Sub
                '            End If
                '        Else
                '            Dr.Close()
                '            CloseTrans()
                '            CloseConn()
                '            MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            Exit Sub
                '        End If
                '    End Using

                '    SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '    SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "'"
                '    Using Dr = OpenTrans(SQL)
                '        If Dr.Read Then
                '            If Dr("debit") <> Dr("kredit") Then
                '                Dr.Close()
                '                CloseTrans()
                '                CloseConn()
                '                MessageBox.Show("Jurnal 2 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                Exit Sub
                '            End If
                '        Else
                '            Dr.Close()
                '            CloseTrans()
                '            CloseConn()
                '            MessageBox.Show("Data jurnal 2 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            Exit Sub
                '        End If
                '    End Using

                'End If
                ''iniiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii

                'Dim Kode_Voucher3 As String = ""
                'Dim Kode_Voucher4 As String = ""
                'Dim __Kode_Voucher3 As String = "NULL"
                'Dim __Kode_Voucher4 As String = "NULL"

                'Get_Isi_Listview(0)
                'Dim ket_di_jurnal As String = "Penj. " & TextBox2.Text.Trim & ";" & TxtFaktur.Text.Trim & ";" & TextBox11.Text.Trim & ";" & HilangkanTanda(LvJml) & "_" & LvNm
                'Dim ket_di_jurnal2 As String = "Penj. " & kepala & ";" & TxtFaktur.Text.Trim & ";" & TextBox11.Text.Trim & ";" & HilangkanTanda(LvJml) & "_" & LvNm

                'If cabang_sendiri = "T" Then 'rsl
                '    If nilai_insentif_1 <> 0 And Len(coa_biaya_insentif_1) <> 1 Then
                '        Kode_Voucher3 = GetLastNumberJurnal(Format(tgl_skrg, "yyyyMM"), fJurnalJual & TextBox21.Text & arrInisialFaktur(ComboBox4.SelectedIndex), KodePerusahaan)
                '        __Kode_Voucher3 = "'" & Kode_Voucher3 & "'"

                '        pagenumber = 1

                '        SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                '        SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                '        SQL = SQL & "'" & Kode_Voucher3 & "', "
                '        SQL = SQL & "'" & Format(tgl_skrg, "yyyy-MM-dd") & "', "
                '        SQL = SQL & "'" & Format(tgl_skrg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                '        SQL = SQL & "'" & KodeProyek & "', '" & Strings.Left(ket_di_jurnal, 80) & "', '', "
                '        SQL = SQL & "'-', '" & UserID & "', '" & ComboBox4.Text & "')"
                '        ExecuteTrans(SQL)

                '        SQL = Get_Detail_Jurnal(Kode_Voucher3, Strings.Left(coa_biaya_insentif_1, 1),
                '                          Strings.Mid(coa_biaya_insentif_1, 2, 1),
                '                          Strings.Mid(Ganti(coa_biaya_insentif_1), 3),
                '                          KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal, 80), nilai_insentif_1, "0", pagenumber, ComboBox4.Text)
                '        ExecuteTrans(SQL)
                '        pagenumber = pagenumber + 1

                '        SQL = Get_Detail_Jurnal(Kode_Voucher3, Strings.Left(coa_hutang_insentif_1, 1),
                '                          Strings.Mid(coa_hutang_insentif_1, 2, 1),
                '                          Strings.Mid(Ganti(coa_hutang_insentif_1), 3),
                '                          KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal, 80), "0", nilai_insentif_1, pagenumber, ComboBox4.Text)
                '        ExecuteTrans(SQL)
                '        pagenumber = pagenumber + 1

                '        SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                '        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '        SQL = SQL & "kode_voucher = '" & Kode_Voucher3 & "'"
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
                '    Else
                '        nilai_insentif_1 = 0
                '    End If

                '    If nilai_insentif_2 <> 0 And Len(coa_biaya_insentif_2) <> 1 And kepala <> "" Then
                '        Kode_Voucher4 = GetLastNumberJurnal(Format(tgl_skrg, "yyyyMM"), fJurnalJual & TextBox21.Text & arrInisialFaktur(ComboBox4.SelectedIndex), KodePerusahaan)
                '        __Kode_Voucher4 = "'" & Kode_Voucher4 & "'"

                '        pagenumber = 1

                '        SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                '        SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                '        SQL = SQL & "'" & Kode_Voucher4 & "', "
                '        SQL = SQL & "'" & Format(tgl_skrg, "yyyy-MM-dd") & "', "
                '        SQL = SQL & "'" & Format(tgl_skrg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                '        SQL = SQL & "'" & KodeProyek & "', '" & Strings.Left(ket_di_jurnal, 80) & "', '', "
                '        SQL = SQL & "'-', '" & UserID & "', '" & ComboBox4.Text & "')"
                '        ExecuteTrans(SQL)

                '        SQL = Get_Detail_Jurnal(Kode_Voucher4, Strings.Left(coa_biaya_insentif_2, 1),
                '                          Strings.Mid(coa_biaya_insentif_2, 2, 1),
                '                          Strings.Mid(Ganti(coa_biaya_insentif_2), 3),
                '                          KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal, 80), nilai_insentif_2, "0", pagenumber, ComboBox4.Text)
                '        ExecuteTrans(SQL)
                '        pagenumber = pagenumber + 1

                '        SQL = Get_Detail_Jurnal(Kode_Voucher4, Strings.Left(coa_hutang_insentif_2, 1),
                '                          Strings.Mid(coa_hutang_insentif_2, 2, 1),
                '                          Strings.Mid(Ganti(coa_hutang_insentif_2), 3),
                '                          KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal, 80), "0", nilai_insentif_2, pagenumber, ComboBox4.Text)
                '        ExecuteTrans(SQL)
                '        pagenumber = pagenumber + 1

                '        SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                '        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '        SQL = SQL & "kode_voucher = '" & Kode_Voucher4 & "'"
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
                '    Else
                '        nilai_insentif_2 = 0
                '    End If
                'End If


                ''=============================================
                'Dim Kode_Voucher5 As String = ""
                'Dim __Kode_Voucher5 As String = "NULL"
                'Dim ket_di_jurnal_promo As String = "Penj. " & TxtFaktur.Text.Trim & ";" & TextBox11.Text.Trim & ";" & HilangkanTanda(LvJml) & "_" & LvNm

                'SQL = "select jenis, sum(hasil) as hasil from penjualan_budgeting where "
                'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "no_faktur = '" & TxtFaktur.Text.Trim & "' group by jenis order by jenis"
                'Using Ds = BindingTrans(SQL)
                '    With Ds.Tables("MyTable")
                '        If .Rows.Count <> 0 Then
                '            Kode_Voucher5 = GetLastNumberJurnal(Format(tgl_skrg, "yyyyMM"), fJurnalJual & TextBox21.Text & arrInisialFaktur(ComboBox4.SelectedIndex), KodePerusahaan)
                '            __Kode_Voucher5 = "'" & Kode_Voucher5 & "'"

                '            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                '            SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                '            SQL = SQL & "'" & Kode_Voucher5 & "', "
                '            SQL = SQL & "'" & Format(tgl_skrg, "yyyy-MM-dd") & "', "
                '            SQL = SQL & "'" & Format(tgl_skrg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                '            SQL = SQL & "'" & KodeProyek & "', '" & Strings.Left(ket_di_jurnal_promo, 80) & "', '', "
                '            SQL = SQL & "'-', '" & UserID & "', '" & ComboBox4.Text & "')"
                '            ExecuteTrans(SQL)

                '            pagenumber = 1
                '            Dim nilai_debit As Double = 0

                '            For b As Integer = 0 To .Rows.Count - 1
                '                Dim akunhutpromo As String = ""

                '                If b = 0 Then
                '                    akunhutpromo = arrAkunBiayaHut1.Item(ComboBox4.SelectedIndex)
                '                ElseIf b = 1 Then
                '                    akunhutpromo = arrAkunBiayaHut2.Item(ComboBox4.SelectedIndex)
                '                ElseIf b = 2 Then
                '                    akunhutpromo = arrAkunBiayaHut3.Item(ComboBox4.SelectedIndex)
                '                Else
                '                    akunhutpromo = "x"
                '                End If

                '                SQL = Get_Detail_Jurnal(Kode_Voucher5, Strings.Left(akunhutpromo, 1),
                '                                  Strings.Mid(akunhutpromo, 2, 1),
                '                                  Strings.Mid(Ganti(akunhutpromo), 3),
                '                                  KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo, 80), "0", .Rows(b).Item("hasil"), pagenumber, ComboBox4.Text)
                '                ExecuteTrans(SQL)
                '                pagenumber = pagenumber + 1

                '                nilai_debit = nilai_debit + .Rows(b).Item("hasil")
                '            Next

                '            SQL = Get_Detail_Jurnal(Kode_Voucher5, Strings.Left(arrAkunBiayaPromo.Item(ComboBox4.SelectedIndex), 1),
                '                                 Strings.Mid(arrAkunBiayaPromo.Item(ComboBox4.SelectedIndex), 2, 1),
                '                                 Strings.Mid(Ganti(arrAkunBiayaPromo.Item(ComboBox4.SelectedIndex)), 3),
                '                                 KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo, 80), nilai_debit, "0", pagenumber, ComboBox4.Text)
                '            ExecuteTrans(SQL)
                '            pagenumber = pagenumber + 1

                '            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                '            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "kode_voucher = '" & Kode_Voucher5 & "'"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    If Dr("debit") <> Dr("kredit") Then
                '                        Dr.Close()
                '                        CloseTrans()
                '                        CloseConn()
                '                        MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                        Exit Sub
                '                    End If
                '                Else
                '                    Dr.Close()
                '                    CloseTrans()
                '                    CloseConn()
                '                    MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                    Exit Sub
                '                End If
                '            End Using
                '        End If
                '    End With
                'End Using


                ''=============================================

                ''=============================================

                'Dim Kode_Voucher6 As String = ""
                'Dim __Kode_Voucher6 As String = "NULL"
                'Dim ket_di_jurnal_promo_mbl As String = "Penj. " & TxtFaktur.Text.Trim & ";" & TextBox11.Text.Trim & ";" & HilangkanTanda(LvJml) & "_" & LvNm


                'SQL = "select jenis, sum(hasil) as hasil from penjualan_budgeting_mbl where "
                'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "no_faktur = '" & TxtFaktur.Text.Trim & "' group by jenis order by jenis"
                'Using Ds = BindingTrans(SQL)
                '    With Ds.Tables("MyTable")
                '        If .Rows.Count <> 0 Then
                '            Kode_Voucher6 = GetLastNumberJurnal(Format(tgl_skrg, "yyyyMM"), fJurnalJual & TextBox21.Text & arrInisialFaktur(ComboBox4.SelectedIndex), KodePerusahaan)
                '            __Kode_Voucher6 = "'" & Kode_Voucher6 & "'"

                '            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                '            SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                '            SQL = SQL & "'" & Kode_Voucher6 & "', "
                '            SQL = SQL & "'" & Format(tgl_skrg, "yyyy-MM-dd") & "', "
                '            SQL = SQL & "'" & Format(tgl_skrg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                '            SQL = SQL & "'" & KodeProyek & "', '" & Strings.Left(ket_di_jurnal_promo_mbl, 80) & "', '', "
                '            SQL = SQL & "'-', '" & UserID & "', '" & ComboBox4.Text & "')"
                '            ExecuteTrans(SQL)

                '            pagenumber = 1

                '            SQL = Get_Detail_Jurnal(Kode_Voucher6, Strings.Left(arrAkunBiayaMbl.Item(ComboBox4.SelectedIndex), 1),
                '                                 Strings.Mid(arrAkunBiayaMbl.Item(ComboBox4.SelectedIndex), 2, 1),
                '                                 Strings.Mid(Ganti(arrAkunBiayaMbl.Item(ComboBox4.SelectedIndex)), 3),
                '                                 KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo_mbl, 80), .Rows(0).Item("hasil"), "0", pagenumber, ComboBox4.Text)
                '            ExecuteTrans(SQL)
                '            pagenumber = pagenumber + 1

                '            SQL = Get_Detail_Jurnal(Kode_Voucher6, Strings.Left(arrAkunBiayaHutMbl.Item(ComboBox4.SelectedIndex), 1),
                '                                  Strings.Mid(arrAkunBiayaHutMbl.Item(ComboBox4.SelectedIndex), 2, 1),
                '                                  Strings.Mid(Ganti(arrAkunBiayaHutMbl.Item(ComboBox4.SelectedIndex)), 3),
                '                                  KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo_mbl, 80), "0", .Rows(0).Item("hasil"), pagenumber, ComboBox4.Text)
                '            ExecuteTrans(SQL)
                '            pagenumber = pagenumber + 1

                '            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                '            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "kode_voucher = '" & Kode_Voucher6 & "'"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    If Dr("debit") <> Dr("kredit") Then
                '                        Dr.Close()
                '                        CloseTrans()
                '                        CloseConn()
                '                        MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                        Exit Sub
                '                    End If
                '                Else
                '                    Dr.Close()
                '                    CloseTrans()
                '                    CloseConn()
                '                    MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                    Exit Sub
                '                End If
                '            End Using
                '        End If
                '    End With
                'End Using



                'Dim Kode_Voucher7 As String = ""
                'Dim __Kode_Voucher7 As String = "NULL"
                'Dim ket_di_jurnal_promo_2 As String = "Penj. BS " & TxtFaktur.Text.Trim & ";" & TextBox11.Text.Trim & ";" & HilangkanTanda(LvJml) & "_" & LvNm


                'SQL = "select jenis, sum(hasil) as hasil from penjualan_budgeting_2 where "
                'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "no_faktur = '" & TxtFaktur.Text.Trim & "' group by jenis order by jenis"
                'Using Ds = BindingTrans(SQL)
                '    With Ds.Tables("MyTable")
                '        If .Rows.Count <> 0 Then
                '            Kode_Voucher7 = GetLastNumberJurnal(Format(tgl_skrg, "yyyyMM"), fJurnalJual & TextBox21.Text & arrInisialFaktur(ComboBox4.SelectedIndex), KodePerusahaan)
                '            __Kode_Voucher7 = "'" & Kode_Voucher7 & "'"


                '            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                '            SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                '            SQL = SQL & "'" & Kode_Voucher7 & "', "
                '            SQL = SQL & "'" & Format(tgl_skrg, "yyyy-MM-dd") & "', "
                '            SQL = SQL & "'" & Format(tgl_skrg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                '            SQL = SQL & "'" & KodeProyek & "', '" & Strings.Left(ket_di_jurnal_promo_2, 80) & "', '', "
                '            SQL = SQL & "'-', '" & UserID & "', '" & ComboBox4.Text & "')"
                '            ExecuteTrans(SQL)

                '            pagenumber = 1
                '            Dim nilai_debit As Double = 0

                '            For b As Integer = 0 To .Rows.Count - 1
                '                Dim akunhutpromo As String = ""

                '                If b = 0 Then
                '                    akunhutpromo = arrAkunBiayaHut1.Item(ComboBox4.SelectedIndex)
                '                ElseIf b = 1 Then
                '                    akunhutpromo = arrAkunBiayaHut2.Item(ComboBox4.SelectedIndex)
                '                ElseIf b = 2 Then
                '                    akunhutpromo = arrAkunBiayaHut3.Item(ComboBox4.SelectedIndex)
                '                Else
                '                    akunhutpromo = "x"
                '                End If

                '                SQL = Get_Detail_Jurnal(Kode_Voucher7, Strings.Left(akunhutpromo, 1),
                '                                  Strings.Mid(akunhutpromo, 2, 1),
                '                                  Strings.Mid(Ganti(akunhutpromo), 3),
                '                                  KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo_2, 80), "0", .Rows(b).Item("hasil"), pagenumber, ComboBox4.Text)
                '                ExecuteTrans(SQL)
                '                pagenumber = pagenumber + 1

                '                nilai_debit = nilai_debit + .Rows(b).Item("hasil")
                '            Next

                '            SQL = Get_Detail_Jurnal(Kode_Voucher7, Strings.Left(arrAkunBiayaPromo.Item(ComboBox4.SelectedIndex), 1),
                '                                 Strings.Mid(arrAkunBiayaPromo.Item(ComboBox4.SelectedIndex), 2, 1),
                '                                 Strings.Mid(Ganti(arrAkunBiayaPromo.Item(ComboBox4.SelectedIndex)), 3),
                '                                 KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo_2, 80), nilai_debit, "0", pagenumber, ComboBox4.Text)
                '            ExecuteTrans(SQL)
                '            pagenumber = pagenumber + 1



                '            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                '            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "kode_voucher = '" & Kode_Voucher7 & "'"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    If Dr("debit") <> Dr("kredit") Then
                '                        Dr.Close()
                '                        CloseTrans()
                '                        CloseConn()
                '                        MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                        Exit Sub
                '                    End If
                '                Else
                '                    Dr.Close()
                '                    CloseTrans()
                '                    CloseConn()
                '                    MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                    Exit Sub
                '                End If
                '            End Using
                '        End If
                '    End With
                'End Using

                ''==========

                'Dim Kode_Voucher8 As String = ""
                'Dim __Kode_Voucher8 As String = "NULL"
                'Dim ket_di_jurnal_promo_3 As String = "Penj. TOTO " & TxtFaktur.Text.Trim & ";" & TextBox11.Text.Trim & ";" & HilangkanTanda(LvJml) & "_" & LvNm


                'SQL = "select jenis, sum(hasil) as hasil from penjualan_budgeting_3 where "
                'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "no_faktur = '" & TxtFaktur.Text.Trim & "' group by jenis order by jenis"
                'Using Ds = BindingTrans(SQL)
                '    With Ds.Tables("MyTable")
                '        If .Rows.Count <> 0 Then
                '            Kode_Voucher8 = GetLastNumberJurnal(Format(tgl_skrg, "yyyyMM"), fJurnalJual & TextBox21.Text & arrInisialFaktur(ComboBox4.SelectedIndex), KodePerusahaan)
                '            __Kode_Voucher8 = "'" & Kode_Voucher8 & "'"


                '            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                '            SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                '            SQL = SQL & "'" & Kode_Voucher8 & "', "
                '            SQL = SQL & "'" & Format(tgl_skrg, "yyyy-MM-dd") & "', "
                '            SQL = SQL & "'" & Format(tgl_skrg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                '            SQL = SQL & "'" & KodeProyek & "', '" & Strings.Left(ket_di_jurnal_promo_3, 80) & "', '', "
                '            SQL = SQL & "'-', '" & UserID & "', '" & ComboBox4.Text & "')"
                '            ExecuteTrans(SQL)

                '            pagenumber = 1
                '            Dim nilai_debit As Double = 0

                '            For b As Integer = 0 To .Rows.Count - 1
                '                Dim akunhutpromo As String = ""

                '                If b = 0 Then
                '                    akunhutpromo = arrAkunBiayaHut1.Item(ComboBox4.SelectedIndex)
                '                ElseIf b = 1 Then
                '                    akunhutpromo = arrAkunBiayaHut2.Item(ComboBox4.SelectedIndex)
                '                ElseIf b = 2 Then
                '                    akunhutpromo = arrAkunBiayaHut3.Item(ComboBox4.SelectedIndex)
                '                Else
                '                    akunhutpromo = "x"
                '                End If

                '                SQL = Get_Detail_Jurnal(Kode_Voucher8, Strings.Left(akunhutpromo, 1),
                '                                  Strings.Mid(akunhutpromo, 2, 1),
                '                                  Strings.Mid(Ganti(akunhutpromo), 3),
                '                                  KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo_3, 80), "0", .Rows(b).Item("hasil"), pagenumber, ComboBox4.Text)
                '                ExecuteTrans(SQL)
                '                pagenumber = pagenumber + 1

                '                nilai_debit = nilai_debit + .Rows(b).Item("hasil")
                '            Next

                '            SQL = Get_Detail_Jurnal(Kode_Voucher8, Strings.Left(arrAkunBiayaPromo.Item(ComboBox4.SelectedIndex), 1),
                '                                 Strings.Mid(arrAkunBiayaPromo.Item(ComboBox4.SelectedIndex), 2, 1),
                '                                 Strings.Mid(Ganti(arrAkunBiayaPromo.Item(ComboBox4.SelectedIndex)), 3),
                '                                 KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo_3, 80), nilai_debit, "0", pagenumber, ComboBox4.Text)
                '            ExecuteTrans(SQL)
                '            pagenumber = pagenumber + 1



                '            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                '            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "kode_voucher = '" & Kode_Voucher8 & "'"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    If Dr("debit") <> Dr("kredit") Then
                '                        Dr.Close()
                '                        CloseTrans()
                '                        CloseConn()
                '                        MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                        Exit Sub
                '                    End If
                '                Else
                '                    Dr.Close()
                '                    CloseTrans()
                '                    CloseConn()
                '                    MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                    Exit Sub
                '                End If
                '            End Using
                '        End If
                '    End With
                'End Using

                ''==========

                'Dim Kode_Voucher9 As String = ""
                'Dim __Kode_Voucher9 As String = "NULL"
                'Dim ket_di_jurnal_promo_4 As String = "Penj. BioCrm " & TxtFaktur.Text.Trim & ";" & TextBox11.Text.Trim & ";" & HilangkanTanda(LvJml) & "_" & LvNm


                'SQL = "select jenis, sum(hasil) as hasil from penjualan_budgeting_4 where "
                'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "no_faktur = '" & TxtFaktur.Text.Trim & "' group by jenis order by jenis"
                'Using Ds = BindingTrans(SQL)
                '    With Ds.Tables("MyTable")
                '        If .Rows.Count <> 0 Then
                '            Kode_Voucher9 = GetLastNumberJurnal(Format(tgl_skrg, "yyyyMM"), fJurnalJual & TextBox21.Text & arrInisialFaktur(ComboBox4.SelectedIndex), KodePerusahaan)
                '            __Kode_Voucher9 = "'" & Kode_Voucher9 & "'"


                '            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                '            SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                '            SQL = SQL & "'" & Kode_Voucher9 & "', "
                '            SQL = SQL & "'" & Format(tgl_skrg, "yyyy-MM-dd") & "', "
                '            SQL = SQL & "'" & Format(tgl_skrg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                '            SQL = SQL & "'" & KodeProyek & "', '" & Strings.Left(ket_di_jurnal_promo_4, 80) & "', '', "
                '            SQL = SQL & "'-', '" & UserID & "', '" & ComboBox4.Text & "')"
                '            ExecuteTrans(SQL)

                '            pagenumber = 1
                '            Dim nilai_debit As Double = 0

                '            'For b As Integer = 0 To .Rows.Count - 1
                '            Dim akunhutpromo As String = arrAkunBiayaHut4.Item(ComboBox4.SelectedIndex)

                '            'If b = 0 Then
                '            '    akunhutpromo = arrAkunBiayaHut4.Item(ComboBox4.SelectedIndex)
                '            'ElseIf b = 1 Then
                '            '    akunhutpromo = arrAkunBiayaHut5.Item(ComboBox4.SelectedIndex)
                '            'ElseIf b = 2 Then
                '            '    akunhutpromo = arrAkunBiayaHut6.Item(ComboBox4.SelectedIndex)
                '            'Else
                '            '    akunhutpromo = "x"
                '            'End If

                '            SQL = Get_Detail_Jurnal(Kode_Voucher9, Strings.Left(akunhutpromo, 1),
                '                              Strings.Mid(akunhutpromo, 2, 1),
                '                              Strings.Mid(Ganti(akunhutpromo), 3),
                '                              KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo_4, 80), "0", .Rows(0).Item("hasil"), pagenumber, ComboBox4.Text)
                '            ExecuteTrans(SQL)
                '            pagenumber = pagenumber + 1

                '            nilai_debit = nilai_debit + .Rows(0).Item("hasil")
                '            ' Next

                '            SQL = Get_Detail_Jurnal(Kode_Voucher9, Strings.Left(arrAkunBiayaPromo2.Item(ComboBox4.SelectedIndex), 1),
                '                                 Strings.Mid(arrAkunBiayaPromo2.Item(ComboBox4.SelectedIndex), 2, 1),
                '                                 Strings.Mid(Ganti(arrAkunBiayaPromo2.Item(ComboBox4.SelectedIndex)), 3),
                '                                 KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo_4, 80), nilai_debit, "0", pagenumber, ComboBox4.Text)
                '            ExecuteTrans(SQL)
                '            pagenumber = pagenumber + 1



                '            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                '            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "kode_voucher = '" & Kode_Voucher9 & "'"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    If Dr("debit") <> Dr("kredit") Then
                '                        Dr.Close()
                '                        CloseTrans()
                '                        CloseConn()
                '                        MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                        Exit Sub
                '                    End If
                '                Else
                '                    Dr.Close()
                '                    CloseTrans()
                '                    CloseConn()
                '                    MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                    Exit Sub
                '                End If
                '            End Using
                '        End If
                '    End With
                'End Using
                '==========

                'Dim Kode_Voucher10 As String = ""
                'Dim __Kode_Voucher10 As String = "NULL"
                'Dim ket_di_jurnal_promo_10 As String = "Penj_ " & TxtFaktur.Text.Trim & ";" & TextBox11.Text.Trim & ";" & HilangkanTanda(LvJml) & "_" & LvNm


                'SQL = "select jenis, sum(hasil) as hasil from penjualan_budgeting_new where "
                'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "no_faktur = '" & TxtFaktur.Text.Trim & "' group by jenis order by jenis"
                'Using Ds = BindingTrans(SQL)
                '    With Ds.Tables("MyTable")
                '        If .Rows.Count <> 0 Then
                '            Kode_Voucher10 = GetLastNumberJurnal(Format(tgl_skrg, "yyyyMM"), fJurnalJual & TextBox21.Text & arrInisialFaktur(ComboBox4.SelectedIndex), KodePerusahaan)
                '            __Kode_Voucher10 = "'" & Kode_Voucher10 & "'"

                '            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                '            SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                '            SQL = SQL & "'" & Kode_Voucher10 & "', "
                '            SQL = SQL & "'" & Format(tgl_skrg, "yyyy-MM-dd") & "', "
                '            SQL = SQL & "'" & Format(tgl_skrg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                '            SQL = SQL & "'" & KodeProyek & "', '" & Strings.Left(ket_di_jurnal_promo_10, 80) & "', '', "
                '            SQL = SQL & "'-', '" & UserID & "', '" & ComboBox4.Text & "')"
                '            ExecuteTrans(SQL)

                '            pagenumber = 1

                '            SQL = Get_Detail_Jurnal(Kode_Voucher10, Strings.Left(arrAkunBiayaNew.Item(ComboBox4.SelectedIndex), 1), _
                '                             Strings.Mid(arrAkunBiayaNew.Item(ComboBox4.SelectedIndex), 2, 1), _
                '                             Strings.Mid(Ganti(arrAkunBiayaNew.Item(ComboBox4.SelectedIndex)), 3), _
                '                             KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo_10, 80), .Rows(0).Item("hasil"), "0", pagenumber)
                '            ExecuteTrans(SQL)
                '            pagenumber = pagenumber + 1

                '            SQL = Get_Detail_Jurnal(Kode_Voucher10, Strings.Left(arrAkunBiayaHutNew.Item(ComboBox4.SelectedIndex), 1), _
                '                              Strings.Mid(arrAkunBiayaHutNew.Item(ComboBox4.SelectedIndex), 2, 1), _
                '                              Strings.Mid(Ganti(arrAkunBiayaHutNew.Item(ComboBox4.SelectedIndex)), 3), _
                '                              KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo_10, 80), "0", .Rows(0).Item("hasil"), pagenumber)
                '            ExecuteTrans(SQL)
                '            pagenumber = pagenumber + 1

                '            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                '            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                '            SQL = SQL & "kode_voucher = '" & Kode_Voucher10 & "'"
                '            Using Dr = OpenTrans(SQL)
                '                If Dr.Read Then
                '                    If Dr("debit") <> Dr("kredit") Then
                '                        Dr.Close()
                '                        CloseTrans()
                '                        CloseConn()
                '                        MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                        Exit Sub
                '                    End If
                '                Else
                '                    Dr.Close()
                '                    CloseTrans()
                '                    CloseConn()
                '                    MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                    Exit Sub
                '                End If
                '            End Using
                '        End If
                '    End With
                'End Using

                Dim _akun_kas As String = ""
                'If ComboBox2.SelectedIndex = 0 Then
                '    _akun_kas = "'" & akun_kas & "'"
                'Else
                '    _akun_kas = "NULL"
                'End If

                SQL = "update penjualan set "
                'If arrMetodePotStock.Item(ComboBox4.SelectedIndex) = "A" And cabang_sendiri = "T" Then
                '    SQL = SQL & "akun_kas = " & akun_kas2 & ", "
                'Else
                '    SQL = SQL & "akun_kas = " & _akun_kas & ", "
                'End If
                SQL = SQL & "persen_insentif_1 = " & persen_insentif_1 & ", persen_insentif_2 = " & persen_insentif_2 & ", "
                SQL = SQL & "nilai_insentif_1 = " & nilai_insentif_1 & ", nilai_insentif_2 = " & nilai_insentif_2 & " "
                'SQL = SQL & "akun_biaya_insentif_1 = '" & coa_biaya_insentif_1 & "', "
                'SQL = SQL & "akun_biaya_insentif_2 = '" & coa_biaya_insentif_2 & "', "
                'SQL = SQL & "akun_hutang_insentif_1 = '" & coa_hutang_insentif_1 & "', "
                'SQL = SQL & "akun_hutang_insentif_2 = '" & coa_hutang_insentif_2 & "', "
                'If arrMetodePotStock.Item(ComboBox4.SelectedIndex) = "A" And cabang_sendiri = "T" Then
                '    SQL = SQL & "kode_voucher_1 = " & __Kode_Voucher & ", "
                '    SQL = SQL & "kode_voucher_2 = " & __Kode_Voucher2 & ", "
                '    SQL = SQL & "kode_voucher_2x = " & __Kode_Voucher2x & ", "
                'End If
                'SQL = SQL & "kode_voucher_3 = " & __Kode_Voucher3 & ", "
                'SQL = SQL & "kode_voucher_4 = " & __Kode_Voucher4 & ", kepala = '" & kepala & "', "
                'SQL = SQL & "kode_voucher_5 = " & __Kode_Voucher5 & ", "
                'SQL = SQL & "kode_voucher_6 = " & __Kode_Voucher6 & ", "
                'SQL = SQL & "kode_voucher_7 = " & __Kode_Voucher7 & ", "
                'SQL = SQL & "kode_voucher_8 = " & __Kode_Voucher8 & ", "
                'SQL = SQL & "kode_voucher_9 = " & __Kode_Voucher9 & " "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & TxtFaktur.Text.Trim & "'"
                ExecuteTrans(SQL)



                SQL = "update emi_po set flag_penjualan = 'Y' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & TextBox23.Text.Trim & "' "
                ExecuteTrans(SQL)


                'SQL = "update penjualan set kode_voucher_2 = '" & Kode_Voucher2 & "', akun_kas = " & akun_kas & ", "
                'SQL = SQL & "akun_piutang = " & akun_piutang & ", akun_piutang_sementara = " & akun_piutang_sementara & ", "
                'SQL = SQL & "persen_insentif_1 = " & persen_insentif_1 & ", persen_insentif_2 = " & persen_insentif_2 & ", "
                'SQL = SQL & "nilai_insentif_1 = " & nilai_insentif_1 & ", nilai_insentif_2 = " & nilai_insentif_2 & ", "
                'SQL = SQL & "akun_biaya_insentif_1 = '" & coa_biaya_insentif_1 & "', "
                'SQL = SQL & "akun_biaya_insentif_2 = '" & coa_biaya_insentif_2 & "', "
                'SQL = SQL & "akun_hutang_insentif_1 = '" & coa_hutang_insentif_1 & "', "
                'SQL = SQL & "akun_hutang_insentif_2 = '" & coa_hutang_insentif_2 & "', "
                'SQL = SQL & "kode_voucher_3 = " & __Kode_Voucher3 & ", "
                'SQL = SQL & "kode_voucher_4 = " & __Kode_Voucher4 & ", kepala = '" & kepala & "', "
                'SQL = SQL & "kode_voucher_5 = " & __Kode_Voucher5 & ", "
                'SQL = SQL & "kode_voucher_6 = " & __Kode_Voucher6 & ", "
                'SQL = SQL & "kode_voucher_7 = " & __Kode_Voucher7 & ", "
                'SQL = SQL & "kode_voucher_8 = " & __Kode_Voucher8 & ", "
                'SQL = SQL & "kode_voucher_9 = " & __Kode_Voucher9 & ", "
                'SQL = SQL & "kode_voucher_2x = " & __Kode_Voucher2x & " "
                'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & TxtFaktur.Text.Trim & "'"
                'ExecuteTrans(SQL)

                SQL = "delete from sementara where kode_perusahaan = '" & KodePerusahaan & "' and kode_unik = '" & Label1.Text & "'"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

                CloseConn()

                'If ComboBox2.SelectedIndex = 0 Then 'tunai
                '    'sp = New SerialPort(My.Settings.Port_Cust_Display, 9600, Parity.None, 8, StopBits.One)
                '    'If Not (sp Is Nothing) Then
                '    '    sp.Open()
                '    '    '// to clear the display
                '    '    sp.Write(Convert.ToString(Chr(12)))

                '    '    '// first line goes here
                '    '    sp.WriteLine("Kembali Rp." & Format(kembalian, "N0"))
                '    '    sp.WriteLine(Chr(13) & "TERIMA KASIH!")

                '    '    sp.Close()
                '    '    sp.Dispose()
                '    '    sp = Nothing
                '    'End If

                '    'fkembalian.ShowDialog()
                'End If
            Catch ex As Exception
                TextBox5.Text = "0"
                For i As Integer = listview1.Items.Count - 1 To 0 Step -1
                    If listview1.Items(i).SubItems(13).Text = "Y" Then
                        listview1.Items(i).Remove()
                    End If
                Next

                HitungGrandTotal()

                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        Else


        End If

        Dim Boleh_Cetak As String = "T"
        Try
            OpenConn()

            If CekButtonRole("Cetak_Faktur_Penjualan") = "T" Then
                Boleh_Cetak = "T"
            Else
                Boleh_Cetak = "Y"
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'If Boleh_Cetak = "Y" Then
        '    Dim TanyaCetak As String = MessageBox.Show("Mau dicetak?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        '    If TanyaCetak = vbYes Then
        '        Cetak()
        '    End If
        'End If

        If cabang_sendiri = "Y" Then
            Dim TanyaCetak As String = MessageBox.Show("Mau dicetak?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If TanyaCetak = vbYes Then
                Cetak()
            End If
        End If

        If Button2.Text.ToUpper = "&SIMPAN" Then
            If ComboBox2.SelectedIndex = 0 Then 'tunai
                'sp = New SerialPort(My.Settings.Port_Cust_Display, 9600, Parity.None, 8, StopBits.One)
                'If Not (sp Is Nothing) Then
                '    sp.Open()
                '    '// to clear the display
                '    sp.Write(Convert.ToString(Chr(12)))

                '    '// first line goes here
                '    sp.WriteLine("Kembali Rp." & Format(kembalian, "N0"))
                '    sp.WriteLine(Chr(13) & "TERIMA KASIH!")

                '    sp.Close()
                '    sp.Dispose()
                '    sp = Nothing
                'End If

                ' fkembalian.ShowDialog()
            End If
        End If

        'Kirim_WA(TxtFaktur.Text.Trim)

        bersihseluruh()
        TextBox15.Focus()
    End Sub

    Private Sub export_inv(ByVal nama_cr As String, ByVal ekspor_kemana As String, ByVal krts As String, ByVal lokasi_file As String, ByVal encrpyt_nama As String)
        Try

            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            If System.IO.Directory.Exists(lokasi_file) = False Then
                System.IO.Directory.CreateDirectory(lokasi_file)
            End If

            Dim format_akhir As String = Format(tgl_skrg, "ddMMMyyyyHHmmss")
            kertas = krts

            SQL = "select a.kode_perusahaan, a.flag_cabang_sendiri, a.jenis from penjualan a, detail_penjualan b where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & TxtFaktur.Text.Trim & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    CrDoc = New Faktur_Penjualan_Akhir_Reseller



                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.PrintOptions.PrinterName = PrinterName
                    CrDoc.RecordSelectionFormula = "{detail_penjualan.Kode_Perusahaan} = '" & KodePerusahaan & "' and {detail_penjualan.No_faktur} = '" & TxtFaktur.Text.Trim & "'"
                    CrDoc.SummaryInfo.ReportTitle = "."

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterName
                    Dim rawKind As Integer
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                            CrDoc.PrintOptions.PaperSize = rawKind
                            Exit For
                        End If
                    Next

                    'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                    'CrDoc.PrintToPrinter(1, False, 1, 99)

                    CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)

                    Dim nama_file As String = Replace(TxtFaktur.Text.Trim, "/", "") & "_" & format_akhir

                    If encrpyt_nama = "Y" Then
                        Dim hasher As MD5 = MD5.Create()
                        Dim dbytes As Byte() = hasher.ComputeHash(Encoding.UTF8.GetBytes(nama_file))
                        Dim sBuilder As New StringBuilder()
                        For n As Integer = 0 To dbytes.Length - 1
                            sBuilder.Append(dbytes(n).ToString("X2"))
                        Next n

                        nama_file = sBuilder.ToString()

                    Else
                        nama_file = nama_file
                    End If

                    hasil_path_inv_pdf = lokasi_file & nama_file & ".pdf"
                    hasil_nama_inv_pdf = nama_file & ".pdf"

                    If ekspor_kemana.ToUpper = "WORD" Then
                        CrDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, lokasi_file & "\" & nama_file & ".doc")
                    Else
                        CrDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, lokasi_file & "\" & nama_file & ".pdf")
                    End If
                End If
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub




    Private Sub xxxxx(ByVal nama_obj As String)
        Dim kolom As String = ""
        Dim isi As String = ""
        If nama_obj = "kd" Then
            'kolom = "a.kode_barang + '$' + convert(varchar(8), b.Tgl_Expire, 112)"
            kolom = "kode_barang"
            isi = kd.Text
        ElseIf nama_obj = "nm" Then
            kolom = "nama"
            isi = kd.Text
        End If

        Try
            OpenConn()

            ListView2.Items.Clear()

            'SQL = "Select kode_stock_owner, kode_barang, Nama, satuan, harga_jual, harga_jual_agen, good_stock, disc1, disc2 From barang where "
            'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and kode_stock_owner = '" & ComboBox4.Text & "' and "
            'SQL = SQL & "kode_pembeda in(" & list_pembeda & ") and (kode_barang like '%" & kd.Text & "%' or nama like '%" & kd.Text & "%') "
            'SQL = SQL & "order by nama"

            SQL = "Select top(25) x_hrg_mid, x_hrg_online, x_hrg_special, x_hrg_modern, x_hrg_min, x_hrg_max, f_hrg_resell_min, f_hrg_resell_std, f_hrg_resell_max, "
            SQL = SQL & "kode_stock_owner, kode_barang, Nama, satuan, "
            SQL = SQL & "harga_jual, good_stock, disc1, disc2 From barang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and kode_stock_owner = '" & TextBoxGudang.Text.Trim & "' and jenis = 'B' and " 'vv
            SQL = SQL & "nama like '%" & isi & "%' "
            If TextBox21.Text = "R" Then
                SQL = SQL & "and flag_ppn = 'T' "
            ElseIf TextBox21.Text = "C" Then
                SQL = SQL & "and flag_ppn = 'Y' "
            End If

            SQL = SQL & "order by nama"
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Dim Lvw As ListViewItem
                        Lvw = ListView2.Items.Add(.Rows(i).Item("kode_stock_owner"))
                        Lvw.SubItems.Add(.Rows(i).Item("kode_barang"))
                        Lvw.SubItems.Add(.Rows(i).Item("Nama"))
                        Lvw.SubItems.Add(.Rows(i).Item("satuan"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item(pake_hrg_yg_mana), "N0"))
                        Lvw.SubItems.Add(TampilanDesimal(.Rows(i).Item("good_stock")))
                        Lvw.SubItems.Add(General_Class.CekZERO(.Rows(i).Item("disc1")))
                        Lvw.SubItems.Add(Format(General_Class.CekZERO(.Rows(i).Item("disc2")), "N0"))
                    Next
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub kd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles kd.TextChanged
        If Label14.Text = "1" And kd.Text.Length >= 3 Then
            If kd.Text.Trim.Length = 0 Then
                ListView2.Visible = False : Exit Sub
            Else
                ListView2.Visible = True
            End If

            xxxxx("nm")
        Else
            ListView2.Visible = False
        End If

    End Sub

    Private Sub ListView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView2.DoubleClick
        If ListView2.Items.Count = 0 Then Exit Sub

        kd.Text = ListView2.FocusedItem.SubItems(1).Text
        kd.Focus()
        If TextBox4.Enabled = True Then
            TextBox4.Focus()
        Else
            jml.Focus()
        End If
        ListView2.Visible = False
    End Sub

    Private Sub ListView2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView2.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView2_DoubleClick(ListView2, e)
        End If
    End Sub

    Private Sub ComboBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox4.KeyPress
        If e.KeyChar = Chr(13) Then kd.Focus()
    End Sub

    Private Sub TextBox10_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox10.KeyDown
        'If e.KeyCode = Keys.Down Then
        '    If ListView10.Items.Count = 0 Then Exit Sub
        '    ListView10.Focus()
        'End If

        If e.KeyCode = Keys.ControlKey Then Exit Sub
    End Sub

    Private Sub TextBox10_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox10.KeyPress
        If e.KeyChar = Chr(13) Then
            'If TextBox10.Text.Trim.Length = 0 Then
            '    ListView10.Visible = False : TextBox11.Focus() : Exit Sub
            'End If
            'TextBox10_Leave(TextBox10, e)
            kd.Focus()
        End If
        If e.KeyChar = Chr(Asc("'")) Or e.KeyChar = Chr(20) Then e.KeyChar = Chr(0)
    End Sub

    Public Sub TextBox10_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox10.Leave
        'If TextBox10.Text.Trim.Length = 0 Then
        '    ListView10.Visible = False
        '    TextBox11.Text = ""
        '    Exit Sub
        'Else
        '    ListView10.Visible = True
        'End If
        'If ListView10.Focused = True Then Exit Sub


    End Sub

    Private Sub TextBox10_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox10.MouseClick
        If e.Button.ToString().ToLower() = "right" Then
            SendKeys.Send("{Esc}")
        End If
    End Sub

    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged


        'If TextBox10.Text.Trim.Length = 0 Then
        '    ListView10.Visible = False : Exit Sub
        'Else
        '    ListView10.Visible = True
        'End If

        'Try
        '    OpenConn()

        '    SQL = "select disc1, disc2, kode_customer, Nama, Alamat, Telepon, hp from customers where "
        '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and kode_customer like '%" & Trim(TextBox10.Text) & "%' order by kode_customer"
        '    ListView10.Items.Clear()
        '    Dim Lvw As ListViewItem
        '    Using Dr = OpenTrans(SQL)
        '        Do While Dr.Read
        '            Lvw = ListView10.Items.Add(Dr("disc1"))
        '            Lvw.SubItems.Add(Dr("disc2"))
        '            Lvw.SubItems.Add(Dr("kode_customer"))
        '            Lvw.SubItems.Add(Dr("Nama"))
        '            Lvw.SubItems.Add(Dr("Alamat"))
        '            Lvw.SubItems.Add(Dr("Telepon"))
        '            Lvw.SubItems.Add(Dr("HP"))
        '        Loop
        '    End Using

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
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
            TextBox2.Focus()
        End If
        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox11_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox11.Leave
        If ListView10.Focused = True Then Exit Sub
        TextBox10.Text = "" : TextBox11.Text = ""
    End Sub

    Private Sub TextBox11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged
        'If TextBox11.Text.Trim.Length = 0 Then
        '    ListView10.Visible = False : Exit Sub
        'Else
        '    ListView10.Visible = True
        'End If

        'Try
        '    OpenConn()

        '    SQL = "select * from customers where kode_perusahaan = '" & KodePerusahaan & "' and nama like '%" & Trim(TextBox11.Text) & "%' order by nama"
        '    ListView10.Items.Clear()
        '    Dim Lvw As ListViewItem
        '    Using Dr = OpenTrans(SQL)
        '        Do While Dr.Read
        '            Lvw = ListView10.Items.Add(Dr("disc1"))
        '            Lvw.SubItems.Add(Dr("disc2"))
        '            Lvw.SubItems.Add(Dr("kode_customer"))
        '            Lvw.SubItems.Add(Dr("Nama"))
        '            Lvw.SubItems.Add(Dr("Alamat"))
        '            Lvw.SubItems.Add(Dr("Telepon"))
        '            Lvw.SubItems.Add(Dr("HP"))
        '        Loop
        '    End Using

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub

    Private Sub ListView10_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView10.DoubleClick
        If ListView10.Items.Count = 0 Then Exit Sub

        TextBox10.Text = ListView10.FocusedItem.SubItems(2).Text

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
                kd.Focus()
            Else
                ComboBox5.Focus()
            End If
        End If
    End Sub

    Private Sub ComboBox2_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.Leave

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedIndex = 0 Then
            DateTimePicker2.Visible = False
            ComboBox5.Visible = False
            'ComboBoxCb1.Enabled = True
            'ComboBoxCb1.SelectedIndex = 0
        Else
            DateTimePicker2.Visible = False
            ComboBox5.Visible = False
            'ComboBoxCb1.Enabled = False
            'ComboBoxCb1.SelectedIndex = 0
        End If

        HitungDiskonCash()
        HitungGrandTotal()
    End Sub

    Private Sub ComboBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox5.KeyPress
        If e.KeyChar = Chr(13) Then kd.Focus()
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox5.SelectedIndexChanged
        Dim xx As Integer = 0
        If ComboBox5.SelectedIndex = -1 Then
            xx = 0
        Else
            xx = Val(ComboBox5.Text)
        End If
        DateTimePicker2.Value = DateAdd(DateInterval.Day, xx, tgl_skrg)
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            'discz.Enabled = True ': discz.Focus()
            CheckBox2.Checked = False
        Else
            'discz.Enabled = False
            discz.Text = "0" : TextBox1.Text = "0"
        End If
        HitungGrandTotal()
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            'Discx.Enabled = True ': Discx.Focus()
            TextBox6.Enabled = True
            CheckBox1.Checked = False
        Else
            'Discx.Enabled = False
            'Discx.Text = "0"
            TextBox6.Enabled = False
            TextBox6.Text = "0"
        End If
        HitungGrandTotal()
    End Sub

    Private Sub Discx_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Discx.TextChanged
        HitungGrandTotal()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        bersihseluruh()
        TextBox15.Focus()
    End Sub

    Private Sub TxtFaktur_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFaktur.KeyPress
        If e.KeyChar = Chr(13) Then
            ComboBox2.Focus()
        End If
    End Sub

    Private Sub TxtFaktur_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtFaktur.Leave
        'If TxtFaktur.Text.Trim.Length = 0 Then
        '    Try

        '        OpenConn()

        '        get_no_faktur("T")

        '        CloseConn()
        '        Exit Sub

        '    Catch ex As Exception
        '        CloseConn()
        '        MessageBox.Show(ex.Message)
        '        Exit Sub
        '    End Try
        'End If

        'Try

        '    OpenConn()

        '    SQL = "select c.subtotal, b.kode_cb, c.nota_kecil, c.kode_marketing, f.nama as nama_marketing, c.modal, b.flag_lipat, "
        '    SQL = SQL & "b.diskon_promo, b.diskon_rupiah, c.keterangan, b.grand, b.kode_perusahaan, b.no_faktur, b.tanggal, b.jam, "
        '    SQL = SQL & "b.jenis_transaksi, b.tgl_jatuh_tempo, b.flag_lunas, b.tgl_lunas, b.userid, "
        '    SQL = SQL & "b.uservalidasi, b.disc1, b.disc2, b.status, b.kode_customer, "
        '    SQL = SQL & "d.nama, b.kode_sales, e.nama as namasales, c.kode_stock_owner, c.kode_barang, "
        '    SQL = SQL & "a.nama as namabarang, c.harga, a.satuan, c.persen_diskon, c.nilai_diskon, "
        '    SQL = SQL & "c.jumlah, c.serial_number, c.harga_min, c.usermin, c.pakai_sn, c.barang_hadiah "
        '    SQL = SQL & "from barang a, Penjualan b, detail_penjualan c, Customers d, sales e, marketing f where "
        '    SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and "
        '    SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan And d.kode_perusahaan = e.kode_perusahaan and e.kode_perusahaan = f.kode_perusahaan and "
        '    SQL = SQL & "a.kode_barang = c.kode_barang and a.kode_stock_owner = c.kode_stock_owner and b.no_faktur = c.no_faktur and "
        '    SQL = SQL & "b.kode_sales = e.kode_sales and c.kode_marketing = f.kode_marketing and a.kode_perusahaan = '" & KodePerusahaan & "' and "
        '    SQL = SQL & "b.kode_customer = d.kode_customer and b.no_faktur = '" & TxtFaktur.Text.Trim & "' and "
        '    SQL = SQL & "b.status is null order by c.no_urut"
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then
        '            Dim Ket As String = ""
        '            listview1.Items.Clear()
        '            TextBox10.Text = Dr("kode_customer")
        '            TextBox11.Text = Dr("nama")
        '            ListView10.Visible = False
        '            DateTimePicker1.Value = Dr("tanggal")
        '            DateTimePicker1.Enabled = False

        '            TextBox2.Text = Dr("kode_sales")
        '            TextBox3.Text = Dr("namasales")
        '            ListView5.Visible = False

        '            ComboBox5.SelectedIndex = -1

        '            Button2.Text = "&Update"

        '            If Dr("jenis_transaksi") = "T" Then
        '                ComboBox2.SelectedIndex = 0
        '                DateTimePicker2.Value = tgl_skrg
        '                'ComboBoxCb1.Text = Dr("kode_cb")
        '            Else
        '                ComboBox2.SelectedIndex = 1
        '                DateTimePicker2.Value = Dr("tgl_jatuh_tempo")
        '                ' ComboBoxCb1.SelectedIndex = -1
        '            End If

        '            If Dr("disc1") = 0 Then
        '                CheckBox1.Checked = False
        '            Else
        '                CheckBox1.Checked = True
        '            End If

        '            If Dr("diskon_rupiah") = 0 Then
        '                CheckBox2.Checked = False
        '            Else
        '                CheckBox2.Checked = True
        '            End If

        '            TextBox5.Text = Dr("diskon_promo")
        '            TextBox6.Text = Dr("diskon_rupiah")
        '            discz.Text = Dr("disc1")
        '            Discx.Text = Dr("disc2")

        '            ComboBox1.Text = Dr("flag_lipat")

        '            If CekNULL(Dr("keterangan")) = "" Then
        '                Ket = ""
        '            Else
        '                Ket = Dr("keterangan")
        '            End If

        '            Dim lv As New ListViewItem
        '            Dim GrandTotal As Double = 0

        '            lv = listview1.Items.Add(Dr("kode_stock_owner"))
        '            lv.SubItems.Add(Dr("kode_barang"))
        '            lv.SubItems.Add(Dr("namabarang") & Ket)
        '            If Dr("pakai_sn") = "Y" Then
        '                lv.SubItems.Add(Dr("serial_number"))
        '            Else
        '                lv.SubItems.Add("-")
        '            End If
        '            lv.SubItems.Add(Format(Dr("harga"), "N0"))
        '            lv.SubItems.Add(TampilanDesimal(Dr("jumlah")))
        '            lv.SubItems.Add(Dr("satuan"))
        '            lv.SubItems.Add(Dr("persen_diskon"))
        '            lv.SubItems.Add(Format(Dr("nilai_diskon"), "N0"))

        '            If Dr("persen_diskon") = 0 Then
        '                GrandTotal = HasilDiskon(Dr("harga"), Dr("jumlah"), 0, Dr("nilai_diskon"), "RP")
        '            Else
        '                GrandTotal = HasilDiskon(Dr("harga"), Dr("jumlah"), Dr("persen_diskon"), 0, "PERSEN")
        '            End If
        '            lv.SubItems.Add(Format(Dr("subtotal"), "N0"))
        '            lv.SubItems.Add(Format(Dr("harga_min"), "N0"))
        '            If CekNULL(Dr("usermin")) = "" Then
        '                lv.SubItems.Add("NULL")
        '            Else
        '                lv.SubItems.Add(Dr("usermin"))
        '            End If
        '            lv.SubItems.Add(Dr("pakai_sn"))
        '            lv.SubItems.Add(Dr("barang_hadiah"))
        '            lv.SubItems.Add(Dr("modal"))
        '            lv.SubItems.Add(Dr("nota_kecil"))
        '            lv.SubItems.Add(Dr("kode_marketing"))
        '            lv.SubItems.Add(Dr("nama_marketing"))

        '            Do While Dr.Read
        '                GrandTotal = 0
        '                If CekNULL(Dr("keterangan")) = "" Then
        '                    Ket = ""
        '                Else
        '                    Ket = Dr("keterangan")
        '                End If

        '                lv = listview1.Items.Add(Dr("kode_stock_owner"))
        '                lv.SubItems.Add(Dr("kode_barang"))
        '                lv.SubItems.Add(Dr("namabarang") & Ket)
        '                If Dr("pakai_sn") = "Y" Then
        '                    lv.SubItems.Add(Dr("serial_number"))
        '                Else
        '                    lv.SubItems.Add("-")
        '                End If
        '                lv.SubItems.Add(Format(Dr("harga"), "N0"))
        '                lv.SubItems.Add(TampilanDesimal(Dr("jumlah")))
        '                lv.SubItems.Add(Dr("satuan"))
        '                lv.SubItems.Add(Dr("persen_diskon"))
        '                lv.SubItems.Add(Format(Dr("nilai_diskon"), "N0"))

        '                If Dr("persen_diskon") = 0 Then
        '                    GrandTotal = HasilDiskon(Dr("harga"), Dr("jumlah"), 0, Dr("nilai_diskon"), "RP")
        '                Else
        '                    GrandTotal = HasilDiskon(Dr("harga"), Dr("jumlah"), Dr("persen_diskon"), 0, "PERSEN")
        '                End If
        '                lv.SubItems.Add(Format(Dr("subtotal"), "N0"))
        '                lv.SubItems.Add(Format(Dr("harga_min"), "N0"))
        '                If CekNULL(Dr("usermin")) = "" Then
        '                    lv.SubItems.Add("NULL")
        '                Else
        '                    lv.SubItems.Add(Dr("usermin"))
        '                End If
        '                lv.SubItems.Add(Dr("pakai_sn"))
        '                lv.SubItems.Add(Dr("barang_hadiah"))
        '                lv.SubItems.Add(Dr("modal"))
        '                lv.SubItems.Add(Dr("nota_kecil"))
        '                lv.SubItems.Add(Dr("kode_marketing"))
        '                lv.SubItems.Add(Dr("nama_marketing"))
        '            Loop

        '            Disable_Customer()
        '            Label27.Visible = True
        '        Else
        '            bersihseluruh()
        '            TextBox10.Focus()
        '        End If
        '    End Using

        '    CloseConn()

        '    HitungGrandTotal()

        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub

    'Private Sub DateTimePicker1_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        OpenConn()

    '        If ComboBox2.SelectedIndex = 1 Then 'non tunai
    '            get_no_faktur("K")
    '        Else
    '            get_no_faktur("T")
    '        End If

    '        CloseConn()
    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    'End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox10.Text.Trim.Length = 0 Then
            MessageBox.Show("Customer harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox10.Focus()
            Exit Sub
        ElseIf TextBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("Sales harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox2.Focus()
            Exit Sub
        ElseIf ComboBox2.SelectedIndex = -1 Then
            MessageBox.Show("Pembayaran harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus()
            Exit Sub
        End If

        If ComboBox2.SelectedIndex = 0 Then
            'If ComboBoxCb1.SelectedIndex = -1 Or ComboBoxCb1.SelectedIndex = 0 Then
            '    MessageBox.Show("Cara bayar harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    ComboBoxCb1.Focus()
            '    Exit Sub
            'End If
        ElseIf ComboBox2.SelectedIndex = 1 Then
            If Format(DateTimePicker2.Value, "yyyy-MM-dd") < Format(tgl_skrg, "yyyy-MM-dd") Then
                MessageBox.Show("Tanggal jatuh tempo tidak boleh kurang dari tanggal sekarang.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                DateTimePicker2.Focus()
                Exit Sub
            End If
        End If

        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("Kelipatan point harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus()
            Exit Sub
        End If

        If TextBox5.Text.Trim.Length = 0 Then discz.Text = 0
        If TextBox6.Text.Trim.Length = 0 Then discz.Text = 0
        If discz.Text.Trim.Length = 0 Then discz.Text = 0
        If Discx.Text.Trim.Length = 0 Then Discx.Text = 0

        If listview1.Items.Count = 0 Then
            MessageBox.Show("Data barang yang akan disimpan masih kosong!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tny As String = MessageBox.Show("Yakin akan disimpan sementara?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tny = vbNo Then Exit Sub

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            SQL = "delete from sementara where kode_perusahaan = '" & KodePerusahaan & "' and kode_unik = '" & Label1.Text & "'"
            ExecuteTrans(SQL)

            Dim Grand_Jumlah As Double = 0
            For i As Integer = 0 To listview1.Items.Count - 1
                Get_Isi_Listview(i)

                Grand_Jumlah = Grand_Jumlah + Val(HilangkanTanda(LvJml))
            Next

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

            'Dim cb_1 As String = ""
            'If ComboBox2.SelectedIndex = 0 Then 'kalo tunai
            '    cb_1 = "'" & ComboBoxCb1.Text & "'"
            'Else
            '    cb_1 = "NULL"
            'End If

            SQL = "insert into Sementara(kode_perusahaan, kode_unik, tanggal, jam, kode_customer, "
            SQL = SQL & "kode_sales, jenis_transaksi, tgl_jatuh_tempo, discP, discRP, grand_jumlah, "
            SQL = SQL & "grand_total, userid, kode_cb) values('" & KodePerusahaan & "', '" & Label1.Text & "', "
            SQL = SQL & "'" & Format(tgl_skrg, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(tgl_skrg, "HH:mm:ss") & "', "
            SQL = SQL & "'" & TextBox10.Text.Trim & "', '" & TextBox2.Text.Trim & "', '" & Strings.Left(ComboBox2.Text, 1) & "', "
            SQL = SQL & "" & TJT & ", " & diskon1 & ", " & diskon2 & ", "
            SQL = SQL & "" & Grand_Jumlah & ", " & HilangkanTanda(Label12.Text) & ", '" & UserID & "' )"
            ExecuteTrans(SQL)

            For i As Integer = 0 To listview1.Items.Count - 1
                Get_Isi_Listview(i)

                Dim cari As Integer = InStr(LvNm, "[")
                Dim keterangan As String

                If cari = 0 Then
                    keterangan = "NULL"
                Else
                    keterangan = "'" & Strings.Mid(LvNm, cari) & "'"
                End If

                Dim xUsrMin As String = ""
                If LvUserID = "NULL" Then
                    xUsrMin = "NULL"
                Else
                    xUsrMin = "'" & LvUserID & "'"
                End If

                Dim xserial As String = ""
                If LvPakaiSN = "Y" Then
                    xserial = "'" & LvSerialNumber & "'"
                Else
                    xserial = "NULL"
                End If

                SQL = "insert into detail_Sementara(kode_perusahaan, kode_unik, kode_stock_owner, kode_barang, keterangan, jumlah,"
                SQL = SQL & "harga,persen_diskon,nilai_diskon, harga_min, usermin, "
                SQL = SQL & "pakai_sn, barang_hadiah, modal, nota_kecil, kode_marketing) values ('" & KodePerusahaan & "', "
                SQL = SQL & "'" & Label1.Text & "', '" & LvSO & "','" & LvKB & "', "
                SQL = SQL & "" & keterangan & ", '" & HilangkanTanda(LvJml) & "', "
                SQL = SQL & "'" & HilangkanTanda(LvHrg) & "', "
                SQL = SQL & "" & HilangkanTanda(LvDiscp) & ", "
                SQL = SQL & "" & HilangkanTanda(LvDiscrp) & ", "
                SQL = SQL & "" & HilangkanTanda(LvHrgMin) & ", " & xUsrMin & ", "
                SQL = SQL & "'" & LvPakaiSN & "', '" & LvHadiah & "', '" & HilangkanTanda(LvModal) & "', "
                SQL = SQL & "'" & LvNotaKecil & "', '" & LvKdSales & "')"
                ExecuteTrans(SQL)
            Next

            Cmd.Transaction.Commit()

            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        bersihseluruh()
        kd.Focus()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        ' cari_sementara("012614363809540")
        'cari_sementara("012614482121254")
    End Sub

    Private Sub SmpnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SmpnToolStripMenuItem.Click
        Button2_Click(SmpnToolStripMenuItem, e)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        HitungGrandTotal()
    End Sub

    Private Sub TextBox4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView3.Items.Count = 0 Then Exit Sub
            ListView3.Focus()
        End If
    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Chr(13) Then ket.Focus()
        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox4_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox4.Leave
        'If TextBox4.Text.Trim.Length = 0 Then Exit Sub
        'If ListView3.Focused = True Then Exit Sub

        'If ComboBox4.Text.Trim.Length = 0 Then
        '    MessageBox.Show("Stock owner harus diisi dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    kd.Text = "" : ComboBox4.Focus()
        '    Exit Sub
        'ElseIf kd.Text.Trim.Length = 0 Then
        '    MessageBox.Show("Kode barang harus diisi dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    kd.Text = "" : kd.Focus()
        '    Exit Sub
        'End If

        'Try
        '    OpenConn()

        '    SQL = "Select serial_number from barang_sn where kode_perusahaan = '" & KodePerusahaan & "' and "
        '    SQL = SQL & "kode_stock_owner = '" & ComboBox4.Text & "' and kode_barang = '" & kd.Text.Trim & "' and "
        '    SQL = SQL & "serial_number = '" & TextBox4.Text & "' and jumlah > 0"
        '    SQL = SQL & "order by serial_number"
        '    Using dr = OpenTrans(SQL)
        '        If dr.Read Then
        '            TextBox4.Text = dr("serial_number")
        '            ket.Focus()
        '            ListView3.Visible = False
        '        Else
        '            TextBox4.Text = ""
        '            TextBox4.Focus()
        '        End If
        '    End Using

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        'If TextBox4.Text.Trim.Length = 0 Then
        '    ListView3.Visible = False : Exit Sub
        'Else
        '    ListView3.Visible = True
        'End If

        'Try
        '    OpenConn()

        '    ListView3.Items.Clear()

        '    SQL = "Select serial_number from barang_sn where kode_perusahaan = '" & KodePerusahaan & "' and "
        '    SQL = SQL & "kode_stock_owner = '" & ComboBox4.Text & "' and kode_barang = '" & kd.Text.Trim & "' and "
        '    SQL = SQL & "serial_number like '" & TextBox4.Text & "%' and jumlah > 0"
        '    SQL = SQL & "order by serial_number"
        '    Using ds = BindingTrans(SQL)
        '        With ds.Tables("MyTable")
        '            For i As Integer = 0 To .Rows.Count - 1
        '                Dim Lvw As ListViewItem
        '                Lvw = ListView3.Items.Add(.Rows(i).Item("serial_number"))
        '            Next
        '        End With
        '    End Using

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub

    Private Sub ListView3_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView3.DoubleClick
        'If ListView3.Items.Count = 0 Then Exit Sub

        'TextBox4.Text = ListView3.FocusedItem.Text
        'TextBox4.Focus()
        'ket.Focus()
        'ListView3.Visible = False
    End Sub

    Private Sub ListView3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView3.KeyDown
        'If e.KeyCode = Keys.Enter Then
        '    ListView3_DoubleClick(ListView3, e)
        'End If
    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        HitungGrandTotal()
    End Sub

    Private Sub Cek_Promo()
        arrKDpromo.Clear() : arrKDXpromo.Clear() : arrJmlpromo.Clear()
        Dim diskonbaru As Double = 0

        SQL = "delete from promo_sementara where kode_perusahaan = '" & KodePerusahaan & "' and "
        SQL = SQL & "kode_unik = '" & Label1.Text & "'"
        ExecuteTrans(SQL)

        For i As Integer = 0 To listview1.Items.Count - 1
            Get_Isi_Listview(i)

            SQL = "select kode_perusahaan from promo_sementara where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_unik = '" & Label1.Text & "' and kode_stock_owner = '" & LvSO.Trim & "' and "
            SQL = SQL & "kode_barang = '" & LvKB.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    SQL = "update promo_sementara set jumlah = jumlah + " & HilangkanTanda(LvJml.Trim) & " where "
                    SQL = SQL & "kode_perusahaan= '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_unik = '" & Label1.Text & "' and "
                    SQL = SQL & "kode_stock_owner = '" & LvSO.Trim & "' and "
                    SQL = SQL & "kode_barang = '" & LvKB.Trim & "'"
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    SQL = "insert into promo_sementara(kode_perusahaan, kode_unik, kode_stock_owner, kode_barang, "
                    SQL = SQL & "jumlah) values('" & KodePerusahaan & "', '" & Label1.Text & "', "
                    SQL = SQL & "'" & LvSO.Trim & "', '" & LvKB.Trim & "', " & HilangkanTanda(LvJml) & ")"
                    ExecuteTrans(SQL)
                End If
            End Using
        Next

        ' If kodepromo <> "NULL" Then
        Dim hitung1 As Double = 0

        SQL = "select c.kode_promo, b.kode_x, c.tanda, c.jumlah, "
        SQL = SQL & "isnull(("
        SQL = SQL & "select sum(x.jumlah) from promo_sementara x where x.kode_perusahaan = a.kode_perusahaan and "
        SQL = SQL & "x.kode_stock_owner = c.kode_stock_owner and x.kode_barang = c.kode_barang and "
        SQL = SQL & "x.kode_unik = '" & Label1.Text & "'"
        SQL = SQL & "), 0) as sementara "
        SQL = SQL & "from promo a, detail_promo1 b, detail_promo2 c where "
        SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And b.kode_perusahaan = c.kode_perusahaan And "
        SQL = SQL & "a.kode_promo = b.kode_promo And b.kode_promo = c.kode_promo And "
        SQL = SQL & "b.kode_x = c.kode_x And a.kode_perusahaan = '" & KodePerusahaan & "' and c.Tanda = '+' and "
        SQL = SQL & "a.kode_promo + '/' + b.Kode_X in ("

        SQL = SQL & "select a.kode_promo + '/' + c.Kode_X from promo a, detail_promo2 c, promo_sementara d where "
        SQL = SQL & "a.kode_perusahaan = c.kode_perusahaan And c.kode_perusahaan = d.kode_perusahaan And "
        SQL = SQL & "a.kode_promo = c.kode_promo And c.kode_stock_owner = d.kode_stock_owner and "
        SQL = SQL & "c.kode_barang = d.kode_barang and "
        SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
        SQL = SQL & "a.akhir_promo > '" & Format(tgl_skrg, "yyyy-MM-dd") & "' and "
        SQL = SQL & "a.awal_promo < '" & Format(tgl_skrg, "yyyy-MM-dd") & "' and "
        SQL = SQL & "d.kode_unik = '" & Label1.Text & "'"

        SQL = SQL & ") order by a.kode_promo "
        Dim kdprom As String = ""
        Dim kd_x As String = ""
        Dim hitkdprom As Integer = 0
        Dim ygmemenuhi As Integer = 0
        ' Dim jmlpromosekarang As Integer = 0
        Dim jmlpromosebelumnya As Integer = 0

        Using Ds = BindingTrans(SQL)
            With Ds.Tables("MyTable")
                If .Rows.Count <> 0 Then
                    For i As Integer = 0 To .Rows.Count - 1
                        If i = 0 Then
                            kdprom = .Rows(i).Item("kode_promo") & "/" & .Rows(i).Item("kode_x")
                            jmlpromosebelumnya = Math.Floor(.Rows(i).Item("sementara") / .Rows(i).Item("jumlah"))
                        End If

                        'If kdprom = .Rows(i).Item("kode_promo") & "/" & .Rows(i).Item("kode_x") Then
                        hitkdprom = hitkdprom + 1

                        If Math.Floor(.Rows(i).Item("sementara") / .Rows(i).Item("jumlah")) >= 1 Then
                            If jmlpromosebelumnya > Math.Floor(.Rows(i).Item("sementara") / .Rows(i).Item("jumlah")) Then
                                jmlpromosebelumnya = Math.Floor(.Rows(i).Item("sementara") / .Rows(i).Item("jumlah"))
                            End If

                            ygmemenuhi = ygmemenuhi + 1
                        End If

                        If i <> .Rows.Count - 1 Then
                            If kdprom <> .Rows(i + 1).Item("kode_promo") & "/" & .Rows(i + 1).Item("kode_x") Then
                                If hitkdprom = ygmemenuhi Then
                                    arrKDpromo.Add(.Rows(i).Item("kode_promo"))
                                    arrKDXpromo.Add(.Rows(i).Item("kode_x"))
                                    arrJmlpromo.Add(jmlpromosebelumnya)
                                End If

                                kdprom = .Rows(i + 1).Item("kode_promo") & "/" & .Rows(i + 1).Item("kode_x")
                                hitkdprom = 0
                                ygmemenuhi = 0
                                jmlpromosebelumnya = Math.Floor(.Rows(i + 1).Item("sementara") / .Rows(i + 1).Item("jumlah"))
                            End If
                        Else
                            If hitkdprom = ygmemenuhi Then
                                arrKDpromo.Add(.Rows(i).Item("kode_promo"))
                                arrKDXpromo.Add(.Rows(i).Item("kode_x"))
                                arrJmlpromo.Add(jmlpromosebelumnya)
                            End If
                        End If
                    Next
                End If
            End With
        End Using

        For i As Integer = 0 To arrKDpromo.Count - 1
            SQL = "select a.jenis_promo, a.diskon, b.kode_stock_owner, b.kode_barang, c.nama, "
            SQL = SQL & "c.pakai_sn, b.jumlah, c.satuan from detail_promo1 a, detail_promo2 b, barang c where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and "
            SQL = SQL & "a.kode_promo = b.kode_promo and a.kode_x = b.kode_x and "
            SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.kode_barang = c.kode_barang and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.kode_promo = '" & arrKDpromo.Item(i) & "' and "
            SQL = SQL & "a.kode_x = '" & arrKDXpromo.Item(i) & "' and b.tanda = '='"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    If Dr("jenis_promo") = "1" Then 'beli 1 a + 4 b + 3 c dapet bonus 2 d
                        lv = listview1.Items.Add(Dr("kode_stock_owner"))
                        lv.SubItems.Add(Dr("kode_barang"))
                        lv.SubItems.Add(Dr("nama"))
                        If Dr("pakai_sn") = "Y" Then
                            lv.SubItems.Add("xxxxxxx")
                        Else
                            lv.SubItems.Add("-")
                        End If
                        lv.SubItems.Add("0")
                        lv.SubItems.Add(TampilanDesimal(Dr("jumlah")))
                        lv.SubItems.Add(Dr("satuan"))
                        lv.SubItems.Add("0")
                        lv.SubItems.Add("0")
                        lv.SubItems.Add("0")
                        lv.SubItems.Add("0")
                        lv.SubItems.Add(user_harga_min)
                        If Dr("pakai_sn") = "Y" Then
                            lv.SubItems.Add("Y")
                        Else
                            lv.SubItems.Add("T")
                        End If
                        lv.SubItems.Add("Y")

                        Do While Dr.Read
                            lv = listview1.Items.Add(Dr("kode_stock_owner"))
                            lv.SubItems.Add(Dr("kode_barang"))
                            lv.SubItems.Add(Dr("nama"))
                            If Dr("pakai_sn") = "Y" Then
                                lv.SubItems.Add("xxxxxxx")
                            Else
                                lv.SubItems.Add("-")
                            End If
                            lv.SubItems.Add("0")
                            lv.SubItems.Add(TampilanDesimal(Dr("jumlah")))
                            lv.SubItems.Add(Dr("satuan"))
                            lv.SubItems.Add("0")
                            lv.SubItems.Add("0")
                            lv.SubItems.Add("0")
                            lv.SubItems.Add("0")
                            lv.SubItems.Add(UserID)
                            If Dr("pakai_sn") = "Y" Then
                                lv.SubItems.Add("Y")
                            Else
                                lv.SubItems.Add("T")
                            End If
                            lv.SubItems.Add("Y")
                        Loop
                    ElseIf Dr("jenis_promo") = "2" Then ' beli 3 c + 4 a dapet diskon
                        diskonbaru = diskonbaru + (Dr("diskon") * arrJmlpromo.Item(i))
                    End If
                Loop
            End Using
        Next

        TextBox5.Text = Format(diskonbaru, "N0")
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        TextBox5.Text = "0"
        arrKDpromo.Clear() : arrKDXpromo.Clear() : arrJmlpromo.Clear()

        For i As Integer = listview1.Items.Count - 1 To 0 Step -1
            If listview1.Items(i).SubItems(13).Text = "Y" Then
                listview1.Items(i).Remove()
            End If
        Next

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            Cek_Promo()

            Cmd.Transaction.Commit()

            CloseConn()
        Catch ex As Exception
            TextBox5.Text = "0"
            For i As Integer = listview1.Items.Count - 1 To 0 Step -1
                If listview1.Items(i).SubItems(13).Text = "Y" Then
                    listview1.Items(i).Remove()
                End If
            Next

            HitungGrandTotal()

            CloseConn()
            MessageBox.Show("Proses perhitungan promo gagal!!! Ulangi transasksi ini lagi atau hubungi administrator!!!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        HitungGrandTotal()
    End Sub

    Private Sub nm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nm.TextChanged
        If kd.Text.Trim.Length <> 0 Then
            Exit Sub
        End If

        If nm.Text.Trim.Length = 0 Then
            ListView2.Visible = False : Exit Sub
        Else
            ListView2.Visible = True
        End If

        xxxxx("nm")
    End Sub

    Private Sub TextBox7_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox7.GotFocus
        TextBox7.Focus()
        TextBox7.SelectionStart = 0
        TextBox7.SelectionLength = 5
    End Sub

    Private Sub TextBox7_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox7.KeyDown
        If e.KeyCode = Keys.Up Then
            disc.Focus()
        End If
    End Sub

    Private Sub TextBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox7.KeyPress
        If e.KeyChar = Chr(13) Then
            TextBox8.Focus()
            TextBox8.SelectionStart = 0
            TextBox8.SelectionLength = 5
        End If
        If Not ((e.KeyChar >= Chr(Asc("A")) And e.KeyChar <= Chr(Asc("Z"))) Or (e.KeyChar >= Chr(Asc("a")) And e.KeyChar <= Chr(Asc("z"))) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged
        If TextBox7.Text.Trim.Length = 1 Then
            TextBox8.Focus()
        End If
    End Sub

    Private Sub TextBox8_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox8.GotFocus
        TextBox8.Focus()
        TextBox8.SelectionStart = 0
        TextBox8.SelectionLength = 5
    End Sub

    Private Sub TextBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyDown
        If e.KeyCode = Keys.Up Then
            TextBox7.Focus()
        End If
    End Sub

    Private Sub TextBox8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox8.KeyPress
        If e.KeyChar = Chr(13) Then TextBox9.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub



    Private Sub TextBox9_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox9.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView20.Items.Count = 0 Then Exit Sub
            ListView20.Focus()
        End If
    End Sub

    Private Sub TextBox9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox9.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox9.Text.Trim.Length = 0 Then
                ListView20.Visible = False : TextBox12.Focus() : Exit Sub
            End If
            TextBox9_Leave(TextBox9, e)
        End If
        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
    End Sub

    Public Sub TextBox9_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox9.Leave
        If TextBox9.Text.Trim.Length = 0 Then
            ListView20.Visible = False : Exit Sub
        Else
            ListView20.Visible = True
        End If
        If ListView20.Focused = True Then Exit Sub

        Try

            OpenConn()

            Using Dr = OpenTrans("select * from marketing where kode_perusahaan = '" & KodePerusahaan & "' and kode_marketing = '" & TextBox9.Text & "' order by kode_marketing")
                If Dr.Read Then
                    TextBox9.Text = Dr("kode_marketing")
                    TextBox12.Text = Dr("nama")
                    ok.Focus()
                Else
                    TextBox9.Text = ""
                    TextBox12.Text = ""
                    TextBox9.Focus()
                End If
                ListView20.Visible = False
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged
        If TextBox9.Text.Trim.Length = 0 Then
            ListView20.Visible = False
            TextBox12.Text = ""
            Exit Sub
        Else
            ListView20.Visible = True
        End If

        Try
            OpenConn()

            Dim lv As New ListViewItem
            ListView20.Items.Clear()

            SQL = "select kode_marketing, nama from marketing where kode_perusahaan = '" & KodePerusahaan & "' and kode_marketing like '%" & TextBox9.Text & "%' order by kode_marketing"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = ListView20.Items.Add(Dr("kode_marketing"))
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

    Private Sub TextBox12_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox12.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView20.Items.Count = 0 Then Exit Sub
            ListView20.Focus()
        End If
    End Sub

    Private Sub TextBox12_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox12.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox9.Text.Trim.Length = 0 Then TextBox12.Text = "" : ListView20.Visible = False ': Exit Sub
            ok.Focus()
        End If
        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox12_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox12.Leave
        If ListView20.Focused = True Then Exit Sub
        TextBox9.Text = "" : TextBox12.Text = ""
    End Sub

    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox12.TextChanged
        If TextBox12.Text.Trim.Length = 0 Then
            ListView20.Visible = False : Exit Sub
        Else
            ListView20.Visible = True
        End If

        Try
            OpenConn()

            Dim lv As New ListViewItem
            ListView20.Items.Clear()

            SQL = "select kode_marketing, nama from marketing where kode_perusahaan = '" & KodePerusahaan & "' and nama like '%" & TextBox12.Text & "%' order by nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = ListView20.Items.Add(Dr("kode_marketing"))
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

    Private Sub ListView20_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView20.DoubleClick
        Dim kode As String = ListView20.FocusedItem.Text
        Dim nama As String = ListView20.FocusedItem.SubItems(1).Text
        TextBox9.Text = kode
        TextBox12.Text = nama
        ListView20.Visible = False
        ok.Focus()
    End Sub

    Private Sub ListView20_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView20.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView20_DoubleClick(ListView20, e)
        End If
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        bersihseluruh()
        kd.Focus()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        TextBox15.Text = ""
    End Sub

    Private Sub Button7_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Label14.Text = "1"
    End Sub

    Private Sub CariBarangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CariBarangToolStripMenuItem.Click
        Button7_Click_1(CariBarangToolStripMenuItem, e)
        kd_TextChanged(CariBarangToolStripMenuItem, e)
    End Sub

    Private Sub listview1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listview1.SelectedIndexChanged

    End Sub

    Private Sub TextBox15_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox15.KeyPress
        If e.KeyChar = Chr(13) Then
            TextBox10.Focus()
        End If
    End Sub

    Public Sub TextBox15_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox15.Leave
        arrDariKolom.Clear()
        arrDariNilai.Clear()
        arrKeNilai.Clear()
        arrDiskon.Clear()
        arrMinimum.Clear()

        x_brg_sdr = 0
        x_brg_org = 0
        x_total_sebelumnya = 0
        Dim kat_cust As String = ""
        Dim f_jt As String = ""
        Dim f_lama_jt As Integer = 0

        Try
            OpenConn()
            'vv
            SQL = "select a.ket_tambahan, a.lokasi_gudang, a.jenis_trans, a.lama_jt, a.flag_harga, a.kode_customer, "
            SQL = SQL & "a.Nama, a.kode_kategori, a.cabang_sendiri from "
            SQL = SQL & "customers a where "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.kode_customer = '" & Trim(TextBox15.Text) & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        TextBox10.Text = .Rows(0).Item("kode_customer")
                        TextBox11.Text = .Rows(0).Item("nama")
                        '  TextBoxGudang.Text = .Rows(0).Item("lokasi_gudang")


                        TextBox15.Text = ""
                        pake_hrg_yg_mana = .Rows(0).Item("flag_harga")

                        f_jt = .Rows(0).Item("jenis_trans")
                        f_lama_jt = .Rows(0).Item("lama_jt")

                        cabang_sendiri = .Rows(0).Item("cabang_sendiri")

                        If General_Class.CekNULL(.Rows(0).Item("ket_tambahan")) = "" Then
                            Label51.Text = ""
                        Else
                            Label51.Text = .Rows(0).Item("ket_tambahan")
                        End If

                        If .Rows(0).Item("cabang_sendiri") = "Y" Then
                            CheckBox3.Checked = False
                            CheckBox3.Enabled = False
                            TextBox22.Enabled = False
                            TextBox22.Text = ""
                            Label47.Text = ""
                        Else
                            CheckBox3.Checked = False
                            CheckBox3.Enabled = True
                            TextBox22.Enabled = True
                            TextBox22.Text = ""
                            Label47.Text = ""
                        End If


                    Else

                        f_lama_jt = 0
                        f_jt = "T"
                        Label51.Text = ""
                        TextBox10.Text = ""
                        TextBox11.Text = ""
                        TextBoxGudang.Text = "" 'vv
                        TextBox11.Focus()
                    End If
                End With

                ListView10.Visible = False
            End Using

            CloseConn()

            OpenConn()

            SQL = "select * from kategori_diskon where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_kategori = '" & kat_cust & "'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    arrDariKolom.Add(Dr("dari_kolom"))
                    arrDariNilai.Add(Dr("dari_nilai"))
                    arrKeNilai.Add(Dr("ke_nilai"))
                    arrDiskon.Add(Dr("diskon"))
                    arrMinimum.Add(Dr("minimum"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        OpenConn()

        If f_jt = "T" Then
            ComboBox2.SelectedIndex = 0
            ComboBox5.SelectedIndex = 0
            ComboBox2.Enabled = False
            ComboBox5.Enabled = False
            DateTimePicker2.Visible = False
        Else
            ComboBox2.SelectedIndex = 1
            ComboBox5.Text = f_lama_jt
            ComboBox2.Enabled = False
            ComboBox5.Enabled = False
            DateTimePicker2.Visible = False
        End If

        CloseConn()

        HitungGrandTotal()
    End Sub

    Private Sub TextBox15_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox15.TextChanged
        'If TextBox15.Text.Trim.Length >= 3 Then
        '    Timer1.Enabled = True
        'Else
        '    Timer1.Enabled = False
        'End If
    End Sub

    Private Sub CaraByrToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CaraByrToolStripMenuItem.Click
        TextBox2.Focus()
    End Sub

    Private Sub TextBox18_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox18.TextChanged

    End Sub

    Private Sub Label31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label31.Click

    End Sub





    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView5.Items.Count = 0 Then Exit Sub
            ListView5.Focus()
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox2.Text.Trim.Length = 0 Then
                ListView5.Visible = False : TextBox3.Focus() : Exit Sub
            End If
            TextBox2_Leave(TextBox2, e)
        End If
        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
    End Sub

    Public Sub TextBox2_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.Leave
        If TextBox2.Text.Trim.Length = 0 Then
            ListView5.Visible = False : Exit Sub
        Else
            ListView5.Visible = True
        End If
        If ListView5.Focused = True Then Exit Sub

        Try

            OpenConn()

            SQL = "select kode_karyawan, nama from karyawan where kode_perusahaan = '" & KodePerusahaan & "' and kode_karyawan = '" & TextBox2.Text & "' and aktif = 'Y' order by kode_karyawan"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TextBox2.Text = Dr("kode_karyawan")
                    TextBox3.Text = Dr("nama")
                    ComboBox2.Focus()
                Else
                    TextBox2.Text = ""
                    TextBox3.Text = ""
                    TextBox2.Focus()
                End If
                ListView5.Visible = False
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If TextBox2.Text.Length >= 3 Then
            If TextBox2.Text.Trim.Length = 0 Then
                ListView5.Visible = False
                TextBox3.Text = ""
                Exit Sub
            Else
                ListView5.Visible = True
            End If

            Try
                OpenConn()

                Dim lv As New ListViewItem
                ListView5.Items.Clear()

                SQL = "select kode_karyawan, nama from karyawan where kode_perusahaan = '" & KodePerusahaan & "' and kode_karyawan like '%" & TextBox2.Text & "%' and aktif = 'Y' order by kode_karyawan"
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        lv = ListView5.Items.Add(Dr("kode_karyawan"))
                        lv.SubItems.Add(Dr("nama"))
                    Loop
                End Using

                CloseConn()

            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        Else
            ListView5.Visible = False
        End If
    End Sub

    Private Sub TextBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView5.Items.Count = 0 Then Exit Sub
            ListView5.Focus()
        End If
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox2.Text.Trim.Length = 0 Then TextBox3.Text = "" : ListView5.Visible = False ': Exit Sub
            ComboBox2.Focus()
        End If
        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox3_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox3.Leave
        If ListView5.Focused = True Then Exit Sub
        TextBox2.Text = "" : TextBox3.Text = ""
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        If TextBox3.Text.Length >= 3 Then
            If TextBox3.Text.Trim.Length = 0 Then
                ListView5.Visible = False : Exit Sub
            Else
                ListView5.Visible = True
            End If

            Try

                OpenConn()

                Dim lv As New ListViewItem
                ListView5.Items.Clear()

                SQL = "select kode_karyawan, nama from karyawan where kode_perusahaan = '" & KodePerusahaan & "' and nama like '%" & TextBox3.Text & "%' and aktif = 'Y' order by nama"
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        lv = ListView5.Items.Add(Dr("kode_karyawan"))
                        lv.SubItems.Add(Dr("nama"))
                    Loop
                End Using

                CloseConn()

            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            ListView5.Visible = False
        End If
    End Sub

    Private Sub ListView5_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView5.DoubleClick
        Dim kode As String = ListView5.FocusedItem.Text
        Dim nama As String = ListView5.FocusedItem.SubItems(1).Text
        TextBox2.Text = kode
        TextBox3.Text = nama
        ListView5.Visible = False
        ComboBox2.Focus()
    End Sub

    Private Sub ListView5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView5.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView5_DoubleClick(ListView5, e)
        End If
    End Sub

    Private Sub FokusToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FokusToolStripMenuItem1.Click
        'TextBox15.Focus()
        'Master_Customers.Label8.Text = "J"
        'Master_Customers.ShowDialog()
    End Sub

    Private Sub TxtFaktur_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtFaktur.TextChanged

    End Sub

    Private Sub TextBox22_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox22.KeyPress
        If e.KeyChar = Chr(13) Then kd.Focus()
    End Sub

    Private Sub TextBox22_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox22.Leave
        x_brg_sdr = 0
        x_brg_org = 0
        x_total_sebelumnya = 0

        If cabang_sendiri = "Y" Then
            TextBox22.Text = ""
            Label47.Text = ""
            TextBox22.Enabled = False
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "select b.subtotal, b.flag_sdr, b.jumlah, grand - nilai_ppn as tot from "
            SQL = SQL & "penjualan a, detail_penjualan b where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.status is null and a.kode_customer = '" & TextBox10.Text.Trim & "' and "
            SQL = SQL & "a.no_faktur = '" & TextBox22.Text.Trim & "' and no_fak_setelahnya is null"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    x_total_sebelumnya = Ds.Tables("MyTable").Rows(0).Item("tot")
                    Label47.Text = "ADA"

                    For i As Integer = 0 To Ds.Tables("MyTable").Rows.Count - 1
                        If Ds.Tables("MyTable").Rows(i).Item("flag_sdr") = "Y" Then
                            x_brg_sdr = x_brg_sdr + Ds.Tables("MyTable").Rows(i).Item("subtotal")
                        Else
                            x_brg_org = x_brg_org + Ds.Tables("MyTable").Rows(i).Item("subtotal")
                        End If
                    Next
                Else
                    Label47.Text = ""
                    TextBox22.Text = ""
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox22_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox22.TextChanged

    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            TextBox22.Text = ""
            TextBox22.Enabled = True
            Label47.Text = ""
        Else
            TextBox22.Text = ""
            TextBox22.Enabled = False
            Label47.Text = ""
        End If
    End Sub

    'Private Sub BalikToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BalikToolStripMenuItem.Click
    '    If TextBox21.Text.ToUpper = "C" Then
    '        Dim kupon As String = InputBox("CARI", "CARI", "CARI")
    '        If kupon <> "" Then
    '            If kupon.ToUpper = "LAINLAIN" Then

    '                fmenu.b027_Click(Me, Nothing)
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Display_Cari_Keluar_Barang_.ShowDialog()
        HitungDiskonCash()
        HitungGrandTotal()
    End Sub


    Private Sub ListView_DO_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView_DO.SelectedIndexChanged
        Try
            If ListView_DO.Items.Count = 0 Then Exit Sub

            OpenConn()

            ListView_DO_Detail.Items.Clear()

            SQL = "Select a.kode_stock_owner, a.Kode_barang, b.nama, a.keterangan, a.jumlah, b.satuan, a.no_urut, "
            SQL = SQL & "isnull((select sum(y.good_stock + y.bad_stock) from "
            SQL = SQL & "retur_penjualan x, detail_r_penjualan y where "
            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_retur_jual = y.no_retur_jual and "
            SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan And "
            SQL = SQL & "x.status is null and y.urut = a.no_urut and x.no_faktur_jual = a.no_faktur), 0) as rtr "
            SQL = SQL & "from detail_penjualan a, barang b where "
            SQL = SQL & "a.kode_perusahaan = b.kode_Perusahaan and a.kode_barang = b.kode_barang and "
            SQL = SQL & "a.kode_stock_owner = b.kode_stock_owner and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & ListView_DO.FocusedItem.Text & "'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView_DO_Detail.Items.Add(Dr("kode_stock_owner"))
                    Lvw.SubItems.Add(Dr("kode_barang"))
                    Lvw.SubItems.Add(Dr("nama"))
                    Lvw.SubItems.Add(Format(Dr("jumlah"), "N0"))
                    Lvw.SubItems.Add(Format(Dr("rtr"), "N0"))
                    Lvw.SubItems.Add(Format(Dr("jumlah") - Dr("rtr"), "N0"))
                    Lvw.SubItems.Add(Dr("satuan"))
                    Lvw.SubItems.Add(Dr("no_urut"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Val(HilangkanTanda(TextBox16.Text)) = 0 Then
            discz.Text = "0"
        End If
    End Sub

    Private Sub TextBox28_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox28.TextChanged

    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        tgl_skrg = Format(DateAdd(DateInterval.Second, 1, tgl_skrg), "yyyy-MM-dd HH:mm:ss")
    End Sub

    Private Sub BudgetingToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BudgetingToolStripMenuItem.Click
        listview1.FocusedItem.SubItems(21).Text = "Y"
    End Sub

End Class
