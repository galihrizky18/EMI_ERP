Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
'Imports Microsoft.SqlServer.Server


Public Class EMI_Schedule_SD_Tambah
    Public filter_tambahan, filter_kdSupplier As String
    Public asal As String
    Dim arrId_Routing As New ArrayList
    Dim Jenis = "Tampil_Barang"

    Private Sub kosong()
        get_jam()

        If BtnPilihBarang_Simpan.Tag = "Simpan" Then
            ''TxtSchedule_text_temp.Text = ""
            TxtSchedule_Deskripsi.Text = ""

            DtpSchedule_DateStart.Value = tgl_skg
            DtpSchedule_DateEnd.Value = tgl_skg
            DtpSchedule_TimeStart.Value = tgl_skg
            DtpSchedule_TimeEnd.Value = tgl_skg

            Jenis_Produk()
            Line()
            Ambil_Data()
            CmbSchedule_Kategori.Focus()
        Else

            Jenis_Produk()
            Line()
            Ambil_Data2()
            CmbSchedule_Kategori.Focus()
        End If

    End Sub

    Private Sub Ambil_Data2()


        Try
            OpenConn()

            SQL = "select a.no_faktur, a.id_jenis_produk, b.keterangan, judul, deskripsi, c.id_routing, Tanggal_Awal, Tanggal_akhir, "
            SQL = SQL & "d.Kode_Barang,e.Nama,d.Jumlah,d.Satuan from EMI_Order_Produksi a, "
            SQL = SQL & "EMI_Jenis_Produk b, emi_schedule c, Emi_Order_Produksi_Detail d, barang e where "
            'SQL = SQL & "format(Tanggal_Awal,'yyyy-MM-dd') as Tanggal_awal, format(Tanggal_Awal,'HH:mm:ss') as Jam_awal, "
            'SQL = SQL & "format(Tanggal_akhir,'yyyy-MM-dd') as Tanggal_akhir, format(Tanggal_Akhir,'HH:mm:ss') as Jam_akhir "
            SQL = SQL & " a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = d.No_Faktur "
            SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and  d.Kode_Barang = e.Kode_Barang and d.Kode_Stock_Owner = e.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Perusahaan=c.Kode_Perusahaan and a.id_schedule=c.id and a.status Is null "
            SQL = SQL & " And a.no_faktur ='" & TxtSchedule_NoFaktur.Text & "' and a.Kode_Perusahaan='" & KodePerusahaan & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then

                    For index = 0 To CmbSchedule_Kategori.Items.Count - 1
                        If arrId_Routing.Item(index) = dr("id_routing") Then
                            CmbSchedule_Kategori.SelectedIndex = index
                            Exit For
                        End If
                    Next

                    ''TxtSchedule_text_temp.Text = ""
                    TxtSchedule_Deskripsi.Text = ""
                    DtpSchedule_DateStart.Value = General_Class.CekNULL(dr("Tanggal_Awal"))
                    DtpSchedule_TimeStart.Value = General_Class.CekNULL(dr("Tanggal_Awal"))
                    DtpSchedule_DateEnd.Value = General_Class.CekNULL(dr("Tanggal_Akhir"))
                    DtpSchedule_TimeEnd.Value = General_Class.CekNULL(dr("Tanggal_Akhir"))
                    TxtKodeBarang.Text = dr("kode_barang")
                    TxtNamaBarang.Text = dr("nama")
                    TxtJumlah.Text = Format(dr("jumlah"), "N2")
                    TxtSatuan.Text = Format(dr("satuan"))
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("data tidak ditemukan . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Ambil_Data()


        Try
            OpenConn()


            SQL = "select a.no_faktur, a.id_jenis_produk, b.keterangan, a.id_routing, "
            SQL = SQL & "d.Kode_Barang,e.Nama,d.Jumlah,d.Satuan from EMI_Order_Produksi a, "
            SQL = SQL & "EMI_Jenis_Produk b, Emi_Order_Produksi_Detail d, barang e where "
            'SQL = SQL & "format(Tanggal_Awal,'yyyy-MM-dd') as Tanggal_awal, format(Tanggal_Awal,'HH:mm:ss') as Jam_awal, "
            'SQL = SQL & "format(Tanggal_akhir,'yyyy-MM-dd') as Tanggal_akhir, format(Tanggal_Akhir,'HH:mm:ss') as Jam_akhir "
            SQL = SQL & " a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = d.No_Faktur "
            SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and  d.Kode_Barang = e.Kode_Barang and d.Kode_Stock_Owner = e.Kode_Stock_Owner "
            SQL = SQL & " and a.status Is null "
            SQL = SQL & " And a.no_faktur ='" & TxtSchedule_NoFaktur.Text & "' and a.Kode_Perusahaan='" & KodePerusahaan & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then

                    For index = 0 To CmbSchedule_Kategori.Items.Count - 1
                        If arrId_Routing.Item(index) = dr("id_routing") Then
                            CmbSchedule_Kategori.SelectedIndex = index
                            Exit For
                        End If
                    Next


                    TxtKodeBarang.Text = dr("kode_barang")
                    TxtNamaBarang.Text = dr("nama")
                    TxtJumlah.Text = Format(dr("jumlah"), "N2")
                    TxtSatuan.Text = Format(dr("satuan"))
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("data tidak ditemukan . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Jenis_Produk()

        Try
            OpenConn()

            SQL = "select a.no_faktur, a.id_jenis_produk, b.keterangan from "
            SQL = SQL & "EMI_Order_Produksi a, EMI_Jenis_Produk b where "
            SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.id_jenis_produk=b.id_jenis_produk "
            SQL = SQL & " And a.status Is null And a.no_faktur ='" & TxtSchedule_NoFaktur.Text & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    TxtSchedule_JenisProduk.Text = dr("keterangan") : TxtSchedule_IdJenisProduk.Text = dr("id_jenis_produk")
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("data tidak ditemukan . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Line()
        If TxtSchedule_IdJenisProduk.Text.Trim.Length = 0 Then

            Exit Sub
        End If


        Try
            OpenConn()
            Dim id As Integer = 0
            CmbSchedule_Kategori.Items.Clear()
            'SQL = "select a.id_line, a.Keterangan as ket_line, b.Id_Jenis_Produk, b.Keterangan, a.backcolor "
            'SQL = SQL & "from EMI_Line a, EMI_Jenis_Produk b where "
            'SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Jenis_Produk=b.Id_Jenis_Produk "
            'SQL = SQL & " And a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            'SQL = SQL & "and a.Id_Jenis_Produk='" & TxtSchedule_IdJenisProduk.Text & "' "
            SQL = "select a.id_routing, a.Keterangan as ket_routing, b.Id_Jenis_Produk, b.Keterangan, a.backcolor "
            SQL = SQL & "from EMI_Master_Routing a, EMI_Jenis_Produk b where "
            SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Jenis_Produk=b.Id_Jenis_Produk "
            SQL = SQL & "and a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            SQL = SQL & "and a.Id_Jenis_Produk='" & TxtSchedule_IdJenisProduk.Text & "' "

            SQL = SQL & "order by a.Keterangan "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    arrId_Routing.Add(dr("id_routing"))
                    CmbSchedule_Kategori.Items.Add(dr("Keterangan") & " " & dr("ket_routing"))

                    id += 1
                Loop
            End Using

            CmbSchedule_Kategori.SelectedIndex = 0
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


    Private Sub SD_Pilih_Barang_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub SD_Pilih_Barang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()

            Base_Language.Get_Languages_Global(Bahasa_Pilihan)

            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            'LblSchedule_Judul.Text = Base_Language.Lang_TampilBarang_Judul
            'LblSchedule_Kategori.Text = Base_Language.Lang_Global_Lokasi
            'LblSchedule_text.Text = Base_Language.Lang_Global_KodeBarang
            'LblSchedule_Deskripsi.Text = Base_Language.Lang_Global_NamaBarang
            'LblSchedule_TanggalStart.Text = Base_Language.Lang_Global_Satuan

            'BtnPilihBarang_Simpan.Text = Base_Language.Lang_Global_Simpan
            BtnPilihBarang_Refresh.Text = Base_Language.Lang_Global_Refresh

            kosong()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs)
        kosong()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then TxtSchedule_Deskripsi.Focus()
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then BtnPilihBarang_Simpan.Focus()
    End Sub

    Private Sub BtnPilihBarang_Simpan_Click(sender As Object, e As EventArgs) Handles BtnPilihBarang_Simpan.Click
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If BtnPilihBarang_Simpan.Tag = "Simpan" Then
                'SQL = "insert into emi_schedule(Kode_Perusahaan, Judul, Deskripsi, Tanggal_Awal, Tanggal_akhir, Id_Line, no_rencana_produksi) "
                'SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtSchedule_text.Text & "', '" & TxtSchedule_Deskripsi.Text & "', "
                'SQL = SQL & "'" & Format(DtpSchedule_DateStart.Value, "yyyy-MM-dd") & " " & Format(DtpSchedule_TimeStart.Value, "HH:mm:00") & "', "
                'SQL = SQL & "'" & Format(DtpSchedule_DateEnd.Value, "yyyy-MM-dd") & " " & Format(DtpSchedule_TimeEnd.Value, "HH:mm:00") & "', "
                'SQL = SQL & "'" & arrId_line.Item(CmbSchedule_Kategori.SelectedIndex) & "', '" & TxtSchedule_NoFaktur.Text & "') "
                'ExecuteTrans(SQL)
                SQL = "insert into emi_schedule(Kode_Perusahaan, Judul, Deskripsi, Tanggal_Awal, Tanggal_akhir, Id_Routing, no_rencana_produksi) "
                SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtNamaBarang.Text & "', '" & TxtSchedule_Deskripsi.Text & "', "
                SQL = SQL & "'" & Format(DtpSchedule_DateStart.Value, "yyyy-MM-dd") & " " & Format(DtpSchedule_TimeStart.Value, "HH:mm:00") & "', "
                SQL = SQL & "'" & Format(DtpSchedule_DateEnd.Value, "yyyy-MM-dd") & " " & Format(DtpSchedule_TimeEnd.Value, "HH:mm:00") & "', "
                SQL = SQL & "'" & arrId_Routing.Item(CmbSchedule_Kategori.SelectedIndex) & "', '" & TxtSchedule_NoFaktur.Text & "') "
                ExecuteTrans(SQL)

                Dim x_no_urut_Schedule As Integer = 0
                SQL = "select IDENT_CURRENT('emi_schedule') as urutan"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        x_no_urut_Schedule = Dr("urutan")
                    End If
                End Using

                SQL = "select ID from emi_schedule where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_rencana_produksi = '" & TxtSchedule_NoFaktur.Text & "' and id = '" & x_no_urut_Schedule & "'"
                Using Dr = OpenTrans(SQL)
                    If Not (Dr.Read) Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Harap ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "select a.no_faktur from "
                SQL = SQL & "EMI_Order_Produksi a where "
                SQL = SQL & "a.status Is null And a.no_faktur ='" & TxtSchedule_NoFaktur.Text & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        dr.Close()
                        SQL = "update EMI_Order_Produksi set "
                        SQL = SQL & "tanggal_produksi='" & Format(DtpSchedule_DateStart.Value, "yyyy-MM-dd") & "', "
                        SQL = SQL & "Jam_Produksi='" & Format(DtpSchedule_TimeStart.Value, "HH:mm:00") & "', "
                        SQL = SQL & "id_routing='" & arrId_Routing.Item(CmbSchedule_Kategori.SelectedIndex) & "', "
                        SQL = SQL & "id_schedule='" & x_no_urut_Schedule & "' "
                        SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and no_faktur='" & TxtSchedule_NoFaktur.Text & "' "
                        ExecuteTrans(SQL)
                    Else
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("data tidak ditemukan . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using
            Else

                SQL = "select Kode_Perusahaan from "
                SQL = SQL & "emi_schedule a where "
                SQL = SQL & "a.status Is null And a.no_rencana_Produksi ='" & TxtSchedule_NoFaktur.Text & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then

                        dr.Close()
                        SQL = "update emi_schedule set "
                        SQL = SQL & "Judul='" & TxtNamaBarang.Text & "', "
                        SQL = SQL & "Deskripsi='" & TxtSchedule_Deskripsi.Text & "', "
                        SQL = SQL & "Tanggal_Awal='" & Format(DtpSchedule_DateStart.Value, "yyyy-MM-dd") & " " & Format(DtpSchedule_TimeStart.Value, "HH:mm:00") & "', "
                        SQL = SQL & "Tanggal_Akhir='" & Format(DtpSchedule_DateEnd.Value, "yyyy-MM-dd") & " " & Format(DtpSchedule_TimeEnd.Value, "HH:mm:00") & "', "
                        SQL = SQL & "id_routing='" & arrId_Routing.Item(CmbSchedule_Kategori.SelectedIndex) & "' "
                        SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and no_rencana_Produksi ='" & TxtSchedule_NoFaktur.Text & "' "
                        ExecuteTrans(SQL)
                    Else
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("data tidak ditemukan . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "select a.no_faktur, id_schedule from "
                SQL = SQL & "EMI_Order_Produksi a where "
                SQL = SQL & "a.status Is null And a.no_faktur ='" & TxtSchedule_NoFaktur.Text & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        Dim id_schdule As String = dr("id_schedule")
                        dr.Close()
                        SQL = "update EMI_Order_Produksi set "
                        SQL = SQL & "tanggal_produksi='" & Format(DtpSchedule_DateStart.Value, "yyyy-MM-dd") & "', "
                        SQL = SQL & "Jam_Produksi='" & Format(DtpSchedule_TimeStart.Value, "HH:mm:00") & "', "
                        SQL = SQL & "id_routing='" & arrId_Routing.Item(CmbSchedule_Kategori.SelectedIndex) & "', "
                        SQL = SQL & "id_schedule='" & id_schdule & "' "
                        SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and no_faktur='" & TxtSchedule_NoFaktur.Text & "' "
                        ExecuteTrans(SQL)
                    Else
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("data tidak ditemukan . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using
            End If


            Cmd.Transaction.Commit()
            CloseConn()
            EMI_Schedule.Data_Produksi()
            EMI_Schedule.Load_data()
            Me.Close()

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub BtnPilihBarang_Refresh_Click(sender As Object, e As EventArgs) Handles BtnPilihBarang_Refresh.Click

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub CmbPilihBarang_Satuan_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then BtnPilihBarang_Simpan.Focus()
    End Sub

End Class