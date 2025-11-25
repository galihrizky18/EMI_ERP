Public Class N_EMI_Transaksi_Waste_Product_Received

    Dim Lv_No_Split, Lv_No_Faktur, Lv_Tanggal, Lv_Lokasi, Lv_Keterangan, Lv_User As String

    Dim item_No_Split As Integer = 0
    Dim item_No_Faktur As Integer = 1
    Dim item_Tanggal As Integer = 2
    Dim item_Lokasi As Integer = 3
    Dim item_Keterangan As Integer = 4
    Dim item_User As Integer = 5

    Dim Random As New Random()

    Private Sub N_EMI_Transaksi_Waste_Product_Received_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("No Split", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("No Faktur", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Tanggal", 130, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Lokasi", 180, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Keterangan", 400, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("User", 130, HorizontalAlignment.Center)
        Lv_Data.View = View.Details

        Lv_Detail.Columns.Clear()
        Lv_Detail.Columns.Add("Lokasi", 130, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Barang", 200, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Barcode", 250, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Jumlah_Bags", 130, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_Detail.View = View.Details

        Kosong()

    End Sub

    Private Sub Kosong()

        Dtp_1.Value = Date.Now : Dtp_2.Value = Date.Now
        Txt_No_Split.Text = ""

        Get_Data()

    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        Get_Data(True)
    End Sub

    Private Sub Get_Data_Lv(ByVal index As Integer)

        Lv_No_Split = Lv_Data.Items(index).SubItems(item_No_Split).Text
        Lv_No_Faktur = Lv_Data.Items(index).SubItems(item_No_Faktur).Text
        Lv_Tanggal = Lv_Data.Items(index).SubItems(item_Tanggal).Text
        Lv_Lokasi = Lv_Data.Items(index).SubItems(item_Lokasi).Text
        Lv_Keterangan = Lv_Data.Items(index).SubItems(item_Keterangan).Text
        Lv_User = Lv_Data.Items(index).SubItems(item_User).Text
    End Sub

    Private Sub Get_Data(ByVal Optional Filter As Boolean = False)

        Try
            OpenConn()

            Lv_Data.Items.Clear() : Lv_Detail.Items.Clear()
            SQL = "select distinct f.No_Production_Order as No_Split, a.No_Faktur, a.Tanggal, a.Jam, a.Kode_Stock_Owner, a.Keterangan, a.UserID "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a, N_EMI_Transaksi_Transfer_Waste_Produk_Detail b, N_EMI_Transaksi_Transfer_Waste_Produk_Det c, Emi_Production_Results_Detail_Scrap d, Emi_Production_Results_HPP e, Emi_Production_Results f "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and d.Kode_Perusahaan = e.Kode_Perusahaan and e.Kode_Perusahaan = f.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and a.status is null and f.Status is null and a.Flag_Validasi is null "
            SQL = SQL & "and b.Flag_Timbang = 'T' and c.Selesai is null "
            SQL = SQL & "and c.Serial_Number_Awal = d.Serial_Number "
            SQL = SQL & "and d.No_Transaksi = e.No_Transaksi and d.Urut_HPP = e.Urut "
            SQL = SQL & "and e.No_Transaksi = f.No_Transaksi "
            SQL = SQL & "and a.Flag_Waste_Product = 'Y' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            If Filter Then
                SQL = SQL & "and a.Tanggal between '" & Format(Dtp_1.Value, "yyyy-MM-dd") & "' and '" & Format(Dtp_2.Value, "yyyy-MM-dd") & "' "
                If Txt_No_Split.Text.Trim.Length > 0 Then
                    SQL = SQL & "and f.No_Production_Order like '" & Txt_No_Split.Text & "%' "
                End If
            End If

            SQL = SQL & "union all "
            SQL = SQL & "select distinct e.No_Production_Order as No_Split, a.No_Faktur, a.Tanggal, a.Jam, a.Kode_Stock_Owner, a.Keterangan, a.UserID "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a, N_EMI_Transaksi_Transfer_Waste_Produk_Detail b, N_EMI_Transaksi_Transfer_Waste_Produk_Det c, Emi_Production_Results_Validation_Detail d, Emi_Production_Results_Validation e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan  "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and a.status is null and e.Status is null and a.Flag_Validasi is null "
            SQL = SQL & "and b.Flag_Timbang = 'T' and c.Selesai is null "
            SQL = SQL & "and c.Serial_Number_Awal = d.Serial_Number_Tujuan "
            SQL = SQL & "and d.No_Transaksi = e.No_Transaksi "
            SQL = SQL & "and a.Flag_Waste_Product = 'Y' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "

            If Filter Then
                SQL = SQL & "and a.Tanggal between '" & Format(Dtp_1.Value, "yyyy-MM-dd") & "' and '" & Format(Dtp_2.Value, "yyyy-MM-dd") & "' "
                If Txt_No_Split.Text.Trim.Length > 0 Then
                    SQL = SQL & "and e.No_Production_Order like '" & Txt_No_Split.Text & "%' "
                End If
            End If

            SQL = SQL & "union all "
            SQL = SQL & "select  distinct d.No_Split as No_Split, a.No_Faktur, a.Tanggal, a.Jam, a.Kode_Stock_Owner, a.Keterangan, a.UserID "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a, N_EMI_Transaksi_Transfer_Waste_Produk_Detail b, N_EMI_Transaksi_Transfer_Waste_Produk_Det c, "
            SQL = SQL & "EMI_Production_Results_Detail_Change_Packaging d, EMI_Production_Results_Detail_Change_Packaging_Detail e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan  "
            SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur  "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and d.No_Transaksi = e.No_Transaksi "
            SQL = SQL & "and c.Serial_Number_Awal = e.SN_Scrap "
            SQL = SQL & "and a.status is null and a.Status is null and a.Flag_Validasi is null "
            SQL = SQL & "and b.Flag_Timbang = 'T' "
            SQL = SQL & "and c.Selesai is null "
            SQL = SQL & "and a.Flag_Waste_Product = 'Y' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            If Filter Then
                SQL = SQL & "and a.Tanggal between '" & Format(Dtp_1.Value, "yyyy-MM-dd") & "' and '" & Format(Dtp_2.Value, "yyyy-MM-dd") & "' "
                If Txt_No_Split.Text.Trim.Length > 0 Then
                    SQL = SQL & "and d.No_Split like '" & Txt_No_Split.Text & "%' "
                End If
            End If


            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("No_Split"))
                    Lv.SubItems.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("UserID"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Lv_Data_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Data.SelectedIndexChanged
        If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Get_Data_Lv(Lv_Data.FocusedItem.Index)
            Dim No_Transaksi As String = Lv_No_Faktur
            Dim noSsplit As String = Lv_No_Split

            Lv_Detail.Items.Clear()
            SQL = "select a.No_Faktur, a.Kode_Stock_Owner, b.Kode_Barang, d.Nama as Nama_Barang, (e.Qr_Code + '-' + e.Kode_Unik_Berjalan) as Barcode, sum(c.Jumlah) as Jumlah, sum(c.Jumlah_Bags) as Jumlah_Bags, b.Satuan "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a, N_EMI_Transaksi_Transfer_Waste_Produk_Detail b, N_EMI_Transaksi_Transfer_Waste_Produk_Det c, barang d, Barang_SN e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and a.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and c.Serial_Number_Awal = e.Serial_Number "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & No_Transaksi & "' "
            SQL = SQL & "group by a.No_Faktur, a.Kode_Stock_Owner, b.Kode_Barang, d.Nama, (e.Qr_Code + '-' + e.Kode_Unik_Berjalan), b.Satuan "
            SQL = SQL & "order by (e.Qr_Code + '-' + e.Kode_Unik_Berjalan) "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail.Items.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Dr("Barcode"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N4"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_Bags"), "N0"))
                    Lv.SubItems.Add(Dr("Satuan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Lv_Data_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles Lv_Data.ItemCheck
        'If Lv_Data.Items.Count = 0 Then Exit Sub
        'If e.NewValue = CheckState.Unchecked Then Exit Sub

        'If Lv_Data.CheckedItems.Count > 0 Then
        '    Dim refItem As ListViewItem = Lv_Data.CheckedItems(0)
        '    Dim refValue As String = refItem.SubItems(item_Lokasi).Text
        '    Dim refValue2 As String = refItem.SubItems(item_No_Split).Text

        '    Dim currentItem As ListViewItem = Lv_Data.Items(e.Index)
        '    Dim currentValue As String = currentItem.SubItems(item_Lokasi).Text
        '    Dim currentValue2 As String = currentItem.SubItems(item_No_Split).Text

        '    If refValue2.ToUpper() <> currentValue2.ToUpper() Then
        '        e.NewValue = CheckState.Unchecked '

        '        MessageBox.Show("Hanya bisa memilih item dengan Split yang sama.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '        Exit Sub
        '    End If

        '    If refValue.ToUpper() <> currentValue.ToUpper() Then
        '        e.NewValue = CheckState.Unchecked '

        '        MessageBox.Show("Hanya bisa memilih item dengan lokasi yang sama.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '        Exit Sub
        '    End If
        'End If
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        Dim hasData As Boolean = False
        For i As Integer = 0 To Lv_Data.Items.Count - 1
            If Lv_Data.Items(i).Checked Then
                hasData = True
                Exit For
            End If
        Next

        If Not hasData Then
            MessageBox.Show("Pilih Dahulu Data yang Ingin di Proses", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Lv_Data.Focus() : Exit Sub
        End If

        Dim SelectedSplit As String = ""
        Dim SelectedFaktur As New ArrayList

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '===========================
            '=     CEK BUTTON ROLE     =
            '===========================
            If CekButtonRole("Transaksi_Waste_Product_Received") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Transaksi Waste Product Received", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            SelectedFaktur.Clear()

            For g As Integer = 0 To Lv_Data.Items.Count - 1
                If Not Lv_Data.Items(g).Checked Then Continue For

                Get_Data_Lv(g)

                SelectedSplit = Lv_No_Split
                SelectedFaktur.Add(Lv_No_Faktur)

                SQL = "select a.No_Faktur, a.Kode_Stock_Owner, a.Kode_Stock_Owner_Tujuan, b.Kode_Barang, d.Nama as Nama_Barang, (e.Qr_Code + '-' + e.Kode_Unik_Berjalan) as Barcode, sum(c.Jumlah) as Jumlah, sum(c.Jumlah_Bags) as Jumlah_Bags, b.Satuan "
                SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a, N_EMI_Transaksi_Transfer_Waste_Produk_Detail b, N_EMI_Transaksi_Transfer_Waste_Produk_Det c, barang d, Barang_SN e "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Perusahaan = e.Kode_Perusahaan "
                SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
                SQL = SQL & "and a.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
                SQL = SQL & "and c.Serial_Number_Awal = e.Serial_Number "
                SQL = SQL & "and a.Status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Faktur = '" & Lv_No_Faktur & "' "
                SQL = SQL & "group by a.No_Faktur, a.Kode_Stock_Owner, a.Kode_Stock_Owner_Tujuan, b.Kode_Barang, d.Nama, (e.Qr_Code + '-' + e.Kode_Unik_Berjalan), b.Satuan "
                SQL = SQL & "order by (e.Qr_Code + '-' + e.Kode_Unik_Berjalan)"
                Using Ds9 = BindingTrans(SQL)
                    If Ds9.Tables("MyTable").Rows.Count <> 0 Then
                        For p As Integer = 0 To Ds9.Tables("MyTable").Rows.Count - 1

                            Dim QrLama As String = ""
                            Dim expDate As String = ""
                            Dim batchLama As String = ""
                            Dim tglMsk As String = ""
                            Dim metodePengeluaranStock As String = ""
                            Dim GetDataKodeTransfer, GetDataLokasi, GetDataKdBrg, GetDataNmBrg, GetDataBrgSN, GetDataJmlEstimasi, GetDataSatuanBesar, GetDataSatuanKecil, GetDataUrutOto As String
                            Dim GetJumlahBags, GetRakTujuan, GetPalletTujuan, GetWarna As String
                            Dim SN As String = ""

                            Dim Gudang_Tujuan As String = ""

                            Dim arr_Sn As New ArrayList

                            Dim Barocde_Scan As String = Ds9.Tables("MyTable").Rows(p).Item("Barcode")

                            Dim ada_data As Boolean = False
                            SQL = "Select distinct c.serial_number from N_EMI_Transaksi_Transfer_Waste_Produk a, N_EMI_Transaksi_Transfer_Waste_Produk_Det b, barang_sn c where "
                            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And a.no_faktur = b.no_faktur "
                            SQL = SQL & "And a.status Is null And b.selesai Is null  "
                            SQL = SQL & "And b.kode_perusahaan=c.kode_Perusahaan And b.serial_number_awal=c.serial_number "
                            SQL = SQL & "And c.kode_perusahaan='" & KodePerusahaan & "' and c.qr_code+'-'+kode_unik_berjalan='" & Barocde_Scan & "' "
                            Using dr = OpenTrans(SQL)
                                Do While dr.Read
                                    ada_data = True
                                    arr_Sn.Add(dr("serial_number"))
                                Loop
                            End Using

                            If ada_data = False Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Data Barcode Tidak di temukan . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Kosong()
                                Exit Sub
                            End If

                            For Indxx = 0 To arr_Sn.Count - 1

                                'Ambil Data SN Berdasar Barcode
                                SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, b.Metode_Pengeluaran_Stok, a.Tgl_Masuk, a.Blok_SN "
                                SQL = SQL & "from barang_sn a, barang b "
                                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                                SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                                SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                                SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "and a.Jumlah = 0 "
                                SQL = SQL & "and a.qr_code + '-' + a.kode_unik_berjalan ='" & Barocde_Scan & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then

                                        If General_Class.CekNULL(Dr("Blok_SN")) = "Y" Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("SN Pada Pallet di Block, Validasi di Batalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Kosong()
                                            Exit Sub
                                        End If

                                        QrLama = General_Class.CekNULL(Dr("Qr_Code"))
                                        batchLama = General_Class.CekNULL(Dr("Batch_Number"))
                                        SN = Dr("serial_number")
                                        expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
                                        tglMsk = General_Class.CekNULL(Dr("tgl_masuk"))
                                        metodePengeluaranStock = General_Class.CekNULL(Dr("Metode_Pengeluaran_Stok"))
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Kosong()
                                        Exit Sub
                                    End If
                                End Using

                                'Cek data YG Mau di TF, Berdasar SN dr Barcode
                                SQL = "select a.No_Faktur, a.Kode_Stock_Owner, a.Kode_Stock_Owner_Tujuan, b.Kode_Barang, d.nama as Nama_Barang, c.Jumlah, c.Jumlah_Bags, b.Satuan, c.Serial_Number_Awal, "
                                SQL = SQL & "b.Satuan_Barang, c.Urut_Oto, c.Warna, c.Id_Wms_Tujuan "
                                SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a, N_EMI_Transaksi_Transfer_Waste_Produk_Detail b, N_EMI_Transaksi_Transfer_Waste_Produk_Det c, Barang d "
                                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan "
                                SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                                SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
                                SQL = SQL & "and a.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.kode_barang "
                                SQL = SQL & "and a.status is null and a.Flag_Validasi is null "
                                SQL = SQL & "and b.Flag_Timbang = 'T' and c.Selesai is null "
                                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and c.Serial_Number_Awal = '" & SN & "' "

                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then

                                        GetDataKodeTransfer = Dr("No_faktur")
                                        GetDataLokasi = Dr("Kode_Stock_Owner")
                                        GetDataKdBrg = Dr("Kode_Barang")
                                        GetDataNmBrg = Dr("Nama_Barang")
                                        GetDataBrgSN = Dr("Serial_Number_Awal")
                                        GetDataJmlEstimasi = HilangkanTanda(Format(Dr("Jumlah"), "N4"))
                                        GetJumlahBags = Dr("Jumlah_Bags")
                                        GetDataSatuanKecil = Dr("Satuan_Barang")
                                        GetDataSatuanBesar = Dr("Satuan")
                                        GetWarna = Dr("Warna")
                                        GetDataUrutOto = Dr("urut_oto")
                                        GetRakTujuan = Dr("Id_Wms_Tujuan")
                                        Gudang_Tujuan = Dr("Kode_Stock_Owner_Tujuan")

                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Kosong()
                                        Exit Sub
                                    End If
                                End Using

                                SQL = "select a.Status, c.Selesai, b.Flag_Timbang "
                                SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a, N_EMI_Transaksi_Transfer_Waste_Produk_Detail b, N_EMI_Transaksi_Transfer_Waste_Produk_Det c "
                                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.no_Faktur = b.No_Faktur and "
                                SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.no_Faktur = c.No_Faktur and b.urut_oto=c.urut_TF "
                                SQL = SQL & "and a.No_Faktur = '" & GetDataKodeTransfer & "' and c.urut_oto = '" & GetDataUrutOto & "'  "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then

                                        If General_Class.CekNULL(Dr("status")) <> "" Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Proses tidak bisa dilanjutkan, barang sudah dibatalkan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        ElseIf General_Class.CekNULL(Dr("selesai")) = "Y" Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi kesalahan, barang sudah selesai diproses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        ElseIf General_Class.CekNULL(Dr("Flag_Timbang")) = "Y" Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi kesalahan, ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                SQL = "Select Top(1) nomor_urut from view_warehouse_position_detail where "
                                SQL = SQL & "kode_Perusahaan ='" & KodePerusahaan & "' and kode_barang is null and "
                                SQL = SQL & "id_wms_warehouse_position = '" & GetRakTujuan & "' "
                                SQL = SQL & "order by nomor_urut "
                                Using dr = OpenTrans(SQL)
                                    If dr.Read Then
                                        GetPalletTujuan = dr("nomor_urut")
                                    Else
                                        dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("data Rak Sudah Penuh . . ! ! ")
                                        Exit Sub
                                    End If
                                End Using

                                '=============================================================================================
                                '=============================================================================================
                                '=======================================================================================

                                '====================================
                                '=       CONVERT SATUAN KECIL       =
                                '====================================
                                Dim nilai_kecildetail As Double = 0
                                SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & GetDataKdBrg & "', '" & GetDataSatuanBesar & "',"
                                SQL = SQL & "'" & GetDataSatuanKecil & "', '" & GetDataJmlEstimasi & "' ) as hasil"
                                Using Dr1 = OpenTrans(SQL)
                                    If Dr1.Read Then
                                        If General_Class.CekNULL(Dr1("hasil")) = "" Then
                                            Dr1.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("data konversi satuan kirim tidak ada ")
                                            Exit Sub
                                        End If

                                        nilai_kecildetail = Dr1("hasil")
                                    Else
                                        Dr1.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("data konversi satuan kirim tidak ada ")
                                        Exit Sub
                                    End If
                                End Using

                                '============================
                                '=       POTONG STOCK       =
                                '============================

                                Dim nilai_persediaan_min As Double = 0
                                SQL = "select round(dbo.get_hpp(serial_number) * " & nilai_kecildetail & ", 2) as rp_persediaan_min from barang_sn where "
                                SQL = SQL & "Kode_Stock_Owner='" & GetDataLokasi & "' and Kode_Barang='" & GetDataKdBrg & "' "
                                SQL = SQL & "and Serial_Number='" & GetDataBrgSN & "'"
                                Using dr = OpenTrans(SQL)
                                    If dr.Read Then
                                        nilai_persediaan_min = dr("rp_persediaan_min")
                                    Else
                                        dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                'Dim Nama As String = ""
                                ''Dim jumlahAkhir As Double = Val(dgv_GoodStock) - Val(dgv_Jumlah)
                                'SQL = "select Nama, Kode_Barang, round(good_stock,4) as good_stock, Jumlah_Bags from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetDataLokasi & "' "
                                'SQL = SQL & "and Kode_Barang='" & GetDataKdBrg & "' "
                                'Using dr = OpenTrans(SQL)
                                '    If dr.Read Then
                                '        Nama = dr("Kode_Barang")
                                '        If dr("good_stock") < nilai_kecildetail Then
                                '            dr.Close()
                                '            CloseTrans()
                                '            CloseConn()
                                '            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                '            Exit Sub
                                '        ElseIf dr("Jumlah_Bags") < GetJumlahBags Then
                                '            dr.Close()
                                '            CloseTrans()
                                '            CloseConn()
                                '            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                '            Exit Sub
                                '        Else
                                '            dr.Close()
                                '            SQL = "update barang set Good_Stock = Good_Stock - Round(" & nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags - " & GetJumlahBags & " "
                                '            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetDataLokasi & "' "
                                '            SQL = SQL & " and Kode_Barang='" & GetDataKdBrg & "'"
                                '            ExecuteTrans(SQL)
                                '        End If
                                '    Else
                                '        dr.Close()
                                '        CloseTrans()
                                '        CloseConn()
                                '        MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '        Exit Sub
                                '    End If
                                'End Using

                                'SQL = "select round(jumlah,4) as jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & GetDataLokasi & "' "
                                'SQL = SQL & "and Kode_Barang='" & GetDataKdBrg & "' "
                                'SQL = SQL & "and Serial_Number='" & GetDataBrgSN & "'"
                                'Using dr = OpenTrans(SQL)
                                '    If dr.Read Then
                                '        If dr("jumlah") < nilai_kecildetail Then
                                '            dr.Close()
                                '            CloseTrans()
                                '            CloseConn()
                                '            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                '            Exit Sub
                                '        ElseIf dr("Jumlah_Bags") < GetJumlahBags Then
                                '            dr.Close()
                                '            CloseTrans()
                                '            CloseConn()
                                '            MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                '            Exit Sub
                                '        Else
                                '            dr.Close()
                                '            SQL = "update barang_sn set jumlah = jumlah - Round(" & nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags - " & GetJumlahBags & " "
                                '            SQL = SQL & "where Kode_Stock_Owner='" & GetDataLokasi & "' and Kode_Barang='" & GetDataKdBrg & "' "
                                '            SQL = SQL & "and Serial_Number='" & GetDataBrgSN & "'"
                                '            ExecuteTrans(SQL)
                                '        End If
                                '    Else
                                '        dr.Close()
                                '        CloseTrans()
                                '        CloseConn()
                                '        MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '        Exit Sub
                                '    End If
                                'End Using

                                ''====================================
                                ''=       CEK KESESUAIAN STOCK       =
                                ''====================================
                                'SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                                'SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                                'SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                                'SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                                'SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                                'SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                                'SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & GetDataLokasi & "' "
                                'SQL = SQL & "AND a.Kode_Barang = '" & GetDataKdBrg & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                'SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                                'Using Ds = BindingTrans(SQL)
                                '    With Ds.Tables("MyTable")
                                '        If .Rows.Count <> 0 Then
                                '            If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
                                '                CloseTrans()
                                '                CloseConn()
                                '                MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                Exit Sub
                                '            End If
                                '        Else
                                '            CloseTrans()
                                '            CloseConn()
                                '            MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '            Exit Sub
                                '        End If
                                '    End With
                                'End Using

                                '==============================
                                '=       INSERT SN BARU       =
                                '==============================

                                Dim hargaIsn As String = ""
                                Dim namaBarang As String = ""
                                Dim warnaLama As String = ""

                                'Ambil Data Lama
                                SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.warna "
                                SQL = SQL & "from barang_sn a, barang b "
                                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                                SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                                SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                                SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "and a.Kode_Stock_Owner='" & GetDataLokasi & "' "
                                SQL = SQL & "and a.Kode_Barang ='" & GetDataKdBrg & "' "
                                SQL = SQL & "and a.Serial_Number='" & GetDataBrgSN & "' "
                                'SQL = SQL & "and a.Jumlah <> 0 "
                                Using Dr = OpenTrans(SQL)
                                    Do While Dr.Read
                                        hargaIsn = Get_Harga_SN(Dr("Serial_Number"))
                                        QrLama = General_Class.CekNULL(Dr("Qr_Code"))
                                        batchLama = General_Class.CekNULL(Dr("Batch_Number"))
                                        namaBarang = General_Class.CekNULL(Dr("Nama"))
                                        expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
                                        warnaLama = General_Class.CekNULL(Dr("warna"))
                                    Loop
                                End Using

                                'GENERATE SN BARU
                                Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                                Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                                Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

                                Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)

                                'INSERT BARANG SN BARU
                                SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah,  Jumlah_Bags, "
                                SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk, Blok_SN) "
                                SQL = SQL & "select Kode_Perusahaan, '" & Gudang_Tujuan & "', Kode_Barang, '" & SN_Baru & "', '" & nilai_kecildetail & "', " & GetJumlahBags & ", "
                                SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, '" & GetRakTujuan & "', id_Susunan , Qr_Code, '" & newKodeUnikBerjalan & "', "
                                SQL = SQL & "Kode_Unik_Asal, '" & GetPalletTujuan & "', batch_number, '" & warnaLama & "', Tgl_Masuk, 'Y' "
                                SQL = SQL & "from Barang_SN "
                                SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "and Kode_Stock_Owner='" & GetDataLokasi & "' "
                                SQL = SQL & "and Kode_Barang='" & GetDataKdBrg & "' "
                                SQL = SQL & "and Serial_Number='" & GetDataBrgSN & "' "
                                ExecuteTrans(SQL)

                                '============================
                                '=       TAMBAH STOCK       =
                                '============================

                                SQL = "update barang set Good_Stock= Good_Stock + Round(" & nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags + " & GetJumlahBags & " "
                                SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Gudang_Tujuan & "' "
                                SQL = SQL & " and Kode_Barang='" & GetDataKdBrg & "'"
                                ExecuteTrans(SQL)

                                'CEK KESESUAIAN STOCK
                                SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                                SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                                SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                                SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                                SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                                SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                                SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & Gudang_Tujuan & "' "
                                SQL = SQL & "AND a.Kode_Barang = '" & GetDataKdBrg & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                                Using Ds = BindingTrans(SQL)
                                    With Ds.Tables("MyTable")
                                        If .Rows.Count <> 0 Then
                                            If .Rows(0).Item("good_stock") <> .Rows(0).Item("Jumlah_sn") Or .Rows(0).Item("jumlah_bags_barang") <> .Rows(0).Item("jumlah_bags_sn") Then
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

#Region "Jurnal"

                                'dari
                                Dim inisial_faktur_dari As String = ""
                                Dim akun_persediaan_dari As String = ""
                                Dim akun_persediaan_tujuan As String = ""

                                SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
                                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & GetDataLokasi & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        'akun_persediaan_dari = Dr("persediaan")
                                        inisial_faktur_dari = Dr("inisial_faktur")
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                SQL = "select akun_gantung_waste_produk from stock_owner_gudang where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & Gudang_Tujuan & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        akun_persediaan_dari = Dr("akun_gantung_waste_produk")
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                SQL = "select c.akun_Persediaan "
                                SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                                SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                                SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                                SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and b.kode_stock_owner = '" & GetDataLokasi & "' and b.Kode_Barang='" & GetDataKdBrg & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        akun_persediaan_tujuan = Dr("akun_Persediaan")
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                                Dim Kode_voucher As String = ""
                                Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
                                Dim pagenumber As Integer = 1

                                SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                                SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                                SQL = SQL & "'" & Kode_voucher & "', "
                                SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                                SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                                SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock " & GetDataKodeTransfer & "', '', "
                                SQL = SQL & "'-', '" & UserID & "')"
                                ExecuteTrans(SQL)

                                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
                                          Strings.Mid(akun_persediaan_dari, 2, 1),
                                          Strings.Mid(Ganti(akun_persediaan_dari), 3),
                                          KodePerusahaan, KodeProyek, "Persedian " & GetDataKodeTransfer, "0", nilai_persediaan_min, pagenumber, GetDataLokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                                pagenumber = pagenumber + 1

                                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
                                         Strings.Mid(akun_persediaan_tujuan, 2, 1),
                                         Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
                                         KodePerusahaan, KodeProyek, "Persedian " & GetDataKodeTransfer, nilai_persediaan_min, "0", pagenumber, GetDataLokasi, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                ExecuteTrans(SQL)
                                pagenumber = pagenumber + 1

                                SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        If Dr("debit") <> Dr("kredit") Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

#End Region

                                SQL = "insert into N_EMI_Transaksi_Transfer_Waste_Produk_Det2(kode_perusahaan, No_faktur, Urut_Det, No_Pallet, "
                                SQL = SQL & "Serial_Number, Jumlah, UserID, Tanggal, Jam, Kode_Voucher, Jumlah_Bags) values( "
                                SQL = SQL & "'" & KodePerusahaan & "', '" & GetDataKodeTransfer & "', '" & GetDataUrutOto & "', "
                                SQL = SQL & "'" & GetPalletTujuan & "', '" & SN_Baru & "', '" & nilai_kecildetail & "', "
                                SQL = SQL & "'" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
                                SQL = SQL & "'" & Kode_voucher & "', '" & GetJumlahBags & "') "
                                ExecuteTrans(SQL)

                                SQL = "update N_EMI_Transaksi_Transfer_Waste_Produk_Det set  "
                                SQL = SQL & "Selesai = 'Y' "
                                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and urut_oto = '" & GetDataUrutOto & "' "
                                ExecuteTrans(SQL)

                            Next

                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi Kesalahan, Data Tidak Ditemukan")
                        Exit Sub
                    End If
                End Using

            Next

            'If True Then
            '    CloseTrans()
            '    CloseConn()
            '    MessageBox.Show("Tahan")
            '    Exit Sub
            'End If

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil Di Simpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        '=================================
        '=     CETAK FAKTUR TF STOCK     =
        '=================================
        'Try
        '    OpenConn()

        '    Dim CrDoc As New Object
        '    Dim kertas As String = ""

        '    Dim Kumpulan_Faktur As String = String.Join(", ", SelectedFaktur.Cast(Of Object)().Select(Function(x) $"'{Trim(x.ToString())}'"))

        '    Dim FakturList As String = String.Join(", ", SelectedFaktur.Cast(Of Object)().Select(Function(x) $"""{Trim(x.ToString())}"""))

        '    SQL = "select a.Kode_Perusahaan "
        '    SQL = SQL & "from N_EMI_View_Berita_Acara_Pemusnahan_Waste_Produk a where "
        '    SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and a.No_Production_Order = '" & Trim(SelectedSplit) & "' "
        '    SQL = SQL & "and a.No_Faktur in (" & Kumpulan_Faktur & ") "

        '    Using Ds = BindingTrans(SQL)
        '        If Ds.Tables("MyTable").Rows.Count <> 0 Then

        '            CrDoc = New N_EMI_CR_Berita_Acara_Pemusnahan_Waste_Produk
        '            kertas = "Faktur"

        '            With A_Place_For_Printing2
        '                CrDoc.SetDataSource(Ds)
        '                CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
        '                CrDoc.PrintOptions.PrinterName = ""
        '                CrDoc.RecordSelectionFormula = "{N_EMI_View_Berita_Acara_Pemusnahan_Waste_Produk.Kode_Perusahaan} = '" & Trim(KodePerusahaan) & "' " &
        '                    "AND {N_EMI_View_Berita_Acara_Pemusnahan_Waste_Produk.No_Production_Order} = '" & Trim(SelectedSplit) & "' " &
        '                    "AND {N_EMI_View_Berita_Acara_Pemusnahan_Waste_Produk.No_Faktur} IN [" & FakturList & "]"

        '                CrDoc.SummaryInfo.ReportTitle = "TF"
        '                .Text = "TF"
        '                .CrystalReportViewer1.ReportSource = CrDoc
        '                .Refresh()
        '                .Show()
        '            End With

        '            '============================================================================================================================================
        '            '============================================================================================================================================
        '            'CrDoc.SetDataSource(Ds)
        '            'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
        '            'CrDoc.PrintOptions.PrinterName = PrinterNameTS
        '            'CrDoc.RecordSelectionFormula = "{N_EMI_View_Berita_Acara_Pemusnahan_Waste_Produk.Kode_Perusahaan} = '" & Trim(KodePerusahaan) & "' " &
        '            '      "AND {N_EMI_View_Berita_Acara_Pemusnahan_Waste_Produk.No_Production_Order} = '" & Trim(SelectedSplit) & "' " &
        '            '      "AND {N_EMI_View_Berita_Acara_Pemusnahan_Waste_Produk.No_Faktur} IN [" & FakturList & "]"
        '            ''CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

        '            'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
        '            'doctoprint.PrinterSettings.PrinterName = PrinterNameTS
        '            ''doctoprint.DefaultPageSettings.Landscape = True
        '            'Dim rawKind As Integer
        '            'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        '            'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
        '            '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
        '            '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
        '            '        CrDoc.PrintOptions.PaperSize = rawKind
        '            '        Exit For
        '            '    End If
        '            'Next

        '            'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
        '            'CrDoc.PrintToPrinter(1, False, 1, 99)

        '            'MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        '        End If
        '    End Using

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        Kosong()

    End Sub

    Private Sub Dtp_1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Dtp_1.KeyPress
        If e.KeyChar = Chr(13) Then Dtp_2.Focus()
    End Sub

    Private Sub Dtp_2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Dtp_2.KeyPress
        If e.KeyChar = Chr(13) Then Txt_No_Split.Focus()
    End Sub

    Private Sub Txt_No_Split_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_Split.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

End Class