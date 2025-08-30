Public Class EMI_Pembelian_PO_Summary_Data_Barang_Lain

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

        Lv_PO.Items.Clear()
        'ListView1.Columns.Add(Base_Language.Lang_Global_NoFaktur, 130, HorizontalAlignment.Left)
        'ListView1.Columns.Add(Base_Language.Lang_Global_No_Nota, 120, HorizontalAlignment.Left)
        'ListView1.Columns.Add(Base_Language.Lang_Global_Supplier, 120, HorizontalAlignment.Left)
        'ListView1.Columns.Add(Base_Language.lang_global_Nama_Supplier, 200, HorizontalAlignment.Left)
        'ListView1.Columns.Add(Base_Language.Lang_Pmb_Barang_masuk_No_Plat, 100, HorizontalAlignment.Center)
        'ListView1.Columns.Add(Base_Language.Lang_Pmb_Barang_Masuk_Tanggal_Bongkar, 120, HorizontalAlignment.Left)
        'ListView1.Columns.Add(Base_Language.Lang_Global_Tanggal, 100, HorizontalAlignment.Left)
        'ListView1.Columns.Add(Base_Language.Lang_Global_Validasi, 0, HorizontalAlignment.Left)
        Lv_PO.Columns.Add(Base_Language.Lang_Global_NoFaktur, 170, HorizontalAlignment.Left)
        'Lv_PO.Columns.Add(Base_Language.Lang_Global_No_Nota, 120, HorizontalAlignment.Left)
        Lv_PO.Columns.Add(Base_Language.Lang_Global_Supplier, 0, HorizontalAlignment.Left)
        Lv_PO.Columns.Add(Base_Language.lang_global_Nama_Supplier, 250, HorizontalAlignment.Left)
        Lv_PO.Columns.Add("PO Created", 110, HorizontalAlignment.Center)
        Lv_PO.Columns.Add("PO Released", 110, HorizontalAlignment.Center)
        Lv_PO.Columns.Add("ETD", 110, HorizontalAlignment.Center)
        Lv_PO.Columns.Add("Status PO", 100, HorizontalAlignment.Center)
        Lv_PO.Columns.Add("User ID", 100, HorizontalAlignment.Center)
        'Lv_PO.Columns.Add("Jenis Pembayaran", 100, HorizontalAlignment.Center)
        'Lv_PO.Columns.Add("Cara Bayar", 150, HorizontalAlignment.Left)
        'Lv_PO.Columns.Add("Tanggal Jatuh Tempo", 120, HorizontalAlignment.Center)
        'Lv_PO.Columns.Add("Mata Uang", 100, HorizontalAlignment.Center)
        'Lv_PO.Columns.Add("Total MUA", 150, HorizontalAlignment.Right)
        'Lv_PO.Columns.Add("Total idr", 150, HorizontalAlignment.Right)
        'Lv_PO.Columns.Add("PPN", 80, HorizontalAlignment.Center)
        'Lv_PO.Columns.Add("Grand Total", 150, HorizontalAlignment.Right)
        Lv_PO.View = View.Details

        Lv_PODetail.Items.Clear()
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_KodeBarang, 120, HorizontalAlignment.Left) '0
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_NamaBarang, 250, HorizontalAlignment.Left) '1
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_Jumlah, 130, HorizontalAlignment.Right) '2
        Lv_PODetail.Columns.Add("Jumlah Masuk", 130, HorizontalAlignment.Right) '3
        Lv_PODetail.Columns.Add("Sisa", 130, HorizontalAlignment.Right) '4
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_Satuan, 80, HorizontalAlignment.Center) '5
        Lv_PODetail.Columns.Add("%Complete", 130, HorizontalAlignment.Center) '6
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_Harga, 130, HorizontalAlignment.Right) '7
        Lv_PODetail.Columns.Add("NoFakPO", 0, HorizontalAlignment.Right) '8
        Lv_PODetail.Columns.Add("UrutPO", 0, HorizontalAlignment.Right) '9
        Lv_PODetail.Columns.Add("NOPenawaran", 0, HorizontalAlignment.Right) '10
        Lv_PODetail.View = View.Details


        Lv_DataLoading.Columns.Clear()
        Lv_DataLoading.Columns.Add("No Loading", 140, HorizontalAlignment.Left)
        Lv_DataLoading.Columns.Add("Tanggal", 130, HorizontalAlignment.Center)
        Lv_DataLoading.Columns.Add("Lokasi", 150, HorizontalAlignment.Center)
        Lv_DataLoading.Columns.Add("Kd Barang", 130, HorizontalAlignment.Left)
        Lv_DataLoading.Columns.Add("Jumlah", 180, HorizontalAlignment.Right)
        Lv_DataLoading.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        Lv_DataLoading.View = View.Details

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
            ComboBox3.Items.Add("Tanggal") : Arr1.Add("a.Tanggal")
            ComboBox3.Items.Add("Tanggal Release") : Arr1.Add("a.tanggal_release")
            ComboBox3.Items.Add("Tanggal Keberangkatan") : Arr1.Add("a.ETD_Simulasi")

            'TextBoxa.Text = "0" 
            ComboBox3.Enabled = False : ComboBox2.Enabled = False
            DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            TextBox4.Enabled = False
            ComboBox1.Enabled = False

            ComboBox2.Items.Clear() : ComboBox2.Text = "" : Arr2.Clear()
            ComboBox2.Items.Add("No Faktur") : Arr2.Add("a.no_faktur")
            ComboBox2.Items.Add("Supplier") : Arr2.Add("b.Nama")
            ComboBox2.Items.Add("User ID") : Arr2.Add("a.userid")

            ComboBox1.Items.Clear() : ComboBox1.Text = "" : Arr3.Clear()
            ComboBox1.Items.Add("SUBMITTED") : Arr3.Add("a.Flag_Release = 'Y'")
            ComboBox1.Items.Add("UNSUBMITTED") : Arr3.Add("a.Flag_Release is null")

            Label1.Text = "Summary Data - Pembelian PO"
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

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_PO.SelectedIndexChanged
        Try
            OpenConn()
            Lv_PODetail.Items.Clear() : Lv_DataLoading.Items.Clear()
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
            '------------------------------------------
            'SQL = "select a.kode_stock_owner, a.Kode_Barang,b.Nama,a.jumlah,a.jumlah_masuk, a.Satuan,a.Harga,a.Satuan,a.Nilai_Barang "
            'SQL = SQL & ""
            SQL = "select a.kode_stock_owner, a.Kode_Barang,b.Nama,a.harga, a.jumlah,  "

            SQL = SQL & "isnull((  select (   sum(z.Jumlah_Masuk) ) from EMI_Pembelian_Loading_detail_Barang_Lain z, emi_pembelian_loading_Barang_Lain y where a.Kode_Perusahaan = z.Kode_Perusahaan "
            SQL = SQL & "And a.No_Faktur = z.No_PO  And a.Kode_Stock_Owner = z.Kode_Stock_Owner And a.Kode_Barang  = z.Kode_Barang And z.no_faktur=y.no_faktur "
            SQL = SQL & "And y.status Is null And z.Urut_PO=a.No_Urut "
            SQL = SQL & "group by z.Kode_Perusahaan, z.Kode_Barang, z.Satuan_Barang  ), 0) as jumlah_masuk, "

            SQL = SQL & "a.Satuan, (a.jumlah - isnull((  select (  sum(z.Jumlah_Masuk)) from EMI_Pembelian_Loading_detail_Barang_Lain z, emi_pembelian_loading_Barang_Lain y "
            SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan And a.No_Faktur = z.No_PO And a.Kode_Stock_Owner = z.Kode_Stock_Owner And a.Kode_Barang = z.Kode_Barang "
            SQL = SQL & "And z.no_faktur=y.no_faktur And y.status Is null And z.Urut_PO=a.No_Urut "
            SQL = SQL & "group by z.Kode_Perusahaan, z.Kode_Barang, z.Satuan_Barang ), 0)  ) as sisa, "

            SQL = SQL & "round((isnull((  select  sum(z.Jumlah_Masuk)   from EMI_Pembelian_Loading_detail_Barang_Lain z, emi_pembelian_loading_Barang_Lain y "
            SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan And a.No_Faktur = z.No_PO And a.Kode_Stock_Owner = z.Kode_Stock_Owner And "
            SQL = SQL & "a.Kode_Barang = z.Kode_Barang And z.no_faktur = y.no_faktur And y.status Is null And z.Urut_PO = a.No_Urut "
            SQL = SQL & "group by z.Kode_Perusahaan, z.Kode_Barang, z.Satuan_Barang  ), 0) / a.jumlah) * 100,2) as percentComplete, "

            SQL = SQL & "a.No_Faktur, a.No_Urut, a.No_Penawaran "

            SQL = SQL & "From EMI_Pembelian_PO_Detail_Barang_Lain a, barang_lain b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_faktur = '" & Lv_PO.FocusedItem.SubItems(0).Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_PODetail.Items.Add(Dr("kode_barang"))
                    lvw.SubItems.Add(Dr("nama"))
                    lvw.SubItems.Add(Format(Dr("jumlah"), "N0"))
                    lvw.SubItems.Add(Format(Dr("jumlah_masuk"), "N0"))
                    lvw.SubItems.Add(Format(Dr("sisa"), "N0"))
                    lvw.SubItems.Add(Dr("satuan"))
                    lvw.SubItems.Add(Format(Dr("percentComplete"), "N1") & " %")
                    lvw.SubItems.Add(Format(Dr("harga"), "N2"))
                    lvw.SubItems.Add(Dr("No_Faktur"))
                    lvw.SubItems.Add(Dr("No_Urut"))
                    lvw.SubItems.Add(Dr("No_Penawaran"))
                    'lvw.SubItems.Add(Format(Dr("nilai_barang"), "N0"))
                    'lvw.SubItems.Add(Format(Dr("nilai_barang"), "N0"))
                    'lvw.SubItems.Add(Format(Dr("tgl_produksi"), "dd MMM yyyy"))
                    'lvw.SubItems.Add(Format(Dr("tgl_expired"), "dd MMM yyyy"))


                Loop
            End Using










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

            If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then
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
            ElseIf CheckBox4.Checked Then
                If ComboBox1.SelectedIndex = -1 Then
                    MessageBox.Show("Status Harus Di Pilih", Judul)
                    ComboBox1.Focus() : Exit Sub
                End If
            End If

            OpenConn()

            Lv_PO.Items.Clear()
            Lv_PODetail.Items.Clear()
            Lv_DataLoading.Items.Clear()

            'SQL = "select a.No_Faktur,a.No_Nota,ETD_Simulasi,b.kode_supplier, b.Nama,a.Jenis_Pembayaran,a.Tgl_Jatuh_Tempo,a.Cara_Bayar,"
            'SQL = SQL & "a.Tgl_Jatuh_Tempo,a.Total_mua,a.Total_IDR,a.Mata_Uang,PPN,Grand,ETD_Simulasi, a.flag_release "
            'SQL = SQL & "from EMI_Pembelian_PO a, Suppliers b  "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Supplier = b.Kode_Supplier "
            'SQL = SQL & "and a.Status is null "
            SQL = "select a.No_Faktur,b.kode_supplier,b.Nama,a.tanggal,a.tanggal_release, a.ETD_Simulasi, a.userid, a.Flag_Release, a.Status, a.Flag_Selesai_PO "
            SQL = SQL & "from EMI_Pembelian_PO_Barang_Lain a, Suppliers b  "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Supplier = b.Kode_Supplier "
            'SQL = SQL & "and a.Status is null "

            If CheckBox4.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & Arr3(ComboBox1.SelectedIndex) & " "
            End If

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

            SQL = SQL & "order by a.No_Faktur, a.tanggal , a.jam"

            Dim Lvw As ListViewItem

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Lvw = Lv_PO.Items.Add(.Rows(i).Item("no_faktur"))

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

                            'Lvw.SubItems.Add(.Rows(i).Item("mata_uang"))
                            'Lvw.SubItems.Add(Format(.Rows(i).Item("total_mua"), "N2"))
                            'Lvw.SubItems.Add(Format(.Rows(i).Item("total_idr"), "N2"))
                            'Lvw.SubItems.Add(Format(.Rows(i).Item("ppn"), "N2"))
                            'Lvw.SubItems.Add(Format(.Rows(i).Item("grand"), "N2"))
                            'Lvw.SubItems.Add(Format(.Rows(i).Item("etd_simulasi"), "dd MMM yyyy"))

                            'Lv_PO.Items(i).ForeColor = T

                            'If General_Class.CekNULL(.Rows(i).Item("flag_release")) <> "Y" Then
                            '    Lv_PO.Items(i).ForeColor = Batal
                            'End If

                            Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal"), "dd MMM yyyy"))
                            If General_Class.CekNULL(.Rows(i).Item("tanggal_release")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_release"), "dd MMM yyyy"))
                            End If
                            Lvw.SubItems.Add(Format(.Rows(i).Item("etd_simulasi"), "dd MMM yyyy"))

                            If General_Class.CekNULL(.Rows(i).Item("Flag_release")) = "Y" Then
                                Lvw.SubItems.Add("SUBMITTED")
                            Else
                                Lvw.SubItems.Add("UNSUBMITTED")
                            End If
                            Lvw.SubItems.Add(.Rows(i).Item("userid"))


                            If General_Class.CekNULL(.Rows(i).Item("Flag_Selesai_PO")) = "Y" Then
                                Lvw.BackColor = Color.FromArgb(0, 128, 0)
                                Lvw.ForeColor = Color.White
                            End If

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

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub

    Private Sub CetakUlangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangToolStripMenuItem.Click
        If Lv_PO.Items.Count = 0 Or Lv_PO.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        Try
            OpenConn()

            '==================================
            '=     UPDATE JUMLAH CETAK PO     =
            '==================================
            SQL = "update EMI_Pembelian_PO_Barang_Lain set Jumlah_Print = isnull(Jumlah_Print,0) + 1 where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Lv_PO.FocusedItem.Text & "'"
            ExecuteTrans(SQL)


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Try
            OpenConn()

            SQL = "select Kode_Perusahaan, No_Faktur from View_Laporan_PO2_Barang_Lain "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & Lv_PO.FocusedItem.Text & "' "

            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    With Ds.Tables(0)
                        Dim CrDoc As New Faktur_Purchase_Order2_Barang_Lain
                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Laporan Faktur Purchase Order"
                            CrDoc.RecordSelectionFormula = " {View_Laporan_PO2_Barang_Lain.Kode_Perusahaan} = '" & KodePerusahaan & "' and {View_Laporan_PO2_Barang_Lain.No_Faktur} = '" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "'"

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
        If Lv_PO.Items.Count = 0 Or Lv_PO.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("Pembatalan_PO_Barang_Lain") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan PO", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin akan membatalkan Purhcase Order ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            SQL = "select Status from EMI_Pembelian_PO_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & Lv_PO.FocusedItem.Text & "' "
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

            SQL = "select a.No_Faktur from EMI_Pembelian_PO_Barang_Lain a,"
            SQL = SQL & "EMI_Pembelian_PO_Detail_Barang_Lain b,EMI_Pembelian_Loading_Detail_Barang_Lain c,EMI_Pembelian_Loading_Barang_Lain d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Status is null and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur "
            SQL = SQL & "and d.Status is null and b.No_Faktur = c.No_PO "
            SQL = SQL & "and b.No_Urut = c.Urut_PO and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & Lv_PO.FocusedItem.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Purhcase Order tidak bisa dibatalkan,karena sudah masuk tahap Loading Barang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            SQL = "Update EMI_Pembelian_PO_Barang_Lain set Status = 'Y' where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & Lv_PO.FocusedItem.Text & "' "
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Purhcase Order berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        BtnBarangMasuk_Cari_Click(BatalToolStripMenuItem, e)
    End Sub



    Private Sub DisplayRakToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If Lv_PO.Items.Count = 0 Or Lv_PO.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        'JANGAN LUPA DI UNCOMMENT
        'EMI_Barang_Masuk_Display_Rak.TxtNoBM.Text = Lv_PO.FocusedItem.Text
        'EMI_Barang_Masuk_Display_Rak.ShowDialog()
    End Sub



    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked Then
            ComboBox1.Enabled = True
        Else
            ComboBox1.Enabled = False
            ComboBox1.SelectedIndex = -1
        End If
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

    Private Sub Lv_PODetail_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_PODetail.SelectedIndexChanged
        If Lv_PODetail.Items.Count = 0 Or Lv_PODetail.FocusedItem.Index = -1 Then Exit Sub

        Try
            OpenConn()


            Lv_DataLoading.Items.Clear()
            SQL = "select a.No_Faktur, d.No_Faktur as No_Loading, d.Tanggal, c.Kode_Stock_Owner, c.Kode_Barang, SUM(c.Jumlah) as Jumlah, c.Satuan, d.Status, b.No_Urut "
            SQL = SQL & "from EMI_Pembelian_PO_Barang_Lain a, EMI_Pembelian_PO_Detail_Barang_Lain b, EMI_Pembelian_Loading_detail_Barang_Lain c, EMI_Pembelian_Loading_Barang_Lain d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur and b.No_Faktur = c.No_PO and b.No_Urut = c.Urut_PO and c.No_Faktur = d.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & Lv_PODetail.FocusedItem.SubItems(8).Text & "' "
            SQL = SQL & "and c.Urut_PO = '" & Lv_PODetail.FocusedItem.SubItems(9).Text & "' "
            SQL = SQL & "and b.No_Penawaran = '" & Lv_PODetail.FocusedItem.SubItems(10).Text & "' "
            SQL = SQL & "group by a.No_Faktur, d.No_Faktur, d.Tanggal, c.Kode_Stock_Owner, c.Kode_Barang, c.Satuan, d.Status, b.No_Urut"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_DataLoading.Items.Add(Dr("No_Loading"))
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


    Private Sub SelesaiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelesaiToolStripMenuItem.Click
        If Lv_PO.Items.Count = 0 Or Lv_PO.SelectedItems.Count = 0 Then
            Exit Sub
        End If


        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("Penyelesaian_SubPO_Barang_Lain") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Penyelesaian PO", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Menyelesaikan Purhcase Order ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If


            '=======================================
            '=     CEK APAKAH PO SUDAH SELESAI     =
            '=======================================
            SQL = "select Flag_Selesai_PO from EMI_Pembelian_PO_Barang_Lain "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Lv_PO.FocusedItem.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Flag_Selesai_PO")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("PO Sudah Selesai, Tidak Bisa Diselesaikan Lagi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data PO Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '================================
            '=     CEK VALIDASI LOADING     =
            '================================
            Dim hasData As Boolean = False
            SQL = "select distinct a.No_Faktur, b.No_PO, a.Flag_Selisih_BM "
            SQL = SQL & "from EMI_Pembelian_Loading_Barang_Lain a, EMI_Pembelian_Loading_Detail_Barang_Lain b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_PO = '" & Lv_PO.FocusedItem.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    hasData = True
                Else
                    hasData = False
                End If
            End Using

            If hasData Then
                SQL = "select distinct a.No_Faktur, b.No_PO, a.Flag_Selisih_BM "
                SQL = SQL & "from EMI_Pembelian_Loading_Barang_Lain a, EMI_Pembelian_Loading_Detail_Barang_Lain b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                SQL = SQL & "and a.status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and b.No_PO = '" & Lv_PO.FocusedItem.Text & "'"
                SQL = SQL & "and a.Flag_Selisih_BM is null"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count = 0 Then

                            '================================
                            '=     CEK SELISIH BM DI PO     =
                            '================================
                            SQL = "select Flag_Selisih_BM from EMI_Pembelian_PO_Barang_Lain "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and No_Faktur = '" & Lv_PO.FocusedItem.Text & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                    If General_Class.CekNULL(Ds1.Tables("MyTable").Rows(0).Item("Flag_Selisih_BM")) = "" Then
                                        '=====================================
                                        '=     UPDATE FLAG SELISIH BM PO     =
                                        '=====================================
                                        SQL = "update EMI_Pembelian_PO_Barang_Lain set Flag_Selisih_BM = 'Y' "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and No_Faktur = '" & Lv_PO.FocusedItem.Text & "' "
                                        SQL = SQL & "and Flag_Selisih_BM is null "
                                        ExecuteTrans(SQL)

                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("PO Tidak Bisa diselesaikan, Harap Hubungi tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Terjadi Kesahalahan, Harap Hubungi tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using


                        End If
                    End With
                End Using
            End If

            '==================================
            '=     UPDATE FLAG SELESAI PO     =
            '==================================
            SQL = "update EMI_Pembelian_PO_Barang_Lain set Flag_Selesai_PO = 'Y', UserID_Selesai = '" & UserID & "', "
            SQL = SQL & "Tanggal_Selesai = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Selesai = '" & Format(tgl_skg, "HH:mm:ss") & "'  "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Lv_PO.FocusedItem.Text & "' and Flag_Selesai_PO is null"
            ExecuteTrans(SQL)


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("PO Berhasil Diselesaikan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        BtnBarangMasuk_Cari_Click(BatalToolStripMenuItem, e)

    End Sub

End Class