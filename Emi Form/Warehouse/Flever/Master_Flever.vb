Imports System.Reflection
Imports LovePdf.Model.TaskParams.Sign.Elements

Public Class Master_Flever
    Dim isFillingData As Boolean = False

    Dim arrSo, arrCari As New ArrayList

    Dim LvIdFlever, LvKdBrgAwal, LvNmBrgAwal, LvKdBrgAkhir, LvNmBrgAkhir, LvJmlDibutuhkan, LvSatuanMinimum, LvJmljadi, lvSatuanJadi As String

    Dim LvBarangKdSo, LvBarangKdBrg, LvBarangNmBrg As String

    Dim itemData_IdFlever As Integer = 0
    Dim itemData_KdBrgAwal As Integer = 1
    Dim itemData_NmBrgAwal As Integer = 2
    Dim itemData_KdBrgAkhir As Integer = 3
    Dim itemData_NmBrgAkhir As Integer = 4
    Dim itemData_JmlDibutuhkan As Integer = 5
    Dim itemData_SatuanMinimum As Integer = 6
    Dim itemData_JmlJadi As Integer = 7
    Dim itemData_SatuanJadi As Integer = 8

    Dim itemBarang_KdSo As Integer = 0
    Dim itemBarang_KdBrg As Integer = 1
    Dim itemBarang_NmBrg As Integer = 2

    Private Sub Master_Flever_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lv_DetailBarang.Columns.Clear()
        Lv_Data.Columns.Clear()

        Lv_DetailBarang.Columns.Add("Kode Stock Owner", 0, HorizontalAlignment.Left)
        Lv_DetailBarang.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_DetailBarang.Columns.Add("Nama", 300, HorizontalAlignment.Left)
        Lv_DetailBarang.View = View.Details

        Lv_Data.Columns.Add("ID", 0, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Kode Barang Awal", 130, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Nama Barang Awal", 300, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Kode Barang Akhir", 130, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Nama Barang Akhir", 300, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Jumlah Dibutuhkan", 120, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Satuan Minimum", 120, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jumlah Jadi", 120, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Satuan Jadi", 120, HorizontalAlignment.Center)
        Lv_Data.View = View.Details

        kosong()
    End Sub

    Private Sub kosong()

        Cmb_Lokasi.Items.Clear()
        Cmb_Satuan.SelectedIndex = -1 : Cmb_Satuan.Items.Clear()

        Txt_IdFlever.Text = String.Empty
        Txt_KdBarangAwal.Text = String.Empty
        Txt_KdBarangAkhir.Text = String.Empty
        Txt_NamaBarangAwal.Text = String.Empty
        Txt_NamaBarangAkhir.Text = String.Empty
        Txt_JmlhMaterialAwal.Text = String.Empty
        Txt_HslMaterialAkhir.Text = String.Empty

        Lv_Data.Items.Clear()
        Lv_DetailBarang.Items.Clear()

        Lv_DetailBarang.Visible = False
        Lv_DetailBarang.Location = New Point(880, 191)
        Btn_Simpan.Tag = "&Simpan"
        Btn_Simpan.Text = "&Simpan"
        Btn_Hapus.Enabled = False

        Cmb_Kolom.Items.Clear() : arrCari.Clear() : Cmb_Kolom.SelectedIndex = -1 : Txt_Value.Text = ""
        Cmb_Kolom.Items.Add("Kode Barang Awal") : arrCari.Add("Kode_Barang_Min")
        Cmb_Kolom.Items.Add("Kode Barang Akhir") : arrCari.Add("Kode_Barang_Plus")
        Cmb_Kolom.Items.Add("Satuan Minimum") : arrCari.Add("Satuan_Awal")
        Cmb_Kolom.Items.Add("Satuan Akhir") : arrCari.Add("Satuan_Akhir")

        Try
            OpenConn()

            SQL = "select * from binding_lokasi_gudang "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and gudang_default = 'Y' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Lokasi.Items.Add(Dr("Kode_Stock_Owner_Gudang"))
                Loop
            End Using

            Cmb_Lokasi.SelectedIndex = 0

            SQL = "select satuan from barang_detail_satuan "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Flag_Tampil_Display = 'Y' group by satuan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Satuan.Items.Add(Dr("satuan"))
                Loop
            End Using

            Lv_Data.Items.Clear()
            SQL = "select a.id_flever, a.Kode_Barang_Min, a.Kode_Barang_Plus, "
            SQL = SQL & "ISNULL((select top(1) nama from barang z where a.Kode_Perusahaan=z.Kode_Perusahaan and a.Kode_Barang_Min=z.Kode_Barang ),'') as Nama_Barang_Min,"
            SQL = SQL & "ISNULL((select top(1) nama from barang z where a.Kode_Perusahaan=z.Kode_Perusahaan and a.Kode_Barang_Plus=z.Kode_Barang ),'') as Nama_Barang_Plus,"
            SQL = SQL & "a.jumlah_awal as Jumlah_Minimum,"
            SQL = SQL & "a.Satuan_Awal as Satuan_Minimum,"
            SQL = SQL & "a.jumlah_akhir as Jumlah_Jadi,"
            SQL = SQL & "a.Satuan_Akhir as Satuan_Jadi,"
            SQL = SQL & "a.RV "
            SQL = SQL & "from emi_master_flever a "
            SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "group by a.Id_Flever, a.Kode_Barang_Min, a.Kode_Barang_Plus, a.jumlah_awal,a.Satuan_Awal, "
            SQL = SQL & "a.jumlah_akhir, a.Satuan_Akhir, a.RV, a.Kode_Perusahaan, a.Kode_Barang_Min, a.Kode_Barang_Plus "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("Id_Flever"))
                    Lv.SubItems.Add(Dr("Kode_Barang_Min"))
                    Lv.SubItems.Add(Dr("Nama_Barang_Min"))
                    Lv.SubItems.Add(Dr("Kode_Barang_Plus"))
                    Lv.SubItems.Add(Dr("Nama_Barang_Plus"))
                    Lv.SubItems.Add(Dr("Jumlah_Minimum"))
                    Lv.SubItems.Add(Dr("Satuan_Minimum"))
                    Lv.SubItems.Add(Dr("Jumlah_Jadi"))
                    Lv.SubItems.Add(Dr("Satuan_Jadi"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Get_ListView_Data(ByVal NoIndex As Integer)
        LvIdFlever = Lv_Data.Items(NoIndex).Text
        LvKdBrgAwal = Lv_Data.Items(NoIndex).SubItems(itemData_KdBrgAwal).Text
        LvNmBrgAwal = Lv_Data.Items(NoIndex).SubItems(itemData_NmBrgAwal).Text
        LvKdBrgAkhir = Lv_Data.Items(NoIndex).SubItems(itemData_KdBrgAkhir).Text
        LvNmBrgAkhir = Lv_Data.Items(NoIndex).SubItems(itemData_NmBrgAkhir).Text
        LvJmlDibutuhkan = Lv_Data.Items(NoIndex).SubItems(itemData_JmlDibutuhkan).Text
        LvSatuanMinimum = Lv_Data.Items(NoIndex).SubItems(itemData_SatuanMinimum).Text
        LvJmljadi = Lv_Data.Items(NoIndex).SubItems(itemData_JmlJadi).Text
        lvSatuanJadi = Lv_Data.Items(NoIndex).SubItems(itemData_SatuanJadi).Text
    End Sub

    Private Sub Get_ListView_Barang(ByVal NoIndex As Integer)
        LvBarangKdSo = Lv_DetailBarang.Items(NoIndex).Text
        LvBarangKdBrg = Lv_DetailBarang.Items(NoIndex).SubItems(itemBarang_KdBrg).Text
        LvBarangNmBrg = Lv_DetailBarang.Items(NoIndex).SubItems(itemBarang_NmBrg).Text
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If String.IsNullOrEmpty(Txt_KdBarangAwal.Text) Then
            MessageBox.Show("Kode Barang Awal Harus Dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarangAwal.Focus() : Exit Sub
        ElseIf String.IsNullOrEmpty(Txt_KdBarangAkhir.Text) Then
            MessageBox.Show("Kode Barang Akhir Harus Dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarangAkhir.Focus() : Exit Sub
        ElseIf String.IsNullOrEmpty(Txt_JmlhMaterialAwal.Text) Then
            MessageBox.Show("Jumlah Minimum Harus Diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_JmlhMaterialAwal.Focus() : Exit Sub
        ElseIf String.IsNullOrEmpty(Txt_HslMaterialAkhir.Text) Then
            MessageBox.Show("Jumlah Hasil Jadi Harus Diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_HslMaterialAkhir.Focus() : Exit Sub
        ElseIf Cmb_Satuan.SelectedIndex = -1 Then
            MessageBox.Show("Satuan Harus Diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Satuan.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction
            If Btn_Simpan.Tag = "&Simpan" Then
                '=======================================
                '=      CEK APAKAH DATA SUDAH ADA      =
                '=======================================

                SQL = "select * from emi_master_flever where Kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & " and Kode_Barang_Min='" & Txt_KdBarangAwal.Text & "' "
                SQL = SQL & "and Kode_Barang_Plus='" & Txt_KdBarangAkhir.Text & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.HasRows Then
                        CloseConn()
                        MessageBox.Show("Data Sudah Ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                '============= convert ke satuan kecil ============='
                Dim convertKeSatuanAsli_bhn As String = ""
                Dim jumlahConvertBhn As Double = 0

                SQL = "select satuan From barang where Kode_barang = '" & Txt_KdBarangAwal.Text & "' "
                ''SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & arrSo(Cmb_SoAwal.SelectedIndex) & "' "
                SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Cmb_Lokasi.Text & "' "
                Using Dr3 = OpenTrans(SQL)
                    If Dr3.Read Then
                        convertKeSatuanAsli_bhn = Dr3("satuan")
                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Txt_KdBarangAwal.Text & "',"
                        ''SQL = SQL & "'" & txtSatuanAwal.Text & "','" & Dr3("satuan") & "',"
                        SQL = SQL & "'" & Cmb_Satuan.Text & "','" & Dr3("satuan") & "',"
                        SQL = SQL & "" & HilangkanTanda(Txt_JmlhMaterialAwal.Text) & ") as Hasil "
                        Dr3.Close()

                        Using dr4 = OpenTrans(SQL)
                            If dr4.Read Then
                                If General_Class.CekNULL(dr4("Hasil")) <> "" Then
                                    If dr4("Hasil") = 0 Then
                                        dr4.Close()
                                        CloseTrans()
                                        CloseConn()
                                        ''MessageBox.Show("Satuan " & txtSatuanAwal.Text & " Ke " & convertKeSatuanAsli_bhn & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        MessageBox.Show("Satuan " & Cmb_Satuan.Text & " Ke " & convertKeSatuanAsli_bhn & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    Else
                                        jumlahConvertBhn = dr4("hasil")

                                    End If
                                Else
                                    dr4.Close()
                                    CloseTrans()
                                    CloseConn()
                                    ''MessageBox.Show("Satuan " & txtSatuanAwal.Text & " Ke " & convertKeSatuanAsli_bhn & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    MessageBox.Show("Satuan " & Cmb_Satuan.Text & " Ke " & convertKeSatuanAsli_bhn & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If
                        End Using
                    Else
                        Dr3.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                '============= convert ke satuan kecil ============='
                Dim convertSatuanAsli As String = ""
                Dim jumlahConvert As Double = 0

                SQL = "select satuan From barang where Kode_barang = '" & Txt_KdBarangAkhir.Text & "' "
                ''SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & arrSo(Cmb_SoAkhir.SelectedIndex) & "' "
                SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Cmb_Lokasi.Text & "' "
                Using Dr3 = OpenTrans(SQL)
                    If Dr3.Read Then
                        convertSatuanAsli = Dr3("satuan")
                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Txt_KdBarangAkhir.Text & "',"
                        ''SQL = SQL & "'" & txtSatuanAkhir.Text & "','" & Dr3("satuan") & "',"
                        SQL = SQL & "'" & Cmb_Satuan.Text & "','" & Dr3("satuan") & "',"
                        SQL = SQL & "" & HilangkanTanda(Txt_HslMaterialAkhir.Text) & ") as Hasil "
                        Dr3.Close()

                        Using dr4 = OpenTrans(SQL)
                            If dr4.Read Then
                                If General_Class.CekNULL(dr4("Hasil")) <> "" Then
                                    If dr4("Hasil") = 0 Then
                                        dr4.Close()
                                        CloseTrans()
                                        CloseConn()
                                        ''MessageBox.Show("Satuan " & txtSatuanAwal.Text & " Ke " & convertKeSatuanAsli_bhn & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        MessageBox.Show("Satuan " & Cmb_Satuan.Text & " Ke " & convertKeSatuanAsli_bhn & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    Else
                                        jumlahConvert = dr4("hasil")
                                    End If
                                Else
                                    dr4.Close()
                                    CloseTrans()
                                    CloseConn()
                                    ''MessageBox.Show("Satuan " & txtSatuanAwal.Text & " Ke " & convertKeSatuanAsli_bhn & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    MessageBox.Show("Satuan " & Cmb_Satuan.Text & " Ke " & convertKeSatuanAsli_bhn & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If
                        End Using
                    Else
                        Dr3.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                '=========================
                '=      INSERT DATA      =
                '=========================
                ''SQL = SQL & "'" & Txt_JmlhMin.Text & "','" & txtSatuanAwal.Text & "', '" & jumlahConvertBhn & "', '" & convertKeSatuanAsli_bhn & "' "
                ''SQL = SQL & ", '" & Txt_JmlhPlus.Text & "', '" & txtSatuanAkhir.Text & "', '" & jumlahConvert & "' , '" & convertSatuanAsli & "')"

                SQL = "insert into emi_master_flever (Kode_Perusahaan, Kode_Barang_Min, "
                SQL = SQL & "Kode_Barang_Plus, jumlah_awal,satuan_awal,jumlah_barang_awal,satuan_barang_awal, "
                SQL = SQL & "jumlah_akhir,satuan_akhir,jumlah_barang_akhir,satuan_barang_akhir ) values"
                SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_KdBarangAwal.Text & "', "
                SQL = SQL & "'" & Txt_KdBarangAkhir.Text & "', "
                SQL = SQL & "'" & Txt_JmlhMaterialAwal.Text & "','" & Cmb_Satuan.Text & "', '" & jumlahConvertBhn & "', '" & convertKeSatuanAsli_bhn & "' "
                SQL = SQL & ", '" & Txt_HslMaterialAkhir.Text & "', '" & Cmb_Satuan.Text & "', '" & jumlahConvert & "' , '" & convertSatuanAsli & "')"
                ExecuteTrans(SQL)
            Else 'UPDATE
                SQL = "update emi_master_flever set "
                SQL &= "Kode_Barang_Min = '" & Txt_KdBarangAwal.Text & "', "
                SQL &= "Kode_Barang_Plus = '" & Txt_KdBarangAkhir.Text & "', "
                SQL &= "jumlah_awal = '" & Txt_JmlhMaterialAwal.Text & "', "
                SQL &= "satuan_awal = '" & Cmb_Satuan.Text & "', "
                SQL &= "jumlah_akhir = '" & Txt_HslMaterialAkhir.Text & "', "
                SQL &= "satuan_akhir = '" & Cmb_Satuan.Text & "' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Flever = '" & Txt_IdFlever.Text & "' "
                ExecuteTrans(SQL)
            End If

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Berhasil Disimpan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub
    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cmb_Kolom.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_Kolom & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Cmb_Kolom.Focus() : Exit Sub
        ElseIf Txt_Value.Text.Trim.Length = 0 Then
            MessageBox.Show("Value" & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Value.Focus() : Exit Sub
        End If

        Cari("T")
    End Sub

    Private Sub Cari(ByVal semua As String)
        Try
            OpenConn()
            Lv_Data.Items.Clear()
            SQL = "select a.Id_Flever, a.Kode_Barang_Min, a.Kode_Barang_Plus, "
            SQL = SQL & "ISNULL((select top(1) nama from barang z where a.Kode_Perusahaan=z.Kode_Perusahaan and a.Kode_Barang_Min=z.Kode_Barang ),'') as Nama_Barang_Min,"
            SQL = SQL & "ISNULL((select top(1) nama from barang z where a.Kode_Perusahaan=z.Kode_Perusahaan and a.Kode_Barang_Plus=z.Kode_Barang ),'') as Nama_Barang_Plus,"
            SQL = SQL & "a.jumlah_awal as Jumlah_Minimum,"
            SQL = SQL & "a.Satuan_Awal as Satuan_Minimum,"
            SQL = SQL & "a.jumlah_akhir as Jumlah_Jadi,"
            SQL = SQL & "a.Satuan_Akhir as Satuan_Jadi,"
            SQL = SQL & "a.RV "
            SQL = SQL & "from emi_master_flever a "
            SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' "
            If semua = "T" Then
                SQL = SQL & "and " & arrCari.Item(Cmb_Kolom.SelectedIndex) & " like '%" & Txt_Value.Text & "%' "
                SQL = SQL & "group by a.Id_Flever, a.Kode_Barang_Min, a.Kode_Barang_Plus, a.jumlah_awal,a.Satuan_Awal, "
                SQL = SQL & "a.jumlah_akhir, a.Satuan_Akhir, a.RV, a.Kode_Perusahaan, a.Kode_Barang_Min, a.Kode_Barang_Plus "
                SQL = SQL & "order by a.Id_Flever"
            Else
                SQL = SQL & "group by a.Id_Flever, a.Kode_Barang_Min, a.Kode_Barang_Plus, a.jumlah_awal,a.Satuan_Awal, "
                SQL = SQL & "a.jumlah_akhir, a.Satuan_Akhir, a.RV, a.Kode_Perusahaan, a.Kode_Barang_Min, a.Kode_Barang_Plus "
                SQL = SQL & "order by a.Id_Flever"
            End If

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("Id_Flever"))
                    Lv.SubItems.Add(Dr("Kode_Barang_Min"))
                    Lv.SubItems.Add(Dr("Nama_Barang_Min"))
                    Lv.SubItems.Add(Dr("Kode_Barang_Plus"))
                    Lv.SubItems.Add(Dr("Nama_Barang_Plus"))
                    Lv.SubItems.Add(Dr("Jumlah_Minimum"))
                    Lv.SubItems.Add(Dr("Satuan_Minimum"))
                    Lv.SubItems.Add(Dr("Jumlah_Jadi"))
                    Lv.SubItems.Add(Dr("Satuan_Jadi"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs)
        '''If Lv_Data.Items.Count = 0 Then Exit Sub

        '''Dim Tanya_Cetak As String = MessageBox.Show("Yakin ingin Hapus?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        '''If Tanya_Cetak = vbNo Then
        '''    Exit Sub
        '''End If

        '''Get_ListView_Data(Lv_Data.FocusedItem.Index)

        '''Try
        '''    OpenConn()

        '''    SQL = "delete from emi_master_flever where Kode_Perusahaan='" & KodePerusahaan & "' "
        '''    SQL = SQL & "and Kode_Stock_Owner_Min='" & LvData_SoAwal & "' and Kode_Barang_Min='" & LvData_KdBarangAwal & "' "
        '''    SQL = SQL & "and Kode_Stock_Owner_Plus='" & LvData_SoAkhir & "' and Kode_Barang_Plus='" & LvData_KdBarangAkhir & "'"
        '''    ExecuteTrans(SQL)

        '''    CloseConn()
        '''Catch ex As Exception
        '''    CloseConn()
        '''    MessageBox.Show(ex.Message)
        '''    Exit Sub
        '''End Try

        '''kosong()
    End Sub

    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus1 = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction

                SQL = "Delete From EMI_Master_Flever where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Flever = '" & Txt_IdFlever.Text.Trim & "' "
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Hapus, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        kosong()
        Txt_KdBarangAwal.Focus()
    End Sub



    Private Sub Txt_KdBarangAwal_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarangAwal.TextChanged
        If isFillingData Then Exit Sub

        If Cmb_Lokasi.SelectedIndex = -1 Then Exit Sub

        If Txt_KdBarangAwal.Text = "" Then
            Txt_NamaBarangAwal.Text = String.Empty
            Lv_DetailBarang.Visible = False
            Lv_DetailBarang.Location = New Point(880, 191)
            Lv_DetailBarang.Items.Clear()
            Exit Sub
        End If

        Try
            OpenConn()

            Lv_DetailBarang.Items.Clear()

            SQL = "select Kode_Stock_Owner, Kode_Barang, Nama from Barang "
            SQL = SQL & "where Kode_Stock_Owner='" & Cmb_Lokasi.Text & "' "
            SQL = SQL & "and nama like '" & Txt_KdBarangAwal.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As ListViewItem
                    lv = Lv_DetailBarang.Items.Add(Dr("Kode_Stock_Owner"))
                    lv.SubItems.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            Lv_DetailBarang.Visible = True
            Lv_DetailBarang.Location = New Point(216, 116)
            Lv_DetailBarang.Tag = "BARANG AWAL"

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub



    Private Sub Txt_KdBarangAkhir_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarangAkhir.TextChanged
        If isFillingData Then Exit Sub

        If Cmb_Lokasi.SelectedIndex = -1 Then Exit Sub

        If Txt_KdBarangAkhir.Text = "" Then
            Txt_NamaBarangAkhir.Text = String.Empty
            Lv_DetailBarang.Visible = False
            Lv_DetailBarang.Location = New Point(880, 191)
            Lv_DetailBarang.Items.Clear()
            Exit Sub
        End If

        Try
            OpenConn()

            Lv_DetailBarang.Items.Clear()

            SQL = "select Kode_Stock_Owner, Kode_Barang, Nama from Barang "
            ''SQL = SQL & "where Kode_Stock_Owner='" & arrSo(Cmb_SoAkhir.SelectedIndex) & "' "
            SQL = SQL & "where Kode_Stock_Owner='" & Cmb_Lokasi.Text & "' "
            SQL = SQL & "and nama like '" & Txt_KdBarangAkhir.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As ListViewItem
                    lv = Lv_DetailBarang.Items.Add(Dr("Kode_Stock_Owner"))
                    lv.SubItems.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            Lv_DetailBarang.Visible = True
            Lv_DetailBarang.Location = New Point(216, 194)
            Lv_DetailBarang.Tag = "BARANG AKHIR"

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub Lv_DetailBarang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DetailBarang.DoubleClick

        If Lv_DetailBarang.Items.Count = 0 Then Exit Sub

        Get_ListView_Barang(Lv_DetailBarang.FocusedItem.Index)

        If Lv_DetailBarang.Tag = "BARANG AWAL" Then
            Txt_KdBarangAwal.Text = LvBarangKdBrg
            Txt_NamaBarangAwal.Text = LvBarangNmBrg

            Try
                OpenConn()

                SQL = "select satuan from barang_detail_satuan a "
                SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.kode_barang = '" & Txt_KdBarangAwal.Text & "' "
                SQL = SQL & "and a.flag_tampil_display = 'Y' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        ''txtSatuanAwal.Text = dr("satuan")
                        Cmb_Satuan.Text = dr("satuan")
                    Else
                        CloseConn()
                        MessageBox.Show("Detail satuan barang tidak ada")
                        Txt_KdBarangAwal.Focus()
                        Exit Sub
                    End If
                End Using

                Txt_KdBarangAkhir.Focus()

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try


        ElseIf Lv_DetailBarang.Tag = "BARANG AKHIR" Then
            Txt_KdBarangAkhir.Text = LvBarangKdBrg
            Txt_NamaBarangAkhir.Text = LvBarangNmBrg

            Try
                OpenConn()

                SQL = "select satuan from barang_detail_satuan a "
                SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.kode_barang = '" & Txt_KdBarangAkhir.Text & "' "
                SQL = SQL & "and a.flag_tampil_display = 'Y' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        ''txtSatuanAkhir.Text = dr("satuan")
                        Cmb_Satuan.Text = dr("satuan")
                    Else
                        CloseConn()
                        MessageBox.Show("Detail satuan barang tidak ada")
                        Txt_KdBarangAkhir.Focus()
                        Exit Sub
                    End If
                End Using

                Txt_JmlhMaterialAwal.Focus()

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If

        Lv_DetailBarang.Items.Clear()
        Lv_DetailBarang.Tag = ""
        Lv_DetailBarang.Visible = False
        Lv_DetailBarang.Location = New Point(880, 191)

    End Sub

    Private Sub Cmb_SoAwal_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then Txt_KdBarangAwal.Focus() : Exit Sub
    End Sub
    ''Private Sub Txt_KdBarangAwal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarangAwal.KeyPress
    ''    If e.KeyChar = Chr(13) Then Cmb_Lokasi.Focus() : Exit Sub
    ''End Sub
    Private Sub Cmb_SoAkhir_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then Txt_KdBarangAkhir.Focus() : Exit Sub
    End Sub

    Private Sub Txt_JmlhPlus_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_HslMaterialAkhir.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus() : Exit Sub
    End Sub


    Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick
        isFillingData = True

        If Lv_Data.Items.Count = 0 Then Exit Sub

        Lv_DetailBarang.Items.Clear()
        Lv_DetailBarang.Tag = ""
        Lv_DetailBarang.Visible = False
        Lv_DetailBarang.Location = New Point(880, 191)

        Txt_IdFlever.Text = Lv_Data.FocusedItem.Text
        Txt_KdBarangAwal.Text = Lv_Data.FocusedItem.SubItems(itemData_KdBrgAwal).Text
        Txt_NamaBarangAwal.Text = Lv_Data.FocusedItem.SubItems(itemData_NmBrgAwal).Text
        Txt_KdBarangAkhir.Text = Lv_Data.FocusedItem.SubItems(itemData_KdBrgAkhir).Text
        Txt_NamaBarangAkhir.Text = Lv_Data.FocusedItem.SubItems(itemData_NmBrgAkhir).Text
        Txt_JmlhMaterialAwal.Text = Lv_Data.FocusedItem.SubItems(itemData_JmlDibutuhkan).Text
        Txt_HslMaterialAkhir.Text = Lv_Data.FocusedItem.SubItems(itemData_JmlJadi).Text
        Cmb_Satuan.Text = Lv_Data.FocusedItem.SubItems(itemData_SatuanJadi).Text

        isFillingData = False

        Txt_IdFlever_Leave(Lv_Data, e)
    End Sub

    Private Sub Txt_IdFlever_TextChanged(sender As Object, e As EventArgs) Handles Txt_IdFlever.TextChanged

    End Sub

    Private Sub Txt_IdFlever_Leave(sender As Object, e As EventArgs) Handles Txt_IdFlever.Leave
        If Txt_IdFlever.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            SQL = "select a.id_flever, a.Kode_Barang_Min, a.Kode_Barang_Plus, "
            SQL = SQL & "ISNULL((select top(1) nama from barang z where a.Kode_Perusahaan=z.Kode_Perusahaan and a.Kode_Barang_Min=z.Kode_Barang ),'') as Nama_Barang_Min,"
            SQL = SQL & "ISNULL((select top(1) nama from barang z where a.Kode_Perusahaan=z.Kode_Perusahaan and a.Kode_Barang_Plus=z.Kode_Barang ),'') as Nama_Barang_Plus,"
            SQL = SQL & "a.jumlah_awal as Jumlah_Minimum,"
            SQL = SQL & "a.Satuan_Awal as Satuan_Minimum,"
            SQL = SQL & "a.jumlah_akhir as Jumlah_Jadi,"
            SQL = SQL & "a.Satuan_Akhir as Satuan_Jadi,"
            SQL = SQL & "a.RV "
            SQL = SQL & "from emi_master_flever a "
            SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.id_flever = '" & Txt_IdFlever.Text.Trim & "' "
            SQL = SQL & "group by a.Id_Flever, a.Kode_Barang_Min, a.Kode_Barang_Plus, a.jumlah_awal,a.Satuan_Awal, "
            SQL = SQL & "a.jumlah_akhir, a.Satuan_Akhir, a.RV, a.Kode_Perusahaan, a.Kode_Barang_Min, a.Kode_Barang_Plus "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Txt_IdFlever.Text = Dr("id_flever")
                    Txt_KdBarangAwal.Text = Dr("Kode_Barang_Min")
                    Txt_NamaBarangAwal.Text = Dr("Nama_Barang_Min")
                    Txt_KdBarangAkhir.Text = Dr("Kode_Barang_Plus")
                    Txt_NamaBarangAkhir.Text = Dr("Nama_Barang_Plus")
                    Txt_JmlhMaterialAwal.Text = Dr("Jumlah_Minimum")
                    Txt_HslMaterialAkhir.Text = Dr("Jumlah_Jadi")
                    Cmb_Satuan.Text = Dr("Satuan_Jadi")

                    Btn_Simpan.Text = "&Update" : Btn_Hapus.Enabled = True
                    Btn_Simpan.Tag = "&Update"
                Else
                    Txt_IdFlever.Text = ""
                    Txt_KdBarangAwal.Text = ""
                    Txt_NamaBarangAwal.Text = ""
                    Txt_KdBarangAkhir.Text = ""
                    Txt_NamaBarangAkhir.Text = ""
                    Txt_JmlhMaterialAwal.Text = ""
                    Txt_HslMaterialAkhir.Text = ""
                    Cmb_Satuan.SelectedIndex = -1

                    Btn_Simpan.Text = "&Simpan" : Btn_Hapus.Enabled = False
                    Btn_Simpan.Tag = "&Simpan"
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

End Class