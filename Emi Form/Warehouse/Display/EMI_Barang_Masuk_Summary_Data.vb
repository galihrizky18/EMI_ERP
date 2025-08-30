Public Class EMI_Barang_Masuk_Summary_Data

    Dim Arr1, Arr2, Arr3, Arr4 As New ArrayList
    Dim pertama As Integer = 1
    Dim T As Color = Color.Blue
    Dim KT As Color = Color.Red
    Dim KY As Color = Color.Green
    Dim Batal As Color = Color.Black

    Dim LvPallet_KdBarang, LvPallet_NmBarang, LvPallet_Jumlah, LvPallet_Satuan, LvPallet_Batch, LvPallet_Qr, LvPallet_KdRak, LvPallet_FlagSelesai As String

    Dim item_PembelianPONoFaktur As Integer = 0

    Dim itemDet_NoPO As Integer = 0
    Dim itemDet_KdBarang As Integer = 1
    Dim itemDet_NmBarang As Integer = 2
    Dim itemDet_Satuan As Integer = 3
    Dim itemDet_TglProduksi As Integer = 4
    Dim itemDet_TglExpired As Integer = 5
    Dim itemDet_Jumlah As Integer = 6
    Dim itemDet_JumlahMasuk As Integer = 7

    Dim itemPallet_KdBarang As Integer = 0
    Dim itemPallet_NmBarang As Integer = 1
    Dim itemPallet_Jumlah As Integer = 2
    Dim itemPallet_Satuan As Integer = 3
    Dim itemPallet_Batch As Integer = 4
    Dim itemPallet_Qr As Integer = 5
    Dim itemPallet_KdRak As Integer = 6
    Dim itemPallet_FlagSelesai As Integer = 7

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

        Txt_JumlahMasuk.Text = ""
        Txt_PalletMasuk.Text = ""
        Txt_JumlahBlmMasuk.Text = ""
        Txt_PalletBlmMasuk.Text = ""

        LV_PembelianLoading.Items.Clear() : LV_PembelianLoading.Columns.Clear()
        LV_PembelianLoading.Columns.Add(Base_Language.Lang_Global_NoFaktur, 180, HorizontalAlignment.Left)
        LV_PembelianLoading.Columns.Add(Base_Language.Lang_Global_Supplier, 0, HorizontalAlignment.Left)
        LV_PembelianLoading.Columns.Add(Base_Language.lang_global_Nama_Supplier, 300, HorizontalAlignment.Left)
        LV_PembelianLoading.Columns.Add(Base_Language.Lang_GLOBAL_No_Surat_Jalan, 180, HorizontalAlignment.Left)
        LV_PembelianLoading.Columns.Add(Base_Language.Lang_Global_PlatNomor, 130, HorizontalAlignment.Center)
        LV_PembelianLoading.Columns.Add(Base_Language.Lang_Global_Supir, 200, HorizontalAlignment.Left)
        LV_PembelianLoading.Columns.Add("Tanggal Masuk", 140, HorizontalAlignment.Center)
        LV_PembelianLoading.Columns.Add("Selesai", 90, HorizontalAlignment.Center)
        LV_PembelianLoading.View = View.Details

        Lv_PODetail.Items.Clear() : Lv_PODetail.Columns.Clear()
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_No_PO, 130, HorizontalAlignment.Left) '0
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_KodeBarang, 120, HorizontalAlignment.Left) '1
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_NamaBarang, 200, HorizontalAlignment.Left) '2
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_Satuan, 90, HorizontalAlignment.Center) '3
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_Tanggal_Produksi, 130, HorizontalAlignment.Center) '4
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_Tanggal_Expired, 130, HorizontalAlignment.Center) '5
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_Jumlah, 100, HorizontalAlignment.Center) '6
        Lv_PODetail.Columns.Add("Jumlah Masuk", 0, HorizontalAlignment.Center) '7
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_Satuan, 0, HorizontalAlignment.Center) '8
        Lv_PODetail.Columns.Add("Selisih", 100, HorizontalAlignment.Right) '9
        Lv_PODetail.View = View.Details

        ListView1.Columns.Clear() : ListView1.Items.Clear()
        ListView1.Columns.Add(Base_Language.Lang_Global_KodeBarang, 0, HorizontalAlignment.Left) '0
        ListView1.Columns.Add(Base_Language.Lang_Global_NamaBarang, 0, HorizontalAlignment.Left) '1
        ListView1.Columns.Add(Base_Language.Lang_Global_Jumlah, 140, HorizontalAlignment.Right) '2
        ListView1.Columns.Add(Base_Language.Lang_Global_Satuan, 80, HorizontalAlignment.Center) '3
        ListView1.Columns.Add("Batch Number", 0, HorizontalAlignment.Left) '4
        ListView1.Columns.Add("QR Code", 200, HorizontalAlignment.Left) '5
        ListView1.Columns.Add("Kode Rak", 180, HorizontalAlignment.Center) '6
        ListView1.Columns.Add("Selesai", 0, HorizontalAlignment.Center) '7
        ListView1.View = View.Details

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
            ComboBox3.Items.Add("Tanggal Loading") : Arr1.Add("a.Tanggal")
            ComboBox3.Items.Add("Tanggal Masuk") : Arr1.Add("a.tanggal_masuk")

            'TextBoxa.Text = "0"
            ComboBox3.Enabled = False : ComboBox2.Enabled = False
            DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            TextBox4.Enabled = False

            ComboBox2.Items.Clear() : ComboBox2.Text = "" : Arr2.Clear()
            ComboBox2.Items.Add("No Faktur") : Arr2.Add("a.no_faktur")
            ComboBox2.Items.Add("Kode Supplier") : Arr2.Add("a.kode_supplier")
            ComboBox2.Items.Add("Nama Supplier") : Arr2.Add("b.nama")
            ComboBox2.Items.Add("Supir") : Arr2.Add("a.Driver")
            ComboBox2.Items.Add("No Plat") : Arr2.Add("a.no_plat")
            ComboBox2.Items.Add("Surat Jalan") : Arr2.Add("a.no_sj")

            Label1.Text = "Summary Data - Barang Masuk"
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
        PrinterNameSPB = "EPSON LX-310 ESC/P"
        CheckBox3.Focus()
    End Sub

    Private Sub GetData_Pallet(ByVal index As Integer)
        LvPallet_KdBarang = ListView1.Items(index).SubItems(itemPallet_KdBarang).Text
        LvPallet_NmBarang = ListView1.Items(index).SubItems(itemPallet_NmBarang).Text
        LvPallet_Jumlah = ListView1.Items(index).SubItems(itemPallet_Jumlah).Text
        LvPallet_Satuan = ListView1.Items(index).SubItems(itemPallet_Satuan).Text
        LvPallet_Batch = ListView1.Items(index).SubItems(itemPallet_Batch).Text
        LvPallet_Qr = ListView1.Items(index).SubItems(itemPallet_Qr).Text
        LvPallet_KdRak = ListView1.Items(index).SubItems(itemPallet_KdRak).Text
        LvPallet_FlagSelesai = ListView1.Items(index).SubItems(itemPallet_FlagSelesai).Text
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox1.Checked = False
            BtnBarangMasuk_Cari_Click(CheckBox3, e)
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LV_PembelianLoading.SelectedIndexChanged
        Try
            OpenConn()
            Lv_PODetail.Items.Clear()

            Txt_JumlahMasuk.Text = ""
            Txt_PalletMasuk.Text = ""
            Txt_JumlahBlmMasuk.Text = ""
            Txt_PalletBlmMasuk.Text = ""

            Lv_PODetail.Items.Clear() : ListView1.Items.Clear()
            SQL = "select b.no_po,b.Kode_Barang, c.Nama, "
            SQL = SQL & "isnull((select Harga from EMI_Pembelian_PO_Detail x where x.Kode_Perusahaan = b.Kode_Perusahaan and x.No_Urut= b.Urut_PO ), 0 ) as Harga, "
            SQL = SQL & "b.jumlah as Jumlah_Kirim,b.Satuan,b.Tanggal_Produksi,b.Tanggal_Expired "
            SQL = SQL & ",isnull((select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA',b.Kode_Barang, b.Satuan_Barang,x.Satuan,b.Jumlah_Masuk)  from Barang_Detail_Satuan x where b.Kode_Perusahaan = x.Kode_Perusahaan  "
            SQL = SQL & "and b.Kode_Barang = x.Kode_barang and x.Flag_Tampil_Display = 'Y' ),0) as Jumlah_masuk "
            SQL = SQL & ",isnull((select x.Satuan from Barang_Detail_Satuan x where b.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and b.Kode_Barang = x.Kode_barang and x.Flag_Tampil_Display = 'Y' ),0) as Satuan_masuk, "
            SQL = SQL & "ISNULL(( (b.jumlah) - ( select sum(z.jumlah) from EMI_Barang_Masuk_Perpallet z where a.Kode_Perusahaan = z.Kode_Perusahaan  "
            SQL = SQL & "and a.No_Faktur = z.No_Pembelian_Loading and a.Kode_Supplier = z.Kode_Supplier "
            SQL = SQL & "and b.Kode_Barang = z.Kode_Barang ) ), 0) as Selisih "
            SQL = SQL & "from EMI_Pembelian_Loading a, EMI_Pembelian_Loading_Detail b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur  "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "'  and a.No_Faktur ='" & LV_PembelianLoading.FocusedItem.Text & "'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_PODetail.Items.Add(Dr("no_po"))
                    lvw.SubItems.Add(Dr("kode_barang"))
                    lvw.SubItems.Add(Dr("nama"))
                    lvw.SubItems.Add(Dr("satuan"))
                    lvw.SubItems.Add(Format(Dr("Tanggal_Produksi"), "dd MMM yyyy"))
                    lvw.SubItems.Add(Format(Dr("Tanggal_Expired"), "dd MMM yyyy"))
                    lvw.SubItems.Add(Format(Dr("Jumlah_Kirim"), "N2"))
                    lvw.SubItems.Add(Format(Dr("jumlah_masuk"), "N2"))
                    lvw.SubItems.Add(Dr("satuan_masuk"))
                    lvw.SubItems.Add(Format(Dr("Selisih"), "N2"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv_PODetail_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_PODetail.SelectedIndexChanged
        Try
            OpenConn()

            Dim TotalMasuk As Double = 0
            Dim TotalBlmMasuk As Double = 0
            Dim TotalPalletMasuk As Double = 0
            Dim TotalPalletBlmMasuk As Double = 0

            ListView1.Items.Clear()
            SQL = "Select a.Batch_Number, Qr_Code+'-'+Kode_Unik_Berjalan as QR_Code,a.Kode_Barang, b.nama, a.Tgl_Produksi_Real, "
            SQL = SQL & "a.Tgl_Expired_Real, a.jumlah, a.satuan, a.Id_Warehouse, a.selesai, a.Sdh_Cetak, "
            SQL = SQL & "isnull((select c.Labeling_WMS_Position from View_Warehouse_Position c where "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan And a.Id_Warehouse = c.Id_WMS_Warehouse_Position),'') as Labeling_WMS_Position, "
            SQL = SQL & "a.Flag_angkut, a.Selesai "
            SQL = SQL & "From EMI_Barang_Masuk_Perpallet a, Barang b "
            SQL = SQL & "Where a.no_Pembelian_loading ='" & LV_PembelianLoading.FocusedItem.Text & "' "
            SQL = SQL & "and a.Kode_Barang = '" & Lv_PODetail.Items(Lv_PODetail.FocusedItem.Index).SubItems(itemDet_KdBarang).Text & "' "
            SQL = SQL & "And a.Kode_Barang = b.Kode_Barang And a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.status is null "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = ListView1.Items.Add(Dr("Kode_Barang"))
                    lvw.SubItems.Add(Dr("nama"))
                    lvw.SubItems.Add(Format(Dr("jumlah"), "N2"))
                    lvw.SubItems.Add(Dr("satuan"))
                    lvw.SubItems.Add(If(General_Class.CekNULL(Dr("Batch_Number")) = "", "", Dr("Batch_Number")))
                    lvw.SubItems.Add(If(General_Class.CekNULL(Dr("QR_Code")) = "", "", Dr("QR_Code")))
                    lvw.SubItems.Add(If(General_Class.CekNULL(Dr("Labeling_WMS_Position")) = "", "", Dr("Labeling_WMS_Position")))
                    lvw.SubItems.Add(If(General_Class.CekNULL(Dr("Sdh_Cetak")) = "", "", Dr("Sdh_Cetak")))

                    If General_Class.CekNULL(Dr("Sdh_Cetak")) = "" Or General_Class.CekNULL(Dr("Sdh_Cetak")) = "T" And General_Class.CekNULL(Dr("Flag_angkut")) = "" And General_Class.CekNULL(Dr("Selesai")) = "" Then
                        lvw.BackColor = Color.LightGray
                    ElseIf General_Class.CekNULL(Dr("Sdh_Cetak")) = "Y" And General_Class.CekNULL(Dr("Flag_angkut")) = "" And General_Class.CekNULL(Dr("Selesai")) = "" Then
                        lvw.BackColor = Color.LightYellow
                    ElseIf General_Class.CekNULL(Dr("Sdh_Cetak")) = "Y" And General_Class.CekNULL(Dr("Flag_angkut")) = "Y" And General_Class.CekNULL(Dr("Selesai")) = "Y" Then
                        lvw.BackColor = Color.LightGreen
                    End If

                    If General_Class.CekNULL(Dr("Flag_angkut")) = "Y" And General_Class.CekNULL(Dr("Selesai")) = "Y" Then
                        TotalMasuk += Val(HilangkanTanda(Dr("jumlah")))
                        TotalPalletMasuk += 1
                    Else
                        TotalBlmMasuk += Val(HilangkanTanda(Dr("jumlah")))
                        TotalPalletBlmMasuk += 1
                    End If

                Loop
            End Using

            If TotalBlmMasuk < 0 Then
                TotalBlmMasuk = 0
            End If

            Txt_JumlahMasuk.Text = Format(TotalMasuk, "N2")
            Txt_PalletMasuk.Text = Format(TotalPalletMasuk, "N2")
            Txt_JumlahBlmMasuk.Text = Format(TotalBlmMasuk, "N2")
            Txt_PalletBlmMasuk.Text = Format(TotalPalletBlmMasuk, "N2")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub BtnBarangMasuk_Cari_Click(sender As Object, e As EventArgs) Handles BtnBarangMasuk_Cari.Click

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
        End If

        If CheckBox2.Checked Then
            If ComboBox2.SelectedIndex = -1 Then
                MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain, Judul)
                ComboBox2.Focus() : Exit Sub
            ElseIf TextBox4.Text.Trim.Length = 0 Then
                MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain2, Judul)
                TextBox4.Focus() : Exit Sub
            End If
        End If

        Try
            pertama = 1

            OpenConn()

            LV_PembelianLoading.Items.Clear()
            Lv_PODetail.Items.Clear()
            ListView1.Items.Clear()

            Txt_JumlahMasuk.Text = ""
            Txt_PalletMasuk.Text = ""
            Txt_JumlahBlmMasuk.Text = ""
            Txt_PalletBlmMasuk.Text = ""

            SQL = "select a.No_Faktur,a.Kode_Supplier,a.selesai,b.Nama,a.No_SJ,a.No_Plat,a.Driver, a.tanggal_masuk, a.jam_masuk,tanggal_otw, eta, a.Status "
            SQL = SQL & "from EMI_Pembelian_Loading a, Suppliers b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Supplier = b.Kode_Supplier "

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
                            Lvw = LV_PembelianLoading.Items.Add(.Rows(i).Item("no_faktur"))

                            Lvw.SubItems.Add(.Rows(i).Item("kode_supplier"))
                            Lvw.SubItems.Add(.Rows(i).Item("nama"))

                            Lvw.SubItems.Add(.Rows(i).Item("no_sj"))
                            Lvw.SubItems.Add(.Rows(i).Item("no_plat"))
                            Lvw.SubItems.Add(.Rows(i).Item("Driver"))

                            If General_Class.CekNULL(.Rows(i).Item("tanggal_masuk")) <> "" Then
                                Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_masuk"), "dd MMM yyyy"))
                            Else
                                Lvw.SubItems.Add("-")
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("selesai")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(.Rows(i).Item("selesai"))
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("Status")) = "Y" Then
                                Lvw.BackColor = Color.DarkRed
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

    Private Sub DisplayRakToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If LV_PembelianLoading.Items.Count = 0 Or LV_PembelianLoading.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        EMI_Barang_Masuk_Display_Rak.TxtNoBM.Text = LV_PembelianLoading.FocusedItem.Text
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

    Private Sub CetakPerintahBongkarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakPerintahBongkarToolStripMenuItem.Click
        If LV_PembelianLoading.Items.Count = 0 Then Exit Sub

        Try
            OpenConn()

            Dim no_faktur As String = LV_PembelianLoading.FocusedItem.Text

            Dim isAvailable As Boolean = False
            Dim No_PO As String = ""
            Dim urutLoading As String = ""

            '=========================================================
            '=     CEK APAKAH DATA PEMBELIAN LOADING DI BATALKAN     =
            '=========================================================
            SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & no_faktur & "' and Status = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Gagal Cetak Ulang Karena Pembelian Loading Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '========================================
            '=     CEK APAKAH DATA LEBIH DARI 1     =
            '========================================
            Dim Has2Data As Boolean = False
            SQL = "select Distinct Count(No_Faktur) as Jumlah_Baris from EMI_Timbang_Unloading "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Loading = '" & no_faktur & "' and Status is null  "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("Jumlah_Baris") = 0 Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Data Timbang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        If Dr("Jumlah_Baris") > 1 Then

                            Dim tanya As String = MessageBox.Show("Terdapat lebih dari satu data penimbangan. Apakah Anda ingin memilih salah satu?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            If tanya = vbNo Then
                                CloseTrans()
                                CloseConn()
                                Exit Sub
                            ElseIf tanya = vbYes Then
                                Has2Data = True
                            End If
                        End If
                    End If
                End If
            End Using

            If Has2Data Then

                EMI_Barang_Masuk_Summary_Data_SD.NoLoading = no_faktur
                EMI_Barang_Masuk_Summary_Data_SD.Asal = "PERINTAHBONGKAR"
                EMI_Barang_Masuk_Summary_Data_SD.ShowDialog()
            Else

                Dim CrDoc As New Object
                Dim kertas As String = ""

                SQL = "select No_Faktur from EMI_Timbang_Unloading where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Loading = '" & no_faktur & "' and status is null "
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then

                        CrDoc = New Rpt_Surat_Perintah_Bongkar
                        kertas = "Faktur"

                        'CrDoc.SetDataSource(Ds)
                        'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        'CrDoc.SummaryInfo.ReportTitle = "Faktur Perintah Bongkar"
                        'CrDoc.RecordSelectionFormula = "{EMI_Timbang_Unloading.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Timbang_Unloading.No_Faktur}='" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "' "

                        'With A_Place_For_Printing2
                        '    .Text = "Faktur Perintah Bongkar"
                        '    .CrystalReportViewer1.ReportSource = CrDoc
                        '    .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                        '    .Refresh()
                        '    .Show()
                        'End With

                        '====================================

                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        'CrDoc.PrintOptions.PrinterName = ""
                        CrDoc.PrintOptions.PrinterName = PrinterNameSPB
                        CrDoc.RecordSelectionFormula = "{EMI_Timbang_Unloading.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Timbang_Unloading.No_Faktur}='" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "' "
                        'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        doctoprint.PrinterSettings.PrinterName = PrinterNameSPB
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
                    Else
                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub


                    End If
                End Using

            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub CetakPenerimaanBarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakPenerimaanBarangToolStripMenuItem.Click
        If LV_PembelianLoading.Items.Count = 0 Then Exit Sub

        Try
            OpenConn()

            Dim no_faktur As String = LV_PembelianLoading.FocusedItem.Text

            Dim isAvailable As Boolean = False
            Dim No_PO As String = ""
            Dim UrutLoading As String = ""


            '=========================================================
            '=     CEK APAKAH DATA PEMBELIAN LOADING DI BATALKAN     =
            '=========================================================
            SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & no_faktur & "' and Status = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Gagal Cetak Ulang Karena Pembelian Loading Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            '========================================
            '=     CEK APAKAH DATA LEBIH DARI 1     =
            '========================================
            Dim Has2Data As Boolean = False
            SQL = "select Distinct Count(No_Faktur) as Jumlah_Baris from EMI_Timbang_Unloading "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Loading = '" & no_faktur & "' and Status is null  "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("Jumlah_Baris") = 0 Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Data Timbang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        If Dr("Jumlah_Baris") > 1 Then

                            Dim tanya As String = MessageBox.Show("Terdapat lebih dari satu data penimbangan. Apakah Anda ingin memilih salah satu?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            If tanya = vbNo Then
                                CloseTrans()
                                CloseConn()
                                Exit Sub
                            ElseIf tanya = vbYes Then
                                Has2Data = True
                            End If
                        End If
                    End If
                End If
            End Using

            If Has2Data Then

                EMI_Barang_Masuk_Summary_Data_SD.NoLoading = no_faktur
                EMI_Barang_Masuk_Summary_Data_SD.Asal = "PENERIMAANBARANG"
                EMI_Barang_Masuk_Summary_Data_SD.ShowDialog()
            Else

                Dim CrDoc As New Object
                Dim kertas As String = ""

                SQL = "select No_Faktur from EMI_Timbang_Unloading where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Loading = '" & no_faktur & "' and status is null "
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then

                        CrDoc = New Rpt_Bukti_Penerimaan_Barang
                        kertas = "Faktur"

                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.SummaryInfo.ReportTitle = "Faktur Penerimaan Barang"
                        CrDoc.RecordSelectionFormula = "{EMI_Timbang_Unloading.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Timbang_Unloading.No_Faktur}='" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "' "

                        With A_Place_For_Printing2
                            .Text = "Faktur Penerimaan Barang"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                            .Refresh()
                            .Show()
                        End With

                        '====================================

                        'CrDoc.SetDataSource(Ds)
                        'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        ''CrDoc.PrintOptions.PrinterName = ""
                        'CrDoc.PrintOptions.PrinterName = PrinterNameBPB
                        'CrDoc.RecordSelectionFormula = "{EMI_Timbang_Unloading.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Timbang_Unloading.No_Faktur}='" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "' "
                        ''CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                        'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        ''doctoprint.DefaultPageSettings.Landscape = False
                        'doctoprint.PrinterSettings.PrinterName = PrinterNameBPB
                        'Dim rawKind As Integer
                        'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                        '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                        '        CrDoc.PrintOptions.PaperSize = rawKind
                        '        Exit For
                        '    End If
                        'Next

                        'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                        'CrDoc.PrintToPrinter(1, False, 1, 99)

                        'MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    Else
                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub

                    End If
                End Using

            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub CetakBuktiTimbangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakBuktiTimbangToolStripMenuItem.Click
        If LV_PembelianLoading.Items.Count = 0 Then Exit Sub

        Try
            OpenConn()

            Dim no_faktur As String = LV_PembelianLoading.FocusedItem.Text

            Dim isAvailable As Boolean = False
            Dim No_PO As String = ""
            Dim UrutLoading As String = ""

            '=========================================================
            '=     CEK APAKAH DATA PEMBELIAN LOADING DI BATALKAN     =
            '=========================================================
            SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & no_faktur & "' and Status = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Gagal Cetak Ulang Karena Pembelian Loading Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            '========================================
            '=     CEK APAKAH DATA LEBIH DARI 1     =
            '========================================
            Dim Has2Data As Boolean = False
            SQL = "select Distinct Count(No_Faktur) as Jumlah_Baris from EMI_Timbang_Unloading "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Loading = '" & no_faktur & "' and Status is null  "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("Jumlah_Baris") = 0 Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Data Timbang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        If Dr("Jumlah_Baris") > 1 Then

                            Dim tanya As String = MessageBox.Show("Terdapat lebih dari satu data penimbangan. Apakah Anda ingin memilih salah satu?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            If tanya = vbNo Then
                                CloseTrans()
                                CloseConn()
                                Exit Sub
                            ElseIf tanya = vbYes Then
                                Has2Data = True
                            End If
                        End If
                    End If
                End If
            End Using

            If Has2Data Then

                EMI_Barang_Masuk_Summary_Data_SD.NoLoading = no_faktur
                EMI_Barang_Masuk_Summary_Data_SD.Asal = "BUKTITIMBANG"
                EMI_Barang_Masuk_Summary_Data_SD.ShowDialog()
            Else

                Dim CrDoc As New Object
                Dim kertas As String = ""


                SQL = "select a.No_Faktur from Vw_Bukti_Timbang a, EMI_Timbang_Unloading b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and b.Status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and b.No_Loading = '" & no_faktur & "' "
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then

                        CrDoc = New Rpt_Bukti_Timbang
                        kertas = "Faktur"

                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.SummaryInfo.ReportTitle = "Faktur Bukti Timbang"
                        CrDoc.RecordSelectionFormula = "{Vw_Bukti_Timbang.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_Bukti_Timbang.No_Faktur}='" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "' "

                        With A_Place_For_Printing2
                            .Text = "Faktur Bukti Timbang"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                            .Refresh()
                            .Show()
                        End With

                        '====================================

                        'CrDoc.SetDataSource(Ds)
                        'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        ''CrDoc.PrintOptions.PrinterName = ""
                        'CrDoc.PrintOptions.PrinterName = PrinterNameBuktiTimbang
                        'CrDoc.RecordSelectionFormula = "{Vw_Bukti_Timbang.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_Bukti_Timbang.No_Faktur}='" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "' "
                        ''CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                        'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        'doctoprint.PrinterSettings.PrinterName = PrinterNameBuktiTimbang
                        'Dim rawKind As Integer
                        'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                        '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                        '        CrDoc.PrintOptions.PaperSize = rawKind
                        '        Exit For
                        '    End If
                        'Next

                        'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                        'CrDoc.PrintToPrinter(1, False, 1, 99)

                        'MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub


                    End If
                End Using

            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub SalinNoFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoFakturToolStripMenuItem.Click
        If LV_PembelianLoading.Items.Count = 0 Or LV_PembelianLoading.SelectedItems.Count = 0 Or LV_PembelianLoading.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(LV_PembelianLoading.FocusedItem.Text)
    End Sub

    Private Sub SalinBarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinBarcodeToolStripMenuItem.Click
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Or ListView1.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        GetData_Pallet(ListView1.FocusedItem.IndentCount)

        Clipboard.SetText(LvPallet_Qr)
    End Sub

    '======================================================================================================================================================================================
    '=     PEMBATALAN
    '======================================================================================================================================================================================
    Private Sub BatalkanRegisterMobilToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalkanRegisterMobilToolStripMenuItem.Click
        If LV_PembelianLoading.Items.Count = 0 Or LV_PembelianLoading.FocusedItem.Index = -1 Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim JudulNotif As String = "Pembatalan Register Kendaraan"

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_Register_Kendaraan") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Register Kendaraan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan Register Kendaraan Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            Dim NoLoading As String = LV_PembelianLoading.FocusedItem.Text

            '=========================================
            '=     CEK APAKAH LOADING DIBATALKAN     =
            '=========================================
            SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' and status = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan kendaraan tidak dapat dilakukan karena No Loading Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '==============================
            '=     CEK DATA KENDARAAN     =
            '==============================
            SQL = "select a.Status "
            SQL = SQL & "from EMI_Register_Kendaraan_BM a,EMI_Pembelian_Loading b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Fak_Loading_Barang = b.No_Faktur  "
            SQL = SQL & "and b.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Fak_Loading_Barang = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Kendaraan Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Kendaraan Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '===============================
            '=     CEK APAKAH SUDAH QC     =
            '===============================
            SQL = "select a.Kode_Perusahaan "
            SQL = SQL & "from EMI_Hasil_Quality_Control a, emi_pembelian_loading b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Fak_Loading_Barang = b.No_Faktur "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Jenis_QC = '1' "
            SQL = SQL & "and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Faktur = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan kendaraan tidak dapat dilakukan karena sudah melewati proses QC", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '==========================================
            '=     CEK APAKAH SUDAH TIMBANG MASUK     =
            '==========================================
            SQL = "select a.Kode_Perusahaan "
            SQL = SQL & "from EMI_Timbang_Unloading a, EMI_Pembelian_Loading b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Loading = b.No_Faktur "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Loading = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan kendaraan tidak dapat dilakukan karena sudah melewati proses Timbang Masuk", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '===============================
            '=     UPDATE DATA LOADING     =
            '===============================
            SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading  "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' "
            SQL = SQL & "and status is null "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    SQL = "update EMI_Pembelian_Loading  "
                    SQL = SQL & "set Flag_Security = NULL, Flag_QC_Pertama = NULL, Flag_QC = NULL, tanggal_masuk = NULL, jam_masuk = NULL, Seq_No = NULL "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and No_Faktur = '" & NoLoading & "' "
                    SQL = SQL & "and status is null "
                    ExecuteTrans(SQL)
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No Loading Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '======================================
            '=     UPDATE DATA DETAIL LOADING     =
            '======================================
            SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading_Detail "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    SQL = "update EMI_Pembelian_Loading_Detail "
                    SQL = SQL & "set Flag_QC_Pertama = NULL, Flag_QC = NULL, Warna = NULL "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and No_Faktur = '" & NoLoading & "' "
                    ExecuteTrans(SQL)
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No Loading Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '==========================================
            '=     UPDATE DATA REGISTER KENDARAAN     =
            '==========================================
            SQL = "select Kode_Perusahaan from EMI_Register_Kendaraan_BM "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Fak_Loading_Barang = '" & NoLoading & "' "
            SQL = SQL & "and status is null "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    SQL = "update EMI_Register_Kendaraan_BM "
                    SQL = SQL & "set Status = 'Y', UserID_Batal = '" & UserID & "', Tanggal_Batal = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Batal = '" & Format(tgl_skg, "HH:mm:ss") & "'  "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and No_Fak_Loading_Barang = '" & NoLoading & "' and status is null "
                    ExecuteTrans(SQL)
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Kendaraaan Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Kendaraan Berhasil Dibatalkan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        BtnBarangMasuk_Cari_Click(Me, New EventArgs)

    End Sub

    Private Sub BatalBarangMasukAndroidToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalBarangMasukAndroidToolStripMenuItem.Click
        If LV_PembelianLoading.Items.Count = 0 Or LV_PembelianLoading.FocusedItem.Index = -1 Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim JudulNotif As String = "Pembatalan Timbang Masuk"

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_Timbang_Masuk") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Timbang Masuk", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan Timbang Masuk pada No Loading Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            Dim NoLoading As String = LV_PembelianLoading.FocusedItem.Text

            '=========================================
            '=     CEK APAKAH LOADING DIBATALKAN     =
            '=========================================
            SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' and status = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Timbang Masuk tidak dapat dilakukan karena No Loading Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '====================================================
            '=     CEK APAKAH DATA BERADA DI TIMBANG KELUAR     =
            '====================================================
            'SQL = ";with cte as( SELECT a.lokasi, a.Kode_Supplier, a.No_Faktur, b.Nama, a.No_SJ, a.ETA, a.driver AS Supir, "
            'SQL = SQL & "a.no_plat AS Plat_Number,a.flag_proses_loading, a.No_faktur AS no_loading, a.ID_Jenis_Muatan, 'JNE' as Nama_Ekspedisi, "
            'SQL = SQL & "isnull((select top(1) 'Y' from EMI_Pembelian_Loading_detail x where x.no_faktur=a.no_faktur and "
            'SQL = SQL & "x.flag_timbang_masuk is null and a.Flag_Proses_loading is null ORDER BY x.no_faktur),'-') as Timbang_Masuk, "
            'SQL = SQL & "isnull((select top(1) 'Y' from EMI_Pembelian_Loading_detail x where x.no_faktur=a.no_faktur and "
            'SQL = SQL & "x.flag_sudah_bongkar_android is null and x.flag_timbang_masuk='Y' and a.Flag_Proses_Loading is null ORDER BY x.no_faktur),'-') as Unloading, "
            'SQL = SQL & "isnull((select top(1) 'Y' from EMI_Pembelian_Loading_detail x where x.no_faktur=a.no_faktur and "
            'SQL = SQL & "x.flag_sudah_bongkar_android ='Y' and a.Flag_timbang_keluar is null and a.Flag_Proses_Loading='Y' ORDER BY x.no_faktur),'-') as Timbang_Keluar, "
            'SQL = SQL & "isnull((select top(1) No_Faktur from emi_timbang_unloading x where x.no_loading=a.no_faktur and "
            'SQL = SQL & "x.flag_selesai is null ORDER BY x.no_loading),'-') as No_Timbangan "
            'SQL = SQL & "FROM EMI_Pembelian_Loading a, Suppliers b WHERE "
            'SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan AND a.Kode_Supplier = b.Kode_Supplier "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' AND a.Status IS NULL "
            'SQL = SQL & "and flag_security='Y' and Flag_Qc_Pertama='Y') "
            'SQL = SQL & "select * from cte "
            'SQL = SQL & "where timbang_masuk='Y' and No_Faktur = '" & NoLoading & "' "
            'SQL = SQL & "ORDER BY ETA DESC; "
            'Using Dr = OpenTrans(SQL)
            '    If Not Dr.Read Then
            '        Dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("Pembatalan Timbang Masuk tidak dapat dilakukan karena No Loading Tidak dalam Proses Timbang Masuk", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using

            SQL = "select a.Kode_Perusahaan "
            SQL = SQL & "from EMI_Pembelian_Loading_detail a , EMI_Pembelian_Loading b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Status is null "
            SQL = SQL & "and a.Flag_Timbang_Masuk is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Faktur = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Timbang Masuk tidak dapat dilakukan karena Data Belum Selesai Proses Timbang Masuk", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '================================================================
            '=     CEK APAKAH DATA TIMBANG SUDAH DI BATALKAN SEBELUMNYA     =
            '================================================================
            SQL = "select a.Kode_Perusahaan, a.Status, a.Flag_Selesai, b.Flag_Timbang_Keluar "
            SQL = SQL & "from EMI_Timbang_Unloading a, EMI_Pembelian_Loading b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Loading = b.No_Faktur "
            SQL = SQL & "and b.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Loading = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Timbang Masuk Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If General_Class.CekNULL(Dr("Flag_Selesai")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Timbang Masuk Sudah Berada pada Proses Timbang Keluar", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If General_Class.CekNULL(Dr("Flag_Timbang_Keluar")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Timbang Masuk Sudah Berada pada Proses Timbang Keluar", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Timbang Masuk Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=========================================
            '=     CEK DATA REGISTRASI KENDARAAN     =
            '=========================================
            SQL = "select a.Kode_Perusahaan, a.Status "
            SQL = SQL & "from EMI_Register_Kendaraan_BM a, EMI_Pembelian_Loading b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Fak_Loading_Barang = b.No_Faktur "
            SQL = SQL & "and b.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Fak_Loading_Barang = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Pembatalan Timbang Masuk tidak dapat dilakukan karena Data Registerasi Kendaraan Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Timbang Masuk tidak dapat dilakukan karena No Loading Belum Melalui Proses Registerasi Kendaraan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=========================
            '=     CEK DATA QC 1     =
            '=========================
            SQL = "select a.Kode_Perusahaan "
            SQL = SQL & "from EMI_Hasil_Quality_Control a, emi_pembelian_loading b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Fak_Loading_Barang = b.No_Faktur "
            SQL = SQL & "and b.Status is Null "
            SQL = SQL & "and a.Status = 'Y' "
            SQL = SQL & "and a.Jenis_QC = '1' "
            SQL = SQL & "and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Faktur = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Timbang Masuk tidak dapat dilakukan karena Data QC 1 Sudah Dibatalkan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=========================================
            '=     CEK DATA QC 1 FLAG QC_PERTAMA     =
            '=========================================
            SQL = "select a.Kode_Perusahaan "
            SQL = SQL & "from EMI_Hasil_Quality_Control a, emi_pembelian_loading b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Fak_Loading_Barang = b.No_Faktur "
            SQL = SQL & "and b.Status is Null "
            SQL = SQL & "and a.Status is Null "
            SQL = SQL & "and b.Flag_QC_Pertama is null "
            SQL = SQL & "and a.Jenis_QC = '1' "
            SQL = SQL & "and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Faktur = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Timbang Masuk tidak dapat dilakukan karena Data QC 1 Belum Selesai", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=========================
            '=     CEK DATA QC 2     =
            '=========================
            SQL = "select a.Kode_Perusahaan "
            SQL = SQL & "from EMI_Hasil_Quality_Control a, emi_pembelian_loading b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Fak_Loading_Barang = b.No_Faktur "
            SQL = SQL & "and b.Status is null "
            SQL = SQL & "and a.Status Is Null "
            SQL = SQL & "and a.Jenis_QC = '2' "
            SQL = SQL & "and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Faktur = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Timbang Masuk tidak dapat dilakukan karena Loading Sudah Masuk ke Tahan QC 2", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '==============================================
            '=     CEK DATA BARANG MASUK ANDROID FLAG     =
            '==============================================
            SQL = "select a.Kode_Perusahaan from EMI_Pembelian_Loading_Detail a, emi_pembelian_loading b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Status is null "
            SQL = SQL & "and a.Flag_Sudah_Bongkar_Android = 'Y' "
            SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Timbang Masuk tidak dapat dilakukan karena No Loading Sudah Dibongkar", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=========================================
            '=     CEK DATA BARANG MASUK ANDROID     =
            '=========================================
            SQL = "select a.Kode_Perusahaan "
            SQL = SQL & "from EMI_Barang_Masuk_Perpallet a, emi_pembelian_loading b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Pembelian_Loading = b.No_Faktur "
            SQL = SQL & "and b.Status is null and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Pembelian_Loading = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Timbang Masuk tidak dapat dilakukan karena Loading Sudah Masuk ke Tahap Barang Masuk Android", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '===================================
            '=     UPDATE PEMBELIAN LOADING    =
            '===================================
            SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    SQL = "update EMI_Pembelian_Loading set ID_Jenis_Muatan = NULL, flag_proses_loading = NULL, Flag_Timbang  = NULL "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' and status is null "
                    ExecuteTrans(SQL)
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No Loading Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '==========================================
            '=     UPDATE PEMBELIAN LOADING DETAIL    =
            '==========================================
            SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading_Detail "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    SQL = "update EMI_Pembelian_Loading_Detail set flag_timbang_masuk = NULL "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "'  "
                    ExecuteTrans(SQL)
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No Loading Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '===================================
            '=     UPDATE TIMBANG UNLOADING    =
            '===================================
            SQL = "select Kode_Perusahaan from EMI_Timbang_Unloading "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Loading = '" & NoLoading & "' and Status is null "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    SQL = "update EMI_Timbang_Unloading set Status = 'Y', ID_Jenis_Muatan = NULL, UserID_Batal = '" & UserID & "', Tanggal_Batal = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Batal = '" & Format(tgl_skg, "HH:mm:ss") & "' "
                    SQL = SQL & "where Kode_Perusahaan ='" & KodePerusahaan & "' and Status is null and No_Loading = '" & NoLoading & "' "
                    ExecuteTrans(SQL)
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Timbang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=====================
            '=     INSERT LOG    =
            '=====================
            SQL = "insert into N_EMI_Log_Pembatalan_Timbang_Keluar (Kode_Perusahaan, No_Faktur, Jenis, User_ID, Tanggal, Jam)"
            SQL = SQL & "values ('" & KodePerusahaan & "', '" & NoLoading & "', 'Timbang Masuk', '" & UserID & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "')"
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Timbang Masuk Berhasil Dibatalkan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        BtnBarangMasuk_Cari_Click(Me, New EventArgs)

    End Sub

    Private Sub BatalTimbangKeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalTimbangKeluarToolStripMenuItem.Click
        If LV_PembelianLoading.Items.Count = 0 Or LV_PembelianLoading.FocusedItem.Index = -1 Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim JudulNotif As String = "Pembatalan Timbang Keluar"

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_Timbang_Keluar") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Timbang Keluar", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan Timbang Keluar pada No Loading Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            Dim NoLoading As String = LV_PembelianLoading.FocusedItem.Text

            '=========================================
            '=     CEK APAKAH LOADING DIBATALKAN     =
            '=========================================
            SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' and status = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Timbang Keluar tidak dapat dilakukan karena No Loading Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '====================================================
            '=     CEK APAKAH DATA BERADA DI TIMBANG KELUAR     =
            '====================================================
            'SQL = ";with cte as( SELECT a.lokasi, a.Kode_Supplier, a.No_Faktur, b.Nama, a.No_SJ, a.ETA, a.driver AS Supir, "
            'SQL = SQL & "a.no_plat AS Plat_Number,a.flag_proses_loading, a.No_faktur AS no_loading, a.ID_Jenis_Muatan, 'JNE' as Nama_Ekspedisi, "
            'SQL = SQL & "isnull((select top(1) 'Y' from EMI_Pembelian_Loading_detail x where x.no_faktur=a.no_faktur and "
            'SQL = SQL & "x.flag_timbang_masuk is null and a.Flag_Proses_loading is null ORDER BY x.no_faktur),'-') as Timbang_Masuk, "
            'SQL = SQL & "isnull((select top(1) 'Y' from EMI_Pembelian_Loading_detail x where x.no_faktur=a.no_faktur and "
            'SQL = SQL & "x.flag_sudah_bongkar_android is null and x.flag_timbang_masuk='Y' and a.Flag_Proses_Loading is null ORDER BY x.no_faktur),'-') as Unloading, "
            'SQL = SQL & "isnull((select top(1) 'Y' from EMI_Pembelian_Loading_detail x where x.no_faktur=a.no_faktur and "
            'SQL = SQL & "x.flag_sudah_bongkar_android ='Y' and a.Flag_timbang_keluar is null and a.Flag_Proses_Loading='Y' ORDER BY x.no_faktur),'-') as Timbang_Keluar, "
            'SQL = SQL & "isnull((select top(1) No_Faktur from emi_timbang_unloading x where x.no_loading=a.no_faktur and "
            'SQL = SQL & "x.flag_selesai is null ORDER BY x.no_loading),'-') as No_Timbangan "
            'SQL = SQL & "FROM EMI_Pembelian_Loading a, Suppliers b WHERE "
            'SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan AND a.Kode_Supplier = b.Kode_Supplier "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' AND a.Status IS NULL "
            'SQL = SQL & "and flag_security='Y' and Flag_Qc_Pertama='Y') "
            'SQL = SQL & "select * from cte "
            'SQL = SQL & "where timbang_keluar='Y' and No_Faktur = '" & NoLoading & "' "
            'SQL = SQL & "ORDER BY ETA DESC; "
            'Using Dr = OpenTrans(SQL)
            '    If Not Dr.Read Then
            '        Dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("Pembatalan Timbang Keluar tidak dapat dilakukan karena No Loading Tidak dalam Proses Timbang Keluar", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using

            SQL = "select kode_perusahaan from EMI_Pembelian_Loading where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' and status is null "
            SQL = SQL & "and Flag_timbang_keluar = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Belum Selesai pada Proses Timbang Keluar", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '================================================================
            '=     CEK APAKAH DATA TIMBANG SUDAH DI BATALKAN SEBELUMNYA     =
            '================================================================
            SQL = "select a.Kode_Perusahaan "
            SQL = SQL & "from EMI_Timbang_Unloading a, EMI_Pembelian_Loading b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Loading = b.No_Faktur "
            SQL = SQL & "and b.status is null and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Loading = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Timbang Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using




            '=====================================================
            '=     CEK APAKAH TIMBANG KELUAR SUDAH DILAKUKAN     =
            '=====================================================
            SQL = "select a.kode_perusahaan "
            SQL = SQL & "from EMI_Timbang_Unloading a, EMI_Pembelian_Loading b "
            SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Loading = b.No_Faktur "
            SQL = SQL & "and b.Status is null "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.flag_Selesai is null " 'Ini adalah baris untuk cek apakah sudah timbang keluar atau belum
            SQL = SQL & "and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Loading = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No Loading Belum Timbang Keluar Sepenuhnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '====================================================
            '=     CEK APAKAH ADA DATA BARANG MASUK ANDROID     =
            '====================================================
            SQL = "select a.kode_perusahaan "
            SQL = SQL & "from EMI_Barang_Masuk_Perpallet a, EMI_Pembelian_Loading b "
            SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Pembelian_Loading = b.No_Faktur "
            SQL = SQL & "and b.Status is null "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Pembelian_Loading = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Barang Masuk Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub

                End If
            End Using

            '=================================================================
            '=     CEK APAKAH DATA BARANG MASUK ANDROID SUDAH DIBATALKAN     =
            '=================================================================
            'SQL = "select a.kode_perusahaan "
            'SQL = SQL & "from EMI_Barang_Masuk_Perpallet a, EMI_Pembelian_Loading b "
            'SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Pembelian_Loading = b.No_Faktur "
            'SQL = SQL & "and b.Status is null "
            'SQL = SQL & "and a.Status = 'Y' "
            'SQL = SQL & "and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Pembelian_Loading = '" & NoLoading & "' "
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        Dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("Data Barang Masuk Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using

            '==================================================
            '=     CEK APAKAH DATA SUDAH VALIDASI ANDROID     =
            '==================================================
            SQL = "select a.kode_perusahaan "
            SQL = SQL & "from EMI_Barang_Masuk_Perpallet a, EMI_Pembelian_Loading b "
            SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Pembelian_Loading = b.No_Faktur "
            SQL = SQL & "and b.Status is null "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Flag_angkut = 'Y' "
            'SQL = SQL & "and a.Selesai = 'Y' "
            SQL = SQL & "and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Pembelian_Loading = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Timbang Keluar tidak dapat dilakukan karena No Loading Sudah Divalidasi", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            '========================================
            '=     CEK APAKAH DATA LEBIH DARI 1     =
            '========================================
            Dim Has2Data As Boolean = False
            SQL = "select Distinct Count(No_Faktur) as Jumlah_Baris from EMI_Timbang_Unloading "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Loading = '" & NoLoading & "' and Status is null  "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("Jumlah_Baris") = 0 Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Data Timbang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        If Dr("Jumlah_Baris") > 1 Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Pembatalan Timbang Keluar tidak dapat dilakukan karena Terdapat lebih dari 1 penimbangan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub

                            'Dim tanya2 As String = MessageBox.Show("Terdapat lebih dari satu data penimbangan. Apakah Anda ingin memilih salah satu?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            'If tanya2 = vbNo Then
                            '    CloseTrans()
                            '    CloseConn()
                            '    Exit Sub
                            'ElseIf tanya2 = vbYes Then
                            '    Has2Data = True
                            'End If
                        End If
                    End If
                End If
            End Using

            'If Has2Data Then
            '    EMI_Barang_Masuk_Summary_Data_SD.NoLoading = NoLoading
            '    EMI_Barang_Masuk_Summary_Data_SD.Asal = "BATALTIMBANGKELUAR"
            '    EMI_Barang_Masuk_Summary_Data_SD.ShowDialog()

            'Else

            'End If

            '=========================
            '=     ROLLBACK DATA     =
            '=========================
            SQL = "select Kode_Perusahaan "
            SQL = SQL & "from EMI_Timbang_Unloading "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and no_loading= '" & NoLoading & "' "
            SQL = SQL & "and status is null "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    SQL = "update EMI_Timbang_Unloading "
                    SQL = SQL & "set Timbang_Keluar = NULL, Tgl_Timbang_Keluar = NULL, Jam_Timbang_Keluar = NULL, User_Timbang_Keluar = NULL, Foto_Timbang_Keluar_1 = NULL, "
                    SQL = SQL & "Foto_Timbang_Keluar_2 = NULL, Netto = NULL, flag_Selesai = NULL, Jumlah_Bags = NULL "
                    SQL = SQL & "where Kode_Perusahaan ='" & KodePerusahaan & "' "
                    SQL = SQL & "and no_loading= '" & NoLoading & "' "
                    SQL = SQL & "and status is null "
                    ExecuteTrans(SQL)
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Timbang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading_Detail "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    SQL = "update EMI_Pembelian_Loading_Detail set Jumlah_Masuk = NULL, flag_timbang_keluar = NULL "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' "
                    ExecuteTrans(SQL)
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Timbang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select a.No_Faktur, b.No_PO "
            SQL = SQL & "from EMI_Timbang_Unloading a, EMI_Timbang_Unloading_PO b, EMI_Timbang_Unloading_PO_Det c, EMI_Pembelian_Loading d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.No_PO = c.No_PO "
            SQL = SQL & "and a.No_Loading = d.No_Faktur "
            SQL = SQL & "and a.Status is null and d.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Loading = '" & NoLoading & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            SQL = "update EMI_Timbang_Unloading_PO_Det "
                            SQL = SQL & "set jumlah = NULL, satuan = NULL, nilai_barang = NULL, Satuan_Barang = NULL, Jumlah_Bag = NULL, Harga = NULL "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & .Rows(i).Item("No_Faktur") & "' "
                            SQL = SQL & "and no_po = '" & .Rows(i).Item("No_PO") & "'  "
                            ExecuteTrans(SQL)

                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Pallet Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            SQL = "select Jumlah, Jumlah_Bags, kode_stock_owner, kode_barang, Serial_Number_Awal "
            SQL = SQL & "from EMI_Barang_Masuk_Perpallet "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Pembelian_Loading= '" & NoLoading & "'  "
            SQL = SQL & "and status is null "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            SQL = "select Jumlah, Jumlah_Bags from Barang_SN  "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and serial_number = '" & .Rows(i).Item("Serial_Number_Awal") & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    If Val(HilangkanTanda(Dr("Jumlah"))) < Val(HilangkanTanda(.Rows(i).Item("Jumlah"))) Or Val(HilangkanTanda(Dr("Jumlah_Bags"))) < Val(HilangkanTanda(.Rows(i).Item("Jumlah_Bags"))) Then
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan Saat Rollback Data, Stock akan menjadi Negatif", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using

                            SQL = "select Good_Stock, Jumlah_Bags from barang "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and kode_stock_owner = '" & .Rows(i).Item("kode_stock_owner") & "' and kode_barang = '" & .Rows(i).Item("kode_barang") & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    If Val(HilangkanTanda(Dr("Good_Stock"))) < Val(HilangkanTanda(.Rows(i).Item("Jumlah"))) Or Val(HilangkanTanda(Dr("Jumlah_Bags"))) < Val(HilangkanTanda(.Rows(i).Item("Jumlah_Bags"))) Then
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan Saat Rollback Data, Stock akan menjadi Negatif", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using

                            SQL = "select kode_perusahaan "
                            SQL = SQL & "from Barang_SN "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and serial_number = '" & .Rows(i).Item("Serial_Number_Awal") & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                    SQL = "update Barang_SN set Jumlah = jumlah - " & Val(HilangkanTanda(.Rows(i).Item("Jumlah"))) & ", "
                                    SQL = SQL & "Jumlah_Bags = Jumlah_Bags - " & Val(HilangkanTanda(.Rows(i).Item("Jumlah_Bags"))) & " "
                                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and serial_number = '" & .Rows(i).Item("Serial_Number_Awal") & "' "
                                    ExecuteTrans(SQL)
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang SN Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            SQL = "select kode_perusahaan from barang "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and kode_stock_owner = '" & .Rows(i).Item("kode_stock_owner") & "' and kode_barang = '" & .Rows(i).Item("kode_barang") & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                    SQL = "update barang set Good_Stock = Good_Stock - " & Val(HilangkanTanda(.Rows(i).Item("Jumlah"))) & ", "
                                    SQL = SQL & "Jumlah_Bags = Jumlah_Bags - " & Val(HilangkanTanda(.Rows(i).Item("Jumlah_Bags"))) & " "
                                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and kode_stock_owner = '" & .Rows(i).Item("kode_stock_owner") & "' and kode_barang = '" & .Rows(i).Item("kode_barang") & "' "
                                    ExecuteTrans(SQL)
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            'CEK KESESUAIAN STOCK
                            SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & .Rows(i).Item("kode_stock_owner") & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & .Rows(i).Item("kode_barang") & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using Ds1 = BindingTrans(SQL)
                                If .Rows.Count <> 0 Then
                                    If Ds1.Tables("MyTable").Rows(0).Item("good_stock") <> Ds1.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Pallet Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            SQL = "select b.Metode_Timbang "
            SQL = SQL & "from EMI_Timbang_Unloading a, EMI_Master_Jenis_Muatan b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.ID_Jenis_Muatan = b.Id_Jenis_Muatan "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Loading = '" & NoLoading & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        If .Rows(0).Item("Metode_Timbang").ToString.ToUpper = "TRUCK SCALE" Then
                            SQL = "update EMI_Barang_Masuk_Perpallet "
                            SQL = SQL & "set Flag_Timbang = 0, jumlah = 0, Nilai_Barang = 0, tanggal_Timbang = NULL, jam_Timbang = NULL, user_Timbang = NULL, serial_number_awal = NULL, Flag_Timbang_Keluar = NULL "
                            SQL = SQL & "where Kode_Perusahaan ='" & KodePerusahaan & "' and No_Pembelian_Loading= '" & NoLoading & "' and status is null"
                            ExecuteTrans(SQL)
                        Else
                            SQL = "update EMI_Barang_Masuk_Perpallet "
                            SQL = SQL & "set tanggal_Timbang = NULL, jam_Timbang = NULL, user_Timbang = NULL, serial_number_awal = NULL, Flag_Timbang_Keluar = NULL "
                            SQL = SQL & "where Kode_Perusahaan ='" & KodePerusahaan & "' and No_Pembelian_Loading= '" & NoLoading & "' and status is null"
                            ExecuteTrans(SQL)
                        End If
                    End If
                End With
            End Using

            '=======================
            '=     UPDATE DATA     =
            '=======================
            SQL = "select Kode_Perusahaan "
            SQL = SQL & "from EMI_Pembelian_Loading where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' and status is null "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    SQL = "update EMI_Pembelian_Loading "
                    SQL = SQL & "set Flag_Proses_loading = 'Y', flag_sdh_update = NULL, Flag_Timbang_Keluar = NULL "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and No_Faktur = '" & NoLoading & "' "
                    SQL = SQL & "and status is null "
                    ExecuteTrans(SQL)
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No Loading Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=====================
            '=     INSERT LOG    =
            '=====================
            SQL = "insert into N_EMI_Log_Pembatalan_Timbang_Keluar (Kode_Perusahaan, No_Faktur, Jenis, User_ID, Tanggal, Jam)"
            SQL = SQL & "values ('" & KodePerusahaan & "', '" & NoLoading & "', 'Timbang Keluar', '" & UserID & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "')"
            ExecuteTrans(SQL)

            'If True Then
            '    CloseTrans()
            '    CloseConn()
            '    MessageBox.Show("Tahan")
            '    Exit Sub
            'End If

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Timbang Keluar Berhasil Dibatalkan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        BtnBarangMasuk_Cari_Click(Me, New EventArgs)

    End Sub

    Private Sub BatalPalletMasukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalPalletMasukToolStripMenuItem.Click
        If LV_PembelianLoading.Items.Count = 0 Or LV_PembelianLoading.FocusedItem.Index = -1 Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim JudulNotif As String = "Pembatalan Penyelesaian Mobil"

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_Pallet_Masuk") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Penyelesaian Mobil", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan Penyelesaian Mobil pada No Loading Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            Dim NoLoading As String = LV_PembelianLoading.FocusedItem.Text

            '=========================================
            '=     CEK APAKAH LOADING DIBATALKAN     =
            '=========================================
            SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' and status = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Penyelesaian Mobil tidak dapat dilakukan karena No Loading Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "Select Kode_Perusahaan from EMI_Pembelian_Loading "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' and status is null and flag_sudah_bongkar_android is NULL "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Penyelesaian Mobil tidak dapat dilakukan karena No Loading Beleum Menyelesaikan Mobil", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '==========================================
            '=     CEK APAKAH SUDAH TIMBANG MASUK     =
            '==========================================
            SQL = "select a.Kode_Perusahaan "
            SQL = SQL & "from EMI_Timbang_Unloading a, EMI_Pembelian_Loading b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Loading = b.No_Faktur "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Loading = '" & NoLoading & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Penyelesaian Mobil tidak dapat dilakukan karena Belum Timbang Masuk", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '===========================================
            '=     CEK APAKAH SUDAH TIMBANG KELUAR     =
            '===========================================
            SQL = "select kode_perusahaan from EMI_Pembelian_Loading where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' and status is null "
            SQL = SQL & "and Flag_timbang_keluar = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Penyelesaian Mobil tidak dapat dilakukan karena Data Belum Sudah Melalui Step Timbang Keluar", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            'SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading "
            'SQL = SQL & "where Flag_Timbang_Keluar = 'Y' and Flag_Proses_loading = 'Y' and Status is null "
            'SQL = SQL & "and Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' "
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        Dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("Pembatalan Penyelesaian Mobil tidak dapat dilakukan karena Data Belum Sudah Melalui Step Timbang Keluar", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using

            '=======================
            '=     UPDATE DATA     =
            '=======================
            SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' "
            SQL = SQL & "and status is null "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    SQL = "update EMI_Pembelian_Loading set flag_sudah_bongkar_android = NULL, Flag_QC = NULL  "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "' and status is null "
                    ExecuteTrans(SQL)
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Loading Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading_Detail "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoLoading & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    SQL = "update EMI_Pembelian_Loading_Detail set flag_sudah_bongkar_android = NULL, Flag_QC = NULL  "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoLoading & "'"
                    ExecuteTrans(SQL)
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Loading Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Penyelesaian Mobil Berhasil Dibatalkan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        BtnBarangMasuk_Cari_Click(Me, New EventArgs)
    End Sub

    '============================================================================================================================================================================
    '=     HANDLE KEY PRESS
    '============================================================================================================================================================================
    Private Sub LV_PembelianLoading_KeyPress(sender As Object, e As KeyPressEventArgs) Handles LV_PembelianLoading.KeyPress
        If e.KeyChar = Chr(13) Then Lv_PODetail.Focus()
    End Sub

    Private Sub Lv_PODetail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Lv_PODetail.KeyPress
        If e.KeyChar = Chr(13) Then ListView1.Focus()
    End Sub

    Private Sub ListView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ListView1.KeyPress
        If e.KeyChar = Chr(13) Then
            ComboBox6.DroppedDown = True
            ComboBox6.Focus()
        End If
    End Sub

    Private Sub ComboBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox6.KeyPress
        If e.KeyChar = Chr(13) Then CheckBox3.Focus()
    End Sub

    Private Sub CheckBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox3.KeyPress
        If e.KeyChar = Chr(13) Then CheckBox1.Focus()
    End Sub

    Private Sub CheckBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            If CheckBox1.Checked Then
                ComboBox3.DroppedDown = True
                ComboBox3.Focus()
            Else
                CheckBox2.Focus()
            End If

        End If
    End Sub

    Private Sub ComboBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker1.Focus()
    End Sub

    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub

    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Chr(13) Then CheckBox2.Focus()
    End Sub

    Private Sub CheckBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            If CheckBox2.Checked Then
                ComboBox2.DroppedDown = True
                ComboBox2.Focus()
            Else
                BtnBarangMasuk_Cari.Focus()
            End If

        End If
    End Sub

    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox4.Focus()
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Chr(13) Then BtnBarangMasuk_Cari.Focus()
    End Sub

    '======================================================================================================================================
    '=     HANDLE CETAK ULANG
    '======================================================================================================================================
    Public Sub CetakUlangPerintahBongkar(ByVal noFaktur As String, ByVal asal As String)

        If noFaktur.Trim.Length = 0 Then
            MessageBox.Show("No Faktur Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            If asal.ToUpper = "BUKTITIMBANG" Then

                SQL = "select a.No_Faktur from Vw_Bukti_Timbang a, EMI_Timbang_Unloading b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and b.Status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and b.No_Faktur = '" & noFaktur & "' "
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then

                        CrDoc = New Rpt_Bukti_Timbang
                        kertas = "Faktur"

                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.SummaryInfo.ReportTitle = "Faktur Bukti Timbang"
                        CrDoc.RecordSelectionFormula = "{Vw_Bukti_Timbang.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_Bukti_Timbang.No_Faktur}='" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "' "

                        With A_Place_For_Printing2
                            .Text = "Faktur Bukti Timbang"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                            .Refresh()
                            .Show()
                        End With

                        '====================================

                        'CrDoc.SetDataSource(Ds)
                        'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        ''CrDoc.PrintOptions.PrinterName = ""
                        'CrDoc.PrintOptions.PrinterName = PrinterNameBuktiTimbang
                        'CrDoc.RecordSelectionFormula = "{Vw_Bukti_Timbang.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_Bukti_Timbang.No_Faktur}='" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "' "
                        ''CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                        'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        'doctoprint.PrinterSettings.PrinterName = PrinterNameBuktiTimbang
                        'Dim rawKind As Integer
                        'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                        '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                        '        CrDoc.PrintOptions.PaperSize = rawKind
                        '        Exit For
                        '    End If
                        'Next

                        'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                        'CrDoc.PrintToPrinter(1, False, 1, 99)

                        'MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    Else
                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

            Else

                SQL = "select No_Faktur from EMI_Timbang_Unloading where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & noFaktur & "' and status is null "
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then


                        If asal.ToUpper = "PERINTAHBONGKAR" Then

                            CrDoc = New Rpt_Surat_Perintah_Bongkar
                            kertas = "Faktur"

                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Faktur Perintah Bongkar"
                            CrDoc.RecordSelectionFormula = "{EMI_Timbang_Unloading.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Timbang_Unloading.No_Faktur}='" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "' "

                            With A_Place_For_Printing2
                                .Text = "Faktur Perintah Bongkar"
                                .CrystalReportViewer1.ReportSource = CrDoc
                                .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                                .Refresh()
                                .Show()
                            End With

                            '=======================================================================

                            'CrDoc.SetDataSource(Ds)
                            'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            ''CrDoc.PrintOptions.PrinterName = ""
                            'CrDoc.PrintOptions.PrinterName = PrinterNameSPB
                            'CrDoc.RecordSelectionFormula = "{EMI_Timbang_Unloading.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Timbang_Unloading.No_Faktur}='" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "' "
                            ''CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                            'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                            'doctoprint.PrinterSettings.PrinterName = PrinterNameSPB
                            'Dim rawKind As Integer
                            'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                            'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                            '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                            '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                            '        CrDoc.PrintOptions.PaperSize = rawKind
                            '        Exit For
                            '    End If
                            'Next

                            'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                            'CrDoc.PrintToPrinter(1, False, 1, 99)

                            'MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        ElseIf asal.ToUpper = "PENERIMAANBARANG" Then

                            CrDoc = New Rpt_Bukti_Penerimaan_Barang
                            kertas = "Faktur"

                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Faktur Penerimaan Barang"
                            CrDoc.RecordSelectionFormula = "{EMI_Timbang_Unloading.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Timbang_Unloading.No_Faktur}='" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "' "

                            With A_Place_For_Printing2
                                .Text = "Faktur Penerimaan Barang"
                                .CrystalReportViewer1.ReportSource = CrDoc
                                .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                                .Refresh()
                                .Show()
                            End With

                            '====================================

                            'CrDoc.SetDataSource(Ds)
                            'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            ''CrDoc.PrintOptions.PrinterName = ""
                            'CrDoc.PrintOptions.PrinterName = PrinterNameBPB
                            'CrDoc.RecordSelectionFormula = "{EMI_Timbang_Unloading.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Timbang_Unloading.No_Faktur}='" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "' "
                            ''CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                            'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                            ''doctoprint.DefaultPageSettings.Landscape = False
                            'doctoprint.PrinterSettings.PrinterName = PrinterNameBPB
                            'Dim rawKind As Integer
                            'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                            'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                            '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                            '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                            '        CrDoc.PrintOptions.PaperSize = rawKind
                            '        Exit For
                            '    End If
                            'Next

                            'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                            'CrDoc.PrintToPrinter(1, False, 1, 99)

                            'MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)


                        End If

                    Else
                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub

                    End If
                End Using

            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

End Class