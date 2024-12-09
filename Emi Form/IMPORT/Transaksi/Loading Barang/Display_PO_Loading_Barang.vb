
Imports System.Reflection

Public Class Display_PO_Loading_Barang
    Dim Arr1, Arr2, Arr3, arrbatal As New ArrayList
    Dim Clr_Selesai As Color = Color.LightBlue
    Dim Clr_Batal As Color = Color.Black
    Dim Clr_Default As Color = Color.Blue
    Public filter_tambahan As String


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Display_Data_Pembelian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LvRencanaOrder.Columns.Clear()
        LvRencanaOrder.Columns.Add("No Faktur", 100, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("ID Rencana", 80, HorizontalAlignment.Center)
        LvRencanaOrder.Columns.Add("Tgl PO", 100, HorizontalAlignment.Center)
        LvRencanaOrder.Columns.Add("UserID", 80, HorizontalAlignment.Center)
        LvRencanaOrder.Columns.Add("Jenis Transaksi", 90, HorizontalAlignment.Center)
        LvRencanaOrder.Columns.Add("Mata Uang", 80, HorizontalAlignment.Center)
        LvRencanaOrder.Columns.Add("No Rekening", 100, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Kode Supplier", 0, HorizontalAlignment.Center)
        LvRencanaOrder.Columns.Add("Nama Supplier", 130, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Tanggal_PO", 100, HorizontalAlignment.Center)
        LvRencanaOrder.Columns.Add("Kode Kontainer", 80, HorizontalAlignment.Center)
        LvRencanaOrder.View = View.Details

        LvDetailRencanaOrder.Columns.Clear()
        LvDetailRencanaOrder.Columns.Add("Lokasi", 130, HorizontalAlignment.Left)
        LvDetailRencanaOrder.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        LvDetailRencanaOrder.Columns.Add("Nama Barang", 300, HorizontalAlignment.Left)
        LvDetailRencanaOrder.Columns.Add("Jumlah PO", 180, HorizontalAlignment.Center)
        LvDetailRencanaOrder.View = View.Details

        'ChkTgl.Checked = False : ChkParameterLain.Checked = False
        'CmbTgl.Items.Clear() : CmbTgl.Text = "" : Arr1.Clear()
        'CmbTgl.Items.Add("Tanggal") : Arr1.Add("AND ro.tanggal_po")

        'CmbParameterLain.Items.Clear() : CmbParameterLain.Text = "" : Arr2.Clear()
        'CmbParameterLain.Items.Add("ID Rencana") : Arr2.Add("ro.id_rencana")
        'CmbParameterLain.Items.Add("Kode Supplier") : Arr2.Add("ro.kode_supplier")
        'CmbParameterLain.Items.Add("Nama Supplier") : Arr2.Add("s.nama")
        'CmbParameterLain.Items.Add("No. PO") : Arr2.Add("ro.no_po")
        'CmbParameterLain.Items.Add("UserID") : Arr2.Add("ro.UserID")
        'CmbParameterLain.Items.Add("Kode Kontainer") : Arr2.Add("ro.kode_kontainer")
        'CmbParameterLain.Items.Add("No. PO Pembelian") : Arr2.Add("ro.no_po_pembelian")

        'DtpTgl1.Value = CDate(fmenu.ToolStripStatusLabel3.Text) : DtpTgl2.Value = CDate(fmenu.ToolStripStatusLabel3.Text)
        'TxtValue.Text = ""

        'CmbSelesai.Items.Clear() : arrbatal.clear()
        'CmbSelesai.Items.Add("-- Seluruh --") : arrbatal.add("")
        'CmbSelesai.Items.Add("Batal") : arrbatal.Add("AND ro.status = 'Y' ")
        'CmbSelesai.Items.Add("Tdk Batal") : arrbatal.Add("AND ro.status is null ")
        'CmbSelesai.SelectedIndex = 2

        'CmbTgl.Enabled = False : CmbParameterLain.Enabled = False
        'DtpTgl1.Enabled = False : DtpTgl2.Enabled = False
        'TxtValue.Enabled = False

        Try
            OpenConn()

            'CmbLokasi.Items.Clear()
            'CmbLokasi.Items.Add("-- Seluruh --")

            'xSplit = CekKotaRole().Split(",")
            'SQL = "Select kode_stock_owner From "
            'SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and kode_kota in("
            'For i As Integer = 0 To xSplit.Count - 1
            '    SQL = SQL & "'" & xSplit(i).Trim & "', "
            'Next
            'SQL = Strings.Left(SQL, Len(SQL) - 2)
            'SQL = SQL & ") "
            'SQL = SQL & "order by kode_stock_owner"
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read
            '        CmbLokasi.Items.Add(dr("kode_stock_owner"))
            '    Loop
            'End Using

            'CmbLokasi.Text = Lokasi

            'If CekButtonRole("Ganti_Lokasi_Display_Rencana_Order") = "T" Then
            '    CmbLokasi.Enabled = False
            'Else
            '    CmbLokasi.Enabled = True
            'End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            SQL = "SELECT a.No_faktur, a.id_rencana, a.Tanggal, a.UserID, a.Jenis_Transaksi, a.Mata_Uang, a.No_Rekening, a.Kode_supplier, b.nama, c.Tanggal_PO, c.Kode_Kontainer "
            SQL = SQL & "FROM Submit_PO a, suppliers b, Rencana_Order c "
            SQL = SQL & "WHERE a.kode_perusahaan = b.kode_perusahaan AND a.kode_supplier = b.kode_supplier and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.kode_perusahaan = c.kode_perusahaan AND a.id_rencana = c.id_rencana "
            SQL = SQL & "and a.status is null and c.status is null and c.selesai is null and C.Flag_Loading_Barang is null and C.flag_submit_PO ='Y' "
            'If CmbLokasi.SelectedIndex <> 0 Then
            '    SQL = SQL & " AND ro.lokasi = '" & CmbLokasi.Text & "' "
            'End If


            'If CmbLokasi.SelectedIndex = 0 Then
            '    SQL = SQL & "AND ro.Lokasi IN("

            '    Dim List_Kota As String = ""
            '    For x As Integer = 1 To CmbLokasi.Items.Count - 1
            '        List_Kota = List_Kota & "'" & CmbLokasi.Items(x).ToString & "', "
            '    Next
            '    List_Kota = Strings.Left(List_Kota, Len(List_Kota) - 2)

            '    SQL = SQL & List_Kota & ")"
            'Else
            '    SQL = SQL & "AND ro.Lokasi = '" & CmbLokasi.Text & "'"
            'End If


            'SQL = SQL & arrbatal.Item(CmbSelesai.SelectedIndex)

            'If ChkTransaksiHariIni.Checked Then
            '    SQL = SQL & "AND ro.tanggal_po BETWEEN '"
            '    SQL = SQL & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' AND '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' "
            'End If

            'If ChkTgl.Checked Then
            '    SQL = SQL & Arr1.Item(CmbTgl.SelectedIndex) & " BETWEEN '"
            '    SQL = SQL & Format(DtpTgl1.Value, "yyyy-MM-dd") & "' AND '" & Format(DtpTgl2.Value, "yyyy-MM-dd") & "' "
            'End If

            'If ChkParameterLain.Checked Then
            '    SQL = SQL & "AND " & Arr2.Item(CmbParameterLain.SelectedIndex) & " LIKE '%" & Trim(TxtValue.Text) & "%' "
            'End If

            SQL = SQL & "Order BY a.tanggal Desc "

            LvRencanaOrder.Items.Clear() : LvDetailRencanaOrder.Items.Clear()
            Dim Lvw As ListViewItem
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Lvw = LvRencanaOrder.Items.Add(.Rows(i).Item("No_faktur"))
                        Lvw.SubItems.Add(.Rows(i).Item("id_rencana"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item("Tanggal"), "dd-MMM-yyyy"))
                        Lvw.SubItems.Add(.Rows(i).Item("UserID"))
                        Lvw.SubItems.Add(.Rows(i).Item("Jenis_Transaksi"))
                        Lvw.SubItems.Add(.Rows(i).Item("Mata_Uang"))
                        Lvw.SubItems.Add(.Rows(i).Item("No_Rekening"))
                        Lvw.SubItems.Add(.Rows(i).Item("Kode_supplier"))
                        Lvw.SubItems.Add(.Rows(i).Item("nama"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item("Tanggal_PO"), "dd-MMM-yyyy"))
                        Lvw.SubItems.Add(.Rows(i).Item("Kode_Kontainer"))
                        LvRencanaOrder.Items(i).ForeColor = Clr_Default

                        'If General_Class.CekNULL(.Rows(i).Item("selesai")) = "Y" Then
                        '    LvRencanaOrder.Items(i).BackColor = Clr_Selesai
                        'End If
                        'Lvw.SubItems.Add(.Rows(i).Item("Kode_Stock_Owner_import"))
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

    Private Sub LvRencanaOrder_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LvRencanaOrder.DoubleClick
        Try
            If LvRencanaOrder.Items.Count = 0 Then Exit Sub
            Loading_Barang_Import.Kosong()
            OpenConn()
            SQL = "SELECT a.No_faktur, a.id_rencana, a.Tanggal, a.UserID, a.Jenis_Transaksi, a.Mata_Uang, a.No_Rekening, a.Kode_supplier, b.nama, c.Tanggal_PO, c.Kode_Kontainer, d.Kode_stock_owner_import, cast(RV as bigint) as rv, c.Total_Persen, c.lokasi  "
            SQL = SQL & "FROM Submit_PO a, suppliers b, Rencana_Order c, supplier_stock_owner d "
            SQL = SQL & "WHERE a.kode_perusahaan = b.kode_perusahaan AND a.kode_supplier = b.kode_supplier and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.kode_perusahaan = c.kode_perusahaan AND a.id_rencana = c.id_rencana "
            SQL = SQL & "and a.kode_perusahaan = d.kode_perusahaan AND a.kode_supplier = d.kode_supplier "
            SQL = SQL & "and a.status is null and c.status is null and c.Flag_Loading_Barang is null and c.flag_submit_PO ='Y' AND a.No_Faktur = '" & LvRencanaOrder.FocusedItem.Text & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Loading_Barang_Import.TxtContainer.Text = (.Rows(i).Item("Kode_kontainer"))
                        Loading_Barang_Import.Txtfaktur.Text = (.Rows(i).Item("No_faktur"))

                        Dim kontainer As Integer = (.Rows(i).Item("total_persen")) / 100
                        Dim jumlah As Integer = kontainer * 100
                        Dim selisih As Integer = (.Rows(i).Item("total_persen")) - jumlah

                        If selisih = 0 Or selisih <= 99 Then
                            Loading_Barang_Import.TxtJumlah_conte.Text = kontainer
                        Else
                            Loading_Barang_Import.TxtJumlah_conte.Text = kontainer + 1
                        End If

                        'Loading_Barang_Import.TextBoxRV.Text = (.Rows(i).Item("rv"))
                        Loading_Barang_Import.DtTanggal_Po.Value = (Format(.Rows(i).Item("tanggal_po"), "dd-MMM-yyyy"))
                        Loading_Barang_Import.TxtSupplier.Text = (.Rows(i).Item("nama"))
                        Loading_Barang_Import.TextBox1.Text = (.Rows(i).Item("kode_supplier"))
                        Loading_Barang_Import.TextBox2.Text = (.Rows(i).Item("Kode_Stock_Owner_import"))
                        Loading_Barang_Import.TextBox2.Text = (.Rows(i).Item("Kode_Stock_Owner_import"))
                        Loading_Barang_Import.TxtId_Rencana.Text = (.Rows(i).Item("id_rencana"))
                        '''PERUBAHAN PADA EMI
                        Loading_Barang_Import.CmbLokasi.Text = (.Rows(i).Item("lokasi"))
                        '------------------------
                        Loading_Barang_Import.TxtId_Rencana_Leave(LvRencanaOrder, e)
                        Loading_Barang_Import.get_Kontainer()
                        Loading_Barang_Import.Cek_Bahan()

                    Next
                End With
            End Using

            Me.Close()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    'Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkTgl.CheckedChanged
    '    If ChkTgl.Checked Then
    '        CmbTgl.Enabled = True : DtpTgl1.Enabled = True : DtpTgl2.Enabled = True
    '        ChkTransaksiHariIni.Checked = False
    '    Else
    '        CmbTgl.Enabled = False : DtpTgl1.Enabled = False : DtpTgl2.Enabled = False
    '        CmbTgl.SelectedIndex = -1 : DtpTgl1.Value = CDate(fmenu.ToolStripStatusLabel3.Text) : DtpTgl2.Value = CDate(fmenu.ToolStripStatusLabel3.Text)
    '    End If
    'End Sub

    'Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkParameterLain.CheckedChanged
    '    If ChkParameterLain.Checked Then
    '        CmbParameterLain.Enabled = True : TxtValue.Enabled = True
    '    Else
    '        CmbParameterLain.Enabled = False : TxtValue.Enabled = False
    '        CmbParameterLain.SelectedIndex = -1 : TxtValue.Text = ""
    '    End If
    'End Sub

    'Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkTransaksiHariIni.CheckedChanged
    '    If ChkTransaksiHariIni.Checked = True Then
    '        ChkTgl.Checked = False
    '        Button1_Click(ChkTransaksiHariIni, e)
    '    End If
    'End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtCari.Click
    '    If ChkTgl.Checked = False And ChkParameterLain.Checked = False And ChkTransaksiHariIni.Checked = False Then
    '        MessageBox.Show("Pilih terlebih dahulu parameter pencarian data!", Judul)
    '        ChkTgl.Focus() : Exit Sub
    '    End If

    '    If ChkTgl.Checked Then
    '        If CmbTgl.SelectedIndex = -1 Then
    '            MessageBox.Show("Parameter pencarian per tanggal harus diisi!", Judul)
    '            CmbTgl.Focus() : Exit Sub
    '        ElseIf DtpTgl1.Value > DtpTgl2.Value Then
    '            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
    '            DtpTgl1.Value = CDate(fmenu.ToolStripStatusLabel3.Text) : DtpTgl2.Value = CDate(fmenu.ToolStripStatusLabel3.Text)
    '            Exit Sub
    '        End If
    '    End If
    '    If ChkParameterLain.Checked Then
    '        If CmbParameterLain.SelectedIndex = -1 Then
    '            MessageBox.Show("Parameter lain harus diisi!", Judul)
    '            CmbParameterLain.Focus() : Exit Sub
    '        ElseIf TxtValue.Text.Trim.Length = 0 Then
    '            MessageBox.Show("Value parameter lain harus diisi!", Judul)
    '            TxtValue.Focus() : Exit Sub
    '        End If
    '    End If

    '    Try
    '        OpenConn()

    '        SQL = "SELECT ro.id_rencana, ro.lokasi, ro.kode_supplier, s.nama nama_supplier, ro.no_po, ro.tanggal_po, ro.userid, CAST(ro.rv AS BIGINT) rv, ro.kode_kontainer, ro.total_persen, ro.no_po_pembelian, ro.status, ro.selesai "
    '        SQL = SQL & "FROM rencana_order ro, suppliers s "
    '        SQL = SQL & "WHERE ro.kode_perusahaan = s.kode_perusahaan AND ro.kode_supplier = s.kode_supplier AND ro.kode_perusahaan = '" & KodePerusahaan & "' "

    '        'If CmbLokasi.SelectedIndex <> 0 Then
    '        '    SQL = SQL & " AND ro.lokasi = '" & CmbLokasi.Text & "' "
    '        'End If


    '        If CmbLokasi.SelectedIndex = 0 Then
    '            SQL = SQL & "AND ro.Lokasi IN("

    '            Dim List_Kota As String = ""
    '            For x As Integer = 1 To CmbLokasi.Items.Count - 1
    '                List_Kota = List_Kota & "'" & CmbLokasi.Items(x).ToString & "', "
    '            Next
    '            List_Kota = Strings.Left(List_Kota, Len(List_Kota) - 2)

    '            SQL = SQL & List_Kota & ")"
    '        Else
    '            SQL = SQL & "AND ro.Lokasi = '" & CmbLokasi.Text & "'"
    '        End If


    '        SQL = SQL & arrbatal.Item(CmbSelesai.SelectedIndex)

    '        If ChkTransaksiHariIni.Checked Then
    '            SQL = SQL & "AND ro.tanggal_po BETWEEN '"
    '            SQL = SQL & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' AND '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' "
    '        End If

    '        If ChkTgl.Checked Then
    '            SQL = SQL & Arr1.Item(CmbTgl.SelectedIndex) & " BETWEEN '"
    '            SQL = SQL & Format(DtpTgl1.Value, "yyyy-MM-dd") & "' AND '" & Format(DtpTgl2.Value, "yyyy-MM-dd") & "' "
    '        End If

    '        If ChkParameterLain.Checked Then
    '            SQL = SQL & "AND " & Arr2.Item(CmbParameterLain.SelectedIndex) & " LIKE '%" & Trim(TxtValue.Text) & "%' "
    '        End If

    '        SQL = SQL & "Order BY ro.lokasi, ro.tanggal_po Desc "

    '        LvRencanaOrder.Items.Clear() : LvDetailRencanaOrder.Items.Clear()
    '        Dim Lvw As ListViewItem
    '        Using Ds = BindingTrans(SQL)
    '            With Ds.Tables("MyTable")
    '                For i As Integer = 0 To .Rows.Count - 1
    '                    Lvw = LvRencanaOrder.Items.Add(.Rows(i).Item("id_rencana"))
    '                    Lvw.SubItems.Add(.Rows(i).Item("lokasi"))
    '                    Lvw.SubItems.Add("")
    '                    Lvw.SubItems.Add(.Rows(i).Item("nama_supplier"))
    '                    Lvw.SubItems.Add(.Rows(i).Item("no_po"))
    '                    Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_po"), "dd-MMM-yyyy"))
    '                    Lvw.SubItems.Add(.Rows(i).Item("userid"))
    '                    Lvw.SubItems.Add(.Rows(i).Item("rv"))
    '                    Lvw.SubItems.Add(.Rows(i).Item("kode_kontainer"))
    '                    Lvw.SubItems.Add(.Rows(i).Item("total_persen"))
    '                    Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("no_po_pembelian")))

    '                    LvRencanaOrder.Items(i).ForeColor = Clr_Default

    '                    If General_Class.CekNULL(.Rows(i).Item("selesai")) = "Y" Then
    '                        LvRencanaOrder.Items(i).BackColor = Clr_Selesai
    '                    End If

    '                    If General_Class.CekNULL(.Rows(i).Item("status")) = "Y" Then
    '                        LvRencanaOrder.Items(i).ForeColor = Clr_Batal
    '                    End If
    '                Next
    '            End With
    '        End Using

    '        CloseConn()
    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    'End Sub

    'Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtValue.KeyPress
    '    If e.KeyChar = Chr(13) Then Button1_Click(TxtValue, e)
    'End Sub

    'Private Sub CheckBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ChkTgl.KeyPress
    '    If e.KeyChar = Chr(13) Then
    '        If CmbTgl.Enabled = True Then
    '            CmbTgl.Focus()
    '        Else
    '            ChkParameterLain.Focus()
    '        End If
    '    End If
    'End Sub

    'Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbTgl.KeyPress
    '    If e.KeyChar = Chr(13) Then DtpTgl1.Focus()
    'End Sub

    'Private Sub DateTimePicker1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DtpTgl1.KeyPress
    '    If e.KeyChar = Chr(13) Then DtpTgl2.Focus()
    'End Sub

    'Private Sub DateTimePicker2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DtpTgl2.KeyPress
    '    If e.KeyChar = Chr(13) Then Button1_Click(DtpTgl2, e)
    'End Sub

    'Private Sub CheckBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ChkParameterLain.KeyPress
    '    If e.KeyChar = Chr(13) Then
    '        If CmbParameterLain.Enabled = True Then
    '            CmbParameterLain.Focus()
    '        Else
    '            BtCari.Focus()
    '        End If
    '    End If
    'End Sub

    'Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbParameterLain.KeyPress
    '    If e.KeyChar = Chr(13) Then TxtValue.Focus()
    'End Sub

    'Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbTgl.SelectedIndexChanged
    '    DtpTgl1.Focus()
    'End Sub

    'Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbParameterLain.SelectedIndexChanged
    '    TxtValue.Focus()
    'End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LvRencanaOrder.SelectedIndexChanged
        Try
            If LvRencanaOrder.Items.Count = 0 Then Exit Sub

            OpenConn()

            LvDetailRencanaOrder.Items.Clear()
            SQL = "SELECT a.Kode_stock_owner, a.kode_barang, b.nama as nama_barang, a.Jml_satuan_besar "
            SQL = SQL & "FROM detail_submit_po a, barang b WHERE "
            SQL = SQL & "a.kode_perusahaan = b.kode_Perusahaan AND a.kode_stock_owner = b.kode_stock_owner AND a.kode_barang = b.kode_barang AND "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' AND a.No_Faktur = '" & LvRencanaOrder.FocusedItem.Text & "' "
            SQL = SQL & "ORDER BY a.no_urut"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = LvDetailRencanaOrder.Items.Add(Dr("Kode_stock_owner"))
                    Lvw.SubItems.Add(Dr("kode_barang"))
                    Lvw.SubItems.Add(Dr("nama_barang"))
                    Lvw.SubItems.Add(Format(Dr("Jml_satuan_besar"), "N0"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub LvRencanaOrder_Layout(sender As Object, e As LayoutEventArgs) Handles LvRencanaOrder.Layout

    End Sub

    'Private Sub BatalkanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BatalkanToolStripMenuItem.Click
    '    If LvRencanaOrder.Items.Count = 0 Or LvRencanaOrder.SelectedItems.Count = 0 Then
    '        MessageBox.Show("Pilih dahulu rencana order yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Exit Sub
    '    End If

    '    Dim tanya As String = MessageBox.Show("Yakin akan membatalkan rencana order ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    '    If tanya = vbYes Then
    '        Try
    '            OpenConn()

    '            Cmd.Transaction = Cn.BeginTransaction

    '            If CekButtonRole("batal_rencana_order") = "T" Then
    '                CloseTrans()
    '                CloseConn()
    '                MessageBox.Show("Anda tidak memiliki akses untuk memproses rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                Exit Sub
    '            End If

    '            SQL = "SELECT a.selesai, a.status, no_po_pembelian, CAST(a.rv AS BIGINT) rv FROM rencana_order a WHERE "
    '            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
    '            Using Dr = OpenTrans(SQL)
    '                If Dr.Read Then
    '                    If General_Class.CekNULL(Dr("selesai")) = "Y" Then
    '                        Dr.Close()
    '                        CloseTrans()
    '                        CloseConn()
    '                        MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah selesai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        Exit Sub
    '                    ElseIf General_Class.CekNULL(Dr("status")) = "Y" Then
    '                        Dr.Close()
    '                        CloseTrans()
    '                        CloseConn()
    '                        MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        Exit Sub
    '                    ElseIf General_Class.CekNULL(Dr("no_po_pembelian")) <> "" Then
    '                        Dr.Close()
    '                        CloseTrans()
    '                        CloseConn()
    '                        MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibuat po pembelian!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        Exit Sub
    '                    ElseIf LvRencanaOrder.FocusedItem.SubItems(7).Text <> Dr("rv") Then
    '                        Dr.Close()
    '                        CloseTrans()
    '                        CloseConn()
    '                        MessageBox.Show("Proses tidak dapat dilanjutkan karena terdapat perubahan pada rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        Exit Sub
    '                    End If
    '                Else
    '                    Dr.Close()
    '                    CloseTrans()
    '                    CloseConn()
    '                    MessageBox.Show("ID Rencana Order tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    Exit Sub
    '                End If
    '            End Using

    '            SQL = "UPDATE rencana_order SET status = 'Y' WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
    '            ExecuteTrans(SQL)

    '            SQL = "INSERT INTO log_rencana_order(kode_perusahaan, id_rencana, tanggal, jam, userid) VALUES("
    '            SQL = SQL & "'" & KodePerusahaan & "', '" & LvRencanaOrder.FocusedItem.Text & "', "
    '            SQL = SQL & "'" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
    '            SQL = SQL & "'" & UserID & "')"
    '            ExecuteTrans(SQL)

    '            Cmd.Transaction.Commit()

    '            CloseConn()

    '            MessageBox.Show("Rencana Order berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

    '            LvRencanaOrder.FocusedItem.ForeColor = Clr_Batal
    '        Catch ex As Exception
    '            CloseTrans()
    '            CloseConn()
    '            MessageBox.Show(ex.Message)
    '            Exit Sub
    '        End Try
    '    End If
    'End Sub

    'Private Sub CetakToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CetakToolStripMenuItem.Click
    '    If LvRencanaOrder.Items.Count = 0 Or LvRencanaOrder.SelectedItems.Count = 0 Then
    '        MessageBox.Show("Pilih dahulu rencana order yang mau dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Exit Sub
    '    End If

    '    Try
    '        OpenConn()
    '        Cmd.Transaction = Cn.BeginTransaction

    '        Dim jenis As String = ""
    '        SQL = "select a.Jenis_invoice from suppliers a, rencana_order b where a.kode_perusahaan = '" & KodePerusahaan & "' and a.kode_perusahaan = b.kode_perusahaan and "
    '        SQL = SQL & "a.Kode_supplier = b.kode_supplier and b.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
    '        Using Ds = BindingTrans(SQL)
    '            With Ds.Tables("MyTable")
    '                If .Rows.Count <> 0 Then
    '                    jenis = (.Rows(0).Item("Jenis_invoice"))
    '                Else
    '                    CloseTrans()
    '                    CloseConn()
    '                    MessageBox.Show("Data tidak ditemukan!!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Exit Sub
    '                End If
    '            End With
    '        End Using

    '        SQL = "select id_rencana from rencana_order where kode_perusahaan = '" & KodePerusahaan & "' and id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
    '        Using Ds = BindingTrans(SQL)
    '            If jenis = "1" Then
    '                If Ds.Tables("MyTable").Rows.Count <> 0 Then
    '                    Dim CrDoc As New Commercial_Invoice1

    '                    With A_Place_For_Printing2
    '                        CrDoc.SetDataSource(Ds)
    '                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
    '                        CrDoc.RecordSelectionFormula = "{Rencana_order.Kode_Perusahaan} = '" & KodePerusahaan & "' and " & _
    '                                    "{Rencana_order.id_rencana} = " & LvRencanaOrder.FocusedItem.Text & ""
    '                        'CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd MMM yyyy") & " s/d " & Format(Tgl2.Value, "dd MMM yyyy")
    '                        '.Text = "Laporan Pengajuan"
    '                        .CrystalReportViewer1.ReportSource = CrDoc
    '                        .CrystalReportViewer1.DisplayGroupTree = False
    '                        .Refresh()
    '                        .Show()
    '                        .Focus()
    '                    End With
    '                Else
    '                    CloseTrans()
    '                    CloseConn()
    '                    MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Exit Sub
    '                End If
    '            ElseIf jenis = "2" Then
    '                If Ds.Tables("MyTable").Rows.Count <> 0 Then
    '                    Dim CrDoc As New Commercial_Invoice2

    '                    With A_Place_For_Printing2
    '                        CrDoc.SetDataSource(Ds)
    '                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
    '                        CrDoc.RecordSelectionFormula = "{Rencana_order.Kode_Perusahaan} = '" & KodePerusahaan & "' and " & _
    '                                    "{Rencana_order.id_rencana} = " & LvRencanaOrder.FocusedItem.Text & ""
    '                        'CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd MMM yyyy") & " s/d " & Format(Tgl2.Value, "dd MMM yyyy")
    '                        '.Text = "Laporan Pengajuan"
    '                        .CrystalReportViewer1.ReportSource = CrDoc
    '                        .CrystalReportViewer1.DisplayGroupTree = False
    '                        .Refresh()
    '                        .Show()
    '                        .Focus()
    '                    End With
    '                Else
    '                    CloseTrans()
    '                    CloseConn()
    '                    MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Exit Sub
    '                End If
    '            ElseIf jenis = "3" Then
    '                If Ds.Tables("MyTable").Rows.Count <> 0 Then
    '                    Dim CrDoc As New Commercial_Invoice3

    '                    With A_Place_For_Printing2
    '                        CrDoc.SetDataSource(Ds)
    '                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
    '                        CrDoc.RecordSelectionFormula = "{Rencana_order.Kode_Perusahaan} = '" & KodePerusahaan & "' and " & _
    '                                    "{Rencana_order.id_rencana} = " & LvRencanaOrder.FocusedItem.Text & ""
    '                        'CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd MMM yyyy") & " s/d " & Format(Tgl2.Value, "dd MMM yyyy")
    '                        '.Text = "Laporan Pengajuan"
    '                        .CrystalReportViewer1.ReportSource = CrDoc
    '                        .CrystalReportViewer1.DisplayGroupTree = False
    '                        .Refresh()
    '                        .Show()
    '                        .Focus()
    '                    End With
    '                Else
    '                    CloseTrans()
    '                    CloseConn()
    '                    MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Exit Sub
    '                End If
    '            ElseIf jenis = "4" Then
    '                If Ds.Tables("MyTable").Rows.Count <> 0 Then
    '                    Dim CrDoc As New Commercial_Invoice4

    '                    With A_Place_For_Printing2
    '                        CrDoc.SetDataSource(Ds)
    '                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
    '                        CrDoc.RecordSelectionFormula = "{Rencana_order.Kode_Perusahaan} = '" & KodePerusahaan & "' and " & _
    '                                    "{Rencana_order.id_rencana} = " & LvRencanaOrder.FocusedItem.Text & ""
    '                        'CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd MMM yyyy") & " s/d " & Format(Tgl2.Value, "dd MMM yyyy")
    '                        '.Text = "Laporan Pengajuan"
    '                        .CrystalReportViewer1.ReportSource = CrDoc
    '                        .CrystalReportViewer1.DisplayGroupTree = False
    '                        .Refresh()
    '                        .Show()
    '                        .Focus()
    '                    End With
    '                Else
    '                    CloseTrans()
    '                    CloseConn()
    '                    MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Exit Sub
    '                End If
    '            Else
    '                CloseTrans()
    '                CloseConn()
    '                MessageBox.Show("Jenis Invoice tidak dapat ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                Exit Sub
    '            End If
    '        End Using

    '        Cmd.Transaction.Commit()
    '        CloseConn()
    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    'End Sub
End Class