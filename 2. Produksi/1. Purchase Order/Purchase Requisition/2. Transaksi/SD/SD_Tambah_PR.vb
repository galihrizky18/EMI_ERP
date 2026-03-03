Imports System.ComponentModel
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button


Public Class SD_Tambah_PR
    Public filter_tambahan, filter_kdSupplier As String
    Public asal, faktur_MR As String
    Dim arrcari As New ArrayList
    Dim Jenis = "Tampil_Barang"

    'Private Purchase_Requisition As Purchase_Requisition

    '' Buat constructor yang menerima referensi form utama
    'Public Sub New(ByVal formUtamaRef As Purchase_Requisition)
    '    ' Inisialisasi form utama
    '    InitializeComponent()
    '    Purchase_Requisition = formUtamaRef
    'End Sub

    'Private formUtama As Object

    '' Constructor yang menerima referensi form utama
    'Public Sub New(ByVal formUtamaRef As Form)
    '    InitializeComponent()
    '    formUtama = formUtamaRef
    'End Sub

    Public Sub kosong()
        TxtTiba.Text = ""
        Try
            OpenConn()

            CmbPilihBarang_Lokasi.Items.Clear()

            SQL = "select Kode_Stock_Owner from view_lokasi_stock where Aktif='Y' "
            SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbPilihBarang_Lokasi.Items.Add(dr("Kode_Stock_Owner"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'If asal = "SD_Data_PR" Then
        '    LvPilihBarang_DataBarang.Items.Clear()
        '    TxtPilihBarang_KodeBarang.Text = ""
        '    TxtPilihBarang_NamaBarang.Text = ""
        '    TxtPilihBarang_Satuan.Text = ""
        '    'DateTimePicker1.ResetText()
        '    txtKeterangan.Text = ""
        '    txtJumlah.Focus()

        '    Try
        '        OpenConn()
        '        CmbPilihBarang_Satuan.Items.Clear()
        '        SQL = "select Satuan from barang_Detail_Satuan where Kode_Barang = '" & Trim(Lbl_GetKdBrg.Text) & "' "
        '        Using dr = OpenTrans(SQL)
        '            Do While dr.Read
        '                CmbPilihBarang_Satuan.Items.Add(dr("satuan"))
        '            Loop
        '        End Using
        '        CloseConn()
        '    Catch ex As Exception
        '        CloseConn()
        '        MessageBox.Show(ex.Message)
        '        Exit Sub
        '    End Try

        'End If

    End Sub

    Public Sub lokasi()
        Try
            OpenConn()

            CmbPilihBarang_Lokasi.Items.Clear()

            SQL = "select Kode_Stock_Owner from view_lokasi_stock where Aktif='Y' "
            SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbPilihBarang_Lokasi.Items.Add(dr("Kode_Stock_Owner"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try

    End Sub

    Private Sub SD_Pilih_Barang_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
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

            LblPilihBarang_Judul.Text = Base_Language.Lang_TampilBarang_Judul
            LblPilihBarang_Lokasi.Text = Base_Language.Lang_Global_Lokasi
            LblPilihBarang_KodeBarang.Text = Base_Language.Lang_Global_KodeBarang
            LblPilihBarang_NamaBarang.Text = Base_Language.Lang_Global_NamaBarang
            LblPilihBarang_Satuan.Text = Base_Language.Lang_Global_Satuan

            BtnPilihBarang_Simpan.Text = Base_Language.Lang_Global_Simpan
            BtnPilihBarang_Refresh.Text = Base_Language.Lang_Global_Refresh

            TxtTiba.Text = ""
            If asal = "Purchase_Requisition" Then
                LvPilihBarang_DataBarang.Items.Clear()
                TxtPilihBarang_KodeBarang.Text = ""
                TxtPilihBarang_NamaBarang.Text = ""
                TxtPilihBarang_Satuan.Text = ""
                DateTimePicker1.ResetText()
                txtJumlah.Text = ""
                txtKeterangan.Text = ""
                Txt_KDSo.Text = ""
                CmbPilihBarang_Satuan.Items.Clear()
                TxtPilihBarang_KodeBarang.Focus()
                TxtPilihBarang_KodeBarang.Enabled = True
                TxtPilihBarang_NamaBarang.Enabled = False
                TxtPilihBarang_Satuan.Enabled = False
                txtJumlah.Enabled = True

                Try
                    OpenConn()

                    CmbPilihBarang_Lokasi.Items.Clear()

                    SQL = "select Kode_Stock_Owner from view_lokasi_stock where Aktif='Y' "
                    SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
                    Using dr = OpenTrans(SQL)
                        Do While dr.Read
                            CmbPilihBarang_Lokasi.Items.Add(dr("Kode_Stock_Owner"))
                        Loop
                    End Using

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

        LvPilihBarang_DataBarang.Visible = False
        LvPilihBarang_DataBarang.Location = New Point(139, 96)

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs)
        kosong()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then TxtPilihBarang_NamaBarang.Focus()
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then BtnPilihBarang_Simpan.Focus()
    End Sub

    Private Sub TxtKode_TextChanged(sender As Object, e As EventArgs) Handles TxtPilihBarang_KodeBarang.TextChanged
        If asal = "Purchase_Requisition" Then
            If TxtPilihBarang_KodeBarang.Text.Length >= 3 Then
                If TxtPilihBarang_KodeBarang.Text.Trim.Length = 0 Then
                    LvPilihBarang_DataBarang.Visible = False : Exit Sub
                Else
                    LvPilihBarang_DataBarang.Visible = True
                End If

                If TxtPilihBarang_KodeBarang.Text.Trim.Length = 0 Then
                    LvPilihBarang_DataBarang.Visible = False : Exit Sub
                Else
                    LvPilihBarang_DataBarang.Visible = True
                End If

                txtJumlah.Text = ""
                TxtPilihBarang_NamaBarang.Text = ""
                CmbPilihBarang_Lokasi.SelectedIndex = -1
                CmbPilihBarang_Satuan.SelectedIndex = -1

                Try
                    OpenConn()

                    LvPilihBarang_DataBarang.Items.Clear()
                    Dim Lvw As ListViewItem

                    SQL = "select a.Kode_Barang,a.Nama, a.Satuan, c.lokasi_gudang  "
                    SQL = SQL & "from barang a, EMI_Group_Jenis b, EMI_Kategori_Gudang_PerLokasi c where a.Kode_Perusahaan=b.Kode_Perusahaan  "
                    SQL = SQL & "and a.Id_Group_Jenis=b.Id_Group_Jenis and a.Kode_Perusahaan='" & KodePerusahaan & "'  "
                    SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang "
                    SQL = SQL & "and Nama like '%" & TxtPilihBarang_KodeBarang.Text & "%' and aktif = 'Y' and (b.Flag_Raw_Material = 'Y' or b.Flag_Packaging = 'Y' or b.Flag_bahan_bakar = 'Y') " & filter_tambahan & " "
                    SQL = SQL & "group by a.Kode_Barang,a.Nama, a.Satuan,c.lokasi_gudang "
                    Using dr = OpenTrans(SQL)
                        Do While dr.Read
                            Lvw = LvPilihBarang_DataBarang.Items.Add(dr("lokasi_gudang"))
                            Lvw.SubItems.Add(dr("kode_barang"))
                            Lvw.SubItems.Add(dr("Nama"))
                            Lvw.SubItems.Add(dr("satuan"))
                        Loop
                    End Using

                    CloseConn()
                Catch ex As Exception
                    CloseConn()
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try
            Else
                LvPilihBarang_DataBarang.Visible = False
            End If
        End If

    End Sub

    Private Sub TxtKode_Leave(sender As Object, e As EventArgs) Handles TxtPilihBarang_KodeBarang.Leave

        If TxtPilihBarang_KodeBarang.Text.Trim.Length = 0 Then Exit Sub
        If LvPilihBarang_DataBarang.Focused = True Then Exit Sub

        get_jam()

        Try
            OpenConn()

            SQL = "select a.Kode_Barang,a.Nama, a.Satuan,c.lokasi_gudang "
            SQL = SQL & "from barang a, EMI_Group_Jenis b, EMI_Kategori_Gudang_PerLokasi c where a.Kode_Perusahaan=b.Kode_Perusahaan  "
            SQL = SQL & "and a.Id_Group_Jenis=b.Id_Group_Jenis and a.Kode_Perusahaan='" & KodePerusahaan & "'  "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang "
            SQL = SQL & "and a.kode_barang like '%" & TxtPilihBarang_KodeBarang.Text & "%' and aktif = 'Y' and (b.Flag_Raw_Material = 'Y' or b.Flag_Packaging = 'Y'  or b.Flag_bahan_bakar = 'Y') " & filter_tambahan & " "
            SQL = SQL & "group by a.Kode_Barang,a.Nama, a.Satuan,c.lokasi_gudang"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    CmbPilihBarang_Lokasi.Text = dr("lokasi_gudang")
                    TxtPilihBarang_KodeBarang.Text = dr("kode_barang")
                    TxtPilihBarang_NamaBarang.Text = dr("nama")
                    TxtPilihBarang_Satuan.Text = dr("Satuan")
                    dr.Close()
                    CmbPilihBarang_Satuan.Items.Clear()

                    Dim satuan As String = ""
                    Dim indexTampilDisplay As Integer
                    Dim index As Integer = 0
                    SQL = "select Satuan,Flag_Tampil_Display from barang_Detail_Satuan where Kode_Barang= '" & Trim(TxtPilihBarang_KodeBarang.Text) & "' "
                    Using dr2 = OpenTrans(SQL)
                        Do While dr2.Read
                            CmbPilihBarang_Satuan.Items.Add(dr2("satuan"))

                            If General_Class.CekNULL(dr2("Flag_Tampil_Display")) = "Y" Then
                                indexTampilDisplay = index
                            End If

                            index = index + 1
                        Loop
                    End Using

                    SQL = "select top(1) Waktu_Pengiriman from emi_detail_proses_pengiriman_po where Kode_Barang= '" & Trim(TxtPilihBarang_KodeBarang.Text) & "' "
                    SQL = SQL & "order by Waktu_Pengiriman Desc "
                    Using dr2 = OpenTrans(SQL)
                        If dr2.Read Then
                            TxtTiba.Text = dr2("Waktu_Pengiriman")
                        Else
                            TxtTiba.Text = 0
                        End If
                    End Using


                    CmbPilihBarang_Satuan.SelectedIndex = indexTampilDisplay
                    CmbPilihBarang_Satuan.Enabled = False
                    'BtnPilihBarang_Simpan.Focus()
                    txtJumlah.Focus()

                    LvPilihBarang_DataBarang.Visible = False
                Else
                    TxtPilihBarang_KodeBarang.Text = ""
                    TxtPilihBarang_NamaBarang.Text = ""
                    TxtPilihBarang_Satuan.Text = ""
                    CmbPilihBarang_Satuan.Items.Clear()
                    TxtPilihBarang_KodeBarang.Focus()
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        DateTimePicker1.Value = DateAdd(DateInterval.Day, Val(TxtTiba.Text) + 1, tgl_skg)


    End Sub
    ''
    Private Sub TxtKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPilihBarang_KodeBarang.KeyPress
        If e.KeyChar = Chr(13) Then CmbPilihBarang_Satuan.Focus()

    End Sub

    Private Sub TxtKode_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPilihBarang_KodeBarang.KeyDown
        If e.KeyCode = Keys.Down Then
            If LvPilihBarang_DataBarang.Items.Count = 0 Then Exit Sub
            LvPilihBarang_DataBarang.Focus()
        End If
    End Sub

    Private Sub Lv2_DoubleClick(sender As Object, e As EventArgs) Handles LvPilihBarang_DataBarang.DoubleClick
        If LvPilihBarang_DataBarang.Items.Count = 0 Then Exit Sub

        TxtPilihBarang_KodeBarang.Text = LvPilihBarang_DataBarang.FocusedItem.SubItems(1).Text
        TxtPilihBarang_KodeBarang.Focus() : DateTimePicker1.Focus()
        LvPilihBarang_DataBarang.Visible = False

    End Sub

    Private Sub Lv2_KeyDown(sender As Object, e As KeyEventArgs) Handles LvPilihBarang_DataBarang.KeyDown
        If e.KeyCode = Keys.Enter Then Lv2_DoubleClick(LvPilihBarang_DataBarang, e)
    End Sub

    Private Sub Btn_Simpan_Click_1(sender As Object, e As EventArgs) Handles BtnPilihBarang_Simpan.Click

        If asal = "SD_Data_PR" Then
            'If txtJumlah.Text.Trim.Length = 0 Then
            '    MessageBox.Show(Base_Language.Lang_Global_Error_Jumlah, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    txtJumlah.Focus()
            '    Exit Sub
            If CmbPilihBarang_Satuan.Text.Trim.Length = 0 Then
                MessageBox.Show(Base_Language.Lang_Global_Satuan & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                CmbPilihBarang_Satuan.Focus()
                Exit Sub
                'ElseIf txtKeterangan.Text.Trim.Length = 0 Then
                '    MessageBox.Show(Base_Language.lang_global_keterangan & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '    txtKeterangan.Focus()
                '    Exit Sub
            End If


            ' SD_Data_PR.DataGridView1.CurrentRow.Cells(7).Value = txtJumlah.Text
            SD_Data_PR.DataGridView1.CurrentRow.Cells(7).Value = CmbPilihBarang_Satuan.Text
            SD_Data_PR.DataGridView1.CurrentRow.Cells(8).Value = Format(DateTimePicker1.Value, "dd MMM yyyy")
            'SD_Data_PR.DataGridView1.CurrentRow.Cells(10).Value = txtKeterangan.Text
            Me.Close()

        ElseIf asal = "Purchase_Requisition" Then

            Dim Sisa As Double = 0

            If TxtPilihBarang_KodeBarang.Text.Trim.Length = 0 Then
                MessageBox.Show(Base_Language.Lang_Global_KodeBarang & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TxtPilihBarang_KodeBarang.Focus()
                Exit Sub
            ElseIf TxtPilihBarang_NamaBarang.Text.Trim.Length = 0 Then
                MessageBox.Show(Base_Language.Lang_Global_NamaBarang & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TxtPilihBarang_NamaBarang.Focus()
                Exit Sub
            ElseIf CmbPilihBarang_Satuan.Text.Trim.Length = 0 Then
                MessageBox.Show(Base_Language.Lang_Global_Satuan & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                CmbPilihBarang_Satuan.Focus()
                Exit Sub
            ElseIf CmbPilihBarang_Lokasi.Text.Trim.Length = 0 Then
                MessageBox.Show(Base_Language.Lang_Global_Lokasi & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                CmbPilihBarang_Lokasi.Focus()
                Exit Sub
                'ElseIf txtJumlah.Text.Trim.Length = 0 Then
                '    MessageBox.Show(Base_Language.Lang_Global_Error_Jumlah, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    txtJumlah.Focus()
                '    Exit Sub
            End If

            If Not faktur_MR = "" Then

                '=======================================
                '=     CEK APAKAH BARANG ADA DI MR     =
                '=======================================
                Try
                    OpenConn()

                    SQL = "select Kode_Barang from EMI_Transaksi_Material_Requsition_detail where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and Kode_Barang = '" & TxtPilihBarang_KodeBarang.Text & "' and No_Faktur = '" & faktur_MR & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then

                        Else
                            CloseConn()
                            MessageBox.Show("Barang Tidak Ada Dalam Material Requisition", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    '=======================
                    '=     HITUNG SISA     =
                    '=======================
                    SQL = "select a.Nilai_PPIC, "
                    SQL = SQL & "isnull((select sum(y.jumlah) from EMI_Purchase_Requisition x, EMI_Purchase_Requisition_Detail y "
                    SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and y.Kode_Perusahaan = a.Kode_Perusahaan "
                    SQL = SQL & "and y.Kode_Stock_Owner = a.Kode_Stock_Owner and y.Kode_Barang = a.Kode_Barang and x.Status is null and a.No_Faktur = x.no_fak_material_requisition "
                    SQL = SQL & "), 0) as jumlah_pr "
                    SQL = SQL & "from EMI_Transaksi_Material_Requsition_detail a "
                    SQL = SQL & "where a.Kode_Perusahaan = '001' "
                    SQL = SQL & "and a.Kode_Barang= '" & TxtPilihBarang_KodeBarang.Text & "' "
                    SQL = SQL & "and a.No_Faktur = '" & faktur_MR & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Sisa = Val(Dr("Nilai_PPIC")) - Val(Dr("jumlah_pr"))
                        Else
                            CloseConn()
                            MessageBox.Show("Ada Masalah Pada Sisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    CloseConn()
                Catch ex As Exception
                    CloseConn()
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try

            End If



            For i As Integer = 0 To N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows.Count - 1
                If TxtPilihBarang_KodeBarang.Text.Trim = N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(i).Cells(1).Value Then
                    If CmbPilihBarang_Satuan.Text = N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(i).Cells(4).Value Then
                        If Format(DateTimePicker1.Value, "dd MMM yyyy") = N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(i).Cells(5).Value Then
                            N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(i).Cells(3).Value = Val(N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(i).Cells(3).Value) + Val(txtJumlah.Text)
                            Me.Close()
                            Exit Sub
                        End If
                    End If
                End If
            Next

            Dim index As Integer = 0

            index = N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows.Count - 1

            Dim jumlahIndexDGv As Integer

            N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows.Add(1)

            'buat ambil jumlah dgv nya
            jumlahIndexDGv = N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows.Count - 1

            'Purchase_Requisition
            N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(index).Cells(0).Value = CmbPilihBarang_Lokasi.Text
            N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(index).Cells(1).Value = TxtPilihBarang_KodeBarang.Text
            N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(index).Cells(2).Value = TxtPilihBarang_NamaBarang.Text
            'Purchase_Requisition.Dgv_DataBarang.Rows(index).Cells(3).Value = txtJumlah.Text.Trim
            N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(index).Cells(3).Value = 0
            N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(index).Cells(4).Value = CmbPilihBarang_Satuan.Text
            N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(index).Cells(5).Value = Format(DateTimePicker1.Value, "dd MMM yyyy")
            N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(index).Cells(6).Value = txtKeterangan.Text.Trim
            N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(index).Cells(7).Value = Sisa
            N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(index).Cells(9).Value = TxtTiba.Text

            N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(jumlahIndexDGv).Cells(0).ReadOnly = True
            N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(jumlahIndexDGv).Cells(1).ReadOnly = True
            N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(jumlahIndexDGv).Cells(2).ReadOnly = True
            N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(jumlahIndexDGv).Cells(3).ReadOnly = False
            N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(jumlahIndexDGv).Cells(4).ReadOnly = True
            N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(jumlahIndexDGv).Cells(5).ReadOnly = True
            N_EMI_Transaksi_Purchase_Requisition_Trial.Dgv_DataBarang.Rows(jumlahIndexDGv).Cells(6).ReadOnly = False

            'Dim pengali As Double = 0
            'Try
            '    OpenConn()

            '    SQL = "select Nilai_Pengali from emi_satuan_detail_perhitungan "
            '    SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and "
            '    SQL = SQL & "Satuan_Awal='" & CmbPilihBarang_Satuan.Text & "' and satuan_akhir='" & TxtPilihBarang_Satuan.Text & "' "
            '    Using dr = OpenTrans(SQL)
            '        If dr.Read Then
            '            pengali = dr("Nilai_Pengali")
            '        Else
            '            dr.Close()
            '            CloseConn()
            '            MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '            Exit Sub
            '        End If
            '    End Using
            '    CloseConn()
            'Catch ex As Exception
            '    CloseConn()
            '    MessageBox.Show(ex.Message)
            '    Exit Sub
            'End Try

            'If asal = "Transaksi_Formulator" Then
            '    Transaksi_Formula.DgvFormulator_StepFormulator.CurrentRow.Cells(Transaksi_Formula.cellQty).Value = ""
            '    Transaksi_Formula.DgvFormulator_StepFormulator.CurrentRow.Cells(Transaksi_Formula.cellPersentase).Value = ""
            '    Transaksi_Formula.DgvFormulator_StepFormulator.CurrentRow.Cells(Transaksi_Formula.cellQty_SatHasil).Value = ""
            '    Transaksi_Formula.DgvFormulator_StepFormulator.CurrentRow.Cells(Transaksi_Formula.cellKdBarang).Value = TxtPilihBarang_KodeBarang.Text
            '    Transaksi_Formula.DgvFormulator_StepFormulator.CurrentRow.Cells(Transaksi_Formula.cellNama).Value = TxtPilihBarang_NamaBarang.Text
            '    Transaksi_Formula.DgvFormulator_StepFormulator.CurrentRow.Cells(Transaksi_Formula.cellSatuan).Value = CmbPilihBarang_Satuan.Text
            '    Transaksi_Formula.DgvFormulator_StepFormulator.CurrentRow.Cells(Transaksi_Formula.cellPengali).Value = pengali
            '    Transaksi_Formula.DgvFormulator_StepFormulator.CurrentRow.Cells(Transaksi_Formula.cellSatuanBarang).Value = TxtPilihBarang_Satuan.Text
            '    Transaksi_Formula.isi_barang = True
            'ElseIf asal = "Master_Penawaran" Then

            '    For index = 0 To Master_Penawaran.DgvMaster_Penawaran.Rows.Count - 1
            '        If Master_Penawaran.DgvMaster_Penawaran.Rows(index).Cells(Master_Penawaran.cellKdBrg).Value = TxtPilihBarang_KodeBarang.Text Then
            '            MessageBox.Show("Barang Sudah ada")
            '            Exit Sub
            '        End If
            '    Next

            '    Dim rows As Double = Master_Penawaran.DgvMaster_Penawaran.Rows.Count
            '    Master_Penawaran.DgvMaster_Penawaran.Rows.Add(1)
            '    Master_Penawaran.DgvMaster_Penawaran.Rows(rows).Cells(Master_Penawaran.cellKdBrg).Value = TxtPilihBarang_KodeBarang.Text
            '    Master_Penawaran.DgvMaster_Penawaran.Rows(rows).Cells(Master_Penawaran.cellNmBrg).Value = TxtPilihBarang_NamaBarang.Text


            '    Dim dgvcc As DataGridViewComboBoxCell
            '    dgvcc = Master_Penawaran.DgvMaster_Penawaran.Rows(rows).Cells(Master_Penawaran.cellSatuan)
            '    dgvcc.Items.Clear()

            '    Try
            '        OpenConn()

            '        SQL = "select satuan from barang_detail_Satuan where Kode_Barang ='" & TxtPilihBarang_KodeBarang.Text & "'  and Kode_Perusahaan='" & KodePerusahaan & "' "
            '        Using dr2 = OpenTrans(SQL)
            '            Do While dr2.Read
            '                dgvcc.Items.Add(dr2("satuan"))
            '            Loop
            '        End Using

            '        CloseConn()
            '    Catch ex As Exception
            '        CloseConn()
            '        MessageBox.Show(ex.Message)
            '        Exit Sub
            '    End Try


            '    Master_Penawaran.DgvMaster_Penawaran.Rows(rows).Cells(Master_Penawaran.cellSatuan).Value = CmbPilihBarang_Satuan.Text
            '    Master_Penawaran.DgvMaster_Penawaran.Rows(rows).Cells(Master_Penawaran.cellSatuan).ReadOnly = False

            '    Master_Penawaran.Lbl_GetKdBrg.Text = TxtPilihBarang_KodeBarang.Text
            '    Master_Penawaran.Lbl_NmBrg.Text = TxtPilihBarang_NamaBarang.Text
            '    Master_Penawaran.Lbl_SatuanBrg.Text = CmbPilihBarang_Satuan.Text
            'Else
            '    MessageBox.Show(Base_Language.Lang_Global_FormAsal & " . .!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If

    End Sub

    Private Sub Btn_Refresh_Click_1(sender As Object, e As EventArgs) Handles BtnPilihBarang_Refresh.Click
        kosong()
    End Sub

    Private Sub txtJumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJumlah.KeyPress
        If e.KeyChar = Chr(13) Then
            CmbPilihBarang_Satuan.Focus()
        End If
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar <= Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub CmbPilihBarang_Lokasi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPilihBarang_Lokasi.SelectedIndexChanged

    End Sub

    Private Sub txtKeterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKeterangan.KeyPress, Txt_KDSo.KeyPress

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        get_jam()

        Dim Diff As Integer = DateDiff(DateInterval.Day, tgl_skg, DateTimePicker1.Value)


        If Diff < Val(TxtTiba.Text) Then
            Dim Msg As String = MessageBox.Show("Tanggal Dibutuhkan Kurang dari Estimasi Tiba,  Tetap Lanjutkan ? ", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If Msg = vbNo Then
                DateTimePicker1.Value = DateAdd(DateInterval.Day, Val(TxtTiba.Text) + 1, tgl_skg)
            End If
        End If

    End Sub

    Private Sub CmbPilihBarang_Satuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbPilihBarang_Satuan.KeyPress
        If e.KeyChar = Chr(13) Then txtKeterangan.Focus()
    End Sub


    Private Sub txtJumlah_Validating(sender As Object, e As CancelEventArgs) Handles txtJumlah.Validating
        If asal = "SD_Data_PR" Then
            Dim sisa As Decimal = Txt_Sisa.Text
            Dim jumlah As Decimal

            If Decimal.TryParse(txtJumlah.Text, jumlah) Then
                If jumlah > sisa Then
                    MessageBox.Show("Jumlah tidak boleh lebih dari " & sisa.ToString(), "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtJumlah.Text = sisa.ToString()
                    txtJumlah.Focus()
                End If
            End If

        End If

    End Sub

    ''Private Sub SD_Tambah_PR_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    ''    formUtama.BtnFormulator_Refresh_Click("", e)
    ''End Sub



End Class