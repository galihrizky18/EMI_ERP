Public Class EMI_Pembelian_PR_Summary_Data

    Dim Arr1, Arr2, Arr3, Arr4 As New ArrayList
    Dim pertama As Integer = 1
    Dim T As Color = Color.Blue
    Dim KT As Color = Color.Red
    Dim KY As Color = Color.Green
    Dim Batal As Color = Color.Black

    Private Sub Display_Pembelian_Barang_Masuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong()
    End Sub

    Private Sub kosong()

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Display_Barang_Masuk")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Pembelian_Barang_Masuk")
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Lv_PR.Items.Clear() : Lv_PR.Columns.Clear()
        Lv_PR.Columns.Add(Base_Language.Lang_Global_NoFaktur, 170, HorizontalAlignment.Left)
        Lv_PR.Columns.Add("PR Created", 110, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("PR Released", 110, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("Keterangan", 400, HorizontalAlignment.Left)
        Lv_PR.Columns.Add("Status PR", 100, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("User ID", 100, HorizontalAlignment.Left)
        Lv_PR.Columns.Add("Status", 100, HorizontalAlignment.Center)
        Lv_PR.View = View.Details

        Lv_PRDetail.Items.Clear() : Lv_PRDetail.Columns.Clear()
        Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left)
        Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_NamaBarang, 200, HorizontalAlignment.Left)
        Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_Satuan, 100, HorizontalAlignment.Center)
        Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_Jumlah, 100, HorizontalAlignment.Center)
        Lv_PRDetail.Columns.Add("Jumlah PO", 110, HorizontalAlignment.Center)
        Lv_PRDetail.Columns.Add("Sisa", 110, HorizontalAlignment.Center)
        Lv_PRDetail.Columns.Add("%Complete", 110, HorizontalAlignment.Center)
        Lv_PRDetail.Columns.Add("UrutPR", 0, HorizontalAlignment.Center)
        Lv_PRDetail.Columns.Add("NoFaktur", 0, HorizontalAlignment.Center)
        'Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_Harga, 110, HorizontalAlignment.Right)
        'Lv_PRDetail.Columns.Add("Jumlah Masuk", 110, HorizontalAlignment.Right)
        'Lv_PRDetail.Columns.Add("Sisa", 110, HorizontalAlignment.Right)
        'Lv_PRDetail.Columns.Add("%Complete", 110, HorizontalAlignment.Right)
        'Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_Total, 140, HorizontalAlignment.Right)
        'ListView2.Columns.Add(Base_Language.Lang_Pmb_Barang_Masuk_Tanggal_produksi, 120, HorizontalAlignment.Center)
        'ListView2.Columns.Add(Base_Language.Lang_Pmb_Barang_Masuk_Tanggal_Expire, 120, HorizontalAlignment.Center)
        Lv_PRDetail.View = View.Details

        Lv_Detail.Items.Clear() : Lv_Detail.Columns.Clear()
        Lv_Detail.Columns.Add("No PO", 150, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Tanggal PO", 100, HorizontalAlignment.Center)
        Lv_Detail.Columns.Add("Lokasi", 200, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Jumlah", 200, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        Lv_Detail.View = View.Details




        Try
            OpenConn()

            ComboBox6.Items.Clear()
            ComboBox6.Items.Add(Base_Language.Lang_Global_SeluruhCombobox)

            'xSplit = CekKotaRole().Split(", ")

            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and kode_kota in( "
            'For i As Integer = 0 To xSplit.Count - 1
            '    SQL = SQL & "'" & xSplit(i).Trim & "', "
            'Next
            'SQL = Strings.Left(SQL, Len(SQL) - 2)

            'SQL = SQL & ") "
            SQL = SQL & "order by kode_stock_owner"
            'ComboBox1.Items.Add("Seluruh")
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox6.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using


            ComboBox6.Text = Lokasi

            If CekButtonRole("Ganti_Lokasi_Display_Penjualan") = "T" Then
                ComboBox6.Enabled = False
            Else
                ComboBox6.Enabled = True
            End If

            'ComboBox3.Items.Add("Y") : Arr4.Add("Y")
            'ComboBox3.Items.Add("T") : Arr4.Add("T")
            'ComboBox3.SelectedIndex = 1

            ComboBox3.Items.Clear() : Arr1.Clear()
            ComboBox3.Items.Add("Tanggal") : Arr1.Add("Tanggal")

            'TextBoxa.Text = "0" 
            ComboBox3.Enabled = False : ComboBox2.Enabled = False
            DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            TextBox4.Enabled = False

            ComboBox2.Items.Clear() : ComboBox2.Text = "" : Arr2.Clear()
            ComboBox2.Items.Add("No Faktur") : Arr2.Add("no_faktur")
            'ComboBox2.Items.Add("NO Nota") : Arr2.Add("a.no_nota")
            'ComboBox2.Items.Add("Kode Supplier") : Arr2.Add("a.kode_supplier")

            Label1.Text = "Summary Data - Purchase Requisition"
            CheckBox3.Text = Base_Language.Lang_Global_Hari_ini
            CheckBox1.Text = Base_Language.Lang_Global_Para_Tbl
            CheckBox2.Text = Base_Language.Lang_Global_Para_lain
            BtnBarangMasuk_Cari.Text = Base_Language.Lang_Global_Cari
            CloseConn()
        Catch ex As Exception
            ComboBox6.Items.Clear()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub GroupBox3_Enter(sender As Object, e As EventArgs) Handles GroupBox3.Enter

    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox1.Checked = False
            BtnBarangMasuk_Cari_Click(CheckBox3, e)
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_PR.SelectedIndexChanged
        Try
            OpenConn()
            Lv_PRDetail.Items.Clear() : Lv_Detail.Items.Clear()
            'If ListView1.FocusedItem.SubItems(8).Text = "Y" Then
            '    SQL = "select a.Kode_Stock_Owner,a.Kode_Barang,b.Nama,a.jumlah,a.Satuan,a.Nilai_Pengali,a.Satuan_Barang,a.Nilai_Barang, "
            '    SQL = SQL & "a.Tgl_Produksi,a.Tgl_Expired from EMI_Pembelian_Barang_Masuk_Detail a, barang b "
            '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang "
            '    SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "'and a.no_faktur = '" & ListView1.FocusedItem.Text & "' "
            '    SQL = SQL & "group by  a.Kode_Stock_Owner,a.Kode_Barang,b.Nama,a.jumlah,a.Satuan,a.Nilai_Pengali,a.Satuan_Barang,a.Nilai_Barang,a.Tgl_Produksi,a.Tgl_Expired "

            '    SQL = SQL & "order by kode_barang "
            'Else
            '    SQL = "select a.Kode_Stock_Owner,a.Kode_Barang,b.Nama,a.jumlah,a.Satuan,a.Nilai_Pengali,a.Satuan_Barang,a.Nilai_Barang, "
            '    SQL = SQL & "a.Tgl_Produksi,a.Tgl_Expired from EMI_Pembelian_Barang_Masuk_Sementara_Det a, barang b "
            '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang "
            '    SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "'and a.no_faktur = '" & ListView1.FocusedItem.Text & "' "
            '    SQL = SQL & "group by  a.Kode_Stock_Owner,a.Kode_Barang,b.Nama,a.jumlah,a.Satuan,a.Nilai_Pengali,a.Satuan_Barang,a.Nilai_Barang,a.Tgl_Produksi,a.Tgl_Expired "

            '    SQL = SQL & "order by kode_barang "
            'End If

            'SQL = "select a.kode_stock_owner, a.Kode_Barang,b.Nama,a.jumlah,a.Satuan,a.Harga,a.Satuan,a.Nilai_Barang From EMI_Purchase_Requisition_Detail a, barang b "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and  "
            'SQL = SQL & "a.Kode_Stock_Owner = b.Kode_Stock_Owner  and a.Kode_Barang = b.Kode_Barang "
            'SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.no_faktur = '" & Lv_PR.FocusedItem.SubItems(0).Text & "' "
            SQL = "select a.kode_stock_owner, a.Kode_Barang,b.Nama,a.jumlah,a.Satuan, a.no_urut, a.no_faktur, "
            'jumlah masuk
            SQL = SQL & "isnull((select sum(y.Jumlah) from EMI_Pembelian_PO x, EMI_Pembelian_PO_Det y "
            SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur  "
            SQL = SQL & "and y.Kode_Perusahaan = a.Kode_Perusahaan and y.no_urut_pr = a.No_Urut and x.status is null and x.No_Faktur_Induk is null and y.No_FakInduk is null ),  "
            SQL = SQL & "0) as jumlah_masuk, "

            SQL = SQL & "isnull((select sum(y.Jumlah) from EMI_Pembelian_PO_Induk x, EMI_Pembelian_PO_Det_Induk y "
            SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur "
            SQL = SQL & "and y.Kode_Perusahaan = a.Kode_Perusahaan and y.no_urut_pr = a.No_Urut and x.status is null), "
            SQL = SQL & "0) as jumlah_masuk_new, isnull(a.flag_sudah_po,'T') as flag_selesai_po "
            'sisa
            'SQL = SQL & "(a.jumlah - isnull((select sum(y.Jumlah) from EMI_Pembelian_PO x, EMI_Pembelian_PO_Det y "
            'SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and "
            'SQL = SQL & "y.Kode_Perusahaan = a.Kode_Perusahaan and y.no_urut_pr = a.No_Urut and x.status is null), 0)) "
            'SQL = SQL & "as sisa, "
            ''percentComplete
            'SQL = SQL & "(isnull((select sum(y.Jumlah) from EMI_Pembelian_PO x, EMI_Pembelian_PO_Det y "
            'SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur "
            'SQL = SQL & "and y.Kode_Perusahaan = a.Kode_Perusahaan and y.no_urut_pr = a.No_Urut), "
            'SQL = SQL & "0) / a.jumlah) * 100 as percentComplete "

            SQL = SQL & "From EMI_Purchase_Requisition_Detail a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.Kode_Stock_Owner = b.Kode_Stock_Owner  and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_faktur = '" & Lv_PR.FocusedItem.SubItems(0).Text & "' and a.jumlah<>0 "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim total As Double = Dr("jumlah_masuk") + Dr("jumlah_masuk_new")

                    Dim lvw As ListViewItem
                    lvw = Lv_PRDetail.Items.Add(Dr("kode_barang"))
                    lvw.SubItems.Add(Dr("nama"))
                    lvw.SubItems.Add(Dr("satuan"))
                    lvw.SubItems.Add(Format(Dr("jumlah"), "N2"))
                    lvw.SubItems.Add(Format(total, "N2"))

                    Dim sisa As Double = Dr("jumlah") - total
                    Dim persen As Double = total / Dr("jumlah") * 100

                    If Dr("flag_selesai_po") = "Y" Then
                        lvw.BackColor = Color.LightGreen
                        lvw.SubItems.Add(Format(0, "N2"))
                    Else
                        lvw.SubItems.Add(Format(sisa, "N2"))
                    End If

                    lvw.SubItems.Add(Format(persen, "N2"))
                    lvw.SubItems.Add(Dr("no_urut"))
                    lvw.SubItems.Add(Dr("no_faktur"))


                    'lvw.SubItems.Add(Format(Dr("tgl_produksi"), "dd MMM yyyy"))
                    'lvw.SubItems.Add(Format(Dr("tgl_expired"), "dd MMM yyyy"))
                Loop
            End Using

            'Lv_Detail.Items.Clear()
            'SQL = "select a.No_Faktur, d.No_Faktur as No_PO, c.Kode_Stock_Owner, c.Kode_Barang, c.Jumlah, c.Satuan, d.Tanggal, d.Status "
            'SQL = SQL & "from EMI_Purchase_Requisition a, EMI_Purchase_Requisition_Detail b,EMI_Pembelian_PO_Det_Induk c,EMI_Pembelian_PO_Induk d  "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan  "
            'SQL = SQL & "and a.No_Faktur = b.No_Faktur and c.No_Faktur = d.No_Faktur and b.No_Urut = c.No_Urut_PR  "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Faktur ='" & Lv_PR.FocusedItem.SubItems(0).Text & "' "
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Dim Lv As ListViewItem
            '        Lv = Lv_Detail.Items.Add(Dr("No_PO"))
            '        Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
            '        Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
            '        Lv.SubItems.Add(Dr("Kode_Barang"))
            '        Lv.SubItems.Add(Format(Dr("Jumlah"), "N2"))
            '        Lv.SubItems.Add(Dr("Satuan"))

            '        If General_Class.CekNULL(Dr("Status")) <> "" Then
            '            Lv.BackColor = Color.FromArgb(139, 0, 0)
            '            Lv.ForeColor = Color.White
            '        End If
            '    Loop
            'End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ValidasiInquiryToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub BtnBarangMasuk_Cari_Click(sender As Object, e As EventArgs) Handles BtnBarangMasuk_Cari.Click
        Try
            pertama = 1

            If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False Then
                MessageBox.Show(Base_Language.Lang_Global_Error_Paramater, Judul)
                CheckBox1.Focus() : Exit Sub
            End If

            If CheckBox1.Checked Then
                If ComboBox3.SelectedIndex = -1 Then
                    MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Tgl, Judul)
                    ComboBox3.Focus() : Exit Sub
                ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
                    MessageBox.Show("Periode I " & Base_Language.Lang_Global_TidakBolehLebihDari & " periode II!", Judul)
                    DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
                    Exit Sub
                End If
            ElseIf CheckBox2.Checked Then
                If ComboBox2.SelectedIndex = -1 Then
                    MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain, Judul)
                    ComboBox2.Focus() : Exit Sub
                ElseIf TextBox4.Text.Trim.Length = 0 Then
                    MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain2, Judul)
                    TextBox4.Focus() : Exit Sub
                End If
            End If

            OpenConn()

            Lv_PR.Items.Clear()
            Lv_PRDetail.Items.Clear()
            Lv_Detail.Items.Clear()

            'SQL = "select a.No_Faktur,b.kode_supplier,b.Nama, a.tanggal, a.userid, a.keterangan "
            'SQL = SQL & "from EMI_Purchase_Requisition a, Suppliers b  "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Supplier = b.Kode_Supplier "
            'SQL = SQL & "and a.Status is null "
            SQL = "select No_Faktur, tanggal, tanggal_release, keterangan, userid, Flag_Release, Status "
            SQL = SQL & "from EMI_Purchase_Requisition   "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "

            If CheckBox1.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "and "

                SQL = SQL & Arr1.Item(ComboBox3.SelectedIndex) & " between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If CheckBox2.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & Arr2.Item(ComboBox2.SelectedIndex) & " like '%" & Trim(TextBox4.Text) & "%' "
            End If

            If CheckBox3.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & " tanggal between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If

            If ComboBox6.SelectedIndex = 0 Then
                SQL = SQL & " and Lokasi in("
                Dim list_kota As String = ""
                For x As Integer = 1 To ComboBox6.Items.Count - 1
                    list_kota = list_kota & "'" & ComboBox6.Items(x).ToString & "', "
                Next

                list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

                SQL = SQL & list_kota & ")"
            Else
                SQL = SQL & " and Lokasi = '" & ComboBox6.Text & "' "
            End If

            SQL = SQL & "order by No_Faktur, Tanggal, Tanggal_Release, UserId"


            Dim Lvw As ListViewItem

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Lvw = Lv_PR.Items.Add(.Rows(i).Item("no_faktur"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal"), "dd MMM yyyy"))
                            If General_Class.CekNULL(.Rows(i).Item("tanggal_release")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_release"), "dd MMM yyyy"))
                            End If

                            Lvw.SubItems.Add(.Rows(i).Item("keterangan"))

                            If General_Class.CekNULL(.Rows(i).Item("Flag_Release")) = "Y" Then
                                Lvw.SubItems.Add("SUBMITTED")
                            Else
                                Lvw.SubItems.Add("UNSUBMITTED")
                            End If

                            Lvw.SubItems.Add(.Rows(i).Item("userid"))

                            If General_Class.CekNULL(.Rows(i).Item("Status")) <> "" Then
                                Lvw.BackColor = Color.FromArgb(139, 0, 0)
                                Lvw.ForeColor = Color.White
                            End If

                        Next
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

    Private Sub CetakUlangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangToolStripMenuItem.Click
        If Lv_PR.Items.Count = 0 Or Lv_PR.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "select a.No_Faktur from EMI_Purchase_Requisition a, EMI_Purchase_Requisition_Detail b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur='" & Lv_PR.FocusedItem.Text & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    With Ds.Tables(0)
                        Dim CrDoc As New Faktur_Purchase_Requisition
                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Laporan Faktur Purchase Requisition"
                            CrDoc.RecordSelectionFormula = " {EMI_Purchase_Requisition.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Purchase_Requisition.No_Faktur} = '" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "'"

                            .Text = "Laporan Faktur Purchase Requisition"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                            .Refresh()
                            .Show()
                        End With
                    End With
                Else
                    MessageBox.Show("Data tidak ditemukan!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub BatalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalToolStripMenuItem.Click
        If Lv_PR.Items.Count = 0 Or Lv_PR.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("Pembatalan_PR") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan PR", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin akan membatalkan Purhcase Requisition ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then Exit Sub

            SQL = "select Status from EMI_Purchase_Requisition where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & Lv_PR.FocusedItem.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Status")) <> "" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Purhcase Requisition sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Purhcase Requisition tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select a.No_Faktur from EMI_Purchase_Requisition a,"
            SQL = SQL & "EMI_Purchase_Requisition_Detail b,EMI_Pembelian_PO_Det_Induk c,EMI_Pembelian_PO_Induk d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Status is null and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur "
            SQL = SQL & "and b.No_Urut = c.No_Urut_PR and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and d.Status is null and a.No_Faktur = '" & Lv_PR.FocusedItem.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Purhcase Requisition tidak bisa dibatalkan,karena sudah masuk tahap Purhcase Order!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "Update EMI_Purchase_Requisition set Status = 'Y', "
            SQL = SQL & "UserID_Batal = '" & UserID & "', "
            SQL = SQL & "Tanggal_Batal = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "Jam_Batal = '" & Format(tgl_skg, "HH:mm:ss") & "' "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & Lv_PR.FocusedItem.Text & "' "
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Purhcase Requisition berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        BtnBarangMasuk_Cari_Click(BatalToolStripMenuItem, e)
    End Sub


    Private Sub DisplayRakToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If Lv_PR.Items.Count = 0 Or Lv_PR.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        EMI_Barang_Masuk_Display_Rak.TxtNoBM.Text = Lv_PR.FocusedItem.Text
        EMI_Barang_Masuk_Display_Rak.ShowDialog()
    End Sub


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            ComboBox3.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
            CheckBox3.Checked = False
        Else
            ComboBox3.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            ComboBox3.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            ComboBox2.Enabled = True : TextBox4.Enabled = True
        Else
            ComboBox2.Enabled = False : TextBox4.Enabled = False
            ComboBox2.SelectedIndex = -1 : TextBox4.Text = ""
        End If
    End Sub

    ''Dim arrcari As New ArrayList
    ''Dim Jenis = "Master_Jenis_Hewan"
    ''Private Sub kosong()
    ''    TextBox1.Text = ""
    ''    TextBox2.Text = ""

    ''    ComboBox1.Items.Clear() : arrcari.Clear()
    ''    ComboBox1.Items.Add(Base_Language.Lang_Jenis_Hewan_Kode) : arrcari.Add("kode_jenis_hewan")
    ''    ComboBox1.Items.Add(Base_Language.Lang_Jenis_Hewan_Keterangan) : arrcari.Add("keterangan")
    ''    TextBox3.Text = ""

    ''    Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
    ''    Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
    ''    Btn_Cari.Text = Base_Language.Lang_Global_Cari
    ''    Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
    ''    Btn_Simpan.Tag = "&Simpan"
    ''    Btn_Hapus.Enabled = False

    ''End Sub

    ''Private Sub Cari(ByVal semua As String)
    ''    Try

    ''        OpenConn()

    ''        ListView1.Items.Clear()
    ''        SQL = "Select kode_jenis_hewan, keterangan From emi_jenis_hewan where kode_perusahaan = '" & KodePerusahaan & "' "
    ''        If semua = "T" Then
    ''            SQL = SQL & "and " & arrcari.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
    ''            SQL = SQL & "order by " & arrcari.Item(ComboBox1.SelectedIndex) & " "
    ''        Else
    ''            SQL = SQL & "order by nama"
    ''        End If
    ''        Using dr = OpenTrans(SQL)
    ''            Do While dr.Read
    ''                Dim Lvw As ListViewItem
    ''                Lvw = ListView1.Items.Add(dr("kode_jenis_hewan"))
    ''                Lvw.SubItems.Add(dr("keterangan"))
    ''            Loop
    ''        End Using

    ''        CloseConn()

    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub
    ''    End Try
    ''End Sub
    ''Private Sub Master_Jenis_Hewan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
    ''    My.Application.ChangeCulture("en-us")
    ''    My.Application.ChangeUICulture("en-us")
    ''End Sub

    ''Private Sub Master_Jenis_Hewan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ''    My.Application.ChangeCulture("en-us")
    ''    My.Application.ChangeUICulture("en-us")

    ''    Try
    ''        OpenConn()

    ''        Base_Language.Get_Languages_Global(Bahasa_Pilihan)

    ''        Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

    ''        Label1.Text = Base_Language.Lang_Jenis_Hewan_Judul
    ''        Label2.Text = Base_Language.Lang_Jenis_Hewan_Kode
    ''        Label3.Text = Base_Language.Lang_Jenis_Hewan_Keterangan
    ''        Label4.Text = Base_Language.Lang_Jenis_Hewan_Kolom

    ''        ListView1.Columns.Add(Base_Language.Lang_Jenis_Hewan_Kode, 150, HorizontalAlignment.Left)
    ''        ListView1.Columns.Add(Base_Language.Lang_Jenis_Hewan_Keterangan, 725, HorizontalAlignment.Left)
    ''        ListView1.View = View.Details

    ''        kosong()

    ''        CloseConn()
    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub

    ''    End Try


    ''End Sub

    ''Private Sub TextBox1_Leave(sender As Object, e As EventArgs)
    ''    If TextBox1.Text.Trim.Length = 0 Then Exit Sub

    ''    Try

    ''        OpenConn()

    ''        SQL = "Select kode_jenis_hewan, keterangan From emi_jenis_hewan Where "
    ''        SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
    ''        SQL = SQL & "kode_jenis_hewan = '" & TextBox1.Text.Trim & "'"
    ''        Using Dr = OpenTrans(SQL)
    ''            If Dr.Read Then
    ''                TextBox1.Text = Dr("kode_jenis_hewan")
    ''                TextBox2.Text = Dr("keterangan")

    ''                Btn_Simpan.Text = Base_Language.Lang_Global_Update : Btn_Hapus.Enabled = True
    ''                Btn_Simpan.Tag = "&Update"
    ''            Else
    ''                TextBox2.Text = ""

    ''                Btn_Simpan.Text = Base_Language.Lang_Global_Simpan : Btn_Hapus.Enabled = False
    ''                Btn_Simpan.Tag = "&Simpan"
    ''            End If
    ''        End Using

    ''        CloseConn()
    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub
    ''    End Try
    ''End Sub

    ''Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs)
    ''    If TextBox1.Text.Trim.Length = 0 Then
    ''        MessageBox.Show(Base_Language.Lang_Jenis_Hewan_Error_Kode, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''        TextBox1.Focus() : Exit Sub
    ''    ElseIf TextBox2.Text.Trim.Length = 0 Then
    ''        MessageBox.Show(Base_Language.Lang_Jenis_Hewan_Error_Nama, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''        TextBox2.Focus() : Exit Sub
    ''    End If

    ''    Try

    ''        OpenConn()

    ''        Cmd.Transaction = Cn.BeginTransaction

    ''        If Btn_Simpan.Tag = "&Simpan" Then
    ''            SQL = "Insert Into emi_jenis_hewan(Kode_Perusahaan, kode_jenis_hewan, keterangan) "
    ''            SQL = SQL & "Values('" & KodePerusahaan & "', "
    ''            SQL = SQL & "'" & TextBox1.Text.Trim & "', '" & TextBox2.Text.Trim & "')"
    ''            ExecuteTrans(SQL)
    ''        Else
    ''            SQL = "Update emi_jenis_hewan Set keterangan = '" & TextBox2.Text.Trim & "' "
    ''            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_jenis_hewan = '" & TextBox1.Text.Trim & "'"
    ''            ExecuteTrans(SQL)
    ''        End If

    ''        Cmd.Transaction.Commit()

    ''        CloseConn()

    ''    Catch ex As Exception
    ''        CloseConn()
    ''        MessageBox.Show(ex.Message)
    ''        Exit Sub
    ''    End Try

    ''    kosong()
    ''    TextBox1.Focus()
    ''End Sub

    ''Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs)
    ''    Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    ''    If Hapus1 = vbYes Then

    ''        Try

    ''            OpenConn()

    ''            Cmd.Transaction = Cn.BeginTransaction

    ''            SQL = "Delete From emi_jenis_hewan where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_jenis_hewan = '" & TextBox1.Text.Trim & "'"
    ''            ExecuteTrans(SQL)

    ''            Cmd.Transaction.Commit()

    ''            CloseConn()
    ''        Catch ex As Exception
    ''            CloseTrans()
    ''            CloseConn()
    ''            MessageBox.Show(ex.Message)
    ''            Exit Sub
    ''        End Try

    ''    Else
    ''        MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    ''    End If

    ''    kosong()
    ''    TextBox1.Focus()
    ''End Sub

    ''Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs)
    ''    kosong()
    ''End Sub

    ''Private Sub Btn_Cari_Click(sender As Object, e As EventArgs)
    ''    If ComboBox1.Text.Trim.Length = 0 Then Exit Sub
    ''    If TextBox3.Text.Trim.Length = 0 Then Exit Sub

    ''    Cari("T")
    ''End Sub

    ''Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
    ''    If e.KeyChar = Chr(13) Then TextBox2.Focus()
    ''End Sub

    ''Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs)
    ''    If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    ''End Sub

    ''Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    ''End Sub

    ''Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
    ''    If e.KeyChar = Chr(13) Then TextBox3.Focus()
    ''End Sub

    ''Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs)
    ''    If e.KeyChar = Chr(13) Then Btn_Cari_Click(TextBox3, e)
    ''End Sub

    ''Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs)

    ''End Sub


    Private Sub Lv_PRDetail_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_PRDetail.SelectedIndexChanged
        If Lv_PRDetail.Items.Count = 0 Or Lv_PRDetail.FocusedItem.Index = -1 Then Exit Sub

        Try
            OpenConn()

            Lv_Detail.Items.Clear()
            SQL = "select a.No_Faktur, d.No_Faktur as No_PO, c.Kode_Stock_Owner, c.Kode_Barang, c.Jumlah, c.Satuan, d.Tanggal, d.Status "
            SQL = SQL & "from EMI_Purchase_Requisition a, EMI_Purchase_Requisition_Detail b,EMI_Pembelian_PO_Det_Induk c,EMI_Pembelian_PO_Induk d  "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan  "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur and c.No_Faktur = d.No_Faktur and b.No_Urut = c.No_Urut_PR and b.No_Urut = c.No_Urut_PR "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur ='" & Lv_PRDetail.FocusedItem.SubItems(8).Text & "' "
            SQL = SQL & "and b.No_Urut = '" & Lv_PRDetail.FocusedItem.SubItems(7).Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail.Items.Add(Dr("No_PO"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N2"))
                    Lv.SubItems.Add(Dr("Satuan"))

                    If General_Class.CekNULL(Dr("Status")) <> "" Then
                        Lv.BackColor = Color.FromArgb(139, 0, 0)
                        Lv.ForeColor = Color.White
                    End If
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


End Class