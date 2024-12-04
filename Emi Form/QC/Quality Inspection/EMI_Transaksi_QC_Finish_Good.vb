Imports System.Reflection
Imports System.Windows.Forms.VisualStyles
Imports System.Xml


Public Class EMI_Transaksi_QC_Finish_Good
    Dim arrcari, arrJenisQC As New ArrayList

    Dim Jenis = "Master_Quality_Control"
    Dim id_qc As String
    Dim warna As String
    Dim SudahLoadWarna As Boolean = False

    'Array 2 dimensi menggunakan list
    Dim arr2Switch, arrSwitch As New List(Of List(Of String))

    Public SN_Baru_For_Update As String = ""

    Dim LvIDKategori As String
    Dim LvNmKategori As String
    Dim LvIDUji As String
    Dim LvKodeUji As String
    Dim LvNmUji As String
    Dim LvSatuan As String
    Dim LvValue As String
    Dim LvCmbValue As String
    Dim LvMinAwal As String
    Dim LvMaxAwal As String
    Dim LvMinHasil As String
    Dim LvMaxHasil As String
    Dim LvWarna As String
    Dim LvUrut As String
    Dim LvIDWarna As String
    Dim LvKeterangan As String

    Dim CellIDKategori As Integer = 0
    Dim CellNmKategori As Integer = 1
    Dim CellIDUji As Integer = 2
    Dim CellKodeUji As Integer = 3
    Dim CellNmUji As Integer = 4
    Dim CellSatuan As Integer = 5
    Dim CellValue As Integer = 6
    Dim CellCmbValue As Integer = 7
    Dim CellMinAwal As Integer = 8
    Dim CellMaxAwal As Integer = 9
    Dim CellMinHasil As Integer = 10
    Dim CellMaxHasil As Integer = 11
    Dim CellWarna As Integer = 12
    Dim CellID As Integer = 13
    Dim CellIDWarna As Integer = 14
    Dim CellKeterangan As Integer = 15



    'from emi_qc_hasil_produksi
    Dim Lv_NoTrans, Lv_Nama, Lv_Rak, Lv_Tgl, Lv_Jam, Lv_Jumlah, Lv_Satuan, Lv_SnBaru, Lv_KdBarang, LvKso, Lv_UrutOto As String

    Dim item_NoTrans As Integer = 0
    Dim item_Nama As Integer = 1
    Dim item_Rak As Integer = 2
    Dim item_Tanggal As Integer = 3
    Dim item_Jam As Integer = 4
    Dim item_Jumlah As Integer = 5
    Dim item_Satuan As Integer = 6
    Dim item_SNBaru As Integer = 7
    Dim item_KDBrg As Integer = 8
    Dim item_Kso As Integer = 9
    Dim item_Urut_Oto As Integer = 10


    Private Sub Master_Jenis_Hewan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Master_Jenis_Hewan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        kosong()
    End Sub

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)

        LvIDKategori = CekNothing(Dgv_QC_Lab.Rows(No_Index).Cells(CellIDKategori).Value)
        LvNmKategori = CekNothing(Dgv_QC_Lab.Rows(No_Index).Cells(CellNmKategori).Value)
        LvIDUji = CekNothing(Dgv_QC_Lab.Rows(No_Index).Cells(CellIDUji).Value)
        LvKodeUji = CekNothing(Dgv_QC_Lab.Rows(No_Index).Cells(CellKodeUji).Value)
        LvNmUji = CekNothing(Dgv_QC_Lab.Rows(No_Index).Cells(CellNmUji).Value)
        LvSatuan = CekNothing(Dgv_QC_Lab.Rows(No_Index).Cells(CellSatuan).Value)
        LvValue = CekNothing(Dgv_QC_Lab.Rows(No_Index).Cells(CellValue).Value)
        If Dgv_QC_Lab.Rows(No_Index).Cells(CellCmbValue).Value IsNot Nothing Then

            LvCmbValue = CekNothing(Dgv_QC_Lab.Rows(No_Index).Cells(CellCmbValue).Value)
        Else
            LvCmbValue = ""
        End If
        LvMinAwal = CekNothing(Dgv_QC_Lab.Rows(No_Index).Cells(CellMinAwal).Value)
        LvMaxAwal = CekNothing(Dgv_QC_Lab.Rows(No_Index).Cells(CellMaxAwal).Value)
        LvMinHasil = CekNothing(Dgv_QC_Lab.Rows(No_Index).Cells(CellMinHasil).Value)
        LvMaxHasil = CekNothing(Dgv_QC_Lab.Rows(No_Index).Cells(CellMaxHasil).Value)
        LvWarna = CekNothing(Dgv_QC_Lab.Rows(No_Index).Cells(CellWarna).Value)
        LvUrut = CekNothing(Dgv_QC_Lab.Rows(No_Index).Cells(CellID).Value)
        LvIDWarna = CekNothing(Dgv_QC_Lab.Rows(No_Index).Cells(CellIDWarna).Value)
        LvKeterangan = CekNothing(Dgv_QC_Lab.Rows(No_Index).Cells(CellKeterangan).Value)

    End Sub


    Private Sub Get_Data_Lv(ByVal index As Integer)

        Lv_NoTrans = EMI_Display_QC_Produksi.Lv_Data.Items(index).SubItems(item_NoTrans).Text
        Lv_Nama = EMI_Display_QC_Produksi.Lv_Data.Items(index).SubItems(item_Nama).Text
        Lv_Rak = EMI_Display_QC_Produksi.Lv_Data.Items(index).SubItems(item_Rak).Text
        Lv_Tgl = EMI_Display_QC_Produksi.Lv_Data.Items(index).SubItems(item_Tanggal).Text
        Lv_Jam = EMI_Display_QC_Produksi.Lv_Data.Items(index).SubItems(item_Jam).Text
        Lv_Jumlah = EMI_Display_QC_Produksi.Lv_Data.Items(index).SubItems(item_Jumlah).Text
        Lv_Satuan = EMI_Display_QC_Produksi.Lv_Data.Items(index).SubItems(item_Satuan).Text
        Lv_SnBaru = EMI_Display_QC_Produksi.Lv_Data.Items(index).SubItems(item_SNBaru).Text
        Lv_KdBarang = EMI_Display_QC_Produksi.Lv_Data.Items(index).SubItems(item_KDBrg).Text
        LvKso = EMI_Display_QC_Produksi.Lv_Data.Items(index).SubItems(item_Kso).Text
        Lv_UrutOto = EMI_Display_QC_Produksi.Lv_Data.Items(index).SubItems(item_Urut_Oto).Text

    End Sub

    Private Sub kosong()

        Try
            OpenConn()

            get_no_faktur()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        'TxtNoProduksi.Text = ""
        'TxtNamaBarang.Text = ""
        'TxtKdBarang.Text = ""
        'txtKso.Text = ""

        Dgv_QC_Lab.Rows.Clear()

        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
        Btn_Hapus.Text = Base_Language.Lang_Global_Hapus

        Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
        Btn_Simpan.Tag = "&Simpan"
        Btn_Hapus.Enabled = False

        warna = String.Empty
        Pnl_Sesuai.BackColor = Color.FromArgb(158, 158, 158)
        Pnl_TidakSesuai.BackColor = Color.FromArgb(158, 158, 158)
        PnlBelumSelesai.BackColor = Color.FromArgb(158, 158, 158)

        SudahLoadWarna = False


        Load_QC()


    End Sub

    Private Sub get_no_faktur()
        Dim fQP As String = "QC"
        txtNoFaktur.Text = fQP & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("emi_hasil_QC_produksi", "no_faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_faktur, 1, " & Len(fQP) + 4 & ")", fQP & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub Hasil_QC()
        Dim Data_Kosong As Integer = 0
        Dim Data_Tidak_sesuai As Integer = 0

        For index = 0 To Dgv_QC_Lab.Rows.Count - 1
            Get_Isi_Listview(index)

            If LvIDWarna = "" Then
                Data_Kosong += 1
            End If

            If LvIDWarna.ToUpper = "MERAH" Then
                Data_Tidak_sesuai += 1
            End If
        Next

        If Data_Kosong > 0 Then

            PnlBelumSelesai.BackColor = Color.FromArgb(173, 216, 230)
            Pnl_TidakSesuai.BackColor = Color.FromArgb(158, 158, 158)
            Pnl_Sesuai.BackColor = Color.FromArgb(158, 158, 158)
            warna = "PUTIH"
        ElseIf Data_Tidak_sesuai > 0 Then
            PnlBelumSelesai.BackColor = Color.FromArgb(158, 158, 158)
            Pnl_TidakSesuai.BackColor = Color.FromArgb(255, 225, 53)
            Pnl_Sesuai.BackColor = Color.FromArgb(158, 158, 158)
            warna = "KUNING"
        Else
            PnlBelumSelesai.BackColor = Color.FromArgb(158, 158, 158)
            Pnl_TidakSesuai.BackColor = Color.FromArgb(158, 158, 158)
            Pnl_Sesuai.BackColor = Color.FromArgb(144, 238, 144)
            warna = "HIJAU"
        End If

    End Sub
    Private Sub Load_QC()
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Label1.Text = "Transaksi - Quality Control (Finish Good)"

            SQL = "Select a.kode_perusahaan, b.Id_Kategori_Komponen, c.Keterangan As Kategori_Komponen, "
            SQL = SQL & "a.Id_QC_Formula, b.Kode_Uji, b.Keterangan, b.satuan, a.Id, "
            SQL = SQL & "b.Flag_Tampil_Android, b.Flag_Tampil_Dekstop, "
            SQL = SQL & "isnull(c.Flag_Input,'T') as Flag_Input, isnull(c.Flag_Option,'T') as Flag_Option, "
            SQL = SQL & "isnull(c.Flag_Slider,'T') as Flag_Slider, "
            SQL = SQL & "isnull(a.min_range,0) as min_range, isnull(a.Max_Range,0) as Max_Range, "
            SQL = SQL & "isnull(a.Min_Nilai_Seharusnya, 0) Min_Nilai_Seharusnya, isnull(a.Max_Nilai_Seharusnya,0) Max_Nilai_Seharusnya "
            SQL = SQL & "From EMI_Quality_Control_PerBarang a, EMI_Quality_Control b, EMI_Kategori_Komponen c "
            SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And b.Kode_Perusahaan=c.Kode_Perusahaan "
            SQL = SQL & "And a.Id_QC_Formula = b.Id_QC_Formula "
            SQL = SQL & "And b.Id_Kategori_Komponen=c.Id_Kategori_Komponen "
            SQL = SQL & "And a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_barang = '" & TxtKdBarang.Text & "' "

            'If filter = "lapangan" Then
            '    SQL = SQL & "And flag_tampil_android = 'Y' "
            'ElseIf filter = "lab" Then
            '    SQL = SQL & "And Flag_Tampil_Dekstop = 'Y' "
            'End If

            SQL = SQL & "order by keterangan "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        '===========================
                        arr2Switch.Clear()
                        For i As Integer = 0 To .Rows.Count - 1

                            Dgv_QC_Lab.Rows.Add(1)

                            Dim subArr As New List(Of String)

                            Dgv_QC_Lab.Rows(i).Cells(CellIDKategori).Value = .Rows(i).Item("Id_Kategori_Komponen")
                            Dgv_QC_Lab.Rows(i).Cells(CellNmKategori).Value = .Rows(i).Item("Kategori_Komponen")
                            Dgv_QC_Lab.Rows(i).Cells(CellIDUji).Value = .Rows(i).Item("Id_QC_Formula")
                            Dgv_QC_Lab.Rows(i).Cells(CellKodeUji).Value = .Rows(i).Item("Kode_Uji")
                            Dgv_QC_Lab.Rows(i).Cells(CellNmUji).Value = .Rows(i).Item("Keterangan")
                            Dgv_QC_Lab.Rows(i).Cells(CellSatuan).Value = .Rows(i).Item("satuan")

                            If .Rows(i).Item("Flag_Option") = "T" Then
                                Dgv_QC_Lab.Rows(i).Cells(CellValue).Value = ""
                                Dgv_QC_Lab.Rows(i).Cells(CellCmbValue).Value = ""

                                Dgv_QC_Lab.Rows(i).Cells(CellValue).Style.BackColor = Color.LightGray
                                Dgv_QC_Lab.Rows(i).Cells(CellCmbValue).Style.BackColor = Color.White

                                Dgv_QC_Lab.Rows(i).Cells(CellValue).ReadOnly = False
                                Dgv_QC_Lab.Rows(i).Cells(CellCmbValue).ReadOnly = True

                                subArr.Add("")
                            Else
                                Dgv_QC_Lab.Rows(i).Cells(CellValue).Value = ""

                                Dim dgvCmbValueSwitch As DataGridViewComboBoxCell
                                dgvCmbValueSwitch = Dgv_QC_Lab.Rows(i).Cells(CellCmbValue)
                                dgvCmbValueSwitch.Items.Clear()

                                'Dim indexValue As Integer = 0
                                Dim inddd As Integer = 0
                                SQL = "select a.Id_Switch, a.Keterangan from EMI_Switch a "
                                SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Id_QC_Formula = '" & .Rows(i).Item("Id_QC_Formula") & "' "
                                Using dr2 = OpenTrans(SQL)
                                    Do While dr2.Read
                                        dgvCmbValueSwitch.Items.Add(dr2("Keterangan")) : subArr.Add(dr2("id_switch"))


                                        inddd += 1
                                    Loop
                                End Using

                                'dgvCmbValueSwitch.Value = dgvCmbValueSwitch.Items(indexValue)

                                Dgv_QC_Lab.Rows(i).Cells(CellValue).Style.BackColor = Color.White
                                Dgv_QC_Lab.Rows(i).Cells(CellCmbValue).Style.BackColor = Color.LightGray

                                Dgv_QC_Lab.Rows(i).Cells(CellValue).ReadOnly = True
                                Dgv_QC_Lab.Rows(i).Cells(CellCmbValue).ReadOnly = False
                            End If

                            arr2Switch.Add(subArr)

                            If .Rows(i).Item("Flag_Slider") = "Y" Then
                                Dgv_QC_Lab.Rows(i).Cells(CellMinAwal).Value = .Rows(i).Item("Min_Range")
                                Dgv_QC_Lab.Rows(i).Cells(CellMaxAwal).Value = .Rows(i).Item("Max_Range")
                                Dgv_QC_Lab.Rows(i).Cells(CellMinHasil).Value = .Rows(i).Item("Min_Nilai_Seharusnya")
                                Dgv_QC_Lab.Rows(i).Cells(CellMaxHasil).Value = .Rows(i).Item("Max_Nilai_Seharusnya")
                            Else
                                Dgv_QC_Lab.Rows(i).Cells(CellMinAwal).Value = ""
                                Dgv_QC_Lab.Rows(i).Cells(CellMaxAwal).Value = ""
                                Dgv_QC_Lab.Rows(i).Cells(CellMinHasil).Value = ""
                                Dgv_QC_Lab.Rows(i).Cells(CellMaxHasil).Value = ""
                            End If

                            'If .Rows(i).Item("Warna").ToString.ToUpper = "HIJAU" Then
                            '    Dgv_QC_Lab.Rows(i).Cells(CellWarna).Style.BackColor = Color.FromArgb(144, 238, 144)
                            'ElseIf .Rows(i).Item("Warna").ToString.ToUpper = "MERAH" Then
                            '    Dgv_QC_Lab.Rows(i).Cells(CellWarna).Style.BackColor = Color.FromArgb(254, 46, 46)
                            'Else
                            '    Dgv_QC_Lab.Rows(i).Cells(CellWarna).Style.BackColor = Color.FromArgb(255, 255, 255)
                            'End If

                            Dgv_QC_Lab.Rows(i).Cells(CellWarna).Value = ""
                            Dgv_QC_Lab.Rows(i).Cells(CellIDWarna).Value = ""
                            Dgv_QC_Lab.Rows(i).Cells(CellID).Value = .Rows(i).Item("id")
                            Dgv_QC_Lab.Rows(i).Cells(CellKeterangan).Value = ""


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

        Hasil_QC()
    End Sub



    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If txtNoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Quality_Control_Error_Kode, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtNoFaktur.Focus() : Exit Sub

        ElseIf Dgv_QC_Lab.Rows.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dgv_QC_Lab.Focus() : Exit Sub
        End If

        If warna = "PUTIH" Then
            MessageBox.Show("Data Belum di Lengkapi . . ! ! ", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub

        End If
        Dim Hasil As String = ""

        If warna = "HIJAU" Then
            Hasil = "DITERIMA"
        ElseIf warna = "KUNING" Then
            warna = "MERAH"
            Hasil = "TOLAK"
        Else
            Hasil = "TOLAK"
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur()

            Dim jumlah As Integer = 0
            SQL = "select count(Kode_Perusahaan) as jumlah from EMI_Hasil_QC_Produksi a "
            SQL = SQL & "where a.kode_Perusahaan='" & KodePerusahaan & "' and a.No_Fak_Produksi_Order='" & TxtNoProduksi.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    jumlah = dr("jumlah") + 1
                End If
            End Using


            SQL = "insert into EMI_Hasil_QC_Produksi(Kode_Perusahaan, No_Faktur, No_Fak_Produksi_Order, Tanggal, Jam, UserId, Kode_Stock_Owner, "
            SQL = SQL & "Kode_Barang, Keterangan, Warna, Step, Hasil)  values( "
            SQL = SQL & "'" & KodePerusahaan & "', '" & txtNoFaktur.Text.Trim & "', '" & TxtNoProduksi.Text.Trim & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', "
            SQL = SQL & "'" & UserID & "', "
            SQL = SQL & "'" & txtKso.Text.Trim & "', '" & TxtKdBarang.Text.Trim & "', '" & txtKeterangan.Text & "',"
            SQL = SQL & "'" & warna & "', " & jumlah & ", '" & Hasil & "')"
            ExecuteTrans(SQL)

            For i As Integer = 0 To Dgv_QC_Lab.Rows.Count - 1
                Get_Isi_Listview(i)
                'cek apakah semua data sudah di isi

                'cek apakah semua data sudah di isi
                If Dgv_QC_Lab.Rows(i).Cells(CellCmbValue).Value = "" And Dgv_QC_Lab.Rows(i).Cells(CellValue).Value = "" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Kode Uji " & Dgv_QC_Lab.Rows(i).Cells(CellNmUji).Value & " belum di isi!")
                    Exit Sub
                End If

                ' cek apakah dia switch apa bukan
                'kalauu switch update di EMI_Hasil_Detail_Switch_QC
                If Dgv_QC_Lab.Rows(i).Cells(CellCmbValue).Value <> "" Then
                    Dim comboBoxCell As DataGridViewComboBoxCell = CType(Dgv_QC_Lab.Rows(i).Cells(CellCmbValue), DataGridViewComboBoxCell)
                    Dim index As Integer = comboBoxCell.Items.IndexOf(comboBoxCell.Value)

                    Dim valuekodeuji As String = arr2Switch(i)(index).ToString

                    'simpan

                    SQL = "insert into EMI_Hasil_QC_produksi_detail_switch(Kode_Perusahaan, No_Faktur, Id_Quality_Control, Value_Kode_Uji, Keterangan, Warna) values ("
                    SQL = SQL & "'" & KodePerusahaan & "', '" & txtNoFaktur.Text & "', '" & Dgv_QC_Lab.Rows(i).Cells(CellIDUji).Value & "',"
                    SQL = SQL & "'" & valuekodeuji & "', '" & LvKeterangan & "', '" & LvIDWarna & "' )"
                    ExecuteTrans(SQL)
                Else
                    'update di EMI_Hasil_Detail_Quality_Control
                    SQL = "insert into emi_hasil_QC_produksi_detail(Kode_Perusahaan, No_Faktur, Id_Quality_Control, Value_Kode_Uji, Keterangan, Warna) values ("
                    SQL = SQL & "'" & KodePerusahaan & "', '" & txtNoFaktur.Text & "', '" & Dgv_QC_Lab.Rows(i).Cells(CellIDUji).Value & "',"
                    SQL = SQL & " '" & Dgv_QC_Lab.Rows(i).Cells(CellValue).Value & "', '" & LvKeterangan & "', '" & LvIDWarna & "' )"
                    ExecuteTrans(SQL)
                End If


            Next


            For i As Integer = 0 To EMI_Display_QC_Produksi.Lv_Data.Items.Count - 1
                Get_Data_Lv(i)

                '====================================
                ' cek udh pernah ke update atau belum
                '=====================================
                SQL = "select serial_number from barang_sn where kode_perusahaan = '" & KodePerusahaan & "'  "
                SQL = SQL & "and flag_qi = 'Y' and serial_number = '" & Lv_SnBaru & "'  "
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi kesalahan, ada perubahan data pada barang!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                SQL = "update barang_sn set "
                SQL = SQL & "warna = '" & warna & "', "
                SQL = SQL & "flag_qi = null "
                SQL = SQL & "where kode_perusahaan  = '" & KodePerusahaan & "' "
                SQL = SQL & "and serial_number = '" & Lv_SnBaru & "' "
                ExecuteTrans(SQL)


                SQL = "update Emi_Production_Results_Detail_Pallet set "
                SQL = SQL & "warna_qi = '" & warna & "', "
                SQL = SQL & "no_faktur_qc = '" & txtNoFaktur.Text & "', "
                SQL = SQL & "flag_sudah_qi = 'Y' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and urut_oto = '" & Lv_UrutOto & "' "
                ExecuteTrans(SQL)


            Next




            Cmd.Transaction.Commit()
            MessageBox.Show("Data berhasil disimpan ", Judul, MessageBoxButtons.OK)
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        '=================
        '=     CETAK     =
        '=================
        Try
            OpenConn()

            SQL = "select Kode_Perusahaan from View_Laporan_Hasil_QC_FG where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & txtNoFaktur.Text & "' and No_Produksi_Order = '" & TxtNoProduksi.Text & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Dim CrDoc As New Rpt_Laporan_Hasil_QC_FG
                    With A_Place_For_Printing2
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = "{View_Laporan_Hasil_QC_FG.Kode_Perusahaan} = '" & KodePerusahaan & "' and {View_Laporan_Hasil_QC_FG.No_Faktur} = '" & txtNoFaktur.Text & "' and {View_Laporan_Hasil_QC_FG.No_Produksi_Order} = '" & TxtNoProduksi.Text & "' "
                        .Text = "Bukti Hasil Quality Control Produksi"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .Refresh()
                        .Show()
                    End With
                Else
                    MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try




        kosong()
        EMI_Display_QC_Produksi.Kosong()
        'EMI_Display_Quality_Control.Btn_Refresh_Click(Btn_Simpan, e)
        Me.Close()
    End Sub





    'FUNCTION UTILITY
    Private Sub Dgv_QC_Lab_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_QC_Lab.CellClick
        If e.ColumnIndex = DataGridViewComboBoxColumn1.Index Then
            Dgv_QC_Lab.CurrentCell = Dgv_QC_Lab.Rows(e.RowIndex).Cells(e.ColumnIndex)
            Dgv_QC_Lab.BeginEdit(True)
        End If
    End Sub

    Private Sub Dgv_QC_Lab_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_QC_Lab.CellEndEdit

        If Dgv_QC_Lab.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim currentRow = Dgv_QC_Lab.CurrentRow.Index
        Dim currentCell = Dgv_QC_Lab.CurrentCellAddress.X

        Dim data = Dgv_QC_Lab.Rows(currentRow).Cells(currentCell)

        If currentCell = CellValue Then
            If Val(Dgv_QC_Lab.CurrentRow.Cells(CellValue).Value) < 0 Or IsNumeric(Dgv_QC_Lab.CurrentRow.Cells(CellValue).Value) = False Then
                Dgv_QC_Lab.CurrentRow.Cells(CellValue).Value = 0
            End If

            Get_Isi_Listview(currentRow)
            Dim Flag_slider As String = ""
            Dim Flag_Input As String = ""
            Try
                OpenConn()

                SQL = "select ISNULL(Flag_Slider,'T') as Flag_Slider, "
                SQL = SQL & "ISNULL(Flag_Option,'T') as Flag_Option, "
                SQL = SQL & "ISNULL(Flag_Input,'T') as Flag_Input from "
                SQL = SQL & "EMI_Kategori_Komponen where kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Kategori_Komponen='" & LvIDKategori & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        Flag_slider = dr("Flag_Slider")
                        Flag_Input = dr("Flag_Input")
                    Else
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("Data Tidak ditemukan . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using
                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            If Flag_slider = "Y" Then
                If Val(HilangkanTanda(LvValue)) < Val(HilangkanTanda(LvMinAwal)) Or Val(HilangkanTanda(LvValue)) > Val(HilangkanTanda(LvMaxAwal)) Then
                    Dgv_QC_Lab.CurrentRow.Cells(CellValue).Value = ""
                    MessageBox.Show("Value Tidak Boleh Lebih atau Kurang dari Range", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                If Val(HilangkanTanda(LvValue)) >= Val(HilangkanTanda(LvMinHasil)) And Val(HilangkanTanda(LvValue)) <= Val(HilangkanTanda(LvMaxHasil)) Then
                    Dgv_QC_Lab.CurrentRow.Cells(CellWarna).Style.BackColor = Color.FromArgb(144, 238, 144)
                    Dgv_QC_Lab.CurrentRow.Cells(CellIDWarna).Value = "HIJAU"
                Else
                    Dgv_QC_Lab.CurrentRow.Cells(CellWarna).Style.BackColor = Color.FromArgb(254, 46, 46)
                    Dgv_QC_Lab.CurrentRow.Cells(CellIDWarna).Value = "MERAH"
                End If
            End If

            If Flag_Input = "Y" Then
                Dgv_QC_Lab.CurrentRow.Cells(CellWarna).Style.BackColor = Color.FromArgb(144, 238, 144)
                Dgv_QC_Lab.CurrentRow.Cells(CellIDWarna).Value = "HIJAU"
            End If


        ElseIf currentCell = CellCmbValue Then
            Get_Isi_Listview(currentRow)

            Dim Flag_Option As String = ""

            Try
                OpenConn()

                SQL = "select ISNULL(Flag_Slider,'T') as Flag_Slider, "
                SQL = SQL & "ISNULL(Flag_Option,'T') as Flag_Option, "
                SQL = SQL & "ISNULL(Flag_Input,'T') as Flag_Input from "
                SQL = SQL & "EMI_Kategori_Komponen where kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Kategori_Komponen='" & LvIDKategori & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then

                        Flag_Option = dr("Flag_Option")

                    Else
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("Data Tidak ditemukan . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using
                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
            If Flag_Option = "Y" Then

                Dim comboBoxCell As DataGridViewComboBoxCell = CType(Dgv_QC_Lab.Rows(currentRow).Cells(CellCmbValue), DataGridViewComboBoxCell)
                Dim index As Integer = comboBoxCell.Items.IndexOf(comboBoxCell.Value)

                Dim data_default As String = ""
                Try
                    OpenConn()

                    SQL = "select isnull(flag_default,'T') as flag_default "
                    SQL = SQL & "from EMI_Switch where id_qc_formula='" & LvIDUji & "' and "
                    SQL = SQL & "kode_Perusahaan='" & KodePerusahaan & "' and id_switch='" & arr2Switch(currentRow)(index) & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            data_default = dr("flag_default")
                        Else
                            dr.Close()
                            CloseConn()
                            MessageBox.Show("Data Tidak ditemukan . .  ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using
                    CloseConn()
                Catch ex As Exception
                    CloseConn()
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try

                If data_default = "Y" Then
                    Dgv_QC_Lab.CurrentRow.Cells(CellWarna).Style.BackColor = Color.FromArgb(144, 238, 144)
                    Dgv_QC_Lab.CurrentRow.Cells(CellIDWarna).Value = "HIJAU"
                Else
                    Dgv_QC_Lab.CurrentRow.Cells(CellWarna).Style.BackColor = Color.FromArgb(254, 46, 46)
                    Dgv_QC_Lab.CurrentRow.Cells(CellIDWarna).Value = "MERAH"
                End If

            End If
        End If


        Hasil_QC()


    End Sub


    Private Function CekNothing(ByVal str As String) As String
        Dim hasil As String = ""

        If str Is Nothing Then
            hasil = ""
        Else
            hasil = str
        End If

        Return hasil
    End Function




End Class