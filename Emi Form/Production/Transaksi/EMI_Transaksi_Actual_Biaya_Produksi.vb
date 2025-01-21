Imports System.Web.UI.WebControls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button


Public Class EMI_Transaksi_Actual_Biaya_Produksi
    Dim arrcari As New ArrayList
    Dim Jenis = "Binding_Barcode"
    Public urut As Integer
    Dim Kd_Brg_Sampel As String

    Dim arrJenisBiaya, arrSatuan, arrMesin, arrLokasi As New ArrayList


    Private Sub Transaksi_Binding_Barcode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        get_jam()
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)


            Label1.Text = "Transaksi - Actual Biaya Produksi"
            Label2.Text = Base_Language.Lang_Global_No_Transaksi
            Label4.Text = Base_Language.Lang_Global_Jumlah
            Label5.Text = Base_Language.Lang_Global_Satuan


            get_no_faktur()


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub


    Private Sub get_no_faktur()
        TxtFaktur.Text = fab & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("emi_actual_biaya_produksi", "no_faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_faktur, 1, " & Len(fab) + 4 & ")", fab & Format(tgl_skg, "MMyy"))
    End Sub


    Private Sub kosong()

        get_jam()

        Try

            OpenConn()

            get_no_faktur()

            arrLokasi.Clear()

            DtpTanggal.ResetText()
            txtJumlah.Text = ""
            txtSatuan.Text = ""
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Txt_KdBarang.Enabled = False
            Txt_NmBarang.Enabled = False


            Cmb_Lokasi.Text = ""
            Cmb_Lokasi.Items.Clear()

            cmbJenisBiaya.Items.Clear() : arrJenisBiaya.Clear() : arrSatuan.Clear()
            SQL = "select id_jenis_biaya_produksi,keterangan,satuan from Emi_Jenis_Biaya_Produksi where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & "order by keterangan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    cmbJenisBiaya.Items.Add(Dr("keterangan")) : arrJenisBiaya.Add(Dr("id_jenis_biaya_produksi"))
                    arrSatuan.Add(Dr("satuan"))
                Loop
            End Using

            cmbMesin.Items.Clear() : arrMesin.Clear()
            SQL = "select id_work_center,keterangan from EMI_Master_Work_Center where  "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & "order by keterangan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    cmbMesin.Items.Add(Dr("Keterangan")) : arrMesin.Add(Dr("Id_Work_Center"))
                Loop
            End Using


            cmbStockOwner.Items.Clear()
            cmbStockOwner.Items.Add("-- Seluruh --")
            xSplit = CekKotaRole().Split(",")

            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and kode_kota in("
            For i As Integer = 0 To xSplit.Count - 1
                SQL = SQL & "'" & xSplit(i).Trim & "', "
            Next

            SQL = Strings.Left(SQL, Len(SQL) - 2)

            SQL = SQL & ") "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    cmbStockOwner.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using
            cmbStockOwner.Text = Lokasi


            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub cmbJenisBiaya_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbJenisBiaya.KeyPress
        If e.KeyChar = Chr(13) Then
            cmbMesin.Focus()
        End If
    End Sub

    Private Sub cmbMesin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbMesin.KeyPress
        If e.KeyChar = Chr(13) Then
            txtJumlah.Focus()
        End If
    End Sub

    Private Sub cmbJenisBiaya_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbJenisBiaya.SelectedIndexChanged
        txtSatuan.Text = arrSatuan.Item(cmbJenisBiaya.SelectedIndex)

        Try
            OpenConn()

            SQL = "select a.Id_Jenis_Biaya_Produksi, a.Flag_Potong_Stock, a.Kode_Barang, b.nama as Nama_Barang From Emi_Jenis_Biaya_Produksi a, barang b "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.flag_potong_stock = 'Y' and a.Id_Jenis_Biaya_Produksi = '" & arrJenisBiaya(cmbJenisBiaya.SelectedIndex) & "' "
            SQL = SQL & "group by a.Id_Jenis_Biaya_Produksi, a.Flag_Potong_Stock, a.Kode_Barang, b.nama "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            '   Txt_KdBarang.Enabled = False
                            '  Txt_NmBarang.Enabled = True

                            Txt_KdBarang.Text = .Rows(i).Item("Kode_Barang")
                            Txt_NmBarang.Text = .Rows(i).Item("Nama_Barang")

                            Cmb_Lokasi.Enabled = True
                            Cmb_Lokasi.Items.Clear()
                            arrLokasi.Clear()
                            '======================
                            '=     GET LOKASI     =
                            '======================
                            SQL = "select Kode_Stock_Owner, Keterangan from Stock_Owner_Gudang where Kode_Perusahaan = '" & KodePerusahaan & "'"
                            Using Dr = OpenTrans(SQL)
                                Do While Dr.Read
                                    Cmb_Lokasi.Items.Add(Dr("Keterangan")) : arrLokasi.Add(Dr("Kode_Stock_Owner"))
                                Loop
                            End Using

                        Next

                    Else

                        Txt_KdBarang.Text = ""
                        Txt_NmBarang.Text = ""

                        Txt_KdBarang.Enabled = False
                        Txt_NmBarang.Enabled = False

                        Cmb_Lokasi.Enabled = False
                        Cmb_Lokasi.Items.Clear()
                    End If
                End With
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub btnKosong_Click(sender As Object, e As EventArgs) Handles btnKosong.Click
        kosong()
    End Sub

    Private Sub txtJumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJumlah.KeyPress
        If e.KeyChar = Chr(13) Then
            Btn_Simpan.Focus()
        End If
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If cmbJenisBiaya.Text.Trim.Length = 0 Then
            MessageBox.Show("Jenis Biaya harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf cmbMesin.Text.Trim.Length = 0 Then
            MessageBox.Show("Work Center harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf txtSatuan.Text.Trim.Length = 0 Then
            MessageBox.Show("Satuan harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf txtJumlah.Text.Trim.Length = 0 Then
            MessageBox.Show("Work Center harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim isPotongStock = False

            '==================================================
            '=     CEK APAKAH JENIS BAIAYA = POTONG STOCK     =
            '==================================================
            SQL = "select Id_Jenis_Biaya_Produksi, Flag_Potong_Stock, Kode_Barang, Nama_Barang "
            SQL = SQL & "from Emi_Jenis_Biaya_Produksi "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Id_Jenis_Biaya_Produksi = '" & arrJenisBiaya(cmbJenisBiaya.SelectedIndex) & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("Flag_Potong_Stock")) = "Y" Then
                        If Cmb_Lokasi.SelectedIndex = -1 Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Lokasi Harus di Isi Terlebih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                        isPotongStock = True
                    Else
                        isPotongStock = False
                    End If

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Jenis Biaya tidak diTemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '========================
            '=     POTONG STOCK     =
            '========================

            If isPotongStock Then
                Dim convertKeSatuanAsli_bhn As String = ""
                Dim jumlahConvertBhn As Double = 0

                SQL = "select satuan From barang where Kode_barang = '" & Txt_KdBarang.Text.Trim & "' "
                SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Cmb_Lokasi.Text & "' "
                Using Dr3 = OpenTrans(SQL)
                    If Dr3.Read Then
                        convertKeSatuanAsli_bhn = Dr3("satuan")
                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Txt_KdBarang.Text.Trim & "',"
                        SQL = SQL & "'" & txtSatuan.Text.Trim & "','" & Dr3("satuan") & "',"
                        SQL = SQL & "" & txtJumlah.Text.Trim & ") as Hasil "
                        Dr3.Close()

                        Using dr4 = OpenTrans(SQL)
                            If dr4.Read Then
                                If General_Class.CekNULL(dr4("Hasil")) <> "" Then
                                    If dr4("Hasil") = 0 Then
                                        dr4.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Satuan " & txtSatuan.Text & " Ke " & convertKeSatuanAsli_bhn & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    Else
                                        jumlahConvertBhn = dr4("hasil")

                                    End If
                                Else
                                    dr4.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Satuan " & txtSatuan.Text & " Ke " & convertKeSatuanAsli_bhn & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If
                        End Using
                    Else
                        Dr3.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                '========================
                '=     POTONG STOCK barang     =
                '========================

                SQL = "select round(good_stock,2) as good_stock, flag_ppn from barang where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & Cmb_Lokasi.Text & "' and "
                SQL = SQL & "kode_barang = '" & Txt_KdBarang.Text & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            If .Rows(0).Item("good_stock") - jumlahConvertBhn < BolehNegatif Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Proses membuat stock menjadi negatif untuk kode barang " & Txt_KdBarang.Text & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            Else
                                SQL = "Update barang set good_stock = good_stock - " & jumlahConvertBhn & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_stock_owner = '" & Cmb_Lokasi.Text & "' and "
                                SQL = SQL & "kode_barang = '" & Txt_KdBarang.Text & "'"
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


                '=================================
                '=     POTONG STOCK barang sn    =
                '=================================

                Dim sisa As Double = 0
                SQL = "select kode_stock_owner, kode_barang, serial_number, round(jumlah,2) as jumlah from barang_sn where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & Cmb_Lokasi.Text.Trim & "' and "
                SQL = SQL & "kode_barang = '" & Txt_KdBarang.Text & "' and jumlah <> 0 "
                SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            sisa = Val(jumlahConvertBhn)
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

                                    SQL = "INSERT INTO emi_actual_biaya_produksi_det(Kode_Perusahaan,no_faktur,Kode_Stock_Owner,Kode_Barang,"
                                    SQL = SQL & "jumlah,Serial_Number) VALUES('" & KodePerusahaan & "','" & TxtFaktur.Text & "',"
                                    SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "','" & .Rows(h).Item("kode_barang") & "',"
                                    SQL = SQL & "" & sisa & ",'" & .Rows(h).Item("serial_number") & "')"
                                    ExecuteTrans(SQL)


                                    sisa = 0
                                ElseIf sisa > .Rows(h).Item("jumlah") Then

                                    SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                                    SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                                    SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                                    ExecuteTrans(SQL)

                                    SQL = "INSERT INTO emi_actual_biaya_produksi_det(Kode_Perusahaan,no_faktur,Kode_Stock_Owner,Kode_Barang,"
                                    SQL = SQL & "jumlah,Serial_Number) VALUES('" & KodePerusahaan & "','" & TxtFaktur.Text & "',"
                                    SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "','" & .Rows(h).Item("kode_barang") & "',"
                                    SQL = SQL & "" & .Rows(h).Item("jumlah") & ",'" & .Rows(h).Item("serial_number") & "')"
                                    ExecuteTrans(SQL)


                                    sisa = sisa - .Rows(h).Item("jumlah")


                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang SN terjadi kesalahan untuk kode barang " & Txt_KdBarang.Text.Trim & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                                If sisa <> 0 And h = .Rows.Count - 1 Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Jumlah stock tidak mencukupi untuk kode barang " & Txt_KdBarang.Text.Trim & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                            Next ' for barang sn
                        End If 'count <> 0
                    End With
                End Using


            End If



            SQL = "insert into emi_actual_biaya_produksi(kode_perusahaan,no_faktur,tanggal,jam,iduser,id_jenis_biaya,id_work_center,jumlah,satuan,lokasi, Flag_Potong_Stock, Kode_Stock_Owner, Kode_Barang) values ( "
            SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "',"
            SQL = SQL & " '" & Format(DtpTanggal.Value, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "' ,"
            SQL = SQL & "'" & UserID & "', '" & arrJenisBiaya.Item(cmbJenisBiaya.SelectedIndex) & "',   "
            SQL = SQL & "'" & arrMesin.Item(cmbMesin.SelectedIndex) & "',"
            SQL = SQL & " '" & txtJumlah.Text.Trim & "',  '" & txtSatuan.Text.Trim & "',"
            SQL = SQL & "'" & cmbStockOwner.Text & "', "

            If isPotongStock Then
                SQL = SQL & "'Y', '" & arrLokasi(Cmb_Lokasi.SelectedIndex) & "', '" & Txt_KdBarang.Text & "'"
            Else
                SQL = SQL & "NULL, NULL, NULL"
            End If

            SQL = SQL & ")"
            ExecuteTrans(SQL)





            Cmd.Transaction.Commit()
            MessageBox.Show("Data berhasil disimpan ", Judul, MessageBoxButtons.OK)
            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


#Region "Cetak Lama"

        '=================
        '=     CETAK     =
        '=================
        'Try
        '    OpenConn()
        '    Dim kertas As String = ""
        '    SQL = "select Kode_Perusahaan from View_Laporan_Actual_Biaya_Produksi where "
        '    SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
        '    ' SQL = SQL & "no_faktur = '" & TxtFaktur.Text & "' "
        '    SQL = SQL & "no_faktur = 'FAB1224-00002' "
        '    Using Ds = BindingTrans(SQL)
        '        If Ds.Tables("MyTable").Rows.Count <> 0 Then


        '            Dim CrDoc = New Rpt_Laporan_Actual_Biaya_Produksi
        '            kertas = "A4"

        '            CrDoc.SetDataSource(Ds)
        '            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
        '            CrDoc.PrintOptions.PrinterName = PrinterNameSPB
        '            CrDoc.RecordSelectionFormula = "{View_Laporan_Actual_Biaya_Produksi.Kode_Perusahaan} = '" & KodePerusahaan & "' and {View_Laporan_Actual_Biaya_Produksi.no_faktur} = 'FAB1224-00002' "
        '            'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

        '            Dim doctoprint As New System.Drawing.Printing.PrintDocument()
        '            doctoprint.PrinterSettings.PrinterName = PrinterNameSPB
        '            Dim rawKind As Integer
        '            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        '            For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
        '                If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
        '                    rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
        '                    CrDoc.PrintOptions.PaperSize = rawKind
        '                    Exit For
        '                End If
        '            Next

        '            CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
        '            CrDoc.PrintToPrinter(1, False, 1, 99)

        '            MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Else
        '            MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        End If
        '    End Using

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
#End Region

        '==============================================
        '=     CETAK FAKTUR ACTUAL BIAYA PRODUKSI     =
        '==============================================
        Try
            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            SQL = "select Kode_Perusahaan from View_Laporan_Actual_Biaya_Produksi "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & TxtFaktur.Text & "' "

            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    CrDoc = New Rpt_Laporan_Actual_Biaya_Produksi
                    kertas = "Faktur"

                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{View_Laporan_Actual_Biaya_Produksi.Kode_Perusahaan} = '" & KodePerusahaan & "' and {View_Laporan_Actual_Biaya_Produksi.No_Faktur}='" & TxtFaktur.Text & "' "
                    '    CrDoc.SummaryInfo.ReportTitle = "TF"
                    '    .Text = "TF"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    '============================================================================================================================================
                    '============================================================================================================================================
                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.PrintOptions.PrinterName = PrinterNameTS
                    CrDoc.RecordSelectionFormula = "{View_Laporan_Actual_Biaya_Produksi.Kode_Perusahaan} = '" & KodePerusahaan & "' and {View_Laporan_Actual_Biaya_Produksi.No_Faktur}='" & TxtFaktur.Text & "' "
                    'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.DefaultPageSettings.Landscape = True
                    doctoprint.PrinterSettings.PrinterName = PrinterNameTS
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

                    MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)


                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try











        kosong()

    End Sub
End Class