Public Class EMI_Pembelian_Summary_Data

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

        Lv_Pembelian.Items.Clear()

        Lv_Pembelian.Columns.Add(Base_Language.Lang_Global_NoFaktur, 130, HorizontalAlignment.Left) '0
        Lv_Pembelian.Columns.Add(Base_Language.Lang_Global_Supplier, 0, HorizontalAlignment.Left) '1
        Lv_Pembelian.Columns.Add(Base_Language.lang_global_Nama_Supplier, 250, HorizontalAlignment.Left) '2
        Lv_Pembelian.Columns.Add("No PO", 130, HorizontalAlignment.Left) '3
        Lv_Pembelian.Columns.Add("Keterangan", 150, HorizontalAlignment.Left) '4
        Lv_Pembelian.Columns.Add("Tanggal", 110, HorizontalAlignment.Center) '5
        Lv_Pembelian.Columns.Add("Pembayaran", 110, HorizontalAlignment.Center) '6
        Lv_Pembelian.Columns.Add("User ID", 100, HorizontalAlignment.Center) '7
        Lv_Pembelian.Columns.Add("Total Hutang", 150, HorizontalAlignment.Right) '8
        Lv_Pembelian.Columns.Add("PPN", 80, HorizontalAlignment.Center) '9
        Lv_Pembelian.Columns.Add("Total PPN", 150, HorizontalAlignment.Right) '10
        Lv_Pembelian.Columns.Add("Grand Total", 150, HorizontalAlignment.Right) '11

        Lv_Pembelian.Columns(8).DisplayIndex = 7
        Lv_Pembelian.Columns(9).DisplayIndex = 8
        Lv_Pembelian.Columns(10).DisplayIndex = 9
        Lv_Pembelian.Columns(11).DisplayIndex = 10

        Lv_Pembelian.View = View.Details

        Lv_Pembelian_Detail.Items.Clear()
        Lv_Pembelian_Detail.Columns.Add(Base_Language.Lang_Global_KodeBarang, 120, HorizontalAlignment.Left) '0
        Lv_Pembelian_Detail.Columns.Add(Base_Language.Lang_Global_NamaBarang, 250, HorizontalAlignment.Left) '1
        Lv_Pembelian_Detail.Columns.Add(Base_Language.Lang_Global_Jumlah, 110, HorizontalAlignment.Right) '2
        Lv_Pembelian_Detail.Columns.Add("Satuan", 100, HorizontalAlignment.Center) '3
        Lv_Pembelian_Detail.Columns.Add("Harga", 150, HorizontalAlignment.Right) '4
        Lv_Pembelian_Detail.Columns.Add("Total", 0, HorizontalAlignment.Right) '5
        Lv_Pembelian_Detail.Columns.Add("Harga Akhir", 150, HorizontalAlignment.Right) '6
        Lv_Pembelian_Detail.Columns.Add("Total Akhir", 150, HorizontalAlignment.Right) '7
        Lv_Pembelian_Detail.Columns.Add("Grand Akhir", 150, HorizontalAlignment.Right) '8
        Lv_Pembelian_Detail.Columns.Add("Harga PO", 150, HorizontalAlignment.Right) '9
        Lv_Pembelian_Detail.Columns.Add("Harga Akhir", 150, HorizontalAlignment.Right) '10
        Lv_Pembelian_Detail.Columns.Add("Jumlah PO", 150, HorizontalAlignment.Right) '11
        Lv_Pembelian_Detail.Columns.Add("Jumlah Masuk", 150, HorizontalAlignment.Right) '12
        Lv_Pembelian_Detail.Columns.Add("Jumlah Hutang", 150, HorizontalAlignment.Right) '13
        Lv_Pembelian_Detail.Columns.Add("Total PO", 150, HorizontalAlignment.Right) '14
        Lv_Pembelian_Detail.Columns.Add("Total Masuk", 150, HorizontalAlignment.Right) '15
        Lv_Pembelian_Detail.Columns.Add("Total Hutang", 150, HorizontalAlignment.Right) '16
        Lv_Pembelian_Detail.View = View.Details


        Lv_Pelunasan.Items.Clear()
        Lv_Pelunasan.Columns.Add("Kode", 200, HorizontalAlignment.Left) '0
        Lv_Pelunasan.Columns.Add("Kode Akun", 200, HorizontalAlignment.Left) '1
        Lv_Pelunasan.Columns.Add("Keterangan", 310, HorizontalAlignment.Left) '2
        Lv_Pelunasan.Columns.Add("Nilai", 200, HorizontalAlignment.Right) '3
        Lv_Pelunasan.View = View.Details

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
            ComboBox3.Items.Add("Tanggal") : Arr1.Add("a.tanggal")


            'TextBoxa.Text = "0" 
            ComboBox3.Enabled = False : ComboBox2.Enabled = False
            DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            TextBox4.Enabled = False

            ComboBox2.Items.Clear() : ComboBox2.Text = "" : Arr2.Clear()
            ComboBox2.Items.Add("No Faktur") : Arr2.Add("a.no_faktur")
            ComboBox2.Items.Add("Lokasi") : Arr2.Add("a.Lokasi")
            ComboBox2.Items.Add("Kode Supplier") : Arr2.Add("b.Kode_Supplier")
            ComboBox2.Items.Add("Nama") : Arr2.Add("c.nama")
            ComboBox2.Items.Add("Tanggal") : Arr2.Add("a.Tanggal")
            ComboBox2.Items.Add("No PO") : Arr2.Add("a.No_PO")
            ComboBox2.Items.Add("Keterangan") : Arr2.Add("b.No_Nota")
            ComboBox2.Items.Add("Jenis Pembayaran") : Arr2.Add("a.Jenis_Pembayaran")
            ComboBox2.Items.Add("User Id") : Arr2.Add("a.UserID")


            Label1.Text = "Summary Data - Pembelian"
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


    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox1.Checked = False
            BtnBarangMasuk_Cari_Click(CheckBox3, e)
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Pembelian.SelectedIndexChanged
        Try
            OpenConn()
            Lv_Pembelian_Detail.Items.Clear()


            SQL = "select a.Kode_Stock_Owner,a.jumlah_masuk,ppn,a.harga_akhir,a.Kode_Barang,b.Nama,a.Harga,a.jumlah,a.Satuan, a.Total,harga_akhir * jumlah_masuk as  Total_Akhir,harga_akhir * jumlah_masuk + ppn as  Grand_Akhir, "
            SQL = SQL & "a.Harga as Harga_PO, a.Harga_Akhir, a.Jumlah as JumlahPO, a.Jumlah_Masuk, a.jumlah_Hutang,  "
            SQL = SQL & "isnull((a.Harga * a.Jumlah), 0) as Total_PO, isnull((a.Harga * a.Jumlah_Masuk), 0) as Total_Masuk, isnull((a.Harga * a.jumlah_Hutang), 0) as Total_Hutang "
            SQL = SQL & "From EMI_Pembelian_Detail2 a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner  and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_faktur = '" & Lv_Pembelian.FocusedItem.SubItems(0).Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_Pembelian_Detail.Items.Add(Dr("kode_barang")) '0
                    lvw.SubItems.Add(Dr("nama")) '1
                    lvw.SubItems.Add(Format(Dr("jumlah"), "N0")) '2
                    lvw.SubItems.Add(Dr("satuan")) '3
                    lvw.SubItems.Add(Format(Dr("harga"), "N2")) '4
                    lvw.SubItems.Add(Format(Dr("total"), "N2")) '5
                    lvw.SubItems.Add(Format(Dr("harga_akhir"), "N2")) '6
                    lvw.SubItems.Add(Format(Dr("total_akhir"), "N2")) '7
                    lvw.SubItems.Add(Format(Dr("grand_akhir"), "N2")) '8

                    lvw.SubItems.Add(Format(Dr("Harga_PO"), "N2")) '9
                    lvw.SubItems.Add(Format(Dr("Harga_Akhir"), "N2")) '10
                    lvw.SubItems.Add(Format(Dr("JumlahPO"), "N2")) '11
                    lvw.SubItems.Add(Format(Dr("Jumlah_Masuk"), "N2")) '12
                    lvw.SubItems.Add(Format(Val(General_Class.CekNULL(Dr("jumlah_Hutang"))), "N2")) '13
                    lvw.SubItems.Add(Format(Dr("Total_PO"), "N2")) '14
                    lvw.SubItems.Add(Format(Dr("Total_Masuk"), "N2")) '15
                    lvw.SubItems.Add(Format(Dr("Total_Hutang"), "N2")) '16

                Loop
            End Using



            Lv_Pelunasan.Items.Clear()
            SQL = "select a.Kode,a.Kode_Akun,b.Keterangan,a.Nilai From Pelunasan_Pembelian a, Detail_Account b where  "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Akun = b.Kode_Account "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_faktur = '" & Lv_Pembelian.FocusedItem.SubItems(0).Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_Pelunasan.Items.Add(Dr("Kode"))
                    lvw.SubItems.Add(Dr("kode_akun"))
                    lvw.SubItems.Add(Dr("Keterangan"))
                    lvw.SubItems.Add(Format(Dr("Nilai"), "N2"))

                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
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

            Lv_Pembelian.Items.Clear()
            Lv_Pembelian_Detail.Items.Clear()


            SQL = "select a.No_Faktur, a.Lokasi, b.Kode_Supplier, c.nama, a.Tanggal, a.Jam, a.No_PO, b.No_Nota, a.Jenis_Pembayaran, a.UserID, "
            SQL = SQL & "a.Grand_Sebelum_PPN as Jumlah_Hutang, a.PPN, a.Nilai_PPN, a.Grand "
            SQL = SQL & "From emi_pembelian a, EMI_Pembelian_PO b, Suppliers c  where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_PO = b.No_Faktur "
            SQL = SQL & "and b.kode_perusahaan = c.Kode_Perusahaan and b.Kode_Supplier = c.Kode_Supplier "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.Status is null "



            If CheckBox1.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

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

                SQL = SQL & " a.tanggal between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If

            If ComboBox6.SelectedIndex = 0 Then
                SQL = SQL & " and a.Lokasi in("
                Dim list_kota As String = ""
                For x As Integer = 1 To ComboBox6.Items.Count - 1
                    list_kota = list_kota & "'" & ComboBox6.Items(x).ToString & "', "
                Next

                list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

                SQL = SQL & list_kota & ")"
            Else
                SQL = SQL & " and a.Lokasi = '" & ComboBox6.Text & "' "
            End If

            SQL = SQL & "order by a.tanggal , a.jam"

            Dim Lvw As ListViewItem

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1





                            Lvw = Lv_Pembelian.Items.Add(.Rows(i).Item("no_faktur"))

                            'Lvw.SubItems.Add(.Rows(i).Item("no_nota"))
                            Lvw.SubItems.Add(.Rows(i).Item("kode_supplier"))
                            Lvw.SubItems.Add(.Rows(i).Item("nama"))

                            'If General_Class.CekNULL(.Rows(i).Item("jenis_pembayaran")) = "N" Then
                            '    Lvw.SubItems.Add("Non Tunai")
                            'Else
                            '    Lvw.SubItems.Add("Tunai")
                            'End If
                            'Lvw.SubItems.Add(.Rows(i).Item("cara_bayar"))

                            'If .Rows(i).Item("jenis_pembayaran") = "N" Then
                            '    Lvw.SubItems.Add(Format(.Rows(i).Item("Tgl_Jatuh_Tempo"), "dd MMM yyyy"))
                            'Else
                            '    Lvw.SubItems.Add("-")
                            'End If

                            Lvw.SubItems.Add(.Rows(i).Item("no_po"))
                            Lvw.SubItems.Add(.Rows(i).Item("no_nota"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy"))
                            If General_Class.CekNULL(.Rows(i).Item("jenis_pembayaran")) = "N" Then
                                Lvw.SubItems.Add("Non Tunai")
                            Else
                                Lvw.SubItems.Add("Tunai")
                            End If

                            'Lvw.SubItems.Add(Format(.Rows(i).Item("total_idr"), "N2"))
                            'Lvw.SubItems.Add(Format(.Rows(i).Item("ppn"), "N2"))
                            'Lvw.SubItems.Add(Format(.Rows(i).Item("grand"), "N2"))
                            'Lvw.SubItems.Add(Format(.Rows(i).Item("etd_simulasi"), "dd MMM yyyy"))

                            'Lv_PO.Items(i).ForeColor = T

                            'If General_Class.CekNULL(.Rows(i).Item("flag_release")) <> "Y" Then
                            '    Lv_PO.Items(i).ForeColor = Batal
                            'End If

                            'Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal"), "dd MMM yyyy"))
                            'If General_Class.CekNULL(.Rows(i).Item("tanggal_release")) = "" Then
                            '    Lvw.SubItems.Add("-")
                            'Else
                            '    Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_release"), "dd MMM yyyy"))
                            'End If
                            'Lvw.SubItems.Add(Format(.Rows(i).Item("etd_simulasi"), "dd MMM yyyy"))

                            'If General_Class.CekNULL(.Rows(i).Item("Flag_release")) = "Y" Then
                            '    Lvw.SubItems.Add("SUBMITTED")
                            'Else
                            '    Lvw.SubItems.Add("UNSUBMITTED")
                            'End If
                            Lvw.SubItems.Add(.Rows(i).Item("userid"))

                            Lvw.SubItems.Add(Format(.Rows(i).Item("Jumlah_Hutang"), "N2"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("PPN"), "N0") & " %")
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Nilai_PPN"), "N2"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Grand"), "N2"))

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

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub

    Private Sub CetakUlangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangToolStripMenuItem.Click
        If Lv_Pembelian.Items.Count = 0 Or Lv_Pembelian.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        Try
            OpenConn()

            '==================================
            '=     UPDATE JUMLAH CETAK PO     =
            '==================================
            SQL = "update EMI_Pembelian_PO set Jumlah_Print = Jumlah_Print + 1 where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Lv_Pembelian.FocusedItem.Text & "'"
            ExecuteTrans(SQL)


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Try
            OpenConn()

            SQL = "select a.No_Faktur "
            SQL = SQL & "from EMI_Pembelian_PO a, EMI_Pembelian_PO_Detail b, barang c, suppliers d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Kode_Supplier = d.Kode_Supplier "
            SQL = SQL & "and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur='" & Lv_Pembelian.FocusedItem.Text & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    With Ds.Tables(0)
                        Dim CrDoc As New Faktur_Purchase_Order
                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Laporan Faktur Purchase Order"
                            CrDoc.RecordSelectionFormula = " {EMI_Pembelian_PO.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Pembelian_PO.No_Faktur} = '" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "'"

                            .Text = "Laporan Faktur Purchase Order"
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
        If Lv_Pembelian.Items.Count = 0 Or Lv_Pembelian.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim tanya As String = MessageBox.Show("Yakin akan membatalkan Purhcase Order ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then Exit Sub

            SQL = "select Status from EMI_Pembelian_PO where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & Lv_Pembelian.FocusedItem.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Status")) <> "" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Purhcase Order sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Purhcase Order tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select a.No_Faktur from EMI_Pembelian_PO a,"
            SQL = SQL & "EMI_Pembelian_PO_Detail b,EMI_Pembelian_Loading_Detail c,EMI_Pembelian_Loading d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Status is null and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur "
            SQL = SQL & "and d.Status is null and b.No_Faktur = c.No_PO "
            SQL = SQL & "and b.No_Urut = c.Urut_PO and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & Lv_Pembelian.FocusedItem.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Purhcase Order tidak bisa dibatalkan,karena sudah masuk tahap Loading Barang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            SQL = "Select No_Urut_PR from "
            SQL = SQL & "emi_pembelian_po_det where "
            SQL = SQL & "Kode_Perusahaan='" & KodePerusahaan & "' and "
            SQL = SQL & "no_faktur='" & Lv_Pembelian.FocusedItem.Text & "' "
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For index = 0 To .Rows.Count - 1

                        SQL = "Update EMI_Purchase_Requisition_Detail set Flag_Sudah_PO = null where "
                        SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Urut = '" & .Rows(index).Item("No_Urut_PR") & "' "
                        ExecuteTrans(SQL)

                    Next
                End With
            End Using


            SQL = "Update EMI_Pembelian_PO set Status = 'Y' where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & Lv_Pembelian.FocusedItem.Text & "' "
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            MessageBox.Show("Purhcase Order berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        BtnBarangMasuk_Cari_Click(BatalToolStripMenuItem, e)
    End Sub



    Private Sub DisplayRakToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If Lv_Pembelian.Items.Count = 0 Or Lv_Pembelian.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        'JANGAN LUPA DI UNCOMMENT
        'EMI_Barang_Masuk_Display_Rak.TxtNoBM.Text = Lv_PO.FocusedItem.Text
        'EMI_Barang_Masuk_Display_Rak.ShowDialog()
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

    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox2.Focus()
    End Sub

    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox4.Focus()
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Chr(13) Then BtnBarangMasuk_Cari.Focus()
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then ComboBox3.Focus()
    End Sub
    Private Sub ComboBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker1.Focus()
    End Sub
    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub

End Class