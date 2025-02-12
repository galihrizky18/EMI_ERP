Public Class Display_HPP_Local
    Dim Arr1, Arr2, Arr3, arrbatal As New ArrayList

    'Private Sub cetak(ByVal nama_cr As String)
    '    'Private Sub cetak()
    '    Try
    '        OpenConn()

    '        SQL = "select kode_perusahaan from transaksi_biaya_import where "
    '        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.Text & "'"
    '        Using Ds = BindingTrans(SQL)
    '            If Ds.Tables("MyTable").Rows.Count <> 0 Then
    '                Dim CrDoc As New Faktur_Biaya_Import
    '                With A_Place_For_Printing2
    '                    CrDoc.SetDataSource(Ds)
    '                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
    '                    CrDoc.RecordSelectionFormula = "{transaksi_biaya_import.Kode_Perusahaan} = '" & KodePerusahaan & "' and " &
    '                              "{transaksi_biaya_import.no_faktur} = '" & ListView1.FocusedItem.Text & "'"
    '                    'CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd MMM yyyy") & " s/d " & Format(Tgl2.Value, "dd MMM yyyy")
    '                    .Text = "Laporan Komposisi Barang Jadi"
    '                    .CrystalReportViewer1.ReportSource = CrDoc
    '                    .CrystalReportViewer1.DisplayGroupTree = False
    '                    .Refresh()
    '                    .Show()
    '                    .Focus()
    '                End With
    '            Else
    '                CloseTrans()
    '                CloseConn()
    '                MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                Exit Sub
    '            End If
    '        End Using

    '        CloseConn()

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Exit Sub

    '    End Try
    'End Sub

    Private Sub Display_Transaksi_Biaya_Import_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ListView1.Columns.Add("No Faktur", 130, HorizontalAlignment.Left) '0
        ListView1.Columns.Add("No Nota", 160, HorizontalAlignment.Left) '1
        ListView1.Columns.Add("Lokasi ", 130, HorizontalAlignment.Left) '2
        ListView1.Columns.Add("Tanggal", 100, HorizontalAlignment.Center) '3
        ListView1.Columns.Add("Jam", 80, HorizontalAlignment.Center) '4
        ListView1.Columns.Add("Status", 0, HorizontalAlignment.Left) '5
        ListView1.Columns.Add("Tanggal Jatuh Tempo", 130, HorizontalAlignment.Center) '6
        ListView1.Columns.Add("Kode Supplier", 100, HorizontalAlignment.Center) '7
        ListView1.Columns.Add("Nama Supplier", 260, HorizontalAlignment.Center) '8
        ListView1.Columns.Add("User ID", 80, HorizontalAlignment.Center) '9
        ListView1.View = View.Details

        CheckBox1.Checked = False : CheckBox3.Checked = False
        DateTimePicker1.Value = CDate(FMenu.ToolStripStatusLabel3.Text) : DateTimePicker2.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
        DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False

        ComboBox1.Enabled = False
        ComboBox1.Items.Clear() : ComboBox1.Text = "" : Arr1.Clear()
        ComboBox1.Items.Add("Tanggal") : Arr1.Add("a.Tanggal")
        ComboBox1.Items.Add("Tgl Jatuh Tempo") : Arr1.Add("a.Tgl_Jatuh_Tempo")

        Try
            OpenConn()

            ComboBox6.Items.Clear()
            ComboBox6.Items.Add("-- Seluruh --")

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
                    ComboBox6.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using

            ComboBox6.Text = Lokasi

            If CekButtonRole("Ganti_Lokasi_Display_HPP_Local") = "T" Then
                ComboBox6.Enabled = False
            Else
                ComboBox6.Enabled = True
            End If

            CloseConn()
        Catch ex As Exception
            ComboBox6.Items.Clear()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        ComboBox2.Items.Clear() : ComboBox2.Text = "" : Arr2.Clear()
        ComboBox2.Items.Add("No Faktur") : Arr2.Add("a.no_faktur")
        ComboBox2.Items.Add("No Nota") : Arr2.Add("a.no_nota")
        ComboBox2.Items.Add("Kode Supplier") : Arr2.Add("a.kode_supplier")
        ComboBox2.Items.Add("Nama Supplier") : Arr2.Add("b.nama")
        ComboBox2.Items.Add("User ID") : Arr2.Add("a.userid")

        ComboBox1.Enabled = False : ComboBox2.Enabled = False
        DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
        TextBox1.Enabled = False

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            ComboBox1.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
            CheckBox3.Checked = False

        Else
            ComboBox1.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            ComboBox1.SelectedIndex = -1 : DateTimePicker1.Value = CDate(FMenu.ToolStripStatusLabel3.Text) : DateTimePicker2.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox1.Checked = False
            Button1_Click(CheckBox3, e)
        End If
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker1.Focus()
    End Sub

    Private Sub DateTimePicker1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub

    Private Sub DateTimePicker2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Chr(13) Then Button1.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If CheckBox1.Checked = False And CheckBox3.Checked = False And CheckBox2.Checked = False Then
            MessageBox.Show("Pilih terlebih dahulu parameter pencarian data!", Judul)
            CheckBox1.Focus() : Exit Sub
        End If

        If CheckBox1.Checked Then
            If ComboBox1.SelectedIndex = -1 Then
                MessageBox.Show("Parameter pencarian per tanggal harus diisi!", Judul)
                ComboBox1.Focus() : Exit Sub
            ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
                DateTimePicker1.Value = CDate(FMenu.ToolStripStatusLabel3.Text) : DateTimePicker2.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
                Exit Sub
            End If

        ElseIf CheckBox2.Checked Then
            If ComboBox2.SelectedIndex = -1 Then
                MessageBox.Show("Parameter lain harus diisi!", Judul)
                ComboBox2.Focus() : Exit Sub
            ElseIf TextBox1.Text.Trim.Length = 0 Then
                MessageBox.Show("Value parameter lain harus diisi!", Judul)
                TextBox1.Focus() : Exit Sub
            End If
        End If

        Try
            OpenConn()

            SQL = "select a.No_Faktur, a.No_Nota, a.Lokasi, a.Tanggal, a.Jam, a.status, a.Tgl_Jatuh_Tempo, a.Kode_Supplier, b.nama, a.UserID "
            SQL = SQL & "from emi_pembelian_po a, suppliers b "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and a.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.kode_supplier = b.kode_supplier "

            If ComboBox6.SelectedIndex = 0 Then
                SQL = SQL & " and a.lokasi in("
                Dim list_kota As String = ""
                For x As Integer = 1 To ComboBox6.Items.Count - 1
                    list_kota = list_kota & "'" & ComboBox6.Items(x).ToString & "', "
                Next

                list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

                SQL = SQL & list_kota & ")"
            Else
                SQL = SQL & " and a.lokasi = '" & ComboBox6.Text & "'"
            End If

            If CheckBox1.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & Arr1.Item(ComboBox1.SelectedIndex) & " between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If CheckBox2.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & Arr2.Item(ComboBox2.SelectedIndex) & " like '%" & Trim(TextBox1.Text) & "%' "
            End If

            If CheckBox3.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & " a.tanggal between '"
                SQL = SQL & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' and '" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' "
            End If

            SQL = SQL & "group by a.No_Faktur, a.No_Nota, a.Lokasi, a.Tanggal, a.Jam, a.status, a.Tgl_Jatuh_Tempo, a.Kode_Supplier, b.nama, a.UserID "
            SQL = SQL & "ORDER BY a.tanggal + a.jam DESC"

            Ds = New DataSet
            Ds = BindingTrans(SQL)

            ListView1.Items.Clear()
            Dim i As Integer = 0
            Dim j As Integer = 0

            While i <= Ds.Tables("MyTable").Rows.Count - 1
                With Ds.Tables("MyTable").Rows(i)
                    Dim lvi As ListViewItem
                    lvi = ListView1.Items.Add(.Item("no_faktur")) '0
                    lvi.SubItems.Add(.Item("no_nota")) '1
                    lvi.SubItems.Add(.Item("lokasi")) '2
                    lvi.SubItems.Add(Format(.Item("tanggal"), "dd MMM yyyy")) '3
                    lvi.SubItems.Add(.Item("jam")) '4

                    If General_Class.CekNULL(.Item("status")) = "" Then
                        lvi.SubItems.Add("-")
                    Else
                        lvi.SubItems.Add(.Item("status")) '5
                    End If

                    If General_Class.CekNULL(.Item("tgl_jatuh_tempo")) = "" Then
                        lvi.SubItems.Add("-")
                    Else
                        lvi.SubItems.Add(Format(.Item("tgl_jatuh_tempo"), "dd MMM yyyy")) '6
                    End If

                    lvi.SubItems.Add(.Item("kode_supplier")) '7
                    lvi.SubItems.Add(.Item("nama")) '8
                    lvi.SubItems.Add(.Item("userid")) '9
                    j += 1
                End With
                i += 1
            End While

            CloseConn()
        Catch ex As Exception
            ComboBox6.Items.Clear()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try

    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        Try
            If ListView1.Items.Count = 0 Then Exit Sub
            OpenConn()
            DgvDataBarang.Rows.Clear()
            DgvDataKendaraan.Rows.Clear()

            '--- Data Kendaraan
            SQL = "select b.No_PO, a.No_Faktur, a.No_SJ, a.Driver, a.No_Plat, a.Biaya_Perjalanan, a.Tanggal_Masuk, a.Jam_Masuk "
            SQL = SQL & "from emi_pembelian_loading a, EMI_Pembelian_Loading_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_PO = '" & ListView1.FocusedItem.SubItems(0).Text & "' "
            SQL = SQL & "group by a.No_Faktur,b.No_PO, a.No_SJ, a.Driver, a.No_Plat, a.Biaya_Perjalanan, a.Tanggal_Masuk, a.Jam_Masuk "

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For index As Integer = 0 To .Rows.Count - 1

                        DgvDataKendaraan.Rows.Add(1)
                        DgvDataKendaraan.Rows.Item(index).Cells(0).Value = .Rows(index).Item("No_PO")
                        DgvDataKendaraan.Rows.Item(index).Cells(1).Value = .Rows(index).Item("No_Faktur")
                        DgvDataKendaraan.Rows.Item(index).Cells(2).Value = .Rows(index).Item("No_SJ")
                        DgvDataKendaraan.Rows.Item(index).Cells(3).Value = .Rows(index).Item("Driver")
                        DgvDataKendaraan.Rows.Item(index).Cells(4).Value = .Rows(index).Item("No_Plat")

                        If General_Class.CekNULL(.Rows(index).Item("biaya_perjalanan")) = "" Then
                            DgvDataKendaraan.Rows.Item(index).Cells(5).Value = "0"
                        Else
                            DgvDataKendaraan.Rows.Item(index).Cells(5).Value = Format(.Rows(index).Item("biaya_perjalanan"), "N2")
                        End If

                        If General_Class.CekNULL(.Rows(index).Item("Tanggal_Masuk")) = "" Then
                            DgvDataKendaraan.Rows.Item(index).Cells(6).Value = "-"
                        Else
                            DgvDataKendaraan.Rows.Item(index).Cells(6).Value = Format(.Rows(index).Item("Tanggal_Masuk"), "dd MMM yyyy")
                        End If

                        If General_Class.CekNULL(.Rows(index).Item("Jam_Masuk")) = "" Then
                            DgvDataKendaraan.Rows.Item(index).Cells(7).Value = "-"
                        Else
                            DgvDataKendaraan.Rows.Item(index).Cells(7).Value = .Rows(index).Item("Jam_Masuk")
                        End If

                    Next
                End With
            End Using

            '--- Data Barang
            SQL = "select a.no_faktur, a.no_po, a.kode_barang, b.nama, "
            SQL = SQL & "DBO.Ubah_Satuan(A.Kode_Perusahaan, 'MASA', a.Kode_Barang, a.Satuan_Barang, a.Satuan,a.jumlah_barang) as Jumlah_PO, "
            SQL = SQL & "DBO.Ubah_Satuan(A.Kode_Perusahaan, 'MASA', a.Kode_Barang, a.Satuan_Barang, a.Satuan,a.jumlah_masuk)  as Jumlah_Masuk, "
            SQL = SQL & "DBO.Ubah_Satuan(A.Kode_Perusahaan, 'UANG', a.Kode_Barang, a.Satuan_Barang, a.Satuan,a.Harga_Barang)  as Harga_PO, "
            SQL = SQL & "a.HPP_Satuan_Display as Harga_Akhir, "

            SQL = SQL & "DBO.Ubah_Satuan(A.Kode_Perusahaan, 'MASA', a.Kode_Barang, a.Satuan_Barang, a.Satuan,a.jumlah_masuk) * "
            SQL = SQL & "DBO.Ubah_Satuan(A.Kode_Perusahaan, 'UANG', a.Kode_Barang, a.Satuan_Barang, a.Satuan,a.Harga_Barang) as Grand_PO, "

            SQL = SQL & "DBO.Ubah_Satuan(A.Kode_Perusahaan, 'MASA', a.Kode_Barang, a.Satuan_Barang, a.Satuan,a.jumlah_masuk) * "
            SQL = SQL & "a.HPP_Satuan_Display as Grand_Akhir "

            SQL = SQL & "from EMI_Pembelian_Loading_Detail a, barang b, EMI_Pembelian_Loading c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.kode_barang and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_po = '" & ListView1.FocusedItem.SubItems(0).Text & "' "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan  and a.no_faktur=c.No_Faktur "
            SQL = SQL & "and a.Flag_Timbang_Keluar='Y' and ((c.Flag_Import='Y' and c.Flag_Import_HPP='Y') or Flag_Import is null) "

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For index As Integer = 0 To .Rows.Count - 1

                        DgvDataBarang.Rows.Add(1)
                        DgvDataBarang.Rows.Item(index).Cells(0).Value = .Rows(index).Item("no_faktur")
                        DgvDataBarang.Rows.Item(index).Cells(1).Value = .Rows(index).Item("no_po")
                        DgvDataBarang.Rows.Item(index).Cells(2).Value = .Rows(index).Item("kode_barang")
                        DgvDataBarang.Rows.Item(index).Cells(3).Value = .Rows(index).Item("nama")
                        DgvDataBarang.Rows.Item(index).Cells(4).Value = Format(.Rows(index).Item("jumlah_po"), "N2")
                        DgvDataBarang.Rows.Item(index).Cells(5).Value = Format(.Rows(index).Item("jumlah_masuk"), "N2")
                        DgvDataBarang.Rows.Item(index).Cells(6).Value = Format(.Rows(index).Item("harga_po"), "N2")
                        DgvDataBarang.Rows.Item(index).Cells(7).Value = Format(.Rows(index).Item("harga_akhir"), "N2")
                        DgvDataBarang.Rows.Item(index).Cells(8).Value = Format(.Rows(index).Item("Grand_PO"), "N2")
                        DgvDataBarang.Rows.Item(index).Cells(9).Value = Format(.Rows(index).Item("Grand_akhir"), "N2")

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

    Private Sub CetakUlangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CetakUlangToolStripMenuItem.Click
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau cetak ulang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        'cetak("")
    End Sub

    Private Sub CetakGroupingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CetakGroupingToolStripMenuItem.Click
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau cetak ulang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            'SQL = "select kode_perusahaan from transaksi_biaya_import where "
            'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.Text & "'"
            'Using Ds = BindingTrans(SQL)
            '    If Ds.Tables("MyTable").Rows.Count <> 0 Then

            '        Dim CrDoc As New Faktur_Biaya_Import_Grouping


            '        With A_Place_For_Printing2
            '            CrDoc.SetDataSource(Ds)
            '            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '            CrDoc.RecordSelectionFormula = "{transaksi_biaya_import.Kode_Perusahaan} = '" & KodePerusahaan & "' and " &
            '                        "{transaksi_biaya_import.no_faktur} = '" & ListView1.FocusedItem.Text & "'"
            '            'CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd MMM yyyy") & " s/d " & Format(Tgl2.Value, "dd MMM yyyy")
            '            .Text = "Laporan Komposisi Barang Jadi"
            '            .CrystalReportViewer1.ReportSource = CrDoc
            '            .CrystalReportViewer1.DisplayGroupTree = False
            '            .Refresh()
            '            .Show()
            '            .Focus()
            '        End With
            '    Else
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub BatalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BatalToolStripMenuItem.Click
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih Dahulu Data yang akan dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tny As String = MessageBox.Show("Yakin akan dibatalkan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If tny = vbNo Then Exit Sub

        GetTime()

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("Batal_Hpp_Import") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            SQL = "select Id_Rencana from Rencana_Order_gabungan rog where "
            SQL = SQL & "rog.Kode_Perusahaan = '" & KodePerusahaan & "' and rog.Id_Rencana_induk = '" & ListView1.FocusedItem.SubItems(4).Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Do While Dr.Read
                        Dr.Close()
                        SQL = "select count(kode_perusahaan) as count from barang_masuk_sementara bms where bms.status is null and "
                        SQL = SQL & "bms.Kode_Perusahaan = '" & KodePerusahaan & "' and bms.id_rencana = '" & Dr("id_rencana") & "'"
                        Using De = OpenTrans(SQL)
                            If De.Read Then
                                If General_Class.CekZERO(De("Count")) <> 0 Then
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("ID Rencana ini terdapat rencana_order_gabungan yang memiliki barang_masuk_sementara!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            Else
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("ID Rencana ini tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using
                    Loop
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("ID Rencana ini tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select Status from Rencana_Order ro where "
            SQL = SQL & "ro.Kode_Perusahaan = '" & KodePerusahaan & "' and ro.Id_Rencana = '" & ListView1.FocusedItem.SubItems(4).Text & "' and "
            SQL = SQL & "ro.flag_hpp is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("ID Rencana Sudah di batalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Id Rencana Tidak Bisa Dibatalkan karena Sudah Di Proses Di Tahap Lain!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            SQL = "select Status from hpp_import where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.SubItems(0).Text & "' and id_rencana ='" & ListView1.FocusedItem.SubItems(4).Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Faktur Telah di batalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
            End Using

            SQL = "Update hpp_import set status = 'Y' where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & ListView1.FocusedItem.Text & "' and id_rencana ='" & ListView1.FocusedItem.SubItems(4).Text & "'"
            ExecuteTrans(SQL)

            'ListView1_SelectedIndexChanged(ContextMenuStrip, e)

            Cmd.Transaction.Commit()

            MessageBox.Show("Data Berhasil Dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Button1_Click(BatalToolStripMenuItem, e)

    End Sub

    Private Sub CetakGroupingAverageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CetakGroupingAverageToolStripMenuItem.Click
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau cetak ulang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try

            OpenConn()

            'SQL = "select kode_perusahaan from transaksi_biaya_import where "
            'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.Text & "'"
            'Using Ds = BindingTrans(SQL)
            '    If Ds.Tables("MyTable").Rows.Count <> 0 Then

            '        Dim CrDoc As New Faktur_Biaya_Import_Grouping


            '        With A_Place_For_Printing2
            '            CrDoc.SetDataSource(Ds)
            '            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '            CrDoc.RecordSelectionFormula = "{transaksi_biaya_import.Kode_Perusahaan} = '" & KodePerusahaan & "' and " &
            '                        "{transaksi_biaya_import.no_faktur} = '" & ListView1.FocusedItem.Text & "'"
            '            'CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd MMM yyyy") & " s/d " & Format(Tgl2.Value, "dd MMM yyyy")
            '            .Text = "Laporan Komposisi Barang Jadi"
            '            .CrystalReportViewer1.ReportSource = CrDoc
            '            .CrystalReportViewer1.DisplayGroupTree = False
            '            .Refresh()
            '            .Show()
            '            .Focus()
            '        End With
            '    Else
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CheckBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CheckBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            If ComboBox2.Enabled = True Then
                ComboBox2.Focus()
            Else
                Button1.Focus()
            End If
        End If
    End Sub


    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            ComboBox2.Enabled = True : TextBox1.Enabled = True
        Else
            ComboBox2.Enabled = False : TextBox1.Enabled = False
            ComboBox2.SelectedIndex = -1 : TextBox1.Text = ""
        End If
    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox1.Focus()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        TextBox1.Focus()
    End Sub

End Class