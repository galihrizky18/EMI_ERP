Public Class Display_HPP_Import
    Dim Arr1, Arr2, Arr3, arrbatal As New ArrayList

    Private Sub cetak(ByVal nama_cr As String)
        'Private Sub cetak()
        Try

            OpenConn()

            'SQL = "select kode_perusahaan from transaksi_biaya_import where "
            'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.Text & "'"
            'Using Ds = BindingTrans(SQL)
            '    If Ds.Tables("MyTable").Rows.Count <> 0 Then
            '        Dim CrDoc As New Faktur_Biaya_Import
            '        With A_Place_For_Printing2
            '            CrDoc.SetDataSource(Ds)
            '            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '            CrDoc.RecordSelectionFormula = "{transaksi_biaya_import.Kode_Perusahaan} = '" & KodePerusahaan & "' and " & _
            '                      "{transaksi_biaya_import.no_faktur} = '" & ListView1.FocusedItem.Text & "'"
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
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try
    End Sub

    Private Sub Display_Transaksi_Biaya_Import_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ListView1.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        ListView1.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Jam", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Supplier", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Nama Supplier", 300, HorizontalAlignment.Center)
        ListView1.Columns.Add("ID Rencana", 80, HorizontalAlignment.Center)
        ListView1.Columns.Add("Lokasi ", 130, HorizontalAlignment.Left)
        ListView1.Columns.Add("Batal", 50, HorizontalAlignment.Center)
        ListView1.Columns.Add("Tanggal PO", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("User ID", 80, HorizontalAlignment.Center)
        ListView1.View = View.Details

        CheckBox1.Checked = False : CheckBox3.Checked = False
        DateTimePicker1.Value = CDate(FMenu.ToolStripStatusLabel3.Text) : DateTimePicker2.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
        DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False

        ComboBox1.Enabled = False
        ComboBox1.Items.Clear() : ComboBox1.Text = "" : Arr1.Clear()
        ComboBox1.Items.Add("Tanggal") : Arr1.Add("a.Tanggal")
        ComboBox1.Items.Add("Tanggal PO") : Arr1.Add("a.tanggal_po")

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

            If CekButtonRole("Ganti_Lokasi_Display_HPP_Import") = "T" Then
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
        ComboBox2.Items.Add("Id Rencana") : Arr2.Add("a.id_rencana")
        ComboBox2.Items.Add("Kode Supplier") : Arr2.Add("c.kode_supplier")
        ComboBox2.Items.Add("Nama Supplier") : Arr2.Add("c.nama")
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

            SQL = "select a.no_faktur, b.lokasi, a.tanggal, a.jam, a.id_rencana,a.status, b.tanggal_po, c.kode_supplier, c.nama, a.userid "
            SQL = SQL & " from hpp_import a, rencana_order b, suppliers c "
            SQL = SQL & " where a.kode_perusahaan = b.kode_perusahaan and a.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & " a.id_rencana = b.id_rencana and a.kode_supplier = c.kode_supplier  "

            If ComboBox6.SelectedIndex = 0 Then
                SQL = SQL & " and b.lokasi in("
                Dim list_kota As String = ""
                For x As Integer = 1 To ComboBox6.Items.Count - 1
                    list_kota = list_kota & "'" & ComboBox6.Items(x).ToString & "', "
                Next

                list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

                SQL = SQL & list_kota & ")"
            Else
                SQL = SQL & " and b.lokasi = '" & ComboBox6.Text & "'"
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

            SQL = SQL & "group by a.no_faktur, b.lokasi, a.tanggal, a.jam, a.id_rencana, a.status,b.tanggal_po, c.kode_supplier, c.nama, a.userid "
            SQL = SQL & "ORDER BY a.tanggal + a.jam DESC"

            Ds = New DataSet
            Ds = BindingTrans(SQL)

            ListView1.Items.Clear()
            Dim i As Integer = 0
            Dim j As Integer = 0

            While i <= Ds.Tables("MyTable").Rows.Count - 1
                With Ds.Tables("MyTable").Rows(i)
                    Dim lvi As ListViewItem
                    lvi = ListView1.Items.Add(.Item("no_faktur"))
                    lvi.SubItems.Add(Format(.Item("tanggal"), "dd MMM yyyy"))
                    lvi.SubItems.Add(.Item("jam"))
                    lvi.SubItems.Add(.Item("kode_supplier"))
                    lvi.SubItems.Add(.Item("nama"))
                    lvi.SubItems.Add(.Item("id_rencana"))
                    lvi.SubItems.Add(.Item("lokasi"))
                    lvi.SubItems.Add(General_Class.CekNULL(.Item("status")))
                    lvi.SubItems.Add(Format(.Item("tanggal_po"), "dd MMM yyyy"))
                    lvi.SubItems.Add(.Item("userid"))
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
            DataGridView1.Rows.Clear()
            DataGridView2.Rows.Clear()
            DataGridView3.Rows.Clear()
            DataGridView4.Rows.Clear()

            'hpp_import kotor
            SQL = " select a.no_faktur,d.lokasi,c.kode_barang,c.nama, b.jumlah,c.harga_Declare,b.total_harga, b.volume, b.jml_satuan_besar,b.isi_satuan_besar,b.berat_bersih,b.berat_kotor,b.total_berat_bersih,b.total_berat_kotor, "
            SQL = SQL & " b.panjang, b.lebar,b.tinggi,b.no_urut,b.Mata_Uang,b.nilai_pot_stock, b.nilai_tdk_pot_stock_lns, b.nilai_tdk_pot_stock_htg_utama, b.nilai_tdk_pot_stock_htg_penolong, b.nilai_hpp_besar, b.nilai_hpp_kecil,  "
            SQL = SQL & " b.nilai1,b.nilai2,b.pph29, b.biaya_import, b.biaya_billing, b.biaya_kontainer, b.biaya_freight_int, b.nilai_hpp_barang_per_pcs, b.kurs_nilai1 "
            SQL = SQL & " from hpp_import a, detail_hpp_import b, barang c, Rencana_Order d where  "
            SQL = SQL & " a.kode_perusahaan = b.kode_perusahaan and a.kode_perusahaan ='" & KodePerusahaan & "' and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
            SQL = SQL & " and a.No_Faktur = b.No_Faktur and b.Kode_Barang = c.Kode_Barang and a.ID_Rencana = d.ID_Rencana "
            SQL = SQL & " and a.id_rencana = '" & ListView1.FocusedItem.SubItems(5).Text & "' "
            SQL = SQL & " order by a.no_faktur"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For index As Integer = 0 To .Rows.Count - 1

                        DataGridView1.Rows.Add(1)
                        DataGridView1.Rows.Item(index).Cells(0).Value = .Rows(index).Item("no_faktur")
                        DataGridView1.Rows.Item(index).Cells(1).Value = .Rows(index).Item("lokasi")
                        DataGridView1.Rows.Item(index).Cells(2).Value = .Rows(index).Item("kode_barang")
                        DataGridView1.Rows.Item(index).Cells(3).Value = .Rows(index).Item("nama")
                        DataGridView1.Rows.Item(index).Cells(4).Value = Format(.Rows(index).Item("jumlah"), "N2")
                        DataGridView1.Rows.Item(index).Cells(5).Value = Format(.Rows(index).Item("harga_declare"), "N2")
                        DataGridView1.Rows.Item(index).Cells(6).Value = Format(.Rows(index).Item("total_harga"), "N2")
                        DataGridView1.Rows.Item(index).Cells(7).Value = Format(.Rows(index).Item("volume"), "N2")
                        DataGridView1.Rows.Item(index).Cells(8).Value = Format(.Rows(index).Item("jml_satuan_besar"), "N2")
                        DataGridView1.Rows.Item(index).Cells(9).Value = Format(.Rows(index).Item("isi_satuan_besar"), "N2")
                        DataGridView1.Rows.Item(index).Cells(10).Value = Format(.Rows(index).Item("berat_bersih"), "N2")
                        DataGridView1.Rows.Item(index).Cells(11).Value = Format(.Rows(index).Item("berat_kotor"), "N2")


                        DataGridView1.Rows.Item(index).Cells(12).Value = Format(.Rows(index).Item("total_berat_bersih"), "N2")
                        DataGridView1.Rows.Item(index).Cells(13).Value = Format(.Rows(index).Item("total_berat_kotor"), "N2")

                        DataGridView1.Rows.Item(index).Cells(14).Value = Format(.Rows(index).Item("panjang"), "N0")
                        DataGridView1.Rows.Item(index).Cells(15).Value = Format(.Rows(index).Item("lebar"), "N0")
                        DataGridView1.Rows.Item(index).Cells(16).Value = Format(.Rows(index).Item("tinggi"), "N0")
                        DataGridView1.Rows.Item(index).Cells(17).Value = Format(.Rows(index).Item("no_urut"), "N2")
                        DataGridView1.Rows.Item(index).Cells(18).Value = Format(.Rows(index).Item("mata_uang"), "N2")

                        DataGridView1.Rows.Item(index).Cells(19).Value = Format(.Rows(index).Item("nilai_pot_stock"), "N2")
                        DataGridView1.Rows.Item(index).Cells(20).Value = Format(.Rows(index).Item("nilai_tdk_pot_stock_lns"), "N2")
                        DataGridView1.Rows.Item(index).Cells(21).Value = Format(.Rows(index).Item("nilai_tdk_pot_stock_htg_utama"), "N2")
                        DataGridView1.Rows.Item(index).Cells(22).Value = Format(.Rows(index).Item("nilai_tdk_pot_stock_htg_penolong"), "N2")
                        DataGridView1.Rows.Item(index).Cells(23).Value = Format(.Rows(index).Item("nilai_hpp_besar"), "N2")
                        DataGridView1.Rows.Item(index).Cells(24).Value = Format(.Rows(index).Item("nilai_hpp_kecil"), "N2")

                        DataGridView1.Rows.Item(index).Cells(25).Value = Format(.Rows(index).Item("nilai1"), "N2")
                        DataGridView1.Rows.Item(index).Cells(26).Value = Format(.Rows(index).Item("nilai2"), "N2")
                        DataGridView1.Rows.Item(index).Cells(27).Value = Format(.Rows(index).Item("pph29"), "N2")

                        DataGridView1.Rows.Item(index).Cells(28).Value = Format(.Rows(index).Item("biaya_import"), "N2")
                        DataGridView1.Rows.Item(index).Cells(29).Value = Format(.Rows(index).Item("biaya_billing"), "N2")
                        DataGridView1.Rows.Item(index).Cells(30).Value = Format(.Rows(index).Item("biaya_kontainer"), "N2")
                        DataGridView1.Rows.Item(index).Cells(31).Value = Format(.Rows(index).Item("biaya_freight_int"), "N2")
                        DataGridView1.Rows.Item(index).Cells(32).Value = Format(.Rows(index).Item("nilai_hpp_barang_per_pcs"), "N0")
                        DataGridView1.Rows.Item(index).Cells(33).Value = General_Class.CekNULL(.Rows(index).Item("kurs_nilai1"))

                    Next
                End With
            End Using

            'hpp import bersih
            SQL = "select a.no_faktur,d.lokasi,b.lokasi_tujuan,c.kode_barang,c.nama, b.jumlah,c.harga_declare,b.total_harga, b.volume, b.jml_satuan_besar, b.isi_satuan_besar, b.berat_bersih, b.berat_kotor, "
            SQL = SQL & " b.total_berat_bersih,b.total_berat_kotor, b.panjang, b.lebar, b.tinggi, b.biaya_import2, b.biaya_import_wet_dry,b.input_hpp,  "
            SQL = SQL & " b.nilai_hpp_barang_per_pcs_brsh from hpp_import a, detail_hpp_import2 b, barang c, Rencana_Order d "
            SQL = SQL & " where a.kode_perusahaan = b.kode_perusahaan and a.kode_perusahaan ='" & KodePerusahaan & "' "
            SQL = SQL & " and a.No_Faktur = b.No_Faktur and b.Kode_Barang = c.Kode_Barang and b.Kode_Stock_Owner = c.Kode_Stock_Owner and a.ID_Rencana = d.ID_Rencana "
            SQL = SQL & " and a.id_rencana = '" & ListView1.FocusedItem.SubItems(5).Text & "' "
            SQL = SQL & " order by a.no_faktur"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For index As Integer = 0 To .Rows.Count - 1

                        DataGridView2.Rows.Add(1)
                        DataGridView2.Rows.Item(index).Cells(0).Value = .Rows(index).Item("no_faktur")
                        DataGridView2.Rows.Item(index).Cells(1).Value = .Rows(index).Item("lokasi")
                        DataGridView2.Rows.Item(index).Cells(2).Value = .Rows(index).Item("lokasi_tujuan")
                        DataGridView2.Rows.Item(index).Cells(3).Value = .Rows(index).Item("kode_barang")
                        DataGridView2.Rows.Item(index).Cells(4).Value = .Rows(index).Item("nama")
                        DataGridView2.Rows.Item(index).Cells(5).Value = Format(.Rows(index).Item("jumlah"), "N2")
                        DataGridView2.Rows.Item(index).Cells(6).Value = Format(.Rows(index).Item("harga_declare"), "N2")
                        DataGridView2.Rows.Item(index).Cells(7).Value = Format(.Rows(index).Item("total_harga"), "N2")
                        DataGridView2.Rows.Item(index).Cells(8).Value = Format(.Rows(index).Item("volume"), "N2")
                        DataGridView2.Rows.Item(index).Cells(9).Value = Format(.Rows(index).Item("jml_satuan_besar"), "N2")
                        DataGridView2.Rows.Item(index).Cells(10).Value = Format(.Rows(index).Item("isi_satuan_besar"), "N2")
                        DataGridView2.Rows.Item(index).Cells(11).Value = Format(.Rows(index).Item("berat_bersih"), "N2")
                        DataGridView2.Rows.Item(index).Cells(12).Value = Format(.Rows(index).Item("berat_kotor"), "N2")
                        DataGridView2.Rows.Item(index).Cells(13).Value = Format(.Rows(index).Item("total_berat_bersih"), "N2")
                        DataGridView2.Rows.Item(index).Cells(14).Value = Format(.Rows(index).Item("total_berat_kotor"), "N2")
                        DataGridView2.Rows.Item(index).Cells(15).Value = Format(.Rows(index).Item("panjang"), "N2")
                        DataGridView2.Rows.Item(index).Cells(16).Value = Format(.Rows(index).Item("lebar"), "N2")
                        DataGridView2.Rows.Item(index).Cells(17).Value = Format(.Rows(index).Item("tinggi"), "N2")
                        DataGridView2.Rows.Item(index).Cells(18).Value = Format(.Rows(index).Item("biaya_import2"), "N2")
                        'DataGridView2.Rows.Item(index).Cells(19).Value = Format(.Rows(index).Item("nilai_hpp_barang_per_pcs"), "N2")
                        DataGridView2.Rows.Item(index).Cells(19).Value = Format(.Rows(index).Item("biaya_import_wet_dry"), "N2")
                        DataGridView2.Rows.Item(index).Cells(20).Value = Format(.Rows(index).Item("input_hpp"), "N2")
                        DataGridView2.Rows.Item(index).Cells(21).Value = Format(.Rows(index).Item("nilai_hpp_barang_per_pcs_brsh"), "N2")

                    Next
                End With
            End Using



            SQL = " select metode_hitung_konte from stock_owner where kode_Stock_owner = '" & ListView1.FocusedItem.SubItems(6).Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("metode_hitung_konte")) = "A" Then
                        Dr.Close()

                        SQL = " select a.no_faktur, a.kode_Stock_owner, a.kode_kontainer,b.Lokasi, a.dari,  a.harga, a.jumlah_kontainer, a.biaya  "
                        SQL = SQL & " from detail_storage_hpp_a a, pelabuhan b "
                        SQL = SQL & " where a.kode_perusahaan = b.Kode_Perusahaan and a.Kode_Pelabuhan = b.Kode_Pelabuhan and a.no_faktur = '" & ListView1.FocusedItem.SubItems(0).Text & "' "
                        Using Ds = BindingTrans(SQL)
                            With Ds.Tables("MyTable")
                                For index As Integer = 0 To .Rows.Count - 1

                                    DataGridView3.Size = New Size(850, 176)
                                    DataGridView3.Columns(4).Visible = False
                                    DataGridView3.Columns(5).Visible = True
                                    DataGridView3.Columns(6).Visible = True
                                    DataGridView3.Columns(7).Visible = True
                                    DataGridView3.Columns(8).Visible = False

                                    DataGridView3.Rows.Add(1)
                                    DataGridView3.Rows.Item(index).Cells(0).Value = .Rows(index).Item("no_faktur")
                                    DataGridView3.Rows.Item(index).Cells(1).Value = .Rows(index).Item("Kode_Stock_Owner")
                                    DataGridView3.Rows.Item(index).Cells(2).Value = .Rows(index).Item("Kode_Kontainer")
                                    DataGridView3.Rows.Item(index).Cells(3).Value = .Rows(index).Item("lokasi")
                                    DataGridView3.Rows.Item(index).Cells(4).Value = ""
                                    DataGridView3.Rows.Item(index).Cells(5).Value = .Rows(index).Item("Dari")
                                    DataGridView3.Rows.Item(index).Cells(6).Value = Format(.Rows(index).Item("Harga"), "N2")
                                    DataGridView3.Rows.Item(index).Cells(7).Value = .Rows(index).Item("Jumlah_Kontainer")
                                    DataGridView3.Rows.Item(index).Cells(8).Value = ""
                                    DataGridView3.Rows.Item(index).Cells(9).Value = Format(.Rows(index).Item("Biaya"), "N2")


                                Next
                            End With
                        End Using
                    ElseIf General_Class.CekNULL(Dr("metode_hitung_konte")) = "B" Then
                        Dr.Close()
                        SQL = " select a.no_faktur, a.kode_Stock_owner, a.kode_kontainer,b.lokasi,a.no_kontainer,a.jumlah_hari ,a.biaya  "
                        SQL = SQL & " from detail_storage_hpp_b a, pelabuhan b "
                        SQL = SQL & " where a.kode_perusahaan = b.Kode_Perusahaan and a.Kode_Pelabuhan = b.Kode_Pelabuhan and a.no_faktur = '" & ListView1.FocusedItem.SubItems(0).Text & "' "
                        Using Ds = BindingTrans(SQL)
                            With Ds.Tables("MyTable")
                                For index As Integer = 0 To .Rows.Count - 1

                                    DataGridView3.Size = New Size(750, 176)
                                    DataGridView3.Columns(4).Visible = True
                                    DataGridView3.Columns(5).Visible = False
                                    DataGridView3.Columns(6).Visible = False
                                    DataGridView3.Columns(7).Visible = False
                                    DataGridView3.Columns(8).Visible = True

                                    DataGridView3.Rows.Add(1)
                                    DataGridView3.Rows.Item(index).Cells(0).Value = .Rows(index).Item("no_faktur")
                                    DataGridView3.Rows.Item(index).Cells(1).Value = .Rows(index).Item("Kode_Stock_Owner")
                                    DataGridView3.Rows.Item(index).Cells(2).Value = .Rows(index).Item("Kode_Kontainer")
                                    DataGridView3.Rows.Item(index).Cells(3).Value = .Rows(index).Item("Lokasi")
                                    DataGridView3.Rows.Item(index).Cells(4).Value = .Rows(index).Item("No_Kontainer")
                                    DataGridView3.Rows.Item(index).Cells(5).Value = ""
                                    DataGridView3.Rows.Item(index).Cells(6).Value = ""
                                    DataGridView3.Rows.Item(index).Cells(7).Value = ""
                                    DataGridView3.Rows.Item(index).Cells(8).Value = .Rows(index).Item("Jumlah_Hari")
                                    DataGridView3.Rows.Item(index).Cells(9).Value = Format(.Rows(index).Item("Biaya"), "N2")

                                Next
                            End With
                        End Using
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Id Rencana Tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = " select a.no_faktur,b.id_rencana, c.lokasi, a.mata_uang, a.jenis,a.nilai from kurs_hpp_import a, HPP_Import b, Rencana_Order c "
            SQL = SQL & " where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.no_faktur and b.id_rencana = c.id_rencana "
            SQL = SQL & " and b.id_rencana = '" & ListView1.FocusedItem.SubItems(5).Text & "' "
            SQL = SQL & " group by  a.no_faktur,b.id_rencana, c.lokasi, a.mata_uang, a.jenis,a.nilai "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For index As Integer = 0 To .Rows.Count - 1

                        DataGridView4.Rows.Add(1)
                        DataGridView4.Rows.Item(index).Cells(0).Value = .Rows(index).Item("no_faktur")
                        DataGridView4.Rows.Item(index).Cells(1).Value = .Rows(index).Item("lokasi")
                        DataGridView4.Rows.Item(index).Cells(2).Value = .Rows(index).Item("id_rencana")
                        DataGridView4.Rows.Item(index).Cells(3).Value = .Rows(index).Item("mata_uang")
                        DataGridView4.Rows.Item(index).Cells(4).Value = .Rows(index).Item("jenis")
                        DataGridView4.Rows.Item(index).Cells(5).Value = Format(.Rows(index).Item("nilai"), "N2")

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

        cetak("")
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

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

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