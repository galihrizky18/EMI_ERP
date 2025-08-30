Public Class EMI_Penawaran_Harga_Summary_Data

    Dim JudulForm As String = "Display Penawaran"

    Dim Arr1, Arr2, Arr3, Arr4, arrSubmited, arrAktif As New ArrayList
    Dim pertama As Integer = 1
    Dim T As Color = Color.Blue
    Dim KT As Color = Color.Red
    Dim KY As Color = Color.Green
    Dim Batal As Color = Color.Black

    Dim ExpPenwaran As Integer = 10

    Dim LvNoFaktur As String
    Dim LvNoPenawaran As String
    Dim LvPrAwal As String
    Dim LvPrAkhir As String
    Dim LvKdSupp As String
    Dim LvNoUrut As String
    Dim LvNmSupp As String
    Dim LvAktif As String
    Dim LvSisaHari As String
    Dim LvStatus As String
    Dim LvLokasi As String

    Dim CellNoFaktur As Integer = 0
    Dim CellNoPenawaran As Integer = 1
    Dim CellPrAwal As Integer = 2
    Dim CellPrAkhir As Integer = 3
    Dim CellKdSupp As Integer = 4
    Dim CellNoUrut As Integer = 5
    Dim CellNmSupp As Integer = 6
    Dim CellAktif As Integer = 7
    Dim CellSisaHari As Integer = 8
    Dim CellStatus As Integer = 9
    Dim CellLokasi As Integer = 10

    Private Sub Display_Pembelian_Barang_Masuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong()
    End Sub



    Private Sub get_det_barang(ByVal index As Integer)
        LvNoFaktur = LvPenawaranHarga.Items(index).SubItems(CellNoFaktur).Text
        LvNoPenawaran = LvPenawaranHarga.Items(index).SubItems(CellNoPenawaran).Text
        LvPrAwal = LvPenawaranHarga.Items(index).SubItems(CellPrAwal).Text
        LvPrAkhir = LvPenawaranHarga.Items(index).SubItems(CellPrAkhir).Text
        LvKdSupp = LvPenawaranHarga.Items(index).SubItems(CellKdSupp).Text
        LvNoUrut = LvPenawaranHarga.Items(index).SubItems(CellNoUrut).Text
        LvNmSupp = LvPenawaranHarga.Items(index).SubItems(CellNmSupp).Text
        LvAktif = LvPenawaranHarga.Items(index).SubItems(CellAktif).Text
        LvSisaHari = LvPenawaranHarga.Items(index).SubItems(CellSisaHari).Text
        LvStatus = LvPenawaranHarga.Items(index).SubItems(CellStatus).Text
        LvLokasi = LvPenawaranHarga.Items(index).SubItems(CellLokasi).Text

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

        LvPenawaranHarga.Columns.Clear() : LvPenawaranHarga.Items.Clear()
        LvPenawaranHarga.Columns.Add(Base_Language.Lang_Global_NoFaktur, 120, HorizontalAlignment.Left)
        LvPenawaranHarga.Columns.Add(Base_Language.Lang_Penawaran_NoPenawaran, 190, HorizontalAlignment.Left)
        LvPenawaranHarga.Columns.Add(Base_Language.Lang_global_Periode_Awal, 130, HorizontalAlignment.Center)
        LvPenawaranHarga.Columns.Add(Base_Language.Lang_global_Periode_Akhir, 130, HorizontalAlignment.Center)
        LvPenawaranHarga.Columns.Add(Base_Language.Lang_Global_Supplier, 0, HorizontalAlignment.Left)
        LvPenawaranHarga.Columns.Add("NoUrut", 0, HorizontalAlignment.Right)
        LvPenawaranHarga.Columns.Add(Base_Language.lang_global_Nama_Supplier, 220, HorizontalAlignment.Left).DisplayIndex = 1
        LvPenawaranHarga.Columns.Add(Base_Language.Lang_Global_Aktif, 100, HorizontalAlignment.Center)
        LvPenawaranHarga.Columns.Add("sisa hari", 120, HorizontalAlignment.Center)
        LvPenawaranHarga.Columns.Add("Status", 100, HorizontalAlignment.Center)
        LvPenawaranHarga.Columns.Add("Lokasi", 0, HorizontalAlignment.Center)
        LvPenawaranHarga.View = View.Details

        LvPenawaranHarga_Detail.Columns.Clear() : LvPenawaranHarga_Detail.Items.Clear()
        LvPenawaranHarga_Detail.Columns.Add(Base_Language.Lang_Global_KodeBarang, 140, HorizontalAlignment.Left)
        LvPenawaranHarga_Detail.Columns.Add(Base_Language.Lang_Global_Nama, 350, HorizontalAlignment.Left)
        LvPenawaranHarga_Detail.Columns.Add("Kategori Barang", 150, HorizontalAlignment.Center)
        LvPenawaranHarga_Detail.Columns.Add(Base_Language.Lang_Global_MinOrder, 110, HorizontalAlignment.Center)
        LvPenawaranHarga_Detail.Columns.Add(Base_Language.Lang_Global_Satuan, 110, HorizontalAlignment.Center)
        LvPenawaranHarga_Detail.Columns.Add(Base_Language.Lang_Global_HargaSatuan, 180, HorizontalAlignment.Right)
        LvPenawaranHarga_Detail.Columns.Add("Mata Uang", 90, HorizontalAlignment.Center)
        LvPenawaranHarga_Detail.View = View.Details


        Try
            OpenConn()

            ComboBox6.Items.Clear()
            ComboBox6.Items.Add(Base_Language.Lang_Global_SeluruhCombobox)

            'xSplit = CekKotaRole().Split(", ")

            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
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
            ComboBox3.Items.Add("Periode Awal") : Arr1.Add("a.Tgl_Penawaran_Hrg")
            ComboBox3.Items.Add("Periode Akhir") : Arr1.Add("a.Periode_Akhir_Penawaran")

            CmbSubmited.Items.Clear() : arrSubmited.Clear()
            CmbSubmited.Items.Add("--Seluruh--")
            CmbSubmited.Items.Add("Submited")
            CmbSubmited.Items.Add("Unsbumited")
            CmbSubmited.SelectedIndex = 0

            cmb_aktif.Items.Clear() : arrAktif.Clear()
            cmb_aktif.Items.Add("--Seluruh--") : arrAktif.Add("seluruh")
            cmb_aktif.Items.Add("Aktif") : arrAktif.Add("Y")
            cmb_aktif.Items.Add("Belum AKtif") : arrAktif.Add("Y")
            cmb_aktif.Items.Add("Tidak Aktif") : arrAktif.Add("T")

            cmb_aktif.SelectedIndex = 0

            'TextBoxa.Text = "0" 
            ComboBox3.Enabled = False : ComboBox2.Enabled = False
            DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            TextBox4.Enabled = False

            ComboBox2.Items.Clear() : ComboBox2.Text = "" : Arr2.Clear()
            ComboBox2.Items.Add("No Faktur") : Arr2.Add("a.no_faktur")
            ComboBox2.Items.Add("No Penawaran") : Arr2.Add("a.no_penawaran")
            ' ComboBox2.Items.Add("Kode Supplier") : Arr2.Add("a.kode_supplier")
            ComboBox2.Items.Add("Nama Supplier") : Arr2.Add("nama")
            'ComboBox2.Items.Add("NO Nota") : Arr2.Add("a.no_nota")
            'ComboBox2.Items.Add("Kode Supplier") : Arr2.Add("a.kode_supplier")

            Label1.Text = "Summary Data - Penawaran Harga"
            CheckBox3.Text = Base_Language.Lang_Global_Hari_ini
            CheckBox1.Text = Base_Language.Lang_Global_Para_Tbl
            CheckBox2.Text = Base_Language.Lang_Global_Para_lain
            BtnBarangMasuk_Cari.Text = Base_Language.Lang_Global_Cari

#Region "Kode Lama"

            'get_jam()
            'SQL = ";with cte as ( "
            'SQL = SQL & "Select a.kode_Perusahaan,a.tanggal,a.lokasi,a.flag_release, a.no_faktur, a.no_penawaran,Tgl_Penawaran_Hrg,Periode_Akhir_Penawaran,a.Kode_Supplier,b.nama, a.noUrut, "
            'SQL = SQL & "(case "
            'SQL = SQL & "when a.Selesai='Y' then 'T' "
            'SQL = SQL & "when '" & Format(tgl_skg, "yyyy-MM-dd") & "' not between a.Tgl_Penawaran_Hrg And a.Periode_Akhir_Penawaran then 'T' "
            'SQL = SQL & "when a.flag_release = 'Y' then 'Y' "
            'SQL = SQL & "else 'S' end "
            'SQL = SQL & ") as aktif, datediff(day, '" & Format(tgl_skg, "yyyy-MM-dd") & "',a.Periode_Akhir_Penawaran) as sisa_hari "
            'SQL = SQL & "From EMI_Master_Penawaran a, suppliers b "
            'SQL = SQL & "Where a.Kode_Perusahaan =b.kode_Perusahaan and a.Kode_supplier=b.kode_supplier "
            'SQL = SQL & ") select * from cte a where Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and aktif = '" & arrAktif.Item(cmb_aktif.SelectedIndex) & "' "


            'If ComboBox6.SelectedIndex = 0 Then
            '    SQL = SQL & " and a.Lokasi in("
            '    Dim list_kota As String = ""
            '    For x As Integer = 1 To ComboBox6.Items.Count - 1
            '        list_kota = list_kota & "'" & ComboBox6.Items(x).ToString & "', "
            '    Next

            '    list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

            '    SQL = SQL & list_kota & ")"
            'Else
            '    SQL = SQL & " and a.Lokasi = '" & ComboBox6.Text & "' "
            'End If

            'SQL = SQL & "order by Tgl_Penawaran_Hrg"

            'Dim Lvw As ListViewItem

            'Using Ds = BindingTrans(SQL)
            '    With Ds.Tables("MyTable")
            '        If .Rows.Count <> 0 Then
            '            For i As Integer = 0 To .Rows.Count - 1

            '                Lvw = LvPenawaranHarga.Items.Add(.Rows(i).Item("no_faktur"))
            '                Lvw.SubItems.Add(.Rows(i).Item("no_penawaran"))
            '                Lvw.SubItems.Add(Format(.Rows(i).Item("Tgl_Penawaran_Hrg"), "dd MMM yyyy"))
            '                Lvw.SubItems.Add(Format(.Rows(i).Item("Periode_Akhir_Penawaran"), "dd MMM yyyy"))
            '                Lvw.SubItems.Add(.Rows(i).Item("kode_supplier"))
            '                Lvw.SubItems.Add(.Rows(i).Item("NoUrut"))
            '                Lvw.SubItems.Add(.Rows(i).Item("nama"))

            '                If .Rows(i).Item("aktif") = "Y" Then
            '                    Lvw.SubItems.Add("Aktif")
            '                    Lvw.BackColor = Color.LightGreen
            '                ElseIf .Rows(i).Item("aktif") = "T" Then
            '                    Lvw.SubItems.Add("Tidak Aktif")
            '                    Lvw.BackColor = Color.FromArgb(231, 64, 50)
            '                ElseIf .Rows(i).Item("aktif") = "S" Then
            '                    Lvw.SubItems.Add("Belum Aktif")
            '                    Lvw.BackColor = Color.LightBlue
            '                End If

            '                If General_Class.CekNULL(.Rows(i).Item("aktif")) = "Y" Then
            '                    Lvw.SubItems.Add(.Rows(i).Item("sisa_hari") & " hari")

            '                    If .Rows(i).Item("sisa_hari") < ExpPenwaran Then
            '                        Lvw.BackColor = Color.LightYellow
            '                    End If

            '                Else
            '                    Lvw.SubItems.Add("-")
            '                End If
            '                If General_Class.CekNULL(.Rows(i).Item("Flag_release")) = "Y" Then
            '                    Lvw.SubItems.Add("SUBMITTED")
            '                Else
            '                    Lvw.SubItems.Add("UNSUBMITTED")
            '                End If
            '                Lvw.SubItems.Add(.Rows(i).Item("lokasi"))


            '            Next
            '        End If
            '    End With
            'End Using
#End Region

            cari()

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

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LvPenawaranHarga.SelectedIndexChanged
        Try
            OpenConn()
            LvPenawaranHarga_Detail.Items.Clear()


            Dim gudangDefault As String = ""

            SQL = "SELECT Kode_Stock_Owner_Gudang From Binding_Lokasi_Gudang "
            SQL = SQL & "Where Kode_stock_owner = '" & LvPenawaranHarga.FocusedItem.SubItems(CellLokasi).Text & "' and "
            SQL = SQL & "Gudang_Default = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    gudangDefault = Dr("Kode_Stock_Owner_Gudang")
                End If
            End Using


            SQL = "Select a.Kode_Barang, b.Nama, a.Min_Order, a.Satuan, a.Harga_Satuan, c.Kode_Group_Jenis, a.Mata_Uang "
            SQL = SQL & "From EMI_Master_Penawaran_Detail a, Barang b, EMI_Group_Jenis c "
            SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Perusahaan = b.Kode_Perusahaan and  b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang and b.Kode_Stock_Owner = '" & gudangDefault & "' "
            SQL = SQL & "and b.id_group_Jenis = c.Id_Group_Jenis and c.Flag_Penawaran = 'Y' "
            SQL = SQL & "and a.No_Faktur = '" & LvPenawaranHarga.FocusedItem.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    Dim lvw As ListViewItem
                    For i As Integer = 0 To .Rows.Count - 1

                        lvw = LvPenawaranHarga_Detail.Items.Add(.Rows(i).Item("Kode_Barang"))
                        lvw.SubItems.Add(.Rows(i).Item("Nama"))
                        lvw.SubItems.Add(.Rows(i).Item("Kode_Group_Jenis"))
                        lvw.SubItems.Add(Format(.Rows(i).Item("Min_Order"), "N0"))
                        lvw.SubItems.Add(.Rows(i).Item("Satuan"))
                        lvw.SubItems.Add(Format(.Rows(i).Item("Harga_Satuan"), "N4"))
                        lvw.SubItems.Add(.Rows(i).Item("Mata_Uang"))
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

    Private Sub BtnBarangMasuk_Cari_Click(sender As Object, e As EventArgs) Handles BtnBarangMasuk_Cari.Click
        cari(True)
    End Sub



    Private Sub cari(ByVal Optional filter As Boolean = False)
        If filter Then

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
                    If TextBox4.Text.Trim.Length = 0 Then
                        MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain2, Judul)
                        TextBox4.Focus() : Exit Sub
                    End If
                End If
            End If
        End If

        Try

            OpenConn()

            LvPenawaranHarga.Items.Clear()
            LvPenawaranHarga_Detail.Items.Clear()

            get_jam()
            SQL = ";with cte as ( "
            SQL = SQL & "Select a.kode_Perusahaan,a.tanggal,a.lokasi,a.flag_release, a.no_faktur, a.no_penawaran,Tgl_Penawaran_Hrg,Periode_Akhir_Penawaran,a.Kode_Supplier,b.nama, a.noUrut,  a.status, "
            SQL = SQL & "(case "
            SQL = SQL & "when a.Selesai='Y' then 'T' "
            SQL = SQL & "when '" & Format(tgl_skg, "yyyy-MM-dd") & "' not between a.Tgl_Penawaran_Hrg And a.Periode_Akhir_Penawaran then 'T' "
            SQL = SQL & "when a.flag_release = 'Y' then 'Y' "
            SQL = SQL & "else 'S' end "
            SQL = SQL & ") as aktif, datediff(day, '" & Format(tgl_skg, "yyyy-MM-dd") & "',a.Periode_Akhir_Penawaran) as sisa_hari "
            SQL = SQL & "From EMI_Master_Penawaran a, suppliers b "
            SQL = SQL & "Where a.Kode_Perusahaan =b.kode_Perusahaan and a.Kode_supplier = b.kode_supplier "
            SQL = SQL & ") select * from cte a where Kode_Perusahaan = '" & KodePerusahaan & "' "

            If Not filter Then
                If Not cmb_aktif.SelectedIndex = 0 Then
                    SQL = SQL & "and aktif = '" & arrAktif.Item(cmb_aktif.SelectedIndex) & "' "
                End If
            End If

            If CmbSubmited.SelectedIndex = 0 Then
                SQL = SQL & " "
            ElseIf CmbSubmited.SelectedIndex = 1 Then
                SQL = SQL & "and flag_release = 'Y' "
            ElseIf CmbSubmited.SelectedIndex = 2 Then
                SQL = SQL & "and flag_release is null "
            End If

            If cmb_aktif.SelectedIndex = 0 Then
                SQL = SQL & " "
            ElseIf cmb_aktif.SelectedIndex = 1 Then
                SQL = SQL & "and aktif = 'Y' "
            ElseIf cmb_aktif.SelectedIndex = 2 Then
                SQL = SQL & "and aktif = 'S' "
            ElseIf cmb_aktif.SelectedIndex = 3 Then
                SQL = SQL & "and aktif = 'T' "
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

            SQL = SQL & "order by Tgl_Penawaran_Hrg, aktif DESC"

            Dim Lvw As ListViewItem

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Lvw = LvPenawaranHarga.Items.Add(.Rows(i).Item("no_faktur"))
                            Lvw.SubItems.Add(.Rows(i).Item("no_penawaran"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Tgl_Penawaran_Hrg"), "dd MMM yyyy"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Periode_Akhir_Penawaran"), "dd MMM yyyy"))
                            Lvw.SubItems.Add(.Rows(i).Item("kode_supplier"))
                            Lvw.SubItems.Add(.Rows(i).Item("NoUrut"))
                            Lvw.SubItems.Add(.Rows(i).Item("nama"))

                            If .Rows(i).Item("aktif") = "Y" Then
                                Lvw.SubItems.Add("Aktif")
                                Lvw.BackColor = Color.LightGreen
                                Lvw.ForeColor = Color.Black
                            ElseIf .Rows(i).Item("aktif") = "T" Then
                                Lvw.SubItems.Add("Tidak Aktif")
                                Lvw.BackColor = Color.LightGray
                                Lvw.ForeColor = Color.Black
                            ElseIf .Rows(i).Item("aktif") = "S" Then
                                Lvw.SubItems.Add("Belum Aktif")
                                Lvw.BackColor = Color.LightYellow
                                Lvw.ForeColor = Color.Black
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("aktif")) = "Y" Then
                                Lvw.SubItems.Add(.Rows(i).Item("sisa_hari") & " hari")
                                If .Rows(i).Item("sisa_hari") < ExpPenwaran Then
                                    Lvw.BackColor = Color.LightYellow
                                    Lvw.ForeColor = Color.Black
                                End If
                            Else
                                Lvw.SubItems.Add("-")
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("Flag_release")) = "Y" Then
                                Lvw.SubItems.Add("SUBMITTED")
                            Else
                                Lvw.SubItems.Add("UNSUBMITTED")
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("status")) = "Y" Then
                                Lvw.BackColor = Color.DarkRed
                                Lvw.ForeColor = Color.White
                            End If

                            Lvw.SubItems.Add(.Rows(i).Item("lokasi"))

                            'Lv_PO.Columns.Add(Base_Language.Lang_Global_NoFaktur, 120, HorizontalAlignment.Left)
                            'Lv_PO.Columns.Add(Base_Language.Lang_Penawaran_NoPenawaran, 190, HorizontalAlignment.Left)
                            'Lv_PO.Columns.Add(Base_Language.Lang_global_Periode_Awal, 120, HorizontalAlignment.Center)
                            'Lv_PO.Columns.Add(Base_Language.Lang_global_Periode_Akhir, 120, HorizontalAlignment.Center)
                            'Lv_PO.Columns.Add(Base_Language.Lang_Global_Supplier, 0, HorizontalAlignment.Left)
                            'Lv_PO.Columns.Add("NoUrut", 0, HorizontalAlignment.Right)
                            'Lv_PO.Columns.Add(Base_Language.lang_global_Nama_Supplier, 220, HorizontalAlignment.Left).DisplayIndex = 1
                            'Lv_PO.Columns.Add(Base_Language.Lang_Global_Aktif, 80, HorizontalAlignment.Center)
                            'Lv_PO.Columns.Add("sisa hari", 120, HorizontalAlignment.Center)
                            'Lv_PO.Columns.Add("Status", 100, HorizontalAlignment.Center)



                            'Lvw = Lv_PO.Items.Add(.Rows(i).Item("no_faktur"))

                            ''Lvw.SubItems.Add(.Rows(i).Item("no_nota"))
                            'Lvw.SubItems.Add(.Rows(i).Item("kode_supplier"))
                            'Lvw.SubItems.Add(.Rows(i).Item("nama"))



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
                            'Lvw.SubItems.Add(.Rows(i).Item("userid"))
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



    Private Sub Btn_PilihBarang_Click(sender As Object, e As EventArgs) Handles Btn_PilihBarang.Click
        Emi_Display_Barang_Penawaran.dari = "Summary Data"
        Emi_Display_Barang_Penawaran.ShowDialog()
    End Sub

    'Private Sub CetakUlangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangToolStripMenuItem.Click
    'If LvPenawaranHarga.Items.Count = 0 Or LvPenawaranHarga.SelectedItems.Count = 0 Then
    '    Exit Sub
    'End If

    'Try
    '    OpenConn()

    '    SQL = "select a.No_Faktur from EMI_Pembelian_PO a, EMI_Pembelian_PO_Detail b, barang c, suppliers d "
    '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
    '    SQL = SQL & "and a.No_Faktur = b.No_Faktur "
    '    SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
    '    SQL = SQL & "and a.Kode_Supplier = d.Kode_Supplier "
    '    SQL = SQL & "and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
    '    SQL = SQL & "and a.No_Faktur='" & LvPenawaranHarga.FocusedItem.Text & "' "
    '    Using Ds = BindingTrans(SQL)
    '        If Ds.Tables("MyTable").Rows.Count <> 0 Then
    '            With Ds.Tables(0)
    '                Dim CrDoc As New Faktur_Purchase_Order
    '                With A_Place_For_Printing2
    '                    CrDoc.SetDataSource(Ds)
    '                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
    '                    CrDoc.SummaryInfo.ReportTitle = "Laporan Faktur Purchase Order"
    '                    CrDoc.RecordSelectionFormula = " {EMI_Pembelian_PO.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Pembelian_PO.No_Faktur} = '" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "'"

    '                    .Text = "Laporan Faktur Purchase Order"
    '                    .CrystalReportViewer1.ReportSource = CrDoc
    '                    .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
    '                    .Refresh()
    '                    .Show()
    '                End With
    '            End With
    '        Else
    '            MessageBox.Show("Data tidak ditemukan!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        End If

    '    End Using

    '    CloseConn()
    'Catch ex As Exception
    '    CloseConn()
    '    MessageBox.Show(ex.Message)
    '    Exit Sub
    'End Try
    'End Sub

    Private Sub AkhiriPenawranToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AkhiriPenawranToolStripMenuItem.Click
        If LvPenawaranHarga.Items.Count = 0 Or LvPenawaranHarga.SelectedItems.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Error_Validasi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tny As String = MessageBox.Show(Base_Language.Lang_GLOBAL_Tanya_Validasi, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If tny = vbNo Then Exit Sub
        Try
            OpenConn()
            Cmd.Transaction() = Cn.BeginTransaction

            '===========================
            '=     CEK BUTTON ROLE     =
            '===========================
            If CekButtonRole("Akhiri_Penawaran") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Mengakhiri Penawaran", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


            SQL = "SELECT Kode_Perusahaan From EMI_Master_Penawaran "
            SQL = SQL & "Where NoUrut = '" & LvPenawaranHarga.FocusedItem.SubItems(CellNoUrut).Text & "' and "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    Dr.Close()
                    SQL = "Update EMI_Master_Penawaran set selesai = 'Y' "
                    SQL = SQL & "Where NoUrut = '" & LvPenawaranHarga.FocusedItem.SubItems(CellNoUrut).Text & "' and "
                    SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_GLOBAL_Tidak_Ditemukan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        cari()
    End Sub

    Private Sub BatalkanPenawarnToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalkanPenawarnToolStripMenuItem.Click
        If LvPenawaranHarga.Items.Count = 0 Or LvPenawaranHarga.FocusedItem.Index = -1 Then Exit Sub

        Dim Pertanyaan As String = MessageBox.Show("Yakin Ingin Batalkan Penawaran?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Pertanyaan = vbNo Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '===========================
            '=     CEK BUTTON ROLE     =
            '===========================
            If CekButtonRole("Batal_Penawaran") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Penawaran", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim NoPenawaran As String = LvPenawaranHarga.FocusedItem.Text

            '=================================================
            '=     CEK APAKAH PENAWARAN SUDAH DIBATALKAN     =
            '=================================================
            SQL = "select No_Penawaran from EMI_Master_Penawaran where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoPenawaran & "' and status is not null"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Penawaran Tidak Bisa Dibatalkan, Karena Sudah Dibatalkan Sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            '=======================================================
            '=     CEK APAKAH PENAWARAN SUDAH DIPAKAI PO INDUK     =
            '=======================================================
            SQL = "select top 1 a.No_Faktur, b.No_Penawaran "
            SQL = SQL & "from EMI_Pembelian_PO_Induk a, EMI_Pembelian_PO_Detail_Induk b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Penawaran = '" & NoPenawaran & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Penawaran Tidak Bisa Dibatalkan, Karena Penawaran Sudah Dipakai di PO!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=====================================================
            '=     CEK APAKAH PENAWARAN SUDAH DIPAKAI SUB PO     =
            '=====================================================
            SQL = "select top 1 a.No_Faktur, b.No_Penawaran "
            SQL = SQL & "from EMI_Pembelian_PO a, EMI_Pembelian_PO_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Penawaran = '" & NoPenawaran & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Penawaran Tidak Bisa Dibatalkan, Karena Penawaran Sudah Dipakai di PO!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '========================================
            '=     UPDATE FLAG STATUS PENAWARAN     =
            '========================================
            SQL = "update EMI_Master_Penawaran set Status = 'Y', UserId_Batal = '" & UserID & "', "
            SQL = SQL & "Tanggal_Batal = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Batal = '" & Format(tgl_skg, "HH:mm:ss") & "' "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoPenawaran & "' and status is null"
            ExecuteTrans(SQL)




            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Penawaran Berhasil Dibatalkan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        cari()

    End Sub

    'Private Sub DisplayRakToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    If LvPenawaranHarga.Items.Count = 0 Or LvPenawaranHarga.SelectedItems.Count = 0 Then
    '        Exit Sub
    '    End If
    '    EMI_Barang_Masuk_Display_Rak.TxtNoBM.Text = LvPenawaranHarga.FocusedItem.Text
    '    EMI_Barang_Masuk_Display_Rak.ShowDialog()
    'End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            ComboBox3.Enabled = True
            CheckBox3.Checked = False
        Else
            ComboBox3.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            ComboBox3.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.SelectedIndex = -1 Then Exit Sub

        DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
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