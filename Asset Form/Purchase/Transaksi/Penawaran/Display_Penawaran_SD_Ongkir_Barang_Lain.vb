'Imports System.Windows.Forms.VisualStyles.VisualStyleElement
'Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button


Imports System.Reflection

Public Class Display_Penawaran_SD_Ongkir_Barang_Lain
    '    Dim arrcari As New ArrayList
    '    Dim Jenis = "Master_Jenis_Hewan"
    '    Private Sub kosong()
    '        TextBox1.Text = ""
    '        TextBox2.Text = ""



    '        ComboBox1.Items.Clear() : arrcari.Clear()
    '        ComboBox1.Items.Add(Base_Language.Lang_Jenis_Hewan_Kode) : arrcari.Add("kode_jenis_hewan")
    '        ComboBox1.Items.Add(Base_Language.Lang_Jenis_Hewan_Keterangan) : arrcari.Add("keterangan")
    '        TextBox3.Text = ""

    '        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
    '        Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
    '        Btn_Cari.Text = Base_Language.Lang_Global_Cari
    '        Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
    '        Btn_Simpan.Tag = "&Simpan"
    '        Btn_Hapus.Enabled = False

    '    End Sub

    '    Private Sub Cari(ByVal semua As String)
    '        Try

    '            OpenConn()

    '            ListView1.Items.Clear()
    '            SQL = "Select kode_jenis_hewan, keterangan From emi_jenis_hewan where kode_perusahaan = '" & KodePerusahaan & "' "
    '            If semua = "T" Then
    '                SQL = SQL & "and " & arrcari.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
    '                SQL = SQL & "order by " & arrcari.Item(ComboBox1.SelectedIndex) & " "
    '            Else
    '                SQL = SQL & "order by nama"
    '            End If
    '            Using dr = OpenTrans(SQL)
    '                Do While dr.Read
    '                    Dim Lvw As ListViewItem
    '                    Lvw = ListView1.Items.Add(dr("kode_jenis_hewan"))
    '                    Lvw.SubItems.Add(dr("keterangan"))
    '                Loop
    '            End Using

    '            CloseConn()

    '        Catch ex As Exception
    '            CloseConn()
    '            MessageBox.Show(ex.Message)
    '            Exit Sub
    '        End Try
    '    End Sub
    '    Private Sub Master_Jenis_Hewan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
    '        My.Application.ChangeCulture("en-us")
    '        My.Application.ChangeUICulture("en-us")
    '    End Sub

    Private Sub Master_Jenis_Hewan_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Btn_Refresh_Click(Me, Nothing)



    End Sub

    Private Sub kosong()

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Display_Penawaran")
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



        DataGridView4.Rows.Clear()
        DataGridView4.Columns(0).HeaderText = Base_Language.lang_global_keterangan
        DataGridView4.Columns(1).HeaderText = Base_Language.Lang_Display_Penawaran_Media_Kirim
        DataGridView4.Columns(2).HeaderText = Base_Language.Lang_Global_Lokasi_Awal
        DataGridView4.Columns(3).HeaderText = Base_Language.Lang_Global_Lokasi_Tujuan
        DataGridView4.Columns(4).HeaderText = Base_Language.Lang_Display_Penawaran_Provinsi_Awal
        DataGridView4.Columns(5).HeaderText = Base_Language.Lang_Display_Penawaran_Provinsi_Tujuan
        DataGridView4.Columns(6).HeaderText = Base_Language.Lang_Display_Penawaran_Kota_Asal
        DataGridView4.Columns(7).HeaderText = Base_Language.Lang_Display_Penawaran_Kota_Tujuan
        DataGridView4.Columns(8).HeaderText = Base_Language.Lang_Display_Penawaran_Kecamatan_Asal
        DataGridView4.Columns(9).HeaderText = Base_Language.Lang_Display_Penawaran_Kecamatan_Tujuan
        DataGridView4.Columns(10).HeaderText = Base_Language.Lang_Display_Penawaran_Kelurahan_Asal
        DataGridView4.Columns(11).HeaderText = Base_Language.Lang_Display_Penawaran_Kelurahan_Tujuan

        Label1.Text = Base_Language.Lang_Display_Penawaran_Judul

        tampil_pengiriman()
    End Sub

    Private Sub tampil_pengiriman()
        Try
            OpenConn()

            DataGridView4.Rows.Clear()
            Dim index As Integer = 0
            SQL = ";with cte as( "
            SQL = SQL & "select  c.id_cara_kirim,c.id_media_kirim,c.Keterangan as media_kirim,a.Kode_Stock_Owner as Lokasi_Awal,b.Alamat_Penerima as Lokasi_Tujuan, "
            SQL = SQL & "isnull((select x.id_provinsi from tbl_provinsi x where a.id_provinsi = x.id_provinsi ), null) as id_Provinsi_Awal, "
            SQL = SQL & "isnull((select x.nama_provinsi from tbl_provinsi x where a.id_provinsi = x.id_provinsi ), null) as Provinsi_Awal, "
            SQL = SQL & "isnull((select x.id_provinsi from tbl_provinsi x where b.id_provinsi = x.id_provinsi ), null) as id_Provinsi_Tujuan,"
            SQL = SQL & "isnull((select x.nama_provinsi from tbl_provinsi x where b.id_provinsi = x.id_provinsi ), null) as Provinsi_Tujuan,"
            SQL = SQL & "isnull((select x.id_kabupaten_kota from tbl_kabupaten_kota x where a.id_kabupaten_kota = x.id_kabupaten_kota ), null) as id_kota_Awal,"
            SQL = SQL & "isnull((select x.nama_kabupaten_kota from tbl_kabupaten_kota x where a.id_kabupaten_kota = x.id_kabupaten_kota ), null) as kota_Awal,"
            SQL = SQL & "isnull((select x.id_kabupaten_kota from tbl_kabupaten_kota x where b.id_kabupaten_kota = x.id_kabupaten_kota ), null) as id_Kota_Tujuan,  "
            SQL = SQL & "isnull((select x.nama_kabupaten_kota from tbl_kabupaten_kota x where b.id_kabupaten_kota = x.id_kabupaten_kota ), null) as Kota_Tujuan,  "
            SQL = SQL & "isnull((select x.id_kecamatan from tbl_kecamatan x where a.id_kecamatan = x.id_kecamatan ), null) as id_Kecamatan_Awal, "
            SQL = SQL & "isnull((select x.nama_kecamatan from tbl_kecamatan x where a.id_kecamatan = x.id_kecamatan ), null) as Kecamatan_Awal, "
            SQL = SQL & "isnull((select x.id_kecamatan from tbl_kecamatan x where b.id_kecamatan = x.id_kecamatan ), null) as Id_Kecamatan_Tujuan, "
            SQL = SQL & "isnull((select x.nama_kecamatan from tbl_kecamatan x where b.id_kecamatan = x.id_kecamatan ), null) as Kecamatan_Tujuan, "
            SQL = SQL & "isnull((select x.id_kelurahan from tbl_kelurahan x where a.id_kelurahan = x.id_kelurahan), null) as Id_Kelurahan_Awal, "
            SQL = SQL & "isnull((select x.nama_kelurahan from tbl_kelurahan x where a.id_kelurahan = x.id_kelurahan), null) as Kelurahan_Awal, "
            SQL = SQL & "isnull((select x.id_kelurahan from tbl_kelurahan x where  b.id_kelurahan = x.id_kelurahan ), null) as Id_Kelurahan_Tujuan,"
            SQL = SQL & "isnull((select x.nama_kelurahan from tbl_kelurahan x where  b.id_kelurahan = x.id_kelurahan ), null) as Kelurahan_Tujuan, b.Kode_Customer "
            SQL = SQL & "from Stock_Owner_Gudang_Lain a, Emi_Customer_Gudang b, EMI_Media_Kirim c where  b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and b.Id_Media_Kirim = c.Id_Media_Kirim ) "

            SQL = SQL & "select id_cara_kirim,id_media_kirim,id_Provinsi_Awal,id_Provinsi_Tujuan,id_kota_Awal, id_kota_tujuan,id_Kecamatan_Awal,Id_Kecamatan_Tujuan,Id_Kelurahan_Awal,Id_Kelurahan_Tujuan ,  media_kirim,Lokasi_Awal,Lokasi_Tujuan,Provinsi_Awal,Provinsi_Tujuan,kota_Awal,Kota_Tujuan,Kecamatan_Awal,Kecamatan_Tujuan,Kelurahan_Awal,Kelurahan_Tujuan,Kode_Customer from cte "
            SQL = SQL & "group by id_cara_kirim,id_media_kirim,id_Provinsi_Awal,id_Provinsi_Tujuan,id_kota_Awal, id_kota_tujuan,id_Kecamatan_Awal,Id_Kecamatan_Tujuan,Id_Kelurahan_Awal,Id_Kelurahan_Tujuan , media_kirim,Lokasi_Awal,Lokasi_Tujuan,Provinsi_Awal,Provinsi_Tujuan,kota_Awal,Kota_Tujuan,Kecamatan_Awal,Kecamatan_Tujuan,Kelurahan_Awal,Kelurahan_Tujuan, Kode_Customer "
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            SQL = "select * from EMI_Master_Ongkir_Lain where kode_perusahaan = '" & KodePerusahaan & "'  "
                            SQL = SQL & "and id_kab_kota_asal = '" & .Rows(i).Item("id_kota_awal") & "' and id_kab_kota_tujuan = '" & .Rows(i).Item("id_kota_tujuan") & "' "
                            SQL = SQL & "and id_provinsi_asal = '" & .Rows(i).Item("id_provinsi_awal") & "'  and id_provinsi_tujuan = '" & .Rows(i).Item("id_provinsi_tujuan") & "' "
                            SQL = SQL & "and id_kecamatan_asal = '" & .Rows(i).Item("id_kecamatan_awal") & "' and id_kecamatan_tujuan = '" & .Rows(i).Item("id_kecamatan_tujuan") & "'  "
                            SQL = SQL & "and id_kelurahan_asal = '" & .Rows(i).Item("id_kelurahan_awal") & "' and id_kelurahan_tujuan = '" & .Rows(i).Item("id_kelurahan_tujuan") & "' "
                            SQL = SQL & "and id_media_kirim = '" & .Rows(i).Item("id_media_kirim") & "' and Selesai Is null And '" & Format(tgl_skg, "yyyy-MM-dd") & "' between "
                            SQL = SQL & "Tgl_Penawaran_Hrg And Periode_Akhir_Penawaran "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then

                                    Dim rentang As Integer = DateDiff(DateInterval.Day, tgl_skg, Dr("Periode_Akhir_Penawaran"))

                                    If rentang <= 90 Then
                                        DataGridView4.Rows.Add(1)
                                        DataGridView4.Rows(index).Cells(0).Value = "Penawaran berakhir dalam waktu " & rentang & " hari"
                                        DataGridView4.Rows(index).Cells(1).Value = .Rows(i).Item("media_kirim")
                                        DataGridView4.Rows(index).Cells(2).Value = .Rows(i).Item("lokasi_awal")
                                        DataGridView4.Rows(index).Cells(3).Value = .Rows(i).Item("lokasi_tujuan")
                                        DataGridView4.Rows(index).Cells(4).Value = .Rows(i).Item("provinsi_awal")
                                        DataGridView4.Rows(index).Cells(5).Value = .Rows(i).Item("provinsi_tujuan")
                                        DataGridView4.Rows(index).Cells(6).Value = .Rows(i).Item("kota_awal")
                                        DataGridView4.Rows(index).Cells(7).Value = .Rows(i).Item("kota_tujuan")
                                        DataGridView4.Rows(index).Cells(8).Value = .Rows(i).Item("kecamatan_awal")
                                        DataGridView4.Rows(index).Cells(9).Value = .Rows(i).Item("kecamatan_Tujuan")
                                        DataGridView4.Rows(index).Cells(10).Value = .Rows(i).Item("kelurahan_awal")
                                        DataGridView4.Rows(index).Cells(11).Value = .Rows(i).Item("kelurahan_tujuan")

                                        DataGridView4.Rows(index).Cells(12).Value = .Rows(i).Item("id_provinsi_awal")
                                        DataGridView4.Rows(index).Cells(13).Value = .Rows(i).Item("id_provinsi_tujuan")
                                        DataGridView4.Rows(index).Cells(14).Value = .Rows(i).Item("id_kota_awal")
                                        DataGridView4.Rows(index).Cells(15).Value = .Rows(i).Item("id_kota_tujuan")
                                        DataGridView4.Rows(index).Cells(16).Value = .Rows(i).Item("id_kecamatan_awal")
                                        DataGridView4.Rows(index).Cells(17).Value = .Rows(i).Item("id_kecamatan_tujuan")
                                        DataGridView4.Rows(index).Cells(18).Value = .Rows(i).Item("id_kelurahan_awal")
                                        DataGridView4.Rows(index).Cells(19).Value = .Rows(i).Item("id_kelurahan_tujuan")
                                        DataGridView4.Rows(index).Cells(20).Value = .Rows(i).Item("id_cara_kirim")
                                        DataGridView4.Rows(index).Cells(21).Value = .Rows(i).Item("id_media_kirim")
                                        DataGridView4.Rows(index).Cells(0).Style.BackColor = Color.LightGoldenrodYellow
                                        index += 1
                                    End If
                                Else
                                    DataGridView4.Rows.Add(1)
                                    DataGridView4.Rows(index).Cells(0).Value = "Belum Ada Penawaran"
                                    DataGridView4.Rows(index).Cells(1).Value = .Rows(i).Item("media_kirim")
                                    DataGridView4.Rows(index).Cells(2).Value = .Rows(i).Item("lokasi_awal")
                                    DataGridView4.Rows(index).Cells(3).Value = .Rows(i).Item("lokasi_tujuan")
                                    DataGridView4.Rows(index).Cells(4).Value = .Rows(i).Item("provinsi_awal")
                                    DataGridView4.Rows(index).Cells(5).Value = .Rows(i).Item("provinsi_tujuan")
                                    DataGridView4.Rows(index).Cells(6).Value = .Rows(i).Item("kota_awal")
                                    DataGridView4.Rows(index).Cells(7).Value = .Rows(i).Item("kota_tujuan")
                                    DataGridView4.Rows(index).Cells(8).Value = .Rows(i).Item("kecamatan_awal")
                                    DataGridView4.Rows(index).Cells(9).Value = .Rows(i).Item("kecamatan_Tujuan")
                                    DataGridView4.Rows(index).Cells(10).Value = .Rows(i).Item("kelurahan_awal")
                                    DataGridView4.Rows(index).Cells(11).Value = .Rows(i).Item("kelurahan_tujuan")

                                    DataGridView4.Rows(index).Cells(12).Value = .Rows(i).Item("id_provinsi_awal")
                                    DataGridView4.Rows(index).Cells(13).Value = .Rows(i).Item("id_provinsi_tujuan")
                                    DataGridView4.Rows(index).Cells(14).Value = .Rows(i).Item("id_kota_awal")
                                    DataGridView4.Rows(index).Cells(15).Value = .Rows(i).Item("id_kota_tujuan")
                                    DataGridView4.Rows(index).Cells(16).Value = .Rows(i).Item("id_kecamatan_awal")
                                    DataGridView4.Rows(index).Cells(17).Value = .Rows(i).Item("id_kecamatan_tujuan")
                                    DataGridView4.Rows(index).Cells(18).Value = .Rows(i).Item("id_kelurahan_awal")
                                    DataGridView4.Rows(index).Cells(19).Value = .Rows(i).Item("id_kelurahan_tujuan")
                                    DataGridView4.Rows(index).Cells(20).Value = .Rows(i).Item("id_cara_kirim")
                                    DataGridView4.Rows(index).Cells(21).Value = .Rows(i).Item("id_media_kirim")
                                    DataGridView4.Rows(index).Cells(0).Style.BackColor = Color.Coral
                                    index += 1
                                End If
                            End Using

                        Next
                    Else
                        CloseConn()
                        Exit Sub
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
    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles Panel4.Paint

    End Sub

    Private Sub DataGridView4_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView4.CellContentClick

    End Sub

    Private Sub DataGridView4_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView4.DoubleClick
        If DataGridView4.Rows.Count = 0 Then
            Exit Sub
        End If

        For index = 0 To Transaksi_Penawaran_Barang_Lain.arrIdProvAsal.Count - 1
            If Transaksi_Penawaran_Barang_Lain.arrIdProvAsal.Item(index) = DataGridView4.CurrentRow.Cells(12).Value Then
                Transaksi_Penawaran_Barang_Lain.Cmb_ProvAsal.SelectedIndex = index
            End If
        Next
        Transaksi_Penawaran_Barang_Lain.Get_KabupatenKota_Asal()
        For index = 0 To Transaksi_Penawaran_Barang_Lain.arrIdKabKotaAsal.Count - 1
            If Transaksi_Penawaran_Barang_Lain.arrIdKabKotaAsal.Item(index) = DataGridView4.CurrentRow.Cells(14).Value Then
                Transaksi_Penawaran_Barang_Lain.Cmb_KabKotaAwal.SelectedIndex = index
            End If
        Next
        Transaksi_Penawaran_Barang_Lain.Get_Kecamatan_Asal()
        For index = 0 To Transaksi_Penawaran_Barang_Lain.arrIdKecAsal.Count - 1
            If Transaksi_Penawaran_Barang_Lain.arrIdKecAsal.Item(index) = DataGridView4.CurrentRow.Cells(16).Value Then
                Transaksi_Penawaran_Barang_Lain.Cmb_KecAsal.SelectedIndex = index
            End If
        Next
        Transaksi_Penawaran_Barang_Lain.Get_Kelurahan_Asal()
        For index = 0 To Transaksi_Penawaran_Barang_Lain.arrIdKelAsal.Count - 1
            If Transaksi_Penawaran_Barang_Lain.arrIdKelAsal.Item(index) = DataGridView4.CurrentRow.Cells(18).Value Then
                Transaksi_Penawaran_Barang_Lain.Cmb_KelAsal.SelectedIndex = index
            End If
        Next
        '--------------------------------------------------------------
        For index = 0 To Transaksi_Penawaran_Barang_Lain.arrIdProvTujuan.Count - 1
            If Transaksi_Penawaran_Barang_Lain.arrIdProvTujuan.Item(index) = DataGridView4.CurrentRow.Cells(13).Value Then
                Transaksi_Penawaran_Barang_Lain.Cmb_ProvTujuan.SelectedIndex = index
            End If
        Next
        Transaksi_Penawaran_Barang_Lain.Get_KabupatenKota_Tujuan()
        For index = 0 To Transaksi_Penawaran_Barang_Lain.arrIdKabKotaTujuan.Count - 1
            If Transaksi_Penawaran_Barang_Lain.arrIdKabKotaTujuan.Item(index) = DataGridView4.CurrentRow.Cells(15).Value Then
                Transaksi_Penawaran_Barang_Lain.Cmb_KabKotaTujuan.SelectedIndex = index
            End If
        Next
        Transaksi_Penawaran_Barang_Lain.Get_Kecamatan_Tujuan()
        For index = 0 To Transaksi_Penawaran_Barang_Lain.arrIdKecTujuan.Count - 1
            If Transaksi_Penawaran_Barang_Lain.arrIdKecTujuan.Item(index) = DataGridView4.CurrentRow.Cells(17).Value Then
                Transaksi_Penawaran_Barang_Lain.Cmb_KecTujuan.SelectedIndex = index
            End If
        Next
        Transaksi_Penawaran_Barang_Lain.Get_Kelurahan_Tujuan()
        For index = 0 To Transaksi_Penawaran_Barang_Lain.arrIdKelTujuan.Count - 1
            If Transaksi_Penawaran_Barang_Lain.arrIdKelTujuan.Item(index) = DataGridView4.CurrentRow.Cells(19).Value Then
                Transaksi_Penawaran_Barang_Lain.Cmb_KelTujuan.SelectedIndex = index
            End If
        Next

        For index = 0 To Transaksi_Penawaran_Barang_Lain.arrCaraKirim.Count - 1
            If Transaksi_Penawaran_Barang_Lain.arrCaraKirim.Item(index) = DataGridView4.CurrentRow.Cells(20).Value Then
                Transaksi_Penawaran_Barang_Lain.Cmb_CaraKirim.SelectedIndex = index
            End If
        Next
        Transaksi_Penawaran_Barang_Lain.Get_Media_Kirim()
        For index = 0 To Transaksi_Penawaran_Barang_Lain.arrMediaKirim.Count - 1
            If Transaksi_Penawaran_Barang_Lain.arrMediaKirim.Item(index) = DataGridView4.CurrentRow.Cells(21).Value Then
                Transaksi_Penawaran_Barang_Lain.Cmb_MediaKirim.SelectedIndex = index
            End If
        Next
        Me.Close()

    End Sub


    '        My.Application.ChangeCulture("en-us")
    '        My.Application.ChangeUICulture("en-us")

    '        Try
    '            OpenConn()

    '            Base_Language.Get_Languages_Global(Bahasa_Pilihan)

    '            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

    '            Label1.Text = Base_Language.Lang_Jenis_Hewan_Judul
    '            Label2.Text = Base_Language.Lang_Jenis_Hewan_Kode
    '            Label3.Text = Base_Language.Lang_Jenis_Hewan_Keterangan
    '            Label4.Text = Base_Language.Lang_Jenis_Hewan_Kolom

    '            ListView1.Columns.Add(Base_Language.Lang_Jenis_Hewan_Kode, 150, HorizontalAlignment.Left)
    '            ListView1.Columns.Add(Base_Language.Lang_Jenis_Hewan_Keterangan, 725, HorizontalAlignment.Left)
    '            ListView1.View = View.Details

    '            kosong()

    '            CloseConn()
    '        Catch ex As Exception
    '            CloseConn()
    '            MessageBox.Show(ex.Message)
    '            Exit Sub

    '        End Try


    '    End Sub

    '    Private Sub TextBox1_Leave(sender As Object, e As EventArgs)
    '        If TextBox1.Text.Trim.Length = 0 Then Exit Sub

    '        Try

    '            OpenConn()

    '            SQL = "Select kode_jenis_hewan, keterangan From emi_jenis_hewan Where "
    '            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
    '            SQL = SQL & "kode_jenis_hewan = '" & TextBox1.Text.Trim & "'"
    '            Using Dr = OpenTrans(SQL)
    '                If Dr.Read Then
    '                    TextBox1.Text = Dr("kode_jenis_hewan")
    '                    TextBox2.Text = Dr("keterangan")

    '                    Btn_Simpan.Text = Base_Language.Lang_Global_Update : Btn_Hapus.Enabled = True
    '                    Btn_Simpan.Tag = "&Update"
    '                Else
    '                    TextBox2.Text = ""

    '                    Btn_Simpan.Text = Base_Language.Lang_Global_Simpan : Btn_Hapus.Enabled = False
    '                    Btn_Simpan.Tag = "&Simpan"
    '                End If
    '            End Using

    '            CloseConn()
    '        Catch ex As Exception
    '            CloseConn()
    '            MessageBox.Show(ex.Message)
    '            Exit Sub
    '        End Try
    '    End Sub

    '    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs)
    '        If TextBox1.Text.Trim.Length = 0 Then
    '            MessageBox.Show(Base_Language.Lang_Jenis_Hewan_Error_Kode, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            TextBox1.Focus() : Exit Sub
    '        ElseIf TextBox2.Text.Trim.Length = 0 Then
    '            MessageBox.Show(Base_Language.Lang_Jenis_Hewan_Error_Nama, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            TextBox2.Focus() : Exit Sub
    '        End If

    '        Try

    '            OpenConn()

    '            Cmd.Transaction = Cn.BeginTransaction

    '            If Btn_Simpan.Tag = "&Simpan" Then
    '                SQL = "Insert Into emi_jenis_hewan(Kode_Perusahaan, kode_jenis_hewan, keterangan) "
    '                SQL = SQL & "Values('" & KodePerusahaan & "', "
    '                SQL = SQL & "'" & TextBox1.Text.Trim & "', '" & TextBox2.Text.Trim & "')"
    '                ExecuteTrans(SQL)
    '            Else
    '                SQL = "Update emi_jenis_hewan Set keterangan = '" & TextBox2.Text.Trim & "' "
    '                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_jenis_hewan = '" & TextBox1.Text.Trim & "'"
    '                ExecuteTrans(SQL)
    '            End If

    '            Cmd.Transaction.Commit()

    '            CloseConn()

    '        Catch ex As Exception
    '            CloseConn()
    '            MessageBox.Show(ex.Message)
    '            Exit Sub
    '        End Try

    '        kosong()
    '        TextBox1.Focus()
    '    End Sub

    '    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs)
    '        Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    '        If Hapus1 = vbYes Then

    '            Try

    '                OpenConn()

    '                Cmd.Transaction = Cn.BeginTransaction

    '                SQL = "Delete From emi_jenis_hewan where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_jenis_hewan = '" & TextBox1.Text.Trim & "'"
    '                ExecuteTrans(SQL)

    '                Cmd.Transaction.Commit()

    '                CloseConn()
    '            Catch ex As Exception
    '                CloseTrans()
    '                CloseConn()
    '                MessageBox.Show(ex.Message)
    '                Exit Sub
    '            End Try

    '        Else
    '            MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        End If

    '        kosong()
    '        TextBox1.Focus()
    '    End Sub

    '    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs)
    '        kosong()
    '    End Sub

    '    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs)
    '        If ComboBox1.Text.Trim.Length = 0 Then Exit Sub
    '        If TextBox3.Text.Trim.Length = 0 Then Exit Sub

    '        Cari("T")
    '    End Sub

    '    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
    '        If e.KeyChar = Chr(13) Then TextBox2.Focus()
    '    End Sub

    '    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs)
    '        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    '    End Sub

    '    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    '    End Sub

    '    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
    '        If e.KeyChar = Chr(13) Then TextBox3.Focus()
    '    End Sub

    '    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs)
    '        If e.KeyChar = Chr(13) Then Btn_Cari_Click(TextBox3, e)
    '    End Sub

    '    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    '    End Sub
End Class