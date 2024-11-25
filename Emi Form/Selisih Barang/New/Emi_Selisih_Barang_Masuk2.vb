Imports BigIntegerLibrary

Public Class Emi_Selisih_Barang_Masuk2

    Dim arrInisialFaktur As New ArrayList

    Dim Dgv_Lokasi, Dgv_KdBarang, Dgv_Nama, Dgv_JmlhPL, Dgv_JmlhBM, Dgv_Satuan, Dgv_Selisih, Dgv_Penyelesaian_Tambah, Dgv_PenyelesaianKurang, Dgv_Keterangan As String
    Dim Dgv_Selisih_Rp, Dgv_Selisih_Fix, Dgv_HargaPerPcs, Dgv_TglProduksi, Dgv_TglExpired, Dgv_SN, Dgv_JmlhPlHitung, Dgv_JmlhBMHitung, Dgv_HargaHitung As String

    Public P_NoFakturPO As String = ""
    Public P_Supplier As String = ""
    Public P_KdSupplier As String = ""
    Public P_NoSJ As String = ""
    Public P_NoPlat As String = ""
    Public P_Driver As String = ""
    Public P_TglMasuk As String = ""
    Public P_TglBerangkat As String = ""
    Public P_JamMasuk As String = ""

    Dim Cell_Lokasi As Integer = 0
    Dim Cell_KdBarang As Integer = 1
    Dim Cell_Nama As Integer = 2
    Dim Cell_JmlhPL As Integer = 3
    Dim Cell_JmlhBM As Integer = 4
    Dim Cell_Satuan As Integer = 5
    Dim Cell_Selisih As Integer = 6
    Dim Cell_QtyPenyelesaian_Tambah As Integer = 7
    Dim Cell_QtyPenyelesaian_Kurang As Integer = 8
    Dim Cell_Keterangan As Integer = 9
    Dim Cell_Selisih_RP As Integer = 10
    Dim Cell_Selisih_Fix As Integer = 11
    Dim Cell_HargaPerPcs As Integer = 12
    Dim Cell_TglProduksi As Integer = 13
    Dim Cell_TglExpired As Integer = 14
    Dim Cell_SN As Integer = 15
    Dim Cell_jmlhPlHitung As Integer = 16
    Dim Cell_jmlhBMHitung As Integer = 17
    Dim Cell_hargaHitung As Integer = 18

    Private Sub Get_DGV_Data(ByVal Index As Integer)

        Dgv_Lokasi = Dgv_DetailBarang.Rows(Index).Cells(Cell_Lokasi).Value
        Dgv_KdBarang = Dgv_DetailBarang.Rows(Index).Cells(Cell_KdBarang).Value
        Dgv_Nama = Dgv_DetailBarang.Rows(Index).Cells(Cell_Nama).Value
        Dgv_JmlhPL = Dgv_DetailBarang.Rows(Index).Cells(Cell_JmlhPL).Value
        Dgv_JmlhBM = Dgv_DetailBarang.Rows(Index).Cells(Cell_JmlhBM).Value
        Dgv_Satuan = Dgv_DetailBarang.Rows(Index).Cells(Cell_Satuan).Value
        Dgv_Selisih = Dgv_DetailBarang.Rows(Index).Cells(Cell_Selisih).Value
        Dgv_Penyelesaian_Tambah = Dgv_DetailBarang.Rows(Index).Cells(Cell_QtyPenyelesaian_Tambah).Value
        Dgv_PenyelesaianKurang = Dgv_DetailBarang.Rows(Index).Cells(Cell_QtyPenyelesaian_Kurang).Value
        Dgv_Keterangan = Dgv_DetailBarang.Rows(Index).Cells(Cell_Keterangan).Value
        Dgv_Selisih_Rp = Dgv_DetailBarang.Rows(Index).Cells(Cell_Selisih_RP).Value
        Dgv_Selisih_Fix = Dgv_DetailBarang.Rows(Index).Cells(Cell_Selisih_Fix).Value
        Dgv_HargaPerPcs = Dgv_DetailBarang.Rows(Index).Cells(Cell_HargaPerPcs).Value
        Dgv_TglProduksi = Dgv_DetailBarang.Rows(Index).Cells(Cell_TglProduksi).Value
        Dgv_TglExpired = Dgv_DetailBarang.Rows(Index).Cells(Cell_TglExpired).Value
        Dgv_SN = Dgv_DetailBarang.Rows(Index).Cells(Cell_SN).Value
        Dgv_JmlhPlHitung = Dgv_DetailBarang.Rows(Index).Cells(Cell_jmlhPlHitung).Value
        Dgv_JmlhBMHitung = Dgv_DetailBarang.Rows(Index).Cells(Cell_jmlhBMHitung).Value
        Dgv_HargaHitung = Dgv_DetailBarang.Rows(Index).Cells(Cell_hargaHitung).Value

    End Sub

    Private Sub Emi_Selisih_Barang_Masuk_SD_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'My.Application.ChangeCulture("en-us")
        'My.Application.ChangeUICulture("en-us")

        kosong()
        Load_data()

    End Sub

    Private Sub Emi_Selisih_Barang_Masuk2_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub BtnSelisihBrgMsk_Refresh_Click(sender As Object, e As EventArgs) Handles BtnSelisihBrgMsk_Refresh.Click
        kosong()
        Load_data()
    End Sub

    Private Sub kosong()
        Txt_FakturPO.Text = P_NoFakturPO

        Txt_Supplier.Text = String.Empty
        Txt_NoSJ.Text = String.Empty
        Txt_Plat.Text = String.Empty
        Txt_Driver.Text = String.Empty
        Txt_TglBerangkat.Text = String.Empty
        Txt_TglMasuk.Text = String.Empty
        Txt_JamMasuk.Text = String.Empty
        Txt_TotSelisihQTY.Text = String.Empty
        Txt_TotSelisihRP.Text = String.Empty

        Cmb_JenisSelisih.Items.Clear()
        Cmb_JenisSelisih.Items.Add("Internal")
        Cmb_JenisSelisih.Items.Add("Pabrik")

        Dgv_DetailBarang.Rows.Clear()

        Txt_Supplier.Text = P_Supplier
        Txt_NoSJ.Text = P_NoSJ
        Txt_Plat.Text = P_NoPlat
        Txt_Driver.Text = P_Driver
        Txt_TglBerangkat.Text = P_TglBerangkat
        Txt_TglMasuk.Text = P_TglMasuk
        Txt_JamMasuk.Text = P_JamMasuk

        Try
            OpenConn()

            Cmb_LokasiPO.Items.Clear() : arrInisialFaktur.Clear()
            SQL = "select Kode_Stock_Owner, persediaan ,inisial_faktur from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and aktif = 'Y' and kode_stock_owner = '" & General_Module.Lokasi & "' order by Kode_Stock_Owner"

            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_LokasiPO.Items.Add(dr("Kode_Stock_Owner")) : arrInisialFaktur.Add(dr("inisial_faktur"))
                Loop
            End Using

            Cmb_LokasiPO.Text = General_Module.Lokasi

            get_no_faktur()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



    End Sub

    Private Sub get_no_faktur()
        Txt_NoFaktur.Text = fsb & arrInisialFaktur.Item(Cmb_LokasiPO.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy") & "-" &
                                     General_Class.Get_Last_Number2("emi_pembelian_selisih_barang_masuk", "no_faktur", Jumlah_Digit,
                                     "Kode_perusahaan", KodePerusahaan,
                                     "And", "substring(no_faktur,1," & Len(fsb) + Len(arrInisialFaktur.Item(Cmb_LokasiPO.SelectedIndex)) + 6 & ")", fsb & arrInisialFaktur.Item(Cmb_LokasiPO.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))
    End Sub

    Private Sub Load_data()
        If P_NoFakturPO.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            SQL = "select b.Kode_Stock_Owner, b.Kode_Barang, c.Nama, b.Jumlah_Barang as Jmlh_Pl_Hitung, b.Jumlah as Jmlh_Pl ,b.Jumlah_Masuk as jmlh_BM, b.Satuan, b.satuan_barang, d.Harga_Barang, d.Harga,"
            SQL = SQL & "ISNULL((b.Jumlah_Barang *  d.Harga_Barang), 0) as Harga_Pl, "
            SQL = SQL & "ISNULL((b.Jumlah_Masuk *  d.Harga_Barang), 0) as Harga_BM "
            SQL = SQL & "from EMI_Pembelian_Loading a, EMI_Pembelian_Loading_Detail b, Barang c, EMI_Pembelian_PO_Detail d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and b.Urut_PO = d.No_Urut "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' and a.No_Faktur='" & Txt_FakturPO.Text & "' "
            SQL = SQL & "and a.No_SJ='" & P_NoSJ & "' and a.No_Plat='" & P_NoPlat & "' "
            Using DS = BindingTrans(SQL)
                With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim totalSelisih As Double = 0
                        Dim totalSelisihRP As Double = 0

                        For i As Integer = 0 To .Rows.Count - 1

                            Dim selisihRP As Double = 0
                            Dim selisih As Double = 0
                            Dim jmlhBm_Display As Double = Ubah_Satuan(.Rows(i).Item("Kode_Barang"), .Rows(i).Item("jmlh_BM"), .Rows(i).Item("satuan_barang"), .Rows(i).Item("Satuan"))

                            selisih = Val(HilangkanTanda(jmlhBm_Display)) - Val(HilangkanTanda(.Rows(i).Item("Jmlh_Pl")))
                            selisihRP = Val(HilangkanTanda(.Rows(i).Item("Harga_BM"))) - Val(HilangkanTanda(.Rows(i).Item("Harga_Pl")))

                            totalSelisih = totalSelisih + selisih
                            totalSelisihRP = totalSelisihRP + selisihRP

                            Txt_TotSelisihQTY.Text = Format(totalSelisih, "N0")
                            Txt_TotSelisihRP.Text = Format(totalSelisihRP, "N0")

                            Dgv_DetailBarang.Rows.Add(1)
                            Dgv_DetailBarang.Rows(i).Cells(Cell_Lokasi).Value = .Rows(i).Item("Kode_Stock_Owner")
                            Dgv_DetailBarang.Rows(i).Cells(Cell_KdBarang).Value = .Rows(i).Item("Kode_Barang")
                            Dgv_DetailBarang.Rows(i).Cells(Cell_Nama).Value = .Rows(i).Item("Nama")

                            Dgv_DetailBarang.Rows(i).Cells(Cell_JmlhPL).Value = Format(.Rows(i).Item("Jmlh_Pl"), "N0")

                            'Dgv_DetailBarang.Rows(i).Cells(Cell_JmlhBM).Value = Format(.Rows(i).Item("jmlh_BM"), "N0")

                            Dgv_DetailBarang.Rows(i).Cells(Cell_JmlhBM).Value = Format(jmlhBm_Display, "N0")

                            'Jumlah Untuk Hitung
                            Dgv_DetailBarang.Rows(i).Cells(Cell_jmlhPlHitung).Value = Format(.Rows(i).Item("Jmlh_Pl_Hitung"), "N0")
                            Dgv_DetailBarang.Rows(i).Cells(Cell_jmlhBMHitung).Value = Format(.Rows(i).Item("jmlh_BM"), "N0")

                            Dgv_DetailBarang.Rows(i).Cells(Cell_Selisih).Value = Format(selisih, "N0")
                            Dgv_DetailBarang.Rows(i).Cells(Cell_Selisih_RP).Value = Format(selisihRP, "N0")
                            Dgv_DetailBarang.Rows(i).Cells(Cell_Selisih_Fix).Value = 0

                            Dgv_DetailBarang.Rows(i).Cells(Cell_HargaPerPcs).Value = Format(.Rows(i).Item("Harga"), "N0")
                            Dgv_DetailBarang.Rows(i).Cells(Cell_hargaHitung).Value = Format(.Rows(i).Item("Harga_Barang"), "N0")

                            Dgv_DetailBarang.Rows(i).Cells(Cell_TglProduksi).Value = ""
                            Dgv_DetailBarang.Rows(i).Cells(Cell_TglExpired).Value = ""
                            Dgv_DetailBarang.Rows(i).Cells(Cell_SN).Value = ""

                            'WARNAI KOLOM
                            Dgv_DetailBarang.Rows(i).Cells(Cell_QtyPenyelesaian_Tambah).Style.BackColor = Color.FromArgb(235, 235, 235)
                            Dgv_DetailBarang.Rows(i).Cells(Cell_QtyPenyelesaian_Kurang).Style.BackColor = Color.FromArgb(235, 235, 235)
                            Dgv_DetailBarang.Rows(i).Cells(Cell_Keterangan).Style.BackColor = Color.FromArgb(235, 235, 235)


                            ' menambahkan satuan yang disesuaikan dengan barang 
                            Dim dgvcc As DataGridViewComboBoxCell
                            dgvcc = Dgv_DetailBarang.Rows(i).Cells(Cell_Satuan)
                            dgvcc.Items.Clear()

                            SQL = "select satuan from barang_detail_Satuan where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Barang ='" & .Rows(i).Item("Kode_Barang") & "'"
                            SQL = SQL & "and flag_tampil_display = 'Y'"
                            Using dr = OpenTrans(SQL)
                                Do While dr.Read
                                    dgvcc.Items.Add(dr("satuan"))
                                Loop

                                dgvcc.Value = .Rows(i).Item("Satuan")
                            End Using


                        Next

                    Else
                        CloseConn()
                        MessageBox.Show("Barang masuk tidak ditemukan", Judul, MessageBoxButtons.OK)
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


    'FUNCTION HANDLE
    Private Sub Dgv_DetailBarang_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_DetailBarang.CellEndEdit
        If Dgv_DetailBarang.Rows.Count = 0 Then Exit Sub

        Dim Cell_QtyTambah As String = Dgv_DetailBarang.CurrentRow.Cells(Cell_QtyPenyelesaian_Tambah).Value
        Dim Cell_QtyKurang As String = Dgv_DetailBarang.CurrentRow.Cells(Cell_QtyPenyelesaian_Kurang).Value
        Dim Cell_SelisiFix As String = Dgv_DetailBarang.CurrentRow.Cells(Cell_Selisih_Fix).Value
        Dim cell_HargaPcs As String = Dgv_DetailBarang.CurrentRow.Cells(Cell_HargaPerPcs).Value

        Cell_SelisiFix = "0"

        'CEK SELISIH
        Dim jmlhPL As Double = Val(HilangkanTanda(Dgv_DetailBarang.Rows(e.RowIndex).Cells(Cell_JmlhPL).Value))
        Dim JmlhBM As Double = Val(HilangkanTanda(Dgv_DetailBarang.Rows(e.RowIndex).Cells(Cell_JmlhBM).Value.ToString()))
        Dim selisih As Double = JmlhBM - jmlhPL

        If Not IsNumeric(Dgv_DetailBarang.CurrentRow.Cells(Cell_QtyPenyelesaian_Tambah).Value) And Not String.IsNullOrWhiteSpace(Dgv_DetailBarang.CurrentRow.Cells(Cell_QtyPenyelesaian_Tambah).Value) Then
            Dgv_DetailBarang.CurrentRow.Cells(Cell_QtyPenyelesaian_Tambah).Value = ""

            Dim hasilSelisih As Double = selisih + Val(Cell_QtyTambah)
            Dgv_DetailBarang.CurrentRow.Cells(Cell_Selisih_Fix).Value = Format(hasilSelisih, "N0")
            Dgv_DetailBarang.CurrentRow.Cells(Cell_Selisih_RP).Value = Format((hasilSelisih * Val(HilangkanTanda(cell_HargaPcs))), "N0")
            Jumlah_selisih()

            Exit Sub
        ElseIf Not IsNumeric(Dgv_DetailBarang.CurrentRow.Cells(Cell_QtyPenyelesaian_Kurang).Value) And Not String.IsNullOrWhiteSpace(Dgv_DetailBarang.CurrentRow.Cells(Cell_QtyPenyelesaian_Kurang).Value) Then
            Dgv_DetailBarang.CurrentRow.Cells(Cell_QtyPenyelesaian_Kurang).Value = ""

            Dim hasilSelisih As Double = selisih + Val(Cell_QtyTambah)
            Dgv_DetailBarang.CurrentRow.Cells(Cell_Selisih_Fix).Value = Format(hasilSelisih, "N0")
            Dgv_DetailBarang.CurrentRow.Cells(Cell_Selisih_RP).Value = Format((hasilSelisih * Val(HilangkanTanda(cell_HargaPcs))), "N0")
            Jumlah_selisih()

            Exit Sub
        End If


        If e.ColumnIndex = Cell_QtyPenyelesaian_Tambah Then
            Dgv_DetailBarang.CurrentRow.Cells(Cell_Keterangan).Value = ""
            If Not String.IsNullOrWhiteSpace(Dgv_DetailBarang.Rows(e.RowIndex).Cells(Cell_QtyPenyelesaian_Tambah).Value) Then
                Dgv_DetailBarang.Rows(e.RowIndex).Cells(Cell_QtyPenyelesaian_Kurang).Value = ""
            End If

            Dim hasilSelisih As Double = selisih + Val(Cell_QtyTambah)
            Dgv_DetailBarang.CurrentRow.Cells(Cell_Selisih_Fix).Value = Format(hasilSelisih, "N0")
            Dgv_DetailBarang.CurrentRow.Cells(Cell_Selisih_RP).Value = Format((hasilSelisih * Val(HilangkanTanda(cell_HargaPcs))), "N0")

        ElseIf e.ColumnIndex = Cell_QtyPenyelesaian_Kurang Then
            Dgv_DetailBarang.CurrentRow.Cells(Cell_Keterangan).Value = ""
            If Not String.IsNullOrWhiteSpace(Dgv_DetailBarang.Rows(e.RowIndex).Cells(Cell_QtyPenyelesaian_Kurang).Value) Then
                Dgv_DetailBarang.Rows(e.RowIndex).Cells(Cell_QtyPenyelesaian_Tambah).Value = ""
            End If

            Dim hasilSelisih As Double = selisih - Val(Cell_QtyKurang)
            Dgv_DetailBarang.CurrentRow.Cells(Cell_Selisih_Fix).Value = Format(hasilSelisih, "N0")
            Dgv_DetailBarang.CurrentRow.Cells(Cell_Selisih_RP).Value = Format((hasilSelisih * Val(HilangkanTanda(cell_HargaPcs))), "N0")
        End If

        Jumlah_selisih()

    End Sub

    Private Sub Dgv_DetailBarang_DoubleClick(sender As Object, e As EventArgs) Handles Dgv_DetailBarang.DoubleClick
        Dim currentRow = Dgv_DetailBarang.CurrentRow.Index

        Get_DGV_Data(currentRow)

        Dim CekColumn As Boolean = False

        If Dgv_Selisih = "0" AndAlso Dgv_Penyelesaian_Tambah = "" AndAlso Dgv_PenyelesaianKurang = "" Then
            MessageBox.Show("Tidak Perlu Input Posisi Rak untuk data tidak selisih !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dgv_DetailBarang.CurrentRow.Cells(Cell_Keterangan).Value = ""
            Exit Sub
        End If

        If Dgv_Penyelesaian_Tambah = "" And Dgv_PenyelesaianKurang = "" Or Dgv_Keterangan = "" Then
            MessageBox.Show("Terdapat Data Belum Di Input !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If



        'If Dgv_Penyelesaian_Tambah > 0 Then

        '    EMI_Selisih_Barang_Masuk_SD_SN.Plus = "Y"

        'Else
        '    EMI_Selisih_Barang_Masuk_SD_SN.Plus = "T"
        'End If

        Emi_Selisih_Barang_Masuk_SD_DN2.kosong()

        Emi_Selisih_Barang_Masuk_SD_DN2.Txt_NoPO.Text = Txt_FakturPO.Text
        Emi_Selisih_Barang_Masuk_SD_DN2.Txt_SelisihQty.Text = Dgv_Selisih_Fix
        Emi_Selisih_Barang_Masuk_SD_DN2.Txt_Satuan.Text = Dgv_Satuan
        Emi_Selisih_Barang_Masuk_SD_DN2.Txt_Lokasi.Text = Dgv_Lokasi
        Emi_Selisih_Barang_Masuk_SD_DN2.Txt_KdBarang.Text = Dgv_KdBarang
        Emi_Selisih_Barang_Masuk_SD_DN2.Txt_NmBarang.Text = Dgv_Nama
        Emi_Selisih_Barang_Masuk_SD_DN2.Load_Posisi_Rak()


        'If LvQtyPlus > 0 Then
        '    EMI_Selisih_Barang_Masuk_SD_SN.TextBoxQty.Text = LvQtyPlus
        '    EMI_Selisih_Barang_Masuk_SD_SN.Label8.Text = "Plus"
        '    EMI_Selisih_Barang_Masuk_SD_SN.Plus = "Y"

        'Else
        '    EMI_Selisih_Barang_Masuk_SD_SN.TextBoxQty.Text = LvQtyMin
        '    EMI_Selisih_Barang_Masuk_SD_SN.Label8.Text = "Min"
        '    EMI_Selisih_Barang_Masuk_SD_SN.Plus = "T"
        'End If

        Emi_Selisih_Barang_Masuk_SD_DN2.ShowDialog()
    End Sub

    Private Sub BtnSelisihBrgMsk_Simpan_Click(sender As Object, e As EventArgs) Handles BtnSelisihBrgMsk_Simpan.Click
        'If txtNoBM.Text.Trim.Length = 0 Then
        '    MessageBox.Show(Base_Language.Lang_Selisih_BM_Err_Brng_Msk, Judul, MessageBoxButtons.OK)
        '    Exit Sub
        'Else
        If Txt_FakturPO.Text.Trim.Length = 0 Then
            MessageBox.Show("No Faktur Tidak Boleh Kosong", Judul, MessageBoxButtons.OK)
            Exit Sub
        ElseIf Txt_Supplier.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Supplier Tidak Boleh Kosong", Judul, MessageBoxButtons.OK)
            Exit Sub
        ElseIf Cmb_JenisSelisih.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Selisih Harus Dipilih", Judul, MessageBoxButtons.OK)
            Exit Sub
        End If

        get_jam()

        Try

            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction


            '===================================================
            '=     CEK APAKAH ADA DATA YANG BELUM DI INPUT     =
            '===================================================

            For i As Integer = 0 To Dgv_DetailBarang.Rows.Count - 1
                Get_DGV_Data(i)

                If Not Dgv_Penyelesaian_Tambah = "" Or Not Dgv_PenyelesaianKurang = "" Then
                    If Dgv_Keterangan = "" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Keterangan Harus Di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If

            Next


            SQL = "insert into EMI_Pembelian_Selisih_Barang_Masuk(Kode_Perusahaan, No_Faktur, Tanggal, Jam, UserID, Kode_Supplier, Total_Selisih, Total_Harga_Selisih, No_Faktur_BM) values( "
            SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "', '" & P_KdSupplier & "', "
            SQL = SQL & "'" & HilangkanTanda(Txt_TotSelisihQTY.Text) & "', '" & HilangkanTanda(Txt_TotSelisihRP.Text) & "', '" & Txt_FakturPO.Text & "')"
            ExecuteTrans(SQL)


            For i As Integer = 0 To Dgv_DetailBarang.Rows.Count - 1
                Get_DGV_Data(i)

                Dim nilaiQtyMin As Double = 0
                Dim nilaiQtyPlus As Double = 0
                If Dgv_PenyelesaianKurang = "" Then
                    nilaiQtyMin = 0
                Else
                    nilaiQtyMin = Dgv_PenyelesaianKurang
                End If

                If Dgv_Penyelesaian_Tambah = "" Then
                    nilaiQtyPlus = 0
                Else
                    nilaiQtyPlus = Dgv_Penyelesaian_Tambah
                End If

                Dim sn = "NULL"
                Dim TglProduksi = "NULL"
                Dim TglExpired = "NULL"

                If Not Dgv_Penyelesaian_Tambah = "" Or Not Dgv_PenyelesaianKurang = "" Then

                    If Dgv_SN = "" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Untuk data Selisih, Posisi Rak Harus di isi")
                        Exit Sub
                    End If
                    sn = "'" & Dgv_SN & "'"
                    TglProduksi = "'" & Format(CDate(Dgv_TglProduksi), "yyyy-MM-dd") & "'"
                    TglExpired = "'" & Format(CDate(Dgv_TglExpired), "yyyy-MM-dd") & "'"

                End If


                Dim satuan_Kecil As String = ""

                '============================
                '=     CEK SATUAN KECIL     =
                '============================
                SQL = "select Satuan from barang_detail_Satuan where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_barang='" & Dgv_KdBarang & "' and Flag_Tampil_Display is null"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        satuan_Kecil = Dr("Satuan")
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Satuan Kecil Tidak Ditemukan")
                        Exit Sub
                    End If
                End Using

                Dim Dgv_JmlhPL_Gram As Double = Ubah_Satuan(Dgv_KdBarang, Dgv_JmlhPL, Dgv_Satuan, satuan_Kecil)
                Dim Dgv_JmlhBM_Gram As Double = Ubah_Satuan(Dgv_KdBarang, Dgv_JmlhBM, Dgv_Satuan, satuan_Kecil)

                SQL = "insert into EMI_Pembelian_Selisih_Barang_Masuk_Det(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Jumlah_PL, Jumlah_BM, Selisih, Qty_PenyelesaianPlus, Qty_PenyelesaianMin, Keterangan, Selisih_Fix, Harga, Selisih_Rp, No_Faktur_Barang_Masuk, Serial_Number, Tgl_Produksi, Tgl_Expired, Satuan)values("
                SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "', '" & Dgv_Lokasi & "', '" & Dgv_KdBarang & "', '" & HilangkanTanda(Dgv_JmlhPL_Gram) & "', '" & HilangkanTanda(Dgv_JmlhBM_Gram) & "', "
                SQL = SQL & " " & HilangkanTanda(Dgv_Selisih) & ", " & nilaiQtyPlus & ", " & nilaiQtyMin & ",'" & Dgv_Keterangan & "', '" & HilangkanTanda(Dgv_Selisih_Fix) & "', " & HilangkanTanda(Dgv_HargaHitung) & ", " & HilangkanTanda(Dgv_Selisih_Rp) & ", "
                SQL = SQL & "'" & Txt_FakturPO.Text & "', " & sn & ", " & TglProduksi & ", " & TglExpired & ", '" & satuan_Kecil & "')"
                ExecuteTrans(SQL)


                'SQL = "select a.No_PO, a.Kode_Barang, a.Kode_Stock_Owner "
                'SQL = SQL & "from EMI_BM_VS_PO a "
                'SQL = SQL & "where "
                'SQL = SQL & "a.NO_PO = '" & txtNoPO.Text & "' "
                'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "'and a.Kode_Barang = '" & LvKdBarang & "' and a.Kode_Stock_Owner = '" & LvLokasi & "'"
                'Using Dr = OpenTrans(SQL)
                '    If Dr.Read Then

                '        SQL = "Update EMI_BM_VS_PO set Flag_Proses = 'Y' "
                '        SQL = SQL & "where No_PO = '" & Dr("No_PO") & "' and Kode_Perusahaan = '" & KodePerusahaan & "'and "
                '        SQL = SQL & "Kode_Barang = '" & Dr("Kode_Barang") & "' and Kode_Stock_Owner = '" & Dr("Kode_Stock_Owner") & "' "
                '        Dr.Close()
                '        ExecuteTrans(SQL)
                '    Else
                '        Dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("barang " & LvNama & " Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '        Exit Sub
                '    End If
                'End Using

            Next

            SQL = "Update EMI_Pembelian_Loading set Flag_Selisih_BM = 'Y' "
            SQL = SQL & "where No_Faktur = '" & Txt_FakturPO.Text & "' and KOde_Perusahaan='" & KodePerusahaan & "' "
            ExecuteTrans(SQL)

            'MessageBox.Show("berhasil update")
            'CloseTrans()
            'CloseConn()
            'Exit Sub


            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Berhasil Di Simpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kosong()

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Me.Close()
    End Sub


    Public Sub Jumlah_selisih()
        Dim total As Double = 0
        Dim totalHarga As Double = 0
        For index As Integer = 0 To Dgv_DetailBarang.Rows.Count - 1
            Get_DGV_Data(index)

            Dim selisih As Double = Val(HilangkanTanda(Dgv_Selisih_Fix))
            Dim selisihRP As Double = Val(HilangkanTanda(Dgv_Selisih_Rp))
            total = total + selisih
            totalHarga = totalHarga + selisihRP
        Next
        Txt_TotSelisihQTY.Text = Format(total, "N0")
        Txt_TotSelisihRP.Text = Format(totalHarga, "N0")
    End Sub

    Private Function Ubah_Satuan(ByVal kdBarang As String, ByVal jmlhUbah As String, ByVal satuanAwal As String, ByVal satuanAkhir As String) As Double

        Dim Result As Double = 0

        SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & kdBarang & "', '" & satuanAwal & "',"
        SQL = SQL & "'" & satuanAkhir & "', '" & HilangkanTanda(jmlhUbah) & "' ) as hasil"
        Using Dr1 = OpenTrans(SQL)
            If Dr1.Read Then
                If General_Class.CekNULL(Dr1("hasil")) = "" Then
                    Dr1.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("data konversi satuan kirim tidak ada ")
                    Return Nothing
                End If

                Result = Dr1("hasil")
            Else
                Dr1.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("data konversi satuan kirim tidak ada ")
                Return Nothing
            End If
        End Using


        Return Result

    End Function


End Class