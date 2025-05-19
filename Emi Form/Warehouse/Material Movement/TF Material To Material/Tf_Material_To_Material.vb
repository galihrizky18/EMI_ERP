Public Class Tf_Material_To_Material

    Dim JudulForm As String = "Transfer Material To Material"

    Dim arrSO, arrInisialFaktur, arrIdWMSWarehouse, WarehosePosition, arrJmlBrngAwal, arrJmlBrgAkhir, arrKdBrgTujuan, arrKSO As New ArrayList

    Dim arr2RakTujuan As New List(Of List(Of String))

    Dim lv_DetKodeSO, lv_DetKodeBarang, lv_DetNamaBarang, lv_DetGoodStock, lv_DetSatuan, lv_DetSatuanDIsplay, lv_DetJmlhBags, lv_DetSatuanBags As String

    Dim dgv_Lokasi, dgv_KodeBarang, dgv_SerialNumber, dgv_Nama, dgv_IDWareHouse, dgv_KodeRak As String
    Dim dgv_IDPallet, dgv_GoodStock, dgv_Satuan, dgv_Jumlah, dgv_JmlhBags, dgv_Batch, dgv_Harga, dgv_HargaDisplay, dgv_TglProduksi, dgv_TglExpired, dgv_Barcode, dgv_FlagBlokSN As String
    Dim dgv_JumlahAwal, dgvJumlahAkhir, dgvKdBarangTujuan As String
    Dim dgv_CheckBox As Boolean

    Dim kd_barang As String

    Dim TotalTransfer As Double = 0

    Dim itemDetKodeSO As Integer = 0
    Dim itemDetKodeBarang As Integer = 1
    Dim itemDetNamaBarang As Integer = 2
    Dim itemDetGoodStock As Integer = 3
    Dim itemDetSatuan As Integer = 4
    Dim itemDetSatuandisplay As Integer = 5
    Dim itemDetJmlhBags As Integer = 6
    Dim itemDetSatuanBags As Integer = 7

    Dim itemDgvLokasi As Integer = 0
    Dim itemDgvKodeBarang As Integer = 1
    Dim itemDgvSerialNumber As Integer = 2
    Dim itemDgvNama As Integer = 3
    Dim itemDgvIDWareHose As Integer = 4
    Dim itemDgvKodeRak As Integer = 5
    Dim itemDgvIDPallet As Integer = 6
    Dim itemDgvGoodStock As Integer = 7
    Dim itemDgvSatuan As Integer = 8
    Dim itemDgvStockBags As Integer = 9
    Dim itemDgvCheckBox As Integer = 10
    Dim itemDgvJumlah As Integer = 11
    Dim itemDgvBags As Integer = 12
    Dim itemDgvBatch As Integer = 13
    Dim itemDgvHarga As Integer = 14
    Dim itemDgvHargaDisplay As Integer = 15
    Dim itemDgvJmlAwal As Integer = 16
    Dim itemDgvJmlAkhir As Integer = 17
    Dim itemDgvKdBrgTujuan As Integer = 18
    Dim itemTglProduksi As Integer = 19
    Dim itemTglExpired As Integer = 20
    Dim itemBarcode As Integer = 21
    Dim itemFlagBlokSn As Integer = 22

    Private Sub Transfer_Stock_3_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Transfer_Stock_3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        kosong()
        Initial_List_View()

    End Sub

    Private Sub kosong()
        get_jam()

        'DGV_Data_TF.Columns(itemDgvHarga).DisplayIndex = 0
        'DGV_Data_TF.Columns(itemDgvHargaDisplay).DisplayIndex = 6
        DGV_Data_TF.Columns(itemTglProduksi).DisplayIndex = 0
        DGV_Data_TF.Columns(itemTglExpired).DisplayIndex = 1

        DGV_Data_TF.Columns(itemBarcode).DisplayIndex = 2

        Try
            OpenConn()


            CmbSO_Asal.Items.Clear() : CmbSO_Asal.SelectedIndex = -1
            SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From Stock_Owner_Gudang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and (flag_produksi='Y' or Flag_Penyimpanan='Y') "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    arrSO.Add(dr("kode_stock_owner"))
                    CmbSO_Asal.Items.Add(dr("Keterangan")) : arrInisialFaktur.Add(dr("inisial_faktur"))
                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Lv_DetBarang.Items.Clear()

        Cmb_Brg_Tujuan.Items.Clear()

        TxtSatuanKecil.Text = ""
        TxtSatuan.Text = ""
        TxtStock.Text = ""
        TxtKeterangan.Text = String.Empty
        TxtKd_Barang.Text = String.Empty
        Txt_SO.Text = String.Empty
        TxtNm_Barang.Text = String.Empty
        TxtBags.Text = String.Empty
        'TxtSatuanBags.Text = String.Empty
        TxtTotalTransferBags.Text = String.Empty

        CmbSO_Asal.Enabled = True

        DGV_Data_TF.Rows.Clear()

    End Sub



    Private Sub TxtKd_Barang_Leave(sender As Object, e As EventArgs) Handles TxtKd_Barang.Leave
        If TxtKd_Barang.Text.Trim.Length = 0 Then Exit Sub

        If CmbSO_Asal.Text.Trim.Length = 0 Then Exit Sub

        If Lv_DetBarang.Focused = True Then Exit Sub

        Try
            OpenConn()

            SQL = "select a.kode_stock_owner, a.kode_barang, a.nama, dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, a.satuan, "
            SQL = SQL & "b.satuan, a.good_stock) as Good_Stock, a.Satuan, b.satuan as satuan_display, ISNULL(a.Jumlah_Bags, 0) as Jumlah_Bags, "
            SQL = SQL & " a.Satuan_Isi_Bags from barang a, barang_detail_satuan b "
            SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' and a.Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
            SQL = SQL & "and a.kode_barang = '" & TxtKd_Barang.Text & "' and a.Kode_Barang=b.kode_barang "
            SQL = SQL & "And a.kode_Perusahaan = b.kode_Perusahaan And b.flag_tampil_display ='Y'  "
            SQL = SQL & "order by a.Kode_Barang"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    Txt_SO.Text = dr("kode_stock_owner")
                    TxtKd_Barang.Text = dr("kode_barang")
                    TxtNm_Barang.Text = "X"
                    TxtStock.Text = dr("Good_Stock")
                    TxtSatuan.Text = dr("Satuan_display")
                    TxtSatuanKecil.Text = dr("satuan")
                    TxtBags.Text = dr("Jumlah_Bags")
                    dr.Close()

                    OpenConn()
                    Cmb_Brg_Tujuan.Items.Clear() : arrJmlBrngAwal.Clear() : arrJmlBrgAkhir.Clear() : arrKdBrgTujuan.Clear()
                    SQL = "select a.kode_barang_plus, a.jumlah_barang_awal, b.Nama, a.Jumlah_Barang_Akhir, b.kode_barang "
                    SQL = SQL & "from EMI_Master_Flever a, barang b "
                    SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Barang_Min = '" & TxtKd_Barang.Text & "' "
                    SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang_Plus = b.Kode_Barang "
                    SQL = SQL & "group by a.kode_barang_plus, a.jumlah_barang_awal,b.Nama,a.Jumlah_Barang_Akhir, b.kode_barang "
                    SQL = SQL & "order by b.Nama "
                    Using Dr2 = OpenTrans(SQL)
                        Do While Dr2.Read
                            Cmb_Brg_Tujuan.Items.Add(Dr2("kode_barang")) : arrJmlBrngAwal.Add(Dr2("jumlah_barang_awal"))
                            arrJmlBrgAkhir.Add(Dr2("jumlah_barang_akhir")) : arrKdBrgTujuan.Add(Dr2("kode_barang_plus"))
                        Loop
                    End Using

                    ''convert satuan kecil ke satuan tampil display
                    'SQL = "select Satuan from Barang_Detail_Satuan where  Flag_Tampil_Display = 'Y' "
                    'SQL = SQL & "and kode_barang = '" & TxtKd_Barang.Text & "' "
                    'SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
                    'Using Dr2 = OpenTrans(SQL)
                    '    Do While Dr2.Read
                    '        TxtSatuan.Text = Dr2("satuan")
                    '    Loop
                    'End Using

                    ''convert good stock dari satuan kecil ke satuan tampil display

                    'SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & TxtKd_Barang.Text & "',"
                    'SQL = SQL & "'" & satuanKecil & "','" & TxtSatuan.Text & "',"
                    'SQL = SQL & "" & stockSatuanKecil & ") as Hasil "
                    'dr.Close()

                    'Using dr2 = OpenTrans(SQL)
                    '    If dr2.Read Then
                    '        If General_Class.CekNULL(dr2("Hasil")) <> "" Then
                    '            If dr2("Hasil") = 0 Then
                    '                MessageBox.Show("Satuan " & satuanKecil & " Ke " & TxtSatuan.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                Exit Sub
                    '            Else
                    '                TxtStock.Text = dr2("hasil")
                    '            End If
                    '        Else
                    '            dr2.Close()
                    '            CloseConn()
                    '            MessageBox.Show("Satuan " & satuanKecil & " Ke " & TxtSatuan.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '            Exit Sub
                    '        End If
                    '    End If
                    'End Using

                    Cmb_Brg_Tujuan.Focus()

                    Lv_DetBarang.Visible = False
                Else
                    Txt_SO.Text = ""
                    TxtKd_Barang.Text = ""
                    TxtNm_Barang.Text = ""
                    TxtStock.Text = ""
                    TxtSatuan.Text = ""
                    TxtBags.Text = ""
                    TxtSatuanKecil.Text = ""
                    Cmb_Brg_Tujuan.Items.Clear()
                    Cmb_Brg_Tujuan.SelectedIndex = -1
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv_DetBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_DetBarang.KeyDown
        If e.KeyCode = Keys.Enter Then Lv_DetBarang_DoubleClick(Lv_DetBarang, e)
    End Sub

    'Dim itemDgvIDWareHouseTujuan As Integer = 10



    Private Sub get_det_barang(ByVal index As Integer)
        lv_DetKodeSO = Lv_DetBarang.Items(index).SubItems(itemDetKodeSO).Text
        lv_DetKodeBarang = Lv_DetBarang.Items(index).SubItems(itemDetKodeBarang).Text
        lv_DetNamaBarang = Lv_DetBarang.Items(index).SubItems(itemDetNamaBarang).Text
        lv_DetGoodStock = Lv_DetBarang.Items(index).SubItems(itemDetGoodStock).Text
        lv_DetSatuan = Lv_DetBarang.Items(index).SubItems(itemDetSatuan).Text
        lv_DetSatuanDIsplay = Lv_DetBarang.Items(index).SubItems(itemDetSatuandisplay).Text
        lv_DetJmlhBags = Lv_DetBarang.Items(index).SubItems(itemDetJmlhBags).Text
        lv_DetSatuanBags = Lv_DetBarang.Items(index).SubItems(itemDetSatuanBags).Text
    End Sub

    Private Sub get_grid_view(ByVal index As Integer)

        dgv_Lokasi = DGV_Data_TF.Rows(index).Cells(itemDgvLokasi).Value
        dgv_KodeBarang = DGV_Data_TF.Rows(index).Cells(itemDgvKodeBarang).Value
        dgv_SerialNumber = DGV_Data_TF.Rows(index).Cells(itemDgvSerialNumber).Value
        dgv_Nama = DGV_Data_TF.Rows(index).Cells(itemDgvNama).Value
        dgv_IDWareHouse = DGV_Data_TF.Rows(index).Cells(itemDgvIDWareHose).Value
        dgv_KodeRak = DGV_Data_TF.Rows(index).Cells(itemDgvKodeRak).Value
        dgv_IDPallet = DGV_Data_TF.Rows(index).Cells(itemDgvIDPallet).Value
        dgv_GoodStock = DGV_Data_TF.Rows(index).Cells(itemDgvGoodStock).Value
        dgv_Satuan = DGV_Data_TF.Rows(index).Cells(itemDgvSatuan).Value

        dgv_CheckBox = Convert.ToBoolean(DGV_Data_TF.Rows(index).Cells(itemDgvCheckBox).Value)
        dgv_Jumlah = DGV_Data_TF.Rows(index).Cells(itemDgvJumlah).Value
        dgv_JmlhBags = DGV_Data_TF.Rows(index).Cells(itemDgvBags).Value
        dgv_Batch = DGV_Data_TF.Rows(index).Cells(itemDgvBatch).Value
        dgv_Harga = DGV_Data_TF.Rows(index).Cells(itemDgvHarga).Value
        dgv_HargaDisplay = DGV_Data_TF.Rows(index).Cells(itemDgvHargaDisplay).Value
        dgv_JumlahAwal = DGV_Data_TF.Rows(index).Cells(itemDgvJmlAwal).Value
        dgvJumlahAkhir = DGV_Data_TF.Rows(index).Cells(itemDgvJmlAkhir).Value
        dgvKdBarangTujuan = DGV_Data_TF.Rows(index).Cells(itemDgvKdBrgTujuan).Value

        dgv_TglProduksi = DGV_Data_TF.Rows(index).Cells(itemTglProduksi).Value
        dgv_TglExpired = DGV_Data_TF.Rows(index).Cells(itemTglExpired).Value
        dgv_Barcode = DGV_Data_TF.Rows(index).Cells(itemBarcode).Value

    End Sub

    Private Sub Initial_List_View()

        Lv_DetBarang.Columns.Add("Kode SO", 180, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Kode Barang", 150, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Nama", 0, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Stock", 150, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Satuan", 0, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Jumlah Bags", 150, HorizontalAlignment.Center)
        Lv_DetBarang.Columns.Add("Satuan", 0, HorizontalAlignment.Center)

        Lv_DetBarang.View = View.Details

    End Sub

    'FUNCTION UTILITY


    Private Sub DGV_Data_TF_MouseLeave(sender As Object, e As EventArgs) Handles DGV_Data_TF.MouseLeave
        'If DGV_Data_TF.RowCount = 0 Then Exit Sub

        'Dim cellValue As String = DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value

        'If Not IsNumeric(cellValue) Then
        '    DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
        'End If

        ''PINDAH FOKUS
        'DGV_Data_TF.CurrentCell = DGV_Data_TF.CurrentRow.Cells(itemDgvSatuan)
        ''DGV_Data_TF.BeginEdit(True)
    End Sub


    Private Sub get_no_faktur()

        TxtNo_Transaksi.Text = FAdj & arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy") & "-" &
                                      General_Class.Get_Last_Number2("EMI_TF_Material_To_Material", "kode_Transfer", JumlahDigit,
                                      "Kode_perusahaan", KodePerusahaan,
                                      "And", "substring(kode_Transfer,1," & Len(FAdj) + Len(arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex)) + 6 & ")", FAdj & arrInisialFaktur.Item(CmbSO_Asal.SelectedIndex) & "-" & Format(tgl_skg, "MM/yy"))

    End Sub

    Private Sub DGV_Data_TF_Leave(sender As Object, e As EventArgs) Handles DGV_Data_TF.Leave

    End Sub

    'FUNCTION HANDLE
    Private Sub TxtKd_Barang_TextChanged(sender As Object, e As EventArgs) Handles TxtKd_Barang.TextChanged, Txt_SO.TextChanged
        If CmbSO_Asal.Items.Count = 0 Or CmbSO_Asal.SelectedIndex = -1 Then Exit Sub

        If TxtKd_Barang.Text.Length >= 3 Then

            If TxtKd_Barang.Text.Trim.Length = 0 Then
                TxtKd_Barang.Visible = False : Exit Sub
            Else
                Lv_DetBarang.Visible = True
            End If

            If TxtKd_Barang.Text.Trim.Length = 0 Then
                Lv_DetBarang.Visible = False : Exit Sub
            Else
                Lv_DetBarang.Visible = True
            End If

            Lv_DetBarang.Location = New Point(18, 203)
            Lv_DetBarang.Visible = True

            TxtSatuan.Text = ""
            TxtStock.Text = ""
            TxtNm_Barang.Text = ""
            TxtBags.Text = ""
            Cmb_Brg_Tujuan.SelectedIndex = -1

            'txtJumlah.Text = ""
            'TxtPilihBarang_NamaBarang.Text = ""
            'CmbPilihBarang_Lokasi.SelectedIndex = -1
            'CmbPilihBarang_Satuan.SelectedIndex = -1

            Try
                OpenConn()

                Lv_DetBarang.Items.Clear()

                SQL = "select a.kode_stock_owner, a.kode_barang, a.nama, dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, a.satuan, "
                SQL = SQL & "b.satuan, a.good_stock) as Good_Stock, a.Satuan, b.satuan as satuan_display, ISNULL(a.Jumlah_Bags, 0) as Jumlah_Bags, "
                SQL = SQL & "a.Satuan_Isi_Bags from barang a, barang_detail_satuan b "
                SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' and a.Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
                SQL = SQL & "and a.kode_barang like '%" & TxtKd_Barang.Text & "%' and a.Kode_Barang=b.kode_barang "
                SQL = SQL & "And a.kode_Perusahaan = b.kode_Perusahaan And b.flag_tampil_display ='Y'  "
                SQL = SQL & "order by a.Kode_Barang"
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim Lv As New ListViewItem
                        Lv = Lv_DetBarang.Items.Add(Dr("kode_stock_owner"))
                        Lv.SubItems.Add(Dr("kode_barang"))
                        Lv.SubItems.Add("X")
                        Lv.SubItems.Add(Dr("Good_Stock"))
                        Lv.SubItems.Add(Dr("Satuan"))
                        Lv.SubItems.Add(Dr("satuan_display"))
                        Lv.SubItems.Add(General_Class.CekNULL(Dr("Jumlah_Bags")))
                        Lv.SubItems.Add(General_Class.CekNULL(Dr("Satuan_Isi_Bags")))
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            Lv_DetBarang.Visible = False
            Lv_DetBarang.Location = New Point(803, 258)
            Lv_DetBarang.Visible = False
        End If

        'If Not TxtKd_Barang.Text.Trim.Count = 0 Then
        '    Lv_DetBarang.Location = New Point(22, 256)
        '    Lv_DetBarang.Visible = True
        'Else
        '    Lv_DetBarang.Location = New Point(803, 258)
        '    Lv_DetBarang.Visible = False
        'End If

        'Try
        '    OpenConn()

        '    Lv_DetBarang.Items.Clear()

        '    SQL = "select a.kode_stock_owner, a.kode_barang, a.nama, dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, a.satuan, "
        '    SQL = SQL & "b.satuan, a.good_stock) as Good_Stock, a.Satuan, b.satuan as satuan_display, ISNULL(a.Jumlah_Bags, 0) as Jumlah_Bags, "
        '    SQL = SQL & " a.Satuan_Isi_Bags from barang a, barang_detail_satuan b "
        '    SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' and a.Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
        '    SQL = SQL & "and a.nama like '" & TxtKd_Barang.Text & "%' and a.Kode_Barang=b.kode_barang "
        '    SQL = SQL & "And a.kode_Perusahaan = b.kode_Perusahaan And b.flag_tampil_display ='Y'  "
        '    SQL = SQL & "order by a.Kode_Barang"
        '    Using Dr = OpenTrans(SQL)
        '        Do While Dr.Read
        '            Dim Lv As New ListViewItem
        '            Lv = Lv_DetBarang.Items.Add(Dr("kode_stock_owner"))
        '            Lv.SubItems.Add(Dr("kode_barang"))
        '            Lv.SubItems.Add(Dr("nama"))
        '            Lv.SubItems.Add(Dr("Good_Stock"))
        '            Lv.SubItems.Add(Dr("Satuan"))
        '            Lv.SubItems.Add(Dr("satuan_display"))
        '            Lv.SubItems.Add(General_Class.CekNULL(Dr("Jumlah_Bags")))
        '            Lv.SubItems.Add(General_Class.CekNULL(Dr("Satuan_Isi_Bags")))
        '        Loop
        '    End Using

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

    End Sub

    Private Sub Lv_DetBarang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DetBarang.DoubleClick

        If Lv_DetBarang.Items.Count = 0 Then Exit Sub

        TxtKd_Barang.Text = Lv_DetBarang.FocusedItem.SubItems(1).Text
        TxtKd_Barang.Focus() : Cmb_Brg_Tujuan.Focus()
        Lv_DetBarang.Visible = False

        'If Lv_DetBarang.Items.Count = 0 Or TxtKd_Barang.Text.Trim = "" Then Exit Sub

        'get_det_barang(Lv_DetBarang.FocusedItem.Index)

        'TxtKd_Barang.Text = String.Empty
        'Txt_SO.Text = String.Empty
        'TxtNm_Barang.Text = String.Empty

        'TxtKd_Barang.Text = lv_DetKodeBarang
        'Txt_SO.Text = lv_DetKodeSO
        'TxtNm_Barang.Text = lv_DetNamaBarang
        'TxtSatuan.Text = lv_DetSatuanDIsplay
        'TxtSatuanKecil.Text = lv_DetSatuan
        'TxtStock.Text = Format(Val(lv_DetGoodStock), "N0")
        'TxtBags.Text = Format(Val(lv_DetJmlhBags), "N0")
        ''TxtSatuanBags.Text = lv_DetSatuanBags

        'Lv_DetBarang.Location = New Point(803, 258)
        'Lv_DetBarang.Visible = False

        'Btn_GetData.Focus()
    End Sub

    Private Sub Btn_Insert_Click(sender As Object, e As EventArgs) Handles Btn_GetData.Click
        If CmbSO_Asal.SelectedIndex = -1 Then
            MessageBox.Show("Isi SO Awal terlebih dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSO_Asal.Focus() : Exit Sub
        ElseIf TxtKd_Barang.Text.Trim.Length = 0 Or TxtKd_Barang.Text.Trim = "" Or TxtNm_Barang.Text.Trim.Length = 0 Then
            MessageBox.Show("Harap Pilih Barang Dahulu", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKd_Barang.Focus() : Exit Sub
        ElseIf Cmb_Brg_Tujuan.Text.Trim.Length = 0 Then
            MessageBox.Show("Harap Pilih Nama Barang Tujuan Dahulu", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Brg_Tujuan.Focus() : Exit Sub
        End If



        Try
            OpenConn()

            Dim rows As Integer = 0
            DGV_Data_TF.Rows.Clear()

            kd_barang = String.Empty
            kd_barang = TxtKd_Barang.Text

            arrIdWMSWarehouse.Clear()
            WarehosePosition.Clear()

            SQL = "Select a.Id_WMS_Warehouse_Position, a.Keterangan  from "
            SQL = SQL & "view_warehouse_position a, view_warehouse_position_detail b "
            SQL = SQL & "where a.id_wms_warehouse_position = b.id_wms_warehouse_position "
            SQL = SQL & "And a.KOde_Perusahaan = b.KOde_Perusahaan "
            SQL = SQL & "And b.kode_Barang Is null "
            SQL = SQL & "and a.Kode_Stock_Owner='" & arrSO(CmbSO_Asal.SelectedIndex) & "' "
            SQL = SQL & "group by a.Id_WMS_Warehouse_Position, a.Keterangan "
            SQL = SQL & "order by a.Keterangan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    WarehosePosition.Add(Dr("Keterangan")) : arrIdWMSWarehouse.Add(Dr("Id_WMS_Warehouse_Position"))
                Loop
            End Using

            SQL = "select a.Kode_Stock_Owner, a.Kode_Barang, a.Serial_Number, dbo.get_hpp(a.Serial_Number) as Harga, b.Nama, a.Id_Warehouse, c.Keterangan as kode_rak, "
            SQL = SQL & " a.Id_Nametag_pallet, dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, b.satuan, "
            SQL = SQL & "'" & TxtSatuan.Text & "', a.jumlah) as jumlah, b.satuan, a.nomor_pallet, ISNULL(a.Jumlah_Bags, 0) as stock_bags, a.Batch_number "

            SQL = SQL & ",dbo.ubah_satuan('" & KodePerusahaan & "', 'UANG','" & TxtKd_Barang.Text & "', '" & TxtSatuanKecil.Text & "',"
            SQL = SQL & "'" & TxtSatuan.Text & "', dbo.get_hpp(a.Serial_Number) ) as Harga_display, a.Tgl_Produksi, a.Tgl_Expired, "

            SQL = SQL & "(a.Qr_Code + '-' + a.Kode_Unik_Berjalan) as Barcode, a.Blok_SN "

            SQL = SQL & "from barang_sn a, barang b, View_Warehouse_Position c "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Barang=b.Kode_Barang and a.kode_stock_Owner=b.kode_stock_Owner "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner=c.Kode_Stock_Owner "
            SQL = SQL & "and a.Id_Warehouse=c.Id_WMS_Warehouse_Position "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and b.Kode_Stock_Owner='" & Txt_SO.Text & "' and b.Kode_Barang='" & TxtKd_Barang.Text & "' "
            SQL = SQL & "and a.jumlah > 0 "
            SQL = SQL & "order by a.Kode_Barang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim subArr As New List(Of String)

                    DGV_Data_TF.Rows.Add(1)
                    DGV_Data_TF.Rows(rows).Cells(itemDgvLokasi).Value = Dr("Kode_Stock_Owner")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvKodeBarang).Value = Dr("Kode_Barang")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvSerialNumber).Value = Dr("Serial_Number")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvNama).Value = "X"
                    DGV_Data_TF.Rows(rows).Cells(itemDgvIDWareHose).Value = Dr("Id_Warehouse")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvKodeRak).Value = Dr("kode_rak")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvIDPallet).Value = Dr("nomor_pallet")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvGoodStock).Value = Format(Dr("jumlah"), "N0")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvStockBags).Value = Format(Dr("stock_bags"), "N0")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvBatch).Value = Dr("Batch_number")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvHarga).Value = Dr("Harga")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvHargaDisplay).Value = Format(Dr("Harga_display"), "N0")

                    DGV_Data_TF.Rows(rows).Cells(itemDgvSatuan).Value = TxtSatuan.Text

                    DGV_Data_TF.Rows(rows).Cells(itemDgvJumlah).Value = Format(Dr("jumlah"), "N0")
                    DGV_Data_TF.Rows(rows).Cells(itemDgvBags).Value = Format(Dr("stock_bags"), "N0")

                    DGV_Data_TF.Rows(rows).Cells(itemDgvJumlah).ReadOnly = True
                    DGV_Data_TF.Rows(rows).Cells(itemDgvBags).ReadOnly = True

                    DGV_Data_TF.Rows(rows).Cells(itemDgvJmlAwal).Value = arrJmlBrngAwal.Item(Cmb_Brg_Tujuan.SelectedIndex)

                    DGV_Data_TF.Rows(rows).Cells(itemDgvJmlAkhir).Value = arrJmlBrgAkhir.Item(Cmb_Brg_Tujuan.SelectedIndex)

                    DGV_Data_TF.Rows(rows).Cells(itemDgvKdBrgTujuan).Value = arrKdBrgTujuan.Item(Cmb_Brg_Tujuan.SelectedIndex)

                    DGV_Data_TF.Rows(rows).Cells(itemTglProduksi).Value = Format(Dr("Tgl_Produksi"), "dd MMM yyyy")
                    DGV_Data_TF.Rows(rows).Cells(itemTglExpired).Value = Format(Dr("Tgl_Expired"), "dd MMM yyyy")

                    DGV_Data_TF.Rows(rows).Cells(itemBarcode).Value = Dr("Barcode")
                    DGV_Data_TF.Rows(rows).Cells(itemFlagBlokSn).Value = General_Class.CekNULL(Dr("Blok_SN"))

                    rows = rows + 1

                Loop
            End Using

            TxtTotalTransfer.Text = 0
            TxtTotalTransferBags.Text = 0

            CmbSO_Asal.Enabled = False

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

        If DGV_Data_TF.RowCount = 0 Then
            MessageBox.Show("Belum ada barang yang mau di transfer!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKd_Barang.Focus() : Exit Sub
        ElseIf TxtKeterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKeterangan.Focus() : Exit Sub
        End If

        If CmbSO_Asal.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi Awal harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbSO_Asal.Focus() : Exit Sub
        End If

        Dim hasData As Boolean = False

        For i As Integer = 0 To DGV_Data_TF.Rows.Count - 1
            If DGV_Data_TF.Rows(i).Cells(itemDgvCheckBox).Value = "true" Then
                hasData = True : Exit For
            End If
        Next

        If Not hasData Then
            MessageBox.Show("Minimal Pilih 1 Rak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DGV_Data_TF.Focus() : Exit Sub
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur()

            '====================
            '=     CEK DATA     =
            '====================
            SQL = "select top 1 Kode_Perusahaan from EMI_TF_Material_To_Material  "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Kode_Barang_awal = '" & TxtKd_Barang.Text & "' "
            SQL = SQL & "and status is null "
            SQL = SQL & "and Flag_Validasi is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Barang Belum di Validasi", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim Total_Transfer_Kecil As Double = 0
            SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & TxtKd_Barang.Text & "', '" & TxtSatuan.Text & "',"
            SQL = SQL & "'" & TxtSatuanKecil.Text & "', '" & HilangkanTanda(TxtTotalTransfer.Text) & "' ) as hasil"
            Using Dr1 = OpenTrans(SQL)
                If Dr1.Read Then
                    If General_Class.CekNULL(Dr1("hasil")) = "" Then
                        Dr1.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("data konversi satuan kirim tidak ada ")
                        Exit Sub
                    End If

                    Total_Transfer_Kecil = Dr1("hasil")
                Else
                    Dr1.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("data konversi satuan kirim tidak ada ")
                    Exit Sub
                End If
            End Using

            '========================
            '=     INSERT INDUK     =
            '========================
            SQL = "insert into EMI_TF_Material_To_Material (Kode_Perusahaan, Kode_Transfer, Tanggal, Jam, Kode_Stock_Owner, Kode_Barang_Awal, Kode_Barang_Tujuan, Total, Satuan, Total_Bags, Total_Barang, Satuan_Barang, Keterangan, UserID) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(TxtNo_Transaksi.Text) & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & arrSO(CmbSO_Asal.SelectedIndex) & "', "
            SQL = SQL & "'" & TxtKd_Barang.Text & "', '" & arrKdBrgTujuan(Cmb_Brg_Tujuan.SelectedIndex) & "', '" & HilangkanTanda(TxtTotalTransfer.Text) & "', '" & TxtSatuan.Text & "', '" & HilangkanTanda(TxtTotalTransferBags.Text) & "', '" & HilangkanTanda(Total_Transfer_Kecil) & "', "
            SQL = SQL & "'" & TxtSatuanKecil.Text & "', '" & TxtKeterangan.Text & "', '" & UserID & "')"
            ExecuteTrans(SQL)

            Dim jumlahAkhir As Double = 0

            For row As Integer = 0 To DGV_Data_TF.RowCount - 1

                get_grid_view(row)

                If dgv_CheckBox = True Then

                    If dgv_CheckBox = False Then
                        Continue For
                    End If

                    If HilangkanTanda(dgv_Jumlah) = "" Or dgv_JmlhBags = "" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Jumlah harus diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If dgv_FlagBlokSN = "Y" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("SN pada Pallet Di Block, Pallet Tidak Bisa di Transfer " & vbNewLine & " Barcode : " & dgv_Barcode, "Transfer Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    Dim JumlahBarangKecil As Double = 0
                    SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & TxtKd_Barang.Text & "', '" & TxtSatuan.Text & "',"
                    SQL = SQL & "'" & TxtSatuanKecil.Text & "', '" & HilangkanTanda(dgv_Jumlah) & "' ) as hasil"
                    Using Dr1 = OpenTrans(SQL)
                        If Dr1.Read Then
                            If General_Class.CekNULL(Dr1("hasil")) = "" Then
                                Dr1.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("data konversi satuan kirim tidak ada ")
                                Exit Sub
                            End If

                            JumlahBarangKecil = Dr1("hasil")
                        Else
                            Dr1.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("data konversi satuan kirim tidak ada ")
                            Exit Sub
                        End If
                    End Using

                    '=========================
                    '=     INSERT DETAIL     =
                    '=========================
                    Dim Tgl_Produksi As Date = DateTime.ParseExact(dgv_TglProduksi, "dd MMM yyyy", Globalization.CultureInfo.InvariantCulture)
                    Dim Tgl_Expired As Date = DateTime.ParseExact(dgv_TglExpired, "dd MMM yyyy", Globalization.CultureInfo.InvariantCulture)
                    SQL = "insert into EMI_TF_Material_To_Material_Detail (Kode_Perusahaan, Kode_Transfer, Tgl_Produksi, Tgl_Expired, Serial_Number_Awal, Jumlah_Barang, Satuan_Barang, Jumlah_Bags, Id_Warehouse_Awal, Nomor_Pallet_Awal) values "
                    SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(TxtNo_Transaksi.Text) & "', '" & Format(Tgl_Produksi, "yyyy-MM-dd") & "', '" & Format(Tgl_Expired, "yyyy-MM-dd") & "',  "
                    SQL = SQL & "'" & dgv_SerialNumber & "', '" & HilangkanTanda(JumlahBarangKecil) & "', '" & TxtSatuanKecil.Text & "', '" & HilangkanTanda(dgv_JmlhBags) & "', '" & dgv_IDWareHouse & "', '" & dgv_IDPallet & "')"
                    ExecuteTrans(SQL)

                End If
            Next


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            kosong()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub CmbSO_Asal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSO_Asal.SelectedIndexChanged
        DGV_Data_TF.Rows.Clear()
        TxtKd_Barang.Text = ""
        Txt_SO.Text = ""
        TxtNm_Barang.Text = ""

        Try
            OpenConn()
            get_no_faktur()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub DGV_Data_TF_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Data_TF.CellEndEdit

        get_grid_view(DGV_Data_TF.CurrentRow.Index)

        Dim currentRow = DGV_Data_TF.CurrentRow.Index
        Dim currentCell = DGV_Data_TF.CurrentCell.ColumnIndex

        Dim chkBox As String = DGV_Data_TF.CurrentRow.Cells(itemDgvCheckBox).Value
        Dim cellValue As String = DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value
        Dim cellValue2 As String = DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value

        If currentCell = itemDgvCheckBox Then
            Dim totalJmlh As Double = 0
            Dim totalBags As Double = 0

            For i As Integer = 0 To DGV_Data_TF.RowCount - 1
                get_grid_view(i)
                If dgv_CheckBox = True Then
                    totalJmlh = totalJmlh + Val(isNull(HilangkanTanda(DGV_Data_TF.Rows(i).Cells(itemDgvJumlah).Value)))
                    totalBags = totalBags + Val(isNull(HilangkanTanda(DGV_Data_TF.Rows(i).Cells(itemDgvBags).Value)))
                End If

                'If chkBox = "True" Then
                '    totalJmlh = totalJmlh + Val(isNull(DGV_Data_TF.Rows(i).Cells(itemDgvJumlah).Value))
                '    totalBags = totalBags + Val(isNull(DGV_Data_TF.Rows(i).Cells(itemDgvBags).Value))
                'Else
                '    Continue For
                'End If
            Next

            TxtTotalTransfer.Text = Format(totalJmlh, "N0")
            TxtTotalTransferBags.Text = Format(totalBags, "N0")

        End If

        'If chkBox = "True" Then
        '    DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).ReadOnly = False
        '    DGV_Data_TF.CurrentRow.Cells(itemDgvBags).ReadOnly = False
        'Else
        '    DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).ReadOnly = True
        '    DGV_Data_TF.CurrentRow.Cells(itemDgvBags).ReadOnly = True

        '    DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
        '    DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = ""

        'End If

        'If Not IsNumeric(cellValue) Then
        '    DGV_Data_TF.CurrentRow.Cells(itemDgvJumlah).Value = ""
        'End If
        'If Not IsNumeric(cellValue2) Then
        '    DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = ""
        'End If

    End Sub

    Public Shared Function isNull(ByVal xNullString As String) As String
        Try
            If String.IsNullOrWhiteSpace(xNullString) Then
                Return "0"
            Else
                Return xNullString
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return "0"
        End Try
    End Function
    Private Sub TxtKd_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtKd_Barang.KeyDown
        If e.KeyCode = Keys.Down Then
            If Lv_DetBarang.Items.Count = 0 Then Exit Sub
            Lv_DetBarang.Focus()
        End If
    End Sub

    Private Sub TxtKd_Barang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKd_Barang.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_Brg_Tujuan.Focus()
    End Sub


End Class