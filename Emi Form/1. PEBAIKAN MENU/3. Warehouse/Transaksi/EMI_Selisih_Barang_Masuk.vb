Public Class EMI_Selisih_Barang_Masuk

    Dim faktur As String = ""
    Dim arrInisialFaktur As New ArrayList


    Dim LvLokasi As String
    Dim LvKdBarang As String
    Dim LvNama As String
    Dim LvJumlahPL As String
    Dim LvJumlahBM As String
    Dim LvSatuan As String
    Dim LvSelisih As String
    Dim LvQtyPlus As String
    Dim LvQtyMin As String
    Dim LvKet As String
    Dim LvSelisihFix As String
    Dim LvHarga As String
    Dim LvSelisihRp As String
    Dim LvFakturBM As String
    Dim LvProduksi As String
    Dim LvExpired As String
    Dim LvSN As String


    Dim cellLokasi As Integer = 0
    Dim cellKdBarang As Integer = 1
    Dim cellNama As Integer = 2
    Dim cellJumlahPL As Integer = 3
    Dim cellJumlahBM As Integer = 4
    Dim cellSatuan As Integer = 5
    Dim cellSelisih As Integer = 6
    Dim cellQtyPlus As Integer = 7
    Dim cellQtyMin As Integer = 8
    Dim cellKet As Integer = 9
    Dim cellSelisihFix As Integer = 10
    Dim cellSelisihRp As Integer = 12
    Dim cellHarga As Integer = 11

    Dim CellFakturBM As Integer = 13
    Dim CellProduksi As Integer = 14
    Dim CellExpired As Integer = 15
    Dim CellSN As Integer = 16

    Dim id_rencana_group As String = ""
    Public No_BM As String = ""
    Dim hitung As Long = 0
    Dim _filter_tambahan As String = "and isnull((select X.Id_rencana_induk from rencana_order_gabungan X where ro.Flag_Gabungan = 'Y' and ro.ID_Rencana = X.ID_Rencana ),ro.id_rencana) = ro.id_rencana " 'and


    Private Sub Display_selisih_Barang_Masuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        get_data()

    End Sub

    Private Sub get_no_faktur()
        TxtSelisihBrgMsk_NoFaktur.Text = fSB & arrInisialFaktur.Item(CmbSelisihBrgMsk_Lokasi.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy") & "-" &
                                     General_Class.Get_Last_Number2("emi_pembelian_selisih_barang_masuk", "no_faktur", Jumlah_Digit,
                                     "Kode_perusahaan", KodePerusahaan,
                                     "And", "substring(no_faktur,1," & Len(fSB) + Len(arrInisialFaktur.Item(CmbSelisihBrgMsk_Lokasi.SelectedIndex)) + 6 & ")", fSB & arrInisialFaktur.Item(CmbSelisihBrgMsk_Lokasi.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))
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

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)

        LvLokasi = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellLokasi).Value
        LvKdBarang = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellKdBarang).Value
        LvNama = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellNama).Value
        LvJumlahPL = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellJumlahPL).Value
        LvJumlahBM = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellJumlahBM).Value
        LvSatuan = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellSatuan).Value
        LvSelisih = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellSelisih).Value
        LvQtyPlus = CekNothing(DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellQtyPlus).Value)
        LvQtyMin = CekNothing(DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellQtyMin).Value)
        LvKet = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellKet).Value
        LvSelisihFix = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellSelisihFix).Value
        LvSelisihRp = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellSelisihRp).Value
        LvProduksi = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(CellProduksi).Value
        LvExpired = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(CellExpired).Value
        LvSN = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(CellSN).Value
        LvHarga = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellHarga).Value

    End Sub
    Public Sub get_data()
        Try
            OpenConn()

            DgvSelisihBrgMsk_DataSelisih.Rows.Clear()

            SQL = "select a.kode_stock_owner, a.Kode_Barang,b.Nama,b.Satuan,a.Jumlah as jumlah_pl,a.Jumlah_Masuk, "
            SQL = SQL & "a.jumlah_masuk - a.jumlah as selisih, a.Harga "
            SQL = SQL & "from EMI_BM_VS_PO a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang  "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_PO = '" & txtNoPO.Text & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim totalSelisih As Double = 0
                        Dim totalSelisihRP As Double = 0

                        For i As Integer = 0 To .Rows.Count - 1

                            Dim selisihRP As Double = 0

                            selisihRP = Val(.Rows(i).Item("harga")) * Val(.Rows(i).Item("selisih"))
                            totalSelisihRP = totalSelisihRP + selisihRP
                            totalSelisih = totalSelisih + Val(.Rows(i).Item("selisih"))

                            TxtSelisihBrgMsk_TotalQty.Text = Format(totalSelisih, "N2")
                            TxtSelisihBrgMsk_TotalHarga.Text = Format(totalSelisihRP, "N2")


                            DgvSelisihBrgMsk_DataSelisih.Rows.Add(1)
                            DgvSelisihBrgMsk_DataSelisih.Rows(i).Cells(cellLokasi).Value = .Rows(i).Item("kode_stock_owner")
                            DgvSelisihBrgMsk_DataSelisih.Rows(i).Cells(cellKdBarang).Value = .Rows(i).Item("kode_barang")
                            DgvSelisihBrgMsk_DataSelisih.Rows(i).Cells(cellNama).Value = .Rows(i).Item("nama")
                            DgvSelisihBrgMsk_DataSelisih.Rows(i).Cells(cellJumlahPL).Value = Format(.Rows(i).Item("jumlah_pl"), "N0")
                            DgvSelisihBrgMsk_DataSelisih.Rows(i).Cells(cellJumlahBM).Value = Format(.Rows(i).Item("jumlah_masuk"), "N0")
                            DgvSelisihBrgMsk_DataSelisih.Rows(i).Cells(cellSelisih).Value = Format(.Rows(i).Item("selisih"), "N0")
                            DgvSelisihBrgMsk_DataSelisih.Rows(i).Cells(cellSelisihFix).Value = 0
                            DgvSelisihBrgMsk_DataSelisih.Rows(i).Cells(cellHarga).Value = Format(.Rows(i).Item("harga"), "N0")
                            DgvSelisihBrgMsk_DataSelisih.Rows(i).Cells(cellSelisihRp).Value = Format(selisihRP, "N0")
                            DgvSelisihBrgMsk_DataSelisih.Rows(i).Cells(CellProduksi).Value = ""
                            DgvSelisihBrgMsk_DataSelisih.Rows(i).Cells(CellExpired).Value = ""
                            DgvSelisihBrgMsk_DataSelisih.Rows(i).Cells(CellSN).Value = ""


                            ' menambahkan satuan yang disesuaikan dengan barang 
                            Dim dgvcc As DataGridViewComboBoxCell
                            dgvcc = DgvSelisihBrgMsk_DataSelisih.Rows(i).Cells(cellSatuan)
                            dgvcc.Items.Clear()

                            SQL = "select satuan from barang_detail_Satuan where kode_perusahaan = '" & KodePerusahaan & "' and  Kode_Barang ='" & .Rows(i).Item("kode_barang") & "'"
                            Using dr = OpenTrans(SQL)
                                Do While dr.Read
                                    dgvcc.Items.Add(dr("satuan"))
                                Loop
                            End Using


                            SQL = "select  satuan from barang where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & .Rows(i).Item("kode_stock_owner") & "' "
                            SQL = SQL & "and kode_barang = '" & .Rows(i).Item("kode_barang") & "'"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    DgvSelisihBrgMsk_DataSelisih.Rows(i).Cells(cellSatuan).Value = Dr("satuan")
                                Else
                                    Dr.Close()
                                    MessageBox.Show("Kode barang tidak tersedia", Judul, MessageBoxButtons.OK)
                                    Exit Sub
                                End If
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
    Public Sub kosong()
        get_jam()
        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Pembelian_Barang_Masuk")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Selisih_BM")
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        CmbSelisihBrgMsk_Jenis.Items.Clear()
        CmbSelisihBrgMsk_Jenis.Items.Add("Internal")
        CmbSelisihBrgMsk_Jenis.Items.Add("Pabrik")

        Try
            OpenConn()

            SQL = "select Kode_Stock_Owner, persediaan ,inisial_faktur from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and aktif = 'Y'  and kode_stock_owner = '" & Lokasi & "' order by Kode_Stock_Owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSelisihBrgMsk_Lokasi.Items.Add(dr("Kode_Stock_Owner")) : arrInisialFaktur.Add(dr("inisial_faktur"))
                Loop
            End Using


            CmbSelisihBrgMsk_Lokasi.Text = Lokasi

            get_no_faktur()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Label1.Text = Base_Language.Lang_Selisih_BM_Judul

        lblNoBm.Text = Base_Language.Lang_Global_NO_BM
        lblNoPo.Text = Base_Language.Lang_Global_No_PO
        lblTglPO.Text = Base_Language.Lang_Global_Tanggal_PO
        lblSupplier.Text = Base_Language.Lang_Global_Supplier
        lblJenisSelisih.Text = Base_Language.Lang_Selisih_BM_Jenis_Selisih
        LblSelisihBrgMsk_TotalHarga.Text = Base_Language.Lang_Selisih_BM_Total_Selisih_Rp
        LblSelisihBrgMsk_TotalQty.Text = Base_Language.Lang_Selisih_BM_Total_Selisih_QTY

        BtnSelisihBrgMsk_Refresh.Text = Base_Language.Lang_Global_Refresh
        BtnSelisihBrgMsk_Simpan.Text = Base_Language.Lang_Global_Simpan

        DgvSelisihBrgMsk_DataSelisih.Columns(cellKdBarang).HeaderText = Base_Language.Lang_Global_KodeBarang
        DgvSelisihBrgMsk_DataSelisih.Columns(cellNama).HeaderText = Base_Language.Lang_Global_NamaBarang
        DgvSelisihBrgMsk_DataSelisih.Columns(cellJumlahPL).HeaderText = Base_Language.Lang_Selisih_BM_Jumlah_PL
        DgvSelisihBrgMsk_DataSelisih.Columns(cellJumlahBM).HeaderText = Base_Language.Lang_Selisih_BM_Jumlah_BM
        DgvSelisihBrgMsk_DataSelisih.Columns(cellSatuan).HeaderText = Base_Language.Lang_Global_Satuan
        DgvSelisihBrgMsk_DataSelisih.Columns(cellSelisih).HeaderText = Base_Language.Lang_Selisih_BM_Selisih
        DgvSelisihBrgMsk_DataSelisih.Columns(cellQtyPlus).HeaderText = Base_Language.Lang_Selisih_BM_Penyelesaian_Plus
        DgvSelisihBrgMsk_DataSelisih.Columns(cellQtyMin).HeaderText = Base_Language.Lang_Selisih_BM_Penyelesaian_Min
        DgvSelisihBrgMsk_DataSelisih.Columns(cellKet).HeaderText = Base_Language.lang_global_keterangan
        DgvSelisihBrgMsk_DataSelisih.Columns(cellSelisihFix).HeaderText = Base_Language.Lang_Selisih_BM_Selisih_Fix
        DgvSelisihBrgMsk_DataSelisih.Columns(cellSelisihRp).HeaderText = Base_Language.Lang_Selisih_BM_Selisih_Rp
        DgvSelisihBrgMsk_DataSelisih.Columns(cellHarga).HeaderText = Base_Language.Lang_Selisih_BM_Harga_Per_PCS


        BtnSelisihBrgMsk_Cari.Text = Base_Language.Lang_Global_Cari


        txtNoBM.Clear()
        DgvSelisihBrgMsk_DataSelisih.Rows.Clear()
        txtNoPO.Text = ""
        dtpTanggalPO.Value = tgl_skg
        txtKodeSupplier.Text = ""
        TxtSelisihBrgMsk_TotalHarga.Text = 0
        TxtSelisihBrgMsk_TotalQty.Text = 0
        CmbSelisihBrgMsk_Jenis.SelectedIndex = -1
    End Sub
    'Private Function CekNothing(ByVal str As String) As String
    '    Dim hasil As String = ""

    '    If str Is Nothing Then
    '        hasil = ""
    '    Else
    '        hasil = str
    '    End If

    '    Return hasil
    'End Function

    'Public Sub Get_Isi_Listview(ByVal No_Index As Integer)

    '    LvKonte = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellKonte).Value.ToString
    '    LvLokasi = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellLokasi).Value.ToString
    '    LvKdBarang = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellKdBarang).Value.ToString
    '    LvNama = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellNama).Value.ToString
    '    LvJumlahPL = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellJumlahPL).Value.ToString
    '    LvJumlahBM = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellJumlahBM).Value.ToString
    '    LvSelisih = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellSelisih).Value.ToString
    '    LvQtyPlus = CekNothing(DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellQtyPlus).Value)
    '    LvQtyMin = CekNothing(DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellQtyMin).Value)
    '    LvKet = CekNothing(DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellKet).Value)
    '    LvSelisihFix = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellSelisihFix).Value.ToString
    '    LvHarga = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellHarga).Value.ToString
    '    LvSelisihRp = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(cellSelisihRp).Value.ToString
    '    LvFakturBM = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(CellFakturBM).Value.ToString
    '    LvTglExpired = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(CellTglExpired).Value.ToString
    '    LvTglProduksi = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(CellTglProduksi).Value.ToString
    '    LvSN = DgvSelisihBrgMsk_DataSelisih.Rows(No_Index).Cells(CellSN).Value.ToString
    'End Sub


    'Private Sub get_Faktur()
    '    faktur = FSBM & arrInisialFaktur & "-" & Format(Tanggal_Sekarang, "MM/yy") & "-" &
    '                        General_Class.Get_Last_Number2("Selisih_Barang_Masuk", "No_Faktur", JumlahDigit,
    '                        "Kode_perusahaan", KodePerusahaan,
    '                        "And", "substring(No_Faktur,1," & Len(FSBM) + Len(arrInisialFaktur) + 6 & ")",
    '                         FSBM & arrInisialFaktur & "-" & Format(Tanggal_Sekarang, "MM/yy"))

    'End Sub

    'Private Sub Hitung_Total_Billing_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
    '    My.Application.ChangeCulture("en-us")
    '    My.Application.ChangeUICulture("en-us")
    'End Sub

    'Private Sub Rencana_order_Biaya_Import_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    My.Application.ChangeCulture("en-us")
    '    My.Application.ChangeUICulture("en-us")

    '    Kosong()
    'End Sub

    'Public Sub Jumlah_selisih()
    '    Dim total As Double = 0
    '    Dim totalHarga As Double = 0
    '    For index As Integer = 0 To DgvSelisihBrgMsk_DataSelisih.Rows.Count - 1
    '        Get_Isi_Listview(index)

    '        total = total + Val(LvSelisihFix)
    '        totalHarga = totalHarga + Val((LvSelisihRp))
    '    Next
    '    TxtSelisihBrgMsk_TotalQty.Text = Format(total, "N0")
    '    TxtSelisihBrgMsk_TotalHarga.Text = Format(totalHarga, "N0")
    'End Sub

    Private Sub BtnSelisihBrgMsk_Simpan_Click(sender As Object, e As EventArgs) Handles BtnSelisihBrgMsk_Simpan.Click

        'If txtNoBM.Text.Trim.Length = 0 Then
        '    MessageBox.Show(Base_Language.Lang_Selisih_BM_Err_Brng_Msk, Judul, MessageBoxButtons.OK)
        '    Exit Sub
        'Else
        If txtNoPO.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Selisih_BM_Err_No_PO, Judul, MessageBoxButtons.OK)
            Exit Sub
        ElseIf txtKodeSupplier.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Selisih_BM_Err_KdSupp, Judul, MessageBoxButtons.OK)
            Exit Sub
        ElseIf CmbSelisihBrgMsk_Jenis.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Selisih_BM_Err_Jenis)
            Exit Sub
        End If
        get_jam()

        Try

            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction


            For i As Integer = 0 To DgvSelisihBrgMsk_DataSelisih.Rows.Count - 1
                Get_Isi_Listview(i)

                If LvQtyMin = "" Or LvQtyPlus = "" Or LvKet = "" Then
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_Selisih_BM_Err_Data_Kosong, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf LvQtyPlus > 0 And LvQtyMin > 0 Then

                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Penyelesaian Hanya Boleh Satu Kolom Saja!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub

                End If
            Next


            SQL = "insert into EMI_Pembelian_Selisih_Barang_Masuk(Kode_Perusahaan, No_Faktur, Tanggal, Jam, UserID, Kode_Supplier, Total_Selisih, Total_Harga_Selisih, No_Faktur_BM, No_PO) values("
            SQL = SQL & "'" & KodePerusahaan & "', '" & TxtSelisihBrgMsk_NoFaktur.Text & "','" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "','" & UserID & "', '" & txtKodeSupplier.Text & "',"
            SQL = SQL & " " & HilangkanTanda(TxtSelisihBrgMsk_TotalQty.Text) & ", " & HilangkanTanda(TxtSelisihBrgMsk_TotalHarga.Text) & ", '" & txtNoBM.Text & "','" & txtNoPO.Text & "')"
            ExecuteTrans(SQL)


            For i As Integer = 0 To DgvSelisihBrgMsk_DataSelisih.Rows.Count - 1
                Get_Isi_Listview(i)

                Dim nilaiQtyMin As Double = 0
                Dim nilaiQtyPlus As Double = 0
                If LvQtyMin = "" Then
                    nilaiQtyMin = 0
                Else
                    nilaiQtyMin = LvQtyMin
                End If

                If LvQtyPlus = "" Then
                    nilaiQtyPlus = 0
                Else
                    nilaiQtyPlus = LvQtyPlus
                End If

                Dim sn = "NULL"
                Dim TglProduksi = "NULL"
                Dim TglExpired = "NULL"

                If LvQtyPlus > 0 Or LvQtyMin > 0 Then

                    If LvSN = "" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Untuk data Selisih Tgl Exp Harus di isi")
                        Exit Sub
                    End If
                    sn = "'" & LvSN & "'"
                    TglProduksi = "'" & Format(CDate(LvProduksi), "yyyy-MM-dd") & "'"
                    TglExpired = "'" & Format(CDate(LvExpired), "yyyy-MM-dd") & "'"

                End If

                SQL = "insert into EMI_Pembelian_Selisih_Barang_Masuk_Det(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Jumlah_PL, Jumlah_BM, Selisih, Qty_PenyelesaianPlus, Qty_PenyelesaianMin, Keterangan, Selisih_Fix, Harga, Selisih_Rp, No_Faktur_Barang_Masuk, Serial_Number, Tgl_Produksi, Tgl_Expired, Satuan)values("
                SQL = SQL & "'" & KodePerusahaan & "', '" & TxtSelisihBrgMsk_NoFaktur.Text & "', '" & LvLokasi & "', '" & LvKdBarang & "' ,'" & HilangkanTanda(LvJumlahPL) & "','" & HilangkanTanda(LvJumlahBM) & "'"
                SQL = SQL & ",'" & HilangkanTanda(LvSelisih) & "', " & nilaiQtyPlus & ", " & nilaiQtyMin & ",'" & LvKet & "', '" & HilangkanTanda(LvSelisihFix) & "', " & HilangkanTanda(LvHarga) & "," & HilangkanTanda(LvSelisihRp) & ",  "
                SQL = SQL & "'" & txtNoBM.Text & "', " & sn & "," & TglProduksi & ", " & TglExpired & ", '" & LvSatuan & "')"
                ExecuteTrans(SQL)


                SQL = "select a.No_PO, a.Kode_Barang, a.Kode_Stock_Owner "
                SQL = SQL & "from EMI_BM_VS_PO a "
                SQL = SQL & "where "
                SQL = SQL & "a.NO_PO = '" & txtNoPO.Text & "' "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "'and a.Kode_Barang = '" & LvKdBarang & "' and a.Kode_Stock_Owner = '" & LvLokasi & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        SQL = "Update EMI_BM_VS_PO set Flag_Proses = 'Y' "
                        SQL = SQL & "where No_PO = '" & Dr("No_PO") & "' and Kode_Perusahaan = '" & KodePerusahaan & "'and "
                        SQL = SQL & "Kode_Barang = '" & Dr("Kode_Barang") & "' and Kode_Stock_Owner = '" & Dr("Kode_Stock_Owner") & "' "
                        Dr.Close()
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("barang " & LvNama & " Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End Using

            Next

            SQL = "Update EMI_Pembelian_PO set Flag_Selisih_BM = 'Y' "
            SQL = SQL & "where No_Faktur = '" & txtNoPO.Text & "' and KOde_Perusahaan='" & KodePerusahaan & "' "
            ExecuteTrans(SQL)
            'MessageBox.Show("berhasil update")
            'CloseTrans()
            'CloseConn()
            'Exit Sub


            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kosong()

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        EMI_PO_Pembelian_Display2.Cari("Y")
        Me.Close()

    End Sub

    Private Sub BtnSelisihBrgMsk_Refresh_Click(sender As Object, e As EventArgs) Handles BtnSelisihBrgMsk_Refresh.Click
        kosong()
    End Sub

    Private Sub BtnSelisihBrgMsk_Cari_Click(sender As Object, e As EventArgs) Handles BtnSelisihBrgMsk_Cari.Click
        'txtNoPO.Text = "PO-08/23-0011"
        'dtpTanggalPO.Value = "2023-09-13"
        'txtKodeSupplier.Text = "Sup Ikan"

        'Dim dgvcheckbox As DataGridViewComboBoxCell
        'Dim dgvcheckbox1 As DataGridViewComboBoxCell
        'Dim dgvcheckbox2 As DataGridViewComboBoxCell








        'DgvSelisihBrgMsk_DataSelisih.Rows.Add(1)
        'dgvcheckbox = DgvSelisihBrgMsk_DataSelisih.Rows(0).Cells(cellSatuan)
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(0).Cells(cellKonte).Value = "a"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(0).Cells(cellLokasi).Value = "a"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(0).Cells(cellKdBarang).Value = "Xoo1"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(0).Cells(cellNama).Value = "Ori cat"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(0).Cells(cellJumlahPL).Value = "1000"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(0).Cells(cellJumlahBM).Value = "995"
        'dgvcheckbox.Items.Add("Gram")
        'dgvcheckbox.Items.Add("Kg")
        'dgvcheckbox.Items.Add("Ton")
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(0).Cells(cellSelisih).Value = "5"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(0).Cells(cellQtyPlus).Value = ""
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(0).Cells(cellQtyMin).Value = ""
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(0).Cells(cellKet).Value = ""
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(0).Cells(cellSelisihFix).Value = "-5"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(0).Cells(cellHarga).Value = Format(10000, "N0")
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(0).Cells(cellSelisihRp).Value = Format(-5 * 10000, "N0")
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(0).Cells(CellFakturBM).Value = "aaa"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(0).Cells(CellTglProduksi).Value = ""
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(0).Cells(CellTglExpired).Value = ""
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(0).Cells(CellSN).Value = ""

        'DgvSelisihBrgMsk_DataSelisih.Rows.Add(1)
        'dgvcheckbox1 = DgvSelisihBrgMsk_DataSelisih.Rows(1).Cells(cellSatuan)
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(1).Cells(cellKonte).Value = "a"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(1).Cells(cellLokasi).Value = "a"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(1).Cells(cellKdBarang).Value = "Xoo2"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(1).Cells(cellNama).Value = "life cat"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(1).Cells(cellJumlahPL).Value = "1000"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(1).Cells(cellJumlahBM).Value = "1000"
        'dgvcheckbox1.Items.Add("Gram")
        'dgvcheckbox1.Items.Add("Kg")
        'dgvcheckbox1.Items.Add("Ton")
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(1).Cells(cellSelisih).Value = "0"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(1).Cells(cellQtyPlus).Value = ""
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(1).Cells(cellQtyMin).Value = ""
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(1).Cells(cellKet).Value = ""
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(1).Cells(cellSelisihFix).Value = "0"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(1).Cells(cellHarga).Value = Format(20000, "N0")
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(1).Cells(cellSelisihRp).Value = Format(0 * 20000, "N0")
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(1).Cells(CellFakturBM).Value = "aaa"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(1).Cells(CellTglProduksi).Value = ""
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(1).Cells(CellTglExpired).Value = ""
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(1).Cells(CellSN).Value = ""

        'DgvSelisihBrgMsk_DataSelisih.Rows.Add(1)
        'dgvcheckbox2 = DgvSelisihBrgMsk_DataSelisih.Rows(2).Cells(cellSatuan)
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(2).Cells(cellKonte).Value = "a"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(2).Cells(cellLokasi).Value = "a"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(2).Cells(cellKdBarang).Value = "Xoo3"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(2).Cells(cellNama).Value = "bio cat"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(2).Cells(cellJumlahPL).Value = "1000"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(2).Cells(cellJumlahBM).Value = "1002"
        'dgvcheckbox2.Items.Add("Gram")
        'dgvcheckbox2.Items.Add("Kg")
        'dgvcheckbox2.Items.Add("Ton")
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(2).Cells(cellSelisih).Value = "2"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(2).Cells(cellQtyPlus).Value = ""
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(2).Cells(cellQtyMin).Value = ""
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(2).Cells(cellKet).Value = ""
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(2).Cells(cellSelisihFix).Value = "2"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(2).Cells(cellHarga).Value = Format(13000, "N0")
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(2).Cells(cellSelisihRp).Value = Format(2 * 13000, "N0")
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(2).Cells(CellFakturBM).Value = "aaa"
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(2).Cells(CellTglProduksi).Value = ""
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(2).Cells(CellTglExpired).Value = ""
        'DgvSelisihBrgMsk_DataSelisih.Rows.Item(2).Cells(CellSN).Value = ""

        'TxtSelisihBrgMsk_TotalQty.Text = Format(56655, "N0")
        'TxtSelisihBrgMsk_TotalHarga.Text = Format(444554445, "N0")

        SD_Pilih_BM.ShowDialog()
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub
    Public Sub Jumlah_selisih()
        Dim total As Double = 0
        Dim totalHarga As Double = 0
        For index As Integer = 0 To DgvSelisihBrgMsk_DataSelisih.Rows.Count - 1
            Get_Isi_Listview(index)

            total = total + Val(LvSelisihFix)
            totalHarga = totalHarga + Val(HilangkanTanda(LvSelisihRp))
        Next
        TxtSelisihBrgMsk_TotalQty.Text = Format(total, "N0")
        TxtSelisihBrgMsk_TotalHarga.Text = Format(totalHarga, "N0")
    End Sub
    Private Sub DgvSelisihBrgMsk_DataSelisih_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DgvSelisihBrgMsk_DataSelisih.CellEndEdit
        Dim currentRow = DgvSelisihBrgMsk_DataSelisih.CurrentRow.Index
        Dim currentCell = DgvSelisihBrgMsk_DataSelisih.CurrentCellAddress.X

        Get_Isi_Listview(currentRow)

        If IsNumeric(LvQtyPlus) = False Or Val(LvQtyPlus) < 0 Then
            DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(cellQtyPlus).Value = ""
        End If

        If IsNumeric(LvQtyMin) = False Or Val(LvQtyMin) < 0 Then
            DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(cellQtyMin).Value = ""
        End If

        Get_Isi_Listview(currentRow)

        Dim QtyPlus As Double = 0
        Dim QtyMin As Double = 0

        If LvQtyPlus = "" Then
            QtyPlus = 0
        Else
            QtyPlus = Val(HilangkanTanda(LvQtyPlus))
        End If

        If LvQtyMin = "" Then
            QtyMin = 0
        Else
            QtyMin = Val(HilangkanTanda(LvQtyMin))
        End If

        Dim selisih_fix As Double = 0

        selisih_fix = Val(HilangkanTanda(LvSelisih)) + QtyPlus - QtyMin

        DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(cellSelisihFix).Value = selisih_fix
        DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(cellSelisihRp).Value = Format(selisih_fix * Val(HilangkanTanda(LvHarga)), "N0")

        Jumlah_selisih()



        'Dim selisihfix As Double = 0
        'Dim selisihRp As Double = 0

        'If currentCell = 7 And LvQtyPlus <> 0 Then
        '    selisihfix = Val(HilangkanTanda(LvQtyPlus)) + Val(HilangkanTanda(LvJumlahBM)) - Val(HilangkanTanda(LvJumlahPL))
        '    selisihRp = Val(HilangkanTanda(selisihfix)) * Val(HilangkanTanda(LvHarga))
        'ElseIf currentCell = 8 And LvQtyMin <> 0 Then
        '    selisihfix = Val(HilangkanTanda(LvJumlahBM)) - Val(HilangkanTanda(LvQtyMin)) - Val(HilangkanTanda(LvJumlahPL))
        '    selisihRp = Val(HilangkanTanda(selisihfix)) * Val(HilangkanTanda(LvHarga))
        'Else
        '    selisihfix = 0
        '    DgvSelisihBrgMsk_DataSelisih.Rows(currentRow).Cells(cellSelisihFix).Value = 0
        '    DgvSelisihBrgMsk_DataSelisih.Rows(currentRow).Cells(cellSelisihRp).Value = 0

        'End If


        'DgvSelisihBrgMsk_DataSelisih.Rows(currentRow).Cells(cellSelisihFix).Value = Format(selisihfix, "N0")
        'DgvSelisihBrgMsk_DataSelisih.Rows(currentRow).Cells(cellSelisihRp).Value = Format(selisihRp, "N0")


        'Dim totalSeluruhSelisih As Double = 0
        'Dim totalSeluruhSelisihRP As Double = 0
        'For i As Integer = 0 To DgvSelisihBrgMsk_DataSelisih.Rows.Count - 1
        '    Get_Isi_Listview(i)

        '    totalSeluruhSelisih = totalSeluruhSelisih + LvSelisihFix
        '    totalSeluruhSelisihRP = totalSeluruhSelisihRP + LvSelisihRp

        'Next


        'TxtSelisihBrgMsk_TotalQty.Text = Format(totalSeluruhSelisih, "N2")
        'TxtSelisihBrgMsk_TotalHarga.Text = Format(totalSeluruhSelisihRP, "N2")
    End Sub


    Private Sub DgvSelisihBrgMsk_DataSelisih_DoubleClick(sender As Object, e As EventArgs) Handles DgvSelisihBrgMsk_DataSelisih.DoubleClick
        Dim currentRow = DgvSelisihBrgMsk_DataSelisih.CurrentRow.Index
        Get_Isi_Listview(currentRow)

        If LvQtyPlus = "" Or LvQtyMin = "" Then
            MessageBox.Show("Data Belum DI Input !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If LvQtyPlus = "0" And LvQtyMin = "0" Then
            MessageBox.Show("Tidak Perlu Input Tgl Expired untuk data tidak selisih !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If LvQtyPlus > 0 Then

            EMI_Selisih_Barang_Masuk_SD_SN.Plus = "Y"

        Else
            EMI_Selisih_Barang_Masuk_SD_SN.Plus = "T"
        End If

        EMI_Selisih_Barang_Masuk_SD_SN.kosong()

        EMI_Selisih_Barang_Masuk_SD_SN.txtSatuan.Text = LvSatuan
        EMI_Selisih_Barang_Masuk_SD_SN.Harga = HilangkanTanda(LvHarga)
        EMI_Selisih_Barang_Masuk_SD_SN.TextBoxNoKonte.Text = txtNoPO.Text
        EMI_Selisih_Barang_Masuk_SD_SN.TextBoxLokasi.Text = LvLokasi
        EMI_Selisih_Barang_Masuk_SD_SN.TextBoxkdBrg.Text = LvKdBarang
        EMI_Selisih_Barang_Masuk_SD_SN.TextBoxNmBrg.Text = LvNama


        If LvQtyPlus > 0 Then
            EMI_Selisih_Barang_Masuk_SD_SN.TextBoxQty.Text = LvQtyPlus
            EMI_Selisih_Barang_Masuk_SD_SN.Label8.Text = "Plus"
            EMI_Selisih_Barang_Masuk_SD_SN.Plus = "Y"

        Else
            EMI_Selisih_Barang_Masuk_SD_SN.TextBoxQty.Text = LvQtyMin
            EMI_Selisih_Barang_Masuk_SD_SN.Label8.Text = "Min"
            EMI_Selisih_Barang_Masuk_SD_SN.Plus = "T"
        End If

        EMI_Selisih_Barang_Masuk_SD_SN.ShowDialog()
    End Sub






    'Public Sub Kosong()
    '    TxtSelisihBrgMsk_NmContainer.Text = ""
    '    TxtSelisihBrgMsk_NoPo.Text = ""
    '    TxtSelisihBrgMsk_JmlContainer.Text = ""
    '    TxtSelisihBrgMsk_NmSupplier.Text = ""
    '    TextBox1.Text = ""
    '    TxtSelisihBrgMsk_TotalQty.Text = ""
    '    TxtSelisihBrgMsk_TotalHarga.Text = ""


    '    TxtSelisihBrgMsk_RV.Text = ""


    '    GetTime()
    '    DtpSelisihBrgMsk_PO.Value = Tanggal_Sekarang

    '    DgvSelisihBrgMsk_DataSelisih.Rows.Clear()

    '    CmbSelisihBrgMsk_Jenis.Items.Clear()
    '    CmbSelisihBrgMsk_Jenis.Items.Add("INTERNAL")
    '    CmbSelisihBrgMsk_Jenis.Items.Add("PABRIK")
    '    CmbSelisihBrgMsk_Jenis.Items.Add("TOLERANSI GUDANG")
    '    CmbSelisihBrgMsk_Jenis.SelectedIndex = -1

    '    Try
    '        OpenConn()

    '        CmbSelisihBrgMsk_Lokasi.Items.Clear()
    '        Sql = "Select Kode_stock_owner From stock_owner where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_stock_owner"
    '        Using dr = OpenTrans(Sql)
    '            Do While dr.Read
    '                CmbSelisihBrgMsk_Lokasi.Items.Add(dr("kode_stock_owner"))
    '            Loop
    '        End Using

    '        CloseConn()
    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    '    CmbSelisihBrgMsk_Lokasi.Text = Lokasi
    '    'CmbLokasi.Text = "DIST SEMARANG"
    'End Sub

    'Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSelisihBrgMsk_Refresh.Click
    '    Kosong()
    'End Sub

    'Private Sub BtCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSelisihBrgMsk_Cari.Click
    '    'Display_Rencana_Order_Lain_Lain.dari_mana = "SELISIH_BARANG_MASUK"
    '    'Display_Rencana_Order_Lain_Lain.filter_tambahan = _filter_tambahan
    '    'Display_Rencana_Order_Lain_Lain.ShowDialog()


    '    Display_Barang_Masuk_Per_Kontainer.ShowDialog()

    'End Sub

    'Private Sub BtnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSelisihBrgMsk_Simpan.Click
    '    If DgvSelisihBrgMsk_DataSelisih.Rows.Count = 0 Then
    '        MessageBox.Show("Data Harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        DgvSelisihBrgMsk_DataSelisih.Focus()
    '        Exit Sub
    '    ElseIf CmbSelisihBrgMsk_Jenis.SelectedIndex = -1 Then
    '        MessageBox.Show("Jenis Harus Di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        CmbSelisihBrgMsk_Jenis.Focus()
    '        Exit Sub
    '    End If

    '    GetTime()
    '    Try
    '        OpenConn()
    '        Cmd.Transaction() = Cn.BeginTransaction

    '        For i As Integer = 0 To DgvSelisihBrgMsk_DataSelisih.Rows.Count - 1
    '            Get_Isi_Listview(i)

    '            If LvQtyPlus > 0 And LvQtyMin > 0 Then
    '                MessageBox.Show("Penyelesaian Hanya Boleh Satu Kolom Saja!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                CloseTrans()
    '                CloseConn()
    '                Exit Sub
    '            End If

    '            If LvKet = "" Or LvQtyPlus = "" Or LvQtyMin = "" Then
    '                MessageBox.Show("Data Tidak Boleh Kosong!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                CloseTrans()
    '                CloseConn()
    '                Exit Sub
    '            End If

    '            If LvQtyPlus > 0 Or LvQtyMin > 0 Then

    '                If LvTglExpired = "" Or LvTglProduksi = "" Or LvSN = "" Then
    '                    MessageBox.Show("Untuk Data yang Selisih Tanggal Expired Wajib di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    CloseTrans()
    '                    CloseConn()
    '                    Exit Sub
    '                End If

    '                If LvHarga <> Get_Harga_SN(LvSN) Then
    '                    MessageBox.Show("Terjadi Kesalahan, Silahkan Ulangi Transaksi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    CloseTrans()
    '                    CloseConn()
    '                    Exit Sub
    '                End If

    '            End If
    '        Next


    '        Sql = "select count(a.Kode_Perusahaan) as jumlah_data "
    '        Sql = Sql & "from BM_Per_Container_VS_Kontainer_Masuk a, Barang_Masuk b "
    '        Sql = Sql & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur "
    '        Sql = Sql & "and a.No_Container = b.No_Container and id_rencana in(" & id_rencana_group & ") and b.No_Faktur = '" & No_BM & "' "
    '        Sql = Sql & "and a.No_Container = '" & TxtSelisihBrgMsk_NmContainer.Text & "' AND a.Kode_Perusahaan = '" & KodePerusahaan & "'"
    '        Using Dr = OpenTrans(Sql)
    '            If Dr.Read Then
    '                If Dr("Jumlah_Data") <> DgvSelisihBrgMsk_DataSelisih.Rows.Count Then
    '                    Dr.Close()
    '                    CloseTrans()
    '                    CloseConn()
    '                    MessageBox.Show("Jumlah Barang Tidak Sama!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Exit Sub
    '                End If
    '            End If
    '        End Using

    '        'cek faktur, cek akun

    '        Dim akun_plus As String = ""
    '        Dim akun_min As String = ""
    '        Sql = "select inisial_faktur, Biaya_Pabrik_selisih_import_min, Biaya_Pabrik_selisih_import_plus, "
    '        Sql = Sql & "Biaya_Gudang_selisih_import_min, Biaya_Gudang_selisih_import_plus, "
    '        Sql = Sql & "Biaya_selisih_import_min, Biaya_selisih_import_plus "
    '        Sql = Sql & "from stock_owner where Kode_stock_Owner = '" & CmbSelisihBrgMsk_Lokasi.Text & "' AND Kode_Perusahaan = '" & KodePerusahaan & "'"
    '        Using dr = OpenTrans(Sql)
    '            If dr.Read Then
    '                arrInisialFaktur = dr("inisial_faktur")

    '                If CmbSelisihBrgMsk_Jenis.SelectedIndex = 0 Then
    '                    akun_plus = dr("Biaya_selisih_import_plus")
    '                    akun_min = dr("Biaya_selisih_import_min")

    '                ElseIf CmbSelisihBrgMsk_Jenis.SelectedIndex = 1 Then
    '                    akun_plus = dr("Biaya_Pabrik_selisih_import_plus")
    '                    akun_min = dr("Biaya_Pabrik_selisih_import_min")

    '                ElseIf CmbSelisihBrgMsk_Jenis.SelectedIndex = 2 Then
    '                    akun_plus = dr("Biaya_Gudang_selisih_import_plus")
    '                    akun_min = dr("Biaya_Gudang_selisih_import_min")

    '                Else
    '                    dr.Close()
    '                    CloseTrans()
    '                    CloseConn()
    '                    MessageBox.Show("Jenis Selisih Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                    Exit Sub
    '                End If
    '            Else
    '                dr.Close()
    '                CloseTrans()
    '                CloseConn()
    '                MessageBox.Show("Inisial Faktur Tidak ditemukan")
    '                Exit Sub
    '            End If
    '        End Using

    '        get_Faktur()
    '        Sql = "insert into Selisih_Barang_Masuk(kode_perusahaan, no_faktur, Id_rencana, tanggal, jam, UserID, Total_selisih, Total_Harga_Selisih, No_Container, No_Faktur_BM, Jenis_Selisih, Akun_Selisih_Plus, Akun_Selisih_Min, xtermxx) "
    '        Sql = Sql & "values('" & KodePerusahaan & "','" & faktur & "','" & TxtSelisihBrgMsk_NoPo.Text & "', '" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
    '        Sql = Sql & "'" & UserID & "', '" & HilangkanTanda(TxtSelisihBrgMsk_TotalQty.Text) & "', '" & HilangkanTanda(TxtSelisihBrgMsk_TotalHarga.Text) & "', '" & TxtSelisihBrgMsk_NmContainer.Text & "', '" & DgvSelisihBrgMsk_DataSelisih.Rows(0).Cells(CellFakturBM).Value.ToString & "', "
    '        Sql = Sql & "'" & CmbSelisihBrgMsk_Jenis.Text & "','" & akun_plus & "','" & akun_min & "', 'x')"
    '        ExecuteTrans(Sql)

    '        'insert ke detail
    '        For i As Integer = 0 To DgvSelisihBrgMsk_DataSelisih.Rows.Count - 1
    '            Get_Isi_Listview(i)

    '            Dim sn = "NULL"
    '            Dim TglProduksi = "NULL"
    '            Dim TglExpired = "NULL"

    '            If LvQtyPlus > 0 Or LvQtyMin > 0 Then

    '                sn = "'" & LvSN & "'"
    '                TglProduksi = "'" & LvTglProduksi & "'"
    '                TglExpired = "'" & LvTglExpired & "'"

    '            End If

    '            Sql = "insert into detail_Selisih_Barang_Masuk (kode_perusahaan, No_faktur, No_Container, Kode_Stock_Owner, Kode_Barang, Jumlah_PL, "
    '            Sql = Sql & "Jumlah_BM, Selisih, Qty_PenyelesaianPlus, Qty_PenyelesaianMin, Keterangan, Selisih_Fix, Harga, Selisih_Rp, No_Faktur_Barang_Masuk, Tgl_Produksi, Tgl_Expired, Serial_Number) values( "
    '            Sql = Sql & "'" & KodePerusahaan & "', '" & faktur & "', '" & LvKonte & "', '" & LvLokasi & "', '" & LvKdBarang & "', "
    '            Sql = Sql & "'" & LvJumlahPL & "', '" & LvJumlahBM & "', '" & LvSelisih & "', '" & LvQtyPlus & "', "
    '            Sql = Sql & "'" & LvQtyMin & "','" & LvKet & "', '" & LvSelisihFix & "', '" & HilangkanTanda(LvHarga) & "', '" & HilangkanTanda(LvSelisihRp) & "', '" & LvFakturBM & "', " & TglProduksi & ", " & TglExpired & ", " & sn & ")"
    '            ExecuteTrans(Sql)

    '            Sql = "select a.No_Faktur, a.No_Container, a.Kode_Barang, a.Kode_Stock_Owner "
    '            Sql = Sql & "from BM_Per_Container_VS_Kontainer_Masuk a, Barang_Masuk b "
    '            Sql = Sql & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur and Flag_Proses is null "
    '            Sql = Sql & "and a.No_Container = b.No_Container and id_rencana in(" & id_rencana_group & ") and a.No_Faktur = '" & No_BM & "' and a.No_Container = '" & TxtSelisihBrgMsk_NmContainer.Text & "' "
    '            Sql = Sql & "and a.Kode_Perusahaan = '" & KodePerusahaan & "'and a.Kode_Barang = '" & LvKdBarang & "' and a.Kode_Stock_Owner = '" & LvLokasi & "'"
    '            Using Dr = OpenTrans(Sql)
    '                If Dr.Read Then

    '                    Sql = "Update BM_Per_Container_VS_Kontainer_Masuk set Flag_Proses = 'Y' "
    '                    Sql = Sql & "where No_Faktur = '" & Dr("No_Faktur") & "' and Kode_Perusahaan = '" & KodePerusahaan & "'and "
    '                    Sql = Sql & "Kode_Barang = '" & Dr("Kode_Barang") & "' and Kode_Stock_Owner = '" & Dr("Kode_Stock_Owner") & "' and No_Container = '" & Dr("No_Container") & "'"
    '                    Dr.Close()
    '                    ExecuteTrans(Sql)
    '                Else
    '                    Dr.Close()
    '                    CloseTrans()
    '                    CloseConn()
    '                    MessageBox.Show("barang " & LvNama & " Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                    Exit Sub
    '                End If
    '            End Using
    '        Next



    '        'SQL = "update rencana_Order set flag_Selisih_Barang_Masuk = 'Y' "
    '        'SQL = SQL & " where Id_Rencana = '" & TxtId_Rencana.Text & "'"
    '        'ExecuteTrans(SQL)



    '        Cmd.Transaction.Commit()
    '        CloseTrans()
    '        CloseConn()
    '        MessageBox.Show("Data Tersimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    Catch ex As Exception
    '        CloseTrans()
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    '    Kosong()
    'End Sub

    'Public Sub TxtId_Rencana_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtSelisihBrgMsk_NoPo.Leave
    '    If TxtSelisihBrgMsk_NoPo.Text.Trim.Length = 0 Then Exit Sub
    '    Try

    '        OpenConn()
    '        Cmd.Transaction() = Cn.BeginTransaction

    '        DgvSelisihBrgMsk_DataSelisih.Rows.Clear()
    '        'ListView2.Items.Clear()

    '        'and b.No_Faktur = 'PO-GB-12/22-0004'

    '        ' ''id_rencana_group = ""
    '        ' ''Dim Konte_group As Double = 0
    '        ' ''Dim i As Integer = 0
    '        ' ''SQL = "select Flag_Gabungan from rencana_order where "
    '        ' ''SQL = SQL & "Id_rencana = '" & TxtId_Rencana.Text & "'"
    '        ' ''Using Dr2 = OpenTrans(SQL)
    '        ' ''    If Dr2.Read Then
    '        ' ''        If General_Class.CekNULL(Dr2("Flag_Gabungan")) = "Y" Then
    '        ' ''            Dr2.Close()
    '        ' ''            SQL = "select a.id_rencana, a.Total_Persen from rencana_order a, rencana_order_gabungan b where "
    '        ' ''            SQL = SQL & "a.id_rencana = b.Id_rencana and b.Id_rencana_induk = '" & TxtId_Rencana.Text & "'"
    '        ' ''            Using Dr = OpenTrans(SQL)
    '        ' ''                Do While Dr.Read
    '        ' ''                    If i <> 0 Then
    '        ' ''                        id_rencana_group = id_rencana_group & ", "
    '        ' ''                    End If
    '        ' ''                    id_rencana_group = id_rencana_group & "'" & Dr("id_rencana") & "'"
    '        ' ''                    Konte_group = Konte_group + Dr("Total_Persen")
    '        ' ''                    i += 1
    '        ' ''                Loop
    '        ' ''            End Using

    '        ' ''            Dim kontainer As Integer = Konte_group / 100
    '        ' ''            Dim jumlah As Integer = kontainer * 100
    '        ' ''            Dim selisih As Integer = Konte_group - jumlah

    '        ' ''            If selisih = 0 Or selisih <= 20 Then
    '        ' ''                TxtJumlah_conte.Text = kontainer
    '        ' ''            Else
    '        ' ''                TxtJumlah_conte.Text = kontainer + 1
    '        ' ''            End If

    '        ' ''        Else
    '        id_rencana_group = "'" & TxtSelisihBrgMsk_NoPo.Text & "'"
    '        ' ''        End If
    '        ' ''    Else
    '        ' ''Dr2.Close()
    '        ' ''CloseTrans()
    '        ' ''CloseConn()
    '        ' ''MessageBox.Show("Id Rencana tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        ' ''Exit Sub
    '        ' ''    End If
    '        ' ''End Using


    '        ' ''SQL = "select a.No_Container, a.Kode_Stock_Owner, a.Kode_Barang, c.nama, a.Jumlah, "
    '        ' ''SQL = SQL & "a.Jumlah_Masuk, (a.Jumlah_Masuk - a.Jumlah) as selisih, b.No_Faktur "
    '        ' ''SQL = SQL & "from BM_Per_Container_VS_Kontainer_Masuk a, Barang_Masuk b, Barang c "
    '        ' ''SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur_Loading_Barang "
    '        ' ''SQL = SQL & "and a.No_Container = b.No_Container and a.Kode_Perusahaan = c.Kode_Perusahaan and "
    '        ' ''SQL = SQL & "a.Kode_stock_Owner = c.Kode_stock_Owner and "
    '        ' ''SQL = SQL & "a.Kode_barang = c.Kode_Barang and id_rencana in(" & id_rencana_group & ") "

    'Sql = "select a.No_Container, a.Kode_Stock_Owner, a.Kode_Barang, c.nama, a.Jumlah, "
    'Sql = Sql & "a.Jumlah_Masuk, (a.Jumlah_Masuk - a.Jumlah) as selisih, b.No_Faktur, a.Harga "
    'Sql = Sql & "from BM_Per_Container_VS_Kontainer_Masuk a, Barang_Masuk b, Barang c "
    'Sql = Sql & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur "
    'Sql = Sql & "and a.No_Container = b.No_Container and a.Kode_Perusahaan = c.Kode_Perusahaan and "
    'Sql = Sql & "a.Kode_stock_Owner = c.Kode_stock_Owner and "
    'Sql = Sql & "a.Kode_barang = c.Kode_Barang and id_rencana in(" & id_rencana_group & ") and a.No_Faktur = '" & No_BM & "'"
    'Sql = Sql & "and a.No_Container = '" & TxtSelisihBrgMsk_NmContainer.Text & "' AND a.Kode_Perusahaan = '" & KodePerusahaan & "' and b.status is null"
    '        Using Ds = BindingTrans(Sql)
    '            With Ds.Tables("MyTable")
    '                For index As Integer = 0 To .Rows.Count - 1

    '                    DgvSelisihBrgMsk_DataSelisih.Rows.Add(1)
    '                    DgvSelisihBrgMsk_DataSelisih.Rows.Item(index).Cells(cellKonte).Value = .Rows(index).Item("No_Container")
    '                    DgvSelisihBrgMsk_DataSelisih.Rows.Item(index).Cells(cellLokasi).Value = .Rows(index).Item("Kode_Stock_Owner")
    '                    DgvSelisihBrgMsk_DataSelisih.Rows.Item(index).Cells(cellKdBarang).Value = .Rows(index).Item("Kode_Barang")
    '                    DgvSelisihBrgMsk_DataSelisih.Rows.Item(index).Cells(cellNama).Value = .Rows(index).Item("nama")
    '                    DgvSelisihBrgMsk_DataSelisih.Rows.Item(index).Cells(cellJumlahPL).Value = .Rows(index).Item("Jumlah")
    '                    DgvSelisihBrgMsk_DataSelisih.Rows.Item(index).Cells(cellJumlahBM).Value = .Rows(index).Item("Jumlah_Masuk")
    '                    DgvSelisihBrgMsk_DataSelisih.Rows.Item(index).Cells(cellSelisih).Value = .Rows(index).Item("selisih")
    '                    DgvSelisihBrgMsk_DataSelisih.Rows.Item(index).Cells(cellQtyPlus).Value = ""
    '                    DgvSelisihBrgMsk_DataSelisih.Rows.Item(index).Cells(cellQtyMin).Value = ""
    '                    DgvSelisihBrgMsk_DataSelisih.Rows.Item(index).Cells(cellKet).Value = ""
    '                    DgvSelisihBrgMsk_DataSelisih.Rows.Item(index).Cells(cellSelisihFix).Value = .Rows(index).Item("selisih")
    '                    DgvSelisihBrgMsk_DataSelisih.Rows.Item(index).Cells(cellHarga).Value = Format(.Rows(index).Item("Harga"), "N0")
    '                    DgvSelisihBrgMsk_DataSelisih.Rows.Item(index).Cells(cellSelisihRp).Value = Format(.Rows(index).Item("selisih") * .Rows(index).Item("Harga"), "N0")
    '                    DgvSelisihBrgMsk_DataSelisih.Rows.Item(index).Cells(CellFakturBM).Value = .Rows(index).Item("No_Faktur")
    '                    DgvSelisihBrgMsk_DataSelisih.Rows.Item(index).Cells(CellTglProduksi).Value = ""
    '                    DgvSelisihBrgMsk_DataSelisih.Rows.Item(index).Cells(CellTglExpired).Value = ""
    '                    DgvSelisihBrgMsk_DataSelisih.Rows.Item(index).Cells(CellSN).Value = ""

    '                Next
    '            End With
    '        End Using


    '        Cmd.Transaction.Commit()
    '        CloseConn()



    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    '    Jumlah_selisih()
    'End Sub

    'Private Sub TxtId_Rencana_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtSelisihBrgMsk_NoPo.TextChanged


    'End Sub

    'Private Sub TxtContainer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSelisihBrgMsk_NmContainer.TextChanged

    'End Sub

    'Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblSelisihBrgMsk_NmContainer.Click

    'End Sub


    'Private Sub Submit_PO_Import_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
    '    Label1.Size = New Point(Me.Width, 33)
    'End Sub

    'Private Sub DataGridView1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvSelisihBrgMsk_DataSelisih.CellEndEdit
    '    Get_Isi_Listview(DgvSelisihBrgMsk_DataSelisih.CurrentRow.Index)


    '    If IsNumeric(LvQtyPlus) = False Or Val(LvQtyPlus) < 0 Then
    '        DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(cellQtyPlus).Value = ""
    '    End If

    '    If IsNumeric(LvQtyMin) = False Or Val(LvQtyMin) < 0 Then
    '        DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(cellQtyMin).Value = ""
    '    End If

    '    Get_Isi_Listview(DgvSelisihBrgMsk_DataSelisih.CurrentRow.Index)

    '    Dim QtyPlus As Double = 0
    '    Dim QtyMin As Double = 0

    '    If LvQtyPlus = "" Then
    '        QtyPlus = 0
    '    Else
    '        QtyPlus = Val(HilangkanTanda(LvQtyPlus))
    '    End If

    '    If LvQtyMin = "" Then
    '        QtyMin = 0
    '    Else
    '        QtyMin = Val(HilangkanTanda(LvQtyMin))
    '    End If

    '    Dim selisih_fix As Double = 0

    '    selisih_fix = Val(HilangkanTanda(LvSelisih)) + QtyPlus - QtyMin

    '    DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(cellSelisihFix).Value = selisih_fix
    '    DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(cellSelisihRp).Value = Format(selisih_fix * Val(HilangkanTanda(LvHarga)), "N0")

    '    Jumlah_selisih()

    'End Sub

    'Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvSelisihBrgMsk_DataSelisih.CellContentClick

    'End Sub

    'Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DgvSelisihBrgMsk_DataSelisih.DoubleClick
    '    If CekNothing(DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(cellQtyPlus).Value) = "" Or CekNothing(DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(cellQtyMin).Value) = "" Then
    '        MessageBox.Show("Data Belum DI Input !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Exit Sub
    '    End If

    '    If CekNothing(DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(cellQtyPlus).Value) = "0" And CekNothing(DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(cellQtyMin).Value) = "0" Then
    '        MessageBox.Show("Tidak Perlu Input Tgl Expired untuk data tidak selisih !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Exit Sub
    '    End Ifdele

    '    Display_SN_Selisih_Barang_Masuk.kosong()

    '    Display_SN_Selisih_Barang_Masuk.FakturBM = DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(CellFakturBM).Value
    '    Display_SN_Selisih_Barang_Masuk.Harga = DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(cellHarga).Value
    '    Display_SN_Selisih_Barang_Masuk.TextBoxNoKonte.Text = DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(cellKonte).Value
    '    Display_SN_Selisih_Barang_Masuk.TextBoxLokasi.Text = DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(cellLokasi).Value
    '    Display_SN_Selisih_Barang_Masuk.TextBoxkdBrg.Text = DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(cellKdBarang).Value
    '    Display_SN_Selisih_Barang_Masuk.TextBoxNmBrg.Text = DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(cellNama).Value


    '    If DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(cellQtyPlus).Value > 0 Then
    '        Display_SN_Selisih_Barang_Masuk.TextBoxQty.Text = DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(cellQtyPlus).Value
    '        Display_SN_Selisih_Barang_Masuk.Label8.Text = "Plus"
    '        Display_SN_Selisih_Barang_Masuk.CheckBox1.Enabled = True
    '    Else
    '        Display_SN_Selisih_Barang_Masuk.TextBoxQty.Text = DgvSelisihBrgMsk_DataSelisih.CurrentRow.Cells(cellQtyMin).Value
    '        Display_SN_Selisih_Barang_Masuk.Label8.Text = "Min"
    '        Display_SN_Selisih_Barang_Masuk.CheckBox1.Enabled = False
    '    End If

    '    Display_SN_Selisih_Barang_Masuk.ShowDialog()
    'End Sub
End Class