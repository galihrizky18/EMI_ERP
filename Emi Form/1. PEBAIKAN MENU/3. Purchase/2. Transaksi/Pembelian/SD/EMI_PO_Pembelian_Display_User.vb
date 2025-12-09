Public Class EMI_PO_Pembelian_Display_User

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
    Dim LvSatuanInput As String
    Dim LvJumlahInput As String

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
    Dim cellSatuanInput As Integer = 17
    Dim cellJumlahInput As Integer = 18

    Public asal_data As String = ""

    Dim arr2SatuanInput As New List(Of List(Of String))

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
        LvSatuanInput = Dgv_Pr.Rows(No_Index).Cells(cellSatuanInput).Value.ToString
        LvJumlahInput = Dgv_Pr.Rows(No_Index).Cells(cellJumlahInput).Value.ToString
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
        lvPO_Lokasi = EMI_PO_Pembelian.LvPO_DataPO.Items(No_Index).SubItems(cellPO_Lokasi).Text
        lvPO_KdBarang = EMI_PO_Pembelian.LvPO_DataPO.Items(No_Index).SubItems(cellPO_KdBarang).Text
        lvPO_NmBarang = EMI_PO_Pembelian.LvPO_DataPO.Items(No_Index).SubItems(cellPO_NmBarang).Text
        lvPO_Harga = EMI_PO_Pembelian.LvPO_DataPO.Items(No_Index).SubItems(cellPO_Harga).Text
        lvPO_Jumlah = EMI_PO_Pembelian.LvPO_DataPO.Items(No_Index).SubItems(cellPO_Jumlah).Text
        lvPO_Satuan = EMI_PO_Pembelian.LvPO_DataPO.Items(No_Index).SubItems(cellPO_Satuan).Text
        lvPO_Harga_SB = EMI_PO_Pembelian.LvPO_DataPO.Items(No_Index).SubItems(cellPO_Harga_SB).Text
        lvPO_Jumlah_SB = EMI_PO_Pembelian.LvPO_DataPO.Items(No_Index).SubItems(cellPO_Jumlah_SB).Text
        lvPO_Satuan_SB = EMI_PO_Pembelian.LvPO_DataPO.Items(No_Index).SubItems(cellPO_Satuan_SB).Text
        lvPO_NoPenawaran = EMI_PO_Pembelian.LvPO_DataPO.Items(No_Index).SubItems(cellPO_NoPenawaran).Text
        lvPO_ID = EMI_PO_Pembelian.LvPO_DataPO.Items(No_Index).SubItems(cellPO_ID).Text
        lvPO_Total = EMI_PO_Pembelian.LvPO_DataPO.Items(No_Index).SubItems(cellPO_Total).Text
        lvPO_Urut = EMI_PO_Pembelian.LvPO_DataPO.Items(No_Index).SubItems(cellPO_Urut).Text
        lvPO_PR = EMI_PO_Pembelian.LvPO_DataPO.Items(No_Index).SubItems(cellPO_PR).Text
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

        Dgv_Pr.Columns(cellJumlahInput).DisplayIndex = 7
        Dgv_Pr.Columns(cellSatuanInput).DisplayIndex = 8
        arr2SatuanInput.Clear()


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
            SQL = SQL & "b.Satuan, b.tanggal_delivery, b.no_urut, b.Jumlah, b.No_Penawaran, "

            SQL = SQL & "isnull((select  sum(y.Jumlah) from  EMI_Pembelian_PO_Induk x, EMI_Pembelian_PO_Det_Induk y where "
            SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur And "
            SQL = SQL & "y.Kode_Perusahaan = a.Kode_Perusahaan And y.no_urut_pr = b.No_Urut And x.status Is null And x.Flag_Release Is null ),0) As jumlah_Sementara, "

            SQL = SQL & "isnull((select  sum(y.Jumlah) from  EMI_Pembelian_PO_Induk x, EMI_Pembelian_PO_Det_Induk y where "
            SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur And "
            SQL = SQL & "y.Kode_Perusahaan = a.Kode_Perusahaan And y.no_urut_pr = b.No_Urut And x.status Is null And x.Flag_Release ='Y' ),0) as jumlah_Release, "

            SQL = SQL & "ISNULL((select waktu_pabrikasi from emi_detail_proses_pengiriman_po x, Suppliers y where "
            SQL = SQL & "b.Kode_Perusahaan = x.Kode_Perusahaan And b.Kode_Barang = x.kode_barang And "
            SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan And x.Id_Kategori_Supplier = y.ID_Kategori_Suppliers "
            SQL = SQL & "And y.Kode_Supplier = '" & EMI_PO_Pembelian.TxtPO_KdSupplier.Text & "'),0) as Waktu_Pabrikasi,  "

            SQL = SQL & "ISNULL((select  Waktu_Pengiriman from emi_detail_proses_pengiriman_po x, Suppliers y "
            SQL = SQL & "where b.Kode_Perusahaan = x.Kode_Perusahaan And b.Kode_Barang = x.kode_barang And "
            SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan And x.Id_Kategori_Supplier = y.ID_Kategori_Suppliers "
            SQL = SQL & "And y.Kode_Supplier = '" & EMI_PO_Pembelian.TxtPO_KdSupplier.Text & "' ),0) as Waktu_Pengiriman, "

            SQL = SQL & "ISNULL((select CASE WHEN b.No_Penawaran IS NULL THEN 0 ELSE z.Harga_Satuan END "
            SQL = SQL & "from EMI_Master_Penawaran_Detail z where "
            SQL = SQL & "z.No_Faktur = b.No_Penawaran And z.Kode_Barang = b.Kode_Barang And z.Mata_Uang = '" & MataUang & "'),0) as Harga_Satuan, "

            SQL = SQL & "ISNULL((select CASE WHEN b.No_Penawaran IS NULL THEN 0 ELSE z.Nilai_Barang END "
            SQL = SQL & "from EMI_Master_Penawaran_Detail z where "
            SQL = SQL & "z.No_Faktur = b.No_Penawaran And z.Kode_Barang = b.Kode_Barang And z.Mata_Uang = '" & MataUang & "'),0) as Nilai_Barang, "

            SQL = SQL & "(select Satuan_Barang "
            SQL = SQL & "from EMI_Master_Penawaran_Detail z where "
            SQL = SQL & "z.No_Faktur = b.No_Penawaran And z.Kode_Barang = b.Kode_Barang And z.Mata_Uang = '" & MataUang & "') as Satuan_Barang "

            SQL = SQL & "From EMI_Purchase_Requisition a, EMI_Purchase_Requisition_Detail b , barang c, Emi_Role_Kategori_PO d "
            SQL = SQL & "Where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur And "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.Kode_Barang = c.Kode_Barang And "
            SQL = SQL & "b.Kode_Stock_Owner = c.Kode_Stock_Owner And a.kode_perusahaan = '" & KodePerusahaan & "' and a.Status is null And b.No_Penawaran is not null "
            SQL = SQL & " And flag_release = 'Y' and c.kode_Perusahaan=d.kode_Perusahaan and "
            SQL = SQL & "c.id_kategori_PO = d.kategori_po And d.userid = '" & UserID & "' and b.flag_sudah_po is null and b.Flag_Pengajuan_Selesai is null "
            SQL = SQL & ") "

            SQL = SQL & "Select No_Faktur, Kode_Stock_Owner, Kode_Barang, Nama, satuan_kecil_barang, Satuan, Tanggal_Delivery, No_Urut, No_Penawaran, "
            SQL = SQL & "jumlah-(jumlah_Sementara + jumlah_Release) As Jumlah, Waktu_Pabrikasi, Waktu_Pengiriman, Harga_Satuan, Nilai_Barang, Satuan_Barang, "
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
                        Dgv_Pr.Rows(i).Cells(cellHarga).Value = Format(.Rows(i).Item("Harga_Satuan"), "N2")
                        Dgv_Pr.Rows(i).Cells(cellJumlah).Value = Format(0, "N2")
                        Dgv_Pr.Rows(i).Cells(cellSatuanPO).Value = .Rows(i).Item("satuan")
                        Dgv_Pr.Rows(i).Cells(cellNoPenawaran).Value = .Rows(i).Item("No_Penawaran")
                        Dgv_Pr.Rows(i).Cells(cellNoUrutPR).Value = .Rows(i).Item("no_urut")
                        Dgv_Pr.Rows(i).Cells(cellSatuanHarga).Value = .Rows(i).Item("Satuan_Barang")
                        Dgv_Pr.Rows(i).Cells(cellHargaId).Value = .Rows(i).Item("Nilai_Barang")
                        Dgv_Pr.Rows(i).Cells(cellSkBrg).Value = .Rows(i).Item("satuan_kecil_barang")
                        Dgv_Pr.Rows(i).Cells(cellTglDeliv).Value = Format(.Rows(i).Item("tanggal_delivery"), "dd MMM yyyy")
                        Dgv_Pr.Rows(i).Cells(cellSkBrg).Value = .Rows(i).Item("satuan_kecil_barang")
                        Dgv_Pr.Rows(i).Cells(cellWktPabrikasi).Value = .Rows(i).Item("Waktu_Proses_Pengiriman")
                        Dgv_Pr.Rows(i).Cells(cellTglActDelivery).Value = Format(.Rows(i).Item("tanggal_actual_delivery"), "dd MMM yyyy")

                        Dim subArrSatuanInput As New List(Of String)
                        subArrSatuanInput.Clear()
                        Dim dgvSatuanInput As DataGridViewComboBoxCell
                        dgvSatuanInput = Dgv_Pr.Rows(i).Cells(cellSatuanInput)
                        dgvSatuanInput.Items.Clear()

                        SQL = "select Satuan, Flag_Default from N_EMI_Master_Satuan where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang = '" & .Rows(i).Item("kode_barang") & "'"
                        Using Ds2 = BindingTrans(SQL)
                            If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1
                                    dgvSatuanInput.Items.Add(Ds2.Tables("MyTable").Rows(j).Item("Satuan"))
                                    subArrSatuanInput.Add(Ds2.Tables("MyTable").Rows(j).Item("Satuan"))
                                    If General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Flag_Default")) = "Y" Then
                                        dgvSatuanInput.Value = Ds2.Tables("MyTable").Rows(j).Item("Satuan")
                                    End If
                                Next
                            End If
                        End Using

                        arr2SatuanInput.Add(subArrSatuanInput)

                        Dgv_Pr.Rows(i).Cells(cellJumlahInput).Value = Format(0, "N2")


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

    Private Sub btnPilih_Click(sender As Object, e As EventArgs) Handles btnPilih.Click
        Dim Kode_Kategori_Besar As String = ""
        Dim Count_Valid_Data As Integer = 0

        For indexDisplayUserPO As Integer = 0 To Dgv_Pr.Rows.Count - 1

            Get_Isi_Listview(indexDisplayUserPO)

            '=======================================
            '     CEK APAKAH ADA DATA TERLEWAT     =
            '=======================================
            If Not Val(HilangkanTanda(lvJumlah)) = 0 Then
                Count_Valid_Data += 1

                If Val(HilangkanTanda(lvHarga)) = 0 Then
                    MessageBox.Show("Harga pada Baris ke -" & indexDisplayUserPO + 1 & " Tidak Boleh 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                If LvSatuanInput = "" Then
                    MessageBox.Show("Satuan Input pada Baris ke -" & indexDisplayUserPO + 1 & " Harus Pilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

            If Val(HilangkanTanda(Dgv_Pr.Rows(indexDisplayUserPO).Cells(cellJumlah).Value)) = 0 Then
                Continue For
            End If


            For i As Integer = 0 To EMI_PO_Pembelian.LvPO_DataPO.Items.Count - 1

                If EMI_PO_Pembelian.LvPO_DataPO.Items(i).SubItems(cellPO_KdBarang).Text = Dgv_Pr.Rows(indexDisplayUserPO).Cells(cellKdBarang).Value Then

                    'cek di lv emi po apakah sudah ada data yang sama atau belum
                    If EMI_PO_Pembelian.LvPO_DataPO.Items(i).SubItems(cellPO_Satuan).Text <> Dgv_Pr.Rows(indexDisplayUserPO).Cells(cellSatuanPO).Value Then
                        MessageBox.Show(Base_Language.Lang_Global_Satuan & " " & Base_Language.Lang_Global_Tidak_Bisa_Berbeda & ". . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    'cek di lv emi po apakah sudah ada data yang sama atau belum
                    If EMI_PO_Pembelian.LvPO_DataPO.Items(i).SubItems(cellPO_ID).Text = "T" Then

                        If EMI_PO_Pembelian.LvPO_DataPO.Items(i).SubItems(cellPO_NoPenawaran).Text = Dgv_Pr.Rows(indexDisplayUserPO).Cells(cellNoPenawaran).Value Then
                            If EMI_PO_Pembelian.LvPO_DataPO.Items(i).SubItems(cellPO_PR).Text = Dgv_Pr.Rows(indexDisplayUserPO).Cells(cellNoUrutPR).Value Then
                                MessageBox.Show(Base_Language.Lang_Global_Data_Sdh_Ada & ". . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End If
                    End If

                End If
            Next

        Next

        If Count_Valid_Data = 0 Then
            MessageBox.Show("Minimal ada 1 baris data yang harus diinputkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

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
                    SQL = "Select dbo.Ubah_Satuan('" & KodePerusahaan & "','UANG','" & lvKdBarang & "',"
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
                    SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & lvKdBarang & "',"
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
                    SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & lvKdBarang & "',"
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

                    SQL = "select a.Kode_Stock_Owner_Gudang From Binding_Lokasi_Gudang a where a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
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

                    '==============================
                    '=     GEK JENIS KATEGORI     =
                    '==============================
                    SQL = "select top 1 b.Kode_Kategori_Besar, b.Jenis_Kategori "
                    SQL = SQL & "from Barang a, Kategori_Besar b "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                    SQL = SQL & "and a.Kode_Kategori_Besar = b.Kode_Kategori_Besar "
                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Kode_Barang = '" & lvKdBarang & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then

                            If EMI_PO_Pembelian.LvPO_DataPO.Items.Count = 0 Then
                                If Kode_Kategori_Besar = "" Then
                                    Kode_Kategori_Besar = Dr("Jenis_Kategori")

                                    If Dr("Jenis_Kategori").ToString.ToUpper = "BARANG" Then
                                        'tempDataPajak.Clear()
                                        EMI_PO_Pembelian.tempDataPajak.RemoveAll(Function(item) item.isPPN = False)
                                    End If
                                Else
                                    If Not Kode_Kategori_Besar = Dr("Jenis_Kategori") Then
                                        Dr.Close()
                                        CloseConn()
                                        MessageBox.Show("Jenis Kategori Besar Tidak Boleh Berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            Else

                                If Not EMI_PO_Pembelian.LvPO_DataPO.Items(0).SubItems(EMI_PO_Pembelian.cellPO_JnsKategori).Text = Dr("Jenis_Kategori") Then
                                    Dr.Close()
                                    CloseConn()
                                    MessageBox.Show("Jenis Kategori Besar Tidak Boleh Berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                Else
                                    Kode_Kategori_Besar = Dr("Jenis_Kategori")
                                End If
                            End If
                        End If
                    End Using

                    Dim lvw As ListViewItem
                    lvw = EMI_PO_Pembelian.LvPO_DataPO.Items.Add(lokasi_gudang_bahan) '0
                    lvw.SubItems.Add(lvKdBarang) '1
                    lvw.SubItems.Add(lvNmBarang) '2
                    lvw.SubItems.Add(Format(Val(harga_satuan_besar), "N4")) '3
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

                    lvw.SubItems.Add(Format(Jumlah_satuan_Kecil * Val(LvHargaId), "N4")) '11
                    lvw.SubItems.Add("") '12
                    lvw.SubItems.Add(lvNoUrutPR) '13

                    Dim tempo_penawaran As String = ""
                    Dim jatuh_tempo As String = ""
                    SQL = "select a.No_Faktur, a.no_penawaran,a.Kode_Supplier, c.Nama,b.satuan, b.Nilai_Barang,b.harga_satuan, b.satuan_Barang,  "

                    SQL = SQL & "isnull((select top(1) x.Lama_Pembayaran from EMI_Master_Penawaran_Jatuh_Tempo x where a.Kode_Perusahaan = x.Kode_Perusahaan "
                    SQL = SQL & "and a.No_Faktur = x.No_Faktur), 0) as jatuh_Tempo,"

                    SQL = SQL & "isnull((select top(1) x.Tempo_Pembayaran from EMI_Master_Penawaran_Jatuh_Tempo x where a.Kode_Perusahaan = x.Kode_Perusahaan "
                    SQL = SQL & "and a.No_Faktur = x.No_Faktur), '') as Tempo_Pembayaran "

                    SQL = SQL & "from EMI_Master_Penawaran a, EMI_Master_Penawaran_Detail b, Suppliers c "
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

                    '==================================
                    '=     HARGA PER SATUAN DASAR     =
                    '==================================
                    Dim HargaInput As Double = 0
                    Dim HargaSatuanDasar As Double = 0
                    SQL = "select "
                    SQL = SQL & "b.Harga_Satuan as hasil, b.Nilai_Barang "
                    'If isSatuanDasar Then
                    'Else
                    '    SQL = SQL & "(b.Min_Order * b.Harga_Satuan) as hasil "
                    'End If
                    SQL = SQL & "from EMI_Master_Penawaran a, EMI_Master_Penawaran_Detail b "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                    SQL = SQL & "and a.No_Faktur = b.No_Faktur "
                    SQL = SQL & "and a.Status is null "
                    SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and a.No_Faktur= '" & lvNoPenawaran & "' "
                    SQL = SQL & "and b.Kode_Barang = '" & lvKdBarang & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            HargaInput = Val(HilangkanTanda(Dr("hasil")))
                            HargaSatuanDasar = Val(HilangkanTanda(Dr("Nilai_Barang")))
                        Else
                            Dr.Close()
                            CloseConn()
                            MessageBox.Show("Harga Penawaran Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    lvw.SubItems.Add(LvJumlahInput) '18
                    lvw.SubItems.Add(LvSatuanInput) '19
                    lvw.SubItems.Add(Format(Val(HargaInput), "N4")) '20

                    '=======================
                    '=     UBAH SATUAN     =
                    '=======================
                    Dim Nilai_Per_Satuan_Default As Double = 0
                    SQL = "select a.Nilai "
                    SQL = SQL & "from N_EMI_Master_Satuan a "
                    SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and a.Kode_Barang = '" & lvKdBarang & "' "
                    SQL = SQL & "and a.Satuan = '" & LvSatuanInput & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Nilai_Per_Satuan_Default = Dr("Nilai")
                        Else
                            CloseConn()
                            MessageBox.Show("Satuan Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    Dim Harga_Display As Double = HargaSatuanDasar * Nilai_Per_Satuan_Default

                    lvw.SubItems.Add(Format(Val(Harga_Display), "N4")) '21 ' HARGA DISPLAY

                End If

            Next
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        EMI_PO_Pembelian.HitungGrandTotal()
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


            '============================
            '=     GET SATUAN DASAR     =
            '============================
            Dim satuanDasar As String = ""
            SQL = "select Satuan from N_EMI_Master_Satuan "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Kode_Barang = '" & Dgv_Pr.CurrentRow.Cells(cellKdBarang).Value.ToString & "' "
            SQL = SQL & "and Flag_Dasar = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    satuanDasar = Dr("Satuan")
                Else
                    CloseConn()
                    MessageBox.Show("Satuan Dasar Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim satuanInputDefault As String = If(Dgv_Pr.CurrentRow.Cells(cellSatuanInput).Value.ToString Is Nothing Or Dgv_Pr.CurrentRow.Cells(cellSatuanInput).Value.ToString = "",
                    satuanDasar, Dgv_Pr.CurrentRow.Cells(cellSatuanInput).Value.ToString)

            Dim JumlahInput As Double = Val(HilangkanTanda(Dgv_Pr.CurrentRow.Cells(cellJumlahInput).Value))

            '=======================
            '=     UBAH SATUAN     =
            '=======================
            SQL = "select dbo.Ubah_Satuan_Baru('" & KodePerusahaan & "', '" & Dgv_Pr.CurrentRow.Cells(cellKdBarang).Value.ToString & "', "
            SQL = SQL & "'" & satuanInputDefault & "', '" & satuanDasar & "', " & JumlahInput & ", 'masa') as hasil"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("hasil")) = "" Then
                        CloseConn()
                        MessageBox.Show("Terjadi Kesalahan Saat Melakukan Convert Jumlah", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        Dgv_Pr.CurrentRow.Cells(cellJumlah).Value = Format(Dr("hasil"), "N2")
                    End If
                Else
                    CloseConn()
                    MessageBox.Show("Terjadi Kesalahan Saat Melakukan Convert Jumlah", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If Dgv_Pr.CurrentCell.ColumnIndex = cellJumlahInput Then


                Get_Isi_Listview(Dgv_Pr.CurrentRow.Index)

                Dim Jumlah_satuan_Kecil As Double = 0
                SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & lvKdBarang & "',"
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
                SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & lvKdBarang & "',"
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

                Dim Nilai_Per_Satuan_Default As Double = 0
                SQL = "select "
                SQL = SQL & "isnull(( select z.Satuan from N_EMI_Master_Satuan z where a.Kode_Perusahaan = z.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Barang = z.Kode_Barang "
                SQL = SQL & "and z.Flag_Dasar = 'Y' "
                SQL = SQL & "), '-') as Satuan_Dasar, a.Nilai "
                SQL = SQL & "from N_EMI_Master_Satuan a "
                SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Kode_Barang = '" & lvKdBarang & "' "
                SQL = SQL & "and a.Satuan = '" & satuanInputDefault & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Nilai_Per_Satuan_Default = Dr("Nilai")
                    Else
                        CloseConn()
                        MessageBox.Show("Satuan Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                If jumlah_sisa_satuan_kecil < Jumlah_satuan_Kecil Then

                    If Math.Abs((jumlah_sisa_satuan_kecil - Jumlah_satuan_Kecil)) > Nilai_Per_Satuan_Default - 1 Then

                        MessageBox.Show("Jumlah po tidak boleh lebih besar dari jumlah PR!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Dgv_Pr.CurrentCell.Value = Format(0, "N2")
                        Dgv_Pr.CurrentRow.Cells(cellJumlah).Value = Format(0, "N2")
                        CloseConn()
                        Exit Sub
                    End If


                End If
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

        If Dgv_Pr.CurrentCell.ColumnIndex = cellHarga Or Dgv_Pr.CurrentCell.ColumnIndex = cellJumlah Or Dgv_Pr.CurrentCell.ColumnIndex = cellJumlahInput Then

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




    Private Sub TolakPrToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TolakPrToolStripMenuItem.Click
        Dim currentRow = Dgv_Pr.CurrentRow.Index
        Dim currentCell = Dgv_Pr.CurrentCellAddress.X

        Try
            OpenConn()

            If CekButtonRole("Pengajuan_Batal_PR") = "T" Then
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
            SD_Pengajuan_Selesai_PR.UrutPR = UrutPR
            SD_Pengajuan_Selesai_PR.Txt_NoPR.Text = NoPR
            SD_Pengajuan_Selesai_PR.Txt_KdSo.Text = Lokasi
            SD_Pengajuan_Selesai_PR.Txt_KdBrang.Text = KdBarang
            SD_Pengajuan_Selesai_PR.Txt_NmBarang.Text = NmBarang
            SD_Pengajuan_Selesai_PR.Txt_SisaPR.Text = Format(Val(HilangkanTanda(SisaPR)), "N2")
            SD_Pengajuan_Selesai_PR.DTP_TglDelivery.Value = Dtp_TglDelivery
            SD_Pengajuan_Selesai_PR.DTP_TglEstimasi.Value = Dtp_TglEstimasi

            SD_Pengajuan_Selesai_PR.Cmd_SatuanSisa.Items.Add(SatuanPR)
            SD_Pengajuan_Selesai_PR.Cmd_SatuanSisa.SelectedIndex = 0

            SD_Pengajuan_Selesai_PR.ShowDialog()
        Else
            MessageBox.Show("Pilih Dahulu Data yang Ingin di Ajukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

    End Sub

    Private Sub Dgv_Pr_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Pr.CellClick
        '=====================================
        '=      BUKA COMBOBOX SAAT CLICK     =
        '=====================================
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            If e.RowIndex >= 0 AndAlso e.ColumnIndex = cellSatuanInput Then
                Dgv_Pr.BeginEdit(True)
                Dim combo = TryCast(Dgv_Pr.EditingControl, ComboBox)
                If combo IsNot Nothing Then combo.DroppedDown = True
            End If
        End If
    End Sub
End Class