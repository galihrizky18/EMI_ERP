Imports System.Reflection
Imports LovePdf.Model.TaskParams.Sign.Elements

Public Class Master_Flever


    Dim arrSo As New ArrayList
    Dim LvBarang_KdSO, LvBarang_KdBarang, LvBarang_Nama As String
    Dim LvData_JmlhMin, LvData_JmlhPlus, LvData_SoAwal, LvData_SoAkhir, LvData_KdBarangAwal, LvData_KdBarangAkhir As String

    Dim itemBarang_KdSO As Integer = 0
    Dim itemBarang_KDBarang As Integer = 1
    Dim itemBarang_Nama As Integer = 2

    Dim itemData_jmlhMin As Integer = 4
    Dim itemData_jmlhPlus As Integer = 5
    Dim itemData_KdSoAwal As Integer = 6
    Dim itemData_KdSoAkhir As Integer = 7
    Dim itemData_KdBarangAwal As Integer = 8
    Dim itemData_KdBarangAkhir As Integer = 9

    Private Sub Master_Flever_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Intial_ListView()
        kosong()
        Load_Data_ListView()
    End Sub

    Private Sub kosong()

        Cmb_SoAwal.Items.Clear()
        Cmb_SoAkhir.Items.Clear()

        Txt_KdBarangAwal.Text = String.Empty
        Txt_KdBarangAkhir.Text = String.Empty
        Txt_NamaBarangAwal.Text = String.Empty
        Txt_NamaBarangAkhir.Text = String.Empty
        Txt_JmlhMin.Text = String.Empty
        Txt_JmlhPlus.Text = String.Empty

        Lv_Data.Items.Clear()
        Lv_DetailBarang.Items.Clear()

        Lv_DetailBarang.Tag = ""

        Lv_DetailBarang.Visible = False
        Lv_DetailBarang.Location = New Point(880, 191)

        Load_All_Combobox()

        Btn_Simpan.Tag = "SIMPAN"

    End Sub

    Private Sub Load_All_Combobox()

        Try
            OpenConn()

            SQL = "select Kode_Stock_Owner, Keterangan from stock_owner_gudang"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_SoAwal.Items.Add(Dr("Keterangan")) : Cmb_SoAkhir.Items.Add(Dr("Keterangan"))
                    arrSo.Add(Dr("Kode_Stock_Owner"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Load_Intial_ListView()

        Lv_DetailBarang.Columns.Clear()
        Lv_Data.Columns.Clear()

        Lv_DetailBarang.Columns.Add("Kode Stock Owner", 120, HorizontalAlignment.Center)
        Lv_DetailBarang.Columns.Add("Kode Barang", 120, HorizontalAlignment.Center)
        Lv_DetailBarang.Columns.Add("Nama", 150, HorizontalAlignment.Center)
        Lv_DetailBarang.View = View.Details

        Lv_Data.Columns.Add("Gudang Awal", 150, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Barang Awal", 160, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Gudang Akhir", 150, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Barang Akhir", 160, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jumlah Dibutuhkan", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jumlah Jadi", 100, HorizontalAlignment.Center)

        'Hidden
        Lv_Data.Columns.Add("KodeSoAwal", 0, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("KodeSoAkhir", 0, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("KodeBrngAWal", 0, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("KodeBrngAkhir", 0, HorizontalAlignment.Center)

        Lv_Data.View = View.Details

    End Sub

    Private Sub Get_ListView_Barang(ByVal index As Integer)
        LvBarang_KdSO = Lv_DetailBarang.Items(index).SubItems(itemBarang_KdSO).Text
        LvBarang_KdBarang = Lv_DetailBarang.Items(index).SubItems(itemBarang_KDBarang).Text
        LvBarang_Nama = Lv_DetailBarang.Items(index).SubItems(itemBarang_Nama).Text
    End Sub

    Private Sub Get_ListView_Data(ByVal index As Integer)
        LvData_JmlhMin = Lv_Data.Items(index).SubItems(itemData_jmlhMin).Text
        LvData_JmlhPlus = Lv_Data.Items(index).SubItems(itemData_jmlhPlus).Text
        LvData_SoAwal = Lv_Data.Items(index).SubItems(itemData_KdSoAwal).Text
        LvData_SoAkhir = Lv_Data.Items(index).SubItems(itemData_KdSoAkhir).Text
        LvData_KdBarangAwal = Lv_Data.Items(index).SubItems(itemData_KdBarangAwal).Text
        LvData_KdBarangAkhir = Lv_Data.Items(index).SubItems(itemData_KdBarangAkhir).Text
    End Sub

    Private Sub Load_Data_ListView()
        Try
            OpenConn()

            Lv_Data.Items.Clear()


            SQL = "select a.Kode_Stock_Owner_Min, a.Kode_Stock_Owner_Plus, a.Kode_Barang_Min, a.Kode_Barang_Plus, "
            SQL = SQL & "ISNULL((select nama from barang z where a.Kode_Perusahaan=z.Kode_Perusahaan and a.Kode_Stock_Owner_Min=z.Kode_Stock_Owner and a.Kode_Barang_Min=z.Kode_Barang ),'') as Nama_Barang_Min, "
            SQL = SQL & "ISNULL((select nama from barang z where a.Kode_Perusahaan=z.Kode_Perusahaan and a.Kode_Stock_Owner_Plus=z.Kode_Stock_Owner and a.Kode_Barang_Plus=z.Kode_Barang ),'') as Nama_Barang_Plus, "
            SQL = SQL & "a.Jumlah_Plus as Jumlah_Minimum, "
            SQL = SQL & "a.Pengali_Jml_Minimum as Jumlah_Jadi, a.RV "
            SQL = SQL & "from emi_master_flever a "
            SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "group by  a.Kode_Stock_Owner_Min, a.Kode_Stock_Owner_Plus, a.Kode_Barang_Min, a.Kode_Barang_Plus, a.Jumlah_Plus, a.Pengali_Jml_Minimum, a.RV, "
            SQL = SQL & "a.Kode_Perusahaan, a.Kode_Stock_Owner_Min, a.Kode_Barang_Min, a.Kode_Stock_Owner_Plus, a.Kode_Barang_Plus "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("Kode_Stock_Owner_Min"))
                    Lv.SubItems.Add(Dr("Nama_Barang_Min"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner_Plus"))
                    Lv.SubItems.Add(Dr("Nama_Barang_Plus"))
                    Lv.SubItems.Add(Dr("Jumlah_Minimum"))
                    Lv.SubItems.Add(Dr("Jumlah_Jadi"))

                    'Hiddeon
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner_Min"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner_Plus"))
                    Lv.SubItems.Add(Dr("Kode_Barang_Min"))
                    Lv.SubItems.Add(Dr("Kode_Barang_Plus"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub




    'FUNCTION BUTTON HANDLE
    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Load_Intial_ListView()
        kosong()
        Load_Data_ListView()
    End Sub



    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If Cmb_SoAwal.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi Gudang Awal Harus Diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_SoAwal.Focus() : Exit Sub
        ElseIf Cmb_SoAkhir.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi Gudang Akhir Harus Diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_SoAkhir.Focus() : Exit Sub
        ElseIf String.IsNullOrEmpty(Txt_KdBarangAwal.Text) Then
            MessageBox.Show("Kode Barang Awal Harus Dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarangAwal.Focus() : Exit Sub
        ElseIf String.IsNullOrEmpty(Txt_KdBarangAkhir.Text) Then
            MessageBox.Show("Kode Barang Akhir Harus Dipilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdBarangAkhir.Focus() : Exit Sub
        ElseIf String.IsNullOrEmpty(Txt_JmlhMin.Text) Then
            MessageBox.Show("Jumlah Minimum Harus Diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_JmlhMin.Focus() : Exit Sub
        ElseIf String.IsNullOrEmpty(Txt_JmlhPlus.Text) Then
            MessageBox.Show("Jumlah Hasil Jadi Harus Diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_JmlhPlus.Focus() : Exit Sub
        End If


        Try
            OpenConn()

            '=======================================
            '=      CEK APAKAH DATA SUDAH ADA      =
            '=======================================

            SQL = "select Kode_Stock_Owner_Min from emi_master_flever where Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and Kode_Stock_Owner_Min='" & arrSo(Cmb_SoAwal.SelectedIndex) & "' and Kode_Barang_Min='" & Txt_KdBarangAwal.Text & "' "
            SQL = SQL & "and Kode_Stock_Owner_Plus='" & arrSo(Cmb_SoAkhir.SelectedIndex) & "' and Kode_Barang_Plus='" & Txt_KdBarangAkhir.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.HasRows Then
                    CloseConn()
                    MessageBox.Show("Data Sudah Ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=========================
            '=      INSERT DATA      =
            '=========================

            SQL = "insert into emi_master_flever (Kode_Perusahaan, Kode_Stock_Owner_Min, Kode_Barang_Min, "
            SQL = SQL & "Kode_Stock_Owner_Plus, Kode_Barang_Plus, Jumlah_Plus, Pengali_Jml_Minimum) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & arrSo(Cmb_SoAwal.SelectedIndex) & "', '" & Txt_KdBarangAwal.Text & "', "
            SQL = SQL & "'" & arrSo(Cmb_SoAkhir.SelectedIndex) & "', '" & Txt_KdBarangAkhir.Text & "', "
            SQL = SQL & "'" & Txt_JmlhMin.Text & "', '" & Txt_JmlhPlus.Text & "')"
            ExecuteTrans(SQL)




            CloseConn()
            MessageBox.Show("Berhasil Disimpan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Load_Intial_ListView()
            kosong()
            Load_Data_ListView()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub


    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        If Lv_Data.Items.Count = 0 Then Exit Sub

        Dim Tanya_Cetak As String = MessageBox.Show("Yakin ingin Hapus?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Tanya_Cetak = vbNo Then
            Exit Sub
        End If

        Get_ListView_Data(Lv_Data.FocusedItem.Index)

        Try
            OpenConn()

            SQL = "delete from emi_master_flever where Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and Kode_Stock_Owner_Min='" & LvData_SoAwal & "' and Kode_Barang_Min='" & LvData_KdBarangAwal & "' "
            SQL = SQL & "and Kode_Stock_Owner_Plus='" & LvData_SoAkhir & "' and Kode_Barang_Plus='" & LvData_KdBarangAkhir & "'"
            ExecuteTrans(SQL)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Load_Intial_ListView()
        kosong()
        Load_Data_ListView()
    End Sub




    'FUCNTION HANDLE
    Private Sub Txt_KdBarangAwal_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarangAwal.TextChanged

        If Cmb_SoAwal.SelectedIndex = -1 Then Exit Sub

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
            SQL = SQL & "where Kode_Stock_Owner='" & arrSo(Cmb_SoAwal.SelectedIndex) & "' "
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
            Lv_DetailBarang.Location = New Point(43, 191)
            Lv_DetailBarang.Tag = "BARANG AWAL"

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub



    Private Sub Txt_KdBarangAkhir_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarangAkhir.TextChanged
        If Cmb_SoAkhir.SelectedIndex = -1 Then Exit Sub

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
            SQL = SQL & "where Kode_Stock_Owner='" & arrSo(Cmb_SoAkhir.SelectedIndex) & "' "
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
            Lv_DetailBarang.Location = New Point(471, 191)
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
            Txt_KdBarangAwal.Text = LvBarang_KdBarang
            Txt_NamaBarangAwal.Text = LvBarang_Nama
        ElseIf Lv_DetailBarang.Tag = "BARANG AKHIR" Then
            Txt_KdBarangAkhir.Text = LvBarang_KdBarang
            Txt_NamaBarangAkhir.Text = LvBarang_Nama
        End If


        Lv_DetailBarang.Items.Clear()

        Lv_DetailBarang.Tag = ""

        Lv_DetailBarang.Visible = False
        Lv_DetailBarang.Location = New Point(880, 191)


    End Sub


    'FUNCTION HANDLE KEYPRESS
    Private Sub Cmb_SoAwal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_SoAwal.KeyPress
        If e.KeyChar = Chr(13) Then Txt_KdBarangAwal.Focus() : Exit Sub
    End Sub
    Private Sub Txt_KdBarangAwal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarangAwal.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_SoAkhir.Focus() : Exit Sub
    End Sub
    Private Sub Cmb_SoAkhir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_SoAkhir.KeyPress
        If e.KeyChar = Chr(13) Then Txt_KdBarangAkhir.Focus() : Exit Sub
    End Sub
    Private Sub Txt_KdBarangAkhir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarangAkhir.KeyPress
        If e.KeyChar = Chr(13) Then Txt_JmlhMin.Focus() : Exit Sub
    End Sub
    Private Sub Txt_JmlhPlus_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_JmlhPlus.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus() : Exit Sub
    End Sub





End Class