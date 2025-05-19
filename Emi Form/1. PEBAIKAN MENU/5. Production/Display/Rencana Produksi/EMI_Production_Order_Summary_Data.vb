Public Class EMI_Production_Order_Summary_Data

    Dim Arr1, Arr2, Arr3, Arr4, arrStatus, arrRelease As New ArrayList
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

        Lv_PR.Items.Clear()
        Lv_PR.Columns.Add(Base_Language.Lang_Global_NoFaktur, 150, HorizontalAlignment.Left)
        Lv_PR.Columns.Add("Production Order Created", 200, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("Production Order Released", 200, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("Id_Routing", 0, HorizontalAlignment.Left)
        Lv_PR.Columns.Add("Routing", 150, HorizontalAlignment.Left)
        Lv_PR.Columns.Add("Jumlah", 100, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("Jumlah Masuk", 100, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("Sisa", 100, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("% Complete", 100, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        Lv_PR.Columns.Add("Status Production Order", 200, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("User ID", 100, HorizontalAlignment.Left)
        Lv_PR.View = View.Details

        Lv_PRDetail.Items.Clear()
        Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_NoFaktur, 150, HorizontalAlignment.Left)
        Lv_PRDetail.Columns.Add("No SO", 150, HorizontalAlignment.Left)
        Lv_PRDetail.Columns.Add("Kode Stock Owner", 150, HorizontalAlignment.Left)
        Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left)
        Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_NamaBarang, 200, HorizontalAlignment.Left)
        Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_jumlah_kuantiti, 100, HorizontalAlignment.Center)
        Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_Satuan, 100, HorizontalAlignment.Center)
        Lv_PRDetail.Columns.Add("Jenis Order", 100, HorizontalAlignment.Center)
        Lv_PRDetail.View = View.Details

        Lv_Split.Items.Clear()
        Lv_Split.Columns.Add(Base_Language.Lang_Global_No_Transaksi, 150, HorizontalAlignment.Left)
        Lv_Split.Columns.Add("No PO", 0, HorizontalAlignment.Left)
        Lv_Split.Columns.Add("Lokasi", 0, HorizontalAlignment.Left)
        Lv_Split.Columns.Add("Tanggal", 130, HorizontalAlignment.Center)
        Lv_Split.Columns.Add("Jam", 100, HorizontalAlignment.Center)
        Lv_Split.Columns.Add("Kode Stock Owner", 0, HorizontalAlignment.Left)
        Lv_Split.Columns.Add(Base_Language.Lang_Global_KodeBarang, 0, HorizontalAlignment.Left)
        Lv_Split.Columns.Add(Base_Language.Lang_Global_NamaBarang, 0, HorizontalAlignment.Left)
        Lv_Split.Columns.Add(Base_Language.Lang_Global_jumlah_kuantiti, 100, HorizontalAlignment.Center)
        Lv_Split.Columns.Add(Base_Language.Lang_Global_Satuan, 100, HorizontalAlignment.Center)
        Lv_Split.Columns.Add("Tanggal Produksi", 130, HorizontalAlignment.Center)
        Lv_Split.Columns.Add("Jam Produksi", 100, HorizontalAlignment.Center)
        Lv_Split.Columns.Add("No Batch", 150, HorizontalAlignment.Left)
        Lv_Split.View = View.Details

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
            ComboBox2.Items.Add("No Faktur") : Arr2.Add("a.no_faktur")
            'ComboBox2.Items.Add("NO Nota") : Arr2.Add("a.no_nota")
            'ComboBox2.Items.Add("Kode Supplier") : Arr2.Add("a.kode_supplier")

            Label1.Text = "Summary Data - Production Order"
            CheckBox3.Text = Base_Language.Lang_Global_Hari_ini
            CheckBox1.Text = Base_Language.Lang_Global_Para_Tbl
            CheckBox2.Text = Base_Language.Lang_Global_Para_lain
            BtnBarangMasuk_Cari.Text = Base_Language.Lang_Global_Cari


            Cmb_Status.Items.Clear() : arrStatus.Clear()
            Cmb_Status.Items.Add("---SEMUA---") : arrStatus.Add("---SEMUA---")
            Cmb_Status.Items.Add("Aktif") : arrStatus.Add("a.Status is null")
            Cmb_Status.Items.Add("Batal") : arrStatus.Add("a.Status is not null")
            Cmb_Status.SelectedIndex = 0

            Cmb_Release.Items.Clear() : arrRelease.Clear()
            Cmb_Release.Items.Add("---SEMUA---") : arrRelease.Add("---SEMUA---")
            Cmb_Release.Items.Add("Release") : arrRelease.Add("a.Flag_Release = 'Y'")
            Cmb_Release.Items.Add("Submit") : arrRelease.Add("a.Flag_Release is null")
            Cmb_Release.SelectedIndex = 0



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



    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_PR.SelectedIndexChanged
        Try
            OpenConn()
            Lv_PRDetail.Items.Clear()
            Lv_Split.Items.Clear()
            'SQL = "select a.kode_stock_owner, a.Kode_Barang,b.Nama,a.jumlah,a.Satuan,"
            ''jumlah masuk
            'SQL = SQL & "isnull((select y.Jumlah from EMI_Pembelian_PO x, EMI_Pembelian_PO_Det y "
            'SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur "
            'SQL = SQL & "and y.Kode_Perusahaan = a.Kode_Perusahaan and y.no_urut_pr = a.No_Urut), "
            'SQL = SQL & "a.Jumlah) as jumlah_masuk, "
            ''sisa
            'SQL = SQL & "(a.jumlah - isnull((select y.Jumlah from EMI_Pembelian_PO x, EMI_Pembelian_PO_Det y "
            'SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and "
            'SQL = SQL & "y.Kode_Perusahaan = a.Kode_Perusahaan and y.no_urut_pr = a.No_Urut), a.Jumlah)) "
            'SQL = SQL & "as sisa, "
            ''percentComplete
            'SQL = SQL & "(isnull((select y.Jumlah from EMI_Pembelian_PO x, EMI_Pembelian_PO_Det y "
            'SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur "
            'SQL = SQL & "and y.Kode_Perusahaan = a.Kode_Perusahaan and y.no_urut_pr = a.No_Urut), "
            'SQL = SQL & "a.Jumlah) / a.jumlah) * 100 as percentComplete "
            'SQL = SQL & "From EMI_Purchase_Requisition_Detail a, barang b "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and "
            'SQL = SQL & "a.Kode_Stock_Owner = b.Kode_Stock_Owner  and a.Kode_Barang = b.Kode_Barang "
            'SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.no_faktur = '" & Lv_PR.FocusedItem.SubItems(0).Text & "' "
            SQL = "select a.no_faktur, a.no_so, a.kode_stock_owner, a.Kode_Barang, b.Nama, a.jumlah, a.Satuan, a.jenis_order "
            SQL = SQL & "From emi_order_produksi_detail a, barang b where "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.kode_perusahaan = b.kode_perusahaan "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang and a.kode_stock_owner=b.kode_stock_owner "
            SQL = SQL & "and a.no_faktur = '" & Lv_PR.FocusedItem.SubItems(0).Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_PRDetail.Items.Add(Dr("no_faktur"))
                    lvw.SubItems.Add(Dr("no_so"))
                    lvw.SubItems.Add(Dr("kode_stock_owner"))
                    lvw.SubItems.Add(Dr("Kode_Barang"))
                    lvw.SubItems.Add(Dr("Nama"))
                    lvw.SubItems.Add(Format(Dr("jumlah"), "N0"))
                    lvw.SubItems.Add(Dr("Satuan"))
                    lvw.SubItems.Add(Dr("Jenis_Order"))
                Loop
            End Using

            SQL = "select a.No_Transaksi, a.No_PO, a.Lokasi, a.Tanggal, a.Jam, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama as Nama_Barang, "
            SQL = SQL & "a.Jumlah, a.Satuan, a.Tgl_Produksi, a.Jam_Produksi, a.No_Batch "
            SQL = SQL & "From Emi_Split_Production_Order a, barang b "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.kode_perusahaan = b.kode_perusahaan "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang and a.kode_stock_owner=b.kode_stock_owner "
            SQL = SQL & "and a.No_PO = '" & Lv_PR.FocusedItem.SubItems(0).Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_Split.Items.Add(Dr("No_Transaksi"))
                    lvw.SubItems.Add(Dr("No_PO"))
                    lvw.SubItems.Add(Dr("Lokasi"))
                    lvw.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    lvw.SubItems.Add(Dr("Jam"))
                    lvw.SubItems.Add(Dr("Kode_Stock_Owner"))
                    lvw.SubItems.Add(Dr("Kode_Barang"))
                    lvw.SubItems.Add(Dr("Nama_Barang"))
                    lvw.SubItems.Add(Format(Dr("jumlah"), "N0"))
                    lvw.SubItems.Add(Dr("Satuan"))
                    lvw.SubItems.Add(Format(Dr("Tgl_Produksi"), "dd MMM yyyy"))
                    lvw.SubItems.Add(Dr("Jam_Produksi"))
                    lvw.SubItems.Add(Dr("No_Batch"))
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

            Lv_PR.Items.Clear()
            Lv_PRDetail.Items.Clear()
            Lv_Split.Items.Clear()

            SQL = "select a.no_faktur, a.Tanggal, a.Tanggal_Release, a.Keterangan, a.Id_Routing, b.keterangan as routing, a.jumlah, a.satuan, a.UserId, a.Status, a.Flag_Release "
            SQL = SQL & "from emi_order_produksi a, EMI_Master_Routing b "
            SQL = SQL & "where a. Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Routing = b.Id_Routing "

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

            If Cmb_Status.SelectedIndex <> 0 Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrStatus(Cmb_Status.SelectedIndex) & " "
            End If

            If Cmb_Release.SelectedIndex <> 0 Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrRelease(Cmb_Release.SelectedIndex) & " "
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
                            Lvw = Lv_PR.Items.Add(.Rows(i).Item("no_faktur"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal"), "dd MMM yyyy"))
                            If General_Class.CekNULL(.Rows(i).Item("tanggal_release")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_release"), "dd MMM yyyy"))
                            End If
                            Lvw.SubItems.Add(.Rows(i).Item("Id_Routing"))
                            Lvw.SubItems.Add(.Rows(i).Item("routing"))
                            If General_Class.CekNULL(.Rows(i).Item("jumlah")) = "" Then
                                Lvw.SubItems.Add("0")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("jumlah"), "N0"))
                            End If
                            If General_Class.CekNULL(.Rows(i).Item("satuan")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(.Rows(i).Item("satuan"))
                            End If
                            Lvw.SubItems.Add("0")
                            Lvw.SubItems.Add("0")
                            Lvw.SubItems.Add("0.0")
                            Lvw.SubItems.Add(.Rows(i).Item("keterangan"))
                            Lvw.SubItems.Add("-")
                            Lvw.SubItems.Add(.Rows(i).Item("userid"))


                            If Not General_Class.CekNULL(.Rows(i).Item("Flag_Release")) = "" Then
                                Lvw.BackColor = Color.LightGreen
                            End If

                            If Not General_Class.CekNULL(.Rows(i).Item("Status")) = "" Then
                                Lvw.BackColor = Color.FromArgb(242, 139, 130)
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


    Private Sub BatalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalToolStripMenuItem.Click
        If Lv_PR.Items.Count = 0 Then Exit Sub


        Try
            OpenConn()

            Dim SelectedFaktur As String = Lv_PR.FocusedItem.SubItems(0).Text

            '========================================
            '=     CEK APAKAH PO SUDAH BERJALAN     =
            '========================================
            SQL = "select Kode_Perusahaan from Emi_Split_Production_Order where Kode_Perusahaan = '" & KodePerusahaan & "' and No_PO = '" & SelectedFaktur & "' and Status is null "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        CloseConn()
                        MessageBox.Show("Tidak Bisa Membatalkan PO yang sudah Mulai Produksi")
                        Exit Sub
                    Else
                        SQL = "update EMI_Order_Produksi set Status='Y' where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & SelectedFaktur & "'"
                        ExecuteTrans(SQL)
                    End If
                End With
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        BtnBarangMasuk_Cari_Click(sender, e)

    End Sub
End Class