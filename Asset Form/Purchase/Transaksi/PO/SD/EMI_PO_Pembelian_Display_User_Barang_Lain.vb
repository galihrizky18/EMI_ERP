Public Class EMI_PO_Pembelian_Display_User_Barang_Lain

    Dim arrcari As New ArrayList
    Dim arrNoPenawaran, arrIndex As New ArrayList
    Dim Jenis As String = "Master_Jenis_Hewan"
    Public KdSupp As String = ""
    Public LokasiPO As String = ""
    Public MataUang As String = ""

    Dim jumlahCheckedItem = 0

    Dim SkipBrg As String = "Y"

    Dim FlagSelisihPO As String = ""

    Public Jenismenu As String = "Menu"

    Dim lvCheckbox As String
    Dim lvNoPR As String
    Dim lvgudang As String
    Dim lvKdBarang As String
    Dim lvNmBarang As String
    Dim LvSisa As String
    Dim lvSatuan As String
    Dim lvHarga As String
    Dim lvJumlah As String
    Dim lvSatuanPo As String
    Dim lvNoPenawaran As String
    Dim lvNoUrutPR As String
    Dim LvSatuanHarga As String
    Dim LvHargaId As String
    Dim LvSKBrg As String
    Dim LvTglDeliv As String
    Dim LvWaktuPabrikasi As String
    Dim LvTglActDelivery As String


    Dim cellNoPR As Integer = 0
    Dim cellgudang As Integer = 1
    Dim cellKdBarang As Integer = 2
    Dim cellNmBarang As Integer = 3
    Dim cellSisa As Integer = 4
    Dim cellSatuan As Integer = 5
    Dim cellHarga As Integer = 6
    Dim cellJumlah As Integer = 7
    Dim cellSatuanPO As Integer = 8
    Dim cellNoPenawaran As Integer = 9
    Dim cellNoUrutPR As Integer = 10
    Dim cellSatuanHarga As Integer = 11
    Dim cellHargaId As Integer = 12
    Dim cellSkBrg As Integer = 13
    Dim cellTglDeliv As Integer = 14
    Dim cellWktPabrikasi As Integer = 15
    Dim cellTglActDelivery As Integer = 16

    Public asal_data As String = ""

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
        lvNoPR = Dgv_Pr.Rows(No_Index).Cells(cellNoPR).Value.ToString
        lvgudang = Dgv_Pr.Rows(No_Index).Cells(cellgudang).Value.ToString
        lvKdBarang = Dgv_Pr.Rows(No_Index).Cells(cellKdBarang).Value.ToString
        lvNmBarang = Dgv_Pr.Rows(No_Index).Cells(cellNmBarang).Value.ToString
        LvSisa = Dgv_Pr.Rows(No_Index).Cells(cellSisa).Value.ToString
        lvSatuan = Dgv_Pr.Rows(No_Index).Cells(cellSatuan).Value.ToString
        lvHarga = Dgv_Pr.Rows(No_Index).Cells(cellHarga).Value.ToString
        lvJumlah = Dgv_Pr.Rows(No_Index).Cells(cellJumlah).Value.ToString
        lvSatuanPo = Dgv_Pr.Rows(No_Index).Cells(cellSatuanPO).Value.ToString
        lvNoPenawaran = Dgv_Pr.Rows(No_Index).Cells(cellNoPenawaran).Value.ToString
        lvNoUrutPR = Dgv_Pr.Rows(No_Index).Cells(cellNoUrutPR).Value.ToString
        LvSatuanHarga = Dgv_Pr.Rows(No_Index).Cells(cellSatuanHarga).Value.ToString
        LvHargaId = Dgv_Pr.Rows(No_Index).Cells(cellHargaId).Value.ToString
        LvSKBrg = Dgv_Pr.Rows(No_Index).Cells(cellSkBrg).Value.ToString
        LvTglDeliv = Dgv_Pr.Rows(No_Index).Cells(cellTglDeliv).Value.ToString
        LvWaktuPabrikasi = Dgv_Pr.Rows(No_Index).Cells(cellWktPabrikasi).Value.ToString
        LvTglActDelivery = Dgv_Pr.Rows(No_Index).Cells(cellTglActDelivery).Value.ToString
    End Sub


    Dim lvPO_Lokasi As String
    Dim lvPO_KdBarang As String
    Dim lvPO_NmBarang As String
    Dim lvPO_Harga As String
    Dim lvPO_Jumlah As String
    Dim lvPO_Satuan As String
    Dim lvPO_Harga_SB As String
    Dim lvPO_Jumlah_SB As String
    Dim lvPO_Satuan_SB As String
    Dim lvPO_NoPenawaran As String
    Dim lvPO_ID As String
    Dim lvPO_Total As String
    Dim lvPO_Urut As String
    Dim lvPO_PR As String

    Dim cellPO_Lokasi As Integer = 0
    Dim cellPO_KdBarang As Integer = 1
    Dim cellPO_NmBarang As Integer = 2
    Dim cellPO_Harga As Integer = 3
    Dim cellPO_Jumlah As Integer = 4
    Dim cellPO_Satuan As Integer = 5
    Dim cellPO_Harga_SB As Integer = 6
    Dim cellPO_Jumlah_SB As Integer = 7
    Dim cellPO_Satuan_SB As Integer = 8
    Dim cellPO_NoPenawaran As Integer = 9
    Dim cellPO_ID As Integer = 10
    Dim cellPO_Total As Integer = 11
    Dim cellPO_Urut As Integer = 12
    Dim cellPO_PR As Integer = 13

    Private Sub Get_Isi_Listview_po_pembelian(ByVal No_Index As Integer)
        lvPO_Lokasi = EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items(No_Index).SubItems(cellPO_Lokasi).Text
        lvPO_KdBarang = EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items(No_Index).SubItems(cellPO_KdBarang).Text
        lvPO_NmBarang = EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items(No_Index).SubItems(cellPO_NmBarang).Text
        lvPO_Harga = EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items(No_Index).SubItems(cellPO_Harga).Text
        lvPO_Jumlah = EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items(No_Index).SubItems(cellPO_Jumlah).Text
        lvPO_Satuan = EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items(No_Index).SubItems(cellPO_Satuan).Text
        lvPO_Harga_SB = EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items(No_Index).SubItems(cellPO_Harga_SB).Text
        lvPO_Jumlah_SB = EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items(No_Index).SubItems(cellPO_Jumlah_SB).Text
        lvPO_Satuan_SB = EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items(No_Index).SubItems(cellPO_Satuan_SB).Text
        lvPO_NoPenawaran = EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items(No_Index).SubItems(cellPO_NoPenawaran).Text
        lvPO_ID = EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items(No_Index).SubItems(cellPO_ID).Text
        lvPO_Total = EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items(No_Index).SubItems(cellPO_Total).Text
        lvPO_Urut = EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items(No_Index).SubItems(cellPO_Urut).Text
        lvPO_PR = EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items(No_Index).SubItems(cellPO_PR).Text
    End Sub


    Private Sub Display_Gudang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Pembelian_Barang_Masuk")
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try




        'Dgv_Pr.Columns(cellgudang).HeaderText = Base_Language.Lang_Global_LokasiGudang
        'Dgv_Pr.Columns(cellKdBarang).HeaderText = Base_Language.Lang_Global_KodeBarang
        'Dgv_Pr.Columns(cellNmBarang).HeaderText = Base_Language.Lang_Global_NamaBarang
        'Dgv_Pr.Columns(cellJumlah).HeaderText = Base_Language.Lang_Global_Jumlah
        'Dgv_Pr.Columns(cellSatuan).HeaderText = Base_Language.Lang_Global_Satuan
        'Dgv_Pr.Columns(cellSusunan).HeaderText = "" 'Base_Language.Lang_Global_sus
        'Dgv_Pr.Columns(cellLokasiRak).HeaderText = "" 'Base_Language.Lang_Global_ra

        kosong()
    End Sub


    Public Sub kosong()

        CmbPO_JnsBayar.Items.Clear()
        CmbPO_JnsBayar.Items.Add("No PR") : arrcari.Add("no_faktur")
        CmbPO_JnsBayar.Items.Add("Tanggal Delivery") : arrcari.Add("tanggal_delivery")
        CmbPO_JnsBayar.Items.Add("Nama") : arrcari.Add("nama")

        CheckBox1.Checked = False


        Cari()
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Cari()
    End Sub

    Private Sub Cari()
        get_jam()

        Try

            OpenConn()
            Dgv_Pr.Rows.Clear()

            SQL = "With cte As ( "
            SQL = SQL & "Select a.No_Faktur,b.Kode_Stock_Owner,b.Kode_Barang,c.Nama,c.satuan As satuan_kecil_barang, "
            SQL = SQL & "b.Satuan, b.tanggal_delivery, b.no_urut, b.Jumlah, "

            SQL = SQL & "isnull((select  sum(y.Jumlah) from  EMI_Pembelian_PO_Induk_Barang_Lain x, EMI_Pembelian_PO_Det_Induk_Barang_Lain y where "
            SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur And "
            SQL = SQL & "y.Kode_Perusahaan = a.Kode_Perusahaan And y.no_urut_pr = b.No_Urut And x.status Is null And x.Flag_Release Is null ),0) As jumlah_Sementara, "

            SQL = SQL & "isnull((select  sum(y.Jumlah) from  EMI_Pembelian_PO_Induk_Barang_Lain x, EMI_Pembelian_PO_Det_Induk_Barang_Lain y where "
            SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur And "
            SQL = SQL & "y.Kode_Perusahaan = a.Kode_Perusahaan And y.no_urut_pr = b.No_Urut And x.status Is null And x.Flag_Release ='Y' ),0) as jumlah_Release, "

            SQL = SQL & "ISNULL((select waktu_pabrikasi from emi_detail_proses_pengiriman_po_Barang_Lain x, Suppliers y where "
            SQL = SQL & "b.Kode_Perusahaan = x.Kode_Perusahaan And b.Kode_Barang = x.kode_barang And "
            SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan And x.Id_Kategori_Supplier = y.ID_Kategori_Suppliers "
            SQL = SQL & "And y.Kode_Supplier = '" & EMI_PO_Pembelian_Barang_Lain.TxtPO_KdSupplier.Text & "'),0) as Waktu_Pabrikasi,  "

            SQL = SQL & "ISNULL((select  Waktu_Pengiriman from emi_detail_proses_pengiriman_po_Barang_Lain x, Suppliers y "
            SQL = SQL & "where b.Kode_Perusahaan = x.Kode_Perusahaan And b.Kode_Barang = x.kode_barang And "
            SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan And x.Id_Kategori_Supplier = y.ID_Kategori_Suppliers "
            SQL = SQL & "And y.Kode_Supplier = '" & EMI_PO_Pembelian_Barang_Lain.TxtPO_KdSupplier.Text & "' ),0) as Waktu_Pengiriman "

            SQL = SQL & "From EMI_Purchase_Requisition_Barang_Lain a, EMI_Purchase_Requisition_Barang_Lain_Detail b , barang_Lain c, Emi_Role_Kategori_PO d "
            SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur And "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.Kode_Barang = c.Kode_Barang And "
            SQL = SQL & "b.Kode_Stock_Owner = c.Kode_Stock_Owner And a.kode_perusahaan = '" & KodePerusahaan & "' and a.Status is null "
            SQL = SQL & " And flag_release = 'Y' and c.kode_Perusahaan=d.kode_Perusahaan and "
            SQL = SQL & "c.id_kategori_PO = d.kategori_po And d.userid = '" & UserID & "' and b.flag_sudah_po is null and b.Flag_Pengajuan_Selesai is null "
            SQL = SQL & ") "

            SQL = SQL & "Select No_Faktur, Kode_Stock_Owner, Kode_Barang, Nama, satuan_kecil_barang, Satuan, Tanggal_Delivery, No_Urut, "
            SQL = SQL & "jumlah-(jumlah_Sementara + jumlah_Release) As Jumlah, Waktu_Pabrikasi, Waktu_Pengiriman, "
            SQL = SQL & "DateDiff(Day, Tanggal_Delivery, DateAdd(Day, Waktu_Pabrikasi + Waktu_Pengiriman, '" & Format(tgl_skg, "yyyy-MM-dd") & "') ) as  Waktu_Proses_Pengiriman, "
            SQL = SQL & "DateAdd(Day, Waktu_Pabrikasi + Waktu_Pengiriman, '" & Format(tgl_skg, "yyyy-MM-dd") & "') as tanggal_actual_delivery "

            SQL = SQL & "From cte "
            SQL = SQL & "Where jumlah - (jumlah_Sementara + jumlah_Release) <> 0 "
            If CmbPO_JnsBayar.SelectedIndex = -1 Then
                SQL = SQL & "order by no_faktur"
            Else
                SQL = SQL & "order by " & arrcari.Item(CmbPO_JnsBayar.SelectedIndex) & " "
            End If
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1

                        Dgv_Pr.Rows.Add(1)
                        '  Get_Isi_Listview(i)
                        'Dgv_Pr.Rows(i).Cells(cellNoPR).Value = .Rows(i).Item("no_faktur")
                        'Dgv_Pr.Rows(i).Cells(cellgudang).Value = .Rows(i).Item("kode_stock_owner")
                        'Dgv_Pr.Rows(i).Cells(cellKdBarang).Value = .Rows(i).Item("kode_barang")
                        'Dgv_Pr.Rows(i).Cells(cellNmBarang).Value = .Rows(i).Item("nama")
                        'Dgv_Pr.Rows(i).Cells(cellSisa).Value = Format(.Rows(i).Item("jumlah"), "N2")
                        'Dgv_Pr.Rows(i).Cells(cellSatuan).Value = .Rows(i).Item("satuan")

                        Dgv_Pr.Rows(i).Cells(cellNoPR).Value = .Rows(i).Item("no_faktur")
                        Dgv_Pr.Rows(i).Cells(cellgudang).Value = .Rows(i).Item("kode_stock_owner")
                        Dgv_Pr.Rows(i).Cells(cellKdBarang).Value = .Rows(i).Item("kode_barang")
                        Dgv_Pr.Rows(i).Cells(cellNmBarang).Value = .Rows(i).Item("nama")
                        Dgv_Pr.Rows(i).Cells(cellSisa).Value = Format(.Rows(i).Item("jumlah"), "N2")
                        Dgv_Pr.Rows(i).Cells(cellSatuan).Value = .Rows(i).Item("satuan")
                        Dgv_Pr.Rows(i).Cells(cellHarga).Value = Format(0, "N2")
                        Dgv_Pr.Rows(i).Cells(cellJumlah).Value = Format(0, "N2")
                        Dgv_Pr.Rows(i).Cells(cellSatuanPO).Value = .Rows(i).Item("satuan")
                        Dgv_Pr.Rows(i).Cells(cellNoPenawaran).Value = ""
                        Dgv_Pr.Rows(i).Cells(cellNoUrutPR).Value = .Rows(i).Item("no_urut")
                        Dgv_Pr.Rows(i).Cells(cellSatuanHarga).Value = ""
                        Dgv_Pr.Rows(i).Cells(cellHargaId).Value = ""
                        Dgv_Pr.Rows(i).Cells(cellSkBrg).Value = .Rows(i).Item("satuan_kecil_barang")
                        Dgv_Pr.Rows(i).Cells(cellTglDeliv).Value = Format(.Rows(i).Item("tanggal_delivery"), "dd MMM yyyy")
                        Dgv_Pr.Rows(i).Cells(cellSkBrg).Value = .Rows(i).Item("satuan_kecil_barang")
                        Dgv_Pr.Rows(i).Cells(cellWktPabrikasi).Value = .Rows(i).Item("Waktu_Proses_Pengiriman")
                        Dgv_Pr.Rows(i).Cells(cellTglActDelivery).Value = Format(.Rows(i).Item("tanggal_actual_delivery"), "dd MMM yyyy")


                        If Jenismenu = "Display" Then
                            Dgv_Pr.Rows(i).Cells(cellNoPR).ReadOnly = True
                            Dgv_Pr.Rows(i).Cells(cellgudang).ReadOnly = True
                            Dgv_Pr.Rows(i).Cells(cellKdBarang).ReadOnly = True
                            Dgv_Pr.Rows(i).Cells(cellNmBarang).ReadOnly = True
                            Dgv_Pr.Rows(i).Cells(cellSisa).ReadOnly = True
                            Dgv_Pr.Rows(i).Cells(cellSatuan).ReadOnly = True
                            Dgv_Pr.Rows(i).Cells(cellHarga).ReadOnly = True
                            Dgv_Pr.Rows(i).Cells(cellJumlah).ReadOnly = True
                            Dgv_Pr.Rows(i).Cells(cellSatuanPO).ReadOnly = True
                            Dgv_Pr.Rows(i).Cells(cellNoPenawaran).ReadOnly = True
                            Dgv_Pr.Rows(i).Cells(cellNoUrutPR).ReadOnly = True
                            Dgv_Pr.Rows(i).Cells(cellTglDeliv).ReadOnly = True

                        End If

                    Next
                End With
            End Using
            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Master_Jenis_Hewan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        kosong()
    End Sub

    Private Sub CmbPO_JnsBayar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPO_JnsBayar.SelectedIndexChanged
        Cari()
    End Sub

    Private Sub Dgv_Pr_DoubleClick(sender As Object, e As EventArgs) Handles Dgv_Pr.DoubleClick
        If Dgv_Pr.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim currentRow = Dgv_Pr.CurrentRow.Index
        Dim currentCell = Dgv_Pr.CurrentCellAddress.X

        Dim data = Dgv_Pr.Rows(currentRow).Cells(currentCell)


        If currentCell = cellHarga Then
            SD_Pilih_Harga_PO_Barang_Lain.kodeSupplier = KdSupp
            SD_Pilih_Harga_PO_Barang_Lain.kodeBarang = Dgv_Pr.Rows(currentRow).Cells(cellKdBarang).Value
            SD_Pilih_Harga_PO_Barang_Lain.cellDgv = currentCell
            SD_Pilih_Harga_PO_Barang_Lain.cellNoPenawaran = cellNoPenawaran
            SD_Pilih_Harga_PO_Barang_Lain.cellSatuanHarga = cellSatuanHarga
            SD_Pilih_Harga_PO_Barang_Lain.cellHargaID = cellHargaId
            SD_Pilih_Harga_PO_Barang_Lain.MataUang = MataUang
            SD_Pilih_Harga_PO_Barang_Lain.rowDgv = currentRow
            SD_Pilih_Harga_PO_Barang_Lain.ShowDialog()
        End If

    End Sub

    Private Sub btnPilih_Click(sender As Object, e As EventArgs) Handles btnPilih.Click
        Dim Kode_Kategori_Besar As String = ""
        For indexDisplayUserPO As Integer = 0 To Dgv_Pr.Rows.Count - 1

            Get_Isi_Listview(indexDisplayUserPO)

            '=======================================
            '     CEK APAKAH ADA DATA TERLEWAT     =
            '=======================================
            If Not Val(HilangkanTanda(lvHarga)) = 0 Then
                If Val(HilangkanTanda(lvJumlah)) = 0 Then
                    MessageBox.Show("Jumlah pada Baris ke -" & indexDisplayUserPO + 1 & " Tidak Boleh 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

            ElseIf Not Val(HilangkanTanda(lvJumlah)) = 0 Then
                If Val(HilangkanTanda(lvHarga)) = 0 Then
                    MessageBox.Show("Harga pada Baris ke -" & indexDisplayUserPO + 1 & " Tidak Boleh 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

            End If

            'If Dgv_Pr.Rows(indexDisplayUserPO).Cells(7).Value = "" Then
            '    MessageBox.Show("Harga pada " & Dgv_Pr.Rows(indexDisplayUserPO).Cells(3).Value & " belum di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'ElseIf Dgv_Pr.Rows(indexDisplayUserPO).Cells(8).Value = 0 Then
            '    MessageBox.Show("Jumlah pada " & Dgv_Pr.Rows(indexDisplayUserPO).Cells(3).Value & " belum di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'ElseIf Dgv_Pr.Rows(indexDisplayUserPO).Cells(9).Value = "" Then
            '    MessageBox.Show("Satuan pada " & Dgv_Pr.Rows(indexDisplayUserPO).Cells(3).Value & " belum di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If


            For i As Integer = 0 To EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items.Count - 1

                If EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items(i).SubItems(cellPO_KdBarang).Text = Dgv_Pr.Rows(indexDisplayUserPO).Cells(cellKdBarang).Value Then

                    'cek di lv emi po apakah sudah ada data yang sama atau belum
                    If EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items(i).SubItems(cellPO_Satuan).Text <> Dgv_Pr.Rows(indexDisplayUserPO).Cells(cellSatuanPO).Value Then
                        MessageBox.Show(Base_Language.Lang_Global_Satuan & " " & Base_Language.Lang_Global_Tidak_Bisa_Berbeda & ". . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    'cek di lv emi po apakah sudah ada data yang sama atau belum
                    If EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items(i).SubItems(cellPO_ID).Text = "T" Then

                        If EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items(i).SubItems(cellPO_NoPenawaran).Text = Dgv_Pr.Rows(indexDisplayUserPO).Cells(cellNoPenawaran).Value Then
                            If EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items(i).SubItems(cellPO_PR).Text = Dgv_Pr.Rows(indexDisplayUserPO).Cells(cellNoUrutPR).Value Then
                                MessageBox.Show(Base_Language.Lang_Global_Data_Sdh_Ada & ". . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End If
                    End If

                End If
            Next

        Next



        Try
            OpenConn()

            Dim cekLanjutPO As Boolean = True

            Dim pesan As String = "Berikut Data yang akan datang terlambat dari estimasi delivery " & vbNewLine
            Dim flag_stok_cukup As Boolean = True

            For indexDisplayUserPO As Integer = 0 To Dgv_Pr.Rows.Count - 1
                If Not Val(HilangkanTanda(Dgv_Pr.Rows(indexDisplayUserPO).Cells(cellHarga).Value)) = 0 And Not Val(HilangkanTanda(Dgv_Pr.Rows(indexDisplayUserPO).Cells(cellJumlah).Value)) = 0 Then
                    Get_Isi_Listview(indexDisplayUserPO)







                    If LvWaktuPabrikasi > 0 Then
                        cekLanjutPO = False
                        pesan = pesan & lvNmBarang & vbNewLine & " - Tanggal Estimasi Delivery  " & LvTglDeliv & vbNewLine & " - Tanggal Actual Delivery " & LvTglActDelivery & vbNewLine & vbNewLine
                    End If






                End If
            Next

            If cekLanjutPO = False Then
                Dim tanya As String = MessageBox.Show(pesan & vbNewLine & "Apakah ingin melanjutkan transaksi ? ", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If tanya = vbNo Then
                    CloseConn()
                    MessageBox.Show("Transaksi dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End If
            For indexDisplayUserPO As Integer = 0 To Dgv_Pr.Rows.Count - 1

                If Not Val(HilangkanTanda(Dgv_Pr.Rows(indexDisplayUserPO).Cells(cellHarga).Value)) = 0 And Not Val(HilangkanTanda(Dgv_Pr.Rows(indexDisplayUserPO).Cells(cellJumlah).Value)) = 0 Then
                    Get_Isi_Listview(indexDisplayUserPO)

                    lvHarga = HilangkanTanda(lvHarga)
                    lvJumlah = HilangkanTanda(lvJumlah)

                    Dim harga_satuan_besar As Double = 0
                    SQL = "Select dbo.Ubah_Satuan_lain('" & KodePerusahaan & "','UANG','" & lvKdBarang & "',"
                    SQL = SQL & "'" & LvSatuanHarga & "','" & lvSatuanPo & "',"
                    SQL = SQL & "" & LvHargaId & ") as Hasil "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then

                            If General_Class.CekNULL(dr("Hasil")) <> "" Then
                                If dr("Hasil") = 0 Then
                                    MessageBox.Show("Satuan " & LvSatuanHarga & " Ke " & lvSatuanPo & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    CloseConn()
                                    Exit Sub
                                Else
                                    harga_satuan_besar = dr("hasil")
                                End If
                            Else
                                MessageBox.Show("Satuan " & LvSatuanHarga & " Ke " & lvSatuanPo & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                CloseConn()
                                Exit Sub
                            End If
                        End If
                    End Using



                    Dim Jumlah_satuan_Kecil As Double = 0
                    SQL = "select dbo.Ubah_Satuan_lain('" & KodePerusahaan & "','MASA','" & lvKdBarang & "',"
                    SQL = SQL & "'" & lvSatuanPo & "','" & LvSKBrg & "',"
                    SQL = SQL & "" & lvJumlah & ") as Hasil "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then

                            If General_Class.CekNULL(dr("Hasil")) <> "" Then
                                If dr("Hasil") = 0 Then
                                    MessageBox.Show("Satuan " & lvSatuanPo & " Ke " & LvSKBrg & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    CloseConn()
                                    Exit Sub
                                Else
                                    Jumlah_satuan_Kecil = dr("hasil")
                                End If
                            Else
                                MessageBox.Show("Satuan " & lvSatuanPo & " Ke " & LvSKBrg & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                CloseConn()
                                Exit Sub
                            End If
                        End If
                    End Using

                    Dim jumlah_sisa_satuan_kecil As Double = 0
                    SQL = "select dbo.Ubah_Satuan_Lain('" & KodePerusahaan & "','MASA','" & lvKdBarang & "',"
                    SQL = SQL & "'" & lvSatuan & "','" & LvSKBrg & "',"
                    SQL = SQL & "" & HilangkanTanda(LvSisa) & ") as Hasil "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then

                            If General_Class.CekNULL(dr("Hasil")) <> "" Then
                                If dr("Hasil") = 0 Then
                                    MessageBox.Show("Satuan " & lvSatuanPo & " Ke " & LvSKBrg & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    CloseConn()
                                    Exit Sub
                                Else
                                    jumlah_sisa_satuan_kecil = dr("hasil")
                                End If
                            Else
                                MessageBox.Show("Satuan " & lvSatuanPo & " Ke " & LvSKBrg & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                CloseConn()
                                Exit Sub
                            End If
                        End If
                    End Using

                    Dim lokasi_gudang_bahan As String = ""

                    SQL = "select a.Kode_Stock_Owner_Gudang From Binding_Lokasi_Gudang_lain a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "a.Kode_Stock_Owner = '" & LokasiPO & "' and a.Gudang_Default = 'Y'"
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            lokasi_gudang_bahan = dr("Kode_Stock_Owner_Gudang")
                        Else
                            dr.Close()
                            CloseConn()
                            MessageBox.Show(Base_Language.Lang_Global_LokasiGudang & " " & Base_Language.Lang_GLOBAL_Tidak_Ditemukan & ". . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using


                    ''==============================
                    ''=     GEK JENIS KATEGORI     =
                    ''==============================
                    'SQL = "select top 1 b.Kode_Kategori_Besar, b.Jenis_Kategori "
                    'SQL = SQL & "from Barang_lain a, Kategori_Besar b "
                    'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                    'SQL = SQL & "and a.Kode_Kategori_Besar = b.Kode_Kategori_Besar "
                    'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Barang = '" & lvKdBarang & "' "
                    'Using Dr = OpenTrans(SQL)
                    '    If Dr.Read Then

                    '        If EMI_PO_Pembelian_Barang_lain.LvPO_DataPO.Items.Count = 0 Then
                    '            If Kode_Kategori_Besar = "" Then
                    '                Kode_Kategori_Besar = Dr("Jenis_Kategori")

                    '                If Dr("Jenis_Kategori").ToString.ToUpper = "BARANG" Then
                    '                    'tempDataPajak.Clear()

                    EMI_PO_Pembelian_Barang_Lain.tempDataPajak.RemoveAll(Function(item) item.isPPN = False)

                    '                End If

                    '            Else
                    '                If Not Kode_Kategori_Besar = Dr("Jenis_Kategori") Then
                    '                    Dr.Close()
                    '                    CloseConn()
                    '                    MessageBox.Show("Jenis Kategori Besar Tidak Boleh Berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                    Exit Sub
                    '                End If
                    '            End If

                    '        Else

                    '            If Not EMI_PO_Pembelian_Barang_lain.LvPO_DataPO.Items(0).SubItems(EMI_PO_Pembelian_Barang_lain.cellPO_JnsKategori).Text = Dr("Jenis_Kategori") Then
                    '                Dr.Close()
                    '                CloseConn()
                    '                MessageBox.Show("Jenis Kategori Besar Tidak Boleh Berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                Exit Sub
                    '            Else
                    '                Kode_Kategori_Besar = Dr("Jenis_Kategori")
                    '            End If
                    '        End If
                    '    End If
                    'End Using

                    Dim lvw As ListViewItem
                    lvw = EMI_PO_Pembelian_Barang_Lain.LvPO_DataPO.Items.Add(lokasi_gudang_bahan) '0
                    lvw.SubItems.Add(lvKdBarang) '1
                    lvw.SubItems.Add(lvNmBarang) '2
                    lvw.SubItems.Add(Format(Val(harga_satuan_besar), "N2")) '3
                    lvw.SubItems.Add(Format(Val(lvJumlah), "N2")) '4
                    lvw.SubItems.Add(lvSatuan) '5
                    lvw.SubItems.Add(LvHargaId) '6
                    lvw.SubItems.Add(Jumlah_satuan_Kecil) '7
                    lvw.SubItems.Add(LvSKBrg) '8
                    lvw.SubItems.Add(lvNoPenawaran) '9
                    If FlagSelisihPO = "Y" Then
                        lvw.SubItems.Add("X") '10
                    Else
                        lvw.SubItems.Add("T") '10
                    End If


                    lvw.SubItems.Add(Format(Jumlah_satuan_Kecil * Val(LvHargaId), "N2")) '11
                    lvw.SubItems.Add("") '12
                    lvw.SubItems.Add(lvNoUrutPR) '13


                    Dim tempo_penawaran As String = ""
                    Dim jatuh_tempo As String = ""
                    SQL = "select a.No_Faktur, a.no_penawaran,a.Kode_Supplier, c.Nama,b.satuan, b.Nilai_Barang,b.harga_satuan, b.satuan_Barang,  "

                    SQL = SQL & "isnull((select x.Lama_Pembayaran from EMI_Master_Penawaran_Jatuh_Tempo_Barang_Lain x where a.Kode_Perusahaan = x.Kode_Perusahaan "
                    SQL = SQL & "and a.No_Faktur = x.No_Faktur), 0) as jatuh_Tempo,"

                    SQL = SQL & "isnull((select x.Tempo_Pembayaran from EMI_Master_Penawaran_Jatuh_Tempo_Barang_Lain x where a.Kode_Perusahaan = x.Kode_Perusahaan "
                    SQL = SQL & "and a.No_Faktur = x.No_Faktur), null) as Tempo_Pembayaran "

                    SQL = SQL & "from EMI_Master_Penawaran_Barang_Lain a, EMI_Master_Penawaran_Detail_Barang_Lain b, Suppliers c "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
                    SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Supplier = c.Kode_Supplier "
                    SQL = SQL & "and b.kode_barang = '" & lvKdBarang & "' and a.no_faktur='" & lvNoPenawaran & "' "
                    Using dr2 = OpenTrans(SQL)
                        Do While dr2.Read
                            jatuh_tempo = dr2("jatuh_tempo")
                            If General_Class.CekNULL(dr2("tempo_pembayaran")) = "" Then
                                tempo_penawaran = "-"
                            Else
                                tempo_penawaran = dr2("tempo_pembayaran")
                            End If
                        Loop
                    End Using

                    lvw.SubItems.Add(tempo_penawaran) '14
                    lvw.SubItems.Add(jatuh_tempo) '15
                    lvw.SubItems.Add("") '16
                    lvw.SubItems.Add(Kode_Kategori_Besar) '17

                End If


            Next
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



        EMI_PO_Pembelian_Barang_Lain.HitungGrandTotal()
        Me.Close()
    End Sub


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        ' SkipBrg = "Y"
        If CheckBox1.Checked = True Then

            For i As Integer = 0 To Dgv_Pr.Rows.Count - 1
                Dgv_Pr.Rows(i).Cells(0).Value = True

            Next


        Else



            For i As Integer = 0 To Dgv_Pr.Rows.Count - 1
                Dgv_Pr.Rows(i).Cells(0).Value = False

                Dgv_Pr.Rows(i).Cells(0).Value = False
                Dgv_Pr.Rows(i).Cells(7).ReadOnly = True
                Dgv_Pr.Rows(i).Cells(8).ReadOnly = False
                Dgv_Pr.Rows(i).Cells(9).ReadOnly = True

                Dgv_Pr.Rows(i).Cells(7).Value = ""
                Dgv_Pr.Rows(i).Cells(8).Value = ""

            Next


        End If
    End Sub

    Private Sub Dgv_Pr_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Pr.CellEndEdit
        If Dgv_Pr.Rows.Count = 0 Then
            Exit Sub
        End If

        If Dgv_Pr.CurrentCell.ColumnIndex = cellHarga Or Dgv_Pr.CurrentCell.ColumnIndex = cellJumlah Then
            If Not IsNumeric(Dgv_Pr.CurrentCell.Value) Then
                Dgv_Pr.CurrentCell.Value = Format(0, "N2")
            End If
        End If

        Try
            OpenConn()

            Get_Isi_Listview(Dgv_Pr.CurrentRow.Index)

            Dim Jumlah_satuan_Kecil As Double = 0
            SQL = "select dbo.Ubah_Satuan_lain('" & KodePerusahaan & "','MASA','" & lvKdBarang & "',"
            SQL = SQL & "'" & lvSatuanPo & "','" & LvSKBrg & "',"
            SQL = SQL & "" & lvJumlah & ") as Hasil "
            Using dr = OpenTrans(SQL)
                If dr.Read Then

                    If General_Class.CekNULL(dr("Hasil")) <> "" Then
                        If dr("Hasil") = 0 Then
                            MessageBox.Show("Satuan " & lvSatuanPo & " Ke " & LvSKBrg & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        Else
                            Jumlah_satuan_Kecil = dr("hasil")
                        End If
                    Else
                        MessageBox.Show("Satuan " & lvSatuanPo & " Ke " & LvSKBrg & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
            End Using

            Dim jumlah_sisa_satuan_kecil As Double = 0
            SQL = "select dbo.Ubah_Satuan_Lain('" & KodePerusahaan & "','MASA','" & lvKdBarang & "',"
            SQL = SQL & "'" & lvSatuan & "','" & LvSKBrg & "',"
            SQL = SQL & "" & HilangkanTanda(LvSisa) & ") as Hasil "
            Using dr = OpenTrans(SQL)
                If dr.Read Then

                    If General_Class.CekNULL(dr("Hasil")) <> "" Then
                        If dr("Hasil") = 0 Then
                            MessageBox.Show("Satuan " & lvSatuanPo & " Ke " & LvSKBrg & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        Else
                            jumlah_sisa_satuan_kecil = dr("hasil")
                        End If
                    Else
                        MessageBox.Show("Satuan " & lvSatuanPo & " Ke " & LvSKBrg & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
            End Using


            If jumlah_sisa_satuan_kecil < Jumlah_satuan_Kecil Then
                MessageBox.Show("Jumlah po tidak boleh lebih besar dari jumlah PR!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Dgv_Pr.CurrentCell.Value = Format(0, "N2")
                CloseConn()
                Exit Sub
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        '======================
        '=     SET FORMAT     =
        '======================

        If Dgv_Pr.CurrentCell.ColumnIndex = cellHarga Or Dgv_Pr.CurrentCell.ColumnIndex = cellJumlah Then

            Dim cellKuantity As String = Dgv_Pr.CurrentCell.Value

            If cellKuantity.Contains(",") Then
                MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Dgv_Pr.CurrentCell.Value = Format(0, "N2")
                Exit Sub
            End If

            Dim nilai As Decimal = Decimal.Parse(cellKuantity)
            Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

            Dgv_Pr.CurrentCell.Value = formattedValue
        End If


#Region "Kode Lama"

        'Dim currentRow = Dgv_Pr.CurrentRow.Index
        'Dim currentCell = Dgv_Pr.CurrentCellAddress.X

        'Dim data = Dgv_Pr.Rows(currentRow).Cells(currentCell)

        ''CentangSemuaData()
#End Region

    End Sub

    Private Sub Dgv_Pr_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Pr.CellEnter
        '======================
        '=     SET FORMAT     =
        '======================

        If Dgv_Pr.CurrentCell.ColumnIndex = cellHarga Or Dgv_Pr.CurrentCell.ColumnIndex = cellJumlah Then
            Dim cellKuantity As String = Dgv_Pr.CurrentCell.Value

            If cellKuantity = "" Then
                Exit Sub
            End If

            Dim cleanedStr As String = HilangkanTanda(cellKuantity) ' Menghapus titik
            Dim nilai As Decimal = Decimal.Parse(cleanedStr)

            Dgv_Pr.CurrentCell.Value = nilai
        End If
    End Sub

    Private Sub Dgv_Pr_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Pr.CellLeave
        '======================
        '=     SET FORMAT     =
        '======================

        If Dgv_Pr.CurrentCell.ColumnIndex = cellHarga Or Dgv_Pr.CurrentCell.ColumnIndex = cellJumlah Then
            Dim cellKuantity As String = Dgv_Pr.CurrentCell.Value

            If cellKuantity = "" Then
                Exit Sub
            End If


            Dim nilai As Decimal = Decimal.Parse(cellKuantity)
            Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

            Dgv_Pr.CurrentCell.Value = formattedValue

        End If
    End Sub



    Private Sub CentangSemuaData()
        'arrIndex.Clear()

        If SkipBrg = "Y" Then

            If Val(Dgv_Pr.CurrentRow.Cells(8).Value) < 0 Or IsNumeric(Dgv_Pr.CurrentRow.Cells(8).Value) = False Then
                Dgv_Pr.CurrentRow.Cells(8).Value = 0
            End If


            For i As Integer = 0 To Dgv_Pr.Rows.Count - 1
                If Dgv_Pr.Rows(i).Cells(0).Value = True Then

                    jumlahCheckedItem = jumlahCheckedItem + 1

                    Dgv_Pr.Rows(i).Cells(7).ReadOnly = True
                    Dgv_Pr.Rows(i).Cells(8).ReadOnly = False
                    Dgv_Pr.Rows(i).Cells(9).ReadOnly = False

                Else
                    jumlahCheckedItem = jumlahCheckedItem - 1
                    Dgv_Pr.Rows(i).Cells(7).ReadOnly = True
                    Dgv_Pr.Rows(i).Cells(8).ReadOnly = False
                    Dgv_Pr.Rows(i).Cells(9).ReadOnly = True

                    Dgv_Pr.Rows(i).Cells(7).Value = ""
                    Dgv_Pr.Rows(i).Cells(8).Value = ""


                End If
            Next
        End If


    End Sub

    Private Sub Dgv_Pr_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Pr.CellContentClick

    End Sub

    Private Sub TolakPrToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TolakPrToolStripMenuItem.Click
        Dim currentRow = Dgv_Pr.CurrentRow.Index
        Dim currentCell = Dgv_Pr.CurrentCellAddress.X

        Try
            OpenConn()

            If CekButtonRole("Pengajuan_Batal_PR_Barang_Lain") = "T" Then
                MessageBox.Show("User Tidak Ada Akses Pengajuan Selesai PR", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Dim NoPR As String = Dgv_Pr.Rows(currentRow).Cells(cellNoPR).Value
        Dim UrutPR As String = Dgv_Pr.Rows(currentRow).Cells(cellNoUrutPR).Value
        Dim Lokasi As String = Dgv_Pr.Rows(currentRow).Cells(cellgudang).Value
        Dim KdBarang As String = Dgv_Pr.Rows(currentRow).Cells(cellKdBarang).Value
        Dim NmBarang As String = Dgv_Pr.Rows(currentRow).Cells(cellNmBarang).Value
        Dim SisaPR As String = Dgv_Pr.Rows(currentRow).Cells(cellSisa).Value
        Dim SatuanPR As String = Dgv_Pr.Rows(currentRow).Cells(cellSatuan).Value
        Dim Dtp_TglDelivery As String = Dgv_Pr.Rows(currentRow).Cells(cellTglDeliv).Value
        Dim Dtp_TglEstimasi As String = Dgv_Pr.Rows(currentRow).Cells(cellTglActDelivery).Value

        If Not String.IsNullOrEmpty(UrutPR) Then
            SD_Pengajuan_Selesai_PR_Barang_Lain.UrutPR = UrutPR
            SD_Pengajuan_Selesai_PR_Barang_Lain.Txt_NoPR.Text = NoPR
            SD_Pengajuan_Selesai_PR_Barang_Lain.Txt_KdSo.Text = Lokasi
            SD_Pengajuan_Selesai_PR_Barang_Lain.Txt_KdBrang.Text = KdBarang
            SD_Pengajuan_Selesai_PR_Barang_Lain.Txt_NmBarang.Text = NmBarang
            SD_Pengajuan_Selesai_PR_Barang_Lain.Txt_SisaPR.Text = Format(Val(HilangkanTanda(SisaPR)), "N2")
            SD_Pengajuan_Selesai_PR_Barang_Lain.DTP_TglDelivery.Value = Dtp_TglDelivery
            SD_Pengajuan_Selesai_PR_Barang_Lain.DTP_TglEstimasi.Value = Dtp_TglEstimasi

            SD_Pengajuan_Selesai_PR_Barang_Lain.Cmd_SatuanSisa.Items.Add(SatuanPR)
            SD_Pengajuan_Selesai_PR_Barang_Lain.Cmd_SatuanSisa.SelectedIndex = 0

            SD_Pengajuan_Selesai_PR_Barang_Lain.ShowDialog()

        Else
            MessageBox.Show("Pilih Dahulu Data yang Ingin di Ajukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If



    End Sub


End Class


