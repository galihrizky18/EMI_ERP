Imports System.IO

Public Class N_EMI_Display_Request_Departement_Barang_Lain
    Dim xno_faktur As String = ""
    Dim arrcari, arrcari2 As New ArrayList
    Dim Jenis = "N_EMI_Display_Request_Departement_Barang_Lain"
    Public asal As String = ""
    Public xCmb_Kategori_Gudang As String = ""
    Private Sub N_EMI_Display_Request_Departement_Barang_Lain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        kosong()




        'Cari3("Y")
        'Cari2("Y")
    End Sub


    Private Sub Cari(ByVal semua As String)
        Try
            OpenConn()


            Dim kode_kategori_gudang As String = Purchase_Requisition_Barang_Lain.Cmb_Kategori_Gudang.Text

            Dim kso1 As String = ""
            Dim kodeKategGudang As String = ""
            SQL = "select Kode_Stock_Owner_Gudang,Kode_Kategori_Gudang  "
            SQL = SQL & "From N_EMI_Master_Kategori_Gudang_Barang_Lain where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Kategori_Gudang = '" & kode_kategori_gudang & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    kso1 = Dr("Kode_Stock_Owner_Gudang")
                    kodeKategGudang = Dr("kode_kategori_gudang")

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Terjadi kesalahan pada kode kategori gudang!", "Keep Stok Barang", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            DataGridView1.Rows.Clear()

            SQL = "with cte as ( "
            SQL = SQL & "select a.Kode_Perusahaan, b.Id_Sub_Kategori_Jenis From N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain a "
            SQL = SQL & "inner join N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain b "
            SQL = SQL & "on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Gudang = b.Id_Kategori_Gudang "
            SQL = SQL & "inner join N_EMI_Master_Kategori_Gudang_Barang_Lain c  on a.Id_Kategori_Gudang = c.Urut_Oto "
            SQL = SQL & "where User_ID = '" & UserID & "' "
            SQL = SQL & "and c.Jenis_Gudang = 'Warehouse'  "
            SQL = SQL & "group by b.Id_Sub_Kategori_Jenis, a.Kode_Perusahaan  "
            SQL = SQL & ") "

            SQL = SQL & "select a.No_Faktur,a.Kode_Stock_Owner, a.Kode_Barang, a.Nama_Barang, a.Jumlah, a.Jmlh_PR, a.Satuan, b.Keterangan as Cost_Center, d.kode_kategori_gudang,d.tanggal,"
            SQL = SQL & "a.No_Urut, d.Lokasi, a.Flag_Ajukan, a.Link, d.UserId, a.id_sub_kategori_jenis, a.Id_Cost_Center, a.Alasan_Tolak, "

            SQL = SQL & "isnull(( select c.Keterangan from N_EMI_Master_Gedung_Barang_Lain c "
            SQL = SQL & "where a.Kode_Perusahaan = c.Kode_Perusahaan and a.ID_Gedung = c.ID_Gedung ), NULL) as Gedung, "

            SQL = SQL & "isnull((select sum(e.Jumlah) from Barang_Lain_SN e where a.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and '" & kso1 & "' = e.Kode_Stock_Owner and a.Kode_Barang = e.Kode_Barang),0) as stock, "

            SQL = SQL & "isnull((select sum(f.Jumlah) from N_EMI_Keep_Stock_Barang_Lain_Departement f where "
            SQL = SQL & "a.Kode_Perusahaan = f.Kode_Perusahaan and '" & kso1 & "' = f.Kode_Stock_Owner and a.Kode_Barang = f.Kode_Barang "
            SQL = SQL & "and a.No_Urut = f.Urut_Departement and f.Flag_Selesai_Pengeluaran_Barang is null and f.Status is null),0) as Jumlah_Keep_Stock, "

            SQL = SQL & "isnull((select sum(f.Jumlah) from N_EMI_Keep_Stock_Barang_Lain_Departement f where "
            SQL = SQL & "a.Kode_Perusahaan = f.Kode_Perusahaan and a.Kode_Barang = f.Kode_Barang and '" & kso1 & "' = f.Kode_Stock_Owner "
            SQL = SQL & "and f.Flag_Selesai_Pengeluaran_Barang is null and f.Status is null and f.Urut_Departement = a.No_Urut),0) as Jumlah_Keep_Stock_2, "


            SQL = SQL & " isnull((
            select sum(z.jumlah) from N_EMI_Transfer_Stock_Barang_Lain_Detail x, N_EMI_Transfer_Stock_Barang_Lain y, N_EMI_Transfer_Stock_Barang_Lain_Det  z
            where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and x.Kode_Perusahaan = z.Kode_Perusahaan and
            x.Urut_Oto = z.Urut_TF and z.Selesai = 'Y' and  y.Status is null
            and x.Kode_Perusahaan = a.Kode_Perusahaan and x.Urut_PR_Dept = a.No_Urut
            ),0) as Jumlah_Sdh_Transfer , "


            SQL = SQL & "ISNULL(CAST(a.Id_Sub_Kategori_Jenis AS VARCHAR(20)), null) as Id_Sub_Kategori_Jenis, "

            SQL = SQL & "isnull((select z.Keterangan + ' - ' + x.Keterangan as Sub_Kategori_Jenis "
            SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis z, N_EMI_Master_Kategori_Jenis x "
            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and z.Id_Kategori_Jenis = x.Id_Kategori_Jenis "
            SQL = SQL & "and z.Id_Sub_Kategori_Jenis = a.Id_Sub_Kategori_Jenis ),null) as Sub_Kategori_Jenis "

            SQL = SQL & "FROM N_EMI_Purchase_Requisition_Barang_Lain_Departement d, N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a,  "
            SQL = SQL & " EMI_Master_Cost_Center b , cte c "

            SQL = SQL & "WHERE a.Kode_Perusahaan = d.Kode_Perusahaan AND a.No_Faktur = d.No_Faktur AND a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "AND a.Id_Cost_Center = b.Id_Cost_Center   and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis = c.Id_Sub_Kategori_Jenis "
            SQL = SQL & "AND d.Status IS NULL AND d.Flag_Release = 'Y' and flag_selesai_transfer is null  AND d.Flag_PR IS NULL AND a.Flag_Sudah_PR IS NULL "
            SQL = SQL & "AND a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Flag_Reject is null "

            If semua = "T" Then
                If CbTanggal.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & "d.Tanggal between '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
                End If

                If CheckBox2.Checked Then
                    'Pasang And
                    If CmbSatuan_Kolom.Text = "Barang dalam pengajuan" Then
                        If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                        SQL = SQL & arrcari.Item(CmbSatuan_Kolom.SelectedIndex) & " = 'Y' "
                    Else
                        If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                        SQL = SQL & arrcari.Item(CmbSatuan_Kolom.SelectedIndex) & " like '%" & TxtSatuan_Value.Text & "%' "
                    End If

                End If

                If cbHariIni.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & " d.tanggal between '"
                    SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
                End If
            End If
            SQL = SQL & "order by d .tanggal asc, d.jam asc "



            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            'Dim Kategori_Gudang As String = .Rows(i).Item("kode_kategori_gudang")

                            'Dim Kategori_Gudang As String = ""
                            ''SQL = "select top(1) kode_kategori_gudang From N_EMI_View_Master_Kategori_Gudang_Binding_Departement_Barang_Lain  "
                            ''SQL = SQL & "where user_id = '" & .Rows(i).Item("userid") & "' and kode_perusahaan = '" & KodePerusahaan & "' "
                            ''SQL = SQL & "and Kode_Kategori_Gudang = '" & xCmb_Kategori_Gudang & "' "

                            'SQL = "select top(1) kode_kategori_gudang From N_EMI_View_Master_Kategori_Gudang_Binding_Barang_Lain  "
                            'SQL = SQL & "where user_id = '" & .Rows(i).Item("userid") & "' and kode_perusahaan = '" & KodePerusahaan & "' "
                            'SQL = SQL & "and id_sub_kategori_jenis = '" & .Rows(i).Item("id_sub_kategori_jenis") & "' "
                            'Using Dr = OpenTrans(SQL)
                            '    If Dr.Read Then
                            '        Kategori_Gudang = Dr("kode_kategori_gudang")
                            '    End If
                            'End Using

                            DataGridView1.Rows.Add(1)
                            'DataGridView1.Rows.Item(i).Cells(0).Value = "" Jumlah_Keep_Stock



                            DataGridView1.Rows.Item(i).Cells(1).Value = .Rows(i).Item("No_Faktur") & " - " & .Rows(i).Item("kode_kategori_gudang")
                            DataGridView1.Rows.Item(i).Cells(2).Value = .Rows(i).Item("Kode_Stock_Owner")
                            DataGridView1.Rows.Item(i).Cells(3).Value = .Rows(i).Item("Kode_Barang")
                            DataGridView1.Rows.Item(i).Cells(4).Value = .Rows(i).Item("Nama_Barang")
                            DataGridView1.Rows.Item(i).Cells(5).Value = (Format(.Rows(i).Item("jumlah") - .Rows(i).Item("Jmlh_PR") - .Rows(i).Item("Jumlah_Keep_Stock"), "N2") - .Rows(i).Item("Jumlah_Sdh_Transfer"))
                            DataGridView1.Rows.Item(i).Cells(6).Value = (Format(.Rows(i).Item("stock") - .Rows(i).Item("Jumlah_Keep_Stock"), "N2"))
                            DataGridView1.Rows.Item(i).Cells(7).Value = .Rows(i).Item("Satuan")
                            DataGridView1.Rows.Item(i).Cells(8).Value = .Rows(i).Item("Cost_Center")
                            DataGridView1.Rows.Item(i).Cells(9).Value = .Rows(i).Item("Gedung")
                            DataGridView1.Rows.Item(i).Cells(10).Value = .Rows(i).Item("No_Urut")
                            DataGridView1.Rows.Item(i).Cells(11).Value = .Rows(i).Item("Lokasi")
                            DataGridView1.Rows.Item(i).Cells(13).Value = .Rows(i).Item("Link")
                            DataGridView1.Rows.Item(i).Cells(14).Value = .Rows(i).Item("Id_Cost_Center")

                            If .Rows(i).Item("Kode_Stock_Owner") = "-" Then
                                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightSteelBlue
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("Flag_Ajukan")) = "Y" Then
                                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.NavajoWhite
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("Alasan_Tolak")) = "" Then
                                DataGridView1.Rows.Item(i).Cells(15).Value = "-"
                            Else
                                DataGridView1.Rows.Item(i).Cells(15).Value = .Rows(i).Item("Alasan_Tolak")
                            End If

                            DataGridView1.Rows.Item(i).Cells(16).Value = .Rows(i).Item("UserId")

                            If General_Class.CekNULL(.Rows(i).Item("Sub_Kategori_Jenis")) = "" Then
                                DataGridView1.Rows.Item(i).Cells(17).Value = "-"
                            Else
                                DataGridView1.Rows.Item(i).Cells(17).Value = .Rows(i).Item("Sub_Kategori_Jenis")
                            End If


                            If General_Class.CekNULL(.Rows(i).Item("Id_Sub_Kategori_Jenis")) = "" Then
                                DataGridView1.Rows.Item(i).Cells(18).Value = "-"
                            Else
                                DataGridView1.Rows.Item(i).Cells(18).Value = .Rows(i).Item("Id_Sub_Kategori_Jenis")
                            End If

                            DataGridView1.Rows.Item(i).Cells(19).Value = Format(.Rows(i).Item("tanggal"), "dd MMM yyyy")
                            DataGridView1.Rows.Item(i).Cells(21).Value = .Rows(i).Item("No_Faktur")


                            If General_Class.CekNULL(.Rows(i).Item("Alasan_Tolak")) <> "" And .Rows(i).Item("Kode_Stock_Owner") = "-" Then
                                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.RosyBrown
                            End If
                        Next
                        'Else
                        '    CloseConn()
                        '    MessageBox.Show("Data tidak ditemuakan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
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

    Private Sub Cari2(ByVal semua As String)
        Try
            OpenConn()

            DataGridView1.Rows.Clear()

            SQL = "select DISTINCT a.No_Faktur,a.Kode_Stock_Owner, a.Kode_Barang, a.Nama_Barang, a.Jumlah, a.Jmlh_PR, a.Satuan, b.Keterangan as Cost_Center, a.No_Urut, d.Lokasi, a.Flag_Ajukan, a.Link, d.UserId, "
            SQL = SQL & "isnull(( select c.Keterangan from N_EMI_Master_Gedung_Barang_Lain c where "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.ID_Gedung = c.ID_Gedung ), NULL) as Gedung, a.Id_Cost_Center, a.Alasan_Tolak, "
            SQL = SQL & "isnull((select sum(e.Jumlah) from Barang_Lain_SN e where a.Kode_Perusahaan = e.Kode_Perusahaan and a.Kode_Stock_Owner = e.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = e.Kode_Barang),0) as stock, "
            SQL = SQL & "isnull((select sum(f.Jumlah) from N_EMI_Keep_Stock_Barang_Lain_Departement f where a.Kode_Perusahaan = f.Kode_Perusahaan and a.Kode_Stock_Owner = f.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = f.Kode_Barang and a.No_Urut = f.Urut_Departement and f.Flag_Selesai_Pengeluaran_Barang is null and f.Status is null and f.Urut_Departement = a.No_Urut),0) as Jumlah_Keep_Stock, "
            SQL = SQL & "isnull((select sum(f.Jumlah) from N_EMI_Keep_Stock_Barang_Lain_Departement f where a.Kode_Perusahaan = f.Kode_Perusahaan and "
            SQL = SQL & "a.Kode_Barang = f.Kode_Barang and a.Kode_Stock_Owner = f.Kode_Stock_Owner and f.Flag_Selesai_Pengeluaran_Barang is null and f.Status is null and f.Urut_Departement = a.No_Urut),0) as Jumlah_Keep_Stock_2 "
            '  SQL = SQL & ",a.Id_Sub_Kategori_Jenis, a.Sub_Kategori_Jenis "
            SQL = SQL & ",isnull(( select c.Keterangan from N_EMI_Master_Gedung_Barang_Lain c where "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.ID_Gedung = c.ID_Gedung ), NULL) as Gedung, a.Id_Cost_Center, a.Alasan_Tolak, "
            SQL = SQL & "ISNULL(CAST(a.Id_Sub_Kategori_Jenis AS VARCHAR(20)), null) as Id_Sub_Kategori_Jenis, "
            SQL = SQL & "isnull((select z.Keterangan + ' - ' + x.Keterangan as Sub_Kategori_Jenis from N_EMI_Master_Sub_Kategori_Jenis z, N_EMI_Master_Kategori_Jenis x "
            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and z.Id_Kategori_Jenis = x.Id_Kategori_Jenis and z.Id_Sub_Kategori_Jenis = a.Id_Sub_Kategori_Jenis "
            SQL = SQL & "),null) as Sub_Kategori_Jenis "

            SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a, EMI_Master_Cost_Center b, N_EMI_Purchase_Requisition_Barang_Lain_Departement d, "
            ' SQL = SQL & "Barang_Lain e, View_Kategori_Turunan f, N_EMI_Master_Role_Sub_Kategori g "

            SQL = SQL & " N_EMI_View_Master_Kategori_Gudang_Binding_Barang_Lain g "

            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and d.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Cost_Center = b.Id_Cost_Center  "

            '  SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and a.Kode_Stock_Owner = e.Kode_Stock_Owner and a.Kode_Barang = e.Kode_Barang and e.Kode_Perusahaan = f.Kode_Perusahaan and "
            ' SQL = SQL & "e.Id_Sub_Kategori_Jenis_3 = f.Id_Sub_Kategori_Jenis_3 and f.Id_Kategori_Jenis = g.Id_Kategori_Jenis "
            'SQL = SQL & "and f.Id_Sub_Kategori_Jenis = g.Id_Sub_Kategori_Jenis and g.UserID = '" & UserID & "' "

            SQL = SQL & "and a.kode_perusahaan = g.kode_perusahaan and a.Id_Sub_Kategori_Jenis = g.Id_Sub_Kategori_Jenis and g.User_ID = '" & UserID & "' "
            SQL = SQL & "and a.Flag_Sudah_PR is null  "
            'SQL = SQL & "and a.Kode_Barang <> '-'"
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = d.No_Faktur and d.Flag_Release = 'Y' and d.Flag_PR is null and a.Flag_Reject is null  "

            If semua = "T" Then
                If CbTanggal.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & "d.Tanggal between '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
                End If

                If CheckBox2.Checked Then
                    'Pasang And
                    If CmbSatuan_Kolom.Text = "Barang dalam pengajuan" Then
                        If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                        SQL = SQL & arrcari.Item(CmbSatuan_Kolom.SelectedIndex) & " = 'Y' "
                    Else
                        If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                        SQL = SQL & arrcari.Item(CmbSatuan_Kolom.SelectedIndex) & " like '%" & TxtSatuan_Value.Text & "%' "
                    End If

                End If

                If cbHariIni.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & " d.tanggal between '"
                    SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
                End If
            End If
            SQL = SQL & "order by a.No_Faktur "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DataGridView1.Rows.Add(1)

                            Dim Kategori_Gudang As String = ""
                            SQL = "select top(1) kode_kategori_gudang From N_EMI_View_Master_Kategori_Gudang_Binding_Departement_Barang_Lain  "
                            SQL = SQL & "where user_id = '" & .Rows(i).Item("userid") & "' and kode_perusahaan = '" & KodePerusahaan & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Kategori_Gudang = Dr("kode_kategori_gudang")
                                End If
                            End Using


                            'DataGridView1.Rows.Item(i).Cells(0).Value = "" Jumlah_Keep_Stock
                            DataGridView1.Rows.Item(i).Cells(1).Value = .Rows(i).Item("No_Faktur") & " - " & Kategori_Gudang
                            DataGridView1.Rows.Item(i).Cells(2).Value = .Rows(i).Item("Kode_Stock_Owner")
                            DataGridView1.Rows.Item(i).Cells(3).Value = .Rows(i).Item("Kode_Barang")
                            DataGridView1.Rows.Item(i).Cells(4).Value = .Rows(i).Item("Nama_Barang")
                            DataGridView1.Rows.Item(i).Cells(5).Value = (Format(.Rows(i).Item("jumlah") - .Rows(i).Item("Jmlh_PR") - .Rows(i).Item("Jumlah_Keep_Stock"), "N2"))
                            DataGridView1.Rows.Item(i).Cells(6).Value = (Format(.Rows(i).Item("stock") - .Rows(i).Item("Jumlah_Keep_Stock_2"), "N2"))
                            DataGridView1.Rows.Item(i).Cells(7).Value = .Rows(i).Item("Satuan")
                            DataGridView1.Rows.Item(i).Cells(8).Value = .Rows(i).Item("Cost_Center")
                            DataGridView1.Rows.Item(i).Cells(9).Value = .Rows(i).Item("Gedung")
                            DataGridView1.Rows.Item(i).Cells(10).Value = .Rows(i).Item("No_Urut")
                            DataGridView1.Rows.Item(i).Cells(11).Value = .Rows(i).Item("Lokasi")
                            DataGridView1.Rows.Item(i).Cells(13).Value = .Rows(i).Item("Link")
                            DataGridView1.Rows.Item(i).Cells(14).Value = .Rows(i).Item("Id_Cost_Center")

                            If .Rows(i).Item("Kode_Stock_Owner") = "-" Then
                                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightSteelBlue
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("Flag_Ajukan")) = "Y" Then
                                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.NavajoWhite
                            End If

                            If General_Class.CekNULL(.Rows(i).Item("Alasan_Tolak")) = "" Then
                                DataGridView1.Rows.Item(i).Cells(15).Value = "-"
                            Else
                                DataGridView1.Rows.Item(i).Cells(15).Value = .Rows(i).Item("Alasan_Tolak")
                            End If

                            DataGridView1.Rows.Item(i).Cells(16).Value = .Rows(i).Item("UserId")

                            If General_Class.CekNULL(.Rows(i).Item("Sub_Kategori_Jenis")) = "" Then
                                DataGridView1.Rows.Item(i).Cells(17).Value = "-"
                            Else
                                DataGridView1.Rows.Item(i).Cells(17).Value = .Rows(i).Item("Sub_Kategori_Jenis")
                            End If


                            If General_Class.CekNULL(.Rows(i).Item("Id_Sub_Kategori_Jenis")) = "" Then
                                DataGridView1.Rows.Item(i).Cells(18).Value = "-"
                            Else
                                DataGridView1.Rows.Item(i).Cells(18).Value = .Rows(i).Item("Id_Sub_Kategori_Jenis")
                            End If



                            If General_Class.CekNULL(.Rows(i).Item("Alasan_Tolak")) <> "" And .Rows(i).Item("Kode_Stock_Owner") = "-" Then
                                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.RosyBrown
                            End If
                        Next
                        'Else
                        '    CloseConn()
                        '    MessageBox.Show("Data tidak ditemuakan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
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

    Private Sub Cari3(ByVal semua As String)
        Try
            OpenConn()

            DataGridView3.Rows.Clear()

            SQL = "with cte as("
            SQL = SQL & "select a.Kode_Perusahaan, a.No_Faktur, a.Kode_Barang_Baru, "
            SQL = SQL & "isnull((select c.Nama from Barang_Lain c where a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Barang_Baru = c.Kode_Barang "
            SQL = SQL & "group by c.Nama),'-') as Nama_Barang_Baru, "
            SQL = SQL & "b.Kategori_Jenis, b.Sub_Kategori_Jenis, b.Sub_Kategori_Jenis_1, b.Sub_Kategori_Jenis_2, "
            SQL = SQL & "b.Sub_Kategori_Jenis_3, a.Tanggal, a.Userid, a.Flag_Pengajuan_Barang_Baru, a.Keterangan_Tolak "
            SQL = SQL & "from N_EMI_Pengajuan_Barang_Baru_Lain a, View_Kategori_Turunan b, N_EMI_View_Master_Kategori_Gudang_Binding_Barang_Lain c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis_3 = b.Id_Sub_Kategori_Jenis_3  "
            SQL = SQL & "and b.Id_Kategori_Jenis = c.Id_Kategori_Jenis "
            SQL = SQL & "And b.Id_Sub_Kategori_Jenis = c.Id_Sub_Kategori_Jenis And c.User_ID = '" & UserID & "' and a.Urut_Departement is null "

            SQL = SQL & "union all "
            SQL = SQL & "select a.Kode_Perusahaan, a.No_Faktur, a.Kode_Barang_Baru, "
            SQL = SQL & "isnull((select c.Nama from Barang_Lain c where a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Barang_Baru = c.Kode_Barang "
            SQL = SQL & "group by c.Nama),'-') as Nama_Barang_Baru, "
            SQL = SQL & "b.Kategori_Jenis, b.Sub_Kategori_Jenis, b.Sub_Kategori_Jenis_1, b.Sub_Kategori_Jenis_2, "
            SQL = SQL & "b.Sub_Kategori_Jenis_3, a.Tanggal, a.Userid, a.Flag_Pengajuan_Barang_Baru, a.Keterangan_Tolak, a.Urut_Departement "
            SQL = SQL & "from N_EMI_Pengajuan_Barang_Baru_Lain a, View_Kategori_Turunan b, N_EMI_View_Master_Kategori_Gudang_Binding_Barang_Lain c, "
            SQL = SQL & "N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis_3 = b.Id_Sub_Kategori_Jenis_3 "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Urut_Departement = d.No_Urut and d.Flag_Reject is null "
            SQL = SQL & "and b.Id_Kategori_Jenis = c.Id_Kategori_Jenis "
            SQL = SQL & "And b.Id_Sub_Kategori_Jenis = c.Id_Sub_Kategori_Jenis "
            SQL = SQL & "And c.User_ID = '" & UserID & "' "

            SQL = SQL & ") "
            SQL = SQL & "select Kode_Perusahaan,No_Faktur, Kode_Barang_Baru, Nama_Barang_Baru, "
            SQL = SQL & "Kategori_Jenis, Sub_Kategori_Jenis, Sub_Kategori_Jenis_1, Sub_Kategori_Jenis_2, Sub_Kategori_Jenis_3, "
            SQL = SQL & "Tanggal, Userid, Flag_Pengajuan_Barang_Baru, Keterangan_Tolak "
            SQL = SQL & "from cte where Kode_Perusahaan = '" & KodePerusahaan & "' "

            If semua = "T" Then
                If CheckBox5.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & "Tanggal between '" & Format(DateTimePicker3.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker4.Value, "yyyy-MM-dd") & "' "
                End If

                If CheckBox6.Checked Then
                    'Pasang And
                    If ComboBox2.Text = "Barang dalam pengajuan" Then
                        If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                        SQL = SQL & arrcari2.Item(ComboBox2.SelectedIndex) & " = 'Y' "
                    Else
                        If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                        SQL = SQL & arrcari2.Item(ComboBox2.SelectedIndex) & " like '%" & TextBox1.Text & "%' "
                    End If

                End If

                If CheckBox4.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & " tanggal between '"
                    SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
                End If
            End If
            SQL = SQL & "order by No_Faktur "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DataGridView3.Rows.Add(1)
                            DataGridView3.Rows.Item(i).Cells(0).Value = .Rows(i).Item("No_Faktur")

                            If General_Class.CekNULL(.Rows(i).Item("Kode_Barang_Baru")) = "" Then
                                DataGridView3.Rows.Item(i).Cells(1).Value = "-"
                            Else
                                DataGridView3.Rows.Item(i).Cells(1).Value = .Rows(i).Item("Kode_Barang_Baru")
                            End If

                            DataGridView3.Rows.Item(i).Cells(2).Value = .Rows(i).Item("Nama_Barang_Baru")
                            DataGridView3.Rows.Item(i).Cells(3).Value = .Rows(i).Item("Kategori_Jenis")
                            DataGridView3.Rows.Item(i).Cells(4).Value = .Rows(i).Item("Sub_Kategori_Jenis")
                            DataGridView3.Rows.Item(i).Cells(5).Value = .Rows(i).Item("Sub_Kategori_Jenis_1")
                            DataGridView3.Rows.Item(i).Cells(6).Value = .Rows(i).Item("Sub_Kategori_Jenis_2")
                            DataGridView3.Rows.Item(i).Cells(7).Value = .Rows(i).Item("Sub_Kategori_Jenis_3")
                            DataGridView3.Rows.Item(i).Cells(8).Value = Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy")
                            DataGridView3.Rows.Item(i).Cells(9).Value = .Rows(i).Item("Userid")

                            If General_Class.CekNULL(.Rows(i).Item("Flag_Pengajuan_Barang_Baru")) = "Y" Then
                                DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.NavajoWhite
                            ElseIf General_Class.CekNULL(.Rows(i).Item("Flag_Pengajuan_Barang_Baru")) = "T" Then
                                DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.RosyBrown
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

    'Private Sub Cari2(ByVal semua As String)
    '    Try
    '        OpenConn()

    '        DataGridView2.Rows.Clear()

    '        SQL = "select a.No_Faktur,a.Kode_Stock_Owner, a.Kode_Barang, a.Nama_Barang, a.Jumlah, a.Jmlh_PR, a.Satuan, b.Keterangan as Cost_Center, a.No_Urut, d.Lokasi, a.Flag_Ajukan, a.Link, d.UserId, "
    '        SQL = SQL & "isnull(( select c.Keterangan from N_EMI_Master_Gedung_Barang_Lain c where "
    '        SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.ID_Gedung = c.ID_Gedung ), NULL) as Gedung, a.Id_Cost_Center, a.Alasan_Tolak, "
    '        SQL = SQL & "ISNULL(CAST(a.Id_Sub_Kategori_Jenis AS VARCHAR(20)), '-') as Id_Sub_Kategori_Jenis, "
    '        SQL = SQL & "isnull((select z.Keterangan + ' - ' + x.Keterangan as Sub_Kategori_Jenis from N_EMI_Master_Sub_Kategori_Jenis z, N_EMI_Master_Kategori_Jenis x "
    '        SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and z.Id_Kategori_Jenis = x.Id_Kategori_Jenis and z.Id_Sub_Kategori_Jenis = a.Id_Sub_Kategori_Jenis "
    '        SQL = SQL & "),'-') as Sub_Kategori_Jenis "
    '        SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a, EMI_Master_Cost_Center b, N_EMI_Purchase_Requisition_Barang_Lain_Departement d "
    '        SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and d.Status is null "
    '        SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Cost_Center = b.Id_Cost_Center "
    '        SQL = SQL & "and a.Flag_Sudah_PR is null and a.Kode_Barang = '-' "
    '        SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = d.No_Faktur and d.Flag_Release = 'Y' and d.Flag_PR is null "

    '        If semua = "T" Then
    '            If CheckBox1.Checked Then
    '                'Pasang And
    '                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

    '                SQL = SQL & "d.Tanggal between '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
    '            End If

    '            If CheckBox2.Checked Then
    '                'Pasang And
    '                If CmbSatuan_Kolom.Text = "Barang dalam pengajuan" Then
    '                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

    '                    SQL = SQL & arrcari.Item(CmbSatuan_Kolom.SelectedIndex) & " = 'Y' "
    '                Else
    '                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

    '                    SQL = SQL & arrcari.Item(CmbSatuan_Kolom.SelectedIndex) & " like '%" & TxtSatuan_Value.Text & "%' "
    '                End If

    '            End If

    '            If CheckBox3.Checked Then
    '                'Pasang And
    '                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

    '                SQL = SQL & " d.tanggal between '"
    '                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
    '            End If
    '        End If
    '        SQL = SQL & "order by a.No_Faktur "
    '        Using Ds = BindingTrans(SQL)
    '            With Ds.Tables("MyTable")
    '                If .Rows.Count <> 0 Then
    '                    For i As Integer = 0 To .Rows.Count - 1
    '                        DataGridView2.Rows.Add(1)
    '                        'DataGridView2.Rows.Item(i).Cells(0).Value = ""
    '                        DataGridView2.Rows.Item(i).Cells(1).Value = .Rows(i).Item("No_Faktur")
    '                        DataGridView2.Rows.Item(i).Cells(2).Value = .Rows(i).Item("Kode_Stock_Owner")
    '                        DataGridView2.Rows.Item(i).Cells(3).Value = .Rows(i).Item("Kode_Barang")
    '                        DataGridView2.Rows.Item(i).Cells(4).Value = .Rows(i).Item("Nama_Barang")
    '                        DataGridView2.Rows.Item(i).Cells(5).Value = (Format(.Rows(i).Item("jumlah") - .Rows(i).Item("Jmlh_PR"), "N2"))
    '                        DataGridView2.Rows.Item(i).Cells(6).Value = .Rows(i).Item("Satuan")
    '                        DataGridView2.Rows.Item(i).Cells(7).Value = .Rows(i).Item("Cost_Center")
    '                        DataGridView2.Rows.Item(i).Cells(8).Value = .Rows(i).Item("Gedung")
    '                        DataGridView2.Rows.Item(i).Cells(9).Value = .Rows(i).Item("No_Urut")
    '                        DataGridView2.Rows.Item(i).Cells(10).Value = .Rows(i).Item("Lokasi")
    '                        DataGridView2.Rows.Item(i).Cells(12).Value = .Rows(i).Item("Link")
    '                        DataGridView2.Rows.Item(i).Cells(13).Value = .Rows(i).Item("Id_Cost_Center")

    '                        If .Rows(i).Item("Kode_Stock_Owner") = "-" Then
    '                            DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.LightSteelBlue
    '                        End If

    '                        If General_Class.CekNULL(.Rows(i).Item("Flag_Ajukan")) = "Y" Then
    '                            DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.NavajoWhite
    '                        End If

    '                        If General_Class.CekNULL(.Rows(i).Item("Alasan_Tolak")) = "" Then
    '                            DataGridView2.Rows.Item(i).Cells(14).Value = "-"
    '                        Else
    '                            DataGridView2.Rows.Item(i).Cells(14).Value = .Rows(i).Item("Alasan_Tolak")
    '                        End If

    '                        DataGridView2.Rows.Item(i).Cells(15).Value = .Rows(i).Item("UserID")
    '                        DataGridView2.Rows.Item(i).Cells(16).Value = .Rows(i).Item("Sub_Kategori_Jenis")
    '                        DataGridView2.Rows.Item(i).Cells(17).Value = .Rows(i).Item("Id_Sub_Kategori_Jenis")

    '                        If General_Class.CekNULL(.Rows(i).Item("Alasan_Tolak")) <> "" And .Rows(i).Item("Kode_Stock_Owner") = "-" Then
    '                            DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.RosyBrown
    '                        End If
    '                    Next
    '                    'Else
    '                    '    CloseConn()
    '                    '    MessageBox.Show("Data tidak ditemuakan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    '    Exit Sub
    '                End If
    '            End With
    '        End Using

    '        CloseConn()
    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    'End Sub
    Private Sub kosong()
        Try
            OpenConn()
            DateTimePicker1.Value = Now
            DateTimePicker2.Value = Now
            DateTimePicker3.Value = Now
            DateTimePicker4.Value = Now

            CmbSatuan_Kolom.Items.Clear() : arrcari.Clear()
            CmbSatuan_Kolom.Items.Add("No Faktur") : arrcari.Add("a.No_Faktur")
            CmbSatuan_Kolom.Items.Add("Kode Barang") : arrcari.Add("a.Kode_Barang")
            CmbSatuan_Kolom.Items.Add("Nama Barang") : arrcari.Add("a.Nama_Barang")
            CmbSatuan_Kolom.Items.Add("Cost Center") : arrcari.Add("b.Keterangan")
            CmbSatuan_Kolom.Items.Add("Gedung") : arrcari.Add("c.Keterangan")
            CmbSatuan_Kolom.Items.Add("Barang dalam pengajuan") : arrcari.Add("a.Flag_Ajukan")

            ComboBox2.Items.Clear() : arrcari2.Clear()
            ComboBox2.Items.Add("No Faktur") : arrcari2.Add("No_Faktur")
            ComboBox2.Items.Add("Kode Barang") : arrcari2.Add("Kode_Barang_Baru")
            ComboBox2.Items.Add("Nama Barang") : arrcari2.Add("Nama_Barang_Baru")
            ComboBox2.Items.Add("Kategori Jenis") : arrcari2.Add("Kategori_Jenis")
            ComboBox2.Items.Add("Sub Kategori Jenis") : arrcari2.Add("Sub_Kategori_Jenis")
            ComboBox2.Items.Add("Sub Kategori Jenis 1") : arrcari2.Add("Sub_Kategori_Jenis_1")
            ComboBox2.Items.Add("Sub Kategori Jenis 2") : arrcari2.Add("Sub_Kategori_Jenis_2")
            ComboBox2.Items.Add("Sub Kategori Jenis 3") : arrcari2.Add("Sub_Kategori_Jenis_3")

            CbTanggal.Checked = False
            CheckBox2.Checked = False
            cbHariIni.Checked = False
            CheckBox4.Checked = False
            CheckBox5.Checked = False
            CheckBox6.Checked = False

            CmbSatuan_Kolom.SelectedIndex = -1
            TxtSatuan_Value.Text = ""
            ComboBox2.SelectedIndex = -1
            TextBox1.Text = ""

            Label4.BackColor = Color.LightSteelBlue
            Label6.BackColor = Color.NavajoWhite
            Label5.BackColor = Color.RosyBrown

            ComboBox6.Items.Clear()
            ComboBox6.Items.Add("--SELURUH--")

            ComboBox1.Items.Clear()
            ComboBox1.Items.Add("--SELURUH--")

            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by kode_stock_owner"
            'ComboBox1.Items.Add("Seluruh")
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox6.Items.Add(dr("kode_stock_owner"))
                    ComboBox1.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using

            ComboBox6.Text = Lokasi
            ComboBox1.Text = Lokasi

            If CekButtonRole("Ganti_Lokasi_Display_Penjualan") = "T" Then
                ComboBox6.Enabled = False
                'ComboBox1.Enabled = False
            Else
                ComboBox6.Enabled = True
                'ComboBox1.Enabled = True
            End If

            'DataGridView1.Rows.Clear()
            'SQL = "select a.No_Faktur,a.Kode_Stock_Owner, a.Kode_Barang, a.Nama_Barang, a.Jumlah, a.Jmlh_PR, a.Satuan, b.Keterangan as Cost_Center, a.No_Urut, d.Lokasi, a.Flag_Ajukan, "
            'SQL = SQL & "isnull(( select c.Keterangan from N_EMI_Master_Gedung_Barang_Lain c where "
            'SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.ID_Gedung = c.ID_Gedung ), NULL) as Gedung, a.Id_Cost_Center, a.Alasan_Tolak "
            'SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a, EMI_Master_Cost_Center b, N_EMI_Purchase_Requisition_Barang_Lain_Departement d "
            'SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Cost_Center = b.Id_Cost_Center "
            'SQL = SQL & "and a.Flag_Sudah_PR is null "
            'SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = d.No_Faktur and d.Flag_Release = 'Y' and d.Flag_PR is null "
            'SQL = SQL & "order by a.No_Faktur "
            'Using Ds = BindingTrans(SQL)
            '    With Ds.Tables("MyTable")
            '        If .Rows.Count <> 0 Then
            '            For i As Integer = 0 To .Rows.Count - 1
            '                DataGridView1.Rows.Add(1)
            '                'DataGridView1.Rows.Item(i).Cells(0).Value = ""
            '                DataGridView1.Rows.Item(i).Cells(1).Value = .Rows(i).Item("No_Faktur")
            '                DataGridView1.Rows.Item(i).Cells(2).Value = .Rows(i).Item("Kode_Stock_Owner")
            '                DataGridView1.Rows.Item(i).Cells(3).Value = .Rows(i).Item("Kode_Barang")
            '                DataGridView1.Rows.Item(i).Cells(4).Value = .Rows(i).Item("Nama_Barang")
            '                DataGridView1.Rows.Item(i).Cells(5).Value = (Format(.Rows(i).Item("jumlah") - .Rows(i).Item("Jmlh_PR"), "N2"))
            '                DataGridView1.Rows.Item(i).Cells(6).Value = .Rows(i).Item("Satuan")
            '                DataGridView1.Rows.Item(i).Cells(7).Value = .Rows(i).Item("Cost_Center")
            '                DataGridView1.Rows.Item(i).Cells(8).Value = .Rows(i).Item("Gedung")
            '                DataGridView1.Rows.Item(i).Cells(9).Value = .Rows(i).Item("No_Urut")
            '                DataGridView1.Rows.Item(i).Cells(10).Value = .Rows(i).Item("Lokasi")
            '                DataGridView1.Rows.Item(i).Cells(12).Value = .Rows(i).Item("Id_Cost_Center")

            '                If .Rows(i).Item("Kode_Stock_Owner") = "-" Then
            '                    DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightSteelBlue
            '                End If

            '                If General_Class.CekNULL(.Rows(i).Item("Flag_Ajukan")) = "Y" Then
            '                    DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.NavajoWhite
            '                End If

            '                If General_Class.CekNULL(.Rows(i).Item("Alasan_Tolak")) = "" Then
            '                    DataGridView1.Rows.Item(i).Cells(13).Value = "-"
            '                Else
            '                    DataGridView1.Rows.Item(i).Cells(13).Value = .Rows(i).Item("Alasan_Tolak")
            '                End If

            '                If General_Class.CekNULL(.Rows(i).Item("Alasan_Tolak")) <> "" And .Rows(i).Item("Kode_Stock_Owner") = "-" Then
            '                    DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.RosyBrown
            '                End If

            '            Next
            '        Else
            '            CloseConn()
            '            MessageBox.Show("Data tidak ditemuakan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            Exit Sub
            '        End If
            '    End With
            'End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'Cari("Y")



        If asal = "Purchase_Requisition_Barang_Lain" Then

            TabControl1.SelectedIndex = 0


            '   TabControl1.SelectedTab = TabPage1

            Cari("Y")
            Btn_Simpan.Visible = True
            Button1.Visible = True
            Me.Size = New Size(1117, 650)
            DataGridView1.Columns("Column3").Visible = True
            DataGridView3.Size = New Size(1066, 328)
            Label14.Location = New Point(10, 359)
            Label13.Location = New Point(27, 354)
            Label16.Location = New Point(85, 359)
            Label12.Location = New Point(102, 354)
            GroupBox1.Location = New Point(5, 376)

        Else
            Btn_Simpan.Visible = False
            Button1.Visible = False
            Me.Size = New Size(1117, 601)
            DataGridView1.Columns("Column3").Visible = False

            DataGridView3.Size = New Size(1066, 296)
            Label14.Location = New Point(10, 325)
            Label13.Location = New Point(27, 320)
            Label16.Location = New Point(85, 325)
            Label12.Location = New Point(102, 320)
            GroupBox1.Location = New Point(5, 342)

            TabControl1.SelectedIndex = 1
            Cari3("Y")

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        kosong()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        Try
            OpenConn()

            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                Dim xuserid As String = ""
                'If DataGridView1.Rows(i).Cells(0).Value = True And DataGridView1.Rows(i).Cells(5).Value = 0 Then
                If DataGridView1.Rows(i).Cells(0).Value = True Then
                    SQL = "select a.No_Faktur,a.Kode_Stock_Owner, a.Kode_Barang, a.Nama_Barang, a.Jumlah, a.Jmlh_PR, a.Satuan, b.Keterangan as Cost_Center, a.No_Urut, d.Lokasi, a.Flag_Ajukan, d.UserId, "
                    SQL = SQL & "isnull(( select c.Keterangan from N_EMI_Master_Gedung_Barang_Lain c where "
                    SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.ID_Gedung = c.ID_Gedung ), NULL) as Gedung, a.Id_Cost_Center, a.Alasan_Tolak, "
                    SQL = SQL & "isnull((select sum(e.Jumlah) from Barang_Lain_SN e where a.Kode_Perusahaan = e.Kode_Perusahaan and a.Kode_Stock_Owner = e.Kode_Stock_Owner "
                    SQL = SQL & "and a.Kode_Barang = e.Kode_Barang),0) as stock, "
                    SQL = SQL & "isnull((select sum(f.Jumlah) from N_EMI_Keep_Stock_Barang_Lain_Departement f where a.Kode_Perusahaan = f.Kode_Perusahaan and a.Kode_Stock_Owner = f.Kode_Stock_Owner "
                    SQL = SQL & "and a.Kode_Barang = f.Kode_Barang and a.No_Urut = f.Urut_Departement and f.Flag_Selesai_Pengeluaran_Barang is null and f.Status is null),0) as Jumlah_Keep_Stock, "
                    SQL = SQL & "isnull((select sum(f.Jumlah) from N_EMI_Keep_Stock_Barang_Lain_Departement f where a.Kode_Perusahaan = f.Kode_Perusahaan and "
                    SQL = SQL & "a.Kode_Barang = f.Kode_Barang and a.Kode_Stock_Owner = f.Kode_Stock_Owner and f.Flag_Selesai_Pengeluaran_Barang is null and f.Status is null),0) as Jumlah_Keep_Stock_2 "
                    SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a, EMI_Master_Cost_Center b, N_EMI_Purchase_Requisition_Barang_Lain_Departement d "
                    SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and d.Status is null "
                    SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Cost_Center = b.Id_Cost_Center "
                    SQL = SQL & "and a.Flag_Sudah_PR is null and a.Kode_Barang <> '-' and a.No_Urut = '" & DataGridView1.Rows(i).Cells(10).Value & "' "
                    SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = d.No_Faktur and d.Flag_Release = 'Y' and d.Flag_PR is null "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            If DataGridView1.Rows(i).Cells(5).Value = 0 Then
                                If dr("Jumlah") - dr("Jmlh_PR") - dr("Jumlah_Keep_Stock") <> 0 Then
                                    dr.Close()
                                    MessageBox.Show("jumlah yang di input salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                                    CloseConn()
                                    Exit Sub
                                End If
                            End If
                            xuserid = dr("userid")
                        End If
                    End Using

                    'Dim Kategori_Gudang As String = ""
                    ''SQL = "select top(1) kode_kategori_gudang From N_EMI_View_Master_Kategori_Gudang_Binding_Departement_Barang_Lain  "
                    ''SQL = SQL & "where user_id = '" & xuserid & "' and kode_perusahaan = '" & KodePerusahaan & "' "
                    'SQL = "select top(1) kode_kategori_gudang From N_EMI_View_Master_Kategori_Gudang_Binding_Barang_Lain "
                    'SQL = SQL & "where user_id = '" & xuserid & "' and kode_perusahaan = '" & KodePerusahaan & "' "
                    'Using Ds = BindingTrans(SQL)
                    '    With Ds.Tables("MyTable")
                    '        If .Rows.Count <> 0 Then
                    '            For z As Integer = 0 To .Rows.Count - 1
                    '                Kategori_Gudang = .Rows(z).Item("kode_kategori_gudang")
                    '            Next
                    '        End If
                    '    End With
                    'End Using

                    'If Kategori_Gudang <> xCmb_Kategori_Gudang Then
                    '    MessageBox.Show("data tidak bisa ditambahkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    CloseConn()
                    '    Exit Sub
                    'End If

                    Dim CekAksesGudang As Boolean = False

                    SQL = "select b.kode_kategori_gudang from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a, "
                    SQL = SQL & "N_EMI_View_Master_Kategori_Gudang_Binding_Barang_Lain b "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis = b.id_sub_kategori_jenis "
                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and a.No_Urut = '" & DataGridView1.Rows(i).Cells(10).Value & "' and b.user_id = '" & UserID & "' "
                    Using ds = BindingTrans(SQL)


                        If ds.Tables("MyTable").Rows.Count <> 0 Then
                            For indexCekGudang As Integer = 0 To ds.Tables("MyTable").Rows.Count - 1
                                If ds.Tables("MyTable").Rows(indexCekGudang).Item("kode_kategori_gudang") = xCmb_Kategori_Gudang Then
                                    CekAksesGudang = True
                                    Exit For
                                End If
                            Next
                        Else

                            MessageBox.Show("anda tidak memiliki akses untuk melakukan purchase requisition barang ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                            CloseConn()
                            Exit Sub
                        End If

                        If CekAksesGudang = False Then

                            MessageBox.Show("kategori gudang harus sama !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            CloseConn()
                            Exit Sub

                        End If



                        'If Not dr.Read Then
                        '    dr.Close()
                        '    MessageBox.Show("anda tidak memiliki akses untuk melakukan purchase requisition barang ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    CloseTrans()
                        '    CloseConn()
                        '    Exit Sub
                        'Else
                        '    If dr("kode_kategori_gudang") <> xCmb_Kategori_Gudang Then
                        '        dr.Close()
                        '        MessageBox.Show("kategori gudang harus sama !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '        CloseTrans()
                        '        CloseConn()
                        '        Exit Sub
                        '    End If
                        'End If
                    End Using
                End If

            Next



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Dim list_urut As String = ""
        Dim flag_add As String = "T"
        For Each row As DataGridViewRow In DataGridView1.Rows
            ' Pastikan bukan baris baru (yang kosong di bawah)
            If Not row.IsNewRow Then
                ' Cek apakah checkbox (kolom 0) dicentang
                If Convert.ToBoolean(row.Cells(0).Value) = True Then
                    ' Ambil nilai dari kolom ke-1 sebagai nomor faktur
                    list_urut &= "'" & row.Cells(10).Value.ToString().Trim() & "', "
                    flag_add = "Y"

                    If Convert.ToString(row.Cells(2).Value).Trim() = "-" Then
                        MessageBox.Show("Data harus ada kode barang (kode barang tidak boleh '-' !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If

            End If
        Next

        If flag_add = "T" Then
            MessageBox.Show("barang yang mau dipurchaseing requisition belum di pilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        ' Hapus koma terakhir
        If list_urut.EndsWith(", ") Then
            list_urut = list_urut.Substring(0, list_urut.Length - 2)
        End If

        Purchase_Requisition_Barang_Lain.Label3.Text = list_urut
        Purchase_Requisition_Barang_Lain.cari_pr_departement()
        Me.Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CbTanggal.CheckedChanged
        If CbTanggal.Checked = True Then
            cbHariIni.Checked = False
            DateTimePicker1.Enabled = True
            DateTimePicker2.Enabled = True
        Else
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs)
        If CheckBox2.Checked = True Then
            CmbSatuan_Kolom.Enabled = True
            CmbSatuan_Kolom.SelectedIndex = -1
            TxtSatuan_Value.Enabled = True
            TxtSatuan_Value.Text = ""
        Else
            CmbSatuan_Kolom.Enabled = False
            CmbSatuan_Kolom.SelectedIndex = -1
            TxtSatuan_Value.Enabled = False
            TxtSatuan_Value.Text = ""
        End If
    End Sub

    Public Sub BtnCari_Click(sender As Object, e As EventArgs) Handles BtnCari.Click
        If CbTanggal.Checked = True Then
            If DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show("Tanggal mulai tidak boleh lebih dari tanggal selesai . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information)
                DateTimePicker1.Focus() : Exit Sub
            End If
        ElseIf CheckBox2.Checked = True Then
            If CmbSatuan_Kolom.SelectedIndex = -1 Then
                MessageBox.Show("Parameter filter harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CmbSatuan_Kolom.Focus() : Exit Sub
            ElseIf TxtSatuan_Value.Text.Trim.Length = 0 And CmbSatuan_Kolom.Text <> "Barang dalam pengajuan" Then
                MessageBox.Show("Value filter harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information)
                TxtSatuan_Value.Focus() : Exit Sub
            End If


        End If

        If asal = "Purchase_Requisition_Barang_Lain" Then
            Cari("T")
        Else
            Cari2("T")
        End If

        '  Cari2("T")

        'Try
        '    OpenConn()

        '    DataGridView1.Rows.Clear()

        '    SQL = "select a.No_Faktur,a.Kode_Stock_Owner, a.Kode_Barang, a.Nama_Barang, a.Jumlah, a.Jmlh_PR, a.Satuan, b.Keterangan as Cost_Center, a.No_Urut, d.Lokasi, a.Flag_Ajukan, "
        '    SQL = SQL & "isnull(( select c.Keterangan from N_EMI_Master_Gedung_Barang_Lain c where "
        '    SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.ID_Gedung = c.ID_Gedung ), NULL) as Gedung, a.Id_Cost_Center, a.Alasan_Tolak "
        '    SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a, EMI_Master_Cost_Center b, N_EMI_Purchase_Requisition_Barang_Lain_Departement d "
        '    SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Cost_Center = b.Id_Cost_Center "
        '    SQL = SQL & "and a.Flag_Sudah_PR is null and a.Kode_Barang <> '-' "
        '    SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = d.No_Faktur and d.Flag_Release = 'Y' and d.Flag_PR is null "

        '    If CheckBox1.Checked Then
        '        'Pasang And
        '        If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

        '        SQL = SQL & "d.Tanggal between '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
        '    End If

        '    If CheckBox2.Checked Then
        '        'Pasang And
        '        If CmbSatuan_Kolom.Text = "Barang dalam pengajuan" Then
        '            If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

        '            SQL = SQL & arrcari.Item(CmbSatuan_Kolom.SelectedIndex) & " = 'Y' "
        '        Else
        '            If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

        '            SQL = SQL & arrcari.Item(CmbSatuan_Kolom.SelectedIndex) & " like '%" & TxtSatuan_Value.Text & "%' "
        '        End If

        '    End If

        '    If CheckBox3.Checked Then
        '        'Pasang And
        '        If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

        '        SQL = SQL & " d.tanggal between '"
        '        SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
        '    End If

        '    SQL = SQL & "order by a.No_Faktur "
        '    Using Ds = BindingTrans(SQL)
        '        With Ds.Tables("MyTable")
        '            If .Rows.Count <> 0 Then
        '                For i As Integer = 0 To .Rows.Count - 1
        '                    DataGridView1.Rows.Add(1)
        '                    'DataGridView1.Rows.Item(i).Cells(0).Value = ""
        '                    DataGridView1.Rows.Item(i).Cells(1).Value = .Rows(i).Item("No_Faktur")
        '                    DataGridView1.Rows.Item(i).Cells(2).Value = .Rows(i).Item("Kode_Stock_Owner")
        '                    DataGridView1.Rows.Item(i).Cells(3).Value = .Rows(i).Item("Kode_Barang")
        '                    DataGridView1.Rows.Item(i).Cells(4).Value = .Rows(i).Item("Nama_Barang")
        '                    DataGridView1.Rows.Item(i).Cells(5).Value = (Format(.Rows(i).Item("jumlah") - .Rows(i).Item("Jmlh_PR"), "N2"))
        '                    DataGridView1.Rows.Item(i).Cells(6).Value = .Rows(i).Item("Satuan")
        '                    DataGridView1.Rows.Item(i).Cells(7).Value = .Rows(i).Item("Cost_Center")
        '                    DataGridView1.Rows.Item(i).Cells(8).Value = .Rows(i).Item("Gedung")
        '                    DataGridView1.Rows.Item(i).Cells(9).Value = .Rows(i).Item("No_Urut")
        '                    DataGridView1.Rows.Item(i).Cells(10).Value = .Rows(i).Item("Lokasi")
        '                    DataGridView1.Rows.Item(i).Cells(12).Value = .Rows(i).Item("Id_Cost_Center")

        '                    If .Rows(i).Item("Kode_Stock_Owner") = "-" Then
        '                        DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightSteelBlue
        '                    End If

        '                    If General_Class.CekNULL(.Rows(i).Item("Flag_Ajukan")) = "Y" Then
        '                        DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.NavajoWhite
        '                    End If

        '                    If General_Class.CekNULL(.Rows(i).Item("Alasan_Tolak")) = "" Then
        '                        DataGridView1.Rows.Item(i).Cells(13).Value = "-"
        '                    Else
        '                        DataGridView1.Rows.Item(i).Cells(13).Value = .Rows(i).Item("Alasan_Tolak")
        '                    End If

        '                    If General_Class.CekNULL(.Rows(i).Item("Alasan_Tolak")) <> "" And .Rows(i).Item("Kode_Stock_Owner") = "-" Then
        '                        DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.RosyBrown
        '                    End If
        '                Next
        '            Else
        '                CloseConn()
        '                MessageBox.Show("Data tidak ditemuakan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Exit Sub
        '            End If
        '        End With
        '    End Using

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        'If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedCells.Count = 0 Then
        '    Exit Sub
        'End If

        'Dim currentRow = DataGridView1.CurrentRow.Index
        'Dim currentCell = DataGridView1.CurrentCellAddress.X

        'If e.KeyCode = Keys.F2 Then
        '    If DataGridView1.CurrentRow.Cells(3).Value = "-" Then
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.filter_tambahan = "and c.Kode_Stock_Owner = '" & DataGridView1.CurrentRow.Cells(10).Value & "'"
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.asal = Jenis
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.asal = Jenis
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.xurut_departement = DataGridView1.CurrentRow.Cells(9).Value
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_KodeBarang.Visible = True
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_Satuan.Visible = True
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_NamaBarang.Visible = True
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.LblPilihBarang_NamaBarang.Visible = True
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.LblPilihBarang_KodeBarang.Visible = True

        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_CostCenter.Enabled = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_KdGedung.Enabled = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Gedung.Enabled = False

        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_CostCenter.Text = DataGridView1.CurrentRow.Cells(7).Value
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Gedung.Text = DataGridView1.CurrentRow.Cells(8).Value
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lv_CostCenter.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lv_Gedung.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Size = New Size(593, 355)

        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lbl_PR.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lbl_Order.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lbl_Sisa.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_PR.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Order.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Sisa.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.ShowDialog()
        '    End If

        'End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If DataGridView1.Columns(e.ColumnIndex).Name = "btnView1" Then

            ' pastikan bukan header
            If e.RowIndex < 0 Then Exit Sub

            ' If DataGridView1.Rows.Count - 1 = e.RowIndex Then Exit Sub


            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)


            ' ambil value dari kolom mana pun
            ' Dim id As String = row.Cells(0).Value.ToString()
            Dim no_pr As String = row.Cells(21).Value.ToString()


            Try
                OpenConn()

                Dim pathFile As String
                Dim containerFile As String

                SQL = "select b.Path_File, b.container_file From N_EMI_Purchase_requisition_barang_lain_departement a,  "
                SQL = SQL & "N_EMI_Purchase_Requisition_Barang_Lain_Departement_Attachment b "
                SQL = SQL & "where Flag_Pra_Release = 'Y' and Flag_Release = 'Y' "
                SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.no_faktur = b.No_Faktur "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & no_pr & "' "
                SQL = SQL & " and a.Status is null and b.Status is null "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        pathFile = General_Class.CekNULL(Dr("path_file"))
                        containerFile = General_Class.CekNULL(Dr("container_file"))
                    Else
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Tidak ada file yang di upload!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                'Dim urlPdf As String = AzureHelper_EMI.DownloadFromAzure(containerFile, pathFile)

                'If urlPdf = "" Then Exit Sub

                'Using sfd As New SaveFileDialog()
                '    sfd.Filter = "PDF Files (*.pdf)|*.pdf"
                '    sfd.FileName = Path.GetFileName(pathFile)

                '    If sfd.ShowDialog() = DialogResult.OK Then
                '        Using wc As New Net.WebClient()
                '            wc.DownloadFile(urlPdf, sfd.FileName)
                '        End Using

                '        MessageBox.Show("File berhasil disimpan.", "Sukses",
                '                MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    End If
                'End Using

                Dim result = AzureHelper_EMI.DownloadFromAzure(containerFile, pathFile)

                If Not result.Success Then
                    MessageBox.Show(
                    If(String.IsNullOrWhiteSpace(result.Message),
                       "File pdf tidak ditemukan atau gagal diambil.",
                       result.Message),
                    "Informasi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )
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
                Exit Sub
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If


        If e.ColumnIndex = DataGridView1.Columns(12).Index AndAlso e.RowIndex >= 0 Then
            If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedCells.Count = 0 Then
                Exit Sub
            End If

            Dim currentRow = DataGridView1.CurrentRow.Index
            Dim currentCell = DataGridView1.CurrentCellAddress.X





            If DataGridView1.CurrentRow.Cells(3).Value = "-" And (DataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.LightSteelBlue Or DataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.RosyBrown) Then
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.filter_tambahan = "and c.Kode_Stock_Owner = '" & DataGridView1.CurrentRow.Cells(11).Value & "'"
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.asal = Jenis
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.asal = Jenis
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.xurut_departement = DataGridView1.CurrentRow.Cells(10).Value
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_KodeBarang.Visible = True
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_Satuan.Visible = True
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_NamaBarang.Visible = True
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.LblPilihBarang_NamaBarang.Visible = True
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.LblPilihBarang_KodeBarang.Visible = True

                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.CheckBox1.Checked = True
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_NamaBarang.Text = DataGridView1.CurrentRow.Cells(4).Value
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TextBox1.Text = DataGridView1.CurrentRow.Cells(13).Value
                Try
                    OpenConn()

                    N_EMI_SD_Tambah_PR_Barang_Lain_Departement.CmbSub.Items.Clear() : N_EMI_SD_Tambah_PR_Barang_Lain_Departement.arridSub.Clear()
                    SQL = "select b.Keterangan as Kategori_Jenis, a.Keterangan as Sub_Kategori_Jenis, a.Id_Sub_Kategori_Jenis "
                    SQL = SQL & "from N_EMI_Master_Sub_Kategori_Jenis a, N_EMI_Master_Kategori_Jenis b "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Jenis = b.Id_Kategori_Jenis "
                    SQL = SQL & "and a.Kode_Perusahaan =  '" & KodePerusahaan & "' order by b.Keterangan, a.Keterangan "
                    Using dr = OpenTrans(SQL)
                        Do While dr.Read
                            N_EMI_SD_Tambah_PR_Barang_Lain_Departement.CmbSub.Items.Add(dr("Sub_Kategori_Jenis") & " - " & dr("Kategori_Jenis"))
                            N_EMI_SD_Tambah_PR_Barang_Lain_Departement.arridSub.Add(dr("Id_Sub_Kategori_Jenis"))
                        Loop
                    End Using

                    CloseConn()
                Catch ex As Exception
                    CloseConn()
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try

                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.CmbSub.Text = DataGridView1.CurrentRow.Cells(16).Value

                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_CostCenter.Enabled = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_KdGedung.Enabled = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Gedung.Enabled = False

                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_CostCenter.Text = DataGridView1.CurrentRow.Cells(7).Value
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Id_CostCenter.Text = DataGridView1.CurrentRow.Cells(12).Value
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Gedung.Text = DataGridView1.CurrentRow.Cells(8).Value

                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.SO_Kategori_Gudang_Pilih = xCmb_Kategori_Gudang


                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lv_CostCenter.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lv_Gedung.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Size = New Size(595, 409)

                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lbl_PR.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lbl_Order.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lbl_Sisa.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_PR.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Order.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Sisa.Visible = False
                N_EMI_SD_Tambah_PR_Barang_Lain_Departement.ShowDialog()
            End If

        End If


        'If e.ColumnIndex = DataGridView1.Columns(12).Index AndAlso e.RowIndex >= 0 Then
        '    If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedCells.Count = 0 Then
        '        Exit Sub
        '    End If

        '    Dim currentRow = DataGridView1.CurrentRow.Index
        '    Dim currentCell = DataGridView1.CurrentCellAddress.X

        '    If DataGridView1.CurrentRow.Cells(3).Value = "-" And (DataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.LightSteelBlue Or DataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.RosyBrown) Then
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.filter_tambahan = "and c.Kode_Stock_Owner = '" & DataGridView1.CurrentRow.Cells(10).Value & "'"
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.asal = Jenis
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.asal = Jenis
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.xurut_departement = DataGridView1.CurrentRow.Cells(10).Value
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_KodeBarang.Visible = True
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_Satuan.Visible = True
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_NamaBarang.Visible = True
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.LblPilihBarang_NamaBarang.Visible = True
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.LblPilihBarang_KodeBarang.Visible = True
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.CheckBox1.Checked = True
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TxtPilihBarang_NamaBarang.Text = DataGridView1.CurrentRow.Cells(4).Value
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.TextBox1.Text = DataGridView1.CurrentRow.Cells(13).Value

        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_CostCenter.Enabled = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_KdGedung.Enabled = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Gedung.Enabled = False

        '        'N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_CostCenter.Text = DataGridView1.CurrentRow.Cells(7).Value
        '        'N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Id_CostCenter.Text = DataGridView1.CurrentRow.Cells(12).Value
        '        'N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Gedung.Text = DataGridView1.CurrentRow.Cells(8).Value
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lv_CostCenter.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lv_Gedung.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Size = New Size(593, 355)

        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lbl_PR.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lbl_Order.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Lbl_Sisa.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_PR.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Order.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.Txt_Sisa.Visible = False
        '        N_EMI_SD_Tambah_PR_Barang_Lain_Departement.ShowDialog()
        '    End If

        'End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs)
        If cbHariIni.Checked = True Then
            CbTanggal.Checked = False
            BtnCari_Click(cbHariIni, e)
        End If
    End Sub

    Private Sub N_EMI_Display_Request_Departement_Barang_Lain_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub



    Private Sub AmbilStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AmbilStockToolStripMenuItem.Click
        If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedCells.Count = 0 Then
            Exit Sub
        End If

        Dim currentRow = DataGridView1.CurrentRow.Index
        Dim currentCell = DataGridView1.CurrentCellAddress.X

        Try
            OpenConn()

            Dim gudangKeepStok As String = ""

            SQL = "select Kode_Stock_Owner_Gudang From N_EMI_Master_Kategori_Gudang_Barang_Lain "
            SQL = SQL & "where kode_kategori_gudang = '" & xCmb_Kategori_Gudang & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("kode_stock_owner_gudang")) = "" Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Kategori gudang tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        gudangKeepStok = Dr("Kode_Stock_Owner_Gudang")
                    End If
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Kategori gudang tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            If DataGridView1.CurrentRow.Cells(6).Value <> 0 Then
                N_EMI_SD_Keep_Stock_Barang_Lain.TextBox10.Text = DataGridView1.CurrentRow.Cells(1).Value
                N_EMI_SD_Keep_Stock_Barang_Lain.TextBox2.Text = DataGridView1.CurrentRow.Cells(3).Value
                N_EMI_SD_Keep_Stock_Barang_Lain.TextBox3.Text = DataGridView1.CurrentRow.Cells(4).Value
                N_EMI_SD_Keep_Stock_Barang_Lain.TextBox4.Text = DataGridView1.CurrentRow.Cells(6).Value
                N_EMI_SD_Keep_Stock_Barang_Lain.TextBox5.Text = ""
                N_EMI_SD_Keep_Stock_Barang_Lain.TextBox6.Text = DataGridView1.CurrentRow.Cells(10).Value
                N_EMI_SD_Keep_Stock_Barang_Lain.TextBox7.Text = DataGridView1.CurrentRow.Cells(7).Value
                N_EMI_SD_Keep_Stock_Barang_Lain.TextBox8.Text = gudangKeepStok
                N_EMI_SD_Keep_Stock_Barang_Lain.TextBox9.Text = DataGridView1.CurrentRow.Cells(5).Value
                N_EMI_SD_Keep_Stock_Barang_Lain.TextBox5.Focus()
                N_EMI_SD_Keep_Stock_Barang_Lain.ShowDialog()
            Else
                CloseConn()
                MessageBox.Show("barang ini tidak memiliki stock . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        'If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedCells.Count = 0 Then
        '    Exit Sub
        'End If

        'Dim currentRow = DataGridView1.CurrentRow.Index
        'Dim currentCell = DataGridView1.CurrentCellAddress.X

        'If DataGridView1.CurrentRow.Cells(6).Value <> 0 Then
        '    N_EMI_SD_Keep_Stock_Barang_Lain.TextBox10.Text = DataGridView1.CurrentRow.Cells(1).Value
        '    N_EMI_SD_Keep_Stock_Barang_Lain.TextBox2.Text = DataGridView1.CurrentRow.Cells(3).Value
        '    N_EMI_SD_Keep_Stock_Barang_Lain.TextBox3.Text = DataGridView1.CurrentRow.Cells(4).Value
        '    N_EMI_SD_Keep_Stock_Barang_Lain.TextBox4.Text = DataGridView1.CurrentRow.Cells(6).Value
        '    N_EMI_SD_Keep_Stock_Barang_Lain.TextBox5.Text = ""
        '    N_EMI_SD_Keep_Stock_Barang_Lain.TextBox6.Text = DataGridView1.CurrentRow.Cells(10).Value
        '    N_EMI_SD_Keep_Stock_Barang_Lain.TextBox7.Text = DataGridView1.CurrentRow.Cells(7).Value
        '    N_EMI_SD_Keep_Stock_Barang_Lain.TextBox8.Text = DataGridView1.CurrentRow.Cells(2).Value
        '    N_EMI_SD_Keep_Stock_Barang_Lain.TextBox9.Text = DataGridView1.CurrentRow.Cells(5).Value
        '    N_EMI_SD_Keep_Stock_Barang_Lain.TextBox5.Focus()
        '    N_EMI_SD_Keep_Stock_Barang_Lain.ShowDialog()
        'Else
        '    MessageBox.Show("barang ini tidak memiliki stock . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Exit Sub
        'End If

    End Sub

    Private Sub DataGridView3_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView3.KeyDown
        If e.KeyCode = Keys.F1 Then

            If TabControl1.SelectedIndex = 1 Then
                N_EMI_Master_Kategori_Jenis_Sub_Kategori.Asal_proses = "pengajuan_barang_baru"
                N_EMI_Master_Kategori_Jenis_Sub_Kategori.ShowDialog()
            End If
        End If
    End Sub

    Public Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If CheckBox5.Checked = True Then
            If DateTimePicker3.Value > DateTimePicker4.Value Then
                MessageBox.Show("Tanggal mulai tidak boleh lebih dari tanggal selesai . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information)
                DateTimePicker3.Focus() : Exit Sub
            End If
        ElseIf CheckBox6.Checked = True Then
            If ComboBox2.SelectedIndex = -1 Then
                MessageBox.Show("Parameter filter harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ComboBox2.Focus() : Exit Sub
            ElseIf TextBox1.Text.Trim.Length = 0 And CmbSatuan_Kolom.Text <> "Barang dalam pengajuan" Then
                MessageBox.Show("Value filter harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information)
                TextBox1.Focus() : Exit Sub
            End If
        End If

        Cari3("T")
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged

        If CheckBox4.Checked = True Then
            CheckBox5.Checked = False
            Button2_Click(CheckBox4, e)
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked = True Then
            CheckBox4.Checked = False
            DateTimePicker3.Enabled = True
            DateTimePicker4.Enabled = True
        Else
            DateTimePicker3.Enabled = False
            DateTimePicker4.Enabled = False
        End If
    End Sub

    Private Sub TabControl1_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles TabControl1.Selecting

        If asal <> "Purchase_Requisition_Barang_Lain" Then
            If e.TabPageIndex < 1 Then
                e.Cancel = True
            End If
        Else
            If e.TabPageIndex < 0 Then
                e.Cancel = True
            End If
        End If

    End Sub

    Private Sub RejectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RejectToolStripMenuItem.Click
        If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedCells.Count = 0 Then
            Exit Sub
        End If

        Dim currentRow = DataGridView1.CurrentRow.Index
        Dim currentCell = DataGridView1.CurrentCellAddress.X

        get_jam()

        Try

            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("Reject_Purchase_Requisition_Departement") = "T" Then
                CloseConn()
                MessageBox.Show("anda tidak memiliki akses ! !")
                Exit Sub
            End If

            SQL = "select a.No_Faktur, a.Flag_Reject, "
            SQL = SQL & "isnull((select sum(f.Jumlah) from N_EMI_Keep_Stock_Barang_Lain_Departement f where "
            SQL = SQL & "a.Kode_Perusahaan = f.Kode_Perusahaan and a.Kode_Barang = f.Kode_Barang and a.Kode_Stock_Owner = f.Kode_Stock_Owner "
            SQL = SQL & "and f.Flag_Selesai_Pengeluaran_Barang is null and f.Status is null and f.Urut_Departement = a.No_Urut),0) as Jumlah_Keep_Stock_2, "
            SQL = SQL & "isnull((select sum(f.Jumlah) from EMI_Purchase_Requisition_Barang_Lain_Detail f, EMI_Purchase_Requisition_Barang_Lain g where "
            SQL = SQL & "a.Kode_Perusahaan = f.Kode_Perusahaan and a.Kode_Barang = f.Kode_Barang and a.Kode_Stock_Owner = f.Kode_Stock_Owner "
            SQL = SQL & "and g.Status is null and f.Urut_Departement = a.No_Urut and f.No_Faktur = g.No_Faktur),0) as Jumlah_PR "
            SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a where  "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Urut = '" & DataGridView1.CurrentRow.Cells(10).Value & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Flag_Reject")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Barang sudah pernah di reject", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(Dr("Jumlah_Keep_Stock_2")) <> 0 Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Barang tidak bisa di reject, karena sudah ambil stock barang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(Dr("Jumlah_PR")) <> 0 Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Barang tidak bisa di reject, karena sudah Purchasing Requisition warehouse ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Data PR tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail set "
            SQL = SQL & "Flag_Reject = 'Y', Tgl_Reject = '" & Format(tgl_skg, "yyyy-MM-dd") & "' , "
            SQL = SQL & "Jam_Reject = '" & Format(tgl_skg, "HH:mm:ss") & "', User_Reject = '" & UserID & "' "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Urut = '" & DataGridView1.CurrentRow.Cells(10).Value & "'"
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Berhasil reject", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kosong()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked = True Then
            ComboBox2.Enabled = True
            ComboBox2.SelectedIndex = -1
            TextBox1.Enabled = True
            TextBox1.Text = ""
        Else
            ComboBox2.Enabled = False
            ComboBox2.SelectedIndex = -1
            TextBox1.Enabled = False
            TextBox1.Text = ""
        End If
    End Sub
End Class