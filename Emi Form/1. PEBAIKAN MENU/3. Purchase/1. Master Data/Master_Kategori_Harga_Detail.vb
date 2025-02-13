Imports System.Diagnostics.Eventing.Reader
Imports System.Web.Management
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports CrystalDecisions.Windows.Forms

Public Class Master_Kategori_Harga_Detail

    Dim arrcari, arrJnsKatHrg, arrKategoriProduk, arrKategoriKemasan, arrKategoriBerat As New ArrayList

    Dim arrIdBiaya As New ArrayList

    Dim Jenis = "Master_Jenis_Member_Perbarang"

    Dim LvID, LvId_Jenis_Kategori_Harga, lvId_Jenis_Produk, lvId_Jenis_Kemasan_Utama, lvId_Kapasitas_Kemasan_Utama As String
    Dim LvJenisKategoriHarga, LvKategoriProduk, LvKategoriKemasan, LvKategoriBerat, LvRangeHargaMin, LvRangeHargaMax, LvPersenMarkUp As String

    Dim itemID As Integer = 0
    Dim itemJenisKategoriHarga As Integer = 1
    Dim itemKategoriProduk As Integer = 2
    Dim itemKategoriKemasan As Integer = 3
    Dim itemKategoriBerat As Integer = 4
    Dim itemRangeHargaMin As Integer = 5
    Dim itemRangeHargaMax As Integer = 6
    Dim itemPersenMarkUp As Integer = 7

    Dim itemIdJenisKategoriHarga As Integer = 8
    Dim itemIdKategoriProduk As Integer = 9
    Dim itemIdJenisKemasanUtam As Integer = 10
    Dim itemIdKapasitasKemasanUtama As Integer = 11

    Dim lvIdBiaya, lvBiaya, lvNilaiBiaya As String
    Dim itemIdBiaya As Integer = 0
    Dim itemBiaya As Integer = 1
    Dim itemNilaiBiaya As Integer = 2

    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)
        LvID = Lv_Data_MasterJenisMemberPerbarang.Items(NoIndex).Text
        LvJenisKategoriHarga = Lv_Data_MasterJenisMemberPerbarang.Items(NoIndex).SubItems(itemJenisKategoriHarga).Text
        LvKategoriProduk = Lv_Data_MasterJenisMemberPerbarang.Items(NoIndex).SubItems(itemKategoriProduk).Text
        LvKategoriKemasan = Lv_Data_MasterJenisMemberPerbarang.Items(NoIndex).SubItems(itemKategoriKemasan).Text
        LvKategoriBerat = Lv_Data_MasterJenisMemberPerbarang.Items(NoIndex).SubItems(itemKategoriBerat).Text
        LvRangeHargaMin = Lv_Data_MasterJenisMemberPerbarang.Items(NoIndex).SubItems(itemRangeHargaMin).Text
        LvRangeHargaMax = Lv_Data_MasterJenisMemberPerbarang.Items(NoIndex).SubItems(itemRangeHargaMax).Text
        LvPersenMarkUp = Lv_Data_MasterJenisMemberPerbarang.Items(NoIndex).SubItems(itemPersenMarkUp).Text

        LvId_Jenis_Kategori_Harga = Lv_Data_MasterJenisMemberPerbarang.Items(NoIndex).SubItems(itemIdJenisKategoriHarga).Text
        lvId_Jenis_Produk = Lv_Data_MasterJenisMemberPerbarang.Items(NoIndex).SubItems(itemIdKategoriProduk).Text
        lvId_Jenis_Kemasan_Utama = Lv_Data_MasterJenisMemberPerbarang.Items(NoIndex).SubItems(itemIdJenisKemasanUtam).Text
        lvId_Kapasitas_Kemasan_Utama = Lv_Data_MasterJenisMemberPerbarang.Items(NoIndex).SubItems(itemIdKapasitasKemasanUtama).Text
    End Sub

    Private Sub Get_Isi_ListView_BiayaDetail(ByVal NoIndex As Integer)
        lvIdBiaya = Lv_BiayaDetail.Items(NoIndex).Text
        lvBiaya = Lv_BiayaDetail.Items(NoIndex).SubItems(itemBiaya).Text
        lvNilaiBiaya = Lv_BiayaDetail.Items(NoIndex).SubItems(itemNilaiBiaya).Text
    End Sub

    Private Sub Master_Jenis_Member_Perbarang_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Master_Jenis_Member_Perbarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Btn_Tambah.Text = Base_Language.Lang_Global_Tambah
            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
            Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
            Btn_Cari.Text = Base_Language.Lang_Global_Cari
            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
            Btn_Simpan.Tag = "&Simpan"
            Btn_Hapus.Enabled = False

            Lbl_Judul.Text = Base_Language.Lang_JenisMemberPerbarang_Judul
            Lbl_JnsKategoriHrg.Text = Base_Language.Lang_Global_JenisKategoriHarga
            Lbl_KategoriKemasan.Text = Base_Language.Lang_Global_KategoriKemasan
            Lbl_KategoriBerat.Text = Base_Language.Lang_Global_KategoriBerat
            Lbl_RangeHrg.Text = Base_Language.Lang_Global_RangeHarga
            Lbl_SampaiDengan.Text = Base_Language.Lang_Global_sampaidengan
            Lbl_PersenMarkUp.Text = Base_Language.Lang_Global_PersenMarkUp
            Label1.Text = Base_Language.Lang_JenisMemberPerbarang_Persen

            Label4.Text = Base_Language.Lang_Global_Kolom

            Lv_Data_MasterJenisMemberPerbarang.Columns.Clear()
            Lv_Data_MasterJenisMemberPerbarang.Columns.Add("ID", 0, HorizontalAlignment.Left)
            Lv_Data_MasterJenisMemberPerbarang.Columns.Add(Base_Language.Lang_Global_JenisKategoriHarga, 130, HorizontalAlignment.Left)
            Lv_Data_MasterJenisMemberPerbarang.Columns.Add(Base_Language.Lang_Global_KategoriProduk, 130, HorizontalAlignment.Left)
            Lv_Data_MasterJenisMemberPerbarang.Columns.Add(Base_Language.Lang_Global_KategoriKemasan, 130, HorizontalAlignment.Left)
            Lv_Data_MasterJenisMemberPerbarang.Columns.Add(Base_Language.Lang_Global_KategoriBerat, 120, HorizontalAlignment.Center)
            Lv_Data_MasterJenisMemberPerbarang.Columns.Add(Base_Language.Lang_Global_RangeHarga + " " + "Min", 120, HorizontalAlignment.Right)
            Lv_Data_MasterJenisMemberPerbarang.Columns.Add(Base_Language.Lang_Global_RangeHarga + " " + "Max", 120, HorizontalAlignment.Right)
            Lv_Data_MasterJenisMemberPerbarang.Columns.Add(Base_Language.Lang_Global_PersenMarkUp, 120, HorizontalAlignment.Center)
            Lv_Data_MasterJenisMemberPerbarang.Columns.Add(Base_Language.Lang_JenisMemberPerbarang_Persen, 120, HorizontalAlignment.Center)

            Lv_Data_MasterJenisMemberPerbarang.Columns.Add("ID Jenis_Kategori_Harga", 0, HorizontalAlignment.Center)
            Lv_Data_MasterJenisMemberPerbarang.Columns.Add("ID Jenis_Produk", 0, HorizontalAlignment.Center)
            Lv_Data_MasterJenisMemberPerbarang.Columns.Add("ID Jenis_Kemasan_Utama", 0, HorizontalAlignment.Center)
            Lv_Data_MasterJenisMemberPerbarang.Columns.Add("ID Kapasitas_Kemasan_Utama", 0, HorizontalAlignment.Center)
            Lv_Data_MasterJenisMemberPerbarang.View = View.Details

            Lv_BiayaDetail.Columns.Clear()
            Lv_BiayaDetail.Columns.Add("ID Biaya", 0, HorizontalAlignment.Left)
            Lv_BiayaDetail.Columns.Add(Base_Language.Lang_Global_biaya, 225, HorizontalAlignment.Left)
            Lv_BiayaDetail.Columns.Add(Base_Language.Lang_Global_NilaiBiaya, 270, HorizontalAlignment.Right)
            Lv_BiayaDetail.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try

        kosong()

    End Sub

    Private Sub kosong()
        Lbl_ID.Text = ""
        Lbl_IdJnsKategoriHrg.Text = ""
        Lbl_IdJnsProduk.Text = ""
        Lbl_IdJnsKemasanUtama.Text = ""
        Lbl_IdKapasitasKemasanUtama.Text = ""
        Cmb_JnsKategoriHrg.Items.Clear()
        Cmb_KategoriProduk.Items.Clear()
        Cmb_KategoriKemasan.Items.Clear()
        Cmb_KategoriBerat.Items.Clear()
        Txt_RangeHrgMin.Text = ""
        Txt_RangeHrgMax.Text = ""
        Txt_PersenMarkUp.Text = ""
        TextBox1.Text = ""

        Cmb_Biaya.Items.Clear()
        Txt_InputBiaya.Text = ""

        Lv_BiayaDetail.Items.Clear()

        Try
            OpenConn()

            Lv_Data_MasterJenisMemberPerbarang.Items.Clear()
            SQL = "Select a.Id_Jenis_Member_Perbarang, "
            SQL = SQL & "b.Nama_Jenis_Kategori_Harga as Jenis_Kategori_Harga, "
            SQL = SQL & "e.Keterangan as Jenis_Produk, "
            SQL = SQL & "c.Keterangan as Jenis_Kemasan_Utama, "
            SQL = SQL & "d.Keterangan as Kapasitas_Kemasan_Utama, "
            SQL = SQL & "a.Range_Harga_Min, a.Range_Harga_Max, a.Persen_MarkUp, "
            SQL = SQL & "a.Persen_Penentu_Harga_Jual, "
            SQL = SQL & "a.Id_Jenis_Kategori_Harga, a.Id_Jenis_Produk, a.Id_Jenis_Kemasan_Utama, a.Id_Kapasitas_Kemasan_Utama "
            SQL = SQL & "From EMI_Master_Jenis_Member_Perbarang a, "
            SQL = SQL & "EMI_Master_Jenis_Kategori_Harga b, "
            SQL = SQL & "EMI_Jenis_Produk e, "
            SQL = SQL & "EMI_Jenis_Kemasan_Utama c, "
            SQL = SQL & "EMI_Kapasitas_Kemasan_Utama d "
            SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Jenis_Kategori_Harga = b.Id_Jenis_Kategori_Harga "
            SQL = SQL & "and a.Id_Jenis_Produk = e.Id_Jenis_Produk "
            SQL = SQL & "and e.Id_Jenis_Produk = c.Id_Jenis_Produk "
            SQL = SQL & "and c.Id_Jenis_Kemasan_Utama = d.Id_Jenis_Kemasan_Utama "
            SQL = SQL & "and a.Id_Jenis_Kemasan_Utama = c.Id_Jenis_Kemasan_Utama "
            SQL = SQL & "and a.Id_Kapasitas_Kemasan_Utama = d.Id_Kapasitas_Kemasan_Utama "
            SQL = SQL & "Order By a.Id_Jenis_Member_Perbarang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_Data_MasterJenisMemberPerbarang.Items.Add(Dr("Id_Jenis_Member_Perbarang"))
                    lvw.SubItems.Add(Dr("Jenis_Kategori_Harga"))
                    lvw.SubItems.Add(Dr("Jenis_Produk"))
                    lvw.SubItems.Add(Dr("Jenis_Kemasan_Utama"))
                    lvw.SubItems.Add(Dr("Kapasitas_Kemasan_Utama"))
                    lvw.SubItems.Add(Format(Dr("Range_Harga_Min"), "N0"))
                    lvw.SubItems.Add(Format(Dr("Range_Harga_Max"), "N0"))
                    lvw.SubItems.Add(Format(Dr("Persen_MarkUp"), "N0"))
                    lvw.SubItems.Add(Dr("Persen_Penentu_Harga_Jual"))
                    '
                    lvw.SubItems.Add(Dr("Id_Jenis_Kategori_Harga"))
                    lvw.SubItems.Add(Dr("Id_Jenis_Produk"))
                    lvw.SubItems.Add(Dr("Id_Jenis_Kemasan_Utama"))
                    lvw.SubItems.Add(Dr("Id_Kapasitas_Kemasan_Utama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        ComboBox1.Items.Clear() : arrcari.Clear()
        ComboBox1.Items.Add(Base_Language.Lang_Global_JenisKategoriHarga) : arrcari.Add("Nama_Jenis_Kategori_Harga")
        ComboBox1.Items.Add(Base_Language.Lang_Global_KategoriProduk) : arrcari.Add("e.Keterangan")
        ComboBox1.Items.Add(Base_Language.Lang_Global_KategoriKemasan) : arrcari.Add("c.Keterangan")
        ComboBox1.Items.Add(Base_Language.Lang_Global_KategoriBerat) : arrcari.Add("d.Keterangan")
        TextBox3.Text = ""

        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
        Btn_Simpan.Enabled = True : Btn_Hapus.Enabled = False

        Get_Jenis_Kategori_Harga()
        Get_Kategori_Produk()
        Get_Biaya()

    End Sub

    Private Sub Get_Jenis_Kategori_Harga()
        Try
            OpenConn()

            Cmb_JnsKategoriHrg.Items.Clear() : arrJnsKatHrg.Clear()
            SQL = "Select * From "
            SQL = SQL & "EMI_Master_Jenis_Kategori_Harga "
            SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_JnsKategoriHrg.Items.Add(Dr("Nama_Jenis_Kategori_Harga")) : arrJnsKatHrg.Add(Dr("Id_Jenis_Kategori_Harga"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Get_Kategori_Produk()
        Try
            OpenConn()

            Cmb_KategoriProduk.Items.Clear() : arrKategoriProduk.Clear()
            SQL = "Select * From "
            SQL = SQL & "EMI_Jenis_Produk "
            SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_KategoriProduk.Items.Add(Dr("Keterangan")) : arrKategoriProduk.Add(Dr("Id_Jenis_Produk"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Get_Kategori_Kemasan()
        Try
            OpenConn()

            Cmb_KategoriKemasan.Items.Clear() : arrKategoriKemasan.Clear()
            SQL = "Select * From "
            SQL = SQL & "EMI_Jenis_Kemasan_Utama "
            SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Id_Jenis_Produk = '" & arrKategoriProduk.Item(Cmb_KategoriProduk.SelectedIndex) & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_KategoriKemasan.Items.Add(Dr("Keterangan")) : arrKategoriKemasan.Add(Dr("Id_Jenis_Kemasan_Utama"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Get_Kategori_Berat()
        Try
            OpenConn()

            Cmb_KategoriBerat.Items.Clear() : arrKategoriBerat.Clear()
            SQL = "Select * From "
            SQL = SQL & "EMI_Kapasitas_Kemasan_Utama "
            SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Id_Jenis_Kemasan_Utama = '" & arrKategoriKemasan.Item(Cmb_KategoriKemasan.SelectedIndex) & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_KategoriBerat.Items.Add(Dr("Keterangan")) : arrKategoriBerat.Add(Dr("Id_Kapasitas_Kemasan_Utama"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Get_Biaya()
        Try
            OpenConn()
            Cmb_Biaya.Items.Clear() : arrIdBiaya.Clear()
            SQL = "Select * From "
            SQL = SQL & "EMI_Biaya "
            SQL = SQL & "order by Id_Biaya"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Biaya.Items.Add(dr("Keterangan")) : arrIdBiaya.Add(dr("Id_Biaya"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        Dim jnsKategoriHrg = Cmb_JnsKategoriHrg.SelectedItem
        Dim kategoriProduk = Cmb_KategoriProduk.SelectedItem
        Dim kategoriKemasan = Cmb_KategoriKemasan.SelectedItem
        Dim kategoriBerat = Cmb_KategoriBerat.SelectedItem
        Dim rangeHrgMin = Txt_RangeHrgMin.Text
        Dim rangeHrgMax = Txt_RangeHrgMax.Text
        Dim persenMarkUp = Txt_PersenMarkUp.Text

        If Cmb_JnsKategoriHrg.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_JenisKategoriHarga & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Cmb_JnsKategoriHrg.Focus() : Exit Sub
        ElseIf Cmb_KategoriProduk.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_KategoriProduk & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Cmb_KategoriProduk.Focus() : Exit Sub
        ElseIf Cmb_KategoriKemasan.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_KategoriKemasan & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Cmb_KategoriKemasan.Focus() : Exit Sub
        ElseIf Cmb_KategoriBerat.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_KategoriBerat & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Cmb_KategoriBerat.Focus() : Exit Sub
        ElseIf rangeHrgMin.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_RangeHarga & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_RangeHrgMin.Focus() : Exit Sub
        ElseIf rangeHrgMax.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_RangeHarga & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_RangeHrgMax.Focus() : Exit Sub
        ElseIf persenMarkUp.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_PersenMarkUp & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_PersenMarkUp.Focus() : Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction() = Cn.BeginTransaction

            If Btn_Simpan.Text = Base_Language.Lang_Global_Simpan Then

                '- Insert EMI_Master_Jenis_Member_Perbarang
                SQL = "Insert Into EMI_Master_Jenis_Member_Perbarang "
                SQL = SQL & "(Kode_Perusahaan, Id_Jenis_Kategori_Harga, "
                SQL = SQL & "Id_Jenis_Produk, Id_Jenis_Kemasan_Utama, "
                SQL = SQL & "Id_Kapasitas_Kemasan_Utama, Range_Harga_Min, "
                SQL = SQL & "Range_Harga_Max, Persen_Markup,Persen_Penentu_Harga_Jual) "
                SQL = SQL & "Values ('" & KodePerusahaan & "', "
                SQL = SQL & "'" & arrJnsKatHrg.Item(Cmb_JnsKategoriHrg.SelectedIndex) & "', "
                SQL = SQL & "'" & arrKategoriProduk.Item(Cmb_KategoriProduk.SelectedIndex) & "', "
                SQL = SQL & "'" & arrKategoriKemasan.Item(Cmb_KategoriKemasan.SelectedIndex) & "', "
                SQL = SQL & "'" & arrKategoriBerat.Item(Cmb_KategoriBerat.SelectedIndex) & "', "
                SQL = SQL & "'" & Txt_RangeHrgMin.Text & "', "
                SQL = SQL & "'" & Txt_RangeHrgMax.Text & "', "
                SQL = SQL & "'" & Txt_PersenMarkUp.Text & "',"
                SQL = SQL & "'" & TextBox1.Text & "')"
                ExecuteTrans(SQL)

                '-- Select untuk get data ID Current
                Dim id_Current As Integer = 0
                SQL = "select IDENT_CURRENT('EMI_Master_Jenis_Member_Perbarang') as urutan"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        id_Current = Dr("urutan")
                    End If
                End Using

                '--- Delete isi EMI_Master_Jenis_Member_Perbarang_DetailBiaya
                'SQL = "Delete from EMI_Master_Jenis_Member_Perbarang_DetailBiaya "
                'SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and Id_Jenis_Member_Per_Barang = '" & id_Current & "' "
                'ExecuteTrans(SQL)

                '--- Insert ke EMI_Master_Jenis_Member_Perbarang_DetailBiaya
                'For i As Integer = 0 To Lv_BiayaDetail.Items.Count - 1
                '    Get_Isi_ListView_BiayaDetail(i)
                '    SQL = "Insert Into EMI_Master_Jenis_Member_Perbarang_DetailBiaya "
                '    SQL = SQL & "(Kode_Perusahaan, Id_Jenis_Member_Per_Barang, "
                '    SQL = SQL & "Id_Biaya, Nilai_Biaya) "
                '    SQL = SQL & "Values ('" & KodePerusahaan & "', "
                '    SQL = SQL & "'" & id_Current & "', "
                '    SQL = SQL & "'" & lvIdBiaya & "', "
                '    SQL = SQL & "'" & lvNilaiBiaya & "')"
                '    ExecuteTrans(SQL)
                'Next

            Else
                SQL = "Update EMI_Master_Jenis_Member_Perbarang "
                SQL = SQL & "Set Id_Jenis_Kategori_Harga = '" & arrJnsKatHrg.Item(Cmb_JnsKategoriHrg.SelectedIndex) & "', "
                SQL = SQL & "Id_Jenis_Produk = '" & arrKategoriProduk.Item(Cmb_KategoriProduk.SelectedIndex) & "', "
                SQL = SQL & "Id_Jenis_Kemasan_Utama = '" & arrKategoriKemasan.Item(Cmb_KategoriKemasan.SelectedIndex) & "', "
                SQL = SQL & "Id_Kapasitas_Kemasan_Utama = '" & arrKategoriBerat.Item(Cmb_KategoriBerat.SelectedIndex) & "', "
                SQL = SQL & "Range_Harga_Min = '" & Val(HilangkanTanda(rangeHrgMin)) & "', "
                SQL = SQL & "Range_Harga_Max = '" & Val(HilangkanTanda(rangeHrgMax)) & "', "
                SQL = SQL & "Persen_Markup = '" & Val(HilangkanTanda(persenMarkUp)) & "', "
                SQL = SQL & "Persen_Penentu_Harga_Jual = '" & TextBox1.Text & "' "
                SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Jenis_Member_Perbarang = '" & Lbl_ID.Text & "' "
                ExecuteTrans(SQL)

                '--- Delete isi EMI_Master_Jenis_Member_Perbarang_DetailBiaya
                'SQL = "Delete from EMI_Master_Jenis_Member_Perbarang_DetailBiaya "
                'SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
                'SQL = SQL & "and Id_Jenis_Member_Per_Barang = '" & Lbl_ID.Text & "' "
                'ExecuteTrans(SQL)

                ''--- Insert ke EMI_Master_Jenis_Member_Perbarang_DetailBiaya
                'For i As Integer = 0 To Lv_BiayaDetail.Items.Count - 1
                '    Get_Isi_ListView_BiayaDetail(i)
                '    SQL = "Insert Into EMI_Master_Jenis_Member_Perbarang_DetailBiaya "
                '    SQL = SQL & "(Kode_Perusahaan, Id_Jenis_Member_Per_Barang, "
                '    SQL = SQL & "Id_Biaya, Nilai_Biaya) "
                '    SQL = SQL & "Values ('" & KodePerusahaan & "', "
                '    SQL = SQL & "'" & Lbl_ID.Text & "', "
                '    SQL = SQL & "'" & lvIdBiaya & "', "
                '    SQL = SQL & "'" & lvNilaiBiaya & "')"
                '    ExecuteTrans(SQL)
                'Next
            End If

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub

    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        Get_Isi_ListView(Lv_Data_MasterJenisMemberPerbarang.FocusedItem.Index)

        Dim Hapus As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction() = Cn.BeginTransaction

                SQL = "DELETE FROM EMI_Master_Jenis_Member_Perbarang "
                SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Jenis_Member_Perbarang = '" & LvID & "' "
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Hapus, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        kosong()
        'Cmb_JnsKategoriHrg.Focus()
    End Sub

    Private Sub Cmb_KategoriProduk_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_KategoriProduk.SelectedIndexChanged
        Cmb_KategoriKemasan.Items.Clear()
        Cmb_KategoriBerat.Items.Clear()
        Get_Kategori_Kemasan()
    End Sub

    Private Sub Cmb_KategoriKemasan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_KategoriKemasan.SelectedIndexChanged
        Get_Kategori_Berat()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Txt_RangeHrgMax_TextChanged(sender As Object, e As EventArgs) Handles Txt_RangeHrgMax.TextChanged

    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        'If ComboBox1.Text.Trim.Length = 0 Then Exit Sub
        'If TextBox3.Text.Trim.Length = 0 Then Exit Sub

        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_Kolom & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ComboBox1.Focus() : Exit Sub
        ElseIf TextBox3.Text.Trim.Length = 0 Then
            MessageBox.Show("Value" & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox3.Focus() : Exit Sub
        End If

        Cari("T")
    End Sub

    Private Sub Txt_PersenMarkUp_TextChanged(sender As Object, e As EventArgs) Handles Txt_PersenMarkUp.TextChanged

    End Sub

    Private Sub Cari(ByVal semua As String)

        Try
            OpenConn()

            Lv_Data_MasterJenisMemberPerbarang.Items.Clear()
            SQL = "Select a.Id_Jenis_Member_Perbarang, "
            SQL = SQL & "b.Nama_Jenis_Kategori_Harga as Jenis_Kategori_Harga, "
            SQL = SQL & "e.Keterangan as Jenis_Produk, "
            SQL = SQL & "c.Keterangan as Jenis_Kemasan_Utama, "
            SQL = SQL & "d.Keterangan as Kapasitas_Kemasan_Utama, "
            SQL = SQL & "a.Range_Harga_Min, a.Range_Harga_Max, a.Persen_MarkUp, "
            SQL = SQL & "a.Persen_Penentu_Harga_Jual From EMI_Master_Jenis_Member_Perbarang a, "
            SQL = SQL & "EMI_Master_Jenis_Kategori_Harga b, "
            SQL = SQL & "EMI_Jenis_Produk e, "
            SQL = SQL & "EMI_Jenis_Kemasan_Utama c, "
            SQL = SQL & "EMI_Kapasitas_Kemasan_Utama d "
            SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Jenis_Kategori_Harga = b.Id_Jenis_Kategori_Harga "
            SQL = SQL & "and a.Id_Jenis_Produk = e.Id_Jenis_Produk "
            SQL = SQL & "and e.Id_Jenis_Produk = c.Id_Jenis_Produk "
            SQL = SQL & "and c.Id_Jenis_Kemasan_Utama = d.Id_Jenis_Kemasan_Utama "
            SQL = SQL & "and a.Id_Jenis_Kemasan_Utama = c.Id_Jenis_Kemasan_Utama "
            SQL = SQL & "and a.Id_Kapasitas_Kemasan_Utama = d.Id_Kapasitas_Kemasan_Utama "
            If semua = "T" Then
                SQL = SQL & "and " & arrcari.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
                SQL = SQL & "order by " & arrcari.Item(ComboBox1.SelectedIndex) & " "
            Else
                SQL = SQL & "Order By a.Id_Jenis_Member_Perbarang "
            End If

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_Data_MasterJenisMemberPerbarang.Items.Add(Dr("Id_Jenis_Member_Perbarang"))
                    lvw.SubItems.Add(Dr("Jenis_Kategori_Harga"))
                    lvw.SubItems.Add(Dr("Jenis_Produk"))
                    lvw.SubItems.Add(Dr("Jenis_Kemasan_Utama"))
                    lvw.SubItems.Add(Dr("Kapasitas_Kemasan_Utama"))
                    lvw.SubItems.Add(Format(Dr("Range_Harga_Min"), "N0"))
                    lvw.SubItems.Add(Format(Dr("Range_Harga_Max"), "N0"))
                    lvw.SubItems.Add(Format(Dr("Persen_MarkUp"), "N0"))
                    lvw.SubItems.Add(Dr("Persen_Penentu_Harga_Jual"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'Try

        '    OpenConn()

        '    Lv_Data_MasterJenisMemberPerbarang.Items.Clear()
        '    SQL = "Select * From EMI_Master_Jenis_Member_Perbarang where kode_perusahaan = '" & KodePerusahaan & "' "
        '    If semua = "T" Then
        '        SQL = SQL & "and " & arrcari.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
        '        SQL = SQL & "order by " & arrcari.Item(ComboBox1.SelectedIndex) & " "
        '    Else
        '        SQL = SQL & "order by Id_Jenis_Member_Perbarang"
        '    End If
        '    Using dr = OpenTrans(SQL)
        '        Do While dr.Read
        '            Dim lvw As ListViewItem
        '            lvw = Lv_Data_MasterJenisMemberPerbarang.Items.Add(dr("Divisi_Mesin"))
        '            lvw.SubItems.Add(dr("Seri_Mesin"))
        '            lvw.SubItems.Add(dr("Nama_Mesin"))
        '            lvw.SubItems.Add(dr("Keterangan"))
        '            lvw.SubItems.Add(dr("NoUrut"))
        '        Loop
        '    End Using

        '    CloseConn()

        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari_Click(TextBox3, e)
    End Sub

    Private Sub Lv_Data_MasterJenisMemberPerbarang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data_MasterJenisMemberPerbarang.DoubleClick
        If Lv_Data_MasterJenisMemberPerbarang.Items.Count = 0 Then Exit Sub
        Lbl_ID.Text = Lv_Data_MasterJenisMemberPerbarang.FocusedItem.Text
        'Cmb_JnsKategoriHrg.SelectedItem = Lv_Data_MasterJenisMemberPerbarang.FocusedItem.Text
        Cmb_JnsKategoriHrg.SelectedItem = Lv_Data_MasterJenisMemberPerbarang.FocusedItem.SubItems(itemJenisKategoriHarga).Text
        Cmb_KategoriProduk.SelectedItem = Lv_Data_MasterJenisMemberPerbarang.FocusedItem.SubItems(itemKategoriProduk).Text
        Cmb_KategoriKemasan.SelectedItem = Lv_Data_MasterJenisMemberPerbarang.FocusedItem.SubItems(itemKategoriKemasan).Text
        Cmb_KategoriBerat.SelectedItem = Lv_Data_MasterJenisMemberPerbarang.FocusedItem.SubItems(itemKategoriBerat).Text
        Txt_RangeHrgMin.Text = HilangkanTanda(Lv_Data_MasterJenisMemberPerbarang.FocusedItem.SubItems(itemRangeHargaMin).Text)
        Txt_RangeHrgMax.Text = HilangkanTanda(Lv_Data_MasterJenisMemberPerbarang.FocusedItem.SubItems(itemRangeHargaMax).Text)
        Txt_PersenMarkUp.Text = HilangkanTanda(Lv_Data_MasterJenisMemberPerbarang.FocusedItem.SubItems(itemPersenMarkUp).Text)

        Lbl_IdJnsKategoriHrg.Text = Lv_Data_MasterJenisMemberPerbarang.FocusedItem.SubItems(itemIdJenisKategoriHarga).Text
        Lbl_IdJnsProduk.Text = Lv_Data_MasterJenisMemberPerbarang.FocusedItem.SubItems(itemIdKategoriProduk).Text
        Lbl_IdJnsKemasanUtama.Text = Lv_Data_MasterJenisMemberPerbarang.FocusedItem.SubItems(itemIdJenisKemasanUtam).Text
        Lbl_IdKapasitasKemasanUtama.Text = Lv_Data_MasterJenisMemberPerbarang.FocusedItem.SubItems(itemIdKapasitasKemasanUtama).Text

        Cmb_JnsKategoriHrg_Leave(Lv_Data_MasterJenisMemberPerbarang, e)
    End Sub

    Private Sub Get_Data_Biaya_Detail()
        Try
            OpenConn()

            Lv_BiayaDetail.Items.Clear()
            SQL = "Select b.Id_Biaya, a.Keterangan, b.Nilai_Biaya "
            SQL = SQL & "From EMI_Biaya a, EMI_Master_Jenis_Member_Perbarang_DetailBiaya b "
            SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Biaya = b.Id_Biaya "
            SQL = SQL & "and b.Id_Jenis_Member_Per_Barang = '" & Lbl_ID.Text & "' "
            'SQL = SQL & "and Id_Jenis_Kategori_Harga = '" & arrJnsKatHrg.Item(Cmb_JnsKategoriHrg.SelectedIndex) & "' "
            'SQL = SQL & "and Id_Jenis_Produk = '" & arrKategoriProduk.Item(Cmb_KategoriProduk.SelectedIndex) & "' "
            'SQL = SQL & "and Id_Jenis_Kemasan_Utama = '" & arrKategoriKemasan.Item(Cmb_KategoriKemasan.SelectedIndex) & "' "
            'SQL = SQL & "and Id_Kapasitas_Kemasan_Utama = '" & arrKategoriBerat.Item(Cmb_KategoriBerat.SelectedIndex) & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem
                    lv = Lv_BiayaDetail.Items.Add(Dr("Id_Biaya"))
                    lv.SubItems.Add(Dr("Keterangan"))
                    lv.SubItems.Add(Dr("Nilai_Biaya"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Btn_Tambah_Click(sender As Object, e As EventArgs) Handles Btn_Tambah.Click
        Dim nilaiBiaya = Txt_InputBiaya.Text

        If Cmb_Biaya.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_biaya & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Cmb_Biaya.Focus() : Exit Sub
        ElseIf nilaiBiaya.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_NilaiBiaya & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_InputBiaya.Focus() : Exit Sub
        End If

        For i As Integer = 0 To Lv_BiayaDetail.Items.Count - 1
            Get_Isi_ListView_BiayaDetail(i)
            If Cmb_Biaya.SelectedItem.ToString() = lvBiaya Then
                MessageBox.Show(Base_Language.Lang_Global_biaya & " " & Base_Language.Lang_Global_SudahAda & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Cmb_Biaya.Focus() : Exit Sub
            End If
        Next

        Dim lvw As ListViewItem
        lvw = Lv_BiayaDetail.Items.Add(arrIdBiaya.Item(Cmb_Biaya.SelectedIndex))
        lvw.SubItems.Add(Cmb_Biaya.SelectedItem.ToString())
        lvw.SubItems.Add(Txt_InputBiaya.Text)


        Cmb_Biaya.SelectedIndex = -1
        Txt_InputBiaya.Text = ""
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Cmb_Biaya_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Biaya.SelectedIndexChanged

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        TextBox3.Enabled = True : TextBox3.Text = ""
    End Sub

    Private Sub Lv_BiayaDetail_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_BiayaDetail.SelectedIndexChanged

    End Sub

    Private Sub Txt_InputBiaya_TextChanged(sender As Object, e As EventArgs) Handles Txt_InputBiaya.TextChanged

    End Sub

    Private Sub Cmb_Divisi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_JnsKategoriHrg.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_KategoriProduk.Focus()
    End Sub

    Private Sub Cmb_KategoriProduk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_KategoriProduk.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_KategoriKemasan.Focus()
    End Sub

    Private Sub Cmb_KategoriKemasan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_KategoriKemasan.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_KategoriBerat.Focus()
    End Sub

    Private Sub Cmb_KategoriBerat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_KategoriBerat.KeyPress
        If e.KeyChar = Chr(13) Then Txt_RangeHrgMin.Focus()
    End Sub

    Private Sub Txt_RangeHrgMin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_RangeHrgMin.KeyPress
        If e.KeyChar = Chr(13) Then Txt_RangeHrgMax.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub Txt_RangeHrgMax_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_RangeHrgMax.KeyPress
        If e.KeyChar = Chr(13) Then Txt_PersenMarkUp.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub Txt_PersenMarkUp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_PersenMarkUp.KeyPress
        If e.KeyChar = Chr(13) Then TextBox1.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(Asc(".")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub Txt_RangeHrgMax_Leave(sender As Object, e As EventArgs) Handles Txt_RangeHrgMax.Leave
        If Txt_RangeHrgMax.Text.Trim = "" Or Txt_RangeHrgMin.Text = "" Then
            Exit Sub
        End If
        If Convert.ToInt32(Txt_RangeHrgMax.Text) <= Convert.ToInt32(Txt_RangeHrgMin.Text) Then
            MessageBox.Show(Base_Language.Lang_Global_Rangetidakbolehlebihrendah + ". . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_RangeHrgMax.Text = ""
            Txt_RangeHrgMax.Focus()
            Exit Sub
        End If
        '
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_Biaya.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(Asc(".")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub Txt_InputBiaya_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_InputBiaya.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Tambah.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(Asc(".")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub Cmb_Biaya_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Biaya.KeyPress
        If e.KeyChar = Chr(13) Then Txt_InputBiaya.Focus()
    End Sub

    Private Sub Lv_BiayaDetail_DoubleClick(sender As Object, e As EventArgs) Handles Lv_BiayaDetail.DoubleClick

        If Lv_BiayaDetail.SelectedItems.Count > 0 Then
            Dim selectedItem As ListViewItem = Lv_BiayaDetail.SelectedItems(0)

            Dim Hapus As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If Hapus = DialogResult.Yes Then
                Lv_BiayaDetail.Items.Remove(selectedItem)
            End If
        End If

    End Sub

    Private Sub Get_Check_Data()
        Dim id = Lbl_ID.Text

        If Cmb_JnsKategoriHrg.Text.Trim.Length = 0 Then Exit Sub
        If Cmb_KategoriProduk.Text.Trim.Length = 0 Then Exit Sub
        If Cmb_KategoriKemasan.Text.Trim.Length = 0 Then Exit Sub
        If Cmb_KategoriBerat.Text.Trim.Length = 0 Then Exit Sub

        Dim jnsKategoriHrg = Cmb_JnsKategoriHrg.SelectedItem
        Dim kategoriProduk = Cmb_KategoriProduk.SelectedItem
        Dim kategoriKemasan = Cmb_KategoriKemasan.SelectedItem
        Dim kategoriBerat = Cmb_KategoriBerat.SelectedItem

        Try
            OpenConn()
            'Get_Isi_ListView(Lv_Data_MasterJenisMemberPerbarang.FocusedItem.Index)

            SQL = "Select * From "
            SQL = SQL & "EMI_Master_Jenis_Member_Perbarang "
            SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and Id_Jenis_Member_Perbarang = '" & LvID & "' "
            SQL = SQL & "and Id_Jenis_Kategori_Harga = '" & arrJnsKatHrg.Item(Cmb_JnsKategoriHrg.SelectedIndex) & "' "
            SQL = SQL & "and Id_Jenis_Produk = '" & arrKategoriProduk.Item(Cmb_KategoriProduk.SelectedIndex) & "' "
            SQL = SQL & "and Id_Jenis_Kemasan_Utama = '" & arrKategoriKemasan.Item(Cmb_KategoriKemasan.SelectedIndex) & "' "
            SQL = SQL & "and Id_Kapasitas_Kemasan_Utama = '" & arrKategoriBerat.Item(Cmb_KategoriBerat.SelectedIndex) & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Lbl_ID.Text = Dr("Id_Jenis_Member_Perbarang")
                    Txt_RangeHrgMin.Text = Dr("Range_Harga_Min")
                    Txt_RangeHrgMax.Text = Dr("Range_Harga_Max")
                    Txt_PersenMarkUp.Text = Dr("Persen_Markup")
                    TextBox1.Text = Dr("Persen_Penentu_Harga_Jual")
                    Btn_Simpan.Text = Base_Language.Lang_Global_Update
                    Btn_Hapus.Enabled = True
                Else
                    Lbl_ID.Text = ""
                    Txt_RangeHrgMin.Text = ""
                    Txt_RangeHrgMax.Text = ""
                    Txt_PersenMarkUp.Text = ""
                    TextBox1.Text = ""
                    Btn_Simpan.Text = Base_Language.Lang_Global_Simpan : Btn_Hapus.Enabled = True
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'Get_Data_Biaya_Detail()
    End Sub

    Private Sub Cmb_JnsKategoriHrg_Leave(sender As Object, e As EventArgs) Handles Cmb_JnsKategoriHrg.Leave
        Get_Check_Data()
    End Sub

    Private Sub Cmb_KategoriProduk_Leave(sender As Object, e As EventArgs) Handles Cmb_KategoriProduk.Leave
        Get_Check_Data()
    End Sub

    Private Sub Cmb_KategoriKemasan_Leave(sender As Object, e As EventArgs) Handles Cmb_KategoriKemasan.Leave
        Get_Check_Data()
    End Sub

    Private Sub Cmb_KategoriBerat_Leave(sender As Object, e As EventArgs) Handles Cmb_KategoriBerat.Leave
        Get_Check_Data()
    End Sub

End Class