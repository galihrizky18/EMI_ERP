Imports System.Security.Cryptography
Imports CrystalDecisions.CrystalReports.Engine

Public Class Hitung_HPP_Import
    Dim arrselisih, arrselisih_biaya As New ArrayList

    Public arrNoUrutBiaya, arrNilaiPakaiBiaya As New ArrayList
    Public arrNoUrutPO, arrNilaiPakaiPO As New ArrayList


    Dim faktur As String = ""
    Dim arrInisialFaktur As String = ""

    Dim LvLokasiAVG As String
    Dim LvKdBarangAVG As String
    Dim LvjenisAVG As String
    Dim LvBiayaImportAVG As String
    Dim LvBiayaImportAVGNew As String


    Dim Lv2LokasiTujuanAVG As String
    Dim Lv2KdBarangAVG As String
    Dim Lv2jenisAVG As String
    Dim Lv2BiayaImportAVG As String
    Dim Lv2BiayaImportWetDryAVG As String
    Dim Lv2BiayaImportAVGNew As String
    Dim Lv2BiayaImportWetDryAVGNew As String

    Dim LvLokasi As String
    Dim LvKdBarang As String
    Dim LvNama As String
    Dim LvJumlah As String
    Dim LvHarga As String
    Dim LvTotalHarga As String
    Dim LvVolume As String
    Dim LvJmlBsr As String
    Dim LvIsiBsr As String
    Dim LvBeratBrsh As String
    Dim LvBeratKtr As String
    Dim LvTotBeratBrsh As String
    Dim LvTotBeratKtr As String
    Dim LvPjg As String
    Dim LvLbr As String
    Dim LvTinggi As String
    Dim LvUrut As String
    Dim LvMataUang As String
    Dim LvPotStock As String
    Dim LvTdkPotStockLunas As String
    Dim LvTdkPotStockBlumLunasUtama As String
    Dim LvTdkPotStockBlumLunasPenolong As String
    Dim LvTotHPPBsr As String
    Dim LvTotHPPKcl As String
    Dim LvBiayaImport As String
    Dim LvKurs As String
    Dim LvColumn1 As String
    Dim LvColumn2 As String
    Dim LvColumn3 As String
    Dim LvBilling As String
    Dim LvBiayaKonte As String
    Dim LvBiayaFreight As String
    Dim LvHPP_Per_Pcs As String
    Dim LvSelisihPO As String
    Dim LvSelisihPO_biaya As String

    Dim cellLokasi As Integer = 0
    Dim cellKdBarang As Integer = 1
    Dim cellNama As Integer = 2
    Dim cellJumlah As Integer = 3
    Dim cellHarga As Integer = 4
    Dim cellTotalHarga As Integer = 5
    Dim cellVolume As Integer = 6
    Dim cellJmlBsr As Integer = 7
    Dim cellIsiBsr As Integer = 8
    Dim cellBeratBrsh As Integer = 9
    Dim cellBeratKtr As Integer = 10
    Dim cellTotBeratBrsh As Integer = 11
    Dim cellTotBeratKtr As Integer = 12
    Dim cellPjg As Integer = 13
    Dim cellLbr As Integer = 14
    Dim cellTinggi As Integer = 15
    Dim cellUrut As Integer = 16
    Dim cellMataUang As Integer = 17
    Dim cellPotStock As Integer = 18
    Dim cellTdkPotStockLunas As Integer = 19
    Dim cellTdkPotStockBlumLunasUtama As Integer = 20
    Dim cellTdkPotStockBlumLunasPenolong As Integer = 21
    Dim cellTotHPPBsr As Integer = 22
    Dim cellTotHPPKcl As Integer = 23
    Dim cellBiayaImport As Integer = 24
    Dim cellKurs As Integer = 25
    Dim cellColumn1 As Integer = 26
    Dim cellColumn2 As Integer = 27
    Dim cellColumn3 As Integer = 28
    Dim cellBilling As Integer = 29
    Dim cellBiayaKonte As Integer = 30
    Dim cellBiayaFreight As Integer = 31
    Dim cellHPP_Per_Pcs As Integer = 32
    Dim cellSelisihPO As Integer = 33
    Dim cellSelisihPO_biaya As Integer = 34

    Dim Lv2Lokasi As String
    Dim Lv2LokasiTujuan As String
    Dim Lv2KdBarang As String
    Dim Lv2Nama As String
    Dim Lv2Jumlah As String
    Dim Lv2Harga As String
    Dim Lv2TotalHarga As String
    Dim Lv2Volume As String
    Dim Lv2JmlBsr As String
    Dim Lv2IsiBsr As String
    Dim Lv2BeratBrsh As String
    Dim Lv2BeratKtr As String
    Dim Lv2TotBeratBrsh As String
    Dim Lv2TotBeratKtr As String
    Dim Lv2Pjg As String
    Dim Lv2Lbr As String
    Dim Lv2Tinggi As String
    Dim Lv2HPP_Per_Pcs_ktr As String
    Dim Lv2BiayaImport As String
    Dim Lv2BiayaImportWetDry As String
    Dim Lv2Input As String
    Dim Lv2HPP_Per_Pcs_brsh As String
    Dim Lv2lkstujuanInduk As String

    Dim cell2Lokasi As Integer = 0
    Dim cell2LokasiTujuan As Integer = 1
    Dim cell2KdBarang As Integer = 2
    Dim cell2Nama As Integer = 3
    Dim cell2Jumlah As Integer = 4
    Dim cell2Harga As Integer = 5
    Dim cell2TotalHarga As Integer = 6
    Dim cell2Volume As Integer = 7
    Dim cell2JmlBsr As Integer = 8
    Dim cell2IsiBsr As Integer = 9
    Dim cell2BeratBrsh As Integer = 10
    Dim cell2BeratKtr As Integer = 11
    Dim cell2TotBeratBrsh As Integer = 12
    Dim cell2TotBeratKtr As Integer = 13
    Dim cell2Pjg As Integer = 14
    Dim cell2Lbr As Integer = 15
    Dim cell2Tinggi As Integer = 16
    Dim cell2HPP_Per_Pcs_ktr As Integer = 17
    Dim cell2BiayaImport As Integer = 18
    Dim cell2BiayaImportWetDry As Integer = 19
    Dim cell2Input As Integer = 20
    Dim cell2HPP_Per_Pcs_brsh As Integer = 21
    Dim cell2lkstujuanInduk As Integer = 22

    Dim id_rencana_group As String
    Dim Kode_Unik As String
    Dim hitung As Long = 0
    Dim _filter_tambahan As String = "and isnull((select X.Id_rencana_induk from rencana_order_gabungan X where ro.Flag_Gabungan = 'Y' and ro.ID_Rencana = X.ID_Rencana ),ro.id_rencana) = ro.id_rencana and ro.flag_submit_po = 'Y' and ro.flag_loading_barang = 'Y' and ro.flag_otw = 'Y' and ro.Flag_Draft = 'Y' and ro.Flag_Final = 'Y' and ro.Flag_Kirim = 'Y' and ro.Flag_Finish = 'Y' and ro.flag_kapal_tiba = 'Y' and ro.flag_penjaluran = 'Y' and ro.flag_sppb = 'Y' and ro.flag_bongkar = 'Y' and ro.flag_sudah_transaksi = 'Y' and ro.flag_sudah_transaksi3 = 'Y' and ro.Flag_Lokasi_Tujuan = 'Y' and ro.flag_billing = 'Y' and ro.flag_hpp is null " 'and

    Dim IsError As Boolean = False

    Public Sub Get_Isi_ListviewAVG(ByVal No_Index As Integer)

        LvLokasiAVG = DataGridViewavg1.Rows(No_Index).Cells(0).Value.ToString
        LvKdBarangAVG = DataGridViewavg1.Rows(No_Index).Cells(1).Value.ToString
        LvjenisAVG = DataGridViewavg1.Rows(No_Index).Cells(2).Value.ToString
        LvBiayaImportAVG = DataGridViewavg1.Rows(No_Index).Cells(3).Value.ToString
        LvBiayaImportAVGNew = DataGridViewavg1.Rows(No_Index).Cells(4).Value.ToString

    End Sub

    Public Sub Get_Isi_Listview2AVG(ByVal No_Index As Integer)

        Lv2LokasiTujuanAVG = DataGridViewavg2.Rows(No_Index).Cells(0).Value.ToString
        Lv2KdBarangAVG = DataGridViewavg2.Rows(No_Index).Cells(1).Value.ToString
        Lv2jenisAVG = DataGridViewavg2.Rows(No_Index).Cells(2).Value.ToString
        Lv2BiayaImportAVG = DataGridViewavg2.Rows(No_Index).Cells(3).Value.ToString
        Lv2BiayaImportAVGNew = DataGridViewavg2.Rows(No_Index).Cells(4).Value.ToString
        Lv2BiayaImportWetDryAVG = DataGridViewavg2.Rows(No_Index).Cells(5).Value.ToString
        Lv2BiayaImportWetDryAVGNew = DataGridViewavg2.Rows(No_Index).Cells(6).Value.ToString

    End Sub

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)

        LvLokasi = DataGridView1.Rows(No_Index).Cells(cellLokasi).Value.ToString
        LvKdBarang = DataGridView1.Rows(No_Index).Cells(cellKdBarang).Value.ToString
        LvNama = DataGridView1.Rows(No_Index).Cells(cellNama).Value.ToString
        LvJumlah = DataGridView1.Rows(No_Index).Cells(cellJumlah).Value.ToString
        LvHarga = DataGridView1.Rows(No_Index).Cells(cellHarga).Value.ToString
        LvTotalHarga = DataGridView1.Rows(No_Index).Cells(cellTotalHarga).Value.ToString
        LvVolume = DataGridView1.Rows(No_Index).Cells(cellVolume).Value.ToString
        LvJmlBsr = DataGridView1.Rows(No_Index).Cells(cellJmlBsr).Value.ToString
        LvIsiBsr = DataGridView1.Rows(No_Index).Cells(cellIsiBsr).Value.ToString
        LvBeratBrsh = DataGridView1.Rows(No_Index).Cells(cellBeratBrsh).Value.ToString
        LvBeratKtr = DataGridView1.Rows(No_Index).Cells(cellBeratKtr).Value.ToString
        LvTotBeratBrsh = DataGridView1.Rows(No_Index).Cells(cellTotBeratBrsh).Value.ToString
        LvTotBeratKtr = DataGridView1.Rows(No_Index).Cells(cellTotBeratKtr).Value.ToString
        LvPjg = DataGridView1.Rows(No_Index).Cells(cellPjg).Value.ToString
        LvLbr = DataGridView1.Rows(No_Index).Cells(cellLbr).Value.ToString
        LvTinggi = DataGridView1.Rows(No_Index).Cells(cellTinggi).Value.ToString
        LvUrut = DataGridView1.Rows(No_Index).Cells(cellUrut).Value.ToString
        LvMataUang = DataGridView1.Rows(No_Index).Cells(cellMataUang).Value.ToString
        LvPotStock = DataGridView1.Rows(No_Index).Cells(cellPotStock).Value.ToString
        LvTdkPotStockLunas = DataGridView1.Rows(No_Index).Cells(cellTdkPotStockLunas).Value.ToString
        LvTdkPotStockBlumLunasUtama = DataGridView1.Rows(No_Index).Cells(cellTdkPotStockBlumLunasUtama).Value.ToString
        LvTdkPotStockBlumLunasPenolong = DataGridView1.Rows(No_Index).Cells(cellTdkPotStockBlumLunasPenolong).Value.ToString
        LvTotHPPBsr = DataGridView1.Rows(No_Index).Cells(cellTotHPPBsr).Value.ToString
        LvTotHPPKcl = DataGridView1.Rows(No_Index).Cells(cellTotHPPKcl).Value.ToString
        LvBiayaImport = DataGridView1.Rows(No_Index).Cells(cellBiayaImport).Value.ToString
        LvKurs = DataGridView1.Rows(No_Index).Cells(cellKurs).Value.ToString
        LvColumn1 = DataGridView1.Rows(No_Index).Cells(cellColumn1).Value.ToString
        LvColumn2 = DataGridView1.Rows(No_Index).Cells(cellColumn2).Value.ToString
        LvColumn3 = DataGridView1.Rows(No_Index).Cells(cellColumn3).Value.ToString
        LvBilling = DataGridView1.Rows(No_Index).Cells(cellBilling).Value.ToString
        LvBiayaKonte = DataGridView1.Rows(No_Index).Cells(cellBiayaKonte).Value.ToString
        LvBiayaFreight = DataGridView1.Rows(No_Index).Cells(cellBiayaFreight).Value.ToString
        LvHPP_Per_Pcs = DataGridView1.Rows(No_Index).Cells(cellHPP_Per_Pcs).Value.ToString
        LvSelisihPO = DataGridView1.Rows(No_Index).Cells(cellSelisihPO).Value.ToString
        LvSelisihPO_biaya = DataGridView1.Rows(No_Index).Cells(cellSelisihPO_biaya).Value.ToString
    End Sub

    Public Sub Get_Isi_Listview2(ByVal No_Index As Integer)

        Lv2Lokasi = DataGridView2.Rows(No_Index).Cells(cell2Lokasi).Value.ToString
        Lv2LokasiTujuan = DataGridView2.Rows(No_Index).Cells(cell2LokasiTujuan).Value.ToString
        Lv2KdBarang = DataGridView2.Rows(No_Index).Cells(cell2KdBarang).Value.ToString
        Lv2Nama = DataGridView2.Rows(No_Index).Cells(cell2Nama).Value.ToString
        Lv2Jumlah = DataGridView2.Rows(No_Index).Cells(cell2Jumlah).Value.ToString
        Lv2Harga = DataGridView2.Rows(No_Index).Cells(cell2Harga).Value.ToString
        Lv2TotalHarga = DataGridView2.Rows(No_Index).Cells(cell2TotalHarga).Value.ToString
        Lv2Volume = DataGridView2.Rows(No_Index).Cells(cell2Volume).Value.ToString
        Lv2JmlBsr = DataGridView2.Rows(No_Index).Cells(cell2JmlBsr).Value.ToString
        Lv2IsiBsr = DataGridView2.Rows(No_Index).Cells(cell2IsiBsr).Value.ToString
        Lv2BeratBrsh = DataGridView2.Rows(No_Index).Cells(cell2BeratBrsh).Value.ToString
        Lv2BeratKtr = DataGridView2.Rows(No_Index).Cells(cell2BeratKtr).Value.ToString
        Lv2TotBeratBrsh = DataGridView2.Rows(No_Index).Cells(cell2TotBeratBrsh).Value.ToString
        Lv2TotBeratKtr = DataGridView2.Rows(No_Index).Cells(cell2TotBeratKtr).Value.ToString
        Lv2Pjg = DataGridView2.Rows(No_Index).Cells(cell2Pjg).Value.ToString
        Lv2Lbr = DataGridView2.Rows(No_Index).Cells(cell2Lbr).Value.ToString
        Lv2Tinggi = DataGridView2.Rows(No_Index).Cells(cell2Tinggi).Value.ToString
        Lv2HPP_Per_Pcs_ktr = DataGridView2.Rows(No_Index).Cells(cell2HPP_Per_Pcs_ktr).Value.ToString
        Lv2BiayaImport = DataGridView2.Rows(No_Index).Cells(cell2BiayaImport).Value.ToString
        Lv2BiayaImportWetDry = DataGridView2.Rows(No_Index).Cells(cell2BiayaImportWetDry).Value.ToString
        Lv2Input = DataGridView2.Rows(No_Index).Cells(cell2Input).Value.ToString
        Lv2HPP_Per_Pcs_brsh = DataGridView2.Rows(No_Index).Cells(cell2HPP_Per_Pcs_brsh).Value.ToString
        Lv2lkstujuanInduk = DataGridView2.Rows(No_Index).Cells(cell2lkstujuanInduk).Value.ToString
    End Sub

    Private Sub HitungGrand()

        Dim ttl As Double = 0

        For i As Integer = 0 To DataGridView3.Rows.Count - 1
            ttl = ttl + Val(HilangkanTanda(DataGridView3.Rows.Item(i).Cells(8).Value))
        Next

        TextBoxBiayaStorage.Text = Format(ttl, "N2")
    End Sub

    Private Sub get_Faktur()
        faktur = FHPP & arrInisialFaktur & "-" & Format(Tanggal_Sekarang, "MM/yy") & "-" &
                            General_Class.Get_Last_Number2("HPP_Import", "No_Faktur", JumlahDigit,
                            "Kode_perusahaan", KodePerusahaan,
                            "And", "substring(No_Faktur,1," & Len(FHPP) + Len(arrInisialFaktur) + 6 & ")",
                             FHPP & arrInisialFaktur & "-" & Format(Tanggal_Sekarang, "MM/yy"))

    End Sub

    Private Sub kosong_mata_uang()
        ListView1.Items.Clear()
        TextBox2.Text = ""
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
    End Sub

    Private Sub ambil_selisih_PO()
        Try

            OpenConn()

            DataGridView1.Rows.Clear()
            Dim Kode_sup As String = ""

            SQL = "select cast(RV as bigint) as rvx, biaya_form_e, b.Flag_Average, b.Metode_Selisih_Declare, b.Kode_Supplier from rencana_order a, Suppliers b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and a.id_rencana = '" & TxtId_Rencana.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Kode_sup = Dr("Kode_Supplier")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Id Rencana tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            arrNoUrutPO.Clear()
            arrNilaiPakaiPO.Clear()
            TxtJmlPakaiPO.Clear()

            Dim total As Double = 0



            SQL = "select a.no_val,a.id_rencana ,round(a.sisa,0) as sisa,a.urut from  Selisih_Kurs_hutang_loading_barang a, rencana_order b "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Rencana=b.ID_rencana and a.kode_perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & "and a.flag_pakai is null and round(a.sisa,0) <> 0 and a.status is null "
            SQL = SQL & "and b.Lokasi='" & CmbLokasi.Text & "' and b.Kode_Supplier='" & Kode_sup & "' "
            SQL = SQL & "order by no_val asc"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For index As Integer = 0 To .Rows.Count - 1


                        total += .Rows(index).Item("sisa")
                        arrNilaiPakaiPO.Add(.Rows(index).Item("sisa"))
                        arrNoUrutPO.Add(.Rows(index).Item("urut"))
                    Next
                End With
            End Using

            CloseConn()

            TxtJmlPakaiPO.Text = Format(total, "N0")
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub ambil_selisih_Biaya()
        Try

            OpenConn()

            Dim Kode_sup As String = ""

            SQL = "select cast(RV as bigint) as rvx, biaya_form_e, b.Flag_Average, b.Metode_Selisih_Declare, b.Kode_Supplier from rencana_order a, Suppliers b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and a.id_rencana = '" & TxtId_Rencana.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Kode_sup = Dr("Kode_Supplier")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Id Rencana tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            arrNoUrutBiaya.Clear()
            arrNilaiPakaiBiaya.Clear()
            txtJmlPakaiBiaya.Clear()

            Dim total As Double = 0


            SQL = "select a.no_val,a.id_rencana ,round(a.sisa,0) as sisa ,a.urut from  Selisih_Kurs_Biaya_Import_By_Perusahaan a, rencana_order b "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Rencana=b.ID_rencana and a.kode_perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & "and a.flag_pakai is null and round(a.sisa,0) <> 0  and a.status is null "
            SQL = SQL & "and b.Lokasi='" & CmbLokasi.Text & "' and b.Kode_Supplier='" & Kode_sup & "' "
            SQL = SQL & "order by no_val asc"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For index As Integer = 0 To .Rows.Count - 1


                        total += .Rows(index).Item("sisa")
                        arrNilaiPakaiBiaya.Add(.Rows(index).Item("sisa"))
                        arrNoUrutBiaya.Add(.Rows(index).Item("urut"))
                    Next
                End With
            End Using

            CloseConn()

            txtJmlPakaiBiaya.Text = Format(total, "N0")
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub
    Private Sub Hitung_HPP_Import_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Rencana_order_Biaya_Import_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        DataGridView1.Columns(33).DisplayIndex = 22

        ListView1.Clear()
        ListView1.Columns.Add("Mata Uang", 140, HorizontalAlignment.Center)
        ListView1.Columns.Add("Jenis", 140, HorizontalAlignment.Center)
        ListView1.Columns.Add("Nilai Kurs", 150, HorizontalAlignment.Right)
        Kosong()
        kosong_mata_uang()
    End Sub


    Public Sub Kosong()

        arrselisih.Clear()
        arrselisih_biaya.Clear()
        arrNoUrutBiaya.Clear()
        arrNilaiPakaiBiaya.Clear()
        arrNoUrutPO.Clear()
        arrNilaiPakaiPO.Clear()

        GetTime()
        Dim Rand As New Random
        Kode_Unik = Format(Rand.Next(0, 999), "000") & Format(Tanggal_Sekarang, "ddMMyyHHmmss")

        TxtContainer.Text = ""
        TxtId_Rencana.Text = ""
        TxtJumlah_conte.Text = ""
        TxtSupplier.Text = ""
        TextBox1.Text = ""
        'TextBox4.Text = ""
        'TextBox6.Text = ""
        TextBoxRV.Text = ""

        id_rencana_group = ""
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
        'ComboBox3.SelectedIndex = -1
        DtTanggal_Po.Value = FMenu.ToolStripStatusLabel3.Text

        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()

        txtJmlPakaiBiaya.Text = 0
        TxtJmlPakaiPO.Text = 0

        'txtJmlPakaiBiaya.Enabled = True
        'TxtJmlPakaiPO.Enabled = True

        'Column22.Visible = False
        'Column23.Visible = False

        DataGridView1.Columns.Item(cellColumn1).Visible = False
        DataGridView1.Columns.Item(cellColumn2).Visible = False


        btnSelisihPO.Enabled = False
        btnSelisihBiaya.Enabled = False

        Try
            OpenConn()

            CmbLokasi.Items.Clear()
            SQL = "Select Kode_stock_owner From stock_owner where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbLokasi.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using


            ComboBox1.Items.Clear()
            SQL = "Select Kode_mata_uang From mata_uang where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_mata_uang"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox1.Items.Add(dr("Kode_Mata_Uang"))
                Loop
            End Using

            ComboBox2.Items.Clear()
            ComboBox2.Items.Add("UTAMA")
            ComboBox2.Items.Add("PENOLONG")
            ComboBox2.Items.Add("FREIGHT")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        CmbLokasi.Text = Lokasi
    End Sub

    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Kosong()
        kosong_mata_uang()
    End Sub

    Private Sub BtCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtCari.Click

        Display_Rencana_Order_Lain_Lain.dari_mana = "HITUNG_HPP"
        Display_Rencana_Order_Lain_Lain.filter_tambahan = _filter_tambahan
        Display_Rencana_Order_Lain_Lain.ShowDialog()

    End Sub

    Private Sub BtnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSimpan.Click
        If ListView1.Items.Count = 0 Then
            MessageBox.Show("Tidak Ada Kurs yang di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ListView1.Focus()
            Exit Sub

            'ElseIf TextBox4.Text = "" Then
            '    MessageBox.Show("Nomor BL Harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    TextBox4.Focus()
            '    Exit Sub
            'ElseIf ComboBox2.SelectedIndex = -1 Then
            '    MessageBox.Show("No Rekening Harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    ComboBox2.Focus()
            '    Exit Sub
            'ElseIf ComboBox3.SelectedIndex = -1 Then
            '    MessageBox.Show("Jenis_Transaksi Harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    ComboBox3.Focus()
            'Exit Sub
        End If


        For index As Integer = 0 To DataGridView1.Rows.Count - 1
            Get_Isi_Listview(index)
            'MessageBox.Show(Val(HilangkanTanda(LvTdkPotStockBlumLunas)))
            'Exit Sub
            If LvTdkPotStockBlumLunasUtama < 0 Or LvTdkPotStockBlumLunasPenolong < 0 Or LvBiayaFreight < 0 Then

                MessageBox.Show("Proses tidak dapat dilanjutkan karena ada mata uang yang belum di input!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

        Next

        GetTime()
        Try
            OpenConn()
            Cmd.Transaction() = Cn.BeginTransaction


            get_Faktur()
            'cek faktur
            SQL = "select inisial_faktur from stock_owner "
            SQL = SQL & "where Kode_stock_Owner = '" & CmbLokasi.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    arrInisialFaktur = dr("inisial_faktur")
                Else
                    CloseConn()
                    MessageBox.Show("Inisial Faktur Tidak ditemukan")
                    Exit Sub
                End If
            End Using


            Dim Metode_Hitung_Selisih As String = ""
            Dim kode_sup As String = ""

            SQL = "select cast(RV as bigint) as rvx, b.Flag_Average, b.Metode_Selisih_Declare, b.Kode_Supplier from rencana_order a, Suppliers b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and a.id_rencana = '" & TxtId_Rencana.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    kode_sup = Dr("Kode_Supplier")
                    Metode_Hitung_Selisih = Dr("Metode_Selisih_Declare")

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Id Rencana tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim ttl_selisihPO As Double = 0
            Dim ttl_selisihbiaya As Double = 0
            For index As Integer = 0 To arrNilaiPakaiPO.Count - 1
                ttl_selisihPO = ttl_selisihPO + arrNilaiPakaiPO.Item(index)
            Next

            If ttl_selisihPO <> Val(HilangkanTanda(TxtJmlPakaiPO.Text)) Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Terjadi kesalahan pada selisih . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            For index As Integer = 0 To arrNilaiPakaiBiaya.Count - 1
                ttl_selisihbiaya = ttl_selisihbiaya + arrNilaiPakaiBiaya.Item(index)
            Next

            If ttl_selisihbiaya <> Val(HilangkanTanda(txtJmlPakaiBiaya.Text)) Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Terjadi kesalahan pada selisih . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            For index As Integer = 0 To arrNilaiPakaiPO.Count - 1

                SQL = "select nilai as selisih,urut, a.id_rencana, Kategori from Selisih_Kurs_Hutang_Loading_Barang a, Rencana_Order b "
                SQL = SQL & "where a.Kode_Perusahaan =b.Kode_Perusahaan and "
                SQL = SQL & "a.Id_Rencana = b.Id_rencana And flag_pakai Is null "
                SQL = SQL & "and b.Kode_supplier='" & kode_sup & "' and b.Lokasi='" & CmbLokasi.Text & "' "
                SQL = SQL & "and a.Status is null and b.Status is null and a.urut ='" & arrNoUrutPO.Item(index) & "'"

                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        SQL = "insert into HPP_Import_Log_Selisih(Kode_Perusahaan, No_Faktur, Id_rencana_Selisih, Nilai_Selisih, Jenis, No_Urut_Selisih) values"
                        SQL = SQL & "('" & KodePerusahaan & "', '" & faktur & "','" & Dr("id_rencana") & "', '" & arrNilaiPakaiPO.Item(index) & "', '" & Dr("kategori") & "', '" & arrNoUrutPO.Item(index) & "')"
                        Dr.Close()
                        ExecuteTrans(SQL)

                        SQL = "update selisih_kurs_hutang_loading_barang set "
                        SQL = SQL & "sisa = sisa - (" & arrNilaiPakaiPO.Item(index) & ") "
                        SQL = SQL & "where urut = '" & arrNoUrutPO.Item(index) & "'"
                        ExecuteTrans(SQL)


                        'SQL = "Update Selisih_Kurs_Hutang_Loading_Barang set Flag_Pakai ='Y', "
                        'SQL = SQL & "ID_Rencana_Pakai ='" & TxtId_Rencana.Text & "', "
                        'SQL = SQL & "UserID ='" & UserID & "', "
                        'SQL = SQL & "Tanggal ='" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', "
                        'SQL = SQL & "Jam ='" & Format(Tanggal_Sekarang, "HH:mm:ss") & "' "
                        'SQL = SQL & "where Urut ='" & arrselisih.Item(index) & "'"
                        'ExecuteTrans(SQL)

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Id Rencana Selisih tidak di temukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                End Using

            Next

            For index As Integer = 0 To arrNilaiPakaiBiaya.Count - 1

                SQL = "select nilai as selisih,urut, a.id_rencana, Kode_Perusahaan_Biaya_IMport from Selisih_Kurs_Biaya_Import_By_Perusahaan a, Rencana_Order b "
                SQL = SQL & "where a.Kode_Perusahaan =b.Kode_Perusahaan and "
                SQL = SQL & "a.Id_Rencana = b.Id_rencana And flag_pakai Is null "
                SQL = SQL & "and b.Kode_supplier='" & kode_sup & "' and b.Lokasi='" & CmbLokasi.Text & "' "
                SQL = SQL & "and a.Status is null and b.Status is null and a.urut ='" & arrNoUrutBiaya.Item(index) & "'"

                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then

                        SQL = "insert into HPP_Import_Log_Selisih(Kode_Perusahaan, No_Faktur, Id_rencana_Selisih, Nilai_Selisih, Jenis, No_Urut_Selisih) values"
                        SQL = SQL & "('" & KodePerusahaan & "', '" & faktur & "','" & Dr("id_rencana") & "', '" & arrNilaiPakaiBiaya.Item(index) & "', '" & Dr("Kode_Perusahaan_Biaya_IMport") & "', '" & arrNoUrutBiaya.Item(index) & "')"
                        Dr.Close()
                        ExecuteTrans(SQL)

                        SQL = "update Selisih_Kurs_Biaya_Import_By_Perusahaan set  "
                        SQL = SQL & "sisa = sisa - (" & arrNilaiPakaiBiaya.Item(index) & ") "
                        SQL = SQL & "where urut = '" & arrNoUrutBiaya.Item(index) & "' "
                        ExecuteTrans(SQL)

                        'SQL = "Update Selisih_Kurs_Hutang_Loading_Barang set Flag_Pakai ='Y', "
                        'SQL = SQL & "ID_Rencana_Pakai ='" & TxtId_Rencana.Text & "', "
                        'SQL = SQL & "UserID ='" & UserID & "', "
                        'SQL = SQL & "Tanggal ='" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', "
                        'SQL = SQL & "Jam ='" & Format(Tanggal_Sekarang, "HH:mm:ss") & "' "
                        'SQL = SQL & "where Urut ='" & arrselisih_biaya.Item(index) & "'"
                        'ExecuteTrans(SQL)

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Id Rencana Selisih tidak di temukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                End Using

            Next



            SQL = "select cast(rv as bigint) as rv, lokasi from rencana_order ro where "
            SQL = SQL & "id_rencana = '" & TxtId_Rencana.Text & "' and selesai is null and "
            SQL = SQL & "status is null " & _filter_tambahan
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("rv") <> TextBoxRV.Text Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini sudah diubah sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    ElseIf Dr("lokasi").ToString.ToUpper <> CmbLokasi.Text.ToUpper Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi kesalahan pada lokasi! Harap login ulang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Rencana order tidak ditemukan/sudah selesai/sudah batal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End Using

            Dim id_rencana_group As String = ""

            Dim rr As Integer = 0
            SQL = "select Flag_Gabungan from rencana_order where "
            SQL = SQL & "Id_rencana = '" & TxtId_Rencana.Text & "'"
            Using Dr2 = OpenTrans(SQL)
                If Dr2.Read Then
                    If General_Class.CekNULL(Dr2("Flag_Gabungan")) = "Y" Then
                        Dr2.Close()
                        SQL = "select a.id_rencana from rencana_order a, rencana_order_gabungan b where "
                        SQL = SQL & "a.id_rencana = b.Id_rencana and b.Id_rencana_induk = '" & TxtId_Rencana.Text & "'"
                        Using Dr = OpenTrans(SQL)
                            Do While Dr.Read
                                If rr <> 0 Then
                                    id_rencana_group = id_rencana_group & ", "
                                End If
                                id_rencana_group = id_rencana_group & "'" & Dr("id_rencana") & "'"
                                rr += 1
                            Loop
                        End Using
                    Else
                        id_rencana_group = "'" & TxtId_Rencana.Text & "'"
                    End If
                Else
                    Dr2.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Id Rencana tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim No_PO As String = ""
            SQL = "select string_agg(''''+no_faktur+'''', ', ') as No_faktur "
            SQL = SQL & "from submit_po where id_rencana in(" & id_rencana_group & ") and status is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    No_PO = Dr("No_faktur")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No PO tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End Using


            SQL = "Select Flag_HPP, status from Rencana_Order where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Rencana in(" & id_rencana_group & ")"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Flag_HPP")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Perhitungan HPP sudah pernah dilakukan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    ElseIf General_Class.CekNULL(Dr("status")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Rencana order ini sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Rencana order tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End Using


            SQL = "insert into HPP_Import(kode_perusahaan, no_faktur, Id_rencana, tanggal, jam, UserID, "
            SQL = SQL & " Kode_Supplier, Grand_total, Baru) "
            SQL = SQL & "values('" & KodePerusahaan & "','" & faktur & "','" & TxtId_Rencana.Text & "', '" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(Tanggal_Sekarang, "HH:mm:ss") & "',"
            SQL = SQL & "'" & UserID & "', "
            SQL = SQL & "'" & TextBox1.Text & "', NULL,'Y')"
            ExecuteTrans(SQL)

            'insert ke detail_submit_po
            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                Get_Isi_Listview(i)
                'Get_Isi_ListviewAVG(i)


                'If LvLokasi <> LvLokasiAVG Or LvKdBarang <> LvKdBarangAVG Then
                '    CloseTrans()
                '    CloseConn()
                '    MessageBox.Show("Terjadi Kesalahan Pada Perhitungan . .  !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '    Exit Sub
                'End If

                SQL = "insert into detail_HPP_Import (kode_perusahaan, No_faktur, Kode_Stock_Owner, Kode_Barang, Jumlah, Harga, Total_Harga, "
                SQL = SQL & "Volume, Jml_Satuan_Besar, Isi_Satuan_Besar, Berat_Bersih, Berat_Kotor, Total_Berat_Bersih, "
                SQL = SQL & " Total_Berat_kotor, Panjang, Lebar, Tinggi, Urut_Rencana, Mata_Uang, Nilai_Pot_Stock, Nilai_Tdk_Pot_stock_LNS, "
                SQL = SQL & "Nilai_Tdk_Pot_stock_HTG_Utama, Nilai_Tdk_Pot_stock_HTG_Penolong, Nilai_HPP_Besar, Nilai_HPP_Kecil, Nilai1, Nilai2, PPH29, Biaya_Import, "
                SQL = SQL & "Biaya_Billing, Biaya_Kontainer, Biaya_Freight_int, Nilai_HPP_Barang_Per_Pcs, Kurs_Nilai1, Nilai_Selisih_PO, Nilai_Selisih_PO_Biaya) values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & faktur & "', "
                SQL = SQL & "'" & LvLokasi & "', '" & LvKdBarang & "', "
                SQL = SQL & "'" & LvJumlah & "', '" & HilangkanTanda(LvHarga) & "', '" & HilangkanTanda(LvTotalHarga) & "', "
                SQL = SQL & "'" & HilangkanTanda(LvVolume) & "', "
                SQL = SQL & "'" & HilangkanTanda(LvJmlBsr) & "', '" & LvIsiBsr & "', '" & HilangkanTanda(LvBeratBrsh) & "', '" & HilangkanTanda(LvBeratKtr) & "', "
                SQL = SQL & "'" & HilangkanTanda(LvTotBeratBrsh) & "', '" & HilangkanTanda(LvTotBeratKtr) & "', '" & LvPjg & "', '" & LvLbr & "', '" & LvTinggi & "', '" & LvUrut & "', '" & LvMataUang & "', "
                SQL = SQL & "'" & HilangkanTanda(LvPotStock) & "', '" & HilangkanTanda(LvTdkPotStockLunas) & "', '" & HilangkanTanda(LvTdkPotStockBlumLunasUtama) & "', '" & HilangkanTanda(LvTdkPotStockBlumLunasPenolong) & "', "
                SQL = SQL & "'" & HilangkanTanda(LvTotHPPBsr) & "', '" & HilangkanTanda(LvTotHPPKcl) & "', '" & HilangkanTanda(LvColumn1) & "', '" & HilangkanTanda(LvColumn2) & "', '" & HilangkanTanda(LvColumn3) & "', "
                SQL = SQL & "'" & HilangkanTanda(LvBiayaImport) & "', '" & HilangkanTanda(LvBilling) & "', '" & HilangkanTanda(LvBiayaKonte) & "', '" & HilangkanTanda(LvBiayaFreight) & "', "
                SQL = SQL & "'" & HilangkanTanda(LvHPP_Per_Pcs) & "', '" & HilangkanTanda(LvKurs) & "', '" & HilangkanTanda(LvSelisihPO) & "', '" & HilangkanTanda(LvSelisihPO_biaya) & "')"
                ExecuteTrans(SQL)
            Next
            Dim avg As String = "NULL"
            If TxtFlag_Gabungan.Text <> "Y" Then
                avg = "'Y'"
            End If

            For i As Integer = 0 To DataGridViewavg1.Rows.Count - 1
                Get_Isi_ListviewAVG(i)

                SQL = "insert into detail_HPP_Import_biaya (kode_perusahaan, No_faktur, Kode_Stock_Owner, Kode_Barang, Kode_Kategori_Biaya_import, Biaya, Biaya_AVG, Flag_Average) "
                SQL = SQL & " values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & faktur & "', "
                SQL = SQL & "'" & LvLokasiAVG & "', '" & LvKdBarangAVG & "', '" & LvjenisAVG & "',"
                SQL = SQL & "'" & HilangkanTanda(LvBiayaImportAVG) & "', '" & HilangkanTanda(LvBiayaImportAVGNew) & "', " & avg & ") "
                ExecuteTrans(SQL)
            Next

            Dim hasData As Boolean = False

            For i As Integer = 0 To DataGridViewavg2.Rows.Count - 1
                Get_Isi_Listview2AVG(i)

                Dim Lokasi_gudang As String = ""
                SQL = "Select Kode_Stock_Owner_Gudang From binding_lokasi_Gudang Where "
                SQL = SQL & "Kode_Stock_Owner ='" & CmbLokasi.Text & "' and Gudang_Default='Y' and Kode_Perusahaan='" & KodePerusahaan & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        Lokasi_gudang = dr("Kode_Stock_Owner_Gudang")
                    Else
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("Lokasi Gudang Default tidak ada . . .! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "insert into detail_HPP_Import2_biaya (kode_perusahaan, No_faktur, Kode_Stock_Owner, Kode_Barang, Lokasi_Tujuan, Kode_Kategori_Biaya_import, Biaya2, biaya_avg2, BiayaWetDry, BiayaWetDry_AVG, flag_average) "
                SQL = SQL & " values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & faktur & "', "
                SQL = SQL & "'" & Lokasi_gudang & "', '" & Lv2KdBarangAVG & "', '" & Lv2LokasiTujuanAVG & "', '" & Lv2jenisAVG & "',"
                SQL = SQL & "'" & HilangkanTanda(Lv2BiayaImportAVG) & "', '" & HilangkanTanda(Lv2BiayaImportAVGNew) & "', "
                SQL = SQL & "'" & HilangkanTanda(Lv2BiayaImportWetDryAVG) & "', '" & HilangkanTanda(Lv2BiayaImportWetDryAVGNew) & "', " & avg & ") "
                ExecuteTrans(SQL)
            Next
            'ini dayat
            'select kode_kategori2, * from barang where nama like 'ori%'

            'select * from Promo_Budgeting_BS
            ' harga_agen = barang.x_hrg_mid

            'select Flag_Harga_Agen, * from stock_owner	

            'harga_agen = barang.x_hrg_mid
            'harga_agen2 = barang.F_Hrg_Resell_Max


            For i As Integer = 0 To DataGridView2.Rows.Count - 1
                Get_Isi_Listview2(i)

                'If Lv2LokasiTujuan <> Lv2LokasiTujuanAVG Or Lv2KdBarang <> Lv2KdBarangAVG Then
                '    CloseTrans()
                '    CloseConn()
                '    MessageBox.Show("Terjadi Kesalahan Pada Perhitungan . .  !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '    Exit Sub
                'End If


                '''Dim kode_kategori2 As String
                '''Dim harga_agen As Double
                '''Dim flag_harga_agen As String
                ''''cek kode_stock_owner sama kode_barang
                '''SQL = "select kode_kategori2 from barang where kode_perusahaan ='" & KodePerusahaan & "' and kode_stock_owner ='" & Lv2LokasiTujuan & "' and kode_barang ='" & Lv2KdBarang & "'"
                '''Using Dr = OpenTrans(SQL)
                '''    If Dr.Read Then

                '''        kode_kategori2 = Dr("kode_kategori2")

                '''        Dr.Close()
                '''        SQL = "select count(kode_perusahaan) as count from Promo_Budgeting_BS where kode_perusahaan = '" & KodePerusahaan & "' and kode_kategori2 ='" & kode_kategori2 & "' "
                '''        Using Dr1 = OpenTrans(SQL)
                '''            If Dr1.Read Then
                '''                If General_Class.CekNULL(Dr1("count")) > 0 Then

                '''                    Dr1.Close()
                '''                    'harga_agen = x_hrg_mid
                '''                    SQL = "select x_hrg_mid from barang where kode_perusahaan ='" & KodePerusahaan & "' and kode_stock_owner ='" & Lv2LokasiTujuan & "' and kode_barang ='" & Lv2KdBarang & "'"
                '''                    Using Dr2 = OpenTrans(SQL)
                '''                        If Dr2.Read Then
                '''                            harga_agen = Dr2("x_hrg_mid")
                '''                        Else
                '''                            Dr2.Close()
                '''                            CloseTrans()
                '''                            CloseConn()
                '''                            MessageBox.Show("Harga Barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '''                            Exit Sub
                '''                        End If
                '''                    End Using

                '''                Else

                '''                    Dr1.Close()
                '''                    'cek flag_harga_agen di stock_owner
                '''                    SQL = "select flag_harga_agen from stock_owner where kode_perusahaan ='" & KodePerusahaan & "' and kode_stock_owner='" & Lv2lkstujuanInduk & "'"
                '''                    Using Dr3 = OpenTrans(SQL)
                '''                        If Dr3.Read Then

                '''                            flag_harga_agen = Dr3("flag_harga_agen").ToString.ToUpper

                '''                            Dr3.Close()
                '''                            If flag_harga_agen = "HARGA_AGEN" Then

                '''                                'harga_agen = x_hrg_mid
                '''                                SQL = "select x_hrg_mid from barang where kode_perusahaan ='" & KodePerusahaan & "' and kode_stock_owner ='" & Lv2LokasiTujuan & "' and kode_barang ='" & Lv2KdBarang & "'"
                '''                                Using Dr4 = OpenTrans(SQL)
                '''                                    If Dr4.Read Then
                '''                                        harga_agen = Dr4("x_hrg_mid")
                '''                                    Else
                '''                                        Dr4.Close()
                '''                                        CloseTrans()
                '''                                        CloseConn()
                '''                                        MessageBox.Show("Harga barang tidak ditemukan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '''                                        Exit Sub
                '''                                    End If
                '''                                End Using

                '''                            ElseIf flag_harga_agen = "HARGA_AGEN2" Then

                '''                                'harga_agen = F_Hrg_Resell_Max
                '''                                SQL = "select F_Hrg_Resell_Max from barang where kode_perusahaan ='" & KodePerusahaan & "' and kode_stock_owner ='" & Lv2LokasiTujuan & "' and kode_barang ='" & Lv2KdBarang & "'"
                '''                                Using Dr4 = OpenTrans(SQL)
                '''                                    If Dr4.Read Then
                '''                                        harga_agen = Dr4("F_Hrg_Resell_Max")
                '''                                    Else
                '''                                        Dr4.Close()
                '''                                        CloseTrans()
                '''                                        CloseConn()
                '''                                        MessageBox.Show("Harga barang tidak ditemukan!!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '''                                        Exit Sub
                '''                                    End If
                '''                                End Using

                '''                            Else
                '''                                CloseTrans()
                '''                                CloseConn()
                '''                                MessageBox.Show("Barang ini tidak memiliki harga agen ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '''                                Exit Sub
                '''                            End If
                '''                        Else
                '''                            Dr3.Close()
                '''                            CloseTrans()
                '''                            CloseConn()
                '''                            MessageBox.Show("Flag Agen tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '''                            Exit Sub
                '''                        End If
                '''                    End Using
                '''                End If
                '''            End If
                '''        End Using
                '''    Else
                '''        Dr.Close()
                '''        CloseTrans()
                '''        CloseConn()
                '''        MessageBox.Show("Kode Barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '''        Exit Sub
                '''    End If
                '''End Using

                '''If harga_agen = 0 Then
                '''    CloseTrans()
                '''    CloseConn()
                '''    MessageBox.Show("Harga tidak tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '''    Exit Sub
                '''End If

                '''Dim Harga_Baru As Double
                '''Harga_Baru = Val(HilangkanTanda(Format(Lv2HPP_Per_Pcs_brsh + (Lv2HPP_Per_Pcs_brsh * 5 / 100), "N0")))


                '''If Harga_Baru > harga_agen Then
                '''    CloseTrans()
                '''    CloseConn()
                '''    MessageBox.Show("Harga lebih besar dari harga agen !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '''    Exit Sub
                '''End If


                SQL = "insert into detail_HPP_Import2 (kode_perusahaan, No_faktur, Kode_Stock_Owner, Lokasi_Tujuan, Kode_Barang, Jumlah, Harga, Total_Harga, "
                SQL = SQL & "Volume, Jml_Satuan_Besar, Isi_Satuan_Besar, Berat_Bersih, Berat_Kotor, Total_Berat_Bersih, "
                SQL = SQL & " Total_Berat_kotor, Panjang, Lebar, Tinggi, Biaya_Import2, Biaya_import_Wet_Dry, Input_HPP, Nilai_HPP_Barang_Per_pcs_brsh,lks_tujuan "
                SQL = SQL & ") values( "
                SQL = SQL & "'" & KodePerusahaan & "', '" & faktur & "', "
                SQL = SQL & "'" & Lv2Lokasi & "', '" & Lv2LokasiTujuan & "', '" & Lv2KdBarang & "', "
                SQL = SQL & "'" & Lv2Jumlah & "', '" & HilangkanTanda(Lv2Harga) & "', '" & HilangkanTanda(Lv2TotalHarga) & "', "
                SQL = SQL & "'" & HilangkanTanda(Lv2Volume) & "', "
                SQL = SQL & "'" & HilangkanTanda(Lv2JmlBsr) & "', '" & Lv2IsiBsr & "', '" & HilangkanTanda(Lv2BeratBrsh) & "', '" & HilangkanTanda(Lv2BeratKtr) & "', "
                SQL = SQL & "'" & HilangkanTanda(Lv2TotBeratBrsh) & "', '" & HilangkanTanda(Lv2TotBeratKtr) & "', '" & Lv2Pjg & "', '" & Lv2Lbr & "', '" & Lv2Tinggi & "', "
                SQL = SQL & "'" & HilangkanTanda(Lv2BiayaImport) & "', '" & HilangkanTanda(Lv2BiayaImportWetDry) & "', '" & HilangkanTanda(Lv2Input) & "',"
                SQL = SQL & "'" & HilangkanTanda(Lv2HPP_Per_Pcs_brsh) & "', '" & Lv2lkstujuanInduk & "')"
                ExecuteTrans(SQL)

                Dim satuan_kirim As String = ""
                Dim satuan_barang As String = ""
                Dim nilai_kirim As Double = 0
                SQL = "select satuan from barang_detail_satuan where kode_barang='" & Lv2KdBarang & "' "
                SQL = SQL & "and kode_Perusahaan='" & KodePerusahaan & "' and flag_kirim='Y' "
                Using dr3 = OpenTrans(SQL)
                    If dr3.Read Then
                        satuan_kirim = dr3("satuan")
                    Else
                        dr3.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("data satuan kirim tidak ada ")
                        Exit Sub
                    End If
                End Using

                SQL = "select satuan from barang where kode_barang='" & Lv2KdBarang & "' "
                SQL = SQL & "and kode_Perusahaan='" & KodePerusahaan & "' "
                Using dr3 = OpenTrans(SQL)
                    If dr3.Read Then
                        satuan_barang = dr3("satuan")
                    Else
                        dr3.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("data satuan kirim tidak ada ")
                        Exit Sub
                    End If
                End Using

                SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'UANG','" & Lv2KdBarang & "', '" & satuan_kirim & "',"
                SQL = SQL & "'" & satuan_kirim & "', '" & HilangkanTanda(Lv2HPP_Per_Pcs_brsh) & "' ) as hasil"
                Using Dr1 = OpenTrans(SQL)
                    If Dr1.Read Then
                        If General_Class.CekNULL(Dr1("hasil")) = "" Then
                            Dr1.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("data konversi satuan kirim tidak ada ")
                            Exit Sub
                        End If

                        nilai_kirim = Dr1("hasil")
                    Else
                        Dr1.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("data konversi satuan kirim tidak ada ")
                        Exit Sub
                    End If
                End Using


                Dim No_Fak_Pembelian_Loading As New ArrayList()
                SQL = "select string_agg(''''+no_faktur+'''', ', ') as No_faktur from emi_pembelian_loading "
                SQL = SQL & "where No_Fak_Submit_PO in(" & No_PO & ") "
                SQL = SQL & "and Lokasi='" & CmbLokasi.Text & "' "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        Dim No_fak As String = dr("No_faktur")
                        dr.Close()

                        SQL = "Update emi_pembelian_loading set "
                        SQL = SQL & "Flag_Import_HPP='Y', No_Fak_HPP='" & faktur & "' "
                        SQL = SQL & "where No_Faktur in(" & No_fak & ") "
                        SQL = SQL & "and Lokasi='" & CmbLokasi.Text & "' "
                        ExecuteTrans(SQL)

                        '========================
                        '=     GET HARGA PO     =
                        '========================
                        Dim noPO As String = ""
                        SQL = "select No_PO, c.No_Urut "
                        SQL = SQL & "from submit_PO a, Rencana_Order b, EMI_Pembelian_PO_detail c "
                        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                        SQL = SQL & "and a.ID_Rencana = b.ID_Rencana "
                        SQL = SQL & "and b.No_PO = c.No_Faktur "
                        SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and b.ID_Rencana = '" & TxtId_Rencana.Text & "' "
                        SQL = SQL & "and c.kode_barang='" & Lv2KdBarang & "'"
                        Using dr2 = OpenTrans(SQL)
                            If dr2.Read Then
                                noPO = dr2("No_PO")
                                dr2.Close()

                            Else
                                dr2.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Harga PO Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub

                            End If
                        End Using

                        Dim hargaPO As Double = 0
                        SQL = "select Harga_barang from EMI_Pembelian_PO_Detail WHERE Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur ='" & noPO & "' and Kode_Barang = '" & Lv2KdBarang & "' "
                        Using dr2 = OpenTrans(SQL)
                            If dr2.Read Then
                                hargaPO = Val(HilangkanTanda(dr2("Harga_barang")))
                                dr2.Close()

                            Else
                                dr2.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Harga PO Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub

                            End If
                        End Using

                        SQL = "Update emi_pembelian_loading_detail set "
                        SQL = SQL & "Harga_Barang='" & hargaPO & "', "
                        SQL = SQL & "HPP_Satuan_Display='" & HilangkanTanda(Lv2HPP_Per_Pcs_brsh) & "' "
                        SQL = SQL & "where No_Faktur in(" & No_fak & ") "
                        SQL = SQL & "and Kode_Barang='" & Lv2KdBarang & "' "
                        ExecuteTrans(SQL)


                        No_fak = No_fak.Replace("'", "").Replace(" ", "")
                        No_Fak_Pembelian_Loading.AddRange(No_fak.Split(","c))

                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("No PO tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using


                '=======================
                '=     GET SN LAMA     =
                '=======================
                For j As Integer = 0 To No_Fak_Pembelian_Loading.Count - 1
                    SQL = "select Kode_Perusahaan, No_Faktur, No_Pembelian_Loading, Serial_Number as SN_Lama, Nilai_Barang as JumlahMasukKecil, Jumlah_Bags "
                    SQL = SQL & "from EMI_Barang_Masuk_Perpallet a "
                    SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and a.No_Pembelian_Loading = '" & No_Fak_Pembelian_Loading(j) & "' "
                    'SQL = SQL & "and Flag_angkut = 'Y' "
                    SQL = SQL & "and kode_barang = '" & Lv2KdBarang & "'"
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then

                                For k As Integer = 0 To Ds.Tables("MyTable").Rows.Count - 1

                                    '========================================
                                    '=     CEK DATA APAKAH SUDAH ANGKUT     =
                                    '========================================
                                    SQL = "select kode_perusahaan "
                                    SQL = SQL & "from EMI_Barang_Masuk_Perpallet "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and no_faktur = '" & Ds.Tables("MyTable").Rows(k).Item("No_Faktur") & "' "
                                    SQL = SQL & "and Flag_angkut = 'Y' "
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            Dr.Close()

                                        Else
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Barang Belum Masuk Gudang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using

                                    hasData = True
                                    '==========================
                                    '=     INSERT SN BARU     =
                                    '==========================
                                    Dim Total_HPP As Double = Val(HilangkanTanda(Lv2HPP_Per_Pcs_brsh))

                                    Dim Random As New Random()
                                    Dim str As String = Format(Random.Next(0, 999), "000") & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HHmmss")
                                    Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                                    Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & Total_HPP & Tanda_SN & "02" & Tanda_SN & Format(DateTime.Now, "yyyy-MM-dd")

                                    SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah, Warna, Warning_Stock_X, Bad_Stock_X, Jumlah_Bags, Lama, thn, Tgl_Expired, Tgl_Produksi, "
                                    SQL = SQL & "Bulan, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, rr, Id_Nametag_pallet, Batch_Number, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, Flag_QI, Tgl_Masuk) "
                                    SQL = SQL & "select Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, '" & SN_Baru & "', Jumlah, Warna, Warning_Stock_X, Bad_Stock_X, Jumlah_Bags, Lama, thn, Tgl_Expired, Tgl_Produksi, Bulan, Stock_PO, "
                                    SQL = SQL & "Stock_Inquiry, Id_Warehouse, id_Susunan, rr, Id_Nametag_pallet, Batch_Number, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, Flag_QI, Tgl_Masuk "
                                    SQL = SQL & "from Barang_SN where Serial_Number = '" & .Rows(k).Item("SN_Lama") & "' "
                                    ExecuteTrans(SQL)

                                    '========================
                                    '=     POTONG STOCK     =
                                    '========================
                                    SQL = "update Barang_SN set Jumlah = Jumlah - " & .Rows(k).Item("JumlahMasukKecil") & ", Jumlah_Bags = Jumlah_Bags - " & .Rows(k).Item("Jumlah_Bags") & " where Serial_Number = '" & .Rows(k).Item("SN_Lama") & "' "
                                    ExecuteTrans(SQL)

                                    '==========================================
                                    '=     CEK APAKAH BARANG_SN MENJADI 0     =
                                    '==========================================
                                    Dim kode_SO As String = ""
                                    SQL = "select Jumlah, Kode_Stock_Owner from Barang_SN where Serial_Number = '" & .Rows(k).Item("SN_Lama") & "' "
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            kode_SO = Dr("Kode_Stock_Owner")

                                            If Val(Dr("Jumlah")) <> 0 Then
                                                Dr.Close()
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Terdapat Kesalahan saat Potong Stock : Jumlah tidak 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If

                                        Else
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terdapat Kesalahan saat Potong Stock", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    End Using


                                    '==============================
                                    '=     CEK KESESUAIAN DATA    =
                                    '==============================
                                    SQL = "select a.Kode_Barang, a.Nama, "
                                    SQL = SQL & "ISNULL(ROUND(sum(Good_Stock), 2), 0) as GoodStock, "
                                    SQL = SQL & "ISNULL(ROUND(sum(Jumlah_Bags), 2), 0) as Bags_Barang, "
                                    SQL = SQL & "ISNULL((select ROUND(sum(z.Jumlah),2) from Barang_SN z where  a.Kode_Perusahaan=z.Kode_Perusahaan "
                                    SQL = SQL & "and a.kode_barang=z.Kode_Barang and a.Kode_Stock_Owner=z.Kode_Stock_Owner), 0) as Jumlah_Sn, "
                                    SQL = SQL & "ISNULL((select ROUND(sum(z.Jumlah_Bags),2) from Barang_SN z where  a.Kode_Perusahaan=z.Kode_Perusahaan "
                                    SQL = SQL & "and a.kode_barang=z.Kode_Barang and a.Kode_Stock_Owner=z.Kode_Stock_Owner), 0) as Bags_Sn "
                                    SQL = SQL & "from barang a "
                                    SQL = SQL & "where a.Kode_Perusahaan='" & KodePerusahaan & "' and a.Kode_Stock_Owner='" & kode_SO & "' "
                                    SQL = SQL & "and a.Kode_Barang='" & Lv2KdBarang & "' "
                                    SQL = SQL & "group by a.Kode_Barang, a.Nama, a.Kode_Perusahaan, a.Kode_Stock_Owner "
                                    Using Ds2 = BindingTrans(SQL)
                                        With Ds2.Tables("MyTable")
                                            If .Rows.Count <> 0 Then
                                                If Ds2.Tables("MyTable").Rows(0).Item("GoodStock") <> Ds2.Tables("MyTable").Rows(0).Item("Jumlah_Sn") Or Ds2.Tables("MyTable").Rows(0).Item("Bags_Barang") <> Ds2.Tables("MyTable").Rows(0).Item("Bags_Sn") Then
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

                                    '==========================
                                    '=     INSERT SN BARU     =
                                    '==========================
                                    SQL = "update EMI_Barang_Masuk_Perpallet set Serial_Number_Import = '" & SN_Baru & "' where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and No_Faktur = '" & .Rows(k).Item("No_Faktur") & "' and No_Pembelian_Loading = '" & No_Fak_Pembelian_Loading(j) & "'"
                                    ExecuteTrans(SQL)

                                Next


                            End If
                        End With
                    End Using
                Next

            Next

            '==================
            '=     JURNAL     =
            '==================
            IsError = False
            If hasData = True Then
                Jurnal_Import_Pertimbangan()

                If IsError = False Then
                    MessageBox.Show("Terdapat Masalah Saat Simpan Jurnal", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    Exit Sub
                End If
            End If



            For index As Integer = 0 To ListView1.Items.Count - 1
                SQL = "insert into Kurs_HPP_Import(kode_perusahaan, no_faktur, Mata_Uang, Jenis, Nilai) Values( "
                SQL = SQL & "'" & KodePerusahaan & "','" & faktur & "','" & ListView1.Items(index).SubItems(0).Text & "', "
                SQL = SQL & "'" & ListView1.Items(index).SubItems(1).Text & "', '" & ListView1.Items(index).SubItems(2).Text & "')"
                ExecuteTrans(SQL)
            Next

            Dim metode_Hitung_Konte As String = ""
            SQL = "select Metode_Hitung_Konte from Stock_Owner where Kode_Stock_Owner ='" & CmbLokasi.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    metode_Hitung_Konte = Dr("Metode_Hitung_Konte")
                Else
                    MessageBox.Show("Lokasi tidak ada !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    Exit Sub
                End If
            End Using

            SQL = ";with "
            If metode_Hitung_Konte = "A" Then
                SQL = SQL & "cte_Kontainer as( "
                SQL = SQL & "select a.Kode_Perusahaan, a.id_rencana, a.ETA, c.Lokasi, b.kode_pelabuhan, c.free_storage, d.No_Container, "
                SQL = SQL & "d.Tgl_Tarik ,datediff(day,format(DATEADD(dd, c.free_storage, a.ETA), 'yyyy-MM-dd'), format(d.Tgl_Tarik, 'yyyy-MM-dd')) as jumlah_hari "
                SQL = SQL & "from ubah_status_otw a, "
                SQL = SQL & "kapal_tiba_import b, pelabuhan c, Tarik_Kontainer d  where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.id_rencana = b.id_rencana and b.Kode_Perusahaan = c.Kode_Perusahaan and b.kode_pelabuhan = "
                SQL = SQL & "c.Kode_Pelabuhan and a.Kode_Perusahaan = d.Kode_perusahaan and a.Id_rencana = d.id_rencana and "
                SQL = SQL & "a.id_rencana in (" & id_rencana_group & ") "
                SQL = SQL & ") "
                SQL = SQL & ",cte_total_Kontainer as ( "
                SQL = SQL & "select a.*,b.id_rencana, isnull(( "
                SQL = SQL & "select count(X.no_container) from cte_Kontainer X where jumlah_hari+1 >= dari "
                SQL = SQL & "and X.Id_rencana = d.id_rencana ), 0) as Jumlah_Kontainer, Harga*isnull(( "
                SQL = SQL & "select count(X.no_container) from cte_Kontainer X where jumlah_hari+1 >= dari "
                SQL = SQL & "and X.Id_rencana = d.id_rencana ), 0) as Biaya "
                SQL = SQL & "from storage a, Kapal_Tiba_import b, Pelabuhan c, rencana_order d where "
                SQL = SQL & "b.Kode_Pelabuhan = c.Kode_Pelabuhan and B.Kode_Perusahaan = C.Kode_Perusahaan and "
                SQL = SQL & "a.kode_stock_owner = c.Lokasi And a.kode_pelabuhan = b.Kode_Pelabuhan and "
                SQL = SQL & "b.Kode_Perusahaan = d.Kode_Perusahaan  and b.Id_Rencana = d.Id_rencana and "
                SQL = SQL & "a.Kode_Kontainer = d.Kode_Kontainer "
                SQL = SQL & "and d.id_rencana in (" & id_rencana_group & ") ) "
            ElseIf metode_Hitung_Konte = "B" Then
                SQL = SQL & "cte_total_Kontainer as ( "
                SQL = SQL & "select a.Kode_Perusahaan, c.Lokasi as Kode_stock_Owner, a.id_rencana,e.Kode_Kontainer, a.ETA,b.kode_pelabuhan, c.free_storage, "
                SQL = SQL & "d.No_Container, d.Tgl_Tarik ,datediff(day,format(DATEADD(dd, c.free_storage, a.ETA), 'yyyy-MM-dd'), "
                SQL = SQL & "format(d.Tgl_Tarik, 'yyyy-MM-dd')) as jumlah_hari, isnull((select X.Harga from storage X "
                SQL = SQL & "where datediff(day,format(DATEADD(dd, c.free_storage, a.ETA), 'yyyy-MM-dd'), "
                SQL = SQL & "format(d.Tgl_Tarik, 'yyyy-MM-dd'))= sampai and X.Kode_Perusahaan = c.Kode_Perusahaan and "
                SQL = SQL & "X.Kode_Stock_Owner = c.Lokasi and X.Kode_Pelabuhan = c.Kode_Pelabuhan and X.Kode_Kontainer = "
                SQL = SQL & "e.Kode_Kontainer),0) as biaya from ubah_status_otw a, kapal_tiba_import b, pelabuhan c, "
                SQL = SQL & "Tarik_Kontainer d, rencana_order e  where a.Kode_Perusahaan = b.Kode_Perusahaan and a.id_rencana = "
                SQL = SQL & "b.id_rencana and b.Kode_Perusahaan = c.Kode_Perusahaan and b.kode_pelabuhan = c.Kode_Pelabuhan and "
                SQL = SQL & "a.Kode_Perusahaan = d.Kode_perusahaan and a.Id_rencana = d.id_rencana and a.kode_perusahaan = "
                SQL = SQL & "e.Kode_Perusahaan and a.Id_rencana = e.Id_rencana and a.id_rencana in (" & id_rencana_group & ") "
                SQL = SQL & ") "
            Else
                MessageBox.Show("Metode Perhitungan Konte tidak ada !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                CloseTrans()
                CloseConn()
                Exit Sub
            End If
            SQL = SQL & "select* from "
            SQL = SQL & "cte_Total_kontainer a where biaya<>0 "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For index As Integer = 0 To .Rows.Count - 1

                        If metode_Hitung_Konte = "A" Then
                            SQL = "Insert Into Detail_Storage_HPP_A(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Kontainer, Kode_Pelabuhan, Dari, Sampai, Harga, Jumlah_Kontainer, Biaya) "
                            SQL = SQL & "Values('" & KodePerusahaan & "', '" & faktur & "', '" & .Rows(index).Item("Kode_Stock_Owner") & "', '" & .Rows(index).Item("Kode_Kontainer") & "', "
                            SQL = SQL & "'" & .Rows(index).Item("Kode_Pelabuhan") & "', '" & .Rows(index).Item("Dari") & "', '" & .Rows(index).Item("Sampai") & "', '" & .Rows(index).Item("Harga") & "', "
                            SQL = SQL & "'" & .Rows(index).Item("Jumlah_Kontainer") & "', '" & .Rows(index).Item("Biaya") & "')"
                            ExecuteTrans(SQL)

                        ElseIf metode_Hitung_Konte = "B" Then
                            SQL = "Insert Into Detail_Storage_HPP_B(Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Kontainer, Kode_Pelabuhan, No_Kontainer, Jumlah_Hari, Biaya) "
                            SQL = SQL & "Values('" & KodePerusahaan & "', '" & faktur & "', '" & .Rows(index).Item("Kode_Stock_Owner") & "', '" & .Rows(index).Item("Kode_Kontainer") & "', "
                            SQL = SQL & "'" & .Rows(index).Item("Kode_Pelabuhan") & "', '" & .Rows(index).Item("No_Container") & "', "
                            SQL = SQL & "'" & .Rows(index).Item("Jumlah_Hari") & "', '" & .Rows(index).Item("Biaya") & "')"
                            ExecuteTrans(SQL)
                        Else
                            MessageBox.Show("Metode Perhitungan Konte tidak ada !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            CloseTrans()
                            CloseConn()
                            Exit Sub

                        End If

                    Next
                End With
            End Using


            SQL = "Delete From HPP_Temp "
            ExecuteTrans(SQL)

            SQL = "select Flag_Gabungan from rencana_order where "
            SQL = SQL & "Id_rencana = '" & TxtId_Rencana.Text & "'"
            Using Dr2 = OpenTrans(SQL)
                If Dr2.Read Then
                    If General_Class.CekNULL(Dr2("Flag_Gabungan")) = "Y" Then
                        Dr2.Close()
                        SQL = "select a.id_rencana from rencana_order a, rencana_order_gabungan b where "
                        SQL = SQL & "a.id_rencana = b.Id_rencana and b.Id_rencana_induk = '" & TxtId_Rencana.Text & "'"
                        Using Ds = BindingTrans(SQL)
                            With Ds.Tables("MyTable")
                                If .Rows.Count <> 0 Then
                                    For i As Integer = 0 To .Rows.Count - 1
                                        SQL = "update rencana_Order set flag_HPP = 'Y' "
                                        SQL = SQL & " where Id_Rencana = '" & .Rows(i).Item("id_rencana") & "'"
                                        ExecuteTrans(SQL)
                                    Next
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data lokasi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End With
                        End Using
                    Else
                        Dr2.Close()
                        SQL = "update rencana_Order set flag_HPP = 'Y' "
                        SQL = SQL & " where Id_Rencana = '" & TxtId_Rencana.Text.Trim & "'"
                        ExecuteTrans(SQL)
                    End If
                Else
                    Dr2.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Id Rencana tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            ' cek
            SQL = Simpan_Status_Rencana_Order(TxtId_Rencana.Text, "HITUNG_HPP", faktur)
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Tersimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()
        kosong_mata_uang()

    End Sub

    Private Sub TxtId_Rencana_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtId_Rencana.Click

    End Sub


    Public Sub TxtId_Rencana_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtId_Rencana.Leave
        If TxtId_Rencana.Text.Trim.Length = 0 Then Exit Sub


        ambil_selisih_PO()
        ambil_selisih_Biaya()
        Try

            OpenConn()
            Cmd.Transaction() = Cn.BeginTransaction

            DataGridView1.Rows.Clear()
            DataGridView2.Rows.Clear()
            'ListView2.Items.Clear()

            'Dim Mata_Uang_Declare As String = ""
            Dim ind As Integer = 0
            'SQL = "Select Mata_Uang_Rek, Mata_Uang_Declare From suppliers where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Supplier = '" & TextBox1.Text & "' "
            'Using dr = OpenTrans(SQL)
            '    If dr.Read Then

            '        For ind = 0 To ComboBox1.Items.Count - 1
            '            If ComboBox1.Items(ind) = dr("Mata_Uang_Rek") Then
            '                Exit For
            '            End If
            '        Next
            '        Mata_Uang_Declare = dr("Mata_Uang_Declare")
            '    Else
            '        CloseConn()
            '        MessageBox.Show("Mata Uang Declare/Rekening Tidak ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        Exit Sub
            '    End If
            'End Using
            'ComboBox1.SelectedIndex = ind

            'If ComboBox2.Items.Count = 0 Then
            '    CloseConn()
            '    MessageBox.Show("Rekening dalam mata uang itu tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If

            btnSelisihBiaya.Enabled = True
            btnSelisihPO.Enabled = True


            Dim Flag_Average_Sup As String = ""

            Dim biaya_form_e As Double = 0
            Dim Metode_Hitung_Selisih As String = ""
            Dim Kode_sup As String = ""

            SQL = "select cast(RV as bigint) as rvx, biaya_form_e, b.Flag_Average, b.Metode_Selisih_Declare, b.Kode_Supplier from rencana_order a, Suppliers b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and a.id_rencana = '" & TxtId_Rencana.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TextBoxRV.Text = Dr("rvx")
                    Flag_Average_Sup = Dr("Flag_Average")
                    Metode_Hitung_Selisih = Dr("Metode_Selisih_Declare")
                    Kode_sup = Dr("Kode_Supplier")
                    biaya_form_e = Dr("biaya_form_e")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Id Rencana tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            If biaya_form_e <> 0 Then
                Dim datae As Boolean = False
                For index As Integer = 0 To ListView1.Items.Count - 1

                    If ListView1.Items(index).SubItems(1).Text = "UTAMA" Then
                        datae = True
                        biaya_form_e = biaya_form_e * Val(HilangkanTanda(ListView1.Items(index).SubItems(2).Text))
                    End If
                Next

                If datae = False Then
                    MessageBox.Show("Kurs untuk Bahan Utama tidak ada !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CloseTrans()
                    CloseConn()
                    Exit Sub
                End If
            End If



            'arrselisih.Clear()
            'Dim nilai_selisih As Double = 0

            'SQL = "select nilai as selisih,urut from Selisih_Kurs_Hutang_Loading_Barang a, Rencana_Order b "
            'SQL = SQL & "where a.Kode_Perusahaan =b.Kode_Perusahaan and "
            'SQL = SQL & "a.Id_Rencana = b.Id_rencana And flag_pakai Is null "
            'SQL = SQL & "and b.Kode_supplier='" & Kode_sup & "' and b.Lokasi='" & CmbLokasi.Text & "' "
            'SQL = SQL & "and a.Status is null and b.Status is null "
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        arrselisih.Add(Dr("urut"))
            '        nilai_selisih = nilai_selisih + Dr("selisih")
            '    Loop

            'End Using

            'arrselisih_biaya.Clear()
            'Dim nilai_selisih_biaya As Double = 0

            'SQL = "select nilai as selisih,urut from Selisih_Kurs_Biaya_Import_By_Perusahaan a, Rencana_Order b "
            'SQL = SQL & "where a.Kode_Perusahaan =b.Kode_Perusahaan and "
            'SQL = SQL & "a.Id_Rencana = b.Id_rencana And flag_pakai Is null "
            'SQL = SQL & "and b.Kode_supplier='" & Kode_sup & "' and b.Lokasi='" & CmbLokasi.Text & "' "
            'SQL = SQL & "and a.Status is null and b.Status is null "
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        arrselisih_biaya.Add(Dr("urut"))
            '        nilai_selisih_biaya = nilai_selisih_biaya + Dr("selisih")
            '    Loop

            'End Using


            id_rencana_group = ""

            Dim i As Integer = 0
            SQL = "select Flag_Gabungan from rencana_order where "
            SQL = SQL & "Id_rencana = '" & TxtId_Rencana.Text & "'"
            Using Dr2 = OpenTrans(SQL)
                If Dr2.Read Then
                    If General_Class.CekNULL(Dr2("Flag_Gabungan")) = "Y" Then
                        Dr2.Close()
                        SQL = "select a.id_rencana from rencana_order a, rencana_order_gabungan b where "
                        SQL = SQL & "a.id_rencana = b.Id_rencana and b.Id_rencana_induk = '" & TxtId_Rencana.Text & "'"
                        Using Dr = OpenTrans(SQL)
                            Do While Dr.Read
                                If i <> 0 Then
                                    id_rencana_group = id_rencana_group & ", "
                                End If
                                id_rencana_group = id_rencana_group & "'" & Dr("id_rencana") & "'"
                                i += 1
                            Loop
                        End Using
                    Else
                        id_rencana_group = "'" & TxtId_Rencana.Text & "'"
                    End If
                Else
                    Dr2.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Id Rencana tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using




            Dim Metode_Hitung_Konte As String = ""

            SQL = "select Metode_Hitung_Konte from Stock_Owner where Kode_Stock_Owner ='" & CmbLokasi.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Metode_Hitung_Konte = Dr("Metode_Hitung_Konte")
                Else
                    MessageBox.Show("Lokasi tidak ada !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    Exit Sub
                End If
            End Using

            Dim Lokasi_gudang As String = ""
            SQL = "Select Kode_Stock_Owner_Gudang From binding_lokasi_Gudang Where "
            SQL = SQL & "Kode_Stock_Owner ='" & CmbLokasi.Text & "' and Gudang_Default='Y' and Kode_Perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    Lokasi_gudang = dr("Kode_Stock_Owner_Gudang")
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Lokasi Gudang Default tidak ada . . .! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "Delete From HPP_Temp "
            ExecuteTrans(SQL)



            SQL = ";with "
            SQL = SQL & "cte_persen_konte as( "
            SQL = SQL & "select a.Kode_Perusahaan, b.ID_Rencana, a.No_Faktur, a.Jenis, "
            SQL = SQL & "sum(Persentase)/isnull((select count( distinct X.No_Container) from Kontainer_Masuk_Per_Jenis X "
            SQL = SQL & "where X.Kode_perusahaan = a.Kode_Perusahaan and X.No_Faktur = A.No_Faktur ),0) as Persen_tot_Konte "
            SQL = SQL & "from Kontainer_Masuk_Per_Jenis a, Submit_PO b where a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.No_Faktur = b.no_faktur and b.id_rencana in(" & id_rencana_group & ") and b.status is null "
            SQL = SQL & "group by a.Kode_Perusahaan, b.ID_Rencana, a . No_Faktur, a.Jenis  "
            SQL = SQL & ") "

            SQL = SQL & ",cte_konte_per_lokasi as( "
            SQL = SQL & "select a.KOde_Perusahaan, a.Id_Rencana,Count (distinct B.No_container) as Konte_Per_Rencana, "
            SQL = SQL & "Isnull((select Count (distinct Y.No_container) from submit_PO X, Kontainer_Masuk Y "
            SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur "
            SQL = SQL & "and x. Id_Rencana in (" & id_rencana_group & ") and X.status is null ),0) as Total_Konte from submit_PO A, Kontainer_Masuk B  where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and A. Id_Rencana in (" & id_rencana_group & ") and a.status is null "
            SQL = SQL & "group by a.Kode_Perusahaan, a.Id_Rencana "
            SQL = SQL & ")"


            If Flag_Average_Sup = "Y" Then

                SQL = SQL & ", cte_nilai_biaya as( "
                SQL = SQL & "select a.kode_perusahaan, a.No_Faktur, a.id_rencana, sum(b.Total_AVG_Biaya) as grand_total "
                SQL = SQL & "from Transaksi_Biaya_Import a, aVg_Kategori_Biaya_Import b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_faktur = b.No_Faktur "
                SQL = SQL & "and a.status is null and a.Id_Rencana = '" & TxtId_Rencana.Text & "' and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "group by a.kode_perusahaan, a.No_Faktur, a.id_rencana "
                SQL = SQL & ") "

            Else

                SQL = SQL & ", cte_nilai_biaya as( "
                SQL = SQL & "select a.kode_perusahaan, a.No_Faktur, a.id_rencana, sum(b.total_avg_biaya) as grand_total "
                SQL = SQL & "from transaksi_biaya_import a, detail_transaksi_biaya_import b  where "
                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And a.no_faktur = b.no_faktur And a.status Is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & TxtId_Rencana.Text & "' and b.Jenis_Perhitungan <> 'G' "
                SQL = SQL & "group by a.kode_perusahaan, a.No_Faktur, a.id_rencana "
                SQL = SQL & ") "

            End If

            SQL = SQL & ",cte_sum_detail_biaya as ( "
            SQL = SQL & "select a.Kode_Perusahaan, a.Id_Rencana, b.No_Faktur, ((grand_total*Konte_Per_Rencana)/Total_Konte) as grand_Total from cte_konte_per_lokasi a, cte_nilai_biaya b "
            SQL = SQL & "where a.Kode_Perusahaan = b.KOde_Perusahaan "
            SQL = SQL & ") "

            SQL = SQL & ",cte_billing as( "
            SQL = SQL & "select b.id_rencana,a.Pembulatan_BM, a.Pembulatan_PPH, e.Jenis, d.Persen_tot_Konte, "
            SQL = SQL & "sum(g.Jumlah* g.Berat_Bersih) as Tot_berat_bersih, ((((((a.Pembulatan_BM + a.Pembulatan_PPH)* "
            SQL = SQL & "f.Konte_Per_Rencana)/f.Total_Konte) *d.Persen_tot_Konte)/100)/(sum(g.jumlah* g.Berat_Bersih))) "
            SQL = SQL & "as nilai_per_berat from Total_Billing a, Loading_Barang B, Detail_Loading_Barang G, Barang c, cte_Persen_konte d, "
            SQL = SQL & "Kategori_Besar e, cte_konte_per_lokasi f where a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.id_rencana = '" & TxtId_Rencana.Text & "' And g.kode_perusahaan = c.Kode_Perusahaan And g.Kode_stock_owner "
            SQL = SQL & "= c.Kode_Stock_Owner And g.Kode_barang = c.Kode_Barang and c.Kode_Perusahaan = "
            SQL = SQL & "e.Kode_Perusahaan and c.Kode_kategori_Besar = e.Kode_Kategori_Besar and b.Kode_Perusahaan = "
            SQL = SQL & "d.Kode_Perusahaan and b.Id_rencana = d.ID_Rencana and e.Jenis = d.Jenis and f.Kode_Perusahaan "
            SQL = SQL & "=b.KOde_Perusahaan and f.Id_Rencana = b.id_rencana and b.Kode_Perusahaan = G.Kode_Perusahaan "
            SQL = SQL & "and b.No_Faktur= g.No_Faktur and b.id_rencana in(" & id_rencana_group & ") and "
            SQL = SQL & "a.status is null and b.Status is null "
            SQL = SQL & "group by b.id_rencana, a.no_faktur,a.Pembulatan_BM,a.Pembulatan_PPH, e.Jenis, "
            SQL = SQL & "d.Persen_tot_Konte, f.Konte_Per_Rencana, f.Total_Konte"
            SQL = SQL & ")"

            SQL = SQL & ",cte_biaya as ( "
            SQL = SQL & "select b.id_rencana,a.Grand_total, e.Jenis, d.Persen_tot_Konte, sum(g.Jumlah* g.Berat_Bersih) "
            SQL = SQL & "as Tot_berat_bersih, (((a.Grand_total*d.Persen_tot_Konte)/100)/(sum(g.Jumlah* g.Berat_Bersih))) "
            SQL = SQL & "as nilai_per_berat from cte_sum_detail_biaya a, Loading_Barang B, Detail_Loading_Barang G, Barang c, "
            SQL = SQL & "cte_Persen_konte d, Kategori_Besar e where a.Kode_Perusahaan = b.Kode_Perusahaan And "
            SQL = SQL & "a.id_rencana = b.Id_rencana And g.kode_perusahaan = c.Kode_Perusahaan And g.Kode_stock_owner "
            SQL = SQL & "= c.Kode_Stock_Owner And g.Kode_barang = c.Kode_Barang and c.Kode_Perusahaan = "
            SQL = SQL & "e.Kode_Perusahaan and c.Kode_kategori_Besar = e.Kode_Kategori_Besar and b.Kode_Perusahaan "
            SQL = SQL & "= d.Kode_Perusahaan And b.Id_rencana = d.ID_Rencana And e.Jenis = d.Jenis and b.Kode_Perusahaan "
            SQL = SQL & "= G.Kode_Perusahaan and b.No_Faktur= g.No_Faktur and b.id_rencana in(" & id_rencana_group & ") and b.Status is null "
            SQL = SQL & "group by b.id_rencana, a.no_faktur,a.Grand_total, e.Jenis, d.Persen_tot_Konte "
            SQL = SQL & ") "

            SQL = SQL & ",cte_selisih as( "
            SQL = SQL & "select b.id_rencana, e.Jenis, d.Persen_tot_Konte, "
            SQL = SQL & "sum(g.Jumlah* g.Berat_Bersih) as Tot_berat_bersih, ((((((" & HilangkanTanda(TxtJmlPakaiPO.Text) & ")* "
            SQL = SQL & "f.Konte_Per_Rencana)/f.Total_Konte)*d.Persen_tot_Konte)/100)/(sum(g.jumlah*g.Berat_Bersih))) "
            SQL = SQL & "as nilai_per_berat from Loading_Barang B, Detail_Loading_Barang G, Barang c, cte_Persen_konte d, "
            SQL = SQL & "Kategori_Besar e, cte_konte_per_lokasi f where g.kode_perusahaan = c.Kode_Perusahaan And g.Kode_stock_owner "
            SQL = SQL & "= c.Kode_Stock_Owner And g.Kode_barang = c.Kode_Barang and c.Kode_Perusahaan = "
            SQL = SQL & "e.Kode_Perusahaan and c.Kode_kategori_Besar = e.Kode_Kategori_Besar and b.Kode_Perusahaan = "
            SQL = SQL & "d.Kode_Perusahaan and b.Id_rencana = d.ID_Rencana and e.Jenis = d.Jenis and f.Kode_Perusahaan "
            SQL = SQL & "=b.KOde_Perusahaan and f.Id_Rencana = b.id_rencana and b.Kode_Perusahaan = G.Kode_Perusahaan "
            SQL = SQL & "and b.No_Faktur= g.No_Faktur and b.id_rencana in(" & id_rencana_group & ") and "
            SQL = SQL & "b.Status is null "
            SQL = SQL & "group by b.id_rencana, e.Jenis, "
            SQL = SQL & "d.Persen_tot_Konte, f.Konte_Per_Rencana, f.Total_Konte "
            SQL = SQL & ") "

            SQL = SQL & ",cte_selisih_biaya as( "
            SQL = SQL & "select b.id_rencana, e.Jenis, d.Persen_tot_Konte, "
            SQL = SQL & "sum(g.Jumlah* g.Berat_Bersih) as Tot_berat_bersih, ((((((" & HilangkanTanda(txtJmlPakaiBiaya.Text) & ")* "
            SQL = SQL & "f.Konte_Per_Rencana)/f.Total_Konte)*d.Persen_tot_Konte)/100)/(sum(g.jumlah*g.Berat_Bersih))) "
            SQL = SQL & "as nilai_per_berat from Loading_Barang B, Detail_Loading_Barang G, Barang c, cte_Persen_konte d, "
            SQL = SQL & "Kategori_Besar e, cte_konte_per_lokasi f where g.kode_perusahaan = c.Kode_Perusahaan And g.Kode_stock_owner "
            SQL = SQL & "= c.Kode_Stock_Owner And g.Kode_barang = c.Kode_Barang and c.Kode_Perusahaan = "
            SQL = SQL & "e.Kode_Perusahaan and c.Kode_kategori_Besar = e.Kode_Kategori_Besar and b.Kode_Perusahaan = "
            SQL = SQL & "d.Kode_Perusahaan and b.Id_rencana = d.ID_Rencana and e.Jenis = d.Jenis and f.Kode_Perusahaan "
            SQL = SQL & "=b.KOde_Perusahaan and f.Id_Rencana = b.id_rencana and b.Kode_Perusahaan = G.Kode_Perusahaan "
            SQL = SQL & "and b.No_Faktur= g.No_Faktur and b.id_rencana in(" & id_rencana_group & ") and "
            SQL = SQL & "b.Status is null "
            SQL = SQL & "group by b.id_rencana, e.Jenis, "
            SQL = SQL & "d.Persen_tot_Konte, f.Konte_Per_Rencana, f.Total_Konte "
            SQL = SQL & ") "

            SQL = SQL & ",cte_form_e as( "
            SQL = SQL & "select b.id_rencana, e.Jenis, d.Persen_tot_Konte, "
            SQL = SQL & "sum(g.Jumlah* g.Berat_Bersih) as Tot_berat_bersih, ((((((" & biaya_form_e & ")* "
            SQL = SQL & "f.Konte_Per_Rencana)/f.Total_Konte)*d.Persen_tot_Konte)/100)/(sum(g.jumlah*g.Berat_Bersih))) "
            SQL = SQL & "as nilai_per_berat from Loading_Barang B, Detail_Loading_Barang G, Barang c, cte_Persen_konte d, "
            SQL = SQL & "Kategori_Besar e, cte_konte_per_lokasi f where g.kode_perusahaan = c.Kode_Perusahaan And g.Kode_stock_owner "
            SQL = SQL & "= c.Kode_Stock_Owner And g.Kode_barang = c.Kode_Barang and c.Kode_Perusahaan = "
            SQL = SQL & "e.Kode_Perusahaan and c.Kode_kategori_Besar = e.Kode_Kategori_Besar and b.Kode_Perusahaan = "
            SQL = SQL & "d.Kode_Perusahaan and b.Id_rencana = d.ID_Rencana and e.Jenis = d.Jenis and f.Kode_Perusahaan "
            SQL = SQL & "=b.KOde_Perusahaan and f.Id_Rencana = b.id_rencana and b.Kode_Perusahaan = G.Kode_Perusahaan "
            SQL = SQL & "and b.No_Faktur= g.No_Faktur and b.id_rencana in(" & id_rencana_group & ") and "
            SQL = SQL & "b.Status is null "
            SQL = SQL & "group by b.id_rencana, e.Jenis, "
            SQL = SQL & "d.Persen_tot_Konte, f.Konte_Per_Rencana, f.Total_Konte "
            SQL = SQL & ") "


            If Metode_Hitung_Konte = "A" Then

                SQL = SQL & ",cte_Kontainer as( "
                SQL = SQL & "select a.Kode_Perusahaan, a.id_rencana, a.ETA, c.Lokasi, b.kode_pelabuhan, c.free_storage, d.No_Container, "
                SQL = SQL & "d.Tgl_Tarik ,datediff(day,format(DATEADD(dd, c.free_storage, a.ETA), 'yyyy-MM-dd'), format(d.Tgl_Tarik, 'yyyy-MM-dd')) as jumlah_hari "
                SQL = SQL & "from ubah_status_otw a, "
                SQL = SQL & "kapal_tiba_import b, pelabuhan c, Tarik_Kontainer d  where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.id_rencana = b.id_rencana and b.Kode_Perusahaan = c.Kode_Perusahaan and b.kode_pelabuhan = "
                SQL = SQL & "c.Kode_Pelabuhan and a.Kode_Perusahaan = d.Kode_perusahaan and a.Id_rencana = d.id_rencana and "
                SQL = SQL & "a.id_rencana in(" & id_rencana_group & ") "
                SQL = SQL & ") "
                SQL = SQL & ",cte_total_Kontainer as ( "
                SQL = SQL & "select a.*,b.id_rencana, Harga*isnull(( "
                SQL = SQL & "select count(X.no_container) from cte_Kontainer X where jumlah_hari+1 >= dari "
                SQL = SQL & "and X.Id_rencana = d.id_rencana ), 0) as biaya "
                SQL = SQL & "from storage a, Kapal_Tiba_import b, Pelabuhan c, rencana_order d where "
                SQL = SQL & "b.Kode_Pelabuhan = c.Kode_Pelabuhan and B.Kode_Perusahaan = C.Kode_Perusahaan and "
                SQL = SQL & "a.kode_stock_owner = c.Lokasi And a.kode_pelabuhan = b.Kode_Pelabuhan and "
                SQL = SQL & "b.Kode_Perusahaan = d.Kode_Perusahaan  and b.Id_Rencana = d.Id_rencana and "
                SQL = SQL & "a.Kode_Kontainer = d.Kode_Kontainer "
                SQL = SQL & "and d.id_rencana in(" & id_rencana_group & ") "
                SQL = SQL & ") "


            ElseIf Metode_Hitung_Konte = "B" Then


                SQL = SQL & ",cte_total_Kontainer as ( "
                SQL = SQL & "select a.Kode_Perusahaan, a.id_rencana,e.Kode_Kontainer, a.ETA,b.kode_pelabuhan, c.free_storage, d.No_Container, "
                SQL = SQL & "d.Tgl_Tarik ,datediff(day,format(DATEADD(dd, c.free_storage, a.ETA), 'yyyy-MM-dd'), "
                SQL = SQL & "format(d.Tgl_Tarik, 'yyyy-MM-dd')) as jumlah_hari, "
                SQL = SQL & "isnull((select X.Harga from storage X where "
                SQL = SQL & "datediff(day,format(DATEADD(dd, c.free_storage, a.ETA), 'yyyy-MM-dd'), "
                SQL = SQL & "format(d.Tgl_Tarik, 'yyyy-MM-dd'))= sampai and X.Kode_Perusahaan = c.Kode_Perusahaan "
                SQL = SQL & "and X.Kode_Stock_Owner = c.Lokasi and X.Kode_Pelabuhan = c.Kode_Pelabuhan and X.Kode_Kontainer = e.Kode_Kontainer),0) as biaya from ubah_status_otw a, "
                SQL = SQL & "kapal_tiba_import b, pelabuhan c, Tarik_Kontainer d, rencana_order e  where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.id_rencana = b.id_rencana and b.Kode_Perusahaan = c.Kode_Perusahaan and b.kode_pelabuhan = "
                SQL = SQL & "c.Kode_Pelabuhan and a.Kode_Perusahaan = d.Kode_perusahaan and a.Id_rencana = d.id_rencana and "
                SQL = SQL & "a.kode_perusahaan = e.Kode_Perusahaan and a.Id_rencana = e.Id_rencana and "
                SQL = SQL & "a.id_rencana in(" & id_rencana_group & ")) "

            Else
                MessageBox.Show("Metode Perhitungan Konte tidak ada !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            SQL = SQL & ",cte_sum_kontainer as ( "
            SQL = SQL & "select a.id_rencana, Tot_berat_Bersih, b.Jenis, b.Persen_tot_Konte, ((sum(a.biaya)*Persen_tot_Konte)/100)/tot_berat_bersih as tot_bayar_per_berat "
            SQL = SQL & "from cte_Total_kontainer a, cte_billing b where a.id_rencana = b.id_rencana "
            SQL = SQL & "group by a.id_rencana,b.Tot_berat_Bersih, b.Jenis, b.Persen_tot_Konte) "


            SQL = SQL & ",cte_Kurs_freight as( "
            SQL = SQL & "select a.Kode_Perusahaan, c.id_rencana, a.no_faktur, "

            If Flag_Average_Sup = "Y" Then
                SQL = SQL & "(isnull((select sum(total_avg_biaya) from aVg_Kategori_Biaya_Import3 X where a.Kode_Perusahaan = X.Kode_Perusahaan "
                SQL = SQL & "and a.no_faktur = X.no_faktur ),0)*d.Konte_Per_Rencana)/d.Total_Konte as Grand_total_hpp "
            Else
                SQL = SQL & "(isnull((select sum(total_avg_biaya) from detail_transaksi_biaya_import3 X where a.Kode_Perusahaan = X.Kode_Perusahaan "
                SQL = SQL & "and a.no_faktur = X.no_faktur ),0)*d.Konte_Per_Rencana)/d.Total_Konte as Grand_total_hpp "
            End If





            SQL = SQL & ", isnull((select case "
            Dim CekMataUang2 As String = ""

            If ListView1.Items.Count <> 0 Then
                Dim data2 As Boolean = False
                For index As Integer = 0 To ListView1.Items.Count - 1
                    If ListView1.Items(index).SubItems(1).Text = "FREIGHT" Then
                        data2 = True
                        CekMataUang2 = CekMataUang2 & " when (isnull((select Mata_Uang_HPP as mata_uang from detail_transaksi_biaya_import3 X where a.Kode_Perusahaan = X.Kode_Perusahaan and a.no_faktur = X.no_faktur group by mata_uang_hpp),0)) = '" & ListView1.Items(index).SubItems(0).Text & "' then " & ListView1.Items(index).SubItems(2).Text & " "
                    End If
                Next

                If data2 = False Then
                    MessageBox.Show("Kurs untuk Freight tidak ada !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CloseTrans()
                    CloseConn()
                    Exit Sub
                End If

            Else
                CekMataUang2 = " when (isnull((select Mata_Uang_HPP as mata_uang from detail_transaksi_biaya_import3 X where a.Kode_Perusahaan = X.Kode_Perusahaan and a.no_faktur = X.no_faktur group by mata_uang_hpp),0)) = '' then 000000000 "

            End If
            SQL = SQL & CekMataUang2
            SQL = SQL & "else -1 end),0) as kurs_freight from "
            SQL = SQL & "Transaksi_Biaya_import3 a, Detail_Loading_Barang b, Loading_Barang c, cte_konte_per_lokasi D "
            SQL = SQL & "where a.Kode_Perusahaan = c.Kode_Perusahaan and a.id_rencana = '" & TxtId_Rencana.Text & "' And b.kode_perusahaan "
            SQL = SQL & "= c.Kode_Perusahaan And  b.No_Faktur = c.No_Faktur and c.id_rencana in(" & id_rencana_group & ") "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Rencana = d.Id_Rencana and a.status is null and c.status is null "
            SQL = SQL & "group by a.Kode_Perusahaan, a.Grand_total_hpp, c.id_rencana, a.no_faktur, d.Konte_Per_Rencana, d.Total_Konte "
            SQL = SQL & ") "

            SQL = SQL & ",cte_freight as( "
            SQL = SQL & "select c.id_rencana, (g.Grand_total_hpp * g.kurs_freight) as grand_total, e.Jenis, "
            SQL = SQL & "d.Persen_tot_Konte, sum(Total_berat_bersih) as Tot_Berat_Bersih, ((((g.Grand_total_hpp * "
            SQL = SQL & "g.kurs_freight)*d.Persen_tot_Konte)/100)/(sum(Total_berat_bersih))) as nilai_per_berat "
            SQL = SQL & "from Detail_Loading_Barang b, Loading_Barang c, Barang f, cte_Persen_konte d, Kategori_Besar e, "
            SQL = SQL & "cte_Kurs_freight g  where b.kode_perusahaan = c.Kode_Perusahaan And b.No_Faktur = c.No_Faktur And "
            SQL = SQL & "b.kode_perusahaan = f.Kode_Perusahaan And b.Kode_stock_owner = f.Kode_Stock_Owner And b.Kode_barang "
            SQL = SQL & "= f.Kode_Barang and f.Kode_Perusahaan = e.Kode_Perusahaan and f.Kode_kategori_Besar = "
            SQL = SQL & "e.Kode_Kategori_Besar and  c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_rencana = d.ID_Rencana "
            SQL = SQL & "and e.Jenis = d.Jenis and c.id_rencana in(" & id_rencana_group & ") and c.Kode_Perusahaan = g.Kode_Perusahaan "
            SQL = SQL & "and c.id_rencana = g.id_rencana and c.status is null "
            SQL = SQL & "group by b.Kode_Perusahaan, c.id_rencana, g.no_faktur, b.No_Faktur, g.Grand_total_hpp, "
            SQL = SQL & "g.kurs_freight, e.Jenis, d.Persen_tot_Konte "
            SQL = SQL & ") "

            SQL = SQL & ",cte_data as("
            SQL = SQL & "select a.Kode_Perusahaan, cast(c.rv as int) as rvx, a.Kode_Stock_Owner, b.kode_barang, "
            SQL = SQL & "b.Nama, e.Jumlah, volume,  Jml_Satuan_Besar as "
            SQL = SQL & "tot_sat_bsr, e.isi_satuan_besar, e.Berat_bersih as berat, e.berat_kotor, Total_Berat_Bersih as tot_berat_brsh, "
            SQL = SQL & "Total_Berat_Kotor as tot_berat_kotor, e.panjang, e.lebar, e.tinggi, a.no_urut, b.mata_uang, e.Harga_Declare, e.Total, "

            SQL = SQL & "ISNULL(("
            SQL = SQL & "select sum(X.Jumlah*Y.Harga_IDR) from det_Loading_Barang X, Bahan_SN Y, Loading_Barang Z where "
            SQL = SQL & "X.Kode_Perusahaan = Y.Kode_Perusahaan And X.Serial_Number = Y.Serial_Number And "
            SQL = SQL & "X.Kode_Perusahaan = Z.Kode_Perusahaan and X.No_Faktur = Z.No_Faktur and "
            SQL = SQL & "Z.Id_Rencana = A.Id_rencana and x.kode_stock_owner = a.kode_stock_owner and X.Kode_Barang = A.Kode_Barang and z.status is null "
            'SQL = SQL & "and Z.No_Faktur = 'LB-DS-12/22-0008' " 'Hapus'
            SQL = SQL & "),0)as Tot_Pot_Stock_IDR , "

            'SQL = SQL & "isnull(("
            'SQL = SQL & "select sum(X.Harga*Y.Kurs) from detail_Loading_Barang3 X, detail_val_pel_Hutang_Loading_Barang Y, "
            'SQL = SQL & "Loading_Barang Z, Detail_Loading_Barang2 W where "
            'SQL = SQL & "X.Kode_Perusahaan = Y.Kode_Perusahaan And "
            'SQL = SQL & "x.kode_Stock_owner_import = y.kode_Stock_owner_import AND X.Kode_Bahan = Y.KOde_Bahan and "
            'SQL = SQL & "X.No_Faktur = Y.No_Faktur and X.Kode_Perusahaan = Z.Kode_Perusahaan and X.No_Faktur = Z.No_Faktur and "
            'SQL = SQL & "X.Kode_Perusahaan = W.Kode_Perusahaan and X.No_Faktur = W.No_Faktur and "
            'SQL = SQL & "x.kode_Stock_owner_import = y.kode_Stock_owner_import AND X.KOde_Bahan = W.Kode_Bahan and "
            'SQL = SQL & "W.Flag_Potong_stock = 'T' and W.Flag_Lunas = 'Y' and Z.Id_Rencana = A.Id_rencana and "
            'SQL = SQL & "x.kode_stock_owner = a.kode_stock_owner and X.Kode_Barang = A.Kode_Barang and z.status is null "
            ''SQL = SQL & "and Z.No_Faktur = 'LB-DS-12/22-0008' " 'Hapus'
            'SQL = SQL & "), 0) as Tdk_Pot_Stock_IDR "


            SQL = SQL & "0 as Tdk_Pot_Stock_IDR "
            SQL = SQL & ",isnull((select sum(case "

            Dim CekMataUang As String = ""

            If ListView1.Items.Count <> 0 Then

                Dim data As Boolean = False
                For index As Integer = 0 To ListView1.Items.Count - 1

                    If ListView1.Items(index).SubItems(1).Text = "UTAMA" Then
                        data = True
                        CekMataUang = CekMataUang & " when W.Mata_Uang = '" & ListView1.Items(index).SubItems(0).Text & "' then X.Harga*" & ListView1.Items(index).SubItems(2).Text & " "
                    End If
                Next

                If data = False Then
                    MessageBox.Show("Kurs untuk Bahan Utama tidak ada !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CloseTrans()
                    CloseConn()
                    Exit Sub
                End If

            Else
                CekMataUang = " when W.Mata_Uang = '' then 000000000 "
            End If

            SQL = SQL & CekMataUang
            SQL = SQL & "else -1 end) from detail_Loading_Barang3 X, Loading_Barang Z, Detail_Loading_Barang2 W "
            SQL = SQL & "where X.Kode_Perusahaan = Z.Kode_Perusahaan and X.No_Faktur = Z.No_Faktur and "
            SQL = SQL & "X.Kode_Perusahaan = W.Kode_Perusahaan and X.No_Faktur = W.No_Faktur and "
            SQL = SQL & "x.kode_stock_owner_import = w.kode_Stock_owner_import and X.Kode_Bahan = W.Kode_Bahan and "
            SQL = SQL & "W.Flag_Potong_stock = 'T' and W.Kategori = 'Utama' and W.Flag_Lunas is null and Z.Id_Rencana = A.Id_rencana and "
            SQL = SQL & "x.kode_stock_owner = a.kode_Stock_owner and X.Kode_Barang = A.Kode_Barang and z.status is null "
            'SQL = SQL & "and Z.No_Faktur = 'LB-DS-12/22-0008' " 'Hapus'
            SQL = SQL & "),0)as Tdk_Pot_Stock_Hutang_IDR_Utama "

            SQL = SQL & ",isnull((select sum(case "

            Dim CekMataUang3 As String = ""

            If ListView1.Items.Count <> 0 Then

                Dim data3 As Boolean = False
                For index As Integer = 0 To ListView1.Items.Count - 1

                    If ListView1.Items(index).SubItems(1).Text = "PENOLONG" Then
                        data3 = True
                        CekMataUang3 = CekMataUang3 & " when W.Mata_Uang = '" & ListView1.Items(index).SubItems(0).Text & "' then X.Harga*" & ListView1.Items(index).SubItems(2).Text & " "
                    End If
                Next
                If data3 = False Then
                    MessageBox.Show("Kurs untuk Bahan Penolong tidak ada !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CloseTrans()
                    CloseConn()
                    Exit Sub
                End If
            Else
                CekMataUang3 = " when W.Mata_Uang = '' then 000000000 "
            End If

            SQL = SQL & CekMataUang3
            SQL = SQL & "else -1 end) from detail_Loading_Barang3 X, Loading_Barang Z, Detail_Loading_Barang2 W "
            SQL = SQL & "where X.Kode_Perusahaan = Z.Kode_Perusahaan and X.No_Faktur = Z.No_Faktur and "
            SQL = SQL & "X.Kode_Perusahaan = W.Kode_Perusahaan and X.No_Faktur = W.No_Faktur and "
            SQL = SQL & "x.kode_stock_owner_import = w.kode_Stock_owner_import and X.Kode_Bahan = W.Kode_Bahan and "
            SQL = SQL & "W.Flag_Potong_stock = 'T' and W.Kategori = 'Penolong' and W.Flag_Lunas is null and Z.Id_Rencana = A.Id_rencana and "
            SQL = SQL & "x.kode_stock_owner = a.kode_Stock_owner and X.Kode_Barang = A.Kode_Barang and z.status is null "
            'SQL = SQL & "and Z.No_Faktur = 'LB-DS-12/22-0008' " 'Hapus'
            SQL = SQL & "),0)as Tdk_Pot_Stock_Hutang_IDR_Penolong "

            SQL = SQL & ",isnull((select X.nilai_Per_berat*e.berat_bersih*a.Jumlah_PO from cte_billing X where X.Id_Rencana= a.Id_rencana "
            SQL = SQL & "and X.Jenis = d.Jenis),0) as Biaya_Billing "
            SQL = SQL & ",isnull((select X.nilai_Per_berat*e.berat_bersih*a.Jumlah_PO from cte_biaya X where X.Id_Rencana= a.Id_rencana "
            SQL = SQL & "and X.Jenis = d.Jenis),0) as Biaya_import "
            SQL = SQL & ",isnull((select X.tot_bayar_per_berat*e.berat_bersih*a.Jumlah_PO from cte_sum_kontainer X where X.Id_Rencana= a.Id_rencana "
            SQL = SQL & "and X.Jenis = d.Jenis),0) as Biaya_kontainer "
            SQL = SQL & ",isnull((select X.nilai_per_berat*e.berat_bersih*a.Jumlah_PO from cte_freight X where X.Id_Rencana= a.Id_rencana "
            SQL = SQL & "and X.Jenis = d.Jenis),0) as Biaya_freight_int "
            SQL = SQL & ",isnull((select X.nilai_per_berat*e.berat_bersih*a.Jumlah_PO from cte_selisih X where X.Id_Rencana= a.Id_rencana "
            SQL = SQL & "and X.Jenis = d.Jenis),0) as Biaya_selisih "
            SQL = SQL & ",isnull((select X.nilai_per_berat*e.berat_bersih*a.Jumlah_PO from cte_selisih_biaya X where X.Id_Rencana= a.Id_rencana "
            SQL = SQL & "and X.Jenis = d.Jenis),0) as Biaya_selisih_biaya "
            SQL = SQL & ",isnull((select X.nilai_per_berat*e.berat_bersih*a.Jumlah_PO from cte_form_e X where X.Id_Rencana= a.Id_rencana "
            SQL = SQL & "and X.Jenis = d.Jenis),0) as Biaya_form_e "

            SQL = SQL & "from detail_rencana_order a, barang b, "
            SQL = SQL & "rencana_order c, Kategori_Besar d, Detail_Loading_Barang e, Loading_Barang f where a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = "
            SQL = SQL & "c.kode_perusahaan and a.kode_stock_owner = b.kode_stock_owner and a.kode_barang = b.kode_Barang and "
            SQL = SQL & "a.id_rencana = c.id_rencana and a.Jumlah_PO <> 0 and b.Kode_Perusahaan = d.Kode_Perusahaan and "
            SQL = SQL & "b.Kode_Kategori_Besar = d.Kode_Kategori_Besar and "
            SQL = SQL & "f.Kode_Perusahaan = c.Kode_Perusahaan and f.Id_rencana = c.Id_Rencana and "
            SQL = SQL & "e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Faktur= f.No_Faktur and "
            SQL = SQL & "a.Kode_Perusahaan = e.Kode_Perusahaan And a.Kode_Stock_Owner = e.Kode_Stock_Owner And a.Kode_Barang = e.Kode_Barang "
            SQL = SQL & "and a.id_rencana in(" & id_rencana_group & ") and f.status is null and c.status is null "
            SQL = SQL & ")"
            'SQL = SQL & "and f.No_Faktur = 'LB-DS-12/22-0008'" 'Hapus'
            SQL = SQL & "select Kode_Perusahaan, Kode_Barang, Nama, sum(jumlah) as jumlah, sum(volume) as volume, sum(tot_sat_bsr) as tot_sat_bsr "
            SQL = SQL & ", isi_satuan_besar,berat, berat_kotor, sum(tot_berat_brsh) as tot_berat_brsh, "
            SQL = SQL & "sum(tot_berat_kotor) as tot_berat_kotor, panjang, lebar, tinggi, mata_uang, Harga_Declare, isnull((select Kurs from "
            SQL = SQL & "Total_Billing X, Detail_Total_Billing Y where X.Kode_Perusahaan = Y.Kode_Perusahaan And X.No_Faktur = Y.No_Faktur "
            SQL = SQL & "and X.Id_Rencana = '" & TxtId_Rencana.Text & "' and Y.Kode_Perusahaan = A.Kode_Perusahaan and Y.Kode_Barang = A.Kode_Barang "
            SQL = SQL & "and Y.Kode_Stock_Owner ='" & Lokasi_gudang & "' and X.status is null),0) as kurs, sum(Total) as Total, "
            SQL = SQL & "sum(Tot_Pot_Stock_IDR) as Tot_Pot_Stock_IDR, sum(Tdk_Pot_Stock_IDR) as Tdk_Pot_Stock_IDR, "
            SQL = SQL & "sum(Tdk_Pot_Stock_Hutang_IDR_Utama) as Tdk_Pot_Stock_Hutang_IDR_Utama, sum(Tdk_Pot_Stock_Hutang_IDR_Penolong) as Tdk_Pot_Stock_Hutang_IDR_Penolong "
            SQL = SQL & ",sum(biaya_Billing) as biaya_Billing, sum(Biaya_import) as Biaya_Import, "
            SQL = SQL & "sum(Biaya_Kontainer) as Biaya_Kontainer, sum(Biaya_Freight_int) as Biaya_Freight_int, sum(Biaya_Selisih) as Biaya_Selisih, sum(Biaya_Selisih_biaya) as Biaya_Selisih_Biaya, sum(Biaya_form_e) as Biaya_form_e "
            SQL = SQL & "from cte_data A "
            SQL = SQL & "group by Kode_Perusahaan, Kode_Barang, Nama, isi_satuan_besar,berat, berat_kotor, panjang, lebar, tinggi, "
            SQL = SQL & "mata_uang, Harga_Declare "
            SQL = SQL & "order by nama "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For index As Integer = 0 To .Rows.Count - 1
                        ' TextBoxRV.Text = .Rows(0).Item("rvx")

                        'If General_Class.CekNULL(.Rows(index).Item("Harga_Declare")) = "" Then
                        '    CloseConn()
                        '    MessageBox.Show(" Ada data yang masih kosong di Variabel Barang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    DataGridView1.Rows.Clear()
                        '    Exit Sub
                        'End If

                        'If .Rows(index).Item("mata_uang") <> Mata_Uang_Declare Then
                        '    CloseConn()
                        '    MessageBox.Show("Mata Uang Declare Supplier Berbeda dengan barang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    DataGridView1.Rows.Clear()
                        '    Exit Sub
                        'End If

                        'If .Rows(0).Item("mata_uang") <> .Rows(index).Item("mata_uang") Then
                        '    CloseConn()
                        '    MessageBox.Show("Ada Mata Uang yang Berbeda pada Barang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    DataGridView1.Rows.Clear()
                        '    Exit Sub
                        'End If



                        Dim nilai_PPH As Double = 0
                        DataGridView1.Rows.Add(1)
                        DataGridView1.Rows.Item(index).Cells(cellLokasi).Value = Lokasi_gudang
                        DataGridView1.Rows.Item(index).Cells(cellKdBarang).Value = .Rows(index).Item("kode_barang")
                        DataGridView1.Rows.Item(index).Cells(cellNama).Value = .Rows(index).Item("Nama")
                        DataGridView1.Rows.Item(index).Cells(cellJumlah).Value = .Rows(index).Item("Jumlah")
                        DataGridView1.Rows.Item(index).Cells(cellHarga).Value = Format(.Rows(index).Item("Harga_Declare"), "N2")
                        DataGridView1.Rows.Item(index).Cells(cellTotalHarga).Value = Format(.Rows(index).Item("Total"), "N2")
                        DataGridView1.Rows.Item(index).Cells(cellVolume).Value = Format(.Rows(index).Item("volume"), "N0")
                        DataGridView1.Rows.Item(index).Cells(cellJmlBsr).Value = .Rows(index).Item("tot_sat_bsr")
                        DataGridView1.Rows.Item(index).Cells(cellIsiBsr).Value = .Rows(index).Item("isi_satuan_besar")
                        DataGridView1.Rows.Item(index).Cells(cellBeratBrsh).Value = Format(.Rows(index).Item("Berat"), "N0")
                        DataGridView1.Rows.Item(index).Cells(cellBeratKtr).Value = Format(.Rows(index).Item("berat_kotor"), "N0")
                        DataGridView1.Rows.Item(index).Cells(cellTotBeratBrsh).Value = Format(.Rows(index).Item("tot_berat_brsh"), "N0")
                        DataGridView1.Rows.Item(index).Cells(cellTotBeratKtr).Value = Format(.Rows(index).Item("tot_berat_kotor"), "N0")
                        DataGridView1.Rows.Item(index).Cells(cellPjg).Value = .Rows(index).Item("panjang")
                        DataGridView1.Rows.Item(index).Cells(cellLbr).Value = .Rows(index).Item("lebar")
                        DataGridView1.Rows.Item(index).Cells(cellTinggi).Value = .Rows(index).Item("tinggi")
                        DataGridView1.Rows.Item(index).Cells(cellUrut).Value = 0 '.Rows(index).Item("No_Urut")
                        DataGridView1.Rows.Item(index).Cells(cellMataUang).Value = .Rows(index).Item("Mata_Uang")
                        DataGridView1.Rows.Item(index).Cells(cellPotStock).Value = Format(.Rows(index).Item("Tot_Pot_Stock_IDR"), "N2")
                        DataGridView1.Rows.Item(index).Cells(cellTdkPotStockLunas).Value = Format(.Rows(index).Item("Tdk_Pot_Stock_IDR"), "N2")
                        DataGridView1.Rows.Item(index).Cells(cellTdkPotStockBlumLunasUtama).Value = Format(.Rows(index).Item("Tdk_Pot_Stock_Hutang_IDR_Utama") + .Rows(index).Item("biaya_form_e"), "N2")
                        DataGridView1.Rows.Item(index).Cells(cellTdkPotStockBlumLunasPenolong).Value = Format(.Rows(index).Item("Tdk_Pot_Stock_Hutang_IDR_Penolong"), "N2")
                        DataGridView1.Rows.Item(index).Cells(cellSelisihPO).Value = Format(.Rows(index).Item("Biaya_Selisih"), "N2")

                        DataGridView1.Rows.Item(index).Cells(cellTotHPPBsr).Value = Format(.Rows(index).Item("Tot_Pot_Stock_IDR") + .Rows(index).Item("Tdk_Pot_Stock_IDR") + .Rows(index).Item("Tdk_Pot_Stock_Hutang_IDR_Utama") + .Rows(index).Item("biaya_form_e") + .Rows(index).Item("Tdk_Pot_Stock_Hutang_IDR_Penolong"), "N2")
                        DataGridView1.Rows.Item(index).Cells(cellTotHPPKcl).Value = Format((.Rows(index).Item("Tot_Pot_Stock_IDR") + .Rows(index).Item("Tdk_Pot_Stock_IDR") + .Rows(index).Item("Tdk_Pot_Stock_Hutang_IDR_Utama") + .Rows(index).Item("biaya_form_e") + .Rows(index).Item("Tdk_Pot_Stock_Hutang_IDR_Penolong")) / .Rows(index).Item("Jumlah"), "N2")
                        DataGridView1.Rows.Item(index).Cells(cellBiayaImport).Value = Format(.Rows(index).Item("Biaya_Import"), "N2")
                        DataGridView1.Rows.Item(index).Cells(cellKurs).Value = Format(.Rows(index).Item("kurs"), "N2")
                        DataGridView1.Rows.Item(index).Cells(cellColumn1).Value = Format((.Rows(index).Item("Total") * .Rows(index).Item("kurs") * 1) / 100, "N0")
                        DataGridView1.Rows.Item(index).Cells(cellColumn2).Value = Format((.Rows(index).Item("Total") * .Rows(index).Item("kurs") * 0.5) / 100, "N0")

                        nilai_PPH = Val(HilangkanTanda(Format(((.Rows(index).Item("Total") * .Rows(index).Item("kurs") * 1) / 100), "N0"))) + Val(HilangkanTanda(Format(((.Rows(index).Item("Total") * .Rows(index).Item("kurs") * 0.5) / 100), "N0")))
                        DataGridView1.Rows.Item(index).Cells(cellColumn3).Value = Format(nilai_PPH, "N0")

                        nilai_PPH = HilangkanTanda(Format(nilai_PPH, "N0"))

                        DataGridView1.Rows.Item(index).Cells(cellBilling).Value = Format(.Rows(index).Item("Biaya_Billing"), "N2")
                        DataGridView1.Rows.Item(index).Cells(cellBiayaKonte).Value = Format(.Rows(index).Item("Biaya_Kontainer"), "N2")
                        DataGridView1.Rows.Item(index).Cells(cellBiayaFreight).Value = Format(.Rows(index).Item("Biaya_freight_int"), "N2")
                        DataGridView1.Rows.Item(index).Cells(cellHPP_Per_Pcs).Value = Format((.Rows(index).Item("Tot_Pot_Stock_IDR") + .Rows(index).Item("Tdk_Pot_Stock_IDR") + .Rows(index).Item("Tdk_Pot_Stock_Hutang_IDR_Utama") + .Rows(index).Item("biaya_form_e") + .Rows(index).Item("Tdk_Pot_Stock_Hutang_IDR_Penolong") + .Rows(index).Item("Biaya_Selisih") + .Rows(index).Item("Biaya_Import") + .Rows(index).Item("Biaya_Billing") + .Rows(index).Item("Biaya_Kontainer") + .Rows(index).Item("Biaya_freight_int") + nilai_PPH + .Rows(index).Item("Biaya_Selisih_Biaya")) / .Rows(index).Item("Jumlah"), "N0")

                        DataGridView1.Rows.Item(index).Cells(cellSelisihPO_biaya).Value = Format(.Rows(index).Item("Biaya_Selisih_Biaya"), "N2")

                        'SQL = "select kode_barang from komposisi_barang_jadi a "
                        'SQL = SQL & "where kode_barang = '" & .Rows(index).Item("kode_barang") & "' and  a.kode_perusahaan = '" & KodePerusahaan & "' "
                        'Using dr = OpenTrans(SQL)
                        '    If Not dr.Read Then
                        '        CloseConn()
                        '        MessageBox.Show(.Rows(index).Item("Nama") & " tidak ada dalam komposisi barang jadi")
                        '        DataGridView1.Rows.Clear()
                        '        Exit Sub
                        '    End If
                        'End Using

                        SQL = "Insert Into HPP_Temp(Kode_Unik, Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Harga, Isi_Satuan_Besar, "
                        SQL = SQL & "Berat_Bersih, Berat_Kotor, Panjang, Lebar, Tinggi, Nilai_HPP_Barang_Per_pcs)"
                        SQL = SQL & "Values('" & Kode_Unik & "', '" & KodePerusahaan & "', '" & Lokasi_gudang & "', '" & .Rows(index).Item("kode_barang") & "', "
                        SQL = SQL & "'" & .Rows(index).Item("Harga_Declare") & "', '" & .Rows(index).Item("isi_satuan_besar") & "', '" & .Rows(index).Item("Berat") & "', '" & .Rows(index).Item("Berat_Kotor") & "', "
                        SQL = SQL & "'" & .Rows(index).Item("panjang") & "', '" & .Rows(index).Item("Lebar") & "', '" & .Rows(index).Item("Tinggi") & "', "
                        SQL = SQL & "'" & (.Rows(index).Item("Tot_Pot_Stock_IDR") + .Rows(index).Item("Tdk_Pot_Stock_IDR") + .Rows(index).Item("Tdk_Pot_Stock_Hutang_IDR_Utama") + .Rows(index).Item("biaya_form_e") + .Rows(index).Item("Tdk_Pot_Stock_Hutang_IDR_Penolong") + .Rows(index).Item("Biaya_Selisih") + .Rows(index).Item("Biaya_Import") + .Rows(index).Item("Biaya_Billing") + .Rows(index).Item("Biaya_Kontainer") + .Rows(index).Item("Biaya_freight_int") + nilai_PPH + .Rows(index).Item("Biaya_Selisih_Biaya")) / .Rows(index).Item("Jumlah") & "')"
                        ExecuteTrans(SQL)
                    Next
                End With
            End Using



            'Flag_Average_Sup = "T"
            SQL = ";with cte_persen_konte as( "
            SQL = SQL & "select C.Kode_Perusahaan, b.ID_Rencana, C.No_Faktur, a.Jenis, c.Kode_Gudang, "
            SQL = SQL & "sum(Persentase)/isnull((select count(Y.No_Container) from Kontainer_Masuk_Per_Lokasi Y "
            SQL = SQL & "where Y.Kode_Perusahaan = C.Kode_Perusahaan and Y.No_Faktur = C.No_Faktur and Y.Kode_Gudang "
            SQL = SQL & "= C.Kode_Gudang ),0) as Persen_tot_Konte from Kontainer_Masuk_Per_Jenis a, Submit_PO b "
            SQL = SQL & ", Kontainer_Masuk_Per_Lokasi c where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur "
            SQL = SQL & "= b.no_faktur and a.Kode_Perusahaan = c.Kode_Perusahaan And a.No_Container = c.No_Container "
            SQL = SQL & "And a.no_faktur = c.no_faktur and b.id_rencana in (" & id_rencana_group & ") and b.status is null group by "
            SQL = SQL & "c.Kode_Perusahaan, b.ID_Rencana, C.No_Faktur, a.Jenis, c.Kode_Gudang "
            SQL = SQL & ")"

            SQL = SQL & ",cte_konte_per_lokasi as( "
            SQL = SQL & "select a.KOde_Perusahaan, a.Id_Rencana, c.Kode_Gudang, Count (distinct B.No_container) as Konte_Per_Rencana, "
            SQL = SQL & "Isnull((select Count (distinct Y.No_container) from submit_PO X, Kontainer_Masuk Y, Kontainer_masuk_per_lokasi Z "
            SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur and "
            SQL = SQL & "y.Kode_Perusahaan = z.kode_Perusahaan And y.no_faktur = z.No_Faktur And y.No_Container = z.No_Container "
            SQL = SQL & "and x. Id_Rencana in (" & id_rencana_group & ") and X.status is null and z.Kode_Gudang =c.Kode_Gudang ),0) as Total_Konte from submit_PO A, Kontainer_Masuk B, Kontainer_masuk_per_lokasi c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur and "
            SQL = SQL & "b.Kode_Perusahaan = c.kode_Perusahaan And b.no_faktur = c.No_Faktur And b.No_Container = c.No_Container "
            SQL = SQL & "and A.Status is null and A. Id_Rencana in "
            SQL = SQL & "(" & id_rencana_group & ") group by a.Kode_Perusahaan, a.Id_Rencana, c.Kode_Gudang "
            SQL = SQL & ")"

            SQL = SQL & ",cte_persen_konteWET as( "
            SQL = SQL & "select C.Kode_Perusahaan, b.ID_Rencana, C.No_Faktur, a.Jenis, c.Kode_Gudang, "
            SQL = SQL & "sum(Persentase) as Persen_tot_Konte,isnull((select sum(z.Persentase) from Kontainer_Masuk_Per_Lokasi Y, "
            SQL = SQL & "Kontainer_Masuk_Per_Jenis Z, Rencana_order XX, Submit_po YY where Y.Kode_Perusahaan=z.Kode_perusahaan and Y.no_faktur=z.No_faktur "
            SQL = SQL & "and Y.No_Container=Z.No_COntainer and Y.Kode_Perusahaan= C.Kode_Perusahaan and z.Jenis=a.Jenis "
            SQL = SQL & "and Y.Kode_Gudang = C.Kode_Gudang and xx.ID_Rencana=yy.Id_Rencana and xx.Kode_Perusahaan=yy.Kode_Perusahaan "
            SQL = SQL & "and yy.Status is null and yy.No_Faktur=y.No_Faktur and yy.Kode_Perusahaan=y.Kode_Perusahaan and xx.ID_Rencana IN(" & id_rencana_group & ")),0) as Total_PersenWETDRY from Kontainer_Masuk_Per_Jenis a, "
            SQL = SQL & "Submit_PO b , Kontainer_Masuk_Per_Lokasi c where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.no_faktur "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan And a.No_Container = c.No_Container And a.no_faktur = c.no_faktur and "
            SQL = SQL & "b.id_rencana in (" & id_rencana_group & ") and b.status is null "
            SQL = SQL & "group by c.Kode_Perusahaan, b.ID_Rencana, C.No_Faktur, a.Jenis, c.Kode_Gudang "
            SQL = SQL & ")"


            If Flag_Average_Sup <> "Y" Then

                SQL = SQL & ",cte_sum_detail_biaya as ( "
                SQL = SQL & "select a.kode_perusahaan, a.No_Faktur, a.id_rencana, b.Kode_stock_Owner, sum(b.total_avg_biaya)"
                SQL = SQL & "as grand_total from "
                SQL = SQL & "transaksi_biaya_import a, detail_transaksi_biaya_import b where a.kode_perusahaan = "
                SQL = SQL & "b.kode_perusahaan and a.no_faktur = b.no_faktur and a.status is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Id_Rencana = '" & TxtId_Rencana.Text & "' and b.Jenis_Perhitungan = 'G' and b.Jns = 'all' "
                SQL = SQL & "group by a.kode_perusahaan, a.No_Faktur, a.id_rencana, b.Kode_stock_Owner ) "

                SQL = SQL & ",cte_sum_detail_biaya_wet_dry as ( "
                SQL = SQL & "select a.kode_perusahaan, a.No_Faktur, a.id_rencana, b.Kode_stock_Owner, b.jns, "
                SQL = SQL & "sum(b.total_avg_biaya)as grand_total from transaksi_biaya_import a, detail_transaksi_biaya_import b "
                SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan And a.no_faktur = b.no_faktur And a.status Is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Id_Rencana = '" & TxtId_Rencana.Text & "' and b.Jenis_Perhitungan = 'G' and b.Jns <> 'all' "
                SQL = SQL & "group by a.kode_perusahaan, a.No_Faktur, a.id_rencana, b.Kode_stock_Owner, b.jns "
                SQL = SQL & ") "

            End If

            SQL = SQL & ",cte_berat as( "
            SQL = SQL & "select a.Kode_perusahaan, a.Id_Rencana, b.No_Faktur, b.Kode_stock_Owner, c.Kode_Gudang, "
            SQL = SQL & "f.jenis, sum(b.qty*G.Berat_Bersih) as Total_Berat from Submit_PO A, Kontainer_masuk B, "
            SQL = SQL & "Kontainer_Masuk_Per_Lokasi C, barang e, Kategori_Besar f, Detail_Submit_PO G where A.Kode_Perusahaan = "
            SQL = SQL & "B.Kode_Perusahaan And A.No_Faktur = B.No_Faktur And B.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and B.No_Container = C.No_Container and b.Kode_Perusahaan = "
            SQL = SQL & "e.Kode_Perusahaan And b.Kode_stock_Owner = e.Kode_stock_Owner And b.Kode_Barang = "
            SQL = SQL & "e.Kode_Barang and e.Kode_Perusahaan = f.Kode_Perusahaan and e.Kode_Kategori_Besar = "
            SQL = SQL & "f.Kode_Kategori_besar and B.Kode_Perusahaan = G.Kode_Perusahaan and B.No_Faktur= G.No_Faktur "
            SQL = SQL & "and B.Kode_Barang = G.Kode_Barang and B.Kode_Stock_Owner = G.Kode_Stock_Owner "
            SQL = SQL & "and ID_Rencana in (" & id_rencana_group & ") and a.status is null "
            SQL = SQL & "group by a.Kode_perusahaan, a.Id_Rencana, b.No_Faktur, b.Kode_stock_Owner, c.Kode_Gudang, f.jenis "
            SQL = SQL & ")"


            If Flag_Average_Sup <> "Y" Then

                SQL = SQL & ",cte_nilai_per_lokasi as( "
                SQL = SQL & "select A.Kode_Perusahaan, A.Id_Rencana, A.Jenis, a.Kode_Gudang, "
                SQL = SQL & "(((A.Persen_tot_Konte*((b.grand_total*d.Konte_Per_Rencana)/d.Total_Konte))/100)/c.Total_Berat) as Nilai_Per_Berat "
                SQL = SQL & "from Cte_persen_Konte a, cte_sum_detail_biaya b, Cte_Berat C, cte_konte_per_lokasi d where a.Kode_Perusahaan = "
                SQL = SQL & "b.Kode_Perusahaan And B.Kode_Stock_Owner = A.Kode_Gudang and a.Kode_Perusahaan = "
                SQL = SQL & "c.Kode_Perusahaan And a.id_Rencana = c.Id_Rencana And  a.Kode_Gudang = C.Kode_Gudang "
                SQL = SQL & "And a.Jenis = c.Jenis and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_rencana = d.Id_rencana "
                SQL = SQL & " and c.Kode_Gudang =d.Kode_Gudang ) "


                SQL = SQL & ",cte_nilai_per_lokasi_dry_wet as( "
                SQL = SQL & "select A.Kode_Perusahaan, A.Id_Rencana, A.Jenis, a.Kode_Gudang, "
                SQL = SQL & "(((b.grand_total*a.Persen_tot_Konte)/a.Total_PersenWETDRY)/c.Total_Berat) "
                SQL = SQL & "as Nilai_Per_Berat from Cte_persen_KonteWET a, cte_sum_detail_biaya_wet_dry b, Cte_Berat C, "
                SQL = SQL & "cte_konte_per_lokasi d where a.Kode_Perusahaan = b.Kode_Perusahaan And B.Kode_Stock_Owner "
                SQL = SQL & "= A.Kode_Gudang and a.Jenis = b.jns and a.Kode_Perusahaan = c.Kode_Perusahaan And a.id_Rencana = c.Id_Rencana "
                SQL = SQL & "And  a.Kode_Gudang = C.Kode_Gudang And a.Jenis = c.Jenis and c.Kode_Perusahaan = "
                SQL = SQL & "d.Kode_Perusahaan and c.Id_rencana = d.Id_rencana and c.Kode_Gudang =d.Kode_Gudang) "

            End If



            SQL = SQL & ",cte_data as( "
            SQL = SQL & "select a.Id_Rencana, b.No_Faktur, isnull((select top(1) X.Kode_stock_Owner From detail_rencana_order X where X.id_rencana ='" & TxtId_Rencana.Text & "'),0) as Kode_stock_Owner "
            SQL = SQL & ", b.Kode_Barang, e.nama, c.Kode_Gudang, f.Jenis, sum(b.qty) as Jumlah, "


            If Flag_Average_Sup = "Y" Then

                SQL = SQL & "0 as Total_Bayar, 0 as Total_Bayar_dry_wet "

            Else

                SQL = SQL & "sum(b.qty)*G.Berat_Bersih* "
                SQL = SQL & "isnull((select X.Nilai_Per_Berat from cte_nilai_per_lokasi X where X.Kode_Gudang = "
                SQL = SQL & "c.Kode_Gudang and X.Jenis =f.Jenis and X.id_rencana = A.id_rencana ),0) as Total_Bayar, "

                SQL = SQL & "sum(b.qty)*G.Berat_Bersih* "
                SQL = SQL & "isnull((select X.Nilai_Per_Berat from cte_nilai_per_lokasi_dry_wet X where X.Kode_Gudang = "
                SQL = SQL & "c.Kode_Gudang and X.Jenis =f.Jenis and X.id_rencana = A.id_rencana ),0) as Total_Bayar_dry_wet "

            End If


            SQL = SQL & " ,c.lokasi_tujuan "
            SQL = SQL & "from Submit_PO A, Kontainer_masuk B, Kontainer_Masuk_Per_Lokasi C, "
            SQL = SQL & "Barang e, Kategori_Besar f, Detail_Submit_PO G where A.Kode_Perusahaan = B.Kode_Perusahaan and A.No_Faktur = "
            SQL = SQL & "B.No_Faktur and B.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and "
            SQL = SQL & "B.No_Container = C.No_Container And b.Kode_Perusahaan = e.Kode_Perusahaan And b.Kode_stock_Owner "
            SQL = SQL & "= e.Kode_stock_Owner And b.Kode_Barang = e.Kode_Barang and e.Kode_Perusahaan = f.Kode_Perusahaan "
            SQL = SQL & "and e.Kode_Kategori_Besar = f.Kode_Kategori_Besar and B.Kode_Perusahaan = G.Kode_Perusahaan and B.No_Faktur= G.No_Faktur "
            SQL = SQL & "and B.Kode_Barang = G.Kode_Barang and B.Kode_Stock_Owner = G.Kode_Stock_Owner and ID_Rencana in(" & id_rencana_group & ") and "
            SQL = SQL & "a.status is null "
            SQL = SQL & "group by a.Id_Rencana, b.No_Faktur, b.Kode_Barang, e.nama, c.Kode_Gudang, f.Jenis, G.Berat_Bersih,c.lokasi_tujuan "
            SQL = SQL & ") "

            SQL = SQL & "select a.Kode_stock_Owner, a.Kode_Barang, a.nama, a.Kode_Gudang,a.lokasi_tujuan, "
            SQL = SQL & "d.Harga, (d.Harga*sum(a.jumlah))as total, d.Isi_Satuan_Besar, d.Panjang, d.Lebar, d.Tinggi, "
            SQL = SQL & "((d.Panjang*d.Lebar*d.Tinggi)*(sum(a.jumlah)/d.Isi_satuan_Besar))as volume, d.Berat_Bersih, "
            SQL = SQL & "(d.Berat_Bersih*sum(a.jumlah)) as tot_brt_brsh, d.Berat_Kotor, (d.Berat_Kotor*sum(a.Jumlah)) "
            SQL = SQL & "as tot_brt_ktr, d.Nilai_HPP_Barang_Per_pcs, a.Jenis, sum(a.jumlah) as Jumlah, "
            SQL = SQL & "(sum(a.Jumlah)/d.isi_satuan_besar) as jml_sat_besar, sum(a.Total_Bayar) as Total_Bayar, sum(a.Total_Bayar_dry_wet) as Total_Bayar_dry_wet from Hpp_Temp d, cte_data a "
            SQL = SQL & "where a.Kode_Stock_Owner = d.Kode_Stock_Owner And a.Kode_Barang = d.Kode_Barang and d.Kode_Unik = '" & Kode_Unik & "' "
            SQL = SQL & "group by a.Kode_stock_Owner, a.Kode_Barang, a.nama, a.Kode_Gudang, a.lokasi_tujuan, "
            SQL = SQL & "d.Harga, d.Isi_Satuan_Besar, d.Panjang, d.Lebar, d.Tinggi, d.Berat_Bersih, d.Berat_Kotor, "
            SQL = SQL & "d.Nilai_HPP_Barang_Per_pcs, a.Jenis order by a.nama "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For index As Integer = 0 To .Rows.Count - 1

                        DataGridView2.Rows.Add(1)
                        DataGridView2.Rows.Item(index).Cells(cell2Lokasi).Value = .Rows(index).Item("Kode_Stock_Owner")
                        DataGridView2.Rows.Item(index).Cells(cell2LokasiTujuan).Value = .Rows(index).Item("Kode_Gudang")
                        DataGridView2.Rows.Item(index).Cells(cell2KdBarang).Value = .Rows(index).Item("kode_barang")
                        DataGridView2.Rows.Item(index).Cells(cell2Nama).Value = .Rows(index).Item("Nama")
                        DataGridView2.Rows.Item(index).Cells(cell2Jumlah).Value = .Rows(index).Item("Jumlah")
                        DataGridView2.Rows.Item(index).Cells(cell2Harga).Value = Format(.Rows(index).Item("Harga"), "N2")
                        DataGridView2.Rows.Item(index).Cells(cell2TotalHarga).Value = Format(.Rows(index).Item("Total"), "N2")
                        DataGridView2.Rows.Item(index).Cells(cell2Volume).Value = Format(.Rows(index).Item("volume"), "N0")
                        DataGridView2.Rows.Item(index).Cells(cell2JmlBsr).Value = .Rows(index).Item("jml_sat_besar")
                        DataGridView2.Rows.Item(index).Cells(cell2IsiBsr).Value = .Rows(index).Item("isi_satuan_besar")
                        DataGridView2.Rows.Item(index).Cells(cell2BeratBrsh).Value = Format(.Rows(index).Item("Berat_Bersih"), "N0")
                        DataGridView2.Rows.Item(index).Cells(cell2BeratKtr).Value = Format(.Rows(index).Item("berat_kotor"), "N0")
                        DataGridView2.Rows.Item(index).Cells(cell2TotBeratBrsh).Value = Format(.Rows(index).Item("tot_brt_brsh"), "N0")
                        DataGridView2.Rows.Item(index).Cells(cell2TotBeratKtr).Value = Format(.Rows(index).Item("tot_brt_ktr"), "N0")
                        DataGridView2.Rows.Item(index).Cells(cell2Pjg).Value = .Rows(index).Item("panjang")
                        DataGridView2.Rows.Item(index).Cells(cell2Lbr).Value = .Rows(index).Item("lebar")
                        DataGridView2.Rows.Item(index).Cells(cell2Tinggi).Value = .Rows(index).Item("tinggi")
                        DataGridView2.Rows.Item(index).Cells(cell2HPP_Per_Pcs_ktr).Value = Format(.Rows(index).Item("Nilai_HPP_Barang_Per_pcs"), "N0")
                        DataGridView2.Rows.Item(index).Cells(cell2BiayaImport).Value = Format(.Rows(index).Item("Total_Bayar"), "N0")
                        DataGridView2.Rows.Item(index).Cells(cell2BiayaImportWetDry).Value = Format(.Rows(index).Item("Total_Bayar_dry_wet"), "N0")
                        DataGridView2.Rows.Item(index).Cells(cell2Input).Value = 0
                        DataGridView2.Rows.Item(index).Cells(cell2HPP_Per_Pcs_brsh).Value = Format(.Rows(index).Item("Nilai_HPP_Barang_Per_pcs") + ((.Rows(index).Item("Total_Bayar") + .Rows(index).Item("Total_Bayar_dry_wet")) / .Rows(index).Item("Jumlah")), "N0")
                        DataGridView2.Rows.Item(index).Cells(cell2lkstujuanInduk).Value = .Rows(index).Item("lokasi_tujuan")
                    Next
                End With
            End Using


            '-------------------------------------------------------------------------------------------------------------
            '-------------------------------------------------------------------------------------------------------------
            '-------------------------------------------------------------------------------------------------------------
            '-------------------------------------------------------------------------------------------------------------
            '-------------------------------------------------------------------------------------------------------------

            Dim arr_kategori As New ArrayList
            Dim arr_jenis_kategori As New ArrayList
            arr_kategori.Clear()
            arr_jenis_kategori.Clear()
            SQL = "select Kode_Kategori_Biaya_Import,1 as jenis from Transaksi_Biaya_Import a, Detail_Transaksi_Biaya_Import b "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and "
            SQL = SQL & "a.No_faktur = b.No_Faktur And a.status Is null "
            SQL = SQL & "and a.id_rencana='" & TxtId_Rencana.Text & "' "
            SQL = SQL & "group by Kode_Kategori_Biaya_Import "
            SQL = SQL & "union "
            SQL = SQL & "select Kode_Kategori_Biaya_Import,3 as jenis from Transaksi_Biaya_Import3 a, Detail_Transaksi_Biaya_Import3 b "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and "
            SQL = SQL & "a.No_faktur = b.No_Faktur And a.status Is null "
            SQL = SQL & "and a.id_rencana='" & TxtId_Rencana.Text & "' "
            SQL = SQL & "group by Kode_Kategori_Biaya_Import "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    arr_kategori.Add(dr("Kode_Kategori_Biaya_Import"))
                    arr_jenis_kategori.Add(dr("jenis"))
                Loop
            End Using


            DataGridViewavg1.Rows.Clear()

            DataGridViewavg2.Rows.Clear()
            Dim ind1 As Integer = 0
            Dim ind2 As Integer = 0

            'cekkkk.Text = ""
            'Dim abc As Double = 0
            For index As Integer = 0 To arr_kategori.Count - 1
                SQL = ";with "
                SQL = SQL & "cte_persen_konte as( "
                SQL = SQL & "select a.Kode_Perusahaan, b.ID_Rencana, a.No_Faktur, a.Jenis, "
                SQL = SQL & "sum(Persentase)/isnull((select count( distinct X.No_Container) from Kontainer_Masuk_Per_Jenis X "
                SQL = SQL & "where X.Kode_perusahaan = a.Kode_Perusahaan and X.No_Faktur = A.No_Faktur ),0) as Persen_tot_Konte "
                SQL = SQL & "from Kontainer_Masuk_Per_Jenis a, Submit_PO b where a.Kode_Perusahaan = b.Kode_Perusahaan and "
                SQL = SQL & "a.No_Faktur = b.no_faktur and b.id_rencana in(" & id_rencana_group & ") and b.status is null "
                SQL = SQL & "group by a.Kode_Perusahaan, b.ID_Rencana, a . No_Faktur, a.Jenis  "
                SQL = SQL & ") "


                SQL = SQL & ",cte_konte_per_lokasi as( "
                SQL = SQL & "select a.KOde_Perusahaan, a.Id_Rencana,Count (distinct B.No_container) as Konte_Per_Rencana, "
                SQL = SQL & "Isnull((select Count (distinct Y.No_container) from submit_PO X, Kontainer_Masuk Y "
                SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur "
                SQL = SQL & "and x. Id_Rencana in (" & id_rencana_group & ") and X.status is null ),0) as Total_Konte from submit_PO A, Kontainer_Masuk B  where "
                SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur "
                SQL = SQL & "and A. Id_Rencana in (" & id_rencana_group & ") and a.status is null "
                SQL = SQL & "group by a.Kode_Perusahaan, a.Id_Rencana "
                SQL = SQL & ")"



                SQL = SQL & ", cte_nilai_biayaAVG as( "
                SQL = SQL & "select a.kode_perusahaan, a.No_Faktur, a.id_rencana, sum(b.Total_AVG_Biaya) as grand_total "
                SQL = SQL & "from Transaksi_Biaya_Import a, aVg_Kategori_Biaya_Import b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_faktur = b.No_Faktur "
                SQL = SQL & "and a.status is null and a.Id_Rencana = '" & TxtId_Rencana.Text & "' and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & " and b.Kode_Kategori_Biaya_Import ='" & arr_kategori.Item(index) & "'"
                SQL = SQL & "group by a.kode_perusahaan, a.No_Faktur, a.id_rencana "
                SQL = SQL & ") "



                SQL = SQL & ", cte_nilai_biaya as( "
                SQL = SQL & "select a.kode_perusahaan, a.No_Faktur, a.id_rencana, sum(b.total_avg_biaya) as grand_total "
                SQL = SQL & "from transaksi_biaya_import a, detail_transaksi_biaya_import b  where "
                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And a.no_faktur = b.no_faktur And a.status Is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & TxtId_Rencana.Text & "' and b.Jenis_Perhitungan <> 'G' "
                SQL = SQL & " and b.Kode_Kategori_Biaya_Import ='" & arr_kategori.Item(index) & "'"
                SQL = SQL & "group by a.kode_perusahaan, a.No_Faktur, a.id_rencana "
                SQL = SQL & ") "

                SQL = SQL & ",cte_sum_detail_biayaAVG as ( "
                SQL = SQL & "select a.Kode_Perusahaan, a.Id_Rencana, b.No_Faktur, ((grand_total*Konte_Per_Rencana)/Total_Konte) as grand_Total from cte_konte_per_lokasi a, cte_nilai_biayaAVG b "
                SQL = SQL & "where a.Kode_Perusahaan = b.KOde_Perusahaan "
                SQL = SQL & ") "

                SQL = SQL & ",cte_sum_detail_biaya as ( "
                SQL = SQL & "select a.Kode_Perusahaan, a.Id_Rencana, b.No_Faktur, ((grand_total*Konte_Per_Rencana)/Total_Konte) as grand_Total from cte_konte_per_lokasi a, cte_nilai_biaya b "
                SQL = SQL & "where a.Kode_Perusahaan = b.KOde_Perusahaan "
                SQL = SQL & ") "


                SQL = SQL & ",cte_biayaAVG as ( "
                SQL = SQL & "select b.id_rencana,a.Grand_total, e.Jenis, d.Persen_tot_Konte, sum(g.Jumlah* g.Berat_Bersih) "
                SQL = SQL & "as Tot_berat_bersih, (((a.Grand_total*d.Persen_tot_Konte)/100)/(sum(g.Jumlah* g.Berat_Bersih))) "
                SQL = SQL & "as nilai_per_berat from cte_sum_detail_biayaAVG a, Loading_Barang B, Detail_Loading_Barang G, Barang c, "
                SQL = SQL & "cte_Persen_konte d, Kategori_Besar e where a.Kode_Perusahaan = b.Kode_Perusahaan And "
                SQL = SQL & "a.id_rencana = b.Id_rencana And g.kode_perusahaan = c.Kode_Perusahaan And g.Kode_stock_owner "
                SQL = SQL & "= c.Kode_Stock_Owner And g.Kode_barang = c.Kode_Barang and c.Kode_Perusahaan = "
                SQL = SQL & "e.Kode_Perusahaan and c.Kode_kategori_Besar = e.Kode_Kategori_Besar and b.Kode_Perusahaan "
                SQL = SQL & "= d.Kode_Perusahaan And b.Id_rencana = d.ID_Rencana And e.Jenis = d.Jenis and b.Kode_Perusahaan "
                SQL = SQL & "= G.Kode_Perusahaan and b.No_Faktur= g.No_Faktur and b.id_rencana in(" & id_rencana_group & ") and b.Status is null "
                SQL = SQL & "group by b.id_rencana, a.no_faktur,a.Grand_total, e.Jenis, d.Persen_tot_Konte "
                SQL = SQL & ") "

                SQL = SQL & ",cte_biaya as ( "
                SQL = SQL & "select b.id_rencana,a.Grand_total, e.Jenis, d.Persen_tot_Konte, sum(g.Jumlah* g.Berat_Bersih) "
                SQL = SQL & "as Tot_berat_bersih, (((a.Grand_total*d.Persen_tot_Konte)/100)/(sum(g.Jumlah* g.Berat_Bersih))) "
                SQL = SQL & "as nilai_per_berat from cte_sum_detail_biaya a, Loading_Barang B, Detail_Loading_Barang G, Barang c, "
                SQL = SQL & "cte_Persen_konte d, Kategori_Besar e where a.Kode_Perusahaan = b.Kode_Perusahaan And "
                SQL = SQL & "a.id_rencana = b.Id_rencana And g.kode_perusahaan = c.Kode_Perusahaan And g.Kode_stock_owner "
                SQL = SQL & "= c.Kode_Stock_Owner And g.Kode_barang = c.Kode_Barang and c.Kode_Perusahaan = "
                SQL = SQL & "e.Kode_Perusahaan and c.Kode_kategori_Besar = e.Kode_Kategori_Besar and b.Kode_Perusahaan "
                SQL = SQL & "= d.Kode_Perusahaan And b.Id_rencana = d.ID_Rencana And e.Jenis = d.Jenis and b.Kode_Perusahaan "
                SQL = SQL & "= G.Kode_Perusahaan and b.No_Faktur= g.No_Faktur and b.id_rencana in(" & id_rencana_group & ") and b.Status is null "
                SQL = SQL & "group by b.id_rencana, a.no_faktur,a.Grand_total, e.Jenis, d.Persen_tot_Konte "
                SQL = SQL & ") "



                SQL = SQL & ",cte_Kurs_freightAVG as( "
                SQL = SQL & "select a.Kode_Perusahaan, c.id_rencana, a.no_faktur, "
                SQL = SQL & "(isnull((select sum(total_avg_biaya) from aVg_Kategori_Biaya_Import3 X where a.Kode_Perusahaan = X.Kode_Perusahaan "
                SQL = SQL & "and a.no_faktur = X.no_faktur and x.Kode_Kategori_Biaya_Import ='" & arr_kategori.Item(index) & "'),0)*d.Konte_Per_Rencana)/d.Total_Konte as Grand_total_hpp "

                SQL = SQL & ", isnull((select case "
                Dim CekMataUang99 As String = ""

                If ListView1.Items.Count <> 0 Then
                    Dim data2 As Boolean = False
                    For indexxx As Integer = 0 To ListView1.Items.Count - 1
                        If ListView1.Items(indexxx).SubItems(1).Text = "FREIGHT" Then
                            data2 = True
                            CekMataUang99 = CekMataUang99 & " when (isnull((select Mata_Uang_HPP as mata_uang from detail_transaksi_biaya_import3 X where a.Kode_Perusahaan = X.Kode_Perusahaan and a.no_faktur = X.no_faktur group by mata_uang_hpp),0)) = '" & ListView1.Items(indexxx).SubItems(0).Text & "' then " & ListView1.Items(indexxx).SubItems(2).Text & " "
                        End If
                    Next

                    If data2 = False Then
                        MessageBox.Show("Kurs untuk Freight tidak ada !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        CloseTrans()
                        CloseConn()
                        Exit Sub
                    End If

                Else
                    CekMataUang99 = " when (isnull((select Mata_Uang_HPP as mata_uang from detail_transaksi_biaya_import3 X where a.Kode_Perusahaan = X.Kode_Perusahaan and a.no_faktur = X.no_faktur group by mata_uang_hpp),0)) = '' then 000000000 "

                End If
                SQL = SQL & CekMataUang99
                SQL = SQL & "else -1 end),0) as kurs_freight from "
                SQL = SQL & "Transaksi_Biaya_import3 a, Detail_Loading_Barang b, Loading_Barang c, cte_konte_per_lokasi D "
                SQL = SQL & "where a.Kode_Perusahaan = c.Kode_Perusahaan and a.id_rencana = '" & TxtId_Rencana.Text & "' And b.kode_perusahaan "
                SQL = SQL & "= c.Kode_Perusahaan And  b.No_Faktur = c.No_Faktur and c.id_rencana in(" & id_rencana_group & ") "
                SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Rencana = d.Id_Rencana and a.status is null and c.status is null "
                SQL = SQL & "group by a.Kode_Perusahaan, a.Grand_total_hpp, c.id_rencana, a.no_faktur, d.Konte_Per_Rencana, d.Total_Konte "
                SQL = SQL & ") "

                SQL = SQL & ",cte_Kurs_freight as( "
                SQL = SQL & "select a.Kode_Perusahaan, c.id_rencana, a.no_faktur, "
                SQL = SQL & "(isnull((select sum(total_avg_biaya) from detail_transaksi_biaya_import3 X where a.Kode_Perusahaan = X.Kode_Perusahaan "
                SQL = SQL & "and a.no_faktur = X.no_faktur and x.Kode_Kategori_Biaya_Import ='" & arr_kategori.Item(index) & "'),0)*d.Konte_Per_Rencana)/d.Total_Konte as Grand_total_hpp "

                SQL = SQL & ", isnull((select case "
                Dim CekMataUang98 As String = ""

                If ListView1.Items.Count <> 0 Then
                    Dim data2 As Boolean = False
                    For indexxx As Integer = 0 To ListView1.Items.Count - 1
                        If ListView1.Items(indexxx).SubItems(1).Text = "FREIGHT" Then
                            data2 = True
                            CekMataUang98 = CekMataUang98 & " when (isnull((select Mata_Uang_HPP as mata_uang from detail_transaksi_biaya_import3 X where a.Kode_Perusahaan = X.Kode_Perusahaan and a.no_faktur = X.no_faktur group by mata_uang_hpp),0)) = '" & ListView1.Items(indexxx).SubItems(0).Text & "' then " & ListView1.Items(indexxx).SubItems(2).Text & " "
                        End If
                    Next

                    If data2 = False Then
                        MessageBox.Show("Kurs untuk Freight tidak ada !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        CloseTrans()
                        CloseConn()
                        Exit Sub
                    End If

                Else
                    CekMataUang98 = " when (isnull((select Mata_Uang_HPP as mata_uang from detail_transaksi_biaya_import3 X where a.Kode_Perusahaan = X.Kode_Perusahaan and a.no_faktur = X.no_faktur group by mata_uang_hpp),0)) = '' then 000000000 "

                End If
                SQL = SQL & CekMataUang98
                SQL = SQL & "else -1 end),0) as kurs_freight from "
                SQL = SQL & "Transaksi_Biaya_import3 a, Detail_Loading_Barang b, Loading_Barang c, cte_konte_per_lokasi D "
                SQL = SQL & "where a.Kode_Perusahaan = c.Kode_Perusahaan and a.id_rencana = '" & TxtId_Rencana.Text & "' And b.kode_perusahaan "
                SQL = SQL & "= c.Kode_Perusahaan And  b.No_Faktur = c.No_Faktur and c.id_rencana in(" & id_rencana_group & ") "
                SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Rencana = d.Id_Rencana and a.status is null and c.status is null "
                SQL = SQL & "group by a.Kode_Perusahaan, a.Grand_total_hpp, c.id_rencana, a.no_faktur, d.Konte_Per_Rencana, d.Total_Konte "
                SQL = SQL & ") "

                SQL = SQL & ",cte_freightAVG as( "
                SQL = SQL & "select c.id_rencana, (g.Grand_total_hpp * g.kurs_freight) as grand_total, e.Jenis, "
                SQL = SQL & "d.Persen_tot_Konte, sum(Total_berat_bersih) as Tot_Berat_Bersih, ((((g.Grand_total_hpp * "
                SQL = SQL & "g.kurs_freight)*d.Persen_tot_Konte)/100)/(sum(Total_berat_bersih))) as nilai_per_berat "
                SQL = SQL & "from Detail_Loading_Barang b, Loading_Barang c, Barang f, cte_Persen_konte d, Kategori_Besar e, "
                SQL = SQL & "cte_Kurs_freightAVG g  where b.kode_perusahaan = c.Kode_Perusahaan And b.No_Faktur = c.No_Faktur And "
                SQL = SQL & "b.kode_perusahaan = f.Kode_Perusahaan And b.Kode_stock_owner = f.Kode_Stock_Owner And b.Kode_barang "
                SQL = SQL & "= f.Kode_Barang and f.Kode_Perusahaan = e.Kode_Perusahaan and f.Kode_kategori_Besar = "
                SQL = SQL & "e.Kode_Kategori_Besar and  c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_rencana = d.ID_Rencana "
                SQL = SQL & "and e.Jenis = d.Jenis and c.id_rencana in(" & id_rencana_group & ") and c.Kode_Perusahaan = g.Kode_Perusahaan "
                SQL = SQL & "and c.id_rencana = g.id_rencana and c.status is null "
                SQL = SQL & "group by b.Kode_Perusahaan, c.id_rencana, g.no_faktur, b.No_Faktur, g.Grand_total_hpp, "
                SQL = SQL & "g.kurs_freight, e.Jenis, d.Persen_tot_Konte "
                SQL = SQL & ") "

                SQL = SQL & ",cte_freight as( "
                SQL = SQL & "select c.id_rencana, (g.Grand_total_hpp * g.kurs_freight) as grand_total, e.Jenis, "
                SQL = SQL & "d.Persen_tot_Konte, sum(Total_berat_bersih) as Tot_Berat_Bersih, ((((g.Grand_total_hpp * "
                SQL = SQL & "g.kurs_freight)*d.Persen_tot_Konte)/100)/(sum(Total_berat_bersih))) as nilai_per_berat "
                SQL = SQL & "from Detail_Loading_Barang b, Loading_Barang c, Barang f, cte_Persen_konte d, Kategori_Besar e, "
                SQL = SQL & "cte_Kurs_freight g  where b.kode_perusahaan = c.Kode_Perusahaan And b.No_Faktur = c.No_Faktur And "
                SQL = SQL & "b.kode_perusahaan = f.Kode_Perusahaan And b.Kode_stock_owner = f.Kode_Stock_Owner And b.Kode_barang "
                SQL = SQL & "= f.Kode_Barang and f.Kode_Perusahaan = e.Kode_Perusahaan and f.Kode_kategori_Besar = "
                SQL = SQL & "e.Kode_Kategori_Besar and  c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_rencana = d.ID_Rencana "
                SQL = SQL & "and e.Jenis = d.Jenis and c.id_rencana in(" & id_rencana_group & ") and c.Kode_Perusahaan = g.Kode_Perusahaan "
                SQL = SQL & "and c.id_rencana = g.id_rencana and c.status is null "
                SQL = SQL & "group by b.Kode_Perusahaan, c.id_rencana, g.no_faktur, b.No_Faktur, g.Grand_total_hpp, "
                SQL = SQL & "g.kurs_freight, e.Jenis, d.Persen_tot_Konte "
                SQL = SQL & ") "

                SQL = SQL & ",cte_data as("
                SQL = SQL & "select a.Kode_Perusahaan, cast(c.rv as int) as rvx, a.Kode_Stock_Owner, b.kode_barang, "
                SQL = SQL & "b.Nama, e.Jumlah, volume,  Jml_Satuan_Besar as "
                SQL = SQL & "tot_sat_bsr, e.isi_satuan_besar, e.Berat_bersih as berat, e.berat_kotor, Total_Berat_Bersih as tot_berat_brsh, "
                SQL = SQL & "Total_Berat_Kotor as tot_berat_kotor, e.panjang, e.lebar, e.tinggi, a.no_urut, b.mata_uang, e.Harga_Declare, e.Total "


                SQL = SQL & ",isnull((select X.nilai_Per_berat*e.berat_bersih*a.Jumlah_PO from cte_biaya X where X.Id_Rencana= a.Id_rencana "
                SQL = SQL & "and X.Jenis = d.Jenis),0) as Biaya_import "

                SQL = SQL & ",isnull((select X.nilai_Per_berat*e.berat_bersih*a.Jumlah_PO from cte_biayaAVG X where X.Id_Rencana= a.Id_rencana "
                SQL = SQL & "and X.Jenis = d.Jenis),0) as Biaya_importAVG "
                SQL = SQL & ",isnull((select X.nilai_per_berat*e.berat_bersih*a.Jumlah_PO from cte_freight X where X.Id_Rencana= a.Id_rencana "
                SQL = SQL & "and X.Jenis = d.Jenis),0) as Biaya_freight_int "
                SQL = SQL & ",isnull((select X.nilai_per_berat*e.berat_bersih*a.Jumlah_PO from cte_freightAVG X where X.Id_Rencana= a.Id_rencana "
                SQL = SQL & "and X.Jenis = d.Jenis),0) as Biaya_freight_intAVG "

                SQL = SQL & "from detail_rencana_order a, barang b, "
                SQL = SQL & "rencana_order c, Kategori_Besar d, Detail_Loading_Barang e, Loading_Barang f where a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = "
                SQL = SQL & "c.kode_perusahaan and a.kode_stock_owner = b.kode_stock_owner and a.kode_barang = b.kode_Barang and "
                SQL = SQL & "a.id_rencana = c.id_rencana and a.Jumlah_PO <> 0 and b.Kode_Perusahaan = d.Kode_Perusahaan and "
                SQL = SQL & "b.Kode_Kategori_Besar = d.Kode_Kategori_Besar and "
                SQL = SQL & "f.Kode_Perusahaan = c.Kode_Perusahaan and f.Id_rencana = c.Id_Rencana and "
                SQL = SQL & "e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Faktur= f.No_Faktur and "
                SQL = SQL & "a.Kode_Perusahaan = e.Kode_Perusahaan And a.Kode_Stock_Owner = e.Kode_Stock_Owner And a.Kode_Barang = e.Kode_Barang "
                SQL = SQL & "and a.id_rencana in(" & id_rencana_group & ") and f.status is null and c.status is null "
                SQL = SQL & ")"
                'SQL = SQL & "and f.No_Faktur = 'LB-DS-12/22-0008'" 'Hapus'
                SQL = SQL & "select Kode_Perusahaan, Kode_Barang, Nama, sum(jumlah) as jumlah, sum(volume) as volume, sum(tot_sat_bsr) as tot_sat_bsr "
                SQL = SQL & ", isi_satuan_besar,berat, berat_kotor, sum(tot_berat_brsh) as tot_berat_brsh, "
                SQL = SQL & "sum(tot_berat_kotor) as tot_berat_kotor, panjang, lebar, tinggi, mata_uang, Harga_Declare, isnull((select Kurs from "
                SQL = SQL & "Total_Billing X, Detail_Total_Billing Y where X.Kode_Perusahaan = Y.Kode_Perusahaan And X.No_Faktur = Y.No_Faktur "
                SQL = SQL & "and X.Id_Rencana = '" & TxtId_Rencana.Text & "' and Y.Kode_Perusahaan = A.Kode_Perusahaan and Y.Kode_Barang = A.Kode_Barang "
                SQL = SQL & "and Y.Kode_Stock_Owner ='" & Lokasi_gudang & "' and X.status is null),0) as kurs, sum(Total) as Total, "
                SQL = SQL & "sum(Biaya_import) as Biaya_Import, sum(Biaya_importAVG) as Biaya_ImportAVG, "
                SQL = SQL & "sum(Biaya_Freight_int) as Biaya_Freight_int, sum(Biaya_Freight_intAVG) as Biaya_Freight_intAVG "
                SQL = SQL & "from cte_data A "
                SQL = SQL & "group by Kode_Perusahaan, Kode_Barang, Nama, isi_satuan_besar,berat, berat_kotor, panjang, lebar, tinggi, "
                SQL = SQL & "mata_uang, Harga_Declare "
                SQL = SQL & "order by nama "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        For index3 As Integer = 0 To .Rows.Count - 1
                            ' TextBoxRV.Text = .Rows(0).Item("rvx")

                            'If General_Class.CekNULL(.Rows(index).Item("Harga_Declare")) = "" Then
                            '    CloseConn()
                            '    MessageBox.Show(" Ada data yang masih kosong di Variabel Barang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            '    DataGridView1.Rows.Clear()
                            '    Exit Sub
                            'End If

                            'If .Rows(index).Item("mata_uang") <> Mata_Uang_Declare Then
                            '    CloseConn()
                            '    MessageBox.Show("Mata Uang Declare Supplier Berbeda dengan barang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    DataGridView1.Rows.Clear()
                            '    Exit Sub
                            'End If

                            'If .Rows(0).Item("mata_uang") <> .Rows(index).Item("mata_uang") Then
                            '    CloseConn()
                            '    MessageBox.Show("Ada Mata Uang yang Berbeda pada Barang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    DataGridView1.Rows.Clear()
                            '    Exit Sub
                            'End If

                            Dim nilai_PPH As Double = 0
                            DataGridViewavg1.Rows.Add(1)
                            DataGridViewavg1.Rows.Item(ind1).Cells(0).Value = Lokasi_gudang
                            DataGridViewavg1.Rows.Item(ind1).Cells(1).Value = .Rows(index3).Item("kode_barang")
                            DataGridViewavg1.Rows.Item(ind1).Cells(2).Value = arr_kategori.Item(index)

                            If arr_jenis_kategori.Item(index) = "1" Then
                                DataGridViewavg1.Rows.Item(ind1).Cells(3).Value = Format(.Rows(index3).Item("Biaya_Import"), "N2")

                                If Flag_Average_Sup = "Y" Then
                                    DataGridViewavg1.Rows.Item(ind1).Cells(4).Value = Format(.Rows(index3).Item("Biaya_ImportAVG"), "N2")

                                Else
                                    DataGridViewavg1.Rows.Item(ind1).Cells(4).Value = Format(.Rows(index3).Item("Biaya_Import"), "N2")

                                End If


                            Else

                                DataGridViewavg1.Rows.Item(ind1).Cells(3).Value = Format(.Rows(index3).Item("Biaya_freight_int"), "N2")

                                If Flag_Average_Sup = "Y" Then
                                    DataGridViewavg1.Rows.Item(ind1).Cells(4).Value = Format(.Rows(index3).Item("Biaya_freight_intAVG"), "N2")
                                Else
                                    DataGridViewavg1.Rows.Item(ind1).Cells(4).Value = Format(.Rows(index3).Item("Biaya_freight_int"), "N2")
                                End If

                            End If

                            ind1 += 1
                        Next
                    End With
                End Using






                SQL = ";with cte_persen_konte as( "
                SQL = SQL & "select C.Kode_Perusahaan, b.ID_Rencana, C.No_Faktur, a.Jenis, c.Kode_Gudang, "
                SQL = SQL & "sum(Persentase)/isnull((select count(Y.No_Container) from Kontainer_Masuk_Per_Lokasi Y "
                SQL = SQL & "where Y.Kode_Perusahaan = C.Kode_Perusahaan and Y.No_Faktur = C.No_Faktur and Y.Kode_Gudang "
                SQL = SQL & "= C.Kode_Gudang ),0) as Persen_tot_Konte from Kontainer_Masuk_Per_Jenis a, Submit_PO b "
                SQL = SQL & ", Kontainer_Masuk_Per_Lokasi c where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur "
                SQL = SQL & "= b.no_faktur and a.Kode_Perusahaan = c.Kode_Perusahaan And a.No_Container = c.No_Container "
                SQL = SQL & "And a.no_faktur = c.no_faktur and b.id_rencana in (" & id_rencana_group & ") and b.status is null group by "
                SQL = SQL & "c.Kode_Perusahaan, b.ID_Rencana, C.No_Faktur, a.Jenis, c.Kode_Gudang "
                SQL = SQL & ")"

                SQL = SQL & ",cte_persen_konteWET as( "
                SQL = SQL & "select C.Kode_Perusahaan, b.ID_Rencana, C.No_Faktur, a.Jenis, c.Kode_Gudang, "
                SQL = SQL & "sum(Persentase) as Persen_tot_Konte,isnull((select sum(z.Persentase) from Kontainer_Masuk_Per_Lokasi Y, "
                SQL = SQL & "Kontainer_Masuk_Per_Jenis Z, Rencana_order XX, Submit_po YY where Y.Kode_Perusahaan=z.Kode_perusahaan and Y.no_faktur=z.No_faktur "
                SQL = SQL & "and Y.No_Container=Z.No_COntainer and Y.Kode_Perusahaan= C.Kode_Perusahaan and z.Jenis=a.Jenis "
                SQL = SQL & "and Y.Kode_Gudang = C.Kode_Gudang and xx.ID_Rencana=yy.Id_Rencana and xx.Kode_Perusahaan=yy.Kode_Perusahaan "
                SQL = SQL & "and yy.Status is null and yy.No_Faktur=y.No_Faktur and yy.Kode_Perusahaan=y.Kode_Perusahaan and xx.ID_Rencana IN(" & id_rencana_group & ")),0) as Total_PersenWETDRY from Kontainer_Masuk_Per_Jenis a, "
                SQL = SQL & "Submit_PO b , Kontainer_Masuk_Per_Lokasi c where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.no_faktur "
                SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan And a.No_Container = c.No_Container And a.no_faktur = c.no_faktur and "
                SQL = SQL & "b.id_rencana in (" & id_rencana_group & ") and b.status is null "
                SQL = SQL & "group by c.Kode_Perusahaan, b.ID_Rencana, C.No_Faktur, a.Jenis, c.Kode_Gudang "
                SQL = SQL & ")"

                SQL = SQL & ",cte_konte_per_lokasi as( "
                SQL = SQL & "select a.KOde_Perusahaan, a.Id_Rencana, c.Kode_Gudang, Count (distinct B.No_container) as Konte_Per_Rencana, "
                SQL = SQL & "Isnull((select Count (distinct Y.No_container) from submit_PO X, Kontainer_Masuk Y, Kontainer_masuk_per_lokasi Z "
                SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur and "
                SQL = SQL & "y.Kode_Perusahaan = z.kode_Perusahaan And y.no_faktur = z.No_Faktur And y.No_Container = z.No_Container "
                SQL = SQL & "and x. Id_Rencana in (" & id_rencana_group & ") and X.status is null and z.Kode_Gudang =c.Kode_Gudang ),0) as Total_Konte from submit_PO A, Kontainer_Masuk B, Kontainer_masuk_per_lokasi c "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur and "
                SQL = SQL & "b.Kode_Perusahaan = c.kode_Perusahaan And b.no_faktur = c.No_Faktur And b.No_Container = c.No_Container "
                SQL = SQL & "and A.Status is null and A. Id_Rencana in "
                SQL = SQL & "(" & id_rencana_group & ") group by a.Kode_Perusahaan, a.Id_Rencana, c.Kode_Gudang "
                SQL = SQL & ")"


                SQL = SQL & ",cte_sum_detail_biaya as ( "
                SQL = SQL & "select a.kode_perusahaan, a.No_Faktur, a.id_rencana, b.Kode_stock_Owner, sum(b.total_avg_biaya)"
                SQL = SQL & "as grand_total from "
                SQL = SQL & "transaksi_biaya_import a, detail_transaksi_biaya_import b where a.kode_perusahaan = "
                SQL = SQL & "b.kode_perusahaan and a.no_faktur = b.no_faktur and a.status is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Id_Rencana = '" & TxtId_Rencana.Text & "' and b.Jenis_Perhitungan = 'G' and b.Jns = 'all' "
                SQL = SQL & "and b.Kode_Kategori_Biaya_Import ='" & arr_kategori.Item(index) & "'"
                SQL = SQL & "group by a.kode_perusahaan, a.No_Faktur, a.id_rencana, b.Kode_stock_Owner ) "

                SQL = SQL & ",cte_sum_detail_biaya_wet_dry as ( "
                SQL = SQL & "select a.kode_perusahaan, a.No_Faktur, a.id_rencana, b.Kode_stock_Owner, b.jns, "
                SQL = SQL & "sum(b.total_avg_biaya)as grand_total from transaksi_biaya_import a, detail_transaksi_biaya_import b "
                SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan And a.no_faktur = b.no_faktur And a.status Is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Id_Rencana = '" & TxtId_Rencana.Text & "' and b.Jenis_Perhitungan = 'G' and b.Jns <> 'all' "
                SQL = SQL & "and b.Kode_Kategori_Biaya_Import ='" & arr_kategori.Item(index) & "'"
                SQL = SQL & "group by a.kode_perusahaan, a.No_Faktur, a.id_rencana, b.Kode_stock_Owner, b.jns "
                SQL = SQL & ") "

                SQL = SQL & ",cte_berat as( "
                SQL = SQL & "select a.Kode_perusahaan, a.Id_Rencana, b.No_Faktur, b.Kode_stock_Owner, c.Kode_Gudang, "
                SQL = SQL & "f.jenis, sum(b.qty*G.Berat_Bersih) as Total_Berat from Submit_PO A, Kontainer_masuk B, "
                SQL = SQL & "Kontainer_Masuk_Per_Lokasi C, barang e, Kategori_Besar f, Detail_Submit_PO G where A.Kode_Perusahaan = "
                SQL = SQL & "B.Kode_Perusahaan And A.No_Faktur = B.No_Faktur And B.Kode_Perusahaan = c.Kode_Perusahaan "
                SQL = SQL & "and b.No_Faktur = c.No_Faktur and B.No_Container = C.No_Container and b.Kode_Perusahaan = "
                SQL = SQL & "e.Kode_Perusahaan And b.Kode_stock_Owner = e.Kode_stock_Owner And b.Kode_Barang = "
                SQL = SQL & "e.Kode_Barang and e.Kode_Perusahaan = f.Kode_Perusahaan and e.Kode_Kategori_Besar = "
                SQL = SQL & "f.Kode_Kategori_besar and B.Kode_Perusahaan = G.Kode_Perusahaan and B.No_Faktur= G.No_Faktur "
                SQL = SQL & "and B.Kode_Barang = G.Kode_Barang and B.Kode_Stock_Owner = G.Kode_Stock_Owner "
                SQL = SQL & "and ID_Rencana in (" & id_rencana_group & ") and a.status is null "
                SQL = SQL & "group by a.Kode_perusahaan, a.Id_Rencana, b.No_Faktur, b.Kode_stock_Owner, c.Kode_Gudang, f.jenis "
                SQL = SQL & ")"



                SQL = SQL & ",cte_nilai_per_lokasi as( "
                SQL = SQL & "select A.Kode_Perusahaan, A.Id_Rencana, A.Jenis, a.Kode_Gudang, "
                SQL = SQL & "(((A.Persen_tot_Konte*((b.grand_total*d.Konte_Per_Rencana)/d.Total_Konte))/100)/c.Total_Berat) as Nilai_Per_Berat "
                SQL = SQL & "from Cte_persen_Konte a, cte_sum_detail_biaya b, Cte_Berat C, cte_konte_per_lokasi d where a.Kode_Perusahaan = "
                SQL = SQL & "b.Kode_Perusahaan And B.Kode_Stock_Owner = A.Kode_Gudang and a.Kode_Perusahaan = "
                SQL = SQL & "c.Kode_Perusahaan And a.id_Rencana = c.Id_Rencana And  a.Kode_Gudang = C.Kode_Gudang "
                SQL = SQL & "And a.Jenis = c.Jenis and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_rencana = d.Id_rencana "
                SQL = SQL & " and c.Kode_Gudang =d.Kode_Gudang ) "

                SQL = SQL & ",cte_nilai_per_lokasi_dry_wet as( "
                SQL = SQL & "select A.Kode_Perusahaan, A.Id_Rencana, A.Jenis, a.Kode_Gudang, "
                SQL = SQL & "(((b.grand_total*a.Persen_tot_Konte)/a.Total_PersenWETDRY)/c.Total_Berat) "
                SQL = SQL & "as Nilai_Per_Berat from Cte_persen_KonteWET a, cte_sum_detail_biaya_wet_dry b, Cte_Berat C, "
                SQL = SQL & "cte_konte_per_lokasi d where a.Kode_Perusahaan = b.Kode_Perusahaan And B.Kode_Stock_Owner "
                SQL = SQL & "= A.Kode_Gudang and a.Jenis = b.jns and a.Kode_Perusahaan = c.Kode_Perusahaan And a.id_Rencana = c.Id_Rencana "
                SQL = SQL & "And  a.Kode_Gudang = C.Kode_Gudang And a.Jenis = c.Jenis and c.Kode_Perusahaan = "
                SQL = SQL & "d.Kode_Perusahaan and c.Id_rencana = d.Id_rencana and c.Kode_Gudang =d.Kode_Gudang) "


                SQL = SQL & ",cte_data as( "
                SQL = SQL & "select a.Id_Rencana, b.No_Faktur, isnull((select top(1) X.Kode_stock_Owner From detail_rencana_order X where X.id_rencana ='" & TxtId_Rencana.Text & "'),0) as Kode_stock_Owner "
                SQL = SQL & ", b.Kode_Barang, e.nama, c.Kode_Gudang, f.Jenis, sum(b.qty) as Jumlah, "


                SQL = SQL & "sum(b.qty)*G.Berat_Bersih* "
                SQL = SQL & "isnull((select X.Nilai_Per_Berat from cte_nilai_per_lokasi X where X.Kode_Gudang = "
                SQL = SQL & "c.Kode_Gudang and X.Jenis =f.Jenis and X.id_rencana = A.id_rencana ),0) as Total_Bayar, "

                SQL = SQL & "sum(b.qty)*G.Berat_Bersih* "
                SQL = SQL & "isnull((select X.Nilai_Per_Berat from cte_nilai_per_lokasi_dry_wet X where X.Kode_Gudang = "
                SQL = SQL & "c.Kode_Gudang and X.Jenis =f.Jenis and X.id_rencana = A.id_rencana ),0) as Total_Bayar_dry_wet "



                SQL = SQL & "from Submit_PO A, Kontainer_masuk B, Kontainer_Masuk_Per_Lokasi C, "
                SQL = SQL & "Barang e, Kategori_Besar f, Detail_Submit_PO G where A.Kode_Perusahaan = B.Kode_Perusahaan and A.No_Faktur = "
                SQL = SQL & "B.No_Faktur and B.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and "
                SQL = SQL & "B.No_Container = C.No_Container And b.Kode_Perusahaan = e.Kode_Perusahaan And b.Kode_stock_Owner "
                SQL = SQL & "= e.Kode_stock_Owner And b.Kode_Barang = e.Kode_Barang and e.Kode_Perusahaan = f.Kode_Perusahaan "
                SQL = SQL & "and e.Kode_Kategori_Besar = f.Kode_Kategori_Besar and B.Kode_Perusahaan = G.Kode_Perusahaan and B.No_Faktur= G.No_Faktur "
                SQL = SQL & "and B.Kode_Barang = G.Kode_Barang and B.Kode_Stock_Owner = G.Kode_Stock_Owner and ID_Rencana in(" & id_rencana_group & ") and "
                SQL = SQL & "a.status is null "
                SQL = SQL & "group by a.Id_Rencana, b.No_Faktur, b.Kode_Barang, e.nama, c.Kode_Gudang, f.Jenis, G.Berat_Bersih "
                SQL = SQL & ") "

                SQL = SQL & "select a.Kode_stock_Owner, a.Kode_Barang, a.nama, a.Kode_Gudang, "
                SQL = SQL & "d.Harga, (d.Harga*sum(a.jumlah))as total, d.Isi_Satuan_Besar, d.Panjang, d.Lebar, d.Tinggi, "
                SQL = SQL & "((d.Panjang*d.Lebar*d.Tinggi)*(sum(a.jumlah)/d.Isi_satuan_Besar))as volume, d.Berat_Bersih, "
                SQL = SQL & "(d.Berat_Bersih*sum(a.jumlah)) as tot_brt_brsh, d.Berat_Kotor, (d.Berat_Kotor*sum(a.Jumlah)) "
                SQL = SQL & "as tot_brt_ktr, d.Nilai_HPP_Barang_Per_pcs, a.Jenis, sum(a.jumlah) as Jumlah, "
                SQL = SQL & "(sum(a.Jumlah)/d.isi_satuan_besar) as jml_sat_besar, sum(a.Total_Bayar) as Total_Bayar, sum(a.Total_Bayar_dry_wet) as Total_Bayar_dry_wet  from Hpp_Temp d, cte_data a "
                SQL = SQL & "where a.Kode_Stock_Owner = d.Kode_Stock_Owner And a.Kode_Barang = d.Kode_Barang and d.Kode_Unik = '" & Kode_Unik & "' "
                SQL = SQL & "group by a.Kode_stock_Owner, a.Kode_Barang, a.nama, a.Kode_Gudang, "
                SQL = SQL & "d.Harga, d.Isi_Satuan_Besar, d.Panjang, d.Lebar, d.Tinggi, d.Berat_Bersih, d.Berat_Kotor, "
                SQL = SQL & "d.Nilai_HPP_Barang_Per_pcs, a.Jenis order by a.nama "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")

                        For index3 As Integer = 0 To .Rows.Count - 1

                            DataGridViewavg2.Rows.Add(1)

                            DataGridViewavg2.Rows.Item(ind2).Cells(0).Value = .Rows(index3).Item("Kode_Gudang")
                            DataGridViewavg2.Rows.Item(ind2).Cells(1).Value = .Rows(index3).Item("kode_barang")
                            DataGridViewavg2.Rows.Item(ind2).Cells(2).Value = arr_kategori.Item(index)



                            'If arr_kategori.Item(index) = "BONGKAR1" Then
                            '    abc = abc + Val(HilangkanTanda(Format(.Rows(index3).Item("Total_Bayar_dry_wet"), "N0")))
                            'End If


                            If Flag_Average_Sup = "Y" Then
                                DataGridViewavg2.Rows.Item(ind2).Cells(3).Value = Format(.Rows(index3).Item("Total_Bayar"), "N0")
                                DataGridViewavg2.Rows.Item(ind2).Cells(4).Value = 0
                                DataGridViewavg2.Rows.Item(ind2).Cells(5).Value = Format(.Rows(index3).Item("Total_Bayar_dry_wet"), "N0")
                                DataGridViewavg2.Rows.Item(ind2).Cells(6).Value = 0
                            Else
                                DataGridViewavg2.Rows.Item(ind2).Cells(3).Value = Format(.Rows(index3).Item("Total_Bayar"), "N0")
                                DataGridViewavg2.Rows.Item(ind2).Cells(4).Value = Format(.Rows(index3).Item("Total_Bayar"), "N0")
                                DataGridViewavg2.Rows.Item(ind2).Cells(5).Value = Format(.Rows(index3).Item("Total_Bayar_dry_wet"), "N0")
                                DataGridViewavg2.Rows.Item(ind2).Cells(6).Value = Format(.Rows(index3).Item("Total_Bayar_dry_wet"), "N0")
                            End If
                            ind2 += 1
                        Next
                    End With
                End Using
            Next
            'MessageBox.Show(abc)


            '-------------------------------------------------------------------------------------------------------------
            '-------------------------------------------------------------------------------------------------------------
            '-------------------------------------------------------------------------------------------------------------
            '-------------------------------------------------------------------------------------------------------------
            '-------------------------------------------------------------------------------------------------------------



            DataGridView3.Columns(1).Visible = True
            DataGridView3.Columns(4).Visible = True
            DataGridView3.Columns(5).Visible = True
            DataGridView3.Columns(6).Visible = True
            DataGridView3.Columns(7).Visible = True
            DataGridView3.Rows.Clear()

            SQL = ";with "
            If Metode_Hitung_Konte = "A" Then
                SQL = SQL & "cte_Kontainer as( "
                SQL = SQL & "select a.Kode_Perusahaan, a.id_rencana, a.ETA, c.Lokasi, b.kode_pelabuhan, c.free_storage, d.No_Container, "
                SQL = SQL & "d.Tgl_Tarik ,datediff(day,format(DATEADD(dd, c.free_storage, a.ETA), 'yyyy-MM-dd'), format(d.Tgl_Tarik, 'yyyy-MM-dd')) as jumlah_hari "
                SQL = SQL & "from ubah_status_otw a, "
                SQL = SQL & "kapal_tiba_import b, pelabuhan c, Tarik_Kontainer d  where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.id_rencana = b.id_rencana and b.Kode_Perusahaan = c.Kode_Perusahaan and b.kode_pelabuhan = "
                SQL = SQL & "c.Kode_Pelabuhan and a.Kode_Perusahaan = d.Kode_perusahaan and a.Id_rencana = d.id_rencana and "
                SQL = SQL & "a.id_rencana in (" & id_rencana_group & ") "
                SQL = SQL & ") "
                SQL = SQL & ",cte_total_Kontainer as ( "
                SQL = SQL & "select a.*,b.id_rencana, isnull(( "
                SQL = SQL & "select count(X.no_container) from cte_Kontainer X where jumlah_hari+1 >= dari "
                SQL = SQL & "and X.Id_rencana = d.id_rencana ), 0) as Jumlah_Kontainer, Harga*isnull(( "
                SQL = SQL & "select count(X.no_container) from cte_Kontainer X where jumlah_hari+1 >= dari "
                SQL = SQL & "and X.Id_rencana = d.id_rencana ), 0) as Biaya "
                SQL = SQL & "from storage a, Kapal_Tiba_import b, Pelabuhan c, rencana_order d where "
                SQL = SQL & "b.Kode_Pelabuhan = c.Kode_Pelabuhan and B.Kode_Perusahaan = C.Kode_Perusahaan and "
                SQL = SQL & "a.kode_stock_owner = c.Lokasi And a.kode_pelabuhan = b.Kode_Pelabuhan and "
                SQL = SQL & "b.Kode_Perusahaan = d.Kode_Perusahaan  and b.Id_Rencana = d.Id_rencana and "
                SQL = SQL & "a.Kode_Kontainer = d.Kode_Kontainer "
                SQL = SQL & "and d.id_rencana in (" & id_rencana_group & ") ) "
            ElseIf Metode_Hitung_Konte = "B" Then
                SQL = SQL & "cte_total_Kontainer as ( "
                SQL = SQL & "select a.Kode_Perusahaan, c.Lokasi as Kode_stock_Owner, a.id_rencana,e.Kode_Kontainer, a.ETA,b.kode_pelabuhan, c.free_storage, "
                SQL = SQL & "d.No_Container, d.Tgl_Tarik ,datediff(day,format(DATEADD(dd, c.free_storage, a.ETA), 'yyyy-MM-dd'), "
                SQL = SQL & "format(d.Tgl_Tarik, 'yyyy-MM-dd')) as jumlah_hari, isnull((select X.Harga from storage X "
                SQL = SQL & "where datediff(day,format(DATEADD(dd, c.free_storage, a.ETA), 'yyyy-MM-dd'), "
                SQL = SQL & "format(d.Tgl_Tarik, 'yyyy-MM-dd'))= sampai and X.Kode_Perusahaan = c.Kode_Perusahaan and "
                SQL = SQL & "X.Kode_Stock_Owner = c.Lokasi and X.Kode_Pelabuhan = c.Kode_Pelabuhan and X.Kode_Kontainer = "
                SQL = SQL & "e.Kode_Kontainer),0) as biaya from ubah_status_otw a, kapal_tiba_import b, pelabuhan c, "
                SQL = SQL & "Tarik_Kontainer d, rencana_order e  where a.Kode_Perusahaan = b.Kode_Perusahaan and a.id_rencana = "
                SQL = SQL & "b.id_rencana and b.Kode_Perusahaan = c.Kode_Perusahaan and b.kode_pelabuhan = c.Kode_Pelabuhan and "
                SQL = SQL & "a.Kode_Perusahaan = d.Kode_perusahaan and a.Id_rencana = d.id_rencana and a.kode_perusahaan = "
                SQL = SQL & "e.Kode_Perusahaan and a.Id_rencana = e.Id_rencana and a.id_rencana in (" & id_rencana_group & ") "
                SQL = SQL & ") "
            Else
                MessageBox.Show("Metode Perhitungan Konte tidak ada !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                CloseTrans()
                CloseConn()
                Exit Sub
            End If
            SQL = SQL & "select* from "
            SQL = SQL & "cte_Total_kontainer a where biaya<>0 "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For index As Integer = 0 To .Rows.Count - 1

                        If Metode_Hitung_Konte = "A" Then

                            Label12.Text = "Metode Perhitungan : A"
                            ' DataGridView3.Size = New Size(750, 176)
                            DataGridView3.Columns(1).Visible = False
                            DataGridView3.Columns(7).Visible = False

                            DataGridView3.Rows.Add(1)
                            DataGridView3.Rows.Item(index).Cells(0).Value = .Rows(index).Item("Kode_Stock_Owner")
                            DataGridView3.Rows.Item(index).Cells(1).Value = ""
                            DataGridView3.Rows.Item(index).Cells(2).Value = .Rows(index).Item("Kode_Kontainer")
                            DataGridView3.Rows.Item(index).Cells(3).Value = .Rows(index).Item("Kode_Pelabuhan")
                            DataGridView3.Rows.Item(index).Cells(4).Value = .Rows(index).Item("Dari")
                            DataGridView3.Rows.Item(index).Cells(5).Value = Format(.Rows(index).Item("Harga"), "N2")
                            DataGridView3.Rows.Item(index).Cells(6).Value = .Rows(index).Item("Jumlah_Kontainer")
                            DataGridView3.Rows.Item(index).Cells(7).Value = ""
                            DataGridView3.Rows.Item(index).Cells(8).Value = Format(.Rows(index).Item("Biaya"), "N2")



                        ElseIf Metode_Hitung_Konte = "B" Then

                            Label12.Text = "Metode Perhitungan : B"
                            'DataGridView3.Size = New Size(650, 176)
                            DataGridView3.Columns(4).Visible = False
                            DataGridView3.Columns(5).Visible = False
                            DataGridView3.Columns(6).Visible = False

                            DataGridView3.Rows.Add(1)
                            DataGridView3.Rows.Item(index).Cells(0).Value = .Rows(index).Item("Kode_Stock_Owner")
                            DataGridView3.Rows.Item(index).Cells(1).Value = .Rows(index).Item("No_Container")
                            DataGridView3.Rows.Item(index).Cells(2).Value = .Rows(index).Item("Kode_Kontainer")
                            DataGridView3.Rows.Item(index).Cells(3).Value = .Rows(index).Item("Kode_Pelabuhan")
                            DataGridView3.Rows.Item(index).Cells(4).Value = ""
                            DataGridView3.Rows.Item(index).Cells(5).Value = ""
                            DataGridView3.Rows.Item(index).Cells(6).Value = ""
                            DataGridView3.Rows.Item(index).Cells(7).Value = .Rows(index).Item("Jumlah_Hari")
                            DataGridView3.Rows.Item(index).Cells(8).Value = Format(.Rows(index).Item("Biaya"), "N2")
                        Else
                            MessageBox.Show("Metode Perhitungan Konte tidak ada !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            CloseTrans()
                            CloseConn()
                            Exit Sub

                        End If

                    Next
                End With
            End Using

            DataGridView4.Rows.Clear()
            SQL = "select a.KOde_Perusahaan, c.Kode_Gudang ,Count (distinct B.No_container) "
            SQL = SQL & "as Total_Konte, Isnull((select Count (distinct Y.No_container) from submit_PO X, "
            SQL = SQL & "Kontainer_Masuk Y, Kontainer_Masuk_Per_Lokasi Z where "
            SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur And Y.Kode_Perusahaan = z.Kode_Perusahaan "
            SQL = SQL & "and Y.No_Faktur = Z.No_Faktur and Y.No_Container =Z.No_Container and Z.Kode_Gudang = C.Kode_Gudang "
            SQL = SQL & "and x. Id_Rencana in (" & id_rencana_group & ") and x.status is null ),0) as Konte_Per_Tujuan from submit_PO A, "
            SQL = SQL & "Kontainer_Masuk B, Kontainer_Masuk_Per_Lokasi C where a.Kode_Perusahaan = b.Kode_Perusahaan And "
            SQL = SQL & "a.No_Faktur = b.No_Faktur And b.Kode_Perusahaan = c.Kode_Perusahaan And b.No_faktur = c.No_faktur "
            SQL = SQL & "and A.Id_Rencana in (" & id_rencana_group & ") and a.status is null "
            SQL = SQL & "group by a.Kode_Perusahaan, c.Kode_Gudang "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For index As Integer = 0 To .Rows.Count - 1

                        DataGridView4.Rows.Add(1)
                        DataGridView4.Rows.Item(index).Cells(0).Value = .Rows(index).Item("Kode_Gudang")
                        DataGridView4.Rows.Item(index).Cells(1).Value = .Rows(index).Item("Konte_Per_Tujuan")
                        DataGridView4.Rows.Item(index).Cells(2).Value = .Rows(index).Item("Total_Konte")

                    Next
                End With
            End Using


            SQL = ";with "
            SQL = SQL & "cte_persen_konte as( "
            SQL = SQL & "select C.Kode_Perusahaan, b.ID_Rencana, C.No_Faktur, a.Jenis, c.Kode_Gudang, "
            SQL = SQL & "sum(Persentase)/isnull((select count(Y.No_Container) from Kontainer_Masuk_Per_Lokasi Y "
            SQL = SQL & "where Y.Kode_Perusahaan = C.Kode_Perusahaan And Y.No_Faktur = C.No_Faktur "
            SQL = SQL & "and Y.Kode_Gudang = C.Kode_Gudang ),0) as Persen_tot_Konte from "
            SQL = SQL & "Kontainer_Masuk_Per_Jenis a, Submit_PO b, Kontainer_Masuk_Per_Lokasi c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.no_faktur "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan And a.No_Container = c.No_Container "
            SQL = SQL & "And a.no_faktur = c.no_faktur and b.id_rencana in (" & id_rencana_group & ") and b.status is null "
            SQL = SQL & "group by c.Kode_Perusahaan, b.ID_Rencana, C.No_Faktur, a.Jenis, c.Kode_Gudang ) "

            SQL = SQL & ",cte_konte_per_lokasi as( "
            SQL = SQL & "select a.KOde_Perusahaan, a.Id_Rencana, c.Kode_Gudang, Count (distinct B.No_container) "
            SQL = SQL & "as Konte_Per_Rencana, Isnull((select Count (distinct Y.No_container) from submit_PO X, "
            SQL = SQL & "Kontainer_Masuk Y, Kontainer_masuk_per_lokasi Z where x.Kode_Perusahaan = y.Kode_Perusahaan And "
            SQL = SQL & "x.No_Faktur = y.No_Faktur And y.Kode_Perusahaan = z.kode_Perusahaan And y.no_faktur "
            SQL = SQL & "= z.No_Faktur And y.No_Container = z.No_Container and x. Id_Rencana in (" & id_rencana_group & ") "
            SQL = SQL & "and z.Kode_Gudang =c.Kode_Gudang  and X.status is null ),0) as Total_Konte from submit_PO A, "
            SQL = SQL & "Kontainer_Masuk B, Kontainer_masuk_per_lokasi c where a.Kode_Perusahaan = "
            SQL = SQL & "b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur and b.Kode_Perusahaan = "
            SQL = SQL & "c.kode_Perusahaan And b.no_faktur = c.No_Faktur And b.No_Container = "
            SQL = SQL & "c.No_Container and A. Id_Rencana in (" & id_rencana_group & ") and a.status is null "
            SQL = SQL & "group by a.Kode_Perusahaan, a.Id_Rencana, c.Kode_Gudang ) "

            SQL = SQL & ",cte_konte as( "
            SQL = SQL & "select a.KOde_Perusahaan, a.Id_Rencana,Count (distinct B.No_container) as Konte_Per_Rencana, "
            SQL = SQL & "Isnull((select Count (distinct Y.No_container) from submit_PO X, Kontainer_Masuk Y "
            SQL = SQL & "where(x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur) "
            SQL = SQL & "and x. Id_Rencana in (" & id_rencana_group & ") and X.Status is null ),0) as Total_Konte from submit_PO A, Kontainer_Masuk B  where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and A. Id_Rencana in (" & id_rencana_group & ") and a.status is null  "
            SQL = SQL & "group by a.Kode_Perusahaan, a.Id_Rencana "
            SQL = SQL & ")"

            If Flag_Average_Sup = "Y" Then

                SQL = SQL & ",cte_nilai_biaya as( "
                SQL = SQL & "select a.kode_perusahaan, a.No_Faktur, a.id_rencana, sum(b.Total_AVG_Biaya) as grand_total "
                SQL = SQL & "from Transaksi_Biaya_Import a, aVg_Kategori_Biaya_Import b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_faktur = b.No_Faktur "
                SQL = SQL & "and a.status is null and a.Id_Rencana = '" & TxtId_Rencana.Text & "' and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "group by a.kode_perusahaan, a.No_Faktur, a.id_rencana "
                SQL = SQL & ") "

            Else

                SQL = SQL & ",cte_sum_detail_biaya as ( "
                SQL = SQL & "select a.kode_perusahaan, a.No_Faktur, a.id_rencana, b.Kode_stock_Owner, "
                SQL = SQL & "sum(b.total_avg_biaya)as grand_total from transaksi_biaya_import a, "
                SQL = SQL & "detail_transaksi_biaya_import b where a.kode_perusahaan = b.kode_perusahaan "
                SQL = SQL & "and a.no_faktur = b.no_faktur and a.status is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Id_Rencana = '" & TxtId_Rencana.Text & "' and b.Jenis_Perhitungan = 'G' and b.Jns = 'all' "
                SQL = SQL & "group by a.kode_perusahaan, a.No_Faktur, a.id_rencana, b.Kode_stock_Owner "
                SQL = SQL & ") "

                SQL = SQL & ",cte_sum_detail_biaya_wet_dry as ( "
                SQL = SQL & "select a.kode_perusahaan, a.No_Faktur, a.id_rencana, b.Kode_stock_Owner, b.jns, "
                SQL = SQL & "sum(b.total_avg_biaya)as grand_total from transaksi_biaya_import a, "
                SQL = SQL & "detail_transaksi_biaya_import b where a.kode_perusahaan = b.kode_perusahaan And "
                SQL = SQL & "a.no_faktur = b.no_faktur And a.status Is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "a.Id_Rencana = '" & TxtId_Rencana.Text & "' and b.Jenis_Perhitungan = 'G' and b.Jns <> 'all' "
                SQL = SQL & "group by a.kode_perusahaan, a.No_Faktur, a.id_rencana, b.Kode_stock_Owner, b.jns "
                SQL = SQL & ") "

                SQL = SQL & ",cte_nilai_per_lokasi as( "
                SQL = SQL & "select A.Kode_Perusahaan, A.Id_Rencana, A.Jenis, a.Kode_Gudang, "
                SQL = SQL & "(((A.Persen_tot_Konte*((b.grand_total*d.Konte_Per_Rencana)/d.Total_Konte))/100) "
                SQL = SQL & ") as Nilai_Per_Berat from Cte_persen_Konte a, cte_sum_detail_biaya b, "
                SQL = SQL & "cte_konte_per_lokasi d where a.Kode_Perusahaan = b.Kode_Perusahaan And "
                SQL = SQL & "B.Kode_Stock_Owner = A.Kode_Gudang And a.Kode_Perusahaan = d.Kode_Perusahaan "
                SQL = SQL & "and a.Id_rencana = d.Id_rencana and a.Kode_Gudang = d.Kode_Gudang) "

                SQL = SQL & ",cte_nilai_per_lokasi_dry_wet as( "
                SQL = SQL & "select A.Kode_Perusahaan, A.Id_Rencana, A.Jenis, "
                SQL = SQL & "a.Kode_Gudang, (((b.grand_total * d.Konte_Per_Rencana) / d.Total_Konte)) "
                SQL = SQL & "as Nilai_Per_Berat from Cte_persen_Konte a, cte_sum_detail_biaya_wet_dry b, "
                SQL = SQL & "cte_konte_per_lokasi d where a.Kode_Perusahaan = b.Kode_Perusahaan And B.Kode_Stock_Owner "
                SQL = SQL & "= A.Kode_Gudang and a.Jenis = b.jns and a.Kode_Perusahaan = d.Kode_Perusahaan "
                SQL = SQL & "and a.Id_rencana = d.Id_rencana and a.Kode_Gudang = d.Kode_Gudang) "

                SQL = SQL & ",cte_nilai_biaya as( "
                SQL = SQL & "select a.kode_perusahaan, a.No_Faktur, a.id_rencana, sum(b.total_avg_biaya) as grand_total "
                SQL = SQL & "from transaksi_biaya_import a, detail_transaksi_biaya_import b where a.kode_perusahaan = "
                SQL = SQL & "b.kode_perusahaan And a.no_faktur = b.no_faktur And a.status Is null and a.Kode_Perusahaan "
                SQL = SQL & "= '" & KodePerusahaan & "' and a.id_rencana = '" & TxtId_Rencana.Text & "' and b.Jenis_Perhitungan <> 'G' "
                SQL = SQL & "group by a.kode_perusahaan, a.No_Faktur, a.id_rencana ) "

            End If



            SQL = SQL & ",cte_sum_detail_biaya_import as ( "
            SQL = SQL & "select a.Kode_Perusahaan, a.Id_Rencana, b.No_Faktur,((grand_total*Konte_Per_Rencana)/Total_Konte) "
            SQL = SQL & "as grand_Total from cte_konte a, cte_nilai_biaya b where a.Kode_Perusahaan =b.KOde_Perusahaan "
            SQL = SQL & ")"

            SQL = SQL & ",cte_biaya as ( "
            SQL = SQL & "select d.id_rencana,a.Grand_total, d.Jenis, d.Persen_tot_Konte, d.Kode_Gudang, "
            SQL = SQL & "(((a.Grand_total*d.Persen_tot_Konte)/100)) as nilai_per_berat from "
            SQL = SQL & "cte_sum_detail_biaya_import a, cte_Persen_konte d where a.Kode_Perusahaan = "
            SQL = SQL & "d.Kode_Perusahaan and a.Id_Rencana = d.Id_Rencana and d.id_rencana in(" & id_rencana_group & ") "
            SQL = SQL & "group by d.id_rencana,a.Grand_total, d.Jenis, d.Kode_Gudang, d.Persen_tot_Konte "
            SQL = SQL & ") "
            SQL = SQL & "select a.*, "


            If Flag_Average_Sup = "Y" Then

                SQL = SQL & "0 as BiayaDryWet, 0 as BiayaLokasi, "

            Else

                SQL = SQL & "isnull((select X.Nilai_Per_Berat from cte_nilai_per_lokasi_dry_wet X where X.Kode_Gudang = "
                SQL = SQL & "a.Kode_Gudang and X.Jenis =a.Jenis and X.id_rencana = A.id_rencana ),0) as BiayaDryWet, "
                SQL = SQL & "isnull((select X.Nilai_Per_Berat from cte_nilai_per_lokasi X where X.Kode_Gudang = "
                SQL = SQL & "a.Kode_Gudang and X.Jenis =a.Jenis and X.id_rencana = A.id_rencana ),0) as BiayaLokasi, "
            End If






            SQL = SQL & "isnull((select X.nilai_per_berat from cte_biaya X where X.Jenis =a.Jenis "
            SQL = SQL & "and X.id_rencana = A.id_rencana and a.Kode_Gudang = X.Kode_Gudang),0) as BiayaImport "
            SQL = SQL & "from Cte_persen_Konte a "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For index As Integer = 0 To .Rows.Count - 1

                        DataGridView5.Rows.Add(1)
                        DataGridView5.Rows.Item(index).Cells(0).Value = .Rows(index).Item("Id_rencana")
                        DataGridView5.Rows.Item(index).Cells(1).Value = .Rows(index).Item("Kode_Gudang")
                        DataGridView5.Rows.Item(index).Cells(2).Value = .Rows(index).Item("Jenis")
                        DataGridView5.Rows.Item(index).Cells(3).Value = .Rows(index).Item("Persen_Tot_Konte")
                        DataGridView5.Rows.Item(index).Cells(4).Value = .Rows(index).Item("BiayaImport")
                        DataGridView5.Rows.Item(index).Cells(5).Value = .Rows(index).Item("BiayaLokasi")
                        DataGridView5.Rows.Item(index).Cells(6).Value = .Rows(index).Item("BiayaDryWet")

                    Next
                End With
            End Using
            Cmd.Transaction.Commit()
            CloseConn()

            HitungGrand()
            'HitungGrand()
            'ComboBox1.Focus()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub







    Private Sub TxtId_Rencana_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtId_Rencana.TextChanged


    End Sub

    Private Sub TxtContainer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtContainer.TextChanged

    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click

    End Sub

    'Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
    '    If e.KeyChar = Chr(13) Then ComboBox2.Focus()
    'End Sub

    'Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
    '    If ComboBox1.SelectedIndex = -1 Then Exit Sub

    '    ComboBox2.Items.Clear()
    '    SQL = "Select No_Rekening From rekening_suppliers where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Supplier = '" & TextBox1.Text & "' and Mata_Uang ='" & ComboBox1.Text & "' order by No_Rekening"
    '    Using dr = OpenTrans(SQL)
    '        Do While dr.Read
    '            ComboBox2.Items.Add(dr("No_Rekening"))
    '        Loop
    '    End Using

    '    'Try
    '    '    OpenConn()
    '    '    ComboBox2.Items.Clear()
    '    '    SQL = "Select No_Rekening From rekening_suppliers where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Supplier = '" & TextBox1.Text & "' and Mata_Uang ='" & ComboBox1.Text & "' order by No_Rekening"
    '    '    Using dr = OpenTrans(SQL)
    '    '        Do While dr.Read
    '    '        Loop
    '    '            ComboBox2.Items.Add(dr("No_Rekening"))
    '    '    End Using
    '    '    CloseConn()
    '    'Catch ex As Exception
    '    '    CloseConn()
    '    '    MessageBox.Show(ex.Message)
    '    '    Exit Sub
    '    'End Try
    'End Sub

    Private Sub Submit_PO_Import_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Label1.Size = New Point(Me.Width, 33)
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("Kurs Harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus()
            Exit Sub
        ElseIf ComboBox2.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus()
            Exit Sub
        ElseIf TextBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("Nilai Kurs Harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox2.Focus()
            Exit Sub
        End If

        For index As Integer = 0 To ListView1.Items.Count - 1

            If ListView1.Items(index).SubItems(0).Text = ComboBox1.Text And ListView1.Items(index).SubItems(1).Text = ComboBox2.Text Then
                ListView1.Items(index).SubItems(2).Text = TextBox2.Text

                ComboBox1.SelectedIndex = -1
                ComboBox2.SelectedIndex = -1
                TextBox2.Text = ""

                If TxtId_Rencana.Text <> "" Then
                    TxtId_Rencana_Leave(Button1, e)
                End If

                Exit Sub
            End If

        Next

        Dim Lvw As ListViewItem
        Lvw = ListView1.Items.Add(ComboBox1.Text)
        Lvw.SubItems.Add(ComboBox2.Text)
        Lvw.SubItems.Add(TextBox2.Text)

        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
        TextBox2.Text = ""

        If TxtId_Rencana.Text <> "" Then
            TxtId_Rencana_Leave(Button1, e)
        End If
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox2.Focus()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then Button1.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub HapusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HapusToolStripMenuItem.Click
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih Dahulu Data yang Mau di hapus!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        ListView1.FocusedItem.Remove()
        If TxtId_Rencana.Text <> "" Then
            TxtId_Rencana_Leave(Button1, e)
        End If

    End Sub

    Private Sub DataGridView2_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellEndEdit
        Get_Isi_Listview2(DataGridView2.CurrentRow.Index)


        If IsNumeric(Lv2Input) = False Or Val(Lv2Input) < 0 Then
            DataGridView2.CurrentRow.Cells(cell2Input).Value = 0
        End If

        Dim Total As Double = 0

        Total = Val(HilangkanTanda(Lv2HPP_Per_Pcs_ktr)) + Val(HilangkanTanda(Lv2BiayaImport)) + Val(HilangkanTanda(Lv2Input))

        DataGridView2.CurrentRow.Cells(cell2Input).Value = Format(Val(HilangkanTanda(Lv2Input)), "N2")
        DataGridView2.CurrentRow.Cells(cell2HPP_Per_Pcs_brsh).Value = Format(Total, "N0")

    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox2.Focus()
    End Sub

    Private Sub BtnSimpan_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSimpan.Leave

    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub


    Private Sub btnSelisihBiaya_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelisihBiaya.Click
        Display_Selisih_Biaya.ShowDialog()
    End Sub

    Private Sub btnSelisihPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelisihPO.Click
        Display_Selisih_PO.ShowDialog()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub


    '====================================================================================
    '=                                  JURNAL IMPORT                                   =
    '====================================================================================

    Private Sub Jurnal_Import_Pertimbangan()


        Dim id_rencana_group As String = ""

        Dim rr As Integer = 0
        SQL = "select Flag_Gabungan from rencana_order where "
        SQL = SQL & "Id_rencana = '" & TxtId_Rencana.Text & "'"
        Using Dr2 = OpenTrans(SQL)
            If Dr2.Read Then
                If General_Class.CekNULL(Dr2("Flag_Gabungan")) = "Y" Then
                    Dr2.Close()
                    SQL = "select a.id_rencana from rencana_order a, rencana_order_gabungan b where "
                    SQL = SQL & "a.id_rencana = b.Id_rencana and b.Id_rencana_induk = '" & TxtId_Rencana.Text & "'"
                    Using Dr = OpenTrans(SQL)
                        Do While Dr.Read
                            If rr <> 0 Then
                                id_rencana_group = id_rencana_group & ", "
                            End If
                            id_rencana_group = id_rencana_group & "'" & Dr("id_rencana") & "'"
                            rr += 1
                        Loop
                    End Using
                Else
                    id_rencana_group = "'" & TxtId_Rencana.Text & "'"
                End If
            Else
                Dr2.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Id Rencana tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End Using

        Dim No_PO As String = ""
        SQL = "select string_agg(''''+no_faktur+'''', ', ') as No_faktur "
        SQL = SQL & "from submit_po where id_rencana in(" & id_rencana_group & ") and status is null "
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                No_PO = Dr("No_faktur")
            Else
                Dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("No PO tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End Using


        '===============================================
        '=     GET PO BERDASARKAN FAKTUR SUBMIT PO     =
        '===============================================
        SQL = "select No_Faktur from emi_pembelian_loading "
        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Fak_Submit_PO in(" & No_PO & ") "
        SQL = SQL & "and Lokasi='" & CmbLokasi.Text & "' "
        Using Ds = BindingTrans(SQL)
            With Ds.Tables("MyTable")
                If .Rows.Count <> 0 Then

                    For i As Integer = 0 To .Rows.Count - 1

                        SQL = "select No_Faktur as No_Unloading, No_Loading from emi_timbang_unloading "
                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Loading = '" & .Rows(i).Item("No_Faktur") & "' "
                        Using Ds2 = BindingTrans(SQL)
                            If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

                                    Dim NoUnloading As String = Ds2.Tables("MyTable").Rows(j).Item("No_Unloading")
                                    Dim noLoading As String = Ds2.Tables("MyTable").Rows(j).Item("No_Loading")

                                    Jurnal_Import(NoUnloading, noLoading)

                                Next
                            End If
                        End Using


                    Next

                End If

            End With

        End Using

    End Sub

    Private Sub Jurnal_Import(ByVal faktur_timbang_unloading As String, ByVal faktur_pembelian_loading As String)

        '=== GET DATA RENCANA ORDER, BERDASAR NO LOADING ===
        Dim id_rencana As Integer
        Dim Lokasi_Jurnal As String
        SQL = "select Lokasi, No_Fak_HPP, b.ID_Rencana "
        SQL = SQL & "from emi_pembelian_loading a, HPP_Import b where "
        SQL = SQL & "a.Kode_Perusahaan=b.kode_perusahaan and a.No_Fak_HPP=b.No_Faktur "
        SQL = SQL & "and a.status is null and b.status is null "
        SQL = SQL & "and a.No_Faktur = '" & faktur_pembelian_loading & "' "
        SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
        Using dr = OpenTrans(SQL)
            If dr.Read Then
                id_rencana = dr("ID_Rencana")
                Lokasi_Jurnal = dr("Lokasi")
            Else
                dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("No Faktur Tidak ditemukan . . ! ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End Using

        '=== GET DATA RENCANA ORDER, JIKA TERDAPAT GABUNG RO ===
        Dim id_rencana_group As String = ""

        Dim i As Integer = 0
        SQL = "select Flag_Gabungan from rencana_order where "
        SQL = SQL & "Id_rencana = '" & id_rencana & "'"
        Using Dr2 = OpenTrans(SQL)
            If Dr2.Read Then
                If General_Class.CekNULL(Dr2("Flag_Gabungan")) = "Y" Then
                    Dr2.Close()
                    SQL = "select a.id_rencana, a.Total_Persen from rencana_order a, rencana_order_gabungan b where "
                    SQL = SQL & "a.id_rencana = b.Id_rencana and b.Id_rencana_induk = '" & id_rencana & "'"
                    Using Dr = OpenTrans(SQL)
                        Do While Dr.Read
                            If i <> 0 Then
                                id_rencana_group = id_rencana_group & ", "
                            End If

                            id_rencana_group = id_rencana_group & "'" & Dr("id_rencana") & "'"
                            i += 1
                        Loop
                    End Using


                Else
                    id_rencana_group = "'" & id_rencana & "'"
                End If
            Else
                Dr2.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Id Rencana tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End Using

        Dim lokasi_Barang As String = ""

        SQL = "select kode_stock_owner from EMI_Barang_Masuk_Perpallet a where "
        SQL = SQL & "Kode_Perusahaan='" & KodePerusahaan & "' and "
        SQL = SQL & "No_Pembelian_Loading='" & faktur_pembelian_loading & "' and status is null "
        '    SQL = SQL & "and Flag_Timbang_Keluar is null "
        Using dr = OpenTrans(SQL)
            If dr.Read Then
                lokasi_Barang = dr("kode_stock_owner")
            Else
                dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Data Tidak ditemukan . . ! !")
                Exit Sub
            End If
        End Using

        '=== INISIAL FAKTUR UNTUK JURNAL ======
        Dim inisial_faktur_dari As String
        SQL = "select inisial_faktur from stock_owner_gudang "
        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & lokasi_Barang & "' "
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

        '=== AMBIL DATA UNTUK KETERANGAN PO ====
        Dim Lokasi_Group As String = ""
        Dim Konte_group As String = ""
        Dim PO_Induk As String = ""
        Dim Kategori_Group As String = ""

        SQL = "select Tanggal_PO from Rencana_Order a where "
        SQL = SQL & "a.id_rencana ='" & id_rencana & "'"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                PO_Induk = "PO " & Format(Dr("Tanggal_PO"), "MM.dd")
            Else
                Dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Id Rencana tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End Using

        Dim Konte As Double = 0
        Dim xxz As Integer = 0
        SQL = "select a.id_rencana, a.Total_Persen, a.Lokasi, B.Inisial_Faktur "
        SQL = SQL & "from rencana_order a, Stock_Owner b where "
        SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and A.Lokasi = B.Kode_Stock_Owner and "
        SQL = SQL & "a.id_rencana in(" & id_rencana_group & ")"
        Using Dr = OpenTrans(SQL)
            Do While Dr.Read
                If xxz <> 0 Then
                    Lokasi_Group = Lokasi_Group & ", "
                End If
                Lokasi_Group = Lokasi_Group & Dr("Inisial_Faktur")
                Konte = Konte + Dr("Total_Persen")

                xxz += 1
            Loop
        End Using

        'If Lokasi_Group.Length <> 0 Then
        '    Lokasi_Group = Strings.Left(Lokasi_Group, Len(Lokasi_Group) - 2)
        'End If

        Dim kontainer As Integer = Konte / 100
        Dim jumlah As Integer = kontainer * 100
        Dim selisih As Integer = Konte - jumlah

        If selisih = 0 Or selisih <= 99 Then
            kontainer = kontainer
        Else
            kontainer = kontainer + 1
        End If

        Konte_group = kontainer & " C"
        Dim ix As Integer = 0

        SQL = "select d.Jenis from "
        SQL = SQL & "submit_Po A, Detail_Submit_PO B, Barang C, Kategori_Besar d where "
        SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur "
        SQL = SQL & "and B.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner= "
        SQL = SQL & "c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and "
        SQL = SQL & "c.Kode_Perusahaan = d.Kode_Perusahaan And c.Kode_Kategori_Besar = d.Kode_Kategori_Besar "
        SQL = SQL & "and id_rencana in(" & id_rencana_group & ") and a.status is null "
        SQL = SQL & "group by d.Jenis "
        Using Ds = BindingTrans(SQL)
            With Ds.Tables("MyTable")
                For index As Integer = 0 To .Rows.Count - 1
                    ix = .Rows.Count

                    SQL = "select top(1) d.Kode_Kategori_Besar from "
                    SQL = SQL & "submit_Po A, Detail_Submit_PO B, Barang C, Kategori_Besar d where "
                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur "
                    SQL = SQL & "and B.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner= "
                    SQL = SQL & "c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and "
                    SQL = SQL & "c.Kode_Perusahaan = d.Kode_Perusahaan And c.Kode_Kategori_Besar = d.Kode_Kategori_Besar "
                    SQL = SQL & "and id_rencana in(" & id_rencana_group & ")  and d.Jenis = '" & .Rows(index).Item("Jenis") & "' and a.status is null "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If index <> 0 Then
                                Kategori_Group = Kategori_Group & ", "
                            End If
                            Kategori_Group = Kategori_Group & Dr("Kode_Kategori_Besar")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Jenis Kategori Barang Tidak Di Temukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                Next
            End With
        End Using

        Dim ket As String = Strings.Left(faktur_timbang_unloading & "; " & PO_Induk & "; " & Konte_group & "; " & Kategori_Group & "; " & Lokasi_Group, 180)


        'inser jurnal ard
        Dim coa_Hutang_Dalam_Proses As String = ""
        Dim coa_Selisih_Hutang_Import As String = ""
        Dim coa_Billing As String = ""
        Dim coa_freigt As String = ""
        Dim coa_Storage As String = ""
        Dim coa_Tot_Pot_Stock_IDR As String = ""
        Dim coa_Tdk_Pot_Stock_IDR As String = ""
        Dim coa_Tdk_Pot_Stock_Hutang_IDR_Utama As String = ""
        Dim coa_Tdk_Pot_Stock_Hutang_IDR_Penolong As String = ""
        Dim coa_pph As String = ""
        Dim coa_pib As String = ""
        Dim coa_selisih_pib As String = ""
        Dim coa_pph_billing As String = ""
        Dim coa_hutang_pph_billing As String = ""
        Dim coa_Selisih_AVG_Import As String = ""
        Dim coa_Selisih_PO As String = ""
        Dim coa_Selisih_PO_Biaya As String = ""
        Dim coa_selisih_new
        Dim Metode_Hitung_Konte As String = ""


        SQL = "select hutang_pph_billing, pph_billing, Metode_Hitung_Konte, Hutang_Dalam_Proses, Selisih_Hutang_Import, Hutang_Billing_Import, "
        SQL = SQL & "Hutang_Storage_Import, Hutang_Freight_Import, Akun_Tot_Pot_Stock, "
        SQL = SQL & "Akun_Tdk_Pot_Stock, Akun_Tdk_Pot_Stock_Hutang_Utama, "
        SQL = SQL & "Akun_Tdk_Pot_Stock_Hutang_Penolong, Akun_pph, akun_pib, akun_selisih_pib, Akun_Selisih_AVG_Import, Akun_Selisih_PO, Akun_Selisih_PO_Biaya from stock_Owner "
        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Kode_stock_Owner = '" & Lokasi_Jurnal & "' "
        Using dr = OpenTrans(SQL)
            If dr.Read Then
                Metode_Hitung_Konte = dr("Metode_Hitung_Konte")
                coa_Hutang_Dalam_Proses = dr("Hutang_Dalam_Proses")
                coa_Selisih_Hutang_Import = dr("Selisih_Hutang_Import")
                coa_Billing = dr("Hutang_Billing_Import")
                coa_freigt = dr("Hutang_Freight_Import")
                coa_Storage = dr("Hutang_Storage_Import")
                coa_Tot_Pot_Stock_IDR = dr("Akun_Tot_Pot_Stock")
                coa_Tdk_Pot_Stock_IDR = dr("Akun_Tdk_Pot_Stock")
                coa_Tdk_Pot_Stock_Hutang_IDR_Utama = dr("Akun_Tdk_Pot_Stock_Hutang_Utama")
                coa_Tdk_Pot_Stock_Hutang_IDR_Penolong = dr("Akun_Tdk_Pot_Stock_Hutang_Penolong")
                coa_pph = dr("Akun_pph")
                coa_pib = dr("akun_pib")
                coa_selisih_pib = dr("akun_selisih_pib")
                coa_pph_billing = dr("pph_billing")
                coa_hutang_pph_billing = dr("hutang_pph_billing")
                coa_Selisih_AVG_Import = dr("Akun_Selisih_AVG_Import")
                coa_Selisih_PO = dr("Akun_Tdk_Pot_Stock_Hutang_Utama")
                coa_Selisih_PO_Biaya = dr("Akun_Tdk_Pot_Stock_Hutang_Utama")
                coa_selisih_new = dr("Akun_Selisih_PO")
            Else
                dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Lokasi Tidak ditemukan")
                Exit Sub
            End If
        End Using

        Dim Flag_Average_Sup As String = ""
        SQL = "select Flag_average "
        SQL = SQL & "from Transaksi_Biaya_Import a where  a.Id_rencana = '" & id_rencana & "' and status is null"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                Flag_Average_Sup = Dr("Flag_average")
            Else
                Dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Supplier Tidak ditemukan")
                Exit Sub
            End If
        End Using

        Dim Flag_Average_Sup3 As String = ""
        SQL = "select Flag_average "
        SQL = SQL & "from Transaksi_Biaya_Import3 a where  a.Id_rencana = '" & id_rencana & "' and status is null"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                Flag_Average_Sup3 = Dr("Flag_average")
            Else
                Dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Supplier Tidak ditemukan")
                Exit Sub
            End If
        End Using





        Dim Arr_Biaya_Import_Master As New ArrayList
        Dim Arr_Biaya_Import As New ArrayList
        Dim Arr_Biaya_Import_AVG As New ArrayList
        Dim Arr_Akun1 As New ArrayList
        Dim Arr_Akun2 As New ArrayList
        Dim Arr_Biaya_Import_Kategori As New ArrayList

        Dim Arr_Biaya_Bongkar_Import_Master As New ArrayList
        Dim Arr_Biaya_Bongkar_Import As New ArrayList
        Dim Arr_Lokasi_Bongkar_Import As New ArrayList
        Dim Arr_Akun1_Bongkar As New ArrayList
        Dim Arr_Akun2_Bongkar As New ArrayList
        Dim Arr_Biaya_Bongkar_Import_Kategori As New ArrayList

        Dim Biaya_Import_AVG As Double = 0
        Dim Selisih_Import_AVG As Double = 0
        Dim Biaya_Import_Total As Double = 0
        Dim Hutang_Dalam_Proses As Double = 0
        Dim Selisih_Hutang As Double = 0
        Dim Biaya_PPN As Double = 0
        Dim Billing As Double = 0
        Dim pib As Double = 0
        Dim pph_billing As Double = 0
        Dim Selisih_PO As Double = 0
        Dim Selisih_PO_Biaya As Double = 0

        Dim freigt As Double = 0
        Dim Storage As Double = 0
        Dim Tot_Pot_Stock_IDR As Double = 0
        Dim Tdk_Pot_Stock_IDR As Double = 0
        Dim Tdk_Pot_Stock_Hutang_IDR_Utama As Double = 0
        Dim Tdk_Pot_Stock_Hutang_IDR_Penolong As Double = 0
        Dim pph_pakai_persentase As Double = 0

        Dim PPN As Double = 0


        '==========================================
        '=     Get Data Emi Timbang Unloading     =
        '==========================================
        SQL = " select c.no_PO, c.Kode_Barang, d.nama as Nama_Barang, c.Tanggal_Expired, c.Tanggal_Produksi, "
        SQL = SQL & "c.Urut_PO, c.Jumlah, c.Urut_Oto as Urut_Loading, c.satuan  "
        SQL = SQL & "from EMI_Timbang_Unloading a, EMI_Timbang_Unloading_PO_Det b, EMI_Pembelian_Loading_Detail c, barang d "
        SQL = SQL & "where a.Kode_Perusahaan =b.Kode_Perusahaan and a.no_faktur=b.no_faktur and "
        SQL = SQL & "b.Kode_Perusahaan=c.Kode_Perusahaan and b.Urut_loading=c.Urut_Oto "
        SQL = SQL & "and c.Kode_Perusahaan=d.Kode_Perusahaan and c.Kode_Barang=d.Kode_Barang and c.Kode_Stock_Owner=d.Kode_Stock_Owner "
        SQL = SQL & "and a.status is null and a.No_faktur='" & faktur_timbang_unloading & "' and a.kode_Perusahaan ='" & KodePerusahaan & "' "
        SQL = SQL & "Order By d.nama "
        Using Ds3 = BindingTrans(SQL)
            If Ds3.Tables("MyTable").Rows.Count <> 0 Then

                For index = 0 To Ds3.Tables("MyTable").Rows.Count - 1

                    SQL = "Select c.PPN "
                    SQL = SQL & "From EMI_Pembelian_Loading_Detail a, EMI_Pembelian_PO_Detail b, EMI_Pembelian_PO c Where "
                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.Urut_PO = b.No_Urut And "
                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.No_Faktur = c.No_Faktur and c.status is null And "
                    SQL = SQL & "a.Urut_Oto = '" & Ds3.Tables("MyTable").Rows(index).Item("Urut_Loading") & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            PPN = dr("PPN")
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("PO Tidak ditemukan . . ! !")
                            Exit Sub
                        End If
                    End Using

                    SQL = "select a.No_faktur, a.ID_Rencana, b.Kode_stock_owner, b.Kode_barang, b.jumlah,"
                    SQL = SQL & "b.Nilai_Pot_Stock/Jumlah as Nilai_Pot_Stock, Nilai_Tdk_Pot_stock_LNS/Jumlah as Nilai_Tdk_Pot_stock_LNS,"
                    SQL = SQL & "b.Nilai_tdk_pot_stock_htg_utama/Jumlah as Nilai_tdk_pot_stock_htg_utama,"
                    SQL = SQL & "b.Nilai_Tdk_Pot_Stock_HTG_Penolong/Jumlah as Nilai_Tdk_Pot_Stock_HTG_Penolong,"
                    SQL = SQL & "b.PPH29/Jumlah as PPH29,Biaya_Billing/Jumlah as Biaya_Billing,Biaya_Kontainer/Jumlah as Biaya_Kontainer, b.Nilai_Selisih_PO/Jumlah as Nilai_Selisih_PO, b.Nilai_Selisih_PO_Biaya/Jumlah as Nilai_Selisih_PO_Biaya "
                    SQL = SQL & "from HPP_Import a, Detail_HPP_Import b where "
                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_faktur And a.Status Is null And id_rencana ='" & id_rencana & "' AND B.Kode_Barang='" & Ds3.Tables("MyTable").Rows(index).Item("Kode_Barang") & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dim jumlahUnloading As Double = Ds3.Tables("MyTable").Rows(index).Item("Jumlah")
                            Storage = Storage + (Dr("biaya_kontainer") * jumlahUnloading)
                            Billing = Billing + (Dr("Biaya_Billing") * jumlahUnloading)
                            Tot_Pot_Stock_IDR = Tot_Pot_Stock_IDR + (Dr("Nilai_Pot_Stock") * jumlahUnloading)
                            Tdk_Pot_Stock_IDR = Tdk_Pot_Stock_IDR + (Dr("Nilai_Tdk_Pot_stock_LNS") * jumlahUnloading)
                            Tdk_Pot_Stock_Hutang_IDR_Utama = Tdk_Pot_Stock_Hutang_IDR_Utama + (Dr("Nilai_tdk_pot_stock_htg_utama") * jumlahUnloading)
                            Tdk_Pot_Stock_Hutang_IDR_Penolong = Tdk_Pot_Stock_Hutang_IDR_Penolong + (Dr("Nilai_Tdk_Pot_Stock_HTG_Penolong") * jumlahUnloading)
                            pph_pakai_persentase = pph_pakai_persentase + (Dr("PPH29") * jumlahUnloading)
                            Selisih_PO = Selisih_PO + (Dr("Nilai_Selisih_PO") * jumlahUnloading)
                            Selisih_PO_Biaya = Selisih_PO_Biaya + (Dr("Nilai_Selisih_PO_Biaya") * jumlahUnloading)
                        End If
                    End Using

                    SQL = "select b.kode_stock_owner, b.Kode_Barang, jumlah, b.Nilai_PPH/Jumlah as Nilai_PPH, Nilai_PPN/Jumlah as Nilai_PPN "
                    SQL = SQL & "from Total_Billing a, Detail_Total_Billing b where a.Kode_Perusahaan=b.Kode_perusahaan and a.No_Faktur=b.No_Faktur "
                    SQL = SQL & "and a.ID_Rencana='" & id_rencana & "' and a.Status is nulL AND B.Kode_Barang='" & Ds3.Tables("MyTable").Rows(index).Item("Kode_Barang") & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dim jumlahUnloading As Double = Ds3.Tables("MyTable").Rows(index).Item("Jumlah")
                            pib = pib + (Dr("Nilai_PPN") * jumlahUnloading)
                            pph_billing = pph_billing + (Dr("Nilai_PPH") * jumlahUnloading)

                        End If
                    End Using

                    '1
                    SQL = "select b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal, "
                    SQL = SQL & "round(sum(b.total), 0) as Biaya, "

                    SQL = SQL & "isnull(("
                    SQL = SQL & "select akun_1 from detail_account_master X where X.Kode_Perusahaan = b.Kode_Perusahaan and "
                    SQL = SQL & "x.Kode_Master_Kategori_Biaya_import = b.Kode_Master_Kategori_Biaya_import and "
                    SQL = SQL & "x.lokasi = '" & Lokasi_Jurnal & "' "
                    SQL = SQL & "),0) as Akun_1, "

                    SQL = SQL & "isnull(("
                    SQL = SQL & "select akun_2 from detail_account_master X where X.Kode_Perusahaan = b.Kode_Perusahaan and "
                    SQL = SQL & "x.Kode_Master_Kategori_Biaya_import = b.Kode_Master_Kategori_Biaya_import and "
                    SQL = SQL & "x.lokasi = '" & Lokasi_Jurnal & "' "
                    SQL = SQL & "),0) as Akun_2 "

                    SQL = SQL & "from transaksi_biaya_import a, detail_transaksi_biaya_import b, Master_Kategori_Biaya_Import c where "
                    SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And "
                    SQL = SQL & "a.no_faktur = b.no_faktur And a.status Is null and "
                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Master_Kategori_Biaya_import = c.Kode_Master_Kategori_Biaya_Import and "
                    SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & id_rencana & "' " 'and C.Flag_Gabungan = 'Y' "
                    SQL = SQL & "group by b.Kode_Perusahaan, b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal "
                    SQL = SQL & "order by b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import"
                    Using ds4 = BindingTrans(SQL)
                        With ds4.Tables("MyTable")
                            For index4 As Integer = 0 To ds4.Tables("MyTable").Rows.Count - 1

                                Dim cek As Boolean = False
                                SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.Jumlah,"
                                SQL = SQL & "c.Kode_Kategori_Biaya_Import, c.Biaya/Jumlah as Biaya, c.Biaya_AVG/Jumlah as Biaya_AVG, c.Flag_Average "
                                SQL = SQL & "from hpp_import a, detail_hpp_import b,detail_hpp_import_biaya c where "
                                SQL = SQL & "a.kode_perusahaan=b.Kode_Perusahaan and a.No_faktur=b.No_Faktur and b.no_faktur=c.No_Faktur "
                                SQL = SQL & "and b.Kode_Barang=c.Kode_Barang and b.Kode_stock_owner=c.Kode_stock_owner and "
                                SQL = SQL & "c.Kode_kategori_biaya_import ='" & ds4.Tables("MyTable").Rows(index4).Item("Kode_kategori_biaya_import") & "' and c.Kode_Barang='" & Ds3.Tables("MyTable").Rows(index).Item("Kode_Barang") & "' and a.Status Is null And a.id_rencana ='" & id_rencana & "' "
                                Using dr = OpenTrans(SQL)
                                    If dr.Read Then

                                        Dim jumlahUnloading As Double = Ds3.Tables("MyTable").Rows(index).Item("Jumlah")

                                        For index1 As Integer = 0 To Arr_Biaya_Import_Kategori.Count - 1


                                            If Arr_Biaya_Import_Kategori.Item(index1) = ds4.Tables("MyTable").Rows(index4).Item("Kode_kategori_biaya_import") Then

                                                Arr_Biaya_Import.Item(index1) += Val(HilangkanTanda(Format(dr("Biaya") * jumlahUnloading, "N0")))

                                                Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format(dr("Biaya") * jumlahUnloading, "N0")))
                                                Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format(dr("Biaya_AVG") * jumlahUnloading, "N0")))
                                                cek = True
                                            End If

                                        Next

                                        If cek = False Then
                                            Arr_Biaya_Import_Master.Add(ds4.Tables("MyTable").Rows(index4).Item("Kode_Master_Kategori_Biaya_Import"))
                                            Arr_Biaya_Import_Kategori.Add(dr("Kode_Kategori_Biaya_Import"))
                                            Arr_Biaya_Import.Add(Val(HilangkanTanda(Format(dr("Biaya") * jumlahUnloading, "N0"))))
                                            Arr_Akun1.Add(ds4.Tables("MyTable").Rows(index4).Item("Akun_1"))
                                            Arr_Akun2.Add(ds4.Tables("MyTable").Rows(index4).Item("Akun_2"))

                                            Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format(dr("Biaya") * jumlahUnloading, "N0")))
                                            Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format(dr("Biaya_AVG") * jumlahUnloading, "N0")))
                                        End If

                                    End If
                                End Using


                                ' ''SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.Jumlah, "
                                ' ''SQL = SQL & "c.Kode_Kategori_Biaya_Import, c.Biaya2/Jumlah as Biaya2, c.Biaya_AVG2/Jumlah as Biaya_AVG2, "
                                ' ''SQL = SQL & "c.Biayawetdry/Jumlah as Biayawetdry, c.Biayawetdry_AVG/Jumlah as Biayawetdry_AVG, c.Flag_Average "
                                ' ''SQL = SQL & "from hpp_import a, detail_hpp_import2 b,detail_hpp_import2_biaya c where "
                                ' ''SQL = SQL & "a.kode_perusahaan=b.Kode_Perusahaan and a.No_faktur=b.No_Faktur and b.no_faktur=c.No_Faktur "
                                ' ''SQL = SQL & "and b.Kode_Barang=c.Kode_Barang and b.Kode_stock_owner=c.Kode_stock_owner and b.Lokasi_Tujuan=c.lokasi_tujuan and "
                                ' ''SQL = SQL & "c.Kode_kategori_biaya_import ='" & .Rows(index3).Item("Kode_kategori_biaya_import") & "' and c.Kode_Barang='" & LvKdbarang & "' and c.Lokasi_tujuan ='" & LvSO & "'  and a.Status Is null And a.id_rencana ='" & id_rencana & "' "
                                ' ''Using dr = OpenTrans(SQL)
                                ' ''    If dr.Read Then


                                ' ''        For index1 As Integer = 0 To Arr_Biaya_Import_Kategori.Count - 1


                                ' ''            If Arr_Biaya_Import_Kategori.Item(index1) = .Rows(index3).Item("Kode_kategori_biaya_import") Then

                                ' ''                Arr_Biaya_Import.Item(index1) += Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * LvJumlah, "N0")))

                                ' ''                Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * LvJumlah, "N0")))
                                ' ''                Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format((dr("Biaya_AVG2") + dr("Biayawetdry_AVG")) * LvJumlah, "N0")))
                                ' ''                cek = True
                                ' ''            End If

                                ' ''        Next

                                ' ''        If cek = False Then
                                ' ''            Arr_Biaya_Import_Master.Add(.Rows(index3).Item("Kode_Master_Kategori_Biaya_Import"))
                                ' ''            Arr_Biaya_Import_Kategori.Add(dr("Kode_Kategori_Biaya_Import"))
                                ' ''            Arr_Biaya_Import.Add(Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * LvJumlah, "N0"))))
                                ' ''            Arr_Akun1.Add(.Rows(index3).Item("Akun_1"))
                                ' ''            Arr_Akun2.Add(.Rows(index3).Item("Akun_2"))

                                ' ''            Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * LvJumlah, "N0")))
                                ' ''            Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format((dr("Biaya_AVG2") + dr("Biayawetdry_AVG")) * LvJumlah, "N0")))
                                ' ''        End If

                                ' ''    End If
                                ' ''End Using


                            Next
                        End With


                    End Using


                    SQL = "select b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal, B.Kode_Stock_Owner, "
                    SQL = SQL & "round(sum(b.total), 0) as Biaya, "

                    SQL = SQL & "isnull(("
                    SQL = SQL & "select akun_1 from detail_account_master X where X.Kode_Perusahaan = b.Kode_Perusahaan and "
                    SQL = SQL & "x.Kode_Master_Kategori_Biaya_import = b.Kode_Master_Kategori_Biaya_import and "
                    SQL = SQL & "x.lokasi = '" & Lokasi_Jurnal & "' "
                    SQL = SQL & "),0) as Akun_1, "

                    SQL = SQL & "isnull(("
                    SQL = SQL & "select akun_2 from detail_account_master X where X.Kode_Perusahaan = b.Kode_Perusahaan and "
                    SQL = SQL & "x.Kode_Master_Kategori_Biaya_import = b.Kode_Master_Kategori_Biaya_import and "
                    SQL = SQL & "x.lokasi = '" & Lokasi_Jurnal & "' "
                    SQL = SQL & "),0) as Akun_2 "

                    SQL = SQL & "from transaksi_biaya_import a, detail_transaksi_biaya_import b, Master_Kategori_Biaya_Import c where "
                    SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And "
                    SQL = SQL & "a.no_faktur = b.no_faktur And a.status Is null and "
                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Master_Kategori_Biaya_import = c.Kode_Master_Kategori_Biaya_Import and "
                    SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & id_rencana & "' and b.kode_stock_owner='" & Lokasi_Jurnal & "' " ' and C.Flag_Gabungan = 'T' "
                    SQL = SQL & "group by b.Kode_Perusahaan, b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal, B.Kode_Stock_Owner "
                    SQL = SQL & "order by b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import"
                    Using ds5 = BindingTrans(SQL)
                        With ds5.Tables("MyTable")
                            For index5 As Integer = 0 To .Rows.Count - 1

                                Dim cek As Boolean = False

                                SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.Jumlah, "
                                SQL = SQL & "c.Kode_Kategori_Biaya_Import, c.Biaya2/Jumlah as Biaya2, c.Biaya_AVG2/Jumlah as Biaya_AVG2, "
                                SQL = SQL & "c.Biayawetdry/Jumlah as Biayawetdry, c.Biayawetdry_AVG/Jumlah as Biayawetdry_AVG, c.Flag_Average "
                                SQL = SQL & "from hpp_import a, detail_hpp_import2 b,detail_hpp_import2_biaya c where "
                                SQL = SQL & "a.kode_perusahaan=b.Kode_Perusahaan and a.No_faktur=b.No_Faktur and b.no_faktur=c.No_Faktur "
                                SQL = SQL & "and b.Kode_Barang=c.Kode_Barang and b.Kode_stock_owner=c.Kode_stock_owner and b.Lokasi_Tujuan=c.lokasi_tujuan and "
                                SQL = SQL & "c.Kode_kategori_biaya_import ='" & ds5.Tables("MyTable").Rows(index5).Item("Kode_kategori_biaya_import") & "' and c.Kode_Barang='" & Ds3.Tables("MyTable").Rows(index).Item("Kode_Barang") & "' and c.Lokasi_tujuan ='" & Lokasi_Jurnal & "'  and a.Status Is null And a.id_rencana ='" & id_rencana & "' "
                                Using dr = OpenTrans(SQL)
                                    If dr.Read Then

                                        Dim jumlahUnloading As Double = Ds3.Tables("MyTable").Rows(index).Item("Jumlah")

                                        For index1 As Integer = 0 To Arr_Biaya_Bongkar_Import.Count - 1


                                            If Arr_Biaya_Bongkar_Import_Kategori.Item(index1) = ds5.Tables("MyTable").Rows(index5).Item("Kode_kategori_biaya_import") Then

                                                Arr_Biaya_Bongkar_Import.Item(index1) += Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * jumlahUnloading, "N0")))

                                                Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * jumlahUnloading, "N0")))
                                                Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format((dr("Biaya_AVG2") + dr("Biayawetdry_AVG")) * jumlahUnloading, "N0")))
                                                cek = True
                                            End If

                                        Next

                                        If cek = False Then

                                            Arr_Biaya_Bongkar_Import_Master.Add(ds5.Tables("MyTable").Rows(index5).Item("Kode_Master_Kategori_Biaya_Import"))
                                            Arr_Biaya_Bongkar_Import_Kategori.Add(ds5.Tables("MyTable").Rows(index5).Item("Kode_Kategori_Biaya_Import"))
                                            Arr_Biaya_Bongkar_Import.Add(Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * jumlahUnloading, "N0"))))
                                            Arr_Lokasi_Bongkar_Import.Add(Lokasi_Jurnal)
                                            Arr_Akun1_Bongkar.Add(ds5.Tables("MyTable").Rows(index5).Item("Akun_1"))
                                            Arr_Akun2_Bongkar.Add(ds5.Tables("MyTable").Rows(index5).Item("Akun_2"))

                                            Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * jumlahUnloading, "N0")))
                                            Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format((dr("Biaya_AVG2") + dr("Biayawetdry_AVG")) * jumlahUnloading, "N0")))
                                        End If

                                    End If
                                End Using


                            Next
                        End With


                    End Using



                    SQL = "select b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal "

                    SQL = SQL & "from transaksi_biaya_import3 a, detail_transaksi_biaya_import3 b, Master_Kategori_Biaya_Import c where "
                    SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And "
                    SQL = SQL & "a.no_faktur = b.no_faktur And a.status Is null and "
                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Master_Kategori_Biaya_import = c.Kode_Master_Kategori_Biaya_Import and "
                    SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & id_rencana & "' "
                    SQL = SQL & "group by b.Kode_Perusahaan, b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal "
                    SQL = SQL & "order by b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import"
                    Using ds6 = BindingTrans(SQL)
                        With ds6.Tables("MyTable")
                            For index6 As Integer = 0 To .Rows.Count - 1

                                SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.Jumlah, "
                                SQL = SQL & "c.Kode_Kategori_Biaya_Import, c.Biaya/Jumlah as Biaya, c.Biaya_AVG/Jumlah as Biaya_AVG, c.Flag_Average "
                                SQL = SQL & "from hpp_import a, detail_hpp_import b,detail_hpp_import_biaya c where "
                                SQL = SQL & "a.kode_perusahaan=b.Kode_Perusahaan and a.No_faktur=b.No_Faktur and b.no_faktur=c.No_Faktur "
                                SQL = SQL & "and b.Kode_Barang=c.Kode_Barang and b.Kode_stock_owner=c.Kode_stock_owner and "
                                SQL = SQL & "c.Kode_kategori_biaya_import ='" & ds6.Tables("MyTable").Rows(index6).Item("Kode_kategori_biaya_import") & "' and c.Kode_Barang='" & Ds3.Tables("MyTable").Rows(index).Item("Kode_Barang") & "'  and a.Status Is null And a.id_rencana ='" & id_rencana & "' "
                                Using dr = OpenTrans(SQL)
                                    If dr.Read Then

                                        Dim jumlahUnloading As Double = Ds3.Tables("MyTable").Rows(index).Item("Jumlah")

                                        freigt = freigt + Val(HilangkanTanda(Format(dr("Biaya") * jumlahUnloading, "N0")))
                                        Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format(dr("Biaya_AVG") * jumlahUnloading, "N0")))

                                    End If
                                End Using


                            Next
                        End With


                    End Using
                Next

            End If
        End Using

        pib = Val(HilangkanTanda(Format(pib, "N0")))

        pph_billing = Val(HilangkanTanda(Format(pph_billing, "N0")))

        Storage = Val(HilangkanTanda(Format(Storage, "N0")))

        Billing = Val(HilangkanTanda(Format(Billing, "N0")))

        Tot_Pot_Stock_IDR = Val(HilangkanTanda(Format(Tot_Pot_Stock_IDR, "N0")))

        Tdk_Pot_Stock_IDR = Val(HilangkanTanda(Format(Tdk_Pot_Stock_IDR, "N0")))

        Tdk_Pot_Stock_Hutang_IDR_Utama = Val(HilangkanTanda(Format(Tdk_Pot_Stock_Hutang_IDR_Utama, "N0")))

        Tdk_Pot_Stock_Hutang_IDR_Penolong = Val(HilangkanTanda(Format(Tdk_Pot_Stock_Hutang_IDR_Penolong, "N0")))

        pph_pakai_persentase = Val(HilangkanTanda(Format(pph_pakai_persentase, "N0")))

        Selisih_PO = Val(HilangkanTanda(Format(Selisih_PO, "N0")))

        Selisih_PO_Biaya = Val(HilangkanTanda(Format(Selisih_PO_Biaya, "N0")))

        SQL = "select sum(Jumlah * hpp_satuan_display) as Biaya from EMI_Pembelian_Loading_detail "
        SQL = SQL & "where No_Faktur = '" & faktur_pembelian_loading & "'"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                Hutang_Dalam_Proses = Dr("Biaya")
            Else
                Dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show(" Nilai Hutang Dalam Proses Tidak ditemukan")
                Exit Sub
            End If
        End Using




        For index As Integer = 0 To Arr_Biaya_Import_Master.Count - 1

            SQL = "select* from Detail_Account_Master where "
            SQL = SQL & "Lokasi = '" & Lokasi_Jurnal & "' and Kode_master_Kategori_biaya_import = '" & Arr_Biaya_Import_Master.Item(index) & "' "
            SQL = SQL & "and Akun_1 ='" & Arr_Akun1.Item(index) & "'  and Akun_2 = '" & Arr_Akun2.Item(index) & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Akun " & Arr_Biaya_Import_Master.Item(index) & " Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End Using

        Next

        For index As Integer = 0 To Arr_Biaya_Bongkar_Import_Master.Count - 1

            SQL = "select* from Detail_Account_Master where "
            SQL = SQL & "Lokasi = '" & Lokasi_Jurnal & "' and Kode_master_Kategori_biaya_import = '" & Arr_Biaya_Bongkar_Import_Master.Item(index) & "' "
            SQL = SQL & "and Akun_1 ='" & Arr_Akun1_Bongkar.Item(index) & "'  and Akun_2 = '" & Arr_Akun2_Bongkar.Item(index) & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Akun " & Arr_Biaya_Bongkar_Import_Master.Item(index) & " Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End Using

        Next



        If PPN <> 0 Then
            Biaya_PPN = HilangkanTanda(Format(Hutang_Dalam_Proses * PPN / 100, "N0"))
        End If



        If Flag_Average_Sup = "Y" Then
            Selisih_Import_AVG = Biaya_Import_AVG - (Biaya_Import_Total + freigt)
        End If

        Selisih_Hutang = (Hutang_Dalam_Proses + Biaya_PPN + pph_billing) - (Biaya_Import_Total + Selisih_Import_AVG + Billing + Storage + freigt + pph_pakai_persentase + Tot_Pot_Stock_IDR + Tdk_Pot_Stock_IDR + Tdk_Pot_Stock_Hutang_IDR_Utama + Tdk_Pot_Stock_Hutang_IDR_Penolong + pib + (Biaya_PPN - pib) + pph_billing + Selisih_PO + Selisih_PO_Biaya)
        Selisih_Hutang = Val(HilangkanTanda(Format(Selisih_Hutang, "N0")))



        Dim Kode_voucher As String = ""
        Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)

        Dim kode_voucher2_ As String = "NULL"
        Dim Kode_Voucher2 As String = ""


        Dim sudah_jurnal As Boolean = False


        Dim pagenumber As Integer = 1
        Dim pagenumber2 As Integer = 1

        SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
        SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
        SQL = SQL & "'" & Kode_voucher & "', "
        SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
        SQL = SQL & "'" & Format(CDate(tgl_skg), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
        SQL = SQL & "'" & KodeProyek & "', 'Pembelian " & faktur_timbang_unloading & "', '', "
        SQL = SQL & "'-', '" & UserID & "')"
        ExecuteTrans(SQL)


        SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Hutang_Dalam_Proses, 1),
                    Strings.Mid(coa_Hutang_Dalam_Proses, 2, 1),
                    Strings.Mid(Ganti(coa_Hutang_Dalam_Proses), 3),
                    KodePerusahaan, KodeProyek, ket, Hutang_Dalam_Proses, "0", pagenumber)
        ExecuteTrans(SQL)
        pagenumber = pagenumber + 1

        If PPN <> 0 Then 'ada ppn
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Hutang_Dalam_Proses, 1),
                      Strings.Mid(coa_Hutang_Dalam_Proses, 2, 1),
                      Strings.Mid(Ganti(coa_Hutang_Dalam_Proses), 3),
                      KodePerusahaan, KodeProyek, ket, Biaya_PPN, "0", pagenumber)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1
        End If

        If pph_billing <> 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_pph_billing, 1),
                     Strings.Mid(coa_pph_billing, 2, 1),
                     Strings.Mid(Ganti(coa_pph_billing), 3),
                     KodePerusahaan, KodeProyek, ket, pph_billing, "0", pagenumber)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1
        End If

        For index As Integer = 0 To Arr_Biaya_Import_Master.Count - 1
            If Val(Arr_Biaya_Import.Item(index)) <> 0 Then
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Arr_Akun2.Item(index), 1),
                     Strings.Mid(Arr_Akun2.Item(index), 2, 1),
                     Strings.Mid(Ganti(Arr_Akun2.Item(index)), 3),
                     KodePerusahaan, KodeProyek, ket & "; " & Arr_Biaya_Import_Kategori.Item(index), "0", Arr_Biaya_Import.Item(index), pagenumber)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                SQL = SQL & "values('" & KodePerusahaan & "', '" & faktur_timbang_unloading & "', "
                SQL = SQL & "'" & Arr_Biaya_Import_Kategori.Item(index) & "', '" & Arr_Biaya_Import.Item(index) & "', "
                SQL = SQL & "'" & Arr_Akun2.Item(index) & "')"
                ExecuteTrans(SQL)
            End If
        Next

        For index As Integer = 0 To Arr_Biaya_Bongkar_Import_Master.Count - 1
            Dim ket2 As String = Strings.Left(ket & "; " & Arr_Biaya_Bongkar_Import_Kategori.Item(index) & "-" & Arr_Lokasi_Bongkar_Import.Item(index), 180)
            If Arr_Biaya_Bongkar_Import.Item(index) <> 0 Then
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Arr_Akun2_Bongkar.Item(index), 1),
                      Strings.Mid(Arr_Akun2_Bongkar.Item(index), 2, 1),
                      Strings.Mid(Ganti(Arr_Akun2_Bongkar.Item(index)), 3),
                      KodePerusahaan, KodeProyek, ket2, "0", Arr_Biaya_Bongkar_Import.Item(index), pagenumber)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                SQL = SQL & "values('" & KodePerusahaan & "', '" & faktur_timbang_unloading & "', "
                SQL = SQL & "'" & Arr_Biaya_Bongkar_Import_Kategori.Item(index) & "-" & Arr_Lokasi_Bongkar_Import.Item(index) & "', '" & Arr_Biaya_Bongkar_Import.Item(index) & "', "
                SQL = SQL & "'" & Arr_Akun2_Bongkar.Item(index) & "')"
                ExecuteTrans(SQL)
            End If

        Next

        If Selisih_Import_AVG > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Selisih_AVG_Import, 1),
                    Strings.Mid(coa_Selisih_AVG_Import, 2, 1),
                    Strings.Mid(Ganti(coa_Selisih_AVG_Import), 3),
                    KodePerusahaan, KodeProyek, ket, "0", Selisih_Import_AVG, pagenumber)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1
        End If

        If Selisih_Import_AVG < 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Selisih_AVG_Import, 1),
                    Strings.Mid(coa_Selisih_AVG_Import, 2, 1),
                    Strings.Mid(Ganti(coa_Selisih_AVG_Import), 3),
                    KodePerusahaan, KodeProyek, ket, Math.Abs(Selisih_Import_AVG), "0", pagenumber)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1
        End If

        If Billing > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Billing, 1),
                    Strings.Mid(coa_Billing, 2, 1),
                    Strings.Mid(Ganti(coa_Billing), 3),
                    KodePerusahaan, KodeProyek, ket, "0", Billing, pagenumber)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & faktur_timbang_unloading & "', "
            SQL = SQL & "'BILLING', '" & Billing & "', "
            SQL = SQL & "'" & coa_Billing & "')"
            ExecuteTrans(SQL)

        End If

        If Storage > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Storage, 1),
                    Strings.Mid(coa_Storage, 2, 1),
                    Strings.Mid(Ganti(coa_Storage), 3),
                    KodePerusahaan, KodeProyek, ket & "; STORAGE", "0", Storage, pagenumber)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & faktur_timbang_unloading & "', "
            SQL = SQL & "'STORAGE', '" & Storage & "', "
            SQL = SQL & "'" & coa_Storage & "')"
            ExecuteTrans(SQL)

        End If

        If freigt > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_freigt, 1),
                    Strings.Mid(coa_freigt, 2, 1),
                    Strings.Mid(Ganti(coa_freigt), 3),
                    KodePerusahaan, KodeProyek, ket, "0", freigt, pagenumber)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & faktur_timbang_unloading & "', "
            SQL = SQL & "'FREIGHT', '" & freigt & "', "
            SQL = SQL & "'" & coa_freigt & "')"
            ExecuteTrans(SQL)

        End If

        If pph_pakai_persentase > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_pph, 1),
                Strings.Mid(coa_pph, 2, 1),
                Strings.Mid(Ganti(coa_pph), 3),
                KodePerusahaan, KodeProyek, ket, "0", pph_pakai_persentase, pagenumber)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & faktur_timbang_unloading & "', "
            SQL = SQL & "'PPH29', '" & pph_pakai_persentase & "', "
            SQL = SQL & "'" & coa_pph & "')"
            ExecuteTrans(SQL)

        End If

        'zzzz

        If Tot_Pot_Stock_IDR > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Tot_Pot_Stock_IDR, 1),
                    Strings.Mid(coa_Tot_Pot_Stock_IDR, 2, 1),
                    Strings.Mid(Ganti(coa_Tot_Pot_Stock_IDR), 3),
                    KodePerusahaan, KodeProyek, ket, "0", Tot_Pot_Stock_IDR, pagenumber)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & faktur_timbang_unloading & "', "
            SQL = SQL & "'Total Potong Stock', '" & Tot_Pot_Stock_IDR & "', "
            SQL = SQL & "'" & coa_Tot_Pot_Stock_IDR & "')"
            ExecuteTrans(SQL)

        End If

        'SQL = "select kode_perusahaan from detail_jurnal where Kode_Voucher = '" & Kode_Voucher & "' "
        'SQL = SQL & "and Kode_Master_Acc+Kode_Acc+Kode_Detail_Acc = '" & coa_Tot_Pot_Stock_IDR & "'"
        'Using Dr = OpenTrans(SQL)
        '    If Dr.Read Then
        '        Dr.Close()

        '        SQL = "update Detail_Jurnal set Kredit = Kredit + " & Tot_Pot_Stock_IDR & " "
        '        SQL = SQL & "where Kode_Voucher = '" & Kode_Voucher & "' "
        '        SQL = SQL & "and Kode_Master_Acc+Kode_Acc+Kode_Detail_Acc = '" & coa_Tot_Pot_Stock_IDR & "' "
        '        ExecuteTrans(SQL)
        '    Else
        '        If Tot_Pot_Stock_IDR > 0 Then
        '            Dr.Close()
        '            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_Tot_Pot_Stock_IDR, 1), _
        '                    Strings.Mid(coa_Tot_Pot_Stock_IDR, 2, 1), _
        '                    Strings.Mid(Ganti(coa_Tot_Pot_Stock_IDR), 3), _
        '                    KodePerusahaan, KodeProyek, ket, "0", Tot_Pot_Stock_IDR, pagenumber)
        '            ExecuteTrans(SQL)
        '            pagenumber = pagenumber + 1
        '        End If
        '    End If
        'End Using

        If Tdk_Pot_Stock_IDR > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Tdk_Pot_Stock_IDR, 1),
                    Strings.Mid(coa_Tdk_Pot_Stock_IDR, 2, 1),
                    Strings.Mid(Ganti(coa_Tdk_Pot_Stock_IDR), 3),
                    KodePerusahaan, KodeProyek, ket, "0", Tdk_Pot_Stock_IDR, pagenumber)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & faktur_timbang_unloading & "', "
            SQL = SQL & "'Tidak Potong Stock Lunas', '" & Tdk_Pot_Stock_IDR & "', "
            SQL = SQL & "'" & coa_Tdk_Pot_Stock_IDR & "')"
            ExecuteTrans(SQL)

        End If

        'SQL = "select kode_perusahaan from detail_jurnal where Kode_Voucher = '" & Kode_Voucher & "' "
        'SQL = SQL & "and Kode_Master_Acc+Kode_Acc+Kode_Detail_Acc = '" & coa_Tdk_Pot_Stock_IDR & "'"
        'Using Dr = OpenTrans(SQL)
        '    If Dr.Read Then
        '        Dr.Close()

        '        SQL = "update Detail_Jurnal set Kredit = Kredit + " & Tdk_Pot_Stock_IDR & " "
        '        SQL = SQL & "where Kode_Voucher = '" & Kode_Voucher & "' "
        '        SQL = SQL & "and Kode_Master_Acc+Kode_Acc+Kode_Detail_Acc = '" & coa_Tdk_Pot_Stock_IDR & "' "
        '        ExecuteTrans(SQL)
        '    Else
        '        If Tdk_Pot_Stock_IDR > 0 Then
        '            Dr.Close()

        '            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_Tdk_Pot_Stock_IDR, 1), _
        '                    Strings.Mid(coa_Tdk_Pot_Stock_IDR, 2, 1), _
        '                    Strings.Mid(Ganti(coa_Tdk_Pot_Stock_IDR), 3), _
        '                    KodePerusahaan, KodeProyek, ket, "0", Tdk_Pot_Stock_IDR, pagenumber)
        '            ExecuteTrans(SQL)
        '            pagenumber = pagenumber + 1
        '        End If
        '    End If
        'End Using

        If Tdk_Pot_Stock_Hutang_IDR_Utama > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Tdk_Pot_Stock_Hutang_IDR_Utama, 1),
                    Strings.Mid(coa_Tdk_Pot_Stock_Hutang_IDR_Utama, 2, 1),
                    Strings.Mid(Ganti(coa_Tdk_Pot_Stock_Hutang_IDR_Utama), 3),
                    KodePerusahaan, KodeProyek, ket, "0", Tdk_Pot_Stock_Hutang_IDR_Utama, pagenumber)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & faktur_timbang_unloading & "', "
            SQL = SQL & "'Tidak Potong Stock Bahan Utama Hutang', '" & Tdk_Pot_Stock_Hutang_IDR_Utama & "', "
            SQL = SQL & "'" & coa_Tdk_Pot_Stock_Hutang_IDR_Utama & "')"
            ExecuteTrans(SQL)

        End If

        'SQL = "select kode_perusahaan from detail_jurnal where Kode_Voucher = '" & Kode_Voucher & "' "
        'SQL = SQL & "and Kode_Master_Acc+Kode_Acc+Kode_Detail_Acc = '" & coa_Tdk_Pot_Stock_Hutang_IDR_Utama & "'"
        'Using Dr = OpenTrans(SQL)
        '    If Dr.Read Then
        '        Dr.Close()

        '        SQL = "update Detail_Jurnal set Kredit = Kredit + " & Tdk_Pot_Stock_Hutang_IDR_Utama & " "
        '        SQL = SQL & "where Kode_Voucher = '" & Kode_Voucher & "' "
        '        SQL = SQL & "and Kode_Master_Acc+Kode_Acc+Kode_Detail_Acc = '" & coa_Tdk_Pot_Stock_Hutang_IDR_Utama & "' "
        '        ExecuteTrans(SQL)
        '    Else
        '        If Tdk_Pot_Stock_Hutang_IDR_Utama > 0 Then
        '            Dr.Close()
        '            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_Tdk_Pot_Stock_Hutang_IDR_Utama, 1), _
        '                    Strings.Mid(coa_Tdk_Pot_Stock_Hutang_IDR_Utama, 2, 1), _
        '                    Strings.Mid(Ganti(coa_Tdk_Pot_Stock_Hutang_IDR_Utama), 3), _
        '                    KodePerusahaan, KodeProyek, ket, "0", Tdk_Pot_Stock_Hutang_IDR_Utama, pagenumber)
        '            ExecuteTrans(SQL)
        '            pagenumber = pagenumber + 1
        '        End If
        '    End If
        'End Using

        If Tdk_Pot_Stock_Hutang_IDR_Penolong > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Tdk_Pot_Stock_Hutang_IDR_Penolong, 1),
                    Strings.Mid(coa_Tdk_Pot_Stock_Hutang_IDR_Penolong, 2, 1),
                    Strings.Mid(Ganti(coa_Tdk_Pot_Stock_Hutang_IDR_Penolong), 3),
                    KodePerusahaan, KodeProyek, ket, "0", Tdk_Pot_Stock_Hutang_IDR_Penolong, pagenumber)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & faktur_timbang_unloading & "', "
            SQL = SQL & "'Tidak Potong Stock Bahan Penolong Hutang', '" & Tdk_Pot_Stock_Hutang_IDR_Penolong & "', "
            SQL = SQL & "'" & coa_Tdk_Pot_Stock_Hutang_IDR_Penolong & "')"
            ExecuteTrans(SQL)

        End If

        'SQL = "select kode_perusahaan from detail_jurnal where Kode_Voucher = '" & Kode_Voucher & "' "
        'SQL = SQL & "and Kode_Master_Acc+Kode_Acc+Kode_Detail_Acc = '" & coa_Tdk_Pot_Stock_Hutang_IDR_Penolong & "'"
        'Using Dr = OpenTrans(SQL)
        '    If Dr.Read Then
        '        Dr.Close()

        '        SQL = "update Detail_Jurnal set Kredit = Kredit + " & Tdk_Pot_Stock_Hutang_IDR_Penolong & " "
        '        SQL = SQL & "where Kode_Voucher = '" & Kode_Voucher & "' "
        '        SQL = SQL & "and Kode_Master_Acc+Kode_Acc+Kode_Detail_Acc = '" & coa_Tdk_Pot_Stock_Hutang_IDR_Penolong & "' "
        '        ExecuteTrans(SQL)
        '    Else
        '        If Tdk_Pot_Stock_Hutang_IDR_Penolong > 0 Then
        '            Dr.Close()
        '            SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_Tdk_Pot_Stock_Hutang_IDR_Penolong, 1), _
        '                    Strings.Mid(coa_Tdk_Pot_Stock_Hutang_IDR_Penolong, 2, 1), _
        '                    Strings.Mid(Ganti(coa_Tdk_Pot_Stock_Hutang_IDR_Penolong), 3), _
        '                    KodePerusahaan, KodeProyek, ket, "0", Tdk_Pot_Stock_Hutang_IDR_Penolong, pagenumber)
        '            ExecuteTrans(SQL)
        '            pagenumber = pagenumber + 1
        '        End If
        '    End If
        'End Using

        If Selisih_PO <> 0 Then
            If Selisih_PO < 0 Then
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Selisih_PO, 1),
                        Strings.Mid(coa_Selisih_PO, 2, 1),
                        Strings.Mid(Ganti(coa_Selisih_PO), 3),
                        KodePerusahaan, KodeProyek, ket, Math.Abs(Selisih_PO), "0", pagenumber)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

            Else
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Selisih_PO, 1),
                        Strings.Mid(coa_Selisih_PO, 2, 1),
                        Strings.Mid(Ganti(coa_Selisih_PO), 3),
                        KodePerusahaan, KodeProyek, ket, "0", Selisih_PO, pagenumber)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

            End If
            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & faktur_timbang_unloading & "', "
            SQL = SQL & "'Selisih PO', '" & Selisih_PO & "', "
            SQL = SQL & "'" & coa_Selisih_PO & "')"
            ExecuteTrans(SQL)
        End If

        If Selisih_PO_Biaya <> 0 Then
            If Selisih_PO_Biaya < 0 Then
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Selisih_PO_Biaya, 1),
                        Strings.Mid(coa_Selisih_PO_Biaya, 2, 1),
                        Strings.Mid(Ganti(coa_Selisih_PO_Biaya), 3),
                        KodePerusahaan, KodeProyek, ket, Math.Abs(Selisih_PO_Biaya), "0", pagenumber)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

            Else
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Selisih_PO_Biaya, 1),
                        Strings.Mid(coa_Selisih_PO_Biaya, 2, 1),
                        Strings.Mid(Ganti(coa_Selisih_PO_Biaya), 3),
                        KodePerusahaan, KodeProyek, ket, "0", Selisih_PO_Biaya, pagenumber)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

            End If

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & faktur_timbang_unloading & "', "
            SQL = SQL & "'Selisih PO Biaya', '" & Selisih_PO_Biaya & "', "
            SQL = SQL & "'" & coa_Selisih_PO_Biaya & "')"
            ExecuteTrans(SQL)
        End If

        If Selisih_Hutang <> 0 Then
            If Selisih_Hutang < 0 Then
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Selisih_Hutang_Import, 1),
                        Strings.Mid(coa_Selisih_Hutang_Import, 2, 1),
                        Strings.Mid(Ganti(coa_Selisih_Hutang_Import), 3),
                        KodePerusahaan, KodeProyek, ket, Math.Abs(Selisih_Hutang), "0", pagenumber)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

            Else
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_Selisih_Hutang_Import, 1),
                        Strings.Mid(coa_Selisih_Hutang_Import, 2, 1),
                        Strings.Mid(Ganti(coa_Selisih_Hutang_Import), 3),
                        KodePerusahaan, KodeProyek, ket, "0", Selisih_Hutang, pagenumber)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

            End If
            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & faktur_timbang_unloading & "', "
            SQL = SQL & "'Selisih Hutang', '" & Selisih_Hutang & "', "
            SQL = SQL & "'" & coa_Selisih_Hutang_Import & "')"
            ExecuteTrans(SQL)
        End If

        If pib > 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_pib, 1),
                       Strings.Mid(coa_pib, 2, 1),
                       Strings.Mid(Ganti(coa_pib), 3),
                       KodePerusahaan, KodeProyek, ket, "0", pib, pagenumber)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & faktur_timbang_unloading & "', "
            SQL = SQL & "'PIB', '" & pib & "', "
            SQL = SQL & "'" & coa_pib & "')"
            ExecuteTrans(SQL)

        End If

        'Metode_Hitung_Konte = Dr("Metode_Hitung_Konte")
        'coa_Hutang_Dalam_Proses = Dr("Hutang_Dalam_Proses")
        'coa_Selisih_Hutang_Import = Dr("Selisih_Hutang_Import")
        'coa_Billing = Dr("Hutang_Billing_Import")
        'coa_freigt = Dr("Hutang_Freight_Import")
        'coa_Storage = Dr("Hutang_Storage_Import")
        'coa_Tot_Pot_Stock_IDR = Dr("Akun_Tot_Pot_Stock")
        'coa_Tdk_Pot_Stock_IDR = Dr("Akun_Tdk_Pot_Stock")
        'coa_Tdk_Pot_Stock_Hutang_IDR_Utama = Dr("Akun_Tdk_Pot_Stock_Hutang_Utama")
        'coa_Tdk_Pot_Stock_Hutang_IDR_Penolong = Dr("Akun_Tdk_Pot_Stock_Hutang_Penolong")
        'coa_pph = Dr("Akun_pph")
        'coa_pib = Dr("akun_pib")
        'coa_selisih_pib = Dr("akun_selisih_pib")
        'coa_pph_billing = Dr("pph_billing")
        'coa_hutang_pph_billing = Dr("hutang_pph_billing")

        If Biaya_PPN - pib <> 0 Then 'ini <> 0 supaya kl minus tetep masuk & masuk jebakan
            If Biaya_PPN - pib < 0 Then
                'CloseTrans()
                'CloseConn()
                'MessageBox.Show("Terjadi kesalahan di jurnal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'Exit Sub

                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_selisih_pib, 1),
                    Strings.Mid(coa_selisih_pib, 2, 1),
                    Strings.Mid(Ganti(coa_selisih_pib), 3),
                    KodePerusahaan, KodeProyek, ket, -(Biaya_PPN - pib), "0", pagenumber)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                SQL = SQL & "values('" & KodePerusahaan & "', '" & faktur_timbang_unloading & "', "
                SQL = SQL & "'Selisih PIB', '" & -(Biaya_PPN - pib) & "', "
                SQL = SQL & "'" & coa_selisih_pib & "')"
                ExecuteTrans(SQL)

            Else
                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_selisih_pib, 1),
                    Strings.Mid(coa_selisih_pib, 2, 1),
                    Strings.Mid(Ganti(coa_selisih_pib), 3),
                    KodePerusahaan, KodeProyek, ket, "0", Biaya_PPN - pib, pagenumber)
                ExecuteTrans(SQL)
                pagenumber = pagenumber + 1

                SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
                SQL = SQL & "values('" & KodePerusahaan & "', '" & faktur_timbang_unloading & "', "
                SQL = SQL & "'Selisih PIB', '" & Biaya_PPN - pib & "', "
                SQL = SQL & "'" & coa_selisih_pib & "')"
                ExecuteTrans(SQL)

            End If

            'If Biaya_PPN - pib < 0 Then
            '    CloseTrans()
            '    CloseConn()
            '    MessageBox.Show("Terjadi kesalahan di jurnal!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If

            'SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_selisih_pib, 1), _
            '         Strings.Mid(coa_selisih_pib, 2, 1), _
            '         Strings.Mid(Ganti(coa_selisih_pib), 3), _
            '         KodePerusahaan, KodeProyek, ket, "0", Biaya_PPN - pib, pagenumber)
            'ExecuteTrans(SQL)
            'pagenumber = pagenumber + 1

            'SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            'SQL = SQL & "values('" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "', "
            'SQL = SQL & "'Selisih PIB', '" & Biaya_PPN - pib & "', "
            'SQL = SQL & "'" & coa_selisih_pib & "')"
            'ExecuteTrans(SQL)

        End If

        If pph_billing <> 0 Then
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(coa_hutang_pph_billing, 1),
                     Strings.Mid(coa_hutang_pph_billing, 2, 1),
                     Strings.Mid(Ganti(coa_hutang_pph_billing), 3),
                     KodePerusahaan, KodeProyek, ket, "0", pph_billing, pagenumber)
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            SQL = "insert into Pelunasan_Pembelian(Kode_perusahaan, No_Faktur, Kode, Nilai, Kode_Akun) "
            SQL = SQL & "values('" & KodePerusahaan & "', '" & faktur_timbang_unloading & "', "
            SQL = SQL & "'PPH BILLING', '" & pph_billing & "', "
            SQL = SQL & "'" & coa_hutang_pph_billing & "')"
            ExecuteTrans(SQL)

        End If

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

        If Selisih_PO + Selisih_PO_Biaya <> 0 Then

            If sudah_jurnal = False Then
                Kode_Voucher2 = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), fJU & fValPemb, KodePerusahaan)
                kode_voucher2_ = "'" & Kode_Voucher2 & "'"

                SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                SQL = SQL & "'" & Kode_Voucher2 & "', "
                SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(CDate(tgl_skg), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                SQL = SQL & "'" & KodeProyek & "', 'Pembelian " & faktur_timbang_unloading & "', '', "
                SQL = SQL & "'-', '" & UserID & "')"
                ExecuteTrans(SQL)

                sudah_jurnal = True
            End If


            'voucher selisih sebelum
            Dim total_selish As Double = Selisih_PO + Selisih_PO_Biaya
            If total_selish > 0 Then
                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_selisih_new & "' and kredit <> 0"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update 

                        SQL = "update detail_jurnal set kredit = kredit+ " & total_selish & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_selisih_new & "' and kredit <> 0"
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert
                        pagenumber2 += 1
                        SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_selisih_new, 1),
                              Strings.Mid(coa_selisih_new, 2, 1),
                              Strings.Mid(Ganti(coa_selisih_new), 3),
                              KodePerusahaan, KodeProyek, ket, "0", total_selish, pagenumber2)
                        ExecuteTrans(SQL)
                    End If
                End Using

                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Tdk_Pot_Stock_Hutang_IDR_Utama & "' and debit <> 0"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update 

                        SQL = "update detail_jurnal set debit = debit+ " & Math.Abs(total_selish) & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Tdk_Pot_Stock_Hutang_IDR_Utama & "' and debit <> 0"
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert
                        pagenumber2 += 1
                        SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_Tdk_Pot_Stock_Hutang_IDR_Utama, 1),
                              Strings.Mid(coa_Tdk_Pot_Stock_Hutang_IDR_Utama, 2, 1),
                              Strings.Mid(Ganti(coa_Tdk_Pot_Stock_Hutang_IDR_Utama), 3),
                              KodePerusahaan, KodeProyek, ket, Math.Abs(total_selish), "0", pagenumber2)
                        ExecuteTrans(SQL)
                    End If
                End Using
            Else
                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_selisih_new & "' and debit <> 0"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update 

                        SQL = "update detail_jurnal set debit = debit+ " & Math.Abs(total_selish) & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_selisih_new & "' and debit <> 0"
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert
                        pagenumber2 += 1
                        SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_selisih_new, 1),
                              Strings.Mid(coa_selisih_new, 2, 1),
                              Strings.Mid(Ganti(coa_selisih_new), 3),
                              KodePerusahaan, KodeProyek, ket, Math.Abs(total_selish), "0", pagenumber2)
                        ExecuteTrans(SQL)
                    End If
                End Using

                SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Tdk_Pot_Stock_Hutang_IDR_Utama & "' and kredit <> 0"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        'update 

                        SQL = "update detail_jurnal set kredit = kredit+ " & Math.Abs(total_selish) & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "' and "
                        SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & coa_Tdk_Pot_Stock_Hutang_IDR_Utama & "' and kredit <> 0"
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        'insert
                        pagenumber2 += 1
                        SQL = Get_Detail_Jurnal(Kode_Voucher2, Strings.Left(coa_Tdk_Pot_Stock_Hutang_IDR_Utama, 1),
                              Strings.Mid(coa_Tdk_Pot_Stock_Hutang_IDR_Utama, 2, 1),
                              Strings.Mid(Ganti(coa_Tdk_Pot_Stock_Hutang_IDR_Utama), 3),
                              KodePerusahaan, KodeProyek, ket, "0", Math.Abs(total_selish), pagenumber2)
                        ExecuteTrans(SQL)
                    End If
                End Using
            End If

        End If

        If Kode_Voucher2 <> "" Then
            SQL = "select round(sum(debit), 2) as debit, round(sum(kredit), 2) as kredit from detail_jurnal where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & Kode_Voucher2 & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("debit") <> Dr("kredit") Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Jurnal 2 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data jurnal 2 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using
        End If


        SQL = "Update EMI_Timbang_Unloading "
        SQL = SQL & "Set Kode_Voucher = '" & Kode_voucher & "' "
        SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
        SQL = SQL & "and No_Faktur = '" & faktur_timbang_unloading & "' "
        ExecuteTrans(SQL)

        IsError = True
    End Sub
End Class