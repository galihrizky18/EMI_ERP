Public Class SD_Pallet_DO

    Dim dataSementara As New List(Of Dictionary(Of String, Object))


    Dim JudulForm As String = "Pallet Barang"

    Dim dgv_Lokasi, dgv_KodeBarang, dgv_SerialNumber, dgv_Nama, dgv_IDWareHouse, dgv_KodeRak As String
    Dim dgv_IDPallet, dgv_GoodStock, dgv_Satuan, dgv_Jumlah, dgv_RakTujuan, dgv_IDWarehouseTujuan, dgv_JmlhBags, dgv_Warna, dgv_JenisKemasan As String
    Dim dgv_IsiPerBags, dgv_SatuanIsiBags, dgv_TglProd, dgv_TglExp, dgv_KetWarna, dgv_Barcode, dgv_FlagBlokSN As String

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
    Dim itemDgvJumlah As Integer = 10
    Dim itemDgvBags As Integer = 11
    Dim itemDgvWarna As Integer = 12
    Dim itemJenisKemasan As Integer = 13
    Dim itemDGVIsiPerBags As Integer = 14
    Dim itemDGVSatuanIsiBags As Integer = 15
    Dim itemDGVTglProd As Integer = 16
    Dim itemDGVTglExp As Integer = 17
    Dim itemDGVKetWarna As Integer = 18
    Dim itemDGVBarcode As Integer = 19
    Dim itemDGVFlagBlokSN As Integer = 20

    Private Sub SD_Pallet_DO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

    End Sub

    Private Sub get_grid_view(ByVal index As Integer)
        dgv_Lokasi = DGV_Data_Pallet.Rows(index).Cells(itemDgvLokasi).Value
        dgv_KodeBarang = DGV_Data_Pallet.Rows(index).Cells(itemDgvKodeBarang).Value
        dgv_SerialNumber = DGV_Data_Pallet.Rows(index).Cells(itemDgvSerialNumber).Value
        dgv_Nama = DGV_Data_Pallet.Rows(index).Cells(itemDgvNama).Value
        dgv_IDWareHouse = DGV_Data_Pallet.Rows(index).Cells(itemDgvIDWareHose).Value
        dgv_KodeRak = DGV_Data_Pallet.Rows(index).Cells(itemDgvKodeRak).Value
        dgv_IDPallet = DGV_Data_Pallet.Rows(index).Cells(itemDgvIDPallet).Value
        dgv_GoodStock = DGV_Data_Pallet.Rows(index).Cells(itemDgvGoodStock).Value
        dgv_Satuan = DGV_Data_Pallet.Rows(index).Cells(itemDgvSatuan).Value
        dgv_Jumlah = If(General_Class.CekNULL(DGV_Data_Pallet.Rows(index).Cells(itemDgvJumlah).Value) = "", "0", DGV_Data_Pallet.Rows(index).Cells(itemDgvJumlah).Value)
        dgv_JmlhBags = DGV_Data_Pallet.Rows(index).Cells(itemDgvBags).Value
        dgv_Warna = DGV_Data_Pallet.Rows(index).Cells(itemDgvWarna).Value
        dgv_JenisKemasan = DGV_Data_Pallet.Rows(index).Cells(itemJenisKemasan).Value
        dgv_IsiPerBags = If(General_Class.CekNULL(DGV_Data_Pallet.Rows(index).Cells(itemDGVIsiPerBags).Value) = "", "0", DGV_Data_Pallet.Rows(index).Cells(itemDGVIsiPerBags).Value)
        dgv_SatuanIsiBags = DGV_Data_Pallet.Rows(index).Cells(itemDGVSatuanIsiBags).Value
        dgv_TglProd = DGV_Data_Pallet.Rows(index).Cells(itemDGVTglProd).Value
        dgv_TglExp = DGV_Data_Pallet.Rows(index).Cells(itemDGVTglExp).Value
        dgv_KetWarna = DGV_Data_Pallet.Rows(index).Cells(itemDGVKetWarna).Value
        dgv_Barcode = DGV_Data_Pallet.Rows(index).Cells(itemDGVBarcode).Value
        dgv_FlagBlokSN = DGV_Data_Pallet.Rows(index).Cells(itemDGVFlagBlokSN).Value

    End Sub

    Public Sub kosong()
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        'Txt_KdSO.Text = ""
        'Txt_KdBarang.Text = ""
        'Txt_NmBarang.Text = ""
        'Txt_QR.Text = ""
        'Txt_JmlhReq.Text = ""
        'Txt_Sisa.Text = ""
        'Txt_Total.Text = ""
        'Txt_NoPenjualan.Text = ""

        DGV_Data_Pallet.Columns(itemDgvWarna).DisplayIndex = 6
        DGV_Data_Pallet.Columns(itemDGVKetWarna).DisplayIndex = 4
        DGV_Data_Pallet.Columns(itemDGVTglExp).DisplayIndex = 5
        DGV_Data_Pallet.Columns(itemDGVTglProd).DisplayIndex = 6
        DGV_Data_Pallet.Columns(itemDGVBarcode).DisplayIndex = 3

        dataSementara.Clear()
        LoadDataPallet()

    End Sub

    Private Sub LoadDataPallet()
        If Txt_KdBarang.Text.Trim.Length = 0 Or Txt_KdSO.Text.Trim.Length = 0 Then
            MessageBox.Show("Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Close()
        End If

        Try
            OpenConn()

            Dim rows As Integer = 0

            DGV_Data_Pallet.Rows.Clear()
            SQL = "Select a.Kode_Stock_Owner, a.Kode_Barang, '-' as Serial_Number, b.Nama, "
            SQL = SQL & "a.Id_Warehouse, c.Keterangan As kode_rak, a.Id_Nametag_pallet, "
            SQL = SQL & "dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, b.satuan, "
            SQL = SQL & "(select top 1 satuan from Barang_Detail_Satuan where Kode_Perusahaan = a.Kode_Perusahaan and Kode_barang = a.Kode_Barang and Flag_Tampil_Display = 'Y'), "
            SQL = SQL & "sum(a.jumlah)) as jumlah_Besar, "
            SQL = SQL & "(select top 1 satuan from Barang_Detail_Satuan where Kode_Perusahaan = a.Kode_Perusahaan and Kode_barang = a.Kode_Barang and Flag_Tampil_Display = 'Y') as Satuan_Besar, "
            SQL = SQL & "sum(a.Jumlah) as Jumlah, b.satuan as Satuan_Kecil, a.nomor_pallet, isNull(sum(a.Jumlah_Bags), 0) As stock_bags, a.warna, b.Metode_Pengeluaran_Stok, "
            SQL = SQL & "b.Jenis_Kemasan, isnull(b.Isi_Per_Bags,0) as Isi_Per_Bags, b.Satuan_Isi_Bags, a.Tgl_Expired, a.Tgl_Produksi, "
            SQL = SQL & "isNull((select x.keterangan from emi_master_warna x "
            SQL = SQL & "where x.kode_Perusahaan = a.kode_Perusahaan And x.kode_warna = a.warna),NULL) As Ket_Warna, "
            SQL = SQL & "(a.Qr_Code + '-' + a.Kode_Unik_Berjalan) as Barcode, a.Blok_SN "

            'SQL = SQL & "isnull(( "
            'SQL = SQL & "select sum(z.Jumlah) as Jumlah "
            'SQL = SQL & "from Emi_DO_Pallet_Sementara z, Barang_SN x "
            'SQL = SQL & "where z.kode_perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = a.Kode_Perusahaan "
            'SQL = SQL & "and z.serial_number = x.Serial_Number "
            'SQL = SQL & "and z.serial_number = x.Serial_Number "
            'SQL = SQL & "and a.Serial_Number = x.Serial_Number "
            'SQL = SQL & "and z.userid = '" & UserID & "' "
            'SQL = SQL & "group by (x.Qr_Code+'-'+x.kode_unik_berjalan) "
            'SQL = SQL & "), 0) as Jumlah_Input, "

            'SQL = SQL & "isnull(( "
            'SQL = SQL & "select sum(z.Bags) as Jumlah "
            'SQL = SQL & "from Emi_DO_Pallet_Sementara z, Barang_SN x "
            'SQL = SQL & "where z.kode_perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = a.Kode_Perusahaan "
            'SQL = SQL & "and z.serial_number = x.Serial_Number "
            'SQL = SQL & "and z.serial_number = x.Serial_Number "
            'SQL = SQL & "and a.Serial_Number = x.Serial_Number "
            'SQL = SQL & "and z.userid = '" & UserID & "' "
            'SQL = SQL & "group by (x.Qr_Code+'-'+x.kode_unik_berjalan) "
            'SQL = SQL & "), 0) as Bags_Input "


            SQL = SQL & "From barang_sn a, barang b, View_Warehouse_Position c "
            SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "And a.Kode_Barang = b.Kode_Barang And a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "And a.Id_Warehouse = c.Id_WMS_Warehouse_Position "
            SQL = SQL & "And a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            SQL = SQL & "And b.Kode_Stock_Owner ='" & Txt_KdSO.Text & "' "
            SQL = SQL & "And b.Kode_Barang='" & Txt_KdBarang.Text & "' "
            SQL = SQL & "And a.Jumlah <> 0 "
            SQL = SQL & "group by a.kode_perusahaan, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama, a.Id_Warehouse, c.Keterangan, a.Id_Nametag_pallet, "
            SQL = SQL & "b.satuan, a.nomor_pallet, a.warna, b.Metode_Pengeluaran_Stok, b.Jenis_Kemasan, b.Isi_Per_Bags, b.Satuan_Isi_Bags, "
            SQL = SQL & "a.Tgl_Expired, a.Tgl_Produksi, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan), a.blok_sn, a.tgl_masuk  "
            SQL = SQL & "order by case "
            SQL = SQL & "when Metode_Pengeluaran_Stok='FIFO' then a.Tgl_Masuk "
            SQL = SQL & "Else a.Tgl_Expired End "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            DGV_Data_Pallet.Rows.Add(1)
                            DGV_Data_Pallet.Rows(rows).Cells(itemDgvLokasi).Value = General_Class.CekNULL(.Rows(i).Item("Kode_Stock_Owner"))
                            DGV_Data_Pallet.Rows(rows).Cells(itemDgvKodeBarang).Value = General_Class.CekNULL(.Rows(i).Item("Kode_Barang"))
                            DGV_Data_Pallet.Rows(rows).Cells(itemDgvSerialNumber).Value = General_Class.CekNULL(.Rows(i).Item("Serial_Number"))
                            DGV_Data_Pallet.Rows(rows).Cells(itemDgvNama).Value = General_Class.CekNULL(.Rows(i).Item("Nama"))
                            DGV_Data_Pallet.Rows(rows).Cells(itemDgvIDWareHose).Value = General_Class.CekNULL(.Rows(i).Item("Id_Warehouse"))
                            DGV_Data_Pallet.Rows(rows).Cells(itemDgvKodeRak).Value = General_Class.CekNULL(.Rows(i).Item("kode_rak"))
                            DGV_Data_Pallet.Rows(rows).Cells(itemDgvIDPallet).Value = General_Class.CekNULL(.Rows(i).Item("nomor_pallet"))
                            DGV_Data_Pallet.Rows(rows).Cells(itemDgvGoodStock).Value = If(General_Class.CekNULL(.Rows(i).Item("jumlah")) = "", "", Format(.Rows(i).Item("jumlah"), "N2"))
                            DGV_Data_Pallet.Rows(rows).Cells(itemDgvStockBags).Value = If(General_Class.CekNULL(.Rows(i).Item("stock_bags")) = "", "", Format(.Rows(i).Item("stock_bags"), "N0"))
                            DGV_Data_Pallet.Rows(rows).Cells(itemDgvWarna).Value = General_Class.CekNULL(.Rows(i).Item("warna"))
                            DGV_Data_Pallet.Rows(rows).Cells(itemJenisKemasan).Value = General_Class.CekNULL(.Rows(i).Item("Jenis_Kemasan"))
                            DGV_Data_Pallet.Rows(rows).Cells(itemDGVIsiPerBags).Value = General_Class.CekNULL(.Rows(i).Item("Isi_Per_Bags"))
                            DGV_Data_Pallet.Rows(rows).Cells(itemDGVSatuanIsiBags).Value = General_Class.CekNULL(.Rows(i).Item("Satuan_Isi_Bags"))
                            DGV_Data_Pallet.Rows(rows).Cells(itemDGVKetWarna).Value = General_Class.CekNULL(.Rows(i).Item("Ket_Warna"))
                            DGV_Data_Pallet.Rows(rows).Cells(itemDGVTglProd).Value = If(General_Class.CekNULL(.Rows(i).Item("Tgl_Produksi")) = "", "", Format(.Rows(i).Item("Tgl_Produksi"), "dd MMM yyyy"))
                            DGV_Data_Pallet.Rows(rows).Cells(itemDgvSatuan).Value = General_Class.CekNULL(.Rows(i).Item("Satuan_Besar"))

                            If .Rows(i).Item("Jenis_Kemasan").ToString.ToUpper = "ORIGINAL BAGS" Then
                                DGV_Data_Pallet.Rows(rows).Cells(itemDgvJumlah).ReadOnly = True
                                DGV_Data_Pallet.Rows(rows).Cells(itemDgvBags).ReadOnly = False

                                DGV_Data_Pallet.Rows(rows).Cells(itemDgvBags).Style.BackColor = Color.LightGray
                            Else
                                DGV_Data_Pallet.Rows(rows).Cells(itemDgvJumlah).ReadOnly = False
                                DGV_Data_Pallet.Rows(rows).Cells(itemDgvBags).ReadOnly = False

                                DGV_Data_Pallet.Rows(rows).Cells(itemDgvJumlah).Style.BackColor = Color.LightGray
                                DGV_Data_Pallet.Rows(rows).Cells(itemDgvBags).Style.BackColor = Color.LightGray
                            End If

                            If .Rows(i).Item("Metode_Pengeluaran_Stok").ToString.ToUpper = "FIFO" Then
                                DGV_Data_Pallet.Rows(rows).Cells(itemDGVTglExp).Value = "-"
                            Else
                                DGV_Data_Pallet.Rows(rows).Cells(itemDGVTglExp).Value = If(General_Class.CekNULL(.Rows(i).Item("Tgl_Expired")) = "", "", Format(.Rows(i).Item("Tgl_Expired"), "dd MMM yyyy"))
                            End If

                            DGV_Data_Pallet.Rows(rows).Cells(itemDGVBarcode).Value = .Rows(i).Item("Barcode")
                            DGV_Data_Pallet.Rows(rows).Cells(itemDGVFlagBlokSN).Value = General_Class.CekNULL(.Rows(i).Item("Blok_SN"))

                            '===============================
                            '=     CEK TABEL SEMENTARA     =
                            '===============================
                            'SQL = "select No_FakturPenjualan, Kd_Barang, Serial_Number, Jumlah, Bags "
                            'SQL = SQL & "from Emi_DO_Pallet_Sementara "
                            'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            'SQL = SQL & "and No_FakturPenjualan = '" & Txt_NoPenjualan.Text & "' "
                            'SQL = SQL & "and Kd_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                            'SQL = SQL & "and Serial_Number = '" & .Rows(i).Item("Serial_Number") & "' "
                            'SQL = SQL & "and userid = '" & UserID & "' "

                            SQL = "select sum(a.Jumlah) as Jumlah, sum(a.Bags) as Bags "
                            SQL = SQL & "from Emi_DO_Pallet_Sementara a, Barang_SN b "
                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                            SQL = SQL & "and a.Serial_Number = b.Serial_Number "
                            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and a.No_FakturPenjualan = '" & Txt_NoPenjualan.Text & "' "
                            SQL = SQL & "and a.Kd_Barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                            SQL = SQL & "and (b.Qr_Code+'-'+b.Kode_Unik_Berjalan) = '" & .Rows(i).Item("Barcode") & "' "
                            SQL = SQL & "and a.userid = '" & UserID & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    DGV_Data_Pallet.Rows(rows).Cells(itemDgvJumlah).Value = Format(If(General_Class.CekNULL(Dr("Jumlah")) = "", 0, Dr("Jumlah")), "N2")
                                    DGV_Data_Pallet.Rows(rows).Cells(itemDgvBags).Value = Format(If(General_Class.CekNULL(Dr("Bags")) = "", 0, Dr("Bags")), "N2")
                                Else
                                    DGV_Data_Pallet.Rows(rows).Cells(itemDgvJumlah).Value = 0
                                    DGV_Data_Pallet.Rows(rows).Cells(itemDgvBags).Value = 0
                                End If
                            End Using

                            'DGV_Data_Pallet.Rows(rows).Cells(itemDgvJumlah).Value = If(General_Class.CekNULL(.Rows(i).Item("Jumlah_Input")) = "", 0, Format(.Rows(i).Item("Jumlah_Input"), "N2"))
                            'DGV_Data_Pallet.Rows(rows).Cells(itemDgvBags).Value = If(General_Class.CekNULL(.Rows(i).Item("Bags_Input")) = "", 0, Format(.Rows(i).Item("Jumlah_Input"), "N2"))

                            rows = rows + 1

                        Next
                    End If
                End With
            End Using

            HitungGrand()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub DGV_Data_Pallet_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Data_Pallet.CellClick
        'cek apakah yang di klik adalah header
        If e.RowIndex = -1 Then Exit Sub

        Dim currentCell As Integer = DGV_Data_Pallet.CurrentCell.ColumnIndex
        Dim currentRow As Integer = DGV_Data_Pallet.CurrentRow.Index

        If currentCell = itemDgvJumlah Or currentCell = itemDgvBags Then
            Dim cellValue As Object = HilangkanTanda(DGV_Data_Pallet.Rows(currentRow).Cells(currentCell).Value)

            DGV_Data_Pallet.Rows(currentRow).DefaultCellStyle.BackColor = Color.White
            Dim cellKuantity As String = HilangkanTanda(DGV_Data_Pallet.CurrentCell.Value)

            If cellKuantity = "" Then
                Exit Sub
            End If

            Dim cleanedStr As String = HilangkanTanda(cellKuantity)
            Dim nilai As Decimal = Decimal.Parse(cleanedStr)

            DGV_Data_Pallet.Rows(currentRow).Cells(currentCell).Value = nilai

        End If
    End Sub

    Private Sub DGV_Data_Pallet_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Data_Pallet.CellEndEdit
        If DGV_Data_Pallet.Rows.Count = 0 Then Exit Sub

        Dim indexRow As Integer = DGV_Data_Pallet.CurrentRow.Index

        Dim currentColumn As Integer = DGV_Data_Pallet.CurrentCell.ColumnIndex
        Dim currentRow As Integer = DGV_Data_Pallet.CurrentRow.Index
        Dim cellValue As Object = DGV_Data_Pallet.CurrentRow.Cells(currentColumn).Value

        Dim TempArray As New ArrayList

        TempArray.Clear()
        For i As Integer = 0 To DGV_Data_Pallet.Columns.Count - 1

            If i <> Val(itemDgvJumlah) AndAlso i <> Val(itemDgvBags) AndAlso i <> Val(itemDGVTglExp) AndAlso i <> Val(itemDGVFlagBlokSN) Then
                TempArray.Add(DGV_Data_Pallet.Rows(indexRow).Cells(i).Value)
            End If

        Next
        If TempArray.Contains("") Then
            MessageBox.Show("Terdapat Data Pada Barang yang Belum Lengkap")
            DGV_Data_Pallet.CurrentRow.Cells(itemDgvJumlah).Value = ""
            DGV_Data_Pallet.CurrentRow.Cells(itemDgvBags).Value = ""
            Exit Sub
        End If

        If DGV_Data_Pallet.CurrentRow.Cells(itemDgvWarna).Value.ToString.ToUpper <> "HIJAU" Then
            DGV_Data_Pallet.CurrentRow.Cells(itemDgvJumlah).Value = ""
            DGV_Data_Pallet.CurrentRow.Cells(itemDgvBags).Value = ""
            MessageBox.Show("Data Tidak bisa di kirim . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If currentColumn = itemDgvBags OrElse currentColumn = itemDgvJumlah Then
            If Not IsNumeric(cellValue) Then
                DGV_Data_Pallet.CurrentRow.Cells(itemDgvBags).Value = ""
                DGV_Data_Pallet.CurrentRow.Cells(itemDgvJumlah).Value = ""
                Exit Sub
            End If
        End If

        If DGV_Data_Pallet.CurrentRow.Cells(itemJenisKemasan).Value.ToString.ToUpper = "ORIGINAL BAGS" Then
            DGV_Data_Pallet.CurrentRow.Cells(itemDgvJumlah).ReadOnly = True

            If Not DGV_Data_Pallet.CurrentRow.Cells(itemDgvBags).Value = "" Then

                Dim stockBags As Double = Val(HilangkanTanda(DGV_Data_Pallet.CurrentRow.Cells(itemDgvStockBags).Value))
                Dim jumlahInputBags As Double = Val(HilangkanTanda(DGV_Data_Pallet.CurrentRow.Cells(itemDgvBags).Value))
                Dim isiPerbags As Double = Val(HilangkanTanda(DGV_Data_Pallet.CurrentRow.Cells(itemDGVIsiPerBags).Value))
                Dim jumlahStock As Double = Val(HilangkanTanda(DGV_Data_Pallet.CurrentRow.Cells(itemDgvGoodStock).Value))

                'cek apakah input melebihi
                If jumlahInputBags > stockBags Then
                    MessageBox.Show("Bags Tidak Boleh Melebihi Stock Bags", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    DGV_Data_Pallet.CurrentRow.Cells(itemDgvJumlah).Value = ""
                    DGV_Data_Pallet.CurrentRow.Cells(itemDgvBags).Value = ""
                    Exit Sub
                End If

                Dim valueJumlah As Double = isiPerbags * jumlahInputBags

                DGV_Data_Pallet.CurrentRow.Cells(itemDgvJumlah).Value = Format(valueJumlah, "N2")
                If valueJumlah > jumlahStock Then
                    DGV_Data_Pallet.CurrentRow.Cells(itemDgvJumlah).Value = Format(jumlahStock, "N2")
                    'DGV_Data_TF.CurrentRow.Cells(itemDgvBags).Value = Math.Floor(jumlahStock / isiPerbags)
                Else
                    DGV_Data_Pallet.CurrentRow.Cells(itemDgvJumlah).Value = Format(valueJumlah, "N2")
                End If

            End If
        Else
            DGV_Data_Pallet.CurrentRow.Cells(itemDgvJumlah).ReadOnly = False

            Dim jumlahStock As Double = Val(HilangkanTanda(DGV_Data_Pallet.CurrentRow.Cells(itemDgvGoodStock).Value))
            Dim jumlahInput As Double = Val(HilangkanTanda(DGV_Data_Pallet.CurrentRow.Cells(itemDgvJumlah).Value))
            Dim stockBags As Double = Val(HilangkanTanda(DGV_Data_Pallet.CurrentRow.Cells(itemDgvStockBags).Value))
            Dim jumlahInputBags As Double = Val(HilangkanTanda(DGV_Data_Pallet.CurrentRow.Cells(itemDgvBags).Value))

            If jumlahInput > jumlahStock Then
                MessageBox.Show("Jumlah Tidak Boleh Melebihi Stock ", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                DGV_Data_Pallet.CurrentRow.Cells(itemDgvJumlah).Value = ""
                DGV_Data_Pallet.CurrentRow.Cells(itemDgvBags).Value = ""
                Exit Sub
            End If

            'cek apakah input melebihi
            If jumlahInputBags > stockBags Then
                MessageBox.Show("Bags Tidak Boleh Melebihi Stock Bags", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                DGV_Data_Pallet.CurrentRow.Cells(itemDgvBags).Value = ""
                Exit Sub
            End If

        End If

        DGV_Data_Pallet.CurrentRow.Cells(itemDgvBags).ReadOnly = False

        If currentColumn = itemDgvBags OrElse currentColumn = itemDgvJumlah Then

            Dim jumlahValue As Object = HilangkanTanda(DGV_Data_Pallet.CurrentRow.Cells(itemDgvJumlah).Value)
            If jumlahValue IsNot Nothing AndAlso IsNumeric(jumlahValue) Then
                DGV_Data_Pallet.CurrentRow.Cells(itemDgvJumlah).Value = Convert.ToDecimal(jumlahValue).ToString("N2")
            Else
                DGV_Data_Pallet.CurrentRow.Cells(itemDgvJumlah).Value = ""
            End If

            Dim bagsValue As Object = HilangkanTanda(DGV_Data_Pallet.CurrentRow.Cells(itemDgvBags).Value)
            If bagsValue IsNot Nothing AndAlso IsNumeric(bagsValue) Then
                DGV_Data_Pallet.CurrentRow.Cells(itemDgvBags).Value = Convert.ToDecimal(bagsValue).ToString("N2")
            Else
                DGV_Data_Pallet.CurrentRow.Cells(itemDgvBags).Value = ""
            End If
        End If

        HitungGrand()
    End Sub

    Private Sub DGV_Data_Pallet_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Data_Pallet.CellLeave
        If e.RowIndex = -1 Then Exit Sub

        Dim currentCell As Integer = DGV_Data_Pallet.CurrentCell.ColumnIndex
        Dim currentRow As Integer = DGV_Data_Pallet.CurrentRow.Index

        If currentCell = itemDgvBags Or currentCell = itemDgvJumlah Then
            Dim cellKuantity As String = DGV_Data_Pallet.CurrentCell.Value

            If Not String.IsNullOrEmpty(cellKuantity) Then

                Dim nilai As Decimal = Decimal.Parse(cellKuantity)
                Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

                DGV_Data_Pallet.CurrentCell.Value = formattedValue
            End If
        End If
    End Sub

    Private Sub Txt_QR_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_QR.KeyPress
        If e.KeyChar = Chr(13) Then

            If Txt_QR.Text.Trim.Length <> 0 Then
                Btn_Scan_Click(Me, Nothing)
            End If

        End If
    End Sub

    Private Sub Btn_Scan_Click(sender As Object, e As EventArgs) Handles Btn_Scan.Click

        If DGV_Data_Pallet.Rows.Count = 0 Then Exit Sub

        For i As Integer = 0 To DGV_Data_Pallet.Rows.Count - 1
            get_grid_view(i)

            If dgv_Barcode.Trim.ToUpper = Txt_QR.Text.Trim.ToUpper Then

                DGV_Data_Pallet.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                Txt_QR.Text = ""

                Dim targetIndex As Integer = i

                If targetIndex < DGV_Data_Pallet.Rows.Count Then
                    DGV_Data_Pallet.FirstDisplayedScrollingRowIndex = targetIndex
                Else
                    MessageBox.Show("Jumlah data melebihi data yang tersedia!", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

                Exit Sub
            End If

        Next

        MessageBox.Show("Barcode Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Exit Sub
    End Sub

    Private Sub HitungGrand()
        If DGV_Data_Pallet.Rows.Count = 0 Then Exit Sub

        Dim total As Double = 0
        Dim totalBags As Double = 0

        For i As Integer = 0 To DGV_Data_Pallet.Rows.Count - 1
            get_grid_view(i)


            total = total + Val(HilangkanTanda(If(dgv_Jumlah = "", 0, dgv_Jumlah)))
            totalBags = totalBags + Val(HilangkanTanda(If(dgv_JmlhBags = "", 0, dgv_JmlhBags)))

        Next

        Txt_Total.Text = Format(total, "N2")

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click

        Txt_Total.Text = ""
        Txt_QR.Text = ""

        dataSementara.Clear()
        LoadDataPallet()
        HitungGrand()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        If DGV_Data_Pallet.Rows.Count = 0 Then Exit Sub

        Dim hasData As Boolean = False
        For i As Integer = 0 To DGV_Data_Pallet.Rows.Count - 1
            If Not DGV_Data_Pallet.Rows(i).Cells(itemDgvJumlah).Value = "" Or Not DGV_Data_Pallet.Rows(i).Cells(itemDgvBags).Value = "" Then
                hasData = True
                Exit For
            End If
        Next

        If Not hasData Then
            MessageBox.Show("Tidak Ada Data yang Akan Disimpan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If Txt_NoPenjualan.Text.Trim.Length = 0 Then
            MessageBox.Show("Penjualan Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If Val(HilangkanTanda(Txt_Total.Text)) > Val(HilangkanTanda(Txt_Sisa.Text)) Then
            MessageBox.Show("Total Tidak Boleh Lebih Besar dari Sisa", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DGV_Data_Pallet.Focus()
            Exit Sub
        End If
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim isUpdate As String = ""

            '==========================================
            '=     CEK APAKAH SEMENTARA SUDAH ADA     =
            '==========================================
            dataSementara.Clear()
            SQL = "select No_FakturPenjualan, Serial_Number, Kd_So, Kd_Barang from Emi_DO_Pallet_Sementara "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_FakturPenjualan = '" & Txt_NoPenjualan.Text & "' and userid = '" & UserID & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        isUpdate = "Y"
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim isiList As New Dictionary(Of String, Object)
                            isiList("KdSo") = .Rows(i).Item("Kd_So")
                            isiList("KdBarang") = .Rows(i).Item("Kd_Barang")
                            isiList("Serial_Number") = .Rows(i).Item("Serial_Number")

                            dataSementara.Add(isiList)

                        Next
                    Else
                        isUpdate = "T"
                    End If
                End With
            End Using

            If isUpdate = "" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Terjadi Kesalahan pada Form", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            For i As Integer = 0 To DGV_Data_Pallet.Rows.Count - 1

                get_grid_view(i)

                Dim sisaPotong As Double = 0
                Dim JumlahDipotong As Double = 0
                SQL = "select a.Jumlah as Stock_SN, a.serial_number "
                SQL = SQL & "from Barang_SN a where "
                SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.qr_Code+'-'+a.kode_unik_berjalan = '" & dgv_Barcode & "' "
                SQL = SQL & "and a.Kode_stock_owner = '" & dgv_Lokasi & "' and a.jumlah<>0 "
                SQL = SQL & "order by a.Tgl_Expired "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            sisaPotong = Val(HilangkanTanda(dgv_Jumlah))

                            For Index As Integer = 0 To .Rows.Count - 1
                                If sisaPotong = 0 Then
                                    Exit For
                                ElseIf sisaPotong < 0 Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Terdapat Kesalahan saat Potong Barang Produksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                                Dim JumlahInsert As Double = 0
                                Dim Satuan As String = ""

                                Dim Data_SN As String = .Rows(Index).Item("serial_number")

                                If sisaPotong < Val(HilangkanTanda(.Rows(Index).Item("Stock_SN"))) Or sisaPotong = Val(HilangkanTanda(.Rows(Index).Item("Stock_SN"))) Then

                                    JumlahInsert = sisaPotong
                                    ' Satuan = .Rows(Index).Item("Satuan").ToString.Trim


                                    JumlahDipotong += sisaPotong
                                    sisaPotong = 0

                                ElseIf sisaPotong > Val(HilangkanTanda(.Rows(Index).Item("Stock_SN"))) Then

                                    JumlahInsert = Val(HilangkanTanda(Format(.Rows(Index).Item("Stock_SN"), "N4")))
                                    'Satuan = .Rows(Index).Item("Satuan").ToString.Trim

                                    JumlahDipotong += Val(HilangkanTanda(Format(.Rows(Index).Item("Stock_SN"), "N4")))
                                    sisaPotong = sisaPotong - Val(HilangkanTanda(Format(.Rows(Index).Item("Stock_SN"), "N4")))
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Terjadi Kesalaham pada Barang SN untuk Kode Barang !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                                Dim hasFoundData As Boolean = False
                                Dim asdada = dataSementara.Count
                                For j As Integer = 0 To dataSementara.Count - 1
                                    Dim Data As Dictionary(Of String, Object) = dataSementara(j)

                                    If dgv_Lokasi = Data("KdSo") And dgv_KodeBarang = Data("KdBarang") And Data_SN = Data("Serial_Number") Then

                                        isUpdate = "Y"
                                        hasFoundData = True
                                        SQL = "update Emi_DO_Pallet_Sementara set Jumlah = '" & HilangkanTanda(dgv_Jumlah) & "', bags = '" & HilangkanTanda(dgv_JmlhBags) & "' "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_FakturPenjualan = '" & Txt_NoPenjualan.Text & "' "
                                        SQL = SQL & "and Kd_SO = '" & dgv_Lokasi & "' and Kd_Barang = '" & dgv_KodeBarang & "' and Serial_Number = '" & Data_SN & "' and userid = '" & UserID & "'"
                                        ExecuteTrans(SQL)

                                    End If

                                Next

                                If isUpdate = "T" Or (isUpdate = "Y" And hasFoundData = False) Then

                                    SQL = "insert into Emi_DO_Pallet_Sementara (Kode_Perusahaan, No_FakturPenjualan, Kd_SO, Kd_Barang, Serial_Number, Jumlah, Bags, UserId) "
                                    SQL = SQL & "values ('" & KodePerusahaan & "', '" & Txt_NoPenjualan.Text & "', '" & dgv_Lokasi & "', '" & dgv_KodeBarang & "',  "
                                    SQL = SQL & "'" & Data_SN & "', '" & HilangkanTanda(dgv_Jumlah) & "', '" & HilangkanTanda(dgv_JmlhBags) & "', '" & UserID & "')"
                                    ExecuteTrans(SQL)

                                End If

                            Next
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi Kesalahan Pada Barang !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using




            Next


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        MessageBox.Show("Data Berhasil Disimpan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
        DO_Reseller_New.GetJumlahKirim(Txt_KdSO.Text, Txt_KdBarang.Text)
        Me.Close()

    End Sub





End Class