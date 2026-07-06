Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Policy
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class N_EMI_Display_Purchase_Requisition_Barang_Lain_Departement

    Dim Arr1, Arr2, Arr3, Arr4 As New ArrayList
    Dim pertama As Integer = 1
    Dim T As Color = Color.Blue
    Dim KT As Color = Color.Red
    Dim KY As Color = Color.Green
    Dim Batal As Color = Color.Black

    Private Sub N_EMI_Pembelian_PR_Summary_Data_Barang_Lain_Departement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        Panel9.BackColor = Color.FromArgb(128, 64, 64)
        kosong()
    End Sub

    Private Sub kosong()
        Sync_Data_PR_Pra_Release()

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
        Lv_PR.Columns.Add("No PR", 110, HorizontalAlignment.Left)
        Lv_PR.Columns.Add("User Created", 100, HorizontalAlignment.Left)
        Lv_PR.Columns.Add("PR Created", 100, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("User Released", 100, HorizontalAlignment.Left)
        Lv_PR.Columns.Add("PR Released", 100, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        Lv_PR.Columns.Add("Status PR", 100, HorizontalAlignment.Center)

        'Lv_PR.Columns.Add("Status", 100, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("Jenis", 0, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("Kategori Gudang", 120, HorizontalAlignment.Center).DisplayIndex = 1
        Lv_PR.Columns.Add("User Batal", 100, HorizontalAlignment.Left)
        Lv_PR.Columns.Add("Tanggal Batal", 100, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("User Pra Release", 120, HorizontalAlignment.Center).DisplayIndex = 4
        Lv_PR.Columns.Add("Tanggal Pra Release", 120, HorizontalAlignment.Left).DisplayIndex = 5

        Lv_PR.View = View.Details

        Lv_PRDetail.Items.Clear() : Lv_PRDetail.Columns.Clear()
        Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left)
        Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_NamaBarang, 200, HorizontalAlignment.Left)
        Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_Satuan, 100, HorizontalAlignment.Center)
        Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_Jumlah, 100, HorizontalAlignment.Center)
        Lv_PRDetail.Columns.Add("Jumlah PR", 110, HorizontalAlignment.Center)
        'Lv_PRDetail.Columns.Add("Jumlah Keep Stock", 110, HorizontalAlignment.Center)
        Lv_PRDetail.Columns.Add("Sisa", 110, HorizontalAlignment.Center)
        Lv_PRDetail.Columns.Add("Link", 200, HorizontalAlignment.Left)
        Lv_PRDetail.Columns.Add("Est. Harga", 200, HorizontalAlignment.Right)
        Lv_PRDetail.Columns.Add("Jumlah Transfer", 200, HorizontalAlignment.Left)
        ' Lv_PRDetail.Columns.Add("%Complete", 110, HorizontalAlignment.Center)
        'Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_Harga, 110, HorizontalAlignment.Right)
        'Lv_PRDetail.Columns.Add("Jumlah Masuk", 110, HorizontalAlignment.Right)
        'Lv_PRDetail.Columns.Add("Sisa", 110, HorizontalAlignment.Right)
        'Lv_PRDetail.Columns.Add("%Complete", 110, HorizontalAlignment.Right)
        'Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_Total, 140, HorizontalAlignment.Right)
        'ListView2.Columns.Add(Base_Language.Lang_Pmb_Barang_Masuk_Tanggal_produksi, 120, HorizontalAlignment.Center)
        'ListView2.Columns.Add(Base_Language.Lang_Pmb_Barang_Masuk_Tanggal_Expire, 120, HorizontalAlignment.Center)
        Lv_PRDetail.View = View.Details

        Lv_Detail.Items.Clear() : Lv_Detail.Columns.Clear()
        Lv_Detail.Columns.Add("No PR", 150, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Tanggal PR", 100, HorizontalAlignment.Center)
        Lv_Detail.Columns.Add("Lokasi", 200, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Jumlah", 200, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        Lv_Detail.Columns.Add("Link", 200, HorizontalAlignment.Left)



        Lv_Detail.View = View.Details


        Try
            OpenConn()

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

            If CekButtonRole("Ganti_Lokasi_PR_Barang_Lain_Departement") = "T" Then
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
            ComboBox2.Items.Add("Kategori Gudang") : Arr2.Add("a.kode_kategori_gudang")
            ComboBox2.Items.Add("User Created") : Arr2.Add("a.userid")
            ComboBox2.Items.Add("User Released") : Arr2.Add("a.user_release")
            'ComboBox2.Items.Add("NO Nota") : Arr2.Add("a.no_nota")
            'ComboBox2.Items.Add("Kode Supplier") : Arr2.Add("a.kode_supplier")

            Label1.Text = "Display - Purchase Requisition Barang Lain Per Departement"
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

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_PR.SelectedIndexChanged
        Try
            OpenConn()
            Lv_PRDetail.Items.Clear()
            SQL = "select a.kode_stock_owner, a.Kode_Barang,a.Nama_Barang,a.jumlah,a.Satuan,"

            SQL = SQL & "isnull((select sum(y.total) from N_EMI_Transfer_Stock_Barang_Lain_Detail y, N_EMI_Transfer_Stock_Barang_Lain z "
            SQL = SQL & "where y.Kode_Perusahaan = a.Kode_Perusahaan and y.Urut_Pr_dept = a.No_Urut and z.status is null "
            SQL = SQL & "and  y.kode_perusahaan = z.kode_perusahaan and y.no_faktur = z.no_faktur "
            SQL = SQL & "), 0) as Jumlah_Transfer, "

            SQL = SQL & "isnull((select sum(x.Jumlah) from EMI_Purchase_Requisition_Barang_Lain_Detail x, EMI_Purchase_Requisition_Barang_Lain y "
            SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur "
            SQL = SQL & "And y.Kode_Perusahaan = a.Kode_Perusahaan And x.urut_departement = a.No_Urut And y.status Is null ), "
            SQL = SQL & "0) as jumlah_masuk, "

            SQL = SQL & "isnull((select sum(y.Jumlah) from N_EMI_Keep_Stock_Barang_Lain_Departement y "
            SQL = SQL & "where y.Kode_Perusahaan = a.Kode_Perusahaan and y.Urut_Departement = a.No_Urut and y.status is null "
            SQL = SQL & "), 0) as Jumlah_Keep_Stock, "

            SQL = SQL & "isnull(a.flag_sudah_pr,'T') as flag_selesai_pr, Link, estimasi_harga as Estimasi_Harga "

            SQL = SQL & "From N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a where "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_faktur = '" & Lv_PR.FocusedItem.SubItems(0).Text & "' and a.jumlah<>0 "

            'If Lv_PR.FocusedItem.SubItems(6).Text = "ada kode" Then
            '    SQL = SQL & "and a.Kode_Barang <> '-' "
            'Else
            '    SQL = SQL & "and a.Kode_Barang = '-' "
            'End If

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim total As Double = Dr("jumlah_masuk") + Dr("Jumlah_Keep_Stock")

                    Dim lvw As ListViewItem
                    lvw = Lv_PRDetail.Items.Add(Dr("kode_barang"))
                    lvw.SubItems.Add(Dr("Nama_Barang"))
                    lvw.SubItems.Add(Dr("satuan"))
                    lvw.SubItems.Add(Format(Dr("jumlah"), "N2"))
                    lvw.SubItems.Add(Format(total, "N2"))
                    'lvw.SubItems.Add(Format(Dr("Jumlah_Keep_Stock"), "N2"))

                    Dim sisa As Double = Dr("jumlah") - total
                    Dim persen As Double = total / Dr("jumlah") * 100
                    If Dr("flag_selesai_pr") = "Y" Then
                        lvw.BackColor = Color.LightGreen
                        lvw.SubItems.Add(Format(sisa, "N2"))
                    Else
                        lvw.SubItems.Add(Format(sisa, "N2"))
                    End If

                    lvw.SubItems.Add(Dr("Link"))
                    If General_Class.CekNULL(Dr("Estimasi_Harga")) = "" Then
                        lvw.SubItems.Add("-")
                    Else
                        lvw.SubItems.Add(Format(Dr("Estimasi_Harga"), "N2"))
                    End If

                    lvw.SubItems.Add(Format(Dr("Jumlah_Transfer"), "N2"))

                    'lvw.SubItems.Add(Format(Dr("tgl_produksi"), "dd MMM yyyy"))
                    'lvw.SubItems.Add(Format(Dr("tgl_expired"), "dd MMM yyyy"))
                Loop
            End Using

            Lv_Detail.Items.Clear()
            SQL = "select a.No_Faktur, d.No_Faktur as No_PR, c.Kode_Stock_Owner, c.Kode_Barang, c.Jumlah, c.Satuan, d.Tanggal, d.Status, c.Link "
            SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement a, N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail b, "
            SQL = SQL & "EMI_Purchase_Requisition_Barang_Lain_Detail c,EMI_Purchase_Requisition_Barang_Lain d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan  "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur and c.No_Faktur = d.No_Faktur and b.No_Urut = c.Urut_Departement  "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur ='" & Lv_PR.FocusedItem.SubItems(0).Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail.Items.Add(Dr("No_PR"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N2"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("Link"))

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



            ' --- Ambil daftar gudang ---
            Dim listGudang As New List(Of String)

            SQL = "select Kode_Kategori_Gudang From N_EMI_View_Master_Kategori_Gudang_Binding_Departement_Barang_Lain where User_ID = '" & UserID & "'"

            SQL = SQL & "group by kode_kategori_gudang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    listGudang.Add("'" & Dr("Kode_Kategori_Gudang").ToString() & "'")
                Loop
            End Using

            ' Jika kosong, kasih nilai palsu biar IN() tidak error
            If listGudang.Count = 0 Then
                listGudang.Add("'0'")
            End If

            ' Gabungkan hasil jadi 1 string
            Dim inGudang As String = String.Join(",", listGudang)





            SQL = "select a.No_Faktur, a.tanggal,a.keterangan,a.flag_pra_release,User_Pra_Release,a.tanggal_pra_release,a.user_release, a.tanggal_release, a.keterangan, isnull((a.kode_kategori_gudang),'-') as Kode_Kategori_Gudang,a.userid, a.Flag_Release, a.Status, 'tidak ada kode' as Jenis "
            SQL = SQL & ",userid_batal, tanggal_batal "
            SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement a "

            SQL = SQL & " where  "


            SQL = SQL & " a.kode_kategori_gudang in (" & inGudang & ") "

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

                SQL = SQL & " a.tanggal between '"
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
                SQL = SQL & " and a.Lokasi = '" & ComboBox6.Text & "' "
            End If
            SQL = SQL & " "
            SQL = SQL & "order by a.Tanggal, a.No_Faktur, Jenis "

            Dim Lvw As ListViewItem
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Lvw = Lv_PR.Items.Add(.Rows(i).Item("no_faktur"))
                            Lvw.SubItems.Add(.Rows(i).Item("userid"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal"), "dd MMM yyyy"))


                            If General_Class.CekNULL(.Rows(i).Item("user_release")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(.Rows(i).Item("user_release"))
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("tanggal_release")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_release"), "dd MMM yyyy"))
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("keterangan")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(.Rows(i).Item("keterangan"))
                            End If






                            If General_Class.CekNULL(.Rows(i).Item("Flag_Release")) = "Y" Then
                                Lvw.SubItems.Add("SUBMITTED")
                            Else
                                Lvw.SubItems.Add("UNSUBMITTED")
                            End If




                            Lvw.SubItems.Add(.Rows(i).Item("Jenis"))
                            Lvw.SubItems.Add(.Rows(i).Item("kode_kategori_gudang"))
                            If General_Class.CekNULL(.Rows(i).Item("UserId_Batal")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(.Rows(i).Item("UserId_Batal"))
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("Tanggal_Batal")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("Tanggal_Batal"), "dd MMM yyyy"))
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("user_pra_release")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(.Rows(i).Item("user_pra_release"))
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("tanggal_pra_release")) = "" Then
                                Lvw.SubItems.Add("-")
                            Else
                                Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_pra_release"), "dd MMM yyyy"))
                            End If





                            If General_Class.CekNULL(.Rows(i).Item("Status")) <> "" Then
                                Lvw.BackColor = Color.FromArgb(128, 64, 64)
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

            SQL = "select Kode_Perusahaan, No_Faktur from N_EMI_View_Transaksi_Purchase_Requisition_Barang_Lain_Departement "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & Lv_PR.FocusedItem.Text & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    With Ds.Tables(0)
                        Dim CrDoc As New N_EMI_CR_Faktur_Purchase_Requisition_Barang_Lain_Departement
                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Laporan Faktur Purchase Requisition Barang Lain Departement"
                            CrDoc.RecordSelectionFormula = " {N_EMI_View_Transaksi_Purchase_Requisition_Barang_Lain_Departement.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Transaksi_Purchase_Requisition_Barang_Lain_Departement.No_Faktur} = '" & Lv_PR.FocusedItem.Text & "'"

                            .Text = "Laporan Faktur Purchase Requisition Barang Lain Departement"
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

            If CekButtonRole("Pembatalan_PR_Barang_Lain_Departement") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan PR", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin akan membatalkan Purhcase Requisition ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then Exit Sub


            ' --- Ambil daftar gudang ---
            Dim listGudang As New List(Of String)

            SQL = "select Kode_Kategori_Gudang From N_EMI_View_Master_Kategori_Gudang_Binding_Departement_Barang_Lain where User_ID = '" & UserID & "'"

            SQL = SQL & "group by kode_kategori_gudang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    listGudang.Add("'" & Dr("Kode_Kategori_Gudang").ToString() & "'")
                Loop
            End Using

            ' Jika kosong, kasih nilai palsu biar IN() tidak error
            If listGudang.Count = 0 Then
                listGudang.Add("'0'")
            End If

            ' Gabungkan hasil jadi 1 string
            Dim inGudang As String = String.Join(",", listGudang)

            SQL = "select Status,kode_kategori_gudang,  userid from N_EMI_Purchase_Requisition_Barang_Lain_Departement where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & Lv_PR.FocusedItem.Text & "' and kode_kategori_gudang in (" & inGudang & ") "
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
                    MessageBox.Show("Anda tidak bisa membatalkan gudang yang bukan milik anda", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select a.No_Faktur from N_EMI_Purchase_Requisition_Barang_Lain_Departement a,"
            SQL = SQL & "N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail b, "
            SQL = SQL & "EMI_Purchase_Requisition_Barang_Lain_Detail c, EMI_Purchase_Requisition_Barang_Lain d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Status is null and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur "
            SQL = SQL & "and b.No_Urut = c.Urut_Departement and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and d.Status is null and a.No_Faktur = '" & Lv_PR.FocusedItem.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Purhcase Requisition Departement tidak bisa dibatalkan,karena sudah masuk tahap Purhcase Requisition!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select a.No_Faktur from N_EMI_Purchase_Requisition_Barang_Lain_Departement a,"
            SQL = SQL & "N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail b, "
            SQL = SQL & "N_EMI_Keep_Stock_Barang_Lain_Departement c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Status is null and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and b.No_Urut = c.Urut_Departement and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and c.Status is null and a.No_Faktur = '" & Lv_PR.FocusedItem.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Purhcase Requisition Departement tidak bisa dibatalkan,karena ada barang yang di ambil dari stock!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "Update N_EMI_Purchase_Requisition_Barang_Lain_Departement set Status = 'Y', "
            SQL = SQL & "UserID_Batal = '" & UserID & "', "
            SQL = SQL & "Tanggal_Batal = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "Jam_Batal = '" & Format(tgl_skg, "HH:mm:ss") & "' "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & Lv_PR.FocusedItem.Text & "' "
            ExecuteTrans(SQL)


            'batalkan juga attachment 
            SQL = " update N_EMI_Purchase_Requisition_Barang_Lain_Departement_Attachment set  "
            SQL = SQL & "status = 'Y' where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and no_faktur = '" & Lv_PR.FocusedItem.Text & "' "
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

    Private Sub CopyNoPRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyNoPRToolStripMenuItem.Click
        If Lv_PR.Items.Count = 0 Or Lv_PR.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no pr yang mau copy!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(Lv_PR.FocusedItem.Text)
    End Sub

    Private Sub LihatPDFToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LihatPDFToolStripMenuItem.Click
        If Lv_PR.Items.Count = 0 Or Lv_PR.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu No PR yang ingin dilihat!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            Dim pathFile As String
            Dim containerFile As String

            SQL = "select b.Path_File, b.container_file From N_EMI_Purchase_Requisition_Barang_Lain_Departement a,  "
            SQL = SQL & "N_EMI_Purchase_Requisition_Barang_Lain_Departement_Attachment b "
            SQL = SQL & "where Flag_Pra_Release = 'Y' and Flag_Release = 'Y' "
            SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.no_faktur = b.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & Lv_PR.FocusedItem.Text & "' "
            SQL = SQL & " and a.Status is null and b.Status is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    pathFile = General_Class.CekNULL(Dr("path_file"))
                    containerFile = General_Class.CekNULL(Dr("container_file"))
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Belum ada file yang di upload untuk no pr ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            'Dim urlPdf As String = AzureHelper_EMI.DownloadFromAzure(containerFile, pathFile)

            'If String.IsNullOrWhiteSpace(urlPdf) Then
            '    MessageBox.Show("File pdf tidak ditemukan")
            'ElseIf urlPdf = "Gagal" Then
            '    MessageBox.Show("Gagal mengambil file dari storage")
            'Else
            '    Using sfd As New SaveFileDialog()
            '        sfd.Filter = "PDF Files (*.pdf)|*.pdf"
            '        sfd.FileName = Path.GetFileName(pathFile)

            '        If sfd.ShowDialog() = DialogResult.OK Then
            '            Using wc As New Net.WebClient()
            '                wc.DownloadFile(urlPdf, sfd.FileName)
            '            End Using

            '            MessageBox.Show("File berhasil disimpan.", "Sukses",
            '            MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        End If
            '    End Using
            'End If

            Dim result = AzureHelper_EMI.DownloadFromAzure(
                containerFile,
                pathFile
            )

            If Not result.Success Then

                If result.Message.Contains("tidak ditemukan") Then
                    MessageBox.Show("File pdf tidak ditemukan")
                Else
                    MessageBox.Show("Gagal mengambil file dari storage." & vbCrLf & result.Message)
                End If
                Exit Sub
            End If

            Using sfd As New SaveFileDialog()
                sfd.Filter = "PDF Files (*.pdf)|*.pdf"
                sfd.FileName = Path.GetFileName(pathFile)

                If sfd.ShowDialog() = DialogResult.OK Then
                    Using wc As New Net.WebClient()
                        wc.DownloadFile(result.Url, sfd.FileName)
                    End Using

                    MessageBox.Show(
                        "File berhasil disimpan.",
                        "Sukses",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    )
                End If
            End Using




            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

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

    Private Sub N_EMI_Pembelian_PR_Summary_Data_Barang_Lain_Departement_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

End Class