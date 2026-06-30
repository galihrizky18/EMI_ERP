Imports System.ComponentModel


Public Class SD_Tambah_PR_Barang_Lain
    Public filter_tambahan, filter_kdSupplier As String
    Public asal, faktur_MR, SO_Kategori_Gudang_Pilih As String
    Dim arrcari As New ArrayList
    Dim Jenis = "Tampil_Barang"


    Dim FlagOnLoad As Boolean = False

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
        Try
            OpenConn()

            CmbPilihBarang_Lokasi.Items.Clear()

            SQL = "select Kode_Stock_Owner from View_Lokasi_Stock_Lain where Aktif='Y' "
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

        Txt_CostCenter.Text = "" : Txt_CostCenter.Text = "" : Txt_LokasiGudang.Text = ""
        Txt_KdGedung.Text = "" : Txt_Gedung.Text = "" : Txt_IdGedung.Text = ""
        Txt_SisaStock.Text = ""

        TxtPilihBarang_KodeBarang.Text = "" : TxtPilihBarang_Satuan.Text = ""
        TxtPilihBarang_NamaBarang.Text = "" : CmbPilihBarang_Satuan.Text = "" : CmbPilihBarang_Satuan.SelectedIndex = -1

        Cmb_Stock.Items.Clear()
        Cmb_Stock.Items.Add("Y") : Cmb_Stock.Items.Add("T")
        Cmb_Stock.SelectedIndex = 0



        'If asal = "SD_Data_PR_Barang_Lain" Then
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
        '        SQL = "select Satuan from Barang_Detail_Satuan_Lain where Kode_Barang = '" & Trim(Lbl_GetKdBrg.Text) & "' "
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

            SQL = "select Kode_Stock_Owner from View_Lokasi_Stock_Lain where Aktif='Y' "
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

        FlagOnLoad = True

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

            If asal = "Purchase_Requisition_Barang_Lain" Then
                LvPilihBarang_DataBarang.Items.Clear()
                TxtPilihBarang_KodeBarang.Text = ""
                TxtPilihBarang_NamaBarang.Text = ""
                TxtPilihBarang_Satuan.Text = ""
                DateTimePicker1.ResetText()
                txtJumlah.Text = ""
                txtKeterangan.Text = ""
                Txt_KDSo.Text = ""
                Txt_LokasiGudang.Text = ""
                Txt_SisaStock.Text = ""
                TextBox1.Text = ""

                Txt_CostCenter.Text = "" : Txt_CostCenter.Text = ""
                Txt_KdGedung.Text = "" : Txt_Gedung.Text = "" : Txt_IdGedung.Text = ""

                Cmb_Stock.Items.Clear()
                Cmb_Stock.Items.Add("Y") : Cmb_Stock.Items.Add("T")
                Cmb_Stock.SelectedIndex = 0

                CmbPilihBarang_Satuan.Items.Clear()
                TxtPilihBarang_KodeBarang.Focus()
                TxtPilihBarang_KodeBarang.Enabled = True
                TxtPilihBarang_NamaBarang.Enabled = False
                TxtPilihBarang_Satuan.Enabled = False
                txtJumlah.Enabled = True

                Try
                    OpenConn()

                    CmbPilihBarang_Lokasi.Items.Clear()

                    SQL = "select Kode_Stock_Owner from View_Lokasi_Stock_Lain where Aktif='Y' "
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

        FlagOnLoad = False

        Lv_CostCenter.Columns.Clear() : Lv_CostCenter.Items.Clear()
        Lv_CostCenter.Columns.Add("ID Cost Center", 100, HorizontalAlignment.Left)
        Lv_CostCenter.Columns.Add("Kode Cost Center", 130, HorizontalAlignment.Left)
        Lv_CostCenter.Columns.Add("Keterangan", 180, HorizontalAlignment.Left)
        Lv_CostCenter.View = View.Details

        Lv_Gedung.Columns.Clear() : Lv_Gedung.Items.Clear()
        Lv_Gedung.Columns.Add("Kode Gedung", 100, HorizontalAlignment.Left)
        Lv_Gedung.Columns.Add("Keterangan", 180, HorizontalAlignment.Left)
        Lv_Gedung.Columns.Add("Id_Gedung", 0, HorizontalAlignment.Left)
        Lv_Gedung.View = View.Details



        LvPilihBarang_DataBarang.Visible = False
        LvPilihBarang_DataBarang.Location = New Point(139, 110)


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
        If asal = "Purchase_Requisition_Barang_Lain" Then
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

                    'SQL = "select a.Kode_Barang,a.Nama, a.Satuan, c.lokasi_gudang "
                    'SQL = SQL & "from Barang_Lain a, EMI_Group_Jenis_Lain b, EMI_Kategori_Gudang_PerLokasi_Barang_Lain c where a.Kode_Perusahaan=b.Kode_Perusahaan  "
                    'SQL = SQL & "and a.Id_Group_Jenis=b.Id_Group_Jenis and a.Kode_Perusahaan='" & KodePerusahaan & "'  "
                    'SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang "
                    'SQL = SQL & "and Nama like '%" & TxtPilihBarang_KodeBarang.Text & "%' and aktif = 'Y' and (b.Flag_Raw_Material = 'Y' or b.Flag_Packaging = 'Y' or b.Flag_bahan_bakar = 'Y') " & filter_tambahan & " "
                    'SQL = SQL & "group by a.Kode_Barang,a.Nama, a.Satuan,c.lokasi_gudang "

                    SQL = "select a.Kode_Barang,a.Nama, a.Satuan, c.lokasi_gudang "
                    SQL = SQL & "from Barang_Lain a, EMI_Group_Jenis_Lain b, EMI_Kategori_Gudang_PerLokasi_Barang_Lain c "

                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                    SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis and a.Kode_Perusahaan='" & KodePerusahaan & "'  "

                    SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang and a.Flag_Barang_Lama is null "
                    SQL = SQL & "and Nama like '%" & TxtPilihBarang_KodeBarang.Text & "%' and aktif = 'Y' " & filter_tambahan & " "
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


        Try
            OpenConn()

            Dim hasData As Boolean = False
            SQL = "select a.Kode_Barang,a.Nama, a.Satuan, c.lokasi_gudang, e.Kode_Stock_Owner_Gudang "
            SQL = SQL & "from Barang_Lain a, EMI_Group_Jenis_Lain b, EMI_Kategori_Gudang_PerLokasi_Barang_Lain c, "
            SQL = SQL & "View_Kategori_Turunan d, N_EMI_View_Master_Kategori_Gudang_Binding_Barang_Lain e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis and a.Kode_Perusahaan='" & KodePerusahaan & "'  "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis_3 = d.Id_Sub_Kategori_Jenis_3 "
            SQL = SQL & "and d.Id_Kategori_Jenis = e.Id_Kategori_Jenis and d.Id_Sub_Kategori_Jenis = e.Id_Sub_Kategori_Jenis and e.User_ID = '" & UserID & "' "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Kategori_Gudang = c.ID_Kategori_Gudang and a.Flag_Barang_Lama is null "
            SQL = SQL & "and a.kode_barang = '" & TxtPilihBarang_KodeBarang.Text & "' and aktif = 'Y' " & filter_tambahan & " "
            SQL = SQL & "group by a.Kode_Barang,a.Nama, a.Satuan,c.lokasi_gudang, e.Kode_Stock_Owner_Gudang "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    CmbPilihBarang_Lokasi.Text = dr("Kode_Stock_Owner_Gudang")
                    TxtPilihBarang_KodeBarang.Text = dr("kode_barang")
                    TxtPilihBarang_NamaBarang.Text = dr("nama")
                    TxtPilihBarang_Satuan.Text = dr("Satuan")
                    dr.Close()
                    CmbPilihBarang_Satuan.Items.Clear()

                    Dim satuan As String = ""
                    Dim indexTampilDisplay As Integer
                    Dim index As Integer = 0
                    SQL = "select Satuan,Flag_Tampil_Display from Barang_Detail_Satuan_Lain where Kode_Barang= '" & Trim(TxtPilihBarang_KodeBarang.Text) & "' "
                    Using dr2 = OpenTrans(SQL)
                        Do While dr2.Read
                            CmbPilihBarang_Satuan.Items.Add(dr2("satuan"))

                            If General_Class.CekNULL(dr2("Flag_Tampil_Display")) = "Y" Then
                                indexTampilDisplay = index
                            End If

                            index = index + 1
                        Loop
                    End Using
                    CmbPilihBarang_Satuan.SelectedIndex = indexTampilDisplay
                    CmbPilihBarang_Satuan.Enabled = False
                    'BtnPilihBarang_Simpan.Focus()

                    txtJumlah.Focus()

                    hasData = True

                    LvPilihBarang_DataBarang.Visible = False


                    Dim GudangDepartment As String = ""

                    'ambil gudang departmenet berdasrkan user login 

                    SQL = "select  Kode_Stock_Owner_Gudang from barang_lain a , View_Kategori_Turunan b,N_EMI_View_Master_Kategori_Gudang_Binding_Barang_Lain c "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis_3 =b.Id_Sub_Kategori_Jenis_3 "
                    SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Kategori_Jenis = c.id_kategori_jenis "
                    SQL = SQL & "and b.Id_Sub_Kategori_Jenis = c.id_sub_kategori_jenis  "
                    SQL = SQL & "and c.user_id = '" & UserID & "' and a.Kode_Barang = '" & TxtPilihBarang_KodeBarang.Text & "' "
                    Using Dr2 = OpenTrans(SQL)
                        If Dr2.Read Then
                            GudangDepartment = Format(Dr2("Kode_Stock_Owner_Gudang"))
                        Else
                            dr.Close()
                            CloseConn()
                            MessageBox.Show("Gagal, Gudang tujuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using


                    'SQL = "select sum(Good_Stock) as Stock from Barang_Lain "
                    'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_barang = '" & TxtPilihBarang_KodeBarang.Text & "' and aktif = 'Y' "

                    SQL = "select sum(b.Jumlah) as Stock from barang_lain a, Barang_Lain_SN b "
                    SQL = SQL & "where a.Kode_Barang = '" & TxtPilihBarang_KodeBarang.Text & "' and a.kode_perusahaan = '" & KodePerusahaan & "' and b.Kode_Stock_Owner = '" & GudangDepartment & "' "
                    SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                    SQL = SQL & "and b.Jumlah <> 0 "
                    Using Dr2 = OpenTrans(SQL)
                        If Dr2.Read Then

                            If General_Class.CekNULL(Dr2("Stock")) <> "" Then
                                Txt_SisaStock.Text = Format(Dr2("Stock"), "N2")
                            Else
                                Txt_SisaStock.Text = 0
                            End If

                            'Txt_SisaStock.Text = Format(Dr2("Stock"), "N2")
                        Else
                            dr.Close()
                            CloseConn()
                            MessageBox.Show("Terjadi kesalahan pada SN barang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using


                    'SQL = "select sum(Good_Stock) as Stock from Barang_Lain "
                    'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_barang = '" & TxtPilihBarang_KodeBarang.Text & "' and aktif = 'Y' "
                    'Using Dr2 = OpenTrans(SQL)
                    '    If Dr2.Read Then
                    '        Txt_SisaStock.Text = Format(Dr2("Stock"), "N2")
                    '    Else
                    '        Txt_SisaStock.Text = Format(0, "N2")
                    '    End If
                    'End Using


                    SQL = "select top(1) Waktu_Pengiriman from emi_detail_proses_pengiriman_po_Barang_Lain where Kode_Barang= '" & Trim(TxtPilihBarang_KodeBarang.Text) & "' "
                    SQL = SQL & "order by Waktu_Pengiriman Desc "
                    Using dr2 = OpenTrans(SQL)
                        If dr2.Read Then
                            TxtTiba.Text = dr2("Waktu_Pengiriman")
                        Else
                            TxtTiba.Text = 0
                        End If
                    End Using

                Else
                    TxtPilihBarang_KodeBarang.Text = ""
                    TxtPilihBarang_NamaBarang.Text = ""
                    TxtPilihBarang_Satuan.Text = ""
                    Txt_SisaStock.Text = ""
                    CmbPilihBarang_Satuan.Items.Clear()
                    TxtPilihBarang_KodeBarang.Focus()
                End If
            End Using

            Dim harga As Double = 0

            'SQL = "select top(1) dbo.get_hpp(Serial_Number) as Harga From Barang_Lain_sn where kode_barang = '" & TxtPilihBarang_KodeBarang.Text.Trim & "' "
            'SQL = SQL & "and blok_sn is null "
            'SQL = SQL & "order by Serial_Number "
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        harga = Dr("harga")
            '    End If
            'End Using


            SQL = " select top(1) Harga From EMI_Pembelian_PO_Barang_Lain a, emi_pembelian_po_detail_barang_lain b "
            SQL = SQL & " where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.kode_barang = '" & TxtPilihBarang_KodeBarang.Text & "' and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "order by a.Tanggal desc ,jam desc "

            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    harga = Dr("harga")
                End If
            End Using
            TxtHarga.Text = harga

            If hasData Then


                '===================================
                '=     CEK APAKAH BARANG ASSET     =
                '===================================
                Dim isAsset As Boolean = False
                SQL = "select Top 1 a.Kode_Barang, a.Nama, b.Flag_Asset "
                SQL = SQL & "from Barang_Lain a, EMI_Group_Jenis_Lain b "
                SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan "
                SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Kode_Stock_Owner = '" & Txt_LokasiGudang.Text & "' "
                SQL = SQL & "and a.Kode_Barang = '" & TxtPilihBarang_KodeBarang.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("Flag_Asset")) = "Y" Then
                            isAsset = True
                        End If
                    End If
                End Using

                If isAsset Then
                    Txt_CostCenter.Enabled = True
                    Txt_KdGedung.Enabled = True
                    Txt_Gedung.Enabled = True

                Else
                    Txt_CostCenter.Enabled = False
                    Txt_KdGedung.Enabled = False
                    Txt_Gedung.Enabled = True
                End If



                Lv_Gedung.Visible = False

            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    ''
    Private Sub TxtKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPilihBarang_KodeBarang.KeyPress, Txt_IdGedung.KeyPress, Txt_LokasiGudang.KeyPress, Txt_SisaStock.KeyPress
        If e.KeyChar = Chr(13) Then CmbPilihBarang_Satuan.Focus()

    End Sub

    Private Sub TxtKode_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPilihBarang_KodeBarang.KeyDown, Txt_IdGedung.KeyDown, Txt_LokasiGudang.KeyDown, Txt_SisaStock.KeyDown
        If e.KeyCode = Keys.Down Then
            If LvPilihBarang_DataBarang.Items.Count = 0 Then Exit Sub
            LvPilihBarang_DataBarang.Focus()
        End If
    End Sub

    Private Sub Lv2_DoubleClick(sender As Object, e As EventArgs) Handles LvPilihBarang_DataBarang.DoubleClick
        If LvPilihBarang_DataBarang.Items.Count = 0 Then Exit Sub

        Dim KdSo As String = LvPilihBarang_DataBarang.FocusedItem.Text
        Dim KdBarang As String = LvPilihBarang_DataBarang.FocusedItem.SubItems(1).Text

        TxtPilihBarang_KodeBarang.Text = KdBarang
        Txt_LokasiGudang.Text = KdSo

        Try
            OpenConn()

            SQL = "select distinct a.Kode_Barang from Barang_Lain a, View_Kategori_Turunan d, N_EMI_View_Master_Kategori_Gudang_Binding_Barang_Lain e  "
            SQL = SQL & "where a.Kode_Perusahaan = d.Kode_Perusahaan and a.Id_Sub_Kategori_Jenis_3 = d.Id_Sub_Kategori_Jenis_3 "
            SQL = SQL & "and d.Id_Kategori_Jenis = e.Id_Kategori_Jenis and d.Id_Sub_Kategori_Jenis = e.Id_Sub_Kategori_Jenis and e.User_ID = '" & UserID & "' "
            SQL = SQL & "and e.Kode_Stock_Owner_Gudang = '" & SO_Kategori_Gudang_Pilih.Trim & "' and a.Kode_Barang = '" & TxtPilihBarang_KodeBarang.Text & "' "

            'SQL = "select a.Kode_Barang from Barang_Lain a, N_EMI_Master_Role_Sub_Kategori b "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Kategori_Jenis = b.Kode_Kategori_Jenis "
            'SQL = SQL & "and a.Kode_Sub_Kategori_Jenis = b.Kode_Sub_Kategori_Jenis and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.Kode_Stock_Owner = '" & Txt_LokasiGudang.Text & "' and a.Kode_Barang = '" & TxtPilihBarang_KodeBarang.Text & "' "
            'SQL = SQL & "and b.UserID = '" & UserID & "' "
            Using dr = OpenTrans(SQL)
                If Not dr.Read Then
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("user tidak memiliki akses ke barang tersebut", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    TxtPilihBarang_KodeBarang.Text = ""
                    Txt_LokasiGudang.Text = ""
                    TxtHarga.Text = 0
                    TextBox1.Text = ""

                    TxtPilihBarang_KodeBarang.Focus()
                    Exit Sub
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        TxtPilihBarang_KodeBarang.Focus()
        DateTimePicker1.Focus()
        LvPilihBarang_DataBarang.Visible = False
    End Sub

    Private Sub Lv2_KeyDown(sender As Object, e As KeyEventArgs) Handles LvPilihBarang_DataBarang.KeyDown
        If e.KeyCode = Keys.Enter Then Lv2_DoubleClick(LvPilihBarang_DataBarang, e)
    End Sub

    Private Sub Btn_Simpan_Click_1(sender As Object, e As EventArgs) Handles BtnPilihBarang_Simpan.Click
        get_jam()

        If asal = "SD_Data_PR_Barang_Lain" Then
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


            If Format(DateTimePicker1.Value, "yyyy-MM-dd") < Format(tgl_skg, "yyyy-MM-dd") Then
                MessageBox.Show("Tanggal Kebutuhan tidak boleh kurang dari tanggal hari ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                CmbPilihBarang_Satuan.Focus()
                Exit Sub
            End If


            ' SD_Data_PR_Barang_Lain.DataGridView1.CurrentRow.Cells(7).Value = txtJumlah.Text
            SD_Data_PR_Barang_Lain.DataGridView1.CurrentRow.Cells(7).Value = CmbPilihBarang_Satuan.Text
            SD_Data_PR_Barang_Lain.DataGridView1.CurrentRow.Cells(8).Value = Format(DateTimePicker1.Value, "dd MMM yyyy")
            'SD_Data_PR_Barang_Lain.DataGridView1.CurrentRow.Cells(10).Value = txtKeterangan.Text
            Me.Close()

        ElseIf asal = "Purchase_Requisition_Barang_Lain" Then

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

            If Format(DateTimePicker1.Value, "yyyy-MM-dd") < Format(tgl_skg, "yyyy-MM-dd") Then
                MessageBox.Show("Tanggal Kebutuhan tidak boleh kurang dari tanggal hari ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                CmbPilihBarang_Satuan.Focus()
                Exit Sub
            End If


            Dim Diff As Integer = DateDiff(DateInterval.Day, tgl_skg, DateTimePicker1.Value)


            If Diff < Val(TxtTiba.Text) Then
                Dim Msg As String = MessageBox.Show("Tanggal Dibutuhkan Kurang dari Estimasi Tiba,  Tetap Lanjutkan ? ", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If Msg = vbNo Then
                    '   DateTimePicker1.Value = DateAdd(DateInterval.Day, Val(TxtTiba.Text) + 1, tgl_skg)
                    Exit Sub
                End If
            End If

            Dim isAsset As Boolean = False

            Try
                OpenConn()

                '===================================
                '=     CEK APAKAH BARANG ASSET     =
                '===================================
                SQL = "select Top 1 a.Kode_Barang, a.Nama, b.Flag_Asset "
                SQL = SQL & "from Barang_Lain a, EMI_Group_Jenis_Lain b "
                SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan "
                SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Kode_Stock_Owner = '" & Txt_LokasiGudang.Text & "' "
                SQL = SQL & "and a.Kode_Barang = '" & TxtPilihBarang_KodeBarang.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("Flag_Asset")) = "Y" Then
                            isAsset = True
                        End If
                    End If
                End Using

                If isAsset Then


                    '============================================
                    '=     CEK APAKAH COST CENTER DITEMUKAN     =
                    '============================================
                    SQL = "select Id_Cost_Center, Kode_Cost_Center, Keterangan "
                    SQL = SQL & "from EMI_Master_Cost_Center "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and Id_Cost_Center = '" & Txt_Id_CostCenter.Text & "' "
                    Using Dr = OpenTrans(SQL)
                        If Not Dr.Read Then
                            CloseConn()
                            MessageBox.Show("Terjadi Kesahalahan . . ! !, Cost Center Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Txt_CostCenter.Focus()
                            Exit Sub
                        End If
                    End Using

                    '=======================================
                    '=     CEK APAKAH GEDUNG DITEMUKAN     =
                    '=======================================
                    SQL = "select Kode_Gedung, Keterangan, Id_Gedung "
                    SQL = SQL & "from N_EMI_Master_Gedung_Barang_Lain "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and Id_Gedung = '" & Txt_IdGedung.Text & "' "
                    Using Dr = OpenTrans(SQL)
                        If Not Dr.Read Then
                            CloseConn()
                            MessageBox.Show("Terjadi Kesahalahan . . ! !, Gedung Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Txt_KdGedung.Focus()
                            Exit Sub
                        End If
                    End Using

                End If

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

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
                    SQL = SQL & "isnull((select sum(y.jumlah) from EMI_Purchase_Requisition_Barang_Lain x, EMI_Purchase_Requisition_Barang_Lain_Detail y "
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

            For i As Integer = 0 To Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows.Count - 1
                If TxtPilihBarang_KodeBarang.Text.Trim = Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(i).Cells(1).Value Then
                    If CmbPilihBarang_Satuan.Text = Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(i).Cells(4).Value Then
                        If Format(DateTimePicker1.Value, "dd MMM yyyy") = Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(i).Cells(5).Value _
                            And Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(i).Cells(9).Value = Txt_Id_CostCenter.Text _
                            And Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(i).Cells(10).Value = Txt_IdGedung.Text Then
                            Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(i).Cells(3).Value = Val(Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(i).Cells(3).Value) + Val(txtJumlah.Text)
                            Me.Close()
                            Exit Sub
                        End If
                    End If
                End If
            Next

            Dim index As Integer = 0

            index = Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows.Count - 1

            Dim jumlahIndexDGv As Integer

            Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows.Add(1)

            'buat ambil jumlah dgv nya
            jumlahIndexDGv = Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows.Count - 1

            'Purchase_Requisition_Barang_Lain
            Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(0).Value = CmbPilihBarang_Lokasi.Text
            Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(1).Value = TxtPilihBarang_KodeBarang.Text
            Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(2).Value = TxtPilihBarang_NamaBarang.Text
            'Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(3).Value = txtJumlah.Text.Trim
            Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(3).Value = 0
            Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(4).Value = CmbPilihBarang_Satuan.Text
            Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(5).Value = Format(DateTimePicker1.Value, "dd MMM yyyy")
            Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(6).Value = txtKeterangan.Text.Trim
            Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(7).Value = Sisa
            Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(8).Value = 0

            If TextBox1.Text.Trim.Length = 0 Then
                Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(9).Value = "-"
            Else
                Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(9).Value = TextBox1.Text
            End If

            If isAsset Then
                Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(10).Value = Txt_Id_CostCenter.Text
                Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(11).Value = Txt_IdGedung.Text
                Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(13).Value = Txt_CostCenter.Text
                Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(14).Value = Txt_Gedung.Text
            Else
                Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(10).Value = Ket_Cost_Center_HO_Proyek
                Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(11).Value = ""
                Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(13).Value = "-"
                Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(14).Value = "-"
            End If

            Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(12).Value = Cmb_Stock.Text

            If Cmb_Stock.Text = "Y" Then
                Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(15).Value = "Stock"
            Else
                Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(15).Value = "Non Stock"
            End If

            Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(19).Value = TxtTiba.Text


            If TxtHarga.Text <> 0 Then
                Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(18).ReadOnly = True
                Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(18).Value = Format(Val(TxtHarga.Text), "N2")

                Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(20).Value = "Y"
            Else
                Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(18).ReadOnly = False
                Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(18).Value = 0

                Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(index).Cells(20).Value = "T"

            End If

            Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(jumlahIndexDGv).Cells(0).ReadOnly = True
            Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(jumlahIndexDGv).Cells(1).ReadOnly = True
            Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(jumlahIndexDGv).Cells(2).ReadOnly = True
            Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(jumlahIndexDGv).Cells(3).ReadOnly = False
            Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(jumlahIndexDGv).Cells(4).ReadOnly = True
            Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(jumlahIndexDGv).Cells(5).ReadOnly = True
            Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(jumlahIndexDGv).Cells(6).ReadOnly = False



            Purchase_Requisition_Barang_Lain.HasData_DGV()

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

            '        SQL = "select satuan from Barang_Detail_Satuan_Lain where Kode_Barang ='" & TxtPilihBarang_KodeBarang.Text & "'  and Kode_Perusahaan='" & KodePerusahaan & "' "
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


    Private Sub Txt_IDCostCenter_TextChanged(sender As Object, e As EventArgs) Handles Txt_CostCenter.TextChanged
        If Txt_CostCenter.Text.Trim.Length = 0 Then
            Me.Size = New Size(593, 404)
            Lv_CostCenter.Location = New Point(650, 190)
            Lv_CostCenter.Visible = False
            Txt_CostCenter.Text = ""
            Txt_CostCenter.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(593, 422)
            Lv_CostCenter.Visible = True
            Lv_CostCenter.Location = New Point(139, 190)
        End If

        Try
            OpenConn()

            Lv_CostCenter.Items.Clear()

            SQL = "select Id_Cost_Center, Kode_Cost_Center, Keterangan from EMI_Master_Cost_Center where Kode_Perusahaan = '" & KodePerusahaan & "' and Keterangan like '%" & Txt_CostCenter.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_CostCenter.Items.Add(Dr("Id_Cost_Center"))
                    Lv.SubItems.Add(Dr("Kode_Cost_Center"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_IDCostCenter_Leave(sender As Object, e As EventArgs) Handles Txt_CostCenter.Leave
        If Txt_CostCenter.Text.Trim.Length = 0 Then Exit Sub
        If Lv_CostCenter.Focused = True Then Exit Sub

        Try
            OpenConn()


            SQL = "select Id_Cost_Center, Kode_Cost_Center, Keterangan from EMI_Master_Cost_Center where Kode_Perusahaan = '" & KodePerusahaan & "' and Keterangan = '" & Txt_CostCenter.Text & "'"
            Using Dr = Open(SQL)
                If Dr.Read Then
                    Txt_Id_CostCenter.Text = Dr("Id_Cost_Center")
                    Txt_CostCenter.Text = Dr("Keterangan")
                    Txt_KdGedung.Focus()
                Else
                    MessageBox.Show("Cost Center tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Txt_Id_CostCenter.Text = ""
                    Txt_CostCenter.Text = ""
                    Txt_CostCenter.Focus()
                End If

                Me.Size = New Size(593, 404)
                Lv_CostCenter.Location = New Point(650, 190)
                Lv_CostCenter.Visible = False
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_IDCostCenter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_CostCenter.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_CostCenter.Text.Trim.Length = 0 Then Txt_CostCenter.Focus()
            Txt_IDCostCenter_Leave(Txt_CostCenter, e)

            Me.Size = New Size(593, 404)
            Lv_CostCenter.Location = New Point(650, 190)
            Lv_CostCenter.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_IDCostCenter_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_CostCenter.KeyDown
        If e.KeyCode = Keys.Down Then Lv_CostCenter.Focus()
    End Sub

    Private Sub Lv_CostCenter_DoubleClick(sender As Object, e As EventArgs) Handles Lv_CostCenter.DoubleClick
        If Lv_CostCenter.Items.Count = 0 Or Lv_CostCenter.FocusedItem.Index = -1 Then Exit Sub

        Dim Id_CostCenter As String = Lv_CostCenter.FocusedItem.SubItems(0).Text
        Dim NmCostCenter As String = Lv_CostCenter.FocusedItem.SubItems(2).Text

        Txt_Id_CostCenter.Text = Id_CostCenter
        Txt_CostCenter.Text = NmCostCenter

        Me.Size = New Size(593, 404)
        Lv_CostCenter.Location = New Point(650, 190)
        Lv_CostCenter.Visible = False

        Txt_KdGedung.Focus()
    End Sub

    Private Sub Lv_CostCenter_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_CostCenter.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_CostCenter_DoubleClick(Lv_CostCenter, e)
        End If
    End Sub

    Private Sub Txt_KdGedung_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdGedung.TextChanged
        If Txt_KdGedung.Text.Trim.Length = 0 Then
            Me.Size = New Size(593, 404)
            Lv_Gedung.Location = New Point(650, 216)
            Lv_Gedung.Visible = False
            Txt_KdGedung.Text = ""
            Txt_Gedung.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(593, 450)
            Lv_Gedung.Visible = True
            Lv_Gedung.Location = New Point(139, 216)
        End If

        Try
            OpenConn()

            Lv_Gedung.Items.Clear()

            SQL = "select Kode_Gedung, Keterangan, Id_Gedung from N_EMI_Master_Gedung_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Gedung like '%" & Txt_KdGedung.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Gedung.Items.Add(Dr("Kode_Gedung"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("Id_Gedung"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Gedung_TextChanged(sender As Object, e As EventArgs) Handles Txt_Gedung.TextChanged
        If Txt_Gedung.Text.Trim.Length = 0 Then
            Me.Size = New Size(593, 404)
            Lv_Gedung.Location = New Point(650, 216)
            Lv_Gedung.Visible = False
            Txt_KdGedung.Text = ""
            Txt_Gedung.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(593, 450)
            Lv_Gedung.Visible = True
            Lv_Gedung.Location = New Point(139, 216)
        End If

        Try
            OpenConn()

            Lv_Gedung.Items.Clear()

            SQL = "select Kode_Gedung, Keterangan, Id_Gedung from N_EMI_Master_Gedung_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and Keterangan like '%" & Txt_Gedung.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Gedung.Items.Add(Dr("Kode_Gedung"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("Id_Gedung"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_KdGedung_Leave(sender As Object, e As EventArgs) Handles Txt_KdGedung.Leave
        If Txt_KdGedung.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Gedung.Focused = True Then Exit Sub

        Try
            OpenConn()


            SQL = "select Kode_Gedung, Kode_Gedung, Keterangan, Id_Gedung from N_EMI_Master_Gedung_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Gedung = '" & Txt_KdGedung.Text & "'"
            Using Dr = Open(SQL)
                If Dr.Read Then
                    Txt_KdGedung.Text = Dr("Kode_Gedung")
                    Txt_Gedung.Text = Dr("Keterangan")
                    Txt_IdGedung.Text = Dr("Id_Gedung")
                    DateTimePicker1.Focus()
                Else
                    MessageBox.Show("Gedung tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Txt_KdGedung.Text = ""
                    Txt_Gedung.Text = ""
                    Txt_IdGedung.Text = ""
                    Txt_KdGedung.Focus()
                End If

                Me.Size = New Size(593, 404)
                Lv_Gedung.Location = New Point(650, 216)
                Lv_Gedung.Visible = False
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_KdGedung_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdGedung.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdGedung.Text.Trim.Length = 0 Then Txt_KdGedung.Focus()
            Txt_KdGedung_Leave(Txt_KdGedung, e)

            Me.Size = New Size(593, 450)
            Lv_Gedung.Location = New Point(650, 216)
            Lv_Gedung.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_KdGedung_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdGedung.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Gedung.Focus()
    End Sub

    Private Sub Lv_Gedung_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Gedung.DoubleClick
        If Lv_Gedung.Items.Count = 0 Or Lv_Gedung.FocusedItem.Index = -1 Then Exit Sub

        Dim KdGedung As String = Lv_Gedung.FocusedItem.SubItems(0).Text
        Dim NmGedung As String = Lv_Gedung.FocusedItem.SubItems(1).Text
        Dim Id_Gedung As String = Lv_Gedung.FocusedItem.SubItems(2).Text

        Txt_KdGedung.Text = KdGedung
        Txt_Gedung.Text = NmGedung
        Txt_IdGedung.Text = Id_Gedung

        Me.Size = New Size(593, 404)
        Lv_Gedung.Location = New Point(650, 216)
        Lv_Gedung.Visible = False

        DateTimePicker1.Focus()
    End Sub

    Private Sub Lv_Gedung_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Gedung.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Gedung_DoubleClick(Lv_Gedung, e)
        End If
    End Sub

    Private Sub Txt_Gedung_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Gedung.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdGedung_Leave(Txt_Gedung, e)

            Me.Size = New Size(593, 404)
            Lv_Gedung.Location = New Point(650, 216)
            Lv_Gedung.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_Gedung_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Gedung.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Gedung.Focus()
    End Sub

    Private Sub Cmb_Stock_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Stock.SelectedIndexChanged
        If Cmb_Stock.SelectedIndex = 1 Then
            'Txt_CostCenter.Enabled = True
            'Txt_KdGedung.Enabled = True
            'Txt_Gedung.Enabled = True

        Else
            'Txt_CostCenter.Enabled = False
            'Txt_KdGedung.Enabled = False
            'Txt_Gedung.Enabled = False
        End If

        'Txt_CostCenter.Text = "" : Txt_Id_CostCenter.Text = ""
        'Txt_KdGedung.Text = "" : Txt_Gedung.Text = "" : Txt_IdGedung.Text = ""
    End Sub


    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        If FlagOnLoad Then Exit Sub
        get_jam()

        Dim Diff As Integer = DateDiff(DateInterval.Day, tgl_skg, DateTimePicker1.Value)


        If Diff < Val(TxtTiba.Text) Then
            Dim Msg As String = MessageBox.Show("Tanggal Dibutuhkan Kurang dari Estimasi Tiba,  Tetap Lanjutkan ? ", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If Msg = vbNo Then
                DateTimePicker1.Value = DateAdd(DateInterval.Day, Val(TxtTiba.Text) + 1, tgl_skg)
            End If
        End If
    End Sub

    Private Sub CmbPilihBarang_Satuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbPilihBarang_Satuan.KeyPress, Cmb_Stock.KeyPress
        If e.KeyChar = Chr(13) Then txtKeterangan.Focus()
    End Sub


    Private Sub txtJumlah_Validating(sender As Object, e As CancelEventArgs) Handles txtJumlah.Validating
        If asal = "SD_Data_PR_Barang_Lain" Then
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