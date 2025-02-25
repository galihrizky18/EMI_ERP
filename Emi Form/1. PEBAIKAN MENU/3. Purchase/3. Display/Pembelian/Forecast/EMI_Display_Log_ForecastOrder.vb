Public Class EMI_Display_Log_ForecastOrder

    Dim Arr1, Arr2, Arr3, Arr4 As New ArrayList
    Public arrBulan, arrBulanMM, arrCmbParamBarang As New ArrayList
    Dim pertama As Integer = 1
    Dim T As Color = Color.Blue
    Dim KT As Color = Color.Red
    Dim KY As Color = Color.Green
    Dim Batal As Color = Color.Black

    Private Sub Display_Pembelian_Barang_Masuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        Lv_PR.Columns.Clear()
        Lv_PR.Columns.Add(Base_Language.Lang_Global_NoFaktur, 0, HorizontalAlignment.Left) '0
        Lv_PR.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left) '1
        Lv_PR.Columns.Add("Nama Barang", 210, HorizontalAlignment.Left) '2
        Lv_PR.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '3
        Lv_PR.Columns.Add("Bulan", 80, HorizontalAlignment.Center) '4
        Lv_PR.Columns.Add("Tahun", 80, HorizontalAlignment.Center) '5
        Lv_PR.Columns.Add("Urut", 0, HorizontalAlignment.Center) '6
        Lv_PR.Columns.Add("Nilai Sales", 100, HorizontalAlignment.Right) '7
        Lv_PR.Columns.Add("Nilai PPIC", 100, HorizontalAlignment.Right) '8
        Lv_PR.Columns.Add("Jenis", 80, HorizontalAlignment.Center) '9
        Lv_PR.Columns.Add("Tanggal", 100, HorizontalAlignment.Center) '10
        Lv_PR.Columns.Add("Jam", 80, HorizontalAlignment.Center) '11
        Lv_PR.Columns.Add("User ID", 80, HorizontalAlignment.Center) '12

        Lv_DataBarang.Columns.Clear()
        Lv_DataBarang.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left)
        Lv_DataBarang.Columns.Add("Nama", 300, HorizontalAlignment.Left)
        Lv_DataBarang.Location = New Point(119, 105)
        Lv_DataBarang.Visible = False
        kosong()
        'BtnBarangMasuk_Cari_Click("", e)
    End Sub

    Private Sub kosong()



        Try
            OpenConn()

            Cmb_Bulan.Items.Clear() : arrBulan.Clear() : arrBulanMM.Clear()
            Cmb_Bulan.Items.Add("January") : arrBulan.Add("1") : arrBulanMM.Add("01")
            Cmb_Bulan.Items.Add("February") : arrBulan.Add("2") : arrBulanMM.Add("02")
            Cmb_Bulan.Items.Add("March") : arrBulan.Add("3") : arrBulanMM.Add("03")
            Cmb_Bulan.Items.Add("April") : arrBulan.Add("4") : arrBulanMM.Add("04")
            Cmb_Bulan.Items.Add("May") : arrBulan.Add("5") : arrBulanMM.Add("05")
            Cmb_Bulan.Items.Add("June") : arrBulan.Add("6") : arrBulanMM.Add("06")
            Cmb_Bulan.Items.Add("July") : arrBulan.Add("7") : arrBulanMM.Add("07")
            Cmb_Bulan.Items.Add("August") : arrBulan.Add("8") : arrBulanMM.Add("08")
            Cmb_Bulan.Items.Add("September") : arrBulan.Add("9") : arrBulanMM.Add("09")
            Cmb_Bulan.Items.Add("October") : arrBulan.Add("10") : arrBulanMM.Add("10")
            Cmb_Bulan.Items.Add("November") : arrBulan.Add("11") : arrBulanMM.Add("11")
            Cmb_Bulan.Items.Add("December") : arrBulan.Add("12") : arrBulanMM.Add("12")
            Cmb_Bulan.SelectedIndex = -1

            Cmb_Tahun.Items.Clear()
            Dim tahun_awal As Integer = Date.Now.Year - 2
            Dim tahun_akhir As Integer = Date.Now.Year + 2
            For a As Integer = tahun_awal To tahun_akhir
                Cmb_Tahun.Items.Add(a)
            Next
            Cmb_Tahun.SelectedIndex = -1

            ComboBox6.Items.Clear()
            ComboBox6.Items.Add(Base_Language.Lang_Global_SeluruhCombobox)

            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox6.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using

            ComboBox6.Text = Lokasi



            'ComboBox3.Items.Add("Y") : Arr4.Add("Y")
            'ComboBox3.Items.Add("T") : Arr4.Add("T")
            'ComboBox3.SelectedIndex = 1

            ComboBox3.Items.Clear() : Arr1.Clear()
            ComboBox3.Items.Add("Tanggal") : Arr1.Add("Tanggal")

            Cmb_KategoriBesar.Items.Clear() : Cmb_KategoriBesar.SelectedIndex = -1
            Cmb_KategoriBesar.Items.Add("---SELURUH---")
            SQL = "select Kode_Kategori_Besar from Kategori_Besar where Kode_Perusahaan = "
            SQL = SQL & "'" & KodePerusahaan & "' order by Kode_Kategori_Besar"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_KategoriBesar.Items.Add(Dr("Kode_Kategori_Besar"))
                Loop
            End Using

            Cmb_KategoriBesar.SelectedIndex = 0

            cmbParamBarang.Items.Clear() : arrCmbParamBarang.Clear()
            cmbParamBarang.Items.Add("Kode Barang") : arrCmbParamBarang.Add("kode_barang")
            cmbParamBarang.Items.Add("Nama") : arrCmbParamBarang.Add("nama")


            cmbParamBarang.SelectedIndex = 0


            Cb_ParamBarang.Checked = True




            'TextBoxa.Text = "0" 
            ComboBox3.Enabled = False : Cmb_ParamLain.Enabled = False
            DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            Txt_ParamLain.Enabled = False

            Cmb_ParamLain.Items.Clear() : Cmb_ParamLain.Text = "" : Arr2.Clear()
            Cmb_ParamLain.Items.Add("Satuan") : Arr2.Add("satuan")
            Cmb_ParamLain.Items.Add("Jenis") : Arr2.Add("Jenis")
            Cmb_ParamLain.Items.Add("User ID") : Arr2.Add("UserID")

            Label1.Text = "Display - Log Forecast Order"
            'Cb_TransaksiHrIni.Text = Base_Language.Lang_Global_Hari_ini
            Cb_ParamTgl.Text = Base_Language.Lang_Global_Para_Tbl
            Cb_ParamLain.Text = Base_Language.Lang_Global_Para_lain
            BtnBarangMasuk_Cari.Text = Base_Language.Lang_Global_Cari

            Cb_ParamBarang.Checked = True
            'Cb_TransaksiHrIni.Checked = False
            Cb_ParamTgl.Checked = False
            Cb_ParamLain.Checked = False

            'Cmb_KategoriBesar.Enabled = False
            'Cmb_KategoriKecil.Enabled = False
            'Txt_KdBrg.Enabled = False
            'Txt_NmBrg.Enabled = False
            Cmb_Bulan.Enabled = False
            Cmb_Tahun.Enabled = False
            'Cmb_KategoriBesar.SelectedIndex = -1
            'Cmb_KategoriKecil.SelectedIndex = -1
            Txt_KdBrg.Text = ""


            CloseConn()
        Catch ex As Exception
            ComboBox6.Items.Clear()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            If CekButtonRole("Ganti_Lokasi_Display_Penjualan") = "T" Then
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

        BtnBarangMasuk_Cari_Click(Me, Nothing)

    End Sub

    ''Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs)
    ''    If Cb_TransaksiHrIni.Checked = True Then
    ''        Cb_ParamBarang.Checked = False : Cb_ParamTgl.Checked = False : Cb_ParamLain.Checked = False
    ''        BtnBarangMasuk_Cari_Click(Cb_TransaksiHrIni, e)
    ''    End If
    ''End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_PR.SelectedIndexChanged
        Try
            OpenConn()
            Lv_PRDetail.Items.Clear()
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
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Dim lvw As ListViewItem
            '        lvw = Lv_PRDetail.Items.Add(Dr("kode_barang"))
            '        lvw.SubItems.Add(Dr("nama"))
            '        lvw.SubItems.Add(Dr("satuan"))
            '        lvw.SubItems.Add(Format(Dr("jumlah"), "N0"))
            '        lvw.SubItems.Add(Format(Dr("jumlah_masuk"), "N0"))
            '        lvw.SubItems.Add(Format(Dr("sisa"), "N0"))
            '        lvw.SubItems.Add(Format(Dr("percentComplete"), "N1"))
            '        'lvw.SubItems.Add(Format(Dr("tgl_produksi"), "dd MMM yyyy"))
            '        'lvw.SubItems.Add(Format(Dr("tgl_expired"), "dd MMM yyyy"))
            '    Loop
            'End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub BtnBarangMasuk_Cari_Click(sender As Object, e As EventArgs) Handles BtnBarangMasuk_Cari.Click
        If Cb_ParamTgl.Checked = False And Cb_ParamLain.Checked = False And Cb_ParamBarang.Checked = False Then
            MessageBox.Show("Pilih terlebih dahulu parameter pencarian data!", Judul)
            Cb_ParamBarang.Focus() : Exit Sub
        End If
        If Cb_ParamBarang.Checked = True Then
            'If Cmb_KategoriKecil.Text.Trim.Length = 0 Then
            '    MessageBox.Show("Kode kategori kecil harus diisi . . ! !", "Perhatian")
            '    Cmb_KategoriKecil.Focus() : Exit Sub
            'ElseIf Cmb_KategoriBesar.Text.Trim.Length = 0 Then
            '    MessageBox.Show("Kode kategori besar harus diisi . . ! !", "Perhatian")
            '    Cmb_KategoriBesar.Focus() : Exit Sub
            'ElseIf Txt_KdBrg.Text.Trim.Length = 0 Then
            '    MessageBox.Show("Kode Barang harus diisi . . ! !", "Perhatian")
            '    Txt_KdBrg.Focus() : Exit Sub
            'End If
        ElseIf Cb_ParamTgl.Checked = True Then
            If Cmb_Bulan.Text.Trim.Length = 0 Then
                MessageBox.Show("Bulan harus diisi . . ! !", "Perhatian")
                Cmb_Bulan.Focus() : Exit Sub
            ElseIf Cmb_Tahun.Text.Trim.Length = 0 Then
                MessageBox.Show("Tahun harus diisi . . ! !", "Perhatian")
                Cmb_Tahun.Focus() : Exit Sub
            End If
        ElseIf Cb_ParamLain.Checked = True Then
            If Cmb_ParamLain.Text.Trim.Length = 0 Then
                MessageBox.Show("Parameter harus diisi . . ! !", "Perhatian")
                Cmb_ParamLain.Focus() : Exit Sub
            ElseIf Txt_ParamLain.Text.Trim.Length = 0 Then
                MessageBox.Show("Value harus diisi . . ! !", "Perhatian")
                Txt_ParamLain.Focus() : Exit Sub
            End If
        End If

        Try

            OpenConn()
            '
            ''If Cb_ParamBarang.Checked = False And Cb_TransaksiHrIni.Checked = False And Cb_ParamTgl.Checked = False And Cb_ParamLain.Checked = False Then
            ''    SQL = "select c.No_Faktur, b.Kode_Barang, d.Nama, b.satuan, b.bulan, b.tahun, c.Urut, c.Jumlah_Lama_Sales, c.Jumlah_Lama_PPIC, c.Jenis, c.tanggal, c.Jam ,c.UserID "
            ''    SQL = SQL & "from EMI_Transaksi_Sales_Forecasting a, EMI_Transaksi_Sales_Forecasting_Detail b, EMI_Transaksi_Sales_Forecasting_Log c, barang d "
            ''    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            ''    SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.No_Faktur = c.No_Faktur "
            ''    SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.urut = c.Urut_Detail and b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
            ''    SQL = SQL & "and a.Status is null "
            ''    Lv_PR.Items.Clear()
            ''    Dim Lv As ListViewItem
            ''    Using Ds = BindingTrans(SQL)
            ''        With Ds.Tables("MyTable")
            ''            If .Rows.Count <> 0 Then
            ''                For i As Integer = 0 To .Rows.Count - 1
            ''                    Lv = Lv_PR.Items.Add(.Rows(i).Item("No_Faktur")) '0
            ''                    Lv.SubItems.Add(.Rows(i).Item("Kode_Barang")) '1
            ''                    Lv.SubItems.Add(.Rows(i).Item("Nama")) '2
            ''                    Lv.SubItems.Add(.Rows(i).Item("satuan")) '3
            ''                    Lv.SubItems.Add(.Rows(i).Item("bulan")) '4
            ''                    Lv.SubItems.Add(.Rows(i).Item("tahun")) '5
            ''                    Lv.SubItems.Add(.Rows(i).Item("Urut")) '6
            ''                    Lv.SubItems.Add(.Rows(i).Item("Jumlah_Lama_Sales")) '7
            ''                    Lv.SubItems.Add(.Rows(i).Item("Jumlah_Lama_PPIC")) '8
            ''                    Lv.SubItems.Add(.Rows(i).Item("Jenis")) '9
            ''                    Lv.SubItems.Add(Format(.Rows(i).Item("tanggal"), "dd MMM yyyy")) '10
            ''                    Lv.SubItems.Add(.Rows(i).Item("Jam")) '11
            ''                    Lv.SubItems.Add(.Rows(i).Item("UserID")) '12
            ''                Next
            ''            End If
            ''        End With
            ''    End Using
            ''End If

            '---
            SQL = " ;with cte as ( "
            SQL = SQL & "select d.Kode_Kategori_Besar, d.Kode_Kategori_Kecil, c.kode_Perusahaan, c.No_Faktur, b.Kode_Barang, d.Nama, b.satuan, b.bulan, b.tahun, c.Urut, c.Jumlah_Lama_Sales as Nilai_Sales, "
            SQL = SQL & "c.Jumlah_Lama_PPIC as Nilai_PPIC, c.Jenis, cast(format(c.tanggal,'dd MMM yyyy') as Varchar(20)) as Tanggal, c.Jam ,c.UserID from EMI_Transaksi_Sales_Forecasting a, "
            SQL = SQL & "EMI_Transaksi_Sales_Forecasting_Detail b, EMI_Transaksi_Sales_Forecasting_Log c, barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = b.No_Faktur  "
            SQL = SQL & "and a.No_Faktur = c.No_Faktur and b.No_Faktur = c.No_Faktur and b.urut = c.Urut_Detail and "
            SQL = SQL & "b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang and a.Status is null "
            SQL = SQL & "union all "
            SQL = SQL & "select d.Kode_Kategori_Besar, d.Kode_Kategori_Kecil, b.kode_Perusahaan, b.No_Faktur, b.Kode_Barang, d.Nama, b.satuan, b.bulan, b.tahun, 999999999 as urut, b.Nilai_Sales , "
            SQL = SQL & "b.Nilai_PPIC, 'LATEST' as Jenis, '-' as tanggal, '-' as jam ,'-' as UserID from EMI_Transaksi_Sales_Forecasting a, "
            SQL = SQL & "EMI_Transaksi_Sales_Forecasting_Detail b,  barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and "
            SQL = SQL & "b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang and a.Status is null "
            SQL = SQL & ")select * from cte where Kode_Perusahaan = '" & KodePerusahaan & "' "
            '--- Param Barang
            If Cb_ParamBarang.Checked Then

                If Txt_KdBrg.Text = "---SELURUH---" Then
                    SQL = SQL & ""
                Else
                    SQL = SQL & "and " & arrCmbParamBarang.Item(cmbParamBarang.SelectedIndex) & " like '%" & Txt_KdBrg.Text & "%' "
                End If
                If Cmb_KategoriBesar.SelectedIndex = 0 Then
                    SQL = SQL & ""
                Else
                    SQL = SQL & "and Kode_Kategori_Besar = '" & Cmb_KategoriBesar.Text & "' "
                End If

                If Cmb_KategoriKecil.SelectedIndex = 0 Then
                    SQL = SQL & ""
                Else
                    SQL = SQL & "and Kode_Kategori_Kecil = '" & Cmb_KategoriKecil.Text & "' "
                End If
            End If

            '--- Param Bulan Tahun
            If Cb_ParamTgl.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & "bulan = '" & arrBulanMM.Item(Cmb_Bulan.SelectedIndex) & "' and "
                SQL = SQL & "tahun = '" & Cmb_Tahun.SelectedItem.ToString & "' "
            End If
            '--- Param Lain
            If Cb_ParamLain.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & Arr2.Item(Cmb_ParamLain.SelectedIndex) & " like '%" & Trim(Txt_ParamLain.Text) & "%' "
            End If
            SQL = SQL & "order by tahun, bulan,nama,urut "

            Lv_PR.Items.Clear()
            Dim Lvw As ListViewItem
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Lvw = Lv_PR.Items.Add(.Rows(i).Item("No_Faktur")) '0
                            Lvw.SubItems.Add(.Rows(i).Item("Kode_Barang")) '1
                            Lvw.SubItems.Add(.Rows(i).Item("Nama")) '2
                            Lvw.SubItems.Add(.Rows(i).Item("satuan")) '3
                            Dim bulanNumeric As String = .Rows(i).Item("bulan").ToString()
                            Dim bulanNama As String = ""
                            Select Case bulanNumeric
                                Case "01"
                                    bulanNama = "January"
                                Case "02"
                                    bulanNama = "February"
                                Case "03"
                                    bulanNama = "March"
                                Case "04"
                                    bulanNama = "April"
                                Case "05"
                                    bulanNama = "May"
                                Case "06"
                                    bulanNama = "June"
                                Case "07"
                                    bulanNama = "July"
                                Case "08"
                                    bulanNama = "August"
                                Case "09"
                                    bulanNama = "September"
                                Case "10"
                                    bulanNama = "October"
                                Case "11"
                                    bulanNama = "November"
                                Case "12"
                                    bulanNama = "December"
                                Case Else
                                    bulanNama = "Invalid Month"
                            End Select
                            Lvw.SubItems.Add(bulanNama) '4
                            Lvw.SubItems.Add(.Rows(i).Item("tahun")) '5
                            Lvw.SubItems.Add(.Rows(i).Item("Urut")) '6
                            Lvw.SubItems.Add(.Rows(i).Item("Nilai_Sales")) '7
                            Lvw.SubItems.Add(.Rows(i).Item("Nilai_PPIC")) '8
                            Lvw.SubItems.Add(.Rows(i).Item("Jenis")) '9
                            Lvw.SubItems.Add(.Rows(i).Item("tanggal")) '10
                            Lvw.SubItems.Add(.Rows(i).Item("Jam")) '11
                            Lvw.SubItems.Add(.Rows(i).Item("UserID")) '12
                        Next
                    End If
                End With
            End Using

            'Param Barang
            '''If Cb_ParamBarang.Checked = True Then
            '''    SQL = "select c.No_Faktur, b.Kode_Barang, d.Nama, b.satuan, b.bulan, b.tahun, c.Urut, c.Jumlah_Lama_Sales, c.Jumlah_Lama_PPIC, c.Jenis, c.tanggal, c.Jam ,c.UserID "
            '''    SQL = SQL & "from EMI_Transaksi_Sales_Forecasting a, EMI_Transaksi_Sales_Forecasting_Detail b, EMI_Transaksi_Sales_Forecasting_Log c, barang d "
            '''    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            '''    SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.No_Faktur = c.No_Faktur "
            '''    SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.urut = c.Urut_Detail and b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
            '''    SQL = SQL & "and a.Status is null "
            '''End If

            ''''If Cb_ParamTgl.Checked Then
            ''''    'Pasang And
            ''''    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "and "
            ''''    SQL = SQL & Arr1.Item(ComboBox3.SelectedIndex) & " between '"
            ''''    SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            ''''End If

            '''If ComboBox6.SelectedIndex = 0 Then
            '''    SQL = SQL & " and Lokasi in("
            '''    Dim list_kota As String = ""
            '''    For x As Integer = 1 To ComboBox6.Items.Count - 1
            '''        list_kota = list_kota & "'" & ComboBox6.Items(x).ToString & "', "
            '''    Next

            '''    list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

            '''    SQL = SQL & list_kota & ")"
            '''Else
            '''    SQL = SQL & " and Lokasi = '" & ComboBox6.Text & "' "
            '''End If

            '''SQL = SQL & "order by tanggal , jam"


            'Dim Lvw As ListViewItem

            'Using Ds = BindingTrans(SQL)
            '    With Ds.Tables("MyTable")
            '        If .Rows.Count <> 0 Then
            '            For i As Integer = 0 To .Rows.Count - 1
            '                Lvw = Lv_PR.Items.Add(.Rows(i).Item("no_faktur"))
            '                'Lvw.SubItems.Add(.Rows(i).Item("kode_supplier"))
            '                'Lvw.SubItems.Add(.Rows(i).Item("nama"))
            '                Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal"), "dd MMM yyyy"))
            '                If General_Class.CekNULL(.Rows(i).Item("tanggal_release")) = "" Then
            '                    Lvw.SubItems.Add("-")
            '                Else
            '                    Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_release"), "dd MMM yyyy"))
            '                End If
            '                Lvw.SubItems.Add(.Rows(i).Item("keterangan"))
            '                Lvw.SubItems.Add("-")
            '                Lvw.SubItems.Add(.Rows(i).Item("userid"))
            '                ''Lvw = Lv_PR.Items.Add(.Rows(i).Item("no_faktur"))
            '                ''Lvw.SubItems.Add(.Rows(i).Item("no_nota"))
            '                ''Lvw.SubItems.Add(.Rows(i).Item("kode_supplier"))
            '                ''Lvw.SubItems.Add(.Rows(i).Item("nama"))

            '                ''If General_Class.CekNULL(.Rows(i).Item("jenis_pembayaran")) = "N" Then
            '                ''    Lvw.SubItems.Add("Non Tunai")
            '                ''Else
            '                ''    Lvw.SubItems.Add("Tunai")
            '                ''End If
            '                ''Lvw.SubItems.Add(.Rows(i).Item("cara_bayar"))

            '                ''If .Rows(i).Item("jenis_pembayaran") = "N" Then
            '                ''    Lvw.SubItems.Add(Format(.Rows(i).Item("Tgl_Jatuh_Tempo"), "dd MMM yyyy"))
            '                ''Else
            '                ''    Lvw.SubItems.Add("-")
            '                ''End If

            '                ''Lvw.SubItems.Add(.Rows(i).Item("mata_uang"))
            '                ''Lvw.SubItems.Add(Format(.Rows(i).Item("total_mua"), "N2"))
            '                ''Lvw.SubItems.Add(Format(.Rows(i).Item("total_idr"), "N2"))
            '                ''Lvw.SubItems.Add(Format(.Rows(i).Item("ppn"), "N2"))
            '                ''Lvw.SubItems.Add(Format(.Rows(i).Item("grand"), "N2"))
            '                ''Lvw.SubItems.Add(Format(.Rows(i).Item("etd_simulasi"), "dd MMM yyyy"))


            '                ''Lv_PR.Items(i).ForeColor = T

            '                ''If General_Class.CekNULL(.Rows(i).Item("flag_release")) <> "Y" Then
            '                ''    Lv_PR.Items(i).ForeColor = Batal
            '                ''End If
            '            Next
            '        End If
            '    End With
            'End Using



            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    'Private Sub Txt_KdBrg_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBrg.TextChanged
    '    If Txt_KdBrg.Text.Trim.Length = 0 Then
    '        Lv_DataBarang.Visible = False : Exit Sub
    '    Else
    '        Lv_DataBarang.Visible = True
    '    End If

    '    If Cmb_KategoriBesar.SelectedIndex = -1 Then
    '        'MessageBox.Show("Kode kategori harus diisi . . ! !", "Perhatian")
    '        Txt_KdBrg.Text = "" : Txt_NmBrg.Text = "" : Cmb_KategoriBesar.Focus() : Lv_DataBarang.Visible = False : Exit Sub
    '    End If

    '    Lv_DataBarang.Items.Clear()
    '    Dim lv As New ListViewItem

    '    ''Try
    '    ''    OpenConn()

    '    ''    lv = Lv_DataBarang.Items.Add("---SELURUH---")
    '    ''    lv.SubItems.Add("---SELURUH---")
    '    ''    SQL = "select Kode_Barang,Nama from barang where "
    '    ''    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
    '    ''    'SQL = SQL & "Kode_Barang like '" & Txt_KdBrg.Text & "%' "
    '    ''    SQL = SQL & "Nama like '" & Txt_KdBrg.Text & "%' "
    '    ''    SQL = SQL & "group by Kode_Barang, Nama "
    '    ''    Using Dr = OpenTrans(SQL)
    '    ''        Do While Dr.Read
    '    ''            lv = Lv_DataBarang.Items.Add(Dr("Kode_Barang"))
    '    ''            lv.SubItems.Add(Dr("Nama"))
    '    ''        Loop
    '    ''    End Using

    '    ''    CloseConn()
    '    ''Catch ex As Exception
    '    ''    CloseConn()
    '    ''    MessageBox.Show(ex.Message)
    '    ''    Exit Sub
    '    ''End Try

    '    Try
    '        OpenConn()

    '        lv = Lv_DataBarang.Items.Add("---SELURUH---")
    '        lv.SubItems.Add("---SELURUH---")
    '        SQL = "select a.Kode_Barang,a.Nama from Barang a,EMI_Group_Jenis b where "
    '        SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
    '        SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis "
    '        SQL = SQL & "and b.Flag_Finished_Good = 'Y' and a.Nama like '" & Txt_KdBrg.Text & "%' "
    '        If Cmb_KategoriBesar.SelectedIndex = 0 Then
    '            SQL = SQL & " "
    '        Else
    '            SQL = SQL & "and a.Kode_Kategori_Besar = '" & Cmb_KategoriBesar.Text & "'"
    '        End If
    '        If Cmb_KategoriKecil.SelectedIndex = 0 Then
    '            SQL = SQL & ""
    '        Else
    '            SQL = SQL & "and a.Kode_Kategori_Kecil = '" & Cmb_KategoriKecil.Text & "'"
    '        End If
    '        SQL = SQL & "group by a.Kode_Barang,a.Nama order by nama "
    '        Using Dr = OpenTrans(SQL)
    '            Do While Dr.Read
    '                lv = Lv_DataBarang.Items.Add(Dr("Kode_Barang"))
    '                lv.SubItems.Add(Dr("Nama"))
    '            Loop
    '        End Using

    '        CloseConn()
    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    'End Sub

    Private Sub Lv_DataBarang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_DataBarang.SelectedIndexChanged

    End Sub

    Private Sub DisplayRakToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If Lv_PR.Items.Count = 0 Or Lv_PR.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        EMI_Barang_Masuk_Display_Rak.TxtNoBM.Text = Lv_PR.FocusedItem.Text
        EMI_Barang_Masuk_Display_Rak.ShowDialog()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_ParamTgl.CheckedChanged
        If Cb_ParamTgl.Checked Then


            Cb_ParamBarang.Checked = False
            Cb_ParamLain.Checked = False

            Cmb_Tahun.Enabled = True
            Cmb_Bulan.Enabled = True
            Cmb_Tahun.SelectedIndex = -1
            Cmb_Bulan.SelectedIndex = -1


        Else
            ''''ComboBox3.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            ''''ComboBox3.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
            '''Cmb_Bulan.SelectedIndex = -1 : Cmb_Tahun.SelectedIndex = -1
            '''Cmb_Bulan.Enabled = False : Cmb_Tahun.Enabled = False
            Cmb_Tahun.Enabled = False
            Cmb_Bulan.Enabled = False
            Cmb_Tahun.SelectedIndex = -1
            Cmb_Bulan.SelectedIndex = -1
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
        'BtnBarangMasuk_Cari_Click("", e)
    End Sub

    Private Sub Cmb_ParamLain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_ParamLain.SelectedIndexChanged

    End Sub

    Private Sub Txt_KdBrg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBrg.KeyPress
        If e.KeyChar = Chr(13) Then
            BtnBarangMasuk_Cari_Click(Me, Nothing)
        End If
    End Sub

    Private Sub Cb_ParamBarang_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_ParamBarang.CheckedChanged

        If Cb_ParamBarang.Checked Then

            Txt_KdBrg.Enabled = True : Txt_KdBrg.Text = ""

            cmbParamBarang.Enabled = True : cmbParamBarang.SelectedIndex = 0
            Cmb_KategoriBesar.Enabled = True : Cmb_KategoriBesar.SelectedIndex = 0
            Cmb_KategoriKecil.Enabled = True

            Cb_ParamTgl.Checked = False
            Cb_ParamLain.Checked = False



            Cmb_KategoriBesar.SelectedIndex = 0
        Else

            Txt_KdBrg.Enabled = False : Txt_KdBrg.Text = ""

            Cmb_KategoriBesar.Enabled = False : Cmb_KategoriBesar.SelectedIndex = -1
            Cmb_KategoriKecil.Enabled = False : Cmb_KategoriKecil.SelectedIndex = -1

            Cmb_ParamLain.Enabled = False : cmbParamBarang.SelectedIndex = -1
        End If

    End Sub


    Private Sub Cmb_KategoriBesar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_KategoriBesar.SelectedIndexChanged
        If Cmb_KategoriBesar.SelectedIndex = -1 Then
            'MessageBox.Show("Kode kategori besar harus diisi . . ! !", "Perhatian")
            Cmb_KategoriBesar.Focus() : Exit Sub
        End If
        Try
            OpenConn()

            Cmb_KategoriKecil.Items.Clear() : Cmb_KategoriKecil.SelectedIndex = -1
            Cmb_KategoriKecil.Items.Add("---SELURUH---")
            SQL = "select Kode_Kategori_Kecil from Kategori_Kecil where Kode_Perusahaan = "
            SQL = SQL & "'" & KodePerusahaan & "' "
            If Cmb_KategoriBesar.SelectedIndex = 0 Then
                SQL = SQL & " "
            Else
                SQL = SQL & "and Kode_Kategori_Besar = '" & Cmb_KategoriBesar.Text & "' "
            End If
            SQL = SQL & "order by Kode_Kategori_Kecil"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_KategoriKecil.Items.Add(Dr("Kode_Kategori_Kecil"))
                Loop
            End Using


            If Cmb_KategoriBesar.SelectedIndex = 0 Then
                Cmb_KategoriKecil.SelectedIndex = 0
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_ParamLain.CheckedChanged
        If Cb_ParamLain.Checked Then

            Cmb_ParamLain.Enabled = True
            Txt_ParamLain.Enabled = True

            Cb_ParamBarang.Checked = False
            Cb_ParamTgl.Checked = False
        Else

            Cmb_ParamLain.Enabled = False
            Txt_ParamLain.Enabled = False
            Cmb_ParamLain.SelectedIndex = -1
            Txt_ParamLain.Text = ""
        End If
    End Sub

    Private Sub Lv_DataBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_DataBarang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_DataBarang_DoubleClick(Lv_DataBarang, e)
        End If
    End Sub

    Private Sub Lv_DataBarang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DataBarang.DoubleClick
        If Lv_DataBarang.Items.Count = 0 Then Exit Sub
        Dim kode As String = Lv_DataBarang.FocusedItem.Text
        Dim nama As String = Lv_DataBarang.FocusedItem.SubItems(1).Text
        Txt_KdBrg.Text = kode

        Lv_DataBarang.Visible = False
    End Sub

End Class