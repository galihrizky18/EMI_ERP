
Imports System.Diagnostics.Eventing.Reader

Public Class EMI_HPP_Production
    Dim Jenis = "Display_Production_Order"
    Public fkd_so As String
    Dim fid_routing As String

    Dim LvNama As String
    Dim LvReal As String
    Dim LvSatuan As String
    Dim LvPengali As String
    Dim LvTotal As String
    Dim LvGrand As String

    Dim CellNama As Integer = 0
    Dim CellReal As Integer = 1
    Dim CellSatuan As Integer = 2
    Dim CellPengali As Integer = 3
    Dim CellTotal As Integer = 4
    Dim CellGrand As Integer = 5

    Dim Lv2Nama As String
    Dim Lv2Real As String
    Dim Lv2Satuan As String
    Dim Lv2Grand As String

    Dim Cell2Nama As Integer = 0
    Dim Cell2Real As Integer = 1
    Dim Cell2Satuan As Integer = 2
    Dim Cell2Grand As Integer = 3

    Dim Lv3Nama As String
    Dim Lv3Total As String

    Dim Cell3Nama As Integer = 0
    Dim Cell3Total As Integer = 1

    Dim fbulan As String = ""
    Dim ftahun As String = ""

    Dim qty_scrap_gram, qty_gram As Double

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)

        LvNama = Dgv_HslProduction.Rows(No_Index).Cells(CellNama).Value
        LvReal = Dgv_HslProduction.Rows(No_Index).Cells(CellReal).Value
        LvSatuan = Dgv_HslProduction.Rows(No_Index).Cells(CellSatuan).Value
        LvPengali = Dgv_HslProduction.Rows(No_Index).Cells(CellPengali).Value
        LvTotal = Dgv_HslProduction.Rows(No_Index).Cells(CellTotal).Value
        LvGrand = Dgv_HslProduction.Rows(No_Index).Cells(CellGrand).Value

    End Sub

    Public Sub Get_Isi_Listview2(ByVal No_Index As Integer)

        Lv2Nama = DataGridView1.Rows(No_Index).Cells(Cell2Nama).Value
        Lv2Real = DataGridView1.Rows(No_Index).Cells(Cell2Real).Value
        Lv2Satuan = DataGridView1.Rows(No_Index).Cells(Cell2Satuan).Value
        Lv2Grand = DataGridView1.Rows(No_Index).Cells(Cell2Grand).Value

    End Sub

    Public Sub Get_Isi_Listview3(ByVal No_Index As Integer)

        Lv3Nama = DataGridView2.Rows(No_Index).Cells(Cell3Nama).Value
        Lv3Total = DataGridView2.Rows(No_Index).Cells(Cell3Total).Value

    End Sub
    Private Sub EMI_HPP_Production_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        get_jam()

        Dgv_HslProduction.Rows.Clear()
        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()
        TextBox1.Text = ""
        TextBox13.Text = ""

        TextBox12.Text = ""
        TextBox5.Text = ""
        TextBox14.Text = ""

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            'Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            SQL = "select b.flag_release,a.Flag_Hasil_Produksi,a.Flag_Produksi,b.Selesai,b.Id_Routing "
            SQL = SQL & "from Emi_Split_Production_Order a,EMI_Order_Produksi b,Emi_Production_Results c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_PO = b.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Transaksi = c.No_Production_Order "
            SQL = SQL & "and c.Kode_Perusahaan ='" & KodePerusahaan & "' and c.No_Transaksi='" & TextBox4.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    fid_routing = dr("Id_Routing")
                    If General_Class.CekNULL(dr("Selesai")) <> "" Then
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("No Transaksi Sudah Selesai", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("flag_release")) = "" Then
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("No Transaksi belum direlease", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("Flag_Hasil_Produksi")) = "" Then
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("No Transaksi belum selesai produksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("Flag_Produksi")) = "" Then
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("No Transaksi belum diProduksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("No Transaksi tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim n_berat As Double = 0
            SQL = "select a.no_transaksi, a.no_production_Order,b.Kode_Barang,b.Kode_Stock_Owner, c.nama, "
            SQL = SQL & "sum(b.Qty_Hasil_Produksi) as Qty_Hasil_Produksi, sum(b.Qty_Good_Stock) as Qty_Good_Stock, "
            SQL = SQL & "sum(b.Qty_Bad_Stock) as Qty_Bad_Stock, b.satuan, sum(b.Qty_Scrap) as Qty_Scrap, b.Satuan_Scrap, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select sum(dbo.ubah_satuan(b.Kode_Perusahaan, 'masa', b.Kode_Barang_scrap, b.Satuan_Scrap, 'KG', b.Qty_Scrap)) "
            SQL = SQL & "from barang k where k.Kode_Perusahaan = d.Kode_Perusahaan and "
            SQL = SQL & "k.Kode_Stock_Owner = d.Kode_stock_Owner and k.Kode_Barang = b.Kode_Barang_scrap "
            SQL = SQL & "), 0) as Total_Qty_Scrap_Dan_Good_Bad_GR_KG, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select sum(dbo.ubah_satuan(b.Kode_Perusahaan, 'masa', b.Kode_Barang_scrap, b.Satuan_Scrap, 'Gram', b.Qty_Scrap)) "
            SQL = SQL & "from barang k where k.Kode_Perusahaan = d.Kode_Perusahaan and "
            SQL = SQL & "k.Kode_Stock_Owner = d.Kode_stock_Owner and k.Kode_Barang = b.Kode_Barang_scrap "
            SQL = SQL & "), 0) as Qty_Scrap_Dlm_GR, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select sum(dbo.ubah_satuan(b.Kode_Perusahaan, 'masa', b.Kode_Barang, b.Satuan, 'Gram', b.Qty_Hasil_Produksi)) "
            SQL = SQL & "from barang k where k.Kode_Perusahaan = d.Kode_Perusahaan and "
            SQL = SQL & "k.Kode_Stock_Owner = d.Kode_stock_Owner and k.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "), 0) as Qty_Dlm_GR,d.Berat "

            SQL = SQL & "from Emi_Production_Results a, EMI_Production_Results_Detail_Barang b, barang c,EMI_Order_Produksi d,Emi_Split_Production_Order f where "
            SQL = SQL & "a.kode_Perusahaan=b.kode_Perusahaan and a.no_transaksi=b.no_transaksi and a.status is null "
            SQL = SQL & "and d.status is null and a.Kode_Perusahaan = f.Kode_Perusahaan and a.No_Production_Order = f.No_Transaksi "
            SQL = SQL & "and d.Kode_Perusahaan = f.Kode_Perusahaan and f.no_po = d.No_Faktur "
            SQL = SQL & "and b.kode_Barang=c.Kode_Barang and b.KOde_Perusahaan=c.Kode_Perusahaan and b.kode_stock_Owner=c.kode_stock_Owner "
            SQL = SQL & "and a.Kode_Perusahaan ='" & KodePerusahaan & "' and a.No_Transaksi='" & TextBox4.Text & "' "
            SQL = SQL & "group by a.no_transaksi, a.no_production_Order,b.Kode_Barang, "
            SQL = SQL & "b.Kode_Stock_Owner, b.satuan, b.Satuan_Scrap, c.nama,d.Kode_Perusahaan,d.Kode_stock_Owner,b.Kode_Barang_Scrap,d.Berat "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    fkd_so = dr("Kode_Stock_Owner")
                    TextBox6.Text = dr("Kode_Barang")
                    TextBox2.Text = dr("nama")
                    TextBox7.Text = Format(dr("Qty_Hasil_Produksi"), "N2")
                    TextBox9.Text = Format(dr("Total_Qty_Scrap_Dan_Good_Bad_GR_KG"), "N2")
                    TextBox8.Text = dr("no_production_Order")

                    qty_scrap_gram = dr("Qty_Scrap_Dlm_GR")
                    qty_gram = dr("Qty_Dlm_GR")
                    TextBox10.Text = Format(qty_gram, "N2")
                    TextBox11.Text = Format(qty_scrap_gram, "N2")

                    n_berat = dr("berat")
                End If
            End Using

            TextBox15.Text = Format(qty_scrap_gram + qty_gram, "N2")

            Dim kode_barang_inq = ""
            SQL = "Select top(1) Kode_Barang_inq from barang "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and "
            SQL = SQL & "kode_Barang='" & TextBox6.Text & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    kode_barang_inq = dr("Kode_Barang_inq")
                End If
            End Using

            Dim No_faktur_simulasi As String = ""
            SQL = "Select top(1) No_Faktur From emi_hpp_simulasi Where "
            SQL = SQL & "Kode_Perusahaan='" & KodePerusahaan & "' and "
            SQL = SQL & "kode_Barang='" & kode_barang_inq & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    No_faktur_simulasi = dr("No_Faktur")
                End If
            End Using

            Dim nilai_bahan_simulasi As Double = 0
            Dim nilai_Packaging_Simulasi As Double = 0
            Dim nilai_Produksi_Simulasi As Double = 0
            SQL = "Select top(1) biaya_bahan_tertinggi, BIaya_Packaging_Tertinggi, "
            SQL = SQL & "Biaya_Produksi_Tertinggi From emi_hpp_simulasi_detail "
            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur='" & No_faktur_simulasi & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    nilai_bahan_simulasi = dr("biaya_bahan_tertinggi")
                    nilai_Packaging_Simulasi = dr("BIaya_Packaging_Tertinggi")
                    nilai_Produksi_Simulasi = dr("Biaya_Produksi_Tertinggi")
                End If
            End Using


            SQL = "select SUM(Nilai * dbo.get_hpp(Serial_Number)) as Nilai from Emi_Production_Results_Det where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & TextBox4.Text & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    Dim fnilai As Double = Math.Floor(dr("Nilai") / (qty_gram + qty_scrap_gram) * 100) / 100
                    'Dim fnilai As Double = dr("Nilai") / Val(HilangkanTanda(TextBox7.Text))
                    'Dim fnilai As Double = Math.Floor(dr("Nilai") / (qty_gram + qty_scrap_gram))
                    'Dim fnilai As Double = Val(HilangkanTanda(Format(dr("Nilai") / (qty_gram + qty_scrap_gram), "N2")))
                    'Dim ftotal As Double = fnilai * qty_gram
                    Dim ftotal2 As Double = fnilai * qty_scrap_gram
                    Dim ttl_pcs As Double = Math.Floor(fnilai * n_berat)
                    Dim fgrand As Double = ttl_pcs * Val(HilangkanTanda(TextBox7.Text))

                    DataGridView2.Rows.Add()
                    DataGridView2.Rows.Item(0).Cells(Cell3Nama).Value = "Bahan Baku"
                    DataGridView2.Rows.Item(0).Cells(Cell3Total).Value = Format(dr("Nilai"), "N2")

                    Dgv_HslProduction.Rows.Add()
                    Dgv_HslProduction.Rows.Item(0).Cells(CellNama).Value = "Bahan Baku"
                    Dgv_HslProduction.Rows.Item(0).Cells(CellReal).Value = Format(fnilai, "N2")
                    Dgv_HslProduction.Rows.Item(0).Cells(CellSatuan).Value = "Gram"
                    Dgv_HslProduction.Rows.Item(0).Cells(CellPengali).Value = Format(n_berat, "N2")
                    Dgv_HslProduction.Rows.Item(0).Cells(CellTotal).Value = Format(ttl_pcs, "N2")
                    Dgv_HslProduction.Rows.Item(0).Cells(CellGrand).Value = Format(fgrand, "N2")

                    DataGridView1.Rows.Add()
                    DataGridView1.Rows.Item(0).Cells(Cell2Nama).Value = "Bahan Baku"
                    DataGridView1.Rows.Item(0).Cells(Cell2Real).Value = Format(fnilai, "N2")
                    DataGridView1.Rows.Item(0).Cells(Cell2Satuan).Value = "Gram"
                    DataGridView1.Rows.Item(0).Cells(Cell2Grand).Value = Format(ftotal2, "N2")

                End If
            End Using

            SQL = "select SUM(Nilai * dbo.get_hpp(Serial_Number)) as Nilai from Emi_Production_Results_packaging_det where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & TextBox4.Text & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    Dim fnilai As Double = Math.Floor(dr("Nilai") / Val(HilangkanTanda(TextBox7.Text)) * 100) / 100
                    Dim fnilai2 As Double = Math.Floor(fnilai)
                    Dim fgrand As Double = fnilai * Val(HilangkanTanda(TextBox7.Text))

                    DataGridView2.Rows.Add()
                    DataGridView2.Rows.Item(1).Cells(Cell3Nama).Value = "Packaging"
                    DataGridView2.Rows.Item(1).Cells(Cell3Total).Value = Format(dr("Nilai"), "N2")

                    Dgv_HslProduction.Rows.Add()
                    Dgv_HslProduction.Rows.Item(1).Cells(CellNama).Value = "Packaging"
                    Dgv_HslProduction.Rows.Item(1).Cells(CellReal).Value = Format(fnilai, "N2")
                    Dgv_HslProduction.Rows.Item(1).Cells(CellSatuan).Value = "Pcs"
                    Dgv_HslProduction.Rows.Item(1).Cells(CellPengali).Value = "1"
                    Dgv_HslProduction.Rows.Item(1).Cells(CellTotal).Value = Format(fnilai2, "N2")
                    Dgv_HslProduction.Rows.Item(1).Cells(CellGrand).Value = Format(fgrand, "N2")

                End If
            End Using

            SQL = "select FORMAT(Tanggal,'yyyy') as tahun,FORMAT(Tanggal,'MM') as bulan from Emi_Production_Results where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & TextBox4.Text & "' and status is null "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    fbulan = dr("bulan")
                    ftahun = dr("tahun")
                End If
            End Using

            SQL = "select Isnull(SUM(b.Nilai_Per_Pcs),0) as nilai from Emi_Transaksi_Cost_Center a,Emi_Transaksi_Cost_Center_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & fbulan & "' and a.Tahun = '" & ftahun & "' "
            SQL = SQL & "and b.Kode_Barang = '" & TextBox6.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    Dim fnilai As Double = Math.Floor(dr("Nilai") / n_berat * 100) / 100
                    Dim ftotal As Double = fnilai * qty_scrap_gram
                    Dim ftotal2 As Double = Math.Floor(fnilai * n_berat)
                    Dim ftotal3 As Double = fnilai * (qty_gram + qty_scrap_gram)
                    Dim fGrand As Double = ftotal2 * Val(HilangkanTanda(TextBox7.Text))

                    DataGridView2.Rows.Add()
                    DataGridView2.Rows.Item(2).Cells(Cell3Nama).Value = "Cost Center"
                    DataGridView2.Rows.Item(2).Cells(Cell3Total).Value = Format(ftotal3, "N2")

                    Dgv_HslProduction.Rows.Add()
                    Dgv_HslProduction.Rows.Item(2).Cells(CellNama).Value = "Cost Center"
                    Dgv_HslProduction.Rows.Item(2).Cells(CellReal).Value = Format(fnilai, "N2")
                    Dgv_HslProduction.Rows.Item(2).Cells(CellSatuan).Value = "Gram"
                    Dgv_HslProduction.Rows.Item(2).Cells(CellPengali).Value = Format(n_berat, "N2")
                    Dgv_HslProduction.Rows.Item(2).Cells(CellTotal).Value = Format(ftotal2, "N2")
                    Dgv_HslProduction.Rows.Item(2).Cells(CellGrand).Value = Format(fGrand, "N2")


                    DataGridView1.Rows.Add()
                    DataGridView1.Rows.Item(1).Cells(Cell2Nama).Value = "Cost Center"
                    DataGridView1.Rows.Item(1).Cells(Cell2Real).Value = Format(fnilai, "N2")
                    DataGridView1.Rows.Item(1).Cells(Cell2Satuan).Value = "Gram"
                    DataGridView1.Rows.Item(1).Cells(Cell2Grand).Value = Format(ftotal, "N2")

                End If
            End Using




            SQL = "Select Isnull(SUM(b.Nilai_Per_Pcs),0) as nilai from "
            SQL = SQL & "EMI_Transaksi_Work_Center a, Emi_Transaksi_Work_Center_Detail_Per_Mesin b where "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & fbulan & "' and a.Tahun = '" & ftahun & "'  and a.Flag_Release='Y' "
            SQL = SQL & " And a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.no_faktur And b.Id_Routing = '" & fid_routing & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    Dim fnilai As Double = Math.Floor(dr("Nilai") / n_berat * 100) / 100
                    Dim ftotal As Double = fnilai * qty_scrap_gram
                    Dim ftotal2 As Double = Math.Floor(fnilai * n_berat)
                    Dim ftotal3 As Double = fnilai * (qty_gram + qty_scrap_gram)
                    Dim fGrand As Double = ftotal2 * Val(HilangkanTanda(TextBox7.Text))

                    DataGridView2.Rows.Add()
                    DataGridView2.Rows.Item(3).Cells(Cell3Nama).Value = "Work Center "
                    DataGridView2.Rows.Item(3).Cells(Cell3Total).Value = Format(ftotal3, "N2")

                    Dgv_HslProduction.Rows.Add()
                    Dgv_HslProduction.Rows.Item(3).Cells(CellNama).Value = "Work Center"
                    Dgv_HslProduction.Rows.Item(3).Cells(CellReal).Value = Format(fnilai, "N2")
                    Dgv_HslProduction.Rows.Item(3).Cells(CellSatuan).Value = "Gram"
                    Dgv_HslProduction.Rows.Item(3).Cells(CellPengali).Value = Format(n_berat, "N2")
                    Dgv_HslProduction.Rows.Item(3).Cells(CellTotal).Value = Format(ftotal2, "N2")
                    Dgv_HslProduction.Rows.Item(3).Cells(CellGrand).Value = Format(fGrand, "N2")

                    DataGridView1.Rows.Add()
                    DataGridView1.Rows.Item(2).Cells(Cell2Nama).Value = "Work Center "
                    DataGridView1.Rows.Item(2).Cells(Cell2Real).Value = Format(fnilai, "N2")
                    DataGridView1.Rows.Item(2).Cells(Cell2Satuan).Value = "Gram"
                    DataGridView1.Rows.Item(2).Cells(Cell2Grand).Value = Format(ftotal, "N2")

                End If
            End Using

            Dim freal As Double = 0
            Dim fgrand_data As Double = 0
            'Dim fsimulasi As Double = 0
            For a As Integer = 0 To Dgv_HslProduction.Rows.Count - 1
                Get_Isi_Listview(a)
                freal = freal + Val(HilangkanTanda(LvTotal))
                fgrand_data = fgrand_data + Val(HilangkanTanda(LvGrand))
                'fsimulasi = fsimulasi + (Val(HilangkanTanda(LvSimulasi)) * Val(HilangkanTanda(LvPengali)))
            Next
            TextBox1.Text = Format(freal, "N2")
            TextBox13.Text = Format(fgrand_data, "N2")

            Dim fgrand2 As Double = 0
            For a As Integer = 0 To DataGridView2.Rows.Count - 1
                Get_Isi_Listview3(a)
                fgrand2 = fgrand2 + Val(HilangkanTanda(Lv3Total))
            Next
            TextBox14.Text = Format(fgrand2, "N2")

            Dim fgrand3 As Double = 0
            Dim fHrg_satuan As Double = 0
            For a As Integer = 0 To DataGridView1.Rows.Count - 1
                Get_Isi_Listview2(a)
                fHrg_satuan = fHrg_satuan + Val(HilangkanTanda(Lv2Real))
                fgrand3 = fgrand3 + Val(HilangkanTanda(Lv2Grand))
            Next
            TextBox12.Text = Format(fHrg_satuan, "N2")
            TextBox5.Text = Format(fgrand3, "N2")

            get_no_faktur()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Dgv_HslProduction.Rows.Clear()
            DataGridView1.Rows.Clear()
            DataGridView2.Rows.Clear()
            TextBox7.Text = ""
            TextBox10.Text = ""
            TextBox9.Text = ""
            TextBox11.Text = ""
            TextBox14.Text = ""
            TextBox1.Text = ""
            TextBox13.Text = ""
            TextBox12.Text = ""
            TextBox5.Text = ""
            Exit Sub
        End Try
    End Sub

    Private Sub get_no_faktur()
        Dim FPro_Results As String = "HPP"
        TxtFormulator_NoFaktur.Text = FPro_Results & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("EMI_Transaksi_HPP", "No_Transaksi", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Transaksi, 1, " & Len(FPro_Results) + 4 & ")", FPro_Results & String.Format("{0:MMyy}", tgl_skg))
    End Sub

    Private Sub EMI_HPP_Production_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        If TxtFormulator_NoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Error_No_Transaksi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtFormulator_NoFaktur.Focus() : Exit Sub
        ElseIf TextBox7.Text.Trim.Length = 0 Then
            MessageBox.Show("Jumlah Produksi belum diisi", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf TextBox10.Text.Trim.Length = 0 Then
            MessageBox.Show("Jumlah Produksi dalam gram belum diisi", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf TextBox9.Text.Trim.Length = 0 Then
            MessageBox.Show("Jumlah scrap belum diisi", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf TextBox11.Text.Trim.Length = 0 Then
            MessageBox.Show("Jumlah scrap dalam gram belum diisi", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf TextBox14.Text.Trim.Length = 0 Then
            MessageBox.Show("Data HPP belum diisi", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf TextBox13.Text.Trim.Length = 0 Then
            MessageBox.Show("Data HPP goods issue belum diisi", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Data HPP goods issue belum diisi", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf TextBox12.Text.Trim.Length = 0 Then
            MessageBox.Show("Data HPP scrap belum diisi", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf TextBox5.Text.Trim.Length = 0 Then
            MessageBox.Show("Data HPP scrap belum diisi", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf Dgv_HslProduction.Rows.Count = 0 Then
            MessageBox.Show("Data HPP goods issue belum diisi ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        ElseIf DataGridView1.Rows.Count = 0 Then
            MessageBox.Show("Data HPP scrap belum diisi ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        ElseIf DataGridView2.Rows.Count = 0 Then
            MessageBox.Show("Data HPP belum diisi ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction
            get_no_faktur()

            SQL = "select b.flag_release, a.Flag_Hasil_Produksi, a.Flag_Produksi, b.Selesai, b.Id_Routing, c.Flag_HPP, c.Status "
            SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, Emi_Production_Results c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_PO = b.No_Faktur "
            SQL = SQL & "And a.Kode_Perusahaan = c.Kode_Perusahaan And a.No_Transaksi = c.No_Production_Order "
            SQL = SQL & "And c.Kode_Perusahaan ='" & KodePerusahaan & "' and c.No_Transaksi='" & TextBox4.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    fid_routing = dr("Id_Routing")
                    If General_Class.CekNULL(dr("Selesai")) <> "" Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("No Transaksi Sudah Selesai", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("flag_release")) = "" Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("No Transaksi belum direlease", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("Flag_Hasil_Produksi")) = "" Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("No Transaksi belum selesai produksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("Flag_Produksi")) = "" Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("No Transaksi belum diProduksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("Flag_HPP")) <> "" Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("No Transaksi sudah di buat HPP", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("Flag_HPP")) <> "" Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("No Transaksi sudah dibatalkan ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("No Transaksi tidak ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim N_bulat As Double = Val(HilangkanTanda(TextBox14.Text)) - Val(HilangkanTanda(TextBox13.Text)) - Val(HilangkanTanda(TextBox5.Text))

            SQL = "INSERT INTO EMI_Transaksi_HPP(Kode_Perusahaan,No_Transaksi,No_Production_Resault,"
            SQL = SQL & "Kode_Stock_Owner,Kode_Barang,Jumlah,HPP_Real,Jumlah_Scrap_Kg,Jumlah_Scrap_Gram,"
            SQL = SQL & "Jumlah_Gram,Harga_Satuan_Scrape,Total_Scrap,Total_HPP_Real,Grand,Pembulatan_HPP) "
            SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
            SQL = SQL & "'" & TextBox4.Text & "','" & fkd_so & "','" & TextBox6.Text & "',"
            SQL = SQL & "'" & HilangkanTanda(TextBox7.Text) & "','" & HilangkanTanda(TextBox1.Text) & "',"
            SQL = SQL & "'" & HilangkanTanda(TextBox9.Text) & "','" & HilangkanTanda(TextBox11.Text) & "',"
            SQL = SQL & "'" & HilangkanTanda(TextBox10.Text) & "','" & HilangkanTanda(TextBox12.Text) & "',"
            SQL = SQL & "'" & HilangkanTanda(TextBox5.Text) & "','" & HilangkanTanda(TextBox13.Text) & "',"
            SQL = SQL & "'" & HilangkanTanda(TextBox14.Text) & "','" & N_bulat & "')"
            ExecuteTrans(SQL)

            For a As Integer = 0 To DataGridView2.Rows.Count - 1
                Get_Isi_Listview3(a)

                SQL = "INSERT INTO EMI_Transaksi_HPP_Detail(Kode_Perusahaan,No_Transaksi,Nama,Total) VALUES("
                SQL = SQL & "'" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
                SQL = SQL & "'" & Lv3Nama & "','" & HilangkanTanda(Lv3Total) & "')"
                ExecuteTrans(SQL)
            Next

            For a As Integer = 0 To DataGridView1.Rows.Count - 1
                Get_Isi_Listview2(a)

                SQL = "INSERT INTO EMI_Transaksi_HPP_Scrap(Kode_Perusahaan,No_Transaksi,Nama,Harga_Satuan,Satuan,Grand) "
                SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
                SQL = SQL & "'" & Lv2Nama & "','" & HilangkanTanda(Lv2Real) & "','" & Lv2Satuan & "',"
                SQL = SQL & "'" & HilangkanTanda(Lv2Grand) & "')"
                ExecuteTrans(SQL)
            Next

            For a As Integer = 0 To Dgv_HslProduction.Rows.Count - 1
                Get_Isi_Listview(a)

                SQL = "INSERT INTO EMI_Transaksi_HPP_Per_Pcs(Kode_Perusahaan,No_Transaksi,Nama,Harga_Satuan,Satuan,Pengali,Total_Per_Pcs,Grand) "
                SQL = SQL & "VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
                SQL = SQL & "'" & LvNama & "','" & HilangkanTanda(LvReal) & "','" & LvSatuan & "','" & HilangkanTanda(LvPengali) & "',"
                SQL = SQL & "'" & HilangkanTanda(LvTotal) & "','" & HilangkanTanda(LvGrand) & "')"
                ExecuteTrans(SQL)
            Next

            SQL = "select No_Transaksi,Kode_Stock_Owner,Kode_Barang,Nilai,Serial_Number,dbo.get_hpp(Serial_Number) as hpp "
            SQL = SQL & "from Emi_Production_Results_Det where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & TextBox4.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For h As Integer = 0 To .Rows.Count - 1
                            Dim ftotal As Double = .Rows(h).Item("nilai") * .Rows(h).Item("hpp")

                            SQL = "INSERT INTO EMI_Transaksi_HPP_Bahan_Baku(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,"
                            SQL = SQL & "Nilai,Serial_Number,Harga,Total) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
                            SQL = SQL & "'" & .Rows(h).Item("Kode_Stock_Owner") & "','" & .Rows(h).Item("Kode_Barang") & "',"
                            SQL = SQL & "'" & .Rows(h).Item("Nilai") & "','" & .Rows(h).Item("Serial_Number") & "',"
                            SQL = SQL & "'" & .Rows(h).Item("hpp") & "','" & ftotal & "')"
                            ExecuteTrans(SQL)
                        Next
                    End If
                End With
            End Using

            SQL = "select No_Transaksi,Kode_Stock_Owner,Kode_Barang,Nilai,Serial_Number,dbo.get_hpp(Serial_Number) as hpp "
            SQL = SQL & "from Emi_Production_Results_packaging_det where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & TextBox4.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For h As Integer = 0 To .Rows.Count - 1
                            Dim ftotal2 As Double = .Rows(h).Item("nilai") * .Rows(h).Item("hpp")

                            SQL = "INSERT INTO EMI_Transaksi_HPP_Packaging(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,"
                            SQL = SQL & "Nilai,Serial_Number,Harga,Total) VALUES('" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
                            SQL = SQL & "'" & .Rows(h).Item("Kode_Stock_Owner") & "','" & .Rows(h).Item("Kode_Barang") & "',"
                            SQL = SQL & "'" & .Rows(h).Item("Nilai") & "','" & .Rows(h).Item("Serial_Number") & "',"
                            SQL = SQL & "'" & .Rows(h).Item("hpp") & "','" & ftotal2 & "')"
                            ExecuteTrans(SQL)
                        Next
                    End If
                End With
            End Using

            SQL = "select b.Kode_Stock_Owner,b.Kode_Barang,b.Id_Cost_Center,b.Nilai_Per_Pcs "
            SQL = SQL & "from Emi_Transaksi_Cost_Center a,Emi_Transaksi_Cost_Center_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & fbulan & "' and a.Tahun = '" & ftahun & "' "
            SQL = SQL & "and b.Kode_Barang = '" & TextBox6.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For h As Integer = 0 To .Rows.Count - 1
                            Dim fnilai As Double = .Rows(h).Item("Nilai_Per_Pcs") * Val(HilangkanTanda(TextBox7.Text))

                            SQL = "INSERT INTO EMI_Transaksi_HPP_Cost_Center(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,"
                            SQL = SQL & "Kode_Barang,Id_Cost_Center,Nilai_Per_Pcs) VALUES('" & KodePerusahaan & "',"
                            SQL = SQL & "'" & TxtFormulator_NoFaktur.Text & "','" & .Rows(h).Item("Kode_Stock_Owner") & "',"
                            SQL = SQL & "'" & .Rows(h).Item("Kode_Barang") & "','" & .Rows(h).Item("Id_Cost_Center") & "',"
                            SQL = SQL & "'" & fnilai & "')"
                            ExecuteTrans(SQL)
                        Next
                    End If
                End With
            End Using

            'SQL = "select a.Id_Work_Center,a.Nilai_Per_Pcs,a.Jenis_Biaya "
            'SQL = SQL & "from Emi_Transaksi_Work_Center_Detail_Per_Mesin a, "
            'SQL = SQL & "EMI_Master_Routing_Detail c,Emi_Transaksi_Work_Center d where d.Status is null "
            'SQL = SQL & "and d.Kode_Perusahaan = a.Kode_Perusahaan and d.No_Faktur = a.No_Faktur "
            'SQL = SQL & "and c.Kode_Perusahaan = a.Kode_Perusahaan and c.Id_Work_Center = a.Id_Work_Center "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and d.Bulan = '" & fbulan & "' and d.Tahun = '" & ftahun & "' "
            'SQL = SQL & " and c.Id_Routing = '" & fid_routing & "' "

            SQL = "Select b.Id_Work_Center,b.Nilai_Per_Pcs,b.Jenis_Biaya from "
            SQL = SQL & "EMI_Transaksi_Work_Center a, Emi_Transaksi_Work_Center_Detail_Per_Mesin b where "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & fbulan & "' and a.Tahun = '" & ftahun & "'  and a.Flag_Release='Y' "
            SQL = SQL & " And a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.no_faktur And b.Id_Routing = '" & fid_routing & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For h As Integer = 0 To .Rows.Count - 1
                            Dim fnilai As Double = .Rows(h).Item("Nilai_Per_Pcs") * Val(HilangkanTanda(TextBox7.Text))

                            SQL = "INSERT INTO EMI_Transaksi_HPP_Work_Center(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,"
                            SQL = SQL & "Kode_Barang,Id_Work_Center,Nilai_Per_Pcs,Jenis_Biaya) VALUES('" & KodePerusahaan & "',"
                            SQL = SQL & "'" & TxtFormulator_NoFaktur.Text & "','" & "" & "',"
                            SQL = SQL & "'" & TextBox6.Text & "','" & .Rows(h).Item("Id_Work_Center") & "',"
                            SQL = SQL & "'" & fnilai & "','" & .Rows(h).Item("Jenis_Biaya") & "')"
                            ExecuteTrans(SQL)
                        Next
                    End If
                End With
            End Using

            SQL = "UPDATE Emi_Production_Results SET Flag_HPP = 'Y',Tgl_HPP = '" & String.Format("{0:yyyy-MM-dd}", tgl_skg) & "',"
            SQL = SQL & "Jam_HPP = '" & Format(tgl_skg, "HH:mm:ss") & "',UserID_HPP = '" & UserID & "' "
            SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' AND No_Transaksi = '" & TextBox4.Text & "' "
            ExecuteTrans(SQL)

            'awal Jurnal stenly

            Dim inisial_faktur_dari As String = ""
            SQL = "select Inisial_Faktur from Stock_Owner_Gudang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Kode_Stock_Owner = '" & fkd_so & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    inisial_faktur_dari = Dr("inisial_faktur")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            '==================
            '=     JURNAL     =
            '==================
            Dim Kode_voucher As String = ""
            Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
            Dim pagenumber As Integer = 1

            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
            SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
            SQL = SQL & "'" & Kode_voucher & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
            SQL = SQL & "'" & KodeProyek & "', 'HPP Production " & TxtFormulator_NoFaktur.Text & "', '', "
            SQL = SQL & "'-', '" & UserID & "')"
            ExecuteTrans(SQL)

            Dim nilai_HPP_Barang_Setengah_Jadi As Double = 0
            Dim nilai_persedian_brg_dlm_proses As Double = 0

            Dim akun_persedian_brg_dlm_proses As String = ""
            Dim akun_penyusutan_barang_dalam_proses As String = ""
            Dim akun_biaya_cost_center As String = ""

            Dim ket As String = ""
            Dim ket2 As String = ""
            Dim ket3 As String = ""

            'awal persediaan barang dalam proses
            SQL = "select Persediaan_Barang_Dalam_Proses,Biaya_Cost_Center,Penyusutan_Barang_Dalam_Proses from stock_owner_gudang "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & fkd_so & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    akun_persedian_brg_dlm_proses = Dr("Persediaan_Barang_Dalam_Proses")
                    ket = "Persediaan Barang Dalam Proses "

                    akun_biaya_cost_center = Dr("Biaya_Cost_Center")
                    ket2 = "Biaya Cost Center "

                    akun_penyusutan_barang_dalam_proses = Dr("Penyusutan_Barang_Dalam_Proses")
                    ket3 = "Penyusutan Barang Dalam Proses "
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim akun_biaya_listrik As String = ""
            Dim akun_biaya_air As String = ""
            Dim akun_biaya_bb As String = ""
            Dim akun_biaya_gaji As String = ""

            Dim budget_biaya_listrik As String = ""
            Dim budget_biaya_air As String = ""
            Dim budget_biaya_bb As String = ""
            Dim budget_biaya_gaji As String = ""
            Dim budget_biaya_cost_center As String = ""

            Dim flokasi As String = ""
            SQL = "select b.No_Transaksi,b.Lokasi from Emi_Production_Results a,Emi_Split_Production_Order b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Production_Order = b.No_Transaksi "
            SQL = SQL & "and a.Status is null and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & TextBox4.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    flokasi = Dr("Lokasi")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select Kode_Stock_Owner,akun_biaya_listrik,akun_biaya_air,akun_biaya_bb,akun_biaya_gaji,"
            SQL = SQL & "Budget_Biaya_Air,Budget_Biaya_Listrik,Budget_Biaya_BB,Budget_Biaya_Gaji,Budget_Biaya_Cost_Center "
            SQL = SQL & "from Stock_Owner where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & flokasi & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    akun_biaya_listrik = Dr("akun_biaya_listrik")
                    akun_biaya_air = Dr("akun_biaya_air")
                    akun_biaya_bb = Dr("akun_biaya_bb")
                    akun_biaya_gaji = Dr("akun_biaya_gaji")

                    budget_biaya_cost_center = Dr("budget_biaya_cost_center")
                    budget_biaya_listrik = Dr("budget_biaya_listrik")
                    budget_biaya_air = Dr("budget_biaya_air")
                    budget_biaya_bb = Dr("budget_biaya_bb")
                    budget_biaya_gaji = Dr("budget_biaya_gaji")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim fFlag_Semi_FG As String = ""
            Dim fFlag_Finished_Good As String = ""
            Dim Akun_Semi_FG As String = ""

            Dim akun_HPP_Barang_Setengah_Jadi As String = ""
            SQL = "select b.Kode_Barang,d.Flag_Semi_FG,d.Flag_Finished_Good "
            SQL = SQL & "from Emi_Production_Results a,Emi_Split_Production_Order b,Barang c,EMI_Group_Jenis d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Production_Order = b.No_Transaksi "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
            SQL = SQL & "and b.Kode_Barang = c.Kode_Barang and c.Kode_Perusahaan = d.Kode_Perusahaan and a.status is null "
            SQL = SQL & "and c.Id_Group_Jenis = d.Id_Group_Jenis and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & TextBox4.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For h As Integer = 0 To .Rows.Count - 1
                            fFlag_Semi_FG = .Rows(h).Item("Flag_Semi_FG")
                            fFlag_Finished_Good = .Rows(h).Item("Flag_Finished_Good")
                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            Dim Akun_Pembulatan As String = ""
            Dim keterangan As String = ""
            Dim keterangan2 As String = ""
            Dim keterangan3 As String = ""

            SQL = "select Persediaan_Bahan_Setengah_Jadi,HPP_Barang_Setengah_Jadi,"
            SQL = SQL & "Persediaan,HPP,Pembulatan_Finished_Good,Pembulatan_Semi_FG from stock_owner_gudang "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & fkd_so & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If fFlag_Semi_FG = "Y" Then
                        Akun_Semi_FG = Dr("Persediaan_Bahan_Setengah_Jadi")
                        keterangan = "Persediaan Barang Setengah Jadi "

                        akun_HPP_Barang_Setengah_Jadi = Dr("HPP_Barang_Setengah_Jadi")
                        keterangan2 = "HPP Barang Setengah Jadi "

                        Akun_Pembulatan = Dr("Pembulatan_Semi_FG")
                        keterangan3 = "Pembulatan HPP Barang Setengah Jadi "
                    ElseIf fFlag_Finished_Good = "Y" Then
                        Akun_Semi_FG = Dr("Persediaan")
                        keterangan = "Persediaan Barang Jadi "

                        akun_HPP_Barang_Setengah_Jadi = Dr("HPP")
                        keterangan2 = "HPP Barang Jadi "

                        Akun_Pembulatan = Dr("Pembulatan_Finished_Good")
                        keterangan3 = "Pembulatan HPP Barang Jadi "
                    End If

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select Isnull(sum(Total),0) as Total from EMI_Transaksi_HPP_Bahan_Baku where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Transaksi = '" & TxtFormulator_NoFaktur.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    nilai_persedian_brg_dlm_proses = nilai_persedian_brg_dlm_proses + Dr("Total")
                    nilai_HPP_Barang_Setengah_Jadi = nilai_HPP_Barang_Setengah_Jadi + Dr("Total")
                End If
            End Using

            SQL = "select Isnull(sum(Total),0) as Total from EMI_Transaksi_HPP_Packaging where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Transaksi = '" & TxtFormulator_NoFaktur.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    nilai_persedian_brg_dlm_proses = nilai_persedian_brg_dlm_proses + Dr("Total")
                    nilai_HPP_Barang_Setengah_Jadi = nilai_HPP_Barang_Setengah_Jadi + Dr("Total")
                End If
            End Using

            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persedian_brg_dlm_proses, 1),
                                  Strings.Mid(akun_persedian_brg_dlm_proses, 2, 1),
                                  Strings.Mid(Ganti(akun_persedian_brg_dlm_proses), 3),
                                  KodePerusahaan, KodeProyek, ket & TxtFormulator_NoFaktur.Text, "0", nilai_persedian_brg_dlm_proses, pagenumber, "TSSS")
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1
            'akhir persediaan barang dalam proses

            'awal penyusutan barang dalm proses
            Dim nilai_penyusutan_brg_dlm_proses As Double = 0
            Dim jumlahConvertBhn As Double = 0
            SQL = "select a.Kode_Stock_Owner,a.Kode_Barang,a.Nilai,b.Satuan,c.Satuan_Barang "
            SQL = SQL & "from Emi_Production_Results_Det a,Barang b,Emi_Production_Results_Detail c where  "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = c.No_Transaksi and a.Kode_Stock_Owner = c.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & TextBox4.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For h As Integer = 0 To .Rows.Count - 1

                            SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(h).Item("Kode_Barang") & "',"
                            SQL = SQL & "'" & .Rows(h).Item("Satuan_Barang") & "','" & .Rows(h).Item("Satuan") & "',"
                            SQL = SQL & "" & .Rows(h).Item("nilai") & ") as Hasil "
                            Using dr4 = OpenTrans(SQL)
                                If dr4.Read Then
                                    If General_Class.CekNULL(dr4("Hasil")) <> "" Then
                                        If dr4("Hasil") = 0 Then
                                            dr4.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Satuan " & .Rows(h).Item("Satuan_Barang") & " Ke " & .Rows(h).Item("Satuan") & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        Else
                                            jumlahConvertBhn = jumlahConvertBhn + dr4("hasil")
                                        End If
                                    Else
                                        dr4.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Satuan " & .Rows(h).Item("Satuan_Barang") & " Ke " & .Rows(h).Item("Satuan") & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using

                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            Dim berat_lost As Double = jumlahConvertBhn - Val(HilangkanTanda(TextBox10.Text)) - Val(HilangkanTanda(TextBox11.Text))
            Get_Isi_Listview3(0)
            Dim nilai_lost_per_pcs As Double = Math.Floor(Val(HilangkanTanda(Lv3Total)) / jumlahConvertBhn * 100) / 100

            nilai_penyusutan_brg_dlm_proses = berat_lost * nilai_lost_per_pcs

            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_penyusutan_barang_dalam_proses, 1),
                                  Strings.Mid(akun_penyusutan_barang_dalam_proses, 2, 1),
                                  Strings.Mid(Ganti(akun_penyusutan_barang_dalam_proses), 3),
                                  KodePerusahaan, KodeProyek, ket3 & TxtFormulator_NoFaktur.Text, "0", nilai_penyusutan_brg_dlm_proses, pagenumber, "TSSS")
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            nilai_HPP_Barang_Setengah_Jadi = nilai_HPP_Barang_Setengah_Jadi + nilai_penyusutan_brg_dlm_proses
            'awal penyusutan barang dalm proses

            'awal biaya cost center
            Dim nilai_biaya_cost_center As Double = 0

            SQL = "select Isnull(sum(Nilai_Per_Pcs),0) as Nilai from EMI_Transaksi_HPP_Cost_Center "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Transaksi = '" & TxtFormulator_NoFaktur.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    nilai_biaya_cost_center = nilai_biaya_cost_center + Dr("Nilai")
                    nilai_HPP_Barang_Setengah_Jadi = nilai_HPP_Barang_Setengah_Jadi + Dr("Nilai")
                End If
            End Using

            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_biaya_cost_center, 1),
                                  Strings.Mid(akun_biaya_cost_center, 2, 1),
                                  Strings.Mid(Ganti(akun_biaya_cost_center), 3),
                                  KodePerusahaan, KodeProyek, ket2 & TxtFormulator_NoFaktur.Text, "0", nilai_biaya_cost_center, pagenumber, "TSSS")
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1
            'akhir biaya cost center

            'awal biaya work center
            Dim nilai_biaya_listrik As Double = 0
            Dim nilai_biaya_air As Double = 0
            Dim nilai_biaya_bb As Double = 0
            Dim nilai_biaya_gaji As Double = 0

            SQL = "select Jenis_Biaya,Isnull(sum(Nilai_Per_Pcs),0) as nilai from EMI_Transaksi_HPP_Work_Center "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Transaksi = '" & TxtFormulator_NoFaktur.Text & "' "
            SQL = SQL & "group by Jenis_Biaya "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For h As Integer = 0 To .Rows.Count - 1
                            nilai_HPP_Barang_Setengah_Jadi = nilai_HPP_Barang_Setengah_Jadi + .Rows(h).Item("nilai")

                            If .Rows(h).Item("Jenis_Biaya") = "Biaya_Listrik" Then
                                nilai_biaya_listrik = .Rows(h).Item("nilai")

                                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_biaya_listrik, 1),
                                        Strings.Mid(akun_biaya_listrik, 2, 1),
                                        Strings.Mid(Ganti(akun_biaya_listrik), 3),
                                        KodePerusahaan, KodeProyek, "Biaya Listrik " & TxtFormulator_NoFaktur.Text, "0", nilai_biaya_listrik, pagenumber, "TSSS")
                                ExecuteTrans(SQL)
                                pagenumber = pagenumber + 1
                            ElseIf .Rows(h).Item("Jenis_Biaya") = "Biaya_Air" Then
                                nilai_biaya_air = .Rows(h).Item("nilai")

                                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_biaya_air, 1),
                                        Strings.Mid(akun_biaya_air, 2, 1),
                                        Strings.Mid(Ganti(akun_biaya_air), 3),
                                        KodePerusahaan, KodeProyek, "Biaya Air " & TxtFormulator_NoFaktur.Text, "0", nilai_biaya_air, pagenumber, "TSSS")
                                ExecuteTrans(SQL)
                                pagenumber = pagenumber + 1
                            ElseIf .Rows(h).Item("Jenis_Biaya") = "Biaya_Bahan_Bakar" Then
                                nilai_biaya_bb = .Rows(h).Item("nilai")

                                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_biaya_bb, 1),
                                        Strings.Mid(akun_biaya_bb, 2, 1),
                                        Strings.Mid(Ganti(akun_biaya_bb), 3),
                                        KodePerusahaan, KodeProyek, "Biaya Bahan Bakar " & TxtFormulator_NoFaktur.Text, "0", nilai_biaya_bb, pagenumber, "TSSS")
                                ExecuteTrans(SQL)
                                pagenumber = pagenumber + 1
                            ElseIf .Rows(h).Item("Jenis_Biaya") = "Biaya_Gaji" Then
                                nilai_biaya_gaji = .Rows(h).Item("nilai")

                                SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_biaya_gaji, 1),
                                        Strings.Mid(akun_biaya_gaji, 2, 1),
                                        Strings.Mid(Ganti(akun_biaya_gaji), 3),
                                        KodePerusahaan, KodeProyek, "Biaya Air " & TxtFormulator_NoFaktur.Text, "0", nilai_biaya_gaji, pagenumber, "TSSS")
                                ExecuteTrans(SQL)
                                pagenumber = pagenumber + 1
                                'Else
                                '    CloseTrans()
                                '    CloseConn()
                                '    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '    Exit Sub
                            End If
                        Next
                    End If
                End With
            End Using
            'akhir biaya work center

            'jurnal pembulatan
            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(Akun_Pembulatan, 1),
                                  Strings.Mid(Akun_Pembulatan, 2, 1),
                                  Strings.Mid(Ganti(Akun_Pembulatan), 3),
                                  KodePerusahaan, KodeProyek, keterangan3 & TxtFormulator_NoFaktur.Text, "0", N_bulat, pagenumber, "TSSS")
            ExecuteTrans(SQL)
            pagenumber = pagenumber + 1

            nilai_HPP_Barang_Setengah_Jadi = nilai_HPP_Barang_Setengah_Jadi + N_bulat

            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_HPP_Barang_Setengah_Jadi, 1),
                     Strings.Mid(akun_HPP_Barang_Setengah_Jadi, 2, 1),
                     Strings.Mid(Ganti(akun_HPP_Barang_Setengah_Jadi), 3),
                     KodePerusahaan, KodeProyek, keterangan2 & TxtFormulator_NoFaktur.Text, nilai_HPP_Barang_Setengah_Jadi, "0", pagenumber, "TSSS")
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

            'awal jurnal budget
            Dim Kode_voucher2 As String = ""
            Kode_voucher2 = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
            Dim pagenumber2 As Integer = 1

            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
            SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
            SQL = SQL & "'" & Kode_voucher2 & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
            SQL = SQL & "'" & KodeProyek & "', 'Budget Biaya " & TxtFormulator_NoFaktur.Text & "', '', "
            SQL = SQL & "'-', '" & UserID & "')"
            ExecuteTrans(SQL)

            SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(akun_biaya_cost_center, 1),
                     Strings.Mid(akun_biaya_cost_center, 2, 1),
                     Strings.Mid(Ganti(akun_biaya_cost_center), 3),
                     KodePerusahaan, KodeProyek, "Budget Biaya Cost Center " & TxtFormulator_NoFaktur.Text, nilai_biaya_cost_center, "0", pagenumber2, "TSSS")
            ExecuteTrans(SQL)
            pagenumber2 = pagenumber2 + 1

            SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(budget_biaya_cost_center, 1),
                                  Strings.Mid(budget_biaya_cost_center, 2, 1),
                                  Strings.Mid(Ganti(budget_biaya_cost_center), 3),
                                  KodePerusahaan, KodeProyek, "Budget Biaya Cost Center " & TxtFormulator_NoFaktur.Text, "0", nilai_biaya_cost_center, pagenumber2, "TSSS")
            ExecuteTrans(SQL)
            pagenumber2 = pagenumber2 + 1

            SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(akun_biaya_listrik, 1),
                     Strings.Mid(akun_biaya_listrik, 2, 1),
                     Strings.Mid(Ganti(akun_biaya_listrik), 3),
                     KodePerusahaan, KodeProyek, "Budget Biaya Listrik " & TxtFormulator_NoFaktur.Text, nilai_biaya_listrik, "0", pagenumber2, "TSSS")
            ExecuteTrans(SQL)
            pagenumber2 = pagenumber2 + 1

            SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(budget_biaya_listrik, 1),
                                  Strings.Mid(budget_biaya_listrik, 2, 1),
                                  Strings.Mid(Ganti(budget_biaya_listrik), 3),
                                  KodePerusahaan, KodeProyek, "Budget Biaya Listrik " & TxtFormulator_NoFaktur.Text, "0", nilai_biaya_listrik, pagenumber2, "TSSS")
            ExecuteTrans(SQL)
            pagenumber2 = pagenumber2 + 1

            SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(akun_biaya_air, 1),
                     Strings.Mid(akun_biaya_air, 2, 1),
                     Strings.Mid(Ganti(akun_biaya_air), 3),
                     KodePerusahaan, KodeProyek, "Budget Biaya Air " & TxtFormulator_NoFaktur.Text, nilai_biaya_air, "0", pagenumber2, "TSSS")
            ExecuteTrans(SQL)
            pagenumber2 = pagenumber2 + 1

            SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(budget_biaya_air, 1),
                                  Strings.Mid(budget_biaya_air, 2, 1),
                                  Strings.Mid(Ganti(budget_biaya_air), 3),
                                  KodePerusahaan, KodeProyek, "Budget Biaya Air " & TxtFormulator_NoFaktur.Text, "0", nilai_biaya_air, pagenumber2, "TSSS")
            ExecuteTrans(SQL)
            pagenumber2 = pagenumber2 + 1

            SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(akun_biaya_bb, 1),
                     Strings.Mid(akun_biaya_bb, 2, 1),
                     Strings.Mid(Ganti(akun_biaya_bb), 3),
                     KodePerusahaan, KodeProyek, "Budget Biaya Bahan Bakar " & TxtFormulator_NoFaktur.Text, nilai_biaya_bb, "0", pagenumber2, "TSSS")
            ExecuteTrans(SQL)
            pagenumber2 = pagenumber2 + 1

            SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(budget_biaya_bb, 1),
                                  Strings.Mid(budget_biaya_bb, 2, 1),
                                  Strings.Mid(Ganti(budget_biaya_bb), 3),
                                  KodePerusahaan, KodeProyek, "Budget Biaya Bahan Bakar " & TxtFormulator_NoFaktur.Text, "0", nilai_biaya_bb, pagenumber2, "TSSS")
            ExecuteTrans(SQL)
            pagenumber2 = pagenumber2 + 1

            SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(akun_biaya_gaji, 1),
                     Strings.Mid(akun_biaya_gaji, 2, 1),
                     Strings.Mid(Ganti(akun_biaya_gaji), 3),
                     KodePerusahaan, KodeProyek, "Budget Biaya Gaji " & TxtFormulator_NoFaktur.Text, nilai_biaya_gaji, "0", pagenumber2, "TSSS")
            ExecuteTrans(SQL)
            pagenumber2 = pagenumber2 + 1

            SQL = Get_Detail_Jurnal(Kode_voucher2, Strings.Left(budget_biaya_gaji, 1),
                                  Strings.Mid(budget_biaya_gaji, 2, 1),
                                  Strings.Mid(Ganti(budget_biaya_gaji), 3),
                                  KodePerusahaan, KodeProyek, "Budget Biaya Gaji " & TxtFormulator_NoFaktur.Text, "0", nilai_biaya_gaji, pagenumber2, "TSSS")
            ExecuteTrans(SQL)
            pagenumber2 = pagenumber2 + 1

            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & Kode_voucher2 & "'"
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
            'akhir jurnal budget

            'awal jurnal HPP Persediaan Barang Setengah Jadi
            Dim Kode_voucher3 As String = ""
            Kode_voucher3 = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
            Dim pagenumber3 As Integer = 1

            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
            SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
            SQL = SQL & "'" & Kode_voucher3 & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
            SQL = SQL & "'" & KodeProyek & "', 'HPP Persediaan Barang Setengah Jadi " & TxtFormulator_NoFaktur.Text & "', '', "
            SQL = SQL & "'-', '" & UserID & "')"
            ExecuteTrans(SQL)

            SQL = Get_Detail_Jurnal(Kode_voucher3, Strings.Left(Akun_Semi_FG, 1),
                     Strings.Mid(Akun_Semi_FG, 2, 1),
                     Strings.Mid(Ganti(Akun_Semi_FG), 3),
                     KodePerusahaan, KodeProyek, keterangan & TxtFormulator_NoFaktur.Text, nilai_HPP_Barang_Setengah_Jadi, "0", pagenumber3, "TSSS")
            ExecuteTrans(SQL)
            pagenumber3 = pagenumber3 + 1

            SQL = Get_Detail_Jurnal(Kode_voucher3, Strings.Left(akun_HPP_Barang_Setengah_Jadi, 1),
                                  Strings.Mid(akun_HPP_Barang_Setengah_Jadi, 2, 1),
                                  Strings.Mid(Ganti(akun_HPP_Barang_Setengah_Jadi), 3),
                                  KodePerusahaan, KodeProyek, keterangan2 & TxtFormulator_NoFaktur.Text, "0", nilai_HPP_Barang_Setengah_Jadi, pagenumber3, "TSSS")
            ExecuteTrans(SQL)
            pagenumber3 = pagenumber3 + 1

            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & Kode_voucher3 & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("debit") <> Dr("kredit") Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Jurnal 3 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data jurnal 3 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using
            'akhir jurnal HPP Persediaan Barang Setengah Jadi

            Dim Kode_voucher4 As String = ""
            Kode_voucher4 = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
            Dim pagenumber4 As Integer = 1

            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
            SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
            SQL = SQL & "'" & Kode_voucher4 & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
            SQL = SQL & "'" & KodeProyek & "', 'Penyusutan Barang " & TxtFormulator_NoFaktur.Text & "', '', "
            SQL = SQL & "'-', '" & UserID & "')"
            ExecuteTrans(SQL)

            SQL = Get_Detail_Jurnal(Kode_voucher4, Strings.Left(akun_penyusutan_barang_dalam_proses, 1),
                     Strings.Mid(akun_penyusutan_barang_dalam_proses, 2, 1),
                     Strings.Mid(Ganti(akun_penyusutan_barang_dalam_proses), 3),
                     KodePerusahaan, KodeProyek, ket3 & TxtFormulator_NoFaktur.Text, nilai_penyusutan_brg_dlm_proses, "0", pagenumber4, "TSSS")
            ExecuteTrans(SQL)
            pagenumber4 = pagenumber4 + 1

            SQL = Get_Detail_Jurnal(Kode_voucher4, Strings.Left(akun_persedian_brg_dlm_proses, 1),
                                  Strings.Mid(akun_persedian_brg_dlm_proses, 2, 1),
                                  Strings.Mid(Ganti(akun_persedian_brg_dlm_proses), 3),
                                  KodePerusahaan, KodeProyek, ket & TxtFormulator_NoFaktur.Text, "0", nilai_penyusutan_brg_dlm_proses, pagenumber4, "TSSS")
            ExecuteTrans(SQL)
            pagenumber4 = pagenumber4 + 1

            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & Kode_voucher4 & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("debit") <> Dr("kredit") Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Jurnal 4 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data jurnal 4 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim Kode_voucher5 As String = ""
            Kode_voucher5 = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
            Dim pagenumber5 As Integer = 1

            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
            SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
            SQL = SQL & "'" & Kode_voucher5 & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
            SQL = SQL & "'" & KodeProyek & "', 'Pembulatan HPP " & TxtFormulator_NoFaktur.Text & "', '', "
            SQL = SQL & "'-', '" & UserID & "')"
            ExecuteTrans(SQL)

            SQL = Get_Detail_Jurnal(Kode_voucher5, Strings.Left(Akun_Pembulatan, 1),
                     Strings.Mid(Akun_Pembulatan, 2, 1),
                     Strings.Mid(Ganti(Akun_Pembulatan), 3),
                     KodePerusahaan, KodeProyek, "Pembulatan HPP " & TxtFormulator_NoFaktur.Text, N_bulat, "0", pagenumber5, "TSSS")
            ExecuteTrans(SQL)
            pagenumber5 = pagenumber5 + 1

            SQL = Get_Detail_Jurnal(Kode_voucher5, Strings.Left(akun_persedian_brg_dlm_proses, 1),
                                  Strings.Mid(akun_persedian_brg_dlm_proses, 2, 1),
                                  Strings.Mid(Ganti(akun_persedian_brg_dlm_proses), 3),
                                  KodePerusahaan, KodeProyek, "Pembulatan HPP " & TxtFormulator_NoFaktur.Text, "0", N_bulat, pagenumber5, "TSSS")
            ExecuteTrans(SQL)
            pagenumber5 = pagenumber5 + 1

            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_voucher = '" & Kode_voucher5 & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("debit") <> Dr("kredit") Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Jurnal 4 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data jurnal 4 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select No_Production_Order from Emi_Production_Results where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Transaksi = '" & TextBox4.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    SQL = "update Emi_Split_Production_Order set "
                    SQL = SQL & "Kode_Voucher = '" & Kode_voucher & "',"
                    SQL = SQL & "Kode_Voucher2 = '" & Kode_voucher2 & "',"
                    SQL = SQL & "Kode_Voucher3 = '" & Kode_voucher3 & "',"
                    SQL = SQL & "Kode_Voucher4 = '" & Kode_voucher4 & "',"
                    SQL = SQL & "Kode_Voucher5 = '" & Kode_voucher5 & "' "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "No_Transaksi = '" & Dr("No_Production_Order") & "' "
                    Dr.Close()
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data production result tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            'akhir jurnal stenly

            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        EMI_Display_Hasil_HPP.Button1_Click(Btn_Refresh, e)
        Me.Close()
    End Sub

    Private Sub DetailToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DetailToolStripMenuItem.Click
        Get_Isi_Listview(Dgv_HslProduction.CurrentRow.Index)
        If LvNama = "Biaya Bahan Baku" Then
            EMI_HPP_Production_Detail.nno_transaksi = TextBox4.Text
            EMI_HPP_Production_Detail.asal = "Biaya Bahan Baku"
            EMI_HPP_Production_Detail.ShowDialog()
        ElseIf LvNama = "Biaya Packaging" Then
            EMI_HPP_Production_Detail.nno_transaksi = TextBox4.Text
            EMI_HPP_Production_Detail.asal = "Biaya Packaging"
            EMI_HPP_Production_Detail.ShowDialog()
        ElseIf LvNama = "Biaya Produksi" Then
            EMI_HPP_Production_Detail.nbulan = fbulan
            EMI_HPP_Production_Detail.ntahun = ftahun
            EMI_HPP_Production_Detail.nkd_so = fkd_so
            EMI_HPP_Production_Detail.nkd_brg = TextBox6.Text
            EMI_HPP_Production_Detail.asal = "Biaya Produksi"
            EMI_HPP_Production_Detail.ShowDialog()
        End If
    End Sub
End Class