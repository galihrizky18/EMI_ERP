Public Class Laporan_Perusahaan_Biaya_Import_lokal

    Dim lv As New ListViewItem

    Private Sub Laporan_Bahan_Tidak_Potong_Stock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        LvSupplier.Visible = False
        LvSupplier.Location = New Point(182, 40)

        get_lokasi()
        get_Kode_Perusahaan_Biaya_Import()
        get_MUA()

    End Sub

    Private Sub BersihSeluruh()
        TxtKdSupplier.Text = "" : TxtNama.Text = ""
        Cmb_MUA.SelectedIndex = -1 : Cmb_Kategori.SelectedIndex = -1 : CmbLokasi.SelectedIndex = -1
        Tgl1.Value = DateTime.Now : Tgl2.Value = DateTime.Now
    End Sub

    Private Sub get_lokasi()
        Try
            OpenConn()
            CmbLokasi.Items.Clear() : CmbLokasi.Items.Add("---SELURUH---")
            xSplit = CekKotaRole().Split(",")
            SQL = "select kode_stock_owner from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "aktif = 'Y' and kode_kota in("
            For i As Integer = 0 To xSplit.Count - 1
                SQL = SQL & "'" & xSplit(i).Trim & "', "
            Next
            SQL = Strings.Left(SQL, Len(SQL) - 2)
            SQL = SQL & ") "
            SQL = SQL & "order by kode_stock_owner"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    CmbLokasi.Items.Add(Dr("kode_stock_owner"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CmbLokasi.Items.Clear()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        CmbLokasi.SelectedIndex = 0
    End Sub

    Private Sub get_Kode_Perusahaan_Biaya_Import()
        Try
            OpenConn()
            Cmb_Kategori.Items.Clear() : Cmb_Kategori.Items.Add("---SELURUH---")
            SQL = "Select Kode_Perusahaan_Biaya_Import From "
            SQL = SQL & "Perusahaan_Biaya_Import where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "'  and Jenis='LUAR'"
            SQL = SQL & "group by Kode_Perusahaan_Biaya_Import"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Kategori.Items.Add(dr("Kode_Perusahaan_Biaya_Import"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            Cmb_Kategori.Items.Clear()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Cmb_Kategori.SelectedIndex = 0
    End Sub

    Private Sub get_MUA()
        Try
            OpenConn()
            Cmb_MUA.Items.Clear() : Cmb_MUA.Items.Add("---SELURUH---")
            SQL = "Select kode_mata_uang From "
            SQL = SQL & "mata_uang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "group by kode_mata_uang"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_MUA.Items.Add(dr("kode_mata_uang"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            Cmb_MUA.Items.Clear()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Cmb_MUA.SelectedIndex = 0
    End Sub

    Private Sub TxtKdCust_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtKdSupplier.TextChanged
        If TxtKdSupplier.Text.Trim.Length = 0 Then
            LvSupplier.Visible = False : Exit Sub
        Else
            LvSupplier.Visible = True
        End If

        LvSupplier.Items.Clear()
        lv = LvSupplier.Items.Add("-- Seluruh --")
        lv.SubItems.Add("-- Seluruh --")

        Try

            OpenConn()

            SQL = "Select b.kode_supplier, b.nama "
            SQL = SQL & "from suppliers b "
            SQL = SQL & "where Tampil_Di_PO = 'Y' and "
            SQL = SQL & "b.kode_perusahaan = '" & KodePerusahaan & "' and b.kode_supplier like '%" & TxtKdSupplier.Text & "%' "

            'If CmbLokasi.SelectedIndex = 0 Then
            '    SQL = SQL & " and a.lokasi in("
            '    Dim list_kota As String = ""
            '    For x As Integer = 1 To CmbLokasi.Items.Count - 1
            '        list_kota = list_kota & "'" & CmbLokasi.Items(x).ToString & "', "
            '    Next

            '    list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

            '    SQL = SQL & list_kota & ")"
            'Else
            '    SQL = SQL & " and a.lokasi = '" & CmbLokasi.Text & "'"
            'End If

            SQL = SQL & "order by b.kode_supplier"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = LvSupplier.Items.Add(Dr("kode_supplier"))
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

    Private Sub TxtNama_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNama.TextChanged
        If TxtNama.Text.Trim.Length = 0 Then
            LvSupplier.Visible = False : Exit Sub
        Else
            LvSupplier.Visible = True
        End If

        LvSupplier.Items.Clear()
        lv = LvSupplier.Items.Add("-- Seluruh --")
        lv.SubItems.Add("-- Seluruh --")

        Try

            OpenConn()

            SQL = "Select b.kode_supplier, b.nama "
            SQL = SQL & "from suppliers b "
            SQL = SQL & "where Tampil_Di_PO = 'Y' and "
            SQL = SQL & "b.kode_perusahaan = '" & KodePerusahaan & "' and b.nama like '%" & TxtNama.Text & "%' "

            'If CmbLokasi.SelectedIndex = 0 Then
            '    SQL = SQL & " and a.lokasi in("
            '    Dim list_kota As String = ""
            '    For x As Integer = 1 To CmbLokasi.Items.Count - 1
            '        list_kota = list_kota & "'" & CmbLokasi.Items(x).ToString & "', "
            '    Next

            '    list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

            '    SQL = SQL & list_kota & ")"
            'Else
            '    SQL = SQL & " and a.lokasi = '" & CmbLokasi.Text & "'"
            'End If

            SQL = SQL & "order by b.kode_supplier"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = LvSupplier.Items.Add(Dr("kode_supplier"))
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

    Private Sub LvSupplier_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LvSupplier.DoubleClick
        If LvSupplier.Items.Count = 0 Then Exit Sub

        Dim Kode As String = LvSupplier.FocusedItem.Text
        Dim Nama As String = LvSupplier.FocusedItem.SubItems(1).Text

        TxtKdSupplier.Text = Kode
        TxtNama.Text = Nama
        LvSupplier.Visible = False
        BtnCetak.Focus()

    End Sub

    Private Sub LvSupplier_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles LvSupplier.KeyDown
        If e.KeyCode = Keys.Enter Then
            LvSupplier_DoubleClick(LvSupplier, e)
        End If
    End Sub

    'Private Sub BtnCetak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCetak.Click
    '    'If Tgl1.Value = Tgl2.Value Then
    '    '    MessageBox.Show("Periode II tidak boleh sama dengan periode I . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    '    'Tgl1.Value = Now.Date : Tgl2.Value = Now.Date.AddDays(14)
    '    '    Tgl2.Focus() : Exit Sub
    '    'Else
    '    If CmbLokasi.SelectedIndex = -1 Then
    '        MessageBox.Show("Lokasi harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        CmbLokasi.Focus() : Exit Sub
    '    ElseIf Cmb_Kategori.SelectedIndex = -1 Then
    '        MessageBox.Show("Kategori harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Cmb_Kategori.Focus() : Exit Sub
    '    ElseIf Cmb_MUA.SelectedIndex = -1 Then
    '        MessageBox.Show("Mata Uang harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Cmb_MUA.Focus() : Exit Sub
    '    ElseIf TxtKdSupplier.Text.Trim.Length = 0 Then
    '        MessageBox.Show("Supplier harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        TxtKdSupplier.Focus() : Exit Sub
    '    End If

    '    Dim SF As String = ""

    '    '---------- SQL
    '    SQL = "Select a.kode_perusahaan, a.Id_Rencana, a.No_Faktur, b.kode_supplier, b.Lokasi, b.Tanggal_PO, a.Kode_Perusahaan_Biaya_Import, a.Mata_Uang "
    '    SQL = SQL & "From VW_Detail_Transaksi_Biaya_Import_By_Perusahaan_Gabungan a, Rencana_Order b, Perusahaan_Biaya_Import c "
    '    SQL = SQL & "Where a.kode_perusahaan = b.kode_perusahaan and a.kode_perusahaan = c.Kode_Perusahaan and b.kode_perusahaan = c.kode_perusahaan and "
    '    SQL = SQL & "a.Id_Rencana = b.ID_Rencana and a.Kode_Perusahaan_Biaya_Import = c.Kode_Perusahaan_Biaya_Import and "
    '    SQL = SQL & "c.Jenis = 'LUAR' and sudah_ada_Pembelian = 'Y' and "
    '    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
    '    SQL = SQL & "a.tanggal_HPP between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '"
    '    SQL = SQL & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
    '    'LOKASI
    '    If CmbLokasi.SelectedIndex = 0 Then
    '        SQL = SQL & " and b.Lokasi in("
    '        Dim list_kota As String = ""
    '        For x As Integer = 1 To CmbLokasi.Items.Count - 1
    '            list_kota = list_kota & "'" & CmbLokasi.Items(x).ToString & "', "
    '        Next
    '        list_kota = Strings.Left(list_kota, Len(list_kota) - 2)
    '        SQL = SQL & list_kota & ") "
    '    Else
    '        SQL = SQL & " and b.Lokasi = '" & CmbLokasi.Text & "' "
    '    End If
    '    'KATEGORI
    '    If Cmb_Kategori.SelectedIndex = 0 Then
    '        SQL = SQL & " and a.Kode_Perusahaan_Biaya_Import in("
    '        Dim list_Kategori As String = ""
    '        For x As Integer = 1 To Cmb_Kategori.Items.Count - 1
    '            list_Kategori = list_Kategori & "'" & Cmb_Kategori.Items(x).ToString & "', "
    '        Next
    '        list_Kategori = Strings.Left(list_Kategori, Len(list_Kategori) - 2)
    '        SQL = SQL & list_Kategori & ") "
    '    Else
    '        SQL = SQL & " and a.Kode_Perusahaan_Biaya_Import = '" & Cmb_Kategori.Text & "' "
    '    End If
    '    'MUA
    '    If Cmb_MUA.SelectedIndex = 0 Then
    '        SQL = SQL & " and a.Mata_Uang in("
    '        Dim list_MUA As String = ""
    '        For x As Integer = 1 To Cmb_MUA.Items.Count - 1
    '            list_MUA = list_MUA & "'" & Cmb_MUA.Items(x).ToString & "', "
    '        Next
    '        list_MUA = Strings.Left(list_MUA, Len(list_MUA) - 2)
    '        SQL = SQL & list_MUA & ") "
    '    Else
    '        SQL = SQL & " and a.Mata_Uang = '" & Cmb_MUA.Text & "' "
    '    End If
    '    'SUPPLIER
    '    If Not TxtKdSupplier.Text.ToUpper = "-- SELURUH --" Then
    '        SQL = SQL & "AND b.kode_supplier = '" & TxtKdSupplier.Text.Trim & "' "
    '    End If

    '    '---------- SF
    '    SF = "{VW_Detail_Transaksi_Biaya_Import_By_Perusahaan_Gabungan.Kode_Perusahaan} = '" & KodePerusahaan & "' and "
    '    SF = SF & "{Perusahaan_Biaya_Import.Jenis} = 'LUAR' and "
    '    SF = SF & "IsNull({Rencana_Order.Status}) and "
    '    SF = "{VW_Detail_Transaksi_Biaya_Import_By_Perusahaan_Gabungan.sudah_ada_Pembelian} = 'Y' and "
    '    'SF = SF & "IsNull({VW_Detail_Transaksi_Biaya_Import_By_Perusahaan_Gabungan.flag_lunas}) and "
    '    SF = SF & "{VW_Detail_Transaksi_Biaya_Import_By_Perusahaan_Gabungan.Tanggal_HPP} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# AND {VW_Detail_Transaksi_Biaya_Import_By_Perusahaan_Gabungan.Tanggal_HPP} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "
    '    'LOKASI
    '    If CmbLokasi.SelectedIndex = 0 Then
    '        Dim list_kota As String = ""
    '        For x As Integer = 1 To CmbLokasi.Items.Count - 1
    '            list_kota = list_kota & "'" & CmbLokasi.Items(x).ToString & "', "
    '        Next
    '        list_kota = Strings.Left(list_kota, Len(list_kota) - 2)
    '        SF = SF & " and {Rencana_Order.Lokasi} in ["
    '        SF = SF & list_kota
    '        SF = SF & "]"
    '    Else
    '        SF = SF & " and {Rencana_Order.lokasi} = '" & CmbLokasi.Text & "' "
    '    End If
    '    'KATEGORI
    '    If Cmb_Kategori.SelectedIndex = 0 Then
    '        Dim list_kat As String = ""
    '        For x As Integer = 1 To Cmb_Kategori.Items.Count - 1
    '            list_kat = list_kat & "'" & Cmb_Kategori.Items(x).ToString & "', "
    '        Next
    '        list_kat = Strings.Left(list_kat, Len(list_kat) - 2)
    '        SF = SF & " and {VW_Detail_Transaksi_Biaya_Import_By_Perusahaan_Gabungan.Kode_Perusahaan_Biaya_Import} in ["
    '        SF = SF & list_kat
    '        SF = SF & "]"
    '    Else
    '        SF = SF & " and {VW_Detail_Transaksi_Biaya_Import_By_Perusahaan_Gabungan.Kode_Perusahaan_Biaya_Import} = '" & Cmb_Kategori.Text & "' "
    '    End If
    '    'MUA
    '    If Cmb_MUA.SelectedIndex = 0 Then
    '        Dim list_mua As String = ""
    '        For x As Integer = 1 To Cmb_MUA.Items.Count - 1
    '            list_mua = list_mua & "'" & Cmb_MUA.Items(x).ToString & "', "
    '        Next
    '        list_mua = Strings.Left(list_mua, Len(list_mua) - 2)
    '        SF = SF & " and {VW_Detail_Transaksi_Biaya_Import_By_Perusahaan_Gabungan.Mata_Uang} in ["
    '        SF = SF & list_mua
    '        SF = SF & "]"
    '    Else
    '        SF = SF & " and {VW_Detail_Transaksi_Biaya_Import_By_Perusahaan_Gabungan.Mata_Uang} = '" & Cmb_MUA.Text & "' "
    '    End If
    '    'SUPPLIER
    '    If Not TxtKdSupplier.Text.ToUpper = "-- SELURUH --" Then
    '        SF = SF & "AND {Rencana_Order.kode_supplier} = '" & TxtKdSupplier.Text.Trim & "' "
    '    End If

    '    Try
    '        OpenConn()
    '        Using Ds = BindingTrans(SQL)
    '            If Ds.Tables("MyTable").Rows.Count <> 0 Then
    '                Dim CrDoc = New Rpt_Laporan_Perusahaan_Biaya_Import

    '                With A_Place_For_Printing
    '                    CrDoc.SetDataSource(Ds)
    '                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
    '                    CrDoc.RecordSelectionFormula = SF
    '                    .Text = "Laporan Perusahaan Biaya Import"
    '                    .CrystalReportViewer1.ReportSource = CrDoc
    '                    .CrystalReportViewer1.DisplayGroupTree = False
    '                    .Refresh()
    '                    .Show()
    '                End With
    '            Else
    '                MessageBox.Show("Data tidak ada . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            End If
    '        End Using

    '        CloseConn()
    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try

    'End Sub

    Private Sub BtnCetak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCetak.Click
        'If Tgl1.Value = Tgl2.Value Then
        '    MessageBox.Show("Periode II tidak boleh sama dengan periode I . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    'Tgl1.Value = Now.Date : Tgl2.Value = Now.Date.AddDays(14)
        '    Tgl2.Focus() : Exit Sub
        'Else
        If CmbLokasi.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbLokasi.Focus() : Exit Sub
        ElseIf Cmb_Kategori.SelectedIndex = -1 Then
            MessageBox.Show("Kategori harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Kategori.Focus() : Exit Sub
        ElseIf Cmb_MUA.SelectedIndex = -1 Then
            MessageBox.Show("Mata Uang harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_MUA.Focus() : Exit Sub
        ElseIf TxtKdSupplier.Text.Trim.Length = 0 Then
            MessageBox.Show("Supplier harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKdSupplier.Focus() : Exit Sub
        End If

        Dim SF As String = ""

        '---------- SQL
        SQL = "select ID_Rencana, Tanggal_HPP, Lokasi, Kode_Perusahaan_Biaya_Import, "
        SQL = SQL & "Nama, Keterangan,Total_Hutang, Sisa_Hutang from View_Hutang_Agent_Lokal "
        SQL = SQL & "Where kode_perusahaan='" & KodePerusahaan & "' and "
        SQL = SQL & "tanggal_HPP between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and "
        SQL = SQL & "'" & Format(Tgl2.Value, "yyyy-MM-dd") & "'"

        'LOKASI
        If CmbLokasi.SelectedIndex = 0 Then
            SQL = SQL & " and Lokasi in("
            Dim list_kota As String = ""
            For x As Integer = 1 To CmbLokasi.Items.Count - 1
                list_kota = list_kota & "'" & CmbLokasi.Items(x).ToString & "', "
            Next
            list_kota = Strings.Left(list_kota, Len(list_kota) - 2)
            SQL = SQL & list_kota & ") "
        Else
            SQL = SQL & " and Lokasi = '" & CmbLokasi.Text & "' "
        End If
        'KATEGORI
        If Cmb_Kategori.SelectedIndex = 0 Then
            SQL = SQL & " and Kode_Perusahaan_Biaya_Import in("
            Dim list_Kategori As String = ""
            For x As Integer = 1 To Cmb_Kategori.Items.Count - 1
                list_Kategori = list_Kategori & "'" & Cmb_Kategori.Items(x).ToString & "', "
            Next
            list_Kategori = Strings.Left(list_Kategori, Len(list_Kategori) - 2)
            SQL = SQL & list_Kategori & ") "
        Else
            SQL = SQL & " and Kode_Perusahaan_Biaya_Import = '" & Cmb_Kategori.Text & "' "
        End If
        'MUA
        If Cmb_MUA.SelectedIndex = 0 Then
            SQL = SQL & " and Mata_Uang in("
            Dim list_MUA As String = ""
            For x As Integer = 1 To Cmb_MUA.Items.Count - 1
                list_MUA = list_MUA & "'" & Cmb_MUA.Items(x).ToString & "', "
            Next
            list_MUA = Strings.Left(list_MUA, Len(list_MUA) - 2)
            SQL = SQL & list_MUA & ") "
        Else
            SQL = SQL & " and Mata_Uang = '" & Cmb_MUA.Text & "' "
        End If
        'SUPPLIER
        If Not TxtKdSupplier.Text.ToUpper = "-- SELURUH --" Then
            SQL = SQL & "AND kode_supplier = '" & TxtKdSupplier.Text.Trim & "' "
        End If

        '---------- SF
        SF = "{View_Hutang_Agent_Lokal.Kode_Perusahaan} = '" & KodePerusahaan & "' and "
        'SF = SF & "{Perusahaan_Biaya_Import.Jenis} = 'LUAR' and "
        'SF = SF & "IsNull({Rencana_Order.Status}) and "
        SF = "{View_Hutang_Agent_Lokal.sudah_ada_Pembelian} = 'Y' and "
        'SF = SF & "IsNull({VW_Detail_Transaksi_Biaya_Import_By_Perusahaan_Gabungan.flag_lunas}) and "
        SF = SF & "{View_Hutang_Agent_Lokal.Tanggal_HPP} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# AND  "
        SF = SF & "{View_Hutang_Agent_Lokal.Tanggal_HPP} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "#"
        'LOKASI
        If CmbLokasi.SelectedIndex = 0 Then
            Dim list_kota As String = ""
            For x As Integer = 1 To CmbLokasi.Items.Count - 1
                list_kota = list_kota & "'" & CmbLokasi.Items(x).ToString & "', "
            Next
            list_kota = Strings.Left(list_kota, Len(list_kota) - 2)
            SF = SF & " and {View_Hutang_Agent_Lokal.Lokasi} in ["
            SF = SF & list_kota
            SF = SF & "]"
        Else
            SF = SF & " and {View_Hutang_Agent_Lokal.lokasi} = '" & CmbLokasi.Text & "' "
        End If
        'KATEGORI
        If Cmb_Kategori.SelectedIndex = 0 Then
            Dim list_kat As String = ""
            For x As Integer = 1 To Cmb_Kategori.Items.Count - 1
                list_kat = list_kat & "'" & Cmb_Kategori.Items(x).ToString & "', "
            Next
            list_kat = Strings.Left(list_kat, Len(list_kat) - 2)
            SF = SF & " and {View_Hutang_Agent_Lokal.Kode_Perusahaan_Biaya_Import} in ["
            SF = SF & list_kat
            SF = SF & "]"
        Else
            SF = SF & " and {View_Hutang_Agent_Lokal.Kode_Perusahaan_Biaya_Import} = '" & Cmb_Kategori.Text & "' "
        End If
        'MUA
        If Cmb_MUA.SelectedIndex = 0 Then
            Dim list_mua As String = ""
            For x As Integer = 1 To Cmb_MUA.Items.Count - 1
                list_mua = list_mua & "'" & Cmb_MUA.Items(x).ToString & "', "
            Next
            list_mua = Strings.Left(list_mua, Len(list_mua) - 2)
            SF = SF & " and {View_Hutang_Agent_Lokal.Mata_Uang} in ["
            SF = SF & list_mua
            SF = SF & "]"
        Else
            SF = SF & " and {View_Hutang_Agent_Lokal.Mata_Uang} = '" & Cmb_MUA.Text & "' "
        End If
        'SUPPLIER
        If Not TxtKdSupplier.Text.ToUpper = "-- SELURUH --" Then
            SF = SF & "AND {View_Hutang_Agent_Lokal.kode_supplier} = '" & TxtKdSupplier.Text.Trim & "' "
        End If

        'Try
        '    OpenConn()
        '    Using Ds = BindingTrans(SQL)
        '        If Ds.Tables("MyTable").Rows.Count <> 0 Then
        '            Dim CrDoc = New Rpt_Laporan_Perusahaan_Biaya_Import_lokal

        '            With A_Place_For_Printing
        '                CrDoc.SetDataSource(Ds)
        '                CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
        '                CrDoc.RecordSelectionFormula = SF
        '                .Text = "Laporan Perusahaan Biaya Import"
        '                .CrystalReportViewer1.ReportSource = CrDoc
        '                .CrystalReportViewer1.DisplayGroupTree = False
        '                .Refresh()
        '                .Show()
        '            End With
        '        Else
        '            MessageBox.Show("Data tidak ada . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        End If
        '    End Using

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

    End Sub

    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        BersihSeluruh()
    End Sub

End Class