Imports System.Reflection
Imports System.Windows.Forms.VisualStyles
Imports System.Xml


Public Class EMI_Transaksi_QC_Finish_Good2
    Dim arrcari, arrJenisQC As New ArrayList
    Dim arrCmbSwtich As New ArrayList
    Dim Jenis = "Master_Quality_Control"
    Dim id_qc As String
    Dim warna As String
    Dim SudahLoadWarna As Boolean = False

    Public noQc As String

    'Array 2 dimensi menggunakan list
    Dim arr2Switch As New List(Of List(Of String))


    Dim LvIDUji As String
    Dim LvKodeUji As String
    Dim LvKet As String
    Dim LvSatuan As String
    Dim LvMinAwal As String
    Dim LvMaxAwal As String
    Dim LvMinHasil As String
    Dim LvMaxHasil As String
    Dim LvJenis As String
    Dim LvTampilMasuk As String
    Dim LvTampilBongkar As String

    Dim CellIDUji As Integer = 0
    Dim CellKodeUji As Integer = 1
    Dim CellKet As Integer = 2
    Dim CellSatuan As Integer = 3
    Dim CellMinAwal As Integer = 4
    Dim CellMaxAwal As Integer = 5
    Dim CellMinHasil As Integer = 6
    Dim CellMaxHasil As Integer = 7
    Dim CellJenis As Integer = 8
    Dim CellTampilMasuk As Integer = 9
    Dim CellTampilBongkar As Integer = 10

    Dim LvData_Kode As String
    Dim LvData_Ket As String
    Dim LvData_satuan As String
    Dim LvData_ID As String

    Dim cellData_Kode As Integer = 0
    Dim cellData_Ket As Integer = 1
    Dim cellData_satuan As Integer = 2
    Dim cellData_ID As Integer = 3

    Private Sub get_no_faktur()
        Dim fQP As String = "QC"
        txtNoFaktur.Text = fQP & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("emi_hasil_QC_produksi", "no_faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_faktur, 1, " & Len(fQP) + 4 & ")", fQP & Format(tgl_skg, "MMyy"))
    End Sub

    'Public Sub Get_Isi_Listview(ByVal No_Index As Integer)

    '    LvIDUji = DGV_Data_QC.Rows(No_Index).Cells(CellIDUji).Value.ToString
    '    LvKodeUji = DGV_Data_QC.Rows(No_Index).Cells(CellKodeUji).Value.ToString
    '    LvKet = DGV_Data_QC.Rows(No_Index).Cells(CellKet).Value.ToString
    '    LvSatuan = DGV_Data_QC.Rows(No_Index).Cells(CellSatuan).Value.ToString
    '    LvMinAwal = DGV_Data_QC.Rows(No_Index).Cells(CellMinAwal).Value.ToString
    '    LvMaxAwal = DGV_Data_QC.Rows(No_Index).Cells(CellMaxAwal).Value.ToString
    '    LvMinHasil = DGV_Data_QC.Rows(No_Index).Cells(CellMinHasil).Value.ToString
    '    LvMaxHasil = DGV_Data_QC.Rows(No_Index).Cells(CellMaxHasil).Value.ToString
    '    LvJenis = DGV_Data_QC.Rows(No_Index).Cells(CellJenis).Value.ToString
    '    LvTampilMasuk = DGV_Data_QC.Rows(No_Index).Cells(CellTampilMasuk).Value.ToString
    '    LvTampilBongkar = DGV_Data_QC.Rows(No_Index).Cells(CellTampilBongkar).Value.ToString
    'End Sub

    Private Sub Master_Jenis_Hewan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Master_Jenis_Hewan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        kosong()
    End Sub

    Private Sub kosong()
        'txtNoFaktur.Text = ""
        'txtNoFaktur.Text = noQc
        'txtNoFaktur.Focus()

        ' TxtNoProduksi.Text = ""
        '  TxtNamaBarang.Text = ""
        'TxtKdBarang.Text = ""
        get_jam()
        get_no_faktur()

        Dgv_QC_Lab.Rows.Clear()

        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
        Btn_Hapus.Text = Base_Language.Lang_Global_Hapus

        Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
        Btn_Simpan.Tag = "&Simpan"
        Btn_Hapus.Enabled = False

        warna = String.Empty
        Btn_Diterima.BackColor = Color.FromArgb(158, 158, 158)
        Btn_Peringatan.BackColor = Color.FromArgb(158, 158, 158)
        Btn_Ditolak.BackColor = Color.FromArgb(158, 158, 158)


        SudahLoadWarna = False


        Load_QC()
        txtKeterangan.Focus()

    End Sub
    Private Sub Tampil()
        Try
            OpenConn()
            SQL = "Select a.No_Faktur,b.Kode_Supplier,d.Nama,b.driver As Supir,b.No_Plat As Plat_Number,b.No_SJ,a.Tanggal, "
            SQL = SQL & "a.Kode_Barang, c.nama As nama_barang, a.step, a.No_Fak_Loading_Barang, a.Jenis_QC "
            SQL = SQL & "From EMI_Hasil_Quality_Control a, emi_pembelian_loading b, barang c, Suppliers d Where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Fak_Loading_Barang = b.no_faktur And "
            SQL = SQL & "a.status Is null And b.status Is null And a.Kode_Perusahaan = c.Kode_Perusahaan And "
            SQL = SQL & "a.Kode_Barang = c.Kode_Barang And a.Kode_Stock_Owner = c.Kode_Stock_Owner "
            SQL = SQL & "And b.Kode_Perusahaan=d.Kode_Perusahaan And b.Kode_Supplier=d.Kode_Supplier "
            SQL = SQL & "And a.kode_Perusahaan='" & KodePerusahaan & "' and a.no_faktur='" & txtNoFaktur.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    TxtNoProduksi.Text = dr("No_Fak_Loading_Barang")

                    TxtNamaBarang.Text = dr("nama_barang")

                    TxtKdBarang.Text = dr("Kode_Barang")

                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub
    Private Sub Load_QC()
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Label1.Text = "Transaksi - Quality Control (Finish Good)"

            SQL = "select a.id,b.flag_tampil_dekstop,b.flag_tampil_android, a.kode_perusahaan,a.kode_barang,a.id_qc_formula as id_quality_control,b.kode_uji "
            SQL = SQL & ",b.Flag_Tampil_Android,b.keterangan as Keterangan_Kode_QC,b.satuan,b.id_kategori_komponen, c.keterangan as komponen,a.min_range,a.max_range,  "
            SQL = SQL & "a.min_nilai_seharusnya,a.max_nilai_seharusnya, isnull(c.Flag_Option,'T') as Flag_Option "
            SQL = SQL & " from EMI_Quality_Control_PerBarang a, EMI_Quality_Control b, EMI_Kategori_Komponen c  where   "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.Id_QC_Formula = b.Id_QC_Formula  "
            SQL = SQL & " and b.kode_perusahaan = c.kode_perusahaan and b.id_kategori_komponen = c.Id_Kategori_Komponen  "
            SQL = SQL & "and a.kode_barang = '" & TxtKdBarang.Text & "' and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by b.flag_tampil_dekstop,b.keterangan"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then


                        '===========================
                        arr2Switch.Clear()

                            For i As Integer = 0 To .Rows.Count - 1
                                Dgv_QC_Lab.Rows.Add(1)

                                Dim subArr As New List(Of String)

                                Dgv_QC_Lab.Rows(i).Cells(0).Value = .Rows(i).Item("id_quality_control")
                                Dgv_QC_Lab.Rows(i).Cells(1).Value = .Rows(i).Item("kode_uji")
                                Dgv_QC_Lab.Rows(i).Cells(2).Value = .Rows(i).Item("Keterangan_Kode_QC")
                                Dgv_QC_Lab.Rows(i).Cells(3).Value = .Rows(i).Item("satuan")

                            If .Rows(i).Item("Flag_Option") = "Y" Then
                                Dgv_QC_Lab.Rows(i).Cells(4).Value = ""

                                Dim dgvCmbValueSwitch As DataGridViewComboBoxCell
                                dgvCmbValueSwitch = Dgv_QC_Lab.Rows(i).Cells(5)
                                dgvCmbValueSwitch.Items.Clear() : arrCmbSwtich.Clear()

                                SQL = "select a.Id_Switch, a.Keterangan from EMI_Switch a "
                                SQL = SQL & "where  "
                                SQL = SQL & " a.id_qc_formula = '" & .Rows(i).Item("id_quality_control") & "' "
                                Using dr2 = OpenTrans(SQL)
                                    Do While dr2.Read
                                        dgvCmbValueSwitch.Items.Add(dr2("Keterangan")) : subArr.Add(dr2("id_switch"))
                                    Loop
                                End Using

                                Dgv_QC_Lab.Rows(i).Cells(4).ReadOnly = True
                                Dgv_QC_Lab.Rows(i).Cells(5).ReadOnly = False
                            Else
                                subArr.Add("")

                                Dgv_QC_Lab.Rows(i).Cells(4).Value = ""
                                Dgv_QC_Lab.Rows(i).Cells(4).ReadOnly = False
                                Dgv_QC_Lab.Rows(i).Cells(5).ReadOnly = True
                            End If

                            'Dgv_QC_Lab.Rows(i).Cells(6).Value = .Rows(i).Item("step")


                            If .Rows(i).Item("Flag_Option") <> "Y" Then
                                Dgv_QC_Lab.Rows(i).Cells(4).Style.BackColor = Color.LightGray
                            End If


                            Dgv_QC_Lab.Rows(i).Cells(7).Value = .Rows(i).Item("flag_tampil_dekstop")
                            ' Dgv_QC_Lab.Rows(i).Cells(8).Value = .Rows(i).Item("no_urut")

                            arr2Switch.Add(subArr)
                            Next
                        End If

                    'Else
                    '    CloseConn()
                    '    'MessageBox.Show("Data QC tidak ada! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    '    EMI_Kendaraan_QC_Display.kosong()
                    '    Me.Close()

                End With
            End Using


            'Set Warna

            If SudahLoadWarna = False Then
                Try
                    OpenConn()

                    SQL = "select warna from EMI_Hasil_Quality_Control "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & txtNoFaktur.Text & "'"
                    Using Dr = OpenTrans(SQL)
                        Do While Dr.Read
                            If Not General_Class.CekNULL(Dr("warna")) = "" Then

                                If Dr("warna").ToString.ToUpper = "HIJAU" Then
                                    Btn_Diterima.BackColor = Color.FromArgb(75, 176, 80)
                                ElseIf Dr("warna").ToString.ToUpper = "KUNING" Then
                                    Btn_Peringatan.BackColor = Color.FromArgb(255, 206, 68)
                                ElseIf Dr("warna").ToString.ToUpper = "MERAH" Then
                                    Btn_Ditolak.BackColor = Color.FromArgb(217, 59, 46)
                                End If

                            End If
                        Loop
                    End Using

                    SudahLoadWarna = True

                    CloseConn()
                Catch ex As Exception
                    CloseConn()
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try
            End If


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

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If txtNoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Quality_Control_Error_Kode, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtNoFaktur.Focus() : Exit Sub

        ElseIf Dgv_QC_Lab.Rows.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dgv_QC_Lab.Focus() : Exit Sub
        End If

        If String.IsNullOrEmpty(warna) Then
            MessageBox.Show("Belum Pilih Summary", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Label5.Focus() : Exit Sub
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


            SQL = "insert into EMI_Hasil_QC_Produksi(Kode_Perusahaan,No_Faktur,No_Fak_Produksi_Order,Tanggal,Jam,UserId,Kode_Stock_Owner, "
            SQL = SQL & "Kode_Barang,	Keterangan,Warna, Step)  values( "
            SQL = SQL & "'" & KodePerusahaan & "', '" & txtNoFaktur.Text.Trim & "', '" & TxtNoProduksi.Text.Trim & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', "
            SQL = SQL & "'" & UserID & "', "
            SQL = SQL & "'" & txtKso.Text.Trim & "', '" & TxtKdBarang.Text.Trim & "', '" & txtKeterangan.Text & "',"
            SQL = SQL & "'" & warna & "', " & jumlah & ")"
            ExecuteTrans(SQL)

            For i As Integer = 0 To Dgv_QC_Lab.Rows.Count - 1

                'cek apakah semua data sudah di isi
                If Dgv_QC_Lab.Rows(i).Cells(5).Value = "" And Dgv_QC_Lab.Rows(i).Cells(4).Value = "" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Kode Uji " & Dgv_QC_Lab.Rows(i).Cells(2).Value & " belum di isi!")
                    Exit Sub
                End If

                ' cek apakah dia switch apa bukan
                'kalauu switch update di EMI_Hasil_Detail_Switch_QC
                If Dgv_QC_Lab.Rows(i).Cells(5).Value <> "" Then
                    Dim comboBoxCell As DataGridViewComboBoxCell = CType(Dgv_QC_Lab.Rows(i).Cells(5), DataGridViewComboBoxCell)
                    Dim index As Integer = comboBoxCell.Items.IndexOf(comboBoxCell.Value)

                    Dim valuekodeuji As String = arr2Switch(i)(index).ToString

                    'simpan

                    SQL = "insert into EMI_Hasil_QC_produksi_detail_switch(Kode_Perusahaan,No_Faktur,Id_Quality_Control,Value_Kode_Uji) values ("
                    SQL = SQL & "'" & KodePerusahaan & "', '" & txtNoFaktur.Text & "', '" & Dgv_QC_Lab.Rows(i).Cells(0).Value & "',"
                    SQL = SQL & "'" & valuekodeuji & "' )"
                    ExecuteTrans(SQL)
                Else
                    'update di EMI_Hasil_Detail_Quality_Control
                    SQL = "insert into emi_hasil_QC_produksi_detail(Kode_Perusahaan,No_Faktur,Id_Quality_Control,Value_Kode_Uji) values ("
                    SQL = SQL & "'" & KodePerusahaan & "', '" & txtNoFaktur.Text & "', '" & Dgv_QC_Lab.Rows(i).Cells(0).Value & "',"
                    SQL = SQL & " '" & Dgv_QC_Lab.Rows(i).Cells(4).Value & "' )"
                    ExecuteTrans(SQL)
                End If
            Next

            'SQL = "update Emi_Split_Production_Order set Flag_QC = 'Y' where kode_perusahaan = '" & KodePerusahaan & "' and no_transaksi = '" & TxtNoProduksi.Text.Trim & "' "
            'ExecuteTrans(SQL)


            Cmd.Transaction.Commit()
            MessageBox.Show("Data berhasil disimpan ", Judul, MessageBoxButtons.OK)
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosong()
        EMI_Display_Hasil_Produksi.Button1_Click(Btn_Simpan, Nothing)
        Me.Close()
    End Sub



    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        If txtNoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Quality_Control_Error_Kode, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtNoFaktur.Focus() : Exit Sub
        End If
        Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Hapus1 = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction

                SQL = "delete from EMI_Quality_Control_PerBarang where "
                SQL = SQL & "Kode_Perusahaan ='" & KodePerusahaan & "' and "
                SQL = SQL & "Kode_Barang='" & txtNoFaktur.Text & "' "
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        kosong()
    End Sub



    Private Sub DGV_Data_QC_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs)
        If e.RowIndex = 0 And e.ColumnIndex = 5 Then
            ' Melukis latar belakang default cell
            e.PaintBackground(e.ClipBounds, True)

            ' Menggunakan brush untuk mengganti warna latar belakang
            Using brush As New SolidBrush(Color.LightGreen) ' Pilih warna yang diinginkan
                e.Graphics.FillRectangle(brush, e.CellBounds)
            End Using

            ' Melukis konten cell (teks atau isi ComboBox)
            e.PaintContent(e.ClipBounds)

            ' Tandai bahwa cell sudah di-handle (tidak perlu di-render ulang oleh DataGridView)
            e.Handled = True
        End If
    End Sub



    'FUNCTION UTILITY
    Private Sub Dgv_QC_Lab_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_QC_Lab.CellClick
        If e.ColumnIndex = DataGridViewComboBoxColumn1.Index Then
            Dgv_QC_Lab.CurrentCell = Dgv_QC_Lab.Rows(e.RowIndex).Cells(e.ColumnIndex)
            Dgv_QC_Lab.BeginEdit(True)
        End If
    End Sub

    Private Sub Dgv_QC_Lab_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_QC_Lab.CellEndEdit
        If IsNumeric(Dgv_QC_Lab.CurrentRow.Cells(CellMinAwal).Value) = False Then
            Dgv_QC_Lab.CurrentRow.Cells(CellMinAwal).Value = ""
        End If
    End Sub

    'Private Sub Dgv_QC_Lab_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Dgv_QC_Lab.CellFormatting

    '    If Dgv_QC_Lab.Rows(e.RowIndex).Cells(4).ReadOnly Then
    '        Dgv_QC_Lab.Rows(e.RowIndex).Cells(5).Style.BackColor = Color.LightGray
    '    Else
    '        Dgv_QC_Lab.Rows(e.RowIndex).Cells(5).Style.BackColor = Color.White
    '    End If
    'End Sub

    'FUNCTION HANDLE BUTTON

    Private Sub Btn_Diterima_Click(sender As Object, e As EventArgs) Handles Btn_Diterima.Click
        Btn_Peringatan.BackColor = Color.FromArgb(158, 158, 158)
        Btn_Ditolak.BackColor = Color.FromArgb(158, 158, 158)
        Btn_Diterima.BackColor = Color.FromArgb(75, 176, 80)
        warna = "HIJAU"
    End Sub
    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Btn_Peringatan.BackColor = Color.FromArgb(158, 158, 158)
        Btn_Ditolak.BackColor = Color.FromArgb(158, 158, 158)
        Btn_Diterima.BackColor = Color.FromArgb(75, 176, 80)
        warna = "HIJAU"
    End Sub
    Private Sub Btn_Peringatan_Click(sender As Object, e As EventArgs) Handles Btn_Peringatan.Click
        Btn_Diterima.BackColor = Color.FromArgb(158, 158, 158)
        Btn_Ditolak.BackColor = Color.FromArgb(158, 158, 158)
        Btn_Peringatan.BackColor = Color.FromArgb(255, 206, 68)
        warna = "KUNING"
    End Sub
    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Btn_Diterima.BackColor = Color.FromArgb(158, 158, 158)
        Btn_Ditolak.BackColor = Color.FromArgb(158, 158, 158)
        Btn_Peringatan.BackColor = Color.FromArgb(255, 206, 68)
        warna = "KUNING"
    End Sub
    Private Sub Btn_Ditolak_Click(sender As Object, e As EventArgs) Handles Btn_Ditolak.Click
        Btn_Diterima.BackColor = Color.FromArgb(158, 158, 158)
        Btn_Peringatan.BackColor = Color.FromArgb(158, 158, 158)
        Btn_Ditolak.BackColor = Color.FromArgb(217, 59, 46)
        warna = "MERAH"
    End Sub
    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Btn_Diterima.BackColor = Color.FromArgb(158, 158, 158)
        Btn_Peringatan.BackColor = Color.FromArgb(158, 158, 158)
        Btn_Ditolak.BackColor = Color.FromArgb(217, 59, 46)
        warna = "MERAH"
    End Sub


    'HANDLE CURSOR MASUK
    Private Sub Btn_Diterima_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Diterima.MouseEnter
        Btn_Diterima.Cursor = Cursors.Hand
    End Sub
    Private Sub Btn_Peringatan_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Peringatan.MouseEnter
        Btn_Peringatan.Cursor = Cursors.Hand
    End Sub
    Private Sub Btn_Ditolak_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Ditolak.MouseEnter
        Btn_Ditolak.Cursor = Cursors.Hand
    End Sub
    Private Sub Label4_MouseEnter(sender As Object, e As EventArgs) Handles Label4.MouseEnter
        Label4.Cursor = Cursors.Hand
    End Sub
    Private Sub Label6_MouseEnter(sender As Object, e As EventArgs) Handles Label6.MouseEnter
        Label6.Cursor = Cursors.Hand
    End Sub
    Private Sub Label7_MouseEnter(sender As Object, e As EventArgs) Handles Label7.MouseEnter
        Label7.Cursor = Cursors.Hand
    End Sub

    'HANDLE CURSOR KELUAR
    Private Sub Btn_Diterima_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Diterima.MouseLeave
        Btn_Diterima.Cursor = Cursors.Default
    End Sub
    Private Sub Btn_Peringatan_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Peringatan.MouseLeave
        Btn_Peringatan.Cursor = Cursors.Default
    End Sub
    Private Sub Btn_Ditolak_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Ditolak.MouseLeave
        Btn_Ditolak.Cursor = Cursors.Default
    End Sub
    Private Sub Label4_MouseLeave(sender As Object, e As EventArgs) Handles Label4.MouseLeave
        Label4.Cursor = Cursors.Default
    End Sub
    Private Sub Label6_MouseLeave(sender As Object, e As EventArgs) Handles Label6.MouseLeave
        Label6.Cursor = Cursors.Default
    End Sub

    Private Sub Label7_MouseLeave(sender As Object, e As EventArgs) Handles Label7.MouseLeave
        Label7.Cursor = Cursors.Default
    End Sub



End Class