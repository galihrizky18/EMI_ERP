Public Class EMI_Flever
    Dim fSO As String = ""
    Dim arrInisialFaktur As New ArrayList
    'Dim jml_jual, jml_stock, jml_saran, jml_distributor As Double
    Dim LvSO As String
    Dim LvKBMin As String
    Dim LvNmMin As String
    Dim LvJmlMin As String
    Dim LvKBPlus As String
    Dim LvNmPlus As String
    Dim LvJmlPlus As String
    Dim LvJmlMinimum As String
    Dim LvRV As String
    Dim flever_all As String

    ' Dim boleh_lihat_global As Boolean

    Private Sub cetak()
        Try

            OpenConn()

            SQL = "select kode_perusahaan from EMI_Detail_Flever where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "no_faktur = '" & TxtFaktur.Text.Trim & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Dim CrDoc As New Faktur_Flever 'Nama file CR
                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.PrintOptions.PrinterName = PrinterNameTS
                    CrDoc.RecordSelectionFormula = "{EMI_Detail_Flever.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Detail_Flever.no_faktur} = '" & TxtFaktur.Text.Trim & "'"
                    CrDoc.SummaryInfo.ReportTitle = "Faktur Flever"

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

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvSO = DataGridView1.Rows(No_Index).Cells(0).Value
        LvKBMin = DataGridView1.Rows(No_Index).Cells(1).Value
        LvNmMin = DataGridView1.Rows(No_Index).Cells(2).Value
        LvJmlMin = DataGridView1.Rows(No_Index).Cells(3).Value
        LvKBPlus = DataGridView1.Rows(No_Index).Cells(4).Value
        LvNmPlus = DataGridView1.Rows(No_Index).Cells(5).Value
        LvJmlPlus = DataGridView1.Rows(No_Index).Cells(6).Value
        LvJmlMinimum = DataGridView1.Rows(No_Index).Cells(7).Value
        LvRV = DataGridView1.Rows(No_Index).Cells(8).Value
    End Sub

    ' ListView2.Columns.Add("Stock Owner", 0, HorizontalAlignment.Center)
    '    ListView2.Columns.Add("Kode Barang(-)", 165, HorizontalAlignment.Left)
    '    ListView2.Columns.Add("Nama(-)", 255, HorizontalAlignment.Left)
    '    ListView2.Columns.Add("Stock (-)", 80, HorizontalAlignment.Right)

    ''If boleh_lihat_global = True Then
    ''    ListView2.Columns.Add("Stock (-)", 100, HorizontalAlignment.Right)
    ''Else
    ''    ListView2.Columns.Add("Stock (-)", 0, HorizontalAlignment.Right)
    ''End If
    '    ListView2.Columns.Add("Kode Barang(+)", 155, HorizontalAlignment.Left)
    '    ListView2.Columns.Add("Nama(+)", 255, HorizontalAlignment.Left)
    '    ListView2.Columns.Add("Stock (+)", 80, HorizontalAlignment.Right)
    ''If boleh_lihat_global = True Then
    ''    ListView2.Columns.Add("Stock (+)", 100, HorizontalAlignment.Right)
    ''Else
    ''    ListView2.Columns.Add("Stock (+)", 0, HorizontalAlignment.Right)
    ''End If
    '    ListView2.Columns.Add("Jml #", 0, HorizontalAlignment.Left)
    '    ListView2.Columns.Add("RV", 0, HorizontalAlignment.Left)
    '    ListView2.Columns.Add("Pengali", 0, HorizontalAlignment.Left)

    Private Sub get_no_faktur(ByVal pembayaran As String)
        TxtFaktur.Text = fFlever & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(Tanggal_Sekarang, "MM/yy") & "-" &
                             General_Class.Get_Last_Number2("EMI_Flever", "no_faktur", JumlahDigit,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_faktur,1," & Len(fFlever) + Len(arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex)) + 6 & ")", fFlever & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(Tanggal_Sekarang, "MM/yy"))
    End Sub

    Private Sub HitungGrandTotal()
        Dim tot_qty As Integer = 0

        For i As Integer = 0 To DataGridView1.RowCount - 1
            tot_qty = tot_qty + Val(HilangkanTanda(DataGridView1.Rows(i).Cells(6).Value))
        Next

        Label36.Text = Format(tot_qty, "N0")
    End Sub

    Private Sub bersihsebagian()
        kd.Text = ""
        jml.Text = "1"
        nm.Text = ""
        Label14.Text = "0"
    End Sub

    Private Sub Kosong()
        bersihsebagian()
        fSO = ""
        flever_all = " and b.jumlah_plus = '1' "
        GetTime()

        Try
            OpenConn()

            Cmb_Lokasi.Items.Clear()
            arrInisialFaktur.Clear()

            SQL = "Select kode_stock_owner, flag_default, flag_reseller, kategori_pengganti_reseller, inisial_faktur From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Lokasi.Items.Add(dr("kode_stock_owner"))
                    arrInisialFaktur.Add(dr("inisial_faktur"))
                Loop
            End Using

            Cmb_Lokasi.Text = Lokasi

            SQL = "select Top(1)a.Lokasi_Gudang from EMI_Kategori_Gudang_PerLokasi a,Barang b where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.ID_Kategori_Gudang = b.Id_Kategori_Gudang and "
            SQL = SQL & "a.Kode_Stock_Owner = '" & Cmb_Lokasi.Text & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            fSO = .Rows(i).Item("Lokasi_Gudang")
                        Next
                    End If
                End With
            End Using

            'SQL = "select flag_hide_stock, "
            'SQL = SQL & "ISNULL(("
            'SQL = SQL & "select top(1) 'Y' from role_button a where a.kode_perusahaan = x.kode_perusahaan and "
            'SQL = SQL & "a.userid = '" & UserID & "' and buttonname = 'LIHAT_STOCK'"
            'SQL = SQL & "), 'T') AS boleh_lihat_stock "
            'SQL = SQL & " from stock_owner x where x.kode_perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & "x.kode_stock_owner = '" & ComboBox4.Text & "'"
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        If Dr("flag_hide_stock") = "Y" Then
            '            If Dr("boleh_lihat_stock") = "Y" Then
            '                boleh_lihat_global = True
            '            Else
            '                boleh_lihat_global = False
            '            End If
            '        Else
            '            boleh_lihat_global = True
            '        End If
            '    Else
            '        boleh_lihat_global = False
            '    End If
            'End Using

            get_no_faktur("T")

            If CekButtonRole("flever_all") = "T" Then
                flever_all = " and b.jumlah_plus = '1' "
            Else
                flever_all = ""
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        TextBox1.Text = ""

        Label36.Text = "0"

        DataGridView1.Rows.Clear()
    End Sub

    Private Sub Flever_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Penggajian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Kosong()

        ListView2.Columns.Add("Stock Owner", 0, HorizontalAlignment.Center)
        ListView2.Columns.Add("Kode Barang(-)", 165, HorizontalAlignment.Left) '1
        ListView2.Columns.Add("Nama(-)", 255, HorizontalAlignment.Left) '2
        ListView2.Columns.Add("Stock (-)", 80, HorizontalAlignment.Right) '3
        ListView2.Columns.Add("Kode Barang(+)", 155, HorizontalAlignment.Left) '4
        ListView2.Columns.Add("Nama(+)", 255, HorizontalAlignment.Left) '5
        ListView2.Columns.Add("Stock (+)", 80, HorizontalAlignment.Right) '6
        ListView2.Columns.Add("Jml #", 0, HorizontalAlignment.Left) '7
        ListView2.Columns.Add("RV", 0, HorizontalAlignment.Left) '8
        ListView2.Columns.Add("Pengali", 0, HorizontalAlignment.Left) '9
        ListView2.View = View.Details

        ListView2.Location = New Point(20, 223)
        ListView2.Visible = False
    End Sub

    Private Sub Penggajian_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Label1.Size = New Point(Me.Width, 33)
    End Sub

    Private Sub DataGridView1_AllowUserToDeleteRowsChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.AllowUserToDeleteRowsChanged
        HitungGrandTotal()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        HitungGrandTotal()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If DataGridView1.RowCount = 0 Then kd.Focus() : Exit Sub

        If Cmb_Lokasi.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Lokasi.Focus()
            Exit Sub
        ElseIf TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus()
            Exit Sub
        End If

        GetTime()

        Dim tny As String = MessageBox.Show("Yakin akan disimpan!", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tny = vbNo Then Exit Sub

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur("")

            Dim total_hpp As Double = 0
            Dim total_hpp_real As Double = 0

            Dim flag_opm As String = ""
            Dim flag_opm_utk_cek As String = ""

            SQL = "select flag_opname, buka_flever from stock_owner "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_stock_owner = '" & Cmb_Lokasi.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    flag_opm_utk_cek = Dr("flag_opname")

                    If Dr("flag_opname") = "Y" Then
                        If Dr("buka_flever") = 0 Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show(err_msg_opname, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        ElseIf Dr("buka_flever") > 0 Then
                            Dr.Close()
                            SQL = "update stock_owner set buka_flever = buka_flever - 1 "
                            SQL = SQL & "where kode_perusahaan ='" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & Cmb_Lokasi.Text & "'"
                            ExecuteTrans(SQL)

                            flag_opm = "'Y'"
                        Else
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
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim Id_Transaksi As String = "NULL"

            If flag_opm_utk_cek = "Y" Then

                SQL = "Select Kode_Unik "
                SQL = SQL & "from schedule_opname where Lokasi = '" & Cmb_Lokasi.Text & "' "
                SQL = SQL & "and mulai = 'Y' and selesai = 'T'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Id_Transaksi = "'" & Dr("Kode_Unik") & "'"
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi Kesalahan, Silahkan Refresh!!!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using
            End If

            SQL = "insert into EMI_Flever(kode_perusahaan, no_faktur, tanggal, jam, userid, "
            SQL = SQL & "lokasi, total_jml, Keterangan, flag_opm, XTermx, id_transaksi) values("
            SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
            SQL = SQL & "'" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
            'SQL = SQL & "'" & UserID & "', '" & Cmb_Lokasi.Text & "', "
            SQL = SQL & "'" & UserID & "', '" & fSO & "', "
            SQL = SQL & "'" & HilangkanTanda(Label36.Text) & "', '" & TextBox1.Text.Trim & "', "
            SQL = SQL & "" & flag_opm & ", 'x', " & Id_Transaksi & ")"
            ExecuteTrans(SQL)

            For i As Integer = 0 To DataGridView1.RowCount - 1
                Get_Isi_Listview(i)

                If IsNumeric(LvJmlPlus) = False Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Jumlah yg diisi bukan angka!", Judul, MessageBoxButtons.OK)
                    Exit Sub
                ElseIf Val(HilangkanTanda(LvJmlPlus)) < Val(HilangkanTanda(LvJmlMin)) * Val(HilangkanTanda(LvJmlMinimum)) Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Jumlah yg diisi tidak boleh kurang dari minimum!", Judul, MessageBoxButtons.OK)
                    Exit Sub
                ElseIf Val(HilangkanTanda(LvJmlPlus)) > (Val(HilangkanTanda(LvJmlMin)) * Val(HilangkanTanda(LvJmlMinimum))) + 1 Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Jumlah yg diisi tidak boleh lebih dari minimum!", Judul, MessageBoxButtons.OK)
                    Exit Sub
                End If

                SQL = "insert into EMI_Detail_Flever(kode_perusahaan, no_faktur, kode_stock_owner_min, "
                SQL = SQL & "kode_barang_min, jumlah_min, kode_stock_owner_plus, kode_barang_plus, jumlah_plus)"
                SQL = SQL & "values('" & KodePerusahaan & "', "
                SQL = SQL & "'" & TxtFaktur.Text.Trim & "', "
                SQL = SQL & "'" & LvSO & "', "
                SQL = SQL & "'" & LvKBMin & "', "
                SQL = SQL & "'" & HilangkanTanda(LvJmlMin) & "', "
                SQL = SQL & "'" & LvSO & "', "
                SQL = SQL & "'" & LvKBPlus & "', "
                SQL = SQL & "'" & HilangkanTanda(LvJmlPlus) & "')"
                ExecuteTrans(SQL)

                Dim x_no_urut_det_penj As Integer = 0
                SQL = "select IDENT_CURRENT('EMI_Detail_Flever') as urutan"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        x_no_urut_det_penj = Dr("urutan")
                    End If
                End Using

                SQL = "select good_stock from barang where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                SQL = SQL & "kode_barang = '" & LvKBMin & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Dr("good_stock") - HilangkanTanda(LvJmlMin) < 0 Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses membuat stock menjadi negatif untuk barang " & LvNmMin & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        Else
                            Dr.Close()
                            SQL = "Update barang set good_stock = good_stock - " & HilangkanTanda(LvJmlMin) & " where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                            SQL = SQL & "kode_barang = '" & LvKBMin & "'"
                            ExecuteTrans(SQL)
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        Exit Sub
                    End If
                End Using

                SQL = "select good_stock from barang where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                SQL = SQL & "kode_barang = '" & LvKBPlus & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        SQL = "Update barang set good_stock = good_stock + " & HilangkanTanda(LvJmlPlus) & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                        SQL = SQL & "kode_barang = '" & LvKBPlus & "'"
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        Exit Sub
                    End If
                End Using

                SQL = "select cast(rv as bigint) as rvx from EMI_Master_Flever where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner_min = '" & LvSO & "' and "
                SQL = SQL & "kode_barang_min = '" & LvKBMin & "' and "
                SQL = SQL & "kode_stock_owner_plus = '" & LvSO & "' and "
                SQL = SQL & "kode_barang_plus = '" & LvKBPlus & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        If dr("rvx") <> LvRV Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Sudah ada perubahan di master flever!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data master flever tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                Dim sisa As Double = 0
                Dim sisa_plus As Double = 0

                SQL = "select kode_stock_owner, kode_barang, serial_number, jumlah, Tgl_Produksi, Tgl_Expired from barang_sn where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                SQL = SQL & "kode_barang = '" & LvKBMin & "' and jumlah <> 0 "
                SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            sisa = HilangkanTanda(LvJmlMin)
                            sisa_plus = HilangkanTanda(LvJmlPlus)

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

                                    Dim modal_satuan As Double = Math.Floor((Get_Harga_SN(.Rows(h).Item("serial_number")) * sisa) / sisa_plus)
                                    total_hpp = total_hpp + (sisa_plus * modal_satuan)
                                    total_hpp_real = total_hpp_real + (sisa * Get_Harga_SN(.Rows(h).Item("serial_number")))

                                    Dim Rand As New Random
                                    Dim Kode_Unik As String = Format(Rand.Next(0, 999), "000") & Format(Tanggal_Sekarang, "HHmmss")
                                    Dim SN As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & modal_satuan & Tanda_SN & "02" & Tanda_SN & Format(Tanggal_Sekarang, "yyyy-MM-dd")

                                    SQL = "select kode_barang from barang_sn where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                                    SQL = SQL & "kode_barang = '" & LvKBPlus & "' and serial_number = '" & SN & "'"
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi kesalahan pada Barang SN ! Ulangi Transaksi !")
                                            Exit Sub
                                            'SQL = "Update barang_sn set jumlah = jumlah + " & sisa_plus & " where "
                                            'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            'SQL = SQL & "kode_stock_owner = '" & LvSO & "' and kode_barang = '" & LvKBPlus & "' and "
                                            'SQL = SQL & "serial_number = '" & SN & "'"
                                            'ExecuteTrans(SQL)
                                        Else
                                            SQL = "insert into barang_sn(kode_perusahaan, kode_stock_owner, kode_barang, "
                                            SQL = SQL & "serial_number, jumlah, Tgl_Produksi, Tgl_Expired) values('" & KodePerusahaan & "', "
                                            SQL = SQL & "'" & LvSO & "', '" & LvKBPlus & "', "
                                            SQL = SQL & "'" & SN & "', " & sisa_plus & ", '" & .Rows(h).Item("Tgl_Produksi") & "', '" & .Rows(h).Item("Tgl_Expired") & "')"
                                            Dr.Close()
                                            ExecuteTrans(SQL)
                                        End If
                                    End Using

                                    SQL = "insert into EMI_Det_Flever(kode_perusahaan, no_faktur, "
                                    SQL = SQL & "kode_stock_owner_min, kode_barang_min, serial_number_min, "
                                    SQL = SQL & "no_urut, jumlah_min, kode_stock_owner_plus, "
                                    SQL = SQL & "kode_barang_plus, serial_number_plus, jumlah_plus) values("
                                    SQL = SQL & "'" & KodePerusahaan & "', "
                                    SQL = SQL & "'" & TxtFaktur.Text.Trim & "', "
                                    SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
                                    SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
                                    SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
                                    SQL = SQL & "" & x_no_urut_det_penj & ", '" & sisa & "', "
                                    SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
                                    SQL = SQL & "'" & LvKBPlus & "', '" & SN & "', '" & sisa_plus & "')"
                                    ExecuteTrans(SQL)

                                    sisa = 0
                                    sisa_plus = 0
                                ElseIf sisa > .Rows(h).Item("jumlah") Then

                                    Dim modal_satuan As Double = Math.Floor((Get_Harga_SN(.Rows(h).Item("serial_number")) * .Rows(h).Item("jumlah")) / (Val(HilangkanTanda(LvJmlMinimum)) * .Rows(h).Item("jumlah")))

                                    total_hpp = total_hpp + (Val(HilangkanTanda(LvJmlMinimum)) * .Rows(h).Item("jumlah") * modal_satuan)
                                    total_hpp_real = total_hpp_real + (.Rows(h).Item("jumlah") * Get_Harga_SN(.Rows(h).Item("serial_number")))

                                    Dim Rand As New Random
                                    Dim Kode_Unik As String = Format(Rand.Next(0, 999), "000") & Format(Tanggal_Sekarang, "HHmmss")
                                    Dim SN As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & modal_satuan & Tanda_SN & "02" & Tanda_SN & Format(Tanggal_Sekarang, "yyyy-MM-dd")

                                    SQL = "select kode_barang from barang_sn where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                                    SQL = SQL & "kode_barang = '" & LvKBPlus & "' and serial_number = '" & SN & "'"
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi kesalahan pada Barang SN ! Ulangi Transaksi !")
                                            Exit Sub
                                            'SQL = "Update barang_sn set jumlah = jumlah + " & (Val(HilangkanTanda(LvJmlMinimum)) * .Rows(h).Item("jumlah")) & " where "
                                            'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            'SQL = SQL & "kode_stock_owner = '" & LvSO & "' and kode_barang = '" & LvKBPlus & "' and "
                                            'SQL = SQL & "serial_number = '" & SN & "'"
                                            'ExecuteTrans(SQL)
                                        Else
                                            SQL = "insert into barang_sn(kode_perusahaan, kode_stock_owner, kode_barang, "
                                            SQL = SQL & "serial_number, jumlah, Tgl_Produksi, Tgl_Expired) values('" & KodePerusahaan & "', "
                                            SQL = SQL & "'" & LvSO & "', '" & LvKBPlus & "', "
                                            SQL = SQL & "'" & SN & "', " & (Val(HilangkanTanda(LvJmlMinimum)) * .Rows(h).Item("jumlah")) & ", '" & .Rows(h).Item("Tgl_Produksi") & "', '" & .Rows(h).Item("Tgl_Expired") & "')"
                                            Dr.Close()
                                            ExecuteTrans(SQL)
                                        End If
                                    End Using

                                    SQL = "insert into EMI_Det_Flever(kode_perusahaan, no_faktur, "
                                    SQL = SQL & "kode_stock_owner_min, kode_barang_min, serial_number_min, "
                                    SQL = SQL & "no_urut, jumlah_min, kode_stock_owner_plus, "
                                    SQL = SQL & "kode_barang_plus, serial_number_plus, jumlah_plus) values("
                                    SQL = SQL & "'" & KodePerusahaan & "', "
                                    SQL = SQL & "'" & TxtFaktur.Text.Trim & "', "
                                    SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
                                    SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
                                    SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
                                    SQL = SQL & "" & x_no_urut_det_penj & ", '" & .Rows(h).Item("jumlah") & "', "
                                    SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
                                    SQL = SQL & "'" & LvKBPlus & "', '" & SN & "', '" & (Val(HilangkanTanda(LvJmlMinimum)) * .Rows(h).Item("jumlah")) & "')"
                                    ExecuteTrans(SQL)


                                    'SQL = "insert into det_flever(kode_perusahaan, no_faktur, "
                                    'SQL = SQL & "kode_stock_owner, kode_barang, serial_number, no_urut, "
                                    'SQL = SQL & "jumlah) values('" & KodePerusahaan & "', "
                                    'SQL = SQL & "'" & TxtFaktur.Text.Trim & "', "
                                    'SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
                                    'SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
                                    'SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
                                    'SQL = SQL & "" & x_no_urut_det_penj & ", "
                                    'SQL = SQL & "'" & .Rows(h).Item("jumlah") & "')"
                                    'ExecuteTrans(SQL)

                                    SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                                    SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                                    SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                                    ExecuteTrans(SQL)



                                    sisa_plus = sisa_plus - (Val(HilangkanTanda(LvJmlMinimum)) * .Rows(h).Item("jumlah"))
                                    sisa = sisa - .Rows(h).Item("jumlah")

                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang SN terjadi kesalahan untuk barang " & LvNmMin & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                                If sisa <> 0 And h = .Rows.Count - 1 Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Jumlah stock tidak mencukupi untuk barang " & LvNmMin & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            Next
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("SN untuk barang " & LvNmMin & " tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                    End With
                End Using

            Next

            If total_hpp_real - total_hpp <> 0 Then
                Dim pagenumber As Integer = 0

                Get_Data_Acc()
                Dim Kode_Voucher As String = GetLastNumberJurnal(Format(Tanggal_Sekarang, "yyyyMM"), fJU & arrInisialFaktur(Cmb_Lokasi.SelectedIndex), KodePerusahaan)

                pagenumber = 1

                SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                SQL = SQL & "'" & Kode_Voucher & "', "
                SQL = SQL & "'" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                SQL = SQL & "'" & KodeProyek & "', 'Flever " & TxtFaktur.Text.Trim & "', '', "
                ''SQL = SQL & "'-', '" & UserID & "', '" & Cmb_Lokasi.Text & "')"
                SQL = SQL & "'-', '" & UserID & "', '" & fSO & "')"
                ExecuteTrans(SQL)

                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(X_Biaya_Flever, 1),
                              Strings.Mid(X_Biaya_Flever, 2, 1),
                              Strings.Mid(Ganti(X_Biaya_Flever), 3),
                              KodePerusahaan, KodeProyek, "Biaya Flever " & TxtFaktur.Text.Trim, total_hpp_real - total_hpp, "0", pagenumber)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(X_Persediaan, 1),
                              Strings.Mid(X_Persediaan, 2, 1),
                              Strings.Mid(Ganti(X_Persediaan), 3),
                              KodePerusahaan, KodeProyek, "Biaya Flever " & TxtFaktur.Text.Trim, "0", total_hpp_real - total_hpp, pagenumber)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                SQL = "update EMI_Flever set kode_voucher_1 = '" & Kode_Voucher & "' where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_faktur = '" & TxtFaktur.Text.Trim & "'"
                ExecuteTrans(SQL)
            End If

            Cmd.Transaction.Commit()

            CloseConn()

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Dim Tanya_Cetak As String = MessageBox.Show("Cetak Faktur?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If Tanya_Cetak = vbYes Then
            cetak()
        End If

        Kosong()
        TextBox1.Focus()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Kosong()
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

            'iniiiii
            Dim boleh_lihat As Boolean

            SQL = "select flag_hide_stock, "
            SQL = SQL & "ISNULL(("
            SQL = SQL & "select top(1) 'Y' from role_button a where a.kode_perusahaan = x.kode_perusahaan and "
            SQL = SQL & "a.userid = '" & UserID & "' and buttonname = 'LIHAT_STOCK'"
            SQL = SQL & "), 'T') AS boleh_lihat_stock "
            SQL = SQL & " from stock_owner x where x.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "x.kode_stock_owner = '" & Cmb_Lokasi.Text & "'"
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

            ListView2.Items.Clear()

            SQL = "Select top(25) cast(b.rv as bigint) as rvx, b.Pengali_Jml_Minimum, b.jumlah_plus, a.kode_stock_owner, a.kode_barang as kd_min, a.nama as nm_min, "
            SQL = SQL & "c.kode_barang as kd_plus, c.nama as nm_plus, a.good_stock as stock_min, c.good_stock as stock_plus From "
            SQL = SQL & "barang a, EMI_Master_Flever b, barang c where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and "
            SQL = SQL & "a.Kode_Stock_Owner = b.Kode_Stock_Owner_Min and a.kode_barang = b.kode_barang_min and "
            SQL = SQL & "b.Kode_Stock_Owner_Plus = c.Kode_Stock_Owner and b.kode_barang_plus = c.kode_barang and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
            ''SQL = SQL & "a.kode_stock_owner= '" & Cmb_Lokasi.Text & "' and "
            ''SQL = SQL & "c.kode_stock_owner = '" & Cmb_Lokasi.Text & "' and "
            SQL = SQL & "a.kode_stock_owner= '" & fSO & "' and "
            SQL = SQL & "c.kode_stock_owner = '" & fSO & "' and "
            SQL = SQL & "a.aktif = 'Y' and a.jenis = 'B' and "
            SQL = SQL & "a.nama like '%" & isi & "%' " & flever_all
            SQL = SQL & "order by a.nama"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Dim Lvw As ListViewItem
                        Lvw = ListView2.Items.Add(.Rows(i).Item("kode_stock_owner"))
                        Lvw.SubItems.Add(.Rows(i).Item("kd_min"))
                        Lvw.SubItems.Add(.Rows(i).Item("nm_min"))
                        'iniiiii
                        If boleh_lihat = True Then
                            Lvw.SubItems.Add(Format(.Rows(i).Item("stock_min"), "N0"))
                        Else
                            Lvw.SubItems.Add(" ")
                        End If
                        'iniiiii
                        Lvw.SubItems.Add(.Rows(i).Item("kd_plus"))
                        Lvw.SubItems.Add(.Rows(i).Item("nm_plus"))
                        'iniiiii
                        If boleh_lihat = True Then
                            Lvw.SubItems.Add(Format(.Rows(i).Item("stock_plus"), "N0"))
                        Else
                            Lvw.SubItems.Add(" ")
                        End If
                        'iniiiii
                        Lvw.SubItems.Add(.Rows(i).Item("jumlah_plus"))
                        Lvw.SubItems.Add(.Rows(i).Item("rvx"))
                        Lvw.SubItems.Add(.Rows(i).Item("Pengali_Jml_Minimum"))
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
        If e.KeyChar = Chr(13) Then
            Label14.Text = "0"
            jml.Focus()
        End If
        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub kd_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles kd.Leave

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

        If Cmb_Lokasi.Text.Trim.Length = 0 Then
            MessageBox.Show("Stock owner harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Lokasi.Focus() : Exit Sub
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
        End If

        Dim input_baru As String = "Y"
        Dim lv As New ListViewItem

        If Val(jml.Text) Mod Val(ListView2.FocusedItem.SubItems(9).Text) <> 0 Then
            MessageBox.Show("Jumlah harus kelipatan " & ListView2.FocusedItem.SubItems(9).Text & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If DataGridView1.RowCount > 0 Then
            For i As Integer = 0 To DataGridView1.RowCount - 1
                Get_Isi_Listview(i)

                If Cmb_Lokasi.Text.Trim.ToUpper = LvSO.ToUpper And ListView2.FocusedItem.SubItems(1).Text.Trim.ToUpper = LvKBMin.ToUpper And ListView2.FocusedItem.SubItems(4).Text.Trim.ToUpper = LvKBPlus.ToUpper Then
                    MessageBox.Show("Barang ini sudah dimasukan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Next
        End If

        Dim no As Integer = DataGridView1.RowCount

        DataGridView1.Rows.Add(1)

        'ListView2.Columns.Add("Stock Owner", 0, HorizontalAlignment.Center)
        'ListView2.Columns.Add("Kode Barang(-)", 165, HorizontalAlignment.Left) '1
        'ListView2.Columns.Add("Nama(-)", 255, HorizontalAlignment.Left) '2
        'ListView2.Columns.Add("Stock (-)", 80, HorizontalAlignment.Right) '3
        'ListView2.Columns.Add("Kode Barang(+)", 155, HorizontalAlignment.Left) '4
        'ListView2.Columns.Add("Nama(+)", 255, HorizontalAlignment.Left) '5
        'ListView2.Columns.Add("Stock (+)", 80, HorizontalAlignment.Right) '6
        'ListView2.Columns.Add("Jml #", 0, HorizontalAlignment.Left) '7
        'ListView2.Columns.Add("RV", 0, HorizontalAlignment.Left) '8
        'ListView2.Columns.Add("Pengali", 0, HorizontalAlignment.Left) '9
        'ListView2.View = View.Details

        'DataGridView1.Rows(no).Cells(0).Value = Cmb_Lokasi.Text
        DataGridView1.Rows(no).Cells(0).Value = fSO
        DataGridView1.Rows(no).Cells(1).Value = ListView2.FocusedItem.SubItems(1).Text.Trim.ToUpper
        DataGridView1.Rows(no).Cells(2).Value = ListView2.FocusedItem.SubItems(2).Text.Trim.ToUpper
        DataGridView1.Rows(no).Cells(3).Value = Format(Val(jml.Text), "N0")
        DataGridView1.Rows(no).Cells(4).Value = ListView2.FocusedItem.SubItems(4).Text.Trim.ToUpper
        DataGridView1.Rows(no).Cells(5).Value = ListView2.FocusedItem.SubItems(5).Text.Trim.ToUpper
        DataGridView1.Rows(no).Cells(6).Value = ""
        DataGridView1.Rows(no).Cells(7).Value = ListView2.FocusedItem.SubItems(7).Text.Trim.ToUpper
        DataGridView1.Rows(no).Cells(8).Value = ListView2.FocusedItem.SubItems(8).Text.Trim.ToUpper
        HitungGrandTotal()

        bersihsebagian()
        kd.Focus()
    End Sub

    Private Sub ListView2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView2.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView2_DoubleClick(ListView2, e)
        End If
    End Sub

    Private Sub CariToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CariToolStripMenuItem.Click
        Button7_Click_1(CariToolStripMenuItem, e)
        kd_TextChanged(CariToolStripMenuItem, e)
    End Sub

    Private Sub Button7_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Label14.Text = "1"
    End Sub

    Private Sub DataGridView1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DataGridView1.RowsRemoved
        HitungGrandTotal()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then kd.Focus()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

End Class