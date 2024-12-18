Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button


Public Class EMI_Barang_Masuk_Summary_Data

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

        LV_PembelianLoading.Items.Clear()

        LV_PembelianLoading.Columns.Add(Base_Language.Lang_Global_NoFaktur, 170, HorizontalAlignment.Left)
        LV_PembelianLoading.Columns.Add(Base_Language.Lang_Global_Supplier, 0, HorizontalAlignment.Left)
        LV_PembelianLoading.Columns.Add(Base_Language.lang_global_Nama_Supplier, 250, HorizontalAlignment.Left)
        LV_PembelianLoading.Columns.Add(Base_Language.Lang_GLOBAL_No_Surat_Jalan, 110, HorizontalAlignment.Center)
        LV_PembelianLoading.Columns.Add(Base_Language.Lang_Global_PlatNomor, 110, HorizontalAlignment.Center)
        LV_PembelianLoading.Columns.Add(Base_Language.Lang_Global_Supir, 110, HorizontalAlignment.Center)
        LV_PembelianLoading.Columns.Add("Tanggal Masuk", 100, HorizontalAlignment.Center)
        LV_PembelianLoading.Columns.Add("Selesai", 80, HorizontalAlignment.Center)

        LV_PembelianLoading.View = View.Details

        Lv_PODetail.Items.Clear()
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_No_PO, 120, HorizontalAlignment.Left) '
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_KodeBarang, 120, HorizontalAlignment.Left) '
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_NamaBarang, 170, HorizontalAlignment.Left) '
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_Satuan, 120, HorizontalAlignment.Center)
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_Harga, 110, HorizontalAlignment.Right) '
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_Tanggal_Produksi, 110, HorizontalAlignment.Center) '
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_Tanggal_Expired, 110, HorizontalAlignment.Center) '
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_Jumlah, 100, HorizontalAlignment.Center) '
        Lv_PODetail.Columns.Add("Jumlah Masuk", 110, HorizontalAlignment.Center)
        Lv_PODetail.Columns.Add(Base_Language.Lang_Global_Satuan, 0, HorizontalAlignment.Center)

        Lv_PODetail.View = View.Details


        ListView1.Columns.Add(Base_Language.Lang_Global_KodeBarang, 120, HorizontalAlignment.Left) '
        ListView1.Columns.Add(Base_Language.Lang_Global_NamaBarang, 200, HorizontalAlignment.Left) '
        ListView1.Columns.Add(Base_Language.Lang_Global_Jumlah, 100, HorizontalAlignment.Right) '
        ListView1.Columns.Add(Base_Language.Lang_Global_Satuan, 80, HorizontalAlignment.Center) '
        ListView1.Columns.Add("Batch Number", 120, HorizontalAlignment.Left) '
        ListView1.Columns.Add("QR Code", 170, HorizontalAlignment.Left) '
        ListView1.Columns.Add("Kode Rak", 170, HorizontalAlignment.Center) '
        ListView1.Columns.Add("Kode Voucher", 120, HorizontalAlignment.Center) '
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
            ComboBox3.Items.Add("Tanggal") : Arr1.Add("a.Tanggal")

            'TextBoxa.Text = "0" 
            ComboBox3.Enabled = False : ComboBox2.Enabled = False
            DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            TextBox4.Enabled = False

            ComboBox2.Items.Clear() : ComboBox2.Text = "" : Arr2.Clear()
            ComboBox2.Items.Add("No Faktur") : Arr2.Add("a.no_faktur")
            ComboBox2.Items.Add("Kode  Supplier") : Arr2.Add("a.kode_supplier")
            ComboBox2.Items.Add("Nama") : Arr2.Add("b.nama")
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

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LV_PembelianLoading.SelectedIndexChanged
        Try
            OpenConn()
            Lv_PODetail.Items.Clear()

            SQL = "select b.no_po,b.Kode_Barang, c.Nama, "
            SQL = SQL & "isnull((select Harga from EMI_Pembelian_PO_Detail x where x.Kode_Perusahaan = b.Kode_Perusahaan and x.No_Urut= b.Urut_PO ), 0 ) as Harga, "
            SQL = SQL & "b.jumlah as Jumlah_Kirim,b.Satuan,b.Tanggal_Produksi,b.Tanggal_Expired "
            SQL = SQL & ",isnull((select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA',b.Kode_Barang, b.Satuan_Barang,x.Satuan,b.Jumlah_Masuk)  from Barang_Detail_Satuan x where b.Kode_Perusahaan = x.Kode_Perusahaan  "
            SQL = SQL & "and b.Kode_Barang = x.Kode_barang and x.Flag_Tampil_Display = 'Y' ),0) as Jumlah_masuk "
            SQL = SQL & ",isnull((select x.Satuan from Barang_Detail_Satuan x where b.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and b.Kode_Barang = x.Kode_barang and x.Flag_Tampil_Display = 'Y' ),0) as Satuan_masuk "
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
                    lvw.SubItems.Add(Format(Dr("harga"), "N2"))
                    lvw.SubItems.Add(Format(Dr("Tanggal_Produksi"), "dd MMM yyyy"))
                    lvw.SubItems.Add(Format(Dr("Tanggal_Expired"), "dd MMM yyyy"))
                    lvw.SubItems.Add(Format(Dr("Jumlah_Kirim"), "N0"))
                    lvw.SubItems.Add(Format(Dr("jumlah_masuk"), "N0"))
                    lvw.SubItems.Add(Dr("satuan_masuk"))

                Loop
            End Using

            ListView1.Items.Clear()
            SQL = "Select a.Batch_Number, Qr_Code+'-'+Kode_Unik_Berjalan as QR_Code,a.Kode_Barang, b.nama, a.Tgl_Produksi_Real, "
            SQL = SQL & "a.Tgl_Expired_Real, a.jumlah, a.satuan, a.Id_Warehouse, c.Labeling_WMS_Position, a.Kode_Voucher "
            SQL = SQL & "From EMI_Barang_Masuk_Perpallet a, Barang b, View_Warehouse_Position c "
            SQL = SQL & "Where no_Pembelian_loading ='" & LV_PembelianLoading.FocusedItem.Text & "' "
            SQL = SQL & "And a.Kode_Barang = b.Kode_Barang And a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "And a.Kode_Perusahaan = c.Kode_Perusahaan And a.Id_Warehouse = c.Id_WMS_Warehouse_Position and a.selesai='Y' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = ListView1.Items.Add(Dr("Kode_Barang"))
                    lvw.SubItems.Add(Dr("nama"))
                    lvw.SubItems.Add(Format(Dr("jumlah"), "N0"))
                    lvw.SubItems.Add(Dr("satuan"))
                    lvw.SubItems.Add(Dr("Batch_Number"))
                    lvw.SubItems.Add(Dr("QR_Code"))
                    lvw.SubItems.Add(Dr("Labeling_WMS_Position"))
                    lvw.SubItems.Add(Dr("Kode_Voucher"))
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

            LV_PembelianLoading.Items.Clear()
            Lv_PODetail.Items.Clear()


            SQL = "select a.No_Faktur,a.Kode_Supplier,a.selesai,b.Nama,a.No_SJ,a.No_Plat,a.Driver, a.tanggal_masuk,a.jam_masuk,tanggal_otw, eta "
            SQL = SQL & "from EMI_Pembelian_Loading a, Suppliers b  "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and a.Status is null "

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


                            Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_masuk"), "dd MMM yyyy"))

                            If General_Class.CekNULL(.Rows(i).Item("selesai")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(.Rows(i).Item("selesai"))
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
        'EMI_Barang_Masuk_Display_Rak.TxtNoBM.Text = LV_PembelianLoading.FocusedItem.Text
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
End Class