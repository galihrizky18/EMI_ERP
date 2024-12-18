Public Class Emi_Flever2

    Dim arrInisialFaktur As New ArrayList

    Dim Lv_KdSoAwal, Lv_KdSoAkhir, Lv_KdBarangAwal, Lv_KdBarangAkhir, Lv_JmlReq, Lv_JmlhEarn, Lv_Rv, Lv_NamaBarangMin, Lv_NamaBarangPlus As String
    Dim Dgv_KdBarang, Dgv_IdWareHouse, Dgv_Sn, Dgv_NmBarang, Dgv_Posisi, Dgv_GoodStock, Dgv_Satuan, Dgv_ChkBox, Dgv_JmlhFlever As String

    Dim fSO As String = ""

    Dim itemLv_SoAwal As Integer = 0
    Dim itemLv_SoAkhir As Integer = 1
    Dim itemLv_NamaBarangAwal As Integer = 2
    Dim itemLv_NamaBarangAkhir As Integer = 3
    Dim itemLv_Required As Integer = 4
    Dim itemLv_Earned As Integer = 5
    Dim itemLv_KdBarangAwal As Integer = 6
    Dim itemLv_KdBarangAkhir As Integer = 7
    Dim itemLv_Rv As Integer = 8

    Dim itemDgv_KdBarang As Integer = 0
    Dim itemDgv_IdWareHouse As Integer = 1
    Dim itemDgv_Sn As Integer = 2
    Dim itemDgv_NmBarang As Integer = 3
    Dim itemDgv_Posisi As Integer = 4
    Dim itemDgv_GoodStock As Integer = 5
    Dim itemDgv_Satuan As Integer = 6
    Dim itemDgv_ChkBox As Integer = 7
    Dim itemDgv_JmlhFlever As Integer = 8

    Private Sub Emi_Flever2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong()
        Initial_ListView()
    End Sub


    Private Sub kosong()
        Cmb_Lokasi.Items.Clear()

        TxtFaktur.Text = String.Empty
        DateTimePicker2.Value = DateTime.Now
        fSO = ""

        TxtKd_Barang.Text = String.Empty
        Txt_NamaBarang.Text = String.Empty
        Txt_SoAwal.Text = String.Empty
        Txt_SoAkhir.Text = String.Empty
        Txt_BarangAwal.Text = String.Empty
        Txt_BarangAkhir.Text = String.Empty
        Txt_Required.Text = String.Empty
        Txt_Earned.Text = String.Empty
        txt_Keterangan.Text = String.Empty
        Lbl_Total.Text = "0"

        Dgv_BarangSn.Rows.Clear()
        Lv_Cari.Items.Clear()
        Lv_Cari.Visible = False
        Lv_Cari.Location = New Point(1024, 157)

        Try
            OpenConn()

            SQL = "Select kode_stock_owner, flag_default, flag_reseller, kategori_pengganti_reseller, inisial_faktur From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Lokasi.Items.Add(dr("kode_stock_owner"))
                    arrInisialFaktur.Add(dr("inisial_faktur"))
                Loop
            End Using

            SQL = "select Top(1)a.Lokasi_Gudang from EMI_Kategori_Gudang_PerLokasi a,Barang b where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.ID_Kategori_Gudang = b.Id_Kategori_Gudang and "
            SQL = SQL & "a.Kode_Stock_Owner = '" & Cmb_Lokasi.Text & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            fSO = .Rows(i).Item("Lokasi_Gudang")
                        Next
                    End If
                End With
            End Using

            Cmb_Lokasi.SelectedIndex = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub Initial_ListView()
        Lv_Cari.Columns.Add("SO Awal", 150, HorizontalAlignment.Center)
        Lv_Cari.Columns.Add("SO Akhir", 150, HorizontalAlignment.Center)
        Lv_Cari.Columns.Add("Barang Awal", 190, HorizontalAlignment.Center)
        Lv_Cari.Columns.Add("Barang Akhir", 190, HorizontalAlignment.Center)

        'HIDDEN
        Lv_Cari.Columns.Add("Jumlah Dibutuhkan", 0, HorizontalAlignment.Center)
        Lv_Cari.Columns.Add("Hasil Flever", 0, HorizontalAlignment.Center)
        Lv_Cari.Columns.Add("KdBarangAwal", 0, HorizontalAlignment.Center)
        Lv_Cari.Columns.Add("KdBarangAkhir", 0, HorizontalAlignment.Center)
        Lv_Cari.Columns.Add("RV", 0, HorizontalAlignment.Center)

        Lv_Cari.View = View.Details
    End Sub

    Private Sub Get_ListView_Data(ByVal index As Integer)

        Lv_KdSoAwal = Lv_Cari.Items(index).SubItems(itemLv_SoAwal).Text
        Lv_KdSoAkhir = Lv_Cari.Items(index).SubItems(itemLv_SoAkhir).Text
        Lv_KdBarangAwal = Lv_Cari.Items(index).SubItems(itemLv_KdBarangAwal).Text
        Lv_KdBarangAkhir = Lv_Cari.Items(index).SubItems(itemLv_KdBarangAkhir).Text
        Lv_JmlReq = Lv_Cari.Items(index).SubItems(itemLv_Required).Text
        Lv_JmlhEarn = Lv_Cari.Items(index).SubItems(itemLv_Earned).Text
        Lv_Rv = Lv_Cari.Items(index).SubItems(itemLv_Rv).Text
        Lv_NamaBarangMin = Lv_Cari.Items(index).SubItems(itemLv_NamaBarangAwal).Text
        Lv_NamaBarangPlus = Lv_Cari.Items(index).SubItems(itemLv_NamaBarangAkhir).Text

    End Sub

    Private Sub Get_GridView_Data(ByVal index As Integer)

        Dgv_KdBarang = Dgv_BarangSn.Rows(index).Cells(itemDgv_KdBarang).Value
        Dgv_IdWareHouse = Dgv_BarangSn.Rows(index).Cells(itemDgv_IdWareHouse).Value
        Dgv_Sn = Dgv_BarangSn.Rows(index).Cells(itemDgv_Sn).Value
        Dgv_NmBarang = Dgv_BarangSn.Rows(index).Cells(itemDgv_NmBarang).Value
        Dgv_Posisi = Dgv_BarangSn.Rows(index).Cells(itemDgv_Posisi).Value
        Dgv_GoodStock = Dgv_BarangSn.Rows(index).Cells(itemDgv_GoodStock).Value
        Dgv_Satuan = Dgv_BarangSn.Rows(index).Cells(itemDgv_Satuan).Value
        Dgv_ChkBox = Dgv_BarangSn.Rows(index).Cells(itemDgv_ChkBox).Value
        Dgv_JmlhFlever = Dgv_BarangSn.Rows(index).Cells(itemDgv_JmlhFlever).Value


    End Sub



    'FUNCTION HANDLE
    Private Sub TxtKd_Barang_TextChanged(sender As Object, e As EventArgs) Handles TxtKd_Barang.TextChanged
        If Cmb_Lokasi.SelectedIndex = -1 Then Cmb_Lokasi.Focus() : Exit Sub

        If TxtKd_Barang.Text = "" Then
            Txt_NamaBarang.Text = String.Empty
            Txt_Lock_RV.Text = String.Empty
            Txt_SoAwal.Text = String.Empty
            Txt_BarangAwal.Text = String.Empty
            Txt_SoAkhir.Text = String.Empty
            Txt_BarangAkhir.Text = String.Empty
            Txt_Required.Text = String.Empty
            Txt_Earned.Text = String.Empty
            Lv_Cari.Visible = False
            Lv_Cari.Location = New Point(1024, 157)
            Dgv_BarangSn.Rows.Clear()
            Lv_Cari.Items.Clear()
            Exit Sub
        End If

        Try
            OpenConn()

            Lv_Cari.Items.Clear()

            SQL = "select a.Kode_Stock_Owner_Min, a.Kode_Stock_Owner_Plus, a.Kode_Barang_Min, a.Kode_Barang_Plus, "
            SQL = SQL & "ISNULL((select nama from barang z where a.Kode_Perusahaan=z.Kode_Perusahaan and a.Kode_Stock_Owner_Min=z.Kode_Stock_Owner and a.Kode_Barang_Min=z.Kode_Barang ),'') as Nama_Barang_Min, "
            SQL = SQL & "ISNULL((select nama from barang z where a.Kode_Perusahaan=z.Kode_Perusahaan and a.Kode_Stock_Owner_Plus=z.Kode_Stock_Owner and a.Kode_Barang_Plus=z.Kode_Barang ),'') as Nama_Barang_Plus, "
            SQL = SQL & "a.Jumlah_Plus as Required, "
            SQL = SQL & "a.Pengali_Jml_Minimum as Earned, "
            SQL = SQL & "cast(rv as bigint) as RV "
            SQL = SQL & "from emi_master_flever a "
            SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "AND (SELECT z.nama FROM barang z, barang x WHERE a.Kode_Perusahaan = z.Kode_Perusahaan AND a.Kode_Stock_Owner_Min = z.Kode_Stock_Owner "
            SQL = SQL & "AND a.Kode_Barang_Min = z.Kode_Barang and a.Kode_Stock_Owner_Plus=x.Kode_Stock_Owner and a.Kode_Barang_Plus=x.Kode_Barang) LIKE '" & TxtKd_Barang.Text & "%' "
            SQL = SQL & "group by  a.Kode_Stock_Owner_Min, a.Kode_Stock_Owner_Plus, a.Kode_Barang_Min, a.Kode_Barang_Plus, a.Jumlah_Plus, a.Pengali_Jml_Minimum, a.RV, a.Kode_Perusahaan, a.Kode_Stock_Owner_Min, a.Kode_Barang_Min, "
            SQL = SQL & "a.Kode_Stock_Owner_Plus, a.Kode_Barang_Plus "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As ListViewItem
                    lv = Lv_Cari.Items.Add(Dr("Kode_Stock_Owner_Min"))
                    lv.SubItems.Add(Dr("Kode_Stock_Owner_Plus"))
                    lv.SubItems.Add(Dr("Nama_Barang_Min"))
                    lv.SubItems.Add(Dr("Nama_Barang_Plus"))
                    lv.SubItems.Add(Dr("Required"))
                    lv.SubItems.Add(Dr("Earned"))
                    lv.SubItems.Add(Dr("Kode_Barang_Min"))
                    lv.SubItems.Add(Dr("Kode_Barang_Plus"))
                    lv.SubItems.Add(Dr("RV"))
                Loop
            End Using

            Lv_Cari.Visible = True
            'Lv_Cari.Location = New Point(111, 157)
            Lv_Cari.Location = New Point(124, 184)


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv_Cari_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Cari.DoubleClick
        If Lv_Cari.Items.Count = 0 Then Exit Sub

        Dim selectedIndex = Lv_Cari.FocusedItem.Index

        Get_ListView_Data(selectedIndex)

        Txt_Lock_RV.Text = Lv_Rv
        TxtKd_Barang.Text = Lv_KdBarangAwal
        Txt_NamaBarang.Text = Lv_NamaBarangMin
        Txt_SoAwal.Text = Lv_KdSoAwal
        Txt_SoAkhir.Text = Lv_KdSoAkhir
        Txt_BarangAwal.Text = Lv_NamaBarangMin
        Txt_BarangAkhir.Text = Lv_NamaBarangPlus
        Txt_Required.Text = Lv_JmlReq
        Txt_Earned.Text = Lv_JmlhEarn

        Lv_Cari.Items.Clear()
        Lv_Cari.Visible = False
        Lv_Cari.Location = New Point(1024, 157)


        Try
            OpenConn()

            '===================================
            '=      LOAD DATA GRID VIEW        =
            '===================================

            Dgv_BarangSn.Rows.Clear()

            Dim rows As Integer = 0
            SQL = "select b.Kode_Barang, b.Id_Warehouse, b.Serial_Number, a.Nama, C.Keterangan as posisi, b.Jumlah as good_stock, a.Satuan "
            SQL = SQL & "from barang a, Barang_SN b, View_Warehouse_Position c "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and b.Kode_Perusahaan=c.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner=b.Kode_Stock_Owner and a.Kode_Barang=b.Kode_Barang "
            SQL = SQL & "and b.Kode_Stock_Owner=c.Kode_Stock_Owner and b.Id_Warehouse=c.Id_WMS_Warehouse_Position "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Stock_Owner='" & Lv_KdSoAwal & "' "
            SQL = SQL & "and a.Kode_Barang='" & Lv_KdBarangAwal & "' order by a.Nama "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dgv_BarangSn.Rows.Add(1)
                    Dgv_BarangSn.Rows(rows).Cells(itemDgv_KdBarang).Value = Dr("Kode_Barang")
                    Dgv_BarangSn.Rows(rows).Cells(itemDgv_IdWareHouse).Value = Dr("Id_Warehouse")
                    Dgv_BarangSn.Rows(rows).Cells(itemDgv_Sn).Value = Dr("Serial_Number")
                    Dgv_BarangSn.Rows(rows).Cells(itemDgv_NmBarang).Value = Dr("Nama")
                    Dgv_BarangSn.Rows(rows).Cells(itemDgv_Posisi).Value = Dr("posisi")
                    Dgv_BarangSn.Rows(rows).Cells(itemDgv_GoodStock).Value = Dr("good_stock")
                    Dgv_BarangSn.Rows(rows).Cells(itemDgv_Satuan).Value = Dr("Satuan")

                    Dgv_BarangSn.Rows(rows).Cells(itemDgv_JmlhFlever).Style.BackColor = Color.LightGray
                    Dgv_BarangSn.Rows(rows).Cells(itemDgv_JmlhFlever).ReadOnly = True

                    rows = rows + 1
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub Dgv_BarangSn_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_BarangSn.CellEndEdit
        If Dgv_BarangSn.RowCount = 0 Then Exit Sub

        Dim cellGoodStock As String = Dgv_BarangSn.CurrentRow.Cells(itemDgv_GoodStock).Value

        Dim chkBox As String = Dgv_BarangSn.CurrentRow.Cells(itemDgv_ChkBox).Value
        Dim cellValue As String = Dgv_BarangSn.CurrentRow.Cells(itemDgv_JmlhFlever).Value

        If Not chkBox Is Nothing Then
            Dgv_BarangSn.CurrentRow.Cells(itemDgv_JmlhFlever).ReadOnly = False
        Else
            Dgv_BarangSn.CurrentRow.Cells(itemDgv_JmlhFlever).ReadOnly = True
            Dgv_BarangSn.CurrentRow.Cells(itemDgv_JmlhFlever).Value = ""
        End If

        If Not IsNumeric(cellValue) OrElse Val(cellValue) > Val(cellGoodStock) Or Val(cellValue) < 0 Then
            Dgv_BarangSn.CurrentRow.Cells(itemDgv_JmlhFlever).Value = ""
        End If

        HitungGrandTotal()

    End Sub

    Private Sub get_no_faktur(ByVal pembayaran As String)
        TxtFaktur.Text = fFlever & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(Tanggal_Sekarang, "MM/yy") & "-" &
                             General_Class.Get_Last_Number2("EMI_Flever", "no_faktur", JumlahDigit,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_faktur,1," & Len(fFlever) + Len(arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex)) + 6 & ")", fFlever & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(Tanggal_Sekarang, "MM/yy"))
    End Sub
    Private Sub HitungGrandTotal()
        Dim tot_qty As Integer = 0

        For i As Integer = 0 To Dgv_BarangSn.RowCount - 1
            tot_qty = tot_qty + Val(HilangkanTanda(Dgv_BarangSn.Rows(i).Cells(itemDgv_JmlhFlever).Value))
        Next

        Lbl_Total.Text = Format(tot_qty, "N0")
    End Sub


    'HANDLE BUTTON
    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If Dgv_BarangSn.Rows.Count = 0 Then Exit Sub

        If Cmb_Lokasi.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Lokasi.Focus()
            Exit Sub
        ElseIf txt_Keterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txt_Keterangan.Focus()
            Exit Sub
        End If

        GetTime()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur("")
            HitungGrandTotal()

            Dim total_hpp As Double = 0
            Dim total_hpp_real As Double = 0

            Dim flag_opm As String = ""
            Dim flag_opm_utk_cek As String = ""

            SQL = "select flag_opname, buka_flever from stock_owner "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_stock_owner = '" & Cmb_Lokasi.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    flag_opm_utk_cek = Dr("flag_opname")

                    If Dr("flag_opname") = "Y" Then
                        If Dr("buka_flever") = 0 Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show(err_msg_opname, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        ElseIf Dr("buka_flever") > 0 Then
                            Dr.Close()
                            SQL = "update stock_owner set buka_flever = buka_flever - 1 "
                            SQL = SQL & "where kode_perusahaan ='" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & Cmb_Lokasi.Text & "'"
                            ExecuteTrans(SQL)

                            flag_opm = "'Y'"
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi kesalahan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        flag_opm = "NULL"
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim Id_Transaksi As String = "NULL"

            If flag_opm_utk_cek = "Y" Then

                SQL = "Select Kode_Unik "
                SQL = SQL & "from schedule_opname where Lokasi = '" & Cmb_Lokasi.Text & "' "
                SQL = SQL & "and mulai = 'Y' and selesai = 'T'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Id_Transaksi = "'" & Dr("Kode_Unik") & "'"
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi Kesalahan, Silahkan Refresh!!!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using
            End If

            '===============================
            '=      INSERT EMI_FLEVER      =
            '===============================

            SQL = "insert into EMI_Flever(kode_perusahaan, no_faktur, tanggal, jam, userid, "
            SQL = SQL & "lokasi, total_jml, Keterangan, flag_opm, XTermx, id_transaksi) values("
            SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
            SQL = SQL & "'" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
            'SQL = SQL & "'" & UserID & "', '" & Cmb_Lokasi.Text & "', "
            SQL = SQL & "'" & UserID & "', '" & fSO & "', "
            SQL = SQL & "'" & HilangkanTanda(Lbl_Total.Text) & "', '" & txt_Keterangan.Text.Trim & "', "
            SQL = SQL & "" & flag_opm & ", 'x', " & Id_Transaksi & ")"
            ExecuteTrans(SQL)


            For i As Integer = 0 To Dgv_BarangSn.RowCount - 1

                Get_GridView_Data(i)

                If Dgv_ChkBox = "" Then Continue For

                'If IsNumeric(Dgv_JmlhFlever) = False Then
                '    CloseTrans()
                '    CloseConn()
                '    MessageBox.Show("Jumlah yg diisi bukan angka!", Judul, MessageBoxButtons.OK)
                '    Exit Sub
                'ElseIf Val(HilangkanTanda(Dgv_JmlhFlever)) < Val(HilangkanTanda(Txt_Required.Text)) Then
                '    CloseTrans()
                '    CloseConn()
                '    MessageBox.Show("Jumlah yg diisi tidak boleh kurang dari minimum!", Judul, MessageBoxButtons.OK)
                '    Exit Sub
                'ElseIf Val(HilangkanTanda(LvJmlPlus)) > (Val(HilangkanTanda(LvJmlMin)) * Val(HilangkanTanda(LvJmlMinimum))) + 1 Then
                '    CloseTrans()
                '    CloseConn()
                '    MessageBox.Show("Jumlah yg diisi tidak boleh lebih dari minimum!", Judul, MessageBoxButtons.OK)
                '    Exit Sub
                'End If

                If IsNumeric(Dgv_JmlhFlever) = False Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Jumlah yg diisi bukan angka!", Judul, MessageBoxButtons.OK)
                    Exit Sub
                ElseIf Val(HilangkanTanda(Dgv_JmlhFlever)) < Val(HilangkanTanda(Txt_Required.Text)) Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Jumlah yg diisi tidak boleh kurang dari required!", Judul, MessageBoxButtons.OK)
                    Exit Sub
                ElseIf Val(HilangkanTanda(Dgv_JmlhFlever)) > Val(HilangkanTanda(Dgv_GoodStock)) Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Jumlah yg diisi tidak boleh lebih dari good stock!", Judul, MessageBoxButtons.OK)
                    Exit Sub
                End If

                '======================================
                '=      INSERT EMI_DETAIL_FLEVER      =
                '======================================

                SQL = "insert into EMI_Detail_Flever(kode_perusahaan, no_faktur, kode_stock_owner_min, "
                SQL = SQL & "kode_barang_min, jumlah_min, kode_stock_owner_plus, kode_barang_plus, jumlah_plus, id_Warehouse, Serial_Number)"
                SQL = SQL & "values('" & KodePerusahaan & "', "
                SQL = SQL & "'" & TxtFaktur.Text.Trim & "', "
                SQL = SQL & "'" & Lv_KdSoAwal & "', "
                SQL = SQL & "'" & Lv_KdBarangAwal & "', "
                SQL = SQL & "'" & HilangkanTanda(Dgv_GoodStock) & "', "
                SQL = SQL & "'" & Lv_KdSoAkhir & "', "
                SQL = SQL & "'" & Lv_KdBarangAkhir & "', "
                SQL = SQL & "'" & HilangkanTanda(Dgv_JmlhFlever) & "', "
                SQL = SQL & "'" & Dgv_IdWareHouse & "', "
                SQL = SQL & "'" & Dgv_Sn & "')"
                ExecuteTrans(SQL)

                Dim x_no_urut_det_penj As Integer = 0
                SQL = "select IDENT_CURRENT('EMI_Detail_Flever') as urutan"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        x_no_urut_det_penj = Dr("urutan")
                    End If
                End Using


                '============================================
                '=      UPDATE DATA BARANG (BERKURANG)      =
                '============================================

                SQL = "select good_stock from barang where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & Lv_KdSoAwal & "' and "
                SQL = SQL & "kode_barang = '" & Lv_KdBarangAwal & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Dr("good_stock") - HilangkanTanda(Dgv_JmlhFlever) < 0 Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses membuat stock menjadi negatif untuk barang " & Lv_NamaBarangMin & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        Else
                            Dr.Close()
                            SQL = "Update barang set good_stock = good_stock - " & HilangkanTanda(Dgv_JmlhFlever) & " where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & Lv_KdSoAwal & "' and "
                            SQL = SQL & "kode_barang = '" & Lv_KdBarangAwal & "'"
                            ExecuteTrans(SQL)
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        Exit Sub
                    End If
                End Using

                '============================================
                '=      UPDATE DATA BARANG (BERTAMBAH)      =
                '============================================

                SQL = "select good_stock from barang where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & Lv_KdSoAkhir & "' and "
                SQL = SQL & "kode_barang = '" & Lv_KdBarangAkhir & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()

                        Dim hasilDidapat As Double = Math.Round((Val(Dgv_JmlhFlever) / Val(Txt_Required.Text)) * Val(Txt_Earned.Text), 2)
                        Dim hasilDidapat1 = hasilDidapat.ToString().Replace(",", ".")

                        SQL = "Update barang set good_stock = good_stock + " & hasilDidapat1 & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_stock_owner = '" & Lv_KdSoAkhir & "' and "
                        SQL = SQL & "kode_barang = '" & Lv_KdBarangAkhir & "'"
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        Exit Sub
                    End If
                End Using

                '====================
                '=      CEK RV      =
                '====================

                SQL = "select cast(rv as bigint) as rvx from EMI_Master_Flever where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner_min = '" & Lv_KdSoAwal & "' and "
                SQL = SQL & "kode_barang_min = '" & Lv_KdBarangAwal & "' and "
                SQL = SQL & "kode_stock_owner_plus = '" & Lv_KdSoAkhir & "' and "
                SQL = SQL & "kode_barang_plus = '" & Lv_KdBarangAkhir & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        If dr("rvx") <> Txt_Lock_RV.Text Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Sudah ada perubahan di master flever!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data master flever tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                '==============================
                '=      UPDATE BARANG_SN      =
                '==============================

                Dim jmlh_Dibutuhkan As Double = 0
                Dim jmlhInput As Double = 0
                Dim jmlh_Bertambah As Double = 0

                SQL = "select kode_stock_owner, kode_barang, serial_number, jumlah, Tgl_Produksi, Tgl_Expired "
                SQL = SQL & "from barang_sn "
                SQL = SQL & "where Kode_Stock_Owner='" & Lv_KdSoAwal & "' and Kode_Barang='" & Lv_KdBarangAwal & "' "
                SQL = SQL & "and serial_number='" & Dgv_Sn & "' "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            jmlh_Dibutuhkan = HilangkanTanda(Txt_Required.Text)
                            jmlhInput = HilangkanTanda(Dgv_JmlhFlever)
                            jmlh_Bertambah = Math.Round((Val(jmlhInput) / Val(jmlh_Dibutuhkan)) * Val(Txt_Earned.Text), 2)



                            For j As Integer = 0 To .Rows.Count - 1

                                '==========================================
                                '=      UPDATE BARANG_SN (BERKURANG)      =
                                '==========================================

                                If .Rows(j).Item("jumlah") - jmlhInput < 0 Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Jumlah Input Akan Menyebabkan Stock Minus")
                                    Exit Sub

                                Else

                                    SQL = "Update barang_sn set jumlah = jumlah - " & jmlhInput & " where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_stock_owner = '" & .Rows(j).Item("kode_stock_owner") & "' and "
                                    SQL = SQL & "kode_barang = '" & .Rows(j).Item("kode_barang") & "' and "
                                    SQL = SQL & "serial_number = '" & Dgv_Sn & "'"
                                    ExecuteTrans(SQL)
                                End If


                                Dim modal_satuan As Double = Math.Floor((Get_Harga_SN(.Rows(j).Item("serial_number")) * jmlh_Dibutuhkan) / jmlhInput)
                                total_hpp = total_hpp + (jmlhInput * modal_satuan)
                                total_hpp_real = total_hpp_real + (jmlh_Dibutuhkan * Get_Harga_SN(.Rows(j).Item("serial_number")))

                                Dim Rand As New Random
                                Dim Kode_Unik As String = Format(Rand.Next(0, 999), "000") & Format(Tanggal_Sekarang, "HHmmss")
                                Dim SN As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & modal_satuan & Tanda_SN & "02" & Tanda_SN & Format(Tanggal_Sekarang, "yyyy-MM-dd")


                                '====================================
                                '=      INPUT BARANG_SN (BARU)      =
                                '====================================

                                SQL = "select kode_barang from barang_sn where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_stock_owner = '" & Lv_KdSoAkhir & "' and "
                                SQL = SQL & "kode_barang = '" & Lv_KdBarangAkhir & "' and serial_number = '" & SN & "'"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi kesalahan pada Barang SN ! Ulangi Transaksi !")
                                        Exit Sub
                                        'SQL = "Update barang_sn set jumlah = jumlah + " & sisa_plus & " where "
                                        'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        'SQL = SQL & "kode_stock_owner = '" & LvSO & "' and kode_barang = '" & LvKBPlus & "' and "
                                        'SQL = SQL & "serial_number = '" & SN & "'"
                                        'ExecuteTrans(SQL)
                                    Else
                                        SQL = "insert into barang_sn(kode_perusahaan, kode_stock_owner, kode_barang, "
                                        SQL = SQL & "serial_number, jumlah, Tgl_Produksi, Tgl_Expired) values('" & KodePerusahaan & "', "
                                        SQL = SQL & "'" & Lv_KdSoAkhir & "', '" & Lv_KdBarangAkhir & "', "
                                        SQL = SQL & "'" & SN & "', " & jmlh_Bertambah.ToString.Replace(",", ".") & ", '" & .Rows(j).Item("Tgl_Produksi") & "', '" & .Rows(j).Item("Tgl_Expired") & "')"
                                        Dr.Close()
                                        ExecuteTrans(SQL)
                                    End If
                                End Using

                                SQL = "insert into EMI_Det_Flever(kode_perusahaan, no_faktur, "
                                SQL = SQL & "kode_stock_owner_min, kode_barang_min, serial_number_min, "
                                SQL = SQL & "no_urut, jumlah_min, kode_stock_owner_plus, "
                                SQL = SQL & "kode_barang_plus, serial_number_plus, jumlah_plus, Hasil_Yg_Bertambah, id_Warehouse) values("
                                SQL = SQL & "'" & KodePerusahaan & "', "
                                SQL = SQL & "'" & TxtFaktur.Text.Trim & "', "
                                SQL = SQL & "'" & .Rows(j).Item("kode_stock_owner") & "', "
                                SQL = SQL & "'" & .Rows(j).Item("kode_barang") & "', "
                                SQL = SQL & "'" & .Rows(j).Item("serial_number") & "', "
                                SQL = SQL & "" & x_no_urut_det_penj & ", '" & jmlh_Dibutuhkan & "', "
                                SQL = SQL & "'" & Lv_KdSoAkhir & "', "
                                SQL = SQL & "'" & Lv_KdSoAkhir & "', '" & SN & "', " & jmlhInput & ", " & jmlh_Bertambah.ToString.Replace(",", ".") & ", '" & Dgv_IdWareHouse & "')"
                                ExecuteTrans(SQL)

                                jmlh_Dibutuhkan = 0
                                jmlhInput = 0

                            Next


                        End If
                    End With
                End Using

            Next


            If total_hpp_real - total_hpp <> 0 Then
                Dim pagenumber As Integer = 0

                Get_Data_Acc()
                Dim Kode_Voucher As String = GetLastNumberJurnal(Format(Tanggal_Sekarang, "yyyyMM"), fJU & arrInisialFaktur(Cmb_Lokasi.SelectedIndex), KodePerusahaan)

                pagenumber = 1

                SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                SQL = SQL & "'" & Kode_Voucher & "', "
                SQL = SQL & "'" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                SQL = SQL & "'" & KodeProyek & "', 'Flever " & TxtFaktur.Text.Trim & "', '', "
                ''SQL = SQL & "'-', '" & UserID & "', '" & Cmb_Lokasi.Text & "')"
                SQL = SQL & "'-', '" & UserID & "', '" & fSO & "')"
                ExecuteTrans(SQL)

                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(X_Biaya_Flever, 1),
                              Strings.Mid(X_Biaya_Flever, 2, 1),
                              Strings.Mid(Ganti(X_Biaya_Flever), 3),
                              KodePerusahaan, KodeProyek, "Biaya Flever " & TxtFaktur.Text.Trim, total_hpp_real - total_hpp, "0", pagenumber, "BELUM")
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(X_Persediaan, 1),
                              Strings.Mid(X_Persediaan, 2, 1),
                              Strings.Mid(Ganti(X_Persediaan), 3),
                              KodePerusahaan, KodeProyek, "Biaya Flever " & TxtFaktur.Text.Trim, "0", total_hpp_real - total_hpp, pagenumber, "BELUM")
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                SQL = "update EMI_Flever set kode_voucher_1 = '" & Kode_Voucher & "' where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_faktur = '" & TxtFaktur.Text.Trim & "'"
                ExecuteTrans(SQL)
            End If


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Berhasil Input")
            kosong()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub



















End Class