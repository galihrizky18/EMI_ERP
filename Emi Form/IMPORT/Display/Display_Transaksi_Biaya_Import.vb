Imports System.IO
Imports System.Drawing.Bitmap

Public Class Display_Transaksi_Biaya_Import
    Dim Arr1, Arr2, Arr3, arrbatal As New ArrayList


    Private Sub cetak(ByVal nama_cr As String)
        ''Private Sub cetak()
        'Try

        '    OpenConn()

        '    SQL = "select kode_perusahaan from transaksi_biaya_import where "
        '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.Text & "'"
        '    Using Ds = BindingTrans(SQL)
        '        If Ds.Tables("MyTable").Rows.Count <> 0 Then
        '            Dim CrDoc As New Faktur_Biaya_Import
        '            With A_Place_For_Printing2
        '                CrDoc.SetDataSource(Ds)
        '                CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
        '                CrDoc.RecordSelectionFormula = "{transaksi_biaya_import.Kode_Perusahaan} = '" & KodePerusahaan & "' and " & _
        '                          "{transaksi_biaya_import.no_faktur} = '" & ListView1.FocusedItem.Text & "'"
        '                'CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd MMM yyyy") & " s/d " & Format(Tgl2.Value, "dd MMM yyyy")
        '                .Text = "Laporan Komposisi Barang Jadi"
        '                .CrystalReportViewer1.ReportSource = CrDoc
        '                .CrystalReportViewer1.DisplayGroupTree = False
        '                .Refresh()
        '                .Show()
        '                .Focus()
        '            End With
        '        Else
        '            CloseTrans()
        '            CloseConn()
        '            MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        End If
        '    End Using

        '    CloseConn()

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        '    Exit Sub

        'End Try
    End Sub

    Private Sub Display_Transaksi_Biaya_Import_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ListView1.Columns.Add("No Faktur", 0, HorizontalAlignment.Left)
        ListView1.Columns.Add("Tanggal ", 200, HorizontalAlignment.Left)
        ListView1.Columns.Add("Jam", 100, HorizontalAlignment.Left)
        ListView1.Columns.Add("User ID", 120, HorizontalAlignment.Left)
        ListView1.Columns.Add("Keterangan", 500, HorizontalAlignment.Left)
        ListView1.Columns.Add("Jumlah Kontainer", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Id Rencana", 200, HorizontalAlignment.Center).DisplayIndex = 0
        ListView1.View = View.Details

        Lvdetail.Columns.Add("No Faktur", 100, HorizontalAlignment.Left)
        Lvdetail.Columns.Add("Lokasi", 100, HorizontalAlignment.Left)
        Lvdetail.Columns.Add("Kode Kategori", 100, HorizontalAlignment.Left)
        Lvdetail.Columns.Add("Kode Biaya", 100, HorizontalAlignment.Left)
        Lvdetail.Columns.Add("Nama Biaya", 150, HorizontalAlignment.Left)
        Lvdetail.Columns.Add("Kontainer", 100, HorizontalAlignment.Left)
        Lvdetail.Columns.Add("Jumlah Kontainer", 100, HorizontalAlignment.Left)
        Lvdetail.Columns.Add("Kode Perusahaan", 120, HorizontalAlignment.Left)
        Lvdetail.Columns.Add("Nama Perusahaan", 150, HorizontalAlignment.Left)
        Lvdetail.Columns.Add("Jenis Perhitungan", 100, HorizontalAlignment.Center)
        Lvdetail.Columns.Add("Mata Uang", 70, HorizontalAlignment.Center)
        Lvdetail.Columns.Add("Kurs", 70, HorizontalAlignment.Right)
        Lvdetail.Columns.Add("Biaya", 110, HorizontalAlignment.Right)
        Lvdetail.Columns.Add("Nilai2", 110, HorizontalAlignment.Right)
        'ListView2.Columns.Add("Total", 110, HorizontalAlignment.Right)
        Lvdetail.Columns.Add("Kode Master", 120, HorizontalAlignment.Left)
        Lvdetail.Columns.Add("Avg Biaya", 0, HorizontalAlignment.Right)
        Lvdetail.Columns.Add("Total Avg Biaya", 0, HorizontalAlignment.Right)
        Lvdetail.View = View.Details

        Lvkategori.Columns.Add("No Faktur", 100, HorizontalAlignment.Left)
        Lvkategori.Columns.Add("Kode Master", 250, HorizontalAlignment.Left)
        Lvkategori.Columns.Add("Kode Kategori", 250, HorizontalAlignment.Left)
        Lvkategori.Columns.Add("Total", 180, HorizontalAlignment.Right)
        Lvkategori.Columns.Add("Masuk HPP", 80, HorizontalAlignment.Center)
        Lvkategori.View = View.Details

        CheckBox1.Checked = False : CheckBox3.Checked = False
        DateTimePicker1.Value = CDate(FMenuDev.ToolStripStatusLabel3.Text) : DateTimePicker2.Value = CDate(FMenuDev.ToolStripStatusLabel3.Text)
        DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False

        ComboBox1.Enabled = False
        ComboBox1.Items.Clear() : ComboBox1.Text = "" : Arr1.Clear()
        ComboBox1.Items.Add("Tanggal") : Arr1.Add("a.Tanggal")

        Txt_TotMskHPP.Text = 0
        Txt_TotTdkMskHPP.Text = 0
        Txt_TotFreight.Text = 0

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

            If CekButtonRole("Ganti_Lokasi_Display_Transaksi_Biaya_Import") = "T" Then
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


    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            ComboBox1.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
            CheckBox3.Checked = False

        Else
            ComboBox1.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            ComboBox1.SelectedIndex = -1 : DateTimePicker1.Value = CDate(FMenuDev.ToolStripStatusLabel3.Text) : DateTimePicker2.Value = CDate(FMenuDev.ToolStripStatusLabel3.Text)
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
        If CheckBox1.Checked = False And CheckBox3.Checked = False Then
            MessageBox.Show("Pilih terlebih dahulu parameter pencarian data!", Judul)
            CheckBox1.Focus() : Exit Sub
        End If

        If CheckBox1.Checked Then
            If ComboBox1.SelectedIndex = -1 Then
                MessageBox.Show("Parameter pencarian per tanggal harus diisi!", Judul)
                ComboBox1.Focus() : Exit Sub
            ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
                DateTimePicker1.Value = CDate(FMenuDev.ToolStripStatusLabel3.Text) : DateTimePicker2.Value = CDate(FMenuDev.ToolStripStatusLabel3.Text)
                Exit Sub
            End If
        End If

        Try
            OpenConn()

            SQL = "select a.No_faktur,a.Tanggal,a.jam,a.userid,a.keterangan,a.Jml_kontainer,a.id_rencana from "
            SQL = SQL & "transaksi_biaya_import a, detail_transaksi_biaya_import b  where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_faktur = b.No_faktur and a.status is null "

            If ComboBox6.SelectedIndex = 0 Then
                SQL = SQL & " and b.kode_stock_owner in("
                Dim list_kota As String = ""
                For x As Integer = 1 To ComboBox6.Items.Count - 1
                    list_kota = list_kota & "'" & ComboBox6.Items(x).ToString & "', "
                Next

                list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

                SQL = SQL & list_kota & ")"
            Else
                SQL = SQL & " and b.kode_stock_owner = '" & ComboBox6.Text & "'"
            End If

            If CheckBox1.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & Arr1.Item(ComboBox1.SelectedIndex) & " between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If CheckBox3.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & " a.tanggal between '"
                SQL = SQL & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' and '" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' "
            End If

            SQL = SQL & "group by a.No_faktur,a.Tanggal,a.jam,a.userid,a.keterangan,a.Jml_kontainer,a.id_rencana "
            SQL = SQL & "ORDER BY a.tanggal + a.jam DESC"

            Ds = New DataSet
            Ds = BindingTrans(SQL)

            ListView1.Items.Clear()
            Lvdetail.Items.Clear()
            Dim i As Integer = 0
            Dim j As Integer = 0

            While i <= Ds.Tables("MyTable").Rows.Count - 1
                With Ds.Tables("MyTable").Rows(i)
                    Dim lvi As ListViewItem
                    lvi = ListView1.Items.Add(.Item("no_faktur"))
                    lvi.SubItems.Add(Format(.Item("tanggal"), "dd MMM yyyy"))
                    lvi.SubItems.Add(.Item("jam"))
                    lvi.SubItems.Add(.Item("userid"))
                    lvi.SubItems.Add(.Item("Keterangan"))
                    lvi.SubItems.Add(Format(.Item("Jml_kontainer"), "N0"))
                    lvi.SubItems.Add(.Item("id_rencana"))
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

            Lvdetail.Items.Clear()
            Lvkategori.Items.Clear()
            'SQL = "select b.kode_stock_owner,a.no_faktur,b.kode_biaya,b.kode_kontainer,b.jumlah_kontainer,b.kode_Perusahaan_biaya_import, "
            'SQL = SQL & "b.jenis_perhitungan,b.mata_uang,b.kurs,b.biaya,b.total,b.id_rencana from transaksi_biaya_import a, detail_transaksi_biaya_import b "
            'SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and a.kode_perusahaan ='" & KodePerusahaan & "' and a.no_faktur = b.no_faktur and "
            'SQL = SQL & "a.no_faktur = '" & ListView1.FocusedItem.Text & "' "
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Dim Lvw As ListViewItem
            '        Lvw = ListView2.Items.Add(Dr("kode_stock_owner"))
            '        Lvw.SubItems.Add(Dr("no_faktur"))
            '        Lvw.SubItems.Add(Dr("Kode_biaya"))
            '        Lvw.SubItems.Add(Dr("kode_kontainer"))
            '        Lvw.SubItems.Add(Dr("Jumlah_kontainer"))
            '        Lvw.SubItems.Add(Dr("kode_perusahaan_biaya_import"))
            '        Lvw.SubItems.Add(Dr("jenis_perhitungan"))
            '        Lvw.SubItems.Add(Dr("mata_uang"))
            '        Lvw.SubItems.Add(Format(Dr("kurs"), "N0"))
            '        Lvw.SubItems.Add(Format(Dr("biaya"), setN))
            '        Lvw.SubItems.Add(Format(Dr("total"), "N0"))
            '        Lvw.SubItems.Add(Dr("id_rencana"))

            '    Loop
            'End Using

            SQL = "select a.no_faktur,a.Id_Rencana,a.Kode_Stock_Owner,c.Kode_Kategori_Biaya_Import,"
            SQL = SQL & "a.Kode_Biaya, d.nama as nama_biaya, a.kode_kontainer, a.jml_kontainer, "
            SQL = SQL & "a.kode_perusahaan_biaya_import,e.nama,a.jenis_perhitungan,"
            SQL = SQL & "a.Mata_Uang,a.Kurs,a.biaya,a.Nilai_2,a.total,a.kode_master_kategori_biaya_import,"
            SQL = SQL & "a.avg_biaya,a.total_avg_biaya from detail_transaksi_biaya_import a,"
            SQL = SQL & "Master_Kategori_Biaya_Import b,Kategori_Biaya_Import c,biaya_import d,"
            SQL = SQL & "perusahaan_biaya_import e,transaksi_biaya_import f where "
            SQL = SQL & "a.kode_perusahaan=b.kode_perusahaan and b.kode_perusahaan=c.kode_perusahaan "
            SQL = SQL & "and c.kode_perusahaan=d.kode_perusahaan and d.kode_perusahaan=e.kode_perusahaan "
            SQL = SQL & "and e.kode_perusahaan=f.kode_perusahaan and a.kode_perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.kode_master_kategori_biaya_import=b.kode_master_kategori_biaya_import "
            SQL = SQL & "and b.kode_master_kategori_biaya_import=c.kode_master_kategori_biaya_import "
            SQL = SQL & "and c.kode_kategori_biaya_import=d.Kode_Kategori_Biaya_Import "
            SQL = SQL & "and a.Kode_Biaya=d.Kode_Biaya and a.no_faktur=f.No_Faktur "
            SQL = SQL & "and a.kode_perusahaan_biaya_import=e.Kode_Perusahaan_Biaya_Import "
            SQL = SQL & "and f.id_rencana='" & ListView1.FocusedItem.SubItems(6).Text & "' "
            SQL = SQL & " union all "
            SQL = SQL & "select a.no_faktur,a.Id_Rencana,a.Kode_Stock_Owner,c.Kode_Kategori_Biaya_Import,"
            SQL = SQL & "a.Kode_Biaya, d.nama as nama_biaya, a.kode_kontainer, a.jml_kontainer, "
            SQL = SQL & "a.kode_perusahaan_biaya_import,e.nama,a.jenis_perhitungan,"
            SQL = SQL & "a.Mata_Uang,a.Kurs,a.biaya,a.Nilai_2,a.total,a.kode_master_kategori_biaya_import,"
            SQL = SQL & "a.avg_biaya,a.total_avg_biaya from detail_transaksi_biaya_import3 a,"
            SQL = SQL & "Master_Kategori_Biaya_Import b,Kategori_Biaya_Import c,biaya_import d,"
            SQL = SQL & "perusahaan_biaya_import e,transaksi_biaya_import3 f where "
            SQL = SQL & "a.kode_perusahaan=b.kode_perusahaan and b.kode_perusahaan=c.kode_perusahaan "
            SQL = SQL & "and c.kode_perusahaan=d.kode_perusahaan and d.kode_perusahaan=e.kode_perusahaan "
            SQL = SQL & "and e.kode_perusahaan=f.kode_perusahaan and a.kode_perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.kode_master_kategori_biaya_import=b.kode_master_kategori_biaya_import "
            SQL = SQL & "and b.kode_master_kategori_biaya_import=c.kode_master_kategori_biaya_import "
            SQL = SQL & "and c.kode_kategori_biaya_import=d.Kode_Kategori_Biaya_Import "
            SQL = SQL & "and a.Kode_Biaya=d.Kode_Biaya and a.no_faktur=f.No_Faktur "
            SQL = SQL & "and a.kode_perusahaan_biaya_import=e.Kode_Perusahaan_Biaya_Import "
            SQL = SQL & "and f.id_rencana='" & ListView1.FocusedItem.SubItems(6).Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = Lvdetail.Items.Add(Dr("no_faktur"))
                    Lvw.SubItems.Add(Dr("kode_stock_owner"))
                    Lvw.SubItems.Add(Dr("Kode_Kategori_Biaya_Import"))
                    Lvw.SubItems.Add(Dr("Kode_Biaya"))
                    Lvw.SubItems.Add(Dr("nama_biaya"))
                    Lvw.SubItems.Add(Dr("Kode_Kontainer"))
                    Lvw.SubItems.Add(Dr("jml_kontainer"))
                    Lvw.SubItems.Add(Dr("kode_perusahaan_biaya_import"))
                    Lvw.SubItems.Add(Dr("nama"))
                    Lvw.SubItems.Add(Dr("jenis_perhitungan"))
                    Lvw.SubItems.Add(Dr("mata_uang"))
                    Lvw.SubItems.Add(Format(Dr("kurs"), "N0"))
                    Lvw.SubItems.Add(Format(Dr("biaya"), "N0"))
                    Lvw.SubItems.Add(Format(Dr("nilai_2"), "N0"))

                    Lvw.SubItems.Add(Dr("kode_master_kategori_biaya_import"))
                    Lvw.SubItems.Add(Format(Dr("avg_biaya"), "N0"))
                    Lvw.SubItems.Add(Format(Dr("total_avg_biaya"), "N0"))
                Loop
            End Using

            Dim TotMasukHPP As Double = 0
            Dim TotTidakMasukHPP As Double = 0
            Dim TotFreight As Double = 0

            SQL = "select a.no_faktur,a.kode_master_kategori_biaya_import, c.Kode_Kategori_Biaya_Import,"
            SQL = SQL & "sum(a.total) as Total, a.flag_masuk_HPP, 'A' as Jenis "
            SQL = SQL & "from detail_transaksi_biaya_import a,"
            SQL = SQL & "Master_Kategori_Biaya_Import b,Kategori_Biaya_Import c,biaya_import d,"
            SQL = SQL & "perusahaan_biaya_import e,transaksi_biaya_import f where "
            SQL = SQL & "a.kode_perusahaan=b.kode_perusahaan and b.kode_perusahaan=c.kode_perusahaan "
            SQL = SQL & "and c.kode_perusahaan=d.kode_perusahaan and d.kode_perusahaan=e.kode_perusahaan "
            SQL = SQL & "and e.kode_perusahaan=f.kode_perusahaan and a.kode_perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.kode_master_kategori_biaya_import=b.kode_master_kategori_biaya_import "
            SQL = SQL & "and b.kode_master_kategori_biaya_import=c.kode_master_kategori_biaya_import "
            SQL = SQL & "and c.kode_kategori_biaya_import=d.Kode_Kategori_Biaya_Import "
            SQL = SQL & "and a.Kode_Biaya=d.Kode_Biaya and a.no_faktur=f.No_Faktur "
            SQL = SQL & "and a.kode_perusahaan_biaya_import=e.Kode_Perusahaan_Biaya_Import "
            SQL = SQL & "and f.id_rencana='" & ListView1.FocusedItem.SubItems(6).Text & "' "
            SQL = SQL & "group by a.no_faktur,a.kode_master_kategori_biaya_import, c.Kode_Kategori_Biaya_Import, a.flag_masuk_HPP  "

            SQL = SQL & " union all "

            SQL = SQL & "select a.no_faktur,a.kode_master_kategori_biaya_import, c.Kode_Kategori_Biaya_Import,"
            SQL = SQL & "sum(a.total) as Total, a.flag_masuk_HPP, 'B' as Jenis  "
            SQL = SQL & "from detail_transaksi_biaya_import3 a,"
            SQL = SQL & "Master_Kategori_Biaya_Import b,Kategori_Biaya_Import c,biaya_import d,"
            SQL = SQL & "perusahaan_biaya_import e,transaksi_biaya_import3 f where "
            SQL = SQL & "a.kode_perusahaan=b.kode_perusahaan and b.kode_perusahaan=c.kode_perusahaan "
            SQL = SQL & "and c.kode_perusahaan=d.kode_perusahaan and d.kode_perusahaan=e.kode_perusahaan "
            SQL = SQL & "and e.kode_perusahaan=f.kode_perusahaan and a.kode_perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.kode_master_kategori_biaya_import=b.kode_master_kategori_biaya_import "
            SQL = SQL & "and b.kode_master_kategori_biaya_import=c.kode_master_kategori_biaya_import "
            SQL = SQL & "and c.kode_kategori_biaya_import=d.Kode_Kategori_Biaya_Import "
            SQL = SQL & "and a.Kode_Biaya=d.Kode_Biaya and a.no_faktur=f.No_Faktur "
            SQL = SQL & "and a.kode_perusahaan_biaya_import=e.Kode_Perusahaan_Biaya_Import "
            SQL = SQL & "and f.id_rencana='" & ListView1.FocusedItem.SubItems(6).Text & "' "
            SQL = SQL & "group by a.no_faktur,a.kode_master_kategori_biaya_import, c.Kode_Kategori_Biaya_Import, a.flag_masuk_HPP  "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = Lvkategori.Items.Add(Dr("no_faktur"))
                    Lvw.SubItems.Add(Dr("kode_master_kategori_biaya_import"))
                    Lvw.SubItems.Add(Dr("Kode_Kategori_Biaya_Import"))
                    Lvw.SubItems.Add(Format(Dr("Total"), "N2"))

                    If General_Class.CekNULL(Dr("flag_masuk_HPP")) = "" Then
                        Lvw.SubItems.Add("T")
                    Else
                        Lvw.SubItems.Add(Dr("flag_masuk_HPP"))
                    End If


                    If General_Class.CekNULL(Dr("flag_masuk_HPP")) = "" Then
                        Lvw.SubItems.Add("T")
                        TotTidakMasukHPP = TotTidakMasukHPP + Val(HilangkanTanda(Dr("Total")))
                    Else
                        If Dr("flag_masuk_HPP") = "Y" Then
                            TotMasukHPP = TotMasukHPP + Val(HilangkanTanda(Dr("Total")))
                        Else
                            TotTidakMasukHPP = TotTidakMasukHPP + Val(HilangkanTanda(Dr("Total")))
                        End If
                    End If

                    If Dr("Jenis") = "B" Then
                        TotFreight = TotFreight + Val(HilangkanTanda(Dr("Total")))
                    End If

                Loop

                Txt_TotMskHPP.Text = Format(TotMasukHPP, "N2")
                Txt_TotTdkMskHPP.Text = Format(TotTidakMasukHPP, "N2")
                Txt_TotFreight.Text = Format(TotFreight, "N2")

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
            '        Dim CrDoc As New Faktur_Biaya_Import_Grouping_New
            '        With A_Place_For_Printing2
            '            CrDoc.SetDataSource(Ds)
            '            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '            CrDoc.RecordSelectionFormula = "{transaksi_biaya_import.Kode_Perusahaan} = '" & KodePerusahaan & "' and " & _
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

            If CekButtonRole("Batal_Transaksi_Biaya_Import") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


            SQL = "select Status from Rencana_Order ro where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Rencana = '" & ListView1.FocusedItem.SubItems(6).Text & "' and "
            SQL = SQL & "ro.flag_submit_po = 'Y' and ro.flag_loading_barang = 'Y' and ro.flag_otw = 'Y' and ro.Flag_Draft = 'Y' and ro.Flag_Final = 'Y' "
            SQL = SQL & "and ro.Flag_Kirim = 'Y' and ro.Flag_Finish = 'Y' and ro.flag_kapal_tiba = 'Y' and ro.flag_penjaluran = 'Y' and ro.flag_sppb = 'Y' "
            SQL = SQL & "and ro.flag_bongkar = 'Y' and ro.flag_lokasi_tujuan = 'Y' and ro.flag_sudah_transaksi ='Y' and ro.flag_sudah_transaksi3 is null "
            SQL = SQL & "and ro.flag_billing is null and ro.flag_hpp is null and ro.Flag_Barang_Masuk is null and ro.Flag_Selisih_Barang_Masuk is null and ro.Flag_Pembelian is null"
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


            SQL = "select Status from Transaksi_Biaya_Import where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.Text & "'"
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


            SQL = "select a.flag_average, b.Kode_supplier, a.ID from "
            SQL = SQL & "transaksi_Biaya_Import a, Rencana_Order B where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.Id_rencana = b.Id_Rencana and No_faktur = '" & ListView1.FocusedItem.Text & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            If General_Class.CekNULL(.Rows(i).Item("flag_average")) = "Y" Then

                                Dim id As String = .Rows(i).Item("ID")
                                SQL = "select top(1) a.no_faktur, a.ID from transaksi_Biaya_Import a , Rencana_Order B where "
                                SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.Id_rencana = b.Id_Rencana "
                                SQL = SQL & "and b.Kode_Supplier = '" & .Rows(i).Item("Kode_supplier") & "' and a.Flag_Average = 'Y' and a.status is null order by a.ID desc "
                                Using Dr2 = OpenTrans(SQL)
                                    If Dr2.Read Then
                                        If Dr2("ID") <> id Then
                                            Dr2.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Pembatalan Hanya Bisa Untuk Data Terakhir Pada Supplier Ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        Dr2.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data Suppliers Tidak Di temukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using


                                SQL = "update Detail_Selisih_Transaksi_Biaya_Import_By_Kategori set "
                                SQL = SQL & "status ='Y' where No_Faktur =  '" & ListView1.FocusedItem.Text & "'"
                                ExecuteTrans(SQL)


                                SQL = "select Urut_Selisih_Lama from avg_kategori_Biaya_import "
                                SQL = SQL & "where No_Faktur = '" & ListView1.FocusedItem.Text & "'"
                                Using Ds2 = BindingTrans(SQL)
                                    If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                        For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1
                                            If General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Urut_Selisih_Lama")) <> "" Then

                                                SQL = "Select Urut from Detail_Selisih_Transaksi_Biaya_Import_By_Kategori where "
                                                SQL = SQL & "status is null and Pakai = 'Y' and Urut =  '" & Ds2.Tables("MyTable").Rows(j).Item("Urut_Selisih_Lama") & "'"
                                                Using Dr = OpenTrans(SQL)
                                                    If Dr.Read Then

                                                        Dr.Close()
                                                        SQL = "update Detail_Selisih_Transaksi_Biaya_Import_By_Kategori set "
                                                        SQL = SQL & "Pakai = NULL where Urut =  '" & Ds2.Tables("MyTable").Rows(j).Item("Urut_Selisih_Lama") & "'"
                                                        ExecuteTrans(SQL)

                                                    Else
                                                        Dr.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        MessageBox.Show("Data Selisih Tidak Di temukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End Using

                                            End If
                                        Next

                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Faktur Tidak Di temukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                            End If
                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Faktur Tidak Di temukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            SQL = "Update Transaksi_Biaya_Import set status = 'Y' where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & ListView1.FocusedItem.Text & "'"
            ExecuteTrans(SQL)

            SQL = "Update Rencana_Order set Flag_Sudah_Transaksi = null where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "id_rencana = '" & ListView1.FocusedItem.SubItems(6).Text & "'"
            ExecuteTrans(SQL)

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
            '            CrDoc.RecordSelectionFormula = "{transaksi_biaya_import.Kode_Perusahaan} = '" & KodePerusahaan & "' and " & _
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
End Class