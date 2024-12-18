Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Emi_Adjustment_Dist2

    Dim arrSO, arrInisialFaktur As New ArrayList

    Dim Lvp_So, Lvp_KdBarang, Lvp_Nama As String
    Dim lv_KdBarang, lv_NamaBarang, lv_BarangSN, lv_Stock, lv_TglProduksi, lv_TglExpired, lv_KodeSO, lv_position, lv_idWarehouse, lv_satuanDisplay, lv_stockBags, lv_SatuanBarang As String

    Dim ItemLvp_So As Integer = 0
    Dim ItemLvp_KDBarang As Integer = 1
    Dim ItemLvp_Nama As Integer = 2

    Dim itemKdBarang As Integer = 0
    Dim itemNamaBarang As Integer = 1
    Dim itemSN As Integer = 2
    Dim itemStock As Integer = 3
    Dim itemSatuanDisplay As Integer = 4
    Dim itemStockBags As Integer = 5
    Dim itemTglProduksi As Integer = 6
    Dim itemTglExpired As Integer = 7
    Dim itemPosition As Integer = 8
    Dim itemKdSO As Integer = 9
    Dim itemIDWarehonse As Integer = 10
    Dim itemSatuanBarang As Integer = 11

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub



    Private Sub Emi_Adjustment_Dist2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Kosong()
        Initial_List_View()
    End Sub

    Private Sub Kosong()
        Cmb_So.Items.Clear()

        Txt_KdBarang.Text = String.Empty
        Txt_SO.Text = String.Empty
        Txt_NmBarang.Text = String.Empty
        Txt_SisaBags.Text = String.Empty
        Txt_AdjustBags.Text = String.Empty

        Txt_KdBarangAdj.Text = String.Empty
        Txt_SN.Text = String.Empty
        Txt_NmBarangAdj.Text = String.Empty
        Txt_SisaStock.Text = String.Empty
        Txt_HPP.Text = String.Empty
        Txt_JmlhAdj.Text = String.Empty
        Txt_Keterangan.Text = String.Empty
        Txt_Position.Text = String.Empty
        TextBox1.Text = String.Empty
        Id_Position.Text = String.Empty
        txt_satuan.Text = String.Empty
        Txt_SatuanAkhir.Text = String.Empty

        Dtp_Produksi.Value = DateTime.Now
        Dtp_Expred.Value = DateTime.Now

        Lv_BarangSNPallet.Items.Clear()
        Lv_PilihBarang.Items.Clear()

        Lv_PilihBarang.Visible = False
        Lv_PilihBarang.Location = New Point(1250, 108)

        'Cmb_SatuanStock.Items.Add("KG")
        'Cmb_SatuanStock.Items.Add("Gram")


        Try
            OpenConn()

            SQL = "select kode_stock_owner, keterangan, inisial_faktur from Stock_Owner_Gudang"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_So.Items.Add(Dr("keterangan")) : arrSO.Add(Dr("kode_stock_owner"))
                    arrInisialFaktur.Add(Dr("inisial_faktur"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub Get_Pilih_Barang_Data(ByVal index As Integer)

        Lvp_So = Lv_PilihBarang.Items(index).SubItems(ItemLvp_So).Text
        Lvp_KdBarang = Lv_PilihBarang.Items(index).SubItems(ItemLvp_KDBarang).Text
        Lvp_Nama = Lv_PilihBarang.Items(index).SubItems(ItemLvp_Nama).Text

    End Sub
    Private Sub Get_Data_BarangSN(ByVal index As Integer)

        lv_KdBarang = Lv_BarangSNPallet.Items(index).SubItems(itemKdBarang).Text
        lv_NamaBarang = Lv_BarangSNPallet.Items(index).SubItems(itemNamaBarang).Text
        lv_BarangSN = Lv_BarangSNPallet.Items(index).SubItems(itemSN).Text
        lv_Stock = Lv_BarangSNPallet.Items(index).SubItems(itemStock).Text
        lv_TglProduksi = Lv_BarangSNPallet.Items(index).SubItems(itemTglProduksi).Text
        lv_TglExpired = Lv_BarangSNPallet.Items(index).SubItems(itemTglExpired).Text
        lv_KodeSO = Lv_BarangSNPallet.Items(index).SubItems(itemKdSO).Text
        lv_position = Lv_BarangSNPallet.Items(index).SubItems(itemPosition).Text
        lv_idWarehouse = Lv_BarangSNPallet.Items(index).SubItems(itemIDWarehonse).Text
        lv_satuanDisplay = Lv_BarangSNPallet.Items(index).SubItems(itemSatuanDisplay).Text
        lv_stockBags = Lv_BarangSNPallet.Items(index).SubItems(itemStockBags).Text
        lv_SatuanBarang = Lv_BarangSNPallet.Items(index).SubItems(itemSatuanBarang).Text

    End Sub

    Private Sub Initial_List_View()

        Lv_PilihBarang.Columns.Add("Stock Owner", 200, HorizontalAlignment.Center)
        Lv_PilihBarang.Columns.Add("Kode Barang", 130, HorizontalAlignment.Center)
        Lv_PilihBarang.Columns.Add("Nama", 250, HorizontalAlignment.Center)
        Lv_PilihBarang.View = View.Details

        Lv_BarangSNPallet.Columns.Add("Kode Barang", 120, HorizontalAlignment.Center)
        Lv_BarangSNPallet.Columns.Add("Nama Barang", 200, HorizontalAlignment.Center)
        Lv_BarangSNPallet.Columns.Add("Serial Number", 200, HorizontalAlignment.Center)
        Lv_BarangSNPallet.Columns.Add("Stock", 90, HorizontalAlignment.Center)
        Lv_BarangSNPallet.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
        Lv_BarangSNPallet.Columns.Add("Bags", 90, HorizontalAlignment.Center)
        Lv_BarangSNPallet.Columns.Add("Tanggal Produksi", 150, HorizontalAlignment.Center)
        Lv_BarangSNPallet.Columns.Add("Tanggal Expired", 150, HorizontalAlignment.Center)
        Lv_BarangSNPallet.Columns.Add("Position", 150, HorizontalAlignment.Center)
        Lv_BarangSNPallet.Columns.Add("kodeSO", 0, HorizontalAlignment.Center)
        Lv_BarangSNPallet.Columns.Add("IdWarehonse", 0, HorizontalAlignment.Center)
        Lv_BarangSNPallet.Columns.Add("satuanBarang", 0, HorizontalAlignment.Center)
        Lv_BarangSNPallet.View = View.Details

    End Sub


    'FUNCTION LOAD LV
    Private Sub Load_Lv_SN(ByVal kdBarang As String)
        If kdBarang.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            Lv_BarangSNPallet.Items.Clear()

            SQL = "select a.Kode_Barang, b.nama, a.Serial_Number, a.Jumlah, a.Tgl_Produksi, a.Tgl_Expired, a.Kode_Stock_Owner, "
            SQL = SQL & "a.Id_Warehouse, c.Keterangan as position_pallet, d.satuan as Satuan_Display, isnull(a.jumlah_bags, 0) as stock_bags, b.Satuan as satuan_barang "
            SQL = SQL & "from Barang_SN a, barang b, View_Warehouse_Position c, Barang_Detail_Satuan d "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Barang=b.Kode_Barang and a.Kode_Perusahaan=b.Kode_Perusahaan  "
            SQL = SQL & "and a.Kode_Barang=d.Kode_barang and d.Flag_Tampil_Display='Y' and a.Id_Warehouse=c.Id_WMS_Warehouse_Position "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' and a.Kode_Barang='" & kdBarang & "' and a.Kode_Stock_Owner='" & arrSO(Cmb_So.SelectedIndex) & "' "
            SQL = SQL & "group by  a.Kode_Barang, b.nama, a.Serial_Number, a.Jumlah, a.Tgl_Produksi, a.Tgl_Expired, a.Kode_Stock_Owner, a.Id_Warehouse, c.Keterangan, d.satuan, a.jumlah_bags, b.Satuan "
            SQL = SQL & "order by a.Serial_Number"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_BarangSNPallet.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("nama"))
                    Lv.SubItems.Add(Dr("Serial_Number"))
                    Lv.SubItems.Add(Dr("Jumlah"))
                    Lv.SubItems.Add(Dr("Satuan_Display"))
                    Lv.SubItems.Add(Dr("stock_bags"))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Tgl_Produksi")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Tgl_Expired")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("position_pallet")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Kode_Stock_Owner")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Id_Warehouse")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("satuan_barang")))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub


    'FUNCTION HANDLE
    Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
        If Cmb_So.SelectedIndex = -1 Then Exit Sub

        If Txt_KdBarang.Text.Trim.Length = 0 Then
            Lv_PilihBarang.Visible = False
            Lv_PilihBarang.Location = New Point(1250, 108)

            Txt_SO.Text = String.Empty
            Txt_NmBarang.Text = String.Empty
            Exit Sub
        End If

        Try
            OpenConn()

            Lv_PilihBarang.Items.Clear()

            SQL = "select Kode_Stock_Owner, Kode_Barang, Nama from Barang "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & arrSO(Cmb_So.SelectedIndex) & "' "
            SQL = SQL & "and Nama like '" & Txt_KdBarang.Text & "%' "
            SQL = SQL & "group by Kode_Stock_Owner, Kode_Barang, Nama order by Nama "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_PilihBarang.Items.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            Lv_PilihBarang.Visible = True
            Lv_PilihBarang.Location = New Point(12, 108)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Lv_PilihBarang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_PilihBarang.DoubleClick
        If Lv_PilihBarang.Items.Count = 0 Then Exit Sub

        Get_Pilih_Barang_Data(Lv_PilihBarang.FocusedItem.Index)

        Txt_KdBarang.Text = Lvp_KdBarang
        Txt_SO.Text = Lvp_So
        Txt_NmBarang.Text = Lvp_Nama

        Lv_PilihBarang.Items.Clear()
        Lv_PilihBarang.Visible = False
        Lv_PilihBarang.Location = New Point(1250, 108)

        Load_Lv_SN(Lvp_KdBarang)
    End Sub

    Private Sub Lv_BarangSNPallet_DoubleClick(sender As Object, e As EventArgs) Handles Lv_BarangSNPallet.DoubleClick
        If Lv_BarangSNPallet.Items.Count = 0 Then Exit Sub

        Get_Data_BarangSN(Lv_BarangSNPallet.FocusedItem.Index)

        Txt_KdBarangAdj.Text = lv_KdBarang
        Txt_SN.Text = lv_BarangSN
        Txt_NmBarangAdj.Text = lv_NamaBarang
        Txt_SisaStock.Text = lv_Stock
        Txt_Position.Text = lv_position
        txt_satuan.Text = lv_satuanDisplay
        Txt_SisaBags.Text = lv_stockBags
        Txt_SatuanAkhir.Text = lv_SatuanBarang

        Id_Position.Text = lv_idWarehouse

        Dtp_Produksi.Value = If(Not String.IsNullOrEmpty(lv_TglProduksi), Format(DateTime.Parse(lv_TglProduksi), "yyyy-MM-dd"), DateTime.Now)
        Dtp_Expred.Value = If(Not String.IsNullOrEmpty(lv_TglExpired), Format(DateTime.Parse(lv_TglExpired), "yyyy-MM-dd"), DateTime.Now)

        'Dtp_Produksi.Value = lv_TglProduksi
        'Dtp_Expred.Value = lv_TglExpired

        Try
            OpenConn()

            SQL = "select Last_HPP from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and "
            SQL = SQL & "Kode_Stock_Owner='" & lv_KodeSO & "' and Kode_Barang='" & lv_KdBarang & "'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Txt_HPP.Text = Dr("Last_HPP")
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

        get_jam()

        If Txt_JmlhAdj.Text.Trim.Length = 0 Then
            MessageBox.Show("Jumlah Adjust harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_JmlhAdj.Focus() : Exit Sub
        ElseIf Txt_AdjustBags.Text.Trim.Length = 0 Then
            MessageBox.Show("Bags harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_AdjustBags.Focus() : Exit Sub
        ElseIf Txt_Keterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Keterangan.Focus() : Exit Sub
        ElseIf Format(DateTimePicker1.Value, "yyyyMM") <> Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyyMM") Then
            MessageBox.Show("Adjustment tidak boleh dibulan mundur!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DateTimePicker1.Focus()
            Exit Sub
        ElseIf Dtp_Produksi.Text = Dtp_Expred.Text Then
            MessageBox.Show("Tanggal Produksi Tidak Boleh Sama Dengan Tanggal Expire", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dtp_Produksi.Focus() : Exit Sub
        ElseIf Format(Dtp_Produksi.Value, "yyyy-MM-dd") > Format(Dtp_Expred.Value, "yyyy-MM-dd") Then
            MessageBox.Show("Tanggal Produksi Tidak Boleh Lebih Besar dari Tanggal Expired!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dtp_Produksi.Focus() : Exit Sub
        ElseIf Format(Dtp_Produksi.Value, "yyyy-MM-dd") > Format(tgl_skg, "yyyy-MM-dd") Then
            MessageBox.Show("Tanggal Produksi Tidak Boleh di Tanggal Maju!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dtp_Produksi.Focus() : Exit Sub
        End If


        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            'Dim nilai_kecil_Stock As Double = 0
            'SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & Txt_KdBarangAdj.Text & "', '" & txt_satuan.Text & "',"
            'SQL = SQL & "'" & Txt_SatuanAkhir.Text & "', '" & HilangkanTanda(Txt_JmlhAdj.Text) & "' ) as hasil"
            'Using Dr1 = OpenTrans(SQL)
            '    If Dr1.Read Then
            '        If General_Class.CekNULL(Dr1("hasil")) = "" Then
            '            Dr1.Close()
            '            CloseTrans()
            '            CloseConn()
            '            MessageBox.Show("data konversi satuan kirim tidak ada ")
            '            Exit Sub
            '        End If

            '        nilai_kecil_Stock = Dr1("hasil")
            '    Else
            '        Dr1.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("data konversi satuan kirim tidak ada ")
            '        Exit Sub
            '    End If
            'End Using



            '============================
            '=     CEK UPDATE STOCK     =
            '============================

            'CEK IS UPDATE STOCK < 0
            SQL = "select jumlah from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and "
            SQL = SQL & "Kode_Barang='" & Txt_KdBarangAdj.Text & "' and Serial_Number='" & Txt_SN.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("jumlah") + HilangkanTanda(Val(Txt_JmlhAdj.Text)) < 0 Then
                        MessageBox.Show("Proses adjustment akan membuat stock menjadi negatif, proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_JmlhAdj.Focus()
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("Barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    Exit Sub
                End If
            End Using


            '========================
            '=     UPDATE DATA      =
            '========================

            SQL = "UPDATE barang set Good_Stock = Good_Stock + " & HilangkanTanda(Txt_JmlhAdj.Text) & ", Jumlah_Bags = Jumlah_Bags + " & HilangkanTanda(Txt_AdjustBags.Text) & " "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and Kode_Stock_Owner='" & arrSO(Cmb_So.SelectedIndex) & "' and Kode_Barang='" & Txt_KdBarangAdj.Text & "' "
            ExecuteTrans(SQL)

            SQL = "UPDATE barang_SN set jumlah = jumlah + " & HilangkanTanda(Txt_JmlhAdj.Text) & ", Jumlah_Bags = Jumlah_Bags + " & HilangkanTanda(Txt_AdjustBags.Text) & " "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and Kode_Stock_Owner='" & arrSO(Cmb_So.SelectedIndex) & "' and Kode_Barang='" & Txt_KdBarangAdj.Text & "' "
            SQL = SQL & "and Serial_Number='" & Txt_SN.Text & "' "
            ExecuteTrans(SQL)



            '================================
            '=     CEK KESESUAIAN DATA      =
            '================================

            SQL = "select a.Kode_Barang, a.Nama, "
            SQL = SQL & "ISNULL(ROUND(sum(Good_Stock), 2), 0) as GoodStock, "
            SQL = SQL & "ISNULL(ROUND(sum(Jumlah_Bags), 2), 0) as Bags_Barang, "
            SQL = SQL & "ISNULL((select ROUND(sum(z.Jumlah),2) from Barang_SN z where  a.Kode_Perusahaan=z.Kode_Perusahaan "
            SQL = SQL & "and a.kode_barang=z.Kode_Barang and a.Kode_Stock_Owner=z.Kode_Stock_Owner), 0) as Jumlah_Sn, "
            SQL = SQL & "ISNULL((select ROUND(sum(z.Jumlah_Bags),2) from Barang_SN z where  a.Kode_Perusahaan=z.Kode_Perusahaan "
            SQL = SQL & "and a.kode_barang=z.Kode_Barang and a.Kode_Stock_Owner=z.Kode_Stock_Owner), 0) as Bags_Sn "
            SQL = SQL & "from barang a "
            SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' and a.Kode_Stock_Owner='" & arrSO(Cmb_So.SelectedIndex) & "' "
            SQL = SQL & "and a.Kode_Barang='" & Txt_KdBarangAdj.Text & "' "
            SQL = SQL & "group by a.Kode_Barang, a.Nama, a.Kode_Perusahaan, a.Kode_Stock_Owner "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        If .Rows(0).Item("GoodStock") <> .Rows(0).Item("Jumlah_Sn") Or .Rows(0).Item("Bags_Barang") <> .Rows(0).Item("Bags_Sn") Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using


            '========================
            '=     INSERT DATA      =
            '========================

            'GENERATE KODE ADJUSTMENT
            TextBox1.Text = FAdj & arrInisialFaktur.Item(Cmb_So.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy") & "-" &
                          General_Class.Get_Last_Number2("EMI_Adjustment", "kode_adjustment", JumlahDigit,
                          "Kode_perusahaan", KodePerusahaan,
                          "And", "substring(kode_adjustment,1," & Len(FAdj) + Len(arrInisialFaktur.Item(Cmb_So.SelectedIndex)) + 6 & ")", FAdj & arrInisialFaktur.Item(Cmb_So.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy")) &
                          Format(tgl_skg, "yyyyMMddHHmmdd")


            'INSERT KE EMI_Adjustment_Per_Pallet
            SQL = "insert into EMI_Adjustment_Per_Pallet (kode_perusahaan, kode_adjustment, tanggal, jam, kode_stock_owner, kode_barang, "
            SQL = SQL & "serial_number, id_warehouse, jumlah, Tf_Bags, keterangan, userID, harga_beli, tgl_input, tgl_produksi, tgl_expired) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & TextBox1.Text & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & arrSO(Cmb_So.SelectedIndex) & "', '" & Txt_KdBarangAdj.Text & "', "
            SQL = SQL & "'" & Txt_SN.Text & "', '" & Id_Position.Text & "', " & Val(Txt_JmlhAdj.Text) & ", " & Val(Txt_AdjustBags.Text) & " ,'" & Txt_Keterangan.Text & "', "
            SQL = SQL & "'" & UserID & "', '" & Txt_HPP.Text & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(Dtp_Produksi.Value, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(Dtp_Expred.Value, "yyyy-MM-dd") & "')"
            ExecuteTrans(SQL)


            Cmd.Transaction.Commit()
            MessageBox.Show("Berhasil Disimpan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Kosong()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub Cmb_So_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_So.SelectedIndexChanged

        If Cmb_So.Items.Count = 0 Then Exit Sub

        Txt_KdBarang.Text = String.Empty
        Txt_SO.Text = String.Empty
        Txt_NmBarang.Text = String.Empty

        Txt_KdBarang.Text = String.Empty
        Txt_SO.Text = String.Empty
        Txt_NmBarang.Text = String.Empty

        Txt_KdBarangAdj.Text = String.Empty
        Txt_SN.Text = String.Empty
        Txt_NmBarangAdj.Text = String.Empty
        Txt_SisaStock.Text = String.Empty
        Txt_HPP.Text = String.Empty
        Txt_JmlhAdj.Text = String.Empty
        Txt_Keterangan.Text = String.Empty
        Txt_Position.Text = String.Empty
        TextBox1.Text = String.Empty
        Id_Position.Text = String.Empty

        Dtp_Produksi.Value = DateTime.Now
        Dtp_Expred.Value = DateTime.Now

        Lv_BarangSNPallet.Items.Clear()
        Lv_PilihBarang.Items.Clear()

        Lv_PilihBarang.Visible = False
        Lv_PilihBarang.Location = New Point(1250, 108)

    End Sub




    Private Sub Txt_JmlhAdj_Leave(sender As Object, e As EventArgs) Handles Txt_JmlhAdj.Leave
        If Not IsNumeric(Txt_JmlhAdj.Text) Then Txt_JmlhAdj.Text = "" : Exit Sub
    End Sub
    Private Sub Txt_AdjustBags_Leave(sender As Object, e As EventArgs) Handles Txt_AdjustBags.Leave
        If Not IsNumeric(Txt_AdjustBags.Text) Then Txt_AdjustBags.Text = "" : Exit Sub
    End Sub
    Private Sub Txt_JmlhAdj_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_JmlhAdj.KeyPress
        If e.KeyChar = Chr(13) Then Txt_AdjustBags.Focus()
    End Sub
    Private Sub Txt_AdjustBags_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_AdjustBags.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Keterangan.Focus()
    End Sub
    Private Sub Txt_Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Keterangan.KeyPress
        If e.KeyChar = Chr(13) Then Dtp_Produksi.Focus()
    End Sub
    Private Sub Dtp_Produksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Dtp_Produksi.KeyPress
        If e.KeyChar = Chr(13) Then Dtp_Expred.Focus()
    End Sub
    Private Sub Dtp_Expred_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Dtp_Expred.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub
    Private Sub Btn_Simpan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Btn_Simpan.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan_Click(sender, e)
    End Sub

End Class


